using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AnhHydroOctanoPresentacion.Controles
{
    public partial class CtrRepIne : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var lstRegistro = new List<CRegPrueba999>();
            var data = new CRegPrueba999();
            DateTime fechaActual = Convert.ToDateTime("01/06/2021");
            for (int i = 0; i < 100000; i++)
            {
                if (fechaActual >= Convert.ToDateTime("01/06/2021") && fechaActual <= Convert.ToDateTime("30/06/2021"))
                {
                }
                else
                {
                    fechaActual = Convert.ToDateTime("01/06/2021");
                }

                data.strCodigo = "8955623";
                data.strCodUbic = "54875544445";
                data.strCiudad = "COCHABAMBA";
                data.strCategoria = "DOMESTICA";
                data.strNit = "78996548";
                data.strRazonSocial = "JUAN PEREZ";
                data.strDireccion = "AV SIEMPRE VIVA 01";
                data.strSecEcon = "NOID";
                data.strSerMedi = "8955623";
                data.strFechaLec = fechaActual.ToShortDateString();
                data.strLectMed = "6681";
                data.strVolComFac = "1.561";
                data.strImpCons = "34.69";
                data.strCargAdd = "0.98";
                data.strCargAdd = "8955623";
                data.strDescuento = "0";
                data.strImporTotal = (Convert.ToDecimal(data.strImpCons) + Convert.ToDecimal(data.strCargAdd) - Convert.ToDecimal(data.strDescuento)).ToString();
                data.strFactura = "956";
                data.strAutorizacion = "200100123456";
                data.strCodControl = "11-AA-45-45";
                data.strFechaEmFact = fechaActual.ToShortDateString();
                lstRegistro.Add(data);

                fechaActual = fechaActual.AddDays(1);
            }
            dgvReporte.DataSource = lstRegistro;
            dgvReporte.DataBind();
        }


        public class CRegPrueba999
        {
            public string strCodigo { get; set; }
            public string strCodUbic { get; set; }
            public string strCiudad { get; set; }
            public string strCategoria { get; set; }
            public string strNit { get; set; }
            public string strRazonSocial { get; set; }
            public string strDireccion { get; set; }
            public string strSecEcon { get; set; }
            public string strSerMedi { get; set; }
            public string strFechaLec { get; set; }
            public string strLectMed { get; set; }
            public string strVolComFac { get; set; }
            public string strImpCons { get; set; }
            public string strCargAdd { get; set; }
            public string strDescuento { get; set; }
            public string strImporTotal { get; set; }
            public string strFactura { get; set; }
            public string strAutorizacion { get; set; }
            public string strCodControl { get; set; }
            public string strFechaEmFact { get; set; }
        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            dgeExportar.WriteXlsxToResponse();
        }
    }
}