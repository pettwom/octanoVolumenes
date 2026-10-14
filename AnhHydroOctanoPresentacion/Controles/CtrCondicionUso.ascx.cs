
using AnhHydroOctanoPresentacion.Lib;
using ServiceStack.Common;

namespace AnhHydroOctanoPresentacion.Controles
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Reflection;
    using System.Web;
    using System.Web.Script.Serialization;
    using System.Web.UI;
    using AnhHydroOctanoPersistencia.Aplicacion.Consulta;
    using AnhHydroOctanoPersistencia.Aplicacion.Gestion;
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

    public partial class CtrCondicionUso : UserControl
    {
        #region Variables de entorno

        readonly JsonServiceClient _client = new JsonServiceClient(CAppSettings.servicio_octVolumen);
        readonly JsonServiceClient _clientHidro = new JsonServiceClient(CAppSettings.servicio_sireHidro);
        private readonly CPersistenciaConsulta _consulta = new CPersistenciaConsulta();
        private readonly CPersistenciaGestion _gestion = new CPersistenciaGestion();
        readonly CUtil _cUtil = new CUtil();
        CConsultasParametricas cConsultasParametricas = new CConsultasParametricas();
        #endregion

        #region Eventos de Formulario

        protected void Page_Load(object sender, EventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    //if (panelDatosValidacion.IsCallback)
                    //{

                    //}
                    //else
                    //{
                    #region Proceso a Ejecutar

                    if (!Page.IsCallback && !Page.IsPostBack)
                    {
                        Session.Remove(CVariablesSesion.AuxValidaLlave);
                        Session.Remove(CVariablesSesion.EstructuraArbol);
                        Session.Remove(CVariablesSesion.GrillaAcumRep);
                        Session.Remove(CVariablesSesion.GrillaAcumulado);
                        Session.Remove(CVariablesSesion.GrillaAuxiliar);
                        Session.Remove(CVariablesSesion.GrillaTransaccion);
                        Session.Remove(CVariablesSesion.GrillaIntimacion);
                        Session.Remove(CVariablesSesion.ListaParametrica);
                        Session.Remove(CVariablesSesion.ObtenerEntidades);
                        Session.Remove(CVariablesSesion.ObjetoEntidad);

                        MCargaArbol();
                        MCargaParametricas();

                        tlsOperadores.DataSource = Session[CVariablesSesion.EstructuraArbol];
                        tlsOperadores.DataBind();

                        Session[CVariablesSesion.AuxValidaLlave] = 0;

                        MCargaPermisos();
                        MConfigurarFormulario();
                        chkAccion.Checked = true;
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

                    if (dgvEntidades.DataSource == null && Session[CVariablesSesion.GrillaTransaccion] != null)
                    {
                        dgvEntidades.DataSource = Session[CVariablesSesion.GrillaTransaccion];
                        dgvEntidades.DataBind();
                    }
                    else
                    {
                        //todo No es carga inicial: no implementado
                    }

                    if (dgvCuentas.DataSource == null && Session[CVariablesSesion.GrillaIntimacion] != null)
                    {
                        dgvCuentas.DataSource = Session[CVariablesSesion.GrillaIntimacion];
                        dgvCuentas.DataBind();
                    }
                    else
                    {
                        //todo No es carga inicial: no implementado
                    }

                    if (dgvTipoOperacion.IsCallback)
                    {
                        dgvTipoOperacion.DataSource = Session[CVariablesSesion.GrillaAcumulado] != null ? (List<O_LISTA_AUTORIZACION_ACCI_CTY>)Session[CVariablesSesion.GrillaAcumulado] : new List<O_LISTA_AUTORIZACION_ACCI_CTY>();
                        dgvTipoOperacion.DataBind();
                    }
                    if (dgvHidrocarburo.IsCallback)
                    {
                        dgvHidrocarburo.DataSource = Session[CVariablesSesion.GrillaAcumRep] != null ? (List<O_LISTA_AUTORIZACION_COND_CTY>)Session[CVariablesSesion.GrillaAcumRep] : new List<O_LISTA_AUTORIZACION_COND_CTY>();
                        dgvHidrocarburo.DataBind();
                    }
                    if (dgvEntidad.IsCallback)
                    {

                        dgvEntidad.DataSource = Session[CVariablesSesion.GrillaAuxiliar] != null ? (List<O_LISTA_AUTORIZACION_COND_CTY>)Session[CVariablesSesion.GrillaAuxiliar] : new List<O_LISTA_AUTORIZACION_COND_CTY>();
                        dgvEntidad.DataBind();
                    }
                    #endregion
                    //}


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
                    lblDepartamento.Text = tlsOperadores.FocusedNode.GetValue("StrDepartamento").ToString();
                    MLimpiaGrilla();

                    int intDato1 = (int)tlsOperadores.FocusedNode.GetValue("IntIdActividad");
                    string strDato2 = tlsOperadores.FocusedNode.GetValue("StrDepartamento").ToString();
                    int intDato3 = (int)tlsOperadores.FocusedNode.GetValue("IntIdOperador");
                    int intDato4 = (int)tlsOperadores.FocusedNode.GetValue("IntAccion");

                    MCargaGrilla(intDato1, strDato2, intDato3, intDato4);

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

        /* protected void dgvEntidades_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
         {
             try
             {
                 #region Proceso a Ejecutar

                 if (e.VisibleIndex >= 0)
                 {
                     if (e.VisibleIndex%2 == 1)
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
         }*/

        protected void pucConfig_WindowCallback(object source, PopupWindowCallbackArgs e)
        {
            try
            {
                #region Proceso a Ejecutar

                MLimpiaFolmulario();

                if (tlsOperadores.FocusedNode != null)
                {
                    var objParametricas = (O_PARAMETRICAS_GRAL)Session[CVariablesSesion.ListaParametrica];
                    cmbHidrocarburo.DataSource = objParametricas.lstHidrocarburo;
                    cmbHidrocarburo.DataBind();
                    cmbHidrocarburo.SelectedItem = cmbHidrocarburo.Items.FindByValue("-99999");
                    cmbUnidadMedida.DataSource = objParametricas.lstUnidadMedida;
                    cmbUnidadMedida.DataBind();
                    cmbUnidadMedida.SelectedItem = cmbUnidadMedida.Items.FindByValue("-99999");
                    cmbUnidadMedidaAux.DataSource = objParametricas.lstUnidadMedida;
                    cmbUnidadMedidaAux.DataBind();
                    cmbUnidadMedidaAux.SelectedItem = cmbUnidadMedidaAux.Items.FindByValue("-99999");

                    txtActividad.Text = tlsOperadores.FocusedNode.GetValue("StrDescPadre").ToString();
                    pnlGeneral.Visible = true;

                    string strActividades = "";
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

                    if (Convert.ToInt64(txtIdConsumidor.Text) != 0)
                    {
                        int index = dgvEntidades.FocusedRowIndex;
                        txtIdConsumidor.Text = dgvEntidades.GetRowValues(index, "ID_CONSUMIDOR").ToString();
                        txtIdEntidad.Text = dgvEntidades.GetRowValues(index, "ID_ENTIDAD").ToString();
                        txtDepto.Text = dgvEntidades.GetRowValues(index, "DEPARTAMENTO").ToString();
                        pnlGeneral.Visible = false;
                        pgcOpciones.TabPages[3].Visible = false;
                        if (objParametros.IntCodigo == 1 && objParametros.OResultado.Where(x => x.TIPO_VALOR == 9).ToList().FirstOrDefault().VALOR_FIN != null)
                        {
                            strActividades = objParametros.OResultado.Where(x => x.TIPO_VALOR == 9).ToList().FirstOrDefault().VALOR_FIN.Trim();
                        }
                    }
                    else
                    {
                        pgcOpciones.TabPages[3].Visible = true;
                        txtDepto.Text = tlsOperadores.FocusedNode.GetValue("StrDepartamento").ToString();


                        if (objParametros.IntCodigo == 1)
                        {
                            var objParam = objParametros.OResultado;
                            foreach (var param in objParam)
                            {
                                switch ((int)param.TIPO_VALOR)
                                {
                                    case (int)EnumOctanoParametros.DiasHabilitacion:
                                        spnPeriodo.Number = Convert.ToInt32(param.VALOR_FIN);
                                        break;
                                    case (int)EnumOctanoParametros.Perodicidad:
                                        cmbPeriodicidad.Value = param.VALOR_FIN.Trim();
                                        break;
                                    case (int)EnumOctanoParametros.DiasAmpliacion:
                                        spnApertura.Number = Convert.ToInt32(param.VALOR_FIN);
                                        break;
                                    case (int)EnumOctanoParametros.SoloDiasHabiles:
                                        chkDiaHabil.Value = Convert.ToInt32(param.VALOR_FIN);
                                        break;
                                    case (int)EnumOctanoParametros.RequisitoPdf:
                                        chkDigital.Value = Convert.ToInt32(param.VALOR_FIN);
                                        break;
                                    case (int)EnumOctanoParametros.NoIntimacion:
                                        chkIntimacion.Value = Convert.ToInt32(param.VALOR_FIN);
                                        break;
                                    case (int)EnumOctanoParametros.TipoSaldo:
                                        cmbTipoSaldo.Value = Convert.ToInt32(param.VALOR_FIN);
                                        break;
                                    case (int)EnumOctanoParametros.TextoCabecera:
                                        txtCabeceraDj.Text = param.VALOR_FIN.Trim();
                                        break;
                                    case (int)EnumOctanoParametros.ActividadRel:
                                        txtActRel.Text = (param.VALOR_FIN.IsNullOrEmpty()) ? "" : param.VALOR_FIN.Trim();
                                        strActividades = txtActRel.Text;
                                        break;
                                    case (int)EnumOctanoParametros.TipoImpresion:
                                        chkTipoImpresion.Value = Convert.ToInt32(param.VALOR_FIN);
                                        break;
                                }
                            }

                            //CUENTAS
                            var objCuenta = _client.Post<EResultadoConfigHidrocarburo>(CAppSettings.svc_listar_autorizacion_condicion,
                            new
                            {
                                strCredencial = CParametrosHydro.strCredencialFuncionario,
                                decFiltro = 5,
                                strParametro1 = "",
                                strParametro2 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
                                strParametro3 = ""
                            });
                            if (objCuenta.IntCodigo == 1)
                            {
                                objCuenta.OResultado.ForEach(a => a.SUBID_NOMBRE = (a.ESTADO == 1) ? "PADRE" : "HIJO");
                                dgvCuentas.DataSource = objCuenta.OResultado;
                                dgvCuentas.DataBind();

                                Session[CVariablesSesion.GrillaIntimacion] = objCuenta.OResultado;
                            }
                        }
                        else
                        {
                            //todo No implementado
                        }
                    }

                    //TIPOS DE OPERACION
                    var objTipoOperacion = _client.Post<EResultadoConfigTipoOperacion>(CAppSettings.svc_listar_autorizacion_accion,
                        new
                        {
                            strCredencial = CParametrosHydro.strCredencialFuncionario,
                            decFiltro = 1,
                            strParametro1 = txtIdConsumidor.Text.Trim(),
                            strParametro2 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
                            strParametro3 = "",
                            strParametro4 = ""
                        });
                    if (objTipoOperacion.IntCodigo == 1)
                    {
                        objTipoOperacion.OResultado.ForEach(a => a.decMasivo = Convert.ToDecimal(a.BINARIO_ESTADO.Substring(0, 1)));
                        objTipoOperacion.OResultado.ForEach(a => a.decFuncion = Convert.ToDecimal(a.BINARIO_ESTADO.Substring(1, 1)));
                        objTipoOperacion.OResultado.ForEach(a => a.decCumpDiario = Convert.ToDecimal(a.BINARIO_ESTADO.Substring(2, 1)));
                        objTipoOperacion.OResultado.ForEach(a => a.decExcluye = 0);
                        objTipoOperacion.OResultado.ForEach(a => a.strTipo = ((a.FACTOR_CALCULO == 1) ? "ING." : ((a.FACTOR_CALCULO == -1) ? "EGR." : "NEU.")));
                        objTipoOperacion.OResultado = objTipoOperacion.OResultado.OrderByDescending(a => a.FACTOR_CALCULO).ToList();
                        dgvTipoOperacion.DataSource = objTipoOperacion.OResultado;
                        dgvTipoOperacion.DataBind();
                        Session[CVariablesSesion.GrillaAcumulado] = objTipoOperacion.OResultado;
                    }
                    else
                    {
                        MImprimirMensaje(objTipoOperacion.StrMensaje, EnumTiposMensajeAlerta.Error, true);
                    }

                    //HIDROCARBUROS
                    var objHidrocarburo = _client.Post<EResultadoConfigHidrocarburo>(CAppSettings.svc_listar_autorizacion_condicion,
                        new
                        {
                            strCredencial = CParametrosHydro.strCredencialFuncionario,
                            decFiltro = (txtIdConsumidor.Text == "0") ? 2 : 1,
                            strParametro1 = txtIdConsumidor.Text.Trim(),
                            strParametro2 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
                            strParametro3 = ""
                        });
                    if (objHidrocarburo.IntCodigo == 1)
                    {
                        dgvHidrocarburo.DataSource = objHidrocarburo.OResultado;
                        dgvHidrocarburo.DataBind();

                        Session[CVariablesSesion.GrillaAcumRep] = objHidrocarburo.OResultado;
                    }

                    ////ENTIDADES
                    var objEntidades = _client.Post<EResultadoConfigHidrocarburo>(CAppSettings.svc_listar_autorizacion_condicion,
                        new
                        {
                            strCredencial = CParametrosHydro.strCredencialFuncionario,
                            decFiltro = (txtIdConsumidor.Text == "0") ? 4 : 3,
                            strParametro1 = txtIdConsumidor.Text.Trim(),
                            strParametro2 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
                            strParametro3 = ""
                        });
                    if (objEntidades.IntCodigo == 1)
                    {
                        dgvEntidad.DataSource = objEntidades.OResultado;
                        dgvEntidad.DataBind();

                        Session[CVariablesSesion.GrillaAuxiliar] = objEntidades.OResultado;
                    }

                    //ENTIDADES RELACIONADAS
                    Session.Remove(CVariablesSesion.ObjetoEntidad);
                    trvEntidades.Nodes.Clear();
                    if (strActividades.Trim().Length == 0)
                        return;
                    Session[CVariablesSesion.ObjetoEntidad] = strActividades;
                }
                else
                {
                    MImprimirMensaje("Seleccione una actividad.", EnumTiposMensajeAlerta.Alerta, true);
                }

                #endregion
            }
            catch (Exception exp)
            {
                #region RegistraError

                MImprimirMensaje(exp.Message, EnumTiposMensajeAlerta.Error, true);
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

        protected void dgvTipoOperacion_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
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
        protected void dgvTipoOperacionRegistro_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
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
        protected void dgvHidrocarburoRegistro_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar
                    var lstHidrocarburo = new List<O_LISTA_AUTORIZACION_COND_CTY>();
                    lstHidrocarburo = (List<O_LISTA_AUTORIZACION_COND_CTY>)Session[CVariablesSesion.GrillaAcumRep];
                    if (!((cmbHidrocarburo.Value == null || cmbHidrocarburo.Text == "") && (cmbUnidadMedida.Value == null || cmbUnidadMedida.Text == "")))
                    {
                        if (lstHidrocarburo != null && lstHidrocarburo.Any())
                        {
                            lstHidrocarburo.RemoveAll(x => x.ID_IDENTIFICADOR == Convert.ToDecimal(cmbHidrocarburo.Value));
                        }
                        else
                        {
                            lstHidrocarburo = new List<O_LISTA_AUTORIZACION_COND_CTY>();
                        }

                        lstHidrocarburo.Add(new O_LISTA_AUTORIZACION_COND_CTY()
                        {
                            ID_AUTORIZACION_CONDICION = 0,
                            ID_IDENTIFICADOR = Convert.ToDecimal(cmbHidrocarburo.Value),
                            NOMBRE = cmbHidrocarburo.Text,
                            ID_SUB_IDENTIFICADOR = Convert.ToDecimal(cmbUnidadMedida.Value),
                            SUBID_NOMBRE = cmbUnidadMedida.Text,
                            OBSERVACIONES = "",
                            ESTADO = 1
                        });

                        Session[CVariablesSesion.GrillaAcumRep] = lstHidrocarburo;
                        dgvHidrocarburo.DataSource = lstHidrocarburo;
                        dgvHidrocarburo.DataBind();
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
            else
            {
                //todo No implementado.
            }
        }

        protected void dgvHidrocarburo_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    int index = dgvHidrocarburo.FocusedRowIndex;
                    if (index >= 0)
                    {
                        if (e.ButtonID == "btnHidQuitar")
                        {
                            var lstHidrocarburo = new List<O_LISTA_AUTORIZACION_COND_CTY>();
                            lstHidrocarburo = (List<O_LISTA_AUTORIZACION_COND_CTY>)Session[CVariablesSesion.GrillaAcumRep];

                            lstHidrocarburo.RemoveAll(x => x.ID_IDENTIFICADOR == Convert.ToDecimal(dgvHidrocarburo.GetRowValues(index, "ID_IDENTIFICADOR")));

                            Session[CVariablesSesion.GrillaAcumRep] = lstHidrocarburo;
                            dgvHidrocarburo.DataSource = lstHidrocarburo;
                            dgvHidrocarburo.DataBind();
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
        protected void dgvHidrocarburo_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar
                    var lstHidrocarburo = new List<O_LISTA_AUTORIZACION_COND_CTY>();
                    lstHidrocarburo = (List<O_LISTA_AUTORIZACION_COND_CTY>)Session[CVariablesSesion.GrillaAcumRep];
                    if (!((cmbHidrocarburo.Value == null || cmbHidrocarburo.Text == "") && (cmbUnidadMedida.Value == null || cmbUnidadMedida.Text == "")))
                    {
                        if (lstHidrocarburo != null && lstHidrocarburo.Any())
                        {
                            lstHidrocarburo.RemoveAll(x => x.ID_IDENTIFICADOR == Convert.ToDecimal(cmbHidrocarburo.Value));
                        }
                        else
                        {
                            lstHidrocarburo = new List<O_LISTA_AUTORIZACION_COND_CTY>();
                        }

                        lstHidrocarburo.Add(new O_LISTA_AUTORIZACION_COND_CTY()
                        {
                            ID_AUTORIZACION_CONDICION = 0,
                            ID_IDENTIFICADOR = Convert.ToDecimal(cmbHidrocarburo.Value),
                            NOMBRE = cmbHidrocarburo.Text,
                            ID_SUB_IDENTIFICADOR = Convert.ToDecimal(cmbUnidadMedida.Value),
                            SUBID_NOMBRE = cmbUnidadMedida.Text,
                            OBSERVACIONES = "",
                            ESTADO = 1
                        });

                        Session[CVariablesSesion.GrillaAcumRep] = lstHidrocarburo;
                        dgvHidrocarburo.DataSource = lstHidrocarburo;
                        dgvHidrocarburo.DataBind();
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
            else
            {
                //todo No implementado.
            }
        }

        protected void dgvHidrocarburoRegistro_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    int index = dgvHidrocarburo.FocusedRowIndex;
                    if (index >= 0)
                    {
                        if (e.ButtonID == "btnHidQuitar")
                        {
                            var lstHidrocarburo = new List<O_LISTA_AUTORIZACION_COND_CTY>();
                            lstHidrocarburo = (List<O_LISTA_AUTORIZACION_COND_CTY>)Session[CVariablesSesion.GrillaAcumRep];

                            lstHidrocarburo.RemoveAll(x => x.ID_IDENTIFICADOR == Convert.ToDecimal(dgvHidrocarburo.GetRowValues(index, "ID_IDENTIFICADOR")));

                            Session[CVariablesSesion.GrillaAcumRep] = lstHidrocarburo;
                            dgvHidrocarburo.DataSource = lstHidrocarburo;
                            dgvHidrocarburo.DataBind();
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
        protected void dgvHidrocarburo_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
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
        protected void dgvHidrocarburoRegistro_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
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
        protected void dgvEntidad_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    var lstEntidades = Session[CVariablesSesion.GrillaAuxiliar] as List<O_LISTA_AUTORIZACION_COND_CTY>;
                    if (lstEntidades == null)
                    {
                        lstEntidades = new List<O_LISTA_AUTORIZACION_COND_CTY>();
                    }

                    var js = new JavaScriptSerializer();
                    var nroNodos = trvEntidades.Nodes.Count;
                    for (int i = 0; i < nroNodos; i++)
                    {
                        var nroHijos = trvEntidades.Nodes[i].Nodes.Count;
                        for (int j = 0; j < nroHijos; j++)
                        {
                            if (trvEntidades.Nodes[i].Nodes[j].Checked)
                            {
                                var nodo = js.Deserialize<O_OBTIENE_ENTIDADES_CTY>(trvEntidades.Nodes[i].Nodes[j].Name);

                                List<O_LISTA_AUTORIZACION_COND_CTY> lstAux = null;

                                if (lstEntidades != null)
                                {
                                    lstAux = lstEntidades.Where(x => x.ID_IDENTIFICADOR == nodo.ID_CONSUMIDOR && x.ID_SUB_IDENTIFICADOR == nodo.ID_ACTIVIDAD).ToList();
                                }

                                if (lstAux == null || !lstAux.Any())
                                {
                                    var nuevo = new O_LISTA_AUTORIZACION_COND_CTY();
                                    nuevo.ID_AUTORIZACION_CONDICION = 0;
                                    nuevo.ID_SUB_IDENTIFICADOR = nodo.ID_ACTIVIDAD;
                                    nuevo.SUBID_NOMBRE = nodo.ACTIVIDAD;
                                    nuevo.ID_IDENTIFICADOR = nodo.ID_CONSUMIDOR;
                                    nuevo.NOMBRE = nodo.DENOMINACION;
                                    nuevo.DESCRIPCION = "";
                                    lstEntidades.Add(nuevo);
                                }
                            }
                        }
                        dgvEntidad.DataSource = lstEntidades;
                        dgvEntidad.DataBind();
                        Session[CVariablesSesion.GrillaAuxiliar] = lstEntidades;
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
        protected void dgvEntidadRegistro_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    var lstEntidades = Session[CVariablesSesion.GrillaAuxiliar] as List<O_LISTA_AUTORIZACION_COND_CTY>;
                    if (lstEntidades == null)
                    {
                        lstEntidades = new List<O_LISTA_AUTORIZACION_COND_CTY>();
                    }

                    var js = new JavaScriptSerializer();
                    var nroNodos = trvEntidades.Nodes.Count;
                    for (int i = 0; i < nroNodos; i++)
                    {
                        var nroHijos = trvEntidades.Nodes[i].Nodes.Count;
                        for (int j = 0; j < nroHijos; j++)
                        {
                            if (trvEntidades.Nodes[i].Nodes[j].Checked)
                            {
                                var nodo = js.Deserialize<O_OBTIENE_ENTIDADES_CTY>(trvEntidades.Nodes[i].Nodes[j].Name);

                                List<O_LISTA_AUTORIZACION_COND_CTY> lstAux = null;

                                if (lstEntidades != null)
                                {
                                    lstAux = lstEntidades.Where(x => x.ID_IDENTIFICADOR == nodo.ID_CONSUMIDOR && x.ID_SUB_IDENTIFICADOR == nodo.ID_ACTIVIDAD).ToList();
                                }

                                if (lstAux == null || !lstAux.Any())
                                {
                                    var nuevo = new O_LISTA_AUTORIZACION_COND_CTY();
                                    nuevo.ID_AUTORIZACION_CONDICION = 0;
                                    nuevo.ID_SUB_IDENTIFICADOR = nodo.ID_ACTIVIDAD;
                                    nuevo.SUBID_NOMBRE = nodo.ACTIVIDAD;
                                    nuevo.ID_IDENTIFICADOR = nodo.ID_CONSUMIDOR;
                                    nuevo.NOMBRE = nodo.DENOMINACION;
                                    nuevo.DESCRIPCION = "";
                                    lstEntidades.Add(nuevo);
                                }
                            }
                        }
                        dgvEntidad.DataSource = lstEntidades;
                        dgvEntidad.DataBind();
                        Session[CVariablesSesion.GrillaAuxiliar] = lstEntidades;
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
        protected void dgvEntidad_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    int index = dgvEntidad.FocusedRowIndex;
                    if (index >= 0)
                    {
                        if (e.ButtonID == "btnQuitar")
                        {
                            //List<O_LISTA_AUTORIZACION_COND_CTY> lstEntidad;
                            var lstEntidad = Session[CVariablesSesion.GrillaAuxiliar] as List<O_LISTA_AUTORIZACION_COND_CTY>;

                            lstEntidad.RemoveAll(x => x.ID_IDENTIFICADOR == Convert.ToDecimal(dgvEntidad.GetRowValues(index, "ID_IDENTIFICADOR")));

                            Session[CVariablesSesion.GrillaAuxiliar] = lstEntidad;
                            dgvEntidad.DataSource = lstEntidad;
                            dgvEntidad.DataBind();
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
        protected void dgvEntidadRegistro_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    int index = dgvEntidad.FocusedRowIndex;
                    if (index >= 0)
                    {
                        if (e.ButtonID == "btnQuitar")
                        {
                            //List<O_LISTA_AUTORIZACION_COND_CTY> lstEntidad;
                            var lstEntidad = Session[CVariablesSesion.GrillaAuxiliar] as List<O_LISTA_AUTORIZACION_COND_CTY>;

                            lstEntidad.RemoveAll(x => x.ID_IDENTIFICADOR == Convert.ToDecimal(dgvEntidad.GetRowValues(index, "ID_IDENTIFICADOR")));

                            Session[CVariablesSesion.GrillaAuxiliar] = lstEntidad;
                            dgvEntidad.DataSource = lstEntidad;
                            dgvEntidad.DataBind();
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
        protected void dgvEntidad_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
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
        protected void dgvEntidadRegistro_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
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

        protected void btnGuardaGral_Click(object sender, EventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    var datFechaReg = DateTime.Now;

                    string strValor = string.Empty;
                    string strDescripcion = string.Empty;
                    decimal decIdTipoActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                    decimal decAppIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                    decimal decAppFechaRegistro = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString().PadLeft(2, '0') + datFechaReg.Day.ToString().PadLeft(2, '0') + datFechaReg.Hour.ToString().PadLeft(2, '0') + datFechaReg.Minute.ToString().PadLeft(2, '0') + datFechaReg.Second.ToString().PadLeft(2, '0'));

                    for (int intValorTipo = 1; intValorTipo < 11; intValorTipo++)
                    {
                        switch (intValorTipo)
                        {
                            case (int)EnumOctanoParametros.DiasHabilitacion:
                                strValor = spnPeriodo.Number.ToString();
                                strDescripcion = "DIAS HABILITADOS PARA EMITIR LA DECLARACION JURADA";
                                break;
                            case (int)EnumOctanoParametros.Perodicidad:
                                strValor = cmbPeriodicidad.Value.ToString();
                                strDescripcion = "PERIODICIDAD DE DECLARACIONES JURADAS";
                                break;
                            case (int)EnumOctanoParametros.DiasAmpliacion:
                                strValor = spnApertura.Number.ToString();
                                strDescripcion = "DIAS POR DEFECTO PARA AMPLIACIONES";
                                break;
                            case (int)EnumOctanoParametros.SoloDiasHabiles:
                                strValor = chkDiaHabil.Value.ToString();
                                strDescripcion = "CONDICIONA UTILIZAR DIAS CALENDARIO";
                                break;
                            case (int)EnumOctanoParametros.RequisitoPdf:
                                strValor = chkDigital.Value.ToString();
                                strDescripcion = "ESTABLECE COMO REQUISITO EL CARGADO DE UN PDF";
                                break;
                            case (int)EnumOctanoParametros.NoIntimacion:
                                strValor = chkIntimacion.Value.ToString();
                                strDescripcion = "IMPOSIBILITA LA EJECUCION DE PROCESOS SI EXISTE INTIMACION";
                                break;
                            case (int)EnumOctanoParametros.TipoSaldo:
                                strValor = cmbTipoSaldo.Value.ToString();
                                strDescripcion = "HABILITA EL MANEJO DE SALDOS POR TANQUE";
                                break;
                            case (int)EnumOctanoParametros.TextoCabecera:
                                strValor = txtCabeceraDj.Text.Trim();
                                strDescripcion = "TEXTO CABECERA FORMULARIO DDJJ";
                                break;
                            case (int)EnumOctanoParametros.ActividadRel:
                                strValor = txtActRel.Text.Trim();
                                strDescripcion = "ACTIVIDADES RELACIONADAS";
                                break;
                            case (int)EnumOctanoParametros.TipoImpresion:
                                strValor = chkTipoImpresion.Value.ToString();
                                strDescripcion = "AGRUPAR IMPRESION DDJJ POR PRODUCTO";
                                break;
                        }
                        MGrabaOctanoParametrica(strValor, intValorTipo, strDescripcion, decIdTipoActividad, decAppIdUsuario, decAppFechaRegistro);
                    }

                    #endregion
                }
                catch (Exception exp)
                {
                    #region RegistraError

                    MImprimirMensaje(exp.Message, EnumTiposMensajeAlerta.Error, true);
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

        protected void btnGuardaGralRegistro_Click(object sender, EventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    var datFechaReg = DateTime.Now;

                    string strValor = string.Empty;
                    string strDescripcion = string.Empty;
                    decimal decIdTipoActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                    decimal decAppIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                    decimal decAppFechaRegistro = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString().PadLeft(2, '0') + datFechaReg.Day.ToString().PadLeft(2, '0') + datFechaReg.Hour.ToString().PadLeft(2, '0') + datFechaReg.Minute.ToString().PadLeft(2, '0') + datFechaReg.Second.ToString().PadLeft(2, '0'));

                    for (int intValorTipo = 1; intValorTipo < 11; intValorTipo++)
                    {
                        switch (intValorTipo)
                        {
                            case (int)EnumOctanoParametros.DiasHabilitacion:
                                strValor = spnPeriodo.Number.ToString();
                                strDescripcion = "DIAS HABILITADOS PARA EMITIR LA DECLARACION JURADA";
                                break;
                            case (int)EnumOctanoParametros.Perodicidad:
                                strValor = cmbPeriodicidad.Value.ToString();
                                strDescripcion = "PERIODICIDAD DE DECLARACIONES JURADAS";
                                break;
                            case (int)EnumOctanoParametros.DiasAmpliacion:
                                strValor = spnApertura.Number.ToString();
                                strDescripcion = "DIAS POR DEFECTO PARA AMPLIACIONES";
                                break;
                            case (int)EnumOctanoParametros.SoloDiasHabiles:
                                strValor = chkDiaHabil.Value.ToString();
                                strDescripcion = "CONDICIONA UTILIZAR DIAS CALENDARIO";
                                break;
                            case (int)EnumOctanoParametros.RequisitoPdf:
                                strValor = chkDigital.Value.ToString();
                                strDescripcion = "ESTABLECE COMO REQUISITO EL CARGADO DE UN PDF";
                                break;
                            case (int)EnumOctanoParametros.NoIntimacion:
                                strValor = chkIntimacion.Value.ToString();
                                strDescripcion = "IMPOSIBILITA LA EJECUCION DE PROCESOS SI EXISTE INTIMACION";
                                break;
                            case (int)EnumOctanoParametros.TipoSaldo:
                                strValor = cmbTipoSaldo.Value.ToString();
                                strDescripcion = "HABILITA EL MANEJO DE SALDOS POR TANQUE";
                                break;
                            case (int)EnumOctanoParametros.TextoCabecera:
                                strValor = txtCabeceraDj.Text.Trim();
                                strDescripcion = "TEXTO CABECERA FORMULARIO DDJJ";
                                break;
                            case (int)EnumOctanoParametros.ActividadRel:
                                strValor = txtActRel.Text.Trim();
                                strDescripcion = "ACTIVIDADES RELACIONADAS";
                                break;
                            case (int)EnumOctanoParametros.TipoImpresion:
                                strValor = chkTipoImpresion.Value.ToString();
                                strDescripcion = "AGRUPAR IMPRESION DDJJ POR PRODUCTO";
                                break;
                        }
                        MGrabaOctanoParametrica(strValor, intValorTipo, strDescripcion, decIdTipoActividad, decAppIdUsuario, decAppFechaRegistro);
                    }

                    #endregion
                }
                catch (Exception exp)
                {
                    #region RegistraError

                    MImprimirMensaje(exp.Message, EnumTiposMensajeAlerta.Error, true);
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

        protected void btnGuardaTo_Click(object sender, EventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    List<O_LISTA_AUTORIZACION_ACCI_CTY> lstFinalTo = MRecuperaTipoOperacion();

                    if (!lstFinalTo.Any())
                    {
                        MImprimirMensaje("Los tipos de operación no estan definidos.", EnumTiposMensajeAlerta.Error, true);
                    }
                    else
                    {
                        if (Convert.ToInt64(txtIdConsumidor.Text) > 0)
                        {
                            foreach (var reg in lstFinalTo)
                            {
                                MGrabaTipoOperacion(Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()), Convert.ToDecimal(txtIdConsumidor.Text), reg);
                            }
                        }
                        else
                        {
                            for (int i = 0; i < dgvEntidades.VisibleRowCount; i++)
                            {
                                decimal decIdConsumidor = Convert.ToDecimal(dgvEntidades.GetRowValues(i, "ID_CONSUMIDOR").ToString());
                                foreach (var reg in lstFinalTo)
                                {
                                    MGrabaTipoOperacion(Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()), decIdConsumidor, reg);
                                }
                            }
                        }
                    }
                    pucConfig_WindowCallback(null, null);

                    #endregion
                }
                catch (Exception exp)
                {
                    #region RegistraError

                    MImprimirMensaje(exp.Message, EnumTiposMensajeAlerta.Error, true);
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
        protected void btnGuardaToRegistro_Click(object sender, EventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    List<O_LISTA_AUTORIZACION_ACCI_CTY> lstFinalTo = MRecuperaTipoOperacion();

                    if (!lstFinalTo.Any())
                    {
                        MImprimirMensaje("Los tipos de operación no estan definidos.", EnumTiposMensajeAlerta.Error, true);
                    }
                    else
                    {
                        if (Convert.ToInt64(txtIdConsumidor.Text) > 0)
                        {
                            foreach (var reg in lstFinalTo)
                            {
                                MGrabaTipoOperacion(Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()), Convert.ToDecimal(txtIdConsumidor.Text), reg);
                            }
                        }
                        else
                        {
                            for (int i = 0; i < dgvEntidades.VisibleRowCount; i++)
                            {
                                decimal decIdConsumidor = Convert.ToDecimal(dgvEntidades.GetRowValues(i, "ID_CONSUMIDOR").ToString());
                                foreach (var reg in lstFinalTo)
                                {
                                    MGrabaTipoOperacion(Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()), decIdConsumidor, reg);
                                }
                            }
                        }
                    }
                    pucConfig_WindowCallback(null, null);

                    #endregion
                }
                catch (Exception exp)
                {
                    #region RegistraError

                    MImprimirMensaje(exp.Message, EnumTiposMensajeAlerta.Error, true);
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
        protected void btnGuardaHu_Click(object sender, EventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    List<O_LISTA_AUTORIZACION_COND_CTY> lstFinalHi = MRecuperaHidrocarburo();
                    EResultado objResultado;

                    if (!lstFinalHi.Any())
                    {
                        MImprimirMensaje("Los hidrocarburos no estan definidos.", EnumTiposMensajeAlerta.Error, true);
                    }
                    else
                    {
                        if (Convert.ToInt64(txtIdConsumidor.Text) > 0)
                        {
                            var datFechaReg = DateTime.Now;
                            decimal decFecha = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString().PadLeft(2, '0') + datFechaReg.Day.ToString().PadLeft(2, '0') + datFechaReg.Hour.ToString().PadLeft(2, '0') + datFechaReg.Minute.ToString().PadLeft(2, '0') + datFechaReg.Second.ToString().PadLeft(2, '0'));

                            objResultado = _client.Post<EResultado>(CAppSettings.svc_elimina_autorizacion_condicion,
                            new
                            {
                                strCredencial = CParametrosHydro.strCredencialFuncionario,
                                decFiltro = 2,
                                decIdTipoActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()),
                                decIdConsumidor = Convert.ToDecimal(txtIdConsumidor.Text),
                                decIdIdentificador = 0,
                                decAppFechaRegistro = decFecha,
                                decAppIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario])
                            });
                            if (objResultado.intCodigo == 1)
                            {
                                foreach (var reg in lstFinalHi)
                                {
                                    MGrabaCondicion(Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()), Convert.ToDecimal(txtIdConsumidor.Text), 1, reg);
                                }

                            }
                            else
                            {
                                MImprimirMensaje(objResultado.strMensaje, EnumTiposMensajeAlerta.Error, true);
                            }
                        }
                        else
                        {
                            for (int i = 0; i < dgvEntidades.VisibleRowCount; i++)
                            {
                                decimal decIdConsumidor = Convert.ToDecimal(dgvEntidades.GetRowValues(i, "ID_CONSUMIDOR").ToString());
                                var datFechaReg = DateTime.Now;
                                decimal decFecha = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString().PadLeft(2, '0') + datFechaReg.Day.ToString().PadLeft(2, '0') + datFechaReg.Hour.ToString().PadLeft(2, '0') + datFechaReg.Minute.ToString().PadLeft(2, '0') + datFechaReg.Second.ToString().PadLeft(2, '0'));

                                objResultado = _client.Post<EResultado>(CAppSettings.svc_elimina_autorizacion_condicion,
                                new
                                {
                                    strCredencial = CParametrosHydro.strCredencialFuncionario,
                                    decFiltro = 2,
                                    decIdTipoActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()),
                                    decIdConsumidor,
                                    decIdIdentificador = 0,
                                    decAppFechaRegistro = decFecha,
                                    decAppIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario])
                                });
                                if (objResultado.intCodigo == 1)
                                {
                                    foreach (var reg in lstFinalHi)
                                    {
                                        MGrabaCondicion(Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()), decIdConsumidor, 1, reg);
                                    }
                                }
                                else
                                {
                                    MImprimirMensaje(objResultado.strMensaje, EnumTiposMensajeAlerta.Error, true);
                                }
                            }
                        }
                    }
                    pucConfig_WindowCallback(null, null);

                    #endregion
                }
                catch (Exception exp)
                {
                    #region RegistraError

                    MImprimirMensaje(exp.Message, EnumTiposMensajeAlerta.Error, true);
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
        protected void btnGuardaHuRegistro_Click(object sender, EventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    List<O_LISTA_AUTORIZACION_COND_CTY> lstFinalHi = MRecuperaHidrocarburo();
                    EResultado objResultado;

                    if (!lstFinalHi.Any())
                    {
                        MImprimirMensaje("Los hidrocarburos no estan definidos.", EnumTiposMensajeAlerta.Error, true);
                    }
                    else
                    {
                        if (Convert.ToInt64(txtIdConsumidor.Text) > 0)
                        {
                            var datFechaReg = DateTime.Now;
                            decimal decFecha = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString().PadLeft(2, '0') + datFechaReg.Day.ToString().PadLeft(2, '0') + datFechaReg.Hour.ToString().PadLeft(2, '0') + datFechaReg.Minute.ToString().PadLeft(2, '0') + datFechaReg.Second.ToString().PadLeft(2, '0'));

                            objResultado = _client.Post<EResultado>(CAppSettings.svc_elimina_autorizacion_condicion,
                            new
                            {
                                strCredencial = CParametrosHydro.strCredencialFuncionario,
                                decFiltro = 2,
                                decIdTipoActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()),
                                decIdConsumidor = Convert.ToDecimal(txtIdConsumidor.Text),
                                decIdIdentificador = 0,
                                decAppFechaRegistro = decFecha,
                                decAppIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario])
                            });
                            if (objResultado.intCodigo == 1)
                            {
                                foreach (var reg in lstFinalHi)
                                {
                                    MGrabaCondicion(Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()), Convert.ToDecimal(txtIdConsumidor.Text), 1, reg);
                                }
                            }
                            else
                            {
                                MImprimirMensaje(objResultado.strMensaje, EnumTiposMensajeAlerta.Error, true);
                            }
                        }
                        else
                        {
                            for (int i = 0; i < dgvEntidades.VisibleRowCount; i++)
                            {
                                decimal decIdConsumidor = Convert.ToDecimal(dgvEntidades.GetRowValues(i, "ID_CONSUMIDOR").ToString());
                                var datFechaReg = DateTime.Now;
                                decimal decFecha = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString().PadLeft(2, '0') + datFechaReg.Day.ToString().PadLeft(2, '0') + datFechaReg.Hour.ToString().PadLeft(2, '0') + datFechaReg.Minute.ToString().PadLeft(2, '0') + datFechaReg.Second.ToString().PadLeft(2, '0'));

                                objResultado = _client.Post<EResultado>(CAppSettings.svc_elimina_autorizacion_condicion,
                                new
                                {
                                    strCredencial = CParametrosHydro.strCredencialFuncionario,
                                    decFiltro = 2,
                                    decIdTipoActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()),
                                    decIdConsumidor,
                                    decIdIdentificador = 0,
                                    decAppFechaRegistro = decFecha,
                                    decAppIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario])
                                });
                                if (objResultado.intCodigo == 1)
                                {
                                    foreach (var reg in lstFinalHi)
                                    {
                                        MGrabaCondicion(Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()), decIdConsumidor, 1, reg);
                                    }
                                }
                                else
                                {
                                    MImprimirMensaje(objResultado.strMensaje, EnumTiposMensajeAlerta.Error, true);
                                }
                            }
                        }
                    }
                    pucConfig_WindowCallback(null, null);

                    #endregion
                }
                catch (Exception exp)
                {
                    #region RegistraError

                    MImprimirMensaje(exp.Message, EnumTiposMensajeAlerta.Error, true);
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
        protected void btnGuardaEn_Click(object sender, EventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    var lstFinalHi = MRecuperaEntidades();
                    EResultado objResultado;

                    if (Convert.ToInt64(txtIdConsumidor.Text) > 0)
                    {
                        var datFechaReg = DateTime.Now;
                        decimal decFecha = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString().PadLeft(2, '0') + datFechaReg.Day.ToString().PadLeft(2, '0') + datFechaReg.Hour.ToString().PadLeft(2, '0') + datFechaReg.Minute.ToString().PadLeft(2, '0') + datFechaReg.Second.ToString().PadLeft(2, '0'));

                        objResultado = _client.Post<EResultado>(CAppSettings.svc_elimina_autorizacion_condicion,
                        new
                        {
                            strCredencial = CParametrosHydro.strCredencialFuncionario,
                            decFiltro = 3,
                            decIdTipoActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()),
                            decIdConsumidor = Convert.ToDecimal(txtIdConsumidor.Text),
                            decIdIdentificador = 0,
                            decAppFechaRegistro = decFecha,
                            decAppIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario])
                        });
                        if (objResultado.intCodigo == 1)
                        {
                            foreach (var reg in lstFinalHi)
                            {
                                MGrabaCondicion(Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()), Convert.ToDecimal(txtIdConsumidor.Text), 2, reg);
                            }
                        }
                        else
                        {
                            MImprimirMensaje(objResultado.strMensaje, EnumTiposMensajeAlerta.Error, true);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < dgvEntidades.VisibleRowCount; i++)
                        {
                            decimal decIdConsumidor = Convert.ToDecimal(dgvEntidades.GetRowValues(i, "ID_CONSUMIDOR").ToString());
                            var datFechaReg = DateTime.Now;
                            decimal decFecha = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString().PadLeft(2, '0') + datFechaReg.Day.ToString().PadLeft(2, '0') + datFechaReg.Hour.ToString().PadLeft(2, '0') + datFechaReg.Minute.ToString().PadLeft(2, '0') + datFechaReg.Second.ToString().PadLeft(2, '0'));

                            objResultado = _client.Post<EResultado>(CAppSettings.svc_elimina_autorizacion_condicion,
                            new
                            {
                                strCredencial = CParametrosHydro.strCredencialFuncionario,
                                decFiltro = 3,
                                decIdTipoActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()),
                                decIdConsumidor,
                                decIdIdentificador = 0,
                                decAppFechaRegistro = decFecha,
                                decAppIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario])
                            });
                            if (objResultado.intCodigo == 1)
                            {
                                foreach (var reg in lstFinalHi)
                                {
                                    MGrabaCondicion(Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()), decIdConsumidor, 2, reg);
                                }
                            }
                            else
                            {
                                MImprimirMensaje(objResultado.strMensaje, EnumTiposMensajeAlerta.Error, true);
                            }
                        }
                    }

                    pucConfig_WindowCallback(null, null);

                    #endregion
                }
                catch (Exception exp)
                {
                    #region RegistraError

                    MImprimirMensaje(exp.Message, EnumTiposMensajeAlerta.Error, true);
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

        protected void btnGuardaEnRegistro_Click(object sender, EventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    var lstFinalHi = MRecuperaEntidades();
                    EResultado objResultado;

                    if (Convert.ToInt64(txtIdConsumidor.Text) > 0)
                    {
                        var datFechaReg = DateTime.Now;
                        decimal decFecha = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString().PadLeft(2, '0') + datFechaReg.Day.ToString().PadLeft(2, '0') + datFechaReg.Hour.ToString().PadLeft(2, '0') + datFechaReg.Minute.ToString().PadLeft(2, '0') + datFechaReg.Second.ToString().PadLeft(2, '0'));

                        objResultado = _client.Post<EResultado>(CAppSettings.svc_elimina_autorizacion_condicion,
                        new
                        {
                            strCredencial = CParametrosHydro.strCredencialFuncionario,
                            decFiltro = 3,
                            decIdTipoActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()),
                            decIdConsumidor = Convert.ToDecimal(txtIdConsumidor.Text),
                            decIdIdentificador = 0,
                            decAppFechaRegistro = decFecha,
                            decAppIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario])
                        });
                        if (objResultado.intCodigo == 1)
                        {
                            foreach (var reg in lstFinalHi)
                            {
                                MGrabaCondicion(Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()), Convert.ToDecimal(txtIdConsumidor.Text), 2, reg);
                            }
                        }
                        else
                        {
                            MImprimirMensaje(objResultado.strMensaje, EnumTiposMensajeAlerta.Error, true);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < dgvEntidades.VisibleRowCount; i++)
                        {
                            decimal decIdConsumidor = Convert.ToDecimal(dgvEntidades.GetRowValues(i, "ID_CONSUMIDOR").ToString());
                            var datFechaReg = DateTime.Now;
                            decimal decFecha = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString().PadLeft(2, '0') + datFechaReg.Day.ToString().PadLeft(2, '0') + datFechaReg.Hour.ToString().PadLeft(2, '0') + datFechaReg.Minute.ToString().PadLeft(2, '0') + datFechaReg.Second.ToString().PadLeft(2, '0'));

                            objResultado = _client.Post<EResultado>(CAppSettings.svc_elimina_autorizacion_condicion,
                            new
                            {
                                strCredencial = CParametrosHydro.strCredencialFuncionario,
                                decFiltro = 3,
                                decIdTipoActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()),
                                decIdConsumidor,
                                decIdIdentificador = 0,
                                decAppFechaRegistro = decFecha,
                                decAppIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario])
                            });
                            if (objResultado.intCodigo == 1)
                            {
                                foreach (var reg in lstFinalHi)
                                {
                                    MGrabaCondicion(Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()), decIdConsumidor, 2, reg);
                                }
                            }
                            else
                            {
                                MImprimirMensaje(objResultado.strMensaje, EnumTiposMensajeAlerta.Error, true);
                            }
                        }
                    }

                    pucConfig_WindowCallback(null, null);

                    #endregion
                }
                catch (Exception exp)
                {
                    #region RegistraError

                    MImprimirMensaje(exp.Message, EnumTiposMensajeAlerta.Error, true);
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
        protected void btnGuardaSirel_Click(object sender, EventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    if (cmbUnidadMedidaAux.Text == "" || Convert.ToDecimal(cmbUnidadMedidaAux.Value) == -99999)
                    {
                        MImprimirMensaje("Introduzca la unidad de medida.", EnumTiposMensajeAlerta.Alerta, true);
                        return;
                    }
                    var strActividad = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString().Trim();

                    if (Convert.ToInt64(txtIdEntidad.Text) > 0)
                    {
                        var lstInfo = _clientHidro.Get<EResultadoInfoHidro>(CAppSettings.svc_obtenerInformacionSirehidroSirelxId + "/" + CParametrosHydro.StrCredencialSireHidro + "/" + txtIdEntidad.Text.Trim() + "/" + strActividad + "?format=json");
                        if (lstInfo.Resultado == 1)
                        {
                            var strProd = lstInfo.InformacionHydro.FirstOrDefault().decIdHidrocarburo.Trim();
                            var strArreglo = strProd.Split('|');
                            if (strArreglo.Any())
                            {
                                var datFechaReg = DateTime.Now;
                                decimal decFecha = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString().PadLeft(2, '0') + datFechaReg.Day.ToString().PadLeft(2, '0') + datFechaReg.Hour.ToString().PadLeft(2, '0') + datFechaReg.Minute.ToString().PadLeft(2, '0') + datFechaReg.Second.ToString().PadLeft(2, '0'));

                                var objResultado = _client.Post<EResultado>(CAppSettings.svc_elimina_autorizacion_condicion,
                                new
                                {
                                    strCredencial = CParametrosHydro.strCredencialFuncionario,
                                    decFiltro = 2,
                                    decIdTipoActividad = Convert.ToDecimal(strActividad),
                                    decIdConsumidor = Convert.ToDecimal(txtIdConsumidor.Text),
                                    decIdIdentificador = 0,
                                    decAppFechaRegistro = decFecha,
                                    decAppIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario])
                                });
                                if (objResultado.intCodigo == 1)
                                {
                                    for (int i = 0; i < strArreglo.Count(); i++)
                                    {
                                        var objAutCond = new O_LISTA_AUTORIZACION_COND_CTY();
                                        objAutCond.ID_IDENTIFICADOR = Convert.ToDecimal(strArreglo[i].Trim());
                                        objAutCond.ID_SUB_IDENTIFICADOR = Convert.ToDecimal(cmbUnidadMedidaAux.Value.ToString());
                                        objAutCond.OBSERVACIONES = "Integración SIREL";
                                        objAutCond.DESCRIPCION = "";

                                        MGrabaCondicion(Convert.ToDecimal(strActividad), Convert.ToDecimal(txtIdConsumidor.Text), 1, objAutCond);
                                    }
                                }
                                else
                                {
                                    MImprimirMensaje(objResultado.strMensaje, EnumTiposMensajeAlerta.Error, true);
                                }
                            }
                            else
                            {
                                MImprimirMensaje("No existen hidrocarburos asignados por el SIREL", EnumTiposMensajeAlerta.Alerta, true);
                            }
                        }
                        else
                        {
                            MImprimirMensaje("No existe licencia SIREL", EnumTiposMensajeAlerta.Alerta, true);
                        }
                    }
                    else
                    {
                        for (int j = 0; j < dgvEntidades.VisibleRowCount; j++)
                        {

                            decimal decIdEntidad = Convert.ToDecimal(dgvEntidades.GetRowValues(j, "ID_ENTIDAD").ToString());
                            decimal decIdConsumidor = Convert.ToDecimal(dgvEntidades.GetRowValues(j, "ID_CONSUMIDOR").ToString());

                            var lstInfo = _clientHidro.Get<EResultadoInfoHidro>(CAppSettings.svc_obtenerInformacionSirehidroSirelxId + "/" + CParametrosHydro.StrCredencialSireHidro + "/" + decIdEntidad + "/" + strActividad + "?format=json");
                            if (lstInfo.Resultado == 1)
                            {
                                var strProd = lstInfo.InformacionHydro.FirstOrDefault().decIdHidrocarburo.Trim();
                                var strArreglo = strProd.Split('|');
                                if (strArreglo.Any())
                                {
                                    var datFechaReg = DateTime.Now;
                                    decimal decFecha = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString().PadLeft(2, '0') + datFechaReg.Day.ToString().PadLeft(2, '0') + datFechaReg.Hour.ToString().PadLeft(2, '0') + datFechaReg.Minute.ToString().PadLeft(2, '0') + datFechaReg.Second.ToString().PadLeft(2, '0'));

                                    var objResultado = _client.Post<EResultado>(CAppSettings.svc_elimina_autorizacion_condicion,
                                    new
                                    {
                                        strCredencial = CParametrosHydro.strCredencialFuncionario,
                                        decFiltro = 2,
                                        decIdTipoActividad = Convert.ToDecimal(strActividad),
                                        decIdConsumidor,
                                        decIdIdentificador = 0,
                                        decAppFechaRegistro = decFecha,
                                        decAppIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario])
                                    });
                                    if (objResultado.intCodigo == 1)
                                    {
                                        for (int i = 0; i < strArreglo.Count(); i++)
                                        {
                                            var objAutCond = new O_LISTA_AUTORIZACION_COND_CTY();
                                            objAutCond.ID_IDENTIFICADOR = Convert.ToDecimal(strArreglo[i].Trim());
                                            objAutCond.ID_SUB_IDENTIFICADOR = Convert.ToDecimal(cmbUnidadMedidaAux.Value.ToString());
                                            objAutCond.OBSERVACIONES = "Integración SIREL";
                                            objAutCond.DESCRIPCION = "";

                                            MGrabaCondicion(Convert.ToDecimal(strActividad), decIdConsumidor, 1, objAutCond);
                                        }
                                    }
                                    else
                                    {
                                        MImprimirMensaje(objResultado.strMensaje, EnumTiposMensajeAlerta.Error, true);
                                    }
                                }
                                else
                                {
                                    //MImprimirMensaje("No existen hidrocarburos asignados por el SIREL", EnumTiposMensajeAlerta.Alerta, true);
                                }
                            }
                            else
                            {
                                //MImprimirMensaje("No existe licencia SIREL", EnumTiposMensajeAlerta.Alerta, true);
                            }

                        }
                    }
                    pucConfig_WindowCallback(null, null);

                    #endregion
                }
                catch (Exception exp)
                {
                    #region RegistraError

                    MImprimirMensaje(exp.Message, EnumTiposMensajeAlerta.Error, true);
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
        protected void btnGuardaSirelRegistro_Click(object sender, EventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    if (cmbUnidadMedidaAux.Text == "" || Convert.ToDecimal(cmbUnidadMedidaAux.Value) == -99999)
                    {
                        MImprimirMensaje("Introduzca la unidad de medida.", EnumTiposMensajeAlerta.Alerta, true);
                        return;
                    }
                    var strActividad = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString().Trim();

                    if (Convert.ToInt64(txtIdEntidad.Text) > 0)
                    {
                        var lstInfo = _clientHidro.Get<EResultadoInfoHidro>(CAppSettings.svc_obtenerInformacionSirehidroSirelxId + "/" + CParametrosHydro.StrCredencialSireHidro + "/" + txtIdEntidad.Text.Trim() + "/" + strActividad + "?format=json");
                        if (lstInfo.Resultado == 1)
                        {
                            var strProd = lstInfo.InformacionHydro.FirstOrDefault().decIdHidrocarburo.Trim();
                            var strArreglo = strProd.Split('|');
                            if (strArreglo.Any())
                            {
                                var datFechaReg = DateTime.Now;
                                decimal decFecha = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString().PadLeft(2, '0') + datFechaReg.Day.ToString().PadLeft(2, '0') + datFechaReg.Hour.ToString().PadLeft(2, '0') + datFechaReg.Minute.ToString().PadLeft(2, '0') + datFechaReg.Second.ToString().PadLeft(2, '0'));

                                var objResultado = _client.Post<EResultado>(CAppSettings.svc_elimina_autorizacion_condicion,
                                new
                                {
                                    strCredencial = CParametrosHydro.strCredencialFuncionario,
                                    decFiltro = 2,
                                    decIdTipoActividad = Convert.ToDecimal(strActividad),
                                    decIdConsumidor = Convert.ToDecimal(txtIdConsumidor.Text),
                                    decIdIdentificador = 0,
                                    decAppFechaRegistro = decFecha,
                                    decAppIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario])
                                });
                                if (objResultado.intCodigo == 1)
                                {
                                    for (int i = 0; i < strArreglo.Count(); i++)
                                    {
                                        var objAutCond = new O_LISTA_AUTORIZACION_COND_CTY();
                                        objAutCond.ID_IDENTIFICADOR = Convert.ToDecimal(strArreglo[i].Trim());
                                        objAutCond.ID_SUB_IDENTIFICADOR = Convert.ToDecimal(cmbUnidadMedidaAux.Value.ToString());
                                        objAutCond.OBSERVACIONES = "Integración SIREL";
                                        objAutCond.DESCRIPCION = "";

                                        MGrabaCondicion(Convert.ToDecimal(strActividad), Convert.ToDecimal(txtIdConsumidor.Text), 1, objAutCond);
                                    }
                                }
                                else
                                {
                                    MImprimirMensaje(objResultado.strMensaje, EnumTiposMensajeAlerta.Error, true);
                                }
                            }
                            else
                            {
                                MImprimirMensaje("No existen hidrocarburos asignados por el SIREL", EnumTiposMensajeAlerta.Alerta, true);
                            }
                        }
                        else
                        {
                            MImprimirMensaje("No existe licencia SIREL", EnumTiposMensajeAlerta.Alerta, true);
                        }
                    }
                    else
                    {
                        for (int j = 0; j < dgvEntidades.VisibleRowCount; j++)
                        {

                            decimal decIdEntidad = Convert.ToDecimal(dgvEntidades.GetRowValues(j, "ID_ENTIDAD").ToString());
                            decimal decIdConsumidor = Convert.ToDecimal(dgvEntidades.GetRowValues(j, "ID_CONSUMIDOR").ToString());

                            var lstInfo = _clientHidro.Get<EResultadoInfoHidro>(CAppSettings.svc_obtenerInformacionSirehidroSirelxId + "/" + CParametrosHydro.StrCredencialSireHidro + "/" + decIdEntidad + "/" + strActividad + "?format=json");
                            if (lstInfo.Resultado == 1)
                            {
                                var strProd = lstInfo.InformacionHydro.FirstOrDefault().decIdHidrocarburo.Trim();
                                var strArreglo = strProd.Split('|');
                                if (strArreglo.Any())
                                {
                                    var datFechaReg = DateTime.Now;
                                    decimal decFecha = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString().PadLeft(2, '0') + datFechaReg.Day.ToString().PadLeft(2, '0') + datFechaReg.Hour.ToString().PadLeft(2, '0') + datFechaReg.Minute.ToString().PadLeft(2, '0') + datFechaReg.Second.ToString().PadLeft(2, '0'));

                                    var objResultado = _client.Post<EResultado>(CAppSettings.svc_elimina_autorizacion_condicion,
                                    new
                                    {
                                        strCredencial = CParametrosHydro.strCredencialFuncionario,
                                        decFiltro = 2,
                                        decIdTipoActividad = Convert.ToDecimal(strActividad),
                                        decIdConsumidor,
                                        decIdIdentificador = 0,
                                        decAppFechaRegistro = decFecha,
                                        decAppIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario])
                                    });
                                    if (objResultado.intCodigo == 1)
                                    {
                                        for (int i = 0; i < strArreglo.Count(); i++)
                                        {
                                            var objAutCond = new O_LISTA_AUTORIZACION_COND_CTY();
                                            objAutCond.ID_IDENTIFICADOR = Convert.ToDecimal(strArreglo[i].Trim());
                                            objAutCond.ID_SUB_IDENTIFICADOR = Convert.ToDecimal(cmbUnidadMedidaAux.Value.ToString());
                                            objAutCond.OBSERVACIONES = "Integración SIREL";
                                            objAutCond.DESCRIPCION = "";

                                            MGrabaCondicion(Convert.ToDecimal(strActividad), decIdConsumidor, 1, objAutCond);
                                        }
                                    }
                                    else
                                    {
                                        MImprimirMensaje(objResultado.strMensaje, EnumTiposMensajeAlerta.Error, true);
                                    }
                                }
                                else
                                {
                                    //MImprimirMensaje("No existen hidrocarburos asignados por el SIREL", EnumTiposMensajeAlerta.Alerta, true);
                                }
                            }
                            else
                            {
                                //MImprimirMensaje("No existe licencia SIREL", EnumTiposMensajeAlerta.Alerta, true);
                            }

                        }
                    }
                    pucConfig_WindowCallback(null, null);

                    #endregion
                }
                catch (Exception exp)
                {
                    #region RegistraError

                    MImprimirMensaje(exp.Message, EnumTiposMensajeAlerta.Error, true);
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
                    else if (txtCtaId.Text != "0" && txtCtaPadre.Text.Trim().Length > 0)
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
                        MImprimirMensaje("Parametros inválidos.", EnumTiposMensajeAlerta.Error, false);
                        return;
                    }

                    var objResultado = _client.Post<EResultado>(CAppSettings.svc_registra_autorizacion_condicion,
                    new
                    {
                        strCredencial = CParametrosHydro.strCredencialFuncionario,
                        decIdTipoActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()),
                        decIdConsumidor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString()),
                        decIdIdentificador,
                        decIdSubIdentificador = 0,
                        decEstado,
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

                    pucConfig_WindowCallback(null, null);

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
        protected void btnPfGrabarRegistro_Click(object sender, EventArgs e)
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
                    else if (txtCtaId.Text != "0" && txtCtaPadre.Text.Trim().Length > 0)
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
                        MImprimirMensaje("Parametros inválidos.", EnumTiposMensajeAlerta.Error, false);
                        return;
                    }

                    var objResultado = _client.Post<EResultado>(CAppSettings.svc_registra_autorizacion_condicion,
                    new
                    {
                        strCredencial = CParametrosHydro.strCredencialFuncionario,
                        decIdTipoActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()),
                        decIdConsumidor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString()),
                        decIdIdentificador,
                        decIdSubIdentificador = 0,
                        decEstado,
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

                    pucConfig_WindowCallback(null, null);

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
                        if (e.ButtonID == "btnCtaEliminar")
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
                                pucConfig_WindowCallback(null, null);
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
        protected void dgvCuentasRegistro_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
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
                        if (e.ButtonID == "btnCtaEliminar")
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
                                pucConfig_WindowCallback(null, null);
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

        protected void dgvCuentas_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            try
            {
                #region Proceso a Ejecutar

                if (e.VisibleIndex >= 0)
                {
                    if (dgvCuentas.GetRowValues(e.VisibleIndex, "ESTADO").ToString() == "1")
                        e.Row.BackColor = Color.FromArgb(227, 198, 132);
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
        protected void dgvCuentasRegistro_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            try
            {
                #region Proceso a Ejecutar

                if (e.VisibleIndex >= 0)
                {
                    if (dgvCuentas.GetRowValues(e.VisibleIndex, "ESTADO").ToString() == "1")
                        e.Row.BackColor = Color.FromArgb(227, 198, 132);
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
        protected void btnCargaEnt_Click(object sender, EventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    trvEntidades.Nodes.Clear();
                    MCargaProyectoRel();

                    if (Convert.ToInt64(txtIdConsumidor.Text) == 0) return;

                    pnlGeneral.Visible = false;
                    pgcOpciones.TabPages[3].Visible = false;

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
        protected void btnCargaEntRegistro_Click(object sender, EventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    trvEntidades.Nodes.Clear();
                    MCargaProyectoRel();

                    if (Convert.ToInt64(txtIdConsumidor.Text) == 0) return;

                    pnlGeneral.Visible = false;
                    pgcOpciones.TabPages[3].Visible = false;

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

        protected void btnToEliminar_Click(object sender, EventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    var objResultado = _client.Post<EResultado>(CAppSettings.svc_elimina_autorizacion_accion,
                    new
                    {
                        strCredencial = CParametrosHydro.strCredencialFuncionario,
                        decFiltro = 3,
                        decIdTipoActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()),
                        decIdConsumidor = 0,
                        decIdTipoOperacion = 0,
                        decAppFechaRegistro = CFechas.ConvierteDateTimeLong(DateTime.Now),
                        decAppIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario])
                    });
                    if (objResultado.intCodigo != 1)
                    {
                        MImprimirMensaje(objResultado.strMensaje, EnumTiposMensajeAlerta.Error, false);
                    }

                    #endregion
                }
                catch (Exception exp)
                {
                    #region RegistraError

                    MImprimirMensaje(exp.Message, EnumTiposMensajeAlerta.Error, true);
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
        protected void btnToEliminarRegistro_Click(object sender, EventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    var objResultado = _client.Post<EResultado>(CAppSettings.svc_elimina_autorizacion_accion,
                    new
                    {
                        strCredencial = CParametrosHydro.strCredencialFuncionario,
                        decFiltro = 3,
                        decIdTipoActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()),
                        decIdConsumidor = 0,
                        decIdTipoOperacion = 0,
                        decAppFechaRegistro = CFechas.ConvierteDateTimeLong(DateTime.Now),
                        decAppIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario])
                    });
                    if (objResultado.intCodigo != 1)
                    {
                        MImprimirMensaje(objResultado.strMensaje, EnumTiposMensajeAlerta.Error, false);
                    }

                    #endregion
                }
                catch (Exception exp)
                {
                    #region RegistraError

                    MImprimirMensaje(exp.Message, EnumTiposMensajeAlerta.Error, true);
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
        protected void btnHiEliminar_Click(object sender, EventArgs e)
        {
            MEliminaAutCond(1);
        }
        protected void btnHiEliminarRegistro_Click(object sender, EventArgs e)
        {
            MEliminaAutCond(1);
        }
        protected void btnPrEliminar_Click(object sender, EventArgs e)
        {
            MEliminaAutCond(2);
        }
        protected void btnPrEliminarRegistro_Click(object sender, EventArgs e)
        {
            MEliminaAutCond(2);
        }
        protected void btnDcEliminar_Click(object sender, EventArgs e)
        {
            MEliminaAutCond(3);
        }
        protected void btnDcEliminarRegistro_Click(object sender, EventArgs e)
        {
            MEliminaAutCond(3);
        }

        private void MEliminaAutCond(decimal decTipo)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    var objResultado = _client.Post<EResultado>(CAppSettings.svc_elimina_autorizacion_condicion,
                            new
                            {
                                strCredencial = CParametrosHydro.strCredencialFuncionario,
                                decFiltro = 6,
                                decIdTipoActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()),
                                decIdConsumidor = decTipo,
                                decIdIdentificador = 0,
                                decAppFechaRegistro = CFechas.ConvierteDateTimeLong(DateTime.Now),
                                decAppIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario])
                            });
                    if (objResultado.intCodigo != 1)
                    {
                        MImprimirMensaje(objResultado.strMensaje, EnumTiposMensajeAlerta.Error, false);
                    }

                    #endregion
                }
                catch (Exception exp)
                {
                    #region RegistraError

                    MImprimirMensaje(exp.Message, EnumTiposMensajeAlerta.Error, true);
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
                                                                      "/WFCondicionUso.aspx?format=json");
                if (lstResultado.decCodigo == 1)
                {
                    if (lstResultado.oResultado != null && lstResultado.oResultado.Any())
                    {
                        Session[CVariablesSesion.PermisoAlta] = lstResultado.oResultado.FirstOrDefault().ALTAS;
                        Session[CVariablesSesion.PermisoBaja] = lstResultado.oResultado.FirstOrDefault().BAJAS;
                    }
                    else
                    {
                        Session[CVariablesSesion.PermisoAlta] = 0;
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
            if (Convert.ToInt16(Session[CVariablesSesion.PermisoAlta]) == 0)
            {
                btnGuardaGral.Visible = false;
                //btnGuardaTo.Visible = false;
                btnGuardaHu.Visible = false;
                btnGuardaEn.Visible = false;
                btnPfGrabar.Visible = false;
                btnGuardaSirel.Visible = false;
            }
            else
            {
                btnGuardaGral.Visible = true;
                //btnGuardaTo.Visible = true;
                btnGuardaHu.Visible = true;
                btnGuardaEn.Visible = true;
                btnPfGrabar.Visible = true;
                btnGuardaSirel.Visible = true;
            }
            if (Convert.ToInt16(Session[CVariablesSesion.PermisoBaja]) == 0)
            {
                btnToEliminar.Visible = false;
                btnHiEliminar.Visible = false;
                btnPrEliminar.Visible = false;
                btnDcEliminar.Visible = false;
            }
            else
            {
                btnToEliminar.Visible = true;
                btnHiEliminar.Visible = true;
                btnPrEliminar.Visible = true;
                btnDcEliminar.Visible = true;
            }
        }

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
        /// METODO. MLimpiaGrilla
        /// </summary>
        private void MLimpiaGrilla()
        {
            Session.Remove(CVariablesSesion.GrillaTransaccion);
            dgvEntidades.DataSource = null;
            dgvEntidades.DataBind();
            pnlMensaje.Visible = false;
        }

        /// <summary>
        /// METODO. MLimpiaFolmulario
        /// </summary>
        private void MLimpiaFolmulario()
        {
            txtActividad.Text = "";
            txtDepto.Text = "";
            txtIdEntidad.Text = "";

            Session.Remove(CVariablesSesion.GrillaAcumulado);
            dgvTipoOperacion.DataSource = null;
            dgvTipoOperacion.DataBind();

            Session.Remove(CVariablesSesion.GrillaAcumRep);
            dgvHidrocarburo.DataSource = null;
            dgvHidrocarburo.DataBind();

            Session.Remove(CVariablesSesion.GrillaAuxiliar);
            dgvEntidad.DataSource = null;
            dgvEntidad.DataBind();

            Session.Remove(CVariablesSesion.GrillaIntimacion);
            dgvCuentas.DataSource = null;
            dgvCuentas.DataBind();

            spnPeriodo.Number = 10;
            cmbPeriodicidad.Value = "3";
            spnApertura.Number = 2;
            chkDiaHabil.Value = 0;
            chkDigital.Value = 0;
            chkIntimacion.Value = 0;
            cmbTipoSaldo.Value = 1;

            txtActRel.Text = "";
            chkTipoImpresion.Value = 0;
        }

        /// <summary>
        /// METODO. MCargaParametricas
        /// </summary>
        private void MCargaParametricas()
        {
            var objParametricas = new O_PARAMETRICAS_GRAL();

            var objHidrocarburo = _client.Post<EResultadoProducto>(CAppSettings.svc_lista_producto,
            new
            {
                strCredencial = CParametrosHydro.strCredencialFuncionario,
                strCodigo = ""
            });
            if (objHidrocarburo.IntCodigo == 1)
            {
                objParametricas.lstHidrocarburo = objHidrocarburo.OResultado;
            }
            var lstObtCatalogo = _client.Post<EResultadoParametrica>(CAppSettings.svc_obtener_catalogo,
            new
            {
                strLlave = ""
            });

            if (lstObtCatalogo.IntCodigo == 1)
            {
                objParametricas.lstUnidadMedida = lstObtCatalogo.OResultado.CATALOGO_UNIDADES_MEDIDA_GRAL;
            }

            Session[CVariablesSesion.ListaParametrica] = objParametricas;
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
            Session[CVariablesSesion.ObtenerEntidades] = objArbol;
            Session[CVariablesSesion.EstructuraArbol] = _cUtil.MObtenerArbolOrdenado(3, objArbol);

            ////ENTIDADES
            //var lstEntidadRel = Session[CVariablesSesion.ObjetoEntidad] as List<O_OBTIENE_ENTIDADES_CTY>;
            //if (lstEntidadRel == null)
            //{
            //    string strActividades = Properties.Settings.Default.strActividadesRel.Trim();
            //    var objEntidades = _consulta.ObtenerEntidades(
            //        new EObjetoEnvio5ParametrosProto
            //        {
            //            decFiltro = 2,
            //            strParametro1 = "",
            //            strParametro2 = strActividades,
            //            strParametro3 = CAppSettings.idAplicacion,
            //            strParametro4 = "",
            //            strParametro5 = ""
            //        });
            //    if (objEntidades != null && objEntidades.intCodigo == 1)
            //    {
            //        lstEntidadRel = (List<O_OBTIENE_ENTIDADES_CTY>)objEntidades.oResultado;
            //    }
            //    else
            //    {
            //        MImprimirMensaje(objEntidades.strMensaje, EnumTiposMensajeAlerta.Alerta, false);
            //    }
            //}

            //if (lstEntidadRel != null)
            //{
            //    lstEntidadRel = lstEntidadRel.OrderBy(x => x.ID_ACTIVIDAD).ThenBy(x => x.DENOMINACION).ToList();
            //    decimal decId = 0;
            //    foreach (var registro in lstEntidadRel)
            //    {
            //        if (decId != registro.ID_ACTIVIDAD)
            //        {
            //            var itemPadre = new TreeViewNode(registro.ACTIVIDAD, "");
            //            trvEntidades.Nodes.Add(itemPadre);
            //            decId = registro.ID_ACTIVIDAD;
            //        }
            //        else
            //        {
            //            //todo No implementado.
            //        }
            //        var js = new JavaScriptSerializer();
            //        string data = js.Serialize(registro);
            //        var itemHijo = new TreeViewNode(registro.DENOMINACION, data);
            //        trvEntidades.Nodes.FindByText(registro.ACTIVIDAD).Nodes.Add(itemHijo);
            //    }
            //}
            //else
            //{
            //    MImprimirMensaje("NO EXISTEN ENTIDADES RELACIONADAS", EnumTiposMensajeAlerta.Error, false);
            //}
        }

        /// <summary>
        /// METODO. MCargaProyectoRel
        /// </summary>
        private void MCargaProyectoRel()
        {
            if (Session[CVariablesSesion.ObjetoEntidad] == null)
                return;

            var strActividades = (string)Session[CVariablesSesion.ObjetoEntidad];
            var objEntidades = _consulta.ObtenerEntidades(
                new EObjetoEnvio5ParametrosProto
                {
                    decFiltro = 2,
                    strParametro1 = "",
                    strParametro2 = strActividades,
                    strParametro3 = CAppSettings.idAplicacion,
                    strParametro4 = "",
                    strParametro5 = ""
                });
            if (objEntidades != null && objEntidades.intCodigo == 1)
            {
                var lstEntidadRel = (List<O_OBTIENE_ENTIDADES_CTY>)objEntidades.oResultado;

                if (lstEntidadRel != null)
                {
                    lstEntidadRel = lstEntidadRel.OrderBy(x => x.ID_ACTIVIDAD).ThenBy(x => x.DENOMINACION).ToList();
                    decimal decId = 0;
                    foreach (var registro in lstEntidadRel)
                    {
                        if (decId != registro.ID_ACTIVIDAD)
                        {
                            var itemPadre = new TreeViewNode(registro.ACTIVIDAD, "");
                            trvEntidades.Nodes.Add(itemPadre);
                            decId = registro.ID_ACTIVIDAD;
                        }
                        else
                        {
                            //todo No implementado.
                        }
                        var js = new JavaScriptSerializer();
                        string data = js.Serialize(registro);
                        var itemHijo = new TreeViewNode(registro.DENOMINACION, data);
                        trvEntidades.Nodes.FindByText(registro.ACTIVIDAD).Nodes.Add(itemHijo);
                    }
                }
                else
                {
                    MImprimirMensaje("NO EXISTEN ENTIDADES RELACIONADAS", EnumTiposMensajeAlerta.Error, false);
                }

            }
            else
            {
                MImprimirMensaje(objEntidades.strMensaje, EnumTiposMensajeAlerta.Alerta, false);
            }
        }

        /// <summary>
        /// METODO. MCargaGrilla
        /// </summary>
        /// <param name="intIdActividad"></param>
        /// <param name="strDepto"></param>
        /// <param name="intIdOperador"></param>
        /// <param name="intTipo"></param>
        private void MCargaGrilla(int intIdActividad, string strDepto, int intIdOperador, int intTipo)
        {
            var lstEntidad = (List<O_OBTIENE_ENTIDADES_CTY>)Session[CVariablesSesion.ObtenerEntidades];
            switch (intTipo)
            {
                case 1:
                    lstEntidad = lstEntidad.Where(a => a.ID_ACTIVIDAD == intIdActividad).ToList();
                    break;
                case 2:
                    lstEntidad = lstEntidad.Where(a => a.ID_ACTIVIDAD == intIdActividad && a.DEPARTAMENTO == strDepto).ToList();
                    break;
                default:
                    lstEntidad = lstEntidad.Where(a => a.ID_ACTIVIDAD == intIdActividad && a.DEPARTAMENTO == strDepto && a.ID_CONSUMIDOR == intIdOperador).ToList();
                    break;
            }
            lstEntidad = lstEntidad.Where(a => a.ID_ACTIVIDAD == intIdActividad).ToList();
            lstEntidad = lstEntidad.OrderBy(a => a.DEPARTAMENTO).ThenBy(a => a.DENOMINACION).ToList();
            Session[CVariablesSesion.GrillaTransaccion] = lstEntidad;
            dgvEntidades.DataSource = lstEntidad;
            dgvEntidades.DataBind();
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
        /// METODO. MGrabaOctanoParametrica
        /// </summary>
        /// <param name="strValor"></param>
        /// <param name="decTipoValor"></param>
        /// <param name="strDescripcion"></param>
        /// <param name="decIdTipoActividad"></param>
        /// <param name="decAppIdUsuario"></param>
        /// <param name="decAppFechaRegistro"></param>
        private void MGrabaOctanoParametrica(string strValor, decimal decTipoValor, string strDescripcion, decimal decIdTipoActividad, decimal decAppIdUsuario, decimal decAppFechaRegistro)
        {
            var objResultado = _client.Post<EResultado>(CAppSettings.svc_registra_octano_parametrica,
                    new
                    {
                        strCredencial = CParametrosHydro.strCredencialFuncionario,
                        strValorInicio = "",
                        strValorFin = strValor,
                        decTipoValor,
                        decEstado = 1,
                        strDescripcion,
                        decIdTipoActividad,
                        decAppIdUsuario,
                        decAppFechaRegistro
                    });
            if (objResultado.intCodigo != 1)
            {
                MImprimirMensaje(objResultado.strMensaje, EnumTiposMensajeAlerta.Error, true);
            }
        }

        /// <summary>
        /// METODO. MGrabaTipoOperacion
        /// </summary>
        /// <param name="decIdTipoActividad"></param>
        /// <param name="decIdConsumidor"></param>
        /// <param name="objRegistro"></param>
        private void MGrabaTipoOperacion(decimal decIdTipoActividad, decimal decIdConsumidor, O_LISTA_AUTORIZACION_ACCI_CTY objRegistro)
        {
            var datFechaReg = DateTime.Now;
            decimal decFecha = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString().PadLeft(2, '0') + datFechaReg.Day.ToString().PadLeft(2, '0') + datFechaReg.Hour.ToString().PadLeft(2, '0') + datFechaReg.Minute.ToString().PadLeft(2, '0') + datFechaReg.Second.ToString().PadLeft(2, '0'));

            EResultado objResultado;
            if (objRegistro.decExcluye == 0)
            {
                objResultado = _client.Post<EResultado>(CAppSettings.svc_elimina_autorizacion_accion,
                new
                {
                    strCredencial = CParametrosHydro.strCredencialFuncionario,
                    decFiltro = 1,
                    decIdTipoActividad,
                    decIdConsumidor,
                    decIdTipoOperacion = objRegistro.ID_TIPO_OPERACION,
                    decAppFechaRegistro = decFecha,
                    decAppIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario])
                });
                if (objResultado.intCodigo == 1)
                {
                    objResultado = _client.Post<EResultado>(CAppSettings.svc_registra_autorizacion_accion,
                    new
                    {
                        strCredencial = CParametrosHydro.strCredencialFuncionario,
                        decIdTipoActividad,
                        decIdConsumidor,
                        decIdTipoOperacion = objRegistro.ID_TIPO_OPERACION,
                        strBinarioEstado = objRegistro.BINARIO_ESTADO,
                        decFechaFin = 0,
                        decFechaInicio = 0,
                        strObservaciones = objRegistro.OBSERVACIONES,
                        decAccionPermitida = 1,
                        decAppFechaRegistro = decFecha,
                        decAppIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario])
                    });
                    if (objResultado.intCodigo != 1)
                    {
                        MImprimirMensaje(objResultado.strMensaje, EnumTiposMensajeAlerta.Error, true);
                    }
                    else
                    {
                        //todo No implementado.
                    }
                }
                else
                {
                    MImprimirMensaje(objResultado.strMensaje, EnumTiposMensajeAlerta.Error, true);
                }
            }
            else
            {
                //todo No implementado.
            }
        }

        /// <summary>
        /// METODO. MGrabaCondicion
        /// </summary>
        /// <param name="decIdTipoActividad"></param>
        /// <param name="decIdConsumidor"></param>
        /// <param name="tipo"></param>
        /// <param name="objRegistro"></param>
        private void MGrabaCondicion(decimal decIdTipoActividad, decimal decIdConsumidor, decimal tipo, O_LISTA_AUTORIZACION_COND_CTY objRegistro)
        {
            var datFechaReg = DateTime.Now;
            decimal decFecha = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString().PadLeft(2, '0') + datFechaReg.Day.ToString().PadLeft(2, '0') + datFechaReg.Hour.ToString().PadLeft(2, '0') + datFechaReg.Minute.ToString().PadLeft(2, '0') + datFechaReg.Second.ToString().PadLeft(2, '0'));

            var objResultado = _client.Post<EResultado>(CAppSettings.svc_registra_autorizacion_condicion,
            new
            {
                strCredencial = CParametrosHydro.strCredencialFuncionario,
                decIdTipoActividad,
                decIdConsumidor,
                decIdIdentificador = objRegistro.ID_IDENTIFICADOR,
                decIdSubIdentificador = objRegistro.ID_SUB_IDENTIFICADOR,
                decEstado = 0,
                strObservaciones = objRegistro.OBSERVACIONES,
                decTipoCondicion = tipo,
                strCodigo = "",
                strDescripcion = objRegistro.DESCRIPCION,
                decAppFechaRegistro = decFecha,
                decAppIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario])
            });
            if (objResultado.intCodigo != 1)
            {
                MImprimirMensaje(objResultado.strMensaje, EnumTiposMensajeAlerta.Error, true);
            }
        }

        /// <summary>
        /// METODO. MRecuperaTipoOperacion
        /// </summary>
        /// <returns></returns>
        private List<O_LISTA_AUTORIZACION_ACCI_CTY> MRecuperaTipoOperacion()
        {
            var resultado = new List<O_LISTA_AUTORIZACION_ACCI_CTY>();
            string strBinario;
            for (int i = 0; i < dgvTipoOperacion.VisibleRowCount; i++)
            {
                var registro = new O_LISTA_AUTORIZACION_ACCI_CTY();

                registro.ID_TIPO_OPERACION = Convert.ToDecimal(dgvTipoOperacion.GetRowValues(i, "ID_TIPO_OPERACION").ToString());
                registro.ID_AUTORIZACION_ACCION = Convert.ToDecimal(dgvTipoOperacion.GetRowValues(i, "ID_AUTORIZACION_ACCION").ToString());

                GridViewDataTextColumn column = new GridViewDataTextColumn();
                column = (GridViewDataTextColumn)dgvTipoOperacion.Columns["decMasivo"];
                ASPxCheckBox chkMasivo = (dgvTipoOperacion.FindRowCellTemplateControl(i, column, "chkMasivo")) as ASPxCheckBox;
                registro.decMasivo = (decimal)chkMasivo.Value;
                strBinario = registro.decMasivo.ToString();

                column = (GridViewDataTextColumn)dgvTipoOperacion.Columns["decFuncion"];
                ASPxCheckBox chkFuncion = (dgvTipoOperacion.FindRowCellTemplateControl(i, column, "chkFuncion")) as ASPxCheckBox;
                registro.decFuncion = (decimal)chkFuncion.Value;
                strBinario = strBinario + registro.decFuncion;

                //column = (GridViewDataTextColumn)dgvTipoOperacion.Columns["decCumpDiario"];
                //ASPxCheckBox chkCumpDiario = (dgvTipoOperacion.FindRowCellTemplateControl(i, column, "chkCumpDiario")) as ASPxCheckBox;
                //registro.decCumpDiario = (decimal)chkCumpDiario.Value;
                //strBinario = strBinario + registro.decCumpDiario + "0000000";
                strBinario = strBinario + "00000000";

                column = (GridViewDataTextColumn)dgvTipoOperacion.Columns["decExcluye"];
                ASPxCheckBox chkExcluye = (dgvTipoOperacion.FindRowCellTemplateControl(i, column, "chkExcluye")) as ASPxCheckBox;
                registro.decExcluye = (decimal)chkExcluye.Value;

                column = (GridViewDataTextColumn)dgvTipoOperacion.Columns["OBSERVACIONES"];
                ASPxTextBox txtObservacion = (dgvTipoOperacion.FindRowCellTemplateControl(i, column, "txtObservacion")) as ASPxTextBox;
                registro.OBSERVACIONES = txtObservacion.Text.Trim();

                registro.BINARIO_ESTADO = strBinario;
                resultado.Add(registro);
            }

            return resultado;
        }

        /// <summary>
        /// METODO. MRecuperaHidrocarburo
        /// </summary>
        /// <returns></returns>
        private List<O_LISTA_AUTORIZACION_COND_CTY> MRecuperaHidrocarburo()
        {
            var resultado = new List<O_LISTA_AUTORIZACION_COND_CTY>();

            for (int i = 0; i < dgvHidrocarburo.VisibleRowCount; i++)
            {
                var registro = new O_LISTA_AUTORIZACION_COND_CTY();

                registro.ID_AUTORIZACION_CONDICION = Convert.ToDecimal(dgvHidrocarburo.GetRowValues(i, "ID_AUTORIZACION_CONDICION").ToString());
                registro.ID_IDENTIFICADOR = Convert.ToDecimal(dgvHidrocarburo.GetRowValues(i, "ID_IDENTIFICADOR").ToString());
                registro.ID_SUB_IDENTIFICADOR = Convert.ToDecimal(dgvHidrocarburo.GetRowValues(i, "ID_SUB_IDENTIFICADOR").ToString());
                registro.ESTADO = Convert.ToDecimal(dgvHidrocarburo.GetRowValues(i, "ESTADO").ToString());

                GridViewDataTextColumn column = new GridViewDataTextColumn();
                column = (GridViewDataTextColumn)dgvHidrocarburo.Columns["OBSERVACIONES"];
                ASPxTextBox txtObservacion = (dgvHidrocarburo.FindRowCellTemplateControl(i, column, "txtObservacion")) as ASPxTextBox;
                registro.OBSERVACIONES = txtObservacion.Text.Trim();

                resultado.Add(registro);
            }

            return resultado;
        }

        /// <summary>
        /// METODO. MRecuperaEntidades
        /// </summary>
        /// <returns></returns>
        private List<O_LISTA_AUTORIZACION_COND_CTY> MRecuperaEntidades()
        {
            var resultado = new List<O_LISTA_AUTORIZACION_COND_CTY>();

            for (int i = 0; i < dgvEntidad.VisibleRowCount; i++)
            {
                var registro = new O_LISTA_AUTORIZACION_COND_CTY();

                registro.ID_AUTORIZACION_CONDICION = Convert.ToDecimal(dgvEntidad.GetRowValues(i, "ID_AUTORIZACION_CONDICION").ToString());
                registro.ID_IDENTIFICADOR = Convert.ToDecimal(dgvEntidad.GetRowValues(i, "ID_IDENTIFICADOR").ToString());
                registro.ID_SUB_IDENTIFICADOR = Convert.ToDecimal(dgvEntidad.GetRowValues(i, "ID_SUB_IDENTIFICADOR").ToString());
                registro.ESTADO = Convert.ToDecimal(dgvEntidad.GetRowValues(i, "ESTADO").ToString());

                GridViewDataTextColumn column = new GridViewDataTextColumn();
                column = (GridViewDataTextColumn)dgvEntidad.Columns["DESCRIPCION"];
                ASPxTextBox txtAlias = (dgvEntidad.FindRowCellTemplateControl(i, column, "txtAlias")) as ASPxTextBox;
                registro.DESCRIPCION = txtAlias.Text.Trim();

                resultado.Add(registro);
            }

            return resultado;
        }

        /// <summary>
        /// MRecuperaTipoOperacionUnico
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        private O_LISTA_AUTORIZACION_ACCI_CTY MRecuperaTipoOperacionUnico(string strgDatosIngreso)
        {
            string[] strgDatosSpliteado = strgDatosIngreso.Split('|');
            List<O_LISTA_AUTORIZACION_ACCI_CTY> listaAutorizacionAcciCtys = Session[CVariablesSesion.GrillaAcumulado] != null ? (List<O_LISTA_AUTORIZACION_ACCI_CTY>)Session[CVariablesSesion.GrillaAcumulado] : new List<O_LISTA_AUTORIZACION_ACCI_CTY>();
            O_LISTA_AUTORIZACION_ACCI_CTY objetoListaAutorizacionAcciCty = listaAutorizacionAcciCtys.FirstOrDefault(w => w.ID_TIPO_OPERACION == Convert.ToDecimal(strgDatosSpliteado[0]));

            string str = "AM0122200204";
            string substr = str.Substring(0, 3);

            if (strgDatosSpliteado[1] == "HHIM")
            {
                if (strgDatosSpliteado[2] == "true")
                    objetoListaAutorizacionAcciCty.decMasivo = 1;
                else
                    objetoListaAutorizacionAcciCty.decMasivo = 0;
                string strgBinario = objetoListaAutorizacionAcciCty.BINARIO_ESTADO.Substring(0, 2);
                if (strgBinario == "11" || strgBinario == "01")
                {
                    objetoListaAutorizacionAcciCty.BINARIO_ESTADO =
                        objetoListaAutorizacionAcciCty.decMasivo + "100000000";
                }
                else if (strgBinario == "10" || strgBinario == "00")
                {
                    objetoListaAutorizacionAcciCty.BINARIO_ESTADO =
                        objetoListaAutorizacionAcciCty.decMasivo + "000000000";
                }
            }
            else if (strgDatosSpliteado[1] == "HFDM")
            {
                //objetoListaAutorizacionAcciCty.decMasivo = Convert.ToDecimal(strgDatosSpliteado[2]); 
                if (strgDatosSpliteado[2] == "true")
                    objetoListaAutorizacionAcciCty.decMasivo = 1;
                else
                    objetoListaAutorizacionAcciCty.decMasivo = 0;
                string strgBinario = objetoListaAutorizacionAcciCty.BINARIO_ESTADO.Substring(0, 2);
                if (strgBinario == "11" || strgBinario == "10")
                {
                    objetoListaAutorizacionAcciCty.BINARIO_ESTADO = "1" +
                        objetoListaAutorizacionAcciCty.decMasivo + "00000000";
                }
                else if (strgBinario == "01" || strgBinario == "00")
                {
                    objetoListaAutorizacionAcciCty.BINARIO_ESTADO = "0" +
                        objetoListaAutorizacionAcciCty.decMasivo + "00000000";
                }
            }
            else if (strgDatosSpliteado[1] == "ECG")
            {
                if (strgDatosSpliteado[2] == "true")
                    objetoListaAutorizacionAcciCty.decExcluye = 1;
                else
                    objetoListaAutorizacionAcciCty.decExcluye = 0;
            }
            else if (strgDatosSpliteado[1] == "OBS")
            {
                objetoListaAutorizacionAcciCty.OBSERVACIONES = strgDatosSpliteado[4].ToUpper();
            }
            return objetoListaAutorizacionAcciCty;
        }
        #endregion

        protected void dgvTipoOperacion_BatchUpdate(object sender, DevExpress.Web.Data.ASPxDataBatchUpdateEventArgs e)
        {

        }
        protected void panelDatosValidacion_Callback(object sender, CallbackEventArgsBase e)
        {
            try
            {

                if (e.Parameter != null)
                {
                    if (MValidaSesion())
                    {
                        try
                        {
                            #region Proceso a Ejecutar
                            O_LISTA_AUTORIZACION_ACCI_CTY lstFinalTo = MRecuperaTipoOperacionUnico(Convert.ToString(e.Parameter));
                            if (lstFinalTo != null)
                            {
                                MImprimirMensaje("Los tipos de operación no estan definidos.", EnumTiposMensajeAlerta.Error, true);
                            }
                            else
                            {
                                if (Convert.ToInt64(txtIdConsumidor.Text) > 0)
                                {
                                    MGrabaTipoOperacion(Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()), Convert.ToDecimal(txtIdConsumidor.Text), lstFinalTo);
                                }
                                else
                                {
                                    for (int i = 0; i < dgvEntidades.VisibleRowCount; i++)
                                    {
                                        decimal decIdConsumidor = Convert.ToDecimal(dgvEntidades.GetRowValues(i, "ID_CONSUMIDOR").ToString());
                                        MGrabaTipoOperacion(Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()), decIdConsumidor, lstFinalTo);
                                    }
                                }
                            }
                            pucConfig_WindowCallback(null, null);
                            #endregion
                        }
                        catch (Exception exp)
                        {
                            #region RegistraError

                            MImprimirMensaje(exp.Message, EnumTiposMensajeAlerta.Error, true);
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
            }
            catch (Exception ex)
            {
                CLog.Error(this.Context, GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }
        protected void panelDatosValidacionRegistro_Callback(object sender, CallbackEventArgsBase e)
        {
            try
            {

                if (e.Parameter != null)
                {
                    if (MValidaSesion())
                    {
                        try
                        {
                            #region Proceso a Ejecutar
                            O_LISTA_AUTORIZACION_ACCI_CTY lstFinalTo = MRecuperaTipoOperacionUnico(Convert.ToString(e.Parameter));
                            if (lstFinalTo == null)
                            {
                                MImprimirMensaje("Los tipos de operación no estan definidos.", EnumTiposMensajeAlerta.Error, true);
                            }
                            else
                            {
                                if (Convert.ToInt64(txtIdConsumidor.Text) > 0)
                                {
                                    MGrabaTipoOperacion(Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()), Convert.ToDecimal(txtIdConsumidor.Text), lstFinalTo);
                                }
                                else
                                {
                                    //List<O_LISTA_AUTORIZACION_ACCI_CTY> listaAutorizacionAcciCtys = Session[CVariablesSesion.GrillaAcumulado] != null ? (List<O_LISTA_AUTORIZACION_ACCI_CTY>)Session[CVariablesSesion.GrillaAcumulado] : new List<O_LISTA_AUTORIZACION_ACCI_CTY>();
                                    //string[] resultado = e.Parameter.Split('|');
                                    //O_LISTA_AUTORIZACION_ACCI_CTY objReg = listaAutorizacionAcciCtys.FirstOrDefault(w => w.ID_TIPO_OPERACION == Convert.ToDecimal(resultado[0]));
                                    //decimal decIdConsumidor = objReg.ID_CONSUMIDOR;


                                    for (int i = 0; i < dgvEntidades.VisibleRowCount; i++)
                                    {
                                        decimal decIdConsumidor = Convert.ToDecimal(dgvEntidades.GetRowValues(i, "ID_CONSUMIDOR").ToString());
                                        MGrabaTipoOperacion(Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()), decIdConsumidor, lstFinalTo);
                                    }
                                }
                            }
                            pucConfig_WindowCallback(null, null);
                            #endregion
                        }
                        catch (Exception exp)
                        {
                            #region RegistraError

                            MImprimirMensaje(exp.Message, EnumTiposMensajeAlerta.Error, true);
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
            }
            catch (Exception ex)
            {
                CLog.Error(this.Context, GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }
        protected void chkMasivo_Init(object sender, EventArgs e)
        {

            ASPxCheckBox checkBox = sender as ASPxCheckBox;
            GridViewDataItemTemplateContainer container = checkBox.NamingContainer as GridViewDataItemTemplateContainer;
            checkBox.Checked = container.Grid.Selection.IsRowSelected(container.VisibleIndex);

            checkBox.ClientInstanceName = string.Format("chkMasivo_{0}", container.KeyValue);
            checkBox.Value = container.KeyValue;
            checkBox.ClientSideEvents.CheckedChanged = string.Format("function(s, e){{ cbSelectedHIM_CheckedChanged(s, {0}); }}", container.KeyValue);
            //checkBox.ClientSideEvents.CheckedChanged = string.Format("function(s, e){{ cbSelected_CheckedChanged(s, {0}); }}", container.VisibleIndex);
        }
        protected void chkFuncion_Init(object sender, EventArgs e)
        {

            ASPxCheckBox checkBox = sender as ASPxCheckBox;
            GridViewDataItemTemplateContainer container = checkBox.NamingContainer as GridViewDataItemTemplateContainer;
            checkBox.Checked = container.Grid.Selection.IsRowSelected(container.VisibleIndex);

            checkBox.ClientInstanceName = string.Format("chkFuncion_{0}", container.KeyValue);
            checkBox.Value = container.KeyValue;
            checkBox.ClientSideEvents.CheckedChanged = string.Format("function(s, e){{ cbSelectedHFDM_CheckedChanged(s, {0}); }}", container.KeyValue);
            //checkBox.ClientSideEvents.CheckedChanged = string.Format("function(s, e){{ cbSelected_CheckedChanged(s, {0}); }}", container.VisibleIndex);
        }
        protected void chkExcluye_Init(object sender, EventArgs e)
        {

            ASPxCheckBox checkBox = sender as ASPxCheckBox;
            GridViewDataItemTemplateContainer container = checkBox.NamingContainer as GridViewDataItemTemplateContainer;
            checkBox.Checked = container.Grid.Selection.IsRowSelected(container.VisibleIndex);

            checkBox.ClientInstanceName = string.Format("chkExcluye_{0}", container.KeyValue);
            checkBox.Value = container.KeyValue;
            checkBox.ClientSideEvents.CheckedChanged = string.Format("function(s, e){{ cbSelectedECG_CheckedChanged(s, {0}); }}", container.KeyValue);
            //checkBox.ClientSideEvents.CheckedChanged = string.Format("function(s, e){{ cbSelected_CheckedChanged(s, {0}); }}", container.VisibleIndex);
        }

        protected void txtObservacion_Init(object sender, EventArgs e)
        {
            //ASPxTextBox textBoxEdit = sender as ASPxTextBox;
            //GridViewDataItemTemplateContainer container = textBoxEdit.NamingContainer as GridViewDataItemTemplateContainer;
            ////textBoxEdit.Text = container.Grid.Selection.IsRowSelected(container.VisibleIndex);

            //textBoxEdit.ClientInstanceName = string.Format("txtObservacion_{0}", container.KeyValue);
            //textBoxEdit.Value = container.KeyValue;
            //textBoxEdit.ClientSideEvents.TextChanged = string.Format("function(s, e){{ txtObservacion_TextChanged(s, {0}); }}", container.VisibleIndex);

            ASPxTextBox textBox = sender as ASPxTextBox;
            GridViewDataItemTemplateContainer container = textBox.NamingContainer as GridViewDataItemTemplateContainer;

            textBox.JSProperties["cpHFKey"] = String.Format("{0}_txtCountItem_{1}", container.Grid.ClientID, container.KeyValue);
            textBox.ClientSideEvents.TextChanged = string.Format("function(s, e){{ txtObservacion_TextChanged(s, {0}); }}", container.KeyValue);

            //textBox.ClientIDMode = ClientIDMode.Static;



        }

        protected void pucImportaMov_WindowCallback(object source, PopupWindowCallbackArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    txtIdConsumidorMov.Text = "";
                    txtEessHydro.Text = "";
                    detFechaImpIni.Text = "";
                    detFechaImpFin.Text = "";
                    int index = dgvEntidades.FocusedRowIndex;
                    txtIdConsumidorMov.Text = dgvEntidades.GetRowValues(index, "ID_CONSUMIDOR").ToString();
                    txtEessHydro.Text = dgvEntidades.GetRowValues(index, "DENOMINACION").ToString();                    
                    string strDenominacionEess = "";                    
                    string strTipoOp = dgvEntidades.GetRowValues(index, "ID_ACTIVIDAD").ToString() == "2" ? "GNV" : "LIQ";
                    chkAccion.Checked = strTipoOp == "GNV" ? true : strTipoOp == "LIQ" ? true : false;
                    chkAccion.Text = "Actividad (" + strTipoOp + ")";
                    if (txtEessHydro.Text != "")
                    {
                        strDenominacionEess = txtEessHydro.Text.ToUpper().Replace("\"", "");
                        var objResultado = _consulta.obtieneEessBsa(
                        new EObjetoEnvio2ParametrosProto
                        {
                            strCredencial = CParametrosHydro.strCredencialFuncionario,
                            decFiltro = 1,
                            strParametro1 = txtIdConsumidorMov.Text,    //strDenominacionEess,
                            strParametro2 = strTipoOp                   
                        });
                        if (objResultado.intCodigo == 1)
                        {
                            cmbEessBsa.DataSource = objResultado.oResultado;
                            cmbEessBsa.DataBind();
                            if (cmbEessBsa.DataSource != null)
                            {
                                cmbEessBsa.SelectedIndex = 0;
                                var lstEessBsa = ((List<O_LISTA_ESS_BSA_CTY>)objResultado.oResultado).Where(l => l.ID_EESS == Convert.ToDecimal(cmbEessBsa.Value)).ToList();
                                txtIdConsumidorBsa.Text = lstEessBsa[0].ID_CONSUMIDOR.ToString();        
                            }
                            else
                            { txtIdConsumidorBsa.Text = "0"; }                                                                            
                        }
                        else
                        {
                            cmbEessBsa.DataSource = null;                            
                            cmbEessBsa.DataBind();
                            cmbEessBsa.Text = "";
                            txtIdConsumidorBsa.Text = "";
                            MImprimirMensaje(objResultado.strMensaje, EnumTiposMensajeAlerta.Error, true);
                            //return;
                        }
                    }
                    else
                    {
                        //A Implementar
                    }
                    #endregion
                }
                catch (Exception exp)
                {
                    #region RegistraError

                    MImprimirMensaje(exp.Message, EnumTiposMensajeAlerta.Error, true);
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

        protected void btnImportarMov_Click(object sender, EventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    if (detFechaImpIni.Text == "" || detFechaImpFin.Text == ""){                     
                        MImprimirMensaje("Introduzca fechas validas.", EnumTiposMensajeAlerta.Alerta, true);
                        return;
                    }
                    else if (detFechaImpFin.Date < detFechaImpIni.Date)
                    {
                        MImprimirMensaje("Fechas incorrectas.", EnumTiposMensajeAlerta.Alerta, true);
                        return;
                    }
                    else if (detFechaImpIni.Date.Year + detFechaImpIni.Date.Month != detFechaImpFin.Date.Year + detFechaImpFin.Date.Month)
                    {
                         MImprimirMensaje("Fechas fuera de intérvalo. Debe escoger fechas dentro del mismo periodo.", EnumTiposMensajeAlerta.Alerta, true);
                        return;
                    }
                    if (cmbEessBsa.Value.ToString() == "")
                    {
                        MImprimirMensaje("Seleccion Introduzca la unidad de medida.", EnumTiposMensajeAlerta.Alerta, true);
                        return;
                    }
                    else if (txtIdConsumidorMov.Text != txtIdConsumidorBsa.Text)
                    {
                        MImprimirMensaje("El IdConsumidor BSA difiere con el IdConsumidor del OCT.", EnumTiposMensajeAlerta.Alerta, true);
                        return;
                    }
                    else
                    {
                        var decFechaIni = detFechaImpIni.Date.Year + detFechaImpIni.Date.Month.ToString().PadLeft(2, '0') + detFechaImpIni.Date.Day.ToString().PadLeft(2, '0') + "000000" ; //"20170101", //fechaInicio
                        var decFechaFin = detFechaImpFin.Date.Year + detFechaImpFin.Date.Month.ToString().PadLeft(2, '0') + detFechaImpFin.Date.Day.ToString().PadLeft(2, '0') + "235959" ; //"20170131" //fechaFinal
                        O_RESULTADO_CTY obResultad = new O_RESULTADO_CTY();
                        var objResultado = _gestion.ImportaMovimientoBSA(CParametrosHydro.strCredencialOctanoVolumen, Convert.ToDecimal(cmbEessBsa.Value), Convert.ToDecimal(txtIdConsumidorMov.Text), Convert.ToDecimal(decFechaIni), Convert.ToDecimal(decFechaFin));
                        if (objResultado != null)
                        {
                            obResultad.ID_TABLA = objResultado.intCodigo;
                            obResultad.MENSAJE_ERROR = objResultado.strMensaje;
                            MImprimirMensaje(objResultado.strMensaje, EnumTiposMensajeAlerta.Correcto, true);
                            pucImportaMov.ShowOnPageLoad = false;
                        }
                        else {
                               
                        }
                        pucImportaMov_WindowCallback(null, null);                  
                    }

                    #endregion
                }
                catch (Exception exp)
                {
                    #region RegistraError

                    MImprimirMensaje(exp.Message, EnumTiposMensajeAlerta.Error, true);
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


    }
}