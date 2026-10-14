
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Web;
using AnhHydroOctanoPersistencia.Aplicacion.Consulta;
using AnhHydroOctanoPersistencia.Aplicacion.Gestion;
using AnhHydroOctanoPersistencia.Entidades;
using AnhHydroOctanoPresentacion.Clases.Controles;
using AnhHydroOctanoPresentacion.Clases.Util;
using AnhHydroOctanoPresentacion.Entidades;
using AnhHydroOctanoPresentacion.Enumeradores;
using DevExpress.Web;
using ServiceStack.ServiceClient.Web;


using System.Web.UI;
using AnhHydroOctanoPresentacion.Lib;
using ServiceStack.Text;

namespace AnhHydroOctanoPresentacion.Sitio.Movimientos
{
    using System;
    using System.IO;
    using System.Web.Security;
    using Parametros;
    using Librerias.Anh.Us;

    public partial class WFImcidentes : System.Web.UI.Page
    {
        protected void page_Init()
        {
            //try
            //{
            //    string strMensajeError = null;
            //    if (CConsultaAccesos.AccesoFormulario(CParametrosHydro.strCredencialHydroAdmin,
            //        Convert.ToDecimal(CVariablesSesion.UsuarioSession(Context).ID_USUARIO),
            //        Convert.ToDecimal(CParametrosHydro.decIdAplicacion),
            //        Path.GetFileName(Request.Path),
            //        ref strMensajeError)) return;

            //    FormsAuthentication.SignOut();
            //    Response.Cookies.Remove(CVariablesSesion.IdAutenticacion);
            //    Session.RemoveAll();
            //    Response.Redirect("~/Sitio/Persona/wfAutenticacion.aspx");
            //}
            //catch (Exception ex)
            //{
            //    FormsAuthentication.SignOut();
            //    Response.Cookies.Remove(CVariablesSesion.IdAutenticacion);
            //    Session.RemoveAll();
            //    Response.Redirect("~/Sitio/Persona/wfAutenticacion.aspx");
            //}
        }

        //protected void Page_Load(object sender, EventArgs e)
        //{

        //}

        //INICIO
        #region Variables de entorno

        readonly JsonServiceClient _client = new JsonServiceClient(CAppSettings.servicio_octVolumen);
        readonly JsonServiceClient _clientI = new JsonServiceClient(CAppSettings.servicio_octInfraestructura);
        private readonly CPersistenciaConsulta _consulta = new CPersistenciaConsulta();
        private readonly CPersistenciaGestion _gestion = new CPersistenciaGestion();
        readonly CUtil _cUtil = new CUtil();

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
                        Session.Remove(CVariablesSesion.ArchivoDescarga);
                        Session.Remove(CVariablesSesion.Callback);
                        Session.Remove(CVariablesSesion.EstructuraArbol);
                        Session.Remove(CVariablesSesion.GrillaRegObservado);
                        Session.Remove(CVariablesSesion.GrillaTransaccion);
                        Session.Remove(CVariablesSesion.ListaCatalogo);
                        Session.Remove(CVariablesSesion.ListaEntidad);
                        Session.Remove(CVariablesSesion.ListaInfraestructura);
                        Session.Remove(CVariablesSesion.ListaParametrica);
                        Session.Remove(CVariablesSesion.ListaParamGral);
                        Session.Remove(CVariablesSesion.ListaParamInfra);
                        Session.Remove(CVariablesSesion.ReportePrincipal);
                        Session.Remove(CVariablesSesion.TipoReporte);
                        Session.Remove(CVariablesSesion.IdEntidadRevision);

                        Session.Remove(CVariablesSesion.ComboTipoOperacion);

                        Session[CVariablesSesion.Callback] = "0";
                        Session["listaIncidentes"] = null;
                        Session["objResultado"] = null;
                        Session["objRegistro"] = null;
                        Session["accion"] = null;
                        MCargaArbol();
                        MCargaParametricas();

                        MCargaPermisos();
                        MConfigurarFormulario();

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

                    if (tlsOperadores.DataSource == null && Session[CVariablesSesion.EstructuraArbol] != null)
                    {
                        tlsOperadores.DataSource = Session[CVariablesSesion.EstructuraArbol];
                        tlsOperadores.DataBind();
                    }
                    else
                    {
                        //todo no implementado
                    }

                    if (dgvIncidentes.IsCallback)
                    {
                        dgvIncidentes.DataSource = Session["listaIncidentes"] != null ? (List<O_INCIDENTES_CTY>)Session["listaIncidentes"] : new List<O_INCIDENTES_CTY>();
                        dgvIncidentes.DataBind();
                    }
                    else
                    {
                        //todo No es carga inicial: no implementado
                    }

                    //if (dgvTransaccionError.DataSource == null && Session[CVariablesSesion.GrillaRegObservado] != null)
                    //{
                    //    dgvTransaccionError.DataSource = Session[CVariablesSesion.GrillaRegObservado];
                    //    dgvTransaccionError.DataBind();
                    //}
                    //else
                    //{
                    //    //todo No es carga inicial: no implementado
                    //}

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
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    //cmbTipoOperacion.DataSource = null;
                    //cmbTipoOperacion.Text = "";

                    cmbPeriodo.Items.Clear();
                    cmbPeriodo.Text = "";

                    if (tlsOperadores.FocusedNode != null)
                    {
                        if ((int)tlsOperadores.FocusedNode.GetValue("IntAccion") > 0)
                        {
                            //PARAMETRICAS GENERALES
                            Session.Remove(CVariablesSesion.IdEntidadRevision);
                            var objParametros = _client.Post<EResultadoOctanoParametro>(CAppSettings.svc_listar_octano_parametrica,
                            new
                            {
                                strCredencial = CParametrosHydro.strCredencialFuncionario,
                                decFiltro = 2,//1: ID_TIPO_ACTIVIDAD 2: ID_TIPO_ACTIVIDAD y VALOR_TIPO
                                strParametro1 = "9",//VALOR_TIPO
                                strParametro2 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
                                strParametro3 = "",
                                strParametro4 = ""
                            });
                            if (objParametros.OResultado.Count > 0 && objParametros.IntCodigo == 1)
                            {
                                Session[CVariablesSesion.IdEntidadRevision] = objParametros.OResultado.FirstOrDefault().VALOR_FIN;

                                if (Session[CVariablesSesion.IdEntidadRevision] != null)
                                {
                                    var objEntidadRel = _consulta.ObtenerEntidades(
                                    new EObjetoEnvio5ParametrosProto
                                    {
                                        decFiltro = 2,
                                        strParametro1 = "",
                                        strParametro2 = (string)Session[CVariablesSesion.IdEntidadRevision],
                                        strParametro3 = CAppSettings.idAplicacion,
                                        strParametro4 = "",
                                        strParametro5 = ""
                                    });
                                    if (objEntidadRel.intCodigo == 1)
                                    {
                                        var lstEntidadRel = (List<O_OBTIENE_ENTIDADES_CTY>)objEntidadRel.oResultado;
                                        var objEntidades = _client.Post<EResultadoConfigHidrocarburo>(CAppSettings.svc_listar_autorizacion_condicion,
                                        new
                                        {
                                            strCredencial = CParametrosHydro.strCredencialFuncionario,
                                            decFiltro = 3,
                                            strParametro1 = Convert.ToInt32(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString()),
                                            strParametro2 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
                                            strParametro3 = ""
                                        });
                                        if (objEntidades.IntCodigo == 1)
                                        {
                                            var lstAlias = objEntidades.OResultado.Where(x => x.DESCRIPCION != null).ToList();
                                            if (lstAlias.Any())
                                            {
                                                foreach (var reg in lstAlias)
                                                {
                                                    lstEntidadRel.Where(w => w.ID_ACTIVIDAD == reg.ID_SUB_IDENTIFICADOR && w.ID_CONSUMIDOR == reg.ID_IDENTIFICADOR).ToList().ForEach(f => f.USUARIO_ASIGNADO = reg.DESCRIPCION);
                                                }
                                            }
                                        }
                                        Session[CVariablesSesion.ListaEntidad] = lstEntidadRel;
                                    }
                                }
                            }

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
                                int decCod;
                                foreach (var dato in objPeriodo.OResultado)
                                {
                                    decCod = Convert.ToInt32(dato.decAño + dato.decMes.ToString().PadLeft(2, '0'));
                                    cmbPeriodo.Items.Add(dato.strPeriodo, decCod);
                                }

                                decCod = Convert.ToInt32(objPeriodo.OResultado[0].decAño + objPeriodo.OResultado[0].decMes.ToString().PadLeft(2, '0'));
                                cmbPeriodo.Value = decCod;

                                //var objTipoOperacion = _client.Post<EResultadoTipoOperacion>(CAppSettings.svc_obtener_tipos_operacion_act,
                                //new
                                //{
                                //    decFiltro = 1,
                                //    strParametro1 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
                                //    strParametro2 = "",
                                //    strParametro3 = ""
                                //});
                                //if (objTipoOperacion.IntCodigo == 1)
                                //{
                                //    objTipoOperacion.OResultado = objTipoOperacion.OResultado.OrderByDescending(x => x.FACTOR_CALCULO).ThenBy(y => y.NOMBRE).ToList();
                                //    cmbTipoOperacion.DataSource = objTipoOperacion.OResultado;
                                //    cmbTipoOperacion.DataBind();
                                //    Session[CVariablesSesion.ComboTipoOperacion] = objTipoOperacion.OResultado;
                                //}
                                //else
                                //{
                                //    MImprimirMensaje(objTipoOperacion.StrMensaje, EnumTiposMensajeAlerta.Error);
                                //}
                            }
                            else
                            {
                                MImprimirMensaje(objPeriodo.StrMensaje, EnumTiposMensajeAlerta.Error);
                            }
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
                Session[CVariablesSesion.Callback] = "0";
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
                    MMuestraCabecera();
                    MLimpiaGrilla();

                    if ((int)tlsOperadores.FocusedNode.GetValue("IntAccion") > 0)
                    {
                        cargarlistaIncidentes();
                        //string strMensaje = string.Empty;
                        //if (MValidaDatosIniciales(ref strMensaje))
                        //{
                        //    if (MValidaCondicionesFuncionamiento(ref strMensaje))
                        //    {
                        //        MCargaInfraestructura();
                        //        btnImportar.Enabled = true;
                        //        btnLimpiaTo.Enabled = true;
                        //    }
                        //    else
                        //    {
                        //        MImprimirMensaje(strMensaje, EnumTiposMensajeAlerta.Error);
                        //    }
                        //}
                        //else
                        //{
                        //    MImprimirMensaje(strMensaje, EnumTiposMensajeAlerta.Alerta);
                        //}
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
                //todo No implementado.
            }
        }

        private void cargarlistaIncidentes()
        {
            decimal deciIdActividad = -1;
            decimal deciIdTipoIncidente = -1;
            decimal decidecIdPeridod = -1;
            decimal deciIdGestion = -1;
            decimal deciIdOperador = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString());
            var objResultado = _consulta.ObtenerIncidentes(
                new EObjetoEnvioIncidentes
                {
                    strCredencial = CParametrosHydro.strCredencialFuncionario,
                    decIdActividad = deciIdActividad,
                    decIdTipoIncidente = deciIdTipoIncidente,
                    decIdPeridod = decidecIdPeridod,
                    decIdGestion = deciIdGestion,
                    deciIdOperador = deciIdOperador
                });
            if (objResultado.intCodigo == 1)
            {
                List<O_INCIDENTES_CTY> lstDato = (List<O_INCIDENTES_CTY>)objResultado.oResultado;
                dgvIncidentes.DataSource = lstDato;
                dgvIncidentes.DataBind();
                Session["listaIncidentes"] = null;
                Session["listaIncidentes"] = lstDato;
            }

        }

        protected void dgvTransacciones_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            //dgvTransacciones.DataSource = null;
            //dgvTransacciones.DataBind();
        }
        protected void dgvIncidentes_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters == "registro")
            {
                Session["objResultado"] = registrarIncidente();
            }
            else
            {
                string[] variables = e.Parameters.Split('-');
                if (variables[0] == "eliminar")
                {
                    Session["objResultado"] = eliminarItem(Convert.ToDecimal(variables[1]));
                }
            }
            cargarlistaIncidentes();
        }
        protected void panelMensajeError_Callback(object sender, CallbackEventArgsBase e)
        {
            try
            {
                if (Session["objResultado"] != null)
                {
                    O_RESULTADO_CTY objResultado = (O_RESULTADO_CTY)Session["objResultado"];
                    if (objResultado.ID_TABLA > 0)

                        MImprimirMensaje(objResultado.MENSAJE_ERROR, EnumTiposMensajeAlerta.Correcto);
                    else
                        MImprimirMensaje(objResultado.MENSAJE_ERROR, EnumTiposMensajeAlerta.Error);

                }
            }
            catch (Exception ex)
            {
                CLog.Error(this.Context, GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }
        protected void panelRegistroSeguimiento_Callback(object sender, CallbackEventArgsBase e)
        {
            try
            {
                cargarComboRegistro();
                cargarListaCalsificado();
                memoDescripcion.Text = "";
                txtAreaUnidad.Text = "";
                if (e.Parameter != "")
                {
                    string[] variables = e.Parameter.Split('-');
                    if (variables[0] == "moficiar")
                    {
                        List<O_INCIDENTES_CTY> lstDato = (List<O_INCIDENTES_CTY>)Session["listaIncidentes"];
                        O_INCIDENTES_CTY objRegistro =
                            lstDato.FirstOrDefault(w => w.ID_INCIDENTES == Convert.ToDecimal(variables[1]));
                        memoDescripcion.Text = objRegistro.DESCRIPCION;
                        txtAreaUnidad.Text = objRegistro.AREA_UYNIDAD;
                        deFechaIncio.Date = objRegistro.FECHA_INICIO_SUCESO;
                        dfFechaFin.Date = objRegistro.FECHA_FIN_SUCESO;
                        dfFechaRInicio.Date = objRegistro.FECHA_REINCIO;
                        Session["objRegistro"] = objRegistro;
                        Session["accion"] = "modificar";
                    }
                }
                else
                {
                    Session["accion"] = "registrar";
                }
            }
            catch (Exception ex)
            {
                CLog.Error(this.Context, GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }
        protected void btnDescargar_Click(object sender, EventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    //if ((int)tlsOperadores.FocusedNode.GetValue("IntIdActividad") == 0)
                    //{
                    //    MImprimirMensaje("Seleccione una actividad.", EnumTiposMensajeAlerta.Alerta);
                    //}
                    //else if (cmbTipoOperacion.Value == null)
                    //{
                    //    MImprimirMensaje("Seleccione un tipo de operación.", EnumTiposMensajeAlerta.Alerta);
                    //}
                    //else
                    //{
                    //    var cod = cmbTipoOperacion.Text.Split(';');
                    //    var strCodTp = cod[1].Substring(0, 4);
                    //    string fileName;

                    //    switch (strCodTp)
                    //    {
                    //        case "TERC":
                    //            fileName = "plantilla_pscaa_recepcion.xlsx";
                    //            break;
                    //        case "TEDS":
                    //            fileName = "plantilla_pscaa_despacho.xlsx";
                    //            break;
                    //        case "ELRC":
                    //            fileName = "plantilla_eessliq_recepcion.xlsx";
                    //            break;
                    //        case "ELDS":
                    //            fileName = "plantilla_eessliq_despacho.xlsx";
                    //            break;
                    //        case "EGDS":
                    //            fileName = "plantilla_eessgnv_despacho.xlsx";
                    //            break;
                    //        case "TARC":
                    //            fileName = "plantilla_tacl_recepcion.xlsx";
                    //            break;
                    //        case "TADS":
                    //            fileName = "plantilla_tacl_despacho.xlsx";
                    //            break;
                    //        case "PVRC":
                    //            fileName = "plantilla_ptoVentaliq_recepcion.xlsx";
                    //            break;
                    //        case "PVDS":
                    //            fileName = "plantilla_ptoVentaliq_despacho.xlsx";
                    //            break;
                    //        /*lmqc - importadores GLP*/
                    //        case "TERG":
                    //            fileName = "plantilla_glp_recepcion.xlsx";
                    //            break;
                    //        case "TEDG":
                    //            fileName = "plantilla_glp_despacho.xlsx";
                    //            break;

                    //        case "IPRC":
                    //            fileName = "plantilla_importacion_transporte.xlsx";
                    //            break;

                    //        default:
                    //            MImprimirMensaje("Actividad Invalida.", EnumTiposMensajeAlerta.Error);
                    //            return;
                    //    }
                    //    try
                    //    {
                    //        var response = HttpContext.Current.Response;
                    //        response.ClearContent();
                    //        response.Clear();
                    //        response.ContentType = "application/excel";
                    //        response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ";");
                    //        response.TransmitFile(Server.MapPath("~/Sitio/Plantillas/" + fileName));
                    //        response.Flush();
                    //        response.End();
                    //    }
                    //    catch (Exception ex)
                    //    {

                    //    }
                    //}

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

        private O_RESULTADO_CTY registrarIncidente()
        {
            O_RESULTADO_CTY obResultad = new O_RESULTADO_CTY();
            O_INCIDENTES_CTY objRegistro = new O_INCIDENTES_CTY();
            if (Session["accion"] != null)
            {
                if ((string)Session["accion"] == "modificar")
                    objRegistro = (O_INCIDENTES_CTY)Session["objRegistro"];

                objRegistro.ID_OPERADOR = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString());
                objRegistro.ID_ACTIVIDAD = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad"));
                objRegistro.ID_TIPO_INCIDENTE = Convert.ToDecimal(cmbTipoIncidente.Value);
                objRegistro.ID_AREA_UNIDAD = 1;
                objRegistro.AREA_UYNIDAD = txtAreaUnidad.Text;
                objRegistro.DESCRIPCION = memoDescripcion.Text;
                objRegistro.FECHA_INICIO_SUCESO = deFechaIncio.Date;
                objRegistro.FECHA_FIN_SUCESO = dfFechaFin.Date;
                objRegistro.FECHA_REINCIO = dfFechaRInicio.Date;
                objRegistro.APP_ID_USUARIO = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                string[] textoPerido = cmbPeriodo.Text.Split('/');
                objRegistro.PERIODO = Convert.ToDecimal(textoPerido[0]);
                objRegistro.GESTION = Convert.ToDecimal(textoPerido[1]);
                objRegistro.ID_CALSIFICADOR = Convert.ToDecimal(cmbClasificaIncident.Value);
                if ((string)Session["accion"] == "modificar")
                {
                    var objResultado = _gestion.ModificarIncidentes(CParametrosHydro.strCredencialOctanoVolumen,
                        objRegistro.ID_INCIDENTES,
                        objRegistro.ID_OPERADOR,
                        objRegistro.ID_ACTIVIDAD,
                        objRegistro.ID_TIPO_INCIDENTE,
                        objRegistro.ID_AREA_UNIDAD,
                        objRegistro.AREA_UYNIDAD,
                        objRegistro.DESCRIPCION,
                        objRegistro.FECHA_INICIO_SUCESO,
                        objRegistro.FECHA_FIN_SUCESO,
                        objRegistro.FECHA_REINCIO,
                        objRegistro.APP_ID_USUARIO,
                        objRegistro.PERIODO,
                        objRegistro.GESTION,
                        objRegistro.ID_CALSIFICADOR);
                    if (objResultado != null)
                    {
                        obResultad.ID_TABLA = objResultado.intCodigo;
                        obResultad.MENSAJE_ERROR = objResultado.strMensaje;
                    }
                }
                else
                {
                    var objResultado = _gestion.RegistraIncidentes(CParametrosHydro.strCredencialOctanoVolumen,
                        objRegistro.ID_OPERADOR,
                        objRegistro.ID_ACTIVIDAD,
                        objRegistro.ID_TIPO_INCIDENTE,
                        objRegistro.ID_AREA_UNIDAD,
                        objRegistro.AREA_UYNIDAD,
                        objRegistro.DESCRIPCION,
                        objRegistro.FECHA_INICIO_SUCESO,
                        objRegistro.FECHA_FIN_SUCESO,
                        objRegistro.FECHA_REINCIO,
                        objRegistro.APP_ID_USUARIO,
                        objRegistro.PERIODO,
                        objRegistro.GESTION
                        , objRegistro.ID_CALSIFICADOR);
                    if (objResultado != null)
                    {
                        obResultad.ID_TABLA = objResultado.intCodigo;
                        obResultad.MENSAJE_ERROR = objResultado.strMensaje;
                    }
                }
            }
            return obResultad;
        }
        private O_RESULTADO_CTY eliminarItem(decimal decIdRegistro)
        {
            O_RESULTADO_CTY obResultad = new O_RESULTADO_CTY();
            var objResultado = _gestion.EliminaIncidentes(CParametrosHydro.strCredencialOctanoVolumen, decIdRegistro,
                Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]));
            if (objResultado != null)
            {
                obResultad.ID_TABLA = objResultado.intCodigo;
                obResultad.MENSAJE_ERROR = objResultado.strMensaje;
            }
            return obResultad;
        }
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
        private void MCargaArbol()
        {
            CConsultasParametricas cConsultasParametricas = new CConsultasParametricas();
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
            lblMensajeError.Text = "   " + strMensaje + "   ";
        }
        /// <summary>
        /// METODO. MCargaParametricas
        /// </summary>
        private void MCargaParametricas()
        {
            var objParametricas = new O_PARAMETRICAS_GRAL();
            //PAISES
            var objUbicacion = _client.Post<EResultadoUbicacion>(CAppSettings.svc_obtiene_ubicacion,
            new
            {
                strCredencial = CParametrosHydro.strCredencialFuncionario,
                decNivel = 1,
                decIdPadre = 0
            });
            if (objUbicacion.IntCodigo == 1)
            {
                objParametricas.lstPais = objUbicacion.OResultado;
            }
            Session[CVariablesSesion.ListaParametrica] = objParametricas;

            //PARAM GLOBAL CLASIFICACION 1
            var lstObtCatalogo = _client.Post<EResultadoParametricaGlobal>(CAppSettings.svc_lista_parametrica_global,
            new
            {
                decIdDominio = 11
            });
            if (lstObtCatalogo.IntCodigo == 1)
            {
                Session[CVariablesSesion.ListaCatalogo] = lstObtCatalogo.OResultado;
            }
            else
            {
                //todo No implementado.
            }

            //TIPOS DE CONTENEDORES
            var objParamInfra = new O_PARAMETROS_INFRA();
            var objContenedor = _client.Post<EResultadoTipoContenedor>(CAppSettings.svc_lista_tipos_contenedores,
            new
            {
                strCredencial = CParametrosHydro.strCredencialFuncionario,
                decFiltro = 1
            });
            if (objContenedor.intCodigo == 1)
            {
                objParamInfra.TIPOS_CONTENEDORES = objContenedor.oResultado;
            }

            //TIPO INFRAESTRUCTURA
            var objParamInfraestrutura = _clientI.Get<EResultadoParamInfra>(CAppSettings.svc_LListadoParametros + "/" + CParametrosHydro.strCredencialInfra + "/DESC/TIPO_INFRAESTRUCTURA?format=json");
            if (objParamInfraestrutura.decCodigo == 1 && objParamInfraestrutura.oResultado != null && objParamInfraestrutura.oResultado.Any())
            {
                objParamInfra.TIPO_INFRAESTRUCTURA = objParamInfraestrutura.oResultado;
            }
            Session[CVariablesSesion.ListaParamInfra] = objParamInfra;
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
                                                                      "/WFImportacionDatos.aspx?format=json");
                if (lstResultado.decCodigo == 1)
                {
                    if (lstResultado.oResultado != null && lstResultado.oResultado.Any())
                    {
                        Session[CVariablesSesion.PermisoCons] = lstResultado.oResultado.FirstOrDefault().CONSULTA;
                        Session[CVariablesSesion.PermisoAlta] = lstResultado.oResultado.FirstOrDefault().ALTAS;
                        Session[CVariablesSesion.PermisoBaja] = lstResultado.oResultado.FirstOrDefault().BAJAS;
                    }
                    else
                    {
                        Session[CVariablesSesion.PermisoCons] = 0;
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
        /// METODO. ConfigurarFormulario
        /// </summary>
        private void MConfigurarFormulario()
        {
            //btnDescargar.Visible = Convert.ToInt16(Session[CVariablesSesion.PermisoCons]) != 0;
            //btnGrabar.Visible = Convert.ToInt16(Session[CVariablesSesion.PermisoAlta]) != 0;
            //btnLimpiaTo.Visible = Convert.ToInt16(Session[CVariablesSesion.PermisoBaja]) != 0;
        }
        /// <summary>
        /// METODO. MMuestraCabecera
        /// </summary>
        private void MMuestraCabecera()
        {
            lblRazonSocial.Text = tlsOperadores.FocusedNode.GetValue("StrDescPadre").ToString();
            lblNit.Text = tlsOperadores.FocusedNode.GetValue("LngNit").ToString();
            lblDireccion.Text = (tlsOperadores.FocusedNode.GetValue("StrDireccion") == null) ? "No Definido" : tlsOperadores.FocusedNode.GetValue("StrDireccion").ToString();
        }
        /// <summary>
        /// METODO. MLimpiaGrilla
        /// </summary>
        private void MLimpiaGrilla()
        {
            Session.Remove(CVariablesSesion.GrillaTransaccion);
            Session.Remove(CVariablesSesion.GrillaRegObservado);
            //btnImportar.Enabled = false;
            //btnLimpiaTo.Enabled = false;
            //btnGrabar.Enabled = false;
            pnlMensaje.Visible = false;
            //dgvTransacciones.DataSource = null;
            //dgvTransacciones.DataBind();
            //dgvTransaccionError.DataSource = null;
            //dgvTransaccionError.DataBind();
            //lblTipoError.Text = "";
            dgvIncidentes.DataSource = null;
            dgvIncidentes.DataBind();
        }

        private void cargarComboRegistro()
        {
            string strgCredencial = "";
            string decIdParParamDomi = "TIPINCI";
            EPResultado objResultado = _consulta.ObtenerListTipoIncidente(strgCredencial, decIdParParamDomi);
            List<O_TIP_INCIDENTE_CTY> objGlobClas1 = (List<O_TIP_INCIDENTE_CTY>)objResultado.oResultado;
            cmbTipoIncidente.DataSource = objGlobClas1;
            cmbTipoIncidente.ValueField = "ID_PARAMETRICA_GLOBAL";
            //cmbTipoIncidente.TextField = "DESCRIPCION";
            cmbTipoIncidente.DataBind();
        }
        private void cargarListaCalsificado()
        {
            decimal decIdParParamDomi = 73;
            var lstObtCatalogo = _client.Post<EResultadoParametricaGlobal>
            (CAppSettings.svc_lista_parametrica_global, new { decIdDominio = decIdParParamDomi });
            string strgCredencial = "";
            List<O_PARAMETRICA_GLOBAL_CTY> listaParametro = lstObtCatalogo.OResultado;
            cmbClasificaIncident.DataSource = listaParametro;
            //cmbTipoIncidente.ValueField = "ID_PARAMETRICA_GLOBAL";
            //cmbTipoIncidente.TextField = "DESCRIPCION";
            cmbClasificaIncident.DataBind();
        }



        #endregion
    }
}