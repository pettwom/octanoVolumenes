
using AnhHydroOctanoPresentacion.Lib;

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
    using Clases.Util;
    using Enumeradores;
    using Parametros;
    using DevExpress.Web;
    using Librerias.Anh.Us;
    using UserControl = System.Web.UI.UserControl;
    using ServiceStack.Text;
    public partial class CtrRecepciones : UserControl
    {
        #region Variables de entorno

        private readonly CPersistenciaConsulta _consulta = new CPersistenciaConsulta();
        readonly CUtil _cUtil = new CUtil();
        readonly CTrazaInf _cTrazaInf = new CTrazaInf();
        CConsultasParametricas cConsultasParametricas = new CConsultasParametricas();

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
                        Session.Remove(CVariablesSesion.EstructuraArbol);
                        Session.Remove(CVariablesSesion.GrillaCabecera);

                        MCargaArbol();
                        MConfiguracionInicial();

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

                    if (dgvDatos.DataSource == null && Session[CVariablesSesion.GrillaCabecera] != null)
                    {
                        dgvDatos.DataSource = Session[CVariablesSesion.GrillaCabecera];
                        dgvDatos.DataBind();
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

        protected void pnlDatos_Callback(object sender, CallbackEventArgsBase e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    Session[CVariablesSesion.EntidadSeleccionada] = tlsOperadores.FocusedNode.GetValue("StrUbicacion");

                    lblRazonSocial.Text = tlsOperadores.FocusedNode.GetValue("StrDescPadre").ToString();
                    lblNit.Text = tlsOperadores.FocusedNode.GetValue("LngNit").ToString();
                    lblDireccion.Text = (tlsOperadores.FocusedNode.GetValue("StrDireccion") == null) ? "" : tlsOperadores.FocusedNode.GetValue("StrDireccion").ToString();
                    lblDepto.Text = tlsOperadores.FocusedNode.GetValue("StrDepartamento").ToString();

                    MLimpiaGrilla();
                    if ((int) tlsOperadores.FocusedNode.GetValue("IntAccion") > 0)
                    {
                        MCargaGrilla();
                    }

                    #endregion
                }
                catch (Exception exp)
                {
                    #region RegistraError

                    MLimpiaGrilla();
                    MImprimirMensaje(exp.Message, EnumTiposMensajeAlerta.Error, false);
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

        protected void dgvDatos_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            try
            {
                #region Proceso a Ejecutar

                if (e.VisibleIndex >= 0)
                {
                    if (dgvDatos.GetRowValues(e.VisibleIndex, "STR_ESTADO_MOVIMIENTO").ToString().Equals("A"))
                        e.Row.BackColor = Color.FromArgb(215, 135, 135);
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

                MImprimirMensaje(exp.Message, EnumTiposMensajeAlerta.Error, false);
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
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    if (dgvDatos.VisibleRowCount > 0)
                    {
                        if ((int)cmbTipoExport.Value == 1)
                        {
                            dgeExportar.GridViewID = "dgvDatos";
                            dgeExportar.WriteXlsxToResponse("RepRecepciones");
                        }
                        else
                        {


                            var xreReporte = new repRecepciones();
                            xreReporte.xtcFecha.Text = DateTime.Now.ToShortDateString();
                            xreReporte.xtcHora.Text = DateTime.Now.ToLongTimeString();
                            xreReporte.xtcUsuario.Text = CParametrosHydro.StrUsuarioLogin;
                            xreReporte.xtcIntervalo.Text = "Del : " + detFechaIni.Date.ToShortDateString() + "  Al : " + detFechaFin.Date.ToShortDateString();

                            xreReporte.xtcActividad.Text = tlsOperadores.FocusedNode.GetValue("StrActividad").ToString();
                            xreReporte.xtcProyecto.Text = tlsOperadores.FocusedNode.GetValue("StrDescPadre").ToString();
                            xreReporte.xtcNit.Text = tlsOperadores.FocusedNode.GetValue("LngNit").ToString();
                            xreReporte.xtcDireccion.Text = (tlsOperadores.FocusedNode.GetValue("StrDireccion") == null) ? "" : tlsOperadores.FocusedNode.GetValue("StrDireccion").ToString();
                            xreReporte.xtcDepto.Text = tlsOperadores.FocusedNode.GetValue("StrDepartamento").ToString();

                            var lstSelect = new List<O_LISTA_RECEPCIONES_CTY>();
                            for (int i = 0; i < dgvDatos.VisibleRowCount; i++)
                            {
                                var objRep = (O_LISTA_RECEPCIONES_CTY)dgvDatos.GetRow(i);
                                lstSelect.Add(objRep);
                            }
                            xreReporte.DataSource = lstSelect;

                            Session[CVariablesSesion.ReportePrincipal] = xreReporte;
                            Session[CVariablesSesion.TipoReporte] = EnumTipoReporte.RepRecepciones;
                            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( 'WFReporte.aspx', '');", true);
                        }
                    }
                    else
                    {
                        MImprimirMensaje("No existen registros.", EnumTiposMensajeAlerta.Alerta, true);
                    }

                    lblRazonSocial.Text = tlsOperadores.FocusedNode.GetValue("StrDescPadre").ToString();
                    lblNit.Text = tlsOperadores.FocusedNode.GetValue("LngNit").ToString();
                    lblDireccion.Text = (tlsOperadores.FocusedNode.GetValue("StrDireccion") == null) ? "" : tlsOperadores.FocusedNode.GetValue("StrDireccion").ToString();
                    lblDepto.Text = tlsOperadores.FocusedNode.GetValue("StrDepartamento").ToString();

                    #endregion
                }
                catch (Exception exp)
                {
                    #region RegistraError

                    MImprimirMensaje(exp.Message, EnumTiposMensajeAlerta.Error, false);
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
        /// METODO. MCargaArbol
        /// </summary>
        private void MCargaArbol()
        {
            MLimpiaGrilla();
            var objArbol = Session[CVariablesSesion.ObjetoArbol] as List<O_OBTIENE_ENTIDADES_CTY>;
            if (objArbol == null)
            {
                decimal idUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
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
                }
                else
                {
                    MImprimirMensaje(objResultado.strMensaje, EnumTiposMensajeAlerta.Error, true);
                    return;
                }
            }

            Session[CVariablesSesion.EstructuraArbol] = _cUtil.MObtenerArbolOrdenado(Properties.Settings.Default.intOrdenArbol, objArbol);
        }

        /// <summary>
        /// METODO. MConfiguracionInicial
        /// </summary>
        private void MConfiguracionInicial()
        {
            detFechaIni.Date = DateTime.Parse("01/" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "/" + DateTime.Now.Year);
            detFechaFin.Date = DateTime.Now.Date;
        }

        /// <summary>
        /// METODO. MCargaGrilla
        /// </summary>
        private void MCargaGrilla()
        {
            var objConsulta = new EObjetoEnvio4ParametrosProto
            {
                strCredencial = "",
                decFiltro = 1, //Filtro 1 
                strParametro1 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(), //Id proyecto/entidad
                strParametro2 = tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString(),
                strParametro3 = detFechaIni.Date.Year + detFechaIni.Date.Month.ToString().PadLeft(2, '0') + detFechaIni.Date.Day.ToString().PadLeft(2, '0') + "000000",
                strParametro4 = detFechaFin.Date.Year + detFechaFin.Date.Month.ToString().PadLeft(2, '0') + detFechaFin.Date.Day.ToString().PadLeft(2, '0') + "235959"
            };
            var lstRecepPropio = _consulta.ListaRecepciones(objConsulta);
            if (lstRecepPropio.intCodigo == 1)
            {
                var lstRecepDet = (List<O_LISTA_RECEPCIONES_CTY>)lstRecepPropio.oResultado;

                dgvDatos.DataSource = lstRecepDet;
                dgvDatos.DataBind();
                Session[CVariablesSesion.GrillaCabecera] = lstRecepDet;
                
                #region Traza consulta de informacion
                string strAccion = JsonSerializer.SerializeToString(objConsulta);
                string strSalida = lstRecepDet.Count().ToString();
                string mensaje = GetType().Name + "." + MethodBase.GetCurrentMethod().Name;
                _cTrazaInf.MRegistraTrazaInf(strAccion, mensaje, CParametrosHydro.decIdAplicacion, Convert.ToDecimal(Session[CVariablesSesion.IdUsuario].ToString()), HttpContext.Current.Request.UserHostAddress, strSalida);
                #endregion 
            }
            else
            {
                MImprimirMensaje(lstRecepPropio.strMensaje, EnumTiposMensajeAlerta.Error, false);
            }
        }

        /// <summary>
        /// METODO. MLimpiaGrilla
        /// </summary>
        private void MLimpiaGrilla()
        {
            Session.Remove(CVariablesSesion.GrillaCabecera);
            dgvDatos.DataSource = null;
            dgvDatos.DataBind();

            pnlMensaje.Visible = false;
        }

        /// <summary>
        /// METODO. MImprimirMensaje
        /// </summary>
        /// <param name="strMensaje"></param>
        /// <param name="tipoMensaje"></param>
        /// <param name="booActivaJs"></param>
        private void MImprimirMensaje(string strMensaje, EnumTiposMensajeAlerta tipoMensaje, bool booActivaJs)
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
            if (booActivaJs)
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError", "alert('" + strMensaje + "');", true);
        }

        #endregion

    }

}