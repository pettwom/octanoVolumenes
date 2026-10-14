namespace AnhHydroOctanoPresentacion.Controles
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using AnhHydroOctanoPersistencia.Aplicacion.Consulta;
    using AnhHydroOctanoPersistencia.Entidades;
    using Newtonsoft.Json;
    using System.Drawing;
    using System.Reflection;
    using Parametros;
    using Librerias.Anh.Us;
    using Enumeradores;
    using ServiceStack.ServiceClient.Web;
    using System.Globalization;
    public partial class CtrRepConsumoUsuarioDirecto : System.Web.UI.UserControl
    {
        #region Variables de Entorno

        private readonly CPersistenciaConsulta _consulta = new CPersistenciaConsulta();
        JsonServiceClient _clientReg = new JsonServiceClient(CAppSettings.servicio_bsisa);

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
                        DateTime dt = DateTime.Now;
                        int j = dt.Year;
                        for (int i = j; i > 2017; i--)
                        {
                            cmbGestion.Items.Add(Convert.ToString(i));
                        }
                        cmbMes.Value = DateTime.Now.Month;
                        cmbGestion.Value = DateTime.Now.Year;
                    }
                    else
                    {
                        //todo No es carga inicial: no implementado
                    }

                    if (dgvReporte.DataSource == null && Session[CVariablesSesion.GrillaCabecera] != null)
                    {
                        dgvReporte.DataSource = Session[CVariablesSesion.GrillaCabecera];
                        dgvReporte.DataBind();
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

        protected void pnlDatos_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
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
                        int intDia = DateTime.DaysInMonth((int)cmbGestion.Value, (int)cmbMes.Value);
                        //var strFechaIni = cmbGestion.Value + cmbMes.Value.ToString().PadLeft(2, '0') + "01000000";
                        //var strFechaFin = cmbGestion.Value + cmbMes.Value.ToString().PadLeft(2, '0') + intDia.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + "235959";                        
                        var strFechaIni = cmbGestion.Value + cmbMes.Value.ToString().PadLeft(2, '0')+"01";
                        var strFechaFin = cmbGestion.Value + cmbMes.Value.ToString().PadLeft(2, '0')+intDia.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0');                        
                        var strIdActividad = ((int)cmbActividad.Value == 1) ? "1|2|15" : "50";
                        var strIdentificador = txtIdentificador.Text.Trim().ToUpper();

                        MCargaGrilla(strFechaIni, strFechaFin, strIdActividad, strIdentificador);
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

                    if (dgvReporte.VisibleRowCount > 0)
                    {
                        if ((int)cmbTipoExport.Value == 1)
                        {
                            dgeExportar.GridViewID = "dgvReporte";
                            dgeExportar.WriteXlsxToResponse("RepConsumoMtr");
                        }
                        else
                        {
                            //var xreReporte = new repConsumoMtr();
                            //xreReporte.xtcFecha.Text = DateTime.Now.ToShortDateString();
                            //xreReporte.xtcHora.Text = DateTime.Now.ToLongTimeString();
                            //xreReporte.xtcUsuario.Text = CParametrosHydro.StrUsuarioLogin;
                            //xreReporte.xtcIntervalo.Text = "Del : " + detFechaIni.Date.ToShortDateString() + "  Al : " + detFechaFin.Date.ToShortDateString();

                            ////xreReporte.xtcActividad.Text = cmbActividad.Text;
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


        #region Metodos y Funciones

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
       
        /// <summary>
        /// METODO. MLimpiaGrilla
        /// </summary>
        private void MLimpiaGrilla()
        {
            Session.Remove(CVariablesSesion.GrillaCabecera);
            dgvReporte.DataSource = null;
            dgvReporte.DataBind();

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

            //if (detFechaIni.Text == "" || detFechaFin.Text == "")
            //{
            //    strMensaje = "Introduzca fechas validas.";
            //    booResultado = false;
            //}
            //else if (cmbActividad.Text == "")
            //{
            //    strMensaje = "Seleccione una actividad.";
            //    booResultado = false;
            //}
            //else 
            if (txtIdentificador.Text.Length < 4)
            {
                strMensaje = "Introduzca un identificador valido.";
                booResultado = false;
            }
            return booResultado;
        }

        /// <summary>
        /// METODO. MCargaGrilla
        /// </summary>
        /// <param name="strFechaIni"></param>
        /// <param name="strFechaFin"></param>
        /// <param name="strIdActividad"></param>
        /// <param name="strIdDepto"></param>
        private void MCargaGrilla(string strFechaIni, string strFechaFin, string strIdActividad, string strIdentificador)
        {
            _clientReg.Headers.Add(CAppSettings.CabeceraKey, CAppSettings.CabeceraValue);
            var consumidorDirecto = _clientReg.Get<List<EListadoVentas>>("/obtenerreporteventa/" + "0/0/" + strIdentificador + "/" + strFechaIni + "/" + strFechaFin + "?format=json");

            if (consumidorDirecto.Count > 0)
            {
                var cUsuarioDirecto = new O_CUSUARIOS_DIRECTO_CTY();
                var lstUsuarioDirecto = new List<O_CUSUARIOS_DIRECTO_CTY>();                
                
                foreach (var item in consumidorDirecto)
                {
                    var objResultado = _consulta.ObtenerDjCDirecto(
                    new EObjetoEnvio5ParametrosProto
                    {
                        strCredencial = CParametrosHydro.strCredencialFuncionario,
                        decFiltro = 1, //Filtro 1
                        strParametro1 = strFechaIni + "000000",
                        strParametro2 = strFechaFin + "235959",
                        strParametro3 = strIdActividad,
                        strParametro4 = strIdentificador,
                        strParametro5 = item.IdEess.ToString()
                    });
                    if (objResultado.intCodigo == 1)
                    {
                        var list = (List<O_OBTENER_DJ_CDIRECTO>)objResultado.oResultado;
                        cUsuarioDirecto.DENOMINACION = item.Denominacion;
                        cUsuarioDirecto.NIT_EMISOR = item.NitEstacion;
                        cUsuarioDirecto.DEPARTAMENTO = item.Departamento;
                        cUsuarioDirecto.NIT_CONSUMIDOR = item.NitConsumidor;
                        cUsuarioDirecto.RAZON_SOCIAL = item.RazonSocial;
                        cUsuarioDirecto.PRODUCTO = item.Producto;
                        cUsuarioDirecto.CANTIDAD = item.Volumen;
                        cUsuarioDirecto.PRECIO_UNITARIO = item.PrecioUnitario;
                        cUsuarioDirecto.PRECIO_TOTAL = item.Importe;
                        cUsuarioDirecto.FECHA_OPERACION = item.FechaRegistro;
                        cUsuarioDirecto.NRO_TRANSACCION = item.Denominacion;
                        //cUsuarioDirecto.TIPO_DOC = item.Denominacion;
                        //cUsuarioDirecto.DAT_FECHA = item.Denominacion;
                        //cUsuarioDirecto.CITE_GENERADO = (item.IdEess > 0) ? list[0].CITE_GENERADO : " ";
                        //cUsuarioDirecto.REP_NOMBRE = (item.IdEess > 0) ? list[0].REP_NOMBRE : " ";
                        //cUsuarioDirecto.MES = objResultado;


                        lstUsuarioDirecto.Add(cUsuarioDirecto);
                        //var lstReporte = (List<O_CONSUMO_GRAL_CTY>)objResultado.oResultado;
                        //dgvReporte.DataSource = lstReporte;
                        //dgvReporte.DataBind();
                        //Session[CVariablesSesion.GrillaCabecera] = lstReporte;
                    }
                    else
                    {
                        MImprimirMensaje(objResultado.strMensaje, EnumTiposMensajeAlerta.Alerta);
                    }                    
                }

            }
            else
            {
                MImprimirMensaje(" No se encontraron registros (web)", EnumTiposMensajeAlerta.Alerta);
            }
            //var objResultado = _consulta.ReporteConsumoMtr(
            //    new EObjetoEnvio4ParametrosProto
            //    {
            //        strCredencial = CParametrosHydro.strCredencialFuncionario,
            //        decFiltro = 1, //Filtro 1
            //        strParametro1 = strFechaIni,
            //        strParametro2 = strFechaFin,
            //        strParametro3 = strIdActividad,
            //        strParametro4 = strIdentificador
            //    });
            //if (objResultado.intCodigo == 1)
            //{
            //    var lstReporte = (List<O_CONSUMO_GRAL_CTY>)objResultado.oResultado;
            //    dgvReporte.DataSource = lstReporte;
            //    dgvReporte.DataBind();
            //    Session[CVariablesSesion.GrillaCabecera] = lstReporte;
            //}
            //else
            //{
            //    MImprimirMensaje(objResultado.strMensaje, EnumTiposMensajeAlerta.Alerta);
            //}
        }

        #endregion



        private class EListadoVentas
        {
            public int IdEess { get; set; }
            public int NitEstacion { get; set; }
            public string Denominacion { get; set; }
            public string Departamento { get; set; }
            public int IdVentaEstacion { get; set; }
            public string Barcode { get; set; }
            public int IdTransaccion { get; set; }
            public string Identificador { get; set; }
            public int NitConsumidor { get; set; }
            public string RazonSocial { get; set; }
            public string Producto { get; set; }
            public int NroFactura { get; set; }
            public int NroAutorizacion { get; set; }
            public string CodigoControl { get; set; }
            public int Volumen { get; set; }
            public int PrecioUnitario { get; set; }
            public int Importe { get; set; }
            public int FechaRegistro { get; set; }
            public int FechaSistema { get; set; }
        }

    }
}