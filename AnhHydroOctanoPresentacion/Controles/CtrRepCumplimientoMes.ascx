<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtrRepCumplimientoMes.ascx.cs" Inherits="AnhHydroOctanoPresentacion.Controles.CtrRepCumplimientoMes" %>

<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>

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
            <td style="text-align: center;">
                <dx:ASPxRoundPanel runat="server" ShowHeader="False" Width="100%" ID="pnlTitulo">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <dx:ASPxLabel runat="server" ForeColor="#367B2F" ID="lblTitulo" Font-Bold="True" Font-Names="Rockwell" Font-Size="Larger" Text="REPORTE - CUMPLIMIENTO MENSUAL"></dx:ASPxLabel>
                        </dx:PanelContent>
                    </PanelCollection>
                    <Border BorderStyle="Ridge" BorderWidth="1px" BorderColor="#003300"></Border>
                </dx:ASPxRoundPanel>
            </td>
        </tr>
    </table>

    <dx:ASPxSplitter ID="ASPxSplitter1" runat="server" Height="100%">
        <Panes>
            <dx:SplitterPane Size="18%" AllowResize="True" AutoHeight="True">
                <ContentCollection>
                    <dx:SplitterContentControl runat="server">
                        <table style="width: 100%;">
                            <tr>
                                <td style="padding-bottom: 5px">
                                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Font-Bold="True" Font-Names="Rockwell" ForeColor="#487B5C" Text="CARACTERISTICAS" Width="100%">
                                        <BorderBottom BorderStyle="Solid" />
                                    </dx:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td>Mes :<dx:ASPxComboBox ID="cmbMes" runat="server" ClientInstanceName="cmbMes" Theme="Metropolis" ValueType="System.Int32" Width="140px">
                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
    tacMovimientos.SetActiveTabIndex(0);
    dgvAcumulado.SetVisible(true);
    dgvInternacional.SetVisible(false);

    pnlDatos.PerformCallback();
    }" />
                                    <Items>
                                        <dx:ListEditItem Text="ENERO" Value="1" />
                                        <dx:ListEditItem Text="FEBRERO" Value="2" />
                                        <dx:ListEditItem Text="MARZO" Value="3" />
                                        <dx:ListEditItem Text="ABRIL" Value="4" />
                                        <dx:ListEditItem Text="MAYO" Value="5" />
                                        <dx:ListEditItem Text="JUNIO" Value="6" />
                                        <dx:ListEditItem Text="JULIO" Value="7" />
                                        <dx:ListEditItem Text="AGOSTO" Value="8" />
                                        <dx:ListEditItem Text="SEPTIEMBRE" Value="9" />
                                        <dx:ListEditItem Text="OCTUBRE" Value="10" />
                                        <dx:ListEditItem Text="NOVIEMBRE" Value="11" />
                                        <dx:ListEditItem Text="DICIEMBRE" Value="12" />
                                    </Items>
                                    <ClearButton Visibility="Auto">
                                    </ClearButton>
                                    </dx:ASPxComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style1">Año :<dx:ASPxComboBox ID="cmbGestion" runat="server" ClientInstanceName="cmbGestion" Theme="Metropolis" ValueType="System.Int32" Width="140px">
                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
    tacMovimientos.SetActiveTabIndex(0);
    dgvAcumulado.SetVisible(true);
    dgvInternacional.SetVisible(false);
    pnlDatos.PerformCallback();
    }" />
                                    <Items>
                                        <dx:ListEditItem Text="2015" Value="2015" />
                                        <dx:ListEditItem Text="2016" Value="2016" />
                                        <dx:ListEditItem Text="2017" Value="2017" />
                                        <dx:ListEditItem Text="2018" Value="2018" />
                                        <dx:ListEditItem Text="2019" Value="2019" />
                                        <dx:ListEditItem Text="2020" Value="2020" />
                                        <dx:ListEditItem Text="2021" Value="2021" />
                                        <dx:ListEditItem Text="2022" Value="2022" />
                                        <dx:ListEditItem Text="2023" Value="2023" />
                                        <dx:ListEditItem Text="2024" Value="2024" />
                                        <dx:ListEditItem Text="2025" Value="2025" />
                                    </Items>
                                    <ClearButton Visibility="Auto">
                                    </ClearButton>
                                    </dx:ASPxComboBox>
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
                                        <SettingsBehavior AllowFocusedNode="True" />
                                        <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
                                        <ClientSideEvents FocusedNodeChanged="function(s, e) {
	    tacMovimientos.SetActiveTabIndex(0);
	    dgvAcumulado.SetVisible(true);
                    dgvInternacional.SetVisible(false);

	    pnlDatos.PerformCallback();
    }" Init="function(s, e) {
	    pnlDatos.PerformCallback();
    }" />
                                    </dx:ASPxTreeList>
                                </td>
                            </tr>
                        </table>
                    </dx:SplitterContentControl>
                </ContentCollection>
            </dx:SplitterPane>
            <dx:SplitterPane ScrollBars="Horizontal">
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
                                                <dx:ASPxGridView ID="dgvReporte" runat="server" AutoGenerateColumns="False" ClientInstanceName="dgvReporte" EnableTheming="True" Font-Size="Smaller" KeyFieldName="ID_CONSUMIDOR" Theme="Default" Width="100%" OnHtmlRowPrepared="dgvReporte_HtmlRowPrepared" OnCustomButtonInitialize="dgvReporte_CustomButtonInitialize">
                                                    <ClientSideEvents CustomButtonClick="function(s, e) {

    switch(e.buttonID) {

    case&nbsp; 'btnHistorial':
    pucHistorial.Show();
    pucHistorial.PerformCallback();
    break;

    case&nbsp; 'btnResumen':

    s.GetRowValues(e.visibleIndex, 'CITE_GENERADO', function (value) {
    if(value!=null){
    lblTituloDetalle.SetValue('RESUME DE LA DECLARACION JURADA : '+value);
    }
    else{
    lblTituloDetalle.SetValue('RESUME DE LA DECLARACION JURADA : ');
    }

    });

    dgvAcumulado.PerformCallback();
    dgvInternacional.PerformCallback();
    break;

    case&nbsp; 'btnAmpliar':
    pucAmpliacion.Show();
    pucAmpliacion.PerformCallback();
    break;

    case&nbsp; 'btnIntimar':
    pucIntimacion.Show();
    pucIntimacion.PerformCallback();
    break;

    } 

    }
    " />
                                                    <SettingsContextMenu EnableRowMenu="False">
                                                    </SettingsContextMenu>
                                                    <Templates>
                                                        <EditForm>
                                                        </EditForm>
                                                    </Templates>
                                                    <SettingsPager NumericButtonCount="4" PageSize="1000">
                                                        <PageSizeItemSettings Items="50, 100, 200, 500, 1000" Visible="True">
                                                        </PageSizeItemSettings>
                                                    </SettingsPager>
                                                    <SettingsEditing Mode="PopupEditForm">
                                                    </SettingsEditing>
                                                    <Settings ShowFooter="True" ShowHeaderFilterButton="True" ShowFilterRow="True" ShowGroupPanel="True" />
                                                    <SettingsBehavior AllowFocusedRow="True" />
                                                    <SettingsPopup>
                                                        <EditForm Modal="True" />
                                                        <CustomizationWindow HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" />
                                                    </SettingsPopup>
                                                    <SettingsText CommandNew="Nuevo" />
                                                    <Columns>
                                                        <dx:GridViewCommandColumn ShowInCustomizationForm="True" VisibleIndex="11" ShowClearFilterButton="True">
                                                            <CustomButtons>
                                                                <dx:GridViewCommandColumnCustomButton ID="btnResumen" Text="Ver Resumen">
                                                                </dx:GridViewCommandColumnCustomButton>
    <dx:GridViewCommandColumnCustomButton ID="btnHistorial" Text="Ver Historial"></dx:GridViewCommandColumnCustomButton>
                                                                <dx:GridViewCommandColumnCustomButton ID="btnAmpliar" Text="Ampliación">
                                                                </dx:GridViewCommandColumnCustomButton>
                                                                <dx:GridViewCommandColumnCustomButton ID="btnIntimar" Text="Intimación">
                                                                </dx:GridViewCommandColumnCustomButton>
                                                            </CustomButtons>
                                                        </dx:GridViewCommandColumn>
                                                        <dx:GridViewDataTextColumn Caption="CITE DEC. JURADA" FieldName="CITE_GENERADO" ShowInCustomizationForm="True" VisibleIndex="8" ToolTip="Cite de la Declaración Jurada">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="ID_TIPO_ACTIVIDAD" FieldName="ID_TIPO_ACTIVIDAD" ShowInCustomizationForm="True" ToolTip="Tipo de Consumidor" VisibleIndex="13" Visible="False">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="DEPTO." FieldName="DEPARTAMENTO" ShowInCustomizationForm="True" VisibleIndex="1" ToolTip="Departamento">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="ESTADO" FieldName="MENSAJE_MENSUAL" ShowInCustomizationForm="True" VisibleIndex="10">
                                                            <Settings AutoFilterCondition="Contains" />
                                                            <CellStyle Font-Bold="True">
                                                            </CellStyle>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="RAZON SOCIAL" FieldName="DENOMINACION" ShowInCustomizationForm="True" VisibleIndex="2">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="FECHA EMISION" FieldName="strFechaReg" ShowInCustomizationForm="True" VisibleIndex="7">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="ID_CONSUMIDOR" FieldName="ID_CONSUMIDOR" ShowInCustomizationForm="True" Visible="False" VisibleIndex="0">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="ESTADO_MENSUAL" FieldName="ESTADO_MENSUAL" ShowInCustomizationForm="True" Visible="False" VisibleIndex="9">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="JSON_REPORTE_DATOS" FieldName="JSON_REPORTE_DATOS" ShowInCustomizationForm="True" Visible="False" VisibleIndex="12">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="FECHA_REPORTE" FieldName="FECHA_REPORTE" ShowInCustomizationForm="True" Visible="False" VisibleIndex="6">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="NIT" FieldName="NIT" ShowInCustomizationForm="True" VisibleIndex="3">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="DIRECCION" FieldName="DIRECCION" ShowInCustomizationForm="True" VisibleIndex="4">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="REP. LEGAL" FieldName="NOMBRES_REPRESENTANTE" ShowInCustomizationForm="True" ToolTip="Representante Legal" VisibleIndex="5">
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                    <Styles CssPostfix="DevGlp"><AlternatingRow BackColor="#EEEEEE">
                                                        </AlternatingRow></Styles>
                                                    <TotalSummary>
                                                        <dx:ASPxSummaryItem FieldName="DecRecepcion" ShowInColumn="RECEPCION" SummaryType="Custom" Tag="rec" />
                                                        <dx:ASPxSummaryItem FieldName="DecDemasia" ShowInColumn="DEMASIA" SummaryType="Custom" Tag="dem" />
                                                        <dx:ASPxSummaryItem FieldName="DecTotalIng" ShowInColumn="TOT. ING." SummaryType="Custom" Tag="ing" />
                                                        <dx:ASPxSummaryItem FieldName="DecVenta" ShowInColumn="VENTA" SummaryType="Custom" Tag="ven" />
                                                        <dx:ASPxSummaryItem FieldName="DecMerma" ShowInColumn="MERMA" SummaryType="Custom" Tag="mer" />
                                                        <dx:ASPxSummaryItem FieldName="DecTotalSal" ShowInColumn="TOT. SAL." SummaryType="Custom" Tag="sal" />
                                                    </TotalSummary>
                                                </dx:ASPxGridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="5" class="auto-style1">
                                                <dx:ASPxGridViewExporter ID="dgeExportar" runat="server">
                                                </dx:ASPxGridViewExporter>
                                            </td>
                                            <td class="auto-style1">
                                                &nbsp;</td>
                                            <td class="auto-style1">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style1" colspan="7">
                                                <dx:ASPxLabel ID="lblTituloDetalle" runat="server" ClientInstanceName="lblTituloDetalle" Font-Bold="True" ForeColor="#10420F" Text="DETALLE DE LA DECLARACION JURADA">
                                                </dx:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="5">
                                                <dx:ASPxTabControl ID="tacMovimientos" runat="server" ActiveTabIndex="0" ClientInstanceName="tacMovimientos" EnableTheming="True" TabIndex="1" Theme="Metropolis">
                                                    <Tabs>
                                                        <dx:Tab Name="tcpResumen" Text="Mov. Acumulado">
                                                        </dx:Tab>
                                                        <dx:Tab Name="tcpEspecial" Text="Mov. Internacional">
                                                        </dx:Tab>
                                                    </Tabs>
                                                    <ClientSideEvents ActiveTabChanged="function(s, e) {

    if(tacMovimientos.GetActiveTabIndex()==1)
    {
    dgvAcumulado.SetVisible(false);
    dgvInternacional.SetVisible(true);
    }
    else
    {
    dgvInternacional.SetVisible(false);
    dgvAcumulado.SetVisible(true);
    }

    }
    " />
                                                </dx:ASPxTabControl>
                                            </td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td colspan="7">
                                                <dx:ASPxGridView ID="dgvAcumulado" runat="server" AutoGenerateColumns="False" ClientInstanceName="dgvAcumulado" EnableTheming="True" Font-Size="Smaller" KeyFieldName="IntId" OnHtmlRowPrepared="dgvAcumulado_HtmlRowPrepared" Theme="Default" Width="100%" OnCustomCallback="dgvAcumulado_CustomCallback">
                                                    <SettingsContextMenu EnableRowMenu="False">
                                                    </SettingsContextMenu>
                                                    <Templates>
                                                        <EditForm>
                                                        </EditForm>
                                                    </Templates>
                                                    <SettingsPager NumericButtonCount="5">
                                                        <PageSizeItemSettings Items="50, 100, 200, 500, 1000" Visible="True">
                                                        </PageSizeItemSettings>
                                                    </SettingsPager>
                                                    <SettingsEditing Mode="PopupEditForm">
                                                    </SettingsEditing>
                                                    <Settings ShowFooter="True" ShowHeaderFilterButton="True" />
                                                    <SettingsBehavior AllowFocusedRow="True" />
                                                    <SettingsPopup>
                                                        <EditForm Modal="True" />
                                                        <CustomizationWindow HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" />
                                                    </SettingsPopup>
                                                    <SettingsText CommandNew="Nuevo" />
                                                    <EditFormLayoutProperties ColCount="2">
                                                        <Items>
                                                            <dx:GridViewColumnLayoutItem ColumnName="StrFecha">
                                                            </dx:GridViewColumnLayoutItem>
                                                            <dx:GridViewColumnLayoutItem ColumnName="StrCodProducto">
                                                            </dx:GridViewColumnLayoutItem>
                                                            <dx:GridViewColumnLayoutItem ColumnName="DecSaldoInicial">
                                                            </dx:GridViewColumnLayoutItem>
                                                            <dx:GridViewColumnLayoutItem ColumnName="DecTotalIng">
                                                            </dx:GridViewColumnLayoutItem>
                                                            <dx:GridViewColumnLayoutItem ColumnName="DecTotalSal">
                                                            </dx:GridViewColumnLayoutItem>
                                                            <dx:GridViewColumnLayoutItem ColumnName="DecSaldoFinal">
                                                            </dx:GridViewColumnLayoutItem>
                                                            <dx:EditModeCommandLayoutItem ColSpan="2" HorizontalAlign="Right">
                                                            </dx:EditModeCommandLayoutItem>
                                                        </Items>
                                                    </EditFormLayoutProperties>
                                                    <Columns>
                                                        <dx:GridViewCommandColumn ShowInCustomizationForm="True" Visible="False" VisibleIndex="0">
                                                        </dx:GridViewCommandColumn>
                                                        <dx:GridViewDataTextColumn Caption="IntId" FieldName="IntId" ShowInCustomizationForm="True" Visible="False" VisibleIndex="1">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="IntContProd" FieldName="IntContProd" ShowInCustomizationForm="True" Visible="False" VisibleIndex="2">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="U. MEDIDA" FieldName="StrUnidadMedida" ShowInCustomizationForm="True" VisibleIndex="4" ToolTip="Unidad de Medida">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="PRODUCTO" FieldName="StrCodProducto" ShowInCustomizationForm="True" VisibleIndex="3">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="SALDO INICIAL" FieldName="DecSaldoInicial" ShowInCustomizationForm="True" VisibleIndex="7">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="DecIng01" FieldName="DecIng01" ShowInCustomizationForm="True" Visible="False" VisibleIndex="8">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="DecIng02" FieldName="DecIng02" ShowInCustomizationForm="True" Visible="False" VisibleIndex="9">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="DecIng03" FieldName="DecIng03" ShowInCustomizationForm="True" Visible="False" VisibleIndex="10">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="DecIng04" FieldName="DecIng04" ShowInCustomizationForm="True" Visible="False" VisibleIndex="11">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="DecIng05" FieldName="DecIng05" ShowInCustomizationForm="True" Visible="False" VisibleIndex="12">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="DecIng06" FieldName="DecIng06" ShowInCustomizationForm="True" Visible="False" VisibleIndex="13">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="DecIng07" FieldName="DecIng07" ShowInCustomizationForm="True" Visible="False" VisibleIndex="14">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="DecIng08" FieldName="DecIng08" ShowInCustomizationForm="True" Visible="False" VisibleIndex="15">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="DecIng09" FieldName="DecIng09" ShowInCustomizationForm="True" Visible="False" VisibleIndex="16">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="DecIng10" FieldName="DecIng10" ShowInCustomizationForm="True" Visible="False" VisibleIndex="17">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="DecIng11" FieldName="DecIng11" ShowInCustomizationForm="True" Visible="False" VisibleIndex="18">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="DecIng12" FieldName="DecIng12" ShowInCustomizationForm="True" Visible="False" VisibleIndex="19">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="DecIng13" FieldName="DecIng13" ShowInCustomizationForm="True" Visible="False" VisibleIndex="20">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="DecIng14" FieldName="DecIng14" ShowInCustomizationForm="True" Visible="False" VisibleIndex="21">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="DecIng15" FieldName="DecIng15" ShowInCustomizationForm="True" Visible="False" VisibleIndex="22">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="DecIng16" FieldName="DecIng16" ShowInCustomizationForm="True" Visible="False" VisibleIndex="23">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="DecIng17" FieldName="DecIng17" ShowInCustomizationForm="True" Visible="False" VisibleIndex="24">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="DecIng18" FieldName="DecIng18" ShowInCustomizationForm="True" Visible="False" VisibleIndex="25">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="DecIng19" FieldName="DecIng19" ShowInCustomizationForm="True" Visible="False" VisibleIndex="26">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="DecIng20" FieldName="DecIng20" ShowInCustomizationForm="True" Visible="False" VisibleIndex="27">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="TOTAL ING." FieldName="DecTotalIng" ShowInCustomizationForm="True" VisibleIndex="28">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="DecSal01" FieldName="DecSal01" ShowInCustomizationForm="True" Visible="False" VisibleIndex="29">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="DecSal02" FieldName="DecSal02" ShowInCustomizationForm="True" Visible="False" VisibleIndex="30">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="DecSal03" FieldName="DecSal03" ShowInCustomizationForm="True" Visible="False" VisibleIndex="31">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="DecSal04" FieldName="DecSal04" ShowInCustomizationForm="True" Visible="False" VisibleIndex="32">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="DecSal05" FieldName="DecSal05" ShowInCustomizationForm="True" Visible="False" VisibleIndex="33">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="DecSal06" FieldName="DecSal06" ShowInCustomizationForm="True" Visible="False" VisibleIndex="34">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="DecSal07" FieldName="DecSal07" ShowInCustomizationForm="True" Visible="False" VisibleIndex="35">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="DecSal08" FieldName="DecSal08" ShowInCustomizationForm="True" Visible="False" VisibleIndex="36">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="DecSal09" FieldName="DecSal09" ShowInCustomizationForm="True" Visible="False" VisibleIndex="37">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="DecSal10" FieldName="DecSal10" ShowInCustomizationForm="True" Visible="False" VisibleIndex="38">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="DecSal11" FieldName="DecSal11" ShowInCustomizationForm="True" Visible="False" VisibleIndex="39">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="DecSal12" FieldName="DecSal12" ShowInCustomizationForm="True" Visible="False" VisibleIndex="40">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="DecSal13" FieldName="DecSal13" ShowInCustomizationForm="True" Visible="False" VisibleIndex="41">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="DecSal14" FieldName="DecSal14" ShowInCustomizationForm="True" Visible="False" VisibleIndex="42">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="DecSal15" FieldName="DecSal15" ShowInCustomizationForm="True" Visible="False" VisibleIndex="43">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="DecSal16" FieldName="DecSal16" ShowInCustomizationForm="True" Visible="False" VisibleIndex="44">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="DecSal17" FieldName="DecSal17" ShowInCustomizationForm="True" Visible="False" VisibleIndex="45">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="DecSal18" FieldName="DecSal18" ShowInCustomizationForm="True" Visible="False" VisibleIndex="46">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="DecSal19" FieldName="DecSal19" ShowInCustomizationForm="True" Visible="False" VisibleIndex="47">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="DecSal20" FieldName="DecSal20" ShowInCustomizationForm="True" Visible="False" VisibleIndex="48">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="TOTAL SAL." FieldName="DecTotalSal" ShowInCustomizationForm="True" VisibleIndex="49">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="SALDO FINAL" FieldName="DecSaldoFinal" ShowInCustomizationForm="True" VisibleIndex="50">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="TIPO INFRA." FieldName="StrTipoInfraestructura" ShowInCustomizationForm="True" ToolTip="Tipo de Infraestructura" VisibleIndex="5">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="INFRAEST." FieldName="StrInfraestructura" ShowInCustomizationForm="True" ToolTip="Infraestructura" VisibleIndex="6">
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                </dx:ASPxGridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="7" style="margin-left: 40px">
                                                <dx:ASPxGridView ID="dgvInternacional" runat="server" AutoGenerateColumns="False" ClientInstanceName="dgvInternacional" EnableTheming="True" Font-Size="Smaller" KeyFieldName="LngId" Theme="Default" Width="100%" OnCustomCallback="dgvInternacional_CustomCallback" OnHtmlRowPrepared="dgvInternacional_HtmlRowPrepared">
                                                    <ClientSideEvents Init="function(s, e) {
	    dgvInternacional.SetVisible(false);
    }" />
                                                    <SettingsContextMenu EnableRowMenu="False">
                                                    </SettingsContextMenu>
                                                    <Templates>
                                                        <EditForm>
                                                        </EditForm>
                                                    </Templates>
                                                    <SettingsPager PageSize="100">
                                                        <PageSizeItemSettings Items="50, 100, 200, 500, 1000" Visible="True">
                                                        </PageSizeItemSettings>
                                                    </SettingsPager>
                                                    <SettingsEditing Mode="PopupEditForm">
                                                    </SettingsEditing>
                                                    <Settings ShowFooter="True" ShowHeaderFilterButton="True" />
                                                    <SettingsBehavior AllowFocusedRow="True" />
                                                    <SettingsPopup>
                                                        <EditForm Modal="True" />
                                                        <CustomizationWindow HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" />
                                                    </SettingsPopup>
                                                    <SettingsText CommandNew="Nuevo" />
                                                    <Columns>
                                                        <dx:GridViewCommandColumn ShowInCustomizationForm="True" Visible="False" VisibleIndex="21">
                                                        </dx:GridViewCommandColumn>
                                                        <dx:GridViewDataTextColumn Caption="PLACA" FieldName="StrPlaca" ShowInCustomizationForm="True" VisibleIndex="2">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="NIT" FieldName="LngNit" ShowInCustomizationForm="True" ToolTip="Tipo de Pago" VisibleIndex="3">
                                                            <Settings AutoFilterCondition="Contains" />
                                                            <CellStyle HorizontalAlign="Right">
                                                            </CellStyle>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="NF PROPIO" FieldName="LngNoFactProp" ShowInCustomizationForm="True" ToolTip="Tipo de Consumidor" VisibleIndex="6">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="IMP. EXTERNO" FieldName="DecImporteExt" ShowInCustomizationForm="True" ToolTip="Tipo de Crédito Fiscal" VisibleIndex="7">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="IMP. PROPIO" FieldName="DecImporteProp" ShowInCustomizationForm="True" VisibleIndex="8">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="FECHA" FieldName="StrFecha" ShowInCustomizationForm="True" VisibleIndex="1">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="EST." FieldName="StrEstado" ShowInCustomizationForm="True" VisibleIndex="19">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="NF EXTERNO" FieldName="LngNoFactExt" ShowInCustomizationForm="True" VisibleIndex="5">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="PROD." FieldName="StrCodProducto" ShowInCustomizationForm="True" VisibleIndex="4">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="ID" FieldName="LngId" ShowInCustomizationForm="True" Visible="False" VisibleIndex="0">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="PRECIO" FieldName="DecPrecioExt" ShowInCustomizationForm="True" VisibleIndex="9">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="CANTIDAD" FieldName="DecCantidadExt" ShowInCustomizationForm="True" VisibleIndex="14">
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                    <TotalSummary>
                                                        <dx:ASPxSummaryItem FieldName="DecImporteProp" ShowInColumn="IMP. PROPIO" SummaryType="Sum" Tag="impP" />
                                                        <dx:ASPxSummaryItem FieldName="DecImporteExt" ShowInColumn="IMP. EXTERNO" SummaryType="Sum" Tag="impE" />
                                                        <dx:ASPxSummaryItem FieldName="DecCantidadExt" ShowInColumn="CANTIDAD" SummaryType="Sum" Tag="cantidad" />
                                                    </TotalSummary>
                                                </dx:ASPxGridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
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
































<dx:ASPxPopupControl ID="pucAmpliacion" runat="server" AllowDragging="True" ClientInstanceName="pucAmpliacion" EnableTheming="True" HeaderText="Ampliación de Plazos" Height="93px" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Above" Theme="Office2003Olive" Width="650px" OnWindowCallback="pucAmpliacion_WindowCallback">
    <HeaderImage IconID="scheduling_switchtimescalesto_16x16">
    </HeaderImage>
    <ClientSideEvents Closing="function(s, e) {
	pnlDatos.PerformCallback();
}" />
    <ContentCollection>
        <dx:PopupControlContentControl runat="server">
            <table class="dxflInternalEditorTable_Youthful">
                <tr>
                    <td style="width: 25%;">
                        <dx:ASPxLabel ID="ASPxLabel48" runat="server" Font-Bold="True" ForeColor="#003300" Text="Actividad :">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="txtPamActividad" runat="server" BackColor="#DEE8DB" ClientInstanceName="txtActividad" Width="100%">
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel49" runat="server" Font-Bold="True" ForeColor="#003300" Text="Departamento :">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="txtPamDepto" runat="server" BackColor="#DEE8DB" ClientInstanceName="txtDepto" Width="100%">
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel50" runat="server" Font-Bold="True" ForeColor="#003300" Text="ID Entidad :">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="txtPamIdConsumidor" runat="server" BackColor="#DEE8DB" ClientInstanceName="txtIdConsumidor" ReadOnly="True" Text="0" Width="170px">
                            <MaskSettings Mask="&lt;0..999999999999999&gt;" />
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel51" runat="server" Font-Bold="True" ForeColor="#003300" Text="Entidad :">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="txtPamConsumidor" runat="server" BackColor="#DEE8DB" ClientInstanceName="txtDepto" Width="100%">
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2">
                        
                        <asp:Panel ID="pnlRegistraAmp" runat="server">
                            <table style="width: 100%">
                                
                                <tr>
                    <td colspan="2">
                        <dx:ASPxLabel ID="ASPxLabel52" runat="server" Font-Bold="True" ForeColor="#003300" Text="NUEVA AMPLIACION">
                        </dx:ASPxLabel>
                        <hr/>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <dx:ASPxLabel ID="ASPxLabel56" runat="server" Font-Bold="True" Font-Underline="True" ForeColor="#003300" Text="Periodo Inicial de Apertura">
                        </dx:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td style="width: 25%; text-align: right;">
                        <dx:ASPxLabel ID="ASPxLabel73" runat="server" Font-Bold="False" ForeColor="#003300" Text="Mes :">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxComboBox ID="cmbPamMes" runat="server" ClientInstanceName="cmbMes" Theme="Metropolis" ValueType="System.Int32" Width="140px">
                            <Items>
                                <dx:ListEditItem Text="ENERO" Value="1" />
                                <dx:ListEditItem Text="FEBRERO" Value="2" />
                                <dx:ListEditItem Text="MARZO" Value="3" />
                                <dx:ListEditItem Text="ABRIL" Value="4" />
                                <dx:ListEditItem Text="MAYO" Value="5" />
                                <dx:ListEditItem Text="JUNIO" Value="6" />
                                <dx:ListEditItem Text="JULIO" Value="7" />
                                <dx:ListEditItem Text="AGOSTO" Value="8" />
                                <dx:ListEditItem Text="SEPTIEMBRE" Value="9" />
                                <dx:ListEditItem Text="OCTUBRE" Value="10" />
                                <dx:ListEditItem Text="NOVIEMBRE" Value="11" />
                                <dx:ListEditItem Text="DICIEMBRE" Value="12" />
                            </Items>
                            <ClearButton Visibility="Auto">
                            </ClearButton>
                        </dx:ASPxComboBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">
                        <dx:ASPxLabel ID="ASPxLabel74" runat="server" Font-Bold="False" ForeColor="#003300" Text="Gestión :">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxComboBox ID="cmbPamGestion" runat="server" ClientInstanceName="cmbGestion" Theme="Metropolis" ValueType="System.Int32" Width="140px">
                            <Items>
                                <dx:ListEditItem Text="2015" Value="2015" />
                                <dx:ListEditItem Text="2016" Value="2016" />
                                <dx:ListEditItem Text="2017" Value="2017" />
                                <dx:ListEditItem Text="2018" Value="2018" />
                                <dx:ListEditItem Text="2019" Value="2019" />
                                <dx:ListEditItem Text="2020" Value="2020" />
                                <dx:ListEditItem Text="2021" Value="2021" />
                                <dx:ListEditItem Text="2022" Value="2022" />
                                <dx:ListEditItem Text="2023" Value="2023" />
                                <dx:ListEditItem Text="2024" Value="2024" />
                                <dx:ListEditItem Text="2025" Value="2025" />
                            </Items>
                            <ClearButton Visibility="Auto">
                            </ClearButton>
                        </dx:ASPxComboBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel75" runat="server" Font-Bold="True" Font-Underline="True" ForeColor="#003300" Text="Fechas de Habilitación">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: right;">
                        <dx:ASPxLabel ID="ASPxLabel53" runat="server" Font-Bold="False" ForeColor="#003300" Text="Desde :">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxDateEdit ID="detPamFechaIni" runat="server" ClientInstanceName="detFechaIni" EnableTheming="True" Height="23px" Theme="Metropolis" Width="140px">
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
                    <td style="text-align: right;">
                        <dx:ASPxLabel ID="ASPxLabel54" runat="server" Font-Bold="False" ForeColor="#003300" Text="Hasta :">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxDateEdit ID="detPamFechaFin" runat="server" ClientInstanceName="detFechaFin" EnableTheming="True" Height="23px" Theme="Metropolis" Width="140px">
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
                    <td style="text-align: right;">
                        <dx:ASPxLabel ID="ASPxLabel55" runat="server" Font-Bold="False" ForeColor="#003300" Text="Observaciones :">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="txtPamObservacion" runat="server" ClientInstanceName="txtPcObservacion" Width="100%">
                            <MaskSettings PromptChar=" " />
                            <ReadOnlyStyle BackColor="#DEE8DB">
                            </ReadOnlyStyle>
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: right;">
                        <dx:ASPxButton ID="btnPamAgregar" runat="server" Text="Agregar Ampliación" Theme="Metropolis" AutoPostBack="False" Width="200px" OnClick="btnPamAgregar_Click">
                            <Image IconID="actions_download_16x16">
                            </Image>
                        </dx:ASPxButton>
                        <hr/>
                    </td>
                </tr>

                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                
                
                
                
                
                <tr>
                    <td colspan="2">
                        <dx:ASPxLabel ID="ASPxLabel76" runat="server" Font-Bold="True" ForeColor="#003300" Text="AMPLIACIONES REGISTRADAS">
                        </dx:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        
                        <dx:ASPxGridView ID="dgvAmpliacion" runat="server" AutoGenerateColumns="False" ClientInstanceName="dgvHistorico" EnableTheming="True" Font-Size="Smaller" KeyFieldName="ID_AUTORIZACION_CONSUMIDOR" Theme="Default" Width="100%" OnCustomCallback="dgvAmpliacion_CustomCallback" OnHtmlRowPrepared="dgvAmpliacion_HtmlRowPrepared" OnCustomButtonCallback="dgvAmpliacion_CustomButtonCallback">
                            <ClientSideEvents CustomButtonClick="function(s, e) {

switch(e.buttonID) {

case&nbsp; 'btnAnular':
e.processOnServer = confirm('Atención. Una vez anulado el registro no podrá deshacer el proceso. ¿Desea anular la ampliación?'); 
break;

} 

}
" />
                            <SettingsContextMenu EnableRowMenu="False">
                            </SettingsContextMenu>
                            <Templates>
                                <EditForm>
                                </EditForm>
                            </Templates>
                            <SettingsPager>
                                <PageSizeItemSettings Items="50, 100, 200, 500, 1000" Visible="True">
                                </PageSizeItemSettings>
                            </SettingsPager>
                            <SettingsEditing Mode="PopupEditForm">
                            </SettingsEditing>
                            <Settings ShowFooter="True" ShowHeaderFilterButton="True" />
                            <SettingsBehavior AllowFocusedRow="True" />
                            <SettingsPopup>
                                <EditForm Modal="True" />
                                <CustomizationWindow HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" />
                            </SettingsPopup>
                            <SettingsText CommandNew="Nuevo" />
                            <Columns>
                                <dx:GridViewCommandColumn ShowInCustomizationForm="True" VisibleIndex="22">
                                    <CustomButtons>
                                        <dx:GridViewCommandColumnCustomButton ID="btnAnular" Text="Anular">
                                        </dx:GridViewCommandColumnCustomButton>
                                    </CustomButtons>
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn Caption="PERIODO FECHA INICIAL" FieldName="FECHA_INICIO" ShowInCustomizationForm="True" ToolTip="Tipo de Pago" VisibleIndex="5" Visible="False">
                                    <Settings AutoFilterCondition="Contains" />
                                    <CellStyle HorizontalAlign="Right">
                                    </CellStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="USUARIO" FieldName="APP_USUARIO" ShowInCustomizationForm="True" ToolTip="Tipo de Consumidor" VisibleIndex="3">
                                    <Settings AutoFilterCondition="Contains" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="OBSERVACIONES" FieldName="OBSERVACIONES" ShowInCustomizationForm="True" ToolTip="Tipo de Crédito Fiscal" VisibleIndex="14">
                                    <Settings AutoFilterCondition="Contains" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="ESTADO" FieldName="ESTADO" ShowInCustomizationForm="True" Visible="False" VisibleIndex="13">
                                    <Settings AutoFilterCondition="Contains" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="FECHA REGISTRO" FieldName="APP_FECHA_REGISTRO" ShowInCustomizationForm="True" VisibleIndex="1" Visible="False">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="ESTADO" FieldName="strEstado" ShowInCustomizationForm="True" VisibleIndex="12">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="ID_AUTORIZACION_CONSUMIDOR" FieldName="ID_AUTORIZACION_CONSUMIDOR" ShowInCustomizationForm="True" VisibleIndex="0" Visible="False">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="FECHA FINAL" FieldName="FECHA_FIN" ShowInCustomizationForm="True" VisibleIndex="6" Visible="False">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="FECHA_FIN_ACT" FieldName="FECHA_FIN_ACT" ShowInCustomizationForm="True" VisibleIndex="9" Visible="False">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="FECHA_INICIO_ACT" FieldName="FECHA_INICIO_ACT" ShowInCustomizationForm="True" VisibleIndex="7" ToolTip="Fecha de Habilitación (Desde)" Visible="False">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="ID_CONSUMIDOR" FieldName="ID_CONSUMIDOR" ShowInCustomizationForm="True" Visible="False" VisibleIndex="15">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="PERIODO DE AMP." FieldName="strPeriodo" ShowInCustomizationForm="True" VisibleIndex="4" ToolTip="Periodo Inicial de Ampliación">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="FECHA REGISTRO" FieldName="strFechaRegistro" ShowInCustomizationForm="True" VisibleIndex="2">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="DESDE" FieldName="strFechaActIni" ShowInCustomizationForm="True" VisibleIndex="8">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="HASTA" FieldName="strFechaActFin" ShowInCustomizationForm="True" VisibleIndex="10">
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <TotalSummary>
                                <dx:ASPxSummaryItem FieldName="DecRecepcion" ShowInColumn="RECEPCION" SummaryType="Custom" Tag="rec" />
                                <dx:ASPxSummaryItem FieldName="DecDemasia" ShowInColumn="DEMASIA" SummaryType="Custom" Tag="dem" />
                                <dx:ASPxSummaryItem FieldName="DecTotalIng" ShowInColumn="TOT. ING." SummaryType="Custom" Tag="ing" />
                                <dx:ASPxSummaryItem FieldName="DecVenta" ShowInColumn="VENTA" SummaryType="Custom" Tag="ven" />
                                <dx:ASPxSummaryItem FieldName="DecMerma" ShowInColumn="MERMA" SummaryType="Custom" Tag="mer" />
                                <dx:ASPxSummaryItem FieldName="DecTotalSal" ShowInColumn="TOT. SAL." SummaryType="Custom" Tag="sal" />
                            </TotalSummary>
                        </dx:ASPxGridView>
                        
                    </td>

                </tr>
            </table>
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>
































<dx:ASPxPopupControl ID="pucHistorial" runat="server" AllowDragging="True" ClientInstanceName="pucHistorial" EnableTheming="True" HeaderText="Historial de Eventos" Height="93px" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Above" Theme="Office2003Olive" Width="750px" OnWindowCallback="pucHistorial_WindowCallback">
    <HeaderImage IconID="mail_news_16x16">
    </HeaderImage>
    <ContentCollection>
        <dx:PopupControlContentControl runat="server">
            <table class="dxflInternalEditorTable_Youthful">
                <tr>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel77" runat="server" Font-Bold="True" ForeColor="#003300" Text="Periodo :">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="txtPhiPeriodo" runat="server" ReadOnly="True" Width="170px">
                            <MaskSettings PromptChar=" " />
                            <ReadOnlyStyle BackColor="#DEE8DB">
                            </ReadOnlyStyle>
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel78" runat="server" Font-Bold="True" ForeColor="#003300" Text="ID Entidad :">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="txtPhiIdConsumidor" runat="server" BackColor="#DEE8DB" ClientInstanceName="txtIdConsumidor" ReadOnly="True" Text="0" Width="170px">
                            <MaskSettings Mask="&lt;0..999999999999999&gt;" />
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel39" runat="server" Font-Bold="True" ForeColor="#003300" Text="Entidad :">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="txtPhiConsumidor" runat="server" ReadOnly="True" Width="100%">
                            <MaskSettings PromptChar=" " />
                            <ReadOnlyStyle BackColor="#DEE8DB">
                            </ReadOnlyStyle>
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        
                        <dx:ASPxGridView ID="dgvHistorial" runat="server" AutoGenerateColumns="False" ClientInstanceName="dgvHistorial" EnableTheming="True" Font-Size="Smaller" KeyFieldName="APP_FECHA_REGISTRO" Theme="Default" Width="100%" OnCustomCallback="dgvHistorial_CustomCallback" OnHtmlRowPrepared="dgvHistorial_HtmlRowPrepared">
                            <SettingsContextMenu EnableRowMenu="False">
                            </SettingsContextMenu>
                            <Templates>
                                <EditForm>
                                </EditForm>
                            </Templates>
                            <SettingsPager>
                                <PageSizeItemSettings Items="50, 100, 200, 500, 1000" Visible="True">
                                </PageSizeItemSettings>
                            </SettingsPager>
                            <SettingsEditing Mode="PopupEditForm">
                            </SettingsEditing>
                            <Settings ShowFooter="True" ShowHeaderFilterButton="True" />
                            <SettingsBehavior AllowFocusedRow="True" />
                            <SettingsPopup>
                                <EditForm Modal="True" />
                                <CustomizationWindow HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" />
                            </SettingsPopup>
                            <SettingsText CommandNew="Nuevo" />
                            <Columns>
                                <dx:GridViewCommandColumn ShowInCustomizationForm="True" Visible="False" VisibleIndex="22">
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn Caption="APP_FECHA_REGISTRO" FieldName="APP_FECHA_REGISTRO" ShowInCustomizationForm="True" ToolTip="Tipo de Pago" Visible="False" VisibleIndex="0">
                                    <Settings AutoFilterCondition="Contains" />
                                    <CellStyle HorizontalAlign="Right">
                                    </CellStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="USUARIO" FieldName="APP_USUARIO" ShowInCustomizationForm="True" ToolTip="Tipo de Consumidor" VisibleIndex="2">
                                    <Settings AutoFilterCondition="Contains" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="OBSERVACIONES" FieldName="OBSERVACIONES" ShowInCustomizationForm="True" VisibleIndex="11">
                                    <Settings AutoFilterCondition="Contains" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="ESTADO" FieldName="ESTADO" ShowInCustomizationForm="True" Visible="False" VisibleIndex="5">
                                    <Settings AutoFilterCondition="Contains" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="FECHA" FieldName="strFechaRegistro" ShowInCustomizationForm="True" VisibleIndex="1">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="ESTADO" FieldName="strEstado" ShowInCustomizationForm="True" VisibleIndex="6">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="EVENTO" FieldName="TIPO_BITACORA" ShowInCustomizationForm="True" VisibleIndex="3">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="CITE DDJJ" FieldName="CITE_GENERADO" ShowInCustomizationForm="True" VisibleIndex="4">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="FECHA_INICIO" FieldName="FECHA_INICIO" ShowInCustomizationForm="True" Visible="False" VisibleIndex="7">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="PERIODO" FieldName="strFechaInicio" ShowInCustomizationForm="True" VisibleIndex="8">
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <TotalSummary>
                                <dx:ASPxSummaryItem FieldName="DecRecepcion" ShowInColumn="RECEPCION" SummaryType="Custom" Tag="rec" />
                                <dx:ASPxSummaryItem FieldName="DecDemasia" ShowInColumn="DEMASIA" SummaryType="Custom" Tag="dem" />
                                <dx:ASPxSummaryItem FieldName="DecTotalIng" ShowInColumn="TOT. ING." SummaryType="Custom" Tag="ing" />
                                <dx:ASPxSummaryItem FieldName="DecVenta" ShowInColumn="VENTA" SummaryType="Custom" Tag="ven" />
                                <dx:ASPxSummaryItem FieldName="DecMerma" ShowInColumn="MERMA" SummaryType="Custom" Tag="mer" />
                                <dx:ASPxSummaryItem FieldName="DecTotalSal" ShowInColumn="TOT. SAL." SummaryType="Custom" Tag="sal" />
                            </TotalSummary>
                        </dx:ASPxGridView>
                        
                    </td>

                </tr>
            </table>
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>
























































<dx:ASPxPopupControl ID="pucIntimacion" runat="server" AllowDragging="True" ClientInstanceName="pucIntimacion" EnableTheming="True" HeaderText="Registro de intimaciones" Height="93px" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Above" Theme="Office2003Olive" Width="650px" OnWindowCallback="pucIntimacion_WindowCallback">
    <HeaderImage IconID="functionlibrary_datetime_16x16">
    </HeaderImage>
    <ClientSideEvents Closing="function(s, e) {
	pnlDatos.PerformCallback();
}" />
    <ContentCollection>
        <dx:PopupControlContentControl runat="server">
            <table class="dxflInternalEditorTable_Youthful">
                <tr>
                    <td style="width: 25%;">
                        <dx:ASPxLabel ID="ASPxLabel57" runat="server" Font-Bold="True" ForeColor="#003300" Text="Actividad :">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="txtPinActividad" runat="server" BackColor="#DEE8DB" ClientInstanceName="txtActividad" Width="100%">
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel58" runat="server" Font-Bold="True" ForeColor="#003300" Text="Departamento :">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="txtPinDepto" runat="server" BackColor="#DEE8DB" ClientInstanceName="txtDepto" Width="100%">
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel59" runat="server" Font-Bold="True" ForeColor="#003300" Text="ID Consumidor :">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="txtPinIdConsumidor" runat="server" BackColor="#DEE8DB" ClientInstanceName="txtIdConsumidor" ReadOnly="True" Text="0" Width="170px">
                            <MaskSettings Mask="&lt;0..999999999999999&gt;" />
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel60" runat="server" Font-Bold="True" ForeColor="#003300" Text="Consumidor :">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="txtPinConsumidor" runat="server" BackColor="#DEE8DB" ClientInstanceName="txtDepto" Width="100%">
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Panel ID="pnlIntimacion" runat="server">
                            <table style="width: 100%">
                                <tr>
                                    <td colspan="2">
                                        <dx:ASPxLabel ID="ASPxLabel61" runat="server" Font-Bold="True" ForeColor="#003300" Text="NUEVA INTIMACIÓN">
                                        </dx:ASPxLabel>
                                        <hr/>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">
                                        <dx:ASPxLabel ID="ASPxLabel62" runat="server" Font-Bold="True" ForeColor="#003300" Text="Periodo :">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <dx:ASPxTextBox ID="txtPinPeriodo" runat="server" BackColor="#DEE8DB" ClientInstanceName="txtDepto" Width="140px">
                                        </dx:ASPxTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">
                                        <dx:ASPxLabel ID="ASPxLabel66" runat="server" Font-Bold="True" ForeColor="#003300" Text="Descripción :">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <dx:ASPxTextBox ID="txtPinDescripcion" runat="server" BackColor="#DEE8DB" ClientInstanceName="txtDepto" Text="Incumplimiento a la obligación mensual - Registro de Declaración Jurada" Width="100%">
                                        </dx:ASPxTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">
                                        <dx:ASPxLabel ID="ASPxLabel63" runat="server" Font-Bold="True" ForeColor="#003300" Text="Fecha/Hora Notificación :" Visible="False">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <dx:ASPxDateEdit ID="detPinFechaNot" runat="server" ClientInstanceName="detFechaIni" EnableTheming="True" Height="23px" Theme="Metropolis" Width="170px" EditFormat="DateTime" Visible="False">
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
                                            <ValidationSettings>
                                                <RequiredField ErrorText="Introduzca la fecha" IsRequired="True" />
                                            </ValidationSettings>
                                        </dx:ASPxDateEdit>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">
                                        <dx:ASPxLabel ID="ASPxLabel67" runat="server" Font-Bold="True" ForeColor="#003300" Text="Código de Barras :">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <dx:ASPxTextBox ID="txtPinCb" runat="server" ClientInstanceName="txtIdConsumidor" Text="0" Width="170px">
                                            <MaskSettings Mask="&lt;0..999999999999999&gt;" />
                                        </dx:ASPxTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">
                                        <dx:ASPxLabel ID="ASPxLabel64" runat="server" Font-Bold="True" ForeColor="#003300" Text="Persona Notificante (ANH) :" Visible="False">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <dx:ASPxTextBox ID="txtPinNotificante" runat="server" ClientInstanceName="txtPcObservacion" Width="350px" Visible="False">
                                            <MaskSettings PromptChar=" " />
                                            <ValidationSettings>
                                                <RequiredField ErrorText="Introduzca el dato" IsRequired="True" />
                                            </ValidationSettings>
                                            <ReadOnlyStyle BackColor="#DEE8DB">
                                            </ReadOnlyStyle>
                                        </dx:ASPxTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">
                                        <dx:ASPxLabel ID="ASPxLabel68" runat="server" Font-Bold="True" ForeColor="#003300" Text="Persona Notificada :" Visible="False">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <dx:ASPxTextBox ID="txtPinNotificado" runat="server" ClientInstanceName="txtPcObservacion" Width="350px" Visible="False">
                                            <MaskSettings PromptChar=" " />
                                            <ValidationSettings>
                                                <RequiredField ErrorText="Introduzca el dato" IsRequired="True" />
                                            </ValidationSettings>
                                            <ReadOnlyStyle BackColor="#DEE8DB">
                                            </ReadOnlyStyle>
                                        </dx:ASPxTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">
                                        <dx:ASPxLabel ID="ASPxLabel69" runat="server" Font-Bold="True" ForeColor="#003300" Text="Fecha de Inicio :" Visible="False">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <dx:ASPxDateEdit ID="detPinFechaInicio" runat="server" ClientInstanceName="detFechaIni" EnableTheming="True" Height="23px" Theme="Metropolis" Width="170px" Visible="False">
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
                                            <ValidationSettings>
                                                <RequiredField ErrorText="Introduzca la fecha" IsRequired="True" />
                                            </ValidationSettings>
                                        </dx:ASPxDateEdit>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">
                                        <dx:ASPxLabel ID="ASPxLabel70" runat="server" Font-Bold="True" ForeColor="#003300" Text="Días Plazo :" Visible="False">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <dx:ASPxSpinEdit ID="spnPinDiasPlazo" runat="server" MaxValue="10" MinValue="1" Number="2" Theme="Metropolis" Visible="False">
                                            <ClientSideEvents LostFocus="function(s, e) {
if(s.GetNumber()==null)
{
s.SetNumber(2);
}
//	alert(s.GetNumber());
}" />
                                            <ClearButton Visibility="Auto">
                                            </ClearButton>
                                            <ValidationSettings>
                                                <RequiredField ErrorText="Introduzca el dato" IsRequired="True" />
                                            </ValidationSettings>
                                        </dx:ASPxSpinEdit>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">
                                        <dx:ASPxLabel ID="ASPxLabel65" runat="server" Font-Bold="True" ForeColor="#003300" Text="Observaciones :">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <dx:ASPxTextBox ID="txtPinObservacion" runat="server" ClientInstanceName="txtPcObservacion" Width="100%">
                                            <MaskSettings PromptChar=" " />
                                            <ReadOnlyStyle BackColor="#DEE8DB">
                                            </ReadOnlyStyle>
                                        </dx:ASPxTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="text-align: right;">
                                        <dx:ASPxButton ID="btnPinAgregar" runat="server" Text="Guardar Intimación" Theme="Metropolis" AutoPostBack="False" Width="200px" OnClick="btnPinAgregar_Click">
                                            <Image IconID="actions_download_16x16">
                                            </Image>
                                        </dx:ASPxButton>
                                        <hr/>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <dx:ASPxLabel ID="ASPxLabel79" runat="server" Font-Bold="True" ForeColor="#003300" Text="INTIMACIONES REGISTRADAS">
                                        </dx:ASPxLabel>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <dx:ASPxGridView ID="dgvIntimacion" runat="server" AutoGenerateColumns="False" ClientInstanceName="dgvHistorico" EnableTheming="True" Font-Size="Smaller" KeyFieldName="ID_AUTORIZACION_CONSUMIDOR" Theme="Default" Width="100%" OnCustomCallback="dgvIntimacion_CustomCallback" OnCustomButtonCallback="dgvIntimacion_CustomButtonCallback">
                            <SettingsContextMenu EnableRowMenu="False">
                            </SettingsContextMenu>
                            <Templates>
                                <EditForm>
                                </EditForm>
                            </Templates>
                            <SettingsPager>
                                <PageSizeItemSettings Items="50, 100, 200, 500, 1000" Visible="True">
                                </PageSizeItemSettings>
                            </SettingsPager>
                            <SettingsEditing Mode="PopupEditForm">
                            </SettingsEditing>
                            <Settings ShowFooter="True" ShowHeaderFilterButton="True" />
                            <SettingsBehavior AllowFocusedRow="True" />
                            <SettingsPopup>
                                <EditForm Modal="True" />
                                <CustomizationWindow HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" />
                            </SettingsPopup>
                            <SettingsText CommandNew="Nuevo" />
                            <Columns>
                                <dx:GridViewCommandColumn ShowInCustomizationForm="True" VisibleIndex="22" Visible="False">
                                    <CustomButtons>
                                        <dx:GridViewCommandColumnCustomButton ID="btnReimprimir" Text="Reimprimir">
                                        </dx:GridViewCommandColumnCustomButton>
                                    </CustomButtons>
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn Caption="FECHA NOTIFICACION" FieldName="strFechaActIni" ShowInCustomizationForm="True" ToolTip="Fecha de Inicio de la Intimación" VisibleIndex="3">
                                    <Settings AutoFilterCondition="Contains" />
                                    <CellStyle HorizontalAlign="Right">
                                    </CellStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="USUARIO" FieldName="APP_USUARIO" ShowInCustomizationForm="True" VisibleIndex="2">
                                    <Settings AutoFilterCondition="Contains" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="OBSERVACIONES" FieldName="OBSERVACIONES" ShowInCustomizationForm="True" VisibleIndex="8" Visible="False">
                                    <Settings AutoFilterCondition="Contains" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="ESTADO" FieldName="ESTADO" ShowInCustomizationForm="True" Visible="False" VisibleIndex="6">
                                    <Settings AutoFilterCondition="Contains" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="FECHA REGISTRO" FieldName="strFechaRegistro" ShowInCustomizationForm="True" VisibleIndex="1">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="ESTADO" FieldName="strEstado" ShowInCustomizationForm="True" VisibleIndex="5" Visible="False">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="ID" FieldName="ID_AUTORIZACION_CONSUMIDOR" ShowInCustomizationForm="True" VisibleIndex="0" Visible="False">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="DIAS PLAZO" FieldName="FECHA_FIN_ACT" ShowInCustomizationForm="True" VisibleIndex="4" Visible="False">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="COD. BARRAS" FieldName="FECHA_FIN_ACT" ShowInCustomizationForm="True" VisibleIndex="7">
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <TotalSummary>
                                <dx:ASPxSummaryItem FieldName="DecRecepcion" ShowInColumn="RECEPCION" SummaryType="Custom" Tag="rec" />
                                <dx:ASPxSummaryItem FieldName="DecDemasia" ShowInColumn="DEMASIA" SummaryType="Custom" Tag="dem" />
                                <dx:ASPxSummaryItem FieldName="DecTotalIng" ShowInColumn="TOT. ING." SummaryType="Custom" Tag="ing" />
                                <dx:ASPxSummaryItem FieldName="DecVenta" ShowInColumn="VENTA" SummaryType="Custom" Tag="ven" />
                                <dx:ASPxSummaryItem FieldName="DecMerma" ShowInColumn="MERMA" SummaryType="Custom" Tag="mer" />
                                <dx:ASPxSummaryItem FieldName="DecTotalSal" ShowInColumn="TOT. SAL." SummaryType="Custom" Tag="sal" />
                            </TotalSummary>
                        </dx:ASPxGridView>
                    </td>
                </tr>
                <tr>
                    <td>
                        
                        &nbsp;</td>
                </tr>
            </table>
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>

























































































