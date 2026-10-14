
namespace AnhHydroOctanoPresentacion.Controles
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text.RegularExpressions;
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
    using ServiceStack.Common.Extensions;
    using ServiceStack.ServiceClient.Web;
    using ServiceStack.Text;

    public partial class CtrIncentivos : UserControl
    {
        #region Variables de entorno

        readonly JsonServiceClient _client = new JsonServiceClient(CAppSettings.servicio_octVolumen);
        readonly JsonServiceClient _clientI = new JsonServiceClient(CAppSettings.servicio_octInfraestructura);
        private readonly CPersistenciaConsulta _consulta = new CPersistenciaConsulta();
        private readonly CPersistenciaGestion _gestion = new CPersistenciaGestion();
        readonly CUtil _cUtil = new CUtil();

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
                        Session.Remove(CVariablesSesion.EstructuraArbol);
                        Session.Remove(CVariablesSesion.GrillaRegObservado);
                        Session.Remove(CVariablesSesion.GrillaTransaccion);
                        Session.Remove(CVariablesSesion.ListaCatalogo);
                        Session.Remove(CVariablesSesion.ListaEntidad);
                        Session.Remove(CVariablesSesion.ListaInfraestructura);
                        Session.Remove(CVariablesSesion.ListaParametrica);
                        Session.Remove(CVariablesSesion.ListaParamGral);
                        Session.Remove(CVariablesSesion.ListaParamInfra);
                        Session.Remove(CVariablesSesion.ReportePrincipal);
                        Session.Remove(CVariablesSesion.TipoReporte);

                        Session[CVariablesSesion.Callback] = "0";

                        MCargaArbol();
                        MCargaParametricas();
                        ConfigurarFormulario();

                        //tlsOperadores.DataSource = Session[CVariablesSesion.EstructuraArbol];
                        //tlsOperadores.DataBind();

                    }
                    else
                    {
                        //todo No es carga inicial: no implementado
                    }

                    //if (tlsOperadores.DataSource == null && Session[CVariablesSesion.EstructuraArbol] != null)
                    //{
                    //    tlsOperadores.DataSource = Session[CVariablesSesion.EstructuraArbol];
                    //    tlsOperadores.DataBind();
                    //}
                    //else
                    //{
                    //    //todo no implementado
                    //}

                    if (dgvFormA.ClientVisible && dgvFormA.DataSource == null && Session[CVariablesSesion.GrillaTransaccion] != null)
                    {
                        dgvFormA.DataSource = Session[CVariablesSesion.GrillaTransaccion];
                        dgvFormA.DataBind();
                    }
                    else
                    {
                        //todo No es carga inicial: no implementado
                    }

                    if (dgvFormB.ClientVisible && dgvFormB.DataSource == null && Session[CVariablesSesion.GrillaTransaccion] != null)
                    {
                        dgvFormB.DataSource = Session[CVariablesSesion.GrillaTransaccion];
                        dgvFormB.DataBind();
                    }
                    else
                    {
                        //todo No es carga inicial: no implementado
                    }

                    if (dgvFormC.ClientVisible && dgvFormC.DataSource == null && Session[CVariablesSesion.GrillaTransaccion] != null)
                    {
                        dgvFormC.DataSource = Session[CVariablesSesion.GrillaTransaccion];
                        dgvFormC.DataBind();
                    }
                    else
                    {
                        //todo No es carga inicial: no implementado
                    }

                    if (dgvFormD.ClientVisible && dgvFormD.DataSource == null && Session[CVariablesSesion.GrillaTransaccion] != null)
                    {
                        dgvFormD.DataSource = Session[CVariablesSesion.GrillaTransaccion];
                        dgvFormD.DataBind();
                    }
                    else
                    {
                        //todo No es carga inicial: no implementado
                    }

                    if (dgvFormE.ClientVisible && dgvFormE.DataSource == null && Session[CVariablesSesion.GrillaTransaccion] != null)
                    {
                        dgvFormE.DataSource = Session[CVariablesSesion.GrillaTransaccion];
                        dgvFormE.DataBind();
                    }
                    else
                    {
                        //todo No es carga inicial: no implementado
                    }

                    if (dgvFormF.ClientVisible && dgvFormF.DataSource == null && Session[CVariablesSesion.GrillaTransaccion] != null)
                    {
                        dgvFormF.DataSource = Session[CVariablesSesion.GrillaTransaccion];
                        dgvFormF.DataBind();
                    }
                    else
                    {
                        //todo No es carga inicial: no implementado
                    }

                    if (dgvFormG.ClientVisible && dgvFormG.DataSource == null && Session[CVariablesSesion.GrillaTransaccion] != null)
                    {
                        dgvFormG.DataSource = Session[CVariablesSesion.GrillaTransaccion];
                        dgvFormG.DataBind();
                    }
                    else
                    {
                        //todo No es carga inicial: no implementado
                    }

                    if (dgvFormH.ClientVisible && dgvFormH.DataSource == null && Session[CVariablesSesion.GrillaTransaccion] != null)
                    {
                        dgvFormH.DataSource = Session[CVariablesSesion.GrillaTransaccion];
                        dgvFormH.DataBind();
                    }
                    else
                    {
                        //todo No es carga inicial: no implementado
                    }

                    if (dgvTransaccionError.DataSource == null && Session[CVariablesSesion.GrillaRegObservado] != null)
                    {
                        dgvTransaccionError.DataSource = Session[CVariablesSesion.GrillaRegObservado];
                        dgvTransaccionError.DataBind();
                    }
                    else
                    {
                        //todo No es carga inicial: no implementado
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

        protected void pnlCabecera_Callback(object sender, CallbackEventArgsBase e)
        {
            //if (MValidaSesion())
            //{
            //    try
            //    {
            //        #region Proceso a Ejecutar

            //        cmbTipoOperacion.DataSource = null;
            //        cmbTipoOperacion.Text = "";

            //        cmbPeriodo.Items.Clear();
            //        cmbPeriodo.Text = "";

            //        if (tlsOperadores.FocusedNode != null)
            //        {
            //            if ((int)tlsOperadores.FocusedNode.GetValue("IntAccion") > 0)
            //            {
            //                var objPeriodo = _client.Post<EResultadoPeriodos>(CAppSettings.svc_lista_periodos_consumidor,
            //                new
            //                {
            //                    strCredencial = CParametrosHydro.strCredencialFuncionario,
            //                    decFiltro = 1,
            //                    strParametro1 = tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString(),
            //                    strParametro2 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
            //                    strParametro3 = "",
            //                    strParametro4 = "",
            //                    strParametro5 = tlsOperadores.FocusedNode.GetValue("DecIdDepartamento").ToString()
            //                });
            //                if (objPeriodo.IntCodigo == 1)
            //                {
            //                    int decCod;
            //                    foreach (var dato in objPeriodo.OResultado)
            //                    {
            //                        decCod = Convert.ToInt32(dato.decAño + dato.decMes.ToString().PadLeft(2, '0'));
            //                        cmbPeriodo.Items.Add(dato.strPeriodo, decCod);
            //                    }

            //                    decCod = Convert.ToInt32(objPeriodo.OResultado[0].decAño + objPeriodo.OResultado[0].decMes.ToString().PadLeft(2, '0'));
            //                    cmbPeriodo.Value = decCod;

            //                    var objTipoOperacion = _client.Post<EResultadoTipoOperacion>(CAppSettings.svc_obtener_tipos_operacion_act,
            //                    new
            //                    {
            //                        decFiltro = 1,
            //                        strParametro1 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
            //                        strParametro2 = "",
            //                        strParametro3 = ""
            //                    });
            //                    if (objTipoOperacion.IntCodigo == 1)
            //                    {
            //                        objTipoOperacion.OResultado = objTipoOperacion.OResultado.OrderByDescending(x => x.FACTOR_CALCULO).ThenBy(y => y.NOMBRE).ToList();
            //                        cmbTipoOperacion.DataSource = objTipoOperacion.OResultado;
            //                        cmbTipoOperacion.DataBind();
            //                    }
            //                    else
            //                    {
            //                        MImprimirMensaje(objTipoOperacion.StrMensaje, EnumTiposMensajeAlerta.Error);
            //                    }
            //                }
            //                else
            //                {
            //                    MImprimirMensaje(objPeriodo.StrMensaje, EnumTiposMensajeAlerta.Error);
            //                }
            //            }
            //            else
            //            {
            //                //todo No implementado.
            //            }
            //        }
            //        else
            //        {
            //            //todo No implementado.
            //        }

            //        #endregion
            //    }
            //    catch (Exception exp)
            //    {
            //        #region RegistraError

            //        MImprimirMensaje(exp.Message, EnumTiposMensajeAlerta.Error);
            //        var oMensajeUsuario = new CLogTraza.MensajeUsuario
            //        {
            //            decUsuarioId = Session[CVariablesSesion.IdUsuario],
            //            decIdModulo = CParametrosHydro.decIdAplicacion,
            //            strAccion = GetType().Name + "." + MethodBase.GetCurrentMethod().Name,
            //            strIp = HttpContext.Current.Request.UserHostAddress,
            //            decNivelCapa = (int)CLogTraza.CapasNivel.Presentacion
            //        };
            //        CLogTraza.Error(oMensajeUsuario, exp);

            //        #endregion
            //    }
            //    Session[CVariablesSesion.Callback] = "0";
            //}
            //else
            //{
            //    //todo No implementado.
            //}
        }

        protected void pnlDatos_Callback(object sender, CallbackEventArgsBase e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    MLimpiaGrilla();


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

        protected void dgvFormA_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
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
        }

        protected void dgvFormB_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
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
        }

        protected void dgvFormC_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
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
        }

        protected void dgvFormD_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
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
        }

        protected void dgvFormE_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
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
        }

        protected void dgvFormF_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
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
        }

        protected void dgvFormG_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
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
        }

        protected void dgvFormH_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
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
        }


        protected void btnImportar_Click(object sender, EventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    Session[CVariablesSesion.Callback] = "1";

                    var strMensaje = string.Empty;
                    if (MValidaDatosIniciales(ref strMensaje))
                    {
                        if (FileUploader.HasFile)
                        {
                            MLimpiaGrilla();
                            string strExtension = Path.GetExtension(FileUploader.FileName).ToUpper();
                            if (strExtension == ".CSV")
                            {
                                var strRutaServer = Path.GetTempPath();
                                var strNombreArchivo = "INC" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + DateTime.Now.Millisecond;

                                Session["PATH"] = strRutaServer + strNombreArchivo;
                                FileUploader.SaveAs((string)Session["PATH"]);

                                MCargarDatos((string)Session["PATH"]);
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError", "LoadingPanel.Hide();", true);


                                dgvFormA.ClientVisible = false;
                                dgvFormB.ClientVisible = false;
                                dgvFormC.ClientVisible = false;
                                dgvFormD.ClientVisible = false;
                                dgvFormE.ClientVisible = false;
                                dgvFormF.ClientVisible = false;
                                dgvFormG.ClientVisible = false;
                                dgvFormH.ClientVisible = false;

                                switch ((int)cmbTipoFormulario.Value)
                                {
                                    case 1:
                                        dgvFormA.ClientVisible = true;
                                        break;
                                    case 2:
                                        dgvFormB.ClientVisible = true;
                                        break;
                                    case 3:
                                        dgvFormC.ClientVisible = true;
                                        break;
                                    case 4:
                                        dgvFormD.ClientVisible = true;
                                        break;
                                    case 5:
                                        dgvFormE.ClientVisible = true;
                                        break;
                                    case 6:
                                        dgvFormF.ClientVisible = true;
                                        break;
                                    case 7:
                                        dgvFormG.ClientVisible = true;
                                        break;
                                    case 8:
                                        dgvFormH.ClientVisible = true;
                                        break;
                                }
                            }
                            else
                            {
                                MImprimirMensaje("Tipo de archivo incorrecto, el formato debe ser CSV.", EnumTiposMensajeAlerta.Error);
                            }
                        }
                        else
                        {
                            MImprimirMensaje("Archivo Inexistente.", EnumTiposMensajeAlerta.Error);
                        }

                        MMuestraCabecera();
                        btnImportar.Enabled = true;
                        lblTipoError.Text = "Registros Observados - Visualización";
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

        protected void btnGrabar_Click(object sender, EventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    string strMensaje = string.Empty;
                    if (MValidaDatosIniciales(ref strMensaje))
                    {
                        long lngNoMovimientos = 1;
                        var lstRegistros = Session[CVariablesSesion.GrillaTransaccion] as List<CRegistrosImportados>;

                        if (lstRegistros != null && lstRegistros.Any())
                        {
                            var lstProtoMovimientos = new List<ObjProtoMovimientoTransaccion>();
                            var strFechaMin = lstRegistros.Min(x => x.StrFechaEmision);
                            var strFechaMax = lstRegistros.Max(x => x.StrFechaEmision);
                            decimal decFechaIni = Convert.ToDecimal(strFechaMin.Substring(6, 4) + strFechaMin.Substring(3, 2) + strFechaMin.Substring(0, 2));
                            decimal decFechaFin = Convert.ToDecimal(strFechaMax.Substring(6, 4) + strFechaMax.Substring(3, 2) + strFechaMax.Substring(0, 2));

                            var tareasMovimientos = _gestion.TareasMovimientos(1, 5, 8, 8, 6, decFechaIni, decFechaFin);
                            if (tareasMovimientos.intCodigo == 1)
                            {
                                int intControl = 1;
                                foreach (var registro in lstRegistros)
                                {
                                    if (intControl == 0)
                                    {
                                        lstProtoMovimientos = new List<ObjProtoMovimientoTransaccion>();
                                        intControl++;
                                    }
                                    else
                                    {
                                        //todo No implementado.
                                    }

                                    ObjProtoMovimientoTransaccion objProtoReg = registro.objProtoMovimiento;
                                    lstProtoMovimientos.Add(objProtoReg);

                                    if (lstProtoMovimientos.Count == 2000)
                                    {
                                        intControl = 0;
                                        MEnviaObjeto(lstProtoMovimientos, (lngNoMovimientos == lstRegistros.Count), lngNoMovimientos, decFechaIni, decFechaFin, 1);

                                        if (lngNoMovimientos == lstRegistros.Count)
                                        {
                                            lstProtoMovimientos = new List<ObjProtoMovimientoTransaccion>();
                                        }
                                        else
                                        {
                                            //todo No implementado.
                                        }
                                    }
                                    else
                                    {
                                        intControl++;
                                    }
                                    lngNoMovimientos++;
                                }

                                if (lstProtoMovimientos.Count > 0)
                                {
                                    MEnviaObjeto(lstProtoMovimientos, true, lngNoMovimientos - 1, decFechaIni, decFechaFin, 1);
                                }
                                else
                                {
                                    //todo No implementado.
                                }
                            }
                            else
                            {
                                MImprimirMensaje(tareasMovimientos.strMensaje,EnumTiposMensajeAlerta.Error);
                            }
                        }
                        else
                        {
                            MImprimirMensaje("No Existen Registros.", EnumTiposMensajeAlerta.Error);
                        }
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

        

        

        

        #endregion

        #region Metodos y funciones

        private bool MValidaSesion()
        {
            bool booResultado;
            //borrar
            //Session[CVariablesSesion.IdUsuario] = "1802";
            //if (Session[CVariablesSesion.IdUsuario] == null)
            //{
            //    try
            //    {
            //        Response.Redirect("~/Sitio/Persona/wfAutenticacion.aspx", false);
            //        HttpContext.Current.ApplicationInstance.CompleteRequest();
            //    }
            //    catch (Exception)
            //    {
            //        string script = string.Format("document.location.href = '{0}');", "~/Sitio/Persona/wfAutenticacion.aspx");
            //        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "redirect", script, true);
            //    }
            //    booResultado = false;
            //}
            //else
            //{
            //    booResultado = true;
            //}
            booResultado = true;
            return booResultado;
        }

        private void ConfigurarFormulario()
        {
            

            //cmbMes.Value = DateTime.Now.Month;
            //cmbGestion.Value = DateTime.Now.Year;

            //cmbTipoOperacion.Enabled = true;
        }

        private void MMuestraCabecera()
        {
            //lblRazonSocial.Text = tlsOperadores.FocusedNode.GetValue("StrDescPadre").ToString();
            //lblNit.Text = tlsOperadores.FocusedNode.GetValue("LngNit").ToString();
            //lblDireccion.Text = (tlsOperadores.FocusedNode.GetValue("StrDireccion") == null) ? "No Definido" : tlsOperadores.FocusedNode.GetValue("StrDireccion").ToString();
        }

        private void MCargaParametricas()
        {
            var objParametricas = new O_PARAMETRICAS_GRAL();
            ////PAISES
            //var objUbicacion = _client.Post<EResultadoUbicacion>(CAppSettings.svc_obtiene_ubicacion,
            //new
            //{
            //    strCredencial = CParametrosHydro.strCredencialFuncionario,
            //    decNivel = 1,
            //    decIdPadre = 0
            //});
            //if (objUbicacion.IntCodigo == 1)
            //{
            //    objParametricas.lstPais = objUbicacion.OResultado;
            //}
            //Session[CVariablesSesion.ListaParametrica] = objParametricas;

            ////CATALOGO
            //var lstObtCatalogo = _client.Post<EResultadoParametrica>(CAppSettings.svc_obtener_catalogo,
            //new
            //{
            //    strLlave = ""
            //});
            //if (lstObtCatalogo.IntCodigo == 1)
            //{
            //    O_OBTENER_CATALOGOS_CTY objCatalogo = lstObtCatalogo.OResultado;
            //    Session[CVariablesSesion.ListaCatalogo] = objCatalogo;
            //}
            //else
            //{
            //    //todo No implementado.
            //}

            //TIPOS DE CONTENEDORES
            //var objParamInfra = new O_PARAMETROS_INFRA();
            //var objContenedor = _client.Post<EResultadoTipoContenedor>(CAppSettings.svc_lista_tipos_contenedores,
            //new
            //{
            //    strCredencial = CParametrosHydro.strCredencialFuncionario,
            //    decFiltro = 1
            //});
            //if (objContenedor.intCodigo == 1)
            //{
            //    objParamInfra.TIPOS_CONTENEDORES = objContenedor.oResultado;
            //}

            //TIPO INFRAESTRUCTURA
            //var objParamInfraestrutura = _clientI.Get<EResultadoParamInfra>(CAppSettings.svc_LListadoParametros + "/" + CParametrosHydro.strCredencialInfra + "/DESC/TIPO_INFRAESTRUCTURA?format=json");
            //if (objParamInfraestrutura.decCodigo == 1 && objParamInfraestrutura.oResultado != null && objParamInfraestrutura.oResultado.Any())
            //{
            //    objParamInfra.TIPO_INFRAESTRUCTURA = objParamInfraestrutura.oResultado;
            //}
            //Session[CVariablesSesion.ListaParamInfra] = objParamInfra;
        }

        private bool MValidaDatosIniciales(ref string strMensaje)
        {
            bool booResultado = true;

            //if (cmbTipoOperacion.Text == "")
            //{
            //    strMensaje = "Introduzca el tipo de operación.";
            //    booResultado = false;
            //}
            //else if (cmbPeriodo.Text == "")
            //{
            //    strMensaje = "Introduzca un periodo valido.";
            //    booResultado = false;
            //}
            return booResultado;
        }


        
        private bool MValidaCondicionesFuncionamiento(ref string strMesnaje)
        {
            bool booRespuesta = true;
            //Valida si exixte DDJJ en periodo
            var objValidacion = _client.Post<EResultadoGenerico>(CAppSettings.svc_validaciones_general,
            new
            {
                strCredencial = CParametrosHydro.strCredencialFuncionario,
                strFiltro = "1",
                strParametro1 = "",
                strParametro2 = "",
                strParametro3 = "",
                strParametro4 = cmbPeriodo.Value + "01000000",
                strParametro5 = cmbPeriodo.Value + DateTime.DaysInMonth(Convert.ToInt32(cmbPeriodo.Value.ToString().Substring(0, 4)), Convert.ToInt32(cmbPeriodo.Value.ToString().Substring(4, 2))).ToString() + "235959",
                strParametro6 = "",
                decAppFechaRegistro = 0,
                decAppIdUsuario = 0
            });
            if (objValidacion.IntCodigo == 1)
            {
                if (objValidacion.OResultado.RESULTADO == 1)
                {
                    //Valida si el tipo de operacion esta habilitado para la accion
                    var objTipoOperacion = _client.Post<EResultadoConfigTipoOperacion>(CAppSettings.svc_listar_autorizacion_accion,
                    new
                    {
                        strCredencial = CParametrosHydro.strCredencialFuncionario,
                        decFiltro = 1,
                        strParametro1 = "",
                        strParametro2 = "",
                        strParametro3 = "",
                        strParametro4 = ""
                    });
                    
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

        private void MCargaArbol()
        {
            //var objArbol = Session[CVariablesSesion.ObjetoArbol] as List<O_OBTIENE_ENTIDADES_CTY>;
            //if (objArbol == null)
            //{
            //    var objResultado = _consulta.ObtenerEntidades(
            //    new EObjetoEnvio5ParametrosProto
            //    {
            //        decFiltro = 1,
            //        strParametro1 = Session[CVariablesSesion.IdUsuario].ToString(),
            //        strParametro2 = CAppSettings.IdActividades,
            //        strParametro3 = CAppSettings.idAplicacion,
            //        strParametro4 = "",
            //        strParametro5 = ""
            //    });
            //    if (objResultado.intCodigo == 1)
            //    {
            //        objArbol = (List<O_OBTIENE_ENTIDADES_CTY>)objResultado.oResultado;
            //        Session[CVariablesSesion.ObjetoArbol] = objArbol;
            //        Session.Add("entidades_usuario", objResultado);
            //    }
            //    else
            //    {
            //        MImprimirMensaje(objResultado.strMensaje, EnumTiposMensajeAlerta.Error);
            //        return;
            //    }
            //}

            //Session[CVariablesSesion.EstructuraArbol] = _cUtil.MObtenerArbolOrdenado(Properties.Settings.Default.intOrdenArbol, objArbol);
        }


        private void MLimpiaGrilla()
        {
            Session.Remove(CVariablesSesion.GrillaTransaccion);
            Session.Remove(CVariablesSesion.GrillaRegObservado);
            //btnImportar.Enabled = false;

            btnGrabar.Enabled = false;
            pnlMensaje.Visible = false;
            dgvFormA.DataSource = null;
            dgvFormA.DataBind();
            dgvFormB.DataSource = null;
            dgvFormB.DataBind();
            dgvFormC.DataSource = null;
            dgvFormC.DataBind();
            dgvFormD.DataSource = null;
            dgvFormD.DataBind();
            dgvFormE.DataSource = null;
            dgvFormE.DataBind();
            dgvFormF.DataSource = null;
            dgvFormF.DataBind();
            dgvFormG.DataSource = null;
            dgvFormG.DataBind();
            dgvFormH.DataSource = null;
            dgvFormH.DataBind();
            dgvTransaccionError.DataSource = null;
            dgvTransaccionError.DataBind();

            MImprimirMensaje("",EnumTiposMensajeAlerta.Limpiar);
        }



        private void MCargarDatos(string strRuta)
        {
            switch ((int)cmbTipoFormulario.Value)
            {
                case 1:
                    MCargaFormA(strRuta);
                    break;
                case 2:
                    MCargaFormB(strRuta);
                    break;
                case 3:
                    MCargaFormC(strRuta);
                    break;
                case 4:
                    MCargaFormD(strRuta);
                    break;
                case 5:
                    MCargaFormE(strRuta);
                    break;
                case 6:
                    MCargaFormF(strRuta);
                    break;
                case 7:
                    MCargaFormG(strRuta);
                    break;
                case 8:
                    MCargaFormH(strRuta);
                    break;
                default:
                    MImprimirMensaje("FORMULARIO INEXISTENTE", EnumTiposMensajeAlerta.Error);
                    return;
            }

            if (dgvTransaccionError.VisibleRowCount == 0)
            {
                btnGrabar.Enabled = true;
            }
        }

        private bool MValidaFormA(string[] values, ref string strMensaje)
        {
            bool strResultado = true;
            try
            {

                if (values.Length != 9)
                {
                    strMensaje = "- Nro. de columnas del registro invalido.";
                }
                else
                {
                    //todo No implementado.
                }
                var arrayNumero = new[] { 4, 5, 6, 7 };
                decimal dec;
                for (int i = 0; i < arrayNumero.Length; i++)
                {
                    if (!decimal.TryParse(values[arrayNumero[i]].Trim(), out dec))
                    {
                        strMensaje += "- Columna Nro. " + arrayNumero[i] + " No es un número.";
                    }
                }
                if (values[0].Trim().Length == 0)
                {
                    strMensaje += "- Titular inválido.";
                }
                else
                {
                    //todo No implementado.
                }
                if (values[1].Trim().Length == 0)
                {
                    strMensaje += "- Operador incorrecto.";
                }
                else
                {
                    //todo No implementado.
                }
                if (values[2].Trim().Length == 0)
                {
                    strMensaje += "- Campo incorrecto.";
                }
                else
                {
                    //todo No implementado.
                }
                if (values[8].Trim().Length == 0)
                {
                    strMensaje += "- Clasificacion del campo incorrecto.";
                }
                else
                {
                    //todo No implementado.
                }
                if (values[8].Trim() != "PETROLIFERO" && values[8].Trim() != "GASIFERO")
                {
                    strMensaje += "- Clasificacion del campo invalido.";
                }
                else
                {
                    //todo No implementado.
                }

                if (1 == 2)
                {
                    //todo validacion del periodo
                    strMensaje += "- Periodo incorrecto.";
                }
                else
                {
                    //todo No implementado.
                }



            }
            catch (Exception ex)
            {
                strMensaje += ex.Message;
            }

            if (strMensaje.Trim().Length > 0)
            {
                strResultado = false;
            }
            else
            {
                //todo No implementado.
            }
            return strResultado;
        }
        private void MCargaFormA(string strRuta)
        {
            var reader = new StreamReader(File.OpenRead(@strRuta));
            var lstRegistros = new List<CFormularioA>();
            var lstRegistroE = new List<CRegistrosError>();

            long lngNoRegCorrect = 1;
            long lngNoRegInCorrect = 1;

            int intNoReg = 1;

            while (!reader.EndOfStream)
            {

                var line = reader.ReadLine();
                var values = line.Split(';');
                try
                {
                    DateTime datFecha, datFechaReg;
                    string strFecha;
                    ObjProtoMovimientoTransaccion objNuevoTipoReg = null;
                    ObjTransaccion objFactura, objDocumento;
                    int estadoTra;
                    List<ObjTransaccion> lstTransacciones;

                    //var to = cmbTipoOperacion.Text.Split(';');
                    //var strTipoOp = to[1].Substring(2, 2);
                    string strMensaje = string.Empty;
                    if (MValidaFormA(values, ref strMensaje))
                    {
                        var cDato = new CFormularioA();

                        cDato.noRegistro = intNoReg;
                        cDato.campo1 = values[0].Trim().Replace("�", "Ñ").ToUpper();
                        cDato.campo2 = values[1].Trim().Replace("�", "Ñ").ToUpper();
                        cDato.campo3 = values[2].Trim().Replace("�", "Ñ").ToUpper();
                        cDato.campo4 = values[3].Trim().Replace("�", "Ñ").ToUpper();
                        cDato.campo5 = Convert.ToDecimal(values[4].Trim());
                        cDato.campo6 = Convert.ToDecimal(values[5].Trim());
                        cDato.campo7 = Convert.ToDecimal(values[6].Trim());
                        cDato.campo8 = Convert.ToDecimal(values[7].Trim());
                        cDato.campo9 = values[8].Trim();


                        lstRegistros.Add(cDato);
                        lngNoRegCorrect++;
                    }
                    else
                    {
                        var cRegError = new CRegistrosError();
                        cRegError.LngId = intNoReg;
                        cRegError.StrIdVenta = "";
                        cRegError.LngFechaEmision = "";
                        cRegError.LngNit = "";
                        cRegError.LngNroAutorizacion = "";
                        cRegError.LngNroFactura = "";
                        cRegError.LngNroReferencia = "";
                        cRegError.StrErrores = strMensaje.Trim();
                        lstRegistroE.Add(cRegError);
                        lngNoRegInCorrect++;
                    }
                }
                catch (Exception ex)
                {
                    var cRegError = new CRegistrosError();
                    cRegError.LngId = intNoReg;
                    cRegError.StrIdVenta = lngNoRegCorrect.ToString(CultureInfo.InvariantCulture);
                    cRegError.LngFechaEmision = "";
                    cRegError.LngNit = "";
                    cRegError.LngNroAutorizacion = "";
                    cRegError.LngNroFactura = "";
                    cRegError.LngNroReferencia = "";
                    cRegError.StrErrores = "ERROR:" + ex.Message;
                    lstRegistroE.Add(cRegError);
                    lngNoRegInCorrect++;
                }
                intNoReg++;
            }

            dgvFormA.DataSource = lstRegistros;
            dgvFormA.DataBind();
            Session[CVariablesSesion.GrillaTransaccion] = lstRegistros;

            dgvTransaccionError.DataSource = lstRegistroE;
            dgvTransaccionError.DataBind();
            Session[CVariablesSesion.GrillaRegObservado] = lstRegistroE;
        }

        private bool MValidaFormB(string[] values, ref string strMensaje)
        {
            bool strResultado = true;
            try
            {

                if (values.Length != 17)
                {
                    strMensaje = "- Nro. de columnas del registro invalido.";
                }
                else
                {
                    //todo No implementado.
                }
                var arrayNumero = new[] { 4, 5, 6, 7, 8, 10, 11, 12, 13, 14, 15 };
                decimal dec;
                for (int i = 0; i < arrayNumero.Length; i++)
                {
                    if (!decimal.TryParse(values[arrayNumero[i]].Trim(), out dec))
                    {
                        strMensaje += "- Columna Nro. " + arrayNumero[i] + " No es un número.";
                    }
                }
                if (values[0].Trim().Length == 0)
                {
                    strMensaje += "- Titular inválido.";
                }
                else
                {
                    //todo No implementado.
                }
                if (values[1].Trim().Length == 0)
                {
                    strMensaje += "- Operador incorrecto.";
                }
                else
                {
                    //todo No implementado.
                }
                if (values[2].Trim().Length == 0)
                {
                    strMensaje += "- Campo incorrecto.";
                }
                else
                {
                    //todo No implementado.
                }
                if (values[9].Trim().Length == 0)
                {
                    strMensaje += "- Campo gasifero marginal incorrecto.";
                }
                else
                {
                    //todo No implementado.
                }
                if (values[9].Trim() != "SI" && values[9].Trim() != "NO")
                {
                    strMensaje += "- Campo gasifero marginal incorrecto.";
                }
                else
                {
                    //todo No implementado.
                }
                if (values[16].Trim().Length == 0)
                {
                    strMensaje += "- Campo gasifero pequeño incorrecto.";
                }
                else
                {
                    //todo No implementado.
                }
                if (values[16].Trim() != "SI" && values[16].Trim() != "NO")
                {
                    strMensaje += "- Campo gasifero pequeño incorrecto.";
                }
                else
                {
                    //todo No implementado.
                }

                if (1 == 2)
                {
                    //todo validacion del periodo
                    strMensaje += "- Periodo incorrecto.";
                }
                else
                {
                    //todo No implementado.
                }



            }
            catch (Exception ex)
            {
                strMensaje += ex.Message;
            }

            if (strMensaje.Trim().Length > 0)
            {
                strResultado = false;
            }
            else
            {
                //todo No implementado.
            }
            return strResultado;
        }
        private void MCargaFormB(string strRuta)
        {
            var reader = new StreamReader(File.OpenRead(@strRuta));
            var lstRegistros = new List<CFormularioB>();
            var lstRegistroE = new List<CRegistrosError>();

            long lngNoRegCorrect = 1;
            long lngNoRegInCorrect = 1;

            int intNoReg = 1;

            while (!reader.EndOfStream)
            {

                var line = reader.ReadLine();
                var values = line.Split(';');
                try
                {
                    string strMensaje = string.Empty;
                    if (MValidaFormB(values, ref strMensaje))
                    {
                        var cDato = new CFormularioB();

                        cDato.noRegistro = intNoReg;
                        cDato.campo1 = values[0].Trim().Replace("�", "Ñ").ToUpper();
                        cDato.campo2 = values[1].Trim().Replace("�", "Ñ").ToUpper();
                        cDato.campo3 = values[2].Trim().Replace("�", "Ñ").ToUpper();
                        cDato.campo4 = values[3].Trim().Replace("�", "Ñ").ToUpper();
                        cDato.campo5 = Convert.ToDecimal(values[4].Trim());
                        cDato.campo6 = Convert.ToDecimal(values[5].Trim());
                        cDato.campo7 = Convert.ToDecimal(values[6].Trim());
                        cDato.campo8 = Convert.ToDecimal(values[7].Trim());
                        cDato.campo9 = Convert.ToDecimal(values[8].Trim());
                        cDato.campo10 = values[9].Trim().ToUpper();
                        cDato.campo11 = Convert.ToDecimal(values[10].Trim());
                        cDato.campo12 = Convert.ToDecimal(values[11].Trim());
                        cDato.campo13 = Convert.ToDecimal(values[12].Trim());
                        cDato.campo14 = Convert.ToDecimal(values[13].Trim());
                        cDato.campo15 = Convert.ToDecimal(values[14].Trim());
                        cDato.campo16 = Convert.ToDecimal(values[15].Trim());
                        cDato.campo17 = values[16].Trim().ToUpper();




                        lstRegistros.Add(cDato);
                        lngNoRegCorrect++;
                    }
                    else
                    {
                        var cRegError = new CRegistrosError();
                        cRegError.LngId = intNoReg;
                        cRegError.StrIdVenta = "";
                        cRegError.LngFechaEmision = "";
                        cRegError.LngNit = "";
                        cRegError.LngNroAutorizacion = "";
                        cRegError.LngNroFactura = "";
                        cRegError.LngNroReferencia = "";
                        cRegError.StrErrores = strMensaje.Trim();
                        lstRegistroE.Add(cRegError);
                        lngNoRegInCorrect++;
                    }
                }
                catch (Exception ex)
                {
                    var cRegError = new CRegistrosError();
                    cRegError.LngId = intNoReg;
                    cRegError.StrIdVenta = lngNoRegCorrect.ToString(CultureInfo.InvariantCulture);
                    cRegError.LngFechaEmision = "";
                    cRegError.LngNit = "";
                    cRegError.LngNroAutorizacion = "";
                    cRegError.LngNroFactura = "";
                    cRegError.LngNroReferencia = "";
                    cRegError.StrErrores = "ERROR:" + ex.Message;
                    lstRegistroE.Add(cRegError);
                    lngNoRegInCorrect++;
                }
                intNoReg++;
            }

            dgvFormB.DataSource = lstRegistros;
            dgvFormB.DataBind();
            Session[CVariablesSesion.GrillaTransaccion] = lstRegistros;

            dgvTransaccionError.DataSource = lstRegistroE;
            dgvTransaccionError.DataBind();
            Session[CVariablesSesion.GrillaRegObservado] = lstRegistroE;
        }

        private bool MValidaFormC(string[] values, ref string strMensaje)
        {
            bool strResultado = true;
            try
            {
                if (values.Length != 8)
                {
                    strMensaje = "- Nro. de columnas del registro invalido.";
                }
                else
                {
                    //todo No implementado.
                }
                var arrayNumero = new[] { 4, 5, 6 };
                decimal dec;
                for (int i = 0; i < arrayNumero.Length; i++)
                {
                    if (!decimal.TryParse(values[arrayNumero[i]].Trim(), out dec))
                    {
                        strMensaje += "- Columna Nro. " + arrayNumero[i] + " No es un número.";
                    }
                }
                if (values[0].Trim().Length == 0)
                {
                    strMensaje += "- Titular inválido.";
                }
                else
                {
                    //todo No implementado.
                }
                if (values[1].Trim().Length == 0)
                {
                    strMensaje += "- Operador incorrecto.";
                }
                else
                {
                    //todo No implementado.
                }
                if (values[2].Trim().Length == 0)
                {
                    strMensaje += "- Campo incorrecto.";
                }
                else
                {
                    //todo No implementado.
                }
                if (values[7].Trim().Length == 0)
                {
                    strMensaje += "- Aplica incentivo incorrecto.";
                }
                else
                {
                    //todo No implementado.
                }
                if (values[7].Trim() != "SI" && values[7].Trim() != "NO")
                {
                    strMensaje += "- Aplica incentivo invalido.";
                }
                else
                {
                    //todo No implementado.
                }

                if (1 == 2)
                {
                    //todo validacion del periodo
                    strMensaje += "- Periodo incorrecto.";
                }
                else
                {
                    //todo No implementado.
                }
            }
            catch (Exception ex)
            {
                strMensaje += ex.Message;
            }

            if (strMensaje.Trim().Length > 0)
            {
                strResultado = false;
            }
            else
            {
                //todo No implementado.
            }
            return strResultado;
        }
        private void MCargaFormC(string strRuta)
        {
            var reader = new StreamReader(File.OpenRead(@strRuta));
            var lstRegistros = new List<CFormularioC>();
            var lstRegistroE = new List<CRegistrosError>();

            long lngNoRegCorrect = 1;
            long lngNoRegInCorrect = 1;

            int intNoReg = 1;

            while (!reader.EndOfStream)
            {

                var line = reader.ReadLine();
                var values = line.Split(';');
                try
                {
                    string strMensaje = string.Empty;
                    if (MValidaFormC(values, ref strMensaje))
                    {
                        var cDato = new CFormularioC();

                        cDato.noRegistro = intNoReg;
                        cDato.campo1 = values[0].Trim().Replace("�", "Ñ").ToUpper();
                        cDato.campo2 = values[1].Trim().Replace("�", "Ñ").ToUpper();
                        cDato.campo3 = values[2].Trim().Replace("�", "Ñ").ToUpper();
                        cDato.campo4 = values[3].Trim().Replace("�", "Ñ").ToUpper();
                        cDato.campo5 = Convert.ToDecimal(values[4].Trim());
                        cDato.campo6 = Convert.ToDecimal(values[5].Trim());
                        cDato.campo7 = Convert.ToDecimal(values[6].Trim());
                        cDato.campo8 = values[7].Trim();

                        lstRegistros.Add(cDato);
                        lngNoRegCorrect++;
                    }
                    else
                    {
                        var cRegError = new CRegistrosError();
                        cRegError.LngId = intNoReg;
                        cRegError.StrIdVenta = "";
                        cRegError.LngFechaEmision = "";
                        cRegError.LngNit = "";
                        cRegError.LngNroAutorizacion = "";
                        cRegError.LngNroFactura = "";
                        cRegError.LngNroReferencia = "";
                        cRegError.StrErrores = strMensaje.Trim();
                        lstRegistroE.Add(cRegError);
                        lngNoRegInCorrect++;
                    }
                }
                catch (Exception ex)
                {
                    var cRegError = new CRegistrosError();
                    cRegError.LngId = intNoReg;
                    cRegError.StrIdVenta = lngNoRegCorrect.ToString(CultureInfo.InvariantCulture);
                    cRegError.LngFechaEmision = "";
                    cRegError.LngNit = "";
                    cRegError.LngNroAutorizacion = "";
                    cRegError.LngNroFactura = "";
                    cRegError.LngNroReferencia = "";
                    cRegError.StrErrores = "ERROR:" + ex.Message;
                    lstRegistroE.Add(cRegError);
                    lngNoRegInCorrect++;
                }
                intNoReg++;
            }

            dgvFormC.DataSource = lstRegistros;
            dgvFormC.DataBind();
            Session[CVariablesSesion.GrillaTransaccion] = lstRegistros;

            dgvTransaccionError.DataSource = lstRegistroE;
            dgvTransaccionError.DataBind();
            Session[CVariablesSesion.GrillaRegObservado] = lstRegistroE;
        }
        
        private bool MValidaFormD(string[] values, ref string strMensaje)
        {
            bool strResultado = true;
            try
            {
                if (values.Length != 25)
                {
                    strMensaje = "- Nro. de columnas del registro invalido.";
                }
                else
                {
                    //todo No implementado.
                }
                var arrayNumero = new[] { 13, 14, 15, 16, 17, 18, 19, 20 };
                decimal dec;
                for (int i = 0; i < arrayNumero.Length; i++)
                {
                    if (!decimal.TryParse(values[arrayNumero[i]].Trim(), out dec))
                    {
                        strMensaje += "- Columna Nro. " + arrayNumero[i] + " No es un número.";
                    }
                }
                arrayNumero = new[] { 4, 7, 9, 11, 12, 22, 24 };
                DateTime dt;
                for (int i = 0; i < arrayNumero.Length; i++)
                {
                    if (!DateTime.TryParse(values[arrayNumero[i]].Trim(), out dt))
                    {
                        strMensaje += "- Columna Nro. " + arrayNumero[i] + " No es un fecha.";
                    }
                }
                if (values[0].Trim().Length == 0)
                {
                    strMensaje += "- Titular inválido.";
                }
                else
                {
                    //todo No implementado.
                }
                if (values[1].Trim().Length == 0)
                {
                    strMensaje += "- Operador incorrecto.";
                }
                else
                {
                    //todo No implementado.
                }
                if (values[2].Trim().Length == 0)
                {
                    strMensaje += "- Campo incorrecto.";
                }
                else
                {
                    //todo No implementado.
                }
                if (values[3].Trim().Length == 0)
                {
                    strMensaje += "- Nuevo campo incorrecto.";
                }
                else
                {
                    //todo No implementado.
                }
                if (values[3].Trim() != "SI" && values[3].Trim() != "NO")
                {
                    strMensaje += "- Nuevo campo invalido.";
                }
                else
                {
                    //todo No implementado.
                }
                if (values[6].Trim().Length == 0)
                {
                    strMensaje += "- Nuevo reservorio incorrecto.";
                }
                else
                {
                    //todo No implementado.
                }
                if (values[6].Trim() != "SI" && values[6].Trim() != "NO")
                {
                    strMensaje += "- Nuevo reservorio invalido.";
                }
                else
                {
                    //todo No implementado.
                }
                if (values[8].Trim().Length == 0)
                {
                    strMensaje += "- Periodo etapa de evaluacion incorrecto.";
                }
                else
                {
                    //todo No implementado.
                }
                if (values[8].Trim() != "SI" && values[8].Trim() != "NO")
                {
                    strMensaje += "- Periodo etapa de evaluacion invalido.";
                }
                else
                {
                    //todo No implementado.
                }
                if (values[10].Trim().Length == 0)
                {
                    strMensaje += "- En produccion a la fecha de publicacion incorrecto.";
                }
                else
                {
                    //todo No implementado.
                }
                if (values[10].Trim() != "SI" && values[10].Trim() != "NO")
                {
                    strMensaje += "- En produccion a la fecha de publicacion invalido.";
                }
                else
                {
                    //todo No implementado.
                }

                if (1 == 2)
                {
                    //todo validacion del periodo
                    strMensaje += "- Periodo incorrecto.";
                }
                else
                {
                    //todo No implementado.
                }
            }
            catch (Exception ex)
            {
                strMensaje += ex.Message;
            }

            if (strMensaje.Trim().Length > 0)
            {
                strResultado = false;
            }
            else
            {
                //todo No implementado.
            }
            return strResultado;
        }
        private void MCargaFormD(string strRuta)
        {
            var reader = new StreamReader(File.OpenRead(@strRuta));
            var lstRegistros = new List<CFormularioD>();
            var lstRegistroE = new List<CRegistrosError>();

            long lngNoRegCorrect = 1;
            long lngNoRegInCorrect = 1;

            int intNoReg = 1;

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(';');
                try
                {
                    string strMensaje = string.Empty;
                    if (MValidaFormD(values, ref strMensaje))
                    {
                        var cDato = new CFormularioD();

                        cDato.noRegistro = intNoReg;
                        cDato.campo1 = values[0].Trim().Replace("�", "Ñ").ToUpper();
                        cDato.campo2 = values[1].Trim().Replace("�", "Ñ").ToUpper();
                        cDato.campo3 = values[2].Trim().Replace("�", "Ñ").ToUpper();
                        cDato.campo4 = values[3].Trim();
                        cDato.campo5 = values[4].Trim();
                        cDato.campo6 = values[5].Trim().Replace("�", "Ñ").ToUpper();
                        cDato.campo7 = values[6].Trim();
                        cDato.campo8 = values[7].Trim();
                        cDato.campo9 = values[8].Trim();
                        cDato.campo10 = values[9].Trim();
                        cDato.campo11 = values[10].Trim();
                        cDato.campo12 = values[11].Trim();
                        cDato.campo13 = values[12].Trim();
                        cDato.campo14 = Convert.ToDecimal(values[13].Trim());
                        cDato.campo15 = Convert.ToDecimal(values[14].Trim());
                        cDato.campo16 = Convert.ToDecimal(values[15].Trim());
                        cDato.campo17 = Convert.ToDecimal(values[16].Trim());
                        cDato.campo18 = Convert.ToDecimal(values[17].Trim());
                        cDato.campo19 = Convert.ToDecimal(values[18].Trim());
                        cDato.campo20 = Convert.ToDecimal(values[19].Trim());
                        cDato.campo21 = Convert.ToDecimal(values[20].Trim());
                        cDato.campo22 = values[21].Trim();
                        cDato.campo23 = values[22].Trim();
                        cDato.campo24 = values[23].Trim();
                        cDato.campo25 = values[24].Trim();

                        lstRegistros.Add(cDato);
                        lngNoRegCorrect++;
                    }
                    else
                    {
                        var cRegError = new CRegistrosError();
                        cRegError.LngId = intNoReg;
                        cRegError.StrIdVenta = "";
                        cRegError.LngFechaEmision = "";
                        cRegError.LngNit = "";
                        cRegError.LngNroAutorizacion = "";
                        cRegError.LngNroFactura = "";
                        cRegError.LngNroReferencia = "";
                        cRegError.StrErrores = strMensaje.Trim();
                        lstRegistroE.Add(cRegError);
                        lngNoRegInCorrect++;
                    }
                }
                catch (Exception ex)
                {
                    var cRegError = new CRegistrosError();
                    cRegError.LngId = intNoReg;
                    cRegError.StrIdVenta = lngNoRegCorrect.ToString(CultureInfo.InvariantCulture);
                    cRegError.LngFechaEmision = "";
                    cRegError.LngNit = "";
                    cRegError.LngNroAutorizacion = "";
                    cRegError.LngNroFactura = "";
                    cRegError.LngNroReferencia = "";
                    cRegError.StrErrores = "ERROR:" + ex.Message;
                    lstRegistroE.Add(cRegError);
                    lngNoRegInCorrect++;
                }
                intNoReg++;
            }

            dgvFormD.DataSource = lstRegistros;
            dgvFormD.DataBind();
            Session[CVariablesSesion.GrillaTransaccion] = lstRegistros;

            dgvTransaccionError.DataSource = lstRegistroE;
            dgvTransaccionError.DataBind();
            Session[CVariablesSesion.GrillaRegObservado] = lstRegistroE;
        }

        private bool MValidaFormE(string[] values, ref string strMensaje)
        {
            bool strResultado = true;
            try
            {
                if (values.Length != 22)
                {
                    strMensaje = "- Nro. de columnas del registro invalido.";
                }
                else
                {
                    //todo No implementado.
                }
                var arrayNumero = new[] { 11, 12, 13, 16, 17, 18, 19, 20, 21 };
                decimal dec;
                for (int i = 0; i < arrayNumero.Length; i++)
                {
                    if (!decimal.TryParse(values[arrayNumero[i]].Trim(), out dec))
                    {
                        strMensaje += "- Columna Nro. " + arrayNumero[i] + " No es un número.";
                    }
                }
                arrayNumero = new[] { 4, 7, 9, 10, 14 };
                DateTime dt;
                for (int i = 0; i < arrayNumero.Length; i++)
                {
                    if (!DateTime.TryParse(values[arrayNumero[i]].Trim(), out dt))
                    {
                        strMensaje += "- Columna Nro. " + arrayNumero[i] + " No es un fecha.";
                    }
                }
                if (values[0].Trim().Length == 0)
                {
                    strMensaje += "- Titular inválido.";
                }
                else
                {
                    //todo No implementado.
                }
                if (values[1].Trim().Length == 0)
                {
                    strMensaje += "- Operador incorrecto.";
                }
                else
                {
                    //todo No implementado.
                }
                if (values[2].Trim().Length == 0)
                {
                    strMensaje += "- Campo incorrecto.";
                }
                else
                {
                    //todo No implementado.
                }
                if (values[3].Trim().Length == 0)
                {
                    strMensaje += "- Nuevo campo incorrecto.";
                }
                else
                {
                    //todo No implementado.
                }
                if (values[3].Trim() != "SI" && values[3].Trim() != "NO")
                {
                    strMensaje += "- Nuevo campo invalido.";
                }
                else
                {
                    //todo No implementado.
                }
                if (values[6].Trim().Length == 0)
                {
                    strMensaje += "- Nuevo reservorio incorrecto.";
                }
                else
                {
                    //todo No implementado.
                }
                if (values[6].Trim() != "SI" && values[6].Trim() != "NO")
                {
                    strMensaje += "- Nuevo reservorio invalido.";
                }
                else
                {
                    //todo No implementado.
                }
                

                if (1 == 2)
                {
                    //todo validacion del periodo
                    strMensaje += "- Periodo incorrecto.";
                }
                else
                {
                    //todo No implementado.
                }
            }
            catch (Exception ex)
            {
                strMensaje += ex.Message;
            }

            if (strMensaje.Trim().Length > 0)
            {
                strResultado = false;
            }
            else
            {
                //todo No implementado.
            }
            return strResultado;
        }
        private void MCargaFormE(string strRuta)
        {
            var reader = new StreamReader(File.OpenRead(@strRuta));
            var lstRegistros = new List<CFormularioE>();
            var lstRegistroE = new List<CRegistrosError>();

            long lngNoRegCorrect = 1;
            long lngNoRegInCorrect = 1;

            int intNoReg = 1;

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(';');
                try
                {
                    string strMensaje = string.Empty;
                    if (MValidaFormE(values, ref strMensaje))
                    {
                        var cDato = new CFormularioE();

                        cDato.noRegistro = intNoReg;
                        cDato.campo1 = values[0].Trim().Replace("�", "Ñ").ToUpper();
                        cDato.campo2 = values[1].Trim().Replace("�", "Ñ").ToUpper();
                        cDato.campo3 = values[2].Trim().Replace("�", "Ñ").ToUpper();
                        cDato.campo4 = values[3].Trim();
                        cDato.campo5 = values[4].Trim();
                        cDato.campo6 = values[5].Trim().Replace("�", "Ñ").ToUpper();
                        cDato.campo7 = values[6].Trim();
                        cDato.campo8 = values[7].Trim();
                        cDato.campo9 = values[8].Trim();
                        cDato.campo10 = values[9].Trim();
                        cDato.campo11 = values[10].Trim();
                        cDato.campo12 = Convert.ToDecimal(values[11].Trim());
                        cDato.campo13 = Convert.ToDecimal(values[12].Trim());
                        cDato.campo14 = Convert.ToDecimal(values[13].Trim());
                        cDato.campo15 = values[14].Trim();
                        cDato.campo16 = values[15].Trim();
                        cDato.campo17 = Convert.ToDecimal(values[16].Trim());
                        cDato.campo18 = Convert.ToDecimal(values[17].Trim());
                        cDato.campo19 = Convert.ToDecimal(values[18].Trim());
                        cDato.campo20 = Convert.ToDecimal(values[19].Trim());
                        cDato.campo21 = Convert.ToDecimal(values[20].Trim());
                        cDato.campo22 = Convert.ToDecimal(values[21].Trim());

                        lstRegistros.Add(cDato);
                        lngNoRegCorrect++;
                    }
                    else
                    {
                        var cRegError = new CRegistrosError();
                        cRegError.LngId = intNoReg;
                        cRegError.StrIdVenta = "";
                        cRegError.LngFechaEmision = "";
                        cRegError.LngNit = "";
                        cRegError.LngNroAutorizacion = "";
                        cRegError.LngNroFactura = "";
                        cRegError.LngNroReferencia = "";
                        cRegError.StrErrores = strMensaje.Trim();
                        lstRegistroE.Add(cRegError);
                        lngNoRegInCorrect++;
                    }
                }
                catch (Exception ex)
                {
                    var cRegError = new CRegistrosError();
                    cRegError.LngId = intNoReg;
                    cRegError.StrIdVenta = lngNoRegCorrect.ToString(CultureInfo.InvariantCulture);
                    cRegError.LngFechaEmision = "";
                    cRegError.LngNit = "";
                    cRegError.LngNroAutorizacion = "";
                    cRegError.LngNroFactura = "";
                    cRegError.LngNroReferencia = "";
                    cRegError.StrErrores = "ERROR:" + ex.Message;
                    lstRegistroE.Add(cRegError);
                    lngNoRegInCorrect++;
                }
                intNoReg++;
            }

            dgvFormE.DataSource = lstRegistros;
            dgvFormE.DataBind();
            Session[CVariablesSesion.GrillaTransaccion] = lstRegistros;

            dgvTransaccionError.DataSource = lstRegistroE;
            dgvTransaccionError.DataBind();
            Session[CVariablesSesion.GrillaRegObservado] = lstRegistroE;
        }

        private bool MValidaFormF(string[] values, ref string strMensaje)
        {
            bool strResultado = true;
            try
            {
                if (values.Length != 21)
                {
                    strMensaje = "- Nro. de columnas del registro invalido.";
                }
                else
                {
                    //todo No implementado.
                }
                var arrayNumero = new[] { 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
                decimal dec;
                for (int i = 0; i < arrayNumero.Length; i++)
                {
                    if (!decimal.TryParse(values[arrayNumero[i]].Trim(), out dec))
                    {
                        strMensaje += "- Columna Nro. " + arrayNumero[i] + " No es un número.";
                    }
                }
                if (values[0].Trim().Length == 0)
                {
                    strMensaje += "- Titular inválido.";
                }
                else
                {
                    //todo No implementado.
                }
                if (values[1].Trim().Length == 0)
                {
                    strMensaje += "- Operador incorrecto.";
                }
                else
                {
                    //todo No implementado.
                }
                if (values[2].Trim().Length == 0)
                {
                    strMensaje += "- Campo incorrecto.";
                }
                else
                {
                    //todo No implementado.
                }
                
                if (1 == 2)
                {
                    //todo validacion del periodo
                    strMensaje += "- Periodo incorrecto.";
                }
                else
                {
                    //todo No implementado.
                }
            }
            catch (Exception ex)
            {
                strMensaje += ex.Message;
            }

            if (strMensaje.Trim().Length > 0)
            {
                strResultado = false;
            }
            else
            {
                //todo No implementado.
            }
            return strResultado;
        }
        private void MCargaFormF(string strRuta)
        {
            var reader = new StreamReader(File.OpenRead(@strRuta));
            var lstRegistros = new List<CFormularioF>();
            var lstRegistroE = new List<CRegistrosError>();

            long lngNoRegCorrect = 1;
            long lngNoRegInCorrect = 1;

            int intNoReg = 1;

            while (!reader.EndOfStream)
            {

                var line = reader.ReadLine();
                var values = line.Split(';');
                try
                {
                    string strMensaje = string.Empty;
                    if (MValidaFormF(values, ref strMensaje))
                    {
                        var cDato = new CFormularioF();

                        cDato.noRegistro = intNoReg;
                        cDato.campo1 = values[0].Trim().Replace("�", "Ñ").ToUpper();
                        cDato.campo2 = values[1].Trim().Replace("�", "Ñ").ToUpper();
                        cDato.campo3 = values[2].Trim().Replace("�", "Ñ").ToUpper();
                        cDato.campo4 = values[3].Trim().Replace("�", "Ñ").ToUpper();
                        cDato.campo5 = Convert.ToDecimal(values[4].Trim());
                        cDato.campo6 = Convert.ToDecimal(values[5].Trim());
                        cDato.campo7 = Convert.ToDecimal(values[6].Trim());
                        cDato.campo8 = Convert.ToDecimal(values[7].Trim());
                        cDato.campo9 = Convert.ToDecimal(values[8].Trim());
                        cDato.campo10 = Convert.ToDecimal(values[9].Trim());
                        cDato.campo11 = Convert.ToDecimal(values[10].Trim());
                        cDato.campo12 = Convert.ToDecimal(values[11].Trim());
                        cDato.campo13 = Convert.ToDecimal(values[12].Trim());
                        cDato.campo14 = Convert.ToDecimal(values[13].Trim());
                        cDato.campo15 = Convert.ToDecimal(values[14].Trim());
                        cDato.campo16 = Convert.ToDecimal(values[15].Trim());
                        cDato.campo17 = Convert.ToDecimal(values[16].Trim());
                        cDato.campo18 = Convert.ToDecimal(values[17].Trim());
                        cDato.campo19 = Convert.ToDecimal(values[18].Trim());
                        cDato.campo20 = Convert.ToDecimal(values[19].Trim());
                        cDato.campo21 = Convert.ToDecimal(values[20].Trim());

                        lstRegistros.Add(cDato);
                        lngNoRegCorrect++;
                    }
                    else
                    {
                        var cRegError = new CRegistrosError();
                        cRegError.LngId = intNoReg;
                        cRegError.StrIdVenta = "";
                        cRegError.LngFechaEmision = "";
                        cRegError.LngNit = "";
                        cRegError.LngNroAutorizacion = "";
                        cRegError.LngNroFactura = "";
                        cRegError.LngNroReferencia = "";
                        cRegError.StrErrores = strMensaje.Trim();
                        lstRegistroE.Add(cRegError);
                        lngNoRegInCorrect++;
                    }
                }
                catch (Exception ex)
                {
                    var cRegError = new CRegistrosError();
                    cRegError.LngId = intNoReg;
                    cRegError.StrIdVenta = lngNoRegCorrect.ToString(CultureInfo.InvariantCulture);
                    cRegError.LngFechaEmision = "";
                    cRegError.LngNit = "";
                    cRegError.LngNroAutorizacion = "";
                    cRegError.LngNroFactura = "";
                    cRegError.LngNroReferencia = "";
                    cRegError.StrErrores = "ERROR:" + ex.Message;
                    lstRegistroE.Add(cRegError);
                    lngNoRegInCorrect++;
                }
                intNoReg++;
            }

            dgvFormF.DataSource = lstRegistros;
            dgvFormF.DataBind();
            Session[CVariablesSesion.GrillaTransaccion] = lstRegistros;

            dgvTransaccionError.DataSource = lstRegistroE;
            dgvTransaccionError.DataBind();
            Session[CVariablesSesion.GrillaRegObservado] = lstRegistroE;
        }

        private bool MValidaFormG(string[] values, ref string strMensaje)
        {
            bool strResultado = true;
            try
            {
                if (values.Length != 20)
                {
                    strMensaje = "- Nro. de columnas del registro invalido.";
                }
                else
                {
                    //todo No implementado.
                }
                var arrayNumero = new[] { 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19 };
                decimal dec;
                for (int i = 0; i < arrayNumero.Length; i++)
                {
                    if (!decimal.TryParse(values[arrayNumero[i]].Trim(), out dec))
                    {
                        strMensaje += "- Columna Nro. " + arrayNumero[i] + " No es un número.";
                    }
                }
                if (values[0].Trim().Length == 0)
                {
                    strMensaje += "- Titular inválido.";
                }
                else
                {
                    //todo No implementado.
                }
                if (values[1].Trim().Length == 0)
                {
                    strMensaje += "- Operador incorrecto.";
                }
                else
                {
                    //todo No implementado.
                }
                if (values[2].Trim().Length == 0)
                {
                    strMensaje += "- Campo incorrecto.";
                }
                else
                {
                    //todo No implementado.
                }

                if (1 == 2)
                {
                    //todo validacion del periodo
                    strMensaje += "- Periodo incorrecto.";
                }
                else
                {
                    //todo No implementado.
                }
            }
            catch (Exception ex)
            {
                strMensaje += ex.Message;
            }

            if (strMensaje.Trim().Length > 0)
            {
                strResultado = false;
            }
            else
            {
                //todo No implementado.
            }
            return strResultado;
        }
        private void MCargaFormG(string strRuta)
        {
            var reader = new StreamReader(File.OpenRead(@strRuta));
            var lstRegistros = new List<CFormularioG>();
            var lstRegistroE = new List<CRegistrosError>();

            long lngNoRegCorrect = 1;
            long lngNoRegInCorrect = 1;

            int intNoReg = 1;

            while (!reader.EndOfStream)
            {

                var line = reader.ReadLine();
                var values = line.Split(';');
                try
                {
                    string strMensaje = string.Empty;
                    if (MValidaFormG(values, ref strMensaje))
                    {
                        var cDato = new CFormularioG();

                        cDato.noRegistro = intNoReg;
                        cDato.campo1 = values[0].Trim().Replace("�", "Ñ").ToUpper();
                        cDato.campo2 = values[1].Trim().Replace("�", "Ñ").ToUpper();
                        cDato.campo3 = values[2].Trim().Replace("�", "Ñ").ToUpper();
                        cDato.campo4 = values[3].Trim().Replace("�", "Ñ").ToUpper();
                        cDato.campo5 = Convert.ToDecimal(values[4].Trim());
                        cDato.campo6 = Convert.ToDecimal(values[5].Trim());
                        cDato.campo7 = Convert.ToDecimal(values[6].Trim());
                        cDato.campo8 = Convert.ToDecimal(values[7].Trim());
                        cDato.campo9 = Convert.ToDecimal(values[8].Trim());
                        cDato.campo10 = Convert.ToDecimal(values[9].Trim());
                        cDato.campo11 = Convert.ToDecimal(values[10].Trim());
                        cDato.campo12 = Convert.ToDecimal(values[11].Trim());
                        cDato.campo13 = Convert.ToDecimal(values[12].Trim());
                        cDato.campo14 = Convert.ToDecimal(values[13].Trim());
                        cDato.campo15 = Convert.ToDecimal(values[14].Trim());
                        cDato.campo16 = Convert.ToDecimal(values[15].Trim());
                        cDato.campo17 = Convert.ToDecimal(values[16].Trim());
                        cDato.campo18 = Convert.ToDecimal(values[17].Trim());
                        cDato.campo19 = Convert.ToDecimal(values[18].Trim());
                        cDato.campo20 = Convert.ToDecimal(values[19].Trim());

                        lstRegistros.Add(cDato);
                        lngNoRegCorrect++;
                    }
                    else
                    {
                        var cRegError = new CRegistrosError();
                        cRegError.LngId = intNoReg;
                        cRegError.StrIdVenta = "";
                        cRegError.LngFechaEmision = "";
                        cRegError.LngNit = "";
                        cRegError.LngNroAutorizacion = "";
                        cRegError.LngNroFactura = "";
                        cRegError.LngNroReferencia = "";
                        cRegError.StrErrores = strMensaje.Trim();
                        lstRegistroE.Add(cRegError);
                        lngNoRegInCorrect++;
                    }
                }
                catch (Exception ex)
                {
                    var cRegError = new CRegistrosError();
                    cRegError.LngId = intNoReg;
                    cRegError.StrIdVenta = lngNoRegCorrect.ToString(CultureInfo.InvariantCulture);
                    cRegError.LngFechaEmision = "";
                    cRegError.LngNit = "";
                    cRegError.LngNroAutorizacion = "";
                    cRegError.LngNroFactura = "";
                    cRegError.LngNroReferencia = "";
                    cRegError.StrErrores = "ERROR:" + ex.Message;
                    lstRegistroE.Add(cRegError);
                    lngNoRegInCorrect++;
                }
                intNoReg++;
            }

            dgvFormG.DataSource = lstRegistros;
            dgvFormG.DataBind();
            Session[CVariablesSesion.GrillaTransaccion] = lstRegistros;

            dgvTransaccionError.DataSource = lstRegistroE;
            dgvTransaccionError.DataBind();
            Session[CVariablesSesion.GrillaRegObservado] = lstRegistroE;
        }

        private bool MValidaFormH(string[] values, ref string strMensaje)
        {
            bool strResultado = true;
            try
            {
                if (values.Length != 21)
                {
                    strMensaje = "- Nro. de columnas del registro invalido.";
                }
                else
                {
                    //todo No implementado.
                }
                var arrayNumero = new[] { 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
                decimal dec;
                for (int i = 0; i < arrayNumero.Length; i++)
                {
                    if (!decimal.TryParse(values[arrayNumero[i]].Trim(), out dec))
                    {
                        strMensaje += "- Columna Nro. " + arrayNumero[i] + " No es un número.";
                    }
                }
                if (values[0].Trim().Length == 0)
                {
                    strMensaje += "- Titular inválido.";
                }
                else
                {
                    //todo No implementado.
                }
                if (values[1].Trim().Length == 0)
                {
                    strMensaje += "- Operador incorrecto.";
                }
                else
                {
                    //todo No implementado.
                }
                if (values[2].Trim().Length == 0)
                {
                    strMensaje += "- Campo incorrecto.";
                }
                else
                {
                    //todo No implementado.
                }

                if (1 == 2)
                {
                    //todo validacion del periodo
                    strMensaje += "- Periodo incorrecto.";
                }
                else
                {
                    //todo No implementado.
                }
            }
            catch (Exception ex)
            {
                strMensaje += ex.Message;
            }

            if (strMensaje.Trim().Length > 0)
            {
                strResultado = false;
            }
            else
            {
                //todo No implementado.
            }
            return strResultado;
        }
        private void MCargaFormH(string strRuta)
        {
            var reader = new StreamReader(File.OpenRead(@strRuta));
            var lstRegistros = new List<CFormularioH>();
            var lstRegistroE = new List<CRegistrosError>();

            long lngNoRegCorrect = 1;
            long lngNoRegInCorrect = 1;

            int intNoReg = 1;

            while (!reader.EndOfStream)
            {

                var line = reader.ReadLine();
                var values = line.Split(';');
                try
                {
                    string strMensaje = string.Empty;
                    if (MValidaFormH(values, ref strMensaje))
                    {
                        var cDato = new CFormularioH();

                        cDato.noRegistro = intNoReg;
                        cDato.campo1 = values[0].Trim().Replace("�", "Ñ").ToUpper();
                        cDato.campo2 = values[1].Trim().Replace("�", "Ñ").ToUpper();
                        cDato.campo3 = values[2].Trim().Replace("�", "Ñ").ToUpper();
                        cDato.campo4 = values[3].Trim().Replace("�", "Ñ").ToUpper();
                        cDato.campo5 = Convert.ToDecimal(values[4].Trim());
                        cDato.campo6 = Convert.ToDecimal(values[5].Trim());
                        cDato.campo7 = Convert.ToDecimal(values[6].Trim());
                        cDato.campo8 = Convert.ToDecimal(values[7].Trim());
                        cDato.campo9 = Convert.ToDecimal(values[8].Trim());
                        cDato.campo10 = Convert.ToDecimal(values[9].Trim());
                        cDato.campo11 = Convert.ToDecimal(values[10].Trim());
                        cDato.campo12 = Convert.ToDecimal(values[11].Trim());
                        cDato.campo13 = Convert.ToDecimal(values[12].Trim());
                        cDato.campo14 = Convert.ToDecimal(values[13].Trim());
                        cDato.campo15 = Convert.ToDecimal(values[14].Trim());
                        cDato.campo16 = Convert.ToDecimal(values[15].Trim());
                        cDato.campo17 = Convert.ToDecimal(values[16].Trim());
                        cDato.campo18 = Convert.ToDecimal(values[17].Trim());
                        cDato.campo19 = Convert.ToDecimal(values[18].Trim());
                        cDato.campo20 = Convert.ToDecimal(values[19].Trim());
                        cDato.campo21 = Convert.ToDecimal(values[20].Trim());

                        lstRegistros.Add(cDato);
                        lngNoRegCorrect++;
                    }
                    else
                    {
                        var cRegError = new CRegistrosError();
                        cRegError.LngId = intNoReg;
                        cRegError.StrIdVenta = "";
                        cRegError.LngFechaEmision = "";
                        cRegError.LngNit = "";
                        cRegError.LngNroAutorizacion = "";
                        cRegError.LngNroFactura = "";
                        cRegError.LngNroReferencia = "";
                        cRegError.StrErrores = strMensaje.Trim();
                        lstRegistroE.Add(cRegError);
                        lngNoRegInCorrect++;
                    }
                }
                catch (Exception ex)
                {
                    var cRegError = new CRegistrosError();
                    cRegError.LngId = intNoReg;
                    cRegError.StrIdVenta = lngNoRegCorrect.ToString(CultureInfo.InvariantCulture);
                    cRegError.LngFechaEmision = "";
                    cRegError.LngNit = "";
                    cRegError.LngNroAutorizacion = "";
                    cRegError.LngNroFactura = "";
                    cRegError.LngNroReferencia = "";
                    cRegError.StrErrores = "ERROR:" + ex.Message;
                    lstRegistroE.Add(cRegError);
                    lngNoRegInCorrect++;
                }
                intNoReg++;
            }

            dgvFormH.DataSource = lstRegistros;
            dgvFormH.DataBind();
            Session[CVariablesSesion.GrillaTransaccion] = lstRegistros;

            dgvTransaccionError.DataSource = lstRegistroE;
            dgvTransaccionError.DataBind();
            Session[CVariablesSesion.GrillaRegObservado] = lstRegistroE;
        }



        /// <summary>
        /// METODO. MCodificaParametrica
        /// </summary>
        /// <param name="intParametrica"></param>
        /// <param name="strDescripcion"></param>
        /// <returns></returns>
        private int MCodificaParametrica(int intParametrica, string strDescripcion)
        {
            int lngResultado = -99999;

            var objParametricas = Session[CVariablesSesion.ListaParametrica] as O_PARAMETRICAS_GRAL;
            var objParamEspecifica = Session[CVariablesSesion.ListaParamGral] as O_PARAMETRICAS_ACT;
            var objCatalogo = Session[CVariablesSesion.ListaCatalogo] as O_OBTENER_CATALOGOS_CTY;
            var lstInfraestructura = Session[CVariablesSesion.ListaInfraestructura] as List<O_INFRAESTRUCTURA_VOL_CTY>;
            var lstEntidad = Session[CVariablesSesion.ListaEntidad] as List<O_LISTA_AUTORIZACION_COND_CTY>;
            var objParamInf = Session[CVariablesSesion.ListaParamInfra] as O_PARAMETROS_INFRA;

            switch (intParametrica)
            {
                case (int)EnumTipoParametrica.Producto:
                    if (objParamEspecifica.lstProducto != null && objParamEspecifica.lstProducto.Any())
                    {
                        List<O_LISTA_AUTORIZACION_COND_CTY> objHidro = objParamEspecifica.lstProducto;
                        objHidro = objHidro.Where(x => x.CODIGO_NOMBRE == strDescripcion).ToList();
                        if (objHidro.Any())
                        {
                            lngResultado = Convert.ToInt32(objHidro.FirstOrDefault().ID_IDENTIFICADOR);
                        }
                        else
                        {
                            //todo No implementado.
                        }
                    }
                    break;
                case (int)EnumTipoParametrica.Pais:
                    if (objParametricas.lstPais != null && objParametricas.lstPais.Any())
                    {
                        List<O_UBICACION_CTY> objPais = objParametricas.lstPais;
                        objPais = objPais.Where(x => x.DESCRIPCION == strDescripcion).ToList();
                        if (objPais.Any())
                        {
                            lngResultado = Convert.ToInt32(objPais.FirstOrDefault().ID_UBICACION_GEOGRAFICA);
                        }
                        else
                        {
                            //todo No implementado.
                        }
                    }
                    break;
                case (int)EnumTipoParametrica.UnidadMedida:
                    if (objParamEspecifica.lstProducto != null && objParamEspecifica.lstProducto.Any())
                    {
                        var objHidro = objParamEspecifica.lstProducto;
                        objHidro = objHidro.Where(x => x.CODIGO_NOMBRE == strDescripcion).ToList();
                        if (objHidro.Any())
                        {
                            lngResultado = Convert.ToInt32(objHidro.FirstOrDefault().ID_SUB_IDENTIFICADOR);
                        }
                        else
                        {
                            //todo No implementado.
                        }
                    }
                    break;
                case (int)EnumTipoParametrica.TipoContenedor:
                    var lstTipoContenedor = objParamInf.TIPOS_CONTENEDORES;
                    if (lstTipoContenedor != null && lstTipoContenedor.Any())
                    {
                        lstTipoContenedor = lstTipoContenedor.Where(x => x.CODIGO == strDescripcion).ToList();
                        if (lstTipoContenedor.Any())
                        {
                            lngResultado = Convert.ToInt32(lstTipoContenedor.FirstOrDefault().ID_TIPO_CONTENEDOR);
                        }
                        else
                        {
                            //todo No implementado.
                        }
                    }
                    break;
                case (int)EnumTipoParametrica.TipoInfraestructura:
                    var lstTipoInfra = objParamInf.TIPO_INFRAESTRUCTURA;
                    if (lstTipoInfra != null && lstTipoInfra.Any())
                    {
                        lstTipoInfra = lstTipoInfra.Where(x => x.CODIGO == strDescripcion).ToList();
                        if (lstTipoInfra.Any())
                        {
                            lngResultado = Convert.ToInt32(lstTipoInfra.FirstOrDefault().ID_PARAMETRO);
                        }
                        else
                        {
                            //todo No implementado.
                        }
                    }
                    break;
                case (int)EnumTipoParametrica.Infraestructura:
                    var arreglo = strDescripcion.Split('|');
                    if (lstInfraestructura != null && lstInfraestructura.Any())
                    {
                        lstInfraestructura = lstInfraestructura.Where(x => x.TIPO == arreglo[0] && x.CODIGO_OPERADOR== arreglo[1]).ToList();
                        if (lstInfraestructura.Any())
                        {
                            lngResultado = Convert.ToInt32(lstInfraestructura.FirstOrDefault().ID_INFRAESTRUCTURA);
                        }
                        else
                        {
                            //todo No implementado.
                        }
                    }
                    break;
                
                default:
                    break;
            }
            return lngResultado;
        }

        /// <summary>
        /// METODO. MEnviaObjeto
        /// </summary>
        /// <param name="lstProtoMovimientos"></param>
        /// <param name="booTipo"></param>
        /// <param name="lngNoMovimientos"></param>
        /// <param name="decFechaInicio"></param>
        /// <param name="decFechaFin"></param>
        /// <param name="decTipoOperacion"></param>
        private void MEnviaObjeto(List<ObjProtoMovimientoTransaccion> lstProtoMovimientos, bool booTipo, long lngNoMovimientos, decimal decFechaInicio, decimal decFechaFin, decimal decTipoOperacion)
        {
            //var objRegistroo = new ERegistraProtoMovimientoTransaccion();
            //objRegistroo.strCredencial = CParametrosHydro.strCredencialFuncionario;
            //objRegistroo.decIdTipoActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
            //objRegistroo.decIdConsumidor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString());
            //objRegistroo.decIdTipoOperacion = decTipoOperacion;
            //objRegistroo.decFechaInicio = decFechaInicio;
            //objRegistroo.decFechaFin = decFechaFin;
            //objRegistroo.listaMovimientoTransaccion = lstProtoMovimientos;

            ////var ojo = JsonSerializer.SerializeToString(objRegistroo);
            //var resultado = _client.Post<EResultado>(CAppSettings.svc_registra_movimiento_transaccion_protobuf, objRegistroo);
            //if (resultado.intCodigo == 1)
            //{
            //    if (booTipo)
            //    {
            //        var lstDespachos = _client.Post<EResultado>(CAppSettings.svc_activa_json_transaccion,
            //        new
            //        {
            //            strCredencial = CParametrosHydro.strCredencialFuncionario,
            //            decFiltro = 1,
            //            decIdConsumidor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString()),
            //            decIdTipoActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()),
            //            decIdTipoOperacion = decTipoOperacion,
            //            decFechaInicio,
            //            decFechaFin
            //        });
            //        if (lstDespachos.intCodigo == 1)
            //        {
            //            var datFechaReg1 = DateTime.Now;
            //            var repCe = new repConstanciaEnvio();
                        
            //            repCe.xtcFecha.Text = datFechaReg1.ToShortDateString();
            //            repCe.xtcHora.Text = datFechaReg1.ToLongTimeString();
            //            repCe.xtcUsuario.Text = CParametrosHydro.StrUsuarioLogin;
            //            repCe.xtcIntervalo.Text = "";

            //            repCe.xtcActividad.Text = tlsOperadores.FocusedNode.GetValue("StrActividad").ToString();
                        
            //            repCe.xtcPeriodo.Text = cmbPeriodo.Text;
                        
            //            repCe.xtcNit.Text = tlsOperadores.FocusedNode.GetValue("LngNit").ToString();
            //            repCe.xtcRazonSocial.Text = tlsOperadores.FocusedNode.GetValue("StrDescPadre").ToString();
            //            repCe.xtcNomComercial.Text = tlsOperadores.FocusedNode.GetValue("StrDescripcion").ToString();
            //            repCe.xtcNomPropietario.Text = (tlsOperadores.FocusedNode.GetValue("StrPropietario") == null) ? "" : tlsOperadores.FocusedNode.GetValue("StrPropietario").ToString();
            //            repCe.xtcNomRepresentante.Text = tlsOperadores.FocusedNode.GetValue("StrRepLegal").ToString();
            //            repCe.xtcCiudad.Text = tlsOperadores.FocusedNode.GetValue("StrDepartamento").ToString();
            //            repCe.xtcDireccion.Text = tlsOperadores.FocusedNode.GetValue("StrDireccion").ToString();
            //            repCe.xtcTelefono.Text = tlsOperadores.FocusedNode.GetValue("StrTelefono").ToString();
            //            repCe.xtcNoRegistros.Text = lngNoMovimientos.ToString(CultureInfo.InvariantCulture);

            //            Session[CVariablesSesion.TipoReporte] = EnumTipoReporte.ConstanciaEnvio;
            //            Session[CVariablesSesion.ReportePrincipal] = repCe;
            //            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( 'WFReporte.aspx', '');if (confirm('¿Desea emtir la Declaración Jurada?')){window.location.href = 'WFDeclaracionJurada.aspx';}", true);

            //            MLimpiaGrilla();
            //        }
            //        else
            //        {
            //            MImprimirMensaje(lstDespachos.strMensaje, EnumTiposMensajeAlerta.Error);
            //        }
            //    }
            //    else
            //    {
            //        //todo No implementado.
            //    }
            //}
            //else
            //{
            //    MImprimirMensaje(resultado.strMensaje, EnumTiposMensajeAlerta.Error);
            //    //return;
            //}
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
        /// METODO. MEnviaTraza
        /// </summary>
        /// <param name="intTipo"></param>
        /// <param name="mensaje"></param>
        /// <param name="strAccion"></param>
        private void MEnviaTraza(int intTipo, string mensaje, string strAccion)
        {
            if (intTipo == 1)
            {
                CLogTraza.Informacion(strAccion + "." + mensaje, CParametrosHydro.decIdAplicacion, Convert.ToDecimal(Session[CVariablesSesion.IdUsuario].ToString()), HttpContext.Current.Request.UserHostAddress, CLogTraza.CapasNivel.Presentacion.ToString());
            }
            else
            {
                CLogTraza.Advertencia(strAccion + "." + mensaje, strAccion, CParametrosHydro.decIdAplicacion, Convert.ToDecimal(Session[CVariablesSesion.IdUsuario].ToString()), HttpContext.Current.Request.UserHostAddress, "ELSE NO IMPLEMENTADO", CLogTraza.CapasNivel.Presentacion.ToString());
            }
        }

        #endregion


        public class CFormularioA
        {
            public decimal noRegistro { get; set; }
            public string campo1 { get; set; }
            public string campo2 { get; set; }
            public string campo3 { get; set; }
            public string campo4 { get; set; }
            public decimal campo5 { get; set; }
            public decimal campo6 { get; set; }
            public decimal campo7 { get; set; }
            public decimal campo8 { get; set; }
            public string campo9 { get; set; }
        }

        public class CFormularioB
        {
            public decimal noRegistro { get; set; }
            public string campo1 { get; set; }
            public string campo2 { get; set; }
            public string campo3 { get; set; }
            public string campo4 { get; set; }
            public decimal campo5 { get; set; }
            public decimal campo6 { get; set; }
            public decimal campo7 { get; set; }
            public decimal campo8 { get; set; }
            public decimal campo9 { get; set; }
            public string campo10 { get; set; }
            public decimal campo11 { get; set; }
            public decimal campo12 { get; set; }
            public decimal campo13 { get; set; }
            public decimal campo14 { get; set; }
            public decimal campo15 { get; set; }
            public decimal campo16 { get; set; }
            public string campo17 { get; set; }
        }

        public class CFormularioC
        {
            public decimal noRegistro { get; set; }
            public string campo1 { get; set; }
            public string campo2 { get; set; }
            public string campo3 { get; set; }
            public string campo4 { get; set; }
            public decimal campo5 { get; set; }
            public decimal campo6 { get; set; }
            public decimal campo7 { get; set; }
            public string campo8 { get; set; }
        }

        public class CFormularioD
        {
            public decimal noRegistro { get; set; }
            public string campo1 { get; set; }
            public string campo2 { get; set; }
            public string campo3 { get; set; }
            public string campo4 { get; set; }
            public string campo5 { get; set; }
            public string campo6 { get; set; }
            public string campo7 { get; set; }
            public string campo8 { get; set; }
            public string campo9 { get; set; }
            public string campo10 { get; set; }
            public string campo11 { get; set; }
            public string campo12 { get; set; }
            public string campo13 { get; set; }
            public decimal campo14 { get; set; }
            public decimal campo15 { get; set; }
            public decimal campo16 { get; set; }
            public decimal campo17 { get; set; }
            public decimal campo18 { get; set; }
            public decimal campo19 { get; set; }
            public decimal campo20 { get; set; }
            public decimal campo21 { get; set; }
            public string campo22 { get; set; }
            public string campo23 { get; set; }
            public string campo24 { get; set; }
            public string campo25 { get; set; }
        }

        public class CFormularioE
        {
            public decimal noRegistro { get; set; }
            public string campo1 { get; set; }
            public string campo2 { get; set; }
            public string campo3 { get; set; }
            public string campo4 { get; set; }
            public string campo5 { get; set; }
            public string campo6 { get; set; }
            public string campo7 { get; set; }
            public string campo8 { get; set; }
            public string campo9 { get; set; }
            public string campo10 { get; set; }
            public string campo11 { get; set; }
            public decimal campo12 { get; set; }
            public decimal campo13 { get; set; }
            public decimal campo14 { get; set; }
            public string campo15 { get; set; }
            public string campo16 { get; set; }
            public decimal campo17 { get; set; }
            public decimal campo18 { get; set; }
            public decimal campo19 { get; set; }
            public decimal campo20 { get; set; }
            public decimal campo21 { get; set; }
            public decimal campo22 { get; set; }
        }

        public class CFormularioF
        {
            public decimal noRegistro { get; set; }
            public string campo1 { get; set; }
            public string campo2 { get; set; }
            public string campo3 { get; set; }
            public string campo4 { get; set; }
            public decimal campo5 { get; set; }
            public decimal campo6 { get; set; }
            public decimal campo7 { get; set; }
            public decimal campo8 { get; set; }
            public decimal campo9 { get; set; }
            public decimal campo10 { get; set; }
            public decimal campo11 { get; set; }
            public decimal campo12 { get; set; }
            public decimal campo13 { get; set; }
            public decimal campo14 { get; set; }
            public decimal campo15 { get; set; }
            public decimal campo16 { get; set; }
            public decimal campo17 { get; set; }
            public decimal campo18 { get; set; }
            public decimal campo19 { get; set; }
            public decimal campo20 { get; set; }
            public decimal campo21 { get; set; }
        }

        public class CFormularioG
        {
            public decimal noRegistro { get; set; }
            public string campo1 { get; set; }
            public string campo2 { get; set; }
            public string campo3 { get; set; }
            public string campo4 { get; set; }
            public decimal campo5 { get; set; }
            public decimal campo6 { get; set; }
            public decimal campo7 { get; set; }
            public decimal campo8 { get; set; }
            public decimal campo9 { get; set; }
            public decimal campo10 { get; set; }
            public decimal campo11 { get; set; }
            public decimal campo12 { get; set; }
            public decimal campo13 { get; set; }
            public decimal campo14 { get; set; }
            public decimal campo15 { get; set; }
            public decimal campo16 { get; set; }
            public decimal campo17 { get; set; }
            public decimal campo18 { get; set; }
            public decimal campo19 { get; set; }
            public decimal campo20 { get; set; }
        }

        public class CFormularioH
        {
            public decimal noRegistro { get; set; }
            public string campo1 { get; set; }
            public string campo2 { get; set; }
            public string campo3 { get; set; }
            public string campo4 { get; set; }
            public decimal campo5 { get; set; }
            public decimal campo6 { get; set; }
            public decimal campo7 { get; set; }
            public decimal campo8 { get; set; }
            public decimal campo9 { get; set; }
            public decimal campo10 { get; set; }
            public decimal campo11 { get; set; }
            public decimal campo12 { get; set; }
            public decimal campo13 { get; set; }
            public decimal campo14 { get; set; }
            public decimal campo15 { get; set; }
            public decimal campo16 { get; set; }
            public decimal campo17 { get; set; }
            public decimal campo18 { get; set; }
            public decimal campo19 { get; set; }
            public decimal campo20 { get; set; }
            public decimal campo21 { get; set; }
        }

        
    }
}