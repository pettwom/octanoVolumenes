using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnhHydroOctanoPresentacion.Clases.Controles
{
    public class CAnexoB2
    {
        public string NRO_DUI { get; set; }
        public string NRO_FACTURA_PROVEEDOR { get; set; }
        public string EMPRESA_TRANSPORTE { get; set; }
        public string NRO_FACTURA_TRANSPORTE { get; set; }
        public decimal PLANTA_DESPACHO { get; set; }
        public decimal PLANTA_RECEPCION { get; set; }
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