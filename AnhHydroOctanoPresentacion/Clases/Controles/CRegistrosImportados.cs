using AnhHydroOctanoPresentacion.Entidades;

namespace AnhHydroOctanoPresentacion.Clases.Controles
{
    public class CRegistrosImportados
    {
        public long LngId { get; set; }
        public string StrIdVenta { get; set; }
        public string StrFechaEmision { get; set; }
        public string StrTipoMov { get; set; }
        public string StrEstado { get; set; }
        
        public long LngNit { get; set; }
        public string StrRazonSocial { get; set; }
        public string StrIdentificador { get; set; }

        public long LngNroFactura { get; set; }
        public long LngNroAutorizacion { get; set; }
        public string strCodigoAutorizacion { get; set; }   //adicionado en fecha 09-02-2022 
        public string StrNroReferencia { get; set; }
        public long LngNroAutVenta { get; set; }

        public string StrProducto { get; set; }
        public decimal DecVolumen { get; set; }
        public decimal DecPrecio { get; set; }
        public decimal DecImporte { get; set; }

        public decimal DecIdTipoCreditoFiscal { get; set; }

        public ObjProtoMovimientoTransaccion objProtoMovimiento { get; set; }

        public decimal DecIdTipoOp { get; set; }
        public decimal DecOrden { get; set; }
        
    }
}