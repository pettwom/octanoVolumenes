using System.Collections.Generic;

namespace AnhHydroOctanoPresentacion.Clases.Controles
{
    public class CMovimientoAcumulado
    {
        public int IntId { get; set; }
        public int IntContProd { get; set; }
        public string StrFecha { get; set; }
        public int IntIdProducto { get; set; }
        public string StrProducto { get; set; }
        public decimal DecSaldoIni { get; set; }
        public decimal DecSaldoInicial { get; set; }
        public decimal DecRecepcion { get; set; }
        public decimal DecDemasia { get; set; }
        public decimal DecIng03 { get; set; }
        public decimal DecIng04 { get; set; }
        public decimal DecIng05 { get; set; }
        public decimal DecTotalIng { get; set; }
        public decimal DecVenta { get; set; }
        public decimal DecMerma { get; set; }
        public decimal DecSal03 { get; set; }
        public decimal DecSal04 { get; set; }
        public decimal DecSal05 { get; set; }
        public decimal DecTotalSal { get; set; }
        public decimal DecSaldoFinal { get; set; }

    }

    public class CMovAcumulado
    {
        public int IntId { get; set; }
        public int IntContAgrup { get; set; }
        public int IntContProd { get; set; }
        public string StrFecha { get; set; }
        public int IntIdProducto { get; set; }
        public string StrProducto { get; set; }
        public string StrCodProducto { get; set; }
        public string StrUnidadMedida { get; set; }

        public decimal DecIdTipoInfraestructura { get; set; }
        public string StrTipoInfraestructura { get; set; }
        public decimal DecIdInfraestructura { get; set; }
        public string StrInfraestructura { get; set; }
        public string StrCodigoInfraestructura { get; set; }
        public string StrObservaciones { get; set; }

        public decimal DecSaldoIni { get; set; }
        public decimal DecSaldoInicial { get; set; }
        public decimal DecIng01 { get; set; }
        public decimal DecIng02 { get; set; }
        public decimal DecIng03 { get; set; }
        public decimal DecIng04 { get; set; }
        public decimal DecIng05 { get; set; }
        public decimal DecIng06 { get; set; }
        public decimal DecIng07 { get; set; }
        public decimal DecIng08 { get; set; }
        public decimal DecIng09 { get; set; }
        public decimal DecIng10 { get; set; }
        public decimal DecIng11 { get; set; }
        public decimal DecIng12 { get; set; }
        public decimal DecIng13 { get; set; }
        public decimal DecIng14 { get; set; }
        public decimal DecIng15 { get; set; }
        public decimal DecIng16 { get; set; }
        public decimal DecIng17 { get; set; }
        public decimal DecIng18 { get; set; }
        public decimal DecIng19 { get; set; }
        public decimal DecIng20 { get; set; }
        public decimal DecTotalIng { get; set; }
        public decimal DecSal01 { get; set; }
        public decimal DecSal02 { get; set; }
        public decimal DecSal03 { get; set; }
        public decimal DecSal04 { get; set; }
        public decimal DecSal05 { get; set; }
        public decimal DecSal06 { get; set; }
        public decimal DecSal07 { get; set; }
        public decimal DecSal08 { get; set; }
        public decimal DecSal09 { get; set; }
        public decimal DecSal10 { get; set; }
        public decimal DecSal11 { get; set; }
        public decimal DecSal12 { get; set; }
        public decimal DecSal13 { get; set; }
        public decimal DecSal14 { get; set; }
        public decimal DecSal15 { get; set; }
        public decimal DecSal16 { get; set; }
        public decimal DecSal17 { get; set; }
        public decimal DecSal18 { get; set; }
        public decimal DecSal19 { get; set; }
        public decimal DecSal20 { get; set; }
        public decimal DecTotalSal { get; set; }
        public decimal DecSaldoFinal { get; set; }
        public decimal DecSaldoFin { get; set; }
        public decimal[] VecValores { get; set; }
        public string StrIdGrupoUnico { get; set; }
        public decimal DecImporteTotal { get; set; }
    }
    public class CMovAcumulado_SAL
    {
        public int IntId { get; set; }
        public int IntContAgrup { get; set; }
        public int IntContProd { get; set; }
        public string StrFecha { get; set; }
        public int IntIdProducto { get; set; }
        public string StrProducto { get; set; }
        public string StrCodProducto { get; set; }
        public string StrUnidadMedida { get; set; }

        public decimal DecIdTipoInfraestructura { get; set; }
        public string StrTipoInfraestructura { get; set; }
        public decimal DecIdInfraestructura { get; set; }
        public string StrInfraestructura { get; set; }
        public string StrCodigoInfraestructura { get; set; }
        public string StrObservaciones { get; set; }

        public decimal DecSaldoIni { get; set; }
        public decimal DecSaldoInicial { get; set; }
        public decimal DecIng01 { get; set; }
        public decimal DecIng02 { get; set; }
        public decimal DecIng03 { get; set; }
        public decimal DecIng04 { get; set; }
        public decimal DecIng05 { get; set; }
        public decimal DecIng06 { get; set; }
        public decimal DecIng07 { get; set; }
        public decimal DecIng08 { get; set; }
        public decimal DecIng09 { get; set; }
        public decimal DecIng10 { get; set; }
        public decimal DecIng11 { get; set; }
        public decimal DecIng12 { get; set; }
        public decimal DecIng13 { get; set; }
        public decimal DecIng14 { get; set; }
        public decimal DecIng15 { get; set; }
        public decimal DecIng16 { get; set; }
        public decimal DecIng17 { get; set; }
        public decimal DecIng18 { get; set; }
        public decimal DecIng19 { get; set; }
        public decimal DecIng20 { get; set; }
        public decimal DecTotalIng { get; set; }
        public decimal DecSal01 { get; set; }
        public decimal DecSal02 { get; set; }
        public decimal DecSal03 { get; set; }
        public decimal DecSal04 { get; set; }
        public decimal DecSal05 { get; set; }
        public decimal DecSal06 { get; set; }
        public decimal DecSal07 { get; set; }
        public decimal DecSal08 { get; set; }
        public decimal DecSal09 { get; set; }
        public decimal DecSal10 { get; set; }
        public decimal DecSal11 { get; set; }
        public decimal DecSal12 { get; set; }
        public decimal DecSal13 { get; set; }
        public decimal DecSal14 { get; set; }
        public decimal DecSal15 { get; set; }
        public decimal DecSal16 { get; set; }
        public decimal DecSal17 { get; set; }
        public decimal DecSal18 { get; set; }
        public decimal DecSal19 { get; set; }
        public decimal DecSal20 { get; set; }
        public decimal DecTotalSal { get; set; }
        public decimal DecSaldoFinal { get; set; }
        public decimal DecSaldoFin { get; set; }
        public decimal[] VecValores { get; set; }
        public string StrIdGrupoUnico { get; set; }
        public decimal DecImporteTotal { get; set; }

        public decimal DecDia { get; set; }
        public string StrDescripcionDia { get; set; }
        public string StrMes { get; set; }
        public string StrAnio { get; set; }
        public string StrDescripcionTanque { get; set; }
        public decimal DecVolumenNominal { get; set; }
        public decimal DecProgramacion { get; set; }
        public decimal DecRecepProg { get; set; }
        public decimal DecVentaProg { get; set; }
        
    }

}