<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtrRepDosificacion.ascx.cs" Inherits="AnhHydroOctanoPresentacion.Controles.CtrRepDosificacion" %>
<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>

<%@ Register src="CtrFecheroMes.ascx" tagname="CtrFecheroMes" tagprefix="uc1" %>

<style type="text/css">
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
    
    </style>

<div class="col-md-12">
    <table style="width: 100%">
        <tr>
            <td style="text-align: center; padding-bottom: 1px">
                <dx:ASPxRoundPanel runat="server" ShowHeader="False" Width="100%" ID="pnlTitulo">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <dx:ASPxLabel runat="server" ForeColor="#367B2F" ID="lblTitulo" Font-Bold="True" Font-Names="Rockwell" Font-Size="Larger" Text="REPORTE - CONTROL DE DOSIFICACIONES DE FACTURAS"></dx:ASPxLabel>
                    </dx:PanelContent>
                </PanelCollection>
                <Border BorderStyle="Ridge" BorderWidth="1px" BorderColor="#003300"></Border>
                </dx:ASPxRoundPanel>
            </td>
        </tr>
    </table>

    <dx:ASPxSplitter ID="ASPxSplitter1" runat="server" Height="100%">
        <Panes>
            <dx:SplitterPane Size="20%" AllowResize="True" AutoHeight="True">
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
                                    Fecha Inicial:
                                    <dx:ASPxDateEdit ID="detFechaIni" runat="server" ClientInstanceName="detFechaIni" EnableTheming="True" Height="23px" Theme="Metropolis" ToolTip="Seleccione la fecha inicial" Width="140px">
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
                                    Fecha Final :
                                    <dx:ASPxDateEdit ID="detFechaFin" runat="server" ClientInstanceName="detFechaFin" Theme="Metropolis" ToolTip="Seleccione la fecha final" Width="140px">
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
                                <td style="padding-top: 5px">
                                    Selección Directa de Mes :
                                    <uc1:CtrFecheroMes ID="cfmMes" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-top: 5px">
                                    <dx:ASPxButton ID="btnActualizar" runat="server" AutoPostBack="False" Font-Bold="True" ForeColor="Maroon" Text="Obtener Información" Theme="Metropolis" Width="73%">
                                        <ClientSideEvents Click="function(s, e) {
	pnlDatos.PerformCallback();
}" />
                                        <Image IconID="actions_download_16x16">
                                        </Image>
                                    </dx:ASPxButton>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-top: 5px">
                                    <dx:ASPxLabel ID="ASPxLabel58" runat="server" ForeColor="#003300" Text="Proyectos Asignados :">
                                    </dx:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-top: 5px">
                                    <dx:ASPxTextBox ID="txtBuscador" runat="server" BackColor="#FFEFDF" ClientInstanceName="txtBuscador" Font-Size="Smaller" Theme="Metropolis" ToolTip="Buscador" Width="100%">
                                        <BorderTop BorderColor="#FF6600" BorderStyle="Solid" />
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <dx:ASPxButton ID="btnBuscarProyecto" runat="server" AutoPostBack="false" ClientInstanceName="btnBuscarProyecto" Text="Buscar Proyecto" >
                                        <ClientSideEvents Click="function(s, e) {
	                                                    tlsOperadores.PerformCallback();
                                                    }" />
                                        <Image IconID="zoom_zoom_16x16">
                                        </Image>
                                    </dx:ASPxButton>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <dx:ASPxTreeList ID="tlsOperadores" runat="server" AutoGenerateColumns="False" ClientInstanceName="tlsOperadores" Font-Size="Smaller" Height="187px" KeyFieldName="intId" OnCustomCallback="tlsOperadores_CustomCallback" ParentFieldName="intIdPadre" Theme="Youthful" Styles-Header-BackColor="#317D40" ToolTip="Seleccione una entidad">
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
                                        <SettingsBehavior AllowFocusedNode="True" FocusNodeOnExpandButtonClick="False" FocusNodeOnLoad="False" />
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
                                            <td colspan="7" style="text-align: center;">
                                                <dx:ASPxRoundPanel ID="pnlMensaje" runat="server" ShowHeader="False" Visible="False" Width="100%">
                                                    <PanelCollection>
                                                        <dx:PanelContent runat="server">
                                                            <dx:ASPxLabel ID="lblMensajeError" runat="server" ForeColor="#666666">
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
                                                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Rockwell" Font-Size="Medium" ForeColor="#367B2F" Text="DATOS DEL PROYECTO"></asp:Label>
                                                </legend>
                                            </td>
                                        </tr>
                                        <tr style="width: 100%">
                                            <td colspan="3" style="width: 35%">
                                                <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="#10420F" Text="NOMBRE EMPRESA"></asp:Label>
                                            </td>
                                            <td colspan="2" style="width: 20%">
                                                <asp:Label ID="Label5" runat="server" Font-Bold="True" ForeColor="#10420F" Text="ACTIVIDAD"></asp:Label>
                                            </td>
                                            <td style="width: 35%">
                                                <asp:Label ID="Label4" runat="server" Font-Bold="True" ForeColor="#10420F" Text="PROYECTO"></asp:Label>
                                            </td>
                                            <td style="width: 10%; text-align: right">
                                                
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <asp:Label ID="lblEmpresa" runat="server"></asp:Label>
                                            </td>
                                            <td colspan="2">
                                                <asp:Label ID="lblActividad" runat="server"></asp:Label>
                                            </td>
                                            <td colspan="2">
                                                <asp:Label ID="lblProyecto" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="7">
                                                <dx:ASPxGridView ID="dgvCabecera" runat="server" AutoGenerateColumns="False" ClientInstanceName="dgvCabecera" EnableTheming="True" Font-Size="Smaller" KeyFieldName="NRO_AUTORIZACION" Theme="Default" Width="100%" >
                                                    <ClientSideEvents CustomButtonClick="function(s, e) {
s.GetRowValues(e.visibleIndex, 'NRO_AUTORIZACION', function (value) {
lblTituloDetalle.SetValue('DETALLE DE LA DOSIFICACION - NO. AUTORIZACION : ' + value);

if(value == 0)
{
alert('ATENCION: El número de autorización no aplica para la función.');
}


});
switch(e.buttonID) {
case&nbsp; 'btnDetalle':
dgvDetalle.PerformCallback();
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
                                                    <SettingsPager NumericButtonCount="2">
                                                        <PageSizeItemSettings Items="50, 100, 200, 500, 1000" Visible="True">
                                                        </PageSizeItemSettings>
                                                    </SettingsPager>
                                                    <SettingsEditing Mode="PopupEditForm">
                                                    </SettingsEditing>
                                                    <Settings ShowHeaderFilterButton="True" ShowFooter="True" ShowFilterRow="True" />
                                                    <SettingsBehavior AllowFocusedRow="True" />
                                                    <SettingsPopup>
                                                        <EditForm Modal="True" />
                                                        <CustomizationWindow HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" />
                                                    </SettingsPopup>
                                                    <SettingsText CommandNew="Nuevo" />
                                                    <Columns>
                                                        <dx:GridViewCommandColumn ShowInCustomizationForm="True" VisibleIndex="7" ShowClearFilterButton="True" ToolTip="Funciones">
                                                            <CustomButtons>
                                                                <dx:GridViewCommandColumnCustomButton ID="btnDetalle" Text="VerDetalle">
                                                                </dx:GridViewCommandColumnCustomButton>
                                                            </CustomButtons>
                                                        </dx:GridViewCommandColumn>
                                                        <dx:GridViewDataTextColumn Caption="IMPORTE TOTAL" FieldName="MONTO" ShowInCustomizationForm="True" VisibleIndex="4">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="TIPO EMISOR" FieldName="TIPO_EMISOR" ShowInCustomizationForm="True" VisibleIndex="2">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="NRO. AUTORIZACION" FieldName="NRO_AUTORIZACION" ShowInCustomizationForm="True" VisibleIndex="1" ToolTip="Número de Autorización">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="TIPO FACTURACION" FieldName="TIPO_FACTURACION" ShowInCustomizationForm="True" VisibleIndex="3">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="TIPO OPERACION" FieldName="TIPO_OPERACION" ShowInCustomizationForm="True" VisibleIndex="0">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                    <Styles CssPostfix="DevGlp"><AlternatingRow BackColor="#EEEEEE">
                                                        </AlternatingRow></Styles>
                                                </dx:ASPxGridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="7" style="padding-top: 5px">
                                                <dx:ASPxLabel ID="lblTituloDetalle" runat="server" ClientInstanceName="lblTituloDetalle" Font-Bold="True" Font-Underline="False" ForeColor="#FF8800" Text="DETALLE DE LA DOSIFICACION" Width="100%">
                                                    <BorderTop BorderStyle="Solid" />
                                                </dx:ASPxLabel>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td colspan="7">
                                                <dx:ASPxPageControl ID="pctDetalle" runat="server" ActiveTabIndex="0" Width="100%" Theme="Default" EnableTheming="True">
                                                    <TabPages>
                                                        <dx:TabPage Text="   DETALLE DE REGISTROS">
                                                            <TabImage IconID="grid_grid_16x16">
                                                            </TabImage>
                                                            <ContentCollection>
                                                                <dx:ContentControl runat="server">
                                                                    <table style="width: 100%">
                                                                        
                                                                        <tr style="width: 100%">
                                                                            <td style="width: 70%">
                                                                            </td>
                                                                            <td style="width: 15%">
                                                                               <dx:ASPxComboBox ID="cmbTipoExport" runat="server" ClientInstanceName="cmbTipoExport" Height="27px" SelectedIndex="0" Theme="Metropolis" ValueType="System.Int32" Width="100%" ToolTip="Seleccione el Formato del Reporte">
                                                                                    <Items>
                                                                                        <dx:ListEditItem Selected="True" Text="EXCEL" Value="1" />
                                                                                        <dx:ListEditItem Text="PDF" Value="2" />
                                                                                    </Items>
                                                                                    <ClearButton Visibility="Auto">
                                                                                    </ClearButton>
                                                                                </dx:ASPxComboBox>
                                                
                                                                            </td>
                                                                            <td style="width: 15%; text-align: right;">
                                                                                <dx:ASPxButton ID="btnEmiteReporte" runat="server" Font-Bold="True" ForeColor="Maroon" Text="Emitir Reporte" Theme="Metropolis" Width="100%" OnClick="btnEmiteReporte_Click" AutoPostBack="False">
                                                                                    <Image IconID="print_printer_16x16">
                                                                                    </Image>
                                                                                </dx:ASPxButton>
                                                                            </td>
                                                                        </tr>
                                    
                                                                        <tr>
                                                                            <td colspan="3">
                                                                                <dx:ASPxGridView ID="dgvDetalle" runat="server" AutoGenerateColumns="False" ClientInstanceName="dgvDetalle" EnableTheming="True" Font-Size="Smaller" KeyFieldName="ID_MOVIMIENTO" Theme="Default" Width="100%" OnHtmlRowPrepared="dgvDetalle_HtmlRowPrepared" OnCustomCallback="dgvDetalle_CustomCallback">
                                                                                    <ClientSideEvents BeginCallback="function(s, e) {
	                                LoadingPanel.Show();
                                }" EndCallback="function(s, e) {
                                pnlGrafico.PerformCallback();
                                LoadingPanel.Hide();
                                }" />
                                                                                    <SettingsContextMenu EnableRowMenu="False">
                                                                                    </SettingsContextMenu>
                                                                                    <Templates>
                                                                                        <EditForm>
                                                                                        </EditForm>
                                                                                    </Templates>
                                                                                    <SettingsPager PageSize="100">
                                                                                        <PageSizeItemSettings Items="10, 50, 100, 200, 500, 1000" Visible="True">
                                                                                        </PageSizeItemSettings>
                                                                                    </SettingsPager>
                                                                                    <SettingsEditing Mode="PopupEditForm">
                                                                                    </SettingsEditing>
                                                                                    <Settings ShowFooter="True" ShowHeaderFilterButton="True" ShowFilterRow="True" />
                                                                                    <SettingsBehavior AllowFocusedRow="True" />
                                                                                    <SettingsPopup>
                                                                                        <EditForm Modal="True" />
                                                                                        <CustomizationWindow HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" />
                                                                                    </SettingsPopup>
                                                                                    <SettingsText CommandNew="Nuevo" />
                                                                                    <Columns>
                                                                                        <dx:GridViewCommandColumn ShowInCustomizationForm="True" Visible="False" VisibleIndex="21" ShowClearFilterButton="True">
                                                                                        </dx:GridViewCommandColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="NIT" FieldName="NIT" ShowInCustomizationForm="True" VisibleIndex="5">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="RAZON SOCIAL" FieldName="RAZON_SOCIAL" ShowInCustomizationForm="True" VisibleIndex="6">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="CODIGO CONTROL" FieldName="CODIGO_CONTROL" ShowInCustomizationForm="True" ToolTip="Código de Control" VisibleIndex="9" Visible="False">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="ESTADO" FieldName="ESTADO" ShowInCustomizationForm="True" VisibleIndex="10">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="ID_PRODUCTO" FieldName="ID_PRODUCTO" ShowInCustomizationForm="True" ToolTip="Importe Propio" VisibleIndex="11" Visible="False">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="FECHA_MOVIMIENTO" FieldName="FECHA_MOVIMIENTO" ShowInCustomizationForm="True" VisibleIndex="2" ReadOnly="True" Visible="False">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="PRECIO" FieldName="PRECIO" ShowInCustomizationForm="True" VisibleIndex="14">
                                                                                            <Settings AutoFilterCondition="Equals" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="NRO FACTURA" FieldName="NRO_FACTURA" ShowInCustomizationForm="True" ToolTip="Número de Factura" VisibleIndex="8">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                            <CellStyle HorizontalAlign="Right">
                                                                                            </CellStyle>
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="(NRO/COD) AUTORIZACION" FieldName="NRO_AUTORIZACION" ShowInCustomizationForm="True" ToolTip="Producto" VisibleIndex="7" ReadOnly="True">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="FECHA" FieldName="FECHA_FACTURA" ShowInCustomizationForm="True" VisibleIndex="1">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="PRODUCTO" FieldName="PRODUCTO" ShowInCustomizationForm="True" VisibleIndex="12">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="CANTIDAD" FieldName="CANTIDAD" ShowInCustomizationForm="True" VisibleIndex="13">
                                                                                            <Settings AutoFilterCondition="Equals" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="ID_TIPO_EMISOR" FieldName="ID_TIPO_EMISOR" ShowInCustomizationForm="True" Visible="False" VisibleIndex="19">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="ID" FieldName="ID_MOVIMIENTO" ShowInCustomizationForm="True" VisibleIndex="0">
                                                                                            <Settings AutoFilterCondition="Equals" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="ID_PROYECTO" FieldName="ID_PROYECTO" ShowInCustomizationForm="True" Visible="False" VisibleIndex="18">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="ID_ACTIVIDAD" FieldName="ID_ACTIVIDAD" ShowInCustomizationForm="True" Visible="False" VisibleIndex="17">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="PROYECTO" FieldName="PROYECTO" ShowInCustomizationForm="True" VisibleIndex="4">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="ACTIVIDAD" FieldName="ACTIVIDAD" ShowInCustomizationForm="True" VisibleIndex="3">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="IMPORTE_CONTABLE" FieldName="IMPORTE" ShowInCustomizationForm="True" VisibleIndex="16" Visible="False">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="IMPORTE" FieldName="IMPORTE_DOC" ShowInCustomizationForm="True" VisibleIndex="15">
                                                                                            <Settings AutoFilterCondition="Equals" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                    </Columns>
                                                                                    <TotalSummary>
                                                                                        <dx:ASPxSummaryItem FieldName="IMPORTE" ShowInColumn="IMPORTE" SummaryType="Sum" Tag="imp" />
                                                                                    </TotalSummary>
                                                                                </dx:ASPxGridView>
                                                                            </td>
                                                                        </tr>
                                        
                                                                        <tr>
                                                                            <td colspan="3">
                                                
                                                                                <dx:ASPxGridViewExporter ID="dgeExportar" runat="server">
                                                                                </dx:ASPxGridViewExporter>
                                                
                                                                            </td>
                                                                        </tr>

                                                                    </table>
                                                                </dx:ContentControl>
                                                            </ContentCollection>
                                                        </dx:TabPage>
                                                        <dx:TabPage Text="   GRAFICO ESTADISTICO">
                                                            <TabImage IconID="chart_stackedbar_16x16">
                                                            </TabImage>
                                                            <ContentCollection>
                                                                <dx:ContentControl runat="server">
                                                                    
                                                                    <dx:ASPxCallbackPanel ID="pnlGrafico" runat="server" Width="100%" ClientInstanceName="pnlGrafico" OnCallback="pnlGrafico_Callback">
                                                                 
                                                                        <ClientSideEvents EndCallback="function(s, e) {
                                            grafficardatos(s, e);
                                            }" />
                                                                        <Paddings PaddingTop="10px" />
                                                                        <PanelCollection>
                                                                            <dx:PanelContent runat="server">
                                                                                <table style="width: 100%">
                                                                                    <tr>
                                                                                        <td style="width: 70%">
                                                                                            <dx:ASPxLabel ID="ASPxLabel45" runat="server" Width="100%">
                                                                                                <BorderBottom BorderColor="#FF6600" BorderStyle="Solid" />
                                                                                            </dx:ASPxLabel>
                                                                                        </td>
                                                                                        <td style="width: 30%">
                                                                                            <dx:ASPxButton ID="btnGraficar" runat="server" AutoPostBack="False" EnableTheming="True" Font-Bold="True" ForeColor="Maroon" Text="Generar Gráfico" Theme="Metropolis" Width="100%">
                                                                                                <ClientSideEvents Click="function(s, e) {
pnlGrafico.PerformCallback();   
}" />
                                                                                                <Image IconID="chart_fullstackedline_16x16">
                                                                                                </Image>
                                                                                            </dx:ASPxButton>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td colspan="2">
                                                                                            <div id="container" aria-expanded="true" style="min-width: 310px; height: 450px; margin: 0 auto">
                                                                                            </div>
                                                                                            <script type="text/javascript">

                                                                                                function grafficardatos(s, e) {

                                                                                                    var js_title = s.cp_title;
                                                                                                    var js_subtitle = s.cp_subtitle;
                                                                                                    var js_xAxis = s.cp_xAxis;

                                                                                                    var js_colDatA = s.cp_colDatA;
                                                                                                    var js_colDatF = s.cp_colDatF;
                                                                                                    var js_colDatV = s.cp_colDatV;

                                                                                                    var js_pieTotA = s.cp_pieTotA;
                                                                                                    var js_pieTotF = s.cp_pieTotF;
                                                                                                    var js_pieTotV = s.cp_pieTotV;

                                                                                                    Highcharts.chart('container', {
                                                                                                        title: {
                                                                                                            text: js_title
                                                                                                        },
                                                                                                        subtitle: {
                                                                                                            text: js_subtitle
                                                                                                        },
                                                                                                        xAxis: {
                                                                                                            categories: js_xAxis
                                                                                                        },
                                                                                                        series: [{
                                                                                                            type: 'column',
                                                                                                            name: 'VALIDO',
                                                                                                            color: "#71C470",
                                                                                                            data: js_colDatV
                                                                                                        }, {
                                                                                                            type: 'column',
                                                                                                            name: 'ANULADO',
                                                                                                            color: "#DEB150",
                                                                                                            data: js_colDatA
                                                                                                        }, {
                                                                                                            type: 'column',
                                                                                                            name: 'FALTANTE',
                                                                                                            color: "#933131",
                                                                                                            data: js_colDatF
                                                                                                        },
                                                                                                        {
                                                                                                            type: 'pie',
                                                                                                            name: 'Total consumption',
                                                                                                            data: [{
                                                                                                                name: 'VALIDO',
                                                                                                                y: js_pieTotV,
                                                                                                                color: "#71C470"
                                                                                                            }, {
                                                                                                                name: 'ANULADO',
                                                                                                                y: js_pieTotA,
                                                                                                                color: "#DEB150"
                                                                                                            }, {
                                                                                                                name: 'FALTANTE',
                                                                                                                y: js_pieTotF,
                                                                                                                color: "#933131"
                                                                                                            }],
                                                                                                            center: [120, 0],
                                                                                                            size: 90,
                                                                                                            showInLegend: false,
                                                                                                            dataLabels: {
                                                                                                                enabled: true
                                                                                                            }
                                                                                                        }
                                                                                                        ]
                                                                                                    });
                                                                                                }
                                                                                            </script>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>

                                                                            </dx:PanelContent>
                                                                        </PanelCollection>
                                                                    </dx:ASPxCallbackPanel>

                                                                </dx:ContentControl>
                                                            </ContentCollection>
                                                        </dx:TabPage>
                                                    </TabPages>
                                                    <TabStyle Font-Names="Verdana" Font-Size="Smaller">
                                                    </TabStyle>
                                                </dx:ASPxPageControl>
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





























































