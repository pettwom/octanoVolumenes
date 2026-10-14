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
    using AnhHydroOctanoPersistencia.Entidades;
    using System.Drawing;
    using Enumeradores;
    using DevExpress.Web;
    using AnhHydroOctanoPresentacion.Lib;
    using Clases.Util;
    using ServiceStack.Text;
    public partial class CtrVisorPahl : System.Web.UI.UserControl
    {
        #region variables de entorno
        private readonly CPersistenciaConsulta _consulta = new CPersistenciaConsulta();
        readonly JsonServiceClient _client = new JsonServiceClient(CAppSettings.servicio_octVolumen);
        CConsultasParametricas cConsultasParametricas = new CConsultasParametricas();
        readonly CUtil _cUtil = new CUtil();
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
                        Session.Remove(CVariablesSesion.EstructuraArbol);
                        Session.Remove(CVariablesSesion.ListaLectorRFID);
                        Session.Remove(CVariablesSesion.ImgMTR);
                        detFechaIni.Date = DateTime.Now;
                        detFechaFin.Date = DateTime.Now;
                        MCargaArbol();
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
                        //todo No es carga inicial: no implementado
                    }
                    if (dgvListadoPahl.DataSource == null && Session[CVariablesSesion.ListaLectorRFID] != null)
                    {
                        dgvListadoPahl.DataSource = Session[CVariablesSesion.ListaLectorRFID];
                        dgvListadoPahl.DataBind();
                    }
                    else
                    {
                        //todo grilla vacia: no implementado
                    }
                    if (imgFotos.DataSource == null && Session[CVariablesSesion.ImgMTR] != null)
                    {
                        imgFotos.DataSource = Session[CVariablesSesion.ImgMTR];
                        imgFotos.DataBind();
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
        protected void btnEmitir_Click(object sender, EventArgs e)
        {
            try
            {
                #region Proceso a Ejecutar
                dgeExportar.GridViewID = "dgvListadoPahl";
                dgeExportar.WriteXlsxToResponse();
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
        #region #region Metodos y funciones
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

            objArbol = objArbol.Where(x => x.ID_ACTIVIDAD == 5).ToList();

            Session[CVariablesSesion.EstructuraArbol] = _cUtil.MObtenerArbolOrdenado(Properties.Settings.Default.intOrdenArbol, objArbol);
        }
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
        protected void pnlDatos_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar
                    if ((int)tlsOperadores.FocusedNode.GetValue("IntAccion") > 0)
                    {
                        var objConsulta = new EObjetoEnvio5ParametrosProto
                        {
                            strCredencial = CParametrosHydro.strCredencialFuncionario,
                            decFiltro = 1,
                            strParametro1 = tlsOperadores.FocusedNode.GetValue("DecIdDepartamento").ToString(),
                            strParametro2 = tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString(),
                            strParametro3 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
                            strParametro4 = detFechaIni.Date.Year + detFechaIni.Date.Month.ToString().PadLeft(2, '0') + detFechaIni.Date.Day.ToString().PadLeft(2, '0') + "000000",
                            strParametro5 = detFechaFin.Date.Year + detFechaFin.Date.Month.ToString().PadLeft(2, '0') + detFechaFin.Date.Day.ToString().PadLeft(2, '0') + "235959"
                        };
                        var objResultado = _consulta.ObtenerLecturaRFID(objConsulta);
                        if (objResultado.intCodigo == 1)
                        {
                            var lstDato = (List<O_LECTURA_RFID_CTY>)objResultado.oResultado;

                            Session[CVariablesSesion.ListaLectorRFID] = lstDato;
                            dgvListadoPahl.DataSource = lstDato;
                            dgvListadoPahl.DataBind();

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
                            Session.Remove(CVariablesSesion.ListaLectorRFID);
                            dgvListadoPahl.DataSource = null;
                            dgvListadoPahl.DataBind();
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
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError", "alert('" + exp.Message + "');", true);
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
        protected void pnlCabecera_Callback(object sender, CallbackEventArgsBase e)
        {
            //if (MValidaSesion())
            //{
            //    try
            //    {
            //        #region Proceso a Ejecutar

            //        cmbTipoOperacion.DataSource = null;
            //        cmbTipoOperacion.Text = "";
            //        cmbPeriodo.Items.Clear();
            //        cmbPeriodo.Text = "";
            //        if (tlsOperadores.FocusedNode != null)
            //        {
            //            if ((int)tlsOperadores.FocusedNode.GetValue("IntAccion") > 0)
            //            {
            //                int decCod;
            //                DateTime dt = DateTime.Now;
            //                int j = dt.Year;
            //                var lstPeriodo = new List<Periodo>();
            //                for (int i = j; i > 2019; i--)
            //                {
            //                    for (int x = 1; x <= 12; x++)
            //                    {
            //                        var objPeriodo = new Periodo();
            //                        decCod = Convert.ToInt32(i + x.ToString().PadLeft(2, '0'));
            //                        var dia = decCod.ToString().Substring(4, 2);
            //                        var anio = decCod.ToString().Substring(0, 4);
            //                        objPeriodo.decPeriodo = decCod;
            //                        objPeriodo.strPeriodo = dia + "/" + anio;
            //                        //cmbPeriodo.Items.Add(x.ToString().PadLeft(2, '0') + "/" + i, decCod);
            //                        lstPeriodo.Add(objPeriodo);
            //                    }
            //                }
            //                cmbPeriodo.DataSource = lstPeriodo.OrderBy(l => l.decPeriodo);
            //                cmbPeriodo.ValueField = "decPeriodo";
            //                cmbPeriodo.TextField = "strPeriodo";
            //                cmbPeriodo.DataBind();

            //                decCod = Convert.ToInt32(DateTime.Now.Year + DateTime.Now.Month.ToString().PadLeft(2, '0'));
            //                cmbPeriodo.Value = decCod;
            //                var objTipoOperacion = _client.Post<EResultadoTipoOperacion>(CAppSettings.svc_obtener_tipos_operacion_act,
            //                  new
            //                  {
            //                      decFiltro = 5,
            //                      strParametro1 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
            //                      strParametro2 = "",
            //                      strParametro3 = ""
            //                  });
            //                if (objTipoOperacion.IntCodigo == 1)
            //                {
            //                    objTipoOperacion.OResultado = objTipoOperacion.OResultado.OrderByDescending(x => x.FACTOR_CALCULO).ThenBy(y => y.NOMBRE).ToList();
            //                    cmbTipoOperacion.DataSource = objTipoOperacion.OResultado;
            //                    cmbTipoOperacion.DataBind();
            //                    Session[CVariablesSesion.ComboTipoOperacion] = objTipoOperacion.OResultado;
            //                }
            //                else
            //                {
            //                    MImprimirMensaje(objTipoOperacion.StrMensaje, EnumTiposMensajeAlerta.Error);
            //                }
            //                var objArbol = Session[CVariablesSesion.ObjetoArbol] as List<O_OBTIENE_ENTIDADES_CTY>;
            //                if (objArbol != null)
            //                {
            //                    var lstEntidad = (objArbol.Where(l => l.ID_ACTIVIDAD == Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad")))).ToList();
            //                    //objArbol.OrderBy(x => x.ACTIVIDAD).ThenBy(x => x.DEPARTAMENTO).ThenBy(x => x.DENOMINACION).ToList();
            //                    var lstDatos = new List<lstProde>();
            //                    foreach (var lst in lstEntidad)
            //                    {
            //                        //HIDROCARBUROS//
            //                        var objHidrocarburo = _client.Post<EResultadoConfigHidrocarburo>(CAppSettings.svc_listar_autorizacion_condicion,
            //                            new
            //                            {
            //                                strCredencial = CParametrosHydro.strCredencialFuncionario,
            //                                decFiltro = 1,
            //                                strParametro1 = lst.ID_CONSUMIDOR,
            //                                strParametro2 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
            //                                strParametro3 = ""
            //                            });
            //                        if (objHidrocarburo.IntCodigo == 1)
            //                        {
            //                            foreach (var item in objHidrocarburo.OResultado)
            //                            {
            //                                var objDatos = new lstProde();
            //                                objDatos.decIdActividad = lst.ID_ACTIVIDAD;
            //                                objDatos.decIdConsumidor = lst.ID_CONSUMIDOR;
            //                                objDatos.strDenominacion = lst.DENOMINACION;
            //                                objDatos.decIdDepartamento = Convert.ToDecimal(lst.ID_DEPARTAMENTO);
            //                                objDatos.strDepartamento = lst.DEPARTAMENTO;
            //                                objDatos.decIdProducto = item.ID_IDENTIFICADOR;
            //                                objDatos.strProducto = item.NOMBRE;
            //                                objDatos.decCantidad = 0;
            //                                lstDatos.Add(objDatos);
            //                            }
            //                        }
            //                    }
            //                    Session[CVariablesSesion.GrillaTransPlantilla] = lstDatos;
            //                    dgvTransPlantilla.DataSource = lstDatos;
            //                    dgvTransPlantilla.DataBind();

            //                }
            //                else
            //                {
            //                    MImprimirMensaje("No se encontraron registro para la listado ", EnumTiposMensajeAlerta.Error);
            //                    return;
            //                }
            //            }
            //            else
            //            {
            //                //todo No implementado.
            //            }
            //        }
            //        else
            //        {
            //            //todo No implementado.
            //        }

            //        #endregion
            //    }
            //    catch (Exception exp)
            //    {
            //        #region RegistraError

            //        MImprimirMensaje(exp.Message, EnumTiposMensajeAlerta.Error);
            //        var oMensajeUsuario = new CLogTraza.MensajeUsuario
            //        {
            //            decUsuarioId = Session[CVariablesSesion.IdUsuario],
            //            decIdModulo = CParametrosHydro.decIdAplicacion,
            //            strAccion = GetType().Name + "." + MethodBase.GetCurrentMethod().Name,
            //            strIp = HttpContext.Current.Request.UserHostAddress,
            //            decNivelCapa = (int)CLogTraza.CapasNivel.Presentacion
            //        };
            //        CLogTraza.Error(oMensajeUsuario, exp);

            //        #endregion
            //    }
            //    Session[CVariablesSesion.Callback] = "0";
            //}
            //else
            //{
            //    //todo No implementado.
            //}
        }
        protected void pucFoto_WindowCallback(object source, PopupWindowCallbackArgs e)
        {
            try
            {
                #region Proceso a Ejecutar
                if (Session[CVariablesSesion.ListaLectorRFID] != null)
                {
                    decimal decIdLectura = 0;
                    int index = dgvListadoPahl.FocusedRowIndex;
                    if (index >= 0)
                    {
                        decIdLectura = (decimal)dgvListadoPahl.GetRowValues(index, "ID_LECTURA_RFID");
                    }
                    if (decIdLectura > 0)
                    {

                        var lstAeronavesImg = ((List<O_LECTURA_RFID_CTY>)Session[CVariablesSesion.ListaLectorRFID]).Where(l => l.ID_LECTURA_RFID == decIdLectura).ToList();
                        imgFotos.DataSource = lstAeronavesImg;
                        imgFotos.DataBind();
                        Session[CVariablesSesion.ImgMTR] = lstAeronavesImg;
                        lblPlaca.Text = lstAeronavesImg[0].IDENTIFICADOR;
                        lblBarcode.Text = lstAeronavesImg[0].BARCODE;
                        lblMarca.Text = lstAeronavesImg[0].MARCA;
                    }
                    else
                    {
                        MImprimirMensaje("Tarjeta no asignada", EnumTiposMensajeAlerta.Error);
                    }
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
        protected void imgFotos_CustomCallback(object sender, CallbackEventArgsBase e)
        {
            try
            {
                #region Proceso a Ejecutar
                imgFotos.DataSource = null;
                imgFotos.DataBind();
                Session.Remove(CVariablesSesion.ImgMTR);
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
    }
}