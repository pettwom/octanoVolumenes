using AnhHydroOctanoPersistencia.Aplicacion.Consulta;
using AnhHydroOctanoPersistencia.Aplicacion.Gestion;
using AnhHydroOctanoPersistencia.Aplicacion.Reportes;
using AnhHydroOctanoPersistencia.Entidades;
using AnhHydroOctanoPresentacion.Clases.Controles;
using AnhHydroOctanoPresentacion.Entidades;
using AnhHydroOctanoPresentacion.Enumeradores;
using AnhHydroOctanoPresentacion.Parametros;
using DevExpress.Web;
using Librerias.Anh.Us;
using ServiceStack.ServiceClient.Web;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace AnhHydroOctanoPresentacion.Controles
{
    using Clases.Util;
    using AnhHydroOctanoPresentacion.Lib;
    public partial class CtrProde : System.Web.UI.UserControl
    {
        #region variables de entorno

        private readonly CPersistenciaConsulta _consulta = new CPersistenciaConsulta();
        private readonly CPersistenciaReportes _reporte = new CPersistenciaReportes();
        readonly JsonServiceClient _client = new JsonServiceClient(CAppSettings.servicio_octVolumen);
        private readonly CPersistenciaGestion _gestion = new CPersistenciaGestion();
        readonly CUtil _cUtil = new CUtil();
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
                        Session.Remove("PATH");                 
                        Session.Remove(CVariablesSesion.ListaProyectoProducto);
                        Session.Remove(CVariablesSesion.ListaParametrica);
                        Session.Remove(CVariablesSesion.PrimerDiaSemana);
                        Session.Remove(CVariablesSesion.Callback);
                        Session.Remove(CVariablesSesion.ArchivoDescarga);
                        Session.Remove(CVariablesSesion.GrillaTransaccion);
                        Session.Remove(CVariablesSesion.GrillaRegObservado);

                        MCargaArbol();
                        MCargaParametricas();
                        
                        var objParametricas = (O_PARAMETRICAS_GRAL)Session[CVariablesSesion.ListaParametrica];
                        cmbPeriodoGestion.SelectedItem = cmbPeriodoGestion.Items.FindByValue(Convert.ToInt32(DateTime.Now.Year.ToString()));
                        cmbPeriodoMes.SelectedItem = cmbPeriodoMes.Items.FindByValue(Convert.ToInt32(DateTime.Now.Month.ToString()));

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

        #endregion

        #region #region Metodos y funciones
        private void CargarProyProd()
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    var objColeccion = new List<O_REPORTE_PROYECTO_PRODUCTO_CTY>();

                    var objResultado = _reporte.ReporteProyectoProducto(
                            new EObjetoEnvio1ParametrosProto
                            {
                                strCredencial = CParametrosHydro.strCredencialFuncionario,
                                decFiltro = 1,
                                strParametro1 = ""
                            });
                    if (objResultado.intCodigo == 1)
                    {
                        objColeccion = (List<O_REPORTE_PROYECTO_PRODUCTO_CTY>)objResultado.oResultado;
                    }
                    Session[CVariablesSesion.ListaProyectoProducto] = objColeccion;

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

        private void MCargaParametricas()
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

        private int MCodificaParametrica(int intParametrica, string strDescripcion, string strDescripcion2)
        {
            #region Proceso a Ejecutar

            var lngResultado = -99999;

            var lstEntidad = Session[CVariablesSesion.ListaProyectoProducto] as List<O_REPORTE_PROYECTO_PRODUCTO_CTY>;

            if (lstEntidad != null && lstEntidad.Any())
            {
                switch (intParametrica)
                {
                    case (int)EnumTipoParametrica.Sirehidro:
                        lstEntidad = lstEntidad.Where(x => x.COD_HYDRO_PROYECTO == strDescripcion).ToList();
                        if (lstEntidad.Any())
                        {
                            lngResultado = Convert.ToInt32(lstEntidad.FirstOrDefault().ID_CONSUMIDOR);
                        }
                        else
                        {
                            lngResultado = -99999;
                        }
                        break;

                    case (int)EnumTipoParametrica.EntidadSirehidro:
                        lstEntidad = lstEntidad.Where(x => x.COD_HYDRO_PROYECTO == strDescripcion && x.DENOMINACION_PROYECTO == strDescripcion2).ToList();
                        if (lstEntidad.Any())
                        {
                            lngResultado = Convert.ToInt32(lstEntidad.FirstOrDefault().ID_CONSUMIDOR);
                        }
                        else
                        {
                            lngResultado = -99999;
                        }
                        break;

                    case (int)EnumTipoParametrica.SirehidroDepartamento:
                        lstEntidad = lstEntidad.Where(x => x.COD_HYDRO_PROYECTO == strDescripcion && x.DEPARTAMENTO == strDescripcion2).ToList();
                        if (lstEntidad.Any())
                        {
                            lngResultado = Convert.ToInt32(lstEntidad.FirstOrDefault().ID_CONSUMIDOR);
                        }
                        else
                        {
                            lngResultado = -99999;
                        }
                        break;

                    case (int)EnumTipoParametrica.SirehidroActividad:
                        lstEntidad = lstEntidad.Where(x => x.COD_HYDRO_PROYECTO == strDescripcion && x.ACTIVIDAD == strDescripcion2).ToList();
                        if (lstEntidad.Any())
                        {
                            lngResultado = Convert.ToInt32(lstEntidad.FirstOrDefault().ID_CONSUMIDOR);
                        }
                        else
                        {
                            lngResultado = -99999;
                        }
                        break;

                    case (int)EnumTipoParametrica.SirehidroProducto:
                        lstEntidad = lstEntidad.Where(x => x.COD_HYDRO_PROYECTO == strDescripcion && x.PRODUCTO.Contains(strDescripcion2)).ToList();
                        if (lstEntidad.Any())
                        {
                            lngResultado = Convert.ToInt32(lstEntidad.FirstOrDefault().ID_CONSUMIDOR);
                        }
                        else
                        {
                            lngResultado = -99999;
                        }
                        break;
                }
            }
            else
            {
                lngResultado = -99999;
            }

            return lngResultado;

            #endregion
        }

        protected void dgvTransaccionError_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
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

        private bool MValidaLineaProde(string[] values, ref string strMensaje)
        {
            #region Proceso a Ejecutar

            bool strResultado = true;

            try
            {
                #region validaciones por linea

                if (values.Length != 8)
                {
                    strMensaje = "- Nro. de columnas del registro invalido.";
                }
                else
                {
                    strMensaje = "";
                }

                var arrayNumero = new[] { 3, 4, 5, 6, 7 };
                decimal dec;
                for (int i = 0; i < arrayNumero.Length; i++)
                {
                    if (!decimal.TryParse(values[arrayNumero[i]].Trim(), out dec))
                    {
                        strMensaje += "- Columna Nro. " + arrayNumero[i] + " No es un número.";
                    }
                    else
                    {
                        strMensaje += "";
                    }
                }

                if (MCodificaParametrica((int)EnumTipoParametrica.Sirehidro, values[1].Trim(), "") == -99999)
                {
                    strMensaje += "- Codigo Sirehidro no registrado.";
                }
                else if (MCodificaParametrica((int)EnumTipoParametrica.EntidadSirehidro, values[1].Trim(), values[2].Trim()) == -99999)
                {
                    strMensaje += "- El codigo Sirehidro no le pertenece al proyecto.";
                }
                else if (MCodificaParametrica((int)EnumTipoParametrica.SirehidroDepartamento, values[1].Trim(), values[0].Trim()) == -99999)
                {
                    strMensaje += "- El codigo Sirehidro no le pertenece al departamento.";
                }
                else
                {
                    strMensaje += "";
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
            else
            {
                strResultado = true;
            }

            return strResultado;

            #endregion
        }

        private bool MValidaSesion()
        {
            #region Proceso a Ejecutar

            bool booResultado;
            //borrar
            //Session[CVariablesSesion.IdUsuario] = "1802";
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

        /// <summary>
        /// METODO. MCargaArbol
        /// </summary>
        private void MCargaArbol()
        {
            CConsultasParametricas cConsultasParametricas = new CConsultasParametricas();
            var objArbol = Session[CVariablesSesion.ObjetoArbol] as List<O_OBTIENE_ENTIDADES_CTY>;
            if (objArbol == null)
            {
                var objResultado = _consulta.ObtenerEntidades(
                new EObjetoEnvio5ParametrosProto
                {
                    decFiltro = 1,
                    strParametro1 = Session[CVariablesSesion.IdUsuario].ToString(),
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
                    MImprimirMensaje(objResultado.strMensaje, EnumTiposMensajeAlerta.Error);
                    return;
                }
            }

            Session[CVariablesSesion.EstructuraArbol] = _cUtil.MObtenerArbolOrdenado(Properties.Settings.Default.intOrdenArbol, objArbol);
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
                    strMensaje = "";
                    break;
                default:
                    pnlMensaje.Visible = false;
                    lblMensajeError.ForeColor = Color.WhiteSmoke;
                    strMensaje = "-";
                    break;
            }
            lblMensajeError.Text = "   " + strMensaje + "   ";
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError", "alert('" + strMensaje + "');", true);
        }
        #endregion 
    }
}