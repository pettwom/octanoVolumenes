using AnhHydroOctanoPresentacion.Lib;
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

    public partial class CtrImportacionDatos : UserControl
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
                        Session.Remove(CVariablesSesion.IdEntidadRevision);

                        Session.Remove(CVariablesSesion.ListaPoiRecepcion);
                        Session.Remove(CVariablesSesion.ListaPoiEntrega);

                        //Session.Remove(CVariablesSesion.ListaPoi);

                        Session.Remove(CVariablesSesion.ComboTipoOperacion);

                        Session[CVariablesSesion.Callback] = "0";

                        MCargaArbol();
                        MCargaParametricas();

                        MCargaPermisos();
                        MConfigurarFormulario();

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

                    if (dgvTransacciones.DataSource == null && Session[CVariablesSesion.GrillaTransaccion] != null)
                    {
                        dgvTransacciones.DataSource = Session[CVariablesSesion.GrillaTransaccion];
                        dgvTransacciones.DataBind();
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
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    cmbTipoOperacion.DataSource = null;
                    cmbTipoOperacion.Text = "";

                    cmbPeriodo.Items.Clear();
                    cmbPeriodo.Text = "";

                    if (tlsOperadores.FocusedNode != null)
                    {
                        if ((int)tlsOperadores.FocusedNode.GetValue("IntAccion") > 0)
                        {
                            //PARAMETRICAS GENERALES
                            Session.Remove(CVariablesSesion.IdEntidadRevision);
                            var objParametros = _client.Post<EResultadoOctanoParametro>(CAppSettings.svc_listar_octano_parametrica,
                            new
                            {
                                strCredencial = CParametrosHydro.strCredencialFuncionario,
                                decFiltro = 2,//1: ID_TIPO_ACTIVIDAD 2: ID_TIPO_ACTIVIDAD y VALOR_TIPO
                                strParametro1 = "9",//VALOR_TIPO
                                strParametro2 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
                                strParametro3 = "",
                                strParametro4 = ""
                            });
                            if (objParametros.OResultado.Count > 0 && objParametros.IntCodigo == 1)
                            {
                                Session[CVariablesSesion.IdEntidadRevision] = objParametros.OResultado.FirstOrDefault().VALOR_FIN;

                                if (Session[CVariablesSesion.IdEntidadRevision] != null)
                                {
                                    var objEntidadRel = _consulta.ObtenerEntidades(
                                    new EObjetoEnvio5ParametrosProto
                                    {
                                        decFiltro = 2,
                                        strParametro1 = "",
                                        strParametro2 = (string)Session[CVariablesSesion.IdEntidadRevision],
                                        strParametro3 = CAppSettings.idAplicacion,
                                        strParametro4 = "",
                                        strParametro5 = ""
                                    });
                                    if (objEntidadRel.intCodigo == 1)
                                    {
                                        var lstEntidadRel = (List<O_OBTIENE_ENTIDADES_CTY>)objEntidadRel.oResultado;
                                        var objEntidades = _client.Post<EResultadoConfigHidrocarburo>(CAppSettings.svc_listar_autorizacion_condicion,
                                        new
                                        {
                                            strCredencial = CParametrosHydro.strCredencialFuncionario,
                                            decFiltro = 3,
                                            strParametro1 = Convert.ToInt32(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString()),
                                            strParametro2 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
                                            strParametro3 = ""
                                        });
                                        if (objEntidades.IntCodigo == 1)
                                        {
                                            var lstAlias = objEntidades.OResultado.Where(x => x.DESCRIPCION != null).ToList();
                                            if (lstAlias.Any())
                                            {
                                                foreach (var reg in lstAlias)
                                                {
                                                    lstEntidadRel.Where(w => w.ID_ACTIVIDAD == reg.ID_SUB_IDENTIFICADOR && w.ID_CONSUMIDOR == reg.ID_IDENTIFICADOR).ToList().ForEach(f => f.USUARIO_ASIGNADO = reg.DESCRIPCION);
                                                }
                                            }
                                        }
                                        Session[CVariablesSesion.ListaEntidad] = lstEntidadRel;
                                    }
                                }
                            }

                            var objPeriodo = _client.Post<EResultadoPeriodos>(CAppSettings.svc_lista_periodos_consumidor,
                            new
                            {
                                strCredencial = CParametrosHydro.strCredencialFuncionario,
                                decFiltro = 1,
                                strParametro1 = tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString(),
                                strParametro2 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
                                strParametro3 = "",
                                strParametro4 = "",
                                strParametro5 = tlsOperadores.FocusedNode.GetValue("DecIdDepartamento").ToString()
                            });
                            if (objPeriodo.IntCodigo == 1)
                            {
                                int decCod;
                                foreach (var dato in objPeriodo.OResultado)
                                {
                                    decCod = Convert.ToInt32(dato.decAño + dato.decMes.ToString().PadLeft(2, '0'));
                                    cmbPeriodo.Items.Add(dato.strPeriodo, decCod);
                                }

                                decCod = Convert.ToInt32(objPeriodo.OResultado[0].decAño + objPeriodo.OResultado[0].decMes.ToString().PadLeft(2, '0'));
                                cmbPeriodo.Value = decCod;

                                var objTipoOperacion = _client.Post<EResultadoTipoOperacion>(CAppSettings.svc_obtener_tipos_operacion_act,
                                new
                                {
                                    decFiltro = 1,
                                    strParametro1 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
                                    strParametro2 = "",
                                    strParametro3 = ""
                                });
                                if (objTipoOperacion.IntCodigo == 1)
                                {
                                    objTipoOperacion.OResultado = objTipoOperacion.OResultado.OrderByDescending(x => x.FACTOR_CALCULO).ThenBy(y => y.NOMBRE).ToList();
                                    cmbTipoOperacion.DataSource = objTipoOperacion.OResultado;
                                    cmbTipoOperacion.DataBind();
                                    Session[CVariablesSesion.ComboTipoOperacion] = objTipoOperacion.OResultado;
                                }
                                else
                                {
                                    MImprimirMensaje(objTipoOperacion.StrMensaje, EnumTiposMensajeAlerta.Error);
                                }
                            }
                            else
                            {
                                MImprimirMensaje(objPeriodo.StrMensaje, EnumTiposMensajeAlerta.Error);
                            }

                            //en caso de transporte ducto gas
                            List<O_EQUIPO_RD_CTY> EquiposRD = new List<O_EQUIPO_RD_CTY>();
                            decimal _decIdEntidad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdProyecto").ToString());

                            if (tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString() == "108")
                            {
                                var oResultadoEquiposWs = _clientI.Get<EResultadoEquipos>("/ListadoEquipoRd/" + CParametrosHydro.strCredencialInfra + "/" + _decIdEntidad + "/" + 108 + "/" + 0 + "/" + 0 + "/" + 0 + "?format=json");
                                if (oResultadoEquiposWs.decCodigo > 0)
                                {
                                    if (oResultadoEquiposWs.oResultado.Count > 0)
                                    {
                                        EquiposRD = oResultadoEquiposWs.oResultado;
                                        Session[CVariablesSesion.ListaPoi] = oResultadoEquiposWs.oResultado;
                                        //grdEquipos.DataSource = EquiposRD;                                       
                                    }
                                    else
                                    {
                                        EquiposRD = new List<O_EQUIPO_RD_CTY>();
                                        Session[CVariablesSesion.ListaPoi] = EquiposRD;
                                        //grdEquipos.DataSource = new List<O_EQUIPO_RD_CTY>();
                                    }
                                }
                                else
                                {
                                    EquiposRD = new List<O_EQUIPO_RD_CTY>();
                                    Session[CVariablesSesion.ListaPoi] = EquiposRD;
                                    //grdEquipos.DataSource = new List<O_EQUIPO_RD_CTY>();
                                }
                            }
                            else if (tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString() == "12")
                            {
                                var oResultadoEquiposWs = _clientI.Get<EResultadoEquipos>("/ListadoEquipoRd/" + CParametrosHydro.strCredencialInfra + "/" + _decIdEntidad + "/" + 12 + "/" + 0 + "/" + 0 + "/" + 0 + "?format=json");
                                if (oResultadoEquiposWs.decCodigo > 0)
                                {
                                    if (oResultadoEquiposWs.oResultado.Count > 0)
                                    {
                                        EquiposRD = oResultadoEquiposWs.oResultado;
                                        Session[CVariablesSesion.ListaPoi] = oResultadoEquiposWs.oResultado;
                                        //grdEquipos.DataSource = EquiposRD;                                       
                                    }
                                    else
                                    {
                                        EquiposRD = new List<O_EQUIPO_RD_CTY>();
                                        Session[CVariablesSesion.ListaPoi] = EquiposRD;
                                        //grdEquipos.DataSource = new List<O_EQUIPO_RD_CTY>();
                                    }
                                }
                                else
                                {
                                    EquiposRD = new List<O_EQUIPO_RD_CTY>();
                                    Session[CVariablesSesion.ListaPoi] = EquiposRD;
                                    //grdEquipos.DataSource = new List<O_EQUIPO_RD_CTY>();
                                }
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
                Session[CVariablesSesion.Callback] = "0";
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
                    MMuestraCabecera();
                    MLimpiaGrilla();

                    if ((int)tlsOperadores.FocusedNode.GetValue("IntAccion") > 0)
                    {

                        string strMensaje = string.Empty;
                        if (MValidaDatosIniciales(ref strMensaje))
                        {
                            if (MValidaCondicionesFuncionamiento(ref strMensaje))
                            {
                                MCargaInfraestructura();
                                btnImportar.Enabled = true;
                                btnLimpiaTo.Enabled = true;
                            }
                            else
                            {
                                MImprimirMensaje(strMensaje, EnumTiposMensajeAlerta.Error);
                            }
                        }
                        else
                        {
                            MImprimirMensaje(strMensaje, EnumTiposMensajeAlerta.Alerta);
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

        protected void dgvTransacciones_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            dgvTransacciones.DataSource = null;
            dgvTransacciones.DataBind();
        }
        protected void dgvTransaccionRefineria_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            dgvTransaccionRefineria.DataSource = null;
            dgvTransaccionRefineria.DataBind();
        }

        protected void dgvTransTransporteDuctos_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            dgvTransTransporteDuctos.DataSource = null;
            dgvTransTransporteDuctos.DataBind();
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

                    string strMensaje = string.Empty;
                    if (MValidaDatosIniciales(ref strMensaje))
                    {
                        if (FileUploader.HasFile)
                        {
                            MLimpiaGrilla();
                            string strExtension = Path.GetExtension(FileUploader.FileName).ToUpper();
                            if (strExtension == ".CSV")
                            {
                                var strRutaServer = Path.GetTempPath();
                                var strNombreArchivo = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString() + tlsOperadores.FocusedNode.GetValue("IntIdOperador") + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + DateTime.Now.Millisecond;

                                Session["PATH"] = strRutaServer + strNombreArchivo;
                                FileUploader.SaveAs((string)Session["PATH"]);

                                MCargarDatos((string)Session["PATH"]);
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError", "LoadingPanel.Hide();", true);

                                if (dgvTransaccionError.VisibleRowCount > 0)
                                {
                                    Session[CVariablesSesion.ArchivoDescarga] = dgvTransaccionError.DataSource;
                                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( 'WFDescarga.aspx', '')", true);
                                }
                                else
                                {
                                    var lstRegistros = Session[CVariablesSesion.GrillaTransaccion] as List<CRegistrosImportados>;
                                    if (lstRegistros != null && lstRegistros.Any())
                                    {
                                        var strFechaMin = lstRegistros.Min(x => x.StrFechaEmision);
                                        var strFechaMax = lstRegistros.Max(x => x.StrFechaEmision);
                                        decimal decFechaIni = Convert.ToDecimal(strFechaMin.Substring(6, 4) + strFechaMin.Substring(3, 2) + strFechaMin.Substring(0, 2));
                                        decimal decFechaFin = Convert.ToDecimal(strFechaMax.Substring(6, 4) + strFechaMax.Substring(3, 2) + strFechaMax.Substring(0, 2));
                                        //Verifica si existen movimientos registrados en el periodo un tipo de operacion
                                        var objValidacion = _client.Post<EResultadoGenerico>(CAppSettings.svc_validaciones_general,
                                        new
                                        {
                                            strCredencial = CParametrosHydro.strCredencialFuncionario,
                                            strFiltro = "2",
                                            strParametro1 = tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString(),
                                            strParametro2 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
                                            strParametro3 = cmbTipoOperacion.Value,
                                            strParametro4 = decFechaIni,
                                            strParametro5 = decFechaFin,
                                            strParametro6 = "",
                                            decAppFechaRegistro = 0,
                                            decAppIdUsuario = 0
                                        });
                                        if (objValidacion.IntCodigo == 1)
                                        {
                                            if (objValidacion.OResultado.RESULTADO != 1)
                                            {
                                                ScriptManager.RegisterStartupScript(this, typeof(string), "showError", "if (confirm('Existe " + objValidacion.OResultado.ID_TABLA + " movimiento(s) registrado(s) entre las fechas que se importarán, si continua el proceso se ELIMINARAN los anteriores. ¿Desea ir a la pantalla Detalle de Movimientos para revisarlos?')){window.location.href = 'WFFlujoOperador.aspx';}", true);
                                            }
                                            else
                                            {
                                                //todo No Implementado.
                                            }
                                        }
                                        else
                                        {
                                            MImprimirMensaje(objValidacion.StrMensaje, EnumTiposMensajeAlerta.Error);
                                        }
                                    }
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
                        //if ((int)tlsOperadores.FocusedNode.GetValue("IntIdActividad") == (int)EnumActividades.Refinerias)
                        //{
                        //    var lstRegistros = Session[CVariablesSesion.GrillaRefineria] as List<CMovimientoRefineria>;
                        //    var lstTiOp = lstRegistros;                            
                        //    foreach (var tipo in lstTiOp)
                        //    {
                        //        long lngNoMovimientos = 1;
                        //        //var lstReg = lstRegistros.Where(x => x.DecIdTipoOp == tipo.DecIdTipoOp).ToList();

                        //        if (tipo != null)
                        //        {
                        //            var lstProtoMovimientos = new List<ObjProtoMovimientoTransaccion>();
                        //            //var strFechaMin = tipo.DecFecha;
                        //            //var strFechaMax = tipo.DecFecha;
                        //            var decFechaIni = Convert.ToDecimal(tipo.DecFecha.ToString().Substring(0, 8));
                        //            var decFechaFin = Convert.ToDecimal(tipo.DecFecha.ToString().Substring(0, 8));

                        //            var tareasMovimientos = _gestion.TareasMovimientos(1, 5, Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString()), Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()), Convert.ToDecimal(cmbTipoOperacion.Value), decFechaIni, decFechaFin);

                        //            if (tareasMovimientos.intCodigo == 1)
                        //            {
                        //                int intControl = 1;
                        //                if (intControl == 0)
                        //                {
                        //                    lstProtoMovimientos = new List<ObjProtoMovimientoTransaccion>();
                        //                    intControl++;
                        //                }
                        //                else
                        //                {
                        //                    //todo No implementado.
                        //                }

                        //                ObjProtoMovimientoTransaccion objProtoReg = tipo.objProtoMovimiento;
                        //                lstProtoMovimientos.Add(objProtoReg);

                        //                //if (lstProtoMovimientos.Count == 2000)
                        //                //{
                        //                intControl = 0;
                        //                MEnviaObjeto(lstProtoMovimientos, true, lngNoMovimientos, decFechaIni, decFechaFin, Convert.ToDecimal(cmbTipoOperacion.Value), 1);


                        //                //if (lngNoMovimientos == lstReg.Count)
                        //                //{
                        //                //    lstProtoMovimientos = new List<ObjProtoMovimientoTransaccion>();
                        //                //}
                        //                //else
                        //                //{
                        //                //    //todo No implementado.
                        //                //}
                        //                //}
                        //                //else
                        //                //{
                        //                //    intControl++;
                        //                //}

                        //                lngNoMovimientos++;
                        //                //foreach (var registro in tipo)
                        //                //{

                        //                //}

                        //                //if (lstProtoMovimientos.Count > 0)
                        //                //{
                        //                //    MEnviaObjeto(lstProtoMovimientos, true, lngNoMovimientos - 1, decFechaIni,
                        //                //        decFechaFin, Convert.ToDecimal(cmbTipoOperacion.Value), 1);
                        //                //}
                        //                //else
                        //                //{
                        //                //    //todo No implementado.
                        //                //}
                        //            }
                        //            else
                        //            {
                        //                MImprimirMensaje(tareasMovimientos.strMensaje, EnumTiposMensajeAlerta.Error);
                        //            }
                        //        }
                        //        else
                        //        {
                        //            MImprimirMensaje("No Existen Registros.", EnumTiposMensajeAlerta.Error);
                        //        }

                        //    }

                        //}
                        //else 
                        if ((int)tlsOperadores.FocusedNode.GetValue("IntIdActividad") == (int)EnumActividades.TransporteGasDucto)
                        {
                            var lstProtoMovimientos = new List<ObjProtoMovimientoTransaccion>();
                            var lstRegistros =
                            Session[CVariablesSesion.GrillaTransporteDucto] as List<CMovimientoTransporteDucto>;
                            var lstRegistrosDuctos = lstRegistros;

                            long lngNoMovimientos = 1;

                            if (lstRegistrosDuctos != null && lstRegistrosDuctos.Any())
                            {
                                var strFechaMin = lstRegistrosDuctos.Min(x => x.StrFecha);
                                var strFechaMax = lstRegistrosDuctos.Max(x => x.StrFecha);

                                var decFechaIni = Convert.ToDecimal(strFechaMin.Substring(6, 4) + strFechaMin.Substring(3, 2) + strFechaMin.Substring(0, 2));
                                var decFechaFin = Convert.ToDecimal(strFechaMax.Substring(6, 4) + strFechaMax.Substring(3, 2) + strFechaMax.Substring(0, 2));

                                var tareasMovimientos = _gestion.TareasMovimientos(1, 5, Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString()), Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()), Convert.ToDecimal(cmbTipoOperacion.Value), decFechaIni, decFechaFin);
                                if (tareasMovimientos.intCodigo == 1)
                                {
                                    int intControl = 1;
                                    foreach (var registro in lstRegistrosDuctos)
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

                                        if (lstProtoMovimientos.Count == 50)
                                        {
                                            intControl = 0;
                                            MEnviaObjeto(lstProtoMovimientos, (lngNoMovimientos == lstRegistrosDuctos.Count), lngNoMovimientos, decFechaIni, decFechaFin, Convert.ToDecimal(cmbTipoOperacion.Value), 1);

                                            if (lngNoMovimientos == lstRegistrosDuctos.Count)
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
                                        MEnviaObjeto(lstProtoMovimientos, true, lngNoMovimientos - 1, decFechaIni, decFechaFin, Convert.ToDecimal(cmbTipoOperacion.Value), 1);
                                    }
                                    else
                                    {
                                        //todo No implementado.
                                    }
                                }
                                else
                                {
                                    MImprimirMensaje(tareasMovimientos.strMensaje, EnumTiposMensajeAlerta.Error);
                                }
                            }
                            else { MImprimirMensaje("No Existen Registros.", EnumTiposMensajeAlerta.Error); }


                        }
                        else if ((int)tlsOperadores.FocusedNode.GetValue("IntIdActividad") == (int)EnumActividades.DistribucionGasRedes)
                        {
                            var lstProtoMovimientos = new List<ObjProtoMovimientoTransaccion>();
                            var lstRegistros =
                            Session[CVariablesSesion.GrillaRedesGas] as List<CMovimientoRedesGas>;
                            var lstRegistrosRedGas = lstRegistros;

                            long lngNoMovimientos = 1;

                            if (lstRegistrosRedGas != null && lstRegistrosRedGas.Any())
                            {
                                var strFechaMin = lstRegistrosRedGas.Min(x => x.StrFechaFactura);
                                var strFechaMax = lstRegistrosRedGas.Max(x => x.StrFechaFactura);

                                var decFechaIni = Convert.ToDecimal(strFechaMin.Substring(6, 4) + strFechaMin.Substring(3, 2) + strFechaMin.Substring(0, 2));
                                var decFechaFin = Convert.ToDecimal(strFechaMax.Substring(6, 4) + strFechaMax.Substring(3, 2) + strFechaMax.Substring(0, 2));

                                var tareasMovimientos = _gestion.TareasMovimientos(1, 5, Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString()), Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()), Convert.ToDecimal(cmbTipoOperacion.Value), decFechaIni, decFechaFin);
                                if (tareasMovimientos.intCodigo == 1)
                                {
                                    int intControl = 1;
                                    foreach (var registro in lstRegistrosRedGas)
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

                                        if (lstProtoMovimientos.Count == 50)
                                        {
                                            intControl = 0;
                                            MEnviaObjeto(lstProtoMovimientos, (lngNoMovimientos == lstRegistrosRedGas.Count), lngNoMovimientos, decFechaIni, decFechaFin, Convert.ToDecimal(cmbTipoOperacion.Value), 1);

                                            if (lngNoMovimientos == lstRegistrosRedGas.Count)
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
                                        MEnviaObjeto(lstProtoMovimientos, true, lngNoMovimientos - 1, decFechaIni, decFechaFin, Convert.ToDecimal(cmbTipoOperacion.Value), 1);
                                    }
                                    else
                                    {
                                        //todo No implementado.
                                    }
                                }
                                else
                                {
                                    MImprimirMensaje(tareasMovimientos.strMensaje, EnumTiposMensajeAlerta.Error);
                                }
                            }
                            else { MImprimirMensaje("No Existen Registros.", EnumTiposMensajeAlerta.Error); }


                        }
                        else
                        {

                            var lstRegistros = Session[CVariablesSesion.GrillaTransaccion] as List<CRegistrosImportados>;
                            var lstTiOp = lstRegistros.GroupBy(x => new { x.DecIdTipoOp, x.DecOrden })
                           .Select(g => new { g.Key.DecIdTipoOp, g.Key.DecOrden })
                           .OrderBy(d => d.DecOrden).ToList();

                            foreach (var tipo in lstTiOp)
                            {
                                long lngNoMovimientos = 1;
                                var lstReg = lstRegistros.Where(x => x.DecIdTipoOp == tipo.DecIdTipoOp).ToList();

                                if (lstReg != null && lstReg.Any())
                                {
                                    var lstProtoMovimientos = new List<ObjProtoMovimientoTransaccion>();
                                    var strFechaMin = lstReg.Min(x => x.StrFechaEmision);
                                    var strFechaMax = lstReg.Max(x => x.StrFechaEmision);
                                    var decFechaIni = Convert.ToDecimal(strFechaMin.Substring(6, 4) + strFechaMin.Substring(3, 2) + strFechaMin.Substring(0, 2));
                                    var decFechaFin = Convert.ToDecimal(strFechaMax.Substring(6, 4) + strFechaMax.Substring(3, 2) + strFechaMax.Substring(0, 2));

                                    var tareasMovimientos = _gestion.TareasMovimientos(1, 5, Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString()), Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()), tipo.DecIdTipoOp, decFechaIni, decFechaFin);
                                    if (tareasMovimientos.intCodigo == 1)
                                    {
                                        int intControl = 1;
                                        foreach (var registro in lstReg)
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
                                                MEnviaObjeto(lstProtoMovimientos, (lngNoMovimientos == lstReg.Count), lngNoMovimientos, decFechaIni, decFechaFin, tipo.DecIdTipoOp, tipo.DecOrden);

                                                if (lngNoMovimientos == lstReg.Count)
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
                                            MEnviaObjeto(lstProtoMovimientos, true, lngNoMovimientos - 1, decFechaIni, decFechaFin, tipo.DecIdTipoOp, tipo.DecOrden);
                                        }
                                        else
                                        {
                                            //todo No implementado.
                                        }
                                    }
                                    else
                                    {
                                        MImprimirMensaje(tareasMovimientos.strMensaje, EnumTiposMensajeAlerta.Error);
                                    }
                                }
                                else
                                {
                                    MImprimirMensaje("No Existen Registros.", EnumTiposMensajeAlerta.Error);
                                }

                            }




                            //return;
                            //if (lstRegistros != null && lstRegistros.Any())
                            //{
                            //    var lstProtoMovimientos = new List<ObjProtoMovimientoTransaccion>();
                            //    var strFechaMin = lstRegistros.Min(x => x.StrFechaEmision);
                            //    var strFechaMax = lstRegistros.Max(x => x.StrFechaEmision);
                            //    decimal decFechaIni = Convert.ToDecimal(strFechaMin.Substring(6, 4) + strFechaMin.Substring(3, 2) + strFechaMin.Substring(0, 2));
                            //    decimal decFechaFin = Convert.ToDecimal(strFechaMax.Substring(6, 4) + strFechaMax.Substring(3, 2) + strFechaMax.Substring(0, 2));

                            //    var tareasMovimientos = _gestion.TareasMovimientos(1, 5, Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString()), Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()), Convert.ToDecimal(cmbTipoOperacion.Value), decFechaIni, decFechaFin);
                            //    if (tareasMovimientos.intCodigo == 1)
                            //    {
                            //        int intControl = 1;
                            //        foreach (var registro in lstRegistros)
                            //        {
                            //            if (intControl == 0)
                            //            {
                            //                lstProtoMovimientos = new List<ObjProtoMovimientoTransaccion>();
                            //                intControl++;
                            //            }
                            //            else
                            //            {
                            //                //todo No implementado.
                            //            }

                            //            ObjProtoMovimientoTransaccion objProtoReg = registro.objProtoMovimiento;
                            //            lstProtoMovimientos.Add(objProtoReg);

                            //            if (lstProtoMovimientos.Count == 2000)
                            //            {
                            //                intControl = 0;
                            //                MEnviaObjeto(lstProtoMovimientos, (lngNoMovimientos == lstRegistros.Count), lngNoMovimientos, decFechaIni, decFechaFin, Convert.ToDecimal(cmbTipoOperacion.Value));

                            //                if (lngNoMovimientos == lstRegistros.Count)
                            //                {
                            //                    lstProtoMovimientos = new List<ObjProtoMovimientoTransaccion>();
                            //                }
                            //                else
                            //                {
                            //                    //todo No implementado.
                            //                }
                            //            }
                            //            else
                            //            {
                            //                intControl++;
                            //            }
                            //            lngNoMovimientos++;
                            //        }

                            //        if (lstProtoMovimientos.Count > 0)
                            //        {
                            //            MEnviaObjeto(lstProtoMovimientos, true, lngNoMovimientos - 1, decFechaIni, decFechaFin, Convert.ToDecimal(cmbTipoOperacion.Value));
                            //        }
                            //        else
                            //        {
                            //            //todo No implementado.
                            //        }
                            //    }
                            //    else
                            //    {
                            //        MImprimirMensaje(tareasMovimientos.strMensaje,EnumTiposMensajeAlerta.Error);
                            //    }
                            //}
                            //else
                            //{
                            //    MImprimirMensaje("No Existen Registros.", EnumTiposMensajeAlerta.Error);
                            //}
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

        protected void btnDescargar_Click(object sender, EventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    if ((int)tlsOperadores.FocusedNode.GetValue("IntIdActividad") == 0)
                    {
                        MImprimirMensaje("Seleccione una actividad.", EnumTiposMensajeAlerta.Alerta);
                    }
                    else if (cmbTipoOperacion.Value == null)
                    {
                        MImprimirMensaje("Seleccione un tipo de operación.", EnumTiposMensajeAlerta.Alerta);
                    }
                    else
                    {
                        var cod = cmbTipoOperacion.Text.Split(';');
                        var strCodTp = cod[1].Substring(0, 4);
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
                            /*lmqc - importadores GLP*/
                            case "TERG":
                                fileName = "plantilla_glp_recepcion.xlsx";
                                break;
                            case "TEDG":
                                fileName = "plantilla_glp_despacho.xlsx";
                                break;

                            case "IPRC":
                                fileName = "plantilla_importacion_transporte.xlsx";
                                break;
                            //PARA REFINERIAS
                            //case "REFS":
                            case "REFI":
                                fileName = "plantilla_pref_recepcion.xlsx";
                                break;
                            //PARA REDES DE GAS
                            case "DGNR":
                            case "DGNV":
                                fileName = "plantilla_erg_venta.xlsx";
                                break;
                            //PARA TRANSPORTE POR DUCTOS
                            case "TLDE":
                            case "TLDR":
                                fileName = "plantilla_txd.xlsx";
                                break;
                            case "AARC":
                                fileName = "plantilla_ingAzucarero_produccion.xlsx";
                                break;
                            case "AADS":
                                fileName = "plantilla_ingAzucarero_despacho.xlsx";
                                break;
                            case "GCRC":
                                fileName = "plantilla_Graco_recepcion.xlsx";
                                break;
                            case "GPDS":
                                fileName = "plantilla_Graco_despacho.xlsx";
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

        protected void btnVerificaLlave_Click(object sender, EventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    if ((int)tlsOperadores.FocusedNode.GetValue("IntAccion") > 0)
                    {
                        string strMensaje = string.Empty;
                        if (!MValidaCondicionesFuncionamiento(ref strMensaje))
                        {
                            pucDatos.ShowOnPageLoad = true;
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
            else
            {
                //todo No implementado.
            }
        }

        protected void btnSubirLlave_Click(object sender, EventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    var oRegistraLlaveMiddleware = new CRegistroMiddleware
                    {
                        decIdEntidad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdProyecto")),
                        decIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]),
                        decTipoActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad")),
                        strCredencial = CParametrosHydro.strCredencialInfra,
                        strLlaveMiddleware = txtLlaveMiddleware.Text,
                        strProveedorSoftware = ""
                    };

                    MEnviaTraza(1, CAppSettings.svc_RegistraLlaveMiddleware + "." + JsonSerializer.SerializeToString(oRegistraLlaveMiddleware), GetType().Name + "." + MethodBase.GetCurrentMethod().Name);
                    EResultadoI resultado = _clientI.Post<EResultadoI>(CAppSettings.svc_RegistraLlaveMiddleware, oRegistraLlaveMiddleware);
                    if (resultado.decCodigo == 1)
                    {
                        pucDatos.ShowOnPageLoad = false;
                        btnVerificaLlave.Visible = false;
                    }
                    else
                    {
                        MImprimirMensaje(resultado.strMensaje, EnumTiposMensajeAlerta.Alerta);
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

        protected void tlsOperadores_CustomCallback(object sender, DevExpress.Web.ASPxTreeList.TreeListCustomCallbackEventArgs e)
        {
            try
            {
                #region Proceso a Ejecutar

                if (txtBuscador.Text.Trim().Length > 0)
                {
                    var lstArbol = Session[CVariablesSesion.EstructuraArbol] as List<CArbol>;
                    tlsOperadores.CollapseAll();
                    var lstCoinsidencia = lstArbol.Where(x => x.StrDescripcion.ToLower().Contains(txtBuscador.Text.Trim().ToLower())).OrderByDescending(x => x.IntId).ToList();
                    foreach (var reg in lstCoinsidencia)
                    {
                        Session[CVariablesSesion.EntidadSeleccionada] = reg.StrUbicacion;
                        var vecSel = reg.StrUbicacion.Split('-');
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

        protected void btnLimpiaTo_Click(object sender, EventArgs e)
        {
            try
            {
                #region Proceso a Ejecutar
                var decFechaIni = Convert.ToDecimal(cmbPeriodo.Value + "01");
                var decFechaFin = Convert.ToDecimal(cmbPeriodo.Value + DateTime.DaysInMonth(Convert.ToInt16(cmbPeriodo.Value.ToString().Substring(0, 4)), Convert.ToInt16(cmbPeriodo.Value.ToString().Substring(4, 2))).ToString().PadLeft(2, '0'));
                var tareasMovimientos = _gestion.TareasMovimientos(1, 5, Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString()), Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()), Convert.ToDecimal(cmbTipoOperacion.Value), decFechaIni, decFechaFin);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError", "LoadingPanel.Hide();", true);

                if (tareasMovimientos.intCodigo != 1)
                {
                    MImprimirMensaje(tareasMovimientos.strMensaje, EnumTiposMensajeAlerta.Error);
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
                                                                      "/WFImportacionDatos.aspx?format=json");
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
        /// METODO. ConfigurarFormulario
        /// </summary>
        private void MConfigurarFormulario()
        {
            btnDescargar.Visible = Convert.ToInt16(Session[CVariablesSesion.PermisoCons]) != 0;
            btnGrabar.Visible = Convert.ToInt16(Session[CVariablesSesion.PermisoAlta]) != 0;
            btnLimpiaTo.Visible = Convert.ToInt16(Session[CVariablesSesion.PermisoBaja]) != 0;
        }

        /// <summary>
        /// METODO. MLimpiaGrilla
        /// </summary>
        private void MLimpiaGrilla()
        {
            Session.Remove(CVariablesSesion.GrillaTransaccion);
            Session.Remove(CVariablesSesion.GrillaRegObservado);
            btnImportar.Enabled = false;
            btnLimpiaTo.Enabled = false;
            btnGrabar.Enabled = false;
            pnlMensaje.Visible = false;
            dgvTransacciones.DataSource = null;
            dgvTransacciones.DataBind();
            dgvTransaccionError.DataSource = null;
            dgvTransaccionError.DataBind();
            lblTipoError.Text = "";
        }

        /// <summary>
        /// METODO. MMuestraCabecera
        /// </summary>
        private void MMuestraCabecera()
        {
            lblRazonSocial.Text = tlsOperadores.FocusedNode.GetValue("StrDescPadre").ToString();
            lblNit.Text = tlsOperadores.FocusedNode.GetValue("LngNit").ToString();
            lblDireccion.Text = (tlsOperadores.FocusedNode.GetValue("StrDireccion") == null) ? "No Definido" : tlsOperadores.FocusedNode.GetValue("StrDireccion").ToString();
        }

        /// <summary>
        /// METODO. MCargaParametricas
        /// </summary>
        private void MCargaParametricas()
        {
            var objParametricas = new O_PARAMETRICAS_GRAL();
            //PAISES
            var objUbicacion = _client.Post<EResultadoUbicacion>(CAppSettings.svc_obtiene_ubicacion,
            new
            {
                strCredencial = CParametrosHydro.strCredencialFuncionario,
                decNivel = 1,
                decIdPadre = 0
            });
            if (objUbicacion.IntCodigo == 1)
            {
                objParametricas.lstPais = objUbicacion.OResultado;
            }
            Session[CVariablesSesion.ListaParametrica] = objParametricas;

            //PARAM GLOBAL CLASIFICACION 1
            var lstObtCatalogo = _client.Post<EResultadoParametricaGlobal>(CAppSettings.svc_lista_parametrica_global,
            new
            {
                decIdDominio = 11
            });
            if (lstObtCatalogo.IntCodigo == 1)
            {
                Session[CVariablesSesion.ListaCatalogo] = lstObtCatalogo.OResultado;
            }
            else
            {
                //todo No implementado.
            }

            //PARAM LISTA POI RECEPCION
            var lstObtPoiRecepcion = _client.Post<EResultadoParametricaGlobal>(CAppSettings.svc_lista_parametrica_global,
            new
            {
                decIdDominio = 92 //desarrollo = 53, test = 92 produccion = 34
            });
            if (lstObtPoiRecepcion.IntCodigo == 1)
            {
                Session[CVariablesSesion.ListaPoiRecepcion] = lstObtPoiRecepcion.OResultado;
            }
            else
            {
                //todo No implementado.
            }

            //PARAM LISTA POI ENTREGA
            var lstObtPoiEntrega = _client.Post<EResultadoParametricaGlobal>(CAppSettings.svc_lista_parametrica_global,
            new
            {
                decIdDominio = 93 //desarrollo = 54, test = 93 produccion = 35
            });
            if (lstObtPoiEntrega.IntCodigo == 1)
            {
                Session[CVariablesSesion.ListaPoiEntrega] = lstObtPoiEntrega.OResultado;
            }
            else
            {
                //todo No implementado.
            }

            //PARAM LISTA SISTEMA DUCTO
            var lstObtSistemaDucto = _client.Post<EResultadoParametricaGlobal>(CAppSettings.svc_lista_parametrica_global,
            new
            {
                decIdDominio = 94 //desarrollo = 55, test = 94 produccion 33
            });
            if (lstObtSistemaDucto.IntCodigo == 1)
            {
                Session[CVariablesSesion.ListaSistemaDucto] = lstObtSistemaDucto.OResultado;
            }
            else
            {
                //todo No implementado.
            }

            //PARAM LISTA TIPO DE TRANSPORTE DUCTO
            var lstObtTranspDucto = _client.Post<EResultadoParametricaGlobal>(CAppSettings.svc_lista_parametrica_global,
            new
            {
                decIdDominio = 95 //desarrollo = 56, test = 95 produccion = 31
            });
            if (lstObtTranspDucto.IntCodigo == 1)
            {
                Session[CVariablesSesion.ListaTipoTransporte] = lstObtTranspDucto.OResultado;
            }
            else
            {
                //todo No implementado.
            }

            //PARAM LISTA TIPO DE CONSUMIDOR
            var lstObtTipoConsumidor = _client.Post<EResultadoParametricaGlobal>(CAppSettings.svc_lista_parametrica_global,
            new
            {
                decIdDominio = 12 
            });
            if (lstObtTipoConsumidor.IntCodigo == 1)
            {
                Session[CVariablesSesion.ListaTipoConsumidor] = lstObtTipoConsumidor.OResultado;
            }
            else
            {
                //todo No implementado.
            }

            //TIPOS DE CONTENEDORES
            var objParamInfra = new O_PARAMETROS_INFRA();
            var objContenedor = _client.Post<EResultadoTipoContenedor>(CAppSettings.svc_lista_tipos_contenedores,
            new
            {
                strCredencial = CParametrosHydro.strCredencialFuncionario,
                decFiltro = 1
            });
            if (objContenedor.intCodigo == 1)
            {
                objParamInfra.TIPOS_CONTENEDORES = objContenedor.oResultado;
            }

            //TIPO INFRAESTRUCTURA
            var objParamInfraestrutura = _clientI.Get<EResultadoParamInfra>(CAppSettings.svc_LListadoParametros + "/" + CParametrosHydro.strCredencialInfra + "/DESC/TIPO_INFRAESTRUCTURA?format=json");
            if (objParamInfraestrutura.decCodigo == 1 && objParamInfraestrutura.oResultado != null && objParamInfraestrutura.oResultado.Any())
            {
                objParamInfra.TIPO_INFRAESTRUCTURA = objParamInfraestrutura.oResultado;
            }
            Session[CVariablesSesion.ListaParamInfra] = objParamInfra;
        }

        /// <summary>
        /// METODO. MCargaInfraestructura
        /// </summary>
        private void MCargaInfraestructura()
        {
            //INFRAESTRUCTURA
            Session.Remove(CVariablesSesion.ListaInfraestructura);
            var intTipoInfra = 0;
            var intIdEntidad = Convert.ToInt32(tlsOperadores.FocusedNode.GetValue("IntIdProyecto").ToString());
            var intIdActividad = Convert.ToInt32(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());

            //borrar solo para salvar
            if (intIdActividad == 50)
            {
                intIdEntidad = 0;
            }
            var objInfraestructura = _clientI.Get<EResultadoInfraestructura>(CAppSettings.svc_ListadoInfraestructuraVol + "/" + CParametrosHydro.strCredencialInfra + "/" + intIdEntidad + "/" + intIdActividad + "/" + intTipoInfra + "?format=json");
            if (objInfraestructura.decCodigo == 1 && objInfraestructura.oResultado != null && objInfraestructura.oResultado.Any())
            {
                Session[CVariablesSesion.ListaInfraestructura] = objInfraestructura.oResultado;
            }



            //var strActividades = (string)Session[CVariablesSesion.IdEntidadRevision];
            //if(Session[CVariablesSesion.IdEntidadRevision]==null)
            //    return;

            //var objEntidadRel = _consulta.ObtenerEntidades(
            //    new EObjetoEnvio5ParametrosProto
            //    {
            //        decFiltro = 2,
            //        strParametro1 = "",
            //        strParametro2 = (string)Session[CVariablesSesion.IdEntidadRevision],
            //        strParametro3 = CAppSettings.idAplicacion,
            //        strParametro4 = "",
            //        strParametro5 = ""
            //    });
            //if (objEntidadRel.intCodigo == 1)
            //{
            //    var lstEntidadRel = (List<O_OBTIENE_ENTIDADES_CTY>)objEntidadRel.oResultado;


            //    var objEntidades = _client.Post<EResultadoConfigHidrocarburo>(CAppSettings.svc_listar_autorizacion_condicion,
            //    new
            //    {
            //        strCredencial = CParametrosHydro.strCredencialFuncionario,
            //        decFiltro = 3,
            //        strParametro1 = Convert.ToInt32(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString()),
            //        strParametro2 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
            //        strParametro3 = ""
            //    });
            //    if (objEntidades.IntCodigo == 1)
            //    {

            //        var lstAlias = objEntidades.OResultado.Where(x => x.DESCRIPCION.Trim().Length > 0).ToList();
            //        if (lstAlias.Any())
            //        {
            //            foreach (var reg in lstAlias)
            //            {
            //                //lstEntidadRel.ForEach(x => x.ID_INFRA = Convert.ToDecimal(x.ID_TIPO.ToString().Replace("-", "") + x.ID_INFRAESTRUCTURA.ToString().Replace("-", "")));
            //                lstEntidadRel.Where(w => w.ID_ACTIVIDAD == reg.ID_SUB_IDENTIFICADOR && w.ID_CONSUMIDOR == reg.ID_IDENTIFICADOR).ToList().ForEach(f => f.TIPO_SOCIEDAD_PADRE = reg.DESCRIPCION);
            //            }
            //        }
            //    }
            //    Session[CVariablesSesion.ListaEntidad] = lstEntidadRel;
            //}


            ////ENTIDADES
            //var objEntidades1 = _client.Post<EResultadoConfigHidrocarburo>(CAppSettings.svc_listar_autorizacion_condicion,
            //    new
            //    {
            //        strCredencial = CParametrosHydro.strCredencialFuncionario,
            //        decFiltro = 3,
            //        strParametro1 = Convert.ToInt32(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString()),
            //        strParametro2 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
            //        strParametro3 = ""
            //    });
            //if (objEntidades.IntCodigo == 1)
            //{
            //    Session[CVariablesSesion.ListaEntidad] = objEntidades.OResultado;
            //}
        }

        /// <summary>
        /// METODO. MValidaDatosIniciales
        /// </summary>
        /// <param name="strMensaje"></param>
        /// <returns></returns>
        private bool MValidaDatosIniciales(ref string strMensaje)
        {
            bool booResultado = true;

            if (cmbTipoOperacion.Text == "")
            {
                strMensaje = "Introduzca el tipo de operación.";
                booResultado = false;
            }
            else if (cmbPeriodo.Text == "")
            {
                strMensaje = "Introduzca un periodo valido.";
                booResultado = false;
            }
            return booResultado;
        }

        /// <summary>
        /// METODO. MValidaCondicionesFuncionamiento
        /// </summary>
        /// <param name="strMesnaje"></param>
        /// <returns></returns>
        private bool MValidaCondicionesFuncionamiento(ref string strMesnaje)
        {
            bool booRespuesta = true;
            //Valida si exixte DDJJ en periodo
            var objValidacion = _client.Post<EResultadoGenerico>(CAppSettings.svc_validaciones_general,
            new
            {
                strCredencial = CParametrosHydro.strCredencialFuncionario,
                strFiltro = "1",
                strParametro1 = tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString(),
                strParametro2 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
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
                        strParametro1 = tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString(),
                        strParametro2 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
                        strParametro3 = "",
                        strParametro4 = ""
                    });
                    if (objTipoOperacion.IntCodigo == 1)
                    {
                        objTipoOperacion.OResultado = objTipoOperacion.OResultado.Where(x => x.ID_TIPO_OPERACION == Convert.ToDecimal(cmbTipoOperacion.Value)).ToList();
                        if (objTipoOperacion.OResultado.Any())
                        {
                            if (objTipoOperacion.OResultado.FirstOrDefault().BINARIO_ESTADO.Substring(0, 1) == "0")
                            {
                                booRespuesta = false;
                                strMesnaje = "Función no permitida para el tipo de operación seleccionado.";
                            }
                            else
                            {
                                Session[CVariablesSesion.ListaParamGral] = null;
                                var objParametricas = new O_PARAMETRICAS_ACT();

                                //Carga productos y unidades de medida
                                var objHidrocarburo = _client.Post<EResultadoConfigHidrocarburo>(CAppSettings.svc_listar_autorizacion_condicion,
                                new
                                {
                                    strCredencial = CParametrosHydro.strCredencialFuncionario,
                                    decFiltro = 1,
                                    strParametro1 = tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString(),
                                    strParametro2 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
                                    strParametro3 = ""
                                });
                                if (objHidrocarburo.IntCodigo == 1)
                                {
                                    objParametricas.lstProducto = objHidrocarburo.OResultado;
                                }
                                else
                                {
                                    booRespuesta = false;
                                    strMesnaje = "U.M. " + objHidrocarburo.StrMensaje;
                                }

                                //Carga cuentas
                                var objCuentas = _client.Post<EResultadoConfigHidrocarburo>(CAppSettings.svc_listar_autorizacion_condicion,
                                new
                                {
                                    strCredencial = CParametrosHydro.strCredencialFuncionario,
                                    decFiltro = 6,
                                    strParametro1 = "",
                                    strParametro2 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
                                    strParametro3 = ""
                                });
                                if (objCuentas.OResultado != null)
                                {
                                    objParametricas.lstCuentas = objCuentas.OResultado;
                                }
                                //else
                                //{
                                //    booRespuesta = false;
                                //    strMesnaje = objCuentas.StrMensaje;
                                //}

                                Session[CVariablesSesion.ListaParamGral] = objParametricas;
                            }
                        }
                        else
                        {
                            booRespuesta = false;
                            strMesnaje = "Inconsistencia de datos. Tipo de operacion no encontrada.";
                        }
                    }
                    else
                    {
                        booRespuesta = false;
                        strMesnaje = "T.O. " + objTipoOperacion.StrMensaje;
                    }
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

        /// <summary>
        /// METODO. MCargarDatos
        /// </summary>
        /// <param name="strRuta"></param>
        private void MCargarDatos(string strRuta)
        {
            dgvTransaccionRefineria.Visible = false;
            dgvTransacciones.Visible = true;

            switch ((int)tlsOperadores.FocusedNode.GetValue("IntIdActividad"))
            {
                case (int)EnumActividades.EstacionServicioLiquido:
                case (int)EnumActividades.EstacionServicioGnv:
                case (int)EnumActividades.PuestosVentaLiquido:
                    MCargaEess(strRuta);
                    break;
                case (int)EnumActividades.TerminalAlmacenajeLiq:
                    MCargaPahl(strRuta);
                    break;
                case (int)EnumActividades.TerminalAeropuertoLiq:
                    MCargaPscaa(strRuta);
                    break;
                case (int)EnumActividades.FueraAla:
                    MCargaFueraAla(strRuta);
                    break;
                case (int)EnumActividades.IngenioAzucarero:
                    MCargaIngenioAzucarero(strRuta);
                    break;
                case (int)EnumActividades.DistribucionGlpGarrafas://
                    MCargaGlp(strRuta);
                    break;
                case (int)EnumActividades.Refinerias://
                    //dgvTransacciones.Visible = false;
                    MCargarRefinacion(strRuta);
                    break;

                case (int)EnumActividades.TransporteLiquidosDucto://
                case (int)EnumActividades.TransporteGasDucto://
                    dgvTransacciones.Visible = false;
                    dgvTransaccionRefineria.Visible = false;
                    MCargarTransporteDuctos(strRuta);
                    break;

                case (int)EnumActividades.DistribucionGasRedes://
                    dgvTransacciones.Visible = false;
                    dgvTransaccionRefineria.Visible = false;
                    MCargarRedesGas(strRuta);
                    break;
                
                case (int)EnumActividades.Graco://
                    dgvTransacciones.Visible = false;
                    dgvTransaccionRefineria.Visible = false;
                    MCargaGraco(strRuta);
                    break;

                default:
                    MImprimirMensaje("Actividad invalida.", EnumTiposMensajeAlerta.Alerta);
                    return;
            }

            if (dgvTransaccionError.VisibleRowCount == 0)
            {
                btnGrabar.Enabled = true;
            }
        }
        /// <summary>
        /// METODO. MValidaLineaEess
        /// </summary>
        /// <param name="values"></param>
        /// <param name="strMensaje"></param>
        /// <returns></returns>
        private bool MValidaLineaRefinerias_99999(string[] values, ref string strMensaje)
        {
            bool strResultado = true;
            try
            {
                //if (cmbTipoOperacion.Text.Contains("DS"))
                //{
                #region Despachos
                var arrayNumero = new[] { 9, 12, 13, 14, 16, 19, 20, 22, 23, 26 };
                if (!cmbTipoOperacion.Text.Contains("RECEP"))
                {
                    if (values.Length != 24)
                    {
                        strMensaje = "- Nro. de columnas del registro invalido.";
                    }
                    else
                    {
                        arrayNumero = new[] { 1, 11, 12, 14, 21, 22, 23 };
                    }
                }

                //var arrayNumero = new[] { 5, 8, 12, 14, 15, 17, 18, 20, 21, 23, 24, 25, 27, 28, 32, 34, 35};
                decimal dec;
                for (int i = 0; i < arrayNumero.Length; i++)
                {
                    if (!decimal.TryParse(values[arrayNumero[i]].Trim(), out dec))
                    {
                        strMensaje += "- Columna Nro. " + arrayNumero[i] + " No es un número.";
                    }
                }
                if (values[0].Trim().Substring(6, 4) + values[0].Trim().Substring(3, 2) != cmbPeriodo.Value.ToString().Substring(0, 4) + cmbPeriodo.Value.ToString().Substring(4, 2))
                {
                    strMensaje += "- Periodo incorrecto.";
                }
                DateTime dt;
                if (!DateTime.TryParse(values[0].Trim(), out dt))
                {
                    strMensaje += "- Fecha incorrecta.";
                }
                if (MCodificaParametrica((int)EnumTipoParametrica.Producto, values[20].Trim()) == -99999)
                {
                    strMensaje += "- Producto incorrecto.";
                }
                //if (MCodificaParametrica((int)EnumTipoParametrica.UnidadMedida, values[7].Trim()) == -99999)
                //{
                //    strMensaje += "- Unidad de medida invalida.";
                //}
                //if ((values[9].Trim() == "V" && Convert.ToDecimal(values[8]) <= 0) || (values[9].Trim() == "A" && Convert.ToDecimal(values[8]) < 0))
                //{
                //    strMensaje += "- Cantidad incorrecta.";
                //}
                //if (Convert.ToDecimal(values[18].Trim()) > (Convert.ToDecimal(values[17].Trim()) * Convert.ToDecimal(values[8].Trim())) + 2 ||
                //    Convert.ToDecimal(values[18].Trim()) < (Convert.ToDecimal(values[17].Trim()) * Convert.ToDecimal(values[8].Trim())) - 2)
                //{
                //    strMensaje += "- Importe incorrecto.";
                //}
                //if (values[16].Trim().Length == 1 && values[16].Trim() != "0")
                //{
                //    strMensaje += "- Código de control incorrecto.";
                //}
                //if (values[16].Trim().Length > 1 && values[16].Trim().Length < 6)
                //{
                //    strMensaje += "- Código de control incorrecto.";
                //}
                //if (values[3].Trim().Length > 1)
                //{
                //    if (values[3].Trim().ContainsAny("-", "@", "/", "'", "\\", ".", ",", "_", "+", "[", "]", "{", "}", " "))
                //    {
                //        strMensaje += "- Identificador contiene caracteres invalidos.";
                //    }
                //}
                //if (Convert.ToInt64(values[24]) > 0)
                //{
                //    if (Convert.ToDecimal(values[28].Trim()) > (Convert.ToDecimal(values[27].Trim()) * Convert.ToDecimal(values[8].Trim())) + 2 ||
                //        Convert.ToDecimal(values[28].Trim()) < (Convert.ToDecimal(values[27].Trim()) * Convert.ToDecimal(values[8].Trim())) - 2)
                //    {
                //        strMensaje += "- Importe factura externa incorrecto.";
                //    }
                //    if (values[6].Trim() == "BOLIVIA")
                //    {
                //        strMensaje += "- País incorrecto.";
                //    }
                //    if (values[26].Trim().Length == 1 && values[26].Trim() != "0")
                //    {
                //        strMensaje += "- Código de control factura externa incorrecto.";
                //    }
                //    if (values[26].Trim().Length > 1 && values[26].Trim().Length < 6)
                //    {
                //        strMensaje += "- Código de control factura externa incorrecto.";
                //    }
                //}
                //if (Convert.ToDecimal(values[21]) > 0)
                //{
                //    if (Convert.ToDecimal(values[21].Trim()) > (Convert.ToDecimal(values[20].Trim()) * Convert.ToDecimal(values[8].Trim())) + 2 ||
                //        Convert.ToDecimal(values[21].Trim()) < (Convert.ToDecimal(values[20].Trim()) * Convert.ToDecimal(values[8].Trim())) - 2)
                //    {
                //        strMensaje += "- Importe vale incorrecto.";
                //    }
                //}
                //if (values[11].Trim() == "VAL")
                //{
                //    if (values[19].Trim() == "")
                //    {
                //        strMensaje += "- Número de vale incorrecto.";
                //    }
                //}
                //if (values[30].Trim() != "NOID" && values[30].Trim() != "SUIC" && values[30].Trim() != "DS2243" && values[30].Trim() != "SICEVE" && values[30].Trim() != "DS3578")
                //{
                //    strMensaje += "- Servicio adicional incorrecto.";
                //}
                //if (values[30].Trim() != "NOID" && values[31].Trim() == "")
                //{
                //    strMensaje += "- Número de referencia del servicio adicional invalido.";
                //}
                //if (values[33].Trim() != "NOID" && values[33].Trim() != "SC" && values[33].Trim() != "CC")
                //{
                //    strMensaje += "- Tipo de autorización de venta incorrecto.";
                //}
                //if (values[11].Trim() != "EF" && values[11].Trim() != "PRE" && values[11].Trim() != "VAL")
                //{
                //    strMensaje += "- Forma de pago incorrecta.";
                //}
                //if (Convert.ToInt64(values[34]) > 0 && values[33].Trim() == "NOID")
                //{
                //    strMensaje += "- Autorización de venta invalida.";
                //}
                //if (values[2].Trim() != "NOID")
                //{
                //    var lstInfraestructura = Session[CVariablesSesion.ListaInfraestructura] as List<O_INFRAESTRUCTURA_VOL_CTY>;
                //    if (lstInfraestructura != null && lstInfraestructura.Any())
                //    {
                //        lstInfraestructura = lstInfraestructura.Where(x => x.CODIGO_OPERADOR == values[2].Trim() && x.TIPO == values[1].Trim()).ToList();
                //        if (lstInfraestructura.Any())
                //        {
                //            if (lstInfraestructura.FirstOrDefault().ID_PRODUCTO != MCodificaParametrica((int)EnumTipoParametrica.Producto, values[7].Trim()))
                //            {
                //                strMensaje += "- El producto no corresponde a la infraestructura receptora especificada.";
                //            }
                //        }
                //        else
                //        {
                //            strMensaje += "- Infraestructura despachante invalida.";
                //        }
                //    }
                //    else
                //    {
                //        strMensaje += "- Infraestructura despachante no registrada.";
                //    }
                //}
                //if (Convert.ToDecimal(values[14]) == 0 && values[19].Trim().Length == 0 && values[29].Trim().Length == 0)
                //{
                //    strMensaje += "- No existe documento de respaldo.";
                //}
                #endregion
                //}
                //else
                //{
                //    #region Recepciones
                //    //if (values.Length != 30)
                //    //{
                //    //    strMensaje = "- Nro. de columnas del registro invalido.";
                //    //}
                //    //var arrayNumero = new[] { 6, 10, 13, 16, 17, 19, 20, 23, 24, 27, 29 };
                //    //decimal dec;
                //    //for (int i = 0; i < arrayNumero.Length; i++)
                //    //{
                //    //    if (!decimal.TryParse(values[arrayNumero[i]].Trim(), out dec))
                //    //    {
                //    //        strMensaje += "- Columna Nro. " + arrayNumero[i] + " No es un número.";
                //    //    }
                //    //}
                //    //if (values[0].Trim().Substring(6, 4) + values[0].Trim().Substring(3, 2) != cmbPeriodo.Value.ToString().Substring(0, 4) + cmbPeriodo.Value.ToString().Substring(4, 2))
                //    //{
                //    //    strMensaje += "- Periodo incorrecto.";
                //    //}
                //    //DateTime dt;
                //    //if (!DateTime.TryParse(values[0].Trim(), out dt))
                //    //{
                //    //    strMensaje += "- Fecha incorrecta.";
                //    //}
                //    //if (MCodificaParametrica((int)EnumTipoParametrica.Producto, values[9].Trim()) == -99999)
                //    //{
                //    //    strMensaje += "- Producto incorrecto.";
                //    //}
                //    //if (MCodificaParametrica((int)EnumTipoParametrica.UnidadMedida, values[9].Trim()) == -99999)
                //    //{
                //    //    strMensaje += "- Unidad de medida invalida.";
                //    //}
                //    //if (Convert.ToDecimal(values[10]) <= 0)
                //    //{
                //    //    strMensaje += "- Cantidad incorrecto.";
                //    //}
                //    ////La factura puede contener otros items
                //    ////if (Convert.ToDecimal(values[20].Trim()) > (Convert.ToDecimal(values[19].Trim()) * Convert.ToDecimal(values[10].Trim())) + 2 ||
                //    ////    Convert.ToDecimal(values[20].Trim()) < (Convert.ToDecimal(values[19].Trim()) * Convert.ToDecimal(values[10].Trim())) - 2)
                //    ////{
                //    ////    strMensaje += "- Importe incorrecto.";
                //    ////}
                //    //if (values[5].Trim().Length > 1)
                //    //{
                //    //    if (values[5].Trim().ContainsAny("-", "@", "/", "'", "\\", ".", ",", "_", "+", "[", "]", "{", "}", " "))
                //    //    {
                //    //        strMensaje += "- Identificador contiene caracteres invalidos.";
                //    //    }
                //    //}
                //    //if (values[1].Trim() != "NOID")
                //    //{
                //    //    if (MCodificaParametrica((int)EnumTipoParametrica.Entidad, values[1].Trim()) == -99999)
                //    //    {
                //    //        strMensaje += "- Entidad origen invalida.";
                //    //    }
                //    //}
                //    //if (values[3].Trim() != "NOID")
                //    //{
                //    //    var lstInfraestructura = Session[CVariablesSesion.ListaInfraestructura] as List<O_INFRAESTRUCTURA_VOL_CTY>;
                //    //    if (lstInfraestructura != null && lstInfraestructura.Any())
                //    //    {
                //    //        lstInfraestructura = lstInfraestructura.Where(x => x.CODIGO_OPERADOR == values[3].Trim() && x.TIPO == values[2].Trim()).ToList();
                //    //        if (lstInfraestructura.Any())
                //    //        {
                //    //            if (lstInfraestructura.FirstOrDefault().ID_PRODUCTO != MCodificaParametrica((int)EnumTipoParametrica.Producto, values[9].Trim()))
                //    //            {
                //    //                strMensaje += "- El producto no corresponde a la infraestructura receptora especificada.";
                //    //            }
                //    //        }
                //    //        else
                //    //        {
                //    //            strMensaje += "- Infraestructura despachante invalida.";
                //    //        }
                //    //    }
                //    //    else
                //    //    {
                //    //        strMensaje += "- Infraestructura despachante no registrada.";
                //    //    }
                //    //}
                //    //if (Convert.ToDecimal(values[16]) == 0 && values[21].Trim().Length == 0 && values[26].Trim().Length == 0)
                //    //{
                //    //    strMensaje += "- No existe documento de respaldo.";
                //    //}
                //    #endregion
                //}
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
        private bool MValidaLineaRefinerias(string[] values, ref string strMensaje)
        {
            var booResultado = true;
            try
            {
                var to = cmbTipoOperacion.Text.Split(';');
                var strTipoOp = to[1].Substring(2, 2);
                int[] arrayNumero;
                decimal dec;
                DateTime dt;
                switch (strTipoOp)
                {
                    case "RC":
                        #region Recepciones
                        if (values.Length != 25)
                        {
                            strMensaje = "- Nro. de columnas del registro invalido.";
                        }
                        arrayNumero = new[] { 9, 12, 14, 17, 18, 20, 21, 24 };
                        for (int i = 0; i < arrayNumero.Length; i++)
                        {
                            if (!decimal.TryParse(values[arrayNumero[i]].Trim(), out dec))
                            {
                                strMensaje += "- Columna Nro. " + arrayNumero[i] + " No es un número.";
                            }
                        }
                        if (values[0].Trim().Substring(6, 4) + values[0].Trim().Substring(3, 2) != cmbPeriodo.Value.ToString().Substring(0, 4) + cmbPeriodo.Value.ToString().Substring(4, 2))
                        {
                            strMensaje += "- Periodo incorrecto.";
                        }
                        if (!DateTime.TryParse(values[0].Trim(), out dt))
                        {
                            strMensaje += "- Fecha incorrecta.";
                        }
                        if (Convert.ToDecimal(values[12]) <= 0)
                        {
                            strMensaje += "- Cantidad incorrecta.";
                        }
                        //la factura puede contener otros items
                        //if (Convert.ToDecimal(values[28].Trim()) > (Convert.ToDecimal(values[26].Trim()) * Convert.ToDecimal(values[27].Trim())) + 2 ||
                        //    Convert.ToDecimal(values[28].Trim()) < (Convert.ToDecimal(values[26].Trim()) * Convert.ToDecimal(values[27].Trim())) - 2)
                        //{
                        //    strMensaje += "- Importe incorrecto.";
                        //}
                        if (MCodificaParametrica((int)EnumTipoParametrica.Producto, values[11].Trim()) == -99999)
                        {
                            strMensaje += "- Producto incorrecto.";
                        }
                        //if (MCodificaParametrica((int)EnumTipoParametrica.UnidadMedida, values[25].Trim()) == -99999)
                        //{
                        //    strMensaje += "- Unidad de medida invalida.";
                        //}
                        if (values[19].Trim().Length == 1 && values[19].Trim() != "0")
                        {
                            strMensaje += "- Código de control incorrecto.";
                        }
                        if (values[19].Trim().Length > 1 && values[19].Trim().Length < 6)
                        {
                            strMensaje += "- Código de control incorrecto.";
                        }
                        //if (MCodificaParametrica((int)EnumTipoParametrica.Pais, values[7].Trim()) == -99999)
                        //{
                        //    strMensaje += "- País invalido.";
                        //}
                        if (values[7].Trim().Length > 1)
                        {
                            if (values[7].Trim().ContainsAny("-", "@", "/", "'", "\\", ".", ",", "_", "+", "[", "]", "{", "}", " "))
                            {
                                strMensaje += "- Identificador contiene caracteres invalidos.";
                            }
                        }
                        if (values[3].Trim() != "NOID")
                        {
                            var lstInfraestructura = Session[CVariablesSesion.ListaInfraestructura] as List<O_INFRAESTRUCTURA_VOL_CTY>;
                            if (lstInfraestructura != null && lstInfraestructura.Any())
                            {
                                lstInfraestructura = lstInfraestructura.Where(x => x.CODIGO_OPERADOR.Trim() == values[3].Trim() && x.TIPO.Trim() == values[2].Trim()).ToList();
                                if (lstInfraestructura.Any())
                                {
                                    if (lstInfraestructura.FirstOrDefault().ID_PRODUCTO != MCodificaParametrica((int)EnumTipoParametrica.Producto, values[11].Trim()))
                                    {
                                        strMensaje += "- El producto no corresponde a la infraestructura receptora especificada.";
                                    }
                                }
                                else
                                {
                                    strMensaje += "- Infraestructura receptora invalida.";
                                }
                            }
                            else
                            {
                                strMensaje += "- Infraestructura receptora no registrada.";
                            }
                        }
                        if (values[5].Trim() != "NOID")
                        {
                            var lstInfraestructura = Session[CVariablesSesion.ListaInfraestructura] as List<O_INFRAESTRUCTURA_VOL_CTY>;
                            if (lstInfraestructura != null && lstInfraestructura.Any())
                            {
                                lstInfraestructura = lstInfraestructura.Where(x => x.CODIGO_OPERADOR == values[5].Trim() && x.TIPO == values[4].Trim()).ToList();
                                if (lstInfraestructura.Any())
                                {
                                    if (lstInfraestructura.FirstOrDefault().ID_PRODUCTO != MCodificaParametrica((int)EnumTipoParametrica.Producto, values[11].Trim()))
                                    {
                                        strMensaje += "- El producto no corresponde a la infraestructura origen especificada.";
                                    }
                                }
                                else
                                {
                                    strMensaje += "- Infraestructura origen invalida.";
                                }
                            }
                            else
                            {
                                strMensaje += "- Infraestructura origen no definida.";
                            }
                        }
                        if (values[5].Trim() != "NOID" && values[4].Trim() == "NOID")
                        {
                            if (MCodificaParametrica((int)EnumTipoParametrica.TipoInfraestructura, values[5].Trim()) == -99999)
                            {
                                strMensaje += "- Tipo de infraestructura origen invalida.";
                            }
                        }
                        if (values[1].Trim() != "NOID")
                        {
                            if (MCodificaParametrica((int)EnumTipoParametrica.Entidad, values[1].Trim()) == -99999)
                            {
                                strMensaje += "- Entidad origen invalida.";
                            }
                        }
                        if (Convert.ToDecimal(values[17]) == 0 && values[22].Trim().Length == 0 && values[20].Trim().Length == 0)
                        {
                            strMensaje += "- No existe documento de respaldo.";
                        }
                        #endregion
                        break;
                    default:
                        #region Despachos
                        if (values.Length != 25)
                        {
                            strMensaje = "- Nro. de columnas del registro invalido.";
                        }
                        arrayNumero = new[] { 9, 12, 14, 17, 18, 20, 21, 22, 24 };
                        for (int i = 0; i < arrayNumero.Length; i++)
                        {
                            if (!decimal.TryParse(values[arrayNumero[i]].Trim(), out dec))
                            {
                                strMensaje += "- Columna Nro. " + arrayNumero[i] + " No es un número.";
                            }
                        }
                        if (values[0].Trim().Substring(6, 4) + values[0].Trim().Substring(3, 2) != cmbPeriodo.Value.ToString().Substring(0, 4) + cmbPeriodo.Value.ToString().Substring(4, 2))
                        {
                            strMensaje += "- Periodo incorrecto.";
                        }
                        if (!DateTime.TryParse(values[0].Trim(), out dt))
                        {
                            strMensaje += "- Fecha incorrecta.";
                        }
                        if ((values[13].Trim() == "V" && Convert.ToDecimal(values[12]) <= 0) || (values[13].Trim() == "A" && Convert.ToDecimal(values[12]) < 0))
                        {
                            strMensaje += "- Cantidad incorrecta.";
                        }
                        if (Convert.ToDecimal(values[21].Trim()) > (Convert.ToDecimal(values[12].Trim()) * Convert.ToDecimal(values[20].Trim())) + 2 ||
                            Convert.ToDecimal(values[21].Trim()) < (Convert.ToDecimal(values[12].Trim()) * Convert.ToDecimal(values[20].Trim())) - 2)
                        {
                            strMensaje += "- Importe incorrecto.";
                        }
                        if (values[19].Trim().Length == 1 && values[19].Trim() != "0")
                        {
                            strMensaje += "- Código de control incorrecto.";
                        }
                        if (values[19].Trim().Length > 1 && values[19].Trim().Length < 6)
                        {
                            strMensaje += "- Código de control incorrecto.";
                        }
                        if (values[7].Trim().Length > 1)
                        {
                            if (values[7].Trim().ContainsAny("-", "@", "/", "'", "\\", ".", ",", "_", "+", "[", "]", "{", "}", " "))
                            {
                                strMensaje += "- Identificador contiene caracteres invalidos.";
                            }
                        }
                        if (MCodificaParametrica((int)EnumTipoParametrica.Producto, values[11].Trim()) == -99999)
                        {
                            strMensaje += "- Producto incorrecto.";
                        }
                        //if (MCodificaParametrica((int)EnumTipoParametrica.Pais, values[7].Trim()) == -99999)
                        //{
                        //    strMensaje += "- País invalido.";
                        //}
                        //if (values[13].Trim() != "NOID" && values[23].Trim() == "0")
                        //{
                        //    strMensaje += "- El movimiento en envases debe tener Autorización de Sustancias Controladas.";
                        //}
                        if (values[3].Trim() != "NOID")
                        {
                            var lstInfraestructura = Session[CVariablesSesion.ListaInfraestructura] as List<O_INFRAESTRUCTURA_VOL_CTY>;
                            if (lstInfraestructura != null && lstInfraestructura.Any())
                            {
                                lstInfraestructura = lstInfraestructura.Where(x => x.CODIGO_OPERADOR == values[3].Trim() && x.TIPO == values[2].Trim()).ToList();
                                if (lstInfraestructura.Any())
                                {
                                    if (lstInfraestructura.FirstOrDefault().ID_PRODUCTO != MCodificaParametrica((int)EnumTipoParametrica.Producto, values[11].Trim()))
                                    {
                                        strMensaje += "- El producto no corresponde a la infraestructura origen especificada.";
                                    }
                                }
                                else
                                {
                                    strMensaje += "- Infraestructura origen invalida.";
                                }
                            }
                            else
                            {
                                strMensaje += "- Infraestructura origen no definida.";
                            }
                        }
                        if (values[5].Trim() != "NOID")
                        {
                            var lstInfraestructura = Session[CVariablesSesion.ListaInfraestructura] as List<O_INFRAESTRUCTURA_VOL_CTY>;
                            if (lstInfraestructura != null && lstInfraestructura.Any())
                            {
                                lstInfraestructura = lstInfraestructura.Where(x => x.CODIGO_OPERADOR == values[5].Trim() && x.TIPO == values[4].Trim()).ToList();
                                if (lstInfraestructura.Any())
                                {
                                    if (lstInfraestructura.FirstOrDefault().ID_PRODUCTO != MCodificaParametrica((int)EnumTipoParametrica.Producto, values[11].Trim()))
                                    {
                                        strMensaje += "- El producto no corresponde a la infraestructura origen especificada.";
                                    }
                                }
                                else
                                {
                                    strMensaje += "- Infraestructura origen invalida.";
                                }
                            }
                            else
                            {
                                strMensaje += "- Infraestructura origen no definida.";
                            }
                        }
                        if (values[4].Trim() != "NOID" && values[5].Trim() == "NOID")
                        {
                            if (MCodificaParametrica((int)EnumTipoParametrica.TipoInfraestructura, values[4].Trim()) == -99999)
                            {
                                strMensaje += "- Tipo de infraestructura origen invalida.";
                            }
                        }
                        if (values[1].Trim() != "NOID")
                        {
                            if (MCodificaParametrica((int)EnumTipoParametrica.Entidad, values[1].Trim()) == -99999)
                            {
                                strMensaje += "- Entidad origen invalida.";
                            }
                        }
                        if (values[22].Trim().Length == 0 || values[22].Trim() == "0")
                        {
                            strMensaje += "- Número de referencia invalido.";
                        }
                        if (Convert.ToDecimal(values[17]) == 0 && values[22].Trim().Length == 0 && Convert.ToDecimal(values[20]) == 0)
                        {
                            strMensaje += "- No existe documento de respaldo.";
                        }
                        #endregion
                        break;
                }
            }
            catch (Exception ex)
            {
                strMensaje += ex.Message;
            }

            if (strMensaje.Trim().Length > 0)
            {
                booResultado = false;
            }
            return booResultado;
        }
        private bool MValidaLineaTransporteDuctos(string[] values, ref string strMensaje)
        {
            var cmbto = cmbTipoOperacion.Text.Split(';');
            var strTipoOpe = cmbto[1];

            bool strResultado = true;
            try
            {
                #region validaciones

                /*var arrayNumero = new[] { 11, 13 };
                
                if (values.Length != 15)
                {
                    strMensaje = "- Nro. de columnas del registro invalido.";
                }
                else
                {
                    arrayNumero = new[] { 11, 13 };
                }
                                
                decimal dec;
                for (int i = 0; i < arrayNumero.Length; i++)
                {
                    if (!decimal.TryParse(values[arrayNumero[i]].Trim(), out dec))
                    {
                        strMensaje += "- Columna Nro. " + arrayNumero[i] + " No es un número.";
                    }
                }*/
                //if (values[0].Trim().Substring(6, 4) + values[0].Trim().Substring(3, 2) != cmbPeriodo.Value.ToString().Substring(0, 4) + cmbPeriodo.Value.ToString().Substring(4, 2))
                //{
                //    strMensaje += "- Periodo incorrecto.";
                //}
                /*DateTime dt;
                if (!DateTime.TryParse(values[0].Trim(), out dt))
                {
                    strMensaje += "- Fecha incorrecta.";
                }
                if (MCodificaParametrica((int)EnumTipoParametrica.Producto, values[10].Trim()) == -99999)
                {
                    strMensaje += "- Producto incorrecto.";
                }*/

                if (strTipoOpe == "TGDREC")
                {
                    if (MCodificaParametrica((int)EnumTipoParametrica.PoiRecepcion, values[2].Trim()) == -99999)
                    {
                        strMensaje += "- Tipo Transaccion POI incorrecto.";
                    }
                }
                else if (strTipoOpe == "TGDENT")
                {
                    if (MCodificaParametrica((int)EnumTipoParametrica.PoiEntrega, values[2].Trim()) == -99999)
                    {
                        strMensaje += "- Tipo Transaccion POI incorrecto.";
                    }
                }
                else if (strTipoOpe == "TLDREC")
                {
                    if (MCodificaParametrica((int)EnumTipoParametrica.PoiRecepcion, values[2].Trim()) == -99999)
                    {
                        strMensaje += "- Tipo Transaccion POI incorrecto.";
                    }
                }
                else if (strTipoOpe == "TLDENT")
                {
                    if (MCodificaParametrica((int)EnumTipoParametrica.PoiEntrega, values[2].Trim()) == -99999)
                    {
                        strMensaje += "- Tipo Transaccion POI incorrecto.";
                    }
                }
                else { strMensaje += "- Operacion incorrecta."; }

                /*if (values[12].Trim().Length > 0)
                { 
                    if (MCodificaParametrica((int)EnumTipoParametrica.SistemaDucto, values[12].Trim()) == -99999)
                    {
                        strMensaje += "- Sistema Ducto incorrecto.";
                    }
                } */

                /*if (MCodificaParametrica((int)EnumTipoParametrica.TransporteDucto, values[14].Trim()) == -99999)
                {
                    strMensaje += "- Transporte Ducto incorrecto.";
                }
                */
                if (MCodificaParametrica((int)EnumTipoParametrica.CodigoPoi, values[3].Trim()) == -99999)
                {
                    strMensaje += "- POI incorrecto.";
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

        private bool MValidaLineaRedesGas(string[] values, ref string strMensaje)
        {
            var cmbto = cmbTipoOperacion.Text.Split(';');
            var strTipoOpe = cmbto[1];

            bool strResultado = true;
            try
            {
                var arrayNumero = new[] { 4, 11, 12, 13, 14, 15, 16 };

                if (values.Length != 20)
                {
                    strMensaje = "- Nro. de columnas del registro invalido.";
                }
                else
                {
                    arrayNumero = new[] { 4, 11, 12, 13, 14, 15, 16 };
                }

                decimal decvalImport = 0, decValImpTotal = 0;
                decimal dec;
                for (int i = 0; i < arrayNumero.Length; i++)
                {
                    if (!decimal.TryParse(values[arrayNumero[i]].Trim(), out dec))
                    {
                        strMensaje += "- Columna Nro. " + arrayNumero[i] + " No es un número.";
                    }
                }

                if ((Convert.ToDecimal(values[4].Trim()) == 0)  && (Convert.ToDecimal(values[16].Trim()) == 0) && (Convert.ToDecimal(values[17].Trim()) == 0) && (Convert.ToDecimal(values[18].Trim().Length) == 0))
                {
                    strMensaje += "- Error de documento de registro.";
                }

                decvalImport = (Convert.ToDecimal(values[12].Trim()) + Convert.ToDecimal(values[13].Trim()) - Convert.ToDecimal(values[14].Trim()));
                decValImpTotal = (Convert.ToDecimal(values[15].Trim()));

                if (Convert.ToDouble(decValImpTotal) > Convert.ToDouble(decvalImport) + 0.05 || Convert.ToDouble(decValImpTotal) < Convert.ToDouble(decvalImport) - 0.05)
                {
                    strMensaje += "- El importe total facturado no valido.";
                }

                //if (values[21].Trim().Substring(6, 4) + values[21].Trim().Substring(3, 2) != cmbPeriodo.Value.ToString().Substring(0, 4) + cmbPeriodo.Value.ToString().Substring(4, 2))

                if (values[19].Trim().Substring(6, 4) + values[19].Trim().Substring(3, 2) != cmbPeriodo.Value.ToString().Substring(0, 4) + cmbPeriodo.Value.ToString().Substring(4, 2))
                {
                    strMensaje += "- Periodo incorrecto.";
                }

                DateTime dt;
                if (!DateTime.TryParse(values[9].Trim(), out dt))
                {
                    strMensaje += "- Fecha lectura incorrecta.";
                }

                if (MCodificaParametrica((int)EnumTipoParametrica.TipoConsumidor, values[3].Trim()) == -99999)
                {
                    strMensaje += "- Categoria incorrecto.";
                }
                if (MCodificaParametrica((int)EnumTipoParametrica.Producto, "GN") == -99999)
                {
                    strMensaje += "- Producto incorrecto.";
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

            return strResultado;
        }
        /// <summary>
        /// METODO. MValidaLineaEess
        /// </summary>
        /// <param name="values"></param>
        /// <param name="strMensaje"></param>
        /// <returns></returns>
        private bool MValidaLineaEess(string[] values, ref string strMensaje)
        {
            bool strResultado = true;
            try
            {
                if (cmbTipoOperacion.Text.Contains("DS"))
                {
                    #region Despachos
                    var arrayNumero = new[] { 5, 8, 12, 14, 17, 18, 20, 21, 23, 24, 25, 27, 28, 32, 34 };   //15
                    if (!cmbTipoOperacion.Text.Contains("MERMA"))
                    {
                        //if (values.Length != 36)
                        //{
                        //    strMensaje = "- Nro. de columnas del registro invalido.";
                        //}
                        if (values.Length != 36)
                        {
                            if (values.Length != 35)//LMQC-validar un campo extra para el sivetur
                            {
                                strMensaje = "- Nro. de columnas del registro invalido.";
                            }
                        }
                        else
                        {
                            arrayNumero = new[] { 5, 8, 12, 14, 15, 17, 18, 20, 21, 23, 24, 25, 27, 28, 32, 34, 35 };
                        }
                    }
                    else
                    {
                        //if (values.Length != 35)
                        //{
                        //    strMensaje = "- Nro. de columnas del registro invalido.";
                        //}
                        if (values.Length != 35)
                        {
                            if (values.Length != 36)//LMQC-validar un campo extra para el sivetur
                            {
                                strMensaje = "- Nro. de columnas del registro invalido.";
                            }
                            else
                            {
                                arrayNumero = new[] { 5, 8, 12, 14, 15, 17, 18, 20, 21, 23, 24, 25, 27, 28, 32, 34, 35 };
                            }
                        }
                        else
                        {
                            arrayNumero = new[] { 5, 8, 12, 14, 15, 17, 18, 20, 21, 23, 24, 25, 27, 28, 32, 34 };
                        }
                    }
                    //var arrayNumero = new[] { 5, 8, 12, 14, 15, 17, 18, 20, 21, 23, 24, 25, 27, 28, 32, 34, 35};
                    decimal dec;
                    for (int i = 0; i < arrayNumero.Length; i++)
                    {
                        if (!decimal.TryParse(values[arrayNumero[i]].Trim(), out dec))
                        {
                            strMensaje += "- Columna Nro. " + arrayNumero[i] + " No es un número.";
                        }
                    }
                    if (values[0].Trim().Substring(6, 4) + values[0].Trim().Substring(3, 2) != cmbPeriodo.Value.ToString().Substring(0, 4) + cmbPeriodo.Value.ToString().Substring(4, 2))
                    {
                        strMensaje += "- Periodo incorrecto.";
                    }
                    DateTime dt;
                    if (!DateTime.TryParse(values[0].Trim(), out dt))
                    {
                        strMensaje += "- Fecha incorrecta.";
                    }
                    if (MCodificaParametrica((int)EnumTipoParametrica.Producto, values[7].Trim()) == -99999)
                    {
                        strMensaje += "- Producto incorrecto.";
                    }
                    if (MCodificaParametrica((int)EnumTipoParametrica.UnidadMedida, values[7].Trim()) == -99999)
                    {
                        strMensaje += "- Unidad de medida invalida.";
                    }
                    if ((values[9].Trim() == "V" && Convert.ToDecimal(values[8]) <= 0) || (values[9].Trim() == "A" && Convert.ToDecimal(values[8]) < 0))
                    {
                        strMensaje += "- Cantidad incorrecta.";
                    }
                    if (Convert.ToDecimal(values[18].Trim()) > (Convert.ToDecimal(values[17].Trim()) * Convert.ToDecimal(values[8].Trim())) + 2 ||
                        Convert.ToDecimal(values[18].Trim()) < (Convert.ToDecimal(values[17].Trim()) * Convert.ToDecimal(values[8].Trim())) - 2)
                    {
                        strMensaje += "- Importe incorrecto.";
                    }
                    if (values[16].Trim().Length == 1 && values[16].Trim() != "0")
                    {
                        strMensaje += "- Código de control incorrecto.";
                    }
                    if (values[16].Trim().Length > 1 && values[16].Trim().Length < 6)
                    {
                        strMensaje += "- Código de control incorrecto.";
                    }
                    if (values[3].Trim().Length > 1)
                    {
                        if (values[3].Trim().ContainsAny("-", "@", "/", "'", "\\", ".", ",", "_", "+", "[", "]", "{", "}", " "))
                        {
                            strMensaje += "- Identificador contiene caracteres invalidos.";
                        }
                    }
                    if (Convert.ToInt64(values[24]) > 0)
                    {
                        if (Convert.ToDecimal(values[28].Trim()) > (Convert.ToDecimal(values[27].Trim()) * Convert.ToDecimal(values[8].Trim())) + 2 ||
                            Convert.ToDecimal(values[28].Trim()) < (Convert.ToDecimal(values[27].Trim()) * Convert.ToDecimal(values[8].Trim())) - 2)
                        {
                            strMensaje += "- Importe factura externa incorrecto.";
                        }
                        if (values[6].Trim() == "BOLIVIA")
                        {
                            strMensaje += "- País incorrecto.";
                        }
                        if (values[26].Trim().Length == 1 && values[26].Trim() != "0")
                        {
                            strMensaje += "- Código de control factura externa incorrecto.";
                        }
                        if (values[26].Trim().Length > 1 && values[26].Trim().Length < 6)
                        {
                            strMensaje += "- Código de control factura externa incorrecto.";
                        }
                    }
                    if (Convert.ToDecimal(values[21]) > 0)
                    {
                        if (Convert.ToDecimal(values[21].Trim()) > (Convert.ToDecimal(values[20].Trim()) * Convert.ToDecimal(values[8].Trim())) + 2 ||
                            Convert.ToDecimal(values[21].Trim()) < (Convert.ToDecimal(values[20].Trim()) * Convert.ToDecimal(values[8].Trim())) - 2)
                        {
                            strMensaje += "- Importe vale incorrecto.";
                        }
                    }
                    if (values[11].Trim() == "VAL")
                    {
                        if (values[19].Trim() == "")
                        {
                            strMensaje += "- Número de vale incorrecto.";
                        }
                    }
                    if (values[30].Trim() != "NOID" && values[30].Trim() != "SUIC" && values[30].Trim() != "DS2243" && values[30].Trim() != "SICEVE" && values[30].Trim() != "DS3578")
                    {
                        strMensaje += "- Servicio adicional incorrecto.";
                    }
                    if (values[30].Trim() != "NOID" && values[31].Trim() == "")
                    {
                        strMensaje += "- Número de referencia del servicio adicional invalido.";
                    }
                    if (values[33].Trim() != "NOID" && values[33].Trim() != "SC" && values[33].Trim() != "CC")
                    {
                        strMensaje += "- Tipo de autorización de venta incorrecto.";
                    }
                    if (values[11].Trim() != "EF" && values[11].Trim() != "PRE" && values[11].Trim() != "VAL")
                    {
                        strMensaje += "- Forma de pago incorrecta.";
                    }
                    if (Convert.ToInt64(values[34]) > 0 && values[33].Trim() == "NOID")
                    {
                        strMensaje += "- Autorización de venta invalida.";
                    }
                    if (values[2].Trim() != "NOID")
                    {
                        var lstInfraestructura = Session[CVariablesSesion.ListaInfraestructura] as List<O_INFRAESTRUCTURA_VOL_CTY>;
                        if (lstInfraestructura != null && lstInfraestructura.Any())
                        {
                            lstInfraestructura = lstInfraestructura.Where(x => x.CODIGO_OPERADOR == values[2].Trim() && x.TIPO == values[1].Trim()).ToList();
                            if (lstInfraestructura.Any())
                            {
                                if (lstInfraestructura.FirstOrDefault().ID_PRODUCTO != MCodificaParametrica((int)EnumTipoParametrica.Producto, values[7].Trim()))
                                {
                                    strMensaje += "- El producto no corresponde a la infraestructura receptora especificada.";
                                }
                            }
                            else
                            {
                                strMensaje += "- Infraestructura despachante invalida.";
                            }
                        }
                        else
                        {
                            strMensaje += "- Infraestructura despachante no registrada.";
                        }
                    }
                    if (Convert.ToDecimal(values[14]) == 0 && values[19].Trim().Length == 0 && values[29].Trim().Length == 0)
                    {
                        strMensaje += "- No existe documento de respaldo.";
                    }
                    #endregion
                }
                else
                {
                    #region Recepciones
                    if (values.Length != 30)
                    {
                        strMensaje = "- Nro. de columnas del registro invalido.";
                    }
                    var arrayNumero = new[] { 6, 10, 13, 16, 19, 20, 23, 24, 27, 29 };  //17, 
                    decimal dec;
                    for (int i = 0; i < arrayNumero.Length; i++)
                    {
                        if (!decimal.TryParse(values[arrayNumero[i]].Trim(), out dec))
                        {
                            strMensaje += "- Columna Nro. " + arrayNumero[i] + " No es un número.";
                        }
                    }
                    if (values[0].Trim().Substring(6, 4) + values[0].Trim().Substring(3, 2) != cmbPeriodo.Value.ToString().Substring(0, 4) + cmbPeriodo.Value.ToString().Substring(4, 2))
                    {
                        strMensaje += "- Periodo incorrecto.";
                    }
                    DateTime dt;
                    if (!DateTime.TryParse(values[0].Trim(), out dt))
                    {
                        strMensaje += "- Fecha incorrecta.";
                    }
                    if (MCodificaParametrica((int)EnumTipoParametrica.Producto, values[9].Trim()) == -99999)
                    {
                        strMensaje += "- Producto incorrecto.";
                    }
                    if (MCodificaParametrica((int)EnumTipoParametrica.UnidadMedida, values[9].Trim()) == -99999)
                    {
                        strMensaje += "- Unidad de medida invalida.";
                    }
                    if (Convert.ToDecimal(values[10]) <= 0)
                    {
                        strMensaje += "- Cantidad incorrecto.";
                    }
                    //La factura puede contener otros items
                    //if (Convert.ToDecimal(values[20].Trim()) > (Convert.ToDecimal(values[19].Trim()) * Convert.ToDecimal(values[10].Trim())) + 2 ||
                    //    Convert.ToDecimal(values[20].Trim()) < (Convert.ToDecimal(values[19].Trim()) * Convert.ToDecimal(values[10].Trim())) - 2)
                    //{
                    //    strMensaje += "- Importe incorrecto.";
                    //}
                    if (values[5].Trim().Length > 1)
                    {
                        if (values[5].Trim().ContainsAny("-", "@", "/", "'", "\\", ".", ",", "_", "+", "[", "]", "{", "}", " "))
                        {
                            strMensaje += "- Identificador contiene caracteres invalidos.";
                        }
                    }
                    if (values[1].Trim() != "NOID")
                    {
                        if (MCodificaParametrica((int)EnumTipoParametrica.Entidad, values[1].Trim()) == -99999)
                        {
                            strMensaje += "- Entidad origen invalida.";
                        }
                    }
                    if (values[3].Trim() != "NOID")
                    {
                        var lstInfraestructura = Session[CVariablesSesion.ListaInfraestructura] as List<O_INFRAESTRUCTURA_VOL_CTY>;
                        if (lstInfraestructura != null && lstInfraestructura.Any())
                        {
                            lstInfraestructura = lstInfraestructura.Where(x => x.CODIGO_OPERADOR == values[3].Trim() && x.TIPO == values[2].Trim()).ToList();
                            if (lstInfraestructura.Any())
                            {
                                if (lstInfraestructura.FirstOrDefault().ID_PRODUCTO != MCodificaParametrica((int)EnumTipoParametrica.Producto, values[9].Trim()))
                                {
                                    strMensaje += "- El producto no corresponde a la infraestructura receptora especificada.";
                                }
                            }
                            else
                            {
                                strMensaje += "- Infraestructura despachante invalida.";
                            }
                        }
                        else
                        {
                            strMensaje += "- Infraestructura despachante no registrada.";
                        }
                    }
                    if (Convert.ToDecimal(values[16]) == 0 && values[21].Trim().Length == 0 && values[26].Trim().Length == 0)
                    {
                        strMensaje += "- No existe documento de respaldo.";
                    }
                    #endregion
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

            return strResultado;
        }

        /// <summary>
        /// METODO. MCargaEess
        /// </summary>
        /// <param name="strRuta"></param>
        private void MCargaEess(string strRuta)
        {
            var reader = new StreamReader(File.OpenRead(@strRuta));
            var lstRegistros = new List<CRegistrosImportados>();
            var lstRegistroE = new List<CRegistrosError>();
            long lngNoRegCorrect = 1, lngNoRegInCorrect = 1, lngNoReg = 1;
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

                    var to = cmbTipoOperacion.Text.Split(';');
                    var strTipoOp = to[1].Substring(2, 2);
                    string strMensaje = string.Empty;
                    if (MValidaLineaEess(values, ref strMensaje))
                    {
                        var cDato = new CRegistrosImportados();
                        int controlBinario;                                     
                        switch (strTipoOp)
                        {
                            case "DS":
                                #region Valida y genera objeto
                                cDato.LngId = lngNoRegCorrect;
                                cDato.StrIdVenta = lngNoRegCorrect.ToString();
                                datFecha = DateTime.Parse(values[0].Trim());
                                cDato.StrFechaEmision = datFecha.ToString();
                                //cDato.StrTipoMov = (values[6].Trim() == "BOLIVIA") ? "NAC" : "INT";
                                if (values.Length == 36)//LMQC-modificado para sivetur
                                {
                                    if (Convert.ToInt64(values[35]) == 1) { cDato.StrTipoMov = "SIVETUR"; }
                                    else { cDato.StrTipoMov = (values[6].Trim() == "BOLIVIA") ? "NAC" : "INT"; }
                                }
                                cDato.StrEstado = values[7].Trim();
                                cDato.LngNit = Convert.ToInt64(values[12]);
                                cDato.StrRazonSocial = values[13].Trim().ToUpper().Replace("¥", "Ñ").Replace("Á", "A").Replace("É", "E").Replace("Í", "I").Replace("Ó", "O").Replace("Ú", "U").Replace("'", "").Replace("�", "Ñ");
                                cDato.StrRazonSocial = Regex.Replace(cDato.StrRazonSocial, @"\s+", " ");
                                cDato.StrIdentificador = values[3].Trim().Replace("-", "").Replace(" ", "");
                                cDato.LngNroFactura = Convert.ToInt64(values[14]);                            
                                cDato.LngNroAutorizacion = esnumero(values[15].Trim())==true ? Convert.ToInt64(values[15]) : 0; //Convert.ToInt64(values[15]);
                                cDato.strCodigoAutorizacion = esnumero(values[15].Trim()) == true ? "" : values[15].Trim();
                                cDato.StrNroReferencia = values[29].Trim();
                                cDato.LngNroAutVenta = Convert.ToInt64(values[34]);
                                cDato.StrProducto = values[7].Trim();
                                cDato.DecVolumen = Convert.ToDecimal(values[8]);
                                cDato.DecPrecio = Convert.ToDecimal(values[17]);
                                cDato.DecImporte = Convert.ToDecimal(values[18]);
                                cDato.DecIdTipoCreditoFiscal = (Convert.ToDecimal(values[14]) > 0 && Convert.ToDecimal(values[17]) > 0) ? (int)EnumTipoCredFiscal.Si : (int)EnumTipoCredFiscal.No;
                                cDato.objProtoMovimiento = new ObjProtoMovimientoTransaccion();
                                cDato.DecIdTipoOp = Convert.ToDecimal(cmbTipoOperacion.Value);
                                cDato.DecOrden = 1;

                                strFecha = datFecha.Year + datFecha.Month.ToString().PadLeft(2, '0') + datFecha.Day.ToString().PadLeft(2, '0') + datFecha.Hour.ToString().PadLeft(2, '0') + datFecha.Minute.ToString().PadLeft(2, '0') + datFecha.Second.ToString().PadLeft(2, '0');
                                datFechaReg = DateTime.Now;
                                estadoTra = (cDato.StrEstado == "A") ? (int)EnumEstadoMov.Anulado : (int)EnumEstadoMov.Activo;
                                lstTransacciones = new List<ObjTransaccion>();
                                var intFormaPago = (values[11].Trim() == "VAL") ? 3 : (values[10].Trim() == "PRE") ? 2 : 1;
                                controlBinario = 1;

                                //if (Convert.ToDecimal(values[14].Trim()) > 0 && Convert.ToDecimal(values[17].Trim()) > 0)
                                if (Convert.ToDecimal(values[14].Trim()) > 0 && values[15].Trim().Length > 0)
                                {
                                    objFactura = new ObjTransaccion();
                                    objFactura.idMovimiento = 0;
                                    objFactura.idTransaccion = 0;
                                    objFactura.idTransaccionPadre = 0;
                                    objFactura.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                    objFactura.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                    //objFactura.idTipoTransaccion = (decimal)((values[16].Trim().Length > 0) ? EnumTipoTransaccion.FacturaComputarizada : EnumTipoTransaccion.FacturaManual);
                                    objFactura.fechaEmision = Convert.ToInt64(strFecha);
                                    objFactura.numeroTransaccion = values[14].Trim();                                    
                                    objFactura.numeroAutorizacion = esnumero(values[15].Trim()) == true ? Convert.ToDecimal(values[15]) : 0;//Convert.ToDecimal(values[15]);
                                    objFactura.codigoAutorizacion = esnumero(values[15].Trim()) == true ? "" : values[15].Trim();
                                    objFactura.idTipoTransaccion = (decimal)((objFactura.codigoAutorizacion.Length > 25 || values[16].Trim().Length > 0) ? EnumTipoTransaccion.FacturaComputarizada : EnumTipoTransaccion.FacturaManual);
                                    objFactura.codigoControl = values[16].Trim();
                                    objFactura.nitConsumidor = Convert.ToDecimal(values[12].Trim());
                                    objFactura.razonSocial = cDato.StrRazonSocial;
                                    objFactura.precioUnitario = Convert.ToDecimal(values[17].Trim());
                                    objFactura.precioTotal = Convert.ToDecimal(values[18].Trim());
                                    objFactura.usuarioTransaccion = values[10].Trim();
                                    objFactura.observaciones = "";
                                    objFactura.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                    objFactura.idEstadoTransaccion = estadoTra;
                                    objFactura.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.Si;
                                    objFactura.idFormaPago = intFormaPago;
                                    objFactura.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                    objFactura.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                    objFactura.codigoSeguridad = 0;
                                    objFactura.idAutorizacionDato = 0;
                                    objFactura.binarioControl = controlBinario + "000000000";
                                    objFactura.fechaOperacion = Convert.ToInt64(strFecha);
                                    objFactura.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                    objFactura.codigo = "";

                                    lstTransacciones.Add(objFactura);
                                    controlBinario = 0;
                                }

                                if (values[11].Trim() == "VAL" && values[19].Trim().Length > 0)
                                {
                                    objDocumento = new ObjTransaccion();
                                    objDocumento.idMovimiento = 0;
                                    objDocumento.idTransaccion = 0;
                                    objDocumento.idTransaccionPadre = 0;
                                    objDocumento.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                    objDocumento.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                    objDocumento.idTipoTransaccion = (decimal)EnumTipoTransaccion.DocVale;
                                    objDocumento.fechaEmision = Convert.ToInt64(strFecha);
                                    objDocumento.numeroTransaccion = values[19].Trim();
                                    objDocumento.numeroAutorizacion = 0;
                                    objDocumento.codigoControl = "";
                                    objDocumento.nitConsumidor = Convert.ToDecimal(values[23].Trim());
                                    objDocumento.razonSocial = values[22].Trim();
                                    objDocumento.precioUnitario = Convert.ToDecimal(values[20].Trim());
                                    objDocumento.precioTotal = Convert.ToDecimal(values[21].Trim());
                                    objDocumento.usuarioTransaccion = values[10].Trim();
                                    objDocumento.observaciones = "";
                                    objDocumento.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                    objDocumento.idEstadoTransaccion = estadoTra;
                                    objDocumento.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.No;
                                    objDocumento.idFormaPago = intFormaPago;
                                    objDocumento.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                    objDocumento.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                    objDocumento.codigoSeguridad = 0;
                                    objDocumento.idAutorizacionDato = 0;
                                    objDocumento.binarioControl = controlBinario + "000000000";
                                    objDocumento.fechaOperacion = Convert.ToInt64(strFecha);
                                    objDocumento.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                    objDocumento.codigo = "";
                                    lstTransacciones.Add(objDocumento);
                                    controlBinario = 0;
                                }

                                if (values[29].Trim().Length > 0)
                                {
                                    objDocumento = new ObjTransaccion();
                                    objDocumento.idMovimiento = 0;
                                    objDocumento.idTransaccion = 0;
                                    objDocumento.idTransaccionPadre = 0;
                                    objDocumento.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                    objDocumento.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                    objDocumento.idTipoTransaccion = (decimal)EnumTipoTransaccion.NotaMovimiento;
                                    objDocumento.fechaEmision = Convert.ToInt64(strFecha);
                                    objDocumento.numeroTransaccion = values[29].Trim();
                                    objDocumento.numeroAutorizacion = 0;
                                    objDocumento.codigoControl = "";
                                    objDocumento.nitConsumidor = 0;
                                    objDocumento.razonSocial = "";
                                    objDocumento.precioUnitario = 0;
                                    objDocumento.precioTotal = 0;
                                    objDocumento.usuarioTransaccion = values[10].Trim();
                                    objDocumento.observaciones = "";
                                    objDocumento.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                    objDocumento.idEstadoTransaccion = estadoTra;
                                    objDocumento.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.No;
                                    objDocumento.idFormaPago = intFormaPago;
                                    objDocumento.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                    objDocumento.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                    objDocumento.codigoSeguridad = 0;
                                    objDocumento.idAutorizacionDato = 0;
                                    objDocumento.binarioControl = controlBinario + "000000000";
                                    objDocumento.fechaOperacion = Convert.ToInt64(strFecha);
                                    objDocumento.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                    objDocumento.codigo = "";
                                    lstTransacciones.Add(objDocumento);
                                }

                                if (Convert.ToDecimal(values[24].Trim()) > 0)
                                {
                                    objFactura = new ObjTransaccion();
                                    objFactura.idMovimiento = 0;
                                    objFactura.idTransaccion = 0;
                                    objFactura.idTransaccionPadre = 0;
                                    objFactura.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                    objFactura.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                    objFactura.idTipoTransaccion = (decimal)((values[26].Trim().Length > 0) ? EnumTipoTransaccion.FacturaComputarizada : EnumTipoTransaccion.FacturaManual);
                                    objFactura.fechaEmision = Convert.ToInt64(strFecha);
                                    objFactura.numeroTransaccion = values[24].Trim();
                                    objFactura.numeroAutorizacion = Convert.ToDecimal(values[25].Trim());
                                    objFactura.codigoControl = values[26].Trim();
                                    objFactura.nitConsumidor = Convert.ToDecimal(values[12].Trim());
                                    objFactura.razonSocial = cDato.StrRazonSocial;
                                    objFactura.precioUnitario = Convert.ToDecimal(values[27].Trim());
                                    objFactura.precioTotal = Convert.ToDecimal(values[28].Trim());
                                    objFactura.usuarioTransaccion = values[10].Trim();
                                    objFactura.observaciones = "";
                                    objFactura.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                    objFactura.idEstadoTransaccion = estadoTra;
                                    objFactura.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.Si;
                                    objFactura.idFormaPago = intFormaPago;
                                    objFactura.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                    objFactura.idTipoEmisor = (decimal)EnumTipoEmisor.ExternaYpfb;
                                    objFactura.codigoSeguridad = 0;
                                    objFactura.idAutorizacionDato = 0;
                                    objFactura.binarioControl = "0000000000";
                                    objFactura.fechaOperacion = Convert.ToInt64(strFecha);
                                    objFactura.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                    objFactura.codigo = "";
                                    lstTransacciones.Add(objFactura);
                                }
                                if (values[34].Trim() != "0")
                                {
                                    objDocumento = new ObjTransaccion();
                                    objDocumento.idMovimiento = 0;
                                    objDocumento.idTransaccion = 0;
                                    objDocumento.idTransaccionPadre = 0;
                                    objDocumento.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                    objDocumento.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                    objDocumento.idTipoTransaccion = (values[33].Trim() == "CC") ? (decimal)EnumTipoTransaccion.AutComConjunto : (decimal)EnumTipoTransaccion.AutSustCtrl;
                                    objDocumento.fechaEmision = Convert.ToInt64(strFecha);
                                    objDocumento.numeroTransaccion = values[34].Trim();
                                    objDocumento.numeroAutorizacion = 0;
                                    objDocumento.codigoControl = "";
                                    objDocumento.nitConsumidor = 0;
                                    objDocumento.razonSocial = "";
                                    objDocumento.precioUnitario = 0;
                                    objDocumento.precioTotal = 0;
                                    objDocumento.usuarioTransaccion = values[10].Trim();
                                    objDocumento.observaciones = "";
                                    objDocumento.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                    objDocumento.idEstadoTransaccion = estadoTra;
                                    objDocumento.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.No;
                                    objDocumento.idFormaPago = intFormaPago;
                                    objDocumento.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                    objDocumento.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                    objDocumento.codigoSeguridad = 0;
                                    objDocumento.idAutorizacionDato = 0;
                                    objDocumento.binarioControl = "0000000000";
                                    objDocumento.fechaOperacion = Convert.ToInt64(strFecha);
                                    objDocumento.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                    objDocumento.codigo = "";
                                    lstTransacciones.Add(objDocumento);
                                }
                                if (values[30].Trim() != "NOID")
                                {
                                    objDocumento = new ObjTransaccion();
                                    objDocumento.idMovimiento = 0;
                                    objDocumento.idTransaccion = 0;
                                    objDocumento.idTransaccionPadre = 0;
                                    objDocumento.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                    objDocumento.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                    objDocumento.idTipoTransaccion = (values[30].Trim() == "SUIC") ? (decimal)EnumTipoTransaccion.SrvSuic : (values[30].Trim() == "SICEVE") ? (decimal)EnumTipoTransaccion.SrvSiceve : (values[30].Trim() == "DS3578") ? (decimal)EnumTipoTransaccion.SrvDs3578 : (decimal)EnumTipoTransaccion.SrvDs2243;
                                    objDocumento.fechaEmision = Convert.ToInt64(strFecha);
                                    objDocumento.numeroTransaccion = values[31].Trim();
                                    objDocumento.numeroAutorizacion = 0;
                                    objDocumento.codigoControl = "";
                                    objDocumento.nitConsumidor = 0;
                                    objDocumento.razonSocial = "";
                                    objDocumento.precioUnitario = (Convert.ToDecimal(values[32].Trim()) == 0) ? 0 : (Math.Round(Convert.ToDecimal(values[32].Trim()) / Convert.ToDecimal(values[8].Trim()), 2));
                                    objDocumento.precioTotal = Convert.ToDecimal(values[32].Trim());
                                    objDocumento.usuarioTransaccion = values[10].Trim();
                                    objDocumento.observaciones = "";
                                    objDocumento.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                    objDocumento.idEstadoTransaccion = estadoTra;
                                    objDocumento.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.No;
                                    objDocumento.idFormaPago = intFormaPago;
                                    objDocumento.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                    objDocumento.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                    objDocumento.codigoSeguridad = 0;
                                    objDocumento.idAutorizacionDato = 0;
                                    objDocumento.binarioControl = "0000000000";
                                    objDocumento.fechaOperacion = Convert.ToInt64(strFecha);
                                    objDocumento.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                    objDocumento.codigo = "";
                                    lstTransacciones.Add(objDocumento);
                                }
                                if (Convert.ToDecimal(values[14].Trim()) > 0 && Convert.ToDecimal(values[17].Trim()) == 0)
                                {
                                    objDocumento = new ObjTransaccion();
                                    objDocumento.idMovimiento = 0;
                                    objDocumento.idTransaccion = 0;
                                    objDocumento.idTransaccionPadre = 0;
                                    objDocumento.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                    objDocumento.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                    objDocumento.idTipoTransaccion = (decimal)EnumTipoTransaccion.DocFactRef;
                                    objDocumento.fechaEmision = Convert.ToInt64(strFecha);
                                    objDocumento.numeroTransaccion = values[14].Trim();
                                    objDocumento.numeroAutorizacion = Convert.ToDecimal(values[15]);
                                    objDocumento.codigoControl = "";
                                    objDocumento.nitConsumidor = Convert.ToDecimal(values[12].Trim());
                                    objDocumento.razonSocial = cDato.StrRazonSocial;
                                    objDocumento.precioUnitario = 0;
                                    objDocumento.precioTotal = 0;
                                    objDocumento.usuarioTransaccion = values[10].Trim();
                                    objDocumento.observaciones = "";
                                    objDocumento.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                    objDocumento.idEstadoTransaccion = estadoTra;
                                    objDocumento.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.No;
                                    objDocumento.idFormaPago = intFormaPago;
                                    objDocumento.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                    objDocumento.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                    objDocumento.codigoSeguridad = 0;
                                    objDocumento.idAutorizacionDato = 0;
                                    objDocumento.binarioControl = "0000000000";
                                    objDocumento.fechaOperacion = Convert.ToInt64(strFecha);
                                    objDocumento.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                    objDocumento.codigo = "";
                                    lstTransacciones.Add(objDocumento);
                                }

                                objNuevoTipoReg = new ObjProtoMovimientoTransaccion();
                                objNuevoTipoReg.idMovimientoPadre = 0;
                                objNuevoTipoReg.idTipoActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                objNuevoTipoReg.idTipoOperacion = Convert.ToDecimal(cmbTipoOperacion.Value);
                                objNuevoTipoReg.idConsumidor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString());
                                objNuevoTipoReg.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                objNuevoTipoReg.fechaRegistroMovimiento = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                objNuevoTipoReg.fechaOperacion = Convert.ToInt64(strFecha);
                                objNuevoTipoReg.idTipoMovimientoFlujo = (values[11].Trim() == "PRE") ? (int)EnumTipoMovimientoFlujo.Contable : (int)EnumTipoMovimientoFlujo.Fisico;
                                objNuevoTipoReg.idTipoMovimiento = (cDato.StrTipoMov == "NAC") ? (int)EnumTipoMovimiento.NacNac : (cDato.StrTipoMov == "SIVETUR") ? (int)EnumTipoMovimiento.SIVETUR : (int)EnumTipoMovimiento.NacInt;
                                objNuevoTipoReg.idEstadoMovimiento = estadoTra;
                                objNuevoTipoReg.idFuenteOrigenDato = (decimal)EnumTipoRegistro.OrigenCsv;
                                objNuevoTipoReg.idUbicacionOperacion = MCodificaParametrica((int)EnumTipoParametrica.Pais, values[6].Trim());
                                objNuevoTipoReg.idUnidadMedidaHidrocarburo = MCodificaParametrica((int)EnumTipoParametrica.UnidadMedida, values[7].Trim());
                                objNuevoTipoReg.idHidrocarburo = MCodificaParametrica((int)EnumTipoParametrica.Producto, values[7].Trim());
                                objNuevoTipoReg.cantidadHidrocarburo = cDato.DecVolumen;
                                objNuevoTipoReg.identificador = cDato.StrIdentificador;
                                objNuevoTipoReg.subIdentificador = values[4].Trim();
                                objNuevoTipoReg.observacion = "";

                                if (to[1].Substring(4, 2) == "EX")
                                {
                                    objNuevoTipoReg.idTipoMedioTransporte = MCodificaParametrica((int)EnumTipoParametrica.TipoInfraestructura, values[1].Trim());
                                    objNuevoTipoReg.idMedioTransporte = MCodificaParametrica((int)EnumTipoParametrica.Infraestructura, values[1].Trim() + "|" + values[2].Trim());
                                    objNuevoTipoReg.tipoInfraestructura = -99999;
                                    objNuevoTipoReg.idInfraestructura = -99999;
                                }
                                else
                                {
                                    objNuevoTipoReg.idTipoMedioTransporte = -99999;
                                    objNuevoTipoReg.idMedioTransporte = -99999;
                                    objNuevoTipoReg.tipoInfraestructura = MCodificaParametrica((int)EnumTipoParametrica.TipoInfraestructura, values[1].Trim());
                                    objNuevoTipoReg.idInfraestructura = MCodificaParametrica((int)EnumTipoParametrica.Infraestructura, values[1].Trim() + "|" + values[2].Trim());
                                }

                                objNuevoTipoReg.idUnidadEnvasesTransporte = MCodificaParametrica((int)EnumTipoParametrica.TipoContenedor, values[3].Trim());
                                objNuevoTipoReg.idEntidad = -99999;
                                objNuevoTipoReg.idTipoMedicion = -99999;
                                objNuevoTipoReg.cantidadAguaLitros = 0;
                                objNuevoTipoReg.idUnidadTemperatura = -99999;
                                objNuevoTipoReg.temperatura = 0;
                                objNuevoTipoReg.gradoApi = "";
                                objNuevoTipoReg.cantidadEnvasesTransporte = 0;
                                objNuevoTipoReg.binarioControl = "00000-0000000000-0000000000-00000";
                                objNuevoTipoReg.idIntegracion = -99999;
                                objNuevoTipoReg.idTransaccionBaseExterna = "";
                                objNuevoTipoReg.fechaOperacionDia = 0;
                                objNuevoTipoReg.codigo = "";
                                objNuevoTipoReg.barcode = Convert.ToDecimal(values[5].Trim());
                                objNuevoTipoReg.listaTransaccion = JsonSerializer.SerializeToString(lstTransacciones);

                                objNuevoTipoReg.jsonReferencia = "";
                                objNuevoTipoReg.estadoJsonRef = -99999;
                                objNuevoTipoReg.idCuenta = -99999;
                                objNuevoTipoReg.idCuentaAux = -99999;
                                objNuevoTipoReg.idParamGlobal = -99999;
                                objNuevoTipoReg.idParamEspec = -99999;
                                objNuevoTipoReg.idTipoConsumidor = (objNuevoTipoReg.idUnidadEnvasesTransporte == -99999) ? 32 : 31;
                                objNuevoTipoReg.idTipoInfraAux = -99999;
                                objNuevoTipoReg.idInfraAux = -99999;

                                #endregion
                                break;
                            case "RC":
                                #region Valida y genera objeto
                                cDato.LngId = lngNoRegCorrect;
                                cDato.StrIdVenta = lngNoRegCorrect.ToString();
                                datFecha = DateTime.Parse(values[0].Trim());
                                cDato.StrFechaEmision = datFecha.ToString();
                                cDato.StrTipoMov = "NAC";
                                cDato.StrEstado = values[11].Trim();
                                cDato.LngNit = Convert.ToInt64(values[13]);
                                cDato.StrRazonSocial = values[14].Trim().ToUpper().Replace("¥", "Ñ").Replace("Á", "A").Replace("É", "E").Replace("Í", "I").Replace("Ó", "O").Replace("Ú", "U").Replace("'", "").Replace("�", "Ñ");
                                cDato.StrRazonSocial = Regex.Replace(cDato.StrRazonSocial, @"\s+", " ");
                                cDato.StrIdentificador = values[4].Trim().Replace("-", "").Replace(" ", "");
                                cDato.LngNroFactura = Convert.ToInt64(values[16]);                            
                                cDato.LngNroAutorizacion = esnumero(values[17].Trim())==true ? Convert.ToInt64(values[17]) : 0; //Convert.ToInt64(values[17]);
                                cDato.strCodigoAutorizacion = esnumero(values[17].Trim()) == true ? "" : values[17].Trim();
                                cDato.StrNroReferencia = values[21].Trim();
                                cDato.LngNroAutVenta = Convert.ToInt64(values[24]);
                                cDato.StrProducto = values[9].Trim();
                                cDato.DecVolumen = Convert.ToDecimal(values[10]);
                                cDato.DecPrecio = Convert.ToDecimal(values[19]);
                                cDato.DecImporte = Convert.ToDecimal(values[20]);
                                cDato.DecIdTipoCreditoFiscal = (Convert.ToDecimal(values[16]) > 0 && Convert.ToDecimal(values[19]) > 0) ? (int)EnumTipoCredFiscal.Si : (int)EnumTipoCredFiscal.No;
                                cDato.objProtoMovimiento = new ObjProtoMovimientoTransaccion();
                                cDato.DecIdTipoOp = Convert.ToDecimal(cmbTipoOperacion.Value);
                                cDato.DecOrden = 1;

                                strFecha = datFecha.Year + datFecha.Month.ToString().PadLeft(2, '0') + datFecha.Day.ToString().PadLeft(2, '0') + datFecha.Hour.ToString().PadLeft(2, '0') + datFecha.Minute.ToString().PadLeft(2, '0') + datFecha.Second.ToString().PadLeft(2, '0');
                                datFechaReg = DateTime.Now;
                                estadoTra = (cDato.StrEstado == "A") ? (int)EnumEstadoMov.Anulado : (int)EnumEstadoMov.Activo;
                                lstTransacciones = new List<ObjTransaccion>();
                                controlBinario = 1;

                                //if (Convert.ToDecimal(values[16].Trim()) > 0 && Convert.ToDecimal(values[17].Trim()) > 0)
                                if (Convert.ToDecimal(values[16].Trim()) > 0 && values[17].Trim().Length > 0)
                                {
                                    objFactura = new ObjTransaccion();
                                    objFactura.idMovimiento = 0;
                                    objFactura.idTransaccion = 0;
                                    objFactura.idTransaccionPadre = 0;
                                    objFactura.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                    objFactura.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                    //objFactura.idTipoTransaccion = (decimal)((values[18].Trim().Length > 0) ? EnumTipoTransaccion.FacturaComputarizada : EnumTipoTransaccion.FacturaManual);
                                    objFactura.fechaEmision = CFechas.ConvierteDateTimeLong(DateTime.Parse(values[15].Trim()));
                                    objFactura.numeroTransaccion = values[16].Trim();
                                    objFactura.numeroAutorizacion = esnumero(values[17].Trim()) == true ? Convert.ToDecimal(values[17]) : 0;//Convert.ToDecimal(values[17]);
                                    objFactura.codigoAutorizacion = esnumero(values[17].Trim()) == true ? "" : values[17].Trim();
                                    objFactura.idTipoTransaccion = (decimal)((objFactura.codigoAutorizacion.Length > 25 || values[18].Trim().Length > 0) ? EnumTipoTransaccion.FacturaComputarizada : EnumTipoTransaccion.FacturaManual);
                                    objFactura.codigoControl = values[18].Trim();
                                    objFactura.nitConsumidor = Convert.ToDecimal(values[13].Trim());
                                    objFactura.razonSocial = cDato.StrRazonSocial;
                                    objFactura.precioUnitario = Convert.ToDecimal(values[19].Trim());
                                    objFactura.precioTotal = Convert.ToDecimal(values[20].Trim());
                                    objFactura.usuarioTransaccion = values[12].Trim();
                                    objFactura.observaciones = "";
                                    objFactura.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                    objFactura.idEstadoTransaccion = estadoTra;
                                    objFactura.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.Si;
                                    objFactura.idFormaPago = (decimal)EnumTiposPago.Efectivo;
                                    objFactura.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                    objFactura.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                    objFactura.codigoSeguridad = 0;
                                    objFactura.idAutorizacionDato = 0;
                                    objFactura.binarioControl = controlBinario + "000000000";
                                    objFactura.fechaOperacion = Convert.ToInt64(strFecha);
                                    objFactura.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                    objFactura.codigo = "";
                                    lstTransacciones.Add(objFactura);
                                    controlBinario = 0;
                                }
                                if (values[25].Trim().Length > 0 && values[26].Trim().Length > 0)
                                {
                                    objDocumento = new ObjTransaccion();
                                    objDocumento.idMovimiento = 0;
                                    objDocumento.idTransaccion = 0;
                                    objDocumento.idTransaccionPadre = 0;
                                    objDocumento.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                    objDocumento.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                    objDocumento.idTipoTransaccion = (decimal)EnumTipoTransaccion.DocDespacho;

                                    var datFechaDes = DateTime.Parse(values[25].Trim());
                                    var strFechaDes = datFechaDes.Year + datFechaDes.Month.ToString().PadLeft(2, '0') + datFechaDes.Day.ToString().PadLeft(2, '0') + datFechaDes.Hour.ToString().PadLeft(2, '0') + datFechaDes.Minute.ToString().PadLeft(2, '0') + datFechaDes.Second.ToString().PadLeft(2, '0');
                                    objDocumento.fechaEmision = Convert.ToInt64(strFechaDes);

                                    objDocumento.numeroTransaccion = values[26].Trim();
                                    objDocumento.numeroAutorizacion = Convert.ToDecimal(values[27].Trim());
                                    objDocumento.codigoControl = values[28].Trim();
                                    objDocumento.nitConsumidor = 0;
                                    objDocumento.razonSocial = "";
                                    objDocumento.precioUnitario = 0;
                                    objDocumento.precioTotal = 0;
                                    objDocumento.usuarioTransaccion = values[12].Trim();
                                    objDocumento.observaciones = "";
                                    objDocumento.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                    objDocumento.idEstadoTransaccion = estadoTra;
                                    objDocumento.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.No;
                                    objDocumento.idFormaPago = (decimal)EnumTiposPago.Efectivo;
                                    objDocumento.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                    objDocumento.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                    objDocumento.codigoSeguridad = 0;
                                    objDocumento.idAutorizacionDato = 0;
                                    objDocumento.binarioControl = controlBinario + "000000000";
                                    objDocumento.fechaOperacion = Convert.ToInt64(strFecha);
                                    objDocumento.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                    objDocumento.codigo = values[29].Trim();
                                    lstTransacciones.Add(objDocumento);
                                    controlBinario = 0;
                                }
                                if (values[21].Trim().Length > 0)
                                {
                                    objDocumento = new ObjTransaccion();
                                    objDocumento.idMovimiento = 0;
                                    objDocumento.idTransaccion = 0;
                                    objDocumento.idTransaccionPadre = 0;
                                    objDocumento.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                    objDocumento.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                    objDocumento.idTipoTransaccion = (decimal)EnumTipoTransaccion.NotaMovimiento;
                                    objDocumento.fechaEmision = Convert.ToInt64(strFecha);
                                    objDocumento.numeroTransaccion = values[21].Trim();
                                    objDocumento.numeroAutorizacion = 0;
                                    objDocumento.codigoControl = "";
                                    objDocumento.nitConsumidor = Convert.ToDecimal(values[23].Trim());
                                    objDocumento.razonSocial = values[22].Trim();
                                    objDocumento.precioUnitario = 0;
                                    objDocumento.precioTotal = 0;
                                    objDocumento.usuarioTransaccion = values[12].Trim();
                                    objDocumento.observaciones = "";
                                    objDocumento.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                    objDocumento.idEstadoTransaccion = estadoTra;
                                    objDocumento.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.No;
                                    objDocumento.idFormaPago = (decimal)EnumTiposPago.Efectivo;
                                    objDocumento.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                    objDocumento.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                    objDocumento.codigoSeguridad = 0;
                                    objDocumento.idAutorizacionDato = 0;
                                    objDocumento.binarioControl = controlBinario + "000000000";
                                    objDocumento.fechaOperacion = Convert.ToInt64(strFecha);
                                    objDocumento.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                    objDocumento.codigo = "";
                                    lstTransacciones.Add(objDocumento);
                                }

                                if (Convert.ToDecimal(values[24].Trim()) > 0)
                                {
                                    objDocumento = new ObjTransaccion();
                                    objDocumento.idMovimiento = 0;
                                    objDocumento.idTransaccion = 0;
                                    objDocumento.idTransaccionPadre = 0;
                                    objDocumento.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                    objDocumento.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                    objDocumento.idTipoTransaccion = (decimal)EnumTipoTransaccion.AutSustCtrl;
                                    objDocumento.fechaEmision = Convert.ToInt64(strFecha);
                                    objDocumento.numeroTransaccion = values[24].Trim();
                                    objDocumento.numeroAutorizacion = 0;
                                    objDocumento.codigoControl = "";
                                    objDocumento.nitConsumidor = 0;
                                    objDocumento.razonSocial = "";
                                    objDocumento.precioUnitario = 0;
                                    objDocumento.precioTotal = 0;
                                    objDocumento.usuarioTransaccion = values[12].Trim();
                                    objDocumento.observaciones = "AUTORIZACION DE COMPRA LOCAL";
                                    objDocumento.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                    objDocumento.idEstadoTransaccion = estadoTra;
                                    objDocumento.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.No;
                                    objDocumento.idFormaPago = (decimal)EnumTiposPago.Efectivo;
                                    objDocumento.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                    objDocumento.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                    objDocumento.codigoSeguridad = 0;
                                    objDocumento.idAutorizacionDato = 0;
                                    objDocumento.binarioControl = "0000000000";
                                    objDocumento.fechaOperacion = Convert.ToInt64(strFecha);
                                    objDocumento.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                    objDocumento.codigo = "";
                                    lstTransacciones.Add(objDocumento);
                                }

                                if (values[8].Trim().Length > 0)
                                {
                                    var arreglo = values[8].Trim().Split('|');
                                    for (int i = 0; i < arreglo.Count(); i++)
                                    {
                                        objDocumento = new ObjTransaccion();
                                        objDocumento.idMovimiento = 0;
                                        objDocumento.idTransaccion = 0;
                                        objDocumento.idTransaccionPadre = 0;
                                        objDocumento.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                        objDocumento.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                        objDocumento.idTipoTransaccion = (decimal)EnumTipoTransaccion.DocPrecinto;
                                        objDocumento.fechaEmision = Convert.ToInt64(strFecha);
                                        objDocumento.numeroTransaccion = arreglo[i];
                                        objDocumento.numeroAutorizacion = 0;
                                        objDocumento.codigoControl = "";
                                        objDocumento.nitConsumidor = 0;
                                        objDocumento.razonSocial = "";
                                        objDocumento.precioUnitario = 0;
                                        objDocumento.precioTotal = 0;
                                        objDocumento.usuarioTransaccion = values[12].Trim();
                                        objDocumento.observaciones = "";
                                        objDocumento.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                        objDocumento.idEstadoTransaccion = estadoTra;
                                        objDocumento.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.No;
                                        objDocumento.idFormaPago = (int)EnumTiposPago.Efectivo;
                                        objDocumento.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                        objDocumento.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                        objDocumento.codigoSeguridad = 0;
                                        objDocumento.idAutorizacionDato = 0;
                                        objDocumento.binarioControl = "0000000000";
                                        objDocumento.fechaOperacion = Convert.ToInt64(strFecha);
                                        objDocumento.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                        objDocumento.codigo = "";
                                        lstTransacciones.Add(objDocumento);
                                    }
                                }

                                objNuevoTipoReg = new ObjProtoMovimientoTransaccion();
                                objNuevoTipoReg.idMovimientoPadre = 0;
                                objNuevoTipoReg.idTipoActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                objNuevoTipoReg.idTipoOperacion = Convert.ToDecimal(cmbTipoOperacion.Value);
                                objNuevoTipoReg.idConsumidor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString());
                                objNuevoTipoReg.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                objNuevoTipoReg.fechaRegistroMovimiento = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString().PadLeft(2, '0') + datFechaReg.Minute.ToString().PadLeft(2, '0') + datFechaReg.Second.ToString().PadLeft(2, '0'));
                                objNuevoTipoReg.fechaOperacion = Convert.ToInt64(strFecha);
                                objNuevoTipoReg.idTipoMovimientoFlujo = (int)EnumTipoMovimientoFlujo.Fisico;
                                objNuevoTipoReg.idTipoMovimiento = (int)EnumTipoMovimiento.NacNac;
                                objNuevoTipoReg.idEstadoMovimiento = (cDato.StrEstado == "A") ? (int)EnumEstadoMov.Anulado : (int)EnumEstadoMov.Activo;
                                objNuevoTipoReg.idFuenteOrigenDato = (decimal)EnumTipoRegistro.OrigenCsv;
                                objNuevoTipoReg.idUbicacionOperacion = MCodificaParametrica((int)EnumTipoParametrica.Pais, "BOLIVIA");
                                objNuevoTipoReg.idUnidadMedidaHidrocarburo = MCodificaParametrica((int)EnumTipoParametrica.UnidadMedida, values[9].Trim());
                                objNuevoTipoReg.idHidrocarburo = MCodificaParametrica((int)EnumTipoParametrica.Producto, values[9].Trim());
                                objNuevoTipoReg.cantidadHidrocarburo = cDato.DecVolumen;
                                objNuevoTipoReg.identificador = cDato.StrIdentificador;
                                objNuevoTipoReg.subIdentificador = values[5].Trim();
                                objNuevoTipoReg.observacion = "";
                                objNuevoTipoReg.tipoInfraestructura = MCodificaParametrica((int)EnumTipoParametrica.TipoInfraestructura, values[2].Trim());
                                objNuevoTipoReg.idInfraestructura = MCodificaParametrica((int)EnumTipoParametrica.Infraestructura, values[2].Trim() + "|" + values[3].Trim());
                                objNuevoTipoReg.idTipoMedioTransporte = (values[7].Trim().Length == 0) ? -99999 : MCodificaParametrica((int)EnumTipoParametrica.TipoInfraestructura, "CS");
                                objNuevoTipoReg.idMedioTransporte = -99999;
                                objNuevoTipoReg.idUnidadEnvasesTransporte = -99999;
                                objNuevoTipoReg.idEntidad = MCodificaParametrica((int)EnumTipoParametrica.Entidad, values[1].Trim());
                                objNuevoTipoReg.codigo = values[7].Trim();
                                objNuevoTipoReg.idTipoMedicion = -99999;
                                objNuevoTipoReg.cantidadAguaLitros = 0;
                                objNuevoTipoReg.idUnidadTemperatura = -99999;
                                objNuevoTipoReg.temperatura = 0;
                                objNuevoTipoReg.gradoApi = "";
                                objNuevoTipoReg.cantidadEnvasesTransporte = 0;
                                objNuevoTipoReg.binarioControl = "00000-0000000000-0000000000-00000";
                                objNuevoTipoReg.idIntegracion = -99999;
                                objNuevoTipoReg.idTransaccionBaseExterna = "";
                                objNuevoTipoReg.fechaOperacionDia = 0;
                                objNuevoTipoReg.codigo = "";
                                objNuevoTipoReg.barcode = Convert.ToDecimal(values[6].Trim());
                                objNuevoTipoReg.listaTransaccion = JsonSerializer.SerializeToString(lstTransacciones);

                                objNuevoTipoReg.jsonReferencia = "";
                                objNuevoTipoReg.estadoJsonRef = -99999;
                                objNuevoTipoReg.idCuenta = -99999;
                                objNuevoTipoReg.idCuentaAux = -99999;
                                objNuevoTipoReg.idParamGlobal = -99999;
                                objNuevoTipoReg.idParamEspec = -99999;
                                objNuevoTipoReg.idTipoConsumidor = -99999;
                                objNuevoTipoReg.idTipoInfraAux = -99999;
                                objNuevoTipoReg.idInfraAux = -99999;

                                #endregion
                                break;
                        }
                        cDato.objProtoMovimiento = objNuevoTipoReg;
                        lstRegistros.Add(cDato);
                        lngNoRegCorrect++;
                    }
                    else
                    {
                        if (strTipoOp == "DS")
                        {
                            var cRegError = new CRegistrosError();
                            cRegError.LngId = lngNoReg;
                            cRegError.StrIdVenta = values[29].Trim();
                            cRegError.LngFechaEmision = values[0].Trim();
                            cRegError.LngNit = values[12].Trim();
                            cRegError.LngNroAutorizacion = values[15].Trim();
                            cRegError.LngNroFactura = values[14].Trim();
                            cRegError.LngNroReferencia = values[29].Trim();
                            cRegError.StrErrores = strMensaje.Trim();
                            lstRegistroE.Add(cRegError);
                            lngNoRegInCorrect++;
                        }
                        else
                        {
                            var cRegError = new CRegistrosError();
                            cRegError.LngId = lngNoReg;
                            cRegError.StrIdVenta = values[20].Trim();
                            cRegError.LngFechaEmision = values[0].Trim();
                            cRegError.LngNit = "";
                            cRegError.LngNroAutorizacion = values[16].Trim();
                            cRegError.LngNroFactura = values[15].Trim();
                            cRegError.LngNroReferencia = values[20].Trim();
                            cRegError.StrErrores = strMensaje.Trim();
                            lstRegistroE.Add(cRegError);
                            lngNoRegInCorrect++;
                        }
                    }
                }
                catch (Exception ex)
                {
                    var cRegError = new CRegistrosError();
                    cRegError.LngId = lngNoReg;
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
                lngNoReg++;
            }


            dgvTransacciones.DataSource = lstRegistros;
            dgvTransacciones.DataBind();
            Session[CVariablesSesion.GrillaTransaccion] = lstRegistros;

            dgvTransaccionError.DataSource = lstRegistroE;
            dgvTransaccionError.DataBind();
            Session[CVariablesSesion.GrillaRegObservado] = lstRegistroE;
        }

        /// <summary>
        /// METODO. MCargarRefinacion
        /// </summary>
        /// <param name="strRuta"></param>
        private void MCargarRefinacion_99999(string strRuta)
        {
            var reader = new StreamReader(File.OpenRead(@strRuta));
            var lstRegistros = new List<CMovimientoRefineria>();
            var lstRegistroE = new List<CRegistrosError>();
            long lngNoRegCorrect = 1, lngNoRegInCorrect = 1, lngNoReg = 1;
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

                    var to = cmbTipoOperacion.Text.Split(';');
                    var strTipoOp = to[1].Substring(2, 2);
                    string strMensaje = string.Empty;
                    if (MValidaLineaRefinerias(values, ref strMensaje))
                    {
                        var cDato = new CMovimientoRefineria();
                        int controlBinario;
                        switch (strTipoOp)
                        {
                            case "FI":
                                #region Valida y genera objeto
                                cDato.LngId = lngNoRegCorrect;

                                //cDato.DecFecha = Convert.ToDecimal();
                                cDato.DecFecha = CFechas.ConvierteDateTimeLong(Convert.ToDateTime(values[0].Trim()));
                                cDato.DecIdOrigen = Convert.ToDecimal(values[1].Trim());
                                cDato.StrTInfraestructura = Convert.ToString(values[2].Trim());
                                cDato.StrInfraestructura = Convert.ToString(values[3].Trim());
                                cDato.StrProyOrigen = Convert.ToString(values[4].Trim());
                                cDato.StrPais = Convert.ToString(values[5].Trim());
                                cDato.StrIdentificador = Convert.ToString(values[6].Trim());
                                cDato.StrLote = Convert.ToString(values[7].Trim());
                                cDato.StrDui = Convert.ToString(values[8].Trim());
                                cDato.StrNroRecinto = Convert.ToString(values[9].Trim());
                                cDato.StrCodigoTanqueCisterna = Convert.ToString(values[10].Trim());
                                cDato.DecNroFactura = Convert.ToDecimal(values[11].Trim());
                                cDato.DecNroAutorizacion = Convert.ToDecimal(values[12].Trim());
                                cDato.StrCodigoControl = Convert.ToString(values[13].Trim());
                                cDato.DecNit = Convert.ToDecimal(values[14].Trim());
                                cDato.StrRazonSocial = Convert.ToString(values[15].Trim());
                                cDato.StrNroReferencia = Convert.ToString(values[16].Trim());
                                cDato.StrCertificadoRecepcion = Convert.ToString(values[17].Trim());
                                cDato.StrCIUsuario = Convert.ToString(values[18].Trim());
                                cDato.StrEstado = Convert.ToString(values[19].Trim());
                                cDato.StrProducto = Convert.ToString(values[20].Trim());
                                cDato.DecCantidad = Convert.ToDecimal(values[21].Trim());
                                cDato.DecPrecio = Convert.ToDecimal(values[22].Trim());
                                cDato.DecImporte = Convert.ToDecimal(values[23].Trim());

                                datFecha = DateTime.Parse(values[0].Trim());

                                strFecha = datFecha.Year + datFecha.Month.ToString().PadLeft(2, '0') + datFecha.Day.ToString().PadLeft(2, '0') + datFecha.Hour.ToString().PadLeft(2, '0') + datFecha.Minute.ToString().PadLeft(2, '0') + datFecha.Second.ToString().PadLeft(2, '0');
                                datFechaReg = DateTime.Now;
                                lstTransacciones = new List<ObjTransaccion>();
                                controlBinario = 1;


                                objDocumento = new ObjTransaccion();
                                objDocumento.idMovimiento = 0;
                                objDocumento.idTransaccion = 0;
                                objDocumento.idTransaccionPadre = 0;
                                objDocumento.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                objDocumento.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                objDocumento.idTipoTransaccion = 3;
                                objDocumento.fechaEmision = Convert.ToInt64(strFecha);
                                objDocumento.numeroTransaccion = values[11].Trim();
                                objDocumento.numeroAutorizacion = Convert.ToDecimal(values[12]);
                                objDocumento.codigoControl = "";
                                objDocumento.nitConsumidor = Convert.ToDecimal(values[14].Trim());
                                objDocumento.razonSocial = Convert.ToString(values[15].Trim());
                                objDocumento.precioUnitario = Convert.ToDecimal(values[22].Trim());
                                objDocumento.precioTotal = Convert.ToDecimal(values[23].Trim());
                                objDocumento.usuarioTransaccion = values[18].Trim();
                                objDocumento.observaciones = "";
                                objDocumento.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                objDocumento.idEstadoTransaccion = 11;
                                objDocumento.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.No;
                                objDocumento.idFormaPago = 1;
                                objDocumento.idTipoMoneda = 1;
                                objDocumento.idTipoEmisor = 14;
                                objDocumento.codigoSeguridad = 0;
                                objDocumento.idAutorizacionDato = 0;
                                objDocumento.binarioControl = "1000000000";
                                objDocumento.fechaOperacion = Convert.ToInt64(strFecha);
                                objDocumento.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                objDocumento.codigo = "";
                                lstTransacciones.Add(objDocumento);

                                objNuevoTipoReg = new ObjProtoMovimientoTransaccion();
                                objNuevoTipoReg.idMovimientoPadre = 0;
                                objNuevoTipoReg.idTipoActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                objNuevoTipoReg.idTipoOperacion = Convert.ToDecimal(cmbTipoOperacion.Value);
                                objNuevoTipoReg.idConsumidor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString());
                                objNuevoTipoReg.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                objNuevoTipoReg.fechaRegistroMovimiento = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                objNuevoTipoReg.fechaOperacion = Convert.ToInt64(strFecha);
                                objNuevoTipoReg.idTipoMovimientoFlujo = (int)EnumTipoMovimientoFlujo.Fisico;
                                objNuevoTipoReg.idTipoMovimiento = (int)EnumTipoMovimiento.NacNac;
                                objNuevoTipoReg.idEstadoMovimiento = 11;
                                objNuevoTipoReg.idFuenteOrigenDato = (decimal)EnumTipoRegistro.OrigenCsv;
                                objNuevoTipoReg.idUbicacionOperacion = MCodificaParametrica((int)EnumTipoParametrica.Pais, values[5].Trim());
                                objNuevoTipoReg.idUnidadMedidaHidrocarburo = -99999;
                                objNuevoTipoReg.idHidrocarburo = MCodificaParametrica((int)EnumTipoParametrica.Producto, values[20].Trim());
                                objNuevoTipoReg.cantidadHidrocarburo = Convert.ToDecimal(values[21].Trim());
                                objNuevoTipoReg.identificador = "";
                                objNuevoTipoReg.subIdentificador = "";
                                objNuevoTipoReg.observacion = "";

                                objNuevoTipoReg.idTipoMedioTransporte = -99999;
                                objNuevoTipoReg.idMedioTransporte = -99999;
                                objNuevoTipoReg.tipoInfraestructura = MCodificaParametrica((int)EnumTipoParametrica.TipoInfraestructura, values[2].Trim());
                                objNuevoTipoReg.idInfraestructura = MCodificaParametrica((int)EnumTipoParametrica.Infraestructura, values[2].Trim() + "|" + values[3].Trim());


                                objNuevoTipoReg.idUnidadEnvasesTransporte = -99999;
                                objNuevoTipoReg.idEntidad = -99999;
                                objNuevoTipoReg.idTipoMedicion = -99999;
                                objNuevoTipoReg.cantidadAguaLitros = 0;
                                objNuevoTipoReg.idUnidadTemperatura = -99999;
                                objNuevoTipoReg.temperatura = 0;
                                objNuevoTipoReg.gradoApi = "";
                                objNuevoTipoReg.cantidadEnvasesTransporte = 0;
                                objNuevoTipoReg.binarioControl = "00000-0000000000-0000000000-00000";
                                objNuevoTipoReg.idIntegracion = -99999;
                                objNuevoTipoReg.idTransaccionBaseExterna = "";
                                objNuevoTipoReg.fechaOperacionDia = 0;
                                objNuevoTipoReg.codigo = "";
                                objNuevoTipoReg.barcode = -99999;
                                //objNuevoTipoReg.barcode = Convert.ToDecimal(values[5].Trim());
                                objNuevoTipoReg.listaTransaccion = JsonSerializer.SerializeToString(lstTransacciones);

                                objNuevoTipoReg.jsonReferencia = JsonSerializer.SerializeToString(cDato);
                                objNuevoTipoReg.estadoJsonRef = -99999;
                                objNuevoTipoReg.idCuenta = -99999;
                                objNuevoTipoReg.idCuentaAux = -99999;
                                objNuevoTipoReg.idParamGlobal = -99999;
                                objNuevoTipoReg.idParamEspec = -99999;
                                objNuevoTipoReg.idTipoConsumidor = -99999;
                                objNuevoTipoReg.idTipoInfraAux = -99999;
                                objNuevoTipoReg.idInfraAux = -99999;

                                #endregion
                                break;
                        }
                        cDato.objProtoMovimiento = objNuevoTipoReg;
                        lstRegistros.Add(cDato);
                        lngNoRegCorrect++;
                    }
                    else
                    {
                        var cRegError = new CRegistrosError();
                        cRegError.LngId = lngNoReg;
                        cRegError.StrIdVenta = "";
                        cRegError.LngFechaEmision = values[0].Trim();
                        cRegError.LngNit = values[12].Trim();
                        cRegError.LngNroAutorizacion = values[12].Trim();
                        cRegError.LngNroFactura = values[11].Trim();
                        cRegError.LngNroReferencia = values[16].Trim();
                        cRegError.StrErrores = strMensaje.Trim();
                        lstRegistroE.Add(cRegError);
                        lngNoRegInCorrect++;

                    }
                }
                catch (Exception ex)
                {
                    var cRegError = new CRegistrosError();
                    cRegError.LngId = lngNoReg;
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
                lngNoReg++;
            }


            dgvTransaccionRefineria.Visible = true;
            dgvTransaccionRefineria.DataSource = lstRegistros;
            dgvTransaccionRefineria.DataBind();

            //Likst<> listaTransaccion = 
            Session[CVariablesSesion.GrillaRefineria] = lstRegistros;

            //CargarGrillaTransaccion(lstRegistros);
            MmostrarColumnasGrillaTransacciones();

            dgvTransaccionError.DataSource = lstRegistroE;
            dgvTransaccionError.DataBind();
            Session[CVariablesSesion.GrillaRegObservado] = lstRegistroE;
        }

        private void MCargarRefinacion(string strRuta)
        {
            var reader = new StreamReader(File.OpenRead(@strRuta));
            var lstRegistros = new List<CRegistrosImportados>();
            var lstRegistroE = new List<CRegistrosError>();
            long lngNoRegCorrect = 1, lngNoRegInCorrect = 1, lngNoReg = 1;
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(';');
                try
                {
                    DateTime datFecha, datFechaReg;
                    string strFecha;
                    ObjTransaccion objFactura, objDocumento;
                    ObjProtoMovimientoTransaccion objNuevoTipoReg = null;
                    int estadoTra;
                    List<ObjTransaccion> lstTransacciones;
                    var to = cmbTipoOperacion.Text.Split(';');
                    var strTipoOp = to[1].Substring(2, 2);

                    string strMensaje = string.Empty;
                    if (MValidaLineaRefinerias(values, ref strMensaje))
                    {
                        var cDato = new CRegistrosImportados();
                        int controlBinario;
                        switch (strTipoOp)
                        {
                            case "RC":
                                #region Valida y genera objeto
                                cDato.LngId = lngNoRegCorrect;
                                cDato.StrIdVenta = lngNoRegCorrect.ToString();
                                datFecha = DateTime.Parse(values[0].Trim());
                                cDato.StrFechaEmision = datFecha.ToString();
                                cDato.StrTipoMov = "NAC";
                                cDato.StrEstado = values[13].Trim();
                                cDato.LngNit = Convert.ToInt64(values[14]);
                                cDato.StrRazonSocial = values[15].Trim().ToUpper().Replace("¥", "Ñ").Replace("Á", "A").Replace("É", "E").Replace("Í", "I").Replace("Ó", "O").Replace("Ú", "U").Replace("'", "").Replace("�", "Ñ");
                                cDato.StrRazonSocial = Regex.Replace(cDato.StrRazonSocial, @"\s+", " ");
                                cDato.StrIdentificador = values[7].Trim();
                                cDato.LngNroFactura = Convert.ToInt64(values[17]);
                                cDato.LngNroAutorizacion = Convert.ToInt64(values[18]);
                                cDato.StrNroReferencia = values[22].Trim();
                                cDato.LngNroAutVenta = 0;
                                cDato.StrProducto = values[11].Trim();
                                cDato.DecVolumen = Convert.ToDecimal(values[12]);
                                cDato.DecPrecio = Convert.ToDecimal(values[20]);
                                cDato.DecImporte = Convert.ToDecimal(values[21]);
                                cDato.DecIdTipoCreditoFiscal = (cDato.LngNroFactura > 0) ? (int)EnumTipoCredFiscal.Si : (int)EnumTipoCredFiscal.No;
                                cDato.DecIdTipoOp = Convert.ToDecimal(cmbTipoOperacion.Value);
                                cDato.DecOrden = 1;

                                strFecha = datFecha.Year + datFecha.Month.ToString().PadLeft(2, '0') + datFecha.Day.ToString().PadLeft(2, '0') + datFecha.Hour.ToString().PadLeft(2, '0') + datFecha.Minute.ToString().PadLeft(2, '0') + datFecha.Second.ToString().PadLeft(2, '0');
                                datFechaReg = DateTime.Now;
                                estadoTra = (cDato.StrEstado == "A") ? (int)EnumEstadoMov.Anulado : (int)EnumEstadoMov.Activo;
                                lstTransacciones = new List<ObjTransaccion>();

                                controlBinario = 1;
                                if (Convert.ToDecimal(values[17].Trim()) > 0)
                                {
                                    objFactura = new ObjTransaccion();
                                    objFactura.idMovimiento = 0;
                                    objFactura.idTransaccion = 0;
                                    objFactura.idTransaccionPadre = 0;
                                    objFactura.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                    objFactura.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                    objFactura.idTipoTransaccion = (decimal)((values[19].Trim().Length > 4) ? EnumTipoTransaccion.FacturaComputarizada : EnumTipoTransaccion.FacturaManual);
                                    objFactura.fechaEmision = Convert.ToInt64(strFecha);
                                    objFactura.numeroTransaccion = values[17].Trim();
                                    objFactura.numeroAutorizacion = Convert.ToDecimal(values[18].Trim());
                                    objFactura.codigoControl = values[19].Trim();
                                    objFactura.nitConsumidor = Convert.ToDecimal(values[14].Trim());
                                    objFactura.razonSocial = cDato.StrRazonSocial;
                                    objFactura.precioUnitario = Convert.ToDecimal(values[20].Trim());
                                    objFactura.precioTotal = Convert.ToDecimal(values[21].Trim());
                                    objFactura.usuarioTransaccion = values[24].Trim();
                                    objFactura.observaciones = "";
                                    objFactura.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                    objFactura.idEstadoTransaccion = estadoTra;
                                    objFactura.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.Si;
                                    objFactura.idFormaPago = (int)EnumTiposPago.Efectivo;
                                    objFactura.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                    objFactura.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                    objFactura.codigoSeguridad = 0;
                                    objFactura.idAutorizacionDato = 0;
                                    objFactura.binarioControl = controlBinario + "000000000";
                                    objFactura.fechaOperacion = Convert.ToInt64(strFecha);
                                    objFactura.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                    objFactura.codigo = "";
                                    lstTransacciones.Add(objFactura);
                                    controlBinario = 0;
                                }

                                if (values[22].Trim().Length > 0)
                                {
                                    objDocumento = new ObjTransaccion();
                                    objDocumento.idMovimiento = 0;
                                    objDocumento.idTransaccion = 0;
                                    objDocumento.idTransaccionPadre = 0;
                                    objDocumento.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                    objDocumento.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                    objDocumento.idTipoTransaccion = (decimal)EnumTipoTransaccion.NotaMovimiento;
                                    objDocumento.fechaEmision = Convert.ToInt64(strFecha);
                                    objDocumento.numeroTransaccion = values[22].Trim();
                                    objDocumento.numeroAutorizacion = 0;
                                    objDocumento.codigoControl = "";
                                    objDocumento.nitConsumidor = 0;
                                    objDocumento.razonSocial = "";
                                    objDocumento.precioUnitario = 0;
                                    objDocumento.precioTotal = 0;
                                    objDocumento.usuarioTransaccion = values[24].Trim();
                                    objDocumento.observaciones = "";
                                    objDocumento.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                    objDocumento.idEstadoTransaccion = estadoTra;
                                    objDocumento.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.No;
                                    objDocumento.idFormaPago = (decimal)EnumTiposPago.Efectivo;
                                    objDocumento.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                    objDocumento.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                    objDocumento.codigoSeguridad = 0;
                                    objDocumento.idAutorizacionDato = 0;
                                    objDocumento.binarioControl = controlBinario + "000000000";
                                    objDocumento.fechaOperacion = Convert.ToInt64(strFecha);
                                    objDocumento.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                    objDocumento.codigo = "";
                                    lstTransacciones.Add(objDocumento);
                                    controlBinario = 0;
                                }
                                //if (values[20].Trim().Length > 0)
                                //{
                                //    objDocumento = new ObjTransaccion();
                                //    objDocumento.idMovimiento = 0;
                                //    objDocumento.idTransaccion = 0;
                                //    objDocumento.idTransaccionPadre = 0;
                                //    objDocumento.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                //    objDocumento.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString().PadLeft(2, '0') + datFechaReg.Day.ToString().PadLeft(2, '0') + datFechaReg.Hour.ToString().PadLeft(2, '0') + datFechaReg.Minute.ToString().PadLeft(2, '0') + datFechaReg.Second.ToString().PadLeft(2, '0'));
                                //    objDocumento.idTipoTransaccion = (decimal)EnumTipoTransaccion.DocCre;
                                //    objDocumento.fechaEmision = Convert.ToInt64(strFecha);
                                //    objDocumento.numeroTransaccion = values[20].Trim();
                                //    objDocumento.numeroAutorizacion = 0;
                                //    objDocumento.codigoControl = "";
                                //    objDocumento.nitConsumidor = 0;
                                //    objDocumento.razonSocial = "";
                                //    objDocumento.precioUnitario = 0;
                                //    objDocumento.precioTotal = 0;
                                //    objDocumento.usuarioTransaccion = values[23].Trim();
                                //    objDocumento.observaciones = "";
                                //    objDocumento.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                //    objDocumento.idEstadoTransaccion = estadoTra;
                                //    objDocumento.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.No;
                                //    objDocumento.idFormaPago = (int)EnumTiposPago.Efectivo;
                                //    objDocumento.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                //    objDocumento.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                //    objDocumento.codigoSeguridad = 0;
                                //    objDocumento.idAutorizacionDato = 0;
                                //    objDocumento.binarioControl = controlBinario + "000000000";
                                //    objDocumento.fechaOperacion = Convert.ToInt64(strFecha);
                                //    objDocumento.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                //    objDocumento.codigo = "";
                                //    lstTransacciones.Add(objDocumento);
                                //}
                                //if (values[12].Trim().Length > 0)
                                //{
                                //    var arreglo = values[12].Trim().Split('|');
                                //    for (int i = 0; i < arreglo.Count(); i++)
                                //    {
                                //        objDocumento = new ObjTransaccion();
                                //        objDocumento.idMovimiento = 0;
                                //        objDocumento.idTransaccion = 0;
                                //        objDocumento.idTransaccionPadre = 0;
                                //        objDocumento.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                //        objDocumento.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                //        objDocumento.idTipoTransaccion = (decimal)EnumTipoTransaccion.DocPrecinto;
                                //        objDocumento.fechaEmision = Convert.ToInt64(strFecha);
                                //        objDocumento.numeroTransaccion = arreglo[i];
                                //        objDocumento.numeroAutorizacion = 0;
                                //        objDocumento.codigoControl = "";
                                //        objDocumento.nitConsumidor = 0;
                                //        objDocumento.razonSocial = "";
                                //        objDocumento.precioUnitario = 0;
                                //        objDocumento.precioTotal = 0;
                                //        objDocumento.usuarioTransaccion = values[23].Trim();
                                //        objDocumento.observaciones = "";
                                //        objDocumento.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                //        objDocumento.idEstadoTransaccion = estadoTra;
                                //        objDocumento.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.No;
                                //        objDocumento.idFormaPago = (int)EnumTiposPago.Efectivo;
                                //        objDocumento.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                //        objDocumento.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                //        objDocumento.codigoSeguridad = 0;
                                //        objDocumento.idAutorizacionDato = 0;
                                //        objDocumento.binarioControl = "0000000000";
                                //        objDocumento.fechaOperacion = Convert.ToInt64(strFecha);
                                //        objDocumento.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                //        objDocumento.codigo = "";
                                //        lstTransacciones.Add(objDocumento);
                                //    }
                                //}
                                //if (values[22].Trim().Length > 0)
                                //{
                                //    var arreglo = values[22].Trim().Split('|');
                                //    for (int i = 0; i < arreglo.Count(); i++)
                                //    {
                                //        objDocumento = new ObjTransaccion();
                                //        objDocumento.idMovimiento = 0;
                                //        objDocumento.idTransaccion = 0;
                                //        objDocumento.idTransaccionPadre = 0;
                                //        objDocumento.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                //        objDocumento.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString().PadLeft(2, '0') + datFechaReg.Day.ToString().PadLeft(2, '0') + datFechaReg.Hour.ToString().PadLeft(2, '0') + datFechaReg.Minute.ToString().PadLeft(2, '0') + datFechaReg.Second.ToString().PadLeft(2, '0'));
                                //        objDocumento.idTipoTransaccion = (decimal)EnumTipoTransaccion.DocCalidad;
                                //        objDocumento.fechaEmision = Convert.ToInt64(strFecha);
                                //        objDocumento.numeroTransaccion = arreglo[i];
                                //        objDocumento.numeroAutorizacion = 0;
                                //        objDocumento.codigoControl = "";
                                //        objDocumento.nitConsumidor = 0;
                                //        objDocumento.razonSocial = "";
                                //        objDocumento.precioUnitario = 0;
                                //        objDocumento.precioTotal = 0;
                                //        objDocumento.usuarioTransaccion = values[23].Trim();
                                //        objDocumento.observaciones = "";
                                //        objDocumento.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                //        objDocumento.idEstadoTransaccion = estadoTra;
                                //        objDocumento.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.No;
                                //        objDocumento.idFormaPago = (int)EnumTiposPago.Efectivo;
                                //        objDocumento.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                //        objDocumento.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                //        objDocumento.codigoSeguridad = 0;
                                //        objDocumento.idAutorizacionDato = 0;
                                //        objDocumento.binarioControl = "0000000000";
                                //        objDocumento.fechaOperacion = Convert.ToInt64(strFecha);
                                //        objDocumento.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                //        objDocumento.codigo = "";
                                //        lstTransacciones.Add(objDocumento);
                                //    }
                                //}
                                if (values[8].Trim().Length > 0)
                                {
                                    objDocumento = new ObjTransaccion();
                                    objDocumento.idMovimiento = 0;
                                    objDocumento.idTransaccion = 0;
                                    objDocumento.idTransaccionPadre = 0;
                                    objDocumento.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                    objDocumento.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString().PadLeft(2, '0') + datFechaReg.Day.ToString().PadLeft(2, '0') + datFechaReg.Hour.ToString().PadLeft(2, '0') + datFechaReg.Minute.ToString().PadLeft(2, '0') + datFechaReg.Second.ToString().PadLeft(2, '0'));
                                    objDocumento.idTipoTransaccion = (decimal)EnumTipoTransaccion.DocLote;
                                    objDocumento.fechaEmision = Convert.ToInt64(strFecha);
                                    objDocumento.numeroTransaccion = values[8].Trim();
                                    objDocumento.numeroAutorizacion = 0;
                                    objDocumento.codigoControl = "";
                                    objDocumento.nitConsumidor = 0;
                                    objDocumento.razonSocial = "";
                                    objDocumento.precioUnitario = 0;
                                    objDocumento.precioTotal = 0;
                                    objDocumento.usuarioTransaccion = values[24].Trim();
                                    objDocumento.observaciones = "";
                                    objDocumento.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                    objDocumento.idEstadoTransaccion = estadoTra;
                                    objDocumento.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.No;
                                    objDocumento.idFormaPago = (int)EnumTiposPago.Efectivo;
                                    objDocumento.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                    objDocumento.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                    objDocumento.codigoSeguridad = 0;
                                    objDocumento.idAutorizacionDato = 0;
                                    objDocumento.binarioControl = "0000000000";
                                    objDocumento.fechaOperacion = Convert.ToInt64(strFecha);
                                    objDocumento.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                    objDocumento.codigo = "";
                                    lstTransacciones.Add(objDocumento);
                                }

                                //if (Convert.ToDecimal(values[21].Trim()) > 0)
                                //{
                                //    objDocumento = new ObjTransaccion();
                                //    objDocumento.idMovimiento = 0;
                                //    objDocumento.idTransaccion = 0;
                                //    objDocumento.idTransaccionPadre = 0;
                                //    objDocumento.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                //    objDocumento.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString().PadLeft(2, '0') + datFechaReg.Day.ToString().PadLeft(2, '0') + datFechaReg.Hour.ToString().PadLeft(2, '0') + datFechaReg.Minute.ToString().PadLeft(2, '0') + datFechaReg.Second.ToString().PadLeft(2, '0'));
                                //    objDocumento.idTipoTransaccion = (decimal)EnumTipoTransaccion.AutSustCtrl;
                                //    objDocumento.fechaEmision = Convert.ToInt64(strFecha);
                                //    objDocumento.numeroTransaccion = values[21].Trim();
                                //    objDocumento.numeroAutorizacion = 0;
                                //    objDocumento.codigoControl = "";
                                //    objDocumento.nitConsumidor = 0;
                                //    objDocumento.razonSocial = "";
                                //    objDocumento.precioUnitario = 0;
                                //    objDocumento.precioTotal = 0;
                                //    objDocumento.usuarioTransaccion = values[23].Trim();
                                //    objDocumento.observaciones = "";
                                //    objDocumento.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                //    objDocumento.idEstadoTransaccion = estadoTra;
                                //    objDocumento.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.No;
                                //    objDocumento.idFormaPago = (int)EnumTiposPago.Efectivo;
                                //    objDocumento.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                //    objDocumento.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                //    objDocumento.codigoSeguridad = 0;
                                //    objDocumento.idAutorizacionDato = 0;
                                //    objDocumento.binarioControl = "0000000000";
                                //    objDocumento.fechaOperacion = Convert.ToInt64(strFecha);
                                //    objDocumento.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                //    objDocumento.codigo = "";
                                //    lstTransacciones.Add(objDocumento);
                                //}
                                objNuevoTipoReg = new ObjProtoMovimientoTransaccion();
                                objNuevoTipoReg.idMovimientoPadre = 0;
                                objNuevoTipoReg.idTipoActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                objNuevoTipoReg.idTipoOperacion = Convert.ToDecimal(cmbTipoOperacion.Value);
                                objNuevoTipoReg.idConsumidor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString());
                                objNuevoTipoReg.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                objNuevoTipoReg.fechaRegistroMovimiento = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                objNuevoTipoReg.fechaOperacion = Convert.ToInt64(strFecha);
                                objNuevoTipoReg.idTipoMovimientoFlujo = (int)EnumTipoMovimientoFlujo.Fisico;//L,l > Consultar 
                                objNuevoTipoReg.idTipoMovimiento = (int)EnumTipoMovimiento.NacNac;
                                objNuevoTipoReg.idEstadoMovimiento = estadoTra;
                                objNuevoTipoReg.idFuenteOrigenDato = (decimal)EnumTipoRegistro.OrigenCsv;
                                objNuevoTipoReg.idUbicacionOperacion = (decimal)EnumPais.BOLIVIA;
                                objNuevoTipoReg.idUnidadMedidaHidrocarburo = -99999;
                                objNuevoTipoReg.idHidrocarburo = MCodificaParametrica((int)EnumTipoParametrica.Producto, values[11].Trim());
                                objNuevoTipoReg.cantidadHidrocarburo = cDato.DecVolumen;
                                objNuevoTipoReg.identificador = cDato.StrIdentificador;
                                objNuevoTipoReg.subIdentificador = "";
                                objNuevoTipoReg.observacion = "";
                                objNuevoTipoReg.tipoInfraestructura = MCodificaParametrica((int)EnumTipoParametrica.TipoInfraestructura, values[2].Trim());
                                objNuevoTipoReg.idInfraestructura = MCodificaParametrica((int)EnumTipoParametrica.Infraestructura, values[2].Trim() + "|" + values[3].Trim());
                                objNuevoTipoReg.idTipoMedicion = -99999;
                                objNuevoTipoReg.cantidadAguaLitros = 0;
                                objNuevoTipoReg.idUnidadTemperatura = (decimal)EnumUnidadTemperatura.GradoCentigrado;
                                objNuevoTipoReg.temperatura = 0;
                                objNuevoTipoReg.gradoApi = "";
                                objNuevoTipoReg.idEntidad = MCodificaParametrica((int)EnumTipoParametrica.Entidad, values[1].Trim());
                                objNuevoTipoReg.idTipoMedioTransporte = MCodificaParametrica((int)EnumTipoParametrica.TipoInfraestructura, values[4].Trim());
                                objNuevoTipoReg.idMedioTransporte = MCodificaParametrica((int)EnumTipoParametrica.Infraestructura, values[4].Trim() + "|" + values[5].Trim());
                                objNuevoTipoReg.idUnidadEnvasesTransporte = -99999;
                                objNuevoTipoReg.cantidadEnvasesTransporte = 0;
                                objNuevoTipoReg.binarioControl = "00000-0000000000-0000000000-00000";
                                objNuevoTipoReg.idIntegracion = -99999;
                                objNuevoTipoReg.idTransaccionBaseExterna = "";
                                objNuevoTipoReg.codigo = values[10].Trim();
                                objNuevoTipoReg.barcode = 0;
                                objNuevoTipoReg.listaTransaccion = JsonSerializer.SerializeToString(lstTransacciones);

                                objNuevoTipoReg.jsonReferencia = "";
                                objNuevoTipoReg.estadoJsonRef = -99999;
                                objNuevoTipoReg.idCuenta = -99999;
                                objNuevoTipoReg.idCuentaAux = -99999;
                                objNuevoTipoReg.idParamGlobal = -99999;
                                objNuevoTipoReg.idParamEspec = -99999;
                                objNuevoTipoReg.idTipoConsumidor = -99999;
                                objNuevoTipoReg.idTipoInfraAux = -99999;
                                objNuevoTipoReg.idInfraAux = -99999;

                                #endregion
                                break;
                            case "DS":
                                #region Valida y genera objeto
                                cDato.LngId = lngNoRegCorrect;
                                cDato.StrIdVenta = lngNoRegCorrect.ToString();
                                datFecha = DateTime.Parse(values[0].Trim());
                                cDato.StrFechaEmision = datFecha.ToString();
                                cDato.StrTipoMov = "NAC";
                                cDato.StrEstado = values[13].Trim();
                                cDato.LngNit = Convert.ToInt64(values[14]);
                                cDato.StrRazonSocial = values[15].Trim().ToUpper().Replace("¥", "Ñ").Replace("Á", "A").Replace("É", "E").Replace("Í", "I").Replace("Ó", "O").Replace("Ú", "U").Replace("'", "").Replace("�", "Ñ");
                                cDato.StrRazonSocial = Regex.Replace(cDato.StrRazonSocial, @"\s+", " ");
                                cDato.StrIdentificador = values[7].Trim().Replace("-", "").Replace(" ", "");
                                cDato.LngNroFactura = Convert.ToInt64(values[17]);
                                cDato.LngNroAutorizacion = Convert.ToInt64(values[18]);
                                cDato.StrNroReferencia = values[22].Trim();
                                cDato.LngNroAutVenta = 0;
                                cDato.StrProducto = values[11].Trim();
                                cDato.DecVolumen = Convert.ToDecimal(values[12]);
                                cDato.DecPrecio = Convert.ToDecimal(values[20]);
                                cDato.DecImporte = Convert.ToDecimal(values[21]);
                                cDato.DecIdTipoCreditoFiscal = (cDato.LngNroFactura > 0) ? (int)EnumTipoCredFiscal.Si : (int)EnumTipoCredFiscal.No;
                                cDato.DecIdTipoOp = Convert.ToDecimal(cmbTipoOperacion.Value);
                                cDato.DecOrden = 1;

                                strFecha = datFecha.Year + datFecha.Month.ToString().PadLeft(2, '0') + datFecha.Day.ToString().PadLeft(2, '0') + datFecha.Hour.ToString().PadLeft(2, '0') + datFecha.Minute.ToString().PadLeft(2, '0') + datFecha.Second.ToString().PadLeft(2, '0');
                                datFechaReg = DateTime.Now;
                                estadoTra = (cDato.StrEstado == "A") ? (int)EnumEstadoMov.Anulado : (int)EnumEstadoMov.Activo;
                                lstTransacciones = new List<ObjTransaccion>();
                                controlBinario = 1;

                                if (Convert.ToDecimal(values[17].Trim()) > 0)
                                {
                                    objFactura = new ObjTransaccion();
                                    objFactura.idMovimiento = 0;
                                    objFactura.idTransaccion = 0;
                                    objFactura.idTransaccionPadre = 0;
                                    objFactura.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                    objFactura.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString().PadLeft(2, '0'));
                                    objFactura.idTipoTransaccion = (decimal)((values[19].Trim().Length > 4) ? EnumTipoTransaccion.FacturaComputarizada : EnumTipoTransaccion.FacturaManual);
                                    objFactura.fechaEmision = Convert.ToInt64(strFecha);
                                    objFactura.numeroTransaccion = values[17].Trim();
                                    objFactura.numeroAutorizacion = Convert.ToDecimal(values[18].Trim());
                                    objFactura.codigoControl = values[19].Trim();
                                    objFactura.nitConsumidor = Convert.ToDecimal(values[14].Trim());
                                    objFactura.razonSocial = cDato.StrRazonSocial;
                                    objFactura.precioUnitario = Convert.ToDecimal(values[20].Trim());
                                    objFactura.precioTotal = Convert.ToDecimal(values[21].Trim());
                                    objFactura.usuarioTransaccion = values[24].Trim();
                                    objFactura.observaciones = "";
                                    objFactura.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                    objFactura.idEstadoTransaccion = estadoTra;
                                    objFactura.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.Si;
                                    objFactura.idFormaPago = (int)EnumTiposPago.Efectivo;
                                    objFactura.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                    objFactura.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                    objFactura.codigoSeguridad = 0;
                                    objFactura.idAutorizacionDato = 0;
                                    objFactura.binarioControl = controlBinario + "000000000";
                                    objFactura.fechaOperacion = Convert.ToInt64(strFecha);
                                    objFactura.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                    objFactura.codigo = "";
                                    lstTransacciones.Add(objFactura);
                                    controlBinario = 0;
                                }

                                if (values[22].Trim().Length > 0)
                                {
                                    objDocumento = new ObjTransaccion();
                                    objDocumento.idMovimiento = 0;
                                    objDocumento.idTransaccion = 0;
                                    objDocumento.idTransaccionPadre = 0;
                                    objDocumento.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                    objDocumento.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                    objDocumento.idTipoTransaccion = (decimal)EnumTipoTransaccion.NotaMovimiento;
                                    objDocumento.fechaEmision = Convert.ToInt64(strFecha);
                                    objDocumento.numeroTransaccion = values[22].Trim();
                                    objDocumento.numeroAutorizacion = 0;
                                    objDocumento.codigoControl = "";
                                    objDocumento.nitConsumidor = 0;
                                    objDocumento.razonSocial = "";
                                    objDocumento.precioUnitario = 0;
                                    objDocumento.precioTotal = 0;
                                    objDocumento.usuarioTransaccion = values[24].Trim();
                                    objDocumento.observaciones = "";
                                    objDocumento.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                    objDocumento.idEstadoTransaccion = estadoTra;
                                    objDocumento.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.No;
                                    objDocumento.idFormaPago = (decimal)EnumTiposPago.Efectivo;
                                    objDocumento.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                    objDocumento.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                    objDocumento.codigoSeguridad = 0;
                                    objDocumento.idAutorizacionDato = 0;
                                    objDocumento.binarioControl = controlBinario + "000000000";
                                    objDocumento.fechaOperacion = Convert.ToInt64(strFecha);
                                    objDocumento.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                    objDocumento.codigo = "";
                                    lstTransacciones.Add(objDocumento);
                                    controlBinario = 0;
                                }

                                //if (values[20].Trim().Length > 0)
                                //{
                                //    objDocumento = new ObjTransaccion();
                                //    objDocumento.idMovimiento = 0;
                                //    objDocumento.idTransaccion = 0;
                                //    objDocumento.idTransaccionPadre = 0;
                                //    objDocumento.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                //    objDocumento.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                //    objDocumento.idTipoTransaccion = (decimal)EnumTipoTransaccion.AutAasana;
                                //    objDocumento.fechaEmision = Convert.ToInt64(strFecha);
                                //    objDocumento.numeroTransaccion = values[20].Trim();
                                //    objDocumento.numeroAutorizacion = 0;
                                //    objDocumento.codigoControl = "";
                                //    objDocumento.nitConsumidor = Convert.ToDecimal(values[22].Trim());
                                //    objDocumento.razonSocial = values[21].Trim().ToUpper().Replace("¥", "Ñ").Replace("Á", "A").Replace("É", "E").Replace("Í", "I").Replace("Ó", "O").Replace("Ú", "U").Replace("'", "").Replace("�", "Ñ");
                                //    objDocumento.razonSocial = Regex.Replace(cDato.StrRazonSocial, @"\s+", " ");
                                //    objDocumento.precioUnitario = 0;
                                //    objDocumento.precioTotal = 0;
                                //    objDocumento.usuarioTransaccion = values[26].Trim();
                                //    objDocumento.observaciones = "";
                                //    objDocumento.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                //    objDocumento.idEstadoTransaccion = estadoTra;
                                //    objDocumento.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.No;
                                //    objDocumento.idFormaPago = (decimal)EnumTiposPago.Efectivo;
                                //    objDocumento.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                //    objDocumento.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                //    objDocumento.codigoSeguridad = 0;
                                //    objDocumento.idAutorizacionDato = 0;
                                //    objDocumento.binarioControl = controlBinario + "000000000";
                                //    objDocumento.fechaOperacion = Convert.ToInt64(strFecha);
                                //    objDocumento.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                //    objDocumento.codigo = "";
                                //    lstTransacciones.Add(objDocumento);
                                //}

                                var decIdTipoDespacho = 33;
                                //if (Convert.ToDecimal(values[23].Trim()) > 0)
                                //{
                                //    objDocumento = new ObjTransaccion();
                                //    objDocumento.idMovimiento = 0;
                                //    objDocumento.idTransaccion = 0;
                                //    objDocumento.idTransaccionPadre = 0;
                                //    objDocumento.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                //    objDocumento.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                //    objDocumento.idTipoTransaccion = (decimal)EnumTipoTransaccion.AutSustCtrl;
                                //    objDocumento.fechaEmision = Convert.ToInt64(strFecha);
                                //    objDocumento.numeroTransaccion = values[23].Trim();
                                //    objDocumento.numeroAutorizacion = 0;
                                //    objDocumento.codigoControl = "";
                                //    objDocumento.nitConsumidor = 0;
                                //    objDocumento.razonSocial = "";
                                //    objDocumento.precioUnitario = 0;
                                //    objDocumento.precioTotal = 0;
                                //    objDocumento.usuarioTransaccion = values[26].Trim();
                                //    objDocumento.observaciones = "";
                                //    objDocumento.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                //    objDocumento.idEstadoTransaccion = estadoTra;
                                //    objDocumento.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.No;
                                //    objDocumento.idFormaPago = (decimal)EnumTiposPago.Efectivo;
                                //    objDocumento.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                //    objDocumento.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                //    objDocumento.codigoSeguridad = 0;
                                //    objDocumento.idAutorizacionDato = 0;
                                //    objDocumento.binarioControl = "0000000000";
                                //    objDocumento.fechaOperacion = Convert.ToInt64(strFecha);
                                //    objDocumento.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                //    objDocumento.codigo = "";
                                //    lstTransacciones.Add(objDocumento);
                                //    decIdTipoDespacho = 34;
                                //}
                                //if (values[24].Trim().Length > 0)
                                //{
                                //    objDocumento = new ObjTransaccion();
                                //    objDocumento.idMovimiento = 0;
                                //    objDocumento.idTransaccion = 0;
                                //    objDocumento.idTransaccionPadre = 0;
                                //    objDocumento.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                //    objDocumento.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                //    objDocumento.idTipoTransaccion = (decimal)EnumTipoTransaccion.AutAnhVfa;
                                //    objDocumento.fechaEmision = Convert.ToInt64(strFecha);
                                //    objDocumento.numeroTransaccion = values[24].Trim();
                                //    objDocumento.numeroAutorizacion = 0;
                                //    objDocumento.codigoControl = "";
                                //    objDocumento.nitConsumidor = 0;
                                //    objDocumento.razonSocial = "";
                                //    objDocumento.precioUnitario = 0;
                                //    objDocumento.precioTotal = 0;
                                //    objDocumento.usuarioTransaccion = values[26].Trim();
                                //    objDocumento.observaciones = "";
                                //    objDocumento.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                //    objDocumento.idEstadoTransaccion = estadoTra;
                                //    objDocumento.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.No;
                                //    objDocumento.idFormaPago = (decimal)EnumTiposPago.Efectivo;
                                //    objDocumento.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                //    objDocumento.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                //    objDocumento.codigoSeguridad = 0;
                                //    objDocumento.idAutorizacionDato = 0;
                                //    objDocumento.binarioControl = "0000000000";
                                //    objDocumento.fechaOperacion = Convert.ToInt64(strFecha);
                                //    objDocumento.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                //    objDocumento.codigo = "";
                                //    lstTransacciones.Add(objDocumento);
                                //    decIdTipoDespacho = 34;
                                //}
                                //if (values[25].Trim().Length > 0)
                                //{
                                //    var arreglo = values[25].Trim().Split('|');
                                //    for (int i = 0; i < arreglo.Count(); i++)
                                //    {
                                //        objDocumento = new ObjTransaccion();
                                //        objDocumento.idMovimiento = 0;
                                //        objDocumento.idTransaccion = 0;
                                //        objDocumento.idTransaccionPadre = 0;
                                //        objDocumento.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                //        objDocumento.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                //        objDocumento.idTipoTransaccion = (decimal)EnumTipoTransaccion.DocCalidad;
                                //        objDocumento.fechaEmision = Convert.ToInt64(strFecha);
                                //        objDocumento.numeroTransaccion = arreglo[i];
                                //        objDocumento.numeroAutorizacion = 0;
                                //        objDocumento.codigoControl = "";
                                //        objDocumento.nitConsumidor = 0;
                                //        objDocumento.razonSocial = "";
                                //        objDocumento.precioUnitario = 0;
                                //        objDocumento.precioTotal = 0;
                                //        objDocumento.usuarioTransaccion = values[26].Trim();
                                //        objDocumento.observaciones = "";
                                //        objDocumento.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                //        objDocumento.idEstadoTransaccion = estadoTra;
                                //        objDocumento.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.No;
                                //        objDocumento.idFormaPago = (int)EnumTiposPago.Efectivo;
                                //        objDocumento.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                //        objDocumento.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                //        objDocumento.codigoSeguridad = 0;
                                //        objDocumento.idAutorizacionDato = 0;
                                //        objDocumento.binarioControl = "0000000000";
                                //        objDocumento.fechaOperacion = Convert.ToInt64(strFecha);
                                //        objDocumento.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                //        objDocumento.codigo = "";
                                //        lstTransacciones.Add(objDocumento);
                                //    }
                                //}

                                objNuevoTipoReg = new ObjProtoMovimientoTransaccion();
                                objNuevoTipoReg.idMovimientoPadre = 0;
                                objNuevoTipoReg.idTipoActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                objNuevoTipoReg.idTipoOperacion = Convert.ToDecimal(cmbTipoOperacion.Value);
                                objNuevoTipoReg.idConsumidor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString());
                                objNuevoTipoReg.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                objNuevoTipoReg.fechaRegistroMovimiento = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                objNuevoTipoReg.fechaOperacion = Convert.ToInt64(strFecha);
                                objNuevoTipoReg.idTipoMovimientoFlujo = (int)EnumTipoMovimientoFlujo.Fisico;
                                objNuevoTipoReg.idTipoMovimiento = (int)EnumTipoMovimiento.NacNac;
                                objNuevoTipoReg.idEstadoMovimiento = estadoTra;
                                objNuevoTipoReg.idFuenteOrigenDato = (decimal)EnumTipoRegistro.OrigenCsv;
                                objNuevoTipoReg.idUbicacionOperacion = (decimal)EnumPais.BOLIVIA;
                                objNuevoTipoReg.idUnidadMedidaHidrocarburo = -99999;
                                objNuevoTipoReg.idHidrocarburo = MCodificaParametrica((int)EnumTipoParametrica.Producto, values[11].Trim());
                                objNuevoTipoReg.cantidadHidrocarburo = cDato.DecVolumen;
                                objNuevoTipoReg.identificador = cDato.StrIdentificador;
                                objNuevoTipoReg.subIdentificador = "";
                                objNuevoTipoReg.observacion = "";
                                objNuevoTipoReg.tipoInfraestructura = MCodificaParametrica((int)EnumTipoParametrica.TipoInfraestructura, values[2].Trim());
                                objNuevoTipoReg.idInfraestructura = MCodificaParametrica((int)EnumTipoParametrica.Infraestructura, values[2].Trim() + "|" + values[3].Trim());
                                objNuevoTipoReg.idTipoMedicion = -99999;
                                objNuevoTipoReg.cantidadAguaLitros = 0;
                                objNuevoTipoReg.idUnidadTemperatura = (decimal)EnumUnidadTemperatura.GradoCentigrado;
                                objNuevoTipoReg.temperatura = 0;
                                objNuevoTipoReg.gradoApi = "";
                                objNuevoTipoReg.idEntidad = MCodificaParametrica((int)EnumTipoParametrica.Entidad, values[1].Trim());
                                objNuevoTipoReg.idTipoMedioTransporte = MCodificaParametrica((int)EnumTipoParametrica.TipoInfraestructura, values[4].Trim());
                                objNuevoTipoReg.idMedioTransporte = MCodificaParametrica((int)EnumTipoParametrica.Infraestructura, values[4].Trim() + "|" + values[5].Trim());
                                objNuevoTipoReg.idUnidadEnvasesTransporte = -99999;
                                objNuevoTipoReg.cantidadEnvasesTransporte = 0;
                                objNuevoTipoReg.binarioControl = "00000-0000000000-0000000000-00000";
                                objNuevoTipoReg.idIntegracion = -99999;
                                objNuevoTipoReg.idTransaccionBaseExterna = "";
                                objNuevoTipoReg.codigo = values[10].Trim();
                                decimal dec;
                                objNuevoTipoReg.barcode = (decimal.TryParse(values[11].Trim(), out dec)) ? Convert.ToDecimal(values[11].Trim()) : 0;
                                objNuevoTipoReg.listaTransaccion = JsonSerializer.SerializeToString(lstTransacciones);

                                objNuevoTipoReg.jsonReferencia = "";
                                objNuevoTipoReg.estadoJsonRef = -99999;
                                objNuevoTipoReg.idCuenta = -99999;
                                objNuevoTipoReg.idCuentaAux = -99999;
                                objNuevoTipoReg.idParamGlobal = -99999;
                                objNuevoTipoReg.idParamEspec = -99999;
                                objNuevoTipoReg.idTipoConsumidor = decIdTipoDespacho;
                                objNuevoTipoReg.idTipoInfraAux = -99999;
                                objNuevoTipoReg.idInfraAux = -99999;

                                #endregion
                                break;
                        }
                        cDato.objProtoMovimiento = objNuevoTipoReg;
                        lstRegistros.Add(cDato);
                        lngNoRegCorrect++;
                    }
                    else
                    {
                        var cRegError = new CRegistrosError();
                        cRegError.LngId = lngNoReg;
                        cRegError.StrIdVenta = lngNoRegCorrect.ToString();
                        cRegError.LngFechaEmision = values[0].Trim();
                        cRegError.LngNit = values[14].Trim();
                        cRegError.LngNroAutorizacion = values[18].Trim();
                        cRegError.LngNroFactura = values[17].Trim();
                        cRegError.LngNroReferencia = values[22].Trim();
                        cRegError.StrErrores = strMensaje.Trim();
                        lstRegistroE.Add(cRegError);
                        lngNoRegInCorrect++;
                    }
                }
                catch (Exception ex)
                {
                    var cRegError = new CRegistrosError();
                    cRegError.LngId = lngNoReg;
                    cRegError.StrIdVenta = lngNoRegCorrect.ToString();
                    cRegError.LngFechaEmision = "";
                    cRegError.LngNit = "";
                    cRegError.LngNroAutorizacion = "";
                    cRegError.LngNroFactura = "";
                    cRegError.LngNroReferencia = "";
                    cRegError.StrErrores = "ERROR:" + ex.Message;
                    lstRegistroE.Add(cRegError);
                    lngNoRegInCorrect++;
                }
                lngNoReg++;
            }

            //var ojo2 = JsonSerializer.SerializeToString(lstRegistros);
            dgvTransacciones.DataSource = lstRegistros;
            dgvTransacciones.DataBind();
            Session[CVariablesSesion.GrillaTransaccion] = lstRegistros;

            dgvTransaccionError.DataSource = lstRegistroE;
            dgvTransaccionError.DataBind();
            Session[CVariablesSesion.GrillaRegObservado] = lstRegistroE;
        }
        private void MCargarTransporteDuctos(string strRuta)
        {
            var reader = new StreamReader(File.OpenRead(@strRuta));
            var lstRegistros = new List<CMovimientoTransporteDucto>();
            var lstRegistroE = new List<CRegistrosError>();
            long lngNoRegCorrect = 1, lngNoRegInCorrect = 1, lngNoReg = 1;
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

                    var to = cmbTipoOperacion.Text.Split(';');
                    var strTipoOp = to[1].Substring(2, 2);
                    string strMensaje = string.Empty;
                    if (MValidaLineaTransporteDuctos(values, ref strMensaje))
                    {
                        var cDato = new CMovimientoTransporteDucto();
                        int controlBinario;
                        switch (strTipoOp)
                        {
                            case "DE":
                                #region Valida y genera objeto
                                cDato.LngId = lngNoRegCorrect;

                                //cDato.DecFecha = Convert.ToDecimal();
                                cDato.StrFecha = Convert.ToString(values[0].Trim());//CFechas.ConvierteDateTimeLong(Convert.ToDateTime(values[0].Trim()));
                                cDato.StrDucto = Convert.ToString(values[1].Trim());
                                cDato.StrTransaccion = Convert.ToString(values[2].Trim());
                                cDato.StrPoi = Convert.ToString(values[3].Trim());
                                cDato.StrNombre = Convert.ToString(values[4].Trim());
                                cDato.StrPoiActual = Convert.ToString(values[5].Trim());
                                cDato.StrCargador = Convert.ToString(values[6].Trim());
                                cDato.StrUsuario = Convert.ToString(values[7].Trim());
                                cDato.StrLocalidad = Convert.ToString(values[8].Trim());
                                cDato.StrDepartamento = Convert.ToString(values[9].Trim());
                                cDato.StrProducto = Convert.ToString(values[10].Trim());
                                cDato.StrVolumen = Convert.ToString(values[11].Trim());
                                cDato.StrSistema = Convert.ToString(values[12].Trim());
                                cDato.StrPoderCalorifico = Convert.ToString(values[13].Trim());
                                cDato.StrTipoTransporte = Convert.ToString(values[14].Trim());

                                var strEntero = Convert.ToString(values[11].Trim().Split(',')[0]);
                                var strdecimal = "0," + Convert.ToString(values[11].Trim().Split(',')[1]);

                                datFecha = DateTime.Parse(values[0].Trim());

                                strFecha = datFecha.Year + datFecha.Month.ToString().PadLeft(2, '0') + datFecha.Day.ToString().PadLeft(2, '0') + datFecha.Hour.ToString().PadLeft(2, '0') + datFecha.Minute.ToString().PadLeft(2, '0') + datFecha.Second.ToString().PadLeft(2, '0');
                                datFechaReg = DateTime.Now;
                                lstTransacciones = new List<ObjTransaccion>();
                                controlBinario = 1;



                                objDocumento = new ObjTransaccion();
                                objDocumento.idMovimiento = 0;
                                objDocumento.idTransaccion = 0;
                                objDocumento.idTransaccionPadre = 0;
                                objDocumento.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                objDocumento.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                objDocumento.idTipoTransaccion = MCodificaParametrica((int)EnumTipoParametrica.PoiEntrega, values[2].Trim());
                                objDocumento.fechaEmision = Convert.ToInt64(strFecha);
                                objDocumento.numeroTransaccion = "0";
                                objDocumento.numeroAutorizacion = -99999;
                                objDocumento.codigoControl = "";
                                objDocumento.nitConsumidor = 0;
                                objDocumento.razonSocial = "";
                                objDocumento.precioUnitario = 0;
                                objDocumento.precioTotal = 0;
                                objDocumento.usuarioTransaccion = Convert.ToString(values[7].Trim());
                                objDocumento.observaciones = "";
                                objDocumento.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                objDocumento.idEstadoTransaccion = 11;
                                objDocumento.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.No;
                                objDocumento.idFormaPago = 1;
                                objDocumento.idTipoMoneda = 1;
                                objDocumento.idTipoEmisor = 14;
                                objDocumento.codigoSeguridad = 0;
                                objDocumento.idAutorizacionDato = 0;
                                objDocumento.binarioControl = "1000000000";
                                objDocumento.fechaOperacion = Convert.ToInt64(strFecha);
                                objDocumento.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                objDocumento.codigo = "";
                                lstTransacciones.Add(objDocumento);

                                objNuevoTipoReg = new ObjProtoMovimientoTransaccion();
                                objNuevoTipoReg.idMovimientoPadre = 0;
                                objNuevoTipoReg.idTipoActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                objNuevoTipoReg.idTipoOperacion = Convert.ToDecimal(cmbTipoOperacion.Value);
                                objNuevoTipoReg.idConsumidor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString());
                                objNuevoTipoReg.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                objNuevoTipoReg.fechaRegistroMovimiento = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                objNuevoTipoReg.fechaOperacion = Convert.ToInt64(strFecha);
                                objNuevoTipoReg.idTipoMovimientoFlujo = (int)EnumTipoMovimientoFlujo.Fisico;
                                objNuevoTipoReg.idTipoMovimiento = (int)EnumTipoMovimiento.NacNac;
                                objNuevoTipoReg.idEstadoMovimiento = 11;
                                objNuevoTipoReg.idFuenteOrigenDato = (decimal)EnumTipoRegistro.OrigenCsv;
                                objNuevoTipoReg.idUbicacionOperacion = (decimal)EnumPais.BOLIVIA;
                                objNuevoTipoReg.idUnidadMedidaHidrocarburo = -99999;
                                objNuevoTipoReg.idHidrocarburo = MCodificaParametrica((int)EnumTipoParametrica.Producto, values[10].Trim());
                                objNuevoTipoReg.cantidadHidrocarburo = Convert.ToDecimal(Convert.ToDecimal(strEntero) / 1000000);
                                objNuevoTipoReg.identificador = "";
                                objNuevoTipoReg.subIdentificador = "";
                                objNuevoTipoReg.observacion = "";

                                objNuevoTipoReg.idTipoMedioTransporte = -99999;
                                objNuevoTipoReg.idMedioTransporte = -99999;
                                objNuevoTipoReg.tipoInfraestructura = -99999;
                                objNuevoTipoReg.idInfraestructura = MCodificaParametrica((int)EnumTipoParametrica.CodigoPoi, values[3].Trim());

                                objNuevoTipoReg.idUnidadEnvasesTransporte = -99999;
                                objNuevoTipoReg.idEntidad = -99999;
                                objNuevoTipoReg.idTipoMedicion = -99999;
                                objNuevoTipoReg.cantidadAguaLitros = Convert.ToDecimal(strdecimal.Trim().Replace(',', '.'));
                                objNuevoTipoReg.idUnidadTemperatura = -99999;
                                objNuevoTipoReg.temperatura = 0;
                                objNuevoTipoReg.gradoApi = "";
                                objNuevoTipoReg.cantidadEnvasesTransporte = 0;
                                objNuevoTipoReg.binarioControl = "00000-0000000000-0000000000-00000";
                                objNuevoTipoReg.idIntegracion = -99999;
                                objNuevoTipoReg.idTransaccionBaseExterna = "";
                                objNuevoTipoReg.fechaOperacionDia = 0;
                                objNuevoTipoReg.codigo = "";
                                objNuevoTipoReg.barcode = -99999;
                                //objNuevoTipoReg.barcode = Convert.ToDecimal(values[5].Trim());
                                objNuevoTipoReg.listaTransaccion = JsonSerializer.SerializeToString(lstTransacciones);

                                objNuevoTipoReg.jsonReferencia = JsonSerializer.SerializeToString(cDato);
                                objNuevoTipoReg.estadoJsonRef = -99999;
                                objNuevoTipoReg.idCuenta = -99999;
                                objNuevoTipoReg.idCuentaAux = -99999;
                                objNuevoTipoReg.idParamGlobal = -99999;
                                objNuevoTipoReg.idParamEspec = -99999;
                                objNuevoTipoReg.idTipoConsumidor = -99999;
                                objNuevoTipoReg.idTipoInfraAux = -99999;
                                objNuevoTipoReg.idInfraAux = -99999;

                                #endregion
                                break;

                            case "DR":
                                #region Valida y genera objeto
                                cDato.LngId = lngNoRegCorrect;

                                //cDato.DecFecha = Convert.ToDecimal();
                                cDato.StrFecha = Convert.ToString(values[0].Trim());//CFechas.ConvierteDateTimeLong(Convert.ToDateTime(values[0].Trim()));
                                cDato.StrDucto = Convert.ToString(values[1].Trim());
                                cDato.StrTransaccion = Convert.ToString(values[2].Trim());
                                cDato.StrPoi = Convert.ToString(values[3].Trim());
                                cDato.StrNombre = Convert.ToString(values[4].Trim());
                                cDato.StrPoiActual = Convert.ToString(values[5].Trim());
                                cDato.StrCargador = Convert.ToString(values[6].Trim());
                                cDato.StrUsuario = Convert.ToString(values[7].Trim());
                                cDato.StrLocalidad = Convert.ToString(values[8].Trim());
                                cDato.StrDepartamento = Convert.ToString(values[9].Trim());
                                cDato.StrProducto = Convert.ToString(values[10].Trim());
                                cDato.StrVolumen = Convert.ToString(values[11].Trim());
                                cDato.StrSistema = Convert.ToString(values[12].Trim());
                                cDato.StrPoderCalorifico = Convert.ToString(values[13].Trim());
                                cDato.StrTipoTransporte = Convert.ToString(values[14].Trim());

                                datFecha = DateTime.Parse(values[0].Trim());

                                strFecha = datFecha.Year + datFecha.Month.ToString().PadLeft(2, '0') + datFecha.Day.ToString().PadLeft(2, '0') + datFecha.Hour.ToString().PadLeft(2, '0') + datFecha.Minute.ToString().PadLeft(2, '0') + datFecha.Second.ToString().PadLeft(2, '0');
                                datFechaReg = DateTime.Now;
                                lstTransacciones = new List<ObjTransaccion>();
                                controlBinario = 1;

                                objDocumento = new ObjTransaccion();
                                objDocumento.idMovimiento = 0;
                                objDocumento.idTransaccion = 0;
                                objDocumento.idTransaccionPadre = 0;
                                objDocumento.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                objDocumento.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                objDocumento.idTipoTransaccion = MCodificaParametrica((int)EnumTipoParametrica.PoiRecepcion, values[2].Trim());
                                objDocumento.fechaEmision = Convert.ToInt64(strFecha);
                                objDocumento.numeroTransaccion = "0";
                                objDocumento.numeroAutorizacion = -99999;
                                objDocumento.codigoControl = "";
                                objDocumento.nitConsumidor = 0;
                                objDocumento.razonSocial = "";
                                objDocumento.precioUnitario = 0;
                                objDocumento.precioTotal = 0;
                                objDocumento.usuarioTransaccion = Convert.ToString(values[7].Trim());
                                objDocumento.observaciones = "";
                                objDocumento.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                objDocumento.idEstadoTransaccion = 11;
                                objDocumento.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.No;
                                objDocumento.idFormaPago = 1;
                                objDocumento.idTipoMoneda = 1;
                                objDocumento.idTipoEmisor = 14;
                                objDocumento.codigoSeguridad = 0;
                                objDocumento.idAutorizacionDato = 0;
                                objDocumento.binarioControl = "1000000000";
                                objDocumento.fechaOperacion = Convert.ToInt64(strFecha);
                                objDocumento.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                objDocumento.codigo = "";
                                lstTransacciones.Add(objDocumento);

                                objNuevoTipoReg = new ObjProtoMovimientoTransaccion();
                                objNuevoTipoReg.idMovimientoPadre = 0;
                                objNuevoTipoReg.idTipoActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                objNuevoTipoReg.idTipoOperacion = Convert.ToDecimal(cmbTipoOperacion.Value);
                                objNuevoTipoReg.idConsumidor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString());
                                objNuevoTipoReg.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                objNuevoTipoReg.fechaRegistroMovimiento = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                objNuevoTipoReg.fechaOperacion = Convert.ToInt64(strFecha);
                                objNuevoTipoReg.idTipoMovimientoFlujo = (int)EnumTipoMovimientoFlujo.Fisico;
                                objNuevoTipoReg.idTipoMovimiento = (int)EnumTipoMovimiento.NacNac;
                                objNuevoTipoReg.idEstadoMovimiento = 11;
                                objNuevoTipoReg.idFuenteOrigenDato = (decimal)EnumTipoRegistro.OrigenCsv;
                                objNuevoTipoReg.idUbicacionOperacion = (decimal)EnumPais.BOLIVIA;
                                objNuevoTipoReg.idUnidadMedidaHidrocarburo = -99999;
                                objNuevoTipoReg.idHidrocarburo = MCodificaParametrica((int)EnumTipoParametrica.Producto, values[10].Trim());
                                objNuevoTipoReg.cantidadHidrocarburo = Convert.ToDecimal(values[11].Trim());
                                objNuevoTipoReg.identificador = "";
                                objNuevoTipoReg.subIdentificador = "";
                                objNuevoTipoReg.observacion = "";

                                objNuevoTipoReg.idTipoMedioTransporte = -99999;
                                objNuevoTipoReg.idMedioTransporte = -99999;
                                objNuevoTipoReg.tipoInfraestructura = -99999;
                                objNuevoTipoReg.idInfraestructura = MCodificaParametrica((int)EnumTipoParametrica.CodigoPoi, values[3].Trim());

                                objNuevoTipoReg.idUnidadEnvasesTransporte = -99999;
                                objNuevoTipoReg.idEntidad = -99999;
                                objNuevoTipoReg.idTipoMedicion = -99999;
                                objNuevoTipoReg.cantidadAguaLitros = 0;
                                objNuevoTipoReg.idUnidadTemperatura = -99999;
                                objNuevoTipoReg.temperatura = 0;
                                objNuevoTipoReg.gradoApi = "";
                                objNuevoTipoReg.cantidadEnvasesTransporte = 0;
                                objNuevoTipoReg.binarioControl = "00000-0000000000-0000000000-00000";
                                objNuevoTipoReg.idIntegracion = -99999;
                                objNuevoTipoReg.idTransaccionBaseExterna = "";
                                objNuevoTipoReg.fechaOperacionDia = 0;
                                objNuevoTipoReg.codigo = "";
                                objNuevoTipoReg.barcode = -99999;
                                //objNuevoTipoReg.barcode = Convert.ToDecimal(values[5].Trim());
                                objNuevoTipoReg.listaTransaccion = JsonSerializer.SerializeToString(lstTransacciones);

                                objNuevoTipoReg.jsonReferencia = JsonSerializer.SerializeToString(cDato);
                                objNuevoTipoReg.estadoJsonRef = -99999;
                                objNuevoTipoReg.idCuenta = -99999;
                                objNuevoTipoReg.idCuentaAux = -99999;
                                objNuevoTipoReg.idParamGlobal = -99999;
                                objNuevoTipoReg.idParamEspec = -99999;
                                objNuevoTipoReg.idTipoConsumidor = -99999;
                                objNuevoTipoReg.idTipoInfraAux = -99999;
                                objNuevoTipoReg.idInfraAux = -99999;

                                #endregion
                                break;
                        }
                        cDato.objProtoMovimiento = objNuevoTipoReg;
                        lstRegistros.Add(cDato);
                        lngNoRegCorrect++;
                    }
                    else
                    {
                        var cRegError = new CRegistrosError();
                        cRegError.LngId = lngNoReg;
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
                    cRegError.LngId = lngNoReg;
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
                lngNoReg++;
            }


            dgvTransTransporteDuctos.Visible = true;
            dgvTransTransporteDuctos.DataSource = lstRegistros;
            dgvTransTransporteDuctos.DataBind();

            //Likst<> listaTransaccion = 
            Session[CVariablesSesion.GrillaTransporteDucto] = lstRegistros;

            //CargarGrillaTransaccion(lstRegistros);
            //MmostrarColumnasGrillaTransacciones();

            dgvTransaccionError.DataSource = lstRegistroE;
            dgvTransaccionError.DataBind();
            Session[CVariablesSesion.GrillaRegObservado] = lstRegistroE;
        }

        private void MCargarRedesGas(string strRuta)
        {
            var reader = new StreamReader(File.OpenRead(@strRuta));
            var lstRegistros = new List<CMovimientoRedesGas>();
            var lstRegistroE = new List<CRegistrosError>();
            long lngNoRegCorrect = 1, lngNoRegInCorrect = 1, lngNoReg = 1;

            CultureInfo culture = new CultureInfo("en-US");

            NumberFormatInfo nfi = new CultureInfo("en-US", true).NumberFormat;
            nfi.NegativeSign = "(-)";
            nfi.CurrencyDecimalDigits = 4;
            nfi.NumberDecimalDigits = 3;

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

                    var to = cmbTipoOperacion.Text.Split(';');
                    var strTipoOp = to[1].Substring(2, 2);
                    string strMensaje = string.Empty;
                    decimal decTipoTrans = 0;
                    if (MValidaLineaRedesGas(values, ref strMensaje))
                    {
                        var cDato = new CMovimientoRedesGas();
                        int controlBinario;
                        switch (strTipoOp)
                        {
                            case "NV":
                                #region Valida y genera objeto
                                cDato.LngId = lngNoRegCorrect;

                                /* cDato.StrFecha =  Convert.ToString(values[0].Trim());
                                 cDato.DecLecMedidor = Convert.ToDecimal(values[1].Trim());
                                 cDato.StrProducto = Convert.ToString(values[2].Trim());
                                 cDato.DecCantLectura = Convert.ToDecimal(values[3].Trim());
                                 cDato.DecCantFactura = Convert.ToDecimal(values[4].Trim());
                                 cDato.DecFactorCorrec = Convert.ToDecimal(values[5].Trim());
                                 cDato.DecTemperatura = Convert.ToDecimal(values[6].Trim());
                                 cDato.DecPresionPsig = Convert.ToDecimal(values[7].Trim());
                                 cDato.StrEstado = Convert.ToString(values[8].Trim());
                                 cDato.StrCategoria = Convert.ToString(values[9].Trim());
                                 cDato.StrSectorEcon = Convert.ToString(values[10].Trim());
                                 cDato.StrCodUbicacion = Convert.ToString(values[11].Trim());
                                 cDato.StrCodUsuario = Convert.ToString(values[12].Trim());
                                 cDato.DecCodMedidor = Convert.ToDecimal(values[13].Trim());
                                 cDato.StrFormaPago = Convert.ToString(values[14].Trim());
                                 cDato.DecNit = Convert.ToDecimal(values[15].Trim());
                                 cDato.StrRazonSocial = Convert.ToString(values[16].Trim());
                                 cDato.StrDireccion = Convert.ToString(values[17].Trim());
                                 cDato.DecNroFactura = Convert.ToDecimal(values[18].Trim());
                                 cDato.DecNroAutorizacion = Convert.ToDecimal(values[19].Trim());
                                 cDato.StrCodigoControl = Convert.ToString(values[20].Trim());
                                 cDato.StrFechaFactura = Convert.ToString(values[21].Trim());
                                 cDato.DecPrecio = Convert.ToDecimal(values[22].Trim());
                                 cDato.DecImporte = Convert.ToDecimal(values[23].Trim());
                                 cDato.DecNroComprobante = Convert.ToDecimal(values[24].Trim());
                                 cDato.StrResponsableMedicion = Convert.ToString(values[25].Trim());
                                 cDato.StrCiResponMed = Convert.ToString(values[26].Trim());
                                 cDato.DecNroReferencia = Convert.ToDecimal(values[27].Trim());
                                 cDato.StrTipoRefAdicional = Convert.ToString(values[28].Trim());
                                 cDato.DecImportAdicional = Convert.ToDecimal(values[29].Trim());
                                 cDato.StrCiudadPoblacion = Convert.ToString(values[30].Trim());
                                 */
                                cDato.StrCodUsuario = Convert.ToString(values[0].Trim());
                                cDato.StrCodUbicacion = Convert.ToString(values[1].Trim());
                                cDato.StrCiudadPoblacion = Convert.ToString(values[2].Trim().ToUpper());
                                cDato.StrCategoria = Convert.ToString(values[3].Trim());
                                cDato.DecNit = Convert.ToDecimal(values[4].Trim());
                                cDato.StrRazonSocial = values[5].Trim().ToUpper().Replace("¥", "Ñ").Replace("Á", "A").Replace("É", "E").Replace("Í", "I").Replace("Ó", "O").Replace("Ú", "U").Replace("'", "").Replace("�", "Ñ");
                                cDato.StrDireccion = values[6].Trim().ToUpper().Replace("¥", "Ñ").Replace("Á", "A").Replace("É", "E").Replace("Í", "I").Replace("Ó", "O").Replace("Ú", "U").Replace("'", "").Replace("�", "Ñ").Replace("I�", "IO");
                                cDato.StrSectorEcon = Convert.ToString(values[7].Trim());
                                cDato.StrProducto = "GN";

                                //cDato.StrTipoMedidor = Convert.ToString(values[8].Trim());

                                cDato.DecCodMedidor = values[8].Trim().ToUpper();//Convert.ToDecimal(values[8].Trim());
                                cDato.StrFecha = Convert.ToString(values[9].Trim());
                                cDato.DecLecMedidor = Convert.ToDecimal(values[10].Trim());
                                //cDato.DecCantNoCorregido = Convert.ToDecimal(values[11]);
                                //cDato.DecTemperatura = Convert.ToDecimal(values[12]);
                                //cDato.DecPresionPsig = Convert.ToDecimal(values[13]);
                                //cDato.DecFactorCorrec = Convert.ToDecimal(values[14]);
                                cDato.DecCantFactura = Convert.ToDecimal(values[11]);//(Convert.ToDecimal(values[11])*Convert.ToDecimal(values[14]))/1000 ;// Convert.ToDecimal(values[16]);
                                cDato.DecPrecio = 0;//Convert.ToDecimal(values[16].Trim());
                                cDato.DecImporte = Convert.ToDecimal(values[15]);//( ((Convert.ToDecimal(values[11])*Convert.ToDecimal(values[14]))/1000  )*Convert.ToDecimal(values[16])) + Convert.ToDecimal(values[18]); // Convert.ToDecimal(values[18]); 

                                cDato.DecImportAdicional = Convert.ToDecimal(values[12]);
                                cDato.DecImportConsumo = Convert.ToDecimal(values[12]);
                                cDato.DecImportCargo = Convert.ToDecimal(values[13]);
                                cDato.DecImportDescuento = Convert.ToDecimal(values[14]);

                                cDato.DecNroFactura = Convert.ToDecimal(values[16]);                                
                                cDato.DecNroAutorizacion = esnumero(values[17].Trim()) == true ? Convert.ToDecimal(values[17]) : 0;//adicion par el CUF;
                                cDato.codigoAutorizacion = esnumero(values[17].Trim()) == true ? "" : values[17].Trim().ToUpper();
                                cDato.StrCodigoControl = Convert.ToString(values[18].Trim());
                                cDato.StrFechaFactura = Convert.ToString(values[19].Trim());

                                datFecha = DateTime.Parse(values[19].Trim());
                                strFecha = datFecha.Year + datFecha.Month.ToString().PadLeft(2, '0') + datFecha.Day.ToString().PadLeft(2, '0') + datFecha.Hour.ToString().PadLeft(2, '0') + datFecha.Minute.ToString().PadLeft(2, '0') + datFecha.Second.ToString().PadLeft(2, '0');
                                datFechaReg = DateTime.Now;
                                lstTransacciones = new List<ObjTransaccion>();
                                controlBinario = 1;

                                /* objDocumento = new ObjTransaccion();
                                 objDocumento.idMovimiento = 0;
                                 objDocumento.idTransaccion = 0;
                                 objDocumento.idTransaccionPadre = 0;
                                 objDocumento.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                 objDocumento.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                 objDocumento.idTipoTransaccion = (decimal)((values[20].Trim().Length > 0) ? EnumTipoTransaccion.FacturaComputarizada : EnumTipoTransaccion.FacturaManual);
                                 objDocumento.fechaEmision = Convert.ToInt64(strFecha);
                                 objDocumento.numeroTransaccion = Convert.ToString(values[18].Trim());
                                 objDocumento.numeroAutorizacion = Convert.ToDecimal(values[19].Trim());
                                 objDocumento.codigoControl = Convert.ToString(values[20].Trim());
                                 objDocumento.nitConsumidor = Convert.ToDecimal(values[15].Trim());
                                 objDocumento.razonSocial = Convert.ToString(values[16].Trim());
                                 objDocumento.precioUnitario = Convert.ToDecimal(values[22].Trim());
                                 objDocumento.precioTotal = Convert.ToDecimal(values[23].Trim());
                                 objDocumento.usuarioTransaccion = Convert.ToString(values[25].Trim());
                                 objDocumento.observaciones = "";
                                 objDocumento.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                 objDocumento.idEstadoTransaccion = 11;
                                 objDocumento.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.No;
                                 objDocumento.idFormaPago = 1;
                                 objDocumento.idTipoMoneda = 1;
                                 objDocumento.idTipoEmisor = 14;
                                 objDocumento.codigoSeguridad = 0;
                                 objDocumento.idAutorizacionDato = 0;
                                 objDocumento.binarioControl = "1000000000";
                                 objDocumento.fechaOperacion = Convert.ToInt64(strFecha);
                                 objDocumento.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                 objDocumento.codigo = "";
                                 lstTransacciones.Add(objDocumento);*/

                                objDocumento = new ObjTransaccion();
                                objDocumento.idMovimiento = 0;
                                objDocumento.idTransaccion = 0;
                                objDocumento.idTransaccionPadre = 0;
                                objDocumento.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                objDocumento.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                //objDocumento.idTipoTransaccion = (decimal)((values[18].Trim().Length > 0) ? EnumTipoTransaccion.FacturaComputarizada : EnumTipoTransaccion.FacturaManual);

                                //objDocumento.idTipoTransaccion = (decimal)((values[18].Trim().Length > 0) ? EnumTipoTransaccion.FacturaComputarizada : EnumTipoTransaccion.FacturaManual);                                
                                decTipoTrans = (decimal)((cDato.codigoAutorizacion.Length > 25 || values[18].Trim().Length > 0) ? EnumTipoTransaccion.FacturaComputarizada : EnumTipoTransaccion.FacturaManual);
                            
                                if (((Convert.ToDecimal(values[4].Trim()) == 0) && (Convert.ToDecimal(values[17].Trim()) == 0) && (Convert.ToDecimal(values[18].Trim().Length) == 0)))
                                {
                                    decTipoTrans = (decimal)EnumTipoTransaccion.NotaMovimiento;
                                }

                                objDocumento.idTipoTransaccion = decTipoTrans;//(decimal)((Convert.ToDecimal(values[16].Trim()) == 0) ? EnumTipoTransaccion.NotaMovimiento : (values[18].Trim().Length > 0) ? EnumTipoTransaccion.FacturaComputarizada : EnumTipoTransaccion.FacturaManual);

                                objDocumento.fechaEmision = Convert.ToInt64(strFecha);
                                objDocumento.numeroTransaccion = Convert.ToString(values[16].Trim());                                
                                objDocumento.numeroAutorizacion  = esnumero(values[17].Trim()) == true ? Convert.ToDecimal(values[17]) : 0;//adicion par el CUF;
                                objDocumento.codigoAutorizacion = esnumero(values[17].Trim()) == true ? "" : values[17].Trim().ToUpper();
                                objDocumento.codigoControl = Convert.ToString(values[18].Trim());
                                objDocumento.nitConsumidor = Convert.ToDecimal(values[4].Trim());
                                objDocumento.razonSocial = values[5].Trim().ToUpper().Replace("¥", "Ñ").Replace("Á", "A").Replace("É", "E").Replace("Í", "I").Replace("Ó", "O").Replace("Ú", "U").Replace("'", "").Replace("�", "Ñ").Replace("I�", "IO");
                                objDocumento.precioUnitario = 0;//Convert.ToDecimal(values[16].Trim());
                                objDocumento.precioTotal = Convert.ToDecimal(values[15].Trim());//(((Convert.ToDecimal(values[11]) * Convert.ToDecimal(values[14])) / 1000) * Convert.ToDecimal(values[16])) + Convert.ToDecimal(values[18]);
                                // objDocumento.usuarioTransaccion = Convert.ToString(values[25].Trim());
                                objDocumento.observaciones = "";
                                objDocumento.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                objDocumento.idEstadoTransaccion = (int)EnumEstadoMov.Activo; 
                                objDocumento.idTipoCreditoFiscal = (Convert.ToDecimal(values[16].Trim()) == 0) ? (int)EnumTipoCredFiscal.No : (int)EnumTipoCredFiscal.Si;
                                objDocumento.idFormaPago = 1;
                                objDocumento.idTipoMoneda = 1;
                                objDocumento.idTipoEmisor = 14;
                                objDocumento.codigoSeguridad = 0;
                                objDocumento.idAutorizacionDato = 0;
                                objDocumento.binarioControl = "1000000000";
                                objDocumento.fechaOperacion = Convert.ToInt64(strFecha);
                                objDocumento.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                objDocumento.codigo = "";
                                lstTransacciones.Add(objDocumento);

                                /* objNuevoTipoReg = new ObjProtoMovimientoTransaccion();
                                 objNuevoTipoReg.idMovimientoPadre = 0;
                                 objNuevoTipoReg.idTipoActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                 objNuevoTipoReg.idTipoOperacion = Convert.ToDecimal(cmbTipoOperacion.Value);
                                 objNuevoTipoReg.idConsumidor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString());
                                 objNuevoTipoReg.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                 objNuevoTipoReg.fechaRegistroMovimiento = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                 objNuevoTipoReg.fechaOperacion = Convert.ToInt64(strFecha);
                                 objNuevoTipoReg.idTipoMovimientoFlujo = (int)EnumTipoMovimientoFlujo.Fisico;
                                 objNuevoTipoReg.idTipoMovimiento = (int)EnumTipoMovimiento.NacNac;
                                 objNuevoTipoReg.idEstadoMovimiento = 11;
                                 objNuevoTipoReg.idFuenteOrigenDato = (decimal)EnumTipoRegistro.OrigenCsv;
                                 objNuevoTipoReg.idUbicacionOperacion = (decimal)EnumPais.BOLIVIA;
                                 objNuevoTipoReg.idUnidadMedidaHidrocarburo = -99999;
                                 objNuevoTipoReg.idHidrocarburo = MCodificaParametrica((int)EnumTipoParametrica.Producto, values[2].Trim());
                                 objNuevoTipoReg.cantidadHidrocarburo = Convert.ToDecimal(values[3].Trim());
                                 objNuevoTipoReg.identificador = "";
                                 objNuevoTipoReg.subIdentificador = "";
                                 objNuevoTipoReg.observacion = "";*/

                                objNuevoTipoReg = new ObjProtoMovimientoTransaccion();
                                objNuevoTipoReg.idMovimientoPadre = 0;
                                objNuevoTipoReg.idTipoActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                objNuevoTipoReg.idTipoOperacion = Convert.ToDecimal(cmbTipoOperacion.Value);
                                objNuevoTipoReg.idConsumidor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString());
                                objNuevoTipoReg.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                objNuevoTipoReg.fechaRegistroMovimiento = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                objNuevoTipoReg.fechaOperacion = Convert.ToInt64(strFecha);
                                objNuevoTipoReg.idTipoMovimientoFlujo = (int)EnumTipoMovimientoFlujo.Fisico;
                                objNuevoTipoReg.idTipoMovimiento = (int)EnumTipoMovimiento.NacNac;
                                objNuevoTipoReg.idEstadoMovimiento = 11;
                                objNuevoTipoReg.idFuenteOrigenDato = (decimal)EnumTipoRegistro.OrigenCsv;
                                objNuevoTipoReg.idUbicacionOperacion = (decimal)EnumPais.BOLIVIA;
                                objNuevoTipoReg.idUnidadMedidaHidrocarburo = -99999;
                                objNuevoTipoReg.idHidrocarburo = MCodificaParametrica((int)EnumTipoParametrica.Producto, "GN");
                                objNuevoTipoReg.cantidadHidrocarburo = Convert.ToDecimal(values[11]);//(Convert.ToDecimal(values[12])*Convert.ToDecimal(values[15]))/1000; //Convert.ToDecimal(values[16].Trim());
                                objNuevoTipoReg.identificador = values[0].Trim();
                                objNuevoTipoReg.subIdentificador = ""; //values[3].Trim();
                                objNuevoTipoReg.observacion = "";


                                objNuevoTipoReg.idTipoMedioTransporte = -99999;
                                objNuevoTipoReg.idMedioTransporte = -99999;
                                objNuevoTipoReg.tipoInfraestructura = -99999;
                                objNuevoTipoReg.idInfraestructura = -99999;

                                objNuevoTipoReg.idUnidadEnvasesTransporte = -99999;
                                objNuevoTipoReg.idEntidad = -99999;
                                objNuevoTipoReg.idTipoMedicion = -99999;
                                objNuevoTipoReg.cantidadAguaLitros = 0;
                                objNuevoTipoReg.idUnidadTemperatura = -99999;
                                objNuevoTipoReg.temperatura = 0;
                                objNuevoTipoReg.gradoApi = "";
                                objNuevoTipoReg.cantidadEnvasesTransporte = 0;
                                objNuevoTipoReg.binarioControl = "00000-0000000000-0000000000-00000";
                                objNuevoTipoReg.idIntegracion = -99999;
                                objNuevoTipoReg.idTransaccionBaseExterna = "";
                                objNuevoTipoReg.fechaOperacionDia = 0;
                                objNuevoTipoReg.codigo = "";
                                objNuevoTipoReg.barcode = -99999;
                                //objNuevoTipoReg.barcode = Convert.ToDecimal(values[5].Trim());
                                objNuevoTipoReg.listaTransaccion = JsonSerializer.SerializeToString(lstTransacciones);

                                objNuevoTipoReg.jsonReferencia = JsonSerializer.SerializeToString(cDato);
                                objNuevoTipoReg.estadoJsonRef = -99999;
                                objNuevoTipoReg.idCuenta = -99999;
                                objNuevoTipoReg.idCuentaAux = -99999;
                                objNuevoTipoReg.idParamGlobal = -99999;
                                objNuevoTipoReg.idParamEspec = -99999;
                                objNuevoTipoReg.idTipoConsumidor = MCodificaParametrica((int)EnumTipoParametrica.TipoConsumidor, values[3].Trim());
                                objNuevoTipoReg.idTipoInfraAux = -99999;
                                objNuevoTipoReg.idInfraAux = -99999;

                                #endregion
                                break;


                        }
                        cDato.objProtoMovimiento = objNuevoTipoReg;
                        lstRegistros.Add(cDato);
                        lngNoRegCorrect++;
                    }
                    else
                    {
                        var cRegError = new CRegistrosError();
                        cRegError.LngId = lngNoReg;
                        cRegError.StrIdVenta = "";
                        cRegError.LngFechaEmision = Convert.ToString(values[19].Trim()); 
                        cRegError.LngNit = Convert.ToString(values[4].Trim());
                        cRegError.LngNroAutorizacion = esnumero(values[17].Trim()) == true ? values[17].Trim() : values[17].Trim().ToUpper();
                        cRegError.LngNroFactura = Convert.ToString(values[16].Trim());
                        cRegError.LngNroReferencia = "";
                        cRegError.StrErrores = strMensaje.Trim();
                        lstRegistroE.Add(cRegError);
                        lngNoRegInCorrect++;

                    }
                }
                catch (Exception ex)
                {
                    var cRegError = new CRegistrosError();
                    cRegError.LngId = lngNoReg;
                    cRegError.StrIdVenta = lngNoRegCorrect.ToString(CultureInfo.InvariantCulture);
                    cRegError.LngFechaEmision = Convert.ToString(values[19].Trim()); ;
                    cRegError.LngNit = Convert.ToString(values[4].Trim());
                    cRegError.LngNroAutorizacion = esnumero(values[17].Trim()) == true ? values[17].Trim() : values[17].Trim().ToUpper(); ;
                    cRegError.LngNroFactura = Convert.ToString(values[16].Trim());
                    cRegError.LngNroReferencia = "";
                    cRegError.StrErrores = "ERROR:" + ex.Message;
                    lstRegistroE.Add(cRegError);
                    lngNoRegInCorrect++;
                }
                lngNoReg++;
            }


            dgvTransRedesGas.Visible = true;
            dgvTransRedesGas.DataSource = lstRegistros;
            dgvTransRedesGas.DataBind();

            //Likst<> listaTransaccion = 
            Session[CVariablesSesion.GrillaRedesGas] = lstRegistros;

            //CargarGrillaTransaccion(lstRegistros);
            //MmostrarColumnasGrillaTransacciones();

            dgvTransaccionError.DataSource = lstRegistroE;
            dgvTransaccionError.DataBind();
            Session[CVariablesSesion.GrillaRegObservado] = lstRegistroE;
        }


        //private List<CRegistrosImportadores> MListaTransaccionRefineria(List<CMovimientoRefineria> lstRegistros)
        //{
        //    var lstRegistros = new List<CMovimientoRefineria>();
        //    List<CRegistrosImportadores> lstRegistros = new List<CRegistrosImportadores>();
        //    lstRegistros.

        //    return lstRegistros;
        //}

        //private void CargarGrillaTransaccion(List<ObjCargaTransaccion> listaTransaccion, List<CRegistrosImportados> listaTransaccionDistRef = null)
        //{
        //    List<CRegistrosImportados> lstRegistros = new List<CRegistrosImportados>();
        //    List<CMovimientoRefineria> listaMovRefinerias = new List<CMovimientoRefineria>();
        //    List<ObjCargaTransaccion> listaNuevaTransaccion = new List<ObjCargaTransaccion>();
        //    if ((int)tlsOperadores.FocusedNode.GetValue("IntIdActividad") == (int)EnumActividades.Refinerias)
        //    {
        //        listaRegstro = MGenereaLista(lista);
        //        listaNuevaTransaccion = listaTransaccion;
        //    }
        //    else
        //    {
        //        //List<ObjCargaTransaccion> listaNuevaTransaccion = new List<ObjCargaTransaccion>();
        //        ObjCargaTransaccion objeto;
        //        listaRegstro = lista;
        //        foreach (CRegistrosImportados objListaImportados in lstRegistros)
        //        {
        //            objeto = new ObjCargaTransaccion();

        //            listaNuevaTransaccion.Add(objeto);
        //        }

        //        var personasConsulta = from p in lista
        //                               select new ObjCargaTransaccion
        //                               {
        //                                   LngId = p.LngId,
        //                                   DecFecha = p.DecFecha,
        //                                   DecIdOrigen = p.DecIdOrigen,
        //                                   StrTInfraestructura = p.StrTInfraestructura,
        //                                   StrInfraestructura = p.StrInfraestructura,
        //                                   StrProyOrigen = p.StrProyOrigen,
        //                                   StrPais = p.StrPais,
        //                                   StrIdentificador = p.StrIdentificador,
        //                                   StrLote = p.StrLote,
        //                                   StrDui = p.StrDui,
        //                                   StrNroRecinto = p.StrNroRecinto,
        //                                   StrCodigoTanqueCisterna = p.StrCodigoTanqueCisterna,
        //                                   DecNroFactura = p.DecNroFactura,
        //                                   DecNroAutorizacion = p.DecNroAutorizacion,
        //                                   StrCodigoControl = p.StrCodigoControl,
        //                                   DecNit = p.DecNit,
        //                                   StrNroReferencia = p.StrNroReferencia,
        //                                   StrCertificadoRecepcion = p.StrCertificadoRecepcion,
        //                                   StrCIUsuario = p.StrCIUsuario,
        //                                   StrEstado = p.StrEstado,
        //                                   StrProducto = p.StrProducto,
        //                                   StrRazonSocial = p.StrRazonSocial,
        //                                   DecCantidad = p.DecCantidad,
        //                                   DecPrecio = p.DecPrecio,
        //                                   DecImporte = p.DecImporte
        //                               };

        //        listaNuevaTransaccion = from p in listaTransaccionDistRef
        //                                select new ObjCargaTransaccion
        //                                {
        //                                    LngId = p.LngId,
        //                                    StrIdVenta = p.StrIdVenta,
        //                                    StrFechaEmision = p.StrFechaEmision,
        //                                    StrTipoMov = p.StrTipoMov,
        //                                    StrEstado = p.StrEstado,
        //                                    LngNit = p.LngNit,
        //                                    StrRazonSocial = p.StrRazonSocial,
        //                                    StrIdentificador = p.StrIdentificador,
        //                                    LngNroFactura = p.LngNroFactura,
        //                                    LngNroAutorizacion = p.LngNroAutorizacion,
        //                                    StrNroReferencia = p.StrNroReferencia,
        //                                    LngNroAutVenta = p.LngNroAutVenta,
        //                                    StrProducto = p.StrProducto,
        //                                    DecVolumen = p.DecVolumen,
        //                                    DecPrecio = p.DecPrecio,
        //                                    DecImporte = p.DecImporte,
        //                                    DecIdTipoCreditoFiscal = p.DecIdTipoCreditoFiscal,
        //                                    DecIdTipoOp = p.DecIdTipoOp,
        //                                    DecOrden = p.DecOrden
        //                                };


        //    }
        //    dgvTransacciones.DataSource = listaNuevaTransaccion;
        //    dgvTransacciones.DataSource = listaNuevaTransaccion;
        //    dgvTransacciones.DataBind();
        //    Session[CVariablesSesion.GrillaTransaccion] = lstRegistros;
        //}

        private void MmostrarColumnasGrillaTransacciones()
        {
            try
            {
                //dgvTransaccionRefineria.Columns["TIPO MOV."].Visible = false;
                //dgvTransaccionRefineria.Columns["NIT"].Visible = false;
                //dgvTransaccionRefineria.Columns["RAZON SOCIAL"].Visible = false;
                //dgvTransaccionRefineria.Columns["IDENTIFICADOR"].Visible = false;
                //dgvTransaccionRefineria.Columns["PRODUCTO"].Visible = false;
                //dgvTransaccionRefineria.Columns["CANTIDAD"].Visible = false;
                //dgvTransaccionRefineria.Columns["PRECIO"].Visible = false;
                //dgvTransaccionRefineria.Columns["IMPORTE"].Visible = false;
                //dgvTransaccionRefineria.Columns["NO. FACTURA"].Visible = false;
                //dgvTransaccionRefineria.Columns["NO. AUTORIZACION"].Visible = false;
                //dgvTransaccionRefineria.Columns["NO. REF."].Visible = false;
                //dgvTransaccionRefineria.Columns["FECHA"].Visible = false;
                //dgvTransaccionRefineria.Columns["ESTADO"].Visible = false;
                //dgvTransaccionRefineria.Columns["NO.AUT.VTA."].Visible = false;

                //dgvTransaccionRefineria.Columns["FECHA"].Visible = false;
                //dgvTransaccionRefineria.Columns["NO. SIS. ORI."].Visible = false;
                //dgvTransaccionRefineria.Columns["TIP. INFR."].Visible = false;
                //dgvTransaccionRefineria.Columns["NO.AUT.VTA."].Visible = false;
                //dgvTransaccionRefineria.Columns["INFRAESTRUCTURA"].Visible = false;
                //dgvTransaccionRefineria.Columns["PROY. ORIGEN"].Visible = false;
                //dgvTransaccionRefineria.Columns["PAIS"].Visible = false;
                //dgvTransaccionRefineria.Columns["IDENTIF."].Visible = false;
                //dgvTransaccionRefineria.Columns["LOTE"].Visible = false;
                //dgvTransaccionRefineria.Columns["DUI"].Visible = false;
                //dgvTransaccionRefineria.Columns["NO. PRESIN."].Visible = false;
                //dgvTransaccionRefineria.Columns["COD. TAN. CIS."].Visible = false;
                //dgvTransaccionRefineria.Columns["NO. FAC."].Visible = false;
                //dgvTransaccionRefineria.Columns["NO. AUT."].Visible = false;
                //dgvTransaccionRefineria.Columns["COD. CTRL."].Visible = false;
                //dgvTransaccionRefineria.Columns["NIT"].Visible = false;
                //dgvTransaccionRefineria.Columns["RAZ. SOC."].Visible = false;
                //dgvTransaccionRefineria.Columns["NO. REF."].Visible = false;
                //dgvTransaccionRefineria.Columns["CERT. RECEP."].Visible = false;
                //dgvTransaccionRefineria.Columns["CI. USU."].Visible = false;
                //dgvTransaccionRefineria.Columns["ESTADO"].Visible = false;
                //dgvTransaccionRefineria.Columns["PRODUCTO"].Visible = false;
                //dgvTransaccionRefineria.Columns["PRECIO"].Visible = false;
                //dgvTransaccionRefineria.Columns["IMPORTE"].Visible = false;





                switch ((int)tlsOperadores.FocusedNode.GetValue("IntIdActividad"))
                {

                    case (int)EnumActividades.Refinerias://
                        dgvTransaccionRefineria.Columns["FECHA"].Visible = true;
                        dgvTransaccionRefineria.Columns["NO. SIS. ORI."].Visible = true;
                        dgvTransaccionRefineria.Columns["TIP. INFR."].Visible = true;
                        dgvTransaccionRefineria.Columns["NO.AUT.VTA."].Visible = true;
                        dgvTransaccionRefineria.Columns["INFRAESTRUCTURA"].Visible = true;
                        dgvTransaccionRefineria.Columns["PROY. ORIGEN"].Visible = true;
                        dgvTransaccionRefineria.Columns["PAIS"].Visible = true;
                        dgvTransaccionRefineria.Columns["IDENTIF."].Visible = true;
                        dgvTransaccionRefineria.Columns["LOTE"].Visible = true;
                        dgvTransaccionRefineria.Columns["DUI"].Visible = true;
                        dgvTransaccionRefineria.Columns["NO. PRESIN."].Visible = true;
                        dgvTransaccionRefineria.Columns["COD. TAN. CIS."].Visible = true;
                        dgvTransaccionRefineria.Columns["NO. FAC."].Visible = true;
                        dgvTransaccionRefineria.Columns["NO. AUT."].Visible = true;
                        dgvTransaccionRefineria.Columns["COD. CTRL."].Visible = true;
                        dgvTransaccionRefineria.Columns["NIT"].Visible = true;
                        dgvTransaccionRefineria.Columns["RAZ. SOC."].Visible = true;
                        dgvTransaccionRefineria.Columns["NO. REF."].Visible = true;
                        dgvTransaccionRefineria.Columns["CERT. RECEP."].Visible = true;
                        dgvTransaccionRefineria.Columns["CI. USU."].Visible = true;
                        dgvTransaccionRefineria.Columns["ESTADO"].Visible = true;
                        dgvTransaccionRefineria.Columns["PRODUCTO"].Visible = true;
                        dgvTransaccionRefineria.Columns["PRECIO"].Visible = true;
                        dgvTransaccionRefineria.Columns["IMPORTE"].Visible = true;
                        break;
                    default:
                        //dgvTransaccionRefineria.Columns["TIPO MOV."].Visible = true;
                        //dgvTransaccionRefineria.Columns["NIT"].Visible = true;
                        //dgvTransaccionRefineria.Columns["RAZON SOCIAL"].Visible = true;
                        //dgvTransaccionRefineria.Columns["IDENTIFICADOR"].Visible = true;
                        //dgvTransaccionRefineria.Columns["PRODUCTO"].Visible = true;
                        //dgvTransaccionRefineria.Columns["CANTIDAD"].Visible = true;
                        //dgvTransaccionRefineria.Columns["PRECIO"].Visible = true;
                        //dgvTransaccionRefineria.Columns["IMPORTE"].Visible = true;
                        //dgvTransaccionRefineria.Columns["NO. FACTURA"].Visible = true;
                        //dgvTransaccionRefineria.Columns["NO. AUTORIZACION"].Visible = true;
                        //dgvTransaccionRefineria.Columns["NO. REF."].Visible = true;
                        //dgvTransaccionRefineria.Columns["FECHA"].Visible = true;
                        //dgvTransaccionRefineria.Columns["ESTADO"].Visible = true;
                        //dgvTransaccionRefineria.Columns["NO.AUT.VTA."].Visible = true;
                        return;
                }

            }
            catch (Exception ex)
            {
                //strMensaje += ex.Message;
            }

        }

        /// <summary>
        /// METODO. MValidaLineaTaero
        /// </summary>
        /// <param name="values"></param>
        /// <param name="strMensaje"></param>
        /// <returns></returns>
        private bool MValidaLineaPscaa(string[] values, ref string strMensaje)
        {
            var booResultado = true;
            try
            {
                var to = cmbTipoOperacion.Text.Split(';');
                var strTipoOp = to[1].Substring(2, 2);
                int[] arrayNumero;
                decimal dec;
                DateTime dt;
                switch (strTipoOp)
                {
                    case "RC":
                        #region Recepciones
                        if (values.Length != 29)
                        {
                            strMensaje = "- Nro. de columnas del registro invalido.";
                        }
                        arrayNumero = new[] { 1, 8, 14, 15, 17, 21, 26, 27, 28 };
                        for (int i = 0; i < arrayNumero.Length; i++)
                        {
                            if (!decimal.TryParse(values[arrayNumero[i]].Trim(), out dec))
                            {
                                strMensaje += "- Columna Nro. " + arrayNumero[i] + " No es un número.";
                            }
                        }
                        if (values[0].Trim().Substring(6, 4) + values[0].Trim().Substring(3, 2) != cmbPeriodo.Value.ToString().Substring(0, 4) + cmbPeriodo.Value.ToString().Substring(4, 2))
                        {
                            strMensaje += "- Periodo incorrecto.";
                        }
                        if (!DateTime.TryParse(values[0].Trim(), out dt))
                        {
                            strMensaje += "- Fecha incorrecta.";
                        }
                        if (Convert.ToDecimal(values[26]) <= 0)
                        {
                            strMensaje += "- Cantidad incorrecta.";
                        }
                        //la factura puede contener otros items
                        //if (Convert.ToDecimal(values[28].Trim()) > (Convert.ToDecimal(values[26].Trim()) * Convert.ToDecimal(values[27].Trim())) + 2 ||
                        //    Convert.ToDecimal(values[28].Trim()) < (Convert.ToDecimal(values[26].Trim()) * Convert.ToDecimal(values[27].Trim())) - 2)
                        //{
                        //    strMensaje += "- Importe incorrecto.";
                        //}
                        if (MCodificaParametrica((int)EnumTipoParametrica.Producto, values[25].Trim()) == -99999)
                        {
                            strMensaje += "- Producto incorrecto.";
                        }
                        if (MCodificaParametrica((int)EnumTipoParametrica.UnidadMedida, values[25].Trim()) == -99999)
                        {
                            strMensaje += "- Unidad de medida invalida.";
                        }
                        if (values[16].Trim().Length == 1 && values[16].Trim() != "0")
                        {
                            strMensaje += "- Código de control incorrecto.";
                        }
                        if (values[16].Trim().Length > 1 && values[16].Trim().Length < 6)
                        {
                            strMensaje += "- Código de control incorrecto.";
                        }
                        if (MCodificaParametrica((int)EnumTipoParametrica.Pais, values[7].Trim()) == -99999)
                        {
                            strMensaje += "- País invalido.";
                        }
                        if (values[10].Trim().Length > 1)
                        {
                            if (values[10].Trim().ContainsAny("-", "@", "/", "'", "\\", ".", ",", "_", "+", "[", "]", "{", "}", " "))
                            {
                                strMensaje += "- Identificador contiene caracteres invalidos.";
                            }
                        }
                        if (values[3].Trim() != "NOID")
                        {
                            var lstInfraestructura = Session[CVariablesSesion.ListaInfraestructura] as List<O_INFRAESTRUCTURA_VOL_CTY>;
                            if (lstInfraestructura != null && lstInfraestructura.Any())
                            {
                                lstInfraestructura = lstInfraestructura.Where(x => x.CODIGO_OPERADOR.Trim() == values[3].Trim() && x.TIPO.Trim() == values[2].Trim()).ToList();
                                if (lstInfraestructura.Any())
                                {
                                    if (lstInfraestructura.FirstOrDefault().ID_PRODUCTO != MCodificaParametrica((int)EnumTipoParametrica.Producto, values[25].Trim()))
                                    {
                                        strMensaje += "- El producto no corresponde a la infraestructura receptora especificada.";
                                    }
                                }
                                else
                                {
                                    strMensaje += "- Infraestructura receptora invalida.";
                                }
                            }
                            else
                            {
                                strMensaje += "- Infraestructura receptora no registrada.";
                            }
                        }
                        if (values[6].Trim() != "NOID")
                        {
                            var lstInfraestructura = Session[CVariablesSesion.ListaInfraestructura] as List<O_INFRAESTRUCTURA_VOL_CTY>;
                            if (lstInfraestructura != null && lstInfraestructura.Any())
                            {
                                lstInfraestructura = lstInfraestructura.Where(x => x.CODIGO_OPERADOR == values[6].Trim() && x.TIPO == values[5].Trim()).ToList();
                                if (lstInfraestructura.Any())
                                {
                                    if (lstInfraestructura.FirstOrDefault().ID_PRODUCTO != MCodificaParametrica((int)EnumTipoParametrica.Producto, values[25].Trim()))
                                    {
                                        strMensaje += "- El producto no corresponde a la infraestructura origen especificada.";
                                    }
                                }
                                else
                                {
                                    strMensaje += "- Infraestructura origen invalida.";
                                }
                            }
                            else
                            {
                                strMensaje += "- Infraestructura origen no definida.";
                            }
                        }
                        if (values[5].Trim() != "NOID" && values[6].Trim() == "NOID")
                        {
                            if (MCodificaParametrica((int)EnumTipoParametrica.TipoInfraestructura, values[5].Trim()) == -99999)
                            {
                                strMensaje += "- Tipo de infraestructura origen invalida.";
                            }
                        }
                        if (values[4].Trim() != "NOID")
                        {
                            if (MCodificaParametrica((int)EnumTipoParametrica.Entidad, values[4].Trim()) == -99999)
                            {
                                strMensaje += "- Entidad origen invalida.";
                            }
                        }
                        if (Convert.ToDecimal(values[14]) == 0 && values[19].Trim().Length == 0 && values[20].Trim().Length == 0)
                        {
                            strMensaje += "- No existe documento de respaldo.";
                        }
                        #endregion
                        break;
                    default:
                        #region Despachos
                        if (values.Length != 32)
                        {
                            strMensaje = "- Nro. de columnas del registro invalido.";
                        }
                        arrayNumero = new[] { 1, 8, 14, 15, 17, 22, 23, 29, 30, 31 };
                        for (int i = 0; i < arrayNumero.Length; i++)
                        {
                            if (!decimal.TryParse(values[arrayNumero[i]].Trim(), out dec))
                            {
                                strMensaje += "- Columna Nro. " + arrayNumero[i] + " No es un número.";
                            }
                        }
                        if (values[0].Trim().Substring(6, 4) + values[0].Trim().Substring(3, 2) != cmbPeriodo.Value.ToString().Substring(0, 4) + cmbPeriodo.Value.ToString().Substring(4, 2))
                        {
                            strMensaje += "- Periodo incorrecto.";
                        }
                        if (!DateTime.TryParse(values[0].Trim(), out dt))
                        {
                            strMensaje += "- Fecha incorrecta.";
                        }
                        if ((values[27].Trim() == "V" && Convert.ToDecimal(values[29]) <= 0) || (values[27].Trim() == "A" && Convert.ToDecimal(values[29]) < 0))
                        {
                            strMensaje += "- Cantidad incorrecta.";
                        }
                        if (Convert.ToDecimal(values[31].Trim()) > (Convert.ToDecimal(values[29].Trim()) * Convert.ToDecimal(values[30].Trim())) + 2 ||
                            Convert.ToDecimal(values[31].Trim()) < (Convert.ToDecimal(values[29].Trim()) * Convert.ToDecimal(values[30].Trim())) - 2)
                        {
                            strMensaje += "- Importe incorrecto.";
                        }
                        if (values[16].Trim().Length == 1 && values[16].Trim() != "0")
                        {
                            strMensaje += "- Código de control incorrecto.";
                        }
                        if (values[16].Trim().Length > 1 && values[16].Trim().Length < 6)
                        {
                            strMensaje += "- Código de control incorrecto.";
                        }
                        if (values[10].Trim().Length > 1)
                        {
                            if (values[10].Trim().ContainsAny("-", "@", "/", "'", "\\", ".", ",", "_", "+", "[", "]", "{", "}", " "))
                            {
                                strMensaje += "- Identificador contiene caracteres invalidos.";
                            }
                        }
                        if (MCodificaParametrica((int)EnumTipoParametrica.Producto, values[28].Trim()) == -99999)
                        {
                            strMensaje += "- Producto incorrecto.";
                        }
                        if (MCodificaParametrica((int)EnumTipoParametrica.Pais, values[7].Trim()) == -99999)
                        {
                            strMensaje += "- País invalido.";
                        }
                        if (values[13].Trim() != "NOID" && values[23].Trim() == "0")
                        {
                            strMensaje += "- El movimiento en envases debe tener Autorización de Sustancias Controladas.";
                        }
                        if (values[3].Trim() != "NOID")
                        {
                            var lstInfraestructura = Session[CVariablesSesion.ListaInfraestructura] as List<O_INFRAESTRUCTURA_VOL_CTY>;
                            if (lstInfraestructura != null && lstInfraestructura.Any())
                            {
                                lstInfraestructura = lstInfraestructura.Where(x => x.CODIGO_OPERADOR == values[3].Trim() && x.TIPO == values[2].Trim()).ToList();
                                if (lstInfraestructura.Any())
                                {
                                    if (lstInfraestructura.FirstOrDefault().ID_PRODUCTO != MCodificaParametrica((int)EnumTipoParametrica.Producto, values[28].Trim()))
                                    {
                                        strMensaje += "- El producto no corresponde a la infraestructura origen especificada.";
                                    }
                                }
                                else
                                {
                                    strMensaje += "- Infraestructura origen invalida.";
                                }
                            }
                            else
                            {
                                strMensaje += "- Infraestructura origen no definida.";
                            }
                        }
                        if (values[6].Trim() != "NOID")
                        {
                            var lstInfraestructura = Session[CVariablesSesion.ListaInfraestructura] as List<O_INFRAESTRUCTURA_VOL_CTY>;
                            if (lstInfraestructura != null && lstInfraestructura.Any())
                            {
                                lstInfraestructura = lstInfraestructura.Where(x => x.CODIGO_OPERADOR == values[6].Trim() && x.TIPO == values[5].Trim()).ToList();
                                if (lstInfraestructura.Any())
                                {
                                    if (lstInfraestructura.FirstOrDefault().ID_PRODUCTO != MCodificaParametrica((int)EnumTipoParametrica.Producto, values[28].Trim()))
                                    {
                                        strMensaje += "- El producto no corresponde a la infraestructura origen especificada.";
                                    }
                                }
                                else
                                {
                                    strMensaje += "- Infraestructura origen invalida.";
                                }
                            }
                            else
                            {
                                strMensaje += "- Infraestructura origen no definida.";
                            }
                        }
                        if (values[5].Trim() != "NOID" && values[6].Trim() == "NOID")
                        {
                            if (MCodificaParametrica((int)EnumTipoParametrica.TipoInfraestructura, values[5].Trim()) == -99999)
                            {
                                strMensaje += "- Tipo de infraestructura origen invalida.";
                            }
                        }
                        if (values[4].Trim() != "NOID")
                        {
                            if (MCodificaParametrica((int)EnumTipoParametrica.Entidad, values[4].Trim()) == -99999)
                            {
                                strMensaje += "- Entidad origen invalida.";
                            }
                        }
                        if (values[19].Trim().Length == 0 || values[19].Trim() == "0")
                        {
                            strMensaje += "- Número de referencia invalido.";
                        }
                        if (Convert.ToDecimal(values[14]) == 0 && values[19].Trim().Length == 0 && Convert.ToDecimal(values[20]) == 0)
                        {
                            strMensaje += "- No existe documento de respaldo.";
                        }
                        #endregion
                        break;
                }
            }
            catch (Exception ex)
            {
                strMensaje += ex.Message;
            }

            if (strMensaje.Trim().Length > 0)
            {
                booResultado = false;
            }
            return booResultado;
        }

        /// <summary>
        /// METODO. MCargaTerminalAeropuerto
        /// </summary>
        /// <param name="strRuta"></param>
        private void MCargaPscaa(string strRuta)
        {
            var reader = new StreamReader(File.OpenRead(@strRuta));
            var lstRegistros = new List<CRegistrosImportados>();
            var lstRegistroE = new List<CRegistrosError>();
            long lngNoRegCorrect = 1, lngNoRegInCorrect = 1, lngNoReg = 1;
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(';');
                try
                {
                    DateTime datFecha, datFechaReg;
                    string strFecha;
                    ObjTransaccion objFactura, objDocumento;
                    ObjProtoMovimientoTransaccion objNuevoTipoReg = null;
                    int estadoTra;
                    List<ObjTransaccion> lstTransacciones;
                    var to = cmbTipoOperacion.Text.Split(';');
                    var strTipoOp = to[1].Substring(2, 2);

                    string strMensaje = string.Empty;
                    if (MValidaLineaPscaa(values, ref strMensaje))
                    {
                        var cDato = new CRegistrosImportados();
                        int controlBinario;
                        switch (strTipoOp)
                        {
                            case "RC":
                                #region Valida y genera objeto
                                cDato.LngId = lngNoRegCorrect;
                                cDato.StrIdVenta = values[1].Trim();
                                datFecha = DateTime.Parse(values[0].Trim());
                                cDato.StrFechaEmision = datFecha.ToString();
                                cDato.StrTipoMov = (values[7].Trim() == "BOLIVIA") ? "NAC" : "IMP";
                                cDato.StrEstado = values[24].Trim();
                                cDato.LngNit = Convert.ToInt64(values[17]);
                                cDato.StrRazonSocial = values[18].Trim().ToUpper().Replace("¥", "Ñ").Replace("Á", "A").Replace("É", "E").Replace("Í", "I").Replace("Ó", "O").Replace("Ú", "U").Replace("'", "").Replace("�", "Ñ");
                                cDato.StrRazonSocial = Regex.Replace(cDato.StrRazonSocial, @"\s+", " ");
                                cDato.StrIdentificador = values[10].Trim();
                                cDato.LngNroFactura = Convert.ToInt64(values[14]);
                                cDato.LngNroAutorizacion = Convert.ToInt64(values[15]);
                                cDato.StrNroReferencia = values[19].Trim();
                                cDato.LngNroAutVenta = Convert.ToInt64(values[21]);
                                cDato.StrProducto = values[25].Trim();
                                cDato.DecVolumen = Convert.ToDecimal(values[26]);
                                cDato.DecPrecio = Convert.ToDecimal(values[27]);
                                cDato.DecImporte = Convert.ToDecimal(values[28]);
                                cDato.DecIdTipoCreditoFiscal = (cDato.LngNroFactura > 0) ? (int)EnumTipoCredFiscal.Si : (int)EnumTipoCredFiscal.No;
                                cDato.DecIdTipoOp = Convert.ToDecimal(cmbTipoOperacion.Value);
                                cDato.DecOrden = 1;

                                strFecha = datFecha.Year + datFecha.Month.ToString().PadLeft(2, '0') + datFecha.Day.ToString().PadLeft(2, '0') + datFecha.Hour.ToString().PadLeft(2, '0') + datFecha.Minute.ToString().PadLeft(2, '0') + datFecha.Second.ToString().PadLeft(2, '0');
                                datFechaReg = DateTime.Now;
                                estadoTra = (cDato.StrEstado == "A") ? (int)EnumEstadoMov.Anulado : (int)EnumEstadoMov.Activo;
                                lstTransacciones = new List<ObjTransaccion>();

                                controlBinario = 1;
                                if (Convert.ToDecimal(values[14].Trim()) > 0)
                                {
                                    objFactura = new ObjTransaccion();
                                    objFactura.idMovimiento = 0;
                                    objFactura.idTransaccion = 0;
                                    objFactura.idTransaccionPadre = 0;
                                    objFactura.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                    objFactura.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                    objFactura.idTipoTransaccion = (decimal)((values[16].Trim().Length > 4) ? EnumTipoTransaccion.FacturaComputarizada : EnumTipoTransaccion.FacturaManual);
                                    objFactura.fechaEmision = Convert.ToInt64(strFecha);
                                    objFactura.numeroTransaccion = values[14].Trim();
                                    objFactura.numeroAutorizacion = Convert.ToDecimal(values[15].Trim());
                                    objFactura.codigoControl = values[16].Trim();
                                    objFactura.nitConsumidor = Convert.ToDecimal(values[17].Trim());
                                    objFactura.razonSocial = cDato.StrRazonSocial;
                                    objFactura.precioUnitario = Convert.ToDecimal(values[27].Trim());
                                    objFactura.precioTotal = Convert.ToDecimal(values[28].Trim());
                                    objFactura.usuarioTransaccion = values[23].Trim();
                                    objFactura.observaciones = "";
                                    objFactura.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                    objFactura.idEstadoTransaccion = estadoTra;
                                    objFactura.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.Si;
                                    objFactura.idFormaPago = (int)EnumTiposPago.Efectivo;
                                    objFactura.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                    objFactura.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                    objFactura.codigoSeguridad = 0;
                                    objFactura.idAutorizacionDato = 0;
                                    objFactura.binarioControl = controlBinario + "000000000";
                                    objFactura.fechaOperacion = Convert.ToInt64(strFecha);
                                    objFactura.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                    objFactura.codigo = "";
                                    lstTransacciones.Add(objFactura);
                                    controlBinario = 0;
                                }

                                if (values[19].Trim().Length > 0)
                                {
                                    objDocumento = new ObjTransaccion();
                                    objDocumento.idMovimiento = 0;
                                    objDocumento.idTransaccion = 0;
                                    objDocumento.idTransaccionPadre = 0;
                                    objDocumento.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                    objDocumento.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                    objDocumento.idTipoTransaccion = (decimal)EnumTipoTransaccion.NotaMovimiento;
                                    objDocumento.fechaEmision = Convert.ToInt64(strFecha);
                                    objDocumento.numeroTransaccion = values[19].Trim();
                                    objDocumento.numeroAutorizacion = 0;
                                    objDocumento.codigoControl = "";
                                    objDocumento.nitConsumidor = 0;
                                    objDocumento.razonSocial = "";
                                    objDocumento.precioUnitario = 0;
                                    objDocumento.precioTotal = 0;
                                    objDocumento.usuarioTransaccion = values[23].Trim();
                                    objDocumento.observaciones = "";
                                    objDocumento.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                    objDocumento.idEstadoTransaccion = estadoTra;
                                    objDocumento.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.No;
                                    objDocumento.idFormaPago = (decimal)EnumTiposPago.Efectivo;
                                    objDocumento.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                    objDocumento.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                    objDocumento.codigoSeguridad = 0;
                                    objDocumento.idAutorizacionDato = 0;
                                    objDocumento.binarioControl = controlBinario + "000000000";
                                    objDocumento.fechaOperacion = Convert.ToInt64(strFecha);
                                    objDocumento.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                    objDocumento.codigo = "";
                                    lstTransacciones.Add(objDocumento);
                                    controlBinario = 0;
                                }
                                if (values[20].Trim().Length > 0)
                                {
                                    objDocumento = new ObjTransaccion();
                                    objDocumento.idMovimiento = 0;
                                    objDocumento.idTransaccion = 0;
                                    objDocumento.idTransaccionPadre = 0;
                                    objDocumento.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                    objDocumento.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString().PadLeft(2, '0') + datFechaReg.Day.ToString().PadLeft(2, '0') + datFechaReg.Hour.ToString().PadLeft(2, '0') + datFechaReg.Minute.ToString().PadLeft(2, '0') + datFechaReg.Second.ToString().PadLeft(2, '0'));
                                    objDocumento.idTipoTransaccion = (decimal)EnumTipoTransaccion.DocCre;
                                    objDocumento.fechaEmision = Convert.ToInt64(strFecha);
                                    objDocumento.numeroTransaccion = values[20].Trim();
                                    objDocumento.numeroAutorizacion = 0;
                                    objDocumento.codigoControl = "";
                                    objDocumento.nitConsumidor = 0;
                                    objDocumento.razonSocial = "";
                                    objDocumento.precioUnitario = 0;
                                    objDocumento.precioTotal = 0;
                                    objDocumento.usuarioTransaccion = values[23].Trim();
                                    objDocumento.observaciones = "";
                                    objDocumento.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                    objDocumento.idEstadoTransaccion = estadoTra;
                                    objDocumento.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.No;
                                    objDocumento.idFormaPago = (int)EnumTiposPago.Efectivo;
                                    objDocumento.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                    objDocumento.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                    objDocumento.codigoSeguridad = 0;
                                    objDocumento.idAutorizacionDato = 0;
                                    objDocumento.binarioControl = controlBinario + "000000000";
                                    objDocumento.fechaOperacion = Convert.ToInt64(strFecha);
                                    objDocumento.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                    objDocumento.codigo = "";
                                    lstTransacciones.Add(objDocumento);
                                }
                                if (values[12].Trim().Length > 0)
                                {
                                    var arreglo = values[12].Trim().Split('|');
                                    for (int i = 0; i < arreglo.Count(); i++)
                                    {
                                        objDocumento = new ObjTransaccion();
                                        objDocumento.idMovimiento = 0;
                                        objDocumento.idTransaccion = 0;
                                        objDocumento.idTransaccionPadre = 0;
                                        objDocumento.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                        objDocumento.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                        objDocumento.idTipoTransaccion = (decimal)EnumTipoTransaccion.DocPrecinto;
                                        objDocumento.fechaEmision = Convert.ToInt64(strFecha);
                                        objDocumento.numeroTransaccion = arreglo[i];
                                        objDocumento.numeroAutorizacion = 0;
                                        objDocumento.codigoControl = "";
                                        objDocumento.nitConsumidor = 0;
                                        objDocumento.razonSocial = "";
                                        objDocumento.precioUnitario = 0;
                                        objDocumento.precioTotal = 0;
                                        objDocumento.usuarioTransaccion = values[23].Trim();
                                        objDocumento.observaciones = "";
                                        objDocumento.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                        objDocumento.idEstadoTransaccion = estadoTra;
                                        objDocumento.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.No;
                                        objDocumento.idFormaPago = (int)EnumTiposPago.Efectivo;
                                        objDocumento.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                        objDocumento.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                        objDocumento.codigoSeguridad = 0;
                                        objDocumento.idAutorizacionDato = 0;
                                        objDocumento.binarioControl = "0000000000";
                                        objDocumento.fechaOperacion = Convert.ToInt64(strFecha);
                                        objDocumento.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                        objDocumento.codigo = "";
                                        lstTransacciones.Add(objDocumento);
                                    }
                                }
                                if (values[22].Trim().Length > 0)
                                {
                                    var arreglo = values[22].Trim().Split('|');
                                    for (int i = 0; i < arreglo.Count(); i++)
                                    {
                                        objDocumento = new ObjTransaccion();
                                        objDocumento.idMovimiento = 0;
                                        objDocumento.idTransaccion = 0;
                                        objDocumento.idTransaccionPadre = 0;
                                        objDocumento.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                        objDocumento.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString().PadLeft(2, '0') + datFechaReg.Day.ToString().PadLeft(2, '0') + datFechaReg.Hour.ToString().PadLeft(2, '0') + datFechaReg.Minute.ToString().PadLeft(2, '0') + datFechaReg.Second.ToString().PadLeft(2, '0'));
                                        objDocumento.idTipoTransaccion = (decimal)EnumTipoTransaccion.DocCalidad;
                                        objDocumento.fechaEmision = Convert.ToInt64(strFecha);
                                        objDocumento.numeroTransaccion = arreglo[i];
                                        objDocumento.numeroAutorizacion = 0;
                                        objDocumento.codigoControl = "";
                                        objDocumento.nitConsumidor = 0;
                                        objDocumento.razonSocial = "";
                                        objDocumento.precioUnitario = 0;
                                        objDocumento.precioTotal = 0;
                                        objDocumento.usuarioTransaccion = values[23].Trim();
                                        objDocumento.observaciones = "";
                                        objDocumento.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                        objDocumento.idEstadoTransaccion = estadoTra;
                                        objDocumento.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.No;
                                        objDocumento.idFormaPago = (int)EnumTiposPago.Efectivo;
                                        objDocumento.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                        objDocumento.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                        objDocumento.codigoSeguridad = 0;
                                        objDocumento.idAutorizacionDato = 0;
                                        objDocumento.binarioControl = "0000000000";
                                        objDocumento.fechaOperacion = Convert.ToInt64(strFecha);
                                        objDocumento.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                        objDocumento.codigo = "";
                                        lstTransacciones.Add(objDocumento);
                                    }
                                }
                                if (values[11].Trim().Length > 0)
                                {
                                    objDocumento = new ObjTransaccion();
                                    objDocumento.idMovimiento = 0;
                                    objDocumento.idTransaccion = 0;
                                    objDocumento.idTransaccionPadre = 0;
                                    objDocumento.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                    objDocumento.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString().PadLeft(2, '0') + datFechaReg.Day.ToString().PadLeft(2, '0') + datFechaReg.Hour.ToString().PadLeft(2, '0') + datFechaReg.Minute.ToString().PadLeft(2, '0') + datFechaReg.Second.ToString().PadLeft(2, '0'));
                                    objDocumento.idTipoTransaccion = (decimal)EnumTipoTransaccion.DocLote;
                                    objDocumento.fechaEmision = Convert.ToInt64(strFecha);
                                    objDocumento.numeroTransaccion = values[11].Trim();
                                    objDocumento.numeroAutorizacion = 0;
                                    objDocumento.codigoControl = "";
                                    objDocumento.nitConsumidor = 0;
                                    objDocumento.razonSocial = "";
                                    objDocumento.precioUnitario = 0;
                                    objDocumento.precioTotal = 0;
                                    objDocumento.usuarioTransaccion = values[23].Trim();
                                    objDocumento.observaciones = "";
                                    objDocumento.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                    objDocumento.idEstadoTransaccion = estadoTra;
                                    objDocumento.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.No;
                                    objDocumento.idFormaPago = (int)EnumTiposPago.Efectivo;
                                    objDocumento.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                    objDocumento.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                    objDocumento.codigoSeguridad = 0;
                                    objDocumento.idAutorizacionDato = 0;
                                    objDocumento.binarioControl = "0000000000";
                                    objDocumento.fechaOperacion = Convert.ToInt64(strFecha);
                                    objDocumento.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                    objDocumento.codigo = "";
                                    lstTransacciones.Add(objDocumento);
                                }

                                if (Convert.ToDecimal(values[21].Trim()) > 0)
                                {
                                    objDocumento = new ObjTransaccion();
                                    objDocumento.idMovimiento = 0;
                                    objDocumento.idTransaccion = 0;
                                    objDocumento.idTransaccionPadre = 0;
                                    objDocumento.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                    objDocumento.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString().PadLeft(2, '0') + datFechaReg.Day.ToString().PadLeft(2, '0') + datFechaReg.Hour.ToString().PadLeft(2, '0') + datFechaReg.Minute.ToString().PadLeft(2, '0') + datFechaReg.Second.ToString().PadLeft(2, '0'));
                                    objDocumento.idTipoTransaccion = (decimal)EnumTipoTransaccion.AutSustCtrl;
                                    objDocumento.fechaEmision = Convert.ToInt64(strFecha);
                                    objDocumento.numeroTransaccion = values[21].Trim();
                                    objDocumento.numeroAutorizacion = 0;
                                    objDocumento.codigoControl = "";
                                    objDocumento.nitConsumidor = 0;
                                    objDocumento.razonSocial = "";
                                    objDocumento.precioUnitario = 0;
                                    objDocumento.precioTotal = 0;
                                    objDocumento.usuarioTransaccion = values[23].Trim();
                                    objDocumento.observaciones = "";
                                    objDocumento.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                    objDocumento.idEstadoTransaccion = estadoTra;
                                    objDocumento.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.No;
                                    objDocumento.idFormaPago = (int)EnumTiposPago.Efectivo;
                                    objDocumento.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                    objDocumento.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                    objDocumento.codigoSeguridad = 0;
                                    objDocumento.idAutorizacionDato = 0;
                                    objDocumento.binarioControl = "0000000000";
                                    objDocumento.fechaOperacion = Convert.ToInt64(strFecha);
                                    objDocumento.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                    objDocumento.codigo = "";
                                    lstTransacciones.Add(objDocumento);
                                }
                                objNuevoTipoReg = new ObjProtoMovimientoTransaccion();
                                objNuevoTipoReg.idMovimientoPadre = 0;
                                objNuevoTipoReg.idTipoActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                objNuevoTipoReg.idTipoOperacion = Convert.ToDecimal(cmbTipoOperacion.Value);
                                objNuevoTipoReg.idConsumidor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString());
                                objNuevoTipoReg.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                objNuevoTipoReg.fechaRegistroMovimiento = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                objNuevoTipoReg.fechaOperacion = Convert.ToInt64(strFecha);
                                objNuevoTipoReg.idTipoMovimientoFlujo = (int)EnumTipoMovimientoFlujo.Fisico;
                                objNuevoTipoReg.idTipoMovimiento = (values[7].Trim() == "BOLIVIA") ? (int)EnumTipoMovimiento.NacNac : (int)EnumTipoMovimiento.IntNac;
                                objNuevoTipoReg.idEstadoMovimiento = estadoTra;
                                objNuevoTipoReg.idFuenteOrigenDato = (decimal)EnumTipoRegistro.OrigenCsv;
                                objNuevoTipoReg.idUbicacionOperacion = MCodificaParametrica((int)EnumTipoParametrica.Pais, values[7].Trim());
                                objNuevoTipoReg.idUnidadMedidaHidrocarburo = MCodificaParametrica((int)EnumTipoParametrica.UnidadMedida, values[25].Trim());
                                objNuevoTipoReg.idHidrocarburo = MCodificaParametrica((int)EnumTipoParametrica.Producto, values[25].Trim());
                                objNuevoTipoReg.cantidadHidrocarburo = cDato.DecVolumen;
                                objNuevoTipoReg.identificador = cDato.StrIdentificador;
                                objNuevoTipoReg.subIdentificador = "";
                                objNuevoTipoReg.observacion = "";
                                objNuevoTipoReg.tipoInfraestructura = MCodificaParametrica((int)EnumTipoParametrica.TipoInfraestructura, values[2].Trim());
                                objNuevoTipoReg.idInfraestructura = MCodificaParametrica((int)EnumTipoParametrica.Infraestructura, values[2].Trim() + "|" + values[3].Trim());
                                objNuevoTipoReg.idTipoMedicion = -99999;
                                objNuevoTipoReg.cantidadAguaLitros = 0;
                                objNuevoTipoReg.idUnidadTemperatura = (decimal)EnumUnidadTemperatura.GradoCentigrado;
                                objNuevoTipoReg.temperatura = Convert.ToDecimal(values[8]);
                                objNuevoTipoReg.gradoApi = values[9].Trim();
                                objNuevoTipoReg.idEntidad = MCodificaParametrica((int)EnumTipoParametrica.Entidad, values[4].Trim());
                                objNuevoTipoReg.idTipoMedioTransporte = MCodificaParametrica((int)EnumTipoParametrica.TipoInfraestructura, values[5].Trim());
                                objNuevoTipoReg.idMedioTransporte = MCodificaParametrica((int)EnumTipoParametrica.Infraestructura, values[5].Trim() + "|" + values[6].Trim());
                                objNuevoTipoReg.idUnidadEnvasesTransporte = -99999;
                                objNuevoTipoReg.cantidadEnvasesTransporte = 0;
                                objNuevoTipoReg.binarioControl = "00000-0000000000-0000000000-00000";
                                objNuevoTipoReg.idIntegracion = Convert.ToDecimal(values[1]);
                                objNuevoTipoReg.idTransaccionBaseExterna = "";
                                objNuevoTipoReg.codigo = values[13].Trim();
                                objNuevoTipoReg.barcode = 0;
                                objNuevoTipoReg.listaTransaccion = JsonSerializer.SerializeToString(lstTransacciones);

                                objNuevoTipoReg.jsonReferencia = "";
                                objNuevoTipoReg.estadoJsonRef = -99999;
                                objNuevoTipoReg.idCuenta = -99999;
                                objNuevoTipoReg.idCuentaAux = -99999;
                                objNuevoTipoReg.idParamGlobal = -99999;
                                objNuevoTipoReg.idParamEspec = -99999;
                                objNuevoTipoReg.idTipoConsumidor = -99999;
                                objNuevoTipoReg.idTipoInfraAux = -99999;
                                objNuevoTipoReg.idInfraAux = -99999;

                                #endregion
                                break;
                            case "DS":
                                #region Valida y genera objeto
                                cDato.LngId = lngNoRegCorrect;
                                cDato.StrIdVenta = values[1].Trim();
                                datFecha = DateTime.Parse(values[0].Trim());
                                cDato.StrFechaEmision = datFecha.ToString();
                                cDato.StrTipoMov = (values[7].Trim() == "BOLIVIA") ? "NAC" : "EXP";
                                cDato.StrEstado = values[27].Trim();
                                cDato.LngNit = Convert.ToInt64(values[17]);
                                cDato.StrRazonSocial = values[18].Trim().ToUpper().Replace("¥", "Ñ").Replace("Á", "A").Replace("É", "E").Replace("Í", "I").Replace("Ó", "O").Replace("Ú", "U").Replace("'", "").Replace("�", "Ñ");
                                cDato.StrRazonSocial = Regex.Replace(cDato.StrRazonSocial, @"\s+", " ");
                                cDato.StrIdentificador = values[10].Trim().Replace("-", "").Replace(" ", "");
                                cDato.LngNroFactura = Convert.ToInt64(values[14]);
                                cDato.LngNroAutorizacion = Convert.ToInt64(values[15]);
                                cDato.StrNroReferencia = values[19].Trim();
                                cDato.LngNroAutVenta = Convert.ToInt64(values[23]);
                                cDato.StrProducto = values[28].Trim();
                                cDato.DecVolumen = Convert.ToDecimal(values[29]);
                                cDato.DecPrecio = Convert.ToDecimal(values[30]);
                                cDato.DecImporte = Convert.ToDecimal(values[31]);
                                cDato.DecIdTipoCreditoFiscal = (cDato.LngNroFactura > 0) ? (int)EnumTipoCredFiscal.Si : (int)EnumTipoCredFiscal.No;
                                cDato.DecIdTipoOp = Convert.ToDecimal(cmbTipoOperacion.Value);
                                cDato.DecOrden = 1;

                                strFecha = datFecha.Year + datFecha.Month.ToString().PadLeft(2, '0') + datFecha.Day.ToString().PadLeft(2, '0') + datFecha.Hour.ToString().PadLeft(2, '0') + datFecha.Minute.ToString().PadLeft(2, '0') + datFecha.Second.ToString().PadLeft(2, '0');
                                datFechaReg = DateTime.Now;
                                estadoTra = (cDato.StrEstado == "A") ? (int)EnumEstadoMov.Anulado : (int)EnumEstadoMov.Activo;
                                lstTransacciones = new List<ObjTransaccion>();
                                controlBinario = 1;

                                if (Convert.ToDecimal(values[14].Trim()) > 0)
                                {
                                    objFactura = new ObjTransaccion();
                                    objFactura.idMovimiento = 0;
                                    objFactura.idTransaccion = 0;
                                    objFactura.idTransaccionPadre = 0;
                                    objFactura.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                    objFactura.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString().PadLeft(2, '0'));
                                    objFactura.idTipoTransaccion = (decimal)((values[16].Trim().Length > 4) ? EnumTipoTransaccion.FacturaComputarizada : EnumTipoTransaccion.FacturaManual);
                                    objFactura.fechaEmision = Convert.ToInt64(strFecha);
                                    objFactura.numeroTransaccion = values[14].Trim();
                                    objFactura.numeroAutorizacion = Convert.ToDecimal(values[15].Trim());
                                    objFactura.codigoControl = values[16].Trim();
                                    objFactura.nitConsumidor = Convert.ToDecimal(values[17].Trim());
                                    objFactura.razonSocial = cDato.StrRazonSocial;
                                    objFactura.precioUnitario = Convert.ToDecimal(values[30].Trim());
                                    objFactura.precioTotal = Convert.ToDecimal(values[31].Trim());
                                    objFactura.usuarioTransaccion = values[26].Trim();
                                    objFactura.observaciones = "";
                                    objFactura.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                    objFactura.idEstadoTransaccion = estadoTra;
                                    objFactura.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.Si;
                                    objFactura.idFormaPago = (int)EnumTiposPago.Efectivo;
                                    objFactura.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                    objFactura.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                    objFactura.codigoSeguridad = 0;
                                    objFactura.idAutorizacionDato = 0;
                                    objFactura.binarioControl = controlBinario + "000000000";
                                    objFactura.fechaOperacion = Convert.ToInt64(strFecha);
                                    objFactura.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                    objFactura.codigo = "";
                                    lstTransacciones.Add(objFactura);
                                    controlBinario = 0;
                                }

                                if (values[19].Trim().Length > 0)
                                {
                                    objDocumento = new ObjTransaccion();
                                    objDocumento.idMovimiento = 0;
                                    objDocumento.idTransaccion = 0;
                                    objDocumento.idTransaccionPadre = 0;
                                    objDocumento.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                    objDocumento.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                    objDocumento.idTipoTransaccion = (decimal)EnumTipoTransaccion.NotaMovimiento;
                                    objDocumento.fechaEmision = Convert.ToInt64(strFecha);
                                    objDocumento.numeroTransaccion = values[19].Trim();
                                    objDocumento.numeroAutorizacion = 0;
                                    objDocumento.codigoControl = "";
                                    objDocumento.nitConsumidor = 0;
                                    objDocumento.razonSocial = "";
                                    objDocumento.precioUnitario = 0;
                                    objDocumento.precioTotal = 0;
                                    objDocumento.usuarioTransaccion = values[26].Trim();
                                    objDocumento.observaciones = "";
                                    objDocumento.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                    objDocumento.idEstadoTransaccion = estadoTra;
                                    objDocumento.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.No;
                                    objDocumento.idFormaPago = (decimal)EnumTiposPago.Efectivo;
                                    objDocumento.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                    objDocumento.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                    objDocumento.codigoSeguridad = 0;
                                    objDocumento.idAutorizacionDato = 0;
                                    objDocumento.binarioControl = controlBinario + "000000000";
                                    objDocumento.fechaOperacion = Convert.ToInt64(strFecha);
                                    objDocumento.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                    objDocumento.codigo = "";
                                    lstTransacciones.Add(objDocumento);
                                    controlBinario = 0;
                                }

                                if (values[20].Trim().Length > 0)
                                {
                                    objDocumento = new ObjTransaccion();
                                    objDocumento.idMovimiento = 0;
                                    objDocumento.idTransaccion = 0;
                                    objDocumento.idTransaccionPadre = 0;
                                    objDocumento.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                    objDocumento.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                    objDocumento.idTipoTransaccion = (decimal)EnumTipoTransaccion.AutAasana;
                                    objDocumento.fechaEmision = Convert.ToInt64(strFecha);
                                    objDocumento.numeroTransaccion = values[20].Trim();
                                    objDocumento.numeroAutorizacion = 0;
                                    objDocumento.codigoControl = "";
                                    objDocumento.nitConsumidor = Convert.ToDecimal(values[22].Trim());
                                    objDocumento.razonSocial = values[21].Trim().ToUpper().Replace("¥", "Ñ").Replace("Á", "A").Replace("É", "E").Replace("Í", "I").Replace("Ó", "O").Replace("Ú", "U").Replace("'", "").Replace("�", "Ñ");
                                    objDocumento.razonSocial = Regex.Replace(cDato.StrRazonSocial, @"\s+", " ");
                                    objDocumento.precioUnitario = 0;
                                    objDocumento.precioTotal = 0;
                                    objDocumento.usuarioTransaccion = values[26].Trim();
                                    objDocumento.observaciones = "";
                                    objDocumento.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                    objDocumento.idEstadoTransaccion = estadoTra;
                                    objDocumento.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.No;
                                    objDocumento.idFormaPago = (decimal)EnumTiposPago.Efectivo;
                                    objDocumento.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                    objDocumento.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                    objDocumento.codigoSeguridad = 0;
                                    objDocumento.idAutorizacionDato = 0;
                                    objDocumento.binarioControl = controlBinario + "000000000";
                                    objDocumento.fechaOperacion = Convert.ToInt64(strFecha);
                                    objDocumento.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                    objDocumento.codigo = "";
                                    lstTransacciones.Add(objDocumento);
                                }

                                var decIdTipoDespacho = 33;
                                if (Convert.ToDecimal(values[23].Trim()) > 0)
                                {
                                    objDocumento = new ObjTransaccion();
                                    objDocumento.idMovimiento = 0;
                                    objDocumento.idTransaccion = 0;
                                    objDocumento.idTransaccionPadre = 0;
                                    objDocumento.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                    objDocumento.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                    objDocumento.idTipoTransaccion = (decimal)EnumTipoTransaccion.AutSustCtrl;
                                    objDocumento.fechaEmision = Convert.ToInt64(strFecha);
                                    objDocumento.numeroTransaccion = values[23].Trim();
                                    objDocumento.numeroAutorizacion = 0;
                                    objDocumento.codigoControl = "";
                                    objDocumento.nitConsumidor = 0;
                                    objDocumento.razonSocial = "";
                                    objDocumento.precioUnitario = 0;
                                    objDocumento.precioTotal = 0;
                                    objDocumento.usuarioTransaccion = values[26].Trim();
                                    objDocumento.observaciones = "";
                                    objDocumento.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                    objDocumento.idEstadoTransaccion = estadoTra;
                                    objDocumento.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.No;
                                    objDocumento.idFormaPago = (decimal)EnumTiposPago.Efectivo;
                                    objDocumento.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                    objDocumento.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                    objDocumento.codigoSeguridad = 0;
                                    objDocumento.idAutorizacionDato = 0;
                                    objDocumento.binarioControl = "0000000000";
                                    objDocumento.fechaOperacion = Convert.ToInt64(strFecha);
                                    objDocumento.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                    objDocumento.codigo = "";
                                    lstTransacciones.Add(objDocumento);
                                    decIdTipoDespacho = 34;
                                }
                                if (values[24].Trim().Length > 0)
                                {
                                    objDocumento = new ObjTransaccion();
                                    objDocumento.idMovimiento = 0;
                                    objDocumento.idTransaccion = 0;
                                    objDocumento.idTransaccionPadre = 0;
                                    objDocumento.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                    objDocumento.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                    objDocumento.idTipoTransaccion = (decimal)EnumTipoTransaccion.AutAnhVfa;
                                    objDocumento.fechaEmision = Convert.ToInt64(strFecha);
                                    objDocumento.numeroTransaccion = values[24].Trim();
                                    objDocumento.numeroAutorizacion = 0;
                                    objDocumento.codigoControl = "";
                                    objDocumento.nitConsumidor = 0;
                                    objDocumento.razonSocial = "";
                                    objDocumento.precioUnitario = 0;
                                    objDocumento.precioTotal = 0;
                                    objDocumento.usuarioTransaccion = values[26].Trim();
                                    objDocumento.observaciones = "";
                                    objDocumento.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                    objDocumento.idEstadoTransaccion = estadoTra;
                                    objDocumento.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.No;
                                    objDocumento.idFormaPago = (decimal)EnumTiposPago.Efectivo;
                                    objDocumento.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                    objDocumento.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                    objDocumento.codigoSeguridad = 0;
                                    objDocumento.idAutorizacionDato = 0;
                                    objDocumento.binarioControl = "0000000000";
                                    objDocumento.fechaOperacion = Convert.ToInt64(strFecha);
                                    objDocumento.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                    objDocumento.codigo = "";
                                    lstTransacciones.Add(objDocumento);
                                    decIdTipoDespacho = 34;
                                }
                                if (values[25].Trim().Length > 0)
                                {
                                    var arreglo = values[25].Trim().Split('|');
                                    for (int i = 0; i < arreglo.Count(); i++)
                                    {
                                        objDocumento = new ObjTransaccion();
                                        objDocumento.idMovimiento = 0;
                                        objDocumento.idTransaccion = 0;
                                        objDocumento.idTransaccionPadre = 0;
                                        objDocumento.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                        objDocumento.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                        objDocumento.idTipoTransaccion = (decimal)EnumTipoTransaccion.DocCalidad;
                                        objDocumento.fechaEmision = Convert.ToInt64(strFecha);
                                        objDocumento.numeroTransaccion = arreglo[i];
                                        objDocumento.numeroAutorizacion = 0;
                                        objDocumento.codigoControl = "";
                                        objDocumento.nitConsumidor = 0;
                                        objDocumento.razonSocial = "";
                                        objDocumento.precioUnitario = 0;
                                        objDocumento.precioTotal = 0;
                                        objDocumento.usuarioTransaccion = values[26].Trim();
                                        objDocumento.observaciones = "";
                                        objDocumento.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                        objDocumento.idEstadoTransaccion = estadoTra;
                                        objDocumento.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.No;
                                        objDocumento.idFormaPago = (int)EnumTiposPago.Efectivo;
                                        objDocumento.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                        objDocumento.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                        objDocumento.codigoSeguridad = 0;
                                        objDocumento.idAutorizacionDato = 0;
                                        objDocumento.binarioControl = "0000000000";
                                        objDocumento.fechaOperacion = Convert.ToInt64(strFecha);
                                        objDocumento.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                        objDocumento.codigo = "";
                                        lstTransacciones.Add(objDocumento);
                                    }
                                }

                                objNuevoTipoReg = new ObjProtoMovimientoTransaccion();
                                objNuevoTipoReg.idMovimientoPadre = 0;
                                objNuevoTipoReg.idTipoActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                objNuevoTipoReg.idTipoOperacion = Convert.ToDecimal(cmbTipoOperacion.Value);
                                objNuevoTipoReg.idConsumidor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString());
                                objNuevoTipoReg.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                objNuevoTipoReg.fechaRegistroMovimiento = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                objNuevoTipoReg.fechaOperacion = Convert.ToInt64(strFecha);
                                objNuevoTipoReg.idTipoMovimientoFlujo = (int)EnumTipoMovimientoFlujo.Fisico;
                                objNuevoTipoReg.idTipoMovimiento = (values[7].Trim() == "BOLIVIA") ? (int)EnumTipoMovimiento.NacNac : (int)EnumTipoMovimiento.NacInt;
                                objNuevoTipoReg.idEstadoMovimiento = estadoTra;
                                objNuevoTipoReg.idFuenteOrigenDato = (decimal)EnumTipoRegistro.OrigenCsv;
                                objNuevoTipoReg.idUbicacionOperacion = MCodificaParametrica((int)EnumTipoParametrica.Pais, values[7].Trim());
                                objNuevoTipoReg.idUnidadMedidaHidrocarburo = MCodificaParametrica((int)EnumTipoParametrica.UnidadMedida, values[28].Trim());
                                objNuevoTipoReg.idHidrocarburo = MCodificaParametrica((int)EnumTipoParametrica.Producto, values[28].Trim());
                                objNuevoTipoReg.cantidadHidrocarburo = cDato.DecVolumen;
                                objNuevoTipoReg.identificador = cDato.StrIdentificador;
                                objNuevoTipoReg.subIdentificador = "";
                                objNuevoTipoReg.observacion = "";
                                objNuevoTipoReg.tipoInfraestructura = MCodificaParametrica((int)EnumTipoParametrica.TipoInfraestructura, values[2].Trim());
                                objNuevoTipoReg.idInfraestructura = MCodificaParametrica((int)EnumTipoParametrica.Infraestructura, values[2].Trim() + "|" + values[3].Trim());
                                objNuevoTipoReg.idTipoMedicion = -99999;
                                objNuevoTipoReg.cantidadAguaLitros = 0;
                                objNuevoTipoReg.idUnidadTemperatura = (decimal)EnumUnidadTemperatura.GradoCentigrado;
                                objNuevoTipoReg.temperatura = Convert.ToDecimal(values[8]);
                                objNuevoTipoReg.gradoApi = values[9].Trim();
                                objNuevoTipoReg.idEntidad = MCodificaParametrica((int)EnumTipoParametrica.Entidad, values[4].Trim());
                                objNuevoTipoReg.idTipoMedioTransporte = MCodificaParametrica((int)EnumTipoParametrica.TipoInfraestructura, values[5].Trim());
                                objNuevoTipoReg.idMedioTransporte = MCodificaParametrica((int)EnumTipoParametrica.Infraestructura, values[5].Trim() + "|" + values[6].Trim());
                                objNuevoTipoReg.idUnidadEnvasesTransporte = MCodificaParametrica((int)EnumTipoParametrica.TipoContenedor, values[13].Trim());
                                objNuevoTipoReg.cantidadEnvasesTransporte = 0;
                                objNuevoTipoReg.binarioControl = "00000-0000000000-0000000000-00000";
                                objNuevoTipoReg.idIntegracion = Convert.ToDecimal(values[1]);
                                objNuevoTipoReg.idTransaccionBaseExterna = "";
                                objNuevoTipoReg.codigo = values[12].Trim();
                                decimal dec;
                                objNuevoTipoReg.barcode = (decimal.TryParse(values[11].Trim(), out dec)) ? Convert.ToDecimal(values[11].Trim()) : 0;
                                objNuevoTipoReg.listaTransaccion = JsonSerializer.SerializeToString(lstTransacciones);

                                objNuevoTipoReg.jsonReferencia = "";
                                objNuevoTipoReg.estadoJsonRef = -99999;
                                objNuevoTipoReg.idCuenta = -99999;
                                objNuevoTipoReg.idCuentaAux = -99999;
                                objNuevoTipoReg.idParamGlobal = -99999;
                                objNuevoTipoReg.idParamEspec = -99999;
                                objNuevoTipoReg.idTipoConsumidor = decIdTipoDespacho;
                                objNuevoTipoReg.idTipoInfraAux = -99999;
                                objNuevoTipoReg.idInfraAux = -99999;

                                #endregion
                                break;
                        }
                        cDato.objProtoMovimiento = objNuevoTipoReg;
                        lstRegistros.Add(cDato);
                        lngNoRegCorrect++;
                    }
                    else
                    {
                        var cRegError = new CRegistrosError();
                        cRegError.LngId = lngNoReg;
                        cRegError.StrIdVenta = values[1].Trim();
                        cRegError.LngFechaEmision = values[0].Trim();
                        cRegError.LngNit = "";
                        cRegError.LngNroAutorizacion = values[15].Trim();
                        cRegError.LngNroFactura = values[14].Trim();
                        cRegError.LngNroReferencia = values[19].Trim();
                        cRegError.StrErrores = strMensaje.Trim();
                        lstRegistroE.Add(cRegError);
                        lngNoRegInCorrect++;
                    }
                }
                catch (Exception ex)
                {
                    var cRegError = new CRegistrosError();
                    cRegError.LngId = lngNoReg;
                    cRegError.StrIdVenta = lngNoRegCorrect.ToString();
                    cRegError.LngFechaEmision = "";
                    cRegError.LngNit = "";
                    cRegError.LngNroAutorizacion = "";
                    cRegError.LngNroFactura = "";
                    cRegError.LngNroReferencia = "";
                    cRegError.StrErrores = "ERROR:" + ex.Message;
                    lstRegistroE.Add(cRegError);
                    lngNoRegInCorrect++;
                }
                lngNoReg++;
            }

            //var ojo2 = JsonSerializer.SerializeToString(lstRegistros);
            dgvTransacciones.DataSource = lstRegistros;
            dgvTransacciones.DataBind();
            Session[CVariablesSesion.GrillaTransaccion] = lstRegistros;

            dgvTransaccionError.DataSource = lstRegistroE;
            dgvTransaccionError.DataBind();
            Session[CVariablesSesion.GrillaRegObservado] = lstRegistroE;
        }

        private void MCargaGlp(string strRuta)//
        {
            var reader = new StreamReader(File.OpenRead(@strRuta));
            var lstRegistros = new List<CRegistrosImportados>();
            var lstRegistroE = new List<CRegistrosError>();
            long lngNoRegCorrect = 1, lngNoRegInCorrect = 1, lngNoReg = 1;
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

                    var to = cmbTipoOperacion.Text.Split(';');
                    var strTipoOp = to[1].Substring(2, 2);
                    string strMensaje = string.Empty;
                    if (MValidaLineaGlp(values, ref strMensaje))
                    {
                        var cDato = new CRegistrosImportados();
                        int controlBinario;
                        switch (strTipoOp)
                        {
                            case "DG":
                                #region Valida y genera objeto
                                cDato.LngId = lngNoRegCorrect;
                                cDato.StrIdVenta = lngNoRegCorrect.ToString();
                                datFecha = DateTime.Parse(values[0].Trim());
                                cDato.StrFechaEmision = datFecha.ToString();
                                cDato.StrTipoMov = (values[3].Trim() == "BOLIVIA") ? "NAC" : "INT";
                                cDato.StrEstado = values[6].Trim();//
                                cDato.LngNit = Convert.ToInt64(values[8]);
                                cDato.StrRazonSocial = values[9].Trim().ToUpper().Replace("¥", "Ñ").Replace("Á", "A").Replace("É", "E").Replace("Í", "I").Replace("Ó", "O").Replace("Ú", "U").Replace("'", "").Replace("�", "Ñ");
                                cDato.StrRazonSocial = Regex.Replace(cDato.StrRazonSocial, @"\s+", " ");
                                cDato.StrIdentificador = values[1].Trim().Replace("-", "").Replace(" ", "");
                                cDato.LngNroFactura = Convert.ToInt64(values[10]);
                                cDato.LngNroAutorizacion = Convert.ToInt64(values[11]);
                                cDato.StrNroReferencia = values[15];//sirel
                                cDato.LngNroAutVenta = 0;//Convert.ToInt64(values[17]);
                                cDato.StrProducto = values[4].Trim();
                                cDato.DecVolumen = Convert.ToDecimal(values[5].Trim().Replace(',', '.'));
                                cDato.DecPrecio = Convert.ToDecimal(values[13].Trim().Replace(',', '.'));
                                cDato.DecImporte = Convert.ToDecimal(values[14].Trim().Replace(',', '.'));
                                cDato.DecIdTipoCreditoFiscal = (Convert.ToDecimal(values[10]) > 0 && Convert.ToDecimal(values[13]) > 0) ? (int)EnumTipoCredFiscal.Si : (int)EnumTipoCredFiscal.No;
                                cDato.objProtoMovimiento = new ObjProtoMovimientoTransaccion();
                                cDato.DecIdTipoOp = Convert.ToDecimal(cmbTipoOperacion.Value);
                                cDato.DecOrden = 1;

                                strFecha = datFecha.Year + datFecha.Month.ToString().PadLeft(2, '0') + datFecha.Day.ToString().PadLeft(2, '0') + datFecha.Hour.ToString().PadLeft(2, '0') + datFecha.Minute.ToString().PadLeft(2, '0') + datFecha.Second.ToString().PadLeft(2, '0');
                                datFechaReg = DateTime.Now;
                                estadoTra = (cDato.StrEstado == "A") ? (int)EnumEstadoMov.Anulado : (int)EnumEstadoMov.Activo;
                                lstTransacciones = new List<ObjTransaccion>();
                                var intFormaPago = (values[7].Trim() == "VAL") ? 3 : (values[7].Trim() == "PRE") ? 2 : 1;
                                controlBinario = 1;

                                if (Convert.ToDecimal(values[10].Trim()) > 0 && Convert.ToDecimal(values[13].Trim()) > 0)
                                {
                                    objFactura = new ObjTransaccion();
                                    objFactura.idMovimiento = 0;
                                    objFactura.idTransaccion = 0;
                                    objFactura.idTransaccionPadre = 0;
                                    objFactura.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                    objFactura.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                    objFactura.idTipoTransaccion = (decimal)((values[12].Trim().Length > 0) ? EnumTipoTransaccion.FacturaComputarizada : EnumTipoTransaccion.FacturaManual);
                                    objFactura.fechaEmision = Convert.ToInt64(strFecha);
                                    objFactura.numeroTransaccion = values[10].Trim();
                                    objFactura.numeroAutorizacion = Convert.ToDecimal(values[11]);
                                    objFactura.codigoControl = values[12].Trim();
                                    objFactura.nitConsumidor = Convert.ToDecimal(values[8].Trim());
                                    objFactura.razonSocial = cDato.StrRazonSocial;
                                    objFactura.precioUnitario = Convert.ToDecimal(values[13].Trim());
                                    objFactura.precioTotal = Convert.ToDecimal(values[14].Trim());
                                    objFactura.usuarioTransaccion = values[17].Trim();
                                    objFactura.observaciones = "";
                                    objFactura.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                    objFactura.idEstadoTransaccion = estadoTra;
                                    objFactura.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.Si;
                                    objFactura.idFormaPago = intFormaPago;
                                    objFactura.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                    objFactura.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                    objFactura.codigoSeguridad = 0;
                                    objFactura.idAutorizacionDato = 0;
                                    objFactura.binarioControl = controlBinario + "000000000";
                                    objFactura.fechaOperacion = Convert.ToInt64(strFecha);
                                    objFactura.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                    objFactura.codigo = "";

                                    lstTransacciones.Add(objFactura);
                                    controlBinario = 0;
                                }
                                // AUTORIZACION DE VENTA
                                if (values[18].Trim() != "0")
                                {
                                    objDocumento = new ObjTransaccion();
                                    objDocumento.idMovimiento = 0;
                                    objDocumento.idTransaccion = 0;
                                    objDocumento.idTransaccionPadre = 0;
                                    objDocumento.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                    objDocumento.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                    objDocumento.idTipoTransaccion = (decimal)EnumTipoTransaccion.AutDespacho; // (values[16].Trim() == "CC") ? (decimal)EnumTipoTransaccion.AutComConjunto : (decimal)EnumTipoTransaccion.AutSustCtrl;
                                    objDocumento.fechaEmision = Convert.ToInt64(strFecha);
                                    objDocumento.numeroTransaccion = values[18].Trim();
                                    objDocumento.numeroAutorizacion = Convert.ToDecimal(values[18].Trim());
                                    objDocumento.codigoControl = "";
                                    objDocumento.nitConsumidor = Convert.ToDecimal(values[17].Trim());
                                    objDocumento.razonSocial = "";
                                    objDocumento.precioUnitario = 0;
                                    objDocumento.precioTotal = 0;
                                    objDocumento.usuarioTransaccion = values[17].Trim();
                                    objDocumento.observaciones = values[19];
                                    objDocumento.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                    objDocumento.idEstadoTransaccion = estadoTra;
                                    objDocumento.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.No;
                                    objDocumento.idFormaPago = intFormaPago;
                                    objDocumento.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                    objDocumento.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                    objDocumento.codigoSeguridad = 0;
                                    objDocumento.idAutorizacionDato = 0;
                                    objDocumento.binarioControl = controlBinario + "000000000";
                                    objDocumento.fechaOperacion = Convert.ToInt64(strFecha);
                                    objDocumento.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                    objDocumento.codigo = "";
                                    lstTransacciones.Add(objDocumento);
                                    controlBinario = 0;
                                }
                                //doc principal
                                if (values[15].Trim().Length > 0)
                                {
                                    objDocumento = new ObjTransaccion();
                                    objDocumento.idMovimiento = 0;
                                    objDocumento.idTransaccion = 0;
                                    objDocumento.idTransaccionPadre = 0;
                                    objDocumento.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                    objDocumento.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                    objDocumento.idTipoTransaccion = (decimal)EnumTipoTransaccion.DocRespAsociado;
                                    objDocumento.fechaEmision = Convert.ToInt64(strFecha);
                                    objDocumento.numeroTransaccion = values[15].Trim();
                                    objDocumento.numeroAutorizacion = 0;
                                    objDocumento.codigoControl = "";
                                    objDocumento.nitConsumidor = 0;
                                    objDocumento.razonSocial = values[16].Trim();
                                    objDocumento.precioUnitario = 0;
                                    objDocumento.precioTotal = 0;
                                    objDocumento.usuarioTransaccion = values[17].Trim();
                                    objDocumento.observaciones = "";
                                    objDocumento.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                    objDocumento.idEstadoTransaccion = estadoTra;
                                    objDocumento.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.No;
                                    objDocumento.idFormaPago = intFormaPago;
                                    objDocumento.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                    objDocumento.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                    objDocumento.codigoSeguridad = 0;
                                    objDocumento.idAutorizacionDato = 0;
                                    objDocumento.binarioControl = controlBinario + "000000000";
                                    objDocumento.fechaOperacion = Convert.ToInt64(strFecha);
                                    objDocumento.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                    objDocumento.codigo = "";
                                    lstTransacciones.Add(objDocumento);
                                }




                                objNuevoTipoReg = new ObjProtoMovimientoTransaccion();
                                objNuevoTipoReg.idMovimientoPadre = 0;
                                objNuevoTipoReg.idTipoActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                objNuevoTipoReg.idTipoOperacion = Convert.ToDecimal(cmbTipoOperacion.Value);
                                objNuevoTipoReg.idConsumidor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString());
                                objNuevoTipoReg.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                objNuevoTipoReg.fechaRegistroMovimiento = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                objNuevoTipoReg.fechaOperacion = Convert.ToInt64(strFecha);
                                objNuevoTipoReg.idTipoMovimientoFlujo = (values[8].Trim() == "PRE") ? (int)EnumTipoMovimientoFlujo.Contable : (int)EnumTipoMovimientoFlujo.Fisico;
                                objNuevoTipoReg.idTipoMovimiento = (cDato.StrTipoMov == "NAC") ? (int)EnumTipoMovimiento.NacNac : (int)EnumTipoMovimiento.NacInt;
                                objNuevoTipoReg.idEstadoMovimiento = estadoTra;
                                objNuevoTipoReg.idFuenteOrigenDato = (decimal)EnumTipoRegistro.OrigenCsv;
                                objNuevoTipoReg.idUbicacionOperacion = MCodificaParametrica((int)EnumTipoParametrica.Pais, values[3].Trim());
                                objNuevoTipoReg.idUnidadMedidaHidrocarburo = MCodificaParametrica((int)EnumTipoParametrica.UnidadMedida, values[4].Trim());
                                objNuevoTipoReg.idHidrocarburo = MCodificaParametrica((int)EnumTipoParametrica.Producto, values[4].Trim());
                                objNuevoTipoReg.cantidadHidrocarburo = cDato.DecVolumen;
                                objNuevoTipoReg.identificador = cDato.StrIdentificador;
                                objNuevoTipoReg.subIdentificador = "";//values[4].Trim();//??
                                objNuevoTipoReg.observacion = "";

                                //*****************************??
                                /*
                                if (to[1].Substring(4, 2) == "EX")
                                {
                                    objNuevoTipoReg.idTipoMedioTransporte = MCodificaParametrica((int)EnumTipoParametrica.TipoInfraestructura, values[1].Trim());
                                    objNuevoTipoReg.idMedioTransporte = MCodificaParametrica((int)EnumTipoParametrica.Infraestructura, values[1].Trim() + "|" + values[2].Trim());
                                    objNuevoTipoReg.tipoInfraestructura = -99999;
                                    objNuevoTipoReg.idInfraestructura = -99999;
                                }
                                else
                                {
                                    objNuevoTipoReg.idTipoMedioTransporte = -99999;
                                    objNuevoTipoReg.idMedioTransporte = -99999;
                                    objNuevoTipoReg.tipoInfraestructura = MCodificaParametrica((int)EnumTipoParametrica.TipoInfraestructura, values[1].Trim());
                                    objNuevoTipoReg.idInfraestructura = MCodificaParametrica((int)EnumTipoParametrica.Infraestructura, values[1].Trim() + "|" + values[2].Trim());
                                }*/

                                objNuevoTipoReg.tipoInfraestructura = -99999;
                                objNuevoTipoReg.idInfraestructura = -99999;
                                objNuevoTipoReg.idTipoMedioTransporte = -99999;
                                objNuevoTipoReg.idMedioTransporte = -99999;

                                objNuevoTipoReg.idUnidadEnvasesTransporte = -99999;// MCodificaParametrica((int)EnumTipoParametrica.TipoContenedor, "GARR");//values[1].Trim());
                                objNuevoTipoReg.idEntidad = -99999;
                                objNuevoTipoReg.idTipoMedicion = -99999;
                                objNuevoTipoReg.cantidadAguaLitros = 0;
                                objNuevoTipoReg.idUnidadTemperatura = -99999;
                                objNuevoTipoReg.temperatura = 0;
                                objNuevoTipoReg.gradoApi = "";
                                objNuevoTipoReg.cantidadEnvasesTransporte = 0;
                                objNuevoTipoReg.binarioControl = "00000-0000000000-0000000000-00000";
                                objNuevoTipoReg.idIntegracion = -99999;
                                objNuevoTipoReg.idTransaccionBaseExterna = "";
                                objNuevoTipoReg.fechaOperacionDia = 0;
                                objNuevoTipoReg.codigo = "";
                                objNuevoTipoReg.barcode = Convert.ToDecimal(values[2].Trim());
                                objNuevoTipoReg.listaTransaccion = JsonSerializer.SerializeToString(lstTransacciones);

                                objNuevoTipoReg.jsonReferencia = "";
                                objNuevoTipoReg.estadoJsonRef = -99999;
                                objNuevoTipoReg.idCuenta = -99999;
                                objNuevoTipoReg.idCuentaAux = -99999;
                                objNuevoTipoReg.idParamGlobal = -99999;
                                objNuevoTipoReg.idParamEspec = -99999;
                                objNuevoTipoReg.idTipoConsumidor = 33;//-99999;//(objNuevoTipoReg.idUnidadEnvasesTransporte == -99999) ? 32 : 31;//33;//
                                objNuevoTipoReg.idTipoInfraAux = -99999;
                                objNuevoTipoReg.idInfraAux = -99999;

                                #endregion
                                break;
                            case "RG":
                                #region Valida y genera objeto
                                cDato.LngId = lngNoRegCorrect;
                                cDato.StrIdVenta = lngNoRegCorrect.ToString();
                                datFecha = DateTime.Parse(values[0].Trim());
                                cDato.StrFechaEmision = datFecha.ToString();
                                cDato.StrTipoMov = "NAC";
                                cDato.StrEstado = values[6].Trim();
                                cDato.LngNit = Convert.ToInt64(values[7]);
                                cDato.StrRazonSocial = values[8].Trim().ToUpper().Replace("¥", "Ñ").Replace("Á", "A").Replace("É", "E").Replace("Í", "I").Replace("Ó", "O").Replace("Ú", "U").Replace("'", "").Replace("�", "Ñ");
                                cDato.StrRazonSocial = Regex.Replace(cDato.StrRazonSocial, @"\s+", " ");
                                cDato.StrIdentificador = values[2].Trim().Replace("-", "").Replace(" ", "");
                                cDato.LngNroFactura = Convert.ToInt64(values[10]);
                                cDato.LngNroAutorizacion = Convert.ToInt64(values[11]);
                                cDato.StrNroReferencia = values[15].Trim();
                                //cDato.LngNroAutVenta = Convert.ToInt64(values[24]);
                                cDato.StrProducto = values[4].Trim();
                                cDato.DecVolumen = Convert.ToDecimal(values[5].Trim().Replace(',', '.'));
                                cDato.DecPrecio = Convert.ToDecimal(values[13].Trim().Replace(',', '.'));
                                cDato.DecImporte = Convert.ToDecimal(values[14].Trim().Replace(',', '.'));
                                cDato.DecIdTipoCreditoFiscal = (Convert.ToDecimal(values[10]) > 0 && Convert.ToDecimal(values[13]) > 0) ? (int)EnumTipoCredFiscal.Si : (int)EnumTipoCredFiscal.No;
                                cDato.objProtoMovimiento = new ObjProtoMovimientoTransaccion();
                                cDato.DecIdTipoOp = Convert.ToDecimal(cmbTipoOperacion.Value);
                                cDato.DecOrden = 1;

                                strFecha = datFecha.Year + datFecha.Month.ToString().PadLeft(2, '0') + datFecha.Day.ToString().PadLeft(2, '0') + datFecha.Hour.ToString().PadLeft(2, '0') + datFecha.Minute.ToString().PadLeft(2, '0') + datFecha.Second.ToString().PadLeft(2, '0');
                                datFechaReg = DateTime.Now;
                                estadoTra = (cDato.StrEstado == "A") ? (int)EnumEstadoMov.Anulado : (int)EnumEstadoMov.Activo;
                                lstTransacciones = new List<ObjTransaccion>();
                                controlBinario = 1;

                                if (Convert.ToDecimal(values[10].Trim()) > 0 && Convert.ToDecimal(values[11].Trim()) > 0)
                                {
                                    objFactura = new ObjTransaccion();
                                    objFactura.idMovimiento = 0;
                                    objFactura.idTransaccion = 0;
                                    objFactura.idTransaccionPadre = 0;
                                    objFactura.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                    objFactura.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                    objFactura.idTipoTransaccion = (decimal)((values[12].Trim().Length > 0) ? EnumTipoTransaccion.FacturaComputarizada : EnumTipoTransaccion.FacturaManual);
                                    objFactura.fechaEmision = CFechas.ConvierteDateTimeLong(DateTime.Parse(values[9].Trim()));
                                    objFactura.numeroTransaccion = values[10].Trim();
                                    objFactura.numeroAutorizacion = Convert.ToDecimal(values[11]);
                                    objFactura.codigoControl = values[12].Trim();
                                    objFactura.nitConsumidor = Convert.ToDecimal(values[7].Trim());
                                    objFactura.razonSocial = cDato.StrRazonSocial;
                                    objFactura.precioUnitario = Convert.ToDecimal(values[13].Trim());
                                    objFactura.precioTotal = Convert.ToDecimal(values[14].Trim());
                                    objFactura.usuarioTransaccion = values[17].Trim();//
                                    objFactura.observaciones = "";
                                    objFactura.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                    objFactura.idEstadoTransaccion = estadoTra;
                                    objFactura.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.Si;
                                    objFactura.idFormaPago = (decimal)EnumTiposPago.Efectivo;
                                    objFactura.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                    objFactura.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                    objFactura.codigoSeguridad = 0;
                                    objFactura.idAutorizacionDato = 0;
                                    objFactura.binarioControl = controlBinario + "000000000";
                                    objFactura.fechaOperacion = Convert.ToInt64(strFecha);
                                    objFactura.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                    objFactura.codigo = "";
                                    lstTransacciones.Add(objFactura);
                                    controlBinario = 0;
                                }
                                /*
                                if (values[18].Trim().Length > 0 && values[19].Trim().Length > 0)
                                {
                                    objDocumento = new ObjTransaccion();
                                    objDocumento.idMovimiento = 0;
                                    objDocumento.idTransaccion = 0;
                                    objDocumento.idTransaccionPadre = 0;
                                    objDocumento.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                    objDocumento.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                    objDocumento.idTipoTransaccion = (decimal)EnumTipoTransaccion.DocRespAsociado;

                                    var datFechaDes = DateTime.Parse(values[18].Trim());
                                    var strFechaDes = datFechaDes.Year + datFechaDes.Month.ToString().PadLeft(2, '0') + datFechaDes.Day.ToString().PadLeft(2, '0') + datFechaDes.Hour.ToString().PadLeft(2, '0') + datFechaDes.Minute.ToString().PadLeft(2, '0') + datFechaDes.Second.ToString().PadLeft(2, '0');
                                    objDocumento.fechaEmision = Convert.ToInt64(strFechaDes);

                                    objDocumento.numeroTransaccion = values[19].Trim();
                                    objDocumento.numeroAutorizacion = Convert.ToDecimal(values[20].Trim());
                                    objDocumento.codigoControl = values[21].Trim();
                                    objDocumento.nitConsumidor = 0;
                                    objDocumento.razonSocial = "";
                                    objDocumento.precioUnitario = 0;
                                    objDocumento.precioTotal = 0;
                                    objDocumento.usuarioTransaccion = values[21].Trim();//
                                    objDocumento.observaciones = "";
                                    objDocumento.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                    objDocumento.idEstadoTransaccion = estadoTra;
                                    objDocumento.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.No;
                                    objDocumento.idFormaPago = (decimal)EnumTiposPago.Efectivo;
                                    objDocumento.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                    objDocumento.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                    objDocumento.codigoSeguridad = 0;
                                    objDocumento.idAutorizacionDato = 0;
                                    objDocumento.binarioControl = controlBinario + "000000000";
                                    objDocumento.fechaOperacion = Convert.ToInt64(strFecha);
                                    objDocumento.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                    //objDocumento.codigo = values[29].Trim();
                                    lstTransacciones.Add(objDocumento);
                                    controlBinario = 0;
                                }*/
                                if (values[15].Trim().Length > 0)
                                {
                                    objDocumento = new ObjTransaccion();
                                    objDocumento.idMovimiento = 0;
                                    objDocumento.idTransaccion = 0;
                                    objDocumento.idTransaccionPadre = 0;
                                    objDocumento.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                    objDocumento.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                    objDocumento.idTipoTransaccion = (decimal)EnumTipoTransaccion.DocRespAsociado;
                                    objDocumento.fechaEmision = Convert.ToInt64(strFecha);
                                    objDocumento.numeroTransaccion = values[15].Trim();
                                    objDocumento.numeroAutorizacion = 0;
                                    objDocumento.codigoControl = "";
                                    objDocumento.nitConsumidor = Convert.ToDecimal(values[17].Trim());
                                    objDocumento.razonSocial = values[16].Trim();
                                    objDocumento.precioUnitario = 0;
                                    objDocumento.precioTotal = 0;
                                    objDocumento.usuarioTransaccion = values[17].Trim();//
                                    objDocumento.observaciones = "";
                                    objDocumento.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                    objDocumento.idEstadoTransaccion = estadoTra;
                                    objDocumento.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.No;
                                    objDocumento.idFormaPago = (decimal)EnumTiposPago.Efectivo;
                                    objDocumento.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                    objDocumento.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                    objDocumento.codigoSeguridad = 0;
                                    objDocumento.idAutorizacionDato = 0;
                                    objDocumento.binarioControl = controlBinario + "000000000";
                                    objDocumento.fechaOperacion = Convert.ToInt64(strFecha);
                                    objDocumento.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                    objDocumento.codigo = "";
                                    lstTransacciones.Add(objDocumento);
                                }
                                /*
                                if (Convert.ToDecimal(values[24].Trim()) > 0)
                                {
                                    objDocumento = new ObjTransaccion();
                                    objDocumento.idMovimiento = 0;
                                    objDocumento.idTransaccion = 0;
                                    objDocumento.idTransaccionPadre = 0;
                                    objDocumento.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                    objDocumento.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                    objDocumento.idTipoTransaccion = (decimal)EnumTipoTransaccion.AutSustCtrl;
                                    objDocumento.fechaEmision = Convert.ToInt64(strFecha);
                                    objDocumento.numeroTransaccion = values[24].Trim();
                                    objDocumento.numeroAutorizacion = 0;
                                    objDocumento.codigoControl = "";
                                    objDocumento.nitConsumidor = 0;
                                    objDocumento.razonSocial = "";
                                    objDocumento.precioUnitario = 0;
                                    objDocumento.precioTotal = 0;
                                    objDocumento.usuarioTransaccion = values[12].Trim();
                                    objDocumento.observaciones = "AUTORIZACION DE COMPRA LOCAL";
                                    objDocumento.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                    objDocumento.idEstadoTransaccion = estadoTra;
                                    objDocumento.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.No;
                                    objDocumento.idFormaPago = (decimal)EnumTiposPago.Efectivo;
                                    objDocumento.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                    objDocumento.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                    objDocumento.codigoSeguridad = 0;
                                    objDocumento.idAutorizacionDato = 0;
                                    objDocumento.binarioControl = "0000000000";
                                    objDocumento.fechaOperacion = Convert.ToInt64(strFecha);
                                    objDocumento.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                    objDocumento.codigo = "";
                                    lstTransacciones.Add(objDocumento);
                                }
                                */
                                /*
                                if (values[8].Trim().Length > 0)
                                {
                                    var arreglo = values[8].Trim().Split('|');
                                    for (int i = 0; i < arreglo.Count(); i++)
                                    {
                                        objDocumento = new ObjTransaccion();
                                        objDocumento.idMovimiento = 0;
                                        objDocumento.idTransaccion = 0;
                                        objDocumento.idTransaccionPadre = 0;
                                        objDocumento.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                        objDocumento.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                        objDocumento.idTipoTransaccion = (decimal)EnumTipoTransaccion.DocPrecinto;
                                        objDocumento.fechaEmision = Convert.ToInt64(strFecha);
                                        objDocumento.numeroTransaccion = arreglo[i];
                                        objDocumento.numeroAutorizacion = 0;
                                        objDocumento.codigoControl = "";
                                        objDocumento.nitConsumidor = 0;
                                        objDocumento.razonSocial = "";
                                        objDocumento.precioUnitario = 0;
                                        objDocumento.precioTotal = 0;
                                        objDocumento.usuarioTransaccion = values[12].Trim();
                                        objDocumento.observaciones = "";
                                        objDocumento.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                        objDocumento.idEstadoTransaccion = estadoTra;
                                        objDocumento.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.No;
                                        objDocumento.idFormaPago = (int)EnumTiposPago.Efectivo;
                                        objDocumento.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                        objDocumento.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                        objDocumento.codigoSeguridad = 0;
                                        objDocumento.idAutorizacionDato = 0;
                                        objDocumento.binarioControl = "0000000000";
                                        objDocumento.fechaOperacion = Convert.ToInt64(strFecha);
                                        objDocumento.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                        objDocumento.codigo = "";
                                        lstTransacciones.Add(objDocumento);
                                    }
                                }
                                */
                                objNuevoTipoReg = new ObjProtoMovimientoTransaccion();
                                objNuevoTipoReg.idMovimientoPadre = 0;
                                objNuevoTipoReg.idTipoActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                objNuevoTipoReg.idTipoOperacion = Convert.ToDecimal(cmbTipoOperacion.Value);
                                objNuevoTipoReg.idConsumidor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString());
                                objNuevoTipoReg.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                objNuevoTipoReg.fechaRegistroMovimiento = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString().PadLeft(2, '0') + datFechaReg.Minute.ToString().PadLeft(2, '0') + datFechaReg.Second.ToString().PadLeft(2, '0'));
                                objNuevoTipoReg.fechaOperacion = Convert.ToInt64(strFecha);
                                objNuevoTipoReg.idTipoMovimientoFlujo = (int)EnumTipoMovimientoFlujo.Fisico;
                                objNuevoTipoReg.idTipoMovimiento = (int)EnumTipoMovimiento.NacNac;
                                objNuevoTipoReg.idEstadoMovimiento = (cDato.StrEstado == "A") ? (int)EnumEstadoMov.Anulado : (int)EnumEstadoMov.Activo;
                                objNuevoTipoReg.idFuenteOrigenDato = (decimal)EnumTipoRegistro.OrigenCsv;
                                objNuevoTipoReg.idUbicacionOperacion = MCodificaParametrica((int)EnumTipoParametrica.Pais, "BOLIVIA");
                                objNuevoTipoReg.idUnidadMedidaHidrocarburo = MCodificaParametrica((int)EnumTipoParametrica.UnidadMedida, values[4].Trim());
                                objNuevoTipoReg.idHidrocarburo = MCodificaParametrica((int)EnumTipoParametrica.Producto, values[4].Trim());
                                objNuevoTipoReg.cantidadHidrocarburo = cDato.DecVolumen;
                                objNuevoTipoReg.identificador = cDato.StrIdentificador;
                                //objNuevoTipoReg.subIdentificador = values[5].Trim();
                                objNuevoTipoReg.observacion = "";
                                objNuevoTipoReg.tipoInfraestructura = MCodificaParametrica((int)EnumTipoParametrica.TipoInfraestructura, "-99999".Trim());
                                objNuevoTipoReg.idInfraestructura = MCodificaParametrica((int)EnumTipoParametrica.Infraestructura, "-99999".Trim() + "|" + "-99999".Trim());
                                objNuevoTipoReg.idTipoMedioTransporte = -99999;//(values[7].Trim().Length == 0) ? -99999 : MCodificaParametrica((int)EnumTipoParametrica.TipoInfraestructura, "CS");
                                objNuevoTipoReg.idMedioTransporte = -99999;
                                objNuevoTipoReg.idUnidadEnvasesTransporte = -99999;
                                objNuevoTipoReg.idEntidad = MCodificaParametrica((int)EnumTipoParametrica.Entidad, values[1].Trim());
                                //objNuevoTipoReg.codigo = values[7].Trim();
                                objNuevoTipoReg.idTipoMedicion = -99999;
                                objNuevoTipoReg.cantidadAguaLitros = 0;
                                objNuevoTipoReg.idUnidadTemperatura = -99999;
                                objNuevoTipoReg.temperatura = 0;
                                objNuevoTipoReg.gradoApi = "";
                                objNuevoTipoReg.cantidadEnvasesTransporte = 0;
                                objNuevoTipoReg.binarioControl = "00000-0000000000-0000000000-00000";
                                objNuevoTipoReg.idIntegracion = -99999;
                                objNuevoTipoReg.idTransaccionBaseExterna = "";
                                objNuevoTipoReg.fechaOperacionDia = 0;
                                objNuevoTipoReg.codigo = "";
                                objNuevoTipoReg.barcode = Convert.ToDecimal(values[3].Trim());
                                objNuevoTipoReg.listaTransaccion = JsonSerializer.SerializeToString(lstTransacciones);

                                objNuevoTipoReg.jsonReferencia = "";
                                objNuevoTipoReg.estadoJsonRef = -99999;
                                objNuevoTipoReg.idCuenta = -99999;
                                objNuevoTipoReg.idCuentaAux = -99999;
                                objNuevoTipoReg.idParamGlobal = -99999;
                                objNuevoTipoReg.idParamEspec = -99999;
                                objNuevoTipoReg.idTipoConsumidor = -99999;
                                objNuevoTipoReg.idTipoInfraAux = -99999;
                                objNuevoTipoReg.idInfraAux = -99999;

                                #endregion
                                break;
                        }
                        cDato.objProtoMovimiento = objNuevoTipoReg;
                        lstRegistros.Add(cDato);
                        lngNoRegCorrect++;
                    }
                    else
                    {
                        if (strTipoOp == "GS")
                        {
                            var cRegError = new CRegistrosError();
                            cRegError.LngId = lngNoReg;
                            //cRegError.StrIdVenta = values[29].Trim();
                            cRegError.LngFechaEmision = values[0].Trim();
                            cRegError.LngNit = values[9].Trim();
                            cRegError.LngNroAutorizacion = values[12].Trim();
                            cRegError.LngNroFactura = values[11].Trim();
                            //cRegError.LngNroReferencia = values[29].Trim();
                            cRegError.StrErrores = strMensaje.Trim();
                            lstRegistroE.Add(cRegError);
                            lngNoRegInCorrect++;
                        }
                        else
                        {
                            var cRegError = new CRegistrosError();
                            cRegError.LngId = lngNoReg;
                            //cRegError.StrIdVenta = values[20].Trim();
                            cRegError.LngFechaEmision = values[0].Trim();
                            cRegError.LngNit = "";
                            cRegError.LngNroAutorizacion = values[16].Trim();
                            cRegError.LngNroFactura = values[15].Trim();
                            //cRegError.LngNroReferencia = values[20].Trim();
                            cRegError.StrErrores = strMensaje.Trim();
                            lstRegistroE.Add(cRegError);
                            lngNoRegInCorrect++;
                        }
                    }
                }
                catch (Exception ex)
                {
                    var cRegError = new CRegistrosError();
                    cRegError.LngId = lngNoReg;
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
                lngNoReg++;
            }

            dgvTransacciones.DataSource = lstRegistros;
            dgvTransacciones.DataBind();
            Session[CVariablesSesion.GrillaTransaccion] = lstRegistros;

            dgvTransaccionError.DataSource = lstRegistroE;
            dgvTransaccionError.DataBind();
            Session[CVariablesSesion.GrillaRegObservado] = lstRegistroE;
        }

        private bool MValidaLineaGlp(string[] values, ref string strMensaje)
        {
            bool strResultado = true;
            try
            {
                if (cmbTipoOperacion.Text.Contains("DG"))
                {
                    #region Despachos
                    if (values.Length != 20)
                    {
                        strMensaje = "- Nro. de columnas del registro invalido.";
                    }
                    var arrayNumero = new[] { 2, 5, 8, 10, 11, 13, 14, 15, 17, 18 };
                    decimal dec;
                    for (int i = 0; i < arrayNumero.Length; i++)
                    {
                        if (!decimal.TryParse(values[arrayNumero[i]].Trim(), out dec))
                        {
                            strMensaje += "- Columna Nro. " + arrayNumero[i] + " No es un número.";
                        }
                    }
                    if (values[0].Trim().Substring(6, 4) + values[0].Trim().Substring(3, 2) != cmbPeriodo.Value.ToString().Substring(0, 4) + cmbPeriodo.Value.ToString().Substring(4, 2))
                    {
                        strMensaje += "- Periodo incorrecto.";
                    }
                    DateTime dt;
                    if (!DateTime.TryParse(values[0].Trim(), out dt))
                    {
                        strMensaje += "- Fecha incorrecta.";
                    }
                    if (MCodificaParametrica((int)EnumTipoParametrica.Producto, values[4].Trim()) == -99999)
                    {
                        strMensaje += "- Producto incorrecto.";
                    }
                    if (MCodificaParametrica((int)EnumTipoParametrica.UnidadMedida, values[4].Trim()) == -99999)
                    {
                        strMensaje += "- Unidad de medida invalida.";
                    }
                    if ((values[6].Trim() == "V" && Convert.ToDecimal(values[5]) <= 0) || (values[6].Trim() == "A" && Convert.ToDecimal(values[5]) < 0))
                    {
                        strMensaje += "- Cantidad incorrecta.";
                    }

                    if (Convert.ToDecimal(values[14].Trim().Replace(',', '.')) > (Convert.ToDecimal(values[13].Trim().Replace(',', '.')) * Convert.ToDecimal(values[5].Trim())) + 2 ||
                        Convert.ToDecimal(values[14].Trim().Replace(',', '.')) < (Convert.ToDecimal(values[13].Trim().Replace(',', '.')) * Convert.ToDecimal(values[5].Trim())) - 2)
                    {
                        strMensaje += "- Importe incorrecto.";
                    }
                    if (values[12].Trim().Length == 1 && values[12].Trim() != "0")
                    {
                        strMensaje += "- Código de control incorrecto.";
                    }
                    if (values[12].Trim().Length > 1 && values[12].Trim().Length < 6)
                    {
                        strMensaje += "- Código de control incorrecto.";
                    }
                    if (values[3].Trim().Length > 1)
                    {
                        if (values[1].Trim().ContainsAny("-", "@", "/", "'", "\\", ".", ",", "_", "+", "[", "]", "{", "}", " "))
                        {
                            strMensaje += "- Identificador contiene caracteres invalidos.";
                        }
                    }

                    if (Convert.ToDecimal(values[10].Trim()) == 0 && string.IsNullOrEmpty(values[18].Trim()) && string.IsNullOrEmpty(values[19].Trim()))
                    {
                        strMensaje += "- Autorización de venta invalido.";
                    }
                    if (values[7].Trim() != "EF" && values[7].Trim() != "PRE" && values[7].Trim() != "VAL")
                    {
                        strMensaje += "- Forma de pago incorrecta.";
                    }
                    if (Convert.ToInt64(values[18]) > 0 && string.IsNullOrEmpty(values[19].Trim()))
                    {
                        strMensaje += "- Destino invalido.";
                    }

                    if (Convert.ToDecimal(values[15]) == 0 && values[17].Trim().Length == 0)//17 Autoprizacion de salida para venta interno
                    {
                        strMensaje += "- No existe documento de respaldo.";
                    }
                    #endregion
                }
                else
                {
                    #region Recepciones
                    if (values.Length != 18)
                    {
                        strMensaje = "- Nro. de columnas del registro invalido.";
                    }
                    var arrayNumero = new[] { 3, 5, 7, 10, 11, 13, 14, 17 };
                    decimal dec;
                    for (int i = 0; i < arrayNumero.Length; i++)
                    {
                        if (!decimal.TryParse(values[arrayNumero[i]].Trim(), out dec))
                        {
                            strMensaje += "- Columna Nro. " + arrayNumero[i] + " No es un número.";
                        }
                    }
                    if (values[0].Trim().Substring(6, 4) + values[0].Trim().Substring(3, 2) != cmbPeriodo.Value.ToString().Substring(0, 4) + cmbPeriodo.Value.ToString().Substring(4, 2))
                    {
                        strMensaje += "- Periodo incorrecto.";
                    }
                    DateTime dt;
                    if (!DateTime.TryParse(values[0].Trim(), out dt))
                    {
                        strMensaje += "- Fecha incorrecta.";
                    }
                    if (MCodificaParametrica((int)EnumTipoParametrica.Producto, values[4].Trim()) == -99999)
                    {
                        strMensaje += "- Producto incorrecto.";
                    }
                    if (MCodificaParametrica((int)EnumTipoParametrica.UnidadMedida, values[4].Trim()) == -99999)
                    {
                        strMensaje += "- Unidad de medida invalida.";
                    }
                    if (Convert.ToDecimal(values[5]) <= 0)
                    {
                        strMensaje += "- Cantidad incorrecto.";
                    }
                    //La factura puede contener otros items
                    //if (Convert.ToDecimal(values[20].Trim()) > (Convert.ToDecimal(values[19].Trim()) * Convert.ToDecimal(values[10].Trim())) + 2 ||
                    //    Convert.ToDecimal(values[20].Trim()) < (Convert.ToDecimal(values[19].Trim()) * Convert.ToDecimal(values[10].Trim())) - 2)
                    //{
                    //    strMensaje += "- Importe incorrecto.";
                    //}
                    if (values[15].Trim().Length > 1)
                    {
                        if (values[15].Trim().ContainsAny("-", "@", "/", "'", "\\", ".", ",", "_", "+", "[", "]", "{", "}", " "))
                        {
                            strMensaje += "- Identificador contiene caracteres invalidos.";
                        }
                    }
                    if (values[1].Trim() != "NOID")
                    {
                        if (MCodificaParametrica((int)EnumTipoParametrica.Entidad, values[1].Trim()) == -99999)
                        {
                            strMensaje += "- Entidad origen invalida.";
                        }
                    }

                    if (Convert.ToDecimal(values[10]) == 0 && values[15].Trim().Length == 0 && values[19].Trim().Length == 0)
                    {
                        strMensaje += "- No existe documento de respaldo.";
                    }
                    #endregion
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

            return strResultado;
        }

        /// <summary>
        /// METODO. MValidaLineaTaero
        /// </summary>
        /// <param name="values"></param>
        /// <param name="strMensaje"></param>
        /// <returns></returns>

        private bool MValidaPahl(string[] values, ref string strMensaje)
        {
            var booResultado = true;
            try
            {
                var to = cmbTipoOperacion.Text.Split(';');
                var strTipoOp = to[1].Substring(2, 2);
                int[] arrayNumero;
                decimal dec;
                DateTime dt;
                TimeSpan tsp;
                switch (strTipoOp)
                {
                    case "RC":
                        #region Recepciones
                        if (values.Length != 29)  //28,47
                        {
                            strMensaje = "- Nro. de columnas del registro invalido.";
                        }
                        arrayNumero = new[] { 1, 8, 14, 15, 17, 21, 23, 26, 27, 28 };
                        for (int i = 0; i < arrayNumero.Length; i++)
                        {
                            if (!decimal.TryParse(values[arrayNumero[i]].Trim(), out dec))
                            {
                                strMensaje += "- Columna Nro. " + arrayNumero[i] + " No es un número.";
                            }
                        }

                        if (values[0].Trim().Substring(6, 4) + values[0].Trim().Substring(3, 2) != cmbPeriodo.Value.ToString().Substring(0, 4) + cmbPeriodo.Value.ToString().Substring(4, 2))
                        {
                            strMensaje += "- Periodo incorrecto.";
                        }

                        /*if (!TimeSpan.TryParse(values[31].Trim(), out tsp))
                        {
                            strMensaje += "- Hora inicio incorrecta.";
                        }
                        if (!TimeSpan.TryParse(values[33].Trim(), out tsp))
                        {
                            strMensaje += "- Hora final incorrecta.";
                        } */
                        if (!DateTime.TryParse(values[0].Trim(), out dt))
                        {
                            strMensaje += "- Fecha incorrecta.";
                        }
                        if (Convert.ToDecimal(values[26]) <= 0)
                        {
                            strMensaje += "- Cantidad incorrecta.";
                        }
                        if (MCodificaParametrica((int)EnumTipoParametrica.Producto, values[25].Trim()) == -99999)
                        {
                            strMensaje += "- Producto incorrecto.";
                        }
                        /*if (MCodificaParametrica((int)EnumTipoParametrica.UnidadMedida, values[14].Trim()) == -99999)
                        {
                            strMensaje += "- Unidad de medida invalida.";
                        } */
                        if (MCodificaParametrica((int)EnumTipoParametrica.Pais, values[7].Trim()) == -99999)
                        {
                            strMensaje += "- País invalido.";
                        }
                        if (values[10].Trim().Length > 1)
                        {
                            if (values[10].Trim().ContainsAny("-", "@", "/", "'", "\\", ".", ",", "_", "+", "[", "]", "{", "}", " "))
                            {
                                strMensaje += "- Identificador contiene caracteres invalidos.";
                            }
                        }

                        if (values[3].Trim() != "NOID")
                        {
                            var lstInfraestructura = Session[CVariablesSesion.ListaInfraestructura] as List<O_INFRAESTRUCTURA_VOL_CTY>;
                            if (lstInfraestructura != null && lstInfraestructura.Any())
                            {
                                lstInfraestructura = lstInfraestructura.Where(x => x.CODIGO_OPERADOR.Trim() == values[3].Trim() && x.TIPO.Trim() == values[2].Trim()).ToList();
                                if (lstInfraestructura.Any())
                                {
                                    if (lstInfraestructura.FirstOrDefault().ID_PRODUCTO != MCodificaParametrica((int)EnumTipoParametrica.Producto, values[25].Trim()))
                                    {
                                        strMensaje += "- El producto no corresponde a la infraestructura receptora especificada.";
                                    }
                                }
                                else
                                {
                                    strMensaje += "- Infraestructura receptora invalida.";
                                }
                            }
                            else
                            {
                                strMensaje += "- Infraestructura receptora no registrada.";
                            }
                        }

                        if (values[6].Trim() != "NOID")
                        {
                            var lstInfraestructura = Session[CVariablesSesion.ListaInfraestructura] as List<O_INFRAESTRUCTURA_VOL_CTY>;
                            if (lstInfraestructura != null && lstInfraestructura.Any())
                            {
                                lstInfraestructura = lstInfraestructura.Where(x => x.CODIGO_OPERADOR == values[6].Trim() && x.TIPO == values[5].Trim()).ToList();
                                if (lstInfraestructura.Any())
                                {
                                    //if (lstInfraestructura.FirstOrDefault().ID_PRODUCTO != MCodificaParametrica((int)EnumTipoParametrica.Producto, values[14].Trim()))
                                    if (lstInfraestructura.FirstOrDefault().ID_PRODUCTO != MCodificaParametrica((int)EnumTipoParametrica.Producto, values[25].Trim()))
                                    {
                                        strMensaje += "- El producto no corresponde a la infraestructura origen especificada.";
                                    }
                                }
                                else
                                {
                                    strMensaje += "- Infraestructura origen invalida.";
                                }
                            }
                            else
                            {
                                strMensaje += "- Infraestructura origen no definida.";
                            }
                        }
                        if (values[2].Trim() != "NOID" && values[3].Trim() == "NOID")
                        {
                            if (MCodificaParametrica((int)EnumTipoParametrica.TipoInfraestructura, values[2].Trim()) == -99999)
                            {
                                strMensaje += "- Tipo de infraestructura origen invalida.";
                            }
                        }
                        //if (values[5].Trim() == "NOID" || values[6].Trim() == "NOID")                         //Consultar a carla la validacion de infra origen
                        //{
                        //    strMensaje += "- Tipo infraestructura y/o infraestructura receptora invalida, no puede ser NOID.";
                        //}

                        if (values[4].Trim() != "NOID")
                        {
                            if (MCodificaParametrica((int)EnumTipoParametrica.Entidad, values[4].Trim()) == -99999)
                            {
                                strMensaje += "- Proyecto origen invalido.";
                            }
                        }
                        /*
                        if (MCodificaParametrica((int)EnumTipoParametrica.Cuenta, values[20].Trim()) == -99999)
                        {
                            strMensaje += "- Cuenta invalida.";
                        }
                        
                        if (values[21].Trim() != "NOID")
                        {
                            if (MCodificaParametrica((int)EnumTipoParametrica.Cuenta, values[21].Trim()) == -99999)
                            {
                                strMensaje += "- Cuenta Origen invalida.";
                            }
                        }
                        */
                        if (values[17].Trim() == "CTA" && values[22].Trim().Length == 0)
                        {
                            strMensaje += "- Autorizacion de transferencia contable invalida.";
                        }
                        /*if (!(values[17].Trim() == "CTA" || values[17].Trim() == "FIS"))
                        {
                            strMensaje += "- Tipo de movimiento invalido.";
                        }
                        if (values[5].Trim() != "NOID")
                        {
                            var lstInfraestructura = Session[CVariablesSesion.ListaInfraestructura] as List<O_INFRAESTRUCTURA_VOL_CTY>;
                            if (lstInfraestructura != null && lstInfraestructura.Any())
                            {
                                lstInfraestructura = lstInfraestructura.Where(x => x.CODIGO_OPERADOR == values[5].Trim() && x.TIPO == "SR").ToList();
                                if (lstInfraestructura.Any())
                                {
                                    if (lstInfraestructura.FirstOrDefault().ID_PRODUCTO != MCodificaParametrica((int)EnumTipoParametrica.Producto, values[24].Trim()))
                                    {
                                        strMensaje += "- El producto no corresponde al punto de recepcion especificado.";
                                    }
                                }
                                else
                                {
                                    strMensaje += "- Punto de recepcion invalido.";
                                }
                            }
                            else
                            {
                                strMensaje += "- Punto de recepcion no definido.";
                            }
                        }   */
                        /*if (Convert.ToDecimal(values[19]) > 0)
                        {
                            var lstTipoOp = (List<O_TIPOS_OPE_ACT_CTY>)Session[CVariablesSesion.ComboTipoOperacion];
                            if (lstTipoOp == null)
                            {
                                strMensaje += "- Tipos de operaciones no definidos.";
                            }
                            else
                            {
                                lstTipoOp = lstTipoOp.Where(x => x.CODIGO == "C1RCAJ").ToList();
                                if (!lstTipoOp.Any())
                                {
                                    strMensaje += "- Tipo de operación para la demasia por recepcion no definido.";
                                }
                            }
                        }  
                        if (Convert.ToDecimal(values[19]) < 0)
                        {
                            var lstTipoOp = (List<O_TIPOS_OPE_ACT_CTY>)Session[CVariablesSesion.ComboTipoOperacion];
                            if (lstTipoOp == null)
                            {
                                strMensaje += "- Tipos de operaciones no definidos.";
                            }
                            else
                            {
                                lstTipoOp = lstTipoOp.Where(x => x.CODIGO == "C1DSAJ").ToList();
                                if (!lstTipoOp.Any())
                                {
                                    strMensaje += "- Tipo de operación para la merma por recepcion no definido.";
                                }
                            }
                        }   
                        if (values[23].Trim() != "NOID")
                        {
                            if (MCodificaParametrica((int)EnumTipoParametrica.GlobalClasif1, values[23].Trim()) == -99999)
                            {
                                strMensaje += "- Tipo origen invalido.";
                            }
                        }   */
                        if (Convert.ToDecimal(values[1]) == 0 && values[22].Trim().Length == 0 /*&& values[24].Trim().Length == 0*/)
                        {
                            strMensaje += "- No existe documento de respaldo.";
                        }

                        #endregion
                        break;
                    case "MD":
                        #region Mediciones

                        if (values.Length != 20)
                        {
                            strMensaje = "- Nro. de columnas del registro invalido.";
                        }
                        arrayNumero = new[] { 1, 5, 8, 9, 10, 12, 13, 14, 15, 17, 18, 19 };
                        for (int i = 0; i < arrayNumero.Length; i++)
                        {
                            if (!decimal.TryParse(values[arrayNumero[i]].Trim(), out dec))
                            {
                                strMensaje += "- Columna Nro. " + arrayNumero[i] + " No es un número.";
                            }
                        }
                        if (values[0].Trim().Substring(6, 4) + values[0].Trim().Substring(3, 2) != cmbPeriodo.Value.ToString().Substring(0, 4) + cmbPeriodo.Value.ToString().Substring(4, 2))
                        {
                            strMensaje += "- Periodo incorrecto.";
                        }
                        if (!DateTime.TryParse(values[0].Trim(), out dt))
                        {
                            strMensaje += "- Fecha incorrecta.";
                        }
                        if (Convert.ToDecimal(values[9]) < 0)
                        {
                            strMensaje += "- Cantidad incorrecta.";
                        }
                        if (Convert.ToDecimal(values[8]) < 0)
                        {
                            strMensaje += "- Cantidad de agua incorrecta.";
                        }
                        if (MCodificaParametrica((int)EnumTipoParametrica.Producto, values[4].Trim()) == -99999)
                        {
                            strMensaje += "- Producto incorrecto.";
                        }
                        if (MCodificaParametrica((int)EnumTipoParametrica.UnidadMedida, values[4].Trim()) == -99999)
                        {
                            strMensaje += "- Unidad de medida invalida.";
                        }

                        if (values[3].Trim() == "NOID")
                        {
                            strMensaje += "- Infraestructura invalida.";
                        }
                        else
                        {
                            var lstInfraestructura = Session[CVariablesSesion.ListaInfraestructura] as List<O_INFRAESTRUCTURA_VOL_CTY>;
                            if (lstInfraestructura != null && lstInfraestructura.Any())
                            {
                                lstInfraestructura = lstInfraestructura.Where(x => x.CODIGO_OPERADOR == values[3].Trim() && x.TIPO == values[2].Trim()).ToList();
                                if (lstInfraestructura.Any())
                                {
                                    if (lstInfraestructura.FirstOrDefault().ID_PRODUCTO != MCodificaParametrica((int)EnumTipoParametrica.Producto, values[4].Trim()))
                                    {
                                        strMensaje += "- El producto no corresponde a la infraestructura origen especificada.";
                                    }
                                }
                                else
                                {
                                    strMensaje += "- Infraestructura invalida.";
                                }
                            }
                            else
                            {
                                strMensaje += "- Infraestructura no definida.";
                            }
                        }

                        if (!(values[7].Trim() == "MAN" || values[7].Trim() == "TEL" || values[7].Trim() == "CAL"))
                        {
                            strMensaje += "- T ipo de medición invalido.";
                        }

                        if (Convert.ToDecimal(values[1]) <= 0)
                        {
                            strMensaje += "- Id Integración invalido.";
                        }

                        #endregion
                        break;
                    case "DS":
                        #region Despachos comentado 07/10/2021

                        //if (values.Length != 61)
                        //{
                        //    strMensaje = "- Nro. de columnas del registro invalido.";
                        //}
                        //arrayNumero = new[] { 1, 8, 13, 18, 19, 26, 30, 37, 38, 40, 43, 44, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60 };
                        //for (int i = 0; i < arrayNumero.Length; i++)
                        //{
                        //    if (!decimal.TryParse(values[arrayNumero[i]].Trim(), out dec))
                        //    {
                        //        strMensaje += "- Columna Nro. " + arrayNumero[i] + " No es un número.";
                        //    }
                        //}
                        //if (values[0].Trim().Substring(6, 4) + values[0].Trim().Substring(3, 2) != cmbPeriodo.Value.ToString().Substring(0, 4) + cmbPeriodo.Value.ToString().Substring(4, 2))
                        //{
                        //    strMensaje += "- Periodo incorrecto.";
                        //}
                        //if (!DateTime.TryParse(values[0].Trim(), out dt))
                        //{
                        //    strMensaje += "- Fecha incorrecta.";
                        //}
                        //if (Convert.ToDecimal(values[18]) <= 0)
                        //{
                        //    strMensaje += "- Cantidad incorrecta.";
                        //}
                        //if (values[10].Trim().Length > 1)
                        //{
                        //    if (values[10].Trim().ContainsAny("-", "@", "/", "'", "\\", ".", ",", "_", "+", "[", "]", "{", "}", " "))
                        //    {
                        //        strMensaje += "- Identificador contiene caracteres invalidos.";
                        //    }
                        //}
                        //if (MCodificaParametrica((int)EnumTipoParametrica.Producto, values[14].Trim()) == -99999)
                        //{
                        //    strMensaje += "- Producto incorrecto.";
                        //}
                        //if (MCodificaParametrica((int)EnumTipoParametrica.Pais, values[5].Trim()) == -99999)
                        //{
                        //    strMensaje += "- País invalido.";
                        //}
                        //if (!(values[17].Trim() == "CTA" || values[17].Trim() == "FIS"))
                        //{
                        //    strMensaje += "- Tipo de movimiento invalido.";
                        //}
                        //if (values[17].Trim() == "CTA" && values[22].Trim().Length == 0)
                        //{
                        //    strMensaje += "- Autorizacion de transferencia contable invalida.";
                        //}
                        //if (values[3].Trim() != "NOID")
                        //{
                        //    var lstInfraestructura = Session[CVariablesSesion.ListaInfraestructura] as List<O_INFRAESTRUCTURA_VOL_CTY>;
                        //    if (lstInfraestructura != null && lstInfraestructura.Any())
                        //    {
                        //        lstInfraestructura = lstInfraestructura.Where(x => x.CODIGO_OPERADOR == values[3].Trim() && x.TIPO == values[2].Trim()).ToList();
                        //        if (lstInfraestructura.Any())
                        //        {
                        //            if (lstInfraestructura.FirstOrDefault().ID_PRODUCTO != MCodificaParametrica((int)EnumTipoParametrica.Producto, values[14].Trim()))
                        //            {
                        //                strMensaje += "- El producto no corresponde a la infraestructura origen especificada.";
                        //            }
                        //        }
                        //        else
                        //        {
                        //            strMensaje += "- Infraestructura origen invalida.";
                        //        }
                        //    }
                        //    else
                        //    {
                        //        strMensaje += "- Infraestructura origen no definida.";
                        //    }
                        //}
                        //if (values[2].Trim() != "NOID" && values[3].Trim() == "NOID")
                        //{
                        //    if (MCodificaParametrica((int)EnumTipoParametrica.TipoInfraestructura, values[2].Trim()) == -99999)
                        //    {
                        //        strMensaje += "- Tipo de infraestructura origen invalida.";
                        //    }
                        //}
                        //if (values[6].Trim() == "NOID" || values[7].Trim() == "NOID")
                        //{
                        //    strMensaje += "- Tipo infraestructura y/o infraestructura receptora invalida, no puede ser NOID.";
                        //}
                        //if (values[7].Trim() != "NOID")
                        //{
                        //    var lstInfraestructura = Session[CVariablesSesion.ListaInfraestructura] as List<O_INFRAESTRUCTURA_VOL_CTY>;
                        //    if (lstInfraestructura != null && lstInfraestructura.Any())
                        //    {
                        //        lstInfraestructura = lstInfraestructura.Where(x => x.CODIGO_OPERADOR.Trim() == values[7].Trim() && x.TIPO.Trim() == values[6].Trim()).ToList();
                        //        if (lstInfraestructura.Any())
                        //        {
                        //            if (lstInfraestructura.FirstOrDefault().ID_PRODUCTO != MCodificaParametrica((int)EnumTipoParametrica.Producto, values[14].Trim()))
                        //            {
                        //                strMensaje += "- El producto no corresponde a la infraestructura receptora especificada.";
                        //            }
                        //        }
                        //        else
                        //        {
                        //            strMensaje += "- Infraestructura receptora invalida.";
                        //        }
                        //    }
                        //    else
                        //    {
                        //        strMensaje += "- Infraestructura receptora no registrada.";
                        //    }
                        //}
                        //if (values[4].Trim() != "NOID")
                        //{
                        //    if (MCodificaParametrica((int)EnumTipoParametrica.Entidad, values[4].Trim()) == -99999)
                        //    {
                        //        strMensaje += "- Proyecto destino invalido.";
                        //    }
                        //}
                        //if (MCodificaParametrica((int)EnumTipoParametrica.Cuenta, values[20].Trim()) == -99999)
                        //{
                        //    strMensaje += "- Cuenta invalida.";
                        //}
                        //if (values[21].Trim() != "NOID")
                        //{
                        //    if (MCodificaParametrica((int)EnumTipoParametrica.Cuenta, values[21].Trim()) == -99999)
                        //    {
                        //        strMensaje += "- Cuenta destino invalida.";
                        //    }
                        //}
                        //if (values[32].Trim() != "NOID")
                        //{
                        //    var lstInfraestructura = Session[CVariablesSesion.ListaInfraestructura] as List<O_INFRAESTRUCTURA_VOL_CTY>;
                        //    if (lstInfraestructura != null && lstInfraestructura.Any())
                        //    {
                        //        lstInfraestructura = lstInfraestructura.Where(x => x.CODIGO_OPERADOR == values[32].Trim() && x.TIPO == "SC").ToList();
                        //        if (lstInfraestructura.Any())
                        //        {
                        //            if (lstInfraestructura.FirstOrDefault().ID_PRODUCTO != MCodificaParametrica((int)EnumTipoParametrica.Producto, values[14].Trim()))
                        //            {
                        //                strMensaje += "- El producto no corresponde al punto de despacho especificado.";
                        //            }
                        //        }
                        //        else
                        //        {
                        //            strMensaje += "- Punto de despacho invalido.";
                        //        }
                        //    }
                        //    else
                        //    {
                        //        strMensaje += "- Punto de despacho no definido.";
                        //    }
                        //}
                        //if (Convert.ToDecimal(values[19]) > 0)
                        //{
                        //    var lstTipoOp = (List<O_TIPOS_OPE_ACT_CTY>)Session[CVariablesSesion.ComboTipoOperacion];
                        //    if (lstTipoOp == null)
                        //    {
                        //        strMensaje += "- Tipos de operaciones no definidos.";
                        //    }
                        //    else
                        //    {
                        //        lstTipoOp = lstTipoOp.Where(x => x.CODIGO == "C2RCAJ").ToList();
                        //        if (!lstTipoOp.Any())
                        //        {
                        //            strMensaje += "- Tipo de operación para la demasia por recepcion no definido.";
                        //        }
                        //    }
                        //}
                        //if (Convert.ToDecimal(values[19]) < 0)
                        //{
                        //    var lstTipoOp = (List<O_TIPOS_OPE_ACT_CTY>)Session[CVariablesSesion.ComboTipoOperacion];
                        //    if (lstTipoOp == null)
                        //    {
                        //        strMensaje += "- Tipos de operaciones no definidos.";
                        //    }
                        //    else
                        //    {
                        //        lstTipoOp = lstTipoOp.Where(x => x.CODIGO == "C2DSAJ").ToList();
                        //        if (!lstTipoOp.Any())
                        //        {
                        //            strMensaje += "- Tipo de operación para la merma por recepcion no definido.";
                        //        }
                        //    }
                        //}
                        //if (values[23].Trim() != "NOID")
                        //{
                        //    if (MCodificaParametrica((int)EnumTipoParametrica.GlobalClasif1, values[23].Trim()) == -99999)
                        //    {
                        //        strMensaje += "- Tipo origen invalido.";
                        //    }
                        //}
                        //if (Convert.ToDecimal(values[1]) == 0 && values[22].Trim().Length == 0 && values[24].Trim().Length == 0 && values[29].Trim().Length == 0)
                        //{
                        //    strMensaje += "- No existe documento de respaldo.";
                        //}

                        #endregion
                        #region Despachos
                        if (values.Length != 27)
                        {
                            strMensaje = "- Nro. de columnas del registro invalido.";
                        }
                        arrayNumero = new[] { 1, 7, 12, 13, 15, 18, 19, 24, 25, 26};
                        for (int i = 0; i < arrayNumero.Length; i++)
                        {
                            if (!decimal.TryParse(values[arrayNumero[i]].Trim(), out dec))
                            {
                                strMensaje += "- Columna Nro. " + arrayNumero[i] + " No es un número.";
                            }
                        }
                        if (values[0].Trim().Substring(6, 4) + values[0].Trim().Substring(3, 2) != cmbPeriodo.Value.ToString().Substring(0, 4) + cmbPeriodo.Value.ToString().Substring(4, 2))
                        {
                            strMensaje += "- Periodo incorrecto.";
                        }
                        if (!DateTime.TryParse(values[0].Trim(), out dt))
                        {
                            strMensaje += "- Fecha incorrecta.";
                        }
                        if ((values[22].Trim() == "V" && Convert.ToDecimal(values[24]) <= 0) || (values[22].Trim() == "A" && Convert.ToDecimal(values[24]) < 0))
                        {
                            strMensaje += "- Cantidad incorrecta.";
                        }
                        if (Convert.ToDecimal(values[26].Trim()) > (Convert.ToDecimal(values[24].Trim()) * Convert.ToDecimal(values[25].Trim())) + 2 ||
                            Convert.ToDecimal(values[26].Trim()) < (Convert.ToDecimal(values[24].Trim()) * Convert.ToDecimal(values[25].Trim())) - 2)
                        {
                            strMensaje += "- Importe incorrecto.";
                        }
                        if (values[14].Trim().Length == 1 && values[14].Trim() != "0")
                        {
                            strMensaje += "- Código de control incorrecto.";
                        }
                        if (values[14].Trim().Length > 1 && values[14].Trim().Length < 6)
                        {
                            strMensaje += "- Código de control incorrecto.";
                        }
                        if (values[9].Trim().Length > 1)
                        {
                            if (values[9].Trim().ContainsAny("-", "@", "/", "'", "\\", ".", ",", "_", "+", "[", "]", "{", "}", " "))
                            {
                                strMensaje += "- Identificador contiene caracteres invalidos.";
                            }
                        }
                        if (MCodificaParametrica((int)EnumTipoParametrica.Producto, values[23].Trim()) == -99999)
                        {
                            strMensaje += "- Producto incorrecto.";
                        }
                        //if (MCodificaParametrica((int)EnumTipoParametrica.Pais, values[7].Trim()) == -99999)
                        //{
                        //    strMensaje += "- País invalido.";
                        //}
                        //if (values[13].Trim() != "NOID" && values[23].Trim() == "0")
                        //{
                        //    strMensaje += "- El movimiento en envases debe tener Autorización de Sustancias Controladas.";
                        //}
                        if (values[3].Trim() != "NOID")
                        {
                            var lstInfraestructura = Session[CVariablesSesion.ListaInfraestructura] as List<O_INFRAESTRUCTURA_VOL_CTY>;
                            if (lstInfraestructura != null && lstInfraestructura.Any())
                            {
                                lstInfraestructura = lstInfraestructura.Where(x => x.CODIGO_OPERADOR == values[3].Trim() && x.TIPO == values[2].Trim()).ToList();
                                if (lstInfraestructura.Any())
                                {
                                    if (lstInfraestructura.FirstOrDefault().ID_PRODUCTO != MCodificaParametrica((int)EnumTipoParametrica.Producto, values[23].Trim()))
                                    {
                                        strMensaje += "- El producto no corresponde a la infraestructura origen especificada.";
                                    }
                                }
                                else
                                {
                                    strMensaje += "- Infraestructura origen invalida.";
                                }
                            }
                            else
                            {
                                strMensaje += "- Infraestructura origen no definida.";
                            }
                        }
                        if (values[6].Trim() != "NOID")
                        {
                            var lstInfraestructura = Session[CVariablesSesion.ListaInfraestructura] as List<O_INFRAESTRUCTURA_VOL_CTY>;
                            if (lstInfraestructura != null && lstInfraestructura.Any())
                            {
                                lstInfraestructura = lstInfraestructura.Where(x => x.CODIGO_OPERADOR == values[6].Trim() && x.TIPO == values[5].Trim()).ToList();
                                if (lstInfraestructura.Any())
                                {
                                    if (lstInfraestructura.FirstOrDefault().ID_PRODUCTO != MCodificaParametrica((int)EnumTipoParametrica.Producto, values[23].Trim()))
                                    {
                                        strMensaje += "- El producto no corresponde a la infraestructura origen especificada.";
                                    }
                                }
                                else
                                {
                                    strMensaje += "- Infraestructura origen invalida.";
                                }
                            }
                            else
                            {
                                strMensaje += "- Infraestructura origen no definida.";
                            }
                        }
                        if (values[5].Trim() != "NOID" && values[6].Trim() == "NOID")
                        {
                            if (MCodificaParametrica((int)EnumTipoParametrica.TipoInfraestructura, values[5].Trim()) == -99999)
                            {
                                strMensaje += "- Tipo de infraestructura origen invalida.";
                            }
                        }
                        if (values[4].Trim() != "NOID")
                        {
                            if (MCodificaParametrica((int)EnumTipoParametrica.Entidad, values[4].Trim()) == -99999)
                            {
                                strMensaje += "- Entidad origen invalida.";
                            }
                        }
                        if (values[17].Trim().Length == 0 || values[17].Trim() == "0")
                        {
                            strMensaje += "- Número de referencia invalido.";
                        }
                        if (Convert.ToDecimal(values[12]) == 0 && values[17].Trim().Length == 0 && Convert.ToDecimal(values[17]) == 0)
                        {
                            strMensaje += "- No existe documento de respaldo.";
                        }
                        #endregion
                        break;
                    default:
                        strMensaje = "- Tipo de operacion invalida.";
                        break;
                }
            }
            catch (Exception ex)
            {
                strMensaje += ex.Message;
            }

            if (strMensaje.Trim().Length > 0)
            {
                booResultado = false;
            }
            else
            {
                //todo No implementado.
            }

            return booResultado;
        }

        /// <summary>
        /// METODO. MCargaTerminalAeropuerto
        /// </summary>
        /// <param name="strRuta"></param>
        private void MCargaPahl(string strRuta)
        {
            var reader = new StreamReader(File.OpenRead(@strRuta));
            var lstRegistros = new List<CRegistrosImportados>();
            var lstRegistroE = new List<CRegistrosError>();
            long lngNoRegCorrect = 1, lngNoRegInCorrect = 1;
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(';');
                try
                {
                    DateTime datFecha, datFechaReg;
                    string strFecha;
                    //ObjTransaccion objDocumento;
                    ObjTransaccion objFactura, objDocumento;
                    ObjProtoMovimientoTransaccion objNuevoTipoReg = null;
                    int estadoTra;
                    List<ObjTransaccion> lstTransacciones;

                    ObjProtoMovimientoTransaccion objNuevoAjuste = null;
                    var lstTransAjuste = new List<ObjTransaccion>();

                    var to = cmbTipoOperacion.Text.Split(';');
                    var strTipoOp = to[1].Substring(2, 2);

                    var intBinCtrl = 1;

                    string strMensaje = string.Empty;
                    if (MValidaPahl(values, ref strMensaje))
                    {
                        var cDato = new CRegistrosImportados();
                        var cDatoAj = new CRegistrosImportados();
                        int controlBinario;
                        switch (strTipoOp)
                        {
                            case "RC":
                                #region Valida y genera objeto
                                cDato.LngId = lngNoRegCorrect;
                                cDato.StrIdVenta = values[1].Trim();
                                datFecha = DateTime.Parse(values[0].Trim());
                                cDato.StrFechaEmision = datFecha.ToString();
                                cDato.StrTipoMov = (values[7].Trim() == "BOLIVIA") ? "NAC" : "IMP";
                                cDato.StrEstado = values[24].Trim();
                                cDato.LngNit = Convert.ToInt64(values[17]);
                                cDato.StrRazonSocial = values[18].Trim().ToUpper().Replace("¥", "Ñ").Replace("Á", "A").Replace("É", "E").Replace("Í", "I").Replace("Ó", "O").Replace("Ú", "U").Replace("'", "").Replace("�", "Ñ");
                                cDato.StrRazonSocial = Regex.Replace(cDato.StrRazonSocial, @"\s+", " ");
                                cDato.StrIdentificador = values[10].Trim();
                                cDato.LngNroFactura = Convert.ToInt64(values[14]);
                                cDato.LngNroAutorizacion = Convert.ToInt64(values[15]);
                                cDato.StrNroReferencia = values[19].Trim();
                                cDato.LngNroAutVenta = Convert.ToInt64(values[21]);
                                cDato.StrProducto = values[25].Trim();
                                cDato.DecVolumen = Convert.ToDecimal(values[26]);
                                cDato.DecPrecio = Convert.ToDecimal(values[27]);
                                cDato.DecImporte = Convert.ToDecimal(values[28]);
                                cDato.DecIdTipoCreditoFiscal = (cDato.LngNroFactura > 0) ? (int)EnumTipoCredFiscal.Si : (int)EnumTipoCredFiscal.No;
                                cDato.DecIdTipoOp = Convert.ToDecimal(cmbTipoOperacion.Value);
                                cDato.DecOrden = 1;
                                strFecha = datFecha.Year + datFecha.Month.ToString().PadLeft(2, '0') + datFecha.Day.ToString().PadLeft(2, '0') + datFecha.Hour.ToString().PadLeft(2, '0') + datFecha.Minute.ToString().PadLeft(2, '0') + datFecha.Second.ToString().PadLeft(2, '0');
                                datFechaReg = DateTime.Now;
                                estadoTra = (cDato.StrEstado == "A") ? (int)EnumEstadoMov.Anulado : (int)EnumEstadoMov.Activo;

                                lstTransacciones = new List<ObjTransaccion>();
                                controlBinario = 1;

                                //var objAuxiliar = new O_PAHL_RECEP_DA
                                //{
                                //    FACTOR_CORRECCION = Convert.ToDecimal(values[28].Trim()),
                                //    API_MUESTRA = values[29].Trim(),
                                //    TEMPERATURA_METER = Convert.ToDecimal(values[30].Trim()),
                                //    HORA_INICIO = values[31].Trim(),
                                //    METER_INICIAL = Convert.ToDecimal(values[32].Trim()),
                                //    HORA_FINAL = values[33].Trim(),
                                //    METER_FINAL = Convert.ToDecimal(values[34].Trim()),
                                //    VOL_REMITIDO = Convert.ToDecimal(values[35].Trim()),
                                //    VOL_RECIBIDO = Convert.ToDecimal(values[36].Trim()),
                                //    VOL_CORREGIDO = Convert.ToDecimal(values[37].Trim()),
                                //    VOL_PROD_INI = Convert.ToDecimal(values[38].Trim()),
                                //    VOL_PROD_FIN = Convert.ToDecimal(values[39].Trim()),
                                //    VOL_NETO = Convert.ToDecimal(values[40].Trim()),
                                //    NIVEL_PROD_INI = Convert.ToDecimal(values[41].Trim()),
                                //    NIVEL_AGUA_INI = Convert.ToDecimal(values[42].Trim()),
                                //    NIVEL_PROD_FIN = Convert.ToDecimal(values[43].Trim()),
                                //    NIVEL_AGUA_FIN = Convert.ToDecimal(values[44].Trim()),
                                //    TECHO_FLOTANTE_INI = Convert.ToDecimal(values[45].Trim()),
                                //    TECHO_FLOTANTE_FIN = Convert.ToDecimal(values[46].Trim())
                                //};

                                //if (values[22].Trim().Length > 0)
                                //{
                                //    objDocumento = new ObjTransaccion();
                                //    objDocumento.idMovimiento = 0;
                                //    objDocumento.idTransaccion = 0;
                                //    objDocumento.idTransaccionPadre = 0;
                                //    objDocumento.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                //    objDocumento.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                //    objDocumento.idTipoTransaccion = (decimal)EnumTipoTransaccion.DocCuenta;
                                //    objDocumento.fechaEmision = Convert.ToInt64(strFecha);
                                //    objDocumento.numeroTransaccion = values[22].Trim();
                                //    objDocumento.numeroAutorizacion = 0;
                                //    objDocumento.codigoControl = "";
                                //    objDocumento.nitConsumidor = 0;
                                //    objDocumento.razonSocial = "";
                                //    objDocumento.precioUnitario = 0;
                                //    objDocumento.precioTotal = 0;
                                //    objDocumento.usuarioTransaccion = values[15].Trim();
                                //    objDocumento.observaciones = "";
                                //    objDocumento.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                //    objDocumento.idEstadoTransaccion = estadoTra;
                                //    objDocumento.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.No;
                                //    objDocumento.idFormaPago = (decimal)EnumTiposPago.Efectivo;
                                //    objDocumento.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                //    objDocumento.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                //    objDocumento.codigoSeguridad = 0;
                                //    objDocumento.idAutorizacionDato = 0;
                                //    objDocumento.binarioControl = intBinCtrl + "000000000";
                                //    objDocumento.fechaOperacion = Convert.ToInt64(strFecha);
                                //    objDocumento.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                //    objDocumento.codigo = "";
                                //    lstTransacciones.Add(objDocumento);
                                //    intBinCtrl = 0;
                                //}

                                //if (values[24].Trim().Length > 0)
                                //{
                                //    objDocumento = new ObjTransaccion();
                                //    objDocumento.idMovimiento = 0;
                                //    objDocumento.idTransaccion = 0;
                                //    objDocumento.idTransaccionPadre = 0;
                                //    objDocumento.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                //    objDocumento.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                //    objDocumento.idTipoTransaccion = (decimal)EnumTipoTransaccion.DocCre;
                                //    objDocumento.fechaEmision = Convert.ToInt64(strFecha);
                                //    objDocumento.numeroTransaccion = values[24].Trim();
                                //    objDocumento.numeroAutorizacion = 0;
                                //    objDocumento.codigoControl = "";
                                //    objDocumento.nitConsumidor = Convert.ToDecimal(values[25].Trim());
                                //    objDocumento.razonSocial = values[26].Trim();
                                //    objDocumento.precioUnitario = 0;
                                //    objDocumento.precioTotal = 0;
                                //    objDocumento.usuarioTransaccion = values[15].Trim();
                                //    objDocumento.observaciones = "";
                                //    objDocumento.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                //    objDocumento.idEstadoTransaccion = estadoTra;
                                //    objDocumento.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.No;
                                //    objDocumento.idFormaPago = (decimal)EnumTiposPago.Efectivo;
                                //    objDocumento.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                //    objDocumento.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                //    objDocumento.codigoSeguridad = 0;
                                //    objDocumento.idAutorizacionDato = 0;
                                //    objDocumento.binarioControl = intBinCtrl + "000000000";
                                //    objDocumento.fechaOperacion = Convert.ToInt64(strFecha);
                                //    objDocumento.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                //    objDocumento.codigo = "";
                                //    lstTransacciones.Add(objDocumento);
                                //    intBinCtrl = 0;
                                //}

                                //if (lstTransacciones.Count == 0)
                                //{
                                //    objDocumento = new ObjTransaccion();
                                //    objDocumento.idMovimiento = 0;
                                //    objDocumento.idTransaccion = 0;
                                //    objDocumento.idTransaccionPadre = 0;
                                //    objDocumento.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                //    objDocumento.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                //    objDocumento.idTipoTransaccion = (decimal)EnumTipoTransaccion.NotaMovimiento;
                                //    objDocumento.fechaEmision = Convert.ToInt64(strFecha);
                                //    objDocumento.numeroTransaccion = values[1].Trim();
                                //    objDocumento.numeroAutorizacion = 0;
                                //    objDocumento.codigoControl = "";
                                //    objDocumento.nitConsumidor = 0;
                                //    objDocumento.razonSocial = "";
                                //    objDocumento.precioUnitario = 0;
                                //    objDocumento.precioTotal = 0;
                                //    objDocumento.usuarioTransaccion = values[15].Trim();
                                //    objDocumento.observaciones = "";
                                //    objDocumento.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                //    objDocumento.idEstadoTransaccion = estadoTra;
                                //    objDocumento.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.No;
                                //    objDocumento.idFormaPago = (decimal)EnumTiposPago.Efectivo;
                                //    objDocumento.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                //    objDocumento.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                //    objDocumento.codigoSeguridad = 0;
                                //    objDocumento.idAutorizacionDato = 0;
                                //    objDocumento.binarioControl = intBinCtrl + "000000000";
                                //    objDocumento.fechaOperacion = Convert.ToInt64(strFecha);
                                //    objDocumento.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                //    objDocumento.codigo = "";
                                //    lstTransacciones.Add(objDocumento);
                                //}

                                //objNuevoTipoReg = new ObjProtoMovimientoTransaccion();
                                //objNuevoTipoReg.idMovimientoPadre = 0;
                                //objNuevoTipoReg.idTipoActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                //objNuevoTipoReg.idTipoOperacion = Convert.ToDecimal(cmbTipoOperacion.Value);
                                //objNuevoTipoReg.idConsumidor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString());
                                //objNuevoTipoReg.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                //objNuevoTipoReg.fechaRegistroMovimiento = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                //objNuevoTipoReg.fechaOperacion = Convert.ToInt64(strFecha);
                                //objNuevoTipoReg.idTipoMovimientoFlujo = (values[17].Trim() == "CTA") ? (int)EnumTipoMovimientoFlujo.Contable : (int)EnumTipoMovimientoFlujo.Fisico;
                                //objNuevoTipoReg.idTipoMovimiento = (values[5].Trim() == "BOLIVIA") ? (int)EnumTipoMovimiento.NacNac : (int)EnumTipoMovimiento.IntNac;
                                //objNuevoTipoReg.idEstadoMovimiento = estadoTra;
                                //objNuevoTipoReg.idFuenteOrigenDato = (decimal)EnumTipoRegistro.OrigenCsv;
                                //objNuevoTipoReg.idUbicacionOperacion = MCodificaParametrica((int)EnumTipoParametrica.Pais, values[5].Trim());
                                //objNuevoTipoReg.idUnidadMedidaHidrocarburo = MCodificaParametrica((int)EnumTipoParametrica.UnidadMedida, values[14].Trim());
                                //objNuevoTipoReg.idHidrocarburo = MCodificaParametrica((int)EnumTipoParametrica.Producto, values[14].Trim());
                                //objNuevoTipoReg.cantidadHidrocarburo = cDato.DecVolumen;
                                //objNuevoTipoReg.identificador = cDato.StrIdentificador;
                                //objNuevoTipoReg.subIdentificador = "";
                                //objNuevoTipoReg.observacion = "";
                                //objNuevoTipoReg.tipoInfraestructura = MCodificaParametrica((int)EnumTipoParametrica.TipoInfraestructura, values[6].Trim());
                                //objNuevoTipoReg.idInfraestructura = MCodificaParametrica((int)EnumTipoParametrica.Infraestructura, values[6].Trim() + "|" + values[7].Trim());
                                //objNuevoTipoReg.idTipoMedicion = -99999;
                                //objNuevoTipoReg.cantidadAguaLitros = 0;
                                //objNuevoTipoReg.idUnidadTemperatura = (decimal)EnumUnidadTemperatura.GradoFarenheit;
                                //objNuevoTipoReg.temperatura = Convert.ToDecimal(values[8]);
                                //objNuevoTipoReg.gradoApi = values[9].Trim();
                                //objNuevoTipoReg.idEntidad = MCodificaParametrica((int)EnumTipoParametrica.Entidad, values[4].Trim());
                                //objNuevoTipoReg.idTipoMedioTransporte = MCodificaParametrica((int)EnumTipoParametrica.TipoInfraestructura, values[2].Trim());
                                //objNuevoTipoReg.idMedioTransporte = MCodificaParametrica((int)EnumTipoParametrica.Infraestructura, values[2].Trim() + "|" + values[3].Trim());
                                //objNuevoTipoReg.idUnidadEnvasesTransporte = -99999;
                                //objNuevoTipoReg.cantidadEnvasesTransporte = 0;
                                //objNuevoTipoReg.binarioControl = "00000-0000000000-0000000000-00000";
                                //objNuevoTipoReg.idIntegracion = Convert.ToDecimal(values[1]);
                                //objNuevoTipoReg.idTransaccionBaseExterna = "";
                                //objNuevoTipoReg.codigo = values[12].Trim();
                                //objNuevoTipoReg.barcode = Convert.ToDecimal(values[13]);

                                //objNuevoTipoReg.jsonReferencia = JsonSerializer.SerializeToString(objAuxiliar);
                                //objNuevoTipoReg.estadoJsonRef = -99999;
                                //objNuevoTipoReg.idCuenta = MCodificaParametrica((int)EnumTipoParametrica.Cuenta, values[20].Trim());
                                //objNuevoTipoReg.idCuentaAux = MCodificaParametrica((int)EnumTipoParametrica.Cuenta, values[21].Trim());
                                //objNuevoTipoReg.idParamGlobal = MCodificaParametrica((int)EnumTipoParametrica.GlobalClasif1, values[23].Trim());
                                //objNuevoTipoReg.idParamEspec = -99999;
                                //objNuevoTipoReg.idTipoConsumidor = -99999;
                                //objNuevoTipoReg.idTipoInfraAux = MCodificaParametrica((int)EnumTipoParametrica.TipoInfraestructura, "SR");
                                //objNuevoTipoReg.idInfraAux = MCodificaParametrica((int)EnumTipoParametrica.Infraestructura, "SR|" + values[27].Trim());

                                //objNuevoTipoReg.listaTransaccion = JsonSerializer.SerializeToString(lstTransacciones);

                                //if (Convert.ToDecimal(values[19]) != 0 && values[16].Trim() == "V" && values[17].Trim() == "FIS")
                                //{
                                //    cDatoAj = new CRegistrosImportados();
                                //    cDatoAj.LngId = lngNoRegCorrect;
                                //    cDatoAj.StrIdVenta = values[1].Trim();
                                //    datFecha = DateTime.Parse(values[0].Trim());
                                //    cDatoAj.StrFechaEmision = datFecha.ToString();
                                //    cDatoAj.StrTipoMov = (values[5].Trim() == "BOLIVIA") ? "NAC" : "IMP";
                                //    cDatoAj.StrEstado = values[16].Trim();
                                //    cDatoAj.LngNit = 0;
                                //    cDatoAj.StrRazonSocial = "REGISTRO POR DIFERENCIA.";
                                //    cDatoAj.StrIdentificador = values[10].Trim();
                                //    cDatoAj.LngNroFactura = 0;
                                //    cDatoAj.LngNroAutorizacion = 0;
                                //    cDatoAj.StrNroReferencia = values[1].Trim();
                                //    cDatoAj.LngNroAutVenta = 0;
                                //    cDatoAj.StrProducto = values[14].Trim();
                                //    cDatoAj.DecVolumen = Math.Abs(Convert.ToDecimal(values[19]));
                                //    cDatoAj.DecPrecio = 0;
                                //    cDatoAj.DecImporte = 0;
                                //    cDatoAj.DecIdTipoCreditoFiscal = (int)EnumTipoCredFiscal.No;

                                //    objDocumento = new ObjTransaccion();
                                //    objDocumento.idMovimiento = 0;
                                //    objDocumento.idTransaccion = 0;
                                //    objDocumento.idTransaccionPadre = 0;
                                //    objDocumento.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                //    objDocumento.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                //    objDocumento.idTipoTransaccion = (decimal)EnumTipoTransaccion.NotaMovimiento;
                                //    objDocumento.fechaEmision = Convert.ToInt64(strFecha);
                                //    objDocumento.numeroTransaccion = values[1].Trim();
                                //    objDocumento.numeroAutorizacion = 0;
                                //    objDocumento.codigoControl = "";
                                //    objDocumento.nitConsumidor = 0;
                                //    objDocumento.razonSocial = "";
                                //    objDocumento.precioUnitario = 0;
                                //    objDocumento.precioTotal = 0;
                                //    objDocumento.usuarioTransaccion = values[15].Trim();
                                //    objDocumento.observaciones = "";
                                //    objDocumento.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                //    objDocumento.idEstadoTransaccion = estadoTra;
                                //    objDocumento.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.No;
                                //    objDocumento.idFormaPago = (decimal)EnumTiposPago.Efectivo;
                                //    objDocumento.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                //    objDocumento.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                //    objDocumento.codigoSeguridad = 0;
                                //    objDocumento.idAutorizacionDato = 0;
                                //    objDocumento.binarioControl = "1000000000";
                                //    objDocumento.fechaOperacion = Convert.ToInt64(strFecha);
                                //    objDocumento.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                //    objDocumento.codigo = "";
                                //    lstTransAjuste.Add(objDocumento);

                                //    var lstTipoOp = (List<O_TIPOS_OPE_ACT_CTY>)Session[CVariablesSesion.ComboTipoOperacion];
                                //    var idTipoOp = lstTipoOp.Where(x => x.CODIGO == ((Convert.ToDecimal(values[19]) < 0) ? "C1DSAJ" : "C1RCAJ")).ToList().FirstOrDefault().ID_TIPO_OPERACION;

                                //    cDatoAj.DecIdTipoOp = idTipoOp;
                                //    cDatoAj.DecOrden = 0;

                                //    objNuevoAjuste = new ObjProtoMovimientoTransaccion();
                                //    objNuevoAjuste.idMovimientoPadre = 0;
                                //    objNuevoAjuste.idTipoActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                //    objNuevoAjuste.idTipoOperacion = idTipoOp;
                                //    objNuevoAjuste.idConsumidor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString());
                                //    objNuevoAjuste.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                //    objNuevoAjuste.fechaRegistroMovimiento = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                //    objNuevoAjuste.fechaOperacion = Convert.ToInt64(strFecha);
                                //    objNuevoAjuste.idTipoMovimientoFlujo = (int)EnumTipoMovimientoFlujo.Fisico;
                                //    objNuevoAjuste.idTipoMovimiento = (values[5].Trim() == "BOLIVIA") ? (int)EnumTipoMovimiento.NacNac : (int)EnumTipoMovimiento.IntNac;
                                //    objNuevoAjuste.idEstadoMovimiento = estadoTra;
                                //    objNuevoAjuste.idFuenteOrigenDato = (decimal)EnumTipoRegistro.OrigenCsv;
                                //    objNuevoAjuste.idUbicacionOperacion = MCodificaParametrica((int)EnumTipoParametrica.Pais, values[5].Trim());
                                //    objNuevoAjuste.idUnidadMedidaHidrocarburo = MCodificaParametrica((int)EnumTipoParametrica.UnidadMedida, values[14].Trim());
                                //    objNuevoAjuste.idHidrocarburo = MCodificaParametrica((int)EnumTipoParametrica.Producto, values[14].Trim());
                                //    objNuevoAjuste.cantidadHidrocarburo = Math.Abs(Convert.ToDecimal(values[19]));
                                //    objNuevoAjuste.identificador = cDato.StrIdentificador;
                                //    objNuevoAjuste.subIdentificador = "";
                                //    objNuevoAjuste.observacion = "Generado por diferencia - " + cmbTipoOperacion.Text + " - " + values[1].Trim();
                                //    objNuevoAjuste.tipoInfraestructura = MCodificaParametrica((int)EnumTipoParametrica.TipoInfraestructura, values[6].Trim());
                                //    objNuevoAjuste.idInfraestructura = MCodificaParametrica((int)EnumTipoParametrica.Infraestructura, values[6].Trim() + "|" + values[7].Trim());
                                //    objNuevoAjuste.idTipoMedicion = -99999;
                                //    objNuevoAjuste.cantidadAguaLitros = 0;
                                //    objNuevoAjuste.idUnidadTemperatura = (decimal)EnumUnidadTemperatura.GradoFarenheit;
                                //    objNuevoAjuste.temperatura = Convert.ToDecimal(values[8]);
                                //    objNuevoAjuste.gradoApi = values[9].Trim();
                                //    objNuevoAjuste.idEntidad = MCodificaParametrica((int)EnumTipoParametrica.Entidad, values[4].Trim());
                                //    objNuevoAjuste.idTipoMedioTransporte = MCodificaParametrica((int)EnumTipoParametrica.TipoInfraestructura, values[2].Trim());
                                //    objNuevoAjuste.idMedioTransporte = MCodificaParametrica((int)EnumTipoParametrica.Infraestructura, values[2].Trim() + "|" + values[3].Trim());
                                //    objNuevoAjuste.idUnidadEnvasesTransporte = -99999;
                                //    objNuevoAjuste.cantidadEnvasesTransporte = 0;
                                //    objNuevoAjuste.binarioControl = "00000-0000000000-0000000000-00000";
                                //    objNuevoAjuste.idIntegracion = Convert.ToDecimal(values[1]);
                                //    objNuevoAjuste.idTransaccionBaseExterna = "";
                                //    objNuevoAjuste.codigo = values[12].Trim();
                                //    objNuevoAjuste.barcode = Convert.ToDecimal(values[13]);

                                //    objNuevoAjuste.jsonReferencia = "";
                                //    objNuevoAjuste.estadoJsonRef = 1;
                                //    objNuevoAjuste.idCuenta = MCodificaParametrica((int)EnumTipoParametrica.Cuenta, values[20].Trim());
                                //    objNuevoAjuste.idCuentaAux = MCodificaParametrica((int)EnumTipoParametrica.Cuenta, values[21].Trim());
                                //    objNuevoAjuste.idParamGlobal = MCodificaParametrica((int)EnumTipoParametrica.GlobalClasif1, values[23].Trim());
                                //    objNuevoAjuste.idParamEspec = -99999;
                                //    objNuevoAjuste.idTipoConsumidor = -99999;
                                //    objNuevoAjuste.idTipoInfraAux = MCodificaParametrica((int)EnumTipoParametrica.TipoInfraestructura, "SR");
                                //    objNuevoAjuste.idInfraAux = MCodificaParametrica((int)EnumTipoParametrica.Infraestructura, "SR|" + values[27].Trim());

                                //    objNuevoAjuste.listaTransaccion = JsonSerializer.SerializeToString(lstTransAjuste);
                                //}
                                //else
                                //{
                                //    objNuevoAjuste = null;
                                //}
                                if (Convert.ToDecimal(values[14].Trim()) > 0)
                                {
                                    objFactura = new ObjTransaccion();
                                    objFactura.idMovimiento = 0;
                                    objFactura.idTransaccion = 0;
                                    objFactura.idTransaccionPadre = 0;
                                    objFactura.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                    objFactura.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                    objFactura.idTipoTransaccion = (decimal)((values[16].Trim().Length > 4) ? EnumTipoTransaccion.FacturaComputarizada : EnumTipoTransaccion.FacturaManual);
                                    objFactura.fechaEmision = Convert.ToInt64(strFecha);
                                    objFactura.numeroTransaccion = values[14].Trim();
                                    objFactura.numeroAutorizacion = Convert.ToDecimal(values[15].Trim());
                                    objFactura.codigoControl = values[16].Trim();
                                    objFactura.nitConsumidor = Convert.ToDecimal(values[17].Trim());
                                    objFactura.razonSocial = cDato.StrRazonSocial;
                                    objFactura.precioUnitario = Convert.ToDecimal(values[27].Trim());
                                    objFactura.precioTotal = Convert.ToDecimal(values[28].Trim());
                                    objFactura.usuarioTransaccion = values[23].Trim();
                                    objFactura.observaciones = "";
                                    objFactura.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                    objFactura.idEstadoTransaccion = estadoTra;
                                    objFactura.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.Si;
                                    objFactura.idFormaPago = (int)EnumTiposPago.Efectivo;
                                    objFactura.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                    objFactura.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                    objFactura.codigoSeguridad = 0;
                                    objFactura.idAutorizacionDato = 0;
                                    objFactura.binarioControl = controlBinario + "000000000";
                                    objFactura.fechaOperacion = Convert.ToInt64(strFecha);
                                    objFactura.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                    objFactura.codigo = "";
                                    lstTransacciones.Add(objFactura);
                                    controlBinario = 0;
                                }

                                if (values[19].Trim().Length > 0)
                                {
                                    objDocumento = new ObjTransaccion();
                                    objDocumento.idMovimiento = 0;
                                    objDocumento.idTransaccion = 0;
                                    objDocumento.idTransaccionPadre = 0;
                                    objDocumento.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                    objDocumento.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                    objDocumento.idTipoTransaccion = (decimal)EnumTipoTransaccion.NotaMovimiento;
                                    objDocumento.fechaEmision = Convert.ToInt64(strFecha);
                                    objDocumento.numeroTransaccion = values[19].Trim();
                                    objDocumento.numeroAutorizacion = 0;
                                    objDocumento.codigoControl = "";
                                    objDocumento.nitConsumidor = 0;
                                    objDocumento.razonSocial = "";
                                    objDocumento.precioUnitario = 0;
                                    objDocumento.precioTotal = 0;
                                    objDocumento.usuarioTransaccion = values[23].Trim();
                                    objDocumento.observaciones = "";
                                    objDocumento.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                    objDocumento.idEstadoTransaccion = estadoTra;
                                    objDocumento.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.No;
                                    objDocumento.idFormaPago = (decimal)EnumTiposPago.Efectivo;
                                    objDocumento.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                    objDocumento.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                    objDocumento.codigoSeguridad = 0;
                                    objDocumento.idAutorizacionDato = 0;
                                    objDocumento.binarioControl = controlBinario + "000000000";
                                    objDocumento.fechaOperacion = Convert.ToInt64(strFecha);
                                    objDocumento.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                    objDocumento.codigo = "";
                                    lstTransacciones.Add(objDocumento);
                                    controlBinario = 0;
                                }
                                if (values[20].Trim().Length > 0)
                                {
                                    objDocumento = new ObjTransaccion();
                                    objDocumento.idMovimiento = 0;
                                    objDocumento.idTransaccion = 0;
                                    objDocumento.idTransaccionPadre = 0;
                                    objDocumento.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                    objDocumento.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString().PadLeft(2, '0') + datFechaReg.Day.ToString().PadLeft(2, '0') + datFechaReg.Hour.ToString().PadLeft(2, '0') + datFechaReg.Minute.ToString().PadLeft(2, '0') + datFechaReg.Second.ToString().PadLeft(2, '0'));
                                    objDocumento.idTipoTransaccion = (decimal)EnumTipoTransaccion.DocCre;
                                    objDocumento.fechaEmision = Convert.ToInt64(strFecha);
                                    objDocumento.numeroTransaccion = values[20].Trim();
                                    objDocumento.numeroAutorizacion = 0;
                                    objDocumento.codigoControl = "";
                                    objDocumento.nitConsumidor = 0;
                                    objDocumento.razonSocial = "";
                                    objDocumento.precioUnitario = 0;
                                    objDocumento.precioTotal = 0;
                                    objDocumento.usuarioTransaccion = values[23].Trim();
                                    objDocumento.observaciones = "";
                                    objDocumento.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                    objDocumento.idEstadoTransaccion = estadoTra;
                                    objDocumento.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.No;
                                    objDocumento.idFormaPago = (int)EnumTiposPago.Efectivo;
                                    objDocumento.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                    objDocumento.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                    objDocumento.codigoSeguridad = 0;
                                    objDocumento.idAutorizacionDato = 0;
                                    objDocumento.binarioControl = controlBinario + "000000000";
                                    objDocumento.fechaOperacion = Convert.ToInt64(strFecha);
                                    objDocumento.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                    objDocumento.codigo = "";
                                    lstTransacciones.Add(objDocumento);
                                }
                                if (values[12].Trim().Length > 0)
                                {
                                    var arreglo = values[12].Trim().Split('|');
                                    for (int i = 0; i < arreglo.Count(); i++)
                                    {
                                        objDocumento = new ObjTransaccion();
                                        objDocumento.idMovimiento = 0;
                                        objDocumento.idTransaccion = 0;
                                        objDocumento.idTransaccionPadre = 0;
                                        objDocumento.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                        objDocumento.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                        objDocumento.idTipoTransaccion = (decimal)EnumTipoTransaccion.DocPrecinto;
                                        objDocumento.fechaEmision = Convert.ToInt64(strFecha);
                                        objDocumento.numeroTransaccion = arreglo[i];
                                        objDocumento.numeroAutorizacion = 0;
                                        objDocumento.codigoControl = "";
                                        objDocumento.nitConsumidor = 0;
                                        objDocumento.razonSocial = "";
                                        objDocumento.precioUnitario = 0;
                                        objDocumento.precioTotal = 0;
                                        objDocumento.usuarioTransaccion = values[23].Trim();
                                        objDocumento.observaciones = "";
                                        objDocumento.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                        objDocumento.idEstadoTransaccion = estadoTra;
                                        objDocumento.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.No;
                                        objDocumento.idFormaPago = (int)EnumTiposPago.Efectivo;
                                        objDocumento.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                        objDocumento.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                        objDocumento.codigoSeguridad = 0;
                                        objDocumento.idAutorizacionDato = 0;
                                        objDocumento.binarioControl = "0000000000";
                                        objDocumento.fechaOperacion = Convert.ToInt64(strFecha);
                                        objDocumento.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                        objDocumento.codigo = "";
                                        lstTransacciones.Add(objDocumento);
                                    }
                                }
                                if (values[22].Trim().Length > 0)
                                {
                                    var arreglo = values[22].Trim().Split('|');
                                    for (int i = 0; i < arreglo.Count(); i++)
                                    {
                                        objDocumento = new ObjTransaccion();
                                        objDocumento.idMovimiento = 0;
                                        objDocumento.idTransaccion = 0;
                                        objDocumento.idTransaccionPadre = 0;
                                        objDocumento.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                        objDocumento.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString().PadLeft(2, '0') + datFechaReg.Day.ToString().PadLeft(2, '0') + datFechaReg.Hour.ToString().PadLeft(2, '0') + datFechaReg.Minute.ToString().PadLeft(2, '0') + datFechaReg.Second.ToString().PadLeft(2, '0'));
                                        objDocumento.idTipoTransaccion = (decimal)EnumTipoTransaccion.DocCalidad;
                                        objDocumento.fechaEmision = Convert.ToInt64(strFecha);
                                        objDocumento.numeroTransaccion = arreglo[i];
                                        objDocumento.numeroAutorizacion = 0;
                                        objDocumento.codigoControl = "";
                                        objDocumento.nitConsumidor = 0;
                                        objDocumento.razonSocial = "";
                                        objDocumento.precioUnitario = 0;
                                        objDocumento.precioTotal = 0;
                                        objDocumento.usuarioTransaccion = values[23].Trim();
                                        objDocumento.observaciones = "";
                                        objDocumento.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                        objDocumento.idEstadoTransaccion = estadoTra;
                                        objDocumento.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.No;
                                        objDocumento.idFormaPago = (int)EnumTiposPago.Efectivo;
                                        objDocumento.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                        objDocumento.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                        objDocumento.codigoSeguridad = 0;
                                        objDocumento.idAutorizacionDato = 0;
                                        objDocumento.binarioControl = "0000000000";
                                        objDocumento.fechaOperacion = Convert.ToInt64(strFecha);
                                        objDocumento.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                        objDocumento.codigo = "";
                                        lstTransacciones.Add(objDocumento);
                                    }
                                }
                                if (values[11].Trim().Length > 0)
                                {
                                    objDocumento = new ObjTransaccion();
                                    objDocumento.idMovimiento = 0;
                                    objDocumento.idTransaccion = 0;
                                    objDocumento.idTransaccionPadre = 0;
                                    objDocumento.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                    objDocumento.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString().PadLeft(2, '0') + datFechaReg.Day.ToString().PadLeft(2, '0') + datFechaReg.Hour.ToString().PadLeft(2, '0') + datFechaReg.Minute.ToString().PadLeft(2, '0') + datFechaReg.Second.ToString().PadLeft(2, '0'));
                                    objDocumento.idTipoTransaccion = (decimal)EnumTipoTransaccion.DocLote;
                                    objDocumento.fechaEmision = Convert.ToInt64(strFecha);
                                    objDocumento.numeroTransaccion = values[11].Trim();
                                    objDocumento.numeroAutorizacion = 0;
                                    objDocumento.codigoControl = "";
                                    objDocumento.nitConsumidor = 0;
                                    objDocumento.razonSocial = "";
                                    objDocumento.precioUnitario = 0;
                                    objDocumento.precioTotal = 0;
                                    objDocumento.usuarioTransaccion = values[23].Trim();
                                    objDocumento.observaciones = "";
                                    objDocumento.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                    objDocumento.idEstadoTransaccion = estadoTra;
                                    objDocumento.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.No;
                                    objDocumento.idFormaPago = (int)EnumTiposPago.Efectivo;
                                    objDocumento.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                    objDocumento.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                    objDocumento.codigoSeguridad = 0;
                                    objDocumento.idAutorizacionDato = 0;
                                    objDocumento.binarioControl = "0000000000";
                                    objDocumento.fechaOperacion = Convert.ToInt64(strFecha);
                                    objDocumento.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                    objDocumento.codigo = "";
                                    lstTransacciones.Add(objDocumento);
                                }

                                if (Convert.ToDecimal(values[21].Trim()) > 0)
                                {
                                    objDocumento = new ObjTransaccion();
                                    objDocumento.idMovimiento = 0;
                                    objDocumento.idTransaccion = 0;
                                    objDocumento.idTransaccionPadre = 0;
                                    objDocumento.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                    objDocumento.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString().PadLeft(2, '0') + datFechaReg.Day.ToString().PadLeft(2, '0') + datFechaReg.Hour.ToString().PadLeft(2, '0') + datFechaReg.Minute.ToString().PadLeft(2, '0') + datFechaReg.Second.ToString().PadLeft(2, '0'));
                                    objDocumento.idTipoTransaccion = (decimal)EnumTipoTransaccion.AutSustCtrl;
                                    objDocumento.fechaEmision = Convert.ToInt64(strFecha);
                                    objDocumento.numeroTransaccion = values[21].Trim();
                                    objDocumento.numeroAutorizacion = 0;
                                    objDocumento.codigoControl = "";
                                    objDocumento.nitConsumidor = 0;
                                    objDocumento.razonSocial = "";
                                    objDocumento.precioUnitario = 0;
                                    objDocumento.precioTotal = 0;
                                    objDocumento.usuarioTransaccion = values[23].Trim();
                                    objDocumento.observaciones = "";
                                    objDocumento.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                    objDocumento.idEstadoTransaccion = estadoTra;
                                    objDocumento.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.No;
                                    objDocumento.idFormaPago = (int)EnumTiposPago.Efectivo;
                                    objDocumento.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                    objDocumento.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                    objDocumento.codigoSeguridad = 0;
                                    objDocumento.idAutorizacionDato = 0;
                                    objDocumento.binarioControl = "0000000000";
                                    objDocumento.fechaOperacion = Convert.ToInt64(strFecha);
                                    objDocumento.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                    objDocumento.codigo = "";
                                    lstTransacciones.Add(objDocumento);
                                }
                                objNuevoTipoReg = new ObjProtoMovimientoTransaccion();
                                objNuevoTipoReg.idMovimientoPadre = 0;
                                objNuevoTipoReg.idTipoActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                objNuevoTipoReg.idTipoOperacion = Convert.ToDecimal(cmbTipoOperacion.Value);
                                objNuevoTipoReg.idConsumidor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString());
                                objNuevoTipoReg.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                objNuevoTipoReg.fechaRegistroMovimiento = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                objNuevoTipoReg.fechaOperacion = Convert.ToInt64(strFecha);
                                objNuevoTipoReg.idTipoMovimientoFlujo = (int)EnumTipoMovimientoFlujo.Fisico;
                                objNuevoTipoReg.idTipoMovimiento = (values[7].Trim() == "BOLIVIA") ? (int)EnumTipoMovimiento.NacNac : (int)EnumTipoMovimiento.IntNac;
                                objNuevoTipoReg.idEstadoMovimiento = estadoTra;
                                objNuevoTipoReg.idFuenteOrigenDato = (decimal)EnumTipoRegistro.OrigenCsv;
                                objNuevoTipoReg.idUbicacionOperacion = MCodificaParametrica((int)EnumTipoParametrica.Pais, values[7].Trim());
                                objNuevoTipoReg.idUnidadMedidaHidrocarburo = MCodificaParametrica((int)EnumTipoParametrica.UnidadMedida, values[25].Trim());
                                objNuevoTipoReg.idHidrocarburo = MCodificaParametrica((int)EnumTipoParametrica.Producto, values[25].Trim());
                                objNuevoTipoReg.cantidadHidrocarburo = cDato.DecVolumen;
                                objNuevoTipoReg.identificador = cDato.StrIdentificador;
                                objNuevoTipoReg.subIdentificador = "";
                                objNuevoTipoReg.observacion = "";
                                objNuevoTipoReg.tipoInfraestructura = MCodificaParametrica((int)EnumTipoParametrica.TipoInfraestructura, values[2].Trim());
                                objNuevoTipoReg.idInfraestructura = MCodificaParametrica((int)EnumTipoParametrica.Infraestructura, values[2].Trim() + "|" + values[3].Trim());
                                objNuevoTipoReg.idTipoMedicion = -99999;
                                objNuevoTipoReg.cantidadAguaLitros = 0;
                                objNuevoTipoReg.idUnidadTemperatura = (decimal)EnumUnidadTemperatura.GradoCentigrado;
                                objNuevoTipoReg.temperatura = Convert.ToDecimal(values[8]);
                                objNuevoTipoReg.gradoApi = values[9].Trim();
                                objNuevoTipoReg.idEntidad = MCodificaParametrica((int)EnumTipoParametrica.Entidad, values[4].Trim());
                                objNuevoTipoReg.idTipoMedioTransporte = MCodificaParametrica((int)EnumTipoParametrica.TipoInfraestructura, values[5].Trim());
                                objNuevoTipoReg.idMedioTransporte = MCodificaParametrica((int)EnumTipoParametrica.Infraestructura, values[5].Trim() + "|" + values[6].Trim());
                                objNuevoTipoReg.idUnidadEnvasesTransporte = -99999;
                                objNuevoTipoReg.cantidadEnvasesTransporte = 0;
                                objNuevoTipoReg.binarioControl = "00000-0000000000-0000000000-00000";
                                objNuevoTipoReg.idIntegracion = Convert.ToDecimal(values[1]);
                                objNuevoTipoReg.idTransaccionBaseExterna = "";
                                objNuevoTipoReg.codigo = values[13].Trim();
                                objNuevoTipoReg.barcode = 0;
                                objNuevoTipoReg.listaTransaccion = JsonSerializer.SerializeToString(lstTransacciones);

                                objNuevoTipoReg.jsonReferencia = "";
                                objNuevoTipoReg.estadoJsonRef = -99999;
                                objNuevoTipoReg.idCuenta = -99999;
                                objNuevoTipoReg.idCuentaAux = -99999;
                                objNuevoTipoReg.idParamGlobal = -99999;
                                objNuevoTipoReg.idParamEspec = -99999;
                                objNuevoTipoReg.idTipoConsumidor = -99999;
                                objNuevoTipoReg.idTipoInfraAux = -99999;
                                objNuevoTipoReg.idInfraAux = -99999;
                                #endregion
                                break;
                            case "DS":
                                #region Valida y genera objeto 07/10/2021
                                //cDato.LngId = lngNoRegCorrect;
                                //cDato.StrIdVenta = values[1].Trim();
                                //datFecha = DateTime.Parse(values[0].Trim());
                                //cDato.StrFechaEmision = datFecha.ToString();
                                //cDato.StrTipoMov = (values[5].Trim() == "BOLIVIA") ? "NAC" : "EXP";
                                //cDato.StrEstado = values[16].Trim();
                                //cDato.LngNit = 0;
                                //cDato.StrRazonSocial = values[4].Trim().ToUpper().Replace("¥", "Ñ").Replace("�", "Ñ");
                                //cDato.StrRazonSocial = Regex.Replace(cDato.StrRazonSocial, @"\s+", " ");
                                //cDato.StrIdentificador = values[10].Trim().Replace("-", "").Replace(" ", "");
                                //cDato.LngNroFactura = 0;
                                //cDato.LngNroAutorizacion = 0;
                                //cDato.StrNroReferencia = values[1].Trim();
                                //cDato.LngNroAutVenta = 0;
                                //cDato.StrProducto = values[14].Trim();
                                //cDato.DecVolumen = Convert.ToDecimal(values[18]);
                                //cDato.DecPrecio = 0;
                                //cDato.DecImporte = 0;
                                //cDato.DecIdTipoCreditoFiscal = (int)EnumTipoCredFiscal.No;
                                //cDato.DecIdTipoOp = Convert.ToDecimal(cmbTipoOperacion.Value);
                                //cDato.DecOrden = 1;

                                //strFecha = CFechas.ConvierteDateTimeLong(datFecha).ToString();
                                //datFechaReg = DateTime.Now;
                                //estadoTra = (cDato.StrEstado == "A") ? (int)EnumEstadoMov.Anulado : (int)EnumEstadoMov.Activo;
                                //lstTransacciones = new List<ObjTransaccion>();

                                //var objDespacho = new O_PAHL_DESP_DA
                                //{
                                //    FACTOR_CORRECCION_INI = Convert.ToDecimal(values[37].Trim()),
                                //    FACTOR_CORRECCION_FIN = Convert.ToDecimal(values[38].Trim()),
                                //    API_CORREGIDO = values[39].Trim(),
                                //    TEMPERATURA_OBSERV = Convert.ToDecimal(values[40].Trim()),
                                //    HORA_INICIO = values[41].Trim(),
                                //    HORA_FINAL = values[42].Trim(),
                                //    METER_INICIAL = Convert.ToDecimal(values[43].Trim()),
                                //    METER_FINAL = Convert.ToDecimal(values[44].Trim()),
                                //    API_OBSERV_INI = values[45].Trim(),
                                //    API_OBSERV_FIN = values[46].Trim(),
                                //    TEMPERATURA_TK_INI = Convert.ToDecimal(values[47].Trim()),
                                //    TEMPERATURA_TK_FIN = Convert.ToDecimal(values[48].Trim()),
                                //    VOL_NETO_INI = Convert.ToDecimal(values[49].Trim()),
                                //    VOL_AGUA_NETO_INI = Convert.ToDecimal(values[50].Trim()),
                                //    VOL_CORREGIDO_TK_INI = Convert.ToDecimal(values[51].Trim()),
                                //    VOL_CORREGIDO_TK_FIN = Convert.ToDecimal(values[52].Trim()),
                                //    VOL_NETO_FIN = Convert.ToDecimal(values[53].Trim()),
                                //    VOL_AGUA_NETO_FIN = Convert.ToDecimal(values[54].Trim()),
                                //    NIVEL_PROD_INI = Convert.ToDecimal(values[55].Trim()),
                                //    NIVEL_AGUA_INI = Convert.ToDecimal(values[56].Trim()),
                                //    NIVEL_PROD_FIN = Convert.ToDecimal(values[57].Trim()),
                                //    NIVEL_AGUA_FIN = Convert.ToDecimal(values[58].Trim()),
                                //    TECHO_FLOTANTE_INI = Convert.ToDecimal(values[59].Trim()),
                                //    TECHO_FLOTANTE_FIN = Convert.ToDecimal(values[60].Trim())
                                //};

                                //if (values[22].Trim().Length > 0)
                                //{
                                //    objDocumento = new ObjTransaccion();
                                //    objDocumento.idMovimiento = 0;
                                //    objDocumento.idTransaccion = 0;
                                //    objDocumento.idTransaccionPadre = 0;
                                //    objDocumento.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                //    objDocumento.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                //    objDocumento.idTipoTransaccion = (decimal)EnumTipoTransaccion.DocCuenta;
                                //    objDocumento.fechaEmision = Convert.ToInt64(strFecha);
                                //    objDocumento.numeroTransaccion = values[22].Trim();
                                //    objDocumento.numeroAutorizacion = 0;
                                //    objDocumento.codigoControl = "";
                                //    objDocumento.nitConsumidor = 0;
                                //    objDocumento.razonSocial = "";
                                //    objDocumento.precioUnitario = 0;
                                //    objDocumento.precioTotal = 0;
                                //    objDocumento.usuarioTransaccion = values[15].Trim();
                                //    objDocumento.observaciones = "";
                                //    objDocumento.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                //    objDocumento.idEstadoTransaccion = estadoTra;
                                //    objDocumento.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.No;
                                //    objDocumento.idFormaPago = (decimal)EnumTiposPago.Efectivo;
                                //    objDocumento.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                //    objDocumento.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                //    objDocumento.codigoSeguridad = 0;
                                //    objDocumento.idAutorizacionDato = 0;
                                //    objDocumento.binarioControl = intBinCtrl + "000000000";
                                //    objDocumento.fechaOperacion = Convert.ToInt64(strFecha);
                                //    objDocumento.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                //    objDocumento.codigo = "";
                                //    lstTransacciones.Add(objDocumento);
                                //    intBinCtrl = 0;
                                //}

                                //if (values[24].Trim().Length > 0)
                                //{
                                //    objDocumento = new ObjTransaccion();
                                //    objDocumento.idMovimiento = 0;
                                //    objDocumento.idTransaccion = 0;
                                //    objDocumento.idTransaccionPadre = 0;
                                //    objDocumento.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                //    objDocumento.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                //    objDocumento.idTipoTransaccion = (decimal)EnumTipoTransaccion.DocParteSalida;
                                //    objDocumento.fechaEmision = Convert.ToInt64(strFecha);
                                //    objDocumento.numeroTransaccion = values[24].Trim();
                                //    objDocumento.numeroAutorizacion = Convert.ToDecimal(values[26].Trim());
                                //    objDocumento.codigoControl = values[25].Trim();
                                //    objDocumento.nitConsumidor = 0;
                                //    objDocumento.razonSocial = "";
                                //    objDocumento.precioUnitario = 0;
                                //    objDocumento.precioTotal = 0;
                                //    objDocumento.usuarioTransaccion = values[15].Trim();
                                //    objDocumento.observaciones = "";
                                //    objDocumento.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                //    objDocumento.idEstadoTransaccion = estadoTra;
                                //    objDocumento.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.No;
                                //    objDocumento.idFormaPago = (decimal)EnumTiposPago.Efectivo;
                                //    objDocumento.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                //    objDocumento.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                //    objDocumento.codigoSeguridad = 0;
                                //    objDocumento.idAutorizacionDato = 0;
                                //    objDocumento.binarioControl = intBinCtrl + "000000000";
                                //    objDocumento.fechaOperacion = Convert.ToInt64(strFecha);
                                //    objDocumento.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                //    objDocumento.codigo = "HojaRuta:" + values[27].Trim() + " - RutaAuto:" + values[28].Trim();
                                //    lstTransacciones.Add(objDocumento);
                                //    intBinCtrl = 0;
                                //}

                                //if (values[29].Trim().Length > 0)
                                //{
                                //    objDocumento = new ObjTransaccion();
                                //    objDocumento.idMovimiento = 0;
                                //    objDocumento.idTransaccion = 0;
                                //    objDocumento.idTransaccionPadre = 0;
                                //    objDocumento.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                //    objDocumento.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                //    objDocumento.idTipoTransaccion = (decimal)EnumTipoTransaccion.DocCre;
                                //    objDocumento.fechaEmision = Convert.ToInt64(strFecha);
                                //    objDocumento.numeroTransaccion = values[29].Trim();
                                //    objDocumento.numeroAutorizacion = 0;
                                //    objDocumento.codigoControl = "";
                                //    objDocumento.nitConsumidor = Convert.ToDecimal(values[30].Trim());
                                //    objDocumento.razonSocial = values[31].Trim();
                                //    objDocumento.precioUnitario = 0;
                                //    objDocumento.precioTotal = 0;
                                //    objDocumento.usuarioTransaccion = values[15].Trim();
                                //    objDocumento.observaciones = "";
                                //    objDocumento.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                //    objDocumento.idEstadoTransaccion = estadoTra;
                                //    objDocumento.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.No;
                                //    objDocumento.idFormaPago = (decimal)EnumTiposPago.Efectivo;
                                //    objDocumento.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                //    objDocumento.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                //    objDocumento.codigoSeguridad = 0;
                                //    objDocumento.idAutorizacionDato = 0;
                                //    objDocumento.binarioControl = intBinCtrl + "000000000";
                                //    objDocumento.fechaOperacion = Convert.ToInt64(strFecha);
                                //    objDocumento.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                //    objDocumento.codigo = "";
                                //    lstTransacciones.Add(objDocumento);
                                //    intBinCtrl = 0;
                                //}

                                //if (lstTransacciones.Count == 0)
                                //{
                                //    objDocumento = new ObjTransaccion();
                                //    objDocumento.idMovimiento = 0;
                                //    objDocumento.idTransaccion = 0;
                                //    objDocumento.idTransaccionPadre = 0;
                                //    objDocumento.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                //    objDocumento.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                //    objDocumento.idTipoTransaccion = (decimal)EnumTipoTransaccion.NotaMovimiento;
                                //    objDocumento.fechaEmision = Convert.ToInt64(strFecha);
                                //    objDocumento.numeroTransaccion = values[1].Trim();
                                //    objDocumento.numeroAutorizacion = 0;
                                //    objDocumento.codigoControl = "";
                                //    objDocumento.nitConsumidor = 0;
                                //    objDocumento.razonSocial = "";
                                //    objDocumento.precioUnitario = 0;
                                //    objDocumento.precioTotal = 0;
                                //    objDocumento.usuarioTransaccion = values[15].Trim();
                                //    objDocumento.observaciones = "";
                                //    objDocumento.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                //    objDocumento.idEstadoTransaccion = estadoTra;
                                //    objDocumento.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.No;
                                //    objDocumento.idFormaPago = (decimal)EnumTiposPago.Efectivo;
                                //    objDocumento.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                //    objDocumento.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                //    objDocumento.codigoSeguridad = 0;
                                //    objDocumento.idAutorizacionDato = 0;
                                //    objDocumento.binarioControl = intBinCtrl + "000000000";
                                //    objDocumento.fechaOperacion = Convert.ToInt64(strFecha);
                                //    objDocumento.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                //    objDocumento.codigo = "";
                                //    lstTransacciones.Add(objDocumento);
                                //}

                                //if (values[33].Trim().Length > 0)
                                //{
                                //    var arreglo = values[33].Trim().Split('|');
                                //    for (int i = 0; i < arreglo.Count(); i++)
                                //    {
                                //        objDocumento = new ObjTransaccion();
                                //        objDocumento.idMovimiento = 0;
                                //        objDocumento.idTransaccion = 0;
                                //        objDocumento.idTransaccionPadre = 0;
                                //        objDocumento.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                //        objDocumento.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                //        objDocumento.idTipoTransaccion = (decimal)EnumTipoTransaccion.DocPrecinto;
                                //        objDocumento.fechaEmision = Convert.ToInt64(strFecha);
                                //        objDocumento.numeroTransaccion = arreglo[i];
                                //        objDocumento.numeroAutorizacion = 0;
                                //        objDocumento.codigoControl = "";
                                //        objDocumento.nitConsumidor = 0;
                                //        objDocumento.razonSocial = "";
                                //        objDocumento.precioUnitario = 0;
                                //        objDocumento.precioTotal = 0;
                                //        objDocumento.usuarioTransaccion = values[15].Trim();
                                //        objDocumento.observaciones = "";
                                //        objDocumento.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                //        objDocumento.idEstadoTransaccion = estadoTra;
                                //        objDocumento.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.No;
                                //        objDocumento.idFormaPago = (int)EnumTiposPago.Efectivo;
                                //        objDocumento.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                //        objDocumento.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                //        objDocumento.codigoSeguridad = 0;
                                //        objDocumento.idAutorizacionDato = 0;
                                //        objDocumento.binarioControl = "0000000000";
                                //        objDocumento.fechaOperacion = Convert.ToInt64(strFecha);
                                //        objDocumento.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                //        objDocumento.codigo = values[34].Trim();
                                //        lstTransacciones.Add(objDocumento);
                                //    }
                                //}
                                //if (values[35].Trim().Length > 0)
                                //{
                                //    var arreglo = values[35].Trim().Split('|');
                                //    for (int i = 0; i < arreglo.Count(); i++)
                                //    {
                                //        objDocumento = new ObjTransaccion();
                                //        objDocumento.idMovimiento = 0;
                                //        objDocumento.idTransaccion = 0;
                                //        objDocumento.idTransaccionPadre = 0;
                                //        objDocumento.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                //        objDocumento.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                //        objDocumento.idTipoTransaccion = (decimal)EnumTipoTransaccion.DocLote;
                                //        objDocumento.fechaEmision = Convert.ToInt64(strFecha);
                                //        objDocumento.numeroTransaccion = arreglo[i];
                                //        objDocumento.numeroAutorizacion = 0;
                                //        objDocumento.codigoControl = "";
                                //        objDocumento.nitConsumidor = 0;
                                //        objDocumento.razonSocial = "";
                                //        objDocumento.precioUnitario = 0;
                                //        objDocumento.precioTotal = 0;
                                //        objDocumento.usuarioTransaccion = values[15].Trim();
                                //        objDocumento.observaciones = "";
                                //        objDocumento.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                //        objDocumento.idEstadoTransaccion = estadoTra;
                                //        objDocumento.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.No;
                                //        objDocumento.idFormaPago = (int)EnumTiposPago.Efectivo;
                                //        objDocumento.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                //        objDocumento.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                //        objDocumento.codigoSeguridad = 0;
                                //        objDocumento.idAutorizacionDato = 0;
                                //        objDocumento.binarioControl = "0000000000";
                                //        objDocumento.fechaOperacion = Convert.ToInt64(strFecha);
                                //        objDocumento.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                //        objDocumento.codigo = "";
                                //        lstTransacciones.Add(objDocumento);
                                //    }
                                //}
                                //if (values[36].Trim().Length > 0)
                                //{
                                //    var arreglo = values[36].Trim().Split('|');
                                //    for (int i = 0; i < arreglo.Count(); i++)
                                //    {
                                //        objDocumento = new ObjTransaccion();
                                //        objDocumento.idMovimiento = 0;
                                //        objDocumento.idTransaccion = 0;
                                //        objDocumento.idTransaccionPadre = 0;
                                //        objDocumento.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                //        objDocumento.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                //        objDocumento.idTipoTransaccion = (decimal)EnumTipoTransaccion.DocCalidad;
                                //        objDocumento.fechaEmision = Convert.ToInt64(strFecha);
                                //        objDocumento.numeroTransaccion = arreglo[i];
                                //        objDocumento.numeroAutorizacion = 0;
                                //        objDocumento.codigoControl = "";
                                //        objDocumento.nitConsumidor = 0;
                                //        objDocumento.razonSocial = "";
                                //        objDocumento.precioUnitario = 0;
                                //        objDocumento.precioTotal = 0;
                                //        objDocumento.usuarioTransaccion = values[15].Trim();
                                //        objDocumento.observaciones = "";
                                //        objDocumento.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                //        objDocumento.idEstadoTransaccion = estadoTra;
                                //        objDocumento.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.No;
                                //        objDocumento.idFormaPago = (int)EnumTiposPago.Efectivo;
                                //        objDocumento.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                //        objDocumento.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                //        objDocumento.codigoSeguridad = 0;
                                //        objDocumento.idAutorizacionDato = 0;
                                //        objDocumento.binarioControl = "0000000000";
                                //        objDocumento.fechaOperacion = Convert.ToInt64(strFecha);
                                //        objDocumento.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                //        objDocumento.codigo = "";
                                //        lstTransacciones.Add(objDocumento);
                                //    }
                                //}

                                //objNuevoTipoReg = new ObjProtoMovimientoTransaccion();
                                //objNuevoTipoReg.idMovimientoPadre = 0;
                                //objNuevoTipoReg.idTipoActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                //objNuevoTipoReg.idTipoOperacion = Convert.ToDecimal(cmbTipoOperacion.Value);
                                //objNuevoTipoReg.idConsumidor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString());
                                //objNuevoTipoReg.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                //objNuevoTipoReg.fechaRegistroMovimiento = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                //objNuevoTipoReg.fechaOperacion = Convert.ToInt64(strFecha);
                                //objNuevoTipoReg.idTipoMovimientoFlujo = (values[17].Trim() == "CTA") ? (int)EnumTipoMovimientoFlujo.Contable : (int)EnumTipoMovimientoFlujo.Fisico;
                                //objNuevoTipoReg.idTipoMovimiento = (values[5].Trim() == "BOLIVIA") ? (int)EnumTipoMovimiento.NacNac : (int)EnumTipoMovimiento.NacInt;
                                //objNuevoTipoReg.idEstadoMovimiento = estadoTra;
                                //objNuevoTipoReg.idFuenteOrigenDato = (decimal)EnumTipoRegistro.OrigenCsv;
                                //objNuevoTipoReg.idUbicacionOperacion = MCodificaParametrica((int)EnumTipoParametrica.Pais, values[5].Trim());
                                //objNuevoTipoReg.idUnidadMedidaHidrocarburo = MCodificaParametrica((int)EnumTipoParametrica.UnidadMedida, values[14].Trim());
                                //objNuevoTipoReg.idHidrocarburo = MCodificaParametrica((int)EnumTipoParametrica.Producto, values[14].Trim());
                                //objNuevoTipoReg.cantidadHidrocarburo = cDato.DecVolumen;
                                //objNuevoTipoReg.identificador = cDato.StrIdentificador;
                                //objNuevoTipoReg.subIdentificador = "";
                                //objNuevoTipoReg.observacion = "";
                                //objNuevoTipoReg.tipoInfraestructura = MCodificaParametrica((int)EnumTipoParametrica.TipoInfraestructura, values[6].Trim());
                                //objNuevoTipoReg.idInfraestructura = MCodificaParametrica((int)EnumTipoParametrica.Infraestructura, values[6].Trim() + "|" + values[7].Trim());
                                //objNuevoTipoReg.idTipoMedicion = -99999;
                                //objNuevoTipoReg.cantidadAguaLitros = 0;
                                //objNuevoTipoReg.idUnidadTemperatura = (decimal)EnumUnidadTemperatura.GradoFarenheit;
                                //objNuevoTipoReg.temperatura = Convert.ToDecimal(values[8]);
                                //objNuevoTipoReg.gradoApi = values[9].Trim();
                                //objNuevoTipoReg.idEntidad = MCodificaParametrica((int)EnumTipoParametrica.Entidad, values[4].Trim());
                                //objNuevoTipoReg.idTipoMedioTransporte = MCodificaParametrica((int)EnumTipoParametrica.TipoInfraestructura, values[2].Trim());
                                //objNuevoTipoReg.idMedioTransporte = MCodificaParametrica((int)EnumTipoParametrica.Infraestructura, values[2].Trim() + "|" + values[3].Trim());
                                //objNuevoTipoReg.idUnidadEnvasesTransporte = -99999;
                                //objNuevoTipoReg.cantidadEnvasesTransporte = 0;
                                //objNuevoTipoReg.binarioControl = "00000-0000000000-0000000000-00000";
                                //objNuevoTipoReg.idIntegracion = Convert.ToDecimal(values[1]);
                                //objNuevoTipoReg.idTransaccionBaseExterna = "";
                                //objNuevoTipoReg.codigo = values[12].Trim();
                                //objNuevoTipoReg.barcode = Convert.ToDecimal(values[13]);

                                //objNuevoTipoReg.jsonReferencia = JsonSerializer.SerializeToString(objDespacho);
                                //objNuevoTipoReg.estadoJsonRef = -99999;
                                //objNuevoTipoReg.idCuenta = MCodificaParametrica((int)EnumTipoParametrica.Cuenta, values[20].Trim());
                                //objNuevoTipoReg.idCuentaAux = MCodificaParametrica((int)EnumTipoParametrica.Cuenta, values[21].Trim());
                                //objNuevoTipoReg.idParamGlobal = MCodificaParametrica((int)EnumTipoParametrica.GlobalClasif1, values[23].Trim());
                                //objNuevoTipoReg.idParamEspec = -99999;
                                //objNuevoTipoReg.idTipoConsumidor = -99999;
                                //objNuevoTipoReg.idTipoInfraAux = MCodificaParametrica((int)EnumTipoParametrica.TipoInfraestructura, "SC");
                                //objNuevoTipoReg.idInfraAux = MCodificaParametrica((int)EnumTipoParametrica.Infraestructura, "SC|" + values[32].Trim());

                                //objNuevoTipoReg.listaTransaccion = JsonSerializer.SerializeToString(lstTransacciones);

                                //if (Convert.ToDecimal(values[19]) != 0 && values[16].Trim() == "V" && values[17].Trim() == "FIS")
                                //{
                                //    cDatoAj = new CRegistrosImportados();
                                //    cDatoAj.LngId = lngNoRegCorrect;
                                //    cDatoAj.StrIdVenta = values[1].Trim();
                                //    datFecha = DateTime.Parse(values[0].Trim());
                                //    cDatoAj.StrFechaEmision = datFecha.ToString();
                                //    cDatoAj.StrTipoMov = (values[5].Trim() == "BOLIVIA") ? "NAC" : "EXP";
                                //    cDatoAj.StrEstado = values[16].Trim();
                                //    cDatoAj.LngNit = 0;
                                //    cDatoAj.StrRazonSocial = "REGISTRO POR DIFERENCIA.";
                                //    cDatoAj.StrIdentificador = values[10].Trim();
                                //    cDatoAj.LngNroFactura = 0;
                                //    cDatoAj.LngNroAutorizacion = 0;
                                //    cDatoAj.StrNroReferencia = values[1].Trim();
                                //    cDatoAj.LngNroAutVenta = 0;
                                //    cDatoAj.StrProducto = values[14].Trim();
                                //    cDatoAj.DecVolumen = Math.Abs(Convert.ToDecimal(values[19]));
                                //    cDatoAj.DecPrecio = 0;
                                //    cDatoAj.DecImporte = 0;
                                //    cDatoAj.DecIdTipoCreditoFiscal = (int)EnumTipoCredFiscal.No;

                                //    objDocumento = new ObjTransaccion();
                                //    objDocumento.idMovimiento = 0;
                                //    objDocumento.idTransaccion = 0;
                                //    objDocumento.idTransaccionPadre = 0;
                                //    objDocumento.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                //    objDocumento.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                //    objDocumento.idTipoTransaccion = (decimal)EnumTipoTransaccion.NotaMovimiento;
                                //    objDocumento.fechaEmision = Convert.ToInt64(strFecha);
                                //    objDocumento.numeroTransaccion = values[1].Trim();
                                //    objDocumento.numeroAutorizacion = MCodificaParametrica((int)EnumTipoParametrica.Cuenta, values[20].Trim());
                                //    objDocumento.codigoControl = values[21].Trim();
                                //    objDocumento.nitConsumidor = 0;
                                //    objDocumento.razonSocial = values[22].Trim(); ;
                                //    objDocumento.precioUnitario = 0;
                                //    objDocumento.precioTotal = 0;
                                //    objDocumento.usuarioTransaccion = values[15].Trim();
                                //    objDocumento.observaciones = "";
                                //    objDocumento.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                //    objDocumento.idEstadoTransaccion = estadoTra;
                                //    objDocumento.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.No;
                                //    objDocumento.idFormaPago = (decimal)EnumTiposPago.Efectivo;
                                //    objDocumento.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                //    objDocumento.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                //    objDocumento.codigoSeguridad = 0;
                                //    objDocumento.idAutorizacionDato = 0;
                                //    objDocumento.binarioControl = "1000000000";
                                //    objDocumento.fechaOperacion = Convert.ToInt64(strFecha);
                                //    objDocumento.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                //    objDocumento.codigo = "";
                                //    lstTransAjuste.Add(objDocumento);

                                //    var lstTipoOp = (List<O_TIPOS_OPE_ACT_CTY>)Session[CVariablesSesion.ComboTipoOperacion];
                                //    var idTipoOp = lstTipoOp.Where(x => x.CODIGO == ((Convert.ToDecimal(values[19]) > 0) ? "C2DSAJ" : "C2RCAJ")).ToList().FirstOrDefault().ID_TIPO_OPERACION;

                                //    cDatoAj.DecIdTipoOp = idTipoOp;
                                //    cDatoAj.DecOrden = 0;

                                //    objNuevoAjuste = new ObjProtoMovimientoTransaccion();
                                //    objNuevoAjuste.idMovimientoPadre = 0;
                                //    objNuevoAjuste.idTipoActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                //    objNuevoAjuste.idTipoOperacion = idTipoOp;
                                //    objNuevoAjuste.idConsumidor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString());
                                //    objNuevoAjuste.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                //    objNuevoAjuste.fechaRegistroMovimiento = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                //    objNuevoAjuste.fechaOperacion = Convert.ToInt64(strFecha);
                                //    objNuevoAjuste.idTipoMovimientoFlujo = (int)EnumTipoMovimientoFlujo.Fisico;
                                //    objNuevoAjuste.idTipoMovimiento = (values[5].Trim() == "BOLIVIA") ? (int)EnumTipoMovimiento.NacNac : (int)EnumTipoMovimiento.NacInt;
                                //    objNuevoAjuste.idEstadoMovimiento = estadoTra;
                                //    objNuevoAjuste.idFuenteOrigenDato = (decimal)EnumTipoRegistro.OrigenCsv;
                                //    objNuevoAjuste.idUbicacionOperacion = MCodificaParametrica((int)EnumTipoParametrica.Pais, values[5].Trim());
                                //    objNuevoAjuste.idUnidadMedidaHidrocarburo = MCodificaParametrica((int)EnumTipoParametrica.UnidadMedida, values[14].Trim());
                                //    objNuevoAjuste.idHidrocarburo = MCodificaParametrica((int)EnumTipoParametrica.Producto, values[14].Trim());
                                //    objNuevoAjuste.cantidadHidrocarburo = Math.Abs(Convert.ToDecimal(values[19]));
                                //    objNuevoAjuste.identificador = cDato.StrIdentificador;
                                //    objNuevoAjuste.subIdentificador = "";
                                //    objNuevoAjuste.observacion = "Generado por diferencia - " + cmbTipoOperacion.Text + " - " + values[1].Trim();
                                //    objNuevoAjuste.tipoInfraestructura = MCodificaParametrica((int)EnumTipoParametrica.TipoInfraestructura, values[6].Trim());
                                //    objNuevoAjuste.idInfraestructura = MCodificaParametrica((int)EnumTipoParametrica.Infraestructura, values[6].Trim() + "|" + values[7].Trim());
                                //    objNuevoAjuste.idTipoMedicion = -99999;
                                //    objNuevoAjuste.cantidadAguaLitros = 0;
                                //    objNuevoAjuste.idUnidadTemperatura = (decimal)EnumUnidadTemperatura.GradoFarenheit;
                                //    objNuevoAjuste.temperatura = Convert.ToDecimal(values[8]);
                                //    objNuevoAjuste.gradoApi = values[9].Trim();
                                //    objNuevoAjuste.idEntidad = MCodificaParametrica((int)EnumTipoParametrica.Entidad, values[4].Trim());
                                //    objNuevoAjuste.idTipoMedioTransporte = MCodificaParametrica((int)EnumTipoParametrica.TipoInfraestructura, values[2].Trim());
                                //    objNuevoAjuste.idMedioTransporte = MCodificaParametrica((int)EnumTipoParametrica.Infraestructura, values[2].Trim() + "|" + values[3].Trim());
                                //    objNuevoAjuste.idUnidadEnvasesTransporte = -99999;
                                //    objNuevoAjuste.cantidadEnvasesTransporte = 0;
                                //    objNuevoAjuste.binarioControl = "00000-0000000000-0000000000-00000";
                                //    objNuevoAjuste.idIntegracion = Convert.ToDecimal(values[1]);
                                //    objNuevoAjuste.idTransaccionBaseExterna = "";
                                //    objNuevoAjuste.codigo = values[12].Trim();
                                //    objNuevoAjuste.barcode = Convert.ToDecimal(values[13]);

                                //    objNuevoAjuste.jsonReferencia = "";
                                //    objNuevoAjuste.estadoJsonRef = 1;
                                //    objNuevoAjuste.idCuenta = MCodificaParametrica((int)EnumTipoParametrica.Cuenta, values[20].Trim());
                                //    objNuevoAjuste.idCuentaAux = MCodificaParametrica((int)EnumTipoParametrica.Cuenta, values[21].Trim());
                                //    objNuevoAjuste.idParamGlobal = MCodificaParametrica((int)EnumTipoParametrica.GlobalClasif1, values[23].Trim());
                                //    objNuevoAjuste.idParamEspec = -99999;
                                //    objNuevoAjuste.idTipoConsumidor = -99999;
                                //    objNuevoAjuste.idTipoInfraAux = MCodificaParametrica((int)EnumTipoParametrica.TipoInfraestructura, "SC");
                                //    objNuevoAjuste.idInfraAux = MCodificaParametrica((int)EnumTipoParametrica.Infraestructura, "SC|" + values[32].Trim());

                                //    objNuevoAjuste.listaTransaccion = JsonSerializer.SerializeToString(lstTransAjuste);
                                //}
                                //else
                                //{
                                //    objNuevoAjuste = null;
                                //}

                                #endregion
                                #region Valida y genera objeto
                                cDato.LngId = lngNoRegCorrect;
                                cDato.StrIdVenta = values[1].Trim();
                                datFecha = DateTime.Parse(values[0].Trim());
                                cDato.StrFechaEmision = datFecha.ToString();
                                cDato.StrTipoMov = "NAC";
                                cDato.StrEstado = values[22].Trim();
                                cDato.LngNit = Convert.ToInt64(values[15]);
                                cDato.StrRazonSocial = values[16].Trim().ToUpper().Replace("¥", "Ñ").Replace("Á", "A").Replace("É", "E").Replace("Í", "I").Replace("Ó", "O").Replace("Ú", "U").Replace("'", "").Replace("�", "Ñ");
                                cDato.StrRazonSocial = Regex.Replace(cDato.StrRazonSocial, @"\s+", " ");
                                cDato.StrIdentificador = values[9].Trim().Replace("-", "").Replace(" ", "");
                                cDato.LngNroFactura = Convert.ToInt64(values[12]);
                                cDato.LngNroAutorizacion = Convert.ToInt64(values[13]);
                                cDato.StrNroReferencia = values[17].Trim();
                                cDato.LngNroAutVenta = Convert.ToInt64(values[18]);
                                cDato.StrProducto = values[23].Trim();
                                cDato.DecVolumen = Convert.ToDecimal(values[24]);
                                cDato.DecPrecio = Convert.ToDecimal(values[25]);
                                cDato.DecImporte = Convert.ToDecimal(values[26]);
                                cDato.DecIdTipoCreditoFiscal = (cDato.LngNroFactura > 0) ? (int)EnumTipoCredFiscal.Si : (int)EnumTipoCredFiscal.No;
                                cDato.DecIdTipoOp = Convert.ToDecimal(cmbTipoOperacion.Value);
                                cDato.DecOrden = 1;

                                strFecha = datFecha.Year + datFecha.Month.ToString().PadLeft(2, '0') + datFecha.Day.ToString().PadLeft(2, '0') + datFecha.Hour.ToString().PadLeft(2, '0') + datFecha.Minute.ToString().PadLeft(2, '0') + datFecha.Second.ToString().PadLeft(2, '0');
                                datFechaReg = DateTime.Now;
                                estadoTra = (cDato.StrEstado == "A") ? (int)EnumEstadoMov.Anulado : (int)EnumEstadoMov.Activo;
                                lstTransacciones = new List<ObjTransaccion>();
                                controlBinario = 1;

                                if (Convert.ToDecimal(values[12].Trim()) > 0)
                                {
                                    objFactura = new ObjTransaccion();
                                    objFactura.idMovimiento = 0;
                                    objFactura.idTransaccion = 0;
                                    objFactura.idTransaccionPadre = 0;
                                    objFactura.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                    objFactura.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString().PadLeft(2, '0'));
                                    objFactura.idTipoTransaccion = (decimal)((values[13].Trim().Length > 4) ? EnumTipoTransaccion.FacturaComputarizada : EnumTipoTransaccion.FacturaManual);
                                    objFactura.fechaEmision = Convert.ToInt64(strFecha);
                                    objFactura.numeroTransaccion = values[12].Trim();
                                    objFactura.numeroAutorizacion = Convert.ToDecimal(values[13].Trim());
                                    objFactura.codigoControl = values[14].Trim();
                                    objFactura.nitConsumidor = Convert.ToDecimal(values[15].Trim());
                                    objFactura.razonSocial = cDato.StrRazonSocial;
                                    objFactura.precioUnitario = Convert.ToDecimal(values[25].Trim());
                                    objFactura.precioTotal = Convert.ToDecimal(values[26].Trim());
                                    objFactura.usuarioTransaccion = values[21].Trim();
                                    objFactura.observaciones = "";
                                    objFactura.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                    objFactura.idEstadoTransaccion = estadoTra;
                                    objFactura.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.Si;
                                    objFactura.idFormaPago = (int)EnumTiposPago.Efectivo;
                                    objFactura.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                    objFactura.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                    objFactura.codigoSeguridad = 0;
                                    objFactura.idAutorizacionDato = 0;
                                    objFactura.binarioControl = controlBinario + "000000000";
                                    objFactura.fechaOperacion = Convert.ToInt64(strFecha);
                                    objFactura.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                    objFactura.codigo = "";
                                    lstTransacciones.Add(objFactura);
                                    controlBinario = 0;
                                }

                                if (values[17].Trim().Length > 0)
                                {
                                    objDocumento = new ObjTransaccion();
                                    objDocumento.idMovimiento = 0;
                                    objDocumento.idTransaccion = 0;
                                    objDocumento.idTransaccionPadre = 0;
                                    objDocumento.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                    objDocumento.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                    objDocumento.idTipoTransaccion = (decimal)EnumTipoTransaccion.NotaMovimiento;
                                    objDocumento.fechaEmision = Convert.ToInt64(strFecha);
                                    objDocumento.numeroTransaccion = values[17].Trim();
                                    objDocumento.numeroAutorizacion = 0;
                                    objDocumento.codigoControl = "";
                                    objDocumento.nitConsumidor = 0;
                                    objDocumento.razonSocial = "";
                                    objDocumento.precioUnitario = 0;
                                    objDocumento.precioTotal = 0;
                                    objDocumento.usuarioTransaccion = values[21].Trim();
                                    objDocumento.observaciones = "";
                                    objDocumento.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                    objDocumento.idEstadoTransaccion = estadoTra;
                                    objDocumento.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.No;
                                    objDocumento.idFormaPago = (decimal)EnumTiposPago.Efectivo;
                                    objDocumento.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                    objDocumento.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                    objDocumento.codigoSeguridad = 0;
                                    objDocumento.idAutorizacionDato = 0;
                                    objDocumento.binarioControl = controlBinario + "000000000";
                                    objDocumento.fechaOperacion = Convert.ToInt64(strFecha);
                                    objDocumento.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                    objDocumento.codigo = "";
                                    lstTransacciones.Add(objDocumento);
                                    controlBinario = 0;
                                }

                                if (values[18].Trim().Length > 0)
                                {
                                    objDocumento = new ObjTransaccion();
                                    objDocumento.idMovimiento = 0;
                                    objDocumento.idTransaccion = 0;
                                    objDocumento.idTransaccionPadre = 0;
                                    objDocumento.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                    objDocumento.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                    objDocumento.idTipoTransaccion = (decimal)EnumTipoTransaccion.DocDespacho;
                                    objDocumento.fechaEmision = Convert.ToInt64(strFecha);
                                    objDocumento.numeroTransaccion = values[18].Trim();
                                    objDocumento.numeroAutorizacion = 0;
                                    objDocumento.codigoControl = "";
                                    objDocumento.nitConsumidor = 0;
                                    objDocumento.razonSocial = "";                                  
                                    objDocumento.precioUnitario = 0;
                                    objDocumento.precioTotal = 0;
                                    objDocumento.usuarioTransaccion = values[21].Trim();
                                    objDocumento.observaciones = "";
                                    objDocumento.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                    objDocumento.idEstadoTransaccion = estadoTra;
                                    objDocumento.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.No;
                                    objDocumento.idFormaPago = (decimal)EnumTiposPago.Efectivo;
                                    objDocumento.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                    objDocumento.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                    objDocumento.codigoSeguridad = 0;
                                    objDocumento.idAutorizacionDato = 0;
                                    objDocumento.binarioControl = controlBinario + "000000000";
                                    objDocumento.fechaOperacion = Convert.ToInt64(strFecha);
                                    objDocumento.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                    objDocumento.codigo = "";
                                    lstTransacciones.Add(objDocumento);
                                }

                                var decIdTipoDespacho = 33;
                                if (Convert.ToDecimal(values[19].Trim()) > 0)
                                {
                                    objDocumento = new ObjTransaccion();
                                    objDocumento.idMovimiento = 0;
                                    objDocumento.idTransaccion = 0;
                                    objDocumento.idTransaccionPadre = 0;
                                    objDocumento.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                    objDocumento.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                    objDocumento.idTipoTransaccion = (decimal)EnumTipoTransaccion.AutSustCtrl;
                                    objDocumento.fechaEmision = Convert.ToInt64(strFecha);
                                    objDocumento.numeroTransaccion = values[19].Trim();
                                    objDocumento.numeroAutorizacion = 0;
                                    objDocumento.codigoControl = "";
                                    objDocumento.nitConsumidor = 0;
                                    objDocumento.razonSocial = "";
                                    objDocumento.precioUnitario = 0;
                                    objDocumento.precioTotal = 0;
                                    objDocumento.usuarioTransaccion = values[21].Trim();
                                    objDocumento.observaciones = "";
                                    objDocumento.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                    objDocumento.idEstadoTransaccion = estadoTra;
                                    objDocumento.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.No;
                                    objDocumento.idFormaPago = (decimal)EnumTiposPago.Efectivo;
                                    objDocumento.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                    objDocumento.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                    objDocumento.codigoSeguridad = 0;
                                    objDocumento.idAutorizacionDato = 0;
                                    objDocumento.binarioControl = "0000000000";
                                    objDocumento.fechaOperacion = Convert.ToInt64(strFecha);
                                    objDocumento.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                    objDocumento.codigo = "";
                                    lstTransacciones.Add(objDocumento);
                                    decIdTipoDespacho = 34;
                                }
                                //if (values[24].Trim().Length > 0)
                                //{
                                //    objDocumento = new ObjTransaccion();
                                //    objDocumento.idMovimiento = 0;
                                //    objDocumento.idTransaccion = 0;
                                //    objDocumento.idTransaccionPadre = 0;
                                //    objDocumento.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                //    objDocumento.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                //    objDocumento.idTipoTransaccion = (decimal)EnumTipoTransaccion.AutAnhVfa;
                                //    objDocumento.fechaEmision = Convert.ToInt64(strFecha);
                                //    objDocumento.numeroTransaccion = values[24].Trim();
                                //    objDocumento.numeroAutorizacion = 0;
                                //    objDocumento.codigoControl = "";
                                //    objDocumento.nitConsumidor = 0;
                                //    objDocumento.razonSocial = "";
                                //    objDocumento.precioUnitario = 0;
                                //    objDocumento.precioTotal = 0;
                                //    objDocumento.usuarioTransaccion = values[26].Trim();
                                //    objDocumento.observaciones = "";
                                //    objDocumento.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                //    objDocumento.idEstadoTransaccion = estadoTra;
                                //    objDocumento.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.No;
                                //    objDocumento.idFormaPago = (decimal)EnumTiposPago.Efectivo;
                                //    objDocumento.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                //    objDocumento.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                //    objDocumento.codigoSeguridad = 0;
                                //    objDocumento.idAutorizacionDato = 0;
                                //    objDocumento.binarioControl = "0000000000";
                                //    objDocumento.fechaOperacion = Convert.ToInt64(strFecha);
                                //    objDocumento.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                //    objDocumento.codigo = "";
                                //    lstTransacciones.Add(objDocumento);
                                //    decIdTipoDespacho = 34;
                                //}
                                if (values[20].Trim().Length > 0)
                                {
                                    var arreglo = values[20].Trim().Split('|');
                                    for (int i = 0; i < arreglo.Count(); i++)
                                    {
                                        objDocumento = new ObjTransaccion();
                                        objDocumento.idMovimiento = 0;
                                        objDocumento.idTransaccion = 0;
                                        objDocumento.idTransaccionPadre = 0;
                                        objDocumento.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                        objDocumento.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                        objDocumento.idTipoTransaccion = (decimal)EnumTipoTransaccion.DocCalidad;
                                        objDocumento.fechaEmision = Convert.ToInt64(strFecha);
                                        objDocumento.numeroTransaccion = arreglo[i];
                                        objDocumento.numeroAutorizacion = 0;
                                        objDocumento.codigoControl = "";
                                        objDocumento.nitConsumidor = 0;
                                        objDocumento.razonSocial = "";
                                        objDocumento.precioUnitario = 0;
                                        objDocumento.precioTotal = 0;
                                        objDocumento.usuarioTransaccion = values[21].Trim();
                                        objDocumento.observaciones = "";
                                        objDocumento.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                        objDocumento.idEstadoTransaccion = estadoTra;
                                        objDocumento.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.No;
                                        objDocumento.idFormaPago = (int)EnumTiposPago.Efectivo;
                                        objDocumento.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                        objDocumento.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                        objDocumento.codigoSeguridad = 0;
                                        objDocumento.idAutorizacionDato = 0;
                                        objDocumento.binarioControl = "0000000000";
                                        objDocumento.fechaOperacion = Convert.ToInt64(strFecha);
                                        objDocumento.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                        objDocumento.codigo = "";
                                        lstTransacciones.Add(objDocumento);
                                    }
                                }

                                objNuevoTipoReg = new ObjProtoMovimientoTransaccion();
                                objNuevoTipoReg.idMovimientoPadre = 0;
                                objNuevoTipoReg.idTipoActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                objNuevoTipoReg.idTipoOperacion = Convert.ToDecimal(cmbTipoOperacion.Value);
                                objNuevoTipoReg.idConsumidor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString());
                                objNuevoTipoReg.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                objNuevoTipoReg.fechaRegistroMovimiento = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                objNuevoTipoReg.fechaOperacion = Convert.ToInt64(strFecha);
                                objNuevoTipoReg.idTipoMovimientoFlujo = (int)EnumTipoMovimientoFlujo.Fisico;
                                objNuevoTipoReg.idTipoMovimiento = (int)EnumTipoMovimiento.NacNac;
                                objNuevoTipoReg.idEstadoMovimiento = estadoTra;
                                objNuevoTipoReg.idFuenteOrigenDato = (decimal)EnumTipoRegistro.OrigenCsv;
                                objNuevoTipoReg.idUbicacionOperacion = MCodificaParametrica((int)EnumTipoParametrica.Pais, "BOLIVIA");
                                objNuevoTipoReg.idUnidadMedidaHidrocarburo = MCodificaParametrica((int)EnumTipoParametrica.UnidadMedida, values[23].Trim());
                                objNuevoTipoReg.idHidrocarburo = MCodificaParametrica((int)EnumTipoParametrica.Producto, values[23].Trim());
                                objNuevoTipoReg.cantidadHidrocarburo = cDato.DecVolumen;
                                objNuevoTipoReg.identificador = cDato.StrIdentificador;
                                objNuevoTipoReg.subIdentificador = "";
                                objNuevoTipoReg.observacion = "";
                                objNuevoTipoReg.tipoInfraestructura = MCodificaParametrica((int)EnumTipoParametrica.TipoInfraestructura, values[2].Trim());
                                objNuevoTipoReg.idInfraestructura = MCodificaParametrica((int)EnumTipoParametrica.Infraestructura, values[2].Trim() + "|" + values[3].Trim());
                                objNuevoTipoReg.idTipoMedicion = -99999;
                                objNuevoTipoReg.cantidadAguaLitros = 0;
                                objNuevoTipoReg.idUnidadTemperatura = (decimal)EnumUnidadTemperatura.GradoCentigrado;
                                objNuevoTipoReg.temperatura = Convert.ToDecimal(values[7]);
                                objNuevoTipoReg.gradoApi = values[8].Trim();
                                objNuevoTipoReg.idEntidad = MCodificaParametrica((int)EnumTipoParametrica.Entidad, values[4].Trim());
                                objNuevoTipoReg.idTipoMedioTransporte = MCodificaParametrica((int)EnumTipoParametrica.TipoInfraestructura, values[5].Trim());
                                objNuevoTipoReg.idMedioTransporte = MCodificaParametrica((int)EnumTipoParametrica.Infraestructura, values[5].Trim() + "|" + values[6].Trim());
                                objNuevoTipoReg.idUnidadEnvasesTransporte = -99999;
                                objNuevoTipoReg.cantidadEnvasesTransporte = 0;
                                objNuevoTipoReg.binarioControl = "00000-0000000000-0000000000-00000";
                                objNuevoTipoReg.idIntegracion = Convert.ToDecimal(values[1]);
                                objNuevoTipoReg.idTransaccionBaseExterna = "";
                                objNuevoTipoReg.codigo = values[11].Trim();
                                decimal dec;
                                objNuevoTipoReg.barcode = (decimal.TryParse(values[10].Trim(), out dec)) ? Convert.ToDecimal(values[10].Trim()) : 0;
                                objNuevoTipoReg.listaTransaccion = JsonSerializer.SerializeToString(lstTransacciones);

                                objNuevoTipoReg.jsonReferencia = "";
                                objNuevoTipoReg.estadoJsonRef = -99999;
                                objNuevoTipoReg.idCuenta = -99999;
                                objNuevoTipoReg.idCuentaAux = -99999;
                                objNuevoTipoReg.idParamGlobal = -99999;
                                objNuevoTipoReg.idParamEspec = -99999;
                                objNuevoTipoReg.idTipoConsumidor = decIdTipoDespacho;
                                objNuevoTipoReg.idTipoInfraAux = -99999;
                                objNuevoTipoReg.idInfraAux = -99999;

                                #endregion
                                break;
                            case "MD":
                                #region Valida y genera objeto
                                cDato.LngId = lngNoRegCorrect;
                                cDato.StrIdVenta = values[1].Trim();
                                datFecha = DateTime.Parse(values[0].Trim());
                                cDato.StrFechaEmision = datFecha.ToString();
                                cDato.StrTipoMov = "NAC";
                                cDato.StrEstado = "V";
                                cDato.LngNit = 0;
                                cDato.StrRazonSocial = values[11].Trim().ToUpper().Replace("¥", "Ñ").Replace("�", "Ñ");
                                cDato.StrRazonSocial = Regex.Replace(cDato.StrRazonSocial, @"\s+", " ");
                                cDato.StrIdentificador = "";
                                cDato.LngNroFactura = 0;
                                cDato.LngNroAutorizacion = 0;
                                cDato.StrNroReferencia = values[1].Trim();
                                cDato.LngNroAutVenta = 0;
                                cDato.StrProducto = values[4].Trim();
                                cDato.DecVolumen = Convert.ToDecimal(values[9]);
                                cDato.DecPrecio = 0;
                                cDato.DecImporte = 0;
                                cDato.DecIdTipoCreditoFiscal = (int)EnumTipoCredFiscal.No;
                                cDato.DecIdTipoOp = Convert.ToDecimal(cmbTipoOperacion.Value);
                                cDato.DecOrden = 1;

                                strFecha = CFechas.ConvierteDateTimeLong(datFecha).ToString();
                                datFechaReg = DateTime.Now;
                                estadoTra = (int)EnumEstadoMov.Activo;
                                lstTransacciones = new List<ObjTransaccion>();

                                var objMedicion = new O_PAHL_MEDIC_DA
                                {
                                    FACTOR_CORRECCION = Convert.ToDecimal(values[12].Trim()),
                                    NIVEL_PROD = Convert.ToDecimal(values[13].Trim()),
                                    NIVEL_AGUA = Convert.ToDecimal(values[14].Trim()),
                                    TEMPERATURA_TK = Convert.ToDecimal(values[15].Trim()),
                                    API_OBSERVADO = values[16].Trim(),
                                    VOL_NETO = Convert.ToDecimal(values[17].Trim()),
                                    CARGA_MUERTA = Convert.ToDecimal(values[18].Trim()),
                                    ESPACIO_DISP_TK = Convert.ToDecimal(values[19].Trim())
                                };

                                objDocumento = new ObjTransaccion();
                                objDocumento.idMovimiento = 0;
                                objDocumento.idTransaccion = 0;
                                objDocumento.idTransaccionPadre = 0;
                                objDocumento.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                objDocumento.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                objDocumento.idTipoTransaccion = (decimal)EnumTipoTransaccion.DocMedicion;
                                objDocumento.fechaEmision = Convert.ToInt64(strFecha);
                                objDocumento.numeroTransaccion = values[1].Trim();
                                objDocumento.numeroAutorizacion = 0;
                                objDocumento.codigoControl = "";
                                objDocumento.nitConsumidor = Convert.ToDecimal(values[10].Trim());
                                objDocumento.razonSocial = values[11].Trim();
                                objDocumento.precioUnitario = 0;
                                objDocumento.precioTotal = 0;
                                objDocumento.usuarioTransaccion = values[10].Trim();
                                objDocumento.observaciones = "";
                                objDocumento.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                objDocumento.idEstadoTransaccion = estadoTra;
                                objDocumento.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.No;
                                objDocumento.idFormaPago = (decimal)EnumTiposPago.Efectivo;
                                objDocumento.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                objDocumento.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                objDocumento.codigoSeguridad = 0;
                                objDocumento.idAutorizacionDato = 0;
                                objDocumento.binarioControl = "1000000000";
                                objDocumento.fechaOperacion = Convert.ToInt64(strFecha);
                                objDocumento.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                objDocumento.codigo = "";

                                lstTransacciones.Add(objDocumento);

                                objNuevoTipoReg = new ObjProtoMovimientoTransaccion();
                                objNuevoTipoReg.idMovimientoPadre = 0;
                                objNuevoTipoReg.idTipoActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                objNuevoTipoReg.idTipoOperacion = Convert.ToDecimal(cmbTipoOperacion.Value);
                                objNuevoTipoReg.idConsumidor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString());
                                objNuevoTipoReg.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                objNuevoTipoReg.fechaRegistroMovimiento = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                objNuevoTipoReg.fechaOperacion = Convert.ToInt64(strFecha);
                                objNuevoTipoReg.idTipoMovimientoFlujo = (int)EnumTipoMovimientoFlujo.Fisico;
                                objNuevoTipoReg.idTipoMovimiento = (int)EnumTipoMovimiento.NacNac;
                                objNuevoTipoReg.idEstadoMovimiento = estadoTra;
                                objNuevoTipoReg.idFuenteOrigenDato = (decimal)EnumTipoRegistro.OrigenCsv;
                                objNuevoTipoReg.idUbicacionOperacion = MCodificaParametrica((int)EnumTipoParametrica.Pais, "BOLIVIA");
                                objNuevoTipoReg.idUnidadMedidaHidrocarburo = MCodificaParametrica((int)EnumTipoParametrica.UnidadMedida, values[4].Trim());
                                objNuevoTipoReg.idHidrocarburo = MCodificaParametrica((int)EnumTipoParametrica.Producto, values[4].Trim());
                                objNuevoTipoReg.cantidadHidrocarburo = cDato.DecVolumen;
                                objNuevoTipoReg.identificador = cDato.StrIdentificador;
                                objNuevoTipoReg.subIdentificador = "";
                                objNuevoTipoReg.observacion = "";
                                objNuevoTipoReg.tipoInfraestructura = MCodificaParametrica((int)EnumTipoParametrica.TipoInfraestructura, values[2].Trim());
                                objNuevoTipoReg.idInfraestructura = MCodificaParametrica((int)EnumTipoParametrica.Infraestructura, values[2].Trim() + "|" + values[3].Trim());
                                objNuevoTipoReg.idTipoMedicion = (values[7].Trim() == "MAN") ? (decimal)EnumTipoMedicion.Manual : (values[7].Trim() == "TEL") ? (decimal)EnumTipoMedicion.Telemedicion : (decimal)EnumTipoMedicion.CalculoMatematico;
                                objNuevoTipoReg.cantidadAguaLitros = Convert.ToDecimal(values[8]);
                                objNuevoTipoReg.idUnidadTemperatura = (decimal)EnumUnidadTemperatura.GradoFarenheit;
                                objNuevoTipoReg.temperatura = Convert.ToDecimal(values[5]);
                                objNuevoTipoReg.gradoApi = values[6].Trim();
                                objNuevoTipoReg.idEntidad = -99999;
                                objNuevoTipoReg.idTipoMedioTransporte = -99999;
                                objNuevoTipoReg.idMedioTransporte = -99999;
                                objNuevoTipoReg.idUnidadEnvasesTransporte = -99999;
                                objNuevoTipoReg.cantidadEnvasesTransporte = 0;
                                objNuevoTipoReg.binarioControl = "00000-0000000000-0000000000-00000";
                                objNuevoTipoReg.idIntegracion = Convert.ToDecimal(values[1]);
                                objNuevoTipoReg.idTransaccionBaseExterna = "";
                                objNuevoTipoReg.codigo = "";
                                objNuevoTipoReg.barcode = 0;

                                objNuevoTipoReg.jsonReferencia = JsonSerializer.SerializeToString(objMedicion);
                                objNuevoTipoReg.estadoJsonRef = 1;
                                objNuevoTipoReg.idCuenta = -99999;
                                objNuevoTipoReg.idCuentaAux = -99999;
                                objNuevoTipoReg.idParamGlobal = -99999;
                                objNuevoTipoReg.idParamEspec = -99999;
                                objNuevoTipoReg.idTipoConsumidor = -99999;
                                objNuevoTipoReg.idTipoInfraAux = -99999;
                                objNuevoTipoReg.idInfraAux = -99999;

                                objNuevoTipoReg.listaTransaccion = JsonSerializer.SerializeToString(lstTransacciones);

                                #endregion
                                break;
                        }
                        cDato.objProtoMovimiento = objNuevoTipoReg;
                        lstRegistros.Add(cDato);

                        //REGISTROS POR DIFERENCIA
                        if (objNuevoAjuste != null)
                        {
                            cDatoAj.objProtoMovimiento = objNuevoAjuste;
                            lstRegistros.Add(cDatoAj);
                        }

                        lngNoRegCorrect++;
                    }
                    else
                    {
                        var cRegError = new CRegistrosError();
                        cRegError.LngId = lngNoRegInCorrect;
                        cRegError.StrIdVenta = values[1].Trim();
                        cRegError.LngFechaEmision = values[0].Trim();
                        cRegError.LngNit = "";
                        cRegError.LngNroAutorizacion = values[15].Trim();
                        cRegError.LngNroFactura = values[14].Trim();
                        cRegError.LngNroReferencia = values[19].Trim();
                        cRegError.StrErrores = strMensaje.Trim();
                        lstRegistroE.Add(cRegError);
                        lngNoRegInCorrect++;
                    }
                }
                catch (Exception ex)
                {
                    var cRegError = new CRegistrosError();
                    cRegError.LngId = lngNoRegInCorrect;
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
            }

            //var ojo2 = JsonSerializer.SerializeToString(lstRegistros);
            dgvTransacciones.DataSource = lstRegistros;
            dgvTransacciones.DataBind();
            Session[CVariablesSesion.GrillaTransaccion] = lstRegistros;

            dgvTransaccionError.DataSource = lstRegistroE;
            dgvTransaccionError.DataBind();
            Session[CVariablesSesion.GrillaRegObservado] = lstRegistroE;
        }

        /// <summary>
        /// METODO. MValidaLineaFala
        /// </summary>
        /// <param name="values"></param>
        /// <param name="strMensaje"></param>
        /// <returns></returns>
        private bool MValidaLineaFala(string[] values, ref string strMensaje)
        {
            var booResultado = true;
            try
            {
                var to = cmbTipoOperacion.Text.Split(';');
                var strTipoOp = to[1].Substring(2, 2);
                int[] arrayNumero;
                decimal dec;
                DateTime dt;
                switch (strTipoOp)
                {
                    case "RC":
                        #region Recepciones
                        if (values.Length != 6)
                        {
                            strMensaje = "- Nro. de columnas del registro invalido.";
                        }
                        else
                        {
                            //todo No implementado.
                        }
                        arrayNumero = new[] { 3 };
                        for (int i = 0; i < arrayNumero.Length; i++)
                        {
                            if (!decimal.TryParse(values[arrayNumero[i]].Trim(), out dec))
                            {
                                strMensaje += "- Columna Nro. " + arrayNumero[i] + " No es un número.";
                            }
                        }
                        if (values[0].Trim().Substring(6, 4) + values[0].Trim().Substring(3, 2) != cmbPeriodo.Value.ToString().Substring(0, 4) + cmbPeriodo.Value.ToString().Substring(4, 2))
                        {
                            strMensaje += "- Periodo incorrecto.";
                        }
                        else
                        {
                            //todo No implementado.
                        }
                        if (!DateTime.TryParse(values[0].Trim(), out dt))
                        {
                            strMensaje += "- Fecha incorrecta.";
                        }
                        else
                        {
                            //todo No implementado.
                        }
                        if (Convert.ToDecimal(values[3]) <= 0)
                        {
                            strMensaje += "- Cantidad incorrecta.";
                        }
                        else
                        {
                            //todo No implementado.
                        }
                        if (MCodificaParametrica((int)EnumTipoParametrica.Producto, values[1].Trim()) == -99999)
                        {
                            strMensaje += "- Producto incorrecto.";
                        }
                        else
                        {
                            //todo No implementado.
                        }
                        if (MCodificaParametrica((int)EnumTipoParametrica.UnidadMedida, values[1].Trim()) == -99999)
                        {
                            strMensaje += "- Unidad de medida invalida.";
                        }
                        else
                        {
                            //todo No implementado.
                        }
                        if (values[5].Trim().Length == 0 || values[5].Trim() == "0")
                        {
                            strMensaje += "- Número de referencia invalido.";
                        }
                        else
                        {
                            //todo No implementado.
                        }
                        #endregion
                        break;
                    default:
                        #region Despachos
                        if (values.Length != 19)
                        {
                            strMensaje = "- Nro. de columnas del registro invalido.";
                        }
                        else
                        {
                            //todo No implementado.
                        }
                        arrayNumero = new[] { 6, 10, 12, 14, 15, 17, 18 };
                        for (int i = 0; i < arrayNumero.Length; i++)
                        {
                            if (!decimal.TryParse(values[arrayNumero[i]].Trim(), out dec))
                            {
                                strMensaje += "- Columna Nro. " + arrayNumero[i] + " No es un número.";
                            }
                        }
                        if (values[0].Trim().Substring(6, 4) + values[0].Trim().Substring(3, 2) != cmbPeriodo.Value.ToString().Substring(0, 4) + cmbPeriodo.Value.ToString().Substring(4, 2))
                        {
                            strMensaje += "- Periodo incorrecto.";
                        }
                        else
                        {
                            //todo No implementado.
                        }
                        if (!DateTime.TryParse(values[0].Trim(), out dt))
                        {
                            strMensaje += "- Fecha incorrecta.";
                        }
                        else
                        {
                            //todo No implementado.
                        }
                        if (Convert.ToDecimal(values[10]) <= 0)
                        {
                            strMensaje += "- Cantidad incorrecta.";
                        }
                        else
                        {
                            //todo No implementado.
                        }

                        if (Convert.ToDecimal(values[18].Trim()) > (Convert.ToDecimal(values[10].Trim()) * Convert.ToDecimal(values[17].Trim())) + 2 ||
                            Convert.ToDecimal(values[18].Trim()) < (Convert.ToDecimal(values[10].Trim()) * Convert.ToDecimal(values[17].Trim())) - 2)
                        {
                            strMensaje += "- Importe incorrecto.";
                        }
                        else
                        {
                            //todo No implementado.
                        }
                        if (values[16].Trim().Length == 1 && values[16].Trim() != "0")
                        {
                            strMensaje += "- Código de control incorrecto.";
                        }
                        else
                        {
                            //todo No implementado.
                        }
                        if (values[16].Trim().Length > 1 && values[16].Trim().Length < 6)
                        {
                            strMensaje += "- Código de control incorrecto.";
                        }
                        else
                        {
                            //todo No implementado.
                        }
                        if (values[2].Trim().Length > 1)
                        {
                            if (values[2].Trim().ContainsAny("-", "@", "/", "'", "\\", ".", ",", "_", "+", "[", "]", "{", "}", " "))
                            {
                                strMensaje += "- Identificador contiene caracteres invalidos.";
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
                        if (MCodificaParametrica((int)EnumTipoParametrica.Producto, values[9].Trim()) == -99999)
                        {
                            strMensaje += "- Producto incorrecto.";
                        }
                        else
                        {
                            //todo No implementado.
                        }

                        if (values[1].Trim() != "NOID")
                        {
                            if (MCodificaParametrica((int)EnumTipoParametrica.Entidad, values[1].Trim()) == -99999)
                            {
                                strMensaje += "- Entidad origen invalida.";
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
                        if (values[11].Trim().Length == 0 || values[9].Trim() == "0")
                        {
                            strMensaje += "- Número de referencia invalido.";
                        }
                        else
                        {
                            //todo No implementado.
                        }
                        if (Convert.ToDecimal(values[14].Trim()) <= 0)
                        {
                            strMensaje += "- Número de factura invalido.";
                        }
                        else
                        {
                            //todo No implementado.
                        }
                        if (values[4].Trim() != "NOID")
                        {
                            if (values[4].Trim() != "CS")
                            {
                                if (MCodificaParametrica((int)EnumTipoParametrica.TipoContenedor, values[4].Trim()) == -99999)
                                {
                                    strMensaje += "- Medio de transponte invalido.";
                                }
                                else
                                {
                                    //todo No implementado.
                                }
                            }

                        }
                        else
                        {
                            //todo No implementado.
                        }
                        #endregion
                        break;
                }
            }
            catch (Exception ex)
            {
                strMensaje += ex.Message;
            }

            if (strMensaje.Trim().Length > 0)
            {
                booResultado = false;
            }
            else
            {
                //todo No implementado.
            }

            return booResultado;
        }

        /// <summary>
        /// METODO. MCargaTerminalAeropuerto
        /// </summary>
        /// <param name="strRuta"></param>
        private void MCargaFueraAla(string strRuta)
        {
            var reader = new StreamReader(File.OpenRead(@strRuta));
            var lstRegistros = new List<CRegistrosImportados>();
            var lstRegistroE = new List<CRegistrosError>();
            long lngNoRegCorrect = 1, lngNoRegInCorrect = 1;
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(';');
                try
                {
                    DateTime datFecha, datFechaReg;
                    string strFecha;
                    ObjTransaccion objFactura, objDocumento;
                    ObjProtoMovimientoTransaccion objNuevoTipoReg = null;
                    int estadoTra;
                    List<ObjTransaccion> lstTransacciones;
                    var to = cmbTipoOperacion.Text.Split(';');
                    var strTipoOp = to[1].Substring(2, 2);

                    string strMensaje = string.Empty;
                    if (MValidaLineaFala(values, ref strMensaje))
                    {
                        var cDato = new CRegistrosImportados();
                        int controlBinario;
                        switch (strTipoOp)
                        {
                            case "RC":
                                #region Valida y genera objeto
                                cDato.LngId = lngNoRegCorrect;
                                cDato.StrIdVenta = values[5].Trim();
                                datFecha = DateTime.Parse(values[0].Trim());
                                cDato.StrFechaEmision = datFecha.ToString();
                                cDato.StrTipoMov = "NAC";
                                cDato.StrEstado = "V";
                                cDato.LngNit = 0;
                                cDato.StrRazonSocial = values[2].Trim();
                                cDato.StrIdentificador = "";
                                cDato.LngNroFactura = 0;
                                cDato.LngNroAutorizacion = 0;
                                cDato.StrNroReferencia = values[5].Trim();
                                cDato.LngNroAutVenta = 0;
                                cDato.StrProducto = values[1].Trim();
                                cDato.DecVolumen = Convert.ToDecimal(values[3]);
                                cDato.DecPrecio = 0;
                                cDato.DecImporte = 0;
                                cDato.DecIdTipoCreditoFiscal = (int)EnumTipoCredFiscal.No;
                                cDato.DecIdTipoOp = Convert.ToDecimal(cmbTipoOperacion.Value);
                                cDato.DecOrden = 1;

                                strFecha = datFecha.Year + datFecha.Month.ToString().PadLeft(2, '0') + datFecha.Day.ToString().PadLeft(2, '0') + datFecha.Hour.ToString().PadLeft(2, '0') + datFecha.Minute.ToString().PadLeft(2, '0') + datFecha.Second.ToString().PadLeft(2, '0');
                                datFechaReg = DateTime.Now;
                                estadoTra = (int)EnumEstadoMov.Activo;
                                lstTransacciones = new List<ObjTransaccion>();

                                controlBinario = 1;

                                objDocumento = new ObjTransaccion();
                                objDocumento.idMovimiento = 0;
                                objDocumento.idTransaccion = 0;
                                objDocumento.idTransaccionPadre = 0;
                                objDocumento.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                objDocumento.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                objDocumento.idTipoTransaccion = (decimal)EnumTipoTransaccion.AutAnhVfa;
                                objDocumento.fechaEmision = Convert.ToInt64(strFecha);
                                objDocumento.numeroTransaccion = values[5].Trim();
                                objDocumento.numeroAutorizacion = 0;
                                objDocumento.codigoControl = "";
                                objDocumento.nitConsumidor = 0;
                                objDocumento.razonSocial = "";
                                objDocumento.precioUnitario = 0;
                                objDocumento.precioTotal = 0;
                                objDocumento.usuarioTransaccion = values[2].Trim();
                                objDocumento.observaciones = "";
                                objDocumento.nitEmisor = 0;
                                objDocumento.idEstadoTransaccion = estadoTra;
                                objDocumento.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.No;
                                objDocumento.idFormaPago = (decimal)EnumTiposPago.Efectivo;
                                objDocumento.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                objDocumento.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                objDocumento.codigoSeguridad = 0;
                                objDocumento.idAutorizacionDato = 0;
                                objDocumento.binarioControl = controlBinario + "000000000";
                                lstTransacciones.Add(objDocumento);

                                objNuevoTipoReg = new ObjProtoMovimientoTransaccion();
                                objNuevoTipoReg.idMovimientoPadre = 0;
                                objNuevoTipoReg.idTipoActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                objNuevoTipoReg.idTipoOperacion = Convert.ToDecimal(cmbTipoOperacion.Value);
                                objNuevoTipoReg.idConsumidor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString());
                                objNuevoTipoReg.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                objNuevoTipoReg.fechaRegistroMovimiento = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                objNuevoTipoReg.fechaOperacion = Convert.ToInt64(strFecha);
                                objNuevoTipoReg.idTipoMovimientoFlujo = (int)EnumTipoMovimientoFlujo.Fisico;
                                objNuevoTipoReg.idTipoMovimiento = (int)EnumTipoMovimiento.NacNac;
                                objNuevoTipoReg.idEstadoMovimiento = estadoTra;
                                objNuevoTipoReg.idFuenteOrigenDato = (decimal)EnumTipoRegistro.OrigenCsv;
                                objNuevoTipoReg.idUbicacionOperacion = MCodificaParametrica((int)EnumTipoParametrica.Pais, "BOLIVIA");
                                objNuevoTipoReg.idUnidadMedidaHidrocarburo = MCodificaParametrica((int)EnumTipoParametrica.UnidadMedida, values[1].Trim());
                                objNuevoTipoReg.idHidrocarburo = MCodificaParametrica((int)EnumTipoParametrica.Producto, values[1].Trim());
                                objNuevoTipoReg.cantidadHidrocarburo = cDato.DecVolumen;
                                objNuevoTipoReg.identificador = cDato.StrIdentificador;
                                objNuevoTipoReg.subIdentificador = "";
                                objNuevoTipoReg.observacion = values[4].Trim();
                                objNuevoTipoReg.tipoInfraestructura = -99999;
                                objNuevoTipoReg.idInfraestructura = -99999;
                                objNuevoTipoReg.idTipoMedicion = -99999;
                                objNuevoTipoReg.cantidadAguaLitros = 0;
                                objNuevoTipoReg.idUnidadTemperatura = (decimal)EnumUnidadTemperatura.GradoCentigrado;
                                objNuevoTipoReg.temperatura = 0;
                                objNuevoTipoReg.gradoApi = "";
                                objNuevoTipoReg.idEntidad = -99999;
                                objNuevoTipoReg.idTipoMedioTransporte = -99999;
                                objNuevoTipoReg.idMedioTransporte = -99999;
                                objNuevoTipoReg.idUnidadEnvasesTransporte = -99999;
                                objNuevoTipoReg.cantidadEnvasesTransporte = 0;
                                objNuevoTipoReg.binarioControl = "00000-0000000000-0000000000-00000";
                                objNuevoTipoReg.idIntegracion = 0;
                                objNuevoTipoReg.idTransaccionBaseExterna = "";
                                objNuevoTipoReg.codigo = "";
                                objNuevoTipoReg.barcode = 0;
                                objNuevoTipoReg.listaTransaccion = JsonSerializer.SerializeToString(lstTransacciones);
                                #endregion
                                break;
                            case "DS":
                                #region Valida y genera objeto
                                cDato.LngId = lngNoRegCorrect;
                                cDato.StrIdVenta = values[11].Trim();
                                datFecha = DateTime.Parse(values[0].Trim());
                                cDato.StrFechaEmision = datFecha.ToString();
                                cDato.StrTipoMov = "NAC";
                                cDato.StrEstado = "V";
                                cDato.LngNit = Convert.ToInt64(values[12]);
                                cDato.StrRazonSocial = values[13].Trim().ToUpper().Replace("¥", "Ñ").Replace("Á", "A").Replace("É", "E").Replace("Í", "I").Replace("Ó", "O").Replace("Ú", "U").Replace("'", "").Replace("�", "Ñ");
                                cDato.StrRazonSocial = Regex.Replace(cDato.StrRazonSocial, @"\s+", " ");
                                cDato.StrIdentificador = values[2].Trim();
                                cDato.LngNroFactura = Convert.ToInt64(values[14]);
                                cDato.LngNroAutorizacion = Convert.ToInt64(values[15]);
                                cDato.StrNroReferencia = values[11].Trim();
                                cDato.LngNroAutVenta = 0;
                                cDato.StrProducto = values[9].Trim();
                                cDato.DecVolumen = Convert.ToDecimal(values[10]);
                                cDato.DecPrecio = Convert.ToDecimal(values[17]);
                                cDato.DecImporte = Convert.ToDecimal(values[18]);
                                cDato.DecIdTipoCreditoFiscal = (cDato.LngNroFactura > 0) ? (int)EnumTipoCredFiscal.Si : (int)EnumTipoCredFiscal.No;
                                cDato.DecIdTipoOp = Convert.ToDecimal(cmbTipoOperacion.Value);
                                cDato.DecOrden = 1;

                                strFecha = datFecha.Year + datFecha.Month.ToString().PadLeft(2, '0') + datFecha.Day.ToString().PadLeft(2, '0') + datFecha.Hour.ToString().PadLeft(2, '0') + datFecha.Minute.ToString().PadLeft(2, '0') + datFecha.Second.ToString().PadLeft(2, '0');
                                datFechaReg = DateTime.Now;
                                estadoTra = (int)EnumEstadoMov.Activo;
                                lstTransacciones = new List<ObjTransaccion>();

                                controlBinario = 1;
                                if (Convert.ToDecimal(values[12].Trim()) > 0)
                                {
                                    objFactura = new ObjTransaccion();
                                    objFactura.idMovimiento = 0;
                                    objFactura.idTransaccion = 0;
                                    objFactura.idTransaccionPadre = 0;
                                    objFactura.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                    objFactura.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                    objFactura.idTipoTransaccion = (decimal)((values[16].Trim().Length > 4) ? EnumTipoTransaccion.FacturaComputarizada : EnumTipoTransaccion.FacturaManual);
                                    objFactura.fechaEmision = Convert.ToInt64(strFecha);
                                    objFactura.numeroTransaccion = values[14].Trim();
                                    objFactura.numeroAutorizacion = Convert.ToDecimal(values[15].Trim());
                                    objFactura.codigoControl = values[16].Trim();
                                    objFactura.nitConsumidor = Convert.ToDecimal(values[12].Trim());
                                    objFactura.razonSocial = cDato.StrRazonSocial;
                                    objFactura.precioUnitario = Convert.ToDecimal(values[17].Trim());
                                    objFactura.precioTotal = Convert.ToDecimal(values[18].Trim());
                                    objFactura.usuarioTransaccion = values[6].Trim();
                                    objFactura.observaciones = "";
                                    objFactura.nitEmisor = 0;
                                    objFactura.idEstadoTransaccion = estadoTra;
                                    objFactura.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.Si;
                                    objFactura.idFormaPago = (int)EnumTiposPago.Efectivo;
                                    objFactura.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                    objFactura.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                    objFactura.codigoSeguridad = 0;
                                    objFactura.idAutorizacionDato = 0;
                                    objFactura.binarioControl = controlBinario + "000000000";
                                    lstTransacciones.Add(objFactura);
                                    controlBinario = 0;
                                }

                                objDocumento = new ObjTransaccion();
                                objDocumento.idMovimiento = 0;
                                objDocumento.idTransaccion = 0;
                                objDocumento.idTransaccionPadre = 0;
                                objDocumento.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                objDocumento.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                objDocumento.idTipoTransaccion = (decimal)EnumTipoTransaccion.AutAnhVfa;
                                objDocumento.fechaEmision = Convert.ToInt64(strFecha);
                                objDocumento.numeroTransaccion = values[11].Trim();
                                objDocumento.numeroAutorizacion = 0;
                                objDocumento.codigoControl = values[7].Trim();
                                objDocumento.nitConsumidor = Convert.ToDecimal(values[6].Trim());
                                objDocumento.razonSocial = values[8].Trim();
                                objDocumento.precioUnitario = Convert.ToDecimal(values[17].Trim());
                                objDocumento.precioTotal = Convert.ToDecimal(values[18].Trim());
                                objDocumento.usuarioTransaccion = values[6].Trim();
                                objDocumento.observaciones = "";
                                objDocumento.nitEmisor = 0;
                                objDocumento.idEstadoTransaccion = estadoTra;
                                objDocumento.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.No;
                                objDocumento.idFormaPago = (decimal)EnumTiposPago.Efectivo;
                                objDocumento.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                objDocumento.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                objDocumento.codigoSeguridad = 0;
                                objDocumento.idAutorizacionDato = 0;
                                objDocumento.binarioControl = controlBinario + "000000000";
                                lstTransacciones.Add(objDocumento);

                                objNuevoTipoReg = new ObjProtoMovimientoTransaccion();
                                objNuevoTipoReg.idMovimientoPadre = 0;
                                objNuevoTipoReg.idTipoActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                objNuevoTipoReg.idTipoOperacion = Convert.ToDecimal(cmbTipoOperacion.Value);
                                objNuevoTipoReg.idConsumidor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString());
                                objNuevoTipoReg.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                objNuevoTipoReg.fechaRegistroMovimiento = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                objNuevoTipoReg.fechaOperacion = Convert.ToInt64(strFecha);
                                objNuevoTipoReg.idTipoMovimientoFlujo = (int)EnumTipoMovimientoFlujo.Fisico;
                                objNuevoTipoReg.idTipoMovimiento = (int)EnumTipoMovimiento.NacNac;
                                objNuevoTipoReg.idEstadoMovimiento = estadoTra;
                                objNuevoTipoReg.idFuenteOrigenDato = (decimal)EnumTipoRegistro.OrigenCsv;
                                objNuevoTipoReg.idUbicacionOperacion = MCodificaParametrica((int)EnumTipoParametrica.Pais, "BOLIVIA");
                                objNuevoTipoReg.idUnidadMedidaHidrocarburo = MCodificaParametrica((int)EnumTipoParametrica.UnidadMedida, values[9].Trim());
                                objNuevoTipoReg.idHidrocarburo = MCodificaParametrica((int)EnumTipoParametrica.Producto, values[9].Trim());
                                objNuevoTipoReg.cantidadHidrocarburo = cDato.DecVolumen;
                                objNuevoTipoReg.identificador = cDato.StrIdentificador;
                                objNuevoTipoReg.subIdentificador = "";
                                objNuevoTipoReg.observacion = "";
                                objNuevoTipoReg.tipoInfraestructura = -99999;
                                objNuevoTipoReg.idInfraestructura = -99999;
                                objNuevoTipoReg.idTipoMedicion = -99999;
                                objNuevoTipoReg.cantidadAguaLitros = 0;
                                objNuevoTipoReg.idUnidadTemperatura = (decimal)EnumUnidadTemperatura.GradoCentigrado;
                                objNuevoTipoReg.temperatura = 0;
                                objNuevoTipoReg.gradoApi = "";
                                objNuevoTipoReg.idEntidad = MCodificaParametrica((int)EnumTipoParametrica.Entidad, values[1].Trim());
                                objNuevoTipoReg.idTipoMedioTransporte = (values[4].Trim() == "CS") ? MCodificaParametrica((int)EnumTipoParametrica.TipoInfraestructura, values[4].Trim()) : -99999;
                                objNuevoTipoReg.idMedioTransporte = -99999;
                                objNuevoTipoReg.idUnidadEnvasesTransporte = (values[4].Trim() != "CS") ? MCodificaParametrica((int)EnumTipoParametrica.TipoContenedor, values[4].Trim()) : -99999;
                                objNuevoTipoReg.cantidadEnvasesTransporte = 0;
                                objNuevoTipoReg.binarioControl = "00000-0000000000-0000000000-00000";
                                objNuevoTipoReg.idIntegracion = 0;
                                objNuevoTipoReg.idTransaccionBaseExterna = "";
                                objNuevoTipoReg.codigo = values[5].Trim();
                                objNuevoTipoReg.barcode = 0;
                                objNuevoTipoReg.listaTransaccion = JsonSerializer.SerializeToString(lstTransacciones);
                                #endregion
                                break;
                        }

                        cDato.objProtoMovimiento = objNuevoTipoReg;
                        lstRegistros.Add(cDato);
                        lngNoRegCorrect++;
                    }
                    else
                    {
                        if (strTipoOp == "RC")
                        {
                            var cRegError = new CRegistrosError();
                            cRegError.LngId = lngNoRegInCorrect;
                            cRegError.StrIdVenta = values[5].Trim();
                            cRegError.LngFechaEmision = values[0].Trim();
                            cRegError.LngNit = "";
                            cRegError.LngNroAutorizacion = "0";
                            cRegError.LngNroFactura = "0";
                            cRegError.LngNroReferencia = values[5].Trim();
                            cRegError.StrErrores = strMensaje.Trim();
                            lstRegistroE.Add(cRegError);
                            lngNoRegInCorrect++;
                        }
                        else
                        {
                            var cRegError = new CRegistrosError();
                            cRegError.LngId = lngNoRegInCorrect;
                            cRegError.StrIdVenta = values[11].Trim();
                            cRegError.LngFechaEmision = values[0].Trim();
                            cRegError.LngNit = values[12].Trim();
                            cRegError.LngNroAutorizacion = values[15].Trim();
                            cRegError.LngNroFactura = values[14].Trim();
                            cRegError.LngNroReferencia = values[11].Trim();
                            cRegError.StrErrores = strMensaje.Trim();
                            lstRegistroE.Add(cRegError);
                            lngNoRegInCorrect++;
                        }
                    }
                }
                catch (Exception ex)
                {
                    var cRegError = new CRegistrosError();
                    cRegError.LngId = lngNoRegInCorrect;
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
            }

            //var ojo2 = JsonSerializer.SerializeToString(lstRegistros);
            dgvTransacciones.DataSource = lstRegistros;
            dgvTransacciones.DataBind();
            Session[CVariablesSesion.GrillaTransaccion] = lstRegistros;

            dgvTransaccionError.DataSource = lstRegistroE;
            dgvTransaccionError.DataBind();
            Session[CVariablesSesion.GrillaRegObservado] = lstRegistroE;
        }

        /// <summary>
        /// METODO. MValidaIngenioAzucarero
        /// </summary>
        /// <param name="values"></param>
        /// <param name="strMensaje"></param>
        /// <returns></returns>
        private bool MValidaIngenioAzucarero_99999(string[] values, ref string strMensaje)
        {
            var booResultado = true;
            try
            {
                var to = cmbTipoOperacion.Text.Split(';');
                var strTipoOp = to[1].Substring(2, 2);
                int[] arrayNumero;
                decimal dec;
                DateTime dt;
                switch (strTipoOp)
                {
                    case "RC":
                        #region Recepciones
                        if (values.Length != 13)
                        {
                            strMensaje = "- Nro. de columnas del registro invalido.";
                        }
                        arrayNumero = new[] { 6, 7, 12 };
                        for (int i = 0; i < arrayNumero.Length; i++)
                        {
                            if (!decimal.TryParse(values[arrayNumero[i]].Trim(), out dec))
                            {
                                strMensaje += "- Columna Nro. " + arrayNumero[i] + " No es un número.";
                            }
                        }
                        if (values[0].Trim().Substring(6, 4) + values[0].Trim().Substring(3, 2) != cmbPeriodo.Value.ToString().Substring(0, 4) + cmbPeriodo.Value.ToString().Substring(4, 2))
                        {
                            strMensaje += "- Periodo incorrecto.";
                        }
                        if (!DateTime.TryParse(values[0].Trim(), out dt))
                        {
                            strMensaje += "- Fecha incorrecta.";
                        }
                        if (Convert.ToDecimal(values[7]) <= 0)
                        {
                            strMensaje += "- Cantidad incorrecta.";
                        }
                        if (MCodificaParametrica((int)EnumTipoParametrica.Producto, values[5].Trim()) == -99999)
                        {
                            strMensaje += "- Producto incorrecto.";
                        }
                        if (MCodificaParametrica((int)EnumTipoParametrica.UnidadMedida, values[5].Trim()) == -99999)
                        {
                            strMensaje += "- Unidad de medida invalida.";
                        }
                        if (values[11].Trim().Length > 1)
                        {
                            if (values[11].Trim().ContainsAny("@", "/", "'", "\\", ",", "_", "+", "[", "]", "{", "}"))
                            {
                                strMensaje += "- Responsable contiene caracteres invalidos.";
                            }
                        }
                        if (values[10].Trim().Length == 0 || values[10].Trim() == "0")
                        {
                            strMensaje += "- Número de referencia invalido.";
                        }
                        if (values[3].Trim() != "NOID")
                        {
                            var lstInfraestructura = Session[CVariablesSesion.ListaInfraestructura] as List<O_INFRAESTRUCTURA_VOL_CTY>;
                            if (lstInfraestructura != null && lstInfraestructura.Any())
                            {
                                lstInfraestructura = lstInfraestructura.Where(x => x.CODIGO_OPERADOR == values[4].Trim() && x.TIPO == values[3].Trim()).ToList();
                                if (lstInfraestructura.Any())
                                {
                                    if (lstInfraestructura.FirstOrDefault().ID_PRODUCTO != null)
                                    {
                                        if (lstInfraestructura.FirstOrDefault().ID_PRODUCTO != MCodificaParametrica((int)EnumTipoParametrica.Producto, values[5].Trim()))
                                        {
                                            strMensaje += "- El producto no corresponde a la infraestructura origen especificada.";
                                        }
                                    }
                                }
                                else
                                {
                                    strMensaje += "- Infraestructura origen invalida.";
                                }
                            }
                            else
                            {
                                strMensaje += "- Infraestructura origen no definida.";
                            }
                        }
                        if (values[3].Trim() != "NOID" && values[4].Trim() == "NOID")
                        {
                            if (MCodificaParametrica((int)EnumTipoParametrica.TipoInfraestructura, values[3].Trim()) == -99999)
                            {
                                strMensaje += "- Tipo de infraestructura origen invalida.";
                            }
                        }
                        if (values[1].Trim() == "NOID" || values[2].Trim() == "NOID")
                        {
                            //strMensaje += "- Tipo infraestructura y/o infraestructura receptora invalida, no puede ser NOID.";
                        }
                        if (values[1].Trim() != "NOID")
                        {
                            var lstInfraestructura = Session[CVariablesSesion.ListaInfraestructura] as List<O_INFRAESTRUCTURA_VOL_CTY>;
                            if (lstInfraestructura != null && lstInfraestructura.Any())
                            {
                                lstInfraestructura = lstInfraestructura.Where(x => x.CODIGO_OPERADOR.Trim() == values[2].Trim() && x.TIPO.Trim() == values[1].Trim()).ToList();
                                if (lstInfraestructura.Any())
                                {
                                    if (lstInfraestructura.FirstOrDefault().ID_PRODUCTO != null)
                                    {
                                        if (lstInfraestructura.FirstOrDefault().ID_PRODUCTO != MCodificaParametrica((int)EnumTipoParametrica.Producto, values[5].Trim()))
                                        {
                                            strMensaje += "- El producto no corresponde a la infraestructura receptora especificada.";
                                        }
                                    }
                                }
                                else
                                {
                                    strMensaje += "- Infraestructura receptora invalida.";
                                }
                            }
                            else
                            {
                                strMensaje += "- Infraestructura receptora no registrada.";
                            }
                        }
                        #endregion
                        break;
                    default:
                        #region Despachos
                        if (values.Length != 22)
                        {
                            strMensaje = "- Nro. de columnas del registro invalido.";
                        }
                        arrayNumero = new[] { 12, 15, 17, 18, 20, 21 };
                        for (int i = 0; i < arrayNumero.Length; i++)
                        {
                            if (!decimal.TryParse(values[arrayNumero[i]].Trim(), out dec))
                            {
                                strMensaje += "- Columna Nro. " + arrayNumero[i] + " No es un número.";
                            }
                        }
                        if (values[0].Trim().Substring(6, 4) + values[0].Trim().Substring(3, 2) != cmbPeriodo.Value.ToString().Substring(0, 4) + cmbPeriodo.Value.ToString().Substring(4, 2))
                        {
                            strMensaje += "- Periodo incorrecto.";
                        }
                        if (!DateTime.TryParse(values[0].Trim(), out dt))
                        {
                            strMensaje += "- Fecha incorrecta.";
                        }
                        if ((values[13].Trim() == "V" && Convert.ToDecimal(values[12]) <= 0) || (values[13].Trim() == "A" && Convert.ToDecimal(values[12]) < 0))
                        {
                            strMensaje += "- Cantidad incorrecta.";
                        }
                        if (Convert.ToDecimal(values[21].Trim()) > (Convert.ToDecimal(values[12].Trim()) * Convert.ToDecimal(values[20].Trim())) + 2 ||
                            Convert.ToDecimal(values[21].Trim()) < (Convert.ToDecimal(values[12].Trim()) * Convert.ToDecimal(values[20].Trim())) - 2)
                        {
                            strMensaje += "- Importe incorrecto.";
                        }
                        if (values[19].Trim().Length == 1 && values[19].Trim() != "0")
                        {
                            strMensaje += "- Código de control incorrecto.";
                        }
                        if (values[19].Trim().Length > 1 && values[19].Trim().Length < 6)
                        {
                            strMensaje += "- Código de control incorrecto.";
                        }
                        if (values[5].Trim().Length > 1)
                        {
                            if (values[1].Trim().ContainsAny("-", "@", "/", "'", "\\", ".", ",", "_", "+", "[", "]", "{", "}", " "))
                            {
                                strMensaje += "- Identificador contiene caracteres invalidos.";
                            }
                        }
                        if (MCodificaParametrica((int)EnumTipoParametrica.Producto, values[11].Trim()) == -99999)
                        {
                            strMensaje += "- Producto incorrecto.";
                        }
                        if (MCodificaParametrica((int)EnumTipoParametrica.Pais, values[8].Trim()) == -99999)
                        {
                            strMensaje += "- País invalido.";
                        }
                        if (Convert.ToDecimal(values[17].Trim()) == 0 && (values[14].Trim().Length == 0 || values[14].Trim() == "0"))
                        {
                            strMensaje += "- Número de referencia/factura invalido. Debe registrar al menos un documento de respaldo.";
                        }
                        if (!(values[13].Trim() == "V" || values[13].Trim() == "A"))
                        {
                            strMensaje += "- Estado invalido.";
                        }
                        if (!(values[7].Trim() == "CS" || values[7].Trim() == "NOID"))
                        {
                            strMensaje += "- Medio de transporte invalido.";
                        }
                        if (!(values[9].Trim() == "UIND" || values[9].Trim() == "UALI" || values[9].Trim() == "NOID"))
                        {
                            strMensaje += "- Uso específico invalido.";
                        }
                        if (values[3].Trim() != "NOID")
                        {
                            var lstInfraestructura = Session[CVariablesSesion.ListaInfraestructura] as List<O_INFRAESTRUCTURA_VOL_CTY>;
                            if (lstInfraestructura != null && lstInfraestructura.Any())
                            {
                                lstInfraestructura = lstInfraestructura.Where(x => x.CODIGO_OPERADOR == values[4].Trim() && x.TIPO == values[3].Trim()).ToList();
                                if (lstInfraestructura.Any())
                                {
                                    if (lstInfraestructura.FirstOrDefault().ID_PRODUCTO != null)
                                    {
                                        if (lstInfraestructura.FirstOrDefault().ID_PRODUCTO != MCodificaParametrica((int)EnumTipoParametrica.Producto, values[11].Trim()))
                                        {
                                            strMensaje += "- El producto no corresponde a la infraestructura destino especificada.";
                                        }
                                    }
                                }
                                else
                                {
                                    strMensaje += "- Infraestructura destino invalida.";
                                }
                            }
                            else
                            {
                                strMensaje += "- Infraestructura destino no definida.";
                            }
                        }
                        if (values[3].Trim() != "NOID" && values[4].Trim() == "NOID")
                        {
                            if (MCodificaParametrica((int)EnumTipoParametrica.TipoInfraestructura, values[3].Trim()) == -99999)
                            {
                                strMensaje += "- Tipo de infraestructura destino invalida.";
                            }
                        }

                        if (values[1].Trim() == "NOID" || values[2].Trim() == "NOID")
                        {
                            //strMensaje += "- Tipo infraestructura y/o infraestructura despachante invalida, no puede ser NOID.";
                        }
                        if (values[1].Trim() != "NOID")
                        {
                            var lstInfraestructura = Session[CVariablesSesion.ListaInfraestructura] as List<O_INFRAESTRUCTURA_VOL_CTY>;
                            if (lstInfraestructura != null && lstInfraestructura.Any())
                            {
                                lstInfraestructura = lstInfraestructura.Where(x => x.CODIGO_OPERADOR.Trim() == values[2].Trim() && x.TIPO.Trim() == values[1].Trim()).ToList();
                                if (lstInfraestructura.Any())
                                {
                                    if (lstInfraestructura.FirstOrDefault().ID_PRODUCTO != null)
                                    {
                                        if (lstInfraestructura.FirstOrDefault().ID_PRODUCTO != MCodificaParametrica((int)EnumTipoParametrica.Producto, values[11].Trim()))
                                        {
                                            strMensaje += "- El producto no corresponde a la infraestructura despachante especificada.";
                                        }
                                    }
                                }
                                else
                                {
                                    strMensaje += "- Infraestructura despachante invalida.";
                                }
                            }
                            else
                            {
                                strMensaje += "- Infraestructura despachante no registrada.";
                            }
                        }
                        #endregion
                        break;
                }
            }
            catch (Exception ex)
            {
                strMensaje += ex.Message;
            }

            if (strMensaje.Trim().Length > 0)
            {
                booResultado = false;
            }
            else
            {
                //todo No implementado.
            }

            return booResultado;
        }

        private bool MValidaIngenioAzucarero(string[] values, ref string strMensaje)
        {
            var booResultado = true;
            try
            {
                var to = cmbTipoOperacion.Text.Split(';');
                var strTipoOp = to[1].Substring(2, 2);
                int[] arrayNumero;
                decimal dec;
                DateTime dt;
                switch (strTipoOp)
                {
                    case "RC":
                        #region Recepciones
                        if (values.Length != 7)
                        {
                            strMensaje = "- Nro. de columnas del registro invalido.";
                        }
                        arrayNumero = new[] { 2, 3, 6 };
                        for (int i = 0; i < arrayNumero.Length; i++)
                        {
                            if (!decimal.TryParse(values[arrayNumero[i]].Trim(), out dec))
                            {
                                strMensaje += "- Columna Nro. " + arrayNumero[i] + " No es un número.";
                            }
                        }
                        if (values[0].Trim().Substring(6, 4) + values[0].Trim().Substring(3, 2) != cmbPeriodo.Value.ToString().Substring(0, 4) + cmbPeriodo.Value.ToString().Substring(4, 2))
                        {
                            strMensaje += "- Periodo incorrecto.";
                        }
                        if (!DateTime.TryParse(values[0].Trim(), out dt))
                        {
                            strMensaje += "- Fecha incorrecta.";
                        }
                        if (Convert.ToDecimal(values[3]) <= 0)
                        {
                            strMensaje += "- Cantidad incorrecta.";
                        }
                        if (MCodificaParametrica((int)EnumTipoParametrica.Producto, values[1].Trim()) == -99999)
                        {
                            strMensaje += "- Producto incorrecto.";
                        }                     
                        if (values[4].Trim().Length == 0 || values[4].Trim() == "0")
                        {
                            strMensaje += "- Número de referencia invalido.";
                        }                                                         
                       
                        #endregion
                        break;
                    default:
                        #region Despachos
                        if (values.Length != 21)
                        {
                            strMensaje = "- Nro. de columnas del registro invalido.";
                        }
                        arrayNumero = new[] { 7, 8, 10, 11, 15, 16};//12
                        for (int i = 0; i < arrayNumero.Length; i++)
                        {
                            if (!decimal.TryParse(values[arrayNumero[i]].Trim(), out dec))
                            {
                                strMensaje += "- Columna Nro. " + arrayNumero[i] + " No es un número.";
                            }
                        }
                        if (values[0].Trim().Substring(6, 4) + values[0].Trim().Substring(3, 2) != cmbPeriodo.Value.ToString().Substring(0, 4) + cmbPeriodo.Value.ToString().Substring(4, 2))
                        {
                            strMensaje += "- Periodo incorrecto.";
                        }
                        if (!DateTime.TryParse(values[0].Trim(), out dt))
                        {
                            strMensaje += "- Fecha incorrecta.";
                        }
                        if ((values[9].Trim() == "V" && Convert.ToDecimal(values[7]) <= 0) || (values[9].Trim() == "A" && Convert.ToDecimal(values[7]) < 0))
                        {
                            strMensaje += "- Cantidad incorrecta.";
                        }
                        if (Convert.ToDecimal(values[16].Trim()) > (Convert.ToDecimal(values[7].Trim()) * Convert.ToDecimal(values[15].Trim())) + 2 ||
                            Convert.ToDecimal(values[16].Trim()) < (Convert.ToDecimal(values[7].Trim()) * Convert.ToDecimal(values[15].Trim())) - 2)
                        {
                            strMensaje += "- Importe incorrecto.";
                        }
                        if (values[13].Trim().Length == 1 && values[13].Trim() != "0")
                        {
                            strMensaje += "- Código de control incorrecto.";
                        }
                        if (values[13].Trim().Length > 1 && values[13].Trim().Length < 6)
                        {
                            strMensaje += "- Código de control incorrecto.";
                        }
                        if (values[1].Trim().Length > 1)
                        {
                            if (values[1].Trim().ContainsAny("-", "@", "/", "'", "\\", ".", ",", "_", "+", "[", "]", "{", "}", " "))
                            {
                                strMensaje += "- Identificador contiene caracteres invalidos.";
                            }
                        }
                        if (MCodificaParametrica((int)EnumTipoParametrica.Producto, values[5].Trim()) == -99999)
                        {
                            strMensaje += "- Producto incorrecto.";
                        }
                        if (MCodificaParametrica((int)EnumTipoParametrica.Pais, values[2].Trim()) == -99999)
                        {
                            strMensaje += "- País invalido.";
                        }
                        if (Convert.ToDecimal(values[11].Trim()) == 0 && values[17].Trim() == "0" && values[18].Trim() == "0" && values[19].Trim() == "0")
                        {
                            strMensaje += "- Número de certificado recepcion/factura invalido. Debe registrar al menos un documento de respaldo.";
                        }
                        if (!(values[9].Trim() == "V" || values[9].Trim() == "A"))
                        {
                            strMensaje += "- Estado invalido.";
                        }
                        if (!(values[20].Trim() == "FIS" || values[20].Trim() == "CON"))
                        {
                            strMensaje += "- Tipo de movimiento invalido.";
                        }
                        //if (!(values[6].Trim() == "NOID" || values[6].Trim() == "EXP" || values[6].Trim() == "DOM" || values[6].Trim() == "IND" || values[6].Trim() == "COM"))
                        //{
                        //    strMensaje += "- Uso especifico invalido.";
                        //}
                        if (MCodificaParametrica((int)EnumTipoParametrica.TipoConsumidor, values[6].Trim()) == -99999)
                        {
                            strMensaje += "- Uso especifico invalido.";
                        }
                        if (values[3].Trim().Length>0)
                        {
                            if (MCodificaParametrica((int)EnumTipoParametrica.Entidad, values[3].Trim()) == -99999)
                            {
                                strMensaje += "- Planta de almacenaje de destino invalida.";
                            }
                        }
                        #endregion
                        break;
                }
            }
            catch (Exception ex)
            {
                strMensaje += ex.Message;
            }

            if (strMensaje.Trim().Length > 0)
            {
                booResultado = false;
            }
            else
            {
                //todo No implementado.
            }

            return booResultado;
        }    

        /// <summary>
        /// METODO. MCargaIngenioAzucarero
        /// </summary>
        /// <param name="strRuta"></param>
        private void MCargaIngenioAzucarero(string strRuta)
        {
            var reader = new StreamReader(File.OpenRead(@strRuta));
            var lstRegistros = new List<CRegistrosImportados>();
            var lstRegistroE = new List<CRegistrosError>();
            long lngNoRegCorrect = 1, lngNoRegInCorrect = 1;
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(';');
                try
                {
                    DateTime datFecha, datFechaReg;
                    string strFecha;
                    ObjTransaccion objFactura, objDocumento;
                    ObjProtoMovimientoTransaccion objNuevoTipoReg = null;
                    int estadoTra;
                    List<ObjTransaccion> lstTransacciones;
                    var to = cmbTipoOperacion.Text.Split(';');
                    var strTipoOp = to[1].Substring(2, 2);

                    string strMensaje = string.Empty;
                    if (MValidaIngenioAzucarero(values, ref strMensaje))
                    {
                        var cDato = new CRegistrosImportados();
                        int controlBinario;
                        switch (strTipoOp)
                        {
                            case "RC":
                                #region Valida y genera objeto

                                cDato.LngId = lngNoRegCorrect;
                                cDato.StrIdVenta = lngNoRegCorrect.ToString();
                                datFecha = DateTime.Parse(values[0].Trim());
                                cDato.StrFechaEmision = datFecha.ToString();
                                cDato.StrTipoMov = "NAC";
                                cDato.StrEstado = "V";
                                cDato.LngNit = 0;
                                cDato.StrRazonSocial = values[5].Trim().ToUpper().Replace("¥", "Ñ").Replace("Á", "A").Replace("É", "E").Replace("Í", "I").Replace("Ó", "O").Replace("Ú", "U").Replace("'", "").Replace("�", "Ñ");
                                cDato.StrRazonSocial = Regex.Replace(cDato.StrRazonSocial, @"\s+", " ");
                                cDato.StrIdentificador = "";
                                cDato.LngNroFactura = 0;
                                cDato.LngNroAutorizacion = 0;
                                cDato.StrNroReferencia = values[4].Trim();
                                cDato.LngNroAutVenta = 0;
                                cDato.StrProducto = values[1].Trim();
                                cDato.DecVolumen = Convert.ToDecimal(values[3]);
                                cDato.DecPrecio = 0;
                                cDato.DecImporte = 0;
                                cDato.DecIdTipoCreditoFiscal = (int)EnumTipoCredFiscal.No;
                                cDato.objProtoMovimiento = new ObjProtoMovimientoTransaccion();
                                cDato.DecIdTipoOp = Convert.ToDecimal(cmbTipoOperacion.Value);
                                cDato.DecOrden = 1;

                                strFecha = datFecha.Year + datFecha.Month.ToString().PadLeft(2, '0') + datFecha.Day.ToString().PadLeft(2, '0') + datFecha.Hour.ToString().PadLeft(2, '0') + datFecha.Minute.ToString().PadLeft(2, '0') + datFecha.Second.ToString().PadLeft(2, '0');
                                datFechaReg = DateTime.Now;
                                estadoTra = (cDato.StrEstado == "A") ? (int)EnumEstadoMov.Anulado : (int)EnumEstadoMov.Activo;
                                lstTransacciones = new List<ObjTransaccion>();
                                controlBinario = 1;

                                objDocumento = new ObjTransaccion();
                                objDocumento.idMovimiento = 0;
                                objDocumento.idTransaccion = 0;
                                objDocumento.idTransaccionPadre = 0;
                                objDocumento.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                objDocumento.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                objDocumento.idTipoTransaccion = (decimal)EnumTipoTransaccion.NotaMovimiento;
                                objDocumento.fechaEmision = Convert.ToInt64(strFecha);
                                objDocumento.numeroTransaccion = values[4].Trim();;
                                objDocumento.numeroAutorizacion = 0;
                                objDocumento.codigoControl = "";
                                objDocumento.nitConsumidor = Convert.ToDecimal(values[6].Trim());
                                objDocumento.razonSocial = "";
                                objDocumento.precioUnitario = 0;
                                objDocumento.precioTotal = 0;
                                objDocumento.usuarioTransaccion = values[6].Trim();
                                objDocumento.observaciones = "";
                                objDocumento.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                objDocumento.idEstadoTransaccion = estadoTra;
                                objDocumento.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.No;
                                objDocumento.idFormaPago = (decimal)EnumTiposPago.Efectivo;
                                objDocumento.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                objDocumento.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                objDocumento.codigoSeguridad = 0;
                                objDocumento.idAutorizacionDato = 0;
                                objDocumento.binarioControl = controlBinario + "000000000";
                                objDocumento.fechaOperacion = Convert.ToInt64(strFecha);
                                objDocumento.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                objDocumento.codigo = "";
                                lstTransacciones.Add(objDocumento);

                                objNuevoTipoReg = new ObjProtoMovimientoTransaccion();
                                objNuevoTipoReg.idMovimientoPadre = 0;
                                objNuevoTipoReg.idTipoActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                objNuevoTipoReg.idTipoOperacion = Convert.ToDecimal(cmbTipoOperacion.Value);
                                objNuevoTipoReg.idConsumidor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString());
                                objNuevoTipoReg.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                objNuevoTipoReg.fechaRegistroMovimiento = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString().PadLeft(2, '0') + datFechaReg.Minute.ToString().PadLeft(2, '0') + datFechaReg.Second.ToString().PadLeft(2, '0'));
                                objNuevoTipoReg.fechaOperacion = Convert.ToInt64(strFecha);
                                objNuevoTipoReg.idTipoMovimientoFlujo = (int)EnumTipoMovimientoFlujo.Fisico;
                                objNuevoTipoReg.idTipoMovimiento = (int)EnumTipoMovimiento.NacNac;
                                objNuevoTipoReg.idEstadoMovimiento = (cDato.StrEstado == "A") ? (int)EnumEstadoMov.Anulado : (int)EnumEstadoMov.Activo;
                                objNuevoTipoReg.idFuenteOrigenDato = (decimal)EnumTipoRegistro.OrigenCsv;
                                objNuevoTipoReg.idUbicacionOperacion = MCodificaParametrica((int)EnumTipoParametrica.Pais, "BOLIVIA");
                                objNuevoTipoReg.idUnidadMedidaHidrocarburo = -99999;
                                objNuevoTipoReg.idHidrocarburo = MCodificaParametrica((int)EnumTipoParametrica.Producto, values[1].Trim());
                                objNuevoTipoReg.cantidadHidrocarburo = cDato.DecVolumen;
                                objNuevoTipoReg.identificador = cDato.StrIdentificador;
                                objNuevoTipoReg.subIdentificador = "";
                                objNuevoTipoReg.observacion = "";
                                objNuevoTipoReg.tipoInfraestructura = -99999;
                                objNuevoTipoReg.idInfraestructura = -99999;
                                objNuevoTipoReg.idTipoMedioTransporte = -99999;
                                objNuevoTipoReg.idMedioTransporte = -99999;
                                objNuevoTipoReg.idUnidadEnvasesTransporte = -99999;
                                objNuevoTipoReg.idEntidad = -99999;
                                objNuevoTipoReg.codigo = "";
                                objNuevoTipoReg.idTipoMedicion = -99999;
                                objNuevoTipoReg.cantidadAguaLitros = 0;
                                objNuevoTipoReg.idUnidadTemperatura = (decimal)EnumUnidadTemperatura.GradoAlcoholico;
                                objNuevoTipoReg.temperatura = 0;
                                objNuevoTipoReg.gradoApi = values[2].Trim();
                                objNuevoTipoReg.cantidadEnvasesTransporte = 0;
                                objNuevoTipoReg.binarioControl = "00000-0000000000-0000000000-00000";
                                objNuevoTipoReg.idIntegracion = -99999;
                                objNuevoTipoReg.idTransaccionBaseExterna = "";
                                objNuevoTipoReg.fechaOperacionDia = 0;
                                objNuevoTipoReg.codigo = "";
                                objNuevoTipoReg.barcode = 0;
                                objNuevoTipoReg.listaTransaccion = JsonSerializer.SerializeToString(lstTransacciones);

                                objNuevoTipoReg.jsonReferencia = "";
                                objNuevoTipoReg.estadoJsonRef = -99999;
                                objNuevoTipoReg.idCuenta = -99999;
                                objNuevoTipoReg.idCuentaAux = -99999;
                                objNuevoTipoReg.idParamGlobal = -99999;
                                objNuevoTipoReg.idParamEspec = -99999;
                                objNuevoTipoReg.idTipoConsumidor = -99999;
                                objNuevoTipoReg.idTipoInfraAux = -99999;
                                objNuevoTipoReg.idInfraAux = -99999;

                                #endregion
                                break;
                            case "DS":
                                #region Valida y genera objeto

                                cDato.LngId = lngNoRegCorrect;
                                cDato.StrIdVenta = lngNoRegCorrect.ToString();
                                datFecha = DateTime.Parse(values[0].Trim());
                                cDato.StrFechaEmision = datFecha.ToString();
                                cDato.StrTipoMov = (values[2].Trim() == "BOLIVIA") ? "NAC" : "INT";
                                cDato.StrEstado = values[9].Trim();
                                cDato.LngNit = Convert.ToInt64(values[11]);
                                cDato.StrRazonSocial = values[14].Trim().ToUpper().Replace("¥", "Ñ").Replace("Á", "A").Replace("É", "E").Replace("Í", "I").Replace("Ó", "O").Replace("Ú", "U").Replace("'", "").Replace("�", "Ñ");
                                cDato.StrRazonSocial = Regex.Replace(cDato.StrRazonSocial, @"\s+", " ");
                                cDato.StrIdentificador = values[1].Trim().Replace("-", "").Replace(" ", "");
                                cDato.LngNroFactura = Convert.ToInt64(values[11]);                               
                                cDato.LngNroAutorizacion = esnumero(values[12].Trim())==true ? Convert.ToInt64(values[12]) : 0; //adicion par el CUF;
                                cDato.strCodigoAutorizacion = esnumero(values[12].Trim()) == true ? "" : values[12].Trim().ToUpper();
                                cDato.StrNroReferencia = values[17].Trim();
                                cDato.LngNroAutVenta = 0;
                                cDato.StrProducto = values[5].Trim();
                                cDato.DecVolumen = Convert.ToDecimal(values[7]);
                                cDato.DecPrecio = Convert.ToDecimal(values[15]);
                                cDato.DecImporte = Convert.ToDecimal(values[16]);
                                cDato.DecIdTipoCreditoFiscal = (Convert.ToDecimal(values[11]) > 0 && Convert.ToDecimal(values[10]) > 0) ? (int)EnumTipoCredFiscal.Si : (int)EnumTipoCredFiscal.No;
                                cDato.objProtoMovimiento = new ObjProtoMovimientoTransaccion();
                                cDato.DecIdTipoOp = Convert.ToDecimal(cmbTipoOperacion.Value);
                                cDato.DecOrden = 1;

                                strFecha = datFecha.Year + datFecha.Month.ToString().PadLeft(2, '0') + datFecha.Day.ToString().PadLeft(2, '0') + datFecha.Hour.ToString().PadLeft(2, '0') + datFecha.Minute.ToString().PadLeft(2, '0') + datFecha.Second.ToString().PadLeft(2, '0');
                                datFechaReg = DateTime.Now;
                                estadoTra = (cDato.StrEstado == "A") ? (int)EnumEstadoMov.Anulado : (int)EnumEstadoMov.Activo;
                                lstTransacciones = new List<ObjTransaccion>();
                                var intFormaPago = 1;
                                controlBinario = 1;

                                if (Convert.ToDecimal(values[11].Trim()) > 0)
                                {
                                    objFactura = new ObjTransaccion();
                                    objFactura.idMovimiento = 0;
                                    objFactura.idTransaccion = 0;
                                    objFactura.idTransaccionPadre = 0;
                                    objFactura.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                    objFactura.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                    //objFactura.idTipoTransaccion = (decimal)((values[13].Trim().Length > 4) ? EnumTipoTransaccion.FacturaComputarizada : EnumTipoTransaccion.FacturaManual);                                   
                                    objFactura.fechaEmision = Convert.ToInt64(strFecha);
                                    objFactura.numeroTransaccion = values[11].Trim();                                    
                                    objFactura.numeroAutorizacion = esnumero(values[12].Trim()) == true ? Convert.ToDecimal(values[12]) : 0;//adicion par el CUF;
                                    objFactura.codigoAutorizacion = esnumero(values[12].Trim()) == true ? "" : values[12].Trim().ToUpper();
                                    objFactura.idTipoTransaccion = (decimal)((objFactura.codigoAutorizacion.Length > 25 || values[13].Trim().Length > 0) ? EnumTipoTransaccion.FacturaComputarizada : EnumTipoTransaccion.FacturaManual);
                                    objFactura.codigoControl = values[13].Trim();
                                    objFactura.nitConsumidor = Convert.ToDecimal(values[10].Trim());
                                    objFactura.razonSocial = cDato.StrRazonSocial;
                                    objFactura.precioUnitario = Convert.ToDecimal(values[15].Trim());
                                    objFactura.precioTotal = Convert.ToDecimal(values[16].Trim());
                                    objFactura.usuarioTransaccion = "";
                                    objFactura.observaciones = "";
                                    objFactura.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                    objFactura.idEstadoTransaccion = estadoTra;
                                    objFactura.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.Si;
                                    objFactura.idFormaPago = intFormaPago;
                                    objFactura.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                    objFactura.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                    objFactura.codigoSeguridad = 0;
                                    objFactura.idAutorizacionDato = 0;
                                    objFactura.binarioControl = controlBinario + "000000000";
                                    objFactura.fechaOperacion = Convert.ToInt64(strFecha);
                                    objFactura.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                    objFactura.codigo = "";

                                    lstTransacciones.Add(objFactura);
                                    controlBinario = 0;
                                }

                                if (values[18].Trim().Length > 0)
                                {
                                    objDocumento = new ObjTransaccion();
                                    objDocumento.idMovimiento = 0;
                                    objDocumento.idTransaccion = 0;
                                    objDocumento.idTransaccionPadre = 0;
                                    objDocumento.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                    objDocumento.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                    objDocumento.idTipoTransaccion = (decimal)EnumTipoTransaccion.NotaMovimiento;
                                    objDocumento.fechaEmision = Convert.ToInt64(strFecha);
                                    objDocumento.numeroTransaccion = values[18].Trim();
                                    objDocumento.numeroAutorizacion = 0;
                                    objDocumento.codigoControl = "";
                                    objDocumento.nitConsumidor = 0;
                                    objDocumento.razonSocial = "";
                                    objDocumento.precioUnitario = 0;
                                    objDocumento.precioTotal = 0;
                                    objDocumento.usuarioTransaccion = "";
                                    objDocumento.observaciones = "";
                                    objDocumento.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                    objDocumento.idEstadoTransaccion = estadoTra;
                                    objDocumento.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.No;
                                    objDocumento.idFormaPago = intFormaPago;
                                    objDocumento.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                    objDocumento.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                    objDocumento.codigoSeguridad = 0;
                                    objDocumento.idAutorizacionDato = 0;
                                    objDocumento.binarioControl = controlBinario + "000000000";
                                    objDocumento.fechaOperacion = Convert.ToInt64(strFecha);
                                    objDocumento.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                    objDocumento.codigo = "";
                                    lstTransacciones.Add(objDocumento);
                                }
                        
                                if (values[17].Trim().Length > 0)
                                {
                                    objDocumento = new ObjTransaccion();
                                    objDocumento.idMovimiento = 0;
                                    objDocumento.idTransaccion = 0;
                                    objDocumento.idTransaccionPadre = 0;
                                    objDocumento.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                    objDocumento.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString().PadLeft(2, '0') + datFechaReg.Day.ToString().PadLeft(2, '0') + datFechaReg.Hour.ToString().PadLeft(2, '0') + datFechaReg.Minute.ToString().PadLeft(2, '0') + datFechaReg.Second.ToString().PadLeft(2, '0'));
                                    objDocumento.idTipoTransaccion = (decimal)EnumTipoTransaccion.DocCre;
                                    objDocumento.fechaEmision = Convert.ToInt64(strFecha);
                                    objDocumento.numeroTransaccion = values[17].Trim();
                                    objDocumento.numeroAutorizacion = 0;
                                    objDocumento.codigoControl = "";
                                    objDocumento.nitConsumidor = 0;
                                    objDocumento.razonSocial = "";
                                    objDocumento.precioUnitario = 0;
                                    objDocumento.precioTotal = 0;
                                    objDocumento.usuarioTransaccion ="";
                                    objDocumento.observaciones = "";
                                    objDocumento.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                    objDocumento.idEstadoTransaccion = estadoTra;
                                    objDocumento.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.No;
                                    objDocumento.idFormaPago = (int)EnumTiposPago.Efectivo;
                                    objDocumento.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                    objDocumento.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                    objDocumento.codigoSeguridad = 0;
                                    objDocumento.idAutorizacionDato = 0;
                                    objDocumento.binarioControl = controlBinario + "000000000";
                                    objDocumento.fechaOperacion = Convert.ToInt64(strFecha);
                                    objDocumento.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                    objDocumento.codigo = "";
                                    lstTransacciones.Add(objDocumento);
                                }

                                if (values[19].Trim().Length > 0)
                                {
                                    objDocumento = new ObjTransaccion();
                                    objDocumento.idMovimiento = 0;
                                    objDocumento.idTransaccion = 0;
                                    objDocumento.idTransaccionPadre = 0;
                                    objDocumento.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                    objDocumento.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString().PadLeft(2, '0') + datFechaReg.Day.ToString().PadLeft(2, '0') + datFechaReg.Hour.ToString().PadLeft(2, '0') + datFechaReg.Minute.ToString().PadLeft(2, '0') + datFechaReg.Second.ToString().PadLeft(2, '0'));
                                    objDocumento.idTipoTransaccion = (decimal)EnumTipoTransaccion.DocFactRef;
                                    objDocumento.fechaEmision = Convert.ToInt64(strFecha);
                                    objDocumento.numeroTransaccion = values[19].Trim();
                                    objDocumento.numeroAutorizacion = 0;
                                    objDocumento.codigoControl = "";
                                    objDocumento.nitConsumidor = 0;
                                    objDocumento.razonSocial = "";
                                    objDocumento.precioUnitario = 0;
                                    objDocumento.precioTotal = 0;
                                    objDocumento.usuarioTransaccion = "";
                                    objDocumento.observaciones = "";
                                    objDocumento.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                    objDocumento.idEstadoTransaccion = estadoTra;
                                    objDocumento.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.No;
                                    objDocumento.idFormaPago = (int)EnumTiposPago.Efectivo;
                                    objDocumento.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                    objDocumento.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                    objDocumento.codigoSeguridad = 0;
                                    objDocumento.idAutorizacionDato = 0;
                                    objDocumento.binarioControl = controlBinario + "000000000";
                                    objDocumento.fechaOperacion = Convert.ToInt64(strFecha);
                                    objDocumento.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                    objDocumento.codigo = "";
                                    lstTransacciones.Add(objDocumento);
                                }                               
                             
                                objNuevoTipoReg = new ObjProtoMovimientoTransaccion();
                                objNuevoTipoReg.idMovimientoPadre = 0;
                                objNuevoTipoReg.idTipoActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                objNuevoTipoReg.idTipoOperacion = Convert.ToDecimal(cmbTipoOperacion.Value);
                                objNuevoTipoReg.idConsumidor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString());
                                objNuevoTipoReg.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                objNuevoTipoReg.fechaRegistroMovimiento = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                objNuevoTipoReg.fechaOperacion = Convert.ToInt64(strFecha);
                                objNuevoTipoReg.idTipoMovimientoFlujo = (values[20].Trim() == "CON") ? (int)EnumTipoMovimientoFlujo.Contable : (int)EnumTipoMovimientoFlujo.Fisico;
                                objNuevoTipoReg.idTipoMovimiento = (cDato.StrTipoMov == "NAC") ? (int)EnumTipoMovimiento.NacNac : (int)EnumTipoMovimiento.NacInt;
                                objNuevoTipoReg.idEstadoMovimiento = estadoTra;
                                objNuevoTipoReg.idFuenteOrigenDato = (decimal)EnumTipoRegistro.OrigenCsv;
                                objNuevoTipoReg.idUbicacionOperacion = MCodificaParametrica((int)EnumTipoParametrica.Pais, values[2].Trim());
                                objNuevoTipoReg.idUnidadMedidaHidrocarburo = -99999;
                                objNuevoTipoReg.idHidrocarburo = MCodificaParametrica((int)EnumTipoParametrica.Producto, values[5].Trim());
                                objNuevoTipoReg.cantidadHidrocarburo = cDato.DecVolumen;
                                objNuevoTipoReg.identificador = cDato.StrIdentificador;
                                objNuevoTipoReg.subIdentificador = "";//values[6].Trim();
                                objNuevoTipoReg.observacion = "";

                                objNuevoTipoReg.idTipoMedioTransporte = -99999;
                                objNuevoTipoReg.idMedioTransporte = -99999;

                                objNuevoTipoReg.tipoInfraestructura = -99999;
                                objNuevoTipoReg.idInfraestructura = -99999;

                                objNuevoTipoReg.idUnidadEnvasesTransporte = -99999;
                                objNuevoTipoReg.idEntidad = MCodificaParametrica((int)EnumTipoParametrica.Entidad, values[3].Trim());
                                objNuevoTipoReg.idTipoMedicion = -99999;
                                objNuevoTipoReg.cantidadAguaLitros = 0;
                                objNuevoTipoReg.idUnidadTemperatura = -99999;
                                objNuevoTipoReg.temperatura = 0;
                                objNuevoTipoReg.gradoApi = values[8].Trim();
                                objNuevoTipoReg.cantidadEnvasesTransporte = 0;
                                objNuevoTipoReg.binarioControl = "00000-0000000000-0000000000-00000";
                                objNuevoTipoReg.idIntegracion = -99999;
                                objNuevoTipoReg.idTransaccionBaseExterna = "";
                                objNuevoTipoReg.fechaOperacionDia = 0;
                                objNuevoTipoReg.codigo = "";
                                objNuevoTipoReg.barcode = 0;
                                objNuevoTipoReg.listaTransaccion = JsonSerializer.SerializeToString(lstTransacciones);

                                objNuevoTipoReg.jsonReferencia = "";
                                objNuevoTipoReg.estadoJsonRef = -99999;
                                objNuevoTipoReg.idCuenta = -99999;
                                objNuevoTipoReg.idCuentaAux = -99999;
                                objNuevoTipoReg.idParamGlobal = -99999;
                                objNuevoTipoReg.idParamEspec = -99999;
                                objNuevoTipoReg.idTipoConsumidor = MCodificaParametrica((int)EnumTipoParametrica.TipoConsumidor, values[6].Trim());
                                objNuevoTipoReg.idTipoInfraAux = -99999;
                                objNuevoTipoReg.idInfraAux = -99999;

                                #endregion
                                break;
                        }
                        cDato.objProtoMovimiento = objNuevoTipoReg;
                        lstRegistros.Add(cDato);
                        lngNoRegCorrect++;
                    }
                    else
                    {
                        if (strTipoOp == "RC")
                        {
                            var cRegError = new CRegistrosError();
                            cRegError.LngId = lngNoRegInCorrect;
                            cRegError.StrIdVenta = lngNoRegCorrect.ToString(); ;
                            cRegError.LngFechaEmision = values[0].Trim();
                            cRegError.LngNit = "";
                            cRegError.LngNroAutorizacion = "0";
                            cRegError.LngNroFactura = "0";
                            cRegError.LngNroReferencia = values[4].Trim();
                            cRegError.StrErrores = strMensaje.Trim();
                            lstRegistroE.Add(cRegError);
                            lngNoRegInCorrect++;
                        }
                        else
                        {
                            var cRegError = new CRegistrosError();
                            cRegError.LngId = lngNoRegInCorrect;
                            cRegError.StrIdVenta = lngNoRegCorrect.ToString(); ;
                            cRegError.LngFechaEmision = values[0].Trim();
                            cRegError.LngNit = values[10].Trim();
                            cRegError.LngNroAutorizacion = "";
                            cRegError.LngNroFactura = values[11].Trim();
                            cRegError.LngNroReferencia = values[17].Trim();
                            cRegError.StrErrores = strMensaje.Trim();
                            lstRegistroE.Add(cRegError);
                            lngNoRegInCorrect++;
                        }
                    }
                }
                catch (Exception ex)
                {
                    var cRegError = new CRegistrosError();
                    cRegError.LngId = lngNoRegInCorrect;
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
            }

            //var ojo2 = JsonSerializer.SerializeToString(lstRegistros);
            dgvTransacciones.DataSource = lstRegistros;
            dgvTransacciones.DataBind();
            Session[CVariablesSesion.GrillaTransaccion] = lstRegistros;

            dgvTransaccionError.DataSource = lstRegistroE;
            dgvTransaccionError.DataBind();
            Session[CVariablesSesion.GrillaRegObservado] = lstRegistroE;
        }

        private bool MValidaGraco(string[] values, ref string strMensaje)
        {
            var booResultado = true;
            try
            {
                var to = cmbTipoOperacion.Text.Split(';');
                var strTipoOp = to[1].Substring(2, 2);
                int[] arrayNumero;
                decimal dec;
                DateTime dt;
                switch (strTipoOp)
                {
                    case "RC":
                        #region Recepciones
                        if (values.Length != 30)
                        {
                            strMensaje = "- Nro. de columnas del registro invalido.";
                        }
                        arrayNumero = new[] { 6, 10, 13, 16, 19, 20, 23, 24, 27, 29 };                    
                        for (int i = 0; i < arrayNumero.Length; i++)
                        {
                            if (!decimal.TryParse(values[arrayNumero[i]].Trim(), out dec))
                            {
                                strMensaje += "- Columna Nro. " + arrayNumero[i] + " No es un número.";
                            }
                        }

                        if (values[0].Trim().Substring(6, 4) + values[0].Trim().Substring(3, 2) != cmbPeriodo.Value.ToString().Substring(0, 4) + cmbPeriodo.Value.ToString().Substring(4, 2))
                        {
                            strMensaje += "- Periodo incorrecto.";
                        }
                    
                        if (!DateTime.TryParse(values[0].Trim(), out dt))
                        {
                            strMensaje += "- Fecha incorrecta.";
                        }
                        if (MCodificaParametrica((int)EnumTipoParametrica.Producto, values[9].Trim()) == -99999)
                        {
                            strMensaje += "- Producto incorrecto.";
                        }
                        if (MCodificaParametrica((int)EnumTipoParametrica.UnidadMedida, values[9].Trim()) == -99999)
                        {
                            strMensaje += "- Unidad de medida invalida.";
                        }
                        if (Convert.ToDecimal(values[10]) <= 0)
                        {
                            strMensaje += "- Cantidad incorrecto.";
                        }
                   
                        if (values[5].Trim().Length > 1)
                        {
                            if (values[5].Trim().ContainsAny("-", "@", "/", "'", "\\", ".", ",", "_", "+", "[", "]", "{", "}", " "))
                            {
                                strMensaje += "- Identificador contiene caracteres invalidos.";
                            }
                        }
                        if (values[1].Trim() != "NOID")
                        {
                            if (MCodificaParametrica((int)EnumTipoParametrica.Entidad, values[1].Trim()) == -99999)
                            {
                                strMensaje += "- Entidad origen invalida.";
                            }
                        }
                        if (values[3].Trim() != "NOID")
                        {
                            var lstInfraestructura = Session[CVariablesSesion.ListaInfraestructura] as List<O_INFRAESTRUCTURA_VOL_CTY>;
                            if (lstInfraestructura != null && lstInfraestructura.Any())
                            {
                                lstInfraestructura = lstInfraestructura.Where(x => x.CODIGO_OPERADOR == values[3].Trim() && x.TIPO == values[2].Trim()).ToList();
                                if (lstInfraestructura.Any())
                                {
                                    if (lstInfraestructura.FirstOrDefault().ID_PRODUCTO != MCodificaParametrica((int)EnumTipoParametrica.Producto, values[9].Trim()))
                                    {
                                        strMensaje += "- El producto no corresponde a la infraestructura receptora especificada.";
                                    }
                                }
                                else
                                {
                                    strMensaje += "- Infraestructura despachante invalida.";
                                }
                            }
                            else
                            {
                                strMensaje += "- Infraestructura despachante no registrada.";
                            }
                        }
                        if (Convert.ToDecimal(values[16]) == 0 && values[21].Trim().Length == 0 && values[26].Trim().Length == 0)
                        {
                            strMensaje += "- No existe documento de respaldo.";
                        }
                        #endregion
                        break;
                    default:
                        #region Despachos
                        arrayNumero = new[] { 5, 8, 12, 14, 15, 17, 18};
                        if (values.Length != 20)
                        {
                            strMensaje = "- Nro. de columnas del registro invalido.";
                        }
                        else
                        {
                            arrayNumero = new[] { 5, 8, 12, 14, 15, 17, 18 };
                        }                        
                        
                        for (int i = 0; i < arrayNumero.Length; i++)
                        {
                            if (!decimal.TryParse(values[arrayNumero[i]].Trim(), out dec))
                            {
                                strMensaje += "- Columna Nro. " + arrayNumero[i] + " No es un número.";
                            }
                        }
                        if (values[0].Trim().Substring(6, 4) + values[0].Trim().Substring(3, 2) != cmbPeriodo.Value.ToString().Substring(0, 4) + cmbPeriodo.Value.ToString().Substring(4, 2))
                        {
                            strMensaje += "- Periodo incorrecto.";
                        }
                        
                        if (!DateTime.TryParse(values[0].Trim(), out dt))
                        {
                            strMensaje += "- Fecha incorrecta.";
                        }
                        if (MCodificaParametrica((int)EnumTipoParametrica.Producto, values[7].Trim()) == -99999)
                        {
                            strMensaje += "- Producto incorrecto.";
                        }
                        if (MCodificaParametrica((int)EnumTipoParametrica.UnidadMedida, values[7].Trim()) == -99999)
                        {
                            strMensaje += "- Unidad de medida invalida.";
                        }
                        if ((values[9].Trim() == "V" && Convert.ToDecimal(values[8]) <= 0) || (values[9].Trim() == "A" && Convert.ToDecimal(values[8]) < 0))
                        {
                            strMensaje += "- Cantidad incorrecta.";
                        }
                        if (Convert.ToDecimal(values[18].Trim()) > (Convert.ToDecimal(values[17].Trim()) * Convert.ToDecimal(values[8].Trim())) + 2 ||
                            Convert.ToDecimal(values[18].Trim()) < (Convert.ToDecimal(values[17].Trim()) * Convert.ToDecimal(values[8].Trim())) - 2)
                        {
                            strMensaje += "- Importe incorrecto.";
                        }
                        if (values[16].Trim().Length == 1 && values[16].Trim() != "0")
                        {
                            strMensaje += "- Código de control incorrecto.";
                        }
                        if (values[16].Trim().Length > 1 && values[16].Trim().Length < 6)
                        {
                            strMensaje += "- Código de control incorrecto.";
                        }
                        if (values[3].Trim().Length > 1)
                        {
                            if (values[3].Trim().ContainsAny("-", "@", "/", "'", "\\", ".", ",", "_", "+", "[", "]", "{", "}", " "))
                            {
                                strMensaje += "- Identificador contiene caracteres invalidos.";
                            }
                        }
                        //if (Convert.ToInt64(values[24]) > 0)
                        //{
                        //    if (Convert.ToDecimal(values[28].Trim()) > (Convert.ToDecimal(values[27].Trim()) * Convert.ToDecimal(values[8].Trim())) + 2 ||
                        //        Convert.ToDecimal(values[28].Trim()) < (Convert.ToDecimal(values[27].Trim()) * Convert.ToDecimal(values[8].Trim())) - 2)
                        //    {
                        //        strMensaje += "- Importe factura externa incorrecto.";
                        //    }
                        //    if (values[6].Trim() == "BOLIVIA")
                        //    {
                        //        strMensaje += "- País incorrecto.";
                        //    }
                        //    if (values[26].Trim().Length == 1 && values[26].Trim() != "0")
                        //    {
                        //        strMensaje += "- Código de control factura externa incorrecto.";
                        //    }
                        //    if (values[26].Trim().Length > 1 && values[26].Trim().Length < 6)
                        //    {
                        //        strMensaje += "- Código de control factura externa incorrecto.";
                        //    }
                        //}
                        if (Convert.ToDecimal(values[18]) > 0)
                        {
                            if (Convert.ToDecimal(values[18].Trim()) > (Convert.ToDecimal(values[17].Trim()) * Convert.ToDecimal(values[8].Trim())) + 2 ||
                                Convert.ToDecimal(values[18].Trim()) < (Convert.ToDecimal(values[17].Trim()) * Convert.ToDecimal(values[8].Trim())) - 2)
                            {
                                strMensaje += "- Importe vale incorrecto.";
                            }
                        }
                        //if (values[11].Trim() == "VAL")
                        //{
                            if (values[19].Trim() == "")
                            {
                                strMensaje += "- Número de documento incorrecto.";
                            }
                        //}                     
                        if (values[11].Trim() != "EF" && values[11].Trim() != "PRE" && values[11].Trim() != "VAL")
                        {
                            strMensaje += "- Forma de pago incorrecta.";
                        }
                       
                        if (values[2].Trim() != "NOID")
                        {
                            var lstInfraestructura = Session[CVariablesSesion.ListaInfraestructura] as List<O_INFRAESTRUCTURA_VOL_CTY>;
                            if (lstInfraestructura != null && lstInfraestructura.Any())
                            {
                                lstInfraestructura = lstInfraestructura.Where(x => x.CODIGO_OPERADOR == values[2].Trim() && x.TIPO == values[1].Trim()).ToList();
                                if (lstInfraestructura.Any())
                                {
                                    if (lstInfraestructura.FirstOrDefault().ID_PRODUCTO != MCodificaParametrica((int)EnumTipoParametrica.Producto, values[7].Trim()))
                                    {
                                        strMensaje += "- El producto no corresponde a la infraestructura receptora especificada.";
                                    }
                                }
                                else
                                {
                                    strMensaje += "- Infraestructura despachante invalida.";
                                }
                            }
                            else
                            {
                                strMensaje += "- Infraestructura despachante no registrada.";
                            }
                        }
                        if (Convert.ToDecimal(values[14]) == 0 && values[19].Trim().Length == 0)
                        {
                            strMensaje += "- No existe documento de respaldo.";
                        }
                        #endregion
                        break;
                }
            }
            catch (Exception ex)
            {
                strMensaje += ex.Message;
            }

            if (strMensaje.Trim().Length > 0)
            {
                booResultado = false;
            }
            else
            {
                //todo No implementado.
            }

            return booResultado;
        }

        /// <summary>
        /// METODO. MCargaGraco
        /// </summary>
        /// <param name="strRuta"></param>
        private void MCargaGraco(string strRuta)
        {
            var reader = new StreamReader(File.OpenRead(@strRuta));
            var lstRegistros = new List<CRegistrosImportados>();
            var lstRegistroE = new List<CRegistrosError>();
            long lngNoRegCorrect = 1, lngNoRegInCorrect = 1;
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(';');
                try
                {
                    DateTime datFecha, datFechaReg;
                    string strFecha;
                    ObjTransaccion objFactura, objDocumento;
                    ObjProtoMovimientoTransaccion objNuevoTipoReg = null;
                    int estadoTra;
                    List<ObjTransaccion> lstTransacciones;
                    var to = cmbTipoOperacion.Text.Split(';');
                    var strTipoOp = to[1].Substring(2, 2);

                    string strMensaje = string.Empty;
                    if (MValidaGraco(values, ref strMensaje))
                    {
                        var cDato = new CRegistrosImportados();
                        int controlBinario;
                        switch (strTipoOp)
                        {
                            case "DS":
                                #region Valida y genera objeto
                                cDato.LngId = lngNoRegCorrect;
                                cDato.StrIdVenta = lngNoRegCorrect.ToString();
                                datFecha = DateTime.Parse(values[0].Trim());
                                cDato.StrFechaEmision = datFecha.ToString();                                
                                cDato.StrEstado = values[7].Trim();
                                cDato.LngNit = Convert.ToInt64(values[12]);
                                cDato.StrRazonSocial = values[13].Trim().ToUpper().Replace("¥", "Ñ").Replace("Á", "A").Replace("É", "E").Replace("Í", "I").Replace("Ó", "O").Replace("Ú", "U").Replace("'", "").Replace("�", "Ñ");
                                cDato.StrRazonSocial = Regex.Replace(cDato.StrRazonSocial, @"\s+", " ");
                                cDato.StrIdentificador = values[3].Trim().Replace("-", "").Replace(" ", "");
                                cDato.LngNroFactura = Convert.ToInt64(values[14]);                                                                
                                cDato.LngNroAutorizacion = esnumero(values[15].Trim())==true ? Convert.ToInt64(values[15]) : 0; //adicion par el CUF;
                                cDato.strCodigoAutorizacion = esnumero(values[15].Trim()) == true ? "" : values[15].Trim().ToUpper();
                                cDato.StrNroReferencia = values[19].Trim();
                                cDato.LngNroAutVenta = 0;
                                cDato.StrProducto = values[7].Trim();
                                cDato.DecVolumen = Convert.ToDecimal(values[8]);
                                cDato.DecPrecio = Convert.ToDecimal(values[17]);
                                cDato.DecImporte = Convert.ToDecimal(values[18]);
                                cDato.DecIdTipoCreditoFiscal = (int)EnumTipoCredFiscal.Si;//(Convert.ToDecimal(values[14]) > 0 && Convert.ToDecimal(values[17]) > 0) ? (int)EnumTipoCredFiscal.Si : (int)EnumTipoCredFiscal.No;
                                cDato.objProtoMovimiento = new ObjProtoMovimientoTransaccion();
                                cDato.DecIdTipoOp = Convert.ToDecimal(cmbTipoOperacion.Value);
                                cDato.DecOrden = 1;

                                strFecha = datFecha.Year + datFecha.Month.ToString().PadLeft(2, '0') + datFecha.Day.ToString().PadLeft(2, '0') + datFecha.Hour.ToString().PadLeft(2, '0') + datFecha.Minute.ToString().PadLeft(2, '0') + datFecha.Second.ToString().PadLeft(2, '0');
                                datFechaReg = DateTime.Now;
                                estadoTra = (cDato.StrEstado == "A") ? (int)EnumEstadoMov.Anulado : (int)EnumEstadoMov.Activo;
                                lstTransacciones = new List<ObjTransaccion>();
                                var intFormaPago = (values[11].Trim() == "VAL") ? 3 : (values[10].Trim() == "PRE") ? 2 : 1;
                                controlBinario = 1;
                                                                
                                if (Convert.ToDecimal(values[14].Trim()) > 0 && values[15].Trim().Length > 0)
                                {
                                    objFactura = new ObjTransaccion();
                                    objFactura.idMovimiento = 0;
                                    objFactura.idTransaccion = 0;
                                    objFactura.idTransaccionPadre = 0;
                                    objFactura.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                    objFactura.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                    //objFactura.idTipoTransaccion = (decimal)((values[16].Trim().Length > 0) ? EnumTipoTransaccion.FacturaComputarizada : EnumTipoTransaccion.FacturaManual);
                                    objFactura.fechaEmision = Convert.ToInt64(strFecha);
                                    objFactura.numeroTransaccion = values[14].Trim();                                    
                                    objFactura.numeroAutorizacion = esnumero(values[15].Trim()) == true ? Convert.ToDecimal(values[15]) : 0;//Convert.ToDecimal(values[15]);
                                    objFactura.codigoAutorizacion = esnumero(values[15].Trim()) == true ? "" : values[15].Trim();
                                    objFactura.idTipoTransaccion = (decimal)((objFactura.codigoAutorizacion.Length > 25 || values[16].Trim().Length > 0) ? EnumTipoTransaccion.FacturaComputarizada : EnumTipoTransaccion.FacturaManual);
                                    objFactura.codigoControl = values[16].Trim();
                                    objFactura.nitConsumidor = Convert.ToDecimal(values[12].Trim());
                                    objFactura.razonSocial = cDato.StrRazonSocial;
                                    objFactura.precioUnitario = Convert.ToDecimal(values[17].Trim());
                                    objFactura.precioTotal = Convert.ToDecimal(values[18].Trim());
                                    objFactura.usuarioTransaccion = values[10].Trim();
                                    objFactura.observaciones = "";
                                    objFactura.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                    objFactura.idEstadoTransaccion = estadoTra;
                                    objFactura.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.Si;
                                    objFactura.idFormaPago = intFormaPago;
                                    objFactura.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                    objFactura.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                    objFactura.codigoSeguridad = 0;
                                    objFactura.idAutorizacionDato = 0;
                                    objFactura.binarioControl = controlBinario + "000000000";
                                    objFactura.fechaOperacion = Convert.ToInt64(strFecha);
                                    objFactura.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                    objFactura.codigo = "";

                                    lstTransacciones.Add(objFactura);
                                    controlBinario = 0;
                                }                                                               
                             
                                if (values[19].Trim().Length > 0)
                                {
                                    objDocumento = new ObjTransaccion();
                                    objDocumento.idMovimiento = 0;
                                    objDocumento.idTransaccion = 0;
                                    objDocumento.idTransaccionPadre = 0;
                                    objDocumento.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                    objDocumento.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                    objDocumento.idTipoTransaccion = (decimal)EnumTipoTransaccion.NotaMovimiento;
                                    objDocumento.fechaEmision = Convert.ToInt64(strFecha);
                                    objDocumento.numeroTransaccion = values[19].Trim();
                                    objDocumento.numeroAutorizacion = 0;
                                    objDocumento.codigoControl = "";
                                    objDocumento.nitConsumidor = 0;
                                    objDocumento.razonSocial = "";
                                    objDocumento.precioUnitario = 0;
                                    objDocumento.precioTotal = 0;
                                    objDocumento.usuarioTransaccion = values[10].Trim();
                                    objDocumento.observaciones = "";
                                    objDocumento.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                    objDocumento.idEstadoTransaccion = estadoTra;
                                    objDocumento.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.No;
                                    objDocumento.idFormaPago = intFormaPago;
                                    objDocumento.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                    objDocumento.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                    objDocumento.codigoSeguridad = 0;
                                    objDocumento.idAutorizacionDato = 0;
                                    objDocumento.binarioControl = controlBinario + "000000000";
                                    objDocumento.fechaOperacion = Convert.ToInt64(strFecha);
                                    objDocumento.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                    objDocumento.codigo = "";
                                    lstTransacciones.Add(objDocumento);
                                }
                                
                                if (Convert.ToDecimal(values[14].Trim()) > 0 && Convert.ToDecimal(values[17].Trim()) == 0)
                                {
                                    objDocumento = new ObjTransaccion();
                                    objDocumento.idMovimiento = 0;
                                    objDocumento.idTransaccion = 0;
                                    objDocumento.idTransaccionPadre = 0;
                                    objDocumento.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                    objDocumento.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                    objDocumento.idTipoTransaccion = (decimal)EnumTipoTransaccion.DocFactRef;
                                    objDocumento.fechaEmision = Convert.ToInt64(strFecha);
                                    objDocumento.numeroTransaccion = values[14].Trim();
                                    objDocumento.numeroAutorizacion = Convert.ToDecimal(values[15]);
                                    objDocumento.codigoControl = "";
                                    objDocumento.nitConsumidor = Convert.ToDecimal(values[12].Trim());
                                    objDocumento.razonSocial = cDato.StrRazonSocial;
                                    objDocumento.precioUnitario = 0;
                                    objDocumento.precioTotal = 0;
                                    objDocumento.usuarioTransaccion = values[10].Trim();
                                    objDocumento.observaciones = "";
                                    objDocumento.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                    objDocumento.idEstadoTransaccion = estadoTra;
                                    objDocumento.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.No;
                                    objDocumento.idFormaPago = intFormaPago;
                                    objDocumento.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                    objDocumento.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                    objDocumento.codigoSeguridad = 0;
                                    objDocumento.idAutorizacionDato = 0;
                                    objDocumento.binarioControl = "0000000000";
                                    objDocumento.fechaOperacion = Convert.ToInt64(strFecha);
                                    objDocumento.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                    objDocumento.codigo = "";
                                    lstTransacciones.Add(objDocumento);
                                }

                                objNuevoTipoReg = new ObjProtoMovimientoTransaccion();
                                objNuevoTipoReg.idMovimientoPadre = 0;
                                objNuevoTipoReg.idTipoActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                objNuevoTipoReg.idTipoOperacion = Convert.ToDecimal(cmbTipoOperacion.Value);
                                objNuevoTipoReg.idConsumidor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString());
                                objNuevoTipoReg.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                objNuevoTipoReg.fechaRegistroMovimiento = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                objNuevoTipoReg.fechaOperacion = Convert.ToInt64(strFecha);
                                objNuevoTipoReg.idTipoMovimientoFlujo = (values[11].Trim() == "PRE") ? (int)EnumTipoMovimientoFlujo.Contable : (int)EnumTipoMovimientoFlujo.Fisico;
                                objNuevoTipoReg.idTipoMovimiento = (cDato.StrTipoMov == "NAC") ? (int)EnumTipoMovimiento.NacNac : (cDato.StrTipoMov == "SIVETUR") ? (int)EnumTipoMovimiento.SIVETUR : (int)EnumTipoMovimiento.NacInt;
                                objNuevoTipoReg.idEstadoMovimiento = estadoTra;
                                objNuevoTipoReg.idFuenteOrigenDato = (decimal)EnumTipoRegistro.OrigenCsv;
                                objNuevoTipoReg.idUbicacionOperacion = MCodificaParametrica((int)EnumTipoParametrica.Pais, values[6].Trim());
                                objNuevoTipoReg.idUnidadMedidaHidrocarburo = MCodificaParametrica((int)EnumTipoParametrica.UnidadMedida, values[7].Trim());
                                objNuevoTipoReg.idHidrocarburo = MCodificaParametrica((int)EnumTipoParametrica.Producto, values[7].Trim());
                                objNuevoTipoReg.cantidadHidrocarburo = cDato.DecVolumen;
                                objNuevoTipoReg.identificador = cDato.StrIdentificador;
                                objNuevoTipoReg.subIdentificador = values[4].Trim();
                                objNuevoTipoReg.observacion = "";

                                if (to[1].Substring(4, 2) == "EX")
                                {
                                    objNuevoTipoReg.idTipoMedioTransporte = MCodificaParametrica((int)EnumTipoParametrica.TipoInfraestructura, values[1].Trim());
                                    objNuevoTipoReg.idMedioTransporte = MCodificaParametrica((int)EnumTipoParametrica.Infraestructura, values[1].Trim() + "|" + values[2].Trim());
                                    objNuevoTipoReg.tipoInfraestructura = -99999;
                                    objNuevoTipoReg.idInfraestructura = -99999;
                                }
                                else
                                {
                                    objNuevoTipoReg.idTipoMedioTransporte = -99999;
                                    objNuevoTipoReg.idMedioTransporte = -99999;
                                    objNuevoTipoReg.tipoInfraestructura = MCodificaParametrica((int)EnumTipoParametrica.TipoInfraestructura, values[1].Trim());
                                    objNuevoTipoReg.idInfraestructura = MCodificaParametrica((int)EnumTipoParametrica.Infraestructura, values[1].Trim() + "|" + values[2].Trim());
                                }

                                objNuevoTipoReg.idUnidadEnvasesTransporte = MCodificaParametrica((int)EnumTipoParametrica.TipoContenedor, values[3].Trim());
                                objNuevoTipoReg.idEntidad = -99999;
                                objNuevoTipoReg.idTipoMedicion = -99999;
                                objNuevoTipoReg.cantidadAguaLitros = 0;
                                objNuevoTipoReg.idUnidadTemperatura = -99999;
                                objNuevoTipoReg.temperatura = 0;
                                objNuevoTipoReg.gradoApi = "";
                                objNuevoTipoReg.cantidadEnvasesTransporte = 0;
                                objNuevoTipoReg.binarioControl = "00000-0000000000-0000000000-00000";
                                objNuevoTipoReg.idIntegracion = -99999;
                                objNuevoTipoReg.idTransaccionBaseExterna = "";
                                objNuevoTipoReg.fechaOperacionDia = 0;
                                objNuevoTipoReg.codigo = "";
                                objNuevoTipoReg.barcode = Convert.ToDecimal(values[5].Trim());
                                objNuevoTipoReg.listaTransaccion = JsonSerializer.SerializeToString(lstTransacciones);

                                objNuevoTipoReg.jsonReferencia = "";
                                objNuevoTipoReg.estadoJsonRef = -99999;
                                objNuevoTipoReg.idCuenta = -99999;
                                objNuevoTipoReg.idCuentaAux = -99999;
                                objNuevoTipoReg.idParamGlobal = -99999;
                                objNuevoTipoReg.idParamEspec = -99999;
                                objNuevoTipoReg.idTipoConsumidor = (objNuevoTipoReg.idUnidadEnvasesTransporte == -99999) ? 32 : 31;
                                objNuevoTipoReg.idTipoInfraAux = -99999;
                                objNuevoTipoReg.idInfraAux = -99999;

                                #endregion
                                break;
                            case "RC":
                                #region Valida y genera objeto
                                cDato.LngId = lngNoRegCorrect;
                                cDato.StrIdVenta = lngNoRegCorrect.ToString();
                                datFecha = DateTime.Parse(values[0].Trim());
                                cDato.StrFechaEmision = datFecha.ToString();
                                cDato.StrTipoMov = "NAC";
                                cDato.StrEstado = values[11].Trim();
                                cDato.LngNit = Convert.ToInt64(values[13]);
                                cDato.StrRazonSocial = values[14].Trim().ToUpper().Replace("¥", "Ñ").Replace("Á", "A").Replace("É", "E").Replace("Í", "I").Replace("Ó", "O").Replace("Ú", "U").Replace("'", "").Replace("�", "Ñ");
                                cDato.StrRazonSocial = Regex.Replace(cDato.StrRazonSocial, @"\s+", " ");
                                cDato.StrIdentificador = values[4].Trim().Replace("-", "").Replace(" ", "");
                                cDato.LngNroFactura = Convert.ToInt64(values[16]);                            
                                cDato.LngNroAutorizacion = esnumero(values[17].Trim())==true ? Convert.ToInt64(values[17]) : 0; //Convert.ToInt64(values[17]);
                                cDato.strCodigoAutorizacion = esnumero(values[17].Trim()) == true ? "" : values[17].Trim();
                                cDato.StrNroReferencia = values[21].Trim();
                                cDato.LngNroAutVenta = Convert.ToInt64(values[24]);
                                cDato.StrProducto = values[9].Trim();
                                cDato.DecVolumen = Convert.ToDecimal(values[10]);
                                cDato.DecPrecio = Convert.ToDecimal(values[19]);
                                cDato.DecImporte = Convert.ToDecimal(values[20]);
                                cDato.DecIdTipoCreditoFiscal = (Convert.ToDecimal(values[16]) > 0 && Convert.ToDecimal(values[19]) > 0) ? (int)EnumTipoCredFiscal.Si : (int)EnumTipoCredFiscal.No;
                                cDato.objProtoMovimiento = new ObjProtoMovimientoTransaccion();
                                cDato.DecIdTipoOp = Convert.ToDecimal(cmbTipoOperacion.Value);
                                cDato.DecOrden = 1;

                                strFecha = datFecha.Year + datFecha.Month.ToString().PadLeft(2, '0') + datFecha.Day.ToString().PadLeft(2, '0') + datFecha.Hour.ToString().PadLeft(2, '0') + datFecha.Minute.ToString().PadLeft(2, '0') + datFecha.Second.ToString().PadLeft(2, '0');
                                datFechaReg = DateTime.Now;
                                estadoTra = (cDato.StrEstado == "A") ? (int)EnumEstadoMov.Anulado : (int)EnumEstadoMov.Activo;
                                lstTransacciones = new List<ObjTransaccion>();
                                controlBinario = 1;

                                //if (Convert.ToDecimal(values[16].Trim()) > 0 && Convert.ToDecimal(values[17].Trim()) > 0)
                                if (Convert.ToDecimal(values[16].Trim()) > 0 && values[17].Trim().Length > 0)
                                {
                                    objFactura = new ObjTransaccion();
                                    objFactura.idMovimiento = 0;
                                    objFactura.idTransaccion = 0;
                                    objFactura.idTransaccionPadre = 0;
                                    objFactura.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                    objFactura.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                    //objFactura.idTipoTransaccion = (decimal)((values[18].Trim().Length > 0) ? EnumTipoTransaccion.FacturaComputarizada : EnumTipoTransaccion.FacturaManual);
                                    objFactura.fechaEmision = CFechas.ConvierteDateTimeLong(DateTime.Parse(values[15].Trim()));
                                    objFactura.numeroTransaccion = values[16].Trim();
                                    objFactura.numeroAutorizacion = esnumero(values[17].Trim()) == true ? Convert.ToDecimal(values[17]) : 0;//Convert.ToDecimal(values[17]);
                                    objFactura.codigoAutorizacion = esnumero(values[17].Trim()) == true ? "" : values[17].Trim();
                                    objFactura.idTipoTransaccion = (decimal)((objFactura.codigoAutorizacion.Length > 25 || values[18].Trim().Length > 0) ? EnumTipoTransaccion.FacturaComputarizada : EnumTipoTransaccion.FacturaManual);
                                    objFactura.codigoControl = values[18].Trim();
                                    objFactura.nitConsumidor = Convert.ToDecimal(values[13].Trim());
                                    objFactura.razonSocial = cDato.StrRazonSocial;
                                    objFactura.precioUnitario = Convert.ToDecimal(values[19].Trim());
                                    objFactura.precioTotal = Convert.ToDecimal(values[20].Trim());
                                    objFactura.usuarioTransaccion = values[12].Trim();
                                    objFactura.observaciones = "";
                                    objFactura.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                    objFactura.idEstadoTransaccion = estadoTra;
                                    objFactura.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.Si;
                                    objFactura.idFormaPago = (decimal)EnumTiposPago.Efectivo;
                                    objFactura.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                    objFactura.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                    objFactura.codigoSeguridad = 0;
                                    objFactura.idAutorizacionDato = 0;
                                    objFactura.binarioControl = controlBinario + "000000000";
                                    objFactura.fechaOperacion = Convert.ToInt64(strFecha);
                                    objFactura.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                    objFactura.codigo = "";
                                    lstTransacciones.Add(objFactura);
                                    controlBinario = 0;
                                }
                                if (values[25].Trim().Length > 0 && values[26].Trim().Length > 0)
                                {
                                    objDocumento = new ObjTransaccion();
                                    objDocumento.idMovimiento = 0;
                                    objDocumento.idTransaccion = 0;
                                    objDocumento.idTransaccionPadre = 0;
                                    objDocumento.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                    objDocumento.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                    objDocumento.idTipoTransaccion = (decimal)EnumTipoTransaccion.DocDespacho;

                                    var datFechaDes = DateTime.Parse(values[25].Trim());
                                    var strFechaDes = datFechaDes.Year + datFechaDes.Month.ToString().PadLeft(2, '0') + datFechaDes.Day.ToString().PadLeft(2, '0') + datFechaDes.Hour.ToString().PadLeft(2, '0') + datFechaDes.Minute.ToString().PadLeft(2, '0') + datFechaDes.Second.ToString().PadLeft(2, '0');
                                    objDocumento.fechaEmision = Convert.ToInt64(strFechaDes);

                                    objDocumento.numeroTransaccion = values[26].Trim();
                                    objDocumento.numeroAutorizacion = Convert.ToDecimal(values[27].Trim());
                                    objDocumento.codigoControl = values[28].Trim();
                                    objDocumento.nitConsumidor = 0;
                                    objDocumento.razonSocial = "";
                                    objDocumento.precioUnitario = 0;
                                    objDocumento.precioTotal = 0;
                                    objDocumento.usuarioTransaccion = values[12].Trim();
                                    objDocumento.observaciones = "";
                                    objDocumento.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                    objDocumento.idEstadoTransaccion = estadoTra;
                                    objDocumento.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.No;
                                    objDocumento.idFormaPago = (decimal)EnumTiposPago.Efectivo;
                                    objDocumento.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                    objDocumento.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                    objDocumento.codigoSeguridad = 0;
                                    objDocumento.idAutorizacionDato = 0;
                                    objDocumento.binarioControl = controlBinario + "000000000";
                                    objDocumento.fechaOperacion = Convert.ToInt64(strFecha);
                                    objDocumento.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                    objDocumento.codigo = values[29].Trim();
                                    lstTransacciones.Add(objDocumento);
                                    controlBinario = 0;
                                }
                                if (values[21].Trim().Length > 0)
                                {
                                    objDocumento = new ObjTransaccion();
                                    objDocumento.idMovimiento = 0;
                                    objDocumento.idTransaccion = 0;
                                    objDocumento.idTransaccionPadre = 0;
                                    objDocumento.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                    objDocumento.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                    objDocumento.idTipoTransaccion = (decimal)EnumTipoTransaccion.NotaMovimiento;
                                    objDocumento.fechaEmision = Convert.ToInt64(strFecha);
                                    objDocumento.numeroTransaccion = values[21].Trim();
                                    objDocumento.numeroAutorizacion = 0;
                                    objDocumento.codigoControl = "";
                                    objDocumento.nitConsumidor = Convert.ToDecimal(values[23].Trim());
                                    objDocumento.razonSocial = values[22].Trim();
                                    objDocumento.precioUnitario = 0;
                                    objDocumento.precioTotal = 0;
                                    objDocumento.usuarioTransaccion = values[12].Trim();
                                    objDocumento.observaciones = "";
                                    objDocumento.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                    objDocumento.idEstadoTransaccion = estadoTra;
                                    objDocumento.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.No;
                                    objDocumento.idFormaPago = (decimal)EnumTiposPago.Efectivo;
                                    objDocumento.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                    objDocumento.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                    objDocumento.codigoSeguridad = 0;
                                    objDocumento.idAutorizacionDato = 0;
                                    objDocumento.binarioControl = controlBinario + "000000000";
                                    objDocumento.fechaOperacion = Convert.ToInt64(strFecha);
                                    objDocumento.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                    objDocumento.codigo = "";
                                    lstTransacciones.Add(objDocumento);
                                }

                                if (Convert.ToDecimal(values[24].Trim()) > 0)
                                {
                                    objDocumento = new ObjTransaccion();
                                    objDocumento.idMovimiento = 0;
                                    objDocumento.idTransaccion = 0;
                                    objDocumento.idTransaccionPadre = 0;
                                    objDocumento.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                    objDocumento.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                    objDocumento.idTipoTransaccion = (decimal)EnumTipoTransaccion.AutSustCtrl;
                                    objDocumento.fechaEmision = Convert.ToInt64(strFecha);
                                    objDocumento.numeroTransaccion = values[24].Trim();
                                    objDocumento.numeroAutorizacion = 0;
                                    objDocumento.codigoControl = "";
                                    objDocumento.nitConsumidor = 0;
                                    objDocumento.razonSocial = "";
                                    objDocumento.precioUnitario = 0;
                                    objDocumento.precioTotal = 0;
                                    objDocumento.usuarioTransaccion = values[12].Trim();
                                    objDocumento.observaciones = "AUTORIZACION DE COMPRA LOCAL";
                                    objDocumento.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                    objDocumento.idEstadoTransaccion = estadoTra;
                                    objDocumento.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.No;
                                    objDocumento.idFormaPago = (decimal)EnumTiposPago.Efectivo;
                                    objDocumento.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                    objDocumento.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                    objDocumento.codigoSeguridad = 0;
                                    objDocumento.idAutorizacionDato = 0;
                                    objDocumento.binarioControl = "0000000000";
                                    objDocumento.fechaOperacion = Convert.ToInt64(strFecha);
                                    objDocumento.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                    objDocumento.codigo = "";
                                    lstTransacciones.Add(objDocumento);
                                }

                                if (values[8].Trim().Length > 0)
                                {
                                    var arreglo = values[8].Trim().Split('|');
                                    for (int i = 0; i < arreglo.Count(); i++)
                                    {
                                        objDocumento = new ObjTransaccion();
                                        objDocumento.idMovimiento = 0;
                                        objDocumento.idTransaccion = 0;
                                        objDocumento.idTransaccionPadre = 0;
                                        objDocumento.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                        objDocumento.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                        objDocumento.idTipoTransaccion = (decimal)EnumTipoTransaccion.DocPrecinto;
                                        objDocumento.fechaEmision = Convert.ToInt64(strFecha);
                                        objDocumento.numeroTransaccion = arreglo[i];
                                        objDocumento.numeroAutorizacion = 0;
                                        objDocumento.codigoControl = "";
                                        objDocumento.nitConsumidor = 0;
                                        objDocumento.razonSocial = "";
                                        objDocumento.precioUnitario = 0;
                                        objDocumento.precioTotal = 0;
                                        objDocumento.usuarioTransaccion = values[12].Trim();
                                        objDocumento.observaciones = "";
                                        objDocumento.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                        objDocumento.idEstadoTransaccion = estadoTra;
                                        objDocumento.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.No;
                                        objDocumento.idFormaPago = (int)EnumTiposPago.Efectivo;
                                        objDocumento.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                                        objDocumento.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                                        objDocumento.codigoSeguridad = 0;
                                        objDocumento.idAutorizacionDato = 0;
                                        objDocumento.binarioControl = "0000000000";
                                        objDocumento.fechaOperacion = Convert.ToInt64(strFecha);
                                        objDocumento.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                        objDocumento.codigo = "";
                                        lstTransacciones.Add(objDocumento);
                                    }
                                }

                                objNuevoTipoReg = new ObjProtoMovimientoTransaccion();
                                objNuevoTipoReg.idMovimientoPadre = 0;
                                objNuevoTipoReg.idTipoActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                objNuevoTipoReg.idTipoOperacion = Convert.ToDecimal(cmbTipoOperacion.Value);
                                objNuevoTipoReg.idConsumidor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString());
                                objNuevoTipoReg.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                objNuevoTipoReg.fechaRegistroMovimiento = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString().PadLeft(2, '0') + datFechaReg.Minute.ToString().PadLeft(2, '0') + datFechaReg.Second.ToString().PadLeft(2, '0'));
                                objNuevoTipoReg.fechaOperacion = Convert.ToInt64(strFecha);
                                objNuevoTipoReg.idTipoMovimientoFlujo = (int)EnumTipoMovimientoFlujo.Fisico;
                                objNuevoTipoReg.idTipoMovimiento = (int)EnumTipoMovimiento.NacNac;
                                objNuevoTipoReg.idEstadoMovimiento = (cDato.StrEstado == "A") ? (int)EnumEstadoMov.Anulado : (int)EnumEstadoMov.Activo;
                                objNuevoTipoReg.idFuenteOrigenDato = (decimal)EnumTipoRegistro.OrigenCsv;
                                objNuevoTipoReg.idUbicacionOperacion = MCodificaParametrica((int)EnumTipoParametrica.Pais, "BOLIVIA");
                                objNuevoTipoReg.idUnidadMedidaHidrocarburo = MCodificaParametrica((int)EnumTipoParametrica.UnidadMedida, values[9].Trim());
                                objNuevoTipoReg.idHidrocarburo = MCodificaParametrica((int)EnumTipoParametrica.Producto, values[9].Trim());
                                objNuevoTipoReg.cantidadHidrocarburo = cDato.DecVolumen;
                                objNuevoTipoReg.identificador = cDato.StrIdentificador;
                                objNuevoTipoReg.subIdentificador = values[5].Trim();
                                objNuevoTipoReg.observacion = "";
                                objNuevoTipoReg.tipoInfraestructura = MCodificaParametrica((int)EnumTipoParametrica.TipoInfraestructura, values[2].Trim());
                                objNuevoTipoReg.idInfraestructura = MCodificaParametrica((int)EnumTipoParametrica.Infraestructura, values[2].Trim() + "|" + values[3].Trim());
                                objNuevoTipoReg.idTipoMedioTransporte = (values[7].Trim().Length == 0) ? -99999 : MCodificaParametrica((int)EnumTipoParametrica.TipoInfraestructura, "CS");
                                objNuevoTipoReg.idMedioTransporte = -99999;
                                objNuevoTipoReg.idUnidadEnvasesTransporte = -99999;
                                objNuevoTipoReg.idEntidad = MCodificaParametrica((int)EnumTipoParametrica.Entidad, values[1].Trim());
                                objNuevoTipoReg.codigo = values[7].Trim();
                                objNuevoTipoReg.idTipoMedicion = -99999;
                                objNuevoTipoReg.cantidadAguaLitros = 0;
                                objNuevoTipoReg.idUnidadTemperatura = -99999;
                                objNuevoTipoReg.temperatura = 0;
                                objNuevoTipoReg.gradoApi = "";
                                objNuevoTipoReg.cantidadEnvasesTransporte = 0;
                                objNuevoTipoReg.binarioControl = "00000-0000000000-0000000000-00000";
                                objNuevoTipoReg.idIntegracion = -99999;
                                objNuevoTipoReg.idTransaccionBaseExterna = "";
                                objNuevoTipoReg.fechaOperacionDia = 0;
                                objNuevoTipoReg.codigo = "";
                                objNuevoTipoReg.barcode = Convert.ToDecimal(values[6].Trim());
                                objNuevoTipoReg.listaTransaccion = JsonSerializer.SerializeToString(lstTransacciones);

                                objNuevoTipoReg.jsonReferencia = "";
                                objNuevoTipoReg.estadoJsonRef = -99999;
                                objNuevoTipoReg.idCuenta = -99999;
                                objNuevoTipoReg.idCuentaAux = -99999;
                                objNuevoTipoReg.idParamGlobal = -99999;
                                objNuevoTipoReg.idParamEspec = -99999;
                                objNuevoTipoReg.idTipoConsumidor = -99999;
                                objNuevoTipoReg.idTipoInfraAux = -99999;
                                objNuevoTipoReg.idInfraAux = -99999;

                                #endregion
                                break;
                        }
                        cDato.objProtoMovimiento = objNuevoTipoReg;
                        lstRegistros.Add(cDato);
                        lngNoRegCorrect++;
                    }
                    else
                    {
                        if (strTipoOp == "RC")
                        {
                            var cRegError = new CRegistrosError();
                            cRegError.LngId = lngNoRegInCorrect;
                            cRegError.StrIdVenta = lngNoRegCorrect.ToString(); ;
                            cRegError.LngFechaEmision = values[0].Trim();
                            cRegError.LngNit = values[13].Trim();;
                            cRegError.LngNroAutorizacion = values[17].Trim();;
                            cRegError.LngNroFactura = values[16].Trim();;
                            cRegError.LngNroReferencia = values[4].Trim();
                            cRegError.StrErrores = strMensaje.Trim();
                            lstRegistroE.Add(cRegError);
                            lngNoRegInCorrect++;
                        }
                        else
                        {
                            var cRegError = new CRegistrosError();
                            cRegError.LngId = lngNoRegInCorrect;
                            cRegError.StrIdVenta = lngNoRegCorrect.ToString(); ;
                            cRegError.LngFechaEmision = values[0].Trim();
                            cRegError.LngNit = values[12].Trim();
                            cRegError.LngNroAutorizacion = "";
                            cRegError.LngNroFactura = values[14].Trim();
                            cRegError.LngNroReferencia = values[19].Trim();
                            cRegError.StrErrores = strMensaje.Trim();
                            lstRegistroE.Add(cRegError);
                            lngNoRegInCorrect++;
                        }
                    }
                }
                catch (Exception ex)
                {
                    var cRegError = new CRegistrosError();
                    cRegError.LngId = lngNoRegInCorrect;
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
            }

            //var ojo2 = JsonSerializer.SerializeToString(lstRegistros);
            dgvTransacciones.DataSource = lstRegistros;
            dgvTransacciones.DataBind();
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
            var lngResultado = -99999;

            var objParametricas = Session[CVariablesSesion.ListaParametrica] as O_PARAMETRICAS_GRAL;
            var objParamEspecifica = Session[CVariablesSesion.ListaParamGral] as O_PARAMETRICAS_ACT;
            var objGlobClas1 = Session[CVariablesSesion.ListaCatalogo] as List<O_PARAMETRICA_GLOBAL_CTY>;
            var lstInfraestructura = Session[CVariablesSesion.ListaInfraestructura] as List<O_INFRAESTRUCTURA_VOL_CTY>;
            var lstEntidad = Session[CVariablesSesion.ListaEntidad] as List<O_OBTIENE_ENTIDADES_CTY>;
            var objParamInf = Session[CVariablesSesion.ListaParamInfra] as O_PARAMETROS_INFRA;

            var objParamPoiRecepcion = Session[CVariablesSesion.ListaPoiRecepcion] as List<O_PARAMETRICA_GLOBAL_CTY>;
            var objParamPoiEntrega = Session[CVariablesSesion.ListaPoiEntrega] as List<O_PARAMETRICA_GLOBAL_CTY>;
            var objParamSistemaPoi = Session[CVariablesSesion.ListaSistemaDucto] as List<O_PARAMETRICA_GLOBAL_CTY>;
            var objParamTransporteDucto = Session[CVariablesSesion.ListaTipoTransporte] as List<O_PARAMETRICA_GLOBAL_CTY>;
            var objParamTipoConsumidor = Session[CVariablesSesion.ListaTipoConsumidor] as List<O_PARAMETRICA_GLOBAL_CTY>;

            var lstPoiDucto = Session[CVariablesSesion.ListaPoi] as List<O_EQUIPO_RD_CTY>;

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
                    }
                    break;
                case (int)EnumTipoParametrica.Infraestructura:
                    var arreglo = strDescripcion.Split('|');
                    if (lstInfraestructura != null && lstInfraestructura.Any())
                    {
                        lstInfraestructura = lstInfraestructura.Where(x => x.TIPO == arreglo[0] && x.CODIGO_OPERADOR == arreglo[1]).ToList();
                        if (lstInfraestructura.Any())
                        {
                            lngResultado = Convert.ToInt32(lstInfraestructura.FirstOrDefault().ID_INFRAESTRUCTURA);
                        }
                    }
                    break;
                case (int)EnumTipoParametrica.Entidad:
                    if (strDescripcion == "INTERNO")
                    {
                        lngResultado = Convert.ToInt32(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString());
                    }
                    else if (lstEntidad != null && lstEntidad.Any())
                    {
                        strDescripcion = strDescripcion.Replace("\"", "");
                        //lstEntidad = lstEntidad.Where(x => x.DENOMINACION == strDescripcion || x.USUARIO_ASIGNADO == strDescripcion).ToList();
                        lstEntidad = lstEntidad.Where(x => x.DENOMINACION.Replace("\"", "") == strDescripcion).ToList();
                        //lstEntidad = lstEntidad.Where(x => x.DENOMINACION.Contains(strDescripcion) || x.USUARIO_ASIGNADO.Contains(strDescripcion)).ToList();
                        if (lstEntidad.Any())
                        {
                            lngResultado = Convert.ToInt32(lstEntidad.FirstOrDefault().ID_CONSUMIDOR);
                        }
                    }
                    break;
                case (int)EnumTipoParametrica.Cuenta:
                    if (objParamEspecifica.lstCuentas != null && objParamEspecifica.lstCuentas.Any())
                    {
                        var objHidro = objParamEspecifica.lstCuentas;
                        objHidro = objHidro.Where(x => x.CODIGO == strDescripcion).ToList();
                        if (objHidro.Any())
                        {
                            lngResultado = Convert.ToInt32(objHidro.FirstOrDefault().ID_AUTORIZACION_CONDICION);
                        }
                    }
                    break;
                case (int)EnumTipoParametrica.GlobalClasif1:
                    if (objGlobClas1 != null && objGlobClas1.Any())
                    {
                        var lstParamGlb = objGlobClas1.Where(x => x.CODIGO == strDescripcion).ToList();
                        if (lstParamGlb != null && lstParamGlb.Any())
                        {
                            lngResultado = Convert.ToInt32(lstParamGlb.FirstOrDefault().ID_PARAMETRICA_GLOBAL);
                        }
                    }
                    break;

                case (int)EnumTipoParametrica.PoiRecepcion:
                    if (objParamPoiRecepcion != null && objParamPoiRecepcion.Any())
                    {
                        var lstParamPr = objParamPoiRecepcion.Where(x => x.CODIGO == strDescripcion).ToList();
                        if (lstParamPr != null && lstParamPr.Any())
                        {
                            lngResultado = Convert.ToInt32(lstParamPr.FirstOrDefault().ID_PARAMETRICA_GLOBAL);
                        }
                    }
                    break;

                case (int)EnumTipoParametrica.PoiEntrega:
                    if (objParamPoiEntrega != null && objParamPoiEntrega.Any())
                    {
                        var lstParamPe = objParamPoiEntrega.Where(x => x.CODIGO == strDescripcion).ToList();
                        if (lstParamPe != null && lstParamPe.Any())
                        {
                            lngResultado = Convert.ToInt32(lstParamPe.FirstOrDefault().ID_PARAMETRICA_GLOBAL);
                        }
                    }
                    break;

                case (int)EnumTipoParametrica.SistemaDucto:
                    if (objParamSistemaPoi != null && objParamSistemaPoi.Any())
                    {
                        var lstParamPe = objParamSistemaPoi.Where(x => x.CODIGO == strDescripcion).ToList();
                        if (lstParamPe != null && lstParamPe.Any())
                        {
                            lngResultado = Convert.ToInt32(lstParamPe.FirstOrDefault().ID_PARAMETRICA_GLOBAL);
                        }
                    }
                    break;

                case (int)EnumTipoParametrica.CodigoPoi:
                    if (lstPoiDucto != null && lstPoiDucto.Any())
                    {
                        var lstParamPe = lstPoiDucto.Where(x => x.CODIGO == strDescripcion).ToList();
                        if (lstParamPe != null && lstParamPe.Any())
                        {
                            lngResultado = Convert.ToInt32(lstParamPe.FirstOrDefault().ID_EQUIPO_RD);
                        }
                    }
                    break;

                case (int)EnumTipoParametrica.TransporteDucto:
                    if (objParamTransporteDucto != null && objParamTransporteDucto.Any())
                    {
                        var lstParamPe = objParamTransporteDucto.Where(x => x.CODIGO == strDescripcion).ToList();
                        if (lstParamPe != null && lstParamPe.Any())
                        {
                            lngResultado = Convert.ToInt32(lstParamPe.FirstOrDefault().ID_PARAMETRICA_GLOBAL);
                        }
                    }
                    break;
                case (int)EnumTipoParametrica.TipoConsumidor:
                    if (objParamTipoConsumidor != null && objParamTipoConsumidor.Any())
                    {
                        var lstParamPe = objParamTipoConsumidor.Where(x => x.CODIGO == strDescripcion || x.DESCRIPCION==strDescripcion).ToList();
                        if (lstParamPe != null && lstParamPe.Any())
                        {
                            lngResultado = Convert.ToInt32(lstParamPe.FirstOrDefault().ID_PARAMETRICA_GLOBAL);
                        }
                    }
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
        private void MEnviaObjeto(List<ObjProtoMovimientoTransaccion> lstProtoMovimientos, bool booTipo, long lngNoMovimientos, decimal decFechaInicio, decimal decFechaFin, decimal decTipoOperacion, decimal decOrden)
        {
            var objRegistroo = new ERegistraProtoMovimientoTransaccion();
            objRegistroo.strCredencial = CParametrosHydro.strCredencialFuncionario;
            objRegistroo.decIdTipoActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
            objRegistroo.decIdConsumidor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString());
            objRegistroo.decIdTipoOperacion = decTipoOperacion;
            objRegistroo.decFechaInicio = decFechaInicio;
            objRegistroo.decFechaFin = decFechaFin;
            objRegistroo.listaMovimientoTransaccion = lstProtoMovimientos;

            //var ojo = JsonSerializer.SerializeToString(objRegistroo);
            var resultado = _client.Post<EResultado>(CAppSettings.svc_registra_movimiento_transaccion_protobuf, objRegistroo);
            if (resultado.intCodigo == 1)
            {
                if (booTipo)
                {
                    var lstDespachos = _client.Post<EResultado>(CAppSettings.svc_activa_json_transaccion,
                    new
                    {
                        strCredencial = CParametrosHydro.strCredencialFuncionario,
                        decFiltro = 1,
                        decIdConsumidor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString()),
                        decIdTipoActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()),
                        decIdTipoOperacion = decTipoOperacion,
                        decFechaInicio,
                        decFechaFin
                    });
                    if (lstDespachos.intCodigo == 1)
                    {
                        if (decOrden == 1)
                        {
                            var datFechaReg1 = DateTime.Now;
                            var repCe = new repConstanciaEnvio();

                            repCe.xtcFecha.Text = datFechaReg1.ToShortDateString();
                            repCe.xtcHora.Text = datFechaReg1.ToLongTimeString();
                            repCe.xtcUsuario.Text = CParametrosHydro.StrUsuarioLogin;
                            repCe.xtcIntervalo.Text = "";

                            repCe.xtcActividad.Text = tlsOperadores.FocusedNode.GetValue("StrActividad").ToString();
                            repCe.xtcTipoOperacion.Text = cmbTipoOperacion.Text;
                            repCe.xtcPeriodo.Text = cmbPeriodo.Text;

                            repCe.xtcNit.Text = tlsOperadores.FocusedNode.GetValue("LngNit").ToString();
                            repCe.xtcRazonSocial.Text = tlsOperadores.FocusedNode.GetValue("StrDescPadre").ToString();
                            repCe.xtcNomComercial.Text = tlsOperadores.FocusedNode.GetValue("StrDescripcion").ToString();
                            repCe.xtcNomPropietario.Text = (tlsOperadores.FocusedNode.GetValue("StrPropietario") == null) ? "" : tlsOperadores.FocusedNode.GetValue("StrPropietario").ToString();
                            repCe.xtcNomRepresentante.Text = tlsOperadores.FocusedNode.GetValue("StrRepLegal").ToString();
                            repCe.xtcCiudad.Text = tlsOperadores.FocusedNode.GetValue("StrDepartamento").ToString();
                            repCe.xtcDireccion.Text = tlsOperadores.FocusedNode.GetValue("StrDireccion").ToString();
                            repCe.xtcTelefono.Text = tlsOperadores.FocusedNode.GetValue("StrTelefono").ToString();
                            repCe.xtcNoRegistros.Text = lngNoMovimientos.ToString(CultureInfo.InvariantCulture);

                            Session[CVariablesSesion.TipoReporte] = EnumTipoReporte.ConstanciaEnvio;
                            Session[CVariablesSesion.ReportePrincipal] = repCe;
                            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( 'WFReporte.aspx', '');if (confirm('¿Desea emtir la Declaración Jurada?')){window.location.href = 'WFDeclaracionJurada.aspx';}", true);

                            MLimpiaGrilla();
                        }
                    }
                    else
                    {
                        MImprimirMensaje(lstDespachos.strMensaje, EnumTiposMensajeAlerta.Error);
                    }
                }
                else
                {
                    //todo No implementado.
                }
            }
            else
            {
                MImprimirMensaje(resultado.strMensaje, EnumTiposMensajeAlerta.Error);
                //return;
            }
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
        private bool esnumero(string strgVariable)
        {
            try
            {
                Convert.ToDecimal(strgVariable);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        #endregion
        
    }
}