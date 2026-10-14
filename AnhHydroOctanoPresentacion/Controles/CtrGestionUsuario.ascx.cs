
using AnhHydroOctanoPresentacion.Entidades;
using AnhHydroOctanoPresentacion.Lib;
using AnhHydroOctanoPresentacion.Sitio.Usuarios;
using Microsoft.Ajax.Utilities;
using ServiceStack.ServiceClient.Web;

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
    using AnhHydroOctanoAgenteServicio;    

    public partial class CtrGestionUsuario : UserControl
    {
        #region Variables de entorno

        readonly JsonServiceClient client = new JsonServiceClient(CAppSettings.ServicioAdministradorHydro);
        private readonly CPersistenciaConsulta _consulta = new CPersistenciaConsulta();
        readonly JsonServiceClient _client = new JsonServiceClient(CAppSettings.servicio_octVolumen);
        readonly CUtil _cUtil = new CUtil();
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

                        Session.Remove(CVariablesSesion.GrillaAuxiliar);
                        Session.Remove(CVariablesSesion.Aux);

                        Session.Remove(CVariablesSesion.ComboGenerico);


                        Session[CVariablesSesion.ObjetoEntidad] = Session[CVariablesSesion.ObjetoArbol];
                        
                        MCargaDatosIniciales();


                        MCargaArbol(1);
                        

                        tlsOperadores.DataSource = Session[CVariablesSesion.EstructuraArbol];
                        tlsOperadores.DataBind();

                        

                        //tlsAsignar.DataSource = Session[CVariablesSesion.EstructuraArbol];
                        //tlsAsignar.DataBind();
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

                    if (tlsAsignar.DataSource == null)
                    {
                        tlsAsignar.DataSource = Session[CVariablesSesion.Aux];
                        tlsAsignar.DataBind();
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

                    if (dgvUsCabecera.DataSource == null && Session[CVariablesSesion.GrillaListaAuxiliar] != null)
                    {
                        dgvUsCabecera.DataSource = Session[CVariablesSesion.GrillaListaAuxiliar];
                        dgvUsCabecera.DataBind();
                    }
                    else
                    {
                        //todo No implementado.
                    }

                    if (grvListaUsuarios.DataSource == null && Session[CVariablesSesion.ListaUsuarios] != null)
                    {
                        grvListaUsuarios.DataSource = Session[CVariablesSesion.ListaUsuarios];
                        grvListaUsuarios.DataBind();
                    }
                    else
                    {
                        //todo No implementado.
                    }

                    if (cmbModulo.DataSource == null && Session[CVariablesSesion.ComboGenerico] != null)
                    {
                        cmbModulo.DataSource = Session[CVariablesSesion.ComboGenerico];
                        cmbModulo.DataBind();
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
                        
                        var intTipoAccion = (int)tlsOperadores.FocusedNode.GetValue("IntAccion");

                        var strIdEntidad = tlsOperadores.FocusedNode.GetValue("IntIdEntidad").ToString();
                        var strIdTipoUs = "";
                        var strIdTipoCtrl = "";
                        var decFiltro = 1;

                        var strDato1 = "";
                        var strDato2 = "";

                        if ((int)rbtTipoOrden.Value == 1 || intTipoAccion==3)
                        {
                            switch (intTipoAccion)
                            {
                                case 0:
                                    lblEmpresa.Text = tlsOperadores.FocusedNode.GetValue("StrDescPadre").ToString();
                                    strIdTipoUs = "1";
                                    strIdTipoCtrl = "4";

                                    decFiltro = 4;
                                    strDato1 = strIdEntidad;
                                    break;
                                case 1:
                                    lblEmpresa.Text = tlsOperadores.FocusedNode.GetValue("StrDescPadre").ToString();
                                    lblActividad.Text = tlsOperadores.FocusedNode.GetValue("StrActividad").ToString();
                                    return;
                                    break;
                                default:
                                    lblEmpresa.Text = tlsOperadores.FocusedNode.GetValue("StrDescPadre").ToString();
                                    //lblActividad.Text = tlsOperadores.FocusedNode.GetValue("StrActividad").ToString();
                                    lblProyecto.Text = tlsOperadores.FocusedNode.GetValue("StrDescripcion").ToString();
                                    strIdTipoUs = "2";
                                    strIdTipoCtrl = "3";

                                    decFiltro = 3;
                                    strDato1 = strIdEntidad;

                                    break;
                            }

                            if (intTipoAccion == 1)
                                return;
                        }
                        else
                        {
                            decFiltro = 2;
                            switch (intTipoAccion)
                            {
                                case 1:
                                    lblEmpresa.Text = tlsOperadores.FocusedNode.GetValue("StrDescPadre").ToString();
                                    strIdTipoCtrl = "1";
                                    strIdTipoUs = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString();

                                    decFiltro = 1;
                                    strDato1 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString();
                                    break;
                                case 2:
                                    lblEmpresa.Text = tlsOperadores.FocusedNode.GetValue("StrDescPadre").ToString();
                                    //lblActividad.Text = tlsOperadores.FocusedNode.GetValue("StrActividad").ToString();
                                    strIdTipoCtrl = "2";
                                    strIdTipoUs = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString();
                                    strIdEntidad = tlsOperadores.FocusedNode.GetValue("DecIdDepartamento").ToString();
                                    
                                    decFiltro = 2;
                                    strDato1 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString();
                                    strDato2 = tlsOperadores.FocusedNode.GetValue("DecIdDepartamento").ToString();
                                    break;
                                case 3:
                                //    lblEmpresa.Text = tlsOperadores.FocusedNode.GetValue("StrDescPadre").ToString();
                                //    lblActividad.Text = tlsOperadores.FocusedNode.GetValue("StrActividad").ToString();
                                //    lblProyecto.Text = tlsOperadores.FocusedNode.GetValue("StrDescripcion").ToString();
                                //    strIdTipoUs = "2";
                                //    strIdTipoCtrl = "3";
                                    decFiltro = 3;
                                    strDato1 = strIdEntidad;
                                    break;
                            }
                        }


                        MCargaGrilla(decFiltro, strDato1, strDato2);
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
        
        protected void dgvCabecera_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
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
       }

        protected void pnlUsuario_Callback(object sender, CallbackEventArgsBase e)
        {
            if (txtUsuario.Text.Length > 2)
            {
                CargarUsuariosSistema();
            }
            else
            {
                //ClientScript.RegisterStartupScript(this.GetType(), null, "MensajeAdvertencia('DEBE ESCRIBIR MAS DE DOS LETRAS PARA BUSCAR USUARIO.');", true);
            }
        }

        protected void pnlUsCabecera_Callback(object sender, CallbackEventArgsBase e)
        {            
            dgvUsCabecera.DataSource = null;
            dgvUsCabecera.DataBind();
            Session[CVariablesSesion.GrillaListaAuxiliar] = null;

            int index = grvListaUsuarios.FocusedRowIndex;          
            if (index >= 0)
            {
                var idUs = grvListaUsuarios.GetRowValues(index, "ID_USUARIO").ToString();
                var strUs = grvListaUsuarios.GetRowValues(index, "NOMBRE_USUARIO").ToString();           

                var objResultado = _client.Post<EResultadoListarUsuario>(CAppSettings.svc_listar_usuarios,
                new
                {
                    strCredencial = CParametrosHydro.strCredencialFuncionario,
                    decFiltro = 5,//1:arbol x entidad | 2: arbol x actividad | 3: x usuario
                    decIdModulo = Convert.ToDecimal(cmbModulo.Value),
                    strParametro1 = idUs,
                    strParametro2 = ""
                });
                if (objResultado.IntCodigo == 1)
                {
                    dgvUsCabecera.DataSource = objResultado.OResultado;
                    dgvUsCabecera.DataBind();
                    Session[CVariablesSesion.GrillaListaAuxiliar] = objResultado.OResultado;
                }

                CargarPerfilesUsuario(Convert.ToDecimal(idUs), strUs);
            }
                       
        }

        protected void btnEmiteReporte_Click(object sender, EventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    //if (dgvDetalle.VisibleRowCount > 0)
                    //{
                    //    if ((int)cmbTipoExport.Value == 1)
                    //    {
                    //        dgeExportar.GridViewID = "dgvDetalle";
                    //        dgeExportar.WriteXlsxToResponse("RepDosificacion");
                    //    }
                    //    else
                    //    {
                    //        var intTipoAccion = (int)tlsOperadores.FocusedNode.GetValue("IntAccion");
                    //        switch (intTipoAccion)
                    //        {
                    //            case 0:
                    //                lblEmpresa.Text = tlsOperadores.FocusedNode.GetValue("StrDescPadre").ToString();
                    //                lblActividad.Text = "";
                    //                lblProyecto.Text = "";
                    //                break;
                    //            case 1:
                    //                lblEmpresa.Text = tlsOperadores.FocusedNode.GetValue("StrDescPadre").ToString();
                    //                lblActividad.Text = tlsOperadores.FocusedNode.GetValue("StrActividad").ToString();
                    //                lblProyecto.Text = "";
                    //                break;
                    //            default:
                    //                lblEmpresa.Text = tlsOperadores.FocusedNode.GetValue("StrDescPadre").ToString();
                    //                lblActividad.Text = tlsOperadores.FocusedNode.GetValue("StrActividad").ToString();
                    //                lblProyecto.Text = tlsOperadores.FocusedNode.GetValue("StrDescripcion").ToString();
                    //                break;
                    //        }

                    //        var xreReporte = new repDosificacion();
                    //        xreReporte.xtcFecha.Text = DateTime.Now.ToShortDateString();
                    //        xreReporte.xtcHora.Text = DateTime.Now.ToLongTimeString();
                    //        xreReporte.xtcUsuario.Text = CParametrosHydro.StrUsuarioLogin;
                    //        xreReporte.xtcIntervalo.Text = "Del : " + detFechaIni.Date.ToShortDateString() + "  Al : " + detFechaFin.Date.ToShortDateString();

                    //        xreReporte.xtcEmpresa.Text = lblEmpresa.Text;
                    //        xreReporte.xtcActividad.Text = lblActividad.Text;
                    //        xreReporte.xtcProyecto.Text = lblProyecto.Text;

                    //        int index = dgvCabecera.FocusedRowIndex;
                    //        if (index >= 0)
                    //        {
                    //            var lstSelect = new List<O_LISTA_REPORTE_DOSIF_DET_CTY>();
                    //            for (int i = 0; i < dgvDetalle.VisibleRowCount; i++)
                    //            {
                    //                var objRep = (O_LISTA_REPORTE_DOSIF_DET_CTY)dgvDetalle.GetRow(i);
                    //                lstSelect.Add(objRep);
                    //            }

                    //            xreReporte.xtcNoAutorizacion.Text = dgvCabecera.GetRowValues(index, "NRO_AUTORIZACION").ToString();
                    //            xreReporte.xtcTipoOp.Text = dgvCabecera.GetRowValues(index, "TIPO_OPERACION").ToString();
                    //            xreReporte.xtcTipoFact.Text = dgvCabecera.GetRowValues(index, "TIPO_FACTURACION").ToString();
                    //            xreReporte.DataSource = lstSelect;

                    //            Session[CVariablesSesion.ReportePrincipal] = xreReporte;
                    //            Session[CVariablesSesion.TipoReporte] = EnumTipoReporte.RepDosificacion;
                    //            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( 'WFReporte.aspx', '');", true);
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    MImprimirMensaje("No existen registros.", EnumTiposMensajeAlerta.Alerta);
                    //}

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
            //string strNoAut;
            //int index = dgvCabecera.FocusedRowIndex;
            //if (index != -1)
            //{
            //    strNoAut = dgvCabecera.GetRowValues(index, "NRO_AUTORIZACION").ToString();
            //}
            //else
            //{
            //    return;
            //}

            //var panel = sender as ASPxCallbackPanel;
            //var lstVtas = (List<O_LISTA_REPORTE_DOSIF_DET_CTY>)Session[CVariablesSesion.GrillaAuxiliar];
            //if (lstVtas.IsNullOrEmpty())
            //{
            //    panel.JSProperties["cp_xAxis"] = null;
            //    panel.JSProperties["cp_colDatA"] = null;
            //    panel.JSProperties["cp_colDatF"] = null;
            //    panel.JSProperties["cp_colDatV"] = null;
            //    panel.JSProperties["cp_pieTotA"] = null;
            //    panel.JSProperties["cp_pieTotF"] = null;
            //    panel.JSProperties["cp_pieTotV"] = null;
            //    return;
            //}

            //panel.JSProperties["cp_title"] = "Estado de la Dosificación";
            //panel.JSProperties["cp_subtitle"] = "No. Autorizacion: " + strNoAut;

            //var fechaIni = detFechaIni.Date;
            //var fechFin = detFechaFin.Date;
            //var td = fechaIni - fechFin;
            //var intDif = td.Days * -1;
            //var names1 = new string[intDif + 1];
            //var datFechaI = fechaIni;
            
            //for (int i = 0; i <= intDif; i++)
            //{
            //    names1[i] = datFechaI.ToShortDateString();
            //    datFechaI = datFechaI.AddDays(1);
            //}

            //var vecTotal = new int[3];
            //string[] vecEstados = { "A", "F", "V" };

            //var result = lstVtas.GroupBy(x => new { x.FECHA_FACTURA_DIA, x.ESTADO })
            //   .Select(g => new { g.Key.FECHA_FACTURA_DIA, g.Key.ESTADO, MyCount = g.Count() })
            //   .OrderBy(d => d.ESTADO).ThenBy(d => d.FECHA_FACTURA_DIA).ToList();

            //var arrAnu = new int[intDif + 1];
            //var arrFal = new int[intDif + 1];
            //var arrVal = new int[intDif + 1];
            //for (int i = 0; i < 3; i++)
            //{
            //    var lstXestado = result.Where(x => x.ESTADO == vecEstados[i]).OrderBy(x => x.FECHA_FACTURA_DIA).ToList();
            //    var arrSerie = new int[intDif + 1];
            //    datFechaI = fechaIni;
            //    vecTotal[i] = 0;

            //    if (lstXestado.Any())
            //    {
            //        for (int j = 0; j <= intDif; j++)
            //        {
            //            var lstAux = lstXestado.Where(x => x.FECHA_FACTURA_DIA.Equals(datFechaI.ToShortDateString())).ToList();
            //            if (lstAux.Any())
            //            {
            //                arrSerie[j] = lstAux.FirstOrDefault().MyCount;
            //            }
            //            else
            //            {
            //                arrSerie[j] = 0;
            //            }
            //            datFechaI = datFechaI.AddDays(1);
            //            vecTotal[i] = vecTotal[i] + arrSerie[j];
            //        }
            //    }
            //    else
            //    {
            //        for (int j = 0; j <= intDif; j++)
            //        {
            //            arrSerie[j] = 0;
            //        }
            //    }

            //    if (vecEstados[i] == "A")
            //    {
            //        arrAnu = arrSerie;
            //    }
            //    else if (vecEstados[i] == "F")
            //    {
            //        arrFal = arrSerie;
            //    }
            //    else
            //    {
            //        arrVal = arrSerie;
            //    }
            //}

            //panel.JSProperties["cp_xAxis"] = names1;
            //panel.JSProperties["cp_colDatA"] = arrAnu;
            //panel.JSProperties["cp_colDatF"] = arrFal;
            //panel.JSProperties["cp_colDatV"] = arrVal;
            //panel.JSProperties["cp_pieTotA"] = vecTotal[0];
            //panel.JSProperties["cp_pieTotF"] = vecTotal[1];
            //panel.JSProperties["cp_pieTotV"] = vecTotal[2];
        }

        protected void tlsAsignar_CustomCallback(object sender, DevExpress.Web.ASPxTreeList.TreeListCustomCallbackEventArgs e)
        {
            tlsAsignar.DataSource = null;
            tlsAsignar.DataBind();

            if (e.Argument == "1")
                return;

            var objArbol = Session[CVariablesSesion.ObjetoEntidad] as List<O_OBTIENE_ENTIDADES_CTY>;
            if (objArbol == null)
            {
                var objResultado = _consulta.ObtenerEntidades(
                new EObjetoEnvio5ParametrosProto
                {
                    decFiltro = 1,
                    strParametro1 = txtIdUsuario.Text.Trim(),
                    strParametro2 = txtActividad.Text.Trim(),
                    strParametro3 = cmbModulo.Value.ToString(),
                    strParametro4 = "",
                    strParametro5 = ""
                });
                if (objResultado.intCodigo == 1)
                {
                    objArbol = (List<O_OBTIENE_ENTIDADES_CTY>)objResultado.oResultado;
                    Session[CVariablesSesion.ObjetoEntidad] = objArbol;
                }
                else
                {
                    MImprimirMensaje(objResultado.strMensaje, EnumTiposMensajeAlerta.Error);
                    return;
                }
            }
            Session[CVariablesSesion.Aux] = _cUtil.MObtenerArbolOrdenado(((int)rbtTipoControl.Value < 3) ? 3 : 1, objArbol);
            tlsAsignar.DataSource = Session[CVariablesSesion.Aux];
            tlsAsignar.DataBind();
        }

        protected void dgvCabecera_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    var index = dgvCabecera.FocusedRowIndex;
                    var decTipoUs = Convert.ToDecimal(dgvCabecera.GetRowValues(index, "TIPO_USUARIO"));

                    if (decTipoUs == 1) return;

                    var decIdUsuarioEntidad = Convert.ToDecimal(dgvCabecera.GetRowValues(index, "ID_TABLA"));
                    var decIdUsuario = Convert.ToDecimal(dgvCabecera.GetRowValues(index, "ID_USUARIO"));

                    if (decIdUsuarioEntidad <= 0) return;

                    switch (e.ButtonID)
                    {
                        case "btnQuitar":
                            var objResultado = _client.Post<EResultado>(CAppSettings.svc_registra_usuario_entidad_general,
                                new
                                {
                                    strCredencial = CParametrosHydro.StrCredencialSireHidro,
                                    decAccion = 2,
                                    decIdUsuarioEntidad,
                                    decIdUsuario,
                                    decIdEntidad = 0,
                                    decFechaDesde = 0,
                                    decFechaHasta = 0,
                                    decIdTipoUsuario = 0,
                                    decIdTipoControl = 0,
                                    decIdTipoActividad = 0,
                                    decIdUbicacion = 0,
                                    decIdModulo = 0,
                                    decAppIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario])
                                });
                            if (objResultado.intCodigo == 1)
                            {
                                pnlUsCabecera_Callback(null, null);
                            }
                            else
                            {
                                MImprimirMensaje(objResultado.strMensaje, EnumTiposMensajeAlerta.Error);
                            }
                            break;

                        default:
                            //todo No implementado.
                            break;
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

        protected void dgvUsCabecera_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    var index = dgvUsCabecera.FocusedRowIndex;
                    var decTipoUs = Convert.ToDecimal(dgvUsCabecera.GetRowValues(index, "TIPO_USUARIO"));
                    if (decTipoUs == 1)
                        return;

                    var decIdUsuarioEntidad = Convert.ToDecimal(dgvUsCabecera.GetRowValues(index, "ID_TABLA"));
                    var decIdUsuario = Convert.ToDecimal(dgvUsCabecera.GetRowValues(index, "ID_USUARIO"));
                    ////var decTipoTabla = Convert.ToDecimal(dgvUsCabecera.GetRowValues(index, "TABLA"));

                    if (decIdUsuarioEntidad > 0)
                    {
                        switch (e.ButtonID)
                        {
                            case "btnUsQuitar":

                                var objResultado = _client.Post<EResultado>(CAppSettings.svc_registra_usuario_entidad_general,
                                new
                                {
                                    strCredencial = CParametrosHydro.StrCredencialSireHidro,
                                    decAccion = 2,
                                    decIdUsuarioEntidad,
                                    decIdUsuario,
                                    decIdEntidad = -99999,
                                    decFechaDesde = 0,
                                    decFechaHasta = 0,
                                    decIdTipoUsuario = 0,
                                    decIdTipoControl = 0,
                                    decIdTipoActividad = 0,
                                    decIdUbicacion = 0,
                                    decIdModulo = 0,
                                    decAppIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario])
                                });
                                if (objResultado.intCodigo == 1)
                                {
                                    pnlUsCabecera_Callback(null, null);
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError", "alert('" + objResultado.strMensaje + "');", true);
                                    //MImprimirMensaje(objResultado.strMensaje, EnumTiposMensajeAlerta.Error);
                                }

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

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            MLimpiaGrilla();
            tlsOperadores.DataSource = null;
            tlsOperadores.DataBind();

            var objArbol = Session[CVariablesSesion.ObjetoEntidad] as List<O_OBTIENE_ENTIDADES_CTY>;
            if (objArbol == null)
            {
                var objResultado = _consulta.ObtenerEntidades(
                new EObjetoEnvio5ParametrosProto
                {
                    decFiltro = 1,
                    strParametro1 = txtIdUsuario.Text.Trim(),
                    strParametro2 = txtActividad.Text.Trim(),
                    strParametro3 = cmbModulo.Value.ToString(),
                    strParametro4 = "",
                    strParametro5 = ""
                });
                if (objResultado.intCodigo == 1)
                {
                    objArbol = (List<O_OBTIENE_ENTIDADES_CTY>)objResultado.oResultado;
                    Session[CVariablesSesion.ObjetoEntidad] = objArbol;
                }
                else
                {
                    MImprimirMensaje(objResultado.strMensaje, EnumTiposMensajeAlerta.Error);
                    return;
                }
            }

            Session[CVariablesSesion.EstructuraArbol] = _cUtil.MObtenerArbolOrdenado((int)rbtTipoOrden.Value, objArbol);
            tlsOperadores.DataSource = Session[CVariablesSesion.EstructuraArbol];
            tlsOperadores.DataBind();
        }

        protected void btnAsignarUsuario_Click(object sender, EventArgs e)
        {
            var index = grvListaUsuarios.FocusedRowIndex;
            if (index < 0)
                return;

            var lstSelecion = tlsAsignar.GetSelectedNodes();
            if (lstSelecion.Count > 0)
            {
                var intEsquema = 1;
                var decIdTipoUsuario = 2;

                var strCredencial = CParametrosHydro.StrCredencialSireHidro;
                decimal decAccion = 1;
                decimal decIdTipoControl = (int)rbtTipoControl.Value;
                var strUsuario = grvListaUsuarios.GetRowValues(index, "NOMBRE_USUARIO").ToString();
                var decIdUsuario = Convert.ToDecimal(grvListaUsuarios.GetRowValues(index, "ID_USUARIO").ToString());
                //var decIdModulo = CParametrosHydro.decIdAplicacion;
                var decIdModulo = Convert.ToDecimal(cmbModulo.Value);
                decimal decFechaDesde = CFechas.ConvierteDateTimeLong(DateTime.Now);

                if (strUsuario.Contains("@ANH.GOB.BO"))
                {
                    intEsquema = 2;
                    decIdTipoUsuario = 3;
                }


                var dateAppFechaRegistro = DateTime.Now;
                var decAppFechaRegistro = CFechas.ConvierteDateTimeLong(dateAppFechaRegistro);
                //var decAppIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                var decAppIdUsuario = Convert.ToDecimal(txtIdUsuario.Text);


                Nullable<decimal> decIdUsuarioEntidad = null;
                decimal decIdEntidad = -99999;
                Nullable<decimal> decFechaHasta = (detFechaVigencia.Text == "") ? (decimal?)null : CFechas.ConvierteDateTimeLong(detFechaVigencia.Date);
                Nullable<decimal> decIdTipoActividad = null;
                Nullable<decimal> decIdUbicacion = null;

                var strMensaje = string.Empty;
                var strMensajeAcum = string.Empty;

                foreach (var nodo in lstSelecion)
                {
                    strMensaje = string.Empty;

                    var objNodo = (CArbol)nodo.DataItem;
                    switch ((int)rbtTipoControl.Value)
                    {
                        case 1://actividad
                            if (objNodo.IntAccion == 1)
                            {
                                decIdTipoActividad = objNodo.IntIdActividad;

                                MGrabaAsignacion(strCredencial, decAccion, decIdUsuarioEntidad, decIdUsuario, decIdEntidad,
                                     decFechaDesde, decFechaHasta, decIdTipoUsuario, decIdTipoControl, decIdTipoActividad,
                                     decIdUbicacion, decIdModulo, decAppIdUsuario, ref strMensaje);
                                strMensajeAcum += strMensaje;
                            }
                            break;
                        case 2://departamento
                            if (objNodo.IntAccion == 2)
                            {
                                decIdTipoActividad = objNodo.IntIdActividad;
                                decIdUbicacion = objNodo.DecIdDepartamento;

                                MGrabaAsignacion(strCredencial, decAccion, decIdUsuarioEntidad, decIdUsuario, decIdEntidad,
                                     decFechaDesde, decFechaHasta, decIdTipoUsuario, decIdTipoControl, decIdTipoActividad,
                                     decIdUbicacion, decIdModulo, decAppIdUsuario, ref strMensaje);
                                strMensajeAcum += strMensaje;
                            }
                            break;
                        case 3://proyecto
                            if (objNodo.IntAccion == 2)
                            {
                                decIdTipoActividad = objNodo.IntIdActividad;
                                decIdUbicacion = objNodo.DecIdDepartamento;
                                decIdEntidad = objNodo.IntIdEntidad;

                                MGrabaAsignacion(strCredencial, decAccion, decIdUsuarioEntidad, decIdUsuario, decIdEntidad,
                                     decFechaDesde, decFechaHasta, decIdTipoUsuario, decIdTipoControl, decIdTipoActividad,
                                     decIdUbicacion, decIdModulo, decAppIdUsuario, ref strMensaje);
                                strMensajeAcum += strMensaje;
                            }
                            break;
                        case 4://entidad padre
                            if (objNodo.IntAccion == 0)
                            {
                                decIdEntidad = objNodo.IntIdEntidadPadre;

                                MGrabaAsignacion(strCredencial, decAccion, decIdUsuarioEntidad, decIdUsuario, decIdEntidad,
                                     decFechaDesde, decFechaHasta, decIdTipoUsuario, decIdTipoControl, decIdTipoActividad,
                                     decIdUbicacion, decIdModulo, decAppIdUsuario, ref strMensaje);
                                strMensajeAcum += strMensaje;
                            }
                            break;
                    }
                }
                if (strMensajeAcum.Trim().Length > 0)
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError", "alert('" + strMensajeAcum + "');", true);

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError", "alert('No existen elementos asignados para la asignación.');", true);
            }
            pnlUsCabecera_Callback(null, null);
        }

        protected void dgvUsCabecera_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            try
            {
                #region Proceso a Ejecutar

                if (e.VisibleIndex % 2 == 1)
                {
                    e.Row.BackColor = Color.FromArgb(253, 237, 212);
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

            pnlMensaje.Visible = false;
            //lblTituloDetalle.Text = "";
        }

        /// <summary>
        /// METODO. MCargaDatosIniciales
        /// </summary>
        private void MCargaDatosIniciales()
        {
            var listaUsuarios = client.Get<EResultado<List<O_MODULO_CTY>>>("/ListaModulos/" + CParametrosHydro.strCredencialHydroAdmin + "/0?format=json");
            if (listaUsuarios.decCodigo != 1) return;

            cmbModulo.DataSource = listaUsuarios.oResultado;
            cmbModulo.DataBind();
            Session[CVariablesSesion.ComboGenerico] = listaUsuarios.oResultado;
            cmbModulo.SelectedItem = cmbModulo.Items.FindByValue(Convert.ToInt32(CAppSettings.idAplicacion));
            txtActividad.Text = cConsultasParametricas.ObtieneIdsActividadOctanioVol();
            txtIdUsuario.Text = Session[CVariablesSesion.IdUsuario].ToString();
        }

        /// <summary>
        /// METODO. MCargaArbol
        /// </summary>
        private void MCargaArbol(int intTipo)
        {
            MLimpiaGrilla();

            if (intTipo == 2)
            {
                Session.Remove(CVariablesSesion.ObjetoEntidad);
                tlsOperadores.DataSource = null;
                tlsOperadores.DataBind();
                
            }
                

            var objArbol = Session[CVariablesSesion.ObjetoEntidad] as List<O_OBTIENE_ENTIDADES_CTY>;
            if (objArbol == null)
            {
                //decimal idUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                var objResultado = _consulta.ObtenerEntidades(
                new EObjetoEnvio5ParametrosProto
                {
                    decFiltro = 1,
                    strParametro1 = txtIdUsuario.Text.Trim(),
                    strParametro2 = txtActividad.Text.Trim(),
                    strParametro3 = cmbModulo.Value.ToString(),
                    strParametro4 = "",
                    strParametro5 = ""
                });
                if (objResultado.intCodigo == 1)
                {
                    objArbol = (List<O_OBTIENE_ENTIDADES_CTY>)objResultado.oResultado;
                    Session[CVariablesSesion.ObjetoEntidad] = objArbol;

                    
                }
                else
                {
                    MImprimirMensaje(objResultado.strMensaje, EnumTiposMensajeAlerta.Error);
                    return;
                }
            }

            Session[CVariablesSesion.EstructuraArbol] = _cUtil.MObtenerArbolOrdenado(1, objArbol);
            tlsOperadores.DataSource = Session[CVariablesSesion.EstructuraArbol];
            tlsOperadores.DataBind();
        }

        /// <summary>
        /// METODO. MCargaGrilla
        /// </summary>
        /// <param name="intFiltro"></param>
        /// <param name="strParam1"></param>
        /// <param name="intFactor"></param>
        private void MCargaGrilla(int intFiltro, string strDato1, string strDato2)
        {
            dgvCabecera.DataSource = null;
            dgvCabecera.DataBind();
            Session[CVariablesSesion.GrillaCabecera] = null;


            var objResultado = _client.Post<EResultadoListarUsuario>(CAppSettings.svc_listar_usuarios,
                new
                {
                    strCredencial = CParametrosHydro.strCredencialFuncionario,
                    decFiltro = intFiltro,//1:arbol x entidad | 2: arbol x actividad
                    decIdModulo = Convert.ToDecimal(cmbModulo.Value),
                    strParametro1 = strDato1,
                    strParametro2 = strDato2
                });
            if (objResultado.IntCodigo == 1)
            {
                dgvCabecera.DataSource = objResultado.OResultado;
                dgvCabecera.DataBind();
                Session[CVariablesSesion.GrillaCabecera] = objResultado.OResultado;
            }
            else
            {
                MImprimirMensaje(objResultado.StrMensaje, EnumTiposMensajeAlerta.Error);
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

        protected void CargarUsuariosSistema()
        {
            try
            {
                var listaUsuarios = client.Get<EResultadoUsuario>("/ListaUsuarios/" + CParametrosHydro.strCredencialHydroAdmin + "/0?format=json");
                if (listaUsuarios.oResultado != null)
                {
                    List<O_USUARIOS_CTY> usuarioEncontrado = listaUsuarios.oResultado.Where(c => c.USUARIO.StartsWith(txtUsuario.Text.Trim(), StringComparison.OrdinalIgnoreCase)).ToList();
                        //.Where(c => c.USUARIO.Contains(txtUsuario.Text.ToUpper()) || c.NOMBRES.Contains(txtUsuario.Text.ToUpper())).ToList(); //&& c.NOMBRES.Contains("@ANH.GOB.BO")).ToList();
                    //grvListaUsuariosAnh.DataSource = usuarioEncontrado.OrderBy(o => o.USUARIO);
                    //grvListaUsuariosAnh.DataBind();

                    List<O_LISTA_USUARIOS_ENTIDAD_CTY> lstUsuarios = new List<O_LISTA_USUARIOS_ENTIDAD_CTY>();
                    foreach (var a in usuarioEncontrado)
                    {
                        O_LISTA_USUARIOS_ENTIDAD_CTY usuario = new O_LISTA_USUARIOS_ENTIDAD_CTY();
                        usuario.ID_USUARIO = a.ID_USUARIO;
                        usuario.NOMBRE_USUARIO = a.NOMBRES;
                        usuario.NOMBRE_USUARIO = a.USUARIO;
                        lstUsuarios.Add(usuario);
                    }

                    grvListaUsuarios.DataSource = lstUsuarios.OrderBy(o => o.NOMBRE_USUARIO);
                    grvListaUsuarios.DataBind();
                    Session[CVariablesSesion.ListaUsuarios] = lstUsuarios; //listaUsuarios.oResultado.ToList();
                }
                else
                {
                    grvListaUsuarios.DataSource = new List<O_LISTA_USUARIOS_ENTIDAD_CTY>();
                    grvListaUsuarios.DataBind();
                }
            }
            catch (Exception ex)
            {
                string strUbicacionMetodo = this.GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                CLogTraza.Error(strUbicacionMetodo, Convert.ToDecimal(CAppSettings.idAplicacion), Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]), HttpContext.Current.Request.UserHostAddress, ex.Message, Convert.ToString(ex.InnerException), ex.StackTrace);
            }
        }

        

        protected void MGrabaAsignacion(
            string strCredencial,
            decimal decAccion,
            Nullable<decimal> decIdUsuarioEntidad,
            decimal decIdUsuario,
            decimal decIdEntidad,
            Nullable<decimal> decFechaDesde,
            Nullable<decimal> decFechaHasta,
            decimal decIdTipoUsuario,
            decimal decIdTipoControl,
            Nullable<decimal> decIdTipoActividad,
            Nullable<decimal> decIdUbicacion,
            decimal decIdModulo,
            decimal decAppIdUsuario,
            ref string strMensaje)
        {
            //return;
            var objResultado = _client.Post<EResultado>(CAppSettings.svc_registra_usuario_entidad_general,
                                new
                                {
                                    strCredencial,
                                    decAccion,
                                    decIdUsuarioEntidad,
                                    decIdUsuario,
                                    decIdEntidad,
                                    decFechaDesde,
                                    decFechaHasta,
                                    decIdTipoUsuario,
                                    decIdTipoControl,
                                    decIdTipoActividad,
                                    decIdUbicacion,
                                    decIdModulo,
                                    decAppIdUsuario
                                });
            if (objResultado.intCodigo != 1)
                strMensaje = objResultado.strMensaje;
        }

        #endregion

        



        protected void CargarPerfilesUsuario(decimal idUsuaro, string strUsuario)
        {
            try
            {
                lbxPerfilesDisponibles.Items.Clear();
                lbxPerfilesAsignados.Items.Clear();

                var listaPerfilesDisponibles = new List<O_PERFIL_CTY>();
                var listaPerfilesAsignados = new List<O_USUARIO_PERFIL_CTY>();
                var idModulo = Convert.ToDecimal(cmbModulo.Value.ToString());
                var listaIdAsignado = new List<decimal>();
                var client = new JsonServiceClient(CAppSettings.ServicioAdministradorHydro);

                if (Convert.ToDecimal(CAppSettings.idAplicacion) > 0)
                {
                    //var resultadoPerfilesUsuario = client.Get<EResultadoLista>("/EReporteUsuariosPerfiles/" + CParametrosHydro.strCredencialHydroAdmin + "/" + Convert.ToDecimal(CAppSettings.idAplicacion) + "/" + idUsuaro + "?format=json");
                    var resultadoPerfilesUsuario = client.Get<EResultadoLista>("/EReporteUsuariosPerfiles/" + CParametrosHydro.strCredencialHydroAdmin + "/" + idModulo + "/" + idUsuaro + "?format=json");
                    if (resultadoPerfilesUsuario.oResultado != null)
                    {
                        listaPerfilesAsignados = resultadoPerfilesUsuario.oResultado;

                        listaPerfilesAsignados = (from r in listaPerfilesAsignados where r.ID_USUARIO == idUsuaro && r.SISTEMA!=null select r).ToList();

                        //listaPerfilesAsignados = listaPerfilesAsignados.Where(l => l.NOMBRE_PERFIL != "REGULADO").ToList();

                        lbxPerfilesAsignados.DataSource = listaPerfilesAsignados;
                        lbxPerfilesAsignados.DataBind();

                        foreach (var itemPerfil in listaPerfilesAsignados)
                        {
                            listaIdAsignado.Add(itemPerfil.ID_PERFIL);
                        }
                    }
                    //Obtenemos lista de  perfiles disponibles
                    var ResultadoPerfilesSistema = client.Get<EResultadoPerfil>("/ListaPerfiles/" + CParametrosHydro.strCredencialHydroAdmin + "/" + idModulo + "?format=json");

                    var listaPerfilesDisponible = ResultadoPerfilesSistema.oResultado;
                    listaPerfilesDisponibles = listaPerfilesDisponible.Where(p => (p.PRIORIDAD != 1 && p.PRIORIDAD < 90)).ToList();

                    //Valida tipo usuario

                    if (strUsuario.Contains("@ANH.GOB.BO"))
                    {
                        //valida permiso
                        if (1 == 1)
                        {
                            listaPerfilesDisponibles = listaPerfilesDisponible.Where(p => p.PRIORIDAD < 3).ToList();
                        }
                        else
                        {
                            listaPerfilesDisponibles = listaPerfilesDisponible.Where(p => p.PRIORIDAD == 2).ToList();
                        }
                    }
                    else
                    {
                        listaPerfilesDisponibles = listaPerfilesDisponible.Where(p => p.PRIORIDAD == 4).ToList();
                    }

                    lbxPerfilesDisponibles.DataSource = listaPerfilesDisponibles;
                    lbxPerfilesDisponibles.DataBind();

                    
                    
                    //if (listaIdAsignado.Count > 0)
                    //{
                    //    listaPerfilesDisponibles = (from l in listaPerfilesDisponibles where !listaIdAsignado.Contains(l.ID_PERFIL) select l).ToList();
                    //    //if (login_usuario.Contains("@ANH.GOB.BO"))
                    //    //{
                    //    //    listaPerfilesDisponibles =
                    //    //        listaPerfilesDisponibles.Where(l => l.NOMBRE_PERFIL != "REGULADO").ToList();
                    //    //}
                    //    //else
                    //    //{
                    //    listaPerfilesDisponibles =
                    //      listaPerfilesDisponibles.Where(l => l.NOMBRE_PERFIL != "FUNCIONARIO ANH" && l.NOMBRE_PERFIL != "REGULADO").ToList();
                    //    //}

                    //    lbxPerfilesDisponibles.DataSource = listaPerfilesDisponibles;
                    //    lbxPerfilesDisponibles.DataBind();
                    //}
                    //else
                    //{
                    //    //if (login_usuario.Contains("@ANH.GOB.BO"))
                    //    //{
                    //    //    listaPerfilesDisponibles =
                    //    //        listaPerfilesDisponibles.Where(l => l.NOMBRE_PERFIL != "REGULADO").ToList();
                    //    //}
                    //    //else
                    //    //{
                    //    listaPerfilesDisponibles =
                    //      listaPerfilesDisponibles.Where(l => l.NOMBRE_PERFIL != "FUNCIONARIO ANH" && l.NOMBRE_PERFIL != "REGULADO").ToList();
                    //    //}

                    //    lbxPerfilesDisponibles.DataSource = listaPerfilesDisponibles;
                    //    lbxPerfilesDisponibles.DataBind();
                    //}
                }
            }
            catch (Exception ex)
            {
                string strUbicacionMetodo = this.GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                CLogTraza.Error(strUbicacionMetodo, Convert.ToDecimal(CAppSettings.idAplicacion), Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]), HttpContext.Current.Request.UserHostAddress, ex.Message, Convert.ToString(ex.InnerException), ex.StackTrace);
            }
        }



        protected void btnAsignarUno_Click(object sender, EventArgs e)
        {
            try
            {
                //if (Convert.ToDecimal(ViewState["id_usuario"]) == Convert.ToDecimal(grvListaUsuarios.GetRowValues(grvListaUsuarios.FocusedRowIndex, "ID_USUARIO")))
                //{
                var decIdUs = Convert.ToDecimal(grvListaUsuarios.GetRowValues(grvListaUsuarios.FocusedRowIndex, "ID_USUARIO"));
                    var strUs = grvListaUsuarios.GetRowValues(grvListaUsuarios.FocusedRowIndex, "NOMBRE_USUARIO").ToString();

                    if (AgregarPerfil(Convert.ToDecimal(lbxPerfilesDisponibles.SelectedValue), decIdUs))
                    {
                        //CargarMenuUsuarioPorPerfil(Convert.ToDecimal(ViewState["id_usuario"]));
                        CargarPerfilesUsuario(Convert.ToDecimal(ViewState["id_usuario"]), strUs);
                    }
                    else
                    {
                        //ClientScript.RegisterStartupScript(this.GetType(), null, "MensajeError('NO SE PUDO ASIGNAR PERFIL AL USUARIO.');", true);
                    }
                //}
                //else
                //{
                //    //ClientScript.RegisterStartupScript(this.GetType(), null, "MensajeAdvertencia('LO PERFILES NO CORRESPONDEN AL USUARIO SELECCIONADO.');", true);
                //}
            }
            catch (Exception ex)
            {
                string strUbicacionMetodo = this.GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                CLogTraza.Error(strUbicacionMetodo, Convert.ToDecimal(CAppSettings.idAplicacion), Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]), HttpContext.Current.Request.UserHostAddress, ex.Message, Convert.ToString(ex.InnerException), ex.StackTrace);
            }
        }
        protected void btnQuitarUno_Click(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(ViewState["id_usuario"]) ==
                Convert.ToDecimal(grvListaUsuarios.GetRowValues(grvListaUsuarios.FocusedRowIndex, "ID_USUARIO")))
            {
                if (lbxPerfilesAsignados.SelectedValue != "")
                {
                    if (QuitarPerfil(Convert.ToDecimal(lbxPerfilesAsignados.SelectedValue)))
                    {
                        //CargarMenuUsuarioPorPerfil(Convert.ToDecimal(ViewState["id_usuario"]));CargarPerfilesUsuario(Convert.ToDecimal(ViewState["id_usuario"]));
                    }
                    else
                    {
                        //.RegisterStartupScript(this.GetType(), null,"MensajeError('NO SE PUDO ELIMINAR EL PERFIL DEL USUARIO.');", true);
                    }
                }
                else
                {
                    //ClientScript.RegisterStartupScript(this.GetType(), null,"MensajeError('SELECCIONE EL PERFIL DE USUARIO.');", true);
                }
            }
            else
            {
                //ClientScript.RegisterStartupScript(this.GetType(), null, "MensajeAdvertencia('LO PERFILES NO CORRESPONDEN AL USUARIO SELECCIONADO.');", true);
            }
        }
        protected void btnAsignarTodo_Click(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(ViewState["id_usuario"]) ==
                Convert.ToDecimal(grvListaUsuarios.GetRowValues(grvListaUsuarios.FocusedRowIndex, "ID_USUARIO")))
            {
                for (int i = 0; i < lbxPerfilesDisponibles.Items.Count; i++)
                {
                    AgregarPerfil(Convert.ToDecimal(lbxPerfilesDisponibles.Items[i].Value), Convert.ToDecimal(ViewState["id_usuario"]));
                }

                var strUs = grvListaUsuarios.GetRowValues(grvListaUsuarios.FocusedRowIndex, "NOMBRE_USUARIO").ToString();
                //CargarMenuUsuarioPorPerfil(Convert.ToDecimal(ViewState["id_usuario"]));
                CargarPerfilesUsuario(Convert.ToDecimal(ViewState["id_usuario"]), strUs);
            }
            else
            {
                //ClientScript.RegisterStartupScript(this.GetType(), null, "MensajeAdvertencia('LO PERFILES NO CORRESPONDEN AL USUARIO SELECCIONADO.');", true);
            }
        }
        protected void btnQuitarTodos_Click(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(ViewState["id_usuario"]) ==
                Convert.ToDecimal(grvListaUsuarios.GetRowValues(grvListaUsuarios.FocusedRowIndex, "ID_USUARIO")))
            {
                for (int i = 0; i < lbxPerfilesAsignados.Items.Count; i++)
                {
                    QuitarPerfil(Convert.ToDecimal(lbxPerfilesAsignados.Items[i].Value));
                }


                //CargarMenuUsuarioPorPerfil(Convert.ToDecimal(ViewState["id_usuario"]));
                var strUs = grvListaUsuarios.GetRowValues(grvListaUsuarios.FocusedRowIndex, "NOMBRE_USUARIO").ToString();
                CargarPerfilesUsuario(Convert.ToDecimal(ViewState["id_usuario"]), strUs);


            }
            else
            {
                //ClientScript.RegisterStartupScript(this.GetType(), null, "MensajeAdvertencia('LO PERFILES NO CORRESPONDEN AL USUARIO SELECCIONADO.');", true);
            }
        }



        protected bool AgregarPerfil(decimal idPerfil, decimal decIdUsuario)
        {
            try
            {
                EInsertaPerfilUsuario objPerfilUsuario = new EInsertaPerfilUsuario();
                objPerfilUsuario.decAppIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                objPerfilUsuario.decPerfilId = idPerfil;
                objPerfilUsuario.decUsuarioId = decIdUsuario;//Convert.ToDecimal(ViewState["id_usuario"]);
                objPerfilUsuario.strCredencial = CParametrosHydro.strCredencialHydroAdmin;
                //var client = new JsonServiceClient(CParametrosHydro.strServicioAdministrador);
                JsonServiceClient client = new JsonServiceClient(CAppSettings.ServicioAdministradorHydro);
                var objResultado = client.Post<EResultadoI>("/EInsertaPerfilUsuario/?format=json", objPerfilUsuario);
                if (objResultado.decCodigo > 0)
                { return true; }
                return false;
            }
            catch (Exception ex)
            {
                string strUbicacionMetodo = this.GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                CLogTraza.Error(strUbicacionMetodo, Convert.ToDecimal(CAppSettings.idAplicacion), Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]), HttpContext.Current.Request.UserHostAddress, ex.Message, Convert.ToString(ex.InnerException), ex.StackTrace);
                return false;
            }
        }
        protected bool QuitarPerfil(decimal idPerfil)
        {
            try
            {
                ERevocaPerfilUsuario objEliminaPerfilUsuario = new ERevocaPerfilUsuario();
                objEliminaPerfilUsuario.decAppIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                objEliminaPerfilUsuario.decPerfilId = idPerfil; ;
                objEliminaPerfilUsuario.decUsuarioId = Convert.ToDecimal(ViewState["id_usuario"]);
                objEliminaPerfilUsuario.strCredencial = CParametrosHydro.strCredencialHydroAdmin;
                JsonServiceClient client = new JsonServiceClient(CAppSettings.ServicioAdministradorHydro);

                var objResElimina = client.Put<EResultadoI>("/ERevocaPerfilUsuario/?format=json",
                    objEliminaPerfilUsuario);
                if (objResElimina.decCodigo > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                string strUbicacionMetodo = this.GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name;
                CLogTraza.Error(strUbicacionMetodo, Convert.ToDecimal(CAppSettings.idAplicacion), Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]), HttpContext.Current.Request.UserHostAddress, ex.Message, Convert.ToString(ex.InnerException), ex.StackTrace);
                return false;
            }

        }

        protected void btnRecargaArbol_Click(object sender, EventArgs e)
        {
            MCargaArbol(2);
        }       
        
    }
}