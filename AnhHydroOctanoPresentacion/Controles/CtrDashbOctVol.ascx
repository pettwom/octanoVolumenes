<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtrDashbOctVol.ascx.cs" Inherits="AnhHydroOctanoPresentacion.Controles.CtrDashbOctVol" %>

<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraCharts.v15.1.Web, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraCharts.Web" TagPrefix="dx" %>

<!-- Leaflet CSS -->
<link rel="stylesheet" href="https://unpkg.com/leaflet@1.9.4/dist/leaflet.css" />

<style>
    body { 
        font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; 
        background-color: #f1f5f9; 
        margin: 0; 
        padding: 0; 
    }
    
    /* Map Styles */
    #mapContainer { 
        height: 600px; 
        width: 100%; 
        border-radius: 12px; 
        z-index: 1;
    }
    
    .custom-marker {
        background: #10b981;
        border: 3px solid white;
        border-radius: 50%;
        box-shadow: 0 4px 6px -1px rgba(0, 0, 0, 0.3);
        display: flex;
        align-items: center;
        justify-content: center;
        color: white;
        font-weight: bold;
        font-size: 12px;
        width: 30px;
        height: 30px;
        cursor: pointer;
        transition: all 0.3s;
    }
    
    .custom-marker:hover { 
        transform: scale(1.2); 
    }
    
    .marker-critical { 
        background: #ef4444; 
        animation: pulse 2s infinite; 
    }
    
    .marker-warning { 
        background: #f59e0b; 
    }
    
    .marker-good { 
        background: #10b981; 
    }
    
    @keyframes pulse {
        0%, 100% { 
            box-shadow: 0 0 0 0 rgba(239, 68, 68, 0.7); 
        }
        70% { 
            box-shadow: 0 0 0 10px rgba(239, 68, 68, 0); 
        }
    }
    
    .glass-panel {
        background: rgba(255, 255, 255, 0.95);
        backdrop-filter: blur(12px);
        border: 1px solid rgba(255, 255, 255, 0.3);
        box-shadow: 0 4px 6px -1px rgba(0, 0, 0, 0.1);
        border-radius: 12px;
    }
    
    .metric-card {
        transition: all 0.3s ease;
        border-left: 4px solid transparent;
    }
    
    .metric-card:hover {
        transform: translateY(-2px);
        box-shadow: 0 10px 15px -3px rgba(0, 0, 0, 0.1);
    }
    
    .status-dot {
        height: 8px;
        width: 8px;
        border-radius: 50%;
        display: inline-block;
        margin-right: 6px;
    }
    
    .nav-button-active {
        background-color: #059669 !important;
        color: white !important;
        box-shadow: 0 4px 6px -1px rgba(0, 0, 0, 0.1);
        padding: 8px 16px;
        border-radius: 6px;
        font-size: 14px;
        font-weight: 500;
        border: none;
        cursor: pointer;
    }
    
    .nav-button-inactive {
        background-color: transparent !important;
        color: #cbd5e1 !important;
        padding: 8px 16px;
        border-radius: 6px;
        font-size: 14px;
        font-weight: 500;
        border: none;
        cursor: pointer;
    }
    
    .nav-button-inactive:hover {
        color: white !important;
    }
    
    .dept-card {
        cursor: pointer;
        transition: all 0.2s;
    }
    
    .dept-card:hover {
        background-color: #f8fafc;
    }
    
    .dept-card.active {
        border-left-color: #059669;
        background-color: #ecfdf5;
    }
    
    .custom-grid {
        width: 100%;
    }
    
    .custom-grid .dxgvHeader {
        background-color: #f8fafc;
        font-weight: bold;
    }
    
    .custom-grid .dxgvDataRow:hover {
        background-color: #f8fafc;
    }
</style>

<!-- Top Navigation -->
<nav style="background-color: #0f172a; color: white; position: sticky; top: 0; z-index: 50; box-shadow: 0 20px 25px -5px rgba(0, 0, 0, 0.1);">
    <div style="max-width: 100%; margin: 0 auto; padding: 0 24px;">
        <div style="display: flex; justify-content: space-between; height: 64px; align-items: center;">
            <div style="display: flex; align-items: center; gap: 16px;">
                <div style="background-color: #059669; padding: 8px; border-radius: 8px;">
                    <i class="fas fa-map-marked-alt" style="font-size: 20px;"></i>
                </div>
                <div>
                    <h1 style="font-weight: 700; font-size: 20px; letter-spacing: -0.025em; margin: 0;">DASHBO595</h1>
                    <p style="font-size: 12px; color: #94a3b8; margin: 0;">Sistema Nacional de Control de Combustibles - Bolivia</p>
                </div>
            </div>
            
            <div style="display: flex; align-items: center; gap: 24px;">
                <div style="display: flex; background-color: #1e293b; border-radius: 8px; padding: 4px;">
                    <dx:ASPxButton ID="btnNational" runat="server" Text="Vista Nacional" AutoPostBack="false" 
                        CssClass="nav-button-active" ClientInstanceName="btnNational">
                        <ClientSideEvents Click="function(s, e) { switchView('national'); }" />
                    </dx:ASPxButton>
                    <dx:ASPxButton ID="btnRegional" runat="server" Text="Regional" AutoPostBack="false" 
                        CssClass="nav-button-inactive" ClientInstanceName="btnRegional">
                        <ClientSideEvents Click="function(s, e) { switchView('regional'); }" />
                    </dx:ASPxButton>
                    <dx:ASPxButton ID="btnStation" runat="server" Text="Por Estación" AutoPostBack="false" 
                        CssClass="nav-button-inactive" ClientInstanceName="btnStation">
                        <ClientSideEvents Click="function(s, e) { switchView('station'); }" />
                    </dx:ASPxButton>
                </div>
                
                <div style="height: 32px; width: 1px; background-color: #334155;"></div>
                
                <div style="display: flex; align-items: center; gap: 12px; font-size: 14px;">
                    <span style="color: #94a3b8;">
                        <i class="far fa-calendar-alt" style="margin-right: 8px;"></i>
                        <dx:ASPxLabel ID="lblFechaActual" runat="server" Text="27/01/2026" />
                    </span>
                    <dx:ASPxButton ID="btnReporte" runat="server" Text="Reporte ANH" AutoPostBack="false" 
                        BackColor="#059669" ForeColor="White">
                        <Border BorderWidth="0px" />
                        <Paddings Padding="8px" PaddingLeft="16px" PaddingRight="16px" />
                        <ClientSideEvents Click="function(s, e) { generateReport(); }" />
                    </dx:ASPxButton>
                </div>
            </div>
        </div>
    </div>
</nav>

<div style="display: flex; height: calc(100vh - 64px); overflow: hidden;">
    
    <!-- Sidebar Filters -->
    <aside style="width: 320px; background-color: white; border-right: 1px solid #e2e8f0; overflow-y: auto; box-shadow: 0 4px 6px -1px rgba(0, 0, 0, 0.1); z-index: 40;">
        <div style="padding: 24px; border-bottom: 1px solid #e2e8f0;">
            <h2 style="font-weight: 700; font-size: 18px; color: #1e293b; margin-bottom: 4px;">Filtros Geográficos</h2>
            <p style="font-size: 12px; color: #64748b; margin: 0;">Refinar datos del territorio nacional</p>
        </div>

        <div style="padding: 24px;">
            <!-- Department Filter -->
            <div style="margin-bottom: 24px;">
                <label style="display: block; font-size: 14px; font-weight: 600; color: #334155; margin-bottom: 8px;">
                    <i class="fas fa-map-marker-alt" style="margin-right: 8px; color: #059669;"></i>Departamento
                </label>
                <dx:ASPxComboBox ID="cmbDepartamento" runat="server" NullText="Todos los Departamentos"
                    ValueType="System.String" ClientInstanceName="cmbDepartamento" Width="100%">
                    <Items>
                        <dx:ListEditItem Value="all" Text="Todos los Departamentos" Selected="true" />
                        <dx:ListEditItem Value="lp" Text="La Paz" />
                        <dx:ListEditItem Value="sc" Text="Santa Cruz" />
                        <dx:ListEditItem Value="cb" Text="Cochabamba" />
                        <dx:ListEditItem Value="or" Text="Oruro" />
                        <dx:ListEditItem Value="pt" Text="Potosí" />
                        <dx:ListEditItem Value="ch" Text="Chuquisaca" />
                        <dx:ListEditItem Value="tj" Text="Tarija" />
                        <dx:ListEditItem Value="bn" Text="Beni" />
                        <dx:ListEditItem Value="pd" Text="Pando" />
                    </Items>
                    <ClientSideEvents SelectedIndexChanged="function(s, e) { filterByDepartment(s.GetValue()); }" />
                </dx:ASPxComboBox>
            </div>

            <!-- Product Filter -->
            <div style="margin-bottom: 24px;">
                <label style="display: block; font-size: 14px; font-weight: 600; color: #334155; margin-bottom: 8px;">
                    <i class="fas fa-oil-can" style="margin-right: 8px; color: #2563eb;"></i>Tipo de Combustible
                </label>
                <div style="margin-bottom: 8px;">
                    <dx:ASPxCheckBox ID="chkGE" runat="server" Text="Gasolina Especial (GE)" Checked="true" 
                        ClientInstanceName="chkGE">
                        <ClientSideEvents CheckedChanged="function(s, e) { updateMapFilters(); }" />
                    </dx:ASPxCheckBox>
                    <span style="float: right; margin-top: -20px; font-size: 12px; background-color: #dbeafe; color: #1e40af; padding: 2px 8px; border-radius: 9999px;">45%</span>
                </div>
                <div style="margin-bottom: 8px;">
                    <dx:ASPxCheckBox ID="chkGEPlus" runat="server" Text="Gasolina Plus (GE+)" Checked="true" 
                        ClientInstanceName="chkGEPlus">
                        <ClientSideEvents CheckedChanged="function(s, e) { updateMapFilters(); }" />
                    </dx:ASPxCheckBox>
                    <span style="float: right; margin-top: -20px; font-size: 12px; background-color: #f3e8ff; color: #6b21a8; padding: 2px 8px; border-radius: 9999px;">30%</span>
                </div>
                <div>
                    <dx:ASPxCheckBox ID="chkDO" runat="server" Text="Diesel Oil (DO)" Checked="true" 
                        ClientInstanceName="chkDO">
                        <ClientSideEvents CheckedChanged="function(s, e) { updateMapFilters(); }" />
                    </dx:ASPxCheckBox>
                    <span style="float: right; margin-top: -20px; font-size: 12px; background-color: #fef3c7; color: #92400e; padding: 2px 8px; border-radius: 9999px;">25%</span>
                </div>
            </div>

            <!-- Status Filter -->
            <div style="margin-bottom: 24px;">
                <label style="display: block; font-size: 14px; font-weight: 600; color: #334155; margin-bottom: 8px;">
                    <i class="fas fa-exclamation-circle" style="margin-right: 8px; color: #ea580c;"></i>Estado de Stock
                </label>
                <div style="margin-bottom: 8px;">
                    <dx:ASPxButton ID="btnStatusAll" runat="server" Text="Todos los estados" AutoPostBack="false" 
                        BackColor="#f1f5f9" ForeColor="#334155" Width="100%">
                        <Border BorderWidth="1px" BorderColor="#e2e8f0" />
                        <Paddings Padding="8px" PaddingLeft="12px" PaddingRight="12px" />
                        <ClientSideEvents Click="function(s, e) { filterByStatus('all'); }" />
                    </dx:ASPxButton>
                </div>
                <div style="margin-bottom: 8px;">
                    <dx:ASPxButton ID="btnStatusCritical" runat="server" Text="Stock Crítico (< 3 días)" AutoPostBack="false" 
                        BackColor="Transparent" ForeColor="#334155" Width="100%">
                        <Border BorderWidth="1px" BorderColor="Transparent" />
                        <Paddings Padding="8px" PaddingLeft="12px" PaddingRight="12px" />
                        <ClientSideEvents Click="function(s, e) { filterByStatus('critical'); }" />
                    </dx:ASPxButton>
                </div>
                <div style="margin-bottom: 8px;">
                    <dx:ASPxButton ID="btnStatusWarning" runat="server" Text="Stock Bajo (3-7 días)" AutoPostBack="false" 
                        BackColor="Transparent" ForeColor="#334155" Width="100%">
                        <Border BorderWidth="1px" BorderColor="Transparent" />
                        <Paddings Padding="8px" PaddingLeft="12px" PaddingRight="12px" />
                        <ClientSideEvents Click="function(s, e) { filterByStatus('warning'); }" />
                    </dx:ASPxButton>
                </div>
                <div>
                    <dx:ASPxButton ID="btnStatusGood" runat="server" Text="Stock Normal (> 7 días)" AutoPostBack="false" 
                        BackColor="Transparent" ForeColor="#334155" Width="100%">
                        <Border BorderWidth="1px" BorderColor="Transparent" />
                        <Paddings Padding="8px" PaddingLeft="12px" PaddingRight="12px" />
                        <ClientSideEvents Click="function(s, e) { filterByStatus('good'); }" />
                    </dx:ASPxButton>
                </div>
            </div>

            <!-- Date Range -->
            <div style="margin-bottom: 24px;">
                <label style="display: block; font-size: 14px; font-weight: 600; color: #334155; margin-bottom: 8px;">
                    <i class="far fa-calendar" style="margin-right: 8px; color: #475569;"></i>Rango de Fechas
                </label>
                <dx:ASPxDateEdit ID="deFechaInicio" runat="server" Date="2026-01-01" 
                    DisplayFormatString="yyyy-MM-dd" EditFormatString="yyyy-MM-dd" Width="100%" style="margin-bottom: 8px;">
                </dx:ASPxDateEdit>
                <dx:ASPxDateEdit ID="deFechaFin" runat="server" Date="2026-01-27" 
                    DisplayFormatString="yyyy-MM-dd" EditFormatString="yyyy-MM-dd" Width="100%">
                </dx:ASPxDateEdit>
            </div>

            <dx:ASPxButton ID="btnResetFilters" runat="server" Text="Restablecer Filtros" AutoPostBack="false" 
                BackColor="Transparent" ForeColor="#475569" Width="100%">
                <Border BorderWidth="1px" BorderColor="#cbd5e1" />
                <Paddings Padding="8px" />
                <ClientSideEvents Click="function(s, e) { resetFilters(); }" />
            </dx:ASPxButton>
        </div>

        <!-- Mini Stats in Sidebar -->
        <div style="background-color: #f8fafc; padding: 24px; border-top: 1px solid #e2e8f0; margin-top: auto;">
            <h3 style="font-weight: 600; font-size: 14px; color: #334155; margin-bottom: 12px;">Resumen Nacional</h3>
            <div style="margin-bottom: 12px;">
                <div style="display: flex; justify-content: space-between; font-size: 14px; margin-bottom: 8px;">
                    <span style="color: #475569;">Estaciones Activas</span>
                    <dx:ASPxLabel ID="lblEstacionesActivas" runat="server" Text="247" Font-Bold="true" ForeColor="#0f172a" />
                </div>
                <div style="display: flex; justify-content: space-between; font-size: 14px; margin-bottom: 8px;">
                    <span style="color: #475569;">Departamentos</span>
                    <dx:ASPxLabel ID="lblDepartamentos" runat="server" Text="9/9" Font-Bold="true" ForeColor="#0f172a" />
                </div>
                <div style="display: flex; justify-content: space-between; font-size: 14px;">
                    <span style="color: #475569;">Volumen Total</span>
                    <dx:ASPxLabel ID="lblVolumenTotal" runat="server" Text="12.4M L" Font-Bold="true" ForeColor="#059669" />
                </div>
            </div>
        </div>
    </aside>

    <!-- Main Content Area -->
    <main style="flex: 1; overflow-y: auto; background-color: #f1f5f9; padding: 24px;">
        
        <!-- KPIs Section with Callback -->
        <dx:ASPxCallbackPanel ID="cbpKPIs" runat="server" ClientInstanceName="cbpKPIs" OnCallback="cbpKPIs_Callback">
            <PanelCollection>
                <dx:PanelContent>
                    <!-- National KPIs -->
                    <div style="display: grid; grid-template-columns: repeat(4, 1fr); gap: 24px; margin-bottom: 24px;">
                        <div class="glass-panel metric-card" style="padding: 24px; border-left: 4px solid #3b82f6;">
                            <div style="display: flex; justify-content: space-between; align-items: flex-start;">
                                <div>
                                    <p style="font-size: 14px; font-weight: 500; color: #475569; margin-bottom: 4px;">Stock Nacional Total</p>
                                    <dx:ASPxLabel ID="lblStockTotal" runat="server" Text="12.4M L" Font-Size="24px" Font-Bold="true" ForeColor="#0f172a" />
                                    <p style="font-size: 12px; color: #059669; margin-top: 4px;">
                                        <i class="fas fa-arrow-up" style="margin-right: 4px;"></i>+5.3% vs mes anterior
                                    </p>
                                </div>
                                <div style="padding: 12px; background-color: #dbeafe; border-radius: 8px;">
                                    <i class="fas fa-database" style="color: #2563eb; font-size: 20px;"></i>
                                </div>
                            </div>
                        </div>

                        <div class="glass-panel metric-card" style="padding: 24px; border-left: 4px solid #10b981;">
                            <div style="display: flex; justify-content: space-between; align-items: flex-start;">
                                <div>
                                    <p style="font-size: 14px; font-weight: 500; color: #475569; margin-bottom: 4px;">Consumo Diario Promedio</p>
                                    <dx:ASPxLabel ID="lblConsumoPromedio" runat="server" Text="1.2M L" Font-Size="24px" Font-Bold="true" ForeColor="#0f172a" />
                                    <p style="font-size: 12px; color: #64748b; margin-top: 4px;">Cobertura promedio: 10.3 días</p>
                                </div>
                                <div style="padding: 12px; background-color: #d1fae5; border-radius: 8px;">
                                    <i class="fas fa-chart-line" style="color: #059669; font-size: 20px;"></i>
                                </div>
                            </div>
                        </div>

                        <div class="glass-panel metric-card" style="padding: 24px; border-left: 4px solid #f97316;">
                            <div style="display: flex; justify-content: space-between; align-items: flex-start;">
                                <div>
                                    <p style="font-size: 14px; font-weight: 500; color: #475569; margin-bottom: 4px;">Estaciones en Alerta</p>
                                    <div style="display: flex; align-items: baseline; gap: 8px;">
                                        <dx:ASPxLabel ID="lblEstacionesAlerta" runat="server" Text="18" Font-Size="24px" Font-Bold="true" ForeColor="#0f172a" />
                                        <span style="font-size: 14px; color: #64748b;">/ 247</span>
                                    </div>
                                    <p style="font-size: 12px; color: #ea580c; margin-top: 4px;">
                                        <i class="fas fa-exclamation-triangle" style="margin-right: 4px;"></i>Requieren atención inmediata
                                    </p>
                                </div>
                                <div style="padding: 12px; background-color: #ffedd5; border-radius: 8px;">
                                    <i class="fas fa-bell" style="color: #ea580c; font-size: 20px;"></i>
                                </div>
                            </div>
                        </div>

                        <div class="glass-panel metric-card" style="padding: 24px; border-left: 4px solid #8b5cf6;">
                            <div style="display: flex; justify-content: space-between; align-items: flex-start;">
                                <div>
                                    <p style="font-size: 14px; font-weight: 500; color: #475569; margin-bottom: 4px;">Distribución Hoy</p>
                                    <dx:ASPxLabel ID="lblDistribucionHoy" runat="server" Text="845K L" Font-Size="24px" Font-Bold="true" ForeColor="#0f172a" />
                                    <p style="font-size: 12px; color: #059669; margin-top: 4px;">
                                        <i class="fas fa-truck" style="margin-right: 4px;"></i>32 camiones en ruta
                                    </p>
                                </div>
                                <div style="padding: 12px; background-color: #f3e8ff; border-radius: 8px;">
                                    <i class="fas fa-shipping-fast" style="color: #7c3aed; font-size: 20px;"></i>
                                </div>
                            </div>
                        </div>
                    </div>
                </dx:PanelContent>
            </PanelCollection>
        </dx:ASPxCallbackPanel>

        <!-- Map Section (WITHOUT CALLBACK - STATIC) -->
        <div style="display: grid; grid-template-columns: 2fr 1fr; gap: 24px; margin-bottom: 24px;">
            
            <!-- Map Container - FUERA DEL CALLBACK PANEL -->
            <div class="glass-panel" style="padding: 16px;">
                <div style="display: flex; justify-content: space-between; align-items: center; margin-bottom: 16px;">
                    <h3 style="font-weight: 700; font-size: 18px; color: #1e293b; margin: 0;">Mapa de Distribución Nacional</h3>
                    <div style="display: flex; gap: 8px;">
                        <dx:ASPxButton ID="btnZoomLP" runat="server" Text="La Paz" AutoPostBack="false" ClientInstanceName="btnZoomLP">
                            <ClientSideEvents Click="function(s, e) { zoomToDept('lp'); }" />
                        </dx:ASPxButton>
                        <dx:ASPxButton ID="btnZoomSC" runat="server" Text="Santa Cruz" AutoPostBack="false" ClientInstanceName="btnZoomSC">
                            <ClientSideEvents Click="function(s, e) { zoomToDept('sc'); }" />
                        </dx:ASPxButton>
                        <dx:ASPxButton ID="btnZoomCB" runat="server" Text="Cochabamba" AutoPostBack="false" ClientInstanceName="btnZoomCB">
                            <ClientSideEvents Click="function(s, e) { zoomToDept('cb'); }" />
                        </dx:ASPxButton>
                        <dx:ASPxButton ID="btnResetMap" runat="server" Text="Ver Todo" AutoPostBack="false" 
                            BackColor="#d1fae5" ForeColor="#059669" ClientInstanceName="btnResetMap">
                            <Border BorderWidth="0px" />
                            <ClientSideEvents Click="function(s, e) { resetMap(); }" />
                        </dx:ASPxButton>
                    </div>
                </div>
                <div id="mapContainer"></div>
                <dx:ASPxHiddenField ID="hfMapData" runat="server" ClientInstanceName="hfMapData" />
            </div>

            <!-- Regional Rankings with Callback -->
            <dx:ASPxCallbackPanel ID="cbpRankings" runat="server" ClientInstanceName="cbpRankings" OnCallback="cbpRankings_Callback">
                <PanelCollection>
                    <dx:PanelContent>
                        <div class="glass-panel" style="padding: 16px; display: flex; flex-direction: column;">
                            <h3 style="font-weight: 700; font-size: 18px; color: #1e293b; margin-bottom: 16px;">Ranking por Departamento</h3>
                            
                            <asp:Repeater ID="rptDepartamentos" runat="server">
                                <ItemTemplate>
                                    <div class="dept-card metric-card" style="padding: 12px; border-radius: 8px; background-color: white; border: 1px solid #e2e8f0; margin-bottom: 12px;"
                                         onclick='zoomToDept("<%# Eval("Codigo") %>")'>
                                        <div style="display: flex; justify-content: space-between; align-items: center; margin-bottom: 8px;">
                                            <span style="font-weight: 600; color: #334155;"><%# Eval("Nombre") %></span>
                                            <span style="font-size: 12px; padding: 4px 8px; border-radius: 9999px; 
                                                background-color: <%# GetDiasColor(Eval("DiasStock")) %>; 
                                                color: <%# GetDiasTextColor(Eval("DiasStock")) %>;">
                                                <%# Eval("DiasStock") %> días
                                            </span>
                                        </div>
                                        <div style="width: 100%; background-color: #e2e8f0; border-radius: 9999px; height: 8px; margin-bottom: 8px;">
                                            <div style="background-color: <%# GetBarColor(Eval("DiasStock")) %>; height: 8px; border-radius: 9999px; width: <%# Eval("Porcentaje") %>%"></div>
                                        </div>
                                        <div style="display: flex; justify-content: space-between; font-size: 12px; color: #64748b;">
                                            <span>Stock: <%# Eval("StockFormato") %></span>
                                            <span><%# Eval("Estaciones") %> estaciones</span>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>

                            <dx:ASPxButton ID="btnVerReporteCompleto" runat="server" Text="Ver reporte completo" AutoPostBack="false" 
                                BackColor="#f1f5f9" ForeColor="#334569">
                                <Border BorderWidth="0px" />
                                <Paddings Padding="12px" />
                                <ClientSideEvents Click="function(s, e) { showReport(); }" />
                            </dx:ASPxButton>
                        </div>
                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxCallbackPanel>
        </div>

        <!-- Bottom Charts & Tables with Callback -->
        <dx:ASPxCallbackPanel ID="cbpCharts" runat="server" ClientInstanceName="cbpCharts" OnCallback="cbpCharts_Callback">
            <PanelCollection>
                <dx:PanelContent>
                    <div style="display: grid; grid-template-columns: 1fr 1fr; gap: 24px;">
                        
                        <!-- Consumption Chart -->
                        <div class="glass-panel" style="padding: 16px;">
                            <h3 style="font-weight: 700; font-size: 18px; color: #1e293b; margin-bottom: 16px;">Consumo por Tipo de Combustible</h3>
                            
                            <dx:WebChartControl ID="chartFuelType" runat="server" Height="600px" Width="700px"
                                ClientInstanceName="chartFuelType">
                                <Legend AlignmentHorizontal="Right" AlignmentVertical="Top" Visible="True" />
                            </dx:WebChartControl>
                        </div>

                        <!-- Critical Stations Table -->
                        <div class="glass-panel" style="overflow: hidden;">
                            <div style="padding: 16px; border-bottom: 1px solid #e2e8f0; background-color: #fef2f2; display: flex; justify-content: space-between; align-items: center;">
                                <h3 style="font-weight: 700; font-size: 18px; color: #991b1b; margin: 0;">
                                    <i class="fas fa-exclamation-triangle" style="margin-right: 8px;"></i>Estaciones con Stock Crítico
                                </h3>
                                <span style="font-size: 12px; background-color: #fecaca; color: #991b1b; padding: 4px 8px; border-radius: 9999px;">Requieren abastecimiento</span>
                            </div>
                            
                            <dx:ASPxGridView ID="gvEstacionesCriticas" runat="server" AutoGenerateColumns="False"
                                KeyFieldName="Id" ClientInstanceName="gvEstacionesCriticas" CssClass="custom-grid" Width="100%">
                                <Columns>
                                    <dx:GridViewDataTextColumn FieldName="Nombre" Caption="Estación" Width="30%">
                                        <CellStyle Font-Bold="True" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="Departamento" Caption="Depto" Width="15%" />
                                    <dx:GridViewDataTextColumn FieldName="Producto" Caption="Producto" Width="15%">
                                        <DataItemTemplate>
                                            <span style="font-size: 12px; padding: 2px 8px; border-radius: 4px; 
                                                background-color: <%# GetProductColor(Container.Text) %>; 
                                                color: <%# GetProductTextColor(Container.Text) %>;">
                                                <%# Container.Text %>
                                            </span>
                                        </DataItemTemplate>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="Stock" Caption="Stock" Width="20%">
                                        <PropertiesTextEdit DisplayFormatString="{0:N0} L" />
                                        <CellStyle HorizontalAlign="Right" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="Estado" Caption="Estado" Width="20%">
                                        <DataItemTemplate>
                                            <span class="status-dot" style="background-color: <%# GetStatusColor(Container.Text) %>;"></span>
                                            <%# Container.Text %>
                                        </DataItemTemplate>
                                    </dx:GridViewDataTextColumn>
                                </Columns>
                                <Settings ShowFooter="False" VerticalScrollBarMode="Auto" VerticalScrollableHeight="200" />
                                <SettingsPager Mode="ShowAllRecords" />
                                <Styles>
                                    <Header BackColor="#f8fafc" Font-Bold="True" />
                                </Styles>
                            </dx:ASPxGridView>
                            
                            <div style="padding: 12px; border-top: 1px solid #e2e8f0; background-color: #f8fafc; text-align: center;">
                                <dx:ASPxHyperLink ID="lnkVerAlertas" runat="server" Text="Ver todas las alertas →" 
                                    NavigateUrl="javascript:void(0);" ForeColor="#059669" Font-Bold="True" Font-Size="14px">
                                    <ClientSideEvents Click="function(s, e) { showAllAlerts(); e.processOnServer = false; }" />
                                </dx:ASPxHyperLink>
                            </div>
                        </div>
                    </div>
                </dx:PanelContent>
            </PanelCollection>
        </dx:ASPxCallbackPanel>

        <!-- Footer Stats -->
        <div class="glass-panel" style="margin-top: 24px; padding: 16px; display: flex; justify-content: space-between; align-items: center; font-size: 14px; color: #475569;">
            <div style="display: flex; align-items: center; gap: 24px;">
                <span><i class="fas fa-circle" style="color: #10b981; margin-right: 8px;"></i>Normal: <dx:ASPxLabel ID="lblCountNormal" runat="server" Text="229" /> estaciones</span>
                <span><i class="fas fa-circle" style="color: #eab308; margin-right: 8px;"></i>Bajo: <dx:ASPxLabel ID="lblCountWarning" runat="server" Text="12" /> estaciones</span>
                <span><i class="fas fa-circle" style="color: #ef4444; margin-right: 8px;"></i>Crítico: <dx:ASPxLabel ID="lblCountCritical" runat="server" Text="6" /> estaciones</span>
            </div>
            <div style="display: flex; align-items: center; gap: 16px;">
                <span>Última actualización: <dx:ASPxLabel ID="lblUltimaActualizacion" runat="server" Text="Hace 5 minutos" /></span>
                <dx:ASPxButton ID="btnRefresh" runat="server" Text="Actualizar" AutoPostBack="false" 
                    RenderMode="Link" ForeColor="#059669" Font-Size="14px" ClientInstanceName="btnRefresh">
                    <ClientSideEvents Click="function(s, e) { refreshData(); }" />
                </dx:ASPxButton>
            </div>
        </div>
        
    </main>
</div>

<!-- Popup para detalles de estación -->
<dx:ASPxPopupControl ID="popupStation" runat="server" ClientInstanceName="popupStation" 
    HeaderText="Detalle de Estación" Width="500px" Height="400px" 
    Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
    <ContentCollection>
        <dx:PopupControlContentControl>
            <dx:ASPxCallbackPanel ID="cbpStationDetail" runat="server" ClientInstanceName="cbpStationDetail" OnCallback="cbpStationDetail_Callback">
                <PanelCollection>
                    <dx:PanelContent>
                        <div style="padding: 16px;">
                            <dx:ASPxLabel ID="lblStationName" runat="server" Font-Bold="True" Font-Size="18px" />
                            <dx:ASPxLabel ID="lblStationAddress" runat="server" ForeColor="#64748b" style="display: block; margin: 8px 0;" />
                            
                            <div style="margin: 16px 0;">
                                <div style="display: flex; justify-content: space-between; margin-bottom: 8px;">
                                    <span>Stock actual:</span>
                                    <dx:ASPxLabel ID="lblStationStock" runat="server" Font-Bold="True" />
                                </div>
                                <div style="display: flex; justify-content: space-between; margin-bottom: 8px;">
                                    <span>Capacidad:</span>
                                    <dx:ASPxLabel ID="lblStationCapacity" runat="server" />
                                </div>
                                <div style="width: 100%; background-color: #e2e8f0; border-radius: 9999px; height: 8px; margin: 8px 0;">
                                    <div id="stationProgressBar" style="height: 8px; border-radius: 9999px; background-color: #059669; width: 0%"></div>
                                </div>
                            </div>
                            
                            <dx:ASPxLabel ID="lblStationProducts" runat="server" style="display: block; margin: 16px 0;" />
                            
                            <dx:ASPxButton ID="btnClosePopup" runat="server" Text="Cerrar" AutoPostBack="false" 
                                BackColor="#059669" ForeColor="White">
                                <Border BorderWidth="0px" />
                                <ClientSideEvents Click="function(s, e) { popupStation.Hide(); }" />
                            </dx:ASPxButton>
                        </div>
                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxCallbackPanel>
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>

<!-- Leaflet JS -->
<script src="https://unpkg.com/leaflet@1.9.4/dist/leaflet.js"></script>

<!-- Custom JavaScript -->
<script type="text/javascript">
    var map;
    var markers = [];
    var currentStations = [];
    var mapInitialized = false;

    // Inicialización cuando el DOM está listo
    $(document).ready(function () {
        console.log('DOM Ready - Inicializando mapa...');

        // Verificar que el contenedor existe
        if ($('#mapContainer').length > 0) {
            console.log('Contenedor del mapa encontrado');

            // Inicializar mapa inmediatamente
            initMap();

            // Cargar datos después de un pequeño delay para asegurar que todo esté listo
            setTimeout(function () {
                console.log('Cargando datos de estaciones...');
                loadStationsData();
            }, 500);
        } else {
            console.error('Contenedor del mapa NO encontrado');
        }
    });

    function initMap() {
        try {
            console.log('Iniciando mapa de Leaflet...');

            // Crear mapa centrado en Bolivia
            map = L.map('mapContainer').setView([-17.7833, -63.1821], 6);

            // Agregar capa de tiles
            L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
                attribution: '© OpenStreetMap contributors',
                maxZoom: 18
            }).addTo(map);

            mapInitialized = true;
            console.log('Mapa inicializado correctamente');

            // Forzar recalculo del tamaño del mapa
            setTimeout(function () {
                map.invalidateSize();
            }, 100);

        } catch (error) {
            console.error('Error al inicializar mapa:', error);
        }
    }

    function loadStationsData() {
        try {
            console.log('Intentando cargar datos del HiddenField...');

            // Verificar si hfMapData existe y tiene datos
            if (typeof hfMapData !== 'undefined' && hfMapData.Get('stations')) {
                var stationsJson = hfMapData.Get('stations');
                console.log('Datos encontrados en HiddenField:', stationsJson);

                var stations = JSON.parse(stationsJson);
                console.log('Estaciones parseadas:', stations.length);

                renderMarkers(stations);
            } else {
                console.warn('No hay datos en hfMapData, intentando cargar...');
                // Si no hay datos, intentar cargar mediante callback
                setTimeout(function () {
                    if (typeof hfMapData !== 'undefined' && hfMapData.Get('stations')) {
                        var stationsJson = hfMapData.Get('stations');
                        var stations = JSON.parse(stationsJson);
                        renderMarkers(stations);
                    }
                }, 1000);
            }
        } catch (error) {
            console.error('Error al cargar datos de estaciones:', error);
        }
    }

    function renderMarkers(stations) {
        try {
            console.log('Renderizando marcadores:', stations ? stations.length : 0);

            if (!map || !mapInitialized) {
                console.error('Mapa no inicializado');
                return;
            }

            // Limpiar marcadores existentes
            markers.forEach(function (marker) {
                map.removeLayer(marker);
            });
            markers = [];

            if (!stations || stations.length === 0) {
                console.warn('No hay estaciones para mostrar');
                return;
            }

            currentStations = stations;

            stations.forEach(function (station) {
                try {
                    var colorClass = station.Status === 'critical' ? 'marker-critical' :
                                    station.Status === 'warning' ? 'marker-warning' : 'marker-good';

                    var icon = L.divIcon({
                        className: 'custom-marker ' + colorClass,
                        html: '<div style="width: 100%; height: 100%; display: flex; align-items: center; justify-content: center;">' + station.Id + '</div>',
                        iconSize: [30, 30],
                        iconAnchor: [15, 15]
                    });

                    var marker = L.marker([station.Lat, station.Lng], { icon: icon })
                        .addTo(map)
                        .bindPopup(createPopupContent(station));

                    marker.on('click', function () {
                        viewStationDetails(station.Id);
                    });

                    markers.push(marker);
                } catch (err) {
                    console.error('Error creando marcador para estación ' + station.Id, err);
                }
            });

            console.log('Marcadores renderizados:', markers.length);

            // Ajustar vista del mapa
            if (markers.length > 0) {
                var group = new L.featureGroup(markers);
                map.fitBounds(group.getBounds().pad(0.1));
            }

        } catch (error) {
            console.error('Error en renderMarkers:', error);
        }
    }

    function createPopupContent(station) {
        var percentage = ((station.Stock / station.Capacity) * 100).toFixed(1);
        var statusColor = station.Status === 'critical' ? 'red' : station.Status === 'warning' ? 'yellow' : 'green';
        var statusText = station.Status === 'critical' ? 'Crítico' : station.Status === 'warning' ? 'Bajo' : 'Normal';
        var statusBg = statusColor === 'red' ? '#fee2e2' : statusColor === 'yellow' ? '#fef3c7' : '#d1fae5';
        var statusFg = statusColor === 'red' ? '#991b1b' : statusColor === 'yellow' ? '#92400e' : '#065f46';
        var barColor = statusColor === 'red' ? '#ef4444' : statusColor === 'yellow' ? '#eab308' : '#10b981';

        return '<div style="padding: 16px; min-width: 250px;">' +
            '<div style="display: flex; justify-content: space-between; align-items: flex-start; margin-bottom: 12px;">' +
            '<h3 style="font-weight: 700; color: #1e293b; margin: 0; font-size: 16px;">' + station.Name + '</h3>' +
            '<span style="background-color: ' + statusBg + '; color: ' + statusFg + '; font-size: 12px; padding: 4px 8px; border-radius: 9999px; white-space: nowrap;">' + statusText + '</span>' +
            '</div>' +
            '<p style="font-size: 12px; color: #64748b; margin-bottom: 12px;">' + station.Address + '</p>' +
            '<div style="margin-bottom: 12px;">' +
            '<div style="display: flex; justify-content: space-between; font-size: 14px; margin-bottom: 8px;">' +
            '<span style="color: #475569;">Stock actual:</span>' +
            '<span style="font-family: monospace; font-weight: 700;">' + station.Stock.toLocaleString() + ' L</span>' +
            '</div>' +
            '<div style="display: flex; justify-content: space-between; font-size: 14px; margin-bottom: 8px;">' +
            '<span style="color: #475569;">Capacidad:</span>' +
            '<span style="font-family: monospace;">' + station.Capacity.toLocaleString() + ' L</span>' +
            '</div>' +
            '<div style="width: 100%; background-color: #e2e8f0; border-radius: 9999px; height: 8px;">' +
            '<div style="background-color: ' + barColor + '; height: 8px; border-radius: 9999px; width: ' + percentage + '%"></div>' +
            '</div>' +
            '<p style="font-size: 12px; text-align: right; color: #64748b; margin-top: 4px;">' + percentage + '% ocupado</p>' +
            '</div>' +
            '<div style="display: flex; gap: 4px; margin-bottom: 12px; flex-wrap: wrap;">' +
            station.Products.split(',').map(function (p) {
                return '<span style="font-size: 12px; background-color: #f1f5f9; padding: 4px 8px; border-radius: 4px;">' + p.trim() + '</span>';
            }).join('') +
            '</div>' +
            '<button onclick="viewStationDetails(' + station.Id + ')" style="width: 100%; background-color: #059669; color: white; padding: 8px; border-radius: 8px; border: none; cursor: pointer; font-size: 14px; font-weight: 500;">Ver Detalles</button>' +
            '</div>';
    }

    function filterByDepartment(dept) {
        console.log('Filtrando por departamento:', dept);

        if (!currentStations || currentStations.length === 0) {
            console.warn('No hay estaciones cargadas para filtrar');
            return;
        }

        var filtered = dept === 'all'
            ? currentStations
            : currentStations.filter(function (s) { return s.Dept === dept; });

        console.log('Estaciones filtradas:', filtered.length);
        renderMarkers(filtered);

        // Actualizar KPIs si es necesario
        if (typeof cbpKPIs !== 'undefined') {
            cbpKPIs.PerformCallback('FILTER_DEPT|' + dept);
        }
    }

    function filterByStatus(status) {
        console.log('Filtrando por estado:', status);

        if (!currentStations || currentStations.length === 0) {
            console.warn('No hay estaciones cargadas para filtrar');
            return;
        }

        var filtered = status === 'all'
            ? currentStations
            : currentStations.filter(function (s) { return s.Status === status; });

        console.log('Estaciones filtradas:', filtered.length);
        renderMarkers(filtered);
    }

    function updateMapFilters() {
        console.log('Actualizando filtros de productos');

        if (!currentStations || currentStations.length === 0) {
            console.warn('No hay estaciones cargadas para filtrar');
            return;
        }

        var products = [];
        if (chkGE.GetChecked()) products.push('GE');
        if (chkGEPlus.GetChecked()) products.push('GE+');
        if (chkDO.GetChecked()) products.push('DO');

        console.log('Productos seleccionados:', products);

        var filtered = currentStations.filter(function (station) {
            var stationProducts = station.Products.split(',').map(function (p) { return p.trim(); });
            return products.some(function (p) { return stationProducts.indexOf(p) >= 0; });
        });

        console.log('Estaciones filtradas:', filtered.length);
        renderMarkers(filtered);
    }

    function zoomToDept(dept) {
        console.log('Haciendo zoom a departamento:', dept);

        cmbDepartamento.SetValue(dept);
        filterByDepartment(dept);

        // Coordenadas de departamentos
        var coords = {
            'lp': [-16.5000, -68.1500, 10],
            'sc': [-17.7833, -63.1821, 10],
            'cb': [-17.3895, -66.1568, 10],
            'or': [-17.9833, -67.1500, 10],
            'pt': [-19.5836, -65.7531, 10],
            'ch': [-19.0333, -65.2597, 10],
            'tj': [-21.5333, -64.7333, 10],
            'bn': [-14.8333, -64.9000, 9],
            'pd': [-11.0333, -68.7333, 9]
        };

        if (coords[dept] && map) {
            map.setView([coords[dept][0], coords[dept][1]], coords[dept][2]);
        }
    }

    function resetMap() {
        console.log('Reseteando mapa');

        cmbDepartamento.SetValue('all');

        if (map) {
            map.setView([-17.7833, -63.1821], 6);
        }

        renderMarkers(currentStations);
    }

    function resetFilters() {
        console.log('Reseteando todos los filtros');

        cmbDepartamento.SetValue('all');
        chkGE.SetChecked(true);
        chkGEPlus.SetChecked(true);
        chkDO.SetChecked(true);
        resetMap();
    }

    function viewStationDetails(id) {
        console.log('Viendo detalles de estación:', id);

        if (typeof cbpStationDetail !== 'undefined') {
            cbpStationDetail.PerformCallback('STATION_DETAIL|' + id);
        }

        if (typeof popupStation !== 'undefined') {
            popupStation.Show();
        }
    }

    function switchView(view) {
        console.log('Cambiando vista a:', view);

        // Actualizar clases de botones
        btnNational.SetCssClass('nav-button-inactive');
        btnRegional.SetCssClass('nav-button-inactive');
        btnStation.SetCssClass('nav-button-inactive');

        if (view === 'national') {
            btnNational.SetCssClass('nav-button-active');
            resetMap();
        } else if (view === 'regional') {
            btnRegional.SetCssClass('nav-button-active');
            zoomToDept('lp');
        } else if (view === 'station') {
            btnStation.SetCssClass('nav-button-active');
            zoomToDept('lp');
            setTimeout(function () {
                if (markers.length > 0) {
                    markers[0].openPopup();
                }
            }, 500);
        }
    }

    function refreshData() {
        console.log('Refrescando datos');

        if (typeof btnRefresh !== 'undefined') {
            btnRefresh.SetText('Actualizando...');
        }

        // Recargar datos
        loadStationsData();

        // Actualizar todos los callbacks
        if (typeof cbpKPIs !== 'undefined') {
            cbpKPIs.PerformCallback('REFRESH');
        }
        if (typeof cbpRankings !== 'undefined') {
            cbpRankings.PerformCallback('REFRESH');
        }
        if (typeof cbpCharts !== 'undefined') {
            cbpCharts.PerformCallback('REFRESH');
        }

        setTimeout(function () {
            if (typeof btnRefresh !== 'undefined') {
                btnRefresh.SetText('Actualizar');
            }
            if (typeof lblUltimaActualizacion !== 'undefined') {
                lblUltimaActualizacion.SetText('Hace un momento');
            }
        }, 1000);
    }

    function generateReport() {
        console.log('Generando reporte');
        window.open('ReporteANH.aspx', '_blank');
    }

    function showReport() {
        generateReport();
    }

    function showAllAlerts() {
        console.log('Mostrando todas las alertas');
        // Aquí puedes implementar la lógica para mostrar todas las alertas
    }

    // Callback de mapa desde servidor (para mantener compatibilidad)
    function updateMapFromServer(stationsJson) {
        try {
            console.log('Actualizando mapa desde servidor');
            var stations = JSON.parse(stationsJson);
            renderMarkers(stations);
        } catch (error) {
            console.error('Error en updateMapFromServer:', error);
        }
    }
</script>