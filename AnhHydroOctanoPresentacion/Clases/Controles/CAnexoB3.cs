using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnhHydroOctanoPresentacion.Clases.Controles
{
    public class CAnexoB3
    {
        public string NRO_DUI { get; set; }
        public string NRO_FACTURA_PROVEEDOR { get; set; }
        public string PROVEEDOR { get; set; }
        public string EMPRESA { get; set; }
        public string NRO_FACTURA { get; set; }
        public decimal VOLUMEN_DESTINO1 { get; set; }
        public decimal VOLUMEN_DESTINO2 { get; set; }
        public decimal FLETE { get; set; }
        public decimal COSTO_FACTURADO { get; set; }
        public decimal MONTO_NETO_IVA { get; set; }
    }
}