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
    using DevExpress.Web;
    using AnhHydroOctanoPersistencia.Aplicacion.Consulta;
    using AnhHydroOctanoPersistencia.Aplicacion.Gestion;
    using ServiceStack.ServiceClient.Web;
    using Clases.Util;
    using AnhHydroOctanoPresentacion.Lib;
    using AnhHydroOctanoPresentacion.Entidades;
    using AnhHydroOctanoPersistencia.Entidades;
    using AnhHydroOctanoPresentacion.Enumeradores;
    using System.Drawing;
    using AnhHydroOctanoPresentacion.Clases.Controles;
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Globalization;
    using ServiceStack.Common.Extensions;
    using ServiceStack.ServiceClient.Web;
    using ServiceStack.Text;
    public partial class CtrImportacionDatosPDE : System.Web.UI.UserControl
    {
        #region variables de entorno
        private readonly CPersistenciaConsulta _consulta = new CPersistenciaConsulta();
        readonly JsonServiceClient _client = new JsonServiceClient(CAppSettings.servicio_octVolumen);
        private readonly CPersistenciaGestion _gestion = new CPersistenciaGestion();
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
                        Session.Remove(CVariablesSesion.EstructuraArbol);
                        Session.Remove(CVariablesSesion.ListaProyectoProducto);
                        Session.Remove(CVariablesSesion.ListaParametrica);
                        Session.Remove(CVariablesSesion.PrimerDiaSemana);
                        Session.Remove(CVariablesSesion.Callback);
                        Session.Remove(CVariablesSesion.ArchivoDescarga);
                        Session.Remove(CVariablesSesion.GrillaTransaccion);
                        Session.Remove(CVariablesSesion.GrillaRegObservado);
                        Session.Remove(CVariablesSesion.GrillaTransPlantilla);
                        MCargaArbol();
                        MCargaParametricas();

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
                    if (dgvTransPlantilla.DataSource == null && Session[CVariablesSesion.GrillaTransPlantilla] != null)
                    {
                        dgvTransPlantilla.DataSource = Session[CVariablesSesion.GrillaTransPlantilla];
                        dgvTransPlantilla.DataBind();
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
                                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError", "LoadingPanel.Hide();", true);

                                if (dgvTransaccionError.VisibleRowCount > 0)
                                {
                                    //Session[CVariablesSesion.ArchivoDescarga] = dgvTransaccionError.DataSource;
                                    //ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( 'WFDescarga.aspx', '')", true);
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
                        var lstRegistros = Session[CVariablesSesion.GrillaTransaccion] as List<lstImportacionDatos>;                                                                       
                        O_RESULTADO_CTY obResultad = new O_RESULTADO_CTY();

                        var objResultado = _gestion.ImportaProgramacionDPE(lstRegistros);
                        if (objResultado.intCodigo >0)
                        {
                            obResultad.ID_TABLA = objResultado.intCodigo;
                            obResultad.MENSAJE_ERROR = objResultado.strMensaje;
                            //MImprimirMensaje(objResultado.strMensaje, EnumTiposMensajeAlerta.Correcto, true);
                            MImprimirMensaje(objResultado.strMensaje, EnumTiposMensajeAlerta.Correcto);
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError", "alert('" + objResultado.strMensaje + "');", true);
                            MLimpiaGrilla();
                        }
                        else
                        {
                            MImprimirMensaje("Error inesperado comuniquese ", EnumTiposMensajeAlerta.Error);
                            MLimpiaGrilla();
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
        protected void btnLimpiaTo_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    #region Proceso a Ejecutar
            //    var decFechaIni = Convert.ToDecimal(cmbPeriodo.Value + "01");
            //    var decFechaFin = Convert.ToDecimal(cmbPeriodo.Value + DateTime.DaysInMonth(Convert.ToInt16(cmbPeriodo.Value.ToString().Substring(0, 4)), Convert.ToInt16(cmbPeriodo.Value.ToString().Substring(4, 2))).ToString().PadLeft(2, '0'));
            //    var tareasMovimientos = _gestion.TareasMovimientos(1, 5, Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString()), Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()), Convert.ToDecimal(cmbTipoOperacion.Value), decFechaIni, decFechaFin);
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError", "LoadingPanel.Hide();", true);

            //    if (tareasMovimientos.intCodigo != 1)
            //    {
            //        MImprimirMensaje(tareasMovimientos.strMensaje, EnumTiposMensajeAlerta.Error);
            //    }
            //    #endregion
            //}
            //catch (Exception exp)
            //{
            //    #region RegistraError

            //    MImprimirMensaje(exp.Message, EnumTiposMensajeAlerta.Error);
            //    var oMensajeUsuario = new CLogTraza.MensajeUsuario
            //    {
            //        decUsuarioId = Session[CVariablesSesion.IdUsuario],
            //        decIdModulo = CParametrosHydro.decIdAplicacion,
            //        strAccion = GetType().Name + "." + MethodBase.GetCurrentMethod().Name,
            //        strIp = HttpContext.Current.Request.UserHostAddress,
            //        decNivelCapa = (int)CLogTraza.CapasNivel.Presentacion
            //    };
            //    CLogTraza.Error(oMensajeUsuario, exp);

            //    #endregion
            //}
        }
        protected void dgvTransaccionError_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            //try
            //{
            //    #region Proceso a Ejecutar

            //    if (e.VisibleIndex >= 0)
            //    {
            //        if (e.VisibleIndex % 2 == 1)
            //        {
            //            e.Row.BackColor = Color.LightGray;
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

            //    #endregion
            //}
            //catch (Exception exp)
            //{
            //    #region RegistraError

            //    MImprimirMensaje(exp.Message, EnumTiposMensajeAlerta.Error);
            //    var oMensajeUsuario = new CLogTraza.MensajeUsuario
            //    {
            //        decUsuarioId = Session[CVariablesSesion.IdUsuario],
            //        decIdModulo = CParametrosHydro.decIdAplicacion,
            //        strAccion = GetType().Name + "." + MethodBase.GetCurrentMethod().Name,
            //        strIp = HttpContext.Current.Request.UserHostAddress,
            //        decNivelCapa = (int)CLogTraza.CapasNivel.Presentacion
            //    };
            //    CLogTraza.Error(oMensajeUsuario, exp);

            //    #endregion
            //}
        }
        protected void dgvTransacciones_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            dgvTransacciones.DataSource = null;
            dgvTransacciones.DataBind();
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
                            int decCod;
                            DateTime dt = DateTime.Now;
                            int j = dt.Year;
                            var lstPeriodo = new List<Periodo>();
                            for (int i = j; i > 2019; i--)
                            {                               
                                for (int x = 1; x <= 12; x++)
                                {
                                    var objPeriodo = new Periodo();
                                    decCod = Convert.ToInt32(i + x.ToString().PadLeft(2, '0'));
                                    var dia = decCod.ToString().Substring(4,2);
                                    var anio = decCod.ToString().Substring(0, 4);
                                    objPeriodo.decPeriodo = decCod;
                                    objPeriodo.strPeriodo = dia + "/" + anio;
                                    //cmbPeriodo.Items.Add(x.ToString().PadLeft(2, '0') + "/" + i, decCod);
                                    lstPeriodo.Add(objPeriodo);
                                }
                            }
                            cmbPeriodo.DataSource = lstPeriodo.OrderBy(l => l.decPeriodo); 
                            cmbPeriodo.ValueField = "decPeriodo";
                            cmbPeriodo.TextField = "strPeriodo";
                            cmbPeriodo.DataBind();                              

                            decCod = Convert.ToInt32(DateTime.Now.Year + DateTime.Now.Month.ToString().PadLeft(2, '0'));
                            cmbPeriodo.Value = decCod;
                            var objTipoOperacion = _client.Post<EResultadoTipoOperacion>(CAppSettings.svc_obtener_tipos_operacion_act,
                              new
                              {
                                  decFiltro = 5,
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
                            var objArbol = Session[CVariablesSesion.ObjetoArbol] as List<O_OBTIENE_ENTIDADES_CTY>;
                            if (objArbol != null)
                            {
                                var lstEntidad = (objArbol.Where(l => l.ID_ACTIVIDAD == Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad")))).ToList();
                                //objArbol.OrderBy(x => x.ACTIVIDAD).ThenBy(x => x.DEPARTAMENTO).ThenBy(x => x.DENOMINACION).ToList();
                                var lstDatos = new List<lstProde>();
                                foreach (var lst in lstEntidad)
                                {
                                    //HIDROCARBUROS//
                                    var objHidrocarburo = _client.Post<EResultadoConfigHidrocarburo>(CAppSettings.svc_listar_autorizacion_condicion,
                                        new
                                        {
                                            strCredencial = CParametrosHydro.strCredencialFuncionario,
                                            decFiltro = 1,
                                            strParametro1 = lst.ID_CONSUMIDOR,
                                            strParametro2 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
                                            strParametro3 = ""
                                        });
                                    if (objHidrocarburo.IntCodigo == 1)
                                    {
                                        foreach (var item in objHidrocarburo.OResultado)
                                        {
                                            var objDatos = new lstProde();
                                            objDatos.decIdActividad = lst.ID_ACTIVIDAD;
                                            objDatos.decIdConsumidor = lst.ID_CONSUMIDOR;
                                            objDatos.strDenominacion = lst.DENOMINACION;
                                            objDatos.decIdDepartamento = Convert.ToDecimal(lst.ID_DEPARTAMENTO);
                                            objDatos.strDepartamento = lst.DEPARTAMENTO;
                                            objDatos.decIdProducto = item.ID_IDENTIFICADOR;
                                            objDatos.strProducto = item.NOMBRE;
                                            objDatos.decCantidad = 0;
                                            lstDatos.Add(objDatos);
                                        }
                                    }
                                }
                                Session[CVariablesSesion.GrillaTransPlantilla] = lstDatos;
                                dgvTransPlantilla.DataSource = lstDatos;
                                dgvTransPlantilla.DataBind();

                            }
                            else
                            {
                                MImprimirMensaje("No se encontraron registro para la listado ", EnumTiposMensajeAlerta.Error);
                                return;
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
                    MLimpiaGrilla();

                    if ((int)tlsOperadores.FocusedNode.GetValue("IntAccion") > 0)
                    {
                        string strMensaje = string.Empty;
                        if (MValidaDatosIniciales(ref strMensaje))
                        {
                            btnImportar.Enabled = true;
                            btnLimpiaTo.Enabled = true;
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
        protected void tlsOperadores_CustomCallback(object sender, DevExpress.Web.ASPxTreeList.TreeListCustomCallbackEventArgs e)
        {
            //try
            //{
            //    #region Proceso a Ejecutar

            //    if (txtBuscador.Text.Trim().Length > 0)
            //    {
            //        var lstArbol = Session[CVariablesSesion.EstructuraArbol] as List<CArbol>;
            //        tlsOperadores.CollapseAll();
            //        var lstCoinsidencia = lstArbol.Where(x => x.StrDescripcion.ToLower().Contains(txtBuscador.Text.Trim().ToLower())).OrderByDescending(x => x.IntId).ToList();
            //        foreach (var reg in lstCoinsidencia)
            //        {
            //            Session[CVariablesSesion.EntidadSeleccionada] = reg.StrUbicacion;
            //            var vecSel = reg.StrUbicacion.Split('-');
            //            switch (vecSel.Count())
            //            {
            //                case 1:
            //                    tlsOperadores.Nodes[Convert.ToInt16(vecSel[0])].Focus();
            //                    break;
            //                case 2:
            //                    tlsOperadores.Nodes[Convert.ToInt16(vecSel[0])].ChildNodes[Convert.ToInt16(vecSel[1])].Focus();
            //                    break;
            //                case 3:
            //                    tlsOperadores.Nodes[Convert.ToInt16(vecSel[0])].ChildNodes[Convert.ToInt16(vecSel[1])].ChildNodes[Convert.ToInt16(vecSel[2])].Focus();
            //                    break;
            //            }
            //        }
            //    }

            //    #endregion
            //}
            //catch (Exception exp)
            //{
            //    #region RegistraError

            //    MImprimirMensaje(exp.Message, EnumTiposMensajeAlerta.Error);
            //    var oMensajeUsuario = new CLogTraza.MensajeUsuario
            //    {
            //        decUsuarioId = Session[CVariablesSesion.IdUsuario],
            //        decIdModulo = CParametrosHydro.decIdAplicacion,
            //        strAccion = GetType().Name + "." + MethodBase.GetCurrentMethod().Name,
            //        strIp = HttpContext.Current.Request.UserHostAddress,
            //        decNivelCapa = (int)CLogTraza.CapasNivel.Presentacion
            //    };
            //    CLogTraza.Error(oMensajeUsuario, exp);

            //    #endregion
            //}
        }
        protected void btnDescargar_Click(object sender, EventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar
                    string strMensaje = string.Empty;
                    if (MValidaDatosIniciales(ref strMensaje))
                    {
                        dgeExportar.GridViewID = "dgvTransPlantilla";
                        dgeExportar.WriteCsvToResponse("plantilla_prode");
                        //dgeExportar.WriteCsvToResponse("plantilla_prode");
                    }
                    else
                    {
                        MImprimirMensaje(strMensaje, EnumTiposMensajeAlerta.Alerta);
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

            Session[CVariablesSesion.EstructuraArbol] = MObtenerArbolOrdenado(2, objArbol);
        }
        public List<CArbol> MObtenerArbolOrdenado(int intTipoOrden, List<O_OBTIENE_ENTIDADES_CTY> objArbol)
        {
            var lstResultado = new List<CArbol>();
            if (objArbol != null)
            {
                var lstEntidad = objArbol;

                int idDe = 0, idAc = 0;
                int idUnico = 1, idPadre = 0, idUnic1 = 0;
                int intNivel1 = 0, intNivel2 = 0, intNivel3 = 0;
                string strNivel1 = "", strNivel2 = "";
                string idStrAc = "";

                switch (intTipoOrden)
                {
                    case 2:
                        #region orden Actividad
                        lstEntidad = lstEntidad.OrderBy(x => x.ACTIVIDAD).ThenBy(x => x.DEPARTAMENTO).ThenBy(x => x.DENOMINACION).ToList();

                        foreach (var lst in lstEntidad)
                        {
                            long lng;
                            var lngNit = long.TryParse(lst.NIT, out lng) ? Convert.ToInt64(lst.NIT) : 0;

                            CArbol cDato;
                            if (idDe != lst.ID_ACTIVIDAD)
                            {
                                cDato = new CArbol
                                {
                                    IntId = idUnico,
                                    IntIdPadre = 0,
                                    IntIdEntidad = 0,
                                    IntIdActividad = (int)lst.ID_ACTIVIDAD,
                                    IntIdOperador = 0,
                                    IntIdProyecto = 0,
                                    StrDescripcion = lst.ACTIVIDAD.Trim(),
                                    StrDescPadre = lst.ACTIVIDAD.Trim(),
                                    LngNit = 0,
                                    StrLicencia = "",
                                    StrRepLegal = lst.REPRESENTANTE_LEGAL,
                                    StrDireccion = "",
                                    StrDepartamento = "",
                                    DecIdDepartamento = 0,
                                    StrMunicipio = "",
                                    StrLocalidad = "",
                                    StrTelefono = "",
                                    StrAmbito = "",
                                    StrPropietario = "",
                                    StrCiPropietario = "",
                                    StrCiRepresentante = "",
                                    IntAccion = 1,
                                    StrUbicacion = intNivel1.ToString(),
                                    StrNroHydro = ""
                                };
                                lstResultado.Add(cDato);
                                idPadre = idUnico;
                                idUnic1 = idUnico;
                                idDe = (int)lst.ID_ACTIVIDAD;
                                idStrAc = "";
                                idUnico++;
                                strNivel1 = cDato.StrUbicacion;
                                intNivel1++;
                                intNivel2 = 0;
                            }
                            else
                            {
                                //todo No implementado.
                            }
                        }

                        #endregion
                        break;
                }
            }
            return lstResultado;
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
        /// METODO. MCargarDatos
        /// </summary>
        /// <param name="strRuta"></param>
        private void MCargarDatos(string strRuta)
        {
            dgvTransacciones.Visible = true;
            var reader = new StreamReader(File.OpenRead(@strRuta));
            var lstRegistros = new List<lstImportacionDatos>();
            var lstRegistroE = new List<CRegistrosError>();
            long lngNoRegCorrect = 1, lngNoRegInCorrect = 1;
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(';');
                try
                {
                    var to = cmbTipoOperacion.Text.Split(';');
                    var strTipoOp = to[1].Substring(2, 2);

                    string strMensaje = string.Empty;
                    var strRazonSocial = "";
                    if (MValidaPlantillaPDE(values, ref strMensaje))
                    {
                        var cDato = new lstImportacionDatos();
                        switch (strTipoOp)
                        {
                            case "RC":
                                #region Valida y genera objeto

                                cDato.decIdActividad = Convert.ToDecimal(values[0]);
                                cDato.decIdConsumidor = Convert.ToDecimal(values[1]);
                                strRazonSocial = values[2].Trim().ToUpper().Replace("¥", "Ñ").Replace("Á", "A").Replace("É", "E").Replace("Í", "I").Replace("Ó", "O").Replace("Ú", "U").Replace("'", "").Replace("�", "Ñ");
                                cDato.strDenominacion = Regex.Replace(strRazonSocial, @"\s+", " ");
                                cDato.decIdDepartamento = Convert.ToDecimal(values[3]);
                                cDato.strDepartamento = values[4].Trim().ToUpper();
                                cDato.decIdProducto = Convert.ToDecimal(values[5]);
                                cDato.strProducto = values[6].Trim().ToUpper();
                                cDato.decCantidad = Convert.ToDecimal(values[7]);
                                cDato.decPeriodo = Convert.ToDecimal(cmbPeriodo.Value);
                                cDato.decUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                cDato.decTipoOperacion = Convert.ToDecimal(cmbTipoOperacion.Value);
                                #endregion
                                break;
                            case "DS":
                                #region Valida y genera objeto

                                #endregion
                                break;
                        }
                        lstRegistros.Add(cDato);

                    }
                    else
                    {
                        if (strTipoOp == "RC")
                        {
                            var cRegError = new CRegistrosError();
                            cRegError.LngId = lngNoRegInCorrect;
                            cRegError.StrIdVenta = lngNoRegCorrect.ToString(); ;
                            cRegError.LngFechaEmision = cmbPeriodo.Value.ToString();
                            cRegError.LngNit = values[1];
                            cRegError.LngNroAutorizacion = strRazonSocial;
                            cRegError.LngNroFactura = values[4].Trim().ToUpper();
                            cRegError.LngNroReferencia = values[6].Trim().ToUpper();
                            cRegError.StrErrores = strMensaje.Trim();
                            lstRegistroE.Add(cRegError);
                            lngNoRegInCorrect++;
                        }
                        else
                        {
                            var cRegError = new CRegistrosError();
                            cRegError.LngId = lngNoRegInCorrect;
                            cRegError.StrIdVenta = lngNoRegCorrect.ToString(); ;
                            cRegError.LngFechaEmision = "";
                            cRegError.LngNit = "";
                            cRegError.LngNroAutorizacion = "";
                            cRegError.LngNroFactura = "0";
                            cRegError.LngNroReferencia = "";
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



            if (dgvTransaccionError.VisibleRowCount == 0)
            {
                btnGrabar.Enabled = true;
            }
        }
        private bool MValidaPlantillaPDE(string[] values, ref string strMensaje)
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
                        if (values.Length != 8)
                        {
                            strMensaje = "- Nro. de columnas del registro invalido.";
                        }
                        arrayNumero = new[] { 0, 1, 3, 5, 7 };
                        for (int i = 0; i < arrayNumero.Length; i++)
                        {
                            if (!decimal.TryParse(values[arrayNumero[i]].Trim(), out dec))
                            {
                                strMensaje += "- Columna Nro. " + arrayNumero[i] + " No es un número.";
                            }
                        }
                        //if (values[0].Trim().Substring(6, 4) + values[0].Trim().Substring(3, 2) != cmbPeriodo.Value.ToString().Substring(0, 4) + cmbPeriodo.Value.ToString().Substring(4, 2))
                        //{
                        //    strMensaje += "- Periodo incorrecto.";
                        //}
                        //if (!DateTime.TryParse(values[0].Trim(), out dt))
                        //{
                        //    strMensaje += "- Fecha incorrecta.";
                        //}
                        //if (Convert.ToDecimal(values[7]) <= 0)
                        //{
                        //    strMensaje += "- Cantidad incorrecta.";
                        //}
                        //if (MCodificaParametrica((int)EnumTipoParametrica.Producto, values[1].Trim()) == -99999)
                        //{
                        //    strMensaje += "- Producto incorrecto.";
                        //}
                        //if (values[4].Trim().Length == 0 || values[4].Trim() == "0")
                        //{
                        //    strMensaje += "- Número de referencia invalido.";
                        //}

                        #endregion
                        break;
                    default:

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
                        lstEntidad = lstEntidad.Where(x => x.DENOMINACION == strDescripcion || x.USUARIO_ASIGNADO == strDescripcion).ToList();
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
            }
            return lngResultado;
        }
        #endregion
        public class lstProde
        {
            public decimal decIdActividad { get; set; }
            public decimal decIdConsumidor { get; set; }
            public string strDenominacion { get; set; }
            public decimal decIdDepartamento { get; set; }
            public string strDepartamento { get; set; }
            public decimal decIdProducto { get; set; }
            public string strProducto { get; set; }
            public decimal decCantidad { get; set; }
        }
        private class Periodo
        {
            public decimal decPeriodo { get; set; }
            public string strPeriodo { get; set; }
        }

    }
}