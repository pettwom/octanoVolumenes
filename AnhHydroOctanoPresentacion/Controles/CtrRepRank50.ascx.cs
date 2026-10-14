using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
    using ServiceStack.ServiceClient.Web;
    using ServiceStack.Text;
    using Sitio.Usuarios;
    using System.Globalization;
    using Clases.Util;
    public partial class CtrRepRank50 : System.Web.UI.UserControl
    {
        #region Variables de entorno

        private readonly CPersistenciaConsulta _consulta = new CPersistenciaConsulta();
        readonly JsonServiceClient _client = new JsonServiceClient(CAppSettings.servicio_octVolumen);
        readonly JsonServiceClient _clientAdminHydro = new JsonServiceClient(CAppSettings.ServicioAdministradorHydro);
        JsonServiceClient _clientReg = new JsonServiceClient(CAppSettings.servicio_bsisa);
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
                        Session.Remove(CVariablesSesion.ListaPerfiles);
                        Session.Remove(CVariablesSesion.ComboTipoConsumidor);
                        Session.Remove(CVariablesSesion.ListaTransaccion);                     
                        MConfiguraInicio();
                    }
                    else
                    {
                        //todo No es carga inicial: no implementado
                    }

                    if (dgvRepRank.DataSource == null && Session[CVariablesSesion.GrillaCabecera] != null)
                    {
                        dgvRepRank.DataSource = Session[CVariablesSesion.GrillaCabecera];
                        dgvRepRank.DataBind();
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
                        var strFechaIni = detFechaIni.Date.Year + detFechaIni.Date.Month.ToString().PadLeft(2, '0') + detFechaIni.Date.Day.ToString().PadLeft(2, '0');
                        var strFechaFin = detFechaFin.Date.Year + detFechaFin.Date.Month.ToString().PadLeft(2, '0') + detFechaFin.Date.Day.ToString().PadLeft(2, '0');
                        //var strIdActividad = ((int)cmbActividad.Value == 1) ? "1,2" : "50";
                        //var strIdentificador = (tkblstIdentificador.Value.ToString() == "") ? "0" : tkblstIdentificador.Value.ToString().ToUpper();
                     

                        MCargaGrilla(strFechaIni, strFechaFin);
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

        protected void pnlOpciones_Callback(object sender, CallbackEventArgsBase e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Operaciones
                    
                      MConfiguraInicio();                        
                      //cmbActividad.Enabled = true;                   
                      //cmbActividad.SelectedIndex = 0;                   

                    #endregion
                }
                catch (Exception exp)
                {
                    #region RegistraError

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

                    if (dgvRepRank.VisibleRowCount > 0)
                    {
                        if ((int)cmbTipoExport.Value == 1)
                        {
                            dgeExportar.GridViewID = "dgvRepRank";
                            dgeExportar.WriteXlsxToResponse("RepRank");
                        }
                        else
                        {

                            var xreReporte = new repRank50();

                            xreReporte.xrlDia.Text = DateTime.Now.Day.ToString().PadLeft(2, '0');
                            xreReporte.xrlMes.Text = DateTime.Now.Date.Month.ToString().PadLeft(2, '0');
                            xreReporte.xrlGestion.Text = DateTime.Now.Year.ToString();
                            xreReporte.xrlUsuarioEmisor.Text = CVariablesSesion.UsuarioSession(this.Context).NOMBRE_COMPLETO.ToString();                            
                            string hash = CEncriptacion.generarMD5(CVariablesSesion.FuncionarioSesionAnh(this.Context).USUARIO_DOMINIO.Replace("@ANH.GOB.BO","") + "|" + "FECHA: " + CFechas.ConvierteDateTimeLong(DateTime.Now) + "|" + "CANT: " + dgvRepRank.VisibleRowCount);
                            xreReporte.xbcCodigoQr.Text = "EMITIDO: " + CVariablesSesion.FuncionarioSesionAnh(this.Context).USUARIO_DOMINIO.Replace("@ANH.GOB.BO", "") /*CVariablesSesion.UsuarioSession(this.Context).NOMBRE_COMPLETO.ToString()*/ + "|" + "FECHA: " + CFechas.ConvierteDateTimeLong(DateTime.Now) + "|" + "CANT: " + dgvRepRank.VisibleRowCount + "|" + hash;
                            
                            var lstSelect = new List<O_RANK50_CTY>();
                            for (int i = 0; i < dgvRepRank.VisibleRowCount; i++)
                            {
                                var objRep = (O_RANK50_CTY)dgvRepRank.GetRow(i);
                                lstSelect.Add(objRep);
                            }
                            xreReporte.DataSource = lstSelect;

                            Session[CVariablesSesion.ReportePrincipal] = xreReporte;
                            Session[CVariablesSesion.TipoReporte] = EnumTipoReporte.repRank50;
                            
                            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( 'WFReporte.aspx', '');", true);
                            
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
                //todo no implementado
            }
        }

        #endregion

        #region Metodos y funciones      
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
            detFechaIni.Date = DateTime.Now.AddDays(-1);
            detFechaFin.Date = DateTime.Now.AddDays(-1);
         
            //var objHidrocarburo = _client.Post<EResultadoProducto>(CAppSettings.svc_lista_producto,
            //new
            //{
            //    strCredencial = CParametrosHydro.strCredencialFuncionario,
            //    strCodigo = ""
            //});
            //if (objHidrocarburo.IntCodigo == 1)
            //{
            //    tkblistaProducto.DataSource = objHidrocarburo.OResultado.Where(l=>l.ID_PRODUCTO_PADRE==123||l.CODIGO=="GNV").ToList();
            //    tkblistaProducto.DataBind();
            //    //tkblistaProducto. Value = "126, 127, 130, 335, 357, 383, 391";
            //}
            //else 
            //{            
            //    //A implementar
            //}
            
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
        private void MCargaGrilla(string strFechaIni, string strFechaFin)
        {
            #region Operacion OctVol                        
            var lstReporte = new List<O_RANK50_CTY>();
            var objConsulta = new EObjetoEnvio6ParametrosProto
            {
                strCredencial = CParametrosHydro.strCredencialFuncionario,
                decFiltro = 1, //Filtro 1
                strParametro1 = strFechaIni,
                strParametro2 = strFechaFin,
                strParametro3 = "", //strProducto,
                strParametro4 = "", //spnPeriodo.Value.ToString(),
                strParametro5 = "", //strIdTipoConsumidor,
                strParametro6 = ""//strIdentificador
            };

            var objResultado = _consulta.ReporteRank50(objConsulta);
                if (objResultado.intCodigo == 1)
                {
                    lstReporte = (List<O_RANK50_CTY>)objResultado.oResultado;
                     //lstReporteGral.AddRange(lstReporte);
                    dgvRepRank.DataSource = lstReporte;
                    dgvRepRank.DataBind();
                    Session[CVariablesSesion.GrillaCabecera] = lstReporte;

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
        /// METODO. MValidaDatosIniciales
        /// </summary>
        /// <param name="strMensaje"></param>
        /// <returns></returns>
        private bool MValidaDatosIniciales(ref string strMensaje)
        {
            //string[] valores = chkAccion.Text.Split('|');

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
            //else if (cmbActividad.Text == "")
            //{
            //    strMensaje = "Seleccione una actividad.";
            //    booResultado = false;
            //}
            //else if (tkblistaProducto.Value == "")
            //{
            //    strMensaje = "Introduzca un producto valido.";
            //    booResultado = false;
            //}
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
            dgvRepRank.DataSource = null;
            dgvRepRank.DataBind();
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
        private class O_PRODUCTOS_CTY
        {
            public string CODIGO { get; set; }
            public string DESCRIPCION { get; set; }
            public decimal ID_PRODUCTO { get; set; }
            public decimal ID_PRODUCTO_PADRE { get; set; }
            public string NOMBRE { get; set; }  
        }    
    }
    
}