using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnhHydroOctanoAgenteServicio.Aplicacion
{
    [Serializable]
    public class EResultado
    {
        public int intCodigo { get; set; }
        public object oResultado { get; set; }
        public string strMensaje { get; set; }
    }
}
