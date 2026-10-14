
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
    using System.Web.UI.WebControls;
    using AnhHydroOctanoPersistencia.Aplicacion.Consulta;
    using AnhHydroOctanoPersistencia.Entidades;
    using Clases.Controles;
    using Entidades;
    using Enumeradores;
    using Parametros;
    using DevExpress.Web;
    using DevExpress.XtraCharts;
    using Librerias.Anh.Us;
    using ServiceStack.ServiceClient.Web;
    using Page = System.Web.UI.Page;

    public partial class CtrRepGrafico : UserControl
    {
        #region Variables de entorno

        readonly JsonServiceClient _client = new JsonServiceClient(CAppSettings.servicio_octVolumen);
        CConsultasParametricas cConsultasParametricas = new CConsultasParametricas();

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

                        //var tt = System screen.width;
                        //int x = (Request.Browser.ScreenPixelsWidth)*2-100;
                        int x = (Request.Browser.ScreenPixelsWidth) * 2;
                        var g = x + "px";
                        //WebChartControl1.Width = new Unit("1600px");
                        WebChartControl1.Width = new Unit(g);
                        //var t = ASPxSplitter1.Width;

                        //var g = pnlTitulo.Width;
                    }

                    tlsOperadores.DataSource = Session[CVariablesSesion.EstructuraArbol];
                    tlsOperadores.DataBind();

                    if (Session[CVariablesSesion.GrillaCabecera] != null)
                    {
                        //pvgReporte.DataSource = Session[CVariablesSesion.GrillaCabecera];
                        //pvgReporte.DataBind();
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

        protected void pvgReporte_CustomCellStyle(object sender, DevExpress.Web.ASPxPivotGrid.PivotCustomCellStyleEventArgs e)
        {
            if (e.Value == null || (decimal)e.Value == 0)
            {
                var x = e.RowValueType;
                var y = e.ColumnValueType;
                if (x.ToString().Contains("Value") && !y.ToString().Contains("Total"))
                {
                    e.CellStyle.BackColor = Color.FromArgb(241, 186, 118);
                }
                else
                {
                    e.CellStyle.BackColor = Color.FromArgb(239, 244, 218);
                }
            }
            else
            {
                //todo no implementado.
            }
        }

        protected void btnEmitir_Click(object sender, EventArgs e)
        {
            if (MValidaSesion())
            {
                try
                {
                    #region Proceso a Ejecutar

                    //pvgExportar.ASPxPivotGridID = "pvgReporte";
                    //pvgExportar.ExportXlsxToResponse("CumplimientoDiario");

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

        protected void btnGrafica_Click(object sender, EventArgs e)
        {
            // Create a WebChartControl instance. 
            //WebChartControl WebChartControl1 = new WebChartControl();

            // Add the chart to the form. 
            // Note that a chart isn't initialized until it's added to the form's collection of controls. 
            //this.Controls.Add(WebChartControl1);

            // Create a line series and add points to it. 

            Series series01 = new Series("My Series 1", ViewType.StackedLine);
            series01.Points.Add(new SeriesPoint("A", new double[] { 1 }));
            //series01.Points.Add(new SeriesPoint("B", new double[] { 43598 }));
            //series01.Points.Add(new SeriesPoint("C", new double[] { 5 }));
            //series01.Points.Add(new SeriesPoint("D", new double[] { 9 }));
            WebChartControl1.Series.Add(series01);

            Series series02 = new Series("My Series 2", ViewType.StackedLine);
            series01.Points.Add(new SeriesPoint("s", new double[] { 10 }));
            //series01.Points.Add(new SeriesPoint("B", new double[] { 43598 }));
            //series01.Points.Add(new SeriesPoint("C", new double[] { 5 }));
            //series01.Points.Add(new SeriesPoint("D", new double[] { 9 }));
            WebChartControl1.Series.Add(series02);

            //Series series1 = new Series("My Series 1", ViewType.Bar);
            //series1.Points.Add(new SeriesPoint("A", new double[] { 2 }));
            //series1.Points.Add(new SeriesPoint("B", new double[] { 7 }));
            //series1.Points.Add(new SeriesPoint("C", new double[] { 5 }));
            //series1.Points.Add(new SeriesPoint("D", new double[] { 9 }));

            //// Add the series to the chart. 
            //WebChartControl1.Series.Add(series1);

            //series1 = new Series("My Series 2", ViewType.Bar);
            //series1.Points.Add(new SeriesPoint("A", new double[] { 4 }));
            //series1.Points.Add(new SeriesPoint("B", new double[] { 10 }));
            //series1.Points.Add(new SeriesPoint("C", new double[] { 20 }));
            //series1.Points.Add(new SeriesPoint("D", new double[] { 2 }));

            //// Add the series to the chart. 
            //WebChartControl1.Series.Add(series1);

        }

        protected void ASPxButton1_Click(object sender, EventArgs e)
        {
            //carga tiposOperacion
            var lstTipoOperacion = new List<O_TIPOS_OPE_ACT_CTY>();
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
                lstTipoOperacion = objTipoOperacion.OResultado;
                lstTipoOperacion = lstTipoOperacion.Where(x => x.FACTOR_CALCULO != 0).ToList();



            }
            else
            {
                MImprimirMensaje(objTipoOperacion.StrMensaje, EnumTiposMensajeAlerta.Error);
            }


            //var consulta = new CReportesEntidad();
            CPersistenciaConsulta _consulta = new CPersistenciaConsulta();
            var objResultado = _consulta.ReporteMovGral(
            new EObjetoEnvio6ParametrosProto
            {
                strCredencial = CParametrosHydro.strCredencialFuncionario,
                decFiltro = 1, // 1: Actividad | 2: Actividad - Depto | 3: Actividad - Depto - Consumidor
                strParametro1 = tlsOperadores.FocusedNode.GetValue("IntIdActividad").ToString(), // IdActividad
                strParametro2 = "",// F1: VACIO | F2: Id_Depto | F3: IdConsumidor
                strParametro3 = "126", // id_producto
                strParametro4 = "", //id_tipo_operacion
                strParametro5 = "20170101", //fechaInicio
                strParametro6 = "20170131" //fechaFin
            });
            if (objResultado.intCodigo == 1)
            {
                var lstReporte = objResultado.oResultado as List<O_LISTA_REPORTE_MOVGRAL_CTY>;
                if (lstReporte.Any())
                {
                    //WebChartControl1.Legend.AlignmentVertical = LegendAlignmentVertical.Top;
                    //WebChartControl1.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Left;

                    var lstFinal = new List<O_LISTA_REPORTE_MOVGRAL_CTY>();
                    DateTime fIni = DateTime.Parse("01/01/2017");
                    DateTime fFin = DateTime.Parse("31/01/2017");

                    for (DateTime i = fIni; i <= fFin; i = i.AddDays(1))
                    {
                        decimal ttt = Convert.ToDecimal(i.Year + i.Month.ToString().PadLeft(2, '0') + i.Day.ToString().PadLeft(2, '0'));

                        var lstAux = lstReporte.Where(x => x.FECHA == ttt).ToList();
                        if (lstAux.Any())
                        {

                            foreach (var tiOp in lstTipoOperacion)
                            {
                                var reg = new O_LISTA_REPORTE_MOVGRAL_CTY();

                                reg.FECHA = ttt;
                                reg.ID_TIPO_OPERACION = tiOp.ID_TIPO_OPERACION;
                                reg.TIPO_OPERACION = tiOp.NOMBRE;

                                var lstAux21 = lstAux.Where(x => x.ID_TIPO_OPERACION == tiOp.ID_TIPO_OPERACION).ToList();
                                if (lstAux21.Any())
                                {
                                    reg.CANTIDAD = lstAux21.FirstOrDefault().CANTIDAD;
                                }
                                else
                                {
                                    reg.CANTIDAD = 0;
                                }


                                lstFinal.Add(reg);
                            }


                            //var LeftJoin = from emp in lstTipoOperacion
                            //               join dept in lstAux
                            //               on emp.ID_TIPO_OPERACION equals dept.ID_TIPO_OPERACION into JoinedEmpDept
                            //               from dept in JoinedEmpDept.DefaultIfEmpty()
                            //               select new
                            //               {
                            //                   EmployeeName = emp.ID_TIPO_OPERACION,
                            //                   DepartmentName = dept != null ? dept.FECHA : 0
                            //               };

                            //var q = (from aa in lstTipoOperacion
                            //         join bb in lstAux on aa.ID_TIPO_OPERACION equals bb.ID_TIPO_OPERACION into t
                            //         from rt in t.DefaultIfEmpty()
                            //         //orderby aa.ID_TIPO_OPERACION
                            //         select new
                            //         {
                            //             aa.ID_TIPO_OPERACION,
                            //             //aa.NOMBRE,
                            //             FECHA = (rt.FECHA == null) ? 0 : rt.FECHA
                            //             //cccc = (rt.CANTIDAD == null) ? 0 : rt.CANTIDAD,
                            //         }).ToList();

                            //var lines =
                            //(from tl in lstTipoOperacion
                            //join j in lstAux on tl.ID_TIPO_OPERACION equals j.ID_TIPO_OPERACION into tl_j

                            //from j in tl_j.DefaultIfEmpty()
                            // where j == null
                            // select new O_LISTA_REPORTE_MOVGRAL_CTY
                            //{
                            //    ID_TIPO_OPERACION = tl.ID_TIPO_OPERACION,
                            //    TIPO_OPERACION = tl.NOMBRE,
                            //    FECHA = j.FECHA ?? 0,
                            //    CANTIDAD = j.CANTIDAD
                            //}).ToList();


                            //  var gtg = 3;
                            //LeftJoin.ToList();
                        }
                        else
                        {
                            foreach (var tiOp in lstTipoOperacion)
                            {
                                var reg = new O_LISTA_REPORTE_MOVGRAL_CTY();
                                reg.FECHA = ttt;
                                reg.ID_TIPO_OPERACION = tiOp.ID_TIPO_OPERACION;
                                reg.TIPO_OPERACION = tiOp.NOMBRE;
                                reg.CANTIDAD = 0;
                                lstFinal.Add(reg);
                            }
                        }



                    }


                    var thth = lstFinal;

                    lstFinal = lstFinal.OrderBy(x => x.TIPO_OPERACION).ThenBy(x => x.FECHA).ToList();


                    Series series01 = new Series();
                    decimal decIdTipoOp = 0;
                    foreach (var reg in lstFinal)
                    {
                        if (decIdTipoOp != reg.ID_TIPO_OPERACION)
                        {
                            if (series01.Points.Count > 0)
                            {
                                WebChartControl1.Series.Add(series01);
                            }

                            series01 = new Series(reg.TIPO_OPERACION, ViewType.Line);
                            decIdTipoOp = reg.ID_TIPO_OPERACION;



                        }

                        //double tt = (double) reg.CANTIDAD;
                        double tt = Convert.ToDouble(reg.CANTIDAD);

                        //series01.Points.Add(new SeriesPoint(reg.FECHA.ToString(), new double[] { tt }));
                        //var strFecha = reg.FECHA.ToString().Substring(6, 2) + "/" + reg.FECHA.ToString().Substring(4, 2) + "/" + reg.FECHA.ToString().Substring(2, 2);
                        var strFecha = reg.FECHA.ToString().Substring(6, 2) + "/" + reg.FECHA.ToString().Substring(4, 2) + "/" + reg.FECHA.ToString().Substring(2, 2);
                        //strFecha = strFecha.Substring()
                        //series01.Points.Add(new SeriesPoint(reg.FECHA.ToString(), new double[] { tt }));
                        series01.Points.Add(new SeriesPoint(strFecha, new[] { tt }));
                        //series01.Points.Add(new SeriesPoint(strFecha, new[] { Convert.ToInt64(reg.CANTIDAD) }));

                        //series01.Points.Add(new SeriesPoint(reg.FECHA.ToString(), new Decimal[] { reg.CANTIDAD }));
                        //series01.Points.Add(new SeriesPoint("A", new decimal[] { 2 }));


                        //series01.Points.Add(new SeriesPoint(reg.FECHA, new double[][] { reg.CANTIDAD }));


                    }

                    if (series01.Points.Count > 0)
                    {
                        WebChartControl1.Series.Add(series01);
                    }

                }


                //foreach (var VARIABLE in COLLECTION)
                //{

                //}
            }
            else
            {

            }

        }

        protected void ASPxButton2_Click(object sender, EventArgs e)
        {
            //WebChartControl chart = new WebChartControl();

            //// Add the chart to the form. Note that a chart isn't initialized,
            //// until it's added to the form's collection of controls.
            //this.Page.Form.Controls.Add(chart);

            // Create the first side-by-side bar series and add points to it.
            //Series series1 = new Series("Bar Series", ViewType.Bar);
            Series series1 = new Series("Line Series", ViewType.Line);
            series1.Points.Add(new SeriesPoint("I", new double[] { 17125.52 }));
            //series1.Points.Add(new SeriesPoint("II", new double[] { 12 }));
            //series1.Points.Add(new SeriesPoint("III", new double[] { 14 }));
            //series1.Points.Add(new SeriesPoint("IV", new double[] { 17 }));

            // Create the second side-by-side bar series and add points to it.
            Series series2 = new Series("Line Series", ViewType.Line);
            series2.Points.Add(new SeriesPoint("I", new double[] { 43598 }));
            //series2.Points.Add(new SeriesPoint("II", new double[] { 18 }));
            //series2.Points.Add(new SeriesPoint("III", new double[] { 25 }));
            //series2.Points.Add(new SeriesPoint("IV", new double[] { 33 }));

            // Add the series to the chart.
            WebChartControl1.Series.AddRange(new Series[] { series1, series2 });

            // Hide the legend (if necessary).
            //WebChartControl1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;

            // Rotate the diagram (if necessary).
            //((XYDiagram)WebChartControl1.Diagram).Rotated = true;
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
            Session[CVariablesSesion.IdUsuario] = "1802";

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
            detFecha.Date = objFeHoServ.datFechaHoraActu;
        }

        /// <summary>
        /// METODO. MCargaArbol
        /// </summary>
        private void MCargaArbol()
        {
            MLimpiaGrilla();
            decimal idUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]);
            //var lista2 = client.Get<EResultadoEntidad>(CAppSettings.svc_obtener_entidades + "/" + idUsuario + "?format=json");
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
            //ORIGEN: .JSON
            //var lstRegistros = new EResultadoCumplimientoDia();
            //using (StreamReader r = new StreamReader("d:\\convertcsv2.json"))
            //{
            //    string json = r.ReadToEnd();
            //    lstRegistros = JsonConvert.DeserializeObject<EResultadoCumplimientoDia>(json);
            //}
            //pvgReporte.DataSource = lstRegistros.OResultado;
            //pvgReporte.DataBind();
            //Session[CVariablesSesion.GrillaCabecera] = lstRegistros.OResultado;

            var objReporte = _client.Post<EResultadoCumplimientoDia>(CAppSettings.svc_listar_reporte_dia,
            new
            {
                strCredencial = CParametrosHydro.strCredencialFuncionario,
                decFiltro = 1,
                strParametro1 = "",
                decIdUsuario = Convert.ToDecimal(Session[CVariablesSesion.IdUsuario]),
                decIdTipoActividad = intIdActividad,
                decIdFechaOperacionDia = Convert.ToDecimal(detFecha.Date.Year + detFecha.Date.Month.ToString().PadLeft(2, '0') + detFecha.Date.Day.ToString().PadLeft(2,'0'))
            });
            if (objReporte.IntCodigo == 1)
            {
                //pvgReporte.DataSource = objReporte.OResultado;
                //pvgReporte.DataBind();
                Session[CVariablesSesion.GrillaCabecera] = objReporte.OResultado;
            }
            else
            {
                MImprimirMensaje(objReporte.StrMensaje, EnumTiposMensajeAlerta.Error);
            }
        }

        /// <summary>
        /// METODO. MValidaDatosIniciales
        /// </summary>
        /// <returns></returns>
        private bool MValidaDatosIniciales()
        {
            bool booResultado = true;
            O_FECHA_HORA_ACTUAL_CTY objFeHoServ = Session[CVariablesSesion.FechaHoraServ] as O_FECHA_HORA_ACTUAL_CTY;

            if (detFecha.Text == "")
            {
                MImprimirMensaje("Introduzca un día válido.", EnumTiposMensajeAlerta.Error);
                booResultado = false;
            }
            else if (Convert.ToInt32(detFecha.Date.Year+detFecha.Date.Month.ToString().PadLeft(2,'0')) > Convert.ToInt32(objFeHoServ.FECHA_HORA_ACTUAL.ToString().Substring(0,6)))
            {
                MImprimirMensaje("Introduzca una fecha válida.", EnumTiposMensajeAlerta.Error);
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
            //pvgReporte.DataSource = null;
            //pvgReporte.DataBind();

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