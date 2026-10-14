<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtrLogErrores.ascx.cs" Inherits="AnhHydroOctanoPresentacion.Controles.CtrLogErrores" %>
<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>
<%@ Register TagPrefix="dx" Namespace="DevExpress.Web.ASPxPivotGrid" Assembly="DevExpress.Web.ASPxPivotGrid.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>

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
    
    .dxflInternalEditorTable_Youthful {
    width: 100%;
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
.dxgvCommandColumn
{
	padding: 2px;
    white-space: nowrap;
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
                            <dx:ASPxLabel runat="server" ForeColor="#367B2F" ID="lblTitulo" Font-Bold="True" Font-Names="Rockwell" Font-Size="Larger" Text="GESTION DE ERRORES"></dx:ASPxLabel>
                    </dx:PanelContent>
                </PanelCollection>
                <Border BorderStyle="Ridge" BorderWidth="1px" BorderColor="#003300"></Border>
                </dx:ASPxRoundPanel>
            </td>
        </tr>
    </table>

    <dx:ASPxSplitter ID="ASPxSplitter1" runat="server" Height="100%">
        <Panes>
            <dx:SplitterPane Size="28%" AllowResize="True" AutoHeight="True">
                <ContentCollection>
                    <dx:SplitterContentControl runat="server">
                        
                        <dx:ASPxCallbackPanel ID="pnlLateral" runat="server" Width="200px" ClientInstanceName="pnlLateral" OnCallback="pnlLateral_Callback">
                            <PanelCollection>
                                <dx:PanelContent runat="server">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td colspan="2" style="padding-bottom: 5px">
                                                <dx:ASPxLabel ID="ASPxLabel60" runat="server" Font-Bold="True" Font-Names="Rockwell" ForeColor="#487B5C" Text="CARACTERISTICAS" Width="100%">
                                                    <BorderBottom BorderStyle="Solid" />
                                                </dx:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">Tabla :
                                                <dx:ASPxRadioButtonList ID="rbtTabla" runat="server" ClientInstanceName="rbtTabla" SelectedIndex="0" Theme="Metropolis" ValueType="System.Int32" Width="100%">
                                                    <Items>
                                                        <dx:ListEditItem Selected="True" Text="LOG_ERRORES_ORA" Value="1" />
                                                        <dx:ListEditItem Text="SINCONIZACIONES" Value="2" />
                                                    </Items>
                                                </dx:ASPxRadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 60%">
                                                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="No. Registros" Width="100%" Font-Bold="True" ForeColor="Gray">
                                                    <BorderBottom BorderColor="Gray" BorderStyle="Solid" />
                                                </dx:ASPxLabel>
                                            </td>
                                            <td style="width: 40%">
                                                <dx:ASPxSpinEdit ID="spnNoRegistros" runat="server" Font-Bold="True" MaxValue="10000" Number="100" Theme="Metropolis" Width="100%">
                                                    <ClearButton Visibility="Auto">
                                                    </ClearButton>
                                                </dx:ASPxSpinEdit>
                                            </td>
                                        </tr>
                                        <tr style="padding-bottom: 5px">
                                            <td colspan="2">
                                    
                                                <dx:ASPxButton ID="btnActualiza" runat="server" Text="Actualizar Listado" Theme="Metropolis" Width="100%" AutoPostBack="False" Font-Bold="True" ForeColor="Maroon">
                                                    <ClientSideEvents Click="function(s, e) {
pnlLateral.PerformCallback();
pnlDatos.PerformCallback();
}" />
                                                    <Image IconID="actions_reset2_16x16">
                                                    </Image>
                                                </dx:ASPxButton>
                                    
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <dx:ASPxGridView ID="dgvListado" runat="server" AutoGenerateColumns="False" ClientInstanceName="dgvCabecera" EnableTheming="True" Font-Size="Smaller" KeyFieldName="CODIGO_ORACLE" Theme="Metropolis" Width="100%" OnHtmlRowPrepared="dgvListado_HtmlRowPrepared">
                                                    <ClientSideEvents FocusedRowChanged="function(s, e) {
pnlDatos.PerformCallback();
}" />
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
                                                    <Settings ShowFilterRow="True" ShowFooter="True" ShowHeaderFilterButton="True" ShowGroupPanel="True" />
                                                    <SettingsBehavior AllowFocusedRow="True" />
                                                    <SettingsPopup>
                                                        <EditForm Modal="True" />
                                                        <CustomizationWindow HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" />
                                                    </SettingsPopup>
                                                    <Columns>
                                                        <dx:GridViewCommandColumn ShowClearFilterButton="True" ShowInCustomizationForm="True" Visible="False" VisibleIndex="0">
                                                        </dx:GridViewCommandColumn>
                                                        <dx:GridViewDataTextColumn Caption="ID" FieldName="CODIGO_ORACLE" ShowInCustomizationForm="True" VisibleIndex="1">
                                                            <Settings AllowAutoFilter="True" AllowHeaderFilter="True" AutoFilterCondition="Equals" HeaderFilterMode="CheckedList" />
                                                            <CellStyle HorizontalAlign="Right">
                                                            </CellStyle>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="DESCRIPCION" FieldName="MENSAJE_ORACLE" ShowInCustomizationForm="True" VisibleIndex="2">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="CANT." FieldName="CANTIDAD" ShowInCustomizationForm="True" VisibleIndex="3" ToolTip="Cantidad">
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                    <TotalSummary>
                                                        <dx:ASPxSummaryItem FieldName="CANTIDAD" ShowInColumn="CANT." SummaryType="Sum" DisplayFormat="{0}" />
                                                    </TotalSummary>
                                                </dx:ASPxGridView>
                                            </td>
                                        </tr>
                                    </table>
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxCallbackPanel>

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
                                            <td colspan="7"><legend>
                                                <asp:Label ID="lblSubTitulo" runat="server" Font-Bold="True" Font-Names="Rockwell" Font-Size="Medium" ForeColor="#367B2F" Text="DATOS GENERALES"></asp:Label>
                                                </legend></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="#10420F" Text="CODIGO DE ERROR"></asp:Label>
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                            <td></td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td style="text-align: right;">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <asp:Label ID="lblCodigo" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td colspan="2" style="text-align: right">
                                                <dx:ASPxButton ID="btnEliminar" runat="server" Font-Bold="True" ForeColor="Maroon" Text="Eliminar Registros" Theme="Metropolis" Width="200px" OnClick="btnEliminar_Click">
                                                    <ClientSideEvents Click="function(s, e) {

//if(rbtTabla.GetValue()==2)
//{
//alert('ATENCION: La tabla seleccionada no aplica para la función.');
//e.processOnServer = false; 
//}
//else
//{
if(confirm('Se eliminaran todos los registros del código de error seleccinado, una vez realizada la acción no podra reveritir el cambio. ¿Desea continuar?'))
{
LoadingPanel.Show();
e.processOnServer = true; 
}
else{
e.processOnServer = false; 
}
//}
}
" />
                                                    <Image IconID="spreadsheet_deletesheetrows_16x16">
                                                    </Image>
                                                </dx:ASPxButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="7">
                                                <dx:ASPxGridView ID="dgvCabecera" runat="server" AutoGenerateColumns="False" ClientInstanceName="dgvCabecera" EnableTheming="True" Font-Size="Smaller" KeyFieldName="ID_ERROR" Theme="Default" Width="100%" OnCustomButtonCallback="dgvCabecera_CustomButtonCallback">
                                                    <ClientSideEvents CustomButtonClick="function(s, e) {
	
    switch(e.buttonID) {

    case  'btnGrdRegistrar':
    pucRegistro.PerformCallback();
    pucRegistro.Show();
    break;

    case  'btnGrdEliminar':
    if(confirm('¿Desea eliminar el registro? - Una vez realizada la acción no podrá revertir el proceso. ¿Desea continuar?'))
    {
    e.processOnServer = true;
    }
    else{
    e.processOnServer = false; 
    }
    break;


    } 

    }" />
                                                    <SettingsContextMenu EnableColumnMenu="True" Enabled="True">
                                                    </SettingsContextMenu>
                                                    <Templates>
                                                        <EditForm>
                                                        </EditForm>
                                                    </Templates>
                                                    <SettingsPager PageSize="50">
                                                        <PageSizeItemSettings Items="10, 50, 100, 200, 500, 1000" Visible="True">
                                                        </PageSizeItemSettings>
                                                    </SettingsPager>
                                                    <SettingsEditing Mode="EditForm">
                                                    </SettingsEditing>
                                                    <Settings ShowHeaderFilterButton="True" ShowFilterRow="True" ShowGroupPanel="True" ShowFooter="True" ShowGroupFooter="VisibleIfExpanded" ShowFilterBar="Visible" ShowFilterRowMenu="True" />
                                                    <SettingsBehavior AllowFocusedRow="True" EnableCustomizationWindow="True" />
                                                    <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
                                                    <SettingsPopup>
                                                        <EditForm Modal="True" />
                                                        <CustomizationWindow HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" />
                                                    </SettingsPopup>
                                                    <SettingsSearchPanel Visible="True" />
                                                    <SettingsText CommandNew="Nuevo" />
                                                    <Columns>
                                                        <dx:GridViewCommandColumn ShowInCustomizationForm="True" VisibleIndex="7" ShowClearFilterButton="True" ToolTip="Funciones">
                                                            <CustomButtons>
                                                                <dx:GridViewCommandColumnCustomButton ID="btnGrdRegistrar" Text=" ">
                                                                    <Image IconID="data_addnewdatasource_16x16" ToolTip="Registro Directo">
                                                                    </Image>
                                                                </dx:GridViewCommandColumnCustomButton>
                                                                <dx:GridViewCommandColumnCustomButton ID="btnGrdEliminar" Text=" ">
                                                                    <Image IconID="data_deletedatasource_16x16" ToolTip="Eliminar Registro">
                                                                    </Image>
                                                                </dx:GridViewCommandColumnCustomButton>
                                                            </CustomButtons>
                                                        </dx:GridViewCommandColumn>
                                                        <dx:GridViewDataTextColumn Caption="AUD_FECHA" FieldName="AUD_FECHA" ShowInCustomizationForm="True" VisibleIndex="1">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="AUD_USUARIO" FieldName="AUD_USUARIO" ShowInCustomizationForm="True" VisibleIndex="2">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="APLICACION" FieldName="APLICACION" ShowInCustomizationForm="True" VisibleIndex="3">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="PROCEDIMIENTO" FieldName="PROCEDIMIENTO" ShowInCustomizationForm="True" VisibleIndex="4">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="MENSAJE_PERSONALIZADO" FieldName="MENSAJE_PERSONALIZADO" ShowInCustomizationForm="True" VisibleIndex="8">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="MENSAJE_ORACLE" FieldName="MENSAJE_ORACLE" ShowInCustomizationForm="True" VisibleIndex="9">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="ID" FieldName="ID_ERROR" ShowInCustomizationForm="True" VisibleIndex="0">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="ID OR-ERR" FieldName="ID_ORACLE_ERROR" ShowInCustomizationForm="True" VisibleIndex="5">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="ORACLE MENSAJE" FieldName="ID_ORACLE_MENSAJE" ShowInCustomizationForm="True" VisibleIndex="6">
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                    <Styles CssPostfix="DevGlp"><AlternatingRow BackColor="#EEEEEE">
                                                        </AlternatingRow></Styles>
                                                    <TotalSummary>
                                                        <dx:ASPxSummaryItem DisplayFormat="{0}" FieldName="TOTAL_CXC" ShowInColumn="DEUDA" SummaryType="Sum" />
                                                    </TotalSummary>
                                                    <GroupSummary>
                                                        <dx:ASPxSummaryItem DisplayFormat="{0}" FieldName="TOTAL_CXC" ShowInGroupFooterColumn="DEUDA" SummaryType="Sum" />
                                                    </GroupSummary>
                                                </dx:ASPxGridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dx:ASPxButton ID="btnEmitir" runat="server" Font-Bold="True" ForeColor="Maroon" OnClick="btnEmitir_Click" Text="Exportar Registros" Theme="Metropolis" Width="250px">
                                                    <Image IconID="export_exporttoxlsx_16x16">
                                                    </Image>
                                                </dx:ASPxButton>
                                            </td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>
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






    




























































<dx:ASPxPopupControl ID="pucRegistro" runat="server" AllowDragging="True" ClientInstanceName="pucRegistro" EnableTheming="True" HeaderText="Registro Directo" Height="93px" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Above" Theme="Metropolis" Width="650px" OnWindowCallback="pucRegistro_WindowCallback">
    <HeaderImage IconID="mail_attachment_32x32">
    </HeaderImage>
    <HeaderStyle>
    <BorderBottom BorderStyle="Solid" />
    </HeaderStyle>
    <ContentCollection>
        <dx:PopupControlContentControl runat="server">
            
            <dx:ASPxCallbackPanel ID="pnlRecepcion" runat="server" Width="100%">
                <PanelCollection>
                    <dx:PanelContent runat="server">
                        <table  style="width: 100%">
                            <tr>
                                <td colspan="3">
                                    <dx:ASPxTextBox ID="txtCadena" runat="server" AutoResizeWithContainer="True" Width="100%">
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                    <dx:ASPxButton ID="btnParse" runat="server" AutoPostBack="False" OnClick="btnParse_Click" Text="Extraer Cadena">
                                    </dx:ASPxButton>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 10%">
                                    <dx:ASPxLabel ID="ASPxLabel61" runat="server" Text="I_ID_INTEGRACION">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 40%">
                                    <dx:ASPxTextBox ID="txt01" runat="server" Width="170px">
                                    </dx:ASPxTextBox>
                                </td>
                                <td style="width: 10%">
                                    <dx:ASPxLabel ID="ASPxLabel94" runat="server" Text="I_ID_CONSUMIDOR">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 40%">
                                    <dx:ASPxTextBox ID="txt02" runat="server" Width="170px">
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 10%">
                                    <dx:ASPxLabel ID="ASPxLabel74" runat="server" Text="I_ID_TIPO_ACTIVIDAD">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 40%">
                                    <dx:ASPxTextBox ID="txt03" runat="server" Width="170px">
                                    </dx:ASPxTextBox>
                                </td>
                                <td style="width: 10%">
                                    <dx:ASPxLabel ID="ASPxLabel93" runat="server" Text="I_ID_TIPO_MOVIMIENTO">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 40%">
                                    <dx:ASPxTextBox ID="txt04" runat="server" Width="170px">
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 10%">
                                    <dx:ASPxLabel ID="ASPxLabel75" runat="server" Text="I_FECHA_OPERACION">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 40%">
                                    <dx:ASPxTextBox ID="txt05" runat="server" Width="170px">
                                    </dx:ASPxTextBox>
                                </td>
                                <td style="width: 10%">
                                    <dx:ASPxLabel ID="ASPxLabel92" runat="server" Text="I_IDENTIFICADOR">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 40%">
                                    <dx:ASPxTextBox ID="txt06" runat="server" Width="170px">
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 10%">
                                    <dx:ASPxLabel ID="ASPxLabel76" runat="server" Text="I_ID_PRODUCTO_HYDRO">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 40%">
                                    <dx:ASPxTextBox ID="txt07" runat="server" Width="170px">
                                    </dx:ASPxTextBox>
                                </td>
                                <td style="width: 10%">
                                    <dx:ASPxLabel ID="ASPxLabel91" runat="server" Text="I_CANTIDAD">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 40%">
                                    <dx:ASPxTextBox ID="txt08" runat="server" Width="170px">
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 10%">
                                    <dx:ASPxLabel ID="ASPxLabel77" runat="server" Text="I_ID_TRANSACCION">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 40%">
                                    <dx:ASPxTextBox ID="txt09" runat="server" Width="170px">
                                    </dx:ASPxTextBox>
                                </td>
                                <td style="width: 10%">
                                    <dx:ASPxLabel ID="ASPxLabel90" runat="server" Text="I_BARCODE">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 40%">
                                    <dx:ASPxTextBox ID="txt10" runat="server" Width="170px">
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 10%">
                                    <dx:ASPxLabel ID="ASPxLabel78" runat="server" Text="I_ID_CODIGO_PAIS">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 40%">
                                    <dx:ASPxTextBox ID="txt11" runat="server" Width="170px">
                                    </dx:ASPxTextBox>
                                </td>
                                <td style="width: 10%">
                                    <dx:ASPxLabel ID="ASPxLabel89" runat="server" Text="I_FECHA_ENVIO">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 40%">
                                    <dx:ASPxTextBox ID="txt12" runat="server" Width="170px">
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 10%">
                                    <dx:ASPxLabel ID="ASPxLabel79" runat="server" Text="I_BINARIO_CONTROL">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 40%">
                                    <dx:ASPxTextBox ID="txt13" runat="server" Width="170px">
                                    </dx:ASPxTextBox>
                                </td>
                                <td style="width: 10%">
                                    <dx:ASPxLabel ID="ASPxLabel87" runat="server" Text="I_USUARIO">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 40%">
                                    <dx:ASPxTextBox ID="txt14" runat="server" Width="170px">
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 10%">
                                    <dx:ASPxLabel ID="ASPxLabel72" runat="server" Text="I_CI_USUARIO">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 40%">
                                    <dx:ASPxTextBox ID="txt15" runat="server" Width="170px">
                                    </dx:ASPxTextBox>
                                </td>
                                <td style="width: 10%">
                                    <dx:ASPxLabel ID="ASPxLabel88" runat="server" Text="I_COD_TANQUE">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 40%">
                                    <dx:ASPxTextBox ID="txt16" runat="server" Width="170px">
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 10%">
                                    <dx:ASPxLabel ID="ASPxLabel73" runat="server" Text="I_PRECINTOS">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 40%">
                                    <dx:ASPxTextBox ID="txt17" runat="server" Width="170px">
                                    </dx:ASPxTextBox>
                                </td>
                                <td style="width: 10%">
                                    <dx:ASPxLabel ID="ASPxLabel86" runat="server" Text="I_CONDUCTOR">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 40%">
                                    <dx:ASPxTextBox ID="txt18" runat="server" Width="170px">
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 10%">
                                    <dx:ASPxLabel ID="ASPxLabel69" runat="server" Text="I_CI_CONDUCTOR">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 40%">
                                    <dx:ASPxTextBox ID="txt19" runat="server" Width="170px">
                                    </dx:ASPxTextBox>
                                </td>
                                <td style="width: 10%">
                                    <dx:ASPxLabel ID="ASPxLabel85" runat="server" Text="I_FECHA_DESPACHO">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 40%">
                                    <dx:ASPxTextBox ID="txt20" runat="server" Width="170px">
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 10%">
                                    <dx:ASPxLabel ID="ASPxLabel70" runat="server" Text="I_NRO_DESPACHO">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 40%">
                                    <dx:ASPxTextBox ID="txt21" runat="server" Width="170px">
                                    </dx:ASPxTextBox>
                                </td>
                                <td style="width: 10%">
                                    <dx:ASPxLabel ID="ASPxLabel84" runat="server" Text="I_NRO_ORDEN">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 40%">
                                    <dx:ASPxTextBox ID="txt22" runat="server" Width="170px">
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 10%">
                                    <dx:ASPxLabel ID="ASPxLabel68" runat="server" Text="I_DESTINO">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 40%">
                                    <dx:ASPxTextBox ID="txt23" runat="server" Width="170px">
                                    </dx:ASPxTextBox>
                                </td>
                                <td style="width: 10%">
                                    <dx:ASPxLabel ID="ASPxLabel83" runat="server" Text="I_VOLUMEN_PLANTA">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 40%">
                                    <dx:ASPxTextBox ID="txt24" runat="server" Width="170px">
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 10%">
                                    <dx:ASPxLabel ID="ASPxLabel71" runat="server" Text="I_ID_TIPO_TRANSACCION">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 40%">
                                    <dx:ASPxTextBox ID="txt25" runat="server" Width="170px">
                                    </dx:ASPxTextBox>
                                </td>
                                <td style="width: 10%">
                                    <dx:ASPxLabel ID="ASPxLabel81" runat="server" Text="I_NIT_CONSUMIDOR">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 40%">
                                    <dx:ASPxTextBox ID="txt26" runat="server" Width="170px">
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 10%">
                                    <dx:ASPxLabel ID="ASPxLabel67" runat="server" Text="I_NIT_EMISOR">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 40%">
                                    <dx:ASPxTextBox ID="txt27" runat="server" Width="170px">
                                    </dx:ASPxTextBox>
                                </td>
                                <td style="width: 10%">
                                    <dx:ASPxLabel ID="ASPxLabel82" runat="server" Text="I_NRO_TRANSACCION">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 40%">
                                    <dx:ASPxTextBox ID="txt28" runat="server" Width="170px">
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 10%">
                                    <dx:ASPxLabel ID="ASPxLabel62" runat="server" Text="I_NRO_AUTORIZACION">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 40%">
                                    <dx:ASPxTextBox ID="txt29" runat="server" Width="170px">
                                    </dx:ASPxTextBox>
                                </td>
                                <td style="width: 10%">
                                    <dx:ASPxLabel ID="ASPxLabel80" runat="server" Text="I_CODIGO_CONTROL">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 40%">
                                    <dx:ASPxTextBox ID="txt30" runat="server" Width="170px">
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 10%">
                                    <dx:ASPxLabel ID="ASPxLabel65" runat="server" Text="I_RAZON_SOCIAL">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 40%">
                                    <dx:ASPxTextBox ID="txt31" runat="server" Width="170px">
                                    </dx:ASPxTextBox>
                                </td>
                                <td style="width: 10%">
                                    <dx:ASPxLabel ID="ASPxLabel66" runat="server" Text="I_PRECIO_UNITARIO">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 40%">
                                    <dx:ASPxTextBox ID="txt32" runat="server" Width="170px">
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 10%">
                                    <dx:ASPxLabel ID="ASPxLabel64" runat="server" Text="I_PRECIO_TOTAL">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 40%">
                                    <dx:ASPxTextBox ID="txt33" runat="server" Width="170px">
                                    </dx:ASPxTextBox>
                                </td>
                                <td style="width: 10%">
                                    <dx:ASPxLabel ID="ASPxLabel63" runat="server" Text="I_FECHA_TRANSACCION">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 40%">
                                    <dx:ASPxTextBox ID="txt34" runat="server" Width="170px">
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                            
                            <tr>
                                <td colspan="3">
                                    <dx:ASPxTextBox ID="txtResultado" runat="server" Width="170px">
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                    <dx:ASPxButton ID="btnRegistroDir" runat="server" OnClick="btnRegistroDir_Click" Text="Registrar" AutoPostBack="False">
                                    </dx:ASPxButton>
                                </td>
                            </tr>
                            
                        </table>
                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxCallbackPanel>
            
            
            <dx:ASPxCallbackPanel ID="pnlTranAdic" runat="server" Width="100%">
                <PanelCollection>
                    <dx:PanelContent runat="server">
                        <table  style="width: 100%">
                            <tr>
                                <td colspan="3">
                                    <dx:ASPxTextBox ID="txtCadenaTa" runat="server" AutoResizeWithContainer="True" Width="100%">
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                    <dx:ASPxButton ID="btnParseTa" runat="server" AutoPostBack="False" Text="Extraer Cadena" OnClick="btnParseTa_Click">
                                    </dx:ASPxButton>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 10%">
                                    <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="I_ID_MOVIMIENTO">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 40%">
                                    <dx:ASPxTextBox ID="txtTa01" runat="server" Width="170px">
                                    </dx:ASPxTextBox>
                                </td>
                                <td style="width: 10%">
                                    <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="I_ID_CONSUMIDOR">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 40%">
                                    <dx:ASPxTextBox ID="txtTa02" runat="server" Width="170px">
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 10%">
                                    <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="I_ID_INTEGRACION">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 40%">
                                    <dx:ASPxTextBox ID="txtTa03" runat="server" Width="170px">
                                    </dx:ASPxTextBox>
                                </td>
                                <td style="width: 10%">
                                    <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="I_ID_TIPO_TRANSACCION">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 40%">
                                    <dx:ASPxTextBox ID="txtTa04" runat="server" Width="170px">
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 10%">
                                    <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="I_NRO_TRANSACCION">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 40%">
                                    <dx:ASPxTextBox ID="txtTa05" runat="server" Width="170px">
                                    </dx:ASPxTextBox>
                                </td>
                                <td style="width: 10%">
                                    <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="I_NRO_AUTORIZACION">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 40%">
                                    <dx:ASPxTextBox ID="txtTa06" runat="server" Width="170px">
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 10%">
                                    <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="I_TIPO_CREDITO_FISCAL">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 40%">
                                    <dx:ASPxTextBox ID="txtTa07" runat="server" Width="170px">
                                    </dx:ASPxTextBox>
                                </td>
                                <td style="width: 10%">
                                    <dx:ASPxLabel ID="ASPxLabel9" runat="server" Text="I_PRECIO_UNITARIO">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 40%">
                                    <dx:ASPxTextBox ID="txtTa08" runat="server" Width="170px">
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 10%">
                                    <dx:ASPxLabel ID="ASPxLabel10" runat="server" Text="I_IMPORTE">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 40%">
                                    <dx:ASPxTextBox ID="txtTa09" runat="server" Width="170px">
                                    </dx:ASPxTextBox>
                                </td>
                                <td style="width: 10%">
                                    <dx:ASPxLabel ID="ASPxLabel11" runat="server" Text="I_ID_FORMA_PAGO">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 40%">
                                    <dx:ASPxTextBox ID="txtTa10" runat="server" Width="170px">
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 10%">
                                    <dx:ASPxLabel ID="ASPxLabel12" runat="server" Text="I_ID_TIPO_EMISOR">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 40%">
                                    <dx:ASPxTextBox ID="txtTa11" runat="server" Width="170px">
                                    </dx:ASPxTextBox>
                                </td>
                                <td style="width: 10%">
                                    <dx:ASPxLabel ID="ASPxLabel13" runat="server" Text="I_DETALLE_DOC">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 40%">
                                    <dx:ASPxTextBox ID="txtTa12" runat="server" Width="170px">
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 10%">
                                    <dx:ASPxLabel ID="ASPxLabel14" runat="server" Text="I_FECHA_TRANSACCION">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 40%">
                                    <dx:ASPxTextBox ID="txtTa13" runat="server" Width="170px">
                                    </dx:ASPxTextBox>
                                </td>
                                <td style="width: 10%">
                                    <dx:ASPxLabel ID="ASPxLabel15" runat="server" Text="I_FECHA_ENVIO">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 40%">
                                    <dx:ASPxTextBox ID="txtTa14" runat="server" Width="170px">
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 10%">
                                    <dx:ASPxLabel ID="ASPxLabel16" runat="server" Text="I_OBSERVACIONES">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 40%">
                                    <dx:ASPxTextBox ID="txtTa15" runat="server" Width="170px">
                                    </dx:ASPxTextBox>
                                </td>
                                <td style="width: 10%">
                                    <dx:ASPxLabel ID="ASPxLabel17" runat="server" Text="I_ID_TIPO_ACTIVIDAD">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 40%">
                                    <dx:ASPxTextBox ID="txtTa16" runat="server" Width="170px">
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 10%">
                                    <dx:ASPxLabel ID="ASPxLabel18" runat="server" Text="I_FECHA_OPERACION">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 40%">
                                    <dx:ASPxTextBox ID="txtTa17" runat="server" Width="170px">
                                    </dx:ASPxTextBox>
                                </td>
                                <td style="width: 10%">
                                    <dx:ASPxLabel ID="ASPxLabel19" runat="server" Text="I_CODIGO">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 40%">
                                    <dx:ASPxTextBox ID="txtTa18" runat="server" Width="170px">
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                            
                            
                            <tr>
                                <td colspan="3">
                                    <dx:ASPxTextBox ID="txtResultadoTa" runat="server" Width="170px">
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                    <dx:ASPxButton ID="btnRegistroDirTa" runat="server" Text="Registrar" AutoPostBack="False" OnClick="btnRegistroDirTa_Click">
                                    </dx:ASPxButton>
                                </td>
                            </tr>
                            
                        </table>
                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxCallbackPanel>
            
            
            <dx:ASPxCallbackPanel ID="pnlTransaccion" runat="server" Width="100%">
                <PanelCollection>
                    <dx:PanelContent runat="server">
                        <table  style="width: 100%">
                            <tr>
                                <td colspan="3">
                                    <dx:ASPxTextBox ID="txtCadenaTr" runat="server" AutoResizeWithContainer="True" Width="100%">
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                    <dx:ASPxButton ID="btnParseTr" runat="server" AutoPostBack="False" OnClick="btnParseTr_Click" Text="Extraer Cadena">
                                    </dx:ASPxButton>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 10%">
                                    <dx:ASPxLabel ID="ASPxLabel20" runat="server" Text="I_ID_TRANSACCION_PADRE">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 40%">
                                    <dx:ASPxTextBox ID="txtTr01" runat="server" Width="170px">
                                    </dx:ASPxTextBox>
                                </td>
                                <td style="width: 10%">
                                    <dx:ASPxLabel ID="ASPxLabel21" runat="server" Text="I_ID_MOVIMIENTO">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 40%">
                                    <dx:ASPxTextBox ID="txtTr02" runat="server" Width="170px">
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 10%">
                                    <dx:ASPxLabel ID="ASPxLabel22" runat="server" Text="I_ID_TIPO_TRANSACCION">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 40%">
                                    <dx:ASPxTextBox ID="txtTr03" runat="server" Width="170px">
                                    </dx:ASPxTextBox>
                                </td>
                                <td style="width: 10%">
                                    <dx:ASPxLabel ID="ASPxLabel23" runat="server" Text="I_ID_ESTADO_TRANSACCION">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 40%">
                                    <dx:ASPxTextBox ID="txtTr04" runat="server" Width="170px">
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 10%">
                                    <dx:ASPxLabel ID="ASPxLabel24" runat="server" Text="I_FECHA_HORA_EMISION">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 40%">
                                    <dx:ASPxTextBox ID="txtTr05" runat="server" Width="170px">
                                    </dx:ASPxTextBox>
                                </td>
                                <td style="width: 10%">
                                    <dx:ASPxLabel ID="ASPxLabel25" runat="server" Text="I_NIT_CONSUMIDOR">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 40%">
                                    <dx:ASPxTextBox ID="txtTr06" runat="server" Width="170px">
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 10%">
                                    <dx:ASPxLabel ID="ASPxLabel26" runat="server" Text="I_NIT_EMISOR">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 40%">
                                    <dx:ASPxTextBox ID="txtTr07" runat="server" Width="170px">
                                    </dx:ASPxTextBox>
                                </td>
                                <td style="width: 10%">
                                    <dx:ASPxLabel ID="ASPxLabel27" runat="server" Text="I_NRO_TRANSACCION">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 40%">
                                    <dx:ASPxTextBox ID="txtTr08" runat="server" Width="170px">
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 10%">
                                    <dx:ASPxLabel ID="ASPxLabel28" runat="server" Text="I_NRO_AUTORIZACION">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 40%">
                                    <dx:ASPxTextBox ID="txtTr09" runat="server" Width="170px">
                                    </dx:ASPxTextBox>
                                </td>
                                <td style="width: 10%">
                                    <dx:ASPxLabel ID="ASPxLabel29" runat="server" Text="I_CODIGO_CONTROL">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 40%">
                                    <dx:ASPxTextBox ID="txtTr10" runat="server" Width="170px">
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 10%">
                                    <dx:ASPxLabel ID="ASPxLabel30" runat="server" Text="I_RAZON_SOCIAL">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 40%">
                                    <dx:ASPxTextBox ID="txtTr11" runat="server" Width="170px">
                                    </dx:ASPxTextBox>
                                </td>
                                <td style="width: 10%">
                                    <dx:ASPxLabel ID="ASPxLabel31" runat="server" Text="I_PRECIO_UNITARIO">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 40%">
                                    <dx:ASPxTextBox ID="txtTr12" runat="server" Width="170px">
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 10%">
                                    <dx:ASPxLabel ID="ASPxLabel32" runat="server" Text="I_PRECIO_TOTAL">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 40%">
                                    <dx:ASPxTextBox ID="txtTr13" runat="server" Width="170px">
                                    </dx:ASPxTextBox>
                                </td>
                                <td style="width: 10%">
                                    <dx:ASPxLabel ID="ASPxLabel33" runat="server" Text="I_CODIGO_SEGURIDAD">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 40%">
                                    <dx:ASPxTextBox ID="txtTr14" runat="server" Width="170px">
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 10%">
                                    <dx:ASPxLabel ID="ASPxLabel34" runat="server" Text="I_USUARIO_TRANSACCION">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 40%">
                                    <dx:ASPxTextBox ID="txtTr15" runat="server" Width="170px">
                                    </dx:ASPxTextBox>
                                </td>
                                <td style="width: 10%">
                                    <dx:ASPxLabel ID="ASPxLabel35" runat="server" Text="I_OBSERVACIONES">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 40%">
                                    <dx:ASPxTextBox ID="txtTr16" runat="server" Width="170px">
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 10%">
                                    <dx:ASPxLabel ID="ASPxLabel36" runat="server" Text="I_ID_TIPO_CREDITO_FISCAL">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 40%">
                                    <dx:ASPxTextBox ID="txtTr17" runat="server" Width="170px">
                                    </dx:ASPxTextBox>
                                </td>
                                <td style="width: 10%">
                                    <dx:ASPxLabel ID="ASPxLabel37" runat="server" Text="I_ID_FORMA_PAGO">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 40%">
                                    <dx:ASPxTextBox ID="txtTr18" runat="server" Width="170px">
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 10%">
                                    <dx:ASPxLabel ID="ASPxLabel38" runat="server" Text="I_ID_TIPO_MONEDA">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 40%">
                                    <dx:ASPxTextBox ID="txtTr19" runat="server" Width="170px">
                                    </dx:ASPxTextBox>
                                </td>
                                <td style="width: 10%">
                                    <dx:ASPxLabel ID="ASPxLabel39" runat="server" Text="I_ID_TIPO_EMISOR">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 40%">
                                    <dx:ASPxTextBox ID="txtTr20" runat="server" Width="170px">
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 10%">
                                    <dx:ASPxLabel ID="ASPxLabel40" runat="server" Text="I_ID_AUTORIZACION_DATO">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 40%">
                                    <dx:ASPxTextBox ID="txtTr21" runat="server" Width="170px">
                                    </dx:ASPxTextBox>
                                </td>
                                <td style="width: 10%">
                                    <dx:ASPxLabel ID="ASPxLabel41" runat="server" Text="I_BINARIO_CONTROL">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 40%">
                                    <dx:ASPxTextBox ID="txtTr22" runat="server" Width="170px">
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 10%">
                                    <dx:ASPxLabel ID="ASPxLabel42" runat="server" Text="I_ID_TIPO_ACTIVIDAD">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 40%">
                                    <dx:ASPxTextBox ID="txtTr23" runat="server" Width="170px">
                                    </dx:ASPxTextBox>
                                </td>
                                <td style="width: 10%">
                                    <dx:ASPxLabel ID="ASPxLabel43" runat="server" Text="I_FECHA_OPERACION">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 40%">
                                    <dx:ASPxTextBox ID="txtTr24" runat="server" Width="170px">
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 10%">
                                    <dx:ASPxLabel ID="ASPxLabel44" runat="server" Text="I_CODIGO">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 40%">
                                    <dx:ASPxTextBox ID="txtTr25" runat="server" Width="170px">
                                    </dx:ASPxTextBox>
                                </td>
                                <td style="width: 10%">
                                    <dx:ASPxLabel ID="ASPxLabel45" runat="server" Text="I_APP_ID_USUARIO">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 40%">
                                    <dx:ASPxTextBox ID="txtTr26" runat="server" Width="170px">
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 10%">
                                    <dx:ASPxLabel ID="ASPxLabel46" runat="server" Text="I_APP_FECHA_REGISTRO">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 40%">
                                    <dx:ASPxTextBox ID="txtTr27" runat="server" Width="170px">
                                    </dx:ASPxTextBox>
                                </td>
                                <td style="width: 10%">
                                </td>
                                <td style="width: 40%">
                                </td>
                            </tr>
                            
                            <tr>
                                <td colspan="3">
                                    <dx:ASPxTextBox ID="txtResultadoTr" runat="server" Width="170px">
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                    <dx:ASPxButton ID="btnRegistroDirTr" runat="server" Text="Registrar" AutoPostBack="False" OnClick="btnRegistroDirTr_Click">
                                    </dx:ASPxButton>
                                </td>
                            </tr>
                            
                        </table>
                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxCallbackPanel>

        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>
































































    




























































