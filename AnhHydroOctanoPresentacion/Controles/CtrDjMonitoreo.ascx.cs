
using AnhHydroOctanoPresentacion.Lib;

namespace AnhHydroOctanoPresentacion.Controles
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Web;
    using System.Web.UI;
    using AnhHydroOctanoPersistencia.Aplicacion.Consulta;
    using AnhHydroOctanoPersistencia.Entidades;
    using Clases.Controles;
    using Clases.Util;
    using Entidades;
    using Enumeradores;
    using Parametros;
    using DevExpress.Utils;
    using DevExpress.Web;
    using DevExpress.XtraPivotGrid;
    using Librerias.Anh.Us;
    using ServiceStack.ServiceClient.Web;
    using ServiceStack.Text;
    using CArbol = Clases.Controles.CArbol;
    using AnhHydroOctanoPresentacion.Sitio.Reportes;

    public partial class CtrDjMonitoreo : UserControl
    {
        #region Variables de entorno

        readonly JsonServiceClient _client = new JsonServiceClient(CAppSettings.servicio_octVolumen);
        private readonly CPersistenciaConsulta _consulta = new CPersistenciaConsulta();
        readonly CUtil _cUtil = new CUtil();
        CConsultasParametricas cConsultasParametricas = new CConsultasParametricas();

        public string[] listaF1;

        #endregion

        #region Eventos de Formulario

        protected void Page_Load(object sender, EventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    if (!Page.IsCallback && !Page.IsPostBack)
                    {
                        Session.Remove("PATH");
                        Session.Remove(CVariablesSesion.EstructuraArbol);
                        Session.Remove(CVariablesSesion.GrillaAcumRep);
                        Session.Remove(CVariablesSesion.GrillaAcumulado);
                        Session.Remove(CVariablesSesion.GrillaAuxiliar);
                        Session.Remove(CVariablesSesion.GrillaCabecera);
                        Session.Remove(CVariablesSesion.GrillaHistorial);
                        Session.Remove(CVariablesSesion.GrillaInternacional);
                        Session.Remove(CVariablesSesion.ListaPerfiles);
                        Session.Remove(CVariablesSesion.ListaPeriodos);
                        Session.Remove(CVariablesSesion.OctanoParametrica);
                        Session.Remove(CVariablesSesion.ReportePrincipal);
                        Session.Remove(CVariablesSesion.ReporteSecundario);
                        Session.Remove(CVariablesSesion.TipoReporte);
                        Session.Remove(CVariablesSesion.GrillaRecaudo);
                        Session.Remove(CVariablesSesion.GrillaListaAuxiliar);
                        
                        MCargaArbol();
                        
                        tlsOperadores.DataSource = Session[CVariablesSesion.EstructuraArbol];
                        tlsOperadores.DataBind();

                        MCargaPermisos();
                        MCargaDatosIniciales();

                        if (Session[CVariablesSesion.EntidadSeleccionada] != null)
                        {
                            var strSel = Session[CVariablesSesion.EntidadSeleccionada].ToString();
                            var vecSel = strSel.Split('-');
                            switch (vecSel.Count())
                            {
                                case 1:
                                    tlsOperadores.Nodes[Convert.ToInt16(vecSel[0])].Focus();
                                    break;
                                case 2:
                                    tlsOperadores.Nodes[Convert.ToInt16(vecSel[0])].ChildNodes[Convert.ToInt16(vecSel[1])].Focus();
                                    break;
                                case 3:
                                    tlsOperadores.Nodes[Convert.ToInt16(vecSel[0])].ChildNodes[Convert.ToInt16(vecSel[1])].ChildNodes[Convert.ToInt16(vecSel[2])].Focus();
                                    break;
                            }
                        }
                    }

                    if (tlsOperadores.DataSource == null && Session[CVariablesSesion.EstructuraArbol] != null)
                    {
                        tlsOperadores.DataSource = Session[CVariablesSesion.EstructuraArbol];
                        tlsOperadores.DataBind();
                    }

                    if (dgvCabecera.DataSource == null && Session[CVariablesSesion.GrillaCabecera] != null)
                    {
                        dgvCabecera.DataSource = Session[CVariablesSesion.GrillaCabecera];
                        dgvCabecera.DataBind();
                    }

                    if (pvgAcumulado.DataSource == null && Session[CVariablesSesion.GrillaAcumulado] != null)
                    {
                        pvgAcumulado.DataSource = Session[CVariablesSesion.GrillaAcumulado];
                        pvgAcumulado.DataBind();
                    }

                    if (dgvInternacional.DataSource == null && Session[CVariablesSesion.GrillaInternacional] != null)
                    {
                        dgvInternacional.DataSource = Session[CVariablesSesion.GrillaInternacional];
                        dgvInternacional.DataBind();
                    }

                    if (dgvContenedor.DataSource == null && Session[CVariablesSesion.GrillaAuxiliar] != null)
                    {
                        dgvContenedor.DataSource = Session[CVariablesSesion.GrillaAuxiliar];
                        dgvContenedor.DataBind();
                    }

                    if (dgvRecaudo.DataSource == null && Session[CVariablesSesion.GrillaRecaudo] != null)
                    {
                        dgvRecaudo.DataSource = Session[CVariablesSesion.GrillaRecaudo];
                        dgvRecaudo.DataBind();
                    }

                    #endregion
                }
                catch (Exception exp)
                {
                    #region RegistraError
                    var oMensajeUsuario = new CLogTraza.MensajeUsuario
                    {
                        decUsuarioId = Session[CVariablesSesion.IdUsuario],
                        decIdModulo = CParametrosHydro.decIdAplicacion,
                        strAccion = GetType().Name + "." + MethodBase.GetCurrentMethod().Name,
                        strIp = HttpContext.Current.Request.UserHostAddress,
                        decNivelCapa = (int)CLogTraza.CapasNivel.Presentacion
                    };
                    CLogTraza.Error(oMensajeUsuario, exp);
                    #endregion
                }
            }
            else
            {
                //todo No implementado.
            }
        }

        protected void pnlDatos_Callback(object sender, CallbackEventArgsBase e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    Session[CVariablesSesion.EntidadSeleccionada] = tlsOperadores.FocusedNode.GetValue("StrUbicacion");

                    lblRazonSocial.Text = tlsOperadores.FocusedNode.GetValue("StrDescPadre").ToString();
                    lblNit.Text = tlsOperadores.FocusedNode.GetValue("LngNit").ToString();
                    lblDireccion.Text = (tlsOperadores.FocusedNode.GetValue("StrDireccion")==null)?"":tlsOperadores.FocusedNode.GetValue("StrDireccion").ToString();
                    lblDepto.Text = tlsOperadores.FocusedNode.GetValue("StrDepartamento").ToString();
                    MLimpiaGrilla();
                    if ((int)tlsOperadores.FocusedNode.GetValue("IntAccion") > 0)
                    {
                        string strMensaje = string.Empty;
                        if (MValidaDatosIniciales(ref strMensaje))
                        {
                            int intDia = DateTime.DaysInMonth((int)cmbGestion.Value, (int)cmbMes.Value);

                            var intIdAct = Convert.ToInt32(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                            switch (intIdAct)
                            {
                                case (int)EnumActividades.EstacionServicioLiquido:
                                case (int)EnumActividades.EstacionServicioGnv:
                                    tacMovimientos.Tabs[1].ClientVisible = true;
                                    tacMovimientos.Tabs[3].ClientVisible = true;
                                    break;
                                case (int)EnumActividades.TerminalAeropuertoLiq:
                                    tacMovimientos.Tabs[2].ClientVisible = true;
                                    break;
                            }

                            int intFiltro = 1;
                            string strDato1 = tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString();
                            string strDato2 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString();
                            string strDato3 = cmbGestion.Value + cmbMes.Value.ToString().PadLeft(2, '0') + "01000000";
                            string strDato4 = cmbGestion.Value + cmbMes.Value.ToString().PadLeft(2, '0') + intDia.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + "235959";
                            MCargaParametros();
                            MConfiguraGrilla(Convert.ToInt32(strDato2));
                            MCargaGrilla(intFiltro, strDato1, strDato2, strDato3, strDato4);
                            if (intIdAct == 16) {
                                btnReimprimir.Visible = false;
                                lblImprimirCertificado.Visible = true;
                                txtNumCertificado.Visible = true;
                                btnReimprimirCertificado.Visible = true;
                                btnImprimirFormulario.Visible = true;
                            }
                            else{
                                btnReimprimir.Visible = true;
                                lblImprimirCertificado.Visible = false;
                                txtNumCertificado.Visible = false;
                                btnReimprimirCertificado.Visible = false;
                                btnImprimirFormulario.Visible = false;
                            }
                        }
                        else
                        {
                            MImprimirMensaje(strMensaje, EnumTiposMensajeAlerta.Error);
                        }
                    }
                    else
                    {
                        //todo no implementado
                    }

                    #endregion
                }
                catch (Exception exp)
                {
                    #region RegistraError

                    MLimpiaGrilla();
                    MImprimirMensaje(exp.Message, EnumTiposMensajeAlerta.Error);
                    var oMensajeUsuario = new CLogTraza.MensajeUsuario
                    {
                        decUsuarioId = Session[CVariablesSesion.IdUsuario],
                        decIdModulo = CParametrosHydro.decIdAplicacion,
                        strAccion = GetType().Name + "." + MethodBase.GetCurrentMethod().Name,
                        strIp = HttpContext.Current.Request.UserHostAddress,
                        decNivelCapa = (int)CLogTraza.CapasNivel.Presentacion
                    };
                    CLogTraza.Error(oMensajeUsuario, exp);

                    #endregion
                }
            }
            else
            {
                //todo no implementado
            }
        }
        
        protected void dgvCabecera_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            try
            {
                #region Proceso a Ejecutar

                if (e.VisibleIndex >= 0)
                {
                    switch (dgvCabecera.GetRowValues(e.VisibleIndex, "ESTADO").ToString())
                    {
                        case "2":
                            e.Row.BackColor = Color.FromArgb(215, 135, 135);
                            break;
                        case "3":
                            e.Row.BackColor = Color.FromArgb(143, 179, 157);
                            break;
                        case "4":
                            e.Row.BackColor = Color.FromArgb(171, 209, 184);
                            break;
                        default:
                            e.Row.BackColor = Color.FromArgb(175, 207, 167);
                            break;
                    }
                }
                else
                {
                    //todo No implementado.
                }

                #endregion
            }
            catch (Exception exp)
            {
                #region RegistraError

                MImprimirMensaje(exp.Message, EnumTiposMensajeAlerta.Error);
                var oMensajeUsuario = new CLogTraza.MensajeUsuario
                {
                    decUsuarioId = Session[CVariablesSesion.IdUsuario],
                    decIdModulo = CParametrosHydro.decIdAplicacion,
                    strAccion = GetType().Name + "." + MethodBase.GetCurrentMethod().Name,
                    strIp = HttpContext.Current.Request.UserHostAddress,
                    decNivelCapa = (int)CLogTraza.CapasNivel.Presentacion
                };
                CLogTraza.Error(oMensajeUsuario, exp);

                #endregion
            }
        }

        protected void dgvCabecera_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    var index = dgvCabecera.FocusedRowIndex;
                    var lngidCite = Convert.ToInt64(dgvCabecera.GetRowValues(index, "ID_CITE"));
                    var intEstado = Convert.ToInt32(dgvCabecera.GetRowValues(index, "ESTADO"));
                    var decAppIdUs = Convert.ToDecimal(dgvCabecera.GetRowValues(index, "APP_ID_USUARIO"));
                    var decFechaRep = Convert.ToDecimal(dgvCabecera.GetRowValues(index, "FECHA_REPORTE"));
                    
                    if (lngidCite > 0)
                    {
                        var datFechaReg = DateTime.Now;
                        EResultado resultado;
                        switch (e.ButtonID)
                        {
                            case "btnActivaSal":
                                resultado = _client.Post<EResultado>(CAppSettings.svc_actualiza_reporte_estado,
                                new
                                {
                                    strCredencial = CParametrosHydro.strCredencialFuncionario,
                                    decFiltro = 4,
                                    decIdReporteDatoPrin = 0,
                                    decIdCite = lngidCite,
                                    decEstado = 1,
                                    decFechaReporte = decFechaRep,
                                    strObservaciones = "",
                                    decAppFechaRegistro = decFechaRep,
                                    decAppIdUsuario = decAppIdUs
                                });
                                if (resultado.intCodigo == 1)
                                {
                                    pnlDatos_Callback(null, null);
                                }
                                else
                                {
                                    MImprimirMensaje(resultado.strMensaje, EnumTiposMensajeAlerta.Alerta);
                                }
                                break;
                            case "btnAnulaSal":
                                resultado = _client.Post<EResultado>(CAppSettings.svc_actualiza_reporte_estado,
                                new
                                {
                                    strCredencial = CParametrosHydro.strCredencialFuncionario,
                                    decFiltro = 4,
                                    decIdReporteDatoPrin = 0,
                                    decIdCite = lngidCite,
                                    decEstado = 2,
                                    decFechaReporte = decFechaRep,
                                    strObservaciones = "",
                                    decAppFechaRegistro = decFechaRep,
                                    decAppIdUsuario = decAppIdUs
                                });
                                if (resultado.intCodigo == 1)
                                {
                                    pnlDatos_Callback(null, null);
                                }
                                else
                                {
                                    MImprimirMensaje(resultado.strMensaje, EnumTiposMensajeAlerta.Alerta);
                                }
                                break;
                            case "btnAnulaUor":
                                if (intEstado != 2)
                                {
                                    resultado = _client.Post<EResultado>(CAppSettings.svc_actualiza_reporte_estado,
                                    new
                                    {
                                        strCredencial = CParametrosHydro.strCredencialFuncionario,
                                        decFiltro = 3,
                                        decIdReporteDatoPrin = 0,
                                        decIdCite = lngidCite,
                                        decEstado = 2,
                                        decFechaReporte = decFechaRep,
                                        strObservaciones = "",
                                        decAppFechaRegistro = decFechaRep,
                                        decAppIdUsuario = decAppIdUs
                                    });
                                    if (resultado.intCodigo != 1)
                                    {
                                        MImprimirMensaje(resultado.strMensaje, EnumTiposMensajeAlerta.Alerta);
                                    }
                                    else
                                    {
                                        pnlDatos_Callback(null, null);
                                    }
                                }
                                else
                                {
                                    MImprimirMensaje("No puede ejecutar la función debido a que la declaración se encuentra anulada.", EnumTiposMensajeAlerta.Alerta);
                                }
                                break;
                            case "btnAnular":
                                if (intEstado != 2)
                                {
                                    //valida periodo
                                    var lstPeriodos = Session[CVariablesSesion.ListaPeriodos] as List<O_PERIODO_CTY>;
                                    if (lstPeriodos.Any())
                                    {
                                        var lstValida = lstPeriodos.Where(x => x.decAño == Convert.ToDecimal(cmbGestion.Value.ToString()) && x.decMes == Convert.ToDecimal(cmbMes.Value.ToString())).ToList();
                                        if (lstValida.Any())
                                        {
                                            resultado = _client.Post<EResultado>(CAppSettings.svc_actualiza_reporte_estado,
                                            new
                                            {
                                                strCredencial = CParametrosHydro.strCredencialFuncionario,
                                                decFiltro = 2,
                                                decIdReporteDatoPrin = 0,
                                                decIdCite = lngidCite,
                                                decEstado = 2,
                                                decFechaReporte = CFechas.ConvierteDateTimeLong(datFechaReg),//Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0')),
                                                strObservaciones = "",
                                                decAppFechaRegistro = CFechas.ConvierteDateTimeLong(datFechaReg),//Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0')),
                                                decAppIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario])
                                            });
                                            if (resultado.intCodigo != 1)
                                            {
                                                MImprimirMensaje(resultado.strMensaje, EnumTiposMensajeAlerta.Alerta);
                                            }
                                            else
                                            {
                                                pnlDatos_Callback(null, null);
                                            }
                                        }
                                        else
                                        {
                                            MImprimirMensaje("Periodo no habilitado para la función.", EnumTiposMensajeAlerta.Alerta);
                                        }
                                    }
                                    else
                                    {
                                        MImprimirMensaje("Periodo no habilitado para la función.", EnumTiposMensajeAlerta.Alerta);
                                    }
                                }
                                else
                                {
                                    MImprimirMensaje("No puede ejecutar la función debido a que la declaración se encuentra anulada.", EnumTiposMensajeAlerta.Alerta);
                                }
                                break;
                            case "btnRecepcionar":
                                if (intEstado == 2)
                                {
                                    MImprimirMensaje("No puede ejecutar la función debido a que la declaración se encuentra anulada.", EnumTiposMensajeAlerta.Alerta);
                                }
                                else if (intEstado == 1)
                                {
                                    resultado = _client.Post<EResultado>(CAppSettings.svc_actualiza_reporte_estado,
                                    new
                                    {
                                        strCredencial = CParametrosHydro.strCredencialFuncionario,
                                        decFiltro = 2,
                                        decIdReporteDatoPrin = 0,
                                        decIdCite = lngidCite,
                                        decEstado = 3,
                                        decFechaReporte = CFechas.ConvierteDateTimeLong(datFechaReg),//Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0')),
                                        strObservaciones = "",
                                        decAppFechaRegistro = CFechas.ConvierteDateTimeLong(datFechaReg),//Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0')),
                                        decAppIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario])
                                    });
                                    if (resultado.intCodigo != 1)
                                    {
                                        MImprimirMensaje(resultado.strMensaje, EnumTiposMensajeAlerta.Alerta);
                                    }
                                    else
                                    {
                                        pnlDatos_Callback(null, null);
                                    }
                                }
                                else
                                {
                                    MImprimirMensaje("No puede ejecutar la función debido a que la declaración jurada ya fue recepcionada.", EnumTiposMensajeAlerta.Alerta);
                                }
                                break;
                        }
                    }
                    else
                    {
                        //todo No implementado.
                    }

                    #endregion
                }
                catch (Exception exp)
                {
                    #region RegistraError

                    MImprimirMensaje(exp.Message, EnumTiposMensajeAlerta.Error);
                    var oMensajeUsuario = new CLogTraza.MensajeUsuario
                    {
                        decUsuarioId = Session[CVariablesSesion.IdUsuario],
                        decIdModulo = CParametrosHydro.decIdAplicacion,
                        strAccion = GetType().Name + "." + MethodBase.GetCurrentMethod().Name,
                        strIp = HttpContext.Current.Request.UserHostAddress,
                        decNivelCapa = (int)CLogTraza.CapasNivel.Presentacion
                    };
                    CLogTraza.Error(oMensajeUsuario, exp);

                    #endregion
                }
            }
            else
            {
                //todo No implementado.
            }
        }
        
        protected void dgvCabecera_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
        {
            if (!MValidaSesion()) return;
            try
            {
                #region Proceso a Ejecutar

                if (e.VisibleIndex == -1) return;

                if (e.ButtonID == "btnPdf")
                {
                    e.Visible = Convert.ToInt16(Session[CVariablesSesion.PermisoAlta]) == 1 ? DefaultBoolean.True : DefaultBoolean.False;
                }
                if (e.ButtonID == "btnAnular")
                {
                    e.Visible = Convert.ToInt16(Session[CVariablesSesion.PermisoBaja]) == 1 ? DefaultBoolean.True : DefaultBoolean.False;
                }
                if (e.ButtonID == "btnAdjuntos")
                {
                    e.Visible = Convert.ToInt16(Session[CVariablesSesion.PermisoModi]) == 1 ? DefaultBoolean.True : DefaultBoolean.False;
                }
                if (e.ButtonID == "btnRecepcionar")
                {
                    e.Visible = Convert.ToInt16(Session[CVariablesSesion.PermisoRevi]) == 1 ? DefaultBoolean.True : DefaultBoolean.False;
                }
                if (e.ButtonID == "btnAnulaUor")
                {
                    e.Visible = Convert.ToInt16(Session[CVariablesSesion.PermisoApro]) == 1 ? DefaultBoolean.True : DefaultBoolean.False;
                }
                if (e.ButtonID == "btnActivaSal")
                {
                    e.Visible = Convert.ToInt16(Session[CVariablesSesion.PermisoApro]) == 1 ? DefaultBoolean.True : DefaultBoolean.False;
                }
                if (e.ButtonID == "btnAnulaSal")
                {
                    e.Visible = Convert.ToInt16(Session[CVariablesSesion.PermisoApro]) == 1 ? DefaultBoolean.True : DefaultBoolean.False;
                }
                
                #endregion
            }
            catch (Exception exp)
            {
                #region RegistraError

                MImprimirMensaje(exp.Message, EnumTiposMensajeAlerta.Error);
                var oMensajeUsuario = new CLogTraza.MensajeUsuario
                {
                    decUsuarioId = Session[CVariablesSesion.IdUsuario],
                    decIdModulo = CParametrosHydro.decIdAplicacion,
                    strAccion = GetType().Name + "." + MethodBase.GetCurrentMethod().Name,
                    strIp = HttpContext.Current.Request.UserHostAddress,
                    decNivelCapa = (int)CLogTraza.CapasNivel.Presentacion
                };
                CLogTraza.Error(oMensajeUsuario, exp);

                #endregion
            }
        }

        protected void pucHistorial_WindowCallback(object source, PopupWindowCallbackArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    int index = dgvCabecera.FocusedRowIndex;
                    txtCiteDj.Text = dgvCabecera.GetRowValues(index, "CITE_GENERADO").ToString();

                    var lstDeclaraciones = _client.Post<EResultadoListadoHistorialDj>
                        (CAppSettings.svc_listar_historial_reporte_datos_principal,
                        new
                        {
                            strCredencial = CParametrosHydro.strCredencialListadoDj,
                            decFiltro = "1", //TipoMovimiento (1=ventas)
                            strParametro1 = dgvCabecera.GetRowValues(index, "ID_REPORTE_DATO_PRINC").ToString().Trim(), //Id proyecto/entidad
                            strParametro2 = "0",
                            strParametro3 = "0",
                            strParametro4 = "0"
                        });
                    if (lstDeclaraciones.IntCodigo == 1)
                    {
                        var LstResultado = new List<O_HISTORIAL_REPORTE_DATOSP_CTY>();
                        LstResultado = lstDeclaraciones.OResultado;

                        if (LstResultado.Any())
                        {
                            LstResultado.ForEach(a => a.strFecha = a.APP_FECHA_REGISTRO.ToString(CultureInfo.InvariantCulture).Substring(6, 2) + "/" + a.APP_FECHA_REGISTRO.ToString(CultureInfo.InvariantCulture).Substring(4, 2) + "/" + a.APP_FECHA_REGISTRO.ToString(CultureInfo.InvariantCulture).Substring(0, 4) + " " + a.APP_FECHA_REGISTRO.ToString(CultureInfo.InvariantCulture).Substring(8, 2) + ":" + a.APP_FECHA_REGISTRO.ToString(CultureInfo.InvariantCulture).Substring(10, 2) + ":" + a.APP_FECHA_REGISTRO.ToString(CultureInfo.InvariantCulture).Substring(12, 2));
                            LstResultado.ForEach(a => a.strEstado = ((a.ESTADO == 1) ? "DECLARADO" : ((a.ESTADO == 2) ? "ANULADO" : ((a.ESTADO == 3) ? "RECEPCIONADO" : "ARCH.ADJUNTO"))));
                            LstResultado = LstResultado.OrderBy(a => a.APP_FECHA_REGISTRO).ToList();

                            Session[CVariablesSesion.GrillaHistorial] = LstResultado;
                            dgvHistorico.DataSource = LstResultado;
                            dgvHistorico.DataBind();
                        }
                        else
                        {
                            //todo No implementado.
                        }
                    }
                    else
                    {
                        MImprimirMensaje(lstDeclaraciones.StrMensaje, EnumTiposMensajeAlerta.Alerta);
                    }

                    #endregion
                }
                catch (Exception exp)
                {
                    #region RegistraError

                    MImprimirMensaje(exp.Message, EnumTiposMensajeAlerta.Error);
                    var oMensajeUsuario = new CLogTraza.MensajeUsuario
                    {
                        decUsuarioId = Session[CVariablesSesion.IdUsuario],
                        decIdModulo = CParametrosHydro.decIdAplicacion,
                        strAccion = GetType().Name + "." + MethodBase.GetCurrentMethod().Name,
                        strIp = HttpContext.Current.Request.UserHostAddress,
                        decNivelCapa = (int)CLogTraza.CapasNivel.Presentacion
                    };
                    CLogTraza.Error(oMensajeUsuario, exp);

                    #endregion
                }
            }
            else
            {
                //todo No implementado.
            }
        }

        protected void dgvHistorico_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            try
            {
                #region Proceso a Ejecutar

                if (e.VisibleIndex >= 0)
                {
                    if (e.VisibleIndex % 2 == 1)
                    {
                        e.Row.BackColor = Color.FromArgb(253, 237, 212);
                    }
                    else
                    {
                        //todo No implementado.
                    }
                }
                else
                {
                    //todo No implementado.
                }

                #endregion
            }
            catch (Exception exp)
            {
                #region RegistraError

                MImprimirMensaje(exp.Message, EnumTiposMensajeAlerta.Error);
                var oMensajeUsuario = new CLogTraza.MensajeUsuario
                {
                    decUsuarioId = Session[CVariablesSesion.IdUsuario],
                    decIdModulo = CParametrosHydro.decIdAplicacion,
                    strAccion = GetType().Name + "." + MethodBase.GetCurrentMethod().Name,
                    strIp = HttpContext.Current.Request.UserHostAddress,
                    decNivelCapa = (int)CLogTraza.CapasNivel.Presentacion
                };
                CLogTraza.Error(oMensajeUsuario, exp);

                #endregion
            }
        }

        protected void pvgAcumulado_CustomCallback(object sender, DevExpress.Web.ASPxPivotGrid.PivotGridCustomCallbackEventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    Session.Remove(CVariablesSesion.GrillaAcumulado);
                    pvgAcumulado.DataSource = null;
                    pvgAcumulado.DataBind();

                    int index = dgvCabecera.FocusedRowIndex;
                    string strJsonReportes = dgvCabecera.GetRowValues(index, "JSON_REPORTE_DATOS").ToString();
                    var objDdjj = JsonSerializer.DeserializeFromString<O_DECLARACION_JURADA_CTY>(strJsonReportes);

                    var lstAcumulado = objDdjj.LstAcumulado;

                    lstAcumulado.ForEach(a => a.DecIdUnicoHidrocarburo = Convert.ToDecimal(a.DecIdHidrocarburo.ToString() + ((a.DecIdTipoInfraestructura == null) ? "" : a.DecIdTipoInfraestructura.ToString().Replace("-", "")) + ((a.DecIdInfraestructura == null) ? "" : a.DecIdInfraestructura.ToString().Replace("-", ""))));

                    if (lstAcumulado.Any())
                    {
                        #region acumulado
                        decimal decProd = 0;
                        decimal decGrProd = 0;
                        decimal decGrupoProd = 0;
                        int cont = 1, intContProd = 1, IntContAgrup = 0;

                        lstAcumulado = lstAcumulado.OrderBy(x => x.DecIdHidrocarburo).ThenBy(x => x.StrTipoInfraestructura).ThenBy(x => x.StrInfraestructura).ThenBy(x => x.DecFecha).ToList();
                        var lstAcumRep = new List<CMovAcumuladoRep>();
                        var lstMovimientos = new List<CMovAcumulado>();
                        foreach (var lst in lstAcumulado)
                        {
                            var cDato = new CMovAcumulado();
                            var cDatR = new CMovAcumuladoRep();
                            if (decProd != lst.DecIdUnicoHidrocarburo)
                            {
                                decProd = lst.DecIdUnicoHidrocarburo;
                                intContProd = 1;
                                IntContAgrup++;
                                decGrupoProd++;
                            }
                            if (decGrProd != lst.DecIdHidrocarburo)
                            {
                                decGrProd = lst.DecIdHidrocarburo;
                                decGrupoProd = 1;
                            }

                            cDato.IntId = cont;
                            cDato.IntContProd = intContProd;
                            cDato.StrFecha = lst.DecFecha.ToString().Substring(6, 2) + "/" + lst.DecFecha.ToString().Substring(4, 2) + "/" + lst.DecFecha.ToString().Substring(0, 4);
                            cDato.IntIdProducto = (int)lst.DecIdHidrocarburo;
                            cDato.IntContAgrup = IntContAgrup;
                            cDato.StrProducto = lst.StrHidrocarburo;
                            cDato.StrCodProducto = lst.StrCodHidrocarburo;
                            cDato.StrUnidadMedida = lst.StrUnidadMedida;
                            cDato.StrTipoInfraestructura = lst.StrTipoInfraestructura ?? "";
                            cDato.StrInfraestructura = lst.StrInfraestructura ?? "";
                            cDato.StrCodigoInfraestructura = lst.StrInfraestructuraAnh ?? "";
                            cDato.DecSaldoInicial = lst.DecSaldoInicial;
                            cDato.DecTotalIng = lst.DecTotalIngreso;
                            cDato.DecSaldoFinal = lst.DecSaldoFinal;
                            cDato.DecTotalSal = lst.DecTotalEgreso;

                            cDatR.StrFecha = cDato.StrFecha;
                            cDatR.IntIdProducto = cDato.IntIdProducto;
                            cDatR.StrProducto = cDato.StrProducto;
                            cDatR.IntGrupoProducto = Convert.ToInt32(decGrupoProd);
                            cDatR.StrUnidadMedida = cDato.StrUnidadMedida;
                            cDatR.StrInfraestructura = cDato.StrTipoInfraestructura + " - " + cDato.StrInfraestructura;
                            cDatR.DecSaldoInicial = cDato.DecSaldoInicial;
                            cDatR.DecTotalIng = cDato.DecTotalIng;
                            cDatR.DecSaldoFinal = cDato.DecSaldoFinal;
                            cDatR.DecTotalSal = cDato.DecTotalSal;

                            if (lst.LstIngresos == null || !lst.LstIngresos.Any())
                            {
                                cDato.DecIng01 = -99999;
                            }
                            else
                            {
                                foreach (var reg in lst.LstIngresos)
                                {
                                    if ((int) reg.OPERACION_ORDEN > 0)
                                    {
                                        if (!pvgAcumulado.Fields[6 + (int)reg.OPERACION_ORDEN].Visible)
                                        {
                                            pvgAcumulado.Fields[6 + (int)reg.OPERACION_ORDEN].Caption = reg.OPERACION_NOMBRE;
                                            pvgAcumulado.Fields[6 + (int)reg.OPERACION_ORDEN].Visible = true;
                                        }
                                        cDato.DecIng01 = (reg.OPERACION_ORDEN == 1) ? reg.CANTIDAD : cDato.DecIng01;
                                        cDato.DecIng02 = (reg.OPERACION_ORDEN == 2) ? reg.CANTIDAD : cDato.DecIng02;
                                        cDato.DecIng03 = (reg.OPERACION_ORDEN == 3) ? reg.CANTIDAD : cDato.DecIng03;
                                        cDato.DecIng04 = (reg.OPERACION_ORDEN == 4) ? reg.CANTIDAD : cDato.DecIng04;
                                        cDato.DecIng05 = (reg.OPERACION_ORDEN == 5) ? reg.CANTIDAD : cDato.DecIng05;
                                        cDato.DecIng06 = (reg.OPERACION_ORDEN == 6) ? reg.CANTIDAD : cDato.DecIng06;
                                        cDato.DecIng07 = (reg.OPERACION_ORDEN == 7) ? reg.CANTIDAD : cDato.DecIng07;
                                        cDato.DecIng08 = (reg.OPERACION_ORDEN == 8) ? reg.CANTIDAD : cDato.DecIng08;
                                        cDato.DecIng09 = (reg.OPERACION_ORDEN == 9) ? reg.CANTIDAD : cDato.DecIng09;
                                        cDato.DecIng10 = (reg.OPERACION_ORDEN == 10) ? reg.CANTIDAD : cDato.DecIng10;
                                        cDato.DecIng11 = (reg.OPERACION_ORDEN == 11) ? reg.CANTIDAD : cDato.DecIng11;
                                        cDato.DecIng12 = (reg.OPERACION_ORDEN == 12) ? reg.CANTIDAD : cDato.DecIng12;
                                        cDato.DecIng13 = (reg.OPERACION_ORDEN == 13) ? reg.CANTIDAD : cDato.DecIng13;
                                        cDato.DecIng14 = (reg.OPERACION_ORDEN == 14) ? reg.CANTIDAD : cDato.DecIng14;
                                        cDato.DecIng15 = (reg.OPERACION_ORDEN == 15) ? reg.CANTIDAD : cDato.DecIng15;
                                        cDato.DecIng16 = (reg.OPERACION_ORDEN == 16) ? reg.CANTIDAD : cDato.DecIng16;
                                        cDato.DecIng17 = (reg.OPERACION_ORDEN == 17) ? reg.CANTIDAD : cDato.DecIng17;
                                        cDato.DecIng18 = (reg.OPERACION_ORDEN == 18) ? reg.CANTIDAD : cDato.DecIng18;
                                        cDato.DecIng19 = (reg.OPERACION_ORDEN == 19) ? reg.CANTIDAD : cDato.DecIng19;
                                        cDato.DecIng20 = (reg.OPERACION_ORDEN == 20) ? reg.CANTIDAD : cDato.DecIng20;

                                        cDatR.DecIng01 += (reg.REPORTE_ORDEN == 1) ? reg.CANTIDAD : 0;
                                        cDatR.DecIng02 += (reg.REPORTE_ORDEN == 2) ? reg.CANTIDAD : 0;
                                        cDatR.DecIng03 += (reg.REPORTE_ORDEN == 3) ? reg.CANTIDAD : 0;
                                        cDatR.DecIng04 += (reg.REPORTE_ORDEN == 4) ? reg.CANTIDAD : 0;
                                        cDatR.DecIng05 += (reg.REPORTE_ORDEN == 5) ? reg.CANTIDAD : 0;

                                        cDatR.StrTitIng01 = (reg.REPORTE_ORDEN == 1) ? reg.REPORTE_NOMBRE : cDatR.StrTitIng01;
                                        cDatR.StrTitIng02 = (reg.REPORTE_ORDEN == 2) ? reg.REPORTE_NOMBRE : cDatR.StrTitIng02;
                                        cDatR.StrTitIng03 = (reg.REPORTE_ORDEN == 3) ? reg.REPORTE_NOMBRE : cDatR.StrTitIng03;
                                        cDatR.StrTitIng04 = (reg.REPORTE_ORDEN == 4) ? reg.REPORTE_NOMBRE : cDatR.StrTitIng04;
                                        cDatR.StrTitIng05 = (reg.REPORTE_ORDEN == 5) ? reg.REPORTE_NOMBRE : cDatR.StrTitIng05;
                                    }
                                }
                            }

                            if (lst.LstEgresos == null || !lst.LstEgresos.Any())
                            {
                                cDato.DecSal01 = -99999;
                            }
                            else
                            {
                                foreach (var reg in lst.LstEgresos)
                                {
                                    if ((int) reg.OPERACION_ORDEN > 0)
                                    {
                                        if (!pvgAcumulado.Fields[27 + (int)reg.OPERACION_ORDEN].Visible)
                                        {
                                            pvgAcumulado.Fields[27 + (int)reg.OPERACION_ORDEN].Caption = reg.OPERACION_NOMBRE;
                                            pvgAcumulado.Fields[27 + (int)reg.OPERACION_ORDEN].Visible = true;
                                        }
                                        cDato.DecSal01 = (reg.OPERACION_ORDEN == 1) ? reg.CANTIDAD : cDato.DecSal01;
                                        cDato.DecSal02 = (reg.OPERACION_ORDEN == 2) ? reg.CANTIDAD : cDato.DecSal02;
                                        cDato.DecSal03 = (reg.OPERACION_ORDEN == 3) ? reg.CANTIDAD : cDato.DecSal03;
                                        cDato.DecSal04 = (reg.OPERACION_ORDEN == 4) ? reg.CANTIDAD : cDato.DecSal04;
                                        cDato.DecSal05 = (reg.OPERACION_ORDEN == 5) ? reg.CANTIDAD : cDato.DecSal05;
                                        cDato.DecSal06 = (reg.OPERACION_ORDEN == 6) ? reg.CANTIDAD : cDato.DecSal06;
                                        cDato.DecSal07 = (reg.OPERACION_ORDEN == 7) ? reg.CANTIDAD : cDato.DecSal07;
                                        cDato.DecSal08 = (reg.OPERACION_ORDEN == 8) ? reg.CANTIDAD : cDato.DecSal08;
                                        cDato.DecSal09 = (reg.OPERACION_ORDEN == 9) ? reg.CANTIDAD : cDato.DecSal09;
                                        cDato.DecSal10 = (reg.OPERACION_ORDEN == 10) ? reg.CANTIDAD : cDato.DecSal10;
                                        cDato.DecSal11 = (reg.OPERACION_ORDEN == 11) ? reg.CANTIDAD : cDato.DecSal11;
                                        cDato.DecSal12 = (reg.OPERACION_ORDEN == 12) ? reg.CANTIDAD : cDato.DecSal12;
                                        cDato.DecSal13 = (reg.OPERACION_ORDEN == 13) ? reg.CANTIDAD : cDato.DecSal13;
                                        cDato.DecSal14 = (reg.OPERACION_ORDEN == 14) ? reg.CANTIDAD : cDato.DecSal14;
                                        cDato.DecSal15 = (reg.OPERACION_ORDEN == 15) ? reg.CANTIDAD : cDato.DecSal15;
                                        cDato.DecSal16 = (reg.OPERACION_ORDEN == 16) ? reg.CANTIDAD : cDato.DecSal16;
                                        cDato.DecSal17 = (reg.OPERACION_ORDEN == 17) ? reg.CANTIDAD : cDato.DecSal17;
                                        cDato.DecSal18 = (reg.OPERACION_ORDEN == 18) ? reg.CANTIDAD : cDato.DecSal18;
                                        cDato.DecSal19 = (reg.OPERACION_ORDEN == 19) ? reg.CANTIDAD : cDato.DecSal19;
                                        cDato.DecSal20 = (reg.OPERACION_ORDEN == 20) ? reg.CANTIDAD : cDato.DecSal20;

                                        cDatR.DecSal01 += (reg.REPORTE_ORDEN == 1) ? reg.CANTIDAD : 0;
                                        cDatR.DecSal02 += (reg.REPORTE_ORDEN == 2) ? reg.CANTIDAD : 0;
                                        cDatR.DecSal03 += (reg.REPORTE_ORDEN == 3) ? reg.CANTIDAD : 0;
                                        cDatR.DecSal04 += (reg.REPORTE_ORDEN == 4) ? reg.CANTIDAD : 0;
                                        cDatR.DecSal05 += (reg.REPORTE_ORDEN == 5) ? reg.CANTIDAD : 0;

                                        cDatR.StrTitSal01 = (reg.REPORTE_ORDEN == 1) ? reg.REPORTE_NOMBRE : cDatR.StrTitSal01;
                                        cDatR.StrTitSal02 = (reg.REPORTE_ORDEN == 2) ? reg.REPORTE_NOMBRE : cDatR.StrTitSal02;
                                        cDatR.StrTitSal03 = (reg.REPORTE_ORDEN == 3) ? reg.REPORTE_NOMBRE : cDatR.StrTitSal03;
                                        cDatR.StrTitSal04 = (reg.REPORTE_ORDEN == 4) ? reg.REPORTE_NOMBRE : cDatR.StrTitSal04;
                                        cDatR.StrTitSal05 = (reg.REPORTE_ORDEN == 5) ? reg.REPORTE_NOMBRE : cDatR.StrTitSal05;
                                    }
                                }
                            }
                            lstMovimientos.Add(cDato);
                            lstAcumRep.Add(cDatR);
                            cont++;
                            intContProd++;
                        }
                        int intDia = DateTime.DaysInMonth(Convert.ToInt32(cmbGestion.Value.ToString()), Convert.ToInt32(cmbMes.Value.ToString()));
                        string strFechaI = "01/" + cmbMes.Value.ToString().PadLeft(2,'0') + "/" + cmbGestion.Value.ToString().PadLeft(2,'0');
                        string strFechaF = intDia.ToString().PadLeft(2, '0') + "/" + cmbMes.Value.ToString().PadLeft(2, '0') + "/" + cmbGestion.Value.ToString().PadLeft(2, '0');

                        lstMovimientos.ForEach(a => a.DecSaldoIni = (a.StrFecha == strFechaI) ? a.DecSaldoInicial : 0);
                        lstMovimientos.ForEach(a => a.DecSaldoFin = (a.StrFecha == strFechaF) ? a.DecSaldoFinal : 0);
                        lstAcumRep.ForEach(a => a.DecSaldoIni = (a.StrFecha == strFechaI) ? a.DecSaldoInicial : 0);
                        lstAcumRep.ForEach(a => a.DecSaldoFin = (a.StrFecha == strFechaF) ? a.DecSaldoFinal : 0);

                        Session[CVariablesSesion.GrillaAcumulado] = lstMovimientos;
                        pvgAcumulado.DataSource = lstMovimientos;
                        pvgAcumulado.DataBind();

                        var objParametricas = Session[CVariablesSesion.OctanoParametrica] as List<O_LISTA_OCTANO_PAR_CTY>;
                        var paramAgrup = objParametricas.Where(x => x.TIPO_VALOR == 10).ToList().FirstOrDefault();

                        if (paramAgrup.VALOR_FIN == "1")
                        {
                            var lstNuevo = lstAcumRep.GroupBy(n => new { n.StrFecha, n.IntIdProducto, n.StrProducto, n.StrUnidadMedida }).
                                       Select(group =>
                                       new
                                       {
                                           group.Key.StrFecha,
                                           IntGrupoProducto = 0,
                                           group.Key.IntIdProducto,
                                           group.Key.StrProducto,
                                           group.Key.StrUnidadMedida,
                                           IntIdInfraestructura = 0,
                                           StrInfraestructura = "",
                                           DecSaldoIni = group.Sum(c => c.DecSaldoIni),
                                           DecSaldoInicial = group.Sum(c => c.DecSaldoInicial),
                                           DecIng01 = group.Sum(c => c.DecIng01),
                                           DecIng02 = group.Sum(c => c.DecIng02),
                                           DecIng03 = group.Sum(c => c.DecIng03),
                                           DecIng04 = group.Sum(c => c.DecIng04),
                                           DecIng05 = group.Sum(c => c.DecIng05),
                                           DecTotalIng = group.Sum(c => c.DecTotalIng),
                                           DecSal01 = group.Sum(c => c.DecSal01),
                                           DecSal02 = group.Sum(c => c.DecSal02),
                                           DecSal03 = group.Sum(c => c.DecSal03),
                                           DecSal04 = group.Sum(c => c.DecSal04),
                                           DecSal05 = group.Sum(c => c.DecSal05),
                                           DecTotalSal = group.Sum(c => c.DecTotalSal),
                                           DecSaldoFinal = group.Sum(c => c.DecSaldoFinal),
                                           DecSaldoFin = group.Sum(c => c.DecSaldoFin),
                                           StrTitIng01 = group.Max(c => c.StrTitIng01),
                                           StrTitIng02 = group.Max(c => c.StrTitIng02),
                                           StrTitIng03 = group.Max(c => c.StrTitIng03),
                                           StrTitIng04 = group.Max(c => c.StrTitIng04),
                                           StrTitIng05 = group.Max(c => c.StrTitIng05),
                                           StrTitSal01 = group.Max(c => c.StrTitSal01),
                                           StrTitSal02 = group.Max(c => c.StrTitSal02),
                                           StrTitSal03 = group.Max(c => c.StrTitSal03),
                                           StrTitSal04 = group.Max(c => c.StrTitSal04),
                                           StrTitSal05 = group.Max(c => c.StrTitSal05)
                                       }).ToList();
                            Session[CVariablesSesion.GrillaAcumRep] = lstNuevo;
                        }
                        else
                        {
                            Session[CVariablesSesion.GrillaAcumRep] = lstAcumRep;
                        }

                        //Session[CVariablesSesion.GrillaAcumRep] = lstAcumRep;

                        var pvaArea = PivotArea.DataArea;
                        for (int i = 6; i < 50; i++)
                        {
                            if (pvgAcumulado.Fields[i].Visible)
                            {
                                pvgAcumulado.Fields[i].SetAreaPosition(pvaArea, i);
                            }
                        }

                        #endregion
                    }

                    #endregion
                }
                catch (Exception exp)
                {
                    #region RegistraError
                    MImprimirMensaje(exp.Message, EnumTiposMensajeAlerta.Error);
                    var oMensajeUsuario = new CLogTraza.MensajeUsuario
                    {
                        decUsuarioId = Session[CVariablesSesion.IdUsuario],
                        decIdModulo = CParametrosHydro.decIdAplicacion,
                        strAccion = GetType().Name + "." + MethodBase.GetCurrentMethod().Name,
                        strIp = HttpContext.Current.Request.UserHostAddress,
                        decNivelCapa = (int)CLogTraza.CapasNivel.Presentacion
                    };
                    CLogTraza.Error(oMensajeUsuario, exp);
                    #endregion
                }
            }
            else
            {
                //todo No implementado.
            }
        }

        protected void pvgAcumulado_CustomCellStyle(object sender, DevExpress.Web.ASPxPivotGrid.PivotCustomCellStyleEventArgs e)
        {
            try
            {
                #region Proceso a Ejecutar

                if (e.DataField == null) return;

                var x = e.RowValueType;

                if (e.IsFieldValueExpanded(fieldStrProducto) && e.IsFieldValueExpanded(fieldStrUnidadMedida))
                {
                        
                    if (x.ToString().Contains("Value"))
                    {
                        if (e.DataField.FieldName == "DecSaldoInicial" || e.DataField.FieldName == "DecSaldoFinal")
                        {
                            e.CellStyle.BackColor = Color.FromArgb(189, 189, 189);
                        }
                        if (e.DataField.FieldName == "DecTotalIng" || e.DataField.FieldName == "DecTotalSal")
                        {
                            e.CellStyle.BackColor = Color.FromArgb(201, 201, 201);
                        }
                    }
                    else
                    {
                        e.CellStyle.BackColor = Color.FromArgb(189, 189, 189);
                        if (e.DataField.FieldName == "DecSaldoInicial" || e.DataField.FieldName == "DecSaldoFinal")
                        {
                            e.CellStyle.ForeColor = Color.FromArgb(189, 189, 189);
                        }
                    }
                }
                else
                {
                    e.CellStyle.BackColor = Color.FromArgb(189, 189, 189);
                    if (e.DataField.FieldName == "DecSaldoInicial" || e.DataField.FieldName == "DecSaldoFinal")
                    {
                        e.CellStyle.ForeColor = Color.FromArgb(189, 189, 189);
                    }
                        
                }

                #endregion
            }
            catch (Exception exp)
            {
                #region RegistraError

                MImprimirMensaje(exp.Message, EnumTiposMensajeAlerta.Error);
                var oMensajeUsuario = new CLogTraza.MensajeUsuario
                {
                    decUsuarioId = Session[CVariablesSesion.IdUsuario],
                    decIdModulo = CParametrosHydro.decIdAplicacion,
                    strAccion = GetType().Name + "." + MethodBase.GetCurrentMethod().Name,
                    strIp = HttpContext.Current.Request.UserHostAddress,
                    decNivelCapa = (int)CLogTraza.CapasNivel.Presentacion
                };
                CLogTraza.Error(oMensajeUsuario, exp);

                #endregion
            }
        }
        
        protected void dgvInternacional_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    Session.Remove(CVariablesSesion.GrillaInternacional);
                    dgvInternacional.DataSource = null;
                    dgvInternacional.DataBind();

                    int index = dgvCabecera.FocusedRowIndex;
                    string strJsonReportes = dgvCabecera.GetRowValues(index, "JSON_REPORTE_DATOS").ToString();
                    var objDdjj = JsonSerializer.DeserializeFromString<O_DECLARACION_JURADA_CTY>(strJsonReportes);

                    var lstAdic2 = objDdjj.LstaAdicional2;
                    if (lstAdic2 != null && lstAdic2.Any())
                    {
                        #region facturas internacionales
                        lstAdic2 = lstAdic2.OrderBy(x => x.idMovimiento).ThenBy(x => x.idTipoEmisor).ToList();
                        CDjInternacional cDato;
                        long lngIdMovimiento = 0;
                        var lstRep = new List<CDjInternacional>();
                        cDato = null;
                        long lngCont = 1;

                        foreach (var registro in lstAdic2)
                        {
                            if (lngIdMovimiento != registro.idMovimiento)
                            {
                                if (cDato != null)
                                {
                                    lstRep.Add(cDato);
                                }
                                cDato = new CDjInternacional();
                                cDato.LngId = lngCont;
                                cDato.StrFecha = registro.fechaEmision.ToString().Substring(6, 2) + "/" + registro.fechaEmision.ToString().Substring(4, 2) + "/" + registro.fechaEmision.ToString().Substring(0, 4);
                                cDato.StrPlaca = registro.identificador;
                                cDato.StrPais = registro.ubicacionOperacion;
                                cDato.LngNit = (long)registro.nitConsumidor;
                                cDato.StrRazonSocial = registro.razonSocial;
                                cDato.LngNoAutProp = 0;
                                cDato.LngNoAutExt = 0;
                                cDato.LngNoFactProp = 0;
                                cDato.LngNoFactExt = 0;
                                cDato.StrCodCtrlProp = "";
                                cDato.StrCodCtrlExt = "";
                                cDato.DecImporteProp = 0;
                                cDato.DecImporteExt = 0;
                                cDato.DecPrecioProp = 0;
                                cDato.DecPrecioExt = 0;
                                cDato.DecCantidadProp = 0;
                                cDato.DecCantidadExt = 0;
                                cDato.DecIdEstado = registro.idEstadoTransaccion;
                                cDato.StrEstado = registro.EstadoTransaccion;
                                cDato.DecIdProducto = registro.idHidrocarburo;
                                cDato.StrProducto = registro.hidrocarburo;
                                cDato.StrCodProducto = registro.codHidrocarburo;
                                cDato.StrUnidadMedida = registro.unidadMedida;

                                /*if (registro.tipoMovimiento==null)
                                {
                                    cDato.StrTipoMovimiento="NACIONAL A INTERNACIONAL(EXPORTACION)";
                                }
                                else
                                {
                                    cDato.StrTipoMovimiento = registro.tipoMovimiento;
                                }
                                */
                                cDato.StrTipoMovimiento = registro.tipoMovimiento == null ? "NACIONAL A INTERNACIONAL(EXPORTACION)" : registro.tipoMovimiento;
                                lngIdMovimiento = (long)registro.idMovimiento;
                            }

                            switch ((int)registro.idTipoEmisor)
                            {
                                case (int)EnumTipoEmisor.PropiaEntidad:
                                    cDato.LngNoAutProp = (long)registro.numeroAutorizacion;
                                    cDato.LngNoFactProp = Convert.ToInt64(registro.numeroTransaccion);
                                    cDato.StrCodCtrlProp = registro.codigoControl;
                                    cDato.DecPrecioProp = registro.precioUnitario;
                                    cDato.DecPrecioExt += registro.precioUnitario;
                                    if (!registro.EstadoTransaccion.Equals("A"))
                                    {
                                        cDato.DecImporteProp = registro.precioTotal;
                                        cDato.DecCantidadProp = registro.cantidadHidrocarburo;
                                    }
                                    break;
                                case (int)EnumTipoEmisor.ExternaYpfb:
                                    cDato.LngNoAutExt = (long)registro.numeroAutorizacion;
                                    cDato.LngNoFactExt = Convert.ToInt64(registro.numeroTransaccion);
                                    cDato.StrCodCtrlExt = registro.codigoControl;
                                    cDato.DecPrecioExt += registro.precioUnitario;
                                    if (!registro.EstadoTransaccion.Equals("A"))
                                    {
                                        cDato.DecImporteExt = registro.precioTotal;
                                        cDato.DecCantidadExt = registro.cantidadHidrocarburo;
                                    }
                                    break;
                                default:
                                    //todo No implementado.
                                    break;
                            }
                            lngCont++;
                        }

                        if (cDato != null)
                        {
                            lstRep.Add(cDato);
                        }
                        else
                        {
                            //todo No implementado.
                        }

                        lstRep = lstRep.OrderBy(x => x.StrFecha).ToList();

                        Session[CVariablesSesion.GrillaInternacional] = lstRep;
                        dgvInternacional.DataSource = lstRep;
                        dgvInternacional.DataBind();

                        #endregion
                    }

                    #endregion
                }
                catch (Exception exp)
                {
                    #region RegistraError
                    MImprimirMensaje(exp.Message, EnumTiposMensajeAlerta.Error);
                    var oMensajeUsuario = new CLogTraza.MensajeUsuario
                    {
                        decUsuarioId = Session[CVariablesSesion.IdUsuario],
                        decIdModulo = CParametrosHydro.decIdAplicacion,
                        strAccion = GetType().Name + "." + MethodBase.GetCurrentMethod().Name,
                        strIp = HttpContext.Current.Request.UserHostAddress,
                        decNivelCapa = (int)CLogTraza.CapasNivel.Presentacion
                    };
                    CLogTraza.Error(oMensajeUsuario, exp);
                    #endregion
                }
            }
            else
            {
                //todo No implementado.
            }
        }

        protected void dgvInternacional_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            try
            {
                #region Proceso a Ejecutar

                if (e.VisibleIndex >= 0)
                {
                    if (dgvInternacional.GetRowValues(e.VisibleIndex, "StrEstado").ToString() == "A")
                        e.Row.BackColor = Color.FromArgb(249, 131, 131);
                    else
                    {
                        if (e.VisibleIndex % 2 == 1)
                        {
                            e.Row.BackColor = Color.FromArgb(253, 237, 212);
                        }
                        else
                        {
                            //todo No implementado.
                        }
                    }
                }
                else
                {
                    //todo No implementado.
                }

                #endregion
            }
            catch (Exception exp)
            {
                #region RegistraError

                MImprimirMensaje(exp.Message, EnumTiposMensajeAlerta.Error);
                var oMensajeUsuario = new CLogTraza.MensajeUsuario
                {
                    decUsuarioId = Session[CVariablesSesion.IdUsuario],
                    decIdModulo = CParametrosHydro.decIdAplicacion,
                    strAccion = GetType().Name + "." + MethodBase.GetCurrentMethod().Name,
                    strIp = HttpContext.Current.Request.UserHostAddress,
                    decNivelCapa = (int)CLogTraza.CapasNivel.Presentacion
                };
                CLogTraza.Error(oMensajeUsuario, exp);

                #endregion
            }
        }

        protected void dgvRecaudo_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    Session.Remove(CVariablesSesion.GrillaRecaudo);
                    dgvRecaudo.DataSource = null;
                    dgvRecaudo.DataBind();

                    int index = dgvCabecera.FocusedRowIndex;
                    string strJsonReportes = dgvCabecera.GetRowValues(index, "JSON_REPORTE_DATOS").ToString();
                    var objDdjj = JsonSerializer.DeserializeFromString<O_DECLARACION_JURADA_CTY>(strJsonReportes);

                    var lstRecaudo = objDdjj.LstaRecaudoExt;
                    Session[CVariablesSesion.GrillaRecaudo] = lstRecaudo;
                    dgvRecaudo.DataSource = lstRecaudo;
                    dgvRecaudo.DataBind();

                    #endregion
                }
                catch (Exception exp)
                {
                    #region RegistraError
                    MImprimirMensaje(exp.Message, EnumTiposMensajeAlerta.Error);
                    var oMensajeUsuario = new CLogTraza.MensajeUsuario
                    {
                        decUsuarioId = Session[CVariablesSesion.IdUsuario],
                        decIdModulo = CParametrosHydro.decIdAplicacion,
                        strAccion = GetType().Name + "." + MethodBase.GetCurrentMethod().Name,
                        strIp = HttpContext.Current.Request.UserHostAddress,
                        decNivelCapa = (int)CLogTraza.CapasNivel.Presentacion
                    };
                    CLogTraza.Error(oMensajeUsuario, exp);
                    #endregion
                }
            }
            else
            {
                //todo No implementado.
            }
        }

        protected void dgvRecaudo_HtmlRowCreated(object sender, ASPxGridViewTableRowEventArgs e)
        {
            try
            {
                #region Proceso a Ejecutar

                if (e.VisibleIndex < 0) return;
                if (dgvInternacional.GetRowValues(e.VisibleIndex, "StrEstado").ToString() == "A")
                    e.Row.BackColor = Color.FromArgb(249, 131, 131);
                else
                {
                    if (e.VisibleIndex % 2 == 1)
                    {
                        e.Row.BackColor = Color.FromArgb(253, 237, 212);
                    }
                }

                #endregion
            }
            catch (Exception exp)
            {
                #region RegistraError

                MImprimirMensaje(exp.Message, EnumTiposMensajeAlerta.Error);
                var oMensajeUsuario = new CLogTraza.MensajeUsuario
                {
                    decUsuarioId = Session[CVariablesSesion.IdUsuario],
                    decIdModulo = CParametrosHydro.decIdAplicacion,
                    strAccion = GetType().Name + "." + MethodBase.GetCurrentMethod().Name,
                    strIp = HttpContext.Current.Request.UserHostAddress,
                    decNivelCapa = (int)CLogTraza.CapasNivel.Presentacion
                };
                CLogTraza.Error(oMensajeUsuario, exp);

                #endregion
            }
        }

        protected void dgvContenedor_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    Session.Remove(CVariablesSesion.GrillaInternacional);
                    dgvContenedor.DataSource = null;
                    dgvContenedor.DataBind();

                    int index = dgvCabecera.FocusedRowIndex;
                    string strJsonReportes = dgvCabecera.GetRowValues(index, "JSON_REPORTE_DATOS").ToString();
                    var objDdjj = JsonSerializer.DeserializeFromString<O_DECLARACION_JURADA_CTY>(strJsonReportes);

                    var lstAdic1 = objDdjj.LstaAdicional1;
                    if (lstAdic1 != null && lstAdic1.Any())
                    {
                        #region facturas internacionales
                        lstAdic1 = lstAdic1.OrderBy(x => x.idMovimiento).ThenBy(x => x.idTipoTransaccion).ToList();
                        CDjContenedor cDato;
                        long lngIdMovimiento = 0;
                        var lstRep = new List<CDjContenedor>();
                        cDato = null;
                        long lngCont = 1;

                        foreach (var registro in lstAdic1)
                        {
                            if (lngIdMovimiento != registro.idMovimiento)
                            {
                                if (cDato != null)
                                {
                                    lstRep.Add(cDato);
                                }
                                cDato = new CDjContenedor();
                                cDato.LngId = lngCont;
                                cDato.StrFecha = registro.fechaEmision.ToString().Substring(6, 2) + "/" + registro.fechaEmision.ToString().Substring(4, 2) + "/" + registro.fechaEmision.ToString().Substring(0, 4);
                                cDato.StrPais = registro.ubicacionOperacion;
                                cDato.StrIdentificador = registro.identificador;
                                cDato.StrSubIdentificador = registro.subIdentificador;
                                cDato.StrTipoContenedor = registro.tipoEnvase;
                                cDato.DecIdProducto = registro.idHidrocarburo;
                                cDato.StrProducto = registro.hidrocarburo;
                                cDato.StrCodProducto = registro.codHidrocarburo;
                                cDato.StrUnidadMedida = registro.unidadMedida;
                                lngIdMovimiento = (long)registro.idMovimiento;
                            }

                            switch ((int)registro.idTipoTransaccion)
                            {
                                case 2:
                                case 3:
                                    cDato.StrEstado = registro.EstadoTransaccion.Substring(0, 1);
                                    cDato.DecIdEstado = registro.idEstadoTransaccion;

                                    cDato.LngNit = (long)registro.nitConsumidor;
                                    cDato.StrRazonSocial = registro.razonSocial;

                                    cDato.LngNoAut = (long)registro.numeroAutorizacion;
                                    cDato.StrNoTransaccion = registro.numeroTransaccion;
                                    cDato.StrCodCtrl = registro.codigoControl;

                                    cDato.DecPrecio = registro.precioUnitario;
                                    if (registro.idEstadoTransaccion == 11)
                                    {
                                        cDato.DecCantidad = registro.cantidadHidrocarburo;
                                        cDato.DecImporte = registro.precioTotal;
                                    }
                                    else
                                    {
                                        cDato.DecCantidad = 0;
                                        cDato.DecImporte = 0;
                                    }

                                    break;
                                case 7:
                                    cDato.AutSustCtrl = registro.numeroTransaccion;
                                    break;
                                case 21:
                                    cDato.AutAnh = registro.numeroTransaccion;
                                    break;
                            }
                            lngCont++;
                        }

                        if (cDato != null)
                        {
                            lstRep.Add(cDato);
                        }
                        else
                        {
                            //todo No implementado.
                        }

                        lstRep = lstRep.OrderBy(x => x.StrFecha).ToList();

                        Session[CVariablesSesion.GrillaAuxiliar] = lstRep;
                        dgvContenedor.DataSource = lstRep;
                        dgvContenedor.DataBind();

                        #endregion
                    }

                    #endregion
                }
                catch (Exception exp)
                {
                    #region RegistraError
                    MImprimirMensaje(exp.Message, EnumTiposMensajeAlerta.Error);
                    var oMensajeUsuario = new CLogTraza.MensajeUsuario
                    {
                        decUsuarioId = Session[CVariablesSesion.IdUsuario],
                        decIdModulo = CParametrosHydro.decIdAplicacion,
                        strAccion = GetType().Name + "." + MethodBase.GetCurrentMethod().Name,
                        strIp = HttpContext.Current.Request.UserHostAddress,
                        decNivelCapa = (int)CLogTraza.CapasNivel.Presentacion
                    };
                    CLogTraza.Error(oMensajeUsuario, exp);
                    #endregion
                }
            }
            else
            {
                //todo No implementado.
            }
        }

        protected void dgvContenedor_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            try
            {
                #region Proceso a Ejecutar

                if (e.VisibleIndex >= 0)
                {
                    if (dgvContenedor.GetRowValues(e.VisibleIndex, "DecIdEstado").ToString() == "12")
                        e.Row.BackColor = Color.FromArgb(249, 131, 131);
                    else
                    {
                        if (e.VisibleIndex % 2 == 1)
                        {
                            e.Row.BackColor = Color.FromArgb(253, 237, 212);
                        }
                        else
                        {
                            //todo No implementado.
                        }
                    }
                }
                else
                {
                    //todo No implementado.
                }

                #endregion
            }
            catch (Exception exp)
            {
                #region RegistraError

                MImprimirMensaje(exp.Message, EnumTiposMensajeAlerta.Error);
                var oMensajeUsuario = new CLogTraza.MensajeUsuario
                {
                    decUsuarioId = Session[CVariablesSesion.IdUsuario],
                    decIdModulo = CParametrosHydro.decIdAplicacion,
                    strAccion = GetType().Name + "." + MethodBase.GetCurrentMethod().Name,
                    strIp = HttpContext.Current.Request.UserHostAddress,
                    decNivelCapa = (int)CLogTraza.CapasNivel.Presentacion
                };
                CLogTraza.Error(oMensajeUsuario, exp);

                #endregion
            }
        }

        protected void btnReimprimir_Click(object sender, EventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    lblRazonSocial.Text = tlsOperadores.FocusedNode.GetValue("StrDescPadre").ToString();
                    lblNit.Text = tlsOperadores.FocusedNode.GetValue("LngNit").ToString();
                    lblDireccion.Text = (tlsOperadores.FocusedNode.GetValue("StrDireccion") == null) ? "" : tlsOperadores.FocusedNode.GetValue("StrDireccion").ToString();
                    lblDepto.Text = tlsOperadores.FocusedNode.GetValue("StrDepartamento").ToString();

                    var objParametricas = Session[CVariablesSesion.OctanoParametrica] as List<O_LISTA_OCTANO_PAR_CTY>;
                    var strTextoCab = objParametricas.Where(x => x.TIPO_VALOR == 8).ToList().FirstOrDefault().VALOR_FIN;
                    var vecCabecera = strTextoCab.Split('|');
                    
                    var index = dgvCabecera.FocusedRowIndex;
                    if (index >= 0 && Session[CVariablesSesion.GrillaAcumulado] != null)
                    {
                        string strQr;                        
                        if (Convert.ToInt32(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()) == (int)EnumActividades.EstacionServicioGnv)
                        {
                            Session[CVariablesSesion.TipoReporte] = EnumTipoReporte.DeclaracionJuradaEgreso;
                            var xreAcumulado = new repDjAcumuladoEgreso();
                            xreAcumulado.xtcCabCodigo.Text = vecCabecera[0] + " ANEXO 1";
                            xreAcumulado.xtcCabVersion.Text = vecCabecera[1];
                            xreAcumulado.xtcCabAprobado.Text = vecCabecera[2];

                            xreAcumulado.xtcRazonSocial.Text = dgvCabecera.GetRowValues(index, "RAZON_SOCIAL").ToString();
                            xreAcumulado.xtcActividad.Text = tlsOperadores.FocusedNode.GetValue("StrActividad").ToString();
                            xreAcumulado.xtcDireccion.Text = dgvCabecera.GetRowValues(index, "DIRECCION").ToString();
                            xreAcumulado.xtcDepartamento.Text = tlsOperadores.FocusedNode.GetValue("StrDepartamento").ToString();
                            xreAcumulado.xtcUrbano.Text = dgvCabecera.GetRowValues(index, "AMBITO_OPERACION").ToString();
                            xreAcumulado.xtcTelefono.Text = dgvCabecera.GetRowValues(index, "TELEFONO").ToString();
                            xreAcumulado.xtcMunicipio.Text = dgvCabecera.GetRowValues(index, "MUNICIPIO").ToString();
                            xreAcumulado.xtcPublico.Text = (dgvCabecera.GetRowValues(index, "TIPO_SOCIEDAD") == null) ? "" : dgvCabecera.GetRowValues(index, "TIPO_SOCIEDAD").ToString();
                            xreAcumulado.xtcMes.Text = dgvCabecera.GetRowValues(index, "MES").ToString();
                            xreAcumulado.xtcLugar.Text = "LUGAR :   " + dgvCabecera.GetRowValues(index, "LUGAR_REGISTRO");
                            xreAcumulado.xrlDia.Text = dgvCabecera.GetRowValues(index, "APP_FECHA_REGISTRO").ToString().Substring(6, 2);
                            xreAcumulado.xrlMes.Text = dgvCabecera.GetRowValues(index, "APP_FECHA_REGISTRO").ToString().Substring(4, 2);
                            xreAcumulado.xrlGestion.Text = dgvCabecera.GetRowValues(index, "APP_FECHA_REGISTRO").ToString().Substring(0, 4);
                            xreAcumulado.xrlResponsable.Text = (dgvCabecera.GetRowValues(index, "NOMBRES_REPLEGAL") == null) ? "" : dgvCabecera.GetRowValues(index, "NOMBRES_REPLEGAL").ToString();
                            xreAcumulado.xtcCiResponsable.Text = "C.I.: " + dgvCabecera.GetRowValues(index, "CI_REPLEGAL");
                            xreAcumulado.xrlUsuarioEmisor.Text = dgvCabecera.GetRowValues(index, "NOMBRES_USUARIO").ToString();
                            xreAcumulado.xtcCiUsuarioEmisor.Text = "C.I.: " + dgvCabecera.GetRowValues(index, "CI_USUARIO");
                            xreAcumulado.xtcNoCite.Text = dgvCabecera.GetRowValues(index, "CITE_GENERADO").ToString();
                            xreAcumulado.xbcCodigoQr.Text = dgvCabecera.GetRowValues(index, "CITE_GENERADO") + "|" + cmbGestion.Value + cmbMes.Value.ToString().PadLeft(2, '0') + "|" + xreAcumulado.xrlGestion.Text + xreAcumulado.xrlMes.Text + xreAcumulado.xrlDia.Text;
                            strQr = xreAcumulado.xbcCodigoQr.Text;

                            var oListaOctanoParCty = objParametricas.Where(x => x.TIPO_VALOR == 7).ToList().FirstOrDefault();
                            if (oListaOctanoParCty != null && oListaOctanoParCty.VALOR_FIN == "2")
                            {
                                xreAcumulado.grhInfraestructura.Visible = true;
                                xreAcumulado.grfInfraestructura.Visible = true;
                            }
                            else
                            {
                                xreAcumulado.grhInfraestructura.Visible = false;
                                xreAcumulado.grfInfraestructura.Visible = false;
                            }

                            xreAcumulado.DataSource = Session[CVariablesSesion.GrillaAcumRep];
                            Session[CVariablesSesion.ReportePrincipal] = xreAcumulado;
                        }
                        else
                        {
                            Session[CVariablesSesion.TipoReporte] = EnumTipoReporte.DeclaracionJurada;
                            var xreAcumulado = new repDjAcumuladoInfra();
                            xreAcumulado.xtcCabCodigo.Text = vecCabecera[0] + " ANEXO 1";
                            xreAcumulado.xtcCabVersion.Text = vecCabecera[1];
                            xreAcumulado.xtcCabAprobado.Text = vecCabecera[2];

                            xreAcumulado.xtcRazonSocial.Text = dgvCabecera.GetRowValues(index, "RAZON_SOCIAL").ToString();
                            xreAcumulado.xtcActividad.Text = tlsOperadores.FocusedNode.GetValue("StrActividad").ToString();
                            xreAcumulado.xtcDireccion.Text = (dgvCabecera.GetRowValues(index, "DIRECCION") == null) ? "" : dgvCabecera.GetRowValues(index, "DIRECCION").ToString();
                            xreAcumulado.xtcDepartamento.Text = (tlsOperadores.FocusedNode.GetValue("StrDepartamento") == null) ? "" : tlsOperadores.FocusedNode.GetValue("StrDepartamento").ToString();
                            xreAcumulado.xtcUrbano.Text = (dgvCabecera.GetRowValues(index, "AMBITO_OPERACION") == null) ? "" : dgvCabecera.GetRowValues(index, "AMBITO_OPERACION").ToString();
                            xreAcumulado.xtcTelefono.Text = (dgvCabecera.GetRowValues(index, "TELEFONO") == null) ? "" : dgvCabecera.GetRowValues(index, "TELEFONO").ToString();
                            xreAcumulado.xtcMunicipio.Text = (dgvCabecera.GetRowValues(index, "MUNICIPIO") == null) ? "" : dgvCabecera.GetRowValues(index, "MUNICIPIO").ToString();
                            xreAcumulado.xtcPublico.Text = (dgvCabecera.GetRowValues(index, "TIPO_SOCIEDAD") == null) ? "" : dgvCabecera.GetRowValues(index, "TIPO_SOCIEDAD").ToString();
                            xreAcumulado.xtcMes.Text = dgvCabecera.GetRowValues(index, "MES").ToString();
                            xreAcumulado.xtcLugar.Text = "LUGAR :   " + dgvCabecera.GetRowValues(index, "LUGAR_REGISTRO");
                            xreAcumulado.xrlDia.Text = dgvCabecera.GetRowValues(index, "APP_FECHA_REGISTRO").ToString().Substring(6, 2);
                            xreAcumulado.xrlMes.Text = dgvCabecera.GetRowValues(index, "APP_FECHA_REGISTRO").ToString().Substring(4, 2);
                            xreAcumulado.xrlGestion.Text = dgvCabecera.GetRowValues(index, "APP_FECHA_REGISTRO").ToString().Substring(0, 4);
                            xreAcumulado.xrlResponsable.Text = (dgvCabecera.GetRowValues(index, "NOMBRES_REPLEGAL") == null) ? "" : dgvCabecera.GetRowValues(index, "NOMBRES_REPLEGAL").ToString();
                            xreAcumulado.xtcCiResponsable.Text = "C.I.: " + dgvCabecera.GetRowValues(index, "CI_REPLEGAL");
                            xreAcumulado.xrlUsuarioEmisor.Text = dgvCabecera.GetRowValues(index, "NOMBRES_USUARIO").ToString();
                            xreAcumulado.xtcCiUsuarioEmisor.Text = "C.I.: " + dgvCabecera.GetRowValues(index, "CI_USUARIO");
                            xreAcumulado.xtcNoCite.Text = dgvCabecera.GetRowValues(index, "CITE_GENERADO").ToString();
                            xreAcumulado.xbcCodigoQr.Text = dgvCabecera.GetRowValues(index, "CITE_GENERADO") + "|" + cmbGestion.Value + cmbMes.Value.ToString().PadLeft(2, '0') + "|" + xreAcumulado.xrlGestion.Text + xreAcumulado.xrlMes.Text + xreAcumulado.xrlDia.Text;
                            strQr = xreAcumulado.xbcCodigoQr.Text;

                            var paramAgrup = objParametricas.Where(x => x.TIPO_VALOR == 10).ToList().FirstOrDefault();
                            var oListaOctanoParCty = objParametricas.Where(x => x.TIPO_VALOR == 7).ToList().FirstOrDefault();
                            xreAcumulado.grhInfraestructura.Visible = false;
                            xreAcumulado.grfInfraestructura.Visible = false;

                            if (oListaOctanoParCty != null && oListaOctanoParCty.VALOR_FIN == "2")
                            {
                                if (paramAgrup != null && paramAgrup.VALOR_FIN != "1")
                                {
                                    xreAcumulado.grhInfraestructura.Visible = true;
                                    xreAcumulado.grfInfraestructura.Visible = true;
                                }
                            }

                            xreAcumulado.DataSource = Session[CVariablesSesion.GrillaAcumRep];
                            Session[CVariablesSesion.ReportePrincipal] = xreAcumulado;
                        }

                        //vtas internacional
                        if (Session[CVariablesSesion.GrillaInternacional] != null)
                        {
                            var xreInt = new repDjInternacional();
                            xreInt.xtcCabCodigo.Text = vecCabecera[0] + " ANEXO 2";
                            xreInt.xtcCabVersion.Text = vecCabecera[1];
                            xreInt.xtcCabAprobado.Text = vecCabecera[2];

                            xreInt.xtcRazonSocial.Text = dgvCabecera.GetRowValues(index, "RAZON_SOCIAL").ToString();
                            xreInt.xtcActividad.Text = tlsOperadores.FocusedNode.GetValue("StrActividad").ToString();
                            xreInt.xtcDireccion.Text = dgvCabecera.GetRowValues(index, "DIRECCION").ToString();
                            xreInt.xtcDepartamento.Text = tlsOperadores.FocusedNode.GetValue("StrDepartamento").ToString();
                            xreInt.xtcUrbano.Text = dgvCabecera.GetRowValues(index, "AMBITO_OPERACION").ToString();
                            xreInt.xtcTelefono.Text = dgvCabecera.GetRowValues(index, "TELEFONO").ToString();
                            xreInt.xtcMunicipio.Text = dgvCabecera.GetRowValues(index, "MUNICIPIO").ToString();
                            xreInt.xtcPublico.Text = (dgvCabecera.GetRowValues(index, "TIPO_SOCIEDAD") == null) ? "" : dgvCabecera.GetRowValues(index, "TIPO_SOCIEDAD").ToString();
                            xreInt.xtcMes.Text = dgvCabecera.GetRowValues(index, "MES").ToString();
                            xreInt.xtcLugar.Text = "LUGAR :   " + dgvCabecera.GetRowValues(index, "LUGAR_REGISTRO");
                            xreInt.xrlDia.Text = dgvCabecera.GetRowValues(index, "APP_FECHA_REGISTRO").ToString().Substring(6, 2);
                            xreInt.xrlMes.Text = dgvCabecera.GetRowValues(index, "APP_FECHA_REGISTRO").ToString().Substring(4, 2);
                            xreInt.xrlGestion.Text = dgvCabecera.GetRowValues(index, "APP_FECHA_REGISTRO").ToString().Substring(0, 4);
                            xreInt.xrlResponsable.Text = (dgvCabecera.GetRowValues(index, "NOMBRES_REPLEGAL") == null) ? "" : dgvCabecera.GetRowValues(index, "NOMBRES_REPLEGAL").ToString();
                            xreInt.xtcCiResponsable.Text = "C.I.: " + dgvCabecera.GetRowValues(index, "CI_REPLEGAL");
                            xreInt.xrlUsuarioEmisor.Text = dgvCabecera.GetRowValues(index, "NOMBRES_USUARIO").ToString();
                            xreInt.xtcCiUsuarioEmisor.Text = "C.I.: " + dgvCabecera.GetRowValues(index, "CI_USUARIO");
                            xreInt.xtcNoCite.Text = dgvCabecera.GetRowValues(index, "CITE_GENERADO").ToString();
                            xreInt.xbcCodigoQr.Text = strQr;
                            xreInt.DataSource = Session[CVariablesSesion.GrillaInternacional];
                            Session[CVariablesSesion.ReporteSecundario] = xreInt;
                        }

                        //Recaudo Externo
                        if (Session[CVariablesSesion.GrillaRecaudo] != null)
                        {
                            var xreRex = new repDjRecaudoExterno();
                            xreRex.xtcCabCodigo.Text = vecCabecera[0] + " ANEXO 3";
                            xreRex.xtcCabVersion.Text = vecCabecera[1];
                            xreRex.xtcCabAprobado.Text = vecCabecera[2];

                            xreRex.xtcRazonSocial.Text = dgvCabecera.GetRowValues(index, "RAZON_SOCIAL").ToString();
                            xreRex.xtcActividad.Text = tlsOperadores.FocusedNode.GetValue("StrActividad").ToString();
                            xreRex.xtcDireccion.Text = dgvCabecera.GetRowValues(index, "DIRECCION").ToString();
                            xreRex.xtcDepartamento.Text = tlsOperadores.FocusedNode.GetValue("StrDepartamento").ToString();
                            xreRex.xtcUrbano.Text = dgvCabecera.GetRowValues(index, "AMBITO_OPERACION").ToString();
                            xreRex.xtcTelefono.Text = dgvCabecera.GetRowValues(index, "TELEFONO").ToString();
                            xreRex.xtcMunicipio.Text = dgvCabecera.GetRowValues(index, "MUNICIPIO").ToString();
                            xreRex.xtcPublico.Text = (dgvCabecera.GetRowValues(index, "TIPO_SOCIEDAD") == null) ? "" : dgvCabecera.GetRowValues(index, "TIPO_SOCIEDAD").ToString();
                            xreRex.xtcMes.Text = dgvCabecera.GetRowValues(index, "MES").ToString();
                            xreRex.xtcLugar.Text = "LUGAR :   " + dgvCabecera.GetRowValues(index, "LUGAR_REGISTRO");
                            xreRex.xrlDia.Text = dgvCabecera.GetRowValues(index, "APP_FECHA_REGISTRO").ToString().Substring(6, 2);
                            xreRex.xrlMes.Text = dgvCabecera.GetRowValues(index, "APP_FECHA_REGISTRO").ToString().Substring(4, 2);
                            xreRex.xrlGestion.Text = dgvCabecera.GetRowValues(index, "APP_FECHA_REGISTRO").ToString().Substring(0, 4);
                            xreRex.xrlResponsable.Text = (dgvCabecera.GetRowValues(index, "NOMBRES_REPLEGAL") == null) ? "" : dgvCabecera.GetRowValues(index, "NOMBRES_REPLEGAL").ToString();
                            xreRex.xtcCiResponsable.Text = "C.I.: " + dgvCabecera.GetRowValues(index, "CI_REPLEGAL");
                            xreRex.xrlUsuarioEmisor.Text = dgvCabecera.GetRowValues(index, "NOMBRES_USUARIO").ToString();
                            xreRex.xtcCiUsuarioEmisor.Text = "C.I.: " + dgvCabecera.GetRowValues(index, "CI_USUARIO");
                            xreRex.xtcNoCite.Text = dgvCabecera.GetRowValues(index, "CITE_GENERADO").ToString();
                            xreRex.xbcCodigoQr.Text = strQr;
                            xreRex.DataSource = Session[CVariablesSesion.GrillaRecaudo];
                            Session[CVariablesSesion.ReporteCuarto] = xreRex;
                        }

                        //vtas en contenedores
                        if (Session[CVariablesSesion.GrillaAuxiliar] != null)
                        {
                            var xreCont = new repDjContenedor();
                            xreCont.xtcCabCodigo.Text = vecCabecera[0] + " ANEXO 2";
                            xreCont.xtcCabVersion.Text = vecCabecera[1];
                            xreCont.xtcCabAprobado.Text = vecCabecera[2];

                            xreCont.xtcRazonSocial.Text = dgvCabecera.GetRowValues(index, "RAZON_SOCIAL").ToString();
                            xreCont.xtcActividad.Text = tlsOperadores.FocusedNode.GetValue("StrActividad").ToString();
                            xreCont.xtcDireccion.Text = dgvCabecera.GetRowValues(index, "DIRECCION").ToString();
                            xreCont.xtcDepartamento.Text = tlsOperadores.FocusedNode.GetValue("StrDepartamento").ToString();
                            xreCont.xtcUrbano.Text = dgvCabecera.GetRowValues(index, "AMBITO_OPERACION").ToString();
                            xreCont.xtcTelefono.Text = dgvCabecera.GetRowValues(index, "TELEFONO").ToString();
                            xreCont.xtcMunicipio.Text = dgvCabecera.GetRowValues(index, "MUNICIPIO").ToString();
                            xreCont.xtcPublico.Text = (dgvCabecera.GetRowValues(index, "TIPO_SOCIEDAD") == null) ? "" : dgvCabecera.GetRowValues(index, "TIPO_SOCIEDAD").ToString();
                            xreCont.xtcMes.Text = dgvCabecera.GetRowValues(index, "MES").ToString();
                            xreCont.xtcLugar.Text = "LUGAR :   " + dgvCabecera.GetRowValues(index, "LUGAR_REGISTRO");
                            xreCont.xrlDia.Text = dgvCabecera.GetRowValues(index, "APP_FECHA_REGISTRO").ToString().Substring(6, 2);
                            xreCont.xrlMes.Text = dgvCabecera.GetRowValues(index, "APP_FECHA_REGISTRO").ToString().Substring(4, 2);
                            xreCont.xrlGestion.Text = dgvCabecera.GetRowValues(index, "APP_FECHA_REGISTRO").ToString().Substring(0, 4);
                            xreCont.xrlResponsable.Text = (dgvCabecera.GetRowValues(index, "NOMBRES_REPLEGAL") == null) ? "" : dgvCabecera.GetRowValues(index, "NOMBRES_REPLEGAL").ToString();
                            xreCont.xtcCiResponsable.Text = "C.I.: " + dgvCabecera.GetRowValues(index, "CI_REPLEGAL");
                            xreCont.xrlUsuarioEmisor.Text = dgvCabecera.GetRowValues(index, "NOMBRES_USUARIO").ToString();
                            xreCont.xtcCiUsuarioEmisor.Text = "C.I.: " + dgvCabecera.GetRowValues(index, "CI_USUARIO");
                            xreCont.xtcNoCite.Text = dgvCabecera.GetRowValues(index, "CITE_GENERADO").ToString();
                            xreCont.xbcCodigoQr.Text = strQr;
                            xreCont.DataSource = Session[CVariablesSesion.GrillaAuxiliar];
                            Session[CVariablesSesion.ReporteTercero] = xreCont;
                        }
                        

                        

                        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( 'WFReporte.aspx', '')", true);
                    }

                    #endregion
                }
                catch (Exception exp)
                {
                    #region RegistraError
                    MImprimirMensaje(exp.Message, EnumTiposMensajeAlerta.Error);
                    var oMensajeUsuario = new CLogTraza.MensajeUsuario
                    {
                        decUsuarioId = Session[CVariablesSesion.IdUsuario],
                        decIdModulo = CParametrosHydro.decIdAplicacion,
                        strAccion = GetType().Name + "." + MethodBase.GetCurrentMethod().Name,
                        strIp = HttpContext.Current.Request.UserHostAddress,
                        decNivelCapa = (int)CLogTraza.CapasNivel.Presentacion
                    };
                    CLogTraza.Error(oMensajeUsuario, exp);
                    #endregion
                }
            }
            else
            {
                //todo No implementado.
            }
        }

        protected void btnImprimirFormulario_Click(object sender, EventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    var index = dgvCabecera.FocusedRowIndex;
                    if (index >= 0 && Session[CVariablesSesion.GrillaAcumulado] != null)
                    {
                        var FechaI = dgvCabecera.GetRowValues(index, "FECHA_INICIO").ToString();

                        var objNuevoDatoA = new List<O_OBTENER_ANEXO_A>();
                        var objNuevoDatoB2 = new List<O_OBTENER_ANEXO_B2>();

                        var objVolImportA = new List<O_OBTENER_VOLUMEN_IMPORTADO>();
                        var objVolImportB = new List<O_OBTENER_VOLUMEN_IMPORTADO>();
                        var objVolImportC = new List<O_OBTENER_VOLUMEN_IMPORTADO>();
                        var objVolImportD = new List<O_OBTENER_VOLUMEN_IMPORTADO>();
                        var objVolImportE = new List<O_OBTENER_VOLUMEN_IMPORTADO>();
                        var objVolImportF = new List<O_OBTENER_VOLUMEN_IMPORTADO>();
                        var objVolImportG = new List<O_OBTENER_VOLUMEN_IMPORTADO>();
                        var objVolImportH = new List<O_OBTENER_VOLUMEN_IMPORTADO>();
                        var objVolImportI = new List<O_OBTENER_VOLUMEN_IMPORTADO>();

                        var strProveedor = "";
                        var strPlantaDespacho = "";
                        var strPlantaRecepcion = "";

                        var objCostos = new List<O_OBTENER_COSTOS_IMPORTADO>();
                        var objCostosSubvencion = new List<O_OBTENER_COSTOS_SUBVENCION>();
                        var objCostosSubvencionFinal = new List<O_OBTENER_COSTOS_SUBVENCION_FINAL>();

                        var lstDespachos = _consulta.ObtenerMovimiento("",
                        new EObjetoEnvio8ParametrosProto
                        {
                            decFiltro = 4, //Filtro 1 por producto //2 consulta por infraestructura
                            strParametro1 = tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString(), //Id proyecto/entidad
                            strParametro2 = FechaI,//fecha inicial
                            strParametro3 = FechaI.Substring(0, 8) + "235959", //fecha final
                            strParametro4 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(), //id Actividad
                            strParametro5 = "249", //tipo operacion
                            strParametro6 = "0", //idProducto 
                            strParametro7 = "0", // id_infraestructura
                            strParametro8 = "0" // tipo_infraestructura
                        });

                        if (lstDespachos.intCodigo == 1)
                        {
                            var lstResultado = (List<O_CONSULTA_MOVIM_TRAN_CTY>)lstDespachos.oResultado;
                            if (lstResultado.Any())
                            {

                                var registros = lstResultado;
                                Session[CVariablesSesion.GrillaTransaccion] = registros;

                                var objTransaccion = _client.Post<EResultadoTransaccion>(CAppSettings.svc_obtener_transaccion,
                                                new
                                                {
                                                    decFiltro = "3",
                                                    strParametro1 = tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString(),
                                                    strParametro2 = FechaI.Substring(0, 8),
                                                    strParametro3 = FechaI.Substring(0, 8),
                                                    strParametro4 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
                                                    strParametro5 = "0"
                                                });
                                if (objTransaccion != null && objTransaccion.intCodigo == 1)
                                {
                                    List<O_LISTA_TRANSACCION_CTY> lstResultado2 = objTransaccion.oResultado;

                                    Session[CVariablesSesion.GrillaAuxiliar] = lstResultado2;

                                    var lstDocumentoA = new List<O_LISTA_TRANSACCION_CTY>();
                                    if (Session[CVariablesSesion.GrillaAuxiliar] != null)
                                    {
                                        lstDocumentoA = (List<O_LISTA_TRANSACCION_CTY>)Session[CVariablesSesion.GrillaAuxiliar];

                                        //ANEXO A
                                        #region ANEXO A
                                        var objMovA = lstDocumentoA.Where(x => x.OBSERVACIONES.StartsWith("ACCP")).ToList();


                                        foreach (var reg in objMovA)
                                        {
                                            var objTrasaccion = new O_OBTENER_ANEXO_A();
                                            var objVol = new O_OBTENER_VOLUMEN_IMPORTADO();

                                            var parametros = reg.OBSERVACIONES;

                                            string[] lista = parametros.Split('|');

                                            objTrasaccion.NRO_DUI = lista[1].ToString();
                                            objTrasaccion.PROVEEDOR = reg.RAZON_SOCIAL;
                                            objTrasaccion.RON = reg.CODIGO;
                                            objTrasaccion.NRO_FACTURA = reg.NRO_TRANSACCION;
                                            objTrasaccion.VOLUMEN_FACTURADO = Convert.ToDecimal(lista[5].ToString());
                                            objTrasaccion.VALOR_FACTURADO = reg.PRECIO_TOTAL;

                                            objNuevoDatoA.Add(objTrasaccion);

                                            objVol.RON = reg.CODIGO;
                                            objVol.VOLUMEN = Convert.ToDecimal(lista[5].ToString());
                                            objVol.COSTO = reg.PRECIO_TOTAL;

                                            objVolImportA.Add(objVol);
                                        }

                                        Session[CVariablesSesion.GrillaAnexoA] = objNuevoDatoA;

                                        #endregion

                                        //ANEXO B
                                        #region ANEXO B

                                        var objMovB1 = lstDocumentoA.Where(x => x.OBSERVACIONES.StartsWith("B1CCTF")).ToList();

                                        var objVolB = new O_OBTENER_VOLUMEN_IMPORTADO();

                                        var objNuevoDatoB1 = new List<O_OBTENER_ANEXO_B1>();
                                        foreach (var reg in objMovB1)
                                        {
                                            var objTrasaccion = new O_OBTENER_ANEXO_B1();
                                            objVolB = new O_OBTENER_VOLUMEN_IMPORTADO();

                                            var parametros = reg.OBSERVACIONES;

                                            string[] lista = parametros.Split('|');

                                            var decVolDespachado = Convert.ToDecimal(lista[4].ToString());
                                            var decVolRecibido = Convert.ToDecimal(lista[5].ToString());

                                            var decVolDiferencia = decVolDespachado - decVolRecibido;

                                            var decMerma = decVolDiferencia <= 150 ? (decVolRecibido * Convert.ToDecimal("0.0025")) : 0;

                                            objTrasaccion.NRO_DUI = lista[1].ToString();
                                            objTrasaccion.MES = reg.RAZON_SOCIAL;
                                            objTrasaccion.NRO_FACTURA_TRANSPORTE = reg.NRO_TRANSACCION;
                                            objTrasaccion.VOLUMEN_DESPACHADO = Convert.ToDecimal(lista[4].ToString());
                                            objTrasaccion.VOLUMEN_RECIBIDO = Convert.ToDecimal(lista[5].ToString());
                                            objTrasaccion.DIFERENCIA = Convert.ToDecimal(lista[4].ToString()) - Convert.ToDecimal(lista[5].ToString());
                                            objTrasaccion.MERMA_PERMISIBLE = Math.Round(decMerma, 0);
                                            objTrasaccion.MERMA_EXCEDENTE = Math.Round(decVolDiferencia - decMerma, 0) > 0 ? Math.Round(decVolDiferencia - decMerma, 0) : 0;
                                            objTrasaccion.FLETE = Convert.ToDecimal(lista[7].ToString());
                                            objTrasaccion.COSTO_PRODUCTO = reg.PRECIO_UNITARIO;
                                            objTrasaccion.TRANSPORTE_TOTAL = decVolRecibido * Convert.ToDecimal(lista[7].ToString());
                                            objTrasaccion.DESCUENTO_MERMA = (Math.Round(decVolDiferencia - decMerma, 0) > 0 ? Math.Round(decVolDiferencia - decMerma, 0) : 0) * reg.PRECIO_UNITARIO;
                                            objTrasaccion.COSTO_TRANSPORTE = (decVolRecibido * Convert.ToDecimal(lista[7].ToString())) - ((Math.Round(decVolDiferencia - decMerma, 0) > 0 ? Math.Round(decVolDiferencia - decMerma, 0) : 0) * reg.PRECIO_UNITARIO);

                                            objNuevoDatoB1.Add(objTrasaccion);

                                            objVolB.RON = reg.CODIGO;
                                            objVolB.VOLUMEN = Convert.ToDecimal(lista[5].ToString());
                                            objVolB.COSTO = (decVolRecibido * Convert.ToDecimal(lista[7].ToString())) - ((Math.Round(decVolDiferencia - decMerma, 0) > 0 ? Math.Round(decVolDiferencia - decMerma, 0) : 0) * reg.PRECIO_UNITARIO);

                                            objVolImportB.Add(objVolB);

                                        }

                                        Session[CVariablesSesion.GrillaAnexoB1] = objNuevoDatoB1;

                                        var objMovB2 = lstDocumentoA.Where(x => x.OBSERVACIONES.StartsWith("B2CCTT")).ToList();

                                        objNuevoDatoB2 = new List<O_OBTENER_ANEXO_B2>();
                                        foreach (var reg in objMovB2)
                                        {
                                            var objTrasaccion = new O_OBTENER_ANEXO_B2();
                                            objVolB = new O_OBTENER_VOLUMEN_IMPORTADO();

                                            var parametros = reg.OBSERVACIONES;

                                            string[] lista = parametros.Split('|');

                                            string[] listaProveedor = lista[6].Split('-');

                                            var decVolDespachado = Convert.ToDecimal(lista[4].ToString());
                                            var decVolRecibido = Convert.ToDecimal(lista[5].ToString());

                                            var decVolDiferencia = decVolDespachado - decVolRecibido;

                                            var decMerma = decVolDiferencia <= 150 ? (decVolRecibido * Convert.ToDecimal("0.0025")) : 0;

                                            objTrasaccion.NRO_DUI = lista[1].ToString();
                                            if (listaProveedor.Count() == 2)
                                            {
                                                objTrasaccion.NRO_FACTURA_PROVEEDOR = listaProveedor[0].ToString();
                                            }
                                            else
                                            {
                                                objTrasaccion.NRO_FACTURA_PROVEEDOR = "";
                                            }

                                            objTrasaccion.EMPRESA_TRANSPORTE = reg.RAZON_SOCIAL;
                                            objTrasaccion.NRO_FACTURA_TRANSPORTE = reg.NRO_TRANSACCION;
                                            objTrasaccion.PLANTA_DESPACHO = lista[2].ToString();
                                            objTrasaccion.PLANTA_RECEPCION = lista[3].ToString();
                                            objTrasaccion.VOLUMEN_DESPACHADO = decVolDespachado;
                                            objTrasaccion.VOLUMEN_RECIBIDO = decVolRecibido;
                                            objTrasaccion.DIFERENCIA = decVolDiferencia * -1;
                                            objTrasaccion.MERMA_PERMISIBLE = Math.Round(decMerma, 0);
                                            objTrasaccion.MERMA_EXCEDENTE = Math.Round(decVolDiferencia - decMerma, 0) > 0 ? Math.Round(decVolDiferencia - decMerma, 0) : 0;
                                            objTrasaccion.FLETE = Convert.ToDecimal(lista[7].ToString());
                                            objTrasaccion.COSTO_PRODUCTO = reg.PRECIO_UNITARIO;
                                            objTrasaccion.TRANSPORTE_TOTAL = decVolRecibido * Convert.ToDecimal(lista[7].ToString());
                                            objTrasaccion.DESCUENTO_MERMA = (Math.Round(decVolDiferencia - decMerma, 0) > 0 ? Math.Round(decVolDiferencia - decMerma, 0) : 0) * reg.PRECIO_UNITARIO;
                                            objTrasaccion.COSTO_TRANSPORTE = (decVolRecibido * Convert.ToDecimal(lista[7].ToString())) - ((Math.Round(decVolDiferencia - decMerma, 0) > 0 ? Math.Round(decVolDiferencia - decMerma, 0) : 0) * reg.PRECIO_UNITARIO);

                                            objNuevoDatoB2.Add(objTrasaccion);

                                            objVolB.RON = reg.CODIGO;
                                            objVolB.VOLUMEN = 0;
                                            objVolB.COSTO = (decVolRecibido * Convert.ToDecimal(lista[7].ToString())) - ((Math.Round(decVolDiferencia - decMerma, 0) > 0 ? Math.Round(decVolDiferencia - decMerma, 0) : 0) * reg.PRECIO_UNITARIO);

                                            objVolImportB.Add(objVolB);

                                            //txtPlanta = lista[3].ToString();
                                        }

                                        Session[CVariablesSesion.GrillaAnexoB2] = objNuevoDatoB2;

                                        var objMovB3 = lstDocumentoA.Where(x => x.OBSERVACIONES.StartsWith("B3CCTF")).ToList();

                                        var objNuevoDatoB3 = new List<O_OBTENER_ANEXO_B3>();
                                        foreach (var reg in objMovB3)
                                        {
                                            var objTrasaccion = new O_OBTENER_ANEXO_B3();
                                            objVolB = new O_OBTENER_VOLUMEN_IMPORTADO();

                                            var parametros = reg.OBSERVACIONES;

                                            string[] lista = parametros.Split('|');

                                            string[] listaProveedor = lista[6].Split('-');

                                            objTrasaccion.NRO_DUI = lista[1].ToString();

                                            if (listaProveedor.Count() == 2)
                                            {
                                                objTrasaccion.PROVEEDOR = listaProveedor[1].ToString();
                                                objTrasaccion.NRO_FACTURA = listaProveedor[0].ToString();
                                            }
                                            else
                                            {
                                                objTrasaccion.PROVEEDOR = "";
                                                objTrasaccion.NRO_FACTURA = "";
                                            }
                                            objTrasaccion.NRO_FACTURA_PROVEEDOR = listaProveedor[0].ToString();
                                            objTrasaccion.PROVEEDOR = listaProveedor[1].ToString();
                                            objTrasaccion.EMPRESA = reg.RAZON_SOCIAL;
                                            objTrasaccion.NRO_FACTURA = reg.NRO_TRANSACCION;
                                            objTrasaccion.VOLUMEN_DESTINO1 = Convert.ToDecimal(lista[4].ToString());
                                            objTrasaccion.VOLUMEN_DESTINO2 = Convert.ToDecimal(lista[5].ToString());
                                            objTrasaccion.FLETE = Convert.ToDecimal(lista[7].ToString());
                                            objTrasaccion.COSTO_FACTURADO = (Convert.ToDecimal(lista[5].ToString())) * (Convert.ToDecimal(lista[7].ToString()));
                                            objTrasaccion.MONTO_NETO_IVA = ((Convert.ToDecimal(lista[5].ToString())) * (Convert.ToDecimal(lista[7].ToString()))) * Convert.ToDecimal("0.87");

                                            objNuevoDatoB3.Add(objTrasaccion);

                                            objVolB.RON = reg.CODIGO;
                                            objVolB.VOLUMEN = 0;
                                            objVolB.COSTO = ((Convert.ToDecimal(lista[5].ToString())) * (Convert.ToDecimal(lista[7].ToString()))) * Convert.ToDecimal("0.87");

                                            objVolImportB.Add(objVolB);
                                        }

                                        Session[CVariablesSesion.GrillaAnexoB3] = objNuevoDatoB3;

                                        #endregion

                                        //ANEXO C
                                        #region ANEXO C
                                        var objMovC = lstDocumentoA.Where(x => x.OBSERVACIONES.StartsWith("CCS")).ToList();



                                        var objNuevoDatoC = new List<O_OBTENER_ANEXO_C>();
                                        foreach (var reg in objMovC)
                                        {
                                            var objTrasaccion = new O_OBTENER_ANEXO_C();

                                            var objVolC = new O_OBTENER_VOLUMEN_IMPORTADO();

                                            var parametros = reg.OBSERVACIONES;

                                            string[] lista = parametros.Split('|');

                                            objTrasaccion.NRO_DUI = lista[1].ToString();
                                            objTrasaccion.EMPRESA_ASEGURADORA = reg.RAZON_SOCIAL;
                                            objTrasaccion.PERIODO_COBERTURA = "";
                                            objTrasaccion.NRO_FACTURA_ASEGURADORA = reg.NRO_TRANSACCION;
                                            objTrasaccion.MONTO_FACTURADO = reg.PRECIO_TOTAL;
                                            objTrasaccion.MONTO_NETO_IVA = reg.PRECIO_TOTAL * Convert.ToDecimal("0.87");

                                            objNuevoDatoC.Add(objTrasaccion);

                                            objVolC.RON = reg.CODIGO;
                                            objVolC.VOLUMEN = 0;
                                            objVolC.COSTO = reg.PRECIO_TOTAL * Convert.ToDecimal("0.87");

                                            objVolImportC.Add(objVolC);
                                        }

                                        Session[CVariablesSesion.GrillaAnexoC] = objNuevoDatoC;

                                        #endregion

                                        //ANEXO D
                                        #region ANEXO D
                                        var objMovD1 = lstDocumentoA.Where(x => x.OBSERVACIONES.StartsWith("D1IO")).ToList();

                                        var objNuevoDatoD1 = new List<O_OBTENER_ANEXO_D>();
                                        foreach (var reg in objMovD1)
                                        {
                                            var objVolD = new O_OBTENER_VOLUMEN_IMPORTADO();

                                            var objTrasaccion = new O_OBTENER_ANEXO_D();

                                            var parametros = reg.OBSERVACIONES;

                                            string[] lista = parametros.Split('|');

                                            string[] listaProveedor = lista[6].Split('-');

                                            string fecha = reg.FECHA_HORA_EMISION.ToString();

                                            objTrasaccion.MES_CARGA = Convert.ToDateTime(fecha.Substring(0, 4) + "/" + fecha.Substring(4, 2) + "/" + fecha.Substring(6, 2)).ToString("MMMM - yyyy");
                                            objTrasaccion.NRO_DUI = lista[1].ToString();
                                            if (listaProveedor.Count() == 2)
                                            {
                                                objTrasaccion.PROVEEDOR = listaProveedor[1].ToString();
                                                objTrasaccion.NRO_FACTURA = listaProveedor[0].ToString();
                                            }
                                            else
                                            {
                                                objTrasaccion.PROVEEDOR = "";
                                                objTrasaccion.NRO_FACTURA = "";
                                            }
                                            objTrasaccion.VOLUMEN_FACTURADO = Convert.ToDecimal(lista[5].ToString());
                                            objTrasaccion.MONTO_INSPECCION = reg.PRECIO_TOTAL;

                                            objNuevoDatoD1.Add(objTrasaccion);

                                            objVolD.RON = reg.CODIGO;
                                            objVolD.VOLUMEN = 1;
                                            objVolD.COSTO = reg.PRECIO_TOTAL;

                                            objVolImportD.Add(objVolD);
                                        }

                                        Session[CVariablesSesion.GrillaAnexoD1] = objNuevoDatoD1;

                                        var objMovD2 = lstDocumentoA.Where(x => x.OBSERVACIONES.StartsWith("D2II")).ToList();

                                        var objNuevoDatoD2 = new List<O_OBTENER_ANEXO_D>();
                                        foreach (var reg in objMovD2)
                                        {
                                            var objVolD = new O_OBTENER_VOLUMEN_IMPORTADO();

                                            var objTrasaccion = new O_OBTENER_ANEXO_D();

                                            var parametros = reg.OBSERVACIONES;

                                            string[] lista = parametros.Split('|');

                                            string[] listaProveedor = lista[6].Split('-');

                                            string fecha = reg.FECHA_HORA_EMISION.ToString();

                                            objTrasaccion.MES_CARGA = Convert.ToDateTime(fecha.Substring(0, 4) + "/" + fecha.Substring(4, 2) + "/" + fecha.Substring(6, 2)).ToString("MMMM - yyyy");
                                            objTrasaccion.NRO_DUI = lista[1].ToString();
                                            if (listaProveedor.Count() == 2)
                                            {
                                                objTrasaccion.PROVEEDOR = listaProveedor[1].ToString();
                                                objTrasaccion.NRO_FACTURA = listaProveedor[0].ToString();
                                            }
                                            else
                                            {
                                                objTrasaccion.PROVEEDOR = "";
                                                objTrasaccion.NRO_FACTURA = "";
                                            }

                                            objTrasaccion.VOLUMEN_FACTURADO = Convert.ToDecimal(lista[5].ToString());
                                            objTrasaccion.MONTO_INSPECCION = reg.PRECIO_TOTAL;

                                            objNuevoDatoD2.Add(objTrasaccion);

                                            objVolD.RON = reg.CODIGO;
                                            objVolD.VOLUMEN = 0;
                                            objVolD.COSTO = reg.PRECIO_TOTAL;

                                            objVolImportD.Add(objVolD);
                                        }

                                        Session[CVariablesSesion.GrillaAnexoD2] = objNuevoDatoD2;

                                        var objMovD3 = lstDocumentoA.Where(x => x.OBSERVACIONES.StartsWith("D3IDF")).ToList();

                                        var objNuevoDatoD3 = new List<O_OBTENER_ANEXO_D>();
                                        foreach (var reg in objMovD3)
                                        {
                                            var objVolD = new O_OBTENER_VOLUMEN_IMPORTADO();

                                            var objTrasaccion = new O_OBTENER_ANEXO_D();

                                            var parametros = reg.OBSERVACIONES;

                                            string[] lista = parametros.Split('|');

                                            string[] listaProveedor = lista[6].Split('-');

                                            string fecha = reg.FECHA_HORA_EMISION.ToString();

                                            objTrasaccion.MES_CARGA = Convert.ToDateTime(fecha.Substring(0, 4) + "/" + fecha.Substring(4, 2) + "/" + fecha.Substring(6, 2)).ToString("MMMM - yyyy");
                                            objTrasaccion.NRO_DUI = lista[1].ToString();
                                            if (listaProveedor.Count() == 2)
                                            {
                                                objTrasaccion.PROVEEDOR = listaProveedor[1].ToString();
                                                objTrasaccion.NRO_FACTURA = listaProveedor[0].ToString();
                                            }
                                            else
                                            {
                                                objTrasaccion.PROVEEDOR = "";
                                                objTrasaccion.NRO_FACTURA = "";
                                            }
                                            objTrasaccion.VOLUMEN_FACTURADO = Convert.ToDecimal(lista[5].ToString());
                                            objTrasaccion.MONTO_INSPECCION = reg.PRECIO_TOTAL;

                                            objNuevoDatoD3.Add(objTrasaccion);

                                            objVolD.RON = reg.CODIGO;
                                            objVolD.VOLUMEN = 0;
                                            objVolD.COSTO = reg.PRECIO_TOTAL;

                                            objVolImportD.Add(objVolD);
                                        }

                                        Session[CVariablesSesion.GrillaAnexoD3] = objNuevoDatoD3;
                                        #endregion

                                        //ANEXO E
                                        #region ANEXO E
                                        var objMovE = lstDocumentoA.Where(x => x.OBSERVACIONES.StartsWith("ECC")).ToList();

                                        var objNuevoDatoE = new List<O_OBTENER_ANEXO_E>();
                                        foreach (var reg in objMovE)
                                        {
                                            var objTrasaccion = new O_OBTENER_ANEXO_E();
                                            var objVolE = new O_OBTENER_VOLUMEN_IMPORTADO();

                                            var parametros = reg.OBSERVACIONES;

                                            string[] lista = parametros.Split('|');

                                            objTrasaccion.NRO_DUI = lista[1].ToString();
                                            objTrasaccion.GRAVAMEN = Convert.ToDecimal(lista[8].ToString());
                                            objTrasaccion.SIDUINEA = Convert.ToDecimal(lista[9].ToString());
                                            objTrasaccion.COSTO_DEPOSITO = Convert.ToDecimal(lista[10].ToString());
                                            objTrasaccion.TOTAL_COSTO = Convert.ToDecimal(lista[8].ToString()) + Convert.ToDecimal(lista[9].ToString()) + Convert.ToDecimal(lista[10].ToString());

                                            objNuevoDatoE.Add(objTrasaccion);

                                            objVolE.RON = reg.CODIGO;
                                            objVolE.VOLUMEN = 0;
                                            objVolE.COSTO = Convert.ToDecimal(lista[8].ToString()) + Convert.ToDecimal(lista[9].ToString()) + Convert.ToDecimal(lista[10].ToString());

                                            objVolImportE.Add(objVolE);
                                        }

                                        Session[CVariablesSesion.GrillaAnexoE] = objNuevoDatoE;

                                        #endregion

                                        //ANEXO F



                                        //ANEXO G
                                        #region ANEXO G

                                        var objMovG1 = lstDocumentoA.Where(x => x.OBSERVACIONES.StartsWith("G1AI")).ToList();

                                        var objVolG = new O_OBTENER_VOLUMEN_IMPORTADO();

                                        var objNuevoDatoG1 = new List<O_OBTENER_ANEXO_G>();
                                        var objNuevoDatoI1 = new List<O_OBTENER_ANEXO_I>();

                                        foreach (var reg in objMovG1)
                                        {
                                            var objTrasaccion = new O_OBTENER_ANEXO_G();
                                            var objTrasaccionI = new O_OBTENER_ANEXO_I();

                                            objVolG = new O_OBTENER_VOLUMEN_IMPORTADO();

                                            var parametros = reg.OBSERVACIONES;

                                            string[] lista = parametros.Split('|');

                                            string fecha = reg.FECHA_HORA_EMISION.ToString();

                                            objTrasaccion.NRO_DUI = lista[1].ToString();
                                            objTrasaccion.MES = Convert.ToDateTime(fecha.Substring(0, 4) + "/" + fecha.Substring(4, 2) + "/" + fecha.Substring(6, 2)).ToString("MMMM - yyyy");
                                            objTrasaccion.EMPRESA_ALMACENAJE = reg.RAZON_SOCIAL;
                                            objTrasaccion.NRO_FACTURA_ALMACENAJE = reg.NRO_TRANSACCION;
                                            objTrasaccion.TARIFA_UNITARIA = reg.PRECIO_UNITARIO;
                                            objTrasaccion.VOLUMEN_RECIBIDO = Convert.ToDecimal(lista[5].ToString());
                                            objTrasaccion.COSTO_ALMACENAJE = Math.Round(objTrasaccion.TARIFA_UNITARIA * objTrasaccion.VOLUMEN_RECIBIDO, 2);
                                            objTrasaccion.MONTO_NETO_IVA = Math.Round(objTrasaccion.COSTO_ALMACENAJE * Convert.ToDecimal("0.87"), 2);

                                            objNuevoDatoG1.Add(objTrasaccion);

                                            string[] listaProveedor = lista[6].Split('-');

                                            objTrasaccionI.NRO_DUI = lista[1].ToString();
                                            if (listaProveedor.Count() == 2)
                                            {
                                                objTrasaccionI.PROVEEDOR = listaProveedor[1].ToString();
                                                objTrasaccionI.NRO_FACTURA_PROVEEDOR = listaProveedor[0].ToString();
                                            }
                                            else
                                            {
                                                objTrasaccionI.PROVEEDOR = "";
                                                objTrasaccionI.NRO_FACTURA_PROVEEDOR = "";
                                            }
                                            objTrasaccionI.MES_RECEPCION = Convert.ToDateTime(fecha.Substring(0, 4) + "/" + fecha.Substring(4, 2) + "/" + fecha.Substring(6, 2)).ToString("MMMM - yyyy");
                                            objTrasaccionI.PLANTA = "";// txtPlanta;
                                            objTrasaccionI.VOLUMEN = Convert.ToDecimal(lista[5].ToString());

                                            objNuevoDatoI1.Add(objTrasaccionI);

                                            objVolG.RON = reg.CODIGO;
                                            objVolG.VOLUMEN = Convert.ToDecimal(lista[5].ToString());
                                            objVolG.COSTO = Math.Round(objTrasaccion.COSTO_ALMACENAJE * Convert.ToDecimal("0.87"), 2);

                                            objVolImportG.Add(objVolG);
                                        }

                                        Session[CVariablesSesion.GrillaAnexoG1] = objNuevoDatoG1;

                                        Session[CVariablesSesion.GrillaAnexoI1] = objNuevoDatoI1;

                                        var objMovG2 = lstDocumentoA.Where(x => x.OBSERVACIONES.StartsWith("G2ADF")).ToList();

                                        var objNuevoDatoG2 = new List<O_OBTENER_ANEXO_G>();

                                        var objNuevoDatoI2 = new List<O_OBTENER_ANEXO_I>();

                                        foreach (var reg in objMovG2)
                                        {
                                            var objTrasaccion = new O_OBTENER_ANEXO_G();
                                            var objTrasaccionI = new O_OBTENER_ANEXO_I();

                                            var parametros = reg.OBSERVACIONES;

                                            string[] lista = parametros.Split('|');

                                            string fecha = reg.FECHA_HORA_EMISION.ToString();

                                            objTrasaccion.NRO_DUI = lista[1].ToString();
                                            objTrasaccion.MES = Convert.ToDateTime(fecha.Substring(0, 4) + "/" + fecha.Substring(4, 2) + "/" + fecha.Substring(6, 2)).ToString("MMMM - yyyy");
                                            objTrasaccion.EMPRESA_ALMACENAJE = reg.RAZON_SOCIAL;
                                            objTrasaccion.NRO_FACTURA_ALMACENAJE = reg.NRO_TRANSACCION;
                                            objTrasaccion.TARIFA_UNITARIA = reg.PRECIO_UNITARIO;
                                            objTrasaccion.VOLUMEN_RECIBIDO = Convert.ToDecimal(lista[5].ToString());
                                            objTrasaccion.COSTO_ALMACENAJE = Math.Round(objTrasaccion.TARIFA_UNITARIA * objTrasaccion.VOLUMEN_RECIBIDO, 2);
                                            objTrasaccion.MONTO_NETO_IVA = Math.Round(objTrasaccion.COSTO_ALMACENAJE * Convert.ToDecimal("0.87"), 2);

                                            objNuevoDatoG2.Add(objTrasaccion);

                                            string[] listaProveedor = lista[6].Split('-');

                                            objTrasaccionI.NRO_DUI = lista[1].ToString();
                                            if (listaProveedor.Count() == 2)
                                            {
                                                objTrasaccionI.PROVEEDOR = listaProveedor[1].ToString();
                                                objTrasaccionI.NRO_FACTURA_PROVEEDOR = listaProveedor[0].ToString();
                                            }
                                            else
                                            {
                                                objTrasaccionI.PROVEEDOR = "";
                                                objTrasaccionI.NRO_FACTURA_PROVEEDOR = "";
                                            }
                                            objTrasaccionI.MES_RECEPCION = Convert.ToDateTime(fecha.Substring(0, 4) + "/" + fecha.Substring(4, 2) + "/" + fecha.Substring(6, 2)).ToString("MMMM - yyyy");
                                            objTrasaccionI.PLANTA = "LA PAZ";
                                            objTrasaccionI.VOLUMEN = Convert.ToDecimal(lista[5].ToString());

                                            objNuevoDatoI2.Add(objTrasaccionI);


                                            objVolG.RON = reg.CODIGO;
                                            objVolG.VOLUMEN = Convert.ToDecimal(lista[5].ToString());
                                            objVolG.COSTO = Math.Round(objTrasaccion.COSTO_ALMACENAJE * Convert.ToDecimal("0.87"), 2);

                                            objVolImportG.Add(objVolG);
                                        }

                                        Session[CVariablesSesion.GrillaAnexoG2] = objNuevoDatoG2;

                                        Session[CVariablesSesion.GrillaAnexoI2] = objNuevoDatoI2;

                                        #endregion

                                        //ANEXO H

                                        #region ANEXO H

                                        var objMovH = lstDocumentoA.Where(x => x.OBSERVACIONES.StartsWith("HCM")).ToList();

                                        var objNuevoDatoH = new List<O_OBTENER_ANEXO_H>();
                                        foreach (var reg in objMovH)
                                        {
                                            var objTrasaccion = new O_OBTENER_ANEXO_H();
                                            var objVolH = new O_OBTENER_VOLUMEN_IMPORTADO();

                                            var parametros = reg.OBSERVACIONES;

                                            string[] lista = parametros.Split('|');

                                            string[] listaProveedor = lista[6].Split('-');

                                            string fecha = reg.FECHA_HORA_EMISION.ToString();

                                            objTrasaccion.MES = Convert.ToDateTime(fecha.Substring(0, 4) + "/" + fecha.Substring(4, 2) + "/" + fecha.Substring(6, 2)).ToString("MMMM - yyyy");
                                            objTrasaccion.NRO_DUI = lista[1].ToString();
                                            if (listaProveedor.Count() == 2)
                                            {
                                                objTrasaccion.NRO_FACTURA_PROVEEDOR = listaProveedor[0].ToString();
                                            }
                                            else
                                            {
                                                objTrasaccion.NRO_FACTURA_PROVEEDOR = "";
                                            }

                                            objTrasaccion.VOLUMEN_RECIBIDO = lista[5].ToString() == "" ? 0 : Convert.ToDecimal(lista[5].ToString());
                                            objTrasaccion.MONTO_MONITOREO = reg.PRECIO_TOTAL;

                                            objNuevoDatoH.Add(objTrasaccion);

                                            objVolH.RON = reg.CODIGO;
                                            objVolH.VOLUMEN = 0;
                                            objVolH.COSTO = reg.PRECIO_TOTAL;

                                            objVolImportH.Add(objVolH);
                                        }

                                        Session[CVariablesSesion.GrillaAnexoH] = objNuevoDatoH;

                                        #endregion

                                        //ANEXO I
                                    }

                                    //FORMULARIO 1
                                    var datos = registros[0].OBSERVACION_MOVIMIENTO.ToString();
                                    listaF1 = datos.Split('|');

                                    /*lblNombreEmpresa.Text = "a) Nombre de la Empresa: " + tlsOperadores.FocusedNode.GetValue("StrDescripcion").ToString();

                                    lblMontoCompensar.Text = "a) Monto total solicitado para compensar: " + listaF1[1].ToString();

                                    if (tlsOperadores.FocusedNode.GetValue("StrDescripcion").ToString() == "YACIMIENTOS PETROLIFEROS FISCALES BOLIVIANOS - YPFB")
                                    {
                                        chbYacimiento.Checked = true;
                                        chbLogistica.Checked = false;
                                    }
                                    else
                                    {
                                        chbYacimiento.Checked = false;
                                        chbLogistica.Checked = true;
                                    }*/

                                    //formulario 2

                                    //txtDuiRegularizadaF2.Text = listaF1[0].ToString();
                                    Session[CVariablesSesion.txtDuiRegularizadaF2] = listaF1[0].ToString();
                                    //txtFechaDuiF2.Text = registros[0].FECHA_OPERACION_DIA.ToString();
                                    Session[CVariablesSesion.txtFechaDuiF2] = registros[0].FECHA_OPERACION_DIA.ToString();

                                    if (listaF1.Count() == 12)
                                    {
                                        var txtFronteraIngresoF2 = listaF1[9].ToString();
                                        Session[CVariablesSesion.txtFronteraIngresoF2] = listaF1[9].ToString();
                                        var txtIncontermsF2 = listaF1[10].ToString();
                                        Session[CVariablesSesion.txtIncontermsF2] = listaF1[10].ToString();
                                    }

                                    var txtPrecioGasolinaBlancaF2 = listaF1[4].ToString();
                                    Session[CVariablesSesion.txtPrecioGasolinaBlancaF2] = listaF1[4].ToString();
                                    var txtPrecioNetoIvaF2 = listaF1[5].ToString();
                                    Session[CVariablesSesion.txtPrecioNetoIvaF2] = listaF1[5].ToString();
                                    var txtTarifaPoliductoF2 = listaF1[6].ToString();
                                    Session[CVariablesSesion.txtTarifaPoliductoF2] = listaF1[6].ToString();
                                    var txtTarifaNetoIvaF2 = listaF1[7].ToString();
                                    Session[CVariablesSesion.txtTarifaNetoIvaF2] = listaF1[7].ToString();

                                    var datoXgb = Convert.ToDecimal(listaF1[2].ToString()) > 1 ? Convert.ToDecimal(listaF1[2].ToString()) : (Convert.ToDecimal(listaF1[2].ToString()) * 100);
                                    var datoXimp = Convert.ToDecimal(listaF1[3].ToString()) > 1 ? Convert.ToDecimal(listaF1[3].ToString()) : (Convert.ToDecimal(listaF1[3].ToString()) * 100);
                                    var datoXgbC = datoXgb / 100;
                                    var datoXimpC = datoXimp / 100;
                                    var datoPpt = Convert.ToDecimal(listaF1[8].ToString());

                                    var txtCostoGasolinaBlancaNeto = (Convert.ToDecimal(listaF1[5].ToString()) + Convert.ToDecimal(listaF1[7].ToString())).ToString();

                                    var listaAf2 = (from datoA in objVolImportA
                                                    group datoA by datoA.RON into datoAGroup
                                                    select new
                                                    {
                                                        RON = datoAGroup.Key,
                                                        VOLUMEN = datoAGroup.Sum(x => x.VOLUMEN),
                                                        COSTO = datoAGroup.Sum(x => x.COSTO)
                                                    }).ToList();

                                    Session[CVariablesSesion.GrillaResumenAnexoA] = listaAf2;

                                    var ListPro = (from datoA in objNuevoDatoA
                                                   group datoA by datoA.PROVEEDOR into datoAGroupProveedor
                                                   select new
                                                   {
                                                       PROVEEDOR = datoAGroupProveedor.Key
                                                   }).ToList();

                                    strProveedor = ListPro[0].PROVEEDOR.ToString();

                                    var listaBf2 = (from datoA in objVolImportB
                                                    group datoA by datoA.RON into datoAGroup
                                                    select new
                                                    {
                                                        RON = datoAGroup.Key,
                                                        VOLUMEN = datoAGroup.Sum(x => x.VOLUMEN),
                                                        COSTO = datoAGroup.Sum(x => x.COSTO)
                                                    }).ToList();

                                    Session[CVariablesSesion.GrillaResumenAnexoB1] = listaBf2;

                                    var ListPlantaD = (from datoB in objNuevoDatoB2
                                                       group datoB by datoB.PLANTA_DESPACHO into datoBGroupPlanta
                                                       select new
                                                       {
                                                           Despacho = datoBGroupPlanta.Key
                                                       }).ToList();

                                    strPlantaDespacho = ListPlantaD[0].Despacho.ToString();

                                    var ListPlantaR = (from datoB in objNuevoDatoB2
                                                       group datoB by datoB.PLANTA_RECEPCION into datoBGroupPlanta
                                                       select new
                                                       {
                                                           Recepcion = datoBGroupPlanta.Key
                                                       }).ToList();

                                    strPlantaRecepcion = ListPlantaR[0].Recepcion.ToString();

                                    var listaCf2 = (from datoA in objVolImportC
                                                    group datoA by datoA.RON into datoAGroup
                                                    select new
                                                    {
                                                        RON = datoAGroup.Key,
                                                        VOLUMEN = datoAGroup.Sum(x => x.VOLUMEN),
                                                        COSTO = datoAGroup.Sum(x => x.COSTO)
                                                    }).ToList();

                                    Session[CVariablesSesion.GrillaResumenAnexoC] = listaCf2;

                                    var listaD1F2 = (from datoA in objVolImportD
                                                     where datoA.VOLUMEN == 1
                                                     group datoA by datoA.RON into datoAGroup
                                                     select new
                                                     {
                                                         RON = datoAGroup.Key,
                                                         VOLUMEN = datoAGroup.Sum(x => x.VOLUMEN),
                                                         COSTO = datoAGroup.Sum(x => x.COSTO)
                                                     }).ToList();

                                    Session[CVariablesSesion.GrillaResumenAnexoD1] = listaD1F2;

                                    var listaDf2 = (from datoA in objVolImportD
                                                    group datoA by datoA.RON into datoAGroup
                                                    select new
                                                    {
                                                        RON = datoAGroup.Key,
                                                        VOLUMEN = datoAGroup.Sum(x => x.VOLUMEN),
                                                        COSTO = datoAGroup.Sum(x => x.COSTO)
                                                    }).ToList();

                                    Session[CVariablesSesion.GrillaResumenAnexoD2] = listaDf2;

                                    var listaEf2 = (from datoA in objVolImportE
                                                    group datoA by datoA.RON into datoAGroup
                                                    select new
                                                    {
                                                        RON = datoAGroup.Key,
                                                        VOLUMEN = datoAGroup.Sum(x => x.VOLUMEN),
                                                        COSTO = datoAGroup.Sum(x => x.COSTO)
                                                    }).ToList();

                                    Session[CVariablesSesion.GrillaResumenAnexoE] = listaEf2;

                                    var listaGf2 = (from datoA in objVolImportG
                                                    group datoA by datoA.RON into datoAGroup
                                                    select new
                                                    {
                                                        RON = datoAGroup.Key,
                                                        VOLUMEN = datoAGroup.Sum(x => x.VOLUMEN),
                                                        COSTO = datoAGroup.Sum(x => x.COSTO)
                                                    }).ToList();

                                    Session[CVariablesSesion.GrillaResumenAnexoF1] = listaEf2;

                                    var listaHf2 = (from datoA in objVolImportH
                                                    group datoA by datoA.RON into datoAGroup
                                                    select new
                                                    {
                                                        RON = datoAGroup.Key,
                                                        VOLUMEN = datoAGroup.Sum(x => x.VOLUMEN),
                                                        COSTO = datoAGroup.Sum(x => x.COSTO)
                                                    }).ToList();

                                    Session[CVariablesSesion.GrillaResumenAnexoH] = listaHf2;

                                    var objNuevoDatoF1 = new List<O_OBTENER_ANEXO_F>();
                                    var objNuevoDatoF2 = new List<O_OBTENER_ANEXO_F>();
                                    var objNuevoDatoVolumen = new List<O_OBTENER_VOLUMEN_IMPORTADO>();

                                    for (int i = 0; i < listaAf2.Count(); i++)
                                    {

                                        var a = listaDf2.Count();

                                        var objTrasaccionF1 = new O_OBTENER_ANEXO_F();
                                        var objTrasaccionF2 = new O_OBTENER_ANEXO_F();

                                        if (i < a)
                                        {
                                            //ANEXO F
                                            objTrasaccionF1.NRO_DUI = listaF1[0].ToString();
                                            objTrasaccionF1.PROVEEDOR = registros[0].RAZON_SOCIAL;
                                            objTrasaccionF1.NRO_FACTURA_PROVEEDOR = "EN ANEXO A";
                                            objTrasaccionF1.MONTO_FACTURA_PROVEEDOR = listaAf2.Count() == 0 ? 0 : listaAf2[i].COSTO;
                                            objTrasaccionF1.COSTO_FINANCIERO = Math.Round(objTrasaccionF1.MONTO_FACTURA_PROVEEDOR * Convert.ToDecimal("0.001"), 2);
                                            objTrasaccionF1.COSTO_FINANCIERO_NETO = Math.Round(objTrasaccionF1.COSTO_FINANCIERO * Convert.ToDecimal("0.87"), 2);

                                            objNuevoDatoF1.Add(objTrasaccionF1);

                                            objTrasaccionF2.NRO_DUI = listaF1[0].ToString();
                                            objTrasaccionF2.PROVEEDOR = registros[0].RAZON_SOCIAL;
                                            objTrasaccionF2.NRO_FACTURA_PROVEEDOR = "EN ANEXO D1";
                                            objTrasaccionF2.MONTO_FACTURA_PROVEEDOR = listaD1F2.Count() == 0 ? 0 : listaD1F2[i].COSTO;
                                            objTrasaccionF2.COSTO_FINANCIERO = Math.Round(objTrasaccionF2.MONTO_FACTURA_PROVEEDOR * Convert.ToDecimal("0.001"), 2);
                                            objTrasaccionF2.COSTO_FINANCIERO_NETO = Math.Round(objTrasaccionF2.COSTO_FINANCIERO * Convert.ToDecimal("0.87"), 2);

                                            objNuevoDatoF2.Add(objTrasaccionF2);

                                        }

                                        var objCostoAnexo = new O_OBTENER_COSTOS_IMPORTADO();

                                        objCostoAnexo.RON = listaAf2[i].RON;
                                        objCostoAnexo.A = listaAf2.Count() == 0 ? 0 : Math.Round(listaAf2[i].COSTO, 2);
                                        objCostoAnexo.B = listaBf2.Count() == 0 ? 0 : Math.Round(listaBf2[i].COSTO, 2);
                                        objCostoAnexo.C = listaCf2.Count() == 0 ? 0 : Math.Round(listaCf2[i].COSTO, 2);
                                        objCostoAnexo.D = (listaDf2.Count() == 0 || listaDf2.Count() < listaAf2.Count()) ? 0 : Math.Round(listaDf2[i].COSTO, 2);
                                        objCostoAnexo.E = (listaEf2.Count() == 0 || listaEf2.Count() < listaAf2.Count()) ? 0 : Math.Round(listaEf2[i].COSTO, 2);
                                        objCostoAnexo.F = Math.Round(objTrasaccionF1.COSTO_FINANCIERO_NETO + objTrasaccionF2.COSTO_FINANCIERO_NETO, 2);
                                        objCostoAnexo.G = (listaGf2.Count() == 0 || listaGf2.Count() < listaAf2.Count()) ? 0 : Math.Round(listaGf2[i].COSTO, 2);
                                        objCostoAnexo.H = (listaHf2.Count() == 0 || listaHf2.Count() < listaAf2.Count()) ? 0 : Math.Round(listaHf2[i].COSTO, 2);
                                        objCostoAnexo.I = Math.Round(objCostoAnexo.A + objCostoAnexo.B + objCostoAnexo.C + objCostoAnexo.D + objCostoAnexo.E + objCostoAnexo.F + objCostoAnexo.G + objCostoAnexo.H, 2);

                                        objCostos.Add(objCostoAnexo);

                                        var objCostoSub = new O_OBTENER_COSTOS_SUBVENCION();

                                        objCostoSub.DATO_1 = "EN ANEXO A";
                                        objCostoSub.DATO_2 = listaAf2[i].RON;
                                        objCostoSub.DATO_3 = listaAf2[i].VOLUMEN;
                                        objCostoSub.DATO_4 = Math.Round((objCostoAnexo.I) / listaAf2[i].VOLUMEN, 2);
                                        objCostoSub.DATO_5 = datoXimp > 1 ? datoXimp : (datoXimp * 100);
                                        objCostoSub.DATO_6 = Convert.ToDecimal(txtCostoGasolinaBlancaNeto);
                                        objCostoSub.DATO_7 = datoXgb > 1 ? datoXgb : (datoXgb * 100);
                                        objCostoSub.DATO_8 = Math.Round((listaAf2[i].VOLUMEN) / (datoXimpC), 2);
                                        objCostoSub.DATO_9 = Math.Round((((objCostoAnexo.I) / listaAf2[i].VOLUMEN) * datoXimpC) + (datoXgbC * Convert.ToDecimal(txtCostoGasolinaBlancaNeto)), 2);

                                        objCostosSubvencion.Add(objCostoSub);

                                        var objVolumenImportado = new O_OBTENER_VOLUMEN_IMPORTADO();

                                        objVolumenImportado.RON = listaAf2[i].RON;
                                        objVolumenImportado.VOLUMEN = listaAf2[i].VOLUMEN;
                                        objVolumenImportado.COSTO = 0;

                                        objNuevoDatoVolumen.Add(objVolumenImportado);

                                        var objCostoSubFinal = new O_OBTENER_COSTOS_SUBVENCION_FINAL();

                                        objCostoSubFinal.DATO_1 = "YPFB";
                                        objCostoSubFinal.DATO_2 = "EN ANEXO A";
                                        objCostoSubFinal.DATO_3 = objCostoSub.DATO_8;
                                        objCostoSubFinal.DATO_4 = objCostoSub.DATO_9;
                                        objCostoSubFinal.DATO_5 = datoPpt;
                                        objCostoSubFinal.DATO_6 = Math.Round((objCostoSubFinal.DATO_3) * (objCostoSubFinal.DATO_4 - objCostoSubFinal.DATO_5), 2);

                                        objCostosSubvencionFinal.Add(objCostoSubFinal);
                                    }

                                    Session[CVariablesSesion.GrillaFormulario1Rubro4] = objNuevoDatoVolumen;

                                    Session[CVariablesSesion.GrillaFormulario1Rubro3] = objCostos;

                                    Session[CVariablesSesion.GrillaFormulario2Rubro4] = objCostosSubvencion;

                                    Session[CVariablesSesion.GrillaFormulario2Rubro5] = objCostosSubvencionFinal;

                                    Session[CVariablesSesion.GrillaAnexoF1] = objNuevoDatoF1;

                                    Session[CVariablesSesion.GrillaAnexoF2] = objNuevoDatoF2;

                                    Session[CVariablesSesion.TipoReporte] = EnumTipoReporte.repDjImportacion;
                                    var xreImportacion = new repDjImportadores();

                                    xreImportacion.xtcEmpresa.Text = tlsOperadores.FocusedNode.GetValue("StrDescripcion").ToString();
                                    //xreImportacion.xtcMontoCompensar.Text = listaF1[1].ToString();

                                    xreImportacion.xtcMontoCompensarLiteral.Text = "";

                                    xreImportacion.odsCostosImportados.DataSource = Session[CVariablesSesion.GrillaFormulario1Rubro3];
                                    xreImportacion.odsVolumenImportado.DataSource = Session[CVariablesSesion.GrillaFormulario1Rubro4];

                                    if (tlsOperadores.FocusedNode.GetValue("StrDescripcion").ToString() == "YACIMIENTOS PETROLIFEROS FISCALES BOLIVIANOS - YPFB")
                                    {
                                        xreImportacion.xtcYPFB.Text = "X";
                                        xreImportacion.xtcYPFBRefinacion.Text = "";
                                    }
                                    else
                                    {
                                        xreImportacion.xtcYPFB.Text = "";
                                        xreImportacion.xtcYPFBRefinacion.Text = "X";
                                    }

                                    var valor1 = (from datoA in objCostosSubvencionFinal
                                                  group datoA by datoA.DATO_6 into datoAGroup
                                                  select new
                                                  {
                                                      COSTO = datoAGroup.Sum(x => x.DATO_6)
                                                  }).ToList();

                                    var costoImportado = valor1[0].COSTO.ToString();

                                    string strLiteral = _cUtil.Convertir(costoImportado, true, "BOLIVIANOS");
                                    xreImportacion.xtcMontoCompensarLiteral.Text = strLiteral;

                                    xreImportacion.odsCostoInsumoAditivo.DataSource = Session[CVariablesSesion.GrillaFormulario1Rubro3];
                                    xreImportacion.odsCostoSubvencion.DataSource = Session[CVariablesSesion.GrillaFormulario2Rubro4];
                                    xreImportacion.odsCostoSubvencionFinal.DataSource = Session[CVariablesSesion.GrillaFormulario2Rubro5];

                                    xreImportacion.xtcDui.Text = Session[CVariablesSesion.txtDuiRegularizadaF2].ToString();
                                    xreImportacion.xtcFechaDui.Text = Session[CVariablesSesion.txtFechaDuiF2].ToString();
                                    xreImportacion.xtcFronteraIngreso.Text = Session[CVariablesSesion.txtFronteraIngresoF2].ToString();
                                    xreImportacion.xtcInconterms.Text = Session[CVariablesSesion.txtIncontermsF2].ToString();

                                    xreImportacion.xtcProveedor.Text = strProveedor;
                                    xreImportacion.xtcPlantaOrigen.Text = strPlantaDespacho;
                                    xreImportacion.xtcPlantaDestino.Text = strPlantaRecepcion;

                                    xreImportacion.xtcPrecioGasolinaBlanca.Text = Session[CVariablesSesion.txtPrecioGasolinaBlancaF2].ToString();
                                    xreImportacion.xtcPrecioNetoIva.Text = Session[CVariablesSesion.txtPrecioNetoIvaF2].ToString();
                                    xreImportacion.xtcTarifaTransportePoliducto.Text = Session[CVariablesSesion.txtTarifaPoliductoF2].ToString();
                                    xreImportacion.xtcTarifaNetoIva.Text = Session[CVariablesSesion.txtTarifaNetoIvaF2].ToString();

                                    xreImportacion.xtcCostoGasolinaBlanca.Text = Convert.ToString(Convert.ToDecimal(Session[CVariablesSesion.txtPrecioNetoIvaF2]) +
                                                                                  Convert.ToDecimal(Session[CVariablesSesion.txtTarifaNetoIvaF2]));

                                    Session[CVariablesSesion.ReportePrincipal] = xreImportacion;

                                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( 'WFReporte.aspx', '')", true);

                                    pnlDatos_Callback(null, null);

                                }
                                else
                                {
                                    //todo No implementado.
                                }

                            }
                            else
                            {
                                MImprimirMensaje(lstDespachos.strMensaje, (lstDespachos.strMensaje == "No se encontraron registros") ? EnumTiposMensajeAlerta.Alerta : EnumTiposMensajeAlerta.Error);
                            }
                        }


                    }

                    #endregion
                }
                catch (Exception exp)
                {
                    #region RegistraError
                    MImprimirMensaje(exp.Message, EnumTiposMensajeAlerta.Error);
                    var oMensajeUsuario = new CLogTraza.MensajeUsuario
                    {
                        decUsuarioId = Session[CVariablesSesion.IdUsuario],
                        decIdModulo = CParametrosHydro.decIdAplicacion,
                        strAccion = GetType().Name + "." + MethodBase.GetCurrentMethod().Name,
                        strIp = HttpContext.Current.Request.UserHostAddress,
                        decNivelCapa = (int)CLogTraza.CapasNivel.Presentacion
                    };
                    CLogTraza.Error(oMensajeUsuario, exp);
                    #endregion
                }
            }
            else
            {
                //todo No implementado.
            }
        }

        protected void btnImprimirCertificado_Click(object sender, EventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    var index = dgvCabecera.FocusedRowIndex;
                    if (index >= 0 && Session[CVariablesSesion.GrillaAcumulado] != null)
                    {
                        var FechaI = dgvCabecera.GetRowValues(index, "FECHA_INICIO").ToString();

                        var objNuevoDatoA = new List<O_OBTENER_ANEXO_A>();

                        var strProveedor="";

                        var objVolImportA = new List<O_OBTENER_VOLUMEN_IMPORTADO>();
                        var objVolImportB = new List<O_OBTENER_VOLUMEN_IMPORTADO>();
                        var objVolImportC = new List<O_OBTENER_VOLUMEN_IMPORTADO>();
                        var objVolImportD = new List<O_OBTENER_VOLUMEN_IMPORTADO>();
                        var objVolImportE = new List<O_OBTENER_VOLUMEN_IMPORTADO>();
                        var objVolImportF = new List<O_OBTENER_VOLUMEN_IMPORTADO>();
                        var objVolImportG = new List<O_OBTENER_VOLUMEN_IMPORTADO>();
                        var objVolImportH = new List<O_OBTENER_VOLUMEN_IMPORTADO>();
                        var objVolImportI = new List<O_OBTENER_VOLUMEN_IMPORTADO>();


                        var objCostos = new List<O_OBTENER_COSTOS_IMPORTADO>();
                        var objCostosSubvencion = new List<O_OBTENER_COSTOS_SUBVENCION>();
                        var objCostosSubvencionFinal = new List<O_OBTENER_COSTOS_SUBVENCION_FINAL>();

                        var lstDespachos = _consulta.ObtenerMovimiento("",
                        new EObjetoEnvio8ParametrosProto
                        {
                            decFiltro = 4, //Filtro 1 por producto //2 consulta por infraestructura
                            strParametro1 = tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString(), //Id proyecto/entidad
                            strParametro2 = FechaI,//fecha inicial
                            strParametro3 = FechaI.Substring(0, 8) + "235959", //fecha final
                            strParametro4 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(), //id Actividad
                            strParametro5 = "249", //tipo operacion
                            strParametro6 = "0", //idProducto 
                            strParametro7 = "0", // id_infraestructura
                            strParametro8 = "0" // tipo_infraestructura
                        });

                        if (lstDespachos.intCodigo == 1)
                        {
                            var lstResultado = (List<O_CONSULTA_MOVIM_TRAN_CTY>)lstDespachos.oResultado;
                            if (lstResultado.Any())
                            {
                                
                                var registros = lstResultado;
                                Session[CVariablesSesion.GrillaTransaccion] = registros;

                                var objTransaccion = _client.Post<EResultadoTransaccion>(CAppSettings.svc_obtener_transaccion,
                                                new
                                                {
                                                    decFiltro = "3",
                                                    strParametro1 = tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString(),
                                                    strParametro2 = FechaI.Substring(0, 8),
                                                    strParametro3 = FechaI.Substring(0, 8),
                                                    strParametro4 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
                                                    strParametro5 = "0"
                                                });
                                if (objTransaccion != null && objTransaccion.intCodigo == 1)
                                {
                                    List<O_LISTA_TRANSACCION_CTY> lstResultado2 = objTransaccion.oResultado;

                                    Session[CVariablesSesion.GrillaAuxiliar] = lstResultado2;

                                    var lstDocumentoA = new List<O_LISTA_TRANSACCION_CTY>();
                                    if (Session[CVariablesSesion.GrillaAuxiliar] != null)
                                    {
                                        lstDocumentoA = (List<O_LISTA_TRANSACCION_CTY>)Session[CVariablesSesion.GrillaAuxiliar];

                                        //ANEXO A
                                        #region ANEXO A
                                        var objMovA = lstDocumentoA.Where(x => x.OBSERVACIONES.StartsWith("ACCP")).ToList();


                                        foreach (var reg in objMovA)
                                        {
                                            var objTrasaccion = new O_OBTENER_ANEXO_A();
                                            var objVol = new O_OBTENER_VOLUMEN_IMPORTADO();

                                            var parametros = reg.OBSERVACIONES;

                                            string[] lista = parametros.Split('|');

                                            objTrasaccion.NRO_DUI = lista[1].ToString();
                                            objTrasaccion.PROVEEDOR = reg.RAZON_SOCIAL;
                                            objTrasaccion.RON = reg.CODIGO;
                                            objTrasaccion.NRO_FACTURA = reg.NRO_TRANSACCION;
                                            objTrasaccion.VOLUMEN_FACTURADO = Convert.ToDecimal(lista[5].ToString());
                                            objTrasaccion.VALOR_FACTURADO = reg.PRECIO_TOTAL;

                                            objNuevoDatoA.Add(objTrasaccion);

                                            objVol.RON = reg.CODIGO;
                                            objVol.VOLUMEN = Convert.ToDecimal(lista[5].ToString());
                                            objVol.COSTO = reg.PRECIO_TOTAL;

                                            objVolImportA.Add(objVol);
                                        }

                                        Session[CVariablesSesion.GrillaAnexoA] = objNuevoDatoA;

                                        

                                        #endregion

                                        //ANEXO B
                                        #region ANEXO B

                                        var objMovB1 = lstDocumentoA.Where(x => x.OBSERVACIONES.StartsWith("B1CCTF")).ToList();

                                        var objVolB = new O_OBTENER_VOLUMEN_IMPORTADO();

                                        var objNuevoDatoB1 = new List<O_OBTENER_ANEXO_B1>();
                                        foreach (var reg in objMovB1)
                                        {
                                            var objTrasaccion = new O_OBTENER_ANEXO_B1();
                                            objVolB = new O_OBTENER_VOLUMEN_IMPORTADO();

                                            var parametros = reg.OBSERVACIONES;

                                            string[] lista = parametros.Split('|');

                                            var decVolDespachado = Convert.ToDecimal(lista[4].ToString());
                                            var decVolRecibido = Convert.ToDecimal(lista[5].ToString());

                                            var decVolDiferencia = decVolDespachado - decVolRecibido;

                                            var decMerma = decVolDiferencia <= 150 ? (decVolRecibido * Convert.ToDecimal("0.0025")) : 0;

                                            objTrasaccion.NRO_DUI = lista[1].ToString();
                                            objTrasaccion.MES = reg.RAZON_SOCIAL;
                                            objTrasaccion.NRO_FACTURA_TRANSPORTE = reg.NRO_TRANSACCION;
                                            objTrasaccion.VOLUMEN_DESPACHADO = Convert.ToDecimal(lista[4].ToString());
                                            objTrasaccion.VOLUMEN_RECIBIDO = Convert.ToDecimal(lista[5].ToString());
                                            objTrasaccion.DIFERENCIA = Convert.ToDecimal(lista[4].ToString()) - Convert.ToDecimal(lista[5].ToString());
                                            objTrasaccion.MERMA_PERMISIBLE = Math.Round(decMerma, 0);
                                            objTrasaccion.MERMA_EXCEDENTE = Math.Round(decVolDiferencia - decMerma, 0) > 0 ? Math.Round(decVolDiferencia - decMerma, 0) : 0;
                                            objTrasaccion.FLETE = Convert.ToDecimal(lista[7].ToString());
                                            objTrasaccion.COSTO_PRODUCTO = reg.PRECIO_UNITARIO;
                                            objTrasaccion.TRANSPORTE_TOTAL = decVolRecibido * Convert.ToDecimal(lista[7].ToString());
                                            objTrasaccion.DESCUENTO_MERMA = (Math.Round(decVolDiferencia - decMerma, 0) > 0 ? Math.Round(decVolDiferencia - decMerma, 0) : 0) * reg.PRECIO_UNITARIO;
                                            objTrasaccion.COSTO_TRANSPORTE = (decVolRecibido * Convert.ToDecimal(lista[7].ToString())) - ((Math.Round(decVolDiferencia - decMerma, 0) > 0 ? Math.Round(decVolDiferencia - decMerma, 0) : 0) * reg.PRECIO_UNITARIO);

                                            objNuevoDatoB1.Add(objTrasaccion);

                                            objVolB.RON = reg.CODIGO;
                                            objVolB.VOLUMEN = Convert.ToDecimal(lista[5].ToString());
                                            objVolB.COSTO = (decVolRecibido * Convert.ToDecimal(lista[7].ToString())) - ((Math.Round(decVolDiferencia - decMerma, 0) > 0 ? Math.Round(decVolDiferencia - decMerma, 0) : 0) * reg.PRECIO_UNITARIO);

                                            objVolImportB.Add(objVolB);

                                        }

                                        Session[CVariablesSesion.GrillaAnexoB1] = objNuevoDatoB1;

                                        var objMovB2 = lstDocumentoA.Where(x => x.OBSERVACIONES.StartsWith("B2CCTT")).ToList();

                                        var objNuevoDatoB2 = new List<O_OBTENER_ANEXO_B2>();
                                        foreach (var reg in objMovB2)
                                        {
                                            var objTrasaccion = new O_OBTENER_ANEXO_B2();
                                            objVolB = new O_OBTENER_VOLUMEN_IMPORTADO();

                                            var parametros = reg.OBSERVACIONES;

                                            string[] lista = parametros.Split('|');

                                            string[] listaProveedor = lista[6].Split('-');

                                            var decVolDespachado = Convert.ToDecimal(lista[4].ToString());
                                            var decVolRecibido = Convert.ToDecimal(lista[5].ToString());

                                            var decVolDiferencia = decVolDespachado - decVolRecibido;

                                            var decMerma = decVolDiferencia <= 150 ? (decVolRecibido * Convert.ToDecimal("0.0025")) : 0;

                                            objTrasaccion.NRO_DUI = lista[1].ToString();
                                            if (listaProveedor.Count() == 2)
                                            {
                                                objTrasaccion.NRO_FACTURA_PROVEEDOR = listaProveedor[0].ToString();
                                            }
                                            else
                                            {
                                                objTrasaccion.NRO_FACTURA_PROVEEDOR = "";
                                            }

                                            objTrasaccion.EMPRESA_TRANSPORTE = reg.RAZON_SOCIAL;
                                            objTrasaccion.NRO_FACTURA_TRANSPORTE = reg.NRO_TRANSACCION;
                                            objTrasaccion.PLANTA_DESPACHO = lista[2].ToString();
                                            objTrasaccion.PLANTA_RECEPCION = lista[3].ToString();
                                            objTrasaccion.VOLUMEN_DESPACHADO = decVolDespachado;
                                            objTrasaccion.VOLUMEN_RECIBIDO = decVolRecibido;
                                            objTrasaccion.DIFERENCIA = decVolDiferencia * -1;
                                            objTrasaccion.MERMA_PERMISIBLE = Math.Round(decMerma, 0);
                                            objTrasaccion.MERMA_EXCEDENTE = Math.Round(decVolDiferencia - decMerma, 0) > 0 ? Math.Round(decVolDiferencia - decMerma, 0) : 0;
                                            objTrasaccion.FLETE = Convert.ToDecimal(lista[7].ToString());
                                            objTrasaccion.COSTO_PRODUCTO = reg.PRECIO_UNITARIO;
                                            objTrasaccion.TRANSPORTE_TOTAL = decVolRecibido * Convert.ToDecimal(lista[7].ToString());
                                            objTrasaccion.DESCUENTO_MERMA = (Math.Round(decVolDiferencia - decMerma, 0) > 0 ? Math.Round(decVolDiferencia - decMerma, 0) : 0) * reg.PRECIO_UNITARIO;
                                            objTrasaccion.COSTO_TRANSPORTE = (decVolRecibido * Convert.ToDecimal(lista[7].ToString())) - ((Math.Round(decVolDiferencia - decMerma, 0) > 0 ? Math.Round(decVolDiferencia - decMerma, 0) : 0) * reg.PRECIO_UNITARIO);

                                            objNuevoDatoB2.Add(objTrasaccion);

                                            objVolB.RON = reg.CODIGO;
                                            objVolB.VOLUMEN = 0;
                                            objVolB.COSTO = (decVolRecibido * Convert.ToDecimal(lista[7].ToString())) - ((Math.Round(decVolDiferencia - decMerma, 0) > 0 ? Math.Round(decVolDiferencia - decMerma, 0) : 0) * reg.PRECIO_UNITARIO);

                                            objVolImportB.Add(objVolB);

                                            //txtPlanta = lista[3].ToString();
                                        }

                                        Session[CVariablesSesion.GrillaAnexoB2] = objNuevoDatoB2;

                                        var objMovB3 = lstDocumentoA.Where(x => x.OBSERVACIONES.StartsWith("B3CCTF")).ToList();

                                        var objNuevoDatoB3 = new List<O_OBTENER_ANEXO_B3>();
                                        foreach (var reg in objMovB3)
                                        {
                                            var objTrasaccion = new O_OBTENER_ANEXO_B3();
                                            objVolB = new O_OBTENER_VOLUMEN_IMPORTADO();

                                            var parametros = reg.OBSERVACIONES;

                                            string[] lista = parametros.Split('|');

                                            string[] listaProveedor = lista[6].Split('-');

                                            objTrasaccion.NRO_DUI = lista[1].ToString();

                                            if (listaProveedor.Count() == 2)
                                            {
                                                objTrasaccion.PROVEEDOR = listaProveedor[1].ToString();
                                                objTrasaccion.NRO_FACTURA = listaProveedor[0].ToString();
                                            }
                                            else
                                            {
                                                objTrasaccion.PROVEEDOR = "";
                                                objTrasaccion.NRO_FACTURA = "";
                                            }
                                            objTrasaccion.NRO_FACTURA_PROVEEDOR = listaProveedor[0].ToString();
                                            objTrasaccion.PROVEEDOR = listaProveedor[1].ToString();
                                            objTrasaccion.EMPRESA = reg.RAZON_SOCIAL;
                                            objTrasaccion.NRO_FACTURA = reg.NRO_TRANSACCION;
                                            objTrasaccion.VOLUMEN_DESTINO1 = Convert.ToDecimal(lista[4].ToString());
                                            objTrasaccion.VOLUMEN_DESTINO2 = Convert.ToDecimal(lista[5].ToString());
                                            objTrasaccion.FLETE = Convert.ToDecimal(lista[7].ToString());
                                            objTrasaccion.COSTO_FACTURADO = (Convert.ToDecimal(lista[5].ToString())) * (Convert.ToDecimal(lista[7].ToString()));
                                            objTrasaccion.MONTO_NETO_IVA = ((Convert.ToDecimal(lista[5].ToString())) * (Convert.ToDecimal(lista[7].ToString()))) * Convert.ToDecimal("0.87");

                                            objNuevoDatoB3.Add(objTrasaccion);

                                            objVolB.RON = reg.CODIGO;
                                            objVolB.VOLUMEN = 0;
                                            objVolB.COSTO = ((Convert.ToDecimal(lista[5].ToString())) * (Convert.ToDecimal(lista[7].ToString()))) * Convert.ToDecimal("0.87");

                                            objVolImportB.Add(objVolB);
                                        }

                                        Session[CVariablesSesion.GrillaAnexoB3] = objNuevoDatoB3;

                                        #endregion

                                        //ANEXO C
                                        #region ANEXO C
                                        var objMovC = lstDocumentoA.Where(x => x.OBSERVACIONES.StartsWith("CCS")).ToList();



                                        var objNuevoDatoC = new List<O_OBTENER_ANEXO_C>();
                                        foreach (var reg in objMovC)
                                        {
                                            var objTrasaccion = new O_OBTENER_ANEXO_C();

                                            var objVolC = new O_OBTENER_VOLUMEN_IMPORTADO();

                                            var parametros = reg.OBSERVACIONES;

                                            string[] lista = parametros.Split('|');

                                            objTrasaccion.NRO_DUI = lista[1].ToString();
                                            objTrasaccion.EMPRESA_ASEGURADORA = reg.RAZON_SOCIAL;
                                            objTrasaccion.PERIODO_COBERTURA = "";
                                            objTrasaccion.NRO_FACTURA_ASEGURADORA = reg.NRO_TRANSACCION;
                                            objTrasaccion.MONTO_FACTURADO = reg.PRECIO_TOTAL;
                                            objTrasaccion.MONTO_NETO_IVA = reg.PRECIO_TOTAL * Convert.ToDecimal("0.87");

                                            objNuevoDatoC.Add(objTrasaccion);

                                            objVolC.RON = reg.CODIGO;
                                            objVolC.VOLUMEN = 0;
                                            objVolC.COSTO = reg.PRECIO_TOTAL * Convert.ToDecimal("0.87");

                                            objVolImportC.Add(objVolC);
                                        }

                                        Session[CVariablesSesion.GrillaAnexoC] = objNuevoDatoC;

                                        #endregion

                                        //ANEXO D
                                        #region ANEXO D
                                        var objMovD1 = lstDocumentoA.Where(x => x.OBSERVACIONES.StartsWith("D1IO")).ToList();

                                        var objNuevoDatoD1 = new List<O_OBTENER_ANEXO_D>();
                                        foreach (var reg in objMovD1)
                                        {
                                            var objVolD = new O_OBTENER_VOLUMEN_IMPORTADO();

                                            var objTrasaccion = new O_OBTENER_ANEXO_D();

                                            var parametros = reg.OBSERVACIONES;

                                            string[] lista = parametros.Split('|');

                                            string[] listaProveedor = lista[6].Split('-');

                                            string fecha = reg.FECHA_HORA_EMISION.ToString();

                                            objTrasaccion.MES_CARGA = Convert.ToDateTime(fecha.Substring(0, 4) + "/" + fecha.Substring(4, 2) + "/" + fecha.Substring(6, 2)).ToString("MMMM - yyyy");
                                            objTrasaccion.NRO_DUI = lista[1].ToString();
                                            if (listaProveedor.Count() == 2)
                                            {
                                                objTrasaccion.PROVEEDOR = listaProveedor[1].ToString();
                                                objTrasaccion.NRO_FACTURA = listaProveedor[0].ToString();
                                            }
                                            else
                                            {
                                                objTrasaccion.PROVEEDOR = "";
                                                objTrasaccion.NRO_FACTURA = "";
                                            }
                                            objTrasaccion.VOLUMEN_FACTURADO = Convert.ToDecimal(lista[5].ToString());
                                            objTrasaccion.MONTO_INSPECCION = reg.PRECIO_TOTAL;

                                            objNuevoDatoD1.Add(objTrasaccion);

                                            objVolD.RON = reg.CODIGO;
                                            objVolD.VOLUMEN = 1;
                                            objVolD.COSTO = reg.PRECIO_TOTAL;

                                            objVolImportD.Add(objVolD);
                                        }

                                        Session[CVariablesSesion.GrillaAnexoD1] = objNuevoDatoD1;

                                        var objMovD2 = lstDocumentoA.Where(x => x.OBSERVACIONES.StartsWith("D2II")).ToList();

                                        var objNuevoDatoD2 = new List<O_OBTENER_ANEXO_D>();
                                        foreach (var reg in objMovD2)
                                        {
                                            var objVolD = new O_OBTENER_VOLUMEN_IMPORTADO();

                                            var objTrasaccion = new O_OBTENER_ANEXO_D();

                                            var parametros = reg.OBSERVACIONES;

                                            string[] lista = parametros.Split('|');

                                            string[] listaProveedor = lista[6].Split('-');

                                            string fecha = reg.FECHA_HORA_EMISION.ToString();

                                            objTrasaccion.MES_CARGA = Convert.ToDateTime(fecha.Substring(0, 4) + "/" + fecha.Substring(4, 2) + "/" + fecha.Substring(6, 2)).ToString("MMMM - yyyy");
                                            objTrasaccion.NRO_DUI = lista[1].ToString();
                                            if (listaProveedor.Count() == 2)
                                            {
                                                objTrasaccion.PROVEEDOR = listaProveedor[1].ToString();
                                                objTrasaccion.NRO_FACTURA = listaProveedor[0].ToString();
                                            }
                                            else
                                            {
                                                objTrasaccion.PROVEEDOR = "";
                                                objTrasaccion.NRO_FACTURA = "";
                                            }

                                            objTrasaccion.VOLUMEN_FACTURADO = Convert.ToDecimal(lista[5].ToString());
                                            objTrasaccion.MONTO_INSPECCION = reg.PRECIO_TOTAL;

                                            objNuevoDatoD2.Add(objTrasaccion);

                                            objVolD.RON = reg.CODIGO;
                                            objVolD.VOLUMEN = 0;
                                            objVolD.COSTO = reg.PRECIO_TOTAL;

                                            objVolImportD.Add(objVolD);
                                        }

                                        Session[CVariablesSesion.GrillaAnexoD2] = objNuevoDatoD2;

                                        var objMovD3 = lstDocumentoA.Where(x => x.OBSERVACIONES.StartsWith("D3IDF")).ToList();

                                        var objNuevoDatoD3 = new List<O_OBTENER_ANEXO_D>();
                                        foreach (var reg in objMovD3)
                                        {
                                            var objVolD = new O_OBTENER_VOLUMEN_IMPORTADO();

                                            var objTrasaccion = new O_OBTENER_ANEXO_D();

                                            var parametros = reg.OBSERVACIONES;

                                            string[] lista = parametros.Split('|');

                                            string[] listaProveedor = lista[6].Split('-');

                                            string fecha = reg.FECHA_HORA_EMISION.ToString();

                                            objTrasaccion.MES_CARGA = Convert.ToDateTime(fecha.Substring(0, 4) + "/" + fecha.Substring(4, 2) + "/" + fecha.Substring(6, 2)).ToString("MMMM - yyyy");
                                            objTrasaccion.NRO_DUI = lista[1].ToString();
                                            if (listaProveedor.Count() == 2)
                                            {
                                                objTrasaccion.PROVEEDOR = listaProveedor[1].ToString();
                                                objTrasaccion.NRO_FACTURA = listaProveedor[0].ToString();
                                            }
                                            else
                                            {
                                                objTrasaccion.PROVEEDOR = "";
                                                objTrasaccion.NRO_FACTURA = "";
                                            }
                                            objTrasaccion.VOLUMEN_FACTURADO = Convert.ToDecimal(lista[5].ToString());
                                            objTrasaccion.MONTO_INSPECCION = reg.PRECIO_TOTAL;

                                            objNuevoDatoD3.Add(objTrasaccion);

                                            objVolD.RON = reg.CODIGO;
                                            objVolD.VOLUMEN = 0;
                                            objVolD.COSTO = reg.PRECIO_TOTAL;

                                            objVolImportD.Add(objVolD);
                                        }

                                        Session[CVariablesSesion.GrillaAnexoD3] = objNuevoDatoD3;
                                        #endregion

                                        //ANEXO E
                                        #region ANEXO E
                                        var objMovE = lstDocumentoA.Where(x => x.OBSERVACIONES.StartsWith("ECC")).ToList();

                                        var objNuevoDatoE = new List<O_OBTENER_ANEXO_E>();
                                        foreach (var reg in objMovE)
                                        {
                                            var objTrasaccion = new O_OBTENER_ANEXO_E();
                                            var objVolE = new O_OBTENER_VOLUMEN_IMPORTADO();

                                            var parametros = reg.OBSERVACIONES;

                                            string[] lista = parametros.Split('|');

                                            objTrasaccion.NRO_DUI = lista[1].ToString();
                                            objTrasaccion.GRAVAMEN = Convert.ToDecimal(lista[8].ToString());
                                            objTrasaccion.SIDUINEA = Convert.ToDecimal(lista[9].ToString());
                                            objTrasaccion.COSTO_DEPOSITO = Convert.ToDecimal(lista[10].ToString());
                                            objTrasaccion.TOTAL_COSTO = Convert.ToDecimal(lista[8].ToString()) + Convert.ToDecimal(lista[9].ToString()) + Convert.ToDecimal(lista[10].ToString());

                                            objNuevoDatoE.Add(objTrasaccion);

                                            objVolE.RON = reg.CODIGO;
                                            objVolE.VOLUMEN = 0;
                                            objVolE.COSTO = Convert.ToDecimal(lista[8].ToString()) + Convert.ToDecimal(lista[9].ToString()) + Convert.ToDecimal(lista[10].ToString());

                                            objVolImportE.Add(objVolE);
                                        }

                                        Session[CVariablesSesion.GrillaAnexoE] = objNuevoDatoE;

                                        #endregion

                                        //ANEXO F



                                        //ANEXO G
                                        #region ANEXO G

                                        var objMovG1 = lstDocumentoA.Where(x => x.OBSERVACIONES.StartsWith("G1AI")).ToList();

                                        var objVolG = new O_OBTENER_VOLUMEN_IMPORTADO();

                                        var objNuevoDatoG1 = new List<O_OBTENER_ANEXO_G>();
                                        var objNuevoDatoI1 = new List<O_OBTENER_ANEXO_I>();

                                        foreach (var reg in objMovG1)
                                        {
                                            var objTrasaccion = new O_OBTENER_ANEXO_G();
                                            var objTrasaccionI = new O_OBTENER_ANEXO_I();

                                            objVolG = new O_OBTENER_VOLUMEN_IMPORTADO();

                                            var parametros = reg.OBSERVACIONES;

                                            string[] lista = parametros.Split('|');

                                            string fecha = reg.FECHA_HORA_EMISION.ToString();

                                            objTrasaccion.NRO_DUI = lista[1].ToString();
                                            objTrasaccion.MES = Convert.ToDateTime(fecha.Substring(0, 4) + "/" + fecha.Substring(4, 2) + "/" + fecha.Substring(6, 2)).ToString("MMMM - yyyy");
                                            objTrasaccion.EMPRESA_ALMACENAJE = reg.RAZON_SOCIAL;
                                            objTrasaccion.NRO_FACTURA_ALMACENAJE = reg.NRO_TRANSACCION;
                                            objTrasaccion.TARIFA_UNITARIA = reg.PRECIO_UNITARIO;
                                            objTrasaccion.VOLUMEN_RECIBIDO = Convert.ToDecimal(lista[5].ToString());
                                            objTrasaccion.COSTO_ALMACENAJE = Math.Round(objTrasaccion.TARIFA_UNITARIA * objTrasaccion.VOLUMEN_RECIBIDO, 2);
                                            objTrasaccion.MONTO_NETO_IVA = Math.Round(objTrasaccion.COSTO_ALMACENAJE * Convert.ToDecimal("0.87"), 2);

                                            objNuevoDatoG1.Add(objTrasaccion);

                                            string[] listaProveedor = lista[6].Split('-');

                                            objTrasaccionI.NRO_DUI = lista[1].ToString();
                                            if (listaProveedor.Count() == 2)
                                            {
                                                objTrasaccionI.PROVEEDOR = listaProveedor[1].ToString();
                                                objTrasaccionI.NRO_FACTURA_PROVEEDOR = listaProveedor[0].ToString();
                                            }
                                            else
                                            {
                                                objTrasaccionI.PROVEEDOR = "";
                                                objTrasaccionI.NRO_FACTURA_PROVEEDOR = "";
                                            }
                                            objTrasaccionI.MES_RECEPCION = Convert.ToDateTime(fecha.Substring(0, 4) + "/" + fecha.Substring(4, 2) + "/" + fecha.Substring(6, 2)).ToString("MMMM - yyyy");
                                            objTrasaccionI.PLANTA = "";// txtPlanta;
                                            objTrasaccionI.VOLUMEN = Convert.ToDecimal(lista[5].ToString());

                                            objNuevoDatoI1.Add(objTrasaccionI);

                                            objVolG.RON = reg.CODIGO;
                                            objVolG.VOLUMEN = Convert.ToDecimal(lista[5].ToString());
                                            objVolG.COSTO = Math.Round(objTrasaccion.COSTO_ALMACENAJE * Convert.ToDecimal("0.87"), 2);

                                            objVolImportG.Add(objVolG);
                                        }

                                        Session[CVariablesSesion.GrillaAnexoG1] = objNuevoDatoG1;

                                        Session[CVariablesSesion.GrillaAnexoI1] = objNuevoDatoI1;

                                        var objMovG2 = lstDocumentoA.Where(x => x.OBSERVACIONES.StartsWith("G2ADF")).ToList();

                                        var objNuevoDatoG2 = new List<O_OBTENER_ANEXO_G>();

                                        var objNuevoDatoI2 = new List<O_OBTENER_ANEXO_I>();

                                        foreach (var reg in objMovG2)
                                        {
                                            var objTrasaccion = new O_OBTENER_ANEXO_G();
                                            var objTrasaccionI = new O_OBTENER_ANEXO_I();

                                            var parametros = reg.OBSERVACIONES;

                                            string[] lista = parametros.Split('|');

                                            string fecha = reg.FECHA_HORA_EMISION.ToString();

                                            objTrasaccion.NRO_DUI = lista[1].ToString();
                                            objTrasaccion.MES = Convert.ToDateTime(fecha.Substring(0, 4) + "/" + fecha.Substring(4, 2) + "/" + fecha.Substring(6, 2)).ToString("MMMM - yyyy");
                                            objTrasaccion.EMPRESA_ALMACENAJE = reg.RAZON_SOCIAL;
                                            objTrasaccion.NRO_FACTURA_ALMACENAJE = reg.NRO_TRANSACCION;
                                            objTrasaccion.TARIFA_UNITARIA = reg.PRECIO_UNITARIO;
                                            objTrasaccion.VOLUMEN_RECIBIDO = Convert.ToDecimal(lista[5].ToString());
                                            objTrasaccion.COSTO_ALMACENAJE = Math.Round(objTrasaccion.TARIFA_UNITARIA * objTrasaccion.VOLUMEN_RECIBIDO, 2);
                                            objTrasaccion.MONTO_NETO_IVA = Math.Round(objTrasaccion.COSTO_ALMACENAJE * Convert.ToDecimal("0.87"), 2);

                                            objNuevoDatoG2.Add(objTrasaccion);

                                            string[] listaProveedor = lista[6].Split('-');

                                            objTrasaccionI.NRO_DUI = lista[1].ToString();
                                            if (listaProveedor.Count() == 2)
                                            {
                                                objTrasaccionI.PROVEEDOR = listaProveedor[1].ToString();
                                                objTrasaccionI.NRO_FACTURA_PROVEEDOR = listaProveedor[0].ToString();
                                            }
                                            else
                                            {
                                                objTrasaccionI.PROVEEDOR = "";
                                                objTrasaccionI.NRO_FACTURA_PROVEEDOR = "";
                                            }
                                            objTrasaccionI.MES_RECEPCION = Convert.ToDateTime(fecha.Substring(0, 4) + "/" + fecha.Substring(4, 2) + "/" + fecha.Substring(6, 2)).ToString("MMMM - yyyy");
                                            objTrasaccionI.PLANTA = "LA PAZ";
                                            objTrasaccionI.VOLUMEN = Convert.ToDecimal(lista[5].ToString());

                                            objNuevoDatoI2.Add(objTrasaccionI);


                                            objVolG.RON = reg.CODIGO;
                                            objVolG.VOLUMEN = Convert.ToDecimal(lista[5].ToString());
                                            objVolG.COSTO = Math.Round(objTrasaccion.COSTO_ALMACENAJE * Convert.ToDecimal("0.87"), 2);

                                            objVolImportG.Add(objVolG);
                                        }

                                        Session[CVariablesSesion.GrillaAnexoG2] = objNuevoDatoG2;

                                        Session[CVariablesSesion.GrillaAnexoI2] = objNuevoDatoI2;

                                        #endregion

                                        //ANEXO H

                                        #region ANEXO H

                                        var objMovH = lstDocumentoA.Where(x => x.OBSERVACIONES.StartsWith("HCM")).ToList();

                                        var objNuevoDatoH = new List<O_OBTENER_ANEXO_H>();
                                        foreach (var reg in objMovH)
                                        {
                                            var objTrasaccion = new O_OBTENER_ANEXO_H();
                                            var objVolH = new O_OBTENER_VOLUMEN_IMPORTADO();

                                            var parametros = reg.OBSERVACIONES;

                                            string[] lista = parametros.Split('|');

                                            string[] listaProveedor = lista[6].Split('-');

                                            string fecha = reg.FECHA_HORA_EMISION.ToString();

                                            objTrasaccion.MES = Convert.ToDateTime(fecha.Substring(0, 4) + "/" + fecha.Substring(4, 2) + "/" + fecha.Substring(6, 2)).ToString("MMMM - yyyy");
                                            objTrasaccion.NRO_DUI = lista[1].ToString();
                                            if (listaProveedor.Count() == 2)
                                            {
                                                objTrasaccion.NRO_FACTURA_PROVEEDOR = listaProveedor[0].ToString();
                                            }
                                            else
                                            {
                                                objTrasaccion.NRO_FACTURA_PROVEEDOR = "";
                                            }

                                            objTrasaccion.VOLUMEN_RECIBIDO = lista[5].ToString() == "" ? 0 : Convert.ToDecimal(lista[5].ToString());
                                            objTrasaccion.MONTO_MONITOREO = reg.PRECIO_TOTAL;

                                            objNuevoDatoH.Add(objTrasaccion);

                                            objVolH.RON = reg.CODIGO;
                                            objVolH.VOLUMEN = 0;
                                            objVolH.COSTO = reg.PRECIO_TOTAL;

                                            objVolImportH.Add(objVolH);
                                        }

                                        Session[CVariablesSesion.GrillaAnexoH] = objNuevoDatoH;

                                        #endregion

                                        //ANEXO I
                                    }

                                    //FORMULARIO 1
                                    var datos = registros[0].OBSERVACION_MOVIMIENTO.ToString();
                                    listaF1 = datos.Split('|');

                                    /*lblNombreEmpresa.Text = "a) Nombre de la Empresa: " + tlsOperadores.FocusedNode.GetValue("StrDescripcion").ToString();

                                    lblMontoCompensar.Text = "a) Monto total solicitado para compensar: " + listaF1[1].ToString();

                                    if (tlsOperadores.FocusedNode.GetValue("StrDescripcion").ToString() == "YACIMIENTOS PETROLIFEROS FISCALES BOLIVIANOS - YPFB")
                                    {
                                        chbYacimiento.Checked = true;
                                        chbLogistica.Checked = false;
                                    }
                                    else
                                    {
                                        chbYacimiento.Checked = false;
                                        chbLogistica.Checked = true;
                                    }*/

                                    //formulario 2

                                    //txtDuiRegularizadaF2.Text = listaF1[0].ToString();
                                    Session[CVariablesSesion.txtDuiRegularizadaF2] = listaF1[0].ToString();
                                    //txtFechaDuiF2.Text = registros[0].FECHA_OPERACION_DIA.ToString();
                                    Session[CVariablesSesion.txtFechaDuiF2] = registros[0].FECHA_OPERACION_DIA.ToString();

                                    if (listaF1.Count() == 12)
                                    {
                                        var txtFronteraIngresoF2 = listaF1[9].ToString();
                                        Session[CVariablesSesion.txtFronteraIngresoF2] = listaF1[9].ToString();
                                        var txtIncontermsF2 = listaF1[10].ToString();
                                        Session[CVariablesSesion.txtIncontermsF2] = listaF1[10].ToString();
                                    }

                                    var txtPrecioGasolinaBlancaF2 = listaF1[4].ToString();
                                    Session[CVariablesSesion.txtPrecioGasolinaBlancaF2] = listaF1[4].ToString();
                                    var txtPrecioNetoIvaF2 = listaF1[5].ToString();
                                    Session[CVariablesSesion.txtPrecioNetoIvaF2] = listaF1[5].ToString();
                                    var txtTarifaPoliductoF2 = listaF1[6].ToString();
                                    Session[CVariablesSesion.txtTarifaPoliductoF2] = listaF1[6].ToString();
                                    var txtTarifaNetoIvaF2 = listaF1[7].ToString();
                                    Session[CVariablesSesion.txtTarifaNetoIvaF2] = listaF1[7].ToString();

                                    var datoXgb = Convert.ToDecimal(listaF1[2].ToString()) > 1 ? Convert.ToDecimal(listaF1[2].ToString()) : (Convert.ToDecimal(listaF1[2].ToString()) * 100);
                                    var datoXimp = Convert.ToDecimal(listaF1[3].ToString()) > 1 ? Convert.ToDecimal(listaF1[3].ToString()) : (Convert.ToDecimal(listaF1[3].ToString()) * 100);
                                    var datoXgbC = datoXgb / 100;
                                    var datoXimpC = datoXimp / 100;
                                    var datoPpt = Convert.ToDecimal(listaF1[8].ToString());

                                    var txtCostoGasolinaBlancaNeto = (Convert.ToDecimal(listaF1[5].ToString()) + Convert.ToDecimal(listaF1[7].ToString())).ToString();

                                    var listaAf2 = (from datoA in objVolImportA
                                                    group datoA by datoA.RON into datoAGroup
                                                    select new
                                                    {
                                                        RON = datoAGroup.Key,
                                                        VOLUMEN = datoAGroup.Sum(x => x.VOLUMEN),
                                                        COSTO = datoAGroup.Sum(x => x.COSTO)
                                                    }).ToList();

                                    var ListPro = (from datoA in objNuevoDatoA
                                                          group datoA by datoA.PROVEEDOR into datoAGroupProveedor
                                                          select new
                                                          {
                                                              PROVEEDOR = datoAGroupProveedor.Key
                                                          }).ToList();

                                    strProveedor = ListPro[0].PROVEEDOR.ToString();

                                    Session[CVariablesSesion.GrillaResumenAnexoA] = listaAf2;

                                    var listaBf2 = (from datoA in objVolImportB
                                                    group datoA by datoA.RON into datoAGroup
                                                    select new
                                                    {
                                                        RON = datoAGroup.Key,
                                                        VOLUMEN = datoAGroup.Sum(x => x.VOLUMEN),
                                                        COSTO = datoAGroup.Sum(x => x.COSTO)
                                                    }).ToList();

                                    Session[CVariablesSesion.GrillaResumenAnexoB1] = listaBf2;

                                    var listaCf2 = (from datoA in objVolImportC
                                                    group datoA by datoA.RON into datoAGroup
                                                    select new
                                                    {
                                                        RON = datoAGroup.Key,
                                                        VOLUMEN = datoAGroup.Sum(x => x.VOLUMEN),
                                                        COSTO = datoAGroup.Sum(x => x.COSTO)
                                                    }).ToList();

                                    Session[CVariablesSesion.GrillaResumenAnexoC] = listaCf2;

                                    var listaD1F2 = (from datoA in objVolImportD
                                                     where datoA.VOLUMEN == 1
                                                     group datoA by datoA.RON into datoAGroup
                                                     select new
                                                     {
                                                         RON = datoAGroup.Key,
                                                         VOLUMEN = datoAGroup.Sum(x => x.VOLUMEN),
                                                         COSTO = datoAGroup.Sum(x => x.COSTO)
                                                     }).ToList();

                                    Session[CVariablesSesion.GrillaResumenAnexoD1] = listaD1F2;

                                    var listaDf2 = (from datoA in objVolImportD
                                                    group datoA by datoA.RON into datoAGroup
                                                    select new
                                                    {
                                                        RON = datoAGroup.Key,
                                                        VOLUMEN = datoAGroup.Sum(x => x.VOLUMEN),
                                                        COSTO = datoAGroup.Sum(x => x.COSTO)
                                                    }).ToList();

                                    Session[CVariablesSesion.GrillaResumenAnexoD2] = listaDf2;

                                    var listaEf2 = (from datoA in objVolImportE
                                                    group datoA by datoA.RON into datoAGroup
                                                    select new
                                                    {
                                                        RON = datoAGroup.Key,
                                                        VOLUMEN = datoAGroup.Sum(x => x.VOLUMEN),
                                                        COSTO = datoAGroup.Sum(x => x.COSTO)
                                                    }).ToList();

                                    Session[CVariablesSesion.GrillaResumenAnexoE] = listaEf2;

                                    var listaGf2 = (from datoA in objVolImportG
                                                    group datoA by datoA.RON into datoAGroup
                                                    select new
                                                    {
                                                        RON = datoAGroup.Key,
                                                        VOLUMEN = datoAGroup.Sum(x => x.VOLUMEN),
                                                        COSTO = datoAGroup.Sum(x => x.COSTO)
                                                    }).ToList();

                                    Session[CVariablesSesion.GrillaResumenAnexoF1] = listaEf2;

                                    var listaHf2 = (from datoA in objVolImportH
                                                    group datoA by datoA.RON into datoAGroup
                                                    select new
                                                    {
                                                        RON = datoAGroup.Key,
                                                        VOLUMEN = datoAGroup.Sum(x => x.VOLUMEN),
                                                        COSTO = datoAGroup.Sum(x => x.COSTO)
                                                    }).ToList();

                                    Session[CVariablesSesion.GrillaResumenAnexoH] = listaHf2;

                                    var objNuevoDatoF1 = new List<O_OBTENER_ANEXO_F>();
                                    var objNuevoDatoF2 = new List<O_OBTENER_ANEXO_F>();
                                    var objNuevoDatoVolumen = new List<O_OBTENER_VOLUMEN_IMPORTADO>();

                                    for (int i = 0; i < listaAf2.Count(); i++)
                                    {

                                        var a = listaDf2.Count();

                                        var objTrasaccionF1 = new O_OBTENER_ANEXO_F();
                                        var objTrasaccionF2 = new O_OBTENER_ANEXO_F();

                                        if (i < a)
                                        {
                                            //ANEXO F
                                            objTrasaccionF1.NRO_DUI = listaF1[0].ToString();
                                            objTrasaccionF1.PROVEEDOR = registros[0].RAZON_SOCIAL;
                                            objTrasaccionF1.NRO_FACTURA_PROVEEDOR = "EN ANEXO A";
                                            objTrasaccionF1.MONTO_FACTURA_PROVEEDOR = listaAf2.Count() == 0 ? 0 : listaAf2[i].COSTO;
                                            objTrasaccionF1.COSTO_FINANCIERO = Math.Round(objTrasaccionF1.MONTO_FACTURA_PROVEEDOR * Convert.ToDecimal("0.001"), 2);
                                            objTrasaccionF1.COSTO_FINANCIERO_NETO = Math.Round(objTrasaccionF1.COSTO_FINANCIERO * Convert.ToDecimal("0.87"), 2);

                                            objNuevoDatoF1.Add(objTrasaccionF1);

                                            objTrasaccionF2.NRO_DUI = listaF1[0].ToString();
                                            objTrasaccionF2.PROVEEDOR = registros[0].RAZON_SOCIAL;
                                            objTrasaccionF2.NRO_FACTURA_PROVEEDOR = "EN ANEXO D1";
                                            objTrasaccionF2.MONTO_FACTURA_PROVEEDOR = listaD1F2.Count() == 0 ? 0 : listaD1F2[i].COSTO;
                                            objTrasaccionF2.COSTO_FINANCIERO = Math.Round(objTrasaccionF2.MONTO_FACTURA_PROVEEDOR * Convert.ToDecimal("0.001"), 2);
                                            objTrasaccionF2.COSTO_FINANCIERO_NETO = Math.Round(objTrasaccionF2.COSTO_FINANCIERO * Convert.ToDecimal("0.87"), 2);

                                            objNuevoDatoF2.Add(objTrasaccionF2);

                                        }

                                        var objCostoAnexo = new O_OBTENER_COSTOS_IMPORTADO();

                                        objCostoAnexo.RON = listaAf2[i].RON;
                                        objCostoAnexo.A = listaAf2.Count() == 0 ? 0 : Math.Round(listaAf2[i].COSTO, 2);
                                        objCostoAnexo.B = listaBf2.Count() == 0 ? 0 : Math.Round(listaBf2[i].COSTO, 2);
                                        objCostoAnexo.C = listaCf2.Count() == 0 ? 0 : Math.Round(listaCf2[i].COSTO, 2);
                                        objCostoAnexo.D = (listaDf2.Count() == 0 || listaDf2.Count() < listaAf2.Count()) ? 0 : Math.Round(listaDf2[i].COSTO, 2);
                                        objCostoAnexo.E = (listaEf2.Count() == 0 || listaEf2.Count() < listaAf2.Count()) ? 0 : Math.Round(listaEf2[i].COSTO, 2);
                                        objCostoAnexo.F = Math.Round(objTrasaccionF1.COSTO_FINANCIERO_NETO + objTrasaccionF2.COSTO_FINANCIERO_NETO, 2);
                                        objCostoAnexo.G = (listaGf2.Count() == 0 || listaGf2.Count() < listaAf2.Count()) ? 0 : Math.Round(listaGf2[i].COSTO, 2);
                                        objCostoAnexo.H = (listaHf2.Count() == 0 || listaHf2.Count() < listaAf2.Count()) ? 0 : Math.Round(listaHf2[i].COSTO, 2);
                                        objCostoAnexo.I = Math.Round(objCostoAnexo.A + objCostoAnexo.B + objCostoAnexo.C + objCostoAnexo.D + objCostoAnexo.E + objCostoAnexo.F + objCostoAnexo.G + objCostoAnexo.H, 2);

                                        objCostos.Add(objCostoAnexo);

                                        var objCostoSub = new O_OBTENER_COSTOS_SUBVENCION();

                                        objCostoSub.DATO_1 = "EN ANEXO A";
                                        objCostoSub.DATO_2 = listaAf2[i].RON;
                                        objCostoSub.DATO_3 = listaAf2[i].VOLUMEN;
                                        objCostoSub.DATO_4 = Math.Round((objCostoAnexo.I) / listaAf2[i].VOLUMEN, 2);
                                        objCostoSub.DATO_5 = datoXimp > 1 ? datoXimp : (datoXimp * 100);
                                        objCostoSub.DATO_6 = Convert.ToDecimal(txtCostoGasolinaBlancaNeto);
                                        objCostoSub.DATO_7 = datoXgb > 1 ? datoXgb : (datoXgb * 100);
                                        objCostoSub.DATO_8 = Math.Round((listaAf2[i].VOLUMEN) / (datoXimpC), 2);
                                        objCostoSub.DATO_9 = Math.Round((((objCostoAnexo.I) / listaAf2[i].VOLUMEN) * datoXimpC) + (datoXgbC * Convert.ToDecimal(txtCostoGasolinaBlancaNeto)), 2);

                                        objCostosSubvencion.Add(objCostoSub);

                                        var objVolumenImportado = new O_OBTENER_VOLUMEN_IMPORTADO();

                                        objVolumenImportado.RON = listaAf2[i].RON;
                                        objVolumenImportado.VOLUMEN = listaAf2[i].VOLUMEN;
                                        objVolumenImportado.COSTO = 0;

                                        objNuevoDatoVolumen.Add(objVolumenImportado);

                                        var objCostoSubFinal = new O_OBTENER_COSTOS_SUBVENCION_FINAL();

                                        objCostoSubFinal.DATO_1 = "YPFB";
                                        objCostoSubFinal.DATO_2 = "EN ANEXO A";
                                        objCostoSubFinal.DATO_3 = objCostoSub.DATO_8;
                                        objCostoSubFinal.DATO_4 = objCostoSub.DATO_9;
                                        objCostoSubFinal.DATO_5 = datoPpt;
                                        objCostoSubFinal.DATO_6 = Math.Round((objCostoSubFinal.DATO_3) * (objCostoSubFinal.DATO_4 - objCostoSubFinal.DATO_5), 2);

                                        objCostosSubvencionFinal.Add(objCostoSubFinal);
                                    }

                                    Session[CVariablesSesion.GrillaFormulario1Rubro4] = objNuevoDatoVolumen;

                                    Session[CVariablesSesion.GrillaFormulario1Rubro3] = objCostos;

                                    Session[CVariablesSesion.GrillaFormulario2Rubro4] = objCostosSubvencion;

                                    Session[CVariablesSesion.GrillaFormulario2Rubro5] = objCostosSubvencionFinal;

                                }
                            }
                        }

                        var valor2 = (from datoA in objCostosSubvencion
                                      group datoA by datoA.DATO_3 into datoAGroup
                                      select new
                                      {
                                          COSTO = datoAGroup.Sum(x => x.DATO_3)
                                      }).ToList();

                        var volImportado = valor2[0].COSTO.ToString();

                        var valor1 = (from datoA in objCostosSubvencionFinal
                                      group datoA by datoA.DATO_6 into datoAGroup
                                      select new
                                      {
                                          COSTO = datoAGroup.Sum(x => x.DATO_6)
                                      }).ToList();

                        var costoImportado = valor1[0].COSTO.ToString();

                        string strLiteral = _cUtil.Convertir(costoImportado, true, "BOLIVIANOS");

                        var strFechaDia = DateTime.Now.Day.ToString();
                        var strFechaMes = DateTime.Now.Month.ToString();
                        var strFechaGestion = DateTime.Now.Year.ToString();

                        var strDiaLiteral = _cUtil.enLetras(strFechaDia);
                        var strMesLiteral = _cUtil.mesLetras(strFechaMes);
                                               

                        Session[CVariablesSesion.TipoReporte] = EnumTipoReporte.repDjCertificadoImportacion;
                        var xreCertiImp = new repDjCertificadoImportacion();

                        xreCertiImp.strFechaDui.Value = FechaI.Substring(6,2) + "/" + FechaI.Substring(4,2) + "/" + FechaI.Substring(0,4);
                        xreCertiImp.StrDui.Value = listaF1[0].ToString();
                        xreCertiImp.strCite.Value = txtNumCertificado.Text;
                        xreCertiImp.strVolumen.Value = volImportado;
                        xreCertiImp.strPrecio.Value = costoImportado;
                        xreCertiImp.strPrecioLieteral.Value = strLiteral;
                        xreCertiImp.strProveedor.Value = strProveedor;

                        xreCertiImp.strFechaLiteral.Value = strFechaDia + " días del mes de " + strMesLiteral + " de la gestión " + strFechaGestion;

                        Session[CVariablesSesion.ReportePrincipal] = xreCertiImp;

                        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( 'WFReporte.aspx', '')", true);

                        pnlDatos_Callback(null,null);
                    }                    

                    #endregion
                }
                catch (Exception exp)
                {
                    #region RegistraError
                    MImprimirMensaje(exp.Message, EnumTiposMensajeAlerta.Error);
                    var oMensajeUsuario = new CLogTraza.MensajeUsuario
                    {
                        decUsuarioId = Session[CVariablesSesion.IdUsuario],
                        decIdModulo = CParametrosHydro.decIdAplicacion,
                        strAccion = GetType().Name + "." + MethodBase.GetCurrentMethod().Name,
                        strIp = HttpContext.Current.Request.UserHostAddress,
                        decNivelCapa = (int)CLogTraza.CapasNivel.Presentacion
                    };
                    CLogTraza.Error(oMensajeUsuario, exp);
                    #endregion
                }
            }
            else
            {
                //todo No implementado.
            }
        }
        protected void pucCargaArchivo_WindowCallback(object source, PopupWindowCallbackArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    txtPcObservacion.Text = "";
                    int index = dgvCabecera.FocusedRowIndex;
                    if (index >= 0)
                    {
                        txtPcCiteDj.Text = dgvCabecera.GetRowValues(index, "CITE_GENERADO").ToString();
                        Session["PATH"] = dgvCabecera.GetRowValues(index, "ID_REPORTE_DATO_PRINC") + "|" + dgvCabecera.GetRowValues(index, "ID_CITE");
                    }
                    else
                    {
                        pucCargaArchivo.ShowOnPageLoad = false;
                    }

                    #endregion
                }
                catch (Exception exp)
                {
                    #region RegistraError

                    MImprimirMensaje(exp.Message, EnumTiposMensajeAlerta.Error);
                    var oMensajeUsuario = new CLogTraza.MensajeUsuario
                    {
                        decUsuarioId = Session[CVariablesSesion.IdUsuario],
                        decIdModulo = CParametrosHydro.decIdAplicacion,
                        strAccion = GetType().Name + "." + MethodBase.GetCurrentMethod().Name,
                        strIp = HttpContext.Current.Request.UserHostAddress,
                        decNivelCapa = (int)CLogTraza.CapasNivel.Presentacion
                    };
                    CLogTraza.Error(oMensajeUsuario, exp);

                    #endregion
                }
            }
            else
            {
                //todo No implementado.
            }
        }

        protected void uctCargaDj_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    if (e.IsValid)
                    {
                        var strAuxiliar = Session["PATH"].ToString();
                        var astAuxiliar = strAuxiliar.Split('|');
                        decimal decIdReporte = Convert.ToDecimal(astAuxiliar[0]);
                        decimal decIdCite = Convert.ToDecimal(astAuxiliar[1]);
                        var strRutaServer = Path.GetTempPath();
                        var strNombreArchivo = DateTime.Now.Year + "" + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second;
                        Session["PATH"] = strRutaServer + strNombreArchivo;

                        e.UploadedFile.SaveAs((string)Session["PATH"]);
                        byte[] bytes = File.ReadAllBytes((string)Session["PATH"]);

                        var datFechaReg = DateTime.Now;
                        EResultado resultado = _client.Post<EResultado>(CAppSettings.svc_documento_digital,
                        new
                        {
                            strCredencial = CParametrosHydro.strCredencialFuncionario,
                            byteDocumento = bytes,
                            decEstado = 1,
                            strBinario = "00000000000000000000",
                            strObservaciones = txtPcObservacion.Text,
                            decIdTipoDocDigital = 1,
                            //decIdAutorizacionConsumidor = null,
                            decIdReporteDatoPrinc = decIdReporte,
                            decAppFechaRegistro = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0')),
                            decAppIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario])
                        });
                        if (resultado.intCodigo != 1)
                        {
                            MImprimirMensaje(resultado.strMensaje, EnumTiposMensajeAlerta.Alerta);
                        }
                        else
                        {
                            resultado = _client.Post<EResultado>(CAppSettings.svc_actualiza_reporte_estado,
                            new
                            {
                                strCredencial = CParametrosHydro.strCredencialFuncionario,
                                decFiltro = 2,
                                decIdReporteDatoPrin = 0,
                                decIdCite,
                                decEstado = 4,
                                decFechaReporte = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0')),
                                strObservaciones = txtPcObservacion.Text,
                                decAppFechaRegistro = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0')),
                                decAppIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario])
                            });
                            if (resultado.intCodigo != 1)
                            {
                                MImprimirMensaje(resultado.strMensaje, EnumTiposMensajeAlerta.Alerta);
                            }
                            else
                            {
                                //todo No implementado.
                            }
                            txtPcObservacion.Text = "";
                        }
                    }

                    #endregion
                }
                catch (Exception exp)
                {
                    #region RegistraError

                    MImprimirMensaje(exp.Message, EnumTiposMensajeAlerta.Error);
                    var oMensajeUsuario = new CLogTraza.MensajeUsuario
                    {
                        decUsuarioId = Session[CVariablesSesion.IdUsuario],
                        decIdModulo = CParametrosHydro.decIdAplicacion,
                        strAccion = GetType().Name + "." + MethodBase.GetCurrentMethod().Name,
                        strIp = HttpContext.Current.Request.UserHostAddress,
                        decNivelCapa = (int)CLogTraza.CapasNivel.Presentacion
                    };
                    CLogTraza.Error(oMensajeUsuario, exp);

                    #endregion
                }
            }
            else
            {
                //todo No implementado.
            }
        }

        protected void pucAdjuntos_WindowCallback(object source, PopupWindowCallbackArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    Session.Remove(CVariablesSesion.GrillaListaAuxiliar);
                    dgvAdjuntos.DataSource = null;
                    dgvAdjuntos.DataBind();
                    
                    var index = dgvCabecera.FocusedRowIndex;
                    txtPaCiteDj.Text = dgvCabecera.GetRowValues(index, "CITE_GENERADO").ToString();
                    var intEstado = Convert.ToInt32(dgvCabecera.GetRowValues(index, "ESTADO"));
                    dgvAdjuntos.Columns[(int)EnumColumnasGrilla.FoAbm].Visible = intEstado != 2;
                    
                    var lstDeclaraciones = _client.Post<EResultadoAdjuntos>
                        (CAppSettings.svc_lista_documentos_digital,
                        new
                        {
                            strCredencial = CParametrosHydro.strCredencialListadoDj,
                            decFiltro = "1",
                            strParametro1 = dgvCabecera.GetRowValues(index, "ID_REPORTE_DATO_PRINC").ToString().Trim(),
                            strParametro2 = "0",
                            strParametro3 = "0"
                        });
                    if (lstDeclaraciones.IntCodigo == 1)
                    {
                        var LstResultado = new List<O_LISTA_DOCUMENTO_DIGITAL_CTY>();
                        LstResultado = lstDeclaraciones.OResultado;
                        if (LstResultado.Any())
                        {
                            Session[CVariablesSesion.GrillaListaAuxiliar] = LstResultado;
                            dgvAdjuntos.DataSource = LstResultado;
                            dgvAdjuntos.DataBind();
                        }
                        else
                        {
                            //todo No implementado.
                        }
                    }
                    else
                    {
                        MImprimirMensaje(lstDeclaraciones.StrMensaje, EnumTiposMensajeAlerta.Alerta);
                    }

                    #endregion
                }
                catch (Exception exp)
                {
                    #region RegistraError

                    MImprimirMensaje(exp.Message, EnumTiposMensajeAlerta.Error);
                    var oMensajeUsuario = new CLogTraza.MensajeUsuario
                    {
                        decUsuarioId = Session[CVariablesSesion.IdUsuario],
                        decIdModulo = CParametrosHydro.decIdAplicacion,
                        strAccion = GetType().Name + "." + MethodBase.GetCurrentMethod().Name,
                        strIp = HttpContext.Current.Request.UserHostAddress,
                        decNivelCapa = (int)CLogTraza.CapasNivel.Presentacion
                    };
                    CLogTraza.Error(oMensajeUsuario, exp);

                    #endregion
                }
            }
            else
            {
                //todo No implementado.
            }
        }

        protected void dgvAdjuntos_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    int index = dgvAdjuntos.FocusedRowIndex;
                    long lngIdDocDigital = Convert.ToInt64(dgvAdjuntos.GetRowValues(index, "ID_DOCUMENTO_DIGITAL"));
                    if (lngIdDocDigital > 0)
                    {
                        var datFechaReg = DateTime.Now;
                        EResultado resultado;
                        switch (e.ButtonID)
                        {
                            case "btnEliminar":
                                resultado = _client.Post<EResultado>(CAppSettings.svc_actualiza_estado_doc_digital,
                                new
                                {
                                    strCredencial = CParametrosHydro.strCredencialFuncionario,
                                    decFiltro = 1,
                                    decIdDocumentoDigital = lngIdDocDigital,
                                    decEstado = 2,
                                    strObservaciones = "",
                                    decAppFechaRegistro = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0')),
                                    decAppIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario])
                                });
                                if (resultado.intCodigo != 1)
                                {
                                    MImprimirMensaje(resultado.strMensaje, EnumTiposMensajeAlerta.Alerta);
                                }
                                else
                                {
                                    pucAdjuntos_WindowCallback(null, null);
                                }
                                break;
                            default:
                                //todo No implementado.
                                break;
                        }
                    }
                    else
                    {
                        //todo No implementado.
                    }

                    #endregion
                }
                catch (Exception exp)
                {
                    #region RegistraError

                    MImprimirMensaje(exp.Message, EnumTiposMensajeAlerta.Error);
                    var oMensajeUsuario = new CLogTraza.MensajeUsuario
                    {
                        decUsuarioId = Session[CVariablesSesion.IdUsuario],
                        decIdModulo = CParametrosHydro.decIdAplicacion,
                        strAccion = GetType().Name + "." + MethodBase.GetCurrentMethod().Name,
                        strIp = HttpContext.Current.Request.UserHostAddress,
                        decNivelCapa = (int)CLogTraza.CapasNivel.Presentacion
                    };
                    CLogTraza.Error(oMensajeUsuario, exp);

                    #endregion
                }
            }
            else
            {
                //todo No implementado.
            }
        }

        protected void dgvAdjuntos_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    int index = dgvAdjuntos.FocusedRowIndex;
                    long lngIdDocDigital = Convert.ToInt64(dgvAdjuntos.GetRowValues(index, "ID_DOCUMENTO_DIGITAL"));

                    if (index >= 0 && lngIdDocDigital > 0)
                    {
                        switch (e.CommandArgs.CommandName)
                        {
                            case "lnkDescargar":
                                var bytDocumento = (byte[])dgvAdjuntos.GetRowValues(index, "DOCUMENTO");
                                Response.Clear();
                                Response.AddHeader("Content-Length", bytDocumento.Length.ToString(CultureInfo.InvariantCulture));
                                Response.ContentType = "application/pdf";
                                Response.AddHeader("Expires", "0");
                                Response.AddHeader("Cache-Control", "must-revalidate, post-check=0, pre-check=0");
                                Response.AddHeader("Pragma", "public");
                                Response.AddHeader("Content-Disposition", "attachment; filename=" + txtPaCiteDj.Text.Trim() + ".pdf");
                                Response.BinaryWrite(bytDocumento);
                                Response.Flush();
                                Response.Close();
                                break;
                            default:
                                break;
                        }
                    }

                    #endregion
                }
                catch (Exception exp)
                {
                    #region RegistraError

                    MImprimirMensaje(exp.Message, EnumTiposMensajeAlerta.Error);
                    var oMensajeUsuario = new CLogTraza.MensajeUsuario
                    {
                        decUsuarioId = Session[CVariablesSesion.IdUsuario],
                        decIdModulo = CParametrosHydro.decIdAplicacion,
                        strAccion = GetType().Name + "." + MethodBase.GetCurrentMethod().Name,
                        strIp = HttpContext.Current.Request.UserHostAddress,
                        decNivelCapa = (int)CLogTraza.CapasNivel.Presentacion
                    };
                    CLogTraza.Error(oMensajeUsuario, exp);

                    #endregion
                }
            }
            else
            {
                //todo No implementado.
            }
        }

        protected void dgvAdjuntos_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            try
            {
                #region Proceso a Ejecutar

                if (e.VisibleIndex >= 0)
                {
                    if (e.VisibleIndex % 2 == 1)
                    {
                        e.Row.BackColor = Color.FromArgb(253, 237, 212);
                    }
                    else
                    {
                        //todo No implementado.
                    }
                }
                else
                {
                    //todo No implementado.
                }

                #endregion
            }
            catch (Exception exp)
            {
                #region RegistraError

                MImprimirMensaje(exp.Message, EnumTiposMensajeAlerta.Error);
                var oMensajeUsuario = new CLogTraza.MensajeUsuario
                {
                    decUsuarioId = Session[CVariablesSesion.IdUsuario],
                    decIdModulo = CParametrosHydro.decIdAplicacion,
                    strAccion = GetType().Name + "." + MethodBase.GetCurrentMethod().Name,
                    strIp = HttpContext.Current.Request.UserHostAddress,
                    decNivelCapa = (int)CLogTraza.CapasNivel.Presentacion
                };
                CLogTraza.Error(oMensajeUsuario, exp);

                #endregion
            }
        }

        protected void tlsOperadores_CustomCallback(object sender, DevExpress.Web.ASPxTreeList.TreeListCustomCallbackEventArgs e)
        {
            try
            {
                #region Proceso a Ejecutar

                if (txtBuscador.Text.Trim().Length > 0)
                {
                    var lstArbol = Session[CVariablesSesion.EstructuraArbol] as List<CArbol>;
                    tlsOperadores.CollapseAll();
                    var lstCoinsidencia = lstArbol.Where(x => x.StrDescripcion.ToLower().Contains(txtBuscador.Text.Trim().ToLower())).OrderByDescending(x => x.IntId).ToList();
                    foreach (var reg in lstCoinsidencia)
                    {
                        Session[CVariablesSesion.EntidadSeleccionada] = reg.StrUbicacion;
                        var vecSel = reg.StrUbicacion.Split('-');
                        switch (vecSel.Count())
                        {
                            case 1:
                                tlsOperadores.Nodes[Convert.ToInt16(vecSel[0])].Focus();
                                break;
                            case 2:
                                tlsOperadores.Nodes[Convert.ToInt16(vecSel[0])].ChildNodes[Convert.ToInt16(vecSel[1])].Focus();
                                break;
                            case 3:
                                tlsOperadores.Nodes[Convert.ToInt16(vecSel[0])].ChildNodes[Convert.ToInt16(vecSel[1])].ChildNodes[Convert.ToInt16(vecSel[2])].Focus();
                                break;
                        }
                    }
                }

                #endregion
            }
            catch (Exception exp)
            {
                #region RegistraError

                MImprimirMensaje(exp.Message, EnumTiposMensajeAlerta.Error);
                var oMensajeUsuario = new CLogTraza.MensajeUsuario
                {
                    decUsuarioId = Session[CVariablesSesion.IdUsuario],
                    decIdModulo = CParametrosHydro.decIdAplicacion,
                    strAccion = GetType().Name + "." + MethodBase.GetCurrentMethod().Name,
                    strIp = HttpContext.Current.Request.UserHostAddress,
                    decNivelCapa = (int)CLogTraza.CapasNivel.Presentacion
                };
                CLogTraza.Error(oMensajeUsuario, exp);

                #endregion
            }
        }

        #endregion

        #region Metodos y funciones

        /// <summary>
        /// METODO. MValidaSesion
        /// </summary>
        /// <returns></returns>
        private bool MValidaSesion()
        {
            bool booResultado;
            //borrar
            //Session[CVariablesSesion.IdUsuario] = "1802";
            if (Session[CVariablesSesion.IdUsuario] == null)
            {
                try
                {
                    Response.Redirect("~/Sitio/Persona/wfAutenticacion.aspx", false);
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                }
                catch (Exception)
                {
                    string script = string.Format("document.location.href = '{0}');", "~/Sitio/Persona/wfAutenticacion.aspx");
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "redirect", script, true);
                }
                booResultado = false;
            }
            else
            {
                booResultado = true;
            }
            return booResultado;
        }

        /// <summary>
        /// METODO. MCargaPermisos
        /// </summary>
        private void MCargaPermisos()
        {
            try
            {
                var clienteJsonSeg = new JsonServiceClient(System.Web.Configuration.WebConfigurationManager.AppSettings["ServicioJsonSeguridad"]);
                var idUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);

                var lstResultado = clienteJsonSeg.Get<EResultadoUsuarioPermisosForm>("/EPermisoFormularioUsuario/" +
                                                                      CParametrosHydro.strCredencialHydroAdmin + "/" +
                                                                      idUsuario +
                                                                      "/" + CParametrosHydro.decIdAplicacion +
                                                                      "/WFDjMonitoreo.aspx?format=json");
                if (lstResultado.decCodigo == 1)
                {
                    if (lstResultado.oResultado != null && lstResultado.oResultado.Any())
                    {
                        Session[CVariablesSesion.PermisoCons] = lstResultado.oResultado.FirstOrDefault().CONSULTA;
                        Session[CVariablesSesion.PermisoAlta] = lstResultado.oResultado.FirstOrDefault().ALTAS;
                        Session[CVariablesSesion.PermisoBaja] = lstResultado.oResultado.FirstOrDefault().BAJAS;
                        Session[CVariablesSesion.PermisoModi] = lstResultado.oResultado.FirstOrDefault().MODIFICACIONES;
                        Session[CVariablesSesion.PermisoRevi] = lstResultado.oResultado.FirstOrDefault().REVISION;
                        Session[CVariablesSesion.PermisoApro] = lstResultado.oResultado.FirstOrDefault().APROBACION;
                    }
                    else
                    {
                        Session[CVariablesSesion.PermisoCons] = 0;
                        Session[CVariablesSesion.PermisoAlta] = 0;
                        Session[CVariablesSesion.PermisoBaja] = 0;
                        Session[CVariablesSesion.PermisoModi] = 0;
                        Session[CVariablesSesion.PermisoRevi] = 0;
                        Session[CVariablesSesion.PermisoApro] = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                #region RegistraError
                var oMensajeUsuario = new CLogTraza.MensajeUsuario
                {
                    decUsuarioId = Session[CVariablesSesion.UsuarioId],
                    decIdModulo = CParametrosHydro.decIdAplicacion,
                    strAccion = GetType().Name + "." + MethodBase.GetCurrentMethod().Name,
                    strIp = HttpContext.Current.Request.UserHostAddress,
                    decNivelCapa = Convert.ToInt16(CLogTraza.CapasNivel.Presentacion)
                };

                CLogTraza.Error(oMensajeUsuario, ex);
                #endregion
            }
        }

        /// <summary>
        /// METODO. MCargaDatosIniciales
        /// </summary>
        private void MCargaDatosIniciales()
        {
            cmbMes.Value = DateTime.Now.Month;
            cmbGestion.Value = DateTime.Now.Year;
            btnReimprimir.Visible = Convert.ToInt16(Session[CVariablesSesion.PermisoCons]) != 0;
        }

        /// <summary>
        /// METODO. MCargaParametros
        /// </summary>
        private void MCargaParametros()
        {
            //Obtener condiciones de uso
            var objParametros = _client.Post<EResultadoOctanoParametro>(CAppSettings.svc_listar_octano_parametrica,
            new
            {
                strCredencial = CParametrosHydro.strCredencialFuncionario,
                decFiltro = 1,//1: ID_TIPO_ACTIVIDAD 2: ID_TIPO_ACTIVIDAD y VALOR_TIPO
                strParametro1 = "",//VALOR_TIPO
                strParametro2 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
                strParametro3 = "",
                strParametro4 = ""
            });
            if (objParametros.IntCodigo != 1)
            {
                MImprimirMensaje(objParametros.StrMensaje, EnumTiposMensajeAlerta.Error);
            }
            else
            {
                Session[CVariablesSesion.OctanoParametrica] = objParametros.OResultado;
                var objPeriodo = _client.Post<EResultadoPeriodos>(CAppSettings.svc_lista_periodos_consumidor,
                new
                {
                    strCredencial = CParametrosHydro.strCredencialFuncionario,
                    decFiltro = 1,
                    strParametro1 = tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString(),
                    strParametro2 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
                    strParametro3 = "",
                    strParametro4 = "",
                    strParametro5 = tlsOperadores.FocusedNode.GetValue("DecIdDepartamento").ToString()
                });
                if (objPeriodo.IntCodigo == 1)
                {
                    var lstPeriodo = objPeriodo.OResultado;
                    lstPeriodo.RemoveAt(lstPeriodo.Count - 1);
                    Session[CVariablesSesion.ListaPeriodos] = lstPeriodo;
                }
                else
                {
                    MImprimirMensaje(objPeriodo.StrMensaje, EnumTiposMensajeAlerta.Error);
                }
            }
        }

        /// <summary>
        /// METODO. MCargaArbol
        /// </summary>
        private void MCargaArbol()
        {
            var objArbol = Session[CVariablesSesion.ObjetoArbol] as List<O_OBTIENE_ENTIDADES_CTY>;
            if (objArbol == null)
            {
                var objResultado = _consulta.ObtenerEntidades(
                new EObjetoEnvio5ParametrosProto
                {
                    decFiltro = 1,
                    strParametro1 = Session[CVariablesSesion.IdUsuario].ToString(),
                    strParametro2 = cConsultasParametricas.ObtieneIdsActividadOctanioVol(),
                    strParametro3 = CAppSettings.idAplicacion,
                    strParametro4 = "",
                    strParametro5 = ""
                });
                if (objResultado.intCodigo == 1)
                {
                    objArbol = (List<O_OBTIENE_ENTIDADES_CTY>)objResultado.oResultado;
                    Session[CVariablesSesion.ObjetoArbol] = objArbol;
                    Session.Add("entidades_usuario", objResultado);
                }
                else
                {
                    MImprimirMensaje(objResultado.strMensaje, EnumTiposMensajeAlerta.Error);
                    return;
                }
            }
            Session[CVariablesSesion.EstructuraArbol] = _cUtil.MObtenerArbolOrdenado(Properties.Settings.Default.intOrdenArbol, objArbol);
        }

        /// <summary>
        /// METODO. MCargaGrilla
        /// </summary>
        /// <param name="decFiltro"></param>
        /// <param name="strParametro1"></param>
        /// <param name="strParametro2"></param>
        /// <param name="strParametro3"></param>
        /// <param name="strParametro4"></param>
        private void MCargaGrilla(int decFiltro, string strParametro1, string strParametro2, string strParametro3, string strParametro4)
        {
            var lstDeclaraciones = _client.Post<EResultadoListadoDj>(CAppSettings.svc_listar_reporte_datos_principal,
                new
                {
                    strCredencial = CParametrosHydro.strCredencialListadoDj,
                    decFiltro, //TipoMovimiento (1=ventas)
                    strParametro1, //Id proyecto/entidad
                    strParametro2, //id Actividad
                    strParametro3, //fecha inicial
                    strParametro4 //fecha final
                });
            if (lstDeclaraciones.IntCodigo == 1)
            {
                var LstResultado = new List<O_LISTA_REPORTE_DATOSP_CTY>();
                LstResultado = lstDeclaraciones.OResultado;
                if (LstResultado.Any())
                {
                    LstResultado.ForEach(a => a.StrEstado = ((a.ESTADO == 1) ? "DECLARADO" : ((a.ESTADO == 2) ? "ANULADO" : ((a.ESTADO == 3) ? "RECEPCIONADO" : "ARCH.ADJUNTO"))));
                    LstResultado.ForEach(a => a.StrFecha = a.FECHA_REPORTE.ToString(CultureInfo.InvariantCulture).Substring(6, 2) + "/" + a.FECHA_REPORTE.ToString(CultureInfo.InvariantCulture).Substring(4, 2) + "/" + a.FECHA_REPORTE.ToString(CultureInfo.InvariantCulture).Substring(0, 4) + " " + a.FECHA_REPORTE.ToString(CultureInfo.InvariantCulture).Substring(8, 2) + ":" + a.FECHA_REPORTE.ToString(CultureInfo.InvariantCulture).Substring(10, 2) + ":" + a.FECHA_REPORTE.ToString(CultureInfo.InvariantCulture).Substring(12, 2));
                    LstResultado = LstResultado.OrderBy(x => x.FECHA_REPORTE).ToList();

                    Session[CVariablesSesion.GrillaCabecera] = LstResultado;
                    dgvCabecera.DataSource = LstResultado;
                    dgvCabecera.DataBind();
                }
                else
                {
                    //todo No implementado.
                }
            }
            else
            {
                MImprimirMensaje(lstDeclaraciones.StrMensaje, EnumTiposMensajeAlerta.Alerta);
            }
        }

        /// <summary>
        /// METODO. MValidaDatosIniciales
        /// </summary>
        /// <param name="strMesnaje"></param>
        /// <returns></returns>
        private bool MValidaDatosIniciales(ref string strMesnaje)
        {
            bool booResultado = true;

            if (cmbGestion.Text == "" || cmbMes.Text == "")
            {
                strMesnaje = "Introduzca un periodo valido.";
                booResultado = false;
            }
            else
            {
                //todo No implementado.
            }
            return booResultado;
        }

        /// <summary>
        /// METODO. MLimpiaGrilla
        /// </summary>
        private void MLimpiaGrilla()
        {
            Session.Remove(CVariablesSesion.GrillaCabecera);
            dgvCabecera.DataSource = null;
            dgvCabecera.DataBind();

            Session.Remove(CVariablesSesion.GrillaAcumulado);
            pvgAcumulado.DataSource = null;
            pvgAcumulado.DataBind();

            Session.Remove(CVariablesSesion.GrillaInternacional);
            dgvInternacional.DataSource = null;
            dgvInternacional.DataBind();

            Session.Remove(CVariablesSesion.GrillaAuxiliar);
            dgvContenedor.DataSource = null;
            dgvContenedor.DataBind();

            Session.Remove(CVariablesSesion.GrillaRecaudo);
            dgvRecaudo.DataSource = null;
            dgvRecaudo.DataBind();

            pnlMensaje.Visible = false;
            lblTituloDetalle.Text = "DETALLE DE LA DECLARACION JURADA";

            for (int i = 49; i > 5; i--)
            {
                pvgAcumulado.Fields[i].Visible = false;
            }
            tacMovimientos.Tabs[1].ClientVisible = false;
            tacMovimientos.Tabs[2].ClientVisible = false;
            tacMovimientos.Tabs[3].ClientVisible = false;
        }

        /// <summary>
        /// METODO. MConfiguraGrilla
        /// </summary>
        /// <param name="idActividad"></param>
        private void MConfiguraGrilla(int idActividad)
        {
            pvgAcumulado.Fields[(int)EnumColumnasGrilla.PvAcInfraestructura].Visible = true;
            pvgAcumulado.Fields[(int)EnumColumnasGrilla.PvAcTipoInfraestructura].Visible = true;
            pvgAcumulado.Fields[(int)EnumColumnasGrilla.PvAcProducto].Visible = true;
            pvgAcumulado.Fields[(int)EnumColumnasGrilla.PvAcCodProducto].Visible = true;

            var objParametricas = Session[CVariablesSesion.OctanoParametrica] as List<O_LISTA_OCTANO_PAR_CTY>;
            var oListaOctanoParCty = objParametricas.Where(x => x.TIPO_VALOR == 7).ToList().FirstOrDefault();
            if (oListaOctanoParCty != null && oListaOctanoParCty.VALOR_FIN == "2")
            {
                pvgAcumulado.Fields[(int)EnumColumnasGrilla.PvAcCodProducto].Visible = false;
            }
            else
            {
                pvgAcumulado.Fields[(int)EnumColumnasGrilla.PvAcInfraestructura].Visible = false;
                pvgAcumulado.Fields[(int)EnumColumnasGrilla.PvAcTipoInfraestructura].Visible = false;
                pvgAcumulado.Fields[(int)EnumColumnasGrilla.PvAcProducto].Visible = false;
            }

            var pvArea = PivotArea.RowArea;
            for (int i = 0; i < 6; i++)
            {
                if (pvgAcumulado.Fields[i].Visible)
                {
                    pvgAcumulado.Fields[i].SetAreaPosition(pvArea, i);
                }
            }

            pvgAcumulado.Fields[(int)EnumColumnasGrilla.PvAcToatlEgr].Visible = true;
            if (idActividad != (int)EnumActividades.EstacionServicioGnv)
            {
                pvgAcumulado.Fields[(int)EnumColumnasGrilla.PvAcSaldoIni].Visible = true;
                pvgAcumulado.Fields[(int)EnumColumnasGrilla.PvAcTotalIng].Visible = true;
                pvgAcumulado.Fields[(int)EnumColumnasGrilla.PvAcSaldoFin].Visible = true;
            }
        }

        /// <summary>
        /// METODO. MImprimirMensaje
        /// </summary>
        /// <param name="strMensaje"></param>
        /// <param name="tipoMensaje"></param>
        private void MImprimirMensaje(string strMensaje, EnumTiposMensajeAlerta tipoMensaje)
        {
            switch (tipoMensaje)
            {
                case EnumTiposMensajeAlerta.Error:
                    pnlMensaje.Visible = true;
                    pnlMensaje.BorderColor = Color.DarkRed;
                    pnlMensaje.BackColor = Color.Pink;
                    strMensaje = "ERROR: " + strMensaje;
                    break;
                case EnumTiposMensajeAlerta.Alerta:
                    pnlMensaje.Visible = true;
                    pnlMensaje.BorderColor = Color.OrangeRed;
                    pnlMensaje.BackColor = Color.LightSalmon;
                    strMensaje = "  ALERTA: " + strMensaje;
                    break;
                case EnumTiposMensajeAlerta.Correcto:
                    pnlMensaje.Visible = true;
                    pnlMensaje.BorderColor = Color.DarkGreen;
                    pnlMensaje.BackColor = Color.DarkSeaGreen;
                    strMensaje = "  CORRECTO: " + strMensaje;
                    break;
                case EnumTiposMensajeAlerta.Limpiar:
                    pnlMensaje.Visible = false;
                    lblMensajeError.ForeColor = Color.WhiteSmoke;
                    strMensaje = "-";
                    break;
                default:
                    pnlMensaje.Visible = false;
                    lblMensajeError.ForeColor = Color.WhiteSmoke;
                    strMensaje = "-";
                    break;
            }
            if (tipoMensaje != EnumTiposMensajeAlerta.Correcto)
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError", "alert('" + strMensaje + "');", true);
            lblMensajeError.Text = "   " + strMensaje + "   ";
        }

        #endregion

    }
}