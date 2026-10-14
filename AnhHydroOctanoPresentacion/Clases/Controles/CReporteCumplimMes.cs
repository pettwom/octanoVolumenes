
using System;
using System.Collections.Generic;
using System.IO;

namespace AnhHydroOctanoPresentacion.Clases.Controles
{
    public class CReporteCumplimMes
    {
        public string strDepto { get; set; }
        public decimal decIdConsumidor { get; set; }
        public string strConsumidor { get; set; }
        public string strPeriodo { get; set; }
        public string strCite { get; set; }
        public string strFechaPresentacion { get; set; }
        public string strEstado { get; set; }
        public decimal decEstado { get; set; }
        public string strJson { get; set; }

        public List<CReporteCumplimMes> cargaReporte()
        {
            //ORIGEN: .txt
            string strJson = String.Empty;
            using (StreamReader r = new StreamReader("d:\\json_ddjj_resumen.txt"))
            {
                string json = r.ReadToEnd();
                strJson = json;
            }

            List<CReporteCumplimMes> LstResultado = new List<CReporteCumplimMes>();
            LstResultado.Add(new CReporteCumplimMes()
            {
                strDepto = "LA PAZ",
                decIdConsumidor = 111,
                strConsumidor = "EESS 111", 
                strPeriodo = "201609", 
                strCite = "ASB-001", 
                strFechaPresentacion = "05/10/2016 10:50:41", 
                strEstado = "CORRECTO",
                decEstado = 1,
                strJson = strJson
            });
            LstResultado.Add(new CReporteCumplimMes()
            {
                strDepto = "LA PAZ",
                decIdConsumidor = 222,
                strConsumidor = "EESS 222",
                strPeriodo = "201609",
                strCite = "ASB-001",
                strFechaPresentacion = "05/10/2016 10:50:41",
                strEstado = "CORRECTO",
                decEstado = 1,
                strJson = strJson
            });

            LstResultado.Add(new CReporteCumplimMes()
            {
                strDepto = "LA PAZ",
                decIdConsumidor = 333,
                strConsumidor = "EESS 333",
                strPeriodo = "",
                strCite = "",
                strFechaPresentacion = "",
                strEstado = "PLAZO VENCIDO",
                decEstado = 2,
                strJson = ""
            });
            LstResultado.Add(new CReporteCumplimMes()
            {
                strDepto = "LA PAZ",
                decIdConsumidor = 444,
                strConsumidor = "EESS 444",
                strPeriodo = "201609",
                strCite = "ASB-002",
                strFechaPresentacion = "05/10/2016 10:50:41",
                strEstado = "CORRECTO",
                decEstado = 1,
                strJson = strJson
            });
            LstResultado.Add(new CReporteCumplimMes()
            {
                strDepto = "LA PAZ",
                decIdConsumidor = 555,
                strConsumidor = "EESS 555",
                strPeriodo = "201609",
                strCite = "ASB-003",
                strFechaPresentacion = "05/10/2016 10:50:41",
                strEstado = "CORRECTO",
                decEstado = 1,
                strJson = strJson
            });
            LstResultado.Add(new CReporteCumplimMes()
            {
                strDepto = "LA PAZ",
                decIdConsumidor = 666,
                strConsumidor = "EESS 666",
                strPeriodo = "201609",
                strCite = "ASB-003",
                strFechaPresentacion = "05/10/2016 10:50:41",
                strEstado = "CORRECTO",
                decEstado = 1,
                strJson = strJson
            });
            LstResultado.Add(new CReporteCumplimMes()
            {
                strDepto = "LA PAZ",
                decIdConsumidor = 777,
                strConsumidor = "EESS 777",
                strPeriodo = "201609",
                strCite = "ASB-003",
                strFechaPresentacion = "05/10/2016 10:50:41",
                strEstado = "CORRECTO",
                decEstado = 1,
                strJson = strJson
            });
            LstResultado.Add(new CReporteCumplimMes()
            {
                strDepto = "LA PAZ",
                decIdConsumidor = 888,
                strConsumidor = "EESS 888",
                strPeriodo = "",
                strCite = "",
                strFechaPresentacion = "",
                strEstado = "PLAZO VENCIDO",
                decEstado = 2,
                strJson = ""
            });
            LstResultado.Add(new CReporteCumplimMes()
            {
                strDepto = "LA PAZ",
                decIdConsumidor = 999,
                strConsumidor = "EESS 999",
                strPeriodo = "201609",
                strCite = "ASB-005",
                strFechaPresentacion = "05/10/2016 10:50:41",
                strEstado = "CORRECTO",
                decEstado = 1,
                strJson = strJson
            });
            LstResultado.Add(new CReporteCumplimMes()
            {
                strDepto = "ORURO",
                decIdConsumidor = 112,
                strConsumidor = "EESS 112",
                strPeriodo = "201609",
                strCite = "ASB-005",
                strFechaPresentacion = "05/10/2016 10:50:41",
                strEstado = "CORRECTO",
                decEstado = 1,
                strJson = strJson
            });
            LstResultado.Add(new CReporteCumplimMes()
            {
                strDepto = "ORURO",
                decIdConsumidor = 113,
                strConsumidor = "EESS 113",
                strPeriodo = "",
                strCite = "",
                strFechaPresentacion = "",
                strEstado = "AMPLIACION",
                decEstado = 3,
                strJson = ""
            });

            LstResultado.Add(new CReporteCumplimMes()
            {
                strDepto = "TARIJA",
                decIdConsumidor = 223,
                strConsumidor = "EESS 223",
                strPeriodo = "",
                strCite = "",
                strFechaPresentacion = "",
                strEstado = "INTIMADO",
                decEstado = 4,
                strJson = strJson
            });
            LstResultado.Add(new CReporteCumplimMes()
            {
                strDepto = "TARIJA",
                decIdConsumidor = 554,
                strConsumidor = "EESS 554",
                strPeriodo = "201609",
                strCite = "ASB-006",
                strFechaPresentacion = "05/10/2016 10:50:41",
                strEstado = "CORRECTO",
                decEstado = 1,
                strJson = strJson
            });
            LstResultado.Add(new CReporteCumplimMes()
            {
                strDepto = "TARIJA",
                decIdConsumidor = 745,
                strConsumidor = "EESS 745",
                strPeriodo = "201609",
                strCite = "ASB-007",
                strFechaPresentacion = "05/10/2016 10:50:41",
                strEstado = "CORRECTO",
                decEstado = 1,
                strJson = strJson
            });
            LstResultado.Add(new CReporteCumplimMes()
            {
                strDepto = "TARIJA",
                decIdConsumidor = 663,
                strConsumidor = "EESS 663",
                strPeriodo = "201609",
                strCite = "ASB-007",
                strFechaPresentacion = "05/10/2016 10:50:41",
                strEstado = "CORRECTO",
                decEstado = 1,
                strJson = strJson
            });
            LstResultado.Add(new CReporteCumplimMes()
            {
                strDepto = "TARIJA",
                decIdConsumidor = 455,
                strConsumidor = "EESS 455",
                strPeriodo = "",
                strCite = "",
                strFechaPresentacion = "",
                strEstado = "PLAZO VENCIDO",
                decEstado = 2,
                strJson = ""
            });
            LstResultado.Add(new CReporteCumplimMes()
            {
                strDepto = "TARIJA",
                decIdConsumidor = 995,
                strConsumidor = "EESS 995",
                strPeriodo = "201609",
                strCite = "ASB-008",
                strFechaPresentacion = "05/10/2016 10:50:41",
                strEstado = "CORRECTO",
                decEstado = 1,
                strJson = strJson
            });

            return LstResultado;
        }

    }
}