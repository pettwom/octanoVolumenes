
using System.Collections.Generic;
using System.Web;
using AnhHydroOctanoPersistencia.Entidades;

namespace AnhHydroOctanoPresentacion.Sitio.Movimientos
{
    using System;
    using System.IO;
    using System.Web.Security;
    using Parametros;
    using Librerias.Anh.Us;
    public partial class WFDescargaRendimVolumetrico : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //string strgNombre = tlsOperadores.FocusedNode.GetValue("strDescripcion").ToString();
            string strgNombre = Request.Params[0] ?? "-1";
            string[] strgDatosSeparados = strgNombre.Split('_');
            List<O_RENDIMIENTO_CTY> lstDato = (List<O_RENDIMIENTO_CTY>)Session[CVariablesSesion.listaReporteSession];
            if (strgDatosSeparados[0].Contains("ORO"))
            {
                #region MyRegion
                repRendimientoVolumetricoOroNegro reporte = new repRendimientoVolumetricoOroNegro();
                reporte.xtcRazonSocial.Text = strgDatosSeparados[0];
                reporte.xtcGestion.Text = strgDatosSeparados[2];
                reporte.xtcPeriodo.Text = strgDatosSeparados[1];
                reporte.xrTFechaHora.Text = DateTime.Now.ToString("G");
                reporte.xrTQuienImprime.Text = Session[CVariablesSesion.DatosUsuario].ToString();
                foreach (O_RENDIMIENTO_CTY oRendimientoCty in lstDato)
                {
                    if (oRendimientoCty.AREA_UNIDAD.Contains("ATMOSFERICA"))
                    {
                        reporte.xrTDestAdmosOprDia.Text = oRendimientoCty.OPERACION_DIA;
                        reporte.xrTDestAdmosCargTotal.Text = oRendimientoCty.CARGA_TOTAL;
                        reporte.xrTDestAdmosCargPromBBL.Text = oRendimientoCty.CARGA_PROMEDIO;
                        reporte.xrTDestAdmosCapOperaci.Text = oRendimientoCty.CAPACIDAD_OPERACION;
                        reporte.xrTDestAdmosCombLiqu.Text = oRendimientoCty.COMB_LIQUIDO;
                        reporte.xrTDestAdmosCombGas.Text = oRendimientoCty.COMB_GAS;
                        reporte.xrTDestAdmosVaporTn.Text = oRendimientoCty.VAPOR_TN;
                        reporte.xrTDestAdmosAguaNfr.Text = oRendimientoCty.AGUA_ENFR;
                        reporte.xrTDestAdmosAireComp.Text = oRendimientoCty.AIRE_COMP;
                        reporte.xrTDestAdmosCompkWh.Text = oRendimientoCty.COMPARAD_KWH;
                        reporte.xrTDestAdmosPropkWh.Text = oRendimientoCty.PROPIA_KWH;
                        List<O_RENDIMIENTO_VOLUM_CTY> listaCtys = oRendimientoCty.oRendimientoVolumetricoCty != null ? oRendimientoCty.oRendimientoVolumetricoCty : new List<O_RENDIMIENTO_VOLUM_CTY>();
                        foreach (O_RENDIMIENTO_VOLUM_CTY listaCty in listaCtys)
                        {
                            if (listaCty.PRODUCTO.Contains("DIESEL")) { reporte.xrTDestRendDiesl.Text = listaCty.VOLUMEN; }
                            else if (listaCty.PRODUCTO.Contains("GASOLINA")) { reporte.xrTDestRendgasb.Text = listaCty.VOLUMEN; }
                            else if (listaCty.PRODUCTO.Contains("CRUDO")) { reporte.xrTDestRendCrudRes.Text = listaCty.VOLUMEN; }
                            else if (listaCty.PRODUCTO.Contains("PERDIDA")) { reporte.xrTDestRendPerdid.Text = listaCty.VOLUMEN; }
                        }
                    }
                    else if (oRendimientoCty.AREA_UNIDAD.Contains("CATALITICA"))
                    {
                        reporte.xrTReforCatOprDia.Text = oRendimientoCty.OPERACION_DIA;
                        reporte.xrTReformCatsCargTotal0.Text = oRendimientoCty.CARGA_TOTAL;
                        reporte.xrTReformCatsCargPromBbl.Text = oRendimientoCty.CARGA_PROMEDIO;
                        reporte.xrTReformCatOperaci.Text = oRendimientoCty.CAPACIDAD_OPERACION;
                        reporte.xrTReformCatbLiqu.Text = oRendimientoCty.COMB_LIQUIDO;
                        reporte.xrTReformCatCombGas.Text = oRendimientoCty.COMB_GAS;
                        reporte.xrTReformCatVaporTn.Text = oRendimientoCty.VAPOR_TN;
                        reporte.xrTReformCatAguaNfr.Text = oRendimientoCty.AGUA_ENFR;
                        reporte.xrTReformCatAireComp.Text = oRendimientoCty.AIRE_COMP;
                        reporte.xrTReformCatCompkWh.Text = oRendimientoCty.COMPARAD_KWH;
                        reporte.xrTReformCatPropkWh.Text = oRendimientoCty.PROPIA_KWH;
                        List<O_RENDIMIENTO_VOLUM_CTY> listaCtys = oRendimientoCty.oRendimientoVolumetricoCty != null ? oRendimientoCty.oRendimientoVolumetricoCty : new List<O_RENDIMIENTO_VOLUM_CTY>();
                        foreach (O_RENDIMIENTO_VOLUM_CTY listaCty in listaCtys)
                        {
                            if (listaCty.PRODUCTO.Contains("ESPECIAL")) { reporte.xrTDestRendPerdid.Text = listaCty.VOLUMEN; }
                            else if (listaCty.PRODUCTO.Contains("REFINERIA")) { reporte.xrTDestRendPerdid.Text = listaCty.VOLUMEN; }
                            else if (listaCty.PRODUCTO.Contains("GLP")) { reporte.xrTDestRendPerdid.Text = listaCty.VOLUMEN; }
                            else if (listaCty.PRODUCTO.Contains("NAFTA")) { reporte.xrTDestRendPerdid.Text = listaCty.VOLUMEN; }
                            else if (listaCty.PRODUCTO.Contains("PERDIDA")) { reporte.xrTDestRendPerdid.Text = listaCty.VOLUMEN; }
                        }
                    }
                }
                MemoryStream stream = new MemoryStream();
                Response.Clear();
                reporte.ExportToPdf(stream);
                Response.ContentType = "application/pdf";
                Response.AddHeader("Accept-Header", stream.Length.ToString());
                Response.AddHeader("Content-Disposition", "Attachment; filename=" + "reporteRendimientoVolumetricoOroNegro.pdf");
                Response.AddHeader("Content-Length", stream.Length.ToString());
                Response.BinaryWrite(stream.ToArray());
                Response.Flush();


                #endregion

            }
            else if (strgDatosSeparados[0].Contains("ELDER"))
            {
                #region
                repRendimientoVolumetricoGEldeBell reporte = new repRendimientoVolumetricoGEldeBell();
                reporte.xtcRazonSocial.Text = strgDatosSeparados[0];
                reporte.xtcGestion.Text = strgDatosSeparados[2];
                reporte.xtcPeriodo.Text = strgDatosSeparados[1];
                reporte.xrTFechaHora.Text = DateTime.Now.ToString("G");
                reporte.xrTQuienImprime.Text = Session[CVariablesSesion.DatosUsuario].ToString();
                foreach (O_RENDIMIENTO_CTY oRendimientoCty in lstDato)
                {
                    if (oRendimientoCty.AREA_UNIDAD == "U. DESTILACIÓN ADMOSFERICA I")
                    {
                        reporte.xrTDestAdmosfericaOperacion.Text = oRendimientoCty.OPERACION_DIA;
                        reporte.xrTDestAdmosfericaCargaM3.Text = oRendimientoCty.CARGA_TOTAL;
                        reporte.xrTDestAdmosfericaCrgaPromedio.Text = oRendimientoCty.CARGA_PROMEDIO;
                        reporte.xrTDestAdmosfericaCapOperac.Text = oRendimientoCty.CAPACIDAD_OPERACION;
                        reporte.xrTDestAdmosfericaCombLiq.Text = oRendimientoCty.COMB_LIQUIDO;
                        reporte.xrTDestAdmosfericaCombGas.Text = oRendimientoCty.COMB_GAS;
                        reporte.xrTDestAdmosfericaVaporTn.Text = oRendimientoCty.VAPOR_TN;
                        reporte.xrTDestAdmosfericaAguaNfr.Text = oRendimientoCty.AGUA_ENFR;
                        reporte.xrTDestAdmosfericaAireCom.Text = oRendimientoCty.AIRE_COMP;
                        reporte.xrTDestAdmosfericaCompradakWh.Text = oRendimientoCty.COMPARAD_KWH;
                        reporte.xrTDestAdmosfericaPropiakWh.Text = oRendimientoCty.PROPIA_KWH;
                        List<O_RENDIMIENTO_VOLUM_CTY> listaCtys = oRendimientoCty.oRendimientoVolumetricoCty != null ? oRendimientoCty.oRendimientoVolumetricoCty : new List<O_RENDIMIENTO_VOLUM_CTY>();
                        foreach (O_RENDIMIENTO_VOLUM_CTY listaCty in listaCtys)
                        {
                            if (listaCty.PRODUCTO.Contains("C4")) { reporte.xrTDetil1C4.Text = listaCty.VOLUMEN; }
                            else if (listaCty.PRODUCTO.Contains("LSR")) { reporte.xrTDetil1LSR.Text = listaCty.VOLUMEN; }
                            else if (listaCty.PRODUCTO.Contains("MSR")) { reporte.xrTDetil1MSR.Text = listaCty.VOLUMEN; }
                            else if (listaCty.PRODUCTO.Contains("DL")) { reporte.xrTDetil1DL.Text = listaCty.VOLUMEN; }
                            else if (listaCty.PRODUCTO.Contains("DM")) { reporte.xrTDetil1DM.Text = listaCty.VOLUMEN; }
                            else if (listaCty.PRODUCTO.Contains("DP")) { reporte.xrTDetil1DP.Text = listaCty.VOLUMEN; }
                            else if (listaCty.PRODUCTO.Contains("CR")) { reporte.xrTDetil1CR.Text = listaCty.VOLUMEN; }
                            else if (listaCty.PRODUCTO.Contains("GM")) { reporte.xrTDetil1GM.Text = listaCty.VOLUMEN; }
                        }
                    }
                    else if (oRendimientoCty.AREA_UNIDAD == "U. DESTILACIÓN ADMOSFERICA II")
                    {
                        reporte.xrTDestAdmosferica2Operacion.Text = oRendimientoCty.OPERACION_DIA;
                        reporte.xrTDestAdmosferica2CargaM3.Text = oRendimientoCty.CARGA_TOTAL;
                        reporte.xrTDestAdmosferica2CrgaPromedio.Text = oRendimientoCty.CARGA_PROMEDIO;
                        reporte.xrTDestAdmosferica2CapOperac.Text = oRendimientoCty.CAPACIDAD_OPERACION;
                        reporte.xrTDestAdmosferica2CombLiq.Text = oRendimientoCty.COMB_LIQUIDO;
                        reporte.xrTDestAdmosferica2CombGas.Text = oRendimientoCty.COMB_GAS;
                        reporte.xrTDestAdmosferica2VaporTn.Text = oRendimientoCty.VAPOR_TN;
                        reporte.xrTDestAdmosferica2AguaNfr.Text = oRendimientoCty.AGUA_ENFR;
                        reporte.xrTDestAdmosferica2AireCom.Text = oRendimientoCty.AIRE_COMP;
                        reporte.xrTDestAdmosferica2CompradakWh.Text = oRendimientoCty.COMPARAD_KWH;
                        reporte.xrTDestAdmosferica2PropiakWh.Text = oRendimientoCty.PROPIA_KWH;
                        List<O_RENDIMIENTO_VOLUM_CTY> listaCtys = oRendimientoCty.oRendimientoVolumetricoCty != null ? oRendimientoCty.oRendimientoVolumetricoCty : new List<O_RENDIMIENTO_VOLUM_CTY>();
                        foreach (O_RENDIMIENTO_VOLUM_CTY listaCty in listaCtys)
                        {

                            if (listaCty.PRODUCTO.Contains("C4")) { reporte.xrTDetil2C4.Text = listaCty.VOLUMEN; }
                            else if (listaCty.PRODUCTO.Contains("LSR")) { reporte.xrTDetil2LSR.Text = listaCty.VOLUMEN; }
                            else if (listaCty.PRODUCTO.Contains("MSR")) { reporte.xrTDetil2MSR.Text = listaCty.VOLUMEN; }
                            else if (listaCty.PRODUCTO.Contains("DL")) { reporte.xrTDetil2DL.Text = listaCty.VOLUMEN; }
                            else if (listaCty.PRODUCTO.Contains("DM")) { reporte.xrTDetil2DM.Text = listaCty.VOLUMEN; }
                            else if (listaCty.PRODUCTO.Contains("DP")) { reporte.xrTDetil2DP.Text = listaCty.VOLUMEN; }
                            else if (listaCty.PRODUCTO.Contains("CR")) { reporte.xrTDetil2CR.Text = listaCty.VOLUMEN; }
                            else if (listaCty.PRODUCTO.Contains("RECIRC")) { reporte.xrTDetil2RECIRC.Text = listaCty.VOLUMEN; }
                        }
                    }
                    else if (oRendimientoCty.AREA_UNIDAD.Contains("RECUPERACIÓN GASES"))
                    {
                        reporte.xrTRecuperacnOperacion.Text = oRendimientoCty.OPERACION_DIA;
                        reporte.xrTRecuperacnCargaM3.Text = oRendimientoCty.CARGA_TOTAL;
                        reporte.xrTRecuperacnCrgaPromedio.Text = oRendimientoCty.CARGA_PROMEDIO;
                        reporte.xrTRecuperacnCapOperac.Text = oRendimientoCty.CAPACIDAD_OPERACION;
                        reporte.xrTRecuperacnCombLiq.Text = oRendimientoCty.COMB_LIQUIDO;
                        reporte.xrTRecuperacnCombGas.Text = oRendimientoCty.COMB_GAS;
                        reporte.xrTRecuperacnVaporTn.Text = oRendimientoCty.VAPOR_TN;
                        reporte.xrTRecuperacnAguaNfr.Text = oRendimientoCty.AGUA_ENFR;
                        reporte.xrTRecuperacnAireCom.Text = oRendimientoCty.AIRE_COMP;
                        reporte.xrTRecuperacnCompradakWh.Text = oRendimientoCty.COMPARAD_KWH;
                        reporte.xrTRecuperacnPropiakWh.Text = oRendimientoCty.PROPIA_KWH;
                        List<O_RENDIMIENTO_VOLUM_CTY> listaCtys = oRendimientoCty.oRendimientoVolumetricoCty != null ? oRendimientoCty.oRendimientoVolumetricoCty : new List<O_RENDIMIENTO_VOLUM_CTY>();
                        foreach (O_RENDIMIENTO_VOLUM_CTY listaCty in listaCtys)
                        {
                            if (listaCty.PRODUCTO.Contains("GAS REF")) { reporte.xrTRecupGasGASREF.Text = listaCty.VOLUMEN; }
                            else if (listaCty.PRODUCTO.Contains("C4")) { reporte.xrTRecupGasC4.Text = listaCty.VOLUMEN; }
                            else if (listaCty.PRODUCTO.Contains("GLP")) { reporte.xrTRecupGasGLP.Text = listaCty.VOLUMEN; }
                            else if (listaCty.PRODUCTO.Contains("C3")) { reporte.xrTRecupGasC3.Text = listaCty.VOLUMEN; }
                        }
                    }
                    else if (oRendimientoCty.AREA_UNIDAD == "U. REFORMACIÓN CATALITICA I")
                    {
                        reporte.xrTRefCataliticaOperacion.Text = oRendimientoCty.OPERACION_DIA;
                        reporte.xrTRefCataliticaCargaM3.Text = oRendimientoCty.CARGA_TOTAL;
                        reporte.xrTRefCataliticaCrgaPromedio.Text = oRendimientoCty.CARGA_PROMEDIO;
                        reporte.xrTRefCataliticaCapOperac.Text = oRendimientoCty.CAPACIDAD_OPERACION;
                        reporte.xrTRefCataliticaCombLiq.Text = oRendimientoCty.COMB_LIQUIDO;
                        reporte.xrTRefCataliticaCombGas.Text = oRendimientoCty.COMB_GAS;
                        reporte.xrTRefCataliticaVaporTn.Text = oRendimientoCty.VAPOR_TN;
                        reporte.xrTRefCataliticaAguaNfr.Text = oRendimientoCty.AGUA_ENFR;
                        reporte.xrTRefCataliticaAireCom.Text = oRendimientoCty.AIRE_COMP;
                        reporte.xrTRefCataliticaCompradakWh.Text = oRendimientoCty.COMPARAD_KWH;
                        reporte.xrTRefCataliticaPropiakWh.Text = oRendimientoCty.PROPIA_KWH;
                        List<O_RENDIMIENTO_VOLUM_CTY> listaCtys = oRendimientoCty.oRendimientoVolumetricoCty != null ? oRendimientoCty.oRendimientoVolumetricoCty : new List<O_RENDIMIENTO_VOLUM_CTY>();
                        foreach (O_RENDIMIENTO_VOLUM_CTY listaCty in listaCtys)
                        {
                            if (listaCty.PRODUCTO.Contains("GAS REF")) { reporte.xrTCatalitica1GASREF.Text = listaCty.VOLUMEN; }
                            else if (listaCty.PRODUCTO.Contains("C4 + C3")) { reporte.xrTCatalitica1C4C3.Text = listaCty.VOLUMEN; }
                            else if (listaCty.PRODUCTO.Contains("PLATF")) { reporte.xrTCatalitica1PLATF.Text = listaCty.VOLUMEN; }
                            else if (listaCty.PRODUCTO.Contains("REC. GASES")) { reporte.xrTCatalitica1RECGASES.Text = listaCty.VOLUMEN; }
                        }
                    }
                    else if (oRendimientoCty.AREA_UNIDAD == "U. REFORMACIÓN CATALITICA II")
                    {
                        reporte.xrTRefCatalitica2Operacion.Text = oRendimientoCty.OPERACION_DIA;
                        reporte.xrTRefCatalitica2CargaM3.Text = oRendimientoCty.CARGA_TOTAL;
                        reporte.xrTRefCatalitica2CrgaPromedio.Text = oRendimientoCty.CARGA_PROMEDIO;
                        reporte.xrTRefCatalitica2CapOperac.Text = oRendimientoCty.CAPACIDAD_OPERACION;
                        reporte.xrTRefCatalitica2CombLiq.Text = oRendimientoCty.COMB_LIQUIDO;
                        reporte.xrTRefCatalitica2CombGas.Text = oRendimientoCty.COMB_GAS;
                        reporte.xrTRefCatalitica2VaporTn.Text = oRendimientoCty.VAPOR_TN;
                        reporte.xrTRefCatalitica2AguaNfr.Text = oRendimientoCty.AGUA_ENFR;
                        reporte.xrTRefCatalitica2AireCom.Text = oRendimientoCty.AIRE_COMP;
                        reporte.xrTRefCatalitica2CompradakWh.Text = oRendimientoCty.COMPARAD_KWH;
                        reporte.xrTRefCatalitica2PropiakWh.Text = oRendimientoCty.PROPIA_KWH;
                        List<O_RENDIMIENTO_VOLUM_CTY> listaCtys = oRendimientoCty.oRendimientoVolumetricoCty != null ? oRendimientoCty.oRendimientoVolumetricoCty : new List<O_RENDIMIENTO_VOLUM_CTY>();
                        foreach (O_RENDIMIENTO_VOLUM_CTY listaCty in listaCtys)
                        {
                            if (listaCty.PRODUCTO.Contains("GAS REF")) { reporte.xrTCatalitica2GASREF.Text = listaCty.VOLUMEN; }
                            else if (listaCty.PRODUCTO.Contains("C4 + C3")) { reporte.xrTCatalitica2C4C3.Text = listaCty.VOLUMEN; }
                            else if (listaCty.PRODUCTO.Contains("PLATF")) { reporte.xrTCatalitica2PLATF.Text = listaCty.VOLUMEN; }
                            else if (listaCty.PRODUCTO.Contains("REC. GASES")) { reporte.xrTCatalitica2RECGASES.Text = listaCty.VOLUMEN; }
                        }
                    }
                    else if (oRendimientoCty.AREA_UNIDAD.Contains("REDESTILADO"))
                    {
                        reporte.xrTReDestiladoOperacion.Text = oRendimientoCty.OPERACION_DIA;
                        reporte.xrTReDestiladoCargaM3.Text = oRendimientoCty.CARGA_TOTAL;
                        reporte.xrTReDestiladoCrgaPromedio.Text = oRendimientoCty.CARGA_PROMEDIO;
                        reporte.xrTReDestiladoCapOperac.Text = oRendimientoCty.CAPACIDAD_OPERACION;
                        reporte.xrTReDestiladoCombLiq.Text = oRendimientoCty.COMB_LIQUIDO;
                        reporte.xrTReDestiladoCombGas.Text = oRendimientoCty.COMB_GAS;
                        reporte.xrTReDestiladoVaporTn.Text = oRendimientoCty.VAPOR_TN;
                        reporte.xrTReDestiladoAguaNfr.Text = oRendimientoCty.AGUA_ENFR;
                        reporte.xrTReDestiladoAireCom.Text = oRendimientoCty.AIRE_COMP;
                        reporte.xrTReDestiladoCompradakWh.Text = oRendimientoCty.COMPARAD_KWH;
                        reporte.xrTReDestiladoPropiakWh.Text = oRendimientoCty.PROPIA_KWH;
                        List<O_RENDIMIENTO_VOLUM_CTY> listaCtys = oRendimientoCty.oRendimientoVolumetricoCty != null ? oRendimientoCty.oRendimientoVolumetricoCty : new List<O_RENDIMIENTO_VOLUM_CTY>();
                        foreach (O_RENDIMIENTO_VOLUM_CTY listaCty in listaCtys)
                        {
                            if (listaCty.PRODUCTO.Contains("REDEST")) { reporte.xrRedestiREDEST.Text = listaCty.VOLUMEN; }
                            else if (listaCty.PRODUCTO.Contains("CORTE")) { reporte.xrRedestiCORTE.Text = listaCty.VOLUMEN; }
                        }
                    }
                    else if (oRendimientoCty.AREA_UNIDAD.Contains("DEGAN"))
                    {
                        reporte.xrTDeganOperacion.Text = oRendimientoCty.OPERACION_DIA;
                        reporte.xrTDeganCargaM3.Text = oRendimientoCty.CARGA_TOTAL;
                        reporte.xrTDeganCrgaPromedio.Text = oRendimientoCty.CARGA_PROMEDIO;
                        reporte.xrTDeganCapOperac.Text = oRendimientoCty.CAPACIDAD_OPERACION;
                        reporte.xrTDeganCombLiq.Text = oRendimientoCty.COMB_LIQUIDO;
                        reporte.xrTDeganCombGas.Text = oRendimientoCty.COMB_GAS;
                        reporte.xrTDeganVaporTn.Text = oRendimientoCty.VAPOR_TN;
                        reporte.xrTDeganAguaNfr.Text = oRendimientoCty.AGUA_ENFR;
                        reporte.xrTDeganAireCom.Text = oRendimientoCty.AIRE_COMP;
                        reporte.xrTDeganCompradakWh.Text = oRendimientoCty.COMPARAD_KWH;
                        reporte.xrTDeganPropiakWh.Text = oRendimientoCty.PROPIA_KWH;
                        List<O_RENDIMIENTO_VOLUM_CTY> listaCtys = oRendimientoCty.oRendimientoVolumetricoCty != null ? oRendimientoCty.oRendimientoVolumetricoCty : new List<O_RENDIMIENTO_VOLUM_CTY>();
                        foreach (O_RENDIMIENTO_VOLUM_CTY listaCty in listaCtys)
                        {
                            if (listaCty.PRODUCTO.Contains("LIVIANA")) { reporte.xrTDeganLIVIANA.Text = listaCty.VOLUMEN; }
                            else if (listaCty.PRODUCTO.Contains("MEDIA")) { reporte.xrTDeganMEDIA.Text = listaCty.VOLUMEN; }
                            else if (listaCty.PRODUCTO.Contains("NAFTA")) { reporte.xrTDeganNAFTA.Text = listaCty.VOLUMEN; }
                        }
                    }
                    else if (oRendimientoCty.AREA_UNIDAD.Contains("NUIS"))
                    {
                        reporte.xrTNuisOperacion.Text = oRendimientoCty.OPERACION_DIA;
                        reporte.xrTNuisCargaM3.Text = oRendimientoCty.CARGA_TOTAL;
                        reporte.xrTNuisCrgaPromedio.Text = oRendimientoCty.CARGA_PROMEDIO;
                        reporte.xrTNuisCapOperac.Text = oRendimientoCty.CAPACIDAD_OPERACION;
                        reporte.xrTNuisCombLiq.Text = oRendimientoCty.COMB_LIQUIDO;
                        reporte.xrTNuisCombGas.Text = oRendimientoCty.COMB_GAS;
                        reporte.xrTNuisVaporTn.Text = oRendimientoCty.VAPOR_TN;
                        reporte.xrTNuisAguaNfr.Text = oRendimientoCty.AGUA_ENFR;
                        reporte.xrTNuisAireCom.Text = oRendimientoCty.AIRE_COMP;
                        reporte.xrTNuisCompradakWh.Text = oRendimientoCty.COMPARAD_KWH;
                        reporte.xrTNuisPropiakWh.Text = oRendimientoCty.PROPIA_KWH;
                        List<O_RENDIMIENTO_VOLUM_CTY> listaCtys = oRendimientoCty.oRendimientoVolumetricoCty != null ? oRendimientoCty.oRendimientoVolumetricoCty : new List<O_RENDIMIENTO_VOLUM_CTY>();
                        foreach (O_RENDIMIENTO_VOLUM_CTY listaCty in listaCtys)
                        {
                            if (listaCty.PRODUCTO.Contains("ISOMERADO")) { reporte.xrTNuisISOMERADO.Text = listaCty.VOLUMEN; }
                            else if (listaCty.PRODUCTO.Contains("REFINERIA")) { reporte.xrTNuisREFINERIA.Text = listaCty.VOLUMEN; }
                            else if (listaCty.PRODUCTO.Contains("LIVIANA")) { reporte.xrTNuisLIVIANA.Text = listaCty.VOLUMEN; }
                            else if (listaCty.PRODUCTO.Contains("CRUDO")) { reporte.xrTNuisCRUDO.Text = listaCty.VOLUMEN; }
                        }
                    }
                    else if (oRendimientoCty.AREA_UNIDAD.Contains("JET FUIL"))
                    {
                        reporte.xrTJetFuilOperacion.Text = oRendimientoCty.OPERACION_DIA;
                        reporte.xrTJetFuilCargaM3.Text = oRendimientoCty.CARGA_TOTAL;
                        reporte.xrTJetFuilCrgaPromedio.Text = oRendimientoCty.CARGA_PROMEDIO;
                        reporte.xrTJetFuilCapOperac.Text = oRendimientoCty.CAPACIDAD_OPERACION;
                        reporte.xrTJetFuilCombLiq.Text = oRendimientoCty.COMB_LIQUIDO;
                        reporte.xrTJetFuilCombGas.Text = oRendimientoCty.COMB_GAS;
                        reporte.xrTJetFuilVaporTn.Text = oRendimientoCty.VAPOR_TN;
                        reporte.xrTJetFuilAguaNfr.Text = oRendimientoCty.AGUA_ENFR;
                        reporte.xrTJetFuilAireCom.Text = oRendimientoCty.AIRE_COMP;
                        reporte.xrTJetFuilCompradakWh.Text = oRendimientoCty.COMPARAD_KWH;
                        reporte.xrTJetFuilPropiakWh.Text = oRendimientoCty.PROPIA_KWH;
                        List<O_RENDIMIENTO_VOLUM_CTY> listaCtys = oRendimientoCty.oRendimientoVolumetricoCty != null ? oRendimientoCty.oRendimientoVolumetricoCty : new List<O_RENDIMIENTO_VOLUM_CTY>();
                        foreach (O_RENDIMIENTO_VOLUM_CTY listaCty in listaCtys)
                        {
                            if (listaCty.PRODUCTO.Contains("DL")) { reporte.xrTJetFuiDL.Text = listaCty.VOLUMEN; }
                            else if (listaCty.PRODUCTO.Contains("DM")) { reporte.xrTJetFuiDM.Text = listaCty.VOLUMEN; }
                        }
                    }
                }
                MemoryStream stream = new MemoryStream();
                Response.Clear();
                reporte.ExportToPdf(stream);
                Response.ContentType = "application/pdf";
                Response.AddHeader("Accept-Header", stream.Length.ToString());
                Response.AddHeader("Content-Disposition", "Attachment; filename=" + "reporteRendimientoVolumetricoElrBell.pdf");
                Response.AddHeader("Content-Length", stream.Length.ToString());
                Response.BinaryWrite(stream.ToArray());
                Response.Flush();
                #endregion

            }
            else if (strgDatosSeparados[0].Contains("VILLARROEL"))
            {
                #region MyRegion

                repRendimientoVolumetricoGualvertoV reporte = new repRendimientoVolumetricoGualvertoV();
                reporte.xtcRazonSocial.Text = strgDatosSeparados[0];
                reporte.xtcGestion.Text = strgDatosSeparados[2];
                reporte.xtcPeriodo.Text = strgDatosSeparados[1];
                reporte.xrTFechaHora.Text = DateTime.Now.ToString("G");
                reporte.xrTQuienImprime.Text = Session[CVariablesSesion.DatosUsuario].ToString();
                foreach (O_RENDIMIENTO_CTY oRendimientoCty in lstDato)
                {
                    if (oRendimientoCty.AREA_UNIDAD.Contains("UDC-27250"))
                    {
                        reporte.xrTUdc27Operacion.Text = oRendimientoCty.OPERACION_DIA;
                        reporte.xrTUdc27CargaM3.Text = oRendimientoCty.CARGA_TOTAL;
                        reporte.xrTUdc27CrgaPromedio.Text = oRendimientoCty.CARGA_PROMEDIO;
                        reporte.xrTUdc27CapOperac.Text = oRendimientoCty.CAPACIDAD_OPERACION;
                        reporte.xrTUdc27CombLiq.Text = oRendimientoCty.COMB_LIQUIDO;
                        reporte.xrTUdc27CombGas.Text = oRendimientoCty.COMB_GAS;
                        reporte.xrTUdc27VaporTn.Text = oRendimientoCty.VAPOR_TN;
                        reporte.xrTUdc27AguaNfr.Text = oRendimientoCty.AGUA_ENFR;
                        reporte.xrTUdc27AireCom.Text = oRendimientoCty.AIRE_COMP;
                        reporte.xrTUdc27CompradakWh.Text = oRendimientoCty.COMPARAD_KWH;
                        reporte.xrTUdc27PropiakWh.Text = oRendimientoCty.PROPIA_KWH;
                        List<O_RENDIMIENTO_VOLUM_CTY> listaCtys = oRendimientoCty.oRendimientoVolumetricoCty != null ? oRendimientoCty.oRendimientoVolumetricoCty : new List<O_RENDIMIENTO_VOLUM_CTY>();
                        foreach (O_RENDIMIENTO_VOLUM_CTY listaCty in listaCtys)
                        {
                            if (listaCty.PRODUCTO.Contains("C4(-)")) { reporte.xrTUDC27250c4.Text = listaCty.VOLUMEN; }
                            else if (listaCty.PRODUCTO.Contains("LSR")) { reporte.xrTUDC27250LSR.Text = listaCty.VOLUMEN; }
                            else if (listaCty.PRODUCTO.Contains("MSR")) { reporte.xrTUDC27250MSR.Text = listaCty.VOLUMEN; }
                            else if (listaCty.PRODUCTO.Contains("DL")) { reporte.xrTUDC27250DL.Text = listaCty.VOLUMEN; }
                            else if (listaCty.PRODUCTO.Contains("DM")) { reporte.xrTUDC27250DM.Text = listaCty.VOLUMEN; }
                            else if (listaCty.PRODUCTO.Contains("DP")) { reporte.xrTUDC27250DP.Text = listaCty.VOLUMEN; }
                            else if (listaCty.PRODUCTO.Contains("CR")) { reporte.xrTUDC27250CR.Text = listaCty.VOLUMEN; }
                        }
                    }
                    else if (oRendimientoCty.AREA_UNIDAD.Contains("UDC-1250"))
                    {
                        reporte.xrTUdc125Operacion.Text = oRendimientoCty.OPERACION_DIA;
                        reporte.xrTUdc125CargaM3.Text = oRendimientoCty.CARGA_TOTAL;
                        reporte.xrTUdc125CrgaPromedio.Text = oRendimientoCty.CARGA_PROMEDIO;
                        reporte.xrTUdc125CapOperac.Text = oRendimientoCty.CAPACIDAD_OPERACION;
                        reporte.xrTUdc125CombLiq.Text = oRendimientoCty.COMB_LIQUIDO;
                        reporte.xrTUdc125CombGas.Text = oRendimientoCty.COMB_GAS;
                        reporte.xrTUdc125VaporTn.Text = oRendimientoCty.VAPOR_TN;
                        reporte.xrTUdc125AguaNfr.Text = oRendimientoCty.AGUA_ENFR;
                        reporte.xrTUdc125AireCom.Text = oRendimientoCty.AIRE_COMP;
                        reporte.xrTUdc125CompradakWh.Text = oRendimientoCty.COMPARAD_KWH;
                        reporte.xrTUdc125PropiakWh.Text = oRendimientoCty.PROPIA_KWH;
                        List<O_RENDIMIENTO_VOLUM_CTY> listaCtys = oRendimientoCty.oRendimientoVolumetricoCty != null ? oRendimientoCty.oRendimientoVolumetricoCty : new List<O_RENDIMIENTO_VOLUM_CTY>();
                        foreach (O_RENDIMIENTO_VOLUM_CTY listaCty in listaCtys)
                        {
                            if (listaCty.PRODUCTO.Contains("GLP")) { reporte.xrTUDC1250GLP.Text = listaCty.VOLUMEN; }
                            else if (listaCty.PRODUCTO.Contains("LSR")) { reporte.xrTUDC1250LSR.Text = listaCty.VOLUMEN; }
                            else if (listaCty.PRODUCTO.Contains("MSR")) { reporte.xrTUDC1250MSR.Text = listaCty.VOLUMEN; }
                            else if (listaCty.PRODUCTO.Contains("DO")) { reporte.xrTUDC1250DO.Text = listaCty.VOLUMEN; }
                            else if (listaCty.PRODUCTO.Contains("RED+SLOP")) { reporte.xrTUDC1250REDSLOP.Text = listaCty.VOLUMEN; }
                        }
                    }
                    else if (oRendimientoCty.AREA_UNIDAD.Contains("RECUPERACIÓN GASES"))
                    {
                        reporte.xrTRecuperacnOperacion.Text = oRendimientoCty.OPERACION_DIA;
                        reporte.xrTRecuperacnCargaM3.Text = oRendimientoCty.CARGA_TOTAL;
                        reporte.xrTRecuperacnCrgaPromedio.Text = oRendimientoCty.CARGA_PROMEDIO;
                        reporte.xrTRecuperacnCapOperac.Text = oRendimientoCty.CAPACIDAD_OPERACION;
                        reporte.xrTRecuperacnCombLiq.Text = oRendimientoCty.COMB_LIQUIDO;
                        reporte.xrTRecuperacnCombGas.Text = oRendimientoCty.COMB_GAS;
                        reporte.xrTRecuperacnVaporTn.Text = oRendimientoCty.VAPOR_TN;
                        reporte.xrTRecuperacnAguaNfr.Text = oRendimientoCty.AGUA_ENFR;
                        reporte.xrTRecuperacnAireCom.Text = oRendimientoCty.AIRE_COMP;
                        reporte.xrTRecuperacnCompradakWh.Text = oRendimientoCty.COMPARAD_KWH;
                        reporte.xrTRecuperacnPropiakWh.Text = oRendimientoCty.PROPIA_KWH;
                        List<O_RENDIMIENTO_VOLUM_CTY> listaCtys = oRendimientoCty.oRendimientoVolumetricoCty != null ? oRendimientoCty.oRendimientoVolumetricoCty : new List<O_RENDIMIENTO_VOLUM_CTY>();
                        foreach (O_RENDIMIENTO_VOLUM_CTY listaCty in listaCtys)
                        {
                            if (listaCty.PRODUCTO.Contains("GAS REF")) { reporte.xrTRecupGasGASREF.Text = listaCty.VOLUMEN; }
                            else if (listaCty.PRODUCTO.Contains("C4")) { reporte.xrTRecupGasc4.Text = listaCty.VOLUMEN; }
                            else if (listaCty.PRODUCTO.Contains("GLP")) { reporte.xrTRecupGasGLP.Text = listaCty.VOLUMEN; }
                            else if (listaCty.PRODUCTO.Contains("C3")) { reporte.xrTRecupGasC3.Text = listaCty.VOLUMEN; }
                        }
                    }
                    else if (oRendimientoCty.AREA_UNIDAD.Contains("U. REFORMACIÓN CATALITICA"))
                    {
                        reporte.xrTRefCataliticaOperacion.Text = oRendimientoCty.OPERACION_DIA;
                        reporte.xrTRefCataliticaCargaM3.Text = oRendimientoCty.CARGA_TOTAL;
                        reporte.xrTRefCataliticaCrgaPromedio.Text = oRendimientoCty.CARGA_PROMEDIO;
                        reporte.xrTRefCataliticaCapOperac.Text = oRendimientoCty.CAPACIDAD_OPERACION;
                        reporte.xrTRefCataliticaCombLiq.Text = oRendimientoCty.COMB_LIQUIDO;
                        reporte.xrTRefCataliticaCombGas.Text = oRendimientoCty.COMB_GAS;
                        reporte.xrTRefCataliticaVaporTn.Text = oRendimientoCty.VAPOR_TN;
                        reporte.xrTRefCataliticaAguaNfr.Text = oRendimientoCty.AGUA_ENFR;
                        reporte.xrTRefCataliticaAireCom.Text = oRendimientoCty.AIRE_COMP;
                        reporte.xrTRefCataliticaCompradakWh.Text = oRendimientoCty.COMPARAD_KWH;
                        reporte.xrTRefCataliticaPropiakWh.Text = oRendimientoCty.PROPIA_KWH;
                        List<O_RENDIMIENTO_VOLUM_CTY> listaCtys = oRendimientoCty.oRendimientoVolumetricoCty != null ? oRendimientoCty.oRendimientoVolumetricoCty : new List<O_RENDIMIENTO_VOLUM_CTY>();
                        foreach (O_RENDIMIENTO_VOLUM_CTY listaCty in listaCtys)
                        {
                            if (listaCty.PRODUCTO.Contains("GAS REF")) { reporte.xrTReforCatGASREF.Text = listaCty.VOLUMEN; }
                            else if (listaCty.PRODUCTO.Contains("c4")) { reporte.xrTReforCatC4.Text = listaCty.VOLUMEN; }
                            else if (listaCty.PRODUCTO.Contains("PLAT")) { reporte.xrTReforCatPLAT.Text = listaCty.VOLUMEN; }
                            //else if (listaCty.PRODUCTO.Contains("NAFTA")) { reporte.xrTReforCat.Text = listaCty.VOLUMEN; }
                        }
                    }
                    else if (oRendimientoCty.AREA_UNIDAD.Contains("(NURC) REFORMACIÓN CATALITICA"))
                    {
                        reporte.xrTNurcRefCataliticaOperacion.Text = oRendimientoCty.OPERACION_DIA;
                        reporte.xrTNurcRefCataliticaCargaM3.Text = oRendimientoCty.CARGA_TOTAL;
                        reporte.xrTNurcRefCataliticaCrgaPromedio.Text = oRendimientoCty.CARGA_PROMEDIO;
                        reporte.xrTNurcRefCataliticaCapOperac.Text = oRendimientoCty.CAPACIDAD_OPERACION;
                        reporte.xrTNurcRefCataliticaCombLiq.Text = oRendimientoCty.COMB_LIQUIDO;
                        reporte.xrTNurcRefCataliticaCombGas.Text = oRendimientoCty.COMB_GAS;
                        reporte.xrTNurcRefCataliticaVaporTn.Text = oRendimientoCty.VAPOR_TN;
                        reporte.xrTNurcRefCataliticaAguaNfr.Text = oRendimientoCty.AGUA_ENFR;
                        reporte.xrTNurcRefCataliticaAireCom.Text = oRendimientoCty.AIRE_COMP;
                        reporte.xrTNurcRefCataliticaCompradakWh.Text = oRendimientoCty.COMPARAD_KWH;
                        reporte.xrTNurcRefCataliticaPropiakWh.Text = oRendimientoCty.PROPIA_KWH;
                        List<O_RENDIMIENTO_VOLUM_CTY> listaCtys = oRendimientoCty.oRendimientoVolumetricoCty != null ? oRendimientoCty.oRendimientoVolumetricoCty : new List<O_RENDIMIENTO_VOLUM_CTY>();
                        foreach (O_RENDIMIENTO_VOLUM_CTY listaCty in listaCtys)
                        {
                            if (listaCty.PRODUCTO.Contains("GAS REF")) { reporte.xrTNurcGASREF.Text = listaCty.VOLUMEN; }
                            else if (listaCty.PRODUCTO.Contains("GLP")) { reporte.xrTNurcGLP.Text = listaCty.VOLUMEN; }
                            else if (listaCty.PRODUCTO.Contains("PLAT")) { reporte.xrTNurcPLAT.Text = listaCty.VOLUMEN; }
                        }
                    }
                    else if (oRendimientoCty.AREA_UNIDAD.Contains("REDESTILADO"))
                    {
                        reporte.xrTDestiladoOperacion.Text = oRendimientoCty.OPERACION_DIA;
                        reporte.xrTDestiladoCargaM3.Text = oRendimientoCty.CARGA_TOTAL;
                        reporte.xrTDestiladoCrgaPromedio.Text = oRendimientoCty.CARGA_PROMEDIO;
                        reporte.xrTDestiladoCapOperac.Text = oRendimientoCty.CAPACIDAD_OPERACION;
                        reporte.xrTDestiladoCombLiq.Text = oRendimientoCty.COMB_LIQUIDO;
                        reporte.xrTDestiladoCombGas.Text = oRendimientoCty.COMB_GAS;
                        reporte.xrTDestiladoVaporTn.Text = oRendimientoCty.VAPOR_TN;
                        reporte.xrTDestiladoAguaNfr.Text = oRendimientoCty.AGUA_ENFR;
                        reporte.xrTDestiladoAireCom.Text = oRendimientoCty.AIRE_COMP;
                        reporte.xrTDestiladoCompradakWh.Text = oRendimientoCty.COMPARAD_KWH;
                        reporte.xrTDestiladoPropiakWh.Text = oRendimientoCty.PROPIA_KWH;
                        List<O_RENDIMIENTO_VOLUM_CTY> listaCtys = oRendimientoCty.oRendimientoVolumetricoCty != null ? oRendimientoCty.oRendimientoVolumetricoCty : new List<O_RENDIMIENTO_VOLUM_CTY>();
                        foreach (O_RENDIMIENTO_VOLUM_CTY listaCty in listaCtys)
                        {
                            if (listaCty.PRODUCTO.Contains("FDO+CBZA")) { reporte.xrTRedistFDOCBZA.Text = listaCty.VOLUMEN; }
                            else if (listaCty.PRODUCTO.Contains("REDEST")) { reporte.xrTRedistREDEST.Text = listaCty.VOLUMEN; }
                        }
                    }
                    else if (oRendimientoCty.AREA_UNIDAD.Contains("ALQUILATOS"))
                    {
                        reporte.xrTAlquilatosOperacion.Text = oRendimientoCty.OPERACION_DIA;
                        reporte.xrTAlquilatosCargaM3.Text = oRendimientoCty.CARGA_TOTAL;
                        reporte.xrTAlquilatosCrgaPromedio.Text = oRendimientoCty.CARGA_PROMEDIO;
                        reporte.xrTAlquilatosCapOperac.Text = oRendimientoCty.CAPACIDAD_OPERACION;
                        reporte.xrTAlquilatosCombLiq.Text = oRendimientoCty.COMB_LIQUIDO;
                        reporte.xrTAlquilatosCombGas.Text = oRendimientoCty.COMB_GAS;
                        reporte.xrTAlquilatosVaporTn.Text = oRendimientoCty.VAPOR_TN;
                        reporte.xrTAlquilatosAguaNfr.Text = oRendimientoCty.AGUA_ENFR;
                        reporte.xrTAlquilatosAireCom.Text = oRendimientoCty.AIRE_COMP;
                        reporte.xrTAlquilatosCompradakWh.Text = oRendimientoCty.COMPARAD_KWH;
                        reporte.xrTAlquilatosPropiakWh.Text = oRendimientoCty.PROPIA_KWH;
                        List<O_RENDIMIENTO_VOLUM_CTY> listaCtys = oRendimientoCty.oRendimientoVolumetricoCty != null ? oRendimientoCty.oRendimientoVolumetricoCty : new List<O_RENDIMIENTO_VOLUM_CTY>();
                        foreach (O_RENDIMIENTO_VOLUM_CTY listaCty in listaCtys)
                        {
                            if (listaCty.PRODUCTO.Contains("C5")) { reporte.xrTAlquilatosc5.Text = listaCty.VOLUMEN; }
                            else if (listaCty.PRODUCTO.Contains("C6")) { reporte.xrTAlquilatosc6.Text = listaCty.VOLUMEN; }
                        }
                    }
                    else if (oRendimientoCty.AREA_UNIDAD.Contains("DEISOPENTANIZADORA"))
                    {
                        reporte.xrTDesopentanizaOperacion.Text = oRendimientoCty.OPERACION_DIA;
                        reporte.xrTDesopentanizaCargaM3.Text = oRendimientoCty.CARGA_TOTAL;
                        reporte.xrTDesopentanizaCrgaPromedio.Text = oRendimientoCty.CARGA_PROMEDIO;
                        reporte.xrTDesopentanizaCapOperac.Text = oRendimientoCty.CAPACIDAD_OPERACION;
                        reporte.xrTDesopentanizaCombLiq.Text = oRendimientoCty.COMB_LIQUIDO;
                        reporte.xrTDesopentanizaCombGas.Text = oRendimientoCty.COMB_GAS;
                        reporte.xrTDesopentanizaVaporTn.Text = oRendimientoCty.VAPOR_TN;
                        reporte.xrTDesopentanizaAguaNfr.Text = oRendimientoCty.AGUA_ENFR;
                        reporte.xrTDesopentanizaAireCom.Text = oRendimientoCty.AIRE_COMP;
                        reporte.xrTDesopentanizaCompradakWh.Text = oRendimientoCty.COMPARAD_KWH;
                        reporte.xrTDesopentanizaPropiakWh.Text = oRendimientoCty.PROPIA_KWH;
                        List<O_RENDIMIENTO_VOLUM_CTY> listaCtys = oRendimientoCty.oRendimientoVolumetricoCty != null ? oRendimientoCty.oRendimientoVolumetricoCty : new List<O_RENDIMIENTO_VOLUM_CTY>();
                        foreach (O_RENDIMIENTO_VOLUM_CTY listaCty in listaCtys)
                        {
                            if (listaCty.PRODUCTO.Contains("PESADOS")) { reporte.xrTDeisopentaPESADOS.Text = listaCty.VOLUMEN; }
                            else if (listaCty.PRODUCTO.Contains("C4")) { reporte.xrTDeisopentaC4.Text = listaCty.VOLUMEN; }
                            else if (listaCty.PRODUCTO.Contains("C5")) { reporte.xrTDeisopentaC5.Text = listaCty.VOLUMEN; }
                        }
                    }
                    else if (oRendimientoCty.AREA_UNIDAD.Contains("U. VACIO I"))
                    {
                        reporte.xrTVacio1Operacion.Text = oRendimientoCty.OPERACION_DIA;
                        reporte.xrTVacio1CargaM3.Text = oRendimientoCty.CARGA_TOTAL;
                        reporte.xrTVacio1CrgaPromedio.Text = oRendimientoCty.CARGA_PROMEDIO;
                        reporte.xrTVacio1CapOperac.Text = oRendimientoCty.CAPACIDAD_OPERACION;
                        reporte.xrTVacio1CombLiq.Text = oRendimientoCty.COMB_LIQUIDO;
                        reporte.xrTVacio1CombGas.Text = oRendimientoCty.COMB_GAS;
                        reporte.xrTVacio1VaporTn.Text = oRendimientoCty.VAPOR_TN;
                        reporte.xrTVacio1AguaNfr.Text = oRendimientoCty.AGUA_ENFR;
                        reporte.xrTVacio1AireCom.Text = oRendimientoCty.AIRE_COMP;
                        reporte.xrTVacio1CompradakWh.Text = oRendimientoCty.COMPARAD_KWH;
                        reporte.xrTVacio1PropiakWh.Text = oRendimientoCty.PROPIA_KWH;
                        List<O_RENDIMIENTO_VOLUM_CTY> listaCtys = oRendimientoCty.oRendimientoVolumetricoCty != null ? oRendimientoCty.oRendimientoVolumetricoCty : new List<O_RENDIMIENTO_VOLUM_CTY>();
                        foreach (O_RENDIMIENTO_VOLUM_CTY listaCty in listaCtys)
                        {
                            if (listaCty.PRODUCTO.Contains("CABZ")) { reporte.xrTVacio1CABZ.Text = listaCty.VOLUMEN; }
                            else if (listaCty.PRODUCTO.Contains("LAT")) { reporte.xrTVacio1LAT.Text = listaCty.VOLUMEN; }
                            else if (listaCty.PRODUCTO.Contains("FONDO")) { reporte.xrTVacio1FONDO.Text = listaCty.VOLUMEN; }
                        }
                    }
                    else if (oRendimientoCty.AREA_UNIDAD.Contains("VACIO 2"))
                    {
                        reporte.xrTVacio2Operacion.Text = oRendimientoCty.OPERACION_DIA;
                        reporte.xrTVacio2CargaM3.Text = oRendimientoCty.CARGA_TOTAL;
                        reporte.xrTVacio2CrgaPromedio.Text = oRendimientoCty.CARGA_PROMEDIO;
                        reporte.xrTVacio2CapOperac.Text = oRendimientoCty.CAPACIDAD_OPERACION;
                        reporte.xrTVacio2CombLiq.Text = oRendimientoCty.COMB_LIQUIDO;
                        reporte.xrTVacio2CombGas.Text = oRendimientoCty.COMB_GAS;
                        reporte.xrTVacio2VaporTn.Text = oRendimientoCty.VAPOR_TN;
                        reporte.xrTVacio2AguaNfr.Text = oRendimientoCty.AGUA_ENFR;
                        reporte.xrTVacio2AireCom.Text = oRendimientoCty.AIRE_COMP;
                        reporte.xrTVacio2CompradakWh.Text = oRendimientoCty.COMPARAD_KWH;
                        reporte.xrTVacio2PropiakWh.Text = oRendimientoCty.PROPIA_KWH;
                        List<O_RENDIMIENTO_VOLUM_CTY> listaCtys = oRendimientoCty.oRendimientoVolumetricoCty != null ? oRendimientoCty.oRendimientoVolumetricoCty : new List<O_RENDIMIENTO_VOLUM_CTY>();
                        foreach (O_RENDIMIENTO_VOLUM_CTY listaCty in listaCtys)
                        {
                            if (listaCty.PRODUCTO.Contains("S-10-DTL")) { reporte.xrTVacio2S10DTL.Text = listaCty.VOLUMEN; }
                            else if (listaCty.PRODUCTO.Contains("S-20-DTL")) { reporte.xrTVacio2S20DTL.Text = listaCty.VOLUMEN; }
                            else if (listaCty.PRODUCTO.Contains("S-30-DTL")) { reporte.xrTVacio2S30DTL.Text = listaCty.VOLUMEN; }
                        }
                    }
                    else if (oRendimientoCty.AREA_UNIDAD.Contains("DESFALTIZACIÓN"))
                    {
                        reporte.xrTDesasfaltizacionOperacion.Text = oRendimientoCty.OPERACION_DIA;
                        reporte.xrTDesasfaltizacionCargaM3.Text = oRendimientoCty.CARGA_TOTAL;
                        reporte.xrTDesasfaltizacionCrgaPromedio.Text = oRendimientoCty.CARGA_PROMEDIO;
                        reporte.xrTDesasfaltizacionCapOperac.Text = oRendimientoCty.CAPACIDAD_OPERACION;
                        reporte.xrTDesasfaltizacionCombLiq.Text = oRendimientoCty.COMB_LIQUIDO;
                        reporte.xrTDesasfaltizacionCombGas.Text = oRendimientoCty.COMB_GAS;
                        reporte.xrTDesasfaltizacionVaporTn.Text = oRendimientoCty.VAPOR_TN;
                        reporte.xrTDesasfaltizacionAguaNfr.Text = oRendimientoCty.AGUA_ENFR;
                        reporte.xrTDesasfaltizacionAireCom.Text = oRendimientoCty.AIRE_COMP;
                        reporte.xrTDesasfaltizacionCompradakWh.Text = oRendimientoCty.COMPARAD_KWH;
                        reporte.xrTDesasfaltizacionPropiakWh.Text = oRendimientoCty.PROPIA_KWH;
                        List<O_RENDIMIENTO_VOLUM_CTY> listaCtys = oRendimientoCty.oRendimientoVolumetricoCty != null ? oRendimientoCty.oRendimientoVolumetricoCty : new List<O_RENDIMIENTO_VOLUM_CTY>();
                        foreach (O_RENDIMIENTO_VOLUM_CTY listaCty in listaCtys)
                        {
                            if (listaCty.PRODUCTO.Contains("STOCK")) { reporte.xrTDesasfaltizacionSTOCK.Text = listaCty.VOLUMEN; }
                            else if (listaCty.PRODUCTO.Contains("ASFALTO")) { reporte.xrTDesasfaltizacionASFALTO.Text = listaCty.VOLUMEN; }
                        }
                    }
                    else if (oRendimientoCty.AREA_UNIDAD.Contains("FURFURAL"))
                    {
                        reporte.xrTRefFurfuralOperacion.Text = oRendimientoCty.OPERACION_DIA;
                        reporte.xrTRefFurfuralCargaM3.Text = oRendimientoCty.CARGA_TOTAL;
                        reporte.xrTRefFurfuralCrgaPromedio.Text = oRendimientoCty.CARGA_PROMEDIO;
                        reporte.xrTRefFurfuralCapOperac.Text = oRendimientoCty.CAPACIDAD_OPERACION;
                        reporte.xrTRefFurfuralCombLiq.Text = oRendimientoCty.COMB_LIQUIDO;
                        reporte.xrTRefFurfuralCombGas.Text = oRendimientoCty.COMB_GAS;
                        reporte.xrTRefFurfuralVaporTn.Text = oRendimientoCty.VAPOR_TN;
                        reporte.xrTRefFurfuralAguaNfr.Text = oRendimientoCty.AGUA_ENFR;
                        reporte.xrTRefFurfuralAireCom.Text = oRendimientoCty.AIRE_COMP;
                        reporte.xrTRefFurfuralCompradakWh.Text = oRendimientoCty.COMPARAD_KWH;
                        reporte.xrTRefFurfuralPropiakWh.Text = oRendimientoCty.PROPIA_KWH;
                        List<O_RENDIMIENTO_VOLUM_CTY> listaCtys = oRendimientoCty.oRendimientoVolumetricoCty != null ? oRendimientoCty.oRendimientoVolumetricoCty : new List<O_RENDIMIENTO_VOLUM_CTY>();
                        foreach (O_RENDIMIENTO_VOLUM_CTY listaCty in listaCtys)
                        {
                            if (listaCty.PRODUCTO.Contains("S-10-REF")) { reporte.xrTRefFurfuS10REF.Text = listaCty.VOLUMEN; }
                            else if (listaCty.PRODUCTO.Contains("S-20-REF")) { reporte.xrTRefFurfuS20REF.Text = listaCty.VOLUMEN; }
                            else if (listaCty.PRODUCTO.Contains("S-30-REF")) { reporte.xrTRefFurfuS30REF.Text = listaCty.VOLUMEN; }
                            else if (listaCty.PRODUCTO.Contains("BS-REF")) { reporte.xrTRefFurfuBSREF.Text = listaCty.VOLUMEN; }
                        }
                    }
                    else if (oRendimientoCty.AREA_UNIDAD.Contains("DESPARAFINACIÓN"))
                    {
                        reporte.xrTDesparafinacionOperacion.Text = oRendimientoCty.OPERACION_DIA;
                        reporte.xrTDesparafinacionCargaM3.Text = oRendimientoCty.CARGA_TOTAL;
                        reporte.xrTDesparafinacionCrgaPromedio.Text = oRendimientoCty.CARGA_PROMEDIO;
                        reporte.xrTDesparafinacionCapOperac.Text = oRendimientoCty.CAPACIDAD_OPERACION;
                        reporte.xrTDesparafinacionCombLiq.Text = oRendimientoCty.COMB_LIQUIDO;
                        reporte.xrTDesparafinacionCombGas.Text = oRendimientoCty.COMB_GAS;
                        reporte.xrTDesparafinacionVaporTn.Text = oRendimientoCty.VAPOR_TN;
                        reporte.xrTDesparafinacionAguaNfr.Text = oRendimientoCty.AGUA_ENFR;
                        reporte.xrTDesparafinacionAireCom.Text = oRendimientoCty.AIRE_COMP;
                        reporte.xrTDesparafinacionCompradakWh.Text = oRendimientoCty.COMPARAD_KWH;
                        reporte.xrTDesparafinacionPropiakWh.Text = oRendimientoCty.PROPIA_KWH;
                        List<O_RENDIMIENTO_VOLUM_CTY> listaCtys = oRendimientoCty.oRendimientoVolumetricoCty != null ? oRendimientoCty.oRendimientoVolumetricoCty : new List<O_RENDIMIENTO_VOLUM_CTY>();
                        foreach (O_RENDIMIENTO_VOLUM_CTY listaCty in listaCtys)
                        {
                            if (listaCty.PRODUCTO.Contains("S-10-DES")) { reporte.xrTDesparaS10DES.Text = listaCty.VOLUMEN; }
                            else if (listaCty.PRODUCTO.Contains("S-20-DES")) { reporte.xrTDesparaS20DES.Text = listaCty.VOLUMEN; }
                            else if (listaCty.PRODUCTO.Contains("S-30-DES")) { reporte.xrTDesparaS30DES.Text = listaCty.VOLUMEN; }
                            else if (listaCty.PRODUCTO.Contains("BS-DES")) { reporte.xrTDesparaBSDES.Text = listaCty.VOLUMEN; }
                        }
                    }
                    else if (oRendimientoCty.AREA_UNIDAD.Contains("DESACEITADO"))
                    {
                        reporte.xrTDesaseitadoOperacion.Text = oRendimientoCty.OPERACION_DIA;
                        reporte.xrTDesaseitadoCargaM3.Text = oRendimientoCty.CARGA_TOTAL;
                        reporte.xrTDesaseitadoCrgaPromedio.Text = oRendimientoCty.CARGA_PROMEDIO;
                        reporte.xrTDesaseitadoCapOperac.Text = oRendimientoCty.CAPACIDAD_OPERACION;
                        reporte.xrTDesaseitadoCombLiq.Text = oRendimientoCty.COMB_LIQUIDO;
                        reporte.xrTDesaseitadoCombGas.Text = oRendimientoCty.COMB_GAS;
                        reporte.xrTDesaseitadoVaporTn.Text = oRendimientoCty.VAPOR_TN;
                        reporte.xrTDesaseitadoAguaNfr.Text = oRendimientoCty.AGUA_ENFR;
                        reporte.xrTDesaseitadoAireCom.Text = oRendimientoCty.AIRE_COMP;
                        reporte.xrTDesaseitadoCompradakWh.Text = oRendimientoCty.COMPARAD_KWH;
                        reporte.xrTDesaseitadoPropiakWh.Text = oRendimientoCty.PROPIA_KWH;
                        List<O_RENDIMIENTO_VOLUM_CTY> listaCtys = oRendimientoCty.oRendimientoVolumetricoCty != null ? oRendimientoCty.oRendimientoVolumetricoCty : new List<O_RENDIMIENTO_VOLUM_CTY>();
                        foreach (O_RENDIMIENTO_VOLUM_CTY listaCty in listaCtys)
                        {
                            if (listaCty.PRODUCTO.Contains("PARAFINA")) { reporte.xrTDestadPaPARAFINA.Text = listaCty.VOLUMEN; }
                            else if (listaCty.PRODUCTO.Contains("ACEITE")) { reporte.xrTDestadPaACEITE.Text = listaCty.VOLUMEN; }
                        }
                    }
                    else if (oRendimientoCty.AREA_UNIDAD.Contains("HIDROTERMINADO"))
                    {
                        reporte.xrTHidroterminadoOperacion.Text = oRendimientoCty.OPERACION_DIA;
                        reporte.xrTHidroterminadoCargaM3.Text = oRendimientoCty.CARGA_TOTAL;
                        reporte.xrTHidroterminadoCrgaPromedio.Text = oRendimientoCty.CARGA_PROMEDIO;
                        reporte.xrTHidroterminadoCapOperac.Text = oRendimientoCty.CAPACIDAD_OPERACION;
                        reporte.xrTHidroterminadoCombLiq.Text = oRendimientoCty.COMB_LIQUIDO;
                        reporte.xrTHidroterminadoCombGas.Text = oRendimientoCty.COMB_GAS;
                        reporte.xrTHidroterminadoVaporTn.Text = oRendimientoCty.VAPOR_TN;
                        reporte.xrTHidroterminadoAguaNfr.Text = oRendimientoCty.AGUA_ENFR;
                        reporte.xrTHidroterminadoAireCom.Text = oRendimientoCty.AIRE_COMP;
                        reporte.xrTHidroterminadoCompradakWh.Text = oRendimientoCty.COMPARAD_KWH;
                        reporte.xrTHidroterminadoPropiakWh.Text = oRendimientoCty.PROPIA_KWH;
                        List<O_RENDIMIENTO_VOLUM_CTY> listaCtys = oRendimientoCty.oRendimientoVolumetricoCty != null ? oRendimientoCty.oRendimientoVolumetricoCty : new List<O_RENDIMIENTO_VOLUM_CTY>();
                        foreach (O_RENDIMIENTO_VOLUM_CTY listaCty in listaCtys)
                        {
                            if (listaCty.PRODUCTO.Contains("S-10-BA")) { reporte.xrTHidroTerminS10BA.Text = listaCty.VOLUMEN; }
                            else if (listaCty.PRODUCTO.Contains("S-20-BA")) { reporte.xrTHidroTerminS20BA.Text = listaCty.VOLUMEN; }
                            else if (listaCty.PRODUCTO.Contains("S-30-BA")) { reporte.xrTHidroTerminS30BA.Text = listaCty.VOLUMEN; }
                            else if (listaCty.PRODUCTO.Contains("BS-BA")) { reporte.xrTHidroTerminBSBA.Text = listaCty.VOLUMEN; }
                        }
                    }
                }
                MemoryStream stream = new MemoryStream();
                Response.Clear();
                reporte.ExportToPdf(stream);
                Response.ContentType = "application/pdf";
                Response.AddHeader("Accept-Header", stream.Length.ToString());
                Response.AddHeader("Content-Disposition", "Attachment; filename=" + "repRendimientoVolumetricoGualvertoVilla.pdf");
                Response.AddHeader("Content-Length", stream.Length.ToString());
                Response.BinaryWrite(stream.ToArray());
                Response.Flush();

                #endregion

            }
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
    }
}