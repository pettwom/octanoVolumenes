using System;
using System.Reflection;
using System.Web;
using AnhHydroOctanoPresentacion.Parametros;
using Librerias.Anh.Us;

namespace AnhHydroOctanoPresentacion.Sitio.Movimientos
{
    public partial class WFDescarga : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                #region Proceso a Ejecutar

                if (Session[CVariablesSesion.ArchivoDescarga] != null)
                {
                    dgvTransaccionError.DataSource = Session[CVariablesSesion.ArchivoDescarga];
                    dgvTransaccionError.DataBind();

                    dgeExportar.GridViewID = "dgvTransaccionError";
                    dgeExportar.WriteXlsxToResponse();
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

                //var oMensajeUsuario = new CLogTraza.MensajeUsuario
                //{
                //    decUsuarioId = Session[CVariablesSesion.IdUsuario],
                //    decIdModulo = CParametrosHydro.decIdAplicacion,
                //    strAccion = GetType().Name + "." + MethodBase.GetCurrentMethod().Name,
                //    strIp = HttpContext.Current.Request.UserHostAddress,
                //    decNivelCapa = (int)CLogTraza.CapasNivel.Presentacion
                //};
                //CLogTraza.Error(oMensajeUsuario, exp);

                #endregion
            }
        }
    }
}