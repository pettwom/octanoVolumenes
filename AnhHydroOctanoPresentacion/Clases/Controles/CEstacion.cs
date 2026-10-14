namespace AnhHydroOctanoPresentacion.Clases.Controles
{
    public class CEstacion
    {
        public int IdEess { get; set; }
        public long NitEstacion { get; set; }
        public string Empresa { get; set; }
        public string Denominacion { get; set; }
        public string Direccion { get; set; }
        public string Departamento { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set; }
        public decimal TotalVenta { get; set; }
        public string UltimaVenta { get; set; }
        public decimal TotalVentaTag { get; set; }
        public string UltimaVentaTag { get; set; }
        public decimal TotalLecturaRfid { get; set; }
        public string UltimaLecturaRfid { get; set; }
        public decimal ActividadMdw { get; set; }
        public string UltimaActividadMdw { get; set; }
        public string VersionMdw { get; set; }

    }
}