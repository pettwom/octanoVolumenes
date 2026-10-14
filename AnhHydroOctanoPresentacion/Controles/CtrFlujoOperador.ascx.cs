
using System.Threading.Tasks;
using System.Threading; // Importa el espacio de nombres System.Threading
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
    using System.Web;
    using System.Web.UI;
    using AnhHydroOctanoPersistencia.Aplicacion.Consulta;
    using AnhHydroOctanoPersistencia.Aplicacion.Gestion;
    using AnhHydroOctanoPersistencia.Entidades;
    //using AnhHydroOctanoAgenteServicio.ServicioExternoSegip;
    using Clases.Controles;
    using Clases.Util;
    using Entidades;
    using Enumeradores;
    using Parametros;
    using DevExpress.Web;
    using Librerias.Anh.Us;
    using ServiceStack.ServiceClient.Web;
    using ServiceStack.Text;
    using UserControl = System.Web.UI.UserControl;
    using EResultado = Entidades.EResultado;
    using O_LISTA_TRANSACCION_CTY = Entidades.O_LISTA_TRANSACCION_CTY;
    using Page = System.Web.UI.Page;
    using DevExpress.Utils;
    using System.Web.Script.Serialization;
    using AnhHydroOctanoAgenteServicio.ServicioExternoSegip;
    using AnhHydroOctanoPresentacion.Sitio.Reportes;
    using ServiceStack.Common;
    using AnhHydroOctanoAgenteServicio.ServicioHydroSesion;    
    using Clases.Persona;
    using System.Text;
    using AnhHydroOctanoPersistencia.Aplicacion.Listado;
    public partial class CtrFlujoOperador : UserControl
    {
        #region Variables de entorno

        readonly JsonServiceClient _client = new JsonServiceClient(CAppSettings.servicio_octVolumen);
        readonly JsonServiceClient _clientI = new JsonServiceClient(CAppSettings.servicio_octInfraestructura);
        readonly JsonServiceClient _extInteg = new JsonServiceClient(CAppSettings.servicio_octIntegracion);
        readonly JsonServiceClient _srvMtr = new JsonServiceClient(CAppSettings.servicio_Motorizados);
        readonly JsonServiceClient _srvApi = new JsonServiceClient(CAppSettings.servicio_api);

        private readonly CPersistenciaConsulta _consulta = new CPersistenciaConsulta();
        private readonly CPersistenciaGestion _gestion = new CPersistenciaGestion();
        private readonly CPersistenciaListados _listado = new CPersistenciaListados();
        //private readonly CPersistenciaGestionPg _gestionPg = new CPersistenciaGestionPg();  
      

        readonly CUtil _cUtil = new CUtil();
        readonly CTrazaInf _cTrazaInf = new CTrazaInf();
        CConsultasParametricas cConsultasParametricas = new CConsultasParametricas();
        private CPersonaSesion _datSesion;
        decimal _totalImporte, _totalCantidad, _totalImporteRec, _totalCantidadRec;
        long _totalAnulada, _totalValida;
        int intIdHidrocarburo = 0;


        #endregion

        #region Eventos de Formulario

        protected void Page_Load(object sender, EventArgs e)
        {
            
            var objHidrocarburo = _client.Post<EResultadoProducto>(CAppSettings.svc_lista_producto,
       new
       {
           strCredencial = CParametrosHydro.strCredencialFuncionario,
           strCodigo = ""
       });



            if (objHidrocarburo.IntCodigo == 1)
            {
                cmbHidrocarburo.DataSource = objHidrocarburo.OResultado;
                cmbHidrocarburo.DataBind();          
            }
            else
            {
                cmbHidrocarburo.DataSource = null;
                cmbHidrocarburo.DataBind();         
            }
            var lstObtCatalogo = _client.Post<EResultadoParametrica>(CAppSettings.svc_obtener_catalogo,
               new
               {
                   strLlave = ""
               });

            if (lstObtCatalogo.IntCodigo == 1)
            {
                var objParametricas = lstObtCatalogo.OResultado.CATALOGO_UNIDADES_MEDIDA_GRAL;
                cmbUnidadMedida.DataSource = objParametricas;
                cmbUnidadMedida.DataBind();
            }

            /******************************/

            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    if (!Page.IsCallback && !Page.IsPostBack)
                    {
                        Session.Remove(CVariablesSesion.Aux);
                        Session.Remove(CVariablesSesion.AuxActualiza);
                        Session.Remove(CVariablesSesion.ComboTipoOperacion);
                        Session.Remove(CVariablesSesion.ComboTiposOperacion);
                        Session.Remove(CVariablesSesion.EstructuraArbol);
                        Session.Remove(CVariablesSesion.GrillaAcumulado);
                        Session.Remove(CVariablesSesion.GrillaAuxiliar);
                        Session.Remove(CVariablesSesion.GrillaTitulo);
                        Session.Remove(CVariablesSesion.GrillaTransaccion);
                        Session.Remove(CVariablesSesion.HabilitaFunciones);
                        Session.Remove(CVariablesSesion.IdAuxiliar);
                        Session.Remove(CVariablesSesion.ListaParametrica);
                        Session.Remove(CVariablesSesion.ListaParamGral);
                        Session.Remove(CVariablesSesion.ListaParamInfra);
                        Session.Remove(CVariablesSesion.ListaPeriodos);
                        Session.Remove(CVariablesSesion.ListaTransaccion);
                        Session.Remove(CVariablesSesion.OctanoParametrica);
                        Session.Remove(CVariablesSesion.ParamAux);
                        Session.Remove(CVariablesSesion.StrAuxiliar);
                        Session.Remove(CVariablesSesion.ComboGenerico);
                        Session.Remove(CVariablesSesion.HabBotonesSaldoFisico);                   

                        MCargaArbol();
                        MCargaParametricas();
                        MCargaPermisos();
                        ConfigurarFormulario();
                        // CargarConfig();                                                                       
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

                    if (tlsOperadores.DataSource == null)
                    {
                        tlsOperadores.DataSource = Session[CVariablesSesion.EstructuraArbol];
                        tlsOperadores.DataBind();
                    }
                    else
                    {
                        //todo No es carga inicial: no implementado
                    }

                    if (dgvAcumulado.DataSource == null && Session[CVariablesSesion.GrillaAcumulado] != null)
                    {
                        dgvAcumulado.DataSource = Session[CVariablesSesion.GrillaAcumulado];
                        dgvAcumulado.DataBind();

                        string[] strTitulos;
                        strTitulos = Session[CVariablesSesion.GrillaTitulo] as string[];

                        for (int i = 0; i < 100; i++)
                        {
                            if (strTitulos[i] != "")
                            {
                                dgvAcumulado.Columns[i].Caption = strTitulos[i];
                            }
                        }
                    }
                    else
                    {
                        //todo grilla vacia: no implementado
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

                    if (cmbFiltro.DataSource == null && Session[CVariablesSesion.ComboGenerico] != null)
                    {
                        cmbFiltro.DataSource = Session[CVariablesSesion.ComboGenerico];
                        cmbFiltro.DataBind();
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

        protected void pnlDatos_Callback(object sender, CallbackEventArgsBase e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    if (tlsOperadores.FocusedNode == null)
                    {
                        MImprimirMensaje("Seleccione un proyecto.", EnumTiposMensajeAlerta.Error);
                        return;
                    }
                    Session[CVariablesSesion.EntidadSeleccionada] = tlsOperadores.FocusedNode.GetValue("StrUbicacion");

                    lblRazonSocial.Text = tlsOperadores.FocusedNode.GetValue("StrDescPadre").ToString();
                    lblNit.Text = tlsOperadores.FocusedNode.GetValue("LngNit").ToString();
                    lblRepLegal.Text = (tlsOperadores.FocusedNode.GetValue("StrDireccion") == null) ? "" : tlsOperadores.FocusedNode.GetValue("StrDireccion").ToString();
                    lblDepto.Text = tlsOperadores.FocusedNode.GetValue("StrDepartamento").ToString();

                    if (Session[CVariablesSesion.AuxActualiza] != null)
                    {
                        Session.Remove(CVariablesSesion.AuxActualiza);
                        //pnlGrafico_Callback(sender, e);
                        return;
                    }

                    var panel = sender as ASPxCallbackPanel;
                    panel.JSProperties["cp_title"] = "";
                    panel.JSProperties["cp_subtitle"] = "";
                    panel.JSProperties["cp_xAxis"] = null;
                    panel.JSProperties["cp_series"] = null;
                    panel.JSProperties["cp_Alerta"] = ""; //L.l

                    MLimpiaGrilla();
                    if ((int)tlsOperadores.FocusedNode.GetValue("IntAccion") > 0)
                    {
                        var decAct = Convert.ToInt32(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                        if (decAct == 1 || decAct == 2)
                        {
                            pnlHerramienta.ClientVisible = true;
                            //Add en fecha 16/06/2023
                            if (decAct == 1)
                            {
                                btnSaldoFisico.Visible = true;
                                btnAnularSaldoFisico.Visible = true;
                                //btnRegistrarPrecio.Visible = true;
                            }                                                                
                        }

                        //tipos de operacion
                        var objTipoOperacion = _client.Post<EResultadoTipoOperacion>(CAppSettings.svc_obtener_tipos_operacion_act,
                        new
                        {
                            decFiltro = 3,
                            strParametro1 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
                            strParametro2 = tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString(),
                            strParametro3 = detFechaIni.Date.Year + detFechaIni.Date.Month.ToString().PadLeft(2, '0') + detFechaIni.Date.Day.ToString().PadLeft(2, '0') + "000000",
                            strParametro4 = detFechaFin.Date.Year + detFechaFin.Date.Month.ToString().PadLeft(2, '0') + detFechaFin.Date.Day.ToString().PadLeft(2, '0') + "235959"
                        });
                        if (objTipoOperacion.IntCodigo == 1)
                        {
                            Session[CVariablesSesion.ComboTiposOperacion] = objTipoOperacion.OResultado;
                        }
                        else
                        {
                            MImprimirMensaje(objTipoOperacion.StrMensaje, EnumTiposMensajeAlerta.Error);
                        }

                        if (tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString() != "16")//lmqc
                        {
                            if (tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString() != "36")//lmqc
                            {
                                //carga periodos habilitados
                                var objPeriodo = _client.Post<EResultadoPeriodos>(CAppSettings.svc_lista_periodos_consumidor,
                                    new
                                    {
                                        strCredencial = CParametrosHydro.strCredencialFuncionario,
                                        decFiltro = 2,
                                        strParametro1 = tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString(),
                                        strParametro2 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
                                        strParametro3 = "",
                                        strParametro4 = "",
                                        strParametro5 = tlsOperadores.FocusedNode.GetValue("DecIdDepartamento").ToString()
                                    });
                                if (objPeriodo.IntCodigo == 1)
                                {
                                    Session[CVariablesSesion.ListaPeriodos] = objPeriodo.OResultado;
                                }
                                else
                                {
                                    //todo No implementado
                                }
                            }
                        }
                        var strMensaje = string.Empty;
                        if (MValidaDatosIniciales(ref strMensaje))
                        {
                            var strDato1 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString();
                            var strDato2 = tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString();
                            var strDato3 = detFechaIni.Date.Year + detFechaIni.Date.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + detFechaIni.Date.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0');
                            var strDato4 = detFechaFin.Date.Year + detFechaFin.Date.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + detFechaFin.Date.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0');

                            MCargaParametricasEsp();
                            MCargaGrilla(strDato1, strDato2, strDato3, strDato4, sender);
                        }
                        else
                        {
                            MImprimirMensaje(strMensaje, EnumTiposMensajeAlerta.Error);
                        }
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

        protected void pnlTransaccion_Callback(object sender, CallbackEventArgsBase e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    Session.Remove(CVariablesSesion.StrAuxiliar);
                    cmbTipoOperacion.DataSource = null;
                    cmbTipoOperacion.DataBind();
                    cmbTipoOperacion.Text = "";
                    Session[CVariablesSesion.AuxActualiza] = "1";

                    Session.Remove(CVariablesSesion.GrillaTransaccion);
                    dgvTransacciones.DataSource = null;
                    dgvTransacciones.DataBind();
                    dgvTransacciones.ExpandAll();

                    if (tlsOperadores.FocusedNode != null)
                    {
                        if ((int)tlsOperadores.FocusedNode.GetValue("IntAccion") > 0)
                        {
                            var lstTipoOperacion = Session[CVariablesSesion.ComboTiposOperacion] as List<O_TIPOS_OPE_ACT_CTY>;
                            if (lstTipoOperacion != null && lstTipoOperacion.Any())
                            {
                                cmbTipoOperacion.DataSource = lstTipoOperacion.OrderBy(x => x.NOMBRE);
                                cmbTipoOperacion.DataBind();
                                Session[CVariablesSesion.ComboTipoOperacion] = lstTipoOperacion;
                                Session[CVariablesSesion.StrAuxiliar] = e.Parameter;

                                var param = e.Parameter.Split('|');

                                var lstTipoOp = Session[CVariablesSesion.ComboTipoOperacion] as List<O_TIPOS_OPE_ACT_CTY>;

                                if (param[2] == "2")
                                {
                                    lstTipoOp = lstTipoOp.Where(x => x.NOMBRE == param[1]).ToList();
                                    Session.Remove(CVariablesSesion.Aux);
                                    if (lstTipoOp.Any())
                                    {
                                        Session[CVariablesSesion.Aux] = lstTipoOp.FirstOrDefault().CODIGO;
                                        cmbTipoOperacion.SelectedItem = cmbTipoOperacion.Items.FindByValue(lstTipoOp.FirstOrDefault().ID_TIPO_OPERACION.ToString());

                                        Session[CVariablesSesion.TipoOperacion] = cmbTipoOperacion.SelectedItem.Text;
                                        var strFecha = param[0].Substring(6, 4) + param[0].Substring(3, 2) + param[0].Substring(0, 2);
                                        var lstMov = Session[CVariablesSesion.GrillaAcumulado] as List<CMovAcumulado>;
                                        var objMov = lstMov.Where(x => x.StrIdGrupoUnico == cmbFiltro.Value.ToString()).ToList();
                                        if (!objMov.Any())
                                            return;
                                        var strIdProd = objMov.FirstOrDefault().IntIdProducto;
                                        var strIdTiIn = objMov.FirstOrDefault().DecIdTipoInfraestructura;
                                        var strIdInfr = objMov.FirstOrDefault().DecIdInfraestructura;

                                        Session[CVariablesSesion.ParamAux] = strFecha.Trim() + "|" + strIdProd + "|" + strIdTiIn + "|" + strIdInfr;

                                        MConfiguraGrilla(lstTipoOp.FirstOrDefault().BINARIO_ESTADO.Substring(1, 1), Session[CVariablesSesion.Aux].ToString());
                                        dgvTransacciones_CustomCallback(null, new ASPxGridViewCustomCallbackEventArgs(e.Parameter));
                                    }
                                    else
                                    {
                                        //dgvTransacciones.Columns[(int)EnumColumnasGrilla.FoAbm].Visible = false;
                                    }
                                }
                                else
                                {
                                    decimal dec;
                                    if (param[1] == "DecSaldoInicial" || param[1] == "MDIN" || decimal.TryParse(param[1].Substring(6, 2), out dec))
                                    {


                                        if (param[1] == "MDIN")
                                        {
                                            lstTipoOp = lstTipoOp.Where(x => x.CODIGO.Contains("MDIN")).ToList();
                                        }
                                        else if (param[1] == "DecSaldoInicial")
                                        {
                                            lstTipoOp = lstTipoOp.Where(x => x.CODIGO == "TOSICB").ToList();
                                        }
                                        else
                                        {
                                            var intTipo = (param[1].Contains("Ing")) ? 1 : -1;
                                            int intOrden = Convert.ToInt32(param[1].Substring(6, 2));
                                            lstTipoOp = lstTipoOp.Where(x => x.FACTOR_CALCULO == intTipo && x.ORDEN == intOrden).ToList();
                                        }

                                        Session.Remove(CVariablesSesion.Aux);
                                        if (lstTipoOp.Any())
                                        {
                                            Session[CVariablesSesion.Aux] = lstTipoOp.FirstOrDefault().CODIGO;
                                            cmbTipoOperacion.SelectedItem = cmbTipoOperacion.Items.FindByValue(lstTipoOp.FirstOrDefault().ID_TIPO_OPERACION.ToString());
                                            Session[CVariablesSesion.TipoOperacion] = cmbTipoOperacion.SelectedItem.Text;
                                            int index = Convert.ToInt32(param[0].Trim());
                                            if (index >= 0)
                                            {
                                                var strFecha = dgvAcumulado.GetRowValues(index, "StrFecha").ToString();
                                                strFecha = strFecha.Substring(6, 4) + strFecha.Substring(3, 2) + strFecha.Substring(0, 2);
                                                var strIdProd = dgvAcumulado.GetRowValues(index, "IntIdProducto").ToString();
                                                var strIdTiIn = dgvAcumulado.GetRowValues(index, "DecIdTipoInfraestructura").ToString();
                                                var strIdInfr = dgvAcumulado.GetRowValues(index, "DecIdInfraestructura").ToString();

                                                Session[CVariablesSesion.ParamAux] = strFecha.Trim() + "|" + strIdProd + "|" + strIdTiIn + "|" + strIdInfr;

                                                MConfiguraGrilla(lstTipoOp.FirstOrDefault().BINARIO_ESTADO.Substring(1, 1), Session[CVariablesSesion.Aux].ToString());
                                                dgvTransacciones_CustomCallback(null, new ASPxGridViewCustomCallbackEventArgs(e.Parameter));
                                            }
                                        }
                                        else
                                        {
                                            //dgvTransacciones.Columns[(int)EnumColumnasGrilla.FoAbm].Visible = false;
                                        }
                                    }
                                }

                            }
                            else
                            {
                                MImprimirMensaje("Tipos de operación indefinidos.", EnumTiposMensajeAlerta.Error);
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
            }
            else
            {
                //todo No implementado.
            }
        }

        protected void pnlTransaccionNotaDespacho_Callback(object sender, CallbackEventArgsBase e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    var param = e.Parameter.Split('|');
                    int index = Convert.ToInt32(param[0].Trim());

                    txtDocumentoNotaDespacho.Text = tlsOperadores.FocusedNode.GetValue("StrNroHydro").ToString();
                    txtNitOperadorNotaDespacho.Text = tlsOperadores.FocusedNode.GetValue("LngNit").ToString();
                    txtPlantaNotaDespacho.Text = tlsOperadores.FocusedNode.GetValue("StrDescripcion").ToString() + " - " + tlsOperadores.FocusedNode.GetValue("StrDepartamento").ToString();

                    txtFechaSuministroNotaDespacho.Text = DateTime.Now.ToString();

                    txtOperadorNotaDespacho.Text = "AIR BP BOLIVIA S.A. ABBSA";

                    txtProductoNacionalDespacho.Text = dgvAcumulado.GetRowValues(index, "StrProducto").ToString();
                    txtProductoInterNacionalDespacho.Text = dgvAcumulado.GetRowValues(index, "StrProducto").ToString();

                    txtEncargadoNotaDespacho.Text = CVariablesSesion.UsuarioSession(this.Context).NOMBRE_COMPLETO.ToString();

                    txtRefuellerNotaDespachoN.Text = dgvAcumulado.GetRowValues(index, "StrInfraestructura").ToString();
                    txtRefuellerNotaDespachoI.Text = dgvAcumulado.GetRowValues(index, "StrInfraestructura").ToString();
                    intIdHidrocarburo = ((int)dgvAcumulado.GetRowValues(index, "IntIdProducto"));
                    txtIdHidro.Text = ((int)dgvAcumulado.GetRowValues(index, "IntIdProducto")).ToString();
                    switch ((int)dgvAcumulado.GetRowValues(index, "IntIdProducto"))
                    {
                        case 129:
                            txtPrecioUnitarioNacional.Text = "2.77";
                            txtPrecioUnitarioInternacional.Text = "6.65";
                            break;
                        case 128:
                            txtPrecioUnitarioNacional.Text = "4.57";
                            txtPrecioUnitarioInternacional.Text = "0";
                            break;
                        default:
                            txtPrecioUnitarioNacional.Text = "0";
                            txtPrecioUnitarioInternacional.Text = "0";
                            break;
                    }

                    var objRes = _extInteg.Post<EResultadoI>(CAppSettings.svc_GestionAasana,
                    new
                    {
                        strCredencial = CParametrosHydro.StrCredencialExterno,
                        decOrigen = 1, //1:ANH 2:AASANA
                        decAccion = 0,
                        decServicio = 1,//1: parametrica | 2: integracion
                        decIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]),
                        decFiltro = 1,
                        strDato1 = "aeropuerto",
                        strDato2 = "",
                        strDato3 = "",
                        strDato4 = ""
                    });
                    if (objRes.decCodigo == 1)
                    {
                        var lstResultado = JsonSerializer.DeserializeFromString<List<O_LISTA_AASANA_AEROPUERTO_CTY>>((string)objRes.oResultado);
                        cmbOrigenNotaDespacho.DataSource = lstResultado;
                        cmbOrigenNotaDespacho.DataBind();
                        cmbDestinoNotaDespacho.DataSource = lstResultado;
                        cmbDestinoNotaDespacho.DataBind();
                        cmbAlternoNotaDespacho.DataSource = lstResultado;
                        cmbAlternoNotaDespacho.DataBind();
                        Session[CVariablesSesion.GrillaHistorial] = lstResultado;
                    }
                    else
                    {
                        MImprimirMensaje("Obtener Aeropuertos. " + objRes.strMensaje, EnumTiposMensajeAlerta.Error);
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

        protected void pnlDatoAeronave_Callback(object sender, CallbackEventArgsBase e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    var objResultado = _consulta.ObtenerDatoAeronave(
                    new EObjetoEnvio1ParametrosProto
                    {
                        decFiltro = 4,
                        strParametro1 = txtMatriculaNotaDespacho.Text
                    });
                    if (objResultado.intCodigo == 1)
                    {
                        var lstAeronaves = (List<O_OBTENER_DATO_AERONAVES_CTY>)objResultado.oResultado;

                        var lstExplotador = lstAeronaves;

                        txtBarcodeNotaDespacho.Text = lstExplotador[0].BARCODE;
                        txtModeloNotaDespacho.Text = lstExplotador[0].MODELO;

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

        protected void dgvTransacciones_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            try
            {
                #region Proceso a Ejecutar

                Session.Remove(CVariablesSesion.GrillaTransaccion);
                dgvTransacciones.DataSource = null;
                dgvTransacciones.DataBind();
                dgvTransacciones.ExpandAll();

                if (Session[CVariablesSesion.StrAuxiliar] == null)
                    return;

                var strDato = Session[CVariablesSesion.ParamAux].ToString().Split('|');
                var strFecha = strDato[0];
                var strIdProd = strDato[1];
                var strIdTiIn = strDato[2];
                var strIdInfr = strDato[3];
                var decFilt = 1;
                var strPar6 = strIdProd;
                var strPar7 = "";
                var strPar8 = "";

                if (dgvAcumulado.Columns[5].Visible)
                {
                    decFilt = 2;
                    strPar7 = strIdInfr;
                    strPar8 = strIdTiIn;
                }

                var ro = decFilt;//Filtro 1 por producto //2 consulta por infraestructura
                var ro1 = tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString(); //Id proyecto/entidad
                var ro2 = strFecha + "000000";//fecha inicial
                var ro3 = strFecha + "235959"; //fecha final
                var ro4 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(); //id Actividad
                var ro5 = cmbTipoOperacion.Value.ToString(); //tipo operacion
                var ro6 = strPar6; //idProducto 
                var ro7 = strPar7; // id_infraestructura
                var ro8 = strPar8; // tipo_infraestructura

                var lstDespachos = _consulta.ObtenerMovimiento("",
                new EObjetoEnvio8ParametrosProto
                {
                    decFiltro = decFilt, //Filtro 1 por producto //2 consulta por infraestructura
                    strParametro1 = tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString(), //Id proyecto/entidad
                    strParametro2 = strFecha + "000000",//fecha inicial
                    strParametro3 = strFecha + "235959", //fecha final
                    strParametro4 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(), //id Actividad
                    strParametro5 = cmbTipoOperacion.Value.ToString(), //tipo operacion
                    strParametro6 = strPar6, //idProducto 
                    strParametro7 = strPar7, // id_infraestructura
                    strParametro8 = strPar8 // tipo_infraestructura
                });

                if (lstDespachos.intCodigo == 1)
                {
                    var lstResultado = (List<O_CONSULTA_MOVIM_TRAN_CTY>)lstDespachos.oResultado;
                    if (lstResultado.Any())
                    {

                        var registros = lstResultado;
                        Session[CVariablesSesion.GrillaTransaccion] = registros;

                        dgvTransacciones.DataSource = registros;
                        dgvTransacciones.DataBind();
                        dgvTransacciones.ExpandAll();
                    }
                    else
                    {
                        MImprimirMensaje(lstDespachos.strMensaje, (lstDespachos.strMensaje == "No se encontraron registros") ? EnumTiposMensajeAlerta.Alerta : EnumTiposMensajeAlerta.Error);
                    }
                }
                else
                {
                    MImprimirMensaje(lstDespachos.strMensaje, (lstDespachos.strMensaje == "No se encontraron registros") ? EnumTiposMensajeAlerta.Alerta : EnumTiposMensajeAlerta.Error);
                    if (lstDespachos.intCodigo != -100) return;

                    var objEnv = new EObjetoEnvio8ParametrosProto
                    {
                        decFiltro = decFilt,
                        strParametro1 = tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString(),
                        strParametro2 = strFecha + "000000",
                        strParametro3 = strFecha + "235959",
                        strParametro4 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
                        strParametro5 = cmbTipoOperacion.Value.ToString(),
                        strParametro6 = strPar6,
                        strParametro7 = strPar7,
                        strParametro8 = strPar8
                    };
                    var expBd = new Exception(lstDespachos.intCodigo + " " + lstDespachos.strMensaje + " - " + JsonSerializer.SerializeToString(objEnv));
                    var oMensajeUsuario = new CLogTraza.MensajeUsuario
                    {
                        decUsuarioId = Session[CVariablesSesion.IdUsuario],
                        decIdModulo = CParametrosHydro.decIdAplicacion,
                        strAccion = GetType().Name + "." + MethodBase.GetCurrentMethod().Name,
                        strIp = HttpContext.Current.Request.UserHostAddress,
                        decNivelCapa = (int)CLogTraza.CapasNivel.Presentacion
                    };
                    CLogTraza.Error(oMensajeUsuario, expBd);
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

        protected void dgvTransacciones_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    var param = Request.Params.Get("__CALLBACKPARAM");
                    if (!String.IsNullOrEmpty(param) && !param.Contains("CANCELEDIT"))
                    {
                        try
                        {                            
                            MConfiguraFormulario();
                            if (Session[CVariablesSesion.GrillaAuxiliar] != null)
                            {
                                var dgvDocumentos = (dgvTransacciones.FindEditFormTemplateControl("dgvDocumentos")) as ASPxGridView;
                                dgvDocumentos.DataSource = Session[CVariablesSesion.GrillaAuxiliar];
                                dgvDocumentos.DataBind();
                            }
                            else
                            {
                                //todo grilla vacia: no implementado
                            }
                        }
                        catch (Exception)
                        { }

                        dgvTransacciones = (sender) as ASPxGridView;
                        if (tlsOperadores.FocusedNode != null && (int)tlsOperadores.FocusedNode.GetValue("IntIdActividad") > 0)
                        {
                            #region Instancia componentes
                            var txtIdEf = (dgvTransacciones.FindEditFormTemplateControl("txtIdEf")) as ASPxTextBox;
                            var detFechaEf = (dgvTransacciones.FindEditFormTemplateControl("detFechaEf")) as ASPxDateEdit;
                            var cmbEstadoEf = (dgvTransacciones.FindEditFormTemplateControl("cmbEstadoEf")) as ASPxComboBox;
                            var cmbTipoFlujoEf = (dgvTransacciones.FindEditFormTemplateControl("cmbTipoFlujoEf")) as ASPxComboBox;
                            var cmbUnidadEf = (dgvTransacciones.FindEditFormTemplateControl("cmbUnidadEf")) as ASPxComboBox;
                            var cmbTipoTransEf = (dgvTransacciones.FindEditFormTemplateControl("cmbTipoTransEf")) as ASPxComboBox;
                            var cmbMedioTransEf = (dgvTransacciones.FindEditFormTemplateControl("cmbMedioTransEf")) as ASPxComboBox;
                            var cmbEntidadAuxialiarEf = (dgvTransacciones.FindEditFormTemplateControl("cmbEntidadAuxialiarEf")) as ASPxComboBox;
                            var cmbTipoMedicEf = (dgvTransacciones.FindEditFormTemplateControl("cmbTipoMedicEf")) as ASPxComboBox;
                            var cmbTipoMovEf = (dgvTransacciones.FindEditFormTemplateControl("cmbTipoMovEf")) as ASPxComboBox;
                            var cmbPaisEf = (dgvTransacciones.FindEditFormTemplateControl("cmbPaisEf")) as ASPxComboBox;
                            var txtClienteEf = (dgvTransacciones.FindEditFormTemplateControl("txtClienteEf")) as ASPxTextBox;
                            var txtNitEf = (dgvTransacciones.FindEditFormTemplateControl("txtNitEf")) as ASPxTextBox;
                            var txtPlacaEf = (dgvTransacciones.FindEditFormTemplateControl("txtPlacaEf")) as ASPxTextBox;
                            var txtSubIdentEf = (dgvTransacciones.FindEditFormTemplateControl("txtSubIdentEf")) as ASPxTextBox;
                            var cmbTanqueEf = (dgvTransacciones.FindEditFormTemplateControl("cmbTanqueEf")) as ASPxComboBox;
                            var txtVolAguaEf = (dgvTransacciones.FindEditFormTemplateControl("txtVolAguaEf")) as ASPxTextBox;

                            var cmbProdEf = (dgvTransacciones.FindEditFormTemplateControl("cmbProdEf")) as ASPxComboBox;
                            var txtCantidadEf = (dgvTransacciones.FindEditFormTemplateControl("txtCantidadEf")) as ASPxTextBox;
                            var txtObsEf = (dgvTransacciones.FindEditFormTemplateControl("txtObsEf")) as ASPxTextBox;
                            var txtAdicionalEf = (dgvTransacciones.FindEditFormTemplateControl("txtAdicionalEf")) as ASPxTextBox;
                            var dgvDocumentos = (dgvTransacciones.FindEditFormTemplateControl("dgvDocumentos")) as ASPxGridView;
                            var txtCodigoEf = (dgvTransacciones.FindEditFormTemplateControl("txtCodigoEf")) as ASPxTextBox;
                            var cmbTipoInfEf = (dgvTransacciones.FindEditFormTemplateControl("cmbTipoInfEf")) as ASPxComboBox;
                            var cmbTipoEnvaseEf = (dgvTransacciones.FindEditFormTemplateControl("cmbTipoEnvaseEf")) as ASPxComboBox;

                            var cmbTipoTempEf = (dgvTransacciones.FindEditFormTemplateControl("cmbTipoTempEf")) as ASPxComboBox;
                            var txtTemperaturaEf = (dgvTransacciones.FindEditFormTemplateControl("txtTemperaturaEf")) as ASPxTextBox;
                            var txtGravedadEf = (dgvTransacciones.FindEditFormTemplateControl("txtGravedadEf")) as ASPxTextBox;
                            var txtIntegracionEf = (dgvTransacciones.FindEditFormTemplateControl("txtIntegracionEf")) as ASPxTextBox;
                            var txtBarCodeEf = (dgvTransacciones.FindEditFormTemplateControl("txtBarCodeEf")) as ASPxTextBox;

                            var txtJsonRef = (dgvTransacciones.FindEditFormTemplateControl("txtJsonRef")) as ASPxTextBox;
                            var txtEstJsonRef = (dgvTransacciones.FindEditFormTemplateControl("txtEstJsonRef")) as ASPxTextBox;
                            var txtIdCuenta = (dgvTransacciones.FindEditFormTemplateControl("txtIdCuenta")) as ASPxTextBox;
                            var txtIdCuentaAux = (dgvTransacciones.FindEditFormTemplateControl("txtIdCuentaAux")) as ASPxTextBox;
                            var txtIdParGl = (dgvTransacciones.FindEditFormTemplateControl("txtIdParGl")) as ASPxTextBox;
                            var txtIdParEs = (dgvTransacciones.FindEditFormTemplateControl("txtIdParEs")) as ASPxTextBox;

                            var txtIdTipoInfAux = (dgvTransacciones.FindEditFormTemplateControl("txtIdTipoInfAux")) as ASPxTextBox;
                            var txtIdInfAux = (dgvTransacciones.FindEditFormTemplateControl("txtIdInfAux")) as ASPxTextBox;

                            var cmbTipoDespachoEf = (dgvTransacciones.FindEditFormTemplateControl("cmbTipoDespachoEf")) as ASPxComboBox;
                            var btnAceptar = (dgvTransacciones.FindEditFormTemplateControl("btnAceptar")) as ASPxButton;
                            btnAceptar.Visible = true;

                            /*********************************json redes **********************************/
                            var txtCodUsuario = (dgvTransacciones.FindEditFormTemplateControl("txtCodUsuario")) as ASPxTextBox;
                            var txtCodUbicacion = (dgvTransacciones.FindEditFormTemplateControl("txtCodUbicacion")) as ASPxTextBox;
                            var txtCiudadPoblacion = (dgvTransacciones.FindEditFormTemplateControl("txtCiudadPoblacion")) as ASPxTextBox;
                            var txtCategoria = (dgvTransacciones.FindEditFormTemplateControl("txtCategoria")) as ASPxTextBox;


                            var txtDireccion = (dgvTransacciones.FindEditFormTemplateControl("txtDireccion")) as ASPxTextBox;
                            var cmbSectorEconomico = (dgvTransacciones.FindEditFormTemplateControl("cmbSectorEconomico")) as ASPxComboBox;
                            var txtTipoMedidor = (dgvTransacciones.FindEditFormTemplateControl("txtTipoMedidor")) as ASPxTextBox;
                            var txtCodMedidor = (dgvTransacciones.FindEditFormTemplateControl("txtCodMedidor")) as ASPxTextBox;

                            var txtLecMedidor = (dgvTransacciones.FindEditFormTemplateControl("txtLecMedidor")) as ASPxTextBox;
                            var txtFechaLec = (dgvTransacciones.FindEditFormTemplateControl("txtFechaLec")) as ASPxTextBox;

                            var txtPresionPsig = (dgvTransacciones.FindEditFormTemplateControl("txtPresionPsig")) as ASPxTextBox;
                            var txtFactorCorrec = (dgvTransacciones.FindEditFormTemplateControl("txtFactorCorrec")) as ASPxTextBox;
                            // var txtCantFactura = (dgvTransacciones.FindEditFormTemplateControl("txtCantFactura"))  as ASPxTextBox;
                            var txtCantNoCorregido = (dgvTransacciones.FindEditFormTemplateControl("txtCantNoCorregido")) as ASPxTextBox;
                            var txtImportAdicional = (dgvTransacciones.FindEditFormTemplateControl("txtImportAdicional")) as ASPxTextBox;


                            var lblCodUsuario = (dgvTransacciones.FindEditFormTemplateControl("lblCodUsuario")) as ASPxLabel;
                            var lblCodUbicacion = (dgvTransacciones.FindEditFormTemplateControl("lblCodUbicacion")) as ASPxLabel;
                            var lblCiudadPoblacion = (dgvTransacciones.FindEditFormTemplateControl("lblCiudadPoblacion")) as ASPxLabel;
                            var lblCategoria = (dgvTransacciones.FindEditFormTemplateControl("lblCategoria")) as ASPxLabel;
                            var lblDireccion = (dgvTransacciones.FindEditFormTemplateControl("lblDireccion")) as ASPxLabel;
                            var lblSectorEcon = (dgvTransacciones.FindEditFormTemplateControl("lblSectorEcon")) as ASPxLabel;
                            var lblTipoMedidor = (dgvTransacciones.FindEditFormTemplateControl("lblTipoMedidor")) as ASPxLabel;
                            var lblCodMedidor = (dgvTransacciones.FindEditFormTemplateControl("lblCodMedidor")) as ASPxLabel;
                            var lblFechaLec = (dgvTransacciones.FindEditFormTemplateControl("lblFechaLec")) as ASPxLabel;
                            var lblLecMedido = (dgvTransacciones.FindEditFormTemplateControl("lblLecMedido")) as ASPxLabel;
                            var lblPresionPsig = (dgvTransacciones.FindEditFormTemplateControl("lblPresionPsig")) as ASPxLabel;
                            var lblFactorCorrec = (dgvTransacciones.FindEditFormTemplateControl("lblFactorCorrec")) as ASPxLabel;
                            var lblCantNoCorregido = (dgvTransacciones.FindEditFormTemplateControl("lblCantNoCorregido")) as ASPxLabel;
                            var lblImportAdicional = (dgvTransacciones.FindEditFormTemplateControl("lblImportAdicional")) as ASPxLabel;

                            if (Convert.ToInt32(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()) == (int)EnumActividades.DistribucionGasRedes)
                            {
                                txtCodUsuario.Visible = true;
                                txtCodUbicacion.Visible = true;
                                txtCiudadPoblacion.Visible = true;
                                txtCategoria.Visible = true;
                                txtDireccion.Visible = true;
                                cmbSectorEconomico.Visible = true;
                                txtTipoMedidor.Visible = true;
                                txtCodMedidor.Visible = true;
                                txtFechaLec.Visible = true;
                                txtLecMedidor.Visible = true;
                                txtPresionPsig.Visible = true;
                                txtFactorCorrec.Visible = true;
                                txtCantNoCorregido.Visible = true;
                                txtImportAdicional.Visible = true;

                                lblCodUsuario.Visible = true;
                                lblCodUbicacion.Visible = true;
                                lblCiudadPoblacion.Visible = true;
                                lblCategoria.Visible = true;
                                lblDireccion.Visible = true;
                                lblSectorEcon.Visible = true;
                                lblTipoMedidor.Visible = true;
                                lblCodMedidor.Visible = true;
                                lblFechaLec.Visible = true;
                                lblLecMedido.Visible = true;
                                lblPresionPsig.Visible = true;
                                lblFactorCorrec.Visible = true;
                                lblCantNoCorregido.Visible = true;
                                lblImportAdicional.Visible = true;
                            }
                            else
                            {
                                txtCodUsuario.Visible = false;
                                txtCodUbicacion.Visible = false;
                                txtCiudadPoblacion.Visible = false;
                                txtCategoria.Visible = false;
                                txtDireccion.Visible = false;
                                cmbSectorEconomico.Visible = false;
                                txtTipoMedidor.Visible = false;
                                txtCodMedidor.Visible = false;
                                txtFechaLec.Visible = false;
                                txtLecMedidor.Visible = false;
                                txtPresionPsig.Visible = false;
                                txtFactorCorrec.Visible = false;
                                txtCantNoCorregido.Visible = false;
                                txtImportAdicional.Visible = false;

                                lblCodUsuario.Visible = false;
                                lblCodUbicacion.Visible = false;
                                lblCiudadPoblacion.Visible = false;
                                lblCategoria.Visible = false;
                                lblDireccion.Visible = false;
                                lblSectorEcon.Visible = false;
                                lblTipoMedidor.Visible = false;
                                lblCodMedidor.Visible = false;
                                lblFechaLec.Visible = false;
                                lblLecMedido.Visible = false;
                                lblPresionPsig.Visible = false;
                                lblFactorCorrec.Visible = false;
                                lblCantNoCorregido.Visible = false;
                                lblImportAdicional.Visible = false;
                            }

                            #endregion

                            var strValida = (Session[CVariablesSesion.HabilitaBotones] == null) ? "ND" : Session[CVariablesSesion.HabilitaBotones].ToString();
                            if (!strValida.Equals("MODSI"))
                                btnAceptar.Visible = false;

                            if (param.Contains("ADDNEWROW"))
                            {
                                #region nuevo registro

                                Session.Remove(CVariablesSesion.GrillaAuxiliar);
                                txtIdEf.Text = "0";
                                txtBarCodeEf.Text = "";

                                if (Session[CVariablesSesion.ParamAux] != null)
                                {
                                    var strParam = Session[CVariablesSesion.ParamAux].ToString();
                                    var arrParam = strParam.Split('|');

                                    MCargaListados();

                                    detFechaEf.Date = DateTime.Parse(arrParam[0].Substring(6, 2) + "/" + arrParam[0].Substring(4, 2) + "/" + arrParam[0].Substring(0, 4));
                                    detFechaEf.ClientEnabled = true;
                                    if (Convert.ToInt32(cmbTipoOperacion.Value) == (int)EnumTipoOperaciones.SaldoInicial)
                                    {
                                        detFechaEf.Date = DateTime.Parse("01/" + arrParam[0].Substring(4, 2) + "/" + arrParam[0].Substring(0, 4));
                                        detFechaEf.ClientEnabled = false;
                                    }

                                    cmbProdEf.SelectedItem = cmbProdEf.Items.FindByValue(arrParam[1]);
                                    if (dgvAcumulado.Columns[5].Visible)
                                    {
                                        cmbTanqueEf.SelectedItem = cmbTanqueEf.Items.FindByValue(arrParam[2].Replace("-", "") + arrParam[3].Replace("-", ""));
                                    }
                                    else
                                    {
                                        cmbTanqueEf.SelectedItem = cmbTanqueEf.Items.FindByValue("9999999999");
                                        cmbTipoInfEf.SelectedItem = cmbTipoInfEf.Items.FindByValue("-99999");
                                    }

                                    cmbTipoTransEf.SelectedItem = cmbTipoTransEf.Items.FindByValue("-99999");
                                    cmbMedioTransEf.SelectedItem = cmbMedioTransEf.Items.FindByValue("9999999999");
                                    cmbTipoEnvaseEf.SelectedItem = cmbTipoEnvaseEf.Items.FindByValue("-99999");
                                    cmbPaisEf.SelectedItem = cmbPaisEf.Items.FindByValue("120");

                                    //cmbTipoTempEf.SelectedItem = cmbTipoTempEf.Items.FindByValue("-99999");
                                    cmbEntidadAuxialiarEf.SelectedItem = cmbEntidadAuxialiarEf.Items.FindByValue("-99999");
                                    cmbTipoTempEf.SelectedItem = cmbTipoTempEf.Items.FindByValue("17");


                                    var lstDocumentoMov = new List<CRegistroDocumento>();
                                    dgvDocumentos.DataSource = lstDocumentoMov;
                                    dgvDocumentos.DataBind();
                                }
                                else
                                {
                                    //todo No implementado.
                                }


                                #endregion
                            }
                            else if (param.Contains("STARTEDIT"))
                            {
                                #region editar registro

                                var index = dgvTransacciones.EditingRowVisibleIndex;
                                if (index >= 0)
                                {
                                    MCargaListados();

                                    Session.Remove(CVariablesSesion.GrillaAuxiliar);
                                    cmbEstadoEf.Enabled = false;
                                    txtIdEf.Text = dgvTransacciones.GetRowValues(index, "ID_MOVIMIENTO").ToString();
                                    detFechaEf.Date = DateTime.Parse(dgvTransacciones.GetRowValues(index, "STR_FECHA").ToString());

                                    if (Convert.ToInt32(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()) == (int)EnumActividades.DistribucionGasRedes)
                                    {
                                        var json_referencia = JsonSerializer.DeserializeFromString<O_JSON_REFERENCIA>(dgvTransacciones.GetRowValues(index, "JSON_REFERENCIA").ToString());

                                        txtCodUsuario.Text = json_referencia.StrCodUsuario.ToString();
                                        txtCodUbicacion.Text = json_referencia.StrCodUbicacion.ToString();
                                        txtCiudadPoblacion.Text = json_referencia.StrCiudadPoblacion.ToString();
                                        txtCategoria.Text = json_referencia.StrCategoria.ToString();


                                        txtDireccion.Text = json_referencia.StrDireccion.ToString();
                                        cmbSectorEconomico.Value = json_referencia.StrSectorEcon.ToString();
                                        //txtTipoMedidor.Text = json_referencia.StrTipoMedidor.ToString(); //devulve null
                                        txtCodMedidor.Text = json_referencia.DecCodMedidor.ToString();

                                        txtLecMedidor.Text = json_referencia.DecLecMedidor.ToString();
                                        txtFechaLec.Text = json_referencia.StrFecha.ToString();

                                        txtPresionPsig.Text = json_referencia.DecPresionPsig.ToString();
                                        txtFactorCorrec.Text = json_referencia.DecFactorCorrec.ToString();
                                        //  txtCantFactura.Text = json_referencia.DecCantFactura.ToString();
                                        txtCantNoCorregido.Text = json_referencia.DecCantNoCorregido.ToString();
                                        txtImportAdicional.Text = json_referencia.DecImportAdicional.ToString();
                                    }
                                    else
                                    {
                                        //A implementar 
                                    }

                                    detFechaEf.ClientEnabled = true;
                                    if (Convert.ToInt32(cmbTipoOperacion.Value) == (int)EnumTipoOperaciones.SaldoInicial)
                                        detFechaEf.ClientEnabled = false;

                                    cmbEstadoEf.Value = Convert.ToInt32(dgvTransacciones.GetRowValues(index, "ID_ESTADO_MOVIMIENTO"));
                                    cmbTipoFlujoEf.SelectedItem = cmbTipoFlujoEf.Items.FindByValue(dgvTransacciones.GetRowValues(index, "ID_TIPO_FLUJO").ToString());
                                    cmbTipoInfEf.SelectedItem = cmbTipoInfEf.Items.FindByValue(dgvTransacciones.GetRowValues(index, "TIPO_INFRAESTRUCTURA").ToString());
                                    cmbTanqueEf.SelectedItem = cmbTanqueEf.Items.FindByValue(dgvTransacciones.GetRowValues(index, "TIPO_INFRAESTRUCTURA").ToString().Trim().Replace("-", "") + dgvTransacciones.GetRowValues(index, "ID_INFRAESTRUCTURA").ToString().Trim().Replace("-", ""));
                                    cmbTipoTransEf.SelectedItem = cmbTipoTransEf.Items.FindByValue(dgvTransacciones.GetRowValues(index, "ID_TIPO_MEDIO_TRANSPORTE").ToString());
                                    cmbMedioTransEf.SelectedItem = dgvTransacciones.GetRowValues(index, "ID_MEDIO_TRANSPORTE").ToString().Trim() == "-99999" ? cmbMedioTransEf.Items.FindByValue("9999999999") : cmbMedioTransEf.Items.FindByValue(dgvTransacciones.GetRowValues(index, "ID_TIPO_MEDIO_TRANSPORTE").ToString().Trim().Replace("-", "") + dgvTransacciones.GetRowValues(index, "ID_MEDIO_TRANSPORTE").ToString().Trim().Replace("-", ""));
                                    cmbEntidadAuxialiarEf.SelectedItem = cmbEntidadAuxialiarEf.Items.FindByValue(dgvTransacciones.GetRowValues(index, "ID_ENTIDAD").ToString());
                                    cmbTipoMedicEf.SelectedItem = cmbTipoMedicEf.Items.FindByValue(dgvTransacciones.GetRowValues(index, "ID_TIPO_MEDICION").ToString());
                                    cmbTipoMovEf.SelectedItem = cmbTipoMovEf.Items.FindByValue(dgvTransacciones.GetRowValues(index, "ID_TIPO_MOVIMIENTO").ToString());
                                    cmbPaisEf.SelectedItem = cmbPaisEf.Items.FindByValue(dgvTransacciones.GetRowValues(index, "ID_UBICACION_OPERACION").ToString());
                                    txtClienteEf.Text = (dgvTransacciones.GetRowValues(index, "RAZON_SOCIAL") == null) ? "" : (dgvTransacciones.GetRowValues(index, "TIPO_TRANSACCION").ToString().Contains("FACTURA")) ? dgvTransacciones.GetRowValues(index, "RAZON_SOCIAL").ToString() : "";
                                    txtNitEf.Text = (dgvTransacciones.GetRowValues(index, "NIT_CONSUMIDOR") == null) ? "" : dgvTransacciones.GetRowValues(index, "NIT_CONSUMIDOR").ToString();
                                    txtPlacaEf.Text = (dgvTransacciones.GetRowValues(index, "IDENTIFICADOR") == null) ? "" : dgvTransacciones.GetRowValues(index, "IDENTIFICADOR").ToString();
                                    txtSubIdentEf.Text = (dgvTransacciones.GetRowValues(index, "SUB_IDENTIFICADOR") == null) ? "" : dgvTransacciones.GetRowValues(index, "SUB_IDENTIFICADOR").ToString();
                                    txtVolAguaEf.Text = (dgvTransacciones.GetRowValues(index, "CANTIDAD_ADICIONAL") == null) ? "" : dgvTransacciones.GetRowValues(index, "CANTIDAD_ADICIONAL").ToString();
                                    cmbProdEf.SelectedItem = cmbProdEf.Items.FindByValue(dgvTransacciones.GetRowValues(index, "ID_HIDROCARBURO").ToString());
                                    cmbUnidadEf.SelectedItem = cmbUnidadEf.Items.FindByValue(dgvTransacciones.GetRowValues(index, "ID_UNIDAD_HIDROCARBURO").ToString());
                                    txtCantidadEf.Text = dgvTransacciones.GetRowValues(index, "CANTIDAD").ToString();
                                    txtObsEf.Text = (dgvTransacciones.GetRowValues(index, "OBSERVACION_MOVIMIENTO") == null) ? "" : dgvTransacciones.GetRowValues(index, "OBSERVACION_MOVIMIENTO").ToString();
                                    txtAdicionalEf.Text = (dgvTransacciones.GetRowValues(index, "ID_FUENTE_ORIGEN") == null) ? ((int)EnumTipoRegistro.OrigenWeb).ToString() : dgvTransacciones.GetRowValues(index, "ID_FUENTE_ORIGEN").ToString();
                                    txtCodigoEf.Text = (dgvTransacciones.GetRowValues(index, "CODIGO") == null) ? "" : dgvTransacciones.GetRowValues(index, "CODIGO").ToString();
                                    cmbTipoEnvaseEf.SelectedItem = cmbTipoEnvaseEf.Items.FindByValue(dgvTransacciones.GetRowValues(index, "ID_UNIDAD_ENVASES").ToString());
                                    cmbTipoTempEf.SelectedItem = cmbTipoTempEf.Items.FindByValue(dgvTransacciones.GetRowValues(index, "ID_UNIDAD_TEMPERATURA").ToString());
                                    txtTemperaturaEf.Text = (dgvTransacciones.GetRowValues(index, "TEMPERATURA") == null) ? "" : dgvTransacciones.GetRowValues(index, "TEMPERATURA").ToString();
                                    txtGravedadEf.Text = (dgvTransacciones.GetRowValues(index, "GRADO_API") == null) ? "" : dgvTransacciones.GetRowValues(index, "GRADO_API").ToString();
                                    txtIntegracionEf.Text = (dgvTransacciones.GetRowValues(index, "ID_INTEGRACION") == null) ? "" : dgvTransacciones.GetRowValues(index, "ID_INTEGRACION").ToString();
                                    txtBarCodeEf.Text = (dgvTransacciones.GetRowValues(index, "BARCODE") == null) ? "" : dgvTransacciones.GetRowValues(index, "BARCODE").ToString();

                                    txtJsonRef.Text = (dgvTransacciones.GetRowValues(index, "JSON_REFERENCIA") == null) ? "" : dgvTransacciones.GetRowValues(index, "JSON_REFERENCIA").ToString();
                                    txtEstJsonRef.Text = (dgvTransacciones.GetRowValues(index, "ESTADO_JSON_REFERENCIA") == null) ? "0" : dgvTransacciones.GetRowValues(index, "ESTADO_JSON_REFERENCIA").ToString();
                                    txtIdCuenta.Text = (dgvTransacciones.GetRowValues(index, "ID_CUENTA") == null) ? "0" : dgvTransacciones.GetRowValues(index, "ID_CUENTA").ToString();
                                    txtIdCuentaAux.Text = (dgvTransacciones.GetRowValues(index, "ID_CUENTA_AUX") == null) ? "0" : dgvTransacciones.GetRowValues(index, "ID_CUENTA_AUX").ToString();
                                    txtIdParGl.Text = (dgvTransacciones.GetRowValues(index, "ID_PARAM_GLOBAL") == null) ? "0" : dgvTransacciones.GetRowValues(index, "ID_PARAM_GLOBAL").ToString();
                                    txtIdParEs.Text = (dgvTransacciones.GetRowValues(index, "ID_PARAM_ESPEC") == null) ? "0" : dgvTransacciones.GetRowValues(index, "ID_PARAM_ESPEC").ToString();
                                    //txtIdTipoCons.Text = (dgvTransacciones.GetRowValues(index, "ID_TIPO_CONSUMIDOR") == null) ? "0" : dgvTransacciones.GetRowValues(index, "ID_TIPO_CONSUMIDOR").ToString();
                                    txtIdTipoInfAux.Text = (dgvTransacciones.GetRowValues(index, "ID_TIPO_INFRA_AUX") == null) ? "0" : dgvTransacciones.GetRowValues(index, "ID_TIPO_INFRA_AUX").ToString();
                                    txtIdInfAux.Text = (dgvTransacciones.GetRowValues(index, "ID_INFRA_AUX") == null) ? "0" : dgvTransacciones.GetRowValues(index, "ID_INFRA_AUX").ToString();

                                    cmbTipoDespachoEf.SelectedItem = cmbTipoDespachoEf.Items.FindByValue(dgvTransacciones.GetRowValues(index, "ID_TIPO_CONSUMIDOR").ToString());
                                    /*aqui controla cargado*/

                                    var ar = dgvTransacciones.GetRowValues(index, "ID_FUENTE_ORIGEN").ToString();
                                    var br = dgvTransacciones.GetRowValues(index, "ID_MOVIMIENTO").ToString();
                                    var objTransaccion = _client.Post<EResultadoTransaccion>(CAppSettings.svc_obtener_transaccion,
                                    new
                                    {
                                        decFiltro = "1",
                                        strParametro1 = dgvTransacciones.GetRowValues(index, "ID_MOVIMIENTO").ToString(),
                                        strParametro2 = dgvTransacciones.GetRowValues(index, "ID_FUENTE_ORIGEN").ToString(),
                                        strParametro3 = "0",
                                        strParametro4 = "0",
                                        strParametro5 = "0"
                                    });
                                    if (objTransaccion != null && objTransaccion.intCodigo == 1)
                                    {
                                        List<O_LISTA_TRANSACCION_CTY> lstResultado = objTransaccion.oResultado;
                                        lstResultado.ForEach(a => a.FECHA_HORA = a.FECHA_HORA_EMISION.ToString().Substring(6, 2) + "/" + a.FECHA_HORA_EMISION.ToString().Substring(4, 2) + "/" + a.FECHA_HORA_EMISION.ToString().Substring(0, 4) + " " + a.FECHA_HORA_EMISION.ToString().Substring(8, 2) + ":" + a.FECHA_HORA_EMISION.ToString().Substring(10, 2) + ":" + a.FECHA_HORA_EMISION.ToString().Substring(12, 2));
                                        lstResultado.ForEach(a => a.CANTIDAD = Convert.ToDecimal(dgvTransacciones.GetRowValues(index, "CANTIDAD").ToString()));
                                        lstResultado.ForEach(a => a.ID_PADRE_TRANSACCION = MObtenerParametro(a.ID_TIPO_TRANSACCION));

                                        Session[CVariablesSesion.GrillaAuxiliar] = lstResultado;
                                        dgvDocumentos.DataSource = lstResultado;
                                        dgvDocumentos.DataBind();
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
            }
            else
            {
                //todo No implementado.
            }
        }

        protected void dgvTransacciones_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    if (cmbTipoOperacion.Text == "")
                    {
                        e.Cancel = true;
                        dgvTransacciones.CancelEdit();
                        return;
                    }

                    var strTipoHab = (Session[CVariablesSesion.HabilitaBotones] == null) ? "ND" : Session[CVariablesSesion.HabilitaBotones].ToString();
                    if (strTipoHab != "MODSI")
                    {
                        e.Cancel = true;
                        dgvTransacciones.CancelEdit();
                        return;
                    }

                    if ((int)tlsOperadores.FocusedNode.GetValue("IntAccion") > 0)
                    {
                        #region Instancia componentes
                        ASPxDateEdit detFechaEf = (dgvTransacciones.FindEditFormTemplateControl("detFechaEf")) as ASPxDateEdit;
                        ASPxComboBox cmbEstadoEf = (dgvTransacciones.FindEditFormTemplateControl("cmbEstadoEf")) as ASPxComboBox;
                        ASPxComboBox cmbTipoMedicEf = (dgvTransacciones.FindEditFormTemplateControl("cmbTipoMedicEf")) as ASPxComboBox;
                        ASPxComboBox cmbTipoMovEf = (dgvTransacciones.FindEditFormTemplateControl("cmbTipoMovEf")) as ASPxComboBox;
                        ASPxComboBox cmbPaisEf = (dgvTransacciones.FindEditFormTemplateControl("cmbPaisEf")) as ASPxComboBox;
                        ASPxTextBox txtPlacaEf = (dgvTransacciones.FindEditFormTemplateControl("txtPlacaEf")) as ASPxTextBox;
                        ASPxTextBox txtSubIdentEf = (dgvTransacciones.FindEditFormTemplateControl("txtSubIdentEf")) as ASPxTextBox;
                        ASPxTextBox txtVolAguaEf = (dgvTransacciones.FindEditFormTemplateControl("txtVolAguaEf")) as ASPxTextBox;
                        ASPxComboBox cmbProdEf = (dgvTransacciones.FindEditFormTemplateControl("cmbProdEf")) as ASPxComboBox;
                        ASPxComboBox cmbUnidadEf = (dgvTransacciones.FindEditFormTemplateControl("cmbUnidadEf")) as ASPxComboBox;
                        ASPxTextBox txtCantidadEf = (dgvTransacciones.FindEditFormTemplateControl("txtCantidadEf")) as ASPxTextBox;
                        ASPxTextBox txtObsEf = (dgvTransacciones.FindEditFormTemplateControl("txtObsEf")) as ASPxTextBox;
                        ASPxComboBox cmbTipoFlujoEf = (dgvTransacciones.FindEditFormTemplateControl("cmbTipoFlujoEf")) as ASPxComboBox;
                        ASPxTextBox txtCodigoEf = (dgvTransacciones.FindEditFormTemplateControl("txtCodigoEf")) as ASPxTextBox;
                        ASPxComboBox cmbTipoTransEf = (dgvTransacciones.FindEditFormTemplateControl("cmbTipoTransEf")) as ASPxComboBox;
                        ASPxComboBox cmbMedioTransEf = (dgvTransacciones.FindEditFormTemplateControl("cmbMedioTransEf")) as ASPxComboBox;
                        ASPxComboBox cmbTipoInfEf = (dgvTransacciones.FindEditFormTemplateControl("cmbTipoInfEf")) as ASPxComboBox;
                        ASPxComboBox cmbTanqueEf = (dgvTransacciones.FindEditFormTemplateControl("cmbTanqueEf")) as ASPxComboBox;
                        ASPxComboBox cmbTipoEnvaseEf = (dgvTransacciones.FindEditFormTemplateControl("cmbTipoEnvaseEf")) as ASPxComboBox;
                        ASPxComboBox cmbTipoTempEf = (dgvTransacciones.FindEditFormTemplateControl("cmbTipoTempEf")) as ASPxComboBox;
                        ASPxTextBox txtTemperaturaEf = (dgvTransacciones.FindEditFormTemplateControl("txtTemperaturaEf")) as ASPxTextBox;
                        ASPxTextBox txtGravedadEf = (dgvTransacciones.FindEditFormTemplateControl("txtGravedadEf")) as ASPxTextBox;
                        ASPxTextBox txtIntegracionEf = (dgvTransacciones.FindEditFormTemplateControl("txtIntegracionEf")) as ASPxTextBox;
                        ASPxComboBox cmbEntidadAuxialiarEf = (dgvTransacciones.FindEditFormTemplateControl("cmbEntidadAuxialiarEf")) as ASPxComboBox;
                        ASPxTextBox txtBarCodeEf = (dgvTransacciones.FindEditFormTemplateControl("txtBarCodeEf")) as ASPxTextBox;

                        var cmbTipoDespachoEf = (dgvTransacciones.FindEditFormTemplateControl("cmbTipoDespachoEf")) as ASPxComboBox;





                        #endregion

                        try
                        {
                            var datFechaDoc = DateTime.Parse(detFechaEf.Text);
                            var datFechaReg = DateTime.Now;
                            var objNuevoReg = new ObjMovimientoTransaccion();

                            objNuevoReg.idMovimiento = 0;
                            objNuevoReg.idMovimientoPadre = 0;
                            objNuevoReg.idTipoActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                            objNuevoReg.idTipoOperacion = Convert.ToDecimal(cmbTipoOperacion.Value);
                            objNuevoReg.idConsumidor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString());
                            objNuevoReg.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                            objNuevoReg.fechaRegistroMovimiento = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                            objNuevoReg.fechaOperacion = Convert.ToDecimal(datFechaDoc.Year + datFechaDoc.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaDoc.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaDoc.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaDoc.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaDoc.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                            objNuevoReg.idTipoMovimientoFlujo = Convert.ToDecimal(cmbTipoFlujoEf.Value);
                            objNuevoReg.idTipoMovimiento = (cmbTipoMovEf.Value == null) ? -99999 : Convert.ToDecimal(cmbTipoMovEf.Value);
                            objNuevoReg.idEstadoMovimiento = Convert.ToDecimal(cmbEstadoEf.Value);
                            objNuevoReg.idFuenteOrigenDato = (decimal)EnumTipoRegistro.OrigenWeb;
                            objNuevoReg.idUbicacionOperacion = Convert.ToDecimal(cmbPaisEf.Value ?? "120");
                            objNuevoReg.idUnidadMedidaHidrocarburo = Convert.ToDecimal(cmbUnidadEf.Value);
                            objNuevoReg.idHidrocarburo = Convert.ToDecimal(cmbProdEf.Value);
                            objNuevoReg.cantidadHidrocarburo = Convert.ToDecimal(txtCantidadEf.Text);
                            objNuevoReg.identificador = txtPlacaEf.Text;
                            objNuevoReg.subIdentificador = txtSubIdentEf.Text;
                            objNuevoReg.observacion = txtObsEf.Text.Trim();
                            objNuevoReg.barcode = Convert.ToDecimal((txtBarCodeEf.Text == "") ? "0" : txtBarCodeEf.Text);

                            objNuevoReg.idTipoMedicion = (cmbTipoMedicEf.Value == null) ? -99999 : Convert.ToDecimal(cmbTipoMedicEf.Value);
                            objNuevoReg.cantidadAguaLitros = Convert.ToDecimal(txtVolAguaEf.Text);

                            objNuevoReg.idTipoMedioTransporte = (cmbTipoTransEf.Value == null) ? -99999 : Convert.ToDecimal(cmbTipoTransEf.Value);
                            if (cmbMedioTransEf.Value != null && cmbMedioTransEf.Value.ToString() != "9999999999")
                            {
                                var l1 = cmbTipoTransEf.Value.ToString().Trim().Replace("-", "").Length;
                                var l2 = cmbMedioTransEf.Value.ToString().Trim().Replace("-", "").Length;
                                var nCod = cmbMedioTransEf.Value.ToString().Substring(l1, l2 - l1);
                                objNuevoReg.idMedioTransporte = Convert.ToDecimal(nCod);
                            }
                            else
                            {
                                objNuevoReg.idMedioTransporte = -99999;
                            }

                            objNuevoReg.tipoInfraestructura = (cmbTipoInfEf.Value == null) ? -99999 : Convert.ToDecimal(cmbTipoInfEf.Value);
                            if (cmbTanqueEf.Value != null && cmbTanqueEf.Value.ToString() != "9999999999")
                            {
                                var l1 = cmbTipoInfEf.Value.ToString().Trim().Replace("-", "").Length;
                                var l2 = cmbTanqueEf.Value.ToString().Trim().Replace("-", "").Length;
                                var nCod = cmbTanqueEf.Value.ToString().Substring(l1, l2 - l1);
                                objNuevoReg.idInfraestructura = Convert.ToDecimal(nCod);
                            }
                            else
                            {
                                objNuevoReg.idInfraestructura = -99999;
                            }

                            objNuevoReg.idUnidadEnvasesTransporte = (cmbTipoEnvaseEf.Value == null) ? -99999 : Convert.ToDecimal(cmbTipoEnvaseEf.Value);
                            objNuevoReg.codigo = txtCodigoEf.Text.Trim();

                            objNuevoReg.idIntegracion = Convert.ToDecimal(txtIntegracionEf.Text);
                            objNuevoReg.idUnidadTemperatura = (cmbTipoTempEf.Value == null) ? -99999 : Convert.ToDecimal(cmbTipoTempEf.Value);
                            objNuevoReg.temperatura = Convert.ToDecimal(txtTemperaturaEf.Text);
                            objNuevoReg.gradoApi = txtGravedadEf.Text;
                            objNuevoReg.idEntidad = (cmbEntidadAuxialiarEf.Value == null) ? -99999 : Convert.ToDecimal(cmbEntidadAuxialiarEf.Value);

                            objNuevoReg.cantidadEnvasesTransporte = 0;
                            objNuevoReg.binarioControl = "00000-0000000000-0000000000-00000";
                            objNuevoReg.idTransaccionBaseExterna = "";

                            objNuevoReg.idTipoConsumidor = Convert.ToDecimal(cmbTipoDespachoEf.Value);

                            objNuevoReg.jsonReferencia = "";
                            objNuevoReg.estadoJsonRef = -99999;
                            objNuevoReg.idCuenta = -99999;
                            objNuevoReg.idCuentaAux = -99999;
                            objNuevoReg.idParamGlobal = -99999;
                            objNuevoReg.idParamEspec = -99999;
                            objNuevoReg.idTipoInfraAux = -99999;
                            objNuevoReg.idInfraAux = -99999;

                            objNuevoReg.listaTransaccion = new List<ObjTransaccion>();

                            List<O_LISTA_TRANSACCION_CTY> lstDocumentosMov;
                            lstDocumentosMov = (List<O_LISTA_TRANSACCION_CTY>)Session[CVariablesSesion.GrillaAuxiliar];
                            lstDocumentosMov = lstDocumentosMov.OrderBy(x => x.ID_TIPO_TRANSACCION).ToList();
                            foreach (var reg in lstDocumentosMov)
                            {
                                var objTrasaccion = new ObjTransaccion();
                                objTrasaccion.idMovimiento = 0;
                                objTrasaccion.idTransaccion = 0;
                                objTrasaccion.idTransaccionPadre = 0;
                                objTrasaccion.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                objTrasaccion.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                objTrasaccion.idTipoTransaccion = Convert.ToDecimal(reg.ID_TIPO_TRANSACCION);
                                objTrasaccion.fechaEmision = reg.FECHA_HORA_EMISION;
                                objTrasaccion.numeroTransaccion = reg.NRO_TRANSACCION.Trim();
                                objTrasaccion.numeroAutorizacion = Convert.ToDecimal(reg.NRO_AUTORIZACION);
                                objTrasaccion.codigoControl = reg.CODIGO_CONTROL;
                                objTrasaccion.nitConsumidor = reg.NIT_CONSUMIDOR;
                                objTrasaccion.razonSocial = reg.RAZON_SOCIAL;
                                objTrasaccion.precioUnitario = Convert.ToDecimal(reg.PRECIO_UNITARIO);
                                objTrasaccion.precioTotal = Convert.ToDecimal(reg.PRECIO_TOTAL);
                                objTrasaccion.usuarioTransaccion = reg.USUARIO_TRANSACCION;
                                objTrasaccion.observaciones = reg.OBSERVACIONES;
                                objTrasaccion.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                objTrasaccion.idEstadoTransaccion = objNuevoReg.idEstadoMovimiento;
                                objTrasaccion.idTipoCreditoFiscal = Convert.ToDecimal(reg.ID_TIPO_CREDITO_FISCAL);
                                objTrasaccion.idFormaPago = Convert.ToDecimal(reg.ID_FORMA_PAGO);
                                objTrasaccion.idTipoMoneda = Convert.ToDecimal(reg.ID_TIPO_MONEDA);
                                objTrasaccion.idTipoEmisor = Convert.ToDecimal(reg.ID_TIPO_EMISOR);
                                objTrasaccion.codigoSeguridad = 0;
                                objTrasaccion.idAutorizacionDato = 0;
                                objTrasaccion.binarioControl = "0000000000";

                                objTrasaccion.fechaOperacion = Convert.ToDecimal(datFechaDoc.Year + datFechaDoc.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaDoc.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaDoc.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaDoc.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaDoc.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0')); //CFechas.ConvierteDateTimeLong(datFechaDoc);
                                objTrasaccion.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                objTrasaccion.codigo = "";
                                objTrasaccion.codigoAutorizacion = "";//esnumero(values[15].Trim()) == true ? "" : values[15].Trim();
                                objNuevoReg.listaTransaccion.Add(objTrasaccion);
                            }
                            var objRegistro = new ERegistraMovimientoTransaccion();
                            objRegistro.strCredencial = CParametrosHydro.strCredencialFuncionario;
                            objRegistro.decFiltro = 1;
                            var lstRegistros = new List<ObjMovimientoTransaccion>();
                            lstRegistros.Add(objNuevoReg);
                            objRegistro.listaMovimientoTransaccion = lstRegistros;

                            //var ojo = JsonSerializer.SerializeToString(objRegistro);
                            var resultado = _client.Post<EResultado>(CAppSettings.svc_registra_movimiento_transaccion, objRegistro);
                            if (resultado.intCodigo == 1)
                            {
                                e.Cancel = true;
                                Session.Remove(CVariablesSesion.AuxActualiza);
                                dgvTransacciones.CancelEdit();
                                dgvTransacciones_CustomCallback(null, null);
                            }
                            else
                            {
                                MImprimirMensaje(resultado.strMensaje, EnumTiposMensajeAlerta.Alerta);
                            }
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
                        e.Cancel = true;
                        dgvTransacciones.CancelEdit();
                    }

                    #endregion
                }
                catch (Exception exp)
                {
                    var argEx = new ArgumentException(exp.Message);
                    throw argEx;
                }
            }
            else
            {
                //todo No implementado.
            }
        }

        protected void dgvTransacciones_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    var strTipoHab = (Session[CVariablesSesion.HabilitaBotones] == null) ? "ND" : Session[CVariablesSesion.HabilitaBotones].ToString();
                    if (strTipoHab != "MODSI")
                    {
                        e.Cancel = true;
                        dgvTransacciones.CancelEdit();
                        return;
                    }

                    if ((int)tlsOperadores.FocusedNode.GetValue("IntAccion") > 0)
                    {
                        EResultado resultado;
                        #region Instancia componentes
                        ASPxTextBox txtIdEf = (dgvTransacciones.FindEditFormTemplateControl("txtIdEf")) as ASPxTextBox;
                        ASPxDateEdit detFechaEf = (dgvTransacciones.FindEditFormTemplateControl("detFechaEf")) as ASPxDateEdit;
                        ASPxTextBox txtClienteEf = (dgvTransacciones.FindEditFormTemplateControl("txtClienteEf")) as ASPxTextBox;
                        ASPxTextBox txtNitEf = (dgvTransacciones.FindEditFormTemplateControl("txtNitEf")) as ASPxTextBox;
                        ASPxComboBox cmbPaisEf = (dgvTransacciones.FindEditFormTemplateControl("cmbPaisEf")) as ASPxComboBox;
                        ASPxTextBox txtPlacaEf = (dgvTransacciones.FindEditFormTemplateControl("txtPlacaEf")) as ASPxTextBox;
                        ASPxComboBox cmbTipoMovEf = (dgvTransacciones.FindEditFormTemplateControl("cmbTipoMovEf")) as ASPxComboBox;
                        ASPxComboBox cmbEstadoEf = (dgvTransacciones.FindEditFormTemplateControl("cmbEstadoEf")) as ASPxComboBox;
                        ASPxComboBox cmbTipoMedicEf = (dgvTransacciones.FindEditFormTemplateControl("cmbTipoMedicEf")) as ASPxComboBox;
                        ASPxComboBox cmbUnidadEf = (dgvTransacciones.FindEditFormTemplateControl("cmbUnidadEf")) as ASPxComboBox;
                        ASPxComboBox cmbProdEf = (dgvTransacciones.FindEditFormTemplateControl("cmbProdEf")) as ASPxComboBox;
                        ASPxTextBox txtCantidadEf = (dgvTransacciones.FindEditFormTemplateControl("txtCantidadEf")) as ASPxTextBox;
                        ASPxTextBox txtSubIdentEf = (dgvTransacciones.FindEditFormTemplateControl("txtSubIdentEf")) as ASPxTextBox;
                        ASPxTextBox txtObsEf = (dgvTransacciones.FindEditFormTemplateControl("txtObsEf")) as ASPxTextBox;
                        ASPxTextBox txtVolAguaEf = (dgvTransacciones.FindEditFormTemplateControl("txtVolAguaEf")) as ASPxTextBox;
                        ASPxComboBox cmbTipoFlujoEf = (dgvTransacciones.FindEditFormTemplateControl("cmbTipoFlujoEf")) as ASPxComboBox;
                        ASPxTextBox txtCodigoEf = (dgvTransacciones.FindEditFormTemplateControl("txtCodigoEf")) as ASPxTextBox;
                        ASPxComboBox cmbTipoTransEf = (dgvTransacciones.FindEditFormTemplateControl("cmbTipoTransEf")) as ASPxComboBox;
                        ASPxComboBox cmbMedioTransEf = (dgvTransacciones.FindEditFormTemplateControl("cmbMedioTransEf")) as ASPxComboBox;
                        ASPxComboBox cmbTipoInfEf = (dgvTransacciones.FindEditFormTemplateControl("cmbTipoInfEf")) as ASPxComboBox;
                        ASPxComboBox cmbTanqueEf = (dgvTransacciones.FindEditFormTemplateControl("cmbTanqueEf")) as ASPxComboBox;
                        ASPxComboBox cmbTipoEnvaseEf = (dgvTransacciones.FindEditFormTemplateControl("cmbTipoEnvaseEf")) as ASPxComboBox;
                        ASPxComboBox cmbTipoTempEf = (dgvTransacciones.FindEditFormTemplateControl("cmbTipoTempEf")) as ASPxComboBox;
                        ASPxTextBox txtTemperaturaEf = (dgvTransacciones.FindEditFormTemplateControl("txtTemperaturaEf")) as ASPxTextBox;
                        ASPxTextBox txtGravedadEf = (dgvTransacciones.FindEditFormTemplateControl("txtGravedadEf")) as ASPxTextBox;
                        ASPxTextBox txtIntegracionEf = (dgvTransacciones.FindEditFormTemplateControl("txtIntegracionEf")) as ASPxTextBox;
                        ASPxComboBox cmbEntidadAuxialiarEf = (dgvTransacciones.FindEditFormTemplateControl("cmbEntidadAuxialiarEf")) as ASPxComboBox;
                        ASPxTextBox txtBarCodeEf = (dgvTransacciones.FindEditFormTemplateControl("txtBarCodeEf")) as ASPxTextBox;

                        var cmbTipoDespachoEf = (dgvTransacciones.FindEditFormTemplateControl("cmbTipoDespachoEf")) as ASPxComboBox;

                        /********************************* JSON ****************************************************/
                        ASPxTextBox txtCodUsuario = (dgvTransacciones.FindEditFormTemplateControl("txtCodUsuario")) as ASPxTextBox;
                        ASPxTextBox txtCodUbicacion = (dgvTransacciones.FindEditFormTemplateControl("txtCodUbicacion")) as ASPxTextBox;
                        ASPxTextBox txtCiudadPoblacion = (dgvTransacciones.FindEditFormTemplateControl("txtCiudadPoblacion")) as ASPxTextBox;
                        ASPxTextBox txtCategoria = (dgvTransacciones.FindEditFormTemplateControl("txtCategoria")) as ASPxTextBox;
                        ASPxTextBox txtDireccion = (dgvTransacciones.FindEditFormTemplateControl("txtDireccion")) as ASPxTextBox;
                        ASPxComboBox cmbSectorEconomico = (dgvTransacciones.FindEditFormTemplateControl("cmbSectorEconomico")) as ASPxComboBox;
                        ASPxTextBox txtTipoMedidor = (dgvTransacciones.FindEditFormTemplateControl("txtTipoMedidor")) as ASPxTextBox;
                        ASPxTextBox txtCodMedidor = (dgvTransacciones.FindEditFormTemplateControl("txtCodMedidor")) as ASPxTextBox;
                        ASPxTextBox txtFechaLec = (dgvTransacciones.FindEditFormTemplateControl("txtFechaLec")) as ASPxTextBox;
                        ASPxTextBox txtLecMedidor = (dgvTransacciones.FindEditFormTemplateControl("txtLecMedidor")) as ASPxTextBox;
                        ASPxTextBox txtPresionPsig = (dgvTransacciones.FindEditFormTemplateControl("txtPresionPsig")) as ASPxTextBox;
                        ASPxTextBox txtFactorCorrec = (dgvTransacciones.FindEditFormTemplateControl("txtFactorCorrec")) as ASPxTextBox;
                        // ASPxTextBox txtCantFactura = (dgvTransacciones.FindEditFormTemplateControl("txtCantFactura")) as ASPxTextBox;
                        ASPxTextBox txtCantNoCorregido = (dgvTransacciones.FindEditFormTemplateControl("txtCantNoCorregido")) as ASPxTextBox;
                        ASPxTextBox txtImportAdicional = (dgvTransacciones.FindEditFormTemplateControl("txtImportAdicional")) as ASPxTextBox;

                        ASPxLabel lblCodUsuario = (dgvTransacciones.FindEditFormTemplateControl("lblCodUsuario")) as ASPxLabel;
                        ASPxLabel lblCodUbicacion = (dgvTransacciones.FindEditFormTemplateControl("lblCodUbicacion")) as ASPxLabel;
                        ASPxLabel lblCiudadPoblacion = (dgvTransacciones.FindEditFormTemplateControl("lblCiudadPoblacion")) as ASPxLabel;
                        ASPxLabel lblCategoria = (dgvTransacciones.FindEditFormTemplateControl("lblCategoria")) as ASPxLabel;
                        ASPxLabel lblDireccion = (dgvTransacciones.FindEditFormTemplateControl("lblDireccion")) as ASPxLabel;
                        ASPxLabel lblSectorEcon = (dgvTransacciones.FindEditFormTemplateControl("lblSectorEcon")) as ASPxLabel;
                        ASPxLabel lblTipoMedidor = (dgvTransacciones.FindEditFormTemplateControl("lblTipoMedidor")) as ASPxLabel;
                        ASPxLabel lblCodMedidor = (dgvTransacciones.FindEditFormTemplateControl("lblCodMedidor")) as ASPxLabel;
                        ASPxLabel lblFechaLec = (dgvTransacciones.FindEditFormTemplateControl("lblFechaLec")) as ASPxLabel;
                        ASPxLabel lblLecMedido = (dgvTransacciones.FindEditFormTemplateControl("lblLecMedido")) as ASPxLabel;
                        ASPxLabel lblPresionPsig = (dgvTransacciones.FindEditFormTemplateControl("lblPresionPsig")) as ASPxLabel;
                        ASPxLabel lblFactorCorrec = (dgvTransacciones.FindEditFormTemplateControl("lblFactorCorrec")) as ASPxLabel;
                        ASPxLabel lblCantNoCorregido = (dgvTransacciones.FindEditFormTemplateControl("lblCantNoCorregido")) as ASPxLabel;
                        ASPxLabel lblImportAdicional = (dgvTransacciones.FindEditFormTemplateControl("lblImportAdicional")) as ASPxLabel;

                        if (Convert.ToInt32(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()) == (int)EnumActividades.DistribucionGasRedes)
                        {
                            txtCodUsuario.Visible = true;
                            txtCodUbicacion.Visible = true;
                            txtCiudadPoblacion.Visible = true;
                            txtCategoria.Visible = true;
                            txtDireccion.Visible = true;
                            cmbSectorEconomico.Visible = true;
                            txtTipoMedidor.Visible = true;
                            txtCodMedidor.Visible = true;
                            txtFechaLec.Visible = true;
                            txtLecMedidor.Visible = true;
                            txtPresionPsig.Visible = true;
                            txtFactorCorrec.Visible = true;
                            txtCantNoCorregido.Visible = true;
                            txtImportAdicional.Visible = true;

                            lblCodUsuario.Visible = true;
                            lblCodUbicacion.Visible = true;
                            lblCiudadPoblacion.Visible = true;
                            lblCategoria.Visible = true;
                            lblDireccion.Visible = true;
                            lblSectorEcon.Visible = true;
                            lblTipoMedidor.Visible = true;
                            lblCodMedidor.Visible = true;
                            lblFechaLec.Visible = true;
                            lblLecMedido.Visible = true;
                            lblPresionPsig.Visible = true;
                            lblFactorCorrec.Visible = true;
                            lblCantNoCorregido.Visible = true;
                            lblImportAdicional.Visible = true;
                        }
                        else
                        {
                            txtCodUsuario.Visible = false;
                            txtCodUbicacion.Visible = false;
                            txtCiudadPoblacion.Visible = false;
                            txtCategoria.Visible = false;
                            txtDireccion.Visible = false;
                            cmbSectorEconomico.Visible = false;
                            txtTipoMedidor.Visible = false;
                            txtCodMedidor.Visible = false;
                            txtFechaLec.Visible = false;
                            txtLecMedidor.Visible = false;
                            txtPresionPsig.Visible = false;
                            txtFactorCorrec.Visible = false;
                            txtCantNoCorregido.Visible = false;
                            txtImportAdicional.Visible = false;

                            lblCodUsuario.Visible = false;
                            lblCodUbicacion.Visible = false;
                            lblCiudadPoblacion.Visible = false;
                            lblCategoria.Visible = false;
                            lblDireccion.Visible = false;
                            lblSectorEcon.Visible = false;
                            lblTipoMedidor.Visible = false;
                            lblCodMedidor.Visible = false;
                            lblFechaLec.Visible = false;
                            lblLecMedido.Visible = false;
                            lblPresionPsig.Visible = false;
                            lblFactorCorrec.Visible = false;
                            lblCantNoCorregido.Visible = false;
                            lblImportAdicional.Visible = false;
                        }

                        /********************************************************************************************/

                        #endregion
                        try
                        {
                            var datFechaDoc = DateTime.Parse(detFechaEf.Text);
                            var datFechaReg = DateTime.Now;
                            var objNuevoReg = new ObjMovimientoTransaccion();

                            objNuevoReg.idMovimiento = Convert.ToDecimal(txtIdEf.Text);
                            objNuevoReg.idMovimientoPadre = 0;
                            objNuevoReg.idTipoActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                            objNuevoReg.idTipoOperacion = Convert.ToDecimal(cmbTipoOperacion.Value);
                            objNuevoReg.idConsumidor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString());
                            objNuevoReg.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                            objNuevoReg.fechaRegistroMovimiento = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                            objNuevoReg.fechaOperacion = Convert.ToDecimal(datFechaDoc.Year + datFechaDoc.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaDoc.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaDoc.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaDoc.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaDoc.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                            objNuevoReg.idTipoMovimientoFlujo = Convert.ToDecimal(cmbTipoFlujoEf.Value);
                            objNuevoReg.idTipoMovimiento = (cmbTipoMovEf.Value == null) ? (int)EnumTipoMovimiento.NacNac : Convert.ToDecimal(cmbTipoMovEf.Value);
                            objNuevoReg.idEstadoMovimiento = Convert.ToDecimal(cmbEstadoEf.Value);
                            objNuevoReg.idFuenteOrigenDato = (decimal)EnumTipoRegistro.OrigenWeb;
                            objNuevoReg.idUbicacionOperacion = Convert.ToDecimal((cmbPaisEf.Value == null) ? "120" : cmbPaisEf.Value);
                            objNuevoReg.idUnidadMedidaHidrocarburo = Convert.ToDecimal(cmbUnidadEf.Value);
                            objNuevoReg.idHidrocarburo = Convert.ToDecimal(cmbProdEf.Value);
                            objNuevoReg.cantidadHidrocarburo = Convert.ToDecimal(txtCantidadEf.Text);
                            objNuevoReg.identificador = (txtPlacaEf.Text == "") ? "0" : txtPlacaEf.Text;
                            objNuevoReg.subIdentificador = (txtSubIdentEf.Text == "") ? "0" : txtSubIdentEf.Text;
                            objNuevoReg.observacion = txtObsEf.Text.Trim();
                            objNuevoReg.idTipoMedicion = (cmbTipoMedicEf.Value == null) ? -99999 : Convert.ToDecimal(cmbTipoMedicEf.Value);
                            objNuevoReg.cantidadAguaLitros = Convert.ToDecimal(txtVolAguaEf.Text);
                            objNuevoReg.idTipoMedioTransporte = (cmbTipoTransEf.Value == null) ? -99999 : Convert.ToDecimal(cmbTipoTransEf.Value);
                            objNuevoReg.barcode = Convert.ToDecimal(txtBarCodeEf.Text);

                            if (cmbMedioTransEf.Value != null && cmbMedioTransEf.Value.ToString() != "9999999999")
                            {
                                var l1 = cmbTipoTransEf.Value.ToString().Trim().Replace("-", "").Length;
                                var l2 = cmbMedioTransEf.Value.ToString().Trim().Replace("-", "").Length;
                                var c = cmbMedioTransEf.Value.ToString().Substring(l1, l2 - l1);
                                objNuevoReg.idMedioTransporte = Convert.ToDecimal(c);
                            }
                            else
                            {
                                objNuevoReg.idMedioTransporte = -99999;
                            }

                            objNuevoReg.tipoInfraestructura = (cmbTipoInfEf.Value == null) ? -99999 : Convert.ToDecimal(cmbTipoInfEf.Value);
                            if (cmbTanqueEf.Value != null && cmbTanqueEf.Value.ToString() != "9999999999")
                            {
                                var l1 = cmbTipoInfEf.Value.ToString().Trim().Replace("-", "").Length;
                                var l2 = cmbTanqueEf.Value.ToString().Trim().Replace("-", "").Length;
                                var c = cmbTanqueEf.Value.ToString().Substring(l1, l2 - l1);
                                objNuevoReg.idInfraestructura = Convert.ToDecimal(c);
                            }
                            else
                            {
                                objNuevoReg.idInfraestructura = -99999;
                            }

                            objNuevoReg.idUnidadEnvasesTransporte = (cmbTipoEnvaseEf.Value == null) ? -99999 : Convert.ToDecimal(cmbTipoEnvaseEf.Value);
                            objNuevoReg.codigo = txtCodigoEf.Text.Trim();

                            objNuevoReg.idIntegracion = Convert.ToDecimal(txtIntegracionEf.Text);
                            objNuevoReg.idUnidadTemperatura = (cmbTipoTempEf.Value == null) ? -99999 : Convert.ToDecimal(cmbTipoTempEf.Value);
                            objNuevoReg.temperatura = Convert.ToDecimal(txtTemperaturaEf.Text);
                            objNuevoReg.gradoApi = txtGravedadEf.Text;
                            objNuevoReg.idEntidad = (cmbEntidadAuxialiarEf.Value == null) ? -99999 : Convert.ToDecimal(cmbEntidadAuxialiarEf.Value);

                            objNuevoReg.cantidadEnvasesTransporte = 0;
                            objNuevoReg.binarioControl = "00000-0000000000-0000000000-00000";

                            objNuevoReg.idTransaccionBaseExterna = "";

                            if (Convert.ToInt32(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()) == (int)EnumActividades.DistribucionGasRedes)
                            {
                                /*******************************json**********************************/
                                long lngNoRegCorrect = 1;
                                var cDato = new CMovimientoRedesGas();
                                cDato.LngId = lngNoRegCorrect;
                                cDato.StrCodUsuario = Convert.ToString(txtCodUsuario.Text);
                                cDato.StrCodUbicacion = Convert.ToString(txtCodUbicacion.Text);
                                cDato.StrCiudadPoblacion = Convert.ToString(txtCiudadPoblacion.Text);
                                cDato.StrCategoria = Convert.ToString(txtCategoria.Text);
                                cDato.DecNit = Convert.ToDecimal(txtNitEf.Text);
                                cDato.StrRazonSocial = Convert.ToString(txtClienteEf.Text);
                                cDato.StrDireccion = Convert.ToString(txtDireccion.Text);
                                cDato.StrSectorEcon = Convert.ToString(cmbSectorEconomico.Value);
                                cDato.StrProducto = "GN";
                                cDato.StrTipoMedidor = Convert.ToString(txtTipoMedidor.Text);
                                cDato.DecCodMedidor = Convert.ToString(txtCodMedidor.Text);//Convert.ToDecimal(txtCodMedidor.Text);
                                cDato.StrFecha = Convert.ToString(txtFechaLec.Text);
                                cDato.DecLecMedidor = Convert.ToDecimal(txtLecMedidor.Text);

                                cDato.DecTemperatura = Convert.ToDecimal(txtTemperaturaEf.Text);
                                cDato.DecPresionPsig = Convert.ToDecimal(txtPresionPsig.Text);
                                cDato.DecFactorCorrec = Convert.ToDecimal(txtFactorCorrec.Text);
                                // cDato.DecCantFactura = Convert.ToDecimal(txtCantFactura.Text);
                                cDato.DecCantNoCorregido = Convert.ToDecimal(txtCantNoCorregido.Text);
                                //cDato.DecImportAdicional = Convert.ToDecimal(txtImportAdicional.Text);

                                cDato.DecImportAdicional = Convert.ToDecimal(txtImportAdicional.Text);



                                //cDato.DecNroFactura = Convert.ToDecimal(txtNroFactura.Text);
                                // cDato.DecNroAutorizacion = Convert.ToDecimal(txtNoAutoEf.Text);
                                // cDato.StrCodigoControl = Convert.ToString(txtCodigoControl.Text);
                                // cDato.StrFechaFactura = Convert.ToString(detFechaDocEf.Text);
                                /*****************************************************************/

                                objNuevoReg.jsonReferencia = JsonSerializer.SerializeToString(cDato);
                            }
                            else
                            {
                                objNuevoReg.jsonReferencia = "";
                            }

                            objNuevoReg.estadoJsonRef = -99999;
                            objNuevoReg.idCuenta = -99999;
                            objNuevoReg.idCuentaAux = -99999;
                            objNuevoReg.idParamGlobal = -99999;
                            objNuevoReg.idParamEspec = -99999;
                            objNuevoReg.idTipoConsumidor = Convert.ToDecimal(cmbTipoDespachoEf.Value);
                            objNuevoReg.idTipoInfraAux = -99999;
                            objNuevoReg.idInfraAux = -99999;

                            objNuevoReg.listaTransaccion = new List<ObjTransaccion>();

                            var lstDocumentosMov = new List<O_LISTA_TRANSACCION_CTY>();
                            lstDocumentosMov = (List<O_LISTA_TRANSACCION_CTY>)Session[CVariablesSesion.GrillaAuxiliar];
                            foreach (var reg in lstDocumentosMov)
                            {
                                if (reg.ID_ESTADO_TRANSACCION == (decimal)EnumEstadoMov.Anulado)
                                {
                                    if (reg.ID_TRANSACCION > 0)
                                    {
                                        resultado = _client.Post<EResultado>(CAppSettings.svc_eliminar_transaccion,
                                            new
                                            {
                                                strCredencial = CParametrosHydro.strCredencialFuncionario,
                                                decIdTransaccion = reg.ID_TRANSACCION,
                                                decAppFechaRegistro = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0')),
                                                decAppIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario])
                                            });
                                        if (resultado.intCodigo != 1)
                                        {
                                            MImprimirMensaje(resultado.strMensaje, EnumTiposMensajeAlerta.Alerta);
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
                                    var objTrasaccion = new ObjTransaccion();
                                    objTrasaccion.idMovimiento = Convert.ToDecimal(txtIdEf.Text);
                                    objTrasaccion.idTransaccion = (reg.ID_TRANSACCION < 0) ? 0 : reg.ID_TRANSACCION;
                                    objTrasaccion.idTransaccionPadre = 0;
                                    objTrasaccion.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                    objTrasaccion.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                    objTrasaccion.idTipoTransaccion = Convert.ToDecimal(reg.ID_TIPO_TRANSACCION);
                                    objTrasaccion.fechaEmision = reg.FECHA_HORA_EMISION;
                                    objTrasaccion.numeroTransaccion = reg.NRO_TRANSACCION.Trim();
                                    objTrasaccion.numeroAutorizacion = Convert.ToDecimal(reg.NRO_AUTORIZACION);
                                    objTrasaccion.codigoControl = reg.CODIGO_CONTROL;

                                    if (reg.ID_PADRE_TRANSACCION == (int)EnumTipoTrasaccionPadre.Factura)
                                    {
                                        objTrasaccion.nitConsumidor = Convert.ToDecimal(txtNitEf.Text);
                                        objTrasaccion.razonSocial = txtClienteEf.Text.Trim();
                                    }
                                    else
                                    {
                                        objTrasaccion.nitConsumidor = reg.NIT_CONSUMIDOR;
                                        objTrasaccion.razonSocial = (reg.RAZON_SOCIAL == null) ? "" : reg.RAZON_SOCIAL.Trim();
                                    }

                                    objTrasaccion.precioUnitario = Convert.ToDecimal(reg.PRECIO_UNITARIO);
                                    objTrasaccion.precioTotal = Convert.ToDecimal(reg.PRECIO_TOTAL);
                                    objTrasaccion.usuarioTransaccion = reg.USUARIO_TRANSACCION;
                                    objTrasaccion.observaciones = reg.OBSERVACIONES;
                                    objTrasaccion.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                    objTrasaccion.idEstadoTransaccion = Convert.ToDecimal(cmbEstadoEf.Value);
                                    objTrasaccion.idTipoCreditoFiscal = Convert.ToDecimal(reg.ID_TIPO_CREDITO_FISCAL);
                                    objTrasaccion.idFormaPago = Convert.ToDecimal(reg.ID_FORMA_PAGO);
                                    objTrasaccion.idTipoMoneda = Convert.ToDecimal(reg.ID_TIPO_MONEDA);
                                    objTrasaccion.idTipoEmisor = Convert.ToDecimal(reg.ID_TIPO_EMISOR);
                                    objTrasaccion.codigoSeguridad = 0;
                                    objTrasaccion.idAutorizacionDato = 0;
                                    objTrasaccion.binarioControl = "0000000000";

                                    objTrasaccion.fechaOperacion = Convert.ToDecimal(datFechaDoc.Year + datFechaDoc.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaDoc.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaDoc.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaDoc.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaDoc.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                    objTrasaccion.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                    objTrasaccion.codigo = "";

                                    if (reg.ID_TRANSACCION > 0)
                                    {
                                        objNuevoReg.listaTransaccion.Add(objTrasaccion);
                                    }
                                    else
                                    {
                                        var objRegTransaccion = new ETransaccion();
                                        objRegTransaccion.strCredencial = CParametrosHydro.strCredencialFuncionario;
                                        objRegTransaccion.ObjTransaccion = objTrasaccion;
                                        var objResult = _client.Post<EResultadoGestion>(CAppSettings.svc_transaccion, objRegTransaccion);

                                        if (objResult.intCodigo == 1)
                                        {
                                            objTrasaccion.idTransaccion = objResult.oResultado.ID_TABLA;
                                            objNuevoReg.listaTransaccion.Add(objTrasaccion);
                                        }
                                        else
                                        {
                                            MImprimirMensaje(objResult.strMensaje, EnumTiposMensajeAlerta.Alerta);
                                        }
                                    }
                                }
                            }
                            var objRegistro = new EActualizaMovimientoTransaccion();
                            objRegistro.strCredencial = CParametrosHydro.strCredencialFuncionario;
                            var lstRegistros = new List<ObjMovimientoTransaccion>();
                            lstRegistros.Add(objNuevoReg);
                            objRegistro.listaMovimientoTransaccion = lstRegistros;

                            DevExpress.Web.Data.ASPxDataValidationEventArgs e1 = null;
                            resultado = _client.Post<EResultado>(CAppSettings.svc_actualiza_movimiento_transaccion, objRegistro);
                            if (resultado.intCodigo == 1)
                            {
                                e.Cancel = true;
                                Session.Remove(CVariablesSesion.AuxActualiza);
                                dgvTransacciones.CancelEdit();
                                dgvTransacciones_CustomCallback(null, null);
                                
                                //if (cmbTipoOperacion.Text.Equals("DEMASIA DE COMBUSTIBLE") || cmbTipoOperacion.Text.Equals("MERMA DE COMBUSTIBLE"))
                                //{
                                //    _gestionPg.RegistrarSaldoPg(Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdProyecto").ToString()), Convert.ToDecimal(cmbTipoOperacion.Value), Convert.ToDecimal(txtCantidadEf.Text), Convert.ToDecimal(cmbProdEf.Value));
                                //}
                            }
                            else
                            {
                                e1.RowError = "ERROR: WS " + resultado.strMensaje;
                            }
                            #region Traza consulta de informacion
                            var objConsulta = new EObjetoEnvio7ParametrosProto
                            {
                                strCredencial = CParametrosHydro.strCredencialFuncionario,
                                decFiltro = 0, //Filtro 1
                                strParametro1 = tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString(),
                                strParametro2 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
                                strParametro3 = (txtPlacaEf.Text == "") ? "0" : txtPlacaEf.Text,
                                strParametro4 = txtIdEf.Text,
                                strParametro5 = datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'), //fechaRegistroMovimiento
                                strParametro6 = datFechaDoc.Year + datFechaDoc.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaDoc.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaDoc.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaDoc.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaDoc.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'),  //fechaOperacion
                                strParametro7 = cmbTipoOperacion.Value.ToString()
                            };
                            string strAccion = JsonSerializer.SerializeToString(objConsulta);
                            string strSalida = objRegistro.listaMovimientoTransaccion.Count().ToString();
                            string mensaje = GetType().Name + "." + MethodBase.GetCurrentMethod().Name;
                            _cTrazaInf.MRegistraTrazaInf(strAccion, mensaje, CParametrosHydro.decIdAplicacion, Convert.ToDecimal(Session[CVariablesSesion.IdUsuario].ToString()), HttpContext.Current.Request.UserHostAddress, strSalida);
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
                        e.Cancel = true;
                        dgvTransacciones.CancelEdit();
                    }
                    #endregion
                }
                catch (Exception exp)
                {
                    var argEx = new ArgumentException(exp.Message);
                    throw argEx;
                }
            }
            else
            {
                //todo No implementado.
            }
        }

        protected void dgvTransacciones_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar
                    int index = dgvTransacciones.FocusedRowIndex;
                    long lngidventa = Convert.ToInt64(dgvTransacciones.GetRowValues(index, "ID_MOVIMIENTO"));
                    if (lngidventa > 0)
                    {
                        var datFechaReg = DateTime.Now;
                        var decFecha = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                        EResultado resultado;

                        if (e.ButtonID == "btnEliminar")
                        {
                            resultado = _client.Post<EResultado>(CAppSettings.svc_eliminar_movimiento,
                            new
                            {
                                strCredencial = CParametrosHydro.strCredencialFuncionario,
                                decIdMovimiento = lngidventa,
                                decAppFechaRegistro = decFecha,
                                decAppIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario])
                            });
                        }
                        else
                        {
                            resultado = _client.Post<EResultado>(CAppSettings.svc_actualiza_estado_movimiento,
                            new
                            {
                                strCredencial = CParametrosHydro.strCredencialFuncionario,
                                decIdMovimiento = lngidventa,
                                decEstado = Convert.ToInt32((e.ButtonID == "btnAnular") ? EnumEstadoMov.Anulado : EnumEstadoMov.Activo),
                                decAppFechaRegistro = decFecha,
                                decAppIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario])
                            });
                        }

                        if (resultado.intCodigo == 1)
                        {
                            Session.Remove(CVariablesSesion.AuxActualiza);
                            dgvTransacciones_CustomCallback(null, null);
                        }
                        else
                        {
                            MImprimirMensaje(resultado.strMensaje, EnumTiposMensajeAlerta.Error);
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

        protected void dgvTransacciones_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            try
            {
                #region Proceso a Ejecutar

                if (e.VisibleIndex >= 0)
                {
                    if (dgvTransacciones.GetRowValues(e.VisibleIndex, "ESTADO_MOVIMIENTO").ToString().Equals("A"))
                        e.Row.BackColor = Color.FromArgb(215, 135, 135);
                    else if (Convert.ToInt32(dgvTransacciones.GetRowValues(e.VisibleIndex, "ID_TIPO_FLUJO")) == (int)EnumTipoMovimientoFlujo.Contable)
                        e.Row.BackColor = Color.FromArgb(194, 193, 195);
                    else
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

        protected void dgvTransacciones_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            try
            {
                #region Proceso a Ejecutar

                var asPxSummaryItem = e.Item as ASPxSummaryItem;
                if (asPxSummaryItem != null)
                {
                    switch (asPxSummaryItem.Tag)
                    {
                        case "imp":
                            if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Start)
                                _totalImporte = 0;

                            if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Calculate)
                            {
                                if (Convert.ToInt32(e.GetValue("ID_ESTADO_MOVIMIENTO").ToString()) == (int)EnumEstadoMov.Activo && Convert.ToInt32(e.GetValue("ID_TIPO_FLUJO").ToString()) == (int)EnumTipoMovimientoFlujo.Fisico)
                                {
                                    _totalImporte += Convert.ToDecimal(e.GetValue("PRECIO_TOTAL").ToString());
                                }
                            }
                            if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                                e.TotalValue = _totalImporte;
                            break;
                        case "vol":
                            if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Start)
                                _totalCantidad = 0;

                            if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Calculate)
                            {
                                if (Convert.ToInt32(e.GetValue("ID_ESTADO_MOVIMIENTO").ToString()) == (int)EnumEstadoMov.Activo && Convert.ToInt32(e.GetValue("ID_TIPO_FLUJO").ToString()) == (int)EnumTipoMovimientoFlujo.Fisico)
                                {
                                    _totalCantidad += Convert.ToDecimal(e.GetValue("CANTIDAD").ToString());
                                }
                            }
                            if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                                e.TotalValue = _totalCantidad;
                            break;
                        case "est":
                            if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Start)
                            {
                                e.TotalValue = "#An:" + 0 + " #Ac:" + 0;
                                _totalAnulada = 0;
                                _totalValida = 0;
                            }
                            if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Calculate)
                            {
                                if (Convert.ToInt32(e.GetValue("ID_ESTADO_MOVIMIENTO").ToString()) == (int)EnumEstadoMov.Activo)
                                    _totalValida++;
                                else
                                    _totalAnulada++;
                            }
                            if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                                e.TotalValue = "#An:" + _totalAnulada + " #Ac:" + _totalValida;
                            break;
                        case "impRec":
                            if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Start)
                                _totalImporteRec = 0;

                            if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Calculate)
                            {
                                if (Convert.ToInt32(e.GetValue("ID_ESTADO_MOVIMIENTO").ToString()) == (int)EnumEstadoMov.Activo && Convert.ToInt32(e.GetValue("ID_TIPO_FLUJO").ToString()) == (int)EnumTipoMovimientoFlujo.Fisico)
                                {
                                    _totalImporteRec += Convert.ToDecimal(e.GetValue("PRECIO_TOTAL_TRANS").ToString());
                                }
                            }
                            if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                                e.TotalValue = _totalImporteRec;
                            break;

                        case "volRec":
                            if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Start)
                                _totalCantidadRec = 0;

                            if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Calculate)
                            {
                                if (Convert.ToInt32(e.GetValue("ID_ESTADO_MOVIMIENTO").ToString()) == (int)EnumEstadoMov.Activo && Convert.ToInt32(e.GetValue("ID_TIPO_FLUJO").ToString()) == (int)EnumTipoMovimientoFlujo.Fisico)
                                {
                                    _totalCantidadRec += Convert.ToDecimal(e.GetValue("CANTIDAD_TRANS").ToString());
                                }
                            }
                            if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                                e.TotalValue = _totalCantidadRec;
                            break;

                        default:
                            //todo No implementado.
                            break;
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

        protected void dgvTransacciones_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            try
            {
                #region Proceso a Ejecutar

                if (cmbTipoOperacion.Text == "")
                {
                    return;
                }

                string strMensaje = string.Empty;
                if (!MValidaDatos(ref strMensaje, Convert.ToInt32(cmbTipoOperacion.Value), (int)tlsOperadores.FocusedNode.GetValue("IntIdActividad")))
                {
                    e.RowError = strMensaje;
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

        protected void dgvTransacciones_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {
            if (cmbTipoOperacion.Value != null)
            {
                if (Convert.ToInt32(cmbTipoOperacion.Value) == 29 || Convert.ToInt32(cmbTipoOperacion.Value) == 31 || Convert.ToInt32(cmbTipoOperacion.Value) == 33)
                {
                    // Controlar el botón "New" (Nuevo Movimiento)
                    if (e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Edit)
                    {
                        // Ocultar en recepción, mostrar en merma
                        e.Visible = false;
                    }
                }
            }        
        }
        protected void dgvTransacciones_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
        {
            if (!MValidaSesion()) return;
            try
            {
                #region Proceso a Ejecutar
                                  
                var strTipoHab = (Session[CVariablesSesion.HabilitaBotones] == null) ? "ND" : Session[CVariablesSesion.HabilitaBotones].ToString();
                var column = (GridViewCommandColumn)dgvTransacciones.Columns[0];

                column.ShowNewButtonInHeader = strTipoHab == "MODSI";

                if (e.VisibleIndex == -1) return;
                if (cmbTipoOperacion.Value != null)
                {
                    if (Convert.ToInt32(cmbTipoOperacion.Value) == 29)
                    {
                        switch (strTipoHab)
                        {
                            case "MODSI":
                                if (e.ButtonID == "btnEliminar")
                                    e.Visible = DefaultBoolean.True;
                                if (e.ButtonID == "btnAnular")
                                    e.Visible = DefaultBoolean.True;
                                if (e.ButtonID == "btnActivar")
                                    e.Visible = DefaultBoolean.True;                        
                                break;
                            case "ELISI":
                                if (e.ButtonID == "btnAnular")
                                    e.Visible = DefaultBoolean.False;
                                if (e.ButtonID == "btnActivar")
                                    e.Visible = DefaultBoolean.False;
                                if (e.ButtonID == "btnEliminar")
                                    e.Visible = DefaultBoolean.True;
                                break;
                            default:
                                if (e.ButtonID == "btnAnular")
                                    e.Visible = DefaultBoolean.False;
                                if (e.ButtonID == "btnActivar")
                                    e.Visible = DefaultBoolean.False;
                                if (e.ButtonID == "btnEliminar")
                                    e.Visible = DefaultBoolean.False;
                                break;
                        }
                    }
                    else if (Convert.ToInt32(cmbTipoOperacion.Value) == 33 || Convert.ToInt32(cmbTipoOperacion.Value) == 31)
                    {
                        switch (strTipoHab)
                        {
                            case "MODSI":
                                if (e.ButtonID == "btnAnular")
                                    e.Visible = DefaultBoolean.False;
                                if (e.ButtonID == "btnActivar")
                                    e.Visible = DefaultBoolean.False;
                                if (e.ButtonID == "btnEliminar")
                                    e.Visible = DefaultBoolean.False;
                                break;
                            default:
                                if (e.ButtonID == "btnAnular")
                                    e.Visible = DefaultBoolean.False;
                                if (e.ButtonID == "btnActivar")
                                    e.Visible = DefaultBoolean.False;
                                if (e.ButtonID == "btnEliminar")
                                    e.Visible = DefaultBoolean.False;
                                break;
                        }
                    }
                    else
                    {
                        switch (strTipoHab)
                        {
                            case "ELISI":
                                if (e.ButtonID == "btnAnular")
                                    e.Visible = DefaultBoolean.False;
                                if (e.ButtonID == "btnActivar")
                                    e.Visible = DefaultBoolean.False;
                                if (e.ButtonID == "btnEliminar")
                                    e.Visible = DefaultBoolean.True;
                                break;
                            case "MODSI":
                                if (e.ButtonID == "btnAnular")
                                    e.Visible = DefaultBoolean.True;
                                if (e.ButtonID == "btnActivar")
                                    e.Visible = DefaultBoolean.True;
                                if (e.ButtonID == "btnEliminar")
                                    e.Visible = DefaultBoolean.True;
                                break;
                            default:
                                if (e.ButtonID == "btnAnular")
                                    e.Visible = DefaultBoolean.False;
                                if (e.ButtonID == "btnActivar")
                                    e.Visible = DefaultBoolean.False;
                                if (e.ButtonID == "btnEliminar")
                                    e.Visible = DefaultBoolean.False;
                                break;
                        }
                    }
                }

                //switch (strTipoHab)
                //{
                //    case "ELISI":
                //        if (e.ButtonID == "btnAnular")
                //            e.Visible = DefaultBoolean.False;
                //        if (e.ButtonID == "btnActivar")
                //            e.Visible = DefaultBoolean.False;
                //        if (e.ButtonID == "btnEliminar")
                //            e.Visible = DefaultBoolean.True;
                //        break;
                //    case "MODSI":
                //        if (e.ButtonID == "btnAnular")
                //            e.Visible = DefaultBoolean.True;
                //        if (e.ButtonID == "btnActivar")
                //            e.Visible = DefaultBoolean.True;
                //        if (e.ButtonID == "btnEliminar")
                //            e.Visible = DefaultBoolean.True;
                //        break;
                //    default:
                //        if (e.ButtonID == "btnAnular")
                //            e.Visible = DefaultBoolean.False;
                //        if (e.ButtonID == "btnActivar")
                //            e.Visible = DefaultBoolean.False;
                //        if (e.ButtonID == "btnEliminar")
                //            e.Visible = DefaultBoolean.False;
                //        break;
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

        protected void dgvDocumentos_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    var txtClienteEf = (dgvTransacciones.FindEditFormTemplateControl("txtClienteEf")) as ASPxTextBox;
                    var txtNitEf = (dgvTransacciones.FindEditFormTemplateControl("txtNitEf")) as ASPxTextBox;
                    var dgvDocumentos = (dgvTransacciones.FindEditFormTemplateControl("dgvDocumentos")) as ASPxGridView;
                    var cmbTipoDocEf = (dgvTransacciones.FindEditFormTemplateControl("cmbTipoDocEf")) as ASPxComboBox;

                    var lstDocumentoMov = new List<O_LISTA_TRANSACCION_CTY>();
                    if (Session[CVariablesSesion.GrillaAuxiliar] != null)
                    {
                        lstDocumentoMov = (List<O_LISTA_TRANSACCION_CTY>)Session[CVariablesSesion.GrillaAuxiliar];
                    }

                    if (Session[CVariablesSesion.GrillaAuxiliar] != null)
                    {
                        dgvDocumentos.DataSource = Session[CVariablesSesion.GrillaAuxiliar];
                        dgvDocumentos.DataBind();
                    }

                    string strIdDoc, strDoc;
                    var idDocumento = Convert.ToInt64(Session[CVariablesSesion.IdAuxiliar]);
                    if (idDocumento != 0)
                    {
                        var index = dgvDocumentos.FocusedRowIndex;
                        strIdDoc = dgvDocumentos.GetRowValues(index, "ID_TIPO_TRANSACCION").ToString();
                        strDoc = dgvDocumentos.GetRowValues(index, "TIPO_TRANSACCION") + ";" + txtIdPadre.Text.Trim();
                        lstDocumentoMov.RemoveAll(x => x.ID_TRANSACCION == idDocumento);
                    }
                    else
                    {
                        strIdDoc = cmbTipoDocEf.Value.ToString();
                        strDoc = cmbTipoDocEf.Text;
                    }

                    var strPadreTransaccion = strDoc.Split(';');
                    var rnd = new Random();
                    var regDoc = new O_LISTA_TRANSACCION_CTY();
                    regDoc.ID_TRANSACCION = (idDocumento > 0) ? idDocumento : rnd.Next(-99999, -1);
                    regDoc.FECHA_HORA_EMISION = Convert.ToDecimal(detFechaDocEf.Date.Year + detFechaDocEf.Date.Month.ToString().PadLeft(2, '0') + detFechaDocEf.Date.Day.ToString().PadLeft(2, '0') + detFechaDocEf.Date.Hour.ToString().PadLeft(2, '0') + detFechaDocEf.Date.Minute.ToString().PadLeft(2, '0') + detFechaDocEf.Date.Second.ToString().PadLeft(2, '0'));
                    regDoc.FECHA_HORA = detFechaDocEf.Date.ToString();
                    regDoc.ID_TIPO_TRANSACCION = Convert.ToInt32(strIdDoc);
                    regDoc.TIPO_TRANSACCION = strPadreTransaccion[0].Trim();
                    regDoc.ID_PADRE_TRANSACCION = Convert.ToDecimal(txtIdPadre.Text);
                    regDoc.TIPO_CREDITO_FISCAL = "";
                    regDoc.TIPO_EMISOR = cmbTipoEmisorEf.Text;
                    regDoc.FORMA_PAGO = cmbTipoPagoEf.Text;
                    regDoc.OBSERVACIONES = txtObsDocEf.Text;
                    regDoc.ID_ESTADO_TRANSACCION = 13;
                    regDoc.ID_TIPO_MONEDA = Convert.ToInt32(cmbMonedaEf.Value);
                    regDoc.USUARIO_TRANSACCION = txtUsuarioEf.Text;

                    switch (Convert.ToInt32(strPadreTransaccion[1].Trim()))
                    {
                        case (int)EnumTipoTrasaccionPadre.Factura:
                            regDoc.NRO_AUTORIZACION = txtNoAutoEf.Text == "" ? 0 : Convert.ToInt64(txtNoAutoEf.Text);
                            regDoc.NRO_TRANSACCION = txtNoFactEf.Text;
                            regDoc.CODIGO_CONTROL = txtCodCtrlEf.Text;
                            regDoc.PRECIO_UNITARIO = Convert.ToDecimal(txtPrecioEf.Text);
                            regDoc.CANTIDAD = Convert.ToDecimal(txtCantidadDocEf.Text);
                            regDoc.PRECIO_TOTAL = Convert.ToDecimal(txtImporteEf.Text);
                            regDoc.ID_TIPO_CREDITO_FISCAL = Convert.ToInt32(cmbTipoCredEf.Value);
                            regDoc.ID_TIPO_EMISOR = Convert.ToInt32(cmbTipoEmisorEf.Value);
                            regDoc.ID_FORMA_PAGO = Convert.ToInt32(cmbTipoPagoEf.Value);
                            regDoc.NIT_CONSUMIDOR = Convert.ToDecimal(txtNitEf.Text);
                            regDoc.RAZON_SOCIAL = txtClienteEf.Text.Trim();
                            break;
                        case (int)EnumTipoTrasaccionPadre.Autorizacion:
                            regDoc.NRO_AUTORIZACION = 0;
                            regDoc.NRO_TRANSACCION = txtNoDocRefEf.Text;
                            regDoc.CODIGO_CONTROL = "";
                            regDoc.PRECIO_UNITARIO = 0;
                            regDoc.CANTIDAD = 0;
                            regDoc.PRECIO_TOTAL = 0;
                            regDoc.ID_TIPO_CREDITO_FISCAL = (decimal)EnumTipoCredFiscal.No;
                            regDoc.ID_TIPO_EMISOR = -99999;
                            regDoc.ID_FORMA_PAGO = -99999;
                            regDoc.NIT_CONSUMIDOR = Convert.ToDecimal(txtCiEf.Text);
                            regDoc.RAZON_SOCIAL = txtResponsableEf.Text;
                            break;
                        default:
                            regDoc.NRO_AUTORIZACION = 0;
                            regDoc.NRO_TRANSACCION = txtNoDocRefEf.Text;
                            regDoc.CODIGO_CONTROL = "";
                            regDoc.PRECIO_UNITARIO = Convert.ToDecimal(txtPrecioEf.Text);
                            regDoc.CANTIDAD = Convert.ToDecimal(txtCantidadDocEf.Text);
                            regDoc.PRECIO_TOTAL = Convert.ToDecimal(txtImporteEf.Text);
                            regDoc.ID_TIPO_CREDITO_FISCAL = (decimal)EnumTipoCredFiscal.No;
                            regDoc.ID_TIPO_EMISOR = -99999;
                            regDoc.ID_FORMA_PAGO = Convert.ToInt32(cmbTipoPagoEf.Value);
                            regDoc.NIT_CONSUMIDOR = Convert.ToDecimal(txtCiEf.Text);
                            regDoc.RAZON_SOCIAL = txtResponsableEf.Text;
                            break;
                    }

                    lstDocumentoMov.Add(regDoc);

                    dgvDocumentos.DataSource = null;
                    dgvDocumentos.DataBind();
                    Session[CVariablesSesion.GrillaAuxiliar] = lstDocumentoMov;
                    dgvDocumentos.DataSource = lstDocumentoMov;
                    dgvDocumentos.DataBind();

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

        protected void dgvDocumentos_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    var dgvDocumentos = (dgvTransacciones.FindEditFormTemplateControl("dgvDocumentos")) as ASPxGridView;
                    if (Session[CVariablesSesion.GrillaAuxiliar] != null)
                    {
                        dgvDocumentos.DataSource = Session[CVariablesSesion.GrillaAuxiliar];
                        dgvDocumentos.DataBind();
                    }

                    int index = dgvDocumentos.FocusedRowIndex;
                    long lngIdDoc = Convert.ToInt64(dgvDocumentos.GetRowValues(index, "ID_TRANSACCION"));
                    if (lngIdDoc != 0)
                    {
                        var lstDocumentoMov = new List<O_LISTA_TRANSACCION_CTY>();
                        lstDocumentoMov = (List<O_LISTA_TRANSACCION_CTY>)Session[CVariablesSesion.GrillaAuxiliar];
                        if (e.ButtonID == "btnQuitar")
                        {
                            lstDocumentoMov.ForEach(a => a.ID_ESTADO_TRANSACCION = (a.ID_TRANSACCION == lngIdDoc) ? (int)EnumEstadoMov.Anulado : a.ID_ESTADO_TRANSACCION);
                        }
                        else
                        {
                            lstDocumentoMov.ForEach(a => a.ID_ESTADO_TRANSACCION = (a.ID_TRANSACCION == lngIdDoc) ? (int)EnumEstadoMov.Activo : a.ID_ESTADO_TRANSACCION);
                        }
                        Session[CVariablesSesion.GrillaAuxiliar] = lstDocumentoMov;
                        dgvDocumentos.DataSource = lstDocumentoMov;
                        dgvDocumentos.DataBind();
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

        protected void dgvDocumentos_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            try
            {
                #region Proceso a Ejecutar

                ASPxGridView dgvDocumentos = (dgvTransacciones.FindEditFormTemplateControl("dgvDocumentos")) as ASPxGridView;
                if (e.VisibleIndex >= 0)
                {
                    if (dgvDocumentos.GetRowValues(e.VisibleIndex, "ID_ESTADO_TRANSACCION").ToString() == ((int)EnumEstadoMov.Anulado).ToString())
                        e.Row.BackColor = Color.FromArgb(249, 131, 131);
                    else
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

        protected void pucDocumento_WindowCallback(object source, PopupWindowCallbackArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    cmbTipoPagoEf.ReadOnly = false;
                    var objCatalogo = (O_OBTENER_CATALOGOS_CTY)Session[CVariablesSesion.ListaParametrica];
                    cmbMonedaEf.DataSource = objCatalogo.CATALOGO_TIPOS_MONEDA;
                    cmbMonedaEf.DataBind();
                    cmbMonedaEf.SelectedItem = cmbMonedaEf.Items.FindByValue("1");

                    var lstParam = objCatalogo.CATALOGO_PARAMETRICAS_DOMINIO;
                    foreach (var param in lstParam)
                    {
                        var lstObtCatalogo = _client.Post<EResultadoParametricaGlobal>
                            (CAppSettings.svc_lista_parametrica_global,
                            new
                            {
                                decIdDominio = param.ID_CATALOGO
                            });

                        if (lstObtCatalogo.IntCodigo == 1)
                        {
                            List<O_PARAMETRICA_GLOBAL_CTY> objDefecto;
                            var objLstGlobal = lstObtCatalogo.OResultado;

                            switch (param.CODIGO)
                            {
                                case "FRMPAGO":
                                    objDefecto = objLstGlobal.Where(x => x.CODIGO == "EF").ToList();
                                    cmbTipoPagoEf.DataSource = objLstGlobal;
                                    cmbTipoPagoEf.DataBind();
                                    cmbTipoPagoEf.SelectedItem = cmbTipoPagoEf.Items.FindByValue(objDefecto.First().ID_PARAMETRICA_GLOBAL.ToString());
                                    break;
                                case "TEFACT":
                                    objDefecto = objLstGlobal.Where(x => x.CODIGO == "FES").ToList();
                                    cmbTipoEmisorEf.DataSource = objLstGlobal;
                                    cmbTipoEmisorEf.DataBind();
                                    cmbTipoEmisorEf.SelectedItem = cmbTipoEmisorEf.Items.FindByValue(objDefecto.First().ID_PARAMETRICA_GLOBAL.ToString());
                                    break;
                                case "TCFIS":
                                    objDefecto = objLstGlobal.Where(x => x.CODIGO == "FCF").ToList();
                                    cmbTipoCredEf.DataSource = objLstGlobal;
                                    cmbTipoCredEf.DataBind();
                                    cmbTipoCredEf.SelectedItem = cmbTipoCredEf.Items.FindByValue(objDefecto.First().ID_PARAMETRICA_GLOBAL.ToString());
                                    break;
                            }
                        }
                        else
                        {
                            //todo No implementado.
                        }
                    }

                    var detFechaEf = (dgvTransacciones.FindEditFormTemplateControl("detFechaEf")) as ASPxDateEdit;
                    var txtCantidadEf = (dgvTransacciones.FindEditFormTemplateControl("txtCantidadEf")) as ASPxTextBox;
                    var cmbTipoDocEf = (dgvTransacciones.FindEditFormTemplateControl("cmbTipoDocEf")) as ASPxComboBox;
                    var dgvDocumentos = (dgvTransacciones.FindEditFormTemplateControl("dgvDocumentos")) as ASPxGridView;
                    var cmbTipoFlujoEf = (dgvTransacciones.FindEditFormTemplateControl("cmbTipoFlujoEf")) as ASPxComboBox;

                    var txtClienteEf = (dgvTransacciones.FindEditFormTemplateControl("txtClienteEf")) as ASPxTextBox;//lmqc --- importadores

                    var strTipoTransaccion = cmbTipoDocEf.Text.Split(';');
                    txtIdPadre.Text = strTipoTransaccion[1];
                    if (Session[CVariablesSesion.GrillaAuxiliar] != null)
                    {
                        dgvDocumentos.DataSource = Session[CVariablesSesion.GrillaAuxiliar];
                        dgvDocumentos.DataBind();
                    }

                    txtCantidadDocEf.Text = txtCantidadEf.Text;

                    if (txtIdDocEf.Text == "1")
                    {
                        int index = dgvDocumentos.FocusedRowIndex;
                        if (index >= 0)
                        {
                            Session[CVariablesSesion.IdAuxiliar] = Convert.ToInt64(dgvDocumentos.GetRowValues(index, "ID_TRANSACCION").ToString());
                            lblIdDocEf.Text = dgvDocumentos.GetRowValues(index, "ID_TRANSACCION").ToString();
                            detFechaDocEf.Date = DateTime.Parse(dgvDocumentos.GetRowValues(index, "FECHA_HORA").ToString());
                            txtNoAutoEf.Text = dgvDocumentos.GetRowValues(index, "NRO_AUTORIZACION").ToString();
                            txtNoFactEf.Text = dgvDocumentos.GetRowValues(index, "NRO_TRANSACCION").ToString();
                            txtNoDocRefEf.Text = dgvDocumentos.GetRowValues(index, "NRO_TRANSACCION").ToString();
                            txtCodCtrlEf.Text = (dgvDocumentos.GetRowValues(index, "CODIGO_CONTROL") == null) ? "" : dgvDocumentos.GetRowValues(index, "CODIGO_CONTROL").ToString();
                            txtImporteEf.Text = dgvDocumentos.GetRowValues(index, "PRECIO_TOTAL").ToString();
                            txtPrecioEf.Text = dgvDocumentos.GetRowValues(index, "PRECIO_UNITARIO").ToString();
                            txtObsDocEf.Text = (dgvDocumentos.GetRowValues(index, "OBSERVACIONES") == null) ? "" : dgvDocumentos.GetRowValues(index, "OBSERVACIONES").ToString();
                            cmbTipoDocEf.SelectedItem = cmbTipoDocEf.Items.FindByValue(dgvDocumentos.GetRowValues(index, "ID_TIPO_TRANSACCION").ToString());
                            cmbTipoEmisorEf.SelectedItem = cmbTipoEmisorEf.Items.FindByValue(dgvDocumentos.GetRowValues(index, "ID_TIPO_EMISOR").ToString());
                            cmbTipoCredEf.SelectedItem = cmbTipoCredEf.Items.FindByValue(dgvDocumentos.GetRowValues(index, "ID_TIPO_CREDITO_FISCAL").ToString());
                            cmbTipoPagoEf.SelectedItem = cmbTipoPagoEf.Items.FindByValue(dgvDocumentos.GetRowValues(index, "ID_FORMA_PAGO").ToString());
                            cmbMonedaEf.SelectedItem = cmbMonedaEf.Items.FindByValue(dgvDocumentos.GetRowValues(index, "ID_TIPO_MONEDA").ToString());
                            txtUsuarioEf.Text = (dgvDocumentos.GetRowValues(index, "USUARIO_TRANSACCION") == null) ? "" : dgvDocumentos.GetRowValues(index, "USUARIO_TRANSACCION").ToString();
                            txtIdPadre.Text = dgvDocumentos.GetRowValues(index, "ID_PADRE_TRANSACCION").ToString();
                            txtResponsableEf.Text = (dgvDocumentos.GetRowValues(index, "RAZON_SOCIAL") == null) ? "" : dgvDocumentos.GetRowValues(index, "RAZON_SOCIAL").ToString();
                            txtCiEf.Text = (dgvDocumentos.GetRowValues(index, "NIT_CONSUMIDOR") == null) ? "" : dgvDocumentos.GetRowValues(index, "NIT_CONSUMIDOR").ToString();

                        }
                        else
                        {
                            pucDocumento.ShowOnPageLoad = false;
                            return;
                        }
                    }
                    else
                    {
                        Session[CVariablesSesion.IdAuxiliar] = 0;
                        lblIdDocEf.Text = "0";
                        detFechaDocEf.Date = detFechaEf.Date;
                        txtNoAutoEf.Text = "";
                        txtNoFactEf.Text = "";
                        txtCodCtrlEf.Text = "";
                        txtImporteEf.Text = "";
                        txtPrecioEf.Text = "";
                        txtNoDocRefEf.Text = "";
                        txtObsDocEf.Text = "";
                        txtUsuarioEf.Text = "";
                        txtResponsableEf.Text = "";
                        txtCiEf.Text = "";
                        if (Convert.ToInt32(cmbTipoFlujoEf.Value) == (int)EnumTipoMovimientoFlujo.Contable)
                        {
                            cmbTipoPagoEf.SelectedItem = cmbTipoPagoEf.Items.FindByValue(((int)EnumTiposPago.Prepago).ToString());
                            cmbTipoPagoEf.ReadOnly = true;
                        }
                    }

                    switch (Convert.ToInt32(txtIdPadre.Text))
                    {
                        case (int)EnumTipoTrasaccionPadre.Factura:
                            lblNoDocRefEf.Visible = false;
                            txtNoDocRefEf.Visible = false;
                            lblResponsableEf.Visible = false;
                            txtResponsableEf.Visible = false;
                            lblCiEf.Visible = false;
                            txtCiEf.Visible = false;
                            break;
                        case (int)EnumTipoTrasaccionPadre.Autorizacion:
                            lblNoAutoEf.Text = "No. Documento :";
                            lblTipoEmisorEf.Visible = false;
                            cmbTipoEmisorEf.Visible = false;
                            lblTipoCredEf.Visible = false;
                            cmbTipoCredEf.Visible = false;
                            lblNoAutoEf.Visible = false;
                            txtNoAutoEf.Visible = false;
                            lblNoFactEf.Visible = false;
                            txtNoFactEf.Visible = false;
                            lblCodCtrlEf.Visible = false;
                            txtCodCtrlEf.Visible = false;
                            lblImporteEf.Visible = false;
                            txtImporteEf.Visible = false;
                            lblPrecioEf.Visible = false;
                            txtPrecioEf.Visible = false;
                            lblTipoConsEf.Visible = false;
                            cmbTipoPagoEf.Visible = false;
                            break;
                        default:
                            lblNoAutoEf.Text = "No. Documento :";
                            lblTipoEmisorEf.Visible = false;
                            cmbTipoEmisorEf.Visible = false;
                            lblTipoCredEf.Visible = false;
                            cmbTipoCredEf.Visible = false;
                            lblNoAutoEf.Visible = false;
                            txtNoAutoEf.Visible = false;
                            lblNoFactEf.Visible = false;
                            txtNoFactEf.Visible = false;
                            lblCodCtrlEf.Visible = false;
                            txtCodCtrlEf.Visible = false;
                            break;
                    }
                    txtIdDocEf.ReadOnly = true;
                    txtIdPadre.ReadOnly = true;

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

        protected void btnExportarAcum_Click(object sender, EventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    dgeExportar.GridViewID = "dgvAcumulado";
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

        protected void dgvAcumulado_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            try
            {
                #region Proceso a Ejecutar

                if (e.RowType.ToString() == "Data" && e.VisibleIndex >= 0)
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
                else
                {

                    //todo No implementado.
                }
                if (Convert.ToInt32(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()) == (int)EnumActividades.DistribucionGasRedes)
                {

                    dgvAcumulado.Columns[49].Caption = "TOTAL VOLUMEN. M3";

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

        protected void dgvAcumulado_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
        {
            try
            {
                #region Proceso a Ejecutar

                if (Convert.ToInt32(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()) == 29)
                {
                    e.Cell.Attributes.Add("ondblclick", "onCellClick(" + e.VisibleIndex + ", '" + e.DataColumn.FieldName + "Glp" + "',this)");
                }
                else if (e.DataColumn.EditFormCaption == "IMPORTACIÓN DE PRODUCTO")
                {
                    e.Cell.Attributes.Add("ondblclick", "onCellClick(" + e.VisibleIndex + ", '" + e.DataColumn.FieldName + "Im" + "',this)");
                }
                else if (e.DataColumn.EditFormCaption == "CONSUMO PROPIO")
                {
                    e.Cell.Attributes.Add("ondblclick", "onCellClick(" + e.VisibleIndex + ", '" + e.DataColumn.FieldName + "Im" + "',this)");
                }
                else if (e.DataColumn.EditFormCaption == "NOTA DE DESPACHO")
                {
                    e.Cell.Attributes.Add("ondblclick", "onCellClick(" + e.VisibleIndex + ", '" + e.DataColumn.FieldName + "NoD" + "',this)");
                }
                else if (e.DataColumn.EditFormCaption == "COMERCIALIZACION DE PRODUCTO")
                {
                    e.Cell.Attributes.Add("ondblclick", "onCellClick(" + e.VisibleIndex + ", '" + e.DataColumn.FieldName + "Im" + "',this)");
                }
                else
                {
                    e.Cell.Attributes.Add("ondblclick", "onCellClick(" + e.VisibleIndex + ", '" + e.DataColumn.FieldName + "',this)");
                }

                if (Convert.ToInt32(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()) == (int)EnumActividades.DistribucionGasRedes)
                {

                    dgvAcumulado.Columns[49].Caption = "TOTAL VOLUMEN M3";

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

        protected void btnExportaMov_Click(object sender, EventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    if ((int)tlsOperadores.FocusedNode.GetValue("IntAccion") > 0)
                    {
                        if (Convert.ToInt32(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()) < 3)
                        {
                            var arrFiltro = cmbTipoOperacionExp.Text.Split(';');
                            var intFiltro = (arrFiltro[1].Contains("DS")) ? 2 : 1;
                            var r = cmbTipoOperacionExp.Value.ToString();

                            var o1 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(); // IdActividad
                            var o2 = tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString();//"1592572",// IdConsumidor
                            var o3 = cmbTipoOperacionExp.Value.ToString(); // id_tipo_operacion
                            var o4 = detFechaExpIni.Date.Year + detFechaExpIni.Date.Month.ToString().PadLeft(2, '0') + detFechaExpIni.Date.Day.ToString().PadLeft(2, '0'); //"20170101", //fechaInicio
                            var o5 = detFechaExpFin.Date.Year + detFechaExpFin.Date.Month.ToString().PadLeft(2, '0') + detFechaExpFin.Date.Day.ToString().PadLeft(2, '0'); //"20170131" //fechaFinal



                            var objResultado = _consulta.ListadoMovimientosCsv(
                            new EObjetoEnvio5ParametrosProto
                            {
                                strCredencial = CParametrosHydro.strCredencialFuncionario,
                                decFiltro = intFiltro,// 1: Recepciones| 2: Despachos
                                strParametro1 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(), // IdActividad
                                strParametro2 = tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString(),//"1592572",// IdConsumidor
                                strParametro3 = cmbTipoOperacionExp.Value.ToString(),// id_tipo_operacion
                                strParametro4 = detFechaExpIni.Date.Year + detFechaExpIni.Date.Month.ToString().PadLeft(2, '0') + detFechaExpIni.Date.Day.ToString().PadLeft(2, '0'),//"20170101", //fechaInicio
                                strParametro5 = detFechaExpFin.Date.Year + detFechaExpFin.Date.Month.ToString().PadLeft(2, '0') + detFechaExpFin.Date.Day.ToString().PadLeft(2, '0')//"20170131" //fechaFinal
                            });
                            if (objResultado.intCodigo == 1)
                            {
                                if (intFiltro == 2)
                                {
                                    var list = (List<O_LISTA_MOV_CSV_EESSDS_CTY>)objResultado.oResultado;
                                    if (list == null || list.Count == 0) return;

                                    Type tipo = list[0].GetType();
                                    string newLine = Environment.NewLine;

                                    MemoryStream ms1 = new MemoryStream();
                                    using (var sw = new StreamWriter(ms1))
                                    {
                                        object o = Activator.CreateInstance(tipo);
                                        PropertyInfo[] props = o.GetType().GetProperties();
                                        //sw.Write(string.Join(";", props.Select(d => d.Name).ToArray()) + newLine); // agregar cabecera

                                        foreach (O_LISTA_MOV_CSV_EESSDS_CTY item in list)
                                        {
                                            var row = string.Join(";", props.Select(d => item.GetType()
                                                                                            .GetProperty(d.Name)
                                                                                            .GetValue(item, null)
                                                                                            .ToString())
                                                                                    .ToArray());
                                            sw.Write(row + newLine);
                                        }
                                        sw.Flush();
                                        byte[] bytes1 = ms1.ToArray();
                                        ms1.Close();

                                        Response.Clear();
                                        Response.ContentType = "application/force-download";
                                        Response.AddHeader("content-disposition", "attachment;    filename=" + arrFiltro[0].Trim() + ".csv");
                                        Response.BinaryWrite(bytes1);
                                        Response.End();
                                    }
                                }
                                else
                                {
                                    var list = (List<O_LISTA_MOV_CSV_EESSRC_CTY>)objResultado.oResultado;
                                    if (list == null || list.Count == 0) return;

                                    Type tipo = list[0].GetType();
                                    string newLine = Environment.NewLine;

                                    MemoryStream ms1 = new MemoryStream();
                                    using (var sw = new StreamWriter(ms1))
                                    {
                                        object o = Activator.CreateInstance(tipo);
                                        PropertyInfo[] props = o.GetType().GetProperties();
                                        //sw.Write(string.Join(";", props.Select(d => d.Name).ToArray()) + newLine); // agregar cabecera

                                        foreach (O_LISTA_MOV_CSV_EESSRC_CTY item in list)
                                        {
                                            var row = string.Join(";", props.Select(d => item.GetType()
                                                                                            .GetProperty(d.Name)
                                                                                            .GetValue(item, null)
                                                                                            .ToString())
                                                                                    .ToArray());
                                            sw.Write(row + newLine);
                                        }
                                        sw.Flush();
                                        byte[] bytes1 = ms1.ToArray();
                                        ms1.Close();

                                        Response.Clear();
                                        Response.ContentType = "application/force-download";
                                        Response.AddHeader("content-disposition", "attachment;    filename=" + arrFiltro[0].Trim() + ".csv");
                                        Response.BinaryWrite(bytes1);
                                        Response.End();
                                    }
                                }

                                #region Traza consulta de informacion
                                var objConsulta = new EObjetoEnvio5ParametrosProto
                                {
                                    strCredencial = CParametrosHydro.strCredencialFuncionario,
                                    decFiltro = intFiltro,// 1: Recepciones| 2: Despachos
                                    strParametro1 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(), // IdActividad
                                    strParametro2 = tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString(),//"1592572",// IdConsumidor
                                    strParametro3 = cmbTipoOperacionExp.Value.ToString(),// id_tipo_operacion
                                    strParametro4 = detFechaExpIni.Date.Year + detFechaExpIni.Date.Month.ToString().PadLeft(2, '0') + detFechaExpIni.Date.Day.ToString().PadLeft(2, '0'),//"20170101", //fechaInicio
                                    strParametro5 = detFechaExpFin.Date.Year + detFechaExpFin.Date.Month.ToString().PadLeft(2, '0') + detFechaExpFin.Date.Day.ToString().PadLeft(2, '0')//"20170131" //fechaFinal
                                };
                                string strAccion = JsonSerializer.SerializeToString(objConsulta);
                                string strSalida = ((List<O_LISTA_MOV_CSV_EESSDS_CTY>)objResultado.oResultado).Count().ToString();
                                string mensaje = GetType().Name + "." + MethodBase.GetCurrentMethod().Name;
                                _cTrazaInf.MRegistraTrazaInf(strAccion, mensaje, CParametrosHydro.decIdAplicacion, Convert.ToDecimal(Session[CVariablesSesion.IdUsuario].ToString()), HttpContext.Current.Request.UserHostAddress, strSalida);
                                #endregion
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError", "alert('" + objResultado.strMensaje + "');", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError", "alert('Función no habilitada para la actividad seleccionada.');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError", "alert('Seleccione un proyecto.');", true);
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

        protected void pucExportacion_WindowCallback(object source, PopupWindowCallbackArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    detFechaExpIni.Date = detFechaIni.Date;
                    detFechaExpFin.Date = detFechaFin.Date;

                    cmbTipoOperacionExp.DataSource = null;
                    cmbTipoOperacionExp.DataBind();
                    cmbTipoOperacionExp.Text = "";

                    var lstTipoOp = Session[CVariablesSesion.ComboTiposOperacion] as List<O_TIPOS_OPE_ACT_CTY>;
                    if (lstTipoOp != null && lstTipoOp.Any())
                    {
                        lstTipoOp = lstTipoOp.Where(x => x.FACTOR_CALCULO != 0).ToList();
                        cmbTipoOperacionExp.DataSource = lstTipoOp;
                        cmbTipoOperacionExp.DataBind();
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

        protected void btnImportaSaldo_Click(object sender, EventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    if ((int)tlsOperadores.FocusedNode.GetValue("IntAccion") > 0)
                    {
                        if (Convert.ToInt32(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()) == 1)
                        {
                            //VALIDA: existe t.o. ingreso saldo inicial
                            var lstOpAct = Session[CVariablesSesion.ComboTiposOperacion] as List<O_TIPOS_OPE_ACT_CTY>;
                            lstOpAct = lstOpAct.Where(x => x.ID_TIPO_OPERACION == (int)EnumTipoOperaciones.SaldoInicial).ToList();
                            if (!lstOpAct.IsNullOrEmpty())
                            {
                                //VALIDA: no existe ddjj
                                if (Session[CVariablesSesion.HabilitaFunciones] != null)
                                {
                                    var detFechMov = DateTime.Parse("01/" + detFechaIni.Date.Month.ToString().PadLeft(2, '0') + "/" + detFechaIni.Date.Year);
                                    var detFech = detFechMov.AddDays(-1);
                                    var strPeriodo = detFech.Date.Year + "-" + detFech.Date.Month.ToString().PadLeft(2, '0') + "-01";

                                    var objResultado = _consulta.ImportaMovimientoDcod(
                                    new EObjetoEnvio2ParametrosProto
                                    {
                                        strCredencial = CParametrosHydro.strCredencialFuncionario,
                                        decFiltro = 1,// 1: Recepciones| 2: Despachos
                                        strParametro1 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(), // IdActividad
                                        strParametro2 = tlsOperadores.FocusedNode.GetValue("IntIdProyecto").ToString()// IdEntidad
                                    }, strPeriodo);
                                    if (objResultado.intCodigo == 1)
                                    {
                                        //Anular los saldos existentes.
                                        var decFechaIni = Convert.ToDecimal(detFechaIni.Date.Year + detFechaIni.Date.Month.ToString().PadLeft(2, '0') + "01");
                                        var tareasMovimientos = _gestion.TareasMovimientos(1, 1, Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString()), Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()), (decimal)EnumTipoOperaciones.SaldoInicial, decFechaIni, decFechaIni);
                                        if (tareasMovimientos.intCodigo == 1)
                                        {
                                            var strMensaje = string.Empty;
                                            var objSaldos = (List<O_LISTA_SALDOS_DCOD_CTY>)objResultado.oResultado;
                                            foreach (var reg in objSaldos)
                                            {
                                                if (reg.SALDO > 0)
                                                {
                                                    MRegistraSaldoDcod(detFechMov, reg, ref strMensaje);
                                                    if (strMensaje.Length > 0)
                                                    {
                                                        MImprimirMensaje(strMensaje, EnumTiposMensajeAlerta.Error);
                                                        return;
                                                    }
                                                }
                                            }
                                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError", "alert('Proceso ejecutado exitosamente.');", true);
                                            //todo averiguar como invocar callback sin error
                                            //pnlDatos_Callback(null, null);
                                        }
                                        else
                                        {
                                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError", "alert('" + tareasMovimientos.strMensaje + "');", true);
                                        }

                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError", "alert('" + objResultado.strMensaje + "');", true);
                                    }
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError", "alert('El periodo esta cerrado para modificaciones.');", true);
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError", "alert('El tipo de operación SALDO INICIAL no esta habilitado para el periodo.');", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError", "alert('Función no habilitada para la actividad seleccionada.');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError", "alert('Seleccione un proyecto.');", true);
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

        protected void btnImportaRec_Click(object sender, EventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    if ((int)tlsOperadores.FocusedNode.GetValue("IntAccion") > 0)
                    {
                        if (Convert.ToInt32(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()) == 1)
                        {
                            //VALIDA: existe t.o. ingreso x recepcion
                            var lstOpAct = Session[CVariablesSesion.ComboTiposOperacion] as List<O_TIPOS_OPE_ACT_CTY>;
                            lstOpAct = lstOpAct.Where(x => x.CODIGO.Contains("RCEX")).ToList();
                            if (!lstOpAct.IsNullOrEmpty())
                            {
                                //VALIDA: periodo habilitrado
                                if (Session[CVariablesSesion.HabilitaFunciones] != null)
                                {
                                    //VALIDA: fechas dentro el periodo
                                    if (detFechaImpIni.Date >= detFechaIni.Date && detFechaImpIni.Date <= detFechaFin.Date && detFechaImpFin.Date >= detFechaIni.Date && detFechaImpFin.Date <= detFechaFin.Date)
                                    {
                                        var strPeriodo = detFechaImpIni.Date.Year + "-" + detFechaImpIni.Date.Month.ToString().PadLeft(2, '0') + "-" + detFechaImpIni.Date.Day.ToString().PadLeft(2, '0');
                                        strPeriodo = strPeriodo + "|" + detFechaImpFin.Date.Year + "-" + detFechaImpFin.Date.Month.ToString().PadLeft(2, '0') + "-" + detFechaImpFin.Date.Day.ToString().PadLeft(2, '0');

                                        var objResultado = _consulta.ImportaMovimientoDcod(
                                        new EObjetoEnvio2ParametrosProto
                                        {
                                            strCredencial = CParametrosHydro.strCredencialFuncionario,
                                            decFiltro = 2,// 1: Saldo | 2: Recepciones
                                            strParametro1 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(), // IdActividad
                                            strParametro2 = tlsOperadores.FocusedNode.GetValue("IntIdProyecto").ToString()// IdEntidad
                                        }, strPeriodo);
                                        if (objResultado.intCodigo == 1)
                                        {
                                            var strMensaje = string.Empty;
                                            var objSaldos = (List<O_LISTA_RECEPCION_DCOD_CTY>)objResultado.oResultado;
                                            foreach (var reg in objSaldos)
                                            {
                                                if (reg.LITROS_FACTURA > 0)
                                                {
                                                    MRegistraRecepcionDcod(lstOpAct.FirstOrDefault().ID_TIPO_OPERACION, reg, ref strMensaje);
                                                    if (strMensaje.Length > 0)
                                                    {
                                                        MImprimirMensaje(strMensaje, EnumTiposMensajeAlerta.Error);
                                                        return;
                                                    }
                                                }
                                            }
                                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError", "alert('Proceso ejecutado exitosamente.');", true);
                                        }
                                        else
                                        {
                                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError", "alert('" + objResultado.strMensaje + "');", true);
                                        }
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError", "alert('Las fechas de importación deben estar dentro del periodo consultado.');", true);
                                    }
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError", "alert('El periodo esta cerrado para modificaciones.');", true);
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError", "alert('El tipo de operación INGRESO POR RECEPCION no esta habilitado.');", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError", "alert('Función no habilitada para la actividad seleccionada.');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError", "alert('Seleccione un proyecto.');", true);
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

        protected void pnlGrafico_Callback(object sender, CallbackEventArgsBase e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    if (cmbFiltro.Value == null || cmbFiltro.Text == "")
                        return;

                    var objAcumulado = (List<CMovAcumulado>)Session[CVariablesSesion.GrillaAcumulado];
                    var lstDatos = objAcumulado.Where(x => x.StrIdGrupoUnico == (string)cmbFiltro.Value).OrderBy(x => x.StrFecha).ToList();
                    var objRes = new O_GRAFICO_TIPO1();

                    var fechaIni = detFechaIni.Date;
                    var fechFin = detFechaFin.Date;
                    var td = fechaIni - fechFin;
                    var intDif = td.Days * -1;
                    var names = new string[intDif + 1];
                    var datFechaI = fechaIni;
                    int intCont;

                    for (int i = 0; i <= intDif; i++)
                    {
                        names[i] = datFechaI.ToShortDateString();
                        datFechaI = datFechaI.AddDays(1);
                    }
                    objRes.names = names;

                    var series = new List<O_SERIES>();
                    var strTitulos = Session[CVariablesSesion.GrillaTitulo] as string[];
                    for (int i = 0; i < strTitulos.Count(); i++)
                    {
                        if (strTitulos[i] != "")
                        {
                            var objSerie = new O_SERIES();
                            objSerie.name = strTitulos[i];
                            var arrSerie = new double[intDif + 1];
                            intCont = 0;
                            int intAux = 4;
                            if (i > 28)
                                intAux = 5;

                            foreach (var dat in lstDatos)
                            {
                                arrSerie[intCont] = (double)dat.VecValores[i - intAux];
                                intCont++;
                            }
                            objSerie.data = arrSerie;
                            series.Add(objSerie);
                        }
                    }
                    objRes.series = series;

                    var seriesSaldo = new List<O_SERIES>();
                    var objSerSi = new O_SERIES();
                    objSerSi.name = "SALDO INICIAL";
                    var objSerSf = new O_SERIES();
                    objSerSf.name = "SALDO FINAL";
                    var arrSerSi = new double[intDif + 1];
                    var arrSerSf = new double[intDif + 1];
                    intCont = 0;
                    foreach (var dat in lstDatos)
                    {
                        arrSerSi[intCont] = (double)dat.DecSaldoInicial;
                        arrSerSf[intCont] = (double)dat.DecSaldoFinal;
                        intCont++;
                    }
                    objSerSi.data = arrSerSi;
                    objSerSf.data = arrSerSf;
                    seriesSaldo.Add(objSerSi);
                    seriesSaldo.Add(objSerSf);

                    var panel = sender as ASPxCallbackPanel;
                    panel.JSProperties["cp_type"] = cmbTipoGrafico.Value;
                    panel.JSProperties["cp_title"] = "Movimiento Registrado";
                    panel.JSProperties["cp_subtitle"] = "Grupo: " + cmbFiltro.Text;
                    panel.JSProperties["cp_xAxis"] = objRes.names;
                    panel.JSProperties["cp_series"] = objRes.series;

                    panel.JSProperties["cp_titSal"] = "Saldos";
                    panel.JSProperties["cp_serSal"] = seriesSaldo;

                    /*if (Convert.ToInt32(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()) != (int)EnumActividades.DistribucionGasRedes)
                    {
                         panel.JSProperties["cp_titSal"] = "Saldos";
                         panel.JSProperties["cp_serSal"] = seriesSaldo;
                    }*/



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

        protected void pucImportaRec_WindowCallback(object source, PopupWindowCallbackArgs e)
        {
            detFechaImpIni.Date = detFechaIni.Date;
            detFechaImpFin.Date = detFechaFin.Date;
        }

    //    protected void pucRegistroPrecio_WindowCallback(object source, PopupWindowCallbackArgs e)
    //    {
    //        try
    //       {
    //           var p = e.Parameter.Split('|');
    //           var accion = p[0];
    //           if (Page.IsCallback && accion == "")
    //           {
    //               ConfigPrecio();
    //           }
    //           else 
    //           {
    //               //A implementar               
    //           }             
    //            //if (accion == "LISTAR")
    //            //{
    //            //    grdPrecios.DataSource = Lista;
    //            //    grdPrecios.DataBind();
    //            //    return;
    //            //}

    //            switch (accion)
    //            {
    //                case "REGISTRAR":
    //                    //var lstDespachos = _gestion.RegistraPrecioImpBSA(
    //                    //    new EObjetoEnvio8ParametrosProto
    //                    //    {
    //                    //        decFiltro = 0, //Filtro 1 por producto //2 consulta por infraestructura
    //                    //        strParametro1 = tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString(), //Id proyecto/entidad
    //                    //        strParametro2 = strFecha + "000000",//fecha inicial
    //                    //        strParametro3 = strFecha + "235959", //fecha final
    //                    //        strParametro4 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(), //id Actividad
    //                    //        strParametro5 = cmbTipoOperacionImp.Value.ToString(), //tipo operacion
    //                    //        strParametro6 = strPar6, //idProducto 
    //                    //        strParametro7 = strPar7, // id_infraestructura
    //                    //        strParametro8 = strPar8 // tipo_infraestructura
    //                    //    });
    //                    var Registrar = new EObjetoPrecioImportacion
    //                    {
    //                        decIdEESS=0,
    //                        decIdEntidad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdProyecto")),
    //                        decIdProducto = Convert.ToDecimal(cmbHidrocarburoPrecio.Value),
    //                        decVolumen = Convert.ToDecimal(txtVolumen.Value),
    //                        decPrecio = Convert.ToDecimal(txtPrecio.Value),
    //                        strImportadora = cmbEntidadImportadoras.Text,
    //                        strDispenser = txtDispenser.Text,
    //                        strManguera = txtManguera.Text,
    //                        decFechaDesde = CFechas.ConvierteDateTimeLong(detFechaDesde.Date),
    //                        decFechahasta = CFechas.ConvierteDateTimeLong(detFechaHasta.Date),
    //                        strObs = txtObsPrecioImp.Text.ToUpper().Trim()
    //                    };

                       //var RegistrarPrecioImp =  _gestion.RegistraPrecioImpBSA(CParametrosHydro.strCredencialFuncionario,
                       //                               Registrar.decIdEESS,
                       //                               Registrar.decIdEntidad,
                       //                               Registrar.decIdProducto,
                       //                               Registrar.decVolumen,
                       //                               Registrar.decPrecio,
                       //                               Registrar.strImportadora,
                       //                               Registrar.strDispenser,
                       //                               Registrar.strManguera,
                       //                               Registrar.decFechaDesde,
                       //                               Registrar.decFechahasta,
                       //                               Registrar.strObs,
                       //                               Registrar.decEstado
                       //                               );

                                                    
    //                    //Lista.Add(new PrecioVM
    //                    //{
    //                    //    ID_PRECIO = Lista.Count + 1,
    //                    //    //IMPORTADORA = txtImportadora.Text,
    //                    //    //PRODUCTO = txtProducto.Text,
                        
                        
    //                    //    PRECIO_VENTA = Convert.ToDecimal(txtPrecio.Text),
    //                    //    FECHA_DESDE = dtDesde.Date,
    //                    //    FECHA_HASTA = dtHasta.Date,
    //                    //    OBSERVACION = txtObs.Text,
    //                    //    ESTADO = rbActivo.Checked ? "ACTIVO" : "INACTIVO"
    //                    //});
    //                    break;

    //                case "EDITAR":
    //                    //int id = Convert.ToInt32(p[1]);
    //                    //var x = Lista.First(a => a.ID_PRECIO == id);
    //                    //x.PRECIO_VENTA = Convert.ToDecimal(txtPrecio.Text);                 
    //                    //x.ESTADO = rbActivo.Checked ? "ACTIVO" : "INACTIVO";
    //                    break;
    //                case "ELIMINAR":
    //                    //int id2 = Convert.ToInt32(p[1]);
    //                    //Lista.RemoveAll(a => a.ID_PRECIO == id2);
    //                    break;
    //            }                     

    //            grdPrecios.DataSource = Lista;
    //            grdPrecios.DataBind();
 
   

    ////           string[] param = e.Parameter.Split('|');
    ////string accion = param[0];
    ////int id = param.Length > 1 ? Convert.ToInt32(param[1]) : 0;

    ////if (accion == "LISTAR")
    ////{
    ////    grdPrecios.DataSource = ListaPrecios;
    ////    grdPrecios.DataBind();
    ////    return;
    ////}

    ////if (accion == "EDITAR")
    ////{
    ////    var item = ListaPrecios.First(x => x.ID_PRECIO == id);
    ////    item.PRECIO_VENTA = Convert.ToDecimal(txtPrecio.Text);
    ////    item.OBSERVACION = txtObsPrecio.Text;
    ////    item.ESTADO = rbActivo.Checked ? "ACTIVO" : "INACTIVO";
    ////}

    ////if (accion == "ELIMINAR")
    ////{
    ////    ListaPrecios.RemoveAll(x => x.ID_PRECIO == id);
    ////}

    ////grdPrecios.DataSource = ListaPrecios;
    ////grdPrecios.DataBind();
    //        }
    //        catch (Exception exp)
    //        {
    //            #region RegistraError


    //            var oMensajeUsuario = new CLogTraza.MensajeUsuario
    //            {
    //                decUsuarioId = Session[CVariablesSesion.IdUsuario],
    //                decIdModulo = CParametrosHydro.decIdAplicacion,
    //                strAccion = GetType().Name + "." + MethodBase.GetCurrentMethod().Name,
    //                strIp = HttpContext.Current.Request.UserHostAddress,
    //                decNivelCapa = (int)CLogTraza.CapasNivel.Presentacion
    //            };
    //            CLogTraza.Error(oMensajeUsuario, exp);

    //            #endregion
    //        }
    //    }

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
                                                                      "/WFFlujoOperador.aspx?format=json");
                if (lstResultado.decCodigo == 1)
                {
                    if (lstResultado.oResultado != null && lstResultado.oResultado.Any())
                    {
                        Session[CVariablesSesion.PermisoRevi] = lstResultado.oResultado.FirstOrDefault().REVISION;
                        Session[CVariablesSesion.PermisoBaja] = lstResultado.oResultado.FirstOrDefault().BAJAS;
                    }
                    else
                    {
                        Session[CVariablesSesion.PermisoRevi] = 0;
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
        private void ConfigurarFormulario()
        {
            detFechaIni.Date = DateTime.Parse("01/" + DateTime.Now.Month + "/" + DateTime.Now.Year);
            detFechaFin.Date = DateTime.Now;         
            cmbTipoOperacion.Enabled = true;
            detFechaRegSF.Date = DateTime.Now;                                 
        }


        /// <summary>
        /// METODO. MCargaArbol
        /// </summary>
        private void MCargaArbol()
        {
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
        /// METODO. MCargaParametricas
        /// </summary>
        private void MCargaParametricas()
        {
            //TIPO TRANSACCION
            var lstObjTransaccion = _client.Post<EResultadoTipoTransaccion>(CAppSettings.svc_lista_tipos_transaccion,
            new
            {
                strCredencial = CParametrosHydro.strCredencialFuncionario,
                decFiltro = 1,
                strParametro1 = "",
                strParametro2 = ""
            });
            if (lstObjTransaccion.IntCodigo == 1)
            {
                lstObjTransaccion.OResultado = lstObjTransaccion.OResultado.OrderBy(x => x.ID_TIPO_TRANSACCION_PADRE).ThenBy(x => x.NOMBRE).ToList();
                Session[CVariablesSesion.ListaTransaccion] = lstObjTransaccion.OResultado;
            }
            //CATALOGO
            var lstObtCatalogo = _client.Post<EResultadoParametrica>(CAppSettings.svc_obtener_catalogo,
            new
            {
                strLlave = ""
            });

            if (lstObtCatalogo.IntCodigo == 1)
            {
                O_OBTENER_CATALOGOS_CTY objCatalogo = lstObtCatalogo.OResultado;
                Session[CVariablesSesion.ListaParametrica] = objCatalogo;

                var lstSectorEconomico = _client.Post<EResultadoSectorEconomico>(CAppSettings.svc_lista_sector_economico,
                   new
                   {
                       strCredencial = ""
                   });

                List<O_SECTOR_ECONOMICO_CTY> objSectorEco = lstSectorEconomico.OResultado;
                Session[CVariablesSesion.ListaSectorEconomico] = objSectorEco;
            }
            else
            {
                //todo No implementado.
            }
            //PAIS
            var objParametricas = new O_PARAMETRICAS_GRAL();
            var objUbicacion = _client.Post<EResultadoUbicacion>(CAppSettings.svc_obtiene_ubicacion,
            new
            {
                strCredencial = CParametrosHydro.strCredencialFuncionario,
                decNivel = 1,
                decIdPadre = 0
            });
            objParametricas.lstPais = objUbicacion.OResultado;
            objParametricas.lstHidrocarburo = null;
            Session[CVariablesSesion.ListaParamGral] = objParametricas;

            //TIPO MEDIO TRANSPORTE -> ENVASES
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
                objParamInfraestrutura.oResultado.Add(new O_PARAMETROS_CTY() { ID_PARAMETRO = -99999, CODIGO = "NOID", VALOR = "No Identificado", DESCRIPCION = "No Identificado", ORDEN = 0, ID_DOMINIO_PARAMETRO = 0 });
                objParamInfra.TIPO_INFRAESTRUCTURA = objParamInfraestrutura.oResultado;
            }
            Session[CVariablesSesion.ListaParamInfra] = objParamInfra;
        }

        /// <summary>
        /// METODO. MCargaParametricasEspecificas
        /// </summary>
        private void MCargaParametricasEsp()
        {
            //PARAMETRICAS GENERALES


            var objParametros = _client.Post<EResultadoOctanoParametro>(CAppSettings.svc_listar_octano_parametrica,
            new
            {
                strCredencial = CParametrosHydro.strCredencialFuncionario,
                decFiltro = 1,//1: ID_TIPO_ACTIVIDAD 2: ID_TIPO_ACTIVIDAD y VALOR_TIPO
                strParametro1 = "",//VALOR_TIPO
                strParametro2 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
                strParametro3 = "",
                strParametro4 = ""
            });
            if (objParametros.IntCodigo == 1)
            {
                Session[CVariablesSesion.OctanoParametrica] = objParametros.OResultado;
            }
            else
            {
                Session.Remove(CVariablesSesion.OctanoParametrica);
            }
        }

        /// <summary>
        /// METODO. MValidaDuplicadas
        /// </summary>
        /// <param name="decIdConsumidor"></param>
        /// <param name="decIdTipoActividad"></param>
        /// <param name="decFechaHoraInicio"></param>
        /// <param name="decFechaHoraFin"></param>
        private void MValidaDuplicadas(decimal decIdConsumidor, decimal decIdTipoActividad, decimal decFechaHoraInicio, decimal decFechaHoraFin)
        {


            //Baja logica  los movimientos duplicados por anulacion B-Sisa
            var objResultado = _client.Post<EResultado>
                (CAppSettings.svc_verifica_tran_duplicada,
                new
                {
                    strCredencial = CParametrosHydro.strCredencialFuncionario,
                    decFiltro = 1,
                    decIdConsumidor,
                    decIdTipoActividad,
                    decFechaHoraInicio,
                    decFechaHoraFin
                });
            if (objResultado.intCodigo != 1)
            {
                MImprimirMensaje(objResultado.strMensaje, EnumTiposMensajeAlerta.Error);
            }
            else
            {
                //todo No implementado
            }
        }

        /// <summary>
        /// METODO. MCargaGrilla
        /// </summary>
        /// <param name="strParametro1"></param>
        /// <param name="strParametro2"></param>
        /// <param name="strParametro3"></param>
        /// <param name="strParametro4"></param>
        /// <param name="sender"></param>
        private void MCargaGrilla(string strParametro1, string strParametro2, string strParametro3, string strParametro4, object sender)
        {
            Session.Remove(CVariablesSesion.HabilitaFunciones);
            string strMensaje = string.Empty;
            if (MValidaPermisos(ref strMensaje))
            {
                Session[CVariablesSesion.HabilitaFunciones] = 1;
            }
            else
            {
                MImprimirMensaje(strMensaje, EnumTiposMensajeAlerta.Alerta);
            }

            for (int i = 27; i >= 8; i--)
            {
                dgvAcumulado.Columns[i].Visible = false;
                dgvAcumulado.Columns[21 + i].Visible = false;
            }
            var strTitulos = new string[100];
            for (int i = 0; i < 100; i++)
            {
                strTitulos[i] = "";
            }


            dgvAcumulado.Columns[0].Visible = true;
            dgvAcumulado.Columns[3].Caption = "FECHA";
            dgvAcumulado.Columns[6].Visible = true;
            dgvAcumulado.Columns[5].Visible = true;
            dgvAcumulado.Columns[7].Visible = true;

            dgvAcumulado.Columns[28].Visible = true;
            dgvAcumulado.Columns[49].Visible = true;
            dgvAcumulado.Columns[50].Visible = true; //saldo final
            dgvAcumulado.Columns[51].Visible = false; //importe total												   

            /*var lstTipoOperacion = Session[CVariablesSesion.ComboTiposOperacion] as List<O_TIPOS_OPE_ACT_CTY>;
            if (lstTipoOperacion != null && lstTipoOperacion.Any())
            {
                if (lstTipoOperacion.Where(x => x.FACTOR_CALCULO == 1).Count() == 0)
                {
                    dgvAcumulado.Columns[50].Visible = false;
                    dgvAcumulado.Columns[28].Visible = false;
                    dgvAcumulado.Columns[7].Visible = false;
                    dgvAcumulado.Columns[0].Visible = false;                 
                }
                else
                {
                    dgvAcumulado.DataColumns[53].GroupIndex = -1;
                    dgvAcumulado.DataColumns[53].Visible = false;
                    dgvAcumulado.DataColumns[4].GroupIndex = 1;
                    dgvAcumulado.DataColumns[5].GroupIndex = 2;
                }
            }
            else
            {
                // implementar
            }*/

            /***********************modificado*********************/
            if (Convert.ToInt32(strParametro1) == (int)EnumActividades.ImportacionLubricantes)
            {
                //var dgvAcumulado.colu
                dgvAcumulado.DataColumns[4].GroupIndex = 1;
                dgvAcumulado.DataColumns[5].GroupIndex = 2;
                dgvAcumulado.DataColumns[53].GroupIndex = 0;

            }
            else
            {
                dgvAcumulado.DataColumns[53].GroupIndex = -1;
                dgvAcumulado.DataColumns[53].Visible = false;
                dgvAcumulado.DataColumns[4].GroupIndex = 1;
                dgvAcumulado.DataColumns[5].GroupIndex = 2;
            }
            if (Convert.ToInt32(strParametro1) == (int)EnumActividades.EstacionServicioGnv)
            {
                dgvAcumulado.Columns[50].Visible = false;
                dgvAcumulado.Columns[28].Visible = false;
                dgvAcumulado.Columns[7].Visible = false;
                dgvAcumulado.Columns[0].Visible = false;
            }
            else if (Convert.ToInt32(strParametro1) == (int)EnumActividades.ImportacionCarburantes)
            {
                dgvAcumulado.Columns[50].Visible = false;
                dgvAcumulado.Columns[49].Visible = false;
                dgvAcumulado.Columns[29].Visible = false;
                dgvAcumulado.Columns[7].Visible = false;
                dgvAcumulado.Columns[0].Visible = false;
            }
            else if (Convert.ToInt32(strParametro1) == (int)EnumActividades.TransporteGasDucto)
            {
                dgvAcumulado.Columns[50].Visible = false;
                dgvAcumulado.Columns[7].Visible = false;

            }
            else if (Convert.ToInt32(strParametro1) == (int)EnumActividades.TransporteLiquidosDucto)
            {
                dgvAcumulado.Columns[50].Visible = false;
                dgvAcumulado.Columns[7].Visible = false;

            }
            else if (Convert.ToInt32(strParametro1) == (int)EnumActividades.DistribucionGasRedes)
            {
                dgvAcumulado.Columns[50].Visible = false;
                dgvAcumulado.Columns[28].Visible = false;
                dgvAcumulado.Columns[7].Visible = false;
                dgvAcumulado.Columns[0].Visible = false;

                //dgvAcumulado.DataColumns[4].GroupIndex = 1;
                //dgvAcumulado.DataColumns[5].GroupIndex = 2;
                //dgvAcumulado.Columns[3].Width = 170; //fecha
                //dgvAcumulado.Columns[7].Visible = false; //saldo inicial
                //dgvAcumulado.Columns[49].Visible = false; //total sal
                //dgvAcumulado.Columns[50].Visible = false; //saldo final                
            }
            else
            {
                //sin accion
            }

            var lstParam = Session[CVariablesSesion.OctanoParametrica] as List<O_LISTA_OCTANO_PAR_CTY>;
            var lstPar = lstParam.Where(x => x.TIPO_VALOR == 7).ToList();
            if (!lstPar.Any())
            {
                MImprimirMensaje("Parametro TIPO DE MANEJO DE SALDOS, No definido.", EnumTiposMensajeAlerta.Error);
                return;
            }
            if (lstPar.FirstOrDefault().VALOR_FIN != "2")
            {
                dgvAcumulado.DataColumns[4].GroupIndex = -1;
                dgvAcumulado.DataColumns[5].GroupIndex = -1;
                dgvAcumulado.Columns[5].Visible = false;
                dgvAcumulado.Columns[6].Visible = false;
            }
            var decFiltro = Convert.ToDecimal(lstPar.FirstOrDefault().VALOR_FIN);

            if (strParametro1 == "108")
            {
                decFiltro = 7;
            }
            else if (strParametro1 == "12")
            {
                decFiltro = 7;
            }
            else
            {
                //no implementado
            }

            var lstDespachos = _cUtil.MObtieneMovAcumulado(decFiltro, strParametro1, strParametro2, strParametro3, strParametro4, 2, -99999);



            if (lstDespachos.intCodigo == 1)
            {

                var lstResultadoLstAcumulado = (O_DECLARACION_JURADA_CTY)lstDespachos.oResultado;
                Session[CVariablesSesion.DjAcumulado] = lstResultadoLstAcumulado;
                var lstResultado = lstResultadoLstAcumulado.LstAcumulado;

                if (lstResultado.Any())
                {
                    #region acumulado
                    decimal decProd = 0;
                    int cont = 1, intContProd = 1;

                    lstResultado = lstResultado.OrderBy(x => x.DecIdHidrocarburo).ThenBy(x => x.DecFecha).ToList();
                    var lstMovimientos = new List<CMovAcumulado>();
                    foreach (var lst in lstResultado)
                    {
                        var cDato = new CMovAcumulado();
                        if (decProd != lst.DecIdHidrocarburo)
                        {
                            decProd = lst.DecIdHidrocarburo;
                            intContProd = 1;
                        }

                        cDato.IntId = cont;
                        cDato.IntContProd = intContProd;
                        cDato.StrFecha = lst.DecFecha.ToString().Substring(6, 2) + "/" + lst.DecFecha.ToString().Substring(4, 2) + "/" + lst.DecFecha.ToString().Substring(0, 4);
                        cDato.IntIdProducto = (int)lst.DecIdHidrocarburo;
                        cDato.StrProducto = lst.StrHidrocarburo;
                        cDato.StrCodProducto = lst.StrCodHidrocarburo;
                        cDato.StrUnidadMedida = lst.StrUnidadMedida;

                        cDato.DecIdTipoInfraestructura = lst.DecIdTipoInfraestructura;
                        cDato.StrTipoInfraestructura = lst.StrTipoInfraestructura ?? "";
                        cDato.DecIdInfraestructura = lst.DecIdInfraestructura;
                        cDato.StrInfraestructura = lst.StrInfraestructuraAnh ?? "";
                        cDato.StrCodigoInfraestructura = lst.StrInfraestructura ?? "";

                        cDato.StrIdGrupoUnico = "" + lst.DecIdHidrocarburo + lst.DecIdTipoInfraestructura + lst.DecIdInfraestructura;
                        //cDato.StrGrupoUnico = lst.StrCodHidrocarburo + " - " + lst.StrTipoInfraestructura + " - " + lst.StrInfraestructura;

                        cDato.DecSaldoInicial = lst.DecSaldoInicial;
                        cDato.DecTotalIng = lst.DecTotalIngreso;
                        cDato.DecSaldoFinal = lst.DecSaldoFinal;
                        cDato.DecTotalSal = lst.DecTotalEgreso;
                        cDato.DecImporteTotal = lst.DecTotalImporte;
                        cDato.StrObservaciones = lst.StrObservaciones;

                        var vecValores = new decimal[100];
                        vecValores[0] = lst.DecSaldoInicial;
                        vecValores[1] = lst.DecSaldoFinal;
                        vecValores[2] = lst.DecTotalIngreso;
                        vecValores[3] = lst.DecTotalEgreso;


                        if (lst.LstIngresos == null || !lst.LstIngresos.Any())
                        {
                            cDato.DecIng01 = -99999;
                        }
                        else
                        {
                            foreach (var reg in lst.LstIngresos)
                            {
                                if (!dgvAcumulado.Columns[7 + (int)reg.OPERACION_ORDEN].Visible)
                                {

                                    dgvAcumulado.Columns[7 + (int)reg.OPERACION_ORDEN].Visible = true;
                                    dgvAcumulado.Columns[7 + (int)reg.OPERACION_ORDEN].Caption = reg.OPERACION_NOMBRE;
                                    strTitulos[7 + (int)reg.OPERACION_ORDEN] = reg.OPERACION_NOMBRE;

                                }
                                cDato.DecIng01 = (reg.OPERACION_ORDEN == 1) ? reg.CANTIDAD : cDato.DecIng01;
                                cDato.DecIng02 = (reg.OPERACION_ORDEN == 2) ? reg.CANTIDAD : cDato.DecIng02;
                                cDato.DecIng03 = (reg.OPERACION_ORDEN == 3) ? reg.CANTIDAD : cDato.DecIng03;
                                cDato.DecIng04 = (reg.OPERACION_ORDEN == 4) ? reg.CANTIDAD : cDato.DecIng04;
                                cDato.DecIng05 = (reg.OPERACION_ORDEN == 5) ? reg.CANTIDAD : cDato.DecIng05;
                                cDato.DecIng06 = (reg.OPERACION_ORDEN == 6) ? reg.CANTIDAD : cDato.DecIng06;
                                cDato.DecIng07 = (reg.OPERACION_ORDEN == 7) ? reg.CANTIDAD : cDato.DecIng07;
                                cDato.DecIng08 = (reg.OPERACION_ORDEN == 8) ? reg.CANTIDAD : cDato.DecIng08;
                                cDato.DecIng09 = (reg.OPERACION_ORDEN == 9) ? reg.CANTIDAD : cDato.DecIng09;
                                cDato.DecIng10 = (reg.OPERACION_ORDEN == 10) ? reg.CANTIDAD : cDato.DecIng10;
                                cDato.DecIng11 = (reg.OPERACION_ORDEN == 11) ? reg.CANTIDAD : cDato.DecIng11;
                                cDato.DecIng12 = (reg.OPERACION_ORDEN == 12) ? reg.CANTIDAD : cDato.DecIng12;
                                cDato.DecIng13 = (reg.OPERACION_ORDEN == 13) ? reg.CANTIDAD : cDato.DecIng13;
                                cDato.DecIng14 = (reg.OPERACION_ORDEN == 14) ? reg.CANTIDAD : cDato.DecIng14;
                                cDato.DecIng15 = (reg.OPERACION_ORDEN == 15) ? reg.CANTIDAD : cDato.DecIng15;
                                cDato.DecIng16 = (reg.OPERACION_ORDEN == 16) ? reg.CANTIDAD : cDato.DecIng16;
                                cDato.DecIng17 = (reg.OPERACION_ORDEN == 17) ? reg.CANTIDAD : cDato.DecIng17;
                                cDato.DecIng18 = (reg.OPERACION_ORDEN == 18) ? reg.CANTIDAD : cDato.DecIng18;
                                cDato.DecIng19 = (reg.OPERACION_ORDEN == 19) ? reg.CANTIDAD : cDato.DecIng19;
                                cDato.DecIng20 = (reg.OPERACION_ORDEN == 20) ? reg.CANTIDAD : cDato.DecIng20;

                                vecValores[3 + (int)reg.OPERACION_ORDEN] = reg.CANTIDAD;
                            }
                        }

                        if (lst.LstEgresos == null || !lst.LstEgresos.Any())
                        {
                            cDato.DecSal01 = -99999;
                        }
                        else
                        {
                            foreach (var reg in lst.LstEgresos)
                            {
                                if (!dgvAcumulado.Columns[28 + (int)reg.OPERACION_ORDEN].Visible)
                                {
                                    dgvAcumulado.Columns[28 + (int)reg.OPERACION_ORDEN].Visible = true;

                                    dgvAcumulado.Columns[28 + (int)reg.OPERACION_ORDEN].Caption = reg.OPERACION_NOMBRE;
                                    strTitulos[28 + (int)reg.OPERACION_ORDEN] = reg.OPERACION_NOMBRE;

                                }
                                cDato.DecSal01 = (reg.OPERACION_ORDEN == 1) ? reg.CANTIDAD : cDato.DecSal01;
                                cDato.DecSal02 = (reg.OPERACION_ORDEN == 2) ? reg.CANTIDAD : cDato.DecSal02;
                                cDato.DecSal03 = (reg.OPERACION_ORDEN == 3) ? reg.CANTIDAD : cDato.DecSal03;
                                cDato.DecSal04 = (reg.OPERACION_ORDEN == 4) ? reg.CANTIDAD : cDato.DecSal04;
                                cDato.DecSal05 = (reg.OPERACION_ORDEN == 5) ? reg.CANTIDAD : cDato.DecSal05;
                                cDato.DecSal06 = (reg.OPERACION_ORDEN == 6) ? reg.CANTIDAD : cDato.DecSal06;
                                cDato.DecSal07 = (reg.OPERACION_ORDEN == 7) ? reg.CANTIDAD : cDato.DecSal07;
                                cDato.DecSal08 = (reg.OPERACION_ORDEN == 8) ? reg.CANTIDAD : cDato.DecSal08;
                                cDato.DecSal09 = (reg.OPERACION_ORDEN == 9) ? reg.CANTIDAD : cDato.DecSal09;
                                cDato.DecSal10 = (reg.OPERACION_ORDEN == 10) ? reg.CANTIDAD : cDato.DecSal10;
                                cDato.DecSal11 = (reg.OPERACION_ORDEN == 11) ? reg.CANTIDAD : cDato.DecSal11;
                                cDato.DecSal12 = (reg.OPERACION_ORDEN == 12) ? reg.CANTIDAD : cDato.DecSal12;
                                cDato.DecSal13 = (reg.OPERACION_ORDEN == 13) ? reg.CANTIDAD : cDato.DecSal13;
                                cDato.DecSal14 = (reg.OPERACION_ORDEN == 14) ? reg.CANTIDAD : cDato.DecSal14;
                                cDato.DecSal15 = (reg.OPERACION_ORDEN == 15) ? reg.CANTIDAD : cDato.DecSal15;
                                cDato.DecSal16 = (reg.OPERACION_ORDEN == 16) ? reg.CANTIDAD : cDato.DecSal16;
                                cDato.DecSal17 = (reg.OPERACION_ORDEN == 17) ? reg.CANTIDAD : cDato.DecSal17;
                                cDato.DecSal18 = (reg.OPERACION_ORDEN == 18) ? reg.CANTIDAD : cDato.DecSal18;
                                cDato.DecSal19 = (reg.OPERACION_ORDEN == 19) ? reg.CANTIDAD : cDato.DecSal19;
                                cDato.DecSal20 = (reg.OPERACION_ORDEN == 20) ? reg.CANTIDAD : cDato.DecSal20;
                                cDato.DecImporteTotal = reg.PRECIO_TOTAL;
                                vecValores[23 + (int)reg.OPERACION_ORDEN] = reg.CANTIDAD;


                            }
                            cDato.VecValores = vecValores;
                        }

                        lstMovimientos.Add(cDato);
                        cont++;
                        intContProd++;
                    }

                    /*  if (Convert.ToInt32(strParametro1) == (int)EnumActividades.EstacionServicioGnv)
                      {
                          dgvAcumulado.Columns[50].Visible = false;
                          dgvAcumulado.Columns[28].Visible = false;
                          dgvAcumulado.Columns[7].Visible = false;
                          dgvAcumulado.Columns[0].Visible = false;
                      }
                      else if (Convert.ToInt32(strParametro1) == (int)EnumActividades.ImportacionCarburantes)
                      {
                          dgvAcumulado.Columns[50].Visible = false;
                          dgvAcumulado.Columns[49].Visible = false;
                          dgvAcumulado.Columns[29].Visible = false;
                          dgvAcumulado.Columns[7].Visible = false;
                          dgvAcumulado.Columns[0].Visible = false;
                      }
                      else if (Convert.ToInt32(strParametro1) == (int)EnumActividades.ImportacionLubricantes)
                      {
                          dgvAcumulado.Columns[50].Visible = false;
                          dgvAcumulado.Columns[49].Visible = true;
                          dgvAcumulado.Columns[29].Visible = true;
                          dgvAcumulado.Columns[30].Visible = true;
                          dgvAcumulado.Columns[28].Visible = false;
                          //dgvAcumulado.Columns[29].Visible = false;

                          dgvAcumulado.Columns[7].Visible = false;
                          dgvAcumulado.Columns[0].Visible = false;
                      }
                      else if (Convert.ToInt32(strParametro1) == (int)EnumActividades.TransporteGasDucto)
                      {
                          dgvAcumulado.Columns[50].Visible = false;                        
                          dgvAcumulado.Columns[7].Visible = false;
                        
                      }
                      else if (Convert.ToInt32(strParametro1) == (int)EnumActividades.TransporteLiquidosDucto)
                      {
                          dgvAcumulado.Columns[50].Visible = false;
                          dgvAcumulado.Columns[7].Visible = false;

                      }
                      else if (Convert.ToInt32(strParametro1) == (int)EnumActividades.DistribucionGasRedes)
                      {
                          dgvAcumulado.Columns[28].Visible = false;
                          dgvAcumulado.Columns[52].Visible = false;
                      }
                      else {
                      
                      }*/



                    Session[CVariablesSesion.GrillaAcumulado] = lstMovimientos;
                    dgvAcumulado.DataSource = lstMovimientos;
                    dgvAcumulado.DataBind();

                    Session[CVariablesSesion.GrillaTitulo] = strTitulos;

                    //cDato.StrGrupoUnico = lst.StrCodHidrocarburo + " - " + lst.StrTipoInfraestructura + " - " + lst.StrInfraestructura  
                
                    var lstCombo = lstMovimientos.GroupBy(x => new { x.StrIdGrupoUnico, x.StrProducto, x.StrTipoInfraestructura, x.StrCodigoInfraestructura })
                       .Select(g => new { g.Key.StrIdGrupoUnico, g.Key.StrProducto, g.Key.StrTipoInfraestructura, g.Key.StrCodigoInfraestructura })
                       .OrderBy(d => d.StrProducto).ToList();
                    cmbFiltro.DataSource = lstCombo;
                    cmbFiltro.DataBind();
                    Session[CVariablesSesion.ComboGenerico] = lstCombo;

                    //grafico General
                    var objRes = new O_GRAFICO_TIPO1();
                    var series = new List<O_SERIES>();

                    var lstMovi = lstMovimientos.OrderBy(x => x.StrProducto).ToList();
                    var lstProd = lstMovimientos.GroupBy(x => new { x.StrProducto })
                       .Select(g => new { g.Key.StrProducto })
                       .OrderBy(d => d.StrProducto).ToList();
                    var names = new string[lstProd.Count()];
                    int intCont = 0;
                    foreach (var prod in lstProd)
                    {
                        names[intCont] = prod.StrProducto;
                        intCont++;
                    }
                    objRes.names = names;

                    for (int i = 0; i < strTitulos.Count(); i++)
                    {
                        if (strTitulos[i] != "")
                        {
                            var objSerie = new O_SERIES();
                            objSerie.name = strTitulos[i];

                            int intAux = 4;
                            if (i > 28)
                                intAux = 5;

                            var strPro = "";
                            decimal decValor = 0;
                            intCont = 0;

                            var arrSerie = new double[lstProd.Count()];

                            foreach (var rMov in lstMovi)
                            {
                                if (strPro != rMov.StrProducto)
                                {
                                    if (strPro != "")
                                    {
                                        arrSerie[intCont] = (double)decValor;
                                        intCont++;
                                    }
                                    decValor = 0;
                                    strPro = rMov.StrProducto;
                                }
                                decValor = decValor + rMov.VecValores[i - intAux];
                            }
                            arrSerie[intCont] = (double)decValor;
                            objSerie.data = arrSerie;
                            objSerie.stack = intAux.ToString();

                            series.Add(objSerie);
                        }
                    }
                    objRes.series = series;

                    var panel = sender as ASPxCallbackPanel;
                    panel.JSProperties["cp_title"] = "Movimiento Acumulado";
                    panel.JSProperties["cp_subtitle"] = "Del: " + detFechaIni.Date.ToShortDateString() + " Al: " + detFechaFin.Date.ToShortDateString();
                    panel.JSProperties["cp_xAxis"] = objRes.names;
                    panel.JSProperties["cp_series"] = objRes.series;            
                    #endregion

                    #region AlertaRAR-ANH-DGRCR 0171/2021
                    var lstTipoOperacion = Session[CVariablesSesion.ComboTiposOperacion] as List<O_TIPOS_OPE_ACT_CTY>;
                     if (lstTipoOperacion != null && lstTipoOperacion.Any())
                     {
                         if (lstTipoOperacion.Where(x => x.FACTOR_CALCULO == 1).Count() > 0)
                         {
                             MValidaSaldosNeg(strParametro1, strParametro2, strParametro3, strParametro4, sender);
                         }
                         else { 
                         //A Implementar
                         }
                     }
                     else {
                         //A implentar
                     }
                    #endregion

                    #region Traza consulta de informacion
                    
                    var objConsulta = new EObjetoEnvio6ParametrosProto
                    {   
                        strCredencial = CParametrosHydro.strCredencialFuncionario,                   
                        decFiltro = decFiltro,
                        strParametro1 = strParametro1, // IdsConsumidores 
                        strParametro2 = strParametro2, // fechaInicio
                        strParametro3 = strParametro3, // fechaFin
                        strParametro4 = strParametro4,
                        strParametro5 = "2"
                    };
                    string strAccion = JsonSerializer.SerializeToString(objConsulta);
                    string strSalida = lstMovimientos.Count().ToString();
                    string mensaje = GetType().Name + "." + MethodBase.GetCurrentMethod().Name;
                    _cTrazaInf.MRegistraTrazaInf(strAccion, mensaje, CParametrosHydro.decIdAplicacion, Convert.ToDecimal(Session[CVariablesSesion.IdUsuario].ToString()),HttpContext.Current.Request.UserHostAddress, strSalida);
                    #endregion
                }
            }
            else
            {
                MImprimirMensaje(lstDespachos.strMensaje, EnumTiposMensajeAlerta.Error);
            }
        }
        /// <summary>
        /// METODO. MValidaSaldosNeg
        /// </summary>
        private void MValidaSaldosNeg(string strIdTipoActividad, string strIdConsumidor, string strFechaDiaInicio,string strFechaDiaFin, object sender)
        {
            #region Alerta de Saldos Negativos RAR-ANH-DGRCR 0171/2021

            //DateTime dtFechaActual = DateTime.Now;
            //var strFechaDiaInicio = dtFechaActual.Date.Year + dtFechaActual.Date.Month.ToString().PadLeft(2, '0') + "01"; //"20170101", //fechaInicio
            //var strFechaDiaFin = dtFechaActual.Date.Year + dtFechaActual.Date.Month.ToString().PadLeft(2, '0') + dtFechaActual.Date.Day.ToString().PadLeft(2, '0'); //"20170131" //fechaFinal
            var objResultado = _consulta.ObtenerSaldo(
                    new EObjetoEnvio4ParametrosProto
                    {
                        strCredencial = CParametrosHydro.strCredencialFuncionario,
                        decFiltro = 1,
                        strParametro1 = strIdTipoActividad,
                        strParametro2 = strIdConsumidor,
                        strParametro3 = strFechaDiaInicio,
                        strParametro4 = strFechaDiaFin
                    });
            if (objResultado.intCodigo == 1)
            {
                var objSaldos = (List<O_SALDO_FINAL_DIA_CTY>)objResultado.oResultado;
                var auxObjSaldo = objSaldos.Where(x => x.SALDO_FIN < 0).ToList();
                var panel = sender as ASPxCallbackPanel;
                if (auxObjSaldo.Count > 0)
                {   
                    //panel.JSProperties["cp_Alerta"] = "EXISTEN SALDOS NEGATIVOS, VERIFIQUE SU INFORMACION. <br> PROYECTO: " + lblRazonSocial.Text + "<br><br><br> EVITE OBSERVACIONES. <br>EN CUMPLIMIENTO RAR-ANH-DGRCR 0171/2021.";
                    panel.JSProperties["cp_Alerta"] = "EXISTEN SALDOS NEGATIVOS, VERIFIQUE SU INFORMACION. <br> PROYECTO: " + lblRazonSocial.Text + "<br><br><br> EVITE OBSERVACIONES.";                                                          
                }
                else
                {
                    panel.JSProperties["cp_Alerta"] = "";
                }
            }
            else
            {
                //A Implementar 
            }

            #endregion
        }

        /// <summary>
        /// METODO. MCargaListados
        /// </summary>
        private void MCargaListados()
        {
            #region Infraestructura

            var objParamInf = Session[CVariablesSesion.ListaParamInfra] as O_PARAMETROS_INFRA;
            var cmbTipoInfEf = (dgvTransacciones.FindEditFormTemplateControl("cmbTipoInfEf")) as ASPxComboBox;
            cmbTipoInfEf.DataSource = objParamInf.TIPO_INFRAESTRUCTURA;
            cmbTipoInfEf.DataBind();

            var cmbTipoTransEf = (dgvTransacciones.FindEditFormTemplateControl("cmbTipoTransEf")) as ASPxComboBox;
            cmbTipoTransEf.DataSource = objParamInf.TIPO_INFRAESTRUCTURA;
            cmbTipoTransEf.DataBind();

            var cmbTipoEnvaseEf = (dgvTransacciones.FindEditFormTemplateControl("cmbTipoEnvaseEf")) as ASPxComboBox;
            cmbTipoEnvaseEf.DataSource = objParamInf.TIPOS_CONTENEDORES;
            cmbTipoEnvaseEf.DataBind();

            var cmbMedioTransEf = (dgvTransacciones.FindEditFormTemplateControl("cmbMedioTransEf")) as ASPxComboBox;

            var intTipoInfra = 0;
            var intIdEntidad = Convert.ToInt32(tlsOperadores.FocusedNode.GetValue("IntIdProyecto").ToString());
            var intIdActividad = Convert.ToInt32(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
            var objInfraestructura = _clientI.Get<EResultadoInfraestructura>(CAppSettings.svc_ListadoInfraestructuraVol + "/" + CParametrosHydro.strCredencialInfra + "/" + intIdEntidad + "/" + intIdActividad + "/" + intTipoInfra + "?format=json");

            var cmbTanqueEf = (dgvTransacciones.FindEditFormTemplateControl("cmbTanqueEf")) as ASPxComboBox;
            var cmbProdEf = (dgvTransacciones.FindEditFormTemplateControl("cmbProdEf")) as ASPxComboBox;
            var cmbSectorEconomico = (dgvTransacciones.FindEditFormTemplateControl("cmbSectorEconomico")) as ASPxComboBox;

            if (objInfraestructura.decCodigo == 1 && objInfraestructura.oResultado != null && objInfraestructura.oResultado.Any() && objInfraestructura.oResultado.FirstOrDefault().ID_ENTIDAD != -99)
            {
                objInfraestructura.oResultado.Add(new O_INFRAESTRUCTURA_VOL_CTY() { ID_ENTIDAD = 0, ID_TIPO_ACTIVIDAD = 0, CAPACIDAD = 0, ID_UNIDAD_MEDIDA = -99999, UNIDAD_MEDIDA = "", ID_PRODUCTO = 0, PRODUCTO = "", TIPO = "NOID", ID_TIPO = -99999, CODIGO_OPERADOR = "No Identificado", CODIGO_ANH = "", ID_INFRAESTRUCTURA = -99999 });
                objInfraestructura.oResultado.ForEach(x => x.ID_INFRA = Convert.ToDecimal(x.ID_TIPO.ToString().Replace("-", "") + x.ID_INFRAESTRUCTURA.ToString().Replace("-", "")));

                cmbTanqueEf.DataSource = objInfraestructura.oResultado;
                cmbTanqueEf.DataBind();
                cmbTanqueEf.SelectedIndex = 0;

                cmbMedioTransEf.DataSource = objInfraestructura.oResultado;
                cmbMedioTransEf.DataBind();
                cmbMedioTransEf.SelectedIndex = 0;
            }
            else
            {
                var lstResultado = new List<O_INFRAESTRUCTURA_VOL_CTY>();
                lstResultado.Add(new O_INFRAESTRUCTURA_VOL_CTY() { ID_ENTIDAD = 0, ID_TIPO_ACTIVIDAD = 0, CAPACIDAD = 0, ID_UNIDAD_MEDIDA = -99999, UNIDAD_MEDIDA = "No Identificado", ID_PRODUCTO = -99999, PRODUCTO = "No Identificado", TIPO = "No Identificado", ID_TIPO = -99999, CODIGO_OPERADOR = "No Identificado", CODIGO_ANH = "", ID_INFRAESTRUCTURA = -99999, ID_INFRA = 9999999999 });

                cmbTanqueEf.DataSource = lstResultado;
                cmbTanqueEf.DataBind();
                cmbTanqueEf.SelectedIndex = 0;

                cmbMedioTransEf.DataSource = lstResultado;
                cmbMedioTransEf.DataBind();
                cmbMedioTransEf.SelectedIndex = 0;
            }

            //HIDROCARBUROS
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
                cmbProdEf.DataSource = objHidrocarburo.OResultado;
                cmbProdEf.DataBind();
            }
            else
            {
                cmbProdEf.DataSource = null;
                cmbProdEf.DataBind();
            }

      


            //ENTIDADES
            var cmbEntidadAuxialiarEf = (dgvTransacciones.FindEditFormTemplateControl("cmbEntidadAuxialiarEf")) as ASPxComboBox;
            cmbEntidadAuxialiarEf.DataSource = null;
            cmbEntidadAuxialiarEf.DataBind();

            var objEntidades = _client.Post<EResultadoConfigHidrocarburo>(CAppSettings.svc_listar_autorizacion_condicion,
            new
            {
                strCredencial = CParametrosHydro.strCredencialFuncionario,
                decFiltro = 3,
                strParametro1 = tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString(),
                strParametro2 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
                strParametro3 = ""
            });
            if (objEntidades.IntCodigo != 1)
            {
                objEntidades.OResultado = new List<O_LISTA_AUTORIZACION_COND_CTY>();
            }
            objEntidades.OResultado.Add(new O_LISTA_AUTORIZACION_COND_CTY()
            {
                ID_AUTORIZACION_CONDICION = 0,
                ID_IDENTIFICADOR = -99999,
                NOMBRE = "No Identificado",
                ID_SUB_IDENTIFICADOR = -99999,
                SUBID_NOMBRE = "No Identificado",
                DESCRIPCION = "NOID"
            });
            cmbEntidadAuxialiarEf.DataSource = objEntidades.OResultado;
            cmbEntidadAuxialiarEf.DataBind();

            #endregion

            #region Tablas parametricas

            var cmbPaisEf = (dgvTransacciones.FindEditFormTemplateControl("cmbPaisEf")) as ASPxComboBox;
            O_PARAMETRICAS_GRAL objParametricas;
            objParametricas = (O_PARAMETRICAS_GRAL)Session[CVariablesSesion.ListaParamGral];
            if (cmbPaisEf != null)
            {
                cmbPaisEf.DataSource = objParametricas.lstPais;
                cmbPaisEf.DataBind();
            }

            var cmbTipoDocEf = (dgvTransacciones.FindEditFormTemplateControl("cmbTipoDocEf")) as ASPxComboBox;
            cmbTipoDocEf.DataSource = Session[CVariablesSesion.ListaTransaccion];
            cmbTipoDocEf.DataBind();
            cmbTipoDocEf.SelectedItem = cmbTipoDocEf.Items.FindByValue("2");

            //Carga combos de parametricas
            var objCatalogo = (O_OBTENER_CATALOGOS_CTY)Session[CVariablesSesion.ListaParametrica];
            var objSectorEconomico = Session[CVariablesSesion.ListaSectorEconomico];
            var cmbUnidadEf = (dgvTransacciones.FindEditFormTemplateControl("cmbUnidadEf")) as ASPxComboBox;
            var cmbTipoMedicEf = (dgvTransacciones.FindEditFormTemplateControl("cmbTipoMedicEf")) as ASPxComboBox;
            var cmbTipoMovEf = (dgvTransacciones.FindEditFormTemplateControl("cmbTipoMovEf")) as ASPxComboBox;
            var cmbTipoFlujoEf = (dgvTransacciones.FindEditFormTemplateControl("cmbTipoFlujoEf")) as ASPxComboBox;
            var cmbTipoTempEf = (dgvTransacciones.FindEditFormTemplateControl("cmbTipoTempEf")) as ASPxComboBox;

            var cmbTipoDespachoEf = (dgvTransacciones.FindEditFormTemplateControl("cmbTipoDespachoEf")) as ASPxComboBox;

            cmbUnidadEf.DataSource = objCatalogo.CATALOGO_UNIDADES_MEDIDA_GRAL;
            cmbUnidadEf.DataBind();
            cmbTipoTempEf.DataSource = objCatalogo.CATALOGO_UNIDADES_MEDIDA_GRAL;
            cmbTipoTempEf.DataBind();
            cmbTipoMedicEf.DataSource = objCatalogo.CATALOGO_TIPOS_MEDICION;
            cmbTipoMedicEf.DataBind();
            cmbTipoMedicEf.SelectedItem = cmbTipoMedicEf.Items.FindByValue("1");

            cmbSectorEconomico.DataSource = objSectorEconomico;
            cmbSectorEconomico.DataBind();

            var lstParam = objCatalogo.CATALOGO_PARAMETRICAS_DOMINIO;
            foreach (var param in lstParam)
            {
                var lstObtCatalogo = _client.Post<EResultadoParametricaGlobal>
                    (CAppSettings.svc_lista_parametrica_global,
                    new
                    {
                        decIdDominio = param.ID_CATALOGO
                    });

                if (lstObtCatalogo.IntCodigo == 1)
                {
                    List<O_PARAMETRICA_GLOBAL_CTY> objDefecto;
                    var objLstGlobal = lstObtCatalogo.OResultado;

                    switch (param.CODIGO)
                    {
                        case "TMOV":
                            objDefecto = objLstGlobal.Where(x => x.CODIGO == "NN").ToList();
                            cmbTipoMovEf.DataSource = objLstGlobal;
                            cmbTipoMovEf.DataBind();
                            cmbTipoMovEf.SelectedItem = cmbTipoMovEf.Items.FindByValue(objDefecto.First().ID_PARAMETRICA_GLOBAL.ToString());
                            break;
                        case "TFLM":
                            objDefecto = objLstGlobal.Where(x => x.CODIGO == "FIS").ToList();
                            cmbTipoFlujoEf.DataSource = objLstGlobal;
                            cmbTipoFlujoEf.DataBind();
                            cmbTipoFlujoEf.SelectedItem = cmbTipoFlujoEf.Items.FindByValue(objDefecto.First().ID_PARAMETRICA_GLOBAL.ToString());
                            break;
                        case "TCOS":
                            objLstGlobal.Add(new O_PARAMETRICA_GLOBAL_CTY() { ID_PARAMETRICA_GLOBAL = -99999, CODIGO = "NOID", DESCRIPCION = "NO DEFINIDO" });
                            cmbTipoDespachoEf.DataSource = objLstGlobal;
                            cmbTipoDespachoEf.DataBind();
                            cmbTipoDespachoEf.SelectedItem = cmbTipoDespachoEf.Items.FindByValue("-99999");
                            break;
                        case "FRMPAGO":

                            break;
                    }
                }
                else
                {
                    //todo No implementado.
                }
            }

            #endregion
        }

        private void MCargaListadosImp()
        {
            var objParamInf = Session[CVariablesSesion.ListaParamInfra] as O_PARAMETROS_INFRA;

            var cmbProdEfImp = (dgvTransaccionesImp.FindEditFormTemplateControl("cmbProdEfImp")) as ASPxComboBox;

            var strParametro1 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString();

            var opcion = 0;

            if ((Convert.ToInt32(strParametro1) == (int)EnumActividades.ImportacionCarburantes) || (Convert.ToInt32(strParametro1) == (int)EnumActividades.ImportacionLubricantes))
            {
                /*********CARBURANTES Y LUBRICANTES*/

                opcion = 1;

            }
            else
            { //HIDROCARBUROS
                opcion = 1;
            }


            var objHidrocarburo = _client.Post<EResultadoConfigHidrocarburo>(CAppSettings.svc_listar_autorizacion_condicion,
              new
              {
                  strCredencial = CParametrosHydro.strCredencialFuncionario,
                  decFiltro = opcion,
                  strParametro1 = tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString(),
                  strParametro2 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
                  strParametro3 = ""
              });



            if (objHidrocarburo.IntCodigo == 1)
            {
                cmbProdEfImp.DataSource = objHidrocarburo.OResultado;
                cmbProdEfImp.DataBind();
            }
            else
            {
                cmbProdEfImp.DataSource = null;
                cmbProdEfImp.DataBind();
            }


            #region Tablas parametricas


            //Carga combos de parametricas
            var objCatalogo = (O_OBTENER_CATALOGOS_CTY)Session[CVariablesSesion.ListaParametrica];
            var cmbUnidadEfImp = (dgvTransaccionesImp.FindEditFormTemplateControl("cmbUnidadEfImp")) as ASPxComboBox;

            cmbUnidadEfImp.DataSource = objCatalogo.CATALOGO_UNIDADES_MEDIDA_GRAL;
            cmbUnidadEfImp.DataBind();

            //var cmbTipoDocEfImpEdt = (dgvTransacciones.FindEditFormTemplateControl("cmbTipoDocEfImpEdt")) as ASPxComboBox;
            //cmbTipoDocEfImpEdt.DataSource = Session[CVariablesSesion.ListaTransaccion];
            //cmbTipoDocEfImpEdt.DataBind();
            //cmbTipoDocEfImpEdt.SelectedItem = cmbTipoDocEfImpEdt.Items.FindByValue("2");


            var lstParam = objCatalogo.CATALOGO_PARAMETRICAS_DOMINIO;
            foreach (var param in lstParam)
            {
                var lstObtCatalogo = _client.Post<EResultadoParametricaGlobal>
                    (CAppSettings.svc_lista_parametrica_global,
                    new
                    {
                        decIdDominio = param.ID_CATALOGO
                    });

                if (lstObtCatalogo.IntCodigo == 1)
                {
                    List<O_PARAMETRICA_GLOBAL_CTY> objDefecto;
                    var objLstGlobal = lstObtCatalogo.OResultado;

                    switch (param.CODIGO)
                    {
                        case "TMOV":
                            objDefecto = objLstGlobal.Where(x => x.CODIGO == "NN").ToList();
                            //cmbTipoMovEf.DataSource = objLstGlobal;
                            //cmbTipoMovEf.DataBind();
                            //cmbTipoMovEf.SelectedItem = cmbTipoMovEf.Items.FindByValue(objDefecto.First().ID_PARAMETRICA_GLOBAL.ToString());
                            break;
                        case "TFLM":
                            objDefecto = objLstGlobal.Where(x => x.CODIGO == "FIS").ToList();
                            //cmbTipoFlujoEf.DataSource = objLstGlobal;
                            //cmbTipoFlujoEf.DataBind();
                            //cmbTipoFlujoEf.SelectedItem = cmbTipoFlujoEf.Items.FindByValue(objDefecto.First().ID_PARAMETRICA_GLOBAL.ToString());
                            break;
                        case "TCOS":
                            objLstGlobal.Add(new O_PARAMETRICA_GLOBAL_CTY() { ID_PARAMETRICA_GLOBAL = -99999, CODIGO = "NOID", DESCRIPCION = "NO DEFINIDO" });
                            //cmbTipoDespachoEf.DataSource = objLstGlobal;
                            //cmbTipoDespachoEf.DataBind();
                            //cmbTipoDespachoEf.SelectedItem = cmbTipoDespachoEf.Items.FindByValue("-99999");
                            break;
                    }
                }
                else
                {
                    //todo No implementado.
                }
            }

            #endregion
        }

        /// <summary>
        /// METODO. MValidaDatosIniciales
        /// </summary>
        /// <param name="strMensaje"></param>
        /// <returns></returns>
        private bool MValidaDatosIniciales(ref string strMensaje)
        {
            bool booResultado = true;

            if (detFechaIni.Text == "" || detFechaFin.Text == "")
            {
                strMensaje = "Introduzca fechas validas.";
                booResultado = false;
            }
            else if (detFechaFin.Date < detFechaIni.Date)
            {
                strMensaje = "Fechas incorrectas.";
                booResultado = false;
            }
            else if (detFechaIni.Date.Year + detFechaIni.Date.Month != detFechaFin.Date.Year + detFechaFin.Date.Month)
            {
                strMensaje = "Fechas fuera de intérvalo. Debe escoger fechas dentro del mismo periodo.";
                booResultado = false;
            }
            return booResultado;
        }

        /// <summary>
        /// METODO. MValidaDatos
        /// </summary>
        /// <param name="strMensaje"></param>
        /// <param name="intIdOperacion"></param>
        /// <param name="intIdActividad"></param>
        /// <returns></returns>
        private bool MValidaDatos(ref string strMensaje, int intIdOperacion, int intIdActividad)
        {
            bool booResultado = true;

            #region Instancia componentes
            ASPxDateEdit detFechaEf = (dgvTransacciones.FindEditFormTemplateControl("detFechaEf")) as ASPxDateEdit;
            ASPxTextBox txtClienteEf = (dgvTransacciones.FindEditFormTemplateControl("txtClienteEf")) as ASPxTextBox;
            ASPxTextBox txtNitEf = (dgvTransacciones.FindEditFormTemplateControl("txtNitEf")) as ASPxTextBox;
            ASPxComboBox cmbPaisEf = (dgvTransacciones.FindEditFormTemplateControl("cmbPaisEf")) as ASPxComboBox;
            ASPxTextBox txtPlacaEf = (dgvTransacciones.FindEditFormTemplateControl("txtPlacaEf")) as ASPxTextBox;
            ASPxComboBox cmbEntidadAuxialiarEf = (dgvTransacciones.FindEditFormTemplateControl("cmbEntidadAuxialiarEf")) as ASPxComboBox;
            ASPxComboBox cmbTanqueEf = (dgvTransacciones.FindEditFormTemplateControl("cmbTanqueEf")) as ASPxComboBox;
            ASPxComboBox cmbProdEf = (dgvTransacciones.FindEditFormTemplateControl("cmbProdEf")) as ASPxComboBox;
            ASPxTextBox txtCantidadEf = (dgvTransacciones.FindEditFormTemplateControl("txtCantidadEf")) as ASPxTextBox;
            ASPxComboBox cmbTipoMovEf = (dgvTransacciones.FindEditFormTemplateControl("cmbTipoMovEf")) as ASPxComboBox;
            #endregion

            #region Cabecera

            if (txtCantidadEf.Text == "" || Convert.ToDecimal(txtCantidadEf.Text) < 0)
            {
                strMensaje = "ERROR: Introduzca la cantidad del movimiento.";
            }
            else if (txtPlacaEf != null && txtPlacaEf.Visible && txtPlacaEf.Text == "")
            {
                strMensaje = "ERROR: Introduzca un identificador válido (placa, matricula, etc.).";
            }
            /*  else if (cmbPaisEf.Value != null && (Convert.ToInt32(cmbTipoMovEf.Value) != (int)EnumTipoMovimiento.NacNac && (cmbPaisEf.Value == null || Convert.ToInt32(cmbPaisEf.Value) == 120)))
              {
                  strMensaje = "ERROR: Introduzca un país válido.";
              }*/
            else if (cmbTipoMovEf.Value != null && (Convert.ToInt32(cmbTipoMovEf.Value) != (int)EnumTipoMovimiento.NacNac && (int)cmbPaisEf.Value == 120))
            {
                strMensaje = "ERROR: Introduzca un país válido.";
            }
            else if (Convert.ToInt32(cmbTipoOperacion.Value.ToString()) == (int)EnumTipoOperaciones.SaldoInicial && detFechaEf.Date.Day != 1)
            {
                strMensaje = "ERROR: El saldo inicial debe registrarse el primer día del mes.";
            }
            else
            {
                //todo No implementado.
            }

            if (txtNitEf != null && txtNitEf.Visible && txtNitEf.Text == "0" && txtClienteEf.Text.Trim().Length == 0)
            {
                txtClienteEf.Text = "SIN NOMBRE";
            }

            #endregion

            #region Documentos

            if (strMensaje.Trim().Length == 0)
            {
                var lstDocumentoMov = new List<O_LISTA_TRANSACCION_CTY>();
                var lstValidaDoc = new List<O_LISTA_TRANSACCION_CTY>();
                lstDocumentoMov = (List<O_LISTA_TRANSACCION_CTY>)Session[CVariablesSesion.GrillaAuxiliar];

                if (lstDocumentoMov == null)
                {
                    strMensaje = "ERROR: El movimiento debe contener al menos un documento.";
                }
                else
                {
                    lstValidaDoc = lstDocumentoMov.Where(x => x.ID_ESTADO_TRANSACCION != (decimal)EnumEstadoMov.Anulado).ToList();
                    if (lstValidaDoc.Count == 0)
                    {
                        strMensaje = "ERROR: El movimiento debe contener al menos un documento válido.";
                    }
                }

                if (strMensaje.Trim().Length == 0)
                {
                    foreach (var doc in lstDocumentoMov)
                    {
                        if (doc.ID_ESTADO_TRANSACCION != (decimal)EnumEstadoMov.Anulado)
                        {
                            if (doc.PRECIO_TOTAL > (Convert.ToDecimal(txtCantidadEf.Text) * doc.PRECIO_UNITARIO + 2) || doc.PRECIO_TOTAL < (Convert.ToDecimal(txtCantidadEf.Text) * doc.PRECIO_UNITARIO - 2))
                            {
                                strMensaje = "ERROR: Cálculo del importe del movimiento. ID: " + doc.ID_TRANSACCION;
                            }
                            else if (doc.ID_PADRE_TRANSACCION == (decimal)EnumTipoTrasaccionPadre.Factura)
                            {
                                if (doc.NRO_AUTORIZACION == 0 || doc.NRO_TRANSACCION == "0" || doc.NRO_TRANSACCION == "")
                                {
                                    strMensaje = "ERROR: Introduzca el número de Factura/Autorización de la factura. ID: " + doc.ID_TRANSACCION;
                                }
                                else if (doc.ID_TIPO_CREDITO_FISCAL == (int)EnumTipoCredFiscal.Si)
                                {
                                    if (doc.PRECIO_TOTAL == 0)
                                    {
                                        strMensaje = "ERROR: Introduzca el importe de la factura. ID: " + doc.ID_TRANSACCION;
                                    }
                                    else if (doc.CODIGO_CONTROL.Trim().Length > 0 && doc.CODIGO_CONTROL.Trim().Length < 5)
                                    {
                                        strMensaje = "ERROR: Introduzca un código de control válido a la factura. ID: " + doc.ID_TRANSACCION;
                                    }
                                }
                            }
                            else
                            {
                                if (doc.NRO_TRANSACCION == "0" || doc.NRO_TRANSACCION == "")
                                {
                                    strMensaje = "ERROR: Introduzca el número de referencia. ID: " + doc.ID_TRANSACCION;
                                }
                            }

                            if (strMensaje.Trim().Length > 0)
                            {
                                booResultado = false;
                                break;
                            }
                        }
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

        //lmqc
        private bool MValidaDatosImp(ref string strMensaje, int intIdOperacion, int intIdActividad)
        {
            bool booResultado = true;

            /*#region Instancia componentes
            ASPxDateEdit detFechaEf = (dgvTransaccionesImp.FindEditFormTemplateControl("detFechaEf")) as ASPxDateEdit;
            ASPxTextBox txtClienteEf = (dgvTransaccionesImp.FindEditFormTemplateControl("txtClienteEf")) as ASPxTextBox;
            ASPxTextBox txtNitEf = (dgvTransaccionesImp.FindEditFormTemplateControl("txtNitEf")) as ASPxTextBox;
            ASPxComboBox cmbPaisEf = (dgvTransaccionesImp.FindEditFormTemplateControl("cmbPaisEf")) as ASPxComboBox;
            ASPxTextBox txtPlacaEf = (dgvTransaccionesImp.FindEditFormTemplateControl("txtPlacaEf")) as ASPxTextBox;
            ASPxComboBox cmbEntidadAuxialiarEf = (dgvTransaccionesImp.FindEditFormTemplateControl("cmbEntidadAuxialiarEf")) as ASPxComboBox;
            ASPxComboBox cmbTanqueEf = (dgvTransaccionesImp.FindEditFormTemplateControl("cmbTanqueEf")) as ASPxComboBox;
            ASPxComboBox cmbProdEf = (dgvTransaccionesImp.FindEditFormTemplateControl("cmbProdEf")) as ASPxComboBox;
            ASPxTextBox txtCantidadEf = (dgvTransaccionesImp.FindEditFormTemplateControl("txtCantidadEf")) as ASPxTextBox;
            ASPxComboBox cmbTipoMovEf = (dgvTransaccionesImp.FindEditFormTemplateControl("cmbTipoMovEf")) as ASPxComboBox;
            #endregion*/

            ////#region Cabecera

            ////if (txtCantidadEf.Text == "" || Convert.ToDecimal(txtCantidadEf.Text) < 0)
            ////{
            ////    strMensaje = "ERROR: Introduzca la cantidad del movimiento.";
            ////}
            ////else if (txtPlacaEf != null && txtPlacaEf.Visible && txtPlacaEf.Text == "")
            ////{
            ////    strMensaje = "ERROR: Introduzca un identificador válido (placa, matricula, etc.).";
            ////}
            ////else if (cmbPaisEf.Value != null && (Convert.ToInt32(cmbTipoMovEf.Value) != (int)EnumTipoMovimiento.NacNac && (cmbPaisEf.Value == null || Convert.ToInt32(cmbPaisEf.Value) == 120)))
            ////{
            ////    strMensaje = "ERROR: Introduzca un país válido.";
            ////}
            ////else if (cmbTipoMovEf.Value != null && (Convert.ToInt32(cmbTipoMovEf.Value) != (int)EnumTipoMovimiento.NacNac && (int)cmbPaisEf.Value == 120))
            ////{
            ////    strMensaje = "ERROR: Introduzca un país válido.";
            ////}
            ////else if (Convert.ToInt32(cmbTipoOperacion.Value.ToString()) == (int)EnumTipoOperaciones.SaldoInicial && detFechaEf.Date.Day != 1)
            ////{
            ////    strMensaje = "ERROR: El saldo inicial debe registrarse el primer día del mes.";
            ////}
            ////else
            ////{
            ////    //todo No implementado.
            ////}

            ////if (txtNitEf != null && txtNitEf.Visible && txtNitEf.Text == "0" && txtClienteEf.Text.Trim().Length == 0)
            ////{
            ////    txtClienteEf.Text = "SIN NOMBRE";
            ////}

            ////#endregion

            #region Documentos
            //no requiere documentos para la actividad de importacion de lubricantes
            if (intIdActividad != 36)
            {
                if (strMensaje.Trim().Length == 0)
                {
                    var lstDocumentoMov = new List<O_LISTA_TRANSACCION_CTY>();
                    var lstValidaDoc = new List<O_LISTA_TRANSACCION_CTY>();
                    lstDocumentoMov = (List<O_LISTA_TRANSACCION_CTY>)Session[CVariablesSesion.GrillaAuxiliar];

                    if (lstDocumentoMov == null)
                    {
                        strMensaje = "ERROR: El movimiento debe contener al menos un documento.";
                    }
                    else
                    {
                        lstValidaDoc = lstDocumentoMov.Where(x => x.ID_ESTADO_TRANSACCION != (decimal)EnumEstadoMov.Anulado).ToList();
                        if (lstValidaDoc.Count == 0)
                        {
                            strMensaje = "ERROR: El movimiento debe contener al menos un documento válido.";
                        }
                    }

                    ////    if (strMensaje.Trim().Length == 0)
                    ////    {
                    ////        foreach (var doc in lstDocumentoMov)
                    ////        {
                    ////            if (doc.ID_ESTADO_TRANSACCION != (decimal)EnumEstadoMov.Anulado)
                    ////            {
                    ////                if ((doc.PRECIO_TOTAL > (Convert.ToDecimal(txtCantidadEf.Text) * doc.PRECIO_UNITARIO + 2) || doc.PRECIO_TOTAL < (Convert.ToDecimal(txtCantidadEf.Text) * doc.PRECIO_UNITARIO - 2)) && intIdActividad != 16)
                    ////                {
                    ////                    strMensaje = "ERROR: Cálculo del importe del movimiento. ID: " + doc.ID_TRANSACCION;
                    ////                }
                    ////                else if (doc.ID_PADRE_TRANSACCION == (decimal)EnumTipoTrasaccionPadre.Factura)
                    ////                {
                    ////                    if (doc.NRO_AUTORIZACION == 0 || doc.NRO_TRANSACCION == "0" || doc.NRO_TRANSACCION == "")
                    ////                    {
                    ////                        strMensaje = "ERROR: Introduzca el número de Factura/Autorización de la factura. ID: " + doc.ID_TRANSACCION;
                    ////                    }
                    ////                    else if (doc.ID_TIPO_CREDITO_FISCAL == (int)EnumTipoCredFiscal.Si)
                    ////                    {
                    ////                        if (doc.PRECIO_TOTAL == 0)
                    ////                        {
                    ////                            strMensaje = "ERROR: Introduzca el importe de la factura. ID: " + doc.ID_TRANSACCION;
                    ////                        }
                    ////                        else if (doc.CODIGO_CONTROL.Trim().Length > 0 && doc.CODIGO_CONTROL.Trim().Length < 5)
                    ////                        {
                    ////                            strMensaje = "ERROR: Introduzca un código de control válido a la factura. ID: " + doc.ID_TRANSACCION;
                    ////                        }
                    ////                    }
                    ////                }
                    ////                else
                    ////                {
                    ////                    if (doc.NRO_TRANSACCION == "0" || doc.NRO_TRANSACCION == "")
                    ////                    {
                    ////                        strMensaje = "ERROR: Introduzca el número de referencia. ID: " + doc.ID_TRANSACCION;
                    ////                    }
                    ////                }

                    ////                if (strMensaje.Trim().Length > 0)
                    ////                {
                    ////                    booResultado = false;
                    ////                    break;
                    ////                }
                    ////            }
                    ////        }
                    ////    }
                    ////    else
                    ////    {
                    ////        //todo No implementado.
                    ////    }
                }
            }
            else
            {
                //todo No implementado.
            }

            #endregion

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
        /// METODO. MValidaCondicionesFuncionamiento
        /// </summary>
        /// <param name="strMesnaje"></param>
        /// <returns></returns>
        private bool MValidaCondicionesFuncionamiento(ref string strMesnaje)
        {
            bool booRespuesta = true;

            //llave B-Sisa
            //switch ((int)tlsOperadores.FocusedNode.GetValue("IntIdActividad"))
            //{
            //    case (int)EnumActividades.EstacionServicioGnv:
            //    case (int)EnumActividades.EstacionServicioLiquido:
            //        //Llave B-Sisa
            //        //var objBsisa = clientI.Get<EResultadoBsisa>(CAppSettings.svc_ListadoLlaveMiddlewareProyecto + "/" + CParametrosHydro.strCredencialInfra + "/" + tlsOperadores.FocusedNode.GetValue("IntIdProyecto") + "/" + tlsOperadores.FocusedNode.GetValue("IntIdActividad") + "/?format=json");
            //        //if (objBsisa.decCodigo == 1)
            //        //{
            //        //    var objResultado = new O_LLAVE_MIDDLEWARE_CTY();
            //        //    objResultado = objBsisa.oResultado.FirstOrDefault();
            //        //    //if (objResultado == null || (objResultado.LLAVE_MIDDLEWARE == null || objResultado.LLAVE_MIDDLEWARE.Trim().Length == 0))
            //        //    if (1 == 0)
            //        //    {
            //        //        strMesnaje = "Identificación B-sisa Incorrecta. Cargue el archivo de la llave B-sisa.";
            //        //        booRespuesta = false;
            //        //        btnVerificaLlave.Visible = true;
            //        //    }
            //        //    else
            //        //    {
            //        //        //todo No implementado.
            //        //    }
            //        //}
            //        //else
            //        //{
            //        //    strMesnaje = "Identificación B-sisa Incorrecta. Cargue el archivo de la llave B-sisa.";
            //        //    booRespuesta = false;
            //        //    btnVerificaLlave.Visible = true;
            //        //}
            //        break;
            //    default:
            //        btnVerificaLlave.Visible = false;
            //        break;
            //}

            return booRespuesta;

        }

        /// <summary>
        /// METODO. MConfiguraGrilla
        /// </summary>
        /// <param name="binarioEstado"></param>
        /// <param name="strTipoOp"></param>
        private void MConfiguraGrilla(string binarioEstado, string strTipoOp)
        {

            if ((Convert.ToInt32(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()) != (int)EnumActividades.EstacionServicioLiquido &&
                Convert.ToInt32(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()) != (int)EnumActividades.EstacionServicioGnv &&
                Convert.ToInt32(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()) != (int)EnumActividades.PuestosVentaLiquido) ||
                !Session[CVariablesSesion.TipoOperacion].ToString().Contains("VENTA"))
            {
                dgvTransacciones.Columns[(int)EnumColumnasGrilla.FoPrecioRec].Visible = false;
                dgvTransacciones.Columns[(int)EnumColumnasGrilla.FoCantidadRec].Visible = false;
                dgvTransacciones.Columns[(int)EnumColumnasGrilla.FoImporteRec].Visible = false;

                btnCollapse.Visible = false;
                btnExpand.Visible = false;

                dgvTransacciones.GroupBy(dgvTransacciones.Columns[(int)EnumColumnasGrilla.FoFechaMovimiento], -1);
                dgvTransacciones.GroupBy(dgvTransacciones.Columns[(int)EnumColumnasGrilla.FoPlaca], -1);
            }
            else
            {
                dgvTransacciones.Columns[(int)EnumColumnasGrilla.FoPrecioRec].Visible = true;
                dgvTransacciones.Columns[(int)EnumColumnasGrilla.FoCantidadRec].Visible = true;
                dgvTransacciones.Columns[(int)EnumColumnasGrilla.FoImporteRec].Visible = true;

                btnCollapse.Visible = true;
                btnExpand.Visible = true;

                dgvTransacciones.GroupBy(dgvTransacciones.Columns[(int)EnumColumnasGrilla.FoFechaMovimiento], 0);
                dgvTransacciones.GroupBy(dgvTransacciones.Columns[(int)EnumColumnasGrilla.FoPlaca], 1);
            }

            dgvTransacciones.Columns[(int)EnumColumnasGrilla.FoPlaca].Visible = false;
            dgvTransacciones.Columns[(int)EnumColumnasGrilla.FoPais].Visible = false;
            dgvTransacciones.Columns[(int)EnumColumnasGrilla.FoNoAutorizacion].Visible = false;
            dgvTransacciones.Columns[(int)EnumColumnasGrilla.FoNit].Visible = false;
            dgvTransacciones.Columns[(int)EnumColumnasGrilla.FoPrecio].Visible = false;
            dgvTransacciones.Columns[(int)EnumColumnasGrilla.FoImporte].Visible = false;
            dgvTransacciones.Columns[(int)EnumColumnasGrilla.FoTipoMovimiento].Visible = false;

            strTipoOp = strTipoOp.Substring(2, 4);

            switch (strTipoOp)
            {
                case "RCEX":
                case "DSEX":
                    dgvTransacciones.Columns[(int)EnumColumnasGrilla.FoNoAutorizacion].Visible = true;
                    dgvTransacciones.Columns[(int)EnumColumnasGrilla.FoNofactura].Visible = true;
                    dgvTransacciones.Columns[(int)EnumColumnasGrilla.FoNit].Visible = true;
                    dgvTransacciones.Columns[(int)EnumColumnasGrilla.FoPrecio].Visible = true;
                    dgvTransacciones.Columns[(int)EnumColumnasGrilla.FoImporte].Visible = true;
                    dgvTransacciones.Columns[(int)EnumColumnasGrilla.FoTipoMovimiento].Visible = true;
                    dgvTransacciones.Columns[(int)EnumColumnasGrilla.FoPlaca].Visible = true;
                    dgvTransacciones.Columns[(int)EnumColumnasGrilla.FoPais].Visible = true;
                    break;
                case "RCIN":
                case "DSIN":
                    dgvTransacciones.Columns[(int)EnumColumnasGrilla.FoPrecio].Visible = true;
                    dgvTransacciones.Columns[(int)EnumColumnasGrilla.FoImporte].Visible = true;
                    dgvTransacciones.Columns[(int)EnumColumnasGrilla.FoTipoMovimiento].Visible = true;
                    break;
            }

            if (Convert.ToInt32(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()) ==
                                (int)EnumActividades.ImportacionCarburantes ||
                                Convert.ToInt32(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()) ==
                                (int)EnumActividades.ImportacionLubricantes) //LMQC para importadores
            {
                dgvTransacciones.Columns[(int)EnumColumnasGrilla.FoOrigenReg].Visible = false;
                dgvTransacciones.Columns[(int)EnumColumnasGrilla.FoPlaca].Visible = false;
                dgvTransacciones.Columns[(int)EnumColumnasGrilla.FoPais].Visible = false;
                dgvTransacciones.Columns[(int)EnumColumnasGrilla.FoTipoMovimiento].Visible = false;
                dgvTransacciones.Columns[(int)EnumColumnasGrilla.FoTipoFlujo].Visible = false;
            }
            if (Convert.ToInt32(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()) == (int)EnumActividades.DistribucionGasRedes) //LMQC para importadores
            {
                dgvTransacciones.Columns[(int)EnumColumnasGrilla.FoPrecio].Visible = true;
                dgvTransacciones.Columns[(int)EnumColumnasGrilla.FoImporte].Visible = true;
                dgvTransacciones.Columns[(int)EnumColumnasGrilla.FoNoAutorizacion].Visible = true;
            }

            var column = (GridViewCommandColumn)dgvTransacciones.Columns[0];
            column.ShowNewButtonInHeader = true;

            Session.Remove(CVariablesSesion.HabilitaBotones);
            if (Session[CVariablesSesion.HabilitaFunciones] == null || Convert.ToInt16(Session[CVariablesSesion.PermisoRevi]) == 1)
            {
                Session[CVariablesSesion.HabilitaBotones] = "MODNO";
                column.ShowNewButtonInHeader = false;
            }
            else
            {
                if (binarioEstado == "1")
                {
                    Session[CVariablesSesion.HabilitaBotones] = "MODSI";
                }
                else
                {
                    Session[CVariablesSesion.HabilitaBotones] = "ELISI";
                    column.ShowNewButtonInHeader = false;
                }
            }
        }

        //lmqc
        private void MConfiguraGrillaImp(string binarioEstado, string strTipoOp)
        {
            dgvTransaccionesImp.Columns[(int)EnumColumnasGrilla.FoPlaca].Visible = false;
            dgvTransaccionesImp.Columns[(int)EnumColumnasGrilla.FoPais].Visible = false;
            dgvTransaccionesImp.Columns[(int)EnumColumnasGrilla.FoNoAutorizacion].Visible = false;
            dgvTransaccionesImp.Columns[(int)EnumColumnasGrilla.FoNit].Visible = false;
            dgvTransaccionesImp.Columns[(int)EnumColumnasGrilla.FoPrecio].Visible = false;
            dgvTransaccionesImp.Columns[(int)EnumColumnasGrilla.FoImporte].Visible = false;
            dgvTransaccionesImp.Columns[(int)EnumColumnasGrilla.FoTipoMovimiento].Visible = false;

            dgvTransaccionesImp.Columns[(int)EnumColumnasGrilla.FoTipoTransaccion].Visible = false;

            dgvTransaccionesImp.Columns[(int)EnumColumnasGrilla.FoOrigenReg].Visible = false;
            dgvTransaccionesImp.Columns[(int)EnumColumnasGrilla.FoPlaca].Visible = false;
            dgvTransaccionesImp.Columns[(int)EnumColumnasGrilla.FoPais].Visible = false;
            dgvTransaccionesImp.Columns[(int)EnumColumnasGrilla.FoTipoMovimiento].Visible = false;
            dgvTransaccionesImp.Columns[(int)EnumColumnasGrilla.FoTipoFlujo].Visible = false;
            dgvTransaccionesImp.Columns[(int)EnumColumnasGrilla.FoRazonSocial].Visible = false;

            strTipoOp = strTipoOp.Substring(2, 4);

            switch (strTipoOp)
            {
                case "RCEX":
                case "DSEX":
                    dgvTransaccionesImp.Columns[(int)EnumColumnasGrilla.FoNoAutorizacion].Visible = false;
                    dgvTransaccionesImp.Columns[(int)EnumColumnasGrilla.FoNofactura].Visible = false;
                    dgvTransaccionesImp.Columns[(int)EnumColumnasGrilla.FoNit].Visible = false;
                    dgvTransaccionesImp.Columns[(int)EnumColumnasGrilla.FoTipoTransaccion].Visible = false;
                    dgvTransaccionesImp.Columns[(int)EnumColumnasGrilla.FoPrecio].Visible = false;
                    dgvTransaccionesImp.Columns[(int)EnumColumnasGrilla.FoImporte].Visible = false;
                    dgvTransaccionesImp.Columns[(int)EnumColumnasGrilla.FoTipoMovimiento].Visible = true;
                    dgvTransaccionesImp.Columns[(int)EnumColumnasGrilla.FoPlaca].Visible = false;
                    dgvTransaccionesImp.Columns[(int)EnumColumnasGrilla.FoPais].Visible = true;
                    break;
                case "RCIN":
                case "DSIN":
                    dgvTransaccionesImp.Columns[(int)EnumColumnasGrilla.FoPrecio].Visible = false;
                    dgvTransaccionesImp.Columns[(int)EnumColumnasGrilla.FoImporte].Visible = false;
                    dgvTransaccionesImp.Columns[(int)EnumColumnasGrilla.FoTipoMovimiento].Visible = false;
                    break;
            }

            var column = (GridViewCommandColumn)dgvTransaccionesImp.Columns[0];
            column.ShowNewButtonInHeader = true;

            Session.Remove(CVariablesSesion.HabilitaBotones);
            if (Session[CVariablesSesion.HabilitaFunciones] == null || Convert.ToInt16(Session[CVariablesSesion.PermisoRevi]) == 1)
            {
                Session[CVariablesSesion.HabilitaBotones] = "MODNO";
                column.ShowNewButtonInHeader = false;
            }
            else
            {
                if (binarioEstado == "1")
                {
                    Session[CVariablesSesion.HabilitaBotones] = "MODSI";
                }
                else
                {
                    Session[CVariablesSesion.HabilitaBotones] = "ELISI";
                    column.ShowNewButtonInHeader = false;
                    //column.ShowEditButton = false;
                    //column.ShowDeleteButton = false;
                }
            }
        }

        /// <summary>
        /// METODO. MValidaPermisos
        /// </summary>
        /// <param name="strMesnaje"></param>
        /// <returns></returns>
        private bool MValidaPermisos(ref string strMesnaje)
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
                strParametro4 = detFechaIni.Date.Year + detFechaIni.Date.Month.ToString().PadLeft(2, '0') + "01" + "000000",
                strParametro5 = detFechaFin.Date.Year + detFechaFin.Date.Month.ToString().PadLeft(2, '0') + DateTime.DaysInMonth(detFechaFin.Date.Year, detFechaFin.Date.Month).ToString().PadLeft(2, '0') + "235959",
                strParametro6 = "",
                decAppFechaRegistro = 0,
                decAppIdUsuario = 0
            });
            if (objValidacion.IntCodigo == 1)
            {
                if (objValidacion.OResultado.RESULTADO == 1)
                {
                    if (tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString() != "16")
                    {
                        if (tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString() != "36")
                        {
                            //Valida si esta en periodo
                            var lstPeriodos = Session[CVariablesSesion.ListaPeriodos] as List<O_PERIODO_CTY>;
                            if (lstPeriodos.Any())
                            {
                                var lstValida = lstPeriodos.Where(x => x.decAño == detFechaIni.Date.Year && x.decMes == detFechaIni.Date.Month).ToList();
                                if (!lstValida.Any())
                                {
                                    booRespuesta = false;
                                    strMesnaje = "Pediodo Cerrado.";
                                }
                            }
                            else
                            {
                                booRespuesta = false;
                                strMesnaje = "Pediodo Cerrado.";
                            }
                        }
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
        /// METODO. MConfiguraFormulario
        /// </summary>
        private void MConfiguraFormulario()
        {
            #region Instancia componentes
            var cmbTipoTransEf = (dgvTransacciones.FindEditFormTemplateControl("cmbTipoTransEf")) as ASPxComboBox;
            var cmbMedioTransEf = (dgvTransacciones.FindEditFormTemplateControl("cmbMedioTransEf")) as ASPxComboBox;
            var cmbEntidadAuxialiarEf = (dgvTransacciones.FindEditFormTemplateControl("cmbEntidadAuxialiarEf")) as ASPxComboBox;
            var cmbTipoMedicEf = (dgvTransacciones.FindEditFormTemplateControl("cmbTipoMedicEf")) as ASPxComboBox;
            var cmbTipoMovEf = (dgvTransacciones.FindEditFormTemplateControl("cmbTipoMovEf")) as ASPxComboBox;
            var cmbPaisEf = (dgvTransacciones.FindEditFormTemplateControl("cmbPaisEf")) as ASPxComboBox;
            var txtPlacaEf = (dgvTransacciones.FindEditFormTemplateControl("txtPlacaEf")) as ASPxTextBox;
            var txtSubIdentEf = (dgvTransacciones.FindEditFormTemplateControl("txtSubIdentEf")) as ASPxTextBox;
            var txtCodigoEf = (dgvTransacciones.FindEditFormTemplateControl("txtCodigoEf")) as ASPxTextBox;
            var cmbTanqueEf = (dgvTransacciones.FindEditFormTemplateControl("cmbTanqueEf")) as ASPxComboBox;
            var txtVolAguaEf = (dgvTransacciones.FindEditFormTemplateControl("txtVolAguaEf")) as ASPxTextBox;
            var lblTipoTransEf = (dgvTransacciones.FindEditFormTemplateControl("lblTipoTransEf")) as ASPxLabel;
            var lblMedioTransEf = (dgvTransacciones.FindEditFormTemplateControl("lblMedioTransEf")) as ASPxLabel;
            var lblEntidadAuxialiarEf = (dgvTransacciones.FindEditFormTemplateControl("lblEntidadAuxialiarEf")) as ASPxLabel;
            var lblTipoMedicEf = (dgvTransacciones.FindEditFormTemplateControl("lblTipoMedicEf")) as ASPxLabel;
            var lblTipoMovEf = (dgvTransacciones.FindEditFormTemplateControl("lblTipoMovEf")) as ASPxLabel;
            var lblPaisEf = (dgvTransacciones.FindEditFormTemplateControl("lblPaisEf")) as ASPxLabel;
            var lblPlacaEf = (dgvTransacciones.FindEditFormTemplateControl("lblPlacaEf")) as ASPxLabel;
            var lblSubIdentEf = (dgvTransacciones.FindEditFormTemplateControl("lblSubIdentEf")) as ASPxLabel;
            var lblCodigoEf = (dgvTransacciones.FindEditFormTemplateControl("lblCodigoEf")) as ASPxLabel;
            var lblTanqueEf = (dgvTransacciones.FindEditFormTemplateControl("lblTanqueEf")) as ASPxLabel;
            var lblVolAguaEf = (dgvTransacciones.FindEditFormTemplateControl("lblVolAguaEf")) as ASPxLabel;
            var cmbTipoTempEf = (dgvTransacciones.FindEditFormTemplateControl("cmbTipoTempEf")) as ASPxComboBox;
            var lblTemperaturaEf = (dgvTransacciones.FindEditFormTemplateControl("lblTemperaturaEf")) as ASPxLabel;
            var txtTemperaturaEf = (dgvTransacciones.FindEditFormTemplateControl("txtTemperaturaEf")) as ASPxTextBox;
            var lblGravedadEf = (dgvTransacciones.FindEditFormTemplateControl("lblGravedadEf")) as ASPxLabel;
            var txtGravedadEf = (dgvTransacciones.FindEditFormTemplateControl("txtGravedadEf")) as ASPxTextBox;
            var lblIntegracionEf = (dgvTransacciones.FindEditFormTemplateControl("lblIntegracionEf")) as ASPxLabel;
            var txtIntegracionEf = (dgvTransacciones.FindEditFormTemplateControl("txtIntegracionEf")) as ASPxTextBox;
            var cmbTipoEnvaseEf = (dgvTransacciones.FindEditFormTemplateControl("cmbTipoEnvaseEf")) as ASPxComboBox;
            var lblTipoEnvaseEf = (dgvTransacciones.FindEditFormTemplateControl("lblTipoEnvaseEf")) as ASPxLabel;
            var lblTipoInfEf = (dgvTransacciones.FindEditFormTemplateControl("lblTipoInfEf")) as ASPxLabel;
            var cmbTipoInfEf = (dgvTransacciones.FindEditFormTemplateControl("cmbTipoInfEf")) as ASPxComboBox;
            var lblBarCodeEf = (dgvTransacciones.FindEditFormTemplateControl("lblBarCodeEf")) as ASPxLabel;
            var txtBarCodeEf = (dgvTransacciones.FindEditFormTemplateControl("txtBarCodeEf")) as ASPxTextBox;

            var lblTipoDespachoEf = (dgvTransacciones.FindEditFormTemplateControl("lblTipoDespachoEf")) as ASPxLabel;
            var cmbTipoDespachoEf = (dgvTransacciones.FindEditFormTemplateControl("cmbTipoDespachoEf")) as ASPxComboBox;




            // var cmbTipoFlujoEf = (dgvTransaccionesImp.FindEditFormTemplateControl("cmbTipoFlujoEf")) as ASPxComboBox;
            var cmbEstadoEf = (dgvTransaccionesImp.FindEditFormTemplateControl("cmbEstadoEf")) as ASPxComboBox;
            var cmbProdEf = (dgvTransaccionesImp.FindEditFormTemplateControl("cmbProdEf")) as ASPxComboBox;
            var cmbUnidadEf = (dgvTransaccionesImp.FindEditFormTemplateControl("cmbUnidadEf")) as ASPxComboBox;
            var txtEstJsonRef = (dgvTransacciones.FindEditFormTemplateControl("txtEstJsonRef")) as ASPxTextBox;
            var txtIdCuenta = (dgvTransacciones.FindEditFormTemplateControl("txtIdCuenta")) as ASPxTextBox;
            var txtIdCuentaAux = (dgvTransacciones.FindEditFormTemplateControl("txtIdCuentaAux")) as ASPxTextBox;
            var txtIdParGl = (dgvTransacciones.FindEditFormTemplateControl("txtIdParGl")) as ASPxTextBox;
            var txtIdParEs = (dgvTransacciones.FindEditFormTemplateControl("txtIdParEs")) as ASPxTextBox;

            var lblTipoPagoEf = (dgvTransacciones.FindEditFormTemplateControl("lblTipoPagoEf")) as ASPxLabel;
            var cmbTipoFlujoEf = (dgvTransacciones.FindEditFormTemplateControl("cmbTipoFlujoEf")) as ASPxComboBox;

            //  var txtUsuarioEf = (dgvTransaccionesImp.FindEditFormTemplateControl("txtUsuarioEf"));

            #endregion

            var to = Session[CVariablesSesion.Aux].ToString();
            if (to != null && to.Trim().Length > 0)
            {
                var strTipoOp = to.Substring(2, 4);

                switch (strTipoOp)
                {
                    case "RCAJ":
                    case "DSAJ":
                        #region detalle
                        lblEntidadAuxialiarEf.Visible = false;
                        cmbEntidadAuxialiarEf.Visible = false;
                        lblTipoMovEf.Visible = false;
                        cmbTipoMovEf.Visible = false;
                        lblPaisEf.Visible = false;
                        cmbPaisEf.Visible = false;
                        lblMedioTransEf.Visible = false;
                        cmbMedioTransEf.Visible = false;
                        lblTipoTransEf.Visible = false;
                        cmbTipoTransEf.Visible = false;
                        lblTipoMedicEf.Visible = false;
                        cmbTipoMedicEf.Visible = false;
                        lblVolAguaEf.Visible = false;
                        txtVolAguaEf.Visible = false;
                        lblPlacaEf.Visible = false;
                        txtPlacaEf.Visible = false;
                        lblBarCodeEf.Visible = false;
                        txtBarCodeEf.Visible = false;
                        lblSubIdentEf.Visible = false;
                        txtSubIdentEf.Visible = false;
                        lblCodigoEf.Visible = false;
                        txtCodigoEf.Visible = false;
                        lblTipoEnvaseEf.Visible = false;
                        cmbTipoEnvaseEf.Visible = false;
                        lblTemperaturaEf.Visible = false;
                        txtTemperaturaEf.Visible = false;
                        cmbTipoTempEf.Visible = false;
                        lblGravedadEf.Visible = false;
                        txtGravedadEf.Visible = false;
                        lblTipoDespachoEf.Visible = false;
                        cmbTipoDespachoEf.Visible = false;
                        #endregion
                        break;
                    case "RCEX":
                    case "DSEX":
                        #region detalle
                        lblTipoMedicEf.Visible = false;
                        cmbTipoMedicEf.Visible = false;
                        lblVolAguaEf.Visible = false;
                        txtVolAguaEf.Visible = false;
                        if (strTipoOp.Contains("RC"))
                        {
                            lblTipoEnvaseEf.Visible = false;
                            cmbTipoEnvaseEf.Visible = false;
                            if (Convert.ToInt32(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()) ==
                                (int)EnumActividades.EstacionServicioLiquido ||
                                Convert.ToInt32(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()) ==
                                (int)EnumActividades.EstacionServicioGnv)
                            {
                                lblTemperaturaEf.Visible = false;
                                txtTemperaturaEf.Visible = false;
                                cmbTipoTempEf.Visible = false;
                                lblGravedadEf.Visible = false;
                                txtGravedadEf.Visible = false;
                            }
                            if (Convert.ToInt32(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()) ==
                                (int)EnumActividades.ImportacionCarburantes ||
                                Convert.ToInt32(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()) ==
                                (int)EnumActividades.ImportacionLubricantes) //LMQC
                            {
                                lblMedioTransEf.Visible = false;
                                cmbMedioTransEf.Visible = false;

                                lblTipoTransEf.Visible = false;
                                cmbTipoTransEf.Visible = false;

                                lblTanqueEf.Visible = false;
                                cmbTanqueEf.Visible = false;
                                lblTipoInfEf.Visible = false;
                                cmbTipoInfEf.Visible = false;

                                lblTipoMedicEf.Visible = false;
                                cmbTipoMedicEf.Visible = false;
                                lblVolAguaEf.Visible = false;
                                txtVolAguaEf.Visible = false;

                                lblTemperaturaEf.Visible = false;
                                txtTemperaturaEf.Visible = false;
                                cmbTipoTempEf.Visible = false;
                                lblGravedadEf.Visible = false;
                                txtGravedadEf.Visible = false;

                                lblTipoDespachoEf.Visible = false;
                                cmbTipoDespachoEf.Visible = false;

                                lblCodigoEf.Visible = false;
                                txtCodigoEf.Visible = false;

                                lblPlacaEf.Visible = false;
                                txtPlacaEf.Visible = false;
                                lblSubIdentEf.Visible = false;
                                txtSubIdentEf.Visible = false;

                                lblTipoMovEf.Visible = false;
                                cmbTipoMovEf.Visible = false;
                                lblPaisEf.Visible = false;
                                cmbPaisEf.Visible = false;

                                lblEntidadAuxialiarEf.Visible = false;
                                cmbEntidadAuxialiarEf.Visible = false;

                            }
                        }
                        else
                        {
                            lblTemperaturaEf.Visible = false;
                            txtTemperaturaEf.Visible = false;
                            cmbTipoTempEf.Visible = false;
                            lblGravedadEf.Visible = false;
                            txtGravedadEf.Visible = false;
                        }
                        #endregion
                        break;
                    case "RCIN":
                    case "DSIN":
                        #region detalle
                        lblEntidadAuxialiarEf.Visible = false;
                        cmbEntidadAuxialiarEf.Visible = false;
                        lblTipoMovEf.Visible = false;
                        cmbTipoMovEf.Visible = false;
                        lblPaisEf.Visible = false;
                        cmbPaisEf.Visible = false;
                        lblTipoMedicEf.Visible = false;
                        cmbTipoMedicEf.Visible = false;
                        lblVolAguaEf.Visible = false;
                        txtVolAguaEf.Visible = false;
                        lblPlacaEf.Visible = false;
                        txtPlacaEf.Visible = false;
                        lblBarCodeEf.Visible = false;
                        txtBarCodeEf.Visible = false;
                        lblSubIdentEf.Visible = false;
                        txtSubIdentEf.Visible = false;
                        lblCodigoEf.Visible = false;
                        txtCodigoEf.Visible = false;
                        lblTipoEnvaseEf.Visible = false;
                        cmbTipoEnvaseEf.Visible = false;
                        lblTipoDespachoEf.Visible = false;
                        cmbTipoDespachoEf.Visible = false;
                        #endregion
                        break;
                    case "MDIN":
                    case "MDFI":
                        #region detalle
                        lblEntidadAuxialiarEf.Visible = false;
                        cmbEntidadAuxialiarEf.Visible = false;
                        lblIntegracionEf.Visible = false;
                        txtIntegracionEf.Visible = false;
                        lblTipoMovEf.Visible = false;
                        cmbTipoMovEf.Visible = false;
                        lblPaisEf.Visible = false;
                        cmbPaisEf.Visible = false;
                        lblMedioTransEf.Visible = false;
                        cmbMedioTransEf.Visible = false;
                        lblTipoTransEf.Visible = false;
                        cmbTipoTransEf.Visible = false;
                        lblPlacaEf.Visible = false;
                        txtPlacaEf.Visible = false;
                        lblBarCodeEf.Visible = false;
                        txtBarCodeEf.Visible = false;
                        lblSubIdentEf.Visible = false;
                        txtSubIdentEf.Visible = false;
                        lblCodigoEf.Visible = false;
                        txtCodigoEf.Visible = false;
                        lblTipoEnvaseEf.Visible = false;
                        cmbTipoEnvaseEf.Visible = false;
                        lblTipoDespachoEf.Visible = false;
                        cmbTipoDespachoEf.Visible = false;
                        #endregion
                        break;
                    case "SICB":
                        #region detalle
                        lblEntidadAuxialiarEf.Visible = false;
                        cmbEntidadAuxialiarEf.Visible = false;
                        lblIntegracionEf.Visible = false;
                        txtIntegracionEf.Visible = false;
                        lblTipoMovEf.Visible = false;
                        cmbTipoMovEf.Visible = false;
                        lblPaisEf.Visible = false;
                        cmbPaisEf.Visible = false;
                        lblMedioTransEf.Visible = false;
                        cmbMedioTransEf.Visible = false;
                        lblTipoTransEf.Visible = false;
                        cmbTipoTransEf.Visible = false;
                        var lst = Session[CVariablesSesion.OctanoParametrica] as List<O_LISTA_OCTANO_PAR_CTY>;
                        lst = lst.Where(x => x.TIPO_VALOR == 7).ToList();
                        if (lst.Any() && lst[0].VALOR_FIN == "1")
                        {
                            lblTanqueEf.Visible = false;
                            cmbTanqueEf.Visible = false;
                            lblTipoInfEf.Visible = false;
                            cmbTipoInfEf.Visible = false;
                        }
                        lblTipoMedicEf.Visible = false;
                        cmbTipoMedicEf.Visible = false;
                        lblVolAguaEf.Visible = false;
                        txtVolAguaEf.Visible = false;
                        lblPlacaEf.Visible = false;
                        txtPlacaEf.Visible = false;
                        lblBarCodeEf.Visible = false;
                        txtBarCodeEf.Visible = false;
                        lblSubIdentEf.Visible = false;
                        txtSubIdentEf.Visible = false;
                        lblCodigoEf.Visible = false;
                        txtCodigoEf.Visible = false;
                        lblTipoEnvaseEf.Visible = false;
                        cmbTipoEnvaseEf.Visible = false;
                        if (Convert.ToInt32(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()) ==
                            (int)EnumActividades.EstacionServicioLiquido ||
                            Convert.ToInt32(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()) ==
                            (int)EnumActividades.EstacionServicioGnv)
                        {
                            lblTemperaturaEf.Visible = false;
                            txtTemperaturaEf.Visible = false;
                            cmbTipoTempEf.Visible = false;
                            lblGravedadEf.Visible = false;
                            txtGravedadEf.Visible = false;
                        }
                        lblTipoDespachoEf.Visible = false;
                        cmbTipoDespachoEf.Visible = false;
                        #endregion
                        break;
                    case "RGIN":
                    case "DGIN":
                    case "RGEX":
                    case "DGEX":
                        #region detalle
                        lblPaisEf.Visible = false;
                        cmbPaisEf.Visible = false;
                        lblMedioTransEf.Visible = false;
                        cmbMedioTransEf.Visible = false;
                        lblTipoTransEf.Visible = false;
                        cmbTipoTransEf.Visible = false;
                        lblTipoMedicEf.Visible = false;
                        cmbTipoMedicEf.Visible = false;
                        lblVolAguaEf.Visible = false;
                        txtVolAguaEf.Visible = false;
                        lblTipoEnvaseEf.Visible = false;
                        cmbTipoEnvaseEf.Visible = false;
                        lblTemperaturaEf.Visible = false;
                        txtTemperaturaEf.Visible = false;
                        cmbTipoTempEf.Visible = false;
                        lblGravedadEf.Visible = false;
                        txtGravedadEf.Visible = false;
                        lblTanqueEf.Visible = false;
                        cmbTanqueEf.Visible = false;
                        lblTipoDespachoEf.Visible = false;
                        cmbTipoDespachoEf.Visible = false;
                        lblTipoInfEf.Visible = false;
                        cmbTipoInfEf.Visible = false;


                        #endregion
                        break;
                    case "NVEN":
                        #region detalle
                        Console.WriteLine("Case 2");
                        //  cmbTipoFlujoEf.Visible = false;
                        //  lblTipoPagoEf.Visible = false;

                        lblEntidadAuxialiarEf.Visible = false;
                        cmbEntidadAuxialiarEf.Visible = false;

                        lblTipoMovEf.Visible = false;
                        cmbTipoMovEf.Visible = false;

                        lblMedioTransEf.Visible = false;
                        cmbMedioTransEf.Visible = false;

                        lblTanqueEf.Visible = false;
                        cmbTanqueEf.Visible = false;

                        lblTipoMedicEf.Visible = false;
                        cmbTipoMedicEf.Visible = false;

                        lblPlacaEf.Visible = false;
                        txtPlacaEf.Visible = false;

                        lblBarCodeEf.Visible = false;
                        txtBarCodeEf.Visible = false;

                        lblCodigoEf.Visible = false;
                        txtCodigoEf.Visible = false;

                        lblTipoTransEf.Visible = false;
                        cmbTipoTransEf.Visible = false;

                        lblTipoInfEf.Visible = false;
                        cmbTipoInfEf.Visible = false;

                        lblVolAguaEf.Visible = false;
                        txtVolAguaEf.Visible = false;

                        lblSubIdentEf.Visible = false;
                        txtSubIdentEf.Visible = false;

                        lblTipoDespachoEf.Visible = false;
                        cmbTipoDespachoEf.Visible = false;

                        lblTipoEnvaseEf.Visible = false;
                        cmbTipoEnvaseEf.Visible = false;

                        lblGravedadEf.Visible = false;
                        txtGravedadEf.Visible = false;

                        cmbTipoTempEf.Visible = false;
                        lblTemperaturaEf.Text = "Temperatura °C :";

                        //  lblUsuarioEf.Visible = true;
                        // txtUsuarioEf.Visible = false;
                        #endregion
                        break;
                }
            }

        }

        /// <summary>
        /// METODO. MObtenerParametro
        /// </summary>
        /// <param name="decIdTransaccion"></param>
        /// <returns></returns>
        protected int MObtenerParametro(decimal decIdTransaccion)
        {
            int intResultado = -99999;

            var lstTipoTransaccion = (List<O_LISTA_TIPO_TRANSACCION_CTY>)Session[CVariablesSesion.ListaTransaccion];
            lstTipoTransaccion = new List<O_LISTA_TIPO_TRANSACCION_CTY>(lstTipoTransaccion.Where(w => w.ID_TIPO_TRANSACCION == decIdTransaccion));
            if (lstTipoTransaccion.Count > 0)
            {
                intResultado = Convert.ToInt32(lstTipoTransaccion.FirstOrDefault().ID_TIPO_TRANSACCION_PADRE.ToString());
            }

            return intResultado;
        }

        /// <summary>
        /// METODO. MLimpiaGrilla
        /// </summary>
        private void MLimpiaGrilla()
        {
            Session.Remove(CVariablesSesion.GrillaAcumulado);
            dgvAcumulado.DataSource = null;
            dgvAcumulado.DataBind();
            Session.Remove(CVariablesSesion.GrillaTransaccion);
            dgvTransacciones.DataSource = null;
            dgvTransacciones.DataBind();
            cmbTipoOperacion.DataSource = null;
            cmbTipoOperacion.DataBind();
            MImprimirMensaje("", EnumTiposMensajeAlerta.Limpiar);
            Session.Remove(CVariablesSesion.ComboGenerico);
            cmbFiltro.DataSource = null;
            cmbFiltro.DataBind();
            cmbFiltro.Text = "";
            pnlHerramienta.ClientVisible = false;
        }

        private void MLimpiaGrillaMasivo()
        {
            dgvTransCisMas.DataSource = null;
            dgvTransCisMas.DataBind();
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

        /// <summary>
        /// METODO. MRegistraSaldoDcod
        /// </summary>
        /// <param name="datFecha"></param>
        /// <param name="objDcod"></param>
        /// <param name="strMensaje"></param>
        private void MRegistraSaldoDcod(DateTime datFecha, O_LISTA_SALDOS_DCOD_CTY objDcod, ref string strMensaje)
        {
            var datFechaReg = DateTime.Now;
            var objNuevoReg = new ObjMovimientoTransaccion();

            objNuevoReg.idMovimiento = 0;
            objNuevoReg.idMovimientoPadre = 0;
            objNuevoReg.idTipoActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
            objNuevoReg.idTipoOperacion = (decimal)EnumTipoOperaciones.SaldoInicial;
            objNuevoReg.idConsumidor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString());
            objNuevoReg.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
            objNuevoReg.fechaRegistroMovimiento = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
            objNuevoReg.fechaOperacion = Convert.ToDecimal(datFecha.Year + datFecha.Month.ToString().PadLeft(2, '0') + datFecha.Day.ToString().PadLeft(2, '0') + "000000");
            objNuevoReg.idTipoMovimientoFlujo = (decimal)EnumTipoMovimientoFlujo.Fisico;
            objNuevoReg.idTipoMovimiento = (decimal)EnumTipoMovimiento.NacNac;
            objNuevoReg.idEstadoMovimiento = (decimal)EnumEstadoMov.Activo;
            objNuevoReg.idFuenteOrigenDato = (decimal)EnumTipoRegistro.OrigenWeb;
            objNuevoReg.idUbicacionOperacion = (decimal)EnumPais.BOLIVIA;
            objNuevoReg.idUnidadMedidaHidrocarburo = 68;
            objNuevoReg.idHidrocarburo = objDcod.ID_PRODUCTO;
            objNuevoReg.cantidadHidrocarburo = objDcod.SALDO;
            objNuevoReg.identificador = "";
            objNuevoReg.subIdentificador = "";
            objNuevoReg.observacion = "SALDO IMPORTADO DCODIFICADOR - FECHA REGISTRO: " + objDcod.FECHA_REGISTRO + " - PERIODO: " + objDcod.PERIODO.Month + "/" + objDcod.PERIODO.Year;
            objNuevoReg.barcode = 0;

            objNuevoReg.idTipoMedicion = -99999;
            objNuevoReg.cantidadAguaLitros = 0;

            objNuevoReg.idTipoMedioTransporte = -99999;
            objNuevoReg.idMedioTransporte = -99999;

            objNuevoReg.tipoInfraestructura = -99999;
            objNuevoReg.idInfraestructura = -99999;

            objNuevoReg.idUnidadEnvasesTransporte = -99999;
            objNuevoReg.codigo = "";

            objNuevoReg.idIntegracion = objDcod.ID_MOVIMIENTO;
            objNuevoReg.idUnidadTemperatura = -99999;
            objNuevoReg.temperatura = 0;
            objNuevoReg.gradoApi = "";
            objNuevoReg.idEntidad = -99999;

            objNuevoReg.cantidadEnvasesTransporte = 0;
            objNuevoReg.binarioControl = "00000-0000000000-0000000000-00000";
            objNuevoReg.idTransaccionBaseExterna = "";

            objNuevoReg.jsonReferencia = "";
            objNuevoReg.estadoJsonRef = -99999;
            objNuevoReg.idCuenta = -99999;
            objNuevoReg.idCuentaAux = -99999;
            objNuevoReg.idParamGlobal = -99999;
            objNuevoReg.idParamEspec = -99999;
            objNuevoReg.idTipoConsumidor = -99999;
            objNuevoReg.idTipoInfraAux = -99999;
            objNuevoReg.idInfraAux = -99999;

            objNuevoReg.listaTransaccion = new List<ObjTransaccion>();

            var objTrasaccion = new ObjTransaccion();
            objTrasaccion.idMovimiento = 0;
            objTrasaccion.idTransaccion = 0;
            objTrasaccion.idTransaccionPadre = 0;
            objTrasaccion.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
            objTrasaccion.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
            objTrasaccion.idTipoTransaccion = (decimal)EnumTipoTransaccion.NotaMovimiento;
            objTrasaccion.fechaEmision = objNuevoReg.fechaOperacion;
            objTrasaccion.numeroTransaccion = objDcod.ID_MOVIMIENTO.ToString();
            objTrasaccion.numeroAutorizacion = 0;
            objTrasaccion.codigoControl = "";
            objTrasaccion.nitConsumidor = 0;
            objTrasaccion.razonSocial = "";
            objTrasaccion.precioUnitario = 0;
            objTrasaccion.precioTotal = 0;
            objTrasaccion.usuarioTransaccion = "DCODIFICADOR";
            objTrasaccion.observaciones = "";
            objTrasaccion.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
            objTrasaccion.idEstadoTransaccion = objNuevoReg.idEstadoMovimiento;
            objTrasaccion.idTipoCreditoFiscal = (decimal)EnumTipoCredFiscal.No;
            objTrasaccion.idFormaPago = (decimal)EnumTiposPago.Efectivo;
            objTrasaccion.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
            objTrasaccion.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
            objTrasaccion.codigoSeguridad = 0;
            objTrasaccion.idAutorizacionDato = 0;
            objTrasaccion.binarioControl = "0000000000";

            objTrasaccion.fechaOperacion = Convert.ToDecimal(datFecha.Year + datFecha.Month.ToString().PadLeft(2, '0') + datFecha.Day.ToString().PadLeft(2, '0') + "000000");
            objTrasaccion.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
            objTrasaccion.codigo = "";

            objNuevoReg.listaTransaccion.Add(objTrasaccion);

            var objRegistro = new ERegistraMovimientoTransaccion();
            objRegistro.strCredencial = CParametrosHydro.strCredencialFuncionario;
            objRegistro.decFiltro = 1;
            var lstRegistros = new List<ObjMovimientoTransaccion>();
            lstRegistros.Add(objNuevoReg);
            objRegistro.listaMovimientoTransaccion = lstRegistros;

            //var ojo = JsonSerializer.SerializeToString(objRegistro);
            var resultado = _client.Post<EResultado>(CAppSettings.svc_registra_movimiento_transaccion, objRegistro);
            if (resultado.intCodigo != 1)
            {
                strMensaje = resultado.strMensaje;
            }
        }

        /// <summary>
        /// METODO. MRegistraRecepcionDcod
        /// </summary>
        /// <param name="decIdTipoOp"></param>
        /// <param name="objDcod"></param>
        /// <param name="strMensaje"></param>
        private void MRegistraRecepcionDcod(decimal decIdTipoOp, O_LISTA_RECEPCION_DCOD_CTY objDcod, ref string strMensaje)
        {
            var datFechaReg = DateTime.Now;
            var objNuevoReg = new ObjMovimientoTransaccion();

            objNuevoReg.idMovimiento = 0;
            objNuevoReg.idMovimientoPadre = 0;
            objNuevoReg.idTipoActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
            objNuevoReg.idTipoOperacion = decIdTipoOp;
            objNuevoReg.idConsumidor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString());
            objNuevoReg.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
            objNuevoReg.fechaRegistroMovimiento = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
            objNuevoReg.fechaOperacion = objDcod.F_OPERACION;
            objNuevoReg.idTipoMovimientoFlujo = (decimal)EnumTipoMovimientoFlujo.Fisico;
            objNuevoReg.idTipoMovimiento = (decimal)EnumTipoMovimiento.NacNac;
            objNuevoReg.idEstadoMovimiento = (decimal)EnumEstadoMov.Activo;
            objNuevoReg.idFuenteOrigenDato = (decimal)EnumTipoRegistro.OrigenWeb;
            objNuevoReg.idUbicacionOperacion = (decimal)EnumPais.BOLIVIA;
            objNuevoReg.idUnidadMedidaHidrocarburo = 68;
            objNuevoReg.idHidrocarburo = objDcod.ID_COMBUSTIBLE;
            objNuevoReg.cantidadHidrocarburo = objDcod.LITROS_FACTURA;
            objNuevoReg.identificador = objDcod.PLACA;
            objNuevoReg.subIdentificador = "";
            objNuevoReg.observacion = "RECEPCION IMPORTADO DCODIFICADOR";
            objNuevoReg.barcode = 0;

            objNuevoReg.idTipoMedicion = -99999;
            objNuevoReg.cantidadAguaLitros = 0;

            objNuevoReg.idTipoMedioTransporte = -99999;
            objNuevoReg.idMedioTransporte = -99999;

            objNuevoReg.tipoInfraestructura = -99999;
            objNuevoReg.idInfraestructura = -99999;

            objNuevoReg.idUnidadEnvasesTransporte = -99999;
            objNuevoReg.codigo = "";

            objNuevoReg.idIntegracion = objDcod.ID_RECEPCION;
            objNuevoReg.idUnidadTemperatura = -99999;
            objNuevoReg.temperatura = 0;
            objNuevoReg.gradoApi = "";
            objNuevoReg.idEntidad = -99999;

            objNuevoReg.cantidadEnvasesTransporte = 0;
            objNuevoReg.binarioControl = "00000-0000000000-0000000000-00000";
            objNuevoReg.idTransaccionBaseExterna = "";

            objNuevoReg.jsonReferencia = "";
            objNuevoReg.estadoJsonRef = -99999;
            objNuevoReg.idCuenta = -99999;
            objNuevoReg.idCuentaAux = -99999;
            objNuevoReg.idParamGlobal = -99999;
            objNuevoReg.idParamEspec = -99999;
            objNuevoReg.idTipoConsumidor = -99999;
            objNuevoReg.idTipoInfraAux = -99999;
            objNuevoReg.idInfraAux = -99999;

            objNuevoReg.listaTransaccion = new List<ObjTransaccion>();

            var objTrasaccion = new ObjTransaccion();
            objTrasaccion.idMovimiento = 0;
            objTrasaccion.idTransaccion = 0;
            objTrasaccion.idTransaccionPadre = 0;
            objTrasaccion.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
            objTrasaccion.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
            objTrasaccion.idTipoTransaccion = (decimal)EnumTipoTransaccion.FacturaComputarizada;
            objTrasaccion.fechaEmision = objDcod.FECHA_FACTURA;
            objTrasaccion.numeroTransaccion = objDcod.NUMERO_FACTURA.ToString();
            objTrasaccion.numeroAutorizacion = 0;
            objTrasaccion.codigoControl = "";
            objTrasaccion.nitConsumidor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
            objTrasaccion.razonSocial = "";
            objTrasaccion.precioUnitario = 0;
            objTrasaccion.precioTotal = 0;
            objTrasaccion.usuarioTransaccion = "DCODIFICADOR";
            objTrasaccion.observaciones = "";
            objTrasaccion.nitEmisor = 0;
            objTrasaccion.idEstadoTransaccion = objNuevoReg.idEstadoMovimiento;
            objTrasaccion.idTipoCreditoFiscal = (decimal)EnumTipoCredFiscal.Si;
            objTrasaccion.idFormaPago = (decimal)EnumTiposPago.Efectivo;
            objTrasaccion.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
            objTrasaccion.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
            objTrasaccion.codigoSeguridad = 0;
            objTrasaccion.idAutorizacionDato = 0;
            objTrasaccion.binarioControl = "0000000000";

            objTrasaccion.fechaOperacion = objDcod.F_OPERACION;
            objTrasaccion.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
            objTrasaccion.codigo = "";

            objNuevoReg.listaTransaccion.Add(objTrasaccion);

            if (objDcod.NRO_ORDEN.Length > 1)
            {
                //objNuevoReg.listaTransaccion = new List<ObjTransaccion>();

                objTrasaccion = new ObjTransaccion();
                objTrasaccion.idMovimiento = 0;
                objTrasaccion.idTransaccion = 0;
                objTrasaccion.idTransaccionPadre = 0;
                objTrasaccion.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                objTrasaccion.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                objTrasaccion.idTipoTransaccion = (decimal)EnumTipoTransaccion.DocDespacho;
                objTrasaccion.fechaEmision = objDcod.F_OPERACION;
                objTrasaccion.numeroTransaccion = objDcod.ID_RECEPCION.ToString();
                objTrasaccion.numeroAutorizacion = Convert.ToDecimal(objDcod.NRO_ORDEN);
                objTrasaccion.codigoControl = "";
                objTrasaccion.nitConsumidor = 0;
                objTrasaccion.razonSocial = "";
                objTrasaccion.precioUnitario = 0;
                objTrasaccion.precioTotal = 0;
                objTrasaccion.usuarioTransaccion = "DCODIFICADOR";
                objTrasaccion.observaciones = "";
                objTrasaccion.nitEmisor = 0;
                objTrasaccion.idEstadoTransaccion = objNuevoReg.idEstadoMovimiento;
                objTrasaccion.idTipoCreditoFiscal = (decimal)EnumTipoCredFiscal.No;
                objTrasaccion.idFormaPago = (decimal)EnumTiposPago.Efectivo;
                objTrasaccion.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                objTrasaccion.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                objTrasaccion.codigoSeguridad = 0;
                objTrasaccion.idAutorizacionDato = 0;
                objTrasaccion.binarioControl = "0000000000";

                objTrasaccion.fechaOperacion = objDcod.F_OPERACION;
                objTrasaccion.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                objTrasaccion.codigo = "";

                objNuevoReg.listaTransaccion.Add(objTrasaccion);
            }
            if (objDcod.HOJA_RUTA.Length > 0 && objDcod.HOJA_RUTA != "0")
            {
                //objNuevoReg.listaTransaccion = new List<ObjTransaccion>();

                objTrasaccion = new ObjTransaccion();
                objTrasaccion.idMovimiento = 0;
                objTrasaccion.idTransaccion = 0;
                objTrasaccion.idTransaccionPadre = 0;
                objTrasaccion.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                objTrasaccion.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                objTrasaccion.idTipoTransaccion = (decimal)EnumTipoTransaccion.AutSustCtrl;
                objTrasaccion.fechaEmision = objDcod.F_OPERACION;
                objTrasaccion.numeroTransaccion = objDcod.HOJA_RUTA;
                objTrasaccion.numeroAutorizacion = 0;
                objTrasaccion.codigoControl = "";
                objTrasaccion.nitConsumidor = 0;
                objTrasaccion.razonSocial = "";
                objTrasaccion.precioUnitario = 0;
                objTrasaccion.precioTotal = 0;
                objTrasaccion.usuarioTransaccion = "DCODIFICADOR";
                objTrasaccion.observaciones = "";
                objTrasaccion.nitEmisor = 0;
                objTrasaccion.idEstadoTransaccion = objNuevoReg.idEstadoMovimiento;
                objTrasaccion.idTipoCreditoFiscal = (decimal)EnumTipoCredFiscal.No;
                objTrasaccion.idFormaPago = (decimal)EnumTiposPago.Efectivo;
                objTrasaccion.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                objTrasaccion.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                objTrasaccion.codigoSeguridad = 0;
                objTrasaccion.idAutorizacionDato = 0;
                objTrasaccion.binarioControl = "0000000000";

                objTrasaccion.fechaOperacion = objDcod.F_OPERACION;
                objTrasaccion.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                objTrasaccion.codigo = "";

                objNuevoReg.listaTransaccion.Add(objTrasaccion);
            }

            if (objDcod.PRECINTO.Trim().Length > 0 && objDcod.PRECINTO != "0" && objDcod.PRECINTO != "-")
            {
                var arreglo = objDcod.PRECINTO.Trim().Split('-');
                for (var i = 0; i < arreglo.Count(); i++)
                {
                    if (arreglo[i].Length > 0)
                    {
                        //objNuevoReg.listaTransaccion = new List<ObjTransaccion>();

                        objTrasaccion = new ObjTransaccion();
                        objTrasaccion.idMovimiento = 0;
                        objTrasaccion.idTransaccion = 0;
                        objTrasaccion.idTransaccionPadre = 0;
                        objTrasaccion.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                        objTrasaccion.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                        objTrasaccion.idTipoTransaccion = (decimal)EnumTipoTransaccion.DocPrecinto;
                        objTrasaccion.fechaEmision = objDcod.F_OPERACION;
                        objTrasaccion.numeroTransaccion = arreglo[i].Trim();
                        objTrasaccion.numeroAutorizacion = 0;
                        objTrasaccion.codigoControl = "";
                        objTrasaccion.nitConsumidor = 0;
                        objTrasaccion.razonSocial = "";
                        objTrasaccion.precioUnitario = 0;
                        objTrasaccion.precioTotal = 0;
                        objTrasaccion.usuarioTransaccion = "DCODIFICADOR";
                        objTrasaccion.observaciones = "";
                        objTrasaccion.nitEmisor = 0;
                        objTrasaccion.idEstadoTransaccion = objNuevoReg.idEstadoMovimiento;
                        objTrasaccion.idTipoCreditoFiscal = (decimal)EnumTipoCredFiscal.No;
                        objTrasaccion.idFormaPago = (decimal)EnumTiposPago.Efectivo;
                        objTrasaccion.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                        objTrasaccion.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                        objTrasaccion.codigoSeguridad = 0;
                        objTrasaccion.idAutorizacionDato = 0;
                        objTrasaccion.binarioControl = "0000000000";

                        objTrasaccion.fechaOperacion = objDcod.F_OPERACION;
                        objTrasaccion.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                        objTrasaccion.codigo = "";

                        objNuevoReg.listaTransaccion.Add(objTrasaccion);
                    }
                }
            }

            var objRegistro = new ERegistraMovimientoTransaccion();
            objRegistro.strCredencial = CParametrosHydro.strCredencialFuncionario;
            objRegistro.decFiltro = 1;
            var lstRegistros = new List<ObjMovimientoTransaccion>();
            lstRegistros.Add(objNuevoReg);
            objRegistro.listaMovimientoTransaccion = lstRegistros;

            //var ojo = JsonSerializer.SerializeToString(objRegistro);
            var resultado = _client.Post<EResultado>(CAppSettings.svc_registra_movimiento_transaccion, objRegistro);
            if (resultado.intCodigo != 1)
            {
                strMensaje = resultado.strMensaje;
            }
        }

        void ApplyLayout(int layoutIndex)
        {
            dgvTransacciones.BeginUpdate();
            try
            {
                dgvTransacciones.ClearSort();
                switch (layoutIndex)
                {
                    case 0:
                        dgvTransacciones.GroupBy(dgvTransacciones.Columns["STR_FECHA"]);
                        dgvTransacciones.GroupBy(dgvTransacciones.Columns["IDENTIFICADOR"]);
                        /*dgvTransacciones.Columns("CANTIDAD_TRANS").visible = true;
                        dgvTransacciones.Columns("PRECIO_UNITARIO_TRANS").visible = true;
                        dgvTransacciones.Columns("PRECIO_TOTAL_TRANS").visible = true;*/
                        btnCollapse.Visible = true;
                        btnExpand.Visible = true;
                        break;
                    case 1:
                        /*dgvTransacciones.Columns("CANTIDAD_TRANS").visible = false;
                        dgvTransacciones.Columns("PRECIO_UNITARIO_TRANS").visible = false;
                        dgvTransacciones.Columns("PRECIO_TOTAL_TRANS").visible = false;*/
                        btnCollapse.Visible = false;
                        btnExpand.Visible = false;
                        break;
                }
            }
            finally
            {
                dgvTransacciones.EndUpdate();
            }
            dgvTransacciones.ExpandAll();
        }
       
        /// <summary>
        /// Metodo para el envio de email de confirmacion en el sistema
        /// </summary>
        private bool mEnviarEmail(List<O_LISTA_PRODUCTO_AUX> lstProducto, string strDepartamento, string strDenominacion, string strFechaSaldoFisico, O_DETALLE_USUARIO_CTY oDetUsuario)
        {
            bool boolResultado = true;
            StringBuilder htmlBuilder = new StringBuilder();

            htmlBuilder.Append("<html><head></head><body>");
            htmlBuilder.Append("<span style='font-size:18px; font-family:\"Century Gothic\"; text-align:justify;'>");
            htmlBuilder.Append("<table cellpadding='5' style='border:#008000 solid thin; width:650px; background:#008000;'>");
            htmlBuilder.Append("<tbody><tr><td style='text-align:center;'>");
            htmlBuilder.Append("<span style='font-size:17px; text-align:center; color:#FFFFFF; font-family:arial;font-weight:bold;'>");
            htmlBuilder.Append("DECLARACION JURADA - SALDO FISICO");
            htmlBuilder.Append("</span></td></tr></tbody></table>");
            htmlBuilder.Append("<table cellpadding='15' style='border: #008000 solid thin; background: #f5f5dc; width:650px;'>");
            htmlBuilder.Append("<tbody><tr><td><span>ESTIMADO USUARIO</span>:<br/><br/>");
            htmlBuilder.AppendFormat("<span style='font-size:16px;'>SE ACABA DE REALIZAR EL REGISTRO DE COMBUSTIBLES EN FECHA <strong>{0}</strong> EN EL SISTEMA OCTANO VOLUMENES, CORRESPONDIENTE A LA ESTACION DE SERVICIO <strong>{1}</strong> ", DateTime.Now, strDenominacion);
            htmlBuilder.AppendFormat("<br><br>LAS VENTAS REGISTRADAS LA JORNADA DE AYER <strong>{0}</strong> CORRESPONDEN A UN VOLUMEN DE:<br><br>", DateTime.Parse(strFechaSaldoFisico).AddDays(-1).ToShortDateString());
            htmlBuilder.Append("<ul>");

            foreach (var item in lstProducto)
            {
                htmlBuilder.AppendFormat("<li type='square'>{0} {1} DE {2}</li>", item.CANTIDAD, item.UNIDAD_MEDIDA, item.PRODUCTO);
            }

            htmlBuilder.Append("</ul>");
            htmlBuilder.AppendFormat("<br><br><span style='font-size:12px;'>Atte.: Administrador de Sistemas HYDRO<br/>Fecha: {0}</span>", DateTime.Now);
            htmlBuilder.Append("</td></tr></tbody></table></span></body></html>");

            string html = htmlBuilder.ToString();

            try
            {                           
                CCorreo.mEnviarEmail(
                                     CParametrosHydro.StrServidorDireccion,
                                     CParametrosHydro.IntServidorPuerto,
                                     CParametrosHydro.StrUsuarioLogin,
                                     CParametrosHydro.StrUsuarioPassword,
                                     CParametrosHydro.StrUsuarioDe,
                                     oDetUsuario.LOGIN.ToLower(),
                                     "",//CParametrosHydro.StrUsuarioCc,
                                     "",//CParametrosHydro.StrUsuarioCco,
                                     "MENSAJE ANH:",
                                     html,
                                     null,
                                     true,
                                     CParametrosHydro.bolHabilitarSsl,
                                     CParametrosHydro.bolNotificarError);               
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
                boolResultado=false;
                #endregion
            }

            if (boolResultado)
            {
                try
                {
                    CCorreo.mEnviarEmail(
                                         CParametrosHydro.StrServidorDireccion,
                                         CParametrosHydro.IntServidorPuerto,
                                         CParametrosHydro.StrUsuarioLogin,
                                         CParametrosHydro.StrUsuarioPassword,
                                         CParametrosHydro.StrUsuarioDe,
                                         //oDetUsuario.LOGIN.ToLower(),
                                         CParametrosHydro.StrUsuarioCc,
                                         "",
                                         CParametrosHydro.StrUsuarioCco,
                                         "MENSAJE ANH:",
                                         html,
                                         null,
                                         true,
                                         CParametrosHydro.bolHabilitarSsl,
                                         CParametrosHydro.bolNotificarError);
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
                    boolResultado = false;
                    #endregion
                }
            }
            else
            {
                try
                {
                    StringBuilder htmlBuilder2 = new StringBuilder();

                    htmlBuilder2.Append("<html><head></head><body>");
                    htmlBuilder2.Append("<span style='font-size:18px; font-family:\"Century Gothic\"; text-align:justify;'>");
                    htmlBuilder2.Append("<table cellpadding='5' style='border:#008000 solid thin; width:650px; background:#008000;'>");
                    htmlBuilder2.Append("<tbody><tr><td style='text-align:center;'>");
                    htmlBuilder2.Append("<span style='font-size:17px; text-align:center; color:#FFFFFF; font-family:arial;font-weight:bold;'>");
                    htmlBuilder2.Append("DECLARACION JURADA - SALDO FISICO");
                    htmlBuilder2.Append("</span></td></tr></tbody></table>");
                    htmlBuilder2.Append("<table cellpadding='15' style='border: #008000 solid thin; background: #f5f5dc; width:650px;'>");
                    htmlBuilder2.Append("<tbody><tr><td><span>ESTIMADO USUARIO</span>:<br/><br/>");
                    htmlBuilder2.AppendFormat("<span style='font-size:16px;'>SE ACABA DE REALIZAR EL REGISTRO DE COMBUSTIBLES EN FECHA <strong>{0}</strong> EN EL SISTEMA OCTANO VOLUMENES, CORRESPONDIENTE A LA ESTACION DE SERVICIO <strong>{1}</strong> ", DateTime.Now, strDenominacion);
                    htmlBuilder2.AppendFormat("<br><br>LAS VENTAS REGISTRADAS LA JORNADA DE AYER <strong>{0}</strong> CORRESPONDEN A UN VOLUMEN DE:<br><br>", DateTime.Parse(strFechaSaldoFisico).AddDays(-1).ToShortDateString());
                    htmlBuilder2.Append("<ul>");

                    foreach (var item in lstProducto)
                    {
                        htmlBuilder2.AppendFormat("<li type='square'>{0} {1} DE {2}</li>", item.CANTIDAD, item.UNIDAD_MEDIDA, item.PRODUCTO);
                    }
                     
                    htmlBuilder2.Append("</ul>");
                    htmlBuilder2.AppendFormat("<span style='font-size:12px; color: red;'>ADVERTENCIA PROBLEMA EN ENVIO A : <strong>{0}</strong></span>", oDetUsuario.LOGIN);
                    htmlBuilder2.AppendFormat("<br><br><span style='font-size:12px;'>Atte.: Administrador de Sistemas HYDRO<br/>Fecha: {0}</span>", DateTime.Now);
                    htmlBuilder2.Append("</td></tr></tbody></table></span></body></html>");

                    string html2 = htmlBuilder2.ToString();

                    CCorreo.mEnviarEmail(
                                         CParametrosHydro.StrServidorDireccion,
                                         CParametrosHydro.IntServidorPuerto,
                                         CParametrosHydro.StrUsuarioLogin,
                                         CParametrosHydro.StrUsuarioPassword,
                                         CParametrosHydro.StrUsuarioDe,
                                         //oDetUsuario.LOGIN.ToLower(),
                                         CParametrosHydro.StrUsuarioCc,
                                         "",
                                         CParametrosHydro.StrUsuarioCco,
                                         "MENSAJE ANH:",
                                         html2,
                                         null,
                                         true,
                                         CParametrosHydro.bolHabilitarSsl,
                                         CParametrosHydro.bolNotificarError);
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
                    boolResultado = false;
                    #endregion
                }
            }

            return boolResultado;
        }
        private void ConfigPrecio()
        { 
            //HIDROCARBUROS
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
                cmbHidrocarburoPrecio.DataSource = objHidrocarburo.OResultado;
                cmbHidrocarburoPrecio.SelectedIndex = -1;
                cmbHidrocarburoPrecio.DataBind();
            }
            else
            {
                cmbHidrocarburoPrecio.DataSource = null;
                cmbHidrocarburoPrecio.DataBind();
            }
            //ENTIDAD
            var objEntidades = _client.Post<EResultadoConfigHidrocarburo>(CAppSettings.svc_listar_autorizacion_condicion,
                new
                {
                    strCredencial = CParametrosHydro.strCredencialFuncionario,
                    decFiltro = 3,
                    strParametro1 = tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString(),
                    strParametro2 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
                    strParametro3 = ""
                });
            if (objEntidades.IntCodigo != 1)
            {
                objEntidades.OResultado = new List<O_LISTA_AUTORIZACION_COND_CTY>();
            }
            objEntidades.OResultado.Add(new O_LISTA_AUTORIZACION_COND_CTY()
            {
                ID_AUTORIZACION_CONDICION = 0,
                ID_IDENTIFICADOR = -99999,
                NOMBRE = "No Identificado",
                ID_SUB_IDENTIFICADOR = -99999,
                SUBID_NOMBRE = "No Identificado",
                DESCRIPCION = "NOID"
            });
            cmbEntidadImportadoras.DataSource = objEntidades.OResultado;
            cmbEntidadImportadoras.SelectedIndex = -1;
            cmbEntidadImportadoras.DataBind();           

            detFechaDesde.Date = DateTime.Now; //DateTime.Parse("01/" + DateTime.Now.Month + "/" + DateTime.Now.Year);
            txtDispenser.Text = "";
            txtManguera.Text = "";
            txtPrecio.Value = "0";
            txtVolumen.Value = "0";
            rblEstado.SelectedIndex = 0;
            txtObsPrecioImp.Text = "";
            cmbEntidadImportadoras.Focus();    
        }
        public static class CFechas
{
    public static decimal ConvierteDateTimeLong(DateTime fecha)
    {
        return decimal.Parse(fecha.ToString("yyyyMMddHHmm"));
    }
    
    public static DateTime ConvierteLongDateTime(decimal fechaLong)
    {
        string fechaStr = fechaLong.ToString().PadRight(12, '0');
        int año = int.Parse(fechaStr.Substring(0, 4));
        int mes = int.Parse(fechaStr.Substring(4, 2));
        int dia = int.Parse(fechaStr.Substring(6, 2));
        int hora = int.Parse(fechaStr.Substring(8, 2));
        int min = int.Parse(fechaStr.Substring(10, 2));
        
        return new DateTime(año, mes, dia, hora, min, 0);
    }
}
        private List<O_PRECIO_IMP_BSA_CTY> ListaPrecios
        {
            get
            {
                if (Session["PRECIOS_IMPORTADOS"] == null)
                    Session["PRECIOS_IMPORTADOS"] = new List<O_PRECIO_IMP_BSA_CTY>();
                return (List<O_PRECIO_IMP_BSA_CTY>)Session["PRECIOS_IMPORTADOS"];
            }
            set
            {
                Session["PRECIOS_IMPORTADOS"] = value;
            }
        }

        private EPResultado RegistrarPrecio()
        {
            var registrar = new EObjetoPrecioImportacion
            {
                decIdEESS = 0,
                decIdEntidad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdProyecto")),
                decIdProducto = Convert.ToDecimal(cmbHidrocarburoPrecio.Value),
                decVolumen = Convert.ToDouble(txtVolumen.Value),
                decPrecio = Convert.ToDouble(txtPrecio.Value),
                strImportadora = cmbEntidadImportadoras.Text,
                strDispenser = txtDispenser.Text.ToUpper(),
                strManguera = txtManguera.Text.ToUpper(),
                decFechaDesde = Convert.ToDecimal(detFechaDesde.Date.Year + detFechaDesde.Date.Month.ToString().PadLeft(2, '0') + detFechaDesde.Date.Day.ToString().PadLeft(2, '0') + detFechaDesde.Date.Hour.ToString().PadLeft(2, '0') + detFechaDesde.Date.Minute.ToString().PadLeft(2, '0') + detFechaDesde.Date.Second.ToString().PadLeft(2, '0')),
                decFechahasta = detFechaHasta.Value == null ?
                                Convert.ToDecimal(Convert.ToDateTime("01/01/1900").Year + Convert.ToDateTime("01/01/1900").Month.ToString().PadLeft(2, '0') + Convert.ToDateTime("01/01/1900").Day.ToString().PadLeft(2, '0') + "235959")
                                : Convert.ToDecimal(detFechaHasta.Date.Year + detFechaHasta.Date.Month.ToString().PadLeft(2, '0') + detFechaHasta.Date.Day.ToString().PadLeft(2, '0') + "235959"), //CFechas.ConvierteDateTimeLong(detFechaHasta.Date),
                strObs = txtObsPrecioImp.Text.ToUpper().Trim(),
                decEstado = Convert.ToDecimal(rblEstado.Value)
            };
            var resultado = _gestion.RegistraPrecioImpBSA(
                CParametrosHydro.strCredencialFuncionario,
                registrar.decIdEESS,
                registrar.decIdEntidad,
                registrar.decIdProducto,
                registrar.decVolumen,
                registrar.decPrecio,
                registrar.strImportadora,
                registrar.strDispenser,
                registrar.strManguera,
                registrar.decFechaDesde,
                registrar.decFechahasta,
                registrar.strObs,
                registrar.decEstado
            );

            return resultado;
        }

        private EPResultado EditarPrecio(decimal decIdPrecioImp, decimal decIdEntidad)
        {
            var registrar = new EObjetoPrecioImportacion
            {
                decIdEESS = 0,
                decIdEntidad = decIdEntidad,//Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdProyecto")),
                decIdProducto = Convert.ToDecimal(cmbHidrocarburoPrecio.Value),
                decVolumen = Convert.ToDouble(txtVolumen.Value),
                decPrecio = Convert.ToDouble(txtPrecio.Value),
                strImportadora = cmbEntidadImportadoras.Text,
                strDispenser = txtDispenser.Text.ToUpper(),
                strManguera = txtManguera.Text.ToUpper(),
                decFechaDesde = Convert.ToDecimal(detFechaDesde.Date.Year + detFechaDesde.Date.Month.ToString().PadLeft(2, '0') + detFechaDesde.Date.Day.ToString().PadLeft(2, '0') + detFechaDesde.Date.Hour.ToString().PadLeft(2, '0') + detFechaDesde.Date.Minute.ToString().PadLeft(2, '0') + detFechaDesde.Date.Second.ToString().PadLeft(2, '0')),
                decFechahasta = detFechaHasta.Value == null ?
                                Convert.ToDecimal(Convert.ToDateTime("01/01/1900").Year + Convert.ToDateTime("01/01/1900").Month.ToString().PadLeft(2, '0') + Convert.ToDateTime("01/01/1900").Day.ToString().PadLeft(2, '0') + "235959")
                                : Convert.ToDecimal(detFechaHasta.Date.Year + detFechaHasta.Date.Month.ToString().PadLeft(2, '0') + detFechaHasta.Date.Day.ToString().PadLeft(2, '0') + "235959"), //CFechas.ConvierteDateTimeLong(detFechaHasta.Date),
                strObs = txtObsPrecioImp.Text.ToUpper().Trim(),
                decEstado = Convert.ToDecimal(rblEstado.Value)
            };
            var resultado = _gestion.ActualizaPrecioImpBSA(
                CParametrosHydro.strCredencialFuncionario,
                decIdPrecioImp,
                registrar.decIdEESS,
                registrar.decIdEntidad,
                registrar.decIdProducto,
                registrar.decVolumen,
                registrar.decPrecio,
                registrar.strImportadora,
                registrar.strDispenser,
                registrar.strManguera,
                registrar.decFechaDesde,
                registrar.decFechahasta,
                registrar.strObs,
                registrar.decEstado
            );

            return resultado;
        }

        private EPResultado EliminarPrecio(decimal decIdPrecioImp)
        {
            var resultado = _gestion.EliminarPrecioImpBSA(CParametrosHydro.strCredencialFuncionario, decIdPrecioImp);
            return resultado;
        }

 

    //protected void grdPrecios_DataBinding(object sender, EventArgs e)
    //{
    //    grdPrecios.DataSource = ListaPrecios;
    //}

    //protected void grdPrecios_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    //{
    //    if (e.RowType != GridViewRowType.Data) return;
        
    //    // Resaltar filas inactivas
    //    decimal estado = Convert.ToDecimal(e.GetValue("ESTADO"));
    //    if (estado == 0)
    //    {
    //        e.Row.BackColor = System.Drawing.Color.LightGray;
    //    }
    //}

    protected void cmbEntidadImportadoras_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        // Implementar búsqueda de importadoras
        // Ejemplo:
        // var importadoras = _gestion.BuscarImportadoras(e.Filter);
        // cmbEntidadImportadoras.DataSource = importadoras;
        // cmbEntidadImportadoras.DataBind();
    }

    protected void cmbEntidadImportadoras_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
    {
        // Implementar carga por valor específico
        // if (e.Value != null) ...
    }

    protected void cmbHidrocarburoPrecio_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        // Implementar búsqueda de productos
    }

    protected void cmbHidrocarburoPrecio_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
    {
        // Implementar carga por valor específico
    }
    protected void pucRegistroPrecio_WindowCallback(object source, PopupWindowCallbackArgs e)
    {
        try
        {
            var p = e.Parameter.Split('|');
            var accion = p[0];

            if (Page.IsCallback && accion == "" || accion == "NUEVO")
            {
                ConfigPrecio();
            }
            else
            {
                EPResultado Oresultado = new EPResultado();
                switch (accion)
                {
                    case "REGISTRAR":
                        Oresultado = RegistrarPrecio();
                        if (Oresultado.intCodigo > 0)
                            {
                                pucRegistroPrecio.JSProperties["cpResult"] = Oresultado.oResultado; //"OK";
                            }
                            else
                            {
                               pucRegistroPrecio.JSProperties["cpResult"] = "ERROR NO ESPERADO CONSULTE CON EL ADMINISTRADOR.";
                            } 
                        break;

                    case "EDITAR":
                        if (p.Length > 1)
                        {
                            decimal decIdEntidad, decIdPrecioImp = 0;
                            decIdPrecioImp = Convert.ToDecimal(p[1]);
                            decIdEntidad = Convert.ToDecimal(p[2]);

                            Oresultado = EditarPrecio(decIdPrecioImp, decIdEntidad);
                            if (Oresultado.intCodigo > 0)
                            {
                                pucRegistroPrecio.JSProperties["cpResult"] = Oresultado.oResultado;
                            }
                            else
                            {
                                pucRegistroPrecio.JSProperties["cpResult"] = "ERROR NO ESPERADO CONSULTE CON EL ADMINISTRADOR.";
                            }  
                        }
                        break;

                    case "ELIMINAR":
                        if (p.Length > 1)
                        {
                            decimal decIdPrecioImp = Convert.ToDecimal(p[1]);
                            Oresultado = EliminarPrecio(decIdPrecioImp);
                            if (Oresultado.intCodigo > 0)
                            {
                                pucRegistroPrecio.JSProperties["cpResult"] = Oresultado.oResultado;
                            }
                            else
                            {
                                pucRegistroPrecio.JSProperties["cpResult"] = "ERROR NO ESPERADO CONSULTE CON EL ADMINISTRADOR.";
                            }                                
                        }
                        break;
                }
            }

            var lsListadoPrecio = _listado.ListadoPreciosImportados(Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdProyecto")));
            if (lsListadoPrecio.intCodigo > 0)
            {
           
                var aux = lsListadoPrecio.oResultado as List<O_PRECIO_IMP_BSA_CTY>;
                       
                //grdPrecios.DataSource = aux;
                //grdPrecios.DataBind();
                dgvListadoPreciosImp.DataSource = lsListadoPrecio.oResultado as List<O_PRECIO_IMP_BSA_CTY>;
                dgvListadoPreciosImp.DataBind();
              
            }
            else
            {
                dgvListadoPreciosImp.DataSource = null;
                dgvListadoPreciosImp.DataBind();
            }       
        }
        catch (Exception exp)
        {
            #region RegistraError
            var oMensajeUsuario = new CLogTraza.MensajeUsuario
            {
                decUsuarioId = Session[CVariablesSesion.IdUsuario] != null ? Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]) : 0,
                decIdModulo = CParametrosHydro.decIdAplicacion,
                strAccion = GetType().Name + "." + MethodBase.GetCurrentMethod().Name,
                strIp = HttpContext.Current.Request.UserHostAddress,
                decNivelCapa = (int)CLogTraza.CapasNivel.Presentacion
            };
            CLogTraza.Error(oMensajeUsuario, exp);
            #endregion

            //e.Result = "ERROR";
            pucRegistroPrecio.JSProperties["cpResult"] = "ERROR";
            pucRegistroPrecio.JSProperties["cpError"] = exp.Message;
        }
    }
        #endregion

        #region bloque nota de despacho

        protected void btnRegistrarNotaDespacho_Click(object sender, EventArgs e)
        {
            try
            {

                var objRegistro = new ERegistraMovimientoTransaccion();
                objRegistro.strCredencial = CParametrosHydro.strCredencialFuncionario;
                objRegistro.decFiltro = 1;
                var lstRegistros = new List<ObjMovimientoTransaccion>();

                var datFechaDoc = DateTime.Parse(deFechaSalidaNotaDesopacho.Text);
                var datFechaReg = DateTime.Now;

                int nac = 0;
                if (rbNalNotaDespacho.Checked == true) nac = 1;

                int intnac = 0;
                if (rbIntNotaDespacho.Checked == true) intnac = 1;

                if (Convert.ToDecimal(txtCantidadNalNotaDespacho.Text) > 0)
                {
                    //precio nacional
                    var objNuevoReg = new ObjMovimientoTransaccion();

                    objNuevoReg.idMovimiento = 0;
                    objNuevoReg.idMovimientoPadre = 0;
                    objNuevoReg.idTipoActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                    objNuevoReg.idTipoOperacion = 135;// TEST = 253 // PRODUCION = 135;
                    objNuevoReg.idConsumidor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString());
                    objNuevoReg.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                    objNuevoReg.fechaRegistroMovimiento = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                    objNuevoReg.fechaOperacion = Convert.ToDecimal(datFechaDoc.Year + datFechaDoc.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaDoc.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaDoc.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaDoc.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaDoc.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                    objNuevoReg.idTipoMovimientoFlujo = 20;
                    objNuevoReg.idTipoMovimiento = 19;
                    objNuevoReg.idEstadoMovimiento = 11;
                    objNuevoReg.idFuenteOrigenDato = (decimal)EnumTipoRegistro.OrigenWeb;
                    objNuevoReg.idUbicacionOperacion = 120;
                    objNuevoReg.idUnidadMedidaHidrocarburo = 68;
                    objNuevoReg.idHidrocarburo = Convert.ToDecimal(txtIdHidro.Text);
                    objNuevoReg.cantidadHidrocarburo = Convert.ToDecimal(txtCantidadNalNotaDespacho.Text);
                    objNuevoReg.identificador = txtMatriculaNotaDespacho.Text;
                    objNuevoReg.subIdentificador = txtBarcodeNotaDespacho.Text;
                    objNuevoReg.observacion = txtValorInicialNal.Text + "|" + cmbOrigenNotaDespacho.Value + "|" + cmbDestinoNotaDespacho.Value + "|" + cmbAlternoNotaDespacho.Value + "|" + nac + "|" + intnac + "|" + txtConsideracionesNotaDespacho.Text.Trim();
                    objNuevoReg.barcode = 0;

                    objNuevoReg.idTipoMedicion = -99999;
                    objNuevoReg.cantidadAguaLitros = 0;
                    objNuevoReg.idTipoMedioTransporte = -99999;
                    objNuevoReg.idMedioTransporte = -99999;

                    objNuevoReg.tipoInfraestructura = -99999;
                    objNuevoReg.idInfraestructura = -99999;

                    objNuevoReg.idUnidadEnvasesTransporte = -99999;
                    objNuevoReg.codigo = "";

                    objNuevoReg.idIntegracion = 0;
                    objNuevoReg.idUnidadTemperatura = -99999;
                    objNuevoReg.temperatura = -99999;
                    objNuevoReg.gradoApi = "";
                    objNuevoReg.idEntidad = -99999;

                    objNuevoReg.cantidadEnvasesTransporte = 0;
                    objNuevoReg.binarioControl = "00000-0000000000-0000000000-00000";
                    objNuevoReg.idTransaccionBaseExterna = "";

                    objNuevoReg.idTipoConsumidor = -99999;

                    objNuevoReg.jsonReferencia = "";
                    objNuevoReg.estadoJsonRef = -99999;
                    objNuevoReg.idCuenta = -99999;
                    objNuevoReg.idCuentaAux = -99999;
                    objNuevoReg.idParamGlobal = -99999;
                    objNuevoReg.idParamEspec = -99999;
                    objNuevoReg.idTipoInfraAux = -99999;
                    objNuevoReg.idInfraAux = -99999;

                    objNuevoReg.listaTransaccion = new List<ObjTransaccion>();

                    var objTrasaccion = new ObjTransaccion();
                    objTrasaccion.idMovimiento = 0;
                    objTrasaccion.idTransaccion = 0;
                    objTrasaccion.idTransaccionPadre = 0;
                    objTrasaccion.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                    objTrasaccion.fechaRegistroTransaccion = Convert.ToDecimal(datFechaDoc.Year + datFechaDoc.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaDoc.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaDoc.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaDoc.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaDoc.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                    objTrasaccion.idTipoTransaccion = 3;
                    objTrasaccion.fechaEmision = Convert.ToDecimal(datFechaDoc.Year + datFechaDoc.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaDoc.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaDoc.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaDoc.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaDoc.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                    objTrasaccion.numeroTransaccion = "0";
                    objTrasaccion.numeroAutorizacion = 0;
                    objTrasaccion.codigoControl = "";
                    objTrasaccion.nitConsumidor = Convert.ToDecimal(txtNitNotaDespacho.Text);
                    objTrasaccion.razonSocial = txtRazonSocialNotaDespacho.Text;
                    objTrasaccion.precioUnitario = Convert.ToDecimal(txtPrecioUnitarioNacional.Text);
                    objTrasaccion.precioTotal = Convert.ToDecimal(txtPrecioTotalNal.Text);
                    objTrasaccion.usuarioTransaccion = "";
                    objTrasaccion.observaciones = "";
                    objTrasaccion.nitEmisor = Convert.ToDecimal(txtNitOperadorNotaDespacho.Text);
                    objTrasaccion.idEstadoTransaccion = 11;
                    objTrasaccion.idTipoCreditoFiscal = 10;
                    objTrasaccion.idFormaPago = 1;
                    objTrasaccion.idTipoMoneda = 1;
                    objTrasaccion.idTipoEmisor = 14;
                    objTrasaccion.codigoSeguridad = 0;
                    objTrasaccion.idAutorizacionDato = 0;
                    objTrasaccion.binarioControl = "1000000000";
                    objTrasaccion.fechaOperacion = CFechas.ConvierteDateTimeLong(datFechaDoc);
                    objTrasaccion.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                    objTrasaccion.codigo = "";

                    objNuevoReg.listaTransaccion.Add(objTrasaccion);

                    lstRegistros.Add(objNuevoReg);
                }

                if (Convert.ToDecimal(txtCantidadInterNalNotaDespacho.Text) > 0)
                {
                    //precio internacional
                    var objNuevoReg2 = new ObjMovimientoTransaccion();

                    objNuevoReg2.idMovimiento = 0;
                    objNuevoReg2.idMovimientoPadre = 0;
                    objNuevoReg2.idTipoActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                    objNuevoReg2.idTipoOperacion = 135;// TEST = 253 // PRODUCION = 135;
                    objNuevoReg2.idConsumidor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString());
                    objNuevoReg2.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                    objNuevoReg2.fechaRegistroMovimiento = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                    objNuevoReg2.fechaOperacion = Convert.ToDecimal(datFechaDoc.Year + datFechaDoc.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaDoc.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaDoc.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaDoc.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaDoc.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                    objNuevoReg2.idTipoMovimientoFlujo = 20;
                    objNuevoReg2.idTipoMovimiento = 19;
                    objNuevoReg2.idEstadoMovimiento = 11;
                    objNuevoReg2.idFuenteOrigenDato = (decimal)EnumTipoRegistro.OrigenWeb;
                    objNuevoReg2.idUbicacionOperacion = 120;
                    objNuevoReg2.idUnidadMedidaHidrocarburo = 68;
                    objNuevoReg2.idHidrocarburo = Convert.ToDecimal(txtIdHidro.Text);
                    objNuevoReg2.cantidadHidrocarburo = Convert.ToDecimal(txtCantidadInterNalNotaDespacho.Text);
                    objNuevoReg2.identificador = txtMatriculaNotaDespacho.Text;
                    objNuevoReg2.subIdentificador = txtBarcodeNotaDespacho.Text;
                    objNuevoReg2.observacion = txtValorInicialNal.Text + "|" + cmbOrigenNotaDespacho.Value + "|" + cmbDestinoNotaDespacho.Value + "|" + cmbAlternoNotaDespacho.Value + "|" + nac + "|" + intnac + "|" + txtConsideracionesNotaDespacho.Text.Trim();
                    objNuevoReg2.barcode = 0;

                    objNuevoReg2.idTipoMedicion = -99999;
                    objNuevoReg2.cantidadAguaLitros = 0;
                    objNuevoReg2.idTipoMedioTransporte = -99999;
                    objNuevoReg2.idMedioTransporte = -99999;

                    objNuevoReg2.tipoInfraestructura = -99999;
                    objNuevoReg2.idInfraestructura = -99999;

                    objNuevoReg2.idUnidadEnvasesTransporte = -99999;
                    objNuevoReg2.codigo = "";

                    objNuevoReg2.idIntegracion = 0;
                    objNuevoReg2.idUnidadTemperatura = -99999;
                    objNuevoReg2.temperatura = -99999;
                    objNuevoReg2.gradoApi = "";
                    objNuevoReg2.idEntidad = -99999;

                    objNuevoReg2.cantidadEnvasesTransporte = 0;
                    objNuevoReg2.binarioControl = "00000-0000000000-0000000000-00000";
                    objNuevoReg2.idTransaccionBaseExterna = "";

                    objNuevoReg2.idTipoConsumidor = -99999;

                    objNuevoReg2.jsonReferencia = "";
                    objNuevoReg2.estadoJsonRef = -99999;
                    objNuevoReg2.idCuenta = -99999;
                    objNuevoReg2.idCuentaAux = -99999;
                    objNuevoReg2.idParamGlobal = -99999;
                    objNuevoReg2.idParamEspec = -99999;
                    objNuevoReg2.idTipoInfraAux = -99999;
                    objNuevoReg2.idInfraAux = -99999;

                    objNuevoReg2.listaTransaccion = new List<ObjTransaccion>();

                    var objTrasaccion2 = new ObjTransaccion();
                    objTrasaccion2.idMovimiento = 0;
                    objTrasaccion2.idTransaccion = 0;
                    objTrasaccion2.idTransaccionPadre = 0;
                    objTrasaccion2.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                    objTrasaccion2.fechaRegistroTransaccion = Convert.ToDecimal(datFechaDoc.Year + datFechaDoc.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaDoc.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaDoc.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaDoc.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaDoc.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                    objTrasaccion2.idTipoTransaccion = 3;
                    objTrasaccion2.fechaEmision = Convert.ToDecimal(datFechaDoc.Year + datFechaDoc.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaDoc.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaDoc.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaDoc.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaDoc.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                    objTrasaccion2.numeroTransaccion = "0";
                    objTrasaccion2.numeroAutorizacion = 0;
                    objTrasaccion2.codigoControl = "";
                    objTrasaccion2.nitConsumidor = Convert.ToDecimal(txtNitNotaDespacho.Text);
                    objTrasaccion2.razonSocial = txtRazonSocialNotaDespacho.Text;
                    objTrasaccion2.precioUnitario = Convert.ToDecimal(txtPrecioUnitarioInternacional.Text);
                    objTrasaccion2.precioTotal = Convert.ToDecimal(txtPrecioTotalInterNal.Text);
                    objTrasaccion2.usuarioTransaccion = "";
                    objTrasaccion2.observaciones = "";
                    objTrasaccion2.nitEmisor = Convert.ToDecimal(txtNitOperadorNotaDespacho.Text);
                    objTrasaccion2.idEstadoTransaccion = 11;
                    objTrasaccion2.idTipoCreditoFiscal = 10;
                    objTrasaccion2.idFormaPago = 1;
                    objTrasaccion2.idTipoMoneda = 1;
                    objTrasaccion2.idTipoEmisor = 14;
                    objTrasaccion2.codigoSeguridad = 0;
                    objTrasaccion2.idAutorizacionDato = 0;
                    objTrasaccion2.binarioControl = "0000000000";
                    objTrasaccion2.fechaOperacion = CFechas.ConvierteDateTimeLong(datFechaDoc);
                    objTrasaccion2.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                    objTrasaccion2.codigo = "";

                    objNuevoReg2.listaTransaccion.Add(objTrasaccion2);
                    lstRegistros.Add(objNuevoReg2);
                }

                if (lstRegistros.Count() > 0)
                {
                    objRegistro.listaMovimientoTransaccion = lstRegistros;

                    //var ojo = JsonSerializer.SerializeToString(objRegistro);
                    var resultado = _client.Post<EResultado>(CAppSettings.svc_registra_movimiento_transaccion, objRegistro);
                    if (resultado.intCodigo == 1)
                    {
                        //e.Cancel = true;
                        //Session.Remove(CVariablesSesion.AuxActualiza);
                        //dgvTransacciones.CancelEdit();
                        //dgvTransacciones_CustomCallback(null, null);                        
                        ImprimirFormulario(nac, intnac);

                    }
                    else
                    {
                        MImprimirMensaje(resultado.strMensaje, EnumTiposMensajeAlerta.Alerta);
                    }
                }
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

        private void ImprimirFormulario(int nac, int intnac)
        {
            Session[CVariablesSesion.TipoReporte] = EnumTipoReporte.repNotaDespachoAviacion;
            var xreIFormNota = new repNotaDespachoAviacion();

            xreIFormNota.strNumerodocumento.Value = txtDocumentoNotaDespacho.Text;
            xreIFormNota.strFechaSuministro.Value = txtFechaSuministroNotaDespacho.Text;
            xreIFormNota.strOperador.Value = txtOperadorNotaDespacho.Text;
            xreIFormNota.strNitEmisor.Value = txtNitOperadorNotaDespacho.Text;
            xreIFormNota.strPlanta.Value = txtPlantaNotaDespacho.Text;

            xreIFormNota.strMatricula.Value = txtMatriculaNotaDespacho.Text;

            if (nac == 1)
            {
                xreIFormNota.strNac.Value = "X";
            }
            else
            {
                xreIFormNota.strNac.Value = "";
            }

            if (intnac == 1)
            {
                xreIFormNota.strInt.Value = "X";
            }
            else
            {
                xreIFormNota.strInt.Value = "";
            }

            xreIFormNota.strTarjeta.Value = txtBarcodeNotaDespacho.Text;
            xreIFormNota.strModelo.Value = txtModeloNotaDespacho.Text;
            xreIFormNota.strNitConsumidor.Value = txtNitNotaDespacho.Text;
            xreIFormNota.strRazonSocial.Value = txtRazonSocialNotaDespacho.Text;
            xreIFormNota.strEncargado.Value = txtEncargadoNotaDespacho.Text;

            xreIFormNota.strRefueller.Value = txtRefuellerNotaDespachoI.Text;
            xreIFormNota.strInicialNal.Value = txtValorInicialNal.Text;
            xreIFormNota.strInicialInter.Value = txtValorInicialInterNal.Text;
            xreIFormNota.strFinalNal.Value = txtValorFinalNal.Text;
            xreIFormNota.strFinalInter.Value = txtValorFinalInterNal.Text;
            xreIFormNota.strProducto.Value = txtProductoNacionalDespacho.Text;
            xreIFormNota.strCantidadNal.Value = txtCantidadNalNotaDespacho.Text;
            xreIFormNota.strCantidadInter.Value = txtCantidadInterNalNotaDespacho.Text;
            xreIFormNota.strPrecioUnitarioNal.Value = txtPrecioUnitarioNacional.Text;
            xreIFormNota.strPrecioUnitarioInter.Value = txtPrecioUnitarioInternacional.Text;
            xreIFormNota.strPrecioNal.Value = txtPrecioTotalNal.Text;
            xreIFormNota.strPrecioInter.Value = txtPrecioTotalInterNal.Text;

            xreIFormNota.strAeropuertoOrigen.Value = cmbOrigenNotaDespacho.Value;
            xreIFormNota.strAeropuertoDestino.Value = cmbDestinoNotaDespacho.Value;
            xreIFormNota.strAeropuertoAlterno.Value = cmbAlternoNotaDespacho.Value;
            xreIFormNota.strAeropuertoSalida.Value = deFechaSalidaNotaDesopacho.Text;

            xreIFormNota.strConsideraciones.Value = txtConsideracionesNotaDespacho.Text;

            Session[CVariablesSesion.ReportePrincipal] = xreIFormNota;

            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( 'WFReporte.aspx', '')", true);
        }

        #endregion

        #region bloque importadores

        //lmqc importadores

        protected void pnlFacturasImpEdt_Callback(object sender, CallbackEventArgsBase e)//lmqc
        {
            int intIndice = Convert.ToInt32(cmbTipoFacturaImpEdt.Value);

            CargarControlesFacturasImportacion(intIndice, "");
        }

        protected void pnlFacturasImpAdd_Callback(object sender, CallbackEventArgsBase e)//lmqc
        {

            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    int intIndice = Convert.ToInt32(cmbTipoFacturaImpAdd.Value);
                    string strProductoSeleccionado = txtProductoSeleccionado.Text;
                    int intTipoInsumo = tipoInsumoAditivo(strProductoSeleccionado);

                    switch (intIndice)
                    {
                        case 1:
                            pnlFacturasCostoProducto.Visible = true;

                            pnlFacturasCostoInspeccion.Visible = true;
                            pnlFacturasCostoMonitoreo.Visible = true;

                            pnlFacturasCostoTransporte.Visible = false;
                            pnlFacturascostoAlmacenaje.Visible = false;

                            pnlFacturasCostoAduanero.Visible = false;

                            pnlFacturasCostoSeguro.Visible = false;

                            switch (intTipoInsumo)
                            {
                                case 1:
                                case 3:
                                    //producto
                                    lblFacProducto.ClientVisible = true;
                                    txtFacProducto.ClientVisible = true;
                                    lblRazonSocialFacProducto.ClientVisible = true;
                                    txtRazonSocialFacProducto.ClientVisible = true;
                                    lblNitFacProducto.ClientVisible = true;
                                    txtNitFacProducto.ClientVisible = true;
                                    lblTipoCambio.ClientVisible = false;
                                    txtTipoCambio.Visible = false;
                                    lblSecFacProducto.ClientVisible = true;
                                    txtSecFacProducto.ClientVisible = true;
                                    lblPreUnitarioFacProducto.ClientVisible = true;
                                    txtPreUnitarioFacProducto.Visible = true;
                                    lblVolRecibidoFacProducto.ClientVisible = true;
                                    txtVolrecibidoFacProducto.Visible = true;
                                    lblPreTotalFacProducto.ClientVisible = true;
                                    txtPreTotalFacProducto.Visible = true;
                                    lblPreUnitarioSusFacProducto.ClientVisible = false;
                                    txtPreUnitarioSusFacProducto.ClientVisible = false;
                                    lblObsFacProducto.ClientVisible = true;
                                    txtObsFacProducto.ClientVisible = true;

                                    //inspeccion
                                    lblFacInOrigen.ClientVisible = true;
                                    txtFacInOrigen.ClientVisible = true;
                                    lblEmpresaFacInOrigen.ClientVisible = true;
                                    txtEmpresaFacInOrigen.ClientVisible = true;
                                    lblMonFacInOrigen.ClientVisible = true;
                                    txtMonFacInOrigen.Visible = true;
                                    lblMonSusFacInOrigen.ClientVisible = false;
                                    txtMonSusFacInOrigen.ClientVisible = false;
                                    lblSecFacInOrigen.ClientVisible = true;
                                    txtSecFacInOrigen.ClientVisible = true;
                                    lblObsFacInOrigen.ClientVisible = true;
                                    txtObsFacInOrigen.ClientVisible = true;
                                    lblVolFactInOrigen.ClientVisible = true;
                                    txtVolFactInOrigen.Visible = true;

                                    lblFacInIn.ClientVisible = true;
                                    txtFacInIn.ClientVisible = true;
                                    lblEmpresaFacInIn.ClientVisible = true;
                                    txtEmpresaFacInIn.ClientVisible = true;
                                    lblMontoFacInIn.ClientVisible = true;
                                    txtMontoFacInIn.Visible = true;
                                    lblMontoSusFacInIn.ClientVisible = false;
                                    txtMontoSusFacInIn.ClientVisible = false;
                                    lblSecFacInIn.ClientVisible = true;
                                    txtSecFacInIn.ClientVisible = true;
                                    lblObsFacInIn.ClientVisible = true;
                                    txtObsFacInIn.ClientVisible = true;
                                    lblVolFactInIn.ClientVisible = true;
                                    txtVolFactInIn.Visible = true;

                                    lblFacInFin.ClientVisible = true;
                                    txtFacInFin.ClientVisible = true;
                                    lblEmpresaFacInFin.ClientVisible = true;
                                    txtEmpresaFacInFin.ClientVisible = true;
                                    lblMontoFacInFin.ClientVisible = true;
                                    txtMontoFacInFin.Visible = true;
                                    lblMontoSusFacInFin.ClientVisible = false;
                                    txtMontoSusFacInFin.ClientVisible = false;
                                    lblSecFacInFin.ClientVisible = true;
                                    txtSecFacInFin.ClientVisible = true;
                                    lblObsFacInFin.ClientVisible = true;
                                    txtObsFacInFin.ClientVisible = true;
                                    lblVolFactInFin.ClientVisible = true;
                                    txtVolFactInFin.Visible = true;

                                    //monitoreo
                                    lblFacMon.ClientVisible = true;
                                    txtFacMon.ClientVisible = true;
                                    lblEmpresaFacMon.ClientVisible = true;
                                    txtEmpresaFacMon.ClientVisible = true;
                                    lblMontoFacMon.ClientVisible = true;
                                    txtMontoFacMon.Visible = true;
                                    lblMontoSusFacMon.ClientVisible = false;
                                    txtMontoSusFacMon.ClientVisible = false;
                                    lblSecFacMon.ClientVisible = true;
                                    txtSecFacMon.ClientVisible = true;
                                    lblObsFacMon.ClientVisible = true;
                                    txtObsFacMon.ClientVisible = true;
                                    lblVolReMon.ClientVisible = true;
                                    txtVolReMon.Visible = true;
                                    break;
                                case 2:
                                    //producto
                                    lblFacProducto.ClientVisible = false;
                                    txtFacProducto.ClientVisible = false;
                                    lblRazonSocialFacProducto.ClientVisible = false;
                                    txtRazonSocialFacProducto.ClientVisible = false;
                                    lblNitFacProducto.ClientVisible = false;
                                    txtNitFacProducto.ClientVisible = false;
                                    lblTipoCambio.ClientVisible = false;
                                    txtTipoCambio.Visible = false;
                                    lblSecFacProducto.ClientVisible = true;
                                    txtSecFacProducto.ClientVisible = true;
                                    lblPreUnitarioFacProducto.ClientVisible = true;
                                    txtPreUnitarioFacProducto.Visible = true;
                                    lblVolRecibidoFacProducto.ClientVisible = true;
                                    txtVolrecibidoFacProducto.Visible = true;
                                    lblPreTotalFacProducto.ClientVisible = true;
                                    txtPreTotalFacProducto.Visible = true;
                                    lblPreUnitarioSusFacProducto.ClientVisible = false;
                                    txtPreUnitarioSusFacProducto.ClientVisible = false;
                                    lblObsFacProducto.ClientVisible = true;
                                    txtObsFacProducto.ClientVisible = true;

                                    //inspeccion
                                    lblFacInOrigen.ClientVisible = true;
                                    txtFacInOrigen.ClientVisible = true;
                                    lblEmpresaFacInOrigen.ClientVisible = true;
                                    txtEmpresaFacInOrigen.ClientVisible = true;
                                    lblMonFacInOrigen.ClientVisible = true;
                                    txtMonFacInOrigen.Visible = true;
                                    lblMonSusFacInOrigen.ClientVisible = false;
                                    txtMonSusFacInOrigen.ClientVisible = false;
                                    lblSecFacInOrigen.ClientVisible = true;
                                    txtSecFacInOrigen.ClientVisible = true;
                                    lblObsFacInOrigen.ClientVisible = true;
                                    txtObsFacInOrigen.ClientVisible = true;
                                    lblVolFactInOrigen.ClientVisible = true;
                                    txtVolFactInOrigen.Visible = true;

                                    lblFacInIn.ClientVisible = true;
                                    txtFacInIn.ClientVisible = true;
                                    lblEmpresaFacInIn.ClientVisible = true;
                                    txtEmpresaFacInIn.ClientVisible = true;
                                    lblMontoFacInIn.ClientVisible = true;
                                    txtMontoFacInIn.Visible = true;
                                    lblMontoSusFacInIn.ClientVisible = false;
                                    txtMontoSusFacInIn.ClientVisible = false;
                                    lblSecFacInIn.ClientVisible = true;
                                    txtSecFacInIn.ClientVisible = true;
                                    lblObsFacInIn.ClientVisible = true;
                                    txtObsFacInIn.ClientVisible = true;
                                    lblVolFactInIn.ClientVisible = true;
                                    txtVolFactInIn.Visible = true;

                                    lblFacInFin.ClientVisible = true;
                                    txtFacInFin.ClientVisible = true;
                                    lblEmpresaFacInFin.ClientVisible = true;
                                    txtEmpresaFacInFin.ClientVisible = true;
                                    lblMontoFacInFin.ClientVisible = true;
                                    txtMontoFacInFin.Visible = true;
                                    lblMontoSusFacInFin.ClientVisible = false;
                                    txtMontoSusFacInFin.ClientVisible = false;
                                    lblSecFacInFin.ClientVisible = true;
                                    txtSecFacInFin.ClientVisible = true;
                                    lblObsFacInFin.ClientVisible = true;
                                    txtObsFacInFin.ClientVisible = true;
                                    lblVolFactInFin.ClientVisible = true;
                                    txtVolFactInFin.Visible = true;

                                    //monitoreo
                                    lblFacMon.ClientVisible = true;
                                    txtFacMon.ClientVisible = true;
                                    lblEmpresaFacMon.ClientVisible = true;
                                    txtEmpresaFacMon.ClientVisible = true;
                                    lblMontoFacMon.ClientVisible = true;
                                    txtMontoFacMon.Visible = true;
                                    lblMontoSusFacMon.ClientVisible = false;
                                    txtMontoSusFacMon.ClientVisible = false;
                                    lblSecFacMon.ClientVisible = true;
                                    txtSecFacMon.ClientVisible = true;
                                    lblObsFacMon.ClientVisible = true;
                                    txtObsFacMon.ClientVisible = true;
                                    lblVolReMon.ClientVisible = true;
                                    txtVolReMon.Visible = true;
                                    break;
                            }

                            btnActualizarDocumentoImpAdd.Enabled = true;

                            pnlFacturasGasolinaBase.Visible = false;

                            pnlFacturasTransMasivo.Visible = false;
                            break;
                        case 2:

                            pnlFacturasCostoProducto.Visible = true;



                            pnlFacturasCostoInspeccion.Visible = false;
                            pnlFacturasCostoMonitoreo.Visible = false;

                            pnlFacturasCostoTransporte.Visible = true;
                            pnlFacturascostoAlmacenaje.Visible = true;

                            pnlFacturasCostoAduanero.Visible = false;

                            pnlFacturasCostoSeguro.Visible = false;

                            switch (intTipoInsumo)
                            {
                                case 1:
                                case 3:
                                    //producto
                                    lblFacProducto.ClientVisible = true;
                                    txtFacProducto.ClientVisible = true;
                                    lblRazonSocialFacProducto.ClientVisible = true;
                                    txtRazonSocialFacProducto.ClientVisible = true;
                                    lblNitFacProducto.ClientVisible = false;
                                    txtNitFacProducto.ClientVisible = false;
                                    lblTipoCambio.ClientVisible = false;
                                    txtTipoCambio.Visible = false;
                                    lblSecFacProducto.ClientVisible = false;
                                    txtSecFacProducto.ClientVisible = false;
                                    lblPreUnitarioFacProducto.ClientVisible = false;
                                    txtPreUnitarioFacProducto.Visible = false;
                                    lblVolRecibidoFacProducto.ClientVisible = false;
                                    txtVolrecibidoFacProducto.Visible = false;
                                    lblPreTotalFacProducto.ClientVisible = false;
                                    txtPreTotalFacProducto.Visible = false;
                                    lblPreUnitarioSusFacProducto.ClientVisible = false;
                                    txtPreUnitarioSusFacProducto.ClientVisible = false;
                                    lblObsFacProducto.ClientVisible = false;
                                    txtObsFacProducto.ClientVisible = false;

                                    //transporte
                                    lblEmpresaFacTransFlu.ClientVisible = true;
                                    txtEmpresaFacTransFlu.ClientVisible = true;
                                    lblFacTransFlu.ClientVisible = true;
                                    txtFacTransFlu.ClientVisible = true;
                                    lblCostoUniFacTransFlu.ClientVisible = true;
                                    txtCostoUniFacTransFlu.Visible = true;
                                    lblFleteFacTransFlu.ClientVisible = true;
                                    txtFleteFacTransFlu.Visible = true;
                                    lblSecFacTransFlu.ClientVisible = true;
                                    txtSecFacTransFlu.ClientVisible = true;
                                    lblPlantaDespachoFacTransFlu.ClientVisible = true;
                                    txtPlantaDespachoFacTransFlu.ClientVisible = true;
                                    lblVolDespachadoFacTransFlu.ClientVisible = true;
                                    txtVolDespachadoFacTransFlu.Visible = true;
                                    lblPlantaRecepcionFacTransFlu.ClientVisible = true;
                                    txtPlantaRecepcionFacTransFlu.ClientVisible = true;
                                    lblVolRecibidoFacTransFlu.ClientVisible = true;
                                    txtVolRecibidoFacTransFlu.Visible = true;
                                    lblObsFacTransFlu.ClientVisible = true;
                                    txtObsFacTransFlu.ClientVisible = true;

                                    lblEmpresaFacTransTe.ClientVisible = true;
                                    txtEmpresaFacTransTe.ClientVisible = true;
                                    lblFacTransTe.ClientVisible = true;
                                    txtFacTransTe.ClientVisible = true;
                                    lblCostoUniFacTransTe.ClientVisible = true;
                                    txtCostoUniFacTransTe.Visible = true;
                                    lblFleteFacTransTe.ClientVisible = true;
                                    txtFleteFacTransTe.Visible = true;
                                    lblSecFacTransTe.ClientVisible = true;
                                    txtSecFacTransTe.ClientVisible = true;
                                    lblPlantaDespachoFacTransTe.ClientVisible = true;
                                    txtPlantaDespachoFacTransTe.ClientVisible = true;
                                    lblVolDespachadoFacTransTe.ClientVisible = true;
                                    txtVolDespachadoFacTransTe.Visible = true;
                                    lblPlantaRecepcionFacTransTe.ClientVisible = true;
                                    txtPlantaRecepcionFacTransTe.ClientVisible = true;
                                    lblVolRecibidoFacTransTe.ClientVisible = true;
                                    txtVolRecibidoFacTransTe.Visible = true;
                                    lblObsFacTransTe.ClientVisible = true;
                                    txtObsFacTransTe.ClientVisible = true;

                                    //transporte ducto gasolina blanca
                                    lblTituloTransTeDuGb.ClientVisible = false;
                                    lblVolumenFacTransTeDuGb.ClientVisible = false;
                                    lblPreUniFacTransTeDuGb.ClientVisible = false;
                                    lblSecFacTransTeGb.ClientVisible = false;
                                    txtVolFacTransDu.Visible = false;
                                    txtPreUniFacTransDu.Visible = false;
                                    txtSecFacTransTeGb.ClientVisible = false;

                                    lblEmpresaFacTransFe.ClientVisible = true;
                                    txtEmpresaFacTransFe.ClientVisible = true;
                                    lblFacTransFe.ClientVisible = true;
                                    txtFacTransFe.ClientVisible = true;
                                    lblCostoUniFacTransFe.ClientVisible = true;
                                    txtCostoUniFacTransFe.Visible = true;
                                    lblFleteFacTransFe.ClientVisible = true;
                                    txtFleteFacTransFe.Visible = true;
                                    lblSecFacTransFe.ClientVisible = true;
                                    txtSecFacTransFe.ClientVisible = true;
                                    lblVolDespachadoFacTransFe.ClientVisible = true;
                                    txtVolDespachadoFacTransFe.Visible = true;
                                    lblVolRecibidoFacTransFe.ClientVisible = true;
                                    txtVolRecibidoFacTransFe.Visible = true;
                                    lblObsFacTransFe.ClientVisible = true;
                                    txtObsFacTransFe.ClientVisible = true;

                                    //almacenaje
                                    lblFacAlmacenajeIntermedio.ClientVisible = true;
                                    txtFacAlmacenajeIntermedio.ClientVisible = true;
                                    lblEmpresaFacAlmacenajeIntermedio.ClientVisible = true;
                                    txtEmpresaFacAlmacenajeIntermedio.ClientVisible = true;
                                    lblCostoUniFacAlmacenajeIntermedio.ClientVisible = true;
                                    txtCostoUniFacAlmacenajeIntermedio.Visible = true;
                                    lblSecFacAlmacenajeIntermedio.ClientVisible = true;
                                    txtSecFacAlmacenajeIntermedio.ClientVisible = true;
                                    lblVolRecibidoFacAlmacenajeIntermedio.ClientVisible = true;
                                    txtVolRecibidoFacAlmacenajeIntermedio.Visible = true;
                                    lblObsFacAlmacenajeIntermedio.ClientVisible = true;
                                    txtObsFacAlmacenajeIntermedio.ClientVisible = true;

                                    lblFacAlmacenajeFinal.ClientVisible = true;
                                    txtFacAlmacenajeFinal.ClientVisible = true;
                                    lblEmpresaAlmacenajeFinal.ClientVisible = true;
                                    txtEmpresaAlmacenajeFinal.ClientVisible = true;
                                    lblCostoUniFacAlmacenajeFinal.ClientVisible = true;
                                    txtCostoUniFacAlmacenajeFinal.Visible = true;
                                    lblSecFacAlmacenajeFinal.ClientVisible = true;
                                    txtSecFacAlmacenajeFinal.ClientVisible = true;
                                    lblVolRecibidoFacAlmacenajeFinal.ClientVisible = true;
                                    txtVolRecibidoFacAlmacenajeFinal.Visible = true;
                                    lblObsFacAlmacenajeFinal.ClientVisible = true;
                                    txtObsFacAlmacenajeFinal.ClientVisible = true;


                                    break;
                                case 2:
                                    //producto
                                    lblFacProducto.ClientVisible = true;
                                    txtFacProducto.ClientVisible = true;
                                    lblRazonSocialFacProducto.ClientVisible = true;
                                    txtRazonSocialFacProducto.ClientVisible = true;
                                    lblNitFacProducto.ClientVisible = false;
                                    txtNitFacProducto.ClientVisible = false;
                                    lblTipoCambio.ClientVisible = false;
                                    txtTipoCambio.Visible = false;
                                    lblSecFacProducto.ClientVisible = false;
                                    txtSecFacProducto.ClientVisible = false;
                                    lblPreUnitarioFacProducto.ClientVisible = false;
                                    txtPreUnitarioFacProducto.Visible = false;
                                    lblVolRecibidoFacProducto.ClientVisible = false;
                                    txtVolrecibidoFacProducto.Visible = false;
                                    lblPreTotalFacProducto.ClientVisible = false;
                                    txtPreTotalFacProducto.Visible = false;
                                    lblPreUnitarioSusFacProducto.ClientVisible = false;
                                    txtPreUnitarioSusFacProducto.ClientVisible = false;
                                    lblObsFacProducto.ClientVisible = false;
                                    txtObsFacProducto.ClientVisible = false;

                                    //transporte
                                    lblEmpresaFacTransFlu.ClientVisible = true;
                                    txtEmpresaFacTransFlu.ClientVisible = true;
                                    lblFacTransFlu.ClientVisible = true;
                                    txtFacTransFlu.ClientVisible = true;
                                    lblCostoUniFacTransFlu.ClientVisible = true;
                                    txtCostoUniFacTransFlu.Visible = true;
                                    lblFleteFacTransFlu.ClientVisible = true;
                                    txtFleteFacTransFlu.Visible = true;
                                    lblSecFacTransFlu.ClientVisible = true;
                                    txtSecFacTransFlu.ClientVisible = true;
                                    lblPlantaDespachoFacTransFlu.ClientVisible = true;
                                    txtPlantaDespachoFacTransFlu.ClientVisible = true;
                                    lblVolDespachadoFacTransFlu.ClientVisible = true;
                                    txtVolDespachadoFacTransFlu.Visible = true;
                                    lblPlantaRecepcionFacTransFlu.ClientVisible = true;
                                    txtPlantaRecepcionFacTransFlu.ClientVisible = true;
                                    lblVolRecibidoFacTransFlu.ClientVisible = true;
                                    txtVolRecibidoFacTransFlu.Visible = true;
                                    lblObsFacTransFlu.ClientVisible = true;
                                    txtObsFacTransFlu.ClientVisible = true;

                                    lblEmpresaFacTransTe.ClientVisible = true;
                                    txtEmpresaFacTransTe.ClientVisible = true;
                                    lblFacTransTe.ClientVisible = true;
                                    txtFacTransTe.ClientVisible = true;
                                    lblCostoUniFacTransTe.ClientVisible = true;
                                    txtCostoUniFacTransTe.Visible = true;
                                    lblFleteFacTransTe.ClientVisible = true;
                                    txtFleteFacTransTe.Visible = true;
                                    lblSecFacTransTe.ClientVisible = true;
                                    txtSecFacTransTe.ClientVisible = true;
                                    lblPlantaDespachoFacTransTe.ClientVisible = true;
                                    txtPlantaDespachoFacTransTe.ClientVisible = true;
                                    lblVolDespachadoFacTransTe.ClientVisible = true;
                                    txtVolDespachadoFacTransTe.Visible = true;
                                    lblPlantaRecepcionFacTransTe.ClientVisible = true;
                                    txtPlantaRecepcionFacTransTe.ClientVisible = true;
                                    lblVolRecibidoFacTransTe.ClientVisible = true;
                                    txtVolRecibidoFacTransTe.Visible = true;
                                    lblObsFacTransTe.ClientVisible = true;
                                    txtObsFacTransTe.ClientVisible = true;

                                    //transporte ducto gasolina blanca
                                    lblTituloTransTeDuGb.ClientVisible = true;
                                    lblVolumenFacTransTeDuGb.ClientVisible = true;
                                    lblPreUniFacTransTeDuGb.ClientVisible = true;
                                    lblSecFacTransTeGb.ClientVisible = true;
                                    txtVolFacTransDu.Visible = true;
                                    txtPreUniFacTransDu.Visible = true;
                                    txtSecFacTransTeGb.ClientVisible = true;

                                    lblEmpresaFacTransFe.ClientVisible = true;
                                    txtEmpresaFacTransFe.ClientVisible = true;
                                    lblFacTransFe.ClientVisible = true;
                                    txtFacTransFe.ClientVisible = true;
                                    lblCostoUniFacTransFe.ClientVisible = true;
                                    txtCostoUniFacTransFe.Visible = true;
                                    lblFleteFacTransFe.ClientVisible = true;
                                    txtFleteFacTransFe.Visible = true;
                                    lblSecFacTransFe.ClientVisible = true;
                                    txtSecFacTransFe.ClientVisible = true;
                                    lblVolDespachadoFacTransFe.ClientVisible = true;
                                    txtVolDespachadoFacTransFe.Visible = true;
                                    lblVolRecibidoFacTransFe.ClientVisible = true;
                                    txtVolRecibidoFacTransFe.Visible = true;
                                    lblObsFacTransFe.ClientVisible = true;
                                    txtObsFacTransFe.ClientVisible = true;

                                    //almacenaje
                                    lblFacAlmacenajeIntermedio.ClientVisible = true;
                                    txtFacAlmacenajeIntermedio.ClientVisible = true;
                                    lblEmpresaFacAlmacenajeIntermedio.ClientVisible = false;
                                    txtEmpresaFacAlmacenajeIntermedio.ClientVisible = false;
                                    lblCostoUniFacAlmacenajeIntermedio.ClientVisible = true;
                                    txtCostoUniFacAlmacenajeIntermedio.Visible = true;
                                    lblSecFacAlmacenajeIntermedio.ClientVisible = true;
                                    txtSecFacAlmacenajeIntermedio.ClientVisible = true;
                                    lblVolRecibidoFacAlmacenajeIntermedio.ClientVisible = true;
                                    txtVolRecibidoFacAlmacenajeIntermedio.Visible = true;
                                    lblObsFacAlmacenajeIntermedio.ClientVisible = true;
                                    txtObsFacAlmacenajeIntermedio.ClientVisible = true;

                                    lblFacAlmacenajeFinal.ClientVisible = true;
                                    txtFacAlmacenajeFinal.ClientVisible = true;
                                    lblEmpresaAlmacenajeFinal.ClientVisible = false;
                                    txtEmpresaAlmacenajeFinal.ClientVisible = false;
                                    lblCostoUniFacAlmacenajeFinal.ClientVisible = true;
                                    txtCostoUniFacAlmacenajeFinal.Visible = true;
                                    lblSecFacAlmacenajeFinal.ClientVisible = true;
                                    txtSecFacAlmacenajeFinal.ClientVisible = true;
                                    lblVolRecibidoFacAlmacenajeFinal.ClientVisible = true;
                                    txtVolRecibidoFacAlmacenajeFinal.Visible = true;
                                    lblObsFacAlmacenajeFinal.ClientVisible = true;
                                    txtObsFacAlmacenajeFinal.ClientVisible = true;

                                    break;
                            }

                            pnlFacturasGasolinaBase.Visible = false;
                            btnActualizarDocumentoImpAdd.Enabled = true;

                            pnlFacturasTransMasivo.Visible = false;

                            break;
                        case 3:

                            pnlFacturasCostoProducto.Visible = false;
                            pnlFacturasCostoInspeccion.Visible = false;
                            pnlFacturasCostoMonitoreo.Visible = false;

                            pnlFacturasCostoTransporte.Visible = false;
                            pnlFacturascostoAlmacenaje.Visible = false;

                            if (intTipoInsumo == 2)
                            {
                                pnlFacturasCostoAduanero.Visible = false;
                                btnActualizarDocumentoImpAdd.Enabled = false;
                            }
                            else
                            {
                                pnlFacturasCostoAduanero.Visible = true;
                                btnActualizarDocumentoImpAdd.Enabled = true;
                            }

                            pnlFacturasCostoSeguro.Visible = false;

                            pnlFacturasGasolinaBase.Visible = false;

                            pnlFacturasTransMasivo.Visible = false;

                            break;
                        case 4:

                            pnlFacturasCostoProducto.Visible = false;
                            pnlFacturasCostoInspeccion.Visible = false;
                            pnlFacturasCostoMonitoreo.Visible = false;

                            pnlFacturasCostoTransporte.Visible = false;
                            pnlFacturascostoAlmacenaje.Visible = false;

                            pnlFacturasCostoAduanero.Visible = false;

                            pnlFacturasCostoSeguro.Visible = true;

                            btnActualizarDocumentoImpAdd.Enabled = true;
                            pnlFacturasGasolinaBase.Visible = false;

                            pnlFacturasTransMasivo.Visible = false;

                            break;
                        case 5:

                            pnlFacturasCostoProducto.Visible = false;
                            pnlFacturasCostoInspeccion.Visible = false;
                            pnlFacturasCostoMonitoreo.Visible = false;

                            pnlFacturasCostoTransporte.Visible = false;
                            pnlFacturascostoAlmacenaje.Visible = false;

                            pnlFacturasCostoAduanero.Visible = false;

                            pnlFacturasCostoSeguro.Visible = false;

                            pnlFacturasGasolinaBase.Visible = true;

                            btnActualizarDocumentoImpAdd.Enabled = true;

                            pnlFacturasTransMasivo.Visible = false;

                            break;

                        case 6:

                            pnlFacturasCostoProducto.Visible = false;
                            pnlFacturasCostoInspeccion.Visible = false;
                            pnlFacturasCostoMonitoreo.Visible = false;

                            pnlFacturasCostoTransporte.Visible = false;
                            pnlFacturascostoAlmacenaje.Visible = false;

                            pnlFacturasCostoAduanero.Visible = false;

                            pnlFacturasCostoSeguro.Visible = false;

                            pnlFacturasGasolinaBase.Visible = false;

                            btnActualizarDocumentoImpAdd.Enabled = false;

                            pnlFacturasTransMasivo.Visible = true;


                            break;

                        default:
                            pnlFacturasCostoProducto.Visible = false;
                            pnlFacturasCostoInspeccion.Visible = false;
                            pnlFacturasCostoMonitoreo.Visible = false;

                            pnlFacturasCostoTransporte.Visible = false;
                            pnlFacturascostoAlmacenaje.Visible = false;

                            pnlFacturasCostoAduanero.Visible = false;

                            pnlFacturasCostoSeguro.Visible = false;

                            btnActualizarDocumentoImpAdd.Enabled = false;

                            pnlFacturasTransMasivo.Visible = false;

                            break;
                    }

                    #endregion
                }
                catch (Exception exp)
                {
                    #region RegistraError

                    //MImprimirMensajeImportAdd(exp.Message, EnumTiposMensajeAlerta.Error);

                    #endregion
                }
            }
            else
            {
                //todo no implementado
            }
        }

        private int tipoInsumoAditivo(string strProductoSeleccionado)
        {
            int intTipoProducto = 0;
            switch (strProductoSeleccionado)
            {
                case "GRON 85":
                case "GRON 86":
                case "GRON 87":
                case "GRON 88":
                case "GRON 89":
                case "GRON 90":
                case "GRON 91":
                case "GRON 92":
                case "GRON 93":
                case "GRON 94":
                case "GRON 95":
                case "GRON 96":
                case "GRON 97":
                case "GRON 98":
                case "GRON 99":
                case "GRON 100":
                    intTipoProducto = 1;
                    break;
                case "GBLANCA":
                    intTipoProducto = 2;
                    break;
                case "GRON 80":
                case "GRON 81":
                case "GRON 82":
                case "GRON 83":
                case "GRON 84":
                    intTipoProducto = 3;
                    break;
                default:
                    intTipoProducto = 0;
                    break;
            }

            return intTipoProducto;
        }

        private void CargarControlesFacturasImportacion(int intIndice, string producto) //lmqc
        {
            int intTipoInsumo = tipoInsumoAditivo(producto);

            #region controles

            lblFactProveedorImpEdt.Visible = false;
            txtFactProveedorImpEdt.Visible = false;
            lblProveedorImpEdt.Visible = false;
            txtProveedorImpEdt.Visible = false;
            lblFactDuiImpEdt.Visible = false;
            txtFactDuiImpEdt.Visible = false;
            lblSecFactImpEdt.Visible = false;
            txtSecFactImpEdt.Visible = false;
            lblFechaDocumento.Visible = true;
            detFechaDocEfImpEdt.Visible = true;
            lblClienteEfImpEdt.Visible = false;
            txtClienteEfImpEdt.Visible = false;
            lblNitEfImpEdt.Visible = false;
            txtNitEfImpEdt.Visible = false;
            lblNoFactEfImpEdt.Visible = false;
            txtNoFactEfImpEdt.Visible = false;
            lblPrecioEfImpEdt.Visible = false;
            txtPrecioEfImpEdt.Visible = false;
            lblFactVolRecibidoImpEdt.Visible = false;
            txtFactVolRecibidoImpEdt.Visible = false;
            lblImporteEfImpEdt.Visible = false;
            txtImporteEfImpEdt.Visible = false;
            lblFactPlantaOrigenImpEdt.Visible = false;
            txtFactPlantaOrigenImpEdt.Visible = false;
            lblFactPlantaDestinoImpEdt.Visible = false;
            txtFactPlantaDestinoImpEdt.Visible = false;
            lblFactVolDespachadoImpEdt.Visible = false;
            txtFactVolDespachadoImpEdt.Visible = false;
            lblFactFleteTransporteImpEdt.Visible = false;
            txtFactFleteTransporteImpEdt.Visible = false;
            lblFactGravamenArancelarioImpEdt.Visible = false;
            txtFactGravamenArancelarioImpEdt.Visible = false;
            lblFactSiduineaImpEdt.Visible = false;
            txtFactSiduineaImpEdt.Visible = false;
            lblFactCostoArancelarioImpEdt.Visible = false;
            txtFactCostoArancelarioImpEdt.Visible = false;
            lblObsDocEfImpEdt.Visible = true;
            txtObsDocEfImpEdt.Visible = true;

            #endregion

            switch (intIndice)
            {
                case (int)EnumColumnasGrilla.FaAccp:

                    #region controles para costo de producto

                    switch (intTipoInsumo)
                    {
                        case 1:
                        case 3:
                            lblFactDuiImpEdt.Visible = true;
                            txtFactDuiImpEdt.Visible = true;
                            lblSecFactImpEdt.Visible = true;
                            txtSecFactImpEdt.Visible = true;
                            lblClienteEfImpEdt.Visible = true;
                            txtClienteEfImpEdt.Visible = true;
                            lblNitEfImpEdt.Visible = true;
                            txtNitEfImpEdt.Visible = true;
                            lblNoFactEfImpEdt.Visible = true;
                            txtNoFactEfImpEdt.Visible = true;
                            lblPrecioEfImpEdt.Visible = true;
                            txtPrecioEfImpEdt.Visible = true;
                            lblFactVolRecibidoImpEdt.Visible = true;
                            txtFactVolRecibidoImpEdt.Visible = true;
                            lblImporteEfImpEdt.Visible = true;
                            txtImporteEfImpEdt.Visible = true;

                            break;
                        case 2:
                            lblSecFactImpEdt.Visible = true;
                            txtSecFactImpEdt.Visible = true;
                            lblPrecioEfImpEdt.Visible = true;
                            txtPrecioEfImpEdt.Visible = true;
                            lblFactVolRecibidoImpEdt.Visible = true;
                            txtFactVolRecibidoImpEdt.Visible = true;
                            lblImporteEfImpEdt.Visible = true;
                            txtImporteEfImpEdt.Visible = true;

                            break;
                    }

                    lblNoFactEfImpEdt.Text = "No. Factura Proveedor : ";
                    lblClienteEfImpEdt.Text = "Proveedor : ";
                    lblPrecioEfImpEdt.Text = "Precio Unitario (Bs) : ";
                    lblImporteEfImpEdt.Text = "Valor Facturado (Bs) : ";
                    lblFactVolRecibidoImpEdt.Text = "Volumen Facturado (Lts) : ";

                    #endregion

                    break;

                case (int)EnumColumnasGrilla.FaB1cctf:

                    #region controles para costo de transporte ferreo

                    switch (intTipoInsumo)
                    {
                        case 1:
                        case 3:
                            lblFactDuiImpEdt.Visible = true;
                            txtFactDuiImpEdt.Visible = true;
                            lblSecFactImpEdt.Visible = true;
                            txtSecFactImpEdt.Visible = true;
                            lblClienteEfImpEdt.Visible = true;
                            txtClienteEfImpEdt.Visible = true;
                            lblNoFactEfImpEdt.Visible = true;
                            txtNoFactEfImpEdt.Visible = true;
                            lblPrecioEfImpEdt.Visible = true;
                            txtPrecioEfImpEdt.Visible = true;
                            lblFactVolRecibidoImpEdt.Visible = true;
                            txtFactVolRecibidoImpEdt.Visible = true;
                            lblFactPlantaOrigenImpEdt.Visible = true;
                            txtFactPlantaOrigenImpEdt.Visible = true;
                            lblFactPlantaDestinoImpEdt.Visible = true;
                            txtFactPlantaDestinoImpEdt.Visible = true;
                            lblFactVolDespachadoImpEdt.Visible = true;
                            txtFactVolDespachadoImpEdt.Visible = true;
                            lblFactFleteTransporteImpEdt.Visible = true;
                            txtFactFleteTransporteImpEdt.Visible = true;
                            break;
                        case 2:
                            lblSecFactImpEdt.Visible = true;
                            txtSecFactImpEdt.Visible = true;
                            lblClienteEfImpEdt.Visible = true;
                            txtClienteEfImpEdt.Visible = true;
                            lblNoFactEfImpEdt.Visible = true;
                            txtNoFactEfImpEdt.Visible = true;
                            lblPrecioEfImpEdt.Visible = true;
                            txtPrecioEfImpEdt.Visible = true;
                            lblFactVolRecibidoImpEdt.Visible = true;
                            txtFactVolRecibidoImpEdt.Visible = true;
                            lblFactPlantaOrigenImpEdt.Visible = true;
                            txtFactPlantaOrigenImpEdt.Visible = true;
                            lblFactPlantaDestinoImpEdt.Visible = true;
                            txtFactPlantaDestinoImpEdt.Visible = true;
                            lblFactVolDespachadoImpEdt.Visible = true;
                            txtFactVolDespachadoImpEdt.Visible = true;
                            lblFactFleteTransporteImpEdt.Visible = true;
                            txtFactFleteTransporteImpEdt.Visible = true;
                            break;
                    }

                    lblClienteEfImpEdt.Text = "Empresa : ";
                    lblPrecioEfImpEdt.Text = "Costo Unitario : ";
                    lblNoFactEfImpEdt.Text = "Nº Factura Transporte : ";
                    lblFactPlantaOrigenImpEdt.Text = "Planta Despacho : ";
                    lblFactPlantaDestinoImpEdt.Text = "Planta Recepción : ";

                    #endregion

                    break;

                case (int)EnumColumnasGrilla.FaB2cctt:

                    #region controles para costo de transporte por cisternas

                    switch (intTipoInsumo)
                    {
                        case 1:
                        case 3:
                            lblFactProveedorImpEdt.Visible = true;
                            txtFactProveedorImpEdt.Visible = true;
                            lblFactDuiImpEdt.Visible = true;
                            txtFactDuiImpEdt.Visible = true;
                            lblSecFactImpEdt.Visible = true;
                            txtSecFactImpEdt.Visible = true;
                            lblClienteEfImpEdt.Visible = true;
                            txtClienteEfImpEdt.Visible = true;
                            lblNoFactEfImpEdt.Visible = true;
                            txtNoFactEfImpEdt.Visible = true;
                            lblFactVolRecibidoImpEdt.Visible = true;
                            txtFactVolRecibidoImpEdt.Visible = true;
                            lblFactPlantaOrigenImpEdt.Visible = true;
                            txtFactPlantaOrigenImpEdt.Visible = true;
                            lblFactPlantaDestinoImpEdt.Visible = true;
                            txtFactPlantaDestinoImpEdt.Visible = true;
                            lblFactVolDespachadoImpEdt.Visible = true;
                            txtFactVolDespachadoImpEdt.Visible = true;
                            lblFactFleteTransporteImpEdt.Visible = true;
                            txtFactFleteTransporteImpEdt.Visible = true;
                            break;
                        case 2:
                            lblSecFactImpEdt.Visible = true;
                            txtSecFactImpEdt.Visible = true;
                            lblClienteEfImpEdt.Visible = true;
                            txtClienteEfImpEdt.Visible = true;
                            lblNoFactEfImpEdt.Visible = true;
                            txtNoFactEfImpEdt.Visible = true;
                            lblFactVolRecibidoImpEdt.Visible = true;
                            txtFactVolRecibidoImpEdt.Visible = true;
                            lblFactPlantaOrigenImpEdt.Visible = true;
                            txtFactPlantaOrigenImpEdt.Visible = true;
                            lblFactPlantaDestinoImpEdt.Visible = true;
                            txtFactPlantaDestinoImpEdt.Visible = true;
                            lblFactVolDespachadoImpEdt.Visible = true;
                            txtFactVolDespachadoImpEdt.Visible = true;
                            lblFactFleteTransporteImpEdt.Visible = true;
                            txtFactFleteTransporteImpEdt.Visible = true;
                            break;
                    }

                    lblClienteEfImpEdt.Text = "Empresa : ";
                    lblPrecioEfImpEdt.Text = "Costo Unitario : ";
                    lblNoFactEfImpEdt.Text = "Nº Factura Transporte : ";
                    lblFactPlantaOrigenImpEdt.Text = "Planta Despacho : ";
                    lblFactPlantaDestinoImpEdt.Text = "Planta Recepción : ";

                    #endregion

                    break;

                case (int)EnumColumnasGrilla.FaB3cctf:

                    #region controles para costo de transporte fluvial

                    switch (intTipoInsumo)
                    {
                        case 1:
                        case 3:
                            lblFactProveedorImpEdt.Visible = true;
                            txtFactProveedorImpEdt.Visible = true;
                            lblProveedorImpEdt.Visible = true;
                            txtProveedorImpEdt.Visible = true;
                            lblFactDuiImpEdt.Visible = true;
                            txtFactDuiImpEdt.Visible = true;
                            lblSecFactImpEdt.Visible = true;
                            txtSecFactImpEdt.Visible = true;
                            lblClienteEfImpEdt.Visible = true;
                            txtClienteEfImpEdt.Visible = true;
                            lblNoFactEfImpEdt.Visible = true;
                            txtNoFactEfImpEdt.Visible = true;
                            lblPrecioEfImpEdt.Visible = true;
                            txtPrecioEfImpEdt.Visible = true;
                            lblFactVolRecibidoImpEdt.Visible = true;
                            txtFactVolRecibidoImpEdt.Visible = true;
                            lblFactVolDespachadoImpEdt.Visible = true;
                            txtFactVolDespachadoImpEdt.Visible = true;
                            lblFactFleteTransporteImpEdt.Visible = true;
                            txtFactFleteTransporteImpEdt.Visible = true;
                            break;
                        case 2:

                            break;
                    }

                    lblClienteEfImpEdt.Text = "Empresa : ";
                    lblPrecioEfImpEdt.Text = "Costo Unitario : ";
                    lblNoFactEfImpEdt.Text = "Nº Factura Transporte : ";
                    lblFactVolDespachadoImpEdt.Text = "Volumen 1 : ";
                    lblFactVolRecibidoImpEdt.Text = "Volumen 2 : ";

                    #endregion

                    break;

                case (int)EnumColumnasGrilla.FaCcs:

                    #region controles para costo de seguro

                    switch (intTipoInsumo)
                    {
                        case 1:
                        case 3:
                            lblFactDuiImpEdt.Visible = false;
                            txtFactDuiImpEdt.Visible = false;
                            lblSecFactImpEdt.Visible = false;
                            txtSecFactImpEdt.Visible = false;
                            lblClienteEfImpEdt.Visible = false;
                            txtClienteEfImpEdt.Visible = false;
                            lblNoFactEfImpEdt.Visible = false;
                            txtNoFactEfImpEdt.Visible = false;
                            lblImporteEfImpEdt.Visible = false;
                            txtImporteEfImpEdt.Visible = false;
                            break;
                        case 2:
                            lblSecFactImpEdt.Visible = false;
                            txtSecFactImpEdt.Visible = false;
                            lblClienteEfImpEdt.Visible = false;
                            txtClienteEfImpEdt.Visible = false;
                            lblNoFactEfImpEdt.Visible = false;
                            txtNoFactEfImpEdt.Visible = false;
                            lblImporteEfImpEdt.Visible = false;
                            txtImporteEfImpEdt.Visible = false;
                            break;
                    }

                    #endregion

                    break;

                case (int)EnumColumnasGrilla.FaD1io:

                    #region controles para costo de inspeccion de origen

                    switch (intTipoInsumo)
                    {
                        case 1:
                        case 3:
                            lblFactProveedorImpEdt.Visible = true;
                            txtFactProveedorImpEdt.Visible = true;
                            lblProveedorImpEdt.Visible = true;
                            txtProveedorImpEdt.Visible = true;
                            lblFactDuiImpEdt.Visible = true;
                            txtFactDuiImpEdt.Visible = true;
                            lblSecFactImpEdt.Visible = true;
                            txtSecFactImpEdt.Visible = true;
                            lblClienteEfImpEdt.Visible = true;
                            txtClienteEfImpEdt.Visible = true;
                            lblNoFactEfImpEdt.Visible = true;
                            txtNoFactEfImpEdt.Visible = true;
                            lblFactVolRecibidoImpEdt.Visible = true;
                            txtFactVolRecibidoImpEdt.Visible = true;
                            lblImporteEfImpEdt.Visible = true;
                            txtImporteEfImpEdt.Visible = true;

                            break;
                        case 2:
                            lblSecFactImpEdt.Visible = true;
                            txtSecFactImpEdt.Visible = true;
                            lblClienteEfImpEdt.Visible = true;
                            txtClienteEfImpEdt.Visible = true;
                            lblNoFactEfImpEdt.Visible = true;
                            txtNoFactEfImpEdt.Visible = true;
                            lblFactVolRecibidoImpEdt.Visible = true;
                            txtFactVolRecibidoImpEdt.Visible = true;
                            lblImporteEfImpEdt.Visible = true;
                            txtImporteEfImpEdt.Visible = true;
                            break;
                    }

                    lblClienteEfImpEdt.Text = "Empresa Inspectora : ";
                    lblImporteEfImpEdt.Text = "Monto Inspeccion Origen Bs : ";

                    #endregion

                    break;

                case (int)EnumColumnasGrilla.FaD2ii:

                    #region controles para costo de inspeccion intermedia

                    switch (intTipoInsumo)
                    {
                        case 1:
                        case 3:
                            lblFactProveedorImpEdt.Visible = true;
                            txtFactProveedorImpEdt.Visible = true;
                            lblProveedorImpEdt.Visible = true;
                            txtProveedorImpEdt.Visible = true;
                            lblFactDuiImpEdt.Visible = true;
                            txtFactDuiImpEdt.Visible = true;
                            lblSecFactImpEdt.Visible = true;
                            txtSecFactImpEdt.Visible = true;
                            lblClienteEfImpEdt.Visible = true;
                            txtClienteEfImpEdt.Visible = true;
                            lblNoFactEfImpEdt.Visible = true;
                            txtNoFactEfImpEdt.Visible = true;
                            lblFactVolRecibidoImpEdt.Visible = true;
                            txtFactVolRecibidoImpEdt.Visible = true;
                            lblImporteEfImpEdt.Visible = true;
                            txtImporteEfImpEdt.Visible = true;

                            break;
                        case 2:
                            lblSecFactImpEdt.Visible = true;
                            txtSecFactImpEdt.Visible = true;
                            lblClienteEfImpEdt.Visible = true;
                            txtClienteEfImpEdt.Visible = true;
                            lblNoFactEfImpEdt.Visible = true;
                            txtNoFactEfImpEdt.Visible = true;
                            lblFactVolRecibidoImpEdt.Visible = true;
                            txtFactVolRecibidoImpEdt.Visible = true;
                            lblImporteEfImpEdt.Visible = true;
                            txtImporteEfImpEdt.Visible = true;
                            break;
                    }

                    lblClienteEfImpEdt.Text = "Empresa Inspectora : ";
                    lblImporteEfImpEdt.Text = "Monto Inspeccion Intermedio Bs : ";

                    #endregion

                    break;

                case (int)EnumColumnasGrilla.FaD3idf:

                    #region controles para costo de inspeccion final

                    switch (intTipoInsumo)
                    {
                        case 1:
                        case 3:
                            lblFactProveedorImpEdt.Visible = true;
                            txtFactProveedorImpEdt.Visible = true;
                            lblProveedorImpEdt.Visible = true;
                            txtProveedorImpEdt.Visible = true;
                            lblFactDuiImpEdt.Visible = true;
                            txtFactDuiImpEdt.Visible = true;
                            lblSecFactImpEdt.Visible = true;
                            txtSecFactImpEdt.Visible = true;
                            lblClienteEfImpEdt.Visible = true;
                            txtClienteEfImpEdt.Visible = true;
                            lblNoFactEfImpEdt.Visible = true;
                            txtNoFactEfImpEdt.Visible = true;
                            lblFactVolRecibidoImpEdt.Visible = true;
                            txtFactVolRecibidoImpEdt.Visible = true;
                            lblImporteEfImpEdt.Visible = true;
                            txtImporteEfImpEdt.Visible = true;

                            break;
                        case 2:
                            lblSecFactImpEdt.Visible = true;
                            txtSecFactImpEdt.Visible = true;
                            lblClienteEfImpEdt.Visible = true;
                            txtClienteEfImpEdt.Visible = true;
                            lblNoFactEfImpEdt.Visible = true;
                            txtNoFactEfImpEdt.Visible = true;
                            lblFactVolRecibidoImpEdt.Visible = true;
                            txtFactVolRecibidoImpEdt.Visible = true;
                            lblImporteEfImpEdt.Visible = true;
                            txtImporteEfImpEdt.Visible = true;
                            break;
                    }

                    lblClienteEfImpEdt.Text = "Empresa Inspectora : ";
                    lblImporteEfImpEdt.Text = "Monto Inspeccion Final Bs : ";

                    #endregion

                    break;

                case (int)EnumColumnasGrilla.FaEcc:

                    #region controles para costo aduanero

                    switch (intTipoInsumo)
                    {
                        case 1:
                        case 3:
                            lblFechaDocumento.Visible = true;
                            detFechaDocEfImpEdt.Visible = true;
                            lblFactGravamenArancelarioImpEdt.Visible = true;
                            txtFactGravamenArancelarioImpEdt.Visible = true;
                            lblFactSiduineaImpEdt.Visible = true;
                            txtFactSiduineaImpEdt.Visible = true;
                            lblFactCostoArancelarioImpEdt.Visible = true;
                            txtFactCostoArancelarioImpEdt.Visible = true;
                            break;
                        case 2:
                            break;
                    }

                    #endregion

                    break;

                case (int)EnumColumnasGrilla.FaF1pd:

                    #region controles para costo financiero por producto

                    lblFactPlantaOrigenImpEdt.Visible = false;
                    txtFactPlantaOrigenImpEdt.Visible = false;
                    lblFactPlantaDestinoImpEdt.Visible = false;
                    txtFactPlantaDestinoImpEdt.Visible = false;
                    lblFactVolDespachadoImpEdt.Visible = false;
                    txtFactVolDespachadoImpEdt.Visible = false;
                    lblFactVolRecibidoImpEdt.Visible = false;
                    txtFactVolRecibidoImpEdt.Visible = false;
                    lblFactFleteTransporteImpEdt.Visible = false;
                    txtFactFleteTransporteImpEdt.Visible = false;
                    lblFactGravamenArancelarioImpEdt.Visible = false;
                    txtFactGravamenArancelarioImpEdt.Visible = false;
                    lblFactSiduineaImpEdt.Visible = false;
                    txtFactSiduineaImpEdt.Visible = false;
                    txtFactCostoArancelarioImpEdt.Visible = false;
                    lblFactCostoArancelarioImpEdt.Visible = false;

                    #endregion

                    break;

                case (int)EnumColumnasGrilla.FaF2ps:

                    #region controles para costo financiero por servicio

                    lblFactPlantaOrigenImpEdt.Visible = false;
                    txtFactPlantaOrigenImpEdt.Visible = false;
                    lblFactPlantaDestinoImpEdt.Visible = false;
                    txtFactPlantaDestinoImpEdt.Visible = false;
                    lblFactVolDespachadoImpEdt.Visible = false;
                    txtFactVolDespachadoImpEdt.Visible = false;
                    lblFactVolRecibidoImpEdt.Visible = false;
                    txtFactVolRecibidoImpEdt.Visible = false;
                    lblFactFleteTransporteImpEdt.Visible = false;
                    txtFactFleteTransporteImpEdt.Visible = false;
                    lblFactGravamenArancelarioImpEdt.Visible = false;
                    txtFactGravamenArancelarioImpEdt.Visible = false;
                    lblFactSiduineaImpEdt.Visible = false;
                    txtFactSiduineaImpEdt.Visible = false;
                    txtFactCostoArancelarioImpEdt.Visible = false;
                    lblFactCostoArancelarioImpEdt.Visible = false;

                    #endregion

                    break;

                case (int)EnumColumnasGrilla.FaG1ai:

                    #region controles para costo de almacenaje intermedio

                    switch (intTipoInsumo)
                    {
                        case 1:
                        case 3:
                            lblFactDuiImpEdt.Visible = true;
                            txtFactDuiImpEdt.Visible = true;
                            lblSecFactImpEdt.Visible = true;
                            txtSecFactImpEdt.Visible = true;
                            lblClienteEfImpEdt.Visible = true;
                            txtClienteEfImpEdt.Visible = true;
                            lblNoFactEfImpEdt.Visible = true;
                            txtNoFactEfImpEdt.Visible = true;
                            lblPrecioEfImpEdt.Visible = true;
                            txtPrecioEfImpEdt.Visible = true;
                            lblFactVolRecibidoImpEdt.Visible = true;
                            txtFactVolRecibidoImpEdt.Visible = true;
                            break;

                        case 2:
                            lblSecFactImpEdt.Visible = true;
                            txtSecFactImpEdt.Visible = true;
                            lblNoFactEfImpEdt.Visible = true;
                            txtNoFactEfImpEdt.Visible = true;
                            lblPrecioEfImpEdt.Visible = true;
                            txtPrecioEfImpEdt.Visible = true;
                            lblFactVolRecibidoImpEdt.Visible = true;
                            txtFactVolRecibidoImpEdt.Visible = true;
                            break;

                    }

                    lblClienteEfImpEdt.Text = "Empresa Almacenaje : ";

                    #endregion

                    break;
                case (int)EnumColumnasGrilla.FaG2adf:

                    #region controles para costo de almacenaje final

                    switch (intTipoInsumo)
                    {
                        case 1:
                        case 3:
                            lblFactDuiImpEdt.Visible = true;
                            txtFactDuiImpEdt.Visible = true;
                            lblSecFactImpEdt.Visible = true;
                            txtSecFactImpEdt.Visible = true;
                            lblClienteEfImpEdt.Visible = true;
                            txtClienteEfImpEdt.Visible = true;
                            lblNoFactEfImpEdt.Visible = true;
                            txtNoFactEfImpEdt.Visible = true;
                            lblPrecioEfImpEdt.Visible = true;
                            txtPrecioEfImpEdt.Visible = true;
                            lblFactVolRecibidoImpEdt.Visible = true;
                            txtFactVolRecibidoImpEdt.Visible = true;
                            break;

                        case 2:
                            lblSecFactImpEdt.Visible = true;
                            txtSecFactImpEdt.Visible = true;
                            lblNoFactEfImpEdt.Visible = true;
                            txtNoFactEfImpEdt.Visible = true;
                            lblPrecioEfImpEdt.Visible = true;
                            txtPrecioEfImpEdt.Visible = true;
                            lblFactVolRecibidoImpEdt.Visible = true;
                            txtFactVolRecibidoImpEdt.Visible = true;
                            break;

                    }

                    lblClienteEfImpEdt.Text = "Empresa Almacenaje : ";

                    #endregion

                    break;

                case (int)EnumColumnasGrilla.FaHcm:

                    #region controles para costo de monitoreo

                    switch (intTipoInsumo)
                    {
                        case 1:
                        case 3:
                            lblFactProveedorImpEdt.Visible = true;
                            txtFactProveedorImpEdt.Visible = true;
                            lblFactDuiImpEdt.Visible = true;
                            txtFactDuiImpEdt.Visible = true;
                            lblSecFactImpEdt.Visible = true;
                            txtSecFactImpEdt.Visible = true;
                            lblClienteEfImpEdt.Visible = true;
                            txtClienteEfImpEdt.Visible = true;
                            lblNoFactEfImpEdt.Visible = true;
                            txtNoFactEfImpEdt.Visible = true;
                            lblFactVolRecibidoImpEdt.Visible = true;
                            txtFactVolRecibidoImpEdt.Visible = true;
                            lblImporteEfImpEdt.Visible = true;
                            txtImporteEfImpEdt.Visible = true;
                            break;
                        case 2:
                            lblSecFactImpEdt.Visible = true;
                            txtSecFactImpEdt.Visible = true;
                            lblClienteEfImpEdt.Visible = true;
                            txtClienteEfImpEdt.Visible = true;
                            lblNoFactEfImpEdt.Visible = true;
                            txtNoFactEfImpEdt.Visible = true;
                            lblFactVolRecibidoImpEdt.Visible = true;
                            txtFactVolRecibidoImpEdt.Visible = true;
                            lblImporteEfImpEdt.Visible = true;
                            txtImporteEfImpEdt.Visible = true;
                            break;
                    }

                    lblNoFactEfImpEdt.Text = "Nº Factura Monitoreo : ";
                    lblClienteEfImpEdt.Text = "Empresa Monitoreo : ";
                    lblImporteEfImpEdt.Text = "Monto Monitoreo Bs : ";

                    #endregion

                    break;

                case (int)EnumColumnasGrilla.FaI1di:

                    #region controles para costo volumen destino intermedio

                    lblFactPlantaOrigenImpEdt.Visible = false;
                    txtFactPlantaOrigenImpEdt.Visible = false;
                    lblFactPlantaDestinoImpEdt.Visible = true;
                    txtFactPlantaDestinoImpEdt.Visible = true;
                    lblFactVolDespachadoImpEdt.Visible = false;
                    txtFactVolDespachadoImpEdt.Visible = false;
                    lblFactVolRecibidoImpEdt.Visible = true;
                    txtFactVolRecibidoImpEdt.Visible = true;
                    lblFactFleteTransporteImpEdt.Visible = false;
                    txtFactFleteTransporteImpEdt.Visible = false;
                    lblFactGravamenArancelarioImpEdt.Visible = false;
                    txtFactGravamenArancelarioImpEdt.Visible = false;
                    lblFactSiduineaImpEdt.Visible = false;
                    txtFactSiduineaImpEdt.Visible = false;
                    txtFactCostoArancelarioImpEdt.Visible = false;
                    lblFactCostoArancelarioImpEdt.Visible = false;

                    #endregion

                    break;

                case (int)EnumColumnasGrilla.FaI2df:

                    #region controles para costo volumen destino final

                    lblFactPlantaOrigenImpEdt.Visible = false;
                    txtFactPlantaOrigenImpEdt.Visible = false;
                    lblFactPlantaDestinoImpEdt.Visible = true;
                    txtFactPlantaDestinoImpEdt.Visible = true;
                    lblFactVolDespachadoImpEdt.Visible = false;
                    txtFactVolDespachadoImpEdt.Visible = false;
                    lblFactVolRecibidoImpEdt.Visible = true;
                    txtFactVolRecibidoImpEdt.Visible = true;
                    lblFactFleteTransporteImpEdt.Visible = false;
                    txtFactFleteTransporteImpEdt.Visible = false;
                    lblFactGravamenArancelarioImpEdt.Visible = false;
                    txtFactGravamenArancelarioImpEdt.Visible = false;
                    lblFactSiduineaImpEdt.Visible = false;
                    txtFactSiduineaImpEdt.Visible = false;
                    txtFactCostoArancelarioImpEdt.Visible = false;
                    lblFactCostoArancelarioImpEdt.Visible = false;

                    #endregion

                    break;

                default:

                    #region controles costos no identificados

                    lblFactPlantaOrigenImpEdt.Visible = false;
                    txtFactPlantaOrigenImpEdt.Visible = false;
                    lblFactPlantaDestinoImpEdt.Visible = false;
                    txtFactPlantaDestinoImpEdt.Visible = false;
                    lblFactVolDespachadoImpEdt.Visible = false;
                    txtFactVolDespachadoImpEdt.Visible = false;
                    lblFactVolRecibidoImpEdt.Visible = false;
                    txtFactVolRecibidoImpEdt.Visible = false;
                    lblFactFleteTransporteImpEdt.Visible = false;
                    txtFactFleteTransporteImpEdt.Visible = false;
                    lblFactGravamenArancelarioImpEdt.Visible = false;
                    txtFactGravamenArancelarioImpEdt.Visible = false;
                    lblFactSiduineaImpEdt.Visible = false;
                    txtFactSiduineaImpEdt.Visible = false;
                    txtFactCostoArancelarioImpEdt.Visible = false;
                    lblFactCostoArancelarioImpEdt.Visible = false;

                    #endregion

                    break;
            }
        }

        protected void dgvTransaccionesImp_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            try
            {
                #region Proceso a Ejecutar

                Session.Remove(CVariablesSesion.GrillaTransaccion);
                dgvTransaccionesImp.DataSource = null;
                dgvTransaccionesImp.DataBind();

                if (Session[CVariablesSesion.StrAuxiliar] == null)
                    return;

                var strDato = Session[CVariablesSesion.ParamAux].ToString().Split('|');
                var strFecha = strDato[0];
                var strIdProd = strDato[1];
                var strIdTiIn = strDato[2];
                var strIdInfr = strDato[3];
                var decFilt = 1;
                var strPar6 = strIdProd;
                var strPar7 = "";
                var strPar8 = "";

                if (dgvAcumulado.Columns[5].Visible)
                {
                    decFilt = 2;
                    strPar7 = strIdInfr;
                    strPar8 = strIdTiIn;
                }

                var lstDespachos = _consulta.ObtenerMovimiento("",
                new EObjetoEnvio8ParametrosProto
                {
                    decFiltro = 3, //Filtro 1 por producto //2 consulta por infraestructura
                    strParametro1 = tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString(), //Id proyecto/entidad
                    strParametro2 = strFecha + "000000",//fecha inicial
                    strParametro3 = strFecha + "235959", //fecha final
                    strParametro4 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(), //id Actividad
                    strParametro5 = cmbTipoOperacionImp.Value.ToString(), //tipo operacion
                    strParametro6 = strPar6, //idProducto 
                    strParametro7 = strPar7, // id_infraestructura
                    strParametro8 = strPar8 // tipo_infraestructura
                });

                if (lstDespachos.intCodigo == 1)
                {
                    var lstResultado = (List<O_CONSULTA_MOVIM_TRAN_CTY>)lstDespachos.oResultado;
                    if (lstResultado.Any())
                    {

                        var registros = lstResultado;
                        Session[CVariablesSesion.GrillaTransaccion] = registros;

                        dgvTransaccionesImp.DataSource = registros;
                        dgvTransaccionesImp.DataBind();
                    }
                    else
                    {
                        MImprimirMensaje(lstDespachos.strMensaje, (lstDespachos.strMensaje == "No se encontraron registros") ? EnumTiposMensajeAlerta.Alerta : EnumTiposMensajeAlerta.Error);
                    }
                }
                else
                {
                    MImprimirMensaje(lstDespachos.strMensaje, (lstDespachos.strMensaje == "No se encontraron registros") ? EnumTiposMensajeAlerta.Alerta : EnumTiposMensajeAlerta.Error);
                    if (lstDespachos.intCodigo != -100) return;

                    var objEnv = new EObjetoEnvio8ParametrosProto
                    {
                        decFiltro = decFilt,
                        strParametro1 = tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString(),
                        strParametro2 = strFecha + "000000",
                        strParametro3 = strFecha + "235959",
                        strParametro4 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
                        strParametro5 = cmbTipoOperacion.Value.ToString(),
                        strParametro6 = strPar6,
                        strParametro7 = strPar7,
                        strParametro8 = strPar8
                    };
                    var expBd = new Exception(lstDespachos.intCodigo + " " + lstDespachos.strMensaje + " - " + JsonSerializer.SerializeToString(objEnv));
                    var oMensajeUsuario = new CLogTraza.MensajeUsuario
                    {
                        decUsuarioId = Session[CVariablesSesion.IdUsuario],
                        decIdModulo = CParametrosHydro.decIdAplicacion,
                        strAccion = GetType().Name + "." + MethodBase.GetCurrentMethod().Name,
                        strIp = HttpContext.Current.Request.UserHostAddress,
                        decNivelCapa = (int)CLogTraza.CapasNivel.Presentacion
                    };
                    CLogTraza.Error(oMensajeUsuario, expBd);
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

        protected void dgvTransaccionesImp_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    var param = Request.Params.Get("__CALLBACKPARAM");
                    if (!String.IsNullOrEmpty(param) && !param.Contains("CANCELEDIT"))
                    {
                        try
                        {
                            //MConfiguraFormulario();
                            if (Session[CVariablesSesion.GrillaAuxiliar] != null)
                            {
                                var dgvDocumentosImp = (dgvTransaccionesImp.FindEditFormTemplateControl("dgvDocumentosImp")) as ASPxGridView;
                                dgvDocumentosImp.DataSource = Session[CVariablesSesion.GrillaAuxiliar];
                                dgvDocumentosImp.DataBind();
                            }
                            else
                            {
                                //todo grilla vacia: no implementado
                            }
                        }
                        catch (Exception)
                        { }

                        dgvTransaccionesImp = (sender) as ASPxGridView;
                        if (tlsOperadores.FocusedNode != null && (int)tlsOperadores.FocusedNode.GetValue("IntIdActividad") > 0)
                        {
                            #region Instancia componentes
                            var txtIdEfImp = (dgvTransaccionesImp.FindEditFormTemplateControl("txtIdEfImp")) as ASPxTextBox;
                            var detFechaEfImp = (dgvTransaccionesImp.FindEditFormTemplateControl("detFechaEfImp")) as ASPxDateEdit;
                            var cmbEstadoEfImp = (dgvTransaccionesImp.FindEditFormTemplateControl("cmbEstadoEfImp")) as ASPxComboBox;

                            var cmbProdEfImp = (dgvTransaccionesImp.FindEditFormTemplateControl("cmbProdEfImp")) as ASPxComboBox;
                            var cmbUnidadEfImp = (dgvTransaccionesImp.FindEditFormTemplateControl("cmbUnidadEfImp")) as ASPxComboBox;
                            var txtCantidadEfImp = (dgvTransaccionesImp.FindEditFormTemplateControl("txtCantidadEfImp")) as ASPxTextBox;
                            var txtObsEfImp = (dgvTransaccionesImp.FindEditFormTemplateControl("txtObsEfImp")) as ASPxTextBox;
                            var dgvDocumentosImp = (dgvTransaccionesImp.FindEditFormTemplateControl("dgvDocumentosImp")) as ASPxGridView;

                            var txtJsonRef = (dgvTransaccionesImp.FindEditFormTemplateControl("txtJsonRef")) as ASPxTextBox;
                            var txtEstJsonRef = (dgvTransaccionesImp.FindEditFormTemplateControl("txtEstJsonRef")) as ASPxTextBox;
                            var txtIdCuenta = (dgvTransaccionesImp.FindEditFormTemplateControl("txtIdCuenta")) as ASPxTextBox;
                            var txtIdCuentaAux = (dgvTransaccionesImp.FindEditFormTemplateControl("txtIdCuentaAux")) as ASPxTextBox;
                            var txtIdParGl = (dgvTransaccionesImp.FindEditFormTemplateControl("txtIdParGl")) as ASPxTextBox;
                            var txtIdParEs = (dgvTransaccionesImp.FindEditFormTemplateControl("txtIdParEs")) as ASPxTextBox;
                            var txtIdTipoInfAux = (dgvTransaccionesImp.FindEditFormTemplateControl("txtIdTipoInfAux")) as ASPxTextBox;
                            var txtIdInfAux = (dgvTransaccionesImp.FindEditFormTemplateControl("txtIdInfAux")) as ASPxTextBox;

                            var btnAceptarImp = (dgvTransaccionesImp.FindEditFormTemplateControl("btnAceptarImp")) as ASPxButton;
                            btnAceptarImp.Visible = true;

                            #endregion

                            var strValida = (Session[CVariablesSesion.HabilitaBotones] == null) ? "ND" : Session[CVariablesSesion.HabilitaBotones].ToString();
                            if (!strValida.Equals("MODSI"))
                                btnAceptarImp.Visible = false;

                            if (param.Contains("ADDNEWROW"))
                            {
                                #region nuevo registro

                                Session.Remove(CVariablesSesion.GrillaAuxiliar);

                                if (Session[CVariablesSesion.ParamAux] != null)
                                {
                                    var strParam = Session[CVariablesSesion.ParamAux].ToString();
                                    var arrParam = strParam.Split('|');

                                    MCargaListadosImp();

                                    detFechaEfImp.Date = DateTime.Parse(arrParam[0].Substring(6, 2) + "/" + arrParam[0].Substring(4, 2) + "/" + arrParam[0].Substring(0, 4));
                                    detFechaEfImp.ClientEnabled = true;
                                    if (Convert.ToInt32(cmbTipoOperacion.Value) == (int)EnumTipoOperaciones.SaldoInicial)
                                    {
                                        detFechaEfImp.Date = DateTime.Parse("01/" + arrParam[0].Substring(4, 2) + "/" + arrParam[0].Substring(0, 4));
                                        detFechaEfImp.ClientEnabled = false;
                                    }

                                    cmbProdEfImp.SelectedItem = cmbProdEfImp.Items.FindByValue(arrParam[1]);

                                    //lmqc--- desagrega las obervaciones en parametricas para importadores

                                    var txtDui = (dgvTransaccionesImp.FindEditFormTemplateControl("txtDui")) as ASPxTextBox;
                                    var txtMonSolCompensar = (dgvTransaccionesImp.FindEditFormTemplateControl("txtMonSolCompensar")) as ASPxTextBox;
                                    var txtXgasolinaBlanca = (dgvTransaccionesImp.FindEditFormTemplateControl("txtXgasolinaBlanca")) as ASPxTextBox;
                                    var txtXimp = (dgvTransaccionesImp.FindEditFormTemplateControl("txtXimp")) as ASPxTextBox;
                                    var txtPreGasolinaBlanca = (dgvTransaccionesImp.FindEditFormTemplateControl("txtPreGasolinaBlanca")) as ASPxTextBox;
                                    var txtPreNetoIva = (dgvTransaccionesImp.FindEditFormTemplateControl("txtPreNetoIva")) as ASPxTextBox;
                                    var txtTarifaTransPol = (dgvTransaccionesImp.FindEditFormTemplateControl("txtTarifaTransPol")) as ASPxTextBox;
                                    var txtTarifaNetoIva = (dgvTransaccionesImp.FindEditFormTemplateControl("txtTarifaNetoIva")) as ASPxTextBox;
                                    var txtPpt = (dgvTransaccionesImp.FindEditFormTemplateControl("txtPpt")) as ASPxTextBox;

                                    var txtIncoterms = (dgvTransaccionesImp.FindEditFormTemplateControl("txtIncoterms")) as ASPxTextBox;
                                    var txtFronteraIngreso = (dgvTransaccionesImp.FindEditFormTemplateControl("txtFronteraIngreso")) as ASPxTextBox;

                                    var lblDui = (dgvTransaccionesImp.FindEditFormTemplateControl("lblDui")) as ASPxLabel;
                                    var lblMonSolCompensar = (dgvTransaccionesImp.FindEditFormTemplateControl("lblMonSolCompensar")) as ASPxLabel;
                                    var lblXgasolinaBlanca = (dgvTransaccionesImp.FindEditFormTemplateControl("lblXgasolinaBlanca")) as ASPxLabel;
                                    var lblXimp = (dgvTransaccionesImp.FindEditFormTemplateControl("lblXimp")) as ASPxLabel;
                                    var lblPreGasolinaBlanca = (dgvTransaccionesImp.FindEditFormTemplateControl("lblPreGasolinaBlanca")) as ASPxLabel;
                                    var lblPreNetoIva = (dgvTransaccionesImp.FindEditFormTemplateControl("lblPreNetoIva")) as ASPxLabel;
                                    var lblTarifaTransPol = (dgvTransaccionesImp.FindEditFormTemplateControl("lblTarifaTransPol")) as ASPxLabel;
                                    var lblTarifaNetoIva = (dgvTransaccionesImp.FindEditFormTemplateControl("lblTarifaNetoIva")) as ASPxLabel;
                                    var lblPpt = (dgvTransaccionesImp.FindEditFormTemplateControl("lblPpt")) as ASPxLabel;
                                    var lblIncoterms = (dgvTransaccionesImp.FindEditFormTemplateControl("lblIncoterms")) as ASPxLabel;
                                    var lblFronteraIngreso = (dgvTransaccionesImp.FindEditFormTemplateControl("lblFronteraIngreso")) as ASPxLabel;

                                    int intIdProducto = Convert.ToInt32(arrParam[1].ToString());

                                    switch (intIdProducto)
                                    {
                                        case 278:
                                        case 401://test 401 desarrollo 4326 produccion 1829
                                        case 402://test 402 desarrollo 4327 produccion 1830
                                        case 403://test 403 desarrollo 4328 produccion 1831
                                        case 404://test 404 desarrollo 4329 produccion 1832
                                        case 405://test 405 desarrollo 4330 produccion 1833
                                            txtDui.ClientVisible = true;
                                            txtMonSolCompensar.ClientVisible = false;
                                            txtXgasolinaBlanca.ClientVisible = false;
                                            txtXimp.ClientVisible = false;
                                            txtPreGasolinaBlanca.ClientVisible = false;
                                            txtPreNetoIva.ClientVisible = false;
                                            txtTarifaTransPol.ClientVisible = false;
                                            txtTarifaNetoIva.ClientVisible = false;
                                            txtPpt.ClientVisible = false; //rgcg true;
                                            txtIncoterms.ClientVisible = false;
                                            txtFronteraIngreso.ClientVisible = false;
                                            lblDui.ClientVisible = true;
                                            lblMonSolCompensar.ClientVisible = false;
                                            lblXgasolinaBlanca.ClientVisible = false;
                                            lblXimp.ClientVisible = false;
                                            lblPreGasolinaBlanca.ClientVisible = false;
                                            lblPreNetoIva.ClientVisible = false;
                                            lblTarifaTransPol.ClientVisible = false;
                                            lblTarifaNetoIva.ClientVisible = false;
                                            lblPpt.ClientVisible = true;
                                            lblIncoterms.ClientVisible = false;
                                            lblFronteraIngreso.ClientVisible = false;
                                            break;
                                        default:
                                            txtDui.ClientVisible = true;
                                            txtMonSolCompensar.ClientVisible = false;
                                            txtXgasolinaBlanca.ClientVisible = false;
                                            txtXimp.ClientVisible = false;
                                            txtPreGasolinaBlanca.ClientVisible = false; //rgcg ;
                                            txtPreNetoIva.ClientVisible = false;
                                            txtTarifaTransPol.ClientVisible = false;
                                            txtTarifaNetoIva.ClientVisible = false;
                                            txtPpt.ClientVisible = false;
                                            txtIncoterms.ClientVisible = false;
                                            txtFronteraIngreso.ClientVisible = true;
                                            lblDui.ClientVisible = true;
                                            lblMonSolCompensar.ClientVisible = false;
                                            lblXgasolinaBlanca.ClientVisible = false;
                                            lblXimp.ClientVisible = false;
                                            lblPreGasolinaBlanca.ClientVisible = false;
                                            lblPreNetoIva.ClientVisible = false;
                                            lblTarifaTransPol.ClientVisible = false;
                                            lblTarifaNetoIva.ClientVisible = false;
                                            lblPpt.ClientVisible = false;
                                            lblIncoterms.ClientVisible = false;
                                            lblFronteraIngreso.ClientVisible = true;
                                            break;

                                    }

                                    txtDui.Text = "";
                                    txtMonSolCompensar.Text = "";
                                    txtXgasolinaBlanca.Text = ObtenerXgasolinaBlanca(cmbProdEfImp.Text, (int)tlsOperadores.FocusedNode.GetValue("IntIdEntidad")).Replace(".", ",");
                                    txtXimp.Text = ObtenerXimportacion(cmbProdEfImp.Text, (int)tlsOperadores.FocusedNode.GetValue("IntIdEntidad")).Replace(".", ",");
                                    txtPreGasolinaBlanca.Text = "";
                                    txtPreNetoIva.Text = "";
                                    txtTarifaTransPol.Text = "";
                                    txtTarifaNetoIva.Text = "";
                                    txtPpt.Text = "";
                                    txtFronteraIngreso.Text = "";
                                    txtIncoterms.Text = "";
                                    txtObsEfImp.Text = "";

                                    var o2 = tlsOperadores.FocusedNode.GetValue("strActividad").ToString();
                                    if (o2 == "IMPORTACIÓN DE LUBRICANTES")
                                    {
                                        txtXgasolinaBlanca.Visible = false;
                                        lblXgasolinaBlanca.Visible = false;


                                        lblPreGasolinaBlanca.Visible = false;
                                        txtPreGasolinaBlanca.Visible = false;

                                        lblTarifaTransPol.Visible = false;
                                        txtTarifaTransPol.Visible = false;

                                        lblMonSolCompensar.Visible = false;
                                        txtMonSolCompensar.Visible = false;

                                        lblXimp.Visible = false;
                                        txtXimp.Visible = false;

                                        lblPreNetoIva.Visible = false;
                                        txtPreNetoIva.Visible = false;

                                        lblTarifaTransPol.Visible = false;
                                        txtTarifaTransPol.Visible = false;

                                        lblIncoterms.Visible = false;
                                        txtIncoterms.Visible = false;

                                        lblPpt.Visible = false;
                                        txtPpt.Visible = false;

                                        lblFronteraIngreso.Visible = false;
                                        txtFronteraIngreso.Visible = false;

                                    }
                                    else
                                    {
                                        txtXgasolinaBlanca.Visible = true;
                                        lblXgasolinaBlanca.Visible = true;

                                        lblPreGasolinaBlanca.Visible = true;
                                        txtPreGasolinaBlanca.Visible = true;

                                        lblTarifaTransPol.Visible = true;
                                        txtTarifaTransPol.Visible = true;

                                        lblMonSolCompensar.Visible = true;
                                        txtMonSolCompensar.Visible = true;

                                        lblXimp.Visible = true;
                                        txtXimp.Visible = true;

                                        lblPreNetoIva.Visible = true;
                                        txtPreNetoIva.Visible = true;

                                        lblTarifaTransPol.Visible = true;
                                        txtTarifaTransPol.Visible = true;

                                        lblIncoterms.Visible = true;
                                        txtIncoterms.Visible = true;

                                        lblPpt.Visible = true;
                                        txtPpt.Visible = true;

                                        lblFronteraIngreso.Visible = true;
                                        txtFronteraIngreso.Visible = true;
                                    }


                                    var lstDocumentoMov = new List<CRegistroDocumento>();
                                    dgvDocumentosImp.DataSource = lstDocumentoMov;
                                    dgvDocumentosImp.DataBind();
                                }
                                else
                                {
                                    //todo No implementado.
                                }


                                #endregion
                            }
                            else if (param.Contains("STARTEDIT"))
                            {
                                #region editar registro

                                var index = dgvTransaccionesImp.EditingRowVisibleIndex;
                                if (index >= 0)
                                {
                                    MCargaListadosImp();

                                    Session.Remove(CVariablesSesion.GrillaAuxiliar);
                                    cmbEstadoEfImp.Enabled = false;
                                    txtIdEfImp.Text = dgvTransaccionesImp.GetRowValues(index, "ID_MOVIMIENTO").ToString();
                                    detFechaEfImp.Date = DateTime.Parse(dgvTransaccionesImp.GetRowValues(index, "STR_FECHA").ToString());

                                    detFechaEfImp.ClientEnabled = true;
                                    if (Convert.ToInt32(cmbTipoOperacionImp.Value) == (int)EnumTipoOperaciones.SaldoInicial)
                                        detFechaEfImp.ClientEnabled = false;

                                    cmbEstadoEfImp.Value = Convert.ToInt32(dgvTransaccionesImp.GetRowValues(index, "ID_ESTADO_MOVIMIENTO"));

                                    cmbProdEfImp.SelectedItem = cmbProdEfImp.Items.FindByValue(dgvTransaccionesImp.GetRowValues(index, "ID_HIDROCARBURO").ToString());
                                    cmbUnidadEfImp.SelectedItem = cmbUnidadEfImp.Items.FindByValue(dgvTransaccionesImp.GetRowValues(index, "ID_UNIDAD_HIDROCARBURO").ToString());
                                    txtCantidadEfImp.Text = dgvTransaccionesImp.GetRowValues(index, "CANTIDAD").ToString();
                                    txtObsEfImp.Text = (dgvTransaccionesImp.GetRowValues(index, "OBSERVACION_MOVIMIENTO") == null) ? "" : dgvTransaccionesImp.GetRowValues(index, "OBSERVACION_MOVIMIENTO").ToString();

                                    txtJsonRef.Text = (dgvTransaccionesImp.GetRowValues(index, "JSON_REFERENCIA") == null) ? "" : dgvTransaccionesImp.GetRowValues(index, "JSON_REFERENCIA").ToString();
                                    txtEstJsonRef.Text = (dgvTransaccionesImp.GetRowValues(index, "ESTADO_JSON_REFERENCIA") == null) ? "0" : dgvTransaccionesImp.GetRowValues(index, "ESTADO_JSON_REFERENCIA").ToString();
                                    txtIdCuenta.Text = (dgvTransaccionesImp.GetRowValues(index, "ID_CUENTA") == null) ? "0" : dgvTransaccionesImp.GetRowValues(index, "ID_CUENTA").ToString();
                                    txtIdCuentaAux.Text = (dgvTransaccionesImp.GetRowValues(index, "ID_CUENTA_AUX") == null) ? "0" : dgvTransaccionesImp.GetRowValues(index, "ID_CUENTA_AUX").ToString();
                                    txtIdParGl.Text = (dgvTransaccionesImp.GetRowValues(index, "ID_PARAM_GLOBAL") == null) ? "0" : dgvTransaccionesImp.GetRowValues(index, "ID_PARAM_GLOBAL").ToString();
                                    txtIdParEs.Text = (dgvTransaccionesImp.GetRowValues(index, "ID_PARAM_ESPEC") == null) ? "0" : dgvTransaccionesImp.GetRowValues(index, "ID_PARAM_ESPEC").ToString();
                                    //txtIdTipoCons.Text = (dgvTransacciones.GetRowValues(index, "ID_TIPO_CONSUMIDOR") == null) ? "0" : dgvTransacciones.GetRowValues(index, "ID_TIPO_CONSUMIDOR").ToString();
                                    txtIdTipoInfAux.Text = (dgvTransaccionesImp.GetRowValues(index, "ID_TIPO_INFRA_AUX") == null) ? "0" : dgvTransaccionesImp.GetRowValues(index, "ID_TIPO_INFRA_AUX").ToString();
                                    txtIdInfAux.Text = (dgvTransaccionesImp.GetRowValues(index, "ID_INFRA_AUX") == null) ? "0" : dgvTransaccionesImp.GetRowValues(index, "ID_INFRA_AUX").ToString();

                                    //lmqc--- desagrega las obervaciones en parametricas para importadores
                                    var txtDui = (dgvTransaccionesImp.FindEditFormTemplateControl("txtDui")) as ASPxTextBox;
                                    var txtMonSolCompensar = (dgvTransaccionesImp.FindEditFormTemplateControl("txtMonSolCompensar")) as ASPxTextBox;
                                    var txtXgasolinaBlanca = (dgvTransaccionesImp.FindEditFormTemplateControl("txtXgasolinaBlanca")) as ASPxTextBox;
                                    var txtXimp = (dgvTransaccionesImp.FindEditFormTemplateControl("txtXimp")) as ASPxTextBox;
                                    var txtPreGasolinaBlanca = (dgvTransaccionesImp.FindEditFormTemplateControl("txtPreGasolinaBlanca")) as ASPxTextBox;
                                    var txtPreNetoIva = (dgvTransaccionesImp.FindEditFormTemplateControl("txtPreNetoIva")) as ASPxTextBox;
                                    var txtTarifaTransPol = (dgvTransaccionesImp.FindEditFormTemplateControl("txtTarifaTransPol")) as ASPxTextBox;
                                    var txtTarifaNetoIva = (dgvTransaccionesImp.FindEditFormTemplateControl("txtTarifaNetoIva")) as ASPxTextBox;
                                    var txtPpt = (dgvTransaccionesImp.FindEditFormTemplateControl("txtPpt")) as ASPxTextBox;

                                    var txtIncoterms = (dgvTransaccionesImp.FindEditFormTemplateControl("txtIncoterms")) as ASPxTextBox;
                                    var txtFronteraIngreso = (dgvTransaccionesImp.FindEditFormTemplateControl("txtFronteraIngreso")) as ASPxTextBox;

                                    var lblDui = (dgvTransaccionesImp.FindEditFormTemplateControl("lblDui")) as ASPxLabel;
                                    var lblMonSolCompensar = (dgvTransaccionesImp.FindEditFormTemplateControl("lblMonSolCompensar")) as ASPxLabel;
                                    var lblXgasolinaBlanca = (dgvTransaccionesImp.FindEditFormTemplateControl("lblXgasolinaBlanca")) as ASPxLabel;
                                    var lblXimp = (dgvTransaccionesImp.FindEditFormTemplateControl("lblXimp")) as ASPxLabel;
                                    var lblPreGasolinaBlanca = (dgvTransaccionesImp.FindEditFormTemplateControl("lblPreGasolinaBlanca")) as ASPxLabel;
                                    var lblPreNetoIva = (dgvTransaccionesImp.FindEditFormTemplateControl("lblPreNetoIva")) as ASPxLabel;
                                    var lblTarifaTransPol = (dgvTransaccionesImp.FindEditFormTemplateControl("lblTarifaTransPol")) as ASPxLabel;
                                    var lblTarifaNetoIva = (dgvTransaccionesImp.FindEditFormTemplateControl("lblTarifaNetoIva")) as ASPxLabel;
                                    var lblPpt = (dgvTransaccionesImp.FindEditFormTemplateControl("lblPpt")) as ASPxLabel;
                                    var lblIncoterms = (dgvTransaccionesImp.FindEditFormTemplateControl("lblIncoterms")) as ASPxLabel;
                                    var lblFronteraIngreso = (dgvTransaccionesImp.FindEditFormTemplateControl("lblFronteraIngreso")) as ASPxLabel;

                                    int intIdProducto = Convert.ToInt32(cmbProdEfImp.Value);

                                    switch (intIdProducto)
                                    {
                                        case 278:
                                        case 401://test 401 desarrollo 4326 produccion 1829
                                        case 402://test 402 desarrollo 4327 produccion 1830
                                        case 403://test 403 desarrollo 4328 produccion 1831
                                        case 404://test 404 desarrollo 4329 produccion 1832
                                        case 405://test 405 desarrollo 4330 produccion 1833
                                            txtDui.ClientVisible = true;
                                            txtMonSolCompensar.ClientVisible = false;
                                            txtXgasolinaBlanca.ClientVisible = false;
                                            txtXimp.ClientVisible = false;
                                            txtPreGasolinaBlanca.ClientVisible = false;
                                            txtPreNetoIva.ClientVisible = false;
                                            txtTarifaTransPol.ClientVisible = false;
                                            txtTarifaNetoIva.ClientVisible = false;
                                            txtPpt.ClientVisible = true;
                                            txtIncoterms.ClientVisible = false;
                                            txtFronteraIngreso.ClientVisible = false;
                                            lblDui.ClientVisible = true;
                                            lblMonSolCompensar.ClientVisible = false;
                                            lblXgasolinaBlanca.ClientVisible = false;
                                            lblXimp.ClientVisible = false;
                                            lblPreGasolinaBlanca.ClientVisible = false;
                                            lblPreNetoIva.ClientVisible = false;
                                            lblTarifaTransPol.ClientVisible = false;
                                            lblTarifaNetoIva.ClientVisible = false;
                                            lblPpt.ClientVisible = true;
                                            lblIncoterms.ClientVisible = false;
                                            lblFronteraIngreso.ClientVisible = false;
                                            break;
                                        default:
                                            txtDui.ClientVisible = true;
                                            txtMonSolCompensar.ClientVisible = true;
                                            txtXgasolinaBlanca.ClientVisible = true;
                                            txtXimp.ClientVisible = true;
                                            txtPreGasolinaBlanca.ClientVisible = true;
                                            txtPreNetoIva.ClientVisible = true;
                                            txtTarifaTransPol.ClientVisible = true;
                                            txtTarifaNetoIva.ClientVisible = true;
                                            txtPpt.ClientVisible = true;
                                            txtIncoterms.ClientVisible = true;
                                            txtFronteraIngreso.ClientVisible = true;
                                            lblDui.ClientVisible = true;
                                            lblMonSolCompensar.ClientVisible = true;
                                            lblXgasolinaBlanca.ClientVisible = true;
                                            lblXimp.ClientVisible = true;
                                            lblPreGasolinaBlanca.ClientVisible = true;
                                            lblPreNetoIva.ClientVisible = true;
                                            lblTarifaTransPol.ClientVisible = true;
                                            lblTarifaNetoIva.ClientVisible = true;
                                            lblPpt.ClientVisible = true;
                                            lblIncoterms.ClientVisible = true;
                                            lblFronteraIngreso.ClientVisible = true;
                                            break;

                                    }

                                    var parametros = txtObsEfImp.Text;

                                    var inIdentificadosActividad = (int)tlsOperadores.FocusedNode.GetValue("IntIdActividad");

                                    switch (inIdentificadosActividad)
                                    {
                                        case 16:
                                        case 36:
                                            if (parametros != "")
                                            {
                                                string[] lista = parametros.Split('|');

                                                if (lista.Any())
                                                {
                                                    if (lista.Count() == 12)
                                                    {
                                                        txtDui.Text = lista[0];
                                                        txtMonSolCompensar.Text = lista[1];
                                                        txtXgasolinaBlanca.Text = ObtenerXgasolinaBlanca(cmbProdEfImp.Text, (int)tlsOperadores.FocusedNode.GetValue("IntIdEntidad")).Replace(".", ",");
                                                        txtXimp.Text = ObtenerXimportacion(cmbProdEfImp.Text, (int)tlsOperadores.FocusedNode.GetValue("IntIdEntidad")).Replace(".", ",");
                                                        txtPreGasolinaBlanca.Text = lista[4];
                                                        txtPreNetoIva.Text = lista[5];
                                                        txtTarifaTransPol.Text = lista[6];
                                                        txtTarifaNetoIva.Text = lista[7];
                                                        txtPpt.Text = lista[8].ToString().Replace(".", ",");
                                                        txtFronteraIngreso.Text = lista[9];
                                                        txtIncoterms.Text = lista[10];
                                                        txtObsEfImp.Text = lista[11];
                                                    }
                                                    else if (lista.Count() == 10)
                                                    {
                                                        txtDui.Text = lista[0];
                                                        txtMonSolCompensar.Text = lista[1];
                                                        txtXgasolinaBlanca.Text = ObtenerXgasolinaBlanca(cmbProdEfImp.Text, (int)tlsOperadores.FocusedNode.GetValue("IntIdEntidad")).Replace(".", ",");
                                                        txtXimp.Text = ObtenerXimportacion(cmbProdEfImp.Text, (int)tlsOperadores.FocusedNode.GetValue("IntIdEntidad")).Replace(".", ",");
                                                        txtPreGasolinaBlanca.Text = lista[4];
                                                        txtPreNetoIva.Text = lista[5];
                                                        txtTarifaTransPol.Text = lista[6];
                                                        txtTarifaNetoIva.Text = lista[7];
                                                        txtPpt.Text = lista[8].ToString().Replace(".", ",");
                                                        txtFronteraIngreso.Text = "";
                                                        txtIncoterms.Text = "";
                                                        txtObsEfImp.Text = lista[9];
                                                    }
                                                    else
                                                    {
                                                        txtDui.Text = "";
                                                        txtMonSolCompensar.Text = "";
                                                        txtXgasolinaBlanca.Text = ObtenerXgasolinaBlanca(cmbProdEfImp.Text, (int)tlsOperadores.FocusedNode.GetValue("IntIdEntidad")).Replace(".", ",");
                                                        txtXimp.Text = ObtenerXimportacion(cmbProdEfImp.Text, (int)tlsOperadores.FocusedNode.GetValue("IntIdEntidad")).Replace(".", ",");
                                                        txtPreGasolinaBlanca.Text = "";
                                                        txtPreNetoIva.Text = "";
                                                        txtTarifaTransPol.Text = "";
                                                        txtTarifaNetoIva.Text = "";
                                                        txtPpt.Text = "0";
                                                        txtFronteraIngreso.Text = "";
                                                        txtIncoterms.Text = "";
                                                        txtObsEfImp.Text = "";
                                                    }
                                                }
                                            }
                                            else
                                            {

                                            }
                                            break;
                                        default:
                                            txtDui.Visible = false;
                                            txtMonSolCompensar.Visible = false;
                                            txtXgasolinaBlanca.Visible = false;
                                            txtXimp.Visible = false;
                                            txtPreGasolinaBlanca.Visible = false;
                                            txtPreNetoIva.Visible = false;
                                            txtTarifaTransPol.Visible = false;
                                            txtTarifaNetoIva.Visible = false;
                                            txtPpt.Visible = false;

                                            lblDui.Visible = false;
                                            lblMonSolCompensar.Visible = false;
                                            lblXgasolinaBlanca.Visible = false;
                                            lblXimp.Visible = false;
                                            lblPreGasolinaBlanca.Visible = false;
                                            lblPreNetoIva.Visible = false;
                                            lblTarifaTransPol.Visible = false;
                                            lblTarifaNetoIva.Visible = false;
                                            lblPpt.Visible = false;
                                            break;

                                    }

                                    var lstResultadosDocImp = new List<O_LISTA_TRANSACCION_IMP_CTY>();

                                    var objTransaccion = _client.Post<EResultadoTransaccion>(CAppSettings.svc_obtener_transaccion,
                                    new
                                    {
                                        decFiltro = "1",
                                        strParametro1 = dgvTransaccionesImp.GetRowValues(index, "ID_MOVIMIENTO").ToString(),
                                        strParametro2 = dgvTransaccionesImp.GetRowValues(index, "ID_FUENTE_ORIGEN").ToString(),
                                        strParametro3 = "0",
                                        strParametro4 = "0",
                                        strParametro5 = "0"
                                    });
                                    if (objTransaccion != null && objTransaccion.intCodigo == 1)
                                    {
                                        List<O_LISTA_TRANSACCION_CTY> lstResultado = objTransaccion.oResultado;
                                        lstResultado.ForEach(a => a.FECHA_HORA = a.FECHA_HORA_EMISION.ToString().Substring(6, 2) + "/" + a.FECHA_HORA_EMISION.ToString().Substring(4, 2) + "/" + a.FECHA_HORA_EMISION.ToString().Substring(0, 4) + " " + a.FECHA_HORA_EMISION.ToString().Substring(8, 2) + ":" + a.FECHA_HORA_EMISION.ToString().Substring(10, 2) + ":" + a.FECHA_HORA_EMISION.ToString().Substring(12, 2));
                                        lstResultado.ForEach(a => a.CANTIDAD = Convert.ToDecimal(dgvTransaccionesImp.GetRowValues(index, "CANTIDAD").ToString()));
                                        lstResultado.ForEach(a => a.ID_PADRE_TRANSACCION = MObtenerParametro(a.ID_TIPO_TRANSACCION));

                                        Session[CVariablesSesion.GrillaAuxiliar] = lstResultado;
                                        dgvDocumentosImp.DataSource = lstResultado;
                                        dgvDocumentosImp.DataBind();
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
                            else
                            {
                                #region nuevo registro

                                //Session.Remove(CVariablesSesion.GrillaAuxiliar);

                                if (Session[CVariablesSesion.ParamAux] != null)
                                {
                                    var strParam = Session[CVariablesSesion.ParamAux].ToString();
                                    var arrParam = strParam.Split('|');

                                    cmbProdEfImp.SelectedItem = cmbProdEfImp.Items.FindByValue(arrParam[1]);

                                    //lmqc--- desagrega las obervaciones en parametricas para importadores

                                    var txtDui = (dgvTransaccionesImp.FindEditFormTemplateControl("txtDui")) as ASPxTextBox;
                                    var txtMonSolCompensar = (dgvTransaccionesImp.FindEditFormTemplateControl("txtMonSolCompensar")) as ASPxTextBox;
                                    var txtXgasolinaBlanca = (dgvTransaccionesImp.FindEditFormTemplateControl("txtXgasolinaBlanca")) as ASPxTextBox;
                                    var txtXimp = (dgvTransaccionesImp.FindEditFormTemplateControl("txtXimp")) as ASPxTextBox;
                                    var txtPreGasolinaBlanca = (dgvTransaccionesImp.FindEditFormTemplateControl("txtPreGasolinaBlanca")) as ASPxTextBox;
                                    var txtPreNetoIva = (dgvTransaccionesImp.FindEditFormTemplateControl("txtPreNetoIva")) as ASPxTextBox;
                                    var txtTarifaTransPol = (dgvTransaccionesImp.FindEditFormTemplateControl("txtTarifaTransPol")) as ASPxTextBox;
                                    var txtTarifaNetoIva = (dgvTransaccionesImp.FindEditFormTemplateControl("txtTarifaNetoIva")) as ASPxTextBox;
                                    var txtPpt = (dgvTransaccionesImp.FindEditFormTemplateControl("txtPpt")) as ASPxTextBox;

                                    var txtIncoterms = (dgvTransaccionesImp.FindEditFormTemplateControl("txtIncoterms")) as ASPxTextBox;
                                    var txtFronteraIngreso = (dgvTransaccionesImp.FindEditFormTemplateControl("txtFronteraIngreso")) as ASPxTextBox;

                                    var lblDui = (dgvTransaccionesImp.FindEditFormTemplateControl("lblDui")) as ASPxLabel;
                                    var lblMonSolCompensar = (dgvTransaccionesImp.FindEditFormTemplateControl("lblMonSolCompensar")) as ASPxLabel;
                                    var lblXgasolinaBlanca = (dgvTransaccionesImp.FindEditFormTemplateControl("lblXgasolinaBlanca")) as ASPxLabel;
                                    var lblXimp = (dgvTransaccionesImp.FindEditFormTemplateControl("lblXimp")) as ASPxLabel;
                                    var lblPreGasolinaBlanca = (dgvTransaccionesImp.FindEditFormTemplateControl("lblPreGasolinaBlanca")) as ASPxLabel;
                                    var lblPreNetoIva = (dgvTransaccionesImp.FindEditFormTemplateControl("lblPreNetoIva")) as ASPxLabel;
                                    var lblTarifaTransPol = (dgvTransaccionesImp.FindEditFormTemplateControl("lblTarifaTransPol")) as ASPxLabel;
                                    var lblTarifaNetoIva = (dgvTransaccionesImp.FindEditFormTemplateControl("lblTarifaNetoIva")) as ASPxLabel;
                                    var lblPpt = (dgvTransaccionesImp.FindEditFormTemplateControl("lblPpt")) as ASPxLabel;
                                    var lblIncoterms = (dgvTransaccionesImp.FindEditFormTemplateControl("lblIncoterms")) as ASPxLabel;
                                    var lblFronteraIngreso = (dgvTransaccionesImp.FindEditFormTemplateControl("lblFronteraIngreso")) as ASPxLabel;

                                    int intIdProducto = Convert.ToInt32(arrParam[1].ToString());

                                    switch (intIdProducto)
                                    {
                                        case 278:
                                        case 401://test 401 desarrollo 4326 produccion 1829
                                        case 402://test 402 desarrollo 4327 produccion 1830
                                        case 403://test 403 desarrollo 4328 produccion 1831
                                        case 404://test 404 desarrollo 4329 produccion 1832
                                        case 405://test 405 desarrollo 4330 produccion 1833
                                            txtDui.ClientVisible = true;
                                            txtMonSolCompensar.ClientVisible = false;
                                            txtXgasolinaBlanca.ClientVisible = false;
                                            txtXimp.ClientVisible = false;
                                            txtPreGasolinaBlanca.ClientVisible = false;
                                            txtPreNetoIva.ClientVisible = false;
                                            txtTarifaTransPol.ClientVisible = false;
                                            txtTarifaNetoIva.ClientVisible = false;
                                            txtPpt.ClientVisible = true;
                                            txtIncoterms.ClientVisible = false;
                                            txtFronteraIngreso.ClientVisible = false;
                                            lblDui.ClientVisible = true;
                                            lblMonSolCompensar.ClientVisible = false;
                                            lblXgasolinaBlanca.ClientVisible = false;
                                            lblXimp.ClientVisible = false;
                                            lblPreGasolinaBlanca.ClientVisible = false;
                                            lblPreNetoIva.ClientVisible = false;
                                            lblTarifaTransPol.ClientVisible = false;
                                            lblTarifaNetoIva.ClientVisible = false;
                                            lblPpt.ClientVisible = true;
                                            lblIncoterms.ClientVisible = false;
                                            lblFronteraIngreso.ClientVisible = false;
                                            break;
                                        default:
                                            txtDui.ClientVisible = true;
                                            txtMonSolCompensar.ClientVisible = true;
                                            txtXgasolinaBlanca.ClientVisible = true;
                                            txtXimp.ClientVisible = true;
                                            txtPreGasolinaBlanca.ClientVisible = true;
                                            txtPreNetoIva.ClientVisible = true;
                                            txtTarifaTransPol.ClientVisible = true;
                                            txtTarifaNetoIva.ClientVisible = true;
                                            txtPpt.ClientVisible = true;
                                            txtIncoterms.ClientVisible = true;
                                            txtFronteraIngreso.ClientVisible = true;
                                            lblDui.ClientVisible = true;
                                            lblMonSolCompensar.ClientVisible = true;
                                            lblXgasolinaBlanca.ClientVisible = true;
                                            lblXimp.ClientVisible = true;
                                            lblPreGasolinaBlanca.ClientVisible = true;
                                            lblPreNetoIva.ClientVisible = true;
                                            lblTarifaTransPol.ClientVisible = true;
                                            lblTarifaNetoIva.ClientVisible = true;
                                            lblPpt.ClientVisible = true;
                                            lblIncoterms.ClientVisible = true;
                                            lblFronteraIngreso.ClientVisible = true;
                                            break;

                                    }
                                    //rgcg -- oculta campos para imortadores de lubricantes
                                    var o2 = tlsOperadores.FocusedNode.GetValue("strActividad").ToString();
                                    if (o2 == "IMPORTACIÓN DE LUBRICANTES")
                                    {
                                        txtXgasolinaBlanca.Visible = false;
                                        lblXgasolinaBlanca.Visible = false;


                                        lblPreGasolinaBlanca.Visible = false;
                                        txtPreGasolinaBlanca.Visible = false;

                                        lblTarifaTransPol.Visible = false;
                                        txtTarifaTransPol.Visible = false;

                                        lblMonSolCompensar.Visible = false;
                                        txtMonSolCompensar.Visible = false;

                                        lblXimp.Visible = false;
                                        txtXimp.Visible = false;

                                        lblPreNetoIva.Visible = false;
                                        txtPreNetoIva.Visible = false;

                                        lblTarifaTransPol.Visible = false;
                                        txtTarifaTransPol.Visible = false;

                                        lblIncoterms.Visible = false;
                                        txtIncoterms.Visible = false;

                                        lblPpt.Visible = false;
                                        txtPpt.Visible = false;

                                        lblFronteraIngreso.Visible = false;
                                        txtFronteraIngreso.Visible = false;

                                    }
                                    else
                                    {
                                        txtXgasolinaBlanca.Visible = true;
                                        lblXgasolinaBlanca.Visible = true;

                                        lblPreGasolinaBlanca.Visible = true;
                                        txtPreGasolinaBlanca.Visible = true;

                                        lblTarifaTransPol.Visible = true;
                                        txtTarifaTransPol.Visible = true;

                                        lblMonSolCompensar.Visible = true;
                                        txtMonSolCompensar.Visible = true;

                                        lblXimp.Visible = true;
                                        txtXimp.Visible = true;

                                        lblPreNetoIva.Visible = true;
                                        txtPreNetoIva.Visible = true;

                                        lblTarifaTransPol.Visible = true;
                                        txtTarifaTransPol.Visible = true;

                                        lblIncoterms.Visible = true;
                                        txtIncoterms.Visible = true;

                                        lblPpt.Visible = true;
                                        txtPpt.Visible = true;

                                        lblFronteraIngreso.Visible = true;
                                        txtFronteraIngreso.Visible = true;
                                    }
                                }

                                #endregion
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
            }
            else
            {
                //todo No implementado.
            }
        }

        private string TipoFactura(string Obs)
        {
            string strTipoFactura = "";

            if (Obs.StartsWith("ACCP"))
            {
                strTipoFactura = "Factura Producto";
            }
            else if (Obs.StartsWith("B1CCTF"))
            {
                strTipoFactura = "Factura Transporte Fluvial";
            }
            else if (Obs.StartsWith("B2CCTT"))
            {
                strTipoFactura = "Factura Transporte Terrestre";
            }
            else if (Obs.StartsWith("B3CCTF"))
            {
                strTipoFactura = "Factura Transporte Ferreo";
            }
            else if (Obs.StartsWith("CCS"))
            {
                strTipoFactura = "Factura Seguro";
            }
            else if (Obs.StartsWith("D1IO"))
            {
                strTipoFactura = "Factura Inspeccion Origen";
            }
            else if (Obs.StartsWith("D2II"))
            {
                strTipoFactura = "Factura Inspeccion Intermedia";
            }
            else if (Obs.StartsWith("D3IDF"))
            {
                strTipoFactura = "Factura Inspeccion Final";
            }
            else if (Obs.StartsWith("ECC"))
            {
                strTipoFactura = "Factura Aduanera";
            }
            else if (Obs.StartsWith("F1PD"))
            {
                strTipoFactura = "Factura Financiero Producto";
            }
            else if (Obs.StartsWith("F2PS"))
            {
                strTipoFactura = "Factura Financiero Servicio";
            }
            else if (Obs.StartsWith("G1AI"))
            {
                strTipoFactura = "Factura Almacenaje Intermedio";
            }
            else if (Obs.StartsWith("G2ADF"))
            {
                strTipoFactura = "Factura Almacenaje Final";
            }
            else if (Obs.StartsWith("HCM"))
            {
                strTipoFactura = "Factura Monitoreo";
            }
            else if (Obs.StartsWith("I1DI"))
            {
                strTipoFactura = "Volumen Destino Intermedio";
            }
            else if (Obs.StartsWith("I2DF"))
            {
                strTipoFactura = "Volumen Destino Final";
            }
            else
            {
                strTipoFactura = "No identificado";
            }

            return strTipoFactura;

        }

        private string ObtenerXgasolinaBlanca(string ron, int entidad)
        {
            decimal decXgasolinaBlanca = 0;

            if (entidad == 139)
            {
                switch (ron)
                {
                    case "GRON 85":
                        decXgasolinaBlanca = 0;
                        break;
                    case "GRON 86":
                        decXgasolinaBlanca = 5;
                        break;
                    case "GRON 87":
                        decXgasolinaBlanca = 7;
                        break;
                    case "GRON 88":
                        decXgasolinaBlanca = 1;
                        break;
                    case "GRON 89":
                        decXgasolinaBlanca = 12;
                        break;
                    case "GRON 90":
                        decXgasolinaBlanca = 15;
                        break;
                    case "GRON 91":
                    case "GRON 92":
                        decXgasolinaBlanca = 20;
                        break;
                    case "GRON 93":
                        decXgasolinaBlanca = 25;
                        break;
                    case "GRON 94":
                        decXgasolinaBlanca = 24;
                        break;
                    case "GRON 95":
                        decXgasolinaBlanca = 27;
                        break;
                    case "GRON 96":
                        decXgasolinaBlanca = 29;
                        break;
                    case "GRON 97":
                        decXgasolinaBlanca = 30;
                        break;
                    case "GRON 98":
                        decXgasolinaBlanca = 34;
                        break;
                    case "GRON 99":
                        decXgasolinaBlanca = 35;
                        break;
                    case "GRON 100":
                        decXgasolinaBlanca = 36;
                        break;
                    default:
                        decXgasolinaBlanca = 0;
                        break;
                }
            }

            if (entidad == 10)
            {
                switch (ron)
                {
                    case "GRON 85":
                        decXgasolinaBlanca = Convert.ToDecimal("0");
                        break;
                    case "GRON 86":
                        decXgasolinaBlanca = Convert.ToDecimal("0.20");
                        break;
                    case "GRON 87":
                        decXgasolinaBlanca = Convert.ToDecimal("0.40");
                        break;
                    case "GRON 88":
                        decXgasolinaBlanca = Convert.ToDecimal("5.90");
                        break;
                    case "GRON 89":
                        decXgasolinaBlanca = Convert.ToDecimal("10.80");
                        break;
                    case "GRON 90":
                        decXgasolinaBlanca = Convert.ToDecimal("10.30");
                        break;
                    case "GRON 91":
                        decXgasolinaBlanca = Convert.ToDecimal("14.60");
                        break;
                    case "GRON 92":
                        decXgasolinaBlanca = Convert.ToDecimal("18.50");
                        break;
                    case "GRON 93":
                        decXgasolinaBlanca = Convert.ToDecimal("22");
                        break;
                    case "GRON 94":
                        decXgasolinaBlanca = Convert.ToDecimal("25.20");
                        break;
                    case "GRON 95":
                        decXgasolinaBlanca = Convert.ToDecimal("23.30");
                        break;
                    case "GRON 96":
                        decXgasolinaBlanca = Convert.ToDecimal("38.70");
                        break;
                    case "GRON 97":
                        decXgasolinaBlanca = Convert.ToDecimal("41");
                        break;
                    case "GRON 98":
                        decXgasolinaBlanca = Convert.ToDecimal("43.10");
                        break;
                    case "GRON 99":
                        decXgasolinaBlanca = Convert.ToDecimal("45.10");
                        break;
                    case "GRON 100":
                        decXgasolinaBlanca = Convert.ToDecimal("46.90");
                        break;
                    default:
                        decXgasolinaBlanca = 0;
                        break;
                }
            }

            return decXgasolinaBlanca.ToString();
        }

        private string ObtenerXimportacion(string ron, int entidad)
        {
            decimal decXgasolinaBlanca = 0;

            if (entidad == 139)
            {
                switch (ron)
                {
                    case "GRON 85":
                        decXgasolinaBlanca = 100;
                        break;
                    case "GRON 86":
                        decXgasolinaBlanca = 95;
                        break;
                    case "GRON 87":
                        decXgasolinaBlanca = 93;
                        break;
                    case "GRON 88":
                        decXgasolinaBlanca = 90;
                        break;
                    case "GRON 89":
                        decXgasolinaBlanca = 88;
                        break;
                    case "GRON 90":
                        decXgasolinaBlanca = 85;
                        break;
                    case "GRON 91":
                    case "GRON 92":
                        decXgasolinaBlanca = 80;
                        break;
                    case "GRON 93":
                        decXgasolinaBlanca = 75;
                        break;
                    case "GRON 94":
                        decXgasolinaBlanca = 76;
                        break;
                    case "GRON 95":
                        decXgasolinaBlanca = 73;
                        break;
                    case "GRON 96":
                        decXgasolinaBlanca = 71;
                        break;
                    case "GRON 97":
                        decXgasolinaBlanca = 70;
                        break;
                    case "GRON 98":
                        decXgasolinaBlanca = 66;
                        break;
                    case "GRON 99":
                        decXgasolinaBlanca = 65;
                        break;
                    case "GRON 100":
                        decXgasolinaBlanca = 64;
                        break;
                    default:
                        decXgasolinaBlanca = 0;
                        break;
                }
            }

            if (entidad == 10)
            {
                switch (ron)
                {
                    case "GRON 85":
                        decXgasolinaBlanca = Convert.ToDecimal("100");
                        break;
                    case "GRON 86":
                        decXgasolinaBlanca = Convert.ToDecimal("99.80");
                        break;
                    case "GRON 87":
                        decXgasolinaBlanca = Convert.ToDecimal("99.60");
                        break;
                    case "GRON 88":
                        decXgasolinaBlanca = Convert.ToDecimal("94.10");
                        break;
                    case "GRON 89":
                        decXgasolinaBlanca = Convert.ToDecimal("89.20");
                        break;
                    case "GRON 90":
                        decXgasolinaBlanca = Convert.ToDecimal("89.70");
                        break;
                    case "GRON 91":
                        decXgasolinaBlanca = Convert.ToDecimal("85.40");
                        break;
                    case "GRON 92":
                        decXgasolinaBlanca = Convert.ToDecimal("81.50");
                        break;
                    case "GRON 93":
                        decXgasolinaBlanca = Convert.ToDecimal("78.00");
                        break;
                    case "GRON 94":
                        decXgasolinaBlanca = Convert.ToDecimal("74.80");
                        break;
                    case "GRON 95":
                        decXgasolinaBlanca = Convert.ToDecimal("63.70");
                        break;
                    case "GRON 96":
                        decXgasolinaBlanca = Convert.ToDecimal("61.30");
                        break;
                    case "GRON 97":
                        decXgasolinaBlanca = Convert.ToDecimal("5.00");
                        break;
                    case "GRON 98":
                        decXgasolinaBlanca = Convert.ToDecimal("56.90");
                        break;
                    case "GRON 99":
                        decXgasolinaBlanca = Convert.ToDecimal("54.90");
                        break;
                    case "GRON 100":
                        decXgasolinaBlanca = Convert.ToDecimal("53.10");
                        break;
                    default:
                        decXgasolinaBlanca = 0;
                        break;
                }
            }

            return decXgasolinaBlanca.ToString();
        }

        protected void dgvTransaccionesImp_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    if (cmbTipoOperacionImp.Text == "")
                    {
                        e.Cancel = true;
                        dgvTransaccionesImp.CancelEdit();
                        return;
                    }

                    var strTipoHab = (Session[CVariablesSesion.HabilitaBotones] == null) ? "ND" : Session[CVariablesSesion.HabilitaBotones].ToString();
                    if (strTipoHab != "MODSI")
                    {
                        e.Cancel = true;
                        dgvTransaccionesImp.CancelEdit();
                        return;
                    }

                    if ((int)tlsOperadores.FocusedNode.GetValue("IntAccion") > 0)
                    {
                        #region Instancia componentes
                        ASPxDateEdit detFechaEf = (dgvTransaccionesImp.FindEditFormTemplateControl("detFechaEfImp")) as ASPxDateEdit;
                        ASPxComboBox cmbEstadoEf = (dgvTransaccionesImp.FindEditFormTemplateControl("cmbEstadoEfImp")) as ASPxComboBox;
                        ASPxComboBox cmbProdEf = (dgvTransaccionesImp.FindEditFormTemplateControl("cmbProdEfImp")) as ASPxComboBox;
                        ASPxComboBox cmbUnidadEf = (dgvTransaccionesImp.FindEditFormTemplateControl("cmbUnidadEfImp")) as ASPxComboBox;
                        ASPxTextBox txtCantidadEf = (dgvTransaccionesImp.FindEditFormTemplateControl("txtCantidadEfImp")) as ASPxTextBox;
                        ASPxTextBox txtObsEf = (dgvTransaccionesImp.FindEditFormTemplateControl("txtObsEfImp")) as ASPxTextBox;
                        ASPxTextBox txtCodigoEf = (dgvTransaccionesImp.FindEditFormTemplateControl("txtCodigoEfImp")) as ASPxTextBox;
                        ASPxTextBox txtBarCodeEf = (dgvTransaccionesImp.FindEditFormTemplateControl("txtBarCodeEfImp")) as ASPxTextBox;

                        //lmqc---
                        ASPxTextBox txtDui = (dgvTransaccionesImp.FindEditFormTemplateControl("txtDui")) as ASPxTextBox;
                        ASPxTextBox txtMonSolCompensar = (dgvTransaccionesImp.FindEditFormTemplateControl("txtMonSolCompensar")) as ASPxTextBox;
                        ASPxTextBox txtXgasolinaBlanca = (dgvTransaccionesImp.FindEditFormTemplateControl("txtXgasolinaBlanca")) as ASPxTextBox;
                        ASPxTextBox txtXimp = (dgvTransaccionesImp.FindEditFormTemplateControl("txtXimp")) as ASPxTextBox;
                        ASPxTextBox txtPreGasolinaBlanca = (dgvTransaccionesImp.FindEditFormTemplateControl("txtPreGasolinaBlanca")) as ASPxTextBox;
                        ASPxTextBox txtPreNetoIva = (dgvTransaccionesImp.FindEditFormTemplateControl("txtPreNetoIva")) as ASPxTextBox;
                        ASPxTextBox txtTarifaTransPol = (dgvTransaccionesImp.FindEditFormTemplateControl("txtTarifaTransPol")) as ASPxTextBox;
                        ASPxTextBox txtTarifaNetoIva = (dgvTransaccionesImp.FindEditFormTemplateControl("txtTarifaNetoIva")) as ASPxTextBox;
                        ASPxTextBox txtPpt = (dgvTransaccionesImp.FindEditFormTemplateControl("txtPpt")) as ASPxTextBox;
                        ASPxTextBox txtIncoterms = (dgvTransaccionesImp.FindEditFormTemplateControl("txtIncoterms")) as ASPxTextBox;
                        ASPxTextBox txtFronteraIngreso = (dgvTransaccionesImp.FindEditFormTemplateControl("txtFronteraIngreso")) as ASPxTextBox;

                        #endregion

                        try
                        {
                            var datFechaDoc = DateTime.Parse(detFechaEf.Text);
                            var datFechaReg = DateTime.Now;
                            var objNuevoReg = new ObjMovimientoTransaccion();

                            objNuevoReg.idMovimiento = 0;
                            objNuevoReg.idMovimientoPadre = 0;
                            objNuevoReg.idTipoActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                            objNuevoReg.idTipoOperacion = Convert.ToDecimal(cmbTipoOperacionImp.Value);
                            objNuevoReg.idConsumidor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString());
                            objNuevoReg.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                            objNuevoReg.fechaRegistroMovimiento = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                            objNuevoReg.fechaOperacion = Convert.ToDecimal(datFechaDoc.Year + datFechaDoc.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaDoc.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaDoc.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaDoc.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaDoc.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                            objNuevoReg.idTipoMovimientoFlujo = 20;
                            objNuevoReg.idTipoMovimiento = 19;
                            objNuevoReg.idEstadoMovimiento = Convert.ToDecimal(cmbEstadoEf.Value);
                            objNuevoReg.idFuenteOrigenDato = (decimal)EnumTipoRegistro.OrigenWeb;
                            objNuevoReg.idUbicacionOperacion = 120;
                            objNuevoReg.idUnidadMedidaHidrocarburo = Convert.ToDecimal(cmbUnidadEf.Value);
                            objNuevoReg.idHidrocarburo = Convert.ToDecimal(cmbProdEf.Value);

                            // objNuevoReg.cantidadHidrocarburo = Convert.ToDecimal(txtCantidadEf.Text.Replace(",", "."));

                            objNuevoReg.identificador = "";
                            objNuevoReg.subIdentificador = "";
                            if (Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad")) == 16)//lmqc-- para importadores
                            {
                                objNuevoReg.observacion = txtDui.Text + '|' + txtMonSolCompensar.Text + '|' + txtXgasolinaBlanca.Text.Replace(",", ".") + '|' + txtXimp.Text.Replace(",", ".") + '|' + txtPreGasolinaBlanca.Text.Replace(",", ".") + '|' + txtPreNetoIva.Text.Replace(",", ".") + '|' + txtTarifaTransPol.Text.Replace(",", ".") + '|' + txtTarifaNetoIva.Text.Replace(",", ".") + '|' + txtPpt.Text.Replace(",", ".") + '|' + txtFronteraIngreso.Text + '|' + txtIncoterms.Text + '|' + txtObsEf.Text.Trim();
                            }
                            else if (Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad")) == 36)//lmqc-- para importadores
                            {
                                objNuevoReg.observacion = txtDui.Text + '|' + txtMonSolCompensar.Text + '|' + txtXgasolinaBlanca.Text.Replace(",", ".") + '|' + txtXimp.Text.Replace(",", ".") + '|' + txtPreGasolinaBlanca.Text.Replace(",", ".") + '|' + txtPreNetoIva.Text.Replace(",", ".") + '|' + txtTarifaTransPol.Text.Replace(",", ".") + '|' + txtTarifaNetoIva.Text.Replace(",", ".") + '|' + txtPpt.Text.Replace(",", ".") + '|' + txtFronteraIngreso.Text + '|' + txtIncoterms.Text + '|' + txtObsEf.Text.Trim();
                            }
                            else
                            {
                                objNuevoReg.observacion = txtObsEf.Text.Trim();
                            }
                            objNuevoReg.barcode = 0;

                            objNuevoReg.idTipoMedicion = -99999;
                            objNuevoReg.cantidadAguaLitros = 0;

                            objNuevoReg.idTipoMedioTransporte = -99999;
                            objNuevoReg.idMedioTransporte = -99999;

                            objNuevoReg.tipoInfraestructura = -99999;
                            objNuevoReg.idInfraestructura = -99999;

                            objNuevoReg.idUnidadEnvasesTransporte = -99999;
                            objNuevoReg.codigo = "";

                            objNuevoReg.idIntegracion = 0;
                            objNuevoReg.idUnidadTemperatura = -99999;
                            objNuevoReg.temperatura = -99999;
                            objNuevoReg.gradoApi = "";
                            objNuevoReg.idEntidad = -99999;

                            objNuevoReg.cantidadEnvasesTransporte = 0;
                            objNuevoReg.binarioControl = "00000-0000000000-0000000000-00000";
                            objNuevoReg.idTransaccionBaseExterna = "";

                            objNuevoReg.idTipoConsumidor = -99999;

                            objNuevoReg.jsonReferencia = "";
                            objNuevoReg.estadoJsonRef = -99999;
                            objNuevoReg.idCuenta = -99999;
                            objNuevoReg.idCuentaAux = -99999;
                            objNuevoReg.idParamGlobal = -99999;
                            objNuevoReg.idParamEspec = -99999;
                            objNuevoReg.idTipoInfraAux = -99999;
                            objNuevoReg.idInfraAux = -99999;

                            objNuevoReg.listaTransaccion = new List<ObjTransaccion>();

                            decimal decCantidadProducto = Convert.ToDecimal(txtCantidadEf.Value);

                            List<O_LISTA_TRANSACCION_CTY> lstDocumentosMov;
                            lstDocumentosMov = (List<O_LISTA_TRANSACCION_CTY>)Session[CVariablesSesion.GrillaAuxiliar];

                            if (lstDocumentosMov != null)
                            {
                                lstDocumentosMov = lstDocumentosMov.OrderBy(x => x.ID_TIPO_TRANSACCION).ToList();
                                foreach (var reg in lstDocumentosMov)
                                {
                                    var objTrasaccion = new ObjTransaccion();
                                    objTrasaccion.idMovimiento = 0;
                                    objTrasaccion.idTransaccion = 0;
                                    objTrasaccion.idTransaccionPadre = 0;
                                    objTrasaccion.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                    objTrasaccion.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                    objTrasaccion.idTipoTransaccion = Convert.ToDecimal(reg.ID_TIPO_TRANSACCION);
                                    objTrasaccion.fechaEmision = reg.FECHA_HORA_EMISION;
                                    objTrasaccion.numeroTransaccion = reg.NRO_TRANSACCION.Trim();
                                    objTrasaccion.numeroAutorizacion = Convert.ToDecimal(reg.NRO_AUTORIZACION);
                                    objTrasaccion.codigoControl = reg.CODIGO_CONTROL;
                                    objTrasaccion.nitConsumidor = reg.NIT_CONSUMIDOR;
                                    objTrasaccion.razonSocial = reg.RAZON_SOCIAL;
                                    objTrasaccion.precioUnitario = Convert.ToDecimal(reg.PRECIO_UNITARIO.ToString().Replace(",", "."));
                                    objTrasaccion.precioTotal = Convert.ToDecimal(reg.PRECIO_TOTAL.ToString().Replace(",", "."));
                                    objTrasaccion.usuarioTransaccion = reg.USUARIO_TRANSACCION;
                                    objTrasaccion.observaciones = reg.OBSERVACIONES;
                                    objTrasaccion.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                    objTrasaccion.idEstadoTransaccion = objNuevoReg.idEstadoMovimiento;
                                    objTrasaccion.idTipoCreditoFiscal = Convert.ToDecimal(reg.ID_TIPO_CREDITO_FISCAL);
                                    objTrasaccion.idFormaPago = Convert.ToDecimal(reg.ID_FORMA_PAGO);
                                    objTrasaccion.idTipoMoneda = Convert.ToDecimal(reg.ID_TIPO_MONEDA);
                                    objTrasaccion.idTipoEmisor = Convert.ToDecimal(reg.ID_TIPO_EMISOR);
                                    objTrasaccion.codigoSeguridad = 0;
                                    objTrasaccion.idAutorizacionDato = 0;
                                    objTrasaccion.binarioControl = "0000000000";

                                    objTrasaccion.fechaOperacion = CFechas.ConvierteDateTimeLong(datFechaDoc);
                                    objTrasaccion.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                    objTrasaccion.codigo = cmbProdEf.Text;

                                    if (reg.OBSERVACIONES.Substring(0, 4) == "ACCP")
                                    {
                                        string[] listaParametros = reg.OBSERVACIONES.Split('|');

                                        decCantidadProducto = decCantidadProducto + Convert.ToDecimal(listaParametros[5]);
                                    }

                                    objNuevoReg.listaTransaccion.Add(objTrasaccion);
                                }
                            }

                            objNuevoReg.cantidadHidrocarburo = decCantidadProducto;

                            var objRegistro = new ERegistraMovimientoTransaccion();
                            objRegistro.strCredencial = CParametrosHydro.strCredencialFuncionario;
                            objRegistro.decFiltro = 1;
                            var lstRegistros = new List<ObjMovimientoTransaccion>();
                            lstRegistros.Add(objNuevoReg);
                            objRegistro.listaMovimientoTransaccion = lstRegistros;

                            //var ojo = JsonSerializer.SerializeToString(objRegistro);
                            var resultado = _client.Post<EResultado>(CAppSettings.svc_registra_movimiento_transaccion, objRegistro);
                            if (resultado.intCodigo == 1)
                            {
                                e.Cancel = true;
                                Session.Remove(CVariablesSesion.AuxActualiza);
                                dgvTransaccionesImp.CancelEdit();
                                dgvTransaccionesImp_CustomCallback(null, null);

                                //#region guardar condiciones de uso
                                ///* guarda condiciones de uso Lubricantes y carburante*/
                                //var strParametro1 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString();

                                //if ((Convert.ToInt32(strParametro1) == (int)EnumActividades.ImportacionCarburantes) || (Convert.ToInt32(strParametro1) == (int)EnumActividades.ImportacionLubricantes))
                                //{

                                //    decimal decFecha = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString().PadLeft(2, '0') + datFechaReg.Day.ToString().PadLeft(2, '0') + datFechaReg.Hour.ToString().PadLeft(2, '0') + datFechaReg.Minute.ToString().PadLeft(2, '0') + datFechaReg.Second.ToString().PadLeft(2, '0'));
                                //    /*
                                //    var objResultado = _client.Post<EResultado>(CAppSettings.svc_elimina_autorizacion_condicion,
                                //    new
                                //    {
                                //        strCredencial = CParametrosHydro.strCredencialFuncionario,
                                //        decFiltro = 2,
                                //        decIdTipoActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()),
                                //        decIdConsumidor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString()),
                                //        decIdIdentificador = 0,
                                //        decAppFechaRegistro = decFecha,
                                //        decAppIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario])
                                //    });*/


                                //    //if (objResultado.intCodigo == 1)
                                //   // {
                                //       // foreach (var reg in lstFinalHi)
                                //       // {
                                //    //    MGrabaCondicion(Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()), Convert.ToDecimal(Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString())), 1, Convert.ToDecimal(cmbProdEf.Value), Convert.ToDecimal(cmbUnidadEf.Value));
                                //      //  }

                                //   // }

                                //}
                                //#endregion

                            }
                            else
                            {
                                MImprimirMensaje(resultado.strMensaje, EnumTiposMensajeAlerta.Alerta);
                            }
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
                        e.Cancel = true;
                        dgvTransacciones.CancelEdit();
                    }

                    #endregion
                }
                catch (Exception exp)
                {
                    var argEx = new ArgumentException(exp.Message);
                    throw argEx;
                }
            }
            else
            {
                //todo No implementado.
            }
        }

        private void MGrabaCondicion(decimal decIdTipoActividad, decimal decIdConsumidor, decimal tipo, decimal producto, decimal unidad)
        {
            var datFechaReg = DateTime.Now;
            decimal decFecha = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString().PadLeft(2, '0') + datFechaReg.Day.ToString().PadLeft(2, '0') + datFechaReg.Hour.ToString().PadLeft(2, '0') + datFechaReg.Minute.ToString().PadLeft(2, '0') + datFechaReg.Second.ToString().PadLeft(2, '0'));




            var objResultado = _client.Post<EResultado>(CAppSettings.svc_registra_autorizacion_condicion,
            new
            {
                strCredencial = CParametrosHydro.strCredencialFuncionario,
                decIdTipoActividad,
                decIdConsumidor,
                decIdIdentificador = producto,
                decIdSubIdentificador = unidad,
                decEstado = 0,
                strObservaciones = "",
                decTipoCondicion = tipo,
                strCodigo = "",
                strDescripcion = "",
                decAppFechaRegistro = decFecha,
                decAppIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario])
            });

        }


        protected void dgvTransaccionesImp_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    var strTipoHab = (Session[CVariablesSesion.HabilitaBotones] == null) ? "ND" : Session[CVariablesSesion.HabilitaBotones].ToString();
                    if (strTipoHab != "MODSI")
                    {
                        e.Cancel = true;
                        dgvTransacciones.CancelEdit();
                        return;
                    }

                    if ((int)tlsOperadores.FocusedNode.GetValue("IntAccion") > 0)
                    {
                        EResultado resultado;
                        #region Instancia componentes
                        ASPxDateEdit detFechaEf = (dgvTransaccionesImp.FindEditFormTemplateControl("detFechaEfImp")) as ASPxDateEdit;
                        ASPxComboBox cmbEstadoEf = (dgvTransaccionesImp.FindEditFormTemplateControl("cmbEstadoEfImp")) as ASPxComboBox;
                        ASPxComboBox cmbProdEf = (dgvTransaccionesImp.FindEditFormTemplateControl("cmbProdEfImp")) as ASPxComboBox;
                        ASPxComboBox cmbUnidadEf = (dgvTransaccionesImp.FindEditFormTemplateControl("cmbUnidadEfImp")) as ASPxComboBox;

                        ASPxTextBox txtCantidadEf = (dgvTransaccionesImp.FindEditFormTemplateControl("txtCantidadEfImp")) as ASPxTextBox;
                        ASPxTextBox txtObsEf = (dgvTransaccionesImp.FindEditFormTemplateControl("txtObsEfImp")) as ASPxTextBox;
                        ASPxTextBox txtCodigoEf = (dgvTransaccionesImp.FindEditFormTemplateControl("txtCodigoEfImp")) as ASPxTextBox;
                        ASPxTextBox txtBarCodeEf = (dgvTransaccionesImp.FindEditFormTemplateControl("txtBarCodeEfImp")) as ASPxTextBox;

                        ASPxTextBox txtIdEf = (dgvTransaccionesImp.FindEditFormTemplateControl("txtIdEfImp")) as ASPxTextBox;

                        //lmqc---
                        ASPxTextBox txtDui = (dgvTransaccionesImp.FindEditFormTemplateControl("txtDui")) as ASPxTextBox;
                        ASPxTextBox txtMonSolCompensar = (dgvTransaccionesImp.FindEditFormTemplateControl("txtMonSolCompensar")) as ASPxTextBox;
                        ASPxTextBox txtXgasolinaBlanca = (dgvTransaccionesImp.FindEditFormTemplateControl("txtXgasolinaBlanca")) as ASPxTextBox;
                        ASPxTextBox txtXimp = (dgvTransaccionesImp.FindEditFormTemplateControl("txtXimp")) as ASPxTextBox;
                        ASPxTextBox txtPreGasolinaBlanca = (dgvTransaccionesImp.FindEditFormTemplateControl("txtPreGasolinaBlanca")) as ASPxTextBox;
                        ASPxTextBox txtPreNetoIva = (dgvTransaccionesImp.FindEditFormTemplateControl("txtPreNetoIva")) as ASPxTextBox;
                        ASPxTextBox txtTarifaTransPol = (dgvTransaccionesImp.FindEditFormTemplateControl("txtTarifaTransPol")) as ASPxTextBox;
                        ASPxTextBox txtTarifaNetoIva = (dgvTransaccionesImp.FindEditFormTemplateControl("txtTarifaNetoIva")) as ASPxTextBox;
                        ASPxTextBox txtPpt = (dgvTransaccionesImp.FindEditFormTemplateControl("txtPpt")) as ASPxTextBox;

                        ASPxTextBox txtIncoterms = (dgvTransaccionesImp.FindEditFormTemplateControl("txtIncoterms")) as ASPxTextBox;
                        ASPxTextBox txtFronteraIngreso = (dgvTransaccionesImp.FindEditFormTemplateControl("txtFronteraIngreso")) as ASPxTextBox;

                        var cmbTipoDespachoEf = (dgvTransaccionesImp.FindEditFormTemplateControl("cmbTipoDespachoEf")) as ASPxComboBox;

                        #endregion
                        try
                        {
                            var datFechaDoc = DateTime.Parse(detFechaEf.Text);
                            var datFechaReg = DateTime.Now;
                            var objNuevoReg = new ObjMovimientoTransaccion();

                            objNuevoReg.idMovimiento = Convert.ToDecimal(txtIdEf.Text);
                            objNuevoReg.idMovimientoPadre = 0;
                            objNuevoReg.idTipoActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                            objNuevoReg.idTipoOperacion = Convert.ToDecimal(cmbTipoOperacionImp.Value);
                            objNuevoReg.idConsumidor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString());
                            objNuevoReg.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                            objNuevoReg.fechaRegistroMovimiento = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                            objNuevoReg.fechaOperacion = Convert.ToDecimal(datFechaDoc.Year + datFechaDoc.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaDoc.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaDoc.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaDoc.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaDoc.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                            objNuevoReg.idTipoMovimientoFlujo = 20;
                            objNuevoReg.idTipoMovimiento = 19;
                            objNuevoReg.idEstadoMovimiento = Convert.ToDecimal(cmbEstadoEf.Value);
                            objNuevoReg.idFuenteOrigenDato = (decimal)EnumTipoRegistro.OrigenWeb;
                            objNuevoReg.idUbicacionOperacion = 120;
                            objNuevoReg.idUnidadMedidaHidrocarburo = Convert.ToDecimal(cmbUnidadEf.Value);
                            objNuevoReg.idHidrocarburo = Convert.ToDecimal(cmbProdEf.Value);
                            //objNuevoReg.cantidadHidrocarburo = Convert.ToDecimal(txtCantidadEf.Text.Replace(",", "."));
                            objNuevoReg.identificador = "";
                            objNuevoReg.subIdentificador = "";
                            if (Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad")) == 16)//lmqc-- para importadores
                            {
                                objNuevoReg.observacion = txtDui.Text + '|' + txtMonSolCompensar.Text + '|' + txtXgasolinaBlanca.Text.Replace(",", ".") + '|' + txtXimp.Text.Replace(",", ".") + '|' + txtPreGasolinaBlanca.Text.Replace(",", ".") + '|' + txtPreNetoIva.Text.Replace(",", ".") + '|' + txtTarifaTransPol.Text.Replace(",", ".") + '|' + txtTarifaNetoIva.Text.Replace(",", ".") + '|' + txtPpt.Text.Replace(",", ".") + '|' + txtFronteraIngreso.Text + '|' + txtIncoterms.Text + '|' + txtObsEf.Text.Trim();
                            }
                            else if (Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad")) == 36)//lmqc-- para importadores
                            {
                                objNuevoReg.observacion = txtDui.Text + '|' + txtMonSolCompensar.Text + '|' + txtXgasolinaBlanca.Text.Replace(",", ".") + '|' + txtXimp.Text.Replace(",", ".") + '|' + txtPreGasolinaBlanca.Text.Replace(",", ".") + '|' + txtPreNetoIva.Text.Replace(",", ".") + '|' + txtTarifaTransPol.Text.Replace(",", ".") + '|' + txtTarifaNetoIva.Text.Replace(",", ".") + '|' + txtPpt.Text.Replace(",", ".") + '|' + txtFronteraIngreso.Text + '|' + txtIncoterms.Text + '|' + txtObsEf.Text.Trim();
                            }
                            else
                            {
                                objNuevoReg.observacion = txtObsEf.Text.Trim();
                            }
                            objNuevoReg.idTipoMedicion = -99999;
                            objNuevoReg.cantidadAguaLitros = 0;
                            objNuevoReg.idTipoMedioTransporte = -99999;
                            objNuevoReg.barcode = 0;
                            objNuevoReg.idMedioTransporte = -99999;
                            objNuevoReg.tipoInfraestructura = -99999;
                            objNuevoReg.idInfraestructura = -99999;

                            objNuevoReg.idUnidadEnvasesTransporte = -99999;
                            objNuevoReg.codigo = "";

                            objNuevoReg.idIntegracion = 0;
                            objNuevoReg.idUnidadTemperatura = -99999;
                            objNuevoReg.temperatura = 0;
                            objNuevoReg.gradoApi = "";
                            objNuevoReg.idEntidad = -99999;

                            objNuevoReg.cantidadEnvasesTransporte = 0;
                            objNuevoReg.binarioControl = "00000-0000000000-0000000000-00000";
                            objNuevoReg.idTransaccionBaseExterna = "";

                            objNuevoReg.jsonReferencia = "";
                            objNuevoReg.estadoJsonRef = -99999;
                            objNuevoReg.idCuenta = -99999;
                            objNuevoReg.idCuentaAux = -99999;
                            objNuevoReg.idParamGlobal = -99999;
                            objNuevoReg.idParamEspec = -99999;
                            objNuevoReg.idTipoConsumidor = -99999;
                            objNuevoReg.idTipoInfraAux = -99999;
                            objNuevoReg.idInfraAux = -99999;

                            objNuevoReg.listaTransaccion = new List<ObjTransaccion>();

                            decimal decCantidadProducto = 0;

                            var lstDocumentosMov = new List<O_LISTA_TRANSACCION_CTY>();
                            lstDocumentosMov = (List<O_LISTA_TRANSACCION_CTY>)Session[CVariablesSesion.GrillaAuxiliar];
                            foreach (var reg in lstDocumentosMov)
                            {
                                if (reg.ID_ESTADO_TRANSACCION == (decimal)EnumEstadoMov.Anulado)
                                {
                                    if (reg.ID_TRANSACCION > 0)
                                    {
                                        resultado = _client.Post<EResultado>(CAppSettings.svc_eliminar_transaccion,
                                            new
                                            {
                                                strCredencial = CParametrosHydro.strCredencialFuncionario,
                                                decIdTransaccion = reg.ID_TRANSACCION,
                                                decAppFechaRegistro = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0')),
                                                decAppIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario])
                                            });
                                        if (resultado.intCodigo != 1)
                                        {
                                            MImprimirMensaje(resultado.strMensaje, EnumTiposMensajeAlerta.Alerta);
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
                                    var objTrasaccion = new ObjTransaccion();
                                    objTrasaccion.idMovimiento = Convert.ToDecimal(txtIdEf.Text);
                                    objTrasaccion.idTransaccion = (reg.ID_TRANSACCION < 0) ? 0 : reg.ID_TRANSACCION;
                                    objTrasaccion.idTransaccionPadre = 0;
                                    objTrasaccion.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                    objTrasaccion.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                    objTrasaccion.idTipoTransaccion = Convert.ToDecimal(reg.ID_TIPO_TRANSACCION);
                                    objTrasaccion.fechaEmision = reg.FECHA_HORA_EMISION;
                                    objTrasaccion.numeroTransaccion = reg.NRO_TRANSACCION.Trim();
                                    objTrasaccion.numeroAutorizacion = Convert.ToDecimal(reg.NRO_AUTORIZACION);
                                    objTrasaccion.codigoControl = reg.CODIGO_CONTROL;

                                    objTrasaccion.nitConsumidor = reg.NIT_CONSUMIDOR;
                                    objTrasaccion.razonSocial = (reg.RAZON_SOCIAL == null) ? "" : reg.RAZON_SOCIAL.Trim();

                                    objTrasaccion.precioUnitario = Convert.ToDecimal(reg.PRECIO_UNITARIO);
                                    objTrasaccion.precioTotal = Convert.ToDecimal(reg.PRECIO_TOTAL);
                                    objTrasaccion.usuarioTransaccion = reg.USUARIO_TRANSACCION;
                                    objTrasaccion.observaciones = reg.OBSERVACIONES;
                                    objTrasaccion.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString());
                                    objTrasaccion.idEstadoTransaccion = Convert.ToDecimal(cmbEstadoEf.Value);
                                    objTrasaccion.idTipoCreditoFiscal = Convert.ToDecimal(reg.ID_TIPO_CREDITO_FISCAL);
                                    objTrasaccion.idFormaPago = Convert.ToDecimal(reg.ID_FORMA_PAGO);
                                    objTrasaccion.idTipoMoneda = Convert.ToDecimal(reg.ID_TIPO_MONEDA);
                                    objTrasaccion.idTipoEmisor = Convert.ToDecimal(reg.ID_TIPO_EMISOR);
                                    objTrasaccion.codigoSeguridad = 0;
                                    objTrasaccion.idAutorizacionDato = 0;
                                    objTrasaccion.binarioControl = "0000000000";

                                    objTrasaccion.fechaOperacion = Convert.ToDecimal(datFechaDoc.Year + datFechaDoc.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaDoc.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaDoc.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaDoc.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaDoc.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                    objTrasaccion.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                    objTrasaccion.codigo = cmbProdEf.Text;

                                    if (reg.OBSERVACIONES.Substring(0, 4) == "ACCP")
                                    {
                                        string[] listaParametros = reg.OBSERVACIONES.Split('|');

                                        decCantidadProducto = decCantidadProducto + Convert.ToDecimal(listaParametros[5]);
                                    }

                                    if (reg.ID_TRANSACCION > 0)
                                    {
                                        objNuevoReg.listaTransaccion.Add(objTrasaccion);
                                    }
                                    else
                                    {
                                        var objRegTransaccion = new ETransaccion();
                                        objRegTransaccion.strCredencial = CParametrosHydro.strCredencialFuncionario;
                                        objRegTransaccion.ObjTransaccion = objTrasaccion;
                                        var objResult = _client.Post<EResultadoGestion>(CAppSettings.svc_transaccion, objRegTransaccion);

                                        if (objResult.intCodigo == 1)
                                        {
                                            objTrasaccion.idTransaccion = objResult.oResultado.ID_TABLA;
                                            objNuevoReg.listaTransaccion.Add(objTrasaccion);
                                        }
                                        else
                                        {
                                            MImprimirMensaje(objResult.strMensaje, EnumTiposMensajeAlerta.Alerta);
                                        }
                                    }
                                }
                            }

                            objNuevoReg.cantidadHidrocarburo = decCantidadProducto;

                            var objRegistro = new EActualizaMovimientoTransaccion();
                            objRegistro.strCredencial = CParametrosHydro.strCredencialFuncionario;
                            var lstRegistros = new List<ObjMovimientoTransaccion>();
                            lstRegistros.Add(objNuevoReg);
                            objRegistro.listaMovimientoTransaccion = lstRegistros;

                            DevExpress.Web.Data.ASPxDataValidationEventArgs e1 = null;
                            resultado = _client.Post<EResultado>(CAppSettings.svc_actualiza_movimiento_transaccion, objRegistro);
                            if (resultado.intCodigo == 1)
                            {
                                e.Cancel = true;
                                Session.Remove(CVariablesSesion.AuxActualiza);
                                dgvTransaccionesImp.CancelEdit();
                                dgvTransaccionesImp_CustomCallback(null, null);                              
                            }
                            else
                            {
                                e1.RowError = "ERROR: WS " + resultado.strMensaje;
                            }

                            #region Traza consulta de informacion
                            var objConsulta = new EObjetoEnvio7ParametrosProto
                            {
                                strCredencial = CParametrosHydro.strCredencialFuncionario,
                                decFiltro = 0, //Filtro 1
                                strParametro1 = tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString(),
                                strParametro2 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
                                strParametro3 = "",
                                strParametro4 = txtIdEf.Text,
                                strParametro5 = datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'), //fechaRegistroMovimiento
                                strParametro6 = datFechaDoc.Year + datFechaDoc.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaDoc.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaDoc.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaDoc.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaDoc.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'),  //fechaOperacion
                                strParametro7 = cmbTipoOperacion.Value.ToString()
                            };
                            string strAccion = JsonSerializer.SerializeToString(objConsulta);
                            string strSalida = objRegistro.listaMovimientoTransaccion.Count().ToString();
                            string mensaje = GetType().Name + "." + MethodBase.GetCurrentMethod().Name;
                            _cTrazaInf.MRegistraTrazaInf(strAccion, mensaje, CParametrosHydro.decIdAplicacion, Convert.ToDecimal(Session[CVariablesSesion.IdUsuario].ToString()), HttpContext.Current.Request.UserHostAddress, strSalida);
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
                        e.Cancel = true;
                        dgvTransacciones.CancelEdit();
                    }
                    #endregion
                }
                catch (Exception exp)
                {
                    var argEx = new ArgumentException(exp.Message);
                    throw argEx;
                }
            }
            else
            {
                //todo No implementado.
            }
        }

        protected void dgvTransaccionesImp_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar
                    int index = dgvTransaccionesImp.FocusedRowIndex;
                    long lngidventa = Convert.ToInt64(dgvTransaccionesImp.GetRowValues(index, "ID_MOVIMIENTO"));
                    if (lngidventa > 0)
                    {
                        var datFechaReg = DateTime.Now;
                        var decFecha = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                        EResultado resultado;

                        if (e.ButtonID == "btnEliminarImp")
                        {
                            resultado = _client.Post<EResultado>(CAppSettings.svc_eliminar_movimiento,
                            new
                            {
                                strCredencial = CParametrosHydro.strCredencialFuncionario,
                                decIdMovimiento = lngidventa,
                                decAppFechaRegistro = decFecha,
                                decAppIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario])
                            });
                        }
                        else
                        {
                            resultado = _client.Post<EResultado>(CAppSettings.svc_actualiza_estado_movimiento,
                            new
                            {
                                strCredencial = CParametrosHydro.strCredencialFuncionario,
                                decIdMovimiento = lngidventa,
                                decEstado = Convert.ToInt32((e.ButtonID == "btnAnularImp") ? EnumEstadoMov.Anulado : EnumEstadoMov.Activo),
                                decAppFechaRegistro = decFecha,
                                decAppIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario])
                            });
                        }

                        if (resultado.intCodigo == 1)
                        {
                            Session.Remove(CVariablesSesion.AuxActualiza);
                            dgvTransaccionesImp_CustomCallback(null, null);
                        }
                        else
                        {
                            MImprimirMensaje(resultado.strMensaje, EnumTiposMensajeAlerta.Error);
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

        protected void dgvTransaccionesImp_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            try
            {
                #region Proceso a Ejecutar

                if (e.VisibleIndex >= 0)
                {
                    if (dgvTransaccionesImp.GetRowValues(e.VisibleIndex, "ESTADO_MOVIMIENTO").ToString().Equals("A"))
                        e.Row.BackColor = Color.FromArgb(215, 135, 135);
                    else if (Convert.ToInt32(dgvTransaccionesImp.GetRowValues(e.VisibleIndex, "ID_TIPO_FLUJO")) == (int)EnumTipoMovimientoFlujo.Contable)
                        e.Row.BackColor = Color.FromArgb(194, 193, 195);
                    else
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

        protected void dgvTransaccionesImp_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            try
            {
                #region Proceso a Ejecutar

                var asPxSummaryItem = e.Item as ASPxSummaryItem;
                if (asPxSummaryItem != null)
                {
                    switch (asPxSummaryItem.Tag)
                    {
                        case "imp":
                            if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Start)
                                _totalImporte = 0;

                            if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Calculate)
                            {
                                if (Convert.ToInt32(e.GetValue("ID_ESTADO_MOVIMIENTO").ToString()) == (int)EnumEstadoMov.Activo && Convert.ToInt32(e.GetValue("ID_TIPO_FLUJO").ToString()) == (int)EnumTipoMovimientoFlujo.Fisico)
                                {
                                    _totalImporte += Convert.ToDecimal(e.GetValue("PRECIO_TOTAL").ToString());
                                }
                            }
                            if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                                e.TotalValue = _totalImporte;
                            break;
                        case "vol":
                            if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Start)
                                _totalCantidad = 0;

                            if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Calculate)
                            {
                                if (Convert.ToInt32(e.GetValue("ID_ESTADO_MOVIMIENTO").ToString()) == (int)EnumEstadoMov.Activo && Convert.ToInt32(e.GetValue("ID_TIPO_FLUJO").ToString()) == (int)EnumTipoMovimientoFlujo.Fisico)
                                {
                                    _totalCantidad += Convert.ToDecimal(e.GetValue("CANTIDAD").ToString());
                                }
                            }
                            if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                                e.TotalValue = _totalCantidad;
                            break;
                        case "est":
                            if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Start)
                            {
                                e.TotalValue = "#An:" + 0 + " #Ac:" + 0;
                                _totalAnulada = 0;
                                _totalValida = 0;
                            }
                            if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Calculate)
                            {
                                if (Convert.ToInt32(e.GetValue("ID_ESTADO_MOVIMIENTO").ToString()) == (int)EnumEstadoMov.Activo)
                                    _totalValida++;
                                else
                                    _totalAnulada++;
                            }
                            if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                                e.TotalValue = "#An:" + _totalAnulada + " #Ac:" + _totalValida;
                            break;
                        default:
                            //todo No implementado.
                            break;
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

        protected void dgvTransaccionesImp_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            try
            {
                #region Proceso a Ejecutar

                if (cmbTipoOperacionImp.Text == "")
                {
                    return;
                }

                string strMensaje = string.Empty;
                if (!MValidaDatosImp(ref strMensaje, Convert.ToInt32(cmbTipoOperacionImp.Value), (int)tlsOperadores.FocusedNode.GetValue("IntIdActividad")))
                {
                    e.RowError = strMensaje;
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

        protected void dgvTransaccionesImp_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
        {
            if (!MValidaSesion()) return;
            try
            {
                #region Proceso a Ejecutar

                var strTipoHab = (Session[CVariablesSesion.HabilitaBotones] == null) ? "ND" : Session[CVariablesSesion.HabilitaBotones].ToString();
                var column = (GridViewCommandColumn)dgvTransaccionesImp.Columns[0];

                column.ShowNewButtonInHeader = strTipoHab == "MODSI";

                if (e.VisibleIndex == -1) return;

                switch (strTipoHab)
                {
                    case "ELISI":
                        if (e.ButtonID == "btnAnular")
                            e.Visible = DefaultBoolean.False;
                        if (e.ButtonID == "btnActivar")
                            e.Visible = DefaultBoolean.False;
                        if (e.ButtonID == "btnEliminar")
                            e.Visible = DefaultBoolean.True;
                        break;
                    case "MODSI":
                        if (e.ButtonID == "btnAnular")
                            e.Visible = DefaultBoolean.True;
                        if (e.ButtonID == "btnActivar")
                            e.Visible = DefaultBoolean.True;
                        if (e.ButtonID == "btnEliminar")
                            e.Visible = DefaultBoolean.True;
                        break;
                    default:
                        if (e.ButtonID == "btnAnular")
                            e.Visible = DefaultBoolean.False;
                        if (e.ButtonID == "btnActivar")
                            e.Visible = DefaultBoolean.False;
                        if (e.ButtonID == "btnEliminar")
                            e.Visible = DefaultBoolean.False;
                        break;
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

        protected void dgvDocumentosImp_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    if (txtIdDocEfImpEdt.Text == "1")
                    {
                        #region Proceso a Ejecutar

                        var txtClienteEfImpEdt = (dgvTransaccionesImp.FindEditFormTemplateControl("txtClienteEfImpEdt")) as ASPxTextBox;
                        var txtNitEfImpEdt = (dgvTransaccionesImp.FindEditFormTemplateControl("txtNitEfImpEdt")) as ASPxTextBox;
                        var dgvDocumentosImp = (dgvTransaccionesImp.FindEditFormTemplateControl("dgvDocumentosImp")) as ASPxGridView;
                        var cmbTipoDocEfImpEdt = (dgvTransaccionesImp.FindEditFormTemplateControl("cmbTipoDocEfImpEdt")) as ASPxComboBox;

                        var lstDocumentoMov = new List<O_LISTA_TRANSACCION_CTY>();
                        if (Session[CVariablesSesion.GrillaAuxiliar] != null)
                        {
                            lstDocumentoMov = (List<O_LISTA_TRANSACCION_CTY>)Session[CVariablesSesion.GrillaAuxiliar];
                        }

                        if (Session[CVariablesSesion.GrillaAuxiliar] != null)
                        {
                            dgvDocumentosImp.DataSource = Session[CVariablesSesion.GrillaAuxiliar];
                            dgvDocumentosImp.DataBind();
                        }

                        string strIdDoc, strDoc;

                        int a = Convert.ToInt32(cmbTipoFacturaImpAdd.Value);

                        if (a == 1)
                        {
                            #region producto

                            if (txtSecFacProducto.Text != "0")
                            {
                                var idDocumento = Convert.ToInt64(Session[CVariablesSesion.IdAuxiliar]);
                                if (idDocumento != 0)
                                {
                                    var index = dgvDocumentosImp.FocusedRowIndex;
                                    strIdDoc = dgvDocumentosImp.GetRowValues(index, "ID_TIPO_TRANSACCION").ToString();
                                    strDoc = dgvDocumentosImp.GetRowValues(index, "TIPO_TRANSACCION") + ";" + txtIdPadreImpEdt.Text.Trim();
                                    lstDocumentoMov.RemoveAll(x => x.ID_TRANSACCION == idDocumento);
                                }
                                //else
                                //{
                                //    //strIdDoc = cmbTipoDocEfImpEdt.Value.ToString();
                                //    //strDoc = cmbTipoDocEfImpEdt.Tlmqext;
                                //}

                                //var strPadreTransaccion = strDoc.Split(';');
                                var rnd = new Random();
                                var regDoc = new O_LISTA_TRANSACCION_CTY();
                                regDoc.ID_TRANSACCION = (idDocumento > 0) ? idDocumento : rnd.Next(-99999, -1);
                                regDoc.FECHA_HORA_EMISION = Convert.ToDecimal(detFechaFacproducto.Date.Year + detFechaFacproducto.Date.Month.ToString().PadLeft(2, '0') + detFechaFacproducto.Date.Day.ToString().PadLeft(2, '0') + detFechaFacproducto.Date.Hour.ToString().PadLeft(2, '0') + detFechaFacproducto.Date.Minute.ToString().PadLeft(2, '0') + detFechaFacproducto.Date.Second.ToString().PadLeft(2, '0'));
                                regDoc.FECHA_HORA = detFechaFacproducto.Date.ToString();
                                regDoc.ID_TIPO_TRANSACCION = 3;
                                regDoc.TIPO_TRANSACCION = "";// strPadreTransaccion[0].Trim();
                                regDoc.ID_PADRE_TRANSACCION = 1;// Convert.ToDecimal(txtIdPadreImpEdt.Text);
                                regDoc.TIPO_CREDITO_FISCAL = "";
                                regDoc.TIPO_EMISOR = "";
                                regDoc.FORMA_PAGO = "";
                                regDoc.OBSERVACIONES = txtObsFacProducto.Text;
                                regDoc.ID_ESTADO_TRANSACCION = 13;
                                regDoc.ID_TIPO_MONEDA = 1;
                                regDoc.USUARIO_TRANSACCION = "";
                                regDoc.NRO_AUTORIZACION = 0;
                                regDoc.NRO_TRANSACCION = txtFacProducto.Text;
                                regDoc.CODIGO_CONTROL = "";
                                regDoc.PRECIO_UNITARIO = Convert.ToDecimal(txtPreUnitarioFacProducto.Text.Replace(",", "."));
                                regDoc.CANTIDAD = Convert.ToDecimal(txtVolrecibidoFacProducto.Text.Replace(",", "."));
                                regDoc.PRECIO_TOTAL = Convert.ToDecimal(txtPreUnitarioFacProducto.Text.Replace(",", ".")) * Convert.ToDecimal(txtVolrecibidoFacProducto.Text.Replace(",", "."));
                                regDoc.ID_TIPO_CREDITO_FISCAL = -99999;
                                regDoc.ID_TIPO_EMISOR = 14;
                                regDoc.ID_FORMA_PAGO = -99999;
                                regDoc.NIT_CONSUMIDOR = Convert.ToDecimal(txtNitFacProducto.Text);
                                regDoc.RAZON_SOCIAL = txtRazonSocialFacProducto.Text;

                                //lmqc para importadores
                                regDoc.OBSERVACIONES = "ACCP" + txtSecFacProducto.Text + '|' + txtDuiFacProducto.Text + '|' + '|' + '|' + '|'
                                        + txtVolrecibidoFacProducto.Text.Replace(",", ".") + '|' + '|' + '|' + '|' + '|'
                                        + '|' + txtObsFacProducto.Text.Trim();

                                lstDocumentoMov.Add(regDoc);
                            }


                            #endregion

                            #region inspeccion

                            if (txtSecFacInOrigen.Text != "0")
                            {
                                var idDocumento = Convert.ToInt64(Session[CVariablesSesion.IdAuxiliar]);
                                if (idDocumento != 0)
                                {
                                    var index = dgvDocumentosImp.FocusedRowIndex;
                                    strIdDoc = dgvDocumentosImp.GetRowValues(index, "ID_TIPO_TRANSACCION").ToString();
                                    strDoc = dgvDocumentosImp.GetRowValues(index, "TIPO_TRANSACCION") + ";" + txtIdPadreImpEdt.Text.Trim();
                                    lstDocumentoMov.RemoveAll(x => x.ID_TRANSACCION == idDocumento);
                                }
                                else
                                {
                                    //strIdDoc = cmbTipoDocEfImpEdt.Value.ToString();
                                    //strDoc = cmbTipoDocEfImpEdt.Text;
                                }

                                //var strPadreTransaccion = strDoc.Split(';');
                                var rnd = new Random();
                                var regDoc = new O_LISTA_TRANSACCION_CTY();
                                regDoc.ID_TRANSACCION = (idDocumento > 0) ? idDocumento : rnd.Next(-99999, -1);
                                regDoc.FECHA_HORA_EMISION = Convert.ToDecimal(detFechaFacproducto.Date.Year + detFechaFacproducto.Date.Month.ToString().PadLeft(2, '0') + detFechaFacproducto.Date.Day.ToString().PadLeft(2, '0') + detFechaFacproducto.Date.Hour.ToString().PadLeft(2, '0') + detFechaFacproducto.Date.Minute.ToString().PadLeft(2, '0') + detFechaFacproducto.Date.Second.ToString().PadLeft(2, '0'));
                                regDoc.FECHA_HORA = detFechaFacproducto.Date.ToString();
                                regDoc.ID_TIPO_TRANSACCION = 3;
                                regDoc.TIPO_TRANSACCION = "";// strPadreTransaccion[0].Trim();
                                regDoc.ID_PADRE_TRANSACCION = 1;// Convert.ToDecimal(txtIdPadreImpEdt.Text);
                                regDoc.TIPO_CREDITO_FISCAL = "";
                                regDoc.TIPO_EMISOR = "";
                                regDoc.FORMA_PAGO = "";
                                regDoc.OBSERVACIONES = txtObsFacInOrigen.Text;
                                regDoc.ID_ESTADO_TRANSACCION = 13;
                                regDoc.ID_TIPO_MONEDA = 1;
                                regDoc.USUARIO_TRANSACCION = "";
                                regDoc.NRO_AUTORIZACION = 0;
                                regDoc.NRO_TRANSACCION = txtFacInOrigen.Text;
                                regDoc.CODIGO_CONTROL = "";
                                regDoc.PRECIO_UNITARIO = 0;
                                regDoc.CANTIDAD = 0;
                                regDoc.PRECIO_TOTAL = Convert.ToDecimal(txtMonFacInOrigen.Text.Replace(",", "."));
                                regDoc.ID_TIPO_CREDITO_FISCAL = -99999;
                                regDoc.ID_TIPO_EMISOR = 14;
                                regDoc.ID_FORMA_PAGO = -99999;
                                regDoc.NIT_CONSUMIDOR = 0;//Convert.ToDecimal(txtNitEfImpEdt.Text);
                                regDoc.RAZON_SOCIAL = txtEmpresaFacInOrigen.Text;

                                //lmqc para importadores
                                string strCodFactura = "D1IO";//ObtenerCodigoFacturaImportacion(Convert.ToInt32(cmbTipoFacturaImpEdt.Value));

                                regDoc.OBSERVACIONES = strCodFactura + txtSecFacInOrigen.Text + '|' + txtDuiFacProducto.Text + '|' + '|' + '|' + '|'
                                    + txtVolFactInOrigen.Text.Replace(",", ".") + '|' + txtFacProducto.Text + '-' + txtRazonSocialFacProducto.Text + '|' + '|' + '|' + '|'
                                    + '|' + txtObsFacInOrigen.Text.Trim();

                                lstDocumentoMov.Add(regDoc);
                            }

                            if (txtSecFacInIn.Text != "0")
                            {
                                var idDocumento = Convert.ToInt64(Session[CVariablesSesion.IdAuxiliar]);
                                if (idDocumento != 0)
                                {
                                    var index = dgvDocumentosImp.FocusedRowIndex;
                                    strIdDoc = dgvDocumentosImp.GetRowValues(index, "ID_TIPO_TRANSACCION").ToString();
                                    strDoc = dgvDocumentosImp.GetRowValues(index, "TIPO_TRANSACCION") + ";" + txtIdPadreImpEdt.Text.Trim();
                                    lstDocumentoMov.RemoveAll(x => x.ID_TRANSACCION == idDocumento);
                                }
                                else
                                {
                                    //strIdDoc = cmbTipoDocEfImpEdt.Value.ToString();
                                    //strDoc = cmbTipoDocEfImpEdt.Text;
                                }

                                //var strPadreTransaccion = strDoc.Split(';');
                                var rnd = new Random();
                                var regDoc = new O_LISTA_TRANSACCION_CTY();
                                regDoc.ID_TRANSACCION = (idDocumento > 0) ? idDocumento : rnd.Next(-99999, -1);
                                regDoc.FECHA_HORA_EMISION = Convert.ToDecimal(detFechaFacproducto.Date.Year + detFechaFacproducto.Date.Month.ToString().PadLeft(2, '0') + detFechaFacproducto.Date.Day.ToString().PadLeft(2, '0') + detFechaFacproducto.Date.Hour.ToString().PadLeft(2, '0') + detFechaFacproducto.Date.Minute.ToString().PadLeft(2, '0') + detFechaFacproducto.Date.Second.ToString().PadLeft(2, '0'));
                                regDoc.FECHA_HORA = detFechaFacproducto.Date.ToString();
                                regDoc.ID_TIPO_TRANSACCION = 3;
                                regDoc.TIPO_TRANSACCION = "";// strPadreTransaccion[0].Trim();
                                regDoc.ID_PADRE_TRANSACCION = 1;// Convert.ToDecimal(txtIdPadreImpEdt.Text);
                                regDoc.TIPO_CREDITO_FISCAL = "";
                                regDoc.TIPO_EMISOR = "";
                                regDoc.FORMA_PAGO = "";
                                regDoc.OBSERVACIONES = txtObsFacInIn.Text;
                                regDoc.ID_ESTADO_TRANSACCION = 13;
                                regDoc.ID_TIPO_MONEDA = 1;
                                regDoc.USUARIO_TRANSACCION = "";
                                regDoc.NRO_AUTORIZACION = 0;
                                regDoc.NRO_TRANSACCION = txtFacInIn.Text;
                                regDoc.CODIGO_CONTROL = "";
                                regDoc.PRECIO_UNITARIO = 0;
                                regDoc.CANTIDAD = 0;
                                regDoc.PRECIO_TOTAL = Convert.ToDecimal(txtMontoFacInIn.Text.Replace(",", "."));
                                regDoc.ID_TIPO_CREDITO_FISCAL = -99999;
                                regDoc.ID_TIPO_EMISOR = 14;
                                regDoc.ID_FORMA_PAGO = -99999;
                                regDoc.NIT_CONSUMIDOR = 0;//Convert.ToDecimal(txtNitEfImpEdt.Text);
                                regDoc.RAZON_SOCIAL = txtEmpresaFacInIn.Text;

                                //lmqc para importadores
                                string strCodFactura = "D2II";//ObtenerCodigoFacturaImportacion(Convert.ToInt32(cmbTipoFacturaImpEdt.Value));

                                regDoc.OBSERVACIONES = strCodFactura + txtSecFacInIn.Text + '|' + txtDuiFacProducto.Text + '|' + '|' + '|' + '|'
                                    + txtVolFactInIn.Text.Replace(",", ".") + '|' + txtFacProducto.Text + '-' + txtRazonSocialFacProducto.Text + '|' + '|' + '|' + '|'
                                    + '|' + txtObsFacInIn.Text.Trim();

                                lstDocumentoMov.Add(regDoc);
                            }

                            if (txtSecFacInFin.Text != "0")
                            {
                                var idDocumento = Convert.ToInt64(Session[CVariablesSesion.IdAuxiliar]);
                                if (idDocumento != 0)
                                {
                                    var index = dgvDocumentosImp.FocusedRowIndex;
                                    strIdDoc = dgvDocumentosImp.GetRowValues(index, "ID_TIPO_TRANSACCION").ToString();
                                    strDoc = dgvDocumentosImp.GetRowValues(index, "TIPO_TRANSACCION") + ";" + txtIdPadreImpEdt.Text.Trim();
                                    lstDocumentoMov.RemoveAll(x => x.ID_TRANSACCION == idDocumento);
                                }
                                else
                                {
                                    //strIdDoc = cmbTipoDocEfImpEdt.Value.ToString();
                                    //strDoc = cmbTipoDocEfImpEdt.Text;
                                }

                                //var strPadreTransaccion = strDoc.Split(';');
                                var rnd = new Random();
                                var regDoc = new O_LISTA_TRANSACCION_CTY();
                                regDoc.ID_TRANSACCION = (idDocumento > 0) ? idDocumento : rnd.Next(-99999, -1);
                                regDoc.FECHA_HORA_EMISION = Convert.ToDecimal(detFechaFacproducto.Date.Year + detFechaFacproducto.Date.Month.ToString().PadLeft(2, '0') + detFechaFacproducto.Date.Day.ToString().PadLeft(2, '0') + detFechaFacproducto.Date.Hour.ToString().PadLeft(2, '0') + detFechaFacproducto.Date.Minute.ToString().PadLeft(2, '0') + detFechaFacproducto.Date.Second.ToString().PadLeft(2, '0'));
                                regDoc.FECHA_HORA = detFechaFacproducto.Date.ToString();
                                regDoc.ID_TIPO_TRANSACCION = 3;
                                regDoc.TIPO_TRANSACCION = "";// strPadreTransaccion[0].Trim();
                                regDoc.ID_PADRE_TRANSACCION = 1;// Convert.ToDecimal(txtIdPadreImpEdt.Text);
                                regDoc.TIPO_CREDITO_FISCAL = "";
                                regDoc.TIPO_EMISOR = "";
                                regDoc.FORMA_PAGO = "";
                                regDoc.OBSERVACIONES = txtObsFacInFin.Text;
                                regDoc.ID_ESTADO_TRANSACCION = 13;
                                regDoc.ID_TIPO_MONEDA = 1;
                                regDoc.USUARIO_TRANSACCION = "";
                                regDoc.NRO_AUTORIZACION = 0;
                                regDoc.NRO_TRANSACCION = txtFacInFin.Text;
                                regDoc.CODIGO_CONTROL = "";
                                regDoc.PRECIO_UNITARIO = 0;
                                regDoc.CANTIDAD = 0;
                                regDoc.PRECIO_TOTAL = Convert.ToDecimal(txtMontoFacInFin.Text.Replace(",", "."));
                                regDoc.ID_TIPO_CREDITO_FISCAL = -99999;
                                regDoc.ID_TIPO_EMISOR = 14;
                                regDoc.ID_FORMA_PAGO = -99999;
                                regDoc.NIT_CONSUMIDOR = 0;//Convert.ToDecimal(txtNitEfImpEdt.Text);
                                regDoc.RAZON_SOCIAL = txtEmpresaFacInFin.Text;

                                //lmqc para importadores
                                string strCodFactura = "D3IDF";//ObtenerCodigoFacturaImportacion(Convert.ToInt32(cmbTipoFacturaImpEdt.Value));

                                regDoc.OBSERVACIONES = strCodFactura + txtSecFacInFin.Text + '|' + txtDuiFacProducto.Text + '|' + '|' + '|' + '|'
                                    + txtVolFactInFin.Text.Replace(",", ".") + '|' + txtFacProducto.Text + '-' + txtRazonSocialFacProducto.Text + '|' + '|' + '|' + '|'
                                    + '|' + txtObsFacInFin.Text.Trim();

                                lstDocumentoMov.Add(regDoc);
                            }

                            #endregion

                            #region monitoreo

                            if (txtSecFacMon.Text != "0")
                            {
                                var idDocumento = Convert.ToInt64(Session[CVariablesSesion.IdAuxiliar]);
                                if (idDocumento != 0)
                                {
                                    var index = dgvDocumentosImp.FocusedRowIndex;
                                    strIdDoc = dgvDocumentosImp.GetRowValues(index, "ID_TIPO_TRANSACCION").ToString();
                                    strDoc = dgvDocumentosImp.GetRowValues(index, "TIPO_TRANSACCION") + ";" + txtIdPadreImpEdt.Text.Trim();
                                    lstDocumentoMov.RemoveAll(x => x.ID_TRANSACCION == idDocumento);
                                }
                                else
                                {
                                    //strIdDoc = cmbTipoDocEfImpEdt.Value.ToString();
                                    //strDoc = cmbTipoDocEfImpEdt.Text;
                                }

                                //var strPadreTransaccion = strDoc.Split(';');
                                var rnd = new Random();
                                var regDoc = new O_LISTA_TRANSACCION_CTY();
                                regDoc.ID_TRANSACCION = (idDocumento > 0) ? idDocumento : rnd.Next(-99999, -1);
                                regDoc.FECHA_HORA_EMISION = Convert.ToDecimal(detFechaFacproducto.Date.Year + detFechaFacproducto.Date.Month.ToString().PadLeft(2, '0') + detFechaFacproducto.Date.Day.ToString().PadLeft(2, '0') + detFechaFacproducto.Date.Hour.ToString().PadLeft(2, '0') + detFechaFacproducto.Date.Minute.ToString().PadLeft(2, '0') + detFechaFacproducto.Date.Second.ToString().PadLeft(2, '0'));
                                regDoc.FECHA_HORA = detFechaFacproducto.Date.ToString();
                                regDoc.ID_TIPO_TRANSACCION = 3;
                                regDoc.TIPO_TRANSACCION = "";// strPadreTransaccion[0].Trim();
                                regDoc.ID_PADRE_TRANSACCION = 1;// Convert.ToDecimal(txtIdPadreImpEdt.Text);
                                regDoc.TIPO_CREDITO_FISCAL = "";
                                regDoc.TIPO_EMISOR = "";
                                regDoc.FORMA_PAGO = "";
                                regDoc.OBSERVACIONES = txtObsFacMon.Text;
                                regDoc.ID_ESTADO_TRANSACCION = 13;
                                regDoc.ID_TIPO_MONEDA = 1;
                                regDoc.USUARIO_TRANSACCION = "";
                                regDoc.NRO_AUTORIZACION = 0;
                                regDoc.NRO_TRANSACCION = txtFacMon.Text;
                                regDoc.CODIGO_CONTROL = "";
                                regDoc.PRECIO_UNITARIO = 0;
                                regDoc.CANTIDAD = 0;
                                regDoc.PRECIO_TOTAL = Convert.ToDecimal(txtMontoFacMon.Text.Replace(",", "."));
                                regDoc.ID_TIPO_CREDITO_FISCAL = -99999;
                                regDoc.ID_TIPO_EMISOR = 14;
                                regDoc.ID_FORMA_PAGO = -99999;
                                regDoc.NIT_CONSUMIDOR = 0;//Convert.ToDecimal(txtNitEfImpEdt.Text);
                                regDoc.RAZON_SOCIAL = txtEmpresaFacMon.Text;

                                //lmqc para importadores
                                string strCodFactura = "HCM";//ObtenerCodigoFacturaImportacion(Convert.ToInt32(cmbTipoFacturaImpEdt.Value));

                                regDoc.OBSERVACIONES = strCodFactura + txtSecFacMon.Text + '|' + txtDuiFacProducto.Text + '|' + '|' + '|' + '|'
                                    + txtVolReMon.Text.Replace(",", ".") + '|' + txtFacProducto.Text + '-' + txtRazonSocialFacProducto.Text + '|' + '|' + '|' + '|'
                                    + '|' + txtObsFacMon.Text.Trim();

                                lstDocumentoMov.Add(regDoc);
                            }

                            #endregion

                        }

                        if (a == 2)
                        {
                            #region trasnporte

                            if (txtSecFacTransFlu.Text != "0")
                            {
                                var idDocumento = Convert.ToInt64(Session[CVariablesSesion.IdAuxiliar]);
                                if (idDocumento != 0)
                                {
                                    var index = dgvDocumentosImp.FocusedRowIndex;
                                    strIdDoc = dgvDocumentosImp.GetRowValues(index, "ID_TIPO_TRANSACCION").ToString();
                                    strDoc = dgvDocumentosImp.GetRowValues(index, "TIPO_TRANSACCION") + ";" + txtIdPadreImpEdt.Text.Trim();
                                    lstDocumentoMov.RemoveAll(x => x.ID_TRANSACCION == idDocumento);
                                }
                                else
                                {
                                    //strIdDoc = cmbTipoDocEfImpEdt.Value.ToString();
                                    //strDoc = cmbTipoDocEfImpEdt.Text;
                                }

                                //var strPadreTransaccion = strDoc.Split(';');
                                var rnd = new Random();
                                var regDoc = new O_LISTA_TRANSACCION_CTY();
                                regDoc.ID_TRANSACCION = (idDocumento > 0) ? idDocumento : rnd.Next(-99999, -1);
                                regDoc.FECHA_HORA_EMISION = Convert.ToDecimal(detFechaFacproducto.Date.Year + detFechaFacproducto.Date.Month.ToString().PadLeft(2, '0') + detFechaFacproducto.Date.Day.ToString().PadLeft(2, '0') + detFechaFacproducto.Date.Hour.ToString().PadLeft(2, '0') + detFechaFacproducto.Date.Minute.ToString().PadLeft(2, '0') + detFechaFacproducto.Date.Second.ToString().PadLeft(2, '0'));
                                regDoc.FECHA_HORA = detFechaFacproducto.Date.ToString();
                                regDoc.ID_TIPO_TRANSACCION = 3;
                                regDoc.TIPO_TRANSACCION = "";// strPadreTransaccion[0].Trim();
                                regDoc.ID_PADRE_TRANSACCION = 1;// Convert.ToDecimal(txtIdPadreImpEdt.Text);
                                regDoc.TIPO_CREDITO_FISCAL = "";
                                regDoc.TIPO_EMISOR = "";
                                regDoc.FORMA_PAGO = "";
                                regDoc.OBSERVACIONES = txtObsFacTransFlu.Text;
                                regDoc.ID_ESTADO_TRANSACCION = 13;
                                regDoc.ID_TIPO_MONEDA = 1;
                                regDoc.USUARIO_TRANSACCION = "";
                                regDoc.NRO_AUTORIZACION = 0;
                                regDoc.NRO_TRANSACCION = txtFacTransFlu.Text;
                                regDoc.CODIGO_CONTROL = "";
                                regDoc.PRECIO_UNITARIO = Convert.ToDecimal(txtCostoUniFacTransFlu.Text.Replace(",", "."));
                                regDoc.CANTIDAD = 0;
                                regDoc.PRECIO_TOTAL = 0;
                                regDoc.ID_TIPO_CREDITO_FISCAL = -99999;
                                regDoc.ID_TIPO_EMISOR = 14;
                                regDoc.ID_FORMA_PAGO = -99999;
                                regDoc.NIT_CONSUMIDOR = 0;//Convert.ToDecimal(txtNitEfImpEdt.Text);
                                regDoc.RAZON_SOCIAL = txtEmpresaFacTransFlu.Text;

                                //lmqc para importadores
                                string strCodFactura = "B1CCTF";//ObtenerCodigoFacturaImportacion(Convert.ToInt32(cmbTipoFacturaImpEdt.Value));

                                regDoc.OBSERVACIONES = strCodFactura + txtSecFacTransFlu.Text + '|' + txtDuiFacProducto.Text + '|' + txtPlantaDespachoFacTransFlu.Text + '|' + txtPlantaRecepcionFacTransFlu.Text + '|' + txtVolDespachadoFacTransFlu.Text.Replace(",", ".") + '|'
                                    + txtVolRecibidoFacTransFlu.Text.Replace(",", ".") + '|' + txtFacProducto.Text + '-' + txtRazonSocialFacProducto.Text + '|' + txtFleteFacTransFlu.Text.Replace(",", ".") + '|' + '|' + '|'
                                    + Convert.ToDecimal(txtCostoUniFacTransFlu.Text.Replace(",", ".")) + '|' + txtObsFacTransFlu.Text.Trim();

                                lstDocumentoMov.Add(regDoc);

                            }

                            if (txtSecFacTransTe.Text != "0")
                            {
                                var idDocumento = Convert.ToInt64(Session[CVariablesSesion.IdAuxiliar]);
                                if (idDocumento != 0)
                                {
                                    var index = dgvDocumentosImp.FocusedRowIndex;
                                    strIdDoc = dgvDocumentosImp.GetRowValues(index, "ID_TIPO_TRANSACCION").ToString();
                                    strDoc = dgvDocumentosImp.GetRowValues(index, "TIPO_TRANSACCION") + ";" + txtIdPadreImpEdt.Text.Trim();
                                    lstDocumentoMov.RemoveAll(x => x.ID_TRANSACCION == idDocumento);
                                }
                                else
                                {
                                    //strIdDoc = cmbTipoDocEfImpEdt.Value.ToString();
                                    //strDoc = cmbTipoDocEfImpEdt.Text;
                                }

                                //var strPadreTransaccion = strDoc.Split(';');
                                var rnd = new Random();
                                var regDoc = new O_LISTA_TRANSACCION_CTY();
                                regDoc.ID_TRANSACCION = (idDocumento > 0) ? idDocumento : rnd.Next(-99999, -1);
                                regDoc.FECHA_HORA_EMISION = Convert.ToDecimal(detFechaFacproducto.Date.Year + detFechaFacproducto.Date.Month.ToString().PadLeft(2, '0') + detFechaFacproducto.Date.Day.ToString().PadLeft(2, '0') + detFechaFacproducto.Date.Hour.ToString().PadLeft(2, '0') + detFechaFacproducto.Date.Minute.ToString().PadLeft(2, '0') + detFechaFacproducto.Date.Second.ToString().PadLeft(2, '0'));
                                regDoc.FECHA_HORA = detFechaFacproducto.Date.ToString();
                                regDoc.ID_TIPO_TRANSACCION = 3;
                                regDoc.TIPO_TRANSACCION = "";// strPadreTransaccion[0].Trim();
                                regDoc.ID_PADRE_TRANSACCION = 1;// Convert.ToDecimal(txtIdPadreImpEdt.Text);
                                regDoc.TIPO_CREDITO_FISCAL = "";
                                regDoc.TIPO_EMISOR = "";
                                regDoc.FORMA_PAGO = "";
                                regDoc.OBSERVACIONES = txtObsFacTransTe.Text;
                                regDoc.ID_ESTADO_TRANSACCION = 13;
                                regDoc.ID_TIPO_MONEDA = 1;
                                regDoc.USUARIO_TRANSACCION = "";
                                regDoc.NRO_AUTORIZACION = 0;
                                regDoc.NRO_TRANSACCION = txtFacTransTe.Text;
                                regDoc.CODIGO_CONTROL = "";
                                regDoc.PRECIO_UNITARIO = Convert.ToDecimal(txtCostoUniFacTransTe.Text.Replace(",", "."));
                                regDoc.CANTIDAD = 0;
                                regDoc.PRECIO_TOTAL = 0;
                                regDoc.ID_TIPO_CREDITO_FISCAL = -99999;
                                regDoc.ID_TIPO_EMISOR = 14;
                                regDoc.ID_FORMA_PAGO = -99999;
                                regDoc.NIT_CONSUMIDOR = 0;//Convert.ToDecimal(txtNitEfImpEdt.Text);
                                regDoc.RAZON_SOCIAL = txtEmpresaFacTransTe.Text;

                                //lmqc para importadores
                                string strCodFactura = "B2CCTT";//ObtenerCodigoFacturaImportacion(Convert.ToInt32(cmbTipoFacturaImpEdt.Value));

                                regDoc.OBSERVACIONES = strCodFactura + txtSecFacTransTe.Text + '|' + txtDuiFacProducto.Text + '|' + txtPlantaDespachoFacTransTe.Text + '|' + txtPlantaRecepcionFacTransTe.Text + '|' + txtVolDespachadoFacTransTe.Text.Replace(",", ".") + '|'
                                    + txtVolRecibidoFacTransTe.Text.Replace(",", ".") + '|' + txtFacProducto.Text + '-' + txtRazonSocialFacProducto.Text + '|' + txtFleteFacTransTe.Text.Replace(",", ".") + '|' + '|' + '|'
                                    + Convert.ToDecimal(txtCostoUniFacTransTe.Text.Replace(",", ".")) + '|' + txtObsFacTransTe.Text.Trim();

                                lstDocumentoMov.Add(regDoc);

                            }

                            if (txtSecFacTransTeGb.Text != "0")
                            {
                                var idDocumento = Convert.ToInt64(Session[CVariablesSesion.IdAuxiliar]);
                                if (idDocumento != 0)
                                {
                                    var index = dgvDocumentosImp.FocusedRowIndex;
                                    strIdDoc = dgvDocumentosImp.GetRowValues(index, "ID_TIPO_TRANSACCION").ToString();
                                    strDoc = dgvDocumentosImp.GetRowValues(index, "TIPO_TRANSACCION") + ";" + txtIdPadreImpEdt.Text.Trim();
                                    lstDocumentoMov.RemoveAll(x => x.ID_TRANSACCION == idDocumento);
                                }
                                else
                                {
                                    //strIdDoc = cmbTipoDocEfImpEdt.Value.ToString();
                                    //strDoc = cmbTipoDocEfImpEdt.Text;
                                }

                                //var strPadreTransaccion = strDoc.Split(';');
                                var rnd = new Random();
                                var regDoc = new O_LISTA_TRANSACCION_CTY();
                                regDoc.ID_TRANSACCION = (idDocumento > 0) ? idDocumento : rnd.Next(-99999, -1);
                                regDoc.FECHA_HORA_EMISION = Convert.ToDecimal(detFechaFacproducto.Date.Year + detFechaFacproducto.Date.Month.ToString().PadLeft(2, '0') + detFechaFacproducto.Date.Day.ToString().PadLeft(2, '0') + detFechaFacproducto.Date.Hour.ToString().PadLeft(2, '0') + detFechaFacproducto.Date.Minute.ToString().PadLeft(2, '0') + detFechaFacproducto.Date.Second.ToString().PadLeft(2, '0'));
                                regDoc.FECHA_HORA = detFechaFacproducto.Date.ToString();
                                regDoc.ID_TIPO_TRANSACCION = 3;
                                regDoc.TIPO_TRANSACCION = "";// strPadreTransaccion[0].Trim();
                                regDoc.ID_PADRE_TRANSACCION = 1;// Convert.ToDecimal(txtIdPadreImpEdt.Text);
                                regDoc.TIPO_CREDITO_FISCAL = "";
                                regDoc.TIPO_EMISOR = "";
                                regDoc.FORMA_PAGO = "";
                                regDoc.OBSERVACIONES = txtObsFacTransTe.Text;
                                regDoc.ID_ESTADO_TRANSACCION = 13;
                                regDoc.ID_TIPO_MONEDA = 1;
                                regDoc.USUARIO_TRANSACCION = "";
                                regDoc.NRO_AUTORIZACION = 0;
                                regDoc.NRO_TRANSACCION = txtFacTransTe.Text;
                                regDoc.CODIGO_CONTROL = "";
                                regDoc.PRECIO_UNITARIO = Convert.ToDecimal(txtPreUniFacTransDu.Text.Replace(",", "."));
                                regDoc.CANTIDAD = 0;
                                regDoc.PRECIO_TOTAL = 0;
                                regDoc.ID_TIPO_CREDITO_FISCAL = -99999;
                                regDoc.ID_TIPO_EMISOR = 14;
                                regDoc.ID_FORMA_PAGO = -99999;
                                regDoc.NIT_CONSUMIDOR = 0;//Convert.ToDecimal(txtNitEfImpEdt.Text);
                                regDoc.RAZON_SOCIAL = txtEmpresaFacTransTe.Text;

                                //lmqc para importadores
                                string strCodFactura = "B2GBCCTTD";//ObtenerCodigoFacturaImportacion(Convert.ToInt32(cmbTipoFacturaImpEdt.Value));

                                regDoc.OBSERVACIONES = strCodFactura + txtSecFacTransTe.Text + '|' + '|' + '|' + '|' + txtVolFacTransDu.Text.Replace(",", ".") + '|'
                                    + '|' + '|' + '|' + '|' + '|'
                                    + Convert.ToDecimal(txtPreUniFacTransDu.Text.Replace(",", ".")) + '|' + "";

                                lstDocumentoMov.Add(regDoc);

                            }

                            if (txtSecFacTransFe.Text != "0")
                            {
                                var idDocumento = Convert.ToInt64(Session[CVariablesSesion.IdAuxiliar]);
                                if (idDocumento != 0)
                                {
                                    var index = dgvDocumentosImp.FocusedRowIndex;
                                    strIdDoc = dgvDocumentosImp.GetRowValues(index, "ID_TIPO_TRANSACCION").ToString();
                                    strDoc = dgvDocumentosImp.GetRowValues(index, "TIPO_TRANSACCION") + ";" + txtIdPadreImpEdt.Text.Trim();
                                    lstDocumentoMov.RemoveAll(x => x.ID_TRANSACCION == idDocumento);
                                }
                                else
                                {
                                    //strIdDoc = cmbTipoDocEfImpEdt.Value.ToString();
                                    //strDoc = cmbTipoDocEfImpEdt.Text;
                                }

                                //var strPadreTransaccion = strDoc.Split(';');
                                var rnd = new Random();
                                var regDoc = new O_LISTA_TRANSACCION_CTY();
                                regDoc.ID_TRANSACCION = (idDocumento > 0) ? idDocumento : rnd.Next(-99999, -1);
                                regDoc.FECHA_HORA_EMISION = Convert.ToDecimal(detFechaFacproducto.Date.Year + detFechaFacproducto.Date.Month.ToString().PadLeft(2, '0') + detFechaFacproducto.Date.Day.ToString().PadLeft(2, '0') + detFechaFacproducto.Date.Hour.ToString().PadLeft(2, '0') + detFechaFacproducto.Date.Minute.ToString().PadLeft(2, '0') + detFechaFacproducto.Date.Second.ToString().PadLeft(2, '0'));
                                regDoc.FECHA_HORA = detFechaFacproducto.Date.ToString();
                                regDoc.ID_TIPO_TRANSACCION = 3;
                                regDoc.TIPO_TRANSACCION = "";// strPadreTransaccion[0].Trim();
                                regDoc.ID_PADRE_TRANSACCION = 1;// Convert.ToDecimal(txtIdPadreImpEdt.Text);
                                regDoc.TIPO_CREDITO_FISCAL = "";
                                regDoc.TIPO_EMISOR = "";
                                regDoc.FORMA_PAGO = "";
                                regDoc.OBSERVACIONES = txtObsFacTransFe.Text;
                                regDoc.ID_ESTADO_TRANSACCION = 13;
                                regDoc.ID_TIPO_MONEDA = 1;
                                regDoc.USUARIO_TRANSACCION = "";
                                regDoc.NRO_AUTORIZACION = 0;
                                regDoc.NRO_TRANSACCION = txtFacTransFe.Text;
                                regDoc.CODIGO_CONTROL = "";
                                regDoc.PRECIO_UNITARIO = Convert.ToDecimal(txtCostoUniFacTransFe.Text.Replace(",", "."));
                                regDoc.CANTIDAD = 0;
                                regDoc.PRECIO_TOTAL = 0;
                                regDoc.ID_TIPO_CREDITO_FISCAL = -99999;
                                regDoc.ID_TIPO_EMISOR = 14;
                                regDoc.ID_FORMA_PAGO = -99999;
                                regDoc.NIT_CONSUMIDOR = 0;//Convert.ToDecimal(txtNitEfImpEdt.Text);
                                regDoc.RAZON_SOCIAL = txtEmpresaFacTransFe.Text;

                                //lmqc para importadores
                                string strCodFactura = "B3CCTF";//ObtenerCodigoFacturaImportacion(Convert.ToInt32(cmbTipoFacturaImpEdt.Value));

                                regDoc.OBSERVACIONES = strCodFactura + txtSecFacTransFe.Text + '|' + txtDuiFacProducto.Text + '|' + '|' + '|' + txtVolDespachadoFacTransFe.Text.Replace(",", ".") + '|'
                                    + txtVolRecibidoFacTransFe.Text.Replace(",", ".") + '|' + txtFacProducto.Text + '-' + txtRazonSocialFacProducto.Text + '|' + txtFleteFacTransFe.Text.Replace(",", ".") + '|' + '|' + '|'
                                    + Convert.ToDecimal(txtCostoUniFacTransFe.Text.Replace(",", ".")) + '|' + txtObsFacTransFe.Text.Trim();

                                lstDocumentoMov.Add(regDoc);

                            }


                            #endregion

                            #region almacenaje

                            if (txtSecFacAlmacenajeIntermedio.Text != "0")
                            {
                                var idDocumento = Convert.ToInt64(Session[CVariablesSesion.IdAuxiliar]);
                                if (idDocumento != 0)
                                {
                                    var index = dgvDocumentosImp.FocusedRowIndex;
                                    strIdDoc = dgvDocumentosImp.GetRowValues(index, "ID_TIPO_TRANSACCION").ToString();
                                    strDoc = dgvDocumentosImp.GetRowValues(index, "TIPO_TRANSACCION") + ";" + txtIdPadreImpEdt.Text.Trim();
                                    lstDocumentoMov.RemoveAll(x => x.ID_TRANSACCION == idDocumento);
                                }
                                else
                                {
                                    //strIdDoc = cmbTipoDocEfImpEdt.Value.ToString();
                                    //strDoc = cmbTipoDocEfImpEdt.Text;
                                }

                                //var strPadreTransaccion = strDoc.Split(';');
                                var rnd = new Random();
                                var regDoc = new O_LISTA_TRANSACCION_CTY();
                                regDoc.ID_TRANSACCION = (idDocumento > 0) ? idDocumento : rnd.Next(-99999, -1);
                                regDoc.FECHA_HORA_EMISION = Convert.ToDecimal(detFechaFacproducto.Date.Year + detFechaFacproducto.Date.Month.ToString().PadLeft(2, '0') + detFechaFacproducto.Date.Day.ToString().PadLeft(2, '0') + detFechaFacproducto.Date.Hour.ToString().PadLeft(2, '0') + detFechaFacproducto.Date.Minute.ToString().PadLeft(2, '0') + detFechaFacproducto.Date.Second.ToString().PadLeft(2, '0'));
                                regDoc.FECHA_HORA = detFechaFacproducto.Date.ToString();
                                regDoc.ID_TIPO_TRANSACCION = 3;
                                regDoc.TIPO_TRANSACCION = "";// strPadreTransaccion[0].Trim();
                                regDoc.ID_PADRE_TRANSACCION = 1;// Convert.ToDecimal(txtIdPadreImpEdt.Text);
                                regDoc.TIPO_CREDITO_FISCAL = "";
                                regDoc.TIPO_EMISOR = "";
                                regDoc.FORMA_PAGO = "";
                                regDoc.OBSERVACIONES = txtObsFacAlmacenajeIntermedio.Text;
                                regDoc.ID_ESTADO_TRANSACCION = 13;
                                regDoc.ID_TIPO_MONEDA = 1;
                                regDoc.USUARIO_TRANSACCION = "";
                                regDoc.NRO_AUTORIZACION = 0;
                                regDoc.NRO_TRANSACCION = txtFacAlmacenajeIntermedio.Text;
                                regDoc.CODIGO_CONTROL = "";
                                regDoc.PRECIO_UNITARIO = Convert.ToDecimal(txtCostoUniFacAlmacenajeIntermedio.Text.Replace(",", "."));
                                regDoc.CANTIDAD = 0;
                                regDoc.PRECIO_TOTAL = 0;
                                regDoc.ID_TIPO_CREDITO_FISCAL = -99999;
                                regDoc.ID_TIPO_EMISOR = 14;
                                regDoc.ID_FORMA_PAGO = -99999;
                                regDoc.NIT_CONSUMIDOR = 0;//Convert.ToDecimal(txtNitEfImpEdt.Text);
                                regDoc.RAZON_SOCIAL = txtEmpresaFacAlmacenajeIntermedio.Text;

                                //lmqc para importadores
                                string strCodFactura = "G1AI";//ObtenerCodigoFacturaImportacion(Convert.ToInt32(cmbTipoFacturaImpEdt.Value));

                                regDoc.OBSERVACIONES = strCodFactura + txtSecFacAlmacenajeIntermedio.Text + '|' + txtDuiFacProducto.Text + '|' + '|' + '|' + '|'
                                    + txtVolRecibidoFacAlmacenajeIntermedio.Text.Replace(",", ".") + '|' + txtFacProducto.Text + '-' + txtRazonSocialFacProducto.Text + '|' + '|' + '|' + '|'
                                    + '|' + txtObsFacAlmacenajeIntermedio.Text.Trim();

                                lstDocumentoMov.Add(regDoc);
                            }

                            if (txtSecFacAlmacenajeFinal.Text != "0")
                            {
                                var idDocumento = Convert.ToInt64(Session[CVariablesSesion.IdAuxiliar]);
                                if (idDocumento != 0)
                                {
                                    var index = dgvDocumentosImp.FocusedRowIndex;
                                    strIdDoc = dgvDocumentosImp.GetRowValues(index, "ID_TIPO_TRANSACCION").ToString();
                                    strDoc = dgvDocumentosImp.GetRowValues(index, "TIPO_TRANSACCION") + ";" + txtIdPadreImpEdt.Text.Trim();
                                    lstDocumentoMov.RemoveAll(x => x.ID_TRANSACCION == idDocumento);
                                }
                                else
                                {
                                    //strIdDoc = cmbTipoDocEfImpEdt.Value.ToString();
                                    //strDoc = cmbTipoDocEfImpEdt.Text;
                                }

                                //var strPadreTransaccion = strDoc.Split(';');
                                var rnd = new Random();
                                var regDoc = new O_LISTA_TRANSACCION_CTY();
                                regDoc.ID_TRANSACCION = (idDocumento > 0) ? idDocumento : rnd.Next(-99999, -1);
                                regDoc.FECHA_HORA_EMISION = Convert.ToDecimal(detFechaFacproducto.Date.Year + detFechaFacproducto.Date.Month.ToString().PadLeft(2, '0') + detFechaFacproducto.Date.Day.ToString().PadLeft(2, '0') + detFechaFacproducto.Date.Hour.ToString().PadLeft(2, '0') + detFechaFacproducto.Date.Minute.ToString().PadLeft(2, '0') + detFechaFacproducto.Date.Second.ToString().PadLeft(2, '0'));
                                regDoc.FECHA_HORA = detFechaFacproducto.Date.ToString();
                                regDoc.ID_TIPO_TRANSACCION = 3;
                                regDoc.TIPO_TRANSACCION = "";// strPadreTransaccion[0].Trim();
                                regDoc.ID_PADRE_TRANSACCION = 1;// Convert.ToDecimal(txtIdPadreImpEdt.Text);
                                regDoc.TIPO_CREDITO_FISCAL = "";
                                regDoc.TIPO_EMISOR = "";
                                regDoc.FORMA_PAGO = "";
                                regDoc.OBSERVACIONES = txtObsFacAlmacenajeFinal.Text;
                                regDoc.ID_ESTADO_TRANSACCION = 13;
                                regDoc.ID_TIPO_MONEDA = 1;
                                regDoc.USUARIO_TRANSACCION = "";
                                regDoc.NRO_AUTORIZACION = 0;
                                regDoc.NRO_TRANSACCION = txtFacAlmacenajeFinal.Text;
                                regDoc.CODIGO_CONTROL = "";
                                regDoc.PRECIO_UNITARIO = Convert.ToDecimal(txtCostoUniFacAlmacenajeFinal.Text.Replace(",", "."));
                                regDoc.CANTIDAD = 0;
                                regDoc.PRECIO_TOTAL = 0;
                                regDoc.ID_TIPO_CREDITO_FISCAL = -99999;
                                regDoc.ID_TIPO_EMISOR = 14;
                                regDoc.ID_FORMA_PAGO = -99999;
                                regDoc.NIT_CONSUMIDOR = 0;//Convert.ToDecimal(txtNitEfImpEdt.Text);
                                regDoc.RAZON_SOCIAL = txtEmpresaAlmacenajeFinal.Text;

                                //lmqc para importadores
                                string strCodFactura = "G2ADF";//ObtenerCodigoFacturaImportacion(Convert.ToInt32(cmbTipoFacturaImpEdt.Value));

                                regDoc.OBSERVACIONES = strCodFactura + txtSecFacAlmacenajeFinal.Text + '|' + txtDuiFacProducto.Text + '|' + '|' + '|' + '|'
                                    + txtVolRecibidoFacAlmacenajeFinal.Text.Replace(",", ".") + '|' + txtFacProducto.Text + '-' + txtRazonSocialFacProducto.Text + '|' + '|' + '|' + '|'
                                    + '|' + txtObsFacAlmacenajeFinal.Text.Trim();

                                lstDocumentoMov.Add(regDoc);
                            }

                            #endregion
                        }

                        if (a == 3)
                        {
                            #region aduana

                            var idDocumento = Convert.ToInt64(Session[CVariablesSesion.IdAuxiliar]);
                            if (idDocumento != 0)
                            {
                                var index = dgvDocumentosImp.FocusedRowIndex;
                                lstDocumentoMov.RemoveAll(x => x.ID_TRANSACCION == idDocumento);
                            }

                            //var strPadreTransaccion = strDoc.Split(';');
                            var rnd = new Random();
                            var regDoc = new O_LISTA_TRANSACCION_CTY();
                            regDoc.ID_TRANSACCION = (idDocumento > 0) ? idDocumento : rnd.Next(-99999, -1);
                            regDoc.FECHA_HORA_EMISION = Convert.ToDecimal(detFechaFacproducto.Date.Year + detFechaFacproducto.Date.Month.ToString().PadLeft(2, '0') + detFechaFacproducto.Date.Day.ToString().PadLeft(2, '0') + detFechaFacproducto.Date.Hour.ToString().PadLeft(2, '0') + detFechaFacproducto.Date.Minute.ToString().PadLeft(2, '0') + detFechaFacproducto.Date.Second.ToString().PadLeft(2, '0'));
                            regDoc.FECHA_HORA = detFechaFacproducto.Date.ToString();
                            regDoc.ID_TIPO_TRANSACCION = 3;
                            regDoc.TIPO_TRANSACCION = "";// strPadreTransaccion[0].Trim();
                            regDoc.ID_PADRE_TRANSACCION = 1;// Convert.ToDecimal(txtIdPadreImpEdt.Text);
                            regDoc.TIPO_CREDITO_FISCAL = "";
                            regDoc.TIPO_EMISOR = "";
                            regDoc.FORMA_PAGO = "";
                            regDoc.OBSERVACIONES = txtObsFacAduana.Text;
                            regDoc.ID_ESTADO_TRANSACCION = 13;
                            regDoc.ID_TIPO_MONEDA = 1;
                            regDoc.USUARIO_TRANSACCION = "";
                            regDoc.NRO_AUTORIZACION = 0;
                            regDoc.NRO_TRANSACCION = "-99999";//txtNoDocRefEf.Text;
                            regDoc.CODIGO_CONTROL = "";
                            regDoc.PRECIO_UNITARIO = 0;
                            regDoc.CANTIDAD = 0;

                            decimal decGravamen = Convert.ToDecimal(txtGravamenFacAduana.Text.Replace(",", "."));
                            decimal decSiduinea = Convert.ToDecimal(txtSiduineaFacAduana.Text.Replace(",", "."));
                            decimal decDepArancel = Convert.ToDecimal(txtDepArancelarioFacAduana.Text.Replace(",", "."));

                            decimal decTotalAduana = decGravamen + decSiduinea + decDepArancel;

                            regDoc.PRECIO_TOTAL = Math.Round(decTotalAduana, 2);
                            regDoc.ID_TIPO_CREDITO_FISCAL = -99999;
                            regDoc.ID_TIPO_EMISOR = 14;
                            regDoc.ID_FORMA_PAGO = -99999;
                            regDoc.NIT_CONSUMIDOR = 0;//Convert.ToDecimal(txtNitEfImpEdt.Text);
                            regDoc.RAZON_SOCIAL = "";//txtClienteEfImpEdt.Text;

                            //lmqc para importadores
                            string strCodFactura = "ECC";//ObtenerCodigoFacturaImportacion(Convert.ToInt32(cmbTipoFacturaImpEdt.Value));

                            regDoc.OBSERVACIONES = strCodFactura + '1' + '|' + txtDuiFacProducto.Text + '|' + '|' + '|' + '|'
                                + '|' + '|' + '|' + txtGravamenFacAduana.Text.Replace(",", ".") + '|' + txtSiduineaFacAduana.Text.Replace(",", ".") + '|'
                                + txtDepArancelarioFacAduana.Text.Replace(",", ".") + '|' + txtObsFacAduana.Text.Trim();

                            lstDocumentoMov.Add(regDoc);

                            #endregion
                        }

                        if (a == 4)
                        {
                            #region seguro

                            var idDocumento = Convert.ToInt64(Session[CVariablesSesion.IdAuxiliar]);
                            if (idDocumento != 0)
                            {
                                var index = dgvDocumentosImp.FocusedRowIndex;
                                strIdDoc = dgvDocumentosImp.GetRowValues(index, "ID_TIPO_TRANSACCION").ToString();
                                strDoc = dgvDocumentosImp.GetRowValues(index, "TIPO_TRANSACCION") + ";" + txtIdPadreImpEdt.Text.Trim();
                                lstDocumentoMov.RemoveAll(x => x.ID_TRANSACCION == idDocumento);
                            }
                            else
                            {
                                //strIdDoc = cmbTipoDocEfImpEdt.Value.ToString();
                                //strDoc = cmbTipoDocEfImpEdt.Text;
                            }

                            //var strPadreTransaccion = strDoc.Split(';');
                            var rnd = new Random();
                            var regDoc = new O_LISTA_TRANSACCION_CTY();
                            regDoc.ID_TRANSACCION = (idDocumento > 0) ? idDocumento : rnd.Next(-99999, -1);
                            regDoc.FECHA_HORA_EMISION = Convert.ToDecimal(detFechaFacproducto.Date.Year + detFechaFacproducto.Date.Month.ToString().PadLeft(2, '0') + detFechaFacproducto.Date.Day.ToString().PadLeft(2, '0') + detFechaFacproducto.Date.Hour.ToString().PadLeft(2, '0') + detFechaFacproducto.Date.Minute.ToString().PadLeft(2, '0') + detFechaFacproducto.Date.Second.ToString().PadLeft(2, '0'));
                            regDoc.FECHA_HORA = detFechaFacproducto.Date.ToString();
                            regDoc.ID_TIPO_TRANSACCION = 3;
                            regDoc.TIPO_TRANSACCION = "";// strPadreTransaccion[0].Trim();
                            regDoc.ID_PADRE_TRANSACCION = 1;// Convert.ToDecimal(txtIdPadreImpEdt.Text);
                            regDoc.TIPO_CREDITO_FISCAL = "";
                            regDoc.TIPO_EMISOR = "";
                            regDoc.FORMA_PAGO = "";
                            regDoc.OBSERVACIONES = txtObsFacSeguro.Text;
                            regDoc.ID_ESTADO_TRANSACCION = 13;
                            regDoc.ID_TIPO_MONEDA = 1;
                            regDoc.USUARIO_TRANSACCION = "";
                            regDoc.NRO_AUTORIZACION = 0;
                            regDoc.NRO_TRANSACCION = txtNumFacSeguro.Text;
                            regDoc.CODIGO_CONTROL = "";
                            regDoc.PRECIO_UNITARIO = 0;
                            regDoc.CANTIDAD = 0;
                            regDoc.PRECIO_TOTAL = Convert.ToDecimal(txtMontoFacSeguro.Text.Replace(",", "."));
                            regDoc.ID_TIPO_CREDITO_FISCAL = -99999;
                            regDoc.ID_TIPO_EMISOR = 14;
                            regDoc.ID_FORMA_PAGO = -99999;
                            regDoc.NIT_CONSUMIDOR = 0;//Convert.ToDecimal(txtNitEfImpEdt.Text);
                            regDoc.RAZON_SOCIAL = txtEmpAseguradoraFacSeguro.Text;

                            //lmqc para importadores
                            string strCodFactura = "CCS";//ObtenerCodigoFacturaImportacion(Convert.ToInt32(cmbTipoFacturaImpEdt.Value));

                            regDoc.OBSERVACIONES = strCodFactura + '1' + '|' + txtDuiFacProducto.Text + '|' + '|' + '|' + '|'
                                + '|' + '|' + '|' + '|' + '|' + '|' + txtObsFacSeguro.Text.Trim();

                            lstDocumentoMov.Add(regDoc);

                            #endregion
                        }

                        if (a == 5)
                        {
                            #region volumen base

                            var idDocumento = Convert.ToInt64(Session[CVariablesSesion.IdAuxiliar]);
                            if (idDocumento != 0)
                            {
                                var index = dgvDocumentosImp.FocusedRowIndex;
                                strIdDoc = dgvDocumentosImp.GetRowValues(index, "ID_TIPO_TRANSACCION").ToString();
                                strDoc = dgvDocumentosImp.GetRowValues(index, "TIPO_TRANSACCION") + ";" + txtIdPadreImpEdt.Text.Trim();
                                lstDocumentoMov.RemoveAll(x => x.ID_TRANSACCION == idDocumento);
                            }
                            else
                            {
                                //strIdDoc = cmbTipoDocEfImpEdt.Value.ToString();
                                //strDoc = cmbTipoDocEfImpEdt.Text;
                            }

                            //var strPadreTransaccion = strDoc.Split(';');
                            var rnd = new Random();
                            var regDoc = new O_LISTA_TRANSACCION_CTY();
                            regDoc.ID_TRANSACCION = (idDocumento > 0) ? idDocumento : rnd.Next(-99999, -1);
                            regDoc.FECHA_HORA_EMISION = Convert.ToDecimal(detFechaFacproducto.Date.Year + detFechaFacproducto.Date.Month.ToString().PadLeft(2, '0') + detFechaFacproducto.Date.Day.ToString().PadLeft(2, '0') + detFechaFacproducto.Date.Hour.ToString().PadLeft(2, '0') + detFechaFacproducto.Date.Minute.ToString().PadLeft(2, '0') + detFechaFacproducto.Date.Second.ToString().PadLeft(2, '0'));
                            regDoc.FECHA_HORA = detFechaFacproducto.Date.ToString();
                            regDoc.ID_TIPO_TRANSACCION = 3;
                            regDoc.TIPO_TRANSACCION = "";// strPadreTransaccion[0].Trim();
                            regDoc.ID_PADRE_TRANSACCION = 1;// Convert.ToDecimal(txtIdPadreImpEdt.Text);
                            regDoc.TIPO_CREDITO_FISCAL = "";
                            regDoc.TIPO_EMISOR = "";
                            regDoc.FORMA_PAGO = "";
                            regDoc.OBSERVACIONES = "";
                            regDoc.ID_ESTADO_TRANSACCION = 13;
                            regDoc.ID_TIPO_MONEDA = 1;
                            regDoc.USUARIO_TRANSACCION = "";
                            regDoc.NRO_AUTORIZACION = 0;
                            regDoc.NRO_TRANSACCION = "0";
                            regDoc.CODIGO_CONTROL = "";
                            regDoc.PRECIO_UNITARIO = 0;
                            regDoc.CANTIDAD = 0;
                            regDoc.PRECIO_TOTAL = Convert.ToDecimal(0);
                            regDoc.ID_TIPO_CREDITO_FISCAL = -99999;
                            regDoc.ID_TIPO_EMISOR = 14;
                            regDoc.ID_FORMA_PAGO = -99999;
                            regDoc.NIT_CONSUMIDOR = 0;//Convert.ToDecimal(txtNitEfImpEdt.Text);
                            regDoc.RAZON_SOCIAL = "";

                            //lmqc para importadores
                            string strCodFactura = "CVGB";//ObtenerCodigoFacturaImportacion(Convert.ToInt32(cmbTipoFacturaImpEdt.Value));

                            regDoc.OBSERVACIONES = strCodFactura + "1" + '|' + txtDuiFacProducto.Text + '|' + txtPlantaV.Text + '|' + '|' + txtVolumenIyA.Text.Replace(",", ".") + '|'
                                    + txtVolumenGb.Text.Replace(",", ".") + '|' + '|' + '|' + '|' + '|'
                                    + '|' + "";

                            lstDocumentoMov.Add(regDoc);

                            #endregion
                        }

                        if (a == 6)
                        {
                            #region transporte masivo de cisternas

                            var lstRegistros = Session[CVariablesSesion.GrillaTransCisMas] as List<CRegistrosImportadores>;

                            foreach (var dato in lstRegistros)
                            {
                                var idDocumento = Convert.ToInt64(Session[CVariablesSesion.IdAuxiliar]);
                                if (idDocumento != 0)
                                {
                                    var index = dgvDocumentosImp.FocusedRowIndex;
                                    strIdDoc = dgvDocumentosImp.GetRowValues(index, "ID_TIPO_TRANSACCION").ToString();
                                    strDoc = dgvDocumentosImp.GetRowValues(index, "TIPO_TRANSACCION") + ";" + txtIdPadreImpEdt.Text.Trim();
                                    lstDocumentoMov.RemoveAll(x => x.ID_TRANSACCION == idDocumento);
                                }
                                else
                                {
                                    //strIdDoc = cmbTipoDocEfImpEdt.Value.ToString();
                                    //strDoc = cmbTipoDocEfImpEdt.Text;
                                }

                                //var strPadreTransaccion = strDoc.Split(';');
                                var rnd = new Random();
                                var regDoc = new O_LISTA_TRANSACCION_CTY();
                                regDoc.ID_TRANSACCION = (idDocumento > 0) ? idDocumento : rnd.Next(-99999, -1);
                                regDoc.FECHA_HORA_EMISION = Convert.ToDecimal(detFechaFacproducto.Date.Year + detFechaFacproducto.Date.Month.ToString().PadLeft(2, '0') + detFechaFacproducto.Date.Day.ToString().PadLeft(2, '0') + detFechaFacproducto.Date.Hour.ToString().PadLeft(2, '0') + detFechaFacproducto.Date.Minute.ToString().PadLeft(2, '0') + detFechaFacproducto.Date.Second.ToString().PadLeft(2, '0'));
                                regDoc.FECHA_HORA = detFechaFacproducto.Date.ToString();
                                regDoc.ID_TIPO_TRANSACCION = 3;
                                regDoc.TIPO_TRANSACCION = "";// strPadreTransaccion[0].Trim();
                                regDoc.ID_PADRE_TRANSACCION = 1;// Convert.ToDecimal(txtIdPadreImpEdt.Text);
                                regDoc.TIPO_CREDITO_FISCAL = "";
                                regDoc.TIPO_EMISOR = "";
                                regDoc.FORMA_PAGO = "";
                                regDoc.OBSERVACIONES = dato.StrObservacion;
                                regDoc.ID_ESTADO_TRANSACCION = 13;
                                regDoc.ID_TIPO_MONEDA = 1;
                                regDoc.USUARIO_TRANSACCION = "";
                                regDoc.NRO_AUTORIZACION = 0;
                                regDoc.NRO_TRANSACCION = dato.StrFactTransporte;
                                regDoc.CODIGO_CONTROL = "";
                                regDoc.PRECIO_UNITARIO = Convert.ToDecimal(dato.StrCostoUnitario.Replace(",", "."));
                                regDoc.CANTIDAD = 0;
                                regDoc.PRECIO_TOTAL = 0;
                                regDoc.ID_TIPO_CREDITO_FISCAL = -99999;
                                regDoc.ID_TIPO_EMISOR = 14;
                                regDoc.ID_FORMA_PAGO = -99999;
                                regDoc.NIT_CONSUMIDOR = 0;//Convert.ToDecimal(txtNitEfImpEdt.Text);
                                regDoc.RAZON_SOCIAL = dato.StrEmpresa;

                                //lmqc para importadores
                                string strCodFactura = "B2CCTT";//ObtenerCodigoFacturaImportacion(Convert.ToInt32(cmbTipoFacturaImpEdt.Value));

                                regDoc.OBSERVACIONES = strCodFactura + dato.StrFactTransporte + '|' + dato.StrDui + '|' + dato.StrPlantaDespacho + '|' + dato.StrPlantaRecepcion + '|' + dato.DecVolumenDespachado.ToString().Replace(",", ".") + '|'
                                    + dato.DecVolumenRecibido.ToString().Replace(",", ".") + '|' + dato.StrNroFactProveedor + '-' + dato.StrProveedor + '|' + dato.StrFleteTransporte.Replace(",", ".") + '|' + '|' + '|'
                                    + Convert.ToDecimal(dato.StrCostoUnitario.Replace(",", ".")) + '|' + dato.StrObservacion;

                                lstDocumentoMov.Add(regDoc);
                            }

                            #endregion

                        }

                        dgvDocumentosImp.DataSource = null;
                        dgvDocumentosImp.DataBind();
                        Session[CVariablesSesion.GrillaAuxiliar] = lstDocumentoMov;
                        dgvDocumentosImp.DataSource = lstDocumentoMov;
                        dgvDocumentosImp.DataBind();

                        #endregion
                    }
                    else if (txtIdDocEfImpEdt.Text == "0")
                    {
                        #region Proceso a Ejecutar

                        //var txtClienteEfImpEdt = (dgvTransaccionesImp.FindEditFormTemplateControl("txtClienteEfImpEdt")) as ASPxTextBox;
                        //var txtNitEfImpEdt = (dgvTransaccionesImp.FindEditFormTemplateControl("txtNitEfImpEdt")) as ASPxTextBox;
                        var dgvDocumentosImp = (dgvTransaccionesImp.FindEditFormTemplateControl("dgvDocumentosImp")) as ASPxGridView;
                        var cmbTipoDocEfImpEdt = (dgvTransaccionesImp.FindEditFormTemplateControl("cmbTipoDocEfImpEdt")) as ASPxComboBox;

                        var lstDocumentoMov = new List<O_LISTA_TRANSACCION_CTY>();
                        if (Session[CVariablesSesion.GrillaAuxiliar] != null)
                        {
                            lstDocumentoMov = (List<O_LISTA_TRANSACCION_CTY>)Session[CVariablesSesion.GrillaAuxiliar];
                        }

                        if (Session[CVariablesSesion.GrillaAuxiliar] != null)
                        {
                            dgvDocumentosImp.DataSource = Session[CVariablesSesion.GrillaAuxiliar];
                            dgvDocumentosImp.DataBind();
                        }

                        string strIdDoc, strDoc;
                        var idDocumento = Convert.ToInt64(Session[CVariablesSesion.IdAuxiliar]);
                        if (idDocumento != 0)
                        {
                            var index = dgvDocumentosImp.FocusedRowIndex;
                            strIdDoc = dgvDocumentosImp.GetRowValues(index, "ID_TIPO_TRANSACCION").ToString();
                            strDoc = dgvDocumentosImp.GetRowValues(index, "TIPO_TRANSACCION") + ";" + txtIdPadreImpEdt.Text.Trim();
                            lstDocumentoMov.RemoveAll(x => x.ID_TRANSACCION == idDocumento);
                        }
                        else
                        {
                            strIdDoc = cmbTipoDocEfImpEdt.Value.ToString();
                            strDoc = cmbTipoDocEfImpEdt.Text;
                        }

                        var strPadreTransaccion = strDoc.Split(';');
                        var rnd = new Random();
                        var regDoc = new O_LISTA_TRANSACCION_CTY();
                        regDoc.ID_TRANSACCION = (idDocumento > 0) ? idDocumento : rnd.Next(-99999, -1);
                        regDoc.FECHA_HORA_EMISION = Convert.ToDecimal(detFechaDocEfImpEdt.Date.Year + detFechaDocEfImpEdt.Date.Month.ToString().PadLeft(2, '0') + detFechaDocEfImpEdt.Date.Day.ToString().PadLeft(2, '0') + detFechaDocEfImpEdt.Date.Hour.ToString().PadLeft(2, '0') + detFechaDocEfImpEdt.Date.Minute.ToString().PadLeft(2, '0') + detFechaDocEfImpEdt.Date.Second.ToString().PadLeft(2, '0'));
                        regDoc.FECHA_HORA = detFechaDocEfImpEdt.Date.ToString();
                        regDoc.ID_TIPO_TRANSACCION = Convert.ToInt32(strIdDoc);
                        regDoc.TIPO_TRANSACCION = strPadreTransaccion[0].Trim();
                        regDoc.ID_PADRE_TRANSACCION = Convert.ToDecimal(txtIdPadreImpEdt.Text);
                        regDoc.TIPO_CREDITO_FISCAL = "";
                        regDoc.TIPO_EMISOR = "";
                        regDoc.FORMA_PAGO = "";
                        regDoc.OBSERVACIONES = txtObsDocEfImpEdt.Text;
                        regDoc.ID_ESTADO_TRANSACCION = 13;
                        regDoc.ID_TIPO_MONEDA = 1;
                        regDoc.USUARIO_TRANSACCION = "";

                        switch (Convert.ToInt32(strPadreTransaccion[1].Trim()))
                        {
                            case (int)EnumTipoTrasaccionPadre.Factura:
                                regDoc.NRO_AUTORIZACION = 0;//lmqc
                                regDoc.NRO_TRANSACCION = txtNoFactEfImpEdt.Text;
                                regDoc.CODIGO_CONTROL = "";
                                regDoc.PRECIO_UNITARIO = Convert.ToDecimal(txtPrecioEfImpEdt.Text == "" ? "0" : txtPrecioEfImpEdt.Text.Replace(",", "."));
                                regDoc.CANTIDAD = Convert.ToDecimal(txtFactVolRecibidoImpEdt.Text == "" ? "0" : txtFactVolRecibidoImpEdt.Text.Replace(",", "."));
                                regDoc.PRECIO_TOTAL = Convert.ToDecimal(txtImporteEfImpEdt.Text == "" ? "0" : txtImporteEfImpEdt.Text.Replace(",", "."));
                                regDoc.ID_TIPO_CREDITO_FISCAL = -99999;
                                regDoc.ID_TIPO_EMISOR = 14;
                                regDoc.ID_FORMA_PAGO = -99999;
                                regDoc.NIT_CONSUMIDOR = Convert.ToDecimal(txtNitEfImpEdt.Text == "" ? "0" : txtNitEfImpEdt.Text);
                                regDoc.RAZON_SOCIAL = txtClienteEfImpEdt.Text.Trim();
                                break;
                            case (int)EnumTipoTrasaccionPadre.Autorizacion:
                                regDoc.NRO_AUTORIZACION = 0;
                                regDoc.NRO_TRANSACCION = txtNoDocRefEf.Text;
                                regDoc.CODIGO_CONTROL = "";
                                regDoc.PRECIO_UNITARIO = 0;
                                regDoc.CANTIDAD = 0;
                                regDoc.PRECIO_TOTAL = 0;
                                regDoc.ID_TIPO_CREDITO_FISCAL = -99999;
                                regDoc.ID_TIPO_EMISOR = -99999;
                                regDoc.ID_FORMA_PAGO = -99999;
                                regDoc.NIT_CONSUMIDOR = Convert.ToDecimal(txtNitEfImpEdt.Text);
                                regDoc.RAZON_SOCIAL = txtClienteEfImpEdt.Text.Trim();
                                break;
                            default:
                                regDoc.NRO_AUTORIZACION = 0;//lmqc
                                regDoc.NRO_TRANSACCION = txtNoFactEfImpEdt.Text;
                                regDoc.CODIGO_CONTROL = "0";
                                regDoc.PRECIO_UNITARIO = Convert.ToDecimal(txtPrecioEfImpEdt.Text.Replace(",", "."));
                                regDoc.CANTIDAD = Convert.ToDecimal(txtFactVolRecibidoImpEdt.Text.Replace(",", "."));
                                regDoc.PRECIO_TOTAL = Convert.ToDecimal(txtImporteEfImpEdt.Text.Replace(",", "."));
                                regDoc.ID_TIPO_CREDITO_FISCAL = -99999;
                                regDoc.ID_TIPO_EMISOR = 14;
                                regDoc.ID_FORMA_PAGO = -99999;
                                regDoc.NIT_CONSUMIDOR = Convert.ToDecimal(txtNitEfImpEdt.Text);
                                regDoc.RAZON_SOCIAL = txtClienteEfImpEdt.Text;
                                break;
                        }

                        //lmqc para importadores
                        string strCodFactura = ObtenerCodFactImp(Convert.ToInt32(txtMarcaFactura.Text));

                        if (strCodFactura == "ECC")
                        {
                            regDoc.PRECIO_TOTAL = Convert.ToDecimal(txtFactGravamenArancelarioImpEdt.Text.Replace(",", ".")) + Convert.ToDecimal(txtFactSiduineaImpEdt.Text.Replace(",", ".")) + Convert.ToDecimal(txtFactCostoArancelarioImpEdt.Text.Replace(",", "."));
                        }

                        regDoc.OBSERVACIONES = strCodFactura + txtSecFactImpEdt.Text + '|' + txtFactDuiImpEdt.Text + '|' + txtFactPlantaOrigenImpEdt.Text + '|' + txtFactPlantaDestinoImpEdt.Text + '|' + txtFactVolDespachadoImpEdt.Text.Replace(",", ".") + '|'
                            + txtFactVolRecibidoImpEdt.Text.Replace(",", ".") + '|' + txtFactProveedorImpEdt.Text + '-' + txtProveedorImpEdt.Text + '|' + txtFactFleteTransporteImpEdt.Text.Replace(",", ".") + '|' + txtFactGravamenArancelarioImpEdt.Text.Replace(",", ".") + '|' + txtFactSiduineaImpEdt.Text.Replace(",", ".") + '|'
                            + txtFactCostoArancelarioImpEdt.Text.Replace(",", ".") + '|' + txtObsDocEfImpEdt.Text.Trim();

                        lstDocumentoMov.Add(regDoc);

                        dgvDocumentosImp.DataSource = null;
                        dgvDocumentosImp.DataBind();
                        Session[CVariablesSesion.GrillaAuxiliar] = lstDocumentoMov;
                        dgvDocumentosImp.DataSource = lstDocumentoMov;
                        dgvDocumentosImp.DataBind();

                        #endregion
                    }
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

        protected void dgvDocumentosImp_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    var dgvDocumentosImp = (dgvTransaccionesImp.FindEditFormTemplateControl("dgvDocumentosImp")) as ASPxGridView;
                    if (Session[CVariablesSesion.GrillaAuxiliar] != null)
                    {
                        var lstDocumentoEdt = new List<O_LISTA_TRANSACCION_CTY>();
                        lstDocumentoEdt = (List<O_LISTA_TRANSACCION_CTY>)Session[CVariablesSesion.GrillaAuxiliar];
                        dgvDocumentosImp.DataSource = lstDocumentoEdt;
                        dgvDocumentosImp.DataBind();
                    }

                    int index = dgvDocumentosImp.FocusedRowIndex;
                    long lngIdDoc = Convert.ToInt64(dgvDocumentosImp.GetRowValues(index, "ID_TRANSACCION"));
                    if (lngIdDoc != 0)
                    {
                        var lstDocumentoMov = new List<O_LISTA_TRANSACCION_CTY>();
                        lstDocumentoMov = (List<O_LISTA_TRANSACCION_CTY>)Session[CVariablesSesion.GrillaAuxiliar];
                        if (e.ButtonID == "btnQuitarImTran")
                        {
                            lstDocumentoMov.ForEach(a => a.ID_ESTADO_TRANSACCION = (a.ID_TRANSACCION == lngIdDoc) ? (int)EnumEstadoMov.Anulado : a.ID_ESTADO_TRANSACCION);
                        }
                        else
                        {
                            lstDocumentoMov.ForEach(a => a.ID_ESTADO_TRANSACCION = (a.ID_TRANSACCION == lngIdDoc) ? (int)EnumEstadoMov.Activo : a.ID_ESTADO_TRANSACCION);
                        }
                        Session[CVariablesSesion.GrillaAuxiliar] = lstDocumentoMov;
                        dgvDocumentosImp.DataSource = lstDocumentoMov;
                        dgvDocumentosImp.DataBind();
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

        protected void dgvDocumentosImp_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            try
            {
                #region Proceso a Ejecutar

                ASPxGridView dgvDocumentosImp = (dgvTransaccionesImp.FindEditFormTemplateControl("dgvDocumentosImp")) as ASPxGridView;
                if (e.VisibleIndex >= 0)
                {
                    if (dgvDocumentosImp.GetRowValues(e.VisibleIndex, "ID_ESTADO_TRANSACCION").ToString() == ((int)EnumEstadoMov.Anulado).ToString())
                        e.Row.BackColor = Color.FromArgb(249, 131, 131);
                    else
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

        private string ObtenerCodigoFacturaImportacion(int p)//lmqc --- importadores
        {
            var strCodigoFactura = "";

            switch (p)
            {
                case (int)EnumColumnasGrilla.FaAccp:
                    strCodigoFactura = "ACCP";
                    break;
                case (int)EnumColumnasGrilla.FaB1cctf:
                    strCodigoFactura = "B1CCTF";
                    break;
                case (int)EnumColumnasGrilla.FaB2cctt:
                    strCodigoFactura = "B2CCTT";
                    break;
                case (int)EnumColumnasGrilla.FaB3cctf:
                    strCodigoFactura = "B3CCTF";
                    break;
                case (int)EnumColumnasGrilla.FaCcs:
                    strCodigoFactura = "CCS";
                    break;
                case (int)EnumColumnasGrilla.FaD1io:
                    strCodigoFactura = "D1IO";
                    break;
                case (int)EnumColumnasGrilla.FaD2ii:
                    strCodigoFactura = "D2II";
                    break;
                case (int)EnumColumnasGrilla.FaD3idf:
                    strCodigoFactura = "D3IDF";
                    break;
                case (int)EnumColumnasGrilla.FaEcc:
                    strCodigoFactura = "ECC";
                    break;
                case (int)EnumColumnasGrilla.FaF1pd:
                    strCodigoFactura = "F1PD";
                    break;
                case (int)EnumColumnasGrilla.FaF2ps:
                    strCodigoFactura = "F2PS";
                    break;
                case (int)EnumColumnasGrilla.FaG1ai:
                    strCodigoFactura = "G1AI";
                    break;
                case (int)EnumColumnasGrilla.FaG2adf:
                    strCodigoFactura = "G2ADF";
                    break;
                case (int)EnumColumnasGrilla.FaHcm:
                    strCodigoFactura = "HCM";
                    break;
                case (int)EnumColumnasGrilla.FaI1di:
                    strCodigoFactura = "I1DI";
                    break;
                case (int)EnumColumnasGrilla.FaI2df:
                    strCodigoFactura = "I2DF";
                    break;
                default:
                    strCodigoFactura = "N/N";
                    break;
            }


            return strCodigoFactura;
        }

        private string ObtenerCodFactImp(int p)
        {
            var strCodigoFactura = "";

            switch (p)
            {
                case 0:
                    strCodigoFactura = "ACCP";
                    break;
                case 1:
                    strCodigoFactura = "B1CCTF";
                    break;
                case 2:
                    strCodigoFactura = "B2CCTT";
                    break;
                case 3:
                    strCodigoFactura = "B3CCTF";
                    break;
                case 4:
                    strCodigoFactura = "CCS";
                    break;
                case 5:
                    strCodigoFactura = "D1IO";
                    break;
                case 6:
                    strCodigoFactura = "D2II";
                    break;
                case 7:
                    strCodigoFactura = "D3IDF";
                    break;
                case 8:
                    strCodigoFactura = "ECC";
                    break;
                case 9:
                    strCodigoFactura = "F1PD";
                    break;
                case 10:
                    strCodigoFactura = "F2PS";
                    break;
                case 11:
                    strCodigoFactura = "G1AI";
                    break;
                case 12:
                    strCodigoFactura = "G2ADF";
                    break;
                case 13:
                    strCodigoFactura = "HCM";
                    break;
                case 14:
                    strCodigoFactura = "I1DI";
                    break;
                case 15:
                    strCodigoFactura = "I2DF";
                    break;
                default:
                    strCodigoFactura = "N/N";
                    break;
            }


            return strCodigoFactura;
        }

        protected void pucDocumentoImp_WindowCallback(object source, PopupWindowCallbackArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    var objCatalogo = (O_OBTENER_CATALOGOS_CTY)Session[CVariablesSesion.ListaParametrica];

                    var lstParam = objCatalogo.CATALOGO_PARAMETRICAS_DOMINIO;
                    foreach (var param in lstParam)
                    {
                        var lstObtCatalogo = _client.Post<EResultadoParametricaGlobal>
                            (CAppSettings.svc_lista_parametrica_global,
                            new
                            {
                                decIdDominio = param.ID_CATALOGO
                            });

                        if (lstObtCatalogo.IntCodigo == 1)
                        {
                            List<O_PARAMETRICA_GLOBAL_CTY> objDefecto;
                            var objLstGlobal = lstObtCatalogo.OResultado;

                            switch (param.CODIGO)
                            {
                                case "FRMPAGO":
                                    objDefecto = objLstGlobal.Where(x => x.CODIGO == "EF").ToList();
                                    //cmbTipoPagoEf.DataSource = objLstGlobal;
                                    //cmbTipoPagoEf.DataBind();
                                    //cmbTipoPagoEf.SelectedItem = cmbTipoPagoEf.Items.FindByValue(objDefecto.First().ID_PARAMETRICA_GLOBAL.ToString());
                                    break;
                                case "TEFACT":
                                    objDefecto = objLstGlobal.Where(x => x.CODIGO == "FES").ToList();
                                    //cmbTipoEmisorEf.DataSource = objLstGlobal;
                                    //cmbTipoEmisorEf.DataBind();
                                    //cmbTipoEmisorEf.SelectedItem = cmbTipoEmisorEf.Items.FindByValue(objDefecto.First().ID_PARAMETRICA_GLOBAL.ToString());
                                    break;
                                case "TCFIS":
                                    objDefecto = objLstGlobal.Where(x => x.CODIGO == "FCF").ToList();
                                    //cmbTipoCredEf.DataSource = objLstGlobal;
                                    //cmbTipoCredEf.DataBind();
                                    //cmbTipoCredEf.SelectedItem = cmbTipoCredEf.Items.FindByValue(objDefecto.First().ID_PARAMETRICA_GLOBAL.ToString());
                                    break;

                                case "FACIMP":

                                    cmbTipoFacturaImpEdt.Visible = true;

                                    //objDefecto = objLstGlobal.Where(x => x.CODIGO == "FCF").ToList();
                                    cmbTipoFacturaImpEdt.DataSource = objLstGlobal;
                                    cmbTipoFacturaImpEdt.DataBind();
                                    //cmbTipoFacturaImp.SelectedItem = cmbTipoCredEf.Items.FindByValue(objDefecto.First().ID_PARAMETRICA_GLOBAL.ToString());
                                    break;
                            }
                        }
                        else
                        {
                            //todo No implementado.
                        }
                    }

                    var detFechaEf = (dgvTransaccionesImp.FindEditFormTemplateControl("detFechaEf")) as ASPxDateEdit;
                    var txtCantidadEf = (dgvTransaccionesImp.FindEditFormTemplateControl("txtCantidadEf")) as ASPxTextBox;
                    //var cmbTipoDocEfImpEdt = (dgvTransaccionesImp.FindEditFormTemplateControl("cmbTipoDocEfImpEdt")) as ASPxComboBox;
                    var dgvDocumentosImp = (dgvTransaccionesImp.FindEditFormTemplateControl("dgvDocumentosImp")) as ASPxGridView;
                    var cmbTipoFlujoEf = (dgvTransaccionesImp.FindEditFormTemplateControl("cmbTipoFlujoEf")) as ASPxComboBox;

                    var txtClienteEf = (dgvTransaccionesImp.FindEditFormTemplateControl("txtClienteEf")) as ASPxTextBox;//lmqc --- importadores

                    var cmbProducto = (dgvTransaccionesImp.FindEditFormTemplateControl("cmbProdEfImp")) as ASPxComboBox;

                    //var strTipoTransaccion = cmbTipoDocEfImpEdt.Text.Split(';');

                    //if (strTipoTransaccion.Count() > 1)
                    //{
                    //    txtIdPadre.Text = strTipoTransaccion[1];
                    //}
                    //else {
                    //    txtIdPadre.Text = "1";
                    //}

                    if (Session[CVariablesSesion.GrillaAuxiliar] != null)
                    {
                        dgvDocumentosImp.DataSource = Session[CVariablesSesion.GrillaAuxiliar];
                        dgvDocumentosImp.DataBind();
                    }

                    //txtCantidadDocEf.Text = txtCantidadEf.Text;

                    int index = dgvDocumentosImp.FocusedRowIndex;
                    if (index >= 0)
                    {
                        //cmbTipoDocEfImpEdt.DataSource = Session[CVariablesSesion.ListaTransaccion];
                        //cmbTipoDocEfImpEdt.DataBind();

                        Session[CVariablesSesion.IdAuxiliar] = Convert.ToInt64(dgvDocumentosImp.GetRowValues(index, "ID_TRANSACCION").ToString());
                        lblIdDocEf.Text = dgvDocumentosImp.GetRowValues(index, "ID_TRANSACCION").ToString();
                        var strFechaDocumento = dgvDocumentosImp.GetRowValues(index, "FECHA_HORA_EMISION").ToString();
                        //detFechaDocEfImpEdt.Date = DateTime.Parse("01/08/2019");
                        detFechaDocEfImpEdt.Date = DateTime.Parse(strFechaDocumento.Substring(6, 2) + "/" + strFechaDocumento.Substring(4, 2) + "/" + strFechaDocumento.Substring(0, 4));
                        //txtNoAutoEfImpEdt.Text = dgvDocumentosImp.GetRowValues(index, "NRO_AUTORIZACION").ToString();
                        txtNoFactEfImpEdt.Text = dgvDocumentosImp.GetRowValues(index, "NRO_TRANSACCION").ToString();
                        //txtCodCtrlEfImpEdt.Text = (dgvDocumentosImp.GetRowValues(index, "CODIGO_CONTROL") == null) ? "" : dgvDocumentosImp.GetRowValues(index, "CODIGO_CONTROL").ToString();
                        txtImporteEfImpEdt.Text = dgvDocumentosImp.GetRowValues(index, "PRECIO_TOTAL").ToString().Replace(".", ",");
                        txtPrecioEfImpEdt.Text = dgvDocumentosImp.GetRowValues(index, "PRECIO_UNITARIO").ToString().Replace(".", ",");
                        txtObsDocEfImpEdt.Text = (dgvDocumentosImp.GetRowValues(index, "OBSERVACIONES") == null) ? "" : dgvDocumentosImp.GetRowValues(index, "OBSERVACIONES").ToString();
                        //cmbTipoDocEfImpEdt.SelectedItem = cmbTipoDocEfImpEdt.Items.FindByValue(dgvDocumentosImp.GetRowValues(index, "ID_TIPO_TRANSACCION").ToString());
                        //cmbTipoEmisorEf.SelectedItem = cmbTipoEmisorEf.Items.FindByValue(dgvDocumentosImp.GetRowValues(index, "ID_TIPO_EMISOR").ToString());
                        //cmbTipoCredEf.SelectedItem = cmbTipoCredEf.Items.FindByValue(dgvDocumentosImp.GetRowValues(index, "ID_TIPO_CREDITO_FISCAL").ToString());
                        //cmbTipoPagoEf.SelectedItem = cmbTipoPagoEf.Items.FindByValue(dgvDocumentosImp.GetRowValues(index, "ID_FORMA_PAGO").ToString());
                        //cmbMonedaEf.SelectedItem = cmbMonedaEf.Items.FindByValue(dgvDocumentosImp.GetRowValues(index, "ID_TIPO_MONEDA").ToString());
                        txtUsuarioEf.Text = (dgvDocumentosImp.GetRowValues(index, "USUARIO_TRANSACCION") == null) ? "" : dgvDocumentosImp.GetRowValues(index, "USUARIO_TRANSACCION").ToString();
                        txtIdPadreImpEdt.Text = dgvDocumentosImp.GetRowValues(index, "ID_PADRE_TRANSACCION").ToString();
                        txtClienteEfImpEdt.Text = (dgvDocumentosImp.GetRowValues(index, "RAZON_SOCIAL") == null) ? "" : dgvDocumentosImp.GetRowValues(index, "RAZON_SOCIAL").ToString();
                        txtNitEfImpEdt.Text = (dgvDocumentosImp.GetRowValues(index, "NIT_CONSUMIDOR") == null) ? "" : dgvDocumentosImp.GetRowValues(index, "NIT_CONSUMIDOR").ToString();

                        //var strTipoTransaccion = cmbTipoDocEfImpEdt.Text.Split(';');

                        //if (strTipoTransaccion.Count() > 1)
                        //{
                        //    txtIdPadre.Text = strTipoTransaccion[1];
                        //}
                        //else
                        //{
                        //    txtIdPadre.Text = "1";
                        //}


                        //lmqc para importacion
                        var parametros = txtObsDocEfImpEdt.Text;

                        var intIdActividad = (int)tlsOperadores.FocusedNode.GetValue("IntIdActividad");

                        switch (intIdActividad)
                        {
                            case 16:
                            case 36:
                                if (parametros != "")
                                {
                                    string[] lista = parametros.Split('|');

                                    if (lista.Any())
                                    {
                                        if (lista.Count() == 12)
                                        {
                                            txtFactDuiImpEdt.Text = lista[1];
                                            txtFactPlantaOrigenImpEdt.Text = lista[2];
                                            txtFactPlantaDestinoImpEdt.Text = lista[3];
                                            txtFactVolDespachadoImpEdt.Text = lista[4];
                                            txtFactVolRecibidoImpEdt.Text = lista[5].Replace(".", ",");

                                            string[] listaProveedor = lista[6].Split('-');

                                            if (listaProveedor.Count() == 2)
                                            {
                                                txtFactProveedorImpEdt.Text = listaProveedor[0];
                                                txtProveedorImpEdt.Text = listaProveedor[1];
                                            }
                                            else
                                            {
                                                txtFactProveedorImpEdt.Text = "";
                                                txtProveedorImpEdt.Text = "";
                                            }

                                            txtFactFleteTransporteImpEdt.Text = lista[7];
                                            txtFactGravamenArancelarioImpEdt.Text = lista[8].Replace(".", ",");
                                            txtFactSiduineaImpEdt.Text = lista[9].Replace(".", ",");
                                            txtFactCostoArancelarioImpEdt.Text = lista[10].Replace(".", ",");

                                            txtObsDocEfImpEdt.Text = lista[11];

                                            int indiceVarFact = VariableIndiceFacturaImportacion(lista[0]);

                                            int secVarFAct = SecuenciaFacturaImportacion(lista[0]);

                                            txtSecFactImpEdt.Text = secVarFAct.ToString();

                                            cmbTipoFacturaImpEdt.SelectedIndex = indiceVarFact;

                                            txtMarcaFactura.Text = indiceVarFact.ToString();

                                            int val = Convert.ToInt32(cmbTipoFacturaImpEdt.Value);
                                            CargarControlesFacturasImportacion(val, cmbProducto.Text);
                                        }
                                    }
                                }
                                else if (parametros == "")
                                {
                                    txtFactDuiImpEdt.Text = "";
                                    txtFactCostoArancelarioImpEdt.Text = "";
                                    txtFactPlantaOrigenImpEdt.Text = "";
                                    txtFactPlantaDestinoImpEdt.Text = "";
                                    txtFactVolDespachadoImpEdt.Text = "";
                                    txtFactVolRecibidoImpEdt.Text = "";
                                    txtFactProveedorImpEdt.Text = "";
                                    txtProveedorImpEdt.Text = "";
                                    txtFactFleteTransporteImpEdt.Text = "";
                                    txtFactGravamenArancelarioImpEdt.Text = "0";
                                    txtFactSiduineaImpEdt.Text = "0";
                                    txtFactCostoArancelarioImpEdt.Text = "0";
                                    txtSecFactImpEdt.Text = "";

                                    txtObsDocEfImpEdt.Text = "";


                                }
                                else
                                {
                                    CargarControlesFacturasImportacion(0, "");

                                    lblSecFactImpEdt.Visible = false;
                                    txtSecFactImpEdt.Visible = false;

                                    cmbTipoFacturaImpEdt.Visible = false;
                                }

                                break;
                            default:
                                CargarControlesFacturasImportacion(0, "");

                                lblSecFactImpEdt.Visible = false;
                                txtSecFactImpEdt.Visible = false;

                                cmbTipoFacturaImpEdt.Visible = false;

                                break;
                        }
                    }
                    else
                    {
                        pucDocumentoImp.ShowOnPageLoad = false;
                        return;
                    }

                    txtIdDocEfImpEdt.ReadOnly = true;
                    txtIdPadreImpEdt.ReadOnly = true;

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

        protected void pucDocumentoImpAdd_WindowCallback(object source, PopupWindowCallbackArgs e)
        {
            if (MValidaSesion())
            {
                var txtDui = (dgvTransaccionesImp.FindEditFormTemplateControl("txtDui")) as ASPxTextBox;
                var txtFechaDui = (dgvTransaccionesImp.FindEditFormTemplateControl("detFechaEfImp")) as ASPxDateEdit;

                var cmbProducto = (dgvTransaccionesImp.FindEditFormTemplateControl("cmbProdEfImp")) as ASPxComboBox;

                txtDuiFacProducto.Text = txtDui.Text;
                detFechaFacproducto.Date = txtFechaDui.Date;
                txtProductoSeleccionado.Text = cmbProducto.Text;

            }
            else
            {
                //todo No implementado.
            }
        }

        private int SecuenciaFacturaImportacion(string p)//lmqc --- para importadores
        {
            var varSec = 0;

            try
            {
                #region Proceso a Ejecutar

                p = p.Replace("ACCP", "");
                p = p.Replace("B1CCTF", "");
                p = p.Replace("B2CCTT", "");
                p = p.Replace("B3CCTF", "");
                p = p.Replace("CCS", "");
                p = p.Replace("D1IO", "");
                p = p.Replace("D2II", "");
                p = p.Replace("D3IDF", "");
                p = p.Replace("ECC", "");
                p = p.Replace("F1PD", "");
                p = p.Replace("F2PS", "");
                p = p.Replace("G1AI", "");
                p = p.Replace("G2ADF", "");
                p = p.Replace("HCM", "");
                p = p.Replace("I1DI", "");
                p = p.Replace("I2DF", "");

                if (p != "")
                {
                    varSec = Convert.ToInt32(p);
                }
                else
                {
                    varSec = 0;
                }

                #endregion
            }
            catch (Exception exp)
            {
                #region RegistraError

                varSec = 0;

                #endregion
            }

            return varSec;
        }

        private int VariableIndiceFacturaImportacion(string p)//lmqc --- para importadores
        {
            int varIndice = 0;

            if (p.Contains("ACCP"))
            {
                varIndice = 0;
            }
            else if (p.Contains("B1CCTF"))
            {
                varIndice = 1;
            }
            else if (p.Contains("B2CCTT"))
            {
                varIndice = 2;
            }
            else if (p.Contains("B3CCTF"))
            {
                varIndice = 3;
            }
            else if (p.Contains("CCS"))
            {
                varIndice = 4;
            }
            else if (p.Contains("D1IO"))
            {
                varIndice = 5;
            }
            else if (p.Contains("D2II"))
            {
                varIndice = 6;
            }
            else if (p.Contains("D3IDF"))
            {
                varIndice = 7;
            }
            else if (p.Contains("ECC"))
            {
                varIndice = 8;
            }
            else if (p.Contains("F1PD"))
            {
                varIndice = 9;
            }
            else if (p.Contains("F2PS"))
            {
                varIndice = 10;
            }
            else if (p.Contains("G1AI"))
            {
                varIndice = 11;
            }
            else if (p.Contains("G2ADF"))
            {
                varIndice = 12;
            }
            else if (p.Contains("HCM"))
            {
                varIndice = 13;
            }
            else if (p.Contains("I1DI"))
            {
                varIndice = 14;
            }
            else if (p.Contains("I2DF"))
            {
                varIndice = 15;
            }
            else
            {
                varIndice = 0;
            }

            return varIndice;
        }

        protected void dgvTransaccionError_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {

        }

        protected void upcArchivosRA_FilesUploadComplete(object sender, FilesUploadCompleteEventArgs e)
        {
            if (upcArchivosRA.UploadedFiles != null && upcArchivosRA.UploadedFiles[0].ContentLength != 0)
            {
                var lstRegistros = new List<CRegistrosImportadores>();
                var lstRegistroE = new List<CRegistrosError>();

                for (int i = 0; i < upcArchivosRA.UploadedFiles.Length; i++)
                {
                    UploadedFile file = upcArchivosRA.UploadedFiles[i];

                    var reader = new StreamReader(file.FileContent);

                    long lngNoRegCorrect = 1, lngNoRegInCorrect = 1, lngNoReg = 1;

                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(';');

                        try
                        {
                            DateTime datFecha;
                            string strMensaje = string.Empty;
                            if (MValidaLineaImp(values, ref strMensaje))
                            {
                                var cDato = new CRegistrosImportadores();

                                #region Valida y genera objeto
                                cDato.LngId = lngNoRegCorrect;
                                cDato.DecSecuencia = Convert.ToDecimal(values[0]);
                                datFecha = DateTime.Parse(values[1].Trim());
                                cDato.StrFechaDui = datFecha.ToString();
                                cDato.StrDui = values[2].Trim();
                                cDato.StrNroFactProveedor = values[3].Trim();
                                cDato.StrProveedor = values[4].Trim();
                                cDato.StrEmpresa = values[5].Trim();
                                cDato.StrFactTransporte = values[6].Trim();
                                cDato.StrCostoUnitario = values[7].ToString().Trim();
                                cDato.StrFleteTransporte = values[8].ToString().Trim();
                                cDato.StrPlantaDespacho = values[9].Trim();
                                cDato.StrPlantaRecepcion = values[10].Trim();
                                cDato.DecVolumenDespachado = Convert.ToDecimal(values[11]);
                                cDato.DecVolumenRecibido = Convert.ToDecimal(values[12]);
                                cDato.StrObservacion = values[13].Trim();

                                #endregion

                                //cDato.objProtoMovimiento = objNuevoTipoReg;
                                lstRegistros.Add(cDato);
                                lngNoRegCorrect++;
                            }
                            else
                            {
                                //    var cRegError = new CRegistrosError();
                                //    cRegError.LngId = lngNoReg;
                                //    //cRegError.StrIdVenta = values[29].Trim();
                                //    cRegError.LngFechaEmision = values[0].Trim();
                                //    cRegError.LngNit = values[9].Trim();
                                //    cRegError.LngNroAutorizacion = values[12].Trim();
                                //    cRegError.LngNroFactura = values[11].Trim();
                                //    //cRegError.LngNroReferencia = values[29].Trim();
                                //    cRegError.StrErrores = strMensaje.Trim();
                                //    lstRegistroE.Add(cRegError);
                                //    lngNoRegInCorrect++;
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
                }

                Session[CVariablesSesion.GrillaTransCisMas] = lstRegistros;

                Session[CVariablesSesion.GrillaRegObservado] = lstRegistroE;
            }
        }

        protected void pnlFacTransMasImmAdd_Callback(object sender, CallbackEventArgsBase e)
        {
            var lstRegistros = new List<CRegistrosImportadores>();
            var lstRegistroE = new List<CRegistrosError>();

            if (Session[CVariablesSesion.GrillaTransCisMas] != null)
            {
                lstRegistros = (List<CRegistrosImportadores>)Session[CVariablesSesion.GrillaTransCisMas];
                dgvTransCisMas.DataSource = lstRegistros;
                dgvTransCisMas.DataBind();
            }
            else
            {
                dgvTransCisMas.DataSource = null;
                dgvTransCisMas.DataBind();
            }

            if (Session[CVariablesSesion.GrillaRegObservado] != null)
            {

                lblTipoError.Text = "Registros Observados - Visualización";
                lstRegistroE = (List<CRegistrosError>)Session[CVariablesSesion.GrillaRegObservado];

                if (lstRegistroE.Count() > 0)
                {
                    txtErrorMasivo.Text = "1";
                    dgvTransaccionError.DataSource = lstRegistroE;
                    dgvTransaccionError.DataBind();
                }
                else
                {
                    txtErrorMasivo.Text = "0";
                    lblTipoError.Text = "";
                    dgvTransaccionError.DataSource = null;
                    dgvTransaccionError.DataBind();
                }
            }
            else
            {
                txtErrorMasivo.Text = "0";
                lblTipoError.Text = "";
                dgvTransaccionError.DataSource = null;
                dgvTransaccionError.DataBind();
            }

        }

        private bool MValidaLineaImp(string[] values, ref string strMensaje)
        {
            bool strResultado = true;
            try
            {
                #region Proceso a Ejecutar

                //if (cmbTipoOperacion.Text.Contains("DG"))
                //{
                //    #region Despachos
                //    if (values.Length != 20)
                //    {
                //        strMensaje = "- Nro. de columnas del registro invalido.";
                //    }
                //    var arrayNumero = new[] { 2, 5, 8, 10, 11, 13, 14, 15, 17, 18 };
                //    decimal dec;
                //    for (int i = 0; i < arrayNumero.Length; i++)
                //    {
                //        if (!decimal.TryParse(values[arrayNumero[i]].Trim(), out dec))
                //        {
                //            strMensaje += "- Columna Nro. " + arrayNumero[i] + " No es un número.";
                //        }
                //    }
                //    if (values[0].Trim().Substring(6, 4) + values[0].Trim().Substring(3, 2) != cmbPeriodo.Value.ToString().Substring(0, 4) + cmbPeriodo.Value.ToString().Substring(4, 2))
                //    {
                //        strMensaje += "- Periodo incorrecto.";
                //    }
                //    DateTime dt;
                //    if (!DateTime.TryParse(values[0].Trim(), out dt))
                //    {
                //        strMensaje += "- Fecha incorrecta.";
                //    }
                //    if (MCodificaParametrica((int)EnumTipoParametrica.Producto, values[4].Trim()) == -99999)
                //    {
                //        strMensaje += "- Producto incorrecto.";
                //    }
                //    if (MCodificaParametrica((int)EnumTipoParametrica.UnidadMedida, values[4].Trim()) == -99999)
                //    {
                //        strMensaje += "- Unidad de medida invalida.";
                //    }
                //    if ((values[6].Trim() == "V" && Convert.ToDecimal(values[5]) <= 0) || (values[6].Trim() == "A" && Convert.ToDecimal(values[5]) < 0))
                //    {
                //        strMensaje += "- Cantidad incorrecta.";
                //    }
                //    if (Convert.ToDecimal(values[14].Trim()) > (Convert.ToDecimal(values[13].Trim()) * Convert.ToDecimal(values[5].Trim())) + 2 ||
                //        Convert.ToDecimal(values[14].Trim()) < (Convert.ToDecimal(values[13].Trim()) * Convert.ToDecimal(values[5].Trim())) - 2)
                //    {
                //        strMensaje += "- Importe incorrecto.";
                //    }
                //    if (values[12].Trim().Length == 1 && values[12].Trim() != "0")
                //    {
                //        strMensaje += "- Código de control incorrecto.";
                //    }
                //    if (values[12].Trim().Length > 1 && values[12].Trim().Length < 6)
                //    {
                //        strMensaje += "- Código de control incorrecto.";
                //    }
                //    if (values[3].Trim().Length > 1)
                //    {
                //        if (values[1].Trim().ContainsAny("-", "@", "/", "'", "\\", ".", ",", "_", "+", "[", "]", "{", "}", " "))
                //        {
                //            strMensaje += "- Identificador contiene caracteres invalidos.";
                //        }
                //    }

                //    if (Convert.ToDecimal(values[10].Trim()) == 0 && string.IsNullOrEmpty(values[18].Trim()) && string.IsNullOrEmpty(values[19].Trim()))
                //    {
                //        strMensaje += "- Autorización de venta invalido.";
                //    }
                //    if (values[7].Trim() != "EF" && values[7].Trim() != "PRE" && values[7].Trim() != "VAL")
                //    {
                //        strMensaje += "- Forma de pago incorrecta.";
                //    }
                //    if (Convert.ToInt64(values[18]) > 0 && string.IsNullOrEmpty(values[19].Trim()))
                //    {
                //        strMensaje += "- Destino invalido.";
                //    }

                //    if (Convert.ToDecimal(values[15]) == 0 && values[17].Trim().Length == 0)//17 Autoprizacion de salida para venta interno
                //    {
                //        strMensaje += "- No existe documento de respaldo.";
                //    }
                //    #endregion
                //}
                //else
                //{
                //    #region Recepciones
                //    if (values.Length != 18)
                //    {
                //        strMensaje = "- Nro. de columnas del registro invalido.";
                //    }
                //    var arrayNumero = new[] { 3, 5, 7, 10, 11, 13, 14, 17 };
                //    decimal dec;
                //    for (int i = 0; i < arrayNumero.Length; i++)
                //    {
                //        if (!decimal.TryParse(values[arrayNumero[i]].Trim(), out dec))
                //        {
                //            strMensaje += "- Columna Nro. " + arrayNumero[i] + " No es un número.";
                //        }
                //    }
                //    if (values[0].Trim().Substring(6, 4) + values[0].Trim().Substring(3, 2) != cmbPeriodo.Value.ToString().Substring(0, 4) + cmbPeriodo.Value.ToString().Substring(4, 2))
                //    {
                //        strMensaje += "- Periodo incorrecto.";
                //    }
                //    DateTime dt;
                //    if (!DateTime.TryParse(values[0].Trim(), out dt))
                //    {
                //        strMensaje += "- Fecha incorrecta.";
                //    }
                //    if (MCodificaParametrica((int)EnumTipoParametrica.Producto, values[4].Trim()) == -99999)
                //    {
                //        strMensaje += "- Producto incorrecto.";
                //    }
                //    if (MCodificaParametrica((int)EnumTipoParametrica.UnidadMedida, values[4].Trim()) == -99999)
                //    {
                //        strMensaje += "- Unidad de medida invalida.";
                //    }
                //    if (Convert.ToDecimal(values[5]) <= 0)
                //    {
                //        strMensaje += "- Cantidad incorrecto.";
                //    }
                //    //La factura puede contener otros items
                //    //if (Convert.ToDecimal(values[20].Trim()) > (Convert.ToDecimal(values[19].Trim()) * Convert.ToDecimal(values[10].Trim())) + 2 ||
                //    //    Convert.ToDecimal(values[20].Trim()) < (Convert.ToDecimal(values[19].Trim()) * Convert.ToDecimal(values[10].Trim())) - 2)
                //    //{
                //    //    strMensaje += "- Importe incorrecto.";
                //    //}
                //    if (values[15].Trim().Length > 1)
                //    {
                //        if (values[15].Trim().ContainsAny("-", "@", "/", "'", "\\", ".", ",", "_", "+", "[", "]", "{", "}", " "))
                //        {
                //            strMensaje += "- Identificador contiene caracteres invalidos.";
                //        }
                //    }
                //    if (values[1].Trim() != "NOID")
                //    {
                //        if (MCodificaParametrica((int)EnumTipoParametrica.Entidad, values[1].Trim()) == -99999)
                //        {
                //            strMensaje += "- Entidad origen invalida.";
                //        }
                //    }

                //    if (Convert.ToDecimal(values[10]) == 0 && values[15].Trim().Length == 0 && values[19].Trim().Length == 0)
                //    {
                //        strMensaje += "- No existe documento de respaldo.";
                //    }
                //    #endregion
                //}

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

        protected void btnDescargar_Click(object sender, EventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    try
                    {
                        var response = HttpContext.Current.Response;
                        response.ClearContent();
                        response.Clear();
                        response.ContentType = "application/excel";
                        response.AddHeader("Content-Disposition", "attachment; filename=plantilla_importacion_transporte.xlsx;");
                        response.TransmitFile(Server.MapPath("~/Sitio/Plantillas/plantilla_importacion_transporte.xlsx"));
                        response.Flush();
                        response.End();
                    }
                    catch (Exception ex)
                    {

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

        protected void pnlTransaccionImportacion_Callback(object sender, CallbackEventArgsBase e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    Session.Remove(CVariablesSesion.StrAuxiliar);
                    cmbTipoOperacionImp.DataSource = null;
                    cmbTipoOperacionImp.DataBind();
                    cmbTipoOperacionImp.Text = "";
                    Session[CVariablesSesion.AuxActualiza] = "1";

                    Session.Remove(CVariablesSesion.GrillaTransaccion);
                    dgvTransaccionesImp.DataSource = null;
                    dgvTransaccionesImp.DataBind();

                    if (tlsOperadores.FocusedNode != null)
                    {
                        if ((int)tlsOperadores.FocusedNode.GetValue("IntAccion") > 0)
                        {
                            var lstTipoOperacion = Session[CVariablesSesion.ComboTiposOperacion] as List<O_TIPOS_OPE_ACT_CTY>;
                            if (lstTipoOperacion != null && lstTipoOperacion.Any())
                            {
                                cmbTipoOperacionImp.DataSource = lstTipoOperacion.OrderBy(x => x.NOMBRE);
                                cmbTipoOperacionImp.DataBind();
                                Session[CVariablesSesion.ComboTipoOperacion] = lstTipoOperacion;
                                Session[CVariablesSesion.StrAuxiliar] = e.Parameter;

                                var param = e.Parameter.Split('|');

                                var lstTipoOp = Session[CVariablesSesion.ComboTipoOperacion] as List<O_TIPOS_OPE_ACT_CTY>;

                                if (param[2] == "2")
                                {
                                    lstTipoOp = lstTipoOp.Where(x => x.NOMBRE == param[1]).ToList();
                                    Session.Remove(CVariablesSesion.Aux);
                                    if (lstTipoOp.Any())
                                    {
                                        Session[CVariablesSesion.Aux] = lstTipoOp.FirstOrDefault().CODIGO;
                                        cmbTipoOperacionImp.SelectedItem = cmbTipoOperacionImp.Items.FindByValue(lstTipoOp.FirstOrDefault().ID_TIPO_OPERACION.ToString());

                                        var strFecha = param[0].Substring(6, 4) + param[0].Substring(3, 2) + param[0].Substring(0, 2);
                                        var lstMov = Session[CVariablesSesion.GrillaAcumulado] as List<CMovAcumulado>;
                                        var objMov = lstMov.Where(x => x.StrIdGrupoUnico == cmbFiltro.Value.ToString()).ToList();
                                        if (!objMov.Any())
                                            return;
                                        var strIdProd = objMov.FirstOrDefault().IntIdProducto;
                                        var strIdTiIn = objMov.FirstOrDefault().DecIdTipoInfraestructura;
                                        var strIdInfr = objMov.FirstOrDefault().DecIdInfraestructura;

                                        Session[CVariablesSesion.ParamAux] = strFecha.Trim() + "|" + strIdProd + "|" + strIdTiIn + "|" + strIdInfr;

                                        MConfiguraGrillaImp(lstTipoOp.FirstOrDefault().BINARIO_ESTADO.Substring(1, 1), Session[CVariablesSesion.Aux].ToString());
                                        dgvTransaccionesImp_CustomCallback(null, new ASPxGridViewCustomCallbackEventArgs(e.Parameter));
                                    }
                                    else
                                    {
                                        //dgvTransacciones.Columns[(int)EnumColumnasGrilla.FoAbm].Visible = false;
                                    }
                                }
                                else
                                {
                                    decimal dec;
                                    if (param[1] == "DecSaldoInicial" || param[1] == "MDIN" || decimal.TryParse(param[1].Substring(6, 2), out dec))
                                    {


                                        if (param[1] == "MDIN")
                                        {
                                            lstTipoOp = lstTipoOp.Where(x => x.CODIGO.Contains("MDIN")).ToList();
                                        }
                                        else if (param[1] == "DecSaldoInicial")
                                        {
                                            lstTipoOp = lstTipoOp.Where(x => x.CODIGO == "TOSICB").ToList();
                                        }
                                        else
                                        {
                                            var intTipo = (param[1].Contains("Ing")) ? 1 : -1;
                                            int intOrden = Convert.ToInt32(param[1].Substring(6, 2));
                                            lstTipoOp = lstTipoOp.Where(x => x.FACTOR_CALCULO == intTipo && x.ORDEN == intOrden).ToList();
                                        }

                                        Session.Remove(CVariablesSesion.Aux);
                                        if (lstTipoOp.Any())
                                        {
                                            Session[CVariablesSesion.Aux] = lstTipoOp.FirstOrDefault().CODIGO;
                                            cmbTipoOperacionImp.SelectedItem = cmbTipoOperacionImp.Items.FindByValue(lstTipoOp.FirstOrDefault().ID_TIPO_OPERACION.ToString());

                                            int index = Convert.ToInt32(param[0].Trim());
                                            if (index >= 0)
                                            {
                                                var strFecha = dgvAcumulado.GetRowValues(index, "StrFecha").ToString();
                                                strFecha = strFecha.Substring(6, 4) + strFecha.Substring(3, 2) + strFecha.Substring(0, 2);
                                                var strIdProd = dgvAcumulado.GetRowValues(index, "IntIdProducto").ToString();
                                                var strIdTiIn = dgvAcumulado.GetRowValues(index, "DecIdTipoInfraestructura").ToString();
                                                var strIdInfr = dgvAcumulado.GetRowValues(index, "DecIdInfraestructura").ToString();

                                                Session[CVariablesSesion.ParamAux] = strFecha.Trim() + "|" + strIdProd + "|" + strIdTiIn + "|" + strIdInfr;
                                                /**/
                                                MConfiguraGrillaImp(lstTipoOp.FirstOrDefault().BINARIO_ESTADO.Substring(1, 1), Session[CVariablesSesion.Aux].ToString());
                                                dgvTransaccionesImp_CustomCallback(null, new ASPxGridViewCustomCallbackEventArgs(e.Parameter));
                                            }
                                        }
                                        else
                                        {
                                            //dgvTransacciones.Columns[(int)EnumColumnasGrilla.FoAbm].Visible = false;
                                        }
                                    }
                                }

                            }
                            else
                            {
                                MImprimirMensaje("Tipos de operación indefinidos.", EnumTiposMensajeAlerta.Error);
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
            }
            else
            {
                //todo No implementado.
            }
        }

        protected void pucConfig_WindowCallback(object source, PopupWindowCallbackArgs e)
        {

            if (Page.IsCallback)
            {
                CargarConfig();
            }



            try
            {
                #region Proceso a Ejecutar

                dgvHidrocarburo.DataSource = null;
                dgvHidrocarburo.DataBind();

                if (tlsOperadores.FocusedNode != null)
                {                   
                    //HIDROCARBUROS
                    var objHidrocarburo = _client.Post<EResultadoConfigHidrocarburo>(CAppSettings.svc_listar_autorizacion_condicion,
                        new
                        {
                            strCredencial = CParametrosHydro.strCredencialFuncionario,
                            decFiltro = (tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString() == "0") ? 2 : 1,
                            strParametro1 = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString()),
                            strParametro2 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
                            strParametro3 = ""
                        });
                    if (objHidrocarburo.IntCodigo == 1)
                    {
                        dgvHidrocarburo.DataSource = objHidrocarburo.OResultado;
                        dgvHidrocarburo.DataBind();

                        Session[CVariablesSesion.GrillaAcumRep] = objHidrocarburo.OResultado;
                    }
                    
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

        protected void pucDJRecepciones_WindowCallback(object source, PopupWindowCallbackArgs e)
        {            
            try
            {
                #region Valida fechas saldo fisico     
                         
                var lstParam = Session[CVariablesSesion.OctanoParametrica] as List<O_LISTA_OCTANO_PAR_CTY>;
                var lstPar = lstParam.Where(x => x.DESCRIPCION.Contains("SALDO FISICO")).ToList();
                if (!lstPar.Any())
                {
                    MImprimirMensaje("Parametro TIPO DE MANEJO DE SALDOS, No definido.", EnumTiposMensajeAlerta.Error);
                    return;
                }            
                //var datFechaReg = DateTime.Parse(detFechaSaldoFisico.Text);
                var datFechaReporte = DateTime.Now;
                var decTipoOperacion = Session[CVariablesSesion.ComboTiposOperacion] as List<O_TIPOS_OPE_ACT_CTY>;
                decimal decFechaIni = 0, decFechaFin = 0;
                decFechaIni = CFechas.ConvierteDateTimeLong(datFechaReporte);
                decFechaFin = Convert.ToDecimal(datFechaReporte.Year + datFechaReporte.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReporte.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + lstPar[0].VALOR_FIN);
                                
                // Obtener la hora actual del sistema
                TimeSpan currentTime = DateTime.Now.TimeOfDay;
                // Asignar la hora actual al control ASPxTimeEdit
                tedHoraSaldo.DateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).Add(currentTime);

                if (decFechaIni <= decFechaFin)
                {
                    Panel3.Visible = true;
                    Panel5.Visible = false;
                    //detFechaSalFisico.Date = DateTime.Now.AddDays(-1);                    
                    detFechaSalFisico.Date = DateTime.Now;                  
                }
                else
                {
                    detFechaSalFisico.Date = DateTime.Now;
                    Panel3.Visible = false;
                    Panel5.Visible = true;
                    return;
                }

                var objAcumulado = (List<CMovAcumulado>)Session[CVariablesSesion.GrillaAcumulado];
                var lstDatos = objAcumulado.Where(x => x.StrIdGrupoUnico == (string)cmbFiltro.Value).OrderBy(x => x.StrFecha).ToList();
                #endregion              

                #region Proceso a Ejecutar

                dgvProductoRecep.DataSource = null;
                dgvProductoRecep.DataBind();

                if (tlsOperadores.FocusedNode != null)
                {                 
                    //HIDROCARBUROS                    
                    var objHidrocarburo = _client.Post<EResultadoConfigHidrocarburo>(CAppSettings.svc_listar_autorizacion_condicion,
                        new
                        {
                            strCredencial = CParametrosHydro.strCredencialFuncionario,
                            decFiltro = (tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString() == "0") ? 2 : 1,
                            strParametro1 = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString()),
                            strParametro2 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
                            strParametro3 = ""
                        });
                    if (objHidrocarburo.IntCodigo == 1)
                    {
                        var lsProductoAux = objHidrocarburo.OResultado
                                .Select(x =>
                                {
                                    x.OBSERVACIONES = "";
                                    return x;
                                });
                        dgvProductoRecep.DataSource = lsProductoAux;
                        dgvProductoRecep.DataBind();
                        Session[CVariablesSesion.HabBotonesSaldoFisico] = "MODSI";
                    }
                    else
                    {
                        dgvProductoRecep.DataSource = null;
                        dgvProductoRecep.DataBind();                    
                    }
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
        private void CargarConfig()
        {
            var objHidrocarburo = _client.Post<EResultadoProducto>(CAppSettings.svc_lista_producto,
           new
           {
               strCredencial = CParametrosHydro.strCredencialFuncionario,
               strCodigo = ""
           });



            if (objHidrocarburo.IntCodigo == 1)
            {
                cmbHidrocarburo.DataSource = objHidrocarburo.OResultado;
                cmbHidrocarburo.DataBind();
            }
            else
            {
                cmbHidrocarburo.DataSource = null;
                cmbHidrocarburo.DataBind();
            }
            var lstObtCatalogo = _client.Post<EResultadoParametrica>(CAppSettings.svc_obtener_catalogo,
               new
               {
                   strLlave = ""
               });

            if (lstObtCatalogo.IntCodigo == 1)
            {
                var objParametricas = lstObtCatalogo.OResultado.CATALOGO_UNIDADES_MEDIDA_GRAL;
                cmbUnidadMedida.DataSource = objParametricas;
                cmbUnidadMedida.DataBind();
            }                         
        }

        protected void dgvHidrocarburo_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
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

                //  MImprimirMensaje(exp.Message, EnumTiposMensajeAlerta.Error, false);
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

        protected void dgvHidrocarburo_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    int index = dgvHidrocarburo.FocusedRowIndex;
                    if (index >= 0)
                    {
                        if (e.ButtonID == "btnHidQuitar")
                        {
                            var lstHidrocarburo = new List<O_LISTA_AUTORIZACION_COND_CTY>();
                            lstHidrocarburo = (List<O_LISTA_AUTORIZACION_COND_CTY>)Session[CVariablesSesion.GrillaAcumRep];

                            lstHidrocarburo.RemoveAll(x => x.ID_IDENTIFICADOR == Convert.ToDecimal(dgvHidrocarburo.GetRowValues(index, "ID_IDENTIFICADOR")));

                            Session[CVariablesSesion.GrillaAcumRep] = lstHidrocarburo;
                            dgvHidrocarburo.DataSource = lstHidrocarburo;
                            dgvHidrocarburo.DataBind();
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

                    //  MImprimirMensaje(exp.Message, EnumTiposMensajeAlerta.Error, false);
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

        protected void dgvHidrocarburo_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar
                    var lstHidrocarburo = new List<O_LISTA_AUTORIZACION_COND_CTY>();
                    lstHidrocarburo = (List<O_LISTA_AUTORIZACION_COND_CTY>)Session[CVariablesSesion.GrillaAcumRep];
                    if (!((cmbHidrocarburo.Value == null || cmbHidrocarburo.Text == "") && (cmbUnidadMedida.Value == null || cmbUnidadMedida.Text == "")))
                    {
                        if (lstHidrocarburo != null && lstHidrocarburo.Any())
                        {
                            lstHidrocarburo.RemoveAll(x => x.ID_IDENTIFICADOR == Convert.ToDecimal(cmbHidrocarburo.Value));
                        }
                        else
                        {
                            lstHidrocarburo = new List<O_LISTA_AUTORIZACION_COND_CTY>();
                        }

                        lstHidrocarburo.Add(new O_LISTA_AUTORIZACION_COND_CTY()
                        {
                            ID_AUTORIZACION_CONDICION = 0,
                            ID_IDENTIFICADOR = Convert.ToDecimal(cmbHidrocarburo.Value),
                            NOMBRE = cmbHidrocarburo.Text,
                            ID_SUB_IDENTIFICADOR = Convert.ToDecimal(cmbUnidadMedida.Value),
                            SUBID_NOMBRE = cmbUnidadMedida.Text,
                            OBSERVACIONES = "",
                            ESTADO = 1
                        });

                        Session[CVariablesSesion.GrillaAcumRep] = lstHidrocarburo;
                        dgvHidrocarburo.DataSource = lstHidrocarburo;
                        dgvHidrocarburo.DataBind();
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

                    // MImprimirMensaje(exp.Message, EnumTiposMensajeAlerta.Error, false);
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
        private List<O_LISTA_AUTORIZACION_COND_CTY> MRecuperaHidrocarburo()
        {
            var resultado = new List<O_LISTA_AUTORIZACION_COND_CTY>();

            for (int i = 0; i < dgvHidrocarburo.VisibleRowCount; i++)
            {
                var registro = new O_LISTA_AUTORIZACION_COND_CTY();

                registro.ID_AUTORIZACION_CONDICION = Convert.ToDecimal(dgvHidrocarburo.GetRowValues(i, "ID_AUTORIZACION_CONDICION").ToString());
                registro.ID_IDENTIFICADOR = Convert.ToDecimal(dgvHidrocarburo.GetRowValues(i, "ID_IDENTIFICADOR").ToString());
                registro.ID_SUB_IDENTIFICADOR = Convert.ToDecimal(dgvHidrocarburo.GetRowValues(i, "ID_SUB_IDENTIFICADOR").ToString());
                registro.ESTADO = Convert.ToDecimal(dgvHidrocarburo.GetRowValues(i, "ESTADO").ToString());

                GridViewDataTextColumn column = new GridViewDataTextColumn();
                column = (GridViewDataTextColumn)dgvHidrocarburo.Columns["OBSERVACIONES"];
                ASPxTextBox txtObservacion = (dgvHidrocarburo.FindRowCellTemplateControl(i, column, "txtObservacion")) as ASPxTextBox;
                registro.OBSERVACIONES = txtObservacion.Text.Trim();

                resultado.Add(registro);
            }

            return resultado;
        }

        protected void btnGuardaHu_Click(object sender, EventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    List<O_LISTA_AUTORIZACION_COND_CTY> lstFinalHi = MRecuperaHidrocarburo();
                    EResultado objResultado;

                    if (!lstFinalHi.Any())
                    {
                        // MImprimirMensaje("Los hidrocarburos no estan definidos.", EnumTiposMensajeAlerta.Error, true);
                    }
                    else
                    {

                        var datFechaReg = DateTime.Now;
                        decimal decFecha = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString().PadLeft(2, '0') + datFechaReg.Day.ToString().PadLeft(2, '0') + datFechaReg.Hour.ToString().PadLeft(2, '0') + datFechaReg.Minute.ToString().PadLeft(2, '0') + datFechaReg.Second.ToString().PadLeft(2, '0'));

                        objResultado = _client.Post<EResultado>(CAppSettings.svc_elimina_autorizacion_condicion,
                        new
                        {
                            strCredencial = CParametrosHydro.strCredencialFuncionario,
                            decFiltro = 2,
                            decIdTipoActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()),
                            decIdConsumidor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString()),
                            decIdIdentificador = 0,
                            decAppFechaRegistro = decFecha,
                            decAppIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario])
                        });
                        if (objResultado.intCodigo == 1)
                        {
                            foreach (var reg in lstFinalHi)
                            {
                                MGrabaCondicion(Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()), Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString()), 1, reg);
                            }
                            /********************/
                            lblRazonSocial.Text = tlsOperadores.FocusedNode.GetValue("StrDescPadre").ToString();
                            lblNit.Text = tlsOperadores.FocusedNode.GetValue("LngNit").ToString();
                            lblRepLegal.Text = (tlsOperadores.FocusedNode.GetValue("StrDireccion") == null) ? "" : tlsOperadores.FocusedNode.GetValue("StrDireccion").ToString();
                            lblDepto.Text = tlsOperadores.FocusedNode.GetValue("StrDepartamento").ToString();

                            var strDato1 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString();
                            var strDato2 = tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString();
                            var strDato3 = detFechaIni.Date.Year + detFechaIni.Date.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + detFechaIni.Date.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0');
                            var strDato4 = detFechaFin.Date.Year + detFechaFin.Date.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + detFechaFin.Date.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0');

                            MCargaParametricasEsp();
                            MCargaGrilla(strDato1, strDato2, strDato3, strDato4, sender);
                            /**************************/
                        }
                        else
                        {
                            //   MImprimirMensaje(objResultado.strMensaje, EnumTiposMensajeAlerta.Error, true);
                        }

                        /* else
                         {
                             for (int i = 0; i < dgvEntidades.VisibleRowCount; i++)
                             {
                                 decimal decIdConsumidor = Convert.ToDecimal(dgvEntidades.GetRowValues(i, "ID_CONSUMIDOR").ToString());
                                 var datFechaReg = DateTime.Now;
                                 decimal decFecha = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString().PadLeft(2, '0') + datFechaReg.Day.ToString().PadLeft(2, '0') + datFechaReg.Hour.ToString().PadLeft(2, '0') + datFechaReg.Minute.ToString().PadLeft(2, '0') + datFechaReg.Second.ToString().PadLeft(2, '0'));

                                 objResultado = _client.Post<EResultado>(CAppSettings.svc_elimina_autorizacion_condicion,
                                 new
                                 {
                                     strCredencial = CParametrosHydro.strCredencialFuncionario,
                                     decFiltro = 2,
                                     decIdTipoActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()),
                                     decIdConsumidor,
                                     decIdIdentificador = 0,
                                     decAppFechaRegistro = decFecha,
                                     decAppIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario])
                                 });
                                 if (objResultado.intCodigo == 1)
                                 {
                                     foreach (var reg in lstFinalHi)
                                     {
                                         MGrabaCondicion(Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()), decIdConsumidor, 1, reg);
                                     }
                                 }
                                 else
                                 {
                                    // MImprimirMensaje(objResultado.strMensaje, EnumTiposMensajeAlerta.Error, true);
                                 }
                             }
                         }*/
                    }
                    pucConfig_WindowCallback(null, null);

                    #endregion
                }
                catch (Exception exp)
                {
                    #region RegistraError

                    //  MImprimirMensaje(exp.Message, EnumTiposMensajeAlerta.Error, true);
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

        private void MGrabaCondicion(decimal decIdTipoActividad, decimal decIdConsumidor, decimal tipo, O_LISTA_AUTORIZACION_COND_CTY objRegistro)
        {
            var datFechaReg = DateTime.Now;
            decimal decFecha = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString().PadLeft(2, '0') + datFechaReg.Day.ToString().PadLeft(2, '0') + datFechaReg.Hour.ToString().PadLeft(2, '0') + datFechaReg.Minute.ToString().PadLeft(2, '0') + datFechaReg.Second.ToString().PadLeft(2, '0'));

            var objResultado = _client.Post<EResultado>(CAppSettings.svc_registra_autorizacion_condicion,
            new
            {
                strCredencial = CParametrosHydro.strCredencialFuncionario,
                decIdTipoActividad,
                decIdConsumidor,
                decIdIdentificador = objRegistro.ID_IDENTIFICADOR,
                decIdSubIdentificador = objRegistro.ID_SUB_IDENTIFICADOR,
                decEstado = 0,
                strObservaciones = objRegistro.OBSERVACIONES,
                decTipoCondicion = tipo,
                strCodigo = "",
                strDescripcion = objRegistro.DESCRIPCION,
                decAppFechaRegistro = decFecha,
                decAppIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario])
            });
            if (objResultado.intCodigo != 1)
            {
                // MImprimirMensaje(objResultado.strMensaje, EnumTiposMensajeAlerta.Error, true);
            }
        }

        protected void pnlCargarExcel_WindowCallback(object source, PopupWindowCallbackArgs e)
        {

        }

        protected void btnGuarfarDJ_Click(object sender, EventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar
                    //Valida fecha y hora al registrar                   
                    decimal decFechaIniHoraVal = 0, decFechaFinHoraVal = 0;
                    var lstParam = Session[CVariablesSesion.OctanoParametrica] as List<O_LISTA_OCTANO_PAR_CTY>;
                    var lstPar = lstParam.Where(x => x.DESCRIPCION.Contains("SALDO FISICO")).ToList();
                    if (!lstPar.Any())
                    {
                        MImprimirMensaje("Parametro TIPO DE MANEJO DE SALDOS, No definido.", EnumTiposMensajeAlerta.Error);
                        return;
                    }
                    // Obtener el valor del control ASPxTimeEdit
                    DateTime selectedTime = tedHoraSaldo.DateTime;
                    decFechaIniHoraVal = Convert.ToDecimal(DateTime.Parse(detFechaSalFisico.Text).Year + DateTime.Parse(detFechaSalFisico.Text).Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + DateTime.Parse(detFechaSalFisico.Text).Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + selectedTime.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + selectedTime.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + "00");  
                    decFechaFinHoraVal = Convert.ToDecimal(DateTime.Parse(detFechaSalFisico.Text).Year + DateTime.Parse(detFechaSalFisico.Text).Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + DateTime.Parse(detFechaSalFisico.Text).Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + lstPar[0].VALOR_FIN);
                    if (decFechaIniHoraVal <= decFechaFinHoraVal)
                    {
                        List<O_LISTA_PRODUCTO_AUX> lstFinalHi = MRecuperaRecepcionDJ();
                        EResultado objResultado;
                        var strTipoHab = (Session[CVariablesSesion.HabBotonesSaldoFisico] == null) ? "ND" : Session[CVariablesSesion.HabBotonesSaldoFisico].ToString();
                        if (strTipoHab != "MODSI")
                        {
                            return;
                        }

                        if (!lstFinalHi.Any())
                        {
                            MImprimirMensaje("No Cuenta con los productos de operación.", EnumTiposMensajeAlerta.Error);
                        }
                        else
                        {
                            //Comentado en fecha 13/06/2024//
                            //var lstParam = Session[CVariablesSesion.OctanoParametrica] as List<O_LISTA_OCTANO_PAR_CTY>;
                            //var lstPar = lstParam.Where(x => x.DESCRIPCION.Contains("SALDO FISICO")).ToList();
                            //if (!lstPar.Any())
                            //{
                            //    MImprimirMensaje("Parametro TIPO DE MANEJO DE SALDOS, No definido.", EnumTiposMensajeAlerta.Error);
                            //    return;
                            //}

                            var strMensaje = string.Empty;
                            var datFechaReg = DateTime.Parse(detFechaSalFisico.Text);
                            //var datFechaReporte = DateTime.Now;
                            decimal decFiltro = 1, decEstado = 2;
                            var decTipoOperacion = Session[CVariablesSesion.ComboTiposOperacion] as List<O_TIPOS_OPE_ACT_CTY>;
                            decimal decFechaIni = 0, decFechaFin = 0;

                            decFechaIni = Convert.ToInt64(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + "000000");
                            decFechaFin = Convert.ToInt64(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + "235959");

                            //var anulaRegistroExistente = _consulta.ValidaDJSaldoFisico(CParametrosHydro.strCredencialFuncionario, decFiltro, decEstado, decFechaIni, decFechaFin, Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString()), Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()), decTipoOperacion.Find(l => l.NOMBRE == "SALDO FISICO").ID_TIPO_OPERACION, "", CFechas.ConvierteDateTimeLong(DateTime.Now), Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]));
                            var objValSalfisico = new O_VAL_DJ_CTY();
                            var oValSalFisico = _consulta.ValidaDJSaldoFisico(
                            new EObjetoEnvio4ParametrosProto
                            {
                                strCredencial = CParametrosHydro.strCredencialFuncionario,
                                decFiltro = 1,
                                strParametro1 = tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString(),
                                strParametro2 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
                                strParametro3 = decFechaIni.ToString(),
                                strParametro4 = decFechaFin.ToString()
                            });
                            if (oValSalFisico.intCodigo < 0)
                            {
                                MImprimirMensaje("Parametro VALIDA DJ SALDO FISICO, No definido.", EnumTiposMensajeAlerta.Error);
                                return;
                            }
                            objValSalfisico = (O_VAL_DJ_CTY)oValSalFisico.oResultado;
                            //if (anulaRegistroExistente.intCodigo > 0)                        
                            if (objValSalfisico.VALIDA == 0)
                            {
                                if (!RegistarMovSaldoFisico(lstFinalHi, ref strMensaje))
                                {
                                    MImprimirMensaje(strMensaje, EnumTiposMensajeAlerta.Error);
                                }
                                else
                                {
                                    var request = new EListarDeclaracionJuradaAcumuladaD();
                                    request.decFiltro = 3;
                                    request.decIdConsumidor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString());
                                    request.decIdTipoActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                    request.decFechaDiaInicio = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                    request.decFechaDiaFin = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));

                                    var listaTransaccionTipo = _consulta.ListaTransaccionTipo(request);
                                    var LstaAdicional2 = (List<ObjDjTransaccion>)listaTransaccionTipo.oResultado;
                                    var oResp = new O_DECLARACION_JURADA_CTY();
                                    oResp.LstaAdicional2 = (List<ObjDjTransaccion>)LstaAdicional2;

                                    var objNuevaDeclaracion = new EGuardarDeclaracionJurada();
                                    objNuevaDeclaracion.decFechaRegistro = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                    objNuevaDeclaracion.decIdCitePadre = 0;
                                    objNuevaDeclaracion.decIdOpcion = 8;
                                    objNuevaDeclaracion.decIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                    objNuevaDeclaracion.strCodigoFormularioFac = tlsOperadores.FocusedNode.GetValue("StrNroHydro").ToString().Trim();
                                    objNuevaDeclaracion.strCodigoFormularioMov = "UD/AB-RDJ-P01-F01";
                                    objNuevaDeclaracion.decIdMaxMovimiento = 0;
                                    objNuevaDeclaracion.strCredencial = CParametrosHydro.strCredencialFuncionario;
                                    objNuevaDeclaracion.strCredencialFuncionario = CParametrosHydro.strCredencialDeclaracion;
                                    objNuevaDeclaracion.strDpto = "";//MObtenerDeptoAbr(tlsOperadores.FocusedNode.GetValue("StrDepartamento").ToString());
                                    objNuevaDeclaracion.strReferencia = "DECLARACION JURADA";
                                    objNuevaDeclaracion.strSigla = "";//MObtenerSigla((int)tlsOperadores.FocusedNode.GetValue("IntIdActividad"));
                                    objNuevaDeclaracion.cabecera = new O_LISTA_REPORTE_DATOSP_CTY();
                                    var objCabecera = new O_LISTA_REPORTE_DATOSP_CTY();
                                    objCabecera.ID_REPORTE_DATO_PRINC = 0;
                                    objCabecera.FECHA_INICIO = decFechaIni;
                                    objCabecera.FECHA_FIN = decFechaFin;
                                    objCabecera.FECHA_REPORTE = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + selectedTime.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + selectedTime.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + selectedTime.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0')); //CFechas.ConvierteDateTimeLong(DateTime.Now);
                                    objCabecera.ESTADO = 1;
                                    objCabecera.RAZON_SOCIAL = tlsOperadores.FocusedNode.GetValue("StrDescripcion").ToString();
                                    var lstRep = tlsOperadores.FocusedNode.GetValue("StrRepLegal").ToString().Split(',');
                                    //string[] splitResult = lstRep[0].Split('-');
                                    //if (splitResult.Length >= 2)
                                    //{
                                    //    //var strRep = lstRep[0].Split('-');
                                    //    objCabecera.NOMBRES_REPLEGAL = splitResult[0];
                                    //    objCabecera.CI_REPLEGAL = splitResult[1];
                                    //}
                                    //else
                                    //{
                                    //    var strRep = tlsOperadores.FocusedNode.GetValue("StrPropietario").ToString().Trim() + "-" + tlsOperadores.FocusedNode.GetValue("StrCiPropietario").ToString().Trim();
                                    //    objCabecera.NOMBRES_REPLEGAL = strRep[0].ToString();
                                    //    objCabecera.CI_REPLEGAL = strRep[1].ToString();
                                    //}
                                    objCabecera.NOMBRES_REPLEGAL = "";
                                    objCabecera.CI_REPLEGAL = "";

                                    _datSesion = new CPersonaSesion(Server.MapPath("~"), Request.ServerVariables["REMOTE_ADDR"]);
                                    O_DETALLE_USUARIO_CTY oDetUsuario = _datSesion.ObtenerUsuario(Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]));
                                    objCabecera.NOMBRES_USUARIO = oDetUsuario.NOMBRE + " " + oDetUsuario.PRIMER_APELLIDO + " " + oDetUsuario.SEGUNDO_APELLIDO; ;
                                    objCabecera.CI_USUARIO = oDetUsuario.NUMERO_IDENTIFICACION + " " + oDetUsuario.EXPEDICION_ABREVIACION; ;
                                    objCabecera.TIPO_SOCIEDAD = (tlsOperadores.FocusedNode.GetValue("StrTipoSociedad") == null) ? "" : tlsOperadores.FocusedNode.GetValue("StrTipoSociedad").ToString();
                                    objCabecera.OBSERVACIONES = "";
                                    objCabecera.ID_CONSUMIDOR = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString());
                                    objCabecera.ID_CITE = 0;
                                    objCabecera.ID_REPORTE_PRINCIPAL = 0;
                                    objCabecera.APP_ID_USUARIO = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                    objCabecera.APP_FECHA_REGISTRO = CFechas.ConvierteDateTimeLong(DateTime.Now);
                                    objCabecera.APP_ID_HISTORICO = 0;
                                    objCabecera.DIRECCION = tlsOperadores.FocusedNode.GetValue("StrDireccion").ToString();
                                    objCabecera.AMBITO_OPERACION = tlsOperadores.FocusedNode.GetValue("StrAmbito").ToString();
                                    objCabecera.TELEFONO = tlsOperadores.FocusedNode.GetValue("StrTelefono").ToString();
                                    var formatoFecha = CultureInfo.CurrentCulture.DateTimeFormat;
                                    objCabecera.MES = formatoFecha.GetMonthName(Convert.ToInt32(datFechaReg.Date.Month.ToString().PadLeft(2, '0'))).ToUpper() + " - " + datFechaReg.Date.Year;
                                    objCabecera.LUGAR_REGISTRO = tlsOperadores.FocusedNode.GetValue("StrDepartamento").ToString();
                                    objCabecera.ID_TIPO_ACTIVIDAD = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                    objCabecera.MUNICIPIO = tlsOperadores.FocusedNode.GetValue("StrMunicipio") + "/" + tlsOperadores.FocusedNode.GetValue("StrLocalidad");
                                    objNuevaDeclaracion.cabecera = objCabecera;
                                    objNuevaDeclaracion.listaDeclaracion = (O_DECLARACION_JURADA_CTY)oResp;
                                    //var ojo = JsonSerializer.SerializeToString(objNuevaDeclaracion);

                                    var resultado = _client.Post<EResultadoGenerico>(CAppSettings.svc_guardar_recep_declaracion_jurada, objNuevaDeclaracion);
                                    if (resultado.IntCodigo == 1)
                                    {
                                        //MImprimeDeclaracion(datFechaReg, resultado.StrMensaje.Trim());
                                        //MLimpiaGrilla();      
                                        string mensaje = "Saldo fisico REGISTRADO y DECLARADO, Estacion de Servicio: " + tlsOperadores.FocusedNode.GetValue("StrDescripcion").ToString() + " en fecha " + DateTime.Now;
                                        //MImprimirMensaje("Saldo fisico registrado correctamente de la Estacion de Servicio " + tlsOperadores.FocusedNode.GetValue("StrDescripcion").ToString() + " en fecha " + DateTime.Now, EnumTiposMensajeAlerta.Correcto);                                    
                                        pucDJRecepciones.ShowOnPageLoad = false;
                                        if (mEnviarEmail(lstFinalHi, tlsOperadores.FocusedNode.GetValue("StrDepartamento").ToString(), tlsOperadores.FocusedNode.GetValue("StrDescripcion").ToString(), detFechaSalFisico.Text, oDetUsuario))
                                        {
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "showError", "alert('" + mensaje + "');", true);
                                            string script = "pnlDatos.PerformCallback();";
                                            ScriptManager.RegisterStartupScript(this, GetType(), "PerformCallbackScript2", script, true);
                                        }
                                        else
                                        {
                                            mensaje = "Saldo fisico REGISTRADO y DECLARADO, Estacion de Servicio: " + tlsOperadores.FocusedNode.GetValue("StrDescripcion").ToString() + " en fecha " + DateTime.Now +
                                            "Problema en envio a: {" + oDetUsuario.LOGIN + "}, favor capture esta pantalla como constancia del registro.";
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "showError", "alert('" + mensaje + "');", true);
                                        }
                                    }
                                    else
                                    {
                                        MImprimirMensaje(resultado.StrMensaje, EnumTiposMensajeAlerta.Error);
                                        pucDJRecepciones.ShowOnPageLoad = false;
                                        //MPuImprimirMensaje(resultado.StrMensaje, EnumTiposMensajeAlerta.Error);
                                    }
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError", "alert('" + objValSalfisico.MENSAJE + "');", true);
                            }
                        }

                    }
                    else
                    {
                        string mensaje = "La hora máxima permitida es las " + lstPar[0].VALOR_FIN.Insert(2, ":").Insert(5, ":");                      
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + mensaje + "');", true);
                        pucDJRecepciones.ShowOnPageLoad = false;
                    }                                  

                    #endregion
                }
                catch (Exception exp)
                {
                    #region RegistraError

                    //  MImprimirMensaje(exp.Message, EnumTiposMensajeAlerta.Error, true);
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
        private List<O_LISTA_PRODUCTO_AUX> MRecuperaRecepcionDJ()
        {
            var resultado = new List<O_LISTA_PRODUCTO_AUX>();

            for (int i = 0; i < dgvProductoRecep.VisibleRowCount; i++)
            {
                var registro = new O_LISTA_PRODUCTO_AUX();

                registro.ID_PRODUCTO = Convert.ToDecimal(dgvProductoRecep.GetRowValues(i, "ID_IDENTIFICADOR").ToString());
                
                GridViewDataTextColumn column = new GridViewDataTextColumn();
                column = (GridViewDataTextColumn)dgvProductoRecep.Columns["OBSERVACIONES"];
                ASPxTextBox txtObservacion = (dgvProductoRecep.FindRowCellTemplateControl(i, column, "txtObservacion")) as ASPxTextBox;
                registro.OBSERVACIONES = txtObservacion.Text.Trim();

                column = (GridViewDataTextColumn)dgvProductoRecep.Columns["CODIGO"];
                ASPxTextBox txtCantidad = (dgvProductoRecep.FindRowCellTemplateControl(i, column, "txtCantidad")) as ASPxTextBox;
                registro.CANTIDAD = Convert.ToDecimal(txtCantidad.Text.Trim());

                registro.PRODUCTO = dgvProductoRecep.GetRowValues(i, "NOMBRE").ToString();
                registro.UNIDAD_MEDIDA = dgvProductoRecep.GetRowValues(i, "SUBID_NOMBRE").ToString();

                resultado.Add(registro);
            }
            return resultado;
        }
                
        //private List<ERegistraMovimientoTransaccion> RegistarMovimientoRecepcion(List<O_LISTA_PRODUCTO_AUX> lstProducto)
        private bool RegistarMovSaldoFisico(List<O_LISTA_PRODUCTO_AUX> lstProducto, ref string strMensaje)
        {
            bool booResultado = false; 
              var lstMovimientos = (List<CMovAcumulado>)Session[CVariablesSesion.GrillaAcumulado];
              var lstSalFinDia = lstMovimientos.GroupBy(x => new { x.StrIdGrupoUnico, x.StrProducto, x.IntIdProducto, x.StrFecha, x.DecSaldoFinal })
              .Select(g => new { g.Key.StrIdGrupoUnico, g.Key.StrProducto, g.Key.IntIdProducto, g.Key.StrFecha, g.Key.DecSaldoFinal })
              .Where(l => l.StrFecha == detFechaSalFisico.Text)
              .OrderBy(d => d.StrProducto).ToList();
              decimal decCountSalFisico = 0, decDemMer = 0;
            if (MValidaSesion())
            {               
                    #region Proceso a Ejecutar                                   

                    if ((int)tlsOperadores.FocusedNode.GetValue("IntAccion") > 0)
                    {                 
                        try
                        {                                        
                            foreach (var reg in lstProducto)
                            {
                                var decTipoOperacion = Session[CVariablesSesion.ComboTiposOperacion] as List<O_TIPOS_OPE_ACT_CTY>;                              
                                decimal decCantidad = reg.CANTIDAD - lstSalFinDia.Where(l => l.IntIdProducto == reg.ID_PRODUCTO).FirstOrDefault().DecSaldoFinal;                                
                                if (decCantidad > 0)
                                {
                                    decDemMer += MRegMovSalFicoMermaDemacia(reg.ID_PRODUCTO, decCantidad, reg.OBSERVACIONES, decTipoOperacion.Find(l => l.NOMBRE == "DEMASIA DE COMBUSTIBLE").ID_TIPO_OPERACION, (decimal)EnumTipoTransaccion.NotaMovimiento, ref strMensaje);
                                }
                                else
                                {
                                    decDemMer += MRegMovSalFicoMermaDemacia(reg.ID_PRODUCTO, Math.Abs(decCantidad), reg.OBSERVACIONES, decTipoOperacion.Find(l => l.NOMBRE == "MERMA DE COMBUSTIBLE").ID_TIPO_OPERACION, (decimal)EnumTipoTransaccion.NotaMovimiento, ref strMensaje);
                                }
                                decCountSalFisico += MRegMovSalFicoMermaDemacia(reg.ID_PRODUCTO, reg.CANTIDAD, reg.OBSERVACIONES, decTipoOperacion.Find(l => l.NOMBRE == "SALDO FISICO").ID_TIPO_OPERACION, (decimal)EnumTipoTransaccion.DocMedicion, ref strMensaje);                                             
                            }                           
                            //var ojo2 = JsonSerializer.SerializeToString(lsRegistraMovTrans);                                                                                
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
                            //lsRegistraMovTrans = new List<ERegistraMovimientoTransaccion>();
                            strMensaje = "Se tiene problema al registrar los saldos fisicos comuniquese con el administrador. " + JsonSerializer.SerializeToString(lstProducto); 
                            #endregion
                        }
                      
                    }
                    else
                    {
                        //todo No implementado.
                    }

                    #endregion            
            }
            else
            {
                //todo No implementado.
            }

            if (decCountSalFisico > 0 || decCountSalFisico == lstProducto.Count())
            {
                booResultado = true;
                strMensaje = "Se registro correctamente el saldo fisico."; 
            }            
            //return lsRegistraMovTrans;     
            return booResultado;
        }

        private decimal MRegMovSalFicoMermaDemacia(decimal decIdProducto, decimal decCantidad, string strObervacion, decimal decTipoOperacion, decimal decIdTipoTransaccion, ref string strMensaje)
        {
            decimal count = 0;
            try
            {
                // Obtener el valor del control ASPxTimeEdit
                DateTime selectedTime = tedHoraSaldo.DateTime;
                // Obtener la hora, minutos y segundos
                int hora = selectedTime.Hour;
                int minutos = selectedTime.Minute;
                int segundos = selectedTime.Second;

                var datFechaDoc = DateTime.Parse(detFechaSalFisico.Text);
                var datFechaReg = DateTime.Now;
                var objNuevoReg = new ObjMovimientoTransaccion();
                objNuevoReg.idMovimiento = 0;
                objNuevoReg.idMovimientoPadre = 0;
                objNuevoReg.idTipoActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                
                objNuevoReg.idTipoOperacion = decTipoOperacion;//decTipoOperacion.Find(l => l.NOMBRE == "SALDO FISICO").ID_TIPO_OPERACION;
                objNuevoReg.idConsumidor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString());
                objNuevoReg.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                objNuevoReg.fechaRegistroMovimiento = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                objNuevoReg.fechaOperacion = Convert.ToDecimal(datFechaDoc.Year + datFechaDoc.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaDoc.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + selectedTime.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + selectedTime.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + selectedTime.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                objNuevoReg.idTipoMovimientoFlujo = (decimal)EnumTipoMovimientoFlujo.Fisico;
                objNuevoReg.idTipoMovimiento = -99999;//(decimal)EnumTipoMovimiento.NacNac;
                objNuevoReg.idEstadoMovimiento = (decimal)EnumEstadoMov.Activo;
                objNuevoReg.idFuenteOrigenDato = (decimal)EnumTipoRegistro.OrigenWeb;
                objNuevoReg.idUbicacionOperacion = (decimal)EnumPais.BOLIVIA;
                objNuevoReg.idUnidadMedidaHidrocarburo = 68;
                objNuevoReg.idHidrocarburo = decIdProducto;
                objNuevoReg.cantidadHidrocarburo = decCantidad;
                objNuevoReg.identificador = "";
                objNuevoReg.subIdentificador = "";
                objNuevoReg.observacion = strObervacion;
                objNuevoReg.barcode = 0;
                objNuevoReg.idTipoMedicion = -99999;
                objNuevoReg.cantidadAguaLitros = 0;
                objNuevoReg.idTipoMedioTransporte = -99999;
                objNuevoReg.idMedioTransporte = -99999;
                objNuevoReg.tipoInfraestructura = -99999;
                objNuevoReg.idInfraestructura = -99999;

                objNuevoReg.idUnidadEnvasesTransporte = -99999;
                objNuevoReg.codigo = "";
                objNuevoReg.idIntegracion = 0;
                objNuevoReg.idUnidadTemperatura = -99999;
                objNuevoReg.temperatura = 0;
                objNuevoReg.gradoApi = "";
                objNuevoReg.idEntidad = -99999;
                objNuevoReg.cantidadEnvasesTransporte = 0;
                objNuevoReg.binarioControl = "00000-0000000000-0000000000-00000";
                objNuevoReg.idTransaccionBaseExterna = "";
                objNuevoReg.idTipoConsumidor = -99999;
                objNuevoReg.jsonReferencia = "";
                objNuevoReg.estadoJsonRef = -99999;
                objNuevoReg.idCuenta = -99999;
                objNuevoReg.idCuentaAux = -99999;
                objNuevoReg.idParamGlobal = -99999;
                objNuevoReg.idParamEspec = -99999;
                objNuevoReg.idTipoInfraAux = -99999;
                objNuevoReg.idInfraAux = -99999;

                objNuevoReg.listaTransaccion = new List<ObjTransaccion>();

                List<O_LISTA_TRANSACCION_CTY> lstDocumentosMov;
                var objTrasaccion = new ObjTransaccion();
                objTrasaccion.idMovimiento = 0;
                objTrasaccion.idTransaccion = 0;
                objTrasaccion.idTransaccionPadre = 0;
                objTrasaccion.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                objTrasaccion.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                objTrasaccion.idTipoTransaccion = decIdTipoTransaccion;//(decimal)EnumTipoTransaccion.DocMedicion;
                objTrasaccion.fechaEmision = objNuevoReg.fechaOperacion;
                objTrasaccion.numeroTransaccion = CFechas.ConvierteDateTimeLong(datFechaReg).ToString();
                objTrasaccion.numeroAutorizacion = 0;
                objTrasaccion.codigoControl = "";
                objTrasaccion.nitConsumidor = 0;
                objTrasaccion.razonSocial = "";
                objTrasaccion.precioUnitario = 0;
                objTrasaccion.precioTotal = 0;
                objTrasaccion.usuarioTransaccion = "";
                objTrasaccion.observaciones = "";
                objTrasaccion.nitEmisor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("LngNit").ToString()); 
                objTrasaccion.idEstadoTransaccion = objNuevoReg.idEstadoMovimiento;
                objTrasaccion.idTipoCreditoFiscal = (decimal)EnumTipoCredFiscal.No;
                objTrasaccion.idFormaPago = (decimal)EnumTiposPago.Efectivo;
                objTrasaccion.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                objTrasaccion.idTipoEmisor = -99999;
                objTrasaccion.codigoSeguridad = 0;
                objTrasaccion.idAutorizacionDato = 0;
                objTrasaccion.binarioControl = "0000000000";

                objTrasaccion.fechaOperacion = Convert.ToDecimal(datFechaDoc.Year + datFechaDoc.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaDoc.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaDoc.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaDoc.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaDoc.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                objTrasaccion.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                objTrasaccion.codigo = "";
                objTrasaccion.codigoAutorizacion = "";
                objNuevoReg.listaTransaccion.Add(objTrasaccion);

                var objRegistro = new ERegistraMovimientoTransaccion();
                objRegistro.strCredencial = CParametrosHydro.strCredencialFuncionario;
                objRegistro.decFiltro = 1;
                var lstRegistros = new List<ObjMovimientoTransaccion>();
                lstRegistros.Add(objNuevoReg);
                objRegistro.listaMovimientoTransaccion = lstRegistros;
                //var DjJson = JsonSerializer.SerializeToString(objRegistro);
                var resultado = _client.Post<EResultado>(CAppSettings.svc_registra_movimiento_transaccion, objRegistro);

                if (resultado.intCodigo == 1)
                {
                    count++;

                    if (decTipoOperacion == 33)
                    {
                        //_gestionPg.RegistrarSaldoPg(Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdProyecto").ToString()), decTipoOperacion, decCantidad, decIdProducto);     
                        try
                        {
                            // Crea una nueva Task
                            Task task = new Task(() =>
                            {
                                try
                                {
                                    // Tu código que quieres ejecutar asíncronamente
                                    var objRes = _srvApi.Post<EResulApi>(CAppSettings.svc_registrarSaldo,
                                     new
                                     {
                                         idEntidad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdProyecto").ToString()),
                                         idProducto = decIdProducto,
                                         volumen = decCantidad,
                                         idTipo = 1
                                     });
                                }
                                catch (Exception innerExp)
                                {
                                    #region RegistraError
                                    MImprimirMensaje(innerExp.Message, EnumTiposMensajeAlerta.Error);
                                    var oMensajeUsuario = new CLogTraza.MensajeUsuario
                                    {
                                        decUsuarioId = Session[CVariablesSesion.IdUsuario],
                                        decIdModulo = CParametrosHydro.decIdAplicacion,
                                        strAccion = GetType().Name + "." + MethodBase.GetCurrentMethod().Name,
                                        strIp = HttpContext.Current.Request.UserHostAddress,
                                        decNivelCapa = (int)CLogTraza.CapasNivel.Presentacion
                                    };
                                    CLogTraza.Error(oMensajeUsuario, innerExp);
                                    #endregion
                                }
                            });
                            // Inicia la tarea.  Esto implícitamente la encola al ThreadPool.
                            task.Start();
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
                }
                else
                {
                    strMensaje = resultado.strMensaje;
                }
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
                //lsRegistraMovTrans = new List<ERegistraMovimientoTransaccion>();
                strMensaje = "Se tiene problemas al registrar la declaracion del saldo fisico saldo fisico V2 comuniquese con el administrador.";
                #endregion
            }
            return count;        
        }
        private partial class O_LISTA_PRODUCTO_AUX
        {        
            public decimal ID_PRODUCTO { get; set; }
            public decimal CANTIDAD { get; set; }        
            public string OBSERVACIONES { get; set; }
            public string UNIDAD_MEDIDA { get; set; }
            public string PRODUCTO { get; set; }      
        }
        protected void pucSaldosFisco_WindowCallback(object source, PopupWindowCallbackArgs e)
        {         
            try
            {                      
                var datFechaConsulta = DateTime.Parse(detFechaRegSF.Text);
                int decFiltro = 4;
                string strParametro1=tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString();
                string strParametro2= tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString();
                string strParametro3=datFechaConsulta.Year + datFechaConsulta.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaConsulta.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + "000000"; 
                string strParametro4=datFechaConsulta.Year + datFechaConsulta.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaConsulta.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + "235959";


                Session[CVariablesSesion.HabBotonesSaldoFisico] = "MODSI";              

                #region Valida fechas saldo fisico, adicionado 20/06/2023

                    //var lstParam = Session[CVariablesSesion.OctanoParametrica] as List<O_LISTA_OCTANO_PAR_CTY>;
                    //var lstPar = lstParam.Where(x => x.DESCRIPCION.Contains("SALDO FISICO")).ToList();
                    //if (!lstPar.Any())
                    //{
                    //    MImprimirMensaje("Parametro TIPO DE MANEJO DE SALDOS, No definido.", EnumTiposMensajeAlerta.Error);
                    //    return;
                    //}
                    //Session[CVariablesSesion.HoraConfig] = lstPar[0].VALOR_FIN;                 
                #endregion

                #region Proceso a Ejecutar

                dgvSaldoFisico.DataSource = null;
                dgvSaldoFisico.DataBind();

                if (tlsOperadores.FocusedNode != null)
                {                   
                    var lstDeclaraciones = _client.Post<EResultadoListadoDj>(CAppSettings.svc_listar_reporte_datos_principal,
                    new
                    {
                        strCredencial = CParametrosHydro.strCredencialListadoDj,
                        decFiltro, //TipoMovimiento (1=ventas)
                        strParametro1, //Id proyecto/entidad
                        strParametro2, //id Actividad
                        strParametro3, //fecha inicial
                        strParametro4 //fecha final
                    });
                    if (lstDeclaraciones.IntCodigo == 1)
                    {
                        var LstResultado = new List<O_LISTA_REPORTE_DATOSP_CTY>();
                        LstResultado = lstDeclaraciones.OResultado;
                        if (LstResultado.Any())
                        {
                            LstResultado.ForEach(a => a.StrEstado = ((a.ESTADO == 1) ? "DECLARADO" : ((a.ESTADO == 2) ? "ANULADO" : ((a.ESTADO == 3) ? "RECEPCIONADO" : "ARCH.ADJUNTO"))));
                            LstResultado.ForEach(a => a.StrFecha = a.FECHA_REPORTE.ToString(CultureInfo.InvariantCulture).Substring(6, 2) + "/" + a.FECHA_REPORTE.ToString(CultureInfo.InvariantCulture).Substring(4, 2) + "/" + a.FECHA_REPORTE.ToString(CultureInfo.InvariantCulture).Substring(0, 4) + " " + a.FECHA_REPORTE.ToString(CultureInfo.InvariantCulture).Substring(8, 2) + ":" + a.FECHA_REPORTE.ToString(CultureInfo.InvariantCulture).Substring(10, 2) + ":" + a.FECHA_REPORTE.ToString(CultureInfo.InvariantCulture).Substring(12, 2));
                            LstResultado = LstResultado.OrderBy(x => x.FECHA_REPORTE).ToList();

                            Session[CVariablesSesion.GrillaCabecera] = LstResultado;
                            dgvSaldoFisico.DataSource = LstResultado;
                            dgvSaldoFisico.DataBind();
                        }
                        else
                        {
                            dgvProductoRecep.DataSource = null;
                            dgvProductoRecep.DataBind();
                        }
                    }
                    else
                    {
                        dgvProductoRecep.DataSource = null;
                        dgvProductoRecep.DataBind();
                    }                              
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
        protected void dgvSaldoFisico_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            try
            {
                #region Proceso a Ejecutar

                if (e.VisibleIndex >= 0)
                {
                    switch (dgvSaldoFisico.GetRowValues(e.VisibleIndex, "ESTADO").ToString())
                    {
                        case "2":
                            e.Row.BackColor = Color.FromArgb(215, 135, 135);
                            break;
                        case "3":
                            e.Row.BackColor = Color.FromArgb(143, 179, 157);
                            break;
                        case "4":
                            e.Row.BackColor = Color.FromArgb(171, 209, 184);
                            break;
                        default:
                            e.Row.BackColor = Color.FromArgb(175, 207, 167);
                            break;
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
        protected void dgvSaldoFisico_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    var index = dgvSaldoFisico.FocusedRowIndex;
                    //var lngidCite = Convert.ToInt64(dgvSaldoFisico.GetRowValues(index, "ID_CITE"));
                    var intEstado = Convert.ToInt32(dgvSaldoFisico.GetRowValues(index, "ESTADO"));
                    var decAppIdUs = Convert.ToDecimal(dgvSaldoFisico.GetRowValues(index, "APP_ID_USUARIO"));
                    var decFechaRep = Convert.ToDecimal(dgvSaldoFisico.GetRowValues(index, "FECHA_REPORTE"));

                    //if (lngidCite > 0)
                    //{
                        var datFechaReg = DateTime.Now;
                        EResultado resultado;
                        var strTipoHab = (Session[CVariablesSesion.HabBotonesSaldoFisico] == null) ? "ND" : Session[CVariablesSesion.HabBotonesSaldoFisico].ToString();
                        if (strTipoHab != "MODSI")
                        {
                            return;
                        }
                        switch (e.ButtonID)
                        {
                            case "btnAnularSalF2":
                                if (intEstado != 2)
                                {                                   
                                    decimal decFiltro = 1, decEstado = 2;
                                    var decTipoOperacion = Session[CVariablesSesion.ComboTiposOperacion] as List<O_TIPOS_OPE_ACT_CTY>;
                                    decimal decFechaIni = Convert.ToInt64(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + "000000");
                                    decimal decFechaFin = Convert.ToInt64(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + "235959");
                                    var anulaRegistroExistente = _gestion.ActualizaRecepcionesEstado(CParametrosHydro.strCredencialFuncionario, decFiltro, decEstado, decFechaIni, decFechaFin, Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString()), Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()), decTipoOperacion.Find(l => l.NOMBRE == "SALDO FISICO").ID_TIPO_OPERACION, "", CFechas.ConvierteDateTimeLong(DateTime.Now), Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]));
                                    if (anulaRegistroExistente.intCodigo > 0)
                                    {
                                        MImprimirMensaje("Se Anulo el saldo fisico correctamente", EnumTiposMensajeAlerta.Correcto);
                                     
                                        pucSaldosFisco_WindowCallback(null,null);                                      
                                   
                                        pucSaldosFisco.ShowOnPageLoad = false;

                                        string script = "pnlDatos.PerformCallback();";
                                        ScriptManager.RegisterStartupScript(this, GetType(), "PerformCallbackScript", script, true);
                                        
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError", "alert('" + anulaRegistroExistente.strMensaje + "');", true);
                                    }                                   
                                }
                                else
                                {
                                    string strMensaje = "No puede ejecutar la función debido a que la declaración se encuentra anulada";
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError", "alert('" + strMensaje + "');", true);
                                }
                                break;                           
                        }
                    //}
                    //else
                    //{
                    //    //todo No implementado.
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
        protected void dgvSaldoFisico_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
        {
            if (!MValidaSesion()) return;
            try
            {
                #region Proceso a Ejecutar

                if (e.VisibleIndex == -1) return;

                var datFechaReporte = DateTime.Parse(detFechaRegSF.Text);
                decimal sw = 0;
                var decFechaActual = DateTime.Now;
                if (Convert.ToDecimal(decFechaActual.Year + decFechaActual.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + decFechaActual.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0')) <= Convert.ToDecimal(datFechaReporte.Year + datFechaReporte.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReporte.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0')))
                { sw = 1; }
                else
                { sw = 0; }

                if (e.ButtonID == "btnAnularSalF2")
                {
                    e.Visible = Convert.ToInt16(Session[CVariablesSesion.PermisoBaja]) == 1 && sw == 1 ? DefaultBoolean.True : DefaultBoolean.False;
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

        private static List<EObjetoPrecioImportacion> _listaPrecios = new List<EObjetoPrecioImportacion>();
        public List<EObjetoPrecioImportacion> Lista
        {
            get { return _listaPrecios; }
            set { _listaPrecios = value; }
        }
      
    }
        #endregion
    public class EResulApi
    {
        public decimal decCodigo { get; set; }
        public object oResultado { get; set; }
        public string strMensaje { get; set; }
    } 
}