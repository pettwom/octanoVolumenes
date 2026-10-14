using System;

namespace AnhHydroOctanoPresentacion.Clases.Controles
{
    public class CRegistroDocumento
    {
        public DateTime datFecha { get; set; }
        public int IntIdTipoDoc { get; set; }
        public string StrTipoDoc { get; set; }
        public int IntIdEmisor { get; set; }
        public string StrEmisor { get; set; }
        public long lngNoAutorizacion { get; set; }
        public long LngNoFactura { get; set; }
        public string StrCodCtrl { get; set; }
        public long LngNoReferencia { get; set; }
        public decimal DecCantidad { get; set; }
        public decimal DecPrecio { get; set; }
        public decimal DecImporte { get; set; }
        public long LngIdDocumento { get; set; }
        public int IntIdCreditoFis { get; set; }
        public string StrObservacion { get; set; }
        public int IntIdEstado { get; set; }

        public int IntIdFormaPago { get; set; }
        public int IntIdMoneda { get; set; }
        public string StrUsuario { get; set; }
    }
}