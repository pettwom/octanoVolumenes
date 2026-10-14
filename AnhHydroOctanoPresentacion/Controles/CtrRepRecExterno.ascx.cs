namespace AnhHydroOctanoPresentacion.Controles
{
    using System.IO;
    using DevExpress.Export;
    using DevExpress.Utils;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrintingLinks;
    using System;
    using System.Collections.Generic;
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
    using Clases.Util;
    using ServiceStack.Text;
    public partial class CtrRepRecExterno : System.Web.UI.UserControl
    {
        #region Variables de entorno

        private readonly CPersistenciaConsulta _consulta = new CPersistenciaConsulta();
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

                    if (pvgAcumulado.DataSource == null && Session[CVariablesSesion.GrillaCabecera] != null)
                    {
                        pvgAcumulado.DataSource = Session[CVariablesSesion.GrillaCabecera];
                        pvgAcumulado.DataBind();
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
                    pvgAcumulado.Visible = false;
                   
                    var strMensaje = string.Empty;
                    if (MValidaDatosIniciales(ref strMensaje))
                    {
                        var strFechaIni = detFechaIni.Date.Year + detFechaIni.Date.Month.ToString().PadLeft(2, '0') + detFechaIni.Date.Day.ToString().PadLeft(2, '0') + "000000";
                        var strFechaFin = detFechaFin.Date.Year + detFechaFin.Date.Month.ToString().PadLeft(2, '0') + detFechaFin.Date.Day.ToString().PadLeft(2, '0') + "235959";
                        var strIdActividad = cmbActividad.Value.ToString();
                        var strIdDepto = cmbDepartamento.Value.ToString();

                        MCargaGrilla(strFechaIni, strFechaFin, strIdActividad, strIdDepto);
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

                    switch (Convert.ToInt16(cmbTipoReporte.Value.ToString()))
                    {
                        case 1:
                            pgeExportar.ASPxPivotGridID = "pvgAcumulado";
                            break;
                        
                    }

                    if ((int)cmbTipoExport.Value == 1)
                    {
                        var options = new XlsxExportOptionsEx()
                        {
                            ExportType = ExportType.DataAware,
                            AllowGrouping = DefaultBoolean.False,
                            TextExportMode = TextExportMode.Value,
                            AllowFixedColumns = DefaultBoolean.False,
                            AllowFixedColumnHeaderPanel = DefaultBoolean.True,
                            RawDataMode = false
                        };
                        pgeExportar.ExportXlsxToResponse("ConsolidadoRecExterno", options);
                    }
                    else
                    {
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
                            Response.AppendHeader("Content-Disposition", "attachment; filename=ConsolidadoRecExterno.pdf");
                            Response.BinaryWrite(stream.GetBuffer());
                            Response.End();
                        }
                        ps.Dispose();
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
            detFechaIni.Date = DateTime.Now.AddMonths(-1);
            detFechaFin.Date = DateTime.Now.AddMonths(-1);
        }

        /// <summary>
        /// METODO. MCargaGrilla
        /// </summary>
        /// <param name="strFechaIni"></param>
        /// <param name="strFechaFin"></param>
        /// <param name="strIdActividad"></param>
        /// <param name="strIdDepto"></param>
        private void MCargaGrilla(string strFechaIni, string strFechaFin, string strIdActividad, string strIdDepto)
        {
            var objConsulta = new EObjetoEnvio5ParametrosProto
            {
                strCredencial = CParametrosHydro.strCredencialFuncionario,
                decFiltro = 1, //Filtro 1
                strParametro1 = strFechaIni,
                strParametro2 = strFechaFin,
                strParametro3 = strIdActividad,
                strParametro4 = cmbTipoReporte.Value.ToString(),
                strParametro5 = strIdDepto
            };
            var objResultado = _consulta.ReporteMovRecaudoExterno(objConsulta);
            if (objResultado.intCodigo == 1)
            {
              
                pvgAcumulado.Visible = true;
                var lstAcumulado = (List<O_MOV_REPORTE_REC_EXTERNO_CTY>)objResultado.oResultado;
                pvgAcumulado.DataSource = lstAcumulado.Where(l=> l.SA_IMPORTE>0);
                pvgAcumulado.DataBind();
                Session[CVariablesSesion.GrillaCabecera] = lstAcumulado.Where(l => l.SA_IMPORTE > 0);

                #region Traza consulta de informacion
                string strAccion = JsonSerializer.SerializeToString(objConsulta);
                string strSalida = lstAcumulado.Count().ToString();
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
            bool booResultado = true;

            if (detFechaIni.Text == "" || detFechaFin.Text == "")
            {
                strMensaje = "Introduzca fechas validas.";
                booResultado = false;
            }
            //else if (cmbActividad.DataSource == null || cmbActividad.Text == "")
            else if (cmbActividad.Text == "")
            {
                strMensaje = "Seleccione una actividad.";
                booResultado = false;
            }
            return booResultado;
        }

        /// <summary>
        /// METODO. MObtenerParametro
        /// </summary>
        /// <param name="decIdTransaccion"></param>
        /// <returns></returns>
        protected int MObtenerParametro(decimal decIdTransaccion)
        {
            int intResultado = -99999;

            var lstTipoTransaccion = (List<O_LISTA_TIPO_TRANSACCION_CTY>)Session[CVariablesSesion.ListaTransaccion];
            lstTipoTransaccion = new List<O_LISTA_TIPO_TRANSACCION_CTY>(lstTipoTransaccion.Where(w => w.ID_TIPO_TRANSACCION == decIdTransaccion));
            if (lstTipoTransaccion.Count > 0)
            {
                intResultado = Convert.ToInt32(lstTipoTransaccion.FirstOrDefault().ID_TIPO_TRANSACCION_PADRE.ToString());
            }

            return intResultado;
        }

        /// <summary>
        /// METODO. MLimpiaGrilla
        /// </summary>
        private void MLimpiaGrilla()
        {
            Session.Remove(CVariablesSesion.GrillaCabecera);
            pvgAcumulado.DataSource = null;
            pvgAcumulado.DataBind();

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