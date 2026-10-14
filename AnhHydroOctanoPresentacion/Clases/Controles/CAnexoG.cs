using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnhHydroOctanoPresentacion.Clases.Controles
{
    public class CAnexoG
    {
        public string NRO_DUI { get; set; }
        public string MES { get; set; }
        public string EMPRESA_ALMACENAJE { get; set; }
        public string NRO_FACTURA_ALMACENAJE { get; set; }
        public decimal TARIFA_UNITARIA { get; set; }
        public decimal VOLUMEN_RECIBIDO { get; set; }
        public decimal COSTO_ALMACENAJE { get; set; }
        public decimal MONTO_NETO_IVA { get; set; }
    }
}