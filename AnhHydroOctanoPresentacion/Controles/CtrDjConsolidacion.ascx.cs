
using AnhHydroOctanoPresentacion.Lib;
using DevExpress.Utils;

namespace AnhHydroOctanoPresentacion.Controles
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Web.UI;
    using AnhHydroOctanoPersistencia.Aplicacion.Consulta;
    using AnhHydroOctanoPersistencia.Aplicacion.Gestion;
    using AnhHydroOctanoPersistencia.Entidades;
    using Clases.Controles;
    using Clases.Persona;
    using Clases.Util;
    using Entidades;
    using Enumeradores;
    using Parametros;
    using DevExpress.Web;
    using Librerias.Anh.Us;
    using ServiceStack.Common.Extensions;
    using ServiceStack.ServiceClient.Web;
    using ServiceStack.Text;
    using AnhHydroOctanoAgenteServicio.ServicioHydroSesion;

    public partial class CtrDjConsolidacion : UserControl
    {
        #region Variables de entorno

        readonly JsonServiceClient _client = new JsonServiceClient(CAppSettings.servicio_octVolumen);
        private readonly CPersistenciaConsulta _consulta = new CPersistenciaConsulta();
        private readonly CPersistenciaGestion _gestion = new CPersistenciaGestion();
        readonly CUtil _cUtil = new CUtil();
        private CPersonaSesion _datSesion;
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
                        Session.Remove(CVariablesSesion.ArchivoDescarga);
                        Session.Remove(CVariablesSesion.Callback);
                        Session.Remove(CVariablesSesion.EstructuraArbol);
                        Session.Remove(CVariablesSesion.GrillaRegObservado);
                        Session.Remove(CVariablesSesion.GrillaTransaccion);

                        Session.Remove(CVariablesSesion.ListaCatalogo);
                        Session.Remove(CVariablesSesion.ListaEntidad);
                        Session.Remove(CVariablesSesion.ListaInfraestructura);
                        Session.Remove(CVariablesSesion.ListaParametrica);
                        Session.Remove(CVariablesSesion.ListaParamGral);
                        Session.Remove(CVariablesSesion.ListaParamInfra);
                        Session.Remove(CVariablesSesion.ReportePrincipal);
                        Session.Remove(CVariablesSesion.TipoReporte);
                        Session.Remove(CVariablesSesion.IdEntidadRevision);

                        Session.Remove(CVariablesSesion.ComboTipoOperacion);


                        Session.Remove(CVariablesSesion.GrillaCabecera);
                        Session.Remove(CVariablesSesion.GrillaDetalle);
                        Session.Remove(CVariablesSesion.GrillaAcumulado);

                        Session[CVariablesSesion.Callback] = "0";

                        MCargaPermisos();
                        MConfigurarFormulario();
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

                    if (dgvTransacciones.DataSource == null && Session[CVariablesSesion.GrillaTransaccion] != null)
                    {
                        dgvTransacciones.DataSource = Session[CVariablesSesion.GrillaTransaccion];
                        dgvTransacciones.DataBind();
                    }
                    else
                    {
                        //todo No es carga inicial: no implementado
                    }

                    if (dgvTransaccionError.DataSource == null && Session[CVariablesSesion.GrillaRegObservado] != null)
                    {
                        dgvTransaccionError.DataSource = Session[CVariablesSesion.GrillaRegObservado];
                        dgvTransaccionError.DataBind();
                    }
                    else
                    {
                        //todo No es carga inicial: no implementado
                    }

                    if (dgvDdjjCab.DataSource == null && Session[CVariablesSesion.GrillaCabecera] != null)
                    {
                        dgvDdjjCab.DataSource = Session[CVariablesSesion.GrillaCabecera];
                        dgvDdjjCab.DataBind();
                    }
                    else
                    {
                        //todo No es carga inicial: no implementado
                    }

                    if (dgvDdjjDet.DataSource == null && Session[CVariablesSesion.GrillaDetalle] != null)
                    {
                        dgvDdjjDet.DataSource = Session[CVariablesSesion.GrillaDetalle];
                        dgvDdjjDet.DataBind();
                    }
                    else
                    {
                        //todo No es carga inicial: no implementado
                    }
                    
                    if (dgvCabecera.DataSource == null && Session[CVariablesSesion.GrillaAcumulado] != null)
                    {
                        dgvCabecera.DataSource = Session[CVariablesSesion.GrillaAcumulado];
                        dgvCabecera.DataBind();
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



                    //cmbPeriodo.Items.Clear();
                    //cmbPeriodo.Text = "";

                    //if (tlsOperadores.FocusedNode != null)
                    //{
                    //    if ((int)tlsOperadores.FocusedNode.GetValue("IntAccion") > 0)
                    //    {
                    //        //PARAMETRICAS GENERALES
                    //        Session.Remove(CVariablesSesion.IdEntidadRevision);
                    //        var objParametros = _client.Post<EResultadoOctanoParametro>(CAppSettings.svc_listar_octano_parametrica,
                    //        new
                    //        {
                    //            strCredencial = CParametrosHydro.strCredencialFuncionario,
                    //            decFiltro = 2,//1: ID_TIPO_ACTIVIDAD 2: ID_TIPO_ACTIVIDAD y VALOR_TIPO
                    //            strParametro1 = "9",//VALOR_TIPO
                    //            strParametro2 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
                    //            strParametro3 = "",
                    //            strParametro4 = ""
                    //        });
                    //        if (objParametros.OResultado.Count>0 && objParametros.IntCodigo == 1)
                    //        {
                    //            Session[CVariablesSesion.IdEntidadRevision] = objParametros.OResultado.FirstOrDefault().VALOR_FIN;

                    //            if (Session[CVariablesSesion.IdEntidadRevision] != null)
                    //            {
                    //                var objEntidadRel = _consulta.ObtenerEntidades(
                    //                new EObjetoEnvio5ParametrosProto
                    //                {
                    //                    decFiltro = 2,
                    //                    strParametro1 = "",
                    //                    strParametro2 = (string)Session[CVariablesSesion.IdEntidadRevision],
                    //                    strParametro3 = CAppSettings.idAplicacion,
                    //                    strParametro4 = "",
                    //                    strParametro5 = ""
                    //                });
                    //                if (objEntidadRel.intCodigo == 1)
                    //                {
                    //                    var lstEntidadRel = (List<O_OBTIENE_ENTIDADES_CTY>)objEntidadRel.oResultado;
                    //                    var objEntidades = _client.Post<EResultadoConfigHidrocarburo>(CAppSettings.svc_listar_autorizacion_condicion,
                    //                    new
                    //                    {
                    //                        strCredencial = CParametrosHydro.strCredencialFuncionario,
                    //                        decFiltro = 3,
                    //                        strParametro1 = Convert.ToInt32(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString()),
                    //                        strParametro2 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
                    //                        strParametro3 = ""
                    //                    });
                    //                    if (objEntidades.IntCodigo == 1)
                    //                    {
                    //                        var lstAlias = objEntidades.OResultado.Where(x => x.DESCRIPCION != null).ToList();
                    //                        if (lstAlias.Any())
                    //                        {
                    //                            foreach (var reg in lstAlias)
                    //                            {
                    //                                lstEntidadRel.Where(w => w.ID_ACTIVIDAD == reg.ID_SUB_IDENTIFICADOR && w.ID_CONSUMIDOR == reg.ID_IDENTIFICADOR).ToList().ForEach(f => f.USUARIO_ASIGNADO = reg.DESCRIPCION);
                    //                            }
                    //                        }
                    //                    }
                    //                    Session[CVariablesSesion.ListaEntidad] = lstEntidadRel;
                    //                }
                    //            }
                    //        }

                    //        var objPeriodo = _client.Post<EResultadoPeriodos>(CAppSettings.svc_lista_periodos_consumidor,
                    //        new
                    //        {
                    //            strCredencial = CParametrosHydro.strCredencialFuncionario,
                    //            decFiltro = 1,
                    //            strParametro1 = tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString(),
                    //            strParametro2 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
                    //            strParametro3 = "",
                    //            strParametro4 = "",
                    //            strParametro5 = tlsOperadores.FocusedNode.GetValue("DecIdDepartamento").ToString()
                    //        });
                    //        if (objPeriodo.IntCodigo == 1)
                    //        {
                    //            int decCod;
                    //            foreach (var dato in objPeriodo.OResultado)
                    //            {
                    //                decCod = Convert.ToInt32(dato.decAño + dato.decMes.ToString().PadLeft(2, '0'));
                    //                cmbPeriodo.Items.Add(dato.strPeriodo, decCod);
                    //            }

                    //            decCod = Convert.ToInt32(objPeriodo.OResultado[0].decAño + objPeriodo.OResultado[0].decMes.ToString().PadLeft(2, '0'));
                    //            cmbPeriodo.Value = decCod;

                    //            var objTipoOperacion = _client.Post<EResultadoTipoOperacion>(CAppSettings.svc_obtener_tipos_operacion_act,
                    //            new
                    //            {
                    //                decFiltro = 1,
                    //                strParametro1 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
                    //                strParametro2 = "",
                    //                strParametro3 = ""
                    //            });
                    //            if (objTipoOperacion.IntCodigo == 1)
                    //            {
                    //                objTipoOperacion.OResultado = objTipoOperacion.OResultado.OrderByDescending(x => x.FACTOR_CALCULO).ThenBy(y => y.NOMBRE).ToList();

                    //                Session[CVariablesSesion.ComboTipoOperacion] = objTipoOperacion.OResultado;
                    //            }
                    //            else
                    //            {
                    //                MImprimirMensaje(objTipoOperacion.StrMensaje, EnumTiposMensajeAlerta.Error);
                    //            }
                    //        }
                    //        else
                    //        {
                    //            MImprimirMensaje(objPeriodo.StrMensaje, EnumTiposMensajeAlerta.Error);
                    //        }
                    //    }
                    //    else
                    //    {
                    //        //todo No implementado.
                    //    }
                    //}
                    //else
                    //{
                    //    //todo No implementado.
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

                    if (tlsOperadores.FocusedNode == null)
                    {
                        MImprimirMensaje("Seleccione un proyecto.", EnumTiposMensajeAlerta.Error);
                        return;
                    }
                    Session[CVariablesSesion.EntidadSeleccionada] = tlsOperadores.FocusedNode.GetValue("StrUbicacion");

                    MMuestraCabecera();
                    MLimpiaGrilla();

                    pgcOpciones.TabPages[0].Visible = false;
                    pgcOpciones.TabPages[1].Visible = false;
                    pgcOpciones.TabPages[2].Visible = false;
                    btnDeclaracion.Visible = false;
                    //btnGrabar.Visible = false;

                    btnImportar.Enabled = false;
                    btnGrabar.Enabled = false;
                    btnLimpiaTo.Enabled = false;

                    if ((int)tlsOperadores.FocusedNode.GetValue("IntAccion") > 0)
                    {
                        var strMensaje = string.Empty;
                        if (MValidaDatosIniciales(ref strMensaje))
                        {
                            if (MValidaCondicionesFuncionamiento(ref strMensaje))
                            {
                                var objTipoOp = (O_TIPOS_OPE_ACT_CTY)Session[CVariablesSesion.ComboTipoOperacion];

                                var x = cmbPeriodo.Text.Split('-');
                                x[0] = x[0].Trim();
                                x[1] = x[1].Trim();

                                //1. obtenerDDJJ -> pestaña 3
                                var lstDeclaraciones = _client.Post<EResultadoListadoDj>(CAppSettings.svc_listar_reporte_datos_principal,
                                new
                                {
                                    strCredencial = CParametrosHydro.strCredencialListadoDj,
                                    decFiltro = 1, //TipoMovimiento (1=ventas)
                                    strParametro1 = tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString(), //Id proyecto/entidad
                                    strParametro2 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(), //id Actividad
                                    strParametro3 = x[0].Substring(3, 4) + x[0].Substring(0, 2) + "01000000", //fecha inicial
                                    strParametro4 = x[1].Substring(3, 4) + x[1].Substring(0, 2) + DateTime.DaysInMonth(Convert.ToInt16(x[1].Substring(3, 4)), Convert.ToInt16(x[1].Substring(0, 2))).ToString().PadLeft(2, '0') + "235959" //fecha final
                                });

                                var lstResultado = lstDeclaraciones.OResultado; 

                                if (lstDeclaraciones.OResultado != null && lstDeclaraciones.OResultado.Any())
                                {
                                    lstResultado = lstDeclaraciones.OResultado.Where(p => p.ID_REPORTE_PRINCIPAL == 21).ToList();
                                }
                                
                                if (lstResultado != null && lstResultado.Any())
                                {
                                    lstResultado.ForEach(a => a.StrEstado = ((a.ESTADO == 1) ? "DECLARADO" : ((a.ESTADO == 2) ? "ANULADO" : ((a.ESTADO == 3) ? "RECEPCIONADO" : "ARCH.ADJUNTO"))));
                                    lstResultado.ForEach(a => a.StrFecha = a.FECHA_REPORTE.ToString(CultureInfo.InvariantCulture).Substring(6, 2) + "/" + a.FECHA_REPORTE.ToString(CultureInfo.InvariantCulture).Substring(4, 2) + "/" + a.FECHA_REPORTE.ToString(CultureInfo.InvariantCulture).Substring(0, 4) + " " + a.FECHA_REPORTE.ToString(CultureInfo.InvariantCulture).Substring(8, 2) + ":" + a.FECHA_REPORTE.ToString(CultureInfo.InvariantCulture).Substring(10, 2) + ":" + a.FECHA_REPORTE.ToString(CultureInfo.InvariantCulture).Substring(12, 2));
                                    lstResultado = lstResultado.OrderBy(y => y.FECHA_REPORTE).ToList();

                                    Session[CVariablesSesion.GrillaAcumulado] = lstResultado;
                                    dgvCabecera.DataSource = lstResultado;
                                    dgvCabecera.DataBind();

                                    var lstVerifica = lstResultado.Where(y => y.ESTADO != 2).ToList();
                                    if (lstVerifica.Any())
                                    {
                                        pgcOpciones.TabPages[1].Visible = true;
                                        pgcOpciones.TabPages[2].Visible = true;
                                        pgcOpciones.TabPages[1].Text = "DECLARACION JURADA - DETALLE";
                                        pgcOpciones.ActiveTabIndex = 2;
                                    }
                                    else
                                    {
                                        pgcOpciones.TabPages[0].Visible = true;
                                        pgcOpciones.TabPages[1].Visible = true;
                                        pgcOpciones.TabPages[2].Visible = true;
                                        pgcOpciones.TabPages[1].Text = "DECLARACION JURADA - EMISION";
                                        btnDeclaracion.Visible = true;
                                        btnGrabar.Enabled = true;
                                        pgcOpciones.ActiveTabIndex = 1;
                                        btnImportar.Enabled = true;
                                        btnLimpiaTo.Enabled = true;
                                    }
                                }
                                else
                                {
                                    pgcOpciones.TabPages[0].Visible = true;
                                    pgcOpciones.TabPages[1].Visible = true;
                                    pgcOpciones.TabPages[1].Text = "DECLARACION JURADA - EMISION";
                                    btnDeclaracion.Visible = true;
                                    btnGrabar.Enabled = true;
                                    pgcOpciones.ActiveTabIndex = 1;
                                    btnImportar.Enabled = true;
                                    btnLimpiaTo.Enabled = true;
                                }
                                var objAcumulado = _consulta.Consolidacion_Siuc(
                                new EObjetoEnvio5ParametrosProto
                                {
                                    strCredencial = CParametrosHydro.strCredencialFuncionario,
                                    decFiltro = 1,
                                    strParametro1 = x[0].Substring(3, 4) + x[0].Substring(0, 2) + "01000000",
                                    strParametro2 = x[1].Substring(3, 4) + x[1].Substring(0, 2) + DateTime.DaysInMonth(Convert.ToInt16(x[1].Substring(3, 4)), Convert.ToInt16(x[1].Substring(0, 2))).ToString().PadLeft(2, '0') + "235959",
                                    strParametro3 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
                                    strParametro4 = tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString(),
                                    strParametro5 = objTipoOp.ID_TIPO_OPERACION.ToString()
                                });
                                if (objAcumulado.intCodigo == 1)
                                {
                                    dgvDdjjCab.DataSource = objAcumulado.oResultado;
                                    dgvDdjjCab.DataBind();
                                    dgvDdjjCab.ExpandAll();
                                    Session[CVariablesSesion.GrillaCabecera] = objAcumulado.oResultado;
                                }
                                else
                                {
                                    pgcOpciones.ActiveTabIndex = 0;
                                }
                            }
                        }
                        else
                        {
                            MImprimirMensaje(strMensaje, EnumTiposMensajeAlerta.Alerta);
                        }
                    }
                    else
                    {
                        MImprimirMensaje("Seleccione un proyecto.", EnumTiposMensajeAlerta.Error);
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
                
            var objParametricaCierre = Session[CVariablesSesion.OctanoParametrica] as List<O_LISTA_OCTANO_PAR_CTY>;
            if (objParametricaCierre.Where(x => x.TIPO_VALOR == 11).ToList().Count>0)
            {
              var strCierreParam = objParametricaCierre.Where(x => x.TIPO_VALOR == 11).ToList().FirstOrDefault().VALOR_FIN;
            //if (!strCierreParam.IsNullOrEmpty())
            //{
                DateTime fechaCierre = DateTime.Parse(strCierreParam);
                if (fechaCierre < Convert.ToDateTime(DateTime.Now.ToShortDateString()))//System.DateTime.Now)
                {
                    MCierrePeriodo();
                }
                else
                { //no implementado
                }
            }
            else
            { 
                //no implementado
            }
            }
            else
            {
                //todo No implementado.
            }
        }

        protected void dgvTransacciones_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            dgvTransacciones.DataSource = null;
            dgvTransacciones.DataBind();
        }

        protected void dgvTransaccionError_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            try
            {
                #region Proceso a Ejecutar

                if (e.VisibleIndex >= 0)
                {
                    if (e.VisibleIndex % 2 == 1)
                    {
                        e.Row.BackColor = Color.LightGray;
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

        protected void dgvTransacciones_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            try
            {
                #region Proceso a Ejecutar

                if (e.VisibleIndex >= 0)
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
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError", "LoadingPanel.Hide();", true);

                                if (dgvTransaccionError.VisibleRowCount > 0)
                                {
                                    Session[CVariablesSesion.ArchivoDescarga] = dgvTransaccionError.DataSource;
                                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( 'WFDescarga.aspx', '')", true);
                                }
                                else
                                {
                                    //var lstRegistros = Session[CVariablesSesion.GrillaTransaccion] as List<CRegistrosImportados>;
                                    //if (lstRegistros != null && lstRegistros.Any())
                                    //{
                                    //    var strFechaMin = lstRegistros.Min(x => x.StrFechaEmision);
                                    //    var strFechaMax = lstRegistros.Max(x => x.StrFechaEmision);
                                    //    decimal decFechaIni = Convert.ToDecimal(strFechaMin.Substring(6, 4) + strFechaMin.Substring(3, 2) + strFechaMin.Substring(0, 2));
                                    //    decimal decFechaFin = Convert.ToDecimal(strFechaMax.Substring(6, 4) + strFechaMax.Substring(3, 2) + strFechaMax.Substring(0, 2));
                                    //    //Verifica si existen movimientos registrados en el periodo un tipo de operacion
                                    //    var objValidacion = _client.Post<EResultadoGenerico>(CAppSettings.svc_validaciones_general,
                                    //    new
                                    //    {
                                    //        strCredencial = CParametrosHydro.strCredencialFuncionario,
                                    //        strFiltro = "2",
                                    //        strParametro1 = tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString(),
                                    //        strParametro2 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
                                    //        strParametro3 = "",
                                    //        strParametro4 = decFechaIni,
                                    //        strParametro5 = decFechaFin,
                                    //        strParametro6 = "",
                                    //        decAppFechaRegistro = 0,
                                    //        decAppIdUsuario = 0
                                    //    });
                                    //    if (objValidacion.IntCodigo == 1)
                                    //    {
                                    //        if (objValidacion.OResultado.RESULTADO != 1)
                                    //        {
                                    //            ScriptManager.RegisterStartupScript(this, typeof(string), "showError", "if (confirm('Existe " + objValidacion.OResultado.ID_TABLA + " movimiento(s) registrado(s) entre las fechas que se importarán, si continua el proceso se ELIMINARAN los anteriores. ¿Desea ir a la pantalla Detalle de Movimientos para revisarlos?')){window.location.href = 'WFFlujoOperador.aspx';}", true);
                                    //        }
                                    //        else
                                    //        {
                                    //            //todo No Implementado.
                                    //        }
                                    //    }
                                    //    else
                                    //    {
                                    //        MImprimirMensaje(objValidacion.StrMensaje, EnumTiposMensajeAlerta.Error);
                                    //    }
                                    //}
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

                        MMuestraCabecera();
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

                        var lstRegistros = Session[CVariablesSesion.GrillaTransaccion] as List<CRegistrosImportados>;
                        var lstTiOp = lstRegistros.GroupBy(x => new { x.DecIdTipoOp, x.DecOrden })
                       .Select(g => new { g.Key.DecIdTipoOp, g.Key.DecOrden })
                       .OrderBy(d => d.DecOrden).ToList();

                        foreach (var tipo in lstTiOp)
                        {
                            long lngNoMovimientos = 1;
                            var lstReg = lstRegistros.Where(x => x.DecIdTipoOp == tipo.DecIdTipoOp).ToList();

                            if (lstReg != null && lstReg.Any())
                            {
                                var lstProtoMovimientos = new List<ObjProtoMovimientoTransaccion>();
                                var strFechaMinAux = lstReg.Min(x => Convert.ToDateTime(x.StrFechaEmision));
                                var strFechaMaxAux = lstReg.Max(x => Convert.ToDateTime(x.StrFechaEmision));
                                
                                var strFechaMin=strFechaMinAux.ToString();
                                var strFechaMax = strFechaMaxAux.ToString();

                                var decFechaIni = Convert.ToDecimal(strFechaMin.Substring(6, 4) + strFechaMin.Substring(3, 2) + strFechaMin.Substring(0, 2));
                                var decFechaFin = Convert.ToDecimal(strFechaMax.Substring(6, 4) + strFechaMax.Substring(3, 2) + strFechaMax.Substring(0, 2));

                                var tareasMovimientos = _gestion.TareasMovimientos(1, 5, Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString()), Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()), tipo.DecIdTipoOp, decFechaIni, decFechaFin);
                                if (tareasMovimientos.intCodigo == 1)
                                {
                                    int intControl = 1;
                                    foreach (var registro in lstReg)
                                    {
                                        if (intControl == 0)
                                        {
                                            lstProtoMovimientos = new List<ObjProtoMovimientoTransaccion>();
                                            intControl++;
                                        }
                                        else
                                        {
                                            //todo No implementado.
                                        }

                                        ObjProtoMovimientoTransaccion objProtoReg = registro.objProtoMovimiento;
                                        lstProtoMovimientos.Add(objProtoReg);

                                        if (lstProtoMovimientos.Count == 2000)
                                        {
                                            intControl = 0;
                                            MEnviaObjeto(lstProtoMovimientos, (lngNoMovimientos == lstReg.Count), lngNoMovimientos, decFechaIni, decFechaFin, tipo.DecIdTipoOp, tipo.DecOrden);

                                            if (lngNoMovimientos == lstReg.Count)
                                            {
                                                lstProtoMovimientos = new List<ObjProtoMovimientoTransaccion>();
                                            }
                                            else
                                            {
                                                //todo No implementado.
                                            }
                                        }
                                        else
                                        {
                                            intControl++;
                                        }
                                        lngNoMovimientos++;
                                    }

                                    if (lstProtoMovimientos.Count > 0)
                                    {
                                        MEnviaObjeto(lstProtoMovimientos, true, lngNoMovimientos - 1, decFechaIni, decFechaFin, tipo.DecIdTipoOp, tipo.DecOrden);
                                        string script = @"<script type='text/javascript'>
                                                              pnlDatos.PerformCallback();
                                                          </script>";

                                        ScriptManager.RegisterStartupScript(this, typeof(Page), "pnlDatosIR", script, false);
                                    }
                                    else
                                    {
                                        //todo No implementado.
                                    }
                                }
                                else
                                {
                                    MImprimirMensaje(tareasMovimientos.strMensaje, EnumTiposMensajeAlerta.Error);
                                }
                            }
                            else
                            {
                                MImprimirMensaje("No Existen Registros.", EnumTiposMensajeAlerta.Error);
                            }

                        }




                        //return;
                        //if (lstRegistros != null && lstRegistros.Any())
                        //{
                        //    var lstProtoMovimientos = new List<ObjProtoMovimientoTransaccion>();
                        //    var strFechaMin = lstRegistros.Min(x => x.StrFechaEmision);
                        //    var strFechaMax = lstRegistros.Max(x => x.StrFechaEmision);
                        //    decimal decFechaIni = Convert.ToDecimal(strFechaMin.Substring(6, 4) + strFechaMin.Substring(3, 2) + strFechaMin.Substring(0, 2));
                        //    decimal decFechaFin = Convert.ToDecimal(strFechaMax.Substring(6, 4) + strFechaMax.Substring(3, 2) + strFechaMax.Substring(0, 2));

                        //    var tareasMovimientos = _gestion.TareasMovimientos(1, 5, Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString()), Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()), Convert.ToDecimal(cmbEntidadExt.Value), decFechaIni, decFechaFin);
                        //    if (tareasMovimientos.intCodigo == 1)
                        //    {
                        //        int intControl = 1;
                        //        foreach (var registro in lstRegistros)
                        //        {
                        //            if (intControl == 0)
                        //            {
                        //                lstProtoMovimientos = new List<ObjProtoMovimientoTransaccion>();
                        //                intControl++;
                        //            }
                        //            else
                        //            {
                        //                //todo No implementado.
                        //            }

                        //            ObjProtoMovimientoTransaccion objProtoReg = registro.objProtoMovimiento;
                        //            lstProtoMovimientos.Add(objProtoReg);

                        //            if (lstProtoMovimientos.Count == 2000)
                        //            {
                        //                intControl = 0;
                        //                MEnviaObjeto(lstProtoMovimientos, (lngNoMovimientos == lstRegistros.Count), lngNoMovimientos, decFechaIni, decFechaFin, Convert.ToDecimal(cmbEntidadExt.Value));

                        //                if (lngNoMovimientos == lstRegistros.Count)
                        //                {
                        //                    lstProtoMovimientos = new List<ObjProtoMovimientoTransaccion>();
                        //                }
                        //                else
                        //                {
                        //                    //todo No implementado.
                        //                }
                        //            }
                        //            else
                        //            {
                        //                intControl++;
                        //            }
                        //            lngNoMovimientos++;
                        //        }

                        //        if (lstProtoMovimientos.Count > 0)
                        //        {
                        //            MEnviaObjeto(lstProtoMovimientos, true, lngNoMovimientos - 1, decFechaIni, decFechaFin, Convert.ToDecimal(cmbEntidadExt.Value));
                        //        }
                        //        else
                        //        {
                        //            //todo No implementado.
                        //        }
                        //    }
                        //    else
                        //    {
                        //        MImprimirMensaje(tareasMovimientos.strMensaje,EnumTiposMensajeAlerta.Error);
                        //    }
                        //}
                        //else
                        //{
                        //    MImprimirMensaje("No Existen Registros.", EnumTiposMensajeAlerta.Error);
                        //}
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

        protected void btnDescargar_Click(object sender, EventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    if (cmbEntExterna.Text.Length == 0)
                    {
                        MImprimirMensaje("Seleccione la entidad externa.", EnumTiposMensajeAlerta.Alerta);
                    }
                    else
                    {
                        var intTipoEnt = cmbEntExterna.Value.ToString();
                        string fileName;

                        switch (intTipoEnt)
                        {
                            case "EGCSES":
                                fileName = "plantilla_recaudo_suic.xlsx";
                                break;
                            default:
                                MImprimirMensaje("Entidad Externa Invalida.", EnumTiposMensajeAlerta.Error);
                                return;
                        }
                        try
                        {
                            var response = HttpContext.Current.Response;
                            response.ClearContent();
                            response.Clear();
                            response.ContentType = "application/excel";
                            response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ";");
                            response.TransmitFile(Server.MapPath("~/Sitio/Plantillas/" + fileName));
                            response.Flush();
                            response.End();
                        }
                        catch (Exception ex)
                        {

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

        protected void btnLimpiaTo_Click(object sender, EventArgs e)
        {
            try
            {
                #region Proceso a Ejecutar
                var objTipoOp = (O_TIPOS_OPE_ACT_CTY)Session[CVariablesSesion.ComboTipoOperacion];
                var x = cmbPeriodo.Value.ToString().Split('-');
                var decFechaIni = Convert.ToInt64(x[0].Substring(3, 4) + x[0].Substring(0, 2) + "01");
                var decFechaFin = Convert.ToInt64(x[1].Substring(3, 4) + x[1].Substring(0, 2) + DateTime.DaysInMonth(Convert.ToInt16(x[1].Substring(3, 4)), Convert.ToInt16(x[1].Substring(0, 2))).ToString().PadLeft(2, '0'));

                var tareasMovimientos = _gestion.TareasMovimientos(
                    1,
                    5,
                    Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString()),
                    Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()),
                    objTipoOp.ID_TIPO_OPERACION,
                    decFechaIni,
                    decFechaFin);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError", "LoadingPanel.Hide();", true);

                if (tareasMovimientos.intCodigo != 1)
                {
                    MImprimirMensaje(tareasMovimientos.strMensaje, EnumTiposMensajeAlerta.Error);
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

        protected void dgvDdjjDet_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            Session.Remove(CVariablesSesion.GrillaDetalle);
            dgvDdjjDet.DataSource = null;
            dgvDdjjDet.DataBind();
            var objTipoOp = (O_TIPOS_OPE_ACT_CTY)Session[CVariablesSesion.ComboTipoOperacion];

            var index = dgvDdjjCab.FocusedRowIndex;
            var strMes = dgvDdjjCab.GetRowValues(index, "MES").ToString();

            var objResultado = _consulta.Consolidacion_Siuc(
            new EObjetoEnvio5ParametrosProto
            {
                strCredencial = CParametrosHydro.strCredencialFuncionario,
                decFiltro = 2,
                strParametro1 = strMes + "01000000",
                strParametro2 = strMes + DateTime.DaysInMonth(Convert.ToInt16(strMes.Substring(0, 4)), Convert.ToInt16(strMes.Substring(4, 2))).ToString().PadLeft(2, '0') + "235959",
                strParametro3 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
                strParametro4 = tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString(),
                strParametro5 = objTipoOp.ID_TIPO_OPERACION.ToString()
            });
            if (objResultado.intCodigo == 1)
            {
                dgvDdjjDet.DataSource = objResultado.oResultado;
                dgvDdjjDet.DataBind();

                Session[CVariablesSesion.GrillaDetalle] = objResultado.oResultado;
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

                    var strMensaje = string.Empty;
                    if (MValidaDatos(ref strMensaje))
                    {
                        List<O_CONSOLIDACION_SUIC_ACU_CTY> lstGrilla;

                        lstGrilla = (List<O_CONSOLIDACION_SUIC_ACU_CTY>)Session[CVariablesSesion.GrillaCabecera];
                        //if (lstGrilla != null)
                        //{
                        var datFechaReg = DateTime.Now;
                        var x = cmbPeriodo.Value.ToString().Split('-');

                        var objNuevaDeclaracion = new EGuardarDeclaracionJurada();
                        objNuevaDeclaracion.decFechaRegistro = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                        objNuevaDeclaracion.decIdCitePadre = 0;
                        objNuevaDeclaracion.decIdOpcion = 8;
                        objNuevaDeclaracion.decIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                        objNuevaDeclaracion.strCodigoFormularioFac = tlsOperadores.FocusedNode.GetValue("StrNroHydro").ToString().Trim();
                        objNuevaDeclaracion.strCodigoFormularioMov = "UD/AB-RDJ-P02-F01";
                        objNuevaDeclaracion.decIdMaxMovimiento = 0;
                        objNuevaDeclaracion.strCredencial = CParametrosHydro.strCredencialFuncionario;
                        objNuevaDeclaracion.strCredencialFuncionario = CParametrosHydro.strCredencialDeclaracion;
                        objNuevaDeclaracion.strDpto = "";
                        objNuevaDeclaracion.strReferencia = "DECLARACION JURADA - CONSOLIDACION SUIC";
                        objNuevaDeclaracion.strSigla = "";
                        objNuevaDeclaracion.cabecera = new O_LISTA_REPORTE_DATOSP_CTY();
                        var objCabecera = new O_LISTA_REPORTE_DATOSP_CTY();
                        objCabecera.ID_REPORTE_DATO_PRINC = 0;

                        objCabecera.FECHA_INICIO = Convert.ToInt64(x[0].Substring(3, 4) + x[0].Substring(0, 2) + "01000000");
                        objCabecera.FECHA_FIN = Convert.ToInt64(x[1].Substring(3, 4) + x[1].Substring(0, 2) + DateTime.DaysInMonth(Convert.ToInt16(x[1].Substring(3, 4)), Convert.ToInt16(x[1].Substring(0, 2))).ToString().PadLeft(2, '0') + "235959");

                        objCabecera.FECHA_REPORTE = CFechas.ConvierteDateTimeLong(datFechaReg);
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
                        //var formatoFecha = CultureInfo.CurrentCulture.DateTimeFormat;
                        //objCabecera.MES = formatoFecha.GetMonthName(Convert.ToInt32(cmbPeriodo.Value.ToString().Substring(4, 2))).ToUpper() + " - " + cmbPeriodo.Value.ToString().Substring(0, 4);
                        objCabecera.MES = cmbPeriodo.Text;
                        objCabecera.LUGAR_REGISTRO = cmbDepartamento.Text;
                        objCabecera.ID_TIPO_ACTIVIDAD = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                        objCabecera.MUNICIPIO = tlsOperadores.FocusedNode.GetValue("StrMunicipio") + "/" + tlsOperadores.FocusedNode.GetValue("StrLocalidad");                        
                        objNuevaDeclaracion.cabecera = objCabecera;

                        var listados = new O_DECLARACION_JURADA_CTY();
                        listados.LstaConsSuic = lstGrilla;

                        objNuevaDeclaracion.listaDeclaracion = listados;

                        var resultado = _client.Post<EResultadoGenerico>(CAppSettings.svc_guardar_declaracion_jurada, objNuevaDeclaracion);
                        if (resultado.IntCodigo == 1)
                        {
                            MImprimeDeclaracion(datFechaReg, resultado.StrMensaje.Trim());
                            MLimpiaGrilla();
                            pucDatos.ShowOnPageLoad = false;
                            //pnlDatos_Callback(null, null);

                            string script = @"<script type='text/javascript'>
                                               pnlDatos.PerformCallback();
                                             </script>";

                            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", script, false);
                        }
                        else
                        {
                            MPuImprimirMensaje(resultado.StrMensaje, EnumTiposMensajeAlerta.Error);
                            dgvDdjjCab.DataSource = null;
                            dgvDdjjCab.DataBind();
                        }
                        //}
                        //else
                        //{
                        //    MPuImprimirMensaje("No existen registros.", EnumTiposMensajeAlerta.Alerta);
                        //}
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

        protected void pucDatos_WindowCallback(object source, PopupWindowCallbackArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar
                    MPuImprimirMensaje("", EnumTiposMensajeAlerta.Limpiar);

                    var lstDeptos = (List<O_UBICACION_CTY>)Session[CVariablesSesion.ListaDepartamentos];
                    cmbDepartamento.DataSource = lstDeptos;
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

        protected void dgvCabecera_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    var index = dgvCabecera.FocusedRowIndex;
                    var lngidCite = Convert.ToInt64(dgvCabecera.GetRowValues(index, "ID_CITE"));
                    var intEstado = Convert.ToInt32(dgvCabecera.GetRowValues(index, "ESTADO"));
                    var decAppIdUs = Convert.ToDecimal(dgvCabecera.GetRowValues(index, "APP_ID_USUARIO"));
                    var decFechaRep = Convert.ToDecimal(dgvCabecera.GetRowValues(index, "FECHA_REPORTE"));

                    if (lngidCite > 0)
                    {
                        var datFechaReg = DateTime.Now;
                        EResultado resultado;
                        switch (e.ButtonID)
                        {

                            case "btnAnulaUor":
                                if (intEstado != 2)
                                {
                                    resultado = _client.Post<EResultado>(CAppSettings.svc_actualiza_reporte_estado,
                                    new
                                    {
                                        strCredencial = CParametrosHydro.strCredencialFuncionario,
                                        decFiltro = 3,
                                        decIdReporteDatoPrin = 0,
                                        decIdCite = lngidCite,
                                        decEstado = 2,
                                        decFechaReporte = decFechaRep,
                                        strObservaciones = "",
                                        decAppFechaRegistro = decFechaRep,
                                        decAppIdUsuario = decAppIdUs
                                    });
                                    if (resultado.intCodigo != 1)
                                    {
                                        MImprimirMensaje(resultado.strMensaje, EnumTiposMensajeAlerta.Alerta);
                                    }
                                    else
                                    {
                                        pnlDatos_Callback(null, null);
                                    }
                                }
                                else
                                {
                                    MImprimirMensaje("No puede ejecutar la función debido a que la declaración se encuentra anulada.", EnumTiposMensajeAlerta.Alerta);
                                }
                                break;
                            case "btnAnular":
                                if (intEstado != 2)
                                {
                                    resultado = _client.Post<EResultado>(CAppSettings.svc_actualiza_reporte_estado,
                                    new
                                    {
                                        strCredencial = CParametrosHydro.strCredencialFuncionario,
                                        decFiltro = 2,
                                        decIdReporteDatoPrin = 0,
                                        decIdCite = lngidCite,
                                        decEstado = 2,
                                        decFechaReporte = CFechas.ConvierteDateTimeLong(datFechaReg),//Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0')),
                                        strObservaciones = "",
                                        decAppFechaRegistro = CFechas.ConvierteDateTimeLong(datFechaReg),//Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0')),
                                        decAppIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario])
                                    });
                                    if (resultado.intCodigo != 1)
                                    {
                                        MImprimirMensaje(resultado.strMensaje, EnumTiposMensajeAlerta.Alerta);
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError", "alert('Seleccione un proyecto.');", true);
                                        pnlDatos_Callback(null, null);
                                    }
                                }
                                else
                                {
                                    MImprimirMensaje("No puede ejecutar la función debido a que la declaración se encuentra anulada.", EnumTiposMensajeAlerta.Alerta);
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

        protected void pucHistorial_WindowCallback(object source, PopupWindowCallbackArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    int index = dgvCabecera.FocusedRowIndex;
                    txtCiteDj.Text = dgvCabecera.GetRowValues(index, "CITE_GENERADO").ToString();

                    var lstDeclaraciones = _client.Post<EResultadoListadoHistorialDj>
                        (CAppSettings.svc_listar_historial_reporte_datos_principal,
                        new
                        {
                            strCredencial = CParametrosHydro.strCredencialListadoDj,
                            decFiltro = "1", //TipoMovimiento (1=ventas)
                            strParametro1 = dgvCabecera.GetRowValues(index, "ID_REPORTE_DATO_PRINC").ToString().Trim(), //Id proyecto/entidad
                            strParametro2 = "0",
                            strParametro3 = "0",
                            strParametro4 = "0"
                        });
                    if (lstDeclaraciones.IntCodigo == 1)
                    {
                        var LstResultado = new List<O_HISTORIAL_REPORTE_DATOSP_CTY>();
                        LstResultado = lstDeclaraciones.OResultado;

                        if (LstResultado.Any())
                        {
                            LstResultado.ForEach(a => a.strFecha = a.APP_FECHA_REGISTRO.ToString(CultureInfo.InvariantCulture).Substring(6, 2) + "/" + a.APP_FECHA_REGISTRO.ToString(CultureInfo.InvariantCulture).Substring(4, 2) + "/" + a.APP_FECHA_REGISTRO.ToString(CultureInfo.InvariantCulture).Substring(0, 4) + " " + a.APP_FECHA_REGISTRO.ToString(CultureInfo.InvariantCulture).Substring(8, 2) + ":" + a.APP_FECHA_REGISTRO.ToString(CultureInfo.InvariantCulture).Substring(10, 2) + ":" + a.APP_FECHA_REGISTRO.ToString(CultureInfo.InvariantCulture).Substring(12, 2));
                            LstResultado.ForEach(a => a.strEstado = ((a.ESTADO == 1) ? "DECLARADO" : ((a.ESTADO == 2) ? "ANULADO" : ((a.ESTADO == 3) ? "RECEPCIONADO" : "ARCH.ADJUNTO"))));
                            LstResultado = LstResultado.OrderBy(a => a.APP_FECHA_REGISTRO).ToList();

                            Session[CVariablesSesion.GrillaHistorial] = LstResultado;
                            dgvHistorico.DataSource = LstResultado;
                            dgvHistorico.DataBind();
                        }
                        else
                        {
                            //todo No implementado.
                        }
                    }
                    else
                    {
                        MImprimirMensaje(lstDeclaraciones.StrMensaje, EnumTiposMensajeAlerta.Alerta);
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

        }

        protected void pucCargaArchivo_WindowCallback(object source, PopupWindowCallbackArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    txtPcObservacion.Text = "";
                    int index = dgvCabecera.FocusedRowIndex;
                    if (index >= 0)
                    {
                        txtPcCiteDj.Text = dgvCabecera.GetRowValues(index, "CITE_GENERADO").ToString();
                        Session["PATH"] = dgvCabecera.GetRowValues(index, "ID_REPORTE_DATO_PRINC") + "|" + dgvCabecera.GetRowValues(index, "ID_CITE");
                    }
                    else
                    {
                        pucCargaArchivo.ShowOnPageLoad = false;
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

        protected void uctCargaDj_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    if (e.IsValid)
                    {
                        var strAuxiliar = Session["PATH"].ToString();
                        var astAuxiliar = strAuxiliar.Split('|');
                        decimal decIdReporte = Convert.ToDecimal(astAuxiliar[0]);
                        decimal decIdCite = Convert.ToDecimal(astAuxiliar[1]);
                        var strRutaServer = Path.GetTempPath();
                        var strNombreArchivo = DateTime.Now.Year + "" + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second;
                        Session["PATH"] = strRutaServer + strNombreArchivo;

                        e.UploadedFile.SaveAs((string)Session["PATH"]);
                        byte[] bytes = File.ReadAllBytes((string)Session["PATH"]);

                        var datFechaReg = DateTime.Now;
                        EResultado resultado = _client.Post<EResultado>(CAppSettings.svc_documento_digital,
                        new
                        {
                            strCredencial = CParametrosHydro.strCredencialFuncionario,
                            byteDocumento = bytes,
                            decEstado = 1,
                            strBinario = "00000000000000000000",
                            strObservaciones = txtPcObservacion.Text,
                            decIdTipoDocDigital = 1,
                            //decIdAutorizacionConsumidor = null,
                            decIdReporteDatoPrinc = decIdReporte,
                            decAppFechaRegistro = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0')),
                            decAppIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario])
                        });
                        if (resultado.intCodigo != 1)
                        {
                            MImprimirMensaje(resultado.strMensaje, EnumTiposMensajeAlerta.Alerta);
                        }
                        else
                        {
                            resultado = _client.Post<EResultado>(CAppSettings.svc_actualiza_reporte_estado,
                            new
                            {
                                strCredencial = CParametrosHydro.strCredencialFuncionario,
                                decFiltro = 2,
                                decIdReporteDatoPrin = 0,
                                decIdCite,
                                decEstado = 4,
                                decFechaReporte = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0')),
                                strObservaciones = txtPcObservacion.Text,
                                decAppFechaRegistro = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0')),
                                decAppIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario])
                            });
                            if (resultado.intCodigo != 1)
                            {
                                MImprimirMensaje(resultado.strMensaje, EnumTiposMensajeAlerta.Alerta);
                            }
                            else
                            {
                                //todo No implementado.
                            }
                            txtPcObservacion.Text = "";
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

        protected void pucAdjuntos_WindowCallback(object source, PopupWindowCallbackArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    Session.Remove(CVariablesSesion.GrillaAuxiliar);
                    dgvAdjuntos.DataSource = null;
                    dgvAdjuntos.DataBind();

                    var index = dgvCabecera.FocusedRowIndex;
                    txtPaCiteDj.Text = dgvCabecera.GetRowValues(index, "CITE_GENERADO").ToString();
                    var intEstado = Convert.ToInt32(dgvCabecera.GetRowValues(index, "ESTADO"));
                    dgvAdjuntos.Columns[(int)EnumColumnasGrilla.FoAbm].Visible = intEstado != 2;

                    var lstDeclaraciones = _client.Post<EResultadoAdjuntos>
                        (CAppSettings.svc_lista_documentos_digital,
                        new
                        {
                            strCredencial = CParametrosHydro.strCredencialListadoDj,
                            decFiltro = "1",
                            strParametro1 = dgvCabecera.GetRowValues(index, "ID_REPORTE_DATO_PRINC").ToString().Trim(),
                            strParametro2 = "0",
                            strParametro3 = "0"
                        });
                    if (lstDeclaraciones.IntCodigo == 1)
                    {
                        var LstResultado = new List<O_LISTA_DOCUMENTO_DIGITAL_CTY>();
                        LstResultado = lstDeclaraciones.OResultado;
                        if (LstResultado.Any())
                        {
                            Session[CVariablesSesion.GrillaAuxiliar] = LstResultado;
                            dgvAdjuntos.DataSource = LstResultado;
                            dgvAdjuntos.DataBind();
                        }
                        else
                        {
                            //todo No implementado.
                        }
                    }
                    else
                    {
                        MImprimirMensaje(lstDeclaraciones.StrMensaje, EnumTiposMensajeAlerta.Alerta);
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

        protected void dgvAdjuntos_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    int index = dgvAdjuntos.FocusedRowIndex;
                    long lngIdDocDigital = Convert.ToInt64(dgvAdjuntos.GetRowValues(index, "ID_DOCUMENTO_DIGITAL"));
                    if (lngIdDocDigital > 0)
                    {
                        var datFechaReg = DateTime.Now;
                        EResultado resultado;
                        switch (e.ButtonID)
                        {
                            case "btnEliminar":
                                resultado = _client.Post<EResultado>(CAppSettings.svc_actualiza_estado_doc_digital,
                                new
                                {
                                    strCredencial = CParametrosHydro.strCredencialFuncionario,
                                    decFiltro = 1,
                                    decIdDocumentoDigital = lngIdDocDigital,
                                    decEstado = 2,
                                    strObservaciones = "",
                                    decAppFechaRegistro = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0')),
                                    decAppIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario])
                                });
                                if (resultado.intCodigo != 1)
                                {
                                    MImprimirMensaje(resultado.strMensaje, EnumTiposMensajeAlerta.Alerta);
                                }
                                else
                                {
                                    pucAdjuntos_WindowCallback(null, null);
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

        protected void dgvAdjuntos_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    int index = dgvAdjuntos.FocusedRowIndex;
                    long lngIdDocDigital = Convert.ToInt64(dgvAdjuntos.GetRowValues(index, "ID_DOCUMENTO_DIGITAL"));

                    if (index >= 0 && lngIdDocDigital > 0)
                    {
                        switch (e.CommandArgs.CommandName)
                        {
                            case "lnkDescargar":
                                var bytDocumento = (byte[])dgvAdjuntos.GetRowValues(index, "DOCUMENTO");
                                Response.Clear();
                                Response.AddHeader("Content-Length", bytDocumento.Length.ToString(CultureInfo.InvariantCulture));
                                Response.ContentType = "application/pdf";
                                Response.AddHeader("Expires", "0");
                                Response.AddHeader("Cache-Control", "must-revalidate, post-check=0, pre-check=0");
                                Response.AddHeader("Pragma", "public");
                                Response.AddHeader("Content-Disposition", "attachment; filename=" + txtPaCiteDj.Text.Trim() + ".pdf");
                                Response.BinaryWrite(bytDocumento);
                                Response.Flush();
                                Response.Close();
                                break;
                            default:
                                break;
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

        protected void dgvAdjuntos_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
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

        protected void btnReimprimir_Click(object sender, EventArgs e)
        {
            var objParametricas = Session[CVariablesSesion.OctanoParametrica] as List<O_LISTA_OCTANO_PAR_CTY>;
            var strTextoCab = objParametricas.Where(x => x.TIPO_VALOR == 8).ToList().FirstOrDefault().VALOR_FIN;
            var vecCabecera = strTextoCab.Split('|');

            var index = dgvCabecera.FocusedRowIndex;
            if (index < 0) {
                string scriptV1 = @"<script type='text/javascript'>
                               pnlDatos.PerformCallback();
                             </script>";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "pnlDatos_ri", scriptV1, false);
                return;}

            var strJsonReportes = dgvCabecera.GetRowValues(index, "JSON_REPORTE_DATOS").ToString();
            var objDdjj = JsonSerializer.DeserializeFromString<O_DECLARACION_JURADA_CTY>(strJsonReportes);

            Session[CVariablesSesion.TipoReporte] = EnumTipoReporte.DdjjConsolidaSuic;
            var xreAcumulado = new repDjConsolidacion();
            xreAcumulado.xtcCabCodigo.Text = vecCabecera[0] + " ANEXO 1";
            xreAcumulado.xtcCabVersion.Text = vecCabecera[1];
            xreAcumulado.xtcCabAprobado.Text = vecCabecera[2];

            xreAcumulado.xtcRazonSocial.Text = dgvCabecera.GetRowValues(index, "RAZON_SOCIAL").ToString();
            xreAcumulado.xtcActividad.Text = tlsOperadores.FocusedNode.GetValue("StrActividad").ToString();
            xreAcumulado.xtcDireccion.Text = dgvCabecera.GetRowValues(index, "DIRECCION").ToString();
            xreAcumulado.xtcDepartamento.Text = tlsOperadores.FocusedNode.GetValue("StrDepartamento").ToString();
            xreAcumulado.xtcUrbano.Text = dgvCabecera.GetRowValues(index, "AMBITO_OPERACION").ToString();
            xreAcumulado.xtcTelefono.Text = dgvCabecera.GetRowValues(index, "TELEFONO").ToString();
            xreAcumulado.xtcMunicipio.Text = dgvCabecera.GetRowValues(index, "MUNICIPIO").ToString();
            xreAcumulado.xtcPublico.Text = (dgvCabecera.GetRowValues(index, "TIPO_SOCIEDAD") == null) ? "" : dgvCabecera.GetRowValues(index, "TIPO_SOCIEDAD").ToString();
            xreAcumulado.xtcMes.Text = dgvCabecera.GetRowValues(index, "MES").ToString();
            xreAcumulado.xtcLugar.Text = "LUGAR :   " + dgvCabecera.GetRowValues(index, "LUGAR_REGISTRO");
            xreAcumulado.xrlDia.Text = dgvCabecera.GetRowValues(index, "APP_FECHA_REGISTRO").ToString().Substring(6, 2);
            xreAcumulado.xrlMes.Text = dgvCabecera.GetRowValues(index, "APP_FECHA_REGISTRO").ToString().Substring(4, 2);
            xreAcumulado.xrlGestion.Text = dgvCabecera.GetRowValues(index, "APP_FECHA_REGISTRO").ToString().Substring(0, 4);
            xreAcumulado.xrlResponsable.Text = (dgvCabecera.GetRowValues(index, "NOMBRES_REPLEGAL") == null) ? "" : dgvCabecera.GetRowValues(index, "NOMBRES_REPLEGAL").ToString();
            xreAcumulado.xtcCiResponsable.Text = "C.I.: " + dgvCabecera.GetRowValues(index, "CI_REPLEGAL");
            xreAcumulado.xrlUsuarioEmisor.Text = dgvCabecera.GetRowValues(index, "NOMBRES_USUARIO").ToString();
            xreAcumulado.xtcCiUsuarioEmisor.Text = "C.I.: " + dgvCabecera.GetRowValues(index, "CI_USUARIO");
            xreAcumulado.xtcNoCite.Text = dgvCabecera.GetRowValues(index, "CITE_GENERADO").ToString();
            xreAcumulado.xbcCodigoQr.Text = dgvCabecera.GetRowValues(index, "CITE_GENERADO") + "|" + cmbPeriodo.Value + "|" + xreAcumulado.xrlGestion.Text + xreAcumulado.xrlMes.Text + xreAcumulado.xrlDia.Text;

            xreAcumulado.DataSource = objDdjj.LstaConsSuic;
            Session[CVariablesSesion.ReportePrincipal] = xreAcumulado;

            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( 'WFReporte.aspx', '')", true);
            string script = @"<script type='text/javascript'>
                               pnlDatos.PerformCallback();
                             </script>";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "pnlDatos_ri", script, false);
        }

        protected void dgvDdjjCab_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            try
            {
                #region Proceso a Ejecutar

                if (e.VisibleIndex < 0) return;

                /*if (e.VisibleIndex % 2 == 1)
                {
                    e.Row.BackColor = Color.FromArgb(204, 209, 182);
                }*/

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

        protected void dgvDdjjDet_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            try
            {
                #region Proceso a Ejecutar

                if (e.VisibleIndex < 0) return;

                if (e.VisibleIndex % 2 == 1)
                {
                    e.Row.BackColor = Color.FromArgb(253, 237, 212);
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

        protected void dgvCabecera_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            try
            {
                #region Proceso a Ejecutar

                if (e.VisibleIndex >= 0)
                {
                    switch (dgvCabecera.GetRowValues(e.VisibleIndex, "ESTADO").ToString())
                    {
                        case "2":
                            e.Row.BackColor = Color.FromArgb(215, 135, 135);
                            break;
                        case "3":
                            e.Row.BackColor = Color.FromArgb(143, 179, 157);
                            break;
                        case "4":
                            e.Row.BackColor = Color.FromArgb(171, 209, 184);
                            break;
                        default:
                            e.Row.BackColor = Color.FromArgb(175, 207, 167);
                            break;
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

        protected void dgvCabecera_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
        {
            if (!MValidaSesion()) return;
            try
            {
                #region Proceso a Ejecutar

                if (e.VisibleIndex == -1) return;

                if (e.ButtonID == "btnPdf")
                {
                    e.Visible = Convert.ToInt16(Session[CVariablesSesion.PermisoCons]) == 1 ? DefaultBoolean.True : DefaultBoolean.False;
                }
                if (e.ButtonID == "btnAdjuntos")
                {
                    e.Visible = Convert.ToInt16(Session[CVariablesSesion.PermisoCons]) == 1 ? DefaultBoolean.True : DefaultBoolean.False;
                }
                if (e.ButtonID == "btnAnular")
                {
                    e.Visible = Convert.ToInt16(Session[CVariablesSesion.PermisoRevi]) == 1 ? DefaultBoolean.True : DefaultBoolean.False;
                }
                if (e.ButtonID == "btnAnulaUor")
                {
                    e.Visible = Convert.ToInt16(Session[CVariablesSesion.PermisoApro]) == 1 ? DefaultBoolean.True : DefaultBoolean.False;
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
        /// METODO MCierrePeriodo
        /// <summary>
        private void MCierrePeriodo()
        {
            pgcOpciones.TabPages[0].Visible = false;
            pgcOpciones.TabPages[1].Visible = true;
            pgcOpciones.TabPages[1].Text = "DECLARACION JURADA";
            btnDeclaracion.Visible = false;
            btnGrabar.Enabled = false;
            pgcOpciones.ActiveTabIndex = 1;
            btnImportar.Enabled = false;
            btnLimpiaTo.Enabled = false;

            dgvCabecera.Columns["opciones"].Visible=false;
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
                                                                      "/WFDjConsolidacion.aspx?format=json");
                if (lstResultado.decCodigo == 1)
                {
                    if (lstResultado.oResultado != null && lstResultado.oResultado.Any())
                    {
                        Session[CVariablesSesion.PermisoCons] = lstResultado.oResultado.FirstOrDefault().CONSULTA;
                        Session[CVariablesSesion.PermisoAlta] = lstResultado.oResultado.FirstOrDefault().ALTAS;
                        Session[CVariablesSesion.PermisoBaja] = lstResultado.oResultado.FirstOrDefault().BAJAS;
                        Session[CVariablesSesion.PermisoModi] = lstResultado.oResultado.FirstOrDefault().MODIFICACIONES;
                        Session[CVariablesSesion.PermisoRevi] = lstResultado.oResultado.FirstOrDefault().REVISION;
                        Session[CVariablesSesion.PermisoApro] = lstResultado.oResultado.FirstOrDefault().APROBACION;
                    }
                    else
                    {
                        Session[CVariablesSesion.PermisoCons] = 0;
                        Session[CVariablesSesion.PermisoAlta] = 0;
                        Session[CVariablesSesion.PermisoBaja] = 0;
                        Session[CVariablesSesion.PermisoModi] = 0;
                        Session[CVariablesSesion.PermisoRevi] = 0;
                        Session[CVariablesSesion.PermisoApro] = 0;
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
        private void MConfigurarFormulario()
        {
            cmbEntExterna.SelectedIndex = 0;
            cmbPeriodo.SelectedIndex = 0;

            btnGrabar.Visible = Convert.ToInt16(Session[CVariablesSesion.PermisoAlta]) != 0;
            btnLimpiaTo.Visible = Convert.ToInt16(Session[CVariablesSesion.PermisoBaja]) != 0;
            btnEmiteDeclaracion.Visible = Convert.ToInt16(Session[CVariablesSesion.PermisoModi]) != 0;
        }

        /// <summary>
        /// METODO. MLimpiaGrilla
        /// </summary>
        private void MLimpiaGrilla()
        {
            Session.Remove(CVariablesSesion.GrillaTransaccion);
            Session.Remove(CVariablesSesion.GrillaRegObservado);
            Session.Remove(CVariablesSesion.GrillaAcumulado);
            dgvTransacciones.DataSource = null;
            dgvTransacciones.DataBind();
            dgvTransaccionError.DataSource = null;
            dgvTransaccionError.DataBind();

            Session.Remove(CVariablesSesion.GrillaCabecera);
            Session.Remove(CVariablesSesion.GrillaDetalle);
            dgvDdjjCab.DataSource = null;
            dgvDdjjCab.DataBind();
            dgvDdjjDet.DataSource = null;
            dgvDdjjDet.DataBind();

            btnGrabar.Enabled = false;
            pnlMensaje.Visible = false;

            lblTipoError.Text = "";
        }

        /// <summary>
        /// METODO. MMuestraCabecera
        /// </summary>
        private void MMuestraCabecera()
        {
            lblRazonSocial.Text = tlsOperadores.FocusedNode.GetValue("StrDescPadre").ToString();
            lblNit.Text = tlsOperadores.FocusedNode.GetValue("LngNit").ToString();
            lblDireccion.Text = (tlsOperadores.FocusedNode.GetValue("StrDireccion") == null) ? "No Definido" : tlsOperadores.FocusedNode.GetValue("StrDireccion").ToString();
        }

        /// <summary>
        /// METODO. MCargaParametricas
        /// </summary>
        private void MCargaParametricas()
        {
            var objParametricas = new O_PARAMETRICAS_GRAL();
            //PAISES
            var objUbicacion = _client.Post<EResultadoUbicacion>(CAppSettings.svc_obtiene_ubicacion,
            new
            {
                strCredencial = CParametrosHydro.strCredencialFuncionario,
                decNivel = 1,
                decIdPadre = 0
            });
            if (objUbicacion.IntCodigo == 1)
            {
                objParametricas.lstPais = objUbicacion.OResultado;
            }
            Session[CVariablesSesion.ListaParametrica] = objParametricas;

            //PARAM GLOBAL CLASIFICACION 1
            var lstObtCatalogo = _client.Post<EResultadoParametricaGlobal>(CAppSettings.svc_lista_parametrica_global,
            new
            {
                decIdDominio = 11
            });
            if (lstObtCatalogo.IntCodigo == 1)
            {
                Session[CVariablesSesion.ListaCatalogo] = lstObtCatalogo.OResultado;
            }

            var objDeptos = _client.Post<EResultadoUbicacion>(CAppSettings.svc_obtiene_ubicacion,
            new
            {
                strCredencial = CParametrosHydro.strCredencialFuncionario,
                decNivel = 2,
                decIdPadre = 120
            });
            if (objDeptos.IntCodigo == 1)
            {
                Session[CVariablesSesion.ListaDepartamentos] = objDeptos.OResultado;
            }
            else
            {
                Session[CVariablesSesion.ListaDepartamentos] = null;
            }
        }

        /// <summary>
        /// METODO. MValidaDatosIniciales
        /// </summary>
        /// <param name="strMensaje"></param>
        /// <returns></returns>
        private bool MValidaDatosIniciales(ref string strMensaje)
        {
            bool booResultado = true;

            if (cmbEntExterna.Text == "")
            {
                strMensaje = "Introduzca la entidad externa.";
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
        /// METODO. MValidaCondicionesFuncionamiento
        /// </summary>
        /// <param name="strMesnaje"></param>
        /// <returns></returns>
        private bool MValidaCondicionesFuncionamiento(ref string strMensaje)
        {
            var booRespuesta = true;

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

                Session[CVariablesSesion.ListaParamGral] = null;

                var objParametricas = new O_PARAMETRICAS_ACT();

                //Carga productos y unidades de medida
                var objHidrocarburo = _client.Post<EResultadoConfigHidrocarburo>(CAppSettings.svc_listar_autorizacion_condicion,
                new
                {
                    strCredencial = CParametrosHydro.strCredencialFuncionario,
                    decFiltro = 1,
                    strParametro1 = tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString(),
                    strParametro2 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
                    strParametro3 = ""
                });
                if (objHidrocarburo.IntCodigo == 1)
                {
                    objParametricas.lstProducto = objHidrocarburo.OResultado;
                    Session[CVariablesSesion.ListaParamGral] = objParametricas;

                    //carga tipos de operaciones
                    var objTipoOperacion = _client.Post<EResultadoTipoOperacion>(CAppSettings.svc_obtener_tipos_operacion_act,
                    new
                    {
                        decFiltro = 1,
                        strParametro1 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
                        strParametro2 = "",
                        strParametro3 = ""
                    });
                    if (objTipoOperacion.IntCodigo == 1)
                    {
                        //var decIdTipo = objTipoOperacion.OResultado.Where(x => x.CODIGO == cmbEntExterna.Value.ToString()).ToList().FirstOrDefault().ID_TIPO_OPERACION;
                        Session[CVariablesSesion.ComboTipoOperacion] = objTipoOperacion.OResultado.Where(x => x.CODIGO == cmbEntExterna.Value.ToString()).ToList().FirstOrDefault();
                    }
                    else
                    {
                        MImprimirMensaje(objTipoOperacion.StrMensaje, EnumTiposMensajeAlerta.Error);
                    }
                }
                else
                {
                    booRespuesta = false;
                    strMensaje = "U.M. " + objHidrocarburo.StrMensaje;
                }
            }

            return booRespuesta;
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
            //todo. hacerlo configurable
            objArbol = objArbol.Where(x => x.ID_ACTIVIDAD == 2).ToList();

            Session[CVariablesSesion.EstructuraArbol] = _cUtil.MObtenerArbolOrdenado(Properties.Settings.Default.intOrdenArbol, objArbol);
        }

        /// <summary>
        /// METODO. MCargarDatos
        /// </summary>
        /// <param name="strRuta"></param>
        private void MCargarDatos(string strRuta)
        {
            switch (cmbEntExterna.Value.ToString())
            {
                case "EGCSES"://RECAUDO SUIC
                    MCargaSuic(strRuta);
                    break;
                default:
                    MImprimirMensaje("Actividad invalida.", EnumTiposMensajeAlerta.Alerta);
                    return;
            }

            if (dgvTransaccionError.VisibleRowCount == 0)
            {
                btnGrabar.Enabled = true;
            }
        }

        /// <summary>
        /// METODO. MValidaLineaEess
        /// </summary>
        /// <param name="values"></param>
        /// <param name="strMensaje"></param>
        /// <returns></returns>
        private bool MValidaLineaSuic(string[] values, ref string strMensaje)
        {
            bool strResultado = true;
            try
            {
                #region Despachos
                if (values.Length != 17)
                {
                    strMensaje = "- Nro. de columnas del registro invalido.";
                }
                var arrayNumero = new[] { 1, 4, 5, 7, 8, 10, 11, 12, 13, 14, 15, 16 };
                decimal dec;
                for (int i = 0; i < arrayNumero.Length; i++)
                {
                    if (!decimal.TryParse(values[arrayNumero[i]].Trim(), out dec))
                    {
                        strMensaje += "- Columna Nro. " + arrayNumero[i] + " No es un número.";
                    }
                }
                DateTime dt;
                if (!DateTime.TryParse(values[0].Trim(), out dt))
                {
                    strMensaje += "- Fecha incorrecta.";
                }

                var arrPeriodo = cmbPeriodo.Value.ToString().Split('-');
                var decUsPer = Convert.ToDecimal(values[0].Trim().Substring(6, 4) + values[0].Trim().Substring(3, 2));
                var decPerIni = Convert.ToDecimal(arrPeriodo[0].Trim().Substring(3, 4) + arrPeriodo[0].Trim().Substring(0, 2));
                var decPerFin = Convert.ToDecimal(arrPeriodo[1].Trim().Substring(3, 4) + arrPeriodo[1].Trim().Substring(0, 2));
                if (decUsPer < decPerIni || decUsPer > decPerFin)
                {
                    strMensaje += "- Periodo incorrecto.";
                }

                if (MCodificaParametrica((int)EnumTipoParametrica.Producto, values[3].Trim()) == -99999)
                {
                    strMensaje += "- Producto incorrecto.";
                }
                if (MCodificaParametrica((int)EnumTipoParametrica.UnidadMedida, values[3].Trim()) == -99999)
                {
                    strMensaje += "- Unidad de medida invalida.";
                }
                if (Convert.ToDecimal(values[10]) <= 0)
                {
                    strMensaje += "- Cantidad incorrecta.";
                }
                if (Convert.ToDecimal(values[12].Trim()) > (Convert.ToDecimal(values[10].Trim()) * Convert.ToDecimal(values[11].Trim())) + 2 ||
                    Convert.ToDecimal(values[12].Trim()) < (Convert.ToDecimal(values[10].Trim()) * Convert.ToDecimal(values[11].Trim())) - 2)
                {
                    strMensaje += "- Importe incorrecto.";
                }
                if (Convert.ToDecimal(values[16].Trim()) > (Convert.ToDecimal(values[10].Trim()) * Convert.ToDecimal(values[15].Trim())) + 2 ||
                    Convert.ToDecimal(values[16].Trim()) < (Convert.ToDecimal(values[10].Trim()) * Convert.ToDecimal(values[15].Trim())) - 2)
                {
                    strMensaje += "- Importe SUIC incorrecto.";
                }
                if (values[9].Trim().Length == 1 && values[9].Trim() != "0")
                {
                    strMensaje += "- Código de control incorrecto.";
                }
                if (values[9].Trim().Length > 1 && values[9].Trim().Length < 6)
                {
                    strMensaje += "- Código de control incorrecto.";
                }
                if (values[2].Trim().Length > 1)
                {
                    if (values[2].Trim().ContainsAny("-", "@", "/", "'", "\\", ".", ",", "_", "+", "[", "]", "{", "}", " "))
                    {
                        strMensaje += "- Identificador contiene caracteres invalidos.";
                    }
                }
                if (Convert.ToDecimal(values[8]) == 0 || Convert.ToDecimal(values[13]) == 0 || Convert.ToDecimal(values[14]) == 0)
                {
                    strMensaje += "- Campos: Nro.Factura/Nro.ServicioSuic/Nro.Doc.Suic. invalidos.";
                }
                #endregion
            }
            catch (Exception ex)
            {
                strMensaje += ex.Message;
            }

            if (strMensaje.Trim().Length > 0)
            {
                strResultado = false;
            }

            return strResultado;
        }

        /// <summary>
        /// METODO. MCargaEess
        /// </summary>
        /// <param name="strRuta"></param>
        private void MCargaSuic(string strRuta)
        {
            var reader = new StreamReader(File.OpenRead(@strRuta));
            var lstRegistros = new List<CRegistrosImportados>();
            var lstRegistroE = new List<CRegistrosError>();
            long lngNoRegCorrect = 1, lngNoRegInCorrect = 1, lngNoReg = 1;
            var objTipoOp = (O_TIPOS_OPE_ACT_CTY)Session[CVariablesSesion.ComboTipoOperacion];
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(';');
                try
                {
                    DateTime datFecha, datFechaReg;
                    string strFecha;
                    ObjProtoMovimientoTransaccion objNuevoTipoReg = null;
                    ObjTransaccion objFactura, objDocumento;
                    int estadoTra;
                    List<ObjTransaccion> lstTransacciones;

                    //var to = cmbEntidadExt.Text.Split(';');
                    //var strTipoOp = to[1].Substring(2, 2);
                    string strMensaje = string.Empty;
                    if (MValidaLineaSuic(values, ref strMensaje))
                    {
                        var cDato = new CRegistrosImportados();
                        //int controlBinario;

                        #region Valida y genera objeto
                        cDato.LngId = lngNoRegCorrect;
                        cDato.StrIdVenta = lngNoRegCorrect.ToString();
                        datFecha = DateTime.Parse(values[0].Trim());
                        cDato.StrFechaEmision = datFecha.ToString();
                        cDato.StrTipoMov = "NAC";
                        cDato.StrEstado = "V";
                        cDato.LngNit = Convert.ToInt64(values[5]);
                        cDato.StrRazonSocial = values[6].Trim().ToUpper().Replace("¥", "Ñ").Replace("Á", "A").Replace("É", "E").Replace("Í", "I").Replace("Ó", "O").Replace("Ú", "U").Replace("'", "").Replace("�", "Ñ");
                        cDato.StrRazonSocial = Regex.Replace(cDato.StrRazonSocial, @"\s+", " ");
                        cDato.StrIdentificador = values[2].Trim().Replace("-", "").Replace(" ", "");
                        cDato.LngNroFactura = Convert.ToInt64(values[8]);
                        cDato.LngNroAutorizacion = Convert.ToInt64(values[7]);
                        cDato.StrNroReferencia = values[13].Trim();
                        cDato.LngNroAutVenta = Convert.ToInt64(values[14]);
                        cDato.StrProducto = values[3].Trim();
                        cDato.DecVolumen = Convert.ToDecimal(values[10]);
                        cDato.DecPrecio = Convert.ToDecimal(values[15]);
                        cDato.DecImporte = Convert.ToDecimal(values[16]);
                        cDato.DecIdTipoCreditoFiscal = (int)EnumTipoCredFiscal.Si;
                        cDato.objProtoMovimiento = new ObjProtoMovimientoTransaccion();
                        cDato.DecIdTipoOp = objTipoOp.ID_TIPO_OPERACION;
                        cDato.DecOrden = 1;

                        strFecha = datFecha.Year + datFecha.Month.ToString().PadLeft(2, '0') + datFecha.Day.ToString().PadLeft(2, '0') + datFecha.Hour.ToString().PadLeft(2, '0') + datFecha.Minute.ToString().PadLeft(2, '0') + datFecha.Second.ToString().PadLeft(2, '0');
                        datFechaReg = DateTime.Now;
                        estadoTra = (int)EnumEstadoMov.Activo;
                        lstTransacciones = new List<ObjTransaccion>();
                        var intFormaPago = 1;

                        objFactura = new ObjTransaccion();
                        objFactura.idMovimiento = 0;
                        objFactura.idTransaccion = 0;
                        objFactura.idTransaccionPadre = 0;
                        objFactura.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                        objFactura.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                        objFactura.idTipoTransaccion = (decimal)((values[9].Trim().Length > 0) ? EnumTipoTransaccion.FacturaComputarizada : EnumTipoTransaccion.FacturaManual);
                        objFactura.fechaEmision = Convert.ToInt64(strFecha);
                        objFactura.numeroTransaccion = values[8].Trim();
                        objFactura.numeroAutorizacion = Convert.ToDecimal(values[7]);
                        objFactura.codigoControl = values[9].Trim();
                        objFactura.nitConsumidor = Convert.ToDecimal(values[5].Trim());
                        objFactura.razonSocial = cDato.StrRazonSocial;
                        objFactura.precioUnitario = Convert.ToDecimal(values[11].Trim());
                        objFactura.precioTotal = Convert.ToDecimal(values[12].Trim());
                        objFactura.usuarioTransaccion = "";
                        objFactura.observaciones = "";
                        objFactura.nitEmisor = Convert.ToDecimal(values[5].Trim());
                        objFactura.idEstadoTransaccion = estadoTra;
                        objFactura.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.Si;
                        objFactura.idFormaPago = intFormaPago;
                        objFactura.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                        objFactura.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                        objFactura.codigoSeguridad = 0;
                        objFactura.idAutorizacionDato = 0;
                        objFactura.binarioControl = "0000000000";
                        objFactura.fechaOperacion = Convert.ToInt64(strFecha);
                        objFactura.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                        objFactura.codigo = "";
                        lstTransacciones.Add(objFactura);

                        objDocumento = new ObjTransaccion();
                        objDocumento.idMovimiento = 0;
                        objDocumento.idTransaccion = 0;
                        objDocumento.idTransaccionPadre = 0;
                        objDocumento.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                        objDocumento.fechaRegistroTransaccion = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                        objDocumento.idTipoTransaccion = (decimal)EnumTipoTransaccion.SrvSuic;
                        objDocumento.fechaEmision = Convert.ToInt64(strFecha);
                        objDocumento.numeroTransaccion = values[13].Trim();
                        objDocumento.numeroAutorizacion = Convert.ToDecimal(values[14]);
                        objDocumento.codigoControl = "";
                        objDocumento.nitConsumidor = Convert.ToDecimal(values[5].Trim());
                        objDocumento.razonSocial = cDato.StrRazonSocial;
                        objDocumento.precioUnitario = Convert.ToDecimal(values[15].Trim());
                        objDocumento.precioTotal = Convert.ToDecimal(values[16].Trim());
                        objDocumento.usuarioTransaccion = "";
                        objDocumento.observaciones = "";
                        objDocumento.nitEmisor = Convert.ToDecimal(values[5].Trim());
                        objDocumento.idEstadoTransaccion = estadoTra;
                        objDocumento.idTipoCreditoFiscal = (int)EnumTipoCredFiscal.No;
                        objDocumento.idFormaPago = intFormaPago;
                        objDocumento.idTipoMoneda = (decimal)EnumTipoMoneda.Boliviano;
                        objDocumento.idTipoEmisor = (decimal)EnumTipoEmisor.PropiaEntidad;
                        objDocumento.codigoSeguridad = 0;
                        objDocumento.idAutorizacionDato = 0;
                        objDocumento.binarioControl = "1000000000";
                        objDocumento.fechaOperacion = Convert.ToInt64(strFecha);
                        objDocumento.idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                        objDocumento.codigo = "";
                        lstTransacciones.Add(objDocumento);

                        objNuevoTipoReg = new ObjProtoMovimientoTransaccion();
                        objNuevoTipoReg.idMovimientoPadre = 0;
                        objNuevoTipoReg.idTipoActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
                        objNuevoTipoReg.idTipoOperacion = objTipoOp.ID_TIPO_OPERACION;
                        objNuevoTipoReg.idConsumidor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString());
                        objNuevoTipoReg.idUsuarioAplicacion = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
                        objNuevoTipoReg.fechaRegistroMovimiento = Convert.ToDecimal(datFechaReg.Year + datFechaReg.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + datFechaReg.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
                        objNuevoTipoReg.fechaOperacion = Convert.ToInt64(strFecha);
                        objNuevoTipoReg.idTipoMovimientoFlujo = (int)EnumTipoMovimientoFlujo.Contable;
                        objNuevoTipoReg.idTipoMovimiento = (int)EnumTipoMovimiento.NacNac;
                        objNuevoTipoReg.idEstadoMovimiento = estadoTra;
                        objNuevoTipoReg.idFuenteOrigenDato = (decimal)EnumTipoRegistro.OrigenCsv;
                        objNuevoTipoReg.idUbicacionOperacion = MCodificaParametrica((int)EnumTipoParametrica.Pais, "BOLIVIA");
                        objNuevoTipoReg.idUnidadMedidaHidrocarburo = MCodificaParametrica((int)EnumTipoParametrica.UnidadMedida, values[3].Trim());
                        objNuevoTipoReg.idHidrocarburo = MCodificaParametrica((int)EnumTipoParametrica.Producto, values[3].Trim());
                        objNuevoTipoReg.cantidadHidrocarburo = cDato.DecVolumen;
                        objNuevoTipoReg.identificador = cDato.StrIdentificador;
                        objNuevoTipoReg.subIdentificador = "";
                        objNuevoTipoReg.observacion = "";
                        objNuevoTipoReg.idTipoMedioTransporte = -99999;
                        objNuevoTipoReg.idMedioTransporte = -99999;
                        objNuevoTipoReg.tipoInfraestructura = -99999;
                        objNuevoTipoReg.idInfraestructura = -99999;
                        objNuevoTipoReg.idUnidadEnvasesTransporte = -99999;
                        objNuevoTipoReg.idEntidad = -99999;
                        objNuevoTipoReg.idTipoMedicion = -99999;
                        objNuevoTipoReg.cantidadAguaLitros = 0;
                        objNuevoTipoReg.idUnidadTemperatura = -99999;
                        objNuevoTipoReg.temperatura = 0;
                        objNuevoTipoReg.gradoApi = "";
                        objNuevoTipoReg.cantidadEnvasesTransporte = 0;
                        objNuevoTipoReg.binarioControl = "00000-0000000000-0000000000-00000";
                        objNuevoTipoReg.idIntegracion = Convert.ToDecimal(values[1].Trim());
                        objNuevoTipoReg.idTransaccionBaseExterna = "";
                        objNuevoTipoReg.fechaOperacionDia = 0;
                        objNuevoTipoReg.codigo = "";
                        objNuevoTipoReg.barcode = 0;
                        objNuevoTipoReg.listaTransaccion = JsonSerializer.SerializeToString(lstTransacciones);
                        objNuevoTipoReg.jsonReferencia = "";
                        objNuevoTipoReg.estadoJsonRef = -99999;
                        objNuevoTipoReg.idCuenta = -99999;
                        objNuevoTipoReg.idCuentaAux = -99999;
                        objNuevoTipoReg.idParamGlobal = -99999;
                        objNuevoTipoReg.idParamEspec = -99999;
                        objNuevoTipoReg.idTipoConsumidor = 32;
                        objNuevoTipoReg.idTipoInfraAux = -99999;
                        objNuevoTipoReg.idInfraAux = -99999;

                        #endregion

                        cDato.objProtoMovimiento = objNuevoTipoReg;
                        lstRegistros.Add(cDato);
                        lngNoRegCorrect++;
                    }
                    else
                    {
                        var cRegError = new CRegistrosError();
                        cRegError.LngId = lngNoReg;
                        cRegError.StrIdVenta = values[1].Trim();
                        cRegError.LngFechaEmision = values[0].Trim();
                        cRegError.LngNit = values[5].Trim();
                        cRegError.LngNroAutorizacion = values[7].Trim();
                        cRegError.LngNroFactura = values[8].Trim();
                        cRegError.LngNroReferencia = values[1].Trim();
                        cRegError.StrErrores = strMensaje.Trim();
                        lstRegistroE.Add(cRegError);
                        lngNoRegInCorrect++;
                    }
                }
                catch (Exception ex)
                {
                    var cRegError = new CRegistrosError();
                    cRegError.LngId = lngNoReg;
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
                lngNoReg++;
            }

            dgvTransacciones.DataSource = lstRegistros;
            dgvTransacciones.DataBind();
            Session[CVariablesSesion.GrillaTransaccion] = lstRegistros;

            dgvTransaccionError.DataSource = lstRegistroE;
            dgvTransaccionError.DataBind();
            Session[CVariablesSesion.GrillaRegObservado] = lstRegistroE;
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
            }
            return lngResultado;
        }

        /// <summary>
        /// METODO. MEnviaObjeto
        /// </summary>
        /// <param name="lstProtoMovimientos"></param>
        /// <param name="booTipo"></param>
        /// <param name="lngNoMovimientos"></param>
        /// <param name="decFechaInicio"></param>
        /// <param name="decFechaFin"></param>
        /// <param name="decTipoOperacion"></param>
        private void MEnviaObjeto(List<ObjProtoMovimientoTransaccion> lstProtoMovimientos, bool booTipo, long lngNoMovimientos, decimal decFechaInicio, decimal decFechaFin, decimal decTipoOperacion, decimal decOrden)
        {
            var objTipoOp = (O_TIPOS_OPE_ACT_CTY)Session[CVariablesSesion.ComboTipoOperacion];
            var objRegistroo = new ERegistraProtoMovimientoTransaccion();
            objRegistroo.strCredencial = CParametrosHydro.strCredencialFuncionario;
            objRegistroo.decIdTipoActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString());
            objRegistroo.decIdConsumidor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString());
            objRegistroo.decIdTipoOperacion = decTipoOperacion;
            objRegistroo.decFechaInicio = decFechaInicio;
            objRegistroo.decFechaFin = decFechaFin;
            objRegistroo.listaMovimientoTransaccion = lstProtoMovimientos;

            //var ojo = JsonSerializer.SerializeToString(objRegistroo);
            var resultado = _client.Post<EResultado>(CAppSettings.svc_registra_movimiento_transaccion_protobuf, objRegistroo);
            if (resultado.intCodigo == 1)
            {
                if (booTipo)
                {
                    var lstDespachos = _client.Post<EResultado>(CAppSettings.svc_activa_json_transaccion,
                    new
                    {
                        strCredencial = CParametrosHydro.strCredencialFuncionario,
                        decFiltro = 1,
                        decIdConsumidor = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString()),
                        decIdTipoActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString()),
                        decIdTipoOperacion = decTipoOperacion,
                        decFechaInicio,
                        decFechaFin
                    });
                    if (lstDespachos.intCodigo == 1)
                    {
                        if (decOrden == 1)
                        {
                            var datFechaReg1 = DateTime.Now;
                            var repCe = new repConstanciaEnvio();

                            repCe.xtcFecha.Text = datFechaReg1.ToShortDateString();
                            repCe.xtcHora.Text = datFechaReg1.ToLongTimeString();
                            repCe.xtcUsuario.Text = CParametrosHydro.StrUsuarioLogin;
                            repCe.xtcIntervalo.Text = "";

                            repCe.xtcActividad.Text = tlsOperadores.FocusedNode.GetValue("StrActividad").ToString();
                            repCe.xtcTipoOperacion.Text = objTipoOp.NOMBRE;
                            repCe.xtcPeriodo.Text = cmbPeriodo.Text;

                            repCe.xtcNit.Text = tlsOperadores.FocusedNode.GetValue("LngNit").ToString();
                            repCe.xtcRazonSocial.Text = tlsOperadores.FocusedNode.GetValue("StrDescPadre").ToString();
                            repCe.xtcNomComercial.Text = tlsOperadores.FocusedNode.GetValue("StrDescripcion").ToString();
                            repCe.xtcNomPropietario.Text = (tlsOperadores.FocusedNode.GetValue("StrPropietario") == null) ? "" : tlsOperadores.FocusedNode.GetValue("StrPropietario").ToString();
                            repCe.xtcNomRepresentante.Text = tlsOperadores.FocusedNode.GetValue("StrRepLegal").ToString();
                            repCe.xtcCiudad.Text = tlsOperadores.FocusedNode.GetValue("StrDepartamento").ToString();
                            repCe.xtcDireccion.Text = tlsOperadores.FocusedNode.GetValue("StrDireccion").ToString();
                            repCe.xtcTelefono.Text = tlsOperadores.FocusedNode.GetValue("StrTelefono").ToString();
                            repCe.xtcNoRegistros.Text = lngNoMovimientos.ToString(CultureInfo.InvariantCulture);

                            Session[CVariablesSesion.TipoReporte] = EnumTipoReporte.ConstanciaEnvio;
                            Session[CVariablesSesion.ReportePrincipal] = repCe;
                            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( 'WFReporte.aspx', '');", true);

                            MLimpiaGrilla();
                        }
                    }
                    else
                    {
                        MImprimirMensaje(lstDespachos.strMensaje, EnumTiposMensajeAlerta.Error);
                    }
                }
                else
                {
                    //todo No implementado.
                }
            }
            else
            {
                MImprimirMensaje(resultado.strMensaje, EnumTiposMensajeAlerta.Error);
                //return;
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
            lblMensajeError.Text = "   " + strMensaje + "   ";
        }

        /// <summary>
        /// METODO. MValidaDatos
        /// </summary>
        /// <param name="strMensaje"></param>
        /// <returns></returns>
        private bool MValidaDatos(ref string strMensaje)
        {
            var booResultado = true;

            //Valida datos basicos
            //var lstMov = Session[CVariablesSesion.GrillaCabecera] as List<O_CONSOLIDACION_SUIC_ACU_CTY>;
            //if (lstMov == null || !lstMov.Any())
            //{
            //    strMensaje = "No existen registros.";
            //    booResultado = false;
            //}
            //else 
            if (cmbDepartamento.Text == "")
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


            return booResultado;
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

            var strRep = cmbRepresentante.Text.Length > 0 ? cmbRepresentante.Text.Split('-') : txtPadPropietario.Text.Split('-');

            Session.Remove(CVariablesSesion.ReportePrincipal);
            Session[CVariablesSesion.TipoReporte] = EnumTipoReporte.DdjjConsolidaSuic;

            var xreAcumulado = new repDjConsolidacion();
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
            xreAcumulado.xtcMes.Text = cmbPeriodo.Text;
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
            xreAcumulado.DataSource = Session[CVariablesSesion.GrillaCabecera];
            Session[CVariablesSesion.ReportePrincipal] = xreAcumulado;

            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( 'WFReporte.aspx', '')", true);
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