using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using AnhClases;
using System.Configuration;
using AnhHydroOctanoAgenteServicio;
using AnhHydroOctanoPresentacion.Clases.Persona;
using AnhHydroOctanoPresentacion.Entidades;
using AnhHydroOctanoPresentacion.Lib;
using AnhHydroOctanoPresentacion.Parametros;
using AnhHydroOctanoAgenteServicio.ServicioHydroSesion;
//using AnhHydroOctanoPersistencia.Core;
using Librerias.Anh.Us;
using ServiceStack.ServiceClient.Web;
using O_USUARIO_CTY = AnhHydroOctanoAgenteServicio.ServicioHydroSesion.O_USUARIO_CTY;
using DevExpress.Web;

namespace AnhHydroOctanoPresentacion.Sitio.Persona
{
    public partial class wfAutenticacion : System.Web.UI.Page
    {
        #region Variables

        O_USUARIO_CTY _usuario;
        private CPersonaSesion _sesion;
        string direccionIp = string.Empty;
        private string _strMensajeError;

        #endregion
        
        #region ejecucion de la libreria
        protected void page_Init()
        {
            try
            {
                if (!CConsultaAccesos.AccesoFormulario(CParametrosHydro.strCredencialOctanoVolumen,
                    Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]), Convert.ToDecimal(CParametrosHydro.decIdAplicacion), Path.GetFileName(Request.Path),
                    ref _strMensajeError))
                {
                    FormsAuthentication.SignOut();
                }
            }
            catch (Exception ex)
            {

                string strUbicacionMetodo = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                CLogTraza.Error(strUbicacionMetodo, CParametrosHydro.decIdAplicacion,
                    Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]), HttpContext.Current.Request.UserHostAddress,
                    ex.Message, Convert.ToString(ex.InnerException), ex.StackTrace);
                FormsAuthentication.SignOut();
                Response.Cookies.Remove(CVariablesSesion.IdAutenticacion);
                Session.RemoveAll();
                Response.Redirect("~/Sitio/Autenticacion/wfAutenticacion.aspx",false);
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //direccionIp = Convert.ToString(ipCliente.AddressList.Length - 1);
                direccionIp = "de la Direccion IP: " + HttpContext.Current.Request.UserHostAddress;
                HttpBrowserCapabilities browser = Request.Browser;
                decimal version = Convert.ToDecimal(browser.Version);
                if ((browser.Browser == "Firefox" && version > 15) || (browser.Browser == "Chrome" && version > 20) || (browser.Browser == "Safari" && version >= 5))
                {
                    _sesion = new CPersonaSesion(Server.MapPath("~"), Request.ServerVariables["REMOTE_ADDR"]);
                    if (!IsPostBack)
                    {
                        if (Request.QueryString["NPFL"] != null)
                        {
                            MostrarMensajeError("No tiene privilegios suficientes para acceder al sistema, contactese con el administrador.", true);
                        }
                        else
                        {
                            MostrarMensajeError("", false);
                        }
                        loginUsuario.Controls[0].Controls[1].Focus();
                        if (Request.IsAuthenticated)
                        {
                            var httpCookie = Request.Cookies[CVariablesSesion.IdAutenticacion];
                            if (httpCookie != null)
                            {
                                decimal decIdUsuario = Convert.ToDecimal(cEncriptacion.DecryptString(httpCookie.Value));

                                O_DETALLE_USUARIO_CTY detalleUsuario = _sesion.ObtenerUsuario(decIdUsuario);
                                if (detalleUsuario != null)
                                {
                                    _usuario = new O_USUARIO_CTY
                                    {
                                        ID_USUARIO = detalleUsuario.ID_USUARIO,
                                        ID_ENTIDAD = detalleUsuario.ENTIDAD_ID,
                                        NOMBRE_COMPLETO = detalleUsuario.NOMBRE
                                    };
                                    Session.Add(CVariablesSesion.IdUsuario, detalleUsuario.ID_USUARIO);
                                    Session.Add(CVariablesSesion.DatosUsuario,
                                        detalleUsuario.NOMBRE + " " + detalleUsuario.PRIMER_APELLIDO + " " +
                                        detalleUsuario.SEGUNDO_APELLIDO);
                                    Session.Add(CVariablesSesion.Mail, detalleUsuario.EMAIL);

                                    Session.Add(CVariablesSesion.CI, detalleUsuario.NUMERO_IDENTIFICACION + " " + detalleUsuario.EXPEDICION_ABREVIACION);
                                    
                                    Session.Add(CVariablesSesion.ObjUsuario, _usuario);
                                    Session.Add(CVariablesSesion.UsuarioPassword, detalleUsuario.CLAVE );
                                    MRedireccionar();
                                }
                            }
                            else
                            {
                                FormsAuthentication.SignOut();
                                Session.RemoveAll();
                            }
                        }
                    }
                }
                else
                {
                    Response.Redirect("~/Default.aspx",false);
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                }
            }
            catch (System.Threading.ThreadAbortException)
            {
                // Ignora la excepción de este tipo
            }
            catch (Exception exp)
            {
                //_log.Error("Error " + exp.Message + " - " + exp.InnerException + " - " + exp.StackTrace);
                var oMensajeUsuario = new CLogTraza.MensajeUsuario
                {
                    decUsuarioId = Session[CVariablesSesion.IdUsuario],
                    decIdModulo = CParametrosHydro.decIdAplicacion,
                    strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name,
                    strIp = HttpContext.Current.Request.UserHostAddress,
                    decNivelCapa = (int) CLogTraza.CapasNivel.Presentacion
                };

                //CLogTraza.Error(oMensajeUsuario, exp);
            }
        }

        #region 
        protected void LoginUsuario_Authenticate(object sender, AuthenticateEventArgs e)
        {
            Page.Validate("loginValidation");
            if (!Page.IsValid) return;
            try
            {
                Session.Remove(CVariablesSesion.ObjetoArbol);
                string strUsuario = loginUsuario.UserName.ToUpper().Trim();
                string strUsuarioEntidad = strUsuario;
                string strClave = loginUsuario.Password;
                CParametrosHydro.StrUsuarioLogin = strUsuario;
                var captcha = (sender as Login).FindControl("Captcha_ID") as ASPxCaptcha;
                bool resultadocaptcha = captcha.IsValid;
                if (resultadocaptcha)
                {

                    if (!strUsuario.Contains("@") || strUsuario.EndsWith("ANH.GOB.BO"))
                    { // SI ES FUNCIONARIO
                        _usuario = _sesion.AutenticarActDir(strUsuario, strClave);

                        if (_usuario != null)
                        {
                            //Obtiene datos del funcionario y almacena en variable de session
                            JsonServiceClient clienteSirh = new JsonServiceClient(ServiciosRest.ServicioJsonSIRH);
                            var listaResultado = clienteSirh.Get<EResultadoLista<O_DATOS_FUNCIONARIO_CTY>>(ServiciosRest.SvcDatosFuncionarioANH + CParametrosHydro.StrCredencialRrhh + "/ " + _usuario.ID_USUARIO + "?format=json");
                            O_DATOS_FUNCIONARIO_CTY funcionarioAnh = listaResultado.oResultado.FirstOrDefault();
                            if (funcionarioAnh != null)
                            {
                                Session.Add(CVariablesSesion.ObjFuncionarioANH, funcionarioAnh);
                            }
                            //Guarda los datos del usuaio en una variable de sessión
                            if (_usuario.PERFIL_ID >= 0 && _usuario.ESTADO > 0)
                            {
                                e.Authenticated = true;
                                Session.Add(CVariablesSesion.IdUsuario, _usuario.ID_USUARIO);
                                Session.Add(CVariablesSesion.DatosUsuario, _usuario.NOMBRE_COMPLETO);
                                Session.Add(CVariablesSesion.UsuarioNombre, strUsuario);
                                Session.Add(CVariablesSesion.PerfilUsuario, _usuario.PERFIL);
                                Session.Add(CVariablesSesion.Mail, strUsuarioEntidad);
                                Session.Add(CVariablesSesion.UsuarioPassword, strClave);
                                Session.Add(CVariablesSesion.ObjUsuario, _usuario);
                                //_log.Info("Usuario que Ingreso al Sistema: usuario: " + strUsuario + " Perfil: " + _usuario.PERFIL + " Nombre Completo: " + _usuario.NOMBRE_COMPLETO + " " + direccionIp);
                            }
                            else
                            {
                                switch (Convert.ToInt32(_usuario.ID_USUARIO))
                                {
                                    case -1:
                                        Session.Add("login", strUsuario);
                                        Session.Add("Pass", strClave);
                                        Response.Redirect("~/Sitio/Autenticacion/wfSalir.aspx");
                                        //_log.Info("Usuario que debe Registrarse: usuario" + strUsuario + " Perfil" + _usuario.PERFIL + " Nombre Completo" + _usuario.NOMBRE_COMPLETO + " " + direccionIp);
                                        //idmsjError.Text = "Señor usuario que debe Registrarse en la Plataforma Hydro";
                                        CLog.Informacion(this.Context, "Usuario que debe Registrarse: usuario" + strUsuario + " Perfil" + _usuario.PERFIL + " Nombre Completo" + _usuario.NOMBRE_COMPLETO + " " + direccionIp);
                                        break;
                                    default:
                                        //CMessageBoxManager.ShowMessageDialog("El usuario ingresado no tiene permisos para acceder a este módulo.", ref mensaje, CMessageBoxManager.ERROR_MESSAGE);
                                        MostrarMensajeError("El usuario ingresado no tiene permisos para acceder a este módulo.", true);
                                        e.Authenticated = false;
                                        CLog.Error(this.Context, GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name, null);
                                        //_log.Error("El usuario ingresado no tiene permisos para acceder a este módulo usuario " + strUsuario + " " + direccionIp);
                                        break;
                                }
                            }
                        }
                    }
                    else
                    {
                        strClave = cEncriptacion.generarMD5(loginUsuario.Password).ToUpper();
                        _usuario = _sesion.Autenticar(strUsuarioEntidad, strClave);
                        if (_usuario != null)
                        {
                            if (_usuario.ID_USUARIO > 0)
                            {
                                // 1 usuarios normales creados por el sirehidro
                                //-1 permite autenticar usuarios de tipo_usuario 2 creados a traves de octano volumenes
                                if (_usuario.ESTADO == 1 || _usuario.ESTADO == -1)
                                {
                                    e.Authenticated = true;
                                    Session.Add(CVariablesSesion.ObjUsuario, _usuario);

                                    Session.Add(CVariablesSesion.IdUsuario, _usuario.ID_USUARIO);
                                    Session.Add(CVariablesSesion.DatosUsuario, _usuario.NOMBRE_COMPLETO);
                                    Session.Add(CVariablesSesion.PerfilUsuario, _usuario.PERFIL);
                                    Session.Add(CVariablesSesion.Mail, strUsuarioEntidad);
                                    //_log.Info("Usuario que Ingreso al Sistema: usuario: " + strUsuario + " Perfil: " + _usuario.PERFIL + " Nombre Completo: " + _usuario.NOMBRE_COMPLETO + " " + direccionIp);
                                    if (_usuario.ID_ENTIDAD > 0)
                                    {
                                        Session.Add(CVariablesSesion.IdEntidad, _usuario.ID_ENTIDAD);
                                    }
                                }
                                else
                                {
                                    //CMessageBoxManager.ShowMessageDialog("Esta cuenta  no esta activada, ingrese a su cuenta de correo para activar su cuenta.", ref mensaje, CMessageBoxManager.ERROR_MESSAGE);
                                    MostrarMensajeError(
                                            "Esta cuenta  no esta activada, ingrese a su cuenta de correo para activar su cuenta.",
                                            true);
                                    loginUsuario.FailureAction = LoginFailureAction.Refresh;
                                    //_log.Error("Esta cuenta  no esta activada, ingrese a su cuenta de correo para activar su cuenta. " + strUsuario + " " + direccionIp);
                                }
                            }
                            else
                            {
                                //CMessageBoxManager.ShowMessageDialog("* Usuario y/o clave incorrectos.", ref mensaje, CMessageBoxManager.ERROR_MESSAGE);
                                //alert.Visible = true;
                                //idmsjError.Text = "Verifique que su usuario y contraseña.";
                                MostrarMensajeError("* Usuario y/o clave incorrectos.", true);
                                e.Authenticated = false;
                                //_log.Error("* Usuario y/o clave incorrectos. " + strUsuario + " " + direccionIp);
                            }
                        }
                        else
                        {
                            //CMessageBoxManager.ShowMessageDialog("* Usuario y/o clave incorrectos.", ref mensaje, CMessageBoxManager.ERROR_MESSAGE);
                            MostrarMensajeError("* Usuario y/o clave incorrectos.", true);
                            e.Authenticated = false;
                            //_log.Error("* Usuario y/o clave incorrectos. " + strUsuario + " " + direccionIp);
                        }
                    }
                }
                else
                {
                    MostrarMensajeError("* Captcha Incorrecto.", true);
                    e.Authenticated = false;
                }
            }
            catch (Exception exp)
            {
                MostrarMensajeError("** Usuario y/o clave incorrectos.", true);
                var oMensajeUsuario = new CLogTraza.MensajeUsuario
                {
                    decUsuarioId = Session[CVariablesSesion.IdUsuario],
                    decIdModulo = CParametrosHydro.decIdAplicacion,
                    strAccion = GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name,
                    strIp = HttpContext.Current.Request.UserHostAddress,
                    decNivelCapa = (int) CLogTraza.CapasNivel.Presentacion
                };

                CLogTraza.Error(oMensajeUsuario, exp);
                //_logs.Error(exp);
                //CMessageBoxManager.ShowMessageDialog("** Usuario y/o clave incorrectos.", ref mensaje, CMessageBoxManager.ERROR_MESSAGE);
                loginUsuario.FailureAction = LoginFailureAction.Refresh;
                //_log.Error("Error Usuario y/o clave incorrectos. " + exp.Message + " - " + exp.InnerException + " - " + exp.StackTrace + " " + direccionIp);
            }
        }

        protected void LoginUsuario_LoggedIn(object sender, EventArgs e)
        {
            if (_usuario != null)
            {
                var cookie = new HttpCookie(CVariablesSesion.IdAutenticacion, cEncriptacion.EncryptString(_usuario.ID_USUARIO.ToString(CultureInfo.InvariantCulture)))
                {
                    Expires = loginUsuario.RememberMeSet ? DateTime.Now.AddDays(30) : DateTime.Now.AddDays(-1)
                };
                Response.Cookies.Add(cookie);
                MRedireccionar();
            }
        }

        #endregion

        #region Metodos y Funciones

        public void MostrarMensajeError(string mensaje, bool visible)
        {

            lblMensajeError.Text = mensaje;
            divMensaje.Visible = visible;
        }
        public void MRedireccionar()
        {
            List<O_MODULOS_USUARIO_CTY> modulos = _sesion.ObtenerModulos(_usuario.ID_USUARIO);
            if (modulos != null && modulos.Count > 0)
            {
                O_MODULOS_USUARIO_CTY moduloPrincipal = modulos.Find(m => m.ID_MODULO == CParametrosHydro.decIdAplicacion);
                if (moduloPrincipal != null)
                {
                    List<O_PERFILES_USUARIO_CTY> perfiles = _sesion.ObtenerPerfiles(_usuario.ID_USUARIO,
                        moduloPrincipal.ID_MODULO);
                    if (perfiles != null && perfiles.Count > 0)
                    {
                        Session[CVariablesSesion.PerfilesUsuario] = perfiles;
                        foreach (O_PERFILES_USUARIO_CTY perfil in perfiles)
                        {
                            List<O_MENUS_USUARIO_CTY> menues = _sesion.ObtenerMenusPerfil(perfil.ID_PERFIL);
                            if (menues != null && menues.Count > 0)
                            {
                                List<O_MENUS_USUARIO_CTY> menusPerfilModulo =
                                    menues.FindAll(m => m.ID_MODULO == moduloPrincipal.ID_MODULO);
                                menusPerfilModulo = (from t in menusPerfilModulo
                                                     orderby t.ID_MENU_PADRE,t.ORDEN ascending 
                                                     select t).ToList();
                               
                                foreach (O_MENUS_USUARIO_CTY menu in menusPerfilModulo)
                                {
                                    if (menu.ENLACE == "#")
                                    {
                                        continue;
                                    }
                                    Response.Redirect("~/Sitio/Movimientos/WFPrincipal.aspx");
                                    //Response.Redirect(menu.ENLACE);
                                    break;
                                }
                                foreach (O_MENUS_USUARIO_CTY menu in menues)
                                {
                                    if (menu.ENLACE == "#")
                                    {
                                        continue;
                                    }
                                    Response.Redirect(menu.ENLACE);
                                    break;
                                }
                            }
                        }
                    }
                }
                //Response.Redirect("~/Sitio/Consultas/prueba.aspx");
                //Response.Redirect("~/Test/WFFlujoOperador.aspx");
                Response.Redirect("~/Sitio/Movimientos/WFPrincipal.aspx");
                //Response.Redirect(modulos[0].URL);
            }
        }
        
        #endregion
    }
}