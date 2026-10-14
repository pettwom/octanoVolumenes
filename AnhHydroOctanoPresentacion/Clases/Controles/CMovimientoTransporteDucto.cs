using AnhHydroOctanoPresentacion.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnhHydroOctanoPresentacion.Clases.Controles
{
    public class CMovimientoTransporteDucto
    {
        public long LngId { get; set; }
        public string StrFecha { get; set; }
        public string StrDucto { get; set; }
        public string StrTransaccion { get; set; }
        public string StrPoi { get; set; }
        public string StrNombre { get; set; }
        public string StrPoiActual { get; set; }
        public string StrCargador { get; set; }
        public string StrUsuario { get; set; }
        public string StrLocalidad { get; set; }
        public string StrDepartamento { get; set; }
        public string StrProducto { get; set; }
        public string StrVolumen { get; set; }
        public string StrSistema { get; set; }
        public string StrPoderCalorifico { get; set; }
        public string StrTipoTransporte { get; set; }
        public ObjProtoMovimientoTransaccion objProtoMovimiento { get; set; }
    }
}