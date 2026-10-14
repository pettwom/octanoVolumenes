using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Serialization;

namespace AnhHydroOctanoPresentacion.Controles
{
    using System.IO;
    using DevExpress.Export;
    using DevExpress.Utils;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrintingLinks;
    using System;
    using ServiceStack.ServiceClient.Web;
    using System.Drawing;
    using System.Linq;
    using System.Reflection;
    using System.Web;
    using System.Web.UI;
    using AnhHydroOctanoPersistencia.Aplicacion.Consulta;
    using AnhHydroOctanoPersistencia.Entidades;
    using Entidades;
    using Enumeradores;
    using Parametros;
    using DevExpress.Web;
    using Librerias.Anh.Us;
    using UserControl = System.Web.UI.UserControl;
    using Page = System.Web.UI.Page;
    using ServiceStack.Text;
    using Clases.Util;
    public partial class CtrRepSuicDdjj : UserControl
    {
        #region Variables de entorno

        private readonly CPersistenciaConsulta _consulta = new CPersistenciaConsulta();
        readonly JsonServiceClient _client = new JsonServiceClient(CAppSettings.servicio_octVolumen);
        readonly CTrazaInf _cTrazaInf = new CTrazaInf();
        #endregion

        #region Eventos de Formulario

        protected void Page_Load(object sender, EventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    if (!Page.IsCallback && !Page.IsPostBack)
                    {
                        Session.Remove(CVariablesSesion.ComboTiposOperacion);
                        Session.Remove(CVariablesSesion.GrillaCabecera);

                        MConfiguraInicio();
                    }
                    else
                    {
                        //todo No es carga inicial: no implementado
                    }

                    if (pvgReporte.DataSource == null && Session[CVariablesSesion.GrillaCabecera] != null)
                    {
                        pvgReporte.DataSource = Session[CVariablesSesion.GrillaCabecera];
                        pvgReporte.DataBind();
                    }
                    else
                    {
                        //todo grilla vacia: no implementado
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
            else
            {
                //todo No implementado.
            }
        }

        protected void pnlDatos_Callback(object sender, CallbackEventArgsBase e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    MLimpiaGrilla();

                    var strMensaje = string.Empty;
                    if (MValidaDatosIniciales(ref strMensaje))
                    {
                        var strFechaIni = detFechaIni.Value + "000000";
                        var strFechaFin = detFechaFin.Value + "235959";
                        var strIdDepto = cmbDepartamento.Value.ToString();

                        MCargaGrilla(strFechaIni, strFechaFin, strIdDepto);
                    }
                    else
                    {
                        MImprimirMensaje(strMensaje, EnumTiposMensajeAlerta.Error);
                    }

                    #endregion
                }
                catch (Exception exp)
                {
                    #region RegistraError

                    MLimpiaGrilla();
                    MImprimirMensaje(exp.Message, EnumTiposMensajeAlerta.Error);
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
            else
            {
                //todo No implementado.
            }
        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    switch ((int)cmbTipoExport.Value)
                    {
                        case 1:
                            var options = new XlsxExportOptionsEx()
                            {
                                ExportType = ExportType.DataAware,
                                AllowGrouping = DefaultBoolean.False,
                                TextExportMode = TextExportMode.Value,
                                AllowFixedColumns = DefaultBoolean.False,
                                AllowFixedColumnHeaderPanel = DefaultBoolean.True,
                                RawDataMode = false
                            };
                            pgeExportar.ExportXlsxToResponse("ConsolidadoSuicDdjj", options);
                            break;
                        case 2:
                            var ps = new PrintingSystem();
                            var link1 = new PrintableComponentLink();
                            link1.Component = pgeExportar;
                            link1.PrintingSystem = ps;
                            link1.Margins.Top = 1;
                            link1.Margins.Left = 1;

                            ////adjuntar imagen
                            //PrintableComponentLink link2 = new PrintableComponentLink();
                            //WebChartControl1.DataBind();
                            //link2.Component = ((IChartContainer)WebChartControl1).Chart;
                            //link2.PrintingSystem = ps;

                            var compositeLink = new CompositeLink();
                            compositeLink.Links.AddRange(new object[] { link1 });
                            compositeLink.PrintingSystem = ps;
                            compositeLink.CreateDocument();
                            ps.Document.AutoFitToPagesWidth = 1;

                            compositeLink.PrintingSystem.ExportOptions.Pdf.DocumentOptions.Author = "ANH.GOB.BO";
                            using (var stream = new MemoryStream())
                            {
                                compositeLink.PrintingSystem.ExportToPdf(stream);
                                Response.Clear();
                                Response.Buffer = false;
                                Response.AppendHeader("Content-Type", "application/pdf");
                                Response.AppendHeader("Content-Transfer-Encoding", "binary");
                                Response.AppendHeader("Content-Disposition", "attachment; filename=ConsolidadoSuicDdjj.pdf");
                                Response.BinaryWrite(stream.GetBuffer());
                                Response.End();
                            }
                            ps.Dispose();
                            break;
                        case 3:
                            string fileName = "RptSuicDetDecTodoPlano";

                            GridView dg = new GridView();
                            dg.AllowPaging = false;

                            //Session[CVariablesSesion.GrillaCabecera]

                            var listAux = (List<O_MOV_DETALLE_DDJJ_CTY>)pvgReporte.DataSource;

                            //listAux = listAux.Where(x => x.IDENTIFICADOR != "4679EFT").ToList();
                            //listAux = listAux.Take(15000).ToList();

                            //List<O_MOV_DETALLE_DDJJ_CTY> listAux = new List<O_MOV_DETALLE_DDJJ_CTY> (Session[CVariablesSesion.GrillaCabecera]);
                            //listAux = listAux.FirstOrDefault();
                            //List<O_MOV_DETALLE_DDJJ_CTY> listaReporte = new List<O_MOV_DETALLE_DDJJ_CTY>();

                            //listaReporte = (List<O_MOV_DETALLE_DDJJ_CTY>) Session[CVariablesSesion.GrillaCabecera];
                            //listaReporte = listaReporte.Where(x=>);

                            /*foreach (O_REPORTE_CILINDROS_GLP_CTY oCilidroGlpCty in listAux)
                            {
                                listaReporte.Add(new O_REPORTE_AUX_CILINDROS_GLP_CTY
                                {
                                    ID_ITEM= oCilidroGlpCty.ID_ITEM,
                                    FECHA_OPERACION = CFechas.ConvierteLongDateTime(oCilidroGlpCty.FECHA_OPERACION),
                                    TARA = oCilidroGlpCty.TARA,
                                    ID_OPERACION = oCilidroGlpCty.ID_OPERACION,
                                    OPERACION = oCilidroGlpCty.OPERACION,
                                    ID_ESTADO = oCilidroGlpCty.ID_ESTADO,
                                    ESTADO = oCilidroGlpCty.ESTADO,
                                    ID_CODIGO = oCilidroGlpCty.ID_CODIGO,
                                    CODIGO = oCilidroGlpCty.CODIGO,
                                    SERIE = oCilidroGlpCty.SERIE,
                                    ID_TIPO = oCilidroGlpCty.ID_TIPO,
                                    TIPO = oCilidroGlpCty.TIPO,
                                    ID_FABRICANTE= oCilidroGlpCty.ID_FABRICANTE,
                                    FABRICANTE = oCilidroGlpCty.FABRICANTE,
                                    IMPORTADOR = oCilidroGlpCty.IMPORTADOR,
                                    ID_UNIDAD_MEDIDA = oCilidroGlpCty.ID_UNIDAD_MEDIDA,
                                    UNIDAD_MEDIDA = oCilidroGlpCty.UNIDAD_MEDIDA,
                                    FECHA_FABRICACION = CFechas.ConvierteLongDateTime(oCilidroGlpCty.FECHA_FABRICACION),
                                    FECHA_REGISTRO = CFechas.ConvierteLongDateTime(oCilidroGlpCty.FECHA_REGISTRO)
                                });
                            }*/


                            //dg.DataSource = listAux;
                            //dg.DataBind();

                            //Response.Buffer = true;
                            //Response.Clear();
                            //Response.ClearContent();
                            //Response.ClearHeaders();
                            //Response.ContentType = "application/" + ".xls";
                            //Response.AddHeader("content-disposition", "attachment;filename= " + fileName + ".xls");

                            //var physicalExportFilePath = Path.Combine("d:\\", "xxxz");
                            //var sw = new StreamWriter(physicalExportFilePath, false, Encoding.UTF8);
                            //var hw = new HtmlTextWriter(sw);

                            //for (int i = 0; i < dg.Rows.Count; i++)
                            //{
                            //    //Apply text style to each Row
                            //    dg.Rows[i].Attributes.Add("class", "textmode");
                            //}

                            //dg.RenderControl(hw);



                            ////StringWriter stringWriter = new StringWriter();
                            ////XmlTextWriter writer = new XmlTextWriter(stringWriter);

                            ////writer.Formatting = Formatting.Indented;

                            ////XmlSerializer serializer = new XmlSerializer(sw);

                            ////serializer.Serialize(writer, sw);
                            ////writer.Flush();
                            ////string XmlizedString = stringWriter.ToString();
                            ////stringWriter.Close();
                            ////writer.Close();
                            ////return XmlizedString;


                            ////style to format numbers to string
                            //const string style = @"<style> .textmode { mso-number-format:\@; } </style>";
                            //Response.Write(style);
                            //Response.Output.Write(sw.ToString());
                            //Response.Flush();
                            //Response.SuppressContent = true;
                            //HttpContext.Current.ApplicationInstance.CompleteRequest();

                            var listAux1 = new List<O_MOV_DETALLE_DDJJ_CTY>();
                            listAux1 = (List<O_MOV_DETALLE_DDJJ_CTY>)Session[CVariablesSesion.GrillaCabecera];

                            var list = listAux1;//(List<O_MOV_DETALLE_DDJJ_CTY>)objResultado.oResultado;
                            if (list == null || list.Count == 0) return;

                            Type tipo = list[0].GetType();
                            string newLine = Environment.NewLine;

                            MemoryStream ms1 = new MemoryStream();
                            using (var sw = new StreamWriter(ms1))
                            {
                                object o = Activator.CreateInstance(tipo);
                                PropertyInfo[] props = o.GetType().GetProperties();
                                //sw.Write(string.Join(";", props.Select(d => d.Name).ToArray()) + newLine); // agregar cabecera

                                foreach (O_MOV_DETALLE_DDJJ_CTY item in list)
                                {
                                    var row = string.Join(";", props.Select(d => item.GetType()
                                                                                    .GetProperty(d.Name)
                                                                                    .GetValue(item, null)
                                                                                    .ToString())
                                                                            .ToArray());
                                    sw.Write(row + newLine);
                                }
                                sw.Flush();
                                byte[] bytes1 = ms1.ToArray();
                                ms1.Close();

                                Response.Clear();
                                Response.ContentType = "application/force-download";
                                Response.AddHeader("content-disposition", "attachment;    filename=prueba.csv");
                                Response.BinaryWrite(bytes1);
                                Response.End();
                            }




                            break;
                    }
                    if ((int)cmbTipoExport.Value == 1)
                    {

                    }
                    else
                    {

                    }

                    #endregion
                }
                catch (Exception exp)
                {
                    #region RegistraError

                    MImprimirMensaje(exp.Message, EnumTiposMensajeAlerta.Error);

                    #endregion
                }
            }
            else
            {
                //todo no implementado
            }
        }



        #endregion

        #region Metodos y funciones

        /// <summary>
        /// METODO. MValidaSesion
        /// </summary>
        /// <returns></returns>
        private bool MValidaSesion()
        {
            bool booResultado;
            if (Session[CVariablesSesion.IdUsuario] == null)
            {
                try
                {
                    Response.Redirect("~/Sitio/Persona/wfAutenticacion.aspx", false);
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                }
                catch (Exception)
                {
                    string script = string.Format("document.location.href = '{0}');", "~/Sitio/Persona/wfAutenticacion.aspx");
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "redirect", script, true);
                }
                booResultado = false;
            }
            else
            {
                booResultado = true;
            }
            return booResultado;
        }

        /// <summary>
        /// METODO. MConfiguraInicio
        /// </summary>
        private void MConfiguraInicio()
        {
           // detFechaIni.Date = DateTime.Now.AddMonths(-1);
           // detFechaFin.Date = DateTime.Now.AddMonths(-1);
        }

        /// <summary>
        /// METODO. MCargaGrilla
        /// </summary>
        /// <param name="strFechaIni"></param>
        /// <param name="strFechaFin"></param>
        /// <param name="strIdActividad"></param>
        /// <param name="strIdDepto"></param>
        private void MCargaGrilla(string strFechaIni, string strFechaFin, string strIdDepto)
        {
            var objConsulta = new EObjetoEnvio9ParametrosProto
            {
                strCredencial = CParametrosHydro.strCredencialFuncionario,
                decFiltro = 1, //Filtro 1
                strParametro1 = strFechaIni,
                strParametro2 = strFechaFin,
                strParametro3 = strIdDepto,
                decParametro4 = Convert.ToInt32(CAppSettings.idOperacionSuic)
            };
            var objResultado = _consulta.ReporteSuicDdjj(objConsulta);
            if (objResultado.intCodigo == 1)
            {
                //var lstAcumulado = (List<O_LISTADO_DDJJ_CTY>)objResultado.oResultado;
                pvgReporte.DataSource = objResultado.oResultado;
                pvgReporte.DataBind();
                Session[CVariablesSesion.GrillaCabecera] = objResultado.oResultado;

                #region Traza consulta de informacion
                string strAccion = JsonSerializer.SerializeToString(objConsulta);
                string strSalida = ((List<O_LISTA_REPORTE_SUIC_CTY>)objResultado.oResultado).Count().ToString();
                string mensaje = GetType().Name + "." + MethodBase.GetCurrentMethod().Name;
                _cTrazaInf.MRegistraTrazaInf(strAccion, mensaje, CParametrosHydro.decIdAplicacion, Convert.ToDecimal(Session[CVariablesSesion.IdUsuario].ToString()), HttpContext.Current.Request.UserHostAddress, strSalida);
                #endregion  
            }
            else
            {
                MImprimirMensaje(objResultado.strMensaje, EnumTiposMensajeAlerta.Alerta);
            }
        }

        /// <summary>
        /// METODO. MValidaDatosIniciales
        /// </summary>
        /// <param name="strMensaje"></param>
        /// <returns></returns>
        private bool MValidaDatosIniciales(ref string strMensaje)
        {
            var booResultado = true;

            if (detFechaIni.Text == "" || detFechaFin.Text == "")
            {
                strMensaje = "Introduzca fechas validas.";
                booResultado = false;
            }
            return booResultado;
        }

        /// <summary>
        /// METODO. MLimpiaGrilla
        /// </summary>
        private void MLimpiaGrilla()
        {
            Session.Remove(CVariablesSesion.GrillaCabecera);
            pvgReporte.DataSource = null;
            pvgReporte.DataBind();

            MImprimirMensaje("", EnumTiposMensajeAlerta.Limpiar);
        }

        /// <summary>
        /// METODO. MImprimirMensaje
        /// </summary>
        /// <param name="strMensaje"></param>
        /// <param name="tipoMensaje"></param>
        private void MImprimirMensaje(string strMensaje, EnumTiposMensajeAlerta tipoMensaje)
        {
            switch (tipoMensaje)
            {
                case EnumTiposMensajeAlerta.Error:
                    pnlMensaje.Visible = true;
                    pnlMensaje.BorderColor = Color.DarkRed;
                    pnlMensaje.BackColor = Color.Pink;
                    strMensaje = "ERROR: " + strMensaje;
                    break;
                case EnumTiposMensajeAlerta.Alerta:
                    pnlMensaje.Visible = true;
                    pnlMensaje.BorderColor = Color.OrangeRed;
                    pnlMensaje.BackColor = Color.LightSalmon;
                    strMensaje = "  ALERTA: " + strMensaje;
                    break;
                case EnumTiposMensajeAlerta.Correcto:
                    pnlMensaje.Visible = true;
                    pnlMensaje.BorderColor = Color.DarkGreen;
                    pnlMensaje.BackColor = Color.DarkSeaGreen;
                    strMensaje = "  CORRECTO: " + strMensaje;
                    break;
                case EnumTiposMensajeAlerta.Limpiar:
                    pnlMensaje.Visible = false;
                    lblMensajeError.ForeColor = Color.WhiteSmoke;
                    strMensaje = "-";
                    break;
                default:
                    pnlMensaje.Visible = false;
                    lblMensajeError.ForeColor = Color.WhiteSmoke;
                    strMensaje = "-";
                    break;
            }
            lblMensajeError.ForeColor = Color.DimGray;
            lblMensajeError.Text = "   " + strMensaje + "   ";
        }

        #endregion

    }
}