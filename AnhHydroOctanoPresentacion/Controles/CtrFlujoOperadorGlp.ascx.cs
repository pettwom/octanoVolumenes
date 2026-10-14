using AnhHydroOctanoAgenteServicio.ServicioExternoSegip;
using AnhHydroOctanoPersistencia.Aplicacion.Consulta;
using AnhHydroOctanoPersistencia.Entidades;
using AnhHydroOctanoPresentacion.Entidades;
using AnhHydroOctanoPresentacion.Enumeradores;
using AnhHydroOctanoPresentacion.Parametros;
using Librerias.Anh.Us;
using ServiceStack.ServiceClient.Web;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AnhHydroOctanoPresentacion.Controles
{
    public partial class CtrFlujoOperadorGlp : System.Web.UI.UserControl
    {
        readonly JsonServiceClient _client = new JsonServiceClient(CAppSettings.servicio_octVolumen);
        private readonly CPersistenciaConsulta _consulta = new CPersistenciaConsulta();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    if (!Page.IsCallback && !Page.IsPostBack)
                    {

                        txtFechaAsignacionCamion.Text = DateTime.Now.ToString();

                        var objArbol = Session[CVariablesSesion.ObjetoArbol] as List<O_OBTIENE_ENTIDADES_CTY>;
                        var objResultado = _consulta.ObtenerEntidades(
                        new EObjetoEnvio5ParametrosProto
                        {
                            decFiltro = 1,
                            strParametro1 = Session[CVariablesSesion.IdUsuario].ToString(),
                            strParametro2 = "29",
                            strParametro3 = CAppSettings.idAplicacion,
                            strParametro4 = "",
                            strParametro5 = ""
                        });
                        if (objResultado.intCodigo == 1)
                        {
                            objArbol = (List<O_OBTIENE_ENTIDADES_CTY>)objResultado.oResultado;
                            
                            //Session.Remove(CVariablesSesion.ObjetoArbol);
                            //Session[CVariablesSesion.ObjetoArbol] = objArbol;

                            cmbEntidad.DataSource = objArbol;
                            cmbEntidad.DataBind();

                            cmbPlanta.DataSource = objArbol;
                            cmbPlanta.DataBind();
                        }
                        else
                        {
                            //MImprimirMensaje(objResultado.strMensaje, EnumTiposMensajeAlerta.Error);
                            return;
                        }
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

        protected void pnlDatosVentaGlp_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    decimal accion = Convert.ToDecimal(e.Parameter);

                    MImprimirMensaje("", EnumTiposMensajeAlerta.Limpiar);

                    if (accion == 1)//buscar persona
                    {
                        if (txtDocumentoIdentidadGlp.Text != "")
                        {
                            var objResultado = _consulta.ObtenerDatoPersonaHydro(txtDocumentoIdentidadGlp.Text);

                            if (objResultado.intCodigo == 1)
                            {                               

                                var list = (List<O_OBTIENE_DATO_PERSONA_CTY>)objResultado.oResultado;

                                var strNombre = list[0].NOMBRE.ToString();
                                txtNombres_Glp.Text = strNombre;

                                var strApellido1 = list[0].APELLIDO_1.ToString();
                                txtApellidoPat_Glp.Text = strApellido1;

                                var strApellido2 = list[0].APELLIDO_2.ToString();
                                txtApellidoMaterno_Glp.Text = strApellido2;

                            }
                            else
                            {
                                Session.Remove(CVariablesSesion.Nombres);
                                Session.Remove(CVariablesSesion.PrimerApellido);
                                Session.Remove(CVariablesSesion.SegundoApellido);
                                Session.Remove(CVariablesSesion.Complemento);
                                Session.Remove(CVariablesSesion.NumeroDocumento);
                                Session.Remove(CVariablesSesion.FechaNacimiento);
                                
                                string strCi = txtDocumentoIdentidadGlp.Text;
                                string strComplemento = "";

                                ServicioExternoInstitucionClient client = new ServicioExternoInstitucionClient();
                                var a = client.ConsultaDatoPersonaEnJson(110, "einard.joffre", "9rvbnNLdVZ", "E132424285382", null, strCi, strComplemento,
                                                                 null, null, null, null);

                                if (a.EsValido)
                                {
                                    if (a.DatosPersonaEnFormatoJson != "null")
                                    {
                                        //string strBase64 = Convert.ToBase64String(a.Fotografia, 0, a.Fotografia.Length);
                                        //Image1.ImageUrl = "data:image/png;base64," + strBase64;
                                        JavaScriptSerializer serializer = new JavaScriptSerializer();
                                        dynamic item = serializer.Deserialize<object>(a.DatosPersonaEnFormatoJson);

                                        if (item["ComplementoVisible"] == "0")
                                        {
                                            item["Complemento"] = " ";
                                        }

                                        txtNombres_Glp.Text = item["Nombres"].ToString();
                                        txtApellidoPat_Glp.Text = item["PrimerApellido"].ToString();
                                        txtApellidoMaterno_Glp.Text = item["SegundoApellido"].ToString();
                                        //txtDocIdentidadPersonaGlp.Text = item["NumeroDocumento"].ToString();
                                        //txtFechaNacimientoGlp.Text = item["FechaNacimiento"];

                                        Session[CVariablesSesion.Nombres] = item["Nombres"].ToString();
                                        Session[CVariablesSesion.PrimerApellido] = item["PrimerApellido"].ToString();
                                        Session[CVariablesSesion.SegundoApellido] = item["SegundoApellido"].ToString();
                                        Session[CVariablesSesion.Complemento] = item["Complemento"].ToString();
                                        Session[CVariablesSesion.NumeroDocumento] = item["NumeroDocumento"].ToString();
                                        Session[CVariablesSesion.FechaNacimiento] = item["FechaNacimiento"];

                                    }
                                    else
                                    {
                                        txtDocumentoIdentidadGlp.Text = "0";
                                        txtNombres_Glp.Text = "";
                                        txtApellidoPat_Glp.Text = "";
                                        txtApellidoMaterno_Glp.Text = "";
                                        txtCantidad_Glp.Text = "";
                                        txtPrecioTotalGlp.Text = "";

                                        MImprimirMensajeGrabar("Persona no registrada SEGIP", EnumTiposMensajeAlerta.Error);
                                    }
                                }
                                else
                                {
                                    txtDocumentoIdentidadGlp.Text = "0";
                                    txtNombres_Glp.Text = "";
                                    txtApellidoPat_Glp.Text = "";
                                    txtApellidoMaterno_Glp.Text = "";
                                    txtCantidad_Glp.Text = "";
                                    txtPrecioTotalGlp.Text = "";

                                    MImprimirMensajeGrabar("Persona no registrada SEGIP", EnumTiposMensajeAlerta.Error);
                                }

                            }
                        }
                        else
                        {
                            MImprimirMensajeGrabar("Introduzca un CI para buscar", EnumTiposMensajeAlerta.Error);
                        }
                        //pnlConsumoGlp_Callback(null, null);
                    }
                    else if (accion == 2) //validar y grabar
                    {
                        decimal myDec2;
                        var isNumber2 = decimal.TryParse(txtCantidad_Glp.Text, out myDec2);

                        if (cmbEntidad.Text == "")
                        {
                            MImprimirMensajeGrabar("Debe seleccionar una planta", EnumTiposMensajeAlerta.Alerta);
                        }
                        else if (txtDocumentoIdentidadGlp.Text == "" && rbBuscarConCi.Checked == true)
                        {
                            MImprimirMensajeGrabar("Introduzca un CI para buscar", EnumTiposMensajeAlerta.Alerta);
                        }
                        else if (txtCantidad_Glp.Text == "0")
                        {
                            MImprimirMensajeGrabar("Debe introducir una cantidad de garrafas a comercializar", EnumTiposMensajeAlerta.Alerta);
                        }
                        else if (!isNumber2)
                        {
                            MImprimirMensajeGrabar("La Cantidad debe ser numerico", EnumTiposMensajeAlerta.Alerta);
                        }
                        else if (Convert.ToDecimal(txtCantidad_Glp.Text) >= 100)
                        {
                            MImprimirMensajeGrabar("La Cantidad debe ser menor a 100", EnumTiposMensajeAlerta.Alerta);
                        }
                        else
                        {
                            var datFechaDoc = DateTime.Now;
                            var datFechaReg = DateTime.Now;

                            string strFecha = datFechaDoc.Year.ToString() + datFechaDoc.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaDoc.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0');

                            var objNuevoReg = new ObjMovimientoTransaccion();

                            objNuevoReg.idMovimiento = 0;
                            objNuevoReg.idMovimientoPadre = 0;
                            objNuevoReg.idTipoActividad = 125; //produccion 124 -- test 125  -- desarrollo 125
                            objNuevoReg.idTipoOperacion = -99999;
                            objNuevoReg.idConsumidor = Convert.ToDecimal(cmbEntidad.Value);
                            objNuevoReg.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                            objNuevoReg.fechaRegistroMovimiento = Convert.ToDecimal(strFecha + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                            objNuevoReg.fechaOperacion = Convert.ToDecimal(strFecha + datFechaDoc.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaDoc.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaDoc.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                            objNuevoReg.idTipoMovimientoFlujo = 20;
                            objNuevoReg.idTipoMovimiento = 18;
                            objNuevoReg.idEstadoMovimiento = 11;
                            objNuevoReg.idFuenteOrigenDato = 15;
                            objNuevoReg.idUbicacionOperacion = 120;
                            objNuevoReg.idUnidadMedidaHidrocarburo = 107;//produccion 105  -- test 107 -- desarrollo 107
                            objNuevoReg.idHidrocarburo = 125;
                            objNuevoReg.cantidadHidrocarburo = Convert.ToDecimal(txtCantidad_Glp.Text);
                            objNuevoReg.identificador = txtPlacaVenta.Text;
                            objNuevoReg.subIdentificador = "";
                            objNuevoReg.observacion = "";
                            objNuevoReg.barcode = 0;
                            objNuevoReg.idTipoMedicion = -99999;
                            objNuevoReg.cantidadAguaLitros = 0;
                            objNuevoReg.idTipoMedioTransporte = -99999;
                            objNuevoReg.idMedioTransporte = -99999;
                            objNuevoReg.tipoInfraestructura = -99999;
                            objNuevoReg.idInfraestructura = -99999;
                            objNuevoReg.idUnidadEnvasesTransporte = 6;
                            objNuevoReg.codigo = "";
                            objNuevoReg.idIntegracion = 0;
                            objNuevoReg.idUnidadTemperatura = -99999;
                            objNuevoReg.temperatura = 0;
                            objNuevoReg.gradoApi = "";
                            objNuevoReg.idEntidad = -99999;
                            objNuevoReg.cantidadEnvasesTransporte = 0;
                            objNuevoReg.binarioControl = "00000-0000000000-0000000000-00000";
                            objNuevoReg.idTransaccionBaseExterna = "";
                            objNuevoReg.idTipoConsumidor = 0;
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
                                objTrasaccion.fechaRegistroTransaccion = Convert.ToDecimal(strFecha + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                objTrasaccion.idTipoTransaccion = 3;
                                objTrasaccion.fechaEmision = Convert.ToDecimal(strFecha + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                objTrasaccion.numeroTransaccion = "0";
                                objTrasaccion.numeroAutorizacion = 0;
                                objTrasaccion.codigoControl = "";
                                objTrasaccion.nitConsumidor = Convert.ToDecimal(txtDocumentoIdentidadGlp.Text);
                                objTrasaccion.razonSocial = txtApellidoPat_Glp.Text;
                                objTrasaccion.precioUnitario = Convert.ToDecimal(txtPrecioUnitariolGlp.Text);
                                objTrasaccion.precioTotal = Convert.ToDecimal(txtPrecioTotalGlp.Text);
                                objTrasaccion.usuarioTransaccion = "";
                                objTrasaccion.observaciones = "";
                                objTrasaccion.nitEmisor = Convert.ToDecimal(0);
                                objTrasaccion.idEstadoTransaccion = 11;
                                objTrasaccion.idTipoCreditoFiscal = 10;
                                objTrasaccion.idFormaPago = 1;
                                objTrasaccion.idTipoMoneda = 1;
                                objTrasaccion.idTipoEmisor = 14;
                                objTrasaccion.codigoSeguridad = 0;
                                objTrasaccion.idAutorizacionDato = 0;
                                objTrasaccion.binarioControl = "1000000000";
                                objTrasaccion.fechaOperacion = Convert.ToDecimal(strFecha + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                objTrasaccion.idActividad = 125;//produccion 124 -- test 125  -- desarrollo 125
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
                            if (resultado.intCodigo == 1)
                            {
                                //e.Cancel = true;
                                Session.Remove(CVariablesSesion.AuxActualiza);
                                //dgvTransacciones.CancelEdit();
                                //dgvTransacciones_CustomCallback(null, null);
                                txtNombres_Glp.Text = "";
                                txtApellidoPat_Glp.Text = "";
                                txtApellidoMaterno_Glp.Text = "";
                                txtCantidad_Glp.Text = "0";
                                txtDocumentoIdentidadGlp.Text = "";
                                MImprimirMensajeGrabar("Registro Guardado", EnumTiposMensajeAlerta.Correcto);
                                //pnlConsumoGlp_Callback(null, null);

                                //if (Session[CVariablesSesion.Nombres] != null)
                                //{
                                //    var strNombreSegip = Session[CVariablesSesion.Nombres] as string;
                                //    var strPrimerApellidoSegip = Session[CVariablesSesion.PrimerApellido] as string;
                                //    var strSegundoApellidoSegip = Session[CVariablesSesion.SegundoApellido] as string;
                                //    var strComplemento = Session[CVariablesSesion.Complemento] as string;
                                //    var decComplementoSegip = Convert.ToDecimal(strComplemento.Trim() == "" ? "0" : strComplemento.Trim());
                                //    var decNumeroDocumentoSegip = Convert.ToDecimal(Session[CVariablesSesion.NumeroDocumento] as string);
                                //    var dateFechaNacimientoSegip = Session[CVariablesSesion.FechaNacimiento] as string;

                                //    var fecha = Convert.ToDecimal(dateFechaNacimientoSegip.Substring(6, 4) + dateFechaNacimientoSegip.Substring(3, 2) + dateFechaNacimientoSegip.Substring(0, 2));

                                //    var decGeneroSegip = -99999;

                                //    //var objResultado = _gestion.RegistraPersonaHydro(strNombreSegip, strPrimerApellidoSegip, strSegundoApellidoSegip, fecha, decNumeroDocumentoSegip, 1, decComplementoSegip, "", 1, decGeneroSegip);

                                //}

                            }
                            else
                            {
                                MImprimirMensajeGrabar(resultado.strMensaje, EnumTiposMensajeAlerta.Alerta);
                            }
                        }
                        
                    }
                    else
                    {
                        MImprimirMensajeGrabar("", EnumTiposMensajeAlerta.Limpiar);
                    }

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
        
        protected void pnlConsumoGlp_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    var objResultado = _consulta.ReporteMovimientoConsumoUsuario(
                    new EObjetoEnvio1ParametrosProto
                    {
                        strCredencial = "",
                        decFiltro = 1,
                        strParametro1 = txtDocumentoIdentidadGlp.Text
                    });
                    if (objResultado.intCodigo == 1)
                    {
                        var lstDato = (List<O_REPORTE_MOV_CONSUMO_CTY>)objResultado.oResultado;

                        pvgDato.DataSource = lstDato;
                        pvgDato.DataBind();
                        txtConsumoUsuario.Text = lstDato.Count().ToString();
                        Session[CVariablesSesion.GrillaCabecera] = lstDato;
                    }
                    else
                    {
                        pvgDato.DataSource = null;
                        pvgDato.DataBind();
                        //MImprimirMensaje(objResultado.strMensaje, EnumTiposMensajeAlerta.Alerta);
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

        protected void pnlDatosAsignacionCamion_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    decimal accion = Convert.ToDecimal(e.Parameter);

                    MImprimirMensaje("", EnumTiposMensajeAlerta.Limpiar);

                    if (accion == 1) //validar datos y grabar si esta todo bien
                    {
                        decimal myDec;
                        var isNumber = decimal.TryParse(txtCantidadCamionAsignacion.Text, out myDec);

                        if (cmbPlanta.Value == "")
                        {
                            MImprimirMensaje("Debe seleccionar una planta", EnumTiposMensajeAlerta.Alerta);
                        }
                        else if(txtPlacaCamionAsignacion.Text == "")
                        {
                            MImprimirMensaje("Debe introducir una placa", EnumTiposMensajeAlerta.Alerta);
                        }
                        else if (txtCantidadCamionAsignacion.Text == "")
                        {
                            MImprimirMensaje("Debe introducir una cantidad", EnumTiposMensajeAlerta.Alerta);
                        }
                        else if (!isNumber)
                        {
                            MImprimirMensaje("La Cantidad debe ser numerico", EnumTiposMensajeAlerta.Alerta);
                        }
                        else if (Convert.ToDecimal(txtCantidadCamionAsignacion.Text) >= 1001)
                        {
                            MImprimirMensaje("La Cantidad debe ser menor o igual a 1000", EnumTiposMensajeAlerta.Alerta);
                        }
                        else
                        {
                            #region grabar moviminento

                                var datFechaDoc = DateTime.Now;
                                var datFechaReg = DateTime.Now;

                                string strFecha = datFechaDoc.Year.ToString() + datFechaDoc.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaDoc.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0');

                                var objNuevoReg = new ObjMovimientoTransaccion();

                                objNuevoReg.idMovimiento = 0;
                                objNuevoReg.idMovimientoPadre = 0;
                                objNuevoReg.idTipoActividad = 29;
                                objNuevoReg.idTipoOperacion = 235;//produccion 133 -- TEST 235  -- desarrollo 235
                                objNuevoReg.idConsumidor = Convert.ToDecimal(cmbPlanta.Value);
                                objNuevoReg.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                objNuevoReg.fechaRegistroMovimiento = Convert.ToDecimal(strFecha + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                objNuevoReg.fechaOperacion = Convert.ToDecimal(strFecha + datFechaDoc.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaDoc.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaDoc.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                objNuevoReg.idTipoMovimientoFlujo = 20;
                                objNuevoReg.idTipoMovimiento = 18;
                                objNuevoReg.idEstadoMovimiento = 11;
                                objNuevoReg.idFuenteOrigenDato = 15;
                                objNuevoReg.idUbicacionOperacion = 120;
                                objNuevoReg.idUnidadMedidaHidrocarburo = 107;//produccion 105  -- test 107 -- desarrollo 107
                                objNuevoReg.idHidrocarburo = 125;
                                objNuevoReg.cantidadHidrocarburo = Convert.ToDecimal(txtCantidadCamionAsignacion.Text);
                                objNuevoReg.identificador = txtPlacaCamionAsignacion.Text;
                                objNuevoReg.subIdentificador = "";
                                objNuevoReg.observacion = "";
                                objNuevoReg.barcode = 0;
                                objNuevoReg.idTipoMedicion = -99999;
                                objNuevoReg.cantidadAguaLitros = 0;
                                objNuevoReg.idTipoMedioTransporte = -99999;
                                objNuevoReg.idMedioTransporte = -99999;
                                objNuevoReg.tipoInfraestructura = -99999;
                                objNuevoReg.idInfraestructura = -99999;
                                objNuevoReg.idUnidadEnvasesTransporte = 6;
                                objNuevoReg.codigo = "";
                                objNuevoReg.idIntegracion = 0;
                                objNuevoReg.idUnidadTemperatura = -99999;
                                objNuevoReg.temperatura = 0;
                                objNuevoReg.gradoApi = "";
                                objNuevoReg.idEntidad = -99999;
                                objNuevoReg.cantidadEnvasesTransporte = 0;
                                objNuevoReg.binarioControl = "00000-0000000000-0000000000-00000";
                                objNuevoReg.idTransaccionBaseExterna = "";
                                objNuevoReg.idTipoConsumidor = 0;
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
                                objTrasaccion.fechaRegistroTransaccion = Convert.ToDecimal(strFecha + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                objTrasaccion.idTipoTransaccion = 3;
                                objTrasaccion.fechaEmision = Convert.ToDecimal(strFecha + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                objTrasaccion.numeroTransaccion = "0";
                                objTrasaccion.numeroAutorizacion = 0;
                                objTrasaccion.codigoControl = "";
                                objTrasaccion.nitConsumidor = 0;
                                objTrasaccion.razonSocial = "";
                                objTrasaccion.precioUnitario = Convert.ToDecimal("22.50");
                                objTrasaccion.precioTotal = Convert.ToDecimal(txtCantidadCamionAsignacion.Text) * Convert.ToDecimal("22.50");
                                objTrasaccion.usuarioTransaccion = "";
                                objTrasaccion.observaciones = "";
                                objTrasaccion.nitEmisor = Convert.ToDecimal(0);
                                objTrasaccion.idEstadoTransaccion = 11;
                                objTrasaccion.idTipoCreditoFiscal = 10;
                                objTrasaccion.idFormaPago = 1;
                                objTrasaccion.idTipoMoneda = 1;
                                objTrasaccion.idTipoEmisor = 14;
                                objTrasaccion.codigoSeguridad = 0;
                                objTrasaccion.idAutorizacionDato = 0;
                                objTrasaccion.binarioControl = "1000000000";
                                objTrasaccion.fechaOperacion = Convert.ToDecimal(strFecha + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                objTrasaccion.idActividad = 29;
                                objTrasaccion.codigo = "";

                                objNuevoReg.listaTransaccion.Add(objTrasaccion);
                                var objRegistro = new ERegistraMovimientoTransaccion();
                                objRegistro.strCredencial = CParametrosHydro.strCredencialFuncionario;
                                objRegistro.decFiltro = 1;
                                var lstRegistros = new List<ObjMovimientoTransaccion>();
                                lstRegistros.Add(objNuevoReg);
                                objRegistro.listaMovimientoTransaccion = lstRegistros;

                                var resultado = _client.Post<EResultado>(CAppSettings.svc_registra_movimiento_transaccion, objRegistro);
                                if (resultado.intCodigo == 1)
                                {
                                    Session.Remove(CVariablesSesion.AuxActualiza);

                                    txtPlacaCamionAsignacion.Text = "";
                                    txtCantidadCamionAsignacion.Text = "";

                                    MImprimirMensaje(resultado.strMensaje, EnumTiposMensajeAlerta.Correcto);

                                }
                                else
                                {
                                    MImprimirMensaje(resultado.strMensaje, EnumTiposMensajeAlerta.Alerta);
                                }

                            #endregion
                        }
                    }
                    else
                    {
                        MImprimirMensaje("", EnumTiposMensajeAlerta.Limpiar);
                    }

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

        private void MImprimirMensajeGrabar(string strMensaje, EnumTiposMensajeAlerta tipoMensaje)
        {
            switch (tipoMensaje)
            {
                case EnumTiposMensajeAlerta.Error:
                    pnlMensajeGrabar.Visible = true;
                    pnlMensajeGrabar.BorderColor = Color.DarkRed;
                    pnlMensajeGrabar.BackColor = Color.Pink;
                    strMensaje = "ERROR: " + strMensaje;
                    break;
                case EnumTiposMensajeAlerta.Alerta:
                    pnlMensajeGrabar.Visible = true;
                    pnlMensajeGrabar.BorderColor = Color.OrangeRed;
                    pnlMensajeGrabar.BackColor = Color.LightSalmon;
                    strMensaje = "  ALERTA: " + strMensaje;
                    break;
                case EnumTiposMensajeAlerta.Correcto:
                    pnlMensajeGrabar.Visible = true;
                    pnlMensajeGrabar.BorderColor = Color.DarkGreen;
                    pnlMensajeGrabar.BackColor = Color.DarkSeaGreen;
                    strMensaje = "  CORRECTO: " + strMensaje;
                    break;
                case EnumTiposMensajeAlerta.Limpiar:
                    pnlMensajeGrabar.Visible = false;
                    lblMensajeErrorGrabar.ForeColor = Color.WhiteSmoke;
                    strMensaje = "-";
                    break;
                default:
                    pnlMensajeGrabar.Visible = false;
                    lblMensajeErrorGrabar.ForeColor = Color.WhiteSmoke;
                    strMensaje = "-";
                    break;
            }
            lblMensajeErrorGrabar.ForeColor = Color.DimGray;
            //if (tipoMensaje != EnumTiposMensajeAlerta.Correcto)
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError", "alert('" + strMensaje + "');", true);
            lblMensajeErrorGrabar.Text = "   " + strMensaje + "   ";
        }
    }
}