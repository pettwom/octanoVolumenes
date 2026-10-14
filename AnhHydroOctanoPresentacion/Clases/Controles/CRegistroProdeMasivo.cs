using AnhHydroOctanoPresentacion.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnhHydroOctanoPresentacion.Clases.Controles
{
    public class CRegistroProdeMasivo
    {
        public decimal DecIdMovimiento { get; set; }
        public decimal DecIdConsumidor { get; set; }
        public string strDepartamento { get; set; }
        public string StrCodigoSirehidro { get; set; }
        public string StrEstacionServicio { get; set; }
        public decimal DecVolumenSemana1 { get; set; }
        public decimal DecVolumenSemana2 { get; set; }
        public decimal DecVolumenSemana3 { get; set; }
        public decimal DecVolumenSemana4 { get; set; }
        public decimal DecVolumenSemana5 { get; set; }
        public List<ObjProtoMovimientoTransaccion> objProtoMovimiento { get; set; }

    }
}