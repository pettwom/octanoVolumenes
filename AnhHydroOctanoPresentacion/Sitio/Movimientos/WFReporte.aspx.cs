using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Web;
using AnhHydroOctanoPresentacion.Enumeradores;
using AnhHydroOctanoPresentacion.Parametros;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using Librerias.Anh.Us;
using AnhHydroOctanoPresentacion.Sitio.Reportes;

namespace AnhHydroOctanoPresentacion.Sitio.Movimientos
{
    public partial class WFReporte : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                #region Proceso a Ejecutar

                if (Session[CVariablesSesion.ReportePrincipal] != null && Session[CVariablesSesion.TipoReporte] != null)
                {
                    var enuTipo = (EnumTipoReporte)Session[CVariablesSesion.TipoReporte];
                    XtraReport repPrincipal, repSecundario, repTercero, repCuarto, repFinal = new XtraReport();
                    var listReport = new List<XtraReport>();
                    string strRepNombre;
                    
                    switch (enuTipo)
                    {
                        case EnumTipoReporte.ConstanciaEnvio:
                            strRepNombre = "constanciaEnvio";
                            repPrincipal = Session[CVariablesSesion.ReportePrincipal] as repConstanciaEnvio;
                            listReport.Add(repPrincipal);
                            break;
                        case EnumTipoReporte.DeclaracionJurada:
                            strRepNombre = "declaracionJurada";
                            repPrincipal = Session[CVariablesSesion.ReportePrincipal] as repDjAcumuladoInfra;
                            listReport.Add(repPrincipal);
                            if (Session[CVariablesSesion.ReporteSecundario] != null)
                            {
                                repSecundario = Session[CVariablesSesion.ReporteSecundario] as repDjInternacional;
                                listReport.Add(repSecundario);
                            }
                            if (Session[CVariablesSesion.ReporteTercero] != null)
                            {
                                repTercero = Session[CVariablesSesion.ReporteTercero] as repDjContenedor;
                                listReport.Add(repTercero);
                            }
                            if (Session[CVariablesSesion.ReporteCuarto] != null)
                            {
                                repCuarto = Session[CVariablesSesion.ReporteCuarto] as repDjRecaudoExterno;
                                listReport.Add(repCuarto);
                            }
                            break;
                        case EnumTipoReporte.DeclaracionJuradaEgreso:
                            strRepNombre = "declaracionJurada";
                            repPrincipal = Session[CVariablesSesion.ReportePrincipal] as repDjAcumuladoEgreso;
                            listReport.Add(repPrincipal);
                            if (Session[CVariablesSesion.ReporteSecundario] != null)
                            {
                                repSecundario = Session[CVariablesSesion.ReporteSecundario] as repDjInternacional;
                                listReport.Add(repSecundario);
                            }
                            if (Session[CVariablesSesion.ReporteTercero] != null)
                            {
                                repTercero = Session[CVariablesSesion.ReporteTercero] as repDjContenedor;
                                listReport.Add(repTercero);
                            }
                            if (Session[CVariablesSesion.ReporteCuarto] != null)
                            {
                                repCuarto = Session[CVariablesSesion.ReporteCuarto] as repDjRecaudoExterno;
                                listReport.Add(repCuarto);
                            }
                            break;
                        case EnumTipoReporte.DdjjConsolidaSuic:
                            strRepNombre = "decJuradaCnsSuic";
                            repPrincipal = Session[CVariablesSesion.ReportePrincipal] as repDjConsolidacion;
                            listReport.Add(repPrincipal);
                            break;
                        case EnumTipoReporte.Intimacion:
                            strRepNombre = "intimacion";
                            repPrincipal = Session[CVariablesSesion.ReportePrincipal] as repIntimacion;
                            listReport.Add(repPrincipal);
                            break;
                        case EnumTipoReporte.Ampliacion:
                            strRepNombre = "ampliacion";
                            repPrincipal = Session[CVariablesSesion.ReportePrincipal] as repAmpliacion;
                            listReport.Add(repPrincipal);
                            break;
                        case EnumTipoReporte.RepCumplimientoMes:
                            strRepNombre = "RepCumplimientoMensual";
                            repPrincipal = Session[CVariablesSesion.ReportePrincipal] as repCumplimientoMes;
                            listReport.Add(repPrincipal);
                            break;
                        case EnumTipoReporte.RepDosificacion:
                            strRepNombre = "RepDosificacion";
                            repPrincipal = Session[CVariablesSesion.ReportePrincipal] as repDosificacion;
                            listReport.Add(repPrincipal);
                            break;
                        case EnumTipoReporte.RepHistorialTarjeta:
                            strRepNombre = "RepHistorialTarjeta";
                            repPrincipal = Session[CVariablesSesion.ReportePrincipal] as repHistorialTarjeta;
                            listReport.Add(repPrincipal);
                            break;
                        case EnumTipoReporte.RepRecepciones:
                            strRepNombre = "RepRecepciones";
                            repPrincipal = Session[CVariablesSesion.ReportePrincipal] as repRecepciones;
                            listReport.Add(repPrincipal);
                            break;
                        case EnumTipoReporte.RepCompInventario:
                            strRepNombre = "RepComparaInventario";
                            repPrincipal = Session[CVariablesSesion.ReportePrincipal] as repInventarioFisDoc;
                            listReport.Add(repPrincipal);
                            break;
                        case EnumTipoReporte.RepKardex:
                            strRepNombre = "repKardex";
                            repPrincipal = Session[CVariablesSesion.ReportePrincipal] as repKardex;
                            listReport.Add(repPrincipal);
                            break;
                        case EnumTipoReporte.RepRegistroVolRef:
                            strRepNombre = "RegistroVolRef";
                            repPrincipal = Session[CVariablesSesion.ReportePrincipal] as repRegistroVolRef;
                            listReport.Add(repPrincipal);
                            break;
                        case EnumTipoReporte.RepConsumoMtr:
                            strRepNombre = "RepConsumoMtr";
                            repPrincipal = Session[CVariablesSesion.ReportePrincipal] as repConsumoMtr;
                            listReport.Add(repPrincipal);
                            break;
                        case EnumTipoReporte.repDjImportacion:
                            strRepNombre = "RepDjImportacion";
                            repPrincipal = Session[CVariablesSesion.ReportePrincipal] as repDjImportadores;
                            listReport.Add(repPrincipal);
                            break;
                        case EnumTipoReporte.repDjImportacionFosil:
                            strRepNombre = "RepDjImportacionFosil";
                            repPrincipal = Session[CVariablesSesion.ReportePrincipal] as repDjImportadoresFosil;
                            listReport.Add(repPrincipal);
                            break;
                        case EnumTipoReporte.repDjCertificadoImportacion:
                            strRepNombre = "RepCertificadoImportacion";
                            repPrincipal = Session[CVariablesSesion.ReportePrincipal] as repDjCertificadoImportacion;
                            listReport.Add(repPrincipal);
                            break;
                        case EnumTipoReporte.repNotaDespachoAviacion:
                            strRepNombre = "RepNotaDespachoAviacion";
                            repPrincipal = Session[CVariablesSesion.ReportePrincipal] as repNotaDespachoAviacion;
                            listReport.Add(repPrincipal);
                            break;
                        case EnumTipoReporte.repRank50:
                            strRepNombre = "RepCarguios" + DateTime.Now;
                            repPrincipal = Session[CVariablesSesion.ReportePrincipal] as repRank50;
                            listReport.Add(repPrincipal);
                            break;
                        default:
                            return;
                    }

                    foreach (var repo in listReport)
                    {
                        repo.CreateDocument();
                        repFinal.Pages.AddRange(repo.Pages);
                    }

                    //Descargar Archivo
                    using (var memoryStream = new MemoryStream())
                    {
                        repFinal.ExportOptions.Pdf.ConvertImagesToJpeg = true;
                        repFinal.ExportOptions.Pdf.ImageQuality = PdfJpegImageQuality.High;
                        repFinal.ExportToPdf(memoryStream);
                        Response.Clear();
                        Response.Buffer = false;
                        Response.AppendHeader("Content-Type", "application/pdf");
                        Response.AppendHeader("Content-Transfer-Encoding", "binary");
                        Response.AppendHeader("Content-Disposition", "attachment; filename=" + strRepNombre + ".pdf");
                        Response.BinaryWrite(memoryStream.GetBuffer());
                        Response.Flush();
                    }
                    
                    //Visualizar PDF
                    //    using (MemoryStream ms = new MemoryStream())
                    //    {
                    //        repGenerico.CreateDocument();
                    //        PdfExportOptions opts = new PdfExportOptions();
                    //        opts.ShowPrintDialogOnOpen = false;

                    //        repGenerico.ExportToPdf(ms, opts);
                    //        ms.Seek(0, SeekOrigin.Begin);
                    //        byte[] report = ms.ToArray();
                    //        Page.Response.ContentType = "application/pdf";
                    //        Page.Response.Clear();
                    //        Page.Response.OutputStream.Write(report, 0, report.Length);
                    //    }
                }
                else
                {
                    //todo No implementado.
                }

                #endregion
            }
            catch (Exception exp)
            {
                #region RegistraError

                var oMensajeUsuario = new CLogTraza.MensajeUsuario
                {
                    decUsuarioId = Session[CVariablesSesion.IdUsuario],
                    decIdModulo = CParametrosHydro.decIdAplicacion,
                    strAccion = GetType().Name + "." + MethodBase.GetCurrentMethod().Name,
                    strIp = HttpContext.Current.Request.UserHostAddress,
                    decNivelCapa = (int)CLogTraza.CapasNivel.Presentacion
                };
                CLogTraza.Error(oMensajeUsuario, exp);

                #endregion
            }
        }
    }
}