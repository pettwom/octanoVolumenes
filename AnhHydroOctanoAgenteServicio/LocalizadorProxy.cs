using AnhHydroOctanoAgenteServicio.ServicioHydroConsultas;
using AnhHydroOctanoAgenteServicio.ServicioHydroListados;
using AnhHydroOctanoAgenteServicio.ServicioHydroSesion;
using AnhHydroOctanoAgenteServicio.ServicioParametricas;

namespace AnhHydroOctanoAgenteServicio
{
    public class LocalizadorProxy
    {
        /// <summary>
        /// Unidad de Programa: ServicioAutenticarUsuario
        /// Fecha Creación: 20/08/2013
        /// Áutor: Guido Cutipa Yujra
        /// Descripción: Listados de calidad
        /// </summary>
        /// <returns>Objetos complejos para autenticar usuarios</returns>
        public static IServicioHydroSesion ServicioAutenticarUsuario()
        {

            //IServicioHydroSesion objServicio = new ServicioHydroSesionClient("ServicioSesionEndPoint");
            IServicioHydroSesion objServicio = new ServicioHydroSesionClient("HydroCoreSesionEndPoint");
            return objServicio;
        }

        public static IServicioHydroConsultas ServicioConsultarHydro()
        {
            IServicioHydroConsultas objServicio = new ServicioHydroConsultasClient("HydroCoreConsultasEndPoint");
            return objServicio;
        }

        public static IServicioHydroListados ServicioHydroListados()
        {
            IServicioHydroListados objServicio = new ServicioHydroListadosClient("HydroCoreListadosEndPoint");
            return objServicio;
        }
        public static IServicioParametricas ServicioParametricas()
        {
            IServicioParametricas objServicio = new ServicioParametricasClient("HydroParametricasEndPoint");
            return objServicio;
        }
    }

}
