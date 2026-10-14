using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AnhHydroOctanoPersistencia.Entidades;
using ServiceStack.ServiceClient.Web;
using Librerias.Anh.Us;
using System.Reflection;
using AnhHydroOctanoPresentacion.Parametros;

namespace AnhHydroOctanoPresentacion.Clases.Util
{
    public class CTrazaInf
    {
        public void MRegistraTrazaInf(string strAccion, string strTipoAccion, decimal decIdModulo, decimal decIdUsuario, string strIp, string strSalida)
        {            
            try
            {
                var clienteJsonSeg = new JsonServiceClient(System.Web.Configuration.WebConfigurationManager.AppSettings["ServicioJsonSeguridad"]);
                var lstResultado = clienteJsonSeg.Post<oResultadoClogTraza>("/EInsertaLogTraza/",
                                                               new
                                                               {
                                                                   dtFechaRegistro = DateTime.Now,
                                                                   strTipoLog = "INFO",
                                                                   strAccion = strTipoAccion,
                                                                   strDetalleAccion = "",
                                                                   decModuloId = decIdModulo,
                                                                   strExcepcionInterna = "",
                                                                   strMensajeExcepcion = "",
                                                                   strSeguimientoPila = "",
                                                                   strIp = strIp,
                                                                   strmensaje = "RESULTADO: " + GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + " ENTRADA: " + strAccion + " SALIDA REG.: " + strSalida,
                                                                   strNivelCapa = CLogTraza.CapasNivel.Presentacion.ToString(),
                                                                   decUsuarioId = decIdUsuario
                                                               });
            }
            catch (Exception ex)
            {
                #region RegistraError
                var oMensajeUsuario = new CLogTraza.MensajeUsuario
                {
                    decUsuarioId = decIdUsuario,
                    decIdModulo = CParametrosHydro.decIdAplicacion,
                    strAccion = GetType().Name + "." + MethodBase.GetCurrentMethod().Name,
                    strIp = HttpContext.Current.Request.UserHostAddress,
                    decNivelCapa = Convert.ToInt16(CLogTraza.CapasNivel.Presentacion)
                };

                CLogTraza.Error(oMensajeUsuario, ex);
                #endregion
            }
        }
        private class oResultadoClogTraza
        {
            public int decCodigo { get; set; }
            public string strMensaje { get; set; }
            public object oResultado { get; set; }
        }
    }
}