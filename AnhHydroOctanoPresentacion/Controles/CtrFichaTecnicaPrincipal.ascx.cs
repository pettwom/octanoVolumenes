
using AnhHydroOctanoPresentacion.Lib;
using ServiceStack.Text;
using DevExpress.XtraCharts.Native;
using DevExpress.XtraPivotGrid;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using DevExpress.XtraCharts;

namespace AnhHydroOctanoPresentacion.Controles
{
    using iTextSharp.text;
    using System.IO;
    using iTextSharp.text.html.simpleparser;
    using iTextSharp.text.pdf;
    using System.Web.UI.DataVisualization.Charting;  

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
    using DevExpress.XtraCharts;
    using DevExpress.XtraCharts.Native;
    using DevExpress.XtraPivotGrid;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrintingLinks;
    using System.IO;
    using DevExpress.XtraRichEdit.API.Native;
    using DevExpress.XtraGrid;
    using System.ComponentModel;


    using System;
    using System.IO;
    using System.Linq;
    using DevExpress.XtraRichEdit;
    using System.Net;
    using System.Globalization;
    
   
    public partial class CtrFichaTecnicaPrincipal : UserControl
    {
        #region Variables de entorno

        readonly JsonServiceClient _clientHidro = new JsonServiceClient(CAppSettings.servicio_sireHidro);
        readonly JsonServiceClient _clientI = new JsonServiceClient(CAppSettings.servicio_octInfraestructura);
        readonly JsonServiceClient _client = new JsonServiceClient(CAppSettings.servicio_octVolumen);
        private readonly CPersistenciaConsulta _consulta = new CPersistenciaConsulta();
        readonly CUtil _cUtil = new CUtil();        
        CConsultasParametricas cConsultasParametricas = new CConsultasParametricas();

        DateTime _detFechaIni, _detFechaFin;
        int[] gestiones;
        
        #endregion

        #region Eventos de Formulario
        private CLogs _logs;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {

                    #region Proceso a Ejecutar

                    _detFechaIni = new DateTime(2018, 1, 1, 0, 0, 0);
                    _detFechaFin = DateTime.Now;
                    gestiones = new int[(_detFechaFin.Date.Year - _detFechaIni.Date.Year) + 1];
                    int  c = 0;
                    for (int i = _detFechaIni.Date.Year; i <= _detFechaFin.Date.Year; i++)
                    {
                        gestiones[c] = i;
                        c++;
                    } 

                    if (!Page.IsCallback && !Page.IsPostBack)
                    {
                        Session.Remove(CVariablesSesion.AuxValidaLlave);
                        Session.Remove(CVariablesSesion.EstructuraArbol);
                        Session.Remove(CVariablesSesion.GrillaTransaccion);
                        Session.Remove(CVariablesSesion.ObtenerEntidades);
                        
                        Session.Remove(CVariablesSesion.PermisoCons);

                        MCargaArbol();
                        tlsOperadores.DataSource = Session[CVariablesSesion.EstructuraArbol];
                        tlsOperadores.DataBind();
                        Session[CVariablesSesion.AuxValidaLlave] = 0;
                        
                        MCargaPermisos();
                        ConfigurarFormulario();
                        //detFechaIni.Date = new DateTime(2018, 1, 1, 0, 0, 0);
                        //detFechaFin.Date = DateTime.Now;                                                                                                 
                    }
                    else
                    {
                       
                         var sw1 = 1;
                        pvgAcumulado.DataSource = Session[CVariablesSesion.GrillaCabecera];
                        pvgAcumulado.DataBind();
                        pvgAcumuladoDO.DataSource = Session["GrillaCabeceraDO"];
                        pvgAcumuladoDO.DataBind();

                        var lstAcumulado1 = (List<O_LISTADO_DDJJ_CTY>)Session[CVariablesSesion.GrillaCabecera];
                        var lstAcumulado2 = (List<O_LISTADO_DDJJ_CTY>)Session["GrillaCabeceraDO"];
                        
                        dgvHydro.DataSource = Session["datosHydro"];
                        dgvHydro.DataBind();


                      
                        //using (WebClient wc = new WebClient())
                        //{
                        //    var url = "https://maps.googleapis.com/maps/api/staticmap?center=-17.2440708207347,-67.9053937643766&zoom=12&size=600x400&key=AIzaSyD3AkEndc2Z3UA3HycG0CPLySrLJnCoE9s";
                        //    wc.DownloadFile(url, @"C:/Users/hherrera.ANH/Pictures/MOCKUP/mapa1.png");
                        //}
                        //todo No implementado
                    }                   
                    //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Tiene actas postback.');", true);
                    //ScriptManager.RegisterStartupScript(this, GetType(), "MensajeAdvertencia", "MensajeAdvertencia('Tiene actas postback.');", true);
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
        public class Entry
        {
            public Entry() { }
            public Entry(string name, string id, string ids,string Dato, string Dato1)
            {
                Name = name; ID = id; IDs = ids; DATO = Dato; DATO1 = Dato1;
            }
            public string Name { get; set; }
            public string ID { get; set; }
            public string IDs { get; set; }
            public string DATO { get; set; }
            public string DATO1 { get; set; }
        }
        public BindingList<Entry> SampleDS()
        {
            BindingList<Entry> ds = new BindingList<Entry>();
            
            return ds;
        }
        protected void pnlDatos_Callback(object sender, CallbackEventArgsBase e)
        {
            if (MValidaSesion())
            {

                try
                {
                    
                    #region Proceso a Ejecutar
                   
                    MLimpiaGrilla();
                    
                    var intDato1 = (int)tlsOperadores.FocusedNode.GetValue("IntIdActividad");
                    var strDato2 = tlsOperadores.FocusedNode.GetValue("StrDepartamento").ToString();
                    var intDato3 = (int)tlsOperadores.FocusedNode.GetValue("IntIdOperador");
                    var intDato4 = (int)tlsOperadores.FocusedNode.GetValue("IntAccion");
                    //Fecha Dinamica//                    
                    //var strFechaIni = new DateTime(2018, 1, 1, 0, 0, 0);
                    //var strFechaFin = DateTime.Now;
                    var strFechaIni = _detFechaIni;
                    var strFechaFin = _detFechaFin;
                    
                    var panel = sender as ASPxCallbackPanel;
                    panel.JSProperties["cp_Alerta"] = ""; //L.l

                    MCargaGrilla(intDato1, strDato2, intDato3, intDato4, sender);
               
                    if (strDato2 != "")
                    {
                        var strIdConsumidor = tlsOperadores.FocusedNode.GetValue("IntIdOperador");
                        var objResultado = _consulta.ListadoDdjjGrafico(
                        new EObjetoEnvio5ParametrosProto
                        {
                            strCredencial = CParametrosHydro.strCredencialFuncionario,
                            decFiltro = 1, //Filtro 1
                            strParametro1 = strFechaIni.Date.Year + strFechaIni.Date.Month.ToString().PadLeft(2, '0') + strFechaIni.Date.Day.ToString().PadLeft(2, '0') + "000000", //"20180101000000",
                            strParametro2 = strFechaFin.Date.Year + strFechaFin.Date.Month.ToString().PadLeft(2, '0') + strFechaFin.Date.Day.ToString().PadLeft(2, '0') + "235959", //strFechaFin + "235959",//strFechaFin,
                            strParametro3 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(), //"1",
                            strParametro4 = tlsOperadores.FocusedNode.GetValue("DecIdDepartamento").ToString(),
                            strParametro5 = "5|" + strIdConsumidor //cmbTipoReporte.Value.ToString()
                        });

                        if (objResultado.intCodigo == 1)
                        {
                            pvgAcumulado.Visible = true;
                            var lstAcumulado = (List<O_LISTADO_DDJJ_CTY>)objResultado.oResultado;
                            pvgAcumulado.DataSource = lstAcumulado;
                            pvgAcumulado.DataBind();
                            Session[CVariablesSesion.GrillaCabecera] = lstAcumulado;
                            MImprimirMensaje(objResultado.strMensaje, EnumTiposMensajeAlerta.Correcto);
                        }
                        else
                        {
                            MImprimirMensaje(objResultado.strMensaje, EnumTiposMensajeAlerta.Alerta);
                            Session.Remove(CVariablesSesion.GrillaCabecera);
                            pvgAcumulado.DataSource = null;
                            pvgAcumulado.DataBind();
                        }

                        ////////////////////////////////////////////////////////
                        var objResultadoDO = _consulta.ListadoDdjjGrafico(
                        new EObjetoEnvio5ParametrosProto
                        {
                            strCredencial = CParametrosHydro.strCredencialFuncionario,
                            decFiltro = 1, //Filtro 1
                            strParametro1 = strFechaIni.Date.Year + strFechaIni.Date.Month.ToString().PadLeft(2, '0') + strFechaIni.Date.Day.ToString().PadLeft(2, '0') + "000000",//"20180101000000",
                            strParametro2 = strFechaFin.Date.Year + strFechaFin.Date.Month.ToString().PadLeft(2, '0') + strFechaFin.Date.Day.ToString().PadLeft(2, '0') + "235959",//strFechaFin + "235959",//strFechaFin,
                            strParametro3 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(), //Actividad
                            strParametro4 = tlsOperadores.FocusedNode.GetValue("DecIdDepartamento").ToString(),
                            strParametro5 = "6|" + strIdConsumidor//cmbTipoReporte.Value.ToString()
                        });

                        if (objResultadoDO.intCodigo == 1)
                        {
                            pvgAcumuladoDO.Visible = true;
                            var lstAcumuladoDO = (List<O_LISTADO_DDJJ_CTY>)objResultadoDO.oResultado;
                            pvgAcumuladoDO.DataSource = lstAcumuladoDO;
                            pvgAcumuladoDO.DataBind();
                            Session["GrillaCabeceraDO"] = lstAcumuladoDO;
                            MImprimirMensaje(objResultado.strMensaje, EnumTiposMensajeAlerta.Correcto);
                        }
                        else
                        {
                            Session.Remove("GrillaCabeceraDO");
                            pvgAcumuladoDO.DataSource = null;
                            pvgAcumuladoDO.DataBind();
                            // MImprimirMensaje(objResultadoDO.strMensaje, EnumTiposMensajeAlerta.Alerta);
                        }
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
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(System.Web.UI.Page), "redirect", script, true);
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
                    var panel1 = sender as ASPxCallbackPanel;
                     panel1.JSProperties["cp_visible"] = "NOok";                            
                    break;
                case 2:
                    lstEntidad = lstEntidad.Where(a => a.ID_ACTIVIDAD == intIdActividad && a.DEPARTAMENTO == strDepto).ToList();
                    var panel2 = sender as ASPxCallbackPanel;
                     panel2.JSProperties["cp_visible"] = "Nook";
                    break;
                default:
                    lstEntidad = lstEntidad.Where(a => a.ID_ACTIVIDAD == intIdActividad && a.DEPARTAMENTO == strDepto && a.ID_CONSUMIDOR == intIdOperador).ToList();   
                     var panel = sender as ASPxCallbackPanel;
                     panel.JSProperties["cp_visible"] = "ok";
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

            var intIdEntidad=lstEntidad[0].ID_ENTIDAD;
            var intIdActividad =lstEntidad[0].ID_ACTIVIDAD;
            var intTipoInfra = 90;// LADO DEL DISPENSER 90;DISPENSER EESS 89; Mangera 91
            var objInfraestructuraGeDis = _clientI.Get<EResultadoInfraestructura>(CAppSettings.svc_ListadoInfraestructuraVol + "/" + CParametrosHydro.strCredencialInfra + "/" + intIdEntidad + "/" + 1 + "/" + intTipoInfra + "?format=json");
            var objInfraestructuraGeMan = _clientI.Get<EResultadoInfraestructura>(CAppSettings.svc_ListadoInfraestructuraVol + "/" + CParametrosHydro.strCredencialInfra + "/" + intIdEntidad + "/" + 1 + "/" + 91 + "?format=json");

            string strDenominacion = "";
            string strDepartamento = "";
            string strMunicipio = "";
            string strTelefono = "";
            string strLicencia = "";
            string strVigencia = "";
            string strDO = "";
            string strGE = "";
            string strAlmacenaje = "";
            string strDispenser = "";
            string strMangueras = "";
            string strLatitud = "";
            string strLongitud = "";
            string strVentaPromedioDO = "";
            string strVentaPromedioGE = "";
            string strProductos = "";
           
            if (objInfraestructuraGeDis == null)
            {
                strDispenser = "";
            }
            else
            {
                //var result = list.Where(x => x.name == "Robert").Select(x => { x.age = 18; return x; });

                int a = 5, b = 5, c = 0; ;                
                foreach (var item in objInfraestructuraGeDis.oResultado)
                {
                    string auxVar = objInfraestructuraGeDis.oResultado[0].CODIGO_ANH.Substring(a, b);
                    if (item.CODIGO_ANH.Substring(a, b) != auxVar)
                    {
                        c = c + 1;                       
                    }                   
                }
                strDispenser = c.ToString();                
                //var  auxlistado = objInfraestructuraGeDis.oResultado.Select(x => { x.CODIGO_ANH = x.CODIGO_ANH.Substring(5,5); return x; }).Distinct().ToList();   //ForEach(x => x.CODIGO_ANH = x.CODIGO_ANH.Substring(0, 5));                     
                //strDispenser = objInfraestructuraGeDis.oResultado.Where(x =>x.CODIGO_ANH.Substring(5, 5) != x.CODIGO_ANH.Substring(5, 5)).ToList().Count().ToString();
                //var sdd =  (from item in objInfraestructuraGeDis.oResultado
                //            where item.CODIGO_ANH.Substring(5, 5) == item.CODIGO_ANH.Substring(5, 5)
                //            select item.CODIGO_ANH.Substring(5, 5)).Count();
            }
            if (objInfraestructuraGeMan == null)
            {
                strDispenser = "";
            }
            else
            {
                strMangueras = objInfraestructuraGeMan.oResultado.Count.ToString();
            }
            
            if (tlsOperadores.FocusedNode == null) return;

            if (lstEntidad == null)
            {
                strVigencia = "";
                strLatitud = "";
                strLongitud = "";
                strProductos= "";
            }
            else
            {
                strDenominacion = lstEntidad[0].DENOMINACION;
                strDepartamento = lstEntidad[0].DEPARTAMENTO;
                strMunicipio = lstEntidad[0].MUNICIPIO;
                strTelefono = lstEntidad[0].TELEFONOS;
                strLicencia = lstEntidad[0].NRO_LICENCIA;
                strVigencia = lstEntidad[0].LIC_VENCIMIENTO;
                strLatitud = lstEntidad[0].LATITUD.ToString();
                strLongitud = lstEntidad[0].LONGITUD.ToString();
                strProductos = lstEntidad[0].LIC_PROD.ToString();

            }

            
            decimal decSumGE = 0, decSumDO = 0;
            decimal decGestionActual= Convert.ToDecimal(DateTime.Now.Year);
            decimal decMesActual = Convert.ToDecimal(DateTime.Now.Month);
            if (Session[CVariablesSesion.GrillaCabecera] != null)
            {
                foreach (var itemGE in ((List<O_LISTADO_DDJJ_CTY>)Session[CVariablesSesion.GrillaCabecera]).OrderBy(x => x.strMes).ToList())
                {
                    if (itemGE.strGestion == decGestionActual)
                    {
                        decSumGE = decSumGE + itemGE.decCantidad;
                    }
                }
            }

            if (Session["GrillaCabeceraDO"] != null)
            {
                foreach (var itemDO in ((List<O_LISTADO_DDJJ_CTY>)Session["GrillaCabeceraDO"]).OrderBy(x => x.strMes).ToList())
                {
                    if (itemDO.strGestion == decGestionActual)
                    {
                        decSumDO = decSumDO + itemDO.decCantidad;
                    }
                }
            }
                                        

            BindingList<Entry> ds = new BindingList<Entry>();
            ds.Add(new Entry(strDenominacion, "", "", "", ""));
            ds.Add(new Entry("DEPARTAMENTO", strDepartamento, "", "", ""));
            ds.Add(new Entry("MUNICIPIO", strMunicipio, "", "", ""));
            ds.Add(new Entry("TELÉFONO", strTelefono, "", "", ""));
            ds.Add(new Entry("LICENCIA", strLicencia, "", "", ""));
            ds.Add(new Entry("VIGENCIA LO", strVigencia, "", "", ""));
            ds.Add(new Entry("PRODUCTOS", strProductos, "", "", ""));
            ds.Add(new Entry("ALMACENAJE (M3)", "", "", "", ""));
            ds.Add(new Entry("SURTIDORES", "Dispenser", strDispenser, "Mangueras", strMangueras));
            ds.Add(new Entry("GEOREFERENCIACIÓN", "Latitud", strLatitud, "Longitud", strLongitud));
            ds.Add(new Entry("VENTA PROMEDIO 2022(m3/mes)", Math.Round(decSumGE / decMesActual, 2) + " GE", Math.Round(decSumDO / decMesActual, 2) + " DO", "", ""));
            ds.AllowNew = true;

            dgvHydro.DataSource = ds;
            dgvHydro.DataBind();
            Session["datosHydro"] = ds;
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

            Page.Response.End();
        }

        /*protected void btnExport_Click(object sender, EventArgs e)
        {
            
            BindingList<Entry> ds = new BindingList<Entry>();
            ds = (BindingList<Entry>)Session["datosHydro"];
            var longitud = ds[9].DATO1;
            var latitud = ds[9].IDs;
            var denominacion = ds[0].Name;
            var departamento = ds[1].ID;
            var municipio = ds[2].ID;
            var telefono = ds[3].ID;
            var licencia = ds[4].ID;
            var vigencia = ds[5].ID;
            var productos = ds[6].ID;
            var dispenser = ds[8].IDs;
            var manguera = ds[8].DATO1;
            //string pathToFiles = Server.MapPath("~/Sitio/Reportes/cabecera.png");

            string pathToFiles = "https://ppa6vrsatcop.anh.gob.bo:9643/M201O8012V4/Sitio/Reportes/cabecera.png";
            //string pathToFiles = "https://ppa1vrsatcoti.anh.gob.bo:8143/a39992015/Sitio/Reportes/cabecera.png";
           // string pathToFiles = "http://localhost:1070/Sitio/Reportes/cabecera.png";
            iTextSharp.text.Image cab = iTextSharp.text.Image.GetInstance(pathToFiles);
           cab.ScaleToFit(480, 260F);

            //Imagen - Esquina inferior izquierda
            //cab.SetAbsolutePosition(10, 650);

            iTextSharp.text.Document Doc = new iTextSharp.text.Document(PageSize.LETTER, 2f, 2f, 40f, 10f);
            PdfWriter.GetInstance(Doc, Response.OutputStream);
            var temppath = System.IO.Path.GetTempFileName();

            var output = new FileStream(temppath, FileMode.Create);
            //Creo el Writer.
            var writer = PdfWriter.GetInstance(Doc, output);

            Doc.Open();
            //Doc.Add(cab);
            MemoryStream memoryStream = new MemoryStream();
            MemoryStream memoryStreamDO = new MemoryStream();

            iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance("https://maps.googleapis.com/maps/api/staticmap?center=" + latitud + "," + longitud + "&zoom=17&size=500x480&markers=color:red%7Clabel:E%7C" + latitud + "," + longitud + "&key=AIzaSyD3AkEndc2Z3UA3HycG0CPLySrLJnCoE9s");
            imagen.ScalePercent(50f);
            Doc.Add(imagen);

            var lstAcumulado1 = (List<O_LISTADO_DDJJ_CTY>)Session[CVariablesSesion.GrillaCabecera];
            var lstAcumulado2 = (List<O_LISTADO_DDJJ_CTY>)Session["GrillaCabeceraDO"];
            iTextSharp.text.Image img2 = null;
            iTextSharp.text.Image img = null;
           
            if (lstAcumulado1 != null && lstAcumulado1.Count > 0)
            {
                Chart1.Series.Add(new System.Web.UI.DataVisualization.Charting.Series("2018"));
                Chart1.Series.Add(new System.Web.UI.DataVisualization.Charting.Series("2019"));
                Chart1.Series.Add(new System.Web.UI.DataVisualization.Charting.Series("2020"));
                Chart1.Series.Add(new System.Web.UI.DataVisualization.Charting.Series("2021"));
                Chart1.Series.Add(new System.Web.UI.DataVisualization.Charting.Series("2022"));
                foreach (var reg in lstAcumulado1.OrderBy(x => x.strMes).ToList())
                {
                    //if (reg.strGestion.ToString() == "2021" && sw1==1)
                    //{
                    //    //Add Series to the Chart.
                    //    Chart1.Series.Add(new System.Web.UI.DataVisualization.Charting.Series("2021"));
                    //    sw1 = 0;
                    //}
                    Chart1.Series[reg.strGestion.ToString()].IsValueShownAsLabel = true;
                    Chart1.Series[reg.strGestion.ToString()].ChartType = SeriesChartType.Column;
                    Chart1.Series[reg.strGestion.ToString()].Points.AddXY(reg.strMes, reg.decCantidad);
                }
                Chart1.SaveImage(memoryStream, ChartImageFormat.Png);
                img = iTextSharp.text.Image.GetInstance(memoryStream.GetBuffer());
                img.ScalePercent(50f);
            }
            else
            {
                Chart1.DataSource = null;
                Chart1.DataBind();
            }

            if (lstAcumulado2 != null && lstAcumulado2.Count > 0)
            {
                Chart2.Series.Add(new System.Web.UI.DataVisualization.Charting.Series("2018"));
                Chart2.Series.Add(new System.Web.UI.DataVisualization.Charting.Series("2019"));
                Chart2.Series.Add(new System.Web.UI.DataVisualization.Charting.Series("2020"));
                Chart2.Series.Add(new System.Web.UI.DataVisualization.Charting.Series("2021"));
                Chart2.Series.Add(new System.Web.UI.DataVisualization.Charting.Series("2022"));
                foreach (var reg2 in lstAcumulado2.OrderBy(x => x.strMes).ToList())
                {
                    //if (reg2.strGestion.ToString() == "2021" && sw1 == 1)
                    //{
                    //    //Add Series to the Chart.
                    //    Chart2.Series.Add(new System.Web.UI.DataVisualization.Charting.Series("2021"));
                    //    sw1 = 0;
                    //}
                    Chart2.Series[reg2.strGestion.ToString()].IsValueShownAsLabel = true;
                    Chart2.Series[reg2.strGestion.ToString()].ChartType = SeriesChartType.Column;
                    Chart2.Series[reg2.strGestion.ToString()].Points.AddXY(reg2.strMes, reg2.decCantidad);
                }
               // Chart2.Titles[0].Text="fff";
                Chart2.SaveImage(memoryStreamDO, ChartImageFormat.Png);
                img2 = iTextSharp.text.Image.GetInstance(memoryStreamDO.GetBuffer());
                img2.ScalePercent(50f);
            }
            else
            {
                Chart2.DataSource = null;
                Chart2.DataBind();
            }







            

          

            //Doc.Add(img);


            PdfPTable tablaPrincipal = new PdfPTable(2);
            var widthsP = new[] { 115f, 105f };

            tablaPrincipal.SetWidths(widthsP);
            PdfPCell cell1;
            PdfPCell cell2;
            PdfPCell cell12;
            cell1 = new PdfPCell();
            cell1.Colspan =2;
            cell1.AddElement(cab);
            
            tablaPrincipal.AddCell(cell1);

            cell1 = new PdfPCell();
            //cell1.AddElement(imagen);
           // tablaPrincipal.AddCell(cell1);


            PdfPTable tablaColumna1 = new PdfPTable(5);
            var widths = new[] { 90f, 45f, 65f, 48f, 65f };

            tablaColumna1.SetWidths(widths);
            var datosGenerales = new iTextSharp.text.Paragraph();
            datosGenerales.Font.SetStyle(iTextSharp.text.Font.BOLD);
            datosGenerales.Font = FontFactory.GetFont("Arial", 10);
            datosGenerales.Alignment = Element.ALIGN_CENTER;
            datosGenerales.Add("DATOS GENERALES");
            cell2 = new PdfPCell();

            cell2.Colspan = 5;
            cell2.AddElement(datosGenerales);
            //cell2.FixedHeight = 14f;
            tablaColumna1.AddCell(cell2);

            var nombreOperador = new iTextSharp.text.Paragraph();
            nombreOperador.Font.SetStyle(iTextSharp.text.Font.BOLD);
            nombreOperador.Font = FontFactory.GetFont("Arial", 9);
            nombreOperador.Alignment = Element.ALIGN_CENTER;
            nombreOperador.Add(denominacion);
            cell2 = new PdfPCell();

            cell2.Colspan = 5;
            cell2.AddElement(nombreOperador);
            tablaColumna1.AddCell(cell2);

            var departamentoOperador = new iTextSharp.text.Paragraph();
            departamentoOperador.Font.SetStyle(iTextSharp.text.Font.BOLD);
            departamentoOperador.Font = FontFactory.GetFont("Arial", 7);
            departamentoOperador.Alignment = Element.ALIGN_JUSTIFIED;
            departamentoOperador.Add("DEPARTAMENTO");
            cell2 = new PdfPCell();
            
            cell2.Colspan = 1;
            cell2.AddElement(departamentoOperador);
            tablaColumna1.AddCell(cell2);

            var varDepartamentoOperador = new iTextSharp.text.Paragraph();
            varDepartamentoOperador.Font = FontFactory.GetFont("Arial", 7);
            varDepartamentoOperador.Alignment = Element.ALIGN_CENTER;
            varDepartamentoOperador.Add(departamento);
            cell2 = new PdfPCell();
            
            cell2.Colspan = 4;
            cell2.AddElement(varDepartamentoOperador);
            tablaColumna1.AddCell(cell2);

            var municipioOperador = new iTextSharp.text.Paragraph();
            municipioOperador.Font.SetStyle(iTextSharp.text.Font.BOLD);
            municipioOperador.Font = FontFactory.GetFont("Arial", 7);
            municipioOperador.Alignment = Element.ALIGN_LEFT;
            municipioOperador.Add("MUNICIPIO");
            cell2 = new PdfPCell();

            cell2.Colspan = 1;
            cell2.AddElement(municipioOperador);
            tablaColumna1.AddCell(cell2);

            var varMunicipioOperador = new iTextSharp.text.Paragraph();
            varMunicipioOperador.Font = FontFactory.GetFont("Arial", 7);
            varMunicipioOperador.Alignment = Element.ALIGN_CENTER;
            varMunicipioOperador.Add(municipio);
            cell2 = new PdfPCell();

            cell2.Colspan = 4;
            cell2.AddElement(varMunicipioOperador);
            tablaColumna1.AddCell(cell2);

            var telefonoOperador = new iTextSharp.text.Paragraph();
            telefonoOperador.Font.SetStyle(iTextSharp.text.Font.BOLD);
            telefonoOperador.Font = FontFactory.GetFont("Arial", 7);
            telefonoOperador.Alignment = Element.ALIGN_LEFT;
            telefonoOperador.Add("TELÉFONO");
            cell2 = new PdfPCell();

            cell2.Colspan = 1;
            cell2.AddElement(telefonoOperador);
            tablaColumna1.AddCell(cell2);

            var varTelefonoOperador = new iTextSharp.text.Paragraph();
            varTelefonoOperador.Font = FontFactory.GetFont("Arial", 7);
            varTelefonoOperador.Alignment = Element.ALIGN_CENTER;
            varTelefonoOperador.Add(telefono);
            cell2 = new PdfPCell();

            cell2.Colspan = 4;
            cell2.AddElement(varTelefonoOperador);
            tablaColumna1.AddCell(cell2);

            var licenciaOperador = new iTextSharp.text.Paragraph();
            licenciaOperador.Font.SetStyle(iTextSharp.text.Font.BOLD);
            licenciaOperador.Font = FontFactory.GetFont("Arial", 7);
            licenciaOperador.Alignment = Element.ALIGN_LEFT;
            licenciaOperador.Add("LICENCIA");
            cell2 = new PdfPCell();
            cell2.Colspan = 1;
            cell2.AddElement(licenciaOperador);
            tablaColumna1.AddCell(cell2);

            var varLicenciaOperador = new iTextSharp.text.Paragraph();
            varLicenciaOperador.Font = FontFactory.GetFont("Arial", 7);
            varLicenciaOperador.Alignment = Element.ALIGN_CENTER;
            varLicenciaOperador.Add(licencia);
            cell2 = new PdfPCell();

            cell2.Colspan = 4;
            cell2.AddElement(varLicenciaOperador);
            tablaColumna1.AddCell(cell2);

            var vigenciaOperador = new iTextSharp.text.Paragraph();
            vigenciaOperador.Font.SetStyle(iTextSharp.text.Font.BOLD);
            vigenciaOperador.Font = FontFactory.GetFont("Arial", 7);
            vigenciaOperador.Alignment = Element.ALIGN_LEFT;
            vigenciaOperador.Add("VIGENCIA L.O.");
            cell2 = new PdfPCell();

            cell2.Colspan = 1;
            cell2.AddElement(vigenciaOperador);
            tablaColumna1.AddCell(cell2);

            var varVigenciaOperador = new iTextSharp.text.Paragraph();
            varVigenciaOperador.Font = FontFactory.GetFont("Arial", 7);
            varVigenciaOperador.Alignment = Element.ALIGN_CENTER;
            varVigenciaOperador.Add(vigencia);
            cell2 = new PdfPCell();

            cell2.Colspan = 4;
            cell2.AddElement(varVigenciaOperador);
            tablaColumna1.AddCell(cell2);

            var productosOperador = new iTextSharp.text.Paragraph();
            productosOperador.Font.SetStyle(iTextSharp.text.Font.BOLD);
            productosOperador.Font = FontFactory.GetFont("Arial", 7);
            productosOperador.Alignment = Element.ALIGN_LEFT;
            productosOperador.Add("PRODUCTOS");
            cell2 = new PdfPCell();

            cell2.Colspan = 1;
            cell2.AddElement(productosOperador);
            tablaColumna1.AddCell(cell2);

            var varProductosDOOperador = new iTextSharp.text.Paragraph();
            varProductosDOOperador.Font.SetStyle(iTextSharp.text.Font.BOLD);
            varProductosDOOperador.Font = FontFactory.GetFont("Arial", 7);
            varProductosDOOperador.Alignment = Element.ALIGN_CENTER;
            varProductosDOOperador.Add(productos);
            cell2 = new PdfPCell();

            cell2.Colspan = 2;
            cell2.AddElement(varProductosDOOperador);
            tablaColumna1.AddCell(cell2);

            var varProductosGEOperador = new iTextSharp.text.Paragraph();
            varProductosGEOperador.Font.SetStyle(iTextSharp.text.Font.BOLD);
            varProductosGEOperador.Font = FontFactory.GetFont("Arial", 7);
            varProductosGEOperador.Alignment = Element.ALIGN_CENTER;
            varProductosGEOperador.Add("");
            cell2 = new PdfPCell();

            cell2.Colspan = 2;
            cell2.AddElement(varProductosGEOperador);
            tablaColumna1.AddCell(cell2);

            var almacenajeOperador = new iTextSharp.text.Paragraph();
            almacenajeOperador.Font.SetStyle(iTextSharp.text.Font.BOLD);
            almacenajeOperador.Font = FontFactory.GetFont("Arial", 6);
            almacenajeOperador.Alignment = Element.ALIGN_LEFT;
            almacenajeOperador.Add("ALMACENAJE(m3)");
            cell2 = new PdfPCell();

            cell2.Colspan = 1;
            cell2.AddElement(almacenajeOperador);
            tablaColumna1.AddCell(cell2);

            var varCantidadDO = new iTextSharp.text.Paragraph();
            varCantidadDO.Font = FontFactory.GetFont("Arial", 7);
            varCantidadDO.Alignment = Element.ALIGN_CENTER;
            varCantidadDO.Add("");
            cell2 = new PdfPCell();

            cell2.Colspan = 2;
            cell2.AddElement(varCantidadDO);
            tablaColumna1.AddCell(cell2);

            var varCantidadGE = new iTextSharp.text.Paragraph();
            varCantidadGE.Font = FontFactory.GetFont("Arial", 7);
            varCantidadGE.Alignment = Element.ALIGN_CENTER;
            varCantidadGE.Add("");
            cell2 = new PdfPCell();

            cell2.Colspan = 2;
            cell2.AddElement(varCantidadGE);
            tablaColumna1.AddCell(cell2);

            var surtidorOperador = new iTextSharp.text.Paragraph();
            surtidorOperador.Font.SetStyle(iTextSharp.text.Font.BOLD);
            surtidorOperador.Font = FontFactory.GetFont("Arial", 6);
            surtidorOperador.Alignment = Element.ALIGN_LEFT;
            surtidorOperador.Add("SURTIDORES");
            cell2 = new PdfPCell();

            cell2.Colspan = 1;
            cell2.AddElement(surtidorOperador);
            tablaColumna1.AddCell(cell2);

            var dispenserOperador = new iTextSharp.text.Paragraph();
            dispenserOperador.Font = FontFactory.GetFont("Arial", 6);
            dispenserOperador.Alignment = Element.ALIGN_CENTER;
            dispenserOperador.Add("Dispenser");
            cell2 = new PdfPCell();

            cell2.Colspan = 1;
            cell2.AddElement(dispenserOperador);
            tablaColumna1.AddCell(cell2);

            var varDispenser = new iTextSharp.text.Paragraph();
            varDispenser.Font = FontFactory.GetFont("Arial", 6);
            varDispenser.Alignment = Element.ALIGN_CENTER;
            varDispenser.Add(dispenser);
            cell2 = new PdfPCell();

            cell2.Colspan = 1;
            cell2.AddElement(varDispenser);
            tablaColumna1.AddCell(cell2);

            var manguerasOperador = new iTextSharp.text.Paragraph();
            manguerasOperador.Font = FontFactory.GetFont("Arial", 6);
            manguerasOperador.Alignment = Element.ALIGN_CENTER;
            manguerasOperador.Add("Mangueras");
            cell2 = new PdfPCell();

            cell2.Colspan = 1;
            cell2.AddElement(manguerasOperador);
            tablaColumna1.AddCell(cell2);

            var varManguerasOperador = new iTextSharp.text.Paragraph();
            varManguerasOperador.Font = FontFactory.GetFont("Arial", 6);
            varManguerasOperador.Alignment = Element.ALIGN_CENTER;
            varManguerasOperador.Add(manguera);
            cell2 = new PdfPCell();

            cell2.Colspan = 1;
            cell2.AddElement(varManguerasOperador);
            tablaColumna1.AddCell(cell2);


            var georeferenciacionOperador = new iTextSharp.text.Paragraph();
            georeferenciacionOperador.Font.SetStyle(iTextSharp.text.Font.BOLD);
            georeferenciacionOperador.Font = FontFactory.GetFont("Arial", 6);
            georeferenciacionOperador.Alignment = Element.ALIGN_LEFT;
            georeferenciacionOperador.Add("GEOREFENCIACIÓN");
            cell2 = new PdfPCell();

            cell2.Colspan = 1;
            cell2.AddElement(georeferenciacionOperador);
            tablaColumna1.AddCell(cell2);

            var latitudOperador = new iTextSharp.text.Paragraph();
            latitudOperador.Font = FontFactory.GetFont("Arial", 6);
            latitudOperador.Alignment = Element.ALIGN_CENTER;
            latitudOperador.Add("Latitud");
            cell2 = new PdfPCell();

            cell2.Colspan = 1;
            cell2.AddElement(latitudOperador);
            tablaColumna1.AddCell(cell2);

            var varlatitudOperador = new iTextSharp.text.Paragraph();
            varlatitudOperador.Font = FontFactory.GetFont("Arial", 6);
            varlatitudOperador.Alignment = Element.ALIGN_CENTER;
            varlatitudOperador.Add(latitud.Substring(0, 12));
            cell2 = new PdfPCell();

            cell2.Colspan = 1;
            cell2.AddElement(varlatitudOperador);
            tablaColumna1.AddCell(cell2);

            var longitudOperador = new iTextSharp.text.Paragraph();
            longitudOperador.Font = FontFactory.GetFont("Arial", 6);
            longitudOperador.Alignment = Element.ALIGN_CENTER;
            longitudOperador.Add("Longitud");
            cell2 = new PdfPCell();

            cell2.Colspan = 1;
            cell2.AddElement(longitudOperador);
            tablaColumna1.AddCell(cell2);

            var varLongitudOperador = new iTextSharp.text.Paragraph();
            varLongitudOperador.Font = FontFactory.GetFont("Arial", 6);
            varLongitudOperador.Alignment = Element.ALIGN_CENTER;
            varLongitudOperador.Add(longitud.Substring(0, 12));
            cell2 = new PdfPCell();

            cell2.Colspan = 1;
            cell2.AddElement(varLongitudOperador);
            tablaColumna1.AddCell(cell2);

            var ventaOperador = new iTextSharp.text.Paragraph();
            ventaOperador.Font.SetStyle(iTextSharp.text.Font.BOLD);
            ventaOperador.Font = FontFactory.GetFont("Arial", 5);
            ventaOperador.Alignment = Element.ALIGN_LEFT;
            ventaOperador.Add("VENTA PROMEDIO 2022(m3/mes)");
            cell2 = new PdfPCell();
            cell2.FixedHeight = 20f;
            cell2.Colspan = 1;
            cell2.AddElement(ventaOperador);
            tablaColumna1.AddCell(cell2);

            var varventaOperador = new iTextSharp.text.Paragraph();
            varventaOperador.Font = FontFactory.GetFont("Arial", 7);
            varventaOperador.Alignment = Element.ALIGN_CENTER;
            varventaOperador.Add("");
            cell2 = new PdfPCell();
            cell2.FixedHeight = 20f;
            cell2.Colspan = 4;
            cell2.AddElement(varventaOperador);
            tablaColumna1.AddCell(cell2);

            cell12 = new PdfPCell();
            
            cell12.Colspan = 2;
            cell12.AddElement(tablaColumna1);

            tablaPrincipal.AddCell(cell12);

            PdfPCell cell3;
            cell3 = new PdfPCell();
            cell3.Colspan = 2;
            cell3.AddElement(img);

            tablaPrincipal.AddCell(cell3);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            
            List<string> lstCabecera = new List<string>() { "PROYECTO", "GESTION", "1-ENERO", "2-FEBRERO", "3-MARZO", "4-ABRIL", "5-MAYO", "6-JUNIO", "7-JULIO", "8-AGOSTO", "9-SEPTIEMBRE", "10-OCTUBRE", "11-NOVIEMBRE", "12-DICIEMBRE" };
            PdfPTable tablaGrid = new PdfPTable(lstCabecera.Count());
            var widthsG = new[] { 30f, 22f, 22f, 30f, 22f, 22f, 22f, 22f, 22f, 30f, 30f, 30f, 30f, 30f };
            tablaGrid.SetWidths(widthsG);

            if (lstAcumulado1 != null && lstAcumulado1.Count > 0)
            {
                //var titulosTablaB = new iTextSharp.text.Paragraph();
                //titulosTablaB.Add("");
                //PdfPCell cell6;
                //cell6 = new PdfPCell();
                //cell6.Colspan = 1;
                //cell6.AddElement(titulosTablaB);
                //tablaGrid.AddCell(cell6);
                //var lstAcumulado11 = lstAcumulado1.OrderBy(x => x.FECHA_REPORTE).ToList();
                var titulosTabla = new iTextSharp.text.Paragraph();
                foreach (var cb in lstCabecera)
                {                    
                    titulosTabla.Font.SetStyle(iTextSharp.text.Font.BOLD);
                    titulosTabla.Font = FontFactory.GetFont("Arial", 5);
                    titulosTabla.Alignment = Element.ALIGN_LEFT;
                    titulosTabla.Add(cb);
                    PdfPCell cell4;
                    cell4 = new PdfPCell();
                    cell4.Colspan = 1;
                    cell4.AddElement(titulosTabla);
                    tablaGrid.AddCell(cell4);
                    titulosTabla.Clear();
                }


                foreach (var fila in lstAcumulado1.OrderBy(x => x.strMes).ToList())
                {
                    foreach (var lstcb in lstCabecera)
                    {
                        if (lstCabecera.FirstOrDefault(l => l == reg2.strMes).Count() > 0)
                        {
                            titulosTabla.Font.SetStyle(iTextSharp.text.Font.BOLD);
                            titulosTabla.Font = FontFactory.GetFont("Arial", 5);
                            titulosTabla.Alignment = Element.ALIGN_LEFT;
                            titulosTabla.Add(cb);
                            PdfPCell cell4;
                            cell4 = new PdfPCell();
                            cell4.Colspan = 1;
                            cell4.AddElement(titulosTabla);
                            tablaGrid.AddCell(cell4);
                            titulosTabla.Clear();

                        }
                        else
                        {
                            titulosTabla.Font.SetStyle(iTextSharp.text.Font.BOLD);
                            titulosTabla.Font = FontFactory.GetFont("Arial", 5);
                            titulosTabla.Alignment = Element.ALIGN_LEFT;
                            titulosTabla.Add("0");
                            PdfPCell cell4;
                            cell4 = new PdfPCell();
                            cell4.Colspan = 1;
                            cell4.AddElement(titulosTabla);
                            tablaGrid.AddCell(cell4);
                            titulosTabla.Clear();
                        }
                    }
                }              

                //var titulosTabla = new iTextSharp.text.Paragraph();
                //titulosTabla.Font.SetStyle(iTextSharp.text.Font.BOLD);
                //titulosTabla.Font = FontFactory.GetFont("Arial", 5);
                //titulosTabla.Alignment = Element.ALIGN_LEFT;
                //titulosTabla.Add("PROYECTO");
                //PdfPCell cell4;
                //cell4 = new PdfPCell();
                //cell4.Colspan = 1;
                //cell4.AddElement(titulosTabla);
                //tablaGrid.AddCell(cell4);
                //titulosTabla.Clear();

                //titulosTabla.Font.SetStyle(iTextSharp.text.Font.BOLD);
                //titulosTabla.Font = FontFactory.GetFont("Arial", 5);
                //titulosTabla.Alignment = Element.ALIGN_LEFT;
                //titulosTabla.Add("GESTION");
                //cell4 = new PdfPCell();
                //cell4.Colspan = 1;
                //cell4.AddElement(titulosTabla);
                //tablaGrid.AddCell(cell4);
                //titulosTabla.Clear();

              

                //titulosTabla.Font.SetStyle(iTextSharp.text.Font.BOLD);
                //titulosTabla.Font = FontFactory.GetFont("Arial", 5);
                //titulosTabla.Alignment = Element.ALIGN_LEFT;
                //titulosTabla.Add("ENERO");
                //cell4 = new PdfPCell();
                //cell4.Colspan = 1;
                //cell4.AddElement(titulosTabla);
                //tablaGrid.AddCell(cell4);
                //titulosTabla.Clear();

                //titulosTabla.Font.SetStyle(iTextSharp.text.Font.BOLD);
                //titulosTabla.Font = FontFactory.GetFont("Arial", 5);
                //titulosTabla.Alignment = Element.ALIGN_LEFT;
                //titulosTabla.Add("FEBRERO");
                //cell4 = new PdfPCell();
                //cell4.Colspan = 1;
                //cell4.AddElement(titulosTabla);
                //tablaGrid.AddCell(cell4);
                //titulosTabla.Clear();

                //titulosTabla.Font.SetStyle(iTextSharp.text.Font.BOLD);
                //titulosTabla.Font = FontFactory.GetFont("Arial", 5);
                //titulosTabla.Alignment = Element.ALIGN_LEFT;
                //titulosTabla.Add("MARZO");
                //cell4 = new PdfPCell();
                //cell4.Colspan = 1;
                //cell4.AddElement(titulosTabla);
                //tablaGrid.AddCell(cell4);
                //titulosTabla.Clear();

                //titulosTabla.Font.SetStyle(iTextSharp.text.Font.BOLD);
                //titulosTabla.Font = FontFactory.GetFont("Arial", 5);
                //titulosTabla.Alignment = Element.ALIGN_LEFT;
                //titulosTabla.Add("ABRIL");
                //cell4 = new PdfPCell();
                //cell4.Colspan = 1;
                //cell4.AddElement(titulosTabla);
                //tablaGrid.AddCell(cell4);
                //titulosTabla.Clear();

                //titulosTabla.Font.SetStyle(iTextSharp.text.Font.BOLD);
                //titulosTabla.Font = FontFactory.GetFont("Arial", 5);
                //titulosTabla.Alignment = Element.ALIGN_LEFT;
                //titulosTabla.Add("MAYO");
                //cell4 = new PdfPCell();
                //cell4.Colspan = 1;
                //cell4.AddElement(titulosTabla);
                //tablaGrid.AddCell(cell4);
                //titulosTabla.Clear();

                //titulosTabla.Font.SetStyle(iTextSharp.text.Font.BOLD);
                //titulosTabla.Font = FontFactory.GetFont("Arial", 5);
                //titulosTabla.Alignment = Element.ALIGN_LEFT;
                //titulosTabla.Add("JUNIO");
                //cell4 = new PdfPCell();
                //cell4.Colspan = 1;
                //cell4.AddElement(titulosTabla);
                //tablaGrid.AddCell(cell4);
                //titulosTabla.Clear();

                //titulosTabla.Font.SetStyle(iTextSharp.text.Font.BOLD);
                //titulosTabla.Font = FontFactory.GetFont("Arial", 5);
                //titulosTabla.Alignment = Element.ALIGN_LEFT;
                //titulosTabla.Add("JULIO");
                //cell4 = new PdfPCell();
                //cell4.Colspan = 1;
                //cell4.AddElement(titulosTabla);
                //tablaGrid.AddCell(cell4);
                //titulosTabla.Clear();

                //titulosTabla.Font.SetStyle(iTextSharp.text.Font.BOLD);
                //titulosTabla.Font = FontFactory.GetFont("Arial", 5);
                //titulosTabla.Alignment = Element.ALIGN_LEFT;
                //titulosTabla.Add("AGOSTO");
                //cell4 = new PdfPCell();
                //cell4.Colspan = 1;
                //cell4.AddElement(titulosTabla);
                //tablaGrid.AddCell(cell4);
                //titulosTabla.Clear();

                //titulosTabla.Font.SetStyle(iTextSharp.text.Font.BOLD);
                //titulosTabla.Font = FontFactory.GetFont("Arial", 5);
                //titulosTabla.Alignment = Element.ALIGN_LEFT;
                //titulosTabla.Add("SEPTIEMBRE");
                //cell4 = new PdfPCell();
                //cell4.Colspan = 1;
                //cell4.AddElement(titulosTabla);
                //tablaGrid.AddCell(cell4);
                //titulosTabla.Clear();

                //titulosTabla.Font.SetStyle(iTextSharp.text.Font.BOLD);
                //titulosTabla.Font = FontFactory.GetFont("Arial", 5);
                //titulosTabla.Alignment = Element.ALIGN_LEFT;
                //titulosTabla.Add("OCTUBRE");
                //cell4 = new PdfPCell();
                //cell4.Colspan = 1;
                //cell4.AddElement(titulosTabla);
                //tablaGrid.AddCell(cell4);
                //titulosTabla.Clear();

                //titulosTabla.Font.SetStyle(iTextSharp.text.Font.BOLD);
                //titulosTabla.Font = FontFactory.GetFont("Arial", 5);
                //titulosTabla.Alignment = Element.ALIGN_LEFT;
                //titulosTabla.Add("NOVIEMBRE");
                //cell4 = new PdfPCell();
                //cell4.Colspan = 1;
                //cell4.AddElement(titulosTabla);
                //tablaGrid.AddCell(cell4);
                //titulosTabla.Clear();

                //titulosTabla.Font.SetStyle(iTextSharp.text.Font.BOLD);
                //titulosTabla.Font = FontFactory.GetFont("Arial", 5);
                //titulosTabla.Alignment = Element.ALIGN_LEFT;
                //titulosTabla.Add("DICIEMBRE");
                //cell4 = new PdfPCell();
                //cell4.Colspan = 1;
                //cell4.AddElement(titulosTabla);
                //tablaGrid.AddCell(cell4);
                //titulosTabla.Clear();



                //PdfPCell cell5;
                //cell5 = new PdfPCell();
                //cell5.Colspan = 2;
                //cell5.AddElement(tablaGrid);
                //tablaPrincipal.AddCell(cell5);


                //int sww = 1;
                //foreach (var reg2 in lstAcumulado11)
                //{
                //    if (sww == 1)
                //    {
                //        var datosTablaGestion = new iTextSharp.text.Paragraph();
                //        datosTablaGestion.Font.SetStyle(iTextSharp.text.Font.BOLD);
                //        datosTablaGestion.Font = FontFactory.GetFont("Arial", 5);
                //        datosTablaGestion.Alignment = Element.ALIGN_LEFT;
                //        datosTablaGestion.Add(reg2.strGestion.ToString());
                //        PdfPCell cell8;
                //        cell8 = new PdfPCell();
                //        cell8.Colspan = 1;
                //        cell8.AddElement(datosTablaGestion);
                //        tablaGrid.AddCell(cell8);
                //        sww = 0;
                //    }
                //    if (reg2.strMes == "6-JUNIO")
                //    {
                //        var datosTabla = new iTextSharp.text.Paragraph();
                //        datosTabla.Font.SetStyle(iTextSharp.text.Font.BOLD);
                //        datosTabla.Font = FontFactory.GetFont("Arial", 5);
                //        datosTabla.Alignment = Element.ALIGN_LEFT;
                //        datosTabla.Add(reg2.decCantidad.ToString());
                //        PdfPCell cell5;
                //        cell5 = new PdfPCell();
                //        cell5.Colspan = 1;
                //        cell5.AddElement(datosTabla);
                //        tablaGrid.AddCell(cell5);
                //        sww++;
                //    }
                //    if (sww == 12) sww = 1;

                //}
                PdfPCell cell7;
                cell7 = new PdfPCell();
                cell7.Colspan = 2;
                cell7.AddElement(tablaGrid);
                tablaPrincipal.AddCell(cell7);
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            
            cell3 = new PdfPCell();
            cell3.Colspan = 2;
            cell3.AddElement(img2);

            tablaPrincipal.AddCell(cell3);

            Doc.Add(tablaPrincipal);

            Doc.Close();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=Chart.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Write(Doc);
            Response.End();
        }*/

        protected void btnExport_Click(object sender, EventArgs e)
        {

            BindingList<Entry> ds = new BindingList<Entry>();
            ds = (BindingList<Entry>)Session["datosHydro"];
            var longitud = ds[9].DATO1;
            var latitud = ds[9].IDs;
            var denominacion = ds[0].Name;
            var departamento = ds[1].ID;
            var municipio = ds[2].ID;
            var telefono = ds[3].ID;
            var licencia = ds[4].ID;
            var vigencia = ds[5].ID;
            var productos = ds[6].ID;
            var dispenser = ds[8].IDs;
            var manguera = ds[8].DATO1;
            var VentaPomedioGE = ds[10].ID;
            var VentaPomedioDO = ds[10].IDs;
            //string pathToFiles = Server.MapPath("~/Sitio/Reportes/cabecera.png");

            string pathToFiles = "https://ppa6vrsatcop.anh.gob.bo:9643/M201O8012V4/Sitio/Reportes/cabecera.png";
            //string pathToFiles = "https://ppa1vrsatcoti.anh.gob.bo:8143/a39992015/Sitio/Reportes/cabecera.png";
            // string pathToFiles = "http://localhost:1070/Sitio/Reportes/cabecera.png";
            iTextSharp.text.Image cab = iTextSharp.text.Image.GetInstance(pathToFiles);
            cab.ScaleToFit(480, 260F);

            //Imagen - Esquina inferior izquierda
            //cab.SetAbsolutePosition(10, 650);

            iTextSharp.text.Document Doc = new iTextSharp.text.Document(PageSize.LETTER, 2f, 2f, 40f, 10f);
            PdfWriter.GetInstance(Doc, Response.OutputStream);
            var temppath = System.IO.Path.GetTempFileName();

            var output = new FileStream(temppath, FileMode.Create);
            //Creo el Writer.
            var writer = PdfWriter.GetInstance(Doc, output);

            Doc.Open();
            //Doc.Add(cab);
            MemoryStream memoryStream = new MemoryStream();
            MemoryStream memoryStreamDO = new MemoryStream();

            //iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance("https://maps.googleapis.com/maps/api/staticmap?center=" + latitud + "," + longitud + "&zoom=17&size=500x480&markers=color:red%7Clabel:E%7C" + latitud + "," + longitud + "&key=AIzaSyD3AkEndc2Z3UA3HycG0CPLySrLJnCoE9s");
            iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance("https://maps.googleapis.com/maps/api/staticmap?center=" + latitud + "," + longitud + "&zoom=17&size=500x480&markers=color:red%7Clabel:E%7C" + latitud + "," + longitud + "&maptype=hybrid&key=AIzaSyD3AkEndc2Z3UA3HycG0CPLySrLJnCoE9s");
            imagen.ScalePercent(50f);
            //Doc.Add(imagen);

            var lstAcumulado1 = (List<O_LISTADO_DDJJ_CTY>)Session[CVariablesSesion.GrillaCabecera];
            var lstAcumulado2 = (List<O_LISTADO_DDJJ_CTY>)Session["GrillaCabeceraDO"];
            iTextSharp.text.Image img2 = null;
            iTextSharp.text.Image img = null;

            if (lstAcumulado1 != null && lstAcumulado1.Count > 0)
            {
                foreach (var reg in gestiones)
                {
                    Chart1.Series.Add(new System.Web.UI.DataVisualization.Charting.Series(reg.ToString()));
                }
                //Chart1.Series.Add(new System.Web.UI.DataVisualization.Charting.Series("2018"));
                //Chart1.Series.Add(new System.Web.UI.DataVisualization.Charting.Series("2019"));
                //Chart1.Series.Add(new System.Web.UI.DataVisualization.Charting.Series("2020"));
                //Chart1.Series.Add(new System.Web.UI.DataVisualization.Charting.Series("2021"));
                //Chart1.Series.Add(new System.Web.UI.DataVisualization.Charting.Series("2022"));
                foreach (var reg in lstAcumulado1.OrderBy(x => x.strMes).ToList())
                {
                    //if (reg.strGestion.ToString() == "2021" && sw1==1)
                    //{
                    //    //Add Series to the Chart.
                    //    Chart1.Series.Add(new System.Web.UI.DataVisualization.Charting.Series("2021"));
                    //    sw1 = 0;
                    //}
                    Chart1.Series[reg.strGestion.ToString()].IsValueShownAsLabel = false;
                    Chart1.Series[reg.strGestion.ToString()].ChartType = SeriesChartType.Column;                    
                    Chart1.Series[reg.strGestion.ToString()].Points.AddXY(reg.strMes, reg.decCantidad);
                }
                Chart1.SaveImage(memoryStream, ChartImageFormat.Png);
                img = iTextSharp.text.Image.GetInstance(memoryStream.GetBuffer());
                img.ScalePercent(40f);
            }
            else
            {
                Chart1.DataSource = null;
                Chart1.DataBind();
            }

            if (lstAcumulado2 != null && lstAcumulado2.Count > 0)
            {
                foreach (var reg in gestiones)
                {                    
                    Chart2.Series.Add(new System.Web.UI.DataVisualization.Charting.Series(reg.ToString()));
                }
                //Chart2.Series.Add(new System.Web.UI.DataVisualization.Charting.Series("2018"));
                //Chart2.Series.Add(new System.Web.UI.DataVisualization.Charting.Series("2019"));
                //Chart2.Series.Add(new System.Web.UI.DataVisualization.Charting.Series("2020"));
                //Chart2.Series.Add(new System.Web.UI.DataVisualization.Charting.Series("2021"));
                //Chart2.Series.Add(new System.Web.UI.DataVisualization.Charting.Series("2022"));
                foreach (var reg2 in lstAcumulado2.OrderBy(x => x.strMes).ToList())
                {
                    //if (reg2.strGestion.ToString() == "2021" && sw1 == 1)
                    //{
                    //    //Add Series to the Chart.
                    //    Chart2.Series.Add(new System.Web.UI.DataVisualization.Charting.Series("2021"));
                    //    sw1 = 0;
                    //}
                    Chart2.Series[reg2.strGestion.ToString()].IsValueShownAsLabel = false;
                    Chart2.Series[reg2.strGestion.ToString()].ChartType = SeriesChartType.Column;
                    Chart2.Series[reg2.strGestion.ToString()].Points.AddXY(reg2.strMes, reg2.decCantidad);
                }
                // Chart2.Titles[0].Text="fff";
                Chart2.SaveImage(memoryStreamDO, ChartImageFormat.Png);
                img2 = iTextSharp.text.Image.GetInstance(memoryStreamDO.GetBuffer());
                img2.ScalePercent(40f);
            }
            else
            {
                Chart2.DataSource = null;
                Chart2.DataBind();
            }
                        
            //Doc.Add(img);
            
            PdfPTable tablaPrincipal = new PdfPTable(2);
            var widthsP = new[] { 115f, 105f };

            tablaPrincipal.SetWidths(widthsP);
            PdfPCell cell1;
            PdfPCell cell2;
            PdfPCell cell12;
            cell1 = new PdfPCell();
            cell1.Colspan = 2;
            cell1.AddElement(cab);

            tablaPrincipal.AddCell(cell1);

            cell1 = new PdfPCell();
            cell1.AddElement(imagen);
            tablaPrincipal.AddCell(cell1);


            PdfPTable tablaColumna1 = new PdfPTable(5);
            var widths = new[] { 90f, 45f, 65f, 48f, 65f };

            tablaColumna1.SetWidths(widths);
            var datosGenerales = new iTextSharp.text.Paragraph();
            datosGenerales.Font.SetStyle(iTextSharp.text.Font.BOLD);
            datosGenerales.Font = FontFactory.GetFont("Arial", 10);
            datosGenerales.Alignment = Element.ALIGN_CENTER;
            datosGenerales.Add("DATOS GENERALES");
            cell2 = new PdfPCell();

            cell2.Colspan = 5;
            cell2.AddElement(datosGenerales);
            //cell2.FixedHeight = 14f;
            tablaColumna1.AddCell(cell2);

            var nombreOperador = new iTextSharp.text.Paragraph();
            nombreOperador.Font.SetStyle(iTextSharp.text.Font.BOLD);
            nombreOperador.Font = FontFactory.GetFont("Arial", 9);
            nombreOperador.Alignment = Element.ALIGN_CENTER;
            nombreOperador.Add(denominacion);
            cell2 = new PdfPCell();

            cell2.Colspan = 5;
            cell2.AddElement(nombreOperador);
            tablaColumna1.AddCell(cell2);

            var departamentoOperador = new iTextSharp.text.Paragraph();
            departamentoOperador.Font.SetStyle(iTextSharp.text.Font.BOLD);
            departamentoOperador.Font = FontFactory.GetFont("Arial", 7);
            departamentoOperador.Alignment = Element.ALIGN_JUSTIFIED;
            departamentoOperador.Add("DEPARTAMENTO");
            cell2 = new PdfPCell();

            cell2.Colspan = 1;
            cell2.AddElement(departamentoOperador);
            tablaColumna1.AddCell(cell2);

            var varDepartamentoOperador = new iTextSharp.text.Paragraph();
            varDepartamentoOperador.Font = FontFactory.GetFont("Arial", 7);
            varDepartamentoOperador.Alignment = Element.ALIGN_CENTER;
            varDepartamentoOperador.Add(departamento);
            cell2 = new PdfPCell();

            cell2.Colspan = 4;
            cell2.AddElement(varDepartamentoOperador);
            tablaColumna1.AddCell(cell2);

            var municipioOperador = new iTextSharp.text.Paragraph();
            municipioOperador.Font.SetStyle(iTextSharp.text.Font.BOLD);
            municipioOperador.Font = FontFactory.GetFont("Arial", 7);
            municipioOperador.Alignment = Element.ALIGN_LEFT;
            municipioOperador.Add("MUNICIPIO");
            cell2 = new PdfPCell();

            cell2.Colspan = 1;
            cell2.AddElement(municipioOperador);
            tablaColumna1.AddCell(cell2);

            var varMunicipioOperador = new iTextSharp.text.Paragraph();
            varMunicipioOperador.Font = FontFactory.GetFont("Arial", 7);
            varMunicipioOperador.Alignment = Element.ALIGN_CENTER;
            varMunicipioOperador.Add(municipio);
            cell2 = new PdfPCell();

            cell2.Colspan = 4;
            cell2.AddElement(varMunicipioOperador);
            tablaColumna1.AddCell(cell2);

            var telefonoOperador = new iTextSharp.text.Paragraph();
            telefonoOperador.Font.SetStyle(iTextSharp.text.Font.BOLD);
            telefonoOperador.Font = FontFactory.GetFont("Arial", 7);
            telefonoOperador.Alignment = Element.ALIGN_LEFT;
            telefonoOperador.Add("TELÉFONO");
            cell2 = new PdfPCell();

            cell2.Colspan = 1;
            cell2.AddElement(telefonoOperador);
            tablaColumna1.AddCell(cell2);

            var varTelefonoOperador = new iTextSharp.text.Paragraph();
            varTelefonoOperador.Font = FontFactory.GetFont("Arial", 7);
            varTelefonoOperador.Alignment = Element.ALIGN_CENTER;
            varTelefonoOperador.Add(telefono);
            cell2 = new PdfPCell();

            cell2.Colspan = 4;
            cell2.AddElement(varTelefonoOperador);
            tablaColumna1.AddCell(cell2);

            var licenciaOperador = new iTextSharp.text.Paragraph();
            licenciaOperador.Font.SetStyle(iTextSharp.text.Font.BOLD);
            licenciaOperador.Font = FontFactory.GetFont("Arial", 7);
            licenciaOperador.Alignment = Element.ALIGN_LEFT;
            licenciaOperador.Add("LICENCIA");
            cell2 = new PdfPCell();
            cell2.Colspan = 1;
            cell2.AddElement(licenciaOperador);
            tablaColumna1.AddCell(cell2);

            var varLicenciaOperador = new iTextSharp.text.Paragraph();
            varLicenciaOperador.Font = FontFactory.GetFont("Arial", 7);
            varLicenciaOperador.Alignment = Element.ALIGN_CENTER;
            varLicenciaOperador.Add(licencia);
            cell2 = new PdfPCell();

            cell2.Colspan = 4;
            cell2.AddElement(varLicenciaOperador);
            tablaColumna1.AddCell(cell2);

            var vigenciaOperador = new iTextSharp.text.Paragraph();
            vigenciaOperador.Font.SetStyle(iTextSharp.text.Font.BOLD);
            vigenciaOperador.Font = FontFactory.GetFont("Arial", 7);
            vigenciaOperador.Alignment = Element.ALIGN_LEFT;
            vigenciaOperador.Add("VIGENCIA L.O.");
            cell2 = new PdfPCell();

            cell2.Colspan = 1;
            cell2.AddElement(vigenciaOperador);
            tablaColumna1.AddCell(cell2);

            var varVigenciaOperador = new iTextSharp.text.Paragraph();
            varVigenciaOperador.Font = FontFactory.GetFont("Arial", 7);
            varVigenciaOperador.Alignment = Element.ALIGN_CENTER;
            varVigenciaOperador.Add(vigencia);
            cell2 = new PdfPCell();

            cell2.Colspan = 4;
            cell2.AddElement(varVigenciaOperador);
            tablaColumna1.AddCell(cell2);

            var productosOperador = new iTextSharp.text.Paragraph();
            productosOperador.Font.SetStyle(iTextSharp.text.Font.BOLD);
            productosOperador.Font = FontFactory.GetFont("Arial", 7);
            productosOperador.Alignment = Element.ALIGN_LEFT;
            productosOperador.Add("PRODUCTOS");
            cell2 = new PdfPCell();

            cell2.Colspan = 1;
            cell2.AddElement(productosOperador);
            tablaColumna1.AddCell(cell2);

            var varProductosDOOperador = new iTextSharp.text.Paragraph();
            varProductosDOOperador.Font.SetStyle(iTextSharp.text.Font.BOLD);
            varProductosDOOperador.Font = FontFactory.GetFont("Arial", 7);
            varProductosDOOperador.Alignment = Element.ALIGN_CENTER;
            varProductosDOOperador.Add(productos);
            cell2 = new PdfPCell();

            cell2.Colspan = 2;
            cell2.AddElement(varProductosDOOperador);
            tablaColumna1.AddCell(cell2);

            var varProductosGEOperador = new iTextSharp.text.Paragraph();
            varProductosGEOperador.Font.SetStyle(iTextSharp.text.Font.BOLD);
            varProductosGEOperador.Font = FontFactory.GetFont("Arial", 7);
            varProductosGEOperador.Alignment = Element.ALIGN_CENTER;
            varProductosGEOperador.Add("");
            cell2 = new PdfPCell();

            cell2.Colspan = 2;
            cell2.AddElement(varProductosGEOperador);
            tablaColumna1.AddCell(cell2);

            var almacenajeOperador = new iTextSharp.text.Paragraph();
            almacenajeOperador.Font.SetStyle(iTextSharp.text.Font.BOLD);
            almacenajeOperador.Font = FontFactory.GetFont("Arial", 6);
            almacenajeOperador.Alignment = Element.ALIGN_LEFT;
            almacenajeOperador.Add("ALMACENAJE(m3)");
            cell2 = new PdfPCell();

            cell2.Colspan = 1;
            cell2.AddElement(almacenajeOperador);
            tablaColumna1.AddCell(cell2);

            var varCantidadDO = new iTextSharp.text.Paragraph();
            varCantidadDO.Font = FontFactory.GetFont("Arial", 7);
            varCantidadDO.Alignment = Element.ALIGN_CENTER;
            varCantidadDO.Add("");
            cell2 = new PdfPCell();

            cell2.Colspan = 2;
            cell2.AddElement(varCantidadDO);
            tablaColumna1.AddCell(cell2);

            var varCantidadGE = new iTextSharp.text.Paragraph();
            varCantidadGE.Font = FontFactory.GetFont("Arial", 7);
            varCantidadGE.Alignment = Element.ALIGN_CENTER;
            varCantidadGE.Add("");
            cell2 = new PdfPCell();

            cell2.Colspan = 2;
            cell2.AddElement(varCantidadGE);
            tablaColumna1.AddCell(cell2);

            var surtidorOperador = new iTextSharp.text.Paragraph();
            surtidorOperador.Font.SetStyle(iTextSharp.text.Font.BOLD);
            surtidorOperador.Font = FontFactory.GetFont("Arial", 6);
            surtidorOperador.Alignment = Element.ALIGN_LEFT;
            surtidorOperador.Add("SURTIDORES");
            cell2 = new PdfPCell();

            cell2.Colspan = 1;
            cell2.AddElement(surtidorOperador);
            tablaColumna1.AddCell(cell2);

            var dispenserOperador = new iTextSharp.text.Paragraph();
            dispenserOperador.Font = FontFactory.GetFont("Arial", 6);
            dispenserOperador.Alignment = Element.ALIGN_CENTER;
            dispenserOperador.Add("Dispenser");
            cell2 = new PdfPCell();

            cell2.Colspan = 1;
            cell2.AddElement(dispenserOperador);
            tablaColumna1.AddCell(cell2);

            var varDispenser = new iTextSharp.text.Paragraph();
            varDispenser.Font = FontFactory.GetFont("Arial", 6);
            varDispenser.Alignment = Element.ALIGN_CENTER;
            varDispenser.Add(dispenser);
            cell2 = new PdfPCell();

            cell2.Colspan = 1;
            cell2.AddElement(varDispenser);
            tablaColumna1.AddCell(cell2);

            var manguerasOperador = new iTextSharp.text.Paragraph();
            manguerasOperador.Font = FontFactory.GetFont("Arial", 6);
            manguerasOperador.Alignment = Element.ALIGN_CENTER;
            manguerasOperador.Add("Mangueras");
            cell2 = new PdfPCell();

            cell2.Colspan = 1;
            cell2.AddElement(manguerasOperador);
            tablaColumna1.AddCell(cell2);

            var varManguerasOperador = new iTextSharp.text.Paragraph();
            varManguerasOperador.Font = FontFactory.GetFont("Arial", 6);
            varManguerasOperador.Alignment = Element.ALIGN_CENTER;
            varManguerasOperador.Add(manguera);
            cell2 = new PdfPCell();

            cell2.Colspan = 1;
            cell2.AddElement(varManguerasOperador);
            tablaColumna1.AddCell(cell2);


            var georeferenciacionOperador = new iTextSharp.text.Paragraph();
            georeferenciacionOperador.Font.SetStyle(iTextSharp.text.Font.BOLD);
            georeferenciacionOperador.Font = FontFactory.GetFont("Arial", 6);
            georeferenciacionOperador.Alignment = Element.ALIGN_LEFT;
            georeferenciacionOperador.Add("GEOREFENCIACIÓN");
            cell2 = new PdfPCell();

            cell2.Colspan = 1;
            cell2.AddElement(georeferenciacionOperador);
            tablaColumna1.AddCell(cell2);

            var latitudOperador = new iTextSharp.text.Paragraph();
            latitudOperador.Font = FontFactory.GetFont("Arial", 6);
            latitudOperador.Alignment = Element.ALIGN_CENTER;
            latitudOperador.Add("Latitud");
            cell2 = new PdfPCell();

            cell2.Colspan = 1;
            cell2.AddElement(latitudOperador);
            tablaColumna1.AddCell(cell2);

            var varlatitudOperador = new iTextSharp.text.Paragraph();
            varlatitudOperador.Font = FontFactory.GetFont("Arial", 6);
            varlatitudOperador.Alignment = Element.ALIGN_CENTER;
            varlatitudOperador.Add(latitud.Substring(0, 12));
            cell2 = new PdfPCell();

            cell2.Colspan = 1;
            cell2.AddElement(varlatitudOperador);
            tablaColumna1.AddCell(cell2);

            var longitudOperador = new iTextSharp.text.Paragraph();
            longitudOperador.Font = FontFactory.GetFont("Arial", 6);
            longitudOperador.Alignment = Element.ALIGN_CENTER;
            longitudOperador.Add("Longitud");
            cell2 = new PdfPCell();

            cell2.Colspan = 1;
            cell2.AddElement(longitudOperador);
            tablaColumna1.AddCell(cell2);

            var varLongitudOperador = new iTextSharp.text.Paragraph();
            varLongitudOperador.Font = FontFactory.GetFont("Arial", 6);
            varLongitudOperador.Alignment = Element.ALIGN_CENTER;
            varLongitudOperador.Add(longitud.Substring(0, 12));
            cell2 = new PdfPCell();

            cell2.Colspan = 1;
            cell2.AddElement(varLongitudOperador);
            tablaColumna1.AddCell(cell2);

            var ventaOperador = new iTextSharp.text.Paragraph();
            ventaOperador.Font.SetStyle(iTextSharp.text.Font.BOLD);
            ventaOperador.Font = FontFactory.GetFont("Arial", 6);
            ventaOperador.Alignment = Element.ALIGN_LEFT;
            ventaOperador.Add("VENTA PROMEDIO 2022(m3/mes)");
            cell2 = new PdfPCell();
            cell2.FixedHeight = 20f;
            cell2.Colspan = 1;
            cell2.AddElement(ventaOperador);
            tablaColumna1.AddCell(cell2);

            //var ventaPromedioGE=lstAcumulado1.FirstOrDefault(l=>l.strMes=="AGOSTO")
            var varventaOperador = new iTextSharp.text.Paragraph();
            varventaOperador.Font = FontFactory.GetFont("Arial", 6);
            varventaOperador.Alignment = Element.ALIGN_CENTER;
            varventaOperador.Add(VentaPomedioGE+" "+VentaPomedioDO);
            cell2 = new PdfPCell();
            cell2.FixedHeight = 20f;
            cell2.Colspan = 4;
            cell2.AddElement(varventaOperador);
            tablaColumna1.AddCell(cell2);

            tablaPrincipal.AddCell(tablaColumna1);

            PdfPCell cell3;
            cell3 = new PdfPCell();
            cell3.Colspan = 2;
            cell3.AddElement(img);

            tablaPrincipal.AddCell(cell3);


            //cell3 = new PdfPCell();
            //cell3.Colspan = 2;
            //cell3.AddElement(img);

            //tablaPrincipal.AddCell(cell3);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            
            //Defincion de del formato decimal//
            NumberFormatInfo formato = new CultureInfo("es-AR").NumberFormat;
            formato.CurrencyGroupSeparator = ".";
            formato.NumberDecimalSeparator = ",";
            //puedes cambiar el simbolo $ por otro.
            //formato.CurrencySymbol = "$";
            //--numero.ToString("C", formato);
            //--numero.ToString("N", formato);
            //end formato//

            PdfPTable tablaGrid = new PdfPTable(13);
            var widthsG = new[] { 35f, 35f, 42f, 35f, 35f, 35f, 35f, 35f, 42f, 50f, 50f, 50f, 50f };
            tablaGrid.TotalWidth = 485f;
            tablaGrid.LockedWidth = true;
            tablaGrid.SetWidths(widthsG);

            PdfPTable tablaGridValores = new PdfPTable(14);
            var widthsGValores = new[] { 290f, 100f, 100f, 100f, 100f, 100f, 100f, 100f, 100f, 100f, 100f, 100f, 100f, 150f };
            tablaGridValores.SetWidths(widthsGValores);
            tablaGridValores.TotalWidth = 485f;
            tablaGridValores.LockedWidth = true;           

            var titulosCab = new iTextSharp.text.Paragraph();
            titulosCab.Font.SetStyle(iTextSharp.text.Font.BOLD);
            titulosCab.Font = FontFactory.GetFont("Arial", 5);
            titulosCab.Alignment = Element.ALIGN_CENTER;
            titulosCab.Add("EXPRESADO EN (m3)");
            PdfPCell cellcab;
            cellcab = new PdfPCell();
            cellcab.Colspan = 13;
            cellcab.AddElement(titulosCab);
            tablaGrid.AddCell(cellcab);

            if (lstAcumulado1 != null && lstAcumulado1.Count > 0)
            {
                var titulosTablaB = new iTextSharp.text.Paragraph();
                titulosTablaB.Font.SetStyle(iTextSharp.text.Font.BOLD);
                titulosTablaB.Font = FontFactory.GetFont("Arial", 5);
                titulosTablaB.Alignment = Element.ALIGN_LEFT;
                titulosTablaB.Add("GESTION");
                PdfPCell cell6;
                cell6 = new PdfPCell();
                cell6.Colspan = 1;
                cell6.AddElement(titulosTablaB);
                tablaGrid.AddCell(cell6);
                var lstAcumulado11 = lstAcumulado1.OrderBy(x => x.FECHA_REPORTE);


                var titulosTabla = new iTextSharp.text.Paragraph();
                titulosTabla.Font.SetStyle(iTextSharp.text.Font.BOLD);
                titulosTabla.Font = FontFactory.GetFont("Arial", 5);
                titulosTabla.Alignment = Element.ALIGN_LEFT;
                titulosTabla.Add("ENERO");
                PdfPCell cell4;
                cell4 = new PdfPCell();
                cell4.Colspan = 1;
                cell4.AddElement(titulosTabla);
                tablaGrid.AddCell(cell4);
                titulosTabla.Clear();


                titulosTabla.Font.SetStyle(iTextSharp.text.Font.BOLD);
                titulosTabla.Font = FontFactory.GetFont("Arial", 5);
                titulosTabla.Alignment = Element.ALIGN_LEFT;
                titulosTabla.Add("FEBRERO");
                cell4 = new PdfPCell();
                cell4.Colspan = 1;
                cell4.AddElement(titulosTabla);
                tablaGrid.AddCell(cell4);
                titulosTabla.Clear();

                titulosTabla.Font.SetStyle(iTextSharp.text.Font.BOLD);
                titulosTabla.Font = FontFactory.GetFont("Arial", 5);
                titulosTabla.Alignment = Element.ALIGN_LEFT;
                titulosTabla.Add("MARZO");
                cell4 = new PdfPCell();
                cell4.Colspan = 1;
                cell4.AddElement(titulosTabla);
                tablaGrid.AddCell(cell4);
                titulosTabla.Clear();

                titulosTabla.Font.SetStyle(iTextSharp.text.Font.BOLD);
                titulosTabla.Font = FontFactory.GetFont("Arial", 5);
                titulosTabla.Alignment = Element.ALIGN_LEFT;
                titulosTabla.Add("ABRIL");
                cell4 = new PdfPCell();
                cell4.Colspan = 1;
                cell4.AddElement(titulosTabla);
                tablaGrid.AddCell(cell4);
                titulosTabla.Clear();

                titulosTabla.Font.SetStyle(iTextSharp.text.Font.BOLD);
                titulosTabla.Font = FontFactory.GetFont("Arial", 5);
                titulosTabla.Alignment = Element.ALIGN_LEFT;
                titulosTabla.Add("MAYO");
                cell4 = new PdfPCell();
                cell4.Colspan = 1;
                cell4.AddElement(titulosTabla);
                tablaGrid.AddCell(cell4);
                titulosTabla.Clear();

                titulosTabla.Font.SetStyle(iTextSharp.text.Font.BOLD);
                titulosTabla.Font = FontFactory.GetFont("Arial", 5);
                titulosTabla.Alignment = Element.ALIGN_LEFT;
                titulosTabla.Add("JUNIO");
                cell4 = new PdfPCell();
                cell4.Colspan = 1;
                cell4.AddElement(titulosTabla);
                tablaGrid.AddCell(cell4);
                titulosTabla.Clear();

                titulosTabla.Font.SetStyle(iTextSharp.text.Font.BOLD);
                titulosTabla.Font = FontFactory.GetFont("Arial", 5);
                titulosTabla.Alignment = Element.ALIGN_LEFT;
                titulosTabla.Add("JULIO");
                cell4 = new PdfPCell();
                cell4.Colspan = 1;
                cell4.AddElement(titulosTabla);
                tablaGrid.AddCell(cell4);
                titulosTabla.Clear();

                titulosTabla.Font.SetStyle(iTextSharp.text.Font.BOLD);
                titulosTabla.Font = FontFactory.GetFont("Arial", 5);
                titulosTabla.Alignment = Element.ALIGN_LEFT;
                titulosTabla.Add("AGOSTO");
                cell4 = new PdfPCell();
                cell4.Colspan = 1;
                cell4.AddElement(titulosTabla);
                tablaGrid.AddCell(cell4);
                titulosTabla.Clear();

                titulosTabla.Font.SetStyle(iTextSharp.text.Font.BOLD);
                titulosTabla.Font = FontFactory.GetFont("Arial", 5);
                titulosTabla.Alignment = Element.ALIGN_LEFT;
                titulosTabla.Add("SEPTIEMBRE");
                cell4 = new PdfPCell();
                cell4.Colspan = 1;
                cell4.AddElement(titulosTabla);
                tablaGrid.AddCell(cell4);
                titulosTabla.Clear();

                titulosTabla.Font.SetStyle(iTextSharp.text.Font.BOLD);
                titulosTabla.Font = FontFactory.GetFont("Arial", 5);
                titulosTabla.Alignment = Element.ALIGN_LEFT;
                titulosTabla.Add("OCTUBRE");
                cell4 = new PdfPCell();
                cell4.Colspan = 1;
                cell4.AddElement(titulosTabla);
                tablaGrid.AddCell(cell4);
                titulosTabla.Clear();

                titulosTabla.Font.SetStyle(iTextSharp.text.Font.BOLD);
                titulosTabla.Font = FontFactory.GetFont("Arial", 5);
                titulosTabla.Alignment = Element.ALIGN_LEFT;
                titulosTabla.Add("NOVIEMBRE");
                cell4 = new PdfPCell();
                cell4.Colspan = 1;
                cell4.AddElement(titulosTabla);
                tablaGrid.AddCell(cell4);
                titulosTabla.Clear();

                titulosTabla.Font.SetStyle(iTextSharp.text.Font.BOLD);
                titulosTabla.Font = FontFactory.GetFont("Arial", 5);
                titulosTabla.Alignment = Element.ALIGN_LEFT;
                titulosTabla.Add("DICIEMBRE");
                cell4 = new PdfPCell();
                cell4.Colspan = 1;
                cell4.AddElement(titulosTabla);
                tablaGrid.AddCell(cell4);
                titulosTabla.Clear();
                              
                string[] meses = { "01-ENERO", "02-FEBRERO", "03-MARZO", "04-ABRIL", "05-MAYO", "06-JUNIO", "07-JULIO", "08-AGOSTO", "09-SEPTIEMBRE", "10-OCTUBRE", "11-NOVIEMBRE", "12-DICIEMBRE" };
                //int[] gestiones = { 2018, 2019, 2020, 2021, 2022 };
                decimal[,] valoresGE = new decimal[2, 12]; 
                int sww = 1;
                int swDF = 0;                
                bool blElse = true;
                for (int jj = 0; jj < gestiones.Count(); jj++)
                {
                    sww = 1;
                    for (int j = 0; j < 12; j++)
                    {
                        foreach (var reg2 in lstAcumulado11.OrderBy(x => x.strMes).ToList())
                        {

                            if (sww == 1)
                            {                                                                        
                                //var str2018 = FontFactory.GetFont("trebuchet ms", 8);
                                //str2018.SetColor(51, 153, 255);
                                //var str2019 = FontFactory.GetFont("trebuchet ms", 8);
                                //str2019.SetColor(255, 153, 51);
                                //var str2020 = FontFactory.GetFont("trebuchet ms", 8);
                                //str2020.SetColor(255, 0, 0);
                                //var str2021 = FontFactory.GetFont("trebuchet ms", 8);
                                //str2021.SetColor(0, 73, 156);
                                //var str2022 = FontFactory.GetFont("trebuchet ms", 8);
                                //str2022.SetColor(96, 96, 96);
                                var datosTablaGestion = new iTextSharp.text.Paragraph();
                                datosTablaGestion.Font.SetStyle(iTextSharp.text.Font.BOLD);
                                datosTablaGestion.Font = FontFactory.GetFont("Arial", 8);
                                datosTablaGestion.Alignment = Element.ALIGN_LEFT;
                                datosTablaGestion.Add(new Chunk(gestiones[jj].ToString()));                                                                                       
                               
                                //if (gestiones[jj].ToString() == "2018") datosTablaGestion.Add(new Chunk(gestiones[jj].ToString(), str2018));
                                //if (gestiones[jj].ToString() == "2019") datosTablaGestion.Add(new Chunk(gestiones[jj].ToString(), str2019));
                                //if (gestiones[jj].ToString() == "2020") datosTablaGestion.Add(new Chunk(gestiones[jj].ToString(), str2020));
                                //if (gestiones[jj].ToString() == "2021") datosTablaGestion.Add(new Chunk(gestiones[jj].ToString(), str2021));
                                //if (gestiones[jj].ToString() == "2022") datosTablaGestion.Add(new Chunk(gestiones[jj].ToString(), str2022));
                                

                                PdfPCell cell8;
                                cell8 = new PdfPCell();
                                cell8.Colspan = 1;
                                cell8.AddElement(datosTablaGestion);
                                tablaGrid.AddCell(cell8);
                                sww = 0;
                            }

                            if (reg2.strGestion == gestiones[jj] && reg2.strMes == meses[j])
                            {
                                var datosTabla = new iTextSharp.text.Paragraph();
                                datosTabla.Font.SetStyle(iTextSharp.text.Font.BOLD);
                                datosTabla.Font = FontFactory.GetFont("Arial", 5);
                                datosTabla.Alignment = Element.ALIGN_RIGHT;
                                datosTabla.Add(reg2.decCantidad.ToString());
                                PdfPCell cell5;
                                cell5 = new PdfPCell();
                                cell5.Colspan = 1;
                                cell5.AddElement(datosTabla);
                                tablaGrid.AddCell(cell5);
                                swDF++;
                                
                                for (int k = 0; k < 12; k++)
                                {
                                    if (reg2.strGestion == _detFechaFin.Date.AddYears(-1).Year && reg2.strMes == meses[k]) //2021
                                    {
                                        valoresGE[0, k] = Math.Round(reg2.decCantidad,2);
                                    }
                                    if (reg2.strGestion == _detFechaFin.Date.Year && reg2.strMes == meses[k]) //2022
                                    {
                                        valoresGE[1, k] = Math.Round(reg2.decCantidad, 2);
                                    }
                                }
                            }

                            //if (sww == 12) sww = 1;

                        }
                        if (swDF == 0)
                        {
                            var datosTablaDF = new iTextSharp.text.Paragraph();
                            datosTablaDF.Font.SetStyle(iTextSharp.text.Font.BOLD);
                            datosTablaDF.Font = FontFactory.GetFont("Arial", 5);
                            datosTablaDF.Alignment = Element.ALIGN_LEFT;
                            datosTablaDF.Add(" ");
                            PdfPCell cell5DF;
                            cell5DF = new PdfPCell();
                            cell5DF.Colspan = 1;
                            cell5DF.AddElement(datosTablaDF);
                            tablaGrid.AddCell(cell5DF);
                        }
                        else
                        {
                            swDF = 0;
                        }
                    }
                
                }

                #region Tabla de Diferencia
                var titulosCabValGE = new iTextSharp.text.Paragraph();
                titulosCabValGE.Font.SetStyle(iTextSharp.text.Font.BOLD);
                titulosCabValGE.Font = FontFactory.GetFont("Arial", 5);
                titulosCabValGE.Alignment = Element.ALIGN_CENTER;
                titulosCabValGE.Add("INCREMENTO EN LA SUBVENCION");
                PdfPCell cellcabValGE;
                cellcabValGE = new PdfPCell();
                cellcabValGE.Colspan = 14;
                cellcabValGE.AddElement(titulosCabValGE);
                tablaGridValores.AddCell(cellcabValGE);

                string[] meseSplit = null;
                var titulosTablaValores = new iTextSharp.text.Paragraph();
                titulosTablaValores.Font.SetStyle(iTextSharp.text.Font.BOLD);
                titulosTablaValores.Font = FontFactory.GetFont("Arial", 5);
                titulosTablaValores.Alignment = Element.ALIGN_LEFT;
                titulosTablaValores.Add("");
                PdfPCell cell6Val;
                cell6Val = new PdfPCell();
                cell6Val.Colspan = 1;
                cell6Val.AddElement(titulosTablaValores);
                tablaGridValores.AddCell(cell6Val);
                titulosTablaValores.Clear();

                for (int kk = 0; kk < 12; kk++)
                {

                    meseSplit = meses[kk].Split('-');
                    titulosTablaValores.Add(meseSplit[1].Substring(0, 3));
                    PdfPCell cell6ValMes;
                    cell6ValMes = new PdfPCell();
                    cell6ValMes.Colspan = 1;
                    cell6ValMes.AddElement(titulosTablaValores);
                    tablaGridValores.AddCell(cell6ValMes);
                    titulosTablaValores.Clear();
                }

                titulosTablaValores.Add("TOTAL");
                cell6Val.Rowspan = 3;
                cell6Val.AddElement(titulosTablaValores);
                tablaGridValores.AddCell(cell6Val);
                titulosTablaValores.Clear();

                titulosTablaValores.Font.SetStyle(iTextSharp.text.Font.BOLD);
                titulosTablaValores.Font = FontFactory.GetFont("Arial", 5);
                titulosTablaValores.Alignment = Element.ALIGN_LEFT;
                titulosTablaValores.Add("Vol variación mes 2022 - 2021");

                cell6Val = new PdfPCell();
                cell6Val.Colspan = 1;
                cell6Val.AddElement(titulosTablaValores);
                tablaGridValores.AddCell(cell6Val);
                titulosTablaValores.Clear();
                decimal decDiferencia = 0;
                for (int kk = 0; kk < 12; kk++)
                {
                    if (kk+1 <= _detFechaFin.Date.Month-1)
                    {
                        decDiferencia = valoresGE[1, kk] - valoresGE[0, kk];
                        var datosTabla = new iTextSharp.text.Paragraph();
                        datosTabla.Font.SetStyle(iTextSharp.text.Font.BOLD);
                        datosTabla.Font = FontFactory.GetFont("Arial", 5);
                        datosTabla.Alignment = Element.ALIGN_RIGHT;
                        datosTabla.Add(decDiferencia.ToString());
                        PdfPCell cell5;
                        cell5 = new PdfPCell();
                        cell5.Colspan = 1;
                        cell5.AddElement(datosTabla);
                        tablaGridValores.AddCell(cell5);
                    }
                    else
                    {
                        var datosTabla = new iTextSharp.text.Paragraph();
                        datosTabla.Font.SetStyle(iTextSharp.text.Font.BOLD);
                        datosTabla.Font = FontFactory.GetFont("Arial", 5);
                        datosTabla.Alignment = Element.ALIGN_RIGHT;
                        datosTabla.Add("");
                        PdfPCell cell5;
                        cell5 = new PdfPCell();
                        cell5.Colspan = 1;
                        cell5.AddElement(datosTabla);
                        tablaGridValores.AddCell(cell5);
                    }             
                }

                titulosTablaValores.Font.SetStyle(iTextSharp.text.Font.BOLD);
                titulosTablaValores.Font = FontFactory.GetFont("Arial", 5);
                titulosTablaValores.Alignment = Element.ALIGN_LEFT;
                titulosTablaValores.Add("Incremento en la subención Sus");

                cell6Val = new PdfPCell();
                cell6Val.Colspan = 1;
                cell6Val.AddElement(titulosTablaValores);
                tablaGridValores.AddCell(cell6Val);
                titulosTablaValores.Clear();

                for (int kk = 0; kk < 12; kk++)
                {
                    if (kk + 1 <= _detFechaFin.Date.Month-1)
                    {
                        var datosTabla = new iTextSharp.text.Paragraph();
                        datosTabla.Font.SetStyle(iTextSharp.text.Font.BOLD);
                        datosTabla.Font = FontFactory.GetFont("Arial", 5);
                        datosTabla.Alignment = Element.ALIGN_RIGHT;
                        datosTabla.Add("4,90");
                        PdfPCell cell5;
                        cell5 = new PdfPCell();
                        cell5.Colspan = 1;
                        cell5.AddElement(datosTabla);
                        tablaGridValores.AddCell(cell5);
                    }
                    else {
                        var datosTabla = new iTextSharp.text.Paragraph();
                        datosTabla.Font.SetStyle(iTextSharp.text.Font.BOLD);
                        datosTabla.Font = FontFactory.GetFont("Arial", 5);
                        datosTabla.Alignment = Element.ALIGN_RIGHT;
                        datosTabla.Add("");
                        PdfPCell cell5;
                        cell5 = new PdfPCell();
                        cell5.Colspan = 1;
                        cell5.AddElement(datosTabla);
                        tablaGridValores.AddCell(cell5);
                    }
                }

                titulosTablaValores.Font.SetStyle(iTextSharp.text.Font.BOLD);
                titulosTablaValores.Font = FontFactory.GetFont("Arial", 5);
                titulosTablaValores.Alignment = Element.ALIGN_LEFT;
                titulosTablaValores.Add("Costo subvención GE Bs/Lt");

                cell6Val = new PdfPCell();
                cell6Val.Colspan = 1;
                cell6Val.AddElement(titulosTablaValores);
                tablaGridValores.AddCell(cell6Val);
                titulosTablaValores.Clear();
                double dbResultado = 0;
                double dbResultadoSum = 0;      
                for (int kk = 0; kk < 12; kk++)
                {
                    if (kk + 1 <= _detFechaFin.Date.Month-1)
                    {
                        decDiferencia = valoresGE[1, kk] - valoresGE[0, kk];
                        dbResultado = Decimal.ToDouble(decDiferencia) * 4.9;
                        dbResultadoSum = dbResultadoSum + dbResultado;
                        var datosTabla = new iTextSharp.text.Paragraph();
                        datosTabla.Font.SetStyle(iTextSharp.text.Font.BOLD);
                        datosTabla.Font = FontFactory.GetFont("Arial", 5);
                        datosTabla.Alignment = Element.ALIGN_RIGHT;
                        datosTabla.Add(Math.Round(dbResultado, 2).ToString("N", formato));
                        PdfPCell cell5;
                        cell5 = new PdfPCell();
                        cell5.Colspan = 1;
                        cell5.AddElement(datosTabla);
                        tablaGridValores.AddCell(cell5);
                    }
                    else 
                    {                        
                        var datosTabla = new iTextSharp.text.Paragraph();
                        datosTabla.Font.SetStyle(iTextSharp.text.Font.BOLD);
                        datosTabla.Font = FontFactory.GetFont("Arial", 5);
                        datosTabla.Alignment = Element.ALIGN_RIGHT;
                        datosTabla.Add("");
                        PdfPCell cell5;
                        cell5 = new PdfPCell();
                        cell5.Colspan = 1;
                        cell5.AddElement(datosTabla);
                        tablaGridValores.AddCell(cell5);
                    }                    
                }

                titulosTablaValores.Font.SetStyle(iTextSharp.text.Font.BOLD);
                titulosTablaValores.Font = FontFactory.GetFont("Arial", 5);
                titulosTablaValores.Alignment = Element.ALIGN_RIGHT;
                titulosTablaValores.Add(Math.Round(dbResultadoSum,2).ToString("N", formato));

                cell6Val = new PdfPCell();
                cell6Val.Colspan = 1;
                cell6Val.AddElement(titulosTablaValores);
                tablaGridValores.AddCell(cell6Val);
                titulosTablaValores.Clear();

                titulosTablaValores.Font.SetStyle(iTextSharp.text.Font.BOLD);
                titulosTablaValores.Font = FontFactory.GetFont("Arial", 5);
                titulosTablaValores.Alignment = Element.ALIGN_LEFT;
                titulosTablaValores.Add("Incremento en la subvención Bs");

                cell6Val = new PdfPCell();
                cell6Val.Colspan = 1;
                cell6Val.AddElement(titulosTablaValores);
                tablaGridValores.AddCell(cell6Val);
                titulosTablaValores.Clear();
                double dbResultadoDL = 0;
                double dbResultadoSumDL = 0;
                for (int kk = 0; kk < 12; kk++)
                {
                    if (kk + 1 <= _detFechaFin.Date.Month-1)
                    {
                        decDiferencia = valoresGE[1, kk] - valoresGE[0, kk];
                        dbResultado = Decimal.ToDouble(decDiferencia) * 4.9;
                        dbResultadoDL = dbResultado / 6.96;
                        dbResultadoSumDL = dbResultadoSumDL + dbResultadoDL;
                        var datosTabla = new iTextSharp.text.Paragraph();
                        datosTabla.Font.SetStyle(iTextSharp.text.Font.BOLD);
                        datosTabla.Font = FontFactory.GetFont("Arial", 5);
                        datosTabla.Alignment = Element.ALIGN_RIGHT;
                        datosTabla.Add(Math.Round(dbResultadoDL, 2).ToString("N", formato));
                        PdfPCell cell5;
                        cell5 = new PdfPCell();
                        cell5.Colspan = 1;
                        cell5.AddElement(datosTabla);
                        tablaGridValores.AddCell(cell5);
                    }
                    else
                    {
                        var datosTabla = new iTextSharp.text.Paragraph();
                        datosTabla.Font.SetStyle(iTextSharp.text.Font.BOLD);
                        datosTabla.Font = FontFactory.GetFont("Arial", 5);
                        datosTabla.Alignment = Element.ALIGN_RIGHT;
                        datosTabla.Add("");
                        PdfPCell cell5;
                        cell5 = new PdfPCell();
                        cell5.Colspan = 1;
                        cell5.AddElement(datosTabla);
                        tablaGridValores.AddCell(cell5);
                    }
                }

                titulosTablaValores.Font.SetStyle(iTextSharp.text.Font.BOLD);
                titulosTablaValores.Font = FontFactory.GetFont("Arial", 5);
                titulosTablaValores.Alignment = Element.ALIGN_RIGHT;
                titulosTablaValores.Add(Math.Round(dbResultadoSumDL, 2).ToString("N", formato));

                cell6Val = new PdfPCell();
                cell6Val.Colspan = 1;
                cell6Val.AddElement(titulosTablaValores);
                tablaGridValores.AddCell(cell6Val);
                titulosTablaValores.Clear();
              
                #endregion
            }
            PdfPCell cell7;
            cell7 = new PdfPCell();
            cell7.Colspan = 2;
            cell7.AddElement(tablaGrid);
            tablaPrincipal.AddCell(cell7);

            PdfPCell cellValores;
            cellValores = new PdfPCell();
            cellValores.Colspan = 2;
            cellValores.AddElement(tablaGridValores);
            tablaPrincipal.AddCell(cellValores);
            
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////                   

            cell3 = new PdfPCell();
            cell3.Colspan = 2;
            cell3.AddElement(img2);

            tablaPrincipal.AddCell(cell3);
            PdfPTable tablaGrid2 = new PdfPTable(13);
            var widthsG2 = new[] { 35f, 35f, 42f, 35f, 35f, 35f, 35f, 35f, 42f, 50f, 50f, 50f, 50f };
            tablaGrid2.TotalWidth = 485f;
            tablaGrid2.LockedWidth = true;
            tablaGrid2.SetWidths(widthsG2);

            PdfPTable tablaGridValoresDO = new PdfPTable(14);
            var widthsGValoresDO = new[] { 290f, 100f, 100f, 100f, 100f, 100f, 100f, 100f, 100f, 100f, 100f, 100f, 100f, 150f };
            tablaGridValoresDO.TotalWidth = 485f;
            tablaGridValoresDO.LockedWidth = true;
            tablaGridValoresDO.SetWidths(widthsGValoresDO);

            var titulosCabXDO = new iTextSharp.text.Paragraph();
            titulosCabXDO.Font.SetStyle(iTextSharp.text.Font.BOLD);
            titulosCabXDO.Font = FontFactory.GetFont("Arial", 5);
            titulosCabXDO.Alignment = Element.ALIGN_CENTER;
            titulosCabXDO.Add("EXPRESADO EN (m3)");
            PdfPCell cellcabXDO;
            cellcabXDO = new PdfPCell();
            cellcabXDO.Colspan = 13;
            cellcabXDO.AddElement(titulosCabXDO);
            tablaGrid2.AddCell(cellcabXDO);


            if (lstAcumulado1 != null && lstAcumulado1.Count > 0)
            {
                var titulosTablaB = new iTextSharp.text.Paragraph();
                titulosTablaB.Font.SetStyle(iTextSharp.text.Font.BOLD);
                titulosTablaB.Font = FontFactory.GetFont("Arial", 5);
                titulosTablaB.Alignment = Element.ALIGN_LEFT;
                titulosTablaB.Add("GESTION");
                PdfPCell cell6;
                cell6 = new PdfPCell();
                cell6.Colspan = 1;
                cell6.AddElement(titulosTablaB);
                tablaGrid2.AddCell(cell6);
                var lstAcumulado11 = lstAcumulado1.OrderBy(x => x.FECHA_REPORTE);


                var titulosTabla = new iTextSharp.text.Paragraph();
                titulosTabla.Font.SetStyle(iTextSharp.text.Font.BOLD);
                titulosTabla.Font = FontFactory.GetFont("Arial", 5);
                titulosTabla.Alignment = Element.ALIGN_LEFT;
                titulosTabla.Add("ENERO");
                PdfPCell cell4;
                cell4 = new PdfPCell();
                cell4.Colspan = 1;
                cell4.AddElement(titulosTabla);
                tablaGrid2.AddCell(cell4);
                titulosTabla.Clear();


                titulosTabla.Font.SetStyle(iTextSharp.text.Font.BOLD);
                titulosTabla.Font = FontFactory.GetFont("Arial", 5);
                titulosTabla.Alignment = Element.ALIGN_LEFT;
                titulosTabla.Add("FEBRERO");
                cell4 = new PdfPCell();
                cell4.Colspan = 1;
                cell4.AddElement(titulosTabla);
                tablaGrid2.AddCell(cell4);
                titulosTabla.Clear();

                titulosTabla.Font.SetStyle(iTextSharp.text.Font.BOLD);
                titulosTabla.Font = FontFactory.GetFont("Arial", 5);
                titulosTabla.Alignment = Element.ALIGN_LEFT;
                titulosTabla.Add("MARZO");
                cell4 = new PdfPCell();
                cell4.Colspan = 1;
                cell4.AddElement(titulosTabla);
                tablaGrid2.AddCell(cell4);
                titulosTabla.Clear();

                titulosTabla.Font.SetStyle(iTextSharp.text.Font.BOLD);
                titulosTabla.Font = FontFactory.GetFont("Arial", 5);
                titulosTabla.Alignment = Element.ALIGN_LEFT;
                titulosTabla.Add("ABRIL");
                cell4 = new PdfPCell();
                cell4.Colspan = 1;
                cell4.AddElement(titulosTabla);
                tablaGrid2.AddCell(cell4);
                titulosTabla.Clear();

                titulosTabla.Font.SetStyle(iTextSharp.text.Font.BOLD);
                titulosTabla.Font = FontFactory.GetFont("Arial", 5);
                titulosTabla.Alignment = Element.ALIGN_LEFT;
                titulosTabla.Add("MAYO");
                cell4 = new PdfPCell();
                cell4.Colspan = 1;
                cell4.AddElement(titulosTabla);
                tablaGrid2.AddCell(cell4);
                titulosTabla.Clear();

                titulosTabla.Font.SetStyle(iTextSharp.text.Font.BOLD);
                titulosTabla.Font = FontFactory.GetFont("Arial", 5);
                titulosTabla.Alignment = Element.ALIGN_LEFT;
                titulosTabla.Add("JUNIO");
                cell4 = new PdfPCell();
                cell4.Colspan = 1;
                cell4.AddElement(titulosTabla);
                tablaGrid2.AddCell(cell4);
                titulosTabla.Clear();

                titulosTabla.Font.SetStyle(iTextSharp.text.Font.BOLD);
                titulosTabla.Font = FontFactory.GetFont("Arial", 5);
                titulosTabla.Alignment = Element.ALIGN_LEFT;
                titulosTabla.Add("JULIO");
                cell4 = new PdfPCell();
                cell4.Colspan = 1;
                cell4.AddElement(titulosTabla);
                tablaGrid2.AddCell(cell4);
                titulosTabla.Clear();

                titulosTabla.Font.SetStyle(iTextSharp.text.Font.BOLD);
                titulosTabla.Font = FontFactory.GetFont("Arial", 5);
                titulosTabla.Alignment = Element.ALIGN_LEFT;
                titulosTabla.Add("AGOSTO");
                cell4 = new PdfPCell();
                cell4.Colspan = 1;
                cell4.AddElement(titulosTabla);
                tablaGrid2.AddCell(cell4);
                titulosTabla.Clear();

                titulosTabla.Font.SetStyle(iTextSharp.text.Font.BOLD);
                titulosTabla.Font = FontFactory.GetFont("Arial", 5);
                titulosTabla.Alignment = Element.ALIGN_LEFT;
                titulosTabla.Add("SEPTIEMBRE");
                cell4 = new PdfPCell();
                cell4.Colspan = 1;
                cell4.AddElement(titulosTabla);
                tablaGrid2.AddCell(cell4);
                titulosTabla.Clear();

                titulosTabla.Font.SetStyle(iTextSharp.text.Font.BOLD);
                titulosTabla.Font = FontFactory.GetFont("Arial", 5);
                titulosTabla.Alignment = Element.ALIGN_LEFT;
                titulosTabla.Add("OCTUBRE");
                cell4 = new PdfPCell();
                cell4.Colspan = 1;
                cell4.AddElement(titulosTabla);
                tablaGrid2.AddCell(cell4);
                titulosTabla.Clear();

                titulosTabla.Font.SetStyle(iTextSharp.text.Font.BOLD);
                titulosTabla.Font = FontFactory.GetFont("Arial", 5);
                titulosTabla.Alignment = Element.ALIGN_LEFT;
                titulosTabla.Add("NOVIEMBRE");
                cell4 = new PdfPCell();
                cell4.Colspan = 1;
                cell4.AddElement(titulosTabla);
                tablaGrid2.AddCell(cell4);
                titulosTabla.Clear();

                titulosTabla.Font.SetStyle(iTextSharp.text.Font.BOLD);
                titulosTabla.Font = FontFactory.GetFont("Arial", 5);
                titulosTabla.Alignment = Element.ALIGN_LEFT;
                titulosTabla.Add("DICIEMBRE");
                cell4 = new PdfPCell();
                cell4.Colspan = 1;
                cell4.AddElement(titulosTabla);
                tablaGrid2.AddCell(cell4);
                titulosTabla.Clear();

                string[] meses = { "01-ENERO", "02-FEBRERO", "03-MARZO", "04-ABRIL", "05-MAYO", "06-JUNIO", "07-JULIO", "08-AGOSTO", "09-SEPTIEMBRE", "10-OCTUBRE", "11-NOVIEMBRE", "12-DICIEMBRE" };
                //int[] gestiones = { 2018, 2019, 2020, 2021, 2022 };
                decimal[,] valoresDO = new decimal[2, 12];
                int sww = 1;
                int swDF = 0;      
                bool blElse = true;
                for (int jj = 0; jj < gestiones.Count(); jj++)
                {

                    sww = 1;
                    for (int j = 0; j < 12; j++)
                    {

                        foreach (var reg2 in lstAcumulado2.OrderBy(x => x.strMes).ToList())
                        {

                            if (sww == 1)
                            {
                                //var str2018 = FontFactory.GetFont("trebuchet ms", 8);
                                //str2018.SetColor(51, 153, 255);
                                //var str2019 = FontFactory.GetFont("trebuchet ms", 8);
                                //str2019.SetColor(255, 153, 51);
                                //var str2020 = FontFactory.GetFont("trebuchet ms", 8);
                                //str2020.SetColor(255, 0, 0);
                                //var str2021 = FontFactory.GetFont("trebuchet ms", 8);
                                //str2021.SetColor(0, 73, 156);
                                //var str2022 = FontFactory.GetFont("trebuchet ms", 8);
                                //str2022.SetColor(96, 96, 96);
                                var datosTablaGestion = new iTextSharp.text.Paragraph();
                                datosTablaGestion.Font.SetStyle(iTextSharp.text.Font.BOLD);
                                datosTablaGestion.Font = FontFactory.GetFont("Arial", 8);
                                datosTablaGestion.Alignment = Element.ALIGN_LEFT;                                      
                                datosTablaGestion.Add(new Chunk(gestiones[jj].ToString()));   
                                //if (gestiones[jj].ToString() == "2018") datosTablaGestion.Add(new Chunk(gestiones[jj].ToString(), str2018));
                                //if (gestiones[jj].ToString() == "2019") datosTablaGestion.Add(new Chunk(gestiones[jj].ToString(), str2019));
                                //if (gestiones[jj].ToString() == "2020") datosTablaGestion.Add(new Chunk(gestiones[jj].ToString(), str2020));
                                //if (gestiones[jj].ToString() == "2021") datosTablaGestion.Add(new Chunk(gestiones[jj].ToString(), str2021));
                                //if (gestiones[jj].ToString() == "2022") datosTablaGestion.Add(new Chunk(gestiones[jj].ToString(), str2022));
                                PdfPCell cell8;
                                cell8 = new PdfPCell();
                                cell8.Colspan = 1;
                                cell8.AddElement(datosTablaGestion);
                                tablaGrid2.AddCell(cell8);
                                sww = 0;
                            }

                            if (reg2.strGestion == gestiones[jj] && reg2.strMes == meses[j])
                            {
                                var datosTabla = new iTextSharp.text.Paragraph();
                                datosTabla.Font.SetStyle(iTextSharp.text.Font.BOLD);
                                datosTabla.Font = FontFactory.GetFont("Arial", 5);
                                datosTabla.Alignment = Element.ALIGN_RIGHT;
                                datosTabla.Add(reg2.decCantidad.ToString());
                                PdfPCell cell5;
                                cell5 = new PdfPCell();
                                cell5.Colspan = 1;
                                cell5.AddElement(datosTabla);
                                tablaGrid2.AddCell(cell5);
                                swDF++;

                                for (int k = 0; k < 12; k++)
                                {
                                    if (reg2.strGestion == _detFechaFin.Date.AddYears(-1).Year && reg2.strMes == meses[k]) //2021
                                    {
                                        valoresDO[0, k] = Math.Round(reg2.decCantidad, 2);
                                    }
                                    if (reg2.strGestion == _detFechaFin.Date.Year && reg2.strMes == meses[k]) //2022
                                    {
                                        valoresDO[1, k] = Math.Round(reg2.decCantidad, 2);
                                    }
                                }
                            }
                            
                            //if (sww == 12) sww = 1;
                        }
                        if (swDF == 0)
                        {
                            var datosTablaDF = new iTextSharp.text.Paragraph();
                            datosTablaDF.Font.SetStyle(iTextSharp.text.Font.BOLD);
                            datosTablaDF.Font = FontFactory.GetFont("Arial", 5);
                            datosTablaDF.Alignment = Element.ALIGN_LEFT;
                            datosTablaDF.Add(" ");
                            PdfPCell cell5DF;
                            cell5DF = new PdfPCell();
                            cell5DF.Colspan = 1;
                            cell5DF.AddElement(datosTablaDF);
                            tablaGrid2.AddCell(cell5DF);

                        }
                        else
                        {
                            swDF = 0;
                        }
                    }

                    //jj++;
                }
                #region Tabla de Diferencia

                var titulosCabValDO = new iTextSharp.text.Paragraph();
                titulosCabValDO.Font.SetStyle(iTextSharp.text.Font.BOLD);
                titulosCabValDO.Font = FontFactory.GetFont("Arial", 5);
                titulosCabValDO.Alignment = Element.ALIGN_CENTER;
                titulosCabValDO.Add("INCREMENTO EN LA SUBVENCION");
                PdfPCell cellcabValDO;
                cellcabValDO = new PdfPCell();
                cellcabValDO.Colspan = 14;
                cellcabValDO.AddElement(titulosCabValDO);
                tablaGridValoresDO.AddCell(cellcabValDO);

                string[] meseSplitDO = null;
                var titulosTablaValoresDO = new iTextSharp.text.Paragraph();
                titulosTablaValoresDO.Font.SetStyle(iTextSharp.text.Font.BOLD);
                titulosTablaValoresDO.Font = FontFactory.GetFont("Arial", 5);
                titulosTablaValoresDO.Alignment = Element.ALIGN_LEFT;
                titulosTablaValoresDO.Add("");
                PdfPCell cell6ValDO;
                cell6ValDO = new PdfPCell();
                cell6ValDO.Colspan = 1;
                cell6ValDO.AddElement(titulosTablaValoresDO);
                tablaGridValoresDO.AddCell(cell6ValDO);
                titulosTablaValoresDO.Clear();

                for (int kk = 0; kk < 12; kk++)
                {

                    meseSplitDO = meses[kk].Split('-');
                    titulosTablaValoresDO.Add(meseSplitDO[1].Substring(0, 3));
                    PdfPCell cell6ValMes;
                    cell6ValMes = new PdfPCell();
                    cell6ValMes.Colspan = 1;
                    cell6ValMes.AddElement(titulosTablaValoresDO);
                    tablaGridValoresDO.AddCell(cell6ValMes);
                    titulosTablaValoresDO.Clear();
                }

                titulosTablaValoresDO.Add("TOTAL");
                cell6ValDO.Rowspan = 3;
                cell6ValDO.AddElement(titulosTablaValoresDO);
                tablaGridValoresDO.AddCell(cell6ValDO);
                titulosTablaValoresDO.Clear();

                titulosTablaValoresDO.Font.SetStyle(iTextSharp.text.Font.BOLD);
                titulosTablaValoresDO.Font = FontFactory.GetFont("Arial", 5);
                titulosTablaValoresDO.Alignment = Element.ALIGN_LEFT;
                titulosTablaValoresDO.Add("Vol variación mes 2022 - 2021");

                cell6ValDO = new PdfPCell();
                cell6ValDO.Colspan = 1;
                cell6ValDO.AddElement(titulosTablaValoresDO);
                tablaGridValoresDO.AddCell(cell6ValDO);
                titulosTablaValoresDO.Clear();
                decimal decDiferencia = 0;
                for (int kk = 0; kk < 12; kk++)
                {
                    if (kk + 1 <= _detFechaFin.Date.Month-1)
                    {
                        decDiferencia = valoresDO[1, kk] - valoresDO[0, kk];
                        var datosTabla = new iTextSharp.text.Paragraph();
                        datosTabla.Font.SetStyle(iTextSharp.text.Font.BOLD);
                        datosTabla.Font = FontFactory.GetFont("Arial", 5);
                        datosTabla.Alignment = Element.ALIGN_RIGHT;
                        datosTabla.Add(decDiferencia.ToString());
                        PdfPCell cell5;
                        cell5 = new PdfPCell();
                        cell5.Colspan = 1;
                        cell5.AddElement(datosTabla);
                        tablaGridValoresDO.AddCell(cell5);
                    }
                    else
                    {                        
                        var datosTabla = new iTextSharp.text.Paragraph();
                        datosTabla.Font.SetStyle(iTextSharp.text.Font.BOLD);
                        datosTabla.Font = FontFactory.GetFont("Arial", 5);
                        datosTabla.Alignment = Element.ALIGN_RIGHT;
                        datosTabla.Add("");
                        PdfPCell cell5;
                        cell5 = new PdfPCell();
                        cell5.Colspan = 1;
                        cell5.AddElement(datosTabla);
                        tablaGridValoresDO.AddCell(cell5);
                    }
                }

                titulosTablaValoresDO.Font.SetStyle(iTextSharp.text.Font.BOLD);
                titulosTablaValoresDO.Font = FontFactory.GetFont("Arial", 5);
                titulosTablaValoresDO.Alignment = Element.ALIGN_LEFT;
                titulosTablaValoresDO.Add("Incremento en la subención Sus");

                cell6ValDO = new PdfPCell();
                cell6ValDO.Colspan = 1;
                cell6ValDO.AddElement(titulosTablaValoresDO);
                tablaGridValoresDO.AddCell(cell6ValDO);
                titulosTablaValoresDO.Clear();

                for (int kk = 0; kk < 12; kk++)
                {
                    if (kk + 1 <= _detFechaFin.Date.Month-1)
                    {
                        var datosTabla = new iTextSharp.text.Paragraph();
                        datosTabla.Font.SetStyle(iTextSharp.text.Font.BOLD);
                        datosTabla.Font = FontFactory.GetFont("Arial", 5);
                        datosTabla.Alignment = Element.ALIGN_RIGHT;
                        datosTabla.Add("4,90");
                        PdfPCell cell5;
                        cell5 = new PdfPCell();
                        cell5.Colspan = 1;
                        cell5.AddElement(datosTabla);
                        tablaGridValoresDO.AddCell(cell5);
                    }
                    else {
                        var datosTabla = new iTextSharp.text.Paragraph();
                        datosTabla.Font.SetStyle(iTextSharp.text.Font.BOLD);
                        datosTabla.Font = FontFactory.GetFont("Arial", 5);
                        datosTabla.Alignment = Element.ALIGN_RIGHT;
                        datosTabla.Add("");
                        PdfPCell cell5;
                        cell5 = new PdfPCell();
                        cell5.Colspan = 1;
                        cell5.AddElement(datosTabla);
                        tablaGridValoresDO.AddCell(cell5);
                    }
                }

                titulosTablaValoresDO.Font.SetStyle(iTextSharp.text.Font.BOLD);
                titulosTablaValoresDO.Font = FontFactory.GetFont("Arial", 5);
                titulosTablaValoresDO.Alignment = Element.ALIGN_LEFT;
                titulosTablaValoresDO.Add("Costo subvención DO Bs/Lt");

                cell6ValDO = new PdfPCell();
                cell6ValDO.Colspan = 1;
                cell6ValDO.AddElement(titulosTablaValoresDO);
                tablaGridValoresDO.AddCell(cell6ValDO);
                titulosTablaValoresDO.Clear();
                double dbResultado = 0;
                double dbResultadoSum = 0;
                for (int kk = 0; kk < 12; kk++)
                {
                    if (kk + 1 <= _detFechaFin.Date.Month-1)
                    {
                        decDiferencia = valoresDO[1, kk] - valoresDO[0, kk];
                        dbResultado = Decimal.ToDouble(decDiferencia) * 4.9;
                        dbResultadoSum = dbResultadoSum + dbResultado;
                        var datosTabla = new iTextSharp.text.Paragraph();
                        datosTabla.Font.SetStyle(iTextSharp.text.Font.BOLD);
                        datosTabla.Font = FontFactory.GetFont("Arial", 5);
                        datosTabla.Alignment = Element.ALIGN_RIGHT;
                        datosTabla.Add(Math.Round(dbResultado, 2).ToString("N", formato));
                        PdfPCell cell5;
                        cell5 = new PdfPCell();
                        cell5.Colspan = 1;
                        cell5.AddElement(datosTabla);
                        tablaGridValoresDO.AddCell(cell5);
                    }
                    else {
                        var datosTabla = new iTextSharp.text.Paragraph();
                        datosTabla.Font.SetStyle(iTextSharp.text.Font.BOLD);
                        datosTabla.Font = FontFactory.GetFont("Arial", 5);
                        datosTabla.Alignment = Element.ALIGN_RIGHT;
                        datosTabla.Add("");
                        PdfPCell cell5;
                        cell5 = new PdfPCell();
                        cell5.Colspan = 1;
                        cell5.AddElement(datosTabla);
                        tablaGridValoresDO.AddCell(cell5);
                    }
                }

                titulosTablaValoresDO.Font.SetStyle(iTextSharp.text.Font.BOLD);
                titulosTablaValoresDO.Font = FontFactory.GetFont("Arial", 5);
                titulosTablaValoresDO.Alignment = Element.ALIGN_RIGHT;
                titulosTablaValoresDO.Add(Math.Round(dbResultadoSum,2).ToString());

                cell6ValDO = new PdfPCell();
                cell6ValDO.Colspan = 1;
                cell6ValDO.AddElement(titulosTablaValoresDO);
                tablaGridValoresDO.AddCell(cell6ValDO);
                titulosTablaValoresDO.Clear();

                titulosTablaValoresDO.Font.SetStyle(iTextSharp.text.Font.BOLD);
                titulosTablaValoresDO.Font = FontFactory.GetFont("Arial", 5);
                titulosTablaValoresDO.Alignment = Element.ALIGN_LEFT;
                titulosTablaValoresDO.Add("Incremento en la subvención Bs");

                cell6ValDO = new PdfPCell();
                cell6ValDO.Colspan = 1;
                cell6ValDO.AddElement(titulosTablaValoresDO);
                tablaGridValoresDO.AddCell(cell6ValDO);
                titulosTablaValoresDO.Clear();
                double dbResultadoDL = 0;
                double dbResultadoSumDL = 0;
                for (int kk = 0; kk < 12; kk++)
                {
                    if (kk + 1 <= _detFechaFin.Date.Month-1)
                    {
                        decDiferencia = valoresDO[1, kk] - valoresDO[0, kk];
                        dbResultado = Decimal.ToDouble(decDiferencia) * 4.9;
                        dbResultadoDL = dbResultado / 6.96;
                        dbResultadoSumDL = dbResultadoSumDL + dbResultadoDL;
                        var datosTabla = new iTextSharp.text.Paragraph();
                        datosTabla.Font.SetStyle(iTextSharp.text.Font.BOLD);
                        datosTabla.Font = FontFactory.GetFont("Arial", 5);
                        datosTabla.Alignment = Element.ALIGN_RIGHT;
                        datosTabla.Add(Math.Round(dbResultadoDL, 2).ToString("N", formato));
                        PdfPCell cell5;
                        cell5 = new PdfPCell();
                        cell5.Colspan = 1;
                        cell5.AddElement(datosTabla);
                        tablaGridValoresDO.AddCell(cell5);
                    }
                    else {
                        var datosTabla = new iTextSharp.text.Paragraph();
                        datosTabla.Font.SetStyle(iTextSharp.text.Font.BOLD);
                        datosTabla.Font = FontFactory.GetFont("Arial", 5);
                        datosTabla.Alignment = Element.ALIGN_RIGHT;
                        datosTabla.Add(Math.Round(dbResultadoDL, 2).ToString("N", formato));
                        PdfPCell cell5;
                        cell5 = new PdfPCell();
                        cell5.Colspan = 1;
                        cell5.AddElement(datosTabla);
                        tablaGridValoresDO.AddCell(cell5);
                    }
                }

                titulosTablaValoresDO.Font.SetStyle(iTextSharp.text.Font.BOLD);
                titulosTablaValoresDO.Font = FontFactory.GetFont("Arial", 5);
                titulosTablaValoresDO.Alignment = Element.ALIGN_RIGHT;
                titulosTablaValoresDO.Add(Math.Round(dbResultadoSumDL, 2).ToString("N", formato));

                cell6ValDO = new PdfPCell();
                cell6ValDO.Colspan = 1;
                cell6ValDO.AddElement(titulosTablaValoresDO);
                tablaGridValoresDO.AddCell(cell6ValDO);
                titulosTablaValoresDO.Clear();

                #endregion
            }
            PdfPCell cell7DO;
            cell7DO = new PdfPCell();
            cell7DO.Colspan = 2;
            cell7DO.AddElement(tablaGrid2);
            tablaPrincipal.AddCell(cell7DO);

            PdfPCell cellValoresDO;
            cellValoresDO = new PdfPCell();
            cellValoresDO.Colspan = 2;
            cellValoresDO.AddElement(tablaGridValoresDO);
            tablaPrincipal.AddCell(cellValoresDO);

            Doc.Add(tablaPrincipal);

        

            Doc.Close();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=FichaTecnicoES.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Write(Doc);
            Response.End();
        }

       

        #endregion

    }

}