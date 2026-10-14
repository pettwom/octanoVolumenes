using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnhHydroOctanoPresentacion.Clases.Controles
{
    public class CRegistrosImportadores
    {
        public long LngId { get; set; }
        public decimal DecSecuencia { get; set; }
        public string StrFechaDui { get; set; }
        public string StrDui { get; set; }
        public string StrNroFactProveedor { get; set; }
        public string StrProveedor { get; set; }
        public string StrEmpresa { get; set; }        
        public string StrFactTransporte { get; set; }
        public string StrCostoUnitario { get; set; }
        public string StrFleteTransporte { get; set; }
        public string StrPlantaDespacho { get; set; }
        public string StrPlantaRecepcion { get; set; }
        public decimal DecVolumenDespachado { get; set; }
        public decimal DecVolumenRecibido { get; set; }
        public string StrObservacion { get; set; }
    }
}