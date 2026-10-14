using AnhHydroOctanoAgenteServicio;
using AnhHydroOctanoAgenteServicio.ServicioHydroSesion;
using AnhHydroOctanoPresentacion.Parametros;
using Librerias.Anh.Us;
using Llaves.Anh.Dtic;
using System;
using System.Collections.Generic;
using System.Web;

namespace AnhHydroOctanoPresentacion.Clases.Persona
{
    public class CPersonaSesion
    {
        private readonly string _mapPath;
        private readonly string _strIp;
        private readonly IServicioHydroSesion _servicio;
        //private readonly CLogs _logs;
        private string _mensajeError;

        private CLogAccesos.EBitacoraLoggin _logSesion;
        public CPersonaSesion(string mapPath, string strIp)
        {
            _mapPath = mapPath;
            _strIp = strIp;
            _servicio = LocalizadorProxy.ServicioAutenticarUsuario();
            //_logs = new CLogs(mapPath, strIp);
            _logSesion = new CLogAccesos.EBitacoraLoggin()
            {
                decModuloId = CParametrosHydro.decIdAplicacion,
                strIp = HttpContext.Current.Request.UserHostAddress
            };

        }

        public string MapPath
        {
            get { return _mapPath; }
        }

        public string StrIp
        {
            get { return _strIp; }
        }

        public O_USUARIO_CTY Autenticar(string strUsuario, string strClave)
        {
            try
            {
                //O_USUARIO_CTY resultado = _servicio.Autenticar(CParametrosHydro.StrCredencialEmpadronamiento, strUsuario,strClave, ref _mensajeError);
                O_USUARIO_CTY resultado = _servicio.AutenticarV2(CParametrosHydro.StrCredencialEmpadronamiento, strUsuario, strClave, 1, CParametrosHydro.decIdAplicacion, ref _mensajeError); ;
                _logSesion.strUsuario = strUsuario;
                _logSesion.strObservaciones = "[" + System.Reflection.MethodBase.GetCurrentMethod().Name + "] " + _mensajeError;

                if (resultado != null && resultado.ID_USUARIO > 0)
                {
                    _logSesion.decAppIdUsuario = resultado.ID_USUARIO;
                    _logSesion.strNombreUsuario = resultado.NOMBRE_COMPLETO;
                    CLogAccesos.RegistrarAccesoExitoso(_logSesion);
                    return resultado;
                }
                else
                {
                    CLogAccesos.RegistrarAccesoFallido(_logSesion);
                    return null;
                }
            }
            catch (Exception exp)
            {
                string strUbicacionMetodo = this.GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                CLogTraza.Error(strUbicacionMetodo, CParametrosHydro.decIdAplicacion, Convert.ToDecimal(HttpContext.Current.Session[CVariablesSesion.IdUsuario]), HttpContext.Current.Request.UserHostAddress, exp.Message, Convert.ToString(exp.InnerException), exp.StackTrace);
                return null;
            }
        }

        public O_USUARIO_CTY AutenticarActDir(string strUsuario, string strClave)
        {
            try
            {
                var llaveSesion = new CSesion();
                string strLlaveAutenticacion = llaveSesion.GenerarLlaveAutenticacion(strUsuario, strClave);

                O_USUARIO_CTY usuario = _servicio.AutenticarActDir(CParametrosHydro.StrCredencialEmpadronamiento, strLlaveAutenticacion, ref _mensajeError);
                _logSesion.strUsuario = strUsuario;
                _logSesion.strObservaciones = _mensajeError;
                if (usuario != null)
                {
                    _logSesion.decAppIdUsuario = usuario.ID_USUARIO;
                    _logSesion.strNombreUsuario = usuario.NOMBRE_COMPLETO;
                    CLogAccesos.RegistrarAccesoExitoso(_logSesion);
                    return usuario;
                }
                else
                {
                    CLogAccesos.RegistrarAccesoFallido(_logSesion);
                    return null;
                }

            }
            catch (Exception exp)
            {
                string strUbicacionMetodo = this.GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                CLogTraza.Error(strUbicacionMetodo, CParametrosHydro.decIdAplicacion, Convert.ToDecimal(HttpContext.Current.Session[CVariablesSesion.IdUsuario]), HttpContext.Current.Request.UserHostAddress, exp.Message, Convert.ToString(exp.InnerException), exp.StackTrace);
                return null;

            }
        }

        public decimal ExisteUsuario(string strUsuario)
        {
            try
            {
                O_RESULTADO_CTY resultado = _servicio.ExisteUsuario(CParametrosHydro.StrCredencialEmpadronamiento,
                    strUsuario, ref _mensajeError);
                if (resultado != null)
                {
                    //return resultado.ID_RESULTADO > 0;
                    return resultado.ID_RESULTADO;
                }
                return 0;
            }
            catch (Exception exp)
            {
                _mensajeError += "[CPersonaSesion.ExisteUsuario] " + exp.Message + " - " + exp.InnerException + " - " + exp.StackTrace;
                return 0;
            }
        }

        public bool CambiarClave(decimal decIdUsuario, string strClaveNueva)
        {
            try
            {
                O_RESULTADO_CTY resultado = _servicio.CambiarClave(2, decIdUsuario, "", strClaveNueva,
                    CParametrosHydro.StrCredencialEmpadronamiento, ref _mensajeError);
                if (resultado != null)
                {
                    return resultado.ID_RESULTADO > 0;
                }
                return false;
            }
            catch (Exception exp)
            {
                _mensajeError += "[CPersonaSesion.CambiarClave] " + exp.Message + " - " + exp.InnerException + " - " + exp.StackTrace;
                return false;
            }
        }

        public bool CambiarClave(decimal decIdUsuario, string strClaveAntigua, string strClaveNueva)
        {
            try
            {
                O_RESULTADO_CTY resultado = _servicio.CambiarClave(1, decIdUsuario, strClaveAntigua, strClaveNueva,
                    CParametrosHydro.StrCredencialEmpadronamiento, ref _mensajeError);
                if (resultado != null)
                {
                    return resultado.ID_RESULTADO > 0;
                }
                return false;
            }
            catch (Exception exp)
            {
                _mensajeError += "[CPersonaSesion.CambiarClave] " + exp.Message + " - " + exp.InnerException + " - " + exp.StackTrace;
                return false;
            }
        }

        public decimal RegistrarUsuario(string usuario)
        {
            try
            {
                O_RESULTADO_CTY resultado = _servicio.RegistrarUsuario(CParametrosHydro.StrCredencialEmpadronamiento,
                    usuario, ref _mensajeError);
                if (resultado != null)
                {
                    return resultado.ID_RESULTADO;
                }
                return 0;
            }
            catch (Exception exp)
            {
                _mensajeError += "[CPersonaSesion.RegistrarUsuario] " + exp.Message + " - " + exp.InnerException + " - " + exp.StackTrace;
                return 0;
            }
        }

        public bool ActivarUsuario(decimal decIdUsuario)
        {
            try
            {
                O_RESULTADO_CTY resultado = _servicio.ActivarUsuario(CParametrosHydro.StrCredencialEmpadronamiento,
                    decIdUsuario, ref _mensajeError);
                if (resultado != null)
                {
                    return resultado.ID_RESULTADO > 0;
                }
                return false;
            }
            catch (Exception exp)
            {
                _mensajeError += "[CPersonaSesion.ActivarUsuario] " + exp.Message + " - " + exp.InnerException + " - " + exp.StackTrace;
                return false;
            }
        }

        public decimal ObtenerUsuario(string strUsuario)
        {
            try
            {
                O_RESULTADO_CTY resultado = _servicio.ExisteUsuario(CParametrosHydro.StrCredencialEmpadronamiento,
                    strUsuario, ref _mensajeError);
                if (resultado != null)
                {
                    return resultado.ID_RESULTADO;
                }
                return -1;
            }
            catch (Exception exp)
            {
                _mensajeError += "[CPersonaSesion.ObtenerUsuario] " + exp.Message + " - " + exp.InnerException + " - " + exp.StackTrace;
                return -1;
            }
        }

        public O_DETALLE_USUARIO_CTY ObtenerUsuario(decimal decIdUsuario)
        {
            try
            {
                O_DETALLE_USUARIO_CTY resultado = _servicio.ObtenerUsuario(
                    CParametrosHydro.StrCredencialEmpadronamiento, decIdUsuario, ref _mensajeError);
                if (resultado != null)
                {
                    return resultado;
                }
                return null;
            }
            catch (Exception exp)
            {
                _mensajeError += "[CPersonaSesion.ObtenerUsuario] " + exp.Message + " - " + exp.InnerException + " - " + exp.StackTrace;
                return null;
            }
        }

        public List<O_MODULOS_USUARIO_CTY> ObtenerModulos(decimal decIdUsuario)
        {
            try
            {
                List<O_MODULOS_USUARIO_CTY> resultado = _servicio.ObtenerModulosUsuario(decIdUsuario,
                    CParametrosHydro.StrCredencialEmpadronamiento, ref _mensajeError);
                if (resultado != null)
                {
                    return resultado;
                }
                return null;
            }
            catch (Exception exp)
            {
                _mensajeError += "[CPersonaSesion.ObtenerModulos] " + exp.Message + " - " + exp.InnerException + " - " + exp.StackTrace;
                return null;
            }
        }

        public List<O_MENUS_USUARIO_CTY> ObtenerMenus(decimal decIdModulo, decimal decIdUsuario)
        {
            try
            {
                List<O_MENUS_USUARIO_CTY> resultado = _servicio.ObtenerMenusUsuario(decIdUsuario, decIdModulo, 0,
                    CParametrosHydro.StrCredencialEmpadronamiento, ref _mensajeError);
                if (resultado != null)
                {
                    return resultado;
                }
                return null;
            }
            catch (Exception exp)
            {
                _mensajeError += "[CPersonaSesion.ObtenerMenus] " + exp.Message + " - " + exp.InnerException + " - " + exp.StackTrace;
                return null;
            }
        }

        public List<O_MENUS_USUARIO_CTY> ObtenerMenusPerfil(decimal decIdPerfil)
        {
            try
            {
                List<O_MENUS_USUARIO_CTY> resultado = _servicio.ObtenerMenusPerfil(decIdPerfil,
                    CParametrosHydro.StrCredencialEmpadronamiento, ref _mensajeError);
                if (resultado != null && resultado.Count > 0)
                {
                    return resultado;
                }
                return null;
            }
            catch (Exception exp)
            {
                _mensajeError += "[CPersonaSesion.ObtenerMenusPerfil] " + exp.Message + " - " + exp.InnerException + " - " + exp.StackTrace;
                return null;
            }
        }

        public List<O_PERFILES_USUARIO_CTY> ObtenerPerfiles(decimal decIdUsuario, decimal decIdModulo)
        {
            try
            {
                List<O_PERFILES_USUARIO_CTY> resultado = _servicio.ObternerPerfilesUsuario(decIdUsuario, decIdModulo,
                    CParametrosHydro.StrCredencialEmpadronamiento, ref _mensajeError);
                if (resultado != null)
                {
                    return resultado;
                }
                return null;
            }
            catch (Exception exp)
            {
                _mensajeError += "[CPersonaSesion.ObtenerPerfiles] " + exp.Message + " - " + exp.InnerException + " - " + exp.StackTrace;
                return null;
            }
        }

        [Serializable]
        public class Usuario
        {
            #region Variables

            public int idPersona { get; set; }
            public string numeroIdentificacion { get; set; }
            public int expedicion { get; set; }
            public string complemento { get; set; }
            public string nombre { get; set; }
            public string primerApellido { get; set; }
            public string segundoApellido { get; set; }
            public long fechaNacimiento { get; set; }
            public int genero { get; set; }
            public string email { get; set; }
            public string clave { get; set; }
            public int idPerfil { get; set; }
            public int tipoIdentificacion { get; set; }
            public int idUsuario { get; set; }
            #endregion
        }
    }
}