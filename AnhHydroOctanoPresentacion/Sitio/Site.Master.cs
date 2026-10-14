using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using AnhHydroOctanoAgenteServicio;
using AnhHydroOctanoAgenteServicio.ServicioHydroSesion;
using AnhHydroOctanoPresentacion.Clases.Persona;
using AnhHydroOctanoPresentacion.Lib;
using AnhHydroOctanoPresentacion.Parametros;



namespace AnhHydroOctanoPresentacion
{
    public partial class Site : MasterPage
    {
        #region Variables de entorno
        /// <summary>
        /// El identificador del usuario sesionado.
        /// </summary>
        private int _intIdUsuario;

        /// <summary>
        /// El gestor del servicio Hydro Sesion
        /// </summary>
        private CPersonaSesion _sesion;

        /// <summary>
        /// Recurso de registro de errores de la aplicación.
        /// </summary>
        private CLogs _logs;


        private string _mensajeError;
        private IServicioHydroSesion _servicio;

        #endregion
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[CVariablesSesion.IdUsuario] != null && Request.IsAuthenticated)
            {
                Librerias.Anh.Us.CConfiguracionRegional.GetCustomCultureInfo();
                _intIdUsuario = Int32.Parse(Session[CVariablesSesion.IdUsuario].ToString());
                _sesion = new CPersonaSesion(Server.MapPath("~"), Request.ServerVariables["REMOTE_ADDR"]);
                
                mCargarUsuario();
                panelUsuario.Visible = true;
                mCargarModulos();
            }
        }
        private void mCargarModulos()
        {
            try
            {
                List<O_MODULOS_USUARIO_CTY> modulos = _sesion.ObtenerModulos(_intIdUsuario);
                if (modulos != null)
                {
                    menuModulo.Items.Clear();
                    foreach (O_MODULOS_USUARIO_CTY modulo in modulos)
                    {
                        MenuItem itemModulo = new MenuItem(modulo.DESCRIPCION);
                        itemModulo.NavigateUrl = modulo.URL;
                        menuModulo.Items.Add(itemModulo);
                    }
                }
            }
            catch (Exception exp)
            {
                _logs.Error(exp);
            }

        }

        private void mCargarUsuario()
        {
            //lblUsuario.Text = Session[CVariablesSesion.DatosUsuario].ToString();
            _servicio = LocalizadorProxy.ServicioAutenticarUsuario();
            if (Session[CVariablesSesion.DatosUsuario] != null)
            {
                lblUsuario.Text = Session[CVariablesSesion.DatosUsuario].ToString();
                try
                {
                    menu.Items.Clear();
                    List<O_MENUS_USUARIO_CTY> menues = _sesion.ObtenerMenus(CParametrosHydro.decIdAplicacion, _intIdUsuario);
                    if (menues != null && menues.Count > 0)
                    {// SI NO TIENE MENUS ASIGNADOS - EJ. FUNCIONARIOS CON ASIGNACION ESPECIFICA
                        //menues = menues.OrderBy(m => m.ORDEN).ToList();
                        List<O_PERFILES_USUARIO_CTY> perfiles = _sesion.ObtenerPerfiles(_intIdUsuario, CParametrosHydro.decIdAplicacion);
                        if (perfiles != null)
                        {
                            //panelMenu.Visible = false;
                            foreach (O_PERFILES_USUARIO_CTY perfil in perfiles)
                            {
                                if (perfil.ID_PERFIL == Convert.ToDecimal(CVariablesSesion.IdFuncionario))
                                {
                                    List<O_MENUS_USUARIO_CTY> menusPerfil = _sesion.ObtenerMenusPerfil(Convert.ToDecimal(CVariablesSesion.IdFuncionario));
                                    foreach (O_MENUS_USUARIO_CTY menuPerfil in menusPerfil)
                                    {
                                        if (menuPerfil.ID_MODULO == CParametrosHydro.decIdAplicacion)
                                        {
                                            int intPosicion = 0;
                                            foreach (O_MENUS_USUARIO_CTY tempMenu in menues)
                                            {
                                                if (tempMenu.ORDEN >= menuPerfil.ORDEN)
                                                {
                                                    break;
                                                }
                                                intPosicion++;
                                            }
                                            menues.Insert(intPosicion, menuPerfil);
                                        }
                                    }
                                }
                            }
                        }

                        List<O_MENUS_USUARIO_CTY> menuesPadre = menues.FindAll(m => m.ID_MENU_PADRE == 0);
                        var menuesPadreEli = new List<O_MENUS_USUARIO_CTY>();
                        menuesPadreEli = EliminaDuplicado(menuesPadre);
                        foreach (O_MENUS_USUARIO_CTY menuPadre in menuesPadreEli)
                        {
                            MenuItem itemPadre = mCargarMenu(menues, menuPadre);
                            menu.Items.Add(itemPadre);
                        }
                        panelMenu.Visible = true;
                    }
                    else
                    { // SI NO TIENE MENUS ASIGNADOS - EJ. REGULADOS Y FUNCIONARIOS SIN ASIGNACION ESPECIFICA
                        menues = new List<O_MENUS_USUARIO_CTY>();
                        List<O_PERFILES_USUARIO_CTY> perfiles = _sesion.ObtenerPerfiles(_intIdUsuario, CParametrosHydro.decIdAplicacion);
                        if (perfiles != null)
                        {
                            foreach (O_PERFILES_USUARIO_CTY perfil in perfiles)
                            {
                                if (perfil.ID_PERFIL == Convert.ToDecimal(CVariablesSesion.IdFuncionario))
                                {
                                    List<O_MENUS_USUARIO_CTY> menusPerfiles = _sesion.ObtenerMenusPerfil(Convert.ToDecimal(CVariablesSesion.IdFuncionario));

                                    List<O_MENUS_USUARIO_CTY> menusPerfil = menusPerfiles.OrderBy(x => x.ORDEN).ToList();

                                    foreach (O_MENUS_USUARIO_CTY menuPerfil in menusPerfil)
                                    {
                                        if (menuPerfil.ID_MODULO == CParametrosHydro.decIdAplicacion)
                                        {
                                            menues.Add(menuPerfil);
                                        }
                                    }
                                }
                                if (perfil.ID_PERFIL == CVariablesSesion.IdRegulado)
                                {
                                    // obtener perfiles hijo de regulado
                                    List<O_PERFILES_USUARIO_CTY> perfilesHijo = perfiles.FindAll(p => /*ID_PADRE*/p.ID_PERFIL == CVariablesSesion.IdRegulado);
                                    // sumar menu PERFIL_MENU de regulado (for)
                                    foreach (O_PERFILES_USUARIO_CTY perfilHijo in perfilesHijo)
                                    {
                                        List<O_MENUS_USUARIO_CTY> menusPerfilHijo = _sesion.ObtenerMenusPerfil(perfilHijo.ID_PERFIL);

                                        //List<O_MENUS_USUARIO_CTY> menusPerfilHijito = menusPerfiles.OrderBy(x => x.ORDEN).ToList();

                                        foreach (O_MENUS_USUARIO_CTY menuPerfil in menusPerfilHijo)
                                        {
                                            if (menuPerfil.ID_MODULO == CParametrosHydro.decIdAplicacion)
                                            {
                                                menues.Add(menuPerfil);
                                            }
                                        }
                                    }
                                }
                            }

                        }

                        menues = menues.OrderBy(m => m.ORDEN).ToList();
                        List<O_MENUS_USUARIO_CTY> menuesPadre = menues.FindAll(m => m.ID_MENU_PADRE == 0);
                        foreach (O_MENUS_USUARIO_CTY menuPadre in menuesPadre)
                        {
                            MenuItem itemPadre = mCargarMenu(menues, menuPadre);
                            menu.Items.Add(itemPadre);
                        }
                        panelMenu.Visible = true;
                    }
                }
                catch (Exception exp)
                {
                    _logs.Error(exp);
                }
            }
        }

        private List<O_MENUS_USUARIO_CTY> EliminaDuplicado(List<O_MENUS_USUARIO_CTY> menuesPadre)
        {
            Dictionary<decimal, decimal> uniqueStore = new Dictionary<decimal, decimal>();
            List<O_MENUS_USUARIO_CTY> finalList = new List<O_MENUS_USUARIO_CTY>();
            foreach (O_MENUS_USUARIO_CTY gra in menuesPadre)
            {
                if (!uniqueStore.ContainsKey(gra.ID_MENU))
                {
                    uniqueStore.Add(gra.ID_MENU, 0);
                    finalList.Add(gra);
                }
            }
            return finalList;
        }

        public MenuItem mCargarMenu(List<O_MENUS_USUARIO_CTY> menues, O_MENUS_USUARIO_CTY menuPadre)
        {
            var itemPadre = new MenuItem(menuPadre.TITULO) { NavigateUrl = menuPadre.ENLACE };
            List<O_MENUS_USUARIO_CTY> menuesHijo = menues.FindAll(m => m.ID_MENU_PADRE == menuPadre.ID_MENU);
            List<O_MENUS_USUARIO_CTY> menuesHijos = (menuesHijo.GroupBy(t => t.ID_MENU).Select(a => a.First()).Distinct().ToList()); //no duplicar hijos
            foreach (O_MENUS_USUARIO_CTY menuHijo in menuesHijos)
            {
                itemPadre.Selectable = false;
                MenuItem itemHijo = mCargarMenu(menues, menuHijo);
                itemPadre.ChildItems.Add(itemHijo);
            }
            return itemPadre;
        }

        protected void btnSalir_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/Sitio/Persona/wfSalir.aspx");
        }

        protected void LoginStatus1_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            string direccionIp = "la Direccion IP: " + HttpContext.Current.Request.UserHostAddress;
            FormsAuthentication.SignOut();
            Response.Cookies.Remove(CVariablesSesion.IdAutenticacion);
            Session.RemoveAll();
        }
    }
}