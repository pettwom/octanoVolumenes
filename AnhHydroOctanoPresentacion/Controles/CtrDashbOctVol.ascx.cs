using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using DevExpress.XtraCharts;
using DevExpress.XtraCharts.Web;
using DevExpress.Utils;

namespace AnhHydroOctanoPresentacion.Controles
{
    public partial class CtrDashbOctVol : System.Web.UI.UserControl
    {
       private JavaScriptSerializer serializer = new JavaScriptSerializer();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarDatosIniciales();
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            // Registrar datos del mapa en el HiddenField
            RegistrarScriptsMapa();
            
            // Configurar gráfico
            ConfigurarGrafico();
        }

        #region Configuración del Gráfico

        private void ConfigurarGrafico()
        {
            try
            {
                // Limpiar series existentes
                chartFuelType.Series.Clear();
                
                // Crear serie de tipo Pie
                Series series = new Series("Combustibles", ViewType.Pie);
                
                // Agregar puntos de datos
                DataTable dt = ObtenerConsumoPorTipo();
                foreach (DataRow row in dt.Rows)
                {
                    string tipo = row["Tipo"].ToString();
                    int porcentaje = Convert.ToInt32(row["Porcentaje"]);
                    series.Points.Add(new SeriesPoint(tipo, porcentaje));
                }
                
                // Configurar vista Pie
                PieSeriesView view = new PieSeriesView();
                series.View = view;
                
                // Configurar etiquetas
                series.LabelsVisibility = DefaultBoolean.True;
                series.Label.PointOptions.PointView = PointView.ArgumentAndValues;
                
                // Agregar serie al gráfico
                chartFuelType.Series.Add(series);
                
                // Configurar leyenda
                chartFuelType.Legend.Visible = true;
                chartFuelType.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Right;
                chartFuelType.Legend.AlignmentVertical = LegendAlignmentVertical.Top;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error configurando gráfico: " + ex.Message);
            }
        }

        #endregion

        #region Carga de Datos

        private void CargarDatosIniciales()
        {
            CargarKPIs();
            CargarRankings();
            CargarEstacionesCriticas();
            CargarDatosMapa();
        }

        private void CargarKPIs()
        {
            DataTable dt = ObtenerDatosNacionales();
            
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                lblStockTotal.Text = row["StockTotal"].ToString();
                lblConsumoPromedio.Text = row["ConsumoDiario"].ToString();
                lblEstacionesAlerta.Text = row["EstacionesAlerta"].ToString();
                lblDistribucionHoy.Text = row["DistribucionHoy"].ToString();
                
                lblCountNormal.Text = row["CountNormal"].ToString();
                lblCountWarning.Text = row["CountWarning"].ToString();
                lblCountCritical.Text = row["CountCritical"].ToString();
            }
        }

        private void CargarRankings()
        {
            DataTable dt = ObtenerDepartamentos();
            rptDepartamentos.DataSource = dt;
            rptDepartamentos.DataBind();
        }

        private void CargarEstacionesCriticas()
        {
            DataTable dt = ObtenerEstacionesCriticas();
            gvEstacionesCriticas.DataSource = dt;
            gvEstacionesCriticas.DataBind();
        }

        private void CargarDatosMapa()
        {
            DataTable dt = ObtenerEstacionesMapa();
            string json = DataTableToJson(dt);
            
            // Debug
            System.Diagnostics.Debug.WriteLine("=== CARGANDO MAPA ===");
            System.Diagnostics.Debug.WriteLine("Total de estaciones: " + dt.Rows.Count);
            System.Diagnostics.Debug.WriteLine("JSON generado (primeros 500 chars): " + 
                (json.Length > 500 ? json.Substring(0, 500) + "..." : json));
            
            hfMapData["stations"] = json;
        }

        #endregion

        #region Callbacks

        protected void cbpKPIs_Callback(object sender, CallbackEventArgsBase e)
        {
            string[] parameters = e.Parameter.Split('|');
            string action = parameters[0];
            string value = parameters.Length > 1 ? parameters[1] : string.Empty;

            System.Diagnostics.Debug.WriteLine("cbpKPIs_Callback - Action: " + action + ", Value: " + value);

            switch (action)
            {
                case "FILTER_DEPT":
                    if (value != "all")
                    {
                        ActualizarKPIsPorDepartamento(value);
                    }
                    else
                    {
                        CargarKPIs();
                    }
                    break;
                    
                case "REFRESH":
                    CargarKPIs();
                    break;
            }
        }

        protected void cbpRankings_Callback(object sender, CallbackEventArgsBase e)
        {
            System.Diagnostics.Debug.WriteLine("cbpRankings_Callback");
            CargarRankings();
        }

        protected void cbpCharts_Callback(object sender, CallbackEventArgsBase e)
        {
            System.Diagnostics.Debug.WriteLine("cbpCharts_Callback");
            CargarEstacionesCriticas();
        }

        protected void cbpStationDetail_Callback(object sender, CallbackEventArgsBase e)
        {
            string[] parameters = e.Parameter.Split('|');
            
            System.Diagnostics.Debug.WriteLine("cbpStationDetail_Callback - Params: " + e.Parameter);
            
            if (parameters[0] == "STATION_DETAIL" && parameters.Length > 1)
            {
                int stationId = int.Parse(parameters[1]);
                CargarDetalleEstacion(stationId);
            }
        }

        #endregion

        #region Métodos de Actualización

        private void ActualizarKPIsPorDepartamento(string deptCode)
        {
            DataTable dt = ObtenerDatosPorDepartamento(deptCode);
            
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                lblStockTotal.Text = row["StockTotal"].ToString();
                lblConsumoPromedio.Text = row["ConsumoDiario"].ToString();
                lblEstacionesAlerta.Text = row["EstacionesAlerta"].ToString();
                lblDistribucionHoy.Text = row["DistribucionHoy"].ToString();
            }
        }

        private void CargarDetalleEstacion(int stationId)
        {
            DataTable dt = ObtenerEstacionPorId(stationId);
            
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                lblStationName.Text = row["Name"].ToString();
                lblStationAddress.Text = row["Address"].ToString();
                lblStationStock.Text = string.Format("{0:N0} L", Convert.ToDecimal(row["Stock"]));
                lblStationCapacity.Text = string.Format("{0:N0} L", Convert.ToDecimal(row["Capacity"]));
                lblStationProducts.Text = "Productos: " + row["Products"].ToString();
                
                decimal stock = Convert.ToDecimal(row["Stock"]);
                decimal capacity = Convert.ToDecimal(row["Capacity"]);
                double percentage = (double)((stock / capacity) * 100);
                
                string status = row["Status"].ToString();
                string color = "#10b981";
                if (status == "critical") color = "#ef4444";
                else if (status == "warning") color = "#eab308";
                
                string script = string.Format(
                    "setTimeout(function() {{ " +
                    "var bar = document.getElementById('stationProgressBar'); " +
                    "if (bar) {{ " +
                    "bar.style.width = '{0}%'; " +
                    "bar.style.backgroundColor = '{1}'; " +
                    "}} }}, 100);",
                    percentage.ToString("0.0").Replace(",", "."),
                    color);
                
                ScriptManager.RegisterStartupScript(this, GetType(), "updateProgress", script, true);
            }
        }

        #endregion

        #region Helpers de UI

        protected string GetDiasColor(object dias)
        {
            if (dias == null) return "#f1f5f9";
            int d = Convert.ToInt32(dias);
            if (d < 3) return "#fee2e2";
            if (d < 7) return "#fef3c7";
            return "#d1fae5";
        }

        protected string GetDiasTextColor(object dias)
        {
            if (dias == null) return "#64748b";
            int d = Convert.ToInt32(dias);
            if (d < 3) return "#991b1b";
            if (d < 7) return "#92400e";
            return "#065f46";
        }

        protected string GetBarColor(object dias)
        {
            if (dias == null) return "#94a3b8";
            int d = Convert.ToInt32(dias);
            if (d < 3) return "#ef4444";
            if (d < 7) return "#eab308";
            return "#10b981";
        }

        protected string GetProductColor(string product)
        {
            if (string.IsNullOrEmpty(product)) return "#f1f5f9";
            
            switch (product.Trim())
            {
                case "GE": return "#dbeafe";
                case "GE+": return "#f3e8ff";
                case "DO": return "#fef3c7";
                default: return "#f1f5f9";
            }
        }

        protected string GetProductTextColor(string product)
        {
            if (string.IsNullOrEmpty(product)) return "#334155";
            
            switch (product.Trim())
            {
                case "GE": return "#1e40af";
                case "GE+": return "#6b21a8";
                case "DO": return "#92400e";
                default: return "#334155";
            }
        }

        protected string GetStatusColor(string status)
        {
            if (string.IsNullOrEmpty(status)) return "#94a3b8";
            
            switch (status.Trim())
            {
                case "Crítico": return "#ef4444";
                case "Bajo": return "#eab308";
                case "Normal": return "#10b981";
                default: return "#94a3b8";
            }
        }

        #endregion

        #region Datos Simulados

        private DataTable ObtenerDatosNacionales()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("StockTotal", typeof(string));
            dt.Columns.Add("ConsumoDiario", typeof(string));
            dt.Columns.Add("EstacionesAlerta", typeof(int));
            dt.Columns.Add("DistribucionHoy", typeof(string));
            dt.Columns.Add("CountNormal", typeof(int));
            dt.Columns.Add("CountWarning", typeof(int));
            dt.Columns.Add("CountCritical", typeof(int));

            dt.Rows.Add("12.4M L", "1.2M L", 18, "845K L", 229, 12, 6);
            return dt;
        }

        private DataTable ObtenerDatosPorDepartamento(string deptCode)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("StockTotal", typeof(string));
            dt.Columns.Add("ConsumoDiario", typeof(string));
            dt.Columns.Add("EstacionesAlerta", typeof(int));
            dt.Columns.Add("DistribucionHoy", typeof(string));

            switch (deptCode)
            {
                case "lp":
                    dt.Rows.Add("3.2M L", "320K L", 5, "210K L");
                    break;
                case "sc":
                    dt.Rows.Add("2.8M L", "450K L", 8, "280K L");
                    break;
                case "cb":
                    dt.Rows.Add("2.1M L", "280K L", 2, "150K L");
                    break;
                default:
                    return ObtenerDatosNacionales();
            }
            return dt;
        }

        private DataTable ObtenerDepartamentos()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Codigo", typeof(string));
            dt.Columns.Add("Nombre", typeof(string));
            dt.Columns.Add("DiasStock", typeof(int));
            dt.Columns.Add("Stock", typeof(decimal));
            dt.Columns.Add("StockFormato", typeof(string));
            dt.Columns.Add("Estaciones", typeof(int));
            dt.Columns.Add("Porcentaje", typeof(int));

            dt.Rows.Add("lp", "La Paz", 15, 3200000, "3.2M L", 64, 85);
            dt.Rows.Add("sc", "Santa Cruz", 8, 2800000, "2.8M L", 82, 60);
            dt.Rows.Add("cb", "Cochabamba", 12, 2100000, "2.1M L", 48, 75);
            dt.Rows.Add("or", "Oruro", 5, 890000, "890K L", 23, 35);
            dt.Rows.Add("pt", "Potosí", 18, 1200000, "1.2M L", 18, 90);
            dt.Rows.Add("ch", "Chuquisaca", 14, 980000, "980K L", 15, 82);
            dt.Rows.Add("tj", "Tarija", 10, 750000, "750K L", 12, 70);
            dt.Rows.Add("bn", "Beni", 9, 650000, "650K L", 8, 65);
            dt.Rows.Add("pd", "Pando", 7, 420000, "420K L", 5, 55);

            return dt;
        }

        private DataTable ObtenerConsumoPorTipo()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Tipo", typeof(string));
            dt.Columns.Add("Porcentaje", typeof(int));

            dt.Rows.Add("Gasolina Especial (GE)", 45);
            dt.Rows.Add("Gasolina Plus (GE+)", 30);
            dt.Rows.Add("Diesel Oil (DO)", 25);

            return dt;
        }

        private DataTable ObtenerEstacionesCriticas()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Nombre", typeof(string));
            dt.Columns.Add("Departamento", typeof(string));
            dt.Columns.Add("Producto", typeof(string));
            dt.Columns.Add("Stock", typeof(decimal));
            dt.Columns.Add("Estado", typeof(string));

            dt.Rows.Add(1, "Alto Lima S.R.L.", "La Paz", "GE", 117600, "Crítico");
            dt.Rows.Add(2, "Servicentro El Alto", "La Paz", "DO", 2340000, "Bajo");
            dt.Rows.Add(3, "Estación Patacamaya", "La Paz", "GE+", 890000, "Crítico");
            dt.Rows.Add(4, "Servicentro Oruro Centro", "Oruro", "GE", 1200000, "Bajo");
            dt.Rows.Add(10, "Oruro Principal", "Oruro", "DO", 1200, "Crítico");

            return dt;
        }

        private DataTable ObtenerEstacionesMapa()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Dept", typeof(string));
            dt.Columns.Add("Lat", typeof(double));
            dt.Columns.Add("Lng", typeof(double));
            dt.Columns.Add("Stock", typeof(decimal));
            dt.Columns.Add("Capacity", typeof(decimal));
            dt.Columns.Add("Status", typeof(string));
            dt.Columns.Add("Products", typeof(string));
            dt.Columns.Add("Address", typeof(string));

            // La Paz
            dt.Rows.Add(1, "Alto Lima S.R.L.", "lp", -16.4897, -68.1193, 117600, 500000, "critical", "GE,GE+,DO", "Av. 10 Esq. Calle Sucre, El Alto");
            dt.Rows.Add(2, "Servicentro La Paz Centro", "lp", -16.5004, -68.1223, 450000, 600000, "good", "GE,DO", "Av. Camacho, La Paz");
            dt.Rows.Add(3, "YPFB El Alto Norte", "lp", -16.5156, -68.1635, 120000, 800000, "warning", "GE+,DO", "Zona 12 de Octubre, El Alto");
            dt.Rows.Add(4, "Estación Patacamaya", "lp", -17.2333, -67.9167, 8900, 400000, "critical", "GE+,GE", "Carretera La Paz-Oruro Km 85");
            
            // Santa Cruz
            dt.Rows.Add(5, "Surtidor Santa Cruz Central", "sc", -17.7833, -63.1821, 350000, 1000000, "warning", "GE,GE+,DO", "Av. Cristo Redentor, Santa Cruz");
            dt.Rows.Add(6, "YPFB Plan Tres Mil", "sc", -17.8467, -63.1333, 780000, 1200000, "good", "GE,DO", "Av. Santos Dumont, Santa Cruz");
            dt.Rows.Add(7, "Estación Montero", "sc", -17.3500, -63.2500, 250000, 600000, "warning", "GE+,DO", "Carretera Norte, Montero");
            dt.Rows.Add(8, "Santa Cruz Sur", "sc", -17.8200, -63.1500, 520000, 900000, "good", "GE,GE+,DO", "4to Anillo Sur, Santa Cruz");
            
            // Cochabamba
            dt.Rows.Add(9, "Cochabamba Centro", "cb", -17.3895, -66.1568, 420000, 700000, "good", "GE,GE+,DO", "Av. Heroínas, Cochabamba");
            dt.Rows.Add(10, "Quillacollo Este", "cb", -17.4000, -66.2833, 380000, 650000, "good", "GE,DO", "Av. Blanco Galindo, Quillacollo");
            dt.Rows.Add(11, "Sacaba Norte", "cb", -17.3980, -66.0386, 290000, 550000, "good", "GE+,DO", "Av. Villarroel, Sacaba");
            
            // Oruro
            dt.Rows.Add(12, "Oruro Principal", "or", -17.9833, -67.1500, 12000, 450000, "critical", "GE,DO", "Av. Litoral, Oruro");
            dt.Rows.Add(13, "Oruro Sur", "or", -18.0167, -67.1333, 180000, 500000, "warning", "GE+,DO", "Av. Cívica, Oruro");
            
            // Potosí
            dt.Rows.Add(14, "Potosí Sur", "pt", -19.5836, -65.7531, 520000, 800000, "good", "GE+,GE,DO", "Av. Universitaria, Potosí");
            
            // Chuquisaca
            dt.Rows.Add(15, "Sucre Central", "ch", -19.0333, -65.2597, 310000, 550000, "good", "GE,DO", "Plaza 25 de Mayo, Sucre");
            
            // Tarija
            dt.Rows.Add(16, "Tarija Sur", "tj", -21.5333, -64.7333, 280000, 500000, "warning", "GE+,DO", "Av. Los Ceibos, Tarija");
            
            // Beni
            dt.Rows.Add(17, "Trinidad Centro", "bn", -14.8333, -64.9000, 330000, 600000, "good", "GE,GE+", "Av. Beni, Trinidad");
            
            // Pando
            dt.Rows.Add(18, "Cobija Principal", "pd", -11.0333, -68.7333, 150000, 400000, "warning", "GE,DO", "Av. Internacional, Cobija");

            return dt;
        }

        private DataTable ObtenerEstacionPorId(int id)
        {
            DataTable dt = ObtenerEstacionesMapa();
            DataView dv = dt.DefaultView;
            dv.RowFilter = "Id = " + id.ToString();
            return dv.ToTable();
        }

        #endregion

        #region Utilidades

        private string DataTableToJson(DataTable dt)
        {
            try
            {
                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                
                foreach (DataRow dr in dt.Rows)
                {
                    Dictionary<string, object> row = new Dictionary<string, object>();
                    
                    foreach (DataColumn col in dt.Columns)
                    {
                        row.Add(col.ColumnName, dr[col] == DBNull.Value ? null : dr[col]);
                    }
                    
                    rows.Add(row);
                }
                
                return serializer.Serialize(rows);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error en DataTableToJson: " + ex.Message);
                return "[]";
            }
        }

        private void RegistrarScriptsMapa()
        {
            try
            {
                if (hfMapData.Contains("stations"))
                {
                    string stations = hfMapData["stations"].ToString();
                    
                    System.Diagnostics.Debug.WriteLine("=== REGISTRANDO SCRIPTS DEL MAPA ===");
                    System.Diagnostics.Debug.WriteLine("Tamaño del JSON: " + stations.Length + " caracteres");
                    
                    // Escapar comillas para JavaScript
                    stations = stations.Replace("\\", "\\\\").Replace("'", "\\'").Replace("\r", "").Replace("\n", "");
                    
                    string script = string.Format(
                        "var initialStations = JSON.parse('{0}'); " +
                        "console.log('Datos iniciales cargados:', initialStations.length, 'estaciones'); " +
                        "if (typeof hfMapData !== 'undefined') {{ " +
                        "hfMapData.Set('stations', '{0}'); " +
                        "console.log('HiddenField actualizado'); " +
                        "}}",
                        stations);
                    
                    ScriptManager.RegisterStartupScript(this, GetType(), "initMapData", script, true);
                    
                    System.Diagnostics.Debug.WriteLine("Script registrado correctamente");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("ADVERTENCIA: hfMapData no contiene 'stations'");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error en RegistrarScriptsMapa: " + ex.Message);
            }
        }

        #endregion
    }
}