


namespace AnhHydroOctanoPresentacion.Controles
{
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
    using ServiceStack.ServiceClient.Web;
    using AnhHydroOctanoPersistencia.Entidades;
    using AnhHydroOctanoPersistencia.Aplicacion.Reportes;
    using AnhHydroOctanoPresentacion.Entidades;
    using DevExpress.XtraPrinting;
    using DevExpress.Export;
    using DevExpress.Utils;
    using Page = System.Web.UI.Page;
    using DevExpress.XtraPrintingLinks;
    using System.IO;
    using ServiceStack.Text;
    using Clases.Util;

    
    public partial class CtrRepMovimientoPAHL : System.Web.UI.UserControl
    {
        #region Variables de entorno
        readonly JsonServiceClient _client = new JsonServiceClient(CAppSettings.servicio_octVolumen);
        private readonly CPersistenciaReportes _reporte = new CPersistenciaReportes();
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
                        var strFechaIni = detFechaIni.Date.Year + detFechaIni.Date.Month.ToString().PadLeft(2, '0') + detFechaIni.Date.Day.ToString().PadLeft(2, '0') + "000000";
                        var strFechaFin = detFechaFin.Date.Year + detFechaFin.Date.Month.ToString().PadLeft(2, '0') + detFechaFin.Date.Day.ToString().PadLeft(2, '0') + "235959";
                        var strIdTipoOperacion = tkbTipoOperacion.Value.ToString();
                        var strIdProducto = tkblistaProducto.Value.ToString();
                        var strIdDepto = cmbDepartamento.Value.ToString();

                        if (detFechaIni == null || detFechaFin == null)
                            strMensaje = "Las fechas no pueden ser nulas";
                        else if (detFechaIni.Date > detFechaFin.Date)
                            strMensaje = "La fecha inicial no puede ser mayor que la fecha final";
                        else if (detFechaIni.Date.Year != detFechaFin.Date.Year || detFechaIni.Date.Month != detFechaFin.Date.Month)
                            strMensaje = "Solo se permite consultar dentro del mismo mes";
                        else if (string.IsNullOrEmpty(strIdTipoOperacion) || strIdTipoOperacion == "0")
                            strMensaje = "Debe seleccionar un tipo de operación válido";
                        else if (string.IsNullOrEmpty(strIdProducto) || strIdProducto == "0")
                            strMensaje = "Debe seleccionar un producto válido";
                        else if (string.IsNullOrEmpty(strIdDepto) || strIdDepto == "0")
                            strMensaje = "Debe seleccionar un departamento válido";

                        if (!string.IsNullOrEmpty(strMensaje))
                        {                            
                            MImprimirMensaje(strMensaje, EnumTiposMensajeAlerta.Error);
                            return;
                        }

                        MCargaGrilla(strFechaIni, strFechaFin, strIdTipoOperacion, strIdProducto, strIdDepto);
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

                    if (pvgReporte.RowCount > 0)
                    {
                        ExportarProde(sender, e, "XLS");
                    }
                    else
                    {
                        string mensaje = "No existe datos para exportar.";
                        ScriptManager.RegisterStartupScript(this, GetType(), "Ventana", "Mensaje('" + mensaje + "', 'error');", true);
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
            detFechaIni.Date = DateTime.Now;
            detFechaFin.Date = DateTime.Now;

            var objHidrocarburo = _client.Post<EResultadoProducto>(CAppSettings.svc_lista_producto,
            new
            {
                strCredencial = CParametrosHydro.strCredencialFuncionario,
                strCodigo = ""
            });
            if (objHidrocarburo.IntCodigo == 1)
            {
                tkblistaProducto.DataSource = objHidrocarburo.OResultado.Where(l => l.ID_PRODUCTO_PADRE == 123).ToList();
                tkblistaProducto.Text = "GE,GE+,GP,GP+,GUP100,DO,DO+,DOULS+";
                tkblistaProducto.DataBind();                
            }
            else
            {
                //A implementar
            }
            
            var objTipoOperacion = _client.Post<EResultadoTipoOperacion>(CAppSettings.svc_obtener_tipos_operacion_act,
                                new
                                {
                                    decFiltro = 1,
                                    strParametro1 = 5,//cmbActividad.Value.ToString(),
                                    strParametro2 = "",
                                    strParametro3 = ""
                                });
            if (objTipoOperacion.IntCodigo == 1)
            {
                objTipoOperacion.OResultado = objTipoOperacion.OResultado.OrderByDescending(x => x.FACTOR_CALCULO).ThenBy(y => y.NOMBRE).ToList();
                tkbTipoOperacion.DataSource = objTipoOperacion.OResultado;
                tkbTipoOperacion.Text = "SAL. X MERCADO INTERNO"; //SAL. X MERCADO INTERNO
                tkbTipoOperacion.DataBind();
                
                Session[CVariablesSesion.ComboTipoOperacion] = objTipoOperacion.OResultado;
            }
            else
            {
                MImprimirMensaje(objTipoOperacion.StrMensaje, EnumTiposMensajeAlerta.Error);
            }
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
            //else if (cmbActividad.Text == "")
            //{
            //    strMensaje = "Seleccione una actividad.";
            //    booResultado = false;
            //}
            //else if (cmbTipoOperacion.Text == "")
            //{
            //    strMensaje = "Seleccione un tipo de operación.";
            //    booResultado = false;
            //}
            return booResultado;
        }

        /// <summary>
        /// METODO. MCargaGrilla
        /// </summary>
        /// <param name="strFechaIni"></param>
        /// <param name="strFechaFin"></param>
        /// <param name="strIdActividad"></param>
        /// <param name="strIdDepto"></param>
        private void MCargaGrilla(string strFechaIni, string strFechaFin, string strIdTipoOperacion, string strIdProducto, string strIdDepto)
        {
            var objConsulta = new EObjetoEnvio5ParametrosProto
            {
                strCredencial = CParametrosHydro.strCredencialFuncionario,
                decFiltro = 1, //Filtro 1
                strParametro1 = strFechaIni,
                strParametro2 = strFechaFin,
                strParametro3 = strIdTipoOperacion,
                strParametro4 = strIdProducto,
                strParametro5 = strIdDepto
            };
            var objResultado = _reporte.ReporteMovPAHL(objConsulta);
            if (objResultado.intCodigo == 1)
            {                
                pvgReporte.DataSource = objResultado.oResultado;
                pvgReporte.DataBind();
                Session[CVariablesSesion.GrillaCabecera] = objResultado.oResultado;

                #region Traza consulta de informacion
                string strAccion = JsonSerializer.SerializeToString(objConsulta);
                string strSalida = ((List<O_MOV_PAHL_CTY>)objResultado.oResultado).Count().ToString();
                string mensaje = GetType().Name + "." + MethodBase.GetCurrentMethod().Name;
                _cTrazaInf.MRegistraTrazaInf(strAccion, mensaje, CParametrosHydro.decIdAplicacion, Convert.ToDecimal(Session[CVariablesSesion.IdUsuario].ToString()), HttpContext.Current.Request.UserHostAddress, strSalida);
                #endregion
            }
            else
            {
                MImprimirMensaje(objResultado.strMensaje, EnumTiposMensajeAlerta.Alerta);
            }
        }

        public void ExportarProde(object sender, EventArgs e, string strTipo)
        {
            using (var ps = new PrintingSystem())
            {
                string fileName = "ReporteMovPAHL " + DateTime.Now;
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

        #endregion

    }
}