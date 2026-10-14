using System.Collections.Generic;
using AnhHydroOctanoPresentacion.Entidades;

namespace AnhHydroOctanoPresentacion.Clases.Controles
{
    public class CMovimientoRefineria
    {
        public long LngId { get; set; }
        public decimal DecFecha { get; set; }
        public decimal DecIdOrigen { get; set; }
        public string StrTInfraestructura { get; set; }
        public string StrInfraestructura { get; set; }
        public string StrProyOrigen { get; set; }
        public string StrPais { get; set; }
        public string StrIdentificador { get; set; }
        public string StrLote { get; set; }
        public string StrDui { get; set; }
        public string StrNroRecinto { get; set; }
        public string StrCodigoTanqueCisterna { get; set; }
        public decimal DecNroFactura { get; set; }
        public decimal DecNroAutorizacion { get; set; }
        public string StrCodigoControl { get; set; }
        public decimal DecNit { get; set; }
        public string StrNroReferencia { get; set; }
        public string StrCertificadoRecepcion { get; set; }
        public string StrCIUsuario { get; set; }
        public string StrEstado { get; set; }
        public string StrProducto { get; set; }
        public string StrRazonSocial { get; set; }
        public decimal DecCantidad { get; set; }
        public decimal DecPrecio { get; set; }
        public decimal DecImporte { get; set; }
        public ObjProtoMovimientoTransaccion objProtoMovimiento { get; set; }

    }
    public class ObjCargaTransaccion
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
        public string StrNroReferencia { get; set; }
        public long LngNroAutVenta { get; set; }
        public string StrProducto { get; set; }
        public decimal DecVolumen { get; set; }
        public decimal DecPrecio { get; set; }
        public decimal DecImporte { get; set; }
        public decimal DecIdTipoCreditoFiscal { get; set; }
        public decimal DecIdTipoOp { get; set; }
        public decimal DecOrden { get; set; }
        public ObjProtoMovimientoTransaccion objProtoMovimiento { get; set; }
        
        //---
        public decimal DecFecha { get; set; }
        public decimal DecIdOrigen { get; set; }
        public string StrTInfraestructura { get; set; }
        public string StrInfraestructura { get; set; }
        public string StrProyOrigen { get; set; }
        public string StrPais { get; set; }
        //public string StrIdentificador { get; set; }
        public string StrLote { get; set; }
        public string StrDui { get; set; }
        public string StrNroRecinto { get; set; }
        public string StrCodigoTanqueCisterna { get; set; }
        public decimal DecNroFactura { get; set; }
        public decimal DecNroAutorizacion { get; set; }
        public string StrCodigoControl { get; set; }
        public decimal DecNit { get; set; }
        //public string StrNroReferencia { get; set; }
        public string StrCertificadoRecepcion { get; set; }
        public string StrCIUsuario { get; set; }
        //public string StrEstado { get; set; }
        //public string StrProducto { get; set; }
        //public string StrRazonSocial { get; set; }
        public decimal DecCantidad { get; set; }
        //public decimal DecPrecio { get; set; }
        //public decimal DecImporte { get; set; }

    }
}