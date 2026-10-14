using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AnhHydroSisgenPresentacion.Sitio.Persona
{
    using System.Web.Security;

    using AnhHydroSisgenPresentacion.Parametros;

    public partial class wfSalir : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Cookies.Remove(CVariablesSesion.IdAutenticacion);
            HttpContext.Current.Session.Abandon();
            Session[CVariablesSesion.ObjUsuario] = null;
            Session.RemoveAll();

            Response.Redirect("~/Sitio/Persona/wfAutenticacion.aspx", false);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
    }
}