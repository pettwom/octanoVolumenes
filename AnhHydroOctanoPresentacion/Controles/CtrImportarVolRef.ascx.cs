

namespace AnhHydroOctanoPresentacion.Controles
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Web;
    using System.Web.UI;
    using AnhHydroOctanoPersistencia.Entidades;
    using Entidades;
    using Enumeradores;
    using Parametros;
    using DevExpress.Web;
    using Librerias.Anh.Us;
    using ServiceStack.ServiceClient.Web;
    using ServiceStack.Text;

    public partial class CtrImportarVolRef : UserControl
    {
        #region Variables de entorno

        readonly JsonServiceClient _extInteg = new JsonServiceClient(CAppSettings.servicio_octIntegracion);

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
                        Session.Remove("PATH");
                        Session.Remove(CVariablesSesion.ArchivoDescarga);
                        Session.Remove(CVariablesSesion.Callback);
                        
                        Session.Remove(CVariablesSesion.GrillaCabecera);
                        Session.Remove(CVariablesSesion.GrillaRegObservado);
                        Session.Remove(CVariablesSesion.GrillaTransaccion);

                        Session.Remove(CVariablesSesion.ListaEntidad);
                        Session.Remove(CVariablesSesion.ListaParametrica);
                        Session.Remove(CVariablesSesion.ListaParamGral);


                        Session.Remove(CVariablesSesion.ListaCatalogo);
                        
                        Session.Remove(CVariablesSesion.ListaInfraestructura);
                        
                        Session.Remove(CVariablesSesion.ListaParamInfra);

                        Session.Remove(CVariablesSesion.ReportePrincipal);
                        Session.Remove(CVariablesSesion.TipoReporte);
                        Session.Remove(CVariablesSesion.IdEntidadRevision);

                        Session.Remove(CVariablesSesion.ComboTipoOperacion);

                        Session[CVariablesSesion.Callback] = "0";

                        MCargaParametricas();
                        MCargaPermisos();
                    }

                    if (dgvCabecera.DataSource == null && Session[CVariablesSesion.GrillaCabecera] != null)
                    {
                        dgvCabecera.DataSource = Session[CVariablesSesion.GrillaCabecera];
                        dgvCabecera.DataBind();
                    }

                    if (dgvTransacciones.DataSource == null && Session[CVariablesSesion.GrillaTransaccion] != null)
                    {
                        dgvTransacciones.DataSource = Session[CVariablesSesion.GrillaTransaccion];
                        dgvTransacciones.DataBind();
                    }

                    if (dgvTransaccionError.DataSource == null && Session[CVariablesSesion.GrillaRegObservado] != null)
                    {
                        dgvTransaccionError.DataSource = Session[CVariablesSesion.GrillaRegObservado];
                        dgvTransaccionError.DataBind();
                    }

                    if (cmbExplotador.DataSource == null && Session[CVariablesSesion.ListaEntidad] != null)
                    {
                        cmbExplotador.DataSource = Session[CVariablesSesion.ListaEntidad];
                        cmbExplotador.DataBind();
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
                MLimpiaGrilla();
                try
                {
                    #region Proceso a Ejecutar

                    var objRes = _extInteg.Post<EResultado<List<O_OBTENER_VOLUMEN_REF_CTY>>>(CAppSettings.svc_obtener_volumen_referencial,
                    new
                    {
                        strCredencial = CParametrosHydro.StrCredencialExterno,
                        decFiltro = 2,
                        strDato1 = "",
                        strDato2 = "",
                        strDato3 = "",
                        strDato4 = "",
                        strDato5 = "",
                        strDato6 = ""
                    });
                    if (objRes.decCodigo == 0)
                    {
                        dgvCabecera.DataSource = objRes.oResultado;
                        dgvCabecera.DataBind();
                        Session[CVariablesSesion.GrillaCabecera] = objRes.oResultado;
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

       /* protected void dgvCabecera_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
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

        protected void dgvTransaccionError_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            try
            {
                #region Proceso a Ejecutar

                if (e.VisibleIndex >= 0)
                {
                    if (e.VisibleIndex % 2 == 1)
                    {
                        e.Row.BackColor = Color.LightGray;
                    }
                    else
                    {
                        //todo No implementado.
                    }
                }
                else
                {
                    //todo No implementado.
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

        /*protected void dgvTransacciones_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            try
            {
                #region Proceso a Ejecutar

                if (e.VisibleIndex >= 0)
                {
                    if (e.VisibleIndex % 2 == 1)
                    {
                        e.Row.BackColor = Color.FromArgb(204, 209, 182);
                    }
                    else
                    {
                        //todo No implementado.
                    }
                }
                else
                {
                    //todo No implementado.
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

        protected void btnImportar_Click(object sender, EventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    Session[CVariablesSesion.Callback] = "1";

                    if (FileUploader.HasFile)
                    {
                        MLimpiaPopup();
                        var extension = Path.GetExtension(FileUploader.FileName);
                        if (extension != null)
                        {
                            var strExtension = extension.ToUpper();
                            if (strExtension == ".CSV")
                            {
                                var strRutaServer = Path.GetTempPath();
                                var strNombreArchivo = DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + DateTime.Now.Millisecond;
                            
                                Session["PATH"] = strRutaServer + strNombreArchivo;
                                FileUploader.SaveAs((string)Session["PATH"]);
                                MCargarDatos((string)Session["PATH"]);
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError", "LoadingPanel.Hide();", true);
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError", "alert('Tipo de archivo incorrecto, el formato debe ser CSV.');", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError", "alert('Seleccione un archivo válido.');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError", "alert('Archivo Inexistente.');", true);
                    }

                    #endregion
                }
                catch (Exception exp)
                {
                    #region RegistraError

                    MLimpiaPopup();
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

        protected void btnGrabar_Click(object sender, EventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    var lstRegistros = Session[CVariablesSesion.GrillaTransaccion] as List<O_OBTENER_VOLUMEN_REF_CTY>;

                    var objRes = _extInteg.Post<EResultadoI>(CAppSettings.svc_gestion_volumen_referencial,
                    new
                    {
                        strCredencial = CParametrosHydro.StrCredencialExterno,
                        decFiltro = 1,
                        objRegistros = lstRegistros
                    });
                    if (objRes.decCodigo == 1)
                    {
                        

                        //PDF

                        var datFechaReg1 = DateTime.Now;
                        var repRvr = new repRegistroVolRef();

                        repRvr.xtcFecha.Text = datFechaReg1.ToShortDateString();
                        repRvr.xtcHora.Text = datFechaReg1.ToLongTimeString();
                        repRvr.xtcUsuario.Text = CParametrosHydro.StrUsuarioLogin;
                        repRvr.DataSource = lstRegistros;

                        Session[CVariablesSesion.TipoReporte] = EnumTipoReporte.RepRegistroVolRef;
                        Session[CVariablesSesion.ReportePrincipal] = repRvr;
                        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( '../Movimientos/WFReporte.aspx', '');", true);

                        MLimpiaPopup();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError", "alert('ERROR. '" + objRes.strMensaje + ");", true);
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

                    dgeExportar.GridViewID = "dgvTransaccionError";
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

        protected void btnDescargar_Click(object sender, EventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    //if ((int)tlsOperadores.FocusedNode.GetValue("IntIdActividad") == 0)
                    if (1 == 0)
                    {
                        MImprimirMensaje("Seleccione una actividad.", EnumTiposMensajeAlerta.Alerta);
                    }
                    //else if (cmbTipoOperacion.Value == null)
                    else if (null != null)
                    {
                        MImprimirMensaje("Seleccione un tipo de operación.", EnumTiposMensajeAlerta.Alerta);
                    }
                    else
                    {
                        //var cod = cmbTipoOperacion.Text.Split(';');
                        //var strCodTp = cod[1].Substring(0, 4);
                        var strCodTp = "fdf";
                        string fileName;

                        switch (strCodTp)
                        {
                            case "TERC":
                                fileName = "plantilla_pscaa_recepcion.xlsx";
                                break;
                            case "TEDS":
                                fileName = "plantilla_pscaa_despacho.xlsx";
                                break;
                            case "ELRC":
                                fileName = "plantilla_eessliq_recepcion.xlsx";
                                break;
                            case "ELDS":
                                fileName = "plantilla_eessliq_despacho.xlsx";
                                break;
                            case "EGDS":
                                fileName = "plantilla_eessgnv_despacho.xlsx";
                                break;
                            case "TARC":
                                fileName = "plantilla_tacl_recepcion.xlsx";
                                break;
                            case "TADS":
                                fileName = "plantilla_tacl_despacho.xlsx";
                                break;
                            case "PVRC":
                                fileName = "plantilla_ptoVentaliq_recepcion.xlsx";
                                break;
                            case "PVDS":
                                fileName = "plantilla_ptoVentaliq_despacho.xlsx";
                                break;

                            default:
                                MImprimirMensaje("Actividad Invalida.", EnumTiposMensajeAlerta.Error);
                                return;
                        }
                        try
                        {
                            var response = HttpContext.Current.Response;
                            response.ClearContent();
                            response.Clear();
                            response.ContentType = "application/excel";
                            response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ";");
                            response.TransmitFile(Server.MapPath("~/Sitio/Plantillas/" + fileName));
                            response.Flush();
                            response.End();
                        }
                        catch (Exception ex)
                        {
                            
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
            else
            {
                //todo No implementado.
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
                    var script = string.Format("document.location.href = '{0}');", "~/Sitio/Persona/wfAutenticacion.aspx");
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
        /// METODO. MCargaPermisos
        /// </summary>
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
                                                                      "/WFImportarVolRef.aspx?format=json");
                if (lstResultado.decCodigo == 1)
                {
                    if (lstResultado.oResultado != null && lstResultado.oResultado.Any())
                    {
                        Session[CVariablesSesion.PermisoCons] = lstResultado.oResultado.FirstOrDefault().CONSULTA;
                        Session[CVariablesSesion.PermisoAlta] = lstResultado.oResultado.FirstOrDefault().ALTAS;
                        Session[CVariablesSesion.PermisoBaja] = lstResultado.oResultado.FirstOrDefault().BAJAS;
                    }
                    else
                    {
                        Session[CVariablesSesion.PermisoCons] = 0;
                        Session[CVariablesSesion.PermisoAlta] = 0;
                        Session[CVariablesSesion.PermisoBaja] = 0;
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
        /// METODO. MLimpiaGrilla
        /// </summary>
        private void MLimpiaGrilla()
        {
            Session.Remove(CVariablesSesion.GrillaCabecera);
            pnlMensaje.Visible = false;

            dgvCabecera.DataSource = null;
            dgvCabecera.DataBind();
        }

        /// <summary>
        /// METODO. MLimpiaPopup
        /// </summary>
        private void MLimpiaPopup()
        {
            Session.Remove(CVariablesSesion.GrillaTransaccion);
            Session.Remove(CVariablesSesion.GrillaRegObservado);
            btnGrabar.Enabled = false;

            dgvTransacciones.DataSource = null;
            dgvTransacciones.DataBind();
            dgvTransaccionError.DataSource = null;
            dgvTransaccionError.DataBind();
        }

        /// <summary>
        /// METODO. MCargaParametricas
        /// </summary>
        private void MCargaParametricas()
        {
            //EXPLOTADORES
            var objRes = _extInteg.Post<EResultado<List<O_OBTENER_PARAMETRICA_CTY>>>(CAppSettings.svc_obtener_parametrica_int,
                    new
                    {
                        strCredencial = CParametrosHydro.StrCredencialExterno,
                        decFiltro = 1
                    });
            if (objRes.decCodigo == 1)
            {
                cmbExplotador.DataSource = objRes.oResultado;
                cmbExplotador.DataBind();
                cmbExplotador.SelectedIndex = 0;
                Session[CVariablesSesion.ListaEntidad] = objRes.oResultado;
            }
                

            //DGAC - MODELOS
            var objResultado = _extInteg.Post<EResultadoI>(CAppSettings.svc_GestionDgac,
            new
            {
                strCredencial = CParametrosHydro.StrCredencialExterno,
                decOrigen = 2,//1:anh | 2:dgac
                decServicio = 1,//1:aeronaves | 2:pilotos
                decAccion = 0,//0:sin accion | 1:actualiza tabla local
                decTipo = 0,//0:todos | 1:espeifico
                strDato = "",
                decIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario])
            });
            if (objResultado.decCodigo == 1)
            {
                Session[CVariablesSesion.ListaParametrica] = JsonSerializer.DeserializeFromString<List<O_DGAC_AVION>>((string)objResultado.oResultado);
            }
            else
            {
                objResultado = _extInteg.Post<EResultadoI>(CAppSettings.svc_GestionDgac,
                new
                {
                    strCredencial = CParametrosHydro.StrCredencialExterno,
                    decOrigen = 1,//1:anh | 2:dgac
                    decServicio = 1,//1:aeronaves | 2:pilotos
                    decAccion = 0,//0:sin accion | 1:actualiza tabla local
                    decTipo = 0,//0:todos | 1:espeifico
                    strDato = "",
                    decIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario])
                });
                if (objResultado.decCodigo == 1)
                {
                    //todo devolver mismo objeto :(
                    Session[CVariablesSesion.ListaParametrica] = JsonSerializer.DeserializeFromString<List<O_DGAC_AVION>>((string)objResultado.oResultado);
                }
            }

            //AASANA - AEROPUERTOS
            objResultado = _extInteg.Post<EResultadoI>(CAppSettings.svc_GestionAasana,
                new
                {
                    strCredencial = CParametrosHydro.StrCredencialExterno,
                    decOrigen = 2,//1:anh | 2:aasana
                    decAccion = 0,//0:sin accion | 1:actualiza tabla local
                    decServicio = 1,//1: parametrica | 2: integracion
                    decIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]),
                    decFiltro = 1,//0:todos | 1:espeifico
                    strDato1 = "aeropuerto",//nombre de la tabla
                    strDato2 = "",
                    strDato3 = "",
                    strDato4 = ""
                });
            if (objResultado.decCodigo == 1)
            {
                Session[CVariablesSesion.ListaParamGral] = JsonSerializer.DeserializeFromString<List<O_AASANA_AEROPUERTO>>((string)objResultado.oResultado);
            }
            else
            {
                objResultado = _extInteg.Post<EResultadoI>(CAppSettings.svc_GestionAasana,
                new
                {
                    strCredencial = CParametrosHydro.StrCredencialExterno,
                    decOrigen = 1,//1:anh | 2:aasana
                    decAccion = 0,//0:sin accion | 1:actualiza tabla local
                    decServicio = 1,//1: parametrica | 2: integracion
                    decIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]),
                    decFiltro = 1,//0:todos | 1:espeifico
                    strDato1 = "aeropuerto",//nombre de la tabla
                    strDato2 = "",
                    strDato3 = "",
                    strDato4 = ""
                });
                if (objResultado.decCodigo == 1)
                {
                    //todo devolver mismo objeto :(
                    Session[CVariablesSesion.ListaParamGral] = JsonSerializer.DeserializeFromString<List<O_AASANA_AEROPUERTO>>((string)objResultado.oResultado);
                }
            }
        }

        /// <summary>
        /// METODO. MCargarDatos
        /// </summary>
        /// <param name="strRuta"></param>
        private void MCargarDatos(string strRuta)
        {

            var reader = new StreamReader(File.OpenRead(@strRuta));
            long lngNoReg = 1;

            var lstCorrecto = new List<O_OBTENER_VOLUMEN_REF_CTY>();
            var lstError = new List<O_OBTENER_VOLUMEN_REF_CTY>();

            var intNroLinea = 9;
            if(Convert.ToInt32(cmbExplotador.Value)==-99999)
                intNroLinea = 10;

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(';');
                try
                {
                    var strMensaje = string.Empty;
                    if (MValidaLinea(values, intNroLinea, ref strMensaje))
                    {
                        var objDato = new O_OBTENER_VOLUMEN_REF_CTY();
                        objDato.ID_VOLUMEN_REFERENCIAL = lngNoReg;
                        if (intNroLinea == 10)
                        {
                            objDato.ID_EXPLOTADOR = MCodificaParametrica((int)EnumTipoParametrica.Explotador, values[0].Trim());
                            objDato.EXPLOTADOR = values[0].Trim();
                        }
                        else
                        {
                            objDato.ID_EXPLOTADOR = Convert.ToDecimal(cmbExplotador.Value);
                            objDato.EXPLOTADOR = cmbExplotador.Text;
                        }

                        objDato.MODELO = values[intNroLinea-9].Trim().Replace("-", "").Replace(" ", "");
                        objDato.MATRICULA = "";
                        objDato.ID_AEROPUERTO_ORIGEN = values[intNroLinea-8].Trim();
                        objDato.ID_AEROPUERTO_DESTINO = values[intNroLinea-7].Trim();
                        objDato.ID_AEROPUERTO_ALTERNO = values[intNroLinea-6].Trim();
                        objDato.TIEMPO_VUELO = values[intNroLinea-5].Trim();
                        objDato.CONSUMO_HORA_VUELO = Convert.ToDecimal(values[intNroLinea-4]);
                        objDato.TIEMPO_HORAS_BLOCK = values[intNroLinea-3].Trim();
                        objDato.TANQUEO_MAXIMO_REQUERIDO = Convert.ToDecimal(values[intNroLinea-2]);
                        objDato.TIEMPO_SOBREVUELO_MINIMO = values[intNroLinea-1].Trim();

                        objDato.ID_TIPO_ACTIVIDAD = 50;
                        objDato.ID_HIDROCARBURO = Convert.ToDecimal(EnumProductos.JetFuelA1);
                        objDato.ID_UNIDAD_MEDIDA = 68;
                        objDato.APP_ID_USUARIO = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                        lstCorrecto.Add(objDato);
                    }
                    else
                    {
                        var objDato = new O_OBTENER_VOLUMEN_REF_CTY();
                        objDato.ID_VOLUMEN_REFERENCIAL = lngNoReg;
                        objDato.MNS_ERROR = strMensaje;
                        lstError.Add(objDato);
                    }
                }
                catch (Exception ex)
                {
                    var cRegError = new O_OBTENER_VOLUMEN_REF_CTY();
                    cRegError.ID_VOLUMEN_REFERENCIAL = lngNoReg;
                    cRegError.MNS_ERROR = "ERROR:" + ex.Message;
                    lstError.Add(cRegError);
                }
                lngNoReg++;
            }

            dgvTransacciones.DataSource = lstCorrecto;
            dgvTransacciones.DataBind();
            Session[CVariablesSesion.GrillaTransaccion] = lstCorrecto;

            dgvTransaccionError.DataSource = lstError;
            dgvTransaccionError.DataBind();
            Session[CVariablesSesion.GrillaRegObservado] = lstError;

            btnGrabar.Enabled = !lstError.Any();
        }

        /// <summary>
        /// METODO. MValidaLineaEess
        /// </summary>
        /// <param name="values"></param>
        /// <param name="intNroLinea"></param>
        /// <param name="strMensaje"></param>
        /// <returns></returns>
        private bool MValidaLinea(string[] values, int intNroLinea, ref string strMensaje)
        {
            var strResultado = true;
            try
            {
                #region Despachos
                if (values.Length != intNroLinea)
                {
                    strMensaje = "- Nro. de columnas del registro invalido.";
                }
                var arrayNumero = new[] { intNroLinea-4, intNroLinea-2 };
                decimal dec;
                foreach (var t in arrayNumero)
                {
                    if (!decimal.TryParse(values[t].Trim(), out dec))
                    {
                        strMensaje += "- Columna Nro. " + t + " No es un número.";
                    }
                    else
                    {
                        var decValor = Convert.ToDecimal(values[t]);
                        var decEntero = Math.Truncate(decValor);
                        var decDecimal = decValor - decEntero;
                        if (decEntero.ToString().Length > 9 || decDecimal.ToString().Length > 8)
                        {
                            strMensaje += "- Columna Nro. " + t + " El formato del número no es válido.";
                        }
                    }
                }
                arrayNumero = new[] { intNroLinea - 5, intNroLinea-3, intNroLinea-1 };
                DateTime dt;
                for (int i = 0; i < arrayNumero.Length; i++)
                {
                    if (!DateTime.TryParse(values[arrayNumero[i]].Trim(), out dt))
                    {
                        strMensaje += "- Columna Nro. " + arrayNumero[i] + " No tiene el formato de hora.";
                    }
                }
                if (intNroLinea == 10)
                {
                    if (values[0].Trim().Length > 0)
                    {
                        if (MCodificaParametrica((int)EnumTipoParametrica.Explotador, values[0].Trim()) == -99999)
                        {
                            strMensaje += "- Explotador inválido.";
                        }
                    }
                    else
                    {
                        strMensaje += "- Explotador inválido.";
                    }
                }
                
                if (MCodificaParametrica((int)EnumTipoParametrica.AeronaveModelo, values[intNroLinea-9].Trim()) == -99999)
                {
                    strMensaje += "- Modelo inválido.";
                }
                if (MCodificaParametrica((int)EnumTipoParametrica.Aeropuerto, values[intNroLinea-8].Trim()) == -99999)
                {
                    strMensaje += "- Aeropuerto origen inválido.";
                }
                if (MCodificaParametrica((int)EnumTipoParametrica.Aeropuerto, values[intNroLinea-7].Trim()) == -99999)
                {
                    strMensaje += "- Aeropuerto destino inválido.";
                }
                if (MCodificaParametrica((int)EnumTipoParametrica.Aeropuerto, values[intNroLinea-6].Trim()) == -99999)
                {
                    strMensaje += "- Aeropuerto alterno inválido.";
                }

                #endregion
            }
            catch (Exception ex)
            {
                strMensaje += ex.Message;
            }

            if (strMensaje.Trim().Length > 0)
            {
                strResultado = false;
            }

            return strResultado;
        }

        /// <summary>
        /// METODO. MCodificaParametrica
        /// </summary>
        /// <param name="intParametrica"></param>
        /// <param name="strDescripcion"></param>
        /// <returns></returns>
        private int MCodificaParametrica(int intParametrica, string strDescripcion)
        {
            var lngResultado = -99999;

            var objExplotador = Session[CVariablesSesion.ListaEntidad] as List<O_OBTENER_PARAMETRICA_CTY>;
            var objModelo = Session[CVariablesSesion.ListaParametrica] as List<O_DGAC_AVION>;
            var objAeropuerto = Session[CVariablesSesion.ListaParamGral] as List<O_AASANA_AEROPUERTO>;

            switch (intParametrica)
            {
                case (int)EnumTipoParametrica.Explotador:
                    if (objExplotador != null && objExplotador.Any())
                    {
                        objExplotador = objExplotador.Where(x => x.PARAMETRICA.Trim() == strDescripcion.Trim()).ToList();
                        if (objExplotador.Any())
                        {
                            lngResultado = Convert.ToInt32(objExplotador.FirstOrDefault().ID_PARAMETRICA);
                        }
                    }
                    break;
                case (int)EnumTipoParametrica.AeronaveModelo:
                    if (objModelo != null && objModelo.Any())
                    {
                        objModelo = objModelo.Where(x => x.modelo.Trim().Replace("-", "").Replace(" ", "").ToUpper() == strDescripcion.Trim().Replace("-", "").Replace(" ", "").ToUpper()).ToList();
                        if (objModelo.Any())
                        {
                            lngResultado = 1;
                        }
                    }
                    break;
                case (int)EnumTipoParametrica.Aeropuerto:
                    if (objAeropuerto != null && objAeropuerto.Any())
                    {
                        objAeropuerto = objAeropuerto.Where(x => x.idaeropuerto.Trim() == strDescripcion.Trim()).ToList();
                        if (objAeropuerto.Any())
                        {
                            lngResultado = 1;
                        }
                    }
                    break;
            }
            return lngResultado;
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

        #endregion

    }
}