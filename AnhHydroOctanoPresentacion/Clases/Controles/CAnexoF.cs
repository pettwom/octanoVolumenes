using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnhHydroOctanoPresentacion.Clases.Controles
{
    public class CAnexoF
    {
        public string NRO_DUI { get; set; }
        public string PROVEEDOR { get; set; }
        public string NRO_FACTURA_PROVEEDOR { get; set; }
        public decimal MONTO_FACTURA_PROVEEDOR { get; set; }
        public decimal COSTO_FINANCIERO { get; set; }
        public decimal COSTO_FINANCIERO_NETO { get; set; }
    }
}