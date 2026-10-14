
using AnhHydroOctanoPersistencia.Aplicacion.Gestion;

namespace AnhHydroOctanoPresentacion.Controles
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Reflection;
    using System.Web;
    using System.Web.UI;
    using AnhHydroOctanoPersistencia.Aplicacion.Consulta;
    using AnhHydroOctanoPersistencia.Entidades;
    using Clases.Util;
    using Entidades;
    using Enumeradores;
    using Parametros;
    using DevExpress.Web;
    using Librerias.Anh.Us;
    using ServiceStack.ServiceClient.Web;
    using UserControl = System.Web.UI.UserControl;

    public partial class CtrPlanCuentas : UserControl
    {
        #region Variables de entorno

        readonly JsonServiceClient _client = new JsonServiceClient(CAppSettings.servicio_octVolumen);
        private readonly CPersistenciaConsulta _consulta = new CPersistenciaConsulta();
        readonly CUtil _cUtil = new CUtil();
        private readonly CPersistenciaGestion _gestion = new CPersistenciaGestion();

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
                        Session.Remove(CVariablesSesion.GrillaTransaccion);
                        Session.Remove(CVariablesSesion.ListaParamGral);
                        Session.Remove(CVariablesSesion.ListaParametrica);

                        MCargaArbol();
                        MCargaParametricas();
                    }
                    else
                    {
                        //todo No es carga inicial: no implementado
                    }

                    if (tlsOperadores.DataSource == null && Session[CVariablesSesion.EstructuraArbol] != null)
                    {
                        tlsOperadores.DataSource = Session[CVariablesSesion.EstructuraArbol];
                        tlsOperadores.DataBind();
                    }
                    else
                    {
                        //todo No es carga inicial: no implementado
                    }

                    if (dgvCuentas.DataSource == null && Session[CVariablesSesion.GrillaTransaccion] != null)
                    {
                        dgvCuentas.DataSource = Session[CVariablesSesion.GrillaTransaccion];
                        dgvCuentas.DataBind();
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
                    #region Proceso a Ejecutar

                    lblActividad.Text = tlsOperadores.FocusedNode.GetValue("StrDescPadre").ToString();
                    MLimpiaGrilla();

                    int intIdActividad = (int)tlsOperadores.FocusedNode.GetValue("IntIdActividad");
                    MCargaParamEspecifico(intIdActividad);
                    MCargaGrilla(intIdActividad);

                    #endregion
                }
                catch (Exception exp)
                {
                    #region RegistraError

                    MLimpiaGrilla();
                    MImprimirMensaje(exp.Message, EnumTiposMensajeAlerta.Error, false);
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

        protected void dgvCuentas_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            try
            {
                #region Proceso a Ejecutar

                if (e.VisibleIndex >= 0)
                {
                    if (dgvCuentas.GetRowValues(e.VisibleIndex, "ESTADO").ToString() == "1")
                        e.Row.BackColor = Color.FromArgb(164, 184, 162);
                    else
                    {
                        if (e.VisibleIndex % 2 == 1)
                        {
                            e.Row.BackColor = Color.FromArgb(204, 209, 182);
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

                MImprimirMensaje(exp.Message, EnumTiposMensajeAlerta.Error, false);
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

        protected void pnlCta_Callback(object sender, CallbackEventArgsBase e)
        {
            int index = dgvCuentas.FocusedRowIndex;
            long lngId = Convert.ToInt64(dgvCuentas.GetRowValues(index, "ID_AUTORIZACION_CONDICION"));
            if (lngId > 0)
            {
                txtCtaId.Text = dgvCuentas.GetRowValues(index, "ID_AUTORIZACION_CONDICION").ToString();
                txtCtaPadre.Text = dgvCuentas.GetRowValues(index, "CODIGO").ToString();
                txtCtaCodigo.Text = "";
                txtDescripcion.Text = "";
            }
        }

        protected void dgvCuentas_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    int index = dgvCuentas.FocusedRowIndex;
                    long lngId = Convert.ToInt64(dgvCuentas.GetRowValues(index, "ID_AUTORIZACION_CONDICION"));
                    if (lngId > 0)
                    {
                        if (e.ButtonID == "btnEliminar")
                        {
                            var objResultado = _client.Post<EResultado>(CAppSettings.svc_elimina_autorizacion_condicion,
                            new
                            {
                                strCredencial = CParametrosHydro.strCredencialFuncionario,
                                decFiltro = 5,
                                decIdTipoActividad = lngId,
                                decIdConsumidor = 0,
                                decIdIdentificador = 0,
                                decAppFechaRegistro = CFechas.ConvierteDateTimeLong(DateTime.Now),
                                decAppIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario])
                            });

                            if (objResultado.intCodigo == 1)
                            {
                                pnlDatos_Callback(null, null);
                            }
                            else
                            {
                                MImprimirMensaje(objResultado.strMensaje, EnumTiposMensajeAlerta.Error, false);
                            }
                        }
                    }

                    #endregion
                }
                catch (Exception exp)
                {
                    #region RegistraError

                    MImprimirMensaje(exp.Message, EnumTiposMensajeAlerta.Error, false);
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

        protected void pucConfig_WindowCallback(object source, PopupWindowCallbackArgs e)
        {
            try
            {
                #region Proceso a Ejecutar

                cmbReporteVariable.Text = "";
                cmbReporteVariable.DataSource = null;
                cmbReporteVariable.DataBind();

                if (txtId.Text == "1")
                {
                    int index = dgvCuentas.FocusedRowIndex;
                    if (index >= 0)
                    {
                        txtId.Text = dgvCuentas.GetRowValues(index, "ID_TIPO_OPERACION").ToString();
                        txtCodigo.Text = dgvCuentas.GetRowValues(index, "CODIGO").ToString();
                        txtDescripcion.Text = dgvCuentas.GetRowValues(index, "NOMBRE").ToString();
                        rbtTipo.Value = Convert.ToInt32(dgvCuentas.GetRowValues(index, "FACTOR_CALCULO").ToString());
                        spnOrden.Number = Convert.ToInt32(dgvCuentas.GetRowValues(index, "ORDEN").ToString());
                        txtLimiteMin.Text = dgvCuentas.GetRowValues(index, "LIMITE_MINIMO").ToString();
                        txtLimiteMax.Text = dgvCuentas.GetRowValues(index, "LIMITE_MAXIMO").ToString();

                        cmbReporteVariable_Callback(null, null);
                        cmbReporteVariable.Value = cmbReporteVariable.Items.FindByValue(dgvCuentas.GetRowValues(index, "ID_REPORTE_VARIABLE").ToString());
                    }
                    else
                    {
                        btnGrabar.Enabled = false;
                    }
                }
                else
                {
                    txtId.Text = "0";
                    txtCodigo.Text = "";
                    txtDescripcion.Text = "";
                    rbtTipo.Value = 1;
                    spnOrden.Number = 1;
                    txtLimiteMin.Text = "";
                    txtLimiteMax.Text = "";
                    cmbReporteVariable.ReadOnly = true;
                }

                #endregion
            }
            catch (Exception exp)
            {
                #region RegistraError

                MImprimirMensaje(exp.Message, EnumTiposMensajeAlerta.Error, false);
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

        protected void cmbReporteVariable_Callback(object sender, CallbackEventArgsBase e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    cmbReporteVariable.Text = "";
                    cmbReporteVariable.DataSource = null;
                    cmbReporteVariable.DataBind();

                    var lst = (List<O_LISTA_REPORTE_VARIABLES_CTY>)Session[CVariablesSesion.ListaParamGral];
                    cmbReporteVariable.DataSource = lst.Where(y => y.CODIGO.Contains((((int)rbtTipo.Value == 1) ? "ING" : (((int)rbtTipo.Value == -1) ? "EGR" : "NOP")))).OrderBy(x => x.CODIGO).ToList();
                    cmbReporteVariable.DataBind();

                    #endregion
                }
                catch (Exception exp)
                {
                    #region RegistraError

                    MImprimirMensaje(exp.Message, EnumTiposMensajeAlerta.Error, false);
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

        protected void btnGrabar_Click(object sender, EventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    var LstPadreTipoOperacion = (List<O_TIPOS_OPE_ACT_CTY>)Session[CVariablesSesion.ListaParametrica];
                    LstPadreTipoOperacion = LstPadreTipoOperacion.Where(x => x.CODIGO == (((int)rbtTipo.Value == 1) ? "RECEP" : ((int)rbtTipo.Value == -1) ? "DESP" : "OPENEU")).ToList();
                    if (!LstPadreTipoOperacion.Any())
                    {
                        MImprimirMensaje("Incoherencia de datos. Paramétrica ID_TIPO_OPERACION_PADRE no definida.", EnumTiposMensajeAlerta.Error, true);
                    }
                    else if (Convert.ToDecimal(txtLimiteMin.Text) > Convert.ToDecimal(txtLimiteMax.Text))
                    {
                        MImprimirMensaje("Incoherencia de datos. Introduzca límites válidos.", EnumTiposMensajeAlerta.Error, true);
                    }
                    else
                    {
                        var datFechaReg = DateTime.Now;
                        var decFechaReg = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString().PadLeft(2, '0') + datFechaReg.Day.ToString().PadLeft(2, '0') + datFechaReg.Hour.ToString().PadLeft(2, '0') + datFechaReg.Minute.ToString().PadLeft(2, '0') + datFechaReg.Second.ToString().PadLeft(2, '0'));

                        EResultado resultado;

                        if (Convert.ToInt64(txtId.Text) == 0)
                        {
                            resultado = _client.Post<EResultado>(CAppSettings.svc_tipos_operacion,
                            new
                            {
                                decIdTipoActividad = (int)tlsOperadores.FocusedNode.GetValue("IntIdActividad"),
                                strCodigo = txtCodigo.Text.Trim(),
                                strNombre = txtDescripcion.Text.Trim(),
                                strDescripcion = txtDescripcion.Text.Trim(),
                                decOrden = spnOrden.Number,
                                decFactorCalculo = rbtTipo.Value,
                                decIdTipoOperacionPadre = LstPadreTipoOperacion.FirstOrDefault().ID_TIPO_OPERACION,
                                decLimiteMinimo = Convert.ToDecimal(txtLimiteMin.Text),
                                decLimiteMaximo = Convert.ToDecimal(txtLimiteMax.Text),
                                decAppFechaRegistro = decFechaReg,
                                decAppIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario])
                            });
                        }
                        else
                        {
                            resultado = _client.Post<EResultado>(CAppSettings.svc_actualiza_tipos_operacion,
                            new
                            {
                                decIdTipoActividad = (int)tlsOperadores.FocusedNode.GetValue("IntIdActividad"),
                                decIdTipoOperacion = Convert.ToDecimal(txtId.Text),
                                strCodigo = txtCodigo.Text.Trim(),
                                strNombre = txtDescripcion.Text.Trim(),
                                strDescripcion = txtDescripcion.Text.Trim(),
                                decOrden = spnOrden.Number,
                                decFactorCalculo = rbtTipo.Value,
                                decIdTipoOperacionPadre = LstPadreTipoOperacion.FirstOrDefault().ID_TIPO_OPERACION,
                                decLimiteMinimo = Convert.ToDecimal(txtLimiteMin.Text),
                                decLimiteMaximo = Convert.ToDecimal(txtLimiteMax.Text),
                                decAppFechaRegistro = decFechaReg,
                                decAppIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario])
                            });
                        }
                        if (resultado.intCodigo == 1)
                        {
                            if (cmbReporteVariable.Text.Trim()!="" && Convert.ToDecimal(txtId.Text) > 0)
                            {
                                resultado = _client.Post<EResultado>(CAppSettings.svc_elimina_operacion_variable,
                                new
                                {
                                    strCredencial = CParametrosHydro.strCredencialFuncionario,
                                    decIdTipoActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad")),
                                    decIdTipoOperacion = Convert.ToDecimal(txtId.Text)
                                });
                                if (resultado.intCodigo == 1)
                                {
                                    resultado = _client.Post<EResultado>(CAppSettings.svc_registra_operacion_variable,
                                    new
                                    {
                                        strCredencial = CParametrosHydro.strCredencialFuncionario,
                                        strCodigoReporte = "UD/AB-RDJ-P01-F01",
                                        //strCodigoVariable = cmbReporteVariable.Value,
                                        strCodigoVariable = cmbReporteVariable.Text.Trim(),
                                        decIdTipoActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad")),
                                        strObservaciones = "",
                                        decEstadoReporte = 1,
                                        decIdTipoOperacion = Convert.ToDecimal(txtId.Text),
                                        decAppIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]),
                                        decAppFechaRegistro = decFechaReg
                                    });
                                    if (resultado.intCodigo == 1)
                                    {
                                        pucConfig.ShowOnPageLoad = false;
                                        pnlDatos_Callback(null, null);
                                    }
                                    else
                                    {
                                        MImprimirMensaje(resultado.strMensaje, EnumTiposMensajeAlerta.Error, true);
                                    }
                                }
                                else
                                {
                                    MImprimirMensaje(resultado.strMensaje, EnumTiposMensajeAlerta.Error, true);
                                }
                            }
                            else
                            {
                                pucConfig.ShowOnPageLoad = false;
                                pnlDatos_Callback(null, null);
                            }
                        }
                        else
                        {
                            MImprimirMensaje(resultado.strMensaje, EnumTiposMensajeAlerta.Error, true);
                        }
                    }

                    #endregion
                }
                catch (Exception exp)
                {
                    #region RegistraError

                    MImprimirMensaje(exp.Message, EnumTiposMensajeAlerta.Error, false);
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

        protected void pucFormatoDj_WindowCallback(object source, PopupWindowCallbackArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    txtPfIdActividad.Text = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString().Trim();
                    txtPfActividad.Text = tlsOperadores.FocusedNode.GetValue("StrDescPadre").ToString().Trim();

                    cmbPfColumnas.Text = "";
                    cmbPfColumnas.DataSource = null;
                    cmbPfColumnas.DataBind();
                    txtPfIdColumna.Text = "";
                    txtPfColumna.Text = "";

                    var lst = (List<O_LISTA_REPORTE_VARIABLES_CTY>)Session[CVariablesSesion.ListaParamGral];
                    if (lst == null) return;
                    cmbPfColumnas.DataSource = lst.OrderBy(x => x.CODIGO);
                    cmbPfColumnas.DataBind();

                    #endregion
                }
                catch (Exception exp)
                {
                    #region RegistraError

                    MImprimirMensaje(exp.Message, EnumTiposMensajeAlerta.Error, false);
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

        protected void btnPfGrabar_Click(object sender, EventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar



                    decimal decEstado = 0, decIdIdentificador = 0;

                    //agrega padre
                    if (txtCtaId.Text == "0" && txtCtaPadre.Text.Trim().Length == 0)
                    {
                        decEstado = 1;
                    }
                    //modificacion
                    else if (txtCtaId.Text != "0" && txtCtaPadre.Text.Trim().Length == 0)
                    {

                    }
                    //agrega hijo
                    else if (txtCtaId.Text != "0" && txtCtaPadre.Text.Trim().Length>0)
                    {
                        var index = dgvCuentas.FocusedRowIndex;
                        if (index >= 0)
                        {
                            decEstado = 2;
                            decIdIdentificador = Convert.ToDecimal(txtCtaId.Text);//Convert.ToDecimal(dgvCuentas.GetRowValues(index, "ID_AUTORIZACION_CONDICION").ToString());
                        }
                    }
                    else
                    {
                        MImprimirMensaje("Parametros inválidos.",EnumTiposMensajeAlerta.Error,false);
                        return;
                    }
                    var Y = 2;
                    var objResultado = _client.Post<EResultado>(CAppSettings.svc_registra_autorizacion_condicion,
                    new
                    {
                        strCredencial = CParametrosHydro.strCredencialFuncionario,
                        decIdTipoActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()),
                        decIdConsumidor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString()),
                        decIdIdentificador = decIdIdentificador,
                        decIdSubIdentificador = 0,
                        decEstado = decEstado,
                        strObservaciones = "",
                        decTipoCondicion = 3,
                        strCodigo = txtCtaCodigo.Text,
                        strDescripcion = txtCtaDescripcion.Text,
                        decAppFechaRegistro = CFechas.ConvierteDateTimeLong(DateTime.Now),
                        decAppIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario])
                    });
                    if (objResultado.intCodigo != 1)
                    {
                        MImprimirMensaje(objResultado.strMensaje, EnumTiposMensajeAlerta.Error, true);
                    }

                    
                    //var objResultado = _client.Post<EResultado>(CAppSettings.svc_registra_autorizacion_condicion,
                    //new
                    //{
                    //    strCredencial = CParametrosHydro.strCredencialFuncionario,
                    //    decIdTipoActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()),
                    //    decIdConsumidor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString()),
                    //    decIdIdentificador = 0,
                    //    decIdSubIdentificador = 0,
                    //    decEstado = 1,
                    //    strObservaciones = "",
                    //    decTipoCondicion = 3,
                    //    strCodigo = "YPFB",
                    //    strDescripcion = "YPFB",
                    //    decAppFechaRegistro = CFechas.ConvierteDateTimeLong(DateTime.Now),
                    //    decAppIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario])
                    //});
                    //if (objResultado.intCodigo != 1)
                    //{
                    //    MImprimirMensaje(objResultado.strMensaje, EnumTiposMensajeAlerta.Error, true);
                    //}


                    //var objResultado = _client.Post<EResultado>(CAppSettings.svc_registra_autorizacion_condicion,
                    //new
                    //{
                    //    strCredencial = CParametrosHydro.strCredencialFuncionario,
                    //    decIdTipoActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()),
                    //    decIdConsumidor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString()),
                    //    decIdIdentificador = 16990,
                    //    decIdSubIdentificador = 0,
                    //    decEstado = 2,
                    //    strObservaciones = "",
                    //    decTipoCondicion = 3,
                    //    strCodigo = "YNAC",
                    //    strDescripcion = "YPFB NACIONAL",
                    //    decAppFechaRegistro = CFechas.ConvierteDateTimeLong(DateTime.Now),
                    //    decAppIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario])
                    //});
                    //if (objResultado.intCodigo != 1)
                    //{
                    //    MImprimirMensaje(objResultado.strMensaje, EnumTiposMensajeAlerta.Error, true);
                    //}

                    //objResultado = _client.Post<EResultado>(CAppSettings.svc_registra_autorizacion_condicion,
                    //new
                    //{
                    //    strCredencial = CParametrosHydro.strCredencialFuncionario,
                    //    decIdTipoActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()),
                    //    decIdConsumidor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString()),
                    //    decIdIdentificador = 16990,
                    //    decIdSubIdentificador = 0,
                    //    decEstado = 2,
                    //    strObservaciones = "",
                    //    decTipoCondicion = 3,
                    //    strCodigo = "YIMP",
                    //    strDescripcion = "YPFB IMPORTADO",
                    //    decAppFechaRegistro = CFechas.ConvierteDateTimeLong(DateTime.Now),
                    //    decAppIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario])
                    //});
                    //if (objResultado.intCodigo != 1)
                    //{
                    //    MImprimirMensaje(objResultado.strMensaje, EnumTiposMensajeAlerta.Error, true);
                    //}

                    //objResultado = _client.Post<EResultado>(CAppSettings.svc_registra_autorizacion_condicion,
                    //new
                    //{
                    //    strCredencial = CParametrosHydro.strCredencialFuncionario,
                    //    decIdTipoActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()),
                    //    decIdConsumidor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString()),
                    //    decIdIdentificador = 16990,
                    //    decIdSubIdentificador = 0,
                    //    decEstado = 2,
                    //    strObservaciones = "",
                    //    decTipoCondicion = 3,
                    //    strCodigo = "YINC",
                    //    strDescripcion = "YPFB INCAUTADO",
                    //    decAppFechaRegistro = CFechas.ConvierteDateTimeLong(DateTime.Now),
                    //    decAppIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario])
                    //});
                    //if (objResultado.intCodigo != 1)
                    //{
                    //    MImprimirMensaje(objResultado.strMensaje, EnumTiposMensajeAlerta.Error, true);
                    //}

                    





                    //var datFechaReg = DateTime.Now;
                    //var decFechaReg = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString().PadLeft(2, '0') + datFechaReg.Day.ToString().PadLeft(2, '0') + datFechaReg.Hour.ToString().PadLeft(2, '0') + datFechaReg.Minute.ToString().PadLeft(2, '0') + datFechaReg.Second.ToString().PadLeft(2, '0'));
                    //EResultado resultado = _client.Post<EResultado>(CAppSettings.svc_actualiza_reporte_variable,
                    //new
                    //{
                    //    strCredencial = CParametrosHydro.strCredencialFuncionario,
                    //    decIdReporteVariable = Convert.ToDecimal(txtPfIdColumna.Text),
                    //    strNombre = txtPfColumna.Text.Trim(),
                    //    decAppIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]),
                    //    decAppFechaRegistro = decFechaReg
                    //});
                    //if (resultado.intCodigo == 1)
                    //{
                    //    MCargaParamEspecifico(Convert.ToInt32(txtPfIdActividad.Text));
                    //    pucFormatoDj_WindowCallback(null, null);
                    //}
                    //else
                    //{
                    //    MImprimirMensaje(resultado.strMensaje, EnumTiposMensajeAlerta.Error, true);
                    //}

                    #endregion
                }
                catch (Exception exp)
                {
                    #region RegistraError

                    MLimpiaGrilla();
                    MImprimirMensaje(exp.Message, EnumTiposMensajeAlerta.Error, false);
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
        /// METODO. MCargaArbol
        /// </summary>
        private void MCargaArbol()
        {
            MLimpiaGrilla();
            var objArbol = Session[CVariablesSesion.ObjetoArbol] as List<O_OBTIENE_ENTIDADES_CTY>;
            if (objArbol == null)
            {
                decimal idUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                var objResultado = _consulta.ObtenerEntidades(
                new EObjetoEnvio5ParametrosProto
                {
                    decFiltro = 1,
                    strParametro1 = idUsuario.ToString(),
                    strParametro2 = CAppSettings.IdActividades,
                    strParametro3 = CAppSettings.idAplicacion,
                    strParametro4 = "",
                    strParametro5 = ""
                });
                if (objResultado.intCodigo == 1)
                {
                    objArbol = (List<O_OBTIENE_ENTIDADES_CTY>)objResultado.oResultado;
                    Session[CVariablesSesion.ObjetoArbol] = objArbol;
                }
                else
                {
                    MImprimirMensaje(objResultado.strMensaje, EnumTiposMensajeAlerta.Error, true);
                    return;
                }
            }

            Session[CVariablesSesion.EstructuraArbol] = _cUtil.MObtenerArbolOrdenado(0, objArbol);
        }

        /// <summary>
        /// METODO. MCargaParametricas
        /// </summary>
        private void MCargaParametricas()
        {
            var objTipoOperacion = _client.Post<EResultadoTipoOperacion>(CAppSettings.svc_obtener_tipos_operacion_act,
            new
            {
                decFiltro = 2,
                strParametro1 = "",
                strParametro2 = "",
                strParametro3 = ""
            });
            if (objTipoOperacion.IntCodigo == 1)
            {
                Session[CVariablesSesion.ListaParametrica] = objTipoOperacion.OResultado;
            }
            else
            {
                //todo No implementado.
            }
        }

        /// <summary>
        /// METODO. MCargaGrilla
        /// </summary>
        /// <param name="intIdActividad"></param>
        private void MCargaGrilla(int intIdActividad)
        {

            var objHidrocarburo = _client.Post<EResultadoConfigHidrocarburo>(CAppSettings.svc_listar_autorizacion_condicion,
            new
            {
                strCredencial = CParametrosHydro.strCredencialFuncionario,
                decFiltro = 5,
                strParametro1 = "",
                strParametro2 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
                strParametro3 = ""
            });
            if (objHidrocarburo.IntCodigo == 1)
            {
                objHidrocarburo.OResultado.ForEach(a => a.SUBID_NOMBRE = (a.ESTADO == 1) ? "PADRE" : "HIJO");
                dgvCuentas.DataSource = objHidrocarburo.OResultado;
                dgvCuentas.DataBind();

                //Session[CVariablesSesion.GrillaAcumRep] = objHidrocarburo.OResultado;
                Session[CVariablesSesion.GrillaTransaccion] = objHidrocarburo.OResultado;
            }
            else
            {
                MImprimirMensaje(objHidrocarburo.StrMensaje, EnumTiposMensajeAlerta.Error, false);
            }


            //var objTipoOperacion = _client.Post<EResultadoTipoOperacion>(CAppSettings.svc_obtener_tipos_operacion_act,
            //new
            //{
            //    decFiltro = 1,
            //    strParametro1 = intIdActividad,
            //    strParametro2 = "",
            //    strParametro3 = ""
            //});
            //if (objTipoOperacion.IntCodigo == 1)
            //{
            //    var lstTipoOperacion = objTipoOperacion.OResultado;
            //    lstTipoOperacion.ForEach(a => a.STRTIPO = ((a.FACTOR_CALCULO == 1) ? "INGRESO" : ((a.FACTOR_CALCULO == -1) ? "EGRESO" : "NEUTRO")));

            //    objTipoOperacion.OResultado = objTipoOperacion.OResultado.OrderByDescending(x => x.FACTOR_CALCULO).ThenBy(y => y.NOMBRE).ToList();
            //    dgvCuentas.DataSource = objTipoOperacion.OResultado;
            //    dgvCuentas.DataBind();
            //    Session[CVariablesSesion.GrillaTransaccion] = objTipoOperacion.OResultado;
            //}
            //else
            //{
            //    MImprimirMensaje(objTipoOperacion.StrMensaje, EnumTiposMensajeAlerta.Error, false);
            //}
        }

        /// <summary>
        /// METODO. MCargaParamEspecifico
        /// </summary>
        /// <param name="intIdActividad"></param>
        private void MCargaParamEspecifico(int intIdActividad)
        {
            Session.Remove(CVariablesSesion.ListaParamGral);
            var objReporteVariable = _client.Post<EResultadoReporteVariable>(CAppSettings.svc_listar_reporte_variable,
            new
            {
                strCredencial = CParametrosHydro.strCredencialFuncionario,
                decFiltro = 1,
                strParametro1 = "UD/AB-RDJ-P01-F01",
                strParametro2 = intIdActividad,
                strParametro3 = ""
            });
            if (objReporteVariable.IntCodigo == 1)
            {
                Session[CVariablesSesion.ListaParamGral] = objReporteVariable.OResultado;
            }
            else
            {
                MImprimirMensaje(MethodBase.GetCurrentMethod().Name + " - " + objReporteVariable.StrMensaje, EnumTiposMensajeAlerta.Error, false);
            }
        }
        
        /// <summary>
        /// METODO. MLimpiaGrilla
        /// </summary>
        private void MLimpiaGrilla()
        {
            Session.Remove(CVariablesSesion.GrillaTransaccion);
            dgvCuentas.DataSource = null;
            dgvCuentas.DataBind();
            pnlMensaje.Visible = false;
        }

        /// <summary>
        /// METODO. MImprimirMensaje
        /// </summary>
        /// <param name="strMensaje"></param>
        /// <param name="tipoMensaje"></param>
        /// <param name="booActivaJs"></param>
        private void MImprimirMensaje(string strMensaje, EnumTiposMensajeAlerta tipoMensaje, bool booActivaJs)
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
            lblMensajeError.Text = "   " + strMensaje + "   ";
            if (booActivaJs)
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError", "alert('" + strMensaje + "');", true);
        }

        #endregion

        protected void btnIniciaAct_Click(object sender, EventArgs e)
        {
            var detFechaActual = DateTime.Now;
            var decFechaActual = CFechas.ConvierteDateTimeLong(detFechaActual);

            for (int i = 1; i <= spnNoIngreso.Number; i++)
            {
                var registro = _gestion.RegistraReporteVariable(
                CParametrosHydro.strCredencialFuncionario,
                "UD/AB-RDJ-P01-F01",
                1,
                Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString().Trim()),
                i,
                "ING_0"+i,
                "ING_0" + i,
                "MOVIMIENTOS DECLARADOS COMO INGRESO A NIVEL GENERAL",
                Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]),
                decFechaActual
                );
            }
            for (int i = 1; i <= spnNoEgreso.Number; i++)
            {
                var registro = _gestion.RegistraReporteVariable(
                CParametrosHydro.strCredencialFuncionario,
                "UD/AB-RDJ-P01-F01",
                1,
                Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString().Trim()),
                i,
                "EGR_0" + i,
                "EGR_0" + i,
                "MOVIMIENTOS DECLARADOS COMO INGRESO A NIVEL GENERAL",
                Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]),
                decFechaActual
                );
            }

        }

        

    }

}