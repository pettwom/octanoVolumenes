using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnhHydroOctanoPresentacion.Clases.Controles
{
    public class CAnexoI
    {
        public string NRO_DUI { get; set; }
        public string PROVEEDOR { get; set; }
        public string NRO_FACTURA_PROVEEDOR { get; set; }
        public string MES_RECEPCION { get; set; }
        public string PLANTA { get; set; }
        public decimal VOLUMEN { get; set; }
    }
}