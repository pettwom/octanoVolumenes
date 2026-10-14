using System;

namespace AnhHydroOctanoPresentacion.Clases.Controles
{
    public class CRegistroIntimacion
    {
        public string strPeriodo { get; set; }
        public string strDescripcion { get; set; }
        public decimal decNotificacion { get; set; }
        public string strCodBarra { get; set; }
        public string strNotificante { get; set; }
        public string strNotificado { get; set; }
        public decimal decFechaInicio { get; set; }
        public decimal decFechaFin { get; set; }
        public decimal decDiasPlazo { get; set; }
        public string strObservacion { get; set; }
    }
}