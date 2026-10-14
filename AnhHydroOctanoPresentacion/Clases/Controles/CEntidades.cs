using System.Collections.Generic;

namespace AnhHydroOctanoPresentacion.Clases.Controles
{
    public class CEntidades
    {
        public CEntidades(int intIdEntidad, string strDenominacion, int intNit, string strRepLegal, List<CProyectos> lstOperadores)
        {
            IntIdEntidad = intIdEntidad;
            StrDenominacion = strDenominacion;
            IntNit = intNit;
            StrRepLegal = strRepLegal;
            LstOperadores = lstOperadores;
        }

        public int IntIdEntidad { get; set; }
        public string StrDenominacion { get; set; }
        public int IntNit { get; set; }
        public string StrRepLegal { get; set; }
        public List<CProyectos> LstOperadores { get; set; }
    }

    public class CProyectos
    {
        public CProyectos(int intIdDepto, string strDepto, int intIdProyecto, string strProyecto, string strDireccion, string strLatitud, string strLongitud, List<CActividad> listaActividades)
        {
            IntIdDepto = intIdDepto;
            StrDepto = strDepto;
            IntIdProyecto = intIdProyecto;
            StrProyecto = strProyecto;
            StrDireccion = strDireccion;
            StrLatitud = strLatitud;
            StrLongitud = strLongitud;
            ListaActividades = listaActividades;
        }

        public int IntIdDepto { get; set; }
        public string StrDepto { get; set; }
        public int IntIdProyecto { get; set; }
        public string StrProyecto { get; set; }
        public string StrDireccion { get; set; }
        public string StrLatitud { get; set; }
        public string StrLongitud { get; set; }
        public List<CActividad> ListaActividades { get; set; }
    }

    public class CActividad
    {
        public CActividad(int intIdActividad, string strActividad, int intLicencia)
        {
            IntIdActividad = intIdActividad;
            StrActividad = strActividad;
            IntLicencia = intLicencia;
        }
        public int IntIdActividad { get; set; }
        public string StrActividad { get; set; }
        public int IntLicencia { get; set; }
    }

    public class CListaEntidades
    {
        public int intId { get; set; }
        public int intIdPadre { get; set; }
        public decimal decIdActividad { get; set; }
        public string strActividad { get; set; }
        public decimal decIdConsumidor { get; set; }
        public string strConsumidor { get; set; }
    }

}