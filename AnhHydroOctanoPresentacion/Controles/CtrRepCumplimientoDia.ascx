<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtrRepCumplimientoDia.ascx.cs" Inherits="AnhHydroOctanoPresentacion.Controles.CtrRepCumplimientoDia" %>

<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxPivotGrid.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPivotGrid" tagprefix="dx" %>



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

<div class="col-md-12">
    <table style="width: 100%">
        <tr>
            <td style="text-align: center; padding-bottom: 1px">
                <dx:ASPxRoundPanel runat="server" ShowHeader="False" Width="100%" ID="pnlTitulo">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <dx:ASPxLabel runat="server" ForeColor="#367B2F" ID="lblTitulo" Font-Bold="True" Font-Names="Rockwell" Font-Size="Larger" Text="REPORTE - CUMPLIMIENTO DIARIO"></dx:ASPxLabel>
                        </dx:PanelContent>
                    </PanelCollection>
                    <Border BorderStyle="Ridge" BorderWidth="1px" BorderColor="#003300"></Border>
                </dx:ASPxRoundPanel>
            </td>
        </tr>
    </table>

    <dx:ASPxSplitter ID="ASPxSplitter1" runat="server">
        <Panes>
            <dx:SplitterPane Size="18%" AllowResize="True" AutoHeight="True">
                <ContentCollection>
                    <dx:SplitterContentControl runat="server">
                        <table style="width: 100%;">
                            <tr>
                                <td style="padding-bottom: 5px">
                                    <dx:ASPxLabel ID="ASPxLabel60" runat="server" Font-Bold="True" Font-Names="Rockwell" ForeColor="#487B5C" Text="CARACTERISTICAS" Width="100%">
                                        <BorderBottom BorderStyle="Solid" />
                                    </dx:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <dx:ASPxLabel ID="ASPxLabel72" runat="server" ForeColor="#003300" Text="Fecha Inicial :">
                                    </dx:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <dx:ASPxDateEdit ID="detFechaIni" runat="server" ClientInstanceName="detFechaIni" EnableTheming="True" Height="23px" Theme="Metropolis" ToolTip="Seleccione la fecha inicial" Width="120px" Font-Bold="True">
                                        <TimeSectionProperties>
                                            <TimeEditProperties>
                                                
<ClearButton Visibility="Auto">
                                                </ClearButton>
                                            
</TimeEditProperties>
                                        </TimeSectionProperties>
                                        <ClearButton Visibility="Auto">
                                        </ClearButton>
                                    </dx:ASPxDateEdit>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <dx:ASPxLabel ID="ASPxLabel73" runat="server" ForeColor="#003300" Text="Fecha Final :">
                                    </dx:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <dx:ASPxDateEdit ID="detFechaFin" runat="server" ClientInstanceName="detFechaFin" EnableTheming="True" Font-Bold="True" Height="23px" Theme="Metropolis" ToolTip="Seleccione la fecha inicial" Width="120px">
                                        <TimeSectionProperties>
                                            <TimeEditProperties>
                                                
<ClearButton Visibility="Auto">
                                                </ClearButton>
                                            
</TimeEditProperties>
                                        </TimeSectionProperties>
                                        <ClearButton Visibility="Auto">
                                        </ClearButton>
                                    </dx:ASPxDateEdit>
                                </td>
                            </tr>                          
                            <tr>
                                <td>
                                    <dx:ASPxLabel ID="lblDpto" runat="server" ForeColor="#003300" Text="Departamento :" Visible="false">
                                    </dx:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <dx:ASPxComboBox ID="cmbDepartamento" runat="server" SelectedIndex="0" Theme="Metropolis" Width="120px" Visible="false">
                                        <Items>
                                            <dx:ListEditItem Selected="True" Text="TODOS" Value="0" />
                                            <dx:ListEditItem Text="CHUQUISACA" Value="1" />
                                            <dx:ListEditItem Text="LA PAZ" Value="2" />
                                            <dx:ListEditItem Text="COCHABAMBA" Value="3" />
                                            <dx:ListEditItem Text="ORURO" Value="4" />
                                            <dx:ListEditItem Text="POTOSI" Value="5" />
                                            <dx:ListEditItem Text="TARIJA" Value="6" />
                                            <dx:ListEditItem Text="SANTA CRUZ" Value="7" />
                                            <dx:ListEditItem Text="BENI" Value="8" />
                                            <dx:ListEditItem Text="PANDO" Value="9" />
                                        </Items>
                                        <ClearButton Visibility="Auto">
                                        </ClearButton>
                                    </dx:ASPxComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-top: 5px;padding-bottom: 5px">
                                    <dx:ASPxButton ID="btnConsultar" runat="server" AutoPostBack="False" Font-Bold="True" ForeColor="Maroon" Text="Obtener Información" Theme="Metropolis" Width="75%">
                                        <ClientSideEvents Click="function(s, e) {
pnlDatos.PerformCallback();
}" />
                                        <Image IconID="actions_download_16x16">
                                        </Image>
                                    </dx:ASPxButton>
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
                                    <dx:ASPxTreeList ID="tlsOperadores" runat="server" AutoGenerateColumns="False" ClientInstanceName="tlsOperadores" Font-Size="Smaller" Height="187px" KeyFieldName="intId" ParentFieldName="intIdPadre" Theme="Youthful" Styles-Header-BackColor="#317D40">
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
                                        <SettingsBehavior AllowFocusedNode="True" FocusNodeOnExpandButtonClick="False" />
                                        <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />

<Styles>
<Header BackColor="#317D40"></Header>
</Styles>

                                        <ClientSideEvents NodeDblClick="function(s, e) {
pnlDatos.PerformCallback();
}" />
                                    </dx:ASPxTreeList>
                                </td>
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
                                            <td colspan="7">
                                                <legend>
                                                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Rockwell" Font-Size="Medium" ForeColor="#367B2F" Text="LISTADO DE PROYECTOS"></asp:Label>
                                                </legend>
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
                                                &nbsp;</td>
                                            <td style="text-align: right">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                                <asp:Label ID="lblActividad" runat="server"></asp:Label>
                                            </td>
                                            <td style="text-align: right">
                                                <dx:ASPxButton ID="btnEmitir" runat="server" OnClick="btnEmitir_Click" Text="Exportar Listado Excel" Theme="Metropolis" Width="200px" Font-Bold="True" ForeColor="Maroon">
                                                    <Image IconID="export_exporttoxls_16x16">
                                                    </Image>
                                                </dx:ASPxButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="7">
                                                <dx:ASPxPivotGrid ID="pvgReporte" runat="server" ClientIDMode="AutoID" ClientInstanceName="pvgReporte" EnableTheming="True" Font-Size="Smaller" OnCustomCellStyle="pvgReporte_CustomCellStyle" Theme="Default" Width="100%">
                                                    <Fields>
                                                        <dx:PivotGridField ID="fieldDEPARTAMENTO" Area="RowArea" AreaIndex="0" Caption="DEPARTAMENTO" FieldName="DEPARTAMENTO">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldUNIDADMEDIDA" Area="RowArea" AreaIndex="3" Caption="UNIDAD MEDIDA" FieldName="UNIDAD_MEDIDA" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldPRODUCTO" Area="RowArea" AreaIndex="2" Caption="PRODUCTO" FieldName="PRODUCTO">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDENOMINACION" Area="RowArea" AreaIndex="1" Caption="ENTIDAD" FieldName="DENOMINACION">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldTIPOOPERACION" Area="ColumnArea" AreaIndex="1" Caption="TIPO OPERACION" FieldName="TIPO_OPERACION">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldCANTIDAD" Area="DataArea" AreaIndex="0" Caption="CANTIDAD" FieldName="CANTIDAD" UnboundType="Decimal" CellFormat-FormatType="Custom">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldTIPOOP" Area="ColumnArea" AreaIndex="0" Caption="TIPO" FieldName="TIPO_OP">
                                                        </dx:PivotGridField>
                                                    </Fields>
                                                    <Styles CssPostfix="DevGlp"></Styles>
                                                    <OptionsPager RowsPerPage="100" AlwaysShowPager="True">
                                                        <PageSizeItemSettings Items="100, 1000" ShowAllItem="True" AllItemText="TODOS" Visible="True">
                                                        </PageSizeItemSettings>
                                                    </OptionsPager>
                                                </dx:ASPxPivotGrid>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="7">
                                                <dx:ASPxPivotGridExporter ID="pvgExportar" runat="server">
                                                </dx:ASPxPivotGridExporter>
                                            </td>
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

</div>
























































































































