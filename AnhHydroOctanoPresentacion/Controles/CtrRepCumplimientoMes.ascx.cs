
using AnhHydroOctanoPresentacion.Lib;

namespace AnhHydroOctanoPresentacion.Controles
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Reflection;
    using System.Web;
    using System.Web.UI;
    using AnhHydroOctanoPersistencia.Entidades;
    using Clases.Controles;
    using Entidades;
    using Enumeradores;
    using Parametros;
    using Sitio.Usuarios;
    using DevExpress.Utils;
    using DevExpress.Web;
    using Librerias.Anh.Us;
    using ServiceStack.ServiceClient.Web;
    using ServiceStack.Text;
    using CArbol = Clases.Controles.CArbol;
    using EResultado = Entidades.EResultado;
    using EResultadoEntidad = Entidades.EResultadoEntidad;
    using Page = System.Web.UI.Page;
    using Clases.Util;
    public partial class CtrRepCumplimientoMes : UserControl
    {
        #region Variables de entorno

        readonly JsonServiceClient _client = new JsonServiceClient(CAppSettings.servicio_octVolumen);
        readonly JsonServiceClient _clientAdminHydro = new JsonServiceClient(CAppSettings.ServicioAdministradorHydro);
        private string _strMensajeError;
        private decimal decIngRecepcion, decIngDemasia, decIngreso, decSalVenta, decSalMerma, decSalida;
        private decimal decMontoSum;
        CConsultasParametricas cConsultasParametricas = new CConsultasParametricas();
        readonly CTrazaInf _cTrazaInf = new CTrazaInf();
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
                        Session.Remove(CVariablesSesion.GrillaCabecera);

                        MCargaParametricas();
                        MCargaArbol();
                        MIniciaFechas();
                    }

                    tlsOperadores.DataSource = Session[CVariablesSesion.EstructuraArbol];
                    tlsOperadores.DataBind();

                    if (Session[CVariablesSesion.GrillaCabecera] != null)
                    {
                        dgvReporte.DataSource = Session[CVariablesSesion.GrillaCabecera];
                        dgvReporte.DataBind();
                    }

                    if (Session[CVariablesSesion.GrillaAmpliacion] != null)
                    {
                        dgvAmpliacion.DataSource = Session[CVariablesSesion.GrillaAmpliacion];
                        dgvAmpliacion.DataBind();
                    }
                    else
                    {
                        //todo No es carga inicial: no implementado
                    }

                    if (Session[CVariablesSesion.GrillaIntimacion] != null)
                    {
                        dgvIntimacion.DataSource = Session[CVariablesSesion.GrillaIntimacion];
                        dgvIntimacion.DataBind();
                    }
                    else
                    {
                        //todo No es carga inicial: no implementado
                    }

                    if (Session[CVariablesSesion.GrillaHistorial] != null)
                    {
                        dgvHistorial.DataSource = Session[CVariablesSesion.GrillaHistorial];
                        dgvHistorial.DataBind();
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

        protected void pnlDatos_Callback(object sender, CallbackEventArgsBase e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Codigo a controlar

                    lblActividad.Text = tlsOperadores.FocusedNode.GetValue("StrDescPadre").ToString();
                    MLimpiaGrilla();
                    if ((int)tlsOperadores.FocusedNode.GetValue("IntAccion") > 0)
                    {
                        if (MValidaDatosIniciales())
                        {
                            decimal decIdActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad"));
                            MCargaGrilla(decIdActividad);
                        }
                    }

                    #endregion
                }
                catch (Exception exp)
                {
                    #region RegistraError
                    MLimpiaGrilla();
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

        protected void dgvReporte_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            try
            {
                #region Codigo a controlar

                if (e.VisibleIndex >= 0)
                {
                    if (e.VisibleIndex % 2 == 1)
                    {
                        e.Row.BackColor = Color.FromArgb(226, 233, 198);
                    }
                    else
                    {
                        //todo No implementado.
                    }

                    int valor = Convert.ToInt32(dgvReporte.GetRowValues(e.VisibleIndex, "ESTADO_MENSUAL"));
                    switch (valor)
                    {
                        case 2: //EN PLAZO
                            e.Row.ForeColor = Color.FromArgb(54, 109, 54);
                            break;
                        case 3: //PLAZO VENCIDO
                            e.Row.ForeColor = Color.FromArgb(236, 0, 0);
                            break;
                        case 4: //AMPLIACION
                            e.Row.ForeColor = Color.FromArgb(25, 46, 105);
                            break;
                        case 5: //INTIMACION
                            e.Row.ForeColor = Color.FromArgb(124, 7, 0);
                            break;
                        case 6: //NO HABILITADO
                            e.Row.ForeColor = Color.FromArgb(142, 140, 1);
                            break;
                        default: //DECLARADO
                            e.Row.ForeColor = Color.FromArgb(0, 156, 0);
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

        protected void dgvReporte_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
        {
            var objPerfil = Session[CVariablesSesion.ListaPerfiles] as List<O_USUARIO_PERFIL_CTY>;
            if (objPerfil != null)
            {
                objPerfil = objPerfil.Where(x => x.NOMBRE_PERFIL.Contains("ANH")).ToList();
                if (!objPerfil.Any())
                {
                    if (e.ButtonID == "btnAmpliar")
                        e.Visible = DefaultBoolean.False;
                    if (e.ButtonID == "btnIntimar")
                        e.Visible = DefaultBoolean.False;
                }
            }
            else
            {
                if (e.ButtonID == "btnAmpliar")
                    e.Visible = DefaultBoolean.False;
                if (e.ButtonID == "btnIntimar")
                    e.Visible = DefaultBoolean.False;
            }
        }

        protected void dgvAcumulado_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    dgvAcumulado.Columns[2].Visible = false;
                    for (int i = 6; i <= 25; i++)
                    {
                        dgvAcumulado.Columns[i].Visible = false;
                        dgvAcumulado.Columns[21 + i].Visible = false;
                    }

                    Session.Remove(CVariablesSesion.GrillaAcumulado);
                    dgvAcumulado.DataSource = null;
                    dgvAcumulado.DataBind();

                    int index = dgvReporte.FocusedRowIndex;
                    if (index >= 0)
                    {
                        int intEstado = Convert.ToInt32(dgvReporte.GetRowValues(index, "ESTADO_MENSUAL").ToString());
                        if (intEstado == 1)
                        {
                            string strJsonReportes = dgvReporte.GetRowValues(index, "JSON_REPORTE_DATOS").ToString();
                            if (!string.IsNullOrEmpty(strJsonReportes))
                            {
                                var objDdjj = JsonSerializer.DeserializeFromString<O_DECLARACION_JURADA_CTY>(strJsonReportes);
                                var lstAcumulado = objDdjj.LstAcumulado;
                                if (lstAcumulado.Any())
                                {
                                    #region acumulado
                                    decimal decProd = 0;
                                    int cont = 1, intContProd = 1;

                                    lstAcumulado.ForEach(a => a.DecIdUnicoHidrocarburo = Convert.ToDecimal(a.DecIdHidrocarburo.ToString() + ((a.DecIdTipoInfraestructura == null) ? "" : a.DecIdTipoInfraestructura.ToString().Replace("-", "")) + ((a.DecIdInfraestructura == null) ? "" : a.DecIdInfraestructura.ToString().Replace("-", ""))));
                                    lstAcumulado = lstAcumulado.OrderBy(x => x.DecIdHidrocarburo).ThenBy(x => x.StrTipoInfraestructura).ThenBy(x => x.StrInfraestructura).ThenBy(x => x.DecFecha).ToList();

                                    //lstAcumulado = lstAcumulado.OrderBy(x => x.DecIdHidrocarburo).ThenBy(x => x.DecFecha).ToList();
                                    var lstMovimientos = new List<CMovAcumulado>();

                                    decimal decTotIng = 0, decTotSal = 0;
                                    decimal[] decIng = new decimal[20];
                                    decimal[] decSal = new decimal[20];

                                    var cDato = new CMovAcumulado();

                                    foreach (var lst in lstAcumulado)
                                    {

                                        if (decProd != lst.DecIdUnicoHidrocarburo)
                                        {
                                            if (decProd > 0)
                                            {
                                                cDato.DecIng01 = decIng[0];
                                                cDato.DecIng02 = decIng[1];
                                                cDato.DecIng03 = decIng[2];
                                                cDato.DecIng04 = decIng[3];
                                                cDato.DecIng05 = decIng[4];
                                                cDato.DecIng06 = decIng[5];
                                                cDato.DecIng07 = decIng[6];
                                                cDato.DecIng08 = decIng[7];
                                                cDato.DecIng09 = decIng[8];
                                                cDato.DecIng10 = decIng[9];
                                                cDato.DecIng11 = decIng[10];
                                                cDato.DecIng12 = decIng[11];
                                                cDato.DecIng13 = decIng[12];
                                                cDato.DecIng14 = decIng[13];
                                                cDato.DecIng15 = decIng[14];
                                                cDato.DecIng16 = decIng[15];
                                                cDato.DecIng17 = decIng[16];
                                                cDato.DecIng18 = decIng[17];
                                                cDato.DecIng19 = decIng[18];
                                                cDato.DecIng20 = decIng[19];

                                                cDato.DecSal01 = decSal[0];
                                                cDato.DecSal02 = decSal[1];
                                                cDato.DecSal03 = decSal[2];
                                                cDato.DecSal04 = decSal[3];
                                                cDato.DecSal05 = decSal[4];
                                                cDato.DecSal06 = decSal[5];
                                                cDato.DecSal07 = decSal[6];
                                                cDato.DecSal08 = decSal[7];
                                                cDato.DecSal09 = decSal[8];
                                                cDato.DecSal10 = decSal[9];
                                                cDato.DecSal11 = decSal[10];
                                                cDato.DecSal12 = decSal[11];
                                                cDato.DecSal13 = decSal[12];
                                                cDato.DecSal14 = decSal[13];
                                                cDato.DecSal15 = decSal[14];
                                                cDato.DecSal16 = decSal[15];
                                                cDato.DecSal17 = decSal[16];
                                                cDato.DecSal18 = decSal[17];
                                                cDato.DecSal19 = decSal[18];
                                                cDato.DecSal20 = decSal[19];

                                                cDato.DecTotalIng = decTotIng;
                                                cDato.DecTotalSal = decTotSal;

                                                lstMovimientos.Add(cDato);
                                            }

                                            decProd = lst.DecIdUnicoHidrocarburo;

                                            cDato = new CMovAcumulado();
                                            cDato.IntId = intContProd;
                                            cDato.IntIdProducto = (int)lst.DecIdHidrocarburo;
                                            cDato.StrProducto = lst.StrHidrocarburo;
                                            cDato.StrCodProducto = lst.StrCodHidrocarburo;
                                            cDato.StrUnidadMedida = lst.StrUnidadMedida;
                                            cDato.StrTipoInfraestructura = lst.StrTipoInfraestructura;
                                            cDato.StrInfraestructura = lst.StrInfraestructura;
                                            cDato.DecSaldoInicial = lst.DecSaldoInicial;

                                            intContProd++;
                                            decTotIng = 0;
                                            decTotSal = 0;

                                            for (int i = 0; i < 20; i++)
                                            {
                                                decIng[i] = 0;
                                                decSal[i] = 0;
                                            }
                                        }

                                        cDato.DecSaldoFinal = lst.DecSaldoFinal;
                                        decTotIng += lst.DecTotalIngreso;
                                        decTotSal += lst.DecTotalEgreso;

                                        if (lst.LstIngresos == null || !lst.LstIngresos.Any())
                                        {
                                            cDato.DecIng01 = -99999;
                                        }
                                        else
                                        {
                                            foreach (var reg in lst.LstIngresos)
                                            {
                                                if (!dgvAcumulado.Columns[5 + (int)reg.OPERACION_ORDEN].Visible)
                                                {
                                                    dgvAcumulado.Columns[5 + (int)reg.OPERACION_ORDEN].Visible = true;
                                                    dgvAcumulado.Columns[5 + (int)reg.OPERACION_ORDEN].Caption = reg.OPERACION_NOMBRE;
                                                }

                                                decIng[0] += (reg.OPERACION_ORDEN == 1) ? reg.CANTIDAD : 0;
                                                decIng[1] += (reg.OPERACION_ORDEN == 2) ? reg.CANTIDAD : 0;
                                                decIng[2] += (reg.OPERACION_ORDEN == 3) ? reg.CANTIDAD : 0;
                                                decIng[3] += (reg.OPERACION_ORDEN == 4) ? reg.CANTIDAD : 0;
                                                decIng[4] += (reg.OPERACION_ORDEN == 5) ? reg.CANTIDAD : 0;
                                                decIng[5] += (reg.OPERACION_ORDEN == 6) ? reg.CANTIDAD : 0;
                                                decIng[6] += (reg.OPERACION_ORDEN == 7) ? reg.CANTIDAD : 0;
                                                decIng[7] += (reg.OPERACION_ORDEN == 8) ? reg.CANTIDAD : 0;
                                                decIng[8] += (reg.OPERACION_ORDEN == 9) ? reg.CANTIDAD : 0;
                                                decIng[9] += (reg.OPERACION_ORDEN == 10) ? reg.CANTIDAD : 0;
                                                decIng[10] += (reg.OPERACION_ORDEN == 11) ? reg.CANTIDAD : 0;
                                                decIng[11] += (reg.OPERACION_ORDEN == 12) ? reg.CANTIDAD : 0;
                                                decIng[12] += (reg.OPERACION_ORDEN == 13) ? reg.CANTIDAD : 0;
                                                decIng[13] += (reg.OPERACION_ORDEN == 14) ? reg.CANTIDAD : 0;
                                                decIng[14] += (reg.OPERACION_ORDEN == 15) ? reg.CANTIDAD : 0;
                                                decIng[15] += (reg.OPERACION_ORDEN == 16) ? reg.CANTIDAD : 0;
                                                decIng[16] += (reg.OPERACION_ORDEN == 17) ? reg.CANTIDAD : 0;
                                                decIng[17] += (reg.OPERACION_ORDEN == 18) ? reg.CANTIDAD : 0;
                                                decIng[18] += (reg.OPERACION_ORDEN == 19) ? reg.CANTIDAD : 0;
                                                decIng[19] += (reg.OPERACION_ORDEN == 20) ? reg.CANTIDAD : 0;
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
                                                if (!dgvAcumulado.Columns[26 + (int)reg.OPERACION_ORDEN].Visible)
                                                {
                                                    dgvAcumulado.Columns[26 + (int)reg.OPERACION_ORDEN].Visible = true;
                                                    dgvAcumulado.Columns[26 + (int)reg.OPERACION_ORDEN].Caption = reg.OPERACION_NOMBRE;
                                                }

                                                decSal[0] += (reg.OPERACION_ORDEN == 1) ? reg.CANTIDAD : 0;
                                                decSal[1] += (reg.OPERACION_ORDEN == 2) ? reg.CANTIDAD : 0;
                                                decSal[2] += (reg.OPERACION_ORDEN == 3) ? reg.CANTIDAD : 0;
                                                decSal[3] += (reg.OPERACION_ORDEN == 4) ? reg.CANTIDAD : 0;
                                                decSal[4] += (reg.OPERACION_ORDEN == 5) ? reg.CANTIDAD : 0;
                                                decSal[5] += (reg.OPERACION_ORDEN == 6) ? reg.CANTIDAD : 0;
                                                decSal[6] += (reg.OPERACION_ORDEN == 7) ? reg.CANTIDAD : 0;
                                                decSal[7] += (reg.OPERACION_ORDEN == 8) ? reg.CANTIDAD : 0;
                                                decSal[8] += (reg.OPERACION_ORDEN == 9) ? reg.CANTIDAD : 0;
                                                decSal[9] += (reg.OPERACION_ORDEN == 10) ? reg.CANTIDAD : 0;
                                                decSal[10] += (reg.OPERACION_ORDEN == 11) ? reg.CANTIDAD : 0;
                                                decSal[11] += (reg.OPERACION_ORDEN == 12) ? reg.CANTIDAD : 0;
                                                decSal[12] += (reg.OPERACION_ORDEN == 13) ? reg.CANTIDAD : 0;
                                                decSal[13] += (reg.OPERACION_ORDEN == 14) ? reg.CANTIDAD : 0;
                                                decSal[14] += (reg.OPERACION_ORDEN == 15) ? reg.CANTIDAD : 0;
                                                decSal[15] += (reg.OPERACION_ORDEN == 16) ? reg.CANTIDAD : 0;
                                                decSal[16] += (reg.OPERACION_ORDEN == 17) ? reg.CANTIDAD : 0;
                                                decSal[17] += (reg.OPERACION_ORDEN == 18) ? reg.CANTIDAD : 0;
                                                decSal[18] += (reg.OPERACION_ORDEN == 19) ? reg.CANTIDAD : 0;
                                                decSal[19] += (reg.OPERACION_ORDEN == 20) ? reg.CANTIDAD : 0;
                                            }
                                        }

                                        if (lstAcumulado.Count == cont)
                                        {
                                            cDato.DecIng01 = decIng[0];
                                            cDato.DecIng02 = decIng[1];
                                            cDato.DecIng03 = decIng[2];
                                            cDato.DecIng04 = decIng[3];
                                            cDato.DecIng05 = decIng[4];
                                            cDato.DecIng06 = decIng[5];
                                            cDato.DecIng07 = decIng[6];
                                            cDato.DecIng08 = decIng[7];
                                            cDato.DecIng09 = decIng[8];
                                            cDato.DecIng10 = decIng[9];
                                            cDato.DecIng11 = decIng[10];
                                            cDato.DecIng12 = decIng[11];
                                            cDato.DecIng13 = decIng[12];
                                            cDato.DecIng14 = decIng[13];
                                            cDato.DecIng15 = decIng[14];
                                            cDato.DecIng16 = decIng[15];
                                            cDato.DecIng17 = decIng[16];
                                            cDato.DecIng18 = decIng[17];
                                            cDato.DecIng19 = decIng[18];
                                            cDato.DecIng20 = decIng[19];

                                            cDato.DecSal01 = decSal[0];
                                            cDato.DecSal02 = decSal[1];
                                            cDato.DecSal03 = decSal[2];
                                            cDato.DecSal04 = decSal[3];
                                            cDato.DecSal05 = decSal[4];
                                            cDato.DecSal06 = decSal[5];
                                            cDato.DecSal07 = decSal[6];
                                            cDato.DecSal08 = decSal[7];
                                            cDato.DecSal09 = decSal[8];
                                            cDato.DecSal10 = decSal[9];
                                            cDato.DecSal11 = decSal[10];
                                            cDato.DecSal12 = decSal[11];
                                            cDato.DecSal13 = decSal[12];
                                            cDato.DecSal14 = decSal[13];
                                            cDato.DecSal15 = decSal[14];
                                            cDato.DecSal16 = decSal[15];
                                            cDato.DecSal17 = decSal[16];
                                            cDato.DecSal18 = decSal[17];
                                            cDato.DecSal19 = decSal[18];
                                            cDato.DecSal20 = decSal[19];

                                            cDato.DecTotalIng = decTotIng;
                                            cDato.DecTotalSal = decTotSal;

                                            lstMovimientos.Add(cDato);
                                        }

                                        cont++;

                                    }

                                    Session[CVariablesSesion.GrillaAcumulado] = lstMovimientos;
                                    dgvAcumulado.DataSource = lstMovimientos;
                                    dgvAcumulado.DataBind();

                                    #endregion
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
                        //todo No implementado
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

        protected void dgvAcumulado_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
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

        protected void dgvInternacional_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    Session.Remove(CVariablesSesion.GrillaInternacional);
                    dgvInternacional.DataSource = null;
                    dgvInternacional.DataBind();

                    int index = dgvReporte.FocusedRowIndex;
                    if (index >= 0)
                    {
                        int intEstado = Convert.ToInt32(dgvReporte.GetRowValues(index, "ESTADO_MENSUAL").ToString());
                        if (intEstado == 1)
                        {
                            string strJsonReportes = dgvReporte.GetRowValues(index, "JSON_REPORTE_DATOS").ToString();
                            if (!string.IsNullOrEmpty(strJsonReportes))
                            {
                                var objDdjj = JsonSerializer.DeserializeFromString<O_DECLARACION_JURADA_CTY>(strJsonReportes);

                                var lstAdic2 = objDdjj.LstaAdicional2;
                                if (lstAdic2 != null && lstAdic2.Any())
                                {
                                    #region facturas internacionales
                                    lstAdic2 = lstAdic2.OrderBy(x => x.idMovimiento).ThenBy(x => x.idTipoEmisor).ToList();
                                    CDjInternacional cDato;
                                    long lngIdMovimiento = 0;
                                    var lstRep = new List<CDjInternacional>();
                                    cDato = null;
                                    long lngCont = 1;

                                    foreach (var registro in lstAdic2)
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
                                                if (!registro.EstadoTransaccion.Contains("ANU"))
                                                {
                                                    cDato.DecImporteProp = registro.precioTotal;
                                                    cDato.DecCantidadProp = registro.cantidadHidrocarburo;
                                                }
                                                break;
                                            case (int)EnumTipoEmisor.ExternaYpfb:
                                                cDato.LngNoAutExt = (long)registro.numeroAutorizacion;
                                                cDato.LngNoFactExt = Convert.ToInt64(registro.numeroTransaccion);
                                                cDato.StrCodCtrlExt = registro.codigoControl;
                                                cDato.DecPrecioExt += registro.precioUnitario;
                                                if (!registro.EstadoTransaccion.Contains("ANU"))
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
            else
            {
                //todo No implementado
            }
        }
        
        protected void dgvInternacional_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
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

        protected void dgvAmpliacion_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            try
            {
                #region Proceso a Ejecutar

                if (e.VisibleIndex >= 0)
                {
                    e.Row.BackColor = (Convert.ToInt16(dgvAmpliacion.GetRowValues(e.VisibleIndex, "ESTADO").ToString()) == 2) ? Color.FromArgb(240, 152, 152) : (Convert.ToInt16(dgvAmpliacion.GetRowValues(e.VisibleIndex, "ESTADO").ToString()) == 3) ? Color.FromArgb(235, 198, 155) : Color.FromArgb(229, 253, 212);
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

        protected void pucAmpliacion_WindowCallback(object source, PopupWindowCallbackArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Codigo a controlar

                    int index = dgvReporte.FocusedRowIndex;
                    if (index >= 0)
                    {
                        var decEstado = Convert.ToDecimal(txtPamIdConsumidor.Text = dgvReporte.GetRowValues(index, "ESTADO_MENSUAL").ToString());

                        txtPamActividad.Text = tlsOperadores.FocusedNode.GetValue("StrDescPadre").ToString();
                        txtPamDepto.Text = dgvReporte.GetRowValues(index, "DEPARTAMENTO").ToString();
                        txtPamIdConsumidor.Text = dgvReporte.GetRowValues(index, "ID_CONSUMIDOR").ToString();
                        txtPamConsumidor.Text = dgvReporte.GetRowValues(index, "DENOMINACION").ToString();

                        dgvAmpliacion_CustomCallback(null, null);

                        if (decEstado == 1)
                        {
                            pnlRegistraAmp.Visible = false;
                            dgvAmpliacion.Columns[0].Visible = false;
                        }
                        else
                        {
                            
                            cmbPamMes.Value = cmbMes.Value;
                            cmbPamGestion.Value = cmbGestion.Value;
                            detPamFechaIni.Value = DateTime.Now;
                            detPamFechaFin.Value = DateTime.Now.AddDays(2);
                            dgvAmpliacion.Columns[0].Visible = true;
                        }
                    }
                    else
                    {
                        //todo No implementado
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

        protected void dgvAmpliacion_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Codigo a controlar

                    dgvAmpliacion.DataSource = null;
                    dgvAmpliacion.DataBind();
                    Session.Remove(CVariablesSesion.GrillaAmpliacion);

                    int index = dgvReporte.FocusedRowIndex;
                    if (index >= 0)
                    {
                        var objAmpliacion = _client.Post<EResultadoAutCons>(CAppSettings.svc_listar_autorizacion_consumidor,
                        new
                        {
                            strCredencial = CParametrosHydro.strCredencialFuncionario,
                            decFiltro = 1,
                            strParametro1 = txtPamIdConsumidor.Text,
                            strParametro2 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
                            strParametro3 = cmbGestion.Value + cmbMes.Value.ToString().PadLeft(2, '0') + "01000000",
                            strParametro4 = cmbGestion.Value + cmbMes.Value.ToString().PadLeft(2, '0') + DateTime.DaysInMonth((int)cmbGestion.Value, (int)cmbMes.Value) + "235959",
                            strParametro5 = "1"
                        });
                        if (objAmpliacion.IntCodigo == 1)
                        {
                            objAmpliacion.OResultado.ForEach(a => a.strEstado = (a.ESTADO == 1) ? "ACT." : (a.ESTADO == 2) ? "ANUL." : "VENC.");
                            objAmpliacion.OResultado.ForEach(a => a.strPeriodo = a.FECHA_INICIO.ToString().Substring(4, 2) + "/" + a.FECHA_INICIO.ToString().Substring(0, 4));
                            objAmpliacion.OResultado.ForEach(a => a.strFechaRegistro = a.APP_FECHA_REGISTRO.ToString().Substring(6, 2) + "/" + a.APP_FECHA_REGISTRO.ToString().Substring(4, 2) + "/" + a.APP_FECHA_REGISTRO.ToString().Substring(0, 4) + " " + a.APP_FECHA_REGISTRO.ToString().Substring(8, 2) + ":" + a.APP_FECHA_REGISTRO.ToString().Substring(10, 2) + ":" + a.APP_FECHA_REGISTRO.ToString().Substring(12, 2));
                            objAmpliacion.OResultado.ForEach(a => a.strFechaActIni = a.FECHA_INICIO_ACT.ToString().Substring(6, 2) + "/" + a.FECHA_INICIO_ACT.ToString().Substring(4, 2) + "/" + a.FECHA_INICIO_ACT.ToString().Substring(0, 4));
                            objAmpliacion.OResultado.ForEach(a => a.strFechaActFin = a.FECHA_FIN_ACT.ToString().Substring(6, 2) + "/" + a.FECHA_FIN_ACT.ToString().Substring(4, 2) + "/" + a.FECHA_FIN_ACT.ToString().Substring(0, 4));
                            objAmpliacion.OResultado = objAmpliacion.OResultado.OrderBy(x => x.APP_FECHA_REGISTRO).ToList();
                            dgvAmpliacion.DataSource = objAmpliacion.OResultado;
                            dgvAmpliacion.DataBind();
                            Session[CVariablesSesion.GrillaAmpliacion] = objAmpliacion.OResultado;
                        }
                        else if (objAmpliacion.IntCodigo != -1)
                        {
                            pnlRegistraAmp.Visible = false;
                            MImprimirMensaje(objAmpliacion.StrMensaje, EnumTiposMensajeAlerta.Error);
                        }
                        else
                        {
                            //todo No implementado
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
            else
            {
                //todo No implementado
            }
        }

        protected void dgvAmpliacion_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    int index = dgvAmpliacion.FocusedRowIndex;
                    long lngidAutCons = Convert.ToInt64(dgvAmpliacion.GetRowValues(index, "ID_AUTORIZACION_CONSUMIDOR"));
                    if (lngidAutCons > 0)
                    {
                        if (e.ButtonID == "btnAnular" && Convert.ToInt64(dgvAmpliacion.GetRowValues(index, "ESTADO")) == 1)
                        {
                            var datFechaActual = DateTime.Now;
                            var resultado = _client.Post<EResultado>(CAppSettings.svc_actualiza_estado_aut_con,
                            new
                            {
                                strCredencial = CParametrosHydro.strCredencialFuncionario,
                                decIdAutorizacionConsumidor = lngidAutCons,
                                decEstado = 2,
                                strObservaciones = "",
                                decAppFechaRegistro = Convert.ToDecimal(datFechaActual.Year + datFechaActual.Month.ToString().PadLeft(2, '0') + datFechaActual.Day.ToString().PadLeft(2, '0') + datFechaActual.Hour.ToString().PadLeft(2, '0') + datFechaActual.Minute.ToString().PadLeft(2, '0') + datFechaActual.Second.ToString().PadLeft(2, '0')),
                                decAppIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario])
                            });
                            if (resultado.intCodigo == 1)
                            {
                                dgvAmpliacion_CustomCallback(null, null);
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

        protected void btnPamAgregar_Click(object sender, EventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Codigo a controlar

                    string strMensaje = string.Empty;
                    if (MValidaAmpliacion(ref strMensaje))
                    {
                        var datFechaActual = DateTime.Now;
                        var objResultado = _client.Post<EResultado>(CAppSettings.svc_registra_autorizacion_cons,
                        new
                        {
                            strCredencial = CParametrosHydro.strCredencialFuncionario,
                            decFechaInicio = Convert.ToDecimal(cmbPamGestion.Value + cmbPamMes.Value.ToString().PadLeft(2, '0') + "01000000"),
                            decFechaFin = Convert.ToDecimal(datFechaActual.Date.Year + datFechaActual.Date.Month.ToString().PadLeft(2, '0') + DateTime.DaysInMonth(datFechaActual.Date.Year, datFechaActual.Date.Month) + "235959"),
                            decEstado = 1,
                            strObservaciones = txtPamObservacion.Text,
                            strBinarioEstado = "00000000000000000000",
                            decIdConsumidor = txtPamIdConsumidor.Text,
                            decTipoAutorizacion = 1,
                            decIdTipoActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()),
                            decFechaInicioAct = Convert.ToDecimal(detPamFechaIni.Date.Year + detPamFechaIni.Date.Month.ToString().PadLeft(2, '0') + detPamFechaIni.Date.Day.ToString().PadLeft(2, '0') + "000000"),
                            decFechaFinAct = Convert.ToDecimal(detPamFechaFin.Date.Year + detPamFechaFin.Date.Month.ToString().PadLeft(2, '0') + detPamFechaFin.Date.Day.ToString().PadLeft(2, '0') + "235959"),
                            decAppUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]),
                            decAppFechaRegistro = Convert.ToDecimal(datFechaActual.Year + datFechaActual.Month.ToString().PadLeft(2, '0') + datFechaActual.Day.ToString().PadLeft(2, '0') + datFechaActual.Hour.ToString().PadLeft(2, '0') + datFechaActual.Minute.ToString().PadLeft(2, '0') + datFechaActual.Second.ToString().PadLeft(2, '0'))
                        });
                        if (objResultado.intCodigo == 1)
                        {
                            var repAmp = new repAmpliacion();
                            //repAmp.xtcConsumidor.Text = txtPinConsumidor.Text;
                            repAmp.xtcFechaRegistro.Text = datFechaActual.ToLongDateString() + " " + datFechaActual.ToLongTimeString();
                            repAmp.xtcUsuario.Text = Session[CVariablesSesion.DatosUsuario].ToString();
                            repAmp.xtcApertutaMes.Text = cmbPamMes.Text;
                            repAmp.xtcApertutaGestion.Text = cmbPamGestion.Text;
                            repAmp.xtcHabilitaDesde.Text = detPamFechaIni.Date.ToShortDateString();
                            repAmp.xtcHabilitaHasta.Text = detPamFechaFin.Date.ToShortDateString();
                            repAmp.xtcObservaciones.Text = txtPamObservacion.Text;

                            Session[CVariablesSesion.TipoReporte] = EnumTipoReporte.Ampliacion;
                            Session[CVariablesSesion.ReportePrincipal] = repAmp;
                            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( 'WFReporte.aspx', '');", true);


                            pucAmpliacion.ShowOnPageLoad = false;
                            pnlDatos_Callback(null, null);
                        }
                        else
                        {
                            MImprimirMensaje(objResultado.strMensaje, EnumTiposMensajeAlerta.Error);
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
                //todo No implementado
            }
        }

        protected void pucIntimacion_WindowCallback(object source, PopupWindowCallbackArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Codigo a controlar

                    int index = dgvReporte.FocusedRowIndex;
                    if (index >= 0)
                    {
                        var decEstado = Convert.ToDecimal(txtPamIdConsumidor.Text = dgvReporte.GetRowValues(index, "ESTADO_MENSUAL").ToString());

                        txtPinActividad.Text = tlsOperadores.FocusedNode.GetValue("StrDescPadre").ToString();
                        txtPinDepto.Text = dgvReporte.GetRowValues(index, "DEPARTAMENTO").ToString();
                        txtPinIdConsumidor.Text = dgvReporte.GetRowValues(index, "ID_CONSUMIDOR").ToString();
                        txtPinConsumidor.Text = dgvReporte.GetRowValues(index, "DENOMINACION").ToString();

                        dgvIntimacion_CustomCallback(null, null);

                        if (decEstado != 3)
                        {
                            pnlIntimacion.Visible = false;
                        }
                        else
                        {
                            txtPinPeriodo.Text = cmbMes.Text + "/" + cmbGestion.Text;
                            txtPinDescripcion.Text = "Incumplimiento a la obligación mensual - Registro de Declaración Jurada";
                            detPinFechaNot.Date = DateTime.Now;
                            txtPinCb.Text = "";
                            txtPinNotificante.Text = "";
                            txtPinNotificado.Text = "";
                            detPinFechaInicio.Date = DateTime.Now.Date;
                            spnPinDiasPlazo.Number = 2;
                            txtPinObservacion.Text = "";
                        }
                    }
                    else
                    {
                        //todo No implementado
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
                //todo No implementado
            }
        }

        protected void dgvIntimacion_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Codigo a controlar

                    dgvIntimacion.DataSource = null;
                    dgvIntimacion.DataBind();
                    Session.Remove(CVariablesSesion.GrillaIntimacion);

                    var objIntimacion = _client.Post<EResultadoAutCons>(CAppSettings.svc_listar_autorizacion_consumidor,
                    new
                    {
                        strCredencial = CParametrosHydro.strCredencialFuncionario,
                        decFiltro = 1,
                        strParametro1 = txtPinIdConsumidor.Text,
                        strParametro2 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
                        strParametro3 = cmbGestion.Value + cmbMes.Value.ToString().PadLeft(2, '0') + "01000000",
                        strParametro4 = cmbGestion.Value + cmbMes.Value.ToString().PadLeft(2, '0') + DateTime.DaysInMonth((int)cmbGestion.Value, (int)cmbMes.Value) + "235959",
                        strParametro5 = "2"
                    });
                    if (objIntimacion.IntCodigo == 1)
                    {
                        objIntimacion.OResultado.ForEach(a => a.strFechaRegistro = a.APP_FECHA_REGISTRO.ToString().Substring(6, 2) + "/" + a.APP_FECHA_REGISTRO.ToString().Substring(4, 2) + "/" + a.APP_FECHA_REGISTRO.ToString().Substring(0, 4) + " " + a.APP_FECHA_REGISTRO.ToString().Substring(8, 2) + ":" + a.APP_FECHA_REGISTRO.ToString().Substring(10, 2) + ":" + a.APP_FECHA_REGISTRO.ToString().Substring(12, 2));
                        objIntimacion.OResultado.ForEach(a => a.strFechaActIni = a.FECHA_INICIO_ACT.ToString().Substring(6, 2) + "/" + a.FECHA_INICIO_ACT.ToString().Substring(4, 2) + "/" + a.FECHA_INICIO_ACT.ToString().Substring(0, 4) + " " + a.FECHA_INICIO_ACT.ToString().Substring(8, 2) + ":" + a.FECHA_INICIO_ACT.ToString().Substring(10, 2) + ":" + a.FECHA_INICIO_ACT.ToString().Substring(12, 2));
                        objIntimacion.OResultado = objIntimacion.OResultado.OrderBy(x => x.APP_FECHA_REGISTRO).ToList();

                        dgvIntimacion.DataSource = objIntimacion.OResultado;
                        dgvIntimacion.DataBind();
                        Session[CVariablesSesion.GrillaIntimacion] = objIntimacion.OResultado;
                    }
                    else if (objIntimacion.IntCodigo != -1)
                    {
                        pnlIntimacion.Visible = false;
                        MImprimirMensaje(objIntimacion.StrMensaje, EnumTiposMensajeAlerta.Error);
                    }
                    else
                    {
                        //todo No implementado
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
                //todo No implementado
            }
        }

        protected void dgvIntimacion_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    var index = dgvIntimacion.FocusedRowIndex;
                    var lngidAutCons = Convert.ToInt64(dgvIntimacion.GetRowValues(index, "ID_AUTORIZACION_CONSUMIDOR"));
                    if (lngidAutCons > 0)
                    {
                        if (e.ButtonID == "btnReimprimir")
                        {
                            string strJson = dgvIntimacion.GetRowValues(index, "OBSERVACIONES").ToString();
                            var objIntimacion = JsonSerializer.DeserializeFromString<CRegistroIntimacion>(strJson);

                            var repInt = new repIntimacion();
                            repInt.xtcConsumidor.Text = txtPinConsumidor.Text;
                            repInt.xtcPeriodo.Text = objIntimacion.strPeriodo;
                            repInt.xtcFechaRegistro.Text = dgvIntimacion.GetRowValues(index, "ID_AUTORIZACION_CONSUMIDOR").ToString();
                            repInt.xtcUsuario.Text = dgvIntimacion.GetRowValues(index, "APP_USUARIO").ToString();
                            repInt.xtcCodBarra.Text = objIntimacion.decNotificacion.ToString();
                            repInt.xtcNotificacion.Text = objIntimacion.strCodBarra;
                            repInt.xtcNotificante.Text = objIntimacion.strNotificante;
                            repInt.xtcNotificada.Text = objIntimacion.strNotificado;
                            repInt.xtcFechaInicio.Text = objIntimacion.decFechaInicio.ToString();
                            repInt.xtcDiasPlazo.Text = objIntimacion.decDiasPlazo.ToString();
                            repInt.xtcObservaciones.Text = objIntimacion.strObservacion;

                            Session[CVariablesSesion.TipoReporte] = EnumTipoReporte.Intimacion;
                            Session[CVariablesSesion.ReportePrincipal] = repInt;
                            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( 'WFReporte.aspx', '');", true);
                        }
                        else
                        {
                            //todo No implementado.
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
            else
            {
                //todo No implementado.
            }
        }

        protected void btnPinAgregar_Click(object sender, EventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Codigo a controlar

                    string strMensaje = string.Empty;
                    if (MValidaIntimacion(ref strMensaje))
                    {
                        var objIntimacion = new CRegistroIntimacion();
                        objIntimacion.strPeriodo = txtPinPeriodo.Text;
                        objIntimacion.strDescripcion = txtPinDescripcion.Text;
                        objIntimacion.decNotificacion = Convert.ToDecimal(detPinFechaNot.Date.Year + detPinFechaNot.Date.Month.ToString().PadLeft(2, '0') + detPinFechaNot.Date.Day.ToString().PadLeft(2, '0') + detPinFechaNot.Date.Hour.ToString().PadLeft(2, '0') + detPinFechaNot.Date.Minute.ToString().PadLeft(2, '0') + detPinFechaNot.Date.Second.ToString().PadLeft(2, '0'));
                        objIntimacion.strCodBarra = txtPinCb.Text;
                        objIntimacion.strNotificante = txtPinNotificante.Text;
                        objIntimacion.strNotificado = txtPinNotificado.Text;
                        objIntimacion.decFechaInicio = Convert.ToDecimal(detPinFechaInicio.Date.Year + detPinFechaInicio.Date.Month.ToString().PadLeft(2, '0') + detPinFechaInicio.Date.Day.ToString().PadLeft(2, '0') + "000000");
                        var decUltimodia = detPinFechaInicio.Date.Day + spnPinDiasPlazo.Number;
                        objIntimacion.decFechaFin = Convert.ToDecimal(detPinFechaInicio.Date.Year + detPinFechaInicio.Date.Month.ToString().PadLeft(2, '0') + decUltimodia.ToString().PadLeft(2, '0') + "235959");
                        objIntimacion.decDiasPlazo = spnPinDiasPlazo.Number;
                        objIntimacion.strObservacion = txtPinObservacion.Text;

                        var objJson = JsonSerializer.SerializeToString(objIntimacion);

                        var datFechaActual = DateTime.Now;
                        var objResultado = _client.Post<EResultado>(CAppSettings.svc_registra_autorizacion_cons,
                        new
                        {
                            strCredencial = CParametrosHydro.strCredencialFuncionario,
                            decFechaInicio = Convert.ToDecimal(cmbGestion.Value+cmbMes.Value.ToString().PadLeft(2,'0')+"01000000"),
                            decFechaFin = Convert.ToDecimal(cmbGestion.Value+cmbMes.Value.ToString().PadLeft(2,'0')+DateTime.DaysInMonth((int)cmbGestion.Value,(int)cmbMes.Value)+"235959"),
                            decEstado = 1,
                            strObservaciones = objJson,
                            strBinarioEstado = "00000000000000000000",
                            decIdConsumidor = Convert.ToDecimal(txtPinIdConsumidor.Text),
                            decTipoAutorizacion = 2,
                            decIdTipoActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()),
                            decFechaInicioAct = objIntimacion.decNotificacion,
                            decFechaFinAct = objIntimacion.strCodBarra,
                            decAppUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]),
                            decAppFechaRegistro = Convert.ToDecimal(datFechaActual.Year + datFechaActual.Month.ToString().PadLeft(2, '0') + datFechaActual.Day.ToString().PadLeft(2, '0') + datFechaActual.Hour.ToString().PadLeft(2, '0') + datFechaActual.Minute.ToString().PadLeft(2, '0') + datFechaActual.Second.ToString().PadLeft(2, '0'))
                        });
                        if (objResultado.intCodigo == 1)
                        {
                            var repInt = new repIntimacion();
                            repInt.xtcConsumidor.Text = txtPinConsumidor.Text;
                            repInt.xtcPeriodo.Text = objIntimacion.strPeriodo;
                            repInt.xtcFechaRegistro.Text = datFechaActual.ToLongDateString() + " " +datFechaActual.ToLongTimeString();
                            repInt.xtcUsuario.Text = Session[CVariablesSesion.DatosUsuario].ToString();
                            repInt.xtcCodBarra.Text = detPinFechaNot.Date.ToShortDateString() + " " + detPinFechaNot.Date.ToShortTimeString();
                            repInt.xtcNotificacion.Text = objIntimacion.strCodBarra;
                            repInt.xtcNotificante.Text = objIntimacion.strNotificante;
                            repInt.xtcNotificada.Text = objIntimacion.strNotificado;
                            repInt.xtcFechaInicio.Text = detPinFechaInicio.Date.ToShortDateString();
                            repInt.xtcDiasPlazo.Text = objIntimacion.decDiasPlazo.ToString();
                            repInt.xtcObservaciones.Text = objIntimacion.strObservacion;

                            Session[CVariablesSesion.TipoReporte] = EnumTipoReporte.Intimacion;
                            Session[CVariablesSesion.ReportePrincipal] = repInt;
                            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( 'WFReporte.aspx', '');", true);

                            pucIntimacion.ShowOnPageLoad = false;
                            pnlDatos_Callback(null, null);
                        }
                        else
                        {
                            MImprimirMensaje(objResultado.strMensaje, EnumTiposMensajeAlerta.Error);
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
                //todo No implementado
            }
        }

        protected void pucHistorial_WindowCallback(object source, PopupWindowCallbackArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Codigo a controlar

                    int index = dgvReporte.FocusedRowIndex;
                    if (index >= 0)
                    {
                        txtPhiPeriodo.Text = cmbMes.Text + "/" + cmbGestion.Text;
                        txtPhiIdConsumidor.Text = dgvReporte.GetRowValues(index, "ID_CONSUMIDOR").ToString();
                        txtPhiConsumidor.Text = dgvReporte.GetRowValues(index, "DENOMINACION").ToString();
                        dgvHistorial_CustomCallback(null, null);
                    }
                    else
                    {
                        //todo No implementado
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
                //todo No implementado
            }
        }

        protected void dgvHistorial_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            try
            {
                #region Proceso a Ejecutar

                if (e.VisibleIndex >= 0)
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

        protected void dgvHistorial_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Codigo a controlar

                    dgvHistorial.DataSource = null;
                    dgvHistorial.DataBind();
                    Session.Remove(CVariablesSesion.GrillaHistorial);

                    var objHistorial = _client.Post<EResultadoHistorialCons>(CAppSettings.svc_listar_bitacora_consumidor,
                    new
                    {
                        strCredencial = CParametrosHydro.strCredencialFuncionario,
                        decFiltro = 2,
                        strParametro1 = txtPhiIdConsumidor.Text,
                        strParametro2 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
                        strParametro3 = cmbGestion.Value + cmbMes.Value.ToString().PadLeft(2, '0') + "01000000",
                        strParametro4 = cmbGestion.Value + cmbMes.Value.ToString().PadLeft(2, '0') + DateTime.DaysInMonth((int)cmbGestion.Value, (int)cmbMes.Value) + "235959"
                    });
                    if (objHistorial.IntCodigo == 1)
                    {
                        objHistorial.OResultado.ForEach(a => a.strFechaRegistro = a.APP_FECHA_REGISTRO.ToString().Substring(6, 2) + "/" + a.APP_FECHA_REGISTRO.ToString().Substring(4, 2) + "/" + a.APP_FECHA_REGISTRO.ToString().Substring(0, 4) + " " + a.APP_FECHA_REGISTRO.ToString().Substring(8, 2) + ":" + a.APP_FECHA_REGISTRO.ToString().Substring(10, 2) + ":" + a.APP_FECHA_REGISTRO.ToString().Substring(12, 2));
                        objHistorial.OResultado.ForEach(a => a.strFechaInicio = a.FECHA_INICIO.ToString().Substring(4, 2) + "/" + a.FECHA_INICIO.ToString().Substring(0, 4));
                        objHistorial.OResultado.ForEach(a => a.strEstado = (a.ESTADO == 1) ? "ACTIVO" : (a.ESTADO == 2) ? "ANULADO" : (a.ESTADO == 4) ? "ARCH.ADJ." : (a.TIPO_BITACORA.Contains("AMPLI") && a.ESTADO == 3) ? "VENCIDO" : "RECEPCIONADO");
                        objHistorial.OResultado = objHistorial.OResultado.OrderBy(x => x.APP_FECHA_REGISTRO).ToList();

                        dgvHistorial.DataSource = objHistorial.OResultado;
                        dgvHistorial.DataBind();
                        Session[CVariablesSesion.GrillaHistorial] = objHistorial.OResultado;
                    }
                    else if (objHistorial.IntCodigo != -1)
                    {
                        MImprimirMensaje(objHistorial.StrMensaje, EnumTiposMensajeAlerta.Error);
                    }
                    else
                    {
                        //todo No implementado
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
                //todo No implementado
            }
        }

        protected void btnEmitir_Click(object sender, EventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    dgeExportar.GridViewID = "dgvReporte";
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
        /// METODO. MCargaParametricas
        /// </summary>
        private void MCargaParametricas()
        {
            //Obtener perfiles del usuario
            var objPerfil = _clientAdminHydro.Get<EResultadoLista>("/EReporteUsuariosPerfiles/" + CParametrosHydro.strCredencialHydroAdmin + "/" + Convert.ToDecimal(CAppSettings.idAplicacion) + "/" + Session[CVariablesSesion.IdUsuario] + "?format=json");
            if (objPerfil.decCodigo == 1)
            {
                Session[CVariablesSesion.ListaPerfiles] = objPerfil.oResultado;
            }
            else
            {
                Session.Remove(CVariablesSesion.ListaPerfiles);
            }
            Session.Remove(CVariablesSesion.FechaHoraServ);
            var objFechaHora = _client.Post<EResultadoFeHoServ>(CAppSettings.svc_fecha_hora_actual,
            new
            {
                strCredencial = CParametrosHydro.strCredencialFuncionario
            });
            if (objFechaHora.IntCodigo == 1)
            {
                string strAux = objFechaHora.OResultado[0].FECHA_HORA_ACTUAL.ToString();
                string strValor = strAux.Substring(6, 2) + "/" + strAux.Substring(4, 2) + "/" + strAux.Substring(0, 4) + " " + strAux.Substring(8, 2) + ":" + strAux.Substring(10, 2) + ":" + strAux.Substring(12, 2);
                objFechaHora.OResultado[0].strFechaHoraActu = strValor;
                objFechaHora.OResultado[0].datFechaHoraActu = DateTime.Parse(strValor);
                Session[CVariablesSesion.FechaHoraServ] = objFechaHora.OResultado[0];
            }
            else
            {
                //todo No implementado.
            }
        }

        /// <summary>
        /// METODO. MIniciaFechas
        /// </summary>
        private void MIniciaFechas()
        {
            O_FECHA_HORA_ACTUAL_CTY objFeHoServ = Session[CVariablesSesion.FechaHoraServ] as O_FECHA_HORA_ACTUAL_CTY;
            var x = DateTime.Parse("01/" + objFeHoServ.datFechaHoraActu.Date.Month.ToString().PadLeft(2, '0') +"/"+objFeHoServ.datFechaHoraActu.Date.Year).AddDays(-1);

            cmbMes.Value = x.Month;
            cmbGestion.Value = x.Year;
        }

        /// <summary>
        /// METODO. MCargaArbol
        /// </summary>
        private void MCargaArbol()
        {
            MLimpiaGrilla();
            decimal idUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
            var lista2 = _client.Post<EResultadoEntidad>(CAppSettings.svc_obtener_entidades,
                        new EObtenerEntidadesUsuario()
                        {
                            decFiltro = 1,
                            strParametro1 = idUsuario.ToString(),
                            strParametro2 = cConsultasParametricas.ObtieneIdsActividadOctanioVol(),
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
                    Session[CVariablesSesion.ObtenerEntidades] = lstEntidad;
                    lstEntidad = lstEntidad.OrderBy(x => x.ACTIVIDAD).ThenBy(x => x.DEPARTAMENTO).ThenBy(x => x.DENOMINACION).ToList();

                    int idDe = 0;
                    int idUnico = 1;
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
                            idDe = (int)lst.ID_ACTIVIDAD;
                            idUnico++;
                        }
                        else
                        {
                            //todo No implementado.
                        }
                        idUnico++;
                    }
                    Session[CVariablesSesion.EstructuraArbol] = lstResultado;
                    tlsOperadores.DataSource = lstResultado;
                    tlsOperadores.DataBind();
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

        /// <summary>
        /// METODO. MCargaGrilla
        /// </summary>
        /// <param name="intIdActividad"></param>
        private void MCargaGrilla(decimal intIdActividad)
        {
            var objConsulta =  new
            {
                strCredencial = CParametrosHydro.strCredencialFuncionario,
                decFiltro = 1,
                strParametro1 = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]),
                strParametro2 = intIdActividad,
                strParametro3 = Convert.ToDecimal(cmbGestion.Value + cmbMes.Value.ToString().PadLeft(2, '0') + "01000000"),
                strParametro4 = Convert.ToDecimal(cmbGestion.Value + cmbMes.Value.ToString().PadLeft(2, '0') + DateTime.DaysInMonth((int)cmbGestion.Value, (int)cmbMes.Value).ToString().PadLeft(2, '0') + "235959"),
                strParametro5 = CParametrosHydro.decIdAplicacion
            };
            var objEntidades = _client.Post<EResultadoCumplimientoMes>(CAppSettings.svc_listar_reporte_cumplimiento, objConsulta);
            if (objEntidades.IntCodigo == 1)
            {
                var lstReporteCumplimiento = (List<O_LISTA_REPORTE_MENSUAL_CTY>)objEntidades.OResultado;
                lstReporteCumplimiento.ForEach(a => a.strFechaReg = (a.FECHA_REPORTE == 0 || a.ESTADO_MENSUAL != 1) ? "" : a.FECHA_REPORTE.ToString().Substring(6, 2) + "/" + 
                    a.FECHA_REPORTE.ToString().Substring(4, 2) + "/" + a.FECHA_REPORTE.ToString().Substring(0, 4) + " " + a.FECHA_REPORTE.ToString().Substring(8, 2) + ":" + a.FECHA_REPORTE.ToString().Substring(10, 2) + ":" + a.FECHA_REPORTE.ToString().Substring(10, 2));
                lstReporteCumplimiento = lstReporteCumplimiento.OrderBy(x => x.DEPARTAMENTO).ThenBy(x => x.DENOMINACION).ToList();
                dgvReporte.DataSource = lstReporteCumplimiento;//objEntidades.OResultado;
                dgvReporte.DataBind();
                Session[CVariablesSesion.GrillaCabecera] = lstReporteCumplimiento; //objEntidades.OResultado;

                string strAccion = JsonSerializer.SerializeToString(objConsulta);
                string strSalida = lstReporteCumplimiento.Count().ToString();
                string mensaje = GetType().Name + "." + MethodBase.GetCurrentMethod().Name;
                _cTrazaInf.MRegistraTrazaInf(strAccion, mensaje, CParametrosHydro.decIdAplicacion, Convert.ToDecimal(Session[CVariablesSesion.IdUsuario].ToString()), HttpContext.Current.Request.UserHostAddress, strSalida);  
            }
            else
            {
                MImprimirMensaje(objEntidades.StrMensaje, EnumTiposMensajeAlerta.Error);
            }
        }

        /// <summary>
        /// METODO. MValidaDatosIniciales
        /// </summary>
        /// <returns></returns>
        private bool MValidaDatosIniciales()
        {
            bool booResultado = true;
            var objFeHoServ = Session[CVariablesSesion.FechaHoraServ] as O_FECHA_HORA_ACTUAL_CTY;

            if (cmbGestion.Text == "" || cmbMes.Text == "")
            {
                MImprimirMensaje("Introduzca un periodo valido.", EnumTiposMensajeAlerta.Error);
                booResultado = false;
            }
            else if (Convert.ToInt32(cmbGestion.Value+cmbMes.Value.ToString().PadLeft(2,'0')) > Convert.ToInt32(objFeHoServ.FECHA_HORA_ACTUAL.ToString().Substring(0,6)))
            {
                MImprimirMensaje("Introduzca un periodo valido.", EnumTiposMensajeAlerta.Error);
                booResultado = false;
            }
            return booResultado;
        }

        /// <summary>
        /// METODO. MValidaAmpliacion
        /// </summary>
        /// <param name="strMensaje"></param>
        /// <returns></returns>
        private bool MValidaAmpliacion(ref string strMensaje)
        {
            bool booResultado = true;

            detPamFechaIni.Date = DateTime.Parse(detPamFechaIni.Date.ToShortDateString());
            detPamFechaFin.Date = DateTime.Parse(detPamFechaFin.Date.ToShortDateString());

            var datFechaActual = DateTime.Now.Date;
            if (detPamFechaIni.Date < datFechaActual)
            {
                strMensaje = "Las fechas de habilitación no pueden ser menor a la fecha actual.";
                booResultado = false;
            }
            else if (detPamFechaFin.Date < detPamFechaIni.Date)
            {
                strMensaje = "Fechas de habilitación inválida.";
                booResultado = false;
            }
            else if (detPamFechaIni.Text.Substring(3, 7) != detPamFechaFin.Text.Substring(3, 7))
            {
                strMensaje = "Las fechas de habilitación deben estar en el mismo periodo.";
                booResultado = false;
            }
            else
            {
                var lstAmpliaciones = Session[CVariablesSesion.GrillaAmpliacion] as List<O_LISTA_AUTORIZACION_CONS_CTY>;
                if (lstAmpliaciones != null && lstAmpliaciones.Any())
                {
                    lstAmpliaciones = lstAmpliaciones.Where(x => x.ESTADO == 1).ToList();
                    if (lstAmpliaciones.Any())
                    {
                        strMensaje = "Solo puede tener una ampliación activa por periodo.";
                        booResultado = false;
                    }
                    else
                    {
                        //todo No implementado
                    }
                }
            }
            return booResultado;
        }

        /// <summary>
        /// METODO. MValidaIntimacion
        /// </summary>
        /// <param name="strMensaje"></param>
        /// <returns></returns>
        private bool MValidaIntimacion(ref string strMensaje)
        {
            var booResultado = true;

            if (txtPinCb.Text == "" || txtPinCb.Text == "0")
            {
                strMensaje = "Introduzca un código de barras.";
                booResultado = false;
            }
            else if(txtPinNotificante.Text == "")
            {
                strMensaje = "Introduzca la persona notificante.";
                booResultado = false;
            }
            else if(txtPinNotificado.Text == "")
            {
                strMensaje = "Introduzca la persona notificada.";
                booResultado = false;
            }
            
            return booResultado;
        }

        /// <summary>
        /// METODO. MLimpiaGrilla
        /// </summary>
        private void MLimpiaGrilla()
        {
            Session.Remove(CVariablesSesion.GrillaCabecera);
            dgvReporte.DataSource = null;
            dgvReporte.DataBind();

            Session.Remove(CVariablesSesion.GrillaAcumulado);
            dgvAcumulado.DataSource = null;
            dgvAcumulado.DataBind();

            pnlMensaje.Visible = false;
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