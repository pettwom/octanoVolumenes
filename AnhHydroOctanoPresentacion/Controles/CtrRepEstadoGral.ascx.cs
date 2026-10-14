using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using AnhHydroOctanoPresentacion.Clases.Controles;
using AnhHydroOctanoPresentacion.Entidades;
using AnhHydroOctanoPresentacion.Enumeradores;
using AnhHydroOctanoPresentacion.Parametros;
using DevExpress.Web;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using Librerias.Anh.Us;
using Microsoft.Ajax.Utilities;
using ServiceStack.Common;
using ServiceStack.ServiceClient.Web;
using ServiceStack.Text;
using Page = System.Web.UI.Page;

namespace AnhHydroOctanoPresentacion.Controles
{
    public partial class CtrRepEstadoGral : UserControl
    {
        #region Variables de entorno

        JsonServiceClient client = new JsonServiceClient(CAppSettings.servicio_octVolumen);
        private string _strMensajeError;
        private decimal decIngRecepcion, decIngDemasia, decIngreso, decSalVenta, decSalMerma, decSalida;
        private decimal decMontoSum;
        #endregion

        #region Eventos de Formulario

        protected void Page_Load(object sender, EventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Codigo a controlar
                    if (!Page.IsCallback && !Page.IsPostBack)
                    {
                        Session.Remove(CVariablesSesion.EstructuraArbol);
                        Session.Remove(CVariablesSesion.GrillaAcumulado);
                        Session.Remove(CVariablesSesion.GrillaInternacional);
                        MCargaArbol();
                        MIniciaFechas();
                    }
                    tlsOperadores.DataSource = Session[CVariablesSesion.EstructuraArbol];
                    tlsOperadores.DataBind();

                    if (Session[CVariablesSesion.GrillaAcumulado] != null)
                    {
                        pvgReporte.DataSource = Session[CVariablesSesion.GrillaAcumulado];
                        pvgReporte.DataBind();
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
            try
            {
                lblRazonSocial.Text = tlsOperadores.FocusedNode.GetValue("StrDescPadre").ToString();
                lblNit.Text = tlsOperadores.FocusedNode.GetValue("LngNit").ToString();
                lblRepLegal.Text = tlsOperadores.FocusedNode.GetValue("StrDireccion").ToString();
                lblDepto.Text = tlsOperadores.FocusedNode.GetValue("StrDepartamento").ToString();
                MLimpiaGrilla();
                if ((int)tlsOperadores.FocusedNode.GetValue("IntAccion") > 0)
                {
                    if (MValidaDatosIniciales())
                    {
                        int intDia = DateTime.DaysInMonth((int)spnGestion.Number, (int)spnMes.Number);

                        int intFiltro = 1;
                        string strDato1 = tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString();
                        string strDato2 = spnGestion.Number+spnMes.Number.ToString(CultureInfo.InvariantCulture).PadLeft(2,'0')+"01000000";
                        string strDato3 = spnGestion.Number + spnMes.Number.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + intDia.ToString(CultureInfo.InvariantCulture).PadLeft(2,'0') + "235959";
                        string strDato4 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString();
                        string strDato5 = "0";

                        string strDatE1 = tlsOperadores.FocusedNode.GetValue("IntIdEntidad").ToString();

                        MConfiguraGrilla(Convert.ToInt32(strDato4));
                        MCargaGrilla(intFiltro, strDato1, strDato2, strDato3, strDato4, strDato5, strDatE1);
                    }
                }
            }
            catch (Exception)
            {
                MLimpiaGrilla();
            }
        }
        
        protected void dgvAcumulado_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            switch ((e.Item as ASPxSummaryItem).Tag)
            {
                case "rec":
                    if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Start)
                        decIngRecepcion = 0;

                    if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Calculate)
                        decIngRecepcion += Convert.ToDecimal(e.GetValue("DecRecepcion").ToString());

                    if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                        e.TotalValue = decIngRecepcion;
                    break;
                case "dem":
                    if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Start)
                        decIngRecepcion = 0;

                    if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Calculate)
                        decIngRecepcion += Convert.ToDecimal(e.GetValue("DecDemasia").ToString());

                    if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                        e.TotalValue = decIngRecepcion;
                    break;
                case "ing":
                    if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Start)
                        decIngRecepcion = 0;

                    if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Calculate)
                        decIngRecepcion += Convert.ToDecimal(e.GetValue("DecTotalIng").ToString());

                    if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                        e.TotalValue = decIngRecepcion;
                    break;
                case "ven":
                    if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Start)
                        decSalVenta = 0;

                    if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Calculate)
                        decSalVenta += Convert.ToDecimal(e.GetValue("DecVenta").ToString());

                    if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                        e.TotalValue = decSalVenta;
                    break;
                case "mer":
                    if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Start)
                        decSalVenta = 0;

                    if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Calculate)
                        decSalVenta += Convert.ToDecimal(e.GetValue("DecMerma").ToString());

                    if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                        e.TotalValue = decSalVenta;
                    break;
                case "sal":
                    if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Start)
                        decSalVenta = 0;

                    if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Calculate)
                        decSalVenta += Convert.ToDecimal(e.GetValue("DecTotalSal").ToString());

                    if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                        e.TotalValue = decSalVenta;
                    break;
            }
        }

        protected void dgvAcumulado_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            if (e.VisibleIndex >= 0)
            {
                int valor = (int)dgvAcumulado.GetRowValues(e.VisibleIndex, "IntIdProducto");
                switch (valor)
                {
                    case (int)EnumProductos.Gnv:
                        e.Row.BackColor = ((int)dgvAcumulado.GetRowValues(e.VisibleIndex, "IntId") % 2 == 1) ? Color.FromArgb(249, 131, 131) : Color.FromArgb(255, 207, 210);
                        break;
                    case (int)EnumProductos.Diesel:
                        e.Row.BackColor = ((int)dgvAcumulado.GetRowValues(e.VisibleIndex, "IntId") % 2 == 1) ? Color.FromArgb(131, 146, 249) : Color.FromArgb(207, 226, 255);
                        break;
                    case (int)EnumProductos.GasolinaEspecial:
                        e.Row.BackColor = ((int)dgvAcumulado.GetRowValues(e.VisibleIndex, "IntId") % 2 == 1) ? Color.FromArgb(249, 131, 131) : Color.FromArgb(255, 207, 210);
                        break;
                    case (int)EnumProductos.GasolinaPremium:
                        e.Row.BackColor = ((int)dgvAcumulado.GetRowValues(e.VisibleIndex, "IntId") % 2 == 1) ? Color.FromArgb(249, 131, 131) : Color.FromArgb(255, 207, 210);
                        break;
                }
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

        private void MIniciaFechas()
        {
            spnMes.Number = DateTime.Now.Month;
            spnGestion.Number = DateTime.Now.Year;
        }
        private void MCargaArbol()
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
                    var lstEntidad = new List<O_OBTIENE_ENTIDADES_USR_CTY>();
                    lstEntidad = lista2.OResultado;
                    lstEntidad = lstEntidad.OrderBy(x => x.DENOMINACION_PADRE).ThenBy(x => x.ACTIVIDAD).ThenBy(x => x.DENOMINACION).ToList();

                    int idDe = 0, idAc = 0;
                    int idUnico = 1, idPadre = 0, idUnic1 = 0;
                    var lstResultado = new List<CArbol>();
                    foreach (var lst in lstEntidad)
                    {
                        var cDato = new CArbol();
                        if (idDe != lst.ID_ENTIDAD_PADRE)
                        {
                            cDato = new CArbol();
                            cDato.IntId = idUnico;
                            cDato.IntIdPadre = 0;
                            cDato.IntIdEntidadPadre = (int)lst.ID_ENTIDAD_PADRE;
                            cDato.IntIdActividad = 0;
                            cDato.IntIdEntidad = 0;
                            cDato.IntIdOperador = 0;
                            cDato.StrDescripcion = lst.DENOMINACION_PADRE.Trim();
                            cDato.StrDescPadre = lst.DENOMINACION_PADRE.Trim();
                            cDato.LngNit = Convert.ToInt64(lst.NIT);
                            cDato.StrLicencia = "";
                            cDato.StrRepLegal = lst.REPRESENTANTE_LEGAL;
                            cDato.StrDireccion = "";
                            cDato.StrDepartamento = "";
                            cDato.IntAccion = 0;
                            lstResultado.Add(cDato);
                            idPadre = idUnico;
                            idUnic1 = idUnico;
                            idDe = (int)lst.ID_ENTIDAD_PADRE;
                            idAc = 0;
                            idUnico++;
                        }
                        if (idAc != lst.ID_ACTIVIDAD)
                        {
                            cDato = new CArbol();
                            cDato.IntId = idUnico;
                            cDato.IntIdPadre = idUnic1;
                            cDato.IntIdEntidadPadre = (int)lst.ID_ENTIDAD_PADRE;
                            cDato.IntIdEntidad = 0;
                            cDato.IntIdOperador = 0;
                            cDato.IntIdActividad = (int)lst.ID_ACTIVIDAD;
                            cDato.StrDescripcion = lst.ACTIVIDAD;
                            cDato.StrDescPadre = lst.DENOMINACION_PADRE;
                            cDato.LngNit = Convert.ToInt64(lst.NIT);
                            cDato.StrLicencia = "";
                            cDato.StrRepLegal = lst.REPRESENTANTE_LEGAL;
                            cDato.StrDireccion = "";
                            cDato.StrDepartamento = "";
                            cDato.IntAccion = 0;
                            lstResultado.Add(cDato);
                            idPadre = idUnico;
                            idAc = (int)lst.ID_ACTIVIDAD;
                            idUnico++;
                        }
                        cDato = new CArbol();
                        cDato.IntId = idUnico;
                        cDato.IntIdPadre = idPadre;
                        cDato.IntIdEntidadPadre = (int)lst.ID_ENTIDAD_PADRE;
                        cDato.IntIdEntidad = (int)lst.ID_ENTIDAD;
                        cDato.IntIdOperador = (int)lst.ID_CONSUMIDOR;
                        cDato.IntIdActividad = (int)lst.ID_ACTIVIDAD;
                        cDato.StrDescripcion = lst.DENOMINACION;
                        cDato.StrDescPadre = lst.DENOMINACION;
                        cDato.LngNit = Convert.ToInt64(lst.NIT);
                        cDato.StrLicencia = lst.NRO_LICENCIA;
                        cDato.StrRepLegal = lst.REPRESENTANTE_LEGAL;
                        cDato.StrDireccion = lst.DIRECCION;
                        cDato.StrDepartamento = lst.DEPARTAMENTO;
                        cDato.IntAccion = 1;
                        lstResultado.Add(cDato);
                        idUnico++;
                    }
                    Session[CVariablesSesion.EstructuraArbol] = lstResultado;
                }
            }
        }
        private void MCargaGrilla(int intFiltro, string strDato1, string strDato2, string strDato3, string strDato4, string strDato5, string strDatE1)
        {
            try
            {
                var xxx = new CReporteCumplimientoMes();
                var lstReporte = new List<CReporteCumplimientoMes>();

                lstReporte = xxx.cargaReporte();

                

                Session[CVariablesSesion.GrillaAcumulado] = lstReporte;
                pvgReporte.DataSource = lstReporte;
                pvgReporte.DataBind();

                pvgReporte.Fields[7].Visible = false;
                //var LstDespachos = client.Post<EResultadoDjAcumuado>
                //    (CAppSettings.svc_declaracion_jurada,
                //    new
                //    {
                //        I_FILTRO = intFiltro, //TipoMovimiento (1=ventas)
                //        I_DATO_1 = strDato1, //Id proyecto/entidad
                //        I_DATO_2 = strDato2, //fecha inicial
                //        I_DATO_3 = strDato3, //fecha final
                //        I_DATO_4 = strDato4, //id Actividad
                //        I_DATO_5 = strDato5, //tipo operacion
                //        I_DATO_6 = "0" //producto
                //    });
                //if (LstDespachos.IntCodigo == 1)
                //{
                //    var LstResultado = new List<O_LISTA_DECLARACION_CTY>();
                //    LstResultado = LstDespachos.OResultado;

                //    if (LstResultado.Any())
                //    {
                //        decimal decSalIni = 0;
                //        decimal intprod = 0;
                //        int cont = 1;
                //        //var registros = LstResultado;
                //        //LstResultado = LstResultado.OrderBy(x => x.FECHA_OPERACION).ThenBy(x => x.ID_PRODUCTO).ToList();
                //        LstResultado = LstResultado.OrderBy(x => x.ID_HIDROCARBURO).ThenBy(x => x.FECHA_OPERACION).ToList();
                //        var lstMovimiento = new List<CMovimientoAcumulado>();
                //        foreach (var lst in LstResultado)
                //        {
                //            if (intprod != lst.ID_HIDROCARBURO)
                //            {
                //                decSalIni = 0;
                //                intprod = lst.ID_HIDROCARBURO;
                //            }

                //            var cDato = new CMovimientoAcumulado();

                //            cDato.IntId = cont;
                //            cDato.StrFecha = lst.FECHA_OPERACION.ToString().Substring(6, 2) + "/" + lst.FECHA_OPERACION.ToString().Substring(4, 2) + "/" + lst.FECHA_OPERACION.ToString().Substring(0, 4);
                //            cDato.StrProducto = ((int)lst.ID_HIDROCARBURO == (int)EnumProductos.GasolinaEspecial) ? "GE" : "DO";
                //            cDato.IntIdProducto = (int)lst.ID_HIDROCARBURO;
                //            cDato.DecSaldoInicial = (lst.SALDO_INICIAL == null) ? decSalIni : (decimal)lst.SALDO_INICIAL;
                //            cDato.DecRecepcion = (lst.RECEPCION == null) ? 0 : (decimal)lst.RECEPCION;
                //            cDato.DecDemasia = (lst.DEMASIA == null) ? 0 : (decimal)lst.DEMASIA;
                //            cDato.DecTotalIng = cDato.DecRecepcion + cDato.DecDemasia;
                //            cDato.DecVenta = (lst.SALIDA_VENTA == null) ? 0 : (decimal)lst.SALIDA_VENTA;
                //            cDato.DecMerma = (lst.MERMA == null) ? 0 : (decimal)lst.MERMA;
                //            cDato.DecTotalSal = cDato.DecVenta + cDato.DecMerma;
                //            cDato.DecSaldoFinal = cDato.DecSaldoInicial + cDato.DecTotalIng - cDato.DecTotalSal;

                //            decSalIni = cDato.DecSaldoFinal;

                //            lstMovimiento.Add(cDato);

                //            cont++;


                //        }

                //        lstMovimiento = lstMovimiento.OrderBy(x => x.StrFecha).ThenBy(x => x.StrProducto).ToList();

                //        Session[CVariablesSesion.GrillaAcumulado] = lstMovimiento;
                //        dgvAcumulado.DataSource = lstMovimiento;
                //        dgvAcumulado.DataBind();
                //    }
                //}


            }
            catch (Exception exception)
            {
                MImprimirMensaje(exception.Message + "  - MCargaGrilla", EnumTiposMensajeAlerta.Error);
            }
        }
        private bool MValidaDatosIniciales()
        {
            bool booResultado = true;

            if (spnGestion.Text == "" || spnMes.Text == "")
            {
                MImprimirMensaje("Introduzca un periodo valido.", EnumTiposMensajeAlerta.Error);
                booResultado = false;
            }
            return booResultado;
        }
        private void MConfiguraGrilla(int idActividad)
        {


        }
        private void MLimpiaGrilla()
        {
            Session.Remove(CVariablesSesion.GrillaAcumulado);
            dgvAcumulado.DataSource = null;
            dgvAcumulado.DataBind();

            pnlMensaje.Visible = false;
            dgvAcumulado.Columns[(int)EnumColumnasGrilla.FoAbm].Visible = false;
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
            if (tipoMensaje != EnumTiposMensajeAlerta.Correcto)
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError", "alert('" + strMensaje + "');", true);
            lblMensajeError.Text = "   " + strMensaje + "   ";

        }

        #endregion

        protected void LinkButton1_Click(object sender, EventArgs e)
        {

            
        }

        

        

        



        

        

        

    }
}