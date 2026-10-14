<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtrRepDosifGral.ascx.cs" Inherits="AnhHydroOctanoPresentacion.Controles.CtrRepDosifGral" %>
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
                            <dx:ASPxLabel runat="server" ForeColor="#367B2F" ID="lblTitulo" Font-Bold="True" Font-Names="Rockwell" Font-Size="Larger" Text="REPORTE - CONTROL DE CORRELATIVIDAD DE FACTURAS EMITIDAS"></dx:ASPxLabel>
                    </dx:PanelContent>
                </PanelCollection>
                <Border BorderStyle="Ridge" BorderWidth="1px" BorderColor="#003300"></Border>
                </dx:ASPxRoundPanel>
            </td>
        </tr>
    </table>

    <dx:ASPxSplitter ID="ASPxSplitter1" runat="server" Height="100%">
        <Panes>
            <dx:SplitterPane Size="43%" AllowResize="True" AutoHeight="True">
                <ContentCollection>
                    <dx:SplitterContentControl runat="server">
                        <table style="width: 100%;">
                            <tr>
                                <td colspan="3" style="padding-bottom: 10px">
                                    <dx:ASPxLabel ID="ASPxLabel60" runat="server" ForeColor="#487B5C" Text="CARACTERISTICAS" Font-Bold="True" Width="100%" Font-Names="Rockwell">
                                        <BorderBottom BorderStyle="Solid" />
                                    </dx:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 25%">
                                    Fecha Inicio :<dx:ASPxDateEdit ID="detFechaIni" runat="server" ClientInstanceName="detFechaIni" EnableTheming="True" Height="23px" Theme="Metropolis" ToolTip="Seleccione la fecha inicial" Width="100%">
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
                                <td style="width: 25%">
                                    Fecha Final :<dx:ASPxDateEdit ID="detFechaFin" runat="server" ClientInstanceName="detFechaFin" Theme="Metropolis" ToolTip="Seleccione la fecha final" Width="100%">
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
                                <td style="width: 50%">
                                    Selección Directa de Mes :
                                    <uc1:CtrFecheroMes ID="cfmMes" runat="server" />
                                    
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="padding-bottom: 5px">Operador :<%--Actividad :--%><dx:ASPxComboBox ID="cmbOperador" runat="server" ClientInstanceName="cmbOperador" TextField="DENOMINACION" TextFormatString="{0};{2}" Theme="Metropolis" ValueField="ID_CONSUMIDOR" Width="82%">
                                            <ClientSideEvents SelectedIndexChanged="function(s, e) { }" />
                                            <Columns>
                                                <dx:ListBoxColumn Caption="DENOMINACION" FieldName="DENOMINACION" Name="DENOMINACION" />
                                                <dx:ListBoxColumn Caption="ID_CONSUMIDOR" FieldName="ID_CONSUMIDOR" Name="ID_CONSUMIDOR" Width="0px" />
                                                <dx:ListBoxColumn Caption="ACTIVIDAD" FieldName="ACTIVIDAD" Name="ACTIVIDAD" />
                                                <dx:ListBoxColumn Caption="ID_ACTIVIDAD" FieldName="ID_ACTIVIDAD" Name="ID_ACTIVIDAD" Width="0px" />
                                            </Columns>
                                            <ClearButton Visibility="Auto">
                                            </ClearButton>
                                            <ValidationSettings>
                                                <RequiredField ErrorText="ERROR: Seleccione al operador." IsRequired="True" />
                                            </ValidationSettings>
                                            <ReadOnlyStyle BackColor="#DEE8DB">
                                            </ReadOnlyStyle>
                                            <FocusedStyle BackColor="#FFFFCC">
                                            </FocusedStyle>
                                            <DisabledStyle BackColor="#DEE8DB">
                                            </DisabledStyle>
                                        </dx:ASPxComboBox>
                                   <%-- <dx:ASPxComboBox ID="cmbActividad" runat="server" SelectedIndex="0" Theme="Metropolis" Width="100%">
                                    <Items>
                                        <dx:ListEditItem Selected="True" Text="ESTACIONES DE SERVICIO (LIQ, GNV)" Value="1,2" />
                                        <dx:ListEditItem Text="PLANTAS DE AEROPUERTO" Value="50" />
                                        <dx:ListEditItem Text="TERMINALES DE ALMACENAJE" Value="5" />
                                    </Items>
                                    <ClearButton Visibility="Auto">
                                    </ClearButton>
                                    </dx:ASPxComboBox>--%>
                                </td>
                                <td></td>
                            </tr>

                            <tr>
                                <td colspan="2">
                                    <dx:ASPxLabel ID="ASPxLabel62" runat="server" Width="100%">
                                        <BorderBottom BorderColor="#487B5C" BorderStyle="Solid" />
                                    </dx:ASPxLabel>
                                </td>
                                <td style="padding-top: 5px;">
                                    <dx:ASPxButton ID="btnActualiza" runat="server" AutoPostBack="False" Font-Bold="True" ForeColor="Maroon" Text="Obtener Información" Theme="Metropolis" Width="100%">
                                        <ClientSideEvents Click="function(s, e) {
pnlDatos.PerformCallback();
pnlLateral.PerformCallback();
}" />
                                        <Image IconID="actions_download_16x16">
                                        </Image>
                                    </dx:ASPxButton>
                                </td>
                            </tr>

                            <tr>
                                <td colspan="3" style="padding-top: 5px">
                                    <dx:ASPxCallbackPanel ID="pnlLateral" runat="server" ClientInstanceName="pnlLateral" Width="100%" OnCallback="pnlLateral_Callback">
                                        <ClientSideEvents BeginCallback="function(s, e) {
	LoadingPanel.Show();
}" EndCallback="function(s, e) {
	LoadingPanel.Hide();
}" />
                                        <PanelCollection>
                                            <dx:PanelContent runat="server">
                                                
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxButton ID="btnExportar" runat="server" AutoPostBack="False" Font-Bold="True" ForeColor="Maroon" OnClick="btnExportar_Click" Text="Exportar Registros" Theme="Metropolis" Width="30%">
                                                                <Image IconID="export_exporttoxlsx_16x16">
                                                                </Image>
                                                            </dx:ASPxButton>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 100%">
                                                            
                                                            <dx:ASPxGridView ID="dgvListado" runat="server" AutoGenerateColumns="False" ClientInstanceName="dgvCabecera" EnableTheming="True" Font-Size="Smaller" KeyFieldName="NRO_AUTORIZACION" Theme="Metropolis" Width="100%" OnHtmlRowPrepared="dgvListado_HtmlRowPrepared">
                                                                <ClientSideEvents CustomButtonClick="function(s, e) {
s.GetRowValues(e.visibleIndex, 'NRO_AUTORIZACION', function (value) {
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
                                                                <SettingsPager Mode="ShowAllRecords" NumericButtonCount="1" PageSize="0" Visible="False">
                                                                    <PageSizeItemSettings Items="50, 100, 200, 500, 1000" Visible="True">
                                                                    </PageSizeItemSettings>
                                                                </SettingsPager>
                                                                <Settings ShowFilterRow="True" ShowFooter="True" ShowHeaderFilterButton="True" />
                                                                <SettingsBehavior AllowFocusedRow="True" />
                                                                <SettingsPopup>
                                                                    <EditForm Modal="True" />
                                                                    <CustomizationWindow HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" />
                                                                </SettingsPopup>
                                                                <Columns>
                                                                    <dx:GridViewCommandColumn ShowClearFilterButton="True" ShowInCustomizationForm="True" VisibleIndex="9">
                                                                        <CustomButtons>
                                                                            <dx:GridViewCommandColumnCustomButton ID="btnDetalle" Text=" ">
                                                                                <Image IconID="pdfviewer_marqueezoom_16x16" ToolTip="Ver Detalle">
                                                                                </Image>
                                                                            </dx:GridViewCommandColumnCustomButton>
                                                                        </CustomButtons>
                                                                    </dx:GridViewCommandColumn>
                                                                    <dx:GridViewDataTextColumn Caption="(NRO/COD) AUTORIZACION" FieldName="NRO_AUTORIZACION" ShowInCustomizationForm="True" VisibleIndex="0" ToolTip="Númeo de Autorización">
                                                                        <Settings AllowAutoFilter="True" AllowHeaderFilter="True" AutoFilterCondition="Contains" />
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="F.MIN." FieldName="NO_MIN" ShowInCustomizationForm="True" VisibleIndex="1" ToolTip="Número Factura Mínimo">
                                                                        <Settings AutoFilterCondition="Contains" />
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="F.MAX." FieldName="NO_MAX" ShowInCustomizationForm="True" VisibleIndex="2" ToolTip="Número Factura Máximo">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="N.REG.EXIS." FieldName="NO_REG_REAL" ShowInCustomizationForm="True" VisibleIndex="3" ToolTip="Número Registros Existentes">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="N.REG.CALC." FieldName="NO_REG_CALC" ShowInCustomizationForm="True" VisibleIndex="4" ToolTip="Número Registros Calculado">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="DIF." FieldName="DIFERENCIA" ShowInCustomizationForm="True" VisibleIndex="5" ToolTip="Diferencia">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="EST." FieldName="ESTADO" ShowInCustomizationForm="True" VisibleIndex="6" ToolTip="Estado">
                                                                        <Settings AutoFilterCondition="Contains" />
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="DIF.VAL." FieldName="DIF_VAL" ShowInCustomizationForm="True" VisibleIndex="7" ToolTip="Diferencia Válida">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="%" FieldName="PORCENTAJE" ShowInCustomizationForm="True" ToolTip="Porcentaje" VisibleIndex="8">
                                                                    </dx:GridViewDataTextColumn>
                                                                </Columns>
                                                                <TotalSummary>
                                                                    <dx:ASPxSummaryItem DisplayFormat="{0}" FieldName="DIF_VAL" ShowInColumn="DIF.VAL." SummaryType="Sum" />
                                                                </TotalSummary>
                                                            </dx:ASPxGridView>

                                                        </td>
                                                    </tr>
                                                </table>

                                            </dx:PanelContent>
                                        </PanelCollection>
                                    </dx:ASPxCallbackPanel>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="padding-top: 5px">&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="2" style="padding-top: 5px">
                                    
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="padding-top: 5px">&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="2" style="padding-top: 5px">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </dx:SplitterContentControl>
                </ContentCollection>
            </dx:SplitterPane>
            <dx:SplitterPane ScrollBars="Horizontal">
                <ContentCollection>
                    <dx:SplitterContentControl runat="server">

                        <dx:ASPxCallbackPanel ID="pnlDatos" runat="server" Theme="Youthful" Width="100%" ClientInstanceName="pnlDatos" OnCallback="pnlDatos_Callback">
                            <PanelCollection>
                                <dx:PanelContent runat="server">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td colspan="3" style="text-align: center;">
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
                                            <td colspan="3">
                                                <legend>
                                                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Rockwell" Font-Size="Medium" ForeColor="#367B2F" Text="DETALLE DE LA DOSIFICACION"></asp:Label>
                                                </legend>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                
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
                                                                                            <dx:ASPxButton ID="btnGraficar" runat="server" AutoPostBack="False" EnableTheming="True" Font-Bold="True" ForeColor="Maroon" Text="Actualizar Gráfico" Theme="Metropolis" Width="100%">
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

                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 40%">
                                                <dx:ASPxLabel ID="ASPxLabel61" runat="server" Width="100%">
                                                    <BorderBottom BorderColor="#FF6600" BorderStyle="Solid" />
                                                </dx:ASPxLabel>
                                            </td>
                                            <td style="width: 30%">
                                                <dx:ASPxComboBox ID="cmbTipoExport" runat="server" ClientInstanceName="cmbTipoExport" Height="27px" SelectedIndex="0" Theme="Metropolis" ToolTip="Seleccione el Formato del Reporte" ValueType="System.Int32" Width="100%">
                                                    <Items>
                                                        <dx:ListEditItem Selected="True" Text="EXCEL" Value="1" />
                                                        <dx:ListEditItem Text="PDF" Value="2" />
                                                    </Items>
                                                    <ClearButton Visibility="Auto">
                                                    </ClearButton>
                                                </dx:ASPxComboBox>
                                            </td>
                                            <td style="width: 30%">
                                                <dx:ASPxButton ID="btnEmiteReporte" runat="server" AutoPostBack="False" Font-Bold="True" ForeColor="Maroon" OnClick="btnEmiteReporte_Click" Text="Emitir Reporte" Theme="Metropolis" Width="100%">
                                                    <Image IconID="print_printer_16x16">
                                                    </Image>
                                                </dx:ASPxButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <dx:ASPxGridView ID="dgvDetalle" runat="server" AutoGenerateColumns="False" ClientInstanceName="dgvDetalle" EnableTheming="True" Font-Size="Smaller" KeyFieldName="ID_MOVIMIENTO" OnCustomCallback="dgvDetalle_CustomCallback" OnHtmlRowPrepared="dgvDetalle_HtmlRowPrepared" Theme="Default" Width="100%">
                                                    <ClientSideEvents BeginCallback="function(s, e) {
	                                LoadingPanel.Show();
                                }" EndCallback="function(s, e) {
                                pnlGrafico.PerformCallback();
                                LoadingPanel.Hide();
                                }" />
                                                    <SettingsContextMenu EnableColumnMenu="True" Enabled="True">
                                                    </SettingsContextMenu>
                                                    <Templates>
                                                        <EditForm>
                                                        </EditForm>
                                                    </Templates>
                                                    <SettingsPager NumericButtonCount="1">
                                                        <PageSizeItemSettings Items="10, 50, 100, 200, 500, 1000" Visible="True">
                                                        </PageSizeItemSettings>
                                                    </SettingsPager>
                                                    <SettingsEditing Mode="PopupEditForm">
                                                    </SettingsEditing>
                                                    <Settings ShowFilterRow="True" ShowFooter="True" ShowHeaderFilterButton="True" />
                                                    <SettingsBehavior AllowFocusedRow="True" EnableCustomizationWindow="True" />
                                                    <SettingsPopup>
                                                        <EditForm Modal="True" />
                                                        <CustomizationWindow HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" />
                                                    </SettingsPopup>
                                                    <SettingsText CommandNew="Nuevo" />
                                                    <Columns>
                                                        <dx:GridViewCommandColumn ShowClearFilterButton="True" ShowInCustomizationForm="True" Visible="False" VisibleIndex="21">
                                                        </dx:GridViewCommandColumn>
                                                        <dx:GridViewDataTextColumn Caption="NIT" FieldName="NIT" ShowInCustomizationForm="True" VisibleIndex="5">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="RAZON SOCIAL" FieldName="RAZON_SOCIAL" ShowInCustomizationForm="True" VisibleIndex="6">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="CODIGO CONTROL" FieldName="CODIGO_CONTROL" ShowInCustomizationForm="True" ToolTip="Código de Control" VisibleIndex="9">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="EST." FieldName="ESTADO" ShowInCustomizationForm="True" VisibleIndex="10" ToolTip="Estado">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="ID_PRODUCTO" FieldName="ID_PRODUCTO" ShowInCustomizationForm="True" ToolTip="Importe Propio" Visible="False" VisibleIndex="11">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="FECHA_MOVIMIENTO" FieldName="FECHA_MOVIMIENTO" ReadOnly="True" ShowInCustomizationForm="True" Visible="False" VisibleIndex="2">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="PRECIO" FieldName="PRECIO" ShowInCustomizationForm="True" VisibleIndex="14">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="NRO FACTURA" FieldName="NRO_FACTURA" ShowInCustomizationForm="True" ToolTip="Número de Factura" VisibleIndex="8">
                                                            <Settings AutoFilterCondition="Contains" />
                                                            <CellStyle HorizontalAlign="Right">
                                                            </CellStyle>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="NRO_AUTORIZACION" FieldName="NRO_AUTORIZACION" ReadOnly="True" ShowInCustomizationForm="True" ToolTip="Producto" Visible="False" VisibleIndex="7">
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
                                                        <dx:GridViewDataTextColumn Caption="ID" FieldName="ID_MOVIMIENTO" ShowInCustomizationForm="True" VisibleIndex="0" Visible="False">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="ID_PROYECTO" FieldName="ID_PROYECTO" ShowInCustomizationForm="True" Visible="False" VisibleIndex="18">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="ID_ACTIVIDAD" FieldName="ID_ACTIVIDAD" ShowInCustomizationForm="True" Visible="False" VisibleIndex="17">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="PROYECTO" FieldName="PROYECTO" ShowInCustomizationForm="True" VisibleIndex="4">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="ACTIVIDAD" FieldName="ACTIVIDAD" ShowInCustomizationForm="True" VisibleIndex="3" Visible="False">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="IMPORTE_CONTABLE" FieldName="IMPORTE" ShowInCustomizationForm="True" Visible="False" VisibleIndex="16">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="IMPORTE" FieldName="IMPORTE_DOC" ShowInCustomizationForm="True" VisibleIndex="15">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                    <TotalSummary>
                                                        <dx:ASPxSummaryItem DisplayFormat="{0}" FieldName="IMPORTE" ShowInColumn="IMPORTE" SummaryType="Sum" Tag="imp" />
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
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxCallbackPanel>
                        
                        
                        


                    </dx:SplitterContentControl>
                </ContentCollection>
            </dx:SplitterPane>
        </Panes>
    </dx:ASPxSplitter>
</div>





























































