using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using AnhHydroOctanoPresentacion.Clases.Controles;
using AnhHydroOctanoPresentacion.Entidades;
using AnhHydroOctanoPresentacion.Enumeradores;
using AnhHydroOctanoPresentacion.Parametros;
using DevExpress.Charts.Native;
using DevExpress.Web;
using Librerias.Anh.Us;
using ServiceStack.ServiceClient.Web;
using ServiceStack.Text;

using UserControl = System.Web.UI.UserControl;

namespace AnhHydroOctanoPresentacion.Controles
{
    public partial class CtrPrincipal11 : UserControl
    {
        #region Variables de entorno

        JsonServiceClient client = new JsonServiceClient(CAppSettings.servicio_octVolumen);
        private string _strMensajeError;

        decimal totalImporte, totalCantidad;
        long totalAnulada, totalValida;
        #endregion

        #region Eventos de Formulario

        protected void Page_Load(object sender, EventArgs e)
        {
            //borrar
            Session[CVariablesSesion.IdUsuario] = "1802";

            if (Session[CVariablesSesion.IdUsuario] == null)
            {
                Response.Redirect("~/Sitio/Persona/wfAutenticacion.aspx", false);
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
            else
            {
                try
                {
                    #region Codigo a controlar

                    if (!Page.IsCallback && !Page.IsPostBack)
                    {

                        

                        Session.Remove(CVariablesSesion.EstructuraArbol);
                        Session.Remove(CVariablesSesion.GrillaTransaccion);
                        MCargaArbol();
                        MIniciaFechas();
                        MAplicaPermisos();
                        Session[CVariablesSesion.AuxValidaLlave] = 0;


                        //string nombre = "xxx";
                        ////string nombr1 = "yyy";
                        ////double lat = (double)(entOperador.latitud);
                        ////double lon = (double)(entOperador.longitud);
                        //double lat = -16.500;
                        //double lon = -64.150;

                        //GLatLng ubicacion = new GLatLng(lat, lon);

                        //GLatLng ubicacio1 = new GLatLng(36.898, -1.1333);

                        //GMap1.setCenter(ubicacion, 5);
                        //GMap1.Height = 350;
                        //GMap1.Width = 350;
                        //GMap1.Add(new GControl(GControl.preBuilt.LargeMapControl));
                        //GMap1.Add(new GControl(GControl.preBuilt.MapTypeControl));

                        //GMarker marker = new GMarker(ubicacion);
                        //GMarker marke1 = new GMarker(ubicacio1);

                        ////string strMarker = "<h3>" + lblNomEESS.Text + "</h3>";
                        //string strMarker = "<h3>" + nombre + "</h3>";
                        ////string strMarke1 = "<h3>" + nombr1 + "</h3>";

                        ////GInfoWindow window = new GInfoWindow(marker, strMarker, true);
                        ////GMap2.Add(window);

                        ////GInfoWindow windo1 = new GInfoWindow(marke1, strMarke1, true);
                        ////GMap2.Add(windo1);



                        //GMap1.enableHookMouseWheelToZoom = true;
                        //GMap1.enableGKeyboardHandler = true;

                    }
                    

                    if (tlsOperadores.DataSource == null)
                    {
                        tlsOperadores.DataSource = Session[CVariablesSesion.EstructuraArbol];
                        tlsOperadores.DataBind();
                    }
                    
                    //Session[CVariablesSesion.ObtenerEntidades] = lstEntidad;
                    if (Session[CVariablesSesion.GrillaTransaccion] != null)
                    {
                        dgvEntidades.DataSource = Session[CVariablesSesion.GrillaTransaccion];
                        dgvEntidades.DataBind();
                    }
                    #endregion
                }
                catch (Exception exp)
                {
                    var oMensajeUsuario = new CLogTraza.MensajeUsuario
                    {
                        decUsuarioId = Session[CVariablesSesion.IdUsuario],
                        decIdModulo = CParametrosHydro.decIdAplicacion,
                        strAccion = GetType().Name + "." + MethodBase.GetCurrentMethod().Name,
                        strIp = HttpContext.Current.Request.UserHostAddress,
                        decNivelCapa = (int)CLogTraza.CapasNivel.Presentacion
                    };
                    CLogTraza.Error(oMensajeUsuario, exp);
                }
            }
        }

        protected void pnlDatos_Callback(object sender, CallbackEventArgsBase e)
        {
            try
            {
                lblActividad.Text = tlsOperadores.FocusedNode.GetValue("StrDescPadre").ToString();
                MLimpiaGrilla();
                
                txtPrueba.Text = "";

                int intDato1 = (int)tlsOperadores.FocusedNode.GetValue("IntIdActividad");
                string strDato2 = tlsOperadores.FocusedNode.GetValue("StrDepartamento").ToString();
                int intDato3 = (int)tlsOperadores.FocusedNode.GetValue("IntIdOperador");
                int intDato4 = (int)tlsOperadores.FocusedNode.GetValue("IntAccion");
                MCargaGrilla(intDato1, strDato2, intDato3, intDato4);

                txtPrueba.Text = "4";
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError", "SetMarker(4);", true);


                //string nombre = "xxx";
                //double lat = -16.500;
                //double lon = -64.150;
                //GLatLng ubicacion = new GLatLng(lat, lon);
                //GMap2.setCenter(ubicacion, 12);
                //GMarker marker = new GMarker(ubicacion);
                //string strMarker = "<h3>" + nombre + "</h3>";
                //GInfoWindow window = new GInfoWindow(marker, strMarker, true);
                //GMap2.Add(window);
                //GMap2.resetMarkers();


            }
            catch (Exception)
            {
                MLimpiaGrilla();
            }
        }


        protected void dgvEntidades_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            if (e.VisibleIndex >= 0)
            {
                if (e.VisibleIndex % 2 == 1)
                    e.Row.BackColor = Color.FromArgb(229, 253, 212);
            }
        }
        

        #endregion

        #region Metodos y funciones

        private void MIniciaFechas()
        {
            //detFechaIni.Date = DateTime.Now;
            //detFechaFin.Date = DateTime.Now;
        }
        private void MAplicaPermisos()
        {
            //cmbTipoOperacion.Enabled = true;
            //cmbTipoOperacion.Value = 89;
        }

        private void MCargaArbol()
        {
            MLimpiaGrilla();
            decimal idUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
            var lista2 = client.Get<EResultadoEntidad>(CAppSettings.svc_obtener_entidades + "/" + idUsuario + "?format=json");
            if (lista2 != null)
            {
                if (lista2.IntCodigo == 1)
                {
                    var lstEntidad = new List<O_OBTIENE_ENTIDADES_USR_CTY>();
                    lstEntidad = lista2.OResultado;
                    Session[CVariablesSesion.ObtenerEntidades] = lstEntidad;
                    lstEntidad = lstEntidad.OrderBy(x => x.ACTIVIDAD).ThenBy(x => x.DEPARTAMENTO).ThenBy(x => x.DENOMINACION).ToList();

                    int idDe = 0;
                    string idAc = "";
                    int idUnico = 1, idPadre = 0, idUnic1 = 0;
                    var lstResultado = new List<CArbol>();
                    foreach (var lst in lstEntidad)
                    {
                        var cDato = new CArbol();
                        if (idDe != lst.ID_ACTIVIDAD)
                        {
                            cDato = new CArbol();
                            cDato.IntId = idUnico;
                            cDato.IntIdPadre = 0;
                            cDato.IntIdEntidad = 0;
                            cDato.IntIdActividad = (int)lst.ID_ACTIVIDAD;
                            cDato.IntIdOperador = 0;
                            cDato.StrDescripcion = lst.ACTIVIDAD.Trim();
                            cDato.StrDescPadre = lst.ACTIVIDAD.Trim();
                            cDato.LngNit = 0;
                            cDato.StrLicencia = "";
                            cDato.StrRepLegal = "";
                            cDato.StrDireccion = "";
                            cDato.StrDepartamento = "";
                            cDato.IntAccion = 1;
                            lstResultado.Add(cDato);
                            idPadre = idUnico;
                            idUnic1 = idUnico;
                            idDe = (int)lst.ID_ACTIVIDAD;
                            idAc = "";
                            idUnico++;
                        }
                        if (idAc != lst.DEPARTAMENTO)
                        {
                            cDato = new CArbol();
                            cDato.IntId = idUnico;
                            cDato.IntIdPadre = idUnic1;
                            cDato.IntIdEntidad = 0;
                            cDato.IntIdOperador = 0;
                            cDato.IntIdActividad = (int)lst.ID_ACTIVIDAD;
                            cDato.StrDescripcion = lst.DEPARTAMENTO;
                            cDato.StrDescPadre = lst.ACTIVIDAD;
                            cDato.LngNit = 0;
                            cDato.StrLicencia = "";
                            cDato.StrRepLegal = "";
                            cDato.StrDireccion = "";
                            cDato.StrDepartamento = lst.DEPARTAMENTO;
                            cDato.IntAccion = 2;
                            lstResultado.Add(cDato);
                            idPadre = idUnico;
                            idAc = lst.DEPARTAMENTO;
                            idUnico++;
                        }
                        cDato = new CArbol();
                        cDato.IntId = idUnico;
                        cDato.IntIdPadre = idPadre;
                        cDato.IntIdEntidad = (int)lst.ID_ENTIDAD_PADRE;
                        cDato.IntIdOperador = (int)lst.ID_CONSUMIDOR;
                        cDato.IntIdActividad = (int)lst.ID_ACTIVIDAD;
                        cDato.StrDescripcion = lst.DENOMINACION;
                        cDato.StrDescPadre = lst.DENOMINACION;
                        cDato.LngNit = Convert.ToInt64(lst.NIT);
                        cDato.StrLicencia = lst.NRO_LICENCIA;
                        cDato.StrRepLegal = lst.REPRESENTANTE_LEGAL;
                        cDato.StrDireccion = lst.DIRECCION;
                        cDato.StrDepartamento = lst.DEPARTAMENTO;
                        cDato.IntAccion = 3;
                        lstResultado.Add(cDato);
                        idUnico++;
                    }
                    Session[CVariablesSesion.EstructuraArbol] = lstResultado;
                }
            }
        }
        private void MCargaGrilla(int intIdActividad, string strDepto, int intIdOperador, int intTipo)
        {
            try
            {
                var lstEntidad = new List<O_OBTIENE_ENTIDADES_USR_CTY>();
                lstEntidad = (List<O_OBTIENE_ENTIDADES_USR_CTY>)Session[CVariablesSesion.ObtenerEntidades];
                switch (intTipo)
                {
                    case 1:
                        lstEntidad = lstEntidad.Where(a => a.ID_ACTIVIDAD == intIdActividad).ToList();
                        break;
                    case 2:
                        lstEntidad = lstEntidad.Where(a => a.ID_ACTIVIDAD == intIdActividad && a.DEPARTAMENTO==strDepto).ToList();
                        break;
                    default:
                        lstEntidad = lstEntidad.Where(a => a.ID_ACTIVIDAD == intIdActividad && a.DEPARTAMENTO == strDepto && a.ID_CONSUMIDOR==intIdOperador).ToList();
                        break;
                }
                lstEntidad = lstEntidad.Where(a => a.ID_ACTIVIDAD == intIdActividad).ToList();
                lstEntidad = lstEntidad.OrderBy(a => a.DEPARTAMENTO).ThenBy(a => a.DENOMINACION).ToList();
                Session[CVariablesSesion.GrillaTransaccion] = lstEntidad;
                dgvEntidades.DataSource = lstEntidad;
                dgvEntidades.DataBind();
            }
            catch (Exception exception)
            {
                MImprimirMensaje(exception.Message + "  - MCargaGrilla", EnumTiposMensajeAlerta.Error);
            }
        }

        private bool MValidaDatosIniciales()
        {
            bool booResultado = true;

            //if (cmbTipoOperacion.Text == "")
            //{
            //    MImprimirMensaje("Introduzca el tipo de operación.", EnumTiposMensajeAlerta.Alerta);
            //    booResultado = false;
            //}
            //else if (detFechaIni.Text == "" || detFechaFin.Text == "")
            //{
            //    MImprimirMensaje("Introduzca fechas validas.", EnumTiposMensajeAlerta.Alerta);
            //    booResultado = false;
            //}
            //else if (detFechaFin.Date < detFechaIni.Date)
            //{
            //    MImprimirMensaje("Fechas incorrectas.", EnumTiposMensajeAlerta.Alerta);
            //    booResultado = false;
            //}
            //else if (detFechaIni.Date.Year + detFechaIni.Date.Month != detFechaFin.Date.Year + detFechaFin.Date.Month)
            //{
            //    MImprimirMensaje("Fechas fuera de intérvalo. Debe escoger fechas dentro del mismo periodo.", EnumTiposMensajeAlerta.Alerta);
            //    booResultado = false;
            //}
            return booResultado;
        }
        private string MValidaDatos(int intIdOperacion, int intIdActividad)
        {
            string strResultado = "";

            return strResultado;
        }
        private bool MValidaLlave()
        {
            bool booRespuesta = true;

            if ((int)tlsOperadores.FocusedNode.GetValue("IntIdActividad") == (int)EnumActividades.EstacionServicioLiquido ||
                (int)tlsOperadores.FocusedNode.GetValue("IntIdActividad") == (int)EnumActividades.EstacionServicioGnv)
            {
                //verifica con ws
                switch ((int)tlsOperadores.FocusedNode.GetValue("IntIdActividad"))
                {
                    case (int)EnumActividades.EstacionServicioLiquido:
                        break;
                    case (int)EnumActividades.EstacionServicioGnv:
                        if ((int)Session[CVariablesSesion.AuxValidaLlave] == 0)
                                booRespuesta = false;
                        break;
                }
            }

            return booRespuesta;
        }

        private void MConfiguraGrilla(int idActividad)
        {

        }
        private void MConfiguraFormulario(int intTipoOperacion, int intActividad)
        {
            
        }

        private void MLimpiaGrilla()
        {
            Session.Remove(CVariablesSesion.GrillaTransaccion);
            dgvEntidades.DataSource = null;
            dgvEntidades.DataBind();
            pnlMensaje.Visible = false;
            //dgvEntidades.Columns[(int)EnumColumnasGrilla.FoAbm].Visible = false;
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
            //if (tipoMensaje != EnumTiposMensajeAlerta.Correcto)
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError", "alert('" + strMensaje + "');", true);
            lblMensajeError.Text = "   " + strMensaje + "   ";

        }

        #endregion



        protected void ASPxButton1_Click1(object sender, EventArgs e)
        {

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError", "alert('aaahhh');", true);
            Page.ClientScript.RegisterStartupScript(GetType(), "JsFunc", "alert('Hello!')", true);
            //pucDatos.ShowOnPageLoad = true;


            ////mapa2.DragButton = MouseButtons.Left;
            ////GMap2.CanDragMap = true;
            ////GMap2.MapProvider = GMapProviders.GoogleMap;
            ////GMap2.Position = new PointLatLng(37.983, -1.133);
            ////GMap2.MinZoom = 0;
            ////GMap2.MaxZoom = 24;
            ////GMap2.Zoom = 9;
            ////GMap2.AutoScroll = true;

            //string nombre = "xxx";
            //double lat = -16.500;
            //double lon = -64.150;

            //GLatLng ubicacion = new GLatLng(lat, lon);

            //GMap2.setCenter(ubicacion, 12);
            ////GMap2.Height = 350;
            ////GMap2.Width = 350;
            ////GMap2.Add(new GControl(GControl.preBuilt.LargeMapControl));
            ////GMap2.Add(new GControl(GControl.preBuilt.MapTypeControl));

            //GMarker marker = new GMarker(ubicacion);
            ////GMarker marke1 = new GMarker(ubicacio1);

            ////string strMarker = "<h3>" + lblNomEESS.Text + "</h3>";
            //string strMarker = "<h3>" + nombre + "</h3>";
            ////string strMarke1 = "<h3>" + nombr1 + "</h3>";

            //GInfoWindow window = new GInfoWindow(marker, strMarker, true);
            //GMap2.Add(window);


            ////GMap2.reset();
        }

        protected void pucDatos_WindowCallback(object source, PopupWindowCallbackArgs e)
        {
            int index = dgvEntidades.FocusedRowIndex;
            txtPiId.Text = dgvEntidades.GetRowValues(index, "ID_CONSUMIDOR").ToString();
        }

        protected void pucModEstado_WindowCallback(object source, PopupWindowCallbackArgs e)
        {
            int index = dgvEntidades.FocusedRowIndex;
            txtPeId.Text = dgvEntidades.GetRowValues(index, "ID_CONSUMIDOR").ToString();
            detPeFecha.Date = DateTime.Now;
            cmbPeEstado.Value = 1;
        }

        protected void Panel2_PreRender(object sender, EventArgs e)
        {
            //string nombre = "xxx";
            //double lat = -16.500;
            //double lon = -64.150;
            //GLatLng ubicacion = new GLatLng(lat, lon);
            //GMap1.setCenter(ubicacion, 12);
            //GMarker marker = new GMarker(ubicacion);
            //string strMarker = "<h3>" + nombre + "</h3>";
            //GInfoWindow window = new GInfoWindow(marker, strMarker, true);
            //GMap1.Add(window);
            //GMap1.resetMarkers();
        }

        protected void pnlPrueba_Callback(object sender, CallbackEventArgsBase e)
        {
            //GMap1.resetScreenOverlays();

            //string nombre = "xxx";
            //double lat = -16.500;
            //double lon = -64.150;
            //GLatLng ubicacion = new GLatLng(lat, lon);
            //GMap1.setCenter(ubicacion, 12);
            //GMarker marker = new GMarker(ubicacion);
            //string strMarker = "<h3>" + nombre + "</h3>";
            //GInfoWindow window = new GInfoWindow(marker, strMarker, true);
            //GMap1.Add(window);
            //GMap1.resetMarkers();
            
        }


        [WebMethod] 
        public static List<Contacto> ObtenerContactos()
        {
            return new List<Contacto> 
            { 
                new Contacto { IdContacto=14, Nombre="Pedro", Telefono="601 01 01 01",
                               Email = "pedrog@gmail.com" }, 
                new Contacto { IdContacto=15, Nombre="Maria", Telefono="601 01 01 02",
                               Email = "maria69@gmail.com"  }, 
                new Contacto { IdContacto=16, Nombre="Juana", Telefono="601 01 01 03",
                               Email = "juana33@gmail.com"  } 
            };
        }

    }
    public class Contacto
    {
        public int IdContacto { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
    } 
}