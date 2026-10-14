using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using AnhHydroOctanoPresentacion.Clases.Controles;
using AnhHydroOctanoPresentacion.Entidades;
using AnhHydroOctanoPresentacion.Enumeradores;
using AnhHydroOctanoPresentacion.Parametros;
using DevExpress.Web;
using Librerias.Anh.Us;
using ServiceStack.ServiceClient.Web;
using UserControl = System.Web.UI.UserControl;
using ServiceStack.Text;

namespace AnhHydroOctanoPresentacion.Controles
{
    public partial class CtrReporteMovimiento : UserControl
    {
        #region Variables de entorno

        JsonServiceClient client = new JsonServiceClient(CAppSettings.servicio_octVolumen);
        JsonServiceClient clientI = new JsonServiceClient(CAppSettings.servicio_octInfraestructura);

        decimal _totalImporte, _totalCantidad;
        long _totalAnulada, _totalValida;

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
                        Session.Remove(CVariablesSesion.GrillaTransaccion);
                        Session.Remove(CVariablesSesion.GrillaAuxiliar);
                        MCargaArbol();
                        MCargaParametricas();
                        ConfigurarFormulario();
                        //Session[CVariablesSesion.AuxValidaLlave] = 0;

                        tlsOperadores.DataSource = Session[CVariablesSesion.EstructuraArbol];
                        tlsOperadores.DataBind();
                        if (Session[CVariablesSesion.EntidadSeleccionada] != null)
                        {
                            var strPosicion = Session[CVariablesSesion.EntidadSeleccionada].ToString();
                            switch (strPosicion.Length)
                            {
                                case 1:
                                    tlsOperadores.Nodes[Convert.ToInt16(strPosicion.Substring(0, 1))].Focus();
                                    break;
                                case 2:
                                    tlsOperadores.Nodes[Convert.ToInt16(strPosicion.Substring(0, 1))].ChildNodes[Convert.ToInt16(strPosicion.Substring(1, 1))].Focus();
                                    break;
                                default:
                                    tlsOperadores.Nodes[Convert.ToInt16(strPosicion.Substring(0, 1))].ChildNodes[Convert.ToInt16(strPosicion.Substring(1, 1))].ChildNodes[Convert.ToInt16(strPosicion.Substring(2, 1))].Focus();
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

                    if (Session[CVariablesSesion.GrillaTransaccion] != null)
                    {
                        dgvTransacciones.DataSource = Session[CVariablesSesion.GrillaTransaccion];
                        dgvTransacciones.DataBind();
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

        protected void pnlCabecera_Callback(object sender, CallbackEventArgsBase e)
        {

            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    cmbTipoOperacion.DataSource = null;
                    cmbTipoOperacion.Text = "";

                    if (tlsOperadores.FocusedNode != null)
                    {
                        if ((int)tlsOperadores.FocusedNode.GetValue("IntAccion") > 0)
                        {
                            var objPeriodo = client.Post<EResultadoPeriodos>(CAppSettings.svc_lista_periodos_consumidor,
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
                                Session[CVariablesSesion.ListaPeriodos] = objPeriodo.OResultado;

                                //string strFechaIni = "01/" + objPeriodo.OResultado[0].decMes.ToString().PadLeft(2, '0') + "/" + objPeriodo.OResultado[0].decAño;
                                //detFechaIni.Date = DateTime.Parse(strFechaIni);

                                //string strFechaFin = DateTime.DaysInMonth((int)objPeriodo.OResultado[0].decAño, (int)objPeriodo.OResultado[0].decMes).ToString().PadLeft(2,'0')+"/" + objPeriodo.OResultado[0].decMes.ToString().PadLeft(2, '0') + "/" + objPeriodo.OResultado[0].decAño;
                                //detFechaFin.Date = DateTime.Parse(strFechaFin);

                                cmbTipoOperacion_Callback(null, null);
                            }
                            else
                            {
                                MImprimirMensaje(objPeriodo.StrMensaje, EnumTiposMensajeAlerta.Error);
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
                    lblRepLegal.Text = (tlsOperadores.FocusedNode.GetValue("StrDireccion")==null)?"":tlsOperadores.FocusedNode.GetValue("StrDireccion").ToString();
                    lblDepto.Text = tlsOperadores.FocusedNode.GetValue("StrDepartamento").ToString();
                    MLimpiaGrilla();
                    if ((int) tlsOperadores.FocusedNode.GetValue("IntAccion") > 0)
                    {
                        string strMensaje = string.Empty;
                        if (MValidaDatosIniciales(ref strMensaje))
                        {
                            if (MValidaCondicionesFuncionamiento(ref strMensaje))
                            {
                                int intFiltro = 1;
                                string strDato1 = tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString();
                                string strDato2 = detFechaIni.Date.Year + detFechaIni.Date.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + detFechaIni.Date.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + "000000";
                                string strDato3 = detFechaFin.Date.Year + detFechaFin.Date.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + detFechaFin.Date.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + "235959";
                                string strDato4 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString();
                                string strDato5 = cmbTipoOperacion.Value.ToString();

                                MCargaParametricasEsp();
                                MConfiguraGrilla(Convert.ToInt32(strDato4));
                                MValidaBsisa(Convert.ToDecimal(strDato1), Convert.ToDecimal(strDato4), Convert.ToDecimal(strDato2), Convert.ToDecimal(strDato3));
                                MCargaGrilla(intFiltro, strDato1, strDato2, strDato3, strDato4, strDato5);
                            }
                            else
                            {
                                MImprimirMensaje(strMensaje, EnumTiposMensajeAlerta.Error);
                            }
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
                        decNivelCapa = (int) CLogTraza.CapasNivel.Presentacion
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

        protected void cmbTipoOperacion_Callback(object sender, CallbackEventArgsBase e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    cmbTipoOperacion.DataSource = null;

                    if (detFechaIni.Text == "" || detFechaFin.Text == "")
                    {
                        return;
                    }
                    else if (detFechaFin.Date < detFechaIni.Date)
                    {
                        return;
                    }
                    else if (detFechaIni.Date.Year + detFechaIni.Date.Month != detFechaFin.Date.Year + detFechaFin.Date.Month)
                    {
                        return;
                    }
                    
                    if (tlsOperadores.FocusedNode != null)
                    {
                        if ((int)tlsOperadores.FocusedNode.GetValue("IntAccion") > 0)
                        {
                            var objTipoOperacion = client.Post<EResultadoTipoOperacion>(CAppSettings.svc_obtener_tipos_operacion_act,
                            new
                            {
                                decFiltro = 3   ,
                                strParametro1 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
                                strParametro2 = tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString(),
                                strParametro3 = detFechaIni.Date.Year + detFechaIni.Date.Month.ToString().PadLeft(2, '0') + detFechaIni.Date.Day.ToString().PadLeft(2, '0') + "000000",
                                strParametro4 = detFechaFin.Date.Year + detFechaFin.Date.Month.ToString().PadLeft(2, '0') + detFechaFin.Date.Day.ToString().PadLeft(2, '0') + "235959"
                            });
                            if (objTipoOperacion.IntCodigo == 1)
                            {
                                cmbTipoOperacion.DataSource = objTipoOperacion.OResultado.OrderBy(x=>x.NOMBRE);
                                cmbTipoOperacion.DataBind();
                            }
                            else
                            {
                                MImprimirMensaje(objTipoOperacion.StrMensaje, EnumTiposMensajeAlerta.Error);
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
                                ASPxGridView dgvDocumentos = (dgvTransacciones.FindEditFormTemplateControl("dgvDocumentos")) as ASPxGridView;
                                dgvDocumentos.DataSource = Session[CVariablesSesion.GrillaAuxiliar];
                                dgvDocumentos.DataBind();
                            }
                            else
                            {
                                //todo grilla vacia: no implementado
                            }
                        }
                        catch (Exception)
                        {}
                        
                        dgvTransacciones = (sender) as ASPxGridView;
                        if (tlsOperadores.FocusedNode != null && (int)tlsOperadores.FocusedNode.GetValue("IntIdActividad") > 0)
                        {
                            #region Instancia componentes
                            ASPxTextBox txtIdEf = (dgvTransacciones.FindEditFormTemplateControl("txtIdEf")) as ASPxTextBox;
                            ASPxDateEdit detFechaEf = (dgvTransacciones.FindEditFormTemplateControl("detFechaEf")) as ASPxDateEdit;
                            ASPxComboBox cmbEstadoEf = (dgvTransacciones.FindEditFormTemplateControl("cmbEstadoEf")) as ASPxComboBox;
                            ASPxComboBox cmbTipoFlujoEf = (dgvTransacciones.FindEditFormTemplateControl("cmbTipoFlujoEf")) as ASPxComboBox;
                            ASPxComboBox cmbUnidadEf = (dgvTransacciones.FindEditFormTemplateControl("cmbUnidadEf")) as ASPxComboBox;
                            ASPxComboBox cmbTipoTransEf = (dgvTransacciones.FindEditFormTemplateControl("cmbTipoTransEf")) as ASPxComboBox;
                            ASPxComboBox cmbMedioTransEf = (dgvTransacciones.FindEditFormTemplateControl("cmbMedioTransEf")) as ASPxComboBox;
                            ASPxComboBox cmbEntidadAuxialiarEf = (dgvTransacciones.FindEditFormTemplateControl("cmbEntidadAuxialiarEf")) as ASPxComboBox;
                            ASPxComboBox cmbTipoMedicEf = (dgvTransacciones.FindEditFormTemplateControl("cmbTipoMedicEf")) as ASPxComboBox;
                            ASPxComboBox cmbTipoMovEf = (dgvTransacciones.FindEditFormTemplateControl("cmbTipoMovEf")) as ASPxComboBox;
                            ASPxComboBox cmbPaisEf = (dgvTransacciones.FindEditFormTemplateControl("cmbPaisEf")) as ASPxComboBox;
                            ASPxTextBox txtClienteEf = (dgvTransacciones.FindEditFormTemplateControl("txtClienteEf")) as ASPxTextBox;
                            ASPxTextBox txtNitEf = (dgvTransacciones.FindEditFormTemplateControl("txtNitEf")) as ASPxTextBox;
                            ASPxTextBox txtPlacaEf = (dgvTransacciones.FindEditFormTemplateControl("txtPlacaEf")) as ASPxTextBox;
                            ASPxTextBox txtSubIdentEf = (dgvTransacciones.FindEditFormTemplateControl("txtSubIdentEf")) as ASPxTextBox;
                            ASPxComboBox cmbTanqueEf = (dgvTransacciones.FindEditFormTemplateControl("cmbTanqueEf")) as ASPxComboBox;
                            ASPxTextBox txtVolAguaEf = (dgvTransacciones.FindEditFormTemplateControl("txtVolAguaEf")) as ASPxTextBox;
                            //ASPxComboBox cmbMangueraEf = (dgvTransacciones.FindEditFormTemplateControl("cmbMangueraEf")) as ASPxComboBox;
                            ASPxComboBox cmbProdEf = (dgvTransacciones.FindEditFormTemplateControl("cmbProdEf")) as ASPxComboBox;
                            ASPxTextBox txtCantidadEf = (dgvTransacciones.FindEditFormTemplateControl("txtCantidadEf")) as ASPxTextBox;
                            ASPxTextBox txtObsEf = (dgvTransacciones.FindEditFormTemplateControl("txtObsEf")) as ASPxTextBox;
                            ASPxTextBox txtAdicionalEf = (dgvTransacciones.FindEditFormTemplateControl("txtAdicionalEf")) as ASPxTextBox;
                            ASPxGridView dgvDocumentos = (dgvTransacciones.FindEditFormTemplateControl("dgvDocumentos")) as ASPxGridView;
                            ASPxTextBox txtCodigoEf = (dgvTransacciones.FindEditFormTemplateControl("txtCodigoEf")) as ASPxTextBox;
                            ASPxComboBox cmbTipoInfEf = (dgvTransacciones.FindEditFormTemplateControl("cmbTipoInfEf")) as ASPxComboBox;
                            ASPxComboBox cmbTipoEnvaseEf = (dgvTransacciones.FindEditFormTemplateControl("cmbTipoEnvaseEf")) as ASPxComboBox;

                            ASPxComboBox cmbTipoTempEf = (dgvTransacciones.FindEditFormTemplateControl("cmbTipoTempEf")) as ASPxComboBox;
                            ASPxTextBox txtTemperaturaEf = (dgvTransacciones.FindEditFormTemplateControl("txtTemperaturaEf")) as ASPxTextBox;
                            ASPxTextBox txtGravedadEf = (dgvTransacciones.FindEditFormTemplateControl("txtGravedadEf")) as ASPxTextBox;
                            ASPxTextBox txtIntegracionEf = (dgvTransacciones.FindEditFormTemplateControl("txtIntegracionEf")) as ASPxTextBox;
                            ASPxTextBox txtBarCodeEf = (dgvTransacciones.FindEditFormTemplateControl("txtBarCodeEf")) as ASPxTextBox;
                            
                            #endregion
                            if (param.Contains("ADDNEWROW"))
                            {
                                #region nuevo registro
                                Session.Remove(CVariablesSesion.GrillaAuxiliar);
                                txtIdEf.Text = "0";
                                txtBarCodeEf.Text = "";
                                detFechaEf.Date = detFechaIni.Date;

                                MCargaListados();
                                
                                cmbTipoTransEf.SelectedItem = cmbTipoTransEf.Items.FindByValue("-99999");
                                cmbMedioTransEf.SelectedItem = cmbMedioTransEf.Items.FindByValue("9999999999");
                                cmbTipoEnvaseEf.SelectedItem = cmbTipoEnvaseEf.Items.FindByValue("-99999");
                                cmbPaisEf.SelectedItem = cmbPaisEf.Items.FindByValue("120");
                                cmbProdEf.SelectedIndex = 0;
                                cmbTipoTempEf.SelectedItem = cmbTipoTempEf.Items.FindByValue("-99999");
                                cmbEntidadAuxialiarEf.SelectedItem = cmbEntidadAuxialiarEf.Items.FindByValue("-99999");

                                var lstDocumentoMov = new List<CRegistroDocumento>();
                                dgvDocumentos.DataSource = lstDocumentoMov;
                                dgvDocumentos.DataBind();
                                
                                #endregion
                            }
                            else if (param.Contains("STARTEDIT"))
                            {
                                #region editar registro
                                int index = dgvTransacciones.EditingRowVisibleIndex;
                                if (index >= 0)
                                {
                                    MCargaListados();

                                    Session.Remove(CVariablesSesion.GrillaAuxiliar);
                                    cmbEstadoEf.Enabled = false;
                                    txtIdEf.Text = dgvTransacciones.GetRowValues(index, "ID_MOVIMIENTO").ToString();
                                    detFechaEf.Date = DateTime.Parse(dgvTransacciones.GetRowValues(index, "STR_FECHA").ToString());
                                    cmbEstadoEf.Value = Convert.ToInt32(dgvTransacciones.GetRowValues(index, "ID_ESTADO_MOVIMIENTO"));
                                    cmbTipoFlujoEf.SelectedItem = cmbTipoFlujoEf.Items.FindByValue(dgvTransacciones.GetRowValues(index, "ID_TIPO_FLUJO").ToString());
                                    cmbTipoInfEf.SelectedItem = cmbTipoInfEf.Items.FindByValue(dgvTransacciones.GetRowValues(index, "TIPO_INFRAESTRUCTURA").ToString());
                                    cmbTanqueEf.SelectedItem = cmbTanqueEf.Items.FindByValue(dgvTransacciones.GetRowValues(index, "TIPO_INFRAESTRUCTURA").ToString().Trim().Replace("-", "") + dgvTransacciones.GetRowValues(index, "ID_INFRAESTRUCTURA").ToString().Trim().Replace("-", ""));
                                    cmbTipoTransEf.SelectedItem = cmbTipoTransEf.Items.FindByValue(dgvTransacciones.GetRowValues(index, "ID_TIPO_MEDIO_TRANSPORTE").ToString());
                                    cmbMedioTransEf.SelectedItem = dgvTransacciones.GetRowValues(index, "ID_MEDIO_TRANSPORTE").ToString().Trim() == "-99999" ? cmbMedioTransEf.Items.FindByValue("9999999999") : cmbMedioTransEf.Items.FindByValue(dgvTransacciones.GetRowValues(index, "ID_TIPO_MEDIO_TRANSPORTE").ToString().Trim().Replace("-", "") + dgvTransacciones.GetRowValues(index, "ID_MEDIO_TRANSPORTE").ToString().Trim().Replace("-", ""));
                                    
                                    //cmbEntidadAuxialiarEf.Value = Convert.ToInt32(dgvTransacciones.GetRowValues(index, "ID_ENTIDAD"));
                                    cmbEntidadAuxialiarEf.SelectedItem = cmbEntidadAuxialiarEf.Items.FindByValue(dgvTransacciones.GetRowValues(index, "ID_ENTIDAD").ToString());

                                    cmbTipoMedicEf.SelectedItem = cmbTipoMedicEf.Items.FindByValue(dgvTransacciones.GetRowValues(index, "ID_TIPO_MEDICION").ToString());
                                    cmbTipoMovEf.SelectedItem = cmbTipoMovEf.Items.FindByValue(dgvTransacciones.GetRowValues(index, "ID_TIPO_MOVIMIENTO").ToString());
                                    cmbPaisEf.SelectedItem = cmbPaisEf.Items.FindByValue(dgvTransacciones.GetRowValues(index, "ID_UBICACION_OPERACION").ToString());
                                    txtClienteEf.Text = (dgvTransacciones.GetRowValues(index, "RAZON_SOCIAL") == null) ? "" : (dgvTransacciones.GetRowValues(index, "TIPO_TRANSACCION").ToString().Contains("FACTURA"))?dgvTransacciones.GetRowValues(index, "RAZON_SOCIAL").ToString():"";
                                    txtNitEf.Text = (dgvTransacciones.GetRowValues(index, "NIT_CONSUMIDOR") == null) ? "" : dgvTransacciones.GetRowValues(index, "NIT_CONSUMIDOR").ToString();
                                    txtPlacaEf.Text = (dgvTransacciones.GetRowValues(index, "IDENTIFICADOR") == null) ? "" : dgvTransacciones.GetRowValues(index, "IDENTIFICADOR").ToString();
                                    txtSubIdentEf.Text = (dgvTransacciones.GetRowValues(index, "SUB_IDENTIFICADOR") == null) ? "" : dgvTransacciones.GetRowValues(index, "SUB_IDENTIFICADOR").ToString();
                                    //cmbMangueraEf.SelectedItem = cmbMangueraEf.Items.FindByValue(dgvTransacciones.GetRowValues(index, "ID_INFRAESTRUCTURA").ToString());
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

                                    var objTransaccion = client.Post<EResultadoTransaccion>(CAppSettings.svc_obtener_transaccion,
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
                            //if (dgvTransacciones != null) dgvTransacciones.CancelEdit();
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
                            objNuevoReg.idUbicacionOperacion = Convert.ToDecimal((cmbPaisEf.Value == null) ? "120" : cmbPaisEf.Value);
                            objNuevoReg.idUnidadMedidaHidrocarburo = Convert.ToDecimal(cmbUnidadEf.Value);
                            objNuevoReg.idHidrocarburo = Convert.ToDecimal(cmbProdEf.Value);
                            objNuevoReg.cantidadHidrocarburo = Convert.ToDecimal(txtCantidadEf.Text);
                            objNuevoReg.identificador = txtPlacaEf.Text;
                            objNuevoReg.subIdentificador = txtSubIdentEf.Text;
                            objNuevoReg.observacion = txtObsEf.Text.Trim();
                            objNuevoReg.barcode = Convert.ToDecimal(txtBarCodeEf.Text);
                            
                            objNuevoReg.idTipoMedicion = (cmbTipoMedicEf.Value == null) ? -99999 : Convert.ToDecimal(cmbTipoMedicEf.Value);
                            objNuevoReg.cantidadAguaLitros = Convert.ToDecimal(txtVolAguaEf.Text);
                            
                            objNuevoReg.idTipoMedioTransporte = (cmbTipoTransEf.Value == null) ? -99999 : Convert.ToDecimal(cmbTipoTransEf.Value);
                            if (cmbMedioTransEf.Value != null && cmbMedioTransEf.Value.ToString() != "9999999999")
                            {
                                var l1 = cmbTipoTransEf.Value.ToString().Trim().Replace("-","").Length;
                                var l2 = cmbMedioTransEf.Value.ToString().Trim().Replace("-", "").Length;
                                var c = cmbMedioTransEf.Value.ToString().Substring(l1, l2-l1);
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
                            //objNuevoReg.idUnidadTemperatura = Convert.ToDecimal(cmbTipoTempEf.Value);
                            objNuevoReg.idUnidadTemperatura = (cmbTipoTempEf.Value == null) ? -99999 : Convert.ToDecimal(cmbTipoTempEf.Value);
                            objNuevoReg.temperatura = Convert.ToDecimal(txtTemperaturaEf.Text);
                            objNuevoReg.gradoApi = txtGravedadEf.Text;
                            objNuevoReg.idEntidad = (cmbEntidadAuxialiarEf.Value == null) ? -99999 : Convert.ToDecimal(cmbEntidadAuxialiarEf.Value);

                            objNuevoReg.barcode = 0;
                            objNuevoReg.cantidadEnvasesTransporte = 0;
                            objNuevoReg.binarioControl = "00000-0000000000-0000000000-00000";
                            objNuevoReg.idTransaccionBaseExterna = "";
                            
                            objNuevoReg.listaTransaccion = new List<ObjTransaccion>();

                            var lstDocumentosMov = new List<O_LISTA_TRANSACCION_CTY>();
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
                                objNuevoReg.listaTransaccion.Add(objTrasaccion);
                            }
                            var objRegistro = new ERegistraMovimientoTransaccion();
                            objRegistro.strCredencial = CParametrosHydro.strCredencialFuncionario;
                            objRegistro.decFiltro = 1;
                            var lstRegistros = new List<ObjMovimientoTransaccion>();
                            lstRegistros.Add(objNuevoReg);
                            objRegistro.listaMovimientoTransaccion = lstRegistros;

                            var ojo = JsonSerializer.SerializeToString(objRegistro);

                            EResultado resultado = client.Post<EResultado>(CAppSettings.svc_registra_movimiento_transaccion, objRegistro);
                            if (resultado.intCodigo == 1)
                            {
                                e.Cancel = true;
                                dgvTransacciones.CancelEdit();
                                pnlDatos_Callback(null, null);
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
                    ArgumentException argEx = new ArgumentException(exp.Message);
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
                            objNuevoReg.identificador = txtPlacaEf.Text;
                            objNuevoReg.subIdentificador = txtSubIdentEf.Text;
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

                            objNuevoReg.listaTransaccion = new List<ObjTransaccion>();

                            var lstDocumentosMov = new List<O_LISTA_TRANSACCION_CTY>();
                            lstDocumentosMov = (List<O_LISTA_TRANSACCION_CTY>)Session[CVariablesSesion.GrillaAuxiliar];
                            foreach (var reg in lstDocumentosMov)
                            {
                                if (reg.ID_ESTADO_TRANSACCION == (decimal)EnumEstadoMov.Anulado)
                                {
                                    if (reg.ID_TRANSACCION > 0)
                                    {
                                        resultado = client.Post<EResultado>(CAppSettings.svc_eliminar_transaccion,
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

                                    if (reg.ID_PADRE_TRANSACCION == (int) EnumTipoTrasaccionPadre.Factura)
                                    {
                                        objTrasaccion.nitConsumidor = Convert.ToDecimal(txtNitEf.Text);
                                        objTrasaccion.razonSocial = txtClienteEf.Text.Trim();
                                    }
                                    else
                                    {
                                        objTrasaccion.nitConsumidor = reg.NIT_CONSUMIDOR;
                                        objTrasaccion.razonSocial = (reg.RAZON_SOCIAL==null)?"":reg.RAZON_SOCIAL.Trim();
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
                                    if (reg.ID_TRANSACCION > 0)
                                    {
                                        objNuevoReg.listaTransaccion.Add(objTrasaccion);
                                    }
                                    else
                                    {
                                        var objRegTransaccion = new ETransaccion();
                                        objRegTransaccion.strCredencial = CParametrosHydro.strCredencialFuncionario;
                                        objRegTransaccion.ObjTransaccion = objTrasaccion;
                                        resultado = client.Post<EResultado>(CAppSettings.svc_transaccion, objRegTransaccion);
                                        if (resultado.intCodigo != 1)
                                        {
                                            MImprimirMensaje(resultado.strMensaje, EnumTiposMensajeAlerta.Alerta);
                                        }
                                        else
                                        {
                                            //todo No implementado.
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
                            resultado = client.Post<EResultado>(CAppSettings.svc_actualiza_movimiento_transaccion, objRegistro);
                            if (resultado.intCodigo == 1)
                            {
                                e.Cancel = true;
                                dgvTransacciones.CancelEdit();
                                pnlDatos_Callback(null, null);
                            }
                            else
                            {
                                e1.RowError = "ERROR: WS " + resultado.strMensaje;
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
                    ArgumentException argEx = new ArgumentException(exp.Message);
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
                            resultado = client.Post<EResultado>(CAppSettings.svc_eliminar_movimiento,
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
                            resultado = client.Post<EResultado>(CAppSettings.svc_actualiza_estado_movimiento,
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
                            pnlDatos_Callback(null, null); 
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
                    if (dgvTransacciones.GetRowValues(e.VisibleIndex, "ESTADO_MOVIMIENTO").ToString().Contains("ANU"))
                        e.Row.BackColor = Color.FromArgb(249, 131, 131);
                    else if (Convert.ToInt32(dgvTransacciones.GetRowValues(e.VisibleIndex, "ID_TIPO_FLUJO"))==(int)EnumTipoMovimientoFlujo.Contable)
                        e.Row.BackColor = Color.FromArgb(194, 193, 195);
                    else
                    {
                        if (e.VisibleIndex%2 == 1)
                        {
                            e.Row.BackColor = Color.FromArgb(229, 253, 212);
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

        protected void dgvDocumentos_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    ASPxTextBox txtClienteEf = (dgvTransacciones.FindEditFormTemplateControl("txtClienteEf")) as ASPxTextBox;
                    ASPxTextBox txtNitEf = (dgvTransacciones.FindEditFormTemplateControl("txtNitEf")) as ASPxTextBox;
                    ASPxGridView dgvDocumentos = (dgvTransacciones.FindEditFormTemplateControl("dgvDocumentos")) as ASPxGridView;
                    ASPxComboBox cmbTipoDocEf = (dgvTransacciones.FindEditFormTemplateControl("cmbTipoDocEf")) as ASPxComboBox;

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
                        int index = dgvDocumentos.FocusedRowIndex;
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
                    Random rnd = new Random();
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
                            regDoc.NRO_AUTORIZACION = Convert.ToInt64(txtNoAutoEf.Text);
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
                            regDoc.ID_TIPO_CREDITO_FISCAL = (decimal) EnumTipoCredFiscal.No;
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
                    regDoc.OBSERVACIONES = txtObsDocEf.Text;
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

                    ASPxGridView dgvDocumentos = (dgvTransacciones.FindEditFormTemplateControl("dgvDocumentos")) as ASPxGridView;
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
                            e.Row.BackColor = Color.FromArgb(229, 253, 212);
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
                    O_OBTENER_CATALOGOS_CTY objCatalogo = (O_OBTENER_CATALOGOS_CTY)Session[CVariablesSesion.ListaParametrica];
                    cmbMonedaEf.DataSource = objCatalogo.CATALOGO_TIPOS_MONEDA;
                    cmbMonedaEf.DataBind();
                    cmbMonedaEf.SelectedItem = cmbMonedaEf.Items.FindByValue("1");

                    var lstParam = objCatalogo.CATALOGO_PARAMETRICAS_DOMINIO;
                    foreach (var param in lstParam)
                    {
                        var lstObtCatalogo = client.Post<EResultadoParametricaGlobal>
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
                    
                    ASPxDateEdit detFechaEf = (dgvTransacciones.FindEditFormTemplateControl("detFechaEf")) as ASPxDateEdit;
                    ASPxTextBox txtCantidadEf = (dgvTransacciones.FindEditFormTemplateControl("txtCantidadEf")) as ASPxTextBox;
                    ASPxComboBox cmbTipoDocEf = (dgvTransacciones.FindEditFormTemplateControl("cmbTipoDocEf")) as ASPxComboBox;
                    ASPxGridView dgvDocumentos = (dgvTransacciones.FindEditFormTemplateControl("dgvDocumentos")) as ASPxGridView;
                    ASPxComboBox cmbTipoFlujoEf = (dgvTransacciones.FindEditFormTemplateControl("cmbTipoFlujoEf")) as ASPxComboBox;
                    
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

                    EResultadoI resultado = clientI.Post<EResultadoI>(CAppSettings.svc_RegistraLlaveMiddleware, oRegistraLlaveMiddleware);
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

        #endregion

        #region Metodos y funciones

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

        private void ConfigurarFormulario()
        {
            detFechaIni.Date = DateTime.Now;
            detFechaFin.Date = DateTime.Now;

            cmbTipoOperacion.Enabled = true;

            //switch ((int)Session[CVariablesSesion.UsuarioPerfilId])
            //{
            //    case (int)EnumContextoUsuario.Administrador:
            //        break;
            //    case (int)EnumContextoUsuario.Operador:
            //        break;
            //    default:
            //        break;
            //}
        }

        private void MCargaArbol()
        {
            if (Properties.Settings.Default.intOrdenArbol == 1)
            {
                MArbolOrdenEntidad();
            }
            else
            {
                MArbolOrdenActividad();
            }
        }

        private void MCargaParametricas()
        {
            //TIPO TRANSACCION
            var lstObjTransaccion = client.Post<EResultadoTipoTransaccion>(CAppSettings.svc_lista_tipos_transaccion,
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
            var lstObtCatalogo = client.Post<EResultadoParametrica>(CAppSettings.svc_obtener_catalogo,
            new {
                strLlave = ""
            });

            if (lstObtCatalogo.IntCodigo == 1)
            {
                O_OBTENER_CATALOGOS_CTY objCatalogo = lstObtCatalogo.OResultado;
                Session[CVariablesSesion.ListaParametrica] = objCatalogo;
            }
            else
            {
                //todo No implementado.
            }
            //PAIS
            var objParametricas = new O_PARAMETRICAS_GRAL();
            var objUbicacion = client.Post<EResultadoUbicacion>(CAppSettings.svc_obtiene_ubicacion,
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
            var objContenedor = client.Post<EResultadoTipoContenedor>(CAppSettings.svc_lista_tipos_contenedores,
            new
            {
                strCredencial = CParametrosHydro.strCredencialFuncionario,
                decFiltro = 1
            });
            if (objContenedor.intCodigo == 1)
            {
                objParamInfra.TIPOS_CONTENEDORES = objContenedor.oResultado;
            }
            //var objParamInfraestrutura = clientI.Get<EResultadoParamInfra>(CAppSettings.svc_LListadoParametros + "/" + CParametrosHydro.strCredencialInfra + "/DESC/TIPOS_MEDIOS_TRANSPORTE?format=json");
            //if (objParamInfraestrutura.decCodigo == 1 && objParamInfraestrutura.oResultado != null && objParamInfraestrutura.oResultado.Any())
            //{
            //    objParamInfraestrutura.oResultado.Add(new O_PARAMETROS_CTY() { ID_PARAMETRO = -99999, CODIGO = "NOID", VALOR = "No Identificado", DESCRIPCION = "No Identificado", ORDEN = 0, ID_DOMINIO_PARAMETRO = 0 });
            //    objParamInfra.TIPOS_MEDIOS_TRANSPORTE = objParamInfraestrutura.oResultado;
            //}



            //TIPO INFRAESTRUCTURA
            var objParamInfraestrutura = clientI.Get<EResultadoParamInfra>(CAppSettings.svc_LListadoParametros + "/" + CParametrosHydro.strCredencialInfra + "/DESC/TIPO_INFRAESTRUCTURA?format=json");
            if (objParamInfraestrutura.decCodigo == 1 && objParamInfraestrutura.oResultado != null && objParamInfraestrutura.oResultado.Any())
            {
                objParamInfraestrutura.oResultado.Add(new O_PARAMETROS_CTY() { ID_PARAMETRO = -99999, CODIGO = "NOID", VALOR = "No Identificado", DESCRIPCION = "No Identificado", ORDEN = 0, ID_DOMINIO_PARAMETRO = 0 });
                objParamInfra.TIPO_INFRAESTRUCTURA = objParamInfraestrutura.oResultado;
            }
            Session[CVariablesSesion.ListaParamInfra] = objParamInfra;
        }

        private void MCargaParametricasEsp()
        {
            //PARAMETRICAS GENERALES
            var objParametros = client.Post<EResultadoOctanoParametro>(CAppSettings.svc_listar_octano_parametrica,
            new
            {
                strCredencial = CParametrosHydro.strCredencialFuncionario,
                decFiltro = 1,//1: ID_TIPO_ACTIVIDAD 2: ID_TIPO_ACTIVIDAD y VALOR_TIPO
                strParametro1 = "",//VALOR_TIPO
                strParametro2 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
                strParametro3 = "",
                strParametro4 = ""
            });
            if (objParametros.IntCodigo==1)
            {
                Session[CVariablesSesion.OctanoParametrica] = objParametros.OResultado;
            }
            else
            {
                Session.Remove(CVariablesSesion.OctanoParametrica);
            }
        }

        private void MValidaBsisa(decimal decIdConsumidor, decimal decIdTipoActividad, decimal decFechaHoraInicio, decimal decFechaHoraFin)
        {
            //Baja logica  los movimientos duplicados por anulacion B-Sisa
            var objResultado = client.Post<EResultado>
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

        private void MCargaGrilla(int decFiltro, string strParametro1, string strParametro2, string strParametro3, string strParametro4, string strParametro5)
        {
            client.Timeout = new TimeSpan(0, 0, 5, 0);
            var lstDespachos = client.Post<EResultadoMovimientoVista>
                    (CAppSettings.svc_obtener_movimiento_producto_vista,
                    new
                    {
                        decFiltro,          //TipoMovimiento (1=ventas)
                        strParametro1,      //Id proyecto/entidad
                        strParametro2,      //fecha inicial
                        strParametro3,      //fecha final
                        strParametro4,      //id Actividad
                        strParametro5,      //tipo operacion
                        strParametro6 = "0" //producto - no utilizado
                    });
            if (lstDespachos.IntCodigo == 1)
            {
                List<O_CONSULTA_MOVI_TRAN_CTY> lstResultado = lstDespachos.OResultado;

                if (lstResultado.Any())
                {
                    var registros = lstResultado;
                    registros.ForEach(a => a.STR_ESTADO_DEC = (a.BINARIO_CONTROL.Substring(1, 1) == "1") ? "DEC" : "NOD");
                    registros.ForEach(a => a.STR_FECHA = a.FECHA_OPERACION.ToString().Substring(6, 2) + "/" + a.FECHA_OPERACION.ToString().Substring(4, 2) + "/" + a.FECHA_OPERACION.ToString().Substring(0, 4) + " " + a.FECHA_OPERACION.ToString().Substring(8, 2) + ":" + a.FECHA_OPERACION.ToString().Substring(10, 2) + ":" + a.FECHA_OPERACION.ToString().Substring(12, 2));

                    Session[CVariablesSesion.GrillaTransaccion] = registros;
                    dgvTransacciones.DataSource = registros;
                    dgvTransacciones.DataBind();
                }
            }
            else
            {
                MImprimirMensaje(lstDespachos.StrMensaje, (lstDespachos.StrMensaje=="No se encontraron registros")? EnumTiposMensajeAlerta.Alerta: EnumTiposMensajeAlerta.Error);
            }
        }

        private void MCargaListados()
        {
            #region Infraestructura

            var objParamInf = Session[CVariablesSesion.ListaParamInfra] as O_PARAMETROS_INFRA;
            ASPxComboBox cmbTipoInfEf = (dgvTransacciones.FindEditFormTemplateControl("cmbTipoInfEf")) as ASPxComboBox;
            cmbTipoInfEf.DataSource = objParamInf.TIPO_INFRAESTRUCTURA;
            cmbTipoInfEf.DataBind();

            ASPxComboBox cmbTipoTransEf = (dgvTransacciones.FindEditFormTemplateControl("cmbTipoTransEf")) as ASPxComboBox;
            cmbTipoTransEf.DataSource = objParamInf.TIPO_INFRAESTRUCTURA;
            cmbTipoTransEf.DataBind();

            ASPxComboBox cmbTipoEnvaseEf = (dgvTransacciones.FindEditFormTemplateControl("cmbTipoEnvaseEf")) as ASPxComboBox;
            cmbTipoEnvaseEf.DataSource = objParamInf.TIPOS_CONTENEDORES;
            cmbTipoEnvaseEf.DataBind();

            ASPxComboBox cmbMedioTransEf = (dgvTransacciones.FindEditFormTemplateControl("cmbMedioTransEf")) as ASPxComboBox;

            int intTipoInfra = 0;
            int intIdEntidad = Convert.ToInt32(tlsOperadores.FocusedNode.GetValue("IntIdProyecto").ToString());
            int intIdActividad = Convert.ToInt32(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
            var objInfraestructura = clientI.Get<EResultadoInfraestructura>(CAppSettings.svc_ListadoInfraestructuraVol + "/" + CParametrosHydro.strCredencialInfra + "/" + intIdEntidad + "/" + intIdActividad + "/" + intTipoInfra + "?format=json");
            
            ASPxComboBox cmbTanqueEf = (dgvTransacciones.FindEditFormTemplateControl("cmbTanqueEf")) as ASPxComboBox;
            ASPxComboBox cmbProdEf = (dgvTransacciones.FindEditFormTemplateControl("cmbProdEf")) as ASPxComboBox;

            if (objInfraestructura.decCodigo == 1 && objInfraestructura.oResultado != null && objInfraestructura.oResultado.Any() && objInfraestructura.oResultado.FirstOrDefault().ID_ENTIDAD!=-99)
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
            var objHidrocarburo = client.Post<EResultadoConfigHidrocarburo>(CAppSettings.svc_listar_autorizacion_condicion,
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
            ASPxComboBox cmbEntidadAuxialiarEf = (dgvTransacciones.FindEditFormTemplateControl("cmbEntidadAuxialiarEf")) as ASPxComboBox;
            cmbEntidadAuxialiarEf.DataSource = null;
            cmbEntidadAuxialiarEf.DataBind();
            
            var objEntidades = client.Post<EResultadoConfigHidrocarburo>(CAppSettings.svc_listar_autorizacion_condicion,
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

            ASPxComboBox cmbPaisEf = (dgvTransacciones.FindEditFormTemplateControl("cmbPaisEf")) as ASPxComboBox;
            var objParametricas = new O_PARAMETRICAS_GRAL();
            objParametricas = (O_PARAMETRICAS_GRAL)Session[CVariablesSesion.ListaParamGral];
            cmbPaisEf.DataSource = objParametricas.lstPais;
            cmbPaisEf.DataBind();

            ASPxComboBox cmbTipoDocEf = (dgvTransacciones.FindEditFormTemplateControl("cmbTipoDocEf")) as ASPxComboBox;
            cmbTipoDocEf.DataSource = Session[CVariablesSesion.ListaTransaccion];
            cmbTipoDocEf.DataBind();
            cmbTipoDocEf.SelectedItem = cmbTipoDocEf.Items.FindByValue("2");

            //Carga combos de parametricas
            O_OBTENER_CATALOGOS_CTY objCatalogo = (O_OBTENER_CATALOGOS_CTY) Session[CVariablesSesion.ListaParametrica];
            ASPxComboBox cmbUnidadEf = (dgvTransacciones.FindEditFormTemplateControl("cmbUnidadEf")) as ASPxComboBox;
            ASPxComboBox cmbTipoMedicEf = (dgvTransacciones.FindEditFormTemplateControl("cmbTipoMedicEf")) as ASPxComboBox;
            ASPxComboBox cmbTipoMovEf = (dgvTransacciones.FindEditFormTemplateControl("cmbTipoMovEf")) as ASPxComboBox;
            ASPxComboBox cmbTipoFlujoEf = (dgvTransacciones.FindEditFormTemplateControl("cmbTipoFlujoEf")) as ASPxComboBox;
            ASPxComboBox cmbTipoTempEf = (dgvTransacciones.FindEditFormTemplateControl("cmbTipoTempEf")) as ASPxComboBox;

            cmbUnidadEf.DataSource = objCatalogo.CATALOGO_UNIDADES_MEDIDA_GRAL;
            cmbUnidadEf.DataBind();
            cmbTipoTempEf.DataSource = objCatalogo.CATALOGO_UNIDADES_MEDIDA_GRAL;
            cmbTipoTempEf.DataBind();
            cmbTipoMedicEf.DataSource = objCatalogo.CATALOGO_TIPOS_MEDICION;
            cmbTipoMedicEf.DataBind();
            cmbTipoMedicEf.SelectedItem = cmbTipoMedicEf.Items.FindByValue("1");

            //cmbTipoTransEf.DataSource = objCatalogo.CATALOGO_TIPOS_TRANSPORTE;
            //cmbTipoTransEf.DataBind();
            //cmbTipoTransEf.SelectedItem = cmbTipoTransEf.Items.FindByValue("-99999");

            var lstParam = objCatalogo.CATALOGO_PARAMETRICAS_DOMINIO;
            foreach (var param in lstParam)
            {
                var lstObtCatalogo = client.Post<EResultadoParametricaGlobal>
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
                    }
                }
                else
                {
                    //todo No implementado.
                }
            }

            #endregion
        }

        private bool MValidaDatosIniciales(ref string strMensaje)
        {
            bool booResultado = true;

            if (cmbTipoOperacion.Text == "")
            {
                strMensaje = "Introduzca el tipo de operación.";
                booResultado = false;
            }
            else if (detFechaIni.Text == "" || detFechaFin.Text == "")
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

        private bool MValidaDatos(ref string strMensaje, int intIdOperacion, int intIdActividad)
        {
            bool booResultado = true;

            #region Instancia componentes
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

            if (txtNitEf != null && txtNitEf.Visible && txtNitEf.Text == "0" && txtClienteEf.Text.Trim().Length == 0)
            {
                txtClienteEf.Text = "SIN NOMBRE";
            }
            else if (txtCantidadEf.Text == "" || Convert.ToDecimal(txtCantidadEf.Text) < 0 || (!cmbTipoOperacion.Text.Contains("TOSICB") && Convert.ToDecimal(txtCantidadEf.Text) == 0))
            {
                strMensaje = "ERROR: Introduzca la cantidad del movimiento.";
            }
            else if (txtPlacaEf != null && txtPlacaEf.Visible && txtPlacaEf.Text == "")
            {
                strMensaje = "ERROR: Introduzca una placa.";
            }
            //else if (Convert.ToInt32(cmbTipoMovEf.Value) != (int)EnumTipoMovimiento.NacNac && (cmbPaisEf.Value==null || Convert.ToInt32(cmbPaisEf.Value) == 120))
            else if (cmbPaisEf.Value != null && (Convert.ToInt32(cmbTipoMovEf.Value) != (int)EnumTipoMovimiento.NacNac && (cmbPaisEf.Value == null || Convert.ToInt32(cmbPaisEf.Value) == 120)))
            {
                strMensaje = "ERROR: Introduzca un país válido.";
            }
            else
            {
                //todo No implementado.
            }
            
            switch (intIdOperacion)
            {
                case (int)EnumTipoOperaciones.VentasEess:
                    if (Convert.ToInt32(cmbTipoMovEf.Value) != (int)EnumTipoMovimiento.NacNac && (int)cmbPaisEf.Value == 120)
                    {
                        strMensaje = "ERROR: Introduzca un país válido.";
                    }
                    else
                    {
                        //todo No implementado.
                    }
                    break;

                case (int)EnumTipoOperaciones.MermaCombustible:
                    break;
                case (int)EnumTipoOperaciones.SalidaxTransferencia:
                    if (cmbEntidadAuxialiarEf != null && (long)cmbEntidadAuxialiarEf.Value == -99999)
                    {
                        strMensaje = "ERROR: Defina un destino.";
                    }
                    else
                    {
                        //todo No implementado.
                    }
                    break;
                case (int)EnumTipoOperaciones.RecepcionCombustible:
                    break;
                case (int)EnumTipoOperaciones.DemasiaCombustible:
                    break;
                case (int)EnumTipoOperaciones.IngresoxTransferencia:
                    if (cmbEntidadAuxialiarEf != null && (long)cmbEntidadAuxialiarEf.Value == -99999)
                    {
                        strMensaje = "ERROR: Defina un origen.";
                    }
                    else
                    {
                        //todo No implementado.
                    }
                    break;
                case (int)EnumTipoOperaciones.SaldoInicial:
                    //if (detFechaEf.Date.Day != 1)
                    //{
                    //    strMensaje = "ERROR: El saldo inicial debe registrarse el primer día del mes.";
                    //}
                    //else
                    //{
                    //    var lstValida = (List<O_LISTA_MOVIMIENTO_PROD_CTY>) Session[CVariablesSesion.GrillaTransaccion];
                    //    if (lstValida != null)
                    //    {
                    //        lstValida = lstValida.Where(x => (x.ID_PRODUCTO == Convert.ToDecimal(cmbProdEf.Value)) && (x.ID_MOVIMIENTO != Convert.ToDecimal(txtIdEf.Text))).ToList();
                    //        if (lstValida.Any())
                    //        {
                    //            strMensaje = "ERROR: Solo puede registrar un movimiento de Saldo Inicial por producto.";
                    //        }
                    //        else
                    //        {
                    //            //todo No implementado.
                    //        }
                    //    }
                    //    else
                    //    {
                    //        //todo No implementado.
                    //    }
                    //}
                    break;
                case (int)EnumTipoOperaciones.MedicionInicialTq:
                    if (cmbTanqueEf != null && (cmbTanqueEf.Value == "-99999" || cmbTanqueEf.Value == null))
                    {
                        strMensaje = "ERROR: Defina un tánque.";
                    }
                    else
                    {
                        //todo No implementado.
                    }
                    break;
                case (int)EnumTipoOperaciones.MedicionFinalTq:
                    if (cmbTanqueEf != null && (int)cmbTanqueEf.Value == -99999)
                    {
                        strMensaje = "ERROR: Defina un tánque.";
                    }
                    else
                    {
                        //todo No implementado.
                    }
                    break;
            }

            switch (intIdActividad)
            {
                case (int)EnumActividades.EstacionServicioGnv:
                    if (Convert.ToInt32(cmbProdEf.Value.ToString()) != (int) EnumProductos.Gnv)
                    {
                        strMensaje = "ERROR: Producto incorrecto.";
                    }
                    else
                    {
                        //todo No implementado.
                    }
                    break;
                case (int)EnumActividades.EstacionServicioLiquido:
                    if (Convert.ToInt32(cmbProdEf.Value.ToString()) == (int) EnumProductos.Gnv)
                    {
                        strMensaje = "ERROR: Producto incorrecto.";
                    }
                    else
                    {
                        //todo No implementado.
                    }
                    break;
                case (int)EnumActividades.DistribucionGlpGarrafas:
                case (int)EnumActividades.EngarrafadoGlp:
                    if (Convert.ToInt32(cmbProdEf.Value.ToString()) != (int) EnumProductos.GasLicuadoPetroleo)
                    {
                        strMensaje = "ERROR: Producto incorrecto.";
                    }
                    else
                    {
                        //todo No implementado.
                    }
                    break;
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
                    //else if (rbtTipoVentaEf.Visible)
                    //    switch (intIdActividad)
                    //    {
                    //        case (int)EnumActividades.EstacionServicioLiquido:
                    //        case (int)EnumActividades.EstacionServicioGnv:
                    //            if (cmbTipoMovEf.SelectedItem.GetValue("CODIGO") == "NN")
                    //            {

                    //                lstValidaDoc = lstDocumentoMov.Where(x => x.ID_TIPO_EMISOR == (int)EnumTipoEmisor.PropiaEntidad).ToList();
                    //                if (lstValidaDoc.Count == 0)
                    //                {
                    //                    strMensaje = "ERROR: Debe agregar al menos una factura propia.";
                    //                }

                    //                if ((int)rbtTipoVentaEf.Value == (int)EnumTipoMovimiento.NacInt)
                    //                {
                    //                    lstValidaDoc = lstDocumentoMov.Where(x => x.IntIdEmisor != (int)EnumTipoEmisor.PropiaEntidad).ToList();
                    //                    if (lstValidaDoc.Count == 0)
                    //                    {
                    //                        strMensaje = "ERROR: Debe agregar al menos una factura externa.";
                    //                    }
                    //                }
                    //            }

                    //            break;
                    //        default:
                    //            break;
                    //    }
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
                            else if (doc.ID_PADRE_TRANSACCION == (decimal) EnumTipoTrasaccionPadre.Factura)
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

        private bool MValidaCondicionesFuncionamiento(ref string strMesnaje)
        {
            bool booRespuesta = true;

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
        
        private void MConfiguraGrilla(int idActividad)
        {
            dgvTransacciones.Columns[(int)EnumColumnasGrilla.FoAbm].Visible = false;
            dgvTransacciones.Columns[(int)EnumColumnasGrilla.FoId].Visible = false;
            dgvTransacciones.Columns[(int)EnumColumnasGrilla.FoPlaca].Visible = false;
            dgvTransacciones.Columns[(int)EnumColumnasGrilla.FoPais].Visible = false;
            dgvTransacciones.Columns[(int)EnumColumnasGrilla.FoNoAutorizacion].Visible = false;
            dgvTransacciones.Columns[(int)EnumColumnasGrilla.FoNit].Visible = false;
            dgvTransacciones.Columns[(int)EnumColumnasGrilla.FoPrecio].Visible = false;
            dgvTransacciones.Columns[(int)EnumColumnasGrilla.FoImporte].Visible = false;
            dgvTransacciones.Columns[(int)EnumColumnasGrilla.FoTipoMovimiento].Visible = false;

            var to = cmbTipoOperacion.Text.Split(';');
            var strTipoOp = to[1].Substring(2, 2);

            switch (strTipoOp)
            {
                case "RC":
                case "DS":
                case "VT":
                    dgvTransacciones.Columns[(int)EnumColumnasGrilla.FoNoAutorizacion].Visible = true;
                    dgvTransacciones.Columns[(int)EnumColumnasGrilla.FoNofactura].Visible = true;
                    dgvTransacciones.Columns[(int)EnumColumnasGrilla.FoNit].Visible = true;
                    dgvTransacciones.Columns[(int)EnumColumnasGrilla.FoPrecio].Visible = true;
                    dgvTransacciones.Columns[(int)EnumColumnasGrilla.FoImporte].Visible = true;
                    dgvTransacciones.Columns[(int)EnumColumnasGrilla.FoTipoMovimiento].Visible = true;
                    switch (idActividad)
                    {
                        case (int)EnumActividades.EstacionServicioLiquido:
                        case (int)EnumActividades.EstacionServicioGnv:
                            dgvTransacciones.Columns[(int)EnumColumnasGrilla.FoPlaca].Visible = true;
                            dgvTransacciones.Columns[(int)EnumColumnasGrilla.FoPais].Visible = true;
                            break;
                        default:
                            break;
                    }
                    break;
                case "AJ":
                    break;
                case "MV":
                    dgvTransacciones.Columns[(int)EnumColumnasGrilla.FoPrecio].Visible = true;
                    dgvTransacciones.Columns[(int)EnumColumnasGrilla.FoImporte].Visible = true;
                    dgvTransacciones.Columns[(int)EnumColumnasGrilla.FoTipoMovimiento].Visible = true;
                    break;
                case "SI":
                    break;
                case "ME":
                    break;
            }

            string strMensaje = string.Empty;
            if (MValidaPermisos(ref strMensaje))
            {
                dgvTransacciones.Columns[(int)EnumColumnasGrilla.FoAbm].Visible = true;
            }
            else
            {
                MImprimirMensaje(strMensaje, EnumTiposMensajeAlerta.Error);
            }

        }

        private bool MValidaPermisos(ref string strMesnaje)
        {
            bool booRespuesta = true;

            //Valida si esta en periodo
            var lstPeriodos = Session[CVariablesSesion.ListaPeriodos] as List<O_PERIODO_CTY>;
            if (lstPeriodos.Any())
            {
                var lstValida = lstPeriodos.Where(x => x.decAño == detFechaIni.Date.Year && x.decMes == detFechaIni.Date.Month).ToList();

                if (lstValida.Any())
                {
                    //Valida si exixte DDJJ en periodo
                    var objValidacion = client.Post<EResultadoGenerico>(CAppSettings.svc_validaciones_general,
                    new
                    {
                        strCredencial = CParametrosHydro.strCredencialFuncionario,
                        strFiltro = "1",
                        strParametro1 = tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString(),
                        strParametro2 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
                        strParametro3 = "",
                        strParametro4 = detFechaIni.Date.Year + detFechaIni.Date.Month.ToString().PadLeft(2, '0') + "01" + "000000",
                        strParametro5 = detFechaFin.Date.Year + detFechaFin.Date.Month.ToString().PadLeft(2, '0') + DateTime.DaysInMonth(detFechaFin.Date.Year,detFechaFin.Date.Month).ToString().PadLeft(2, '0') + "235959",
                        strParametro6 = "",
                        decAppFechaRegistro = 0,
                        decAppIdUsuario = 0
                    });
                    if (objValidacion.IntCodigo == 1)
                    {
                        if (objValidacion.OResultado.RESULTADO == 1)
                        {
                            //Valida si el tipo de operacion esta habilitado para la accion
                            var objTipoOperacion = client.Post<EResultadoConfigTipoOperacion>(CAppSettings.svc_listar_autorizacion_accion,
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
                                    if (objTipoOperacion.OResultado.FirstOrDefault().BINARIO_ESTADO.Substring(1, 1) == "0")
                                    {
                                        booRespuesta = false;
                                        strMesnaje = "Función no permitida.";
                                    }
                                    else
                                    {
                                        //todo No implementado.
                                    }
                                }
                                else
                                {
                                    if (!cmbTipoOperacion.Text.Contains("TOSICB"))
                                    {
                                        booRespuesta = false;
                                        strMesnaje = "Inconsistencia de datos. Tipo de operacion no encontrada.";
                                    }
                                    else
                                    {
                                        //todo No implementado.
                                    }
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
                }
                else
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

            return booRespuesta;
        }

        private void MConfiguraFormulario()
        {
            #region Instancia componentes
            ASPxComboBox cmbTipoTransEf = (dgvTransacciones.FindEditFormTemplateControl("cmbTipoTransEf")) as ASPxComboBox;
            ASPxComboBox cmbMedioTransEf = (dgvTransacciones.FindEditFormTemplateControl("cmbMedioTransEf")) as ASPxComboBox;
            ASPxComboBox cmbEntidadAuxialiarEf = (dgvTransacciones.FindEditFormTemplateControl("cmbEntidadAuxialiarEf")) as ASPxComboBox;
            ASPxComboBox cmbTipoMedicEf = (dgvTransacciones.FindEditFormTemplateControl("cmbTipoMedicEf")) as ASPxComboBox;
            ASPxComboBox cmbTipoMovEf = (dgvTransacciones.FindEditFormTemplateControl("cmbTipoMovEf")) as ASPxComboBox;
            ASPxComboBox cmbPaisEf = (dgvTransacciones.FindEditFormTemplateControl("cmbPaisEf")) as ASPxComboBox;
            ASPxTextBox txtPlacaEf = (dgvTransacciones.FindEditFormTemplateControl("txtPlacaEf")) as ASPxTextBox;
            ASPxTextBox txtSubIdentEf = (dgvTransacciones.FindEditFormTemplateControl("txtSubIdentEf")) as ASPxTextBox;
            ASPxTextBox txtCodigoEf = (dgvTransacciones.FindEditFormTemplateControl("txtCodigoEf")) as ASPxTextBox;
            ASPxComboBox cmbTanqueEf = (dgvTransacciones.FindEditFormTemplateControl("cmbTanqueEf")) as ASPxComboBox;
            ASPxTextBox txtVolAguaEf = (dgvTransacciones.FindEditFormTemplateControl("txtVolAguaEf")) as ASPxTextBox;
            ASPxLabel lblTipoTransEf = (dgvTransacciones.FindEditFormTemplateControl("lblTipoTransEf")) as ASPxLabel;
            ASPxLabel lblMedioTransEf = (dgvTransacciones.FindEditFormTemplateControl("lblMedioTransEf")) as ASPxLabel;
            ASPxLabel lblEntidadAuxialiarEf = (dgvTransacciones.FindEditFormTemplateControl("lblEntidadAuxialiarEf")) as ASPxLabel;
            ASPxLabel lblTipoMedicEf = (dgvTransacciones.FindEditFormTemplateControl("lblTipoMedicEf")) as ASPxLabel;
            ASPxLabel lblTipoMovEf = (dgvTransacciones.FindEditFormTemplateControl("lblTipoMovEf")) as ASPxLabel;
            ASPxLabel lblPaisEf = (dgvTransacciones.FindEditFormTemplateControl("lblPaisEf")) as ASPxLabel;
            ASPxLabel lblPlacaEf = (dgvTransacciones.FindEditFormTemplateControl("lblPlacaEf")) as ASPxLabel;
            ASPxLabel lblSubIdentEf = (dgvTransacciones.FindEditFormTemplateControl("lblSubIdentEf")) as ASPxLabel;
            ASPxLabel lblCodigoEf = (dgvTransacciones.FindEditFormTemplateControl("lblCodigoEf")) as ASPxLabel;
            ASPxLabel lblTanqueEf = (dgvTransacciones.FindEditFormTemplateControl("lblTanqueEf")) as ASPxLabel;
            ASPxLabel lblVolAguaEf = (dgvTransacciones.FindEditFormTemplateControl("lblVolAguaEf")) as ASPxLabel;
            ASPxComboBox cmbTipoTempEf = (dgvTransacciones.FindEditFormTemplateControl("cmbTipoTempEf")) as ASPxComboBox;
            ASPxLabel lblTemperaturaEf = (dgvTransacciones.FindEditFormTemplateControl("lblTemperaturaEf")) as ASPxLabel;
            ASPxTextBox txtTemperaturaEf = (dgvTransacciones.FindEditFormTemplateControl("txtTemperaturaEf")) as ASPxTextBox;
            ASPxLabel lblGravedadEf = (dgvTransacciones.FindEditFormTemplateControl("lblGravedadEf")) as ASPxLabel;
            ASPxTextBox txtGravedadEf = (dgvTransacciones.FindEditFormTemplateControl("txtGravedadEf")) as ASPxTextBox;
            ASPxLabel lblIntegracionEf = (dgvTransacciones.FindEditFormTemplateControl("lblIntegracionEf")) as ASPxLabel;
            ASPxTextBox txtIntegracionEf = (dgvTransacciones.FindEditFormTemplateControl("txtIntegracionEf")) as ASPxTextBox;
            ASPxComboBox cmbTipoEnvaseEf = (dgvTransacciones.FindEditFormTemplateControl("cmbTipoEnvaseEf")) as ASPxComboBox;
            ASPxLabel lblTipoEnvaseEf = (dgvTransacciones.FindEditFormTemplateControl("lblTipoEnvaseEf")) as ASPxLabel;
            ASPxLabel lblTipoInfEf = (dgvTransacciones.FindEditFormTemplateControl("lblTipoInfEf")) as ASPxLabel;
            ASPxComboBox cmbTipoInfEf = (dgvTransacciones.FindEditFormTemplateControl("cmbTipoInfEf")) as ASPxComboBox;
            ASPxLabel lblBarCodeEf = (dgvTransacciones.FindEditFormTemplateControl("lblBarCodeEf")) as ASPxLabel;
            ASPxTextBox txtBarCodeEf = (dgvTransacciones.FindEditFormTemplateControl("txtBarCodeEf")) as ASPxTextBox;
            #endregion

            var to = cmbTipoOperacion.Text.Split(';');
            var strTipoOp = to[1].Substring(2, 4);

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
                            (int) EnumActividades.EstacionServicioLiquido ||
                            Convert.ToInt32(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()) ==
                            (int) EnumActividades.EstacionServicioGnv)
                        {
                            lblTemperaturaEf.Visible = false;
                            txtTemperaturaEf.Visible = false;
                            cmbTipoTempEf.Visible = false;
                            lblGravedadEf.Visible = false;
                            txtGravedadEf.Visible = false;
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
                    #endregion
                    break;
            }
        }

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
        
        private void MLimpiaGrilla()
        {
            Session.Remove(CVariablesSesion.GrillaTransaccion);
            dgvTransacciones.DataSource = null;
            dgvTransacciones.DataBind();
            cmbTipoOperacion.DataSource = null;
            cmbTipoOperacion.DataBind();
            MImprimirMensaje("", EnumTiposMensajeAlerta.Limpiar);
            dgvTransacciones.Columns[(int)EnumColumnasGrilla.FoAbm].Visible = false;
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
            //if (tipoMensaje != EnumTiposMensajeAlerta.Correcto)
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError", "alert('" + strMensaje + "');", true);
            lblMensajeError.Text = "   " + strMensaje + "   ";

        }

        private void MArbolOrdenEntidad()
        {
            MLimpiaGrilla();
            decimal idUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
            //var lista2 = client.Get<EResultadoEntidad>(CAppSettings.svc_obtener_entidades + "/" + idUsuario + "?format=json");
            var lista2 = client.Post<EResultadoEntidad>(CAppSettings.svc_obtener_entidades,
                        new EObtenerEntidadesUsuario()
                        {
                            decFiltro = 1,
                            strParametro1 = idUsuario.ToString(),
                            strParametro2 = CAppSettings.IdActividades,
                            strParametro3 = CAppSettings.idAplicacion,
                            strParametro4 = "",
                            strParametro5 = ""
                        });
            if (lista2 != null)
            {
                if (lista2.IntCodigo == 1)
                {
                    List<O_OBTIENE_ENTIDADES_USR_CTY> lstEntidad;
                    lstEntidad = lista2.OResultado;
                    lstEntidad = lstEntidad.OrderBy(x => x.DENOMINACION_PADRE).ThenBy(x => x.ACTIVIDAD).ThenBy(x => x.DENOMINACION).ToList();

                    int idDe = 0, idAc = 0;
                    int idUnico = 1, idPadre = 0, idUnic1 = 0;
                    int intNivel1 = 0, intNivel2 = 0, intNivel3 = 0;
                    string strNivel1 = "", strNivel2 = "";
                    var lstResultado = new List<CArbol>();
                    foreach (var lst in lstEntidad)
                    {
                        CArbol cDato;
                        if (idDe != lst.ID_ENTIDAD_PADRE)
                        {
                            cDato = new CArbol();
                            cDato.IntId = idUnico;
                            cDato.IntIdPadre = 0;
                            cDato.IntIdEntidad = (int)lst.ID_ENTIDAD_PADRE;
                            cDato.IntIdActividad = 0;
                            cDato.IntIdOperador = 0;
                            cDato.IntIdProyecto = 0;
                            cDato.StrDescripcion = lst.DENOMINACION_PADRE.Trim();
                            cDato.StrDescPadre = lst.DENOMINACION_PADRE.Trim();
                            cDato.LngNit = Convert.ToInt64(lst.NIT);
                            cDato.StrLicencia = "";
                            cDato.StrRepLegal = lst.REPRESENTANTE_LEGAL;
                            cDato.StrDireccion = "";
                            cDato.StrDepartamento = "";
                            cDato.StrMunicipio = "";
                            cDato.StrLocalidad = "";
                            cDato.StrTelefono = "";
                            cDato.StrAmbito = "";
                            cDato.StrPropietario = "";
                            cDato.StrCiPropietario = "";
                            cDato.StrCiRepresentante = "";
                            cDato.IntAccion = 0;
                            cDato.StrUbicacion = intNivel1.ToString();
                            lstResultado.Add(cDato);
                            idPadre = idUnico;
                            idUnic1 = idUnico;
                            idDe = (int)lst.ID_ENTIDAD_PADRE;
                            idAc = 0;
                            idUnico++;
                            strNivel1 = cDato.StrUbicacion;
                            intNivel1++;
                            intNivel2 = 0;
                        }
                        else
                        {
                            //todo No implementado.
                        }
                        if (idAc != lst.ID_ACTIVIDAD)
                        {
                            cDato = new CArbol();
                            cDato.IntId = idUnico;
                            cDato.IntIdPadre = idUnic1;
                            cDato.IntIdEntidad = (int)lst.ID_ENTIDAD_PADRE;
                            cDato.IntIdOperador = 0;
                            cDato.IntIdProyecto = 0;
                            cDato.IntIdActividad = (int)lst.ID_ACTIVIDAD;
                            cDato.StrDescripcion = lst.ACTIVIDAD;
                            cDato.StrDescPadre = lst.DENOMINACION_PADRE;
                            cDato.LngNit = Convert.ToInt64(lst.NIT);
                            cDato.StrLicencia = "";
                            cDato.StrRepLegal = lst.REPRESENTANTE_LEGAL;
                            cDato.StrDireccion = "";
                            cDato.StrDepartamento = "";
                            cDato.StrMunicipio = "";
                            cDato.StrLocalidad = "";
                            cDato.StrTelefono = "";
                            cDato.StrAmbito = "";
                            cDato.StrPropietario = "";
                            cDato.StrCiPropietario = "";
                            cDato.StrCiRepresentante = "";
                            cDato.IntAccion = 0;
                            cDato.StrUbicacion = strNivel1 + intNivel2;
                            lstResultado.Add(cDato);
                            idPadre = idUnico;
                            idAc = (int)lst.ID_ACTIVIDAD;
                            idUnico++;
                            strNivel2 = cDato.StrUbicacion;
                            intNivel2++;
                            intNivel3 = 0;
                        }
                        else
                        {
                            //todo No implementado.
                        }
                        cDato = new CArbol();
                        cDato.IntId = idUnico;
                        cDato.IntIdPadre = idPadre;
                        cDato.IntIdEntidad = (int)lst.ID_ENTIDAD_PADRE;
                        cDato.IntIdOperador = (int)lst.ID_CONSUMIDOR;
                        cDato.IntIdProyecto = (int)lst.ID_ENTIDAD;
                        cDato.IntIdActividad = (int)lst.ID_ACTIVIDAD;
                        cDato.StrDescripcion = lst.DENOMINACION;
                        cDato.StrDescPadre = lst.DENOMINACION;
                        cDato.LngNit = Convert.ToInt64(lst.NIT);
                        cDato.StrLicencia = lst.NRO_LICENCIA;
                        cDato.StrRepLegal = lst.REPRESENTANTE_LEGAL;
                        cDato.StrDireccion = lst.DIRECCION;
                        cDato.StrDepartamento = lst.DEPARTAMENTO;
                        cDato.StrMunicipio = lst.MUNICIPIO;
                        cDato.StrLocalidad = lst.LOCALIDAD;
                        cDato.StrTelefono = lst.TELEFONOS;
                        cDato.StrAmbito = lst.AMBITO_OPERACION;
                        cDato.StrPropietario = lst.NOMBRES_PROPIETARIO;
                        cDato.StrCiPropietario = lst.CI_PROPIETARIO;
                        cDato.StrCiRepresentante = lst.CI_REPRESENTANTE;
                        cDato.IntAccion = 1;
                        cDato.StrUbicacion = strNivel2 + intNivel3;
                        lstResultado.Add(cDato);
                        idUnico++;
                        intNivel3++;
                    }
                    Session[CVariablesSesion.EstructuraArbol] = lstResultado;
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

        private void MArbolOrdenActividad()
        {
            decimal idUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
            //var lista2 = client.Get<EResultadoEntidad>(CAppSettings.svc_obtener_entidades + "/" + idUsuario + "?format=json");
            var lista2 = client.Post<EResultadoEntidad>(CAppSettings.svc_obtener_entidades,
                        new EObtenerEntidadesUsuario()
                        {
                            decFiltro = 1,
                            strParametro1 = idUsuario.ToString(),
                            strParametro2 = CAppSettings.IdActividades,
                            strParametro3 = CAppSettings.idAplicacion,
                            strParametro4 = "",
                            strParametro5 = ""
                        });
            if (lista2 != null)
            {
                if (lista2.IntCodigo == 1)
                {
                    List<O_OBTIENE_ENTIDADES_USR_CTY> lstEntidad;
                    lstEntidad = lista2.OResultado;
                    lstEntidad = lstEntidad.OrderBy(x => x.ACTIVIDAD).ThenBy(x => x.DEPARTAMENTO).ThenBy(x => x.DENOMINACION).ToList();

                    int idDe = 0;
                    string idAc = "";
                    int idUnico = 1, idPadre = 0, idUnic1 = 0;
                    int intNivel1 = 0, intNivel2 = 0, intNivel3 = 0;
                    string strNivel1 = "", strNivel2 = "";
                    var lstResultado = new List<CArbol>();
                    foreach (var lst in lstEntidad)
                    {
                        CArbol cDato;
                        if (idDe != lst.ID_ACTIVIDAD)
                        {
                            cDato = new CArbol();
                            cDato.IntId = idUnico;
                            cDato.IntIdPadre = 0;
                            cDato.IntIdEntidad = 0;
                            cDato.IntIdActividad = (int)lst.ID_ACTIVIDAD;
                            cDato.IntIdOperador = 0;
                            cDato.IntIdProyecto = 0;
                            cDato.StrDescripcion = lst.ACTIVIDAD.Trim();
                            cDato.StrDescPadre = lst.ACTIVIDAD.Trim();
                            cDato.LngNit = 0;
                            cDato.StrLicencia = "";
                            cDato.StrRepLegal = lst.REPRESENTANTE_LEGAL;
                            cDato.StrDireccion = "";
                            cDato.StrDepartamento = "";
                            cDato.StrMunicipio = "";
                            cDato.StrLocalidad = "";
                            cDato.StrTelefono = "";
                            cDato.StrAmbito = "";
                            cDato.StrPropietario = "";
                            cDato.StrCiPropietario = "";
                            cDato.StrCiRepresentante = "";
                            cDato.IntAccion = 0;
                            cDato.StrUbicacion = intNivel1.ToString();
                            lstResultado.Add(cDato);
                            idPadre = idUnico;
                            idUnic1 = idUnico;
                            idDe = (int)lst.ID_ACTIVIDAD;
                            idAc = "";
                            idUnico++;
                            strNivel1 = cDato.StrUbicacion;
                            intNivel1++;
                            intNivel2 = 0;
                        }
                        else
                        {
                            //todo No implementado.
                        }
                        if (idAc != lst.DEPARTAMENTO)
                        {
                            cDato = new CArbol();
                            cDato.IntId = idUnico;
                            cDato.IntIdPadre = idUnic1;
                            cDato.IntIdEntidad = 0;
                            cDato.IntIdOperador = 0;
                            cDato.IntIdProyecto = 0;
                            cDato.IntIdActividad = (int)lst.ID_ACTIVIDAD;
                            cDato.StrDescripcion = lst.DEPARTAMENTO;
                            cDato.StrDescPadre = lst.ACTIVIDAD;
                            cDato.LngNit = 0;
                            cDato.StrLicencia = "";
                            cDato.StrRepLegal = "";
                            cDato.StrDireccion = "";
                            cDato.StrDepartamento = lst.DEPARTAMENTO;
                            cDato.StrMunicipio = "";
                            cDato.StrLocalidad = "";
                            cDato.StrTelefono = "";
                            cDato.StrAmbito = "";
                            cDato.StrPropietario = "";
                            cDato.StrCiPropietario = "";
                            cDato.StrCiRepresentante = "";
                            cDato.IntAccion = 0;
                            cDato.StrUbicacion = strNivel1 + intNivel2;
                            lstResultado.Add(cDato);
                            idPadre = idUnico;
                            idAc = lst.DEPARTAMENTO;
                            idUnico++;
                            strNivel2 = cDato.StrUbicacion;
                            intNivel2++;
                            intNivel3 = 0;
                        }
                        else
                        {
                            //todo No implementado.
                        }
                        cDato = new CArbol();
                        cDato.IntId = idUnico;
                        cDato.IntIdPadre = idPadre;
                        cDato.IntIdEntidad = (int)lst.ID_ENTIDAD_PADRE;
                        cDato.IntIdOperador = (int)lst.ID_CONSUMIDOR;
                        cDato.IntIdProyecto = (int)lst.ID_ENTIDAD;
                        cDato.IntIdActividad = (int)lst.ID_ACTIVIDAD;
                        cDato.StrDescripcion = lst.DENOMINACION;
                        cDato.StrDescPadre = lst.DENOMINACION;
                        cDato.LngNit = Convert.ToInt64(lst.NIT);
                        cDato.StrActividad = lst.ACTIVIDAD;
                        cDato.StrLicencia = lst.NRO_LICENCIA;
                        cDato.StrRepLegal = lst.REPRESENTANTE_LEGAL;
                        cDato.StrDireccion = lst.DIRECCION;
                        cDato.StrDepartamento = lst.DEPARTAMENTO;
                        cDato.StrMunicipio = lst.MUNICIPIO;
                        cDato.StrLocalidad = lst.LOCALIDAD;
                        cDato.StrTelefono = lst.TELEFONOS;
                        cDato.StrAmbito = lst.AMBITO_OPERACION;
                        cDato.StrPropietario = lst.NOMBRES_PROPIETARIO;
                        cDato.StrCiPropietario = lst.CI_PROPIETARIO;
                        cDato.StrCiRepresentante = lst.CI_REPRESENTANTE;
                        cDato.IntAccion = 1;
                        cDato.StrUbicacion = strNivel2 + intNivel3;
                        lstResultado.Add(cDato);
                        idUnico++;
                        intNivel3++;
                    }
                    Session[CVariablesSesion.EstructuraArbol] = lstResultado;
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

        #endregion

    }
}