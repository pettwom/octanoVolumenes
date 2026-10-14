<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtrDjMonitoreo.ascx.cs" Inherits="AnhHydroOctanoPresentacion.Controles.CtrDjMonitoreo" %>
<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>
<%@ Register TagPrefix="dx" Namespace="DevExpress.Web.ASPxPivotGrid" Assembly="DevExpress.Web.ASPxPivotGrid.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>

<style type="text/css">
    .auto-style1 {
        height: 18px;
    }
    .auto-style4 {
        height: 18px;
        width: 168px;
    }
    .auto-style6 {
        height: 18px;
        width: 138px;
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
    
    </style>

<div class="col-md-12">
    <table style="width: 100%">
        <tr>
            <td style="text-align: center; padding-bottom: 1px">
                <dx:ASPxRoundPanel runat="server" ShowHeader="False" Width="100%" ID="pnlTitulo">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <dx:ASPxLabel runat="server" ForeColor="#367B2F" ID="lblTitulo" Font-Bold="True" Font-Names="Rockwell" Font-Size="Larger" Text="MONITOREO DE DECLARACIONES JURADAS"></dx:ASPxLabel>
                    </dx:PanelContent>
                </PanelCollection>
                <Border BorderStyle="Ridge" BorderWidth="1px" BorderColor="#003300"></Border>
                </dx:ASPxRoundPanel>
            </td>
        </tr>
    </table>

    <dx:ASPxSplitter ID="ASPxSplitter1" runat="server" Height="100%">
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
                                <td>Mes :<dx:ASPxComboBox ID="cmbMes" runat="server" ClientInstanceName="cmbMes" Theme="Metropolis" ValueType="System.Int32" Width="140px" ToolTip="Seleccione un mes">
                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
    tacMovimientos.SetActiveTabIndex(0);
    pvgAcumulado.SetVisible(true);
    dgvInternacional.SetVisible(false);
    dgvContenedor.SetVisible(false);
    dgvRecaudo.SetVisible(false);

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
                                <td style="padding-bottom: 10px">Año :<dx:ASPxComboBox ID="cmbGestion" runat="server" ClientInstanceName="cmbGestion" Theme="Metropolis" ValueType="System.Int32" Width="140px" ToolTip="Seleccione un año">
                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
    tacMovimientos.SetActiveTabIndex(0);
    pvgAcumulado.SetVisible(true);
    dgvInternacional.SetVisible(false);
    dgvContenedor.SetVisible(false);
    dgvRecaudo.SetVisible(false);
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
                                    <dx:ASPxLabel ID="ASPxLabel58" runat="server" ForeColor="#003300" Text="Proyectos Asignados :">
                                    </dx:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <dx:ASPxTextBox ID="txtBuscador" runat="server" ClientInstanceName="txtBuscador" Font-Size="Smaller" Theme="Metropolis" ToolTip="Buscador" Width="100%" BackColor="#FFEFDF">
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
                                    <dx:ASPxTreeList ID="tlsOperadores" runat="server" AutoGenerateColumns="False" ClientInstanceName="tlsOperadores" Font-Size="Smaller" Height="187px" KeyFieldName="intId" ParentFieldName="intIdPadre" Theme="Youthful" Styles-Header-BackColor="#317D40" ToolTip="Seleccione una entidad" OnCustomCallback="tlsOperadores_CustomCallback">
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

                                        <ClientSideEvents FocusedNodeChanged="function(s, e) {
tacMovimientos.SetActiveTabIndex(0);
pvgAcumulado.SetVisible(true);
dgvInternacional.SetVisible(false);
dgvContenedor.SetVisible(false);
dgvRecaudo.SetVisible(false);
pnlDatos.PerformCallback();
}" Init="function(s, e) {
pnlDatos.PerformCallback();	
}" NodeDblClick="function(s, e) {
tacMovimientos.SetActiveTabIndex(0);
pvgAcumulado.SetVisible(true);
dgvInternacional.SetVisible(false);
dgvContenedor.SetVisible(false);
dgvRecaudo.SetVisible(false);
pnlDatos.PerformCallback();
}" EndCallback="function(s, e) {
tacMovimientos.SetActiveTabIndex(0);
pvgAcumulado.SetVisible(true);
dgvInternacional.SetVisible(false);
dgvContenedor.SetVisible(false);
dgvRecaudo.SetVisible(false);
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
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="#10420F" Text="NOMBRE COMERCIAL"></asp:Label>
                                            </td>
                                            <td></td>
                                            <td></td>
                                            <td>
                                                <asp:Label ID="Label3" runat="server" Font-Bold="True" ForeColor="#10420F" Text="NIT"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label5" runat="server" Font-Bold="True" ForeColor="#10420F" Text="DEPARTAMENTO"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label4" runat="server" Font-Bold="True" ForeColor="#10420F" Text="DIRECCION"></asp:Label>
                                            </td>
                                            <td style="text-align: right;">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <asp:Label ID="lblRazonSocial" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblNit" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblDepto" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblDireccion" runat="server"></asp:Label>
                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td colspan="7">
                                                <dx:ASPxGridView ID="dgvCabecera" runat="server" AutoGenerateColumns="False" ClientInstanceName="dgvCabecera" EnableTheming="True" Font-Size="Smaller" KeyFieldName="ID_REPORTE_DATO_PRINC" OnHtmlRowPrepared="dgvCabecera_HtmlRowPrepared" Theme="Default" Width="100%" OnCustomButtonCallback="dgvCabecera_CustomButtonCallback" OnCustomButtonInitialize="dgvCabecera_CustomButtonInitialize">
                                                    <ClientSideEvents CustomButtonClick="function(s, e) {
	
    switch(e.buttonID) {

    case&nbsp; 'btnHistorial':
    pucHistorial.Show();
    pucHistorial.PerformCallback();
    break;

    case&nbsp; 'btnDetalle':
    s.GetRowValues(e.visibleIndex, 'CITE_GENERADO', function (value) {
    lblTituloDetalle.SetValue('DETALLE DE LA DECLARACION JURADA : '+value);
    });
    pvgAcumulado.PerformCallback();
    dgvInternacional.PerformCallback();
    dgvContenedor.PerformCallback();
    dgvRecaudo.PerformCallback();
    break;

    case  'btnPdf':
    s.GetRowValues(e.visibleIndex, 'ESTADO', function (value) {
    if(value==2){
    alert('No puede ejecutar la función debido a que la declaración se encuentra anulada.');
    }
    //else if(value==1){
    //alert('No puede ejecutar la función debido a que la declaración no esta recepcionada.');
    //}
    else{
    pucCargaArchivo.Show();
    pucCargaArchivo.PerformCallback();
    }
    });
    break;

    case 'btnAdjuntos':
    pucAdjuntos.Show();
    pucAdjuntos.PerformCallback();
    break;

    case&nbsp; 'btnAnular':
    if(confirm('¿Desea anular la Declaracion Jurada? - Una vez realizada la acción no podrá revertir el proceso. ¿Desea continuar?'))
    {
    e.processOnServer = true;
    }
    else{
    e.processOnServer = false; 
    }
    break;

    case&nbsp; 'btnAnulaUor':
    if(confirm('¿Desea anular la Declaracion Jurada? - Una vez realizada la acción no podrá revertir el proceso. ¿Desea continuar?'))
    {
    e.processOnServer = true;
    }
    else{
    e.processOnServer = false; 
    }
    break;

    case&nbsp; 'btnActivaSal':
    if(confirm('¿Desea activar los saldos finales de la Declaracion Juradaseleccionada?'))
    {
    e.processOnServer = true;
    }
    else{
    e.processOnServer = false; 
    }
    break;

   case&nbsp; 'btnAnulaSal':
    if(confirm('¿Desea anular los saldos finales de la Declaracion Jurada seleccionada?'))
    {
    e.processOnServer = true;
    }
    else{
    e.processOnServer = false; 
    }
    break;

    case&nbsp; 'btnRecepcionar':
    if(confirm('¿Desea recepcionar la Declaracion Jurada?'))
    {
    e.processOnServer = true;
    }
    else{
    e.processOnServer = false; 
    }
    break;


    } 

    }" />
                                                    <SettingsContextMenu EnableRowMenu="False">
                                                    </SettingsContextMenu>
                                                    <Templates>
                                                        <EditForm>
                                                        </EditForm>
                                                    </Templates>
                                                    <SettingsPager PageSize="50">
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
                                                                <dx:GridViewCommandColumnCustomButton ID="btnDetalle" Text=" ">
                                                                    <Image IconID="actions_productcomparisons_16x16devav" ToolTip="VerDetalle">
                                                                    </Image>
                                                                </dx:GridViewCommandColumnCustomButton>
                                                                <dx:GridViewCommandColumnCustomButton ID="btnHistorial" Text=" ">
                                                                    <Image IconID="functionlibrary_lookupreference_16x16" ToolTip="VerHistorial">
                                                                    </Image>
                                                                </dx:GridViewCommandColumnCustomButton>
                                                                <dx:GridViewCommandColumnCustomButton ID="btnRecepcionar" Text=" ">
                                                                    <Image IconID="mail_inbox_16x16" ToolTip="Recepcionar">
                                                                    </Image>
                                                                </dx:GridViewCommandColumnCustomButton>
                                                                <dx:GridViewCommandColumnCustomButton ID="btnPdf" Text=" ">
                                                                    <Image IconID="mail_attachmentobject_16x16gray" ToolTip="Adjuntar Archivo">
                                                                    </Image>
                                                                </dx:GridViewCommandColumnCustomButton>
                                                                <dx:GridViewCommandColumnCustomButton ID="btnAdjuntos" Text=" ">
                                                                    <Image IconID="mail_attach_16x16" ToolTip="Ver Archivos Adjuntos">
                                                                    </Image>
                                                                </dx:GridViewCommandColumnCustomButton>
                                                                <dx:GridViewCommandColumnCustomButton ID="btnAnular" Text=" ">
                                                                    <Image IconID="reports_deleteheader_16x16" ToolTip="Anular Declaración Jurada">
                                                                    </Image>
                                                                </dx:GridViewCommandColumnCustomButton>
                                                                <dx:GridViewCommandColumnCustomButton ID="btnAnulaUor" Text=" ">
                                                                    <Image IconID="actions_deletelist_16x16" ToolTip="Anular DDJJ con el Usuario de Origen">
                                                                    </Image>
                                                                </dx:GridViewCommandColumnCustomButton>
                                                                <dx:GridViewCommandColumnCustomButton ID="btnActivaSal" Text=" ">
                                                                    <Image IconID="spreadsheet_showdetail_16x16" ToolTip="Activar Saldos Finales">
                                                                    </Image>
                                                                </dx:GridViewCommandColumnCustomButton>
                                                                <dx:GridViewCommandColumnCustomButton ID="btnAnulaSal" Text=" ">
                                                                    <Image IconID="spreadsheet_ungrouprows_16x16" ToolTip="Anula Saldos Finales">
                                                                    </Image>
                                                                </dx:GridViewCommandColumnCustomButton>
                                                            </CustomButtons>
                                                        </dx:GridViewCommandColumn>
                                                        <dx:GridViewDataTextColumn Caption="USUARIO" FieldName="APP_USUARIO" ShowInCustomizationForm="True" VisibleIndex="3">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="OBSERVACIONES" FieldName="OBSERVACIONES" ShowInCustomizationForm="True" VisibleIndex="4">
                                                            <Settings AutoFilterCondition="Contains" />
                                                            <CellStyle HorizontalAlign="Right">
                                                            </CellStyle>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="ESTADO" FieldName="ESTADO" ShowInCustomizationForm="True" VisibleIndex="5" Visible="False">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="CODIGO" FieldName="CITE_GENERADO" ShowInCustomizationForm="True" VisibleIndex="2">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="FECHA" FieldName="FECHA_REPORTE" ShowInCustomizationForm="True" VisibleIndex="1" Visible="False">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="ESTADO" FieldName="StrEstado" ShowInCustomizationForm="True" VisibleIndex="6">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="ID_REPORTE_DATO_PRINC" FieldName="ID_REPORTE_DATO_PRINC" ShowInCustomizationForm="True" Visible="False" VisibleIndex="8">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="FECHA" FieldName="StrFecha" ShowInCustomizationForm="True" VisibleIndex="0">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="DIRECCION" FieldName="DIRECCION" ShowInCustomizationForm="True" Visible="False" VisibleIndex="9">
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
                                            <td colspan="7">&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">&nbsp;</td>
                                            <td style="text-align: right; width: 10%;"><dx:ASPxLabel runat="server" ID="lblImprimirCertificado" Text="Numero de Certificado" Visible="false"></dx:ASPxLabel></td>
                                            <td style="text-align: right; width: 10%;"><dx:ASPxTextBox runat="server" ID="txtNumCertificado" Visible="false"></dx:ASPxTextBox></td>
                                            <td style="text-align: right; width: 10%;">
                                                <dx:ASPxButton ID="btnReimprimirCertificado" runat="server" Font-Bold="True" ForeColor="Maroon" Text="Imrpimir Certificado" Theme="Metropolis" Width="100%" OnClick="btnImprimirCertificado_Click" Visible="false">
                                                    <Image IconID="print_printer_16x16">
                                                    </Image>
                                                </dx:ASPxButton>
                                            </td>
                                            <td style="text-align: right; width: 10%;">
                                                <dx:ASPxButton ID="btnImprimirFormulario" runat="server" Font-Bold="True" ForeColor="Maroon" Text="Reimprimir Formulario" Theme="Metropolis" Width="100%" OnClick="btnImprimirFormulario_Click" Visible="false">
                                                    <Image IconID="print_printer_16x16">
                                                    </Image>
                                                </dx:ASPxButton>
                                            </td>                                            
                                        </tr>                                        
                                        <tr>
                                            <td colspan="7">
                                                <dx:ASPxLabel ID="lblTituloDetalle" runat="server" ClientInstanceName="lblTituloDetalle" Font-Bold="True" ForeColor="#FF8800" Text="DETALLE DE LA DECLARACION JURADA" Width="100%">
                                                    <BorderTop BorderStyle="Solid" />
                                                </dx:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr style="width: 100%">
                                            <td colspan="6" style="width: 80%">
                                                <dx:ASPxTabControl ID="tacMovimientos" runat="server" ActiveTabIndex="0" ClientInstanceName="tacMovimientos" EnableTheming="True" Theme="Metropolis" Width="100%">
                                                    <Tabs>
                                                        <dx:Tab Name="tcpResumen" Text="Mov. Acumulado">
                                                        </dx:Tab>
                                                        <dx:Tab Name="tcpEspecial" Text="Mov. Internacional" ClientVisible="False">
                                                        </dx:Tab>
                                                        <dx:Tab Name="tcpContenedor" Text="Mov. Fuera de Ala" ClientVisible="False">
                                                        </dx:Tab>
                                                        <dx:Tab ClientVisible="False" Name="tcpRecaudo" Text="Recaudo Externo">
                                                        </dx:Tab>
                                                    </Tabs>
                                                    <ClientSideEvents ActiveTabChanged="function(s, e) {

pvgAcumulado.SetVisible(false);
dgvInternacional.SetVisible(false);
dgvContenedor.SetVisible(false);
dgvRecaudo.SetVisible(false);

switch(tacMovimientos.GetActiveTabIndex()) {
&nbsp;&nbsp;&nbsp; case&nbsp;1:
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; dgvInternacional.SetVisible(true);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; break;
&nbsp;&nbsp;&nbsp; case&nbsp;2:
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; dgvContenedor.SetVisible(true);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; break;
    case&nbsp;3:
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; dgvRecaudo.SetVisible(true);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; break;
&nbsp;&nbsp;&nbsp; default:
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;pvgAcumulado.SetVisible(true);
} 

}" />
                                                </dx:ASPxTabControl>
                                            </td>
                                            <td style="text-align: right; width: 10%;">
                                                <dx:ASPxButton ID="btnReimprimir" runat="server" Font-Bold="True" ForeColor="Maroon" Text="Reimprimir" Theme="Metropolis" Width="100%" OnClick="btnReimprimir_Click">
                                                    <Image IconID="print_printer_16x16">
                                                    </Image>
                                                </dx:ASPxButton>
                                            </td>                                            
                                        </tr>
                                    
                                        <tr>
                                            <td colspan="7">
                                                <dx:ASPxGridView ID="dgvInternacional" runat="server" AutoGenerateColumns="False" ClientInstanceName="dgvInternacional" EnableTheming="True" Font-Size="Smaller" KeyFieldName="LngId" OnCustomCallback="dgvInternacional_CustomCallback" OnHtmlRowPrepared="dgvInternacional_HtmlRowPrepared" Theme="Default" Width="100%">
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
                                                        <dx:GridViewDataTextColumn Caption="NIT" FieldName="LngNit" ShowInCustomizationForm="True" VisibleIndex="3">
                                                            <Settings AutoFilterCondition="Contains" />
                                                            <CellStyle HorizontalAlign="Right">
                                                            </CellStyle>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="N.F. PROPIO" FieldName="LngNoFactProp" ShowInCustomizationForm="True" ToolTip="Número de Factura Propio" VisibleIndex="6">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="IMP. EXTERNO" FieldName="DecImporteExt" ShowInCustomizationForm="True" ToolTip="Importe Externo" VisibleIndex="7">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="IMP. PROPIO" FieldName="DecImporteProp" ShowInCustomizationForm="True" ToolTip="Importe Propio" VisibleIndex="8">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="FECHA" FieldName="StrFecha" ShowInCustomizationForm="True" VisibleIndex="1">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="EST." FieldName="StrEstado" ShowInCustomizationForm="True" ToolTip="Estado" VisibleIndex="18">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="N.F. EXTERNO" FieldName="LngNoFactExt" ShowInCustomizationForm="True" ToolTip="Número de Factura Externo" VisibleIndex="5">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="PROD." FieldName="StrCodProducto" ShowInCustomizationForm="True" ToolTip="Producto" VisibleIndex="4">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="ID" FieldName="LngId" ShowInCustomizationForm="True" Visible="False" VisibleIndex="0">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="PRECIO" FieldName="DecPrecioExt" ShowInCustomizationForm="True" VisibleIndex="9">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="CANTIDAD" FieldName="DecCantidadProp" ShowInCustomizationForm="True" VisibleIndex="13">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="TIPO MOV." FieldName="StrTipoMovimiento" ShowInCustomizationForm="True" VisibleIndex="20">
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                    <TotalSummary>
                                                        <dx:ASPxSummaryItem FieldName="DecImporteProp" ShowInColumn="IMP. PROPIO" SummaryType="Sum" Tag="impP" />
                                                        <dx:ASPxSummaryItem FieldName="DecImporteExt" ShowInColumn="IMP. EXTERNO" SummaryType="Sum" Tag="impE" />
                                                        <dx:ASPxSummaryItem FieldName="DecCantidadExt" ShowInColumn="CANTIDAD" SummaryType="Sum" Tag="cantidad" />
                                                    </TotalSummary>
                                                </dx:ASPxGridView>
                                                <dx:ASPxPivotGrid ID="pvgAcumulado" runat="server" ClientIDMode="AutoID" ClientInstanceName="pvgAcumulado" EnableTheming="True" Font-Size="Smaller" OnCustomCallback="pvgAcumulado_CustomCallback" OnCustomCellStyle="pvgAcumulado_CustomCellStyle" Theme="Default" Width="100%">
                                                    <Fields>
                                                        <dx:PivotGridField ID="fieldStrProducto" Area="RowArea" AreaIndex="0" Caption="PRODUCTO" FieldName="StrProducto">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldStrCodProducto" Area="RowArea" AreaIndex="1" Caption="PRODUCTO" FieldName="StrCodProducto">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldStrUnidadMedida" Area="RowArea" AreaIndex="2" Caption="UNIDAD MEDIDA" FieldName="StrUnidadMedida" Width="20">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldStrTipoInfraestructura" Area="RowArea" AreaIndex="3" Caption="TIPO DE INFRAESTRUCTURA" FieldName="StrTipoInfraestructura">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldStrInfraestructura" Area="RowArea" AreaIndex="4" Caption="INFRAESTRUCTURA" FieldName="StrInfraestructura">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldStrFecha" Area="RowArea" AreaIndex="5" Caption="FECHA" FieldName="StrFecha">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecSaldoInicial" Area="DataArea" AreaIndex="0" Caption="SALDO INICIAL" CellFormat-FormatType="Custom" FieldName="DecSaldoInicial" Tag="salIni" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecIng01" Area="DataArea" AreaIndex="0" Caption="DecIng01" CellFormat-FormatType="Custom" FieldName="DecIng01" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecIng02" Area="DataArea" AreaIndex="0" Caption="DecIng02" CellFormat-FormatType="Custom" FieldName="DecIng02" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecIng03" Area="DataArea" AreaIndex="0" Caption="DecIng03" CellFormat-FormatType="Custom" FieldName="DecIng03" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecIng04" Area="DataArea" AreaIndex="0" Caption="DecIng04" CellFormat-FormatType="Custom" FieldName="DecIng04" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecIng05" Area="DataArea" AreaIndex="0" Caption="DecIng05" CellFormat-FormatType="Custom" FieldName="DecIng05" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecIng06" Area="DataArea" AreaIndex="0" Caption="DecIng06" CellFormat-FormatType="Custom" FieldName="DecIng06" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecIng07" Area="DataArea" AreaIndex="0" Caption="DecIng07" CellFormat-FormatType="Custom" FieldName="DecIng07" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecIng08" Area="DataArea" AreaIndex="0" Caption="DecIng08" CellFormat-FormatType="Custom" FieldName="DecIng08" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecIng09" Area="DataArea" AreaIndex="0" Caption="DecIng09" CellFormat-FormatType="Custom" FieldName="DecIng09" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecIng10" Area="DataArea" AreaIndex="0" Caption="DecIng10" CellFormat-FormatType="Custom" FieldName="DecIng10" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecIng11" Area="DataArea" AreaIndex="0" Caption="DecIng11" CellFormat-FormatType="Custom" FieldName="DecIng11" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecIng12" Area="DataArea" AreaIndex="0" Caption="DecIng12" CellFormat-FormatType="Custom" FieldName="DecIng12" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecIng13" Area="DataArea" AreaIndex="0" Caption="DecIng13" CellFormat-FormatType="Custom" FieldName="DecIng13" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecIng14" Area="DataArea" AreaIndex="0" Caption="DecIng14" CellFormat-FormatType="Custom" FieldName="DecIng14" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecIng15" Area="DataArea" AreaIndex="0" Caption="DecIng15" CellFormat-FormatType="Custom" FieldName="DecIng15" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecIng16" Area="DataArea" AreaIndex="0" Caption="DecIng16" CellFormat-FormatType="Custom" FieldName="DecIng16" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecIng17" Area="DataArea" AreaIndex="0" Caption="DecIng17" CellFormat-FormatType="Custom" FieldName="DecIng17" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecIng18" Area="DataArea" AreaIndex="0" Caption="DecIng18" CellFormat-FormatType="Custom" FieldName="DecIng18" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecIng19" Area="DataArea" AreaIndex="0" Caption="DecIng19" CellFormat-FormatType="Custom" FieldName="DecIng19" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecIng20" Area="DataArea" AreaIndex="0" Caption="DecIng20" CellFormat-FormatType="Custom" FieldName="DecIng20" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecTotalIng" Area="DataArea" AreaIndex="0" Caption="TOTAL INGRESOS" CellFormat-FormatType="Custom" FieldName="DecTotalIng" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecSal01" Area="DataArea" AreaIndex="0" Caption="DecSal01" CellFormat-FormatType="Custom" FieldName="DecSal01" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecSal02" Area="DataArea" AreaIndex="0" Caption="DecSal02" CellFormat-FormatType="Custom" FieldName="DecSal02" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecSal03" Area="DataArea" AreaIndex="0" Caption="DecSal03" CellFormat-FormatType="Custom" FieldName="DecSal03" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecSal04" Area="DataArea" AreaIndex="0" Caption="DecSal04" CellFormat-FormatType="Custom" FieldName="DecSal04" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecSal05" Area="DataArea" AreaIndex="0" Caption="DecSal05" CellFormat-FormatType="Custom" FieldName="DecSal05" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecSal06" Area="DataArea" AreaIndex="0" Caption="DecSal06" CellFormat-FormatType="Custom" FieldName="DecSal06" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecSal07" Area="DataArea" AreaIndex="0" Caption="DecSal07" CellFormat-FormatType="Custom" FieldName="DecSal07" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecSal08" Area="DataArea" AreaIndex="0" Caption="DecSal08" CellFormat-FormatType="Custom" FieldName="DecSal08" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecSal09" Area="DataArea" AreaIndex="0" Caption="DecSal09" CellFormat-FormatType="Custom" FieldName="DecSal09" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecSal10" Area="DataArea" AreaIndex="0" Caption="DecSal10" CellFormat-FormatType="Custom" FieldName="DecSal10" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecSal11" Area="DataArea" AreaIndex="0" Caption="DecSal11" CellFormat-FormatType="Custom" FieldName="DecSal11" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecSal12" Area="DataArea" AreaIndex="0" Caption="DecSal12" CellFormat-FormatType="Custom" FieldName="DecSal12" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecSal13" Area="DataArea" AreaIndex="0" Caption="DecSal13" CellFormat-FormatType="Custom" FieldName="DecSal13" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecSal14" Area="DataArea" AreaIndex="0" Caption="DecSal14" CellFormat-FormatType="Custom" FieldName="DecSal14" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecSal15" Area="DataArea" AreaIndex="0" Caption="DecSal15" CellFormat-FormatType="Custom" FieldName="DecSal15" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecSal16" Area="DataArea" AreaIndex="0" Caption="DecSal16" CellFormat-FormatType="Custom" FieldName="DecSal16" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecSal17" Area="DataArea" AreaIndex="0" Caption="DecSal17" CellFormat-FormatType="Custom" FieldName="DecSal17" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecSal18" Area="DataArea" AreaIndex="0" Caption="DecSal18" CellFormat-FormatType="Custom" FieldName="DecSal18" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecSal19" Area="DataArea" AreaIndex="0" Caption="DecSal19" CellFormat-FormatType="Custom" FieldName="DecSal19" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecSal20" Area="DataArea" AreaIndex="0" Caption="DecSal20" CellFormat-FormatType="Custom" FieldName="DecSal20" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecTotalSal" Area="DataArea" AreaIndex="0" Caption="TOTAL SALIDAS" CellFormat-FormatType="Custom" FieldName="DecTotalSal" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecSaldoFinal" Area="DataArea" AreaIndex="0" Caption="SALDO FINAL" CellFormat-FormatType="Custom" FieldName="DecSaldoFinal" Visible="False">
                                                        </dx:PivotGridField>
                                                    </Fields>
                                                    <OptionsPager AlwaysShowPager="True" NumericButtonCount="5" RowsPerPage="500">
                                                    </OptionsPager>
                                                    <OptionsData AutoExpandGroups="False" />
                                                    <Styles>
                                                        <HeaderStyle Wrap="True" />
                                                    </Styles>
                                                </dx:ASPxPivotGrid>
                                                <dx:ASPxGridView ID="dgvContenedor" runat="server" AutoGenerateColumns="False" ClientInstanceName="dgvContenedor" EnableTheming="True" Font-Size="Smaller" KeyFieldName="LngId" OnHtmlRowPrepared="dgvContenedor_HtmlRowPrepared" Theme="Default" Width="100%" OnCustomCallback="dgvContenedor_CustomCallback">
                                                    <ClientSideEvents Init="function(s, e) {
	    s.SetVisible(false);
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
                                                        <dx:GridViewDataTextColumn Caption="StrSubIdentificador" FieldName="StrSubIdentificador" ShowInCustomizationForm="True" Visible="False" VisibleIndex="5">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="TIPO CONT." FieldName="StrTipoContenedor" ShowInCustomizationForm="True" ToolTip="Tipo de Contenedor Utilizado" VisibleIndex="2">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="NO. AUTORIZ." FieldName="LngNoAut" ShowInCustomizationForm="True" ToolTip="Número de Autorización de la Factura" VisibleIndex="8">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="NO. FACTURA" FieldName="StrNoTransaccion" ShowInCustomizationForm="True" VisibleIndex="9">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="COD. CTRL." FieldName="StrCodCtrl" ShowInCustomizationForm="True" ToolTip="Código de Control" VisibleIndex="10">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="FECHA" FieldName="StrFecha" ShowInCustomizationForm="True" VisibleIndex="1">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="DecIdProducto" FieldName="DecIdProducto" ShowInCustomizationForm="True" ToolTip="Estado del Movimiento" Visible="False" VisibleIndex="13">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="RAZON SOCIAL" FieldName="StrRazonSocial" ShowInCustomizationForm="True" VisibleIndex="7">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="NIT" FieldName="LngNit" ShowInCustomizationForm="True" VisibleIndex="6">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="ID" FieldName="LngId" ShowInCustomizationForm="True" Visible="False" VisibleIndex="0">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="AUT. SUST. CTRL." FieldName="AutSustCtrl" ShowInCustomizationForm="True" ToolTip="Autorización de Sustancias Controladas" VisibleIndex="11">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="AUT. ANH" FieldName="AutAnh" ShowInCustomizationForm="True" ToolTip="Autorización de ANH" VisibleIndex="12">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="IDENTIFICADOR" FieldName="StrIdentificador" ShowInCustomizationForm="True" VisibleIndex="4">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="StrProducto" FieldName="StrProducto" ShowInCustomizationForm="True" Visible="False" VisibleIndex="14">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="PROD." FieldName="StrCodProducto" ShowInCustomizationForm="True" ToolTip="Producto" VisibleIndex="3">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="StrUnidadMedida" FieldName="StrUnidadMedida" ShowInCustomizationForm="True" Visible="False" VisibleIndex="15">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="EST." FieldName="StrEstado" ShowInCustomizationForm="True" ToolTip="Estado" VisibleIndex="16">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="DecIdEstado" FieldName="DecIdEstado" ShowInCustomizationForm="True" Visible="False" VisibleIndex="17">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="CANTIDAD" FieldName="DecCantidad" ShowInCustomizationForm="True" VisibleIndex="18">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="PRECIO" FieldName="DecPrecio" ShowInCustomizationForm="True" VisibleIndex="19">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="IMPORTE" FieldName="DecImporte" ShowInCustomizationForm="True" VisibleIndex="20">
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                    <TotalSummary>
                                                        <dx:ASPxSummaryItem FieldName="DecCantidad" ShowInColumn="DecCantidad" SummaryType="Sum" Tag="cant" />
                                                        <dx:ASPxSummaryItem FieldName="DecImporte" ShowInColumn="DecImporte" SummaryType="Sum" Tag="imp" />
                                                    </TotalSummary>
                                                </dx:ASPxGridView>
                                                <dx:ASPxGridView ID="dgvRecaudo" runat="server" AutoGenerateColumns="False" ClientInstanceName="dgvRecaudo" EnableTheming="True" Font-Size="Smaller" KeyFieldName="LngId" Theme="Default" Width="100%" OnCustomCallback="dgvRecaudo_CustomCallback" OnHtmlRowCreated="dgvRecaudo_HtmlRowCreated">
                                                    <ClientSideEvents Init="function(s, e) {
	    s.SetVisible(false);
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
                                                        <dx:GridViewDataTextColumn Caption="PRODUCTO" FieldName="PRODUCTO" ShowInCustomizationForm="True" VisibleIndex="3">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="NRO. FACTURAS" FieldName="CANTIDAD_FACTURA" ShowInCustomizationForm="True" VisibleIndex="6">
                                                            <Settings AutoFilterCondition="Contains" />
                                                            <CellStyle HorizontalAlign="Right">
                                                            </CellStyle>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="TIPO RECAUDO" FieldName="TIPO_TRANSACCION" ShowInCustomizationForm="True" VisibleIndex="1">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="IMPORTE TOTAL" FieldName="IMPORTE_TOTAL" ShowInCustomizationForm="True" ToolTip="Producto" VisibleIndex="7">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="ID_TIPO_TRANSACCION" FieldName="ID_TIPO_TRANSACCION" ShowInCustomizationForm="True" Visible="False" VisibleIndex="0">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="ID_PRODUCTO" FieldName="ID_PRODUCTO" ShowInCustomizationForm="True" Visible="False" VisibleIndex="2">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="ID_UNIDAD_MEDIDA" FieldName="ID_UNIDAD_MEDIDA" ShowInCustomizationForm="True" Visible="False" VisibleIndex="4">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="UNIDAD MEDIDA" FieldName="UNIDAD_MEDIDA" ShowInCustomizationForm="True" VisibleIndex="5">
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                    <TotalSummary>
                                                        <dx:ASPxSummaryItem DisplayFormat="{0}" FieldName="CANTIDAD_FACTURA" ShowInColumn="CANTIDAD_FACTURA" SummaryType="Sum" />
                                                        <dx:ASPxSummaryItem DisplayFormat="{0}" FieldName="IMPORTE_TOTAL" ShowInColumn="IMPORTE_TOTAL" SummaryType="Sum" />
                                                    </TotalSummary>
                                                </dx:ASPxGridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="7">
                                                
                                                
                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="7">
                                                <dx:ASPxButton ID="btnAmpliarPeriodo" runat="server" Font-Bold="True" ForeColor="#003300" Text="Ampliar Periodo" Theme="Metropolis" Visible="False" Width="150px">
                                                    <Image IconID="actions_converttorange_16x16">
                                                    </Image>
                                                </dx:ASPxButton>
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



<dx:ASPxPopupControl ID="pucHistorial" runat="server" AllowDragging="True" ClientInstanceName="pucHistorial" EnableTheming="True" HeaderText="Historial de la Declaración Jurada" Height="93px" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Above" Theme="Metropolis" Width="650px" OnWindowCallback="pucHistorial_WindowCallback">
    <HeaderImage IconID="mail_news_32x32">
    </HeaderImage>
    <HeaderStyle>
    <BorderBottom BorderStyle="Solid" />
    </HeaderStyle>
    <ContentCollection>
        <dx:PopupControlContentControl runat="server">
            <table class="dxflInternalEditorTable_Youthful">
                <tr>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel39" runat="server" Text="Declaración Jurada:" Font-Bold="True" ForeColor="#333333">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="txtCiteDj" runat="server" ReadOnly="True" Width="300px">
                            <MaskSettings PromptChar=" " />
                            <ReadOnlyStyle BackColor="#FFE1C4">
                            </ReadOnlyStyle>
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        
                        <dx:ASPxGridView ID="dgvHistorico" runat="server" AutoGenerateColumns="False" ClientInstanceName="dgvHistorico" EnableTheming="True" Font-Size="Smaller" KeyFieldName="APP_FECHA_REGISTRO" Theme="Default" Width="100%" OnHtmlRowPrepared="dgvHistorico_HtmlRowPrepared">
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
                                <dx:GridViewDataTextColumn Caption="FECHA" FieldName="APP_FECHA_REGISTRO" ShowInCustomizationForm="True" Visible="False" VisibleIndex="2">
                                    <Settings AutoFilterCondition="Contains" />
                                    <CellStyle HorizontalAlign="Right">
                                    </CellStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="USUARIO" FieldName="APP_USUARIO" ShowInCustomizationForm="True" VisibleIndex="6">
                                    <Settings AutoFilterCondition="Contains" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="OBSERVACIONES" FieldName="OBSERVACIONES" ShowInCustomizationForm="True" ToolTip="Tipo de Crédito Fiscal" VisibleIndex="7">
                                    <Settings AutoFilterCondition="Contains" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="ESTADO" FieldName="ESTADO" ShowInCustomizationForm="True" Visible="False" VisibleIndex="5">
                                    <Settings AutoFilterCondition="Contains" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="FECHA" FieldName="strFecha" ShowInCustomizationForm="True" VisibleIndex="1">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="ESTADO" FieldName="strEstado" ShowInCustomizationForm="True" VisibleIndex="4">
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























<dx:ASPxPopupControl ID="pucCargaArchivo" runat="server" AllowDragging="True" ClientInstanceName="pucCargaArchivo" EnableTheming="True" HeaderText="Cargar Archivo Adjunto" Height="93px" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Above" Theme="Metropolis" Width="500px" OnWindowCallback="pucCargaArchivo_WindowCallback">
    <HeaderImage IconID="mail_attachment_32x32">
    </HeaderImage>
    <ClientSideEvents CloseButtonClick="function(s, e) {
	pnlDatos.PerformCallback();
}" />
    <HeaderStyle>
    <BorderBottom BorderStyle="Solid" />
    </HeaderStyle>
    <ContentCollection>
        <dx:PopupControlContentControl runat="server">
            <table class="dxflInternalEditorTable_Youthful">
                <tr>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel41" runat="server" Font-Bold="True" ForeColor="#333333" Text="Declaración Jurada:">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="txtPcCiteDj" runat="server" ReadOnly="True" Width="300px">
                            <MaskSettings PromptChar=" " />
                            <ReadOnlyStyle BackColor="#FFE1C4">
                            </ReadOnlyStyle>
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel42" runat="server" Font-Bold="True" ForeColor="#333333" Text="Observaciones :">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        

                        <dx:ASPxTextBox ID="txtPcObservacion" runat="server" ClientInstanceName="txtPcObservacion" Width="300px">
                            <MaskSettings PromptChar=" " />
                            <ReadOnlyStyle BackColor="#DEE8DB">
                            </ReadOnlyStyle>
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel40" runat="server" Font-Bold="True" ForeColor="#333333" Text="Archivo (PDF) :">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxUploadControl ID="uctCargaDj" runat="server" ClientInstanceName="uctCargaDj" OnFileUploadComplete="uctCargaDj_FileUploadComplete" ShowProgressPanel="True" ShowUploadButton="True" Theme="Default" UploadMode="Auto" Width="300px" FileUploadMode="OnPageLoad" ToolTip="Seleccione el archivo PDF">
                            <ValidationSettings AllowedFileExtensions=".pdf">
                            </ValidationSettings>
                            <ClientSideEvents TextChanged="function(s, e) {
//s.Upload();
}" />
                            <UploadButton Text="_Carga Archivo   ">
                                <Image IconID="navigation_previous_16x16">
                                </Image>
                            </UploadButton>
                        </dx:ASPxUploadControl>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>

































<dx:ASPxPopupControl ID="pucAdjuntos" runat="server" AllowDragging="True" ClientInstanceName="pucAdjuntos" EnableTheming="True" HeaderText="Archivos Adjuntos" Height="93px" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Above" Theme="Metropolis" Width="650px" OnWindowCallback="pucAdjuntos_WindowCallback">
    <HeaderImage IconID="mail_attachment_32x32">
    </HeaderImage>
    <HeaderStyle>
    <BorderBottom BorderStyle="Solid" />
    </HeaderStyle>
    <ContentCollection>
        <dx:PopupControlContentControl runat="server">
            <table class="dxflInternalEditorTable_Youthful">
                <tr>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel43" runat="server" Text="Declaración Jurada:" Font-Bold="True" ForeColor="#333333">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="txtPaCiteDj" runat="server" ReadOnly="True" Width="300px">
                            <MaskSettings PromptChar=" " />
                            <ReadOnlyStyle BackColor="#FFE1C4">
                            </ReadOnlyStyle>
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        
                        <dx:ASPxGridView ID="dgvAdjuntos" runat="server" AutoGenerateColumns="False" ClientInstanceName="dgvAdjuntos" EnableTheming="True" Font-Size="Smaller" KeyFieldName="APP_FECHA_REGISTRO" Theme="Default" Width="100%" OnCustomButtonCallback="dgvAdjuntos_CustomButtonCallback" OnRowCommand="dgvAdjuntos_RowCommand" OnHtmlRowPrepared="dgvAdjuntos_HtmlRowPrepared">
                            <ClientSideEvents CustomButtonClick="function(s, e) {
	
switch(e.buttonID) {

case&nbsp; 'btnDescargar':
e.processOnServer = true;
break;

case&nbsp; 'btnEliminar':
if(confirm('¿Desea eliminar el archivo adjunto? - Una vez realizada la acción no podrá revertir el proceso. ¿Desea continuar?'))
{
e.processOnServer = true;
}
else{
e.processOnServer = false; 
}
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
                                <dx:GridViewCommandColumn ShowInCustomizationForm="True" VisibleIndex="22" ToolTip="Funciones">
                                    <CustomButtons>
                                        <dx:GridViewCommandColumnCustomButton ID="btnEliminar" Text="Eliminar">
                                        </dx:GridViewCommandColumnCustomButton>
                                    </CustomButtons>
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn Caption="OBSERVACIONES" FieldName="OBSERVACION" ShowInCustomizationForm="True" VisibleIndex="4">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="FECHA" FieldName="APP_FECHA_REGISTRO" ShowInCustomizationForm="True" VisibleIndex="0">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="USUARIO" FieldName="APP_ID_USUARIO" ShowInCustomizationForm="True" VisibleIndex="1">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="ID_DOCUMENTO_DIGITAL" FieldName="ID_DOCUMENTO_DIGITAL" ShowInCustomizationForm="True" VisibleIndex="7" Visible="False">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="DOCUMENTO" FieldName="DOCUMENTO" ShowInCustomizationForm="True" Visible="False" VisibleIndex="9">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="#" ShowInCustomizationForm="True" VisibleIndex="6" ToolTip="Funciones">
                                    <DataItemTemplate>
                                        <asp:LinkButton ID="lnkDescargar" runat="server" CommandName="lnkDescargar" Font-Size="XX-Small">Descargar</asp:LinkButton>
                                    </DataItemTemplate>
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

























































