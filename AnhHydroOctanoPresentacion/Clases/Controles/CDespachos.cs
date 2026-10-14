
using System.Collections.Generic;

namespace AnhHydroOctanoPresentacion.Clases.Controles
{
    public class CDespachos
    {
        public int IntId { get; set; }
        public string StrFecha { get; set; }
        public string StrTipoDocumento { get; set; }
        public long LonNoAutorizacion { get; set; }
        public long LonNoFactura { get; set; }
        public string StrRazonSocial { get; set; }
        public long LonNit { get; set; }
        public string StrEstado { get; set; }
        public string StrTipoVenta { get; set; }
        public string StrTipoEmision { get; set; }
        public string StrPlaca { get; set; }
        public long LonAutorizacion { get; set; }
        public string StrProducto { get; set; }
        public double DouCantidad { get; set; }
        public double DouPrecio { get; set; }
        public double DouImporte { get; set; }

        public List<CDespachos> cargaDespachos()
        {
            List<CDespachos> LstResultado = new List<CDespachos>();
            LstResultado.Add(new CDespachos() { IntId = 1, StrFecha = "01/02/2016", StrTipoDocumento = "EFE", LonNoAutorizacion = 567896789, LonNoFactura = 22, StrRazonSocial = "CLIENTE 1", LonNit = 676767434, StrEstado = "V", StrTipoVenta = "NAC", StrTipoEmision = "C", StrPlaca = "QQQ111", LonAutorizacion = 0, StrProducto = "GE", DouCantidad = 20, DouPrecio = 3.74, DouImporte = 74.8});
            LstResultado.Add(new CDespachos() { IntId = 2, StrFecha = "01/02/2016", StrTipoDocumento = "EFE", LonNoAutorizacion = 567896789, LonNoFactura = 23, StrRazonSocial = "CLIENTE 2", LonNit = 676767434, StrEstado = "V", StrTipoVenta = "NAC", StrTipoEmision = "C", StrPlaca = "QQQ111", LonAutorizacion = 0, StrProducto = "GE", DouCantidad = 20, DouPrecio = 3.74, DouImporte = 74.8 });
            LstResultado.Add(new CDespachos() { IntId = 3, StrFecha = "01/02/2016", StrTipoDocumento = "EFE", LonNoAutorizacion = 567896789, LonNoFactura = 24, StrRazonSocial = "CLIENTE 3", LonNit = 676767434, StrEstado = "V", StrTipoVenta = "NAC", StrTipoEmision = "C", StrPlaca = "QQQ111", LonAutorizacion = 0, StrProducto = "GE", DouCantidad = 20, DouPrecio = 3.74, DouImporte = 74.8 });
            LstResultado.Add(new CDespachos() { IntId = 4, StrFecha = "01/02/2016", StrTipoDocumento = "EFE", LonNoAutorizacion = 567896789, LonNoFactura = 25, StrRazonSocial = "CLIENTE 4", LonNit = 676767434, StrEstado = "V", StrTipoVenta = "NAC", StrTipoEmision = "C", StrPlaca = "QQQ111", LonAutorizacion = 0, StrProducto = "GE", DouCantidad = 20, DouPrecio = 3.74, DouImporte = 74.8 });
            LstResultado.Add(new CDespachos() { IntId = 5, StrFecha = "01/02/2016", StrTipoDocumento = "EFE", LonNoAutorizacion = 567896789, LonNoFactura = 26, StrRazonSocial = "CLIENTE 5", LonNit = 676767434, StrEstado = "V", StrTipoVenta = "NAC", StrTipoEmision = "C", StrPlaca = "QQQ111", LonAutorizacion = 0, StrProducto = "GE", DouCantidad = 20, DouPrecio = 3.74, DouImporte = 74.8 });
            LstResultado.Add(new CDespachos() { IntId = 6, StrFecha = "01/02/2016", StrTipoDocumento = "EFE", LonNoAutorizacion = 567896789, LonNoFactura = 27, StrRazonSocial = "CLIENTE 6", LonNit = 676767434, StrEstado = "V", StrTipoVenta = "NAC", StrTipoEmision = "C", StrPlaca = "QQQ111", LonAutorizacion = 0, StrProducto = "GE", DouCantidad = 20, DouPrecio = 3.74, DouImporte = 74.8 });
            LstResultado.Add(new CDespachos() { IntId = 7, StrFecha = "01/02/2016", StrTipoDocumento = "EFE", LonNoAutorizacion = 567896789, LonNoFactura = 28, StrRazonSocial = "CLIENTE 7", LonNit = 676767434, StrEstado = "V", StrTipoVenta = "NAC", StrTipoEmision = "C", StrPlaca = "QQQ111", LonAutorizacion = 0, StrProducto = "GE", DouCantidad = 20, DouPrecio = 3.74, DouImporte = 74.8 });
            LstResultado.Add(new CDespachos() { IntId = 8, StrFecha = "01/02/2016", StrTipoDocumento = "EFE", LonNoAutorizacion = 567896789, LonNoFactura = 29, StrRazonSocial = "CLIENTE 8", LonNit = 676767434, StrEstado = "V", StrTipoVenta = "NAC", StrTipoEmision = "C", StrPlaca = "QQQ111", LonAutorizacion = 0, StrProducto = "GE", DouCantidad = 20, DouPrecio = 3.74, DouImporte = 74.8 });
            LstResultado.Add(new CDespachos() { IntId = 9, StrFecha = "01/02/2016", StrTipoDocumento = "EFE", LonNoAutorizacion = 567896789, LonNoFactura = 30, StrRazonSocial = "CLIENTE 9", LonNit = 676767434, StrEstado = "V", StrTipoVenta = "NAC", StrTipoEmision = "C", StrPlaca = "QQQ111", LonAutorizacion = 0, StrProducto = "GE", DouCantidad = 20, DouPrecio = 3.74, DouImporte = 74.8 });

            return LstResultado;
        }


    }
}