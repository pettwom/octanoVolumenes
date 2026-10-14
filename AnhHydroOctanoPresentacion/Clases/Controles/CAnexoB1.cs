using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnhHydroOctanoPresentacion.Clases.Controles
{
    public class CAnexoB1
    {
        public string NRO_DUI { get; set; }
        public string MES { get; set; }
        public string NRO_FACTURA_TRANSPORTE { get; set; }
        public decimal VOLUMEN_DESPACHADO { get; set; }
        public decimal VOLUMEN_RECIBIDO { get; set; }
        public decimal DIFERENCIA { get; set; }
        public decimal MERMA_PERMISIBLE { get; set; }
        public decimal MERMA_EXCEDENTE { get; set; }
        public decimal FLETE { get; set; }
        public decimal COSTO_PRODUCTO { get; set; }
        public decimal TRANSPORTE_TOTAL { get; set; }
        public decimal DESCUENTO_MERMA { get; set; }
        public decimal COSTO_TRANSPORTE { get; set; }
    }
}