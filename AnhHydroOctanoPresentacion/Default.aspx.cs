
namespace AnhHydroOctanoPresentacion
{
    using System;
    using System.Web.Script.Services;
    using System.Web.Services;
    using System.Web.UI;
    using Entidades;
    using Parametros;
    using ServiceStack.ServiceClient.Web;

    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Redirect("~/Sitio/Persona/wfAutenticacion.aspx");
        }
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string CargarDatosSegip(string strCi)
        {
            var resultado = "0";
            var _clientSegip = new JsonServiceClient(CAppSettings.servicio_sireHidro);
            var resultadom =
                _clientSegip.Get<RespConsultaFoto>("/consultasegipFOTO/EBF8FDEFA7ED24CF8C47EE0B149FD4F5/" + strCi +"?format=json");
            var resultadoPersona = resultadom.RespuestaConsulta;
            if (resultadoPersona != null)
            {
                resultado = Newtonsoft.Json.JsonConvert.SerializeObject(resultadoPersona[0]);
            }
            return resultado;
        }
    }
}