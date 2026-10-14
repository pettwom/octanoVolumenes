using AnhHydroOctanoPersistencia.Aplicacion.Consulta;
using AnhHydroOctanoPersistencia.Entidades;
using AnhHydroOctanoPresentacion.Enumeradores;
using AnhHydroOctanoPresentacion.Parametros;
using DevExpress.Web;
using Librerias.Anh.Us;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AnhHydroOctanoPresentacion.Sitio.Movimientos
{
    public partial class WFRepMovImpEntidad : System.Web.UI.Page
    {
        #region Variables de entorno

        private readonly CPersistenciaConsulta _consulta = new CPersistenciaConsulta();

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    if (!Page.IsCallback && !Page.IsPostBack)
                    {

                    }
                    else
                    {
                        //todo No es carga inicial: no implementado
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
                                        
                    var mes = "";
                        if (cmbMes.Value.ToString().Length == 2){mes = cmbMes.Value.ToString();}else{mes = "0" + cmbMes.Value.ToString();}

                        var fecha = cmbGestion.Value.ToString() + mes;

                        decimal decFecha = Convert.ToDecimal(fecha);

                        var objResultado = _consulta.ReporteImortacionEntidad(
                        new EObjetoEnvio10ParametrosProto
                        {                            
                            decFiltro = 1,
                            strParametro1 = decFecha.ToString()                         
                        });
                        if (objResultado.intCodigo == 1)
                        {
                            var lstDato = (List<O_REPORTE_IMPORTACION_ENTIDAD_CTY>)objResultado.oResultado;

                            pvgDato.DataSource = lstDato;
                            pvgDato.DataBind();
                        }
                        else
                        {
                            pvgDato.DataSource = null;
                            pvgDato.DataBind();
                            MImprimirMensaje(objResultado.strMensaje, EnumTiposMensajeAlerta.Alerta);
                        }

                    #endregion
                }
                catch (Exception exp)
                {
                    #region RegistraError

                    //MLimpiaGrilla();
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
        
        protected void btnExportar_Click(object sender, EventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    //switch (Convert.ToInt16(cmbTipoReporte.Value.ToString()))
                    //{
                    //    case 1:
                    //        pgeExportar.ASPxPivotGridID = "pvgAcumulado";
                    //        break;
                    //    case 2:
                    //        pgeExportar.ASPxPivotGridID = "pvgInternacional";
                    //        break;
                    //    case 3:
                    //        pgeExportar.ASPxPivotGridID = "pvgFueraAla";
                    //        break;
                    //    case 4:
                    //        pgeExportar.ASPxPivotGridID = "pvgInternacional";
                    //        break;
                    //}

                    //if ((int)cmbTipoExport.Value == 1)
                    //{
                    //    var options = new XlsxExportOptionsEx()
                    //    {
                    //        ExportType = ExportType.DataAware,
                    //        AllowGrouping = DefaultBoolean.False,
                    //        TextExportMode = TextExportMode.Value,
                    //        AllowFixedColumns = DefaultBoolean.False,
                    //        AllowFixedColumnHeaderPanel = DefaultBoolean.True,
                    //        RawDataMode = false
                    //    };
                    //    pgeExportar.ExportXlsxToResponse("ConsolidadoDdjj", options);
                    //}
                    //else
                    //{
                    //    var ps = new PrintingSystem();
                    //    var link1 = new PrintableComponentLink();
                    //    link1.Component = pgeExportar;
                    //    link1.PrintingSystem = ps;
                    //    link1.Margins.Top = 1;
                    //    link1.Margins.Left = 1;

                    //    ////adjuntar imagen
                    //    //PrintableComponentLink link2 = new PrintableComponentLink();
                    //    //WebChartControl1.DataBind();
                    //    //link2.Component = ((IChartContainer)WebChartControl1).Chart;
                    //    //link2.PrintingSystem = ps;

                    //    var compositeLink = new CompositeLink();
                    //    compositeLink.Links.AddRange(new object[] { link1 });
                    //    compositeLink.PrintingSystem = ps;
                    //    compositeLink.CreateDocument();
                    //    ps.Document.AutoFitToPagesWidth = 1;

                    //    compositeLink.PrintingSystem.ExportOptions.Pdf.DocumentOptions.Author = "ANH.GOB.BO";
                    //    using (var stream = new MemoryStream())
                    //    {
                    //        compositeLink.PrintingSystem.ExportToPdf(stream);
                    //        Response.Clear();
                    //        Response.Buffer = false;
                    //        Response.AppendHeader("Content-Type", "application/pdf");
                    //        Response.AppendHeader("Content-Transfer-Encoding", "binary");
                    //        Response.AppendHeader("Content-Disposition", "attachment; filename=ConsolidadoDdjj.pdf");
                    //        Response.BinaryWrite(stream.GetBuffer());
                    //        Response.End();
                    //    }
                    //    ps.Dispose();
                    //}

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
    }
}