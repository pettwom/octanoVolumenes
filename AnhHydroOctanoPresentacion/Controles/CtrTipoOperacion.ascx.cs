
using AnhHydroOctanoPersistencia.Aplicacion.Gestion;
using AnhHydroOctanoPresentacion.Lib;
using DevExpress.Utils;
using DevExpress.Web.ASPxTreeList;

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

    public partial class CtrTipoOperacion : UserControl
    {
        #region Variables de entorno

        readonly JsonServiceClient _client = new JsonServiceClient(CAppSettings.servicio_octVolumen);
        private readonly CPersistenciaConsulta _consulta = new CPersistenciaConsulta();
        readonly CUtil _cUtil = new CUtil();
        private readonly CPersistenciaGestion _gestion = new CPersistenciaGestion();
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
                        Session.Remove(CVariablesSesion.GrillaTransaccion);
                        Session.Remove(CVariablesSesion.ListaParamGral);
                        Session.Remove(CVariablesSesion.ListaParametrica);
                        MCargaListaArbolActividades();
                        //MCargaArbol();
                        MCargaParametricas();
                        MCargaPermisos();
                        ConfigurarFormulario();
                        CargarListaActividadesHydro();
                    }
                    else
                    {
                        //todo No es carga inicial: no implementado
                    }

                    if (tlsOperadores.IsCallback)
                    {
                        MCargaListaArbolActividades();
                    }

                    //if (tlsOperadores.DataSource == null && Session[CVariablesSesion.EstructuraArbol] != null)
                    //{
                    //    tlsOperadores.DataSource = Session[CVariablesSesion.EstructuraArbol];
                    //    tlsOperadores.DataBind();
                    //}
                    //else
                    //{
                    //    //todo No es carga inicial: no implementado
                    //}

                    if (dgvTipoOperacion.DataSource == null && Session[CVariablesSesion.GrillaTransaccion] != null)
                    {
                        dgvTipoOperacion.DataSource = Session[CVariablesSesion.GrillaTransaccion];
                        dgvTipoOperacion.DataBind();
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
                    MCargaListaArbolActividades();

                    lblActividad.Text = tlsOperadores.FocusedNode.GetValue("DESCRIPCION").ToString();
                    MLimpiaGrilla();

                    var objArea = tlsOperadores.FocusedNode.DataItem;

                    int intIdActividad = Convert.ToInt32(tlsOperadores.FocusedNode.GetValue("ID_ACTIVIDAD"));
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

        protected void dgvTipoOperacion_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            try
            {
                #region Proceso a Ejecutar

                if (e.VisibleIndex >= 0)
                {
                    if (dgvTipoOperacion.GetRowValues(e.VisibleIndex, "AUD_ESTADO").ToString().Equals("3"))
                        e.Row.BackColor = Color.FromArgb(215, 135, 135);
                    else
                    {
                        /*if (e.VisibleIndex % 2 == 1)
                        {
                            e.Row.BackColor = Color.FromArgb(204, 209, 182);
                        }*/
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

        protected void dgvTipoOperacion_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    int index = dgvTipoOperacion.FocusedRowIndex;
                    long lngidventa = Convert.ToInt64(dgvTipoOperacion.GetRowValues(index, "ID_TIPO_OPERACION"));
                    if (lngidventa > 0)
                    {
                        if (e.ButtonID == "btnEliminar")
                        {
                            var resultado = _client.Post<EResultado>(CAppSettings.svc_eliminar_tipos_operacion,
                            new
                            {
                                decIdTipoOperacion = lngidventa
                            });
                            if (resultado.intCodigo == 1)
                            {
                                pnlDatos_Callback(null, null);
                            }
                            else
                            {
                                MImprimirMensaje(resultado.strMensaje, EnumTiposMensajeAlerta.Error, false);
                            }
                        }
                        if (e.ButtonID == "btnActivar")
                        {
                            var resultado = _client.Post<EResultado>(CAppSettings.svc_activar_tipos_operacion,
                            new
                            {
                                decIdTipoOperacion = lngidventa
                            });
                            if (resultado.intCodigo == 1)
                            {
                                pnlDatos_Callback(null, null);
                            }
                            else
                            {
                                MImprimirMensaje(resultado.strMensaje, EnumTiposMensajeAlerta.Error, false);
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

        protected void dgvTipoOperacion_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
        {
            if (!MValidaSesion()) return;
            try
            {
                #region Proceso a Ejecutar

                if (e.VisibleIndex == -1) return;

                var column = (GridViewCommandColumn)dgvTipoOperacion.Columns[0];
                var hlkNuevo = (dgvTipoOperacion.FindHeaderTemplateControl(column, "hlkNuevo")) as ASPxHyperLink;
                hlkNuevo.Visible = Convert.ToInt16(Session[CVariablesSesion.PermisoAlta]) == 1;
                if (Convert.ToInt16(Session[CVariablesSesion.PermisoBaja]) == 1)
                {
                    if (e.ButtonID == "btnEliminar")
                        e.Visible = DefaultBoolean.True;
                    if (e.ButtonID == "btnActivar")
                        e.Visible = DefaultBoolean.True;
                }
                else
                {
                    if (e.ButtonID == "btnEliminar")
                        e.Visible = DefaultBoolean.False;
                    if (e.ButtonID == "btnActivar")
                        e.Visible = DefaultBoolean.False;
                }
                if (Convert.ToInt16(Session[CVariablesSesion.PermisoModi]) == 1)
                {
                    if (e.ButtonID == "btnModificar")
                        e.Visible = DefaultBoolean.True;
                }
                else
                {
                    if (e.ButtonID == "btnModificar")
                        e.Visible = DefaultBoolean.False;
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

        protected void pucConfig_WindowCallback(object source, PopupWindowCallbackArgs e)
        {
            try
            {
                #region Proceso a Ejecutar

                cmbReporteVariable.Text = "";
                cmbReporteVariable.DataSource = null;
                cmbReporteVariable.DataBind();

                //if (txtId.Text == "1")
                //{
                int index = dgvTipoOperacion.FocusedRowIndex;
                if (index >= 0)
                {
                    txtId.Text = dgvTipoOperacion.GetRowValues(index, "ID_TIPO_OPERACION").ToString();
                    txtCodigo.Text = dgvTipoOperacion.GetRowValues(index, "CODIGO").ToString();
                    txtDescripcion.Text = dgvTipoOperacion.GetRowValues(index, "NOMBRE").ToString();
                    rbtTipo.Value = Convert.ToInt32(dgvTipoOperacion.GetRowValues(index, "FACTOR_CALCULO").ToString());
                    spnOrden.Number = Convert.ToInt32(dgvTipoOperacion.GetRowValues(index, "ORDEN").ToString());
                    txtLimiteMin.Text = dgvTipoOperacion.GetRowValues(index, "LIMITE_MINIMO").ToString();
                    txtLimiteMax.Text = dgvTipoOperacion.GetRowValues(index, "LIMITE_MAXIMO").ToString();
                    Session["idTipoOperacion"] = Convert.ToDecimal(dgvTipoOperacion.GetRowValues(index, "ID_TIPO_OPERACION"));
                    cmbReporteVariable_Callback(null, null);
                    //cmbReporteVariable.Value = cmbReporteVariable.Items.FindByValue(dgvTipoOperacion.GetRowValues(index, "ID_REPORTE_VARIABLE").ToString());
                }
                else
                {
                    btnGrabar.Enabled = false;
                }
                //}
                //else
                //{
                //    txtId.Text = "0";
                //    txtCodigo.Text = "";
                //    txtDescripcion.Text = "";
                //    rbtTipo.Value = 1;
                //    spnOrden.Number = 1;
                //    txtLimiteMin.Text = "";
                //    txtLimiteMax.Text = "";
                //    cmbReporteVariable.ReadOnly = true;
                //}

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
        protected void pucFormRegistroOperacionActividad_WindowCallback(object source, PopupWindowCallbackArgs e)
        {
            try
            {
                txtCodigoRegOperacionActividad.Text = "";
                txtDescripcionOperacionActividad.Text = "";
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
                    MCargaListaArbolActividades();
                    var LstPadreTipoOperacion = (List<O_TIPOS_OPE_ACT_CTY>)Session[CVariablesSesion.ListaParametrica];
                    LstPadreTipoOperacion = LstPadreTipoOperacion.Where(x => x.CODIGO == (((int)rbtTipo.Value == 1) ? "RECEP" : ((int)rbtTipo.Value == -1) ? "DESP" : "OPENEU")).ToList();
                    if (!LstPadreTipoOperacion.Any())
                    {
                        MImprimirMensaje("Incoherencia de datos. Paramétrica ID_TIPO_OPERACION_PADRE no definida.", EnumTiposMensajeAlerta.Error, true);
                    }
                    //else if (Convert.ToDecimal(txtLimiteMin.Text) > Convert.ToDecimal(txtLimiteMax.Text))
                    //{
                    //    MImprimirMensaje("Incoherencia de datos. Introduzca límites válidos.", EnumTiposMensajeAlerta.Error, true);
                    //}
                    else
                    {

                        decimal decIdTipoOperacion = (decimal)Session["idTipoOperacion"];
                        List<O_TIPOS_OPE_ACT_CTY> listaGrilla = (List<O_TIPOS_OPE_ACT_CTY>)Session[CVariablesSesion.GrillaTransaccion];
                        O_TIPOS_OPE_ACT_CTY objTipoOperacionActCty =
                            listaGrilla.FirstOrDefault(w => w.ID_TIPO_OPERACION == decIdTipoOperacion);

                        var datFechaReg = DateTime.Now;
                        var decFechaReg = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString().PadLeft(2, '0') + datFechaReg.Day.ToString().PadLeft(2, '0') + datFechaReg.Hour.ToString().PadLeft(2, '0') + datFechaReg.Minute.ToString().PadLeft(2, '0') + datFechaReg.Second.ToString().PadLeft(2, '0'));

                        EResultado resultado;

                        string strgLocalTipoEntrada = "";
                        if (Convert.ToDecimal(rbtTipo.Value) == 1)
                            strgLocalTipoEntrada = "ING";
                        else if (Convert.ToDecimal(rbtTipo.Value) == -1)
                            strgLocalTipoEntrada = "EGR";
                        else
                            strgLocalTipoEntrada = "NEU";

                        resultado = _client.Post<EResultado>(CAppSettings.svc_modifica_operacion_actividad,
                            new
                            {
                                strCredencial = CParametrosHydro.strCredencialFuncionario,
                                strgTipoEntrada = strgLocalTipoEntrada,
                                decIdTipoOperacion = objTipoOperacionActCty.ID_TIPO_OPERACION,
                                decIdOperacionVaiable = 0,
                                decIdReporteVaiable = objTipoOperacionActCty.ID_REPORTE_VARIABLE,
                                decIdOrdenOperacion = Convert.ToDecimal(spnOrden.Text),
                                strgCodigoOperacion = txtCodigo.Text,
                                strgNombreOperacion = txtDescripcion.Text,
                                decFechaRegistro = decFechaReg,
                                decAppIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]),
                                decIdTipoActividad = Convert.ToInt32(tlsOperadores.FocusedNode.GetValue("ID_ACTIVIDAD"))
                            });
                        pucConfig.ShowOnPageLoad = false;
                        if (resultado.intCodigo == 1)
                        {
                            MImprimirMensaje(resultado.strMensaje, EnumTiposMensajeAlerta.Correcto, true);

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

                    txtPfIdActividad.Text = tlsOperadores.FocusedNode.GetValue("ID_ACTIVIDAD").ToString().Trim();
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

                    var datFechaReg = DateTime.Now;
                    var decFechaReg = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString().PadLeft(2, '0') + datFechaReg.Day.ToString().PadLeft(2, '0') + datFechaReg.Hour.ToString().PadLeft(2, '0') + datFechaReg.Minute.ToString().PadLeft(2, '0') + datFechaReg.Second.ToString().PadLeft(2, '0'));
                    EResultado resultado = _client.Post<EResultado>(CAppSettings.svc_actualiza_reporte_variable,
                    new
                    {
                        strCredencial = CParametrosHydro.strCredencialFuncionario,
                        decIdReporteVariable = Convert.ToDecimal(txtPfIdColumna.Text),
                        strNombre = txtPfColumna.Text.Trim(),
                        decAppIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]),
                        decAppFechaRegistro = decFechaReg
                    });
                    if (resultado.intCodigo == 1)
                    {
                        MCargaParamEspecifico(Convert.ToInt32(txtPfIdActividad.Text));
                        pucFormatoDj_WindowCallback(null, null);
                    }
                    else
                    {
                        MImprimirMensaje(resultado.strMensaje, EnumTiposMensajeAlerta.Error, true);
                    }

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

        protected void btnIniciaAct_Click(object sender, EventArgs e)
        {
            var detFechaActual = DateTime.Now;
            var decFechaActual = CFechas.ConvierteDateTimeLong(detFechaActual);

            MCargaListaArbolActividades();
            for (int i = 1; i <= spnNoIngreso.Number; i++)
            {
                var registro = _gestion.RegistraReporteVariable(
                CParametrosHydro.strCredencialFuncionario,
                "UD/AB-RDJ-P01-F01",
                1,
                Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("ID_ACTIVIDAD").ToString().Trim()),
                i,
                "ING_0" + i,
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
                Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("ID_ACTIVIDAD").ToString().Trim()),
                i,
                "EGR_0" + i,
                "EGR_0" + i,
                "MOVIMIENTOS DECLARADOS COMO EGRESO A NIVEL GENERAL",
                Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]),
                decFechaActual
                );
            }
        }
        /// <summary>
        /// TODO:Evendo de baja de actividad
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkEliminarItemGrilla_Click(object sender, EventArgs e)
        {
            try
            {
                decimal decIdTipoNormativa = Convert.ToDecimal(tlsOperadores.FocusedNode.Key);
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalTramiteTipoModifica", "$('#modalTramiteTipoModifica').modal();", true);
            }
            catch (Exception ex)
            {
                CLog.Error(this.Context, GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }
        protected void tlsOperadores_CustomCallback(object sender, DevExpress.Web.ASPxTreeList.TreeListCustomCallbackEventArgs e)
        {
            try
            {
                Session["objResultado"] = null;
                if (e.Argument != "")
                {
                    EResultado resultado;
                    string[] accion = e.Argument.Split('-');

                    if (accion[0] == "Eliminar")
                    {
                        List<O_ACTIVIDA_OCT_VOL_CTY> listaActividadesHydro =
                            Session["listaSessionActividaesHydro"] != null
                                ? (List<O_ACTIVIDA_OCT_VOL_CTY>)Session["listaSessionActividaesHydro"]
                                : new List<O_ACTIVIDA_OCT_VOL_CTY>();
                        O_ACTIVIDA_OCT_VOL_CTY objstoActividadOctVol = listaActividadesHydro.FirstOrDefault(w => w.ID_ACTIVIDAD == Convert.ToDecimal(accion[1]));

                        resultado = _client.Post<EResultado>(CAppSettings.svc_EliminarEstActividadOctVolum,
                            new
                            {
                                strCredencial = CParametrosHydro.strCredencialOctanoVolumen,
                                decIdActividadOctVol = objstoActividadOctVol.ID_ACTIVIDAD_OCT_VOL,
                                decAppIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario])
                            });
                        Session["objResultado"] = resultado;

                        //MImprimirMensaje(resultado.strMensaje, EnumTiposMensajeAlerta.Correcto, true);
                    }
                    if (accion[0] == "Modificar")
                    {
                        List<O_ACTIVIDA_OCT_VOL_CTY> listaActividadesHydro =
                            Session["listaSessionActividaesHydro"] != null
                                ? (List<O_ACTIVIDA_OCT_VOL_CTY>)Session["listaSessionActividaesHydro"]
                                : new List<O_ACTIVIDA_OCT_VOL_CTY>();
                        O_ACTIVIDA_OCT_VOL_CTY objstoActividadOctVol = listaActividadesHydro.FirstOrDefault(w => w.ID_ACTIVIDAD == Convert.ToDecimal(accion[1]));
                        resultado = _client.Post<EResultado>(CAppSettings.svc_ModificarEstActividadOctVolum,
                            new
                            {
                                strCredencial = CParametrosHydro.strCredencialOctanoVolumen,
                                decIdActividadOctVol = objstoActividadOctVol.ID_ACTIVIDAD_OCT_VOL,
                                decAppIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]),
                                decIDEstado = (objstoActividadOctVol.ID_ESTADO == 0 ? 1 : 0)
                            });
                        Session["objResultado"] = resultado;

                        //MImprimirMensaje(resultado.strMensaje, EnumTiposMensajeAlerta.Correcto, true);
                    }
                    if (accion[0] == "Agregar")
                    {
                        resultado = _client.Post<EResultado>(CAppSettings.svc_RegistroActividadOctVolum,
                            new
                            {
                                strCredencial = CParametrosHydro.strCredencialOctanoVolumen,
                                decIdActividad = Convert.ToDecimal(cmbListaActividadesHydro.Value),
                                decAppIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario])
                            });
                        Session["objResultado"] = resultado;
                        //MImprimirMensaje(resultado.strMensaje, EnumTiposMensajeAlerta.Correcto, true);
                        //throw new Exception(resultado.strMensaje);
                        //throw new Exception("SaveError", new Exception(resultado.strMensaje));
                        //throw new Exception();
                    }

                }

                MCargaListaArbolActividades();
                //decimal decIdTipoNormativa = Convert.ToDecimal(tlsOperadores.FocusedNode.Key);


                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalTramiteTipoModifica", "$('#modalTramiteTipoModifica').modal();", true);
            }
            catch (Exception ex)
            {
                CLog.Error(this.Context, GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }
        protected void tlsOperadores_CustomErrorText(object sender, ASPxGridViewCustomErrorTextEventArgs e)
        {

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
                                                                      "/WFTipoOperacion.aspx?format=json");
                if (lstResultado.decCodigo == 1)
                {
                    if (lstResultado.oResultado != null && lstResultado.oResultado.Any())
                    {
                        Session[CVariablesSesion.PermisoAlta] = lstResultado.oResultado.FirstOrDefault().ALTAS;
                        Session[CVariablesSesion.PermisoBaja] = lstResultado.oResultado.FirstOrDefault().BAJAS;
                        Session[CVariablesSesion.PermisoModi] = lstResultado.oResultado.FirstOrDefault().MODIFICACIONES;
                        Session[CVariablesSesion.PermisoApro] = lstResultado.oResultado.FirstOrDefault().APROBACION;
                    }
                    else
                    {
                        Session[CVariablesSesion.PermisoAlta] = 0;
                        Session[CVariablesSesion.PermisoBaja] = 0;
                        Session[CVariablesSesion.PermisoModi] = 0;
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
        /// METODO. ConfigurarFormulario
        /// </summary>
        private void ConfigurarFormulario()
        {
            if (Convert.ToInt16(Session[CVariablesSesion.PermisoApro]) != 1)
            {
                btnFormatoDj.Visible = false;
            }
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
                    strParametro2 = cConsultasParametricas.ObtieneIdsActividadOctanioVol(),
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
            var objTipoOperacion = _client.Post<EResultadoTipoOperacion>(CAppSettings.svc_obtener_tipos_operacion_act,
            new
            {
                decFiltro = 4,
                strParametro1 = intIdActividad,
                strParametro2 = "",
                strParametro3 = ""
            });
            if (objTipoOperacion.IntCodigo == 1)
            {
                var lstTipoOperacion = objTipoOperacion.OResultado;
                lstTipoOperacion.ForEach(a => a.STRTIPO = ((a.FACTOR_CALCULO == 1) ? "INGRESO" : ((a.FACTOR_CALCULO == -1) ? "EGRESO" : "NEUTRO")));

                objTipoOperacion.OResultado = objTipoOperacion.OResultado.OrderByDescending(x => x.FACTOR_CALCULO).ThenBy(y => y.NOMBRE).ToList();
                dgvTipoOperacion.DataSource = objTipoOperacion.OResultado;
                dgvTipoOperacion.DataBind();
                Session[CVariablesSesion.GrillaTransaccion] = objTipoOperacion.OResultado;
            }
            else
            {
                MImprimirMensaje(objTipoOperacion.StrMensaje, EnumTiposMensajeAlerta.Error, false);
            }
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
            dgvTipoOperacion.DataSource = null;
            dgvTipoOperacion.DataBind();
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

        /// <summary>
        /// TODO:. CARGA COMBO LISTA ACTIVIDADES HYDRO
        /// </summary>

        private void CargarListaActividadesHydro()
        {
            var objReporteVariable = _client.Post<EResultadoLista<O_ACTIVIDA_CTY>>(CAppSettings.svc_ListaActividadeshydro,
                new
                {
                    strCredencial = CParametrosHydro.strCredencialOctanoVolumen
                });
            List<O_ACTIVIDA_CTY> listaActividadesHydro = new List<O_ACTIVIDA_CTY>();
            if (objReporteVariable.oResultado != null)
            {
                listaActividadesHydro = objReporteVariable.oResultado;
            }

            cmbListaActividadesHydro.DataSource = listaActividadesHydro;
            cmbListaActividadesHydro.DataBind();
        }

        /// <summary>
        /// METODO. MCargaArbol
        /// </summary>
        private void MCargaListaArbolActividades()
        {
            var objReporteVariable = _client.Post<EResultadoLista<O_ACTIVIDA_OCT_VOL_CTY>>(CAppSettings.svc_ListaActividadesOctanoVolumenes,
                new
                {
                    strCredencial = CParametrosHydro.strCredencialOctanoVolumen,
                    decIdActividOctVol = -1,
                    decIdActividad = -1,
                    decIdEstado = -1
                });
            List<O_ACTIVIDA_OCT_VOL_CTY> listaActividadesHydro = objReporteVariable.oResultado != null ? objReporteVariable.oResultado : new List<O_ACTIVIDA_OCT_VOL_CTY>();

            Session["listaSessionActividaesHydro"] = listaActividadesHydro;
            tlsOperadores.DataSource = listaActividadesHydro.OrderBy(w => w.DESCRIPCION);
            tlsOperadores.DataBind();
        }

        #endregion

        protected void tlsOperadores_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxTreeList.TreeListHtmlDataCellEventArgs e)
        {
            if (e.Column.FieldName == "DESCRIPCION")
            {
                TreeListNode node = tlsOperadores.FindNodeByKeyValue(e.NodeKey);
                if (node["ID_ESTADO"].ToString() == "0")
                    e.Cell.BackColor = System.Drawing.Color.LightCoral;
            }
        }
        protected void panelspinEdit_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                CLog.Error(this.Context, GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }
        protected void btnRegistrarOperacionActividad_Click(object sender, EventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar
                    MCargaListaArbolActividades();

                    var LstPadreTipoOperacion = (List<O_TIPOS_OPE_ACT_CTY>)Session[CVariablesSesion.ListaParametrica];
                    LstPadreTipoOperacion = LstPadreTipoOperacion.Where(x => x.CODIGO == (((int)rbtTipo.Value == 1) ? "RECEP" : ((int)rbtTipo.Value == -1) ? "DESP" : "OPENEU")).ToList();
                    if (!LstPadreTipoOperacion.Any())
                    {
                        MImprimirMensaje("Incoherencia de datos. Paramétrica ID_TIPO_OPERACION_PADRE no definida.", EnumTiposMensajeAlerta.Error, true);
                    }
                    else
                    {
                        var datFechaReg = DateTime.Now;
                        var decFechaReg = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString().PadLeft(2, '0') + datFechaReg.Day.ToString().PadLeft(2, '0') + datFechaReg.Hour.ToString().PadLeft(2, '0') + datFechaReg.Minute.ToString().PadLeft(2, '0') + datFechaReg.Second.ToString().PadLeft(2, '0'));

                        string strgLocalTiupoEntrada = "";
                        if (Convert.ToDecimal(rbRegOpActivTipoMovimiento.Value) == 1)
                            strgLocalTiupoEntrada = "ING";
                        else if (Convert.ToDecimal(rbRegOpActivTipoMovimiento.Value) == -1)
                            strgLocalTiupoEntrada = "EGR";
                        else
                            strgLocalTiupoEntrada = "NEU";


                        EResultado resultado;
                        resultado = _client.Post<EResultado>(CAppSettings.svc_registro_operacion_actividad,
                            new
                            {
                                strCredencial = CParametrosHydro.strCredencialOctanoVolumen,
                                strgObservaciones = txtTextoNomFormuario.Text,
                                decIdReportePrincipal = 1,
                                decIdTipoActividad = Convert.ToInt32(tlsOperadores.FocusedNode.GetValue("ID_ACTIVIDAD")),
                                strgNombre = "",
                                decAppIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]),
                                decFechaRegistro = decFechaReg,
                                strgTiupoEntrada = strgLocalTiupoEntrada, //todo:limite
                                strgCodigo = txtCodigoRegOperacionActividad.Text,
                                strgDescripcionTipoOperacion = txtDescripcionOperacionActividad.Text,
                                decFactorCalculo = rbRegOpActivTipoMovimiento.Value,
                                decIdTipoOperacionPadre = LstPadreTipoOperacion.FirstOrDefault().ID_TIPO_OPERACION,
                                decLimiteMaximo = 0,
                                decLimiteMinimo = 0,
                                strgNombreOperacion = txtDescripcionOperacionActividad.Text,
                                decOrden = Convert.ToDecimal(spRegisOpeActivListaOrden.Text),
                                strgCodigoReporte = txtTextoNomFormuario.Text,
                                strgCodigoVariable = "",
                                decEstadoReporte = 1
                            });
                    }
                    pucFormRegistroOperacionActividad.ShowOnPageLoad = false;
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


        protected void panelBusquedaGlobals_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            panelAlertaMensaje.Visible = false;
            panelErro.Visible = false;
            if (Session["objResultado"] != null)
            {
                EResultado objResultCty = (EResultado)Session["objResultado"];
                if (objResultCty.intCodigo > 0)
                {
                    lblErrorActividad.Text = objResultCty.strMensaje;
                    panelAlertaMensaje.Visible = true;
                }
                else
                {
                    lblAlertErro.Text = objResultCty.strMensaje;
                    panelErro.Visible = true;
                }
            }

            Session["objResultado"] = null;
        }
    }

}