
using AnhHydroOctanoPresentacion.Lib;

namespace AnhHydroOctanoPresentacion.Controles
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Reflection;
    using System.Web;
    using System.Web.UI;
    using AnhHydroOctanoPersistencia.Aplicacion.Consulta;
    using AnhHydroOctanoPersistencia.Entidades;
    using Clases.Util;
    using Entidades;
    using Enumeradores;
    using Parametros;
    using DevExpress.Web;
    using Librerias.Anh.Us;
    using ServiceStack.ServiceClient.Web;
    using UserControl = System.Web.UI.UserControl;
    using JsonSerializer = ServiceStack.Text.JsonSerializer;

    public partial class CtrVisorDgac : UserControl
    {
        #region Variables de entorno

        private readonly CPersistenciaConsulta _consulta = new CPersistenciaConsulta();
        readonly CUtil _cUtil = new CUtil();
        readonly JsonServiceClient _extInteg = new JsonServiceClient(CAppSettings.servicio_octIntegracion);
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
                    MLimpiaGrilla();
                    MCargaGrilla();
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

                    dgeExportar.GridViewID = "dgvDatos";
                    dgeExportar.WriteXlsxToResponse("dt_Dgac");

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
            
        }

        /// <summary>
        /// METODO. MCargaGrilla
        /// </summary>
        private void MCargaGrilla()
        {
            var objResultado = _extInteg.Post<EResultadoI>(CAppSettings.svc_GestionDgac,
            new
            {
                strCredencial = CParametrosHydro.StrCredencialExterno,
                decOrigen = rbtOrigen.Value,
                decServicio = rbtServicio.Value,
                decAccion = chkAccion.Value,
                decTipo = ((int)rbtTipo.Value == 0) ? 0 : ((int)rbtServicio.Value == 1) ? rbtEspAeronave.Value : rbtEspPiloto.Value,
                strDato = txtEspecifico.Text.ToUpper(),
                decIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario])
            });
            if (objResultado.decCodigo == 1)
            {
                object lstResultado;
                if ((int)rbtOrigen.Value == 1)
                {
                    if ((int) rbtServicio.Value == 1)
                    {
                        dgvDatos.KeyFieldName = "ID_DGAC_AERONAVE";
                        lstResultado = JsonSerializer.DeserializeFromString<List<O_LISTA_DGAC_AERONAVE_CTY>>((string) objResultado.oResultado);
                    }
                    else
                    {
                        dgvDatos.KeyFieldName = "ID_DGAC_PILOTO";
                        lstResultado = JsonSerializer.DeserializeFromString<List<O_LISTA_DGAC_PILOTO_CTY>>((string)objResultado.oResultado);
                    }
                }
                else
                {
                    if ((int)rbtServicio.Value == 1)
                    {
                        dgvDatos.KeyFieldName = "nroCorrelativo";
                        lstResultado = JsonSerializer.DeserializeFromString<List<O_DGAC_AVION>>((string)objResultado.oResultado);
                    }
                    else
                    {
                        dgvDatos.KeyFieldName = "nroLicencia";
                        lstResultado = JsonSerializer.DeserializeFromString<List<O_DGAC_PILOTO>>((string)objResultado.oResultado);
                    }
                }
                dgvDatos.DataSource = lstResultado;
                dgvDatos.DataBind();
                Session[CVariablesSesion.GrillaCabecera] = lstResultado;
            }
            else
            {
                MImprimirMensaje(objResultado.strMensaje, EnumTiposMensajeAlerta.Error, false);
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
            dgvDatos.Columns.Clear();

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