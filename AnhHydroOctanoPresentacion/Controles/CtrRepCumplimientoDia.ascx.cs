
using AnhHydroOctanoPresentacion.Lib;
using DevExpress.Export;
using DevExpress.Utils;
using DevExpress.XtraPrinting;

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
    using Page = System.Web.UI.Page;
    using ServiceStack.Text;
    using AnhHydroOctanoPresentacion.Sitio.Usuarios;
    
    public partial class CtrRepCumplimientoDia : UserControl
    {
        #region Variables de entorno

        readonly JsonServiceClient _client = new JsonServiceClient(CAppSettings.servicio_octVolumen);
        private readonly CPersistenciaConsulta _consulta = new CPersistenciaConsulta();
        readonly CUtil _cUtil = new CUtil();
        readonly CTrazaInf _cTrazaInf = new CTrazaInf();
        CConsultasParametricas cConsultasParametricas = new CConsultasParametricas();
        readonly JsonServiceClient _clientAdminHydro = new JsonServiceClient(CAppSettings.ServicioAdministradorHydro);
        #endregion

        #region Eventos de Formulario

        protected void Page_Load(object sender, EventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Codigo a controlar
                    if (!Page.IsCallback && !Page.IsPostBack)
                    {
                        Session.Remove(CVariablesSesion.EstructuraArbol);
                        Session.Remove(CVariablesSesion.GrillaCabecera);
                        Session.Remove(CVariablesSesion.FechaHoraServ);
                        Session.Remove(CVariablesSesion.GrillaCabecera);
                        Session.Remove(CVariablesSesion.ObtenerEntidades);

                        MCargaArbol();
                        MIniciaFechas();
                    }

                    tlsOperadores.DataSource = Session[CVariablesSesion.EstructuraArbol];
                    tlsOperadores.DataBind();

                    if (pvgReporte.DataSource == null && Session[CVariablesSesion.GrillaCabecera] != null)
                    {
                        pvgReporte.DataSource = Session[CVariablesSesion.GrillaCabecera];
                        pvgReporte.DataBind();
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
                    #region Codigo a controlar

                    lblActividad.Text = tlsOperadores.FocusedNode.GetValue("StrDescPadre").ToString();
                    MLimpiaGrilla();
                    if ((int)tlsOperadores.FocusedNode.GetValue("IntAccion") > 0)
                    {
                        if (MValidaDatosIniciales())
                        {
                            decimal decIdActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad"));
                            MCargaGrilla(decIdActividad);
                        }
                    }

                    #endregion
                }
                catch (Exception exp)
                {
                    #region RegistraError
                    MLimpiaGrilla();
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

        protected void pvgReporte_CustomCellStyle(object sender, DevExpress.Web.ASPxPivotGrid.PivotCustomCellStyleEventArgs e)
        {
            if (e.Value == null || (decimal)e.Value == 0)
            {
                var x = e.RowValueType;
                var y = e.ColumnValueType;
                if (x.ToString().Contains("Value") && !y.ToString().Contains("Total"))
                {
                    e.CellStyle.BackColor = Color.FromArgb(241, 186, 118);
                }
                else
                {
                    e.CellStyle.BackColor = Color.FromArgb(239, 244, 218);
                }
            }
            else
            {
                //todo no implementado.
            }
        }

        protected void btnEmitir_Click(object sender, EventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    pvgExportar.ASPxPivotGridID = "pvgReporte";
                    //pvgExportar.ExportXlsxToResponse("CumplimientoDiario");

                    var options = new XlsxExportOptionsEx()
                    {
                        ExportType = ExportType.DataAware,
                        AllowGrouping = DefaultBoolean.False,
                        TextExportMode = TextExportMode.Value,
                        AllowFixedColumns = DefaultBoolean.False,
                        AllowFixedColumnHeaderPanel = DefaultBoolean.True,
                        RawDataMode = false
                    };
                    pvgExportar.ExportXlsxToResponse("CumplimientoDiario", options);



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

        /// <summary>
        /// METODO. MValidaSesion
        /// </summary>
        /// <returns></returns>
        private bool MValidaSesion()
        {
            bool booResultado;
            //borrar
            //Session[CVariablesSesion.IdUsuario] = "1802";
            //Session[CVariablesSesion.DatosUsuario] = "xxx";

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
        /// METODO. MIniciaFechas
        /// </summary>
        private void MIniciaFechas()
        {
            detFechaIni.Date = DateTime.Now;
            detFechaFin.Date = DateTime.Now;
            //Obtener perfiles del usuario
            var objPerfil = _clientAdminHydro.Get<EResultadoLista>("/EReporteUsuariosPerfiles/" + CParametrosHydro.strCredencialHydroAdmin + "/" + Convert.ToDecimal(CAppSettings.idAplicacion) + "/" + Session[CVariablesSesion.IdUsuario] + "?format=json");
             if (objPerfil.decCodigo == 1)
             {
                 int intCount = 0;                        
                 foreach (var item in objPerfil.oResultado)
                 {
                     if (item.NOMBRE_PERFIL == "FUNCIONARIO ANH")
                     {
                         intCount = intCount + 1;                 
                     }
                 }
                 if (intCount > 0)
                 {
                     lblDpto.Visible = true;
                     cmbDepartamento.Visible = true;    
                 }
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
                    MImprimirMensaje(objResultado.strMensaje, EnumTiposMensajeAlerta.Error);
                    return;
                }
            }

            Session[CVariablesSesion.EstructuraArbol] = _cUtil.MObtenerArbolOrdenado(0, objArbol);
        }

        /// <summary>
        /// METODO. MCargaGrilla
        /// </summary>
        /// <param name="intIdActividad"></param>
        private void MCargaGrilla(decimal intIdActividad)
        {
            var objConsulta = new EObjetoEnvio6ParametrosProto
            {
                strCredencial = CParametrosHydro.strCredencialFuncionario,
                decFiltro = 1,
                strParametro1 = Session[CVariablesSesion.IdUsuario].ToString(),
                strParametro2 = intIdActividad.ToString(),
                strParametro3 = detFechaIni.Date.Year + detFechaIni.Date.Month.ToString().PadLeft(2, '0') + detFechaIni.Date.Day.ToString().PadLeft(2, '0'),
                strParametro4 = detFechaFin.Date.Year + detFechaFin.Date.Month.ToString().PadLeft(2, '0') + detFechaFin.Date.Day.ToString().PadLeft(2, '0'),
                strParametro5 = CParametrosHydro.decIdAplicacion.ToString(),
                strParametro6 = cmbDepartamento.Value.ToString()
            };
            var objReporte = _consulta.ListaObligacionDia(objConsulta);

            if (objReporte.intCodigo == 1)
            {
                pvgReporte.DataSource = objReporte.oResultado;
                pvgReporte.DataBind();
                Session[CVariablesSesion.GrillaCabecera] = objReporte.oResultado;

                #region Traza consulta de informacion
                string strAccion = JsonSerializer.SerializeToString(objConsulta);
                string strSalida = ((List<AnhHydroOctanoPersistencia.Entidades.O_CUMPLIMIENTO_DIA_CTY>)objReporte.oResultado).Count.ToString();
                string mensaje = GetType().Name + "." + MethodBase.GetCurrentMethod().Name;
                _cTrazaInf.MRegistraTrazaInf(strAccion, mensaje, CParametrosHydro.decIdAplicacion, Convert.ToDecimal(Session[CVariablesSesion.IdUsuario].ToString()), HttpContext.Current.Request.UserHostAddress, strSalida);
                #endregion  
            }
            else
            {
                MImprimirMensaje(objReporte.strMensaje, EnumTiposMensajeAlerta.Error);
            }
        }

        /// <summary>
        /// METODO. MValidaDatosIniciales
        /// </summary>
        /// <returns></returns>
        private bool MValidaDatosIniciales()
        {
            var booResultado = true;

            if (detFechaIni.Text == "")
            {
                MImprimirMensaje("Introduzca un día válido.", EnumTiposMensajeAlerta.Error);
                booResultado = false;
            }
            else if (detFechaIni.Date > detFechaFin.Date)
            {
                MImprimirMensaje("Introduzca fechas válida.", EnumTiposMensajeAlerta.Error);
                booResultado = false;
            }
            return booResultado;
        }

        /// <summary>
        /// METODO. MLimpiaGrilla
        /// </summary>
        private void MLimpiaGrilla()
        {
            Session.Remove(CVariablesSesion.GrillaCabecera);
            pvgReporte.DataSource = null;
            pvgReporte.DataBind();

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
            if (tipoMensaje != EnumTiposMensajeAlerta.Correcto)
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError", "alert('" + strMensaje + "');", true);
            lblMensajeError.Text = "   " + strMensaje + "   ";

        }

        #endregion

    }
}