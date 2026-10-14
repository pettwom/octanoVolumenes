using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
//using AnhAdministradorPresentacion.Sitio.Persona;
using AnhClases;
using AnhCore.Entidades;
using AnhCore.ReglaNegocio;
using AnhHydroOctanoAgenteServicio;
using AnhHydroOctanoPresentacion.Clases.Empadronamiento;
using AnhHydroOctanoPresentacion.Clases.Parametricas;
using AnhHydroOctanoPresentacion.Clases.Persona;
using AnhHydroOctanoPresentacion.Entidades;
using AnhHydroOctanoPresentacion.Lib;
using AnhHydroOctanoPresentacion.Parametros;
using DevExpress.Web;
using Librerias.Anh.Us;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using ServiceStack.ServiceClient.Web;
using ServiceStack.ServiceHost;

namespace AnhHydroOctanoPresentacion.Sitio.Usuarios
{/*
    public partial class wfAdministrarUsuario : System.Web.UI.Page
    {
        #region Variables de Entorno
        /// <summary>
        /// variable de aletay mensajes de error
        /// </summary>
        StringBuilder strMensaje = new StringBuilder();
        private CLogs _logs;
        private CPersonaSesion _sesion;
        JsonServiceClient client = new JsonServiceClient(CAppSettings.servicio_octVolumen);
        JsonServiceClient clienteHydro = new JsonServiceClient(CAppSettings.ServicioAdministradorHydro);
        private string strMensajeError = "";
        private decimal IdModulo = CParametrosHydro.decIdAplicacion;
        #endregion

        #region Inicializacion de la pagina

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {//CParametrosHydro
                if (!CConsultaAccesos.AccesoFormulario(CParametrosHydro.strCredencialHydroAdmin,
                    Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]),
                    Convert.ToDecimal(CParametrosHydro.decIdAplicacion), Path.GetFileName(Request.Path),
                    ref strMensajeError))
                {
                    if (Session[CVariablesSesion.IdUsuario] != null)
                    {
                        _logs = new CLogs(Server.MapPath("~"), Request.ServerVariables["REMOTE_ADDR"]);
                        _sesion = new CPersonaSesion(Server.MapPath("~"), Request.ServerVariables["REMOTE_ADDR"]);
                        mCargarUsuariosEntidad();
                        if (Session["usuario_registrado"] != null)//Existe usuario y no esta asociado a entidades
                        {
                            txtUsuarioPassword1.Text = "***";
                            txtUsuarioPassword2.Text = "***";
                            tableContraseña.Visible = false;
                        }
                        if (!Page.IsCallback && !Page.IsPostBack)
                        {
                            if (Session["lista_entidades"] != null)
                            {
                                CMessageBoxManager.ShowMessageDialog("El usuario ya tiene entidades asociadas previamente, para agregar alguna presione el boton +.", ref mensaje_usuario,
                                    CMessageBoxManager.INFO_MESSAGE);
                                Session.Remove("lista_entidades");
                            }
                            mCargarParametricas(); 
                            //MCargaArbol();
                            //CargarMenuUsuarioPorPerfil((decimal)Session[CVariablesSesion.IdUsuario]); //Convert.ToDecimal(ViewState["decIdUsuario"])
                            //CargarUsuariosSistema2(Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]), 0, 1);
                        }
                        if (tlsOperadores.DataSource == null)
                        {
                            tlsOperadores.DataSource = Session[CVariablesSesion.EstructuraArbol];
                            tlsOperadores.DataBind();
                        }
                        //tlsOperadores.DataSource = Session[CVariablesSesion.EstructuraArbol];
                        //tlsOperadores.DataBind();
                        //if (ViewState["decIdUsuario"] != null)
                        //{
                        //    decimal idUsuario = (decimal)ViewState["decIdUsuario"];
                        //    CargarMenuUsuarioPorPerfil(Convert.ToDecimal(idUsuario));
                        //    CargarPerfilesUsuario(Convert.ToDecimal(idUsuario));
                        //}
                        //else
                        //{
                        //    decimal idUsuario = (decimal)Session[CVariablesSesion.IdUsuario];
                        //    CargarMenuUsuarioPorPerfil(Convert.ToDecimal(idUsuario));
                        //    CargarPerfilesUsuario(Convert.ToDecimal(idUsuario));
                        //}
                        //CMessageBoxManager.HideMessageDialog(ref mensaje);
                        //CargarUsuariosSistema(); no se carga ningun usuario
                    }
                    else
                    {
                        Session.RemoveAll();
                        Response.Redirect("~/Sitio/Persona/wfAutenticacion.aspx", false);
                        HttpContext.Current.ApplicationInstance.CompleteRequest();
                    }
                }
            }
            catch (Exception ex)
            {
                string strUbicacionMetodo = this.GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                CLogTraza.Error(strUbicacionMetodo, IdModulo, Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]), HttpContext.Current.Request.UserHostAddress, ex.Message, Convert.ToString(ex.InnerException), ex.StackTrace);
                FormsAuthentication.SignOut();
                Response.Cookies.Remove(CVariablesSesion.IdAutenticacion);
                Session.RemoveAll();
                Response.Redirect("~/Sitio/Persona/wfAutenticacion.aspx", false);
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
        }

        private void mCargarParametricas()
        {
            CParametricas parametricas = new CParametricas(Server.MapPath("~"), Request.ServerVariables["REMOTE_ADDR"]);
            datosPersona.ServicioParametricas = parametricas.ObtenerServicio();
        }

        private void mCargarUsuariosEntidad()
        {
            decimal idUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
            ClientScript.RegisterStartupScript(this.GetType(), "myScript",
                                    "<script>javascript:CargarEntidadesUsuario();</script>");
        }

        private bool MValidaSesion()
        {
            bool booResultado;
            //borrar
            ///Session[CVariablesSesion.IdUsuario] = "1802";
            if (Session[CVariablesSesion.IdUsuario] == null)
            {
                Response.Redirect("~/Sitio/Persona/wfAutenticacion.aspx", false);
                HttpContext.Current.ApplicationInstance.CompleteRequest();
                booResultado = false;
            }
            else
            {
                booResultado = true;
            }
            return booResultado;
        }
        #endregion

        #region Registrar usuario
        //protected void btnRegistrarInformacion_Click(object sender, ImageClickEventArgs e)
        protected void btnRegistrarInformacion_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["usuario"] != null)
                {
                    CPersonaSesion.Usuario usuario = (CPersonaSesion.Usuario)ViewState["usuario"];
                    string strUsuario = JsonConvert.SerializeObject(usuario);
                    decimal decIdUsuario;
                    if (Session["usuario_registrado"] == null || (decimal)Session["usuario_registrado"] == 0)
                    {
                        decIdUsuario = _sesion.RegistrarUsuario(strUsuario);
                        if (decIdUsuario > 0)
                        {
                            mEnviarEmail(decIdUsuario);
                        }
                        else
                        {
                            CMessageBoxManager.ShowMessageDialog(
                                "Disculpe las molestias, no se pudo registrar la información ingresada..", ref mensaje,
                                CMessageBoxManager.ERROR_MESSAGE);
                        }
                    }
                    else
                    {
                        decIdUsuario = (decimal) Session["usuario_registrado"];
                    }
                    //if (decIdUsuario > 0)
                    //{
                    Session["cuenta_creada"] = true;
                    //Agregar usuario al grupo
                        EResultadoGenerico resultado =
                            clienteHydro.Post<EResultadoGenerico>(
                            CAppSettings.svc_WSAdministrador_EAsignaUsuarioGrupo,
                            new EAsignaUsuarioGrupo()
                            {
                                decUsuarioId = (int) decIdUsuario,
                                decGrupoId = 141,
                                decAppIdUsuario = (int)Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]),
                                strCredencial = CParametrosHydro.strCredencialHydroAdmin
                            });
                        if (resultado.oResultado.ID_TABLA > 0)
                        {
                            //Asociar usuario a entidad(es)
                            List<EntidadActividad> entidadesActividad =
                                (List<EntidadActividad>) ViewState["entidades_actividad"];
                            foreach (EntidadActividad entidadActividad in entidadesActividad)
                            {
                                if (mAsignarUsuarioEntidadGeneral(decIdUsuario, entidadActividad.ID_ENTIDAD, entidadActividad.TIPO_ACTIVIDAD))
                                {
                                    List<Perfil> perfiles = (List<Perfil>) ViewState["perfiles"];
                                    foreach (Perfil perfil in perfiles)
                                    {
                                        if (AgregarPerfil(perfil.ID_PERFIL, decIdUsuario))
                                        {
                                            CMessageBoxManager.ShowMessageDialog(
                                                "Se ralizó la asignación con exito..", ref mensaje,
                                                CMessageBoxManager.OK_MESSAGE);
                                        }
                                        else
                                        {
                                            pucDatos.Visible = false;
                                            CMessageBoxManager.ShowMessageDialog(
                                                "Disculpe las molestias, no se pudo asignar los perfiles...",
                                                ref mensaje,
                                                CMessageBoxManager.ERROR_MESSAGE);
                                        }
                                    }
                                }
                                else
                                {
                                    pucDatos.Visible = false;
                                    CMessageBoxManager.ShowMessageDialog(
                                        "Disculpe las molestias, no se pudieron asociar entidades al usuario...",
                                        ref mensaje,
                                        CMessageBoxManager.ERROR_MESSAGE);
                                }
                            }
                        }
                        else
                        {
                            pucDatos.Visible = false;
                            CMessageBoxManager.ShowMessageDialog(
                                        "Disculpe las molestias, no se pudo registrar la información ingresada...",
                                        ref mensaje,
                                        CMessageBoxManager.ERROR_MESSAGE);
                        }
                        //Asignar perfiles a usuario
                        //ViewState["perfiles"];
                        
                        //Response.Redirect("~/Sitio/Persona/wfFinalizarSolicitarCuenta.aspx");
                    //}
                    //else
                    //{
                    //    CMessageBoxManager.ShowMessageDialog(
                    //        "Disculpe las molestias, no se pudo registrar la información ingresada..", ref mensaje,
                    //        CMessageBoxManager.ERROR_MESSAGE);
                    //}
                }
                else
                {
                    CMessageBoxManager.ShowMessageDialog(
                        "Disculpe las molestias, no se pudo registrada la información ingresada..", ref mensaje,
                        CMessageBoxManager.ERROR_MESSAGE);
                }
            }
            catch (Exception exp)
            {
                _logs.Error(exp);
            }
        }
        #endregion

        #region Email
        /// <summary>
        /// Metodo para el envio de email de confirmacion en el sistema
        /// </summary>
        private void mEnviarEmail(decimal decIdUsuario)
        {
            try
            {
                string strMensaje = "<html><head></head><body>" +

                                    "<span style='font-size:18px; font-family:'Century Gothic'; text-align:justify;'>" +
                                    "<table cellpadding='5' style='border:#3F5C30 solid thin; width:650px; background-color: #006600;'>" +
                                    "<tbody><tr><td style='text-align:center;'>" +
                                    "<span style='font-size:18px; text-align:center; color:#FFFFFF;'>" +
                                    "AGENCIA NACIONAL DE HIDROCARBUROS" +
                                    "</span></td></tr></tbody></table>" +
                                    "<table cellpadding='15' style='border: #3F5C30 solid thin; background-color: #faFfff; width:650px;'>" +
                                    "<tbody><tr><td> Sr(a) <span>" + datosPersona.Nombres.ToUpper() + " " + datosPersona.PrimerApellido.ToUpper() + " " + datosPersona.SegundoApellido.ToUpper() + "</span> fue registrado con éxito.<br><br>" +
                                    "<span style='font-size:16px;'>Sus datos registrados son:<br><br>" +
                                    "<table cellpadding='8' style='border: #3F5C30 solid thin; background-color:#FaFaFa;'>" +
                                    "<tbody><tr><td style='font-size:14px;font-weight:bold;'>" +
                                    "<span style='color:#060;'>USUARIO:</span>" +
                                    " " + datosPersona.Email + "<br><br>" +
                                    "<span style='color:#060;'>CONTRASEÑA:</span> " + (ViewState["ayudaClave"] == null ? "" : ViewState["ayudaClave"].ToString()) + "</td>" +
                                    "</tr></tbody></table><br><br>" +
                                    "Finalize su registro activando su cuenta en un plazo máximo de 24 horas.<br>" +
                                    "<div style='width:100%'><a href='" + CAppSettings.UrlRaiz + "/Sitio/Persona/wfActivarCuenta.aspx?codigo=" + cEncriptacion.EncryptString(datosPersona.Email + "&" + decIdUsuario + "&" + DateTime.Now.ToShortDateString()) + "'>" +
                                    "<span style='background: #005500; color: white; padding:5px; float: left;'>" +
                                    "Activar mi cuenta ahora !</span></a></div><br><br>" +
                                    "En caso de no poder activar su cuenta copie y pegue el siguiente enlace en la URL: <br><br>" +
                                    "<p style='font-size:70%;width: 600px; word-wrap: break-word;'>" + CAppSettings.UrlRaiz + "/Sitio/Persona/wfActivarCuenta.aspx?codigo=" + cEncriptacion.EncryptString(datosPersona.Email + "&" + decIdUsuario + "&" + DateTime.Now.ToShortDateString()) +
                                    "</p><br><br>" +
                                    "De esta forma, nosotros como Agencia Nacional de Hidrocarburos estaremos en contacto con usted." +
                                    "<br><br>Si usted tiene consultas puede  escribirnos a la siguiente dirección de correo: " +
                                     "sistemas@anh.gob.bo o comunicarse a los teléfonos:" +
                                        "<br>Distrital La Paz 2614000 Int. 2401" +
                                        "<br>Distrital Santa Cruz 3459125" +
                                        "<br>Distrital Cochabamba 4485025" +
                                        "<br>Distrital Chuquisaca 6431800" +
                                        "<br>Distrital Beni 682-22095" +
                                        "<br>Distrital Pando 8-423991" +
                                        "<br>Distrital Oruro 800-10-2345" +
                                        "<br>Distrital Potosi 6229930" +
                                        "<br>o al (591-2) 2-614000 Int. 2805 de la Dirección de Tecnologías de Información y Comunicación.</span>" +
                                    "<br><br><span style='font-size:12px;'>Atte.: Administrador de Sistemas HYDRO<br>" +
                                    "Fecha: " + DateTime.Now + "</span>" +
                                    "</td></tr></tbody></table></span></body></html>";

                cCorreo.mEnviarEmail(CParametrosHydro.StrServidorDireccion,
                                     CParametrosHydro.IntServidorPuerto,
                                     CParametrosHydro.StrUsuarioLogin,
                                     CParametrosHydro.StrUsuarioPassword,
                                     CParametrosHydro.StrUsuarioDe,
                                     datosPersona.Email,
                                     CParametrosHydro.StrUsuarioCc,
                                     CParametrosHydro.StrUsuarioCco,
                                     "CONFIRMACIÓN DE CUENTA HYDRO - " + datosPersona.Email + " - " + Request.ServerVariables["REMOTE_ADDR"] + " - " + DateTime.Now.ToShortDateString(),
                                     strMensaje,
                                     null,
                                     true,
                                     CParametrosHydro.bolHabilitarSsl,
                                     CParametrosHydro.bolNotificarError);
                Session["email_enviado"] = true;
            }
            catch (Exception exp)
            {
                CLogs log = new CLogs(Server.MapPath("~"), Request.ServerVariables["REMOTE_ADDR"]);
                log.Error(exp);
            }
        }

        private void mListarUsuariosEntidad()
        {
            decimal idUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
            var lista2 = client.Get<EResultadoEntidad>(CAppSettings.svc_obtener_entidades + "/" + idUsuario + "?format=json");
        }
        #endregion

        #region WebMethod
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string CargarEntidadesUsuario()
        {
            JsonServiceClient _cliente = new JsonServiceClient(CAppSettings.servicio_octVolumen);
            try
            {
                decimal decIdUsuario = Convert.ToDecimal(HttpContext.Current.Session[CVariablesSesion.IdUsuario]);
                var lista2 = _cliente.Get<EResultadoEntidad>(CAppSettings.svc_obtener_entidades + "/" + decIdUsuario + "?format=json");
                if (lista2 != null)
                {
                    List<O_OBTIENE_ENTIDADES_USR_CTY> lstEntidad;
                    lstEntidad = lista2.OResultado;
                    lstEntidad = lstEntidad.OrderBy(x => x.DENOMINACION_PADRE).ThenBy(x => x.ACTIVIDAD).ThenBy(x => x.DENOMINACION).ToList();
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    string strLstEntidad = js.Serialize(lstEntidad);
                    return strLstEntidad;
                }
                return "0";
            }
            catch (Exception ex)
            {
                CLogTraza.MensajeUsuario oMensajeUsuario = new CLogTraza.MensajeUsuario();
                oMensajeUsuario.decUsuarioId = HttpContext.Current.Session[CVariablesSesion.IdUsuario];
                oMensajeUsuario.decIdModulo = CParametrosHydro.decIdAplicacion;
                oMensajeUsuario.strAccion = HttpContext.Current.GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                oMensajeUsuario.strIp = HttpContext.Current.Request.UserHostAddress;
                oMensajeUsuario.decNivelCapa = (int)CLogTraza.CapasNivel.Presentacion;
                CLogTraza.Error(oMensajeUsuario, ex);
                return "0";
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string ListarPerfiles()
        {
            //JsonServiceClient _cliente = new JsonServiceClient(CAppSettings.servicio_octVolumen);
            JsonServiceClient _clienteHydro = new JsonServiceClient(CAppSettings.ServicioAdministradorHydro);
            try
            {
                decimal decIdUsuario = Convert.ToDecimal(HttpContext.Current.Session[CVariablesSesion.IdUsuario]);
                string str = CAppSettings.svc_WSAdministrador_ListaPerfiles + "/" +
                             CParametrosHydro.strCredencialHydroAdmin + "/" + CAppSettings.idAplicacion + "?format=json";
                var lista2 = _clienteHydro.Get<EResultadoPerfil>(CAppSettings.svc_WSAdministrador_ListaPerfiles + "/" + CParametrosHydro.strCredencialHydroAdmin + "/" + CAppSettings.idAplicacion + "?format=json");
                
                if (lista2 != null)
                {
                    List<O_PERFIL_CTY> lstPerfil;
                    lstPerfil = lista2.oResultado;
                    lstPerfil = lstPerfil.FindAll(cty => (cty.ID_PERFIL != Convert.ToDecimal(CAppSettings.IdFuncionario) && cty.ID_PERFIL != Convert.ToDecimal(CAppSettings.IdRegulado))).OrderBy(x => x.NOMBRE_PERFIL).ToList();
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    string strLstEntidad = js.Serialize(lstPerfil);
                    return strLstEntidad;
                }
                return "0";
            }
            catch (Exception ex)
            {
                CLogTraza.MensajeUsuario oMensajeUsuario = new CLogTraza.MensajeUsuario();
                oMensajeUsuario.decUsuarioId = HttpContext.Current.Session[CVariablesSesion.IdUsuario];
                oMensajeUsuario.decIdModulo = CParametrosHydro.decIdAplicacion;
                oMensajeUsuario.strAccion = HttpContext.Current.GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                oMensajeUsuario.strIp = HttpContext.Current.Request.UserHostAddress;
                oMensajeUsuario.decNivelCapa = (int)CLogTraza.CapasNivel.Presentacion;
                CLogTraza.Error(oMensajeUsuario, ex);
                return "0";
            }
        }
        #endregion
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static void CerrarControl()
        {
            Page page = HttpContext.Current.CurrentHandler as Page;
            ScriptManager.RegisterClientScriptBlock(page,typeof(Page),"ClientScript","alert('hello')",true);
            HttpContext.Current.Response.Redirect("wfAdministrarUsuario.aspx");
            ScriptManager.RegisterClientScriptBlock(page, typeof(Page), "ClientScript", "alert('hello')", true);            
        }

        #region Eventos
        private bool mbolValidarUsuario(string strLogin)
        {
            try
            {
                var entUsuario = new entTanh_usuarios();
                var rnUsuario = new rnTanh_usuarios();
                var arrColumnas = new ArrayList();
                arrColumnas.Add("usu_login");

                var arrColumnasWhere = new ArrayList();
                arrColumnasWhere.Add("usu_login");

                var arrValoresWhere = new ArrayList();
                arrValoresWhere.Add(strLogin == "" ? "''" : "'" + strLogin + "'");
                DataTable dtAuxiliar = rnUsuario.CargarDataTable(arrColumnas, arrColumnasWhere, arrValoresWhere);
                if (dtAuxiliar.Rows.Count == 1)
                {
                    CMessageBoxManager.ShowMessageDialog(
                    "Este correo electrónico ya se encuentra registrado...",
                    ref mensaje,
                    CMessageBoxManager.ERROR_MESSAGE
                    );
                    return false;
                }
                else
                {
                    return true;
                }
                //return dtAuxiliar.Rows.Count == 1;
            }
            catch (Exception exp)
            {
                CMessageBoxManager.ShowMessageDialog(
                        "Este correo electrónico ya se encuentra registrado...",
                        ref mensaje,
                        CMessageBoxManager.ERROR_MESSAGE
                        );
                throw exp;
            }
        }

        protected void datosPersonaSelectedIndexChange(object sender, EventArgs e)
        {
            try
            {
                datosPersona.ServicioParametricas = LocalizadorProxy.ServicioParametricas();
            }
            catch (Exception exp)
            {
                //var log = new Logs(Server.MapPath("~"));
                //log.Error(exp);
                CLogs log = new CLogs(Server.MapPath("~"), Request.ServerVariables["REMOTE_ADDR"]);
                log.Error(exp);
            }
        }

        protected void datosPersonaTextChanged(object sender, EventArgs e)
        {
            CHydroConsultas consultas = new CHydroConsultas(Server.MapPath("~"), Request.ServerVariables["REMOTE_ADDR"]);
            datosPersona.ServicioHydroConsultas = consultas.obtenerServicio();
        }

        protected void btnVolver_Click(object sender, ImageClickEventArgs e)
        {
            //Response.Redirect("~/Sitio/Persona/wfAutenticacion.aspx");
            //panelDetalleOverlay.Visible = false;
            pucDatos.ShowOnPageLoad = false;
        }

        protected void btnNuevaCuenta_OnClick(object sender, EventArgs e)
        {
            pucDatos.ShowOnPageLoad = true;
            //panelDetalleOverlay.Visible = true;
            ClientScript.RegisterStartupScript(this.GetType(), "myScript",
                                    "<script>javascript:CargarEntidadesUsuario();</script>");
            //dtgListadoEstaciones.Visible = false;
        }

        private void mAsignarPerfilUsuario()
        {
            
        }
        private bool mAsignarUsuarioEntidadGeneral(decimal decIdUsuario, decimal decIdEntidad, decimal decIdTipoActividad)
        {
            try
            {
                EResultadoGenerico resultado = client.Post<EResultadoGenerico>(CAppSettings.svc_registra_usuario_entidad_general,
                    new ERegistraUsuarioEntidadGeneral()
                    {
                        decIdEntidad = decIdEntidad,
                        dateAppFechaRegistro = DateTime.Now,
                        decAppIdUsuario = Convert.ToInt32(Session[CVariablesSesion.IdUsuario]),
                        decIdTipoActividad = decIdTipoActividad,
                        decIdTipoUsuario = 2,
                        decIdUsuario = decIdUsuario//Convert.ToInt32(Session[CVariablesSesion.IdUsuario])
                    });
                if (resultado.intCodigo == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception exp)
            {
                CMessageBoxManager.ShowMessageDialog(
                        "Este correo electrónico ya se encuentra registrado...",
                        ref mensaje,
                        CMessageBoxManager.ERROR_MESSAGE
                        );
                throw exp;
            }
        }

        private void MCargaArbol()
        {
            decimal idUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
            var lista2 = client.Get<EResultadoEntidad>(CAppSettings.svc_obtener_entidades + "/" + idUsuario + "?format=json");
            if (lista2 != null)
            {
                if (lista2.IntCodigo == 1)
                {
                    List<O_OBTIENE_ENTIDADES_USR_CTY> lstEntidad;
                    lstEntidad = lista2.OResultado;
                    lstEntidad = lstEntidad.OrderBy(x => x.DENOMINACION_PADRE).ThenBy(x => x.ACTIVIDAD).ThenBy(x => x.DENOMINACION).ToList();

                    int idDe = 0, idAc = 0;
                    int idUnico = 1, idPadre = 0, idUnic1 = 0;
                    var lstResultado = new List<CArbol>();
                    foreach (var lst in lstEntidad)
                    {
                        CArbol cDato;
                        if (idDe != lst.ID_ENTIDAD_PADRE)
                        {
                            cDato = new CArbol();
                            cDato.IntId = idUnico;
                            cDato.IntIdPadre = 0;
                            cDato.IntIdEntidad = (int)lst.ID_ENTIDAD_PADRE;
                            cDato.IntIdActividad = 0;
                            cDato.IntIdOperador = 0;
                            cDato.IntIdProyecto = 0;
                            cDato.StrDescripcion = lst.DENOMINACION_PADRE.Trim();
                            cDato.StrDescPadre = lst.DENOMINACION_PADRE.Trim();
                            cDato.LngNit = Convert.ToInt64(lst.NIT);
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
                            lstResultado.Add(cDato);
                            idPadre = idUnico;
                            idUnic1 = idUnico;
                            idDe = (int)lst.ID_ENTIDAD_PADRE;
                            idAc = 0;
                            idUnico++;
                        }
                        else
                        {
                            //todo No implementado.
                        }
                        if (idAc != lst.ID_ACTIVIDAD)
                        {
                            cDato = new CArbol();
                            cDato.IntId = idUnico;
                            cDato.IntIdPadre = idUnic1;
                            cDato.IntIdEntidad = (int)lst.ID_ENTIDAD_PADRE;
                            cDato.IntIdOperador = 0;
                            cDato.IntIdProyecto = 0;
                            cDato.IntIdActividad = (int)lst.ID_ACTIVIDAD;
                            cDato.StrDescripcion = lst.ACTIVIDAD;
                            cDato.StrDescPadre = lst.DENOMINACION_PADRE;
                            cDato.LngNit = Convert.ToInt64(lst.NIT);
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
                            lstResultado.Add(cDato);
                            idPadre = idUnico;
                            idAc = (int)lst.ID_ACTIVIDAD;
                            idUnico++;
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
                        lstResultado.Add(cDato);
                        idUnico++;
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
        }
        #endregion

        #region Vista Previa

        protected void btnSiguiente_Click(object sender, EventArgs e)
        {
            try
            {
                var hdnEntidades = hidenEntidades.Value;
                var hdnPerfiles = hidenPerfiles.Value;
                Page.Validate("PersonaValidationGroup");
                if (!Page.IsValid)
                {
                    CMessageBoxManager.ShowMessageDialog(
                        "Los campos marcados con asteriscos (*) son obligatorios, revise la información ingresada por favor.",
                        ref mensaje, CMessageBoxManager.ERROR_MESSAGE);
                    return;
                }
                if (string.IsNullOrEmpty(hdnEntidades) && string.IsNullOrEmpty(hdnPerfiles))
                {
                    CMessageBoxManager.ShowMessageDialog(
                        "Debe ingresar la entidad y el perfil a a los que se asociará al usuario.",
                        ref mensajeCreaUsuario, CMessageBoxManager.ERROR_MESSAGE);
                    return;
                }
                string strEntidadActividad = Convert.ToString(hdnEntidades);
                strEntidadActividad = "[" + strEntidadActividad  + "]";
                string strPerfil = hdnPerfiles;
                strPerfil = "[" + strPerfil + "]";
                JavaScriptSerializer ser = new JavaScriptSerializer();
                List<EntidadActividad> entidadesActividad = new List<EntidadActividad>();


                //strEntidadActividad = "[{'ID_ENTIDAD':36655,'TIPO_ACTIVIDAD':1},{'ID_ENTIDAD':36655,'TIPO_ACTIVIDAD':1}]";

                entidadesActividad = ser.Deserialize<List<EntidadActividad>>(strEntidadActividad);
                List<Perfil> perfiles = new List<Perfil>();
                perfiles = ser.Deserialize<List<Perfil>>(strPerfil);


                ViewState["entidades_actividad"] = entidadesActividad;
                ViewState["perfiles"] = perfiles;
                //mAsgnarUsuarioEntidadGeneral(1426, strEntidadActividad, strPerfil);
                string strEmail = datosPersona.Email;
                decimal decIdUsuario = _sesion.ExisteUsuario(strEmail);
                if (( decIdUsuario == 0) && !strEmail.ToUpper().EndsWith("@ANH.GOB.BO"))
                {
                    if (datosPersona.FechaNacimiento >= DateTime.Now.AddYears(-100) && datosPersona.FechaNacimiento <= DateTime.Now.AddYears(-18))
                    {
                        CMessageBoxManager.HideMessageDialog(ref mensajeCreaUsuario);

                        int intIdTipoIdentificacion = Convert.ToInt32(datosPersona.TipoIdentificacion);
                        int intIdPersona = Convert.ToInt32(datosPersona.IdPersona);
                        string intNroIdentificacion = datosPersona.NroIdentificacion;
                        int intExpedicion = Convert.ToInt32(datosPersona.Expedicion);
                        string strComplemento = datosPersona.Complemento;
                        string strNombres = datosPersona.Nombres;
                        string strPrimerApellido = datosPersona.PrimerApellido;
                        string strSegundoApellido = datosPersona.SegundoApellido;
                        DateTime dateFechaNacimiento = datosPersona.FechaNacimiento;
                        int intGenero = Convert.ToInt32(datosPersona.Genero);
                        var a = datosPersona.ID;
                        string strClave = cEncriptacion.generarMD5(txtUsuarioPassword1.Text).ToUpper();
                        string strAyudaClave = "";
                        for (int i = 0; i < txtUsuarioPassword1.Text.Length; i++)
                        {
                            if (i == 0)
                            {
                                strAyudaClave += txtUsuarioPassword1.Text[i];
                            }
                            else if (i == txtUsuarioPassword1.Text.Length - 1)
                            {
                                strAyudaClave += txtUsuarioPassword1.Text[i];
                            }
                            else
                            {
                                strAyudaClave += "*";
                            }
                        }
                        ViewState["ayudaClave"] = strAyudaClave;

                        CPersonaSesion.Usuario usuario = new CPersonaSesion.Usuario();
                        usuario.idPersona = intIdPersona;
                        usuario.tipoIdentificacion = intIdTipoIdentificacion;
                        usuario.numeroIdentificacion = intNroIdentificacion;
                        usuario.expedicion = intExpedicion;
                        usuario.complemento = strComplemento;
                        usuario.nombre = strNombres.ToUpper();
                        usuario.primerApellido = strPrimerApellido.ToUpper();
                        usuario.segundoApellido = strSegundoApellido.ToUpper();
                        usuario.fechaNacimiento = CFechas.ConvierteDateTimeLong(dateFechaNacimiento);
                        usuario.genero = intGenero;
                        usuario.email = strEmail.ToUpper();
                        usuario.clave = strClave;
                        usuario.idPerfil = 2; //REGULADO

                        ViewState["usuario"] = usuario;

                        tablaVistaPrevia.Rows.Clear();

                        HtmlTableRow trRep = new HtmlTableRow();
                        HtmlTableCell thRep = new HtmlTableCell();
                        thRep.Width = "250px";
                        thRep.Style.Value = "font-weight: bold; padding-top:20px;";
                        thRep.InnerText = "USUARIO REGISTRADO";
                        HtmlTableCell tdRep = new HtmlTableCell();
                        trRep.Cells.Add(thRep);
                        trRep.Cells.Add(tdRep);
                        tablaVistaPrevia.Rows.Add(trRep);

                        HtmlTableRow tr1 = new HtmlTableRow();
                        HtmlTableCell th1 = new HtmlTableCell();
                        th1.Width = "250px";
                        th1.Style.Value = "font-weight: bold";
                        th1.InnerText = "Nro. de Identificación.";
                        HtmlTableCell td1 = new HtmlTableCell();
                        td1.InnerText = usuario.numeroIdentificacion + " " + usuario.complemento + " ";
                        switch (usuario.expedicion)
                        {
                            case 1: td1.InnerText += "CH"; break;
                            case 2: td1.InnerText += "LP"; break;
                            case 3: td1.InnerText += "CB"; break;
                            case 4: td1.InnerText += "OR"; break;
                            case 5: td1.InnerText += "PT"; break;
                            case 6: td1.InnerText += "TJ"; break;
                            case 7: td1.InnerText += "SC"; break;
                            case 8: td1.InnerText += "BN"; break;
                            case 9: td1.InnerText += "PD"; break;
                        }
                        tr1.Cells.Add(th1);
                        tr1.Cells.Add(td1);
                        tablaVistaPrevia.Rows.Add(tr1);

                        HtmlTableRow tr2 = new HtmlTableRow();
                        HtmlTableCell th2 = new HtmlTableCell();
                        th2.Width = "250px";
                        th2.Style.Value = "font-weight: bold";
                        th2.InnerText = "Nombres y Apellidos";
                        HtmlTableCell td2 = new HtmlTableCell();
                        td2.InnerText = usuario.nombre + " " + usuario.primerApellido + " " +
                                        usuario.segundoApellido;
                        tr2.Cells.Add(th2);
                        tr2.Cells.Add(td2);
                        tablaVistaPrevia.Rows.Add(tr2);

                        HtmlTableRow tr3 = new HtmlTableRow();
                        HtmlTableCell th3 = new HtmlTableCell();
                        th3.Width = "250px";
                        th3.Style.Value = "font-weight: bold";
                        th3.InnerText = "Fecha de Nacimiento";
                        HtmlTableCell td3 = new HtmlTableCell();
                        td3.InnerText = CFechas.ConvierteLongDateTime(usuario.fechaNacimiento).ToShortDateString();
                        tr3.Cells.Add(th3);
                        tr3.Cells.Add(td3);
                        tablaVistaPrevia.Rows.Add(tr3);

                        HtmlTableRow tr4 = new HtmlTableRow();
                        HtmlTableCell th4 = new HtmlTableCell();
                        th4.Width = "250px";
                        th4.Style.Value = "font-weight: bold";
                        th4.InnerText = "Género";
                        HtmlTableCell td4 = new HtmlTableCell();
                        td4.InnerText = usuario.genero == 1 ? "Masculino" : "Femenino";
                        tr4.Cells.Add(th4);
                        tr4.Cells.Add(td4);
                        tablaVistaPrevia.Rows.Add(tr4);

                        HtmlTableRow tr5 = new HtmlTableRow();
                        HtmlTableCell th5 = new HtmlTableCell();
                        th5.Width = "250px";
                        th5.Style.Value = "font-weight: bold";
                        th5.InnerText = "E-mail";
                        HtmlTableCell td5 = new HtmlTableCell();
                        td5.InnerText = usuario.email.ToLower();
                        tr5.Cells.Add(th5);
                        tr5.Cells.Add(td5);
                        tablaVistaPrevia.Rows.Add(tr5);

                        panelUsuario.Visible = false;
                        panelVistaPrevia.Visible = true;
                    }
                    else
                    {
                        CMessageBoxManager.ShowMessageDialog(
                            "Se ha ingresado una fecha de nacimiento inválida. Se permite el registro de personas entre 18 y 100 años de edad. Verifique sus datos por favor",
                            ref mensaje, CMessageBoxManager.ERROR_MESSAGE);
                    }
                }
                else //ya esta registrado y se deben asignar entidades
                {
                    //decIdUsuario 
                    int intIdTipoIdentificacion = Convert.ToInt32(datosPersona.TipoIdentificacion);
                    int intIdPersona = Convert.ToInt32(datosPersona.IdPersona);
                    string intNroIdentificacion = datosPersona.NroIdentificacion;
                    int intExpedicion = Convert.ToInt32(datosPersona.Expedicion);
                    string strComplemento = datosPersona.Complemento;
                    string strNombres = datosPersona.Nombres;
                    string strPrimerApellido = datosPersona.PrimerApellido;
                    string strSegundoApellido = datosPersona.SegundoApellido;
                    DateTime dateFechaNacimiento = datosPersona.FechaNacimiento;
                    int intGenero = Convert.ToInt32(datosPersona.Genero);
                    string strClave = cEncriptacion.generarMD5(txtUsuarioPassword1.Text).ToUpper();
                    string strAyudaClave = "";
                    decIdUsuario = (decimal)Session["usuario_registrado"];
                    //var b = datosPersona.Titulo;
                    //var a = datosPersona.ID;

                    for (int i = 0; i < txtUsuarioPassword1.Text.Length; i++)
                    {
                        if (i == 0)
                        {
                            strAyudaClave += txtUsuarioPassword1.Text[i];
                        }
                        else if (i == txtUsuarioPassword1.Text.Length - 1)
                        {
                            strAyudaClave += txtUsuarioPassword1.Text[i];
                        }
                        else
                        {
                            strAyudaClave += "*";
                        }
                    }
                    ViewState["ayudaClave"] = strAyudaClave;

                    CPersonaSesion.Usuario usuario = new CPersonaSesion.Usuario();
                    usuario.idPersona = intIdPersona;
                    usuario.tipoIdentificacion = intIdTipoIdentificacion;
                    usuario.numeroIdentificacion = intNroIdentificacion;
                    usuario.expedicion = intExpedicion;
                    usuario.complemento = strComplemento;
                    usuario.nombre = strNombres.ToUpper();
                    usuario.primerApellido = strPrimerApellido.ToUpper();
                    usuario.segundoApellido = strSegundoApellido.ToUpper();
                    usuario.fechaNacimiento = CFechas.ConvierteDateTimeLong(dateFechaNacimiento);
                    usuario.genero = intGenero;
                    usuario.email = strEmail.ToUpper();
                    usuario.clave = strClave;
                    usuario.idPerfil = 2; //REGULADO
                    usuario.idUsuario = Convert.ToInt32((decimal)Session["usuario_registrado"]);

                    ViewState["usuario"] = usuario;

                    tablaVistaPrevia.Rows.Clear();

                    HtmlTableRow trRep = new HtmlTableRow();
                    HtmlTableCell thRep = new HtmlTableCell();
                    thRep.Width = "250px";
                    thRep.Style.Value = "font-weight: bold; padding-top:20px;";
                    thRep.InnerText = "USUARIO REGISTRADO";
                    HtmlTableCell tdRep = new HtmlTableCell();
                    trRep.Cells.Add(thRep);
                    trRep.Cells.Add(tdRep);
                    tablaVistaPrevia.Rows.Add(trRep);

                    HtmlTableRow tr1 = new HtmlTableRow();
                    HtmlTableCell th1 = new HtmlTableCell();
                    th1.Width = "250px";
                    th1.Style.Value = "font-weight: bold";
                    th1.InnerText = "Nro. de Identificación.";
                    HtmlTableCell td1 = new HtmlTableCell();
                    td1.InnerText = usuario.numeroIdentificacion + " " + usuario.complemento + " ";
                    switch (usuario.expedicion)
                    {
                        case 1: td1.InnerText += "CH"; break;
                        case 2: td1.InnerText += "LP"; break;
                        case 3: td1.InnerText += "CB"; break;
                        case 4: td1.InnerText += "OR"; break;
                        case 5: td1.InnerText += "PT"; break;
                        case 6: td1.InnerText += "TJ"; break;
                        case 7: td1.InnerText += "SC"; break;
                        case 8: td1.InnerText += "BN"; break;
                        case 9: td1.InnerText += "PD"; break;
                    }
                    tr1.Cells.Add(th1);
                    tr1.Cells.Add(td1);
                    tablaVistaPrevia.Rows.Add(tr1);

                    HtmlTableRow tr2 = new HtmlTableRow();
                    HtmlTableCell th2 = new HtmlTableCell();
                    th2.Width = "250px";
                    th2.Style.Value = "font-weight: bold";
                    th2.InnerText = "Nombres y Apellidos";
                    HtmlTableCell td2 = new HtmlTableCell();
                    td2.InnerText = usuario.nombre + " " + usuario.primerApellido + " " +
                                    usuario.segundoApellido;
                    tr2.Cells.Add(th2);
                    tr2.Cells.Add(td2);
                    tablaVistaPrevia.Rows.Add(tr2);

                    HtmlTableRow tr3 = new HtmlTableRow();
                    HtmlTableCell th3 = new HtmlTableCell();
                    th3.Width = "250px";
                    th3.Style.Value = "font-weight: bold";
                    th3.InnerText = "Fecha de Nacimiento";
                    HtmlTableCell td3 = new HtmlTableCell();
                    td3.InnerText = CFechas.ConvierteLongDateTime(usuario.fechaNacimiento).ToShortDateString();
                    tr3.Cells.Add(th3);
                    tr3.Cells.Add(td3);
                    tablaVistaPrevia.Rows.Add(tr3);

                    HtmlTableRow tr4 = new HtmlTableRow();
                    HtmlTableCell th4 = new HtmlTableCell();
                    th4.Width = "250px";
                    th4.Style.Value = "font-weight: bold";
                    th4.InnerText = "Género";
                    HtmlTableCell td4 = new HtmlTableCell();
                    td4.InnerText = usuario.genero == 1 ? "Masculino" : "Femenino";
                    tr4.Cells.Add(th4);
                    tr4.Cells.Add(td4);
                    tablaVistaPrevia.Rows.Add(tr4);

                    HtmlTableRow tr5 = new HtmlTableRow();
                    HtmlTableCell th5 = new HtmlTableCell();
                    th5.Width = "250px";
                    th5.Style.Value = "font-weight: bold";
                    th5.InnerText = "E-mail";
                    HtmlTableCell td5 = new HtmlTableCell();
                    td5.InnerText = usuario.email.ToLower();
                    tr5.Cells.Add(th5);
                    tr5.Cells.Add(td5);
                    tablaVistaPrevia.Rows.Add(tr5);

                    panelUsuario.Visible = false;
                    panelVistaPrevia.Visible = true;
                    //Asociar Entidades Usuario
                    //sea anh o sea externo

                    //CMessageBoxManager.ShowMessageDialog(
                    //    "El correo electrónico ingresado ya se encuentra registrado, verifique sus datos por favor",
                    //    ref mensaje, CMessageBoxManager.ERROR_MESSAGE);


                }
            }
            catch (Exception exp)
            {
                _logs.Error(exp);
            }
        }

        protected void btnVolver2_Click(object sender, ImageClickEventArgs e)
        {
            panelVistaPrevia.Visible = false;
            panelUsuario.Visible = true;
        }

        #endregion

        #region Funciones y Métodos
        //protected void CargarModulos()
        //{
        //    try
        //    {
        //        wfUsoComun objComun = new wfUsoComun();
        //        decimal decIdUsuarioAdmin = objComun.ObtnerIdFuncionarioAdministrador(this.Context);

        //        //var client = new JsonServiceClient(CParametrosHydro.strServicioAdministrador);
        //        //var listModuloCty = client.Get<wfGestionModulo.EResultadoModulo>("/ListaModulos/" + CParametrosHydro.strCredencialHydroAdmin + "/" + decIdUsuarioAdmin + "?format=json");
        //        //List<O_MODULO_CTY> listModulosVigentes = ((List<O_MODULO_CTY>)listModuloCty.oResultado).Where(l => l.AUD_ESTADO != 3).ToList();

        //        ddlModulosDisponibles.DataSource = objComun.ObtenerModulosVigentesDropDownList(decIdUsuarioAdmin);//listModulosVigentes.OrderBy(o => o.NOMBRE);
        //        ddlModulosDisponibles.DataBind();
        //        ddlModulosDisponibles.Items.Insert(0, new ListItem("SELECCIONE...", "0"));
        //        ddlModulosDisponibles.SelectedIndex = 0;

        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
        protected void CargarUsuariosSistema()
        {
            try
            {
                //var client = new JsonServiceClient(CParametrosHydro.strCredencialHydroAdmin);
                JsonServiceClient client = new JsonServiceClient(CAppSettings.ServicioAdministradorHydro);
                var listaUsuarios =
                    client.Get<EResultadoUsuario>("/ListaUsuarios/" + CParametrosHydro.strCredencialHydroAdmin + "/" + CAppSettings.idGrupo + "?format=json");
                if (listaUsuarios.oResultado != null)
                {
                    List<O_USUARIOS_CTY> usuarioEncontrado =
                        listaUsuarios.oResultado.ToList();
                        //listaUsuarios.oResultado.Where(
                        //    c => c.USUARIO.Contains(txtUsuario.Text) || c.NOMBRES.Contains(txtUsuario.Text)).ToList();
                    grvListaUsuarios.DataSource = usuarioEncontrado.OrderBy(o => o.USUARIO);
                    grvListaUsuarios.DataBind();
                }
                else
                {
                    grvListaUsuarios.DataSource = new List<O_USUARIOS_CTY>();
                    grvListaUsuarios.DataBind();
                }
            }
            catch (Exception ex)
            {
                string strUbicacionMetodo = this.GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                CLogTraza.Error(strUbicacionMetodo, Convert.ToDecimal(CAppSettings.idAplicacion), Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]), HttpContext.Current.Request.UserHostAddress, ex.Message, Convert.ToString(ex.InnerException), ex.StackTrace);
            }
        }
        protected void CargarUsuariosSistema2(decimal decId, decimal decIdTipoActividad, decimal decFiltro)
        {
            try
            {
                //var client = new JsonServiceClient(CParametrosHydro.strCredencialHydroAdmin);
                //JsonServiceClient client = new JsonServiceClient(CAppSettings.ServicioAdministradorHydro);
                var listaUsuarios =
                    client.Post<EResultadoUsuarioEntidad>(CAppSettings.svc_listar_usuarios_entidad, 
                    new EListarUsuariosEntidad
                    {
                        decFiltro = decFiltro,  // 1 = CONSULTA POR USUARIO ;2 = CONSULTA POR ENTIDAD
                        strCredencial = CParametrosHydro.strCredencialFuncionario,
                        strParametro1 = decId.ToString(),                    // 1 = ID_USUARIO, 2 = ID_ENTIDAD       
                        strParametro2 = decIdTipoActividad.ToString(),       // EN 1 = ID_TIPO_ACTIVIDAD  , 2 = ID_TIPO_ACTIVIDAD       
                        strParametro3 = ""                                                                                               
                    });//("/ListaUsuarios/" + CParametrosHydro.strCredencialHydroAdmin + "/" + CAppSettings.idGrupo + "?format=json");
                if (decFiltro == 1) //cargar grilla de entidades por usuario
                {
                    if (listaUsuarios.oResultado != null)
                    {
                        List<O_LISTA_USUARIOS_ENTIDAD_CTY> usuarioEntidad =
                            listaUsuarios.oResultado.ToList();
                        //listaUsuarios.oResultado.Where(//    c => c.USUARIO.Contains(txtUsuario.Text) || c.NOMBRES.Contains(txtUsuario.Text)).ToList();
                        grvListaEntidades.DataSource = usuarioEntidad.OrderBy(o => o.NOMBRE_ENTIDAD);
                        grvListaEntidades.DataBind();
                        CargarPerfilesUsuario(decId);
                    }
                    else
                    {
                        grvListaEntidades.DataSource = null;
                        grvListaEntidades.DataBind();
                    }
                }
                else //2 cargar grilla de usuarios por idEntidad
                {
                    if (listaUsuarios.oResultado != null)
                    {
                        List<O_LISTA_USUARIOS_ENTIDAD_CTY> usuarioEntidad =
                            listaUsuarios.oResultado.ToList();
                        //listaUsuarios.oResultado.Where(//    c => c.USUARIO.Contains(txtUsuario.Text) || c.NOMBRES.Contains(txtUsuario.Text)).ToList();
                        grvListaUsuarios.DataSource = usuarioEntidad.OrderBy(o => o.NOMBRE_USUARIO).Where(u => u.ID_USUARIO > 0);
                        grvListaUsuarios.DataBind();
                    }
                    else
                    {
                        grvListaUsuarios.DataSource = null;
                        grvListaUsuarios.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                string strUbicacionMetodo = this.GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                CLogTraza.Error(strUbicacionMetodo, Convert.ToDecimal(CAppSettings.idAplicacion), Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]), HttpContext.Current.Request.UserHostAddress, ex.Message, Convert.ToString(ex.InnerException), ex.StackTrace);
            }
        }
        /// <summary>
        /// Menu asociado al perfil asignado al usuario.
        /// </summary>
        /// <param name="idUsuario"></param>
        protected void CargarMenuUsuarioPorPerfil(decimal idUsuario)
        {
            try
            {
                //var client = new JsonServiceClient(CParametrosHydro.strCredencialHydroAdmin);
                JsonServiceClient client = new JsonServiceClient(CAppSettings.ServicioAdministradorHydro);
                var listaMenuUsuario =
                    client.Get<EResultadoMenu>("/ListaMenusPorUsuario/" + 
                                                                   CParametrosHydro.strCredencialHydroAdmin + "/" +
                                                                   idUsuario + "/" + CAppSettings.idAplicacion +
                                                                   //Convert.ToDecimal(ddlModulosDisponibles.SelectedValue) +
                                                                   "?format=json");

                if (listaMenuUsuario.oResultado != null)
                {
                    treeMenuUsuario.DataSource = listaMenuUsuario.oResultado.OrderBy(o => o.ORDEN);
                    treeMenuUsuario.DataBind();
                }
                else
                {
                    treeMenuUsuario.DataSource = new List<O_MENU_CTY>();
                    treeMenuUsuario.DataBind();
                }
            }
            catch (Exception ex)
            {
                string strUbicacionMetodo = this.GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                CLogTraza.Error(strUbicacionMetodo, Convert.ToDecimal(CAppSettings.idAplicacion), Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]), HttpContext.Current.Request.UserHostAddress, ex.Message, Convert.ToString(ex.InnerException), ex.StackTrace);
            }
        }

        protected void CargarPerfilesUsuario(decimal idUsuaro)
        {

            try
            {
                lbxPerfilesDisponibles.Items.Clear();
                lbxPerfilesAsignados.Items.Clear();
                string login_usuario = Convert.ToString(grvListaUsuarios.GetRowValues(grvListaUsuarios.FocusedRowIndex, "USUARIO"));
                List<O_PERFIL_CTY> listaPerfilesDisponibles = new List<O_PERFIL_CTY>();
                List<O_USUARIO_PERFIL_CTY> listaPerfilesAsignados = new List<O_USUARIO_PERFIL_CTY>();
                decimal idModulo = Convert.ToDecimal(CAppSettings.idAplicacion);
                List<decimal> listaIdAsignado = new List<decimal>();

                //var client = new JsonServiceClient(CParametrosHydro.strServicioAdministrador);
                JsonServiceClient client = new JsonServiceClient(CAppSettings.ServicioAdministradorHydro);

                if (Convert.ToDecimal(CAppSettings.idAplicacion) > 0)
                {
                    var resultadoPerfilesUsuario =
                        client.Get<EResultadoLista>("/EReporteUsuariosPerfiles/" +
                                                    CParametrosHydro.strCredencialHydroAdmin + "/" + Convert.ToDecimal(CAppSettings.idAplicacion) + "/" + idUsuaro + "?format=json");


                    if (resultadoPerfilesUsuario.oResultado != null)
                    {
                        listaPerfilesAsignados = (List<O_USUARIO_PERFIL_CTY>)resultadoPerfilesUsuario.oResultado;

                        listaPerfilesAsignados =
                            (from r in listaPerfilesAsignados where r.ID_USUARIO == idUsuaro select r).ToList();

                        lbxPerfilesAsignados.DataSource = listaPerfilesAsignados;
                        lbxPerfilesAsignados.DataBind();

                        foreach (var itemPerfil in listaPerfilesAsignados)
                        {
                            listaIdAsignado.Add(itemPerfil.ID_PERFIL);
                        }
                    }
                    //Obtenemos lista de  perfiles disponibles
                    var ResultadoPerfilesSistema =
                        client.Get<EResultadoPerfil>("/ListaPerfiles/" + CParametrosHydro.strCredencialHydroAdmin + "/" +
                                                   idModulo + "?format=json");

                    listaPerfilesDisponibles = (List<O_PERFIL_CTY>)ResultadoPerfilesSistema.oResultado;
                    if (listaIdAsignado.Count > 0)
                    {
                        listaPerfilesDisponibles =
                            (from l in listaPerfilesDisponibles where !listaIdAsignado.Contains(l.ID_PERFIL) select l)
                                .ToList
                                ();


                        if (login_usuario.Contains("@ANH.GOB.BO"))
                        {
                            listaPerfilesDisponibles =
                                listaPerfilesDisponibles.Where(l => l.NOMBRE_PERFIL != "REGULADO").ToList();
                        }
                        else
                        {
                            listaPerfilesDisponibles =
                              listaPerfilesDisponibles.Where(l => l.NOMBRE_PERFIL != "FUNCIONARIO ANH").ToList();
                        }

                        lbxPerfilesDisponibles.DataSource = listaPerfilesDisponibles;
                        lbxPerfilesDisponibles.DataBind();
                    }
                    else
                    {

                        if (login_usuario.Contains("@ANH.GOB.BO"))
                        {
                            listaPerfilesDisponibles =
                                listaPerfilesDisponibles.Where(l => l.NOMBRE_PERFIL != "REGULADO").ToList();
                        }
                        else
                        {
                            listaPerfilesDisponibles =
                              listaPerfilesDisponibles.Where(l => l.NOMBRE_PERFIL != "FUNCIONARIO ANH").ToList();
                        }

                        lbxPerfilesDisponibles.DataSource = listaPerfilesDisponibles;
                        lbxPerfilesDisponibles.DataBind();
                    }

                }
            }
            catch (Exception ex)
            {
                string strUbicacionMetodo = this.GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                CLogTraza.Error(strUbicacionMetodo, Convert.ToDecimal(CAppSettings.idAplicacion), Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]), HttpContext.Current.Request.UserHostAddress, ex.Message, Convert.ToString(ex.InnerException), ex.StackTrace);
            }

        }
        protected bool AgregarPerfil(decimal idPerfil, decimal decIdUsuario)
        {
            try
            {
                EInsertaPerfilUsuario objPerfilUsuario = new EInsertaPerfilUsuario();
                objPerfilUsuario.decAppIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                objPerfilUsuario.decPerfilId = idPerfil;
                objPerfilUsuario.decUsuarioId = decIdUsuario;//Convert.ToDecimal(ViewState["decIdUsuario"]);
                objPerfilUsuario.strCredencial = CParametrosHydro.strCredencialHydroAdmin;
                //var client = new JsonServiceClient(CParametrosHydro.strServicioAdministrador);
                JsonServiceClient client = new JsonServiceClient(CAppSettings.ServicioAdministradorHydro);
                var objResultado = client.Post<EResultado>("/EInsertaPerfilUsuario/?format=json", objPerfilUsuario);
                if (objResultado.intCodigo > 0)
                {return true;}
                return false;
            }
            catch (Exception ex)
            {
                string strUbicacionMetodo = this.GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                CLogTraza.Error(strUbicacionMetodo, Convert.ToDecimal(CAppSettings.idAplicacion), Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]), HttpContext.Current.Request.UserHostAddress, ex.Message, Convert.ToString(ex.InnerException), ex.StackTrace);
                return false;
            }
        }
        protected bool QuitarPerfil(decimal idPerfil)
        {
            try
            {
                ERevocaPerfilUsuario objEliminaPerfilUsuario = new ERevocaPerfilUsuario();
                objEliminaPerfilUsuario.decAppIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                objEliminaPerfilUsuario.decPerfilId = idPerfil; ;
                objEliminaPerfilUsuario.decUsuarioId = Convert.ToDecimal(ViewState["decIdUsuario"]);
                objEliminaPerfilUsuario.strCredencial = CParametrosHydro.strCredencialHydroAdmin;
                //var client = new JsonServiceClient(CParametrosHydro.strServicioAdministrador);
                JsonServiceClient client = new JsonServiceClient(CAppSettings.ServicioAdministradorHydro);

                var objResElimina = client.Put<EResultado>("/ERevocaPerfilUsuario/?format=json",
                    objEliminaPerfilUsuario);
                if (objResElimina.intCodigo > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                string strUbicacionMetodo = this.GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                CLogTraza.Error(strUbicacionMetodo, Convert.ToDecimal(CAppSettings.idAplicacion), Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]), HttpContext.Current.Request.UserHostAddress, ex.Message, Convert.ToString(ex.InnerException), ex.StackTrace);
                return false;
            }

        }
        #endregion

        protected void lnkCerrar_OnClick(object sender, EventArgs e)
        {
            //panelDetalleOverlay.Visible = false;
            pucDatos.ShowOnPageLoad = false;
        }
        protected void pnlDatos_Callback(object sender, CallbackEventArgsBase e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    if (IsPostBack)
                    {
                        CargarUsuariosSistema2(Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]), 0, 1);
                    //}
                    //else
                    //{
                        decimal decIdEntidad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdProyecto"));
                        decimal decIdTipoActividad =
                            Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad"));
                        decimal decFiltro = 2; //para cargar usuarios por entidad
                        //decimal decIdUsuario =
                        CargarUsuariosSistema2(decIdEntidad, decIdTipoActividad, decFiltro);

                        //lblRazonSocial.Text = tlsOperadores.FocusedNode.GetValue("StrDescPadre").ToString();
                        //lblNit.Text = tlsOperadores.FocusedNode.GetValue("LngNit").ToString();
                        //lblRepLegal.Text = tlsOperadores.FocusedNode.GetValue("StrDireccion").ToString();
                        //lblActividad.Text = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString();
                        //MLimpiaGrilla();
                        //Session["idActividad"] = Convert.ToInt32(tlsOperadores.FocusedNode.GetValue("IntIdActividad"));
                        //if ((int)tlsOperadores.FocusedNode.GetValue("IntAccion") > 0)
                        //{
                        //    string strMensaje = string.Empty;
                        //    if (MValidaDatosIniciales(ref strMensaje))
                        //    {
                        //        if (MValidaCondicionesFuncionamiento(ref strMensaje))
                        //        {
                        //            if (Convert.ToInt32(cmbTipoOperacion.Value) == (int)EnumTipoOperaciones.VentasEess && (Convert.ToInt32(tlsOperadores.FocusedNode.GetValue("IntIdActividad")) == (int)EnumActividades.EstacionServicioLiquido || Convert.ToInt32(tlsOperadores.FocusedNode.GetValue("IntIdActividad")) == (int)EnumActividades.EstacionServicioGnv))
                        //            {
                        //                btnImportar.Enabled = (Properties.Settings.Default.intAbm == 1);
                        //            }
                        //            else
                        //            {
                        //                btnImportar.Enabled = true;
                        //            }
                        //        }
                        //        else
                        //        {
                        //            MImprimirMensaje(strMensaje, EnumTiposMensajeAlerta.Error);
                        //        }
                        //    }
                        //    else
                        //    {
                        //        MImprimirMensaje(strMensaje, EnumTiposMensajeAlerta.Alerta);
                        //    }
                        //}
                        //else
                        //{
                        //    //todo No implementado.
                        //}

                    }

                    #endregion
                }
                catch (Exception exp)
                {
                    #region RegistraError

                    //MLimpiaGrilla();
                    //MImprimirMensaje(exp.Message, EnumTiposMensajeAlerta.Error);
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
        
        //protected void cmbTipoOperacion_Callback(object sender, CallbackEventArgsBase e)
        //{
        //    if (MValidaSesion())
        //    {
        //        try
        //        {
        //            #region Proceso a Ejecutar

        //            if (tlsOperadores.FocusedNode != null)
        //            {
        //                if ((int)tlsOperadores.FocusedNode.GetValue("IntAccion") > 0)
        //                {
        //                    //todo Implementar WS obtener tipos de operacion
        //                    var lstTipoOperacion = new List<CTipoOperacion>();
        //                    switch ((int)tlsOperadores.FocusedNode.GetValue("IntIdActividad"))
        //                    {
        //                        case (int)EnumActividades.DistribucionGlpGarrafas:
        //                            lstTipoOperacion.Add(new CTipoOperacion { IntId = 89, StrDescripcion = "VENTA" });
        //                            lstTipoOperacion.Add(new CTipoOperacion { IntId = 91, StrDescripcion = "MERMA" });
        //                            lstTipoOperacion.Add(new CTipoOperacion { IntId = 86, StrDescripcion = "RECEPCION" });
        //                            lstTipoOperacion.Add(new CTipoOperacion { IntId = 93, StrDescripcion = "DEMASIA" });
        //                            lstTipoOperacion.Add(new CTipoOperacion { IntId = 95, StrDescripcion = "SALDO INICIAL" });
        //                            break;
        //                        default:
        //                            lstTipoOperacion.Add(new CTipoOperacion { IntId = 89, StrDescripcion = "VENTAS DE COMBUSTIBLE" });
        //                            lstTipoOperacion.Add(new CTipoOperacion { IntId = 91, StrDescripcion = "MERMA DE COMBUSTIBLE" });
        //                            lstTipoOperacion.Add(new CTipoOperacion { IntId = 86, StrDescripcion = "RECEPCION DE COMBISTIBLE" });
        //                            lstTipoOperacion.Add(new CTipoOperacion { IntId = 93, StrDescripcion = "DEMASIA DE COMBUSTIBLE" });
        //                            lstTipoOperacion.Add(new CTipoOperacion { IntId = 95, StrDescripcion = "SALDO INICIAL" });
        //                            lstTipoOperacion.Add(new CTipoOperacion { IntId = 97, StrDescripcion = "MEDICION INICIAL TQ" });
        //                            lstTipoOperacion.Add(new CTipoOperacion { IntId = 98, StrDescripcion = "MEDICION FINAL TQ" });
        //                            break;
        //                    }
        //                    //
        //                    foreach (var cDato in lstTipoOperacion)
        //                    {
        //                        cmbTipoOperacion.Items.Add(cDato.StrDescripcion, cDato.IntId);
        //                    }
        //                }
        //                else
        //                {
        //                    cmbTipoOperacion.DataSource = null;
        //                }
        //            }
        //            else
        //            {
        //                //todo No implementado.
        //            }

        //            #endregion
        //        }
        //        catch (Exception exp)
        //        {
        //            #region RegistraError

        //            MImprimirMensaje(exp.Message, EnumTiposMensajeAlerta.Error);
        //            var oMensajeUsuario = new CLogTraza.MensajeUsuario
        //            {
        //                decUsuarioId = Session[CVariablesSesion.IdUsuario],
        //                decIdModulo = CParametrosHydro.decIdAplicacion,
        //                strAccion = GetType().Name + "." + MethodBase.GetCurrentMethod().Name,
        //                strIp = HttpContext.Current.Request.UserHostAddress,
        //                decNivelCapa = (int)CLogTraza.CapasNivel.Presentacion
        //            };
        //            CLogTraza.Error(oMensajeUsuario, exp);

        //            #endregion
        //        }
        //    }
        //    else
        //    {
        //        //todo No implementado.
        //    }
        //}

        ///Eventos de asignacion de perfiles
        /// 
        /// 
        protected void btnBuscarUsuarios_Click(object sender, EventArgs e)
        {
            //if (txtUsuario.Text.Length > 2)
            //{
                CargarUsuariosSistema();
            //}
            //else
            //{
            //    ClientScript.RegisterStartupScript(this.GetType(), null, "MensajeAdvertencia('DEBE ESCRIBIR MAS DE DOS LETRAS PARA BUSCAR USUARIO.');", true);
            //}
        }

        protected void btnVer_Click(object sender, EventArgs e)
        {
            var idUsuario = grvListaUsuarios.GetRowValues(grvListaUsuarios.FocusedRowIndex, "ID_USUARIO");
            var idTipoActividad = grvListaUsuarios.GetRowValues(grvListaUsuarios.FocusedRowIndex, "ID_TIPO_ACTIVIDAD");

            //if (idUsuario != null && Convert.ToDecimal(ddlModulosDisponibles.SelectedValue) > 0)
            if (idUsuario != null && Convert.ToDecimal(CAppSettings.idAplicacion) > 0)
            {
                CargarUsuariosSistema2(Convert.ToDecimal(idUsuario), 0, 1); //Cargado de entidades por usuario
                CargarMenuUsuarioPorPerfil(Convert.ToDecimal(idUsuario));
                CargarPerfilesUsuario(Convert.ToDecimal(idUsuario));
                ViewState["decIdUsuario"] = idUsuario;
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), null, "MensajeAdvertencia('MÓDULO O USUARIO SELECCIONADO NO ES VÁLIDO.');", true);
            }
            //CargarUsuariosSistema();
        }


        protected void btnAsignarUno_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToDecimal(ViewState["decIdUsuario"]) ==
               Convert.ToDecimal(grvListaUsuarios.GetRowValues(grvListaUsuarios.FocusedRowIndex, "ID_USUARIO")))
                {
                    if (AgregarPerfil(Convert.ToDecimal(lbxPerfilesDisponibles.SelectedValue), Convert.ToDecimal(ViewState["decIdUsuario"])))
                    {
                        CargarMenuUsuarioPorPerfil(Convert.ToDecimal(ViewState["decIdUsuario"]));
                        CargarPerfilesUsuario(Convert.ToDecimal(ViewState["decIdUsuario"]));
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), null, "MensajeError('NO SE PUDO ASIGNAR PERFIL AL USUARIO.');", true);
                    }

                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), null, "MensajeAdvertencia('LO PERFILES NO CORRESPONDEN AL USUARIO SELECCIONADO.');", true);
                }
            }
            catch (Exception ex)
            {
                string strUbicacionMetodo = this.GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                CLogTraza.Error(strUbicacionMetodo, Convert.ToDecimal(CAppSettings.idAplicacion), Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]), HttpContext.Current.Request.UserHostAddress, ex.Message, Convert.ToString(ex.InnerException), ex.StackTrace);
            }
        }
        
        protected void btnQuitarUno_Click(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(ViewState["decIdUsuario"]) ==
                Convert.ToDecimal(grvListaUsuarios.GetRowValues(grvListaUsuarios.FocusedRowIndex, "ID_USUARIO")))
            {
                if (lbxPerfilesAsignados.SelectedValue != "")
                {
                    if (QuitarPerfil(Convert.ToDecimal(lbxPerfilesAsignados.SelectedValue)))
                    {
                        CargarMenuUsuarioPorPerfil(Convert.ToDecimal(ViewState["decIdUsuario"]));
                        CargarPerfilesUsuario(Convert.ToDecimal(ViewState["decIdUsuario"]));
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), null,
                            "MensajeError('NO SE PUDO ELIMINAR EL PERFIL DEL USUARIO.');", true);
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), null,
                        "MensajeError('SELECCIONE EL PERFIL DE USUARIO.');", true);
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), null, "MensajeAdvertencia('LO PERFILES NO CORRESPONDEN AL USUARIO SELECCIONADO.');", true);
            }
        }

        protected void btnAsignarTodo_Click(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(ViewState["decIdUsuario"]) ==
                Convert.ToDecimal(grvListaUsuarios.GetRowValues(grvListaUsuarios.FocusedRowIndex, "ID_USUARIO")))
            {
                for (int i = 0; i < lbxPerfilesDisponibles.Items.Count; i++)
                {
                    AgregarPerfil(Convert.ToDecimal(lbxPerfilesDisponibles.Items[i].Value), Convert.ToDecimal(ViewState["decIdUsuario"]));
                }

                CargarMenuUsuarioPorPerfil(Convert.ToDecimal(ViewState["decIdUsuario"]));
                CargarPerfilesUsuario(Convert.ToDecimal(ViewState["decIdUsuario"]));
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), null, "MensajeAdvertencia('LO PERFILES NO CORRESPONDEN AL USUARIO SELECCIONADO.');", true);
            }
        }

        protected void btnQuitarTodos_Click(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(ViewState["decIdUsuario"]) ==
                Convert.ToDecimal(grvListaUsuarios.GetRowValues(grvListaUsuarios.FocusedRowIndex, "ID_USUARIO")))
            {
                for (int i = 0; i < lbxPerfilesAsignados.Items.Count; i++)
                {
                    QuitarPerfil(Convert.ToDecimal(lbxPerfilesAsignados.Items[i].Value));
                }


                CargarMenuUsuarioPorPerfil(Convert.ToDecimal(ViewState["decIdUsuario"]));
                CargarPerfilesUsuario(Convert.ToDecimal(ViewState["decIdUsuario"]));


            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), null, "MensajeAdvertencia('LO PERFILES NO CORRESPONDEN AL USUARIO SELECCIONADO.');", true);
            }
        }

        //protected void ddlModulosDisponibles_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (Convert.ToDecimal(ddlModulosDisponibles.SelectedValue) > 0)
        //    {
        //        lbxPerfilesAsignados.Items.Clear();
        //        lbxPerfilesDisponibles.Items.Clear();

        //        treeMenuUsuario.DataSource = new List<O_PERFIL_MENUS_CTY>();
        //        treeMenuUsuario.DataBind();
        //    }
        //}
        protected void btnNodeChanged_click(object sender, EventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    if (IsPostBack)
                    {
                        CargarUsuariosSistema2(Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]), 0, 1);
                        //}
                        //else
                        //{
                        decimal decIdEntidad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdOperador"));
                        decimal decIdTipoActividad =
                            Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad"));
                        decimal decFiltro = 2; //para cargar usuarios por entidad
                        //decimal decIdUsuario =
                        CargarUsuariosSistema2(decIdEntidad, decIdTipoActividad, decFiltro);

                        //lblRazonSocial.Text = tlsOperadores.FocusedNode.GetValue("StrDescPadre").ToString();
                        //lblNit.Text = tlsOperadores.FocusedNode.GetValue("LngNit").ToString();
                        //lblRepLegal.Text = tlsOperadores.FocusedNode.GetValue("StrDireccion").ToString();
                        //lblActividad.Text = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString();
                        //MLimpiaGrilla();
                        //Session["idActividad"] = Convert.ToInt32(tlsOperadores.FocusedNode.GetValue("IntIdActividad"));
                        //if ((int)tlsOperadores.FocusedNode.GetValue("IntAccion") > 0)
                        //{
                        //    string strMensaje = string.Empty;
                        //    if (MValidaDatosIniciales(ref strMensaje))
                        //    {
                        //        if (MValidaCondicionesFuncionamiento(ref strMensaje))
                        //        {
                        //            if (Convert.ToInt32(cmbTipoOperacion.Value) == (int)EnumTipoOperaciones.VentasEess && (Convert.ToInt32(tlsOperadores.FocusedNode.GetValue("IntIdActividad")) == (int)EnumActividades.EstacionServicioLiquido || Convert.ToInt32(tlsOperadores.FocusedNode.GetValue("IntIdActividad")) == (int)EnumActividades.EstacionServicioGnv))
                        //            {
                        //                btnImportar.Enabled = (Properties.Settings.Default.intAbm == 1);
                        //            }
                        //            else
                        //            {
                        //                btnImportar.Enabled = true;
                        //            }
                        //        }
                        //        else
                        //        {
                        //            MImprimirMensaje(strMensaje, EnumTiposMensajeAlerta.Error);
                        //        }
                        //    }
                        //    else
                        //    {
                        //        MImprimirMensaje(strMensaje, EnumTiposMensajeAlerta.Alerta);
                        //    }
                        //}
                        //else
                        //{
                        //    //todo No implementado.
                        //}

                    }

                    #endregion
                }
                catch (Exception exp)
                {
                    #region RegistraError

                    //MLimpiaGrilla();
                    //MImprimirMensaje(exp.Message, EnumTiposMensajeAlerta.Error);
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
    
    #region ResponseDTO
    public class EResultadoUsuario
    {
        public decimal decCodigo { get; set; }
        public string strMensaje { get; set; }
        public List<O_USUARIOS_CTY> oResultado { get; set; }
    }
    public class EResultadoUsuarioEntidad
    {
        public decimal decCodigo { get; set; }
        public string strMensaje { get; set; }
        public List<O_LISTA_USUARIOS_ENTIDAD_CTY> oResultado { get; set; }
    }
    public class EResultadoEntidad
    {
        public int IntCodigo { get; set; }
        public string StrMensaje { get; set; }
        public List<O_OBTIENE_ENTIDADES_USR_CTY> OResultado { get; set; }
    }
    public class EResultadoMenu
    {
        public decimal decCodigo { get; set; }
        public string strMensaje { get; set; }
        public List<O_MENU_CTY> oResultado { get; set; }
    }
    public class EResultadoPerfil
    {
        public int intCodigo { get; set; }
        public string strMensaje { get; set; }
        public List<O_PERFIL_CTY> oResultado { get; set; }
    }
    public class EResultadoLista
    {
        public decimal decCodigo { get; set; }
        public string strMensaje { get; set; }
        public List<O_USUARIO_PERFIL_CTY> oResultado { get; set; }
    }
    public class EAsignaUsuarioGrupo
    {
        public string strCredencial { get; set; }
        public int decGrupoId { get; set; }
        public int decUsuarioId { get; set; }
        public int decAppIdUsuario { get; set; }
    }
    class EResultadoGenerico
    {
        public int intCodigo { get; set; }
        public string strMensaje { get; set; }
        public O_RESULTADO_CTY oResultado { get; set; }
    }
    public class O_PERFIL_CTY
    {
        public decimal ID_PERFIL { get; set; }
        public string NOMBRE_PERFIL { get; set; }
        public Nullable<decimal> MODULO_ID { get; set; }
        public Nullable<decimal> PRIORIDAD { get; set; }
        public string DESCRIPCION { get; set; }
    }
    public class O_USUARIOS_CTY
    {
        public decimal ID_USUARIO { get; set; }
        public string USUARIO { get; set; }
        public string NOMBRES { get; set; }
        public Nullable<System.DateTime> VIGENTE_DESDE { get; set; }
        public Nullable<System.DateTime> VIGENTE_HASTA { get; set; }
        public string ESTADO { get; set; }
        public Nullable<decimal> ASIGNADO { get; set; }
        public string VIGENCIA { get; set; }
    }
    public class O_MENU_CTY
    {
        public decimal ID_MENU { get; set; }
        public string TITULO { get; set; }
        public string ABREVIACION { get; set; }
        public string ENLACE { get; set; }
        public Nullable<decimal> ID_MENU_PADRE { get; set; }
        public decimal ID_MODULO { get; set; }
        public decimal ORDEN { get; set; }
        public Nullable<decimal> NIVEL { get; set; }
        public byte[] ICONO { get; set; }
        public string DESCRIPCION { get; set; }
    }

    public class O_USUARIO_PERFIL_CTY
    {
        public decimal ID_USUARIO { get; set; }
        public string USUARIO { get; set; }
        public string NOMBRES_USUARIO { get; set; }
        public string SISTEMA { get; set; }
        public string NOMBRE_PERFIL { get; set; }
        public decimal ID_PERFIL { get; set; }
        public Nullable<decimal> APP_ID_USUARIO { get; set; }
        public string ASIGNADO_POR { get; set; }
        public System.DateTime FECHA_ASIGNACION { get; set; }
    }
    public class O_PERFIL_MENUS_CTY
    {
        public decimal ID_MENU_PERFIL { get; set; }
        public Nullable<decimal> ID_PRIVILEGIO { get; set; }
        public Nullable<decimal> PRIORIDAD { get; set; }
        public string PRIVILEGIO { get; set; }
        public decimal ID_PERFIL { get; set; }
        public string NOMBRE_PERFIL { get; set; }
        public Nullable<decimal> ID_MODULO { get; set; }
        public string NOMBRE_MODULO { get; set; }
        public decimal ID_MENU { get; set; }
        public string TITULO { get; set; }
        public Nullable<decimal> ORDEN { get; set; }
        public Nullable<decimal> NIVEL { get; set; }
        public string TITULO_PADRE { get; set; }
        public Nullable<decimal> ID_PADRE_MENU { get; set; }
    }
    //public partial class O_LISTA_USUARIOS_ENTIDAD_CTY
    //{
    //    public decimal ID_USUARIO_ENTIDAD { get; set; }
    //    public decimal APP_ID_USUARIO { get; set; }
    //    public decimal TIPO_USUARIO { get; set; }
    //    public System.DateTime FECHA_HASTA { get; set; }
    //    public System.DateTime FECHA_DESDE { get; set; }
    //    public string NOMBRE_ACTIVIDAD { get; set; }
    //    public string NOMBRE_ENTIDAD { get; set; }
    //    public decimal ID_ENTIDAD { get; set; }
    //    public string NOMBRE_USUARIO { get; set; }
    //    public decimal ID_USUARIO { get; set; }
    //    public System.DateTime APP_FECHA_REGISTRO { get; set; }
    //    public decimal ID_TIPO_ACTIVIDAD { get; set; }
    //}
    public partial class O_LISTA_USUARIOS_ENTIDAD_CTY
    {
        public decimal ID_USUARIO_ENTIDAD { get; set; }
        public Nullable<decimal> APP_ID_USUARIO { get; set; }
        public decimal TIPO_USUARIO { get; set; }
        public Nullable<System.DateTime> FECHA_HASTA { get; set; }
        public Nullable<System.DateTime> FECHA_DESDE { get; set; }
        public string NOMBRE_ACTIVIDAD { get; set; }
        public string NOMBRE_ENTIDAD { get; set; }
        public decimal ID_ENTIDAD { get; set; }
        public string NOMBRE_USUARIO { get; set; }
        public Nullable<decimal> ID_USUARIO { get; set; }
        public Nullable<System.DateTime> APP_FECHA_REGISTRO { get; set; }
        public decimal ID_TIPO_ACTIVIDAD { get; set; }
    }
    public class EListarUsuariosEntidad
    {
        public string strCredencial { get; set; }
        public decimal decFiltro { get; set; }
        public string strParametro1 { get; set; }
        public string strParametro2 { get; set; }
        public string strParametro3 { get; set; }
    }
    public class ERegistraUsuarioEntidadGeneral 
    {
        public decimal decIdEntidad { get; set; }
        public decimal decIdUsuario { get; set; }
        public decimal decIdTipoUsuario { get; set; }
        public decimal decIdTipoActividad { get; set; }
        public decimal decAppIdUsuario { get; set; }
        public DateTime dateAppFechaRegistro { get; set; }
    }  
    public class EAsignarPerfilActividad
    {
        public string strCredencialHydro { get; set; }
        public decimal decPerfilId { get; set; }
        public decimal decActividadId { get; set; }
        public decimal decAppIdUsuario { get; set; }
    }
    [Serializable]
    public class EntidadActividad
    {
        public decimal ID_ENTIDAD { get; set; }
        public decimal TIPO_ACTIVIDAD { get; set; }
    }
    [Serializable]
    public class Perfil
    {
        public decimal ID_PERFIL { get; set; }
    }
    public class CArbol
    {
        public int IntId { get; set; }
        public int IntIdPadre { get; set; }
        public int IntIdEntidadPadre { get; set; }
        public int IntIdEntidad { get; set; }
        public int IntIdOperador { get; set; }
        public int IntIdProyecto { get; set; }
        public int IntIdActividad { get; set; }
        public string StrDescripcion { get; set; }
        public string StrDescPadre { get; set; }
        public long LngNit { get; set; }
        public string StrActividad { get; set; }
        public string StrLicencia { get; set; }
        public string StrRepLegal { get; set; }
        public string StrDireccion { get; set; }
        public string StrDepartamento { get; set; }
        public string StrMunicipio { get; set; }
        public string StrLocalidad { get; set; }
        public string StrTelefono { get; set; }
        public string StrAmbito { get; set; }
        public string StrTipoSociedad { get; set; }
        public string StrPropietario { get; set; }
        public string StrCiPropietario { get; set; }
        public string StrCiRepresentante { get; set; }
        public int IntAccion { get; set; }
    }       
    #endregion
    #region Declaración de clases
    public class ListaMenusPorUsuario : IReturn<EResultado>
    {
        public string strCredencialHydro { get; set; }
        public decimal decUsuarioId { get; set; }
        public decimal decModuloId { get; set; }
    }
    
    public class EInsertaPerfilUsuario : IReturn<EResultado>
    {
        public string strCredencial { get; set; }
        public decimal decPerfilId { get; set; }
        public decimal decUsuarioId { get; set; }
        public decimal decAppIdUsuario { get; set; }
    }
    public class ERevocaPerfilUsuario : IReturn<EResultado>
    {
        public string strCredencial { get; set; }
        public decimal decPerfilId { get; set; }
        public decimal decUsuarioId { get; set; }
        public decimal decAppIdUsuario { get; set; }
    }
    #endregion*/
}