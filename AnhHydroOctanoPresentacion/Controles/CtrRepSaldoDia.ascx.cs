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
    public partial class CtrRepSaldoDia : System.Web.UI.UserControl
    {
        #region variables de entorno
        private readonly CPersistenciaConsulta _consulta = new CPersistenciaConsulta();
        readonly JsonServiceClient _client = new JsonServiceClient(CAppSettings.servicio_octVolumen);
        private readonly CPersistenciaGestion _gestion = new CPersistenciaGestion();
        CConsultasParametricas cConsultasParametricas = new CConsultasParametricas();
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
                        Session.Remove(CVariablesSesion.EstructuraArbol); 
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
                    if (dgvTransacciones.DataSource == null && Session[CVariablesSesion.GrillaTransPlantilla] != null)
                    {
                        dgvTransacciones.DataSource = Session[CVariablesSesion.GrillaTransPlantilla];
                        dgvTransacciones.DataBind();
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
            //if (MValidaSesion())
            //{
                try
                {
                    #region Proceso a Ejecutar
                    
                    var objConsulta = new EObjetoEnvio5ParametrosProto
                    {
                        strCredencial = CParametrosHydro.strCredencialFuncionario,
                        decFiltro = 1, //Filtro 1 
                        strParametro1 = detFechaConsulta.Date.Year + detFechaConsulta.Date.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + detFechaConsulta.Date.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') ,
                        strParametro2 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(),
                        strParametro3 = cmbDepartamento.Text.ToString(),
                        strParametro4 = tkblstIdConsumidor.Value.ToString()                      
                    };
                    string strAccion = JsonSerializer.SerializeToString(objConsulta);
                    string mensaje = GetType().Name + "." + MethodBase.GetCurrentMethod().Name;                    
                    MRegistraTrazaInf(strAccion, mensaje, CParametrosHydro.decIdAplicacion, Convert.ToDecimal(Session[CVariablesSesion.IdUsuario].ToString()), HttpContext.Current.Request.UserHostAddress);

                    dgeExportar.GridViewID = "dgvTransacciones";
                    dgeExportar.FileName = cmbDepartamento.Text;
                    dgeExportar.WriteXlsxToResponse();

                    #endregion
                }
                catch (Exception exp)
                {
                    #region RegistraError

                    MImprimirMensaje(exp.Message, EnumTiposMensajeAlerta.Error);

                    #endregion
                }
            //}
            //else
            //{
            //    //todo no implementado
            //}
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
                        
                            var strDato1 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString();
                            var strDato2 = "";//tlsOperadores.FocusedNode.GetValue("IntIdOperador").ToString();
                            var strDato3 = detFechaConsulta.Date.Year + detFechaConsulta.Date.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + detFechaConsulta.Date.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0');
                            var strDato4 = detFechaConsulta.Date.Year + detFechaConsulta.Date.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') + detFechaConsulta.Date.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0');

                            var idActividad = Convert.ToDecimal(tlsOperadores.FocusedNode.GetValue("IntIdActividad"));
                            var idDepartamento = Convert.ToDecimal(cmbDepartamento.Value);

                            List<decimal> lstidConsumidores = tkblstIdConsumidor.Value.ToString().Length > 0 ? tkblstIdConsumidor.Value.ToString().Split(',').Select(decimal.Parse).ToList() : new List<decimal>();
                           

                            var objArbol = Session[CVariablesSesion.ObjetoArbol] as List<O_OBTIENE_ENTIDADES_CTY>;
                            if (objArbol != null)
                            {
                                var lstEntidad = new List<O_OBTIENE_ENTIDADES_CTY>();
                                if (idDepartamento==10)
                                {
                                    lstEntidad = tkblstIdConsumidor.Value.ToString().Length > 0 ? objArbol.Where(l => (l.ID_ACTIVIDAD == idActividad) && lstidConsumidores.Contains(l.ID_CONSUMIDOR)).ToList() : objArbol.Where(l => (l.ID_ACTIVIDAD == idActividad)).ToList();//&& l.ID_CONSUMIDOR == 1592562   
                                }
                                else
                                {
                                    lstEntidad = tkblstIdConsumidor.Value.ToString().Length > 0 ? objArbol.Where(l => (l.ID_ACTIVIDAD == idActividad && l.ID_DEPARTAMENTO == idDepartamento) && lstidConsumidores.Contains(l.ID_CONSUMIDOR)).ToList() : objArbol.Where(l => (l.ID_ACTIVIDAD == idActividad && l.ID_DEPARTAMENTO == idDepartamento)).ToList();//&& l.ID_CONSUMIDOR == 1592562   
                                }
                
                                var lstMovimientos2023 = new List<CMovAcumulado>();            
                                foreach (var lst in lstEntidad)
                                {
                                    var aux = MCargaGrilla(strDato1, lst.ID_CONSUMIDOR.ToString(), strDato3, strDato4, sender, lst.DENOMINACION, lst.DEPARTAMENTO);
                                    lstMovimientos2023.AddRange(aux);
                                }

                                Session[CVariablesSesion.GrillaTransPlantilla] = lstMovimientos2023;

                                dgvTransacciones.DataSource = lstMovimientos2023;
                                dgvTransacciones.KeyFieldName = "IntId";
                                dgvTransacciones.DataBind();
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

        private List<CMovAcumulado> MCargaGrilla(string strParametro1, string strParametro2, string strParametro3, string strParametro4, object sender, string strDenominacion, string strDepartamento)
        {
            var lstDespachos = _cUtil.MObtieneMovAcumulado(1, strParametro1, strParametro2, strParametro3, strParametro4, 2, -99999);
            var lstMovimientos = new List<CMovAcumulado>();
            if (lstDespachos.intCodigo == 1)
            {

                var lstResultadoLstAcumulado = (O_DECLARACION_JURADA_CTY)lstDespachos.oResultado;
                Session[CVariablesSesion.DjAcumulado] = lstResultadoLstAcumulado;
                var lstResultado = lstResultadoLstAcumulado.LstAcumulado;

                if (lstResultado.Any())
                {
                    #region acumulado
                    decimal decProd = 0;
                    int cont = 1, intContProd = 1;

                    lstResultado = lstResultado.OrderBy(x => x.DecIdHidrocarburo).ThenBy(x => x.DecFecha).ToList();
                    
                    foreach (var lst in lstResultado)
                    {
                        var cDato = new CMovAcumulado();
                        if (decProd != lst.DecIdHidrocarburo)
                        {
                            decProd = lst.DecIdHidrocarburo;
                            intContProd = 1;
                        }

                        cDato.IntId = Convert.ToInt32(strParametro2); //cont;
                        cDato.IntContProd = intContProd;
                        cDato.StrFecha = lst.DecFecha.ToString().Substring(6, 2) + "/" + lst.DecFecha.ToString().Substring(4, 2) + "/" + lst.DecFecha.ToString().Substring(0, 4) +" "+ DateTime.Now.Hour.ToString().PadLeft(2, '0') + ":"+DateTime.Now.Minute.ToString().PadLeft(2, '0') +":"+ DateTime.Now.Second.ToString().PadLeft(2, '0');
                        cDato.IntIdProducto = (int)lst.DecIdHidrocarburo;
                        cDato.StrProducto = strDenominacion;//lst.StrHidrocarburo;
                        cDato.StrCodProducto = lst.StrCodHidrocarburo;
                        cDato.StrUnidadMedida = strDepartamento;//lst.StrUnidadMedida;

                        cDato.DecIdTipoInfraestructura = lst.DecIdTipoInfraestructura;
                        cDato.StrTipoInfraestructura = lst.StrTipoInfraestructura ?? "";
                        cDato.DecIdInfraestructura = lst.DecIdInfraestructura;
                        cDato.StrInfraestructura = lst.StrInfraestructuraAnh ?? "";
                        cDato.StrCodigoInfraestructura = lst.StrInfraestructura ?? "";

                        cDato.StrIdGrupoUnico = "" + lst.DecIdHidrocarburo + lst.DecIdTipoInfraestructura + lst.DecIdInfraestructura;
                        

                        cDato.DecSaldoInicial = lst.DecSaldoInicial;
                        cDato.DecTotalIng = lst.DecTotalIngreso;
                        cDato.DecSaldoFinal = lst.DecSaldoFinal;
                        cDato.DecTotalSal = lst.DecTotalEgreso;
                        cDato.DecImporteTotal = lst.DecTotalImporte;
                        cDato.StrObservaciones = lst.StrObservaciones;

                        var vecValores = new decimal[100];
                        vecValores[0] = lst.DecSaldoInicial;
                        vecValores[1] = lst.DecSaldoFinal;
                        vecValores[2] = lst.DecTotalIngreso;
                        vecValores[3] = lst.DecTotalEgreso;


                        if (lst.LstIngresos == null || !lst.LstIngresos.Any())
                        {
                            cDato.DecIng01 = -99999;
                        }
                        else
                        {
                            foreach (var reg in lst.LstIngresos)
                            {                                
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

                                vecValores[3 + (int)reg.OPERACION_ORDEN] = reg.CANTIDAD;
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
                                cDato.DecImporteTotal = reg.PRECIO_TOTAL;
                                vecValores[23 + (int)reg.OPERACION_ORDEN] = reg.CANTIDAD;

                            }
                            cDato.VecValores = vecValores;
                        }

                        lstMovimientos.Add(cDato);
                        cont++;
                        intContProd++;
                    }                       

                    #endregion
                }
            }
            else
            {
                MImprimirMensaje(lstDespachos.strMensaje, EnumTiposMensajeAlerta.Error);
            }
            return lstMovimientos;
        }
        protected void tlsOperadores_CustomCallback(object sender, DevExpress.Web.ASPxTreeList.TreeListCustomCallbackEventArgs e)
        {
           
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

            detFechaConsulta.Date = DateTime.Now;   

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
        /// METODO. MLimpiaGrilla
        /// </summary>
        private void MLimpiaGrilla()
        {
            Session.Remove(CVariablesSesion.GrillaTransaccion);
            Session.Remove(CVariablesSesion.GrillaRegObservado);

            pnlMensaje.Visible = false;
            dgvTransacciones.DataSource = null;
            dgvTransacciones.DataBind();     
        }
        private void MRegistraTrazaInf(string strAccion, string strTipoAccion, decimal decIdModulo, decimal decIdUsuario, string strIp)
        {            
            var lstVisor = (List<CMovAcumulado>)Session[CVariablesSesion.GrillaTransPlantilla];            
            try
            {
                var clienteJsonSeg = new JsonServiceClient(System.Web.Configuration.WebConfigurationManager.AppSettings["ServicioJsonSeguridad"]);
                var lstResultado = clienteJsonSeg.Post<oResultadoClogTraza>("/EInsertaLogTraza/",
                                                               new
                                                               {
                                                                   dtFechaRegistro = DateTime.Now,
                                                                   strTipoLog = "INFO",
                                                                   strAccion = strTipoAccion,
                                                                   strDetalleAccion = "",
                                                                   decModuloId = decIdModulo,
                                                                   strExcepcionInterna = "",
                                                                   strMensajeExcepcion = "",
                                                                   strSeguimientoPila = "",
                                                                   strIp = strIp,
                                                                   strmensaje = "RESULTADO: " + GetType().Name + "." + System.Reflection.MethodBase.GetCurrentMethod().Name + " ENTRADA: " + strAccion + " SALIDA REG. EXPORTADOS: " + lstVisor.Count(),
                                                                   strNivelCapa = CLogTraza.CapasNivel.Presentacion.ToString(),
                                                                   decUsuarioId = decIdUsuario
                                                               });
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
        private class oResultadoClogTraza
        {
            public int decCodigo { get; set; }
            public string strMensaje { get; set; }
            public object oResultado { get; set; }
        }

        #endregion       
    }
}