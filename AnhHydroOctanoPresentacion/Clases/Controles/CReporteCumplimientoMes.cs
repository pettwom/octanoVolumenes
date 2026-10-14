
using System.Collections.Generic;

namespace AnhHydroOctanoPresentacion.Clases.Controles
{
    public class CReporteCumplimientoMes
    {
        public string strActividad { get; set; }
        public string strDepto { get; set; }
        public string strConsumidor { get; set; }
        public string strPeriodo { get; set; }
        public string strCite { get; set; }
        public string strFechaPresentacion { get; set; }
        public string strProducto { get; set; }
        public decimal decSaldoIni { get; set; }
        public decimal decTotIng { get; set; }
        public decimal decTotSal { get; set; }
        public decimal decSaldoFin { get; set; }
        public string strEstado { get; set; }
        public string strAccion { get; set; }


        public List<CReporteCumplimientoMes> cargaReporte()
        {
            List<CReporteCumplimientoMes> LstResultado = new List<CReporteCumplimientoMes>();
            LstResultado.Add(new CReporteCumplimientoMes() {strActividad = "EESS LIQ",strDepto = "LA PAZ",strConsumidor = "EESS 111",strPeriodo = "201601",strCite = "ASB-001",strFechaPresentacion = "42403",strProducto = "GE",decSaldoIni = 1000,decTotIng = 2000,decTotSal = 500,decSaldoFin = 2500,strEstado = "CUMPLIDO"});
            LstResultado.Add(new CReporteCumplimientoMes()
            {
                strActividad = "EESS LIQ",
                strDepto = "LA PAZ",
                strConsumidor = "EESS 111",
                strPeriodo = "201601",
                strCite = "ASB-001",
                strFechaPresentacion = "42403",
                strProducto = "DO",
                decSaldoIni = 2500,
                decTotIng = 8000,
                decTotSal = 592,
                decSaldoFin = 9908,
                strEstado = "CUMPLIDO"
            });
            LstResultado.Add(new CReporteCumplimientoMes()
            {
                strActividad = "EESS LIQ",
                strDepto = "LA PAZ",
                strConsumidor = "EESS 222",
                strPeriodo = "201601",
                strCite = "ASB-002",
                strFechaPresentacion = "42403",
                strProducto = "GE",
                decSaldoIni = 9908,
                decTotIng = 0,
                decTotSal = 1245,
                decSaldoFin = 8663,
                strEstado = "CUMPLIDO"
            });
            LstResultado.Add(new CReporteCumplimientoMes()
            {
                strActividad = "EESS LIQ",
                strDepto = "LA PAZ",
                strConsumidor = "EESS 222",
                strPeriodo = "201601",
                strCite = "ASB-002",
                strFechaPresentacion = "42403",
                strProducto = "DO",
                decSaldoIni = 8663,
                decTotIng = 1200,
                decTotSal = 500,
                decSaldoFin = 9363,
                strEstado = "CUMPLIDO"
            });
            LstResultado.Add(new CReporteCumplimientoMes()
            {
                strActividad = "EESS LIQ",
                strDepto = "LA PAZ",
                strConsumidor = "EESS 333",
                strPeriodo = "201601",
                strCite = "ASB-003",
                strFechaPresentacion = "42403",
                strProducto = "GE",
                decSaldoIni = 9363,
                decTotIng = 0,
                decTotSal = 34,
                decSaldoFin = 9329,
                strEstado = "CUMPLIDO"
            });
            LstResultado.Add(new CReporteCumplimientoMes()
            {
                strActividad = "EESS LIQ",
                strDepto = "LA PAZ",
                strConsumidor = "EESS 333",
                strPeriodo = "201601",
                strCite = "ASB-003",
                strFechaPresentacion = "42403",
                strProducto = "DO",
                decSaldoIni = 9329,
                decTotIng = 0,
                decTotSal = 666,
                decSaldoFin = 8663,
                strEstado = "CUMPLIDO"
            });
            LstResultado.Add(new CReporteCumplimientoMes()
            {
                strActividad = "EESS LIQ",
                strDepto = "LA PAZ",
                strConsumidor = "EESS 333",
                strPeriodo = "201601",
                strCite = "ASB-003",
                strFechaPresentacion = "42403",
                strProducto = "GP",
                decSaldoIni = 8663,
                decTotIng = 0,
                decTotSal = 634,
                decSaldoFin = 8029,
                strEstado = "CUMPLIDO"
            });
            LstResultado.Add(new CReporteCumplimientoMes()
            {
                strActividad = "EESS LIQ",
                strDepto = "ORURO",
                strConsumidor = "EESS 444",
                strPeriodo = "201601",
                strCite = "ASB-004",
                strFechaPresentacion = "42403",
                strProducto = "GE",
                decSaldoIni = 8029,
                decTotIng = 0,
                decTotSal = 45,
                decSaldoFin = 7984,
                strEstado = "CUMPLIDO"
            });
            LstResultado.Add(new CReporteCumplimientoMes()
            {
                strActividad = "EESS LIQ",
                strDepto = "ORURO",
                strConsumidor = "EESS 555",
                strPeriodo = "201601",
                strCite = "ASB-005",
                strFechaPresentacion = "42403",
                strProducto = "GE",
                decSaldoIni = 7984,
                decTotIng = 0,
                decTotSal = 545,
                decSaldoFin = 7439,
                strEstado = "CUMPLIDO"
            });
            LstResultado.Add(new CReporteCumplimientoMes()
            {
                strActividad = "EESS LIQ",
                strDepto = "ORURO",
                strConsumidor = "EESS 555",
                strPeriodo = "201601",
                strCite = "ASB-005",
                strFechaPresentacion = "42403",
                strProducto = "DO",
                decSaldoIni = 7439,
                decTotIng = 0,
                decTotSal = 1854,
                decSaldoFin = 5585,
                strEstado = "CUMPLIDO"
            });
            LstResultado.Add(new CReporteCumplimientoMes()
            {
                strActividad = "EESS LIQ",
                strDepto = "ORURO",
                strConsumidor = "EESS 555",
                strPeriodo = "201601",
                strCite = "ASB-005",
                strFechaPresentacion = "42403",
                strProducto = "GP",
                decSaldoIni = 5585,
                decTotIng = 455,
                decTotSal = 5000,
                decSaldoFin = 1040,
                strEstado = "CUMPLIDO"
            });

            LstResultado.Add(new CReporteCumplimientoMes()
            {
                strActividad = "EESS LIQ",
                strDepto = "TARIJA",
                strConsumidor = "EESS 666",
                strPeriodo = "201601",
                strCite = "ASB-006",
                strFechaPresentacion = "42403",
                strProducto = "GE",
                decSaldoIni = 5,
                decTotIng = 4545,
                decTotSal = 56,
                decSaldoFin = 5,
                strEstado = "CUMPLIDO"
            });
            LstResultado.Add(new CReporteCumplimientoMes()
            {
                strActividad = "EESS LIQ",
                strDepto = "TARIJA",
                strConsumidor = "EESS 666",
                strPeriodo = "201601",
                strCite = "ASB-006",
                strFechaPresentacion = "42403",
                strProducto = "DO",
                decSaldoIni = 56,
                decTotIng = 45,
                decTotSal = 56,
                decSaldoFin = 45,
                strEstado = "CUMPLIDO"
            });
            LstResultado.Add(new CReporteCumplimientoMes()
            {
                strActividad = "EESS LIQ",
                strDepto = "TARIJA",
                strConsumidor = "EESS 777",
                strPeriodo = "201601",
                strCite = "ASB-007",
                strFechaPresentacion = "42403",
                strProducto = "GE",
                decSaldoIni = 56,
                decTotIng = 456,
                decTotSal = 56,
                decSaldoFin = 56,
                strEstado = "CUMPLIDO"
            });
            LstResultado.Add(new CReporteCumplimientoMes()
            {
                strActividad = "EESS LIQ",
                strDepto = "TARIJA",
                strConsumidor = "EESS 777",
                strPeriodo = "201601",
                strCite = "ASB-007",
                strFechaPresentacion = "42403",
                strProducto = "DO",
                decSaldoIni = 56,
                decTotIng = 56,
                decTotSal = 56,
                decSaldoFin = 56,
                strEstado = "CUMPLIDO"
            });
            LstResultado.Add(new CReporteCumplimientoMes()
            {
                strActividad = "EESS LIQ",
                strDepto = "TARIJA",
                strConsumidor = "EESS 888",
                strPeriodo = "201601",
                strCite = "ASB-008",
                strFechaPresentacion = "42403",
                strProducto = "GE",
                decSaldoIni = 56,
                decTotIng = 56,
                decTotSal = 56,
                decSaldoFin = 56,
                strEstado = "CUMPLIDO"
            });
            LstResultado.Add(new CReporteCumplimientoMes()
            {
                strActividad = "EESS LIQ",
                strDepto = "TARIJA",
                strConsumidor = "EESS 888",
                strPeriodo = "201601",
                strCite = "ASB-008",
                strFechaPresentacion = "42403",
                strProducto = "DO",
                decSaldoIni = 565,
                decTotIng = 56,
                decTotSal = 65,
                decSaldoFin = 56,
                strEstado = "CUMPLIDO"
            });


            return LstResultado;
        }


    }
}