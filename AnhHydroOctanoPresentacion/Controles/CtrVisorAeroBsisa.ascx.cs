
using System.Collections.Specialized;
using System.Net;
using System.Web.Script.Serialization;
using ServiceStack.Common.Extensions;
using ServiceStack.ServiceClient.Web;

namespace AnhHydroOctanoPresentacion.Controles
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Linq;
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
    using JsonSerializer = ServiceStack.Text.JsonSerializer;
    using Page = System.Web.UI.Page;

    public partial class CtrVisorAeroBsisa : UserControl
    {
        #region Variables de entorno

        private readonly CPersistenciaConsulta _consulta = new CPersistenciaConsulta();
        readonly CUtil _cUtil = new CUtil();

        readonly JsonServiceClient _extInteg = new JsonServiceClient(CAppSettings.servicio_octIntegracion);
        readonly JsonServiceClient _extAgent = new JsonServiceClient(CAppSettings.servicio_octAgente);
        readonly JsonServiceClient _srvMtr = new JsonServiceClient(CAppSettings.servicio_Motorizados);
        readonly JsonServiceClient _client = new JsonServiceClient(CAppSettings.servicio_octVolumen);

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
                        Session.Remove(CVariablesSesion.ComboGenerico);

                        Session.Remove(CVariablesSesion.ComboTipoOperacion);
                        Session.Remove(CVariablesSesion.ComboTiposOperacion);
                        Session.Remove(CVariablesSesion.ListaDepartamentos);

                        MCargaDatosIniciales();
                    }
                    else
                    {
                        //todo No implementado.
                    }


                    if (cmbOrigen.DataSource == null && Session[CVariablesSesion.ComboGenerico] != null)
                    {
                        cmbOrigen.DataSource = Session[CVariablesSesion.ComboGenerico];
                        cmbOrigen.DataBind();

                        cmbDestino.DataSource = Session[CVariablesSesion.ComboGenerico];
                        cmbDestino.DataBind();

                        cmbAlterno.DataSource = Session[CVariablesSesion.ComboGenerico];
                        cmbAlterno.DataBind();
                    }

                    if (cmbRegClase.DataSource == null && Session[CVariablesSesion.ComboTipoOperacion] != null)
                    {
                        cmbRegClase.DataSource = Session[CVariablesSesion.ComboTipoOperacion];
                        cmbRegClase.DataBind();
                    }
                    if (cmbRegMarca.DataSource == null && Session[CVariablesSesion.ComboTiposOperacion] != null)
                    {
                        cmbRegMarca.DataSource = Session[CVariablesSesion.ComboTiposOperacion];
                        cmbRegMarca.DataBind();
                    }
                    if (cmbRegPais.DataSource == null && Session[CVariablesSesion.ListaDepartamentos] != null)
                    {
                        cmbRegPais.DataSource = Session[CVariablesSesion.ListaDepartamentos];
                        cmbRegPais.DataBind();
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

                    if(!MValidaDatos())
                        return;

                    //string credidentials = CParametrosHydro.StrTokenYpfbAbp;
                    _extAgent.Headers.Add("Authorization", CParametrosHydro.StrTokenYpfbAbp);
                    //_extAgent
                    var objRes = _extAgent.Post<ResultadoConsumidor>(CAppSettings.svc_ObtenerConsumidor,
                    new
                    {
                        citeInfraestructura = txtCite.Text,
                        idTipoConsulta = rbtTipo.Value,
                        identificador = txtIdentificador.Text,
                        subIdentificador = txtSubIdentificador.Text,//1: parametrica | 2: integracion
                        //idProducto = rbtProducto.Value,
                        nroVuelo = txtNoVuelo.Text,
                        origen = cmbOrigen.Value,
                        destino = cmbDestino.Value,
                        alterno = cmbAlterno.Value
                    });
                    if (objRes.decCodigo == 1)
                    {
                        var objConsum = objRes.oResultado;

                        //if (objConsum.estado == 2)
                        //{
                        //    lblEstado.Text = "2";
                        //    lblMensaje.Text = objConsum.mensaje[0].mensaje;
                        //}

                        if (objConsum.imagen != null && objConsum.imagen.Count > 0 &&
                            objConsum.imagen.FirstNonDefault().IMAGEN != null)
                        {
                            imgFotos.DataSource = objConsum.imagen;
                            imgFotos.DataBind();
                            Session[CVariablesSesion.ListaInfraestructura] = objConsum.imagen;
                        }
                        else
                        {
                            imgFotos.DataSource = null;
                            imgFotos.DataBind();
                            Session.Remove(CVariablesSesion.ListaInfraestructura);
                        }

                        lblEstado.Text = objConsum.estado.ToString();
                        if (lblEstado.Text == "1")
                        {
                            lblEstado.ForeColor = Color.DarkGreen;
                            lblMensaje.ForeColor = Color.DarkGreen;
                        }
                        else
                        {
                            lblEstado.ForeColor = Color.DarkRed;
                            lblMensaje.ForeColor = Color.DarkRed;
                        }

                        if (objConsum.mensaje != null)
                        {
                            lblMensaje.Text = objConsum.mensaje.FirstOrDefault().mensaje;
                        }
                        else
                        {
                            lblEstado.Text = "2";
                            lblMensaje.Text = "INHABILITADO.";
                        }

                        lblExplotador.Text = objConsum.explotador;
                        lblModelo.Text = objConsum.modelo;
                        lblMarca.Text = objConsum.marca;
                        lblIdTransaccion.Text = objConsum.idTransaccion;

                        //var g = new oProducto();

                        
                        if (objConsum.producto != null)
                        {
                            //list = list.Where(w => w.Name == "height").Select(s => { s.Value = 30; return s; }).ToList();
                            //objConsum.producto.Where(a => a.idProducto == 129).Select(s => { s.precioInt = (decimal)8.75; return s; }).ToList();
                            //list.Where(w => w.Name == "height").ToList().ForEach(s => s.Value = 30);
                            //objConsum.producto.Where(a => a.idProducto == 129).ToList().ForEach(s => s.precioInt = (decimal)8.75);
                            //objConsum.producto.ForEach(a => a.strProducto = (a.idProducto == 129) ? "JET FUEL A1" : "GASOLINA DE AVIACION GRADO 100");                            
                            objConsum.producto.ForEach(a => a.strProducto = (a.idProducto == 129) ? "JET FUEL A1" : "GASOLINA DE AVIACION GRADO 100");

                            cmbProducto.DataSource = objConsum.producto;
                            cmbProducto.DataBind();
 
                            cmbProducto.SelectedIndex = 0;

                            var xx = cmbProducto.Text.Split(';');
                            lblPrecioNac.Text = xx[1].Trim();
                            lblPrecioInt.Text = xx[2].Trim();//"8.75";//xx[2].Trim();  //"7.32"

                            //rbtProducto.Value = (Int32)objConsum.producto.idProducto;
                            //lblPrecioNac.Text = objConsum.producto.precioNac.ToString();
                            //lblPrecioInt.Text = objConsum.producto.precioInt.ToString();
                            //lblVolumenMax.Text = objConsum.producto.oConsumo != null ? objConsum.producto.oConsumo.volMaximo.ToString() : "0";
                        }
                        else
                        {
                            cmbProducto.DataSource = null;
                            cmbProducto.DataBind();

                            //rbtProducto.Value = 0;
                            lblPrecioNac.Text = "0.0";
                            lblPrecioInt.Text = "0.0";
                        }




                    }
                    else
                    {
                        MImprimirMensaje(objRes.strMensaje, EnumTiposMensajeAlerta.Error);
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
                //todo no implementado
            }
        }
        
        protected void imgFotos_CustomCallback(object sender, CallbackEventArgsBase e)
        {
            try
            {
                #region Proceso a Ejecutar
                imgFotos.DataSource = null;
                imgFotos.DataBind();
                Session.Remove(CVariablesSesion.ListaInfraestructura);
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

        protected void pucDatos_WindowCallback(object source, PopupWindowCallbackArgs e)
        {
            //todo agregar detalle
            //var xx = e.Parameter;
        }

        protected void btnGrabar_Click(object sender, EventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    //Validaciones
                    if (txtRegMatricula.Text.Trim().ToUpper().StartsWith("FAB") || txtRegMatricula.Text.Trim().ToUpper().StartsWith("CP") ||
                        txtRegMatricula.Text.Trim().ToUpper().StartsWith("EB") || txtRegMatricula.Text.Trim().ToUpper().StartsWith("AB") ||
                        txtRegMatricula.Text.Trim().ToUpper().StartsWith("PB"))
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError", "alert('LA MATRICULA NO ES INTERNACIONAL.');", true);
                        return;
                    }

                    var objRes = _extInteg.Post<EResultadoI>(CAppSettings.svc_GestionAeronaveExtranjera,
                    new
                    {
                        strCredencial = CParametrosHydro.StrCredencialExterno,
                        decFiltro = 1, //1:ALTA
                        objRegistro = new O_OBTENER_AERONAVE_EXTR_CTY
                        {
                            ID_AERONAVE_EXTRANJERA = 0,
                            PROPIETARIO = txtRegPropietario.Text.Trim().ToUpper(),
                            PROPIETARIO_DOC = txtRegNroIdentificacion.Text.Trim().ToUpper(),
                            ID_PAIS = Convert.ToDecimal(cmbRegPais.Value),
                            MATRICULA = txtRegMatricula.Text.Trim().ToUpper(),
                            ID_CLASE = Convert.ToDecimal(cmbRegClase.Value),
                            ID_MARCA = Convert.ToDecimal(cmbRegMarca.Value),
                            ID_TIPO_SERVICIO = Convert.ToDecimal(cmbRegServicio.Value),
                            MODELO = txtRegModelo.Text.Trim().ToUpper(),
                            SERIE = txtRegSerie.Text.Trim().ToUpper(),
                            GESTION_FABRICA = spnRegGesFab.Number,
                            ID_PRODUCTO = Convert.ToDecimal(cmbRegProducto.Value),
                            APP_ID_USUARIO = (decimal)Session[CVariablesSesion.IdUsuario]
                        }
                    });
                    if (objRes.decCodigo == 1)
                    {
                        //MLimpiaPopUp();
                        pucRegistroExt.ShowOnPageLoad = false;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError", "alert('" + objRes.strMensaje + "');", true);
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

        protected void pucRegistroExt_WindowCallback(object source, PopupWindowCallbackArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar
                    MLimpiaPopUp();
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

        protected void btnProcesa_Click(object sender, EventArgs e)
        {
            memCadena.Text = memCadena.Text.ReplaceAll("\r\n", "").ReplaceAll(" ", "");
        }

        protected void btnFormato_Click(object sender, EventArgs e)
        {
            var strCadena = "";
            switch ((int)rblTipoOperacion.Value)
            {
                case 1:
                    strCadena =
                        "{\r\n" +
                        "'citeInfraestructura': 'ANH-IFR 9999999/2017',\r\n" +
                        "'objetoMovimientoTransaccion': {\r\n" +
                        "'idIntegracion': 1806150101,\r\n" +
                        "'idTipoOperacion': 56,\r\n" +
                        "'fechaMovimiento': 20180627080000,\r\n" +
                        "'idProyectoRelacionado': 1598492,\r\n" +
                        "'idActividadProyectoRel': 19,\r\n" +
                        "'idEstadoMovimiento': 11,\r\n" +
                        "'idTipoFlujo': 20,\r\n" +
                        "'idTipoMovimiento': 18,\r\n" +
                        "'idPais': 120,\r\n" +
                        "'idHidrocarburo': 129,\r\n" +
                        "'idUnidadMedidaHidrocarburo': 68,\r\n" +
                        "'cantidadHidrocarburo': 18500,\r\n" +
                        "'codigoBarras': 0,\r\n" +
                        "'identificador': '123PLA',\r\n" +
                        "'subIdentificador': '',\r\n" +
                        "'codigoCs': 'CS123',\r\n" +
                        "'idTipoEnvase': -99999,\r\n" +
                        "'idTipoInfraestructuraRef': 413,\r\n" +
                        "'idInfraestructuraRef': -99999,\r\n" +
                        "'idTipoInfraestructura': 242,\r\n" +
                        "'idInfraestructura': 62,\r\n" +
                        "'idUnidadTemperatura': 17,\r\n" +
                        "'temperatura': 15,\r\n" +
                        "'gravedadEspecifica': '',\r\n" +
                        "'idTipoMedicion': -99999,\r\n" +
                        "'cantidadAguaLitro': 0,\r\n" +
                        "'observacion': 'PRUEBA ANH',\r\n" +
                        "'idAnhTransaccion': '',\r\n" +
                        "'listaFacturas': [\r\n" +
                        "{\r\n" +
                        "'fechaEmision': 20180627080000,\r\n" +
                        "'idTipoTransaccion': 2,\r\n" +
                        "'idTipoEmisor': 14,\r\n" +
                        "'idTipoCreditoFiscal': 10,\r\n" +
                        "'idFormaPago': 1,\r\n" +
                        "'idTipoMoneda': 1,\r\n" +
                        "'nitEmisor': 484878978,\r\n" +
                        "'nitConsumidor': 0,\r\n" +
                        "'razonSocial': 'YPFB REFINERIA',\r\n" +
                        "'numeroAutorizacion': 2001009876548,\r\n" +
                        "'numeroFactura': '801',\r\n" +
                        "'codigoControl': 'AA-44-45-FE',\r\n" +
                        "'precioUnitario': 3,\r\n" +
                        "'importeTotal': 55500,\r\n" +
                        "'usuario': {\r\n" +
                        "'documentoIdentidad': 4988756,\r\n" +
                        "'nombreCompleto': ' JUAN PEREZ'\r\n" +
                        "},\r\n" +
                        "'observaciones': ''\r\n" +
                        "}\r\n" +
                        "],\r\n" +
                        "'listaDocumentos': [\r\n" +
                        "{\r\n" +
                        "'fechaEmision': 20180627080000,\r\n" +
                        "'idTipoTransaccion': 5,\r\n" +
                        "'numeroDocumento': '4897',\r\n" +
                        "'ciResponsable': 95887458,\r\n" +
                        "'responsable': 'DANIEL SANCHEZ',\r\n" +
                        "'precioUnitario': 0,\r\n" +
                        "'importeTotal': 0,\r\n" +
                        "'usuario': {\r\n" +
                        "'documentoIdentidad': 4988756,\r\n" +
                        "'nombreCompleto': ' JUAN PEREZ'\r\n" +
                        "},\r\n" +
                        "'observaciones': ''\r\n" +
                        "},\r\n" +
                        "{\r\n" +
                        "'fechaEmision': 20180627080000,\r\n" +
                        "'idTipoTransaccion': 16,\r\n" +
                        "'numeroDocumento': 'CRE0001',\r\n" +
                        "'ciResponsable': 95887458,\r\n" +
                        "'responsable': 'DANIEL SANCHEZ',\r\n" +
                        "'precioUnitario': 0,\r\n" +
                        "'importeTotal': 0,\r\n" +
                        "'usuario': {\r\n" +
                        "'documentoIdentidad': 4988756,\r\n" +
                        "'nombreCompleto': 'JUAN PEREZ'\r\n" +
                        "},\r\n" +
                        "'observaciones': ''\r\n" +
                        "}\r\n" +
                        "]\r\n" +
                        "}\r\n" +
                        "}\r\n";
                    break;
                case 2:
                    strCadena = "";
                    break;
            }

            memCadena.Text = strCadena.ReplaceAll("'", "\"");



        }

        protected void btnEnvia_Click(object sender, EventArgs e)
        {
            var strCadena = memCadena.Text.ReplaceAll("\r\n", "");
            var objEnvio = JsonSerializer.DeserializeFromString<pscaaObjMovimientoTransaccion>(strCadena);

            var objRes = _extAgent.Post<ResultadoConsumidor>(CAppSettings.svc_RegistrarMovimiento,
            new
            {
                objEnvio.citeInfraestructura,
                objEnvio.objetoMovimientoTransaccion
            });
            if (objRes.decCodigo == 1)
            {

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
        /// METODO. MCargaLateral
        /// </summary>
        private void MCargaDatosIniciales()
        {
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
                cmbOrigen.DataSource = lstResultado;
                cmbOrigen.DataBind();
                cmbDestino.DataSource = lstResultado;
                cmbDestino.DataBind();
                cmbAlterno.DataSource = lstResultado;
                cmbAlterno.DataBind();
                Session[CVariablesSesion.GrillaHistorial] = lstResultado;
            }
            else
            {
                MImprimirMensaje("Obtener Aeropuertos. " + objRes.strMensaje, EnumTiposMensajeAlerta.Error);
            }

            var objMtrClase = _srvMtr.Post<EResultadoInt<O_SERVICIO_PARAMETRICAS>>(CAppSettings.svc_obtener_parametrica_motorizado,
            new
            {
                strCredencial = CParametrosHydro.StrCredencialMtr,
                decTipo = 6, //Aeronaves
                decFiltro = 1 //clases
            });
            if (objMtrClase.intCodigo == 1)
            {
                cmbRegClase.DataSource = objMtrClase.oResultado;
                cmbRegClase.DataBind();
                cmbRegClase.SelectedIndex = 0;
                Session[CVariablesSesion.ComboTipoOperacion] = objMtrClase.oResultado;
            }

            var objMtrMarca = _srvMtr.Post<EResultadoInt<O_SERVICIO_PARAMETRICAS>>(CAppSettings.svc_obtener_parametrica_motorizado,
            new
            {
                strCredencial = CParametrosHydro.StrCredencialMtr,
                decTipo = 6, //Aeronaves
                decFiltro = 2//marcas
            });
            if (objMtrMarca.intCodigo == 1)
            {
                cmbRegMarca.DataSource = objMtrMarca.oResultado;
                cmbRegMarca.DataBind();
                cmbRegMarca.SelectedIndex = 0;
                Session[CVariablesSesion.ComboTiposOperacion] = objMtrMarca.oResultado;
            }

            //paises
            var objUbicacion = _client.Post<EResultadoUbicacion>(CAppSettings.svc_obtiene_ubicacion,
            new
            {
                strCredencial = CParametrosHydro.strCredencialFuncionario,
                decNivel = 1,
                decIdPadre = 0
            });
            if (objUbicacion.IntCodigo == 1)
            {
                cmbRegPais.DataSource = objUbicacion.OResultado;
                cmbRegPais.DataBind();
                cmbRegPais.SelectedIndex = 0;
                Session[CVariablesSesion.ListaDepartamentos] = objUbicacion.OResultado;
            }
        }

        /// <summary>
        /// METODO. MValidaDatos
        /// </summary>
        /// <returns></returns>
        private bool MValidaDatos()
        {
            var booRespuesta = true;

            if ((int)rbtTipo.Value==2 && txtNoVuelo.Text.Length < 6)
            {
                MImprimirMensaje("Introduzca un Nro. de Vuelo valido.",EnumTiposMensajeAlerta.Error);
                booRespuesta = false;
            }
            return booRespuesta;
        }

        /// <summary>
        /// METODO. MLimpiaPopUp
        /// </summary>
        private void MLimpiaPopUp()
        {
            txtRegPropietario.Text = "";
            txtRegNroIdentificacion.Text = "";
            cmbRegPais.SelectedIndex = 0;
            txtRegMatricula.Text = "";
            cmbRegClase.SelectedIndex = 0;
            cmbRegMarca.SelectedIndex = 0;
            cmbRegServicio.SelectedIndex = 0;
            txtRegModelo.Text = "";
            txtRegSerie.Text = "";
            spnRegGesFab.Number = 2000;
            cmbProducto.SelectedIndex = 0;
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