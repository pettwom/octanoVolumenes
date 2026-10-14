
using AnhHydroOctanoPresentacion.Lib;
using ServiceStack.Text;

namespace AnhHydroOctanoPresentacion.Controles
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Reflection;
    using System.Web;
    using System.Web.UI;
    using AnhHydroOctanoPersistencia.Aplicacion.Consulta;
    using AnhHydroOctanoPersistencia.Entidades;
    using Clases.Controles;
    using Clases.Util;
    using Entidades;
    using Enumeradores;
    using Parametros;
    using DevExpress.Web;
    using Librerias.Anh.Us;
    using ServiceStack.ServiceClient.Web;
    using UserControl = System.Web.UI.UserControl;
    using System.Globalization;    
    public partial class CtrPrincipal : UserControl
    {
        #region Variables de entorno

        readonly JsonServiceClient _clientHidro = new JsonServiceClient(CAppSettings.servicio_sireHidro);
        readonly JsonServiceClient _clientI = new JsonServiceClient(CAppSettings.servicio_octInfraestructura);
        readonly JsonServiceClient _client = new JsonServiceClient(CAppSettings.servicio_octVolumen);
        private readonly CPersistenciaConsulta _consulta = new CPersistenciaConsulta();
        readonly CUtil _cUtil = new CUtil();
        CConsultasParametricas cConsultasParametricas = new CConsultasParametricas();
        readonly JsonServiceClient _clientAdminHydro = new JsonServiceClient(CAppSettings.ServicioAdministradorHydro);

        #endregion

        // Variables para notificaciones
        private static List<NotificacionDato> datosNotificaciones = new List<NotificacionDato>();
        private static bool datosInicializados = false;
        private static bool esAdmin = false;

    public class NotificacionDato
    {
        public decimal Id { get; set; }
        public string Estado { get; set; }        
        public string Departamento { get; set; }
        public string RazonSocial { get; set; }
        public string NIT { get; set; }
        public DateTime FechaPeriodo { get; set; }
        public DateTime FechaNotificacion { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public bool Leida { get; set; }
        public DateTime? FechaVista { get; set; }
        public int IdUsuario { get; set; }
    }

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
                        Session.Remove(CVariablesSesion.AuxValidaLlave);
                        Session.Remove(CVariablesSesion.EstructuraArbol);
                        Session.Remove(CVariablesSesion.GrillaTransaccion);
                        Session.Remove(CVariablesSesion.ObtenerEntidades);
                        Session.Remove(CVariablesSesion.ListaPerfiles);
                        datosInicializados = false;
                        esAdmin = false;
                        Session.Remove(CVariablesSesion.ListaAlertaSaldoDiario);

                        Session.Remove(CVariablesSesion.PermisoCons);                                     
                        MCargaArbol();
                        tlsOperadores.DataSource = Session[CVariablesSesion.EstructuraArbol];
                        tlsOperadores.DataBind();
                        Session[CVariablesSesion.AuxValidaLlave] = 0;                        
                        MCargaPermisos();
                        ConfigurarFormulario();
                    }
                    else
                    {
                        //todo No implementado
                    }                        
                    if (tlsOperadores.DataSource == null && Session[CVariablesSesion.EstructuraArbol] != null)
                    {
                        tlsOperadores.DataSource = Session[CVariablesSesion.EstructuraArbol];
                        tlsOperadores.DataBind();
                    }
                    else
                    {
                        //todo No implementado
                    }

                    if (dgvEntidades.DataSource == null && Session[CVariablesSesion.GrillaTransaccion] != null)
                    {
                        dgvEntidades.DataSource = Session[CVariablesSesion.GrillaTransaccion];
                        dgvEntidades.DataBind();
                    }
                    else
                    {
                        //todo No implementado
                    }
                    if (Session[CVariablesSesion.ListaAlertaSaldoDiario]!=null)
                    {
                        dgvNotificaciones.DataSource = Session[CVariablesSesion.ListaAlertaSaldoDiario];
                        dgvNotificaciones.DataBind();                        
                    }
                    else
                    {
                        //todo No implementado
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
                //todo No implementado
            }
        }

        protected void pnlDatos_Callback(object sender, CallbackEventArgsBase e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    if (tlsOperadores.FocusedNode == null) return;        
                    lblActividad.Text = tlsOperadores.FocusedNode.GetValue("StrDescPadre").ToString();
                    MLimpiaGrilla();

                    var intDato1 = (int)tlsOperadores.FocusedNode.GetValue("IntIdActividad");
                    var strDato2 = tlsOperadores.FocusedNode.GetValue("StrDepartamento").ToString();
                    var intDato3 = (int)tlsOperadores.FocusedNode.GetValue("IntIdOperador");
                    var intDato4 = (int)tlsOperadores.FocusedNode.GetValue("IntAccion");
                    
                    var panel = sender as ASPxCallbackPanel;
                    panel.JSProperties["cp_Alerta"] = ""; //L.l

                    MCargaGrilla(intDato1, strDato2, intDato3, intDato4, sender);
                    if (Convert.ToInt16(Session[CVariablesSesion.PermisoCons]) == 1)
                    {
                        MCargaMapa(sender);
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
                //todo No implementado
            }
        }

        /*protected void dgvEntidades_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            try
            {
                #region Proceso a Ejecutar

                if (e.VisibleIndex >= 0)
                {
                    if (e.VisibleIndex%2 == 1)
                    {
                       //e.Row.BackColor = Color.FromArgb(204, 209, 182);
                    }
                    else
                    {
                        //todo No implementado
                    }
                }
                else
                {
                    //todo No implementado
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
        }*/

        protected void pucDatos_WindowCallback(object source, PopupWindowCallbackArgs e)
        {
            try
            {
                #region Proceso a Ejecutar

                txtPiLlave.Visible = false;
                lblLlave.Visible = false;

                int index = dgvEntidades.FocusedRowIndex;
                txtPiId.Text = dgvEntidades.GetRowValues(index, "ID_CONSUMIDOR").ToString();
                txtPiRazonSocial.Text = dgvEntidades.GetRowValues(index, "DENOMINACION").ToString();
                txtPiLatitud.Text = dgvEntidades.GetRowValues(index, "LATITUD").ToString();
                txtPiLongitud.Text = dgvEntidades.GetRowValues(index, "LONGITUD").ToString();

                int intIdEntidad = Convert.ToInt32(dgvEntidades.GetRowValues(index, "ID_ENTIDAD").ToString());
                int intIdActividad = Convert.ToInt32(dgvEntidades.GetRowValues(index, "ID_ACTIVIDAD").ToString());

                var lstInfo = _clientHidro.Get<EResultadoInfoHidro>(CAppSettings.svc_obtenerInformacionSirehidroSirelxId + "/" + CParametrosHydro.StrCredencialSireHidro + "/" + intIdEntidad + "/" + intIdActividad + "?format=json");
                if (lstInfo.Resultado == 1)
                {
                    txtPiProducto.Text = lstInfo.InformacionHydro.FirstOrDefault().strDesHidrocarburo;
                    txtPiLicencia.Text = lstInfo.InformacionHydro.FirstOrDefault().strLicencia;
                    txtPiVigencia.Text = lstInfo.InformacionHydro.FirstOrDefault().strVigencia;
                }
                else
                {
                    txtPiProducto.Text = "ND";
                    txtPiLicencia.Text = "ND";
                    txtPiVigencia.Text = "ND";
                }

                if (intIdActividad == (int)EnumActividades.EstacionServicioLiquido || intIdActividad == (int)EnumActividades.EstacionServicioGnv)
                {
                    txtPiLlave.Visible = true;
                    lblLlave.Visible = true;

                    var objBsisa = _clientI.Get<EResultadoBsisa>(CAppSettings.svc_ListadoLlaveMiddlewareProyecto + "/" + CParametrosHydro.strCredencialInfra + "/" + intIdEntidad + "/" + intIdActividad + "/?format=json");
                    if (objBsisa.decCodigo == 1)
                    {
                        var objResultado = new O_LLAVE_MIDDLEWARE_CTY();
                        objResultado = objBsisa.oResultado.FirstOrDefault();

                        if (objResultado != null && objResultado.LLAVE_MIDDLEWARE != null)
                        {
                            txtPiLlave.Text = objBsisa.oResultado.FirstOrDefault().LLAVE_MIDDLEWARE;
                        }
                        else
                        {
                            txtPiLlave.Text = "ND";
                        }
                    }
                    else
                    {
                        MImprimirMensaje(objBsisa.strMensaje, EnumTiposMensajeAlerta.Error);
                    }
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

        protected void pucModEstado_WindowCallback(object source, PopupWindowCallbackArgs e)
        {
            try
            {
                #region Proceso a Ejecutar

                int index = dgvEntidades.FocusedRowIndex;
                txtPeId.Text = dgvEntidades.GetRowValues(index, "ID_CONSUMIDOR").ToString();
                detPeFecha.Date = DateTime.Now;
                cmbPeEstado.Value = 1;

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

        protected void tlsOperadores_CustomCallback(object sender, DevExpress.Web.ASPxTreeList.TreeListCustomCallbackEventArgs e)
        {
            try
            {
                #region Proceso a Ejecutar

                if (txtBuscador.Text.Trim().Length > 0)
                {
                    var lstArbol = Session[CVariablesSesion.EstructuraArbol] as List<CArbol>;
                    tlsOperadores.CollapseAll();
                    var lstCoinsidencia = lstArbol.Where(x => x.StrDescripcion.ToLower().Contains(txtBuscador.Text.Trim().ToLower())).OrderByDescending(x => x.IntId).ToList();
                    foreach (var reg in lstCoinsidencia)
                    {
                        Session[CVariablesSesion.EntidadSeleccionada] = reg.StrUbicacion;
                        var vecSel = reg.StrUbicacion.Split('-');
                        switch (vecSel.Count())
                        {
                            case 1:
                                tlsOperadores.Nodes[Convert.ToInt16(vecSel[0])].Focus();
                                break;
                            case 2:
                                tlsOperadores.Nodes[Convert.ToInt16(vecSel[0])].ChildNodes[Convert.ToInt16(vecSel[1])].Focus();
                                break;
                            case 3:
                                tlsOperadores.Nodes[Convert.ToInt16(vecSel[0])].ChildNodes[Convert.ToInt16(vecSel[1])].ChildNodes[Convert.ToInt16(vecSel[2])].Focus();
                                break;
                        }
                    }
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

        #endregion

        #region Metodos y funciones

        /// <summary>
        /// METODO. ConfigurarFormulario
        /// </summary>
        private void ConfigurarFormulario()
        {
            if (Convert.ToInt16(Session[CVariablesSesion.PermisoCons]) == 1) return;
            dgvEntidades.Columns[8].Visible = false;
            dgvEntidades.Columns[12].Visible = false;
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

        private void MCargaPermisos()
        {
            try
            {
                var clienteJsonSeg = new JsonServiceClient(System.Web.Configuration.WebConfigurationManager.AppSettings["ServicioJsonSeguridad"]);
                var idUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);

                var lstResultado = clienteJsonSeg.Get<EResultadoUsuarioPermisosForm>("/EPermisoFormularioUsuario/" +
                                                                      CParametrosHydro.strCredencialHydroAdmin + "/" +
                                                                      idUsuario +
                                                                      "/" + CParametrosHydro.decIdAplicacion +
                                                                      "/WFPrincipal.aspx?format=json");
                if (lstResultado.decCodigo == 1)
                {
                    if (lstResultado.oResultado != null && lstResultado.oResultado.Any())
                    {
                        Session[CVariablesSesion.PermisoCons] = lstResultado.oResultado.FirstOrDefault().CONSULTA;
                    }
                    else
                    {
                        Session[CVariablesSesion.PermisoCons] = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                #region RegistraError
                var oMensajeUsuario = new CLogTraza.MensajeUsuario
                {
                    decUsuarioId = Session[CVariablesSesion.UsuarioId],
                    decIdModulo = CParametrosHydro.decIdAplicacion,
                    strAccion = GetType().Name + "." + MethodBase.GetCurrentMethod().Name,
                    strIp = HttpContext.Current.Request.UserHostAddress,
                    decNivelCapa = Convert.ToInt16(CLogTraza.CapasNivel.Presentacion)
                };

                CLogTraza.Error(oMensajeUsuario, ex);
                #endregion
            }
        }

        /// <summary>
        /// METODO. MCargaArbol
        /// </summary>
        private void MCargaArbol()
        {
            MLimpiaGrilla();
            var objArbol = Session[CVariablesSesion.ObjetoArbol] as List<O_OBTIENE_ENTIDADES_CTY>;
            if (objArbol == null)
            {
                var idUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                var objResultado = _consulta.ObtenerEntidades(
                new EObjetoEnvio5ParametrosProto
                {
                    decFiltro = 1,
                    strParametro1 = idUsuario.ToString(),
                    strParametro2 = cConsultasParametricas.ObtieneIdsActividadOctanioVol(),
                    strParametro3 = CAppSettings.idAplicacion,
                    strParametro4 = "",
                    strParametro5 = ""
                });
                if (objResultado.intCodigo == 1)
                {
                    objArbol = (List<O_OBTIENE_ENTIDADES_CTY>)objResultado.oResultado;
                    Session[CVariablesSesion.ObjetoArbol] = objArbol;
                    Session.Add("entidades_usuario", objResultado);
                }
                else
                {
                    ASPxSplitter1.Visible = false;
                    MImprimirMensaje(objResultado.strMensaje, EnumTiposMensajeAlerta.Error);
                    return;
                }
            }
            Session[CVariablesSesion.EstructuraArbol] = _cUtil.MObtenerArbolOrdenado(3, objArbol);
            //var strObjeto = JsonSerializer.SerializeToString((List<CArbol>)Session[CVariablesSesion.EstructuraArbol]);
            Session[CVariablesSesion.ObtenerEntidades] = objArbol;   
               
            if (objArbol.Where(l => l.ID_ACTIVIDAD == 1).Count() > 0)
            {
                InicializarDatosAlerta();                
                CargarGrillaNotificaciones("0", "0");
                ActualizarBadge(); 
                pucInformativo.ShowOnPageLoad = (Request.QueryString["sw"] != "1");                      
            }    
        }

        /// <summary>
        /// METODO. MCargaGrilla
        /// </summary>
        /// <param name="intIdActividad"></param>
        /// <param name="strDepto"></param>
        /// <param name="intIdOperador"></param>
        /// <param name="intTipo"></param>
        private void MCargaGrilla(int intIdActividad, string strDepto, int intIdOperador, int intTipo, object sender)
        {
            var lstEntidad = (List<O_OBTIENE_ENTIDADES_CTY>)Session[CVariablesSesion.ObtenerEntidades];
            switch (intTipo)
            {
                case 1:
                    lstEntidad = lstEntidad.Where(a => a.ID_ACTIVIDAD == intIdActividad).ToList();                                                 
                    break;
                case 2:
                    lstEntidad = lstEntidad.Where(a => a.ID_ACTIVIDAD == intIdActividad && a.DEPARTAMENTO == strDepto).ToList();
                    break;
                default:
                    lstEntidad = lstEntidad.Where(a => a.ID_ACTIVIDAD == intIdActividad && a.DEPARTAMENTO == strDepto && a.ID_CONSUMIDOR == intIdOperador).ToList();   
                    
                    MValidaSaldosNeg(intIdActividad.ToString(), intIdOperador.ToString(), sender, lstEntidad[0].DENOMINACION);
                    break;
            }
            lstEntidad = lstEntidad.Where(a => a.ID_ACTIVIDAD == intIdActividad).ToList();
            lstEntidad = lstEntidad.OrderBy(a => a.DEPARTAMENTO).ThenBy(a => a.DENOMINACION).ToList();
            Session[CVariablesSesion.GrillaTransaccion] = lstEntidad;
            dgvEntidades.DataSource = lstEntidad;
            dgvEntidades.DataBind();
        }

        /// <summary>
        /// METODO. MCargaMapa
        /// </summary>
        /// <param name="sender"></param>
        private void MCargaMapa(object sender)
        {
            var lstEntidad = (List<O_OBTIENE_ENTIDADES_CTY>)Session[CVariablesSesion.GrillaTransaccion];

            double[] latitud = new double[lstEntidad.Count + 1];
            double[] longitud = new double[lstEntidad.Count + 1];
            string[] strDescripcion = new string[lstEntidad.Count + 1];

            double douCentroLat = 0;
            double douCentroLon = 0;
            int intContador = 0;
            for (int i = 0; i < lstEntidad.Count; i++)
            {
                latitud[i + 1] = (lstEntidad[i].LATITUD == null) ? 0 : (double)lstEntidad[i].LATITUD;
                longitud[i + 1] = (lstEntidad[i].LONGITUD == null) ? 0 : (double)lstEntidad[i].LONGITUD;
                strDescripcion[i + 1] = "<div id=\"content\">" + "<h2>" + lstEntidad[i].DENOMINACION + "</h2>" + "<div id=\"bodyContent\">" +
                "<p><b>Ubicación : </b>" + lstEntidad[i].DEPARTAMENTO + "/" + lstEntidad[i].MUNICIPIO + "/" + lstEntidad[i].LOCALIDAD + ".</p>" +
                "<p><b>Dirección : </b>" + lstEntidad[i].DIRECCION + ".</p>" +
                "<p><b>Telefono(s) : </b>" + lstEntidad[i].TELEFONOS + ".</p>" +
                "<p><b>Licencia : </b>" + lstEntidad[i].NRO_LICENCIA + ".</p>" +
                "<p><b>Fecha de Otorgación : </b>" + lstEntidad[i].VIGENCIA + ".</p>" +
                "<p><b>Fecha de Vencimiento : </b>" + lstEntidad[i].LIC_VENCIMIENTO + ".</p>" +
                "<p><b>Productos : </b>" + lstEntidad[i].LIC_PROD + ".</p>" + "</div>" +
                "</div>";

                douCentroLat += (lstEntidad[i].LATITUD == null) ? 0 : (double)lstEntidad[i].LATITUD;
                douCentroLon += (lstEntidad[i].LONGITUD == null) ? 0 : (double)lstEntidad[i].LONGITUD;

                if ((double)lstEntidad[i].LATITUD != 0 && (double)lstEntidad[i].LONGITUD != 0)
                {
                    intContador++;
                }
            }
            intContador = ((intContador == 0) ? 1 : intContador);
            douCentroLat = douCentroLat / intContador;
            douCentroLon = douCentroLon / intContador;

            latitud[0] = douCentroLat;
            longitud[0] = douCentroLon;

            var panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cp_latitud"] = latitud;
            panel.JSProperties["cp_longitud"] = longitud;
            panel.JSProperties["cp_descripcion"] = strDescripcion;
        }

        /// <summary>
        /// METODO. MLimpiaGrilla
        /// </summary>
        private void MLimpiaGrilla()
        {
            Session.Remove(CVariablesSesion.GrillaTransaccion);
            dgvEntidades.DataSource = null;
            dgvEntidades.DataBind();
            pnlMensaje.Visible = false;
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
            lblMensajeError.Text = "   " + strMensaje + "   ";
        }
        /// <summary>
        /// METODO. MValidaSaldosNeg
        /// </summary>
        private void MValidaSaldosNeg(string strIdTipoActividad, string strIdConsumidor, object sender,string strDenominacion)
        {
            #region Alerta de Saldos Negativos RAR-ANH-DGRCR 0171/2021

            DateTime dtFechaActual = DateTime.Now;        
            var strFechaDiaInicio = dtFechaActual.Date.Year + dtFechaActual.Date.Month.ToString().PadLeft(2, '0') + "01"; //"20170101", //fechaInicio
            var strFechaDiaFin = dtFechaActual.Date.Year + dtFechaActual.Date.Month.ToString().PadLeft(2, '0') + dtFechaActual.Date.Day.ToString().PadLeft(2, '0'); //"20170131" //fechaFinal
            
            var objTipoOperacion = _client.Post<EResultadoTipoOperacion>(CAppSettings.svc_obtener_tipos_operacion_act,
                          new
                          {
                              decFiltro = 3,
                              strParametro1 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
                              strParametro2 = tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString(),
                              strParametro3 = strFechaDiaInicio + "01000000",
                              strParametro4 = strFechaDiaFin + "235959"
                          });
            if (objTipoOperacion.IntCodigo == 1)
            {                
                //panel.JSProperties["cp_Alerta"] = "";
                if (objTipoOperacion.OResultado.Where(x => x.FACTOR_CALCULO == 1).Count() > 0)
                {
                    var objResultado = _consulta.ObtenerSaldo(
                    new EObjetoEnvio4ParametrosProto
                    {
                        strCredencial = CParametrosHydro.strCredencialFuncionario,
                        decFiltro = 1,
                        strParametro1 = strIdTipoActividad,
                        strParametro2 = strIdConsumidor,
                        strParametro3 = strFechaDiaInicio,
                        strParametro4 = strFechaDiaFin
                    });
                    if (objResultado.intCodigo == 1)
                    {
                        var objSaldos = (List<O_SALDO_FINAL_DIA_CTY>)objResultado.oResultado;
                        var auxObjSaldo = objSaldos.Where(x => x.SALDO_FIN < 0).ToList();
                        var panel = sender as ASPxCallbackPanel;
                        if (auxObjSaldo.Count > 0)
                        {
                            //panel.JSProperties["cp_Alerta"] = "EXISTEN SALDOS NEGATIVOS, VERIFIQUE SU INFORMACION. <br> PROYECTO: " + strDenominacion + "<br><br><br> EVITE OBSERVACIONES. <br>EN CUMPLIMIENTO RAR-ANH-DGRCR 0171/2021.";
                            panel.JSProperties["cp_Alerta"] = "EXISTEN SALDOS NEGATIVOS, VERIFIQUE SU INFORMACION. <br> PROYECTO: " + strDenominacion + "<br><br><br> EVITE OBSERVACIONES.";
                        }
                        else
                        {
                            panel.JSProperties["cp_Alerta"] = "";
                        }
                    }
                    else
                    {
                        //A Implementar 
                    }
                }
                else { 
                //A implementar
                }
            }
            else
            {
                MImprimirMensaje(objTipoOperacion.StrMensaje, EnumTiposMensajeAlerta.Error);
            }           

            #endregion
        }

        #endregion


        #region Inicialización de Datos de Ejemplo
        private void InicializarDatosAlerta()
        {           
            DeterminarRolUsuario();           
            if (!esAdmin)
            {                
                detFechaIni.Date = DateTime.Today.AddDays(-5);
                detFechaFin.Date = DateTime.Today;
            }
            else
            {
                detFechaIni.Date = DateTime.Today;
                detFechaFin.Date = DateTime.Today;
            }

            if (datosInicializados) return;            
            RecargarDatosNotificaciones(detFechaIni.Date, detFechaFin.Date);
            datosInicializados = true;
        }
        #endregion

        #region Métodos de Notificaciones
        private void ActualizarBadge()
        {
            try
            {
                int notificacionesPendientes = datosNotificaciones.Count(n => n.Estado == "PLAZO VENCIDO" && !n.Leida);
                // CORREGIDO: usar + en lugar de {}
                string script = "if(window.actualizarBadgeNotificaciones) window.actualizarBadgeNotificaciones(" + notificacionesPendientes + ");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ActualizarBadge", script, true);
            }
            catch (Exception ex)
            {
                CLog.Error(this.Context, GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        private void CargarGrillaNotificaciones(string depto, string estado)
        {
            try
            {
                var hoy = DateTime.Now;
                var q = datosNotificaciones.AsEnumerable();

                if (depto != "0" && !string.IsNullOrEmpty(depto))
                {
                    q = q.Where(n => n.Departamento == depto);
                }

                if (estado != "0" && !string.IsNullOrEmpty(estado))
                    q = q.Where(n => n.Estado == estado);

                var data = q
                    .OrderByDescending(n => n.Estado == "PLAZO VENCIDO")
                    .ThenBy(n => n.Departamento)
                    .ThenBy(n => n.RazonSocial)
                    .ThenByDescending(n => n.FechaNotificacion)
                    .Select(n =>
                    {
                        TimeSpan diferencia = hoy - n.FechaVencimiento;
                        return new
                        {
                            n.Id,
                            ESTADO = n.Estado,
                            DEPARTAMENTO = n.Departamento,
                            RAZON_SOCIAL = n.RazonSocial,
                            n.NIT,
                            PERIODO = n.FechaPeriodo.ToString("MMMM yyyy"),
                            FECHA_NOTIFICACION = n.FechaNotificacion.ToString("dd/MM/yyyy HH:mm"),
                            DIAS_VENCIDO = n.Estado == "PLAZO VENCIDO" && diferencia.TotalSeconds > 0
                                ? string.Format("{0}d {1}h", (int)diferencia.TotalDays, diferencia.Hours)
                                : "0",
                            LEIDA = n.Leida
                        };
                    })
                    .ToList();

                Session[CVariablesSesion.ListaAlertaSaldoDiario] = data;
                dgvNotificaciones.DataSource = data;
                dgvNotificaciones.DataBind();
                int vencidas = data.Count(x => x.ESTADO == "PLAZO VENCIDO");
                lblResumenNotif.Text = "Total: " + data.Select(x => x.RAZON_SOCIAL).Distinct().Count() + " estaciones  |  " +
                                      "Plazo vencido: " + vencidas +
                                      "  |  Período: " + detFechaIni.Date.ToString("dd/MM/yyyy") + " - " + detFechaFin.Date.ToString("dd/MM/yyyy");
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

        private void MarcarLeida(int id)
        {
            try
            {
                var n = datosNotificaciones.FirstOrDefault(x => x.Id == id);
                if (n != null)
                {
                    n.Leida = true;
                    n.FechaVista = DateTime.Now;
                    ActualizarBadge();
                }
            }
            catch (Exception ex)
            {
                CLog.Error(this.Context, GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        private void MarcarTodasLeidas()
        {
            try
            {
                int idUsuario = 1; // Obtener de sesión si es necesario
                foreach (var n in datosNotificaciones.Where(n => n.IdUsuario == idUsuario && !n.Leida))
                {
                    n.Leida = true;
                    n.FechaVista = DateTime.Now;
                }
                ActualizarBadge();
            }
            catch (Exception ex)
            {
                CLog.Error(this.Context, GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        private void CargarDetalleEstacion(int idNotif, DateTime fechaGrilla)
        {
            try
            {
                var n = datosNotificaciones.FirstOrDefault(x => x.Id == idNotif && x.FechaNotificacion.ToString("dd/MM/yyyy") == fechaGrilla.ToString("dd/MM/yyyy"));
                //var n = datosNotificaciones.FirstOrDefault(x => x.Id == idNotif);
                if (n == null) return;
                // Calcular la diferencia antes de crear el objeto
                var diff = DateTime.Now - n.FechaVencimiento;
                string diasVencido = (n.Estado == "DECLARADO") ? "0" :(diff.TotalSeconds > 0 ? string.Format("{0}d {1}h", (int)diff.TotalDays, diff.Hours) : "0 días");
                var detalle = new List<dynamic> {

                    new { CAMPO = "NIT",             VALOR = n.NIT },
                    new { CAMPO = "Razón Social",    VALOR = n.RazonSocial },
                    new { CAMPO = "Departamento",    VALOR = n.Departamento },
                    new { CAMPO = "Estado",          VALOR = n.Estado },
                    new { CAMPO = "Periodo",         VALOR = n.FechaPeriodo.ToString("MMMM yyyy") },
                    new { CAMPO = "Fecha límite",    VALOR = n.FechaVencimiento.ToString("dd/MM/yyyy HH:mm")}, 
                    new { CAMPO = "Días vencido",    VALOR = diasVencido }
                    //new { CAMPO = "Días vencido",    VALOR = (DateTime.Today - n.FechaVencimiento).TotalSeconds > 0? string.Format("{0}d {1}h", (int)(DateTime.Today - n.FechaVencimiento).TotalDays, (DateTime.Today - n.FechaVencimiento).Hours) : "0" + " días" },
                    //new { CAMPO = "Notif. enviada",  VALOR = n.Leida ? "Sí — " + (n.FechaVista.ToString() ?? "") : "No" }
                };

                dgvDetalleEstacion.DataSource = detalle;
                dgvDetalleEstacion.DataBind();

                lblDetalleNotif.Text = "DETALLE DE LA ESTACION: " + n.RazonSocial + " | NIT: " + n.NIT;
                Session["IdNotifDetalle"] = idNotif;
            }
            catch (Exception ex)
            {
                CLog.Error(this.Context, GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }
        #endregion

        #region Callbacks de Notificaciones
        protected void pnlNotificaciones_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
        {
            try
            {
                string param = e.Parameter ?? "0|0||";

                if (param.StartsWith("LEIDA|"))
                {
                    int id = Convert.ToInt32(param.Split('|')[1]);
                    MarcarLeida(id);
                    CargarGrillaNotificaciones(cmbDepartamento.Value.ToString() ?? "0", cmbEstadoNotif.Value.ToString() ?? "0");
                    return;
                }

                if (param == "MARCAR_TODAS")
                {
                    MarcarTodasLeidas();
                    CargarGrillaNotificaciones("0", "0");
                    return;
                }

                if (param == "CONTAR")
                {
                    ActualizarBadge();
                    return;
                }

                var partes = param.Split('|');
                string depto = partes.Length > 0 ? partes[0] : "0";
                string estado = partes.Length > 1 ? partes[1] : "0";

                // Obtener fechas del parámetro
                DateTime? fechaInicio = null;
                DateTime? fechaFin = null;

                if (partes.Length > 2 && !string.IsNullOrEmpty(partes[2]))
                {
                    try { fechaInicio = fechaInicio = DateTime.ParseExact(partes[2], "dd/MM/yyyy", CultureInfo.InvariantCulture); }
                    catch { }
                }

                if (partes.Length > 3 && !string.IsNullOrEmpty(partes[3]))
                {
                    try { fechaFin = fechaFin = DateTime.ParseExact(partes[3], "dd/MM/yyyy", CultureInfo.InvariantCulture); }
                    catch { }
                }

                // Actualizar los controles de fecha con los valores seleccionados
                if (fechaInicio.HasValue)
                    detFechaIni.Date = fechaInicio.Value;
                if (fechaFin.HasValue)
                    detFechaFin.Date = fechaFin.Value;

                // Recargar datos con las nuevas fechas
                RecargarDatosNotificaciones(fechaInicio, fechaFin);
                CargarGrillaNotificaciones(depto, estado);
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

        protected void pnlDetalleNotif_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
        {
            try
            {
                int idNotif = Convert.ToInt32(e.Parameter);
                // Obtener el índice de la fila seleccionada
                int focusedIndex = dgvNotificaciones.FocusedRowIndex;
                // Obtener la fecha directamente del grid
                object fechaObj = dgvNotificaciones.GetRowValues(focusedIndex, "FECHA_NOTIFICACION");
                DateTime fecha = Convert.ToDateTime(fechaObj);
                CargarDetalleEstacion(idNotif, fecha);
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
        #endregion

        #region Eventos del Grid de Notificaciones
        protected void dgvNotificaciones_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
        {
            try
            {
                if (e.RowType != DevExpress.Web.GridViewRowType.Data)
                    return;

                string estado = e.GetValue("ESTADO").ToString() ?? "";
                bool leida = false;

                var leidaValue = e.GetValue("LEIDA");
                if (leidaValue != null)
                {
                    leida = Convert.ToBoolean(leidaValue);
                }

                if (estado == "PLAZO VENCIDO")
                {
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFF0F0");
                    e.Row.ForeColor = System.Drawing.ColorTranslator.FromHtml("#C0392B");
                }
                else if (!leida)
                {
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFBE6");
                }
            }
            catch (Exception ex)
            {
                CLog.Error(this.Context, GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        protected void dgvNotificaciones_CustomButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCustomButtonEventArgs e)
        {
            try
            {
                if (e.ButtonID != "btnMarcarLeida") return;
                
                bool leida = Convert.ToBoolean(
                    dgvNotificaciones.GetRowValues(e.VisibleIndex, "LEIDA") ?? false);
                
                e.Visible = leida
                    ? DevExpress.Utils.DefaultBoolean.False
                    : DevExpress.Utils.DefaultBoolean.True;
            }
            catch (Exception ex)
            {
                CLog.Error(this.Context, GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        protected void btnExportarNotif_Click(object sender, EventArgs e)
        {
            try
            {
                dgeNotificaciones.WriteXlsToResponse("Notificaciones");
            }
            catch (Exception ex)
            {
                CLog.Error(this.Context, GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        protected void btnReenviarNotif_Click(object sender, EventArgs e)
{
    try
    {
        int id = Convert.ToInt32(Session["IdNotifDetalle"] ?? 0);
        if (id == 0)
        {
            MostrarMensaje("No se pudo identificar la notificación", "warning");
            return;
        }

        var notif = datosNotificaciones.FirstOrDefault(n => n.Id == id);
        if (notif != null)
        {
            notif.FechaNotificacion = DateTime.Now;
            notif.Leida = false;
            notif.FechaVista = null;

            // CORREGIDO: usar + en lugar de {}
            MostrarMensaje("Notificación reenviada a " + notif.RazonSocial + " correctamente.", "success");
            CargarGrillaNotificaciones("0", "0");
            //CargarDetalleEstacion(id);
            ActualizarBadge();
        }
    }
    catch (Exception ex)
    {
        CLog.Error(this.Context, GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
    }
}

protected void btnRegistrarMulta_Click(object sender, EventArgs e)
{
    try
    {
        int id = Convert.ToInt32(Session["IdNotifDetalle"] ?? 0);
        if (id == 0)
        {
            MostrarMensaje("No se pudo identificar la notificación", "warning");
            return;
        }

        var notif = datosNotificaciones.FirstOrDefault(n => n.Id == id);
        if (notif != null)
        {
            // CORREGIDO: usar + en lugar de {}
            MostrarMensaje("Registrando multa para " + notif.RazonSocial + " (NIT: " + notif.NIT + ")...", "info");
            // Aquí puedes abrir un popup para registrar multa
            // Por ejemplo: pucRegistroMulta.Show();
        }
    }
    catch (Exception ex)
    {
        CLog.Error(this.Context, GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
    }
}
        #endregion

        #region Métodos Auxiliares
        private void MostrarMensaje(string mensaje, string tipo)
        {
            var panel = pnlNotificaciones;

            if (panel != null)
            {
                panel.JSProperties["cp_ShowAlert"] = "true";
                panel.JSProperties["cp_AlertMessage"] = mensaje;
                panel.JSProperties["cp_AlertType"] = tipo;
                return;
            }
           
        


            //string mensajeLimpio = mensaje.Replace("'", "\\'").Replace("\n", "\\n");
            //string script = "alert('[" + tipo.ToUpper() + "] " + mensaje + "');";
            //ScriptManager.RegisterStartupScript(this, GetType(), "Mensaje_" + Guid.NewGuid().ToString(), script, true);
        }
        #endregion

        #region Métodos Públicos para Comunicación con MasterPage
       public void AbrirPopupNotificaciones()
        {
            try
            {
                // En v15.1, no existe Show(), se usa ShowOnPageLoad
                pucNotificaciones.ShowOnPageLoad = true;
        
                // O usar el método Show() del cliente a través de JavaScript
                string script = "pucNotificaciones.Show(); pnlNotificaciones.PerformCallback('0|0');";
                ScriptManager.RegisterStartupScript(this, GetType(), "AbrirNotificaciones", script, true);
            }
            catch (Exception ex)
            {
                CLog.Error(this.Context, GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        public void ActualizarContadorNotificaciones()
        {
            try
            {
                ActualizarBadge();
            }
            catch (Exception ex)
            {
                CLog.Error(this.Context, GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
            }
        }
        #endregion

        #region fechas de notificacion 29/03/2026

        /// <summary>
        /// Determina el estado de la notificación basado en la hora de reporte
        /// </summary>
        private string DeterminarEstadoNotificacion(O_LISTA_REPORTE_MENSUAL_CTY reg)
        {
            // Si ya declaró (envió reporte)
            if (reg.ESTADO_MENSUAL == 1 && reg.FECHA_REPORTE > 0)
            {
                // Verificar si reportó después de las 09:00
                DateTime fechaReporte = DateTime.ParseExact(
                    reg.FECHA_REPORTE.ToString().PadRight(14, '0'), 
                    "yyyyMMddHHmmss", 
                    CultureInfo.InvariantCulture);
        
                TimeSpan horaReporte = fechaReporte.TimeOfDay;
                TimeSpan horaCorte = new TimeSpan(9, 0, 0); // 09:00
        
                if (horaReporte > horaCorte)
                {
                    return "FUERA_DE_PLAZO";
                }
                else
                {
                    return "DECLARADO";
                }
            }
            else
            {
                // No ha declarado, verificar si ya pasó la hora límite
                DateTime fechaActual = DateTime.Now;
                TimeSpan horaActual = fechaActual.TimeOfDay;
                TimeSpan horaCorte = new TimeSpan(9, 0, 0);
        
                if (horaActual > horaCorte)
                {
                    return "PLAZO VENCIDO";
                }
                else
                {
                    return "EN_PLAZO";
                }
            }
        }

        /// <summary>
        /// Obtiene la fecha de notificación
        /// </summary>
        private DateTime ObtenerFechaNotificacion(O_LISTA_REPORTE_MENSUAL_CTY reg)
        {
            if (reg.FECHA_REPORTE > 0)
            {
                return DateTime.ParseExact(reg.FECHA_REPORTE.ToString().PadRight(14, '0'), "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
            }
            else
            {
                // Si no ha reportado, la notificación se genera hoy a las 09:01
                DateTime hoy = DateTime.Today;
                return hoy.AddHours(9).AddMinutes(1);
            }
        }

        /// <summary>
        /// Calcula la fecha de vencimiento basada en el estado
        /// </summary>
        private DateTime CalcularFechaVencimiento(O_LISTA_REPORTE_MENSUAL_CTY reg, string estado)
        {
            DateTime fechaBase;
    
            if (reg.FECHA_REPORTE > 0)
            {
                fechaBase = DateTime.ParseExact(reg.FECHA_REPORTE.ToString().Substring(0, 8), "yyyyMMdd", CultureInfo.InvariantCulture);
            }
            else
            {
                fechaBase = DateTime.Today;
            }
    
            switch (estado)
            {
                case "PLAZO VENCIDO":
                case "FUERA_DE_PLAZO":
                    // Vencimiento el mismo día a las 09:00
                    return fechaBase.AddHours(9);
                case "EN_PLAZO":
                    // Vence hoy a las 09:00
                    return fechaBase;//.AddHours(9);
                default:
                    return fechaBase;//.AddHours(9);
            }
        }  
   
         /// <summary>
        /// Recarga los datos de notificaciones según el rango de fechas
        /// </summary>
        private void RecargarDatosNotificaciones(DateTime? fechaInicio = null, DateTime? fechaFin = null)
        {
            try
            {
                // VALIDAR FECHAS
                if (!ValidarFechas(detFechaIni.Date, detFechaFin.Date))
                {
                    // Restaurar fechas anteriores si hay error
                    detFechaIni.Date = DateTime.Today.AddDays(-5);
                    detFechaFin.Date = DateTime.Today;
                    //fechaIni = detFechaIni.Date;
                    //fechaFinLocal = detFechaFin.Date;                
                }

                var objConsulta = new EObjetoEnvio5ParametrosProto
                {
                    strCredencial = CParametrosHydro.strCredencialFuncionario,
                    decFiltro = 3,
                    strParametro1 = Session[CVariablesSesion.IdUsuario].ToString(),
                    strParametro2 = "1",
                    strParametro3 = detFechaIni.Date.Year + detFechaIni.Date.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + detFechaIni.Date.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + "000000",
                    strParametro4 = detFechaFin.Date.Year + detFechaFin.Date.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + detFechaFin.Date.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + "235959",
                    strParametro5 = CParametrosHydro.decIdAplicacion.ToString()
                };

                var objReporte = _consulta.ListaObligacionMes(objConsulta);

                if (objReporte.intCodigo == 1)
                {
                    objReporte.oResultado.ForEach(a => a.strFechaReg = (a.FECHA_REPORTE == 0 || a.ESTADO_MENSUAL != 1) ? "" :
                        a.FECHA_REPORTE.ToString().Substring(6, 2) + "/" + a.FECHA_REPORTE.ToString().Substring(4, 2) + "/" +
                        a.FECHA_REPORTE.ToString().Substring(0, 4) + " " + a.FECHA_REPORTE.ToString().Substring(8, 2) + ":" +
                        a.FECHA_REPORTE.ToString().Substring(10, 2) + ":" + a.FECHA_REPORTE.ToString().Substring(10, 2));

                    datosNotificaciones = objReporte.oResultado
                        .Select(reg =>
                        {
                            string estado = DeterminarEstadoNotificacion(reg);
                            return new NotificacionDato
                            {
                                Id = reg.ID_CONSUMIDOR,
                                Estado = reg.MENSAJE_MENSUAL,
                                Departamento = reg.DEPARTAMENTO ?? "",
                                RazonSocial = reg.DENOMINACION ?? "",
                                NIT = reg.NIT ?? "",
                                FechaPeriodo = reg.FECHA_REPORTE > 0 ? DateTime.ParseExact(reg.FECHA_REPORTE.ToString().Substring(0, 8), "yyyyMMdd", CultureInfo.InvariantCulture) :
                                    DateTime.Today,
                                FechaNotificacion = DateTime.ParseExact(reg.FECHA_REPORTE.ToString().PadRight(14, '0'), "yyyyMMddHHmmss", CultureInfo.InvariantCulture), //ObtenerFechaNotificacion(reg),
                                FechaVencimiento = CalcularFechaVencimiento(reg, estado),
                                Leida = false,
                                IdUsuario = Convert.ToInt32(Session[CVariablesSesion.IdUsuario])
                            };
                        })
                        .OrderBy(n => n.Departamento)
                        .ThenBy(n => n.RazonSocial)
                        .ThenByDescending(n => n.FechaNotificacion)
                        .ToList();

                }
                else
                {
                    MImprimirMensaje(objReporte.strMensaje, EnumTiposMensajeAlerta.Error);
                }
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

        #endregion        
                
        // En el método DeterminarRolUsuario, modifica así:
        private void DeterminarRolUsuario()
        {
            try
            {
                var objPerfil = _clientAdminHydro.Get<AnhHydroOctanoPresentacion.Sitio.Usuarios.EResultadoLista>("/EReporteUsuariosPerfiles/" + CParametrosHydro.strCredencialHydroAdmin + "/" + Convert.ToDecimal(CAppSettings.idAplicacion) + "/" + Session[CVariablesSesion.IdUsuario] + "?format=json");

                if (objPerfil.decCodigo == 1)
                {
                    Session[CVariablesSesion.ListaPerfiles] = objPerfil.oResultado;
                    var primerPerfil = objPerfil.oResultado.FirstOrDefault();

                    if (primerPerfil != null)
                    {
                        if (primerPerfil.NOMBRE_PERFIL == "REGULADO")
                        {
                            esAdmin = false;                            
                            cmbDepartamento.Enabled = false;
                            cmbEstadoNotif.Enabled = false;
                        }
                        else if (primerPerfil.NOMBRE_PERFIL == "FUNCIONARIO ANH" && objPerfil.oResultado.Count() > 1)
                        {
                            esAdmin = true;                            
                            cmbDepartamento.Visible = true;
                            cmbEstadoNotif.Visible = true;
                        }
                        else
                        {
                            esAdmin = false;
                            cmbDepartamento.Enabled = false;
                            cmbEstadoNotif.Enabled = false;
                        }
                    }
                }

                // ELIMINA ESTA LÍNEA porque está sobrescribiendo esAdmin
                // esAdmin = Session["EsAdmin"] != null && Convert.ToBoolean(Session["EsAdmin"]);
            }
            catch (Exception ex)
            {
                CLog.Error(this.Context, GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name, ex);
                esAdmin = false;
                cmbDepartamento.Visible = false;
                cmbEstadoNotif.Visible = false;
            }
        }

        /// <summary>
        /// Valida las fechas según el rol del usuario
        /// </summary>
        private bool ValidarFechas(DateTime fechaInicio, DateTime fechaFin)
        {
            // 1. Validar que fecha inicio no sea mayor a fecha fin
            if (fechaInicio > fechaFin)
            {
                MostrarMensaje("La fecha inicio no puede ser mayor a la fecha fin", "warning");
                return false;
            }

            // 2. Validar que fecha fin no sea mayor a hoy
            if (fechaFin > DateTime.Today)
            {
                MostrarMensaje("La fecha fin no puede ser mayor al día actual", "warning");
                return false;
            }

            // 3. Validar que estén en el mismo mes y año
            if (fechaInicio.Month != fechaFin.Month || fechaInicio.Year != fechaFin.Year)
            {
                MostrarMensaje("Las fechas deben estar dentro del mismo mes", "warning");
                return false;
            }

            // 4. Validar según el rol
            if (!esAdmin)
            {
                // REGULADO: máximo 5 días
                int diasRango = (fechaFin - fechaInicio).Days;
                if (diasRango > 5)
                {
                    MostrarMensaje("Como usuario REGULADO solo puede consultar un máximo de 5 días", "warning");
                    return false;
                }
            }

            return true;
        }            
    }

}