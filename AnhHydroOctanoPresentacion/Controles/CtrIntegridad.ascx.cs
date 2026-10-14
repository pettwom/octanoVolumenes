
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
    using Enumeradores;
    using Parametros;
    using DevExpress.Web;
    using Librerias.Anh.Us;
    using ServiceStack.ServiceClient.Web;
    using Page = System.Web.UI.Page;

    public partial class CtrIntegridad : UserControl
    {
        #region Variables de entorno

        readonly JsonServiceClient _client = new JsonServiceClient(CAppSettings.servicio_octVolumen);
        private readonly CPersistenciaConsulta _consulta = new CPersistenciaConsulta();

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
                        Session.Remove(CVariablesSesion.GrillaAcumulado);
                        Session.Remove(CVariablesSesion.GrillaHistorial);
                    }
                    else
                    {
                        //todo No implementado.
                    }

                    if (dgvCabecera.DataSource == null && Session[CVariablesSesion.GrillaCabecera] != null)
                    {
                        dgvCabecera.DataSource = Session[CVariablesSesion.GrillaCabecera];
                        dgvCabecera.DataBind();
                    }
                    else
                    {
                        //todo no implementado
                    }

                    if (dgvPrecios.DataSource == null && Session[CVariablesSesion.GrillaAcumulado] != null)
                    {
                        dgvPrecios.DataSource = Session[CVariablesSesion.GrillaAcumulado];
                        dgvPrecios.DataBind();
                    }
                    else
                    {
                        //todo no implementado
                    }

                    if (dgvHistorico.DataSource == null && Session[CVariablesSesion.GrillaHistorial] != null)
                    {
                        dgvHistorico.DataSource = Session[CVariablesSesion.GrillaHistorial];
                        dgvHistorico.DataBind();
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

                    if ((int) rbtTabla.Value == 1)
                    {
                        dgvCabecera.Visible = true;

                        dgvCabecera.Columns[19].Visible = false;
                        if ((int)cmbSubTipo.Value == 5)
                            dgvCabecera.Columns[19].Visible = true;

                        if ((int)cmbSubTipo.Value < 3)
                        {
                            for (int i = 0; i < 19; i++)
                            {
                                dgvCabecera.Columns[i].VisibleIndex = i;
                            }
                        }
                        else
                        {
                            var intAux = 9;
                            for (int i = 0; i < 19; i++)
                            {
                                if (i + intAux > 18)
                                    intAux = -10;
                                dgvCabecera.Columns[i + intAux].VisibleIndex = i;
                            }
                        }

                        var objResultado = _consulta.ListaComparativoEnt(
                        new EObjetoEnvio1ParametrosProto
                        {
                            strCredencial = CParametrosHydro.strCredencialFuncionario,
                            decFiltro = (int)cmbSubTipo.Value,
                            strParametro1 = ""
                        });
                        if (objResultado.intCodigo == 1)
                        {
                            var lstEntidad = (List<O_LISTA_COMPARATIVO_ENT_CTY>)objResultado.oResultado;
                            dgvCabecera.DataSource = lstEntidad;
                            dgvCabecera.DataBind();
                            Session[CVariablesSesion.GrillaCabecera] = lstEntidad;
                        }
                        else
                        {
                            MImprimirMensaje(objResultado.strMensaje, EnumTiposMensajeAlerta.Error);
                        }
                    }
                    else
                    {
                        pnlPrecios.Visible = true;

                        var objResultado = _consulta.ListaPrecioHidroc(
                        new EObjetoEnvio2ParametrosProto
                        {
                            strCredencial = CParametrosHydro.strCredencialFuncionario,
                            decFiltro = 1,
                            strParametro1 = "",
                            strParametro2 = ""
                        });
                        if (objResultado.intCodigo == 1)
                        {
                            var lstPrecios = (List<O_LISTA_PRECIOS_HID_CAB_CTY>)objResultado.oResultado;
                            dgvPrecios.DataSource = lstPrecios;
                            dgvPrecios.DataBind();
                            Session[CVariablesSesion.GrillaAcumulado] = lstPrecios;
                        }
                        else
                        {
                            MImprimirMensaje(objResultado.strMensaje, EnumTiposMensajeAlerta.Error);
                        }
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
                //todo no implementado
            }
        }

        protected void pnlHistorico_Callback(object sender, CallbackEventArgsBase e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    dgvHistorico.DataSource = null;
                    dgvHistorico.DataBind();

                    var panel = sender as ASPxCallbackPanel;
                    panel.JSProperties["cp_title"] = "";
                    panel.JSProperties["cp_subtitle"] = "";
                    panel.JSProperties["cp_xAxis"] = null;
                    panel.JSProperties["cp_series"] = null;

                    int index = dgvPrecios.FocusedRowIndex;
                    if (index != -1)
                    {
                        var objResultado = _consulta.ListaPrecioHidroc(
                        new EObjetoEnvio2ParametrosProto
                        {
                            strCredencial = CParametrosHydro.strCredencialFuncionario,
                            decFiltro = 2,
                            strParametro1 = dgvPrecios.GetRowValues(index, "ID_HIDROCARBURO").ToString(),
                            strParametro2 = dgvPrecios.GetRowValues(index, "ID_CATEGORIA_VEHICULO").ToString()
                        });
                        if (objResultado.intCodigo == 1)
                        {
                            var lstPrecioHist = (List<O_LISTA_PRECIOS_HID_DET_CTY>)objResultado.oResultado;
                            dgvHistorico.DataSource = lstPrecioHist;
                            dgvHistorico.DataBind();
                            Session[CVariablesSesion.GrillaHistorial] = lstPrecioHist;

                            var lstGraf = lstPrecioHist.Take(20).ToList().OrderBy(x => x.ID_TABLA);
                            if (!lstGraf.Any())
                                return;

                            var series = new List<O_SERIES_DEC>();
                            var serie = new O_SERIES_DEC();
                            serie.name = dgvPrecios.GetRowValues(index, "CATEGORIA").ToString();
                            var i = 0;
                            var data = new decimal[lstGraf.Count()];
                            var names = new string[lstGraf.Count()];
                            foreach (var reg in lstGraf)
                            {
                                names[i] = reg.FECHA_DESDE.Substring(0, 10);
                                data[i] = reg.PRECIO_BS;
                                i++;
                            }
                            serie.data = data;
                            series.Add(serie);

                            panel = sender as ASPxCallbackPanel;
                            panel.JSProperties["cp_title"] = "VARIACION DE PRECIO";
                            panel.JSProperties["cp_subtitle"] = "Hidrocarburo: " + dgvPrecios.GetRowValues(index, "HIDRO");
                            panel.JSProperties["cp_xAxis"] = names;
                            panel.JSProperties["cp_series"] = series;
                        }
                        else
                        {
                            MImprimirMensaje(objResultado.strMensaje, EnumTiposMensajeAlerta.Error);
                        }
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
                //todo no implementado
            }
        }

        protected void dgvCabecera_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            try
            {
                #region Proceso a Ejecutar


                if (e.VisibleIndex >= 0)
                {
                    if (dgvCabecera.GetRowValues(e.VisibleIndex, "HYD_ID_ENTIDAD").ToString().Equals("-99999") || dgvCabecera.GetRowValues(e.VisibleIndex, "BSA_ID_ENT_HYD").ToString().Equals("-99999"))
                    {
                        e.Row.BackColor = Color.FromArgb(215, 135, 135);
                    }
                    else
                    {
                        /*if (e.VisibleIndex % 2 == 1)
                        {
                            e.Row.BackColor = Color.FromArgb(204, 209, 182);
                        }
                        else
                        {
                            //todo No implementado.
                        }*/
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

        /*protected void dgvPrecios_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
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

        protected void dgvHistorico_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            try
            {
                #region Proceso a Ejecutar


                if (e.VisibleIndex >= 0)
                {
                    if (e.VisibleIndex % 2 == 1)
                    {
                        e.Row.BackColor = Color.FromArgb(253, 237, 212);
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

        protected void btnEmitir_Click(object sender, EventArgs e)
        {
            try
            {
                #region Proceso a Ejecutar

                if ((int)rbtTabla.Value == 1)
                {
                    dgeExportar.GridViewID = "dgvCabecera";
                }
                else
                {
                    dgeExportar.GridViewID = "dgvPrecios";
                }
                dgeExportar.WriteXlsxToResponse();

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

        protected void btnEmitirDet_Click(object sender, EventArgs e)
        {
            try
            {
                #region Proceso a Ejecutar

                dgeExportar.GridViewID = "dgvHistorico";
                dgeExportar.WriteXlsxToResponse();

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
        /// METODO. MLimpiaGrilla
        /// </summary>
        private void MLimpiaGrilla()
        {
            dgvCabecera.Visible = false;
            pnlPrecios.Visible = false;

            Session.Remove(CVariablesSesion.GrillaCabecera);
            dgvCabecera.DataSource = null;
            dgvCabecera.DataBind();

            Session.Remove(CVariablesSesion.GrillaAcumulado);
            dgvPrecios.DataSource = null;
            dgvPrecios.DataBind();

            MImprimirMensaje("",EnumTiposMensajeAlerta.Limpiar);
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
            if (tipoMensaje != EnumTiposMensajeAlerta.Correcto)
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError", "alert('" + strMensaje + "');", true);
            lblMensajeError.Text = "   " + strMensaje + "   ";
        }

        #endregion

    }
}