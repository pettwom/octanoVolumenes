<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtrComparativoInv.ascx.cs" Inherits="AnhHydroOctanoPresentacion.Controles.CtrComparativoInv" %>

<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>

<%@ Register src="CtrFecheroMes.ascx" tagname="CtrFecheroMes" tagprefix="uc1" %>

<style type="text/css">
    .auto-style10 {
        height: 26px;
    }
    .auto-style11 {
        width: 168px;
        height: 26px;
    }
    .auto-style12 {
        width: 138px;
        height: 26px;
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
    
    .auto-style14 {
        width: 202px;
    }
    
    .auto-style15 {
        height: 18px;
    }
    
    </style>


<div class="col-md-12">
    
    <table style="width: 100%; height: 100%">
    <tr>
        <td style="text-align: center; padding-bottom: 1px">
            <dx:ASPxRoundPanel runat="server" ShowHeader="False" Width="100%" ID="pnlTitulo">
                <PanelCollection>
                    <dx:PanelContent runat="server">
                        <dx:ASPxLabel runat="server" ForeColor="#367B2F" ID="lblTitulo" Font-Bold="True" Font-Names="Rockwell" Font-Size="Larger" Text="COMPARATIVO DE INVENTARIOS - FISICO VS DOCUMENTADO"></dx:ASPxLabel>
                </dx:PanelContent>
            </PanelCollection>
            <Border BorderStyle="Ridge" BorderWidth="1px" BorderColor="#003300"></Border>
            </dx:ASPxRoundPanel>
        </td>
    </tr>
    
    <tr>
        <td style="height: 100%">
            
            <dx:ASPxSplitter ID="ASPxSplitter1" runat="server" Height="98%" Width="100%" >
    <Panes>
        <dx:SplitterPane Size="15%" AllowResize="True" AutoHeight="True">
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
                            <td style="padding-bottom: 5px">
                                <dx:ASPxCallbackPanel ID="pnlCabecera" runat="server" ClientInstanceName="pnlCabecera" Width="100%" OnCallback="pnlCabecera_Callback">
                                    <PanelCollection>
                                        <dx:PanelContent runat="server">
                                            <table style="width: 100%">
                                                <tr>
                                                    <td>
                                                        Fecha Inicial :
                                                        <dx:ASPxDateEdit ID="detFechaIni" runat="server" ClientInstanceName="detFechaIni" Theme="Metropolis" ToolTip="Seleccione la fecha inicial" Width="140px">
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
                                                <tr style="padding-bottom: 5px">
                                                    <td>
                                                        Fecha Final :
                                                        <dx:ASPxDateEdit ID="detFechaFin" runat="server" ClientInstanceName="detFechaFin" Theme="Metropolis" Width="140px" ToolTip="Seleccione la fecha final">
                                                            <TimeSectionProperties>
                                                                <TimeEditProperties>
                                                                    <ClearButton Visibility="Auto">
                                                                    </ClearButton>
                                                                </TimeEditProperties>
                                                            </TimeSectionProperties>
                                                            <ClientSideEvents DateChanged="function(s, e) {
//pnlDatos.PerformCallback();
}" />
                                                            <ClearButton Visibility="Auto">
                                                            </ClearButton>
                                                        </dx:ASPxDateEdit>
                                                    </td>
                                                </tr>
                                                <tr style="padding-bottom: 5px">
                                                    <td>Selección Directa de Mes :<uc1:CtrFecheroMes ID="cfmMes" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr style="padding-bottom: 5px">
                                                    <td>
                                                        Tipo de Operación :
                                                        <dx:ASPxComboBox ID="cmbTipoOperacion" runat="server" ClientInstanceName="cmbTipoOperacion" TextField="TIPO_OPERACION" TextFormatString="{0}" Theme="Metropolis" ValueField="ID_TIPO_OPERACION" Width="140px">
                                                            <ClientSideEvents Init="function(s, e) {
	                    s.GetInputElement().readOnly = true;
                    }" />
                                                            <Columns>
                                                                <dx:ListBoxColumn Caption="DESCRIPCION" FieldName="TIPO_OPERACION" Name="NOMBRE" />
                                                                <dx:ListBoxColumn Caption="ID" FieldName="ID_TIPO_OPERACION" Name="ID_TIPO_OPERACION" Width="0px" />
                                                                <dx:ListBoxColumn Caption="CODIGO" FieldName="CODIGO" Name="CODIGO" Width="0px" />
                                                            </Columns>
                                                            <ClearButton Visibility="Auto">
                                                            </ClearButton>
                                                            <ReadOnlyStyle BackColor="#DEE8DB">
                                                            </ReadOnlyStyle>
                                                        </dx:ASPxComboBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </dx:PanelContent>
                                    </PanelCollection>
                                </dx:ASPxCallbackPanel>
                            </td>
                        </tr>
                        

                        <tr>
                            <td style="padding-bottom: 5px">
                                <dx:ASPxButton ID="btnConsultar" runat="server" AutoPostBack="False" Font-Bold="True" ForeColor="Maroon" Text="Obtener Información" Theme="Metropolis" Width="65%">
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
                                <dx:ASPxLabel ID="ASPxLabel58" runat="server" ForeColor="#003300" Text="Proyectos Asignados :">
                                </dx:ASPxLabel>
                            </td>
                        </tr>

                        <tr>
                            <td>
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
                                <dx:ASPxTreeList ID="tlsOperadores" runat="server" AutoGenerateColumns="False" ClientInstanceName="tlsOperadores" Font-Size="Smaller" Height="187px" KeyFieldName="intId" ParentFieldName="intIdPadre" Theme="Youthful" Styles-Header-BackColor="#317D40" ToolTip="Seleccione un proyecto" OnCustomCallback="tlsOperadores_CustomCallback">
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
pnlCabecera.PerformCallback();
}" FocusedNodeChanged="function(s, e) {
pnlCabecera.PerformCallback();
}" />
                                </dx:ASPxTreeList>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                &nbsp;</td>
                        </tr>

                    </table>
                </dx:SplitterContentControl>
            </ContentCollection>
        </dx:SplitterPane>
        <dx:SplitterPane ScrollBars="Auto">
            <ContentCollection>
                <dx:SplitterContentControl runat="server">
                    
                    <dx:ASPxCallbackPanel ID="pnlDatos" runat="server" Theme="Youthful" Width="100%" ClientInstanceName="pnlDatos" OnCallback="pnlDatos_Callback">
                        <ClientSideEvents EndCallback="function(s, e) {
LoadingPanel.Hide();
}" BeginCallback="function(s, e) {
LoadingPanel.Show();
}" />
                        <PanelCollection>
                            <dx:PanelContent runat="server">
                                <table style="width: 100%;">
                                    <tr>
                                        <td colspan="7" style="text-align: center;width: 100%;">
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
                                    <tr>
                                        <td colspan="3" style="width: 50%">
                                            <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="#10420F" Text="NOMBRE COMERCIAL"></asp:Label>
                                        </td>
                                        <td style="width: 10%">
                                            <asp:Label ID="Label3" runat="server" Font-Bold="True" ForeColor="#10420F" Text="NIT"></asp:Label>
                                        </td>
                                        <td style="width: 15%">
                                            <asp:Label ID="Label5" runat="server" Font-Bold="True" ForeColor="#10420F" Text="DEPARTAMENTO"></asp:Label>
                                        </td>
                                        <td colspan="2" style="width: 25%">
                                            <asp:Label ID="Label4" runat="server" Font-Bold="True" ForeColor="#10420F" Text="DIRECCION"></asp:Label>
                                        </td>

                                    </tr>
                                        <tr>
                                            <td colspan="3" style="width: 50%; padding-bottom: 7px">
                                                <dx:ASPxLabel ID="lblRazonSocial" runat="server" ClientInstanceName="lblRazonSocial">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td style="width: 10%">
                                                <asp:Label ID="lblNit" runat="server"></asp:Label>
                                            </td>
                                            <td style="width: 15%">
                                                <asp:Label ID="lblDepto" runat="server"></asp:Label>
                                            </td>
                                            <td colspan="2" style="width: 25%; padding-bottom: 7px">
                                                <asp:Label ID="lblRepLegal" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" style="width: 50%">
                                                <dx:ASPxLabel ID="ASPxLabel61" runat="server" Width="100%">
                                                    <BorderBottom BorderColor="#FF6600" BorderStyle="Solid" />
                                                </dx:ASPxLabel>
                                            </td>
                                            <td style="width: 15%">
                                                <dx:ASPxComboBox ID="cmbTipoExport" runat="server" ClientInstanceName="cmbTipoExport" Height="27px" SelectedIndex="0" Theme="Metropolis" ToolTip="Seleccione el Formato del Reporte" ValueType="System.Int32" Width="100%">
                                                    <Items>
                                                        <dx:ListEditItem Selected="True" Text="EXCEL" Value="1" />
                                                        <dx:ListEditItem Text="PDF" Value="2" />
                                                    </Items>
                                                    <ClearButton Visibility="Auto">
                                                    </ClearButton>
                                                </dx:ASPxComboBox>
                                            </td>
                                            <td colspan="2" style="width: 20%">
                                                <dx:ASPxButton ID="btnEmiteReporte" runat="server" AutoPostBack="False" Font-Bold="True" ForeColor="Maroon" Text="Emitir Reporte" Theme="Metropolis" Width="100%" OnClick="btnEmiteReporte_Click">
                                                    <Image IconID="print_printer_16x16">
                                                    </Image>
                                                </dx:ASPxButton>
                                            </td>
                                    </tr>
                                        <tr>
                                            <td colspan="7">
                                                
                                                <dx:ASPxGridView ID="dgvComparativo" runat="server" AutoGenerateColumns="False" ClientInstanceName="dgvComparativo" EnableTheming="True" Font-Size="Smaller" KeyFieldName="ID_MOVIMIENTO" Theme="Default" Width="100%" OnHtmlRowPrepared="dgvComparativo_HtmlRowPrepared" OnCustomButtonCallback="dgvComparativo_CustomButtonCallback" OnCustomButtonInitialize="dgvComparativo_CustomButtonInitialize">
                                                    <ClientSideEvents CustomButtonClick="function(s, e) {

switch(e.buttonID) 
{
case&nbsp; 'btnGenerar':
e.processOnServer = confirm('¿Desea generar el documento de diferencia?'); 
break;
} 

}" BeginCallback="function(s, e) {
	LoadingPanel.Show();
}" EndCallback="function(s, e) {
	LoadingPanel.Hide();
}" />
                                                    <SettingsContextMenu EnableRowMenu="False">
                                                    </SettingsContextMenu>
                                                    <Templates>
                                                        <GroupFooterRow>
                                                            <dx:ASPxLabel ID="ASPxLabel341" runat="server" Text="ASPxLabel">
                                                            </dx:ASPxLabel>
                                                        </GroupFooterRow>
                                                        <EditForm>
                                                        </EditForm>
                                                    </Templates>
                                                    <SettingsPager NumericButtonCount="3" PageSize="30">
                                                        <PageSizeItemSettings Items="50, 100, 200, 500, 1000" Visible="True">
                                                        </PageSizeItemSettings>
                                                    </SettingsPager>
                                                    <SettingsEditing Mode="PopupEditForm">
                                                    </SettingsEditing>
                                                    <Settings ShowFilterRow="True" ShowHeaderFilterButton="True" />
                                                    <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" />
                                                    <SettingsPopup>
                                                        <EditForm HorizontalAlign="Center" Modal="True" VerticalAlign="Middle" VerticalOffset="0" />
                                                        <CustomizationWindow HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" />
                                                    </SettingsPopup>
                                                    <SettingsText CommandNew="Nuevo" />
                                                    <Columns>
                                                        <dx:GridViewCommandColumn ShowClearFilterButton="True" ShowInCustomizationForm="True" ToolTip="Funciones" VisibleIndex="21">
                                                            <CustomButtons>
                                                                <dx:GridViewCommandColumnCustomButton ID="btnGenerar" Text=" ">
                                                                    <Image IconID="conditionalformatting_topbottomrules_16x16" ToolTip="Generar Documento de Diferencia">
                                                                    </Image>
                                                                </dx:GridViewCommandColumnCustomButton>
                                                            </CustomButtons>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <CellStyle ForeColor="#006600">
                                                            </CellStyle>
                                                            <HeaderTemplate>
                                                                <dx:ASPxHyperLink ID="hlkNuevo" runat="server" Font-Overline="False" Font-Size="Small" Font-Underline="True" ImageUrl="~/UI/img/Volumenes/page_add.png" Text="Nuevo" ToolTip="Nuevo" Visible="False">
                                                                    <ClientSideEvents Click="function(s, e) {

pucConfig.Show()
txtId.SetValue(0);
pucConfig.PerformCallback();

}" />
                                                                </dx:ASPxHyperLink>
                                                            </HeaderTemplate>
                                                        </dx:GridViewCommandColumn>
                                                        <dx:GridViewDataTextColumn Caption="COD. INFRA." FieldName="CODIGO_OPERADOR" ShowInCustomizationForm="True" ToolTip="Codigo de la infraestructura" VisibleIndex="3">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="PRODUCTO" FieldName="PRODUCTO" ShowInCustomizationForm="True" VisibleIndex="4">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="SALDO DOCUMENTADO" FieldName="SALDO_DOC" ShowInCustomizationForm="True" VisibleIndex="7">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="SALDO FISICO" FieldName="SALDO_FIS" ShowInCustomizationForm="True" VisibleIndex="6">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="APP_ID_USUARIO" FieldName="APP_ID_USUARIO" ShowInCustomizationForm="True" Visible="False" VisibleIndex="8">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="FECHA" FieldName="FECHA" ShowInCustomizationForm="True" VisibleIndex="1">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="T. INFRA." FieldName="TIPO" ShowInCustomizationForm="True" ToolTip="Tipo de infraestructura" VisibleIndex="2">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="APP_FECHA_REGISTRO" FieldName="APP_FECHA_REGISTRO" ShowInCustomizationForm="True" Visible="False" VisibleIndex="9">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="DIFERENCIA" FieldName="DIFERENCIA" ShowInCustomizationForm="True" VisibleIndex="10">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="TIPO_INFRAESTRUCTURA" FieldName="TIPO_INFRAESTRUCTURA" ShowInCustomizationForm="True" Visible="False" VisibleIndex="11">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="ID_INFRAESTRUCTURA" FieldName="ID_INFRAESTRUCTURA" ShowInCustomizationForm="True" Visible="False" VisibleIndex="12">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="ID_HIDROCARBURO" FieldName="ID_HIDROCARBURO" ShowInCustomizationForm="True" Visible="False" VisibleIndex="13">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="FECHA_OPERACION" FieldName="FECHA_OPERACION" ShowInCustomizationForm="True" Visible="False" VisibleIndex="15">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="ID" FieldName="ID_MOVIMIENTO" ShowInCustomizationForm="True" VisibleIndex="0">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="U. MEDIDA" FieldName="U_MEDIDA" ShowInCustomizationForm="True" VisibleIndex="5">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="ID_UNIDAD_HIDROCARBURO" FieldName="ID_UNIDAD_HIDROCARBURO" ShowInCustomizationForm="True" Visible="False" VisibleIndex="14">
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                    <Styles CssPostfix="DevGlp">
                                                        <AlternatingRow BackColor="#EEEEEE">
                                                        </AlternatingRow>
                                                    </Styles>
                                                    <TotalSummary>
                                                        <dx:ASPxSummaryItem FieldName="F_IMPORTE" ShowInColumn="IMPORTE" SummaryType="Custom" Tag="imp" />
                                                        <dx:ASPxSummaryItem FieldName="VOLUMEN" ShowInColumn="CANTIDAD" SummaryType="Custom" Tag="vol" />
                                                        <dx:ASPxSummaryItem FieldName="STR_ESTADO" ShowInColumn="EST." SummaryType="Custom" Tag="est" />
                                                    </TotalSummary>
                                                </dx:ASPxGridView>
                                                
                                            </td>
                                        </tr>

                                        <tr>
                                            <td colspan="7" class="auto-style15">
                                                
                                                <dx:ASPxGridViewExporter ID="dgeExportar" runat="server">
                                                </dx:ASPxGridViewExporter>
                                                
                                                </td>
                                    </tr>
                                        

                                    

                                    <tr>
                                        <td colspan="7">
                                            
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style11">
                                            &nbsp;</td>
                                        <td class="auto-style10">&nbsp;</td>
                                        <td class="auto-style10"></td>
                                        <td class="auto-style12"></td>
                                        <td class="auto-style10">&nbsp;</td>
                                        <td class="auto-style10">&nbsp;</td>
                                        <td class="auto-style14">
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

        </td>
    </tr>
</table>

</div>




    <dx:ASPxPopupControl ID="pucDocumento" runat="server" CloseAction="CloseButton" CloseOnEscape="true"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Above" ClientInstanceName="pucDocumento"
        HeaderText="Documentos" AllowDragging="True" Modal="True" PopupAnimationType="Fade"
        EnableViewState="False" PopupHorizontalOffset="40" PopupVerticalOffset="40" Theme="Metropolis">
        <ClientSideEvents PopUp="function(s, e) { ASPxClientEdit.ClearGroup('createAccountGroup'); }" />
        <HeaderImage IconID="actions_insert_32x32">
        </HeaderImage>
        <SizeGripImage Width="11px" />
        <HeaderStyle>
        <BorderBottom BorderStyle="Solid" />
        </HeaderStyle>
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <dx:ASPxPanel ID="Panel2" runat="server" DefaultButton="btCreate">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <table class="dxflInternalEditorTable_Youthful">
                <tr>
                    <td style="width: 25%; text-align: right;">
                        <dx:ASPxLabel ID="ASPxLabel40" runat="server" Font-Bold="True" ForeColor="#666666" Text="Fecha :">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 25%;">
                        <dx:ASPxDateEdit ID="detFechaDocEf" runat="server" EditFormat="DateTime" Font-Bold="False" Theme="Metropolis">
                            <TimeSectionProperties>
                                <TimeEditProperties>
                                    <ClearButton Visibility="Auto">
                                    </ClearButton>
                                </TimeEditProperties>
                            </TimeSectionProperties>
                            <ClearButton Visibility="Auto">
                            </ClearButton>
                            <ValidationSettings>
                                <RequiredField ErrorText="ERROR: Introduzca la fecha." />
                            </ValidationSettings>
                            <ReadOnlyStyle BackColor="#DEE8DB">
                            </ReadOnlyStyle>
                            <FocusedStyle BackColor="#FFFFCC">
                            </FocusedStyle>
                            <DisabledStyle BackColor="#DEE8DB">
                            </DisabledStyle>
                        </dx:ASPxDateEdit>
                    </td>
                    <td style="width: 25%; text-align: right;">&nbsp;</td>
                    <td style="width: 25%; text-align: right;">
                        <dx:ASPxLabel ID="lblIdDocEf" runat="server" ClientInstanceName="lblIdDocEf" Font-Bold="True" ForeColor="#333333" Height="16px" Text="id" Width="115px">
                        </dx:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td style="width: 25%; text-align: right;">
                        <dx:ASPxLabel ID="lblTipoEmisorEf" runat="server" Font-Bold="True" ForeColor="#666666" Height="16px" Text="Entidad Emisora :" Width="110px">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 25%;">
                        <dx:ASPxComboBox ID="cmbTipoEmisorEf" runat="server" ClientInstanceName="cmbTipoCredEf" TextField="DESCRIPCION" TextFormatString="{0}" ValueField="ID_PARAMETRICA_GLOBAL" Theme="Metropolis">
                            <Columns>
                                <dx:ListBoxColumn Caption="DESCRIPCION" FieldName="DESCRIPCION" Name="DESCRIPCION" />
                                <dx:ListBoxColumn Caption="ID_PARAMETRICA_GLOBAL" FieldName="ID_PARAMETRICA_GLOBAL" Name="ID_PARAMETRICA_GLOBAL" Width="0px" />
                                <dx:ListBoxColumn Caption="CODIGO" FieldName="CODIGO" Name="CODIGO" Width="0px" />
                            </Columns>
                            <ClearButton Visibility="Auto">
                            </ClearButton>
                            <ValidationSettings>
                                <RequiredField IsRequired="True" />
                            </ValidationSettings>
                            <ReadOnlyStyle BackColor="#FFE1C4">
                            </ReadOnlyStyle>
                            <FocusedStyle BackColor="#FFFFCC">
                            </FocusedStyle>
                            <DisabledStyle BackColor="#DEE8DB">
                            </DisabledStyle>
                        </dx:ASPxComboBox>
                    </td>
                    <td style="width: 25%; text-align: right;">
                        <dx:ASPxLabel ID="lblTipoCredEf" runat="server" Font-Bold="True" ForeColor="#666666" Height="16px" Text="Válido Cred. Fiscal :" Width="120px">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 25%; text-align: right;">
                        <dx:ASPxComboBox ID="cmbTipoCredEf" runat="server" ClientInstanceName="cmbTipoCredEf" TextField="DESCRIPCION" TextFormatString="{0}" ValueField="ID_PARAMETRICA_GLOBAL" Theme="Metropolis" AllowMouseWheel="False" AnimationType="None" EnableCallbackMode="True" EnableViewState="False">
                            <Columns>
                                <dx:ListBoxColumn Caption="DESCRIPCION" FieldName="DESCRIPCION" Name="DESCRIPCION" />
                                <dx:ListBoxColumn Caption="ID_PARAMETRICA_GLOBAL" FieldName="ID_PARAMETRICA_GLOBAL" Name="ID_PARAMETRICA_GLOBAL" Width="0px" />
                                <dx:ListBoxColumn Caption="CODIGO" FieldName="CODIGO" Name="CODIGO" Width="0px" />
                            </Columns>
                            <ClearButton Visibility="Auto">
                            </ClearButton>
                            <ReadOnlyStyle BackColor="#FFE1C4">
                            </ReadOnlyStyle>
                            <FocusedStyle BackColor="#FFFFCC">
                            </FocusedStyle>
                            <DisabledStyle BackColor="#DEE8DB">
                            </DisabledStyle>
                        </dx:ASPxComboBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 25%; text-align: right;">
                        <dx:ASPxLabel ID="lblMonedaEf" runat="server" Font-Bold="True" ForeColor="#666666" Text="Moneda :">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 25%;">
                        <dx:ASPxComboBox ID="cmbMonedaEf" runat="server" ReadOnly="True" TextField="NOMBRE" TextFormatString="{0}" ValueField="ID_CATALOGO" Theme="Metropolis">
                            <Columns>
                                <dx:ListBoxColumn Caption="NOMBRE" FieldName="NOMBRE" Name="NOMBRE" />
                                <dx:ListBoxColumn Caption="ID_CATALOGO" FieldName="ID_CATALOGO" Name="ID_CATALOGO" Width="0px" />
                                <dx:ListBoxColumn Caption="CODIGO" FieldName="CODIGO" Name="CODIGO" Width="0px" />
                            </Columns>
                            <ClearButton Visibility="Auto">
                            </ClearButton>
                            <ValidationSettings>
                                <RequiredField ErrorText="ERROR: Introduzca la moneda." IsRequired="True" />
                            </ValidationSettings>
                            <ReadOnlyStyle BackColor="#FFE1C4">
                            </ReadOnlyStyle>
                            <FocusedStyle BackColor="#FFFFCC">
                            </FocusedStyle>
                            <DisabledStyle BackColor="#DEE8DB">
                            </DisabledStyle>
                        </dx:ASPxComboBox>
                    </td>
                    <td style="width: 25%; text-align: right;">
                        <dx:ASPxLabel ID="lblTipoConsEf" runat="server" Font-Bold="True" ForeColor="#666666" Text="Forma de Pago :" Width="115px">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 25%; text-align: right;">
                        <dx:ASPxComboBox ID="cmbTipoPagoEf" runat="server" ClientInstanceName="cmbTipoPagoEf" TextField="DESCRIPCION" TextFormatString="{0}" ValueField="ID_PARAMETRICA_GLOBAL" Theme="Metropolis">
                            <ClientSideEvents SelectedIndexChanged="function(s, e) {

//cmbTipoFlujoEf

//	if(cmbTipoPagoEf.GetValue()==3 || cmbTipoPagoEf.GetValue()==-99999){
//cmbTipoCredEf.SetValue('9');
//	}
//else{
//cmbTipoCredEf.SetValue('10');
//}
}" />
                            <Columns>
                                <dx:ListBoxColumn Caption="DESCRIPCION" FieldName="DESCRIPCION" Name="DESCRIPCION" />
                                <dx:ListBoxColumn Caption="ID_PARAMETRICA_GLOBAL" FieldName="ID_PARAMETRICA_GLOBAL" Name="ID_PARAMETRICA_GLOBAL" Width="0px" />
                                <dx:ListBoxColumn Caption="CODIGO" FieldName="CODIGO" Name="CODIGO" Width="0px" />
                            </Columns>
                            <ClearButton Visibility="Auto">
                            </ClearButton>
                            <ValidationSettings>
                                <RequiredField ErrorText="ERROR : Introduzca la forma de pago." IsRequired="True" />
                            </ValidationSettings>
                            <ReadOnlyStyle BackColor="#FFE1C4">
                            </ReadOnlyStyle>
                            <FocusedStyle BackColor="#FFFFCC">
                            </FocusedStyle>
                            <DisabledStyle BackColor="#DEE8DB">
                            </DisabledStyle>
                        </dx:ASPxComboBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 25%; text-align: right;  padding-bottom: 10px;">
                        <dx:ASPxLabel ID="lblUsuarioEf" runat="server" Font-Bold="True" ForeColor="#666666" Text="Usuario :">
                        </dx:ASPxLabel>
                    </td>
                    <td style="padding-bottom: 10px; width: 25%;">
                        <dx:ASPxTextBox ID="txtUsuarioEf" runat="server" ClientInstanceName="txtId" MaxLength="100">
                            <ReadOnlyStyle BackColor="#DEE8DB">
                            </ReadOnlyStyle>
                            <FocusedStyle BackColor="#FFFFCC">
                            </FocusedStyle>
                            <DisabledStyle BackColor="#DEE8DB">
                            </DisabledStyle>
                        </dx:ASPxTextBox>
                    </td>
                    <td style="width: 25%; text-align: right; padding-bottom: 10px;">
                        &nbsp;</td>
                    <td style="width: 25%; padding-bottom: 10px;">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 25%; text-align: right;">
                        <dx:ASPxLabel ID="lblNoAutoEf" runat="server" Font-Bold="True" ForeColor="#666666" Text="No. Autorización :" Width="100%" ClientInstanceName="lblNoAutoEf">
                        </dx:ASPxLabel>
                        <dx:ASPxLabel ID="lblNoDocRefEf" runat="server" ClientInstanceName="lblNoDocRefEf" Font-Bold="True" ForeColor="#666666" Text="No. Doc. Referencia :" Width="130px">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 25%;">
                        <dx:ASPxTextBox ID="txtNoDocRefEf" runat="server" ClientInstanceName="txtNoDocRefEf" MaxLength="15" Width="170px">
                            <ReadOnlyStyle BackColor="#DEE8DB">
                            </ReadOnlyStyle>
                            <FocusedStyle BackColor="#FFFFCC">
                            </FocusedStyle>
                            <DisabledStyle BackColor="#DEE8DB">
                            </DisabledStyle>
                        </dx:ASPxTextBox>
                        <dx:ASPxTextBox ID="txtNoAutoEf" runat="server" ClientInstanceName="txtNoAutoEf" MaxLength="15" Width="170px" ForeColor="#666666">
                            <ReadOnlyStyle BackColor="#DEE8DB">
                            </ReadOnlyStyle>
                            <FocusedStyle BackColor="#FFFFCC">
                            </FocusedStyle>
                            <DisabledStyle BackColor="#DEE8DB">
                            </DisabledStyle>
                        </dx:ASPxTextBox>
                    </td>
                    <td style="width: 25%; text-align: right;">
                        <dx:ASPxLabel ID="lblNoFactEf" runat="server" Font-Bold="True" ForeColor="#666666" Text="No. Factura :" ClientInstanceName="lblNoFactEf">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 25%">
                        <dx:ASPxTextBox ID="txtNoFactEf" runat="server" MaxLength="15" Text="0" Width="170px" ClientInstanceName="txtNoFactEf">
                            <MaskSettings Mask="&lt;0..999999999999999&gt;" />
                            <ReadOnlyStyle BackColor="#DEE8DB">
                            </ReadOnlyStyle>
                            <FocusedStyle BackColor="#FFFFCC">
                            </FocusedStyle>
                            <DisabledStyle BackColor="#DEE8DB">
                            </DisabledStyle>
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 25%; text-align: right;">
                        <dx:ASPxLabel ID="lblCodCtrlEf" runat="server" ClientInstanceName="lblCodCtrlEf" Font-Bold="True" ForeColor="#666666" Text="Cod. Control :">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 25%;">
                        <dx:ASPxTextBox ID="txtCodCtrlEf" runat="server" ClientInstanceName="txtCodCtrlEf" MaxLength="14" Width="170px">
                            <ClientSideEvents GotFocus="function(s, e) {


try{
str = s.GetValue().split('-').join(''); 
s.SetValue(str);
}
catch(e){
}


}

" KeyPress="function(s, e) {

if (!((event.keyCode &gt;= 48 &amp;&amp; event.keyCode &lt;= 57)||(event.keyCode &gt;= 65 &amp;&amp; event.keyCode &lt;= 70)||(event.keyCode &gt;= 97 &amp;&amp; event.keyCode &lt;= 102)) ) event.returnValue = false;

}" LostFocus="function(s, e) {
s.SetValue(s.GetValue().toUpperCase());

if(s.GetValue().length%2==1)
{
s.SetValue(s.GetValue()+'0');
}

y= s.GetValue();
var x='';
while(y.length&gt;0)
{&nbsp;
z=y.substr(0,2);
x=x+z;
y=y.substr(2,y.length);

if(y.length&gt;0)
{
x=x+'-';
}
} 

s.SetValue(x);

}" />
                            <ValidationSettings SetFocusOnError="True">
                            </ValidationSettings>
                            <ReadOnlyStyle BackColor="#DEE8DB">
                            </ReadOnlyStyle>
                            <FocusedStyle BackColor="#FFFFCC">
                            </FocusedStyle>
                            <DisabledStyle BackColor="#DEE8DB">
                            </DisabledStyle>
                        </dx:ASPxTextBox>
                    </td>
                    <td style="width: 25%; text-align: right;">&nbsp;</td>
                    <td style="width: 25%;">&nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 25%; text-align: right; padding-bottom: 10px;">
                        <dx:ASPxLabel ID="lblResponsableEf" runat="server" Font-Bold="True" ForeColor="#666666" Text="Responsable :">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 25%;" style="padding-bottom: 10px;">
                        <dx:ASPxTextBox ID="txtResponsableEf" runat="server" ClientInstanceName="txtResponsableEf" Height="16px" MaxLength="90">
                            <ClientSideEvents TextChanged="function(s, e) {
 s.SetText(s.GetText().toUpperCase());
}" />
                            <ReadOnlyStyle BackColor="#DEE8DB">
                            </ReadOnlyStyle>
                            <FocusedStyle BackColor="#FFFFCC">
                            </FocusedStyle>
                            <DisabledStyle BackColor="#DEE8DB">
                            </DisabledStyle>
                        </dx:ASPxTextBox>
                    </td>
                    <td style="width: 25%; text-align: right; padding-bottom: 10px;">
                        <dx:ASPxLabel ID="lblCiEf" runat="server" Font-Bold="True" ForeColor="#666666" Text="C.I. :">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 25%; padding-bottom: 10px;">
                        <dx:ASPxTextBox ID="txtCiEf" runat="server" ClientInstanceName="txtCiEf" MaxLength="15" Text="0" Width="170px">
                            <MaskSettings Mask="&lt;0..999999999999999&gt;" />
                            <ReadOnlyStyle BackColor="#DEE8DB">
                            </ReadOnlyStyle>
                            <FocusedStyle BackColor="#FFFFCC">
                            </FocusedStyle>
                            <DisabledStyle BackColor="#DEE8DB">
                            </DisabledStyle>
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 25%; text-align: right;">
                        <dx:ASPxLabel ID="lblPrecioEf" runat="server" Font-Bold="True" ForeColor="#666666" Text="Precio :" ClientInstanceName="lblPrecioEf">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 25%;">
                        <dx:ASPxTextBox ID="txtPrecioEf" runat="server" ClientInstanceName="txtPrecioEf" Text="0" Width="170px">
                            <ClientSideEvents LostFocus="function(s, e) {
if(parseFloat(txtImporteEf.GetText(), 10)==0 )
{
var num1 = parseFloat(txtPrecioEf.GetText(), 10);
var num2 = parseFloat(txtCantidadDocEf.GetText(), 10);
txtImporteEf.SetText((num1*num2).toFixed(2));
}
}" />
                            <MaskSettings IncludeLiterals="DecimalSymbol" Mask="&lt;0..999&gt;.&lt;00..99&gt;" PromptChar=" " />
                            <ReadOnlyStyle BackColor="#DEE8DB">
                            </ReadOnlyStyle>
                            <FocusedStyle BackColor="#FFFFCC">
                            </FocusedStyle>
                            <DisabledStyle BackColor="#DEE8DB">
                            </DisabledStyle>
                        </dx:ASPxTextBox>
                    </td>
                    <td style="width: 25%; text-align: right;">
                        &nbsp;</td>
                    <td style="width: 25%">&nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 25%; text-align: right;">
                        <dx:ASPxLabel ID="lblCantidadDocEf" runat="server" Font-Bold="True" ForeColor="#666666" Text="Cantidad :" ClientInstanceName="lblCantidadDocEf">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 25%;">
                        <dx:ASPxTextBox ID="txtCantidadDocEf" runat="server" ClientInstanceName="txtCantidadDocEf" ReadOnly="True" Text="0.00" Width="170px">
                            <ClientSideEvents LostFocus="function(s, e) {
if(parseFloat(txtImporteEf.GetText(), 10)==0 )
{
var num1 = parseFloat(txtPrecioEf.GetText(), 10);
var num2 = parseFloat(txtCantidadEf.GetText(), 10);
txtImporteEf.SetText((num1*num2).toFixed(2));
}
}" />
                            <MaskSettings IncludeLiterals="DecimalSymbol" Mask="&lt;0..9999999&gt;.&lt;00..99&gt;" PromptChar=" " />
                            <ReadOnlyStyle BackColor="#FFE1C4">
                            </ReadOnlyStyle>
                            <FocusedStyle BackColor="#FFFFCC">
                            </FocusedStyle>
                            <DisabledStyle BackColor="#DEE8DB">
                            </DisabledStyle>
                        </dx:ASPxTextBox>
                    </td>
                    <td style="width: 25%; text-align: right;">&nbsp;</td>
                    <td style="width: 25%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: right; padding-bottom: 10px;">
                        <dx:ASPxLabel ID="lblImporteEf" runat="server" Font-Bold="True" ForeColor="#666666" Text="Importe :" ClientInstanceName="lblImporteEf">
                        </dx:ASPxLabel>
                    </td>
                    <td style="padding-bottom: 10px;" style="width: 25%;">
                        <dx:ASPxTextBox ID="txtImporteEf" runat="server" ClientInstanceName="txtImporteEf" Text="0" Width="170px">
                            <MaskSettings IncludeLiterals="DecimalSymbol" Mask="&lt;0..9999999&gt;.&lt;00..99&gt;" />
                            <ReadOnlyStyle BackColor="#DEE8DB">
                            </ReadOnlyStyle>
                            <FocusedStyle BackColor="#FFFFCC">
                            </FocusedStyle>
                            <DisabledStyle BackColor="#DEE8DB">
                            </DisabledStyle>
                        </dx:ASPxTextBox>
                    </td>
                    <td style="text-align: right; padding-bottom: 10px;"></td>
                    <td style="padding-bottom: 10px;"></td>
                </tr>
                <tr>
                    <td style="width: 25%; text-align: right; padding-bottom: 10px;">
                        <dx:ASPxLabel ID="lblObsDocEf" runat="server" Font-Bold="True" ForeColor="#666666" Text="Observaciones :" Width="150px">
                        </dx:ASPxLabel>
                    </td>
                    <td colspan="3" style="padding-bottom: 10px;">
                        <dx:ASPxTextBox ID="txtObsDocEf" runat="server" MaxLength="15" Width="100%" ClientInstanceName="txtObsDocEf">
                            <ReadOnlyStyle BackColor="#DEE8DB">
                            </ReadOnlyStyle>
                            <FocusedStyle BackColor="#FFFFCC">
                            </FocusedStyle>
                            <DisabledStyle BackColor="#DEE8DB">
                            </DisabledStyle>
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 25%; text-align: right;">&nbsp;</td>
                    <td style="width: 25%;">
                        <dx:ASPxTextBox ID="txtIdDocEf" runat="server" ClientInstanceName="txtIdDocEf" Text="0" Width="170px" Font-Overline="False" Font-Size="XX-Small" ForeColor="White">
                            <MaskSettings Mask="&lt;0..999999999999999&gt;" />
                            <ReadOnlyStyle ForeColor="White">
                            </ReadOnlyStyle>
                            <FocusedStyle BackColor="White">
                            </FocusedStyle>
                            <Border BorderStyle="None" />
                            <BorderBottom BorderStyle="None" />
                        </dx:ASPxTextBox>
                    </td>
                    <td style="width: 25%; text-align: right;">
                        <asp:TextBox ID="txtIdPadre" runat="server" BorderStyle="None" ForeColor="White" style="margin-top: 0px"></asp:TextBox>
                    </td>
                    <td style="width: 25%">
                        <dx:ASPxButton ID="btnAgregaDoc" runat="server" AutoPostBack="False" Text="Agregar Documento" Theme="Metropolis" EnableTheming="True" Width="100%" Font-Bold="True" ForeColor="Maroon">
                            <ClientSideEvents Click="function(s, e) {
dgvDocumentos.PerformCallback();
pucDocumento.Hide();
}" />
                            <Image IconID="reports_addgroupheader_16x16">
                            </Image>
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxPanel>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>








    
