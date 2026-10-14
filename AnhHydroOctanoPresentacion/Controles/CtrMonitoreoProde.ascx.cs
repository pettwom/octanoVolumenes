using AnhHydroOctanoPersistencia.Aplicacion.Consulta;
using AnhHydroOctanoPersistencia.Aplicacion.Reportes;
using AnhHydroOctanoPersistencia.Entidades;
using AnhHydroOctanoPresentacion.Clases.Controles;
using AnhHydroOctanoPresentacion.Entidades;
using AnhHydroOctanoPresentacion.Parametros;
using DevExpress.Web;
using DevExpress.Web.ASPxPivotGrid;
using DevExpress.XtraPivotGrid;
using Librerias.Anh.Us;
using ServiceStack.ServiceClient.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AnhHydroOctanoPresentacion.Controles
{
    public partial class CtrMonitoreoProde : System.Web.UI.UserControl
    {
        #region variables de entorno

        private readonly CPersistenciaConsulta _consulta = new CPersistenciaConsulta();
        private readonly CPersistenciaReportes _reporte = new CPersistenciaReportes();
        readonly JsonServiceClient _client = new JsonServiceClient(CAppSettings.servicio_octVolumen);

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    if (!Page.IsCallback && !Page.IsPostBack)
                    {
                        Session.Remove(CVariablesSesion.ProdeProgramado);
                        Session.Remove(CVariablesSesion.ListaParametrica);
                        Session.Remove(CVariablesSesion.PrimerDiaSemana);

                        MCargaParametricas();

                        var objParametricas = (O_PARAMETRICAS_GRAL)Session[CVariablesSesion.ListaParametrica];
                        cmbProductoProde.DataSource = objParametricas.lstHidrocarburo;
                        cmbProductoProde.DataBind();
                        cmbProductoProde.SelectedItem = cmbProductoProde.Items.FindByValue(Convert.ToInt32("126"));

                        cmbPeriodoGestion.SelectedItem = cmbPeriodoGestion.Items.FindByValue(Convert.ToInt32(DateTime.Now.Year.ToString()));
                        cmbPeriodoMes.SelectedItem = cmbPeriodoMes.Items.FindByValue(Convert.ToInt32(DateTime.Now.Month.ToString()));

                        CargarTechoProde();
                        pnlDatos_Callback(null, null);

                    }
                    else
                    {
                        //todo No es carga inicial: no implementado
                    }

                    if (dgvMonitoreo.DataSource == null && Session[CVariablesSesion.ProdeProgramado] != null)
                    {
                        dgvMonitoreo.DataSource = Session[CVariablesSesion.ProdeProgramado];
                        dgvMonitoreo.DataBind();
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

        private void MCargaParametricas()
        {
            if (MValidaSesion())
            {
                try
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

        private void CargarTechoProde()
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    decimal decMes = Convert.ToDecimal(cmbPeriodoMes.Value);
                    decimal decGestion = Convert.ToDecimal(cmbPeriodoGestion.Value);
                    decimal decIdActividad = Convert.ToDecimal(cmbActividad.Value);
                    decimal decProducto = Convert.ToDecimal(cmbProductoProde.Value);

                    var objResultado = _consulta.ObtenerTechoProde(
                            new EObjetoEnvio5ParametrosProto
                            {
                                strCredencial = CParametrosHydro.strCredencialFuncionario,
                                decFiltro = 1,
                                strParametro1 = decGestion.ToString() + decMes.ToString().PadLeft(2, '0') + "01000000",
                                strParametro2 = decGestion.ToString() + decMes.ToString().PadLeft(2, '0') + "31235959",
                                strParametro3 = decIdActividad.ToString(),
                                strParametro4 = decProducto.ToString(),
                                strParametro5 = ""
                            });
                    if (objResultado.intCodigo == 1)
                    {
                        var lstDato = (List<O_OBTENER_TECHO_PRODE_CTY>)objResultado.oResultado;

                        if (lstDato.Count() > 1)
                        {
                            decimal decTechoProgramado = 0;
                            foreach (var lst in lstDato)
                            {
                                if (lst.ID_TIPO_OPERACION == 337)
                                {
                                    txtTecho.Text = lst.VOLUMEN.ToString();
                                }
                                else if (lst.ID_TIPO_OPERACION == 338)
                                {
                                    txtReserva.Text = lst.VOLUMEN.ToString();
                                }
                                else if (lst.ID_TIPO_OPERACION == 339)
                                {
                                    txtAsignado.Text = lst.VOLUMEN.ToString();
                                }
                                else if (lst.ID_TIPO_OPERACION == 340)
                                {
                                    decTechoProgramado = decTechoProgramado + Convert.ToDecimal(lst.VOLUMEN);
                                }
                                else
                                {
                                    txtTecho.Text = "0";
                                    txtAsignado.Text = "0";
                                    txtReserva.Text = "0";

                                }
                            }

                            txtProgramado.Text = decTechoProgramado.ToString();
                        }
                        else
                        {
                            txtTecho.Text = "0";
                            txtAsignado.Text = "0";
                            txtReserva.Text = "0";
                        }
                    }
                    else
                    {
                        txtTecho.Text = "0";
                        txtAsignado.Text = "0";
                        txtReserva.Text = "0";
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
            #region Proceso a Ejecutar

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

            #endregion
        }

        protected void pnlDatos_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    List<CMonitoreoProde> lstMonitoreoProde = new List<CMonitoreoProde>();

                    string strMes = cmbPeriodoMes.Value.ToString().PadLeft(2, '0');
                    string strGestion = cmbPeriodoGestion.Value.ToString();
                    string strUltimoDia = DateTime.DaysInMonth(Convert.ToInt32(cmbPeriodoGestion.Value), Convert.ToInt32(cmbPeriodoMes.Value)).ToString().PadLeft(2, '0');
                    string strPrimerdiaSemana = Convert.ToDateTime("01/" + strMes + "/" + strGestion + "").DayOfWeek.ToString(); //DateTime.Now.DayOfWeek;                        

                    ImprimirSemanas(strPrimerdiaSemana, strUltimoDia);

                    CargarTechoProde();

                    var objResultado = _consulta.ObtenerVolumenProgramadoProde(
                    new EObjetoEnvio4ParametrosProto
                    {
                        strCredencial = CParametrosHydro.strCredencialFuncionario,
                        decFiltro = Convert.ToDecimal(cmbActividad.Value),
                        strParametro1 = strGestion + strMes + "01000000",
                        strParametro2 = strGestion + strMes + "31235959",
                        strParametro3 = strGestion + strMes,
                        strParametro4 = cmbProductoProde.Value.ToString()
                    });
                    if (objResultado.intCodigo == 1)
                    {
                        var lstDato = (List<O_VOLUMEN_PRODE_CTY>)objResultado.oResultado;

                        if (lstDato.Count() > 1)
                        {
                            foreach (var lst in lstDato)
                            {
                                CMonitoreoProde objMonitoreoProde = new CMonitoreoProde();

                                objMonitoreoProde.DecIdConsumidor = lst.ID_CONSUMIDOR;
                                objMonitoreoProde.StrEstacionServicio = lst.ESTACION_SERVICIO;
                                objMonitoreoProde.StrCodHydroProyecto = lst.COD_HYDRO_PROYECTO;
                                objMonitoreoProde.DecIdHidrocarburo = lst.ID_HIDROCARBURO;
                                objMonitoreoProde.DecIdDepartamento = lst.ID_DEPARTAMENTO;
                                objMonitoreoProde.StrDepartamento = lst.DEPARTAMENTO;
                                objMonitoreoProde.StrSemana = lst.SEMANA;
                                objMonitoreoProde.DecRecepcion = lst.RECEPCION;
                                objMonitoreoProde.DecDemasia = lst.DEMASIA;
                                objMonitoreoProde.DecTotalIngreso = lst.RECEPCION + lst.DEMASIA;
                                objMonitoreoProde.DecVenta = lst.VENTA;
                                objMonitoreoProde.DecMerma = lst.MERMA;
                                objMonitoreoProde.DecTotalEgreso = lst.VENTA + lst.MERMA;
                                objMonitoreoProde.DecProde = lst.PRODE;

                                lstMonitoreoProde.Add(objMonitoreoProde);

                            }

                            lstMonitoreoProde = lstMonitoreoProde.Where(x => x.StrSemana != "Error").ToList();

                            Session[CVariablesSesion.ProdeProgramado] = lstMonitoreoProde;
                            dgvMonitoreo.DataSource = lstMonitoreoProde;
                            dgvMonitoreo.DataBind();

                        }
                        else
                        {
                            Session.Remove(CVariablesSesion.ProdeProgramado);
                            dgvMonitoreo.DataSource = null;
                            dgvMonitoreo.DataBind();
                        }
                    }
                    else
                    {
                        Session.Remove(CVariablesSesion.ProdeProgramado);
                        dgvMonitoreo.DataSource = null;
                        dgvMonitoreo.DataBind();
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

        private void ImprimirSemanas(string strPrimerdiaSemana, string strUltimoDia)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    /*decimal decCantDias = 0;
                    decimal decPrimerDiaSemana = 1;
                    decimal decUltimoDiaSemana = 7;
                    decimal decUltimoDiaMes = Convert.ToDecimal(strUltimoDia);
                    int intSemana = 0;            
                    string[] strSemanas = new string[6];

                    if (strPrimerdiaSemana == "Monday")
                    {
                        decCantDias = 6;
                    }
                    else if (strPrimerdiaSemana == "Tuesday")
                    {
                        decCantDias = 5;
                    }
                    else if (strPrimerdiaSemana == "Wednesday")
                    {
                        decCantDias = 4;
                    }
                    else if (strPrimerdiaSemana == "Thursday")
                    {
                        decCantDias = 3;
                    }
                    else if (strPrimerdiaSemana == "Friday")
                    {
                        decCantDias = 2;
                    }
                    else if (strPrimerdiaSemana == "Saturday")
                    {
                        decCantDias = 4;
                    }
                    else if (strPrimerdiaSemana == "Sunday")
                    {
                        decCantDias = 0;
                    } 
                    else
                    {
                        decCantDias = 0;
                    }

                    decimal strDiaSiguiente = 0;
                    string strCadenaSemana = "";

                    while ((decPrimerDiaSemana + 7) <= decUltimoDiaMes){

                        strCadenaSemana = (decPrimerDiaSemana + strDiaSiguiente).ToString() + " - "; 

                        decPrimerDiaSemana = decPrimerDiaSemana + decCantDias;

                        if ((decPrimerDiaSemana + strDiaSiguiente) == (decPrimerDiaSemana + decCantDias))
                        {
                            strCadenaSemana = decPrimerDiaSemana.ToString();
                        }
                        else
                        {
                            strCadenaSemana = strCadenaSemana + decPrimerDiaSemana.ToString();
                        }               

                        decCantDias = 7;
                        strDiaSiguiente = 1;

                        strSemanas[intSemana] = strCadenaSemana;

                        intSemana++;

                    }

                    strCadenaSemana = (decPrimerDiaSemana + strDiaSiguiente).ToString() + " - ";

                    if ((decPrimerDiaSemana + strDiaSiguiente) == decUltimoDiaMes)
                    {
                        strCadenaSemana = decUltimoDiaMes.ToString();
                    }
                    else
                    {
                        strCadenaSemana = strCadenaSemana + decUltimoDiaMes.ToString();
                    }            
            
                    strSemanas[intSemana] = strCadenaSemana;

                    intSemana++;

                    while (intSemana <= 5) {
                        strSemanas[intSemana] = "";
                        intSemana++;
                    }

                    txtSemana1.Text = strSemanas[0];
                    txtSemana2.Text = strSemanas[1];
                    txtSemana3.Text = strSemanas[2];
                    txtSemana4.Text = strSemanas[3];
                    txtSemana5.Text = strSemanas[4];
                    txtSemana6.Text = strSemanas[5];*/

                    string[] strSemanas = new string[5];

                    strSemanas[0] = "1";
                    strSemanas[1] = "8";
                    strSemanas[2] = "15";
                    strSemanas[3] = "22";

                    txtSemana1.Text = "1 - 7";
                    txtSemana2.Text = "8 - 14";
                    txtSemana3.Text = "15 - 21";
                    txtSemana4.Text = "22 - 28";

                    if (28 == Convert.ToDecimal(strUltimoDia))
                    {
                        txtSemana5.Text = "";
                        strSemanas[4] = "0";
                    }
                    else if (29 == Convert.ToDecimal(strUltimoDia))
                    {
                        txtSemana5.Text = "29";
                        strSemanas[4] = "29";
                    }
                    else if (28 < Convert.ToDecimal(strUltimoDia))
                    {
                        txtSemana5.Text = "29 - " + strUltimoDia;
                        strSemanas[4] = "29";
                    }
                    else
                    {
                        txtSemana5.Text = "";
                        strSemanas[4] = "0";
                    }

                    txtSemana6.Text = "";
                    Session[CVariablesSesion.PrimerDiaSemana] = strSemanas;

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

        protected void dgvMonitoreo_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType != GridViewRowType.Data) return;
            decimal decValorProde = Convert.ToInt32(e.GetValue("DecProde"));

            decimal decValorRecepcion = Convert.ToInt32(e.GetValue("DecRecepcion"));
            decimal decValorDemasia = Convert.ToInt32(e.GetValue("DecDemasia"));

            if ((decValorRecepcion + decValorDemasia) > decValorProde)
            {
                e.Row.ForeColor = System.Drawing.Color.Red;
                e.Row.Cells[0].Style.Add(HtmlTextWriterStyle.BackgroundColor, "LightRed");
                e.Row.Cells[0].Style.Add(HtmlTextWriterStyle.Color, "White");
                //e.Row.BackColor = System.Drawing.Color.DarkRed;

            }

            e.Row.Cells[6].Style.Add(HtmlTextWriterStyle.BackgroundColor, "LightYellow");
            //e.Row.Cells[6].Style.Add(HtmlTextWriterStyle.Color, "Black");
            e.Row.Cells[9].Style.Add(HtmlTextWriterStyle.BackgroundColor, "LightYellow");
            //e.Row.Cells[9].Style.Add(HtmlTextWriterStyle.Color, "Black");
            e.Row.Cells[10].Style.Add(HtmlTextWriterStyle.BackgroundColor, "LightGray");

        }

    }
}