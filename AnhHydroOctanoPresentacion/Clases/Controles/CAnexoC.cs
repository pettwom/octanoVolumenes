using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnhHydroOctanoPresentacion.Clases.Controles
{
    public class CAnexoC
    {
        public string NRO_DUI { get; set; }
        public string EMPRESA_ASEGURADORA { get; set; }
        public string PERIODO_COBERTURA { get; set; }
        public string NRO_FACTURA_ASEGURADORA { get; set; }
        public decimal MONTO_FACTURADO { get; set; }
        public decimal MONTO_NETO_IVA { get; set; }
    }
}