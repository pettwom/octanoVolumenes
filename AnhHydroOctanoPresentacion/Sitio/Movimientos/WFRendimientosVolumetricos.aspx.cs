
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
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
using DevExpress.DashboardCommon.Native;
using DevExpress.Web.Data;
using Newtonsoft.Json;
using ServiceStack.Text;

namespace AnhHydroOctanoPresentacion.Sitio.Movimientos
{
    using System;
    using System.IO;
    using System.Web.Security;
    using Parametros;
    using Librerias.Anh.Us;

    public partial class WFRendimientosVolumetricos : System.Web.UI.Page
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
        //public static string listaReporteSession = "listaReporteSession";
        private void reporte()
        {
            string[] textoPerido = cmbPeriodo.Text.Split('/');
            string strgNombre = tlsOperadores.FocusedNode.GetValue("strDescripcion").ToString() + "_" + textoPerido[0] + "_" + textoPerido[1];
            Session[CVariablesSesion.listaReporteSession] = (List<O_RENDIMIENTO_CTY>)Session["listaSessionResultRendimiento"];
            ASPxWebControl.RedirectOnCallback(string.Format("~/Sitio/Movimientos/WFDescargaRendimVolumetrico.aspx?ID={0}", strgNombre));
        }

        protected void pnlDatos_Callback(object sender, CallbackEventArgsBase e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    if (e.Parameter == "reporte")
                    {
                        reporte();
                    }
                    else
                    {
                        Session[CVariablesSesion.EntidadSeleccionada] = tlsOperadores.FocusedNode.GetValue("StrUbicacion");
                        MMuestraCabecera();
                        MLimpiaGrilla();
                        if ((int)tlsOperadores.FocusedNode.GetValue("IntAccion") > 0)
                        {
                            if (e.Parameter == "registro")
                            {
                                Session["objResultado"] = registrarIncidente();
                            }
                            //else
                            //{
                            //    string[] variables = e.Parameter.Split('-');
                            //    if (variables[0] == "eliminar")
                            //    {
                            //        Session["objResultado"] = eliminarItem(Convert.ToDecimal(variables[1]));
                            //    }
                            //}
                            cargarlistaIncidentes();
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
        private void cargarlistaIncidentes()
        {
            btnAgregarRegistro.Visible = false;
            btnModificarRegistro.Visible = false;
            btnReporte.Visible = true;
            List<O_RENDIMIENTO_CTY> lstDato = new List<O_RENDIMIENTO_CTY>();
            if (VerificaObtienelistaIncial())
            {
                btnModificarRegistro.Visible = true;
                lstDato = (List<O_RENDIMIENTO_CTY>)Session["listaSessionResultRendimiento"];
            }
            else
            {
                btnAgregarRegistro.Visible = true;
                Session["listaSessionResultRendimiento"] = lstDato = obtienelistaLocalRendimientos();
            }
            dgvIncidentes.DataSource = lstDato.OrderBy(w => w.ID_RENDIMIEN);
            dgvIncidentes.DataBind();
            //Session["listaIncidentes"] = lstDato;
        }

        protected void dgvTransacciones_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            //dgvTransacciones.DataSource = null;
            //dgvTransacciones.DataBind();
        }



        protected void dgvIncidentesRegistro_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters == "registro")
            {
                //Session["objResultado"] = registrarIncidente();
                Session["objResultado"] = registroIncidentePorcentVolum();
            }
            //else
            //{
            //    string[] variables = e.Parameters.Split('-');
            //    if (variables[0] == "eliminar")
            //    {
            //        Session["objResultado"] = eliminarItem(Convert.ToDecimal(variables[1]));
            //    }
            //}
            cargarlistaIncidentes();
        }

        private O_RESULTADO_CTY registroIncidentePorcentVolum()
        {
            O_RESULTADO_CTY obResultad = new O_RESULTADO_CTY();

            List<O_RENDIMIENTO_VOLUM_CTY> listaRendimientoProcIndidenteGlob =
                Session["listaSessionRendimPorcenVolum"] != null
                    ? (List<O_RENDIMIENTO_VOLUM_CTY>)Session["listaSessionRendimPorcenVolum"]
                    : new List<O_RENDIMIENTO_VOLUM_CTY>();


            //List<O_RENDIMIENTO_CTY> lstDato = (List<O_RENDIMIENTO_CTY>)Session["listaSessionResultRendimiento"];

            List<O_RENDIMIENTO_VOLUM_CTY> listaRegistro =
                Session["listaSessionRegistroRenPorcVol"] != null ? (List<O_RENDIMIENTO_VOLUM_CTY>)Session["listaSessionRegistroRenPorcVol"] : new
                List<O_RENDIMIENTO_VOLUM_CTY>();
            listaRendimientoProcIndidenteGlob = listaRendimientoProcIndidenteGlob
                .Where(w => w.ID_RENDIMIEN != Convert.ToDecimal((string)Session["idSessionRegRegistro"])).ToList();
            foreach (O_RENDIMIENTO_VOLUM_CTY itemLista in listaRegistro)
                listaRendimientoProcIndidenteGlob.Add(itemLista);

            List<O_RENDIMIENTO_CTY> listObjRendimiento = (List<O_RENDIMIENTO_CTY>)Session["listaSessionResultRendimiento"];
            O_RENDIMIENTO_CTY listaAgregaRendimprocenvolum = new O_RENDIMIENTO_CTY();
            listaAgregaRendimprocenvolum = listObjRendimiento.FirstOrDefault(w => w.ID_RENDIMIEN == Convert.ToDecimal((string)Session["idSessionRegRegistro"]));
            listObjRendimiento = listObjRendimiento.Where(w => w.ID_RENDIMIEN != Convert.ToDecimal((string)Session["idSessionRegRegistro"])).ToList();
            string textMostrar = "";
            foreach (O_RENDIMIENTO_VOLUM_CTY itemLista in listaRegistro)
                if (textMostrar == "")
                {
                    textMostrar = itemLista.PRODUCTO + " = " + itemLista.VOLUMEN;
                }
                else
                {
                    textMostrar = textMostrar + " , " + itemLista.PRODUCTO + " = " + itemLista.VOLUMEN;
                }
            listaAgregaRendimprocenvolum.RENDIMIENTO_PORCENT_VOLUMETRICO = textMostrar;
            listaAgregaRendimprocenvolum.oRendimientoVolumetricoCty = listaRegistro;
            listObjRendimiento.Add(listaAgregaRendimprocenvolum);
            Session["listaSessionResultRendimiento"] = listObjRendimiento;
            return obResultad;
        }

        protected void Grid_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
        {
            //UpdateItem(e.Keys, e.NewValues);
            var id = Convert.ToInt32(e.Keys["ID"]);
            List<O_RENDIMIENTO_CTY> listObjRendimiento = Session["listaRegistroSession"] != null ? (List<O_RENDIMIENTO_CTY>)Session["listaRegistroSession"] : new List<O_RENDIMIENTO_CTY>();

            var item = listObjRendimiento.First(i => i.ID_RENDIMIEN == id);
            //double sss1 = e.NewValues;
            //GridDataItem item
            //item.C1 = Convert.ToInt32(values["C1"]);
            //item.C2 = Convert.ToDouble(values["C2"]);
            //item.C3 = Convert.ToString(values["C3"]);
            //item.C4 = Convert.ToBoolean(values["C4"]);
            //item.C5 = Convert.ToDateTime(values["C5"]);
            CancelEditing(e);
        }
        protected void CancelEditing(CancelEventArgs e)
        {
            e.Cancel = true;
            dgvIncidentes.CancelEdit();
        }
        protected void Grid_BatchUpdate(object sender, ASPxDataBatchUpdateEventArgs e)
        {
            //if (!BatchUpdateCheckBox.Checked)
            //    return;

            //foreach (var args in e.InsertValues)
            //    InsertNewItem(args.NewValues);
            //foreach (var args in e.UpdateValues)
            //    UpdateItem(args.Keys, args.NewValues);
            //foreach (var args in e.DeleteValues)
            //    DeleteItem(args.Keys, args.Values);

            e.Handled = true;
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

        private bool VerificaObtienelistaIncial()
        {
            bool existenRegistros = false;
            //List<O_RENDIMIENTO_CTY> listObjRendimiento = new List<O_RENDIMIENTO_CTY>();
            string strgCredencial = "";
            decimal decIdOperador = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString());
            string[] textoPerido = cmbPeriodo.Text.Split('/');
            if (textoPerido[0] != "")
            {
                decimal decPerido = Convert.ToDecimal(textoPerido[0]);
                decimal decGestion = Convert.ToDecimal(textoPerido[1]);
                EPResultado objResultado = _consulta.ObtenerRendimientos(strgCredencial, decIdOperador, decPerido, decGestion);
                if (objResultado.intCodigo > 0)
                {
                    existenRegistros = true;//Session["listaSessionResultRendimiento"]
                    List<O_RENDIMIENTO_VOL_CTY> objetoResultadoRes = (List<O_RENDIMIENTO_VOL_CTY>)objResultado.oResultado;
                    O_RENDIMIENTO_VOL_CTY Obj = objetoResultadoRes.FirstOrDefault();
                    O_OBJ_RENDIMIENTOS_VOLUM_CTY objServicio = JsonConvert.DeserializeObject<O_OBJ_RENDIMIENTOS_VOLUM_CTY>(Obj.JSON_REGIS_MENSUALES);
                    List<O_RENDIMIENTO_CTY> objJson = new List<O_RENDIMIENTO_CTY>();
                    objJson = objServicio.oRendimientoVolumCty;
                    Session["listaSessionResultRendimiento"] = objJson;
                    Session["IdSessionObjRegistro"] = Obj.ID_RENDIMIEN_VOLUMET;
                }
            }
            return existenRegistros;
        }

        private List<O_RENDIMIENTO_CTY> obtienelistaLocalRendimientos()
        {
            List<O_RENDIMIENTO_CTY> listObjRendimiento = new List<O_RENDIMIENTO_CTY>();
            string strgCredencial = "";
            string strgCodRefineria = "";
            string strgFiltroTipoConsulta = "UNIDAD";
            if (tlsOperadores.FocusedNode.GetValue("strDescripcion").ToString().Contains("ORO NEGRO"))
                strgCodRefineria = "ORN";
            else if (tlsOperadores.FocusedNode.GetValue("strDescripcion").ToString().Contains("GUILLERMO ELDER"))
                strgCodRefineria = "GEB";
            else if (tlsOperadores.FocusedNode.GetValue("strDescripcion").ToString().Contains("GUALBERTO VILLARROEL"))
                strgCodRefineria = "GV";

            EPResultado objResultado = _consulta.listaUnidadesAreaRefineria(strgCredencial, strgCodRefineria, strgFiltroTipoConsulta);
            List<O_UNIDADES_REF_CTY> listUnidadesOrgaRefinerias = (List<O_UNIDADES_REF_CTY>)objResultado.oResultado;
            Session["listaSessionUnindadesOrganizacionales"] = listUnidadesOrgaRefinerias;
            O_RENDIMIENTO_CTY objRendimientosCty;
            decimal decContador = 1;
            foreach (O_UNIDADES_REF_CTY itemlistaCty in listUnidadesOrgaRefinerias)
            {
                objRendimientosCty = new O_RENDIMIENTO_CTY();
                objRendimientosCty.AREA_UNIDAD = itemlistaCty.NOM_AREA_REFIN;
                objRendimientosCty.ID_RENDIMIEN = decContador;
                objRendimientosCty.TIPO_PRODUCTO = "";
                objRendimientosCty.OPERACION_DIA = "0.00";
                objRendimientosCty.CARGA_TOTAL = "0.00";
                objRendimientosCty.CARGA_PROMEDIO = "0.00";
                objRendimientosCty.CAPACIDAD_OPERACION = "0.00";
                objRendimientosCty.COMB_LIQUIDO = "0.00";
                objRendimientosCty.COMB_GAS = "0.00";
                objRendimientosCty.VAPOR_TN = "0.00";
                objRendimientosCty.AGUA_ENFR = "0.00";
                objRendimientosCty.AIRE_COMP = "0.00";
                objRendimientosCty.COMPARAD_KWH = "0.00";
                objRendimientosCty.PROPIA_KWH = "0.00";
                listObjRendimiento.Add(objRendimientosCty);
                decContador = decContador + 1;
            }

            Session["listaSessionResultRendimiento"] = listObjRendimiento;
            return listObjRendimiento;
        }

        private void cargarCombosRegistro()
        {
            string strgCredencial = "";
            string strgCodRefineria = "PAR_REN_VOL";
            string strgFiltroTipoConsulta = "PARREN";
            EPResultado objResultado = _consulta.listaUnidadesAreaRefineria(strgCredencial, strgCodRefineria, strgFiltroTipoConsulta);
            List<O_UNIDADES_REF_CTY> listUnidadesOrgaRefinerias = (List<O_UNIDADES_REF_CTY>)objResultado.oResultado;

            cmbTipRendimVol.DataSource = listUnidadesOrgaRefinerias;
            cmbTipRendimVol.DataBind();
        }

        protected void panelRegistroSeguimiento_Callback(object sender, CallbackEventArgsBase e)
        {
            try
            {
                dgvIncidentesRegistro.Visible = true;
                if (e.Parameter == "registro")
                    registroIncidentePorcentVolum();
                else if (e.Parameter == "iniciarRegistro")
                    Session["accion"] = "registrar";
                else if (e.Parameter == "modificarRegistro")
                    Session["accion"] = "modificar";
                List<O_RENDIMIENTO_CTY> lstDato = (List<O_RENDIMIENTO_CTY>)Session["listaSessionResultRendimiento"];
                dgvIncidentesRegistro.DataSource = lstDato.OrderBy(w => w.ID_RENDIMIEN);
                dgvIncidentesRegistro.KeyFieldName = "ID_RENDIMIEN";
                dgvIncidentesRegistro.DataBind();
            }
            catch (Exception ex)
            {
                CLog.Error(this.Context, GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }
        protected void panelActulizaLista_Callback(object sender, CallbackEventArgsBase e)
        {
            try
            {
                if (e.Parameter != "")
                {
                    string someValue = dgvIncidentesRegistro.GetRowValues(dgvIncidentesRegistro.FocusedRowIndex, "ID_RENDIMIEN").ToString();
                    string[] valoresAcutalizalista = e.Parameter.Split('_');
                    List<O_RENDIMIENTO_CTY> lstRegistro = (List<O_RENDIMIENTO_CTY>)Session["listaSessionResultRendimiento"];
                    O_RENDIMIENTO_CTY objRgistro = new O_RENDIMIENTO_CTY();
                    objRgistro = lstRegistro.FirstOrDefault(w => w.ID_RENDIMIEN == Convert.ToDecimal(someValue));
                    objRgistro.OPERACION_DIA = valoresAcutalizalista[0];
                    objRgistro.CARGA_TOTAL = valoresAcutalizalista[1];
                    objRgistro.CARGA_PROMEDIO = valoresAcutalizalista[2];
                    objRgistro.CAPACIDAD_OPERACION = valoresAcutalizalista[3];
                    objRgistro.COMB_LIQUIDO = valoresAcutalizalista[4];
                    objRgistro.COMB_GAS = valoresAcutalizalista[5];
                    objRgistro.VAPOR_TN = valoresAcutalizalista[6];
                    objRgistro.AGUA_ENFR = valoresAcutalizalista[7];
                    objRgistro.AIRE_COMP = valoresAcutalizalista[8];
                    objRgistro.COMPARAD_KWH = valoresAcutalizalista[9];
                    objRgistro.PROPIA_KWH = valoresAcutalizalista[10];
                    lstRegistro = lstRegistro.Where(w => w.ID_RENDIMIEN != Convert.ToDecimal(someValue)).ToList();
                    lstRegistro.Add(objRgistro);
                    Session["listaSessionResultRendimiento"] = lstRegistro;
                }

                //List<O_RENDIMIENTO_CTY> lstDato = (List<O_RENDIMIENTO_CTY>)Session["listaSessionResultRendimiento"];
            }
            catch (Exception ex)
            {
                CLog.Error(this.Context, GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        private void recuperavaloresGrillaRegistro()
        {
            List<O_RENDIMIENTO_CTY> listObjRendimiento = new List<O_RENDIMIENTO_CTY>();
            //List<O_RENDIMIENTO_CTY> lstDato = (List<O_RENDIMIENTO_CTY>)Session["listaSessionResultRendimiento"];

            //if (Session["listaRegistroSession"] != null)
            //{
            //listObjRendimiento = (List<O_RENDIMIENTO_CTY>)Session["listaRegistroSession"];
            O_RENDIMIENTO_CTY objRendimiento;
            for (int i = 0; i < dgvIncidentesRegistro.VisibleRowCount; i++)
            {
                objRendimiento = new O_RENDIMIENTO_CTY();
                //((Label)dgvIncidentesRegistro.Rows[1].FindControl("lblNombre"));
                objRendimiento.OPERACION_DIA = dgvIncidentesRegistro.GetRowValues(i, "OPERACION_DIA").ToString();
                objRendimiento.CARGA_TOTAL = dgvIncidentesRegistro.GetRowValues(i, "CARGA_TOTAL").ToString();
                objRendimiento.CARGA_PROMEDIO = dgvIncidentesRegistro.GetRowValues(i, "CARGA_PROMEDIO").ToString();
                objRendimiento.CAPACIDAD_OPERACION = dgvIncidentesRegistro.GetRowValues(i, "CAPACIDAD_OPERACION").ToString();
                objRendimiento.COMB_LIQUIDO = dgvIncidentesRegistro.GetRowValues(i, "COMB_LIQUIDO").ToString();
                objRendimiento.COMB_GAS = dgvIncidentesRegistro.GetRowValues(i, "COMB_GAS").ToString();
                objRendimiento.VAPOR_TN = dgvIncidentesRegistro.GetRowValues(i, "VAPOR_TN").ToString();
                objRendimiento.AGUA_ENFR = dgvIncidentesRegistro.GetRowValues(i, "AGUA_ENFR").ToString();
                objRendimiento.AIRE_COMP = dgvIncidentesRegistro.GetRowValues(i, "AIRE_COMP").ToString();
                objRendimiento.COMPARAD_KWH = dgvIncidentesRegistro.GetRowValues(i, "COMPARAD_KWH").ToString();
                objRendimiento.PROPIA_KWH = dgvIncidentesRegistro.GetRowValues(i, "PROPIA_KWH").ToString();
                listObjRendimiento.Add(objRendimiento);
            }
            Session["listaSessionResultRendimiento"] = listObjRendimiento;
            //}
        }

        protected void panelRegistroendimientoPorcentual_Callback(object sender, CallbackEventArgsBase e)
        {
            try
            {
                if (e.Parameter != "")
                {
                    string someValue = dgvIncidentesRegistro.GetRowValues(dgvIncidentesRegistro.FocusedRowIndex, "ID_RENDIMIEN").ToString();
                    Session["idSessionRegRegistro"] = someValue;
                    List<O_RENDIMIENTO_CTY> lstDato = (List<O_RENDIMIENTO_CTY>)Session["listaSessionResultRendimiento"];
                    O_RENDIMIENTO_CTY objLstDato = lstDato.FirstOrDefault(w => w.ID_RENDIMIEN == Convert.ToDecimal(someValue));
                    //List<O_RENDIMIENTO_VOLUM_CTY> listaRendimientoPorcentualVolum = Session["listaSessionRegistroRenPorcVol"] != null ? (List<O_RENDIMIENTO_VOLUM_CTY>)Session["listaSessionRegistroRenPorcVol"]
                    //    : new List<O_RENDIMIENTO_VOLUM_CTY>();
                    List<O_RENDIMIENTO_VOLUM_CTY> listaRendimientoPorcentualVolum = new List<O_RENDIMIENTO_VOLUM_CTY>();

                    listaRendimientoPorcentualVolum = objLstDato.oRendimientoVolumetricoCty;
                    if (listaRendimientoPorcentualVolum != null)
                        listaRendimientoPorcentualVolum = listaRendimientoPorcentualVolum.Where(w => w.ID_RENDIMIEN == Convert.ToDecimal(someValue)).ToList();
                    Session["listaSessionRegistroRenPorcVol"] = listaRendimientoPorcentualVolum;
                    grbListaRegistroRendPorVolum.DataSource = listaRendimientoPorcentualVolum;
                    grbListaRegistroRendPorVolum.DataBind();
                    txtVolumnenRendPorVol.Text = "0";
                    txtVolumnenRendPorVol.ReadOnly = false;
                    txtVolumnenRendPorVol.MaskSettings.Mask = "<0..9999999999999g>.<00..99>";
                    txtVolumnenRendPorVol.MaskSettings.IncludeLiterals = MaskIncludeLiteralsMode.DecimalSymbol;
                    cargarCombosRegistro();
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

        private O_RENDIMIENTO_VOL_CTY recuperaDatosGrillaRegistro()
        {
            O_RENDIMIENTO_VOL_CTY objRegiatro = new O_RENDIMIENTO_VOL_CTY();
            objRegiatro.APP_USUARIO = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
            objRegiatro.ID_OPERADOR = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString());
            DateTime fehaActual = DateTime.Now;
            objRegiatro.FECHA_REGISTRO = fehaActual;
            string[] textoPerido = cmbPeriodo.Text.Split('/');
            objRegiatro.PERIODO = Convert.ToDecimal(textoPerido[0]);
            objRegiatro.GESTION = Convert.ToDecimal(textoPerido[1]);
            objRegiatro.TIPO_INCIDENTE = "";
            List<O_RENDIMIENTO_CTY> listObjRendimiento = new List<O_RENDIMIENTO_CTY>();
            List<O_RENDIMIENTO_CTY> lstDato = (List<O_RENDIMIENTO_CTY>)Session["listaSessionResultRendimiento"];
            O_OBJ_RENDIMIENTOS_VOLUM_CTY objJson = new O_OBJ_RENDIMIENTOS_VOLUM_CTY();
            objJson.ID_OPERADOR = objRegiatro.ID_OPERADOR;
            objJson.oRendimientoVolumCty = lstDato;
            objRegiatro.JSON_REGIS_MENSUALES = objJson.ToJson();
            return objRegiatro;
        }

        private O_RESULTADO_CTY registrarIncidente()
        {
            O_RESULTADO_CTY obResultad = new O_RESULTADO_CTY();
            O_INCIDENTES_CTY objRegistro = new O_INCIDENTES_CTY();
            if (Session["accion"] != null)
            {
                O_RENDIMIENTO_VOL_CTY listObjRendimiento = new O_RENDIMIENTO_VOL_CTY();
                listObjRendimiento = recuperaDatosGrillaRegistro();
                if ((string)Session["accion"] == "modificar")
                {
                    listObjRendimiento.ID_RENDIMIEN_VOLUMET = (decimal)Session["IdSessionObjRegistro"];
                    var objResultado = _gestion.ModificarRendimientos(CParametrosHydro.strCredencialOctanoVolumen, listObjRendimiento);
                    if (objResultado != null)
                    {
                        obResultad.ID_TABLA = objResultado.intCodigo;
                        obResultad.MENSAJE_ERROR = objResultado.strMensaje;
                    }
                }
                else if ((string)Session["accion"] == "registrar")
                {
                    var objResultado = _gestion.RegistraRendmientos(CParametrosHydro.strCredencialOctanoVolumen, listObjRendimiento);
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
            var objResultado = _gestion.EliminaRendmientos(CParametrosHydro.strCredencialOctanoVolumen, decIdRegistro,
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
        }

        #endregion

        protected void cmbTipRendimVol_Callback(object sender, CallbackEventArgsBase e)
        {
            string strgCredencial = "";
            string strgCodRefineria = "PAR_REN_VOL";
            string strgFiltroTipoConsulta = "UNIDAD";
            EPResultado objResultado = _consulta.listaUnidadesAreaRefineria(strgCredencial, strgCodRefineria, strgFiltroTipoConsulta);
            List<O_UNIDADES_REF_CTY> listUnidadesOrgaRefinerias = (List<O_UNIDADES_REF_CTY>)objResultado.oResultado;
            cmbTipRendimVol.DataSource = listUnidadesOrgaRefinerias;
            cmbTipRendimVol.DataBind();
        }

        protected void grbListaRegistroRendPorVolum_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters != "")
            {
                String[] valoresAccion = e.Parameters.Split('-');
                List<O_RENDIMIENTO_VOLUM_CTY> listaRendimientoPorcentualVolum = Session["listaSessionRegistroRenPorcVol"] != null ? (List<O_RENDIMIENTO_VOLUM_CTY>)Session["listaSessionRegistroRenPorcVol"] : new List<O_RENDIMIENTO_VOLUM_CTY>();
                if (valoresAccion[0] == "agregar")
                {
                    O_RENDIMIENTO_VOLUM_CTY objORendimientoVolumCty = new O_RENDIMIENTO_VOLUM_CTY();
                    if (listaRendimientoPorcentualVolum.Count > 0)
                        objORendimientoVolumCty.ID_RENDIMIEN_VOLUM = listaRendimientoPorcentualVolum.Max(w => w.ID_RENDIMIEN_VOLUM) + 1;
                    else
                        objORendimientoVolumCty.ID_RENDIMIEN_VOLUM = 1;
                    objORendimientoVolumCty.ID_RENDIMIEN = Convert.ToDecimal((string)Session["idSessionRegRegistro"]);
                    objORendimientoVolumCty.PRODUCTO = cmbTipRendimVol.Text;
                    objORendimientoVolumCty.ID_PRODUCTO = Convert.ToDecimal(cmbTipRendimVol.Value);
                    objORendimientoVolumCty.VOLUMEN = txtVolumnenRendPorVol.Text;
                    listaRendimientoPorcentualVolum.Add(objORendimientoVolumCty);
                }
                else if (valoresAccion[0] == "eliminar")
                {
                    listaRendimientoPorcentualVolum = listaRendimientoPorcentualVolum.Where(w => w.ID_RENDIMIEN_VOLUM != Convert.ToDecimal(valoresAccion[1])).ToList();
                }
                Session["listaSessionRegistroRenPorcVol"] = listaRendimientoPorcentualVolum;
                grbListaRegistroRendPorVolum.DataSource = listaRendimientoPorcentualVolum;
                grbListaRegistroRendPorVolum.DataBind();
            }
        }

        protected void dgvIncidentesRegistro_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
        {
            try
            {
                decimal idPlanificacion = Convert.ToDecimal(dgvIncidentesRegistro.GetRowValues(e.VisibleIndex, "ID_PLANIFICACION"));
                string comando = e.CommandArgs.CommandName;
                if (comando.Equals("Modificar"))
                {
                }
                else if (comando.Equals("Eliminar"))
                {
                }
            }
            catch (Exception ex)
            {
                CLog.Error(this.Context, GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }


    }
}