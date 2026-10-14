using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AnhHydroOctanoPresentacion.Controles
{
    using AnhHydroOctanoPresentacion.Parametros;
    using Librerias.Anh.Us;
    using System.Reflection;
    using AnhHydroOctanoPresentacion.Entidades;
    using ServiceStack.ServiceClient.Web;
    using AnhHydroOctanoPersistencia.Aplicacion.Consulta;
    using AnhHydroOctanoPersistencia.Aplicacion.Gestion;
    using AnhHydroOctanoPersistencia.Entidades;
    using System.Drawing;
    using Enumeradores;
    using DevExpress.Web;
    using Clases.Util;
    using ServiceStack.Text;
    public partial class CtrProgramacionProde : System.Web.UI.UserControl
    {
        #region variables de entorno        
        private readonly CPersistenciaConsulta _consulta = new CPersistenciaConsulta();
        private readonly CPersistenciaGestion _gestion = new CPersistenciaGestion();
        readonly JsonServiceClient _client = new JsonServiceClient(CAppSettings.servicio_octVolumen);
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
                        Session.Remove(CVariablesSesion.ProdeProgramado);
                        Session.Remove(CVariablesSesion.ListaParametrica);
                        Session.Remove(CVariablesSesion.PrimerDiaSemana);
                        Session.Remove(CVariablesSesion.GrillaTransaccion);
                        Session.Remove(CVariablesSesion.ProgramadoActividadPDE);


                        MCargaParametricas();

                        var objParametricas = (O_PARAMETRICAS_GRAL)Session[CVariablesSesion.ListaParametrica];
                        cmbProductoProde.DataSource = objParametricas.lstHidrocarburo;
                        cmbProductoProde.DataBind();
                        cmbProductoProde.SelectedItem = cmbProductoProde.Items.FindByValue(Convert.ToInt32("126"));

                        cmbPeriodoGestion.SelectedItem = cmbPeriodoGestion.Items.FindByValue(Convert.ToInt32(DateTime.Now.Year.ToString()));
                        cmbPeriodoMes.SelectedItem = cmbPeriodoMes.Items.FindByValue(Convert.ToInt32(DateTime.Now.Month.ToString()));

                        //CargarTechoProde();
                        //pnlDatos_Callback(null, null);

                    }
                    else
                    {
                        //todo No es carga inicial: no implementado
                    }
                    if (dgvTransacciones.DataSource == null && Session[CVariablesSesion.GrillaTransaccion] != null)
                    {
                        dgvTransacciones.DataSource = Session[CVariablesSesion.GrillaTransaccion];
                        dgvTransacciones.DataBind();
                        dgvTransacciones.ExpandAll();
                    }
                    else
                    {
                        //todo grilla vacia: no implementado
                    }
                    if (dgvProgramado.DataSource == null && Session[CVariablesSesion.ProgramadoActividadPDE] != null)
                    {
                        dgvProgramado.DataSource = Session[CVariablesSesion.ProgramadoActividadPDE];
                        dgvProgramado.DataBind();                   
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
        protected void btnExportar_Click(object sender, EventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    dgeExportar.GridViewID = "dgvProgramado";
                    dgeExportar.WriteXlsxToResponse();

                    #endregion
                }
                catch (Exception exp)
                {
                    #region RegistraError

                    //MImprimirMensaje(exp.Message, EnumTiposMensajeAlerta.Error);

                    #endregion
                }
            }
            else
            {
                //todo no implementado
            }
        }
        protected void btnExportarTrans_Click(object sender, EventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    dgeExportar.GridViewID = "dgvTransacciones";
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
        #region #region Metodos y funciones
        private bool MValidaSesion()
        {
            #region Proceso a Ejecutar

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

            #endregion
        }
        private void MCargaParametricas()
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    var objParametricas = new O_PARAMETRICAS_GRAL();

                    var objHidrocarburo = _client.Post<EResultadoProducto>(CAppSettings.svc_lista_producto,
                    new
                    {
                        strCredencial = CParametrosHydro.strCredencialFuncionario,
                        strCodigo = ""
                    });
                    if (objHidrocarburo.IntCodigo == 1)
                    {
                        objParametricas.lstHidrocarburo = objHidrocarburo.OResultado;
                    }
                    var lstObtCatalogo = _client.Post<EResultadoParametrica>(CAppSettings.svc_obtener_catalogo,
                    new
                    {
                        strLlave = ""
                    });

                    if (lstObtCatalogo.IntCodigo == 1)
                    {
                        objParametricas.lstUnidadMedida = lstObtCatalogo.OResultado.CATALOGO_UNIDADES_MEDIDA_GRAL;
                    }

                    Session[CVariablesSesion.ListaParametrica] = objParametricas;

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
        protected void pnlDatos_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar
                    string strMes = cmbPeriodoMes.Value.ToString().PadLeft(2, '0');
                    string strGestion = cmbPeriodoGestion.Value.ToString();
                    //string strUltimoDia = DateTime.DaysInMonth(Convert.ToInt32(cmbPeriodoGestion.Value), Convert.ToInt32(cmbPeriodoMes.Value)).ToString().PadLeft(2, '0');
                    //string strPrimerdiaSemana = Convert.ToDateTime("01/" + strMes + "/" + strGestion + "").DayOfWeek.ToString(); 
                    MLimpiaGrilla();
                    //ImprimirSemanas(strPrimerdiaSemana, strUltimoDia);

                    //CargarTechoProde();
                    var objConsulta = new EObjetoEnvio3ParametrosProto
                    {
                        strCredencial = CParametrosHydro.strCredencialFuncionario,
                        decFiltro = 1,
                        strParametro1 = cmbActividad.Value.ToString(),
                        strParametro2 = "0",
                        strParametro3 = strGestion + strMes
                    };
                    var objResultado = _consulta.ObtenerVolProgramadoPDE(objConsulta);
                    if (objResultado.intCodigo == 1)
                    {
                        var lstDato = (List<O_PRODUCTO_PDE_CTY>)objResultado.oResultado;

                        Session[CVariablesSesion.ProgramadoActividadPDE] = lstDato;
                        dgvProgramado.DataSource = lstDato;
                        dgvProgramado.DataBind();

                        #region Traza consulta de informacion
                        string strAccion = JsonSerializer.SerializeToString(objConsulta);
                        string strSalida = lstDato.Count().ToString();
                        string mensaje = GetType().Name + "." + MethodBase.GetCurrentMethod().Name;
                        _cTrazaInf.MRegistraTrazaInf(strAccion, mensaje, CParametrosHydro.decIdAplicacion, Convert.ToDecimal(Session[CVariablesSesion.IdUsuario].ToString()), HttpContext.Current.Request.UserHostAddress, strSalida);
                        #endregion  
                    }
                    else
                    {
                        MImprimirMensaje(objResultado.strMensaje, EnumTiposMensajeAlerta.Error);
                        Session.Remove(CVariablesSesion.ProgramadoActividadPDE);
                        dgvProgramado.DataSource = null;
                        dgvProgramado.DataBind();
                    }

                    #endregion
                }
                catch (Exception exp)
                {
                    #region RegistraError
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError", "alert('" + exp.Message + "');", true);
                    #endregion
                }
            }
            else
            {
                //todo no implementado
            }
        }
        /// <summary>
        /// METODO. MLimpiaGrilla
        /// </summary>
        private void MLimpiaGrilla()
        {
            Session.Remove(CVariablesSesion.ProgramadoActividadPDE);
            dgvProgramado.DataSource = null;
            dgvProgramado.DataBind();            
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
            //if (tipoMensaje != EnumTiposMensajeAlerta.Correcto)
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError", "alert('" + strMensaje + "');", true);
            lblMensajeError.Text = "   " + strMensaje + "   ";
        }

        protected void pnlTransaccion_Callback(object sender, CallbackEventArgsBase e)
        {   
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar                               
                    var param = e.Parameter.Split('|');
                    Session.Remove(CVariablesSesion.GrillaTransaccion);
                    dgvTransacciones.DataSource = null;
                    dgvTransacciones.DataBind();
                    dgvTransacciones.ExpandAll();             
                          
                    //var objResultado = _consulta.ObtenerIdProdePDE(
                    //new EObjetoEnvio4ParametrosProto
                    //{
                    //    strCredencial = CParametrosHydro.strCredencialFuncionario,
                    //    decFiltro = 1,
                    //    strParametro1 = cmbActividad.Value.ToString(),
                    //    strParametro2 = param[2], //IdConsumidor
                    //    strParametro3 = param[0], //Periodo
                    //    strParametro4 = param[1] //Producto
                    //});
                    //if (objResultado.intCodigo == 1)
                    //{
                        //var objDato = (O_PROGRAMACION_PDE_CTY)objResultado.oResultado;
                        var objResultadoDoc = _consulta.ObtenerDocAutorizadoPDE(
                        new EObjetoEnvio5ParametrosProto
                        {
                            strCredencial = CParametrosHydro.strCredencialFuncionario,
                            decFiltro = 1,
                            strParametro1 = cmbActividad.Value.ToString(),//objDato.decIdProde.ToString(), //IdProde
                            strParametro2 = param[2], //Accion 2: Muestra solo los Doc de sustancias controladas 3: Muestra solo los Doc de YPFB 4: Muestra todos los documentos de autorizacion                               
                            strParametro3 = param[0],
                            strParametro4 = param[1],
                            strParametro5 = param[3],
                            strParametro6 = null
                        });
                        if (objResultadoDoc.intCodigo == 1)
                        {
                            var lstDato = (List<O_DOCUMENTO_PDE_CTY>)objResultadoDoc.oResultado;
                            Session[CVariablesSesion.GrillaTransaccion] = lstDato;
                            dgvTransacciones.DataSource = lstDato;
                            dgvTransacciones.DataBind();
                        }
                        else
                        {
                            MImprimirMensaje(objResultadoDoc.strMensaje, EnumTiposMensajeAlerta.Error);
                            Session.Remove(CVariablesSesion.ProgramadoActividadPDE);
                            dgvTransacciones.DataSource = null;
                            dgvTransacciones.DataBind();
                        }
                    //}
                    //else
                    //{
                    //    MImprimirMensaje(objResultado.strMensaje, EnumTiposMensajeAlerta.Error);                      
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
        #endregion            

        protected void pucCerrarPeriodo_WindowCallback(object source, PopupWindowCallbackArgs e)
        {
            txtCodBarras.Text = "";
            lblMes.Text = cmbPeriodoMes.Text;
            lblGestion.Text = cmbPeriodoGestion.Value.ToString();
        }

        protected void btnCerrarPerido_Click(object sender, EventArgs e)
        {
             if (MValidaSesion())
            {
                try
                {
                    #region Codigo a controlar
                    decimal decFiltro = 1, decAppEstado=3;
                    string strMes = cmbPeriodoMes.Value.ToString().PadLeft(2, '0');
                    string strGestion = cmbPeriodoGestion.Value.ToString();
                    if (Convert.ToDecimal(txtCodBarras.Text)>0)
                    {
                        var ObjEstadoCerrarPDE = _gestion.PdeCierrePeriodoEstado(CParametrosHydro.strCredencialFuncionario, decFiltro, Convert.ToDecimal(txtCodBarras.Text), Convert.ToDecimal(cmbActividad.Value), Convert.ToDecimal(strGestion + strMes), decAppEstado, Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]));
                        if (ObjEstadoCerrarPDE.intCodigo == 1)
                        {
                            pucCerrarPeriodo.ShowOnPageLoad = false;
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError", "alert('Cierre del periodo registrado correctamente.');", true);

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError", "alert('" + ObjEstadoCerrarPDE.strMensaje + "');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError", "alert('El codigo de barras es requerido.');", true);
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
        
    }
}