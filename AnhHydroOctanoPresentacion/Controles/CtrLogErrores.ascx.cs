
using AnhHydroOctanoPersistencia.Aplicacion.Gestion;
using Newtonsoft.Json;

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
    using Entidades;
    using Enumeradores;
    using Parametros;
    using DevExpress.Web;
    using Librerias.Anh.Us;
    using ServiceStack.ServiceClient.Web;
    using Page = System.Web.UI.Page;

    public partial class CtrLogErrores : UserControl
    {
        #region Variables de entorno

        readonly JsonServiceClient _client = new JsonServiceClient(CAppSettings.servicio_octVolumen);
        private readonly CPersistenciaConsulta _consulta = new CPersistenciaConsulta();
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
                        Session.Remove(CVariablesSesion.GrillaCabecera);
                        Session.Remove(CVariablesSesion.ListaTransaccion);
                        Session.Remove(CVariablesSesion.Aux);

                        MCargaLateral();
                        MCargaPermisos();
                        MConfigurarFormulario();
                    }
                    else
                    {
                        //todo No implementado.
                    }

                    if (dgvListado.DataSource == null && Session[CVariablesSesion.GrillaAuxiliar] != null)
                    {
                        dgvListado.DataSource = Session[CVariablesSesion.GrillaAuxiliar];
                        dgvListado.DataBind();
                    }
                    else
                    {
                        //todo no implementado
                    }
                    if (dgvCabecera.DataSource == null && Session[CVariablesSesion.GrillaCabecera] != null)
                    {
                        dgvCabecera.DataSource = Session[CVariablesSesion.GrillaCabecera];
                        dgvCabecera.DataBind();
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

        protected void pnlLateral_Callback(object sender, CallbackEventArgsBase e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                        MCargaLateral();

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

        protected void pnlDatos_Callback(object sender, CallbackEventArgsBase e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    MLimpiaGrilla();
                    int index = dgvListado.FocusedRowIndex;
                    if (index != -1)
                    {
                        var intFiltro = 2;
                        dgvCabecera.Columns[0].Visible = false;
                        dgvCabecera.Columns[8].Visible = false;
                        dgvCabecera.Columns[9].Visible = false;
                        if ((int) rbtTabla.Value == 2)
                        {
                            intFiltro = 4;
                            dgvCabecera.Columns[0].Visible = true;
                            dgvCabecera.Columns[8].Visible = true;
                            dgvCabecera.Columns[9].Visible = true;
                        }
                        
                        var strCod = dgvListado.GetRowValues(index, "CODIGO_ORACLE").ToString();
                        lblCodigo.Text = strCod;

                        var objResultado = _consulta.ListaErrorLog(
                        new EObjetoEnvio2ParametrosProto
                        {
                            strCredencial = CParametrosHydro.strCredencialFuncionario,
                            decFiltro = intFiltro,
                            strParametro1 = (strCod == "99999") ? "_" : strCod,
                            strParametro2 = spnNoRegistros.Number.ToString()
                        });
                        if (objResultado.intCodigo == 1)
                        {
                            var lstRegistro = (List<O_LISTA_LOG_ERROR_DET_CTY>)objResultado.oResultado;
                            
                            dgvCabecera.DataSource = lstRegistro;
                            dgvCabecera.DataBind();

                            Session[CVariablesSesion.GrillaCabecera] = lstRegistro;
                        }
                        else
                        {
                            MImprimirMensaje(objResultado.strMensaje, EnumTiposMensajeAlerta.Error);
                        }
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
        
        /*protected void dgvCabecera_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            try
            {
                #region Proceso a Ejecutar

                if (e.VisibleIndex >= 0)
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
        }*/

        protected void dgvListado_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            try
            {
                #region Codigo a controlar

                if (e.VisibleIndex >= 0)
                {
                    if (e.VisibleIndex % 2 == 1)
                    {
                        e.Row.BackColor = Color.FromArgb(241, 236, 221);
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

        protected void btnEmitir_Click(object sender, EventArgs e)
        {
            try
            {
                #region Proceso a Ejecutar

                dgeExportar.GridViewID = "dgvCabecera";
                dgeExportar.WriteXlsxToResponse();

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

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    int index = dgvListado.FocusedRowIndex;
                    if (index != -1)
                    {
                        var strCod = dgvListado.GetRowValues(index, "CODIGO_ORACLE").ToString();
                        lblCodigo.Text = strCod;
                        if (strCod == "99999")
                            return;

                        var resultado = _client.Post<EResultado>(CAppSettings.svc_elimina_log_errores,
                        new
                        {
                            strCredencial = CParametrosHydro.strCredencialFuncionario,
                            decFiltro = Convert.ToDecimal(rbtTabla.Value),
                            strCodigoOracle = lblCodigo.Text
                        });
                        if (resultado.intCodigo == 1)
                        {
                            MCargaLateral();
                        }
                        else
                        {
                            MImprimirMensaje(resultado.strMensaje, EnumTiposMensajeAlerta.Error);
                        }
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


        protected void pucRegistro_WindowCallback(object source, PopupWindowCallbackArgs e)
        {
            pnlRecepcion.Visible = false;
            pnlTransaccion.Visible = false;
            pnlTranAdic.Visible = false;

            var index = dgvCabecera.FocusedRowIndex;
            if (index == -1) return;

            var strJson = dgvCabecera.GetRowValues(index, "MENSAJE_PERSONALIZADO").ToString();
            var strMetodo = dgvCabecera.GetRowValues(index, "APLICACION").ToString();
            Session[CVariablesSesion.Aux] = strMetodo;
            switch (strMetodo)
            {
                case "PMOV_INTEGRACIONES.P_REGISTRA_RECEPCIONES_BSISA":
                    try
                    {
                        pnlRecepcion.Visible = true;
                        MParse(strJson);
                    }
                    catch (Exception)
                    {
                        txtCadena.Text = strJson;
                    }
                    break;
                case "PMOV_INTEGRACIONES.P_REGISTRA_TRANSACCION":
                    pnlTransaccion.Visible = true;
                    try
                    {
                        pnlTransaccion.Visible = true;
                        MParseTrans(strJson);
                    }
                    catch (Exception)
                    {
                        txtCadenaTr.Text = strJson;
                    }
                    break;
                case "PMOV_INTEGRACIONES.P_REGISTRA_TRANSACCION_ADIC":
                    pnlTranAdic.Visible = true;
                    try
                    {
                        pnlTranAdic.Visible = true;
                        MParseTransAd(strJson);
                    }
                    catch (Exception ex)
                    {
                        txtCadenaTa.Text = strJson;
                    }
                    break;
            }
        }

        protected void dgvCabecera_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    int index = dgvCabecera.FocusedRowIndex;
                    var decIdError = Convert.ToInt64(dgvCabecera.GetRowValues(index, "ID_ORACLE_ERROR"));
                    if (index >= 0)
                    {
                        switch (e.ButtonID)
                        {
                            case "btnGrdEliminar":

                                var resultado = _client.Post<EResultado>(CAppSettings.svc_elimina_log_errores,
                                new
                                {
                                    strCredencial = CParametrosHydro.strCredencialFuncionario,
                                    decFiltro = 2,//solo sincronizaciones
                                    strCodigoOracle = decIdError.ToString()
                                });
                                if (resultado.intCodigo == 1)
                                {
                                    MCargaLateral();
                                }
                                else
                                {
                                    MImprimirMensaje(resultado.strMensaje, EnumTiposMensajeAlerta.Error);
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

        protected void btnRegistroDir_Click(object sender, EventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar
                    var objResultado = _gestion.RegistraRecepcionBsisa(
                    Convert.ToDecimal(txt01.Text),
                    Convert.ToDecimal(txt02.Text),
                    Convert.ToDecimal(txt03.Text),
                    Convert.ToDecimal(txt04.Text),
                    Convert.ToDecimal(txt05.Text),
                    txt06.Text,
                    Convert.ToDecimal(txt07.Text),
                    Convert.ToDecimal(txt08.Text),
                    txt09.Text,
                    Convert.ToDecimal(txt10.Text),
                    Convert.ToDecimal(txt11.Text),
                    Convert.ToDecimal(txt12.Text),
                    txt13.Text,
                    txt14.Text,
                    txt15.Text,
                    txt16.Text,
                    txt17.Text,
                    txt18.Text,
                    txt19.Text,
                    Convert.ToDecimal(txt20.Text),
                    txt21.Text,
                    txt22.Text,
                    txt23.Text,
                    Convert.ToDecimal(txt24.Text),
                    Convert.ToDecimal(txt25.Text),
                    Convert.ToDecimal(txt26.Text),
                    Convert.ToDecimal(txt27.Text),
                    Convert.ToDecimal(txt28.Text),
                    Convert.ToDecimal(txt29.Text),
                    txt30.Text,
                    txt31.Text,
                    Convert.ToDecimal(txt32.Text),
                    Convert.ToDecimal(txt33.Text),
                    Convert.ToDecimal(txt34.Text),
                    ""
                    );
                        txtResultado.Text = objResultado.intCodigo + " - " + objResultado.strMensaje;
                        MCargaPopup();
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

        protected void btnRegistroDirTr_Click(object sender, EventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar
                    var objResultado = _gestion.RegistraTransaccionBsisa(
                    Convert.ToDecimal(txtTr01.Text),
                    Convert.ToDecimal(txtTr02.Text),
                    Convert.ToDecimal(txtTr03.Text),
                    Convert.ToDecimal(txtTr04.Text),
                    Convert.ToDecimal(txtTr05.Text),
                    Convert.ToDecimal(txtTr06.Text),
                    Convert.ToDecimal(txtTr07.Text),
                    txtTr08.Text,
                    Convert.ToDecimal(txtTr09.Text),
                    txtTr10.Text,
                    txtTr11.Text,
                    Convert.ToDecimal(txtTr12.Text),
                    Convert.ToDecimal(txtTr13.Text),
                    Convert.ToDecimal(txtTr14.Text),
                    txtTr15.Text,
                    txtTr16.Text,
                    Convert.ToDecimal(txtTr17.Text),
                    Convert.ToDecimal(txtTr18.Text),
                    Convert.ToDecimal(txtTr19.Text),
                    Convert.ToDecimal(txtTr20.Text),
                    Convert.ToDecimal(txtTr21.Text),
                    txtTr22.Text,
                    Convert.ToDecimal(txtTr23.Text),
                    Convert.ToDecimal(txtTr24.Text),
                    txtTr25.Text,
                    Convert.ToDecimal(txtTr26.Text),
                    Convert.ToDecimal(txtTr27.Text)
                    );
                        txtResultadoTr.Text = objResultado.intCodigo + " - " + objResultado.strMensaje;
                        MCargaPopup();
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

        protected void btnRegistroDirTa_Click(object sender, EventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar
                    var objResultado = _gestion.RegistraTransAdicBsisa(
                    Convert.ToDecimal(txtTa01.Text),
                    Convert.ToDecimal(txtTa02.Text),
                    Convert.ToDecimal(txtTa03.Text),
                    Convert.ToDecimal(txtTa04.Text),
                    Convert.ToDecimal(txtTa05.Text),
                    txtTa06.Text,
                    Convert.ToDecimal(txtTa07.Text),
                    Convert.ToDecimal(txtTa08.Text),
                    Convert.ToDecimal(txtTa09.Text),
                    Convert.ToDecimal(txtTa10.Text),
                    Convert.ToDecimal(txtTa11.Text),
                    txtTa12.Text,
                    Convert.ToDecimal(txtTa13.Text),
                    Convert.ToDecimal(txtTa14.Text),
                    txtTa15.Text,
                    Convert.ToDecimal(txtTa16.Text),
                    Convert.ToDecimal(txtTa17.Text),
                    txtTa18.Text
                    );
                        txtResultadoTa.Text = objResultado.intCodigo + " - " + objResultado.strMensaje;
                        MCargaPopup();
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

        protected void btnParse_Click(object sender, EventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar
                    MParse(txtCadena.Text);
                    MCargaPopup();
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

        protected void btnParseTa_Click(object sender, EventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar
                    MParseTransAd(txtCadenaTa.Text);
                    MCargaPopup();
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

        protected void btnParseTr_Click(object sender, EventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar
                    MParseTrans(txtCadenaTr.Text);
                    MCargaPopup();
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
                                                                      "/WFLogErrores.aspx?format=json");
                if (lstResultado.decCodigo == 1)
                {
                    if (lstResultado.oResultado != null && lstResultado.oResultado.Any())
                    {
                        Session[CVariablesSesion.PermisoBaja] = lstResultado.oResultado.FirstOrDefault().BAJAS;
                    }
                    else
                    {
                        Session[CVariablesSesion.PermisoBaja] = 0;
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
        /// METODO. MConfigurarFormulario
        /// </summary>
        private void MConfigurarFormulario()
        {
            btnEliminar.Visible = Convert.ToInt16(Session[CVariablesSesion.PermisoBaja]) != 0;
        }

        /// <summary>
        /// METODO. MCargaLateral
        /// </summary>
        private void MCargaLateral()
        {
            var intFiltro = 1;
            if ((int)rbtTabla.Value == 2)
                intFiltro = 3;

            var objResultado = _consulta.ListaErrorLog(
            new EObjetoEnvio2ParametrosProto
            {
                strCredencial = CParametrosHydro.strCredencialFuncionario,
                decFiltro = intFiltro,
                strParametro1 = "",
                strParametro2 = ""
            });
            if (objResultado.intCodigo == 1)
            {
                var lstRegistro = (List<O_LISTA_LOG_ERROR_CAB_CTY>)objResultado.oResultado;
                lstRegistro.Add(new O_LISTA_LOG_ERROR_CAB_CTY
                {
                    CODIGO_ORACLE = "99999",
                    MENSAJE_ORACLE = "_MOSTRAR TODO",
                    CANTIDAD = 0
                });
                lstRegistro = lstRegistro.OrderByDescending(x => Convert.ToDecimal(x.CODIGO_ORACLE)).ToList();

                dgvListado.DataSource = lstRegistro;
                dgvListado.DataBind();

                Session[CVariablesSesion.GrillaAuxiliar] = lstRegistro;
            }
            else
            {
                MImprimirMensaje(objResultado.strMensaje, EnumTiposMensajeAlerta.Error);
            }
        }

        /// <summary>
        /// METODO. MLimpiaGrilla
        /// </summary>
        private void MLimpiaGrilla()
        {
            Session.Remove(CVariablesSesion.GrillaCabecera);
            dgvCabecera.DataSource = null;
            dgvCabecera.DataBind();

            MImprimirMensaje("",EnumTiposMensajeAlerta.Limpiar);
        }

        /// <summary>
        /// METODO. MCargaPopup
        /// </summary>
        protected void MCargaPopup()
        {
            pnlRecepcion.Visible = false;
            pnlTransaccion.Visible = false;
            pnlTranAdic.Visible = false;
            switch (Session[CVariablesSesion.Aux].ToString())
            {
                case "PMOV_INTEGRACIONES.P_REGISTRA_RECEPCIONES_BSISA":
                    pnlRecepcion.Visible = true;
                    break;
                case "PMOV_INTEGRACIONES.P_REGISTRA_TRANSACCION":
                    pnlTransaccion.Visible = true;
                    break;
                case "PMOV_INTEGRACIONES.P_REGISTRA_TRANSACCION_ADIC":
                    pnlTranAdic.Visible = true;
                    break;
            }
        }

        /// <summary>
        /// METODO. MParse
        /// </summary>
        /// <param name="strJson"></param>
        private void MParse(string strJson)
        {
            var lstRegistros = JsonConvert.DeserializeObject<O_RECEPCIONES_BSISA>(strJson);

            txt01.Text = lstRegistros.ID_INTEGRACION.ToString();
            txt02.Text = lstRegistros.ID_CONSUMIDOR.ToString();
            txt03.Text = lstRegistros.ID_TIPO_ACTIVIDAD.ToString();
            txt04.Text = lstRegistros.ID_TIPO_MOVIMIENTO.ToString();
            txt05.Text = lstRegistros.FECHA_OPERACION.ToString();
            txt06.Text = lstRegistros.IDENTIFICADOR;
            txt07.Text = lstRegistros.ID_PRODUCTO_HYDRO.ToString();
            txt08.Text = lstRegistros.CANTIDAD.ToString();
            txt09.Text = lstRegistros.ID_TRANSACCION;
            txt10.Text = lstRegistros.BARCODE.ToString();
            txt11.Text = lstRegistros.ID_CODIGO_PAIS.ToString();
            txt12.Text = lstRegistros.FECHA_ENVIO.ToString();
            txt13.Text = lstRegistros.BINARIO_CONTROL;
            txt14.Text = lstRegistros.USUARIO;
            txt15.Text = lstRegistros.CI_USUARIO;
            txt16.Text = lstRegistros.COD_TANQUE;
            txt17.Text = lstRegistros.PRECINTOS;
            txt18.Text = lstRegistros.CONDUCTOR;
            txt19.Text = lstRegistros.CI_CONDUCTOR;
            txt20.Text = lstRegistros.FECHA_DESPACHO.ToString();
            txt21.Text = lstRegistros.NRO_DESPACHO.ToString();
            txt22.Text = lstRegistros.NRO_ORDEN.ToString();
            txt23.Text = lstRegistros.DESTINO;
            txt24.Text = lstRegistros.VOLUMEN_PLANTA.ToString();
            txt25.Text = lstRegistros.ID_TIPO_TRANSACCION.ToString();
            txt26.Text = lstRegistros.NIT_CONSUMIDOR.ToString();
            txt27.Text = lstRegistros.NIT_EMISOR.ToString();
            txt28.Text = lstRegistros.NRO_TRANSACCION.ToString();
            txt29.Text = lstRegistros.NRO_AUTORIZACION.ToString();
            txt30.Text = lstRegistros.CODIGO_CONTROL;
            txt31.Text = lstRegistros.RAZON_SOCIAL;
            txt32.Text = lstRegistros.PRECIO_UNITARIO.ToString();
            txt33.Text = lstRegistros.PRECIO_TOTAL.ToString();
            txt34.Text = lstRegistros.I_FECHA_TRANSACCION.ToString();
        }

        /// <summary>
        /// METODO. MParseTrans
        /// </summary>
        /// <param name="strJson"></param>
        private void MParseTrans(string strJson)
        {
            var lstRegistros = JsonConvert.DeserializeObject<O_TRANSACCION_BSISA>(strJson);

            txtTr01.Text = lstRegistros.I_ID_TRANSACCION_PADRE.ToString();
            txtTr02.Text = lstRegistros.I_ID_MOVIMIENTO.ToString();
            txtTr03.Text = lstRegistros.I_ID_TIPO_TRANSACCION.ToString();
            txtTr04.Text = lstRegistros.I_ID_ESTADO_TRANSACCION.ToString();
            txtTr05.Text = lstRegistros.I_FECHA_HORA_EMISION.ToString();
            txtTr06.Text = lstRegistros.I_NIT_CONSUMIDOR.ToString();
            txtTr07.Text = lstRegistros.I_NIT_EMISOR.ToString();
            txtTr08.Text = lstRegistros.I_NRO_TRANSACCION;
            txtTr09.Text = lstRegistros.I_NRO_AUTORIZACION.ToString();
            txtTr10.Text = lstRegistros.I_CODIGO_CONTROL;
            txtTr11.Text = lstRegistros.I_RAZON_SOCIAL;
            txtTr12.Text = lstRegistros.I_PRECIO_UNITARIO.ToString();
            txtTr13.Text = lstRegistros.I_PRECIO_TOTAL.ToString();
            txtTr14.Text = lstRegistros.I_CODIGO_SEGURIDAD.ToString();
            txtTr15.Text = lstRegistros.I_USUARIO_TRANSACCION;
            txtTr16.Text = lstRegistros.I_OBSERVACIONES;
            txtTr17.Text = lstRegistros.I_ID_TIPO_CREDITO_FISCAL.ToString();
            txtTr18.Text = lstRegistros.I_ID_FORMA_PAGO.ToString();
            txtTr19.Text = lstRegistros.I_ID_TIPO_MONEDA.ToString();
            txtTr20.Text = lstRegistros.I_ID_TIPO_EMISOR.ToString();
            txtTr21.Text = lstRegistros.I_ID_AUTORIZACION_DATO.ToString();
            txtTr22.Text = lstRegistros.I_BINARIO_CONTROL;
            txtTr23.Text = lstRegistros.I_ID_TIPO_ACTIVIDAD.ToString();
            txtTr24.Text = lstRegistros.I_FECHA_OPERACION.ToString();
            txtTr25.Text = lstRegistros.I_CODIGO;
            txtTr26.Text = lstRegistros.I_APP_ID_USUARIO.ToString();
            txtTr27.Text = lstRegistros.I_APP_FECHA_REGISTRO.ToString();
        }

        /// <summary>
        /// METODO. MParseTransAd
        /// </summary>
        /// <param name="strJson"></param>
        private void MParseTransAd(string strJson)
        {
            var lstRegistros = JsonConvert.DeserializeObject<O_TRANSACCION_ADIC_BSISA>(strJson);

            txtTa01.Text = lstRegistros.ID_MOVIMIENTO.ToString();
            txtTa02.Text = lstRegistros.ID_CONSUMIDOR.ToString();
            txtTa03.Text = lstRegistros.ID_INTEGRACION.ToString();
            txtTa04.Text = lstRegistros.ID_TIPO_FACTURA.ToString();
            txtTa05.Text = lstRegistros.NRO_TRANSACCION.ToString();
            txtTa06.Text = lstRegistros.NRO_AUTORIZACION.ToString();
            txtTa07.Text = lstRegistros.TIPO_CREDITO_FISCAL.ToString();
            txtTa08.Text = lstRegistros.PRECIO_UNITARIO.ToString();
            txtTa09.Text = lstRegistros.IMPORTE.ToString();
            txtTa10.Text = lstRegistros.ID_FORMA_PAGO.ToString();
            txtTa11.Text = lstRegistros.ID_TIPO_EMISOR.ToString();
            txtTa12.Text = lstRegistros.DETALLE_DOC;
            txtTa13.Text = lstRegistros.FECHA_TRANSACCION.ToString();
            txtTa14.Text = lstRegistros.FECHA_ENVIO.ToString();
            txtTa15.Text = lstRegistros.OBSERVACIONES;
            txtTa16.Text = lstRegistros.ID_TIPO_ACTIVIDAD.ToString();
            txtTa17.Text = lstRegistros.FECHA_OPERACION.ToString();
            txtTa18.Text = lstRegistros.I_CODIGO;
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