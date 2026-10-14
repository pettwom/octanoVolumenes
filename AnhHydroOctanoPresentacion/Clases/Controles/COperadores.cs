using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AnhHydroOctanoPresentacion.Enumeradores;

namespace AnhHydroOctanoPresentacion.Clases.Controles
{
    public class CListaOperadores : COperador
    {

        private COperador vv = null;
        public CListaOperadores()
        {
           vv=new COperador();
        }

        public int intIdOperadorPadre { get; set; }
        public string strDescripcion { get; set; }
        public long Nit { get; set; }
        public long Fundempresa { get; set; }
        public List<COperador> ListaOperadores()
        {
            return null;
        }
        /*public List<Persona> Personal { get; set; }
        public class Persona
        {
            public string Nombres { get; set; }
            public string Apellidos { get; set; }
            public EnumTipoPersonaHydro TipoPersonaHydro { get; set; }
        }*/
    }

    public class COperador
    {
        public EnumDepartamento Departamento { get; set; }
        public EnumActividades Actividad { get; set; }
        public int intIdOperador { get; set; }
        public string strDescripcion { get; set; }
        public string strDireccion { get; set; }
        public string strLatitud { get; set; }
        public string strLongitud { get; set; }
    }
}