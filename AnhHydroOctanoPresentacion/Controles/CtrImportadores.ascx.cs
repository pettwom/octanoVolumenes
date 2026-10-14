using AnhHydroOctanoPresentacion.Lib;
using DevExpress.XtraCharts.ModelSupport;
using DevExpress.XtraReports.UI;

namespace AnhHydroOctanoPresentacion.Controles
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Web;
    using System.Web.UI;
    using AnhHydroOctanoAgenteServicio.ServicioHydroSesion;
    using AnhHydroOctanoPersistencia.Entidades;
    using Clases.Controles;
    using Clases.Persona;
    using Clases.Util;
    using Entidades;
    using Enumeradores;
    using Parametros;
    using AnhHydroOctanoPersistencia.Aplicacion.Consulta;
    using DevExpress.Web;
    using DevExpress.XtraPivotGrid;
    using Librerias.Anh.Us;
    using ServiceStack.ServiceClient.Web;
    using ServiceStack.Text;
    using UserControl = System.Web.UI.UserControl;
    using EResultado = Entidades.EResultado;
    using O_LISTA_TRANSACCION_CTY = Entidades.O_LISTA_TRANSACCION_CTY;
    using Page = System.Web.UI.Page;
    using DevExpress.Utils;

    public partial class CtrImportadores : System.Web.UI.UserControl
    {
        #region Variables de entorno

        readonly JsonServiceClient _client = new JsonServiceClient(CAppSettings.servicio_octVolumen);
        private CPersonaSesion _datSesion;
        private readonly CPersistenciaConsulta _consulta = new CPersistenciaConsulta();
        readonly CUtil _cUtil = new CUtil();
        CConsultasParametricas cConsultasParametricas = new CConsultasParametricas();

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
                        Session.Remove(CVariablesSesion.DjAcumulado);
                        Session.Remove(CVariablesSesion.EstructuraArbol);
                        Session.Remove(CVariablesSesion.GrillaAcumRep);
                        Session.Remove(CVariablesSesion.GrillaAcumulado);
                        Session.Remove(CVariablesSesion.GrillaInternacional);
                        Session.Remove(CVariablesSesion.GrillaAuxiliar);
                        Session.Remove(CVariablesSesion.IdPeriodoActual);
                        Session.Remove(CVariablesSesion.ListaParamGral);
                        Session.Remove(CVariablesSesion.OctanoParametrica);
                        Session.Remove(CVariablesSesion.ReportePrincipal);
                        Session.Remove(CVariablesSesion.ReporteSecundario);
                        Session.Remove(CVariablesSesion.ReporteTercero);
                        Session.Remove(CVariablesSesion.TipoReporte);
                        Session.Remove(CVariablesSesion.ComboTiposOperacion);

                        MCargaArbol();
                        MCargaParametricas();
                        ////MCargaPermisos();
                        ////ConfigurarFormulario();

                        tlsOperadores.DataSource = Session[CVariablesSesion.EstructuraArbol];
                        tlsOperadores.DataBind();
                        if (Session[CVariablesSesion.EntidadSeleccionada] != null)
                        {
                            var strSel = Session[CVariablesSesion.EntidadSeleccionada].ToString();
                            var vecSel = strSel.Split('-');
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
                        else
                        {
                            //todo No es carga inicial: no implementado
                        }
                    }
                    else
                    {
                        //todo No es carga inicial: no implementado
                    }

                    if (tlsOperadores.DataSource == null && Session[CVariablesSesion.EstructuraArbol] != null)
                    {
                        tlsOperadores.DataSource = Session[CVariablesSesion.EstructuraArbol];
                        tlsOperadores.DataBind();
                    }
                    else
                    {
                        //todo No es carga inicial: no implementado
                    }

                    ////if (pvgAcumulado.DataSource == null && Session[CVariablesSesion.GrillaAcumulado] != null)
                    ////{
                    ////    pvgAcumulado.DataSource = Session[CVariablesSesion.GrillaAcumulado];
                    ////    pvgAcumulado.DataBind();
                    ////}
                    ////else
                    ////{
                    ////    //todo No es carga inicial: no implementado
                    ////}
                    ////if (dgvInternacional.DataSource == null && Session[CVariablesSesion.GrillaInternacional] != null)
                    ////{
                    ////    dgvInternacional.DataSource = Session[CVariablesSesion.GrillaInternacional];
                    ////    dgvInternacional.DataBind();
                    ////}
                    ////else
                    ////{
                    ////    //todo No es carga inicial: no implementado
                    ////}
                    ////if (dgvContenedor.DataSource == null && Session[CVariablesSesion.GrillaAuxiliar] != null)
                    ////{
                    ////    dgvContenedor.DataSource = Session[CVariablesSesion.GrillaAuxiliar];
                    ////    dgvContenedor.DataBind();
                    ////}
                    ////else
                    ////{
                    ////    //todo No es carga inicial: no implementado
                    ////}

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

        private bool MValidaSesion()
        {
            bool booResultado;
            //borrar
            //Session[CVariablesSesion.IdUsuario] = "1802";
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

        private void MCargaArbol()
        {
            var objArbol = Session[CVariablesSesion.ObjetoArbol] as List<O_OBTIENE_ENTIDADES_CTY>;
            if (objArbol == null)
            {
                var objResultado = _consulta.ObtenerEntidades(
                new EObjetoEnvio5ParametrosProto
                {
                    decFiltro = 1,
                    strParametro1 = Session[CVariablesSesion.IdUsuario].ToString(),
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
                    MImprimirMensaje(objResultado.strMensaje, EnumTiposMensajeAlerta.Error);
                    return;
                }
            }

            objArbol = objArbol.Where(x => x.ID_ACTIVIDAD == 16 || x.ID_ACTIVIDAD == 36).ToList();

            Session[CVariablesSesion.EstructuraArbol] = _cUtil.MObtenerArbolOrdenado(Properties.Settings.Default.intOrdenArbol, objArbol);
        }

        private void MCargaParametricas()
        {
            var objUbicacion = _client.Post<EResultadoUbicacion>(CAppSettings.svc_obtiene_ubicacion,
            new
            {
                strCredencial = CParametrosHydro.strCredencialFuncionario,
                decNivel = 2,
                decIdPadre = 120
            });
            if (objUbicacion.IntCodigo == 1)
            {
                Session[CVariablesSesion.ListaParamGral] = objUbicacion.OResultado;
            }
            else
            {
                Session[CVariablesSesion.ListaParamGral] = null;
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
                    strMensaje = "";
                    break;
                default:
                    pnlMensaje.Visible = false;
                    lblMensajeError.ForeColor = Color.WhiteSmoke;
                    strMensaje = "-";
                    break;
            }
            lblMensajeError.Text = "   " + strMensaje + "   ";
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError", "alert('" + strMensaje + "');", true);
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

        public string[] listaF1;

        protected void pnlDatos_Callback(object sender, CallbackEventArgsBase e)
        {
            try 
            {
                Session.Remove(CVariablesSesion.GrillaTransaccion);
                Session.Remove(CVariablesSesion.GrillaFosil);
                Session.Remove(CVariablesSesion.Grillabase);
                
                var txtPlanta = "";

                decimal decValidar = 0;

                decimal decOpcion = Convert.ToDecimal(cmbResMin.Value);
                
                string strDuiSeleccionado = "";
                //string strDuiSeleccionado = cmbListadoDui.Value.ToString();

                if (cmbListadoDui.Value == null)
                {
                    strDuiSeleccionado = "";
                }
                else
                {
                    strDuiSeleccionado = cmbListadoDui.Value.ToString();
                }


                if (decOpcion == 1)
                {
                    if (strDuiSeleccionado != "")
                    {
                        decValidar = 1;
                    }
                    else
                    {
                        decValidar = 0;
                    }  
                }
                else if (decOpcion == 2)
                {
                    decValidar = 1;
                }
                else
                {
                    decValidar = 0;
                }                

                dgvAnexoA.DataSource = null;
                dgvAnexoA.DataBind();
                dgvAnexoB1.DataSource = null;
                dgvAnexoB1.DataBind();
                dgvAnexoB2.DataSource = null;
                dgvAnexoB2.DataBind();
                dgvAnexoB3.DataSource = null;
                dgvAnexoB3.DataBind();
                dgvAnexoC.DataSource = null;
                dgvAnexoC.DataBind();
                dgvAnexoD1.DataSource = null;
                dgvAnexoD1.DataBind();
                dgvAnexoD2.DataSource = null;
                dgvAnexoD2.DataBind();
                dgvAnexoD3.DataSource = null;
                dgvAnexoD3.DataBind();
                dgvAnexoE.DataSource = null;
                dgvAnexoE.DataBind();
                dgvAnexoG1.DataSource = null;
                dgvAnexoG1.DataBind();
                dgvAnexoG2.DataSource = null;
                dgvAnexoG2.DataBind();
                dgvAnexoH.DataSource = null;
                dgvAnexoH.DataBind();
                dgvAnexoI1.DataSource = null;
                dgvAnexoI1.DataBind();
                dgvAnexoI2.DataSource = null;
                dgvAnexoI2.DataBind();


                dgvVolImportado.DataSource = null;
                dgvVolImportado.DataBind();
                dgvCostosErogados.DataSource = null;
                dgvCostosErogados.DataBind();
                dgvCostosF2.DataSource = null;
                dgvCostosF2.DataBind();
                dgvCostoSubvencion.DataSource = null;
                dgvCostoSubvencion.DataBind();
                dgvSubvencionTotal.DataSource = null;
                dgvSubvencionTotal.DataBind();
                dgvCostoFinancieroProducto.DataSource = null;
                dgvCostoFinancieroProducto.DataBind();
                dgvCostoFinancieroServicio.DataSource = null;
                dgvCostoFinancieroServicio.DataBind();

                lblNombreEmpresa.Text = "a) Nombre de la Empresa: ";
                lblMontoCompensar.Text = "a) Monto total solicitado para compensar: ";
                chbYacimiento.Checked = false;
                chbLogistica.Checked = false;
                txtDuiRegularizadaF2.Text = "";
                txtFechaDuiF2.Text = "";
                txtFronteraIngresoF2.Text = "";
                txtIncontermsF2.Text = "";
                txtPrecioGasolinaBlancaF2.Text = "";
                txtPrecioNetoIvaF2.Text = "";
                txtTarifaPoliductoF2.Text = "";
                txtTarifaNetoIvaF2.Text = "";
                txtCostoGasolinaBlancaNeto.Text = "";


                var objNuevoDatoA = new List<O_OBTENER_ANEXO_A>();
                var objNuevoDatoAnGb = new List<O_OBTENER_ANEXO_A_GB>();


                var objVolImportA = new List<O_OBTENER_VOLUMEN_IMPORTADO>();
                var objVolImportB = new List<O_OBTENER_VOLUMEN_IMPORTADO>();
                var objVolImportC = new List<O_OBTENER_VOLUMEN_IMPORTADO>();
                var objVolImportD = new List<O_OBTENER_VOLUMEN_IMPORTADO>();
                var objVolImportE = new List<O_OBTENER_VOLUMEN_IMPORTADO>();
                var objVolImportF = new List<O_OBTENER_VOLUMEN_IMPORTADO>();
                var objVolImportG = new List<O_OBTENER_VOLUMEN_IMPORTADO>();
                var objVolImportH = new List<O_OBTENER_VOLUMEN_IMPORTADO>();
                var objVolImportI = new List<O_OBTENER_VOLUMEN_IMPORTADO>();

                decimal decIdMovimiento = 0;

                var objCostos = new List<O_OBTENER_COSTOS_IMPORTADO>();
                var objCostosSubvencion = new List<O_OBTENER_COSTOS_SUBVENCION>();
                var objCostosSubvencionFinal = new List<O_OBTENER_COSTOS_SUBVENCION_FINAL>();

                var objCostosUni = new List<O_OBTENER_COSTOS_UNITARIO>();

                var objCostosUniSub1 = new List<O_OBTENER_COSTOS_UNITARIO>();
                var objCostosUniSub2 = new List<O_OBTENER_COSTOS_UNITARIO>();

                var objCostosTotSub = new List<O_OBTENER_COSTOS_TOT_SUB>();

                List<O_LISTA_TRANSACCION_CTY> lstResultadoFosilTotal = new List<O_LISTA_TRANSACCION_CTY>();

                List<O_LISTA_TRANSACCION_CTY> lstResultadoBaseTotal = new List<O_LISTA_TRANSACCION_CTY>();
                                
                string strFechaInicio = "";
                string strFechaFin = "";
                string strFechaInicioT = "";
                string strFechaFinT = "";

                if (decValidar == 1)
                {
                    if (decOpcion == 1)
                    {
                        strFechaInicio = detFechaIni.Date.Year + detFechaIni.Date.Month.ToString().PadLeft(2, '0') + detFechaIni.Date.Day.ToString().PadLeft(2, '0') + "000000";
                        strFechaFin = detFechaIni.Date.Year + detFechaIni.Date.Month.ToString().PadLeft(2, '0') + detFechaIni.Date.Day.ToString().PadLeft(2, '0') + "235959";
                        strFechaInicioT = detFechaIni.Date.Year + detFechaIni.Date.Month.ToString().PadLeft(2, '0') + detFechaIni.Date.Day.ToString().PadLeft(2, '0');
                        strFechaFinT = detFechaIni.Date.Year + detFechaIni.Date.Month.ToString().PadLeft(2, '0') + detFechaIni.Date.Day.ToString().PadLeft(2, '0');
                    }
                    else
                    {
                        strFechaInicio = detFechaIni.Date.Year + detFechaIni.Date.Month.ToString().PadLeft(2, '0') + "01000000";
                        strFechaFin = detFechaIni.Date.Year + detFechaIni.Date.Month.ToString().PadLeft(2, '0') + "31235959";
                        strFechaInicioT = detFechaIni.Date.Year + detFechaIni.Date.Month.ToString().PadLeft(2, '0') + "01";
                        strFechaFinT = detFechaIni.Date.Year + detFechaIni.Date.Month.ToString().PadLeft(2, '0') + "31";
                    }

                    var lstResultadoMovBase = new List<O_CONSULTA_MOVIM_TRAN_CTY>();
                    var lstResultadoMovFosil = new List<O_CONSULTA_MOVIM_TRAN_CTY>();
                    var lstResultadoMovGasBlanca = new List<O_CONSULTA_MOVIM_TRAN_CTY>();
                    var lstResultadoMovOtro = new List<O_CONSULTA_MOVIM_TRAN_CTY>();

                    var lstMovimiento = _consulta.ObtenerMovimiento("",
                        new EObjetoEnvio8ParametrosProto
                        {
                            decFiltro = 4, //Filtro 1 por producto //2 consulta por infraestructura
                            strParametro1 = tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString(), //Id proyecto/entidad
                            strParametro2 = strFechaInicio,//detFechaIni.Date.Year + detFechaIni.Date.Month.ToString().PadLeft(2, '0') + "01000000",//fecha inicial
                            strParametro3 = strFechaFin,//detFechaIni.Date.Year + detFechaIni.Date.Month.ToString().PadLeft(2, '0') + "31235959", //fecha final
                            strParametro4 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(), //id Actividad
                            strParametro5 = "249", //tipo operacion
                            strParametro6 = "0", //idProducto 
                            strParametro7 = "0", // id_infraestructura
                            strParametro8 = "0" // tipo_infraestructura
                        });

                    if (lstMovimiento.intCodigo == 1)
                    {
                        var lstResultado = (List<O_CONSULTA_MOVIM_TRAN_CTY>)lstMovimiento.oResultado;
                        if (lstResultado.Any())
                        {

                            var registros = lstResultado;
                            Session[CVariablesSesion.GrillaTransaccion] = registros;

                            foreach (var dato in registros)
                            {
                                int intProducto = Convert.ToInt32(dato.ID_HIDROCARBURO);
                                decIdMovimiento = Convert.ToDecimal(dato.ID_MOVIMIENTO);
                                switch (intProducto)
                                {
                                    case 401://test 401 desarrollo 4326 produccion 1829
                                    case 402://test 402 desarrollo 4327 produccion 1830
                                    case 403://test 403 desarrollo 4328 produccion 1831
                                    case 404://test 404 desarrollo 4329 produccion 1832
                                    case 405://test 405 desarrollo 4330 produccion 1833

                                        lstResultadoMovFosil.Add(dato);
                                        var objTransaccionFosil = _client.Post<EResultadoTransaccion>(CAppSettings.svc_obtener_transaccion,
                                        new
                                        {
                                            decFiltro = "1",
                                            strParametro1 = decIdMovimiento.ToString(),
                                            strParametro2 = strFechaInicioT,//detFechaIni.Date.Year + detFechaIni.Date.Month.ToString().PadLeft(2, '0') + detFechaIni.Date.Day.ToString().PadLeft(2, '0'),
                                            strParametro3 = strFechaFinT,//detFechaIni.Date.Year + detFechaIni.Date.Month.ToString().PadLeft(2, '0') + detFechaIni.Date.Day.ToString().PadLeft(2, '0'),
                                            strParametro4 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
                                            strParametro5 = "0"
                                        });
                                        if (objTransaccionFosil != null && objTransaccionFosil.intCodigo == 1)
                                        {
                                            List<O_LISTA_TRANSACCION_CTY> lstResultadoFosil = objTransaccionFosil.oResultado;

                                            if ((List<O_LISTA_TRANSACCION_CTY>)Session[CVariablesSesion.GrillaFosil] != null)
                                            {
                                                lstResultadoFosilTotal = (List<O_LISTA_TRANSACCION_CTY>)Session[CVariablesSesion.GrillaFosil];
                                            }

                                            foreach (O_LISTA_TRANSACCION_CTY datos in lstResultadoFosil)
                                            {
                                                lstResultadoFosilTotal.Add(datos);
                                            }

                                            Session[CVariablesSesion.GrillaFosil] = lstResultadoFosilTotal;

                                        }
                                        else { }
                                        break;
                                    case 367: //test 367 desarrollo 367 produccion 367
                                    case 368: //test 368 desarrollo 368 produccion 368
                                    case 369: //test 369 desarrollo 369 produccion 369
                                    case 370: //test 370 desarrollo 370 produccion 370
                                    case 371: //test 371 desarrollo 371 produccion 371
                                    case 372: //test 372 desarrollo 372 produccion 372
                                    case 374: //test 374 desarrollo 374 produccion 373
                                    case 380: //test 380 desarrollo 380 produccion 374
                                    case 382: //test 382 desarrollo 382 produccion 375
                                    case 383: //test 383 desarrollo 383 produccion 376
                                    case 384: //test 384 desarrollo 384 produccion 377
                                    case 385: //test 385 desarrollo 385 produccion 378
                                    case 386: //test 386 desarrollo 386 produccion 379
                                    case 387: //test 387 desarrollo 387 produccion 380
                                    case 388: //test 388 desarrollo 388 produccion 381
                                    case 389: //test 389 desarrollo 389 produccion 382

                                        if (dato.OBSERVACION_MOVIMIENTO.StartsWith(strDuiSeleccionado))
                                        {
                                            lstResultadoMovBase.Add(dato);

                                            var objTransaccionBase = _client.Post<EResultadoTransaccion>(CAppSettings.svc_obtener_transaccion,
                                            new
                                            {
                                                decFiltro = "1",
                                                strParametro1 = decIdMovimiento.ToString(),
                                                strParametro2 = strFechaInicioT,//detFechaIni.Date.Year + detFechaIni.Date.Month.ToString().PadLeft(2, '0') + detFechaIni.Date.Day.ToString().PadLeft(2, '0'),
                                                strParametro3 = strFechaFinT,//detFechaIni.Date.Year + detFechaIni.Date.Month.ToString().PadLeft(2, '0') + detFechaIni.Date.Day.ToString().PadLeft(2, '0'),
                                                strParametro4 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
                                                strParametro5 = "0"
                                            });
                                            if (objTransaccionBase != null && objTransaccionBase.intCodigo == 1)
                                            {
                                                //List<O_LISTA_TRANSACCION_CTY> lstResultado2 = objTransaccionBase.oResultado;

                                                //Session[CVariablesSesion.Grillabase] = lstResultado2;


                                                List<O_LISTA_TRANSACCION_CTY> lstResultadoBaseTran = objTransaccionBase.oResultado;

                                                if ((List<O_LISTA_TRANSACCION_CTY>)Session[CVariablesSesion.Grillabase] != null)
                                                {
                                                    lstResultadoBaseTotal = (List<O_LISTA_TRANSACCION_CTY>)Session[CVariablesSesion.Grillabase];
                                                }

                                                foreach (O_LISTA_TRANSACCION_CTY datos in lstResultadoBaseTran)
                                                {
                                                    lstResultadoBaseTotal.Add(datos);
                                                }

                                                Session[CVariablesSesion.Grillabase] = lstResultadoBaseTotal;


                                            }
                                            else { }
                                        }

                                        break;
                                    case 278:

                                        lstResultadoMovGasBlanca.Add(dato);

                                        var objTransaccionGasolinaBlanca = _client.Post<EResultadoTransaccion>(CAppSettings.svc_obtener_transaccion,
                                            new
                                            {
                                                decFiltro = "1",
                                                strParametro1 = decIdMovimiento.ToString(),
                                                strParametro2 = strFechaInicioT,//detFechaIni.Date.Year + detFechaIni.Date.Month.ToString().PadLeft(2, '0') + detFechaIni.Date.Day.ToString().PadLeft(2, '0'),
                                                strParametro3 = strFechaFinT,//detFechaIni.Date.Year + detFechaIni.Date.Month.ToString().PadLeft(2, '0') + detFechaIni.Date.Day.ToString().PadLeft(2, '0'),
                                                strParametro4 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
                                                strParametro5 = "0"
                                            });
                                        if (objTransaccionGasolinaBlanca != null && objTransaccionGasolinaBlanca.intCodigo == 1)
                                        {
                                            List<O_LISTA_TRANSACCION_CTY> lstResultado2 = objTransaccionGasolinaBlanca.oResultado;

                                            Session[CVariablesSesion.GrillaGasolinaBlanca] = lstResultado2;
                                        }
                                        else { }
                                        break;
                                    default:

                                        lstResultadoMovOtro.Add(dato);

                                        var objTransaccionOtro = _client.Post<EResultadoTransaccion>(CAppSettings.svc_obtener_transaccion,
                                            new
                                            {
                                                decFiltro = "1",
                                                strParametro1 = decIdMovimiento.ToString(),
                                                strParametro2 = strFechaInicioT,//detFechaIni.Date.Year + detFechaIni.Date.Month.ToString().PadLeft(2, '0') + detFechaIni.Date.Day.ToString().PadLeft(2, '0'),
                                                strParametro3 = strFechaFinT,//detFechaIni.Date.Year + detFechaIni.Date.Month.ToString().PadLeft(2, '0') + detFechaIni.Date.Day.ToString().PadLeft(2, '0'),
                                                strParametro4 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
                                                strParametro5 = "0"
                                            });
                                        if (objTransaccionOtro != null && objTransaccionOtro.intCodigo == 1)
                                        {
                                            List<O_LISTA_TRANSACCION_CTY> lstResultado2 = objTransaccionOtro.oResultado;

                                            Session[CVariablesSesion.GrillaOtro] = lstResultado2;
                                        }
                                        else { }
                                        break;
                                }
                            }

                            var lstDocumento = new List<O_LISTA_TRANSACCION_CTY>();
                            var lstDocumentoGb = new List<O_LISTA_TRANSACCION_CTY>();

                            if (decOpcion == 1)
                            {
                                if (Session[CVariablesSesion.Grillabase] != null)
                                {
                                    lstDocumento = (List<O_LISTA_TRANSACCION_CTY>)Session[CVariablesSesion.Grillabase];
                                }
                                else { }
                            }
                            else
                            {
                                if (Session[CVariablesSesion.GrillaFosil] != null)
                                {
                                    lstDocumento = (List<O_LISTA_TRANSACCION_CTY>)Session[CVariablesSesion.GrillaFosil];
                                }

                                if (Session[CVariablesSesion.GrillaGasolinaBlanca] != null)
                                {
                                    lstDocumentoGb = (List<O_LISTA_TRANSACCION_CTY>)Session[CVariablesSesion.GrillaGasolinaBlanca];
                                }
                                else { }
                            }

                            if (lstDocumento != null)
                            {

                                //ANEXO A
                                #region ANEXO A

                                lstDocumento = lstDocumento.Where(x => x.OBSERVACIONES != null).ToList();

                                var objMovA = lstDocumento.Where(x => x.OBSERVACIONES.StartsWith("ACCP")).ToList();

                                foreach (var reg in objMovA)
                                {
                                    var objTrasaccion = new O_OBTENER_ANEXO_A();
                                    var objVol = new O_OBTENER_VOLUMEN_IMPORTADO();

                                    var parametros = reg.OBSERVACIONES;

                                    string[] lista = parametros.Split('|');

                                    objTrasaccion.NRO_DUI = lista[1].ToString();
                                    objTrasaccion.PROVEEDOR = reg.RAZON_SOCIAL;
                                    objTrasaccion.RON = reg.CODIGO;
                                    objTrasaccion.MES = ObtenerPeriodo(reg.FECHA_HORA_EMISION.ToString());
                                    objTrasaccion.NRO_FACTURA = reg.NRO_TRANSACCION;
                                    objTrasaccion.VOLUMEN_FACTURADO = Convert.ToDecimal(lista[5].ToString());
                                    objTrasaccion.VALOR_FACTURADO = reg.PRECIO_TOTAL;

                                    objNuevoDatoA.Add(objTrasaccion);

                                    if (decOpcion == 2)
                                    {
                                        objVol.IDMOVIMIENTO = reg.ID_MOVIMIENTO;
                                        objVol.RON = reg.CODIGO;
                                        objVol.VOLUMEN = Convert.ToDecimal(lista[5].ToString());
                                        objVol.COSTO = reg.PRECIO_TOTAL;

                                        objVolImportA.Add(objVol);

                                    }
                                    else
                                    {

                                        //var registrosBase = lstDocumento;

                                        //decIdMovimiento = Convert.ToDecimal(registrosBase[0].ID_MOVIMIENTO);

                                        //if (reg.ID_MOVIMIENTO == decIdMovimiento)
                                        //{
                                            objVol.IDMOVIMIENTO = reg.ID_MOVIMIENTO;
                                            objVol.RON = reg.CODIGO;
                                            objVol.VOLUMEN = Convert.ToDecimal(lista[5].ToString());
                                            objVol.COSTO = reg.PRECIO_TOTAL;

                                            objVolImportA.Add(objVol);
                                        //}
                                    }


                                }

                                dgvAnexoA.DataSource = objNuevoDatoA;
                                dgvAnexoA.DataBind();

                                Session[CVariablesSesion.GrillaAnexoA] = objNuevoDatoA;

                                if (decOpcion == 2)
                                {
                                    lstDocumentoGb = lstDocumentoGb.Where(x => x.OBSERVACIONES != null).ToList();

                                    var objMovAnGb = lstDocumentoGb.Where(x => x.OBSERVACIONES.StartsWith("ACCP")).ToList();

                                    foreach (var reg in objMovAnGb)
                                    {
                                        var objTrasaccion = new O_OBTENER_ANEXO_A_GB();
                                        var objVol = new O_OBTENER_VOLUMEN_IMPORTADO();

                                        var parametros = reg.OBSERVACIONES;

                                        string[] lista = parametros.Split('|');

                                        objTrasaccion.PRODUCTO = "GASOLINA BLANCA";
                                        objTrasaccion.MES_RECIBIDO = ObtenerPeriodo(reg.FECHA_HORA_EMISION.ToString());
                                        objTrasaccion.VOLUMEN = Convert.ToDecimal(lista[5].ToString()).ToString().Replace(".", ",");
                                        objTrasaccion.PRECIO = reg.PRECIO_UNITARIO.ToString().Replace(".", ",");
                                        objTrasaccion.MONTO_NETO = (Convert.ToDecimal(lista[5].ToString().Replace(",", ".")) * Convert.ToDecimal(reg.PRECIO_UNITARIO.ToString().Replace(",", "."))).ToString();

                                        objNuevoDatoAnGb.Add(objTrasaccion);

                                        /*if (reg.ID_MOVIMIENTO == decIdMovimiento)
                                        {
                                            objVol.RON = reg.CODIGO;
                                            objVol.VOLUMEN = Convert.ToDecimal(lista[5].ToString());
                                            objVol.COSTO = reg.PRECIO_TOTAL;

                                            objVolImportA.Add(objVol);
                                        }*/
                                    }

                                    dgvAnexoAGb.DataSource = objNuevoDatoAnGb;
                                    dgvAnexoAGb.DataBind();

                                    Session[CVariablesSesion.GrillaAnexoAnGb] = objNuevoDatoAnGb;
                                }

                                #endregion

                                //ANEXO B
                                #region ANEXO B

                                txtPlantaOrigen.Text = "";
                                txtPlantaDestino.Text = "";

                                lstDocumento = lstDocumento.Where(x => x.OBSERVACIONES != null).ToList();
                                var objMovB1 = lstDocumento.Where(x => x.OBSERVACIONES.StartsWith("B1CCTF")).ToList();

                                var objVolB = new O_OBTENER_VOLUMEN_IMPORTADO();

                                var objNuevoDatoB1 = new List<O_OBTENER_ANEXO_B1>();
                                var objNuevoDatoB1Gb = new List<O_OBTENER_ANEXO_B1>();

                                foreach (var reg in objMovB1)
                                {
                                    var objTrasaccion = new O_OBTENER_ANEXO_B1();
                                    objVolB = new O_OBTENER_VOLUMEN_IMPORTADO();

                                    var parametros = reg.OBSERVACIONES;

                                    string[] lista = parametros.Split('|');

                                    var decVolDespachado = Convert.ToDecimal(lista[4].ToString());
                                    var decVolRecibido = Convert.ToDecimal(lista[5].ToString());

                                    var decVolDiferencia = decVolDespachado - decVolRecibido;

                                    var decMerma = decVolDiferencia <= 150 ? (decVolRecibido * Convert.ToDecimal("0.0025")) : 0;

                                    decimal decPreUni = lista[10].ToString() == "" ? 0 : Convert.ToDecimal(lista[10].ToString());

                                    objTrasaccion.NRO_DUI = lista[1].ToString();
                                    objTrasaccion.MES = reg.RAZON_SOCIAL;
                                    objTrasaccion.NRO_FACTURA_TRANSPORTE = reg.NRO_TRANSACCION;
                                    objTrasaccion.VOLUMEN_DESPACHADO = Convert.ToDecimal(lista[4].ToString());
                                    objTrasaccion.VOLUMEN_RECIBIDO = Convert.ToDecimal(lista[5].ToString());
                                    objTrasaccion.DIFERENCIA = Convert.ToDecimal(lista[4].ToString()) - Convert.ToDecimal(lista[5].ToString());
                                    objTrasaccion.MERMA_PERMISIBLE = Math.Round(decMerma, 0);
                                    objTrasaccion.MERMA_EXCEDENTE = Math.Round(decVolDiferencia - decMerma, 0) > 0 ? Math.Round(decVolDiferencia - decMerma, 0) : 0;
                                    objTrasaccion.FLETE = Convert.ToDecimal(lista[7].ToString());
                                    objTrasaccion.COSTO_PRODUCTO = decPreUni;
                                    objTrasaccion.TRANSPORTE_TOTAL = decVolRecibido * Convert.ToDecimal(lista[7].ToString());
                                    objTrasaccion.DESCUENTO_MERMA = (Math.Round(decVolDiferencia - decMerma, 0) > 0 ? Math.Round(decVolDiferencia - decMerma, 0) : 0) * Convert.ToDecimal(lista[10].ToString());
                                    objTrasaccion.COSTO_TRANSPORTE = (decVolRecibido * Convert.ToDecimal(lista[7].ToString())) - ((Math.Round(decVolDiferencia - decMerma, 0) > 0 ? Math.Round(decVolDiferencia - decMerma, 0) : 0) * decPreUni);

                                    objNuevoDatoB1.Add(objTrasaccion);

                                    //if (reg.ID_MOVIMIENTO == decIdMovimiento)
                                    //{
                                        objVolB.IDMOVIMIENTO = reg.ID_MOVIMIENTO;
                                        objVolB.RON = reg.CODIGO;
                                        objVolB.VOLUMEN = Convert.ToDecimal(lista[5].ToString());
                                        objVolB.COSTO = (decVolRecibido * Convert.ToDecimal(lista[7].ToString())) - ((Math.Round(decVolDiferencia - decMerma, 0) > 0 ? Math.Round(decVolDiferencia - decMerma, 0) : 0) * decPreUni);

                                        objVolImportB.Add(objVolB);
                                    //}

                                }

                                dgvAnexoB1.DataSource = objNuevoDatoB1;
                                dgvAnexoB1.DataBind();

                                Session[CVariablesSesion.GrillaAnexoB1] = objNuevoDatoB1;

                                if (decOpcion == 2)
                                {
                                    lstDocumentoGb = lstDocumentoGb.Where(x => x.OBSERVACIONES != null).ToList();

                                    var objMovB1Gb = lstDocumentoGb.Where(x => x.OBSERVACIONES.StartsWith("B1CCTF")).ToList();


                                    foreach (var reg in objMovB1Gb)
                                    {
                                        var objTrasaccion = new O_OBTENER_ANEXO_B1();
                                        objVolB = new O_OBTENER_VOLUMEN_IMPORTADO();

                                        var parametros = reg.OBSERVACIONES;

                                        string[] lista = parametros.Split('|');

                                        decimal decPreUni = lista[10].ToString() == "" ? 0 : Convert.ToDecimal(lista[10].ToString());

                                        var decVolDespachado = Convert.ToDecimal(lista[4].ToString());
                                        var decVolRecibido = Convert.ToDecimal(lista[5].ToString());

                                        var decVolDiferencia = decVolDespachado - decVolRecibido;

                                        var decMerma = decVolDiferencia <= 150 ? (decVolRecibido * Convert.ToDecimal("0.0025")) : 0;

                                        objTrasaccion.NRO_DUI = "";
                                        objTrasaccion.MES = reg.RAZON_SOCIAL;
                                        objTrasaccion.NRO_FACTURA_TRANSPORTE = reg.NRO_TRANSACCION;
                                        objTrasaccion.VOLUMEN_DESPACHADO = Convert.ToDecimal(lista[4].ToString());
                                        objTrasaccion.VOLUMEN_RECIBIDO = Convert.ToDecimal(lista[5].ToString());
                                        objTrasaccion.DIFERENCIA = Convert.ToDecimal(lista[4].ToString()) - Convert.ToDecimal(lista[5].ToString());
                                        objTrasaccion.MERMA_PERMISIBLE = Math.Round(decMerma, 0);
                                        objTrasaccion.MERMA_EXCEDENTE = Math.Round(decVolDiferencia - decMerma, 0) > 0 ? Math.Round(decVolDiferencia - decMerma, 0) : 0;
                                        objTrasaccion.FLETE = Convert.ToDecimal(lista[7].ToString());
                                        objTrasaccion.COSTO_PRODUCTO = decPreUni;
                                        objTrasaccion.TRANSPORTE_TOTAL = decVolRecibido * Convert.ToDecimal(lista[7].ToString());
                                        objTrasaccion.DESCUENTO_MERMA = (Math.Round(decVolDiferencia - decMerma, 0) > 0 ? Math.Round(decVolDiferencia - decMerma, 0) : 0) * decPreUni;
                                        objTrasaccion.COSTO_TRANSPORTE = (decVolRecibido * Convert.ToDecimal(lista[7].ToString())) - ((Math.Round(decVolDiferencia - decMerma, 0) > 0 ? Math.Round(decVolDiferencia - decMerma, 0) : 0) * decPreUni);

                                        objNuevoDatoB1Gb.Add(objTrasaccion);

                                        /*if (reg.ID_MOVIMIENTO == decIdMovimiento)
                                        {
                                            objVolB.RON = reg.CODIGO;
                                            objVolB.VOLUMEN = Convert.ToDecimal(lista[5].ToString());
                                            objVolB.COSTO = (decVolRecibido * Convert.ToDecimal(lista[7].ToString())) - ((Math.Round(decVolDiferencia - decMerma, 0) > 0 ? Math.Round(decVolDiferencia - decMerma, 0) : 0) * reg.PRECIO_UNITARIO);

                                            objVolImportB.Add(objVolB);
                                        }*/
                                    }

                                    dgvAnexoB1Gb.DataSource = objNuevoDatoB1Gb;
                                    dgvAnexoB1Gb.DataBind();

                                    Session[CVariablesSesion.GrillaAnexoB1Gb] = objNuevoDatoB1Gb;

                                }

                                var objMovB2 = lstDocumento.Where(x => x.OBSERVACIONES.StartsWith("B2CCTT")).ToList();

                                var objNuevoDatoB2 = new List<O_OBTENER_ANEXO_B2>();
                                var objNuevoDatoB2Gb = new List<O_OBTENER_ANEXO_B2>();
                                var objNuevoDatoB2GbD = new List<O_OBTENER_ANEXO_B2_DU>();

                                var lstrPlantaOrigen = new List<string>();
                                var lstrPlantaDestino = new List<string>();

                                foreach (var reg in objMovB2)
                                {
                                    var objTrasaccion = new O_OBTENER_ANEXO_B2();
                                    objVolB = new O_OBTENER_VOLUMEN_IMPORTADO();

                                    var parametros = reg.OBSERVACIONES;

                                    string[] lista = parametros.Split('|');

                                    string[] listaProveedor = lista[6].Split('-');

                                    var decVolDespachado = Convert.ToDecimal(lista[4].ToString());
                                    var decVolRecibido = Convert.ToDecimal(lista[5].ToString());

                                    decimal decPreUni = lista[10].ToString() == "" ? 0 : Convert.ToDecimal(lista[10].ToString());

                                    var decVolDiferencia = decVolDespachado - decVolRecibido;

                                    var decMerma = decVolDiferencia <= 150 ? (decVolRecibido * Convert.ToDecimal("0.0025")) : 0;

                                    objTrasaccion.NRO_DUI = lista[1].ToString();
                                    if (listaProveedor.Count() == 2)
                                    {
                                        objTrasaccion.NRO_FACTURA_PROVEEDOR = listaProveedor[0].ToString();
                                    }
                                    else
                                    {
                                        objTrasaccion.NRO_FACTURA_PROVEEDOR = "";
                                    }

                                    objTrasaccion.EMPRESA_TRANSPORTE = reg.RAZON_SOCIAL;
                                    objTrasaccion.NRO_FACTURA_TRANSPORTE = reg.NRO_TRANSACCION;
                                    objTrasaccion.PLANTA_DESPACHO = lista[2].ToString();
                                    objTrasaccion.PLANTA_RECEPCION = lista[3].ToString();
                                    objTrasaccion.VOLUMEN_DESPACHADO = decVolDespachado;
                                    objTrasaccion.VOLUMEN_RECIBIDO = decVolRecibido;
                                    objTrasaccion.DIFERENCIA = decVolDiferencia * -1;
                                    objTrasaccion.MERMA_PERMISIBLE = Math.Round(decMerma, 0);
                                    objTrasaccion.MERMA_EXCEDENTE = Math.Round(decVolDiferencia - decMerma, 0) > 0 ? Math.Round(decVolDiferencia - decMerma, 0) : 0;
                                    objTrasaccion.FLETE = Convert.ToDecimal(lista[7].ToString());
                                    objTrasaccion.COSTO_PRODUCTO = decPreUni;
                                    objTrasaccion.TRANSPORTE_TOTAL = decVolRecibido * Convert.ToDecimal(lista[7].ToString());
                                    objTrasaccion.DESCUENTO_MERMA = (Math.Round(decVolDiferencia - decMerma, 0) > 0 ? Math.Round(decVolDiferencia - decMerma, 0) : 0) * decPreUni;
                                    objTrasaccion.COSTO_TRANSPORTE = (decVolRecibido * Convert.ToDecimal(lista[7].ToString())) - ((Math.Round(decVolDiferencia - decMerma, 0) > 0 ? Math.Round(decVolDiferencia - decMerma, 0) : 0) * decPreUni);

                                    objNuevoDatoB2.Add(objTrasaccion);
                                    
                                    //if (reg.ID_MOVIMIENTO == decIdMovimiento)
                                    //{
                                        objVolB.IDMOVIMIENTO = reg.ID_MOVIMIENTO;
                                        objVolB.RON = reg.CODIGO;
                                        objVolB.VOLUMEN = 0;
                                        objVolB.COSTO = (decVolRecibido * Convert.ToDecimal(lista[7].ToString())) - ((Math.Round(decVolDiferencia - decMerma, 0) > 0 ? Math.Round(decVolDiferencia - decMerma, 0) : 0) * decPreUni);

                                        objVolImportB.Add(objVolB);
                                    //}

                                    txtPlanta = lista[3].ToString();

                                    lstrPlantaOrigen.Add(lista[2].ToString());
                                    lstrPlantaDestino.Add(lista[3].ToString());
                                }

                                dgvAnexoB2.DataSource = objNuevoDatoB2;
                                dgvAnexoB2.DataBind();
                                
                                Session[CVariablesSesion.GrillaAnexoB2] = objNuevoDatoB2;

                                var lstPlantaO = lstrPlantaOrigen.Distinct().ToList();
                                string strPlantaO = "";
                                var lstPlantaD = lstrPlantaDestino.Distinct().ToList();
                                string strPlantaD = "";

                                foreach (var itemO in lstPlantaO)
                                {
                                    if (txtPlantaOrigen.Text == "")
                                    { txtPlantaOrigen.Text = txtPlantaOrigen.Text.ToUpper() + strPlantaO.ToUpper(); }
                                    else { txtPlantaOrigen.Text = txtPlantaOrigen.Text.ToUpper() + ", " + strPlantaO.ToUpper(); }
                                    strPlantaO = itemO.ToUpper();
                                }

                                if (txtPlantaOrigen.Text == "")
                                { txtPlantaOrigen.Text = txtPlantaOrigen.Text.ToUpper() + strPlantaO.ToUpper(); }
                                else { txtPlantaOrigen.Text = txtPlantaOrigen.Text.ToUpper() + " y " + strPlantaO.ToUpper(); }

                                foreach (var itemD in lstPlantaD)
                                {
                                    if (txtPlantaDestino.Text == "")
                                    { txtPlantaDestino.Text = txtPlantaDestino.Text.ToUpper() + strPlantaD.ToUpper(); }
                                    else { txtPlantaDestino.Text = txtPlantaDestino.Text.ToUpper() + ", " + strPlantaD.ToUpper(); }
                                    strPlantaD = itemD.ToUpper();
                                }

                                if (txtPlantaDestino.Text == "")
                                { txtPlantaDestino.Text = txtPlantaDestino.Text.ToUpper() + strPlantaD.ToUpper(); }
                                else { txtPlantaDestino.Text = txtPlantaDestino.Text.ToUpper() + " y " + strPlantaD.ToUpper(); }



                                if (decOpcion == 2)
                                {
                                    lstDocumentoGb = lstDocumentoGb.Where(x => x.OBSERVACIONES != null).ToList();

                                    var objMovB2Gb = lstDocumentoGb.Where(x => x.OBSERVACIONES.StartsWith("B2CCTT")).ToList();

                                    foreach (var reg in objMovB2Gb)
                                    {
                                        var objTrasaccion = new O_OBTENER_ANEXO_B2();
                                        objVolB = new O_OBTENER_VOLUMEN_IMPORTADO();

                                        var parametros = reg.OBSERVACIONES;

                                        string[] lista = parametros.Split('|');

                                        string[] listaProveedor = lista[6].Split('-');

                                        var decVolDespachado = Convert.ToDecimal(lista[4].ToString());
                                        var decVolRecibido = Convert.ToDecimal(lista[5].ToString());

                                        decimal decPreUni = lista[10].ToString() == "" ? 0 : Convert.ToDecimal(lista[10].ToString());

                                        var decVolDiferencia = decVolDespachado - decVolRecibido;

                                        var decMerma = decVolDiferencia <= 150 ? (decVolRecibido * Convert.ToDecimal("0.0025")) : 0;

                                        objTrasaccion.NRO_DUI = lista[1].ToString();
                                        if (listaProveedor.Count() == 2)
                                        {
                                            objTrasaccion.NRO_FACTURA_PROVEEDOR = listaProveedor[0].ToString();
                                        }
                                        else
                                        {
                                            objTrasaccion.NRO_FACTURA_PROVEEDOR = "";
                                        }

                                        objTrasaccion.EMPRESA_TRANSPORTE = reg.RAZON_SOCIAL;
                                        objTrasaccion.NRO_FACTURA_TRANSPORTE = reg.NRO_TRANSACCION;
                                        objTrasaccion.PLANTA_DESPACHO = lista[2].ToString();
                                        objTrasaccion.PLANTA_RECEPCION = lista[3].ToString();
                                        objTrasaccion.VOLUMEN_DESPACHADO = decVolDespachado;
                                        objTrasaccion.VOLUMEN_RECIBIDO = decVolRecibido;
                                        objTrasaccion.DIFERENCIA = decVolDiferencia * -1;
                                        objTrasaccion.MERMA_PERMISIBLE = Math.Round(decMerma, 0);
                                        objTrasaccion.MERMA_EXCEDENTE = Math.Round(decVolDiferencia - decMerma, 0) > 0 ? Math.Round(decVolDiferencia - decMerma, 0) : 0;
                                        objTrasaccion.FLETE = Convert.ToDecimal(lista[7].ToString());
                                        objTrasaccion.COSTO_PRODUCTO = decPreUni;
                                        objTrasaccion.TRANSPORTE_TOTAL = decVolRecibido * Convert.ToDecimal(lista[7].ToString());
                                        objTrasaccion.DESCUENTO_MERMA = (Math.Round(decVolDiferencia - decMerma, 0) > 0 ? Math.Round(decVolDiferencia - decMerma, 0) : 0) * decPreUni;
                                        objTrasaccion.COSTO_TRANSPORTE = (decVolRecibido * Convert.ToDecimal(lista[7].ToString())) - ((Math.Round(decVolDiferencia - decMerma, 0) > 0 ? Math.Round(decVolDiferencia - decMerma, 0) : 0) * decPreUni);

                                        objNuevoDatoB2Gb.Add(objTrasaccion);

                                        /*if (reg.ID_MOVIMIENTO == decIdMovimiento)
                                        {
                                            objVolB.RON = reg.CODIGO;
                                            objVolB.VOLUMEN = 0;
                                            objVolB.COSTO = (decVolRecibido * Convert.ToDecimal(lista[7].ToString())) - ((Math.Round(decVolDiferencia - decMerma, 0) > 0 ? Math.Round(decVolDiferencia - decMerma, 0) : 0) * reg.PRECIO_UNITARIO);

                                            objVolImportB.Add(objVolB);
                                        }*/

                                    }

                                    dgvAnexoB2Gb.DataSource = objNuevoDatoB2Gb;
                                    dgvAnexoB2Gb.DataBind();

                                    Session[CVariablesSesion.GrillaAnexoB2Gbc] = objNuevoDatoB2Gb;

                                    var objMovB2GbD = lstDocumentoGb.Where(x => x.OBSERVACIONES.StartsWith("B2GBCCTTD")).ToList();

                                    foreach (var reg in objMovB2GbD)
                                    {
                                        var objTrasaccion = new O_OBTENER_ANEXO_B2_DU();
                                        objVolB = new O_OBTENER_VOLUMEN_IMPORTADO();

                                        var parametros = reg.OBSERVACIONES;

                                        string[] lista = parametros.Split('|');

                                        decimal decPreUni = lista[10].ToString() == "" ? 0 : Convert.ToDecimal(lista[10].ToString());

                                        string[] listaProveedor = lista[6].Split('-');

                                        objTrasaccion.PRODUCTO = "GASOLINA BLANCA";
                                        objTrasaccion.MES_RECIBIDO = ObtenerPeriodo(reg.FECHA_HORA_EMISION.ToString());
                                        objTrasaccion.VOLUMEN = Convert.ToDecimal(lista[4].ToString()).ToString().Replace(".", ",");
                                        objTrasaccion.PRECIO = decPreUni.ToString().Replace(".", ",");
                                        objTrasaccion.MONTO = (decPreUni * Convert.ToDecimal(lista[4].ToString())).ToString();

                                        objNuevoDatoB2GbD.Add(objTrasaccion);

                                        /*if (reg.ID_MOVIMIENTO == decIdMovimiento)
                                        {
                                            objVolB.RON = reg.CODIGO;
                                            objVolB.VOLUMEN = 0;
                                            objVolB.COSTO = (decVolRecibido * Convert.ToDecimal(lista[7].ToString())) - ((Math.Round(decVolDiferencia - decMerma, 0) > 0 ? Math.Round(decVolDiferencia - decMerma, 0) : 0) * reg.PRECIO_UNITARIO);

                                            objVolImportB.Add(objVolB);
                                        }*/

                                    }

                                    dgvAnexoB2DuGb.DataSource = objNuevoDatoB2GbD;
                                    dgvAnexoB2DuGb.DataBind();

                                    Session[CVariablesSesion.GrillaAnexoB2GbD] = objNuevoDatoB2GbD;
                                }


                                var objMovB3 = lstDocumento.Where(x => x.OBSERVACIONES.StartsWith("B3CCTF")).ToList();

                                var objNuevoDatoB3 = new List<O_OBTENER_ANEXO_B3>();
                                foreach (var reg in objMovB3)
                                {
                                    var objTrasaccion = new O_OBTENER_ANEXO_B3();
                                    objVolB = new O_OBTENER_VOLUMEN_IMPORTADO();

                                    var parametros = reg.OBSERVACIONES;

                                    string[] lista = parametros.Split('|');

                                    string[] listaProveedor = lista[6].Split('-');

                                    objTrasaccion.NRO_DUI = lista[1].ToString();

                                    if (listaProveedor.Count() == 2)
                                    {
                                        objTrasaccion.PROVEEDOR = listaProveedor[1].ToString();
                                        objTrasaccion.NRO_FACTURA = listaProveedor[0].ToString();
                                    }
                                    else
                                    {
                                        objTrasaccion.PROVEEDOR = "";
                                        objTrasaccion.NRO_FACTURA = "";
                                    }
                                    objTrasaccion.NRO_FACTURA_PROVEEDOR = listaProveedor[0].ToString();
                                    objTrasaccion.PROVEEDOR = listaProveedor[1].ToString();
                                    objTrasaccion.EMPRESA = reg.RAZON_SOCIAL;
                                    objTrasaccion.NRO_FACTURA = reg.NRO_TRANSACCION;
                                    objTrasaccion.VOLUMEN_DESTINO1 = Convert.ToDecimal(lista[4].ToString());
                                    objTrasaccion.VOLUMEN_DESTINO2 = Convert.ToDecimal(lista[5].ToString());
                                    objTrasaccion.FLETE = Convert.ToDecimal(lista[7].ToString());
                                    objTrasaccion.COSTO_FACTURADO = (Convert.ToDecimal(lista[5].ToString())) * (Convert.ToDecimal(lista[7].ToString()));
                                    objTrasaccion.MONTO_NETO_IVA = ((Convert.ToDecimal(lista[5].ToString())) * (Convert.ToDecimal(lista[7].ToString()))) * Convert.ToDecimal("0.87");

                                    objNuevoDatoB3.Add(objTrasaccion);

                                    //if (reg.ID_MOVIMIENTO == decIdMovimiento)
                                    //{
                                        objVolB.IDMOVIMIENTO = reg.ID_MOVIMIENTO;
                                        objVolB.RON = reg.CODIGO;
                                        objVolB.VOLUMEN = 0;
                                        objVolB.COSTO = ((Convert.ToDecimal(lista[5].ToString())) * (Convert.ToDecimal(lista[7].ToString()))) * Convert.ToDecimal("0.87");

                                        objVolImportB.Add(objVolB);
                                    //}
                                }

                                dgvAnexoB3.DataSource = objNuevoDatoB3;
                                dgvAnexoB3.DataBind();

                                Session[CVariablesSesion.GrillaAnexoB3] = objNuevoDatoB3;

                                #endregion

                                //ANEXO C
                                #region ANEXO C
                                var objMovC = lstDocumento.Where(x => x.OBSERVACIONES.StartsWith("CCS")).ToList();

                                var objNuevoDatoC = new List<O_OBTENER_ANEXO_C>();
                                foreach (var reg in objMovC)
                                {
                                    var objTrasaccion = new O_OBTENER_ANEXO_C();

                                    var objVolC = new O_OBTENER_VOLUMEN_IMPORTADO();

                                    var parametros = reg.OBSERVACIONES;

                                    string[] lista = parametros.Split('|');

                                    objTrasaccion.NRO_DUI = lista[1].ToString();
                                    objTrasaccion.EMPRESA_ASEGURADORA = reg.RAZON_SOCIAL;
                                    objTrasaccion.PERIODO_COBERTURA = "";
                                    objTrasaccion.NRO_FACTURA_ASEGURADORA = reg.NRO_TRANSACCION;
                                    objTrasaccion.MONTO_FACTURADO = reg.PRECIO_TOTAL;
                                    objTrasaccion.MONTO_NETO_IVA = reg.PRECIO_TOTAL * Convert.ToDecimal("0.87");

                                    objNuevoDatoC.Add(objTrasaccion);

                                    //if (reg.ID_MOVIMIENTO == decIdMovimiento)
                                    //{
                                        objVolC.IDMOVIMIENTO = reg.ID_MOVIMIENTO;
                                        objVolC.RON = reg.CODIGO;
                                        objVolC.VOLUMEN = 0;
                                        objVolC.COSTO = reg.PRECIO_TOTAL * Convert.ToDecimal("0.87");

                                        objVolImportC.Add(objVolC);
                                    //}
                                }

                                dgvAnexoC.DataSource = objNuevoDatoC;
                                dgvAnexoC.DataBind();

                                Session[CVariablesSesion.GrillaAnexoC] = objNuevoDatoC;

                                #endregion

                                //ANEXO D
                                #region ANEXO D
                                var objMovD1 = lstDocumento.Where(x => x.OBSERVACIONES.StartsWith("D1IO")).ToList();

                                var objNuevoDatoD1 = new List<O_OBTENER_ANEXO_D>();
                                var objNuevoDatoD1Gb = new List<O_OBTENER_ANEXO_D>();
                                var objNuevoDatoD1Total = new List<O_OBTENER_ANEXO_D>();
                                foreach (var reg in objMovD1)
                                {
                                    var objVolD = new O_OBTENER_VOLUMEN_IMPORTADO();

                                    var objTrasaccion = new O_OBTENER_ANEXO_D();

                                    var parametros = reg.OBSERVACIONES;

                                    string[] lista = parametros.Split('|');

                                    string[] listaProveedor = lista[6].Split('-');

                                    string fecha = reg.FECHA_HORA_EMISION.ToString();

                                    objTrasaccion.MES_CARGA = Convert.ToDateTime(fecha.Substring(0, 4) + "/" + fecha.Substring(4, 2) + "/" + fecha.Substring(6, 2)).ToString("MMMM - yyyy");
                                    objTrasaccion.NRO_DUI = lista[1].ToString();
                                    if (listaProveedor.Count() == 2)
                                    {
                                        objTrasaccion.PROVEEDOR = listaProveedor[1].ToString();
                                        objTrasaccion.NRO_FACTURA = listaProveedor[0].ToString();
                                    }
                                    else
                                    {
                                        objTrasaccion.PROVEEDOR = "";
                                        objTrasaccion.NRO_FACTURA = "";
                                    }
                                    objTrasaccion.VOLUMEN_FACTURADO = Convert.ToDecimal(lista[5].ToString());
                                    objTrasaccion.MONTO_INSPECCION = reg.PRECIO_TOTAL;

                                    objNuevoDatoD1.Add(objTrasaccion);
                                    objNuevoDatoD1Total.Add(objTrasaccion);

                                    //if (reg.ID_MOVIMIENTO == decIdMovimiento)
                                    //{
                                        objVolD.IDMOVIMIENTO = reg.ID_MOVIMIENTO;
                                        objVolD.RON = reg.CODIGO;
                                        objVolD.VOLUMEN = 1;
                                        objVolD.COSTO = reg.PRECIO_TOTAL;

                                        objVolImportD.Add(objVolD);
                                    //}


                                }

                                if (decOpcion == 2)
                                {
                                    lstDocumentoGb = lstDocumentoGb.Where(x => x.OBSERVACIONES != null).ToList();

                                    var objMovGnGb = lstDocumentoGb.Where(x => x.OBSERVACIONES.StartsWith("D1IO")).ToList();

                                    foreach (var reg in objMovGnGb)
                                    {
                                        var objVolD = new O_OBTENER_VOLUMEN_IMPORTADO();

                                        var objTrasaccion = new O_OBTENER_ANEXO_D();

                                        var parametros = reg.OBSERVACIONES;

                                        string[] lista = parametros.Split('|');

                                        string[] listaProveedor = lista[6].Split('-');

                                        string fecha = reg.FECHA_HORA_EMISION.ToString();

                                        objTrasaccion.MES_CARGA = Convert.ToDateTime(fecha.Substring(0, 4) + "/" + fecha.Substring(4, 2) + "/" + fecha.Substring(6, 2)).ToString("MMMM - yyyy");
                                        objTrasaccion.NRO_DUI = "GASOLINA BLANCA";
                                        objTrasaccion.PROVEEDOR = "GASOLINA BLANCA";
                                        objTrasaccion.NRO_FACTURA = "GASOLINA BLANCA";
                                        objTrasaccion.VOLUMEN_FACTURADO = Convert.ToDecimal(lista[5].ToString());
                                        objTrasaccion.MONTO_INSPECCION = reg.PRECIO_TOTAL;

                                        objNuevoDatoD1Gb.Add(objTrasaccion);
                                        objNuevoDatoD1Total.Add(objTrasaccion);

                                        /*if (reg.ID_MOVIMIENTO == decIdMovimiento)
                                        {
                                            objVolD.RON = reg.CODIGO;
                                            objVolD.VOLUMEN = 1;
                                            objVolD.COSTO = reg.PRECIO_TOTAL;

                                            objVolImportD.Add(objVolD);
                                        }*/
                                    }

                                    Session[CVariablesSesion.GrillaAnexoD1Gb] = objNuevoDatoD1Gb;
                                }

                                dgvAnexoD1.DataSource = objNuevoDatoD1Total;
                                dgvAnexoD1.DataBind();

                                Session[CVariablesSesion.GrillaAnexoD1] = objNuevoDatoD1;

                                var objMovD2 = lstDocumento.Where(x => x.OBSERVACIONES.StartsWith("D2II")).ToList();

                                var objNuevoDatoD2 = new List<O_OBTENER_ANEXO_D>();
                                var objNuevoDatoD2Gb = new List<O_OBTENER_ANEXO_D>();
                                var objNuevoDatoD2Total = new List<O_OBTENER_ANEXO_D>();

                                foreach (var reg in objMovD2)
                                {
                                    var objVolD = new O_OBTENER_VOLUMEN_IMPORTADO();

                                    var objTrasaccion = new O_OBTENER_ANEXO_D();

                                    var parametros = reg.OBSERVACIONES;

                                    string[] lista = parametros.Split('|');

                                    string[] listaProveedor = lista[6].Split('-');

                                    string fecha = reg.FECHA_HORA_EMISION.ToString();

                                    objTrasaccion.MES_CARGA = Convert.ToDateTime(fecha.Substring(0, 4) + "/" + fecha.Substring(4, 2) + "/" + fecha.Substring(6, 2)).ToString("MMMM - yyyy");
                                    objTrasaccion.NRO_DUI = lista[1].ToString();
                                    if (listaProveedor.Count() == 2)
                                    {
                                        objTrasaccion.PROVEEDOR = listaProveedor[1].ToString();
                                        objTrasaccion.NRO_FACTURA = listaProveedor[0].ToString();
                                    }
                                    else
                                    {
                                        objTrasaccion.PROVEEDOR = "";
                                        objTrasaccion.NRO_FACTURA = "";
                                    }

                                    objTrasaccion.VOLUMEN_FACTURADO = Convert.ToDecimal(lista[5].ToString());
                                    objTrasaccion.MONTO_INSPECCION = reg.PRECIO_TOTAL;

                                    objNuevoDatoD2.Add(objTrasaccion);
                                    objNuevoDatoD2Total.Add(objTrasaccion);

                                    //if (reg.ID_MOVIMIENTO == decIdMovimiento)
                                    //{
                                        objVolD.IDMOVIMIENTO = reg.ID_MOVIMIENTO;
                                        objVolD.RON = reg.CODIGO;
                                        objVolD.VOLUMEN = 0;
                                        objVolD.COSTO = reg.PRECIO_TOTAL;

                                        objVolImportD.Add(objVolD);
                                    //}
                                }

                                if (decOpcion == 2)
                                {
                                    lstDocumentoGb = lstDocumentoGb.Where(x => x.OBSERVACIONES != null).ToList();

                                    var objMovGnGb = lstDocumentoGb.Where(x => x.OBSERVACIONES.StartsWith("D2II")).ToList();

                                    foreach (var reg in objMovGnGb)
                                    {
                                        var objVolD = new O_OBTENER_VOLUMEN_IMPORTADO();

                                        var objTrasaccion = new O_OBTENER_ANEXO_D();

                                        var parametros = reg.OBSERVACIONES;

                                        string[] lista = parametros.Split('|');

                                        string[] listaProveedor = lista[6].Split('-');

                                        string fecha = reg.FECHA_HORA_EMISION.ToString();

                                        objTrasaccion.MES_CARGA = Convert.ToDateTime(fecha.Substring(0, 4) + "/" + fecha.Substring(4, 2) + "/" + fecha.Substring(6, 2)).ToString("MMMM - yyyy");
                                        objTrasaccion.NRO_DUI = "GASOLINA BLANCA";
                                        objTrasaccion.PROVEEDOR = "GASOLINA BLANCA";
                                        objTrasaccion.NRO_FACTURA = "GASOLINA BLANCA";
                                        objTrasaccion.VOLUMEN_FACTURADO = Convert.ToDecimal(lista[5].ToString());
                                        objTrasaccion.MONTO_INSPECCION = reg.PRECIO_TOTAL;

                                        objNuevoDatoD2Gb.Add(objTrasaccion);
                                        objNuevoDatoD2Total.Add(objTrasaccion);

                                        /*if (reg.ID_MOVIMIENTO == decIdMovimiento)
                                        {
                                            objVolD.RON = reg.CODIGO;
                                            objVolD.VOLUMEN = 1;
                                            objVolD.COSTO = reg.PRECIO_TOTAL;

                                            objVolImportD.Add(objVolD);
                                        }*/
                                    }

                                    Session[CVariablesSesion.GrillaAnexoD2Gb] = objNuevoDatoD2Gb;
                                }

                                dgvAnexoD2.DataSource = objNuevoDatoD2Total;
                                dgvAnexoD2.DataBind();

                                Session[CVariablesSesion.GrillaAnexoD2] = objNuevoDatoD2;

                                var objMovD3 = lstDocumento.Where(x => x.OBSERVACIONES.StartsWith("D3IDF")).ToList();

                                var objNuevoDatoD3 = new List<O_OBTENER_ANEXO_D>();
                                var objNuevoDatoD3Gb = new List<O_OBTENER_ANEXO_D>();
                                var objNuevoDatoD3Total = new List<O_OBTENER_ANEXO_D>();

                                foreach (var reg in objMovD3)
                                {
                                    var objVolD = new O_OBTENER_VOLUMEN_IMPORTADO();

                                    var objTrasaccion = new O_OBTENER_ANEXO_D();

                                    var parametros = reg.OBSERVACIONES;

                                    string[] lista = parametros.Split('|');

                                    string[] listaProveedor = lista[6].Split('-');

                                    string fecha = reg.FECHA_HORA_EMISION.ToString();

                                    objTrasaccion.MES_CARGA = Convert.ToDateTime(fecha.Substring(0, 4) + "/" + fecha.Substring(4, 2) + "/" + fecha.Substring(6, 2)).ToString("MMMM - yyyy");
                                    objTrasaccion.NRO_DUI = lista[1].ToString();
                                    if (listaProveedor.Count() == 2)
                                    {
                                        objTrasaccion.PROVEEDOR = listaProveedor[1].ToString();
                                        objTrasaccion.NRO_FACTURA = listaProveedor[0].ToString();
                                    }
                                    else
                                    {
                                        objTrasaccion.PROVEEDOR = "";
                                        objTrasaccion.NRO_FACTURA = "";
                                    }
                                    objTrasaccion.VOLUMEN_FACTURADO = Convert.ToDecimal(lista[5].ToString());
                                    objTrasaccion.MONTO_INSPECCION = reg.PRECIO_TOTAL;

                                    objNuevoDatoD3.Add(objTrasaccion);
                                    objNuevoDatoD3Total.Add(objTrasaccion);

                                    //if (reg.ID_MOVIMIENTO == decIdMovimiento)
                                    //{
                                        objVolD.IDMOVIMIENTO = reg.ID_MOVIMIENTO;
                                        objVolD.RON = reg.CODIGO;
                                        objVolD.VOLUMEN = 0;
                                        objVolD.COSTO = reg.PRECIO_TOTAL;

                                        objVolImportD.Add(objVolD);
                                    //}
                                }

                                if (decOpcion == 2)
                                {
                                    lstDocumentoGb = lstDocumentoGb.Where(x => x.OBSERVACIONES != null).ToList();

                                    var objMovGnGb = lstDocumentoGb.Where(x => x.OBSERVACIONES.StartsWith("D3IDF")).ToList();

                                    foreach (var reg in objMovGnGb)
                                    {
                                        var objVolD = new O_OBTENER_VOLUMEN_IMPORTADO();

                                        var objTrasaccion = new O_OBTENER_ANEXO_D();

                                        var parametros = reg.OBSERVACIONES;

                                        string[] lista = parametros.Split('|');

                                        string[] listaProveedor = lista[6].Split('-');

                                        string fecha = reg.FECHA_HORA_EMISION.ToString();

                                        objTrasaccion.MES_CARGA = Convert.ToDateTime(fecha.Substring(0, 4) + "/" + fecha.Substring(4, 2) + "/" + fecha.Substring(6, 2)).ToString("MMMM - yyyy");
                                        objTrasaccion.NRO_DUI = "GASOLINA BLANCA";
                                        objTrasaccion.PROVEEDOR = "GASOLINA BLANCA";
                                        objTrasaccion.NRO_FACTURA = "GASOLINA BLANCA";
                                        objTrasaccion.VOLUMEN_FACTURADO = Convert.ToDecimal(lista[5].ToString());
                                        objTrasaccion.MONTO_INSPECCION = reg.PRECIO_TOTAL;

                                        objNuevoDatoD3Gb.Add(objTrasaccion);
                                        objNuevoDatoD3Total.Add(objTrasaccion);

                                        /*if (reg.ID_MOVIMIENTO == decIdMovimiento)
                                        {
                                            objVolD.RON = reg.CODIGO;
                                            objVolD.VOLUMEN = 1;
                                            objVolD.COSTO = reg.PRECIO_TOTAL;

                                            objVolImportD.Add(objVolD);
                                        }*/
                                    }

                                    Session[CVariablesSesion.GrillaAnexoD3Gb] = objNuevoDatoD3Gb;
                                }

                                dgvAnexoD3.DataSource = objNuevoDatoD3Total;
                                dgvAnexoD3.DataBind();

                                Session[CVariablesSesion.GrillaAnexoD3] = objNuevoDatoD3;
                                #endregion

                                //ANEXO E
                                #region ANEXO E
                                var objMovE = lstDocumento.Where(x => x.OBSERVACIONES.StartsWith("ECC")).ToList();

                                var objNuevoDatoE = new List<O_OBTENER_ANEXO_E>();
                                foreach (var reg in objMovE)
                                {
                                    var objTrasaccion = new O_OBTENER_ANEXO_E();
                                    var objVolE = new O_OBTENER_VOLUMEN_IMPORTADO();

                                    var parametros = reg.OBSERVACIONES;

                                    string[] lista = parametros.Split('|');

                                    objTrasaccion.NRO_DUI = lista[1].ToString();
                                    objTrasaccion.GRAVAMEN = Convert.ToDecimal(lista[8].ToString());
                                    objTrasaccion.SIDUINEA = Convert.ToDecimal(lista[9].ToString());
                                    objTrasaccion.COSTO_DEPOSITO = Convert.ToDecimal(lista[10].ToString());
                                    objTrasaccion.TOTAL_COSTO = Convert.ToDecimal(lista[8].ToString()) + Convert.ToDecimal(lista[9].ToString()) + Convert.ToDecimal(lista[10].ToString());

                                    objNuevoDatoE.Add(objTrasaccion);

                                    //if (reg.ID_MOVIMIENTO == decIdMovimiento)
                                    //{
                                        objVolE.IDMOVIMIENTO = reg.ID_MOVIMIENTO;
                                        objVolE.RON = reg.CODIGO;
                                        objVolE.VOLUMEN = 0;
                                        objVolE.COSTO = Convert.ToDecimal(lista[8].ToString()) + Convert.ToDecimal(lista[9].ToString()) + Convert.ToDecimal(lista[10].ToString());

                                        objVolImportE.Add(objVolE);
                                    //}

                                }

                                dgvAnexoE.DataSource = objNuevoDatoE;
                                dgvAnexoE.DataBind();

                                Session[CVariablesSesion.GrillaAnexoE] = objNuevoDatoE;

                                #endregion

                                //ANEXO F
                                #region anexo F

                                var objNuevoDatoF1 = new List<O_OBTENER_ANEXO_F>();
                                if (decOpcion == 2)
                                {
                                    lstDocumento = lstDocumento.Where(x => x.OBSERVACIONES != null).ToList();

                                    var objMovF = lstDocumento.Where(x => x.OBSERVACIONES.StartsWith("ACCP")).ToList();

                                    foreach (var reg in objMovF)
                                    {
                                        var objTrasaccion = new O_OBTENER_ANEXO_F();
                                        var objVol = new O_OBTENER_VOLUMEN_IMPORTADO();

                                        var parametros = reg.OBSERVACIONES;

                                        string[] lista = parametros.Split('|');

                                        objTrasaccion.NRO_DUI = lista[1].ToString();
                                        objTrasaccion.PROVEEDOR = reg.RAZON_SOCIAL;
                                        objTrasaccion.MES_RECIBIDO = ObtenerPeriodo(reg.FECHA_HORA_EMISION.ToString());
                                        objTrasaccion.NRO_FACTURA_PROVEEDOR = reg.NRO_TRANSACCION;
                                        objTrasaccion.MONTO_FACTURA_PROVEEDOR = reg.PRECIO_TOTAL;
                                        objTrasaccion.COSTO_FINANCIERO = reg.PRECIO_TOTAL * Convert.ToDecimal("0.0010");
                                        objTrasaccion.COSTO_FINANCIERO_NETO = (reg.PRECIO_TOTAL * Convert.ToDecimal("0.0010")) * Convert.ToDecimal("0.078");

                                        objNuevoDatoF1.Add(objTrasaccion);

                                    }

                                    dgvCostoFinancieroProducto.DataSource = objNuevoDatoF1;
                                    dgvCostoFinancieroProducto.DataBind();

                                    Session[CVariablesSesion.GrillaAnexoF1] = objNuevoDatoF1;

                                    var objMovF2 = lstDocumento.Where(x => x.OBSERVACIONES.StartsWith("D1IO")).ToList();

                                    var objNuevoDatoF2 = new List<O_OBTENER_ANEXO_F>();

                                    foreach (var reg in objMovF2)
                                    {
                                        var objVolD = new O_OBTENER_VOLUMEN_IMPORTADO();

                                        var objTrasaccion = new O_OBTENER_ANEXO_F();

                                        var parametros = reg.OBSERVACIONES;

                                        string[] lista = parametros.Split('|');

                                        string[] listaProveedor = lista[6].Split('-');

                                        string fecha = reg.FECHA_HORA_EMISION.ToString();

                                        objTrasaccion.MES_RECIBIDO = Convert.ToDateTime(fecha.Substring(0, 4) + "/" + fecha.Substring(4, 2) + "/" + fecha.Substring(6, 2)).ToString("MMMM - yyyy");
                                        objTrasaccion.NRO_DUI = lista[1].ToString();
                                        objTrasaccion.PROVEEDOR = reg.RAZON_SOCIAL;
                                        objTrasaccion.NRO_FACTURA_PROVEEDOR = reg.NRO_TRANSACCION;
                                        objTrasaccion.MONTO_FACTURA_PROVEEDOR = reg.PRECIO_TOTAL;
                                        objTrasaccion.COSTO_FINANCIERO = reg.PRECIO_TOTAL * Convert.ToDecimal("0.0010");
                                        objTrasaccion.COSTO_FINANCIERO_NETO = (reg.PRECIO_TOTAL * Convert.ToDecimal("0.0010")) * Convert.ToDecimal("0.078");

                                        objNuevoDatoF2.Add(objTrasaccion);

                                        /*if (reg.ID_MOVIMIENTO == decIdMovimiento)
                                        {
                                            objVolD.RON = reg.CODIGO;
                                            objVolD.VOLUMEN = 1;
                                            objVolD.COSTO = reg.PRECIO_TOTAL;

                                            objVolImportD.Add(objVolD);
                                        }*/


                                    }

                                    dgvCostoFinancieroServicio.DataSource = objNuevoDatoF2;
                                    dgvCostoFinancieroServicio.DataBind();

                                    Session[CVariablesSesion.GrillaAnexoF2] = objNuevoDatoF2;
                                }

                                #endregion

                                //ANEXO G
                                #region ANEXO G

                                var objMovG1 = lstDocumento.Where(x => x.OBSERVACIONES.StartsWith("G1AI")).ToList();

                                var objVolG = new O_OBTENER_VOLUMEN_IMPORTADO();

                                var objNuevoDatoG1 = new List<O_OBTENER_ANEXO_G>();
                                var objNuevoDatoG1Gb = new List<O_OBTENER_ANEXO_G>();
                                var objNuevoDatoG1Total = new List<O_OBTENER_ANEXO_G>();
                                var objNuevoDatoI1 = new List<O_OBTENER_ANEXO_I>();
                                var objNuevoDatoIGb = new List<O_OBTENER_ANEXO_I_GB>();
                                var objNuevoDatoIGbase = new List<O_OBTENER_ANEXO_I_GBASE>();
                                var objNuevoDatoI1Total = new List<O_OBTENER_ANEXO_I>();

                                foreach (var reg in objMovG1)
                                {
                                    var objTrasaccion = new O_OBTENER_ANEXO_G();
                                    var objTrasaccionI = new O_OBTENER_ANEXO_I();

                                    objVolG = new O_OBTENER_VOLUMEN_IMPORTADO();

                                    var parametros = reg.OBSERVACIONES;

                                    string[] lista = parametros.Split('|');

                                    string fecha = reg.FECHA_HORA_EMISION.ToString();

                                    objTrasaccion.NRO_DUI = lista[1].ToString();
                                    objTrasaccion.MES = Convert.ToDateTime(fecha.Substring(0, 4) + "/" + fecha.Substring(4, 2) + "/" + fecha.Substring(6, 2)).ToString("MMMM - yyyy");
                                    objTrasaccion.EMPRESA_ALMACENAJE = reg.RAZON_SOCIAL;
                                    objTrasaccion.NRO_FACTURA_ALMACENAJE = reg.NRO_TRANSACCION;
                                    objTrasaccion.TARIFA_UNITARIA = reg.PRECIO_UNITARIO;
                                    objTrasaccion.VOLUMEN_RECIBIDO = Convert.ToDecimal(lista[5].ToString());
                                    objTrasaccion.COSTO_ALMACENAJE = Math.Round(objTrasaccion.TARIFA_UNITARIA * objTrasaccion.VOLUMEN_RECIBIDO, 2);
                                    objTrasaccion.MONTO_NETO_IVA = Math.Round(objTrasaccion.COSTO_ALMACENAJE * Convert.ToDecimal("0.87"), 2);

                                    objNuevoDatoG1.Add(objTrasaccion);
                                    objNuevoDatoG1Total.Add(objTrasaccion);

                                    string[] listaProveedor = lista[6].Split('-');

                                    objTrasaccionI.NRO_DUI = lista[1].ToString();
                                    if (listaProveedor.Count() == 2)
                                    {
                                        objTrasaccionI.PROVEEDOR = listaProveedor[1].ToString();
                                        objTrasaccionI.NRO_FACTURA_PROVEEDOR = listaProveedor[0].ToString();
                                    }
                                    else
                                    {
                                        objTrasaccionI.PROVEEDOR = "";
                                        objTrasaccionI.NRO_FACTURA_PROVEEDOR = "";
                                    }
                                    objTrasaccionI.MES_RECEPCION = Convert.ToDateTime(fecha.Substring(0, 4) + "/" + fecha.Substring(4, 2) + "/" + fecha.Substring(6, 2)).ToString("MMMM - yyyy");
                                    objTrasaccionI.PLANTA = txtPlanta;
                                    objTrasaccionI.VOLUMEN = Convert.ToDecimal(lista[5].ToString());

                                    objNuevoDatoI1.Add(objTrasaccionI);

                                    //if (reg.ID_MOVIMIENTO == decIdMovimiento)
                                    //{
                                        objVolG.IDMOVIMIENTO = reg.ID_MOVIMIENTO;
                                        objVolG.RON = reg.CODIGO;
                                        objVolG.VOLUMEN = Convert.ToDecimal(lista[5].ToString());
                                        objVolG.COSTO = Math.Round(objTrasaccion.COSTO_ALMACENAJE * Convert.ToDecimal("0.87"), 2);

                                        objVolImportG.Add(objVolG);
                                    //}
                                }

                                if (decOpcion == 2)
                                {
                                    lstDocumentoGb = lstDocumentoGb.Where(x => x.OBSERVACIONES != null).ToList();

                                    var objMovGnGb = lstDocumentoGb.Where(x => x.OBSERVACIONES.StartsWith("G1AI")).ToList();

                                    foreach (var reg in objMovGnGb)
                                    {
                                        var objTrasaccion = new O_OBTENER_ANEXO_G();
                                        var objTrasaccionI = new O_OBTENER_ANEXO_I_GB();

                                        objVolG = new O_OBTENER_VOLUMEN_IMPORTADO();

                                        var parametros = reg.OBSERVACIONES;

                                        string[] lista = parametros.Split('|');

                                        string fecha = reg.FECHA_HORA_EMISION.ToString();

                                        objTrasaccion.NRO_DUI = "GASOLINA BLANCA";
                                        objTrasaccion.MES = "GASOLINA BLANCA";
                                        objTrasaccion.EMPRESA_ALMACENAJE = "GASOLINA BLANCA";
                                        objTrasaccion.NRO_FACTURA_ALMACENAJE = reg.NRO_TRANSACCION;
                                        objTrasaccion.TARIFA_UNITARIA = reg.PRECIO_UNITARIO;
                                        objTrasaccion.VOLUMEN_RECIBIDO = Convert.ToDecimal(lista[5].ToString());
                                        objTrasaccion.COSTO_ALMACENAJE = Math.Round(objTrasaccion.TARIFA_UNITARIA * objTrasaccion.VOLUMEN_RECIBIDO, 2);
                                        objTrasaccion.MONTO_NETO_IVA = Math.Round(objTrasaccion.COSTO_ALMACENAJE * Convert.ToDecimal("0.87"), 2);

                                        objNuevoDatoG1Gb.Add(objTrasaccion);
                                        objNuevoDatoG1Total.Add(objTrasaccion);

                                        string[] listaProveedor = lista[6].Split('-');

                                        objTrasaccionI.MES_RECIBIDO = Convert.ToDateTime(fecha.Substring(0, 4) + "/" + fecha.Substring(4, 2) + "/" + fecha.Substring(6, 2)).ToString("MMMM - yyyy");
                                        objTrasaccionI.PLANTA = txtPlanta;
                                        objTrasaccionI.VOLUMEN = Convert.ToDecimal(lista[5].ToString());

                                        objNuevoDatoIGb.Add(objTrasaccionI);

                                        /*if (reg.ID_MOVIMIENTO == decIdMovimiento)
                                        {
                                            objVolG.RON = reg.CODIGO;
                                            objVolG.VOLUMEN = Convert.ToDecimal(lista[5].ToString());
                                            objVolG.COSTO = Math.Round(objTrasaccion.COSTO_ALMACENAJE * Convert.ToDecimal("0.87"), 2);

                                            objVolImportG.Add(objVolG);
                                        }*/
                                    }

                                    var objMovGnGbase = lstDocumentoGb.Where(x => x.OBSERVACIONES.StartsWith("CVGB")).ToList();

                                    foreach (var reg in objMovGnGbase)
                                    {
                                        var objTrasaccionIGBase = new O_OBTENER_ANEXO_I_GBASE();

                                        objVolG = new O_OBTENER_VOLUMEN_IMPORTADO();

                                        var parametros = reg.OBSERVACIONES;

                                        string[] lista = parametros.Split('|');

                                        string fecha = reg.FECHA_HORA_EMISION.ToString();

                                        string[] listaProveedor = lista[6].Split('-');

                                        objTrasaccionIGBase.MES_RECIBIDO = Convert.ToDateTime(fecha.Substring(0, 4) + "/" + fecha.Substring(4, 2) + "/" + fecha.Substring(6, 2)).ToString("MMMM - yyyy");
                                        objTrasaccionIGBase.PLANTA = lista[2].ToString();
                                        objTrasaccionIGBase.DATO_1 = Convert.ToDecimal(lista[4].ToString());
                                        objTrasaccionIGBase.DATO_2 = Convert.ToDecimal(lista[5].ToString());
                                        objTrasaccionIGBase.DATO_3 = Convert.ToDecimal(lista[4].ToString()) + Convert.ToDecimal(lista[5].ToString());


                                        objNuevoDatoIGbase.Add(objTrasaccionIGBase);

                                        /*if (reg.ID_MOVIMIENTO == decIdMovimiento)
                                        {
                                            objVolG.RON = reg.CODIGO;
                                            objVolG.VOLUMEN = Convert.ToDecimal(lista[5].ToString());
                                            objVolG.COSTO = Math.Round(objTrasaccion.COSTO_ALMACENAJE * Convert.ToDecimal("0.87"), 2);

                                            objVolImportG.Add(objVolG);
                                        }*/
                                    }


                                    Session[CVariablesSesion.GrillaAnexoG1Gb] = objNuevoDatoG1Gb;
                                }

                                dgvAnexoG1.DataSource = objNuevoDatoG1Total;
                                dgvAnexoG1.DataBind();

                                Session[CVariablesSesion.GrillaAnexoG1] = objNuevoDatoG1;

                                dgvAnexoI1.DataSource = objNuevoDatoI1;
                                dgvAnexoI1.DataBind();

                                Session[CVariablesSesion.GrillaAnexoI1] = objNuevoDatoI1;

                                var objMovG2 = lstDocumento.Where(x => x.OBSERVACIONES.StartsWith("G2ADF")).ToList();

                                var objNuevoDatoG2 = new List<O_OBTENER_ANEXO_G>();
                                var objNuevoDatoG2Gb = new List<O_OBTENER_ANEXO_G>();
                                var objNuevoDatoG2Total = new List<O_OBTENER_ANEXO_G>();

                                var objNuevoDatoI2 = new List<O_OBTENER_ANEXO_I>();

                                foreach (var reg in objMovG2)
                                {
                                    var objTrasaccion = new O_OBTENER_ANEXO_G();
                                    var objTrasaccionI = new O_OBTENER_ANEXO_I();

                                    objVolG = new O_OBTENER_VOLUMEN_IMPORTADO();

                                    var parametros = reg.OBSERVACIONES;

                                    string[] lista = parametros.Split('|');

                                    string fecha = reg.FECHA_HORA_EMISION.ToString();

                                    objTrasaccion.NRO_DUI = lista[1].ToString();
                                    objTrasaccion.MES = Convert.ToDateTime(fecha.Substring(0, 4) + "/" + fecha.Substring(4, 2) + "/" + fecha.Substring(6, 2)).ToString("MMMM - yyyy");
                                    objTrasaccion.EMPRESA_ALMACENAJE = reg.RAZON_SOCIAL;
                                    objTrasaccion.NRO_FACTURA_ALMACENAJE = reg.NRO_TRANSACCION;
                                    objTrasaccion.TARIFA_UNITARIA = reg.PRECIO_UNITARIO;
                                    objTrasaccion.VOLUMEN_RECIBIDO = Convert.ToDecimal(lista[5].ToString());
                                    objTrasaccion.COSTO_ALMACENAJE = Math.Round(objTrasaccion.TARIFA_UNITARIA * objTrasaccion.VOLUMEN_RECIBIDO, 2);
                                    objTrasaccion.MONTO_NETO_IVA = Math.Round(objTrasaccion.COSTO_ALMACENAJE * Convert.ToDecimal("0.87"), 2);

                                    objNuevoDatoG2.Add(objTrasaccion);
                                    objNuevoDatoG2Total.Add(objTrasaccion);

                                    string[] listaProveedor = lista[6].Split('-');

                                    objTrasaccionI.NRO_DUI = lista[1].ToString();
                                    if (listaProveedor.Count() == 2)
                                    {
                                        objTrasaccionI.PROVEEDOR = listaProveedor[1].ToString();
                                        objTrasaccionI.NRO_FACTURA_PROVEEDOR = listaProveedor[0].ToString();
                                    }
                                    else
                                    {
                                        objTrasaccionI.PROVEEDOR = "";
                                        objTrasaccionI.NRO_FACTURA_PROVEEDOR = "";
                                    }
                                    objTrasaccionI.MES_RECEPCION = Convert.ToDateTime(fecha.Substring(0, 4) + "/" + fecha.Substring(4, 2) + "/" + fecha.Substring(6, 2)).ToString("MMMM - yyyy");
                                    objTrasaccionI.PLANTA = "LA PAZ";
                                    objTrasaccionI.VOLUMEN = Convert.ToDecimal(lista[5].ToString());

                                    objNuevoDatoI2.Add(objTrasaccionI);

                                    //if (reg.ID_MOVIMIENTO == decIdMovimiento)
                                    //{
                                        objVolG.IDMOVIMIENTO = reg.ID_MOVIMIENTO;
                                        objVolG.RON = reg.CODIGO;
                                        objVolG.VOLUMEN = Convert.ToDecimal(lista[5].ToString());
                                        objVolG.COSTO = Math.Round(objTrasaccion.COSTO_ALMACENAJE * Convert.ToDecimal("0.87"), 2);

                                        objVolImportG.Add(objVolG);
                                    //}
                                }

                                if (decOpcion == 2)
                                {
                                    lstDocumentoGb = lstDocumentoGb.Where(x => x.OBSERVACIONES != null).ToList();

                                    var objMovGnGb = lstDocumentoGb.Where(x => x.OBSERVACIONES.StartsWith("G2ADF")).ToList();

                                    foreach (var reg in objMovGnGb)
                                    {
                                        var objTrasaccion = new O_OBTENER_ANEXO_G();
                                        var objTrasaccionI = new O_OBTENER_ANEXO_I_GB();

                                        objVolG = new O_OBTENER_VOLUMEN_IMPORTADO();

                                        var parametros = reg.OBSERVACIONES;

                                        string[] lista = parametros.Split('|');

                                        string fecha = reg.FECHA_HORA_EMISION.ToString();

                                        objTrasaccion.NRO_DUI = "GASOLINA BLANCA";
                                        objTrasaccion.MES = "GASOLINA BLANCA";
                                        objTrasaccion.EMPRESA_ALMACENAJE = "GASOLINA BLANCA";
                                        objTrasaccion.NRO_FACTURA_ALMACENAJE = reg.NRO_TRANSACCION;
                                        objTrasaccion.TARIFA_UNITARIA = reg.PRECIO_UNITARIO;
                                        objTrasaccion.VOLUMEN_RECIBIDO = Convert.ToDecimal(lista[5].ToString());
                                        objTrasaccion.COSTO_ALMACENAJE = Math.Round(objTrasaccion.TARIFA_UNITARIA * objTrasaccion.VOLUMEN_RECIBIDO, 2);
                                        objTrasaccion.MONTO_NETO_IVA = Math.Round(objTrasaccion.COSTO_ALMACENAJE * Convert.ToDecimal("0.87"), 2);

                                        objNuevoDatoG2Gb.Add(objTrasaccion);
                                        objNuevoDatoG2Total.Add(objTrasaccion);

                                        string[] listaProveedor = lista[6].Split('-');

                                        objTrasaccionI.MES_RECIBIDO = Convert.ToDateTime(fecha.Substring(0, 4) + "/" + fecha.Substring(4, 2) + "/" + fecha.Substring(6, 2)).ToString("MMMM - yyyy");
                                        objTrasaccionI.PLANTA = txtPlanta;
                                        objTrasaccionI.VOLUMEN = Convert.ToDecimal(lista[5].ToString());

                                        objNuevoDatoIGb.Add(objTrasaccionI);

                                        /*if (reg.ID_MOVIMIENTO == decIdMovimiento)
                                        {
                                            objVolG.RON = reg.CODIGO;
                                            objVolG.VOLUMEN = Convert.ToDecimal(lista[5].ToString());
                                            objVolG.COSTO = Math.Round(objTrasaccion.COSTO_ALMACENAJE * Convert.ToDecimal("0.87"), 2);

                                            objVolImportG.Add(objVolG);
                                        }*/
                                    }

                                    Session[CVariablesSesion.GrillaAnexoG2Gb] = objNuevoDatoG2Gb;

                                }

                                dgvAnexoG2.DataSource = objNuevoDatoG2Total;
                                dgvAnexoG2.DataBind();

                                Session[CVariablesSesion.GrillaAnexoG2] = objNuevoDatoG2;

                                dgvAnexoI2.DataSource = objNuevoDatoI2;
                                dgvAnexoI2.DataBind();

                                Session[CVariablesSesion.GrillaAnexoI2] = objNuevoDatoI2;

                                dgvAnexoIGb.DataSource = objNuevoDatoIGb;
                                dgvAnexoIGb.DataBind();

                                Session[CVariablesSesion.GrillaAnexoIGb] = objNuevoDatoIGb;

                                dgvAnexoIGbase.DataSource = objNuevoDatoIGbase;
                                dgvAnexoIGbase.DataBind();

                                Session[CVariablesSesion.GrillaAnexoIGbase] = objNuevoDatoIGbase;

                                #endregion

                                //ANEXO H
                                #region ANEXO H

                                var objMovH = lstDocumento.Where(x => x.OBSERVACIONES.StartsWith("HCM")).ToList();

                                var objNuevoDatoH = new List<O_OBTENER_ANEXO_H>();
                                var objNuevoDatoHGb = new List<O_OBTENER_ANEXO_H>();
                                var objNuevoDatoHTotal = new List<O_OBTENER_ANEXO_H>();
                                foreach (var reg in objMovH)
                                {
                                    var objTrasaccion = new O_OBTENER_ANEXO_H();
                                    var objVolH = new O_OBTENER_VOLUMEN_IMPORTADO();

                                    var parametros = reg.OBSERVACIONES;

                                    string[] lista = parametros.Split('|');

                                    string[] listaProveedor = lista[6].Split('-');

                                    string fecha = reg.FECHA_HORA_EMISION.ToString();

                                    objTrasaccion.MES = Convert.ToDateTime(fecha.Substring(0, 4) + "/" + fecha.Substring(4, 2) + "/" + fecha.Substring(6, 2)).ToString("MMMM - yyyy");
                                    objTrasaccion.NRO_DUI = lista[1].ToString();
                                    if (listaProveedor.Count() == 2)
                                    {
                                        objTrasaccion.NRO_FACTURA_PROVEEDOR = listaProveedor[0].ToString();
                                    }
                                    else
                                    {
                                        objTrasaccion.NRO_FACTURA_PROVEEDOR = "";
                                    }

                                    objTrasaccion.VOLUMEN_RECIBIDO = lista[5].ToString() == "" ? 0 : Convert.ToDecimal(lista[5].ToString());
                                    objTrasaccion.MONTO_MONITOREO = reg.PRECIO_TOTAL;

                                    objNuevoDatoH.Add(objTrasaccion);
                                    objNuevoDatoHTotal.Add(objTrasaccion);

                                    //if (reg.ID_MOVIMIENTO == decIdMovimiento)
                                    //{
                                        objVolH.IDMOVIMIENTO = reg.ID_MOVIMIENTO;
                                        objVolH.RON = reg.CODIGO;
                                        objVolH.VOLUMEN = 0;
                                        objVolH.COSTO = reg.PRECIO_TOTAL;

                                        objVolImportH.Add(objVolH);
                                    //}
                                }

                                if (decOpcion == 2)
                                {
                                    lstDocumentoGb = lstDocumentoGb.Where(x => x.OBSERVACIONES != null).ToList();

                                    var objMovHnGb = lstDocumentoGb.Where(x => x.OBSERVACIONES.StartsWith("HCM")).ToList();

                                    foreach (var reg in objMovHnGb)
                                    {
                                        var objTrasaccion = new O_OBTENER_ANEXO_H();
                                        var objVolH = new O_OBTENER_VOLUMEN_IMPORTADO();

                                        var parametros = reg.OBSERVACIONES;

                                        string[] lista = parametros.Split('|');

                                        string[] listaProveedor = lista[6].Split('-');

                                        string fecha = reg.FECHA_HORA_EMISION.ToString();

                                        objTrasaccion.MES = Convert.ToDateTime(fecha.Substring(0, 4) + "/" + fecha.Substring(4, 2) + "/" + fecha.Substring(6, 2)).ToString("MMMM - yyyy");
                                        objTrasaccion.NRO_DUI = "GASOLINA BLANCA";
                                        objTrasaccion.NRO_FACTURA_PROVEEDOR = "GASOLINA BLANCA";

                                        objTrasaccion.VOLUMEN_RECIBIDO = lista[5].ToString() == "" ? 0 : Convert.ToDecimal(lista[5].ToString());
                                        objTrasaccion.MONTO_MONITOREO = reg.PRECIO_TOTAL;

                                        objNuevoDatoHGb.Add(objTrasaccion);
                                        objNuevoDatoHTotal.Add(objTrasaccion);

                                        /*if (reg.ID_MOVIMIENTO == decIdMovimiento)
                                        {
                                            objVolH.RON = reg.CODIGO;
                                            objVolH.VOLUMEN = 0;
                                            objVolH.COSTO = reg.PRECIO_TOTAL;

                                            objVolImportH.Add(objVolH);
                                        }*/
                                    }

                                    Session[CVariablesSesion.GrillaAnexoHnGb] = objNuevoDatoHGb;
                                }

                                dgvAnexoH.DataSource = objNuevoDatoHTotal;
                                dgvAnexoH.DataBind();

                                Session[CVariablesSesion.GrillaAnexoH] = objNuevoDatoH;

                                #endregion

                                //ANEXO I
                            }
                            else { }

                            if (decOpcion == 1)
                            {
                                #region base
                                //var registrosBase = (List<O_CONSULTA_MOVIM_TRAN_CTY>)Session[CVariablesSesion.GrillaBase];
                                //FORMULARIO 1
                                

                                var objNuevoDatoF1 = new List<O_OBTENER_ANEXO_F>();
                                var objNuevoDatoF2 = new List<O_OBTENER_ANEXO_F>();
                                var objNuevoDatoVolumen = new List<O_OBTENER_VOLUMEN_IMPORTADO>();

                                var registrosBase = lstResultadoMovBase;

                                foreach (var item in registrosBase)
                                {
                                    listaF1 = item.OBSERVACION_MOVIMIENTO.Split('|');

                                    lblNombreEmpresa.Text = "a) Nombre de la Empresa: " + tlsOperadores.FocusedNode.GetValue("StrDescripcion").ToString();

                                    //lblMontoCompensar.Text = "a) Monto total solicitado para compensar: " + listaF1[1].ToString();

                                    if (tlsOperadores.FocusedNode.GetValue("StrDescripcion").ToString() == "YACIMIENTOS PETROLIFEROS FISCALES BOLIVIANOS - YPFB")
                                    {
                                        chbYacimiento.Checked = true;
                                        chbLogistica.Checked = false;
                                    }
                                    else
                                    {
                                        chbYacimiento.Checked = false;
                                        chbLogistica.Checked = true;
                                    }

                                    //formulario 2

                                    txtDuiRegularizadaF2.Text = listaF1[0].ToString();
                                    Session[CVariablesSesion.txtDuiRegularizadaF2] = listaF1[0].ToString();
                                    txtFechaDuiF2.Text = registros[0].FECHA_OPERACION_DIA.ToString();
                                    Session[CVariablesSesion.txtFechaDuiF2] = registros[0].FECHA_OPERACION_DIA.ToString();


                                    if (listaF1.Count() == 12)
                                    {
                                        txtFronteraIngresoF2.Text = listaF1[9].ToString();
                                        Session[CVariablesSesion.txtFronteraIngresoF2] = listaF1[9].ToString();
                                        txtIncontermsF2.Text = listaF1[10].ToString();
                                        Session[CVariablesSesion.txtIncontermsF2] = listaF1[10].ToString();
                                    }

                                    txtPrecioGasolinaBlancaF2.Text = listaF1[4].ToString();
                                    Session[CVariablesSesion.txtPrecioGasolinaBlancaF2] = listaF1[4].ToString();
                                    txtPrecioNetoIvaF2.Text = listaF1[5].ToString();
                                    Session[CVariablesSesion.txtPrecioNetoIvaF2] = listaF1[5].ToString();
                                    txtTarifaPoliductoF2.Text = listaF1[6].ToString();
                                    Session[CVariablesSesion.txtTarifaPoliductoF2] = listaF1[6].ToString();
                                    txtTarifaNetoIvaF2.Text = listaF1[7].ToString();
                                    Session[CVariablesSesion.txtTarifaNetoIvaF2] = listaF1[7].ToString();


                                    var datoXgb = Convert.ToDecimal(listaF1[2].ToString()) > 1 ? Convert.ToDecimal(listaF1[2].ToString()) : (Convert.ToDecimal(listaF1[2].ToString()) * 100);
                                    var datoXimp = Convert.ToDecimal(listaF1[3].ToString()) > 1 ? Convert.ToDecimal(listaF1[3].ToString()) : (Convert.ToDecimal(listaF1[3].ToString()) * 100);
                                    var datoXgbC = datoXgb / 100;
                                    var datoXimpC = datoXimp / 100;
                                    var datoPpt = Convert.ToDecimal(listaF1[8].ToString());

                                    txtCostoGasolinaBlancaNeto.Text = (Convert.ToDecimal(listaF1[5].ToString()) + Convert.ToDecimal(listaF1[7].ToString())).ToString();

                                    #region sumatorias de anexos
                                    
                                    var listaAf2 = (from datoA in objVolImportA
                                                    where datoA.IDMOVIMIENTO == item.ID_MOVIMIENTO
                                                    group datoA by datoA.RON into datoAGroup
                                                    select new
                                                    {
                                                        RON = datoAGroup.Key,
                                                        VOLUMEN = datoAGroup.Sum(x => x.VOLUMEN),
                                                        COSTO = datoAGroup.Sum(x => x.COSTO)
                                                    }).ToList();

                                    lblAnexoA.Text = "El Costo de Producto, es la suma del valor facturado de la DUI :" + listaF1[0].ToString() + " en Bs. " + Math.Round(listaAf2.Sum(x => x.COSTO), 2).ToString();

                                    Session[CVariablesSesion.GrillaResumenAnexoA] = listaAf2;

                                    var listaBf2 = (from datoA in objVolImportB
                                                    where datoA.IDMOVIMIENTO == item.ID_MOVIMIENTO
                                                    group datoA by datoA.RON into datoAGroup
                                                    select new
                                                    {
                                                        RON = datoAGroup.Key,
                                                        VOLUMEN = datoAGroup.Sum(x => x.VOLUMEN),
                                                        COSTO = datoAGroup.Sum(x => x.COSTO)
                                                    }).ToList();

                                    lblAnexoB.Text = "El Costo total de Transporte, es la suma del valor facturado de la DUI :" + listaF1[0].ToString() + " en Bs. " + Math.Round(listaBf2.Sum(x => x.COSTO), 2).ToString();

                                    Session[CVariablesSesion.GrillaResumenAnexoB1] = listaBf2;

                                    var listaCf2 = (from datoA in objVolImportC
                                                    where datoA.IDMOVIMIENTO == item.ID_MOVIMIENTO
                                                    group datoA by datoA.RON into datoAGroup
                                                    select new
                                                    {
                                                        RON = datoAGroup.Key,
                                                        VOLUMEN = datoAGroup.Sum(x => x.VOLUMEN),
                                                        COSTO = datoAGroup.Sum(x => x.COSTO)
                                                    }).ToList();

                                    lblAnexoC.Text = "El Costo total de Seguro, es la suma del valor facturado de la DUI :" + listaF1[0].ToString() + " en Bs. " + Math.Round(listaCf2.Sum(x => x.COSTO), 2).ToString();

                                    Session[CVariablesSesion.GrillaResumenAnexoC] = listaCf2;

                                    var listaD1F2 = (from datoA in objVolImportD
                                                     where datoA.VOLUMEN == 1 && datoA.IDMOVIMIENTO == item.ID_MOVIMIENTO
                                                     group datoA by datoA.RON into datoAGroup
                                                     select new
                                                     {
                                                         RON = datoAGroup.Key,
                                                         VOLUMEN = datoAGroup.Sum(x => x.VOLUMEN),
                                                         COSTO = datoAGroup.Sum(x => x.COSTO)
                                                     }).ToList();

                                    Session[CVariablesSesion.GrillaResumenAnexoD1] = listaD1F2;

                                    var listaDf2 = (from datoA in objVolImportD
                                                    where datoA.IDMOVIMIENTO == item.ID_MOVIMIENTO
                                                    group datoA by datoA.RON into datoAGroup
                                                    select new
                                                    {
                                                        RON = datoAGroup.Key,
                                                        VOLUMEN = datoAGroup.Sum(x => x.VOLUMEN),
                                                        COSTO = datoAGroup.Sum(x => x.COSTO)
                                                    }).ToList();

                                    lblAnexoD.Text = "El Costo total de Inspeccion, es la suma del valor facturado de la DUI :" + listaF1[0].ToString() + " en Bs. " + Math.Round(listaDf2.Sum(x => x.COSTO), 2).ToString();

                                    Session[CVariablesSesion.GrillaResumenAnexoD2] = listaDf2;

                                    var listaEf2 = (from datoA in objVolImportE
                                                    where datoA.IDMOVIMIENTO == item.ID_MOVIMIENTO
                                                    group datoA by datoA.RON into datoAGroup
                                                    select new
                                                    {
                                                        RON = datoAGroup.Key,
                                                        VOLUMEN = datoAGroup.Sum(x => x.VOLUMEN),
                                                        COSTO = datoAGroup.Sum(x => x.COSTO)
                                                    }).ToList();

                                    lblAnexoE.Text = "El Costo total Aduanero, es la suma del valor facturado de la DUI :" + listaF1[0].ToString() + " en Bs. " + Math.Round(listaEf2.Sum(x => x.COSTO), 2).ToString();

                                    Session[CVariablesSesion.GrillaResumenAnexoE] = listaEf2;

                                    var listaGf2 = (from datoA in objVolImportG
                                                    where datoA.IDMOVIMIENTO == item.ID_MOVIMIENTO
                                                    group datoA by datoA.RON into datoAGroup
                                                    select new
                                                    {
                                                        RON = datoAGroup.Key,
                                                        VOLUMEN = datoAGroup.Sum(x => x.VOLUMEN),
                                                        COSTO = datoAGroup.Sum(x => x.COSTO)
                                                    }).ToList();

                                    lblAnexoG.Text = "El Costo total de Almacenaje, es la suma del valor facturado de la DUI :" + listaF1[0].ToString() + " en Bs. " + Math.Round(listaGf2.Sum(x => x.COSTO), 2).ToString();

                                    Session[CVariablesSesion.GrillaResumenAnexoF1] = listaEf2;

                                    var listaHf2 = (from datoA in objVolImportH
                                                    where datoA.IDMOVIMIENTO == item.ID_MOVIMIENTO
                                                    group datoA by datoA.RON into datoAGroup
                                                    select new
                                                    {
                                                        RON = datoAGroup.Key,
                                                        VOLUMEN = datoAGroup.Sum(x => x.VOLUMEN),
                                                        COSTO = datoAGroup.Sum(x => x.COSTO)
                                                    }).ToList();

                                    lblAnexoH.Text = "El Costo total de Monitoreo, es la suma del valor facturado de la DUI :" + listaF1[0].ToString() + " en Bs. " + Math.Round(listaHf2.Sum(x => x.COSTO), 2).ToString();

                                    Session[CVariablesSesion.GrillaResumenAnexoH] = listaHf2;

                                    #endregion

                                    lblAnexoF.Text = "";
                                    lblAnexoI.Text = "";
                                    
                                    var objTrasaccionF1 = new O_OBTENER_ANEXO_F();
                                    var objTrasaccionF2 = new O_OBTENER_ANEXO_F();

                                    //anexo F
                                    foreach (var datoFProducto in listaAf2)
                                    {
                                        if (item.COD_HIDROCARBURO == datoFProducto.RON)
                                        {
                                            objTrasaccionF1.NRO_DUI = listaF1[0].ToString();
                                            objTrasaccionF1.PROVEEDOR = registros[0].RAZON_SOCIAL;
                                            objTrasaccionF1.NRO_FACTURA_PROVEEDOR = "EN ANEXO A";
                                            objTrasaccionF1.MONTO_FACTURA_PROVEEDOR = listaAf2.Count() == 0 ? 0 : datoFProducto.COSTO;
                                            objTrasaccionF1.COSTO_FINANCIERO = Math.Round(objTrasaccionF1.MONTO_FACTURA_PROVEEDOR * Convert.ToDecimal("0.001"), 2);
                                            objTrasaccionF1.COSTO_FINANCIERO_NETO = Math.Round(objTrasaccionF1.COSTO_FINANCIERO * Convert.ToDecimal("0.87"), 2);

                                            objNuevoDatoF1.Add(objTrasaccionF1);                                           
                                        }
                                        else {
                                            //todo implementado
                                        }
                                    }

                                    foreach (var datoFServicio in listaD1F2)
                                    {
                                        if (item.COD_HIDROCARBURO == datoFServicio.RON)
                                        {
                                            objTrasaccionF2.NRO_DUI = listaF1[0].ToString();
                                            objTrasaccionF2.PROVEEDOR = registros[0].RAZON_SOCIAL;
                                            objTrasaccionF2.NRO_FACTURA_PROVEEDOR = "EN ANEXO D1";
                                            objTrasaccionF2.MONTO_FACTURA_PROVEEDOR = listaD1F2.Count() == 0 ? 0 : datoFServicio.COSTO;
                                            objTrasaccionF2.COSTO_FINANCIERO = Math.Round(objTrasaccionF2.MONTO_FACTURA_PROVEEDOR * Convert.ToDecimal("0.001"), 2);
                                            objTrasaccionF2.COSTO_FINANCIERO_NETO = Math.Round(objTrasaccionF2.COSTO_FINANCIERO * Convert.ToDecimal("0.87"), 2);
                                            
                                            objNuevoDatoF2.Add(objTrasaccionF2);
                                        }
                                        else {
                                            //todo implementado
                                        }
                                    }
                                    
                                    var objCostoAnexo = new O_OBTENER_COSTOS_IMPORTADO();

                                    #region costos sumados por anexo

                                    foreach (var datoAnexoA in listaAf2)
                                    {
                                        if (item.COD_HIDROCARBURO == datoAnexoA.RON)
                                        {
                                            objCostoAnexo.RON = datoAnexoA.RON;
                                            objCostoAnexo.A = Math.Round(datoAnexoA.COSTO, 2);
                                        }
                                        else
                                        {
                                            //todo implementado
                                        }
                                    }

                                    foreach (var datoAnexoB in listaBf2)
                                    {
                                        if (item.COD_HIDROCARBURO == datoAnexoB.RON)
                                        {
                                            objCostoAnexo.B = Math.Round(datoAnexoB.COSTO, 2);
                                        }
                                        else
                                        {
                                            //todo implementado
                                        }
                                    }

                                    foreach (var datoAnexoC in listaCf2)
                                    {
                                        if (item.COD_HIDROCARBURO == datoAnexoC.RON)
                                        {
                                            objCostoAnexo.C = Math.Round(datoAnexoC.COSTO, 2);
                                        }
                                        else
                                        {
                                            //todo implementado
                                        }
                                    }

                                    foreach (var datoAnexoD in listaDf2)
                                    {
                                        if (item.COD_HIDROCARBURO == datoAnexoD.RON)
                                        {
                                            objCostoAnexo.D = Math.Round(datoAnexoD.COSTO, 2);
                                        }
                                        else
                                        {
                                            //todo implementado
                                        }
                                    }

                                    foreach (var datoAnexoE in listaEf2)
                                    {
                                        if (item.COD_HIDROCARBURO == datoAnexoE.RON)
                                        {
                                            objCostoAnexo.E = Math.Round(datoAnexoE.COSTO, 2);
                                        }
                                        else
                                        {
                                            //todo implementado
                                        }
                                    }

                                    objCostoAnexo.F = Math.Round(objTrasaccionF1.COSTO_FINANCIERO_NETO + objTrasaccionF2.COSTO_FINANCIERO_NETO, 2);

                                    foreach (var datoAnexoG in listaGf2)
                                    {
                                        if (item.COD_HIDROCARBURO == datoAnexoG.RON)
                                        {
                                            objCostoAnexo.G = Math.Round(datoAnexoG.COSTO, 2);
                                        }
                                        else
                                        {
                                            //todo implementado
                                        }
                                    }

                                    foreach (var datoAnexoH in listaHf2)
                                    {
                                        if (item.COD_HIDROCARBURO == datoAnexoH.RON)
                                        {
                                            objCostoAnexo.H = Math.Round(datoAnexoH.COSTO, 2);
                                        }
                                        else
                                        {
                                            //todo implementado
                                        }
                                    }
                                    
                                    objCostoAnexo.I = Math.Round(objCostoAnexo.A + objCostoAnexo.B + objCostoAnexo.C + objCostoAnexo.D + objCostoAnexo.E + objCostoAnexo.F + objCostoAnexo.G + objCostoAnexo.H, 2);
                                    
                                    foreach (var datoAnexoA in listaAf2)
                                    {
                                        if (item.COD_HIDROCARBURO == datoAnexoA.RON)
                                        {
                                            var objCostoSub = new O_OBTENER_COSTOS_SUBVENCION();
                                            
                                            objCostoSub.DATO_1 = "EN ANEXO A";
                                            objCostoSub.DATO_2 = datoAnexoA.RON;
                                            objCostoSub.DATO_3 = datoAnexoA.VOLUMEN;
                                            objCostoSub.DATO_4 = Math.Round((objCostoAnexo.I) / datoAnexoA.VOLUMEN, 2);
                                            objCostoSub.DATO_5 = datoXimp > 1 ? datoXimp : (datoXimp * 100);
                                            objCostoSub.DATO_6 = Convert.ToDecimal(txtCostoGasolinaBlancaNeto.Text);
                                            objCostoSub.DATO_7 = datoXgb > 1 ? datoXgb : (datoXgb * 100);
                                            if (datoXimpC > 0)
                                            {
                                                objCostoSub.DATO_8 = Math.Round((datoAnexoA.VOLUMEN) / (datoXimpC), 2);
                                            }
                                            else
                                            {
                                                objCostoSub.DATO_8 = 0;
                                            }

                                            objCostoSub.DATO_9 = Math.Round((((objCostoAnexo.I) / datoAnexoA.VOLUMEN) * datoXimpC) + (datoXgbC * Convert.ToDecimal(txtCostoGasolinaBlancaNeto.Text)), 2);
                                            
                                            objCostosSubvencion.Add(objCostoSub);

                                            var objVolumenImportado = new O_OBTENER_VOLUMEN_IMPORTADO();

                                            objVolumenImportado.RON = datoAnexoA.RON;
                                            objVolumenImportado.VOLUMEN = datoAnexoA.VOLUMEN;
                                            objVolumenImportado.COSTO = 0;
                                            
                                            objNuevoDatoVolumen.Add(objVolumenImportado);

                                            var objCostoSubFinal = new O_OBTENER_COSTOS_SUBVENCION_FINAL();
                                            
                                            objCostoSubFinal.DATO_1 = "YPFB";
                                            objCostoSubFinal.DATO_2 = "EN ANEXO A";
                                            objCostoSubFinal.DATO_3 = objCostoSub.DATO_8;
                                            objCostoSubFinal.DATO_4 = objCostoSub.DATO_9;
                                            objCostoSubFinal.DATO_5 = datoPpt;
                                            objCostoSubFinal.DATO_6 = Math.Round((objCostoSubFinal.DATO_3) * (objCostoSubFinal.DATO_4 - objCostoSubFinal.DATO_5), 2);
                                            
                                            objCostosSubvencionFinal.Add(objCostoSubFinal);
                                        }
                                        else
                                        {
                                            //todo implementado
                                        }
                                    }

                                    #endregion

                                    objCostos.Add(objCostoAnexo);
 
                                }
                                
                                dgvVolImportado.DataSource = objNuevoDatoVolumen.Distinct();
                                dgvVolImportado.DataBind();

                                Session[CVariablesSesion.GrillaFormulario1Rubro4] = objNuevoDatoVolumen;

                                dgvCostosErogados.DataSource = objCostos;
                                dgvCostosErogados.DataBind();

                                dgvCostosF2.DataSource = objCostos;
                                dgvCostosF2.DataBind();

                                Session[CVariablesSesion.GrillaFormulario1Rubro3] = objCostos;

                                dgvCostoSubvencion.DataSource = objCostosSubvencion;
                                dgvCostoSubvencion.DataBind();

                                Session[CVariablesSesion.GrillaFormulario2Rubro4] = objCostosSubvencion;

                                dgvSubvencionTotal.DataSource = objCostosSubvencionFinal;
                                dgvSubvencionTotal.DataBind();

                                decimal decPrecioSubencion = 0;
                                foreach (var precio in objCostosSubvencionFinal)
                                {
                                    decPrecioSubencion = decPrecioSubencion + precio.DATO_6;
                                }

                                if (decPrecioSubencion > 0)
                                {
                                    lblMontoCompensar.Text = "a) Monto total solicitado para compensar: " + decPrecioSubencion.ToString();
                                }
                                else
                                {
                                    lblMontoCompensar.Text = "a) Monto total solicitado para compensar: 0";
                                }

                                Session[CVariablesSesion.GrillaFormulario2Rubro5] = objCostosSubvencionFinal;

                                dgvCostoFinancieroProducto.DataSource = objNuevoDatoF1;
                                dgvCostoFinancieroProducto.DataBind();

                                Session[CVariablesSesion.GrillaAnexoF1] = objNuevoDatoF1;

                                dgvCostoFinancieroServicio.DataSource = objNuevoDatoF2;
                                dgvCostoFinancieroServicio.DataBind();

                                Session[CVariablesSesion.GrillaAnexoF2] = objNuevoDatoF2;

                                #endregion
                            }
                            else
                            {
                                #region fosil
                                //FORMULARIO 1

                                /*var registrosBase = lstResultadoMovBase;

                                var datos = registrosBase[0].OBSERVACION_MOVIMIENTO.ToString();

                                listaF1 = datos.Split('|');*/

                                lblNombreEmpresa.Text = "a) Nombre de la Empresa: " + tlsOperadores.FocusedNode.GetValue("StrDescripcion").ToString();

                                if (tlsOperadores.FocusedNode.GetValue("StrDescripcion").ToString() == "YACIMIENTOS PETROLIFEROS FISCALES BOLIVIANOS - YPFB")
                                {
                                    chbYacimiento.Checked = true;
                                    chbLogistica.Checked = false;
                                }
                                else
                                {
                                    chbYacimiento.Checked = false;
                                    chbLogistica.Checked = true;
                                }

                                //FORMULARIO 2
                                decimal cantVolumenAnIyA = 0;
                                decimal cantVolumenBnIyA = 0;
                                decimal cantVolumenCnIyA = 0;
                                decimal cantVolumenDnIyA = 0;
                                decimal cantVolumenEnIyA = 0;
                                decimal cantVolumenFnIyA = 0;
                                decimal cantVolumenGnIyA = 0;
                                decimal cantVolumenHnIyA = 0;
                                decimal cantVolumenInIyA = 0;

                                decimal cantVolumenAnGb = 0;
                                decimal cantVolumenBnGb = 0;
                                decimal cantVolumenCnGb = 0;
                                decimal cantVolumenDnGb = 0;
                                decimal cantVolumenEnGb = 0;
                                decimal cantVolumenFnGb = 0;
                                decimal cantVolumenGnGb = 0;
                                decimal cantVolumenHnGb = 0;
                                decimal cantVolumenInGb = 0;

                                var listaAnexoA = (List<O_OBTENER_ANEXO_A>)Session[CVariablesSesion.GrillaAnexoA];
                                var listaAnexoAGb = (List<O_OBTENER_ANEXO_A_GB>)Session[CVariablesSesion.GrillaAnexoAnGb];

                                foreach (var dato in listaAnexoA)
                                {
                                    cantVolumenAnIyA = cantVolumenAnIyA + dato.VALOR_FACTURADO;
                                }

                                foreach (var dato in listaAnexoAGb)
                                {
                                    cantVolumenAnGb = cantVolumenAnGb + Convert.ToDecimal(dato.MONTO_NETO);
                                }

                                var listaAnexoB1 = (List<O_OBTENER_ANEXO_B1>)Session[CVariablesSesion.GrillaAnexoB1];
                                var listaAnexoB2 = (List<O_OBTENER_ANEXO_B2>)Session[CVariablesSesion.GrillaAnexoB2];
                                var listaAnexoB3 = (List<O_OBTENER_ANEXO_B3>)Session[CVariablesSesion.GrillaAnexoB3];

                                var listaAnexoB1Gb = (List<O_OBTENER_ANEXO_B1>)Session[CVariablesSesion.GrillaAnexoB1Gb];
                                var listaAnexoB2Gb = (List<O_OBTENER_ANEXO_B2>)Session[CVariablesSesion.GrillaAnexoB2Gbc];
                                var listaAnexoB2GbDu = (List<O_OBTENER_ANEXO_B2_DU>)Session[CVariablesSesion.GrillaAnexoB2GbD];

                                foreach (var dato in listaAnexoB1)
                                {
                                    cantVolumenBnIyA = cantVolumenBnIyA + dato.COSTO_TRANSPORTE;
                                }
                                foreach (var dato in listaAnexoB2)
                                {
                                    cantVolumenBnIyA = cantVolumenBnIyA + dato.COSTO_TRANSPORTE;
                                }
                                foreach (var dato in listaAnexoB3)
                                {
                                    cantVolumenBnIyA = cantVolumenBnIyA + dato.MONTO_NETO_IVA;
                                }

                                foreach (var dato in listaAnexoB1Gb)
                                {
                                    cantVolumenBnGb = cantVolumenBnGb + dato.COSTO_TRANSPORTE;
                                }
                                foreach (var dato in listaAnexoB2Gb)
                                {
                                    cantVolumenBnGb = cantVolumenBnGb + dato.COSTO_TRANSPORTE;
                                }
                                foreach (var dato in listaAnexoB2GbDu)
                                {
                                    cantVolumenBnGb = cantVolumenBnGb + Convert.ToDecimal(dato.MONTO);
                                }

                                var listaAnexoC = (List<O_OBTENER_ANEXO_C>)Session[CVariablesSesion.GrillaAnexoC];

                                foreach (var dato in listaAnexoC)
                                {
                                    cantVolumenCnIyA = cantVolumenCnIyA + dato.MONTO_NETO_IVA;
                                }

                                var listaAnexoD1 = (List<O_OBTENER_ANEXO_D>)Session[CVariablesSesion.GrillaAnexoD1];
                                var listaAnexoD2 = (List<O_OBTENER_ANEXO_D>)Session[CVariablesSesion.GrillaAnexoD2];
                                var listaAnexoD3 = (List<O_OBTENER_ANEXO_D>)Session[CVariablesSesion.GrillaAnexoD3];

                                var listaAnexoD1Gb = (List<O_OBTENER_ANEXO_D>)Session[CVariablesSesion.GrillaAnexoD1Gb];
                                var listaAnexoD2Gb = (List<O_OBTENER_ANEXO_D>)Session[CVariablesSesion.GrillaAnexoD2Gb];
                                var listaAnexoD3GB = (List<O_OBTENER_ANEXO_D>)Session[CVariablesSesion.GrillaAnexoD3Gb];

                                foreach (var dato in listaAnexoD1)
                                {
                                    cantVolumenDnIyA = cantVolumenDnIyA + dato.MONTO_INSPECCION;
                                }
                                foreach (var dato in listaAnexoD2)
                                {
                                    cantVolumenDnIyA = cantVolumenDnIyA + dato.MONTO_INSPECCION;
                                }
                                foreach (var dato in listaAnexoD3)
                                {
                                    cantVolumenDnIyA = cantVolumenDnIyA + dato.MONTO_INSPECCION;
                                }

                                foreach (var dato in listaAnexoD1Gb)
                                {
                                    cantVolumenDnGb = cantVolumenDnGb + dato.MONTO_INSPECCION;
                                }
                                foreach (var dato in listaAnexoD2Gb)
                                {
                                    cantVolumenDnGb = cantVolumenDnGb + dato.MONTO_INSPECCION;
                                }
                                foreach (var dato in listaAnexoD3GB)
                                {
                                    cantVolumenDnGb = cantVolumenDnGb + dato.MONTO_INSPECCION;
                                }

                                var listaAnexoE = (List<O_OBTENER_ANEXO_E>)Session[CVariablesSesion.GrillaAnexoE];

                                foreach (var dato in listaAnexoE)
                                {
                                    cantVolumenEnIyA = cantVolumenEnIyA + dato.TOTAL_COSTO;
                                }

                                var listaAnexoF1 = (List<O_OBTENER_ANEXO_F>)Session[CVariablesSesion.GrillaAnexoF1];
                                var listaAnexoF2 = (List<O_OBTENER_ANEXO_F>)Session[CVariablesSesion.GrillaAnexoF2];

                                foreach (var dato in listaAnexoF1)
                                {
                                    cantVolumenFnIyA = cantVolumenFnIyA + dato.COSTO_FINANCIERO_NETO;
                                }
                                foreach (var dato in listaAnexoF2)
                                {
                                    cantVolumenFnIyA = cantVolumenFnIyA + dato.COSTO_FINANCIERO_NETO;
                                }

                                var listaAnexoG1 = (List<O_OBTENER_ANEXO_G>)Session[CVariablesSesion.GrillaAnexoG1];
                                var listaAnexoG2 = (List<O_OBTENER_ANEXO_G>)Session[CVariablesSesion.GrillaAnexoG2];

                                var listaAnexoG1Gb = (List<O_OBTENER_ANEXO_G>)Session[CVariablesSesion.GrillaAnexoG1Gb];
                                var listaAnexoG2Gb = (List<O_OBTENER_ANEXO_G>)Session[CVariablesSesion.GrillaAnexoG2Gb];

                                foreach (var dato in listaAnexoG1)
                                {
                                    cantVolumenGnIyA = cantVolumenGnIyA + dato.MONTO_NETO_IVA;
                                }
                                foreach (var dato in listaAnexoG2)
                                {
                                    cantVolumenGnIyA = cantVolumenGnIyA + dato.MONTO_NETO_IVA;
                                }

                                foreach (var dato in listaAnexoG1Gb)
                                {
                                    cantVolumenGnGb = cantVolumenGnGb + dato.MONTO_NETO_IVA;
                                }
                                foreach (var dato in listaAnexoG2Gb)
                                {
                                    cantVolumenGnGb = cantVolumenGnGb + dato.MONTO_NETO_IVA;
                                }

                                var listaAnexoH = (List<O_OBTENER_ANEXO_H>)Session[CVariablesSesion.GrillaAnexoH];

                                var listaAnexoHGb = (List<O_OBTENER_ANEXO_H>)Session[CVariablesSesion.GrillaAnexoHnGb];

                                foreach (var dato in listaAnexoH)
                                {
                                    cantVolumenHnIyA = cantVolumenHnIyA + dato.MONTO_MONITOREO;
                                }

                                foreach (var dato in listaAnexoHGb)
                                {
                                    cantVolumenHnGb = cantVolumenHnGb + dato.MONTO_MONITOREO;
                                }

                                var listaAnexoI1 = (List<O_OBTENER_ANEXO_I>)Session[CVariablesSesion.GrillaAnexoI1];
                                var listaAnexoI2 = (List<O_OBTENER_ANEXO_I>)Session[CVariablesSesion.GrillaAnexoI2];
                                var listaAnexoIGb = (List<O_OBTENER_ANEXO_I_GB>)Session[CVariablesSesion.GrillaAnexoIGb];

                                foreach (var dato in listaAnexoI1)
                                {
                                    cantVolumenInIyA = cantVolumenInIyA + dato.VOLUMEN;
                                }
                                foreach (var dato in listaAnexoI2)
                                {
                                    cantVolumenInIyA = cantVolumenInIyA + dato.VOLUMEN;
                                }

                                foreach (var dato in listaAnexoIGb)
                                {
                                    cantVolumenInGb = cantVolumenHnGb + dato.VOLUMEN;
                                }

                                txtSector.Text = sector(tlsOperadores.FocusedNode.GetValue("StrDepartamento").ToString());

                                var objCostoSu1 = new O_OBTENER_COSTOS_UNITARIO();
                                var objCostoSu2 = new O_OBTENER_COSTOS_UNITARIO();

                                var objCostoAnexo = new O_OBTENER_COSTOS_IMPORTADO();

                                objCostoAnexo.RON = "INSUMOS Y ADITIVOS";
                                objCostoAnexo.A = Math.Round(cantVolumenAnIyA, 2);
                                objCostoAnexo.B = Math.Round(cantVolumenBnIyA, 2);
                                objCostoAnexo.C = Math.Round(cantVolumenCnIyA, 2);
                                objCostoAnexo.D = Math.Round(cantVolumenDnIyA, 2);
                                objCostoAnexo.E = Math.Round(cantVolumenEnIyA, 2);
                                objCostoAnexo.F = Math.Round(cantVolumenFnIyA, 2);
                                objCostoAnexo.G = Math.Round(cantVolumenGnIyA, 2);
                                objCostoAnexo.H = Math.Round(cantVolumenHnIyA, 2);
                                objCostoAnexo.I = Math.Round(objCostoAnexo.A + objCostoAnexo.B + objCostoAnexo.C + objCostoAnexo.D + objCostoAnexo.E + objCostoAnexo.F + objCostoAnexo.G + objCostoAnexo.H, 2);

                                var objCostoUnitario = new O_OBTENER_COSTOS_UNITARIO();

                                objCostoUnitario.DATO_1 = "INSUMOS Y ADITIVOS";
                                objCostoUnitario.DATO_2 = cantVolumenInIyA;
                                objCostoUnitario.DATO_3 = objCostoAnexo.I;
                                if (cantVolumenInIyA == 0)
                                {
                                    objCostoUnitario.DATO_4 = 0;
                                }
                                else
                                {
                                    objCostoUnitario.DATO_4 = Math.Round(objCostoAnexo.I / cantVolumenInIyA, 2);
                                }


                                var listaAnexoIGBase = (List<O_OBTENER_ANEXO_I_GBASE>)Session[CVariablesSesion.GrillaAnexoIGbase];
                                foreach (var dato in listaAnexoIGBase)
                                {
                                    objCostoSu1.DATO_1 = "Volumen (lt)";
                                    objCostoSu1.DATO_2 = dato.DATO_1;
                                    objCostoSu1.DATO_3 = dato.DATO_2;
                                    objCostoSu1.DATO_4 = dato.DATO_3;

                                    txtMesRecibido.Text = dato.MES_RECIBIDO;

                                }

                                objCostos.Add(objCostoAnexo);

                                objCostosUni.Add(objCostoUnitario);

                                var objCostoSu1C = new O_OBTENER_COSTOS_UNITARIO();
                                var objCostoSu2C = new O_OBTENER_COSTOS_UNITARIO();

                                var objCostoAnexoGb = new O_OBTENER_COSTOS_IMPORTADO();

                                objCostoAnexoGb.RON = "GASOLINA BLANCA";
                                objCostoAnexoGb.A = Math.Round(cantVolumenAnGb, 2);
                                objCostoAnexoGb.B = Math.Round(cantVolumenBnGb, 2);
                                objCostoAnexoGb.C = Math.Round(cantVolumenCnGb, 2);
                                objCostoAnexoGb.D = Math.Round(cantVolumenDnGb, 2);
                                objCostoAnexoGb.E = Math.Round(cantVolumenEnGb, 2);
                                objCostoAnexoGb.F = Math.Round(cantVolumenFnGb, 2);
                                objCostoAnexoGb.G = Math.Round(cantVolumenGnGb, 2);
                                objCostoAnexoGb.H = Math.Round(cantVolumenHnGb, 2);
                                objCostoAnexoGb.I = Math.Round(objCostoAnexoGb.A + objCostoAnexoGb.B + objCostoAnexoGb.C + objCostoAnexoGb.D + objCostoAnexoGb.E + objCostoAnexoGb.F + objCostoAnexoGb.G + objCostoAnexoGb.H, 2);

                                var objCostoUnitarioGb = new O_OBTENER_COSTOS_UNITARIO();

                                objCostoUnitarioGb.DATO_1 = "GASOLINA BLANCA";
                                objCostoUnitarioGb.DATO_2 = cantVolumenInGb;
                                objCostoUnitarioGb.DATO_3 = objCostoAnexoGb.I;
                                if (cantVolumenInGb == 0)
                                {
                                    objCostoUnitarioGb.DATO_4 = 0;
                                }
                                else
                                {
                                    objCostoUnitarioGb.DATO_4 = Math.Round(objCostoAnexoGb.I / cantVolumenInGb, 2);
                                }


                                objCostoSu1C.DATO_1 = "Costo (Bs/Lt)";
                                objCostoSu1C.DATO_2 = objCostoUnitario.DATO_4;
                                objCostoSu1C.DATO_3 = objCostoUnitarioGb.DATO_4;
                                if (objCostoSu1.DATO_4 == 0)
                                {
                                    objCostoSu1C.DATO_4 = 0;
                                }
                                else
                                {
                                    objCostoSu1C.DATO_4 = Math.Round(((objCostoSu1.DATO_2 / objCostoSu1.DATO_4) * objCostoSu1C.DATO_2) + ((objCostoSu1.DATO_3 / objCostoSu1.DATO_4) * objCostoSu1C.DATO_3), 2);
                                }


                                objCostoSu2.DATO_1 = "Volumen (lt)";
                                objCostoSu2.DATO_2 = objCostoUnitario.DATO_2;
                                objCostoSu2.DATO_3 = objCostoUnitarioGb.DATO_2;
                                objCostoSu2.DATO_4 = objCostoSu2.DATO_2 + objCostoSu2.DATO_3;

                                objCostoSu2C.DATO_1 = "Costo (Bs/Lt)";
                                objCostoSu2C.DATO_2 = objCostoUnitario.DATO_4;
                                objCostoSu2C.DATO_3 = objCostoUnitarioGb.DATO_4;
                                if (objCostoSu2.DATO_4 == 0)
                                {
                                    objCostoSu2C.DATO_4 = 0;
                                }
                                else
                                {
                                    objCostoSu2C.DATO_4 = Math.Round(((objCostoSu2.DATO_2 / objCostoSu2.DATO_4) * objCostoSu2C.DATO_2) + ((objCostoSu2.DATO_3 / objCostoSu2.DATO_4) * objCostoSu2C.DATO_3), 2);
                                }


                                var objCosTotalSuben = new O_OBTENER_COSTOS_TOT_SUB();
                                objCosTotalSuben.DATO_1 = objCostoSu1.DATO_4;
                                objCosTotalSuben.DATO_2 = objCostoSu1C.DATO_4;
                                objCosTotalSuben.DATO_3 = Convert.ToDecimal("3.08");
                                objCosTotalSuben.DATO_4 = Math.Round((objCosTotalSuben.DATO_1 * (objCosTotalSuben.DATO_2 - objCosTotalSuben.DATO_3)), 2);

                                lblMontoCompensar.Text = "a) Monto total solicitado para compensar: " + objCosTotalSuben.DATO_4.ToString();

                                objCostosTotSub.Add(objCosTotalSuben);

                                objCostos.Add(objCostoAnexoGb);

                                objCostosUni.Add(objCostoUnitarioGb);

                                objCostosUniSub1.Add(objCostoSu1);
                                objCostosUniSub1.Add(objCostoSu1C);

                                objCostosUniSub2.Add(objCostoSu2);
                                objCostosUniSub2.Add(objCostoSu2C);

                                dgvCostosF2.DataSource = objCostos;
                                dgvCostosF2.DataBind();

                                dgvRubroCostoUnitario.DataSource = objCostosUni;
                                dgvRubroCostoUnitario.DataBind();

                                dgvCostoUniSub.DataSource = objCostosUniSub1;
                                dgvCostoUniSub.DataBind();

                                dgvCostoUnitarioSu.DataSource = objCostosUniSub2;
                                dgvCostoUnitarioSu.DataBind();

                                dgvCostosErogadosGb.DataSource = objCostosTotSub;
                                dgvCostosErogadosGb.DataBind();

                                lblAnexoA.Text = "";
                                lblAnexoB.Text = "";
                                lblAnexoC.Text = "";
                                lblAnexoD.Text = "";
                                lblAnexoE.Text = "";
                                lblAnexoG.Text = "";
                                lblAnexoH.Text = "";

                                #endregion

                            }

                            #region configuracion grilla

                            if (decOpcion == 1)
                            {
                                dgvAnexoA.Columns[2].Visible = true;
                                dgvAnexoA.Columns[3].Visible = false;

                                lblAaRubroIa.ClientVisible = false;

                                lblAaRubroGb.ClientVisible = false;
                                dgvAnexoAGb.ClientVisible = false;

                                lblAcRubroGb.ClientVisible = false;
                                dgvAnexoCGb.ClientVisible = false;

                                lblIcRubroGb.ClientVisible = false;
                                dgvAnexoIGb.ClientVisible = false;

                                lblB1RubroGb.ClientVisible = false;
                                dgvAnexoB1Gb.ClientVisible = false;
                                lblB2RubroDuGb.ClientVisible = false;
                                dgvAnexoB2DuGb.ClientVisible = false;
                                lblB2RubroGb.ClientVisible = false;
                                dgvAnexoB2Gb.ClientVisible = false;

                                dgvCostosErogadosGb.ClientVisible = false;
                                dgvCostosErogados.ClientVisible = true;
                                dgvVolImportado.ClientVisible = true;

                                lblForm2Dui.ClientVisible = true;
                                txtDuiRegularizadaF2.ClientVisible = true;
                                lblForm2FechaDui.ClientVisible = true;
                                txtFechaDuiF2.ClientVisible = true;
                                lblForms2FronIngr.ClientVisible = true;
                                txtFronteraIngresoF2.ClientVisible = true;
                                lblForm2Incoterms.ClientVisible = true;
                                txtIncontermsF2.ClientVisible = true;

                                lblForm2MesRecibido.ClientVisible = false;
                                txtMesRecibido.ClientVisible = false;
                                lblForm2Sector.ClientVisible = false;
                                txtSector.ClientVisible = false;

                                lblPlantaOrigen.ClientVisible = true;
                                txtPlantaOrigen.ClientVisible = true;
                                lblPlantaDestino.ClientVisible = true;
                                txtPlantaDestino.ClientVisible = true;

                                lblPreNeto.ClientVisible = true;
                                txtPrecioNetoIvaF2.ClientVisible = true;
                                lblTaTraPol.ClientVisible = true;
                                txtTarifaPoliductoF2.ClientVisible = true;
                                lblTaNeto.ClientVisible = true;
                                txtTarifaNetoIvaF2.ClientVisible = true;
                                lblCosGasoBlanca.ClientVisible = true;
                                txtCostoGasolinaBlancaNeto.ClientVisible = true;

                                dgvRubroCostoUnitario.ClientVisible = false;

                                dgvCostoSubvencion.ClientVisible = true;
                                dgvCostoUniSub.ClientVisible = false;

                                dgvSubvencionTotal.ClientVisible = true;
                                dgvCostoUnitarioSu.ClientVisible = false;

                                dgvCostoFinancieroProducto.Columns[1].Visible = false;

                                lblIcRuGbase.ClientVisible = false;
                                dgvAnexoIGbase.ClientVisible = false;

                            }
                            else
                            {
                                dgvAnexoA.Columns[2].Visible = false;
                                dgvAnexoA.Columns[3].Visible = true;

                                lblAaRubroIa.ClientVisible = true;

                                lblAaRubroGb.ClientVisible = true;
                                dgvAnexoAGb.ClientVisible = true;

                                lblAcRubroGb.ClientVisible = true;
                                dgvAnexoCGb.ClientVisible = true;

                                lblIcRubroGb.ClientVisible = true;
                                dgvAnexoIGb.ClientVisible = true;

                                lblB1RubroGb.ClientVisible = true;
                                dgvAnexoB1Gb.ClientVisible = true;
                                lblB2RubroDuGb.ClientVisible = true;
                                dgvAnexoB2DuGb.ClientVisible = true;
                                lblB2RubroGb.ClientVisible = true;
                                dgvAnexoB2Gb.ClientVisible = true;

                                dgvCostosErogadosGb.ClientVisible = true;
                                dgvVolImportado.ClientVisible = false;

                                dgvCostosErogados.ClientVisible = false;

                                lblForm2Dui.ClientVisible = false;
                                txtDuiRegularizadaF2.ClientVisible = false;
                                lblForm2FechaDui.ClientVisible = false;
                                txtFechaDuiF2.ClientVisible = false;
                                lblForms2FronIngr.ClientVisible = false;
                                txtFronteraIngresoF2.ClientVisible = false;
                                lblForm2Incoterms.ClientVisible = false;
                                txtIncontermsF2.ClientVisible = false;

                                lblForm2MesRecibido.ClientVisible = true;
                                txtMesRecibido.ClientVisible = true;
                                lblForm2Sector.ClientVisible = true;
                                txtSector.ClientVisible = true;

                                lblPlantaOrigen.ClientVisible = false;
                                txtPlantaOrigen.ClientVisible = false;
                                lblPlantaDestino.ClientVisible = false;
                                txtPlantaDestino.ClientVisible = false;

                                lblformPreGb.ClientVisible = false;
                                txtPrecioGasolinaBlancaF2.ClientVisible = false;

                                lblPreNeto.ClientVisible = false;
                                txtPrecioNetoIvaF2.ClientVisible = false;
                                lblTaTraPol.ClientVisible = false;
                                txtTarifaPoliductoF2.ClientVisible = false;
                                lblTaNeto.ClientVisible = false;
                                txtTarifaNetoIvaF2.ClientVisible = false;
                                lblCosGasoBlanca.ClientVisible = false;
                                txtCostoGasolinaBlancaNeto.ClientVisible = false;

                                dgvRubroCostoUnitario.ClientVisible = true;

                                dgvCostoSubvencion.ClientVisible = false;
                                dgvCostoUniSub.ClientVisible = true;

                                dgvSubvencionTotal.ClientVisible = false;
                                dgvCostoUnitarioSu.ClientVisible = true;

                                dgvCostoFinancieroProducto.Columns[1].Visible = true;

                                lblIcRuGbase.ClientVisible = true;
                                dgvAnexoIGbase.ClientVisible = true;
                            }

                            #endregion
                        }
                    }
                    else { }

                }
                else
                { 

                }
            }
            catch (Exception exp)
            {
                #region RegistraError

                MImprimirMensaje("Error al cargar la informacion de la DUI, revisar documentos de la misma", EnumTiposMensajeAlerta.Error);
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

        private string sector(string depto)
        {
            string ubicacion = "";

            if (depto == "LA PAZ")
            {
                ubicacion = "Occidente";
            }
            else if (depto == "ORURO")
            {
                ubicacion = "Occidente";
            }
            else if (depto == "POTOSI")
            {
                ubicacion = "Occidente";
            }
            else if (depto == "PANDO")
            {
                ubicacion = "Oriente";
            }
            else if (depto == "BENI")
            {
                ubicacion = "Oriente";
            }
            else if (depto == "SANTA CRUZ")
            {
                ubicacion = "OcciOrientedente";
            }
            else if (depto == "COCHABAMBA")
            {
                ubicacion = "Sur";
            }
            else if (depto == "CHUQUISACA")
            {
                ubicacion = "Sur";
            }
            else if (depto == "TARIJA")
            {
                ubicacion = "Sur";
            }
            else { ubicacion = "SIN"; }

            return  ubicacion;
        }

        private string ObtenerPeriodo(string FechaHora)
        {
            string strMes = FechaHora.Substring(4, 2);
            string strGestion = FechaHora.Substring(0,4);
            string strPeriodo = "";

            switch (strMes)
            { 
                case "01":
                    strPeriodo = "Ene - " + strGestion;
                    break;
                case "02":
                    strPeriodo = "Feb - " + strGestion;
                    break;
                case "03":
                    strPeriodo = "Mar - " + strGestion;
                    break;
                case "04":
                    strPeriodo = "Abr - " + strGestion;
                    break;
                case "05":
                    strPeriodo = "May - " + strGestion;
                    break;
                case "06":
                    strPeriodo = "Jun - " + strGestion;
                    break;
                case "07":
                    strPeriodo = "Jul - " + strGestion;
                    break;
                case "08":
                    strPeriodo = "Ago - " + strGestion;
                    break;
                case "09":
                    strPeriodo = "Sep - " + strGestion;
                    break;
                case "10":
                    strPeriodo = "Oct - " + strGestion;
                    break;
                case "11":
                    strPeriodo = "Nov - " + strGestion;
                    break;
                case "12":
                    strPeriodo = "Dic - " + strGestion;
                    break;
            }

            return strPeriodo;
        }

        protected void btnReimprimir_Click(object sender, EventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    //lblRazonSocial.Text = tlsOperadores.FocusedNode.GetValue("StrDescPadre").ToString();
                    //lblNit.Text = tlsOperadores.FocusedNode.GetValue("LngNit").ToString();
                    //lblDireccion.Text = (tlsOperadores.FocusedNode.GetValue("StrDireccion") == null) ? "" : tlsOperadores.FocusedNode.GetValue("StrDireccion").ToString();
                    //lblDepto.Text = tlsOperadores.FocusedNode.GetValue("StrDepartamento").ToString();

                    //var objParametricas = Session[CVariablesSesion.OctanoParametrica] as List<O_LISTA_OCTANO_PAR_CTY>;
                    //var strTextoCab = objParametricas.Where(x => x.TIPO_VALOR == 8).ToList().FirstOrDefault().VALOR_FIN;
                    //var vecCabecera = strTextoCab.Split('|');

                    //var index = dgvCabecera.FocusedRowIndex;
                    //if (index >= 0 && Session[CVariablesSesion.GrillaAcumulado] != null)
                    //{
                    string strQr;
                    if (Convert.ToInt32(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()) == (int)EnumActividades.ImportacionCarburantes)
                    {
                        //Session[CVariablesSesion.TipoReporte] = EnumTipoReporte.repDjImportacion;
                        //var xreImportacion = new repDjImportadores();

                        //xreImportacion.xtcEmpresa.Text = tlsOperadores.FocusedNode.GetValue("StrDescripcion").ToString();
                        ////xreImportacion.xtcMontoCompensar.Text = listaF1[1].ToString();

                        //xreImportacion.xtcMontoCompensarLiteral.Text = "";

                        //xreImportacion.odsCostosImportados.DataSource = Session[CVariablesSesion.GrillaFormulario1Rubro3];
                        //xreImportacion.odsVolumenImportado.DataSource = Session[CVariablesSesion.GrillaFormulario1Rubro4];

                        //if (tlsOperadores.FocusedNode.GetValue("StrDescripcion").ToString() == "YACIMIENTOS PETROLIFEROS FISCALES BOLIVIANOS - YPFB")
                        //{
                        //    xreImportacion.xtcYPFB.Text = "X";
                        //    xreImportacion.xtcYPFBRefinacion.Text = "";
                        //}
                        //else
                        //{
                        //    xreImportacion.xtcYPFB.Text = "";
                        //    xreImportacion.xtcYPFBRefinacion.Text = "X";
                        //}

                        //xreImportacion.odsCostoInsumoAditivo.DataSource = Session[CVariablesSesion.GrillaFormulario1Rubro3];
                        //xreImportacion.odsCostoSubvencion.DataSource = Session[CVariablesSesion.GrillaFormulario2Rubro4];
                        //xreImportacion.odsCostoSubvencionFinal.DataSource = Session[CVariablesSesion.GrillaFormulario2Rubro5];

                        //xreImportacion.xtcDui.Text = Session[CVariablesSesion.txtDuiRegularizadaF2].ToString();
                        //xreImportacion.xtcFechaDui.Text = Session[CVariablesSesion.txtFechaDuiF2].ToString();
                        //xreImportacion.xtcFronteraIngreso.Text = Session[CVariablesSesion.txtFronteraIngresoF2].ToString();
                        //xreImportacion.xtcInconterms.Text = Session[CVariablesSesion.txtIncontermsF2].ToString();

                        //xreImportacion.xtcProveedor.Text = "";
                        //xreImportacion.xtcPlantaOrigen.Text = "";
                        //xreImportacion.xtcPlantaDestino.Text = "";

                        //xreImportacion.xtcPrecioGasolinaBlanca.Text = Session[CVariablesSesion.txtPrecioGasolinaBlancaF2].ToString();
                        //xreImportacion.xtcPrecioNetoIva.Text = Session[CVariablesSesion.txtPrecioNetoIvaF2].ToString();
                        //xreImportacion.xtcTarifaTransportePoliducto.Text = Session[CVariablesSesion.txtTarifaPoliductoF2].ToString();
                        //xreImportacion.xtcTarifaNetoIva.Text = Session[CVariablesSesion.txtTarifaNetoIvaF2].ToString();

                        //xreImportacion.xtcCostoGasolinaBlanca.Text = Convert.ToString(Convert.ToDecimal(Session[CVariablesSesion.txtPrecioNetoIvaF2]) +
                        //                                              Convert.ToDecimal(Session[CVariablesSesion.txtTarifaNetoIvaF2]));








                        //txtDuiRegularizadaF2.Text = listaF1[0].ToString();
                        //txtFechaDuiF2.Text = registros[0].FECHA_OPERACION_DIA.ToString();

                        //if (listaF1.Count() == 12)
                        //{
                        //    txtFronteraIngresoF2.Text = listaF1[9].ToString();
                        //    txtIncontermsF2.Text = listaF1[10].ToString();
                        //}

                        //txtPrecioGasolinaBlancaF2.Text = listaF1[4].ToString();
                        //txtPrecioNetoIvaF2.Text = listaF1[5].ToString();
                        //txtTarifaPoliductoF2.Text = listaF1[6].ToString();
                        //txtTarifaNetoIvaF2.Text = listaF1[7].ToString();


                        //xreImportacion.odsAnexoA.DataSource = Session[CVariablesSesion.GrillaAnexoA];
                        //xreAcumulado.xtcCabCodigo.Text = vecCabecera[0] + " ANEXO 1";
                        //xreAcumulado.xtcCabVersion.Text = vecCabecera[1];
                        //xreAcumulado.xtcCabAprobado.Text = vecCabecera[2];
                        //xreAnexoE.objectDataSource1.DataSource = Session[CVariablesSesion.GrillaAnexoE];
                        //xreAnexoE.xrPgCostosE.DataSource = Session[CVariablesSesion.GrillaAnexoE];
                        //xreAcumulado.xtcRazonSocial.Text = dgvCabecera.GetRowValues(index, "RAZON_SOCIAL").ToString();
                        //xreAcumulado.xtcActividad.Text = tlsOperadores.FocusedNode.GetValue("StrActividad").ToString();
                        //xreAcumulado.xtcDireccion.Text = dgvCabecera.GetRowValues(index, "DIRECCION").ToString();
                        //xreAcumulado.xtcDepartamento.Text = tlsOperadores.FocusedNode.GetValue("StrDepartamento").ToString();
                        //xreAcumulado.xtcUrbano.Text = dgvCabecera.GetRowValues(index, "AMBITO_OPERACION").ToString();
                        //xreAcumulado.xtcTelefono.Text = dgvCabecera.GetRowValues(index, "TELEFONO").ToString();
                        //xreAcumulado.xtcMunicipio.Text = dgvCabecera.GetRowValues(index, "MUNICIPIO").ToString();
                        //xreAcumulado.xtcPublico.Text = (dgvCabecera.GetRowValues(index, "TIPO_SOCIEDAD") == null) ? "" : dgvCabecera.GetRowValues(index, "TIPO_SOCIEDAD").ToString();
                        //xreAcumulado.xtcMes.Text = dgvCabecera.GetRowValues(index, "MES").ToString();
                        //xreAcumulado.xtcLugar.Text = "LUGAR :   " + dgvCabecera.GetRowValues(index, "LUGAR_REGISTRO");
                        //xreAcumulado.xrlDia.Text = dgvCabecera.GetRowValues(index, "APP_FECHA_REGISTRO").ToString().Substring(6, 2);
                        //xreAcumulado.xrlMes.Text = dgvCabecera.GetRowValues(index, "APP_FECHA_REGISTRO").ToString().Substring(4, 2);
                        //xreAcumulado.xrlGestion.Text = dgvCabecera.GetRowValues(index, "APP_FECHA_REGISTRO").ToString().Substring(0, 4);
                        //xreAcumulado.xrlResponsable.Text = (dgvCabecera.GetRowValues(index, "NOMBRES_REPLEGAL") == null) ? "" : dgvCabecera.GetRowValues(index, "NOMBRES_REPLEGAL").ToString();
                        //xreAcumulado.xtcCiResponsable.Text = "C.I.: " + dgvCabecera.GetRowValues(index, "CI_REPLEGAL");
                        //xreAcumulado.xrlUsuarioEmisor.Text = dgvCabecera.GetRowValues(index, "NOMBRES_USUARIO").ToString();
                        //xreAcumulado.xtcCiUsuarioEmisor.Text = "C.I.: " + dgvCabecera.GetRowValues(index, "CI_USUARIO");
                        //xreAcumulado.xtcNoCite.Text = dgvCabecera.GetRowValues(index, "CITE_GENERADO").ToString();
                        //xreAcumulado.xbcCodigoQr.Text = dgvCabecera.GetRowValues(index, "CITE_GENERADO") + "|" + cmbGestion.Value + cmbMes.Value.ToString().PadLeft(2, '0') + "|" + xreAcumulado.xrlGestion.Text + xreAcumulado.xrlMes.Text + xreAcumulado.xrlDia.Text;
                        //strQr = xreAcumulado.xbcCodigoQr.Text;

                        //var oListaOctanoParCty = objParametricas.Where(x => x.TIPO_VALOR == 7).ToList().FirstOrDefault();
                        //if (oListaOctanoParCty != null && oListaOctanoParCty.VALOR_FIN == "2")
                        //{
                        //    xreAcumulado.grhInfraestructura.Visible = true;
                        //    xreAcumulado.grfInfraestructura.Visible = true;
                        //}
                        //else
                        //{
                        //    xreAcumulado.grhInfraestructura.Visible = false;
                        //    xreAcumulado.grfInfraestructura.Visible = false;
                        //}

                        //xreAcumulado.DataSource = Session[CVariablesSesion.GrillaAcumRep];
                        //Session[CVariablesSesion.ReportePrincipal] = xreImportacion;
                    }
                    else
                    {
                        //Session[CVariablesSesion.TipoReporte] = EnumTipoReporte.DeclaracionJurada;
                        //var xreAcumulado = new repDjAcumuladoInfra();
                        //xreAcumulado.xtcCabCodigo.Text = vecCabecera[0] + " ANEXO 1";
                        //xreAcumulado.xtcCabVersion.Text = vecCabecera[1];
                        //xreAcumulado.xtcCabAprobado.Text = vecCabecera[2];

                        //xreAcumulado.xtcRazonSocial.Text = dgvCabecera.GetRowValues(index, "RAZON_SOCIAL").ToString();
                        //xreAcumulado.xtcActividad.Text = tlsOperadores.FocusedNode.GetValue("StrActividad").ToString();
                        //xreAcumulado.xtcDireccion.Text = (dgvCabecera.GetRowValues(index, "DIRECCION") == null) ? "" : dgvCabecera.GetRowValues(index, "DIRECCION").ToString();
                        //xreAcumulado.xtcDepartamento.Text = (tlsOperadores.FocusedNode.GetValue("StrDepartamento") == null) ? "" : tlsOperadores.FocusedNode.GetValue("StrDepartamento").ToString();
                        //xreAcumulado.xtcUrbano.Text = (dgvCabecera.GetRowValues(index, "AMBITO_OPERACION") == null) ? "" : dgvCabecera.GetRowValues(index, "AMBITO_OPERACION").ToString();
                        //xreAcumulado.xtcTelefono.Text = (dgvCabecera.GetRowValues(index, "TELEFONO") == null) ? "" : dgvCabecera.GetRowValues(index, "TELEFONO").ToString();
                        //xreAcumulado.xtcMunicipio.Text = (dgvCabecera.GetRowValues(index, "MUNICIPIO") == null) ? "" : dgvCabecera.GetRowValues(index, "MUNICIPIO").ToString();
                        //xreAcumulado.xtcPublico.Text = (dgvCabecera.GetRowValues(index, "TIPO_SOCIEDAD") == null) ? "" : dgvCabecera.GetRowValues(index, "TIPO_SOCIEDAD").ToString();
                        //xreAcumulado.xtcMes.Text = dgvCabecera.GetRowValues(index, "MES").ToString();
                        //xreAcumulado.xtcLugar.Text = "LUGAR :   " + dgvCabecera.GetRowValues(index, "LUGAR_REGISTRO");
                        //xreAcumulado.xrlDia.Text = dgvCabecera.GetRowValues(index, "APP_FECHA_REGISTRO").ToString().Substring(6, 2);
                        //xreAcumulado.xrlMes.Text = dgvCabecera.GetRowValues(index, "APP_FECHA_REGISTRO").ToString().Substring(4, 2);
                        //xreAcumulado.xrlGestion.Text = dgvCabecera.GetRowValues(index, "APP_FECHA_REGISTRO").ToString().Substring(0, 4);
                        //xreAcumulado.xrlResponsable.Text = (dgvCabecera.GetRowValues(index, "NOMBRES_REPLEGAL") == null) ? "" : dgvCabecera.GetRowValues(index, "NOMBRES_REPLEGAL").ToString();
                        //xreAcumulado.xtcCiResponsable.Text = "C.I.: " + dgvCabecera.GetRowValues(index, "CI_REPLEGAL");
                        //xreAcumulado.xrlUsuarioEmisor.Text = dgvCabecera.GetRowValues(index, "NOMBRES_USUARIO").ToString();
                        //xreAcumulado.xtcCiUsuarioEmisor.Text = "C.I.: " + dgvCabecera.GetRowValues(index, "CI_USUARIO");
                        //xreAcumulado.xtcNoCite.Text = dgvCabecera.GetRowValues(index, "CITE_GENERADO").ToString();
                        //xreAcumulado.xbcCodigoQr.Text = dgvCabecera.GetRowValues(index, "CITE_GENERADO") + "|" + cmbGestion.Value + cmbMes.Value.ToString().PadLeft(2, '0') + "|" + xreAcumulado.xrlGestion.Text + xreAcumulado.xrlMes.Text + xreAcumulado.xrlDia.Text;
                        //strQr = xreAcumulado.xbcCodigoQr.Text;

                        //var paramAgrup = objParametricas.Where(x => x.TIPO_VALOR == 10).ToList().FirstOrDefault();
                        //var oListaOctanoParCty = objParametricas.Where(x => x.TIPO_VALOR == 7).ToList().FirstOrDefault();
                        //xreAcumulado.grhInfraestructura.Visible = false;
                        //xreAcumulado.grfInfraestructura.Visible = false;

                        //if (oListaOctanoParCty != null && oListaOctanoParCty.VALOR_FIN == "2")
                        //{
                        //    if (paramAgrup != null && paramAgrup.VALOR_FIN != "1")
                        //    {
                        //        xreAcumulado.grhInfraestructura.Visible = true;
                        //        xreAcumulado.grfInfraestructura.Visible = true;
                        //    }
                        //}

                        //xreAcumulado.DataSource = Session[CVariablesSesion.GrillaAcumRep];
                        //Session[CVariablesSesion.ReportePrincipal] = xreAcumulado;
                    }

                    //vtas internacional
                    if (Session[CVariablesSesion.GrillaInternacional] != null)
                    {
                        //var xreInt = new repDjInternacional();
                        //xreInt.xtcCabCodigo.Text = vecCabecera[0] + " ANEXO 2";
                        //xreInt.xtcCabVersion.Text = vecCabecera[1];
                        //xreInt.xtcCabAprobado.Text = vecCabecera[2];

                        //xreInt.xtcRazonSocial.Text = dgvCabecera.GetRowValues(index, "RAZON_SOCIAL").ToString();
                        //xreInt.xtcActividad.Text = tlsOperadores.FocusedNode.GetValue("StrActividad").ToString();
                        //xreInt.xtcDireccion.Text = dgvCabecera.GetRowValues(index, "DIRECCION").ToString();
                        //xreInt.xtcDepartamento.Text = tlsOperadores.FocusedNode.GetValue("StrDepartamento").ToString();
                        //xreInt.xtcUrbano.Text = dgvCabecera.GetRowValues(index, "AMBITO_OPERACION").ToString();
                        //xreInt.xtcTelefono.Text = dgvCabecera.GetRowValues(index, "TELEFONO").ToString();
                        //xreInt.xtcMunicipio.Text = dgvCabecera.GetRowValues(index, "MUNICIPIO").ToString();
                        //xreInt.xtcPublico.Text = (dgvCabecera.GetRowValues(index, "TIPO_SOCIEDAD") == null) ? "" : dgvCabecera.GetRowValues(index, "TIPO_SOCIEDAD").ToString();
                        //xreInt.xtcMes.Text = dgvCabecera.GetRowValues(index, "MES").ToString();
                        //xreInt.xtcLugar.Text = "LUGAR :   " + dgvCabecera.GetRowValues(index, "LUGAR_REGISTRO");
                        //xreInt.xrlDia.Text = dgvCabecera.GetRowValues(index, "APP_FECHA_REGISTRO").ToString().Substring(6, 2);
                        //xreInt.xrlMes.Text = dgvCabecera.GetRowValues(index, "APP_FECHA_REGISTRO").ToString().Substring(4, 2);
                        //xreInt.xrlGestion.Text = dgvCabecera.GetRowValues(index, "APP_FECHA_REGISTRO").ToString().Substring(0, 4);
                        //xreInt.xrlResponsable.Text = (dgvCabecera.GetRowValues(index, "NOMBRES_REPLEGAL") == null) ? "" : dgvCabecera.GetRowValues(index, "NOMBRES_REPLEGAL").ToString();
                        //xreInt.xtcCiResponsable.Text = "C.I.: " + dgvCabecera.GetRowValues(index, "CI_REPLEGAL");
                        //xreInt.xrlUsuarioEmisor.Text = dgvCabecera.GetRowValues(index, "NOMBRES_USUARIO").ToString();
                        //xreInt.xtcCiUsuarioEmisor.Text = "C.I.: " + dgvCabecera.GetRowValues(index, "CI_USUARIO");
                        //xreInt.xtcNoCite.Text = dgvCabecera.GetRowValues(index, "CITE_GENERADO").ToString();
                        //xreInt.xbcCodigoQr.Text = strQr;
                        //xreInt.DataSource = Session[CVariablesSesion.GrillaInternacional];
                        //Session[CVariablesSesion.ReporteSecundario] = xreInt;
                    }

                    //Recaudo Externo
                    if (Session[CVariablesSesion.GrillaRecaudo] != null)
                    {
                        //var xreRex = new repDjRecaudoExterno();
                        //xreRex.xtcCabCodigo.Text = vecCabecera[0] + " ANEXO 3";
                        //xreRex.xtcCabVersion.Text = vecCabecera[1];
                        //xreRex.xtcCabAprobado.Text = vecCabecera[2];

                        //xreRex.xtcRazonSocial.Text = dgvCabecera.GetRowValues(index, "RAZON_SOCIAL").ToString();
                        //xreRex.xtcActividad.Text = tlsOperadores.FocusedNode.GetValue("StrActividad").ToString();
                        //xreRex.xtcDireccion.Text = dgvCabecera.GetRowValues(index, "DIRECCION").ToString();
                        //xreRex.xtcDepartamento.Text = tlsOperadores.FocusedNode.GetValue("StrDepartamento").ToString();
                        //xreRex.xtcUrbano.Text = dgvCabecera.GetRowValues(index, "AMBITO_OPERACION").ToString();
                        //xreRex.xtcTelefono.Text = dgvCabecera.GetRowValues(index, "TELEFONO").ToString();
                        //xreRex.xtcMunicipio.Text = dgvCabecera.GetRowValues(index, "MUNICIPIO").ToString();
                        //xreRex.xtcPublico.Text = (dgvCabecera.GetRowValues(index, "TIPO_SOCIEDAD") == null) ? "" : dgvCabecera.GetRowValues(index, "TIPO_SOCIEDAD").ToString();
                        //xreRex.xtcMes.Text = dgvCabecera.GetRowValues(index, "MES").ToString();
                        //xreRex.xtcLugar.Text = "LUGAR :   " + dgvCabecera.GetRowValues(index, "LUGAR_REGISTRO");
                        //xreRex.xrlDia.Text = dgvCabecera.GetRowValues(index, "APP_FECHA_REGISTRO").ToString().Substring(6, 2);
                        //xreRex.xrlMes.Text = dgvCabecera.GetRowValues(index, "APP_FECHA_REGISTRO").ToString().Substring(4, 2);
                        //xreRex.xrlGestion.Text = dgvCabecera.GetRowValues(index, "APP_FECHA_REGISTRO").ToString().Substring(0, 4);
                        //xreRex.xrlResponsable.Text = (dgvCabecera.GetRowValues(index, "NOMBRES_REPLEGAL") == null) ? "" : dgvCabecera.GetRowValues(index, "NOMBRES_REPLEGAL").ToString();
                        //xreRex.xtcCiResponsable.Text = "C.I.: " + dgvCabecera.GetRowValues(index, "CI_REPLEGAL");
                        //xreRex.xrlUsuarioEmisor.Text = dgvCabecera.GetRowValues(index, "NOMBRES_USUARIO").ToString();
                        //xreRex.xtcCiUsuarioEmisor.Text = "C.I.: " + dgvCabecera.GetRowValues(index, "CI_USUARIO");
                        //xreRex.xtcNoCite.Text = dgvCabecera.GetRowValues(index, "CITE_GENERADO").ToString();
                        //xreRex.xbcCodigoQr.Text = strQr;
                        //xreRex.DataSource = Session[CVariablesSesion.GrillaRecaudo];
                        //Session[CVariablesSesion.ReporteCuarto] = xreRex;
                    }

                    //vtas en contenedores
                    if (Session[CVariablesSesion.GrillaAuxiliar] != null)
                    {
                        //var xreCont = new repDjContenedor();
                        //xreCont.xtcCabCodigo.Text = vecCabecera[0] + " ANEXO 2";
                        //xreCont.xtcCabVersion.Text = vecCabecera[1];
                        //xreCont.xtcCabAprobado.Text = vecCabecera[2];

                        //xreCont.xtcRazonSocial.Text = dgvCabecera.GetRowValues(index, "RAZON_SOCIAL").ToString();
                        //xreCont.xtcActividad.Text = tlsOperadores.FocusedNode.GetValue("StrActividad").ToString();
                        //xreCont.xtcDireccion.Text = dgvCabecera.GetRowValues(index, "DIRECCION").ToString();
                        //xreCont.xtcDepartamento.Text = tlsOperadores.FocusedNode.GetValue("StrDepartamento").ToString();
                        //xreCont.xtcUrbano.Text = dgvCabecera.GetRowValues(index, "AMBITO_OPERACION").ToString();
                        //xreCont.xtcTelefono.Text = dgvCabecera.GetRowValues(index, "TELEFONO").ToString();
                        //xreCont.xtcMunicipio.Text = dgvCabecera.GetRowValues(index, "MUNICIPIO").ToString();
                        //xreCont.xtcPublico.Text = (dgvCabecera.GetRowValues(index, "TIPO_SOCIEDAD") == null) ? "" : dgvCabecera.GetRowValues(index, "TIPO_SOCIEDAD").ToString();
                        //xreCont.xtcMes.Text = dgvCabecera.GetRowValues(index, "MES").ToString();
                        //xreCont.xtcLugar.Text = "LUGAR :   " + dgvCabecera.GetRowValues(index, "LUGAR_REGISTRO");
                        //xreCont.xrlDia.Text = dgvCabecera.GetRowValues(index, "APP_FECHA_REGISTRO").ToString().Substring(6, 2);
                        //xreCont.xrlMes.Text = dgvCabecera.GetRowValues(index, "APP_FECHA_REGISTRO").ToString().Substring(4, 2);
                        //xreCont.xrlGestion.Text = dgvCabecera.GetRowValues(index, "APP_FECHA_REGISTRO").ToString().Substring(0, 4);
                        //xreCont.xrlResponsable.Text = (dgvCabecera.GetRowValues(index, "NOMBRES_REPLEGAL") == null) ? "" : dgvCabecera.GetRowValues(index, "NOMBRES_REPLEGAL").ToString();
                        //xreCont.xtcCiResponsable.Text = "C.I.: " + dgvCabecera.GetRowValues(index, "CI_REPLEGAL");
                        //xreCont.xrlUsuarioEmisor.Text = dgvCabecera.GetRowValues(index, "NOMBRES_USUARIO").ToString();
                        //xreCont.xtcCiUsuarioEmisor.Text = "C.I.: " + dgvCabecera.GetRowValues(index, "CI_USUARIO");
                        //xreCont.xtcNoCite.Text = dgvCabecera.GetRowValues(index, "CITE_GENERADO").ToString();
                        //xreCont.xbcCodigoQr.Text = strQr;
                        //xreCont.DataSource = Session[CVariablesSesion.GrillaAuxiliar];
                        //Session[CVariablesSesion.ReporteTercero] = xreCont;
                    }

                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( 'WFReporte.aspx', '')", true);
                    //}

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

        protected void pnlCargaDui_Callback(object sender, CallbackEventArgsBase e)
        {
            decimal decOpcion = Convert.ToDecimal(cmbResMin.Value);
            
            string strFechaInicio = "";
            string strFechaFin = "";
            string strFechaInicioT = "";
            string strFechaFinT = "";

            if (decOpcion == 1)
            {
                strFechaInicio = detFechaIni.Date.Year + detFechaIni.Date.Month.ToString().PadLeft(2, '0') + detFechaIni.Date.Day.ToString().PadLeft(2, '0') + "000000";
                strFechaFin = detFechaIni.Date.Year + detFechaIni.Date.Month.ToString().PadLeft(2, '0') + detFechaIni.Date.Day.ToString().PadLeft(2, '0') + "235959";
                strFechaInicioT = detFechaIni.Date.Year + detFechaIni.Date.Month.ToString().PadLeft(2, '0') + detFechaIni.Date.Day.ToString().PadLeft(2, '0');
                strFechaFinT = detFechaIni.Date.Year + detFechaIni.Date.Month.ToString().PadLeft(2, '0') + detFechaIni.Date.Day.ToString().PadLeft(2, '0');
            }
            else
            {
                strFechaInicio = detFechaIni.Date.Year + detFechaIni.Date.Month.ToString().PadLeft(2, '0') + "01000000";
                strFechaFin = detFechaIni.Date.Year + detFechaIni.Date.Month.ToString().PadLeft(2, '0') + "31235959";
                strFechaInicioT = detFechaIni.Date.Year + detFechaIni.Date.Month.ToString().PadLeft(2, '0') + "01";
                strFechaFinT = detFechaIni.Date.Year + detFechaIni.Date.Month.ToString().PadLeft(2, '0') + "31";
            }

            var lstResultadoMovBase = new List<O_CONSULTA_MOVIM_TRAN_CTY>();
            var lstResultadoMovFosil = new List<O_CONSULTA_MOVIM_TRAN_CTY>();
            var lstResultadoMovGasBlanca = new List<O_CONSULTA_MOVIM_TRAN_CTY>();
            var lstResultadoMovOtro = new List<O_CONSULTA_MOVIM_TRAN_CTY>();

            var lstNumeroDui = new List<O_LISTADO_DUI>();

            var lstMovimiento = _consulta.ObtenerMovimiento("",
                new EObjetoEnvio8ParametrosProto
                {
                    decFiltro = 4, //Filtro 1 por producto //2 consulta por infraestructura //4 importadores
                    strParametro1 = tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString(), //Id proyecto/entidad
                    strParametro2 = strFechaInicio,//detFechaIni.Date.Year + detFechaIni.Date.Month.ToString().PadLeft(2, '0') + "01000000",//fecha inicial
                    strParametro3 = strFechaFin,//detFechaIni.Date.Year + detFechaIni.Date.Month.ToString().PadLeft(2, '0') + "31235959", //fecha final
                    strParametro4 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(), //id Actividad
                    strParametro5 = "249", //tipo operacion
                    strParametro6 = "0", //idProducto 
                    strParametro7 = "0", // id_infraestructura
                    strParametro8 = "0" // tipo_infraestructura
                });

            if (lstMovimiento.intCodigo == 1)
            {
                var lstResultado = (List<O_CONSULTA_MOVIM_TRAN_CTY>)lstMovimiento.oResultado;
                if (lstResultado.Any())
                {

                    var registros = lstResultado;
                    Session[CVariablesSesion.GrillaTransaccion] = registros;

                    foreach (var dato in registros)
                    {
                        var numDui = new O_LISTADO_DUI();

                        int intProducto = Convert.ToInt32(dato.ID_HIDROCARBURO);                        
                        switch (intProducto)
                        {
                            case 401://test 401 desarrollo 4326 produccion 1829
                            case 402://test 402 desarrollo 4327 produccion 1830
                            case 403://test 403 desarrollo 4328 produccion 1831
                            case 404://test 404 desarrollo 4329 produccion 1832
                            case 405://test 405 desarrollo 4330 produccion 1833
                                                                
                                break;
                            case 367: //test 367 desarrollo 367 produccion 367
                            case 368: //test 368 desarrollo 368 produccion 368
                            case 369: //test 369 desarrollo 369 produccion 369
                            case 370: //test 370 desarrollo 370 produccion 370
                            case 371: //test 371 desarrollo 371 produccion 371
                            case 372: //test 372 desarrollo 372 produccion 372
                            case 374: //test 374 desarrollo 374 produccion 373
                            case 380: //test 380 desarrollo 380 produccion 374
                            case 382: //test 382 desarrollo 382 produccion 375
                            case 383: //test 383 desarrollo 383 produccion 376
                            case 384: //test 384 desarrollo 384 produccion 377
                            case 385: //test 385 desarrollo 385 produccion 378
                            case 386: //test 386 desarrollo 386 produccion 379
                            case 387: //test 387 desarrollo 387 produccion 380
                            case 388: //test 388 desarrollo 388 produccion 381
                            case 389: //test 389 desarrollo 389 produccion 382

                                string[] lista = dato.OBSERVACION_MOVIMIENTO.Split('|');
                                numDui.DUI = lista[0].ToString();
                                numDui.NRO_DUI= lista[0].ToString();
                                break;
                            case 278:
                                                                
                                break;
                            default:

                         
                                break;
                        }

                        if (numDui.DUI != null)
                        {
                            lstNumeroDui.Add(numDui);
                        }
                        else { 
                            //no implementado
                        }

                    }
                }
            }

            if (decOpcion == 1)
            {
                cmbListadoDui.ClientVisible = true;

                var listaDuiObtenido = (from datoDui in lstNumeroDui
                                group datoDui by datoDui.DUI into datoDuiGroup
                                select new
                                {
                                    DUI = datoDuiGroup.Key,
                                    NRO_DUI = datoDuiGroup.Key,                                    
                                }).ToList();

                cmbListadoDui.DataSource = listaDuiObtenido;
                cmbListadoDui.DataBind();
            }
            else
            {
                cmbListadoDui.ClientVisible = false;
                cmbListadoDui.DataSource = null;
                cmbListadoDui.DataBind();
            }

        }
    }
}