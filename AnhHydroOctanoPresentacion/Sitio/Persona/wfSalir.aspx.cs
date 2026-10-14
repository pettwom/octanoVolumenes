using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using AnhHydroOctanoPresentacion.Parametros;

namespace AnhHydroOctanoPresentacion.Sitio.Persona
{
    public partial class wfSalir : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Cookies.Remove(CVariablesSesion.IdAutenticacion);
            HttpContext.Current.Session.Abandon();
            Session[CVariablesSesion.ObjUsuario] = null;
            Session.Remove(CVariablesSesion.ObjetoArbol);
            Session.Remove(CVariablesSesion.ObjetoEntidad);

            Session.RemoveAll();
            Response.Redirect("~/Sitio/Persona/wfAutenticacion.aspx");
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
    }
}