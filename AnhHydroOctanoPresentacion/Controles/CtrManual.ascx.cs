
namespace AnhHydroOctanoPresentacion.Controles
{
    using System;
    using System.Web;
    using System.Web.UI;
    using Parametros;
    using UserControl = System.Web.UI.UserControl;

    public partial class CtrManual : UserControl
    {
        #region Variables de entorno

        #endregion

        #region Eventos de Formulario

        protected void Page_Load(object sender, EventArgs e)
        {
            MValidaSesion();
        }

        #endregion

        #region Metodos y funciones

        /// <summary>
        /// METODO. MValidaSesion
        /// </summary>
        private void MValidaSesion()
        {
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
            }
        }
        
        #endregion

    }
}