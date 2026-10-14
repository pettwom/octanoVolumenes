
using AnhHydroOctanoPresentacion.Lib;

namespace AnhHydroOctanoPresentacion.Controles
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Web;
    using System.Web.UI;
    using AnhHydroOctanoAgenteServicio.ServicioHydroSesion;
    using AnhHydroOctanoPersistencia.Entidades;
    using Clases.Controles;
    using Clases.Persona;
    using Clases.Util;
    using Entidades;
    using Enumeradores;
    using Parametros;
    using AnhHydroOctanoPersistencia.Aplicacion.Consulta;
    using DevExpress.Web;
    using DevExpress.XtraPivotGrid;
    using Librerias.Anh.Us;
    using ServiceStack.ServiceClient.Web;
    using AnhHydroOctanoPresentacion.Sitio.Reportes;
    using ServiceStack.Text;

    public partial class CtrDeclaracionJurada : UserControl
    {
        #region Variables de entorno

        readonly JsonServiceClient _client = new JsonServiceClient(CAppSettings.servicio_octVolumen);
        private CPersonaSesion _datSesion;
        private readonly CPersistenciaConsulta _consulta = new CPersistenciaConsulta();
        readonly CUtil _cUtil = new CUtil();
        CConsultasParametricas cConsultasParametricas = new CConsultasParametricas();

        public string[] listaF1;

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
                        Session.Remove(CVariablesSesion.DjAcumulado);
                        Session.Remove(CVariablesSesion.EstructuraArbol);
                        Session.Remove(CVariablesSesion.GrillaAcumRep);
                        Session.Remove(CVariablesSesion.GrillaAcumulado);
                        Session.Remove(CVariablesSesion.GrillaInternacional);
                        Session.Remove(CVariablesSesion.GrillaAuxiliar);
                        Session.Remove(CVariablesSesion.IdPeriodoActual);
                        Session.Remove(CVariablesSesion.ListaParamGral);
                        Session.Remove(CVariablesSesion.OctanoParametrica);
                        Session.Remove(CVariablesSesion.ReportePrincipal);
                        Session.Remove(CVariablesSesion.ReporteSecundario);
                        Session.Remove(CVariablesSesion.ReporteTercero);
                        Session.Remove(CVariablesSesion.TipoReporte);
                        Session.Remove(CVariablesSesion.ComboTiposOperacion);

                        MCargaArbol();
                        MCargaParametricas();
                        MCargaPermisos();
                        ConfigurarFormulario();

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

                    if (pvgAcumulado.DataSource == null && Session[CVariablesSesion.GrillaAcumulado] != null)
                    {
                        pvgAcumulado.DataSource = Session[CVariablesSesion.GrillaAcumulado];
                        pvgAcumulado.DataBind();
                    }
                    else
                    {
                        //todo No es carga inicial: no implementado
                    }
                    if (dgvInternacional.DataSource == null && Session[CVariablesSesion.GrillaInternacional] != null)
                    {
                        dgvInternacional.DataSource = Session[CVariablesSesion.GrillaInternacional];
                        dgvInternacional.DataBind();
                    }
                    else
                    {
                        //todo No es carga inicial: no implementado
                    }
                    if (dgvContenedor.DataSource == null && Session[CVariablesSesion.GrillaAuxiliar] != null)
                    {
                        dgvContenedor.DataSource = Session[CVariablesSesion.GrillaAuxiliar];
                        dgvContenedor.DataBind();
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

                    cmbPeriodo.Items.Clear();
                    cmbPeriodo.Text = "";//detFechaIni

                    if (tlsOperadores.FocusedNode != null)
                    {
                        if ((int)tlsOperadores.FocusedNode.GetValue("IntAccion") > 0)
                        {
                            if (tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString() == "16")
                            {
                                detFechaIni.Visible = true;
                                cmbPeriodo.Visible = false;
                                btnConsultar.Visible = true;
                                cmbResMin.ClientVisible = true;
                            }
                            else if (tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString() == "36")
                            {
                                detFechaIni.Visible = true;
                                cmbPeriodo.Visible = false;
                                btnConsultar.Visible = true;
                                cmbResMin.ClientVisible = true;
                            }
                            else
                            {
                                detFechaIni.Visible = false;
                                cmbPeriodo.Visible = true;
                                btnConsultar.Visible = false;
                                cmbResMin.ClientVisible = false;

                                Session.Remove(CVariablesSesion.IdPeriodoActual);
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
                                    for (int i = 0; i < objPeriodo.OResultado.Count; i++)
                                    {
                                        decCod = Convert.ToInt32(objPeriodo.OResultado[i].decAño + objPeriodo.OResultado[i].decMes.ToString().PadLeft(2, '0'));
                                        cmbPeriodo.Items.Add(objPeriodo.OResultado[i].strPeriodo, decCod);

                                        if (i == objPeriodo.OResultado.Count - 1)
                                        {
                                            Session[CVariablesSesion.IdPeriodoActual] = objPeriodo.OResultado[i].strPeriodo;
                                        }
                                    }
                                }
                                else
                                {
                                    MImprimirMensaje(objPeriodo.StrMensaje, EnumTiposMensajeAlerta.Error);
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
                    lblDireccion.Text = (tlsOperadores.FocusedNode.GetValue("StrDireccion") == null) ? "" : tlsOperadores.FocusedNode.GetValue("StrDireccion").ToString();
                    lblDepto.Text = tlsOperadores.FocusedNode.GetValue("StrDepartamento").ToString();
                    MLimpiaGrilla();
                    if ((int)tlsOperadores.FocusedNode.GetValue("IntAccion") > 0)
                    {
                        string strMensaje = string.Empty;
                        if (MValidaDatosIniciales(ref strMensaje))
                        {
                            if (MValidaCondicionesFuncionamiento(ref strMensaje))
                            {
                                decimal decIdConsumidor;
                                decimal decIdTipoActividad;
                                decimal decFechaDiaInicio;
                                decimal decFechaDiaFin;

                                var intIdAct = Convert.ToInt32(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());

                                if (intIdAct == 16)
                                {
                                    decIdConsumidor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString());
                                    decIdTipoActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());

                                    int intOpcionRes = Convert.ToInt32(cmbResMin.Value);

                                    if (intOpcionRes == 1)
                                    {
                                        decFechaDiaInicio = Convert.ToDecimal(detFechaIni.Date.Year + detFechaIni.Date.Month.ToString().PadLeft(2, '0') + detFechaIni.Date.Day.ToString().PadLeft(2, '0'));
                                        decFechaDiaFin = Convert.ToDecimal(detFechaIni.Date.Year + detFechaIni.Date.Month.ToString().PadLeft(2, '0') + detFechaIni.Date.Day.ToString().PadLeft(2, '0'));
                                    }
                                    else
                                    {
                                        decFechaDiaInicio = Convert.ToDecimal(detFechaIni.Date.Year + detFechaIni.Date.Month.ToString().PadLeft(2, '0') + "01");
                                        decFechaDiaFin = Convert.ToDecimal(detFechaIni.Date.Year + detFechaIni.Date.Month.ToString().PadLeft(2, '0') + "31");
                                    }

                                }
                                else if (intIdAct == 36)
                                {
                                    decIdConsumidor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString());
                                    decIdTipoActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());

                                    int intOpcionRes = Convert.ToInt32(cmbResMin.Value);

                                    if (intOpcionRes == 1)
                                    {
                                        decFechaDiaInicio = Convert.ToDecimal(detFechaIni.Date.Year + detFechaIni.Date.Month.ToString().PadLeft(2, '0') + detFechaIni.Date.Day.ToString().PadLeft(2, '0'));
                                        decFechaDiaFin = Convert.ToDecimal(detFechaIni.Date.Year + detFechaIni.Date.Month.ToString().PadLeft(2, '0') + detFechaIni.Date.Day.ToString().PadLeft(2, '0'));
                                    }
                                    else
                                    {
                                        decFechaDiaInicio = Convert.ToDecimal(detFechaIni.Date.Year + detFechaIni.Date.Month.ToString().PadLeft(2, '0') + "01");
                                        decFechaDiaFin = Convert.ToDecimal(detFechaIni.Date.Year + detFechaIni.Date.Month.ToString().PadLeft(2, '0') + "31");
                                    }
                                }
                                else
                                {
                                    int intDia = DateTime.DaysInMonth(Convert.ToInt32(cmbPeriodo.Value.ToString().Substring(0, 4)), Convert.ToInt32(cmbPeriodo.Value.ToString().Substring(4, 2)));

                                    switch (intIdAct)
                                    {
                                        case (int)EnumActividades.EstacionServicioLiquido:
                                        case (int)EnumActividades.EstacionServicioGnv:
                                            tacMovimientos.Tabs[1].ClientVisible = true;
                                            tacMovimientos.Tabs[3].ClientVisible = true;
                                            break;
                                        case (int)EnumActividades.TerminalAeropuertoLiq:
                                            tacMovimientos.Tabs[2].ClientVisible = true;
                                            break;
                                    }

                                    //decimal decFiltro = 1;
                                    decIdConsumidor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString());
                                    decIdTipoActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                    decFechaDiaInicio = Convert.ToDecimal(cmbPeriodo.Value + "01");
                                    decFechaDiaFin = Convert.ToDecimal(cmbPeriodo.Value + intDia.ToString().PadLeft(2, '0'));
                                }

                                var decFiltro = MConfiguraGrilla(Convert.ToInt32(decIdTipoActividad.ToString()));
                                MCargaGrilla(decFiltro, decIdConsumidor, decIdTipoActividad, decFechaDiaInicio, decFechaDiaFin);
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

        protected void pvgAcumulado_CustomCellStyle(object sender, DevExpress.Web.ASPxPivotGrid.PivotCustomCellStyleEventArgs e)
        {
            try
            {
                #region Proceso a Ejecutar

                if (e.DataField != null)
                {
                    var x = e.RowValueType;
                    if (x.ToString().Contains("Value"))
                    {
                        if (e.DataField.FieldName == "DecSaldoInicial" || e.DataField.FieldName == "DecSaldoFinal")
                        {
                            e.CellStyle.BackColor = Color.FromArgb(189, 203, 133);
                        }
                        if (e.DataField.FieldName == "DecTotalIng" || e.DataField.FieldName == "DecTotalSal")
                        {
                            e.CellStyle.BackColor = Color.FromArgb(200, 207, 171);
                        }
                    }
                    else
                    {
                        e.CellStyle.BackColor = Color.FromArgb(189, 203, 133);
                        if (e.DataField.FieldName == "DecSaldoInicial" || e.DataField.FieldName == "DecSaldoFinal")
                        {
                            e.CellStyle.ForeColor = Color.FromArgb(189, 203, 133);
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

        protected void dgvInternacional_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            try
            {
                #region Proceso a Ejecutar

                //    var asPxSummaryItem = e.Item as ASPxSummaryItem;
                //    if (asPxSummaryItem != null)
                //    {
                //        switch (asPxSummaryItem.Tag)
                //        {
                //            case "impP":
                //                if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Start)
                //                    _decMontoSum = 0;

                //                if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Calculate)
                //                    _decMontoSum += Convert.ToDecimal(e.GetValue("DecImporteProp").ToString());

                //                if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                //                    e.TotalValue = _decMontoSum;
                //                break;
                //            case "impE":
                //                if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Start)
                //                    _decMontoSum = 0;

                //                if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Calculate)
                //                    _decMontoSum += Convert.ToDecimal(e.GetValue("DecImporteExt").ToString());

                //                if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                //                    e.TotalValue = _decMontoSum;
                //                break;
                //            case "cantidad":
                //                if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Start)
                //                    _decMontoSum = 0;

                //                if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Calculate)
                //                    _decMontoSum += Convert.ToDecimal(e.GetValue("DecCantidadExt").ToString());

                //                if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                //                    e.TotalValue = _decMontoSum;
                //                break;
                //        }
                //    }
                //    else
                //    {
                //        //todo No implementado.
                //    }

                #endregion
            }
            catch (Exception exp)
            {
                #region RegistraError

                //MImprimirMensaje(exp.Message, EnumTiposMensajeAlerta.Error);
                //var oMensajeUsuario = new CLogTraza.MensajeUsuario
                //{
                //    decUsuarioId = Session[CVariablesSesion.IdUsuario],
                //    decIdModulo = CParametrosHydro.decIdAplicacion,
                //    strAccion = GetType().Name + "." + MethodBase.GetCurrentMethod().Name,
                //    strIp = HttpContext.Current.Request.UserHostAddress,
                //    decNivelCapa = (int)CLogTraza.CapasNivel.Presentacion
                //};
                //CLogTraza.Error(oMensajeUsuario, exp);

                #endregion
            }
        }

        protected void dgvInternacional_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            try
            {
                #region Proceso a Ejecutar

                if (e.VisibleIndex >= 0)
                {
                    if (dgvInternacional.GetRowValues(e.VisibleIndex, "StrEstado").ToString().Contains("A"))
                        e.Row.BackColor = Color.FromArgb(249, 131, 131);
                    else
                    {
                        /*if (e.VisibleIndex%2 == 1)
                        {
                            e.Row.BackColor = Color.FromArgb(204, 209, 182);
                        }
                        else
                        {
                            //todo No implementado.
                        }*/
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

        protected void dgvRecaudo_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            try
            {
                #region Proceso a Ejecutar

                if (e.VisibleIndex < 0) return;
                if (dgvInternacional.GetRowValues(e.VisibleIndex, "StrEstado").ToString().Contains("A"))
                    e.Row.BackColor = Color.FromArgb(249, 131, 131);
                else
                {
                    /* if (e.VisibleIndex % 2 == 1)
                     {
                         e.Row.BackColor = Color.FromArgb(204, 209, 182);
                     }*/
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

        protected void dgvContenedor_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            try
            {
                #region Proceso a Ejecutar

                if (e.VisibleIndex >= 0)
                {
                    if (dgvContenedor.GetRowValues(e.VisibleIndex, "DecIdEstado").ToString() == "12")
                        e.Row.BackColor = Color.FromArgb(249, 131, 131);
                    else
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

        protected void pucDatos_WindowCallback(object source, PopupWindowCallbackArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar
                    MPuImprimirMensaje("", EnumTiposMensajeAlerta.Limpiar);

                    List<O_UBICACION_CTY> objParametricas = (List<O_UBICACION_CTY>)Session[CVariablesSesion.ListaParamGral];
                    cmbDepartamento.DataSource = objParametricas;
                    cmbDepartamento.DataBind();
                    cmbDepartamento.SelectedItem = cmbDepartamento.Items.FindByText("LA PAZ");

                    txtPadCodProy.Text = tlsOperadores.FocusedNode.GetValue("StrNroHydro").ToString().Trim();
                    txtPadPropietario.Text = tlsOperadores.FocusedNode.GetValue("StrPropietario").ToString().Trim() + "-" + tlsOperadores.FocusedNode.GetValue("StrCiPropietario").ToString().Trim();

                    var lstRep = tlsOperadores.FocusedNode.GetValue("StrRepLegal").ToString().Split(',');
                    //var lstRep = lstResultado.FirstOrDefault().REPRESENTANTE_LEGAL.Split(',');
                    for (int i = 0; i < lstRep.Length; i++)
                    {
                        cmbRepresentante.Items.Add(lstRep[i], i);
                    }
                    cmbRepresentante.SelectedIndex = 0;

                    _datSesion = new CPersonaSesion(Server.MapPath("~"), Request.ServerVariables["REMOTE_ADDR"]);
                    O_DETALLE_USUARIO_CTY resultado = _datSesion.ObtenerUsuario(Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]));

                    txtPadUsuEmisor.Text = resultado.NOMBRE + " " + resultado.PRIMER_APELLIDO + " " + resultado.SEGUNDO_APELLIDO;
                    txtPadUsuEmisorCi.Text = resultado.NUMERO_IDENTIFICACION + " " + resultado.EXPEDICION_ABREVIACION;

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

        protected void btnEmiteDeclaracion_Click(object sender, EventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    var strCodUnico = tlsOperadores.FocusedNode.GetValue("StrNroHydro").ToString();
                    if (strCodUnico.Trim().Length < 4 || strCodUnico.Contains("SIN LICENCIA"))
                    {
                        MPuImprimirMensaje("No puede emitir la declaración jurada de un proyecto que no cuenta con el código unico del sistema SIREHIDRO.", EnumTiposMensajeAlerta.Error);
                        return;
                    }

                    if (Session[CVariablesSesion.IdMovDdjj] == null)
                    {
                        MImprimirMensaje("Error al generar la declaración jurada", EnumTiposMensajeAlerta.Error);
                        return;
                    }

                    var idMovim = (decimal)Session[CVariablesSesion.IdMovDdjj];
                    Session.Remove(CVariablesSesion.IdMovDdjj);

                    var strMensaje = string.Empty;
                    if (MValidaDatos(ref strMensaje))
                    {
                        List<CMovAcumulado> lstGrilla;

                        lstGrilla = (List<CMovAcumulado>)Session[CVariablesSesion.GrillaAcumulado];
                        if (lstGrilla != null)
                        {
                            var datFechaReg = DateTime.Now;

                            var intIdAct = Convert.ToInt32(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());

                            if (intIdAct == 16)
                            {
                                var objNuevaDeclaracion = new EGuardarDeclaracionJurada();
                                objNuevaDeclaracion.decFechaRegistro = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                objNuevaDeclaracion.decIdCitePadre = 0;
                                objNuevaDeclaracion.decIdOpcion = 8;
                                objNuevaDeclaracion.decIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                objNuevaDeclaracion.strCodigoFormularioFac = tlsOperadores.FocusedNode.GetValue("StrNroHydro").ToString().Trim();
                                objNuevaDeclaracion.strCodigoFormularioMov = "UD/AB-RDJ-P01-F01";
                                objNuevaDeclaracion.decIdMaxMovimiento = idMovim;
                                objNuevaDeclaracion.strCredencial = CParametrosHydro.strCredencialFuncionario;
                                objNuevaDeclaracion.strCredencialFuncionario = CParametrosHydro.strCredencialDeclaracion;
                                objNuevaDeclaracion.strDpto = "";//MObtenerDeptoAbr(tlsOperadores.FocusedNode.GetValue("StrDepartamento").ToString());
                                objNuevaDeclaracion.strReferencia = "DECLARACION JURADA";
                                objNuevaDeclaracion.strSigla = "";//MObtenerSigla((int)tlsOperadores.FocusedNode.GetValue("IntIdActividad"));
                                objNuevaDeclaracion.cabecera = new O_LISTA_REPORTE_DATOSP_CTY();
                                var objCabecera = new O_LISTA_REPORTE_DATOSP_CTY();
                                objCabecera.ID_REPORTE_DATO_PRINC = 0;
                                objCabecera.FECHA_INICIO = Convert.ToInt64(detFechaIni.Date.Year + detFechaIni.Date.Month.ToString().PadLeft(2, '0') + detFechaIni.Date.Day.ToString().PadLeft(2, '0') + "000000");
                                objCabecera.FECHA_FIN = Convert.ToInt64(detFechaIni.Date.Year + detFechaIni.Date.Month.ToString().PadLeft(2, '0') + detFechaIni.Date.Day.ToString().PadLeft(2, '0') + "235959");
                                objCabecera.FECHA_REPORTE = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString().PadLeft(2, '0') + datFechaReg.Day.ToString().PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString().PadLeft(2, '0') + datFechaReg.Second.ToString().PadLeft(2, '0'));
                                objCabecera.ESTADO = 1;
                                objCabecera.RAZON_SOCIAL = tlsOperadores.FocusedNode.GetValue("StrDescripcion").ToString();

                                if (cmbRepresentante.Text.Length > 0)
                                {
                                    var strRep = cmbRepresentante.Text.Split('-');
                                    objCabecera.NOMBRES_REPLEGAL = strRep[0].Trim();
                                    objCabecera.CI_REPLEGAL = strRep[1].Trim();
                                }
                                else
                                {
                                    var strRep = txtPadPropietario.Text.Split('-');
                                    objCabecera.NOMBRES_REPLEGAL = strRep[0].Trim();
                                    objCabecera.CI_REPLEGAL = strRep[1].Trim();
                                }

                                objCabecera.NOMBRES_USUARIO = txtPadUsuEmisor.Text.Trim();
                                objCabecera.CI_USUARIO = txtPadUsuEmisorCi.Text;
                                objCabecera.TIPO_SOCIEDAD = (tlsOperadores.FocusedNode.GetValue("StrTipoSociedad") == null) ? "" : tlsOperadores.FocusedNode.GetValue("StrTipoSociedad").ToString();
                                objCabecera.OBSERVACIONES = txtPadObserv.Text.Trim();
                                objCabecera.ID_CONSUMIDOR = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString());
                                objCabecera.ID_CITE = 0;
                                objCabecera.ID_REPORTE_PRINCIPAL = 0;
                                objCabecera.APP_ID_USUARIO = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                objCabecera.APP_FECHA_REGISTRO = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                objCabecera.APP_ID_HISTORICO = 0;
                                objCabecera.DIRECCION = tlsOperadores.FocusedNode.GetValue("StrDireccion").ToString();
                                objCabecera.AMBITO_OPERACION = tlsOperadores.FocusedNode.GetValue("StrAmbito").ToString();
                                objCabecera.TELEFONO = tlsOperadores.FocusedNode.GetValue("StrTelefono").ToString();
                                var formatoFecha = CultureInfo.CurrentCulture.DateTimeFormat;
                                objCabecera.MES = formatoFecha.GetMonthName(Convert.ToInt32(detFechaIni.Date.Month.ToString().PadLeft(2, '0'))).ToUpper() + " - " + detFechaIni.Date.Year;
                                objCabecera.LUGAR_REGISTRO = cmbDepartamento.Text;
                                objCabecera.ID_TIPO_ACTIVIDAD = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                objCabecera.MUNICIPIO = tlsOperadores.FocusedNode.GetValue("StrMunicipio") + "/" + tlsOperadores.FocusedNode.GetValue("StrLocalidad");
                                objNuevaDeclaracion.cabecera = objCabecera;
                                objNuevaDeclaracion.listaDeclaracion = (O_DECLARACION_JURADA_CTY)Session[CVariablesSesion.DjAcumulado];

                                var resultado = _client.Post<EResultadoGenerico>(CAppSettings.svc_guardar_declaracion_jurada, objNuevaDeclaracion);
                                if (resultado.IntCodigo == 1)
                                {
                                    MImprimeDeclaracion(datFechaReg, resultado.StrMensaje.Trim());
                                    //MImprimeDeclaracion(datFechaReg, "numero 1");
                                    MLimpiaGrilla();
                                    pucDatos.ShowOnPageLoad = false;
                                }
                                else
                                {
                                    MPuImprimirMensaje(resultado.StrMensaje, EnumTiposMensajeAlerta.Error);
                                }

                            }
                            else
                            {
                                var objNuevaDeclaracion = new EGuardarDeclaracionJurada();
                                objNuevaDeclaracion.decFechaRegistro = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                objNuevaDeclaracion.decIdCitePadre = 0;
                                objNuevaDeclaracion.decIdOpcion = 8;
                                objNuevaDeclaracion.decIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                objNuevaDeclaracion.strCodigoFormularioFac = tlsOperadores.FocusedNode.GetValue("StrNroHydro").ToString().Trim();
                                objNuevaDeclaracion.strCodigoFormularioMov = "UD/AB-RDJ-P01-F01";
                                objNuevaDeclaracion.decIdMaxMovimiento = idMovim;
                                objNuevaDeclaracion.strCredencial = CParametrosHydro.strCredencialFuncionario;
                                objNuevaDeclaracion.strCredencialFuncionario = CParametrosHydro.strCredencialDeclaracion;
                                objNuevaDeclaracion.strDpto = "";//MObtenerDeptoAbr(tlsOperadores.FocusedNode.GetValue("StrDepartamento").ToString());
                                objNuevaDeclaracion.strReferencia = "DECLARACION JURADA";
                                objNuevaDeclaracion.strSigla = "";//MObtenerSigla((int)tlsOperadores.FocusedNode.GetValue("IntIdActividad"));
                                objNuevaDeclaracion.cabecera = new O_LISTA_REPORTE_DATOSP_CTY();
                                var objCabecera = new O_LISTA_REPORTE_DATOSP_CTY();
                                objCabecera.ID_REPORTE_DATO_PRINC = 0;
                                objCabecera.FECHA_INICIO = Convert.ToInt64(cmbPeriodo.Value + "01000000");
                                int intDia = DateTime.DaysInMonth(Convert.ToInt32(cmbPeriodo.Value.ToString().Substring(0, 4)), Convert.ToInt32(cmbPeriodo.Value.ToString().Substring(4, 2)));
                                objCabecera.FECHA_FIN = Convert.ToInt64(cmbPeriodo.Value + intDia.ToString().PadLeft(2, '0') + "235959");
                                objCabecera.FECHA_REPORTE = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString().PadLeft(2, '0') + datFechaReg.Day.ToString().PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString().PadLeft(2, '0') + datFechaReg.Second.ToString().PadLeft(2, '0'));
                                objCabecera.ESTADO = 1;
                                objCabecera.RAZON_SOCIAL = tlsOperadores.FocusedNode.GetValue("StrDescripcion").ToString();

                                if (cmbRepresentante.Text.Length > 0)
                                {
                                    var strRep = cmbRepresentante.Text.Split('-');
                                    objCabecera.NOMBRES_REPLEGAL = strRep[0].Trim();
                                    objCabecera.CI_REPLEGAL = strRep[1].Trim();
                                }
                                else
                                {
                                    var strRep = txtPadPropietario.Text.Split('-');
                                    objCabecera.NOMBRES_REPLEGAL = strRep[0].Trim();
                                    objCabecera.CI_REPLEGAL = strRep[1].Trim();
                                }

                                objCabecera.NOMBRES_USUARIO = txtPadUsuEmisor.Text.Trim();
                                objCabecera.CI_USUARIO = txtPadUsuEmisorCi.Text;
                                objCabecera.TIPO_SOCIEDAD = (tlsOperadores.FocusedNode.GetValue("StrTipoSociedad") == null) ? "" : tlsOperadores.FocusedNode.GetValue("StrTipoSociedad").ToString();
                                objCabecera.OBSERVACIONES = txtPadObserv.Text.Trim();
                                objCabecera.ID_CONSUMIDOR = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString());
                                objCabecera.ID_CITE = 0;
                                objCabecera.ID_REPORTE_PRINCIPAL = 0;
                                objCabecera.APP_ID_USUARIO = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                                objCabecera.APP_FECHA_REGISTRO = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                                objCabecera.APP_ID_HISTORICO = 0;
                                objCabecera.DIRECCION = tlsOperadores.FocusedNode.GetValue("StrDireccion").ToString();
                                objCabecera.AMBITO_OPERACION = tlsOperadores.FocusedNode.GetValue("StrAmbito").ToString();
                                objCabecera.TELEFONO = tlsOperadores.FocusedNode.GetValue("StrTelefono").ToString();
                                var formatoFecha = CultureInfo.CurrentCulture.DateTimeFormat;
                                objCabecera.MES = formatoFecha.GetMonthName(Convert.ToInt32(cmbPeriodo.Value.ToString().Substring(4, 2))).ToUpper() + " - " + cmbPeriodo.Value.ToString().Substring(0, 4);
                                objCabecera.LUGAR_REGISTRO = cmbDepartamento.Text;
                                objCabecera.ID_TIPO_ACTIVIDAD = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                                objCabecera.MUNICIPIO = tlsOperadores.FocusedNode.GetValue("StrMunicipio") + "/" + tlsOperadores.FocusedNode.GetValue("StrLocalidad");
                                objNuevaDeclaracion.cabecera = objCabecera;
                                objNuevaDeclaracion.listaDeclaracion = (O_DECLARACION_JURADA_CTY)Session[CVariablesSesion.DjAcumulado];
                                //var ojo = JsonSerializer.SerializeToString(objNuevaDeclaracion);
                                var resultado = _client.Post<EResultadoGenerico>(CAppSettings.svc_guardar_declaracion_jurada, objNuevaDeclaracion);
                                if (resultado.IntCodigo == 1)
                                {
                                    MImprimeDeclaracion(datFechaReg, resultado.StrMensaje.Trim());
                                    MLimpiaGrilla();
                                    pucDatos.ShowOnPageLoad = false;
                                }
                                else
                                {
                                    MPuImprimirMensaje(resultado.StrMensaje, EnumTiposMensajeAlerta.Error);
                                }
                            }


                        }
                        else
                        {
                            MPuImprimirMensaje("No existen registros.", EnumTiposMensajeAlerta.Alerta);
                        }
                    }
                    else
                    {
                        MPuImprimirMensaje(strMensaje, EnumTiposMensajeAlerta.Alerta);
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

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    if (tacMovimientos.ActiveTabIndex == 0)
                    {
                        pgeExportar.ASPxPivotGridID = "pvgAcumulado";
                        pgeExportar.ExportXlsxToResponse("DjAcumulado");
                    }
                    else
                    {
                        dgeExportar.GridViewID = "dgvInternacional";
                        dgeExportar.WriteXlsxToResponse();
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
                                                                      "/WFDeclaracionJurada.aspx?format=json");
                if (lstResultado.decCodigo == 1)
                {
                    if (lstResultado.oResultado != null && lstResultado.oResultado.Any())
                    {
                        Session[CVariablesSesion.PermisoAlta] = lstResultado.oResultado.FirstOrDefault().ALTAS;
                    }
                    else
                    {
                        Session[CVariablesSesion.PermisoAlta] = 0;
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
            btnEmiteDeclaracion.Visible = Convert.ToInt16(Session[CVariablesSesion.PermisoAlta]) != 0;
        }

        /// <summary>
        /// METODO. MValidaDatosIniciales
        /// </summary>
        /// <param name="strMensaje"></param>
        /// <returns></returns>
        private bool MValidaDatosIniciales(ref string strMensaje)
        {
            bool booResultado = true;

            var intIdAct = Convert.ToInt32(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());

            if (intIdAct == 16)
            {
                if (detFechaIni.Text == "")
                {
                    strMensaje = "Introduzca una fecha válida.";
                    booResultado = false;
                }
                else
                {
                    //todo No implementado.
                }
            }
            else
            {
                if (cmbPeriodo.Text == "")
                {
                    strMensaje = "Introduzca un periodo válido.";
                    booResultado = false;
                }
                else
                {
                    //todo No implementado.
                }
            }

            return booResultado;
        }

        /// <summary>
        /// METODO. MValidaCondicionesFuncionamiento
        /// </summary>
        /// <param name="strMensaje"></param>
        /// <returns></returns>
        private bool MValidaCondicionesFuncionamiento(ref string strMensaje)
        {
            bool booRespuesta = true;

            var intIdAct = Convert.ToInt32(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());

            if (intIdAct == 16)
            {
                var objParametros2 = _client.Post<EResultadoOctanoParametro>(CAppSettings.svc_listar_octano_parametrica,
                new
                {
                    strCredencial = CParametrosHydro.strCredencialFuncionario,
                    decFiltro = 1,//1: ID_TIPO_ACTIVIDAD 2: ID_TIPO_ACTIVIDAD y VALOR_TIPO
                    strParametro1 = "",//VALOR_TIPO
                    strParametro2 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
                    strParametro3 = "",
                    strParametro4 = ""
                });
                if (objParametros2.IntCodigo != 1)
                {
                    booRespuesta = false;
                    strMensaje = objParametros2.StrMensaje;
                }
                else
                {
                    Session[CVariablesSesion.OctanoParametrica] = objParametros2.OResultado;
                }

                var objTipoOperacion = _client.Post<EResultadoTipoOperacion>(CAppSettings.svc_obtener_tipos_operacion_act,
                            new
                            {
                                decFiltro = 3,
                                strParametro1 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
                                strParametro2 = tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString(),
                                strParametro3 = detFechaIni.Date.Year + detFechaIni.Date.Month.ToString().PadLeft(2, '0') + detFechaIni.Date.Day.ToString().PadLeft(2, '0') + "01000000",
                                strParametro4 = detFechaIni.Date.Year + detFechaIni.Date.Month.ToString().PadLeft(2, '0') + detFechaIni.Date.Day.ToString().PadLeft(2, '0') + "235959"
                            });
                if (objTipoOperacion.IntCodigo == 1)
                {
                    Session[CVariablesSesion.ComboTiposOperacion] = objTipoOperacion.OResultado;
                }
                else
                {
                    booRespuesta = false;
                    strMensaje = objTipoOperacion.StrMensaje + " - V3";
                }
            }
            else
            {
                string strUltimoDia = DateTime.DaysInMonth(Convert.ToInt32(cmbPeriodo.Value.ToString().Substring(0, 4)), Convert.ToInt32(cmbPeriodo.Value.ToString().Substring(4, 2))).ToString();

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
                if (objParametros.IntCodigo != 1)
                {
                    booRespuesta = false;
                    strMensaje = objParametros.StrMensaje;
                }
                else
                {
                    Session[CVariablesSesion.OctanoParametrica] = objParametros.OResultado;

                    //Valida si existe DDJJ en periodo
                    var objValidacion = _client.Post<EResultadoGenerico>(CAppSettings.svc_validaciones_general,
                    new
                    {
                        strCredencial = CParametrosHydro.strCredencialFuncionario,
                        strFiltro = "1",
                        strParametro1 = tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString(),
                        strParametro2 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
                        strParametro3 = "",
                        strParametro4 = cmbPeriodo.Value + "01000000",
                        strParametro5 = cmbPeriodo.Value + strUltimoDia + "235959",
                        strParametro6 = "",
                        decAppFechaRegistro = 0,
                        decAppIdUsuario = 0
                    });
                    if (objValidacion.IntCodigo == 1)
                    {
                        if (objValidacion.OResultado.RESULTADO == 1)
                        {
                            //tipos de operacion
                            var objTipoOperacion = _client.Post<EResultadoTipoOperacion>(CAppSettings.svc_obtener_tipos_operacion_act,
                            new
                            {
                                decFiltro = 3,
                                strParametro1 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
                                strParametro2 = tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString(),
                                strParametro3 = cmbPeriodo.Value + "01000000",
                                strParametro4 = cmbPeriodo.Value + strUltimoDia + "235959"
                            });
                            if (objTipoOperacion.IntCodigo == 1)
                            {
                                Session[CVariablesSesion.ComboTiposOperacion] = objTipoOperacion.OResultado;
                            }
                            else
                            {
                                booRespuesta = false;
                                strMensaje = objTipoOperacion.StrMensaje + " - V3";
                            }
                        }
                        else
                        {
                            booRespuesta = false;
                            strMensaje = objValidacion.OResultado.MENSAJE_ERROR + " - V2";

                            //todo correr procedimiento de duplicadas B-SISA
                            //Baja logica  los movimientos duplicados por anulacion B-Sisa
                            //var objResultado = _client.Post<EPResultado>(CAppSettings.svc_verifica_tran_duplicada,
                            //new
                            //{
                            //    strCredencial = CParametrosHydro.strCredencialFuncionario,
                            //    decFiltro = 1,
                            //    decIdConsumidor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString()),
                            //    decIdTipoActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()),
                            //    decFechaHoraInicio = Convert.ToDecimal(cmbPeriodo.Value + "01000000"),
                            //    decFechaHoraFin = Convert.ToDecimal(cmbPeriodo.Value + strUltimoDia + "235959")
                            //});
                            //if (objResultado.intCodigo != 1)
                            //{
                            //    MImprimirMensaje(objResultado.strMensaje, EnumTiposMensajeAlerta.Error);
                            //}
                            //else
                            //{
                            //    //todo No implementado
                            //}
                        }
                    }
                    else
                    {
                        booRespuesta = false;
                        strMensaje = objValidacion.StrMensaje;
                    }
                }

                #region validaciones original

                ////Obtener condiciones de uso
                //var objParametros = _client.Post<EResultadoOctanoParametro>(CAppSettings.svc_listar_octano_parametrica,
                //new
                //{
                //    strCredencial = CParametrosHydro.strCredencialFuncionario,
                //    decFiltro = 1,//1: ID_TIPO_ACTIVIDAD 2: ID_TIPO_ACTIVIDAD y VALOR_TIPO
                //    strParametro1 = "",//VALOR_TIPO
                //    strParametro2 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
                //    strParametro3 = "",
                //    strParametro4 = ""
                //});
                //if (objParametros.IntCodigo != 1)
                //{
                //    booRespuesta = false;
                //    strMensaje = objParametros.StrMensaje;
                //}
                //else
                //{
                //    Session[CVariablesSesion.OctanoParametrica] = objParametros.OResultado;

                //    //Valida si existe DDJJ en periodo
                //    var objValidacion = _client.Post<EResultadoGenerico>(CAppSettings.svc_validaciones_general,
                //    new
                //    {
                //        strCredencial = CParametrosHydro.strCredencialFuncionario,
                //        strFiltro = "1",
                //        strParametro1 = tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString(),
                //        strParametro2 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
                //        strParametro3 = "",
                //        strParametro4 = cmbPeriodo.Value + "01000000",
                //        strParametro5 = cmbPeriodo.Value + strUltimoDia + "235959",
                //        strParametro6 = "",
                //        decAppFechaRegistro = 0,
                //        decAppIdUsuario = 0
                //    });
                //    if (objValidacion.IntCodigo == 1)
                //    {
                //        if (objValidacion.OResultado.RESULTADO == 1)
                //        {
                //            //todo revisar procedimiento
                //            //Baja logica  los movimientos duplicados por anulacion B-Sisa
                //            //var objResultado = _client.Post<EPResultado>(CAppSettings.svc_verifica_tran_duplicada,
                //            //new
                //            //{
                //            //    strCredencial = CParametrosHydro.strCredencialFuncionario,
                //            //    decFiltro = 1,
                //            //    decIdConsumidor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString()),
                //            //    decIdTipoActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()),
                //            //    decFechaHoraInicio = Convert.ToDecimal(cmbPeriodo.Value + "01000000"),
                //            //    decFechaHoraFin = Convert.ToDecimal(cmbPeriodo.Value + strUltimoDia + "235959")
                //            //});
                //            //if (objResultado.intCodigo != 1)
                //            //{
                //            //    MImprimirMensaje(objResultado.strMensaje, EnumTiposMensajeAlerta.Error);
                //            //}
                //            //else
                //            //{
                //            //    //todo No implementado
                //            //}

                //            //todo revisar validacion
                //            //Valida si el periodo esta habilitado para cerrar (si el periodo anterior esta cerrado o es el periodo inicial)
                //            //objValidacion = _client.Post<EResultadoGenerico>(CAppSettings.svc_validaciones_general,
                //            //new
                //            //{
                //            //    strCredencial = CParametrosHydro.strCredencialFuncionario,
                //            //    strFiltro = "3",
                //            //    strParametro1 = tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString(),
                //            //    strParametro2 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
                //            //    strParametro3 = "",
                //            //    strParametro4 = cmbPeriodo.Value + "01",
                //            //    strParametro5 = cmbPeriodo.Value + strUltimoDia,
                //            //    strParametro6 = "",
                //            //    decAppFechaRegistro = 0,
                //            //    decAppIdUsuario = 0
                //            //});
                //            //if (objValidacion.IntCodigo == 1)
                //            if (1 == 1)
                //            {
                //                //if (objValidacion.OResultado.RESULTADO == 1)
                //                if (1 == 1)
                //                {
                //                    //Valida si todos los periodos anteriores estan cerrados no existan periodos anulados sin declarar (1 año)
                //                    objValidacion = _client.Post<EResultadoGenerico>(CAppSettings.svc_validaciones_general,
                //                    new
                //                    {
                //                        strCredencial = CParametrosHydro.strCredencialFuncionario,
                //                        strFiltro = "4",
                //                        strParametro1 = tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString(),
                //                        strParametro2 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
                //                        strParametro3 = "",
                //                        strParametro4 = cmbPeriodo.Value + "01",
                //                        strParametro5 = cmbPeriodo.Value + strUltimoDia,
                //                        strParametro6 = "",
                //                        decAppFechaRegistro = 0,
                //                        decAppIdUsuario = 0
                //                    });
                //                    if (objValidacion.IntCodigo == 1)
                //                    {
                //                        if (objValidacion.OResultado.RESULTADO == 1)
                //                        {
                //                            //Valida si existen facturas duplicadas
                //                            //objValidacion = _client.Post<EResultadoGenerico>(CAppSettings.svc_validaciones_general,
                //                            //new
                //                            //{
                //                            //    strCredencial = CParametrosHydro.strCredencialFuncionario,
                //                            //    strFiltro = "5",
                //                            //    strParametro1 = tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString(),
                //                            //    strParametro2 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
                //                            //    strParametro3 = "",
                //                            //    strParametro4 = cmbPeriodo.Value + "01",
                //                            //    strParametro5 = cmbPeriodo.Value + strUltimoDia,
                //                            //    strParametro6 = "",
                //                            //    decAppFechaRegistro = 0,
                //                            //    decAppIdUsuario = 0
                //                            //});
                //                            //if (objValidacion.IntCodigo == 1)
                //                            //{
                //                            //    if (objValidacion.OResultado.RESULTADO != 1)
                //                            //    {
                //                            //        MImprimirMensaje(objValidacion.OResultado.MENSAJE_ERROR + " - F5", EnumTiposMensajeAlerta.Alerta);
                //                            //    }
                //                            //    else
                //                            //    {
                //                            //        //todo No implementado
                //                            //    }
                //                            //}
                //                            //else
                //                            //{
                //                            //    booRespuesta = false;
                //                            //    strMensaje = objValidacion.StrMensaje;
                //                            //}
                //                        }
                //                        else
                //                        {
                //                            booRespuesta = false;
                //                            strMensaje = objValidacion.OResultado.MENSAJE_ERROR + " - F4";
                //                        }
                //                    }
                //                    else
                //                    {
                //                        booRespuesta = false;
                //                        strMensaje = objValidacion.StrMensaje;
                //                    }
                //                }
                //                else
                //                {
                //                    booRespuesta = false;
                //                    strMensaje = objValidacion.OResultado.MENSAJE_ERROR + " - F3";
                //                }
                //            }
                //            else
                //            {
                //                booRespuesta = false;
                //                strMensaje = objValidacion.StrMensaje;
                //            }
                //        }
                //        else
                //        {
                //            booRespuesta = false;
                //            strMensaje = objValidacion.OResultado.MENSAJE_ERROR + " - F1";
                //        }
                //    }
                //    else
                //    {
                //        booRespuesta = false;
                //        strMensaje = objValidacion.StrMensaje;
                //    }
                //}
                #endregion
            }

            return booRespuesta;
        }

        /// <summary>
        /// METODO. MCargaParametricas
        /// </summary>
        private void MCargaParametricas()
        {
            var objUbicacion = _client.Post<EResultadoUbicacion>(CAppSettings.svc_obtiene_ubicacion,
            new
            {
                strCredencial = CParametrosHydro.strCredencialFuncionario,
                decNivel = 2,
                decIdPadre = 120
            });
            if (objUbicacion.IntCodigo == 1)
            {
                Session[CVariablesSesion.ListaParamGral] = objUbicacion.OResultado;
            }
            else
            {
                Session[CVariablesSesion.ListaParamGral] = null;
            }
        }

        /// <summary>
        /// METODO. MValidaDatos
        /// </summary>
        /// <param name="strMensaje"></param>
        /// <returns></returns>
        private bool MValidaDatos(ref string strMensaje)
        {
            var booResultado = true;

            var intIdAct = Convert.ToInt32(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());

            if (intIdAct == 16)
            {
                var lstMov = Session[CVariablesSesion.GrillaAcumulado] as List<CMovAcumulado>;
                if (lstMov == null || !lstMov.Any())
                {
                    strMensaje = "No existen registros.";
                    booResultado = false;
                }
            }
            else
            {
                //Valida datos basicos
                var lstMov = Session[CVariablesSesion.GrillaAcumulado] as List<CMovAcumulado>;
                if (lstMov == null || !lstMov.Any())
                {
                    strMensaje = "No existen registros.";
                    booResultado = false;
                }
                else if (cmbPeriodo.Text.Trim() == Session[CVariablesSesion.IdPeriodoActual].ToString())
                {
                    strMensaje = "No puede emitir una Declaracion Jurada de un periodo en curso.";
                    booResultado = false;
                }
                else if (cmbDepartamento.Text == "")
                {
                    strMensaje = "Introduzca el lugar.";
                    booResultado = false;
                }
                else if (cmbRepresentante.Text == "" && txtPadPropietario.Text.Trim().Length < 3)
                {
                    strMensaje = "Representante legal/Propietario no definido.";
                    booResultado = false;
                }
                else if (txtPadUsuEmisor.Text == "0")
                {
                    strMensaje = "Usuario emisor no definido.";
                    booResultado = false;
                }
                else if (txtPadUsuEmisorCi.Text == "")
                {
                    strMensaje = "Carnet de identidad del usuario emisor no definido.";
                    booResultado = false;
                }

                if (booResultado)
                {
                    //Valida si existen saldos negativos
                    var lstMovimientos = Session[CVariablesSesion.GrillaAcumulado] as List<CMovAcumulado>;
                    lstMovimientos = lstMovimientos.Where(x => x.DecSaldoFinal < 0).ToList();

                    if (!(Convert.ToInt32(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()) == (int)EnumActividades.EstacionServicioGnv || Convert.ToInt32(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()) == (int)EnumActividades.DistribucionGasRedes) && lstMovimientos.Any())
                    {
                        strMensaje = "No puede emitir una Declaración Jurada con SALDOS NEGATIVOS. Revise su información.";
                        booResultado = false;
                    }
                    else
                    {
                        var strUltimoDia = DateTime.DaysInMonth(Convert.ToInt32(cmbPeriodo.Value.ToString().Substring(0, 4)), Convert.ToInt32(cmbPeriodo.Value.ToString().Substring(4, 2))).ToString();
                        var decFechaFin = Convert.ToDecimal(cmbPeriodo.Value + strUltimoDia);
                        var objDdjj = (O_DECLARACION_JURADA_CTY)Session[CVariablesSesion.DjAcumulado];
                        var lstAcum = objDdjj.LstAcumulado;
                        lstAcum = lstAcum.Where(x => x.DecFecha == decFechaFin).ToList();

                        //Valida saldo final del periodo vs saldo inicial del periodo siguiente (si existe)
                        //Validad correlatividad de declaraciones juradas.      
                        var objValidacion = _cUtil.MValidacionDdjj(1, tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(), tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString(), cmbPeriodo.Value + "01000000", lstAcum);

                        if (objValidacion.intCodigo != 1)
                        {
                            strMensaje = objValidacion.strMensaje;
                            booResultado = false;
                        }
                    }
                }
            }


            return booResultado;
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
        /// METODO. MCargaGrilla
        /// </summary>
        /// <param name="decFiltro"></param>
        /// <param name="decIdConsumidor"></param>
        /// <param name="decIdTipoActividad"></param>
        /// <param name="decFechaDiaInicio"></param>
        /// <param name="decFechaDiaFin"></param>
        private void MCargaGrilla(decimal decFiltro, decimal decIdConsumidor, decimal decIdTipoActividad, decimal decFechaDiaInicio, decimal decFechaDiaFin)
        {
            var lstTipoOperacion = Session[CVariablesSesion.ComboTiposOperacion] as List<O_TIPOS_OPE_ACT_CTY>;

            if (decIdTipoActividad == 16)
            {
                #region para importadores

                decFiltro = 6;
                var lstResultadoRes107 = new List<CDjAcumulado>();
                var lstResultadoRes48 = new List<CDjAcumulado>();

                var oTiposOpeActCtyImp = lstTipoOperacion.Where(x => x.NOMBRE.Contains("IMPORTAC")).ToList().FirstOrDefault();
                var decidTipoOpImp = (oTiposOpeActCtyImp != null) ? oTiposOpeActCtyImp.ID_TIPO_OPERACION : -99999;

                var strCite = cmbListadoDui.Text;

                Session.Remove(CVariablesSesion.IdMovDdjj);

                var lstDespachos = _cUtil.MObtieneMovAcumulado(decFiltro, strCite.ToString(), decIdConsumidor.ToString(), decFechaDiaInicio.ToString(), decFechaDiaFin.ToString(), 1, decidTipoOpImp);
                if (lstDespachos.intCodigo == 1)
                {
                    Session[CVariablesSesion.IdMovDdjj] = Convert.ToDecimal(lstDespachos.strMensaje);

                    long lngIdMovimiento;
                    var lstResultadoLstAcumulado = (O_DECLARACION_JURADA_CTY)lstDespachos.oResultado;
                    Session[CVariablesSesion.DjAcumulado] = lstResultadoLstAcumulado;
                    var lstResultado = lstResultadoLstAcumulado.LstAcumulado;

                    lstResultado.ForEach(a => a.DecIdUnicoHidrocarburo = Convert.ToDecimal(a.DecIdHidrocarburo.ToString() + ((a.DecIdTipoInfraestructura == null) ? "" : a.DecIdTipoInfraestructura.ToString().Replace("-", "")) + ((a.DecIdInfraestructura == null) ? "" : a.DecIdInfraestructura.ToString().Replace("-", ""))));
                    var lstResAdic1 = lstResultadoLstAcumulado.LstaAdicional1;
                    var lstResAdic2 = lstResultadoLstAcumulado.LstaAdicional2;
                    var lstRecaudos = lstResultadoLstAcumulado.LstaRecaudoExt;

                    foreach (var dato in lstResultado)
                    {
                        int intProducto = Convert.ToInt32(dato.DecIdHidrocarburo);
                        switch (intProducto)
                        {
                            case 278:
                            case 401://test 401 desarrollo 4326 produccion 1829
                            case 402://test 402 desarrollo 4327 produccion 1830
                            case 403://test 403 desarrollo 4328 produccion 1831
                            case 404://test 404 desarrollo 4329 produccion 1832
                            case 405://test 405 desarrollo 4330 produccion 1833
                                lstResultadoRes107.Add(dato);
                                break;

                            case 367: //test 367 desarrollo 367 produccion 367
                            case 368: //test 368 desarrollo 368 produccion 368
                            case 369: //test 369 desarrollo 369 produccion 369
                            case 370: //test 370 desarrollo 370 produccion 370
                            case 371: //test 371 desarrollo 371 produccion 371
                            case 372: //test 372 desarrollo 372 produccion 372
                            case 374: //test 374 desarrollo 374 produccion 373
                            case 380: //test 380 desarrollo 380 produccion 374
                            case 382: //test 382 desarrollo 382 produccion 375
                            case 383: //test 383 desarrollo 383 produccion 376
                            case 384: //test 384 desarrollo 384 produccion 377
                            case 385: //test 385 desarrollo 385 produccion 378
                            case 386: //test 386 desarrollo 386 produccion 379
                            case 387: //test 387 desarrollo 387 produccion 380
                            case 388: //test 388 desarrollo 388 produccion 381
                            case 389: //test 389 desarrollo 389 produccion 382
                                lstResultadoRes48.Add(dato);
                                break;

                            default:
                                break;
                        }
                    }

                    int opcionRes = Convert.ToInt32(cmbResMin.Value);

                    if (opcionRes == 1)
                    {
                        if (lstResultadoRes48.Any())
                        {
                            #region acumulado

                            decimal decProd = 0;
                            decimal decGrProd = 0;
                            decimal decGrupoProd = 0;
                            int cont = 1, intContProd = 1, IntContAgrup = 0;

                            lstResultadoRes48 = lstResultadoRes48.OrderBy(x => x.DecIdHidrocarburo).ThenBy(x => x.StrTipoInfraestructura).ThenBy(x => x.StrInfraestructura).ThenBy(x => x.DecFecha).ToList();
                            var lstAcumRep = new List<CMovAcumuladoRep>();
                            var lstMovimientos = new List<CMovAcumulado>();
                            foreach (var lst in lstResultadoRes48)
                            {
                                var cDato = new CMovAcumulado();
                                var cDatR = new CMovAcumuladoRep();
                                if (decProd != lst.DecIdUnicoHidrocarburo)
                                {
                                    decProd = lst.DecIdUnicoHidrocarburo;
                                    intContProd = 1;
                                    IntContAgrup++;
                                    decGrupoProd++;
                                }
                                if (decGrProd != lst.DecIdHidrocarburo)
                                {
                                    decGrProd = lst.DecIdHidrocarburo;
                                    decGrupoProd = 1;
                                }

                                cDato.IntId = cont;
                                cDato.IntContProd = intContProd;
                                cDato.StrFecha = lst.DecFecha.ToString().Substring(6, 2) + "/" + lst.DecFecha.ToString().Substring(4, 2) + "/" + lst.DecFecha.ToString().Substring(0, 4);
                                cDato.IntIdProducto = (int)lst.DecIdHidrocarburo;
                                cDato.IntContAgrup = IntContAgrup;
                                cDato.StrProducto = lst.StrHidrocarburo;
                                cDato.StrCodProducto = lst.StrCodHidrocarburo;
                                cDato.StrUnidadMedida = lst.StrUnidadMedida;
                                cDato.StrTipoInfraestructura = lst.StrTipoInfraestructura ?? "";
                                cDato.StrInfraestructura = lst.StrInfraestructura ?? "";
                                cDato.StrCodigoInfraestructura = lst.StrInfraestructuraAnh ?? "";
                                cDato.DecSaldoInicial = lst.DecSaldoInicial;
                                cDato.DecTotalIng = lst.DecTotalIngreso;
                                cDato.DecSaldoFinal = lst.DecSaldoFinal;
                                cDato.DecTotalSal = lst.DecTotalEgreso;

                                cDatR.StrFecha = cDato.StrFecha;
                                cDatR.IntIdProducto = cDato.IntIdProducto;
                                cDatR.StrProducto = cDato.StrProducto;
                                cDatR.IntGrupoProducto = Convert.ToInt32(decGrupoProd);
                                cDatR.StrUnidadMedida = cDato.StrUnidadMedida;
                                cDatR.StrInfraestructura = cDato.StrTipoInfraestructura + " - " + cDato.StrInfraestructura;
                                cDatR.DecSaldoInicial = cDato.DecSaldoInicial;
                                cDatR.DecTotalIng = cDato.DecTotalIng;
                                cDatR.DecSaldoFinal = cDato.DecSaldoFinal;
                                cDatR.DecTotalSal = cDato.DecTotalSal;

                                if (lst.LstIngresos == null || !lst.LstIngresos.Any())
                                {
                                    cDato.DecIng01 = -99999;
                                }
                                else
                                {
                                    foreach (var reg in lst.LstIngresos)
                                    {
                                        if ((int)reg.OPERACION_ORDEN > 0)
                                        {
                                            if (!pvgAcumulado.Fields[6 + (int)reg.OPERACION_ORDEN].Visible)
                                            {
                                                pvgAcumulado.Fields[6 + (int)reg.OPERACION_ORDEN].Caption = reg.OPERACION_NOMBRE;
                                                pvgAcumulado.Fields[6 + (int)reg.OPERACION_ORDEN].Visible = true;
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

                                            cDatR.DecIng01 += (reg.REPORTE_ORDEN == 1) ? reg.CANTIDAD : 0;
                                            cDatR.DecIng02 += (reg.REPORTE_ORDEN == 2) ? reg.CANTIDAD : 0;
                                            cDatR.DecIng03 += (reg.REPORTE_ORDEN == 3) ? reg.CANTIDAD : 0;
                                            cDatR.DecIng04 += (reg.REPORTE_ORDEN == 4) ? reg.CANTIDAD : 0;
                                            cDatR.DecIng05 += (reg.REPORTE_ORDEN == 5) ? reg.CANTIDAD : 0;

                                            cDatR.StrTitIng01 = (reg.REPORTE_ORDEN == 1) ? reg.REPORTE_NOMBRE : cDatR.StrTitIng01;
                                            cDatR.StrTitIng02 = (reg.REPORTE_ORDEN == 2) ? reg.REPORTE_NOMBRE : cDatR.StrTitIng02;
                                            cDatR.StrTitIng03 = (reg.REPORTE_ORDEN == 3) ? reg.REPORTE_NOMBRE : cDatR.StrTitIng03;
                                            cDatR.StrTitIng04 = (reg.REPORTE_ORDEN == 4) ? reg.REPORTE_NOMBRE : cDatR.StrTitIng04;
                                            cDatR.StrTitIng05 = (reg.REPORTE_ORDEN == 5) ? reg.REPORTE_NOMBRE : cDatR.StrTitIng05;
                                        }
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
                                        if ((int)reg.OPERACION_ORDEN > 0)
                                        {
                                            if (!pvgAcumulado.Fields[27 + (int)reg.OPERACION_ORDEN].Visible)
                                            {
                                                pvgAcumulado.Fields[27 + (int)reg.OPERACION_ORDEN].Caption = reg.OPERACION_NOMBRE;
                                                pvgAcumulado.Fields[27 + (int)reg.OPERACION_ORDEN].Visible = true;
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

                                            cDatR.DecSal01 += (reg.REPORTE_ORDEN == 1) ? reg.CANTIDAD : 0;
                                            cDatR.DecSal02 += (reg.REPORTE_ORDEN == 2) ? reg.CANTIDAD : 0;
                                            cDatR.DecSal03 += (reg.REPORTE_ORDEN == 3) ? reg.CANTIDAD : 0;
                                            cDatR.DecSal04 += (reg.REPORTE_ORDEN == 4) ? reg.CANTIDAD : 0;
                                            cDatR.DecSal05 += (reg.REPORTE_ORDEN == 5) ? reg.CANTIDAD : 0;

                                            cDatR.StrTitSal01 = (reg.REPORTE_ORDEN == 1) ? reg.REPORTE_NOMBRE : cDatR.StrTitSal01;
                                            cDatR.StrTitSal02 = (reg.REPORTE_ORDEN == 2) ? reg.REPORTE_NOMBRE : cDatR.StrTitSal02;
                                            cDatR.StrTitSal03 = (reg.REPORTE_ORDEN == 3) ? reg.REPORTE_NOMBRE : cDatR.StrTitSal03;
                                            cDatR.StrTitSal04 = (reg.REPORTE_ORDEN == 4) ? reg.REPORTE_NOMBRE : cDatR.StrTitSal04;
                                            cDatR.StrTitSal05 = (reg.REPORTE_ORDEN == 5) ? reg.REPORTE_NOMBRE : cDatR.StrTitSal05;
                                        }
                                    }
                                }
                                lstMovimientos.Add(cDato);
                                lstAcumRep.Add(cDatR);
                                cont++;
                                intContProd++;
                            }

                            var intIdAct = Convert.ToInt32(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());

                            string strFechaI;
                            string strFechaF;

                            if (intIdAct == 16)
                            {
                                strFechaI = detFechaIni.Text;
                                strFechaF = detFechaIni.Text;
                            }
                            else
                            {
                                int intDia = DateTime.DaysInMonth(Convert.ToInt32(cmbPeriodo.Value.ToString().Substring(0, 4)), Convert.ToInt32(cmbPeriodo.Value.ToString().Substring(4, 2)));
                                strFechaI = "01/" + cmbPeriodo.Value.ToString().Substring(4, 2) + "/" + cmbPeriodo.Value.ToString().Substring(0, 4);
                                strFechaF = intDia.ToString().PadLeft(2, '0') + "/" + cmbPeriodo.Value.ToString().Substring(4, 2) + "/" + cmbPeriodo.Value.ToString().Substring(0, 4);
                            }

                            lstMovimientos.ForEach(a => a.DecSaldoIni = (a.StrFecha == strFechaI) ? a.DecSaldoInicial : 0);
                            lstMovimientos.ForEach(a => a.DecSaldoFin = (a.StrFecha == strFechaF) ? a.DecSaldoFinal : 0);

                            lstAcumRep.ForEach(a => a.DecSaldoIni = (a.StrFecha == strFechaI) ? a.DecSaldoInicial : 0);
                            lstAcumRep.ForEach(a => a.DecSaldoFin = (a.StrFecha == strFechaF) ? a.DecSaldoFinal : 0);

                            Session[CVariablesSesion.GrillaAcumulado] = lstMovimientos;

                            var objParametricas = Session[CVariablesSesion.OctanoParametrica] as List<O_LISTA_OCTANO_PAR_CTY>;
                            var paramAgrup = objParametricas.Where(x => x.TIPO_VALOR == 10).ToList().FirstOrDefault();

                            if (paramAgrup.VALOR_FIN == "1")
                            {
                                var lstNuevo = lstAcumRep.GroupBy(n => new { n.StrFecha, n.IntIdProducto, n.StrProducto, n.StrUnidadMedida }).
                                           Select(group =>
                                           new
                                           {
                                               group.Key.StrFecha,
                                               IntGrupoProducto = 0,
                                               group.Key.IntIdProducto,
                                               group.Key.StrProducto,
                                               group.Key.StrUnidadMedida,
                                               IntIdInfraestructura = 0,
                                               StrInfraestructura = "",
                                               DecSaldoIni = group.Sum(c => c.DecSaldoIni),
                                               DecSaldoInicial = group.Sum(c => c.DecSaldoInicial),
                                               DecIng01 = group.Sum(c => c.DecIng01),
                                               DecIng02 = group.Sum(c => c.DecIng02),
                                               DecIng03 = group.Sum(c => c.DecIng03),
                                               DecIng04 = group.Sum(c => c.DecIng04),
                                               DecIng05 = group.Sum(c => c.DecIng05),
                                               DecTotalIng = group.Sum(c => c.DecTotalIng),
                                               DecSal01 = group.Sum(c => c.DecSal01),
                                               DecSal02 = group.Sum(c => c.DecSal02),
                                               DecSal03 = group.Sum(c => c.DecSal03),
                                               DecSal04 = group.Sum(c => c.DecSal04),
                                               DecSal05 = group.Sum(c => c.DecSal05),
                                               DecTotalSal = group.Sum(c => c.DecTotalSal),
                                               DecSaldoFinal = group.Sum(c => c.DecSaldoFinal),
                                               DecSaldoFin = group.Sum(c => c.DecSaldoFin),
                                               StrTitIng01 = group.Max(c => c.StrTitIng01),
                                               StrTitIng02 = group.Max(c => c.StrTitIng02),
                                               StrTitIng03 = group.Max(c => c.StrTitIng03),
                                               StrTitIng04 = group.Max(c => c.StrTitIng04),
                                               StrTitIng05 = group.Max(c => c.StrTitIng05),
                                               StrTitSal01 = group.Max(c => c.StrTitSal01),
                                               StrTitSal02 = group.Max(c => c.StrTitSal02),
                                               StrTitSal03 = group.Max(c => c.StrTitSal03),
                                               StrTitSal04 = group.Max(c => c.StrTitSal04),
                                               StrTitSal05 = group.Max(c => c.StrTitSal05)
                                           }).ToList();
                                Session[CVariablesSesion.GrillaAcumRep] = lstNuevo;
                            }
                            else
                            {
                                Session[CVariablesSesion.GrillaAcumRep] = lstAcumRep;
                            }

                            pvgAcumulado.DataSource = lstMovimientos;
                            pvgAcumulado.DataBind();

                            var pvaArea = PivotArea.DataArea;
                            for (int i = 6; i < 50; i++)
                            {
                                if (pvgAcumulado.Fields[i].Visible)
                                {
                                    pvgAcumulado.Fields[i].SetAreaPosition(pvaArea, i);
                                }
                            }

                            #endregion
                        }
                    }
                    else if (opcionRes == 2)
                    {
                        if (lstResultadoRes107.Any())
                        {
                            #region acumulado

                            decimal decProd = 0;
                            decimal decGrProd = 0;
                            decimal decGrupoProd = 0;
                            int cont = 1, intContProd = 1, IntContAgrup = 0;

                            lstResultadoRes107 = lstResultadoRes107.OrderBy(x => x.DecIdHidrocarburo).ThenBy(x => x.StrTipoInfraestructura).ThenBy(x => x.StrInfraestructura).ThenBy(x => x.DecFecha).ToList();
                            var lstAcumRep = new List<CMovAcumuladoRep>();
                            var lstMovimientos = new List<CMovAcumulado>();
                            foreach (var lst in lstResultadoRes107)
                            {
                                var cDato = new CMovAcumulado();
                                var cDatR = new CMovAcumuladoRep();
                                if (decProd != lst.DecIdUnicoHidrocarburo)
                                {
                                    decProd = lst.DecIdUnicoHidrocarburo;
                                    intContProd = 1;
                                    IntContAgrup++;
                                    decGrupoProd++;
                                }
                                if (decGrProd != lst.DecIdHidrocarburo)
                                {
                                    decGrProd = lst.DecIdHidrocarburo;
                                    decGrupoProd = 1;
                                }

                                cDato.IntId = cont;
                                cDato.IntContProd = intContProd;
                                cDato.StrFecha = lst.DecFecha.ToString().Substring(6, 2) + "/" + lst.DecFecha.ToString().Substring(4, 2) + "/" + lst.DecFecha.ToString().Substring(0, 4);
                                cDato.IntIdProducto = (int)lst.DecIdHidrocarburo;
                                cDato.IntContAgrup = IntContAgrup;
                                cDato.StrProducto = lst.StrHidrocarburo;
                                cDato.StrCodProducto = lst.StrCodHidrocarburo;
                                cDato.StrUnidadMedida = lst.StrUnidadMedida;
                                cDato.StrTipoInfraestructura = lst.StrTipoInfraestructura ?? "";
                                cDato.StrInfraestructura = lst.StrInfraestructura ?? "";
                                cDato.StrCodigoInfraestructura = lst.StrInfraestructuraAnh ?? "";
                                cDato.DecSaldoInicial = lst.DecSaldoInicial;
                                cDato.DecTotalIng = lst.DecTotalIngreso;
                                cDato.DecSaldoFinal = lst.DecSaldoFinal;
                                cDato.DecTotalSal = lst.DecTotalEgreso;

                                cDatR.StrFecha = cDato.StrFecha;
                                cDatR.IntIdProducto = cDato.IntIdProducto;
                                cDatR.StrProducto = cDato.StrProducto;
                                cDatR.IntGrupoProducto = Convert.ToInt32(decGrupoProd);
                                cDatR.StrUnidadMedida = cDato.StrUnidadMedida;
                                cDatR.StrInfraestructura = cDato.StrTipoInfraestructura + " - " + cDato.StrInfraestructura;
                                cDatR.DecSaldoInicial = cDato.DecSaldoInicial;
                                cDatR.DecTotalIng = cDato.DecTotalIng;
                                cDatR.DecSaldoFinal = cDato.DecSaldoFinal;
                                cDatR.DecTotalSal = cDato.DecTotalSal;

                                if (lst.LstIngresos == null || !lst.LstIngresos.Any())
                                {
                                    cDato.DecIng01 = -99999;
                                }
                                else
                                {
                                    foreach (var reg in lst.LstIngresos)
                                    {
                                        if ((int)reg.OPERACION_ORDEN > 0)
                                        {
                                            if (!pvgAcumulado.Fields[6 + (int)reg.OPERACION_ORDEN].Visible)
                                            {
                                                pvgAcumulado.Fields[6 + (int)reg.OPERACION_ORDEN].Caption = reg.OPERACION_NOMBRE;
                                                pvgAcumulado.Fields[6 + (int)reg.OPERACION_ORDEN].Visible = true;
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

                                            cDatR.DecIng01 += (reg.REPORTE_ORDEN == 1) ? reg.CANTIDAD : 0;
                                            cDatR.DecIng02 += (reg.REPORTE_ORDEN == 2) ? reg.CANTIDAD : 0;
                                            cDatR.DecIng03 += (reg.REPORTE_ORDEN == 3) ? reg.CANTIDAD : 0;
                                            cDatR.DecIng04 += (reg.REPORTE_ORDEN == 4) ? reg.CANTIDAD : 0;
                                            cDatR.DecIng05 += (reg.REPORTE_ORDEN == 5) ? reg.CANTIDAD : 0;

                                            cDatR.StrTitIng01 = (reg.REPORTE_ORDEN == 1) ? reg.REPORTE_NOMBRE : cDatR.StrTitIng01;
                                            cDatR.StrTitIng02 = (reg.REPORTE_ORDEN == 2) ? reg.REPORTE_NOMBRE : cDatR.StrTitIng02;
                                            cDatR.StrTitIng03 = (reg.REPORTE_ORDEN == 3) ? reg.REPORTE_NOMBRE : cDatR.StrTitIng03;
                                            cDatR.StrTitIng04 = (reg.REPORTE_ORDEN == 4) ? reg.REPORTE_NOMBRE : cDatR.StrTitIng04;
                                            cDatR.StrTitIng05 = (reg.REPORTE_ORDEN == 5) ? reg.REPORTE_NOMBRE : cDatR.StrTitIng05;
                                        }
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
                                        if ((int)reg.OPERACION_ORDEN > 0)
                                        {
                                            if (!pvgAcumulado.Fields[27 + (int)reg.OPERACION_ORDEN].Visible)
                                            {
                                                pvgAcumulado.Fields[27 + (int)reg.OPERACION_ORDEN].Caption = reg.OPERACION_NOMBRE;
                                                pvgAcumulado.Fields[27 + (int)reg.OPERACION_ORDEN].Visible = true;
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

                                            cDatR.DecSal01 += (reg.REPORTE_ORDEN == 1) ? reg.CANTIDAD : 0;
                                            cDatR.DecSal02 += (reg.REPORTE_ORDEN == 2) ? reg.CANTIDAD : 0;
                                            cDatR.DecSal03 += (reg.REPORTE_ORDEN == 3) ? reg.CANTIDAD : 0;
                                            cDatR.DecSal04 += (reg.REPORTE_ORDEN == 4) ? reg.CANTIDAD : 0;
                                            cDatR.DecSal05 += (reg.REPORTE_ORDEN == 5) ? reg.CANTIDAD : 0;

                                            cDatR.StrTitSal01 = (reg.REPORTE_ORDEN == 1) ? reg.REPORTE_NOMBRE : cDatR.StrTitSal01;
                                            cDatR.StrTitSal02 = (reg.REPORTE_ORDEN == 2) ? reg.REPORTE_NOMBRE : cDatR.StrTitSal02;
                                            cDatR.StrTitSal03 = (reg.REPORTE_ORDEN == 3) ? reg.REPORTE_NOMBRE : cDatR.StrTitSal03;
                                            cDatR.StrTitSal04 = (reg.REPORTE_ORDEN == 4) ? reg.REPORTE_NOMBRE : cDatR.StrTitSal04;
                                            cDatR.StrTitSal05 = (reg.REPORTE_ORDEN == 5) ? reg.REPORTE_NOMBRE : cDatR.StrTitSal05;
                                        }
                                    }
                                }
                                lstMovimientos.Add(cDato);
                                lstAcumRep.Add(cDatR);
                                cont++;
                                intContProd++;
                            }

                            var intIdAct = Convert.ToInt32(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());

                            string strFechaI;
                            string strFechaF;

                            if (intIdAct == 16)
                            {
                                strFechaI = detFechaIni.Text;
                                strFechaF = detFechaIni.Text;
                            }
                            else
                            {
                                int intDia = DateTime.DaysInMonth(Convert.ToInt32(cmbPeriodo.Value.ToString().Substring(0, 4)), Convert.ToInt32(cmbPeriodo.Value.ToString().Substring(4, 2)));
                                strFechaI = "01/" + cmbPeriodo.Value.ToString().Substring(4, 2) + "/" + cmbPeriodo.Value.ToString().Substring(0, 4);
                                strFechaF = intDia.ToString().PadLeft(2, '0') + "/" + cmbPeriodo.Value.ToString().Substring(4, 2) + "/" + cmbPeriodo.Value.ToString().Substring(0, 4);
                            }

                            lstMovimientos.ForEach(a => a.DecSaldoIni = (a.StrFecha == strFechaI) ? a.DecSaldoInicial : 0);
                            lstMovimientos.ForEach(a => a.DecSaldoFin = (a.StrFecha == strFechaF) ? a.DecSaldoFinal : 0);

                            lstAcumRep.ForEach(a => a.DecSaldoIni = (a.StrFecha == strFechaI) ? a.DecSaldoInicial : 0);
                            lstAcumRep.ForEach(a => a.DecSaldoFin = (a.StrFecha == strFechaF) ? a.DecSaldoFinal : 0);

                            Session[CVariablesSesion.GrillaAcumulado] = lstMovimientos;

                            var objParametricas = Session[CVariablesSesion.OctanoParametrica] as List<O_LISTA_OCTANO_PAR_CTY>;
                            var paramAgrup = objParametricas.Where(x => x.TIPO_VALOR == 10).ToList().FirstOrDefault();

                            if (paramAgrup.VALOR_FIN == "1")
                            {
                                var lstNuevo = lstAcumRep.GroupBy(n => new { n.StrFecha, n.IntIdProducto, n.StrProducto, n.StrUnidadMedida }).
                                           Select(group =>
                                           new
                                           {
                                               group.Key.StrFecha,
                                               IntGrupoProducto = 0,
                                               group.Key.IntIdProducto,
                                               group.Key.StrProducto,
                                               group.Key.StrUnidadMedida,
                                               IntIdInfraestructura = 0,
                                               StrInfraestructura = "",
                                               DecSaldoIni = group.Sum(c => c.DecSaldoIni),
                                               DecSaldoInicial = group.Sum(c => c.DecSaldoInicial),
                                               DecIng01 = group.Sum(c => c.DecIng01),
                                               DecIng02 = group.Sum(c => c.DecIng02),
                                               DecIng03 = group.Sum(c => c.DecIng03),
                                               DecIng04 = group.Sum(c => c.DecIng04),
                                               DecIng05 = group.Sum(c => c.DecIng05),
                                               DecTotalIng = group.Sum(c => c.DecTotalIng),
                                               DecSal01 = group.Sum(c => c.DecSal01),
                                               DecSal02 = group.Sum(c => c.DecSal02),
                                               DecSal03 = group.Sum(c => c.DecSal03),
                                               DecSal04 = group.Sum(c => c.DecSal04),
                                               DecSal05 = group.Sum(c => c.DecSal05),
                                               DecTotalSal = group.Sum(c => c.DecTotalSal),
                                               DecSaldoFinal = group.Sum(c => c.DecSaldoFinal),
                                               DecSaldoFin = group.Sum(c => c.DecSaldoFin),
                                               StrTitIng01 = group.Max(c => c.StrTitIng01),
                                               StrTitIng02 = group.Max(c => c.StrTitIng02),
                                               StrTitIng03 = group.Max(c => c.StrTitIng03),
                                               StrTitIng04 = group.Max(c => c.StrTitIng04),
                                               StrTitIng05 = group.Max(c => c.StrTitIng05),
                                               StrTitSal01 = group.Max(c => c.StrTitSal01),
                                               StrTitSal02 = group.Max(c => c.StrTitSal02),
                                               StrTitSal03 = group.Max(c => c.StrTitSal03),
                                               StrTitSal04 = group.Max(c => c.StrTitSal04),
                                               StrTitSal05 = group.Max(c => c.StrTitSal05)
                                           }).ToList();
                                Session[CVariablesSesion.GrillaAcumRep] = lstNuevo;
                            }
                            else
                            {
                                Session[CVariablesSesion.GrillaAcumRep] = lstAcumRep;
                            }

                            pvgAcumulado.DataSource = lstMovimientos;
                            pvgAcumulado.DataBind();

                            var pvaArea = PivotArea.DataArea;
                            for (int i = 6; i < 50; i++)
                            {
                                if (pvgAcumulado.Fields[i].Visible)
                                {
                                    pvgAcumulado.Fields[i].SetAreaPosition(pvaArea, i);
                                }
                            }

                            #endregion
                        }
                    }
                    else
                    {
                        //sin implementar
                    }

                }
                else
                {
                    MImprimirMensaje(lstDespachos.strMensaje, EnumTiposMensajeAlerta.Error);
                }

                #endregion
            }
            else
            {
                var oTiposOpeActCty = lstTipoOperacion.Where(x => x.NOMBRE.Contains("VENTA")).ToList().FirstOrDefault();
                var decidTipoOp = (oTiposOpeActCty != null) ? oTiposOpeActCty.ID_TIPO_OPERACION : -99999;

                Session.Remove(CVariablesSesion.IdMovDdjj);
                var lstDespachos = _cUtil.MObtieneMovAcumulado(decFiltro, decIdTipoActividad.ToString(), decIdConsumidor.ToString(), decFechaDiaInicio.ToString(), decFechaDiaFin.ToString(), 1, decidTipoOp);
                if (lstDespachos.intCodigo == 1)
                {
                    Session[CVariablesSesion.IdMovDdjj] = Convert.ToDecimal(lstDespachos.strMensaje);

                    long lngIdMovimiento;
                    var lstResultadoLstAcumulado = (O_DECLARACION_JURADA_CTY)lstDespachos.oResultado;
                    Session[CVariablesSesion.DjAcumulado] = lstResultadoLstAcumulado;
                    var lstResultado = lstResultadoLstAcumulado.LstAcumulado;

                    lstResultado.ForEach(a => a.DecIdUnicoHidrocarburo = Convert.ToDecimal(a.DecIdHidrocarburo.ToString() + ((a.DecIdTipoInfraestructura == null) ? "" : a.DecIdTipoInfraestructura.ToString().Replace("-", "")) + ((a.DecIdInfraestructura == null) ? "" : a.DecIdInfraestructura.ToString().Replace("-", ""))));
                    var lstResAdic1 = lstResultadoLstAcumulado.LstaAdicional1;
                    var lstResAdic2 = lstResultadoLstAcumulado.LstaAdicional2;
                    var lstRecaudos = lstResultadoLstAcumulado.LstaRecaudoExt;

                    if (lstResultado.Any())
                    {
                        #region acumulado

                        decimal decProd = 0;
                        decimal decGrProd = 0;
                        decimal decGrupoProd = 0;
                        int cont = 1, intContProd = 1, IntContAgrup = 0;

                        lstResultado = lstResultado.OrderBy(x => x.DecIdHidrocarburo).ThenBy(x => x.StrTipoInfraestructura).ThenBy(x => x.StrInfraestructura).ThenBy(x => x.DecFecha).ToList();
                        var lstAcumRep = new List<CMovAcumuladoRep>();
                        var lstMovimientos = new List<CMovAcumulado>();
                        foreach (var lst in lstResultado)
                        {
                            var cDato = new CMovAcumulado();
                            var cDatR = new CMovAcumuladoRep();
                            if (decProd != lst.DecIdUnicoHidrocarburo)
                            {
                                decProd = lst.DecIdUnicoHidrocarburo;
                                intContProd = 1;
                                IntContAgrup++;
                                decGrupoProd++;
                            }
                            if (decGrProd != lst.DecIdHidrocarburo)
                            {
                                decGrProd = lst.DecIdHidrocarburo;
                                decGrupoProd = 1;
                            }

                            cDato.IntId = cont;
                            cDato.IntContProd = intContProd;
                            cDato.StrFecha = lst.DecFecha.ToString().Substring(6, 2) + "/" + lst.DecFecha.ToString().Substring(4, 2) + "/" + lst.DecFecha.ToString().Substring(0, 4);
                            cDato.IntIdProducto = (int)lst.DecIdHidrocarburo;
                            cDato.IntContAgrup = IntContAgrup;
                            cDato.StrProducto = lst.StrHidrocarburo;
                            cDato.StrCodProducto = lst.StrCodHidrocarburo;
                            cDato.StrUnidadMedida = lst.StrUnidadMedida;
                            cDato.StrTipoInfraestructura = lst.StrTipoInfraestructura ?? "";
                            cDato.StrInfraestructura = lst.StrInfraestructura ?? "";
                            cDato.StrCodigoInfraestructura = lst.StrInfraestructuraAnh ?? "";
                            cDato.DecSaldoInicial = lst.DecSaldoInicial;
                            cDato.DecTotalIng = lst.DecTotalIngreso;
                            cDato.DecSaldoFinal = lst.DecSaldoFinal;
                            cDato.DecTotalSal = lst.DecTotalEgreso;

                            cDatR.StrFecha = cDato.StrFecha;
                            cDatR.IntIdProducto = cDato.IntIdProducto;
                            cDatR.StrProducto = cDato.StrProducto;
                            cDatR.IntGrupoProducto = Convert.ToInt32(decGrupoProd);
                            cDatR.StrUnidadMedida = cDato.StrUnidadMedida;
                            cDatR.StrInfraestructura = cDato.StrTipoInfraestructura + " - " + cDato.StrInfraestructura;
                            cDatR.DecSaldoInicial = cDato.DecSaldoInicial;
                            cDatR.DecTotalIng = cDato.DecTotalIng;
                            cDatR.DecSaldoFinal = cDato.DecSaldoFinal;
                            cDatR.DecTotalSal = cDato.DecTotalSal;

                            if (lst.LstIngresos == null || !lst.LstIngresos.Any())
                            {
                                cDato.DecIng01 = -99999;
                            }
                            else
                            {
                                foreach (var reg in lst.LstIngresos)
                                {
                                    if ((int)reg.OPERACION_ORDEN > 0)
                                    {
                                        if (!pvgAcumulado.Fields[6 + (int)reg.OPERACION_ORDEN].Visible)
                                        {
                                            pvgAcumulado.Fields[6 + (int)reg.OPERACION_ORDEN].Caption = reg.OPERACION_NOMBRE;
                                            pvgAcumulado.Fields[6 + (int)reg.OPERACION_ORDEN].Visible = true;

                                            if (decIdTipoActividad == (int)EnumActividades.DistribucionGasRedes)
                                            {
                                                pvgAcumulado.Fields[6 + (int)reg.OPERACION_ORDEN].Visible = false;
                                            }
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

                                        cDatR.DecIng01 += (reg.REPORTE_ORDEN == 1) ? reg.CANTIDAD : 0;
                                        cDatR.DecIng02 += (reg.REPORTE_ORDEN == 2) ? reg.CANTIDAD : 0;
                                        cDatR.DecIng03 += (reg.REPORTE_ORDEN == 3) ? reg.CANTIDAD : 0;
                                        cDatR.DecIng04 += (reg.REPORTE_ORDEN == 4) ? reg.CANTIDAD : 0;
                                        cDatR.DecIng05 += (reg.REPORTE_ORDEN == 5) ? reg.CANTIDAD : 0;

                                        cDatR.StrTitIng01 = (reg.REPORTE_ORDEN == 1) ? reg.REPORTE_NOMBRE : cDatR.StrTitIng01;
                                        cDatR.StrTitIng02 = (reg.REPORTE_ORDEN == 2) ? reg.REPORTE_NOMBRE : cDatR.StrTitIng02;
                                        cDatR.StrTitIng03 = (reg.REPORTE_ORDEN == 3) ? reg.REPORTE_NOMBRE : cDatR.StrTitIng03;
                                        cDatR.StrTitIng04 = (reg.REPORTE_ORDEN == 4) ? reg.REPORTE_NOMBRE : cDatR.StrTitIng04;
                                        cDatR.StrTitIng05 = (reg.REPORTE_ORDEN == 5) ? reg.REPORTE_NOMBRE : cDatR.StrTitIng05;
                                    }
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
                                    if ((int)reg.OPERACION_ORDEN > 0)
                                    {
                                        if (!pvgAcumulado.Fields[27 + (int)reg.OPERACION_ORDEN].Visible)
                                        {
                                            pvgAcumulado.Fields[27 + (int)reg.OPERACION_ORDEN].Caption = reg.OPERACION_NOMBRE;
                                            pvgAcumulado.Fields[27 + (int)reg.OPERACION_ORDEN].Visible = true;
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

                                        cDatR.DecSal01 += (reg.REPORTE_ORDEN == 1) ? reg.CANTIDAD : 0;
                                        cDatR.DecSal02 += (reg.REPORTE_ORDEN == 2) ? reg.CANTIDAD : 0;
                                        cDatR.DecSal03 += (reg.REPORTE_ORDEN == 3) ? reg.CANTIDAD : 0;
                                        cDatR.DecSal04 += (reg.REPORTE_ORDEN == 4) ? reg.CANTIDAD : 0;
                                        cDatR.DecSal05 += (reg.REPORTE_ORDEN == 5) ? reg.CANTIDAD : 0;

                                        cDatR.StrTitSal01 = (reg.REPORTE_ORDEN == 1) ? reg.REPORTE_NOMBRE : cDatR.StrTitSal01;
                                        cDatR.StrTitSal02 = (reg.REPORTE_ORDEN == 2) ? reg.REPORTE_NOMBRE : cDatR.StrTitSal02;
                                        cDatR.StrTitSal03 = (reg.REPORTE_ORDEN == 3) ? reg.REPORTE_NOMBRE : cDatR.StrTitSal03;
                                        cDatR.StrTitSal04 = (reg.REPORTE_ORDEN == 4) ? reg.REPORTE_NOMBRE : cDatR.StrTitSal04;
                                        cDatR.StrTitSal05 = (reg.REPORTE_ORDEN == 5) ? reg.REPORTE_NOMBRE : cDatR.StrTitSal05;
                                    }
                                }
                            }
                            lstMovimientos.Add(cDato);
                            lstAcumRep.Add(cDatR);
                            cont++;
                            intContProd++;
                        }

                        var intIdAct = Convert.ToInt32(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());

                        string strFechaI;
                        string strFechaF;

                        if (intIdAct == 16)
                        {
                            strFechaI = detFechaIni.Text;
                            strFechaF = detFechaIni.Text;
                        }
                        else
                        {
                            int intDia = DateTime.DaysInMonth(Convert.ToInt32(cmbPeriodo.Value.ToString().Substring(0, 4)), Convert.ToInt32(cmbPeriodo.Value.ToString().Substring(4, 2)));
                            strFechaI = "01/" + cmbPeriodo.Value.ToString().Substring(4, 2) + "/" + cmbPeriodo.Value.ToString().Substring(0, 4);
                            strFechaF = intDia.ToString().PadLeft(2, '0') + "/" + cmbPeriodo.Value.ToString().Substring(4, 2) + "/" + cmbPeriodo.Value.ToString().Substring(0, 4);
                        }

                        lstMovimientos.ForEach(a => a.DecSaldoIni = (a.StrFecha == strFechaI) ? a.DecSaldoInicial : 0);
                        lstMovimientos.ForEach(a => a.DecSaldoFin = (a.StrFecha == strFechaF) ? a.DecSaldoFinal : 0);

                        lstAcumRep.ForEach(a => a.DecSaldoIni = (a.StrFecha == strFechaI) ? a.DecSaldoInicial : 0);
                        lstAcumRep.ForEach(a => a.DecSaldoFin = (a.StrFecha == strFechaF) ? a.DecSaldoFinal : 0);

                        Session[CVariablesSesion.GrillaAcumulado] = lstMovimientos;

                        var objParametricas = Session[CVariablesSesion.OctanoParametrica] as List<O_LISTA_OCTANO_PAR_CTY>;
                        var paramAgrup = objParametricas.Where(x => x.TIPO_VALOR == 10).ToList().FirstOrDefault();

                        if (paramAgrup.VALOR_FIN == "1")
                        {
                            var lstNuevo = lstAcumRep.GroupBy(n => new { n.StrFecha, n.IntIdProducto, n.StrProducto, n.StrUnidadMedida }).
                                       Select(group =>
                                       new
                                       {
                                           group.Key.StrFecha,
                                           IntGrupoProducto = 0,
                                           group.Key.IntIdProducto,
                                           group.Key.StrProducto,
                                           group.Key.StrUnidadMedida,
                                           IntIdInfraestructura = 0,
                                           StrInfraestructura = "",
                                           DecSaldoIni = group.Sum(c => c.DecSaldoIni),
                                           DecSaldoInicial = group.Sum(c => c.DecSaldoInicial),
                                           DecIng01 = group.Sum(c => c.DecIng01),
                                           DecIng02 = group.Sum(c => c.DecIng02),
                                           DecIng03 = group.Sum(c => c.DecIng03),
                                           DecIng04 = group.Sum(c => c.DecIng04),
                                           DecIng05 = group.Sum(c => c.DecIng05),
                                           DecTotalIng = group.Sum(c => c.DecTotalIng),
                                           DecSal01 = group.Sum(c => c.DecSal01),
                                           DecSal02 = group.Sum(c => c.DecSal02),
                                           DecSal03 = group.Sum(c => c.DecSal03),
                                           DecSal04 = group.Sum(c => c.DecSal04),
                                           DecSal05 = group.Sum(c => c.DecSal05),
                                           DecTotalSal = group.Sum(c => c.DecTotalSal),
                                           DecSaldoFinal = group.Sum(c => c.DecSaldoFinal),
                                           DecSaldoFin = group.Sum(c => c.DecSaldoFin),
                                           StrTitIng01 = group.Max(c => c.StrTitIng01),
                                           StrTitIng02 = group.Max(c => c.StrTitIng02),
                                           StrTitIng03 = group.Max(c => c.StrTitIng03),
                                           StrTitIng04 = group.Max(c => c.StrTitIng04),
                                           StrTitIng05 = group.Max(c => c.StrTitIng05),
                                           StrTitSal01 = group.Max(c => c.StrTitSal01),
                                           StrTitSal02 = group.Max(c => c.StrTitSal02),
                                           StrTitSal03 = group.Max(c => c.StrTitSal03),
                                           StrTitSal04 = group.Max(c => c.StrTitSal04),
                                           StrTitSal05 = group.Max(c => c.StrTitSal05)
                                       }).ToList();
                            Session[CVariablesSesion.GrillaAcumRep] = lstNuevo;
                        }
                        else
                        {
                            Session[CVariablesSesion.GrillaAcumRep] = lstAcumRep;
                        }

                        pvgAcumulado.DataSource = lstMovimientos;
                        pvgAcumulado.DataBind();

                        var pvaArea = PivotArea.DataArea;
                        for (int i = 6; i < 50; i++)
                        {
                            if (pvgAcumulado.Fields[i].Visible)
                            {
                                pvgAcumulado.Fields[i].SetAreaPosition(pvaArea, i);
                            }
                        }

                        #endregion
                    }
                    if (lstResAdic1 != null && lstResAdic1.Any())
                    {
                        #region facturas fuera de ala

                        lngIdMovimiento = 0;
                        lstResAdic1 = lstResAdic1.OrderBy(x => x.idMovimiento).ThenBy(x => x.idTipoTransaccion).ToList();

                        long lngCont = 1;
                        CDjContenedor cDato = null;
                        var lstRep = new List<CDjContenedor>();

                        foreach (var registro in lstResAdic1)
                        {
                            if (lngIdMovimiento != registro.idMovimiento)
                            {
                                if (cDato != null)
                                {
                                    lstRep.Add(cDato);
                                }
                                cDato = new CDjContenedor();
                                cDato.LngId = lngCont;
                                cDato.StrFecha = registro.fechaEmision.ToString().Substring(6, 2) + "/" + registro.fechaEmision.ToString().Substring(4, 2) + "/" + registro.fechaEmision.ToString().Substring(0, 4);
                                cDato.StrPais = registro.ubicacionOperacion;
                                cDato.StrIdentificador = registro.identificador;
                                cDato.StrSubIdentificador = registro.subIdentificador;
                                cDato.StrTipoContenedor = registro.tipoEnvase;
                                cDato.DecIdProducto = registro.idHidrocarburo;
                                cDato.StrProducto = registro.hidrocarburo;
                                cDato.StrCodProducto = registro.codHidrocarburo;
                                cDato.StrUnidadMedida = registro.unidadMedida;
                                lngIdMovimiento = (long)registro.idMovimiento;
                            }

                            switch ((int)registro.idTipoTransaccion)
                            {
                                case 2:
                                case 3:
                                    cDato.StrEstado = registro.EstadoTransaccion.Substring(0, 1);
                                    cDato.DecIdEstado = registro.idEstadoTransaccion;

                                    cDato.LngNit = (long)registro.nitConsumidor;
                                    cDato.StrRazonSocial = registro.razonSocial;

                                    cDato.LngNoAut = (long)registro.numeroAutorizacion;
                                    cDato.StrNoTransaccion = registro.numeroTransaccion;
                                    cDato.StrCodCtrl = registro.codigoControl;

                                    cDato.DecPrecio = registro.precioUnitario;
                                    if (registro.idEstadoTransaccion == 11)
                                    {
                                        cDato.DecCantidad = registro.cantidadHidrocarburo;
                                        cDato.DecImporte = registro.precioTotal;
                                    }
                                    else
                                    {
                                        cDato.DecCantidad = 0;
                                        cDato.DecImporte = 0;
                                    }

                                    break;
                                case 7:
                                    cDato.AutSustCtrl = registro.numeroTransaccion;
                                    break;
                                case 21:
                                    cDato.AutAnh = registro.numeroTransaccion;
                                    break;
                            }
                            lngCont++;
                        }
                        if (cDato != null)
                        {
                            lstRep.Add(cDato);
                        }
                        else
                        {
                            //todo No implementado.
                        }

                        lstRep = lstRep.OrderBy(x => x.StrFecha).ToList();

                        Session[CVariablesSesion.GrillaAuxiliar] = lstRep;
                        dgvContenedor.DataSource = lstRep;
                        dgvContenedor.DataBind();


                        #endregion
                    }
                    if (lstRecaudos != null && lstRecaudos.Any())
                    {
                        Session[CVariablesSesion.GrillaRecaudo] = lstRecaudos;
                        dgvRecaudo.DataSource = lstRecaudos;
                        dgvRecaudo.DataBind();
                    }

                    if (lstResAdic2 != null && lstResAdic2.Any())
                    {
                        #region facturas internacionales
                        lstResAdic2 = lstResAdic2.OrderBy(x => x.idMovimiento).ThenBy(x => x.idTipoEmisor).ToList();
                        CDjInternacional cDato = null;
                        lngIdMovimiento = 0;
                        var lstRep = new List<CDjInternacional>();
                        long lngCont = 1;

                        foreach (var registro in lstResAdic2)
                        {
                            if (lngIdMovimiento != registro.idMovimiento)
                            {
                                if (cDato != null)
                                {
                                    lstRep.Add(cDato);
                                }
                                cDato = new CDjInternacional();
                                cDato.LngId = lngCont;
                                cDato.StrFecha = registro.fechaEmision.ToString().Substring(6, 2) + "/" + registro.fechaEmision.ToString().Substring(4, 2) + "/" + registro.fechaEmision.ToString().Substring(0, 4);
                                cDato.StrPlaca = registro.identificador;
                                cDato.StrPais = registro.ubicacionOperacion;
                                cDato.LngNit = (long)registro.nitConsumidor;
                                cDato.StrRazonSocial = registro.razonSocial;
                                cDato.LngNoAutProp = 0;
                                cDato.LngNoAutExt = 0;
                                cDato.LngNoFactProp = 0;
                                cDato.LngNoFactExt = 0;
                                cDato.StrCodCtrlProp = "";
                                cDato.StrCodCtrlExt = "";
                                cDato.DecImporteProp = 0;
                                cDato.DecImporteExt = 0;
                                cDato.DecPrecioProp = 0;
                                cDato.DecPrecioExt = 0;
                                cDato.DecCantidadProp = 0;
                                cDato.DecCantidadExt = 0;
                                cDato.DecIdEstado = registro.idEstadoTransaccion;
                                cDato.StrEstado = registro.EstadoTransaccion;
                                cDato.DecIdProducto = registro.idHidrocarburo;
                                cDato.StrProducto = registro.hidrocarburo;
                                cDato.StrCodProducto = registro.codHidrocarburo;
                                cDato.StrUnidadMedida = registro.unidadMedida;
                                cDato.StrTipoMovimiento = registro.tipoMovimiento;
                                lngIdMovimiento = (long)registro.idMovimiento;
                            }

                            switch ((int)registro.idTipoEmisor)
                            {
                                case (int)EnumTipoEmisor.PropiaEntidad:
                                    cDato.LngNoAutProp = (long)registro.numeroAutorizacion;
                                    cDato.LngNoFactProp = Convert.ToInt64(registro.numeroTransaccion);
                                    cDato.StrCodCtrlProp = registro.codigoControl;
                                    cDato.DecPrecioProp = registro.precioUnitario;
                                    cDato.DecPrecioExt += registro.precioUnitario;
                                    if (!registro.EstadoTransaccion.Equals("A"))
                                    {
                                        cDato.DecImporteProp = registro.precioTotal;
                                        cDato.DecCantidadProp = registro.cantidadHidrocarburo;
                                    }
                                    break;
                                case (int)EnumTipoEmisor.ExternaYpfb:

                                    try
                                    {
                                        cDato.LngNoAutExt = (long)registro.numeroAutorizacion;
                                        cDato.LngNoFactExt = Convert.ToInt64(registro.numeroTransaccion);
                                    }
                                    catch (Exception)
                                    {
                                        var tt = 33;
                                    }

                                    cDato.StrCodCtrlExt = registro.codigoControl;
                                    cDato.DecPrecioExt += registro.precioUnitario;
                                    if (!registro.EstadoTransaccion.Equals("A"))
                                    {
                                        cDato.DecImporteExt = registro.precioTotal;
                                        cDato.DecCantidadExt = registro.cantidadHidrocarburo;
                                    }
                                    break;
                                default:
                                    //todo No implementado.
                                    break;
                            }
                            lngCont++;
                        }

                        if (cDato != null)
                        {
                            lstRep.Add(cDato);
                        }
                        else
                        {
                            //todo No implementado.
                        }

                        lstRep = lstRep.OrderBy(x => x.StrFecha).ToList();

                        Session[CVariablesSesion.GrillaInternacional] = lstRep;
                        dgvInternacional.DataSource = lstRep;
                        dgvInternacional.DataBind();

                        #endregion
                    }
                }
                else
                {
                    MImprimirMensaje(lstDespachos.strMensaje, EnumTiposMensajeAlerta.Error);
                }
            }
        }

        /// <summary>
        /// METODO. MConfiguraGrilla
        /// </summary>
        /// <param name="idActividad"></param>
        /// <returns></returns>
        private decimal MConfiguraGrilla(int idActividad)
        {
            decimal decResultado = 0;
            pvgAcumulado.Fields[(int)EnumColumnasGrilla.PvAcInfraestructura].Visible = true;
            pvgAcumulado.Fields[(int)EnumColumnasGrilla.PvAcTipoInfraestructura].Visible = true;
            pvgAcumulado.Fields[(int)EnumColumnasGrilla.PvAcProducto].Visible = true;
            pvgAcumulado.Fields[(int)EnumColumnasGrilla.PvAcCodProducto].Visible = true;

            var objParametricas = Session[CVariablesSesion.OctanoParametrica] as List<O_LISTA_OCTANO_PAR_CTY>;
            var oListaOctanoParCty = objParametricas.Where(x => x.TIPO_VALOR == 7).ToList().FirstOrDefault();
            decResultado = Convert.ToDecimal(oListaOctanoParCty.VALOR_FIN);
            if (oListaOctanoParCty != null && oListaOctanoParCty.VALOR_FIN == "2")
            {
                pvgAcumulado.Fields[(int)EnumColumnasGrilla.PvAcCodProducto].Visible = false;
            }
            else
            {
                pvgAcumulado.Fields[(int)EnumColumnasGrilla.PvAcInfraestructura].Visible = false;
                pvgAcumulado.Fields[(int)EnumColumnasGrilla.PvAcTipoInfraestructura].Visible = false;
                pvgAcumulado.Fields[(int)EnumColumnasGrilla.PvAcProducto].Visible = false;
            }
            var pvArea = PivotArea.RowArea;
            for (int i = 0; i < 6; i++)
            {
                if (pvgAcumulado.Fields[i].Visible)
                {
                    pvgAcumulado.Fields[i].SetAreaPosition(pvArea, i);
                }
            }
            pvgAcumulado.Fields[(int)EnumColumnasGrilla.PvAcToatlEgr].Visible = true;
            if (idActividad != (int)EnumActividades.EstacionServicioGnv)
            {
                pvgAcumulado.Fields[(int)EnumColumnasGrilla.PvAcSaldoIni].Visible = true;
                pvgAcumulado.Fields[(int)EnumColumnasGrilla.PvAcTotalIng].Visible = true;
                pvgAcumulado.Fields[(int)EnumColumnasGrilla.PvAcSaldoFin].Visible = true;
            }

            if (idActividad == (int)EnumActividades.ImportacionCarburantes)
            {
                pvgAcumulado.Fields[(int)EnumColumnasGrilla.PvAcSaldoIni].Visible = false;
                pvgAcumulado.Fields[(int)EnumColumnasGrilla.PvAcToatlEgr].Visible = false;
                pvgAcumulado.Fields[(int)EnumColumnasGrilla.PvAcSaldoFin].Visible = false;
            }

            if (idActividad == (int)EnumActividades.ImportacionLubricantes)
            {
                pvgAcumulado.Fields[(int)EnumColumnasGrilla.PvAcSaldoIni].Visible = false;
                pvgAcumulado.Fields[(int)EnumColumnasGrilla.PvAcToatlEgr].Visible = false;
                pvgAcumulado.Fields[(int)EnumColumnasGrilla.PvAcSaldoFin].Visible = false;
            }
            if (idActividad == (int)EnumActividades.DistribucionGasRedes)
            {
                pvgAcumulado.Fields[(int)EnumColumnasGrilla.PvAcSaldoIni].Visible = false;
                pvgAcumulado.Fields[(int)EnumColumnasGrilla.PvAcTotalIng].Visible = false;

                pvgAcumulado.Fields[(int)EnumColumnasGrilla.PvAcSaldoFin].Visible = false;
                // pvgAcumulado.Fields[(int)EnumColumnasGrilla.FoCantidadRec].Visible = true;
            }
            return decResultado;
        }

        /// <summary>
        /// METODO. MLimpiaGrilla
        /// </summary>
        private void MLimpiaGrilla()
        {
            Session.Remove(CVariablesSesion.GrillaAcumulado);
            pvgAcumulado.DataSource = null;
            pvgAcumulado.DataBind();

            Session.Remove(CVariablesSesion.GrillaAuxiliar);
            dgvContenedor.DataSource = null;
            dgvContenedor.DataBind();

            Session.Remove(CVariablesSesion.GrillaInternacional);
            dgvInternacional.DataSource = null;
            dgvInternacional.DataBind();

            Session.Remove(CVariablesSesion.GrillaRecaudo);
            dgvRecaudo.DataSource = null;
            dgvRecaudo.DataBind();

            MImprimirMensaje("", EnumTiposMensajeAlerta.Limpiar);

            for (int i = 49; i > 5; i--)
            {
                pvgAcumulado.Fields[i].Visible = false;
            }

            tacMovimientos.Tabs[1].ClientVisible = false;
            tacMovimientos.Tabs[2].ClientVisible = false;
            tacMovimientos.Tabs[3].ClientVisible = false;
        }

        protected void pnlCargaDui_Callback(object sender, CallbackEventArgsBase e)
        {
            decimal decOpcion = Convert.ToDecimal(cmbResMin.Value);

            string strFechaInicio = "";
            string strFechaFin = "";
            string strFechaInicioT = "";
            string strFechaFinT = "";

            if (decOpcion == 1)
            {
                strFechaInicio = detFechaIni.Date.Year + detFechaIni.Date.Month.ToString().PadLeft(2, '0') + detFechaIni.Date.Day.ToString().PadLeft(2, '0') + "000000";
                strFechaFin = detFechaIni.Date.Year + detFechaIni.Date.Month.ToString().PadLeft(2, '0') + detFechaIni.Date.Day.ToString().PadLeft(2, '0') + "235959";
                strFechaInicioT = detFechaIni.Date.Year + detFechaIni.Date.Month.ToString().PadLeft(2, '0') + detFechaIni.Date.Day.ToString().PadLeft(2, '0');
                strFechaFinT = detFechaIni.Date.Year + detFechaIni.Date.Month.ToString().PadLeft(2, '0') + detFechaIni.Date.Day.ToString().PadLeft(2, '0');
            }
            else
            {
                strFechaInicio = detFechaIni.Date.Year + detFechaIni.Date.Month.ToString().PadLeft(2, '0') + "01000000";
                strFechaFin = detFechaIni.Date.Year + detFechaIni.Date.Month.ToString().PadLeft(2, '0') + "31235959";
                strFechaInicioT = detFechaIni.Date.Year + detFechaIni.Date.Month.ToString().PadLeft(2, '0') + "01";
                strFechaFinT = detFechaIni.Date.Year + detFechaIni.Date.Month.ToString().PadLeft(2, '0') + "31";
            }

            var lstResultadoMovBase = new List<O_CONSULTA_MOVIM_TRAN_CTY>();
            var lstResultadoMovFosil = new List<O_CONSULTA_MOVIM_TRAN_CTY>();
            var lstResultadoMovGasBlanca = new List<O_CONSULTA_MOVIM_TRAN_CTY>();
            var lstResultadoMovOtro = new List<O_CONSULTA_MOVIM_TRAN_CTY>();

            var lstNumeroDui = new List<O_LISTADO_DUI>();

            var lstMovimiento = _consulta.ObtenerMovimiento("",
                new EObjetoEnvio8ParametrosProto
                {
                    decFiltro = 4, //Filtro 1 por producto //2 consulta por infraestructura
                    strParametro1 = tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString(), //Id proyecto/entidad
                    strParametro2 = strFechaInicio,//detFechaIni.Date.Year + detFechaIni.Date.Month.ToString().PadLeft(2, '0') + "01000000",//fecha inicial
                    strParametro3 = strFechaFin,//detFechaIni.Date.Year + detFechaIni.Date.Month.ToString().PadLeft(2, '0') + "31235959", //fecha final
                    strParametro4 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(), //id Actividad
                    strParametro5 = "249", //tipo operacion
                    strParametro6 = "0", //idProducto 
                    strParametro7 = "0", // id_infraestructura
                    strParametro8 = "0" // tipo_infraestructura
                });

            if (lstMovimiento.intCodigo == 1)
            {
                var lstResultado = (List<O_CONSULTA_MOVIM_TRAN_CTY>)lstMovimiento.oResultado;
                if (lstResultado.Any())
                {

                    var registros = lstResultado;
                    Session[CVariablesSesion.GrillaTransaccion] = registros;

                    foreach (var dato in registros)
                    {
                        var numDui = new O_LISTADO_DUI();

                        int intProducto = Convert.ToInt32(dato.ID_HIDROCARBURO);
                        switch (intProducto)
                        {
                            case 401://test 401 desarrollo 4326 produccion 1829
                            case 402://test 402 desarrollo 4327 produccion 1830
                            case 403://test 403 desarrollo 4328 produccion 1831
                            case 404://test 404 desarrollo 4329 produccion 1832
                            case 405://test 405 desarrollo 4330 produccion 1833

                                break;
                            case 367: //test 367 desarrollo 367 produccion 367
                            case 368: //test 368 desarrollo 368 produccion 368
                            case 369: //test 369 desarrollo 369 produccion 369
                            case 370: //test 370 desarrollo 370 produccion 370
                            case 371: //test 371 desarrollo 371 produccion 371
                            case 372: //test 372 desarrollo 372 produccion 372
                            case 374: //test 374 desarrollo 374 produccion 373
                            case 380: //test 380 desarrollo 380 produccion 374
                            case 382: //test 382 desarrollo 382 produccion 375
                            case 383: //test 383 desarrollo 383 produccion 376
                            case 384: //test 384 desarrollo 384 produccion 377
                            case 385: //test 385 desarrollo 385 produccion 378
                            case 386: //test 386 desarrollo 386 produccion 379
                            case 387: //test 387 desarrollo 387 produccion 380
                            case 388: //test 388 desarrollo 388 produccion 381
                            case 389: //test 389 desarrollo 389 produccion 382

                                string[] lista = dato.OBSERVACION_MOVIMIENTO.Split('|');
                                numDui.DUI = lista[0].ToString();
                                numDui.NRO_DUI = lista[0].ToString();
                                break;
                            case 278:

                                break;
                            default:


                                break;
                        }

                        if (numDui.DUI != null)
                        {
                            lstNumeroDui.Add(numDui);
                        }
                        else
                        {
                            //no implementado
                        }

                    }
                }
            }

            if (decOpcion == 1)
            {
                cmbListadoDui.ClientVisible = true;

                var listaDuiObtenido = (from datoDui in lstNumeroDui
                                        group datoDui by datoDui.DUI into datoDuiGroup
                                        select new
                                        {
                                            DUI = datoDuiGroup.Key,
                                            NRO_DUI = datoDuiGroup.Key,
                                        }).ToList();

                cmbListadoDui.DataSource = listaDuiObtenido;
                cmbListadoDui.DataBind();
            }
            else
            {
                cmbListadoDui.ClientVisible = false;
                cmbListadoDui.DataSource = null;
                cmbListadoDui.DataBind();
            }

        }

        /// <summary>
        /// METODO. MImprimeDeclaracion
        /// </summary>
        /// <param name="datFechaReg"></param>
        /// <param name="strCite"></param>
        private void MImprimeDeclaracion(DateTime datFechaReg, string strCite)
        {
            var objParametricas = Session[CVariablesSesion.OctanoParametrica] as List<O_LISTA_OCTANO_PAR_CTY>;

            if (objParametricas == null)
            {
                MPuImprimirMensaje("MImprimeDeclaracion. No existen parametricas.", EnumTiposMensajeAlerta.Error);
                return;
            }
            var strTextoCab = objParametricas.Where(x => x.TIPO_VALOR == 8).ToList().FirstOrDefault().VALOR_FIN;
            var vecCabecera = strTextoCab.Split('|');

            var formatoFecha = CultureInfo.CurrentCulture.DateTimeFormat;

            string[] strRep;
            if (cmbRepresentante.Text.Length > 0)
            {
                strRep = cmbRepresentante.Text.Split('-');
            }
            else
            {
                strRep = txtPadPropietario.Text.Split('-');
            }

            Session.Remove(CVariablesSesion.ReportePrincipal);

            if (Convert.ToInt32(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()) == (int)EnumActividades.ImportacionCarburantes)
            {
                Session.Remove(CVariablesSesion.GrillaTransaccion);
                Session.Remove(CVariablesSesion.GrillaFosil);

                var txtPlanta = "";

                decimal decValidar = 0;

                decimal decOpcion = Convert.ToDecimal(cmbResMin.Value);

                string strDuiSeleccionado = "";
                //string strDuiSeleccionado = cmbListadoDui.Value.ToString();

                if (cmbListadoDui.Value == null)
                {
                    strDuiSeleccionado = "";
                }
                else
                {
                    strDuiSeleccionado = cmbListadoDui.Value.ToString();
                }


                if (decOpcion == 1)
                {
                    if (strDuiSeleccionado != "")
                    {
                        decValidar = 1;
                    }
                    else
                    {
                        decValidar = 0;
                    }
                }
                else if (decOpcion == 2)
                {
                    decValidar = 1;
                }
                else
                {
                    decValidar = 0;
                }

                var objNuevoDatoA = new List<O_OBTENER_ANEXO_A>();
                var objNuevoDatoAnGb = new List<O_OBTENER_ANEXO_A_GB>();


                var objVolImportA = new List<O_OBTENER_VOLUMEN_IMPORTADO>();
                var objVolImportB = new List<O_OBTENER_VOLUMEN_IMPORTADO>();
                var objVolImportC = new List<O_OBTENER_VOLUMEN_IMPORTADO>();
                var objVolImportD = new List<O_OBTENER_VOLUMEN_IMPORTADO>();
                var objVolImportE = new List<O_OBTENER_VOLUMEN_IMPORTADO>();
                var objVolImportF = new List<O_OBTENER_VOLUMEN_IMPORTADO>();
                var objVolImportG = new List<O_OBTENER_VOLUMEN_IMPORTADO>();
                var objVolImportH = new List<O_OBTENER_VOLUMEN_IMPORTADO>();
                var objVolImportI = new List<O_OBTENER_VOLUMEN_IMPORTADO>();

                decimal decIdMovimiento = 0;

                var objCostos = new List<O_OBTENER_COSTOS_IMPORTADO>();
                var objCostosSubvencion = new List<O_OBTENER_COSTOS_SUBVENCION>();
                var objCostosSubvencionFinal = new List<O_OBTENER_COSTOS_SUBVENCION_FINAL>();

                var objCostosUni = new List<O_OBTENER_COSTOS_UNITARIO>();

                var objCostosUniSub1 = new List<O_OBTENER_COSTOS_UNITARIO>();
                var objCostosUniSub2 = new List<O_OBTENER_COSTOS_UNITARIO>();

                var objCostosTotSub = new List<O_OBTENER_COSTOS_TOT_SUB>();

                List<O_LISTA_TRANSACCION_CTY> lstResultadoFosilTotal = new List<O_LISTA_TRANSACCION_CTY>();

                List<O_LISTA_TRANSACCION_CTY> lstResultadoBaseTotal = new List<O_LISTA_TRANSACCION_CTY>();

                string strFechaInicio = "";
                string strFechaFin = "";
                string strFechaInicioT = "";
                string strFechaFinT = "";

                if (decValidar == 1)
                {
                    if (decOpcion == 1)
                    {
                        strFechaInicio = detFechaIni.Date.Year + detFechaIni.Date.Month.ToString().PadLeft(2, '0') + detFechaIni.Date.Day.ToString().PadLeft(2, '0') + "000000";
                        strFechaFin = detFechaIni.Date.Year + detFechaIni.Date.Month.ToString().PadLeft(2, '0') + detFechaIni.Date.Day.ToString().PadLeft(2, '0') + "235959";
                        strFechaInicioT = detFechaIni.Date.Year + detFechaIni.Date.Month.ToString().PadLeft(2, '0') + detFechaIni.Date.Day.ToString().PadLeft(2, '0');
                        strFechaFinT = detFechaIni.Date.Year + detFechaIni.Date.Month.ToString().PadLeft(2, '0') + detFechaIni.Date.Day.ToString().PadLeft(2, '0');
                    }
                    else
                    {
                        strFechaInicio = detFechaIni.Date.Year + detFechaIni.Date.Month.ToString().PadLeft(2, '0') + "01000000";
                        strFechaFin = detFechaIni.Date.Year + detFechaIni.Date.Month.ToString().PadLeft(2, '0') + "31235959";
                        strFechaInicioT = detFechaIni.Date.Year + detFechaIni.Date.Month.ToString().PadLeft(2, '0') + "01";
                        strFechaFinT = detFechaIni.Date.Year + detFechaIni.Date.Month.ToString().PadLeft(2, '0') + "31";
                    }

                    var lstResultadoMovBase = new List<O_CONSULTA_MOVIM_TRAN_CTY>();
                    var lstResultadoMovFosil = new List<O_CONSULTA_MOVIM_TRAN_CTY>();
                    var lstResultadoMovGasBlanca = new List<O_CONSULTA_MOVIM_TRAN_CTY>();
                    var lstResultadoMovOtro = new List<O_CONSULTA_MOVIM_TRAN_CTY>();

                    var lstMovimiento = _consulta.ObtenerMovimiento("",
                        new EObjetoEnvio8ParametrosProto
                        {
                            decFiltro = 4, //Filtro 1 por producto //2 consulta por infraestructura
                            strParametro1 = tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString(), //Id proyecto/entidad
                            strParametro2 = strFechaInicio,//detFechaIni.Date.Year + detFechaIni.Date.Month.ToString().PadLeft(2, '0') + "01000000",//fecha inicial
                            strParametro3 = strFechaFin,//detFechaIni.Date.Year + detFechaIni.Date.Month.ToString().PadLeft(2, '0') + "31235959", //fecha final
                            strParametro4 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(), //id Actividad
                            strParametro5 = "249", //tipo operacion
                            strParametro6 = "0", //idProducto 
                            strParametro7 = "0", // id_infraestructura
                            strParametro8 = "0" // tipo_infraestructura
                        });

                    if (lstMovimiento.intCodigo == 1)
                    {
                        var lstResultado = (List<O_CONSULTA_MOVIM_TRAN_CTY>)lstMovimiento.oResultado;
                        if (lstResultado.Any())
                        {

                            var registros = lstResultado;
                            Session[CVariablesSesion.GrillaTransaccion] = registros;

                            foreach (var dato in registros)
                            {
                                int intProducto = Convert.ToInt32(dato.ID_HIDROCARBURO);
                                decIdMovimiento = Convert.ToDecimal(dato.ID_MOVIMIENTO);
                                switch (intProducto)
                                {
                                    case 401://test 401 desarrollo 4326 produccion 1829
                                    case 402://test 402 desarrollo 4327 produccion 1830
                                    case 403://test 403 desarrollo 4328 produccion 1831
                                    case 404://test 404 desarrollo 4329 produccion 1832
                                    case 405://test 405 desarrollo 4330 produccion 1833

                                        lstResultadoMovFosil.Add(dato);
                                        var objTransaccionFosil = _client.Post<EResultadoTransaccion>(CAppSettings.svc_obtener_transaccion,
                                        new
                                        {
                                            decFiltro = "1",
                                            strParametro1 = decIdMovimiento.ToString(),
                                            strParametro2 = strFechaInicioT,//detFechaIni.Date.Year + detFechaIni.Date.Month.ToString().PadLeft(2, '0') + detFechaIni.Date.Day.ToString().PadLeft(2, '0'),
                                            strParametro3 = strFechaFinT,//detFechaIni.Date.Year + detFechaIni.Date.Month.ToString().PadLeft(2, '0') + detFechaIni.Date.Day.ToString().PadLeft(2, '0'),
                                            strParametro4 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
                                            strParametro5 = "0"
                                        });
                                        if (objTransaccionFosil != null && objTransaccionFosil.intCodigo == 1)
                                        {
                                            List<O_LISTA_TRANSACCION_CTY> lstResultadoFosil = objTransaccionFosil.oResultado;

                                            if ((List<O_LISTA_TRANSACCION_CTY>)Session[CVariablesSesion.GrillaFosil] != null)
                                            {
                                                lstResultadoFosilTotal = (List<O_LISTA_TRANSACCION_CTY>)Session[CVariablesSesion.GrillaFosil];
                                            }

                                            foreach (O_LISTA_TRANSACCION_CTY datos in lstResultadoFosil)
                                            {
                                                lstResultadoFosilTotal.Add(datos);
                                            }

                                            Session[CVariablesSesion.GrillaFosil] = lstResultadoFosilTotal;

                                        }
                                        else { }
                                        break;
                                    case 367: //test 367 desarrollo 367 produccion 367
                                    case 368: //test 368 desarrollo 368 produccion 368
                                    case 369: //test 369 desarrollo 369 produccion 369
                                    case 370: //test 370 desarrollo 370 produccion 370
                                    case 371: //test 371 desarrollo 371 produccion 371
                                    case 372: //test 372 desarrollo 372 produccion 372
                                    case 373: //test 374 desarrollo 374 produccion 373
                                    case 374: //test 380 desarrollo 380 produccion 374
                                    case 375: //test 382 desarrollo 382 produccion 375
                                    case 376: //test 383 desarrollo 383 produccion 376
                                    case 377: //test 384 desarrollo 384 produccion 377
                                    case 378: //test 385 desarrollo 385 produccion 378
                                    case 379: //test 386 desarrollo 386 produccion 379
                                    case 380: //test 387 desarrollo 387 produccion 380
                                    case 381: //test 388 desarrollo 388 produccion 381
                                    case 382: //test 389 desarrollo 389 produccion 382

                                        if (dato.OBSERVACION_MOVIMIENTO.StartsWith(strDuiSeleccionado))
                                        {
                                            lstResultadoMovBase.Add(dato);

                                            var objTransaccionBase = _client.Post<EResultadoTransaccion>(CAppSettings.svc_obtener_transaccion,
                                            new
                                            {
                                                decFiltro = "1",
                                                strParametro1 = decIdMovimiento.ToString(),
                                                strParametro2 = strFechaInicioT,//detFechaIni.Date.Year + detFechaIni.Date.Month.ToString().PadLeft(2, '0') + detFechaIni.Date.Day.ToString().PadLeft(2, '0'),
                                                strParametro3 = strFechaFinT,//detFechaIni.Date.Year + detFechaIni.Date.Month.ToString().PadLeft(2, '0') + detFechaIni.Date.Day.ToString().PadLeft(2, '0'),
                                                strParametro4 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
                                                strParametro5 = "0"
                                            });
                                            if (objTransaccionBase != null && objTransaccionBase.intCodigo == 1)
                                            {
                                                //List<O_LISTA_TRANSACCION_CTY> lstResultado2 = objTransaccionBase.oResultado;

                                                //Session[CVariablesSesion.Grillabase] = lstResultado2;


                                                List<O_LISTA_TRANSACCION_CTY> lstResultadoBaseTran = objTransaccionBase.oResultado;

                                                if ((List<O_LISTA_TRANSACCION_CTY>)Session[CVariablesSesion.Grillabase] != null)
                                                {
                                                    lstResultadoBaseTotal = (List<O_LISTA_TRANSACCION_CTY>)Session[CVariablesSesion.Grillabase];
                                                }

                                                foreach (O_LISTA_TRANSACCION_CTY datos in lstResultadoBaseTran)
                                                {
                                                    lstResultadoBaseTotal.Add(datos);
                                                }

                                                Session[CVariablesSesion.Grillabase] = lstResultadoBaseTotal;


                                            }
                                            else { }
                                        }

                                        break;
                                    case 278:

                                        lstResultadoMovGasBlanca.Add(dato);

                                        var objTransaccionGasolinaBlanca = _client.Post<EResultadoTransaccion>(CAppSettings.svc_obtener_transaccion,
                                            new
                                            {
                                                decFiltro = "1",
                                                strParametro1 = decIdMovimiento.ToString(),
                                                strParametro2 = strFechaInicioT,//detFechaIni.Date.Year + detFechaIni.Date.Month.ToString().PadLeft(2, '0') + detFechaIni.Date.Day.ToString().PadLeft(2, '0'),
                                                strParametro3 = strFechaFinT,//detFechaIni.Date.Year + detFechaIni.Date.Month.ToString().PadLeft(2, '0') + detFechaIni.Date.Day.ToString().PadLeft(2, '0'),
                                                strParametro4 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
                                                strParametro5 = "0"
                                            });
                                        if (objTransaccionGasolinaBlanca != null && objTransaccionGasolinaBlanca.intCodigo == 1)
                                        {
                                            List<O_LISTA_TRANSACCION_CTY> lstResultado2 = objTransaccionGasolinaBlanca.oResultado;

                                            Session[CVariablesSesion.GrillaGasolinaBlanca] = lstResultado2;
                                        }
                                        else { }
                                        break;
                                    default:

                                        lstResultadoMovOtro.Add(dato);

                                        var objTransaccionOtro = _client.Post<EResultadoTransaccion>(CAppSettings.svc_obtener_transaccion,
                                            new
                                            {
                                                decFiltro = "1",
                                                strParametro1 = decIdMovimiento.ToString(),
                                                strParametro2 = strFechaInicioT,//detFechaIni.Date.Year + detFechaIni.Date.Month.ToString().PadLeft(2, '0') + detFechaIni.Date.Day.ToString().PadLeft(2, '0'),
                                                strParametro3 = strFechaFinT,//detFechaIni.Date.Year + detFechaIni.Date.Month.ToString().PadLeft(2, '0') + detFechaIni.Date.Day.ToString().PadLeft(2, '0'),
                                                strParametro4 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
                                                strParametro5 = "0"
                                            });
                                        if (objTransaccionOtro != null && objTransaccionOtro.intCodigo == 1)
                                        {
                                            List<O_LISTA_TRANSACCION_CTY> lstResultado2 = objTransaccionOtro.oResultado;

                                            Session[CVariablesSesion.GrillaOtro] = lstResultado2;
                                        }
                                        else { }
                                        break;
                                }
                            }

                            var lstDocumento = new List<O_LISTA_TRANSACCION_CTY>();
                            var lstDocumentoGb = new List<O_LISTA_TRANSACCION_CTY>();

                            if (decOpcion == 1)
                            {
                                if (Session[CVariablesSesion.Grillabase] != null)
                                {
                                    lstDocumento = (List<O_LISTA_TRANSACCION_CTY>)Session[CVariablesSesion.Grillabase];
                                }
                                else { }
                            }
                            else
                            {
                                if (Session[CVariablesSesion.GrillaFosil] != null)
                                {
                                    lstDocumento = (List<O_LISTA_TRANSACCION_CTY>)Session[CVariablesSesion.GrillaFosil];
                                }

                                if (Session[CVariablesSesion.GrillaGasolinaBlanca] != null)
                                {
                                    lstDocumentoGb = (List<O_LISTA_TRANSACCION_CTY>)Session[CVariablesSesion.GrillaGasolinaBlanca];
                                }
                                else { }
                            }

                            var objNuevoDatoB1 = new List<O_OBTENER_ANEXO_B1>();
                            var objNuevoDatoB1Gb = new List<O_OBTENER_ANEXO_B1>();

                            var objNuevoDatoB2 = new List<O_OBTENER_ANEXO_B2>();
                            var objNuevoDatoB2Gb = new List<O_OBTENER_ANEXO_B2>();
                            var objNuevoDatoB2GbD = new List<O_OBTENER_ANEXO_B2_DU>();

                            var objNuevoDatoB3 = new List<O_OBTENER_ANEXO_B3>();

                            var strProveedor = "";
                            var strPlantaDespacho = "";
                            var strPlantaRecepcion = "";


                            if (lstDocumento != null)
                            {
                                //ANEXO A
                                #region ANEXO A

                                var lstrProveedor = new List<string>();

                                lstDocumento = lstDocumento.Where(x => x.OBSERVACIONES != null).ToList();

                                var objMovA = lstDocumento.Where(x => x.OBSERVACIONES.StartsWith("ACCP")).ToList();

                                foreach (var reg in objMovA)
                                {
                                    var objTrasaccion = new O_OBTENER_ANEXO_A();
                                    var objVol = new O_OBTENER_VOLUMEN_IMPORTADO();

                                    var parametros = reg.OBSERVACIONES;

                                    string[] lista = parametros.Split('|');

                                    objTrasaccion.NRO_DUI = lista[1].ToString();
                                    objTrasaccion.PROVEEDOR = reg.RAZON_SOCIAL;
                                    objTrasaccion.RON = reg.CODIGO;
                                    objTrasaccion.MES = ObtenerPeriodo(reg.FECHA_HORA_EMISION.ToString());
                                    objTrasaccion.NRO_FACTURA = reg.NRO_TRANSACCION;
                                    objTrasaccion.VOLUMEN_FACTURADO = Convert.ToDecimal(lista[5].ToString());
                                    objTrasaccion.VALOR_FACTURADO = reg.PRECIO_TOTAL;

                                    objNuevoDatoA.Add(objTrasaccion);

                                    lstrProveedor.Add(reg.RAZON_SOCIAL);

                                    if (decOpcion == 2)
                                    {
                                        objVol.IDMOVIMIENTO = reg.ID_MOVIMIENTO;
                                        objVol.RON = reg.CODIGO;
                                        objVol.VOLUMEN = Convert.ToDecimal(lista[5].ToString());
                                        objVol.COSTO = reg.PRECIO_TOTAL;

                                        objVolImportA.Add(objVol);

                                    }
                                    else
                                    {

                                        //var registrosBase = lstDocumento;

                                        //decIdMovimiento = Convert.ToDecimal(registrosBase[0].ID_MOVIMIENTO);

                                        //if (reg.ID_MOVIMIENTO == decIdMovimiento)
                                        //{
                                        objVol.IDMOVIMIENTO = reg.ID_MOVIMIENTO;
                                        objVol.RON = reg.CODIGO;
                                        objVol.VOLUMEN = Convert.ToDecimal(lista[5].ToString());
                                        objVol.COSTO = reg.PRECIO_TOTAL;

                                        objVolImportA.Add(objVol);
                                        //}
                                    }


                                }

                                Session[CVariablesSesion.GrillaAnexoA] = objNuevoDatoA;

                                if (decOpcion == 2)
                                {
                                    lstDocumentoGb = lstDocumentoGb.Where(x => x.OBSERVACIONES != null).ToList();

                                    var objMovAnGb = lstDocumentoGb.Where(x => x.OBSERVACIONES.StartsWith("ACCP")).ToList();

                                    foreach (var reg in objMovAnGb)
                                    {
                                        var objTrasaccion = new O_OBTENER_ANEXO_A_GB();
                                        var objVol = new O_OBTENER_VOLUMEN_IMPORTADO();

                                        var parametros = reg.OBSERVACIONES;

                                        string[] lista = parametros.Split('|');

                                        objTrasaccion.PRODUCTO = "GASOLINA BLANCA";
                                        objTrasaccion.MES_RECIBIDO = ObtenerPeriodo(reg.FECHA_HORA_EMISION.ToString());
                                        objTrasaccion.VOLUMEN = Convert.ToDecimal(lista[5].ToString()).ToString().Replace(".", ",");
                                        objTrasaccion.PRECIO = reg.PRECIO_UNITARIO.ToString().Replace(".", ",");
                                        objTrasaccion.MONTO_NETO = (Convert.ToDecimal(lista[5].ToString().Replace(",", ".")) * Convert.ToDecimal(reg.PRECIO_UNITARIO.ToString().Replace(",", "."))).ToString();

                                        objNuevoDatoAnGb.Add(objTrasaccion);

                                        /*if (reg.ID_MOVIMIENTO == decIdMovimiento)
                                        {
                                            objVol.RON = reg.CODIGO;
                                            objVol.VOLUMEN = Convert.ToDecimal(lista[5].ToString());
                                            objVol.COSTO = reg.PRECIO_TOTAL;

                                            objVolImportA.Add(objVol);
                                        }*/
                                    }

                                    Session[CVariablesSesion.GrillaAnexoAnGb] = objNuevoDatoAnGb;
                                }

                                var lstProveedor = lstrProveedor.Distinct().ToList();
                                string strPro = "";

                                foreach (var itemPro in lstProveedor)
                                {
                                    if (strProveedor == "")
                                    { strProveedor = strProveedor + strPro; }
                                    else { strProveedor = strProveedor + ", " + strPro; }
                                    strPro = itemPro;
                                }

                                if (strProveedor == "")
                                { strProveedor = strProveedor + strPro; }
                                else { strProveedor = strProveedor + " y " + strPro; }


                                #endregion

                                //ANEXO B
                                #region ANEXO B

                                lstDocumento = lstDocumento.Where(x => x.OBSERVACIONES != null).ToList();
                                var objMovB1 = lstDocumento.Where(x => x.OBSERVACIONES.StartsWith("B1CCTF")).ToList();

                                var objVolB = new O_OBTENER_VOLUMEN_IMPORTADO();

                                foreach (var reg in objMovB1)
                                {
                                    var objTrasaccion = new O_OBTENER_ANEXO_B1();
                                    objVolB = new O_OBTENER_VOLUMEN_IMPORTADO();

                                    var parametros = reg.OBSERVACIONES;

                                    string[] lista = parametros.Split('|');

                                    var decVolDespachado = Convert.ToDecimal(lista[4].ToString());
                                    var decVolRecibido = Convert.ToDecimal(lista[5].ToString());

                                    var decVolDiferencia = decVolDespachado - decVolRecibido;

                                    var decMerma = decVolDiferencia <= 150 ? (decVolRecibido * Convert.ToDecimal("0.0025")) : 0;

                                    decimal decPreUni = lista[10].ToString() == "" ? 0 : Convert.ToDecimal(lista[10].ToString());

                                    objTrasaccion.NRO_DUI = lista[1].ToString();
                                    objTrasaccion.MES = reg.RAZON_SOCIAL;
                                    objTrasaccion.NRO_FACTURA_TRANSPORTE = reg.NRO_TRANSACCION;
                                    objTrasaccion.VOLUMEN_DESPACHADO = Convert.ToDecimal(lista[4].ToString());
                                    objTrasaccion.VOLUMEN_RECIBIDO = Convert.ToDecimal(lista[5].ToString());
                                    objTrasaccion.DIFERENCIA = Convert.ToDecimal(lista[4].ToString()) - Convert.ToDecimal(lista[5].ToString());
                                    objTrasaccion.MERMA_PERMISIBLE = Math.Round(decMerma, 0);
                                    objTrasaccion.MERMA_EXCEDENTE = Math.Round(decVolDiferencia - decMerma, 0) > 0 ? Math.Round(decVolDiferencia - decMerma, 0) : 0;
                                    objTrasaccion.FLETE = Convert.ToDecimal(lista[7].ToString());
                                    objTrasaccion.COSTO_PRODUCTO = decPreUni;
                                    objTrasaccion.TRANSPORTE_TOTAL = decVolRecibido * Convert.ToDecimal(lista[7].ToString());
                                    objTrasaccion.DESCUENTO_MERMA = (Math.Round(decVolDiferencia - decMerma, 0) > 0 ? Math.Round(decVolDiferencia - decMerma, 0) : 0) * Convert.ToDecimal(lista[10].ToString());
                                    objTrasaccion.COSTO_TRANSPORTE = (decVolRecibido * Convert.ToDecimal(lista[7].ToString())) - ((Math.Round(decVolDiferencia - decMerma, 0) > 0 ? Math.Round(decVolDiferencia - decMerma, 0) : 0) * decPreUni);

                                    objNuevoDatoB1.Add(objTrasaccion);

                                    //if (reg.ID_MOVIMIENTO == decIdMovimiento)
                                    //{
                                    objVolB.IDMOVIMIENTO = reg.ID_MOVIMIENTO;
                                    objVolB.RON = reg.CODIGO;
                                    objVolB.VOLUMEN = Convert.ToDecimal(lista[5].ToString());
                                    objVolB.COSTO = (decVolRecibido * Convert.ToDecimal(lista[7].ToString())) - ((Math.Round(decVolDiferencia - decMerma, 0) > 0 ? Math.Round(decVolDiferencia - decMerma, 0) : 0) * decPreUni);

                                    objVolImportB.Add(objVolB);
                                    //}

                                }

                                Session[CVariablesSesion.GrillaAnexoB1] = objNuevoDatoB1;

                                if (decOpcion == 2)
                                {
                                    lstDocumentoGb = lstDocumentoGb.Where(x => x.OBSERVACIONES != null).ToList();

                                    var objMovB1Gb = lstDocumentoGb.Where(x => x.OBSERVACIONES.StartsWith("B1CCTF")).ToList();


                                    foreach (var reg in objMovB1Gb)
                                    {
                                        var objTrasaccion = new O_OBTENER_ANEXO_B1();
                                        objVolB = new O_OBTENER_VOLUMEN_IMPORTADO();

                                        var parametros = reg.OBSERVACIONES;

                                        string[] lista = parametros.Split('|');

                                        decimal decPreUni = lista[10].ToString() == "" ? 0 : Convert.ToDecimal(lista[10].ToString());

                                        var decVolDespachado = Convert.ToDecimal(lista[4].ToString());
                                        var decVolRecibido = Convert.ToDecimal(lista[5].ToString());

                                        var decVolDiferencia = decVolDespachado - decVolRecibido;

                                        var decMerma = decVolDiferencia <= 150 ? (decVolRecibido * Convert.ToDecimal("0.0025")) : 0;

                                        objTrasaccion.NRO_DUI = "";
                                        objTrasaccion.MES = reg.RAZON_SOCIAL;
                                        objTrasaccion.NRO_FACTURA_TRANSPORTE = reg.NRO_TRANSACCION;
                                        objTrasaccion.VOLUMEN_DESPACHADO = Convert.ToDecimal(lista[4].ToString());
                                        objTrasaccion.VOLUMEN_RECIBIDO = Convert.ToDecimal(lista[5].ToString());
                                        objTrasaccion.DIFERENCIA = Convert.ToDecimal(lista[4].ToString()) - Convert.ToDecimal(lista[5].ToString());
                                        objTrasaccion.MERMA_PERMISIBLE = Math.Round(decMerma, 0);
                                        objTrasaccion.MERMA_EXCEDENTE = Math.Round(decVolDiferencia - decMerma, 0) > 0 ? Math.Round(decVolDiferencia - decMerma, 0) : 0;
                                        objTrasaccion.FLETE = Convert.ToDecimal(lista[7].ToString());
                                        objTrasaccion.COSTO_PRODUCTO = decPreUni;
                                        objTrasaccion.TRANSPORTE_TOTAL = decVolRecibido * Convert.ToDecimal(lista[7].ToString());
                                        objTrasaccion.DESCUENTO_MERMA = (Math.Round(decVolDiferencia - decMerma, 0) > 0 ? Math.Round(decVolDiferencia - decMerma, 0) : 0) * decPreUni;
                                        objTrasaccion.COSTO_TRANSPORTE = (decVolRecibido * Convert.ToDecimal(lista[7].ToString())) - ((Math.Round(decVolDiferencia - decMerma, 0) > 0 ? Math.Round(decVolDiferencia - decMerma, 0) : 0) * decPreUni);

                                        objNuevoDatoB1Gb.Add(objTrasaccion);

                                        /*if (reg.ID_MOVIMIENTO == decIdMovimiento)
                                        {
                                            objVolB.RON = reg.CODIGO;
                                            objVolB.VOLUMEN = Convert.ToDecimal(lista[5].ToString());
                                            objVolB.COSTO = (decVolRecibido * Convert.ToDecimal(lista[7].ToString())) - ((Math.Round(decVolDiferencia - decMerma, 0) > 0 ? Math.Round(decVolDiferencia - decMerma, 0) : 0) * reg.PRECIO_UNITARIO);

                                            objVolImportB.Add(objVolB);
                                        }*/
                                    }

                                    Session[CVariablesSesion.GrillaAnexoB1Gb] = objNuevoDatoB1Gb;

                                }

                                var objMovB2 = lstDocumento.Where(x => x.OBSERVACIONES.StartsWith("B2CCTT")).ToList();

                                var lstrPlantaOrigen = new List<string>();
                                var lstrPlantaDestino = new List<string>();

                                foreach (var reg in objMovB2)
                                {
                                    var objTrasaccion = new O_OBTENER_ANEXO_B2();
                                    objVolB = new O_OBTENER_VOLUMEN_IMPORTADO();

                                    var parametros = reg.OBSERVACIONES;

                                    string[] lista = parametros.Split('|');

                                    string[] listaProveedor = lista[6].Split('-');

                                    var decVolDespachado = Convert.ToDecimal(lista[4].ToString());
                                    var decVolRecibido = Convert.ToDecimal(lista[5].ToString());

                                    decimal decPreUni = lista[10].ToString() == "" ? 0 : Convert.ToDecimal(lista[10].ToString());

                                    var decVolDiferencia = decVolDespachado - decVolRecibido;

                                    var decMerma = decVolDiferencia <= 150 ? (decVolRecibido * Convert.ToDecimal("0.0025")) : 0;

                                    objTrasaccion.NRO_DUI = lista[1].ToString();
                                    if (listaProveedor.Count() == 2)
                                    {
                                        objTrasaccion.NRO_FACTURA_PROVEEDOR = listaProveedor[0].ToString();
                                    }
                                    else
                                    {
                                        objTrasaccion.NRO_FACTURA_PROVEEDOR = "";
                                    }

                                    objTrasaccion.EMPRESA_TRANSPORTE = reg.RAZON_SOCIAL;
                                    objTrasaccion.NRO_FACTURA_TRANSPORTE = reg.NRO_TRANSACCION;
                                    objTrasaccion.PLANTA_DESPACHO = lista[2].ToString();
                                    objTrasaccion.PLANTA_RECEPCION = lista[3].ToString();
                                    objTrasaccion.VOLUMEN_DESPACHADO = decVolDespachado;
                                    objTrasaccion.VOLUMEN_RECIBIDO = decVolRecibido;
                                    objTrasaccion.DIFERENCIA = decVolDiferencia * -1;
                                    objTrasaccion.MERMA_PERMISIBLE = Math.Round(decMerma, 0);
                                    objTrasaccion.MERMA_EXCEDENTE = Math.Round(decVolDiferencia - decMerma, 0) > 0 ? Math.Round(decVolDiferencia - decMerma, 0) : 0;
                                    objTrasaccion.FLETE = Convert.ToDecimal(lista[7].ToString());
                                    objTrasaccion.COSTO_PRODUCTO = decPreUni;
                                    objTrasaccion.TRANSPORTE_TOTAL = decVolRecibido * Convert.ToDecimal(lista[7].ToString());
                                    objTrasaccion.DESCUENTO_MERMA = (Math.Round(decVolDiferencia - decMerma, 0) > 0 ? Math.Round(decVolDiferencia - decMerma, 0) : 0) * decPreUni;
                                    objTrasaccion.COSTO_TRANSPORTE = (decVolRecibido * Convert.ToDecimal(lista[7].ToString())) - ((Math.Round(decVolDiferencia - decMerma, 0) > 0 ? Math.Round(decVolDiferencia - decMerma, 0) : 0) * decPreUni);

                                    objNuevoDatoB2.Add(objTrasaccion);

                                    //if (reg.ID_MOVIMIENTO == decIdMovimiento)
                                    //{
                                    objVolB.IDMOVIMIENTO = reg.ID_MOVIMIENTO;
                                    objVolB.RON = reg.CODIGO;
                                    objVolB.VOLUMEN = 0;
                                    objVolB.COSTO = (decVolRecibido * Convert.ToDecimal(lista[7].ToString())) - ((Math.Round(decVolDiferencia - decMerma, 0) > 0 ? Math.Round(decVolDiferencia - decMerma, 0) : 0) * decPreUni);

                                    objVolImportB.Add(objVolB);
                                    //}

                                    txtPlanta = lista[3].ToString();

                                    lstrPlantaOrigen.Add(lista[2].ToString());
                                    lstrPlantaDestino.Add(lista[3].ToString());
                                }

                                Session[CVariablesSesion.GrillaAnexoB2] = objNuevoDatoB2;

                                var lstPlantaO = lstrPlantaOrigen.Distinct().ToList();
                                string strPlantaO = "";
                                var lstPlantaD = lstrPlantaDestino.Distinct().ToList();
                                string strPlantaD = "";

                                foreach (var itemO in lstPlantaO)
                                {
                                    if (strPlantaDespacho == "")
                                    { strPlantaDespacho = strPlantaDespacho + strPlantaO; }
                                    else { strPlantaDespacho = strPlantaDespacho + ", " + strPlantaO; }
                                    strPlantaO = itemO;
                                }

                                if (strPlantaDespacho == "")
                                { strPlantaDespacho = strPlantaDespacho + strPlantaO; }
                                else { strPlantaDespacho = strPlantaDespacho + " y " + strPlantaO; }

                                foreach (var itemD in lstPlantaD)
                                {
                                    if (strPlantaRecepcion == "")
                                    { strPlantaRecepcion = strPlantaRecepcion + strPlantaD; }
                                    else { strPlantaRecepcion = strPlantaRecepcion + ", " + strPlantaD; }
                                    strPlantaD = itemD;
                                }

                                if (strPlantaRecepcion == "")
                                { strPlantaRecepcion = strPlantaRecepcion + strPlantaD; }
                                else { strPlantaRecepcion = strPlantaRecepcion + " y " + strPlantaD; }



                                if (decOpcion == 2)
                                {
                                    lstDocumentoGb = lstDocumentoGb.Where(x => x.OBSERVACIONES != null).ToList();

                                    var objMovB2Gb = lstDocumentoGb.Where(x => x.OBSERVACIONES.StartsWith("B2CCTT")).ToList();

                                    foreach (var reg in objMovB2Gb)
                                    {
                                        var objTrasaccion = new O_OBTENER_ANEXO_B2();
                                        objVolB = new O_OBTENER_VOLUMEN_IMPORTADO();

                                        var parametros = reg.OBSERVACIONES;

                                        string[] lista = parametros.Split('|');

                                        string[] listaProveedor = lista[6].Split('-');

                                        var decVolDespachado = Convert.ToDecimal(lista[4].ToString());
                                        var decVolRecibido = Convert.ToDecimal(lista[5].ToString());

                                        decimal decPreUni = lista[10].ToString() == "" ? 0 : Convert.ToDecimal(lista[10].ToString());

                                        var decVolDiferencia = decVolDespachado - decVolRecibido;

                                        var decMerma = decVolDiferencia <= 150 ? (decVolRecibido * Convert.ToDecimal("0.0025")) : 0;

                                        objTrasaccion.NRO_DUI = lista[1].ToString();
                                        if (listaProveedor.Count() == 2)
                                        {
                                            objTrasaccion.NRO_FACTURA_PROVEEDOR = listaProveedor[0].ToString();
                                        }
                                        else
                                        {
                                            objTrasaccion.NRO_FACTURA_PROVEEDOR = "";
                                        }

                                        objTrasaccion.EMPRESA_TRANSPORTE = reg.RAZON_SOCIAL;
                                        objTrasaccion.NRO_FACTURA_TRANSPORTE = reg.NRO_TRANSACCION;
                                        objTrasaccion.PLANTA_DESPACHO = lista[2].ToString();
                                        objTrasaccion.PLANTA_RECEPCION = lista[3].ToString();
                                        objTrasaccion.VOLUMEN_DESPACHADO = decVolDespachado;
                                        objTrasaccion.VOLUMEN_RECIBIDO = decVolRecibido;
                                        objTrasaccion.DIFERENCIA = decVolDiferencia * -1;
                                        objTrasaccion.MERMA_PERMISIBLE = Math.Round(decMerma, 0);
                                        objTrasaccion.MERMA_EXCEDENTE = Math.Round(decVolDiferencia - decMerma, 0) > 0 ? Math.Round(decVolDiferencia - decMerma, 0) : 0;
                                        objTrasaccion.FLETE = Convert.ToDecimal(lista[7].ToString());
                                        objTrasaccion.COSTO_PRODUCTO = decPreUni;
                                        objTrasaccion.TRANSPORTE_TOTAL = decVolRecibido * Convert.ToDecimal(lista[7].ToString());
                                        objTrasaccion.DESCUENTO_MERMA = (Math.Round(decVolDiferencia - decMerma, 0) > 0 ? Math.Round(decVolDiferencia - decMerma, 0) : 0) * decPreUni;
                                        objTrasaccion.COSTO_TRANSPORTE = (decVolRecibido * Convert.ToDecimal(lista[7].ToString())) - ((Math.Round(decVolDiferencia - decMerma, 0) > 0 ? Math.Round(decVolDiferencia - decMerma, 0) : 0) * decPreUni);

                                        objNuevoDatoB2Gb.Add(objTrasaccion);

                                        /*if (reg.ID_MOVIMIENTO == decIdMovimiento)
                                        {
                                            objVolB.RON = reg.CODIGO;
                                            objVolB.VOLUMEN = 0;
                                            objVolB.COSTO = (decVolRecibido * Convert.ToDecimal(lista[7].ToString())) - ((Math.Round(decVolDiferencia - decMerma, 0) > 0 ? Math.Round(decVolDiferencia - decMerma, 0) : 0) * reg.PRECIO_UNITARIO);

                                            objVolImportB.Add(objVolB);
                                        }*/

                                    }

                                    Session[CVariablesSesion.GrillaAnexoB2Gbc] = objNuevoDatoB2Gb;

                                    var objMovB2GbD = lstDocumentoGb.Where(x => x.OBSERVACIONES.StartsWith("B2GBCCTTD")).ToList();

                                    foreach (var reg in objMovB2GbD)
                                    {
                                        var objTrasaccion = new O_OBTENER_ANEXO_B2_DU();
                                        objVolB = new O_OBTENER_VOLUMEN_IMPORTADO();

                                        var parametros = reg.OBSERVACIONES;

                                        string[] lista = parametros.Split('|');

                                        decimal decPreUni = lista[10].ToString() == "" ? 0 : Convert.ToDecimal(lista[10].ToString());

                                        string[] listaProveedor = lista[6].Split('-');

                                        objTrasaccion.PRODUCTO = "GASOLINA BLANCA";
                                        objTrasaccion.MES_RECIBIDO = ObtenerPeriodo(reg.FECHA_HORA_EMISION.ToString());
                                        objTrasaccion.VOLUMEN = Convert.ToDecimal(lista[4].ToString()).ToString().Replace(".", ",");
                                        objTrasaccion.PRECIO = decPreUni.ToString().Replace(".", ",");
                                        objTrasaccion.MONTO = (decPreUni * Convert.ToDecimal(lista[4].ToString())).ToString();

                                        objNuevoDatoB2GbD.Add(objTrasaccion);

                                        /*if (reg.ID_MOVIMIENTO == decIdMovimiento)
                                        {
                                            objVolB.RON = reg.CODIGO;
                                            objVolB.VOLUMEN = 0;
                                            objVolB.COSTO = (decVolRecibido * Convert.ToDecimal(lista[7].ToString())) - ((Math.Round(decVolDiferencia - decMerma, 0) > 0 ? Math.Round(decVolDiferencia - decMerma, 0) : 0) * reg.PRECIO_UNITARIO);

                                            objVolImportB.Add(objVolB);
                                        }*/

                                    }

                                    Session[CVariablesSesion.GrillaAnexoB2GbD] = objNuevoDatoB2GbD;
                                }


                                var objMovB3 = lstDocumento.Where(x => x.OBSERVACIONES.StartsWith("B3CCTF")).ToList();

                                //var objNuevoDatoB3 = new List<O_OBTENER_ANEXO_B3>();
                                foreach (var reg in objMovB3)
                                {
                                    var objTrasaccion = new O_OBTENER_ANEXO_B3();
                                    objVolB = new O_OBTENER_VOLUMEN_IMPORTADO();

                                    var parametros = reg.OBSERVACIONES;

                                    string[] lista = parametros.Split('|');

                                    string[] listaProveedor = lista[6].Split('-');

                                    objTrasaccion.NRO_DUI = lista[1].ToString();

                                    if (listaProveedor.Count() == 2)
                                    {
                                        objTrasaccion.PROVEEDOR = listaProveedor[1].ToString();
                                        objTrasaccion.NRO_FACTURA = listaProveedor[0].ToString();
                                    }
                                    else
                                    {
                                        objTrasaccion.PROVEEDOR = "";
                                        objTrasaccion.NRO_FACTURA = "";
                                    }
                                    objTrasaccion.NRO_FACTURA_PROVEEDOR = listaProveedor[0].ToString();
                                    objTrasaccion.PROVEEDOR = listaProveedor[1].ToString();
                                    objTrasaccion.EMPRESA = reg.RAZON_SOCIAL;
                                    objTrasaccion.NRO_FACTURA = reg.NRO_TRANSACCION;
                                    objTrasaccion.VOLUMEN_DESTINO1 = Convert.ToDecimal(lista[4].ToString());
                                    objTrasaccion.VOLUMEN_DESTINO2 = Convert.ToDecimal(lista[5].ToString());
                                    objTrasaccion.FLETE = Convert.ToDecimal(lista[7].ToString());
                                    objTrasaccion.COSTO_FACTURADO = (Convert.ToDecimal(lista[5].ToString())) * (Convert.ToDecimal(lista[7].ToString()));
                                    objTrasaccion.MONTO_NETO_IVA = ((Convert.ToDecimal(lista[5].ToString())) * (Convert.ToDecimal(lista[7].ToString()))) * Convert.ToDecimal("0.87");

                                    objNuevoDatoB3.Add(objTrasaccion);

                                    //if (reg.ID_MOVIMIENTO == decIdMovimiento)
                                    //{
                                    objVolB.IDMOVIMIENTO = reg.ID_MOVIMIENTO;
                                    objVolB.RON = reg.CODIGO;
                                    objVolB.VOLUMEN = 0;
                                    objVolB.COSTO = ((Convert.ToDecimal(lista[5].ToString())) * (Convert.ToDecimal(lista[7].ToString()))) * Convert.ToDecimal("0.87");

                                    objVolImportB.Add(objVolB);
                                    //}
                                }

                                Session[CVariablesSesion.GrillaAnexoB3] = objNuevoDatoB3;

                                #endregion

                                //ANEXO C
                                #region ANEXO C
                                var objMovC = lstDocumento.Where(x => x.OBSERVACIONES.StartsWith("CCS")).ToList();

                                var objNuevoDatoC = new List<O_OBTENER_ANEXO_C>();
                                foreach (var reg in objMovC)
                                {
                                    var objTrasaccion = new O_OBTENER_ANEXO_C();

                                    var objVolC = new O_OBTENER_VOLUMEN_IMPORTADO();

                                    var parametros = reg.OBSERVACIONES;

                                    string[] lista = parametros.Split('|');

                                    objTrasaccion.NRO_DUI = lista[1].ToString();
                                    objTrasaccion.EMPRESA_ASEGURADORA = reg.RAZON_SOCIAL;
                                    objTrasaccion.PERIODO_COBERTURA = "";
                                    objTrasaccion.NRO_FACTURA_ASEGURADORA = reg.NRO_TRANSACCION;
                                    objTrasaccion.MONTO_FACTURADO = reg.PRECIO_TOTAL;
                                    objTrasaccion.MONTO_NETO_IVA = reg.PRECIO_TOTAL * Convert.ToDecimal("0.87");

                                    objNuevoDatoC.Add(objTrasaccion);

                                    //if (reg.ID_MOVIMIENTO == decIdMovimiento)
                                    //{
                                    objVolC.IDMOVIMIENTO = reg.ID_MOVIMIENTO;
                                    objVolC.RON = reg.CODIGO;
                                    objVolC.VOLUMEN = 0;
                                    objVolC.COSTO = reg.PRECIO_TOTAL * Convert.ToDecimal("0.87");

                                    objVolImportC.Add(objVolC);
                                    //}
                                }

                                Session[CVariablesSesion.GrillaAnexoC] = objNuevoDatoC;

                                #endregion

                                //ANEXO D
                                #region ANEXO D
                                var objMovD1 = lstDocumento.Where(x => x.OBSERVACIONES.StartsWith("D1IO")).ToList();

                                var objNuevoDatoD1 = new List<O_OBTENER_ANEXO_D>();
                                var objNuevoDatoD1Gb = new List<O_OBTENER_ANEXO_D>();
                                var objNuevoDatoD1Total = new List<O_OBTENER_ANEXO_D>();
                                foreach (var reg in objMovD1)
                                {
                                    var objVolD = new O_OBTENER_VOLUMEN_IMPORTADO();

                                    var objTrasaccion = new O_OBTENER_ANEXO_D();

                                    var parametros = reg.OBSERVACIONES;

                                    string[] lista = parametros.Split('|');

                                    string[] listaProveedor = lista[6].Split('-');

                                    string fecha = reg.FECHA_HORA_EMISION.ToString();

                                    objTrasaccion.MES_CARGA = Convert.ToDateTime(fecha.Substring(0, 4) + "/" + fecha.Substring(4, 2) + "/" + fecha.Substring(6, 2)).ToString("MMMM - yyyy");
                                    objTrasaccion.NRO_DUI = lista[1].ToString();
                                    if (listaProveedor.Count() == 2)
                                    {
                                        objTrasaccion.PROVEEDOR = listaProveedor[1].ToString();
                                        objTrasaccion.NRO_FACTURA = listaProveedor[0].ToString();
                                    }
                                    else
                                    {
                                        objTrasaccion.PROVEEDOR = "";
                                        objTrasaccion.NRO_FACTURA = "";
                                    }
                                    objTrasaccion.VOLUMEN_FACTURADO = Convert.ToDecimal(lista[5].ToString());
                                    objTrasaccion.MONTO_INSPECCION = reg.PRECIO_TOTAL;

                                    objNuevoDatoD1.Add(objTrasaccion);
                                    objNuevoDatoD1Total.Add(objTrasaccion);

                                    //if (reg.ID_MOVIMIENTO == decIdMovimiento)
                                    //{
                                    objVolD.IDMOVIMIENTO = reg.ID_MOVIMIENTO;
                                    objVolD.RON = reg.CODIGO;
                                    objVolD.VOLUMEN = 1;
                                    objVolD.COSTO = reg.PRECIO_TOTAL;

                                    objVolImportD.Add(objVolD);
                                    //}


                                }

                                if (decOpcion == 2)
                                {
                                    lstDocumentoGb = lstDocumentoGb.Where(x => x.OBSERVACIONES != null).ToList();

                                    var objMovGnGb = lstDocumentoGb.Where(x => x.OBSERVACIONES.StartsWith("D1IO")).ToList();

                                    foreach (var reg in objMovGnGb)
                                    {
                                        var objVolD = new O_OBTENER_VOLUMEN_IMPORTADO();

                                        var objTrasaccion = new O_OBTENER_ANEXO_D();

                                        var parametros = reg.OBSERVACIONES;

                                        string[] lista = parametros.Split('|');

                                        string[] listaProveedor = lista[6].Split('-');

                                        string fecha = reg.FECHA_HORA_EMISION.ToString();

                                        objTrasaccion.MES_CARGA = Convert.ToDateTime(fecha.Substring(0, 4) + "/" + fecha.Substring(4, 2) + "/" + fecha.Substring(6, 2)).ToString("MMMM - yyyy");
                                        objTrasaccion.NRO_DUI = "GASOLINA BLANCA";
                                        objTrasaccion.PROVEEDOR = "GASOLINA BLANCA";
                                        objTrasaccion.NRO_FACTURA = "GASOLINA BLANCA";
                                        objTrasaccion.VOLUMEN_FACTURADO = Convert.ToDecimal(lista[5].ToString());
                                        objTrasaccion.MONTO_INSPECCION = reg.PRECIO_TOTAL;

                                        objNuevoDatoD1Gb.Add(objTrasaccion);
                                        objNuevoDatoD1Total.Add(objTrasaccion);

                                        /*if (reg.ID_MOVIMIENTO == decIdMovimiento)
                                        {
                                            objVolD.RON = reg.CODIGO;
                                            objVolD.VOLUMEN = 1;
                                            objVolD.COSTO = reg.PRECIO_TOTAL;

                                            objVolImportD.Add(objVolD);
                                        }*/
                                    }

                                    Session[CVariablesSesion.GrillaAnexoD1Gb] = objNuevoDatoD1Gb;
                                }

                                Session[CVariablesSesion.GrillaAnexoD1] = objNuevoDatoD1;

                                var objMovD2 = lstDocumento.Where(x => x.OBSERVACIONES.StartsWith("D2II")).ToList();

                                var objNuevoDatoD2 = new List<O_OBTENER_ANEXO_D>();
                                var objNuevoDatoD2Gb = new List<O_OBTENER_ANEXO_D>();
                                var objNuevoDatoD2Total = new List<O_OBTENER_ANEXO_D>();

                                foreach (var reg in objMovD2)
                                {
                                    var objVolD = new O_OBTENER_VOLUMEN_IMPORTADO();

                                    var objTrasaccion = new O_OBTENER_ANEXO_D();

                                    var parametros = reg.OBSERVACIONES;

                                    string[] lista = parametros.Split('|');

                                    string[] listaProveedor = lista[6].Split('-');

                                    string fecha = reg.FECHA_HORA_EMISION.ToString();

                                    objTrasaccion.MES_CARGA = Convert.ToDateTime(fecha.Substring(0, 4) + "/" + fecha.Substring(4, 2) + "/" + fecha.Substring(6, 2)).ToString("MMMM - yyyy");
                                    objTrasaccion.NRO_DUI = lista[1].ToString();
                                    if (listaProveedor.Count() == 2)
                                    {
                                        objTrasaccion.PROVEEDOR = listaProveedor[1].ToString();
                                        objTrasaccion.NRO_FACTURA = listaProveedor[0].ToString();
                                    }
                                    else
                                    {
                                        objTrasaccion.PROVEEDOR = "";
                                        objTrasaccion.NRO_FACTURA = "";
                                    }

                                    objTrasaccion.VOLUMEN_FACTURADO = Convert.ToDecimal(lista[5].ToString());
                                    objTrasaccion.MONTO_INSPECCION = reg.PRECIO_TOTAL;

                                    objNuevoDatoD2.Add(objTrasaccion);
                                    objNuevoDatoD2Total.Add(objTrasaccion);

                                    //if (reg.ID_MOVIMIENTO == decIdMovimiento)
                                    //{
                                    objVolD.IDMOVIMIENTO = reg.ID_MOVIMIENTO;
                                    objVolD.RON = reg.CODIGO;
                                    objVolD.VOLUMEN = 0;
                                    objVolD.COSTO = reg.PRECIO_TOTAL;

                                    objVolImportD.Add(objVolD);
                                    //}
                                }

                                if (decOpcion == 2)
                                {
                                    lstDocumentoGb = lstDocumentoGb.Where(x => x.OBSERVACIONES != null).ToList();

                                    var objMovGnGb = lstDocumentoGb.Where(x => x.OBSERVACIONES.StartsWith("D2II")).ToList();

                                    foreach (var reg in objMovGnGb)
                                    {
                                        var objVolD = new O_OBTENER_VOLUMEN_IMPORTADO();

                                        var objTrasaccion = new O_OBTENER_ANEXO_D();

                                        var parametros = reg.OBSERVACIONES;

                                        string[] lista = parametros.Split('|');

                                        string[] listaProveedor = lista[6].Split('-');

                                        string fecha = reg.FECHA_HORA_EMISION.ToString();

                                        objTrasaccion.MES_CARGA = Convert.ToDateTime(fecha.Substring(0, 4) + "/" + fecha.Substring(4, 2) + "/" + fecha.Substring(6, 2)).ToString("MMMM - yyyy");
                                        objTrasaccion.NRO_DUI = "GASOLINA BLANCA";
                                        objTrasaccion.PROVEEDOR = "GASOLINA BLANCA";
                                        objTrasaccion.NRO_FACTURA = "GASOLINA BLANCA";
                                        objTrasaccion.VOLUMEN_FACTURADO = Convert.ToDecimal(lista[5].ToString());
                                        objTrasaccion.MONTO_INSPECCION = reg.PRECIO_TOTAL;

                                        objNuevoDatoD2Gb.Add(objTrasaccion);
                                        objNuevoDatoD2Total.Add(objTrasaccion);

                                        /*if (reg.ID_MOVIMIENTO == decIdMovimiento)
                                        {
                                            objVolD.RON = reg.CODIGO;
                                            objVolD.VOLUMEN = 1;
                                            objVolD.COSTO = reg.PRECIO_TOTAL;

                                            objVolImportD.Add(objVolD);
                                        }*/
                                    }

                                    Session[CVariablesSesion.GrillaAnexoD2Gb] = objNuevoDatoD2Gb;
                                }

                                Session[CVariablesSesion.GrillaAnexoD2] = objNuevoDatoD2;

                                var objMovD3 = lstDocumento.Where(x => x.OBSERVACIONES.StartsWith("D3IDF")).ToList();

                                var objNuevoDatoD3 = new List<O_OBTENER_ANEXO_D>();
                                var objNuevoDatoD3Gb = new List<O_OBTENER_ANEXO_D>();
                                var objNuevoDatoD3Total = new List<O_OBTENER_ANEXO_D>();

                                foreach (var reg in objMovD3)
                                {
                                    var objVolD = new O_OBTENER_VOLUMEN_IMPORTADO();

                                    var objTrasaccion = new O_OBTENER_ANEXO_D();

                                    var parametros = reg.OBSERVACIONES;

                                    string[] lista = parametros.Split('|');

                                    string[] listaProveedor = lista[6].Split('-');

                                    string fecha = reg.FECHA_HORA_EMISION.ToString();

                                    objTrasaccion.MES_CARGA = Convert.ToDateTime(fecha.Substring(0, 4) + "/" + fecha.Substring(4, 2) + "/" + fecha.Substring(6, 2)).ToString("MMMM - yyyy");
                                    objTrasaccion.NRO_DUI = lista[1].ToString();
                                    if (listaProveedor.Count() == 2)
                                    {
                                        objTrasaccion.PROVEEDOR = listaProveedor[1].ToString();
                                        objTrasaccion.NRO_FACTURA = listaProveedor[0].ToString();
                                    }
                                    else
                                    {
                                        objTrasaccion.PROVEEDOR = "";
                                        objTrasaccion.NRO_FACTURA = "";
                                    }
                                    objTrasaccion.VOLUMEN_FACTURADO = Convert.ToDecimal(lista[5].ToString());
                                    objTrasaccion.MONTO_INSPECCION = reg.PRECIO_TOTAL;

                                    objNuevoDatoD3.Add(objTrasaccion);
                                    objNuevoDatoD3Total.Add(objTrasaccion);

                                    //if (reg.ID_MOVIMIENTO == decIdMovimiento)
                                    //{
                                    objVolD.IDMOVIMIENTO = reg.ID_MOVIMIENTO;
                                    objVolD.RON = reg.CODIGO;
                                    objVolD.VOLUMEN = 0;
                                    objVolD.COSTO = reg.PRECIO_TOTAL;

                                    objVolImportD.Add(objVolD);
                                    //}
                                }

                                if (decOpcion == 2)
                                {
                                    lstDocumentoGb = lstDocumentoGb.Where(x => x.OBSERVACIONES != null).ToList();

                                    var objMovGnGb = lstDocumentoGb.Where(x => x.OBSERVACIONES.StartsWith("D3IDF")).ToList();

                                    foreach (var reg in objMovGnGb)
                                    {
                                        var objVolD = new O_OBTENER_VOLUMEN_IMPORTADO();

                                        var objTrasaccion = new O_OBTENER_ANEXO_D();

                                        var parametros = reg.OBSERVACIONES;

                                        string[] lista = parametros.Split('|');

                                        string[] listaProveedor = lista[6].Split('-');

                                        string fecha = reg.FECHA_HORA_EMISION.ToString();

                                        objTrasaccion.MES_CARGA = Convert.ToDateTime(fecha.Substring(0, 4) + "/" + fecha.Substring(4, 2) + "/" + fecha.Substring(6, 2)).ToString("MMMM - yyyy");
                                        objTrasaccion.NRO_DUI = "GASOLINA BLANCA";
                                        objTrasaccion.PROVEEDOR = "GASOLINA BLANCA";
                                        objTrasaccion.NRO_FACTURA = "GASOLINA BLANCA";
                                        objTrasaccion.VOLUMEN_FACTURADO = Convert.ToDecimal(lista[5].ToString());
                                        objTrasaccion.MONTO_INSPECCION = reg.PRECIO_TOTAL;

                                        objNuevoDatoD3Gb.Add(objTrasaccion);
                                        objNuevoDatoD3Total.Add(objTrasaccion);

                                        /*if (reg.ID_MOVIMIENTO == decIdMovimiento)
                                        {
                                            objVolD.RON = reg.CODIGO;
                                            objVolD.VOLUMEN = 1;
                                            objVolD.COSTO = reg.PRECIO_TOTAL;

                                            objVolImportD.Add(objVolD);
                                        }*/
                                    }

                                    Session[CVariablesSesion.GrillaAnexoD3Gb] = objNuevoDatoD3Gb;
                                }

                                Session[CVariablesSesion.GrillaAnexoD3] = objNuevoDatoD3;

                                #endregion

                                //ANEXO E
                                #region ANEXO E
                                var objMovE = lstDocumento.Where(x => x.OBSERVACIONES.StartsWith("ECC")).ToList();

                                var objNuevoDatoE = new List<O_OBTENER_ANEXO_E>();
                                foreach (var reg in objMovE)
                                {
                                    var objTrasaccion = new O_OBTENER_ANEXO_E();
                                    var objVolE = new O_OBTENER_VOLUMEN_IMPORTADO();

                                    var parametros = reg.OBSERVACIONES;

                                    string[] lista = parametros.Split('|');

                                    objTrasaccion.NRO_DUI = lista[1].ToString();
                                    objTrasaccion.GRAVAMEN = Convert.ToDecimal(lista[8].ToString());
                                    objTrasaccion.SIDUINEA = Convert.ToDecimal(lista[9].ToString());
                                    objTrasaccion.COSTO_DEPOSITO = Convert.ToDecimal(lista[10].ToString());
                                    objTrasaccion.TOTAL_COSTO = Convert.ToDecimal(lista[8].ToString()) + Convert.ToDecimal(lista[9].ToString()) + Convert.ToDecimal(lista[10].ToString());

                                    objNuevoDatoE.Add(objTrasaccion);

                                    //if (reg.ID_MOVIMIENTO == decIdMovimiento)
                                    //{
                                    objVolE.IDMOVIMIENTO = reg.ID_MOVIMIENTO;
                                    objVolE.RON = reg.CODIGO;
                                    objVolE.VOLUMEN = 0;
                                    objVolE.COSTO = Convert.ToDecimal(lista[8].ToString()) + Convert.ToDecimal(lista[9].ToString()) + Convert.ToDecimal(lista[10].ToString());

                                    objVolImportE.Add(objVolE);
                                    //}

                                }

                                Session[CVariablesSesion.GrillaAnexoE] = objNuevoDatoE;

                                #endregion

                                //ANEXO F
                                #region anexo F

                                var objNuevoDatoF1 = new List<O_OBTENER_ANEXO_F>();
                                if (decOpcion == 2)
                                {
                                    lstDocumento = lstDocumento.Where(x => x.OBSERVACIONES != null).ToList();

                                    var objMovF = lstDocumento.Where(x => x.OBSERVACIONES.StartsWith("ACCP")).ToList();

                                    foreach (var reg in objMovF)
                                    {
                                        var objTrasaccion = new O_OBTENER_ANEXO_F();
                                        var objVol = new O_OBTENER_VOLUMEN_IMPORTADO();

                                        var parametros = reg.OBSERVACIONES;

                                        string[] lista = parametros.Split('|');

                                        objTrasaccion.NRO_DUI = lista[1].ToString();
                                        objTrasaccion.PROVEEDOR = reg.RAZON_SOCIAL;
                                        objTrasaccion.MES_RECIBIDO = ObtenerPeriodo(reg.FECHA_HORA_EMISION.ToString());
                                        objTrasaccion.NRO_FACTURA_PROVEEDOR = reg.NRO_TRANSACCION;
                                        objTrasaccion.MONTO_FACTURA_PROVEEDOR = reg.PRECIO_TOTAL;
                                        objTrasaccion.COSTO_FINANCIERO = reg.PRECIO_TOTAL * Convert.ToDecimal("0.0010");
                                        objTrasaccion.COSTO_FINANCIERO_NETO = (reg.PRECIO_TOTAL * Convert.ToDecimal("0.0010")) * Convert.ToDecimal("0.078");

                                        objNuevoDatoF1.Add(objTrasaccion);

                                    }

                                    Session[CVariablesSesion.GrillaAnexoF1] = objNuevoDatoF1;

                                    var objMovF2 = lstDocumento.Where(x => x.OBSERVACIONES.StartsWith("D1IO")).ToList();

                                    var objNuevoDatoF2 = new List<O_OBTENER_ANEXO_F>();

                                    foreach (var reg in objMovF2)
                                    {
                                        var objVolD = new O_OBTENER_VOLUMEN_IMPORTADO();

                                        var objTrasaccion = new O_OBTENER_ANEXO_F();

                                        var parametros = reg.OBSERVACIONES;

                                        string[] lista = parametros.Split('|');

                                        string[] listaProveedor = lista[6].Split('-');

                                        string fecha = reg.FECHA_HORA_EMISION.ToString();

                                        objTrasaccion.MES_RECIBIDO = Convert.ToDateTime(fecha.Substring(0, 4) + "/" + fecha.Substring(4, 2) + "/" + fecha.Substring(6, 2)).ToString("MMMM - yyyy");
                                        objTrasaccion.NRO_DUI = lista[1].ToString();
                                        objTrasaccion.PROVEEDOR = reg.RAZON_SOCIAL;
                                        objTrasaccion.NRO_FACTURA_PROVEEDOR = reg.NRO_TRANSACCION;
                                        objTrasaccion.MONTO_FACTURA_PROVEEDOR = reg.PRECIO_TOTAL;
                                        objTrasaccion.COSTO_FINANCIERO = reg.PRECIO_TOTAL * Convert.ToDecimal("0.0010");
                                        objTrasaccion.COSTO_FINANCIERO_NETO = (reg.PRECIO_TOTAL * Convert.ToDecimal("0.0010")) * Convert.ToDecimal("0.078");

                                        objNuevoDatoF2.Add(objTrasaccion);

                                        /*if (reg.ID_MOVIMIENTO == decIdMovimiento)
                                        {
                                            objVolD.RON = reg.CODIGO;
                                            objVolD.VOLUMEN = 1;
                                            objVolD.COSTO = reg.PRECIO_TOTAL;

                                            objVolImportD.Add(objVolD);
                                        }*/


                                    }

                                    Session[CVariablesSesion.GrillaAnexoF2] = objNuevoDatoF2;
                                }

                                #endregion

                                //ANEXO G
                                #region ANEXO G

                                var objMovG1 = lstDocumento.Where(x => x.OBSERVACIONES.StartsWith("G1AI")).ToList();

                                var objVolG = new O_OBTENER_VOLUMEN_IMPORTADO();

                                var objNuevoDatoG1 = new List<O_OBTENER_ANEXO_G>();
                                var objNuevoDatoG1Gb = new List<O_OBTENER_ANEXO_G>();
                                var objNuevoDatoG1Total = new List<O_OBTENER_ANEXO_G>();
                                var objNuevoDatoI1 = new List<O_OBTENER_ANEXO_I>();
                                var objNuevoDatoIGb = new List<O_OBTENER_ANEXO_I_GB>();
                                var objNuevoDatoIGbase = new List<O_OBTENER_ANEXO_I_GBASE>();
                                var objNuevoDatoI1Total = new List<O_OBTENER_ANEXO_I>();

                                foreach (var reg in objMovG1)
                                {
                                    var objTrasaccion = new O_OBTENER_ANEXO_G();
                                    var objTrasaccionI = new O_OBTENER_ANEXO_I();

                                    objVolG = new O_OBTENER_VOLUMEN_IMPORTADO();

                                    var parametros = reg.OBSERVACIONES;

                                    string[] lista = parametros.Split('|');

                                    string fecha = reg.FECHA_HORA_EMISION.ToString();

                                    objTrasaccion.NRO_DUI = lista[1].ToString();
                                    objTrasaccion.MES = Convert.ToDateTime(fecha.Substring(0, 4) + "/" + fecha.Substring(4, 2) + "/" + fecha.Substring(6, 2)).ToString("MMMM - yyyy");
                                    objTrasaccion.EMPRESA_ALMACENAJE = reg.RAZON_SOCIAL;
                                    objTrasaccion.NRO_FACTURA_ALMACENAJE = reg.NRO_TRANSACCION;
                                    objTrasaccion.TARIFA_UNITARIA = reg.PRECIO_UNITARIO;
                                    objTrasaccion.VOLUMEN_RECIBIDO = Convert.ToDecimal(lista[5].ToString());
                                    objTrasaccion.COSTO_ALMACENAJE = Math.Round(objTrasaccion.TARIFA_UNITARIA * objTrasaccion.VOLUMEN_RECIBIDO, 2);
                                    objTrasaccion.MONTO_NETO_IVA = Math.Round(objTrasaccion.COSTO_ALMACENAJE * Convert.ToDecimal("0.87"), 2);

                                    objNuevoDatoG1.Add(objTrasaccion);
                                    objNuevoDatoG1Total.Add(objTrasaccion);

                                    string[] listaProveedor = lista[6].Split('-');

                                    objTrasaccionI.NRO_DUI = lista[1].ToString();
                                    if (listaProveedor.Count() == 2)
                                    {
                                        objTrasaccionI.PROVEEDOR = listaProveedor[1].ToString();
                                        objTrasaccionI.NRO_FACTURA_PROVEEDOR = listaProveedor[0].ToString();
                                    }
                                    else
                                    {
                                        objTrasaccionI.PROVEEDOR = "";
                                        objTrasaccionI.NRO_FACTURA_PROVEEDOR = "";
                                    }
                                    objTrasaccionI.MES_RECEPCION = Convert.ToDateTime(fecha.Substring(0, 4) + "/" + fecha.Substring(4, 2) + "/" + fecha.Substring(6, 2)).ToString("MMMM - yyyy");
                                    objTrasaccionI.PLANTA = txtPlanta;
                                    objTrasaccionI.VOLUMEN = Convert.ToDecimal(lista[5].ToString());

                                    objNuevoDatoI1.Add(objTrasaccionI);

                                    //if (reg.ID_MOVIMIENTO == decIdMovimiento)
                                    //{
                                    objVolG.IDMOVIMIENTO = reg.ID_MOVIMIENTO;
                                    objVolG.RON = reg.CODIGO;
                                    objVolG.VOLUMEN = Convert.ToDecimal(lista[5].ToString());
                                    objVolG.COSTO = Math.Round(objTrasaccion.COSTO_ALMACENAJE * Convert.ToDecimal("0.87"), 2);

                                    objVolImportG.Add(objVolG);
                                    //}
                                }

                                if (decOpcion == 2)
                                {
                                    lstDocumentoGb = lstDocumentoGb.Where(x => x.OBSERVACIONES != null).ToList();

                                    var objMovGnGb = lstDocumentoGb.Where(x => x.OBSERVACIONES.StartsWith("G1AI")).ToList();

                                    foreach (var reg in objMovGnGb)
                                    {
                                        var objTrasaccion = new O_OBTENER_ANEXO_G();
                                        var objTrasaccionI = new O_OBTENER_ANEXO_I_GB();

                                        objVolG = new O_OBTENER_VOLUMEN_IMPORTADO();

                                        var parametros = reg.OBSERVACIONES;

                                        string[] lista = parametros.Split('|');

                                        string fecha = reg.FECHA_HORA_EMISION.ToString();

                                        objTrasaccion.NRO_DUI = "GASOLINA BLANCA";
                                        objTrasaccion.MES = "GASOLINA BLANCA";
                                        objTrasaccion.EMPRESA_ALMACENAJE = "GASOLINA BLANCA";
                                        objTrasaccion.NRO_FACTURA_ALMACENAJE = reg.NRO_TRANSACCION;
                                        objTrasaccion.TARIFA_UNITARIA = reg.PRECIO_UNITARIO;
                                        objTrasaccion.VOLUMEN_RECIBIDO = Convert.ToDecimal(lista[5].ToString());
                                        objTrasaccion.COSTO_ALMACENAJE = Math.Round(objTrasaccion.TARIFA_UNITARIA * objTrasaccion.VOLUMEN_RECIBIDO, 2);
                                        objTrasaccion.MONTO_NETO_IVA = Math.Round(objTrasaccion.COSTO_ALMACENAJE * Convert.ToDecimal("0.87"), 2);

                                        objNuevoDatoG1Gb.Add(objTrasaccion);
                                        objNuevoDatoG1Total.Add(objTrasaccion);

                                        string[] listaProveedor = lista[6].Split('-');

                                        objTrasaccionI.MES_RECIBIDO = Convert.ToDateTime(fecha.Substring(0, 4) + "/" + fecha.Substring(4, 2) + "/" + fecha.Substring(6, 2)).ToString("MMMM - yyyy");
                                        objTrasaccionI.PLANTA = txtPlanta;
                                        objTrasaccionI.VOLUMEN = Convert.ToDecimal(lista[5].ToString());

                                        objNuevoDatoIGb.Add(objTrasaccionI);

                                        /*if (reg.ID_MOVIMIENTO == decIdMovimiento)
                                        {
                                            objVolG.RON = reg.CODIGO;
                                            objVolG.VOLUMEN = Convert.ToDecimal(lista[5].ToString());
                                            objVolG.COSTO = Math.Round(objTrasaccion.COSTO_ALMACENAJE * Convert.ToDecimal("0.87"), 2);

                                            objVolImportG.Add(objVolG);
                                        }*/
                                    }

                                    var objMovGnGbase = lstDocumentoGb.Where(x => x.OBSERVACIONES.StartsWith("CVGB")).ToList();

                                    foreach (var reg in objMovGnGbase)
                                    {
                                        var objTrasaccionIGBase = new O_OBTENER_ANEXO_I_GBASE();

                                        objVolG = new O_OBTENER_VOLUMEN_IMPORTADO();

                                        var parametros = reg.OBSERVACIONES;

                                        string[] lista = parametros.Split('|');

                                        string fecha = reg.FECHA_HORA_EMISION.ToString();

                                        string[] listaProveedor = lista[6].Split('-');

                                        objTrasaccionIGBase.MES_RECIBIDO = Convert.ToDateTime(fecha.Substring(0, 4) + "/" + fecha.Substring(4, 2) + "/" + fecha.Substring(6, 2)).ToString("MMMM - yyyy");
                                        objTrasaccionIGBase.PLANTA = lista[2].ToString();
                                        objTrasaccionIGBase.DATO_1 = Convert.ToDecimal(lista[4].ToString());
                                        objTrasaccionIGBase.DATO_2 = Convert.ToDecimal(lista[5].ToString());
                                        objTrasaccionIGBase.DATO_3 = Convert.ToDecimal(lista[4].ToString()) + Convert.ToDecimal(lista[5].ToString());


                                        objNuevoDatoIGbase.Add(objTrasaccionIGBase);

                                        /*if (reg.ID_MOVIMIENTO == decIdMovimiento)
                                        {
                                            objVolG.RON = reg.CODIGO;
                                            objVolG.VOLUMEN = Convert.ToDecimal(lista[5].ToString());
                                            objVolG.COSTO = Math.Round(objTrasaccion.COSTO_ALMACENAJE * Convert.ToDecimal("0.87"), 2);

                                            objVolImportG.Add(objVolG);
                                        }*/
                                    }


                                    Session[CVariablesSesion.GrillaAnexoG1Gb] = objNuevoDatoG1Gb;
                                }

                                Session[CVariablesSesion.GrillaAnexoG1] = objNuevoDatoG1;

                                Session[CVariablesSesion.GrillaAnexoI1] = objNuevoDatoI1;

                                var objMovG2 = lstDocumento.Where(x => x.OBSERVACIONES.StartsWith("G2ADF")).ToList();

                                var objNuevoDatoG2 = new List<O_OBTENER_ANEXO_G>();
                                var objNuevoDatoG2Gb = new List<O_OBTENER_ANEXO_G>();
                                var objNuevoDatoG2Total = new List<O_OBTENER_ANEXO_G>();

                                var objNuevoDatoI2 = new List<O_OBTENER_ANEXO_I>();

                                foreach (var reg in objMovG2)
                                {
                                    var objTrasaccion = new O_OBTENER_ANEXO_G();
                                    var objTrasaccionI = new O_OBTENER_ANEXO_I();

                                    objVolG = new O_OBTENER_VOLUMEN_IMPORTADO();

                                    var parametros = reg.OBSERVACIONES;

                                    string[] lista = parametros.Split('|');

                                    string fecha = reg.FECHA_HORA_EMISION.ToString();

                                    objTrasaccion.NRO_DUI = lista[1].ToString();
                                    objTrasaccion.MES = Convert.ToDateTime(fecha.Substring(0, 4) + "/" + fecha.Substring(4, 2) + "/" + fecha.Substring(6, 2)).ToString("MMMM - yyyy");
                                    objTrasaccion.EMPRESA_ALMACENAJE = reg.RAZON_SOCIAL;
                                    objTrasaccion.NRO_FACTURA_ALMACENAJE = reg.NRO_TRANSACCION;
                                    objTrasaccion.TARIFA_UNITARIA = reg.PRECIO_UNITARIO;
                                    objTrasaccion.VOLUMEN_RECIBIDO = Convert.ToDecimal(lista[5].ToString());
                                    objTrasaccion.COSTO_ALMACENAJE = Math.Round(objTrasaccion.TARIFA_UNITARIA * objTrasaccion.VOLUMEN_RECIBIDO, 2);
                                    objTrasaccion.MONTO_NETO_IVA = Math.Round(objTrasaccion.COSTO_ALMACENAJE * Convert.ToDecimal("0.87"), 2);

                                    objNuevoDatoG2.Add(objTrasaccion);
                                    objNuevoDatoG2Total.Add(objTrasaccion);

                                    string[] listaProveedor = lista[6].Split('-');

                                    objTrasaccionI.NRO_DUI = lista[1].ToString();
                                    if (listaProveedor.Count() == 2)
                                    {
                                        objTrasaccionI.PROVEEDOR = listaProveedor[1].ToString();
                                        objTrasaccionI.NRO_FACTURA_PROVEEDOR = listaProveedor[0].ToString();
                                    }
                                    else
                                    {
                                        objTrasaccionI.PROVEEDOR = "";
                                        objTrasaccionI.NRO_FACTURA_PROVEEDOR = "";
                                    }
                                    objTrasaccionI.MES_RECEPCION = Convert.ToDateTime(fecha.Substring(0, 4) + "/" + fecha.Substring(4, 2) + "/" + fecha.Substring(6, 2)).ToString("MMMM - yyyy");
                                    objTrasaccionI.PLANTA = "LA PAZ";
                                    objTrasaccionI.VOLUMEN = Convert.ToDecimal(lista[5].ToString());

                                    objNuevoDatoI2.Add(objTrasaccionI);

                                    //if (reg.ID_MOVIMIENTO == decIdMovimiento)
                                    //{
                                    objVolG.IDMOVIMIENTO = reg.ID_MOVIMIENTO;
                                    objVolG.RON = reg.CODIGO;
                                    objVolG.VOLUMEN = Convert.ToDecimal(lista[5].ToString());
                                    objVolG.COSTO = Math.Round(objTrasaccion.COSTO_ALMACENAJE * Convert.ToDecimal("0.87"), 2);

                                    objVolImportG.Add(objVolG);
                                    //}
                                }

                                if (decOpcion == 2)
                                {
                                    lstDocumentoGb = lstDocumentoGb.Where(x => x.OBSERVACIONES != null).ToList();

                                    var objMovGnGb = lstDocumentoGb.Where(x => x.OBSERVACIONES.StartsWith("G2ADF")).ToList();

                                    foreach (var reg in objMovGnGb)
                                    {
                                        var objTrasaccion = new O_OBTENER_ANEXO_G();
                                        var objTrasaccionI = new O_OBTENER_ANEXO_I_GB();

                                        objVolG = new O_OBTENER_VOLUMEN_IMPORTADO();

                                        var parametros = reg.OBSERVACIONES;

                                        string[] lista = parametros.Split('|');

                                        string fecha = reg.FECHA_HORA_EMISION.ToString();

                                        objTrasaccion.NRO_DUI = "GASOLINA BLANCA";
                                        objTrasaccion.MES = "GASOLINA BLANCA";
                                        objTrasaccion.EMPRESA_ALMACENAJE = "GASOLINA BLANCA";
                                        objTrasaccion.NRO_FACTURA_ALMACENAJE = reg.NRO_TRANSACCION;
                                        objTrasaccion.TARIFA_UNITARIA = reg.PRECIO_UNITARIO;
                                        objTrasaccion.VOLUMEN_RECIBIDO = Convert.ToDecimal(lista[5].ToString());
                                        objTrasaccion.COSTO_ALMACENAJE = Math.Round(objTrasaccion.TARIFA_UNITARIA * objTrasaccion.VOLUMEN_RECIBIDO, 2);
                                        objTrasaccion.MONTO_NETO_IVA = Math.Round(objTrasaccion.COSTO_ALMACENAJE * Convert.ToDecimal("0.87"), 2);

                                        objNuevoDatoG2Gb.Add(objTrasaccion);
                                        objNuevoDatoG2Total.Add(objTrasaccion);

                                        string[] listaProveedor = lista[6].Split('-');

                                        objTrasaccionI.MES_RECIBIDO = Convert.ToDateTime(fecha.Substring(0, 4) + "/" + fecha.Substring(4, 2) + "/" + fecha.Substring(6, 2)).ToString("MMMM - yyyy");
                                        objTrasaccionI.PLANTA = txtPlanta;
                                        objTrasaccionI.VOLUMEN = Convert.ToDecimal(lista[5].ToString());

                                        objNuevoDatoIGb.Add(objTrasaccionI);

                                        /*if (reg.ID_MOVIMIENTO == decIdMovimiento)
                                        {
                                            objVolG.RON = reg.CODIGO;
                                            objVolG.VOLUMEN = Convert.ToDecimal(lista[5].ToString());
                                            objVolG.COSTO = Math.Round(objTrasaccion.COSTO_ALMACENAJE * Convert.ToDecimal("0.87"), 2);

                                            objVolImportG.Add(objVolG);
                                        }*/
                                    }

                                    Session[CVariablesSesion.GrillaAnexoG2Gb] = objNuevoDatoG2Gb;

                                }

                                Session[CVariablesSesion.GrillaAnexoG2] = objNuevoDatoG2;

                                Session[CVariablesSesion.GrillaAnexoI2] = objNuevoDatoI2;

                                Session[CVariablesSesion.GrillaAnexoIGb] = objNuevoDatoIGb;

                                Session[CVariablesSesion.GrillaAnexoIGbase] = objNuevoDatoIGbase;

                                #endregion

                                //ANEXO H
                                #region ANEXO H

                                var objMovH = lstDocumento.Where(x => x.OBSERVACIONES.StartsWith("HCM")).ToList();

                                var objNuevoDatoH = new List<O_OBTENER_ANEXO_H>();
                                var objNuevoDatoHGb = new List<O_OBTENER_ANEXO_H>();
                                var objNuevoDatoHTotal = new List<O_OBTENER_ANEXO_H>();
                                foreach (var reg in objMovH)
                                {
                                    var objTrasaccion = new O_OBTENER_ANEXO_H();
                                    var objVolH = new O_OBTENER_VOLUMEN_IMPORTADO();

                                    var parametros = reg.OBSERVACIONES;

                                    string[] lista = parametros.Split('|');

                                    string[] listaProveedor = lista[6].Split('-');

                                    string fecha = reg.FECHA_HORA_EMISION.ToString();

                                    objTrasaccion.MES = Convert.ToDateTime(fecha.Substring(0, 4) + "/" + fecha.Substring(4, 2) + "/" + fecha.Substring(6, 2)).ToString("MMMM - yyyy");
                                    objTrasaccion.NRO_DUI = lista[1].ToString();
                                    if (listaProveedor.Count() == 2)
                                    {
                                        objTrasaccion.NRO_FACTURA_PROVEEDOR = listaProveedor[0].ToString();
                                    }
                                    else
                                    {
                                        objTrasaccion.NRO_FACTURA_PROVEEDOR = "";
                                    }

                                    objTrasaccion.VOLUMEN_RECIBIDO = lista[5].ToString() == "" ? 0 : Convert.ToDecimal(lista[5].ToString());
                                    objTrasaccion.MONTO_MONITOREO = reg.PRECIO_TOTAL;

                                    objNuevoDatoH.Add(objTrasaccion);
                                    objNuevoDatoHTotal.Add(objTrasaccion);

                                    //if (reg.ID_MOVIMIENTO == decIdMovimiento)
                                    //{
                                    objVolH.IDMOVIMIENTO = reg.ID_MOVIMIENTO;
                                    objVolH.RON = reg.CODIGO;
                                    objVolH.VOLUMEN = 0;
                                    objVolH.COSTO = reg.PRECIO_TOTAL;

                                    objVolImportH.Add(objVolH);
                                    //}
                                }

                                if (decOpcion == 2)
                                {
                                    lstDocumentoGb = lstDocumentoGb.Where(x => x.OBSERVACIONES != null).ToList();

                                    var objMovHnGb = lstDocumentoGb.Where(x => x.OBSERVACIONES.StartsWith("HCM")).ToList();

                                    foreach (var reg in objMovHnGb)
                                    {
                                        var objTrasaccion = new O_OBTENER_ANEXO_H();
                                        var objVolH = new O_OBTENER_VOLUMEN_IMPORTADO();

                                        var parametros = reg.OBSERVACIONES;

                                        string[] lista = parametros.Split('|');

                                        string[] listaProveedor = lista[6].Split('-');

                                        string fecha = reg.FECHA_HORA_EMISION.ToString();

                                        objTrasaccion.MES = Convert.ToDateTime(fecha.Substring(0, 4) + "/" + fecha.Substring(4, 2) + "/" + fecha.Substring(6, 2)).ToString("MMMM - yyyy");
                                        objTrasaccion.NRO_DUI = "GASOLINA BLANCA";
                                        objTrasaccion.NRO_FACTURA_PROVEEDOR = "GASOLINA BLANCA";

                                        objTrasaccion.VOLUMEN_RECIBIDO = lista[5].ToString() == "" ? 0 : Convert.ToDecimal(lista[5].ToString());
                                        objTrasaccion.MONTO_MONITOREO = reg.PRECIO_TOTAL;

                                        objNuevoDatoHGb.Add(objTrasaccion);
                                        objNuevoDatoHTotal.Add(objTrasaccion);

                                        /*if (reg.ID_MOVIMIENTO == decIdMovimiento)
                                        {
                                            objVolH.RON = reg.CODIGO;
                                            objVolH.VOLUMEN = 0;
                                            objVolH.COSTO = reg.PRECIO_TOTAL;

                                            objVolImportH.Add(objVolH);
                                        }*/
                                    }

                                    Session[CVariablesSesion.GrillaAnexoHnGb] = objNuevoDatoHGb;
                                }

                                Session[CVariablesSesion.GrillaAnexoH] = objNuevoDatoH;

                                #endregion

                                //ANEXO I
                            }
                            else { }

                            if (decOpcion == 1)
                            {
                                //var registrosBase = (List<O_CONSULTA_MOVIM_TRAN_CTY>)Session[CVariablesSesion.GrillaBase];
                                //FORMULARIO 1
                                #region base
                                var objNuevoDatoF1 = new List<O_OBTENER_ANEXO_F>();
                                var objNuevoDatoF2 = new List<O_OBTENER_ANEXO_F>();
                                var objNuevoDatoVolumen = new List<O_OBTENER_VOLUMEN_IMPORTADO>();

                                var registrosBase = lstResultadoMovBase;

                                foreach (var item in registrosBase)
                                {
                                    listaF1 = item.OBSERVACION_MOVIMIENTO.Split('|');

                                    //lblNombreEmpresa.Text = "a) Nombre de la Empresa: " + tlsOperadores.FocusedNode.GetValue("StrDescripcion").ToString();

                                    //lblMontoCompensar.Text = "a) Monto total solicitado para compensar: " + listaF1[1].ToString();

                                    /*if (tlsOperadores.FocusedNode.GetValue("StrDescripcion").ToString() == "YACIMIENTOS PETROLIFEROS FISCALES BOLIVIANOS - YPFB")
                                    {
                                        chbYacimiento.Checked = true;
                                        chbLogistica.Checked = false;
                                    }
                                    else
                                    {
                                        chbYacimiento.Checked = false;
                                        chbLogistica.Checked = true;
                                    }*/

                                    //formulario 2
                                    //txtDuiRegularizadaF2.Text = listaF1[0].ToString();
                                    Session[CVariablesSesion.txtDuiRegularizadaF2] = listaF1[0].ToString();
                                    //txtFechaDuiF2.Text = registros[0].FECHA_OPERACION_DIA.ToString();
                                    Session[CVariablesSesion.txtFechaDuiF2] = registros[0].FECHA_OPERACION_DIA.ToString();

                                    if (listaF1.Count() == 12)
                                    {
                                        var txtFronteraIngresoF2 = listaF1[9].ToString();
                                        Session[CVariablesSesion.txtFronteraIngresoF2] = listaF1[9].ToString();
                                        var txtIncontermsF2 = listaF1[10].ToString();
                                        Session[CVariablesSesion.txtIncontermsF2] = listaF1[10].ToString();
                                    }

                                    var txtPrecioGasolinaBlancaF2 = listaF1[4].ToString();
                                    Session[CVariablesSesion.txtPrecioGasolinaBlancaF2] = listaF1[4].ToString();
                                    var txtPrecioNetoIvaF2 = listaF1[5].ToString();
                                    Session[CVariablesSesion.txtPrecioNetoIvaF2] = listaF1[5].ToString();
                                    var txtTarifaPoliductoF2 = listaF1[6].ToString();
                                    Session[CVariablesSesion.txtTarifaPoliductoF2] = listaF1[6].ToString();
                                    var txtTarifaNetoIvaF2 = listaF1[7].ToString();
                                    Session[CVariablesSesion.txtTarifaNetoIvaF2] = listaF1[7].ToString();

                                    var datoXgb = Convert.ToDecimal(listaF1[2].ToString()) > 1 ? Convert.ToDecimal(listaF1[2].ToString()) : (Convert.ToDecimal(listaF1[2].ToString()) * 100);
                                    var datoXimp = Convert.ToDecimal(listaF1[3].ToString()) > 1 ? Convert.ToDecimal(listaF1[3].ToString()) : (Convert.ToDecimal(listaF1[3].ToString()) * 100);
                                    var datoXgbC = datoXgb / 100;
                                    var datoXimpC = datoXimp / 100;
                                    var datoPpt = Convert.ToDecimal(listaF1[8].ToString());

                                    var txtCostoGasolinaBlancaNeto = (Convert.ToDecimal(listaF1[5].ToString()) + Convert.ToDecimal(listaF1[7].ToString())).ToString();

                                    #region sumatorias de anexos

                                    var listaAf2 = (from datoA in objVolImportA
                                                    where datoA.IDMOVIMIENTO == item.ID_MOVIMIENTO
                                                    group datoA by datoA.RON into datoAGroup
                                                    select new
                                                    {
                                                        RON = datoAGroup.Key,
                                                        VOLUMEN = datoAGroup.Sum(x => x.VOLUMEN),
                                                        COSTO = datoAGroup.Sum(x => x.COSTO)
                                                    }).ToList();

                                    Session[CVariablesSesion.GrillaResumenAnexoA] = listaAf2;

                                    var listaBf2 = (from datoA in objVolImportB
                                                    where datoA.IDMOVIMIENTO == item.ID_MOVIMIENTO
                                                    group datoA by datoA.RON into datoAGroup
                                                    select new
                                                    {
                                                        RON = datoAGroup.Key,
                                                        VOLUMEN = datoAGroup.Sum(x => x.VOLUMEN),
                                                        COSTO = datoAGroup.Sum(x => x.COSTO)
                                                    }).ToList();

                                    Session[CVariablesSesion.GrillaResumenAnexoB1] = listaBf2;

                                    var listaCf2 = (from datoA in objVolImportC
                                                    where datoA.IDMOVIMIENTO == item.ID_MOVIMIENTO
                                                    group datoA by datoA.RON into datoAGroup
                                                    select new
                                                    {
                                                        RON = datoAGroup.Key,
                                                        VOLUMEN = datoAGroup.Sum(x => x.VOLUMEN),
                                                        COSTO = datoAGroup.Sum(x => x.COSTO)
                                                    }).ToList();

                                    Session[CVariablesSesion.GrillaResumenAnexoC] = listaCf2;

                                    var listaD1F2 = (from datoA in objVolImportD
                                                     where datoA.VOLUMEN == 1 && datoA.IDMOVIMIENTO == item.ID_MOVIMIENTO
                                                     group datoA by datoA.RON into datoAGroup
                                                     select new
                                                     {
                                                         RON = datoAGroup.Key,
                                                         VOLUMEN = datoAGroup.Sum(x => x.VOLUMEN),
                                                         COSTO = datoAGroup.Sum(x => x.COSTO)
                                                     }).ToList();

                                    Session[CVariablesSesion.GrillaResumenAnexoD1] = listaD1F2;

                                    var listaDf2 = (from datoA in objVolImportD
                                                    where datoA.IDMOVIMIENTO == item.ID_MOVIMIENTO
                                                    group datoA by datoA.RON into datoAGroup
                                                    select new
                                                    {
                                                        RON = datoAGroup.Key,
                                                        VOLUMEN = datoAGroup.Sum(x => x.VOLUMEN),
                                                        COSTO = datoAGroup.Sum(x => x.COSTO)
                                                    }).ToList();

                                    Session[CVariablesSesion.GrillaResumenAnexoD2] = listaDf2;

                                    var listaEf2 = (from datoA in objVolImportE
                                                    where datoA.IDMOVIMIENTO == item.ID_MOVIMIENTO
                                                    group datoA by datoA.RON into datoAGroup
                                                    select new
                                                    {
                                                        RON = datoAGroup.Key,
                                                        VOLUMEN = datoAGroup.Sum(x => x.VOLUMEN),
                                                        COSTO = datoAGroup.Sum(x => x.COSTO)
                                                    }).ToList();

                                    Session[CVariablesSesion.GrillaResumenAnexoE] = listaEf2;

                                    var listaGf2 = (from datoA in objVolImportG
                                                    where datoA.IDMOVIMIENTO == item.ID_MOVIMIENTO
                                                    group datoA by datoA.RON into datoAGroup
                                                    select new
                                                    {
                                                        RON = datoAGroup.Key,
                                                        VOLUMEN = datoAGroup.Sum(x => x.VOLUMEN),
                                                        COSTO = datoAGroup.Sum(x => x.COSTO)
                                                    }).ToList();

                                    Session[CVariablesSesion.GrillaResumenAnexoF1] = listaEf2;

                                    var listaHf2 = (from datoA in objVolImportH
                                                    where datoA.IDMOVIMIENTO == item.ID_MOVIMIENTO
                                                    group datoA by datoA.RON into datoAGroup
                                                    select new
                                                    {
                                                        RON = datoAGroup.Key,
                                                        VOLUMEN = datoAGroup.Sum(x => x.VOLUMEN),
                                                        COSTO = datoAGroup.Sum(x => x.COSTO)
                                                    }).ToList();

                                    Session[CVariablesSesion.GrillaResumenAnexoH] = listaHf2;

                                    #endregion

                                    var objTrasaccionF1 = new O_OBTENER_ANEXO_F();
                                    var objTrasaccionF2 = new O_OBTENER_ANEXO_F();

                                    //anexo F
                                    foreach (var datoFProducto in listaAf2)
                                    {
                                        if (item.COD_HIDROCARBURO == datoFProducto.RON)
                                        {
                                            objTrasaccionF1.NRO_DUI = listaF1[0].ToString();
                                            objTrasaccionF1.PROVEEDOR = registros[0].RAZON_SOCIAL;
                                            objTrasaccionF1.NRO_FACTURA_PROVEEDOR = "EN ANEXO A";
                                            objTrasaccionF1.MONTO_FACTURA_PROVEEDOR = listaAf2.Count() == 0 ? 0 : datoFProducto.COSTO;
                                            objTrasaccionF1.COSTO_FINANCIERO = Math.Round(objTrasaccionF1.MONTO_FACTURA_PROVEEDOR * Convert.ToDecimal("0.001"), 2);
                                            objTrasaccionF1.COSTO_FINANCIERO_NETO = Math.Round(objTrasaccionF1.COSTO_FINANCIERO * Convert.ToDecimal("0.87"), 2);

                                            objNuevoDatoF1.Add(objTrasaccionF1);
                                        }
                                        else
                                        {
                                            //todo implementado
                                        }
                                    }

                                    foreach (var datoFServicio in listaD1F2)
                                    {
                                        if (item.COD_HIDROCARBURO == datoFServicio.RON)
                                        {
                                            objTrasaccionF2.NRO_DUI = listaF1[0].ToString();
                                            objTrasaccionF2.PROVEEDOR = registros[0].RAZON_SOCIAL;
                                            objTrasaccionF2.NRO_FACTURA_PROVEEDOR = "EN ANEXO D1";
                                            objTrasaccionF2.MONTO_FACTURA_PROVEEDOR = listaD1F2.Count() == 0 ? 0 : datoFServicio.COSTO;
                                            objTrasaccionF2.COSTO_FINANCIERO = Math.Round(objTrasaccionF2.MONTO_FACTURA_PROVEEDOR * Convert.ToDecimal("0.001"), 2);
                                            objTrasaccionF2.COSTO_FINANCIERO_NETO = Math.Round(objTrasaccionF2.COSTO_FINANCIERO * Convert.ToDecimal("0.87"), 2);

                                            objNuevoDatoF2.Add(objTrasaccionF2);
                                        }
                                        else
                                        {
                                            //todo implementado
                                        }
                                    }

                                    var objCostoAnexo = new O_OBTENER_COSTOS_IMPORTADO();

                                    #region costos sumados por anexo

                                    foreach (var datoAnexoA in listaAf2)
                                    {
                                        if (item.COD_HIDROCARBURO == datoAnexoA.RON)
                                        {
                                            objCostoAnexo.RON = datoAnexoA.RON;
                                            objCostoAnexo.A = Math.Round(datoAnexoA.COSTO, 2);
                                        }
                                        else
                                        {
                                            //todo implementado
                                        }
                                    }

                                    foreach (var datoAnexoB in listaBf2)
                                    {
                                        if (item.COD_HIDROCARBURO == datoAnexoB.RON)
                                        {
                                            objCostoAnexo.B = Math.Round(datoAnexoB.COSTO, 2);
                                        }
                                        else
                                        {
                                            //todo implementado
                                        }
                                    }

                                    foreach (var datoAnexoC in listaCf2)
                                    {
                                        if (item.COD_HIDROCARBURO == datoAnexoC.RON)
                                        {
                                            objCostoAnexo.C = Math.Round(datoAnexoC.COSTO, 2);
                                        }
                                        else
                                        {
                                            //todo implementado
                                        }
                                    }

                                    foreach (var datoAnexoD in listaDf2)
                                    {
                                        if (item.COD_HIDROCARBURO == datoAnexoD.RON)
                                        {
                                            objCostoAnexo.D = Math.Round(datoAnexoD.COSTO, 2);
                                        }
                                        else
                                        {
                                            //todo implementado
                                        }
                                    }

                                    foreach (var datoAnexoE in listaEf2)
                                    {
                                        if (item.COD_HIDROCARBURO == datoAnexoE.RON)
                                        {
                                            objCostoAnexo.E = Math.Round(datoAnexoE.COSTO, 2);
                                        }
                                        else
                                        {
                                            //todo implementado
                                        }
                                    }

                                    objCostoAnexo.F = Math.Round(objTrasaccionF1.COSTO_FINANCIERO_NETO + objTrasaccionF2.COSTO_FINANCIERO_NETO, 2);

                                    foreach (var datoAnexoG in listaGf2)
                                    {
                                        if (item.COD_HIDROCARBURO == datoAnexoG.RON)
                                        {
                                            objCostoAnexo.G = Math.Round(datoAnexoG.COSTO, 2);
                                        }
                                        else
                                        {
                                            //todo implementado
                                        }
                                    }

                                    foreach (var datoAnexoH in listaHf2)
                                    {
                                        if (item.COD_HIDROCARBURO == datoAnexoH.RON)
                                        {
                                            objCostoAnexo.H = Math.Round(datoAnexoH.COSTO, 2);
                                        }
                                        else
                                        {
                                            //todo implementado
                                        }
                                    }

                                    objCostoAnexo.I = Math.Round(objCostoAnexo.A + objCostoAnexo.B + objCostoAnexo.C + objCostoAnexo.D + objCostoAnexo.E + objCostoAnexo.F + objCostoAnexo.G + objCostoAnexo.H, 2);

                                    foreach (var datoAnexoA in listaAf2)
                                    {
                                        if (item.COD_HIDROCARBURO == datoAnexoA.RON)
                                        {
                                            var objCostoSub = new O_OBTENER_COSTOS_SUBVENCION();

                                            objCostoSub.DATO_1 = "EN ANEXO A";
                                            objCostoSub.DATO_2 = datoAnexoA.RON;
                                            objCostoSub.DATO_3 = datoAnexoA.VOLUMEN;
                                            objCostoSub.DATO_4 = Math.Round((objCostoAnexo.I) / datoAnexoA.VOLUMEN, 2);
                                            objCostoSub.DATO_5 = datoXimp > 1 ? datoXimp : (datoXimp * 100);
                                            objCostoSub.DATO_6 = Convert.ToDecimal(txtCostoGasolinaBlancaNeto);
                                            objCostoSub.DATO_7 = datoXgb > 1 ? datoXgb : (datoXgb * 100);
                                            if (datoXimpC > 0)
                                            {
                                                objCostoSub.DATO_8 = Math.Round((datoAnexoA.VOLUMEN) / (datoXimpC), 2);
                                            }
                                            else
                                            {
                                                objCostoSub.DATO_8 = 0;
                                            }

                                            objCostoSub.DATO_9 = Math.Round((((objCostoAnexo.I) / datoAnexoA.VOLUMEN) * datoXimpC) + (datoXgbC * Convert.ToDecimal(txtCostoGasolinaBlancaNeto)), 2);

                                            objCostosSubvencion.Add(objCostoSub);

                                            var objVolumenImportado = new O_OBTENER_VOLUMEN_IMPORTADO();

                                            objVolumenImportado.RON = datoAnexoA.RON;
                                            objVolumenImportado.VOLUMEN = datoAnexoA.VOLUMEN;
                                            objVolumenImportado.COSTO = 0;

                                            objNuevoDatoVolumen.Add(objVolumenImportado);

                                            var objCostoSubFinal = new O_OBTENER_COSTOS_SUBVENCION_FINAL();

                                            objCostoSubFinal.DATO_1 = "YPFB";
                                            objCostoSubFinal.DATO_2 = "EN ANEXO A";
                                            objCostoSubFinal.DATO_3 = objCostoSub.DATO_8;
                                            objCostoSubFinal.DATO_4 = objCostoSub.DATO_9;
                                            objCostoSubFinal.DATO_5 = datoPpt;
                                            objCostoSubFinal.DATO_6 = Math.Round((objCostoSubFinal.DATO_3) * (objCostoSubFinal.DATO_4 - objCostoSubFinal.DATO_5), 2);

                                            objCostosSubvencionFinal.Add(objCostoSubFinal);
                                        }
                                        else
                                        {
                                            //todo implementado
                                        }
                                    }

                                    #endregion

                                    objCostos.Add(objCostoAnexo);

                                }

                                Session[CVariablesSesion.GrillaFormulario1Rubro4] = objNuevoDatoVolumen;

                                Session[CVariablesSesion.GrillaFormulario1Rubro3] = objCostos;

                                Session[CVariablesSesion.GrillaFormulario2Rubro4] = objCostosSubvencion;

                                Session[CVariablesSesion.GrillaFormulario2Rubro5] = objCostosSubvencionFinal;

                                Session[CVariablesSesion.GrillaAnexoF1] = objNuevoDatoF1;

                                Session[CVariablesSesion.GrillaAnexoF2] = objNuevoDatoF2;

                                Session[CVariablesSesion.TipoReporte] = EnumTipoReporte.repDjImportacion;
                                var xreImportacion = new repDjImportadores();

                                xreImportacion.xtcEmpresa.Text = tlsOperadores.FocusedNode.GetValue("StrDescripcion").ToString();
                                //xreImportacion.xtcMontoCompensar.Text = listaF1[1].ToString();

                                xreImportacion.odsCostosImportados.DataSource = Session[CVariablesSesion.GrillaFormulario1Rubro3];
                                xreImportacion.odsVolumenImportado.DataSource = Session[CVariablesSesion.GrillaFormulario1Rubro4];

                                if (tlsOperadores.FocusedNode.GetValue("StrDescripcion").ToString() == "YACIMIENTOS PETROLIFEROS FISCALES BOLIVIANOS - YPFB")
                                {
                                    xreImportacion.xtcYPFB.Text = "X";
                                    xreImportacion.xtcYPFBRefinacion.Text = "";
                                }
                                else
                                {
                                    xreImportacion.xtcYPFB.Text = "";
                                    xreImportacion.xtcYPFBRefinacion.Text = "X";
                                }

                                decimal decPrecioSubencion = 0;
                                foreach (var precio in objCostosSubvencionFinal)
                                {
                                    decPrecioSubencion = decPrecioSubencion + precio.DATO_6;
                                }
                                string strLiteral = "";
                                if (decPrecioSubencion > 0)
                                {
                                    strLiteral = _cUtil.Convertir(decPrecioSubencion.ToString(), true, "BOLIVIANOS");
                                    xreImportacion.xtcMontoCompensar.Text = decPrecioSubencion.ToString();
                                    xreImportacion.xrTotalSubvencion.Text = decPrecioSubencion.ToString();
                                }
                                else
                                {
                                    strLiteral = "CERO BOLIVIANOS";
                                    xreImportacion.xtcMontoCompensar.Text = "0";
                                    xreImportacion.xrTotalSubvencion.Text = "0";
                                }

                                xreImportacion.xtcMontoCompensarLiteral.Text = strLiteral;

                                xreImportacion.odsCostoInsumoAditivo.DataSource = Session[CVariablesSesion.GrillaFormulario1Rubro3];
                                xreImportacion.odsCostoSubvencion.DataSource = Session[CVariablesSesion.GrillaFormulario2Rubro4];
                                xreImportacion.odsCostoSubvencionFinal.DataSource = Session[CVariablesSesion.GrillaFormulario2Rubro5];

                                xreImportacion.xtcDui.Text = Session[CVariablesSesion.txtDuiRegularizadaF2].ToString().ToUpper();
                                xreImportacion.xtcFechaDui.Text = Session[CVariablesSesion.txtFechaDuiF2].ToString().ToUpper();
                                xreImportacion.xtcFronteraIngreso.Text = Session[CVariablesSesion.txtFronteraIngresoF2].ToString().ToUpper();
                                xreImportacion.xtcInconterms.Text = Session[CVariablesSesion.txtIncontermsF2].ToString().ToUpper();

                                xreImportacion.xtcProveedor.Text = strProveedor.ToUpper();
                                xreImportacion.xtcPlantaOrigen.Text = strPlantaDespacho.ToUpper();
                                xreImportacion.xtcPlantaDestino.Text = strPlantaRecepcion.ToUpper();

                                xreImportacion.xtcPrecioGasolinaBlanca.Text = Session[CVariablesSesion.txtPrecioGasolinaBlancaF2].ToString();
                                xreImportacion.xtcPrecioNetoIva.Text = Session[CVariablesSesion.txtPrecioNetoIvaF2].ToString();
                                xreImportacion.xtcTarifaTransportePoliducto.Text = Session[CVariablesSesion.txtTarifaPoliductoF2].ToString();
                                xreImportacion.xtcTarifaNetoIva.Text = Session[CVariablesSesion.txtTarifaNetoIvaF2].ToString();

                                xreImportacion.xtcCostoGasolinaBlanca.Text = Convert.ToString(Convert.ToDecimal(Session[CVariablesSesion.txtPrecioNetoIvaF2]) +
                                                                              Convert.ToDecimal(Session[CVariablesSesion.txtTarifaNetoIvaF2]));

                                Session[CVariablesSesion.ReportePrincipal] = xreImportacion;

                                #endregion
                            }
                            else
                            {
                                #region fosil
                                //FORMULARIO 1

                                /*var registrosBase = lstResultadoMovBase;

                                var datos = registrosBase[0].OBSERVACION_MOVIMIENTO.ToString();

                                listaF1 = datos.Split('|');*/

                                /*lblNombreEmpresa.Text = "a) Nombre de la Empresa: " + tlsOperadores.FocusedNode.GetValue("StrDescripcion").ToString();

                                if (tlsOperadores.FocusedNode.GetValue("StrDescripcion").ToString() == "YACIMIENTOS PETROLIFEROS FISCALES BOLIVIANOS - YPFB")
                                {
                                    chbYacimiento.Checked = true;
                                    chbLogistica.Checked = false;
                                }
                                else
                                {
                                    chbYacimiento.Checked = false;
                                    chbLogistica.Checked = true;
                                }*/

                                //FORMULARIO 2
                                decimal cantVolumenAnIyA = 0;
                                decimal cantVolumenBnIyA = 0;
                                decimal cantVolumenCnIyA = 0;
                                decimal cantVolumenDnIyA = 0;
                                decimal cantVolumenEnIyA = 0;
                                decimal cantVolumenFnIyA = 0;
                                decimal cantVolumenGnIyA = 0;
                                decimal cantVolumenHnIyA = 0;
                                decimal cantVolumenInIyA = 0;

                                decimal cantVolumenAnGb = 0;
                                decimal cantVolumenBnGb = 0;
                                decimal cantVolumenCnGb = 0;
                                decimal cantVolumenDnGb = 0;
                                decimal cantVolumenEnGb = 0;
                                decimal cantVolumenFnGb = 0;
                                decimal cantVolumenGnGb = 0;
                                decimal cantVolumenHnGb = 0;
                                decimal cantVolumenInGb = 0;

                                var listaAnexoA = (List<O_OBTENER_ANEXO_A>)Session[CVariablesSesion.GrillaAnexoA];
                                var listaAnexoAGb = (List<O_OBTENER_ANEXO_A_GB>)Session[CVariablesSesion.GrillaAnexoAnGb];

                                foreach (var dato in listaAnexoA)
                                {
                                    cantVolumenAnIyA = cantVolumenAnIyA + dato.VALOR_FACTURADO;
                                }

                                foreach (var dato in listaAnexoAGb)
                                {
                                    cantVolumenAnGb = cantVolumenAnGb + Convert.ToDecimal(dato.MONTO_NETO);
                                }

                                var listaAnexoB1 = (List<O_OBTENER_ANEXO_B1>)Session[CVariablesSesion.GrillaAnexoB1];
                                var listaAnexoB2 = (List<O_OBTENER_ANEXO_B2>)Session[CVariablesSesion.GrillaAnexoB2];
                                var listaAnexoB3 = (List<O_OBTENER_ANEXO_B3>)Session[CVariablesSesion.GrillaAnexoB3];

                                var listaAnexoB1Gb = (List<O_OBTENER_ANEXO_B1>)Session[CVariablesSesion.GrillaAnexoB1Gb];
                                var listaAnexoB2Gb = (List<O_OBTENER_ANEXO_B2>)Session[CVariablesSesion.GrillaAnexoB2Gbc];
                                var listaAnexoB2GbDu = (List<O_OBTENER_ANEXO_B2_DU>)Session[CVariablesSesion.GrillaAnexoB2GbD];

                                foreach (var dato in listaAnexoB1)
                                {
                                    cantVolumenBnIyA = cantVolumenBnIyA + dato.COSTO_TRANSPORTE;
                                }
                                foreach (var dato in listaAnexoB2)
                                {
                                    cantVolumenBnIyA = cantVolumenBnIyA + dato.COSTO_TRANSPORTE;
                                }
                                foreach (var dato in listaAnexoB3)
                                {
                                    cantVolumenBnIyA = cantVolumenBnIyA + dato.MONTO_NETO_IVA;
                                }

                                foreach (var dato in listaAnexoB1Gb)
                                {
                                    cantVolumenBnGb = cantVolumenBnGb + dato.COSTO_TRANSPORTE;
                                }
                                foreach (var dato in listaAnexoB2Gb)
                                {
                                    cantVolumenBnGb = cantVolumenBnGb + dato.COSTO_TRANSPORTE;
                                }
                                foreach (var dato in listaAnexoB2GbDu)
                                {
                                    cantVolumenBnGb = cantVolumenBnGb + Convert.ToDecimal(dato.MONTO);
                                }

                                var listaAnexoC = (List<O_OBTENER_ANEXO_C>)Session[CVariablesSesion.GrillaAnexoC];

                                foreach (var dato in listaAnexoC)
                                {
                                    cantVolumenCnIyA = cantVolumenCnIyA + dato.MONTO_NETO_IVA;
                                }

                                var listaAnexoD1 = (List<O_OBTENER_ANEXO_D>)Session[CVariablesSesion.GrillaAnexoD1];
                                var listaAnexoD2 = (List<O_OBTENER_ANEXO_D>)Session[CVariablesSesion.GrillaAnexoD2];
                                var listaAnexoD3 = (List<O_OBTENER_ANEXO_D>)Session[CVariablesSesion.GrillaAnexoD3];

                                var listaAnexoD1Gb = (List<O_OBTENER_ANEXO_D>)Session[CVariablesSesion.GrillaAnexoD1Gb];
                                var listaAnexoD2Gb = (List<O_OBTENER_ANEXO_D>)Session[CVariablesSesion.GrillaAnexoD2Gb];
                                var listaAnexoD3GB = (List<O_OBTENER_ANEXO_D>)Session[CVariablesSesion.GrillaAnexoD3Gb];

                                foreach (var dato in listaAnexoD1)
                                {
                                    cantVolumenDnIyA = cantVolumenDnIyA + dato.MONTO_INSPECCION;
                                }
                                foreach (var dato in listaAnexoD2)
                                {
                                    cantVolumenDnIyA = cantVolumenDnIyA + dato.MONTO_INSPECCION;
                                }
                                foreach (var dato in listaAnexoD3)
                                {
                                    cantVolumenDnIyA = cantVolumenDnIyA + dato.MONTO_INSPECCION;
                                }

                                foreach (var dato in listaAnexoD1Gb)
                                {
                                    cantVolumenDnGb = cantVolumenDnGb + dato.MONTO_INSPECCION;
                                }
                                foreach (var dato in listaAnexoD2Gb)
                                {
                                    cantVolumenDnGb = cantVolumenDnGb + dato.MONTO_INSPECCION;
                                }
                                foreach (var dato in listaAnexoD3GB)
                                {
                                    cantVolumenDnGb = cantVolumenDnGb + dato.MONTO_INSPECCION;
                                }

                                var listaAnexoE = (List<O_OBTENER_ANEXO_E>)Session[CVariablesSesion.GrillaAnexoE];

                                foreach (var dato in listaAnexoE)
                                {
                                    cantVolumenEnIyA = cantVolumenEnIyA + dato.TOTAL_COSTO;
                                }

                                var listaAnexoF1 = (List<O_OBTENER_ANEXO_F>)Session[CVariablesSesion.GrillaAnexoF1];
                                var listaAnexoF2 = (List<O_OBTENER_ANEXO_F>)Session[CVariablesSesion.GrillaAnexoF2];

                                foreach (var dato in listaAnexoF1)
                                {
                                    cantVolumenFnIyA = cantVolumenFnIyA + dato.COSTO_FINANCIERO_NETO;
                                }
                                foreach (var dato in listaAnexoF2)
                                {
                                    cantVolumenFnIyA = cantVolumenFnIyA + dato.COSTO_FINANCIERO_NETO;
                                }

                                var listaAnexoG1 = (List<O_OBTENER_ANEXO_G>)Session[CVariablesSesion.GrillaAnexoG1];
                                var listaAnexoG2 = (List<O_OBTENER_ANEXO_G>)Session[CVariablesSesion.GrillaAnexoG2];

                                var listaAnexoG1Gb = (List<O_OBTENER_ANEXO_G>)Session[CVariablesSesion.GrillaAnexoG1Gb];
                                var listaAnexoG2Gb = (List<O_OBTENER_ANEXO_G>)Session[CVariablesSesion.GrillaAnexoG2Gb];

                                foreach (var dato in listaAnexoG1)
                                {
                                    cantVolumenGnIyA = cantVolumenGnIyA + dato.MONTO_NETO_IVA;
                                }
                                foreach (var dato in listaAnexoG2)
                                {
                                    cantVolumenGnIyA = cantVolumenGnIyA + dato.MONTO_NETO_IVA;
                                }

                                foreach (var dato in listaAnexoG1Gb)
                                {
                                    cantVolumenGnGb = cantVolumenGnGb + dato.MONTO_NETO_IVA;
                                }
                                foreach (var dato in listaAnexoG2Gb)
                                {
                                    cantVolumenGnGb = cantVolumenGnGb + dato.MONTO_NETO_IVA;
                                }

                                var listaAnexoH = (List<O_OBTENER_ANEXO_H>)Session[CVariablesSesion.GrillaAnexoH];

                                var listaAnexoHGb = (List<O_OBTENER_ANEXO_H>)Session[CVariablesSesion.GrillaAnexoHnGb];

                                foreach (var dato in listaAnexoH)
                                {
                                    cantVolumenHnIyA = cantVolumenHnIyA + dato.MONTO_MONITOREO;
                                }

                                foreach (var dato in listaAnexoHGb)
                                {
                                    cantVolumenHnGb = cantVolumenHnGb + dato.MONTO_MONITOREO;
                                }

                                var listaAnexoI1 = (List<O_OBTENER_ANEXO_I>)Session[CVariablesSesion.GrillaAnexoI1];
                                var listaAnexoI2 = (List<O_OBTENER_ANEXO_I>)Session[CVariablesSesion.GrillaAnexoI2];
                                var listaAnexoIGb = (List<O_OBTENER_ANEXO_I_GB>)Session[CVariablesSesion.GrillaAnexoIGb];

                                foreach (var dato in listaAnexoI1)
                                {
                                    cantVolumenInIyA = cantVolumenInIyA + dato.VOLUMEN;
                                }
                                foreach (var dato in listaAnexoI2)
                                {
                                    cantVolumenInIyA = cantVolumenInIyA + dato.VOLUMEN;
                                }

                                foreach (var dato in listaAnexoIGb)
                                {
                                    cantVolumenInGb = cantVolumenHnGb + dato.VOLUMEN;
                                }

                                var txtSector = sector(tlsOperadores.FocusedNode.GetValue("StrDepartamento").ToString());

                                var objCostoSu1 = new O_OBTENER_COSTOS_UNITARIO();
                                var objCostoSu2 = new O_OBTENER_COSTOS_UNITARIO();

                                var objCostoAnexo = new O_OBTENER_COSTOS_IMPORTADO();

                                objCostoAnexo.RON = "INSUMOS Y ADITIVOS";
                                objCostoAnexo.A = Math.Round(cantVolumenAnIyA, 2);
                                objCostoAnexo.B = Math.Round(cantVolumenBnIyA, 2);
                                objCostoAnexo.C = Math.Round(cantVolumenCnIyA, 2);
                                objCostoAnexo.D = Math.Round(cantVolumenDnIyA, 2);
                                objCostoAnexo.E = Math.Round(cantVolumenEnIyA, 2);
                                objCostoAnexo.F = Math.Round(cantVolumenFnIyA, 2);
                                objCostoAnexo.G = Math.Round(cantVolumenGnIyA, 2);
                                objCostoAnexo.H = Math.Round(cantVolumenHnIyA, 2);
                                objCostoAnexo.I = Math.Round(objCostoAnexo.A + objCostoAnexo.B + objCostoAnexo.C + objCostoAnexo.D + objCostoAnexo.E + objCostoAnexo.F + objCostoAnexo.G + objCostoAnexo.H, 2);

                                var objCostoUnitario = new O_OBTENER_COSTOS_UNITARIO();

                                objCostoUnitario.DATO_1 = "INSUMOS Y ADITIVOS";
                                objCostoUnitario.DATO_2 = cantVolumenInIyA;
                                objCostoUnitario.DATO_3 = objCostoAnexo.I;
                                if (cantVolumenInIyA == 0)
                                {
                                    objCostoUnitario.DATO_4 = 0;
                                }
                                else
                                {
                                    objCostoUnitario.DATO_4 = Math.Round(objCostoAnexo.I / cantVolumenInIyA, 2);
                                }

                                var MesRecibido = "";

                                var listaAnexoIGBase = (List<O_OBTENER_ANEXO_I_GBASE>)Session[CVariablesSesion.GrillaAnexoIGbase];
                                foreach (var dato in listaAnexoIGBase)
                                {
                                    objCostoSu1.DATO_1 = "Volumen (lt)";
                                    objCostoSu1.DATO_2 = dato.DATO_1;
                                    objCostoSu1.DATO_3 = dato.DATO_2;
                                    objCostoSu1.DATO_4 = dato.DATO_3;

                                    MesRecibido = dato.MES_RECIBIDO;

                                }

                                objCostos.Add(objCostoAnexo);

                                objCostosUni.Add(objCostoUnitario);

                                var objCostoSu1C = new O_OBTENER_COSTOS_UNITARIO();
                                var objCostoSu2C = new O_OBTENER_COSTOS_UNITARIO();

                                var objCostoAnexoGb = new O_OBTENER_COSTOS_IMPORTADO();

                                objCostoAnexoGb.RON = "GASOLINA BLANCA";
                                objCostoAnexoGb.A = Math.Round(cantVolumenAnGb, 2);
                                objCostoAnexoGb.B = Math.Round(cantVolumenBnGb, 2);
                                objCostoAnexoGb.C = Math.Round(cantVolumenCnGb, 2);
                                objCostoAnexoGb.D = Math.Round(cantVolumenDnGb, 2);
                                objCostoAnexoGb.E = Math.Round(cantVolumenEnGb, 2);
                                objCostoAnexoGb.F = Math.Round(cantVolumenFnGb, 2);
                                objCostoAnexoGb.G = Math.Round(cantVolumenGnGb, 2);
                                objCostoAnexoGb.H = Math.Round(cantVolumenHnGb, 2);
                                objCostoAnexoGb.I = Math.Round(objCostoAnexoGb.A + objCostoAnexoGb.B + objCostoAnexoGb.C + objCostoAnexoGb.D + objCostoAnexoGb.E + objCostoAnexoGb.F + objCostoAnexoGb.G + objCostoAnexoGb.H, 2);

                                var objCostoUnitarioGb = new O_OBTENER_COSTOS_UNITARIO();

                                objCostoUnitarioGb.DATO_1 = "GASOLINA BLANCA";
                                objCostoUnitarioGb.DATO_2 = cantVolumenInGb;
                                objCostoUnitarioGb.DATO_3 = objCostoAnexoGb.I;
                                if (cantVolumenInGb == 0)
                                {
                                    objCostoUnitarioGb.DATO_4 = 0;
                                }
                                else
                                {
                                    objCostoUnitarioGb.DATO_4 = Math.Round(objCostoAnexoGb.I / cantVolumenInGb, 2);
                                }


                                objCostoSu1C.DATO_1 = "Costo (Bs/Lt)";
                                objCostoSu1C.DATO_2 = objCostoUnitario.DATO_4;
                                objCostoSu1C.DATO_3 = objCostoUnitarioGb.DATO_4;
                                if (objCostoSu1.DATO_4 == 0)
                                {
                                    objCostoSu1C.DATO_4 = 0;
                                }
                                else
                                {
                                    objCostoSu1C.DATO_4 = Math.Round(((objCostoSu1.DATO_2 / objCostoSu1.DATO_4) * objCostoSu1C.DATO_2) + ((objCostoSu1.DATO_3 / objCostoSu1.DATO_4) * objCostoSu1C.DATO_3), 2);
                                }


                                objCostoSu2.DATO_1 = "Volumen (lt)";
                                objCostoSu2.DATO_2 = objCostoUnitario.DATO_2;
                                objCostoSu2.DATO_3 = objCostoUnitarioGb.DATO_2;
                                objCostoSu2.DATO_4 = objCostoSu2.DATO_2 + objCostoSu2.DATO_3;

                                objCostoSu2C.DATO_1 = "Costo (Bs/Lt)";
                                objCostoSu2C.DATO_2 = objCostoUnitario.DATO_4;
                                objCostoSu2C.DATO_3 = objCostoUnitarioGb.DATO_4;
                                if (objCostoSu2.DATO_4 == 0)
                                {
                                    objCostoSu2C.DATO_4 = 0;
                                }
                                else
                                {
                                    objCostoSu2C.DATO_4 = Math.Round(((objCostoSu2.DATO_2 / objCostoSu2.DATO_4) * objCostoSu2C.DATO_2) + ((objCostoSu2.DATO_3 / objCostoSu2.DATO_4) * objCostoSu2C.DATO_3), 2);
                                }


                                var objCosTotalSuben = new O_OBTENER_COSTOS_TOT_SUB();
                                objCosTotalSuben.DATO_1 = objCostoSu1.DATO_4;
                                objCosTotalSuben.DATO_2 = objCostoSu1C.DATO_4;
                                objCosTotalSuben.DATO_3 = Convert.ToDecimal("3.08");
                                objCosTotalSuben.DATO_4 = Math.Round((objCosTotalSuben.DATO_1 * (objCosTotalSuben.DATO_2 - objCosTotalSuben.DATO_3)), 2);

                                var decMontoCompensar = Math.Round((objCosTotalSuben.DATO_1 * (objCosTotalSuben.DATO_2 - objCosTotalSuben.DATO_3)), 2);
                                //lblMontoCompensar.Text = "a) Monto total solicitado para compensar: " + objCosTotalSuben.DATO_4.ToString();

                                objCostosTotSub.Add(objCosTotalSuben);

                                Session[CVariablesSesion.GrillaCostoTotalFosil] = objCostosTotSub;

                                objCostos.Add(objCostoAnexoGb);

                                Session[CVariablesSesion.GrillaFormulario1Rubro3] = objCostos;

                                objCostosUni.Add(objCostoUnitarioGb);

                                Session[CVariablesSesion.GrillaCostoUnitarioSub] = objCostosUni;

                                objCostosUniSub1.Add(objCostoSu1);
                                objCostosUniSub1.Add(objCostoSu1C);

                                Session[CVariablesSesion.GrillaCostoUniSubvencion1] = objCostosUniSub1;

                                objCostosUniSub2.Add(objCostoSu2);
                                objCostosUniSub2.Add(objCostoSu2C);

                                Session[CVariablesSesion.GrillaCostoUniSubvencion2] = objCostosUniSub2;

                                Session[CVariablesSesion.TipoReporte] = EnumTipoReporte.repDjImportacionFosil;
                                var xreImportacionFosil = new repDjImportadoresFosil();

                                xreImportacionFosil.xtcEmpresa.Text = tlsOperadores.FocusedNode.GetValue("StrDescripcion").ToString();

                                if (tlsOperadores.FocusedNode.GetValue("StrDescripcion").ToString() == "YACIMIENTOS PETROLIFEROS FISCALES BOLIVIANOS - YPFB")
                                {
                                    xreImportacionFosil.xtcYPFB.Text = "X";
                                    xreImportacionFosil.xtcYPFBRefinacion.Text = "";
                                }
                                else
                                {
                                    xreImportacionFosil.xtcYPFB.Text = "";
                                    xreImportacionFosil.xtcYPFBRefinacion.Text = "X";
                                }

                                xreImportacionFosil.xtcMontoCompensar.Text = decMontoCompensar.ToString();

                                string strLiteral = _cUtil.Convertir(decMontoCompensar.ToString(), true, "BOLIVIANOS");
                                xreImportacionFosil.xtcMontoCompensarLiteral.Text = strLiteral;

                                xreImportacionFosil.objectDataSource1.DataSource = Session[CVariablesSesion.GrillaCostoTotalFosil];

                                xreImportacionFosil.xtcDui.Text = MesRecibido;
                                xreImportacionFosil.xtcFechaDui.Text = txtSector;

                                xreImportacionFosil.objectDataSource2.DataSource = Session[CVariablesSesion.GrillaFormulario1Rubro3];

                                xreImportacionFosil.objectDataSource3.DataSource = Session[CVariablesSesion.GrillaCostoUnitarioSub];
                                xreImportacionFosil.objectDataSource4.DataSource = Session[CVariablesSesion.GrillaCostoUniSubvencion1];
                                xreImportacionFosil.objectDataSource5.DataSource = Session[CVariablesSesion.GrillaCostoUniSubvencion2];

                                Session[CVariablesSesion.ReportePrincipal] = xreImportacionFosil;

                                #endregion

                            }

                        }
                    }
                    else { }
                }
                else
                { }

            }
            else
            {
                var lstTipoOperacion = Session[CVariablesSesion.ComboTiposOperacion] as List<O_TIPOS_OPE_ACT_CTY>;
                if (lstTipoOperacion != null && lstTipoOperacion.Any())
                {
                    if (lstTipoOperacion.Where(x => x.FACTOR_CALCULO == 1).Count() == 0)
                    {
                        Session[CVariablesSesion.TipoReporte] = EnumTipoReporte.DeclaracionJuradaEgreso;
                        var xreAcumulado = new repDjAcumuladoEgreso();
                        xreAcumulado.xtcCabCodigo.Text = vecCabecera[0] + " ANEXO 1";
                        xreAcumulado.xtcCabVersion.Text = vecCabecera[1];
                        xreAcumulado.xtcCabAprobado.Text = vecCabecera[2];

                        xreAcumulado.xtcRazonSocial.Text = tlsOperadores.FocusedNode.GetValue("StrDescripcion").ToString();
                        xreAcumulado.xtcActividad.Text = tlsOperadores.FocusedNode.GetValue("StrActividad").ToString();
                        xreAcumulado.xtcDireccion.Text = tlsOperadores.FocusedNode.GetValue("StrDireccion").ToString();
                        xreAcumulado.xtcDepartamento.Text = tlsOperadores.FocusedNode.GetValue("StrDepartamento").ToString();
                        xreAcumulado.xtcUrbano.Text = tlsOperadores.FocusedNode.GetValue("StrAmbito").ToString();
                        xreAcumulado.xtcTelefono.Text = tlsOperadores.FocusedNode.GetValue("StrTelefono").ToString();
                        xreAcumulado.xtcMunicipio.Text = tlsOperadores.FocusedNode.GetValue("StrMunicipio") + "/" + tlsOperadores.FocusedNode.GetValue("StrLocalidad");
                        xreAcumulado.xtcPublico.Text = (tlsOperadores.FocusedNode.GetValue("StrTipoSociedad") == null) ? "" : tlsOperadores.FocusedNode.GetValue("StrTipoSociedad").ToString();
                        xreAcumulado.xtcMes.Text = formatoFecha.GetMonthName(Convert.ToInt32(cmbPeriodo.Value.ToString().Substring(4, 2))).ToUpper() + " - " + cmbPeriodo.Value.ToString().Substring(0, 4);
                        xreAcumulado.xtcLugar.Text = "LUGAR :   " + cmbDepartamento.Text;
                        xreAcumulado.xrlDia.Text = datFechaReg.Day.ToString(CultureInfo.InvariantCulture);
                        xreAcumulado.xrlMes.Text = datFechaReg.Month.ToString(CultureInfo.InvariantCulture);
                        xreAcumulado.xrlGestion.Text = datFechaReg.Year.ToString(CultureInfo.InvariantCulture);
                        xreAcumulado.xrlResponsable.Text = strRep[0].Trim();
                        xreAcumulado.xtcCiUsuarioEmisor.Text = "C.I.: " + strRep[1].Trim();
                        xreAcumulado.xrlUsuarioEmisor.Text = txtPadUsuEmisor.Text.Trim();
                        xreAcumulado.xtcCiUsuarioEmisor.Text = "C.I.: " + txtPadUsuEmisorCi.Text.Trim();
                        xreAcumulado.xtcNoCite.Text = strCite;
                        xreAcumulado.xbcCodigoQr.Text = strCite + "|" + cmbPeriodo.Value + "|" + datFechaReg.Year + datFechaReg.Month.ToString().PadLeft(2, '0') + datFechaReg.Day.ToString().PadLeft(2, '0');

                        var oListaOctanoParCty = objParametricas.Where(x => x.TIPO_VALOR == 7).ToList().FirstOrDefault();
                        if (oListaOctanoParCty != null && oListaOctanoParCty.VALOR_FIN == "2")
                        {
                            xreAcumulado.grhInfraestructura.Visible = true;
                            xreAcumulado.grfInfraestructura.Visible = true;
                        }
                        else
                        {
                            xreAcumulado.grhInfraestructura.Visible = false;
                            xreAcumulado.grfInfraestructura.Visible = false;
                        }
                        xreAcumulado.DataSource = Session[CVariablesSesion.GrillaAcumRep];
                        Session[CVariablesSesion.ReportePrincipal] = xreAcumulado;
                    }
                    else
                    {
                        Session[CVariablesSesion.TipoReporte] = EnumTipoReporte.DeclaracionJurada;
                        var xreAcumulado = new repDjAcumuladoInfra();
                        xreAcumulado.xtcCabCodigo.Text = vecCabecera[0] + " ANEXO 1";
                        xreAcumulado.xtcCabVersion.Text = vecCabecera[1];
                        xreAcumulado.xtcCabAprobado.Text = vecCabecera[2];

                        xreAcumulado.xtcRazonSocial.Text = tlsOperadores.FocusedNode.GetValue("StrDescripcion").ToString();
                        xreAcumulado.xtcActividad.Text = tlsOperadores.FocusedNode.GetValue("StrActividad").ToString();
                        xreAcumulado.xtcDireccion.Text = tlsOperadores.FocusedNode.GetValue("StrDireccion").ToString();
                        xreAcumulado.xtcDepartamento.Text = tlsOperadores.FocusedNode.GetValue("StrDepartamento").ToString();
                        xreAcumulado.xtcUrbano.Text = tlsOperadores.FocusedNode.GetValue("StrAmbito").ToString();
                        xreAcumulado.xtcTelefono.Text = tlsOperadores.FocusedNode.GetValue("StrTelefono").ToString();
                        xreAcumulado.xtcMunicipio.Text = tlsOperadores.FocusedNode.GetValue("StrMunicipio") + "/" + tlsOperadores.FocusedNode.GetValue("StrLocalidad");
                        xreAcumulado.xtcPublico.Text = (tlsOperadores.FocusedNode.GetValue("StrTipoSociedad") == null) ? "" : tlsOperadores.FocusedNode.GetValue("StrTipoSociedad").ToString();
                        xreAcumulado.xtcMes.Text = formatoFecha.GetMonthName(Convert.ToInt32(cmbPeriodo.Value.ToString().Substring(4, 2))).ToUpper() + " - " + cmbPeriodo.Value.ToString().Substring(0, 4);
                        xreAcumulado.xtcLugar.Text = "LUGAR :   " + cmbDepartamento.Text;
                        xreAcumulado.xrlDia.Text = datFechaReg.Day.ToString(CultureInfo.InvariantCulture);
                        xreAcumulado.xrlMes.Text = datFechaReg.Month.ToString(CultureInfo.InvariantCulture);
                        xreAcumulado.xrlGestion.Text = datFechaReg.Year.ToString(CultureInfo.InvariantCulture);
                        xreAcumulado.xrlResponsable.Text = strRep[0].Trim();
                        xreAcumulado.xtcCiResponsable.Text = "C.I.: " + strRep[1].Trim();
                        xreAcumulado.xrlUsuarioEmisor.Text = txtPadUsuEmisor.Text.Trim();
                        xreAcumulado.xtcCiUsuarioEmisor.Text = "C.I.: " + txtPadUsuEmisorCi.Text.Trim();
                        xreAcumulado.xtcNoCite.Text = strCite;
                        xreAcumulado.xbcCodigoQr.Text = strCite + "|" + cmbPeriodo.Value + "|" + datFechaReg.Year + datFechaReg.Month.ToString().PadLeft(2, '0') + datFechaReg.Day.ToString().PadLeft(2, '0');

                        var paramAgrup = objParametricas.Where(x => x.TIPO_VALOR == 10).ToList().FirstOrDefault();
                        var oListaOctanoParCty = objParametricas.Where(x => x.TIPO_VALOR == 7).ToList().FirstOrDefault();

                        xreAcumulado.grhInfraestructura.Visible = false;
                        xreAcumulado.grfInfraestructura.Visible = false;

                        if (oListaOctanoParCty != null && oListaOctanoParCty.VALOR_FIN == "2")
                        {
                            if (paramAgrup != null && paramAgrup.VALOR_FIN != "1")
                            {
                                xreAcumulado.grhInfraestructura.Visible = true;
                                xreAcumulado.grfInfraestructura.Visible = true;
                            }
                        }
                        xreAcumulado.DataSource = Session[CVariablesSesion.GrillaAcumRep];
                        Session[CVariablesSesion.ReportePrincipal] = xreAcumulado;
                    }
                }
                else
                {
                    //implementar
                }

                /*if (Convert.ToInt32(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()) == (int)EnumActividades.EstacionServicioGnv || Convert.ToInt32(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()) == (int)EnumActividades.DistribucionGasRedes)
                {    }
                else
                {    }*/

                //vtas internacional
                Session.Remove(CVariablesSesion.ReporteSecundario);
                if (Session[CVariablesSesion.GrillaInternacional] != null)
                {
                    var xreInt = new repDjInternacional();
                    xreInt.xtcCabCodigo.Text = vecCabecera[0] + " ANEXO 2";
                    xreInt.xtcCabVersion.Text = vecCabecera[1];
                    xreInt.xtcCabAprobado.Text = vecCabecera[2];

                    xreInt.xtcRazonSocial.Text = tlsOperadores.FocusedNode.GetValue("StrDescripcion").ToString();
                    xreInt.xtcActividad.Text = tlsOperadores.FocusedNode.GetValue("StrActividad").ToString();
                    xreInt.xtcDireccion.Text = tlsOperadores.FocusedNode.GetValue("StrDireccion").ToString();
                    xreInt.xtcDepartamento.Text = tlsOperadores.FocusedNode.GetValue("StrDepartamento").ToString();
                    xreInt.xtcUrbano.Text = tlsOperadores.FocusedNode.GetValue("StrAmbito").ToString();
                    xreInt.xtcTelefono.Text = tlsOperadores.FocusedNode.GetValue("StrTelefono").ToString();
                    xreInt.xtcMunicipio.Text = tlsOperadores.FocusedNode.GetValue("StrMunicipio") + "/" + tlsOperadores.FocusedNode.GetValue("StrLocalidad");
                    xreInt.xtcPublico.Text = (tlsOperadores.FocusedNode.GetValue("StrTipoSociedad") == null) ? "" : tlsOperadores.FocusedNode.GetValue("StrTipoSociedad").ToString();
                    xreInt.xtcMes.Text = formatoFecha.GetMonthName(Convert.ToInt32(cmbPeriodo.Value.ToString().Substring(4, 2))).ToUpper() + " - " + cmbPeriodo.Value.ToString().Substring(0, 4);
                    xreInt.xtcLugar.Text = "LUGAR :   " + cmbDepartamento.Text;
                    xreInt.xrlDia.Text = datFechaReg.Day.ToString(CultureInfo.InvariantCulture);
                    xreInt.xrlMes.Text = datFechaReg.Month.ToString(CultureInfo.InvariantCulture);
                    xreInt.xrlGestion.Text = datFechaReg.Year.ToString(CultureInfo.InvariantCulture);
                    xreInt.xrlResponsable.Text = strRep[0].Trim();
                    xreInt.xtcCiResponsable.Text = "C.I.: " + strRep[1].Trim();
                    xreInt.xrlUsuarioEmisor.Text = txtPadUsuEmisor.Text.Trim();
                    xreInt.xtcCiUsuarioEmisor.Text = "C.I.: " + txtPadUsuEmisorCi.Text.Trim();
                    xreInt.xtcNoCite.Text = strCite;
                    xreInt.xbcCodigoQr.Text = strCite + "|" + cmbPeriodo.Value + "|" + datFechaReg.Year + datFechaReg.Month.ToString().PadLeft(2, '0') + datFechaReg.Day.ToString().PadLeft(2, '0');
                    xreInt.DataSource = Session[CVariablesSesion.GrillaInternacional];
                    Session[CVariablesSesion.ReporteSecundario] = xreInt;
                }

                //Recaudo Externo
                Session.Remove(CVariablesSesion.ReporteCuarto);
                if (Session[CVariablesSesion.GrillaRecaudo] != null)
                {
                    var xreRex = new repDjRecaudoExterno();
                    xreRex.xtcCabCodigo.Text = vecCabecera[0] + " ANEXO 3";
                    xreRex.xtcCabVersion.Text = vecCabecera[1];
                    xreRex.xtcCabAprobado.Text = vecCabecera[2];

                    xreRex.xtcRazonSocial.Text = tlsOperadores.FocusedNode.GetValue("StrDescripcion").ToString();
                    xreRex.xtcActividad.Text = tlsOperadores.FocusedNode.GetValue("StrActividad").ToString();
                    xreRex.xtcDireccion.Text = tlsOperadores.FocusedNode.GetValue("StrDireccion").ToString();
                    xreRex.xtcDepartamento.Text = tlsOperadores.FocusedNode.GetValue("StrDepartamento").ToString();
                    xreRex.xtcUrbano.Text = tlsOperadores.FocusedNode.GetValue("StrAmbito").ToString();
                    xreRex.xtcTelefono.Text = tlsOperadores.FocusedNode.GetValue("StrTelefono").ToString();
                    xreRex.xtcMunicipio.Text = tlsOperadores.FocusedNode.GetValue("StrMunicipio") + "/" + tlsOperadores.FocusedNode.GetValue("StrLocalidad");
                    xreRex.xtcPublico.Text = (tlsOperadores.FocusedNode.GetValue("StrTipoSociedad") == null) ? "" : tlsOperadores.FocusedNode.GetValue("StrTipoSociedad").ToString();
                    xreRex.xtcMes.Text = formatoFecha.GetMonthName(Convert.ToInt32(cmbPeriodo.Value.ToString().Substring(4, 2))).ToUpper() + " - " + cmbPeriodo.Value.ToString().Substring(0, 4);
                    xreRex.xtcLugar.Text = "LUGAR :   " + cmbDepartamento.Text;
                    xreRex.xrlDia.Text = datFechaReg.Day.ToString(CultureInfo.InvariantCulture);
                    xreRex.xrlMes.Text = datFechaReg.Month.ToString(CultureInfo.InvariantCulture);
                    xreRex.xrlGestion.Text = datFechaReg.Year.ToString(CultureInfo.InvariantCulture);
                    xreRex.xrlResponsable.Text = strRep[0].Trim();
                    xreRex.xtcCiResponsable.Text = "C.I.: " + strRep[1].Trim();
                    xreRex.xrlUsuarioEmisor.Text = txtPadUsuEmisor.Text.Trim();
                    xreRex.xtcCiUsuarioEmisor.Text = "C.I.: " + txtPadUsuEmisorCi.Text.Trim();
                    xreRex.xtcNoCite.Text = strCite;
                    xreRex.xbcCodigoQr.Text = strCite + "|" + cmbPeriodo.Value + "|" + datFechaReg.Year + datFechaReg.Month.ToString().PadLeft(2, '0') + datFechaReg.Day.ToString().PadLeft(2, '0');
                    xreRex.DataSource = Session[CVariablesSesion.GrillaRecaudo];
                    Session[CVariablesSesion.ReporteCuarto] = xreRex;
                }

                //vtas en contenedores
                Session.Remove(CVariablesSesion.ReporteTercero);
                if (Session[CVariablesSesion.GrillaAuxiliar] != null)
                {
                    var xreCont = new repDjContenedor();
                    xreCont.xtcCabCodigo.Text = vecCabecera[0] + " ANEXO 3";
                    xreCont.xtcCabVersion.Text = vecCabecera[1];
                    xreCont.xtcCabAprobado.Text = vecCabecera[2];

                    xreCont.xtcRazonSocial.Text = tlsOperadores.FocusedNode.GetValue("StrDescripcion").ToString();
                    xreCont.xtcActividad.Text = tlsOperadores.FocusedNode.GetValue("StrActividad").ToString();
                    xreCont.xtcDireccion.Text = tlsOperadores.FocusedNode.GetValue("StrDireccion").ToString();
                    xreCont.xtcDepartamento.Text = tlsOperadores.FocusedNode.GetValue("StrDepartamento").ToString();
                    xreCont.xtcUrbano.Text = tlsOperadores.FocusedNode.GetValue("StrAmbito").ToString();
                    xreCont.xtcTelefono.Text = tlsOperadores.FocusedNode.GetValue("StrTelefono").ToString();
                    xreCont.xtcMunicipio.Text = tlsOperadores.FocusedNode.GetValue("StrMunicipio") + "/" + tlsOperadores.FocusedNode.GetValue("StrLocalidad");
                    xreCont.xtcPublico.Text = (tlsOperadores.FocusedNode.GetValue("StrTipoSociedad") == null) ? "" : tlsOperadores.FocusedNode.GetValue("StrTipoSociedad").ToString();
                    xreCont.xtcMes.Text = formatoFecha.GetMonthName(Convert.ToInt32(cmbPeriodo.Value.ToString().Substring(4, 2))).ToUpper() + " - " + cmbPeriodo.Value.ToString().Substring(0, 4);
                    xreCont.xtcLugar.Text = "LUGAR :   " + cmbDepartamento.Text;
                    xreCont.xrlDia.Text = datFechaReg.Day.ToString(CultureInfo.InvariantCulture);
                    xreCont.xrlMes.Text = datFechaReg.Month.ToString(CultureInfo.InvariantCulture);
                    xreCont.xrlGestion.Text = datFechaReg.Year.ToString(CultureInfo.InvariantCulture);
                    xreCont.xrlResponsable.Text = strRep[0].Trim();
                    xreCont.xtcCiResponsable.Text = "C.I.: " + strRep[1].Trim();
                    xreCont.xrlUsuarioEmisor.Text = txtPadUsuEmisor.Text.Trim();
                    xreCont.xtcCiUsuarioEmisor.Text = "C.I.: " + txtPadUsuEmisorCi.Text.Trim();
                    xreCont.xtcNoCite.Text = strCite;
                    xreCont.xbcCodigoQr.Text = strCite + "|" + cmbPeriodo.Value + "|" + datFechaReg.Year + datFechaReg.Month.ToString().PadLeft(2, '0') + datFechaReg.Day.ToString().PadLeft(2, '0');
                    xreCont.DataSource = Session[CVariablesSesion.GrillaAuxiliar];
                    Session[CVariablesSesion.ReporteTercero] = xreCont;
                }
            }

            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( 'WFReporte.aspx', '')", true);
        }


        private string sector(string depto)
        {
            string ubicacion = "";

            if (depto == "LA PAZ")
            {
                ubicacion = "Occidente";
            }
            else if (depto == "ORURO")
            {
                ubicacion = "Occidente";
            }
            else if (depto == "POTOSI")
            {
                ubicacion = "Occidente";
            }
            else if (depto == "PANDO")
            {
                ubicacion = "Oriente";
            }
            else if (depto == "BENI")
            {
                ubicacion = "Oriente";
            }
            else if (depto == "SANTA CRUZ")
            {
                ubicacion = "OcciOrientedente";
            }
            else if (depto == "COCHABAMBA")
            {
                ubicacion = "Sur";
            }
            else if (depto == "CHUQUISACA")
            {
                ubicacion = "Sur";
            }
            else if (depto == "TARIJA")
            {
                ubicacion = "Sur";
            }
            else { ubicacion = "SIN"; }

            return ubicacion;
        }

        private string ObtenerPeriodo(string FechaHora)
        {
            string strMes = FechaHora.Substring(4, 2);
            string strGestion = FechaHora.Substring(0, 4);
            string strPeriodo = "";

            switch (strMes)
            {
                case "01":
                    strPeriodo = "Ene - " + strGestion;
                    break;
                case "02":
                    strPeriodo = "Feb - " + strGestion;
                    break;
                case "03":
                    strPeriodo = "Mar - " + strGestion;
                    break;
                case "04":
                    strPeriodo = "Abr - " + strGestion;
                    break;
                case "05":
                    strPeriodo = "May - " + strGestion;
                    break;
                case "06":
                    strPeriodo = "Jun - " + strGestion;
                    break;
                case "07":
                    strPeriodo = "Jul - " + strGestion;
                    break;
                case "08":
                    strPeriodo = "Ago - " + strGestion;
                    break;
                case "09":
                    strPeriodo = "Sep - " + strGestion;
                    break;
                case "10":
                    strPeriodo = "Oct - " + strGestion;
                    break;
                case "11":
                    strPeriodo = "Nov - " + strGestion;
                    break;
                case "12":
                    strPeriodo = "Dic - " + strGestion;
                    break;
            }

            return strPeriodo;
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
        /// METODO. MPuImprimirMensaje
        /// </summary>
        /// <param name="strMensaje"></param>
        /// <param name="tipoMensaje"></param>
        private void MPuImprimirMensaje(string strMensaje, EnumTiposMensajeAlerta tipoMensaje)
        {
            switch (tipoMensaje)
            {
                case EnumTiposMensajeAlerta.Error:
                    pnlPuMensaje.Visible = true;
                    pnlPuMensaje.BorderColor = Color.DarkRed;
                    pnlPuMensaje.BackColor = Color.Pink;
                    strMensaje = "ERROR: " + strMensaje;
                    break;
                case EnumTiposMensajeAlerta.Alerta:
                    pnlPuMensaje.Visible = true;
                    pnlPuMensaje.BorderColor = Color.OrangeRed;
                    pnlPuMensaje.BackColor = Color.LightSalmon;
                    strMensaje = "  ALERTA: " + strMensaje;
                    break;
                case EnumTiposMensajeAlerta.Correcto:
                    pnlPuMensaje.Visible = true;
                    pnlPuMensaje.BorderColor = Color.DarkGreen;
                    pnlPuMensaje.BackColor = Color.DarkSeaGreen;
                    strMensaje = "  CORRECTO: " + strMensaje;
                    break;
                case EnumTiposMensajeAlerta.Limpiar:
                    pnlPuMensaje.Visible = false;
                    lblPuMensajeError.ForeColor = Color.WhiteSmoke;
                    strMensaje = "-";
                    break;
                default:
                    pnlPuMensaje.Visible = false;
                    lblPuMensajeError.ForeColor = Color.WhiteSmoke;
                    strMensaje = "-";
                    break;
            }
            lblPuMensajeError.Text = "   " + strMensaje + "   ";
        }

        #endregion



    }
}