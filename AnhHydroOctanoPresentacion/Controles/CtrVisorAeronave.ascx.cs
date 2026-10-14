
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

    public partial class CtrVisorAeronave : UserControl
    {
        #region Variables de entorno

        private readonly CPersistenciaConsulta _consulta = new CPersistenciaConsulta();
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
                        Session.Remove(CVariablesSesion.GrillaAuxiliar);
                        Session.Remove(CVariablesSesion.GrillaCabecera);
                        Session.Remove(CVariablesSesion.ListaTransaccion);
                        Session.Remove(CVariablesSesion.GrillaHistorial);

                        MCargaLateral();
                    }
                    else
                    {
                        //todo No implementado.
                    }

                    if (dgvListado.DataSource == null && Session[CVariablesSesion.GrillaAuxiliar] != null)
                    {
                        dgvListado.DataSource = Session[CVariablesSesion.GrillaAuxiliar];
                        dgvListado.DataBind();
                    }
                    else
                    {
                        //todo no implementado
                    }
                    if (dgvCabecera.DataSource == null && Session[CVariablesSesion.GrillaCabecera] != null)
                    {
                        dgvCabecera.DataSource = Session[CVariablesSesion.GrillaCabecera];
                        dgvCabecera.DataBind();
                    }
                    else
                    {
                        //todo no implementado
                    }
                    if (imgFotos.DataSource == null && Session[CVariablesSesion.ListaInfraestructura] != null)
                    {
                        imgFotos.DataSource = Session[CVariablesSesion.ListaInfraestructura];
                        imgFotos.DataBind();
                    }
                    else
                    {
                        //todo no implementado
                    }
                    if (grid.DataSource == null && Session[CVariablesSesion.GrillaHistorial] != null)
                    {
                        grid.DataSource = Session[CVariablesSesion.GrillaHistorial];
                        grid.DataBind();
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

        protected void pnlLateral_Callback(object sender, CallbackEventArgsBase e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                        MCargaLateral();

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
                //todo no implementado
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
                    int index = dgvListado.FocusedRowIndex;
                    if (index != -1)
                    {
                        if (rblTipoAgrupacion.Items[0].Selected)
                        {
                            lblSubTitulo.Text = "DATOS DEL EXPLOTADOR";
                            lblRazonSocial.Text = dgvListado.GetRowValues(index, "EXPLOTADOR").ToString();
                            lblPais.Text = dgvListado.GetRowValues(index, "PAIS_EXPLOTADOR").ToString();
                            lblCiudad.Text = dgvListado.GetRowValues(index, "CIUDAD_EXPLOTADOR").ToString();
                            lblDireccion.Text = dgvListado.GetRowValues(index, "DOMICILIO_EXPLOTADOR").ToString();
                        }
                        else
                        {
                            lblSubTitulo.Text = "DATOS DEL PROPIETARIO";
                            lblRazonSocial.Text = dgvListado.GetRowValues(index, "PROPIETARIO").ToString();
                            lblPais.Text = dgvListado.GetRowValues(index, "PAIS_PROPIETARIO").ToString();
                            lblCiudad.Text = dgvListado.GetRowValues(index, "CIUDAD_PROPIETARIO").ToString();
                            lblDireccion.Text = dgvListado.GetRowValues(index, "DOMICILIO_PROPIETARIO").ToString();
                        }
                        MCargaGrilla(index);
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
                //todo no implementado
            }
        }
        
        protected void dgvCabecera_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            try
            {
                #region Proceso a Ejecutar

                if (e.VisibleIndex >= 0)
                {
                    if (dgvCabecera.GetRowValues(e.VisibleIndex, "AUD_ESTADO").ToString().Equals("3"))
                        e.Row.BackColor = Color.FromArgb(215, 135, 135);
                    else if (dgvCabecera.GetRowValues(e.VisibleIndex, "ESTADO_BLOQUEO").ToString()!="1")
                        e.Row.BackColor = Color.FromArgb(215, 172, 135);
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

        protected void pucFoto_WindowCallback(object source, PopupWindowCallbackArgs e)
        {
            try
            {
                #region Proceso a Ejecutar
                string strBarcode = "";
                int index = dgvCabecera.FocusedRowIndex;
                if (index >= 0)
                {
                    strBarcode = dgvCabecera.GetRowValues(index, "BARCODE").ToString();
                }
                if (strBarcode.Trim().Length > 0)
                {
                    var objResultado = _consulta.ObtenerAeronaves(
                    new EObjetoEnvio1ParametrosProto
                    {
                        decFiltro = 3,
                        strParametro1 = strBarcode//"6800989418"//strBarcode
                    });
                    if (objResultado.intCodigo == 1)
                    {
                        var lstAeronavesImg = (List<O_IMAGEN_MTR_CTY>)objResultado.oResultado;
                        imgFotos.DataSource = lstAeronavesImg;
                        imgFotos.DataBind();
                        Session[CVariablesSesion.ListaInfraestructura] = lstAeronavesImg;
                    }
                    else
                    {
                        MImprimirMensaje(objResultado.strMensaje, EnumTiposMensajeAlerta.Error);
                    }
                }
                else
                {
                    MImprimirMensaje("Tarjeta no asignada", EnumTiposMensajeAlerta.Error);
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

        protected void btnEmitir_Click(object sender, EventArgs e)
        {
            try
            {
                #region Proceso a Ejecutar
                dgeExportar.GridViewID = "dgvCabecera";
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

        protected void grid_DetailRowExpandedChanged(object sender, ASPxGridViewDetailRowEventArgs e)
        {
            try
            {
                #region Proceso a Ejecutar
                if (e.Expanded)
                {
                    var lstDetalle = (List<O_BARCODE_CXC_CTY>)Session[CVariablesSesion.GrillaDetalle];
                    var detailGrid = grid.FindDetailRowTemplateControl(e.VisibleIndex, "detailGrid") as ASPxGridView;

                    decimal decBarcode = Convert.ToInt32(grid.GetRowValues(e.VisibleIndex, "BARCODE"));
                    lstDetalle = lstDetalle.Where(x => x.BARCODE == decBarcode).ToList();

                    detailGrid.DataSource = lstDetalle;
                    detailGrid.DataBind();
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

        protected void pucHistorial_WindowCallback(object source, PopupWindowCallbackArgs e)
        {
            try
            {
                #region Proceso a Ejecutar
                grid.DataSource = null;
                grid.DataBind();
                Session.Remove(CVariablesSesion.GrillaHistorial);

                //ORIGEN: .JSON
                var objResultado = new EResultadoAuxiliar();
                using (StreamReader r = new StreamReader("d:\\bc_cxc.json"))
                {
                    string json = r.ReadToEnd();
                    objResultado = JsonSerializer.DeserializeFromString<EResultadoAuxiliar>(json);
                }
                if (objResultado.intCodigo == 1)
                {
                    var lstHistorial = objResultado.oResultado;
                    grid.DataSource = lstHistorial;
                    grid.DataBind();
                    Session[CVariablesSesion.GrillaHistorial] = lstHistorial;
                }

                Session.Remove(CVariablesSesion.GrillaDetalle);
                var objResultDet = new EResultadoAuxiliar1();
                using (StreamReader r = new StreamReader("d:\\detalle_cxc.json"))
                {
                    string json = r.ReadToEnd();
                    objResultDet = JsonSerializer.DeserializeFromString<EResultadoAuxiliar1>(json);
                }
                if (objResultDet.intCodigo == 1)
                {
                    var lstDetalle = objResultDet.oResultado;
                    lstDetalle.ForEach(a => a.FECHA_STR = a.FECHA.ToString().Substring(6, 2) + "/" + a.FECHA.ToString().Substring(4, 2) + "/" + a.FECHA.ToString().Substring(0, 4) + " " + a.FECHA.ToString().Substring(8, 2) + ":" + a.FECHA.ToString().Substring(10, 2) + ":" + a.FECHA.ToString().Substring(12, 2));
                    Session[CVariablesSesion.GrillaDetalle] = lstDetalle;
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

        protected void grid_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            try
            {
                #region Codigo a controlar

                if (e.VisibleIndex >= 0)
                {
                    if (e.VisibleIndex % 2 == 1)
                    {
                        e.Row.BackColor = Color.FromArgb(227, 226, 225);
                    }
                    else
                    {
                        //todo No implementado.
                    }

                    string valor = grid.GetRowValues(e.VisibleIndex, "ESTADO").ToString();
                    switch (valor)
                    {
                        case "VIGENTE": //EN PLAZO
                            e.Row.ForeColor = Color.FromArgb(0, 156, 0);
                            break;
                        case "VENCIDO": //PLAZO VENCIDO
                            e.Row.ForeColor = Color.FromArgb(124, 7, 0);
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

        protected void detailGrid_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            try
            {
                #region Codigo a controlar

                if (e.VisibleIndex >= 0)
                {
                    if (e.VisibleIndex % 2 == 1)
                    {
                        e.Row.BackColor = Color.FromArgb(241, 236, 221);
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

        protected void btnReporte_Click(object sender, EventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    if ((int)cmbTipoExport.Value == 1)
                    {
                        dgeExportar.GridViewID = "grid";
                        dgeExportar.WriteXlsxToResponse();
                    }
                    else
                    {
                        var lstReporte = new List<O_BARCODE_CXC_REPORTE>();
                        var lstHistorial = (List<O_BARCODE_HISTORIAL_CTY>)Session[CVariablesSesion.GrillaHistorial];

                        List<O_BARCODE_CXC_CTY> lstDetalle = null;
                        if ((int)cmbTipoExport.Value == 3)
                        {
                            lstDetalle = (List<O_BARCODE_CXC_CTY>)Session[CVariablesSesion.GrillaDetalle];
                        }

                        foreach (var cab in lstHistorial)
                        {
                            var reg1 = new O_BARCODE_CXC_REPORTE();
                            reg1.BARCODE = cab.BARCODE;
                            reg1.TIPO = cab.TIPO;
                            reg1.FECHA_ENTREGA = cab.FECHA_ENTREGA;
                            reg1.LUGAR_ENTREGA = cab.LUGAR_ENTREGA;
                            reg1.USUARIO_ENTREGA = cab.USUARIO_ENTREGA;
                            reg1.ESTADO = cab.ESTADO;
                            reg1.CARGO = cab.CARGO;
                            reg1.ABONO = cab.ABONO;
                            reg1.SALDO = cab.SALDO;

                            if (lstDetalle != null)
                            {
                                var subDet = lstDetalle.Where(x => x.BARCODE == cab.BARCODE).OrderBy(x => x.ID_TIPO).ThenBy(x => x.FECHA).ToList();
                                if (subDet.Any())
                                {
                                    decimal decSaldo = 0;
                                    foreach (var det in subDet)
                                    {
                                        var reg2 = new O_BARCODE_CXC_REPORTE();
                                        reg2.BARCODE = reg1.BARCODE;
                                        reg2.TIPO = reg1.TIPO;
                                        reg2.FECHA_ENTREGA = reg1.FECHA_ENTREGA;
                                        reg2.LUGAR_ENTREGA = reg1.LUGAR_ENTREGA;
                                        reg2.USUARIO_ENTREGA = reg1.USUARIO_ENTREGA;
                                        reg2.ESTADO = reg1.ESTADO;
                                        reg2.CARGO = reg1.CARGO;
                                        reg2.ABONO = reg1.ABONO;
                                        reg2.SALDO = reg1.SALDO;
                                        reg2.E_SALDO = det.ID_TIPO == 1 ? reg1.SALDO : 0;
                                        reg2.D_TIPO = det.TIPO;
                                        reg2.D_FECHA_STR = det.FECHA_STR;
                                        reg2.D_USUARIO = det.USUARIO;
                                        reg2.D_ESTADO = det.ESTADO;
                                        reg2.D_TIPO_PAGO = det.TIPO_PAGO;
                                        reg2.D_BANCO = det.BANCO;
                                        reg2.D_NRO_CTA = det.NRO_CTA;
                                        reg2.D_NRO_REF = det.NRO_REF;
                                        reg2.D_CARGO = det.CARGO;
                                        reg2.D_ABONO = det.ABONO;
                                        reg2.D_SALDO = decSaldo + det.CARGO - det.ABONO;
                                        lstReporte.Add(reg2);
                                        decSaldo = reg2.D_SALDO;
                                    }
                                }
                            }
                            else
                            {
                                reg1.E_SALDO = cab.SALDO;
                                reg1.D_CARGO = cab.CARGO;
                                reg1.D_ABONO = cab.ABONO;
                                lstReporte.Add(reg1);
                            }
                        }

                        var xreReporte = new repHistorialTarjeta();
                        xreReporte.xtcFecha.Text = DateTime.Now.ToShortDateString();
                        xreReporte.xtcHora.Text = DateTime.Now.ToLongTimeString();
                        xreReporte.xtcUsuario.Text = CParametrosHydro.StrUsuarioLogin;

                        int index = dgvCabecera.FocusedRowIndex;
                        if (index != -1)
                        {
                            xreReporte.xtcMatricula.Text = dgvCabecera.GetRowValues(index, "IDENTIFICADOR").ToString();
                            xreReporte.xtcPropietario.Text = dgvCabecera.GetRowValues(index, "PROPIETARIO").ToString();
                            xreReporte.xtcExplotador.Text = dgvCabecera.GetRowValues(index, "EXPLOTADOR").ToString();
                            xreReporte.xtcClase.Text = dgvCabecera.GetRowValues(index, "CLASE").ToString();
                            xreReporte.xtcMarca.Text = dgvCabecera.GetRowValues(index, "MARCA").ToString();
                            xreReporte.xtcServicio.Text = dgvCabecera.GetRowValues(index, "SERVICIO").ToString();
                        }
                        if ((int)cmbTipoExport.Value == 2)
                        {
                            xreReporte.xtcTipoRep.Text = "TIPO - GENERAL";
                            xreReporte.sbnCabDet.Visible = false;
                            xreReporte.Detail.Visible = false;
                            xreReporte.grfBarcode.Visible = false;
                        }
                        else
                        {
                            xreReporte.xtcTipoRep.Text = "TIPO - DETALLADO";
                        }

                        xreReporte.DataSource = lstReporte;
                        Session[CVariablesSesion.ReportePrincipal] = xreReporte;
                        Session[CVariablesSesion.TipoReporte] = EnumTipoReporte.RepHistorialTarjeta;
                        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( '../Movimientos/WFReporte.aspx', '');", true);
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

        protected void pucConsumo_WindowCallback(object source, PopupWindowCallbackArgs e)
        {
            var detFecha = DateTime.Now;
            detFechaIni.Date = DateTime.Parse("01/" + detFecha.Month.ToString().PadLeft(2, '0') + "/" + detFecha.Year.ToString().PadLeft(2, '0'));
            detFechaFin.Date = detFecha;
        }

        protected void pnlGrafico_Callback(object sender, CallbackEventArgsBase e)
        {
            string strMatricula;
            int index = dgvCabecera.FocusedRowIndex;
            if (index != -1)
            {
                strMatricula = dgvCabecera.GetRowValues(index, "IDENTIFICADOR").ToString();
            }
            else
            {
                return;
            }

            var panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cp_type"] = cmbTipoGrafico.Value;
            panel.JSProperties["cp_title"] = "Detalle del Consumo - Matricula: " + strMatricula;
            panel.JSProperties["cp_subtitle"] = "Del: " + detFechaIni.Date.ToShortDateString() + "  Al: " + detFechaFin.Date.ToShortDateString();

            var objResp = _cUtil.MGrafico_Tipo1(Convert.ToDecimal(rbtTipo.Value), Convert.ToDecimal(rbtAgrupacion.Value), "50", "0", strMatricula, detFechaIni.Date, detFechaFin.Date);
            if (objResp.intCodigo == 1)
            {
                var objGrafico = (O_GRAFICO_TIPO1)objResp.oResultado;

                panel.JSProperties["cp_ytitle"] = "Cantidad";
                panel.JSProperties["cp_xAxis"] = objGrafico.names;
                panel.JSProperties["cp_Series"] = objGrafico.series;
            }
            else
            {
                panel.JSProperties["cp_xAxis"] = null;
                panel.JSProperties["cp_Series"] = null;
            }
        }

        protected void pucDatos_WindowCallback(object source, PopupWindowCallbackArgs e)
        {
            //todo agregar detalle
            var xx = e.Parameter;
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
        private void MCargaLateral()
        {
            var objResultado = _consulta.ObtenerAeronaves(
                new EObjetoEnvio1ParametrosProto
                {
                    decFiltro = (chkFiltro.Checked)? 1 : 2,
                    strParametro1 = ""
                });
            if (objResultado.intCodigo == 1)
            {
                var lstAeronaves = (List<O_OBTENER_AERONAVES_CTY>)objResultado.oResultado;

                Session[CVariablesSesion.ListaTransaccion] = lstAeronaves;
                var lstExplotador = lstAeronaves;

                dgvListado.Columns[2].Visible = false;
                dgvListado.Columns[7].Visible = false;

                if (rblTipoAgrupacion.Items[0].Selected)
                {
                    dgvListado.Columns[2].Visible = true;
                    lstExplotador = lstAeronaves
                      .GroupBy(a => a.ID_EXPLOTADOR)
                      .Select(g => g.First())
                      .ToList();
                }
                else
                {
                    dgvListado.Columns[7].Visible = true;
                    lstExplotador = lstAeronaves
                      .GroupBy(a => a.ID_PROPIETARIO)
                      .Select(g => g.First())
                      .ToList();
                }

                lstExplotador.Add(new O_OBTENER_AERONAVES_CTY
                {
                    ID_EXPLOTADOR = -99999,
                    EXPLOTADOR = "_MOSTRAR TODO",
                    CIUDAD_EXPLOTADOR = "",
                    DOMICILIO_EXPLOTADOR = "",
                    PAIS_EXPLOTADOR = "",
                    ID_PROPIETARIO = -99999,
                    PROPIETARIO = "_MOSTRAR TODO",
                    CIUDAD_PROPIETARIO = "",
                    DOMICILIO_PROPIETARIO = "",
                    PAIS_PROPIETARIO = ""
                });

                if (rblTipoAgrupacion.Items[0].Selected)
                {
                    lstExplotador = lstExplotador.OrderBy(x => x.EXPLOTADOR).ToList();
                }
                else
                {
                    lstExplotador = lstExplotador.OrderBy(x => x.PROPIETARIO).ToList();
                }

                dgvListado.DataSource = lstExplotador;
                dgvListado.DataBind();

                Session[CVariablesSesion.GrillaAuxiliar] = lstExplotador;
            }
            else
            {
                MImprimirMensaje(objResultado.strMensaje, EnumTiposMensajeAlerta.Error);
            }


            //ORIGEN: .JSON
            //var objResultado = new EResultadoLstAeronave();
            //using (StreamReader r = new StreamReader("d:\\aeronaves.json"))
            //{
            //    string json = r.ReadToEnd();
            //    objResultado = JsonSerializer.DeserializeFromString<EResultadoLstAeronave>(json);
            //}

            
            
        }

        /// <summary>
        /// METODO. MCargaGrilla
        /// </summary>
        /// <param name="intIndex"></param>
        private void MCargaGrilla(int intIndex)
        {
            decimal decId;
            dgvCabecera.Columns[(int)EnumColumnasGrilla.VAeExplotador].Visible = false;
            dgvCabecera.Columns[(int)EnumColumnasGrilla.VAePropietario].Visible = false;
            dgvCabecera.Columns[(int)EnumColumnasGrilla.VAeEstado].Visible = !chkFiltro.Checked;

            if (rblTipoAgrupacion.Items[0].Selected)
            {
                decId = Convert.ToDecimal(dgvListado.GetRowValues(intIndex, "ID_EXPLOTADOR"));
            }
            else
            {
                decId = Convert.ToDecimal(dgvListado.GetRowValues(intIndex, "ID_PROPIETARIO"));
            }

            MLimpiaGrilla();
            if (decId != null)
            {
                var lstAeronaves = Session[CVariablesSesion.ListaTransaccion] as List<O_OBTENER_AERONAVES_CTY>;
                if (decId != -99999)
                {
                    if (rblTipoAgrupacion.Items[0].Selected)
                    {
                        lstAeronaves = lstAeronaves.Where(x => x.ID_EXPLOTADOR == decId).ToList();
                    }
                    else
                    {
                        lstAeronaves = lstAeronaves.Where(x => x.ID_PROPIETARIO == decId).ToList();
                    }
                }
                else
                {
                    if (rblTipoAgrupacion.Items[0].Selected)
                    {
                        dgvCabecera.Columns[(int)EnumColumnasGrilla.VAeExplotador].Visible = true;
                    }
                    else
                    {
                        dgvCabecera.Columns[(int)EnumColumnasGrilla.VAePropietario].Visible = true;
                    }
                    
                }
                Session[CVariablesSesion.GrillaCabecera] = lstAeronaves;
                dgvCabecera.DataSource = lstAeronaves;
                dgvCabecera.DataBind();
            }
        }

        /// <summary>
        /// METODO. MLimpiaGrilla
        /// </summary>
        private void MLimpiaGrilla()
        {
            Session.Remove(CVariablesSesion.GrillaCabecera);
            dgvCabecera.DataSource = null;
            dgvCabecera.DataBind();
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