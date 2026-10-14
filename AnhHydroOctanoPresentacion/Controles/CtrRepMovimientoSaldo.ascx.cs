using AnhHydroOctanoPresentacion.Lib;
  
namespace AnhHydroOctanoPresentacion.Controles
{
   using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Web;
    using System.Web.UI;
    using AnhHydroOctanoPersistencia.Aplicacion.Consulta;
    using AnhHydroOctanoPersistencia.Aplicacion.Gestion;
    using AnhHydroOctanoPersistencia.Entidades;

    using Clases.Controles;
    using Clases.Util;
    using Entidades;
    using Enumeradores;
    using Parametros;
    using DevExpress.Web;
    using Librerias.Anh.Us;
    using ServiceStack.ServiceClient.Web;
    using ServiceStack.Text;
    using UserControl = System.Web.UI.UserControl;
    using EResultado = Entidades.EResultado;
    using O_LISTA_TRANSACCION_CTY = Entidades.O_LISTA_TRANSACCION_CTY;
    using Page = System.Web.UI.Page;
    using DevExpress.Utils;
    using System.Web.Script.Serialization;
    using AnhHydroOctanoAgenteServicio.ServicioExternoSegip;
    using AnhHydroOctanoPresentacion;

    public partial class CtrRepMovimientoSaldo : UserControl
    {

        readonly JsonServiceClient _client = new JsonServiceClient(CAppSettings.servicio_octVolumen);
        private readonly CPersistenciaConsulta _consulta = new CPersistenciaConsulta();
         readonly CUtil _cUtil = new CUtil();
         CConsultasParametricas cConsultasParametricas = new CConsultasParametricas();


        protected void Page_Load(object sender, EventArgs e)
        {
          
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    if (!Page.IsCallback && !Page.IsPostBack)
                    {
                        Session.Remove(CVariablesSesion.Aux);
                        Session.Remove(CVariablesSesion.AuxActualiza);
                        Session.Remove(CVariablesSesion.ComboTipoOperacion);
                        Session.Remove(CVariablesSesion.ComboTiposOperacion);
                        Session.Remove(CVariablesSesion.EstructuraArbol);
                        Session.Remove(CVariablesSesion.GrillaAcumulado);
                        Session.Remove(CVariablesSesion.GrillaAuxiliar);
                        Session.Remove(CVariablesSesion.GrillaTitulo);
                        Session.Remove(CVariablesSesion.GrillaTransaccion);
                        Session.Remove(CVariablesSesion.HabilitaFunciones);
                        Session.Remove(CVariablesSesion.IdAuxiliar);
                        Session.Remove(CVariablesSesion.ListaParametrica);
                        Session.Remove(CVariablesSesion.ListaParamGral);
                        Session.Remove(CVariablesSesion.ListaParamInfra);
                        Session.Remove(CVariablesSesion.ListaPeriodos);
                        Session.Remove(CVariablesSesion.ListaTransaccion);
                        Session.Remove(CVariablesSesion.OctanoParametrica);
                        Session.Remove(CVariablesSesion.ParamAux);
                        Session.Remove(CVariablesSesion.StrAuxiliar);
                        Session.Remove(CVariablesSesion.ComboGenerico);

                        MCargaArbol();
                    //    MCargaParametricas();
                        MCargaPermisos();
                        ConfigurarFormulario();
                        // CargarConfig();

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

                    if (tlsOperadores.DataSource == null)
                    {
                        tlsOperadores.DataSource = Session[CVariablesSesion.EstructuraArbol];
                        tlsOperadores.DataBind();
                    }
                    else
                    {
                        //todo No es carga inicial: no implementado
                    }

                    if (dgvAcumulado.DataSource == null && Session[CVariablesSesion.GrillaAcumulado] != null)
                    {
                        dgvAcumulado.DataSource = Session[CVariablesSesion.GrillaAcumulado];
                        dgvAcumulado.DataBind();

                        string[] strTitulos;
                        strTitulos = Session[CVariablesSesion.GrillaTitulo] as string[];

                        for (int i = 0; i < 100; i++)
                        {
                            if (strTitulos[i] != "")
                            {
                                dgvAcumulado.Columns[i].Caption = strTitulos[i];
                            }
                        }
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

        protected void tlsOperadores_CustomDataCallback(object sender, DevExpress.Web.ASPxTreeList.TreeListCustomDataCallbackEventArgs e)
        {
           
        }

        protected void btnExportarAcum_Click(object sender, EventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    dgeExportar.GridViewID = "dgvAcumulado";
                    dgeExportar.WriteXlsxToResponse();

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

        protected void dgvAcumulado_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
        {

        }

        protected void dgvAcumulado_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
        {

        }

        protected void btnImportaSaldo_Click(object sender, EventArgs e)
        {

        }

        protected void pnlTransaccion_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {

        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {

        }

        protected void dgvTransacciones_CustomButtonCallback(object sender, DevExpress.Web.ASPxGridViewCustomButtonCallbackEventArgs e)
        {

        }

        protected void dgvTransacciones_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {

        }

        protected void dgvTransacciones_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {

        }

        protected void dgvTransacciones_HtmlEditFormCreated(object sender, DevExpress.Web.ASPxGridViewEditFormEventArgs e)
        {

        }

        protected void dgvTransacciones_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
        {

        }

        protected void dgvTransacciones_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {

        }

        protected void dgvTransacciones_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {

        }

        protected void dgvTransacciones_CustomButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCustomButtonEventArgs e)
        {

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

            Session[CVariablesSesion.EstructuraArbol] = _cUtil.MObtenerArbolOrdenado(Properties.Settings.Default.intOrdenArbol, objArbol);
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
            //if (tipoMensaje != EnumTiposMensajeAlerta.Correcto)
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError", "alert('" + strMensaje + "');", true);
            lblMensajeError.Text = "   " + strMensaje + "   ";
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
                                                                      "/WFFlujoOperador.aspx?format=json");
                if (lstResultado.decCodigo == 1)
                {
                    if (lstResultado.oResultado != null && lstResultado.oResultado.Any())
                    {
                        Session[CVariablesSesion.PermisoRevi] = lstResultado.oResultado.FirstOrDefault().REVISION;
                    }
                    else
                    {
                        Session[CVariablesSesion.PermisoRevi] = 0;
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
        private void ConfigurarFormulario()
        {
            detFechaIni.Date = DateTime.Parse("01/" + DateTime.Now.Month + "/" + DateTime.Now.Year);
            detFechaFin.Date = DateTime.Now;

          
        }

        protected void pnlDatos_Callback(object sender, CallbackEventArgsBase e)
        {

            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    if (tlsOperadores.FocusedNode == null)
                    {
                        MImprimirMensaje("Seleccione un proyecto.", EnumTiposMensajeAlerta.Error);
                        return;
                    }
                    Session[CVariablesSesion.EntidadSeleccionada] = tlsOperadores.FocusedNode.GetValue("StrUbicacion");

                    lblRazonSocial.Text = tlsOperadores.FocusedNode.GetValue("StrDescPadre").ToString();
                    lblNit.Text = tlsOperadores.FocusedNode.GetValue("LngNit").ToString();
                    lblRepLegal.Text = (tlsOperadores.FocusedNode.GetValue("StrDireccion") == null) ? "" : tlsOperadores.FocusedNode.GetValue("StrDireccion").ToString();
                    lblDepto.Text = tlsOperadores.FocusedNode.GetValue("StrDepartamento").ToString();

                    if (Session[CVariablesSesion.AuxActualiza] != null)
                    {
                        Session.Remove(CVariablesSesion.AuxActualiza);
                        //pnlGrafico_Callback(sender, e);
                        return;
                    }

                  

                    MLimpiaGrilla();
                    if ((int)tlsOperadores.FocusedNode.GetValue("IntAccion") > 0)
                    {
                        var decAct = Convert.ToInt32(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                        if (decAct == 1 || decAct == 2)
                        {
                            pnlHerramienta.ClientVisible = true;
                        }

                        //tipos de operacion
                        var objTipoOperacion = _client.Post<EResultadoTipoOperacion>(CAppSettings.svc_obtener_tipos_operacion_act,
                        new
                        {
                            decFiltro = 3,
                            strParametro1 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
                            strParametro2 = tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString(),
                            strParametro3 = detFechaIni.Date.Year + detFechaIni.Date.Month.ToString().PadLeft(2, '0') + detFechaIni.Date.Day.ToString().PadLeft(2, '0') + "000000",
                            strParametro4 = detFechaFin.Date.Year + detFechaFin.Date.Month.ToString().PadLeft(2, '0') + detFechaFin.Date.Day.ToString().PadLeft(2, '0') + "235959"
                        });
                        if (objTipoOperacion.IntCodigo == 1)
                        {
                            Session[CVariablesSesion.ComboTiposOperacion] = objTipoOperacion.OResultado;
                        }
                        else
                        {
                            MImprimirMensaje(objTipoOperacion.StrMensaje, EnumTiposMensajeAlerta.Error);
                        }

                        if (tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString() != "16")//lmqc
                        {
                            if (tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString() != "36")//lmqc
                            {
                                //carga periodos habilitados
                                var objPeriodo = _client.Post<EResultadoPeriodos>(CAppSettings.svc_lista_periodos_consumidor,
                                    new
                                    {
                                        strCredencial = CParametrosHydro.strCredencialFuncionario,
                                        decFiltro = 2,
                                        strParametro1 = tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString(),
                                        strParametro2 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
                                        strParametro3 = "",
                                        strParametro4 = "",
                                        strParametro5 = tlsOperadores.FocusedNode.GetValue("DecIdDepartamento").ToString()
                                    });
                                if (objPeriodo.IntCodigo == 1)
                                {
                                    Session[CVariablesSesion.ListaPeriodos] = objPeriodo.OResultado;
                                }
                                else
                                {
                                    //todo No implementado
                                }
                            }
                        }
                        var strMensaje = string.Empty;
                        if (MValidaDatosIniciales(ref strMensaje))
                        {
                            var strDato1 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString();
                            var strDato2 = tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString();
                            var strDato3 = detFechaIni.Date.Year + detFechaIni.Date.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + detFechaIni.Date.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0');
                            var strDato4 = detFechaFin.Date.Year + detFechaFin.Date.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + detFechaFin.Date.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0');

                            MCargaParametricasEsp();
                            MCargaGrilla(strDato1, strDato2, strDato3, strDato4, sender);
                        }
                        else
                        {
                            MImprimirMensaje(strMensaje, EnumTiposMensajeAlerta.Error);
                        }
                    }
                    else
                    {
                        //todo no implementado
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
        private void MLimpiaGrilla()
        {
            Session.Remove(CVariablesSesion.GrillaAcumulado);
            dgvAcumulado.DataSource = null;
            dgvAcumulado.DataBind();
            Session.Remove(CVariablesSesion.GrillaTransaccion);
          
            MImprimirMensaje("", EnumTiposMensajeAlerta.Limpiar);
            Session.Remove(CVariablesSesion.ComboGenerico);
          //  cmbFiltro.DataSource = null;
           // cmbFiltro.DataBind();
           // cmbFiltro.Text = "";
            pnlHerramienta.ClientVisible = false;
        }
        private bool MValidaDatosIniciales(ref string strMensaje)
        {
            bool booResultado = true;

            if (detFechaIni.Text == "" || detFechaFin.Text == "")
            {
                strMensaje = "Introduzca fechas validas.";
                booResultado = false;
            }
            else if (detFechaFin.Date < detFechaIni.Date)
            {
                strMensaje = "Fechas incorrectas.";
                booResultado = false;
            }
            else if (detFechaIni.Date.Year + detFechaIni.Date.Month != detFechaFin.Date.Year + detFechaFin.Date.Month)
            {
                strMensaje = "Fechas fuera de intérvalo. Debe escoger fechas dentro del mismo periodo.";
                booResultado = false;
            }
            return booResultado;
        }
        private void MCargaGrilla(string strParametro1, string strParametro2, string strParametro3, string strParametro4, object sender)
        {
            Session.Remove(CVariablesSesion.HabilitaFunciones);
            string strMensaje = string.Empty;
            if (MValidaPermisos(ref strMensaje))
            {
                Session[CVariablesSesion.HabilitaFunciones] = 1;
            }
            else
            {
                MImprimirMensaje(strMensaje, EnumTiposMensajeAlerta.Alerta);
            }

         //   dgvAcumulado.Columns["DecIng01"].Caption = dgvAcumulado.Columns["DecSal01"].VisibleIndex.ToString() + "------" + dgvAcumulado.Columns["DecSal20"].VisibleIndex.ToString();
            
            for (int i = 30; i >= 11; i--)
            {
                dgvAcumulado.Columns[i].Visible = false;
                dgvAcumulado.Columns[21 + i].Visible = false;
            }
            
            var strTitulos = new string[100];
            for (int i = 0; i < 100; i++)
            {
                strTitulos[i] = "";
            }
            dgvAcumulado.Columns[53].Visible = true;


            /*dgvAcumulado.Columns[0].Visible = true;
            dgvAcumulado.Columns[3].Caption = "FECHA";
            dgvAcumulado.Columns[6].Visible = true;
            dgvAcumulado.Columns[5].Visible = true;
            dgvAcumulado.Columns[7].Visible = true;

            dgvAcumulado.Columns[28].Visible = true;
            dgvAcumulado.Columns[49].Visible = true;
            dgvAcumulado.Columns[50].Visible = true; //saldo final
            dgvAcumulado.Columns[51].Visible = false; //importe total

         
          
            if (Convert.ToInt32(strParametro1) == (int)EnumActividades.ImportacionLubricantes)
            {
                //var dgvAcumulado.colu
                dgvAcumulado.DataColumns[4].GroupIndex = 1;
                dgvAcumulado.DataColumns[5].GroupIndex = 2;
                dgvAcumulado.DataColumns[53].GroupIndex = 0;

            }
            else
            {
                dgvAcumulado.DataColumns[53].GroupIndex = -1;
                dgvAcumulado.DataColumns[53].Visible = false;
                dgvAcumulado.DataColumns[4].GroupIndex = 1;
                dgvAcumulado.DataColumns[5].GroupIndex = 2;
            }   
     
            if (Convert.ToInt32(strParametro1) == (int)EnumActividades.DistribucionGasRedes)
            {
                dgvAcumulado.DataColumns[4].GroupIndex = 1;
                dgvAcumulado.DataColumns[5].GroupIndex = 2;
                dgvAcumulado.Columns[3].Width = 170; //fecha
                dgvAcumulado.Columns[7].Visible = false; //saldo inicial

                dgvAcumulado.Columns[49].Visible = false; //total sal
                dgvAcumulado.Columns[50].Visible = false; //saldo final
                dgvAcumulado.Columns[51].Visible = true; //importe total
            }*/

            var lstParam = Session[CVariablesSesion.OctanoParametrica] as List<O_LISTA_OCTANO_PAR_CTY>;
            var lstPar = lstParam.Where(x => x.TIPO_VALOR == 7).ToList();
           
            if (!lstPar.Any())
            {
                MImprimirMensaje("Parametro TIPO DE MANEJO DE SALDOS, No definido.", EnumTiposMensajeAlerta.Error);
                return;
            }
           /* if (lstPar.FirstOrDefault().VALOR_FIN != "2")
            {
                dgvAcumulado.DataColumns[4].GroupIndex = -1;
                dgvAcumulado.DataColumns[5].GroupIndex = -1;
                dgvAcumulado.Columns[5].Visible = false;
                dgvAcumulado.Columns[6].Visible = false;
            }*/
            var decFiltro = 1; // Convert.ToDecimal(lstPar.FirstOrDefault().VALOR_FIN);

            if (strParametro1 == "108")
            {
                decFiltro = 7;
            }
            else if (strParametro1 == "12")
            {
                decFiltro = 7;
            }
            else
            {
                //no implementado
            }

            var lstDespachos = _cUtil.MObtieneMovAcumuladoSaldo(decFiltro, strParametro1, strParametro2, strParametro3, strParametro4, 2, -99999);



            if (lstDespachos.intCodigo == 1)
            {

                var lstResultadoLstAcumulado = (O_DECLARACION_JURADA_SAL)lstDespachos.oResultado;
                Session[CVariablesSesion.DjAcumulado] = lstResultadoLstAcumulado;
                var lstResultado = lstResultadoLstAcumulado.LstAcumulado;

                if (lstResultado.Any())
                {
                    #region acumulado
                    decimal decProd = 0;
                    int cont = 1, intContProd = 1;

                    lstResultado = lstResultado.OrderBy(x => x.DecIdHidrocarburo).ThenBy(x => x.DecFecha).ToList();
                    var lstMovimientos = new List<CMovAcumulado_SAL>();
                    foreach (var lst in lstResultado)
                    {
                        var cDato = new CMovAcumulado_SAL();
                        if (decProd != lst.DecIdHidrocarburo)
                        {
                            decProd = lst.DecIdHidrocarburo;
                            intContProd = 1;
                        }

                        cDato.IntId = cont;
                        cDato.IntContProd = intContProd;
                        cDato.StrFecha = lst.DecFecha.ToString().Substring(6, 2) + "/" + lst.DecFecha.ToString().Substring(4, 2) + "/" + lst.DecFecha.ToString().Substring(0, 4);
                        cDato.IntIdProducto = (int)lst.DecIdHidrocarburo;
                        cDato.StrProducto = lst.StrHidrocarburo;
                        cDato.StrCodProducto = lst.StrCodHidrocarburo;
                        cDato.StrUnidadMedida = lst.StrUnidadMedida;

                        cDato.DecIdTipoInfraestructura = lst.DecIdTipoInfraestructura;
                        cDato.StrTipoInfraestructura = lst.StrTipoInfraestructura ?? "";
                        cDato.DecIdInfraestructura = lst.DecIdInfraestructura;
                        cDato.StrInfraestructura = lst.StrInfraestructuraAnh ?? "";
                        cDato.StrCodigoInfraestructura = lst.StrInfraestructura ?? "";

                        cDato.StrIdGrupoUnico = "" + lst.DecIdHidrocarburo + lst.DecIdTipoInfraestructura + lst.DecIdInfraestructura;
                        //cDato.StrGrupoUnico = lst.StrCodHidrocarburo + " - " + lst.StrTipoInfraestructura + " - " + lst.StrInfraestructura;

                        cDato.DecSaldoInicial = lst.DecSaldoInicial;
                        cDato.DecTotalIng = lst.DecTotalIngreso;
                        cDato.DecSaldoFinal = lst.DecSaldoFinal;
                        cDato.DecTotalSal = lst.DecTotalEgreso;
                        cDato.DecImporteTotal = lst.DecTotalImporte;
                        
                        cDato.StrObservaciones = lst.StrObservaciones;

                        cDato.DecDia = lst.DecDia;
                        cDato.StrDescripcionDia = lst.StrDescripcionDia;
                        cDato.StrMes = lst.StrMes;
                        cDato.StrAnio = lst.StrAnio;
                        cDato.StrDescripcionTanque = lst.StrDescripcionTanque;
                        cDato.DecVolumenNominal = lst.DecVolumenNominal;
                        cDato.DecProgramacion = lst.DecProgramacion;
                        cDato.DecRecepProg = lst.DecRecepProg;
                        cDato.DecVentaProg = lst.DecVentaProg;


                        var vecValores = new decimal[100];
                        vecValores[0] = lst.DecSaldoInicial;
                        vecValores[1] = lst.DecSaldoFinal;
                        vecValores[2] = lst.DecTotalIngreso;
                        vecValores[3] = lst.DecTotalEgreso;


                        if (lst.LstIngresos == null || !lst.LstIngresos.Any())
                        {
                            cDato.DecIng01 = -99999;
                        }
                        else
                        {
                            foreach (var reg in lst.LstIngresos)
                            {
                                if (!dgvAcumulado.Columns[10 + (int)reg.OPERACION_ORDEN].Visible)
                                {
                                    /*if (reg.REPORTE_NOMBRE == "RECEPCION")
                                    {*/
                                        dgvAcumulado.Columns[10 + (int)reg.OPERACION_ORDEN].Visible = true;
                                        dgvAcumulado.Columns[10 + (int)reg.OPERACION_ORDEN].Caption = reg.OPERACION_NOMBRE;
                                        strTitulos[10 + (int)reg.OPERACION_ORDEN] = reg.OPERACION_NOMBRE;
                                    /*}
                                    else {
                                        dgvAcumulado.Columns[10 + (int)reg.OPERACION_ORDEN].Visible = false;
                                    }*/
                                }
                                cDato.DecIng01 = (reg.OPERACION_ORDEN == 1) ? reg.CANTIDAD : cDato.DecIng01;
                                cDato.DecIng02 = (reg.OPERACION_ORDEN == 2) ? reg.CANTIDAD : cDato.DecIng02;
                                cDato.DecIng03 = (reg.OPERACION_ORDEN == 3) ? reg.CANTIDAD : cDato.DecIng03;
                                cDato.DecIng04 = (reg.OPERACION_ORDEN == 4) ? reg.CANTIDAD : cDato.DecIng04;
                                cDato.DecIng05 = (reg.OPERACION_ORDEN == 5) ? reg.CANTIDAD : cDato.DecIng05;
                                cDato.DecIng06 = (reg.OPERACION_ORDEN == 6) ? reg.CANTIDAD : cDato.DecIng06;
                                cDato.DecIng07 = (reg.OPERACION_ORDEN == 7) ? reg.CANTIDAD : cDato.DecIng07;
                                cDato.DecIng08 = (reg.OPERACION_ORDEN == 8) ? reg.CANTIDAD : cDato.DecIng08;
                                cDato.DecIng09 = (reg.OPERACION_ORDEN == 9) ? reg.CANTIDAD : cDato.DecIng09;
                                cDato.DecIng10 = (reg.OPERACION_ORDEN == 10) ? reg.CANTIDAD : cDato.DecIng10;
                                cDato.DecIng11 = (reg.OPERACION_ORDEN == 11) ? reg.CANTIDAD : cDato.DecIng11;
                                cDato.DecIng12 = (reg.OPERACION_ORDEN == 12) ? reg.CANTIDAD : cDato.DecIng12;
                                cDato.DecIng13 = (reg.OPERACION_ORDEN == 13) ? reg.CANTIDAD : cDato.DecIng13;
                                cDato.DecIng14 = (reg.OPERACION_ORDEN == 14) ? reg.CANTIDAD : cDato.DecIng14;
                                cDato.DecIng15 = (reg.OPERACION_ORDEN == 15) ? reg.CANTIDAD : cDato.DecIng15;
                                cDato.DecIng16 = (reg.OPERACION_ORDEN == 16) ? reg.CANTIDAD : cDato.DecIng16;
                                cDato.DecIng17 = (reg.OPERACION_ORDEN == 17) ? reg.CANTIDAD : cDato.DecIng17;
                                cDato.DecIng18 = (reg.OPERACION_ORDEN == 18) ? reg.CANTIDAD : cDato.DecIng18;
                                cDato.DecIng19 = (reg.OPERACION_ORDEN == 19) ? reg.CANTIDAD : cDato.DecIng19;
                                cDato.DecIng20 = (reg.OPERACION_ORDEN == 20) ? reg.CANTIDAD : cDato.DecIng20;

                                vecValores[3 + (int)reg.OPERACION_ORDEN] = reg.CANTIDAD;
                            }
                        }

                        if (lst.LstEgresos == null || !lst.LstEgresos.Any())
                        {
                            cDato.DecSal01 = -99999;
                        }
                        else
                        {
                            foreach (var reg in lst.LstEgresos)
                            {
                                if (!dgvAcumulado.Columns[31 + (int)reg.OPERACION_ORDEN].Visible)
                                {
                                    /*if (reg.REPORTE_NOMBRE == "VENTA")
                                    {*/
                                        dgvAcumulado.Columns[31 + (int)reg.OPERACION_ORDEN].Visible = true;

                                        dgvAcumulado.Columns[31 + (int)reg.OPERACION_ORDEN].Caption = reg.OPERACION_NOMBRE;
                                        strTitulos[31 + (int)reg.OPERACION_ORDEN] = reg.OPERACION_NOMBRE;
                                    /*}
                                    else {
                                        dgvAcumulado.Columns[31 + (int)reg.OPERACION_ORDEN].Visible = false;
                                    }*/
                                }
                                cDato.DecSal01 = (reg.OPERACION_ORDEN == 1) ? reg.CANTIDAD : cDato.DecSal01;
                                cDato.DecSal02 = (reg.OPERACION_ORDEN == 2) ? reg.CANTIDAD : cDato.DecSal02;
                                cDato.DecSal03 = (reg.OPERACION_ORDEN == 3) ? reg.CANTIDAD : cDato.DecSal03;
                                cDato.DecSal04 = (reg.OPERACION_ORDEN == 4) ? reg.CANTIDAD : cDato.DecSal04;
                                cDato.DecSal05 = (reg.OPERACION_ORDEN == 5) ? reg.CANTIDAD : cDato.DecSal05;
                                cDato.DecSal06 = (reg.OPERACION_ORDEN == 6) ? reg.CANTIDAD : cDato.DecSal06;
                                cDato.DecSal07 = (reg.OPERACION_ORDEN == 7) ? reg.CANTIDAD : cDato.DecSal07;
                                cDato.DecSal08 = (reg.OPERACION_ORDEN == 8) ? reg.CANTIDAD : cDato.DecSal08;
                                cDato.DecSal09 = (reg.OPERACION_ORDEN == 9) ? reg.CANTIDAD : cDato.DecSal09;
                                cDato.DecSal10 = (reg.OPERACION_ORDEN == 10) ? reg.CANTIDAD : cDato.DecSal10;
                                cDato.DecSal11 = (reg.OPERACION_ORDEN == 11) ? reg.CANTIDAD : cDato.DecSal11;
                                cDato.DecSal12 = (reg.OPERACION_ORDEN == 12) ? reg.CANTIDAD : cDato.DecSal12;
                                cDato.DecSal13 = (reg.OPERACION_ORDEN == 13) ? reg.CANTIDAD : cDato.DecSal13;
                                cDato.DecSal14 = (reg.OPERACION_ORDEN == 14) ? reg.CANTIDAD : cDato.DecSal14;
                                cDato.DecSal15 = (reg.OPERACION_ORDEN == 15) ? reg.CANTIDAD : cDato.DecSal15;
                                cDato.DecSal16 = (reg.OPERACION_ORDEN == 16) ? reg.CANTIDAD : cDato.DecSal16;
                                cDato.DecSal17 = (reg.OPERACION_ORDEN == 17) ? reg.CANTIDAD : cDato.DecSal17;
                                cDato.DecSal18 = (reg.OPERACION_ORDEN == 18) ? reg.CANTIDAD : cDato.DecSal18;
                                cDato.DecSal19 = (reg.OPERACION_ORDEN == 19) ? reg.CANTIDAD : cDato.DecSal19;
                                cDato.DecSal20 = (reg.OPERACION_ORDEN == 20) ? reg.CANTIDAD : cDato.DecSal20;
                                cDato.DecImporteTotal = reg.PRECIO_TOTAL;
                                
                                                             
                                vecValores[31 + (int)reg.OPERACION_ORDEN] = reg.CANTIDAD;


                            }
                            cDato.VecValores = vecValores;
                        }

                        lstMovimientos.Add(cDato);
                        cont++;
                        intContProd++;
                    }
                   


                /*   if (Convert.ToInt32(strParametro1) == (int)EnumActividades.EstacionServicioGnv)
                    {
                        dgvAcumulado.Columns[50].Visible = false;
                        dgvAcumulado.Columns[28].Visible = false;
                        dgvAcumulado.Columns[7].Visible = false;
                        dgvAcumulado.Columns[0].Visible = false;
                    }
                    else if (Convert.ToInt32(strParametro1) == (int)EnumActividades.ImportacionCarburantes)
                    {
                        dgvAcumulado.Columns[50].Visible = false;
                        dgvAcumulado.Columns[49].Visible = false;
                        dgvAcumulado.Columns[29].Visible = false;
                        dgvAcumulado.Columns[7].Visible = false;
                        dgvAcumulado.Columns[0].Visible = false;
                    }
                    else if (Convert.ToInt32(strParametro1) == (int)EnumActividades.ImportacionLubricantes)
                    {
                        dgvAcumulado.Columns[50].Visible = false;
                        dgvAcumulado.Columns[49].Visible = true;
                        dgvAcumulado.Columns[29].Visible = true;
                        dgvAcumulado.Columns[30].Visible = true;
                        dgvAcumulado.Columns[28].Visible = false;
                        //dgvAcumulado.Columns[29].Visible = false;

                        dgvAcumulado.Columns[7].Visible = false;
                        dgvAcumulado.Columns[0].Visible = false;
                    }
                    else if (Convert.ToInt32(strParametro1) == (int)EnumActividades.TransporteGasDucto)
                    {
                        dgvAcumulado.Columns[50].Visible = false;
                        dgvAcumulado.Columns[7].Visible = false;

                    }
                    else if (Convert.ToInt32(strParametro1) == (int)EnumActividades.TransporteLiquidosDucto)
                    {
                        dgvAcumulado.Columns[50].Visible = false;
                        dgvAcumulado.Columns[7].Visible = false;

                    }
                    else if (Convert.ToInt32(strParametro1) == (int)EnumActividades.DistribucionGasRedes)
                    {
                        dgvAcumulado.Columns[28].Visible = false;
                        dgvAcumulado.Columns[52].Visible = false;
                    }
                    else
                    {

                    }*/

                    Session[CVariablesSesion.GrillaAcumulado] = lstMovimientos;
                    dgvAcumulado.DataSource = lstMovimientos;
                    dgvAcumulado.DataBind();
                    /*
                    ASPxGridView1.DataSource = lstMovimientos;
                    ASPxGridView1.DataBind(); 
                    */
                    Session[CVariablesSesion.GrillaTitulo] = strTitulos;

                    //cDato.StrGrupoUnico = lst.StrCodHidrocarburo + " - " + lst.StrTipoInfraestructura + " - " + lst.StrInfraestructura

                    var lstCombo = lstMovimientos.GroupBy(x => new { x.StrIdGrupoUnico, x.StrProducto, x.StrTipoInfraestructura, x.StrCodigoInfraestructura })
                       .Select(g => new { g.Key.StrIdGrupoUnico, g.Key.StrProducto, g.Key.StrTipoInfraestructura, g.Key.StrCodigoInfraestructura })
                       .OrderBy(d => d.StrProducto).ToList();
                  //  cmbFiltro.DataSource = lstCombo;
                  //  cmbFiltro.DataBind();
                    Session[CVariablesSesion.ComboGenerico] = lstCombo;

                    //grafico General
                    var objRes = new O_GRAFICO_TIPO1();
                    var series = new List<O_SERIES>();

                    var lstMovi = lstMovimientos.OrderBy(x => x.StrProducto).ToList();
                    var lstProd = lstMovimientos.GroupBy(x => new { x.StrProducto })
                       .Select(g => new { g.Key.StrProducto })
                       .OrderBy(d => d.StrProducto).ToList();
                    var names = new string[lstProd.Count()];
                    int intCont = 0;
                    foreach (var prod in lstProd)
                    {
                        names[intCont] = prod.StrProducto;
                        intCont++;
                    }
                    objRes.names = names;

                    for (int i = 0; i < strTitulos.Count(); i++)
                    {
                        if (strTitulos[i] != "")
                        {
                            var objSerie = new O_SERIES();
                            objSerie.name = strTitulos[i];

                            int intAux = 4;
                            if (i > 31)
                                intAux = 5;

                            var strPro = "";
                            decimal decValor = 0;
                            intCont = 0;

                            var arrSerie = new double[lstProd.Count()];

                            foreach (var rMov in lstMovi)
                            {
                                if (strPro != rMov.StrProducto)
                                {
                                    if (strPro != "")
                                    {
                                        arrSerie[intCont] = (double)decValor;
                                        intCont++;
                                    }
                                    decValor = 0;
                                    strPro = rMov.StrProducto;
                                }
                                decValor = decValor + rMov.VecValores[i];
                             //   decValor = decValor + rMov.VecValores[i - intAux];
                            }
                            arrSerie[intCont] = (double)decValor;
                            objSerie.data = arrSerie;
                            objSerie.stack = intAux.ToString();

                            series.Add(objSerie);
                        }
                    }
                    objRes.series = series;

                    var panel = sender as ASPxCallbackPanel;
                    panel.JSProperties["cp_title"] = "Movimiento Acumulado";
                    panel.JSProperties["cp_subtitle"] = "Del: " + detFechaIni.Date.ToShortDateString() + " Al: " + detFechaFin.Date.ToShortDateString();
                    panel.JSProperties["cp_xAxis"] = objRes.names;
                    panel.JSProperties["cp_series"] = objRes.series;

                    #endregion
                }
            }
            else
            {
                MImprimirMensaje(lstDespachos.strMensaje, EnumTiposMensajeAlerta.Error);
            }
        }
        private bool MValidaPermisos(ref string strMesnaje)
        {
            bool booRespuesta = true;

            //Valida si exixte DDJJ en periodo
            var objValidacion = _client.Post<EResultadoGenerico>(CAppSettings.svc_validaciones_general,
            new
            {
                strCredencial = CParametrosHydro.strCredencialFuncionario,
                strFiltro = "1",
                strParametro1 = tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString(),
                strParametro2 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
                strParametro3 = "",
                strParametro4 = detFechaIni.Date.Year + detFechaIni.Date.Month.ToString().PadLeft(2, '0') + "01" + "000000",
                strParametro5 = detFechaFin.Date.Year + detFechaFin.Date.Month.ToString().PadLeft(2, '0') + DateTime.DaysInMonth(detFechaFin.Date.Year, detFechaFin.Date.Month).ToString().PadLeft(2, '0') + "235959",
                strParametro6 = "",
                decAppFechaRegistro = 0,
                decAppIdUsuario = 0
            });
            if (objValidacion.IntCodigo == 1)
            {
                if (objValidacion.OResultado.RESULTADO == 1)
                {
                    if (tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString() != "16")
                    {
                        if (tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString() != "36")
                        {
                            //Valida si esta en periodo
                            var lstPeriodos = Session[CVariablesSesion.ListaPeriodos] as List<O_PERIODO_CTY>;
                            if (lstPeriodos.Any())
                            {
                                var lstValida = lstPeriodos.Where(x => x.decAño == detFechaIni.Date.Year && x.decMes == detFechaIni.Date.Month).ToList();
                                if (!lstValida.Any())
                                {
                                    booRespuesta = false;
                                    strMesnaje = "Pediodo Cerrado.";
                                }
                            }
                            else
                            {
                                booRespuesta = false;
                                strMesnaje = "Pediodo Cerrado.";
                            }
                        }
                    }

                }
                else
                {
                    booRespuesta = false;
                    strMesnaje = objValidacion.OResultado.MENSAJE_ERROR;
                }
            }
            else
            {
                booRespuesta = false;
                strMesnaje = objValidacion.StrMensaje;
            }

            return booRespuesta;
        }

        private void MCargaParametricasEsp()
        {
            //PARAMETRICAS GENERALES


            var objParametros = _client.Post<EResultadoOctanoParametro>(CAppSettings.svc_listar_octano_parametrica,
            new
            {
                strCredencial = CParametrosHydro.strCredencialFuncionario,
                decFiltro = 1,//1: ID_TIPO_ACTIVIDAD 2: ID_TIPO_ACTIVIDAD y VALOR_TIPO
                strParametro1 = "",//VALOR_TIPO
                strParametro2 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
                strParametro3 = "",
                strParametro4 = ""
            });
            if (objParametros.IntCodigo == 1)
            {
                Session[CVariablesSesion.OctanoParametrica] = objParametros.OResultado;
            }
            else
            {
                Session.Remove(CVariablesSesion.OctanoParametrica);
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





    }
}