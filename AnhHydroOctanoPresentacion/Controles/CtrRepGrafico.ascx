<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtrRepGrafico.ascx.cs" Inherits="AnhHydroOctanoPresentacion.Controles.CtrRepGrafico" %>

<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>


<%@ Register assembly="DevExpress.XtraCharts.v15.1.Web, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraCharts.Web" tagprefix="dxchartsui" %>



<style type="text/css">
    .auto-style1 {
        height: 18px;
    }
    .auto-style4 {
        height: 18px;
        width: 168px;
    }
    .panelPersona
    {
        position: fixed;
        top: 410px;
        width: 500px;
        background: white;
    }
    .hydro_smart_container
    {
        margin: 0;
        padding: 0;
        Height: 690px;
    }
    .hydro_smart_header
    {
        margin: 0;
        padding: 5px 10px;
        /*background: orange;*/
        background: rgb(0, 132, 38);
        color: white;
        font-size: medium;
        font-weight: bold;
    }
    .hydro_smart_table
    {
        font-size: smaller;
        width: 95%;
        margin: auto;
    }
    .hydro_smart_info {
        /*border: #FFCE4F solid thin;*/
        border: #1d9915 solid thin;
        margin: 5px;
        /*background: #FFF8F0;*/
        background: #f0fff8;
        -ms-border-radius: 3px;
        border-radius: 3px;
    }
    .alert-box {
    color:#555;
    border-radius:10px;
    font-family:Tahoma,Geneva,Arial,sans-serif;font-size:11px;
    padding:10px 10px 10px 36px;
    margin:10px;
}
    
    .dxgvControl,
.dxgvDisabled
{
	border: 1px Solid #9F9F9F;
	font: 12px Tahoma, Geneva, sans-serif;
	background-color: #F2F2F2;
	color: Black;
	cursor: default;
}
.dxgvTable
{
	-webkit-tap-highlight-color: rgba(0,0,0,0);
}

.dxgvTable
{
	background-color: White;
	border-width: 0;
	border-collapse: separate!important;
	overflow: hidden;
}
.dxgvHeader
{
	cursor: pointer;
	white-space: nowrap;
	padding: 4px 6px;
	border: 1px Solid #9F9F9F;
	background-color: #DCDCDC;
	overflow: hidden;
	font-weight: normal;
	text-align: left;
}
.dxgvFocusedRow
{
	background-color: #8D8D8D;
	color: White;
}
.dxgvFooter
{
	background-color: #D7D7D7;
	white-space: nowrap;
}

.dxgvPagerTopPanel,
.dxgvPagerBottomPanel
{
	padding-top: 4px;
	padding-bottom: 4px;
}

    </style>

<table style="width: 100%">
    <tr>
        <td style="text-align: center;">
            <dx:ASPxRoundPanel runat="server" ShowHeader="False" Width="100%" ID="pnlTitulo">
                <PanelCollection>
                    <dx:PanelContent runat="server">
                        <dx:ASPxLabel runat="server" ForeColor="#367B2F" ID="lblTitulo" Font-Bold="True" Font-Names="Rockwell" Font-Size="Larger" Text="REPORTE - GRAFICO"></dx:ASPxLabel>
                    </dx:PanelContent>
                </PanelCollection>
                <Border BorderStyle="Ridge" BorderWidth="1px" BorderColor="#003300"></Border>
            </dx:ASPxRoundPanel>
        </td>
    </tr>
</table>

<dx:ASPxSplitter ID="ASPxSplitter1" runat="server" Height="700px">
    <Panes>
        <dx:SplitterPane Size="18%" ScrollBars="Auto">
            <ContentCollection>
                <dx:SplitterContentControl runat="server">
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <dx:ASPxLabel ID="ASPxLabel72" runat="server" ForeColor="#003300" Text="Día :">
                                </dx:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <dx:ASPxDateEdit ID="detFecha" runat="server" ClientInstanceName="detFecha" EnableTheming="True" Height="23px" Theme="Metropolis" ToolTip="Seleccione la fecha inicial" Width="120px">
                                    <TimeSectionProperties>
                                        <TimeEditProperties>
                                            <ClearButton Visibility="Auto">
                                            </ClearButton>
                                        </TimeEditProperties>
                                    </TimeSectionProperties>
                                    <ClientSideEvents DateChanged="function(s, e) {
pnlDatos.PerformCallback();
}" />
                                    <ClearButton Visibility="Auto">
                                    </ClearButton>
                                </dx:ASPxDateEdit>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <dx:ASPxLabel ID="ASPxLabel71" runat="server" ForeColor="#003300" Text="Actividades Asignadas :">
                                </dx:ASPxLabel>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <dx:ASPxTreeList ID="tlsOperadores" runat="server" AutoGenerateColumns="False" ClientInstanceName="tlsOperadores" Font-Size="Smaller" Height="187px" KeyFieldName="intId" ParentFieldName="intIdPadre" Theme="Youthful">
                                    <Columns>
                                        <dx:TreeListTextColumn Caption="ID" FieldName="intId" ShowInCustomizationForm="True" Visible="False" VisibleIndex="0">
                                        </dx:TreeListTextColumn>
                                        <dx:TreeListTextColumn Caption="PADRE" FieldName="intIdPadre" ShowInCustomizationForm="True" Visible="False" VisibleIndex="1">
                                        </dx:TreeListTextColumn>
                                        <dx:TreeListTextColumn Caption="DESCRIPCION" FieldName="strDescripcion" ShowInCustomizationForm="True" VisibleIndex="2">
                                        </dx:TreeListTextColumn>
                                        <dx:TreeListTextColumn Caption="NIT" FieldName="lngNit" ShowInCustomizationForm="True" Visible="False" VisibleIndex="3">
                                        </dx:TreeListTextColumn>
                                        <dx:TreeListTextColumn Caption="LICENCIA" FieldName="lngLicencia" ShowInCustomizationForm="True" Visible="False" VisibleIndex="4">
                                        </dx:TreeListTextColumn>
                                        <dx:TreeListTextColumn Caption="REP_LEGAL" FieldName="strRepLegal" ShowInCustomizationForm="True" Visible="False" VisibleIndex="5">
                                        </dx:TreeListTextColumn>
                                        <dx:TreeListTextColumn Caption="DIRECCION" FieldName="strDireccion" ShowInCustomizationForm="True" Visible="False" VisibleIndex="6">
                                        </dx:TreeListTextColumn>
                                    </Columns>
                                    <Settings ScrollableHeight="450" />
                                    <SettingsBehavior AllowFocusedNode="True" />
                                    <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
                                    <ClientSideEvents FocusedNodeChanged="function(s, e) {
	pnlDatos.PerformCallback();
}" Init="function(s, e) {
	pnlDatos.PerformCallback();
}" />
                                </dx:ASPxTreeList>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                </dx:SplitterContentControl>
            </ContentCollection>
        </dx:SplitterPane>
        <dx:SplitterPane ScrollBars="Auto">
            <ContentCollection>
                <dx:SplitterContentControl runat="server">
                    <dx:ASPxCallbackPanel ID="pnlDatos" runat="server" Theme="Youthful" Width="100%" ClientInstanceName="pnlDatos" OnCallback="pnlDatos_Callback">
                        <ClientSideEvents BeginCallback="function(s, e) {
	LoadingPanel.Show();
}" EndCallback="function(s, e) {
	LoadingPanel.Hide();
}" />
                        <PanelCollection>
                            <dx:PanelContent runat="server">
                                <table style="width: 100%;">
                                    <tr>
                                        <td colspan="7" style="text-align: center">
                                            <dx:ASPxRoundPanel ID="pnlMensaje" runat="server" ShowHeader="False" Visible="False" Width="100%">
                                                <PanelCollection>
                                                    <dx:PanelContent runat="server">
                                                        <dx:ASPxLabel ID="lblMensajeError" runat="server" ForeColor="#666666" style="text-align:center">
                                                        </dx:ASPxLabel>
                                                    </dx:PanelContent>
                                                </PanelCollection>
                                                <Border BorderStyle="Ridge" BorderWidth="1px" />
                                            </dx:ASPxRoundPanel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Rockwell" Font-Size="Medium" ForeColor="#367B2F" Text="LISTADO DE ENTIDADES"></asp:Label>
                                        </td>
                                        <td class="auto-style1"></td>
                                        <td>&nbsp;</td>
                                        <td colspan="2">
                                            &nbsp;</td>
                                        <td class="auto-style1">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan="7">
                                            <hr/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style4">
                                            <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="#10420F" Text="ACTIVIDAD"></asp:Label>
                                        </td>
                                        <td></td>
                                        <td>
                                            &nbsp;</td>
                                        <td></td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            <dx:ASPxButton ID="ASPxButton2" runat="server" OnClick="ASPxButton2_Click" Text="ASPxButton">
                                            </dx:ASPxButton>
                                        </td>
                                        <td style="text-align: right">
                                            <dx:ASPxButton ID="ASPxButton1" runat="server" Text="ASPxButton" AutoPostBack="False" OnClick="ASPxButton1_Click">
                                            </dx:ASPxButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6" class="auto-style1">
                                            <asp:Label ID="lblActividad" runat="server"></asp:Label>
                                        </td>
                                        <td style="text-align: right" class="auto-style1">
                                            <dx:ASPxButton ID="btnGrafica" runat="server" OnClick="btnGrafica_Click" Text="Graficar">
                                            </dx:ASPxButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="7">
                                            <dxchartsui:WebChartControl ID="WebChartControl1" runat="server" CrosshairEnabled="True" Height="400px" Width="300px" ClientInstanceName="WebChartControl1">
                                            </dxchartsui:WebChartControl>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="7">
                                            &nbsp;</td>
                                    </tr>
                                </table>
                            </dx:PanelContent>
                        </PanelCollection>
                    </dx:ASPxCallbackPanel>
                </dx:SplitterContentControl>
            </ContentCollection>
        </dx:SplitterPane>
    </Panes>
</dx:ASPxSplitter>



























































































































