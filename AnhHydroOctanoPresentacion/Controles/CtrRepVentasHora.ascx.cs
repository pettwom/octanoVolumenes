namespace AnhHydroOctanoPresentacion.Controles
{
    using AnhHydroOctanoPersistencia.Aplicacion.Consulta;
    using Librerias.Anh.Us;
    using System.Drawing;
    using System.IO;
    using DevExpress.Export;
    using DevExpress.Utils;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrintingLinks;
    using Page = System.Web.UI.Page;
    using AnhHydroOctanoPersistencia.Entidades;
    using Entidades;
    using Enumeradores;
    using Parametros;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using DevExpress.Web;
    using Clases.Util;
    using ServiceStack.Text;
    public partial class CtrRepVentasHora : System.Web.UI.UserControl
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
                        Session.Remove(CVariablesSesion.GrillaVentaHora);                      
                      
                        MConfiguraInicio();
                    }
                    else
                    {
                        //todo No es carga inicial: no implementado
                    }

                    if (pvgDatos.DataSource == null && Session[CVariablesSesion.GrillaVentaHora] != null)
                    {
                        pvgDatos.DataSource = Session[CVariablesSesion.GrillaVentaHora];
                        pvgDatos.DataBind();
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
        protected void btnExportar_Click(object sender, EventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    if (pvgDatos.RowCount > 0)
                    {
                        if ((int)cmbTipoExport.Value == 1)
                        {
                            ExportarProde(sender, e, "XLS");
                        }
                        else
                        {
                            //var xreReporte = new repConsumoMtr();
                            //xreReporte.xtcFecha.Text = DateTime.Now.ToShortDateString();
                            //xreReporte.xtcHora.Text = DateTime.Now.ToLongTimeString();
                            //xreReporte.xtcUsuario.Text = CParametrosHydro.StrUsuarioLogin;
                            //xreReporte.xtcIntervalo.Text = "Del : " + detFechaIni.Date.ToShortDateString() + "  Al : " + detFechaFin.Date.ToShortDateString();

                            //xreReporte.xtcActividad.Text = cmbActividad.Text;
                            //xreReporte.xtcIdentificador.Text = txtIdentificador.Text.ToUpper().Trim();

                            //var lstSelect = new List<O_CONSUMO_GRAL_CTY>();
                            //for (var i = 0; i < dgvReporte.VisibleRowCount; i++)
                            //{
                            //    var objRep = (O_CONSUMO_GRAL_CTY)dgvReporte.GetRow(i);
                            //    lstSelect.Add(objRep);
                            //}
                            //xreReporte.DataSource = lstSelect;

                            //Session[CVariablesSesion.ReportePrincipal] = xreReporte;
                            //Session[CVariablesSesion.TipoReporte] = EnumTipoReporte.RepConsumoMtr;
                            //ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( 'WFReporte.aspx', '');", true);
                        }
                    }
                    else
                    {
                        MImprimirMensaje("No existen registros.", EnumTiposMensajeAlerta.Alerta);
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
        public void ExportarProde(object sender, EventArgs e, string strTipo)
        {
            using (var ps = new PrintingSystem())
            {
                string fileName = "ReporteSeg " + DateTime.Now;
                var link1 = new PrintableComponentLink { Component = pgeExportar, PrintingSystem = ps };

                var compositeLink = new CompositeLink();
                compositeLink.Links.AddRange(new object[] { link1 });
                compositeLink.PrintingSystem = ps;
                compositeLink.CreateDocument();

                using (var stream = new MemoryStream())
                {
                    Response.Clear();
                    Response.Buffer = false;
                    switch (strTipo)
                    {
                        case "PDF":
                            compositeLink.PrintingSystem.ExportOptions.Pdf.DocumentOptions.Author = "DTIC";
                            compositeLink.PrintingSystem.ExportToPdf(stream);
                            WriteToResponse(fileName, true, "pdf", "pdf", stream);
                            break;
                        case "XLS":
                            compositeLink.PrintingSystem.ExportToXlsx(stream);
                            WriteToResponse(fileName, true, "xlsx",
                                "vnd.openxmlformats-officedocument.spreadsheetml.sheet", stream);
                            break;
                        case "DOC":
                            compositeLink.PrintingSystem.ExportToRtf(stream);
                            WriteToResponse(fileName, true, "rtf", "rtf", stream);
                            break;
                    }
                    Response.BinaryWrite(stream.GetBuffer());
                    Response.End();
                }
            }
        }

        private void WriteToResponse(string fileName, bool saveAsFile, string fileFormat, string application, MemoryStream stream)
        {
            if (Page == null)
                return;
            string disposition = saveAsFile ? "attachment" : "inline";
            Page.Response.Clear();
            Page.Response.Buffer = false;
            Page.Response.AppendHeader("Content-Type", string.Format("application/{0}", application));
            Page.Response.AppendHeader("Content-Transfer-Encoding", "binary");
            Page.Response.AppendHeader("Content-Disposition",
                                       string.Format("{0}; filename={1}.{2}", disposition, fileName, fileFormat));
            Page.Response.BinaryWrite(stream.ToArray());
            //FitToPageWidth 

            Page.Response.End();
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
                        var strFechaIni = detFechaIni.Date.Year + detFechaIni.Date.Month.ToString().PadLeft(2, '0') + detFechaIni.Date.Day.ToString().PadLeft(2, '0');
                        var strFechaFin = detFechaFin.Date.Year + detFechaFin.Date.Month.ToString().PadLeft(2, '0') + detFechaFin.Date.Day.ToString().PadLeft(2, '0');
                        var strIdActividad = cmbActividad.Value.ToString();
                        var strIdDpto = cmbDepartamento.Value.ToString();
                        MCargaGrilla(strFechaIni, strFechaFin, strIdActividad, strIdDpto);
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
        /// METODO. MCargaGrilla
        /// </summary>
        /// <param name="strFechaIni"></param>
        /// <param name="strFechaFin"></param>
        /// <param name="strIdActividad"></param>
        /// <param name="strIdentificador"></param>
        /// <param name="strIdTipoConsumidor"></param>
        /// <param name="strTipoDoc"></param>
        private void MCargaGrilla(string strFechaIni, string strFechaFin, string strIdActividad, string strIdDpto)
        {
            #region Operacion OctVol
            var objConsulta = new EObjetoEnvio4ParametrosProto
            {
                strCredencial = CParametrosHydro.strCredencialFuncionario,
                decFiltro = 1, //Filtro 1
                strParametro1 = strFechaIni + "000000",
                strParametro2 = strFechaFin + "235959",
                strParametro3 = strIdActividad,
                strParametro4 = strIdDpto
            };
            var objResultado = _consulta.ReporteVentaXHora(objConsulta);
            if (objResultado.intCodigo == 1)
            {
                var lstReporte = (List<O_VENTA_HORA_CTY>)objResultado.oResultado;
                pvgDatos.DataSource = lstReporte;
                pvgDatos.DataBind();
                Session[CVariablesSesion.GrillaVentaHora] = lstReporte;

                #region Traza consulta de informacion
                string strAccion = JsonSerializer.SerializeToString(objConsulta);
                string strSalida = lstReporte.Count().ToString();
                string mensaje = GetType().Name + "." + MethodBase.GetCurrentMethod().Name;
                _cTrazaInf.MRegistraTrazaInf(strAccion, mensaje, CParametrosHydro.decIdAplicacion, Convert.ToDecimal(Session[CVariablesSesion.IdUsuario].ToString()), HttpContext.Current.Request.UserHostAddress, strSalida);
                #endregion 
            }
            else
            {
                MImprimirMensaje(objResultado.strMensaje, EnumTiposMensajeAlerta.Alerta);
            }

            #endregion
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
        /// <summary>
        /// METODO. MLimpiaGrilla
        /// </summary>
        private void MLimpiaGrilla()
        {
            Session.Remove(CVariablesSesion.GrillaVentaHora);
            pvgDatos.DataSource = null;
            pvgDatos.DataBind();

            MImprimirMensaje("", EnumTiposMensajeAlerta.Limpiar);
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
            else if (Convert.ToDateTime(detFechaIni.Text) > Convert.ToDateTime(detFechaFin.Text))
            {
                strMensaje = "La fecha de inicio no puede ser mayo a la fecha fin.";
                booResultado = false;
            }
            else if (cmbActividad.Text == "")
            {
                strMensaje = "Seleccione una actividad.";
                booResultado = false;
            }
            else if ((Convert.ToDateTime(detFechaFin.Text) - Convert.ToDateTime(detFechaIni.Text)).Days > 3)
            {
                strMensaje = "La consulta se debe relizar maximo entre 3 días.";
                booResultado = false;
            }
            return booResultado;
        }
        /// <summary>
        /// METODO. MConfiguraInicio
        /// </summary>
        private void MConfiguraInicio()
        {
            detFechaIni.Date = DateTime.Now;
            detFechaFin.Date = DateTime.Now;
        }
        
        #endregion      
    }
}