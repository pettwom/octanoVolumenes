
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
    public partial class CtrRepConsumo : UserControl
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
                        MCargaParametricas();
                        MConfiguraInicio();
                    }
                    else
                    {
                        //todo No es carga inicial: no implementado
                    }

                    if (pvgDatos.DataSource == null && Session[CVariablesSesion.GrillaCabecera] != null)
                    {
                        pvgDatos.DataSource = Session[CVariablesSesion.GrillaCabecera];
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
                        var strIdActividad = ((int)cmbActividad.Value == 1) ? "1|2|15" : "50";
                        var strIdentificador = (txtIdentificador.Text.Trim().ToUpper() == "") ? "0" : txtIdentificador.Text.Trim().ToUpper();
                        var strTipoDoc = cmbTipoDocEf.Value.ToString();
                        
                        var lisIdTipoConsumidor = "";
                        foreach (var item in (List<O_UNIDADES_REF_CTY>)Session[CVariablesSesion.ComboTipoConsumidor])
                        {
                            if (item.ID_PARAMETRICA_GLOBAL > 0)
                            {
                                lisIdTipoConsumidor = lisIdTipoConsumidor + item.ID_PARAMETRICA_GLOBAL + "|";
                            }
                            else
                            {
                                //A impementar
                            }
                        }
                        var strIdTipoConsumidor = ((int)cmbTipoConsumidor.Value == 0) ? lisIdTipoConsumidor : cmbTipoConsumidor.Value.ToString();

                        MCargaGrilla(strFechaIni, strFechaFin, strIdActividad, strIdentificador, strIdTipoConsumidor, strTipoDoc);
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

                    if (chkAccion.Checked == true)
                    {                        
                        MConfiguraInicio();
                        cmbTipoConsumidor.Enabled = false;
                        cmbActividad.Enabled = false;
                        cmbTipoDocEf.Enabled = false;
                        cmbActividad.SelectedIndex = 0;                        
                    }
                    else
                    {
                        MConfiguraInicio();
                        cmbTipoConsumidor.Enabled = true;
                        cmbActividad.Enabled = true;
                        cmbTipoDocEf.Enabled = true;
                        cmbActividad.SelectedIndex = 0;
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
        /// METODO. MCargaParametricas
        /// </summary>
        private void MCargaParametricas()
        {
            #region Obtener Perfiles de usuarios

            var objPerfil = _clientAdminHydro.Get<EResultadoLista>("/EReporteUsuariosPerfiles/" + CParametrosHydro.strCredencialHydroAdmin + "/" + Convert.ToDecimal(CAppSettings.idAplicacion) + "/" + Session[CVariablesSesion.IdUsuario] + "?format=json");
            if (objPerfil.decCodigo == 1)
            {
                Session[CVariablesSesion.ListaPerfiles] = objPerfil.oResultado;
            }
            else
            {
                Session.Remove(CVariablesSesion.ListaPerfiles);
            }

            #endregion

            #region Tipo de Transaccion

            var lstObjTransaccion = _client.Post<EResultadoTipoTransaccion>(CAppSettings.svc_lista_tipos_transaccion,
           new
           {
               strCredencial = CParametrosHydro.strCredencialFuncionario,
               decFiltro = 1,
               strParametro1 = "",
               strParametro2 = ""
           });
            if (lstObjTransaccion.IntCodigo == 1)
            {
                lstObjTransaccion.OResultado = lstObjTransaccion.OResultado.OrderBy(x => x.ID_TIPO_TRANSACCION_PADRE).ThenBy(x => x.NOMBRE).ToList();
                var objTipoTrans = new O_LISTA_TIPO_TRANSACCION_CTY();
                objTipoTrans.ID_TIPO_TRANSACCION = -99999;
                objTipoTrans.ID_TIPO_TRANSACCION_PADRE = -99999;
                objTipoTrans.ID_ANTERIOR = -99999;
                objTipoTrans.APP_ID_USUARIO = -99999;
                objTipoTrans.APP_FECHA_REGISTRO = 19000101000000;
                objTipoTrans.CODIGO = "DOCTODO";
                objTipoTrans.NOMBRE = "TODO";
                objTipoTrans.OBSERVACIONES = "";
                objTipoTrans.APP_ID_HISTORICO = -99999;
                objTipoTrans.ORDEN = -99999;
                lstObjTransaccion.OResultado.Add(objTipoTrans);
                
                objTipoTrans = new O_LISTA_TIPO_TRANSACCION_CTY();
                objTipoTrans.ID_TIPO_TRANSACCION = 0;
                objTipoTrans.ID_TIPO_TRANSACCION_PADRE = 0;
                objTipoTrans.ID_ANTERIOR = 0;
                objTipoTrans.APP_ID_USUARIO = -99999;
                objTipoTrans.APP_FECHA_REGISTRO = 19000101000000;
                objTipoTrans.CODIGO = "DOCP";
                objTipoTrans.NOMBRE = "DOC. PREDEFINIDO";
                objTipoTrans.OBSERVACIONES = "";
                objTipoTrans.APP_ID_HISTORICO = -99999;
                objTipoTrans.ORDEN = 1;
                lstObjTransaccion.OResultado.Add(objTipoTrans);
                Session[CVariablesSesion.ListaTransaccion] = lstObjTransaccion.OResultado;
            }

            #endregion
        }

        /// <summary>
        /// METODO. MConfiguraInicio
        /// </summary>
        private void MConfiguraInicio()
        {
            detFechaIni.Date = DateTime.Now.AddMonths(-1);
            detFechaFin.Date = DateTime.Now.AddMonths(-1);

            cmbTipoDocEf.DataSource = Session[CVariablesSesion.ListaTransaccion];
            cmbTipoDocEf.DataBind();
            cmbTipoDocEf.SelectedItem = cmbTipoDocEf.Items.FindByValue("0");

            var objPerfil = Session[CVariablesSesion.ListaPerfiles] as List<O_USUARIO_PERFIL_CTY>;
            if (objPerfil != null)
            {
                //objPerfil = objPerfil.Where(x => x.NOMBRE_PERFIL.Contains("ANH")).ToList();
                var objPerfilUsuDir = objPerfil.Where(x => x.NOMBRE_PERFIL.Contains("USUARIO DIRECTO")).ToList().Count();
                var objPerfilAdm = objPerfil.Where(x => x.NOMBRE_PERFIL.Contains("ADM")).ToList().Count();
                string strgCredencial = "";
                string strgCodRefineria = "TCONS";
                string strgFiltroTipoConsulta = "TIPOCONSUMIDOR";
                EPResultado objResultado = _consulta.listaUnidadesAreaRefineria(strgCredencial, strgCodRefineria, strgFiltroTipoConsulta);
                List<O_UNIDADES_REF_CTY> listUnidadesOrgaRefinerias = (List<O_UNIDADES_REF_CTY>)objResultado.oResultado;

                if (objPerfilAdm > 0)
                {
                    cmbActividad.Enabled = true;
                    cmbTipoDocEf.Enabled = true;
                    O_UNIDADES_REF_CTY objTipoConsumidor = new O_UNIDADES_REF_CTY();
                    objTipoConsumidor.ID_PARAMETRICA_GLOBAL = 0;
                    objTipoConsumidor.NOM_AREA_REFIN = "TODO";
                    listUnidadesOrgaRefinerias.Add(objTipoConsumidor);
                    Session[CVariablesSesion.ComboTipoConsumidor] = listUnidadesOrgaRefinerias;
                    cmbTipoConsumidor.DataSource = Session[CVariablesSesion.ComboTipoConsumidor];
                    cmbTipoConsumidor.DataBind();
                    cmbTipoConsumidor.SelectedItem = cmbTipoConsumidor.Items.FindByText("TODO");
                    chkAccion.Visible = true;
                }
                else if (objPerfilUsuDir > 0)
                {
                    cmbActividad.Enabled = false;
                    cmbTipoConsumidor.Enabled = false;
                    cmbTipoDocEf.Enabled = false;
                    List<O_UNIDADES_REF_CTY> listTipoConsumidor = listUnidadesOrgaRefinerias.Where(l => l.NOM_AREA_REFIN.Contains("USUARIO DIRECTO")).ToList();
                    O_UNIDADES_REF_CTY objTipoConsumidor = new O_UNIDADES_REF_CTY();
                    objTipoConsumidor.ID_PARAMETRICA_GLOBAL = listTipoConsumidor[0].ID_PARAMETRICA_GLOBAL;
                    objTipoConsumidor.NOM_AREA_REFIN = "TODO";
                    listTipoConsumidor.Add(objTipoConsumidor);

                    Session[CVariablesSesion.ComboTipoConsumidor] = listTipoConsumidor;
                    cmbTipoConsumidor.DataSource = Session[CVariablesSesion.ComboTipoConsumidor];
                    cmbTipoConsumidor.DataBind();
                    cmbTipoConsumidor.SelectedItem = cmbTipoConsumidor.Items.FindByText("TODO");
                }
                else
                {
                    cmbActividad.Enabled = true;
                    cmbTipoDocEf.Enabled = false;
                    cmbTipoConsumidor.Enabled = true;
                    O_UNIDADES_REF_CTY objTipoConsumidor = new O_UNIDADES_REF_CTY();
                    objTipoConsumidor.ID_PARAMETRICA_GLOBAL = 0;
                    objTipoConsumidor.NOM_AREA_REFIN = "TODO";
                    listUnidadesOrgaRefinerias.Add(objTipoConsumidor);
                    Session[CVariablesSesion.ComboTipoConsumidor] = listUnidadesOrgaRefinerias;
                    cmbTipoConsumidor.DataSource = Session[CVariablesSesion.ComboTipoConsumidor];
                    cmbTipoConsumidor.DataBind();
                    cmbTipoConsumidor.SelectedItem = cmbTipoConsumidor.Items.FindByText("TODO");
                }
            }
            else
            {
                //A Implementar
            }
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
        private void MCargaGrilla(string strFechaIni, string strFechaFin, string strIdActividad, string strIdentificador, string strIdTipoConsumidor, string strTipoDoc)
        {
            if (chkAccion.Checked == true)
            {
                #region Operacion servicio Bsisa Usuario Directo

                _clientReg.Headers.Add(CAppSettings.CabeceraKey, CAppSettings.CabeceraValue);
                var consumidorDirecto = _clientReg.Get<List<EListadoVentas>>("/obtenerreporteventa/" + "0/0/" + strIdentificador + "/" + strFechaIni + "/" + strFechaFin + "?format=json");
                if (consumidorDirecto.Count > 0)
                {
                    var listConsumo = new List<O_CONSUMO_GRAL_CTY>();
                    var objConsumo = new O_CONSUMO_GRAL_CTY();
                    foreach (var item in consumidorDirecto)
                    {
                        DateTime FechaAux = CFechas.ConvierteLongDateTime(item.FechaRegistro);
                        int intDia = DateTime.DaysInMonth((int)FechaAux.Year, (int)FechaAux.Month);
                        var strFechaFinAux = FechaAux.Year + FechaAux.Month.ToString().PadLeft(2, '0') + intDia.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0');

                        var objResultado = _consulta.ObtenerDjCDirecto(
                            new EObjetoEnvio5ParametrosProto
                            {
                                strCredencial = CParametrosHydro.strCredencialFuncionario,
                                decFiltro = 1, //Filtro 1
                                strParametro1 = strIdentificador, 
                                strParametro2 = strIdTipoConsumidor,
                                strParametro3 = item.FechaRegistro.ToString(),
                                strParametro4 = strFechaFinAux + "235959",                                    
                                strParametro5 = item.IdEess.ToString()
                            });
                        if (objResultado.intCodigo == 1)
                        {
                            var list = (List<O_OBTENER_DJ_CDIRECTO>)objResultado.oResultado;
                            
                            objConsumo.ID=0;
                            objConsumo.ID_CONSUMIDOR = list[0].ID_CONSUMIDOR;
                            objConsumo.DENOMINACION = item.Denominacion;
                            objConsumo.NIT_EMISOR = item.NitEstacion;
                            objConsumo.ID_DEPARTAMENTO=0;
                            objConsumo.DEPARTAMENTO = item.Departamento;
                            objConsumo.NIT_CONSUMIDOR = item.NitConsumidor;
                            objConsumo.RAZON_SOCIAL = item.RazonSocial;
                            objConsumo.ID_HIDROCARBURO=0;
                            objConsumo.PRODUCTO = item.Producto;
                            objConsumo.CANTIDAD= item.Volumen;
                            objConsumo.PRECIO_UNITARIO = item.PrecioUnitario;
                            objConsumo.PRECIO_TOTAL = item.Importe;
                            objConsumo.FECHA_OPERACION = item.FechaRegistro;
                            objConsumo.NRO_TRANSACCION = item.NroFactura.ToString();
                            objConsumo.ID_TIPO_TRANSACCION = "";
                            objConsumo.TIPO_DOC = "";
                            objConsumo.DAT_FECHA = FechaAux;
                            objConsumo.FECHA_DJ = Convert.ToDateTime(list[0].FECHA_DJ);
                            objConsumo.CITE_GENERADO = (list[0].ID_CONSUMIDOR > 0) ? list[0].CITE_GENERADO : " ";
                            objConsumo.TIPO_CONSUMIDOR = (list[0].ID_CONSUMIDOR > 0) ? list[0].TIPO_CONSUMIDOR : " ";
                            objConsumo.IDENTIFICADOR = item.Identificador;
                            objConsumo.UMEDIDA = "";
                            objConsumo.MUNICIPIO = "";
                            objConsumo.USUARIO_TRANSACCION = "";     
                            listConsumo.Add(objConsumo);
                        }
                        else
                        {
                            MImprimirMensaje(objResultado.strMensaje, EnumTiposMensajeAlerta.Alerta);
                        }
                    }

                    pvgDatos.DataSource = listConsumo;
                    pvgDatos.DataBind();
                    Session[CVariablesSesion.GrillaCabecera] = listConsumo;
                }
                else
                {
                    MImprimirMensaje(" No se encontraron registros (web)", EnumTiposMensajeAlerta.Alerta);
                }

                #endregion
            }
            else
            {
                #region Operacion OctVol
                var objConsulta = new EObjetoEnvio5ParametrosProto
                {
                    strCredencial = CParametrosHydro.strCredencialFuncionario,
                    decFiltro = 1, //Filtro 1
                    strParametro1 = strFechaIni + "000000",
                    strParametro2 = strFechaFin + "235959",
                    strParametro3 = strIdActividad,
                    strParametro4 = strIdentificador,
                    strParametro5 = strIdTipoConsumidor,
                    strParametro6 = strTipoDoc
                };
                var objResultado = _consulta.ReporteConsumoMtr(objConsulta);
                if (objResultado.intCodigo == 1)
                {
                    var lstReporte = (List<O_CONSUMO_GRAL_CTY>)objResultado.oResultado;
                    pvgDatos.DataSource = lstReporte;
                    pvgDatos.DataBind();
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
        }

        /// <summary>
        /// METODO. MValidaDatosIniciales
        /// </summary>
        /// <param name="strMensaje"></param>
        /// <returns></returns>
        private bool MValidaDatosIniciales(ref string strMensaje)
        {
            string[] valores = chkAccion.Text.Split('|');

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
            //else if (txtIdentificador.Text.Length < 4)
            //{
            //    strMensaje = "Introduzca un identificador valido.";
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
            pvgDatos.DataSource = null;
            pvgDatos.DataBind();

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

        /* protected void dgvReporte_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            try
            {
                #region Proceso a Ejecutar

                if (e.VisibleIndex < 0) return;
                if (e.VisibleIndex % 2 == 1)
                    e.Row.BackColor = Color.FromArgb(204, 209, 182);

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


        private class EListadoVentas
        {
            public decimal IdEess { get; set; }
            public decimal NitEstacion { get; set; }
            public string Denominacion { get; set; }
            public string Departamento { get; set; }
            public decimal IdVentaEstacion { get; set; }
            public string Barcode { get; set; }
            public decimal IdTransaccion { get; set; }
            public string Identificador { get; set; }
            public decimal NitConsumidor { get; set; }
            public string RazonSocial { get; set; }
            public string Producto { get; set; }
            public decimal NroFactura { get; set; }
            public decimal NroAutorizacion { get; set; }
            public string CodigoControl { get; set; }
            public decimal Volumen { get; set; }
            public decimal PrecioUnitario { get; set; }
            public decimal Importe { get; set; }
            public decimal FechaRegistro { get; set; }
            public decimal FechaSistema { get; set; }
        }

    }
}