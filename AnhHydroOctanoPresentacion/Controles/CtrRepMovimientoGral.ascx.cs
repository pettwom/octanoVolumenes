
namespace AnhHydroOctanoPresentacion.Controles
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Reflection;
    using System.Web;
    using System.Web.UI;
    using AnhHydroOctanoAgenteServicio;
    using AnhHydroOctanoPersistencia.Entidades;
    using Clases.Controles;
    using Entidades;
    using Enumeradores;
    using Parametros;
    using Sitio.Usuarios;
    using DevExpress.Utils;
    using DevExpress.XtraCharts;
    using DevExpress.Web;
    using Librerias.Anh.Us;
    using ServiceStack.ServiceClient.Web;
    using ServiceStack.Text;
    using CArbol = Clases.Controles.CArbol;
    using EResultadoEntidad = Entidades.EResultadoEntidad;
    using Page = System.Web.UI.Page;

    public partial class CtrRepMovimientoGral : UserControl
    {
        #region Variables de entorno

        readonly JsonServiceClient _client = new JsonServiceClient(CAppSettings.servicio_octVolumen);
        readonly JsonServiceClient _clientAdminHydro = new JsonServiceClient(CAppSettings.ServicioAdministradorHydro);
        
        #endregion

        #region Eventos de Formulario

        protected void Page_Load(object sender, EventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Codigo a controlar

                    if (!Page.IsCallback && !Page.IsPostBack)
                    {
                        Session.Remove(CVariablesSesion.EstructuraArbol);
                        Session.Remove(CVariablesSesion.FechaHoraServ);
                        Session.Remove(CVariablesSesion.GrillaAcumulado);
                        Session.Remove(CVariablesSesion.GrillaAuxiliar);
                        Session.Remove(CVariablesSesion.GrillaListaAuxiliar);
                        Session.Remove(CVariablesSesion.GrillaCabecera);
                        Session.Remove(CVariablesSesion.GrillaHistorial);
                        Session.Remove(CVariablesSesion.GrillaInternacional);
                        Session.Remove(CVariablesSesion.GrillaIntimacion);
                        Session.Remove(CVariablesSesion.ListaPerfiles);
                        Session.Remove(CVariablesSesion.ObtenerEntidades);
                        Session.Remove(CVariablesSesion.ReportePrincipal);
                        Session.Remove(CVariablesSesion.TipoReporte);
                        Session.Remove(CVariablesSesion.OctanoParametrica);

                        MCargaParametricas();
                        MCargaArbol();
                        MIniciaFechas();
                    }

                    tlsOperadores.DataSource = Session[CVariablesSesion.EstructuraArbol];
                    tlsOperadores.DataBind();

                    if (Session[CVariablesSesion.GrillaCabecera] != null)
                    {
                        dgvReporte.DataSource = Session[CVariablesSesion.GrillaCabecera];
                        dgvReporte.DataBind();
                    }

                    if (Session[CVariablesSesion.GrillaAmpliacion] != null)
                    {
                        dgvAmpliacion.DataSource = Session[CVariablesSesion.GrillaAmpliacion];
                        dgvAmpliacion.DataBind();
                    }
                    else
                    {
                        //todo No es carga inicial: no implementado
                    }

                    if (Session[CVariablesSesion.GrillaIntimacion] != null)
                    {
                        dgvIntimacion.DataSource = Session[CVariablesSesion.GrillaIntimacion];
                        dgvIntimacion.DataBind();
                    }
                    else
                    {
                        //todo No es carga inicial: no implementado
                    }

                    if (Session[CVariablesSesion.GrillaHistorial] != null)
                    {
                        dgvHistorial.DataSource = Session[CVariablesSesion.GrillaHistorial];
                        dgvHistorial.DataBind();
                    }
                    else
                    {
                        //todo No es carga inicial: no implementado
                    }

                    if (Session[CVariablesSesion.GrillaAuxiliar] != null)
                    {
                        dgvReporte.DataSource = Session[CVariablesSesion.GrillaAuxiliar];
                        dgvReporte.DataBind();
                    }
                    else
                    {
                        //todo No es carga inicial: no implementado
                    }

                    if (Session[CVariablesSesion.GrillaInternacional] != null)
                    {
                        dgvInternacional.DataSource = Session[CVariablesSesion.GrillaInternacional];
                        dgvInternacional.DataBind();
                    }
                    else
                    {
                        //todo No es carga inicial: no implementado
                    }

                    if (Session[CVariablesSesion.GrillaListaAuxiliar] != null)
                    {
                        dgvContenedor.DataSource = Session[CVariablesSesion.GrillaListaAuxiliar];
                        dgvContenedor.DataBind();
                    }
                    else
                    {
                        //todo No es carga inicial: no implementado
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
                    #region Codigo a controlar

                    return;

                    lblActividad.Text = tlsOperadores.FocusedNode.GetValue("StrDescPadre").ToString();
                    MLimpiaGrilla();
                    if ((int)tlsOperadores.FocusedNode.GetValue("IntAccion") > 0)
                    {
                        if (MValidaDatosIniciales())
                        {
                            decimal decIdActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad"));
                            MCargaParametricasEsp();
                            MCargaGrilla(decIdActividad);
                            MCargaMapa(sender);
                            MCargaGrafico();
                        }
                    }

                    #endregion
                }
                catch (Exception exp)
                {
                    #region RegistraError
                    MLimpiaGrilla();
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

        protected void dgvReporte_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            try
            {
                #region Codigo a controlar

                if (e.VisibleIndex >= 0)
                {
                    if (e.VisibleIndex % 2 == 1)
                    {
                        e.Row.BackColor = Color.FromArgb(227, 226, 225);
                    }
                    else
                    {
                        //todo No implementado.
                    }

                    int valor = Convert.ToInt32(dgvReporte.GetRowValues(e.VisibleIndex, "ESTADO_MENSUAL"));
                    switch (valor)
                    {
                        case 2: //EN PLAZO
                            //e.Row.BackColor = Color.FromArgb(212, 245, 212);
                            e.Row.ForeColor = Color.FromArgb(54, 109, 54);
                            break;
                        case 3: //PLAZO VENCIDO
                            //e.Row.BackColor = Color.FromArgb(240, 152, 152);
                            e.Row.ForeColor = Color.FromArgb(236, 0, 0);
                            break;
                        case 4: //AMPLIACION
                            //e.Row.BackColor = Color.FromArgb(188, 188, 194);
                            e.Row.ForeColor = Color.FromArgb(25, 46, 105);
                            break;
                        case 5: //INTIMACION
                            //e.Row.BackColor = Color.FromArgb(194, 87, 81);
                            e.Row.ForeColor = Color.FromArgb(124, 7, 0);
                            break;
                        case 6: //NO HABILITADO
                            //e.Row.BackColor = Color.FromArgb(226, 225, 176);
                            e.Row.ForeColor = Color.FromArgb(71, 71, 71);
                            break;
                        default: //DECLARADO
                            //e.Row.BackColor = Color.FromArgb(142, 195, 142);
                            e.Row.ForeColor = Color.FromArgb(0, 156, 0);
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

        protected void dgvReporte_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    var objPerfil = Session[CVariablesSesion.ListaPerfiles] as List<O_USUARIO_PERFIL_CTY>;
                    if (objPerfil != null)
                    {
                        objPerfil = objPerfil.Where(x => x.NOMBRE_PERFIL.Contains("ANH")).ToList();
                        if (!objPerfil.Any())
                        {
                            if (e.ButtonID == "btnAmpliar")
                                e.Visible = DefaultBoolean.False;
                            if (e.ButtonID == "btnIntimar")
                                e.Visible = DefaultBoolean.False;
                        }
                    }
                    else
                    {
                        if (e.ButtonID == "btnAmpliar")
                            e.Visible = DefaultBoolean.False;
                        if (e.ButtonID == "btnIntimar")
                            e.Visible = DefaultBoolean.False;
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

        protected void dgvAcumulado_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    dgvAcumulado.Columns[2].Visible = false;
                    for (int i = 6; i <= 25; i++)
                    {
                        dgvAcumulado.Columns[i].Visible = false;
                        dgvAcumulado.Columns[21 + i].Visible = false;
                    }

                    Session.Remove(CVariablesSesion.GrillaAcumulado);
                    dgvAcumulado.DataSource = null;
                    dgvAcumulado.DataBind();

                    int index = dgvReporte.FocusedRowIndex;
                    if (index >= 0)
                    {
                        int intEstado = Convert.ToInt32(dgvReporte.GetRowValues(index, "ESTADO_MENSUAL").ToString());
                        if (intEstado == 1)
                        {
                            string strJsonReportes = dgvReporte.GetRowValues(index, "JSON_REPORTE_DATOS").ToString();
                            if (!string.IsNullOrEmpty(strJsonReportes))
                            {
                                var objDdjj = JsonSerializer.DeserializeFromString<O_DECLARACION_JURADA_CTY>(strJsonReportes);
                                var lstAcumulado = objDdjj.LstAcumulado;
                                if (lstAcumulado.Any())
                                {
                                    #region acumulado
                                    decimal decProd = 0;
                                    int cont = 1, intContProd = 1;

                                    lstAcumulado.ForEach(a => a.DecIdUnicoHidrocarburo = Convert.ToDecimal(a.DecIdHidrocarburo.ToString() + ((a.DecIdTipoInfraestructura == null) ? "" : a.DecIdTipoInfraestructura.ToString().Replace("-", "")) + ((a.DecIdInfraestructura == null) ? "" : a.DecIdInfraestructura.ToString().Replace("-", ""))));
                                    lstAcumulado = lstAcumulado.OrderBy(x => x.DecIdHidrocarburo).ThenBy(x => x.StrTipoInfraestructura).ThenBy(x => x.StrInfraestructura).ThenBy(x => x.DecFecha).ToList();

                                    //lstAcumulado = lstAcumulado.OrderBy(x => x.DecIdHidrocarburo).ThenBy(x => x.DecFecha).ToList();
                                    var lstMovimientos = new List<CMovAcumulado>();

                                    decimal decTotIng = 0, decTotSal = 0;
                                    decimal[] decIng = new decimal[20];
                                    decimal[] decSal = new decimal[20];

                                    var cDato = new CMovAcumulado();

                                    foreach (var lst in lstAcumulado)
                                    {

                                        if (decProd != lst.DecIdUnicoHidrocarburo)
                                        {
                                            if (decProd > 0)
                                            {
                                                cDato.DecIng01 = decIng[0];
                                                cDato.DecIng02 = decIng[1];
                                                cDato.DecIng03 = decIng[2];
                                                cDato.DecIng04 = decIng[3];
                                                cDato.DecIng05 = decIng[4];
                                                cDato.DecIng06 = decIng[5];
                                                cDato.DecIng07 = decIng[6];
                                                cDato.DecIng08 = decIng[7];
                                                cDato.DecIng09 = decIng[8];
                                                cDato.DecIng10 = decIng[9];
                                                cDato.DecIng11 = decIng[10];
                                                cDato.DecIng12 = decIng[11];
                                                cDato.DecIng13 = decIng[12];
                                                cDato.DecIng14 = decIng[13];
                                                cDato.DecIng15 = decIng[14];
                                                cDato.DecIng16 = decIng[15];
                                                cDato.DecIng17 = decIng[16];
                                                cDato.DecIng18 = decIng[17];
                                                cDato.DecIng19 = decIng[18];
                                                cDato.DecIng20 = decIng[19];

                                                cDato.DecSal01 = decSal[0];
                                                cDato.DecSal02 = decSal[1];
                                                cDato.DecSal03 = decSal[2];
                                                cDato.DecSal04 = decSal[3];
                                                cDato.DecSal05 = decSal[4];
                                                cDato.DecSal06 = decSal[5];
                                                cDato.DecSal07 = decSal[6];
                                                cDato.DecSal08 = decSal[7];
                                                cDato.DecSal09 = decSal[8];
                                                cDato.DecSal10 = decSal[9];
                                                cDato.DecSal11 = decSal[10];
                                                cDato.DecSal12 = decSal[11];
                                                cDato.DecSal13 = decSal[12];
                                                cDato.DecSal14 = decSal[13];
                                                cDato.DecSal15 = decSal[14];
                                                cDato.DecSal16 = decSal[15];
                                                cDato.DecSal17 = decSal[16];
                                                cDato.DecSal18 = decSal[17];
                                                cDato.DecSal19 = decSal[18];
                                                cDato.DecSal20 = decSal[19];

                                                cDato.DecTotalIng = decTotIng;
                                                cDato.DecTotalSal = decTotSal;

                                                lstMovimientos.Add(cDato);
                                            }

                                            decProd = lst.DecIdUnicoHidrocarburo;

                                            cDato = new CMovAcumulado();
                                            cDato.IntId = intContProd;
                                            cDato.IntIdProducto = (int)lst.DecIdHidrocarburo;
                                            cDato.StrProducto = lst.StrHidrocarburo;
                                            cDato.StrCodProducto = lst.StrCodHidrocarburo;
                                            cDato.StrUnidadMedida = lst.StrUnidadMedida;
                                            cDato.StrTipoInfraestructura = lst.StrTipoInfraestructura;
                                            cDato.StrInfraestructura = lst.StrInfraestructura;
                                            cDato.DecSaldoInicial = lst.DecSaldoInicial;

                                            intContProd++;
                                            decTotIng = 0;
                                            decTotSal = 0;

                                            for (int i = 0; i < 20; i++)
                                            {
                                                decIng[i] = 0;
                                                decSal[i] = 0;
                                            }
                                        }

                                        cDato.DecSaldoFinal = lst.DecSaldoFinal;
                                        decTotIng += lst.DecTotalIngreso;
                                        decTotSal += lst.DecTotalEgreso;

                                        if (lst.LstIngresos == null || !lst.LstIngresos.Any())
                                        {
                                            cDato.DecIng01 = -99999;
                                        }
                                        else
                                        {
                                            foreach (var reg in lst.LstIngresos)
                                            {
                                                if (!dgvAcumulado.Columns[5 + (int)reg.OPERACION_ORDEN].Visible)
                                                {
                                                    dgvAcumulado.Columns[5 + (int)reg.OPERACION_ORDEN].Visible = true;
                                                    dgvAcumulado.Columns[5 + (int)reg.OPERACION_ORDEN].Caption = reg.OPERACION_NOMBRE;
                                                }

                                                decIng[0] += (reg.OPERACION_ORDEN == 1) ? reg.CANTIDAD : 0;
                                                decIng[1] += (reg.OPERACION_ORDEN == 2) ? reg.CANTIDAD : 0;
                                                decIng[2] += (reg.OPERACION_ORDEN == 3) ? reg.CANTIDAD : 0;
                                                decIng[3] += (reg.OPERACION_ORDEN == 4) ? reg.CANTIDAD : 0;
                                                decIng[4] += (reg.OPERACION_ORDEN == 5) ? reg.CANTIDAD : 0;
                                                decIng[5] += (reg.OPERACION_ORDEN == 6) ? reg.CANTIDAD : 0;
                                                decIng[6] += (reg.OPERACION_ORDEN == 7) ? reg.CANTIDAD : 0;
                                                decIng[7] += (reg.OPERACION_ORDEN == 8) ? reg.CANTIDAD : 0;
                                                decIng[8] += (reg.OPERACION_ORDEN == 9) ? reg.CANTIDAD : 0;
                                                decIng[9] += (reg.OPERACION_ORDEN == 10) ? reg.CANTIDAD : 0;
                                                decIng[10] += (reg.OPERACION_ORDEN == 11) ? reg.CANTIDAD : 0;
                                                decIng[11] += (reg.OPERACION_ORDEN == 12) ? reg.CANTIDAD : 0;
                                                decIng[12] += (reg.OPERACION_ORDEN == 13) ? reg.CANTIDAD : 0;
                                                decIng[13] += (reg.OPERACION_ORDEN == 14) ? reg.CANTIDAD : 0;
                                                decIng[14] += (reg.OPERACION_ORDEN == 15) ? reg.CANTIDAD : 0;
                                                decIng[15] += (reg.OPERACION_ORDEN == 16) ? reg.CANTIDAD : 0;
                                                decIng[16] += (reg.OPERACION_ORDEN == 17) ? reg.CANTIDAD : 0;
                                                decIng[17] += (reg.OPERACION_ORDEN == 18) ? reg.CANTIDAD : 0;
                                                decIng[18] += (reg.OPERACION_ORDEN == 19) ? reg.CANTIDAD : 0;
                                                decIng[19] += (reg.OPERACION_ORDEN == 20) ? reg.CANTIDAD : 0;
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
                                                if (!dgvAcumulado.Columns[26 + (int)reg.OPERACION_ORDEN].Visible)
                                                {
                                                    dgvAcumulado.Columns[26 + (int)reg.OPERACION_ORDEN].Visible = true;
                                                    dgvAcumulado.Columns[26 + (int)reg.OPERACION_ORDEN].Caption = reg.OPERACION_NOMBRE;
                                                }

                                                decSal[0] += (reg.OPERACION_ORDEN == 1) ? reg.CANTIDAD : 0;
                                                decSal[1] += (reg.OPERACION_ORDEN == 2) ? reg.CANTIDAD : 0;
                                                decSal[2] += (reg.OPERACION_ORDEN == 3) ? reg.CANTIDAD : 0;
                                                decSal[3] += (reg.OPERACION_ORDEN == 4) ? reg.CANTIDAD : 0;
                                                decSal[4] += (reg.OPERACION_ORDEN == 5) ? reg.CANTIDAD : 0;
                                                decSal[5] += (reg.OPERACION_ORDEN == 6) ? reg.CANTIDAD : 0;
                                                decSal[6] += (reg.OPERACION_ORDEN == 7) ? reg.CANTIDAD : 0;
                                                decSal[7] += (reg.OPERACION_ORDEN == 8) ? reg.CANTIDAD : 0;
                                                decSal[8] += (reg.OPERACION_ORDEN == 9) ? reg.CANTIDAD : 0;
                                                decSal[9] += (reg.OPERACION_ORDEN == 10) ? reg.CANTIDAD : 0;
                                                decSal[10] += (reg.OPERACION_ORDEN == 11) ? reg.CANTIDAD : 0;
                                                decSal[11] += (reg.OPERACION_ORDEN == 12) ? reg.CANTIDAD : 0;
                                                decSal[12] += (reg.OPERACION_ORDEN == 13) ? reg.CANTIDAD : 0;
                                                decSal[13] += (reg.OPERACION_ORDEN == 14) ? reg.CANTIDAD : 0;
                                                decSal[14] += (reg.OPERACION_ORDEN == 15) ? reg.CANTIDAD : 0;
                                                decSal[15] += (reg.OPERACION_ORDEN == 16) ? reg.CANTIDAD : 0;
                                                decSal[16] += (reg.OPERACION_ORDEN == 17) ? reg.CANTIDAD : 0;
                                                decSal[17] += (reg.OPERACION_ORDEN == 18) ? reg.CANTIDAD : 0;
                                                decSal[18] += (reg.OPERACION_ORDEN == 19) ? reg.CANTIDAD : 0;
                                                decSal[19] += (reg.OPERACION_ORDEN == 20) ? reg.CANTIDAD : 0;
                                            }
                                        }

                                        if (lstAcumulado.Count == cont)
                                        {
                                            cDato.DecIng01 = decIng[0];
                                            cDato.DecIng02 = decIng[1];
                                            cDato.DecIng03 = decIng[2];
                                            cDato.DecIng04 = decIng[3];
                                            cDato.DecIng05 = decIng[4];
                                            cDato.DecIng06 = decIng[5];
                                            cDato.DecIng07 = decIng[6];
                                            cDato.DecIng08 = decIng[7];
                                            cDato.DecIng09 = decIng[8];
                                            cDato.DecIng10 = decIng[9];
                                            cDato.DecIng11 = decIng[10];
                                            cDato.DecIng12 = decIng[11];
                                            cDato.DecIng13 = decIng[12];
                                            cDato.DecIng14 = decIng[13];
                                            cDato.DecIng15 = decIng[14];
                                            cDato.DecIng16 = decIng[15];
                                            cDato.DecIng17 = decIng[16];
                                            cDato.DecIng18 = decIng[17];
                                            cDato.DecIng19 = decIng[18];
                                            cDato.DecIng20 = decIng[19];

                                            cDato.DecSal01 = decSal[0];
                                            cDato.DecSal02 = decSal[1];
                                            cDato.DecSal03 = decSal[2];
                                            cDato.DecSal04 = decSal[3];
                                            cDato.DecSal05 = decSal[4];
                                            cDato.DecSal06 = decSal[5];
                                            cDato.DecSal07 = decSal[6];
                                            cDato.DecSal08 = decSal[7];
                                            cDato.DecSal09 = decSal[8];
                                            cDato.DecSal10 = decSal[9];
                                            cDato.DecSal11 = decSal[10];
                                            cDato.DecSal12 = decSal[11];
                                            cDato.DecSal13 = decSal[12];
                                            cDato.DecSal14 = decSal[13];
                                            cDato.DecSal15 = decSal[14];
                                            cDato.DecSal16 = decSal[15];
                                            cDato.DecSal17 = decSal[16];
                                            cDato.DecSal18 = decSal[17];
                                            cDato.DecSal19 = decSal[18];
                                            cDato.DecSal20 = decSal[19];

                                            cDato.DecTotalIng = decTotIng;
                                            cDato.DecTotalSal = decTotSal;

                                            lstMovimientos.Add(cDato);
                                        }

                                        cont++;

                                    }

                                    Session[CVariablesSesion.GrillaAcumulado] = lstMovimientos;
                                    dgvAcumulado.DataSource = lstMovimientos;
                                    dgvAcumulado.DataBind();

                                    #endregion
                                }
                            }
                        }
                        else
                        {
                            //todo No implementado.
                        }
                    }
                    else
                    {
                        //todo No implementado
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

        protected void dgvAcumulado_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
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

                    int index = dgvReporte.FocusedRowIndex;
                    if (index >= 0)
                    {
                        int intEstado = Convert.ToInt32(dgvReporte.GetRowValues(index, "ESTADO_MENSUAL").ToString());
                        if (intEstado == 1)
                        {
                            string strJsonReportes = dgvReporte.GetRowValues(index, "JSON_REPORTE_DATOS").ToString();
                            if (!string.IsNullOrEmpty(strJsonReportes))
                            {
                                var objDdjj = JsonSerializer.DeserializeFromString<O_DECLARACION_JURADA_CTY>(strJsonReportes);

                                var lstAdic2 = objDdjj.LstaAdicional2;
                                if (lstAdic2 != null && lstAdic2.Any())
                                {
                                    #region facturas internacionales

                                    lstAdic2 = lstAdic2.Where(x => x.idEstadoTransaccion == 11).ToList();

                                    var lstNuevo = lstAdic2.GroupBy(n => new { n.hidrocarburo, n.unidadMedida }).
                                    Select(group =>
                                    new
                                    {
                                        strHidrocarburo = group.Key.hidrocarburo,
                                        strUMedida = group.Key.unidadMedida,
                                        decImpPr = group.Sum(c => (c.idTipoEmisor == 14) ? c.precioTotal : 0),
                                        decImpAj = group.Sum(c => (c.idTipoEmisor == 15) ? c.precioTotal : 0),
                                        decCantidad = group.Sum(c => c.cantidadHidrocarburo)
                                    }).ToList();

                                    Session[CVariablesSesion.GrillaInternacional] = lstNuevo;
                                    dgvInternacional.DataSource = lstNuevo;
                                    dgvInternacional.DataBind();

                                    #endregion
                                }
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
                //todo No implementado
            }
        }

        protected void dgvInternacional_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
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

        protected void dgvContenedor_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    Session.Remove(CVariablesSesion.GrillaListaAuxiliar);
                    dgvContenedor.DataSource = null;
                    dgvContenedor.DataBind();

                    int index = dgvReporte.FocusedRowIndex;
                    string strJsonReportes = dgvReporte.GetRowValues(index, "JSON_REPORTE_DATOS").ToString();
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

                        Session[CVariablesSesion.GrillaListaAuxiliar] = lstRep;
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

        protected void dgvAmpliacion_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            try
            {
                #region Proceso a Ejecutar

                if (e.VisibleIndex >= 0)
                {
                    e.Row.BackColor = (Convert.ToInt16(dgvAmpliacion.GetRowValues(e.VisibleIndex, "ESTADO").ToString()) == 2) ? Color.FromArgb(240, 152, 152) : (Convert.ToInt16(dgvAmpliacion.GetRowValues(e.VisibleIndex, "ESTADO").ToString()) == 3) ? Color.FromArgb(235, 198, 155) : Color.FromArgb(229, 253, 212);
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

        protected void pucAmpliacion_WindowCallback(object source, PopupWindowCallbackArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Codigo a controlar

                    int index = dgvReporte.FocusedRowIndex;
                    if (index >= 0)
                    {
                        var decEstado = Convert.ToDecimal(txtPamIdConsumidor.Text = dgvReporte.GetRowValues(index, "ESTADO_MENSUAL").ToString());

                        txtPamActividad.Text = tlsOperadores.FocusedNode.GetValue("StrDescPadre").ToString();
                        txtPamDepto.Text = dgvReporte.GetRowValues(index, "DEPARTAMENTO").ToString();
                        txtPamIdConsumidor.Text = dgvReporte.GetRowValues(index, "ID_CONSUMIDOR").ToString();
                        txtPamConsumidor.Text = dgvReporte.GetRowValues(index, "DENOMINACION").ToString();

                        dgvAmpliacion_CustomCallback(null, null);

                        if (decEstado == 1)
                        {
                            pnlRegistraAmp.Visible = false;
                            dgvAmpliacion.Columns[0].Visible = false;
                        }
                        else
                        {
                            var lstParam = Session[CVariablesSesion.OctanoParametrica] as List<O_LISTA_OCTANO_PAR_CTY>;
                            var datNuevaFecha = DateTime.Now.AddDays(1);

                            if (lstParam != null)
                            {
                                if (Convert.ToInt32(lstParam[3].VALOR_FIN) == 0)
                                {
                                    //dias habiles
                                    int intIdDepto = 0;
                                    var lstEntidad = Session[CVariablesSesion.ObtenerEntidades] as List<O_OBTIENE_ENTIDADES_USR_CTY>;
                                    if (lstEntidad != null)
                                    {
                                        lstEntidad = lstEntidad.Where(x => x.DEPARTAMENTO.Trim() == txtPamDepto.Text).ToList();
                                        intIdDepto = (lstEntidad.Any())?Convert.ToInt32(lstEntidad.FirstOrDefault().ID_DEPARTAMENTO.ToString()):0;
                                    }
                                    var strMensajeError = "";
                                    var servicio = LocalizadorProxy.ServicioParametricas();
                                    var datosFecha = servicio.ObtieneFechaLimite(
                                        CFechas.ConvierteDateTimeLong(DateTime.Now),
                                        19000101000000,
                                        Convert.ToInt32(lstParam[2].VALOR_FIN), //dias
                                        intIdDepto, //id_depto
                                        null,
                                        ref strMensajeError);
                                    if (datosFecha != null)
                                    {
                                        datNuevaFecha = DateTime.Parse(datosFecha[0].FECHA_FINALIZACION);
                                    }
                                    else
                                    {
                                        //todo No implementado
                                    }
                                }
                                else
                                {
                                    //dias calendario
                                    datNuevaFecha = DateTime.Now.AddDays(Convert.ToInt32(lstParam[2].VALOR_FIN));
                                }
                            }
                            else
                            {
                                //todo No implementado
                            }
                            cmbPamMes.Value = cmbMes.Value;
                            cmbPamGestion.Value = cmbGestion.Value;
                            detPamFechaIni.Value = DateTime.Now;
                            detPamFechaFin.Value = datNuevaFecha;
                            dgvAmpliacion.Columns[0].Visible = true;
                        }
                    }
                    else
                    {
                        //todo No implementado
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

        protected void dgvAmpliacion_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Codigo a controlar

                    dgvAmpliacion.DataSource = null;
                    dgvAmpliacion.DataBind();
                    Session.Remove(CVariablesSesion.GrillaAmpliacion);

                    int index = dgvReporte.FocusedRowIndex;
                    if (index >= 0)
                    {
                        var objAmpliacion = _client.Post<EResultadoAutCons>(CAppSettings.svc_listar_autorizacion_consumidor,
                        new
                        {
                            strCredencial = CParametrosHydro.strCredencialFuncionario,
                            decFiltro = 1,
                            strParametro1 = txtPamIdConsumidor.Text,
                            strParametro2 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
                            strParametro3 = cmbGestion.Value + cmbMes.Value.ToString().PadLeft(2, '0') + "01000000",
                            strParametro4 = cmbGestion.Value + cmbMes.Value.ToString().PadLeft(2, '0') + DateTime.DaysInMonth((int)cmbGestion.Value, (int)cmbMes.Value) + "235959",
                            strParametro5 = "1"
                        });
                        if (objAmpliacion.IntCodigo == 1)
                        {
                            objAmpliacion.OResultado.ForEach(a => a.strEstado = (a.ESTADO == 1) ? "ACT." : (a.ESTADO == 2) ? "ANUL." : "VENC.");
                            objAmpliacion.OResultado.ForEach(a => a.strPeriodo = a.FECHA_INICIO.ToString().Substring(4, 2) + "/" + a.FECHA_INICIO.ToString().Substring(0, 4));
                            objAmpliacion.OResultado.ForEach(a => a.strFechaRegistro = a.APP_FECHA_REGISTRO.ToString().Substring(6, 2) + "/" + a.APP_FECHA_REGISTRO.ToString().Substring(4, 2) + "/" + a.APP_FECHA_REGISTRO.ToString().Substring(0, 4) + " " + a.APP_FECHA_REGISTRO.ToString().Substring(8, 2) + ":" + a.APP_FECHA_REGISTRO.ToString().Substring(10, 2) + ":" + a.APP_FECHA_REGISTRO.ToString().Substring(12, 2));
                            objAmpliacion.OResultado.ForEach(a => a.strFechaActIni = a.FECHA_INICIO_ACT.ToString().Substring(6, 2) + "/" + a.FECHA_INICIO_ACT.ToString().Substring(4, 2) + "/" + a.FECHA_INICIO_ACT.ToString().Substring(0, 4));
                            objAmpliacion.OResultado.ForEach(a => a.strFechaActFin = a.FECHA_FIN_ACT.ToString().Substring(6, 2) + "/" + a.FECHA_FIN_ACT.ToString().Substring(4, 2) + "/" + a.FECHA_FIN_ACT.ToString().Substring(0, 4));
                            objAmpliacion.OResultado = objAmpliacion.OResultado.OrderBy(x => x.APP_FECHA_REGISTRO).ToList();
                            dgvAmpliacion.DataSource = objAmpliacion.OResultado;
                            dgvAmpliacion.DataBind();
                            Session[CVariablesSesion.GrillaAmpliacion] = objAmpliacion.OResultado;
                        }
                        else if (objAmpliacion.IntCodigo != -1)
                        {
                            pnlRegistraAmp.Visible = false;
                            MImprimirMensaje(objAmpliacion.StrMensaje, EnumTiposMensajeAlerta.Error);
                        }
                        else
                        {
                            //todo No implementado
                        }
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
                //todo No implementado
            }
        }

        protected void dgvAmpliacion_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    int index = dgvAmpliacion.FocusedRowIndex;
                    long lngidAutCons = Convert.ToInt64(dgvAmpliacion.GetRowValues(index, "ID_AUTORIZACION_CONSUMIDOR"));
                    if (lngidAutCons > 0)
                    {
                        if (e.ButtonID == "btnAnular" && Convert.ToInt64(dgvAmpliacion.GetRowValues(index, "ESTADO")) == 1)
                        {
                            var datFechaActual = DateTime.Now;
                            var resultado = _client.Post<EResultado>(CAppSettings.svc_actualiza_estado_aut_con,
                            new
                            {
                                strCredencial = CParametrosHydro.strCredencialFuncionario,
                                decIdAutorizacionConsumidor = lngidAutCons,
                                decEstado = 2,
                                strObservaciones = "",
                                decAppFechaRegistro = Convert.ToDecimal(datFechaActual.Year + datFechaActual.Month.ToString().PadLeft(2, '0') + datFechaActual.Day.ToString().PadLeft(2, '0') + datFechaActual.Hour.ToString().PadLeft(2, '0') + datFechaActual.Minute.ToString().PadLeft(2, '0') + datFechaActual.Second.ToString().PadLeft(2, '0')),
                                decAppIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario])
                            });
                            if (resultado.intCodigo == 1)
                            {
                                dgvAmpliacion_CustomCallback(null, null);
                            }
                            else
                            {
                                MImprimirMensaje(resultado.strMensaje, EnumTiposMensajeAlerta.Error);
                            }
                        }
                        else
                        {
                            //todo No implementado.
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

        protected void btnPamAgregar_Click(object sender, EventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Codigo a controlar

                    string strMensaje = string.Empty;
                    if (MValidaAmpliacion(ref strMensaje))
                    {
                        var datFechaActual = DateTime.Now;
                        var objResultado = _client.Post<EResultado>(CAppSettings.svc_registra_autorizacion_cons,
                        new
                        {
                            strCredencial = CParametrosHydro.strCredencialFuncionario,
                            decFechaInicio = Convert.ToDecimal(cmbPamGestion.Value + cmbPamMes.Value.ToString().PadLeft(2, '0') + "01000000"),
                            decFechaFin = Convert.ToDecimal(datFechaActual.Date.Year + datFechaActual.Date.Month.ToString().PadLeft(2, '0') + DateTime.DaysInMonth(datFechaActual.Date.Year, datFechaActual.Date.Month) + "235959"),
                            decEstado = 1,
                            strObservaciones = txtPamObservacion.Text,
                            strBinarioEstado = "00000000000000000000",
                            decIdConsumidor = txtPamIdConsumidor.Text,
                            decTipoAutorizacion = 1,
                            decIdTipoActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()),
                            decFechaInicioAct = Convert.ToDecimal(detPamFechaIni.Date.Year + detPamFechaIni.Date.Month.ToString().PadLeft(2, '0') + detPamFechaIni.Date.Day.ToString().PadLeft(2, '0') + "000000"),
                            decFechaFinAct = Convert.ToDecimal(detPamFechaFin.Date.Year + detPamFechaFin.Date.Month.ToString().PadLeft(2, '0') + detPamFechaFin.Date.Day.ToString().PadLeft(2, '0') + "235959"),
                            decAppUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]),
                            decAppFechaRegistro = Convert.ToDecimal(datFechaActual.Year + datFechaActual.Month.ToString().PadLeft(2, '0') + datFechaActual.Day.ToString().PadLeft(2, '0') + datFechaActual.Hour.ToString().PadLeft(2, '0') + datFechaActual.Minute.ToString().PadLeft(2, '0') + datFechaActual.Second.ToString().PadLeft(2, '0'))
                        });
                        if (objResultado.intCodigo == 1)
                        {
                            var repAmp = new repAmpliacion();
                            //repAmp.xtcConsumidor.Text = txtPinConsumidor.Text;
                            repAmp.xtcFechaRegistro.Text = datFechaActual.ToLongDateString() + " " + datFechaActual.ToLongTimeString();
                            repAmp.xtcUsuario.Text = Session[CVariablesSesion.DatosUsuario].ToString();
                            repAmp.xtcApertutaMes.Text = cmbPamMes.Text;
                            repAmp.xtcApertutaGestion.Text = cmbPamGestion.Text;
                            repAmp.xtcHabilitaDesde.Text = detPamFechaIni.Date.ToShortDateString();
                            repAmp.xtcHabilitaHasta.Text = detPamFechaFin.Date.ToShortDateString();
                            repAmp.xtcObservaciones.Text = txtPamObservacion.Text;

                            Session[CVariablesSesion.TipoReporte] = EnumTipoReporte.Ampliacion;
                            Session[CVariablesSesion.ReportePrincipal] = repAmp;
                            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( 'WFReporte.aspx', '');", true);


                            pucAmpliacion.ShowOnPageLoad = false;
                            pnlDatos_Callback(null, null);
                        }
                        else
                        {
                            MImprimirMensaje(objResultado.strMensaje, EnumTiposMensajeAlerta.Error);
                        }
                    }
                    else
                    {
                        MImprimirMensaje(strMensaje, EnumTiposMensajeAlerta.Error);
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
                //todo No implementado
            }
        }

        protected void pucIntimacion_WindowCallback(object source, PopupWindowCallbackArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Codigo a controlar

                    int index = dgvReporte.FocusedRowIndex;
                    if (index >= 0)
                    {
                        var decEstado = Convert.ToDecimal(txtPamIdConsumidor.Text = dgvReporte.GetRowValues(index, "ESTADO_MENSUAL").ToString());

                        txtPinActividad.Text = tlsOperadores.FocusedNode.GetValue("StrDescPadre").ToString();
                        txtPinDepto.Text = dgvReporte.GetRowValues(index, "DEPARTAMENTO").ToString();
                        txtPinIdConsumidor.Text = dgvReporte.GetRowValues(index, "ID_CONSUMIDOR").ToString();
                        txtPinConsumidor.Text = dgvReporte.GetRowValues(index, "DENOMINACION").ToString();

                        dgvIntimacion_CustomCallback(null, null);

                        if (decEstado != 3)
                        {
                            pnlIntimacion.Visible = false;
                        }
                        else
                        {
                            txtPinPeriodo.Text = cmbMes.Text + "/" + cmbGestion.Text;
                            txtPinDescripcion.Text = "Incumplimiento a la obligación mensual - Registro de Declaración Jurada";
                            detPinFechaNot.Date = DateTime.Now;
                            txtPinCb.Text = "";
                            txtPinNotificante.Text = "";
                            txtPinNotificado.Text = "";
                            detPinFechaInicio.Date = DateTime.Now.Date;
                            spnPinDiasPlazo.Number = 2;
                            txtPinObservacion.Text = "";
                        }
                    }
                    else
                    {
                        //todo No implementado
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
                //todo No implementado
            }
        }

        protected void dgvIntimacion_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Codigo a controlar

                    dgvIntimacion.DataSource = null;
                    dgvIntimacion.DataBind();
                    Session.Remove(CVariablesSesion.GrillaIntimacion);

                    var objIntimacion = _client.Post<EResultadoAutCons>(CAppSettings.svc_listar_autorizacion_consumidor,
                    new
                    {
                        strCredencial = CParametrosHydro.strCredencialFuncionario,
                        decFiltro = 1,
                        strParametro1 = txtPinIdConsumidor.Text,
                        strParametro2 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
                        strParametro3 = cmbGestion.Value + cmbMes.Value.ToString().PadLeft(2, '0') + "01000000",
                        strParametro4 = cmbGestion.Value + cmbMes.Value.ToString().PadLeft(2, '0') + DateTime.DaysInMonth((int)cmbGestion.Value, (int)cmbMes.Value) + "235959",
                        strParametro5 = "2"
                    });
                    if (objIntimacion.IntCodigo == 1)
                    {
                        objIntimacion.OResultado.ForEach(a => a.strFechaRegistro = a.APP_FECHA_REGISTRO.ToString().Substring(6, 2) + "/" + a.APP_FECHA_REGISTRO.ToString().Substring(4, 2) + "/" + a.APP_FECHA_REGISTRO.ToString().Substring(0, 4) + " " + a.APP_FECHA_REGISTRO.ToString().Substring(8, 2) + ":" + a.APP_FECHA_REGISTRO.ToString().Substring(10, 2) + ":" + a.APP_FECHA_REGISTRO.ToString().Substring(12, 2));
                        objIntimacion.OResultado.ForEach(a => a.strFechaActIni = a.FECHA_INICIO_ACT.ToString().Substring(6, 2) + "/" + a.FECHA_INICIO_ACT.ToString().Substring(4, 2) + "/" + a.FECHA_INICIO_ACT.ToString().Substring(0, 4) + " " + a.FECHA_INICIO_ACT.ToString().Substring(8, 2) + ":" + a.FECHA_INICIO_ACT.ToString().Substring(10, 2) + ":" + a.FECHA_INICIO_ACT.ToString().Substring(12, 2));
                        objIntimacion.OResultado = objIntimacion.OResultado.OrderBy(x => x.APP_FECHA_REGISTRO).ToList();

                        dgvIntimacion.DataSource = objIntimacion.OResultado;
                        dgvIntimacion.DataBind();
                        Session[CVariablesSesion.GrillaIntimacion] = objIntimacion.OResultado;
                    }
                    else if (objIntimacion.IntCodigo != -1)
                    {
                        pnlIntimacion.Visible = false;
                        MImprimirMensaje(objIntimacion.StrMensaje, EnumTiposMensajeAlerta.Error);
                    }
                    else
                    {
                        //todo No implementado
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
                //todo No implementado
            }
        }

        protected void dgvIntimacion_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    var index = dgvIntimacion.FocusedRowIndex;
                    var lngidAutCons = Convert.ToInt64(dgvIntimacion.GetRowValues(index, "ID_AUTORIZACION_CONSUMIDOR"));
                    if (lngidAutCons > 0)
                    {
                        if (e.ButtonID == "btnReimprimir")
                        {
                            string strJson = dgvIntimacion.GetRowValues(index, "OBSERVACIONES").ToString();
                            var objIntimacion = JsonSerializer.DeserializeFromString<CRegistroIntimacion>(strJson);

                            var repInt = new repIntimacion();
                            repInt.xtcConsumidor.Text = txtPinConsumidor.Text;
                            repInt.xtcPeriodo.Text = objIntimacion.strPeriodo;
                            repInt.xtcFechaRegistro.Text = dgvIntimacion.GetRowValues(index, "ID_AUTORIZACION_CONSUMIDOR").ToString();
                            repInt.xtcUsuario.Text = dgvIntimacion.GetRowValues(index, "APP_USUARIO").ToString();
                            repInt.xtcCodBarra.Text = objIntimacion.decNotificacion.ToString();
                            repInt.xtcNotificacion.Text = objIntimacion.strCodBarra;
                            repInt.xtcNotificante.Text = objIntimacion.strNotificante;
                            repInt.xtcNotificada.Text = objIntimacion.strNotificado;
                            repInt.xtcFechaInicio.Text = objIntimacion.decFechaInicio.ToString();
                            repInt.xtcDiasPlazo.Text = objIntimacion.decDiasPlazo.ToString();
                            repInt.xtcObservaciones.Text = objIntimacion.strObservacion;

                            Session[CVariablesSesion.TipoReporte] = EnumTipoReporte.Intimacion;
                            Session[CVariablesSesion.ReportePrincipal] = repInt;
                            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( 'WFReporte.aspx', '');", true);
                        }
                        else
                        {
                            //todo No implementado.
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

        protected void dgvIntimacion_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
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

        protected void btnPinAgregar_Click(object sender, EventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Codigo a controlar

                    string strMensaje = string.Empty;
                    if (MValidaIntimacion(ref strMensaje))
                    {
                        var objIntimacion = new CRegistroIntimacion();
                        objIntimacion.strPeriodo = txtPinPeriodo.Text;
                        objIntimacion.strDescripcion = txtPinDescripcion.Text;
                        objIntimacion.decNotificacion = Convert.ToDecimal(detPinFechaNot.Date.Year + detPinFechaNot.Date.Month.ToString().PadLeft(2, '0') + detPinFechaNot.Date.Day.ToString().PadLeft(2, '0') + detPinFechaNot.Date.Hour.ToString().PadLeft(2, '0') + detPinFechaNot.Date.Minute.ToString().PadLeft(2, '0') + detPinFechaNot.Date.Second.ToString().PadLeft(2, '0'));
                        objIntimacion.strCodBarra = txtPinCb.Text;
                        objIntimacion.strNotificante = txtPinNotificante.Text;
                        objIntimacion.strNotificado = txtPinNotificado.Text;
                        objIntimacion.decFechaInicio = Convert.ToDecimal(detPinFechaInicio.Date.Year + detPinFechaInicio.Date.Month.ToString().PadLeft(2, '0') + detPinFechaInicio.Date.Day.ToString().PadLeft(2, '0') + "000000");
                        var decUltimodia = detPinFechaInicio.Date.Day + spnPinDiasPlazo.Number;
                        objIntimacion.decFechaFin = Convert.ToDecimal(detPinFechaInicio.Date.Year + detPinFechaInicio.Date.Month.ToString().PadLeft(2, '0') + decUltimodia.ToString().PadLeft(2, '0') + "235959");
                        objIntimacion.decDiasPlazo = spnPinDiasPlazo.Number;
                        objIntimacion.strObservacion = txtPinObservacion.Text;

                        var objJson = JsonSerializer.SerializeToString(objIntimacion);

                        var datFechaActual = DateTime.Now;
                        var objResultado = _client.Post<EResultado>(CAppSettings.svc_registra_autorizacion_cons,
                        new
                        {
                            strCredencial = CParametrosHydro.strCredencialFuncionario,
                            decFechaInicio = Convert.ToDecimal(cmbGestion.Value+cmbMes.Value.ToString().PadLeft(2,'0')+"01000000"),
                            decFechaFin = Convert.ToDecimal(cmbGestion.Value+cmbMes.Value.ToString().PadLeft(2,'0')+DateTime.DaysInMonth((int)cmbGestion.Value,(int)cmbMes.Value)+"235959"),
                            decEstado = 1,
                            strObservaciones = objJson,
                            strBinarioEstado = "00000000000000000000",
                            decIdConsumidor = Convert.ToDecimal(txtPinIdConsumidor.Text),
                            decTipoAutorizacion = 2,
                            decIdTipoActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()),
                            decFechaInicioAct = objIntimacion.decNotificacion,
                            decFechaFinAct = objIntimacion.strCodBarra,
                            decAppUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]),
                            decAppFechaRegistro = Convert.ToDecimal(datFechaActual.Year + datFechaActual.Month.ToString().PadLeft(2, '0') + datFechaActual.Day.ToString().PadLeft(2, '0') + datFechaActual.Hour.ToString().PadLeft(2, '0') + datFechaActual.Minute.ToString().PadLeft(2, '0') + datFechaActual.Second.ToString().PadLeft(2, '0'))
                        });
                        if (objResultado.intCodigo == 1)
                        {
                            var repInt = new repIntimacion();
                            repInt.xtcConsumidor.Text = txtPinConsumidor.Text;
                            repInt.xtcPeriodo.Text = objIntimacion.strPeriodo;
                            repInt.xtcFechaRegistro.Text = datFechaActual.ToLongDateString() + " " +datFechaActual.ToLongTimeString();
                            repInt.xtcUsuario.Text = Session[CVariablesSesion.DatosUsuario].ToString();
                            repInt.xtcCodBarra.Text = detPinFechaNot.Date.ToShortDateString() + " " + detPinFechaNot.Date.ToShortTimeString();
                            repInt.xtcNotificacion.Text = objIntimacion.strCodBarra;
                            repInt.xtcNotificante.Text = objIntimacion.strNotificante;
                            repInt.xtcNotificada.Text = objIntimacion.strNotificado;
                            repInt.xtcFechaInicio.Text = detPinFechaInicio.Date.ToShortDateString();
                            repInt.xtcDiasPlazo.Text = objIntimacion.decDiasPlazo.ToString();
                            repInt.xtcObservaciones.Text = objIntimacion.strObservacion;

                            Session[CVariablesSesion.TipoReporte] = EnumTipoReporte.Intimacion;
                            Session[CVariablesSesion.ReportePrincipal] = repInt;
                            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( 'WFReporte.aspx', '');", true);

                            pucIntimacion.ShowOnPageLoad = false;
                            pnlDatos_Callback(null, null);
                        }
                        else
                        {
                            MImprimirMensaje(objResultado.strMensaje, EnumTiposMensajeAlerta.Error);
                        }
                    }
                    else
                    {
                        MImprimirMensaje(strMensaje, EnumTiposMensajeAlerta.Error);
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
                //todo No implementado
            }
        }

        protected void pucHistorial_WindowCallback(object source, PopupWindowCallbackArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Codigo a controlar

                    int index = dgvReporte.FocusedRowIndex;
                    if (index >= 0)
                    {
                        txtPhiPeriodo.Text = cmbMes.Text + "/" + cmbGestion.Text;
                        txtPhiIdConsumidor.Text = dgvReporte.GetRowValues(index, "ID_CONSUMIDOR").ToString();
                        txtPhiConsumidor.Text = dgvReporte.GetRowValues(index, "DENOMINACION").ToString();
                        dgvHistorial_CustomCallback(null, null);
                    }
                    else
                    {
                        //todo No implementado
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
                //todo No implementado
            }
        }

        protected void dgvHistorial_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
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

        protected void dgvHistorial_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Codigo a controlar

                    dgvHistorial.DataSource = null;
                    dgvHistorial.DataBind();
                    Session.Remove(CVariablesSesion.GrillaHistorial);

                    var objHistorial = _client.Post<EResultadoHistorialCons>(CAppSettings.svc_listar_bitacora_consumidor,
                    new
                    {
                        strCredencial = CParametrosHydro.strCredencialFuncionario,
                        decFiltro = 2,
                        strParametro1 = txtPhiIdConsumidor.Text,
                        strParametro2 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
                        strParametro3 = cmbGestion.Value + cmbMes.Value.ToString().PadLeft(2, '0') + "01000000",
                        strParametro4 = cmbGestion.Value + cmbMes.Value.ToString().PadLeft(2, '0') + DateTime.DaysInMonth((int)cmbGestion.Value, (int)cmbMes.Value) + "235959"
                    });
                    if (objHistorial.IntCodigo == 1)
                    {
                        objHistorial.OResultado.ForEach(a => a.strFechaRegistro = a.APP_FECHA_REGISTRO.ToString().Substring(6, 2) + "/" + a.APP_FECHA_REGISTRO.ToString().Substring(4, 2) + "/" + a.APP_FECHA_REGISTRO.ToString().Substring(0, 4) + " " + a.APP_FECHA_REGISTRO.ToString().Substring(8, 2) + ":" + a.APP_FECHA_REGISTRO.ToString().Substring(10, 2) + ":" + a.APP_FECHA_REGISTRO.ToString().Substring(12, 2));
                        objHistorial.OResultado.ForEach(a => a.strFechaInicio = a.FECHA_INICIO.ToString().Substring(4, 2) + "/" + a.FECHA_INICIO.ToString().Substring(0, 4));
                        objHistorial.OResultado.ForEach(a => a.strEstado = (a.ESTADO == 1) ? "ACTIVO" : (a.ESTADO == 2) ? "ANULADO" : (a.ESTADO == 4) ? "ARCH.ADJ." : (a.TIPO_BITACORA.Contains("AMPLI") && a.ESTADO == 3) ? "VENCIDO" : "RECEPCIONADO");
                        objHistorial.OResultado = objHistorial.OResultado.OrderBy(x => x.APP_FECHA_REGISTRO).ToList();

                        dgvHistorial.DataSource = objHistorial.OResultado;
                        dgvHistorial.DataBind();
                        Session[CVariablesSesion.GrillaHistorial] = objHistorial.OResultado;
                    }
                    else if (objHistorial.IntCodigo != -1)
                    {
                        MImprimirMensaje(objHistorial.StrMensaje, EnumTiposMensajeAlerta.Error);
                    }
                    else
                    {
                        //todo No implementado
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
                //todo No implementado
            }
        }

        protected void btnEmitir_Click(object sender, EventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    if ((int)cmbTipoExport.Value == 1)
                    {
                        dgeExportar.GridViewID = "dgvReporte";
                        dgeExportar.WriteXlsxToResponse();
                    }
                    else
                    {
                        var xreReporte = new repCumplimientoMes();
                        xreReporte.xtcFecha.Text = DateTime.Now.ToShortDateString();
                        xreReporte.xtcHora.Text = DateTime.Now.ToLongTimeString();
                        xreReporte.xtcUsuario.Text = CParametrosHydro.StrUsuarioLogin;

                        xreReporte.xtcActividad.Text = tlsOperadores.FocusedNode.GetValue("StrDescPadre").ToString();
                        xreReporte.xtcPeriodo.Text = cmbMes.Text + "/" + cmbGestion.Text;
                        xreReporte.xtcTipo.Text = ((int)rbtTipo.Value == 1) ? "Cumplimiento (%)" : "Detallado por Estados";

                        var lstRep = Session[CVariablesSesion.GrillaAuxiliar] as List<O_LISTA_REPORTE_MENSUAL_CTY>;
                        lstRep = lstRep.OrderBy(x => x.ESTADO_MENSUAL).ThenBy(x => x.DEPARTAMENTO).ToList();
                        xreReporte.DataSource = lstRep;

                        Session[CVariablesSesion.ReportePrincipal] = xreReporte;
                        Session[CVariablesSesion.TipoReporte] = EnumTipoReporte.RepCumplimientoMes;
                        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( 'WFReporte.aspx', '');", true);
                    }

                    #endregion
                }
                catch (Exception exp)
                {
                    #region RegistraError

                    MImprimirMensaje(exp.Message, EnumTiposMensajeAlerta.Error);

                    #endregion
                }
            }
            else
            {
                //todo no implementado
            }
        }

        protected void pnlReporte_Callback(object sender, CallbackEventArgsBase e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Codigo a controlar

                    dgvReporte.DataSource = null;
                    dgvReporte.DataBind();

                    Session.Remove(CVariablesSesion.GrillaAuxiliar);

                    var lstEntidades = Session[CVariablesSesion.GrillaCabecera] as List<O_LISTA_REPORTE_MENSUAL_CTY>;
                    if (e.Parameter.Trim().Length > 0)
                    {
                        lstEntidades = lstEntidades.Where(x => x.DEPARTAMENTO == e.Parameter.Trim()).ToList();
                        dgvReporte.Columns[3].Visible = false;
                    }
                    else
                    {
                        dgvReporte.Columns[3].Visible = true;
                    }

                    Session[CVariablesSesion.GrillaAuxiliar] = lstEntidades;
                    dgvReporte.DataSource = lstEntidades;
                    dgvReporte.DataBind();

                    #endregion
                }
                catch (Exception exp)
                {
                    #region RegistraError
                    MLimpiaGrilla();
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

        #endregion

        #region Metodos y funciones

        /// <summary>
        /// METODO. MCargaMapa
        /// </summary>
        /// <param name="sender"></param>
        private void MCargaMapa(object sender)
        {
            var lstEntidad = new List<O_LISTA_REPORTE_MENSUAL_CTY>();
            lstEntidad = (List<O_LISTA_REPORTE_MENSUAL_CTY>)Session[CVariablesSesion.GrillaCabecera];

            if ((int)rbtTipo.Value == 1)
            {
                lstEntidad.ForEach(a => a.ESTADO_MENSUAL = (a.ESTADO_MENSUAL == 4 || a.ESTADO_MENSUAL == 5) ? 3 : a.ESTADO_MENSUAL);
                lstEntidad.ForEach(a => a.MENSAJE_MENSUAL = (a.ESTADO_MENSUAL == 3) ? "PLAZO VENCIDO" : a.MENSAJE_MENSUAL);
            }
            else
            {
                //todo No implementado
            }

            double[] latitud = new double[lstEntidad.Count + 1];
            double[] longitud = new double[lstEntidad.Count + 1];
            string[] strDescripcion = new string[lstEntidad.Count + 1];
            decimal[] intEstado = new decimal[lstEntidad.Count + 1];

            double douCentroLat = 0;
            double douCentroLon = 0;
            int intContador = 0;
            for (int i = 0; i < lstEntidad.Count; i++)
            {
                latitud[i + 1] = (lstEntidad[i].LATITUD == null) ? 0 : (double)lstEntidad[i].LATITUD;
                longitud[i + 1] = (lstEntidad[i].LONGITUD == null) ? 0 : (double)lstEntidad[i].LONGITUD;
                intEstado[i + 1] = lstEntidad[i].ESTADO_MENSUAL;

                strDescripcion[i + 1] = "<div id=\"content\">" + "<h2>" + lstEntidad[i].DENOMINACION + "</h2>" + "<div id=\"bodyContent\">" +
                "<p><b>Ubicación : </b>" + lstEntidad[i].DEPARTAMENTO + ".</p>" +
                "<p><b>Dirección : </b>" + lstEntidad[i].DIRECCION + ".</p>" +
                "<p><b>NIT : </b>" + lstEntidad[i].NIT + ".</p>" +
                "<p><b>Estado : </b>" + lstEntidad[i].MENSAJE_MENSUAL + ".</p>" + "</div>" +
                "</div>";

                douCentroLat += (lstEntidad[i].LATITUD == null) ? 0 : (double)lstEntidad[i].LATITUD;
                douCentroLon += (lstEntidad[i].LONGITUD == null) ? 0 : (double)lstEntidad[i].LONGITUD;

                if ((double)lstEntidad[i].LATITUD != 0 && (double)lstEntidad[i].LONGITUD != 0)
                {
                    intContador++;
                }
            }
            intContador = ((intContador == 0) ? 1 : intContador);
            douCentroLat = douCentroLat / intContador;
            douCentroLon = douCentroLon / intContador;

            latitud[0] = douCentroLat;
            longitud[0] = douCentroLon;

            var panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cp_latitud"] = latitud;
            panel.JSProperties["cp_longitud"] = longitud;
            panel.JSProperties["cp_descripcion"] = strDescripcion;
            panel.JSProperties["cp_idestado"] = intEstado;
        }

        /// <summary>
        /// METODO. MCargaGrafico
        /// </summary>
        private void MCargaGrafico()
        {
            wccCumplimiento.Series.Clear();

            var lstEntidades = Session[CVariablesSesion.GrillaCabecera] as List<O_LISTA_REPORTE_MENSUAL_CTY>;
            ViewType vtpTipoGrafico;

            if ((int)rbtTipo.Value == 1)
            {
                lstEntidades.ForEach(a => a.ESTADO_MENSUAL = (a.ESTADO_MENSUAL == 4 || a.ESTADO_MENSUAL == 5) ? 3 : a.ESTADO_MENSUAL);
                lstEntidades.ForEach(a => a.MENSAJE_MENSUAL = (a.ESTADO_MENSUAL == 3) ? "PLAZO VENCIDO" : a.MENSAJE_MENSUAL);
                vtpTipoGrafico = ViewType.FullStackedBar;
            }
            else
            {
                vtpTipoGrafico = ViewType.StackedBar;
            }
           
           var lstNuevo = lstEntidades.GroupBy(n => new { n.DEPARTAMENTO, n.ESTADO_MENSUAL, n.MENSAJE_MENSUAL }).
            Select(group =>
            new
            {
                strDepartamento = group.Key.DEPARTAMENTO,
                intEstado = group.Key.ESTADO_MENSUAL,
                strEstado = group.Key.MENSAJE_MENSUAL,
                Cantidad = group.Count()
            }).ToList();
            lstNuevo = lstNuevo.OrderBy(x => x.intEstado).ThenBy(x => x.strDepartamento).ToList();

            var series1 = new Series("Declarado", vtpTipoGrafico);
            var series2 = new Series("En Plazo", vtpTipoGrafico);
            var series3 = new Series("Plazo Vencido", vtpTipoGrafico);
            var series4 = new Series("Ampliaciones", vtpTipoGrafico);
            var series5 = new Series("Intimacion", vtpTipoGrafico);
            var series6 = new Series("No Habilitado", vtpTipoGrafico);

            ((BarSeriesView)series1.View).FillStyle.FillMode = FillMode.Solid;
            ((BarSeriesView)series2.View).FillStyle.FillMode = FillMode.Solid;
            ((BarSeriesView)series3.View).FillStyle.FillMode = FillMode.Solid;
            ((BarSeriesView)series4.View).FillStyle.FillMode = FillMode.Solid;
            ((BarSeriesView)series5.View).FillStyle.FillMode = FillMode.Solid;
            ((BarSeriesView)series6.View).FillStyle.FillMode = FillMode.Solid;

            foreach (var reg in lstNuevo)
            {
                switch ((int)reg.intEstado)
                {
                    case 1:
                        series1.Points.Add(new SeriesPoint(reg.strDepartamento, reg.Cantidad));
                        break;
                    case 2:
                        series2.Points.Add(new SeriesPoint(reg.strDepartamento, reg.Cantidad));
                        break;
                    case 3:
                        series3.Points.Add(new SeriesPoint(reg.strDepartamento, reg.Cantidad));
                        break;
                    case 4:
                        series4.Points.Add(new SeriesPoint(reg.strDepartamento, reg.Cantidad));
                        break;
                    case 5:
                        series5.Points.Add(new SeriesPoint(reg.strDepartamento, reg.Cantidad));
                        break;
                    case 6:
                        series6.Points.Add(new SeriesPoint(reg.strDepartamento, reg.Cantidad));
                        break;
                }
            }

            if ((int)rbtTipo.Value == 1)
            {
                wccCumplimiento.Series.AddRange(new[] { series1, series2, series3, series6 });
                wccCumplimiento.Series[0].View.Color = Color.LimeGreen;
                wccCumplimiento.Series[1].View.Color = Color.DarkOliveGreen;
                wccCumplimiento.Series[2].View.Color = Color.DarkRed;
                wccCumplimiento.Series[3].View.Color = Color.Gray;
            }
            else
            {
                wccCumplimiento.Series.AddRange(new[] { series1, series2, series3, series4, series5, series6 });
                wccCumplimiento.Series[0].View.Color = Color.LimeGreen;
                wccCumplimiento.Series[1].View.Color = Color.DarkOliveGreen;
                wccCumplimiento.Series[2].View.Color = Color.DarkRed;
                wccCumplimiento.Series[3].View.Color = Color.DarkBlue;
                wccCumplimiento.Series[4].View.Color = Color.DarkOrange;
                wccCumplimiento.Series[5].View.Color = Color.Gray;
            }

            wccCumplimiento.Series[0].ShowInLegend = true;
            wccCumplimiento.Legend.AlignmentVertical = LegendAlignmentVertical.TopOutside;
            wccCumplimiento.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Right;
            
            ((XYDiagram)wccCumplimiento.Diagram).Rotated = true;
            ((XYDiagram)wccCumplimiento.Diagram).AxisX.VisualRange.AutoSideMargins = true;
            ((XYDiagram)wccCumplimiento.Diagram).AxisY.Title.Visible = false;
            ((XYDiagram)wccCumplimiento.Diagram).AxisY.VisualRange.AutoSideMargins = false;
            ((XYDiagram)wccCumplimiento.Diagram).AxisY.GridLines.MinorVisible = true;

        }

        /// <summary>
        /// METODO. MValidaSesion
        /// </summary>
        /// <returns></returns>
        private bool MValidaSesion()
        {
            bool booResultado;
            //borrar
            //Session[CVariablesSesion.IdUsuario] = "1802";
            //Session[CVariablesSesion.DatosUsuario] = "xxx";

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
        /// METODO. MCargaParametricas
        /// </summary>
        private void MCargaParametricas()
        {
            //Obtener perfiles del usuario
            var objPerfil = _clientAdminHydro.Get<EResultadoLista>("/EReporteUsuariosPerfiles/" + CParametrosHydro.strCredencialHydroAdmin + "/" + Convert.ToDecimal(CAppSettings.idAplicacion) + "/" + Session[CVariablesSesion.IdUsuario] + "?format=json");
            if (objPerfil.decCodigo == 1)
            {
                Session[CVariablesSesion.ListaPerfiles] = objPerfil.oResultado;
            }
            else
            {
                Session.Remove(CVariablesSesion.ListaPerfiles);
            }
            Session.Remove(CVariablesSesion.FechaHoraServ);
            var objFechaHora = _client.Post<EResultadoFeHoServ>(CAppSettings.svc_fecha_hora_actual,
            new
            {
                strCredencial = CParametrosHydro.strCredencialFuncionario
            });
            if (objFechaHora.IntCodigo == 1)
            {
                string strAux = objFechaHora.OResultado[0].FECHA_HORA_ACTUAL.ToString();
                string strValor = strAux.Substring(6, 2) + "/" + strAux.Substring(4, 2) + "/" + strAux.Substring(0, 4) + " " + strAux.Substring(8, 2) + ":" + strAux.Substring(10, 2) + ":" + strAux.Substring(12, 2);
                objFechaHora.OResultado[0].strFechaHoraActu = strValor;
                objFechaHora.OResultado[0].datFechaHoraActu = DateTime.Parse(strValor);
                Session[CVariablesSesion.FechaHoraServ] = objFechaHora.OResultado[0];
            }
            else
            {
                //todo No implementado.
            }
        }

        /// <summary>
        /// METODO. MCargaParametricasEsp
        /// </summary>
        private void MCargaParametricasEsp()
        {
            //PARAMETRICAS GENERALES
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
            if (objParametros.IntCodigo == 1)
            {
                Session[CVariablesSesion.OctanoParametrica] = objParametros.OResultado;
            }
            else
            {
                Session.Remove(CVariablesSesion.OctanoParametrica);
            }
        }

        /// <summary>
        /// METODO. MIniciaFechas
        /// </summary>
        private void MIniciaFechas()
        {
            O_FECHA_HORA_ACTUAL_CTY objFeHoServ = Session[CVariablesSesion.FechaHoraServ] as O_FECHA_HORA_ACTUAL_CTY;
            var x = DateTime.Parse("01/" + objFeHoServ.datFechaHoraActu.Date.Month.ToString().PadLeft(2, '0') +"/"+objFeHoServ.datFechaHoraActu.Date.Year).AddDays(-1);

            cmbMes.Value = x.Month;
            cmbGestion.Value = x.Year;
        }

        /// <summary>
        /// METODO. MCargaArbol
        /// </summary>
        private void MCargaArbol()
        {
            //MLimpiaGrilla();

            decimal idUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
            var lista2 = _client.Get<EResultadoEntidad>(CAppSettings.svc_obtener_entidades + "/" + idUsuario + "?format=json");
            if (lista2 != null)
            {
                if (lista2.IntCodigo == 1)
                {
                    List<O_OBTIENE_ENTIDADES_USR_CTY> lstEntidad;
                    lstEntidad = lista2.OResultado;
                    lstEntidad = lstEntidad.OrderBy(x => x.ACTIVIDAD).ThenBy(x => x.DEPARTAMENTO).ThenBy(x => x.DENOMINACION).ToList();

                    int idDe = 0;
                    string idAc = "";
                    int idUnico = 1, idPadre = 0, idUnic1 = 0;
                    int intNivel1 = 0, intNivel2 = 0, intNivel3 = 0;
                    string strNivel1 = "", strNivel2 = "";
                    var lstResultado = new List<CArbol>();
                    foreach (var lst in lstEntidad)
                    {
                        CArbol cDato;
                        if (idDe != lst.ID_ACTIVIDAD)
                        {
                            cDato = new CArbol();
                            cDato.IntId = idUnico;
                            cDato.IntIdPadre = 0;
                            cDato.IntIdEntidad = 0;
                            cDato.IntIdActividad = (int)lst.ID_ACTIVIDAD;
                            cDato.IntIdOperador = 0;
                            cDato.IntIdProyecto = 0;
                            cDato.StrDescripcion = lst.ACTIVIDAD.Trim();
                            cDato.StrDescPadre = lst.ACTIVIDAD.Trim();
                            cDato.LngNit = 0;
                            cDato.StrLicencia = "";
                            cDato.StrRepLegal = lst.REPRESENTANTE_LEGAL;
                            cDato.StrDireccion = "";
                            cDato.StrDepartamento = "";
                            cDato.StrMunicipio = "";
                            cDato.StrLocalidad = "";
                            cDato.StrTelefono = "";
                            cDato.StrAmbito = "";
                            cDato.StrPropietario = "";
                            cDato.StrCiPropietario = "";
                            cDato.StrCiRepresentante = "";
                            cDato.IntAccion = 0;
                            cDato.StrUbicacion = intNivel1.ToString();
                            lstResultado.Add(cDato);
                            idPadre = idUnico;
                            idUnic1 = idUnico;
                            idDe = (int)lst.ID_ACTIVIDAD;
                            idAc = "";
                            idUnico++;
                            strNivel1 = cDato.StrUbicacion;
                            intNivel1++;
                            intNivel2 = 0;
                        }
                        else
                        {
                            //todo No implementado.
                        }
                        if (idAc != lst.DEPARTAMENTO)
                        {
                            cDato = new CArbol();
                            cDato.IntId = idUnico;
                            cDato.IntIdPadre = idUnic1;
                            cDato.IntIdEntidad = 0;
                            cDato.IntIdOperador = 0;
                            cDato.IntIdProyecto = 0;
                            cDato.IntIdActividad = (int)lst.ID_ACTIVIDAD;
                            cDato.StrDescripcion = lst.DEPARTAMENTO;
                            cDato.StrDescPadre = lst.ACTIVIDAD;
                            cDato.LngNit = 0;
                            cDato.StrLicencia = "";
                            cDato.StrRepLegal = "";
                            cDato.StrDireccion = "";
                            cDato.StrDepartamento = lst.DEPARTAMENTO;
                            cDato.StrMunicipio = "";
                            cDato.StrLocalidad = "";
                            cDato.StrTelefono = "";
                            cDato.StrAmbito = "";
                            cDato.StrPropietario = "";
                            cDato.StrCiPropietario = "";
                            cDato.StrCiRepresentante = "";
                            cDato.IntAccion = 0;
                            cDato.StrUbicacion = strNivel1 + intNivel2;
                            lstResultado.Add(cDato);
                            idPadre = idUnico;
                            idAc = lst.DEPARTAMENTO;
                            idUnico++;
                            strNivel2 = cDato.StrUbicacion;
                            intNivel2++;
                            intNivel3 = 0;
                        }
                        else
                        {
                            //todo No implementado.
                        }
                        cDato = new CArbol();
                        cDato.IntId = idUnico;
                        cDato.IntIdPadre = idPadre;
                        cDato.IntIdEntidad = (int)lst.ID_ENTIDAD_PADRE;
                        cDato.IntIdOperador = (int)lst.ID_CONSUMIDOR;
                        cDato.IntIdProyecto = (int)lst.ID_ENTIDAD;
                        cDato.IntIdActividad = (int)lst.ID_ACTIVIDAD;
                        cDato.StrDescripcion = lst.DENOMINACION;
                        cDato.StrDescPadre = lst.DENOMINACION;
                        cDato.LngNit = Convert.ToInt64(lst.NIT);
                        cDato.StrActividad = lst.ACTIVIDAD;
                        cDato.StrLicencia = lst.NRO_LICENCIA;
                        cDato.StrRepLegal = lst.REPRESENTANTE_LEGAL;
                        cDato.StrDireccion = lst.DIRECCION;
                        cDato.StrDepartamento = lst.DEPARTAMENTO;
                        cDato.StrMunicipio = lst.MUNICIPIO;
                        cDato.StrLocalidad = lst.LOCALIDAD;
                        cDato.StrTelefono = lst.TELEFONOS;
                        cDato.StrAmbito = lst.AMBITO_OPERACION;
                        cDato.StrPropietario = lst.NOMBRES_PROPIETARIO;
                        cDato.StrCiPropietario = lst.CI_PROPIETARIO;
                        cDato.StrCiRepresentante = lst.CI_REPRESENTANTE;
                        cDato.IntAccion = 1;
                        cDato.StrUbicacion = strNivel2 + intNivel3;
                        lstResultado.Add(cDato);
                        idUnico++;
                        intNivel3++;
                    }
                    Session[CVariablesSesion.EstructuraArbol] = lstResultado;
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



            /////
            //decimal idUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
            //var lista2 = client.Get<EResultadoEntidad>(CAppSettings.svc_obtener_entidades + "/" + idUsuario + "?format=json");
            //if (lista2 != null)
            //{
            //    if (lista2.IntCodigo == 1)
            //    {
            //        var lstEntidad = new List<O_OBTIENE_ENTIDADES_USR_CTY>();
            //        lstEntidad = lista2.OResultado;
            //        Session[CVariablesSesion.ObtenerEntidades] = lstEntidad;
            //        lstEntidad = lstEntidad.OrderBy(x => x.ACTIVIDAD).ThenBy(x => x.DEPARTAMENTO).ThenBy(x => x.DENOMINACION).ToList();

            //        int idDe = 0;
            //        int idUnico = 1;
            //        var lstResultado = new List<CArbol>();
            //        foreach (var lst in lstEntidad)
            //        {
            //            var cDato = new CArbol();
            //            if (idDe != lst.ID_ACTIVIDAD)
            //            {
            //                cDato = new CArbol();
            //                cDato.IntId = idUnico;
            //                cDato.IntIdPadre = 0;
            //                cDato.IntIdEntidad = 0;
            //                cDato.IntIdActividad = (int)lst.ID_ACTIVIDAD;
            //                cDato.IntIdOperador = 0;
            //                cDato.StrDescripcion = lst.ACTIVIDAD.Trim();
            //                cDato.StrDescPadre = lst.ACTIVIDAD.Trim();
            //                cDato.LngNit = 0;
            //                cDato.StrLicencia = "";
            //                cDato.StrRepLegal = "";
            //                cDato.StrDireccion = "";
            //                cDato.StrDepartamento = "";
            //                cDato.IntAccion = 1;
            //                lstResultado.Add(cDato);
            //                idDe = (int)lst.ID_ACTIVIDAD;
            //                idUnico++;
            //            }
            //            else
            //            {
            //                //todo No implementado.
            //            }
            //            idUnico++;
            //        }
            //        Session[CVariablesSesion.EstructuraArbol] = lstResultado;
            //        tlsOperadores.DataSource = lstResultado;
            //        tlsOperadores.DataBind();
            //    }
            //    else
            //    {
            //        //todo No implementado.
            //    }
            //}
            //else
            //{
            //    //todo No implementado.
            //}
        }

        /// <summary>
        /// METODO. MCargaGrilla
        /// </summary>
        /// <param name="intIdActividad"></param>
        private void MCargaGrilla(decimal intIdActividad)
        {
            var objEntidades = _client.Post<EResultadoCumplimientoMes>(CAppSettings.svc_listar_reporte_cumplimiento,
            new
            {
                strCredencial = CParametrosHydro.strCredencialFuncionario,
                decFiltro = 1,
                strParametro1 = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]),
                strParametro2 = intIdActividad,
                strParametro3 = Convert.ToDecimal(cmbGestion.Value + cmbMes.Value.ToString().PadLeft(2, '0') + "01000000"),
                strParametro4 = Convert.ToDecimal(cmbGestion.Value + cmbMes.Value.ToString().PadLeft(2, '0') + DateTime.DaysInMonth((int)cmbGestion.Value, (int)cmbMes.Value).ToString().PadLeft(2, '0') + "235959"),
                strParametro5 = CParametrosHydro.decIdAplicacion
            });
            if (objEntidades.IntCodigo == 1)
            {
                objEntidades.OResultado.ForEach(a => a.strFechaReg = (a.FECHA_REPORTE==0 || a.ESTADO_MENSUAL != 1) ? "" : a.FECHA_REPORTE.ToString().Substring(6, 2) + "/" + a.FECHA_REPORTE.ToString().Substring(4, 2) + "/" + a.FECHA_REPORTE.ToString().Substring(0, 4) + " " + a.FECHA_REPORTE.ToString().Substring(8, 2) + ":" + a.FECHA_REPORTE.ToString().Substring(10, 2) + ":" + a.FECHA_REPORTE.ToString().Substring(10, 2));
                objEntidades.OResultado = objEntidades.OResultado.OrderBy(x => x.DEPARTAMENTO).ThenBy(x => x.DENOMINACION).ToList();
                dgvReporte.DataSource = objEntidades.OResultado;
                dgvReporte.DataBind();
                Session[CVariablesSesion.GrillaCabecera] = objEntidades.OResultado;
            }
            else
            {
                MImprimirMensaje(objEntidades.StrMensaje, EnumTiposMensajeAlerta.Error);
            }
        }

        /// <summary>
        /// METODO. MValidaDatosIniciales
        /// </summary>
        /// <returns></returns>
        private bool MValidaDatosIniciales()
        {
            bool booResultado = true;
            O_FECHA_HORA_ACTUAL_CTY objFeHoServ = Session[CVariablesSesion.FechaHoraServ] as O_FECHA_HORA_ACTUAL_CTY;

            if (cmbGestion.Text == "" || cmbMes.Text == "")
            {
                MImprimirMensaje("Introduzca un periodo valido.", EnumTiposMensajeAlerta.Error);
                booResultado = false;
            }
            else if (Convert.ToInt32(cmbGestion.Value+cmbMes.Value.ToString().PadLeft(2,'0')) > Convert.ToInt32(objFeHoServ.FECHA_HORA_ACTUAL.ToString().Substring(0,6)))
            {
                MImprimirMensaje("Introduzca un periodo valido.", EnumTiposMensajeAlerta.Error);
                booResultado = false;
            }
            return booResultado;
        }

        /// <summary>
        /// METODO. MValidaAmpliacion
        /// </summary>
        /// <param name="strMensaje"></param>
        /// <returns></returns>
        private bool MValidaAmpliacion(ref string strMensaje)
        {
            bool booResultado = true;

            detPamFechaIni.Date = DateTime.Parse(detPamFechaIni.Date.ToShortDateString());
            detPamFechaFin.Date = DateTime.Parse(detPamFechaFin.Date.ToShortDateString());

            var datFechaActual = DateTime.Now.Date;
            if (detPamFechaIni.Date < datFechaActual)
            {
                strMensaje = "Las fechas de habilitación no pueden ser menor a la fecha actual.";
                booResultado = false;
            }
            else if (detPamFechaFin.Date < detPamFechaIni.Date)
            {
                strMensaje = "Fechas de habilitación inválida.";
                booResultado = false;
            }
            else if (detPamFechaIni.Text.Substring(3, 7) != detPamFechaFin.Text.Substring(3, 7))
            {
                strMensaje = "Las fechas de habilitación deben estar en el mismo periodo.";
                booResultado = false;
            }
            else
            {
                var lstAmpliaciones = Session[CVariablesSesion.GrillaAmpliacion] as List<O_LISTA_AUTORIZACION_CONS_CTY>;
                if (lstAmpliaciones != null && lstAmpliaciones.Any())
                {
                    lstAmpliaciones = lstAmpliaciones.Where(x => x.ESTADO == 1).ToList();
                    if (lstAmpliaciones.Any())
                    {
                        strMensaje = "Solo puede tener una ampliación activa por periodo.";
                        booResultado = false;
                    }
                    else
                    {
                        //todo No implementado
                    }
                }
            }
            return booResultado;
        }

        /// <summary>
        /// METODO. MValidaIntimacion
        /// </summary>
        /// <param name="strMensaje"></param>
        /// <returns></returns>
        private bool MValidaIntimacion(ref string strMensaje)
        {
            bool booResultado = true;

            if (txtPinCb.Text == "" || txtPinCb.Text == "0")
            {
                strMensaje = "Introduzca un código de barras.";
                booResultado = false;
            }
            //else if(txtPinNotificante.Text == "")
            //{
            //    strMensaje = "Introduzca la persona notificante.";
            //    booResultado = false;
            //}
            //else if(txtPinNotificado.Text == "")
            //{
            //    strMensaje = "Introduzca la persona notificada.";
            //    booResultado = false;
            //}
            
            return booResultado;
        }

        /// <summary>
        /// METODO. MLimpiaGrilla
        /// </summary>
        private void MLimpiaGrilla()
        {
            Session.Remove(CVariablesSesion.GrillaCabecera);
            dgvReporte.DataSource = null;
            dgvReporte.DataBind();

            Session.Remove(CVariablesSesion.GrillaAcumulado);
            dgvAcumulado.DataSource = null;
            dgvAcumulado.DataBind();

            pnlMensaje.Visible = false;
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