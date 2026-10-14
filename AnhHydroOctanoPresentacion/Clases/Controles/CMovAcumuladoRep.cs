using System.Collections.Generic;

namespace AnhHydroOctanoPresentacion.Clases.Controles
{
    public class CMovAcumuladoRep
    {
        public string StrFecha { get; set; }
        public int IntGrupoProducto { get; set; }
        public int IntIdProducto { get; set; }
        public string StrProducto { get; set; }
        public string StrUnidadMedida { get; set; }
        public int IntIdInfraestructura { get; set; }
        public string StrInfraestructura { get; set; }
        public decimal DecSaldoIni { get; set; }
        public decimal DecSaldoInicial { get; set; }
        public decimal DecIng01 { get; set; }
        public decimal DecIng02 { get; set; }
        public decimal DecIng03 { get; set; }
        public decimal DecIng04 { get; set; }
        public decimal DecIng05 { get; set; }
        public decimal DecTotalIng { get; set; }
        public decimal DecSal01 { get; set; }
        public decimal DecSal02 { get; set; }
        public decimal DecSal03 { get; set; }
        public decimal DecSal04 { get; set; }
        public decimal DecSal05 { get; set; }
        public decimal DecTotalSal { get; set; }
        public decimal DecSaldoFinal { get; set; }
        public decimal DecSaldoFin { get; set; }

        public string StrTitIng01 { get; set; }
        public string StrTitIng02 { get; set; }
        public string StrTitIng03 { get; set; }
        public string StrTitIng04 { get; set; }
        public string StrTitIng05 { get; set; }

        public string StrTitSal01 { get; set; }
        public string StrTitSal02 { get; set; }
        public string StrTitSal03 { get; set; }
        public string StrTitSal04 { get; set; }
        public string StrTitSal05 { get; set; }
    }

}