using System;
using System.Web;
using Librerias.Anh.Us;

namespace AnhHydroOctanoPresentacion.Parametros
{
    public class CLog
    {
        public static void Error(HttpContext contexto, string metodo, Exception ex)
        {
            CLogTraza.MensajeUsuario oMensajeUsuario = new CLogTraza.MensajeUsuario();
            //oMensajeUsuario.decUsuarioId = CVariablesSesion.UsuarioSession(contexto).ID_USUARIO;// contexto.Session[CVariablesSesion.IdUsuario];
            var ttt = (CVariablesSesion.UsuarioSession(contexto) == null) ? 0 : CVariablesSesion.UsuarioSession(contexto).ID_USUARIO;
            //oMensajeUsuario.decUsuarioId = CVariablesSesion.UsuarioSession(contexto).ID_USUARIO;// contexto.Session[CVariablesSesion.IdUsuario];
            oMensajeUsuario.decUsuarioId = ttt;// contexto.Session[CVariablesSesion.IdUsuario];
            oMensajeUsuario.decIdModulo = CParametrosHydro.decIdAplicacion;
            oMensajeUsuario.strAccion = metodo;
            oMensajeUsuario.strIp = HttpContext.Current.Request.UserHostAddress;

            CLogTraza.Error(oMensajeUsuario, ex);
        }
        public static void Depuracion(HttpContext contexto, string mensaje, string metodo, string detalleAccion)
        {
            decimal idModulo = CParametrosHydro.decIdAplicacion;
            decimal idUsuario = CVariablesSesion.UsuarioSession(contexto).ID_USUARIO;
            string strIp = HttpContext.Current.Request.UserHostAddress;
            CLogTraza.Depuracion(mensaje, metodo, idModulo, idUsuario, strIp, detalleAccion);
        }
        public static void Alerta(HttpContext contexto, string mensaje, string metodo, string ex)
        {
            decimal idModulo = CParametrosHydro.decIdAplicacion;
            decimal idUsuario = CVariablesSesion.UsuarioSession(contexto).ID_USUARIO;
            string strIp = HttpContext.Current.Request.UserHostAddress;
            CLogTraza.Advertencia(mensaje, metodo, idModulo, idUsuario, strIp, ex, "0");
        }
        public static void Informacion(HttpContext contexto, string mensaje)
        {
            decimal idModulo = CParametrosHydro.decIdAplicacion;
            decimal idUsuario = CVariablesSesion.UsuarioSession(contexto).ID_USUARIO;
            string strIp = HttpContext.Current.Request.UserHostAddress;
            CLogTraza.Informacion(mensaje, idModulo, idUsuario, strIp);
        }
    }
}