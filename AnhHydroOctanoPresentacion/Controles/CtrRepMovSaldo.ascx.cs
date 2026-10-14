using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AnhHydroOctanoPersistencia.Aplicacion.Consulta;
using AnhHydroOctanoPersistencia.Entidades;
using AnhHydroOctanoPresentacion.Enumeradores;
using AnhHydroOctanoPresentacion.Parametros;
using DevExpress.Export;
using DevExpress.Utils;
using DevExpress.Web;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using Librerias.Anh.Us;
using System.Reflection;
using System.Drawing;
using DevExpress.Web.ASPxPivotGrid;
using System.Globalization;
using AnhHydroOctanoPersistencia.Aplicacion.Gestion;
using ServiceStack.ServiceClient.Web;

namespace AnhHydroOctanoPresentacion.Controles
{
    public partial class CtrRepMovSaldo : System.Web.UI.UserControl
    {
        #region Variables de entorno

        private readonly CPersistenciaConsulta _consulta = new CPersistenciaConsulta();
        private readonly CPersistenciaGestion _gestion = new CPersistenciaGestion();

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
                        Session.Remove(CVariablesSesion.GrillaCabecera);
                        MCargaPermisos();
                    }
                    else
                    {
                        //todo No es carga inicial: no implementado
                    }

                    if (pvgDato.DataSource == null && Session[CVariablesSesion.GrillaCabecera] != null)
                    {
                        pvgDato.DataSource = Session[CVariablesSesion.GrillaCabecera];
                        pvgDato.DataBind();
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
                                                                      "/WFRepMovSaldo.aspx?format=json");
                if (lstResultado.decCodigo == 1)
                {
                    if (lstResultado.oResultado != null && lstResultado.oResultado.Any())
                    {
                        decimal opcion = lstResultado.oResultado.FirstOrDefault().MODIFICACIONES;
                        Session[CVariablesSesion.PermisoModi] = lstResultado.oResultado.FirstOrDefault().MODIFICACIONES;
                        if (opcion == 1)
                        {
                            btnActualizar.Visible = true;
                        }
                        else
                        {
                            btnActualizar.Visible = false;
                        }
                    }
                    else
                    {
                        Session[CVariablesSesion.PermisoModi] = 0;
                        btnActualizar.Visible = false;
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

        protected void pnlDatos_Callback(object sender, CallbackEventArgsBase e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    //para buscar
                    string strFechaIni = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2, '0') + "01000000";
                    //string strFechaIni = "20200101000000";
                    string strFechaFin = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Day.ToString().PadLeft(2, '0') + "235959";

                    var objResultado = _consulta.ReporteEntDepAct(
                    new EObjetoEnvio4ParametrosProto
                    {
                        strCredencial = "",
                        decFiltro = 1,
                        strParametro1 = strFechaIni,
                        strParametro2 = strFechaFin,
                        strParametro3 = "1",
                        strParametro4 = cmbDepartamento.Value.ToString()
                    });
                    if (objResultado.intCodigo == 1)
                    {
                        var lstDato = (List<O_REPORTE_ENT_ACTDEP_CTY>)objResultado.oResultado;

                        var lstSaldo = new List<O_REPORTE_SALDO_DIA_CTY>();

                        foreach (var lst in lstDato)
                        {
                            var dato = new O_REPORTE_SALDO_DIA_CTY();

                            dato.ID_CONSUMIDOR = lst.ID_CONSUMIDOR;
                            dato.DENOMINACION = lst.DENOMINACION;
                            dato.ID_PRODUCTO = lst.ID_PRODUCTO;
                            dato.PRODUCTO = lst.PRODUCTO;
                            dato.SALDO_INICIO_MES = lst.CANTIDAD;
                            dato.INGRESOS = lst.RECEPCION + lst.DEMASIA;
                            dato.EGRESOS = lst.VENTA + lst.MERMA;
                            dato.SALDO_ACTUAL = lst.CANTIDAD + lst.RECEPCION + lst.DEMASIA - lst.VENTA - lst.MERMA;

                            lstSaldo.Add(dato);
                        }

                        if (lstSaldo.Count() > 0)
                        {
                            pvgDato.DataSource = lstSaldo;
                            pvgDato.DataBind();

                            Session[CVariablesSesion.GrillaCabecera] = lstSaldo;

                        }
                        else
                        {
                            pvgDato.DataSource = null;
                            pvgDato.DataBind();
                            MImprimirMensaje("No se tiene registros", EnumTiposMensajeAlerta.Alerta);
                        }

                    }
                    else
                    {
                        pvgDato.DataSource = null;
                        pvgDato.DataBind();
                        MImprimirMensaje(objResultado.strMensaje, EnumTiposMensajeAlerta.Alerta);
                    }
                    
                    #endregion
                }
                catch (Exception exp)
                {
                    #region RegistraError

                    //MLimpiaGrilla();
                    //MImprimirMensaje(exp.Message, EnumTiposMensajeAlerta.Error);
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

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    pgeExportar.ASPxPivotGridID = "pvgDato";
                    pgeExportar.ExportXlsxToResponse("DjAcumulado");

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

        protected void pvgDato_HtmlFieldValuePrepared(object sender, PivotHtmlFieldValuePreparedEventArgs e)
        {
            if (Object.ReferenceEquals(e.Field, fieldSaldoActual))
            {
                if (Convert.ToDecimal(e.Value) <= 0)
                    e.Cell.Style[HtmlTextWriterStyle.BackgroundColor] = ColorTranslator.ToHtml(Color.Red);
                else if (Convert.ToDecimal(e.Value) > 0 && Convert.ToDecimal(e.Value) < 2000)
                    e.Cell.Style[HtmlTextWriterStyle.BackgroundColor] = ColorTranslator.ToHtml(Color.LightPink);
                else if (Convert.ToDecimal(e.Value) >= 2000 && Convert.ToDecimal(e.Value) < 5000)
                    e.Cell.Style[HtmlTextWriterStyle.BackgroundColor] = ColorTranslator.ToHtml(Color.Yellow);
                else if (Convert.ToDecimal(e.Value) >= 5000)
                    e.Cell.Style[HtmlTextWriterStyle.BackgroundColor] = ColorTranslator.ToHtml(Color.Green);
                else
                    e.Cell.Style[HtmlTextWriterStyle.BackgroundColor] = ColorTranslator.ToHtml(Color.White);
            }
            else
            {
                //no implementado
            }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            decimal decFiltro = 1;
            decimal decIdDepartamento = Convert.ToDecimal(cmbDepartamento.Value);

            if (decIdDepartamento == 10)
            {                
                MImprimirMensaje("Seleccione un departamento", EnumTiposMensajeAlerta.Alerta);
            }
            else
            {
                var objResultado = _gestion.ActualizarSaldoMesDepartamento(decFiltro, decIdDepartamento);
                if (objResultado.ID_TABLA == 1)
                {                    
                    MImprimirMensaje(objResultado.MENSAJE_ERROR, EnumTiposMensajeAlerta.Correcto);
                    pvgDato.DataSource = null;
                    pvgDato.DataBind();
                }
                else
                {                    
                    MImprimirMensaje(objResultado.MENSAJE_ERROR, EnumTiposMensajeAlerta.Alerta);
                    pvgDato.DataSource = null;
                    pvgDato.DataBind();
                }
            }
        }
    }
}