using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnhHydroOctanoPresentacion.Clases.Controles
{
    public class CMonitoreoProde
    {
        public decimal DecIdConsumidor { get; set; }
        public string StrEstacionServicio { get; set; }
        public string StrCodHydroProyecto { get; set; }
        public decimal DecIdHidrocarburo { get; set; }
        public decimal DecIdDepartamento { get; set; }
        public string StrDepartamento { get; set; }
        public string StrSemana { get; set; }
        public decimal DecRecepcion { get; set; }
        public decimal DecDemasia { get; set; }
        public decimal DecTotalIngreso { get; set; }
        public decimal DecVenta { get; set; }
        public decimal DecMerma { get; set; }
        public decimal DecTotalEgreso { get; set; }
        public decimal DecProde { get; set; }
    }
}