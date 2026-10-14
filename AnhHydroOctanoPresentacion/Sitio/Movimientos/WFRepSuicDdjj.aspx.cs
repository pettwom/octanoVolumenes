using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AnhHydroOctanoPresentacion.Sitio.Movimientos
{
    using System;
    using System.IO;
    using System.Web.Security;
    using Parametros;
    using Librerias.Anh.Us;
    public partial class WFRepSuicDdjj : System.Web.UI.Page
    {
        protected void page_Init()
        {
            try
            {
                string strMensajeError = null;
                if (CConsultaAccesos.AccesoFormulario(CParametrosHydro.strCredencialHydroAdmin,
                    Convert.ToDecimal(CVariablesSesion.UsuarioSession(Context).ID_USUARIO),
                    Convert.ToDecimal(CParametrosHydro.decIdAplicacion),
                    Path.GetFileName(Request.Path),
                    ref strMensajeError)) return;

                FormsAuthentication.SignOut();
                Response.Cookies.Remove(CVariablesSesion.IdAutenticacion);
                Session.RemoveAll();
                Response.Redirect("~/Sitio/Persona/wfAutenticacion.aspx");
            }
            catch (Exception)
            {
                FormsAuthentication.SignOut();
                Response.Cookies.Remove(CVariablesSesion.IdAutenticacion);
                Session.RemoveAll();
                Response.Redirect("~/Sitio/Persona/wfAutenticacion.aspx");
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}