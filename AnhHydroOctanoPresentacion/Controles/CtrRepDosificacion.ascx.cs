
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
    using AnhHydroOctanoPersistencia.Aplicacion.Consulta;
    using AnhHydroOctanoPersistencia.Entidades;
    using Clases.Util;
    using Enumeradores;
    using Parametros;
    using DevExpress.Web;
    using Librerias.Anh.Us;
    using ServiceStack.Text;
    using CArbol = Clases.Controles.CArbol;

    public partial class CtrRepDosificacion : UserControl
    {
        #region Variables de entorno

        private readonly CPersistenciaConsulta _consulta = new CPersistenciaConsulta();
        readonly CUtil _cUtil = new CUtil();
        readonly CTrazaInf _cTrazaInf = new CTrazaInf();
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
                        Session.Remove(CVariablesSesion.GrillaCabecera);
                        Session.Remove(CVariablesSesion.GrillaDetalle);
                        Session.Remove(CVariablesSesion.LstIdConsumidorIdActividad);
                        Session.Remove(CVariablesSesion.GrillaAuxiliar);
                       
                        MCargaArbol();
                        MCargaDatosIniciales();

                        tlsOperadores.DataSource = Session[CVariablesSesion.EstructuraArbol];
                        tlsOperadores.DataBind();
                    }
                    else
                    {
                        //todo No implementado.
                    }

                    if (tlsOperadores.DataSource == null)
                    {
                        tlsOperadores.DataSource = Session[CVariablesSesion.EstructuraArbol];
                        tlsOperadores.DataBind();
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
                        //todo No implementado.
                    }

                    if (dgvDetalle.DataSource == null && Session[CVariablesSesion.GrillaDetalle] != null)
                    {
                        dgvDetalle.DataSource = Session[CVariablesSesion.GrillaDetalle];
                        dgvDetalle.DataBind();
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

                    if (tlsOperadores.FocusedNode != null)
                    {
                        var lstEntidades = Session[CVariablesSesion.ObjetoArbol] as List<O_OBTIENE_ENTIDADES_CTY>;
                        var intTipoAccion = (int)tlsOperadores.FocusedNode.GetValue("IntAccion");
                        var strIdConsumidor = ""; 
                        var strIdActividad = "";
                        switch (intTipoAccion)
                        {
                            case 0:
                                lblEmpresa.Text = tlsOperadores.FocusedNode.GetValue("StrDescPadre").ToString();
                                lstEntidades = lstEntidades.Where(x => x.ID_ENTIDAD_PADRE == (int)tlsOperadores.FocusedNode.GetValue("IntIdEntidadPadre")).GroupBy(x => x.ID_CONSUMIDOR).Select(grp => grp.First()).ToList();
                                break;
                            case 1:
                                lblEmpresa.Text = tlsOperadores.FocusedNode.GetValue("StrDescPadre").ToString();
                                lblActividad.Text = tlsOperadores.FocusedNode.GetValue("StrActividad").ToString();
                                lstEntidades = lstEntidades.Where(x => x.ID_ENTIDAD_PADRE == (int)tlsOperadores.FocusedNode.GetValue("IntIdEntidadPadre") && x.ID_ACTIVIDAD == (int)tlsOperadores.FocusedNode.GetValue("IntIdActividad")).GroupBy(x => x.ID_CONSUMIDOR).Select(grp => grp.First()).ToList();
                                break;
                            default:
                                lblEmpresa.Text = tlsOperadores.FocusedNode.GetValue("StrDescPadre").ToString();
                                lblActividad.Text = tlsOperadores.FocusedNode.GetValue("StrActividad").ToString();
                                lblProyecto.Text = tlsOperadores.FocusedNode.GetValue("StrDescripcion").ToString();
                                lstEntidades = lstEntidades.Where(x => x.ID_CONSUMIDOR == (int)tlsOperadores.FocusedNode.GetValue("IntIdOperador")).ToList();
                                break;
                        }

                        if (lstEntidades != null && lstEntidades.Any())
                        {
                            foreach (var reg in lstEntidades)
                            {
                                strIdConsumidor += reg.ID_CONSUMIDOR + ",";
                                strIdActividad += reg.ID_ACTIVIDAD + ",";
                            }
                            strIdConsumidor = strIdConsumidor.Substring(0, strIdConsumidor.Length - 1);
                            strIdActividad = strIdActividad.Substring(0, strIdActividad.Length - 1);
                        }
                        Session[CVariablesSesion.LstIdConsumidorIdActividad] = strIdConsumidor + "|" + strIdActividad;                      
                        MCargaGrilla(1, strIdConsumidor, 0);
                    }
                    else
                    {
                        MImprimirMensaje("Seleccione una empresa/Proyecto.", EnumTiposMensajeAlerta.Error);
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
        
        /*protected void dgvCabecera_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
       {
           try
           {
               #region Proceso a Ejecutar

               if (e.VisibleIndex % 2 == 1)
               {
                   e.Row.BackColor = Color.FromArgb(204, 209, 182);
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
       }*/

        protected void dgvDetalle_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            dgvDetalle.DataSource = null;
            dgvDetalle.DataBind();

            int index = dgvCabecera.FocusedRowIndex;
            string strNoAutorizacion = dgvCabecera.GetRowValues(index, "NRO_AUTORIZACION").ToString();
            int intFactor = Convert.ToInt32(dgvCabecera.GetRowValues(index, "FACTOR_CALCULO").ToString());

            if (strNoAutorizacion.Equals("0"))
                return;

            MCargaGrilla(2, Session[CVariablesSesion.LstIdConsumidorIdActividad] + "|" + strNoAutorizacion, intFactor);
        }

        protected void dgvDetalle_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            try
            {
                #region Proceso a Ejecutar

                if (e.VisibleIndex >= 0)
                {
                    if (dgvDetalle.GetRowValues(e.VisibleIndex, "ESTADO").ToString() == "A")
                        e.Row.BackColor = Color.FromArgb(215, 135, 135);
                    else if (dgvDetalle.GetRowValues(e.VisibleIndex, "ESTADO").ToString() == "F")
                    {
                        e.Row.BackColor = Color.FromArgb(178, 1, 1);
                    }
                    else
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

        protected void btnEmiteReporte_Click(object sender, EventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    if (dgvDetalle.VisibleRowCount > 0)
                    {
                        if ((int)cmbTipoExport.Value == 1)
                        {
                            dgeExportar.GridViewID = "dgvDetalle";
                            dgeExportar.WriteXlsxToResponse("RepDosificacion");
                        }
                        else
                        {
                            var intTipoAccion = (int)tlsOperadores.FocusedNode.GetValue("IntAccion");
                            switch (intTipoAccion)
                            {
                                case 0:
                                    lblEmpresa.Text = tlsOperadores.FocusedNode.GetValue("StrDescPadre").ToString();
                                    lblActividad.Text = "";
                                    lblProyecto.Text = "";
                                    break;
                                case 1:
                                    lblEmpresa.Text = tlsOperadores.FocusedNode.GetValue("StrDescPadre").ToString();
                                    lblActividad.Text = tlsOperadores.FocusedNode.GetValue("StrActividad").ToString();
                                    lblProyecto.Text = "";
                                    break;
                                default:
                                    lblEmpresa.Text = tlsOperadores.FocusedNode.GetValue("StrDescPadre").ToString();
                                    lblActividad.Text = tlsOperadores.FocusedNode.GetValue("StrActividad").ToString();
                                    lblProyecto.Text = tlsOperadores.FocusedNode.GetValue("StrDescripcion").ToString();
                                    break;
                            }

                            var xreReporte = new repDosificacion();
                            xreReporte.xtcFecha.Text = DateTime.Now.ToShortDateString();
                            xreReporte.xtcHora.Text = DateTime.Now.ToLongTimeString();
                            xreReporte.xtcUsuario.Text = CParametrosHydro.StrUsuarioLogin;
                            xreReporte.xtcIntervalo.Text = "Del : " + detFechaIni.Date.ToShortDateString() + "  Al : " + detFechaFin.Date.ToShortDateString();

                            xreReporte.xtcEmpresa.Text = lblEmpresa.Text;
                            xreReporte.xtcActividad.Text = lblActividad.Text;
                            xreReporte.xtcProyecto.Text = lblProyecto.Text;

                            int index = dgvCabecera.FocusedRowIndex;
                            if (index >= 0)
                            {
                                var lstSelect = new List<O_LISTA_REPORTE_DOSIF_DET_CTY>();
                                for (int i = 0; i < dgvDetalle.VisibleRowCount; i++)
                                {
                                    var objRep = (O_LISTA_REPORTE_DOSIF_DET_CTY)dgvDetalle.GetRow(i);
                                    lstSelect.Add(objRep);
                                }

                                xreReporte.xtcNoAutorizacion.Text = dgvCabecera.GetRowValues(index, "NRO_AUTORIZACION").ToString();
                                xreReporte.xtcTipoOp.Text = dgvCabecera.GetRowValues(index, "TIPO_OPERACION").ToString();
                                xreReporte.xtcTipoFact.Text = dgvCabecera.GetRowValues(index, "TIPO_FACTURACION").ToString();
                                xreReporte.DataSource = lstSelect;

                                Session[CVariablesSesion.ReportePrincipal] = xreReporte;
                                Session[CVariablesSesion.TipoReporte] = EnumTipoReporte.RepDosificacion;
                                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( 'WFReporte.aspx', '');", true);
                            }
                        }
                    }
                    else
                    {
                        MImprimirMensaje("No existen registros.", EnumTiposMensajeAlerta.Alerta);
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

        protected void pnlGrafico_Callback(object sender, CallbackEventArgsBase e)
        {
            string strNoAut;
            int index = dgvCabecera.FocusedRowIndex;
            if (index != -1)
            {
                strNoAut = dgvCabecera.GetRowValues(index, "NRO_AUTORIZACION").ToString();
            }
            else
            {
                return;
            }

            var panel = sender as ASPxCallbackPanel;
            var lstVtas = (List<O_LISTA_REPORTE_DOSIF_DET_CTY>)Session[CVariablesSesion.GrillaAuxiliar];
            if (lstVtas.IsNullOrEmpty())
            {
                panel.JSProperties["cp_xAxis"] = null;
                panel.JSProperties["cp_colDatA"] = null;
                panel.JSProperties["cp_colDatF"] = null;
                panel.JSProperties["cp_colDatV"] = null;
                panel.JSProperties["cp_pieTotA"] = null;
                panel.JSProperties["cp_pieTotF"] = null;
                panel.JSProperties["cp_pieTotV"] = null;
                return;
            }

            panel.JSProperties["cp_title"] = "Estado de la Dosificación";
            panel.JSProperties["cp_subtitle"] = "No. Autorizacion: " + strNoAut;

            var fechaIni = detFechaIni.Date;
            var fechFin = detFechaFin.Date;
            var td = fechaIni - fechFin;
            var intDif = td.Days * -1;
            var names1 = new string[intDif + 1];
            var datFechaI = fechaIni;
            
            for (int i = 0; i <= intDif; i++)
            {
                names1[i] = datFechaI.ToShortDateString();
                datFechaI = datFechaI.AddDays(1);
            }

            var vecTotal = new int[3];
            string[] vecEstados = { "A", "F", "V" };

            var result = lstVtas.GroupBy(x => new { x.FECHA_FACTURA_DIA, x.ESTADO })
               .Select(g => new { g.Key.FECHA_FACTURA_DIA, g.Key.ESTADO, MyCount = g.Count() })
               .OrderBy(d => d.ESTADO).ThenBy(d => d.FECHA_FACTURA_DIA).ToList();

            var arrAnu = new int[intDif + 1];
            var arrFal = new int[intDif + 1];
            var arrVal = new int[intDif + 1];
            for (int i = 0; i < 3; i++)
            {
                var lstXestado = result.Where(x => x.ESTADO == vecEstados[i]).OrderBy(x => x.FECHA_FACTURA_DIA).ToList();
                var arrSerie = new int[intDif + 1];
                datFechaI = fechaIni;
                vecTotal[i] = 0;

                if (lstXestado.Any())
                {
                    for (int j = 0; j <= intDif; j++)
                    {
                        var lstAux = lstXestado.Where(x => x.FECHA_FACTURA_DIA.Equals(datFechaI.ToShortDateString())).ToList();
                        if (lstAux.Any())
                        {
                            arrSerie[j] = lstAux.FirstOrDefault().MyCount;
                        }
                        else
                        {
                            arrSerie[j] = 0;
                        }
                        datFechaI = datFechaI.AddDays(1);
                        vecTotal[i] = vecTotal[i] + arrSerie[j];
                    }
                }
                else
                {
                    for (int j = 0; j <= intDif; j++)
                    {
                        arrSerie[j] = 0;
                    }
                }

                if (vecEstados[i] == "A")
                {
                    arrAnu = arrSerie;
                }
                else if (vecEstados[i] == "F")
                {
                    arrFal = arrSerie;
                }
                else
                {
                    arrVal = arrSerie;
                }
            }

            panel.JSProperties["cp_xAxis"] = names1;
            panel.JSProperties["cp_colDatA"] = arrAnu;
            panel.JSProperties["cp_colDatF"] = arrFal;
            panel.JSProperties["cp_colDatV"] = arrVal;
            panel.JSProperties["cp_pieTotA"] = vecTotal[0];
            panel.JSProperties["cp_pieTotF"] = vecTotal[1];
            panel.JSProperties["cp_pieTotV"] = vecTotal[2];
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
        /// METODO. MLimpiaGrilla
        /// </summary>
        private void MLimpiaGrilla()
        {
            lblEmpresa.Text = "";
            lblActividad.Text = "";
            lblProyecto.Text = "";

            Session.Remove(CVariablesSesion.GrillaCabecera);
            dgvCabecera.DataSource = null;
            dgvCabecera.DataBind();

            Session.Remove(CVariablesSesion.GrillaDetalle);
            dgvDetalle.DataSource = null;
            dgvDetalle.DataBind();

            pnlMensaje.Visible = false;
            lblTituloDetalle.Text = "DETALLE DE LA DOSIFICACION";
        }

        /// <summary>
        /// METODO. MCargaDatosIniciales
        /// </summary>
        private void MCargaDatosIniciales()
        {
            detFechaIni.Date = DateTime.Now;
            detFechaFin.Date = DateTime.Now;
        }

        /// <summary>
        /// METODO. MCargaArbol
        /// </summary>
        private void MCargaArbol()
        {
            MLimpiaGrilla();
            var objArbol = Session[CVariablesSesion.ObjetoArbol] as List<O_OBTIENE_ENTIDADES_CTY>;
            if (objArbol == null)
            {
                decimal idUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                var objResultado = _consulta.ObtenerEntidades(
                new EObjetoEnvio5ParametrosProto
                {
                    decFiltro = 1,
                    strParametro1 = idUsuario.ToString(),
                    strParametro2 = cConsultasParametricas.ObtieneIdsActividadOctanioVol(),
                    strParametro3 = CAppSettings.idAplicacion,
                    strParametro4 = "",
                    strParametro5 = ""
                });
                if (objResultado.intCodigo == 1)
                {
                    objArbol = (List<O_OBTIENE_ENTIDADES_CTY>)objResultado.oResultado;
                    Session[CVariablesSesion.ObjetoArbol] = objArbol;
                }
                else
                {
                    MImprimirMensaje(objResultado.strMensaje, EnumTiposMensajeAlerta.Error);
                    return;
                }
            }

            Session[CVariablesSesion.EstructuraArbol] = _cUtil.MObtenerArbolOrdenado(1, objArbol);
        }

        /// <summary>
        /// METODO. MCargaGrilla
        /// </summary>
        /// <param name="intFiltro"></param>
        /// <param name="strParam1"></param>
        /// <param name="intFactor"></param>
        private void MCargaGrilla(int intFiltro, string strParam1, int intFactor)
        {
            var objConsulta = new EObjetoEnvio3ParametrosProto
            {
                strCredencial = CParametrosHydro.strCredencialFuncionario,
                decFiltro = intFiltro,// 1: Cabecera| 2: Detalles
                strParametro1 = strParam1, // IdsConsumidores | No.Autorizacion
                strParametro2 = detFechaIni.Date.Year + detFechaIni.Date.Month.ToString().PadLeft(2, '0') + detFechaIni.Date.Day.ToString().PadLeft(2, '0'), // fechaInicio
                strParametro3 = detFechaFin.Date.Year + detFechaFin.Date.Month.ToString().PadLeft(2, '0') + detFechaFin.Date.Day.ToString().PadLeft(2, '0')  // fechaFin
            };
            var objResultado = _consulta.ObtenerDosificacion(intFactor, objConsulta);
            
            if (objResultado.intCodigo == 1 || objResultado.intCodigo == 2)
            { 
                if (intFiltro == 1)
                {
                    dgvCabecera.DataSource = objResultado.oResultado;
                    dgvCabecera.DataBind();
                    Session[CVariablesSesion.GrillaCabecera] = objResultado.oResultado;                                        
                }
                else
                {
                    Session.Remove(CVariablesSesion.GrillaDetalle);
                    Session.Remove(CVariablesSesion.GrillaAuxiliar);

                    dgvDetalle.DataSource = objResultado.oResultado;
                    dgvDetalle.DataBind();
                    Session[CVariablesSesion.GrillaDetalle] = objResultado.oResultado;
                    Session[CVariablesSesion.GrillaAuxiliar] = objResultado.oResultado;

                    if (objResultado.intCodigo == 2)
                    {
                        MImprimirMensaje(objResultado.strMensaje, EnumTiposMensajeAlerta.Alerta);
                    }
                }

                #region Traza consulta de informacion                
                string strAccion = JsonSerializer.SerializeToString(objConsulta);
                string strSalida = intFiltro == 1 ? ((List<O_LISTA_REPORTE_DOSIF_CAB_CTY>)objResultado.oResultado).Count().ToString() : ((List<O_LISTA_REPORTE_DOSIF_DET_CTY>)objResultado.oResultado).Count().ToString();
                string mensaje = GetType().Name + "." + MethodBase.GetCurrentMethod().Name;
                _cTrazaInf.MRegistraTrazaInf(strAccion, mensaje, CParametrosHydro.decIdAplicacion, Convert.ToDecimal(Session[CVariablesSesion.IdUsuario].ToString()), HttpContext.Current.Request.UserHostAddress, strSalida);  
                #endregion                             
            }
            else
            {
                MImprimirMensaje(objResultado.strMensaje, EnumTiposMensajeAlerta.Error);
            }
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