using AnhHydroOctanoPresentacion.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnhHydroOctanoPresentacion.Clases.Controles
{
    public class CMovimientoRedesGas
    {
        public long LngId { get; set; }
        public string StrFecha { get; set; }
        public decimal DecLecMedidor { get; set; }
        public string StrProducto { get; set; }
        public decimal DecCantLectura { get; set; }
        public decimal DecCantFactura { get; set; }
        public decimal DecFactorCorrec { get; set; }
        public decimal DecTemperatura { get; set; }
        public decimal DecPresionPsig { get; set; }
        public string StrEstado { get; set; }
        public string StrCategoria { get; set; }
        public string StrSectorEcon { get; set; }
        public string StrCodUbicacion { get; set; }
        public string StrCodUsuario { get; set; }
        //public decimal DecCodMedidor { get; set; }
        public string DecCodMedidor { get; set; }
        public string StrFormaPago { get; set; }
        public decimal DecNit { get; set; }
        public string StrRazonSocial { get; set; }
        public string StrDireccion { get; set; }
        public decimal DecNroFactura { get; set; }
        public decimal DecNroAutorizacion { get; set; }
        public string codigoAutorizacion { get; set; }
        public string StrCodigoControl { get; set; }
        public string StrFechaFactura { get; set; }
        public decimal DecPrecio { get; set; }
        public decimal DecCantNoCorregido { get; set; }
        public decimal DecNroComprobante { get; set; }
        public string StrResponsableMedicion { get; set; }
        public string StrCiResponMed { get; set; }
        public decimal DecNroReferencia { get; set; }
        public string StrTipoRefAdicional { get; set; }
        public decimal DecImportAdicional { get; set; }
        public decimal DecImportConsumo { get; set; }
        public decimal DecImportCargo { get; set; }
        public decimal DecImportDescuento { get; set; }
        public string StrCiudadPoblacion { get; set; }
        public string StrTipoMedidor { get; set; }
        public decimal DecImporte { get; set; }
        public ObjProtoMovimientoTransaccion objProtoMovimiento { get; set; }
    }
}