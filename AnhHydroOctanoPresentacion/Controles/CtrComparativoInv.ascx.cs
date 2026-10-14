
using AnhHydroOctanoPresentacion.Lib;

namespace AnhHydroOctanoPresentacion.Controles
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Globalization;
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
    using DevExpress.Web;
    using Librerias.Anh.Us;
    using ServiceStack.ServiceClient.Web;
    using UserControl = System.Web.UI.UserControl;
    using EResultado = Entidades.EResultado;
    using Page = System.Web.UI.Page;
    using DevExpress.Utils;
    using ServiceStack.Text;
    public partial class CtrComparativoInv : UserControl
    {
        #region Variables de entorno

        readonly JsonServiceClient _client = new JsonServiceClient(CAppSettings.servicio_octVolumen);
        private readonly CPersistenciaConsulta _consulta = new CPersistenciaConsulta();
        readonly CUtil _cUtil = new CUtil();
        readonly CTrazaInf _cTrazaInf = new CTrazaInf();
        CConsultasParametricas cConsultasParametricas = new CConsultasParametricas();

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
                        Session.Remove(CVariablesSesion.EstructuraArbol);
                        Session.Remove(CVariablesSesion.ComboTiposOperacion);
                        Session.Remove(CVariablesSesion.ListaPeriodos);
                        Session.Remove(CVariablesSesion.GrillaCabecera);
                        Session.Remove(CVariablesSesion.ComboTipoOperacion);
                        Session.Remove(CVariablesSesion.ReportePrincipal);
                        Session.Remove(CVariablesSesion.TipoReporte);

                        MCargaArbol();
                        MCargaParametricas();
                        MCargaPermisos();
                        ConfigurarFormulario();
                        
                        tlsOperadores.DataSource = Session[CVariablesSesion.EstructuraArbol];
                        tlsOperadores.DataBind();
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
                        else
                        {
                            //todo No es carga inicial: no implementado
                        }
                    }
                    else
                    {
                        //todo No es carga inicial: no implementado
                    }

                    if (tlsOperadores.DataSource == null)
                    {
                        tlsOperadores.DataSource = Session[CVariablesSesion.EstructuraArbol];
                        tlsOperadores.DataBind();
                    }
                    else
                    {
                        //todo No es carga inicial: no implementado
                    }

                    if (dgvComparativo.DataSource == null && Session[CVariablesSesion.GrillaCabecera] != null)
                    {
                        dgvComparativo.DataSource = Session[CVariablesSesion.GrillaCabecera];
                        dgvComparativo.DataBind();
                    }
                    else
                    {
                        //todo grilla vacia: no implementado
                    }
                    
                    if (cmbTipoOperacion.DataSource == null && Session[CVariablesSesion.ComboTiposOperacion] != null)
                    {
                        cmbTipoOperacion.DataSource = Session[CVariablesSesion.ComboTiposOperacion];
                        cmbTipoOperacion.DataBind();
                    }
                    else
                    {
                        //todo grilla vacia: no implementado
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

        protected void pnlCabecera_Callback(object sender, CallbackEventArgsBase e)
        {
            Session.Remove(CVariablesSesion.ListaPeriodos);
            Session.Remove(CVariablesSesion.ComboTiposOperacion);
            cmbTipoOperacion.DataSource = null;
            cmbTipoOperacion.DataBind();
            cmbTipoOperacion.Text = "";

            if ((int)tlsOperadores.FocusedNode.GetValue("IntAccion") > 0)
            {
                //tipos de operacion
                var objTipoOperacion = _client.Post<EResultadoConfigTipoOperacion>(CAppSettings.svc_listar_autorizacion_accion,
                new
                {
                    strCredencial = CParametrosHydro.strCredencialFuncionario,
                    decFiltro = 1,
                    strParametro1 = tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString(),
                    strParametro2 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
                    strParametro3 = "",
                    strParametro4 = ""
                });
                if (objTipoOperacion.IntCodigo == 1)
                {
                    var lstTiOp = objTipoOperacion.OResultado;
                    Session[CVariablesSesion.ComboTipoOperacion] = lstTiOp;

                    lstTiOp = lstTiOp.Where(x => x.FACTOR_CALCULO == 0 && x.CODIGO.Contains("MD")).ToList();
                    cmbTipoOperacion.DataSource = lstTiOp;
                    cmbTipoOperacion.DataBind();
                    cmbTipoOperacion.SelectedIndex = 0;

                    Session[CVariablesSesion.ComboTiposOperacion] = lstTiOp;
                }
                else
                {
                    MImprimirMensaje(objTipoOperacion.StrMensaje, EnumTiposMensajeAlerta.Error);
                }

                //carga periodos habilitados
                var objPeriodo = _client.Post<EResultadoPeriodos>(CAppSettings.svc_lista_periodos_consumidor,
                    new
                    {
                        strCredencial = CParametrosHydro.strCredencialFuncionario,
                        decFiltro = 2,
                        strParametro1 = tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString(),
                        strParametro2 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
                        strParametro3 = "",
                        strParametro4 = "",
                        strParametro5 = tlsOperadores.FocusedNode.GetValue("DecIdDepartamento").ToString()
                    });
                if (objPeriodo.IntCodigo == 1)
                {
                    Session[CVariablesSesion.ListaPeriodos] = objPeriodo.OResultado;
                }
                else
                {
                    //todo No implementado
                }
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
                    lblRepLegal.Text = (tlsOperadores.FocusedNode.GetValue("StrDireccion")==null)?"":tlsOperadores.FocusedNode.GetValue("StrDireccion").ToString();
                    lblDepto.Text = tlsOperadores.FocusedNode.GetValue("StrDepartamento").ToString();

                    MLimpiaGrilla();
                    if ((int) tlsOperadores.FocusedNode.GetValue("IntAccion") > 0)
                    {
                        var strMensaje = string.Empty;
                        if (MValidaDatosIniciales(ref strMensaje))
                        {
                            var strDato1 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString();
                            var strDato2 = tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString();
                            var strDato3 = (string)cmbTipoOperacion.Value;
                            var strDato4 = detFechaIni.Date.Year + detFechaIni.Date.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + detFechaIni.Date.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0');
                            var strDato5 = detFechaFin.Date.Year + detFechaFin.Date.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + detFechaFin.Date.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0');

                            MCargaParametricasEsp();
                            MCargaGrilla(strDato1, strDato2, strDato3, strDato4, strDato5);
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
                        decNivelCapa = (int) CLogTraza.CapasNivel.Presentacion
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

        protected void dgvComparativo_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            try
            {
                #region Proceso a Ejecutar

                if (e.VisibleIndex >= 0)
                {
                    e.Row.BackColor = Convert.ToDecimal(dgvComparativo.GetRowValues(e.VisibleIndex, "DIFERENCIA")) == 0 ? Color.FromArgb(186, 209, 182) : Color.FromArgb(215, 135, 135);
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

        protected void dgvComparativo_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar
                    int index = dgvComparativo.FocusedRowIndex;
                    long lngIdMov = Convert.ToInt64(dgvComparativo.GetRowValues(index, "ID_MOVIMIENTO"));
                    if (lngIdMov > 0)
                    {
                        var decFecha = CFechas.ConvierteDateTimeLong(DateTime.Now);
                        var lngFechaOp = Convert.ToInt64(dgvComparativo.GetRowValues(index, "FECHA_OPERACION"));
                        var datFechaOp = CFechas.ConvierteLongDateTime(lngFechaOp);

                        if (e.ButtonID == "btnGenerar")
                        {
                            //existe diferencia
                            if (Convert.ToDecimal(dgvComparativo.GetRowValues(index, "DIFERENCIA")) == 0)
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError", "alert('No existe diferencia.');", true);
                                return;
                            }
                            //valida periodo habilitado
                            var lstPeriodo = (List<O_PERIODO_CTY>)Session[CVariablesSesion.ListaPeriodos];
                            lstPeriodo = lstPeriodo.Where(x => x.decAño == datFechaOp.Date.Year && x.decMes == datFechaOp.Date.Month).ToList();
                            if (!lstPeriodo.Any())
                            {
                                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError", "alert('Periodo no habilitado para ejecutar la función.');", true);
                                MImprimirMensaje("Periodo no habilitado para ejecutar la función.", EnumTiposMensajeAlerta.Error);
                                return;
                            }
                            //que exista diferencia
                            var lstTiOp = (List<O_LISTA_AUTORIZACION_ACCI_CTY>)Session[CVariablesSesion.ComboTipoOperacion];
                            lstTiOp = Convert.ToDecimal(dgvComparativo.GetRowValues(index, "DIFERENCIA")) > 0 ? lstTiOp.Where(x => x.FACTOR_CALCULO == 1 && x.CODIGO.Contains("AJ")).ToList() : lstTiOp.Where(x => x.FACTOR_CALCULO == -1 && x.CODIGO.Contains("AJ")).ToList();
                            if (!lstTiOp.Any())
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError", "alert('Inconsistencia de datos. No existe un tipo de operacion de ajuste para el documento de diferencia.');", true);
                                return;
                            }
                            //no exista ddjj
                            var objValidacion = _client.Post<EResultadoGenerico>(CAppSettings.svc_validaciones_general,
                            new
                            {
                                strCredencial = CParametrosHydro.strCredencialFuncionario,
                                strFiltro = "1",
                                strParametro1 = tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString(),
                                strParametro2 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
                                strParametro3 = "",
                                strParametro4 = lngFechaOp.ToString().Substring(0, 6) + "01" + "000000",
                                strParametro5 = lngFechaOp.ToString().Substring(0, 6) + DateTime.DaysInMonth(datFechaOp.Date.Year, datFechaOp.Date.Month).ToString().PadLeft(2, '0') + "235959",
                                strParametro6 = "",
                                decAppFechaRegistro = 0,
                                decAppIdUsuario = 0
                            });
                            if (objValidacion.IntCodigo != 1)
                            {
                                MImprimirMensaje(objValidacion.StrMensaje, EnumTiposMensajeAlerta.Error);
                                return;
                            }

                            var objNuevoReg = new ObjMovimientoTransaccion();
                            objNuevoReg.idMovimiento = 0;
                            objNuevoReg.idMovimientoPadre = 0;
                            objNuevoReg.idTipoActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                            objNuevoReg.idTipoOperacion = lstTiOp.FirstOrDefault().ID_TIPO_OPERACION;
                            objNuevoReg.idConsumidor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString());
                            objNuevoReg.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                            objNuevoReg.fechaRegistroMovimiento = decFecha;
                            objNuevoReg.fechaOperacion = lngFechaOp;
                            objNuevoReg.idTipoMovimientoFlujo = 20;
                            objNuevoReg.idTipoMovimiento = 18;
                            objNuevoReg.idEstadoMovimiento = 11;
                            objNuevoReg.idFuenteOrigenDato = (decimal)EnumTipoRegistro.OrigenWeb;
                            objNuevoReg.idUbicacionOperacion = 120;
                            objNuevoReg.idUnidadMedidaHidrocarburo = 68;//todo obtener X consulta
                            objNuevoReg.idHidrocarburo = Convert.ToDecimal(dgvComparativo.GetRowValues(index, "ID_HIDROCARBURO"));
                            objNuevoReg.cantidadHidrocarburo = Convert.ToDecimal(dgvComparativo.GetRowValues(index, "DIFERENCIA"));
                            objNuevoReg.cantidadHidrocarburo = (objNuevoReg.cantidadHidrocarburo < 0) ? objNuevoReg.cantidadHidrocarburo * -1 : objNuevoReg.cantidadHidrocarburo;
                            objNuevoReg.identificador = "";
                            objNuevoReg.subIdentificador = "";
                            objNuevoReg.observacion = "Generado desde Comparativo de Inventarios IdMedicion: " + dgvComparativo.GetRowValues(index, "ID_MOVIMIENTO");
                            objNuevoReg.barcode = 0;
                            objNuevoReg.idTipoMedicion = -99999;
                            objNuevoReg.cantidadAguaLitros = 0;
                            objNuevoReg.idTipoMedioTransporte = -99999;
                            objNuevoReg.idMedioTransporte = -99999;
                            objNuevoReg.tipoInfraestructura = Convert.ToDecimal(dgvComparativo.GetRowValues(index, "TIPO_INFRAESTRUCTURA"));
                            objNuevoReg.idInfraestructura = Convert.ToDecimal(dgvComparativo.GetRowValues(index, "ID_INFRAESTRUCTURA"));
                            objNuevoReg.idUnidadEnvasesTransporte = -99999;
                            objNuevoReg.codigo = "";
                            objNuevoReg.idIntegracion = -99999;
                            objNuevoReg.idUnidadTemperatura = -99999;
                            objNuevoReg.temperatura = 0;
                            objNuevoReg.gradoApi = "";
                            objNuevoReg.idEntidad = -99999;
                            objNuevoReg.cantidadEnvasesTransporte = 0;
                            objNuevoReg.binarioControl = "00000-0000000000-0000000000-00000";
                            objNuevoReg.idTransaccionBaseExterna = "";

                            objNuevoReg.jsonReferencia = "";
                            objNuevoReg.estadoJsonRef = -99999;
                            objNuevoReg.idCuenta = -99999;
                            objNuevoReg.idCuentaAux = -99999;
                            objNuevoReg.idParamGlobal = -99999;
                            objNuevoReg.idParamEspec = -99999;
                            objNuevoReg.idTipoConsumidor = -99999;
                            objNuevoReg.idTipoInfraAux = -99999;
                            objNuevoReg.idInfraAux = -99999;

                            objNuevoReg.listaTransaccion = new List<ObjTransaccion>();
                            var nroUnico = DateTime.Now.Hour + "" + DateTime.Now.Minute + DateTime.Now.Second + DateTime.Now.Millisecond;
                            var objTrasaccion = new ObjTransaccion();
                            objTrasaccion.idMovimiento = 0;
                            objTrasaccion.idTransaccion = 0;
                            objTrasaccion.idTransaccionPadre = 0;
                            objTrasaccion.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                            objTrasaccion.fechaRegistroTransaccion = decFecha;
                            objTrasaccion.idTipoTransaccion = (decimal)EnumTipoTransaccion.NotaMovimiento;
                            objTrasaccion.fechaEmision = lngFechaOp;
                            objTrasaccion.numeroTransaccion = nroUnico;
                            objTrasaccion.numeroAutorizacion = 0;
                            objTrasaccion.codigoControl = "";
                            objTrasaccion.nitConsumidor = 0;
                            objTrasaccion.razonSocial = "";
                            objTrasaccion.precioUnitario = 0;
                            objTrasaccion.precioTotal = 0;
                            objTrasaccion.usuarioTransaccion = "";
                            objTrasaccion.observaciones = "";
                            objTrasaccion.nitEmisor = 0;
                            objTrasaccion.idEstadoTransaccion = 11;
                            objTrasaccion.idTipoCreditoFiscal = 9;
                            objTrasaccion.idFormaPago = 1;
                            objTrasaccion.idTipoMoneda = 1;
                            objTrasaccion.idTipoEmisor = 14;
                            objTrasaccion.codigoSeguridad = 0;
                            objTrasaccion.idAutorizacionDato = 0;
                            objTrasaccion.binarioControl = "0000000000";

                            objTrasaccion.fechaOperacion = lngFechaOp;
                            objTrasaccion.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                            objTrasaccion.codigo = "";

                            objNuevoReg.listaTransaccion.Add(objTrasaccion);

                            var objRegistro = new ERegistraMovimientoTransaccion();
                            objRegistro.strCredencial = CParametrosHydro.strCredencialFuncionario;
                            objRegistro.decFiltro = 1;
                            var lstRegistros = new List<ObjMovimientoTransaccion> { objNuevoReg };
                            objRegistro.listaMovimientoTransaccion = lstRegistros;

                            var resultado = _client.Post<EResultado>(CAppSettings.svc_registra_movimiento_transaccion, objRegistro);
                            if (resultado.intCodigo == 1)
                            {
                                pnlDatos_Callback(null, null);
                            }
                            else
                            {
                                MImprimirMensaje(resultado.strMensaje, EnumTiposMensajeAlerta.Alerta);
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
            else
            {
                //todo No implementado.
            }
        }

        protected void dgvComparativo_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
        {
            if (!MValidaSesion()) return;
            try
            {
                #region Proceso a Ejecutar

                if (e.VisibleIndex == -1) return;

                if (e.ButtonID == "btnGenerar")
                {
                    e.Visible = Convert.ToInt16(Session[CVariablesSesion.PermisoAlta]) == 1 ? DefaultBoolean.True : DefaultBoolean.False;
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

        protected void btnEmiteReporte_Click(object sender, EventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    if ((int)cmbTipoExport.Value == 1)
                    {
                        dgeExportar.GridViewID = "dgvComparativo";
                        dgeExportar.WriteXlsxToResponse("RepInventarioFisVsDoc");
                    }
                    else
                    {
                        var xreReporte = new repInventarioFisDoc();
                        xreReporte.xtcFecha.Text = DateTime.Now.ToShortDateString();
                        xreReporte.xtcHora.Text = DateTime.Now.ToLongTimeString();
                        xreReporte.xtcUsuario.Text = CParametrosHydro.StrUsuarioLogin;
                        xreReporte.xtcIntervalo.Text = "Del : " + detFechaIni.Date.ToShortDateString() + "  Al : " + detFechaFin.Date.ToShortDateString();
                        xreReporte.xtcActividad.Text = tlsOperadores.FocusedNode.GetValue("StrActividad").ToString();
                        xreReporte.xtcProyecto.Text = tlsOperadores.FocusedNode.GetValue("StrDescPadre").ToString();
                        xreReporte.xtcNit.Text = tlsOperadores.FocusedNode.GetValue("LngNit").ToString();
                        xreReporte.xtcDireccion.Text = (tlsOperadores.FocusedNode.GetValue("StrDireccion") == null) ? "" : tlsOperadores.FocusedNode.GetValue("StrDireccion").ToString();
                        xreReporte.xtcDepto.Text = tlsOperadores.FocusedNode.GetValue("StrDepartamento").ToString();

                        var lstSelect = new List<O_COMPARATIVO_FIS_DOC_CTY>();
                        for (int i = 0; i < dgvComparativo.VisibleRowCount; i++)
                        {
                            if (dgvComparativo.IsGroupRow(i)) continue;
                            var objRep = (O_COMPARATIVO_FIS_DOC_CTY)dgvComparativo.GetRow(i);
                            lstSelect.Add(objRep);
                        }

                        xreReporte.DataSource = lstSelect;

                        Session[CVariablesSesion.ReportePrincipal] = xreReporte;
                        Session[CVariablesSesion.TipoReporte] = EnumTipoReporte.RepCompInventario;
                        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( 'WFReporte.aspx', '');", true);
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
                //todo no implementado
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
                                                                      "/WFComparativoInv.aspx?format=json");
                if (lstResultado.decCodigo == 1)
                {
                    if (lstResultado.oResultado != null && lstResultado.oResultado.Any())
                    {
                        Session[CVariablesSesion.PermisoAlta] = lstResultado.oResultado.FirstOrDefault().ALTAS;
                    }
                    else
                    {
                        Session[CVariablesSesion.PermisoAlta] = 0;
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
        /// METODO. ConfigurarFormulario
        /// </summary>
        private void ConfigurarFormulario()
        {
            detFechaIni.Date = DateTime.Parse("01/" + DateTime.Now.Month + "/" + DateTime.Now.Year);
            detFechaFin.Date = DateTime.Now;

            cmbTipoOperacion.Enabled = true;
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
        /// METODO. MCargaParametricas
        /// </summary>
        private void MCargaParametricas()
        {
            //TIPO TRANSACCION
            var lstObjTransaccion = _client.Post<EResultadoTipoTransaccion>(CAppSettings.svc_lista_tipos_transaccion,
            new
            {
                strCredencial = CParametrosHydro.strCredencialFuncionario,
                decFiltro = 1,
                strParametro1 = "",
                strParametro2 = ""
            });
            if (lstObjTransaccion.IntCodigo == 1)
            {
                lstObjTransaccion.OResultado = lstObjTransaccion.OResultado.OrderBy(x => x.ID_TIPO_TRANSACCION_PADRE).ThenBy(x => x.NOMBRE).ToList();
                Session[CVariablesSesion.ListaTransaccion] = lstObjTransaccion.OResultado;
            }
        }

        /// <summary>
        /// METODO. MCargaParametricasEspecificas
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
            if (objParametros.IntCodigo==1)
            {
                Session[CVariablesSesion.OctanoParametrica] = objParametros.OResultado;
            }
            else
            {
                Session.Remove(CVariablesSesion.OctanoParametrica);
            }
        }

        /// <summary>
        /// METODO. MCargaGrilla
        /// </summary>
        /// <param name="strIdTipoActividad"></param>
        /// <param name="strIdConsumidor"></param>
        /// <param name="strTipoOperacion"></param>
        /// <param name="strFechaDiaInicio"></param>
        /// <param name="strFechaDiaFin"></param>
        private void MCargaGrilla(string strIdTipoActividad, string strIdConsumidor, string strTipoOperacion, string strFechaDiaInicio, string strFechaDiaFin)
        {
            var objConsulta = new EObjetoEnvio5ParametrosProto
            {
                strCredencial = CParametrosHydro.strCredencialFuncionario,
                decFiltro = 1,
                strParametro1 = strIdTipoActividad,
                strParametro2 = strIdConsumidor,
                strParametro3 = strTipoOperacion,
                strParametro4 = strFechaDiaInicio + "000000",
                strParametro5 = strFechaDiaFin + "235959"
            };
            var objResultado = _consulta.ListaComparativoInv(objConsulta);
            if (objResultado.intCodigo == 1)
            {
                var lstRes = objResultado.oResultado;
                dgvComparativo.DataSource = lstRes;
                dgvComparativo.DataBind();
                Session[CVariablesSesion.GrillaCabecera] = lstRes;

                #region Traza consulta de informacion
                string strAccion = JsonSerializer.SerializeToString(objConsulta);
                string strSalida = ((List<O_COMPARATIVO_FIS_DOC_CTY>)lstRes).Count().ToString();
                string mensaje = GetType().Name + "." + MethodBase.GetCurrentMethod().Name;
                _cTrazaInf.MRegistraTrazaInf(strAccion, mensaje, CParametrosHydro.decIdAplicacion, Convert.ToDecimal(Session[CVariablesSesion.IdUsuario].ToString()), HttpContext.Current.Request.UserHostAddress, strSalida);
                #endregion 
            }
            else
            {
                MImprimirMensaje(objResultado.strMensaje, EnumTiposMensajeAlerta.Error);
            }
        }

        /// <summary>
        /// METODO. MValidaDatosIniciales
        /// </summary>
        /// <param name="strMensaje"></param>
        /// <returns></returns>
        private bool MValidaDatosIniciales(ref string strMensaje)
        {
            bool booResultado = true;

            if (detFechaIni.Text == "" || detFechaFin.Text == "")
            {
                strMensaje = "Introduzca fechas validas.";
                booResultado = false;
            }
            else if (detFechaFin.Date < detFechaIni.Date)
            {
                strMensaje = "Fechas incorrectas.";
                booResultado = false;
            }
            else if (cmbTipoOperacion.DataSource == null || cmbTipoOperacion.Text == "")
            {
                strMensaje = "Seleccione un tipo de operación.";
                booResultado = false;
            }
            return booResultado;
        }

        /// <summary>
        /// METODO. MLimpiaGrilla
        /// </summary>
        private void MLimpiaGrilla()
        {
            Session.Remove(CVariablesSesion.GridCabecera);
            dgvComparativo.DataSource = null;
            dgvComparativo.DataBind();

            Session.Remove(CVariablesSesion.ReportePrincipal);
            Session.Remove(CVariablesSesion.TipoReporte);

            MImprimirMensaje("", EnumTiposMensajeAlerta.Limpiar);
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
            lblMensajeError.ForeColor = Color.DimGray;
            //if (tipoMensaje != EnumTiposMensajeAlerta.Correcto)
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError", "alert('" + strMensaje + "');", true);
            lblMensajeError.Text = "   " + strMensaje + "   ";
        }

        #endregion

    }
}