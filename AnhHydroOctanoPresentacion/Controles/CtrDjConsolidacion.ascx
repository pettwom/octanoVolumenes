<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtrDjConsolidacion.ascx.cs" Inherits="AnhHydroOctanoPresentacion.Controles.CtrDjConsolidacion" %>

<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>

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
    .auto-style13 {
        width: 103%;
    }
    .auto-style16 {
        width: 220px;
    }
    .auto-style18 {
        width: 108px;
    }
    .auto-style19 {
        width: 88px;
    }
    .auto-style20 {
        width: 103%;
        height: 27px;
    }
    .auto-style21 {
        width: 108px;
        height: 27px;
    }
    .auto-style22 {
        width: 220px;
        height: 27px;
    }
    .alert-box {
    color:#555;
    border-radius:10px;
    font-family:Tahoma,Geneva,Arial,sans-serif;font-size:11px;
    padding:10px 10px 10px 36px;
    margin:10px;
}
    
    .auto-style23 {
        height: 18px;
    }
    
    .auto-style14 {
        width: 81px;
    }
        
    .auto-style24 {
        height: 54px;
    }
        
</style>

<div class="col-md-12">
    <table style="width: 100%">
        <tr>
            <td style="text-align: center; padding-bottom: 1px" class="auto-style24">
                <dx:ASPxRoundPanel runat="server" ShowHeader="False" Width="100%" ID="pnlTitulo">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <dx:ASPxLabel runat="server" ForeColor="#367B2F" ID="lblTitulo" Font-Bold="True" Font-Names="Rockwell" Font-Size="Larger" Text="DECLARACION JURADA DE CONSOLIDACION CON ENTIDADES EXTERNAS"></dx:ASPxLabel>
                        </dx:PanelContent>
                    </PanelCollection>
                    <Border BorderStyle="Ridge" BorderWidth="1px" BorderColor="#003300"></Border>
                </dx:ASPxRoundPanel>
            </td>
        </tr>
    </table>
    <dx:ASPxSplitter ID="SplEntorno" runat="server" Height="100%" ClientInstanceName="SplEntorno">
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
                                <td>
                                    <dx:ASPxCallbackPanel ID="pnlCabecera" runat="server" ClientInstanceName="pnlCabecera" OnCallback="pnlCabecera_Callback" Width="100%">
                                        <PanelCollection>
                                            <dx:PanelContent runat="server">
                                                
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxLabel ID="ASPxLabel57" runat="server" ForeColor="#003300" Text="Entidad Externa :">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxComboBox ID="cmbEntExterna" runat="server" ClientInstanceName="cmbEntidadExt" Theme="Metropolis" Width="160px">
                                                                <Items>
                                                                    <dx:ListEditItem Text="RECAUDO SUIC" Value="EGCSES" />
                                                                </Items>
                                                                <ClearButton Visibility="Auto">
                                                                </ClearButton>
                                                            </dx:ASPxComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxButton ID="btnDescargar" runat="server" Font-Bold="True" OnClick="btnDescargar_Click" Text="Descargar Plantilla" ToolTip="Descarga la plantilla del tipo de operación seleccionado" Width="160px">
                                                                <Image IconID="export_exporttoxlsx_16x16">
                                                                </Image>
                                                            </dx:ASPxButton>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-top: 5px">
                                                            <dx:ASPxLabel ID="ASPxLabel56" runat="server" ForeColor="#003300" Text="Periodo :">
                                                            </dx:ASPxLabel>
                                                            <dx:ASPxComboBox ID="cmbPeriodo" runat="server" ClientInstanceName="cmbPeriodo" EnableTheming="True" Font-Bold="True" Theme="Metropolis" ToolTip="Seleccione un Periodo" Width="160px">
                                                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
	pnlDatos.PerformCallback();
}" />
                                                                <Items>
                                                                    <dx:ListEditItem Text="05/2014 - 12/2014" Value="05/2014-12/2014" />
                                                                    <dx:ListEditItem Text="01/2015 - 12/2015" Value="01/2015-12/2015" />
                                                                    <dx:ListEditItem Text="01/2016 - 12/2016" Value="01/2016-12/2016" />
                                                                    <dx:ListEditItem Text="01/2017 - 12/2017" Value="01/2017-12/2017" />
                                                                    <dx:ListEditItem Text="01/2018 - 06/2018" Value="01/2018-06/2018" />
                                                                </Items>
                                                                <ClearButton Visibility="Auto">
                                                                </ClearButton>
                                                                <CaptionStyle Font-Bold="False">
                                                                </CaptionStyle>
                                                                <DisabledStyle Font-Bold="False">
                                                                </DisabledStyle>
                                                            </dx:ASPxComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxButton ID="btnConsultar" runat="server" AutoPostBack="False" Font-Bold="True" ForeColor="Maroon" Text="Cargar Información" Theme="Metropolis" Width="160px">
                                                                <ClientSideEvents Click="function(s, e) {
pnlDatos.PerformCallback();
}" />
                                                                <Image IconID="actions_download_16x16">
                                                                </Image>
                                                            </dx:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>

                                            </dx:PanelContent>
                                        </PanelCollection>
                                    </dx:ASPxCallbackPanel>
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
                                    <dx:ASPxTreeList ID="tlsOperadores" runat="server" AutoGenerateColumns="False" Font-Size="Smaller" Height="187px" KeyFieldName="intId" ParentFieldName="intIdPadre" Theme="Youthful" Styles-Header-BackColor="#317D40" ToolTip="Seleccione un proyecto" ClientInstanceName="tlsOperadores" OnCustomCallback="tlsOperadores_CustomCallback">
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

                                        <ClientSideEvents Init="function(s, e) {
//pnlDatos.PerformCallback();
}" NodeDblClick="function(s, e) {
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
                            <ClientSideEvents EndCallback="function(s, e) {
LoadingPanel.Hide();
}" BeginCallback="function(s, e) {
LoadingPanel.Show();
}" />
                            <PanelCollection>
                                <dx:PanelContent runat="server">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td colspan="3" style="text-align: center;">
                                                <dx:ASPxRoundPanel ID="pnlMensaje" runat="server" ShowHeader="False" Visible="False" Width="100%">
                                                    <PanelCollection>
                                                        <dx:PanelContent runat="server">
                                                            <dx:ASPxLabel ID="lblMensajeError" runat="server" ForeColor="#666666" Width="100%">
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
                                                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Rockwell" Font-Size="Medium" ForeColor="#367B2F" Text="DATOS DEL PROYECTO"></asp:Label>
                                                </legend>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="#10420F" Text="NOMBRE COMERCIAL"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label3" runat="server" Font-Bold="True" ForeColor="#10420F" Text="NIT"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label4" runat="server" Font-Bold="True" ForeColor="#10420F" Text="DIRECCION"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding-bottom: 5px; width: 50%">
                                                <asp:Label ID="lblRazonSocial" runat="server"></asp:Label>
                                            </td>
                                            <td style="padding-bottom: 5px; width: 10%">
                                                <asp:Label ID="lblNit" runat="server"></asp:Label>
                                            </td>
                                            <td style="padding-bottom: 5px; width: 40%">
                                                <asp:Label ID="lblDireccion" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <dx:ASPxPageControl ID="pgcOpciones" runat="server" ActiveTabIndex="1" Theme="Metropolis" Width="100%">
                                                    <TabPages>
                                                        <dx:TabPage Text="IMPORTACION INFORMACIÓN">
                                                            <ContentCollection>
                                                                <dx:ContentControl runat="server">
                                                                    <table style="width: 100%">
                                                                        <tr>
                                                                            <td style="width: 30%">
                                                                                <asp:FileUpload ID="FileUploader" runat="server" ToolTip="Seleccione el archivo .CSV" Width="100%" />
                                                                            </td>
                                                                            <td colspan="4" style="width: 60%">
                                                                                <dx:ASPxButton ID="btnImportar" runat="server" AutoPostBack="False" Font-Bold="True" ForeColor="Maroon" OnClick="btnImportar_Click" Text="Visualizar Registros" Theme="Metropolis">
                                                                                    <ClientSideEvents Click="function(s, e) {
//         LoadingPanel.Show();
}" />
                                                                                    <Image IconID="zoom_zoom_16x16">
                                                                                    </Image>
                                                                                </dx:ASPxButton>
                                                                                <dx:ASPxButton ID="btnGrabar" runat="server" Font-Bold="True" ForeColor="Maroon" OnClick="btnGrabar_Click" Text="Importar Registros" Theme="Metropolis">
                                                                                    <ClientSideEvents Click="function(s, e) {
                                    if(confirm('Se ELIMINARAN los movimientos registrados previamente a este proceso./n ¿Desea importar los movimientos?'))
                                    {
                                    LoadingPanel.Show();
                                    e.processOnServer = true;
                                    }
                                    else
                                    {
                                    e.processOnServer = false;
                                    }

                                    }" />
                                                                                    <Image IconID="actions_addfile_16x16">
                                                                                    </Image>
                                                                                </dx:ASPxButton>
                                                                            </td>
                                                                            <td style="width: 10%; text-align: right">
                                                                                <dx:ASPxButton ID="btnLimpiaTo" runat="server" Enabled="False" Font-Bold="True" ForeColor="Maroon" OnClick="btnLimpiaTo_Click" Text="Eliminar Mov. por  T.Op." Theme="Metropolis" ToolTip="Eliminar los Movimientos del Tipo de Operacion del Periodo" Width="160px">
                                                                                    <ClientSideEvents Click="function(s, e) {
                                    if(confirm('Se ELIMINARAN los movimientos registrados del tipo de operación y periodo seleccionado; una vez realizado el proceso no podra volver atras./n ¿Desea eliminar los movimientos?'))
                                    {
                                    LoadingPanel.Show();
                                    e.processOnServer = true;
                                    }
                                    else
                                    {
                                    e.processOnServer = false;
                                    }

                                    }
                                " />
                                                                                    <Image IconID="actions_deletelist_16x16">
                                                                                    </Image>
                                                                                </dx:ASPxButton>
                                                                            </td>
                                                                        </tr>
                                                                        
                                                                        <tr>
                                        
                                            <td colspan="6">
                                                <dx:ASPxGridView ID="dgvTransacciones" runat="server" Theme="Default" AutoGenerateColumns="False" KeyFieldName="LngId" EnableTheming="True" Font-Size="Smaller" ClientInstanceName="dgvTransacciones" OnHtmlRowPrepared="dgvTransacciones_HtmlRowPrepared" Width="100%" OnCustomCallback="dgvTransacciones_CustomCallback">
                                                    <SettingsContextMenu EnableRowMenu="False">
                                                    </SettingsContextMenu>
                                                    <Templates>
                                                        <EditForm >
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="ID :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtId" runat="server" ClientInstanceName="txtId" Width="170px" ReadOnly="True">
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                </tr>

                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="Fecha :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxDateEdit ID="detFecha" runat="server" EditFormat="DateTime" Font-Size="Smaller">
                                                                            <TimeSectionProperties>
                                                                                <TimeEditProperties>
                                                                                    <ClearButton Visibility="Auto">
                                                                                    </ClearButton>
                                                                                </TimeEditProperties>
                                                                            </TimeSectionProperties>
                                                                            <ClearButton Visibility="Auto">
                                                                            </ClearButton>
                                                                            <ValidationSettings>
                                                                                <RequiredField IsRequired="True" />
                                                                            </ValidationSettings>
                                                                        </dx:ASPxDateEdit>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel12" runat="server" Height="16px" Text="Válido Cred. Fiscal :" Width="110px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxComboBox ID="cmbTipoCred" runat="server" Font-Size="Smaller" ValueType="System.Int32">
                                                                            <Items>
                                                                                <dx:ListEditItem Text="SI" Value="1" />
                                                                                <dx:ListEditItem Text="NO" Value="2" />
                                                                            </Items>
                                                                            <ClearButton Visibility="Auto">
                                                                            </ClearButton>
                                                                            <ValidationSettings>
                                                                                <RequiredField IsRequired="True" />
                                                                            </ValidationSettings>
                                                                        </dx:ASPxComboBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel11" runat="server" Text="Tipo Pago :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxComboBox ID="cmbTipoPago" runat="server" Font-Size="Smaller" ValueType="System.Int32">
                                                                            <Items>
                                                                                <dx:ListEditItem Text="EFECTIVO" Value="1" />
                                                                                <dx:ListEditItem Text="PREPAGO" Value="2" />
                                                                                <dx:ListEditItem Text="VALE" Value="3" />
                                                                            </Items>
                                                                            <ClearButton Visibility="Auto">
                                                                            </ClearButton>
                                                                            <ValidationSettings>
                                                                                <RequiredField IsRequired="True" />
                                                                            </ValidationSettings>
                                                                        </dx:ASPxComboBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td><br />
                                                                        <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="Tipo Consumidor :" Width="110px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxComboBox ID="cmbTipoCons" runat="server" Font-Size="Smaller" ValueType="System.Int32">
                                                                            <Items>
                                                                                <dx:ListEditItem Text="MOTORIZADO" Value="1" />
                                                                                <dx:ListEditItem Text="BIDON" Value="2" />
                                                                                <dx:ListEditItem Text="BOTELLA" Value="3" />
                                                                                <dx:ListEditItem Text="OTRO" Value="4" />
                                                                            </Items>
                                                                            <ClearButton Visibility="Auto">
                                                                            </ClearButton>
                                                                            <ValidationSettings>
                                                                                <RequiredField IsRequired="True" />
                                                                            </ValidationSettings>
                                                                        </dx:ASPxComboBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td></td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="Cliente :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtCliente" runat="server" Width="170px" Height="16px" Font-Size="Smaller" MaxLength="90">
                                                                            <ClientSideEvents TextChanged="function(s, e) {
	    s.SetText(s.GetText().toUpperCase());
    }" />
                                                                            <ValidationSettings>
                                                                                <RequiredField IsRequired="True" />
                                                                            </ValidationSettings>
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="NIT :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtNit" runat="server" Font-Size="Smaller" MaxLength="15" Width="170px">
                                                                            <MaskSettings Mask="&lt;0..999999999999999&gt;" PromptChar=" " />
                                                                            <ValidationSettings>
                                                                                <RequiredField IsRequired="True" />
                                                                            </ValidationSettings>
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel14" runat="server" Text="Usuario :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtUsuario" runat="server" ClientInstanceName="txtId" Font-Size="Smaller" MaxLength="100" Width="170px">
                                                                            <ValidationSettings>
                                                                                <RequiredField IsRequired="True" />
                                                                            </ValidationSettings>
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel20" runat="server" Text="Tipo de Venta :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxRadioButtonList ID="rbtTipoVenta" runat="server" SelectedIndex="0" ClientInstanceName="rbtTipoVenta" ValueType="System.Int32">
                                                                            <ClientSideEvents ValueChanged="function(s, e) {
	    console.log('entraaaaaa....');
    alert(&quot;xxxxxxxxxxxx&quot;);
    txtNoAutoExt.SetVisible(false);

    }" />
                                                                            <Items>
                                                                                <dx:ListEditItem Selected="True" Text="Nacional" Value="1" />
                                                                                <dx:ListEditItem Text="Internacional" Value="2" />
                                                                            </Items>
                                                                        </dx:ASPxRadioButtonList>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel19" runat="server" Text="País :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxComboBox ID="cmbPais" runat="server">
                                                                            <Items>
                                                                                <dx:ListEditItem Text="BOLIVIA" Value="68" />
                                                                                <dx:ListEditItem Text="ARGENTINA" Value="32" />
                                                                                <dx:ListEditItem Text="BRASIL" Value="76" />
                                                                                <dx:ListEditItem Text="CHILE" Value="152" />
                                                                                <dx:ListEditItem Text="COLOMBIA" Value="170" />
                                                                                <dx:ListEditItem Text="ECUADOR" Value="218" />
                                                                                <dx:ListEditItem Text="PARAGUAY" Value="600" />
                                                                                <dx:ListEditItem Text="PERU" Value="604" />
                                                                                <dx:ListEditItem Text="URUGUAY" Value="858 " />
                                                                                <dx:ListEditItem Text="VENEZUELA" Value="862" />
                                                                            </Items>
                                                                            <ClearButton Visibility="Auto">
                                                                            </ClearButton>
                                                                            <ValidationSettings>
                                                                                <RequiredField IsRequired="True" />
                                                                            </ValidationSettings>
                                                                        </dx:ASPxComboBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel13" runat="server" Text="Placa :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtPlaca" runat="server" ClientInstanceName="txtId" Font-Size="Smaller" MaxLength="11" Width="170px">
                                                                            <ClientSideEvents TextChanged="function(s, e) {
	    s.SetText(s.GetText().toUpperCase());
    }" />
                                                                            <ValidationSettings>
                                                                                <RequiredField IsRequired="True" />
                                                                            </ValidationSettings>
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel15" runat="server" Text="No. Autorización :" Width="100px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtNoAuto" runat="server" Font-Size="Smaller" MaxLength="15" Width="170px">
                                                                            <MaskSettings Mask="&lt;0..999999999999999&gt;" />
                                                                            <ValidationSettings>
                                                                                <RequiredField IsRequired="True" />
                                                                            </ValidationSettings>
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="No. Factura :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtNoFact" runat="server" Font-Size="Smaller" MaxLength="15" Width="170px">
                                                                            <MaskSettings Mask="&lt;0..999999999999999&gt;" />
                                                                            <ValidationSettings>
                                                                                <RequiredField IsRequired="True" />
                                                                            </ValidationSettings>
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel26" runat="server" Text="Cod. Control :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtCodCtrl" runat="server" Font-Size="Smaller" MaxLength="14" Width="170px">
                                                                            <MaskSettings Mask="" />
                                                                            <ValidationSettings SetFocusOnError="True">
                                                                            </ValidationSettings>
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel18" runat="server" Text="Importe :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtImporte" runat="server" Width="170px">
                                                                            <MaskSettings IncludeLiterals="DecimalSymbol" Mask="&lt;0..9999999&gt;.&lt;00..99&gt;" />
                                                                            <ValidationSettings>
                                                                                <RequiredField IsRequired="True" />
                                                                            </ValidationSettings>
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
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
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                    <td runat="server">
                                                                        <dx:ASPxLabel ID="ASPxLabel21" runat="server" Text="No. Autorización Ext. :" Width="130px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtNoAutoExt" runat="server" ClientInstanceName="txtNoAutoExt" Font-Size="Smaller" MaxLength="15" Width="170px">
                                                                            <MaskSettings Mask="&lt;0..999999999999999&gt;" />
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel22" runat="server" Text="No. Factura Ext. :" Width="120px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtNoFactExt" runat="server" ClientInstanceName="txtNoFactExt" Font-Size="Smaller" MaxLength="15" Width="170px">
                                                                            <MaskSettings Mask="&lt;0..999999999999999&gt;" />
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                    <td >
                                                                        <dx:ASPxLabel ID="ASPxLabel16" runat="server" Text="Cod. Control Ext. :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtCodCtrlExt" runat="server" Font-Size="Smaller" MaxLength="14" Width="170px">
                                                                            <MaskSettings Mask="" />
                                                                            <ValidationSettings SetFocusOnError="True">
                                                                            </ValidationSettings>
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel27" runat="server" Text="Importe Ext. :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtImporteExt" runat="server" Width="170px">
                                                                            <MaskSettings IncludeLiterals="DecimalSymbol" Mask="&lt;0..9999999&gt;.&lt;00..99&gt;" />
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
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
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel25" runat="server" Text="No. Doc. Referencia :" Width="130px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtNoDocRef" runat="server" Font-Size="Smaller" MaxLength="15" Width="170px">
                                                                            <MaskSettings Mask="&lt;0..999999999999999&gt;" />
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="Tipo Factura :" Visible="False">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxComboBox ID="cmbTipoFact" runat="server" Font-Size="Smaller" style="margin-top: 0" ValueType="System.Int32" Visible="False">
                                                                            <Items>
                                                                                <dx:ListEditItem Text="MANUAL" Value="1" />
                                                                                <dx:ListEditItem Text="COMPUTARIZADA" Value="2" />
                                                                            </Items>
                                                                            <ClearButton Visibility="Auto">
                                                                            </ClearButton>
                                                                        </dx:ASPxComboBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="Estado :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxComboBox ID="cmbEstado" runat="server" Font-Size="Smaller" ValueType="System.Int32">
                                                                            <Items>
                                                                                <dx:ListEditItem Text="VALIDO" Value="1" />
                                                                                <dx:ListEditItem Text="ANULADO" Value="2" />
                                                                            </Items>
                                                                            <ClearButton Visibility="Auto">
                                                                            </ClearButton>
                                                                            <ValidationSettings>
                                                                                <RequiredField IsRequired="True" />
                                                                            </ValidationSettings>
                                                                        </dx:ASPxComboBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel9" runat="server" Text="Producto :" Width="100px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxComboBox ID="cmbProd" runat="server" ValueType="System.Int32" Font-Size="Smaller">
                                                                            <Items>
                                                                                <dx:ListEditItem Text="GNV" Value="1" />
                                                                                <dx:ListEditItem Text="DIESEL OIL" Value="2" />
                                                                                <dx:ListEditItem Text="GASOLINA ESPECIAL" Value="3" />
                                                                                <dx:ListEditItem Text="GASOLINA PREMIUM" Value="4" />
                                                                            </Items>
                                                                            <ClearButton Visibility="Auto">
                                                                            </ClearButton>
                                                                            <ValidationSettings>
                                                                                <RequiredField IsRequired="True" />
                                                                            </ValidationSettings>
                                                                        </dx:ASPxComboBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel10" runat="server" Text="Cantidad :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td class="auto-style16">
                                                                        <dx:ASPxTextBox ID="txtCantidad" runat="server" Width="170px">
                                                                            <MaskSettings Mask="&lt;0..9999999&gt;.&lt;00..99&gt;" IncludeLiterals="DecimalSymbol" />
                                                                            <ValidationSettings>
                                                                                <RequiredField IsRequired="True" />
                                                                            </ValidationSettings>
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td class="auto-style19">
                                                                        <dx:ASPxLabel ID="ASPxLabel17" runat="server" Text="Precio :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtPrecio" runat="server" Width="170px">
                                                                            <MaskSettings IncludeLiterals="DecimalSymbol" Mask="&lt;0..999&gt;.&lt;00..99&gt;" PromptChar=" " />
                                                                            <ValidationSettings>
                                                                                <RequiredField IsRequired="True" />
                                                                            </ValidationSettings>
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="auto-style13">&nbsp;</td>
                                                                    <td class="auto-style18">&nbsp;</td>
                                                                    <td class="auto-style16">&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td class="auto-style19">&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="auto-style13">&nbsp;</td>
                                                                    <td class="auto-style18">
                                                                        <dx:ASPxLabel ID="ASPxLabel23" runat="server" Text="Autorización de Venta :" Width="150px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td class="auto-style16">
                                                                        <dx:ASPxComboBox ID="cmbAutoVta" runat="server">
                                                                            <Items>
                                                                                <dx:ListEditItem Text="SUSTANCIAS CONTROLADAS" Value="1" />
                                                                                <dx:ListEditItem Text="COMANDO CONJUNTO" Value="2" />
                                                                            </Items>
                                                                            <ClearButton Visibility="Auto">
                                                                            </ClearButton>
                                                                        </dx:ASPxComboBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td class="auto-style19">
                                                                        <dx:ASPxLabel ID="ASPxLabel24" runat="server" Text="No. Hoja Ruta :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtNoAutoVta" runat="server" Font-Size="Smaller" MaxLength="15" Width="170px">
                                                                            <MaskSettings Mask="&lt;0..999999999999999&gt;" />
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="auto-style13">&nbsp;</td>
                                                                    <td class="auto-style18">&nbsp;</td>
                                                                    <td class="auto-style16">&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td class="auto-style19">&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="auto-style13">&nbsp;</td>
                                                                    <td class="auto-style18">&nbsp;</td>
                                                                    <td class="auto-style16">&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td class="auto-style19">&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="auto-style13">&nbsp;</td>
                                                                    <td class="auto-style18">
                                                                        <dx:ASPxLabel ID="lblMensaje" runat="server" Text="hghgh">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td class="auto-style16">&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td class="auto-style19">&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="auto-style13">&nbsp;</td>
                                                                    <td class="auto-style18">&nbsp;</td>
                                                                    <td class="auto-style16">&nbsp;</td>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                    <td class="auto-style19">
                                                                        <dx:ASPxButton ID="btnAceptar" runat="server" AutoPostBack="False" ClientInstanceName="btnAceptar" Text="Aceptar" ForeColor="White" BackColor="#307D40" Width="72px">
                                                                            <ClientSideEvents Click="function(s, e) {
                                                                                    dgvTransacciones.UpdateEdit()	
                                                                            }" />
                                                                        </dx:ASPxButton>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxButton ID="btnCancelar" runat="server" AutoPostBack="False" ClientInstanceName="btnCancelar" Text="Cerrar" ForeColor="White" BackColor="#307D40">
                                                                            <ClientSideEvents Click="function(s, e) {
                                                                                    dgvTransacciones.CancelEdit()	
                                                                            }" />
                                                                        </dx:ASPxButton>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                </tr>

                                                                <tr>
                                                                    <td class="auto-style13">&nbsp;</td>
                                                                    <td class="auto-style18">&nbsp;</td>
                                                                    <td class="auto-style16">&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td class="auto-style19">&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                </tr>

                                                            </table>
                                                        
                                                        </EditForm>
                                                    </Templates>
                                                    <SettingsPager PageSize="100">
                                                        <PageSizeItemSettings Items="50, 100, 200, 500, 1000" Visible="True">
                                                        </PageSizeItemSettings>
                                                    </SettingsPager>
                                                    <SettingsEditing Mode="PopupEditForm">
                                                    </SettingsEditing>
                                                    <Settings ShowHeaderFilterButton="True" />
                                                    <SettingsBehavior AllowFocusedRow="True" />
                                                    <SettingsPopup>
                                                        <EditForm Modal="True" />
                                                    </SettingsPopup>
                                                    <SettingsText CommandNew="Nuevo" />
                                                    <Columns>
                                                        <dx:GridViewCommandColumn ShowInCustomizationForm="True" VisibleIndex="0" Visible="False">
                                                        </dx:GridViewCommandColumn>
                                                        <dx:GridViewDataTextColumn Caption="ID MOV." FieldName="StrIdVenta" ShowInCustomizationForm="True" VisibleIndex="2" ToolTip="Identificador del Movimiento">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="TIPO MOV." FieldName="StrTipoMov" ShowInCustomizationForm="True" VisibleIndex="4" ToolTip="Tipo de Movimiento">
                                                            <Settings AutoFilterCondition="Contains" />
                                                            <CellStyle HorizontalAlign="Center">
                                                            </CellStyle>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="NIT" FieldName="LngNit" ShowInCustomizationForm="True" VisibleIndex="6">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="RAZON SOCIAL" FieldName="StrRazonSocial" ShowInCustomizationForm="True" VisibleIndex="7">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="IDENTIFICADOR" FieldName="StrIdentificador" ShowInCustomizationForm="True" VisibleIndex="8">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="PRODUCTO" FieldName="StrProducto" ShowInCustomizationForm="True" VisibleIndex="13">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="CANTIDAD" FieldName="DecVolumen" ShowInCustomizationForm="True" VisibleIndex="15" ToolTip="Tipo de Factura">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="PRECIO" FieldName="DecPrecio" ShowInCustomizationForm="True" VisibleIndex="14">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="IMPORTE" FieldName="DecImporte" ShowInCustomizationForm="True" VisibleIndex="16">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="NO. FACTURA" FieldName="LngNroFactura" ShowInCustomizationForm="True" VisibleIndex="10" ToolTip="Número de Factura">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="NO. AUTORIZACION" FieldName="LngNroAutorizacion" ShowInCustomizationForm="True" VisibleIndex="9" ToolTip="Número de Autorización">
                                                            <Settings AutoFilterCondition="Contains" />
                                                            <FilterTemplate>
                                                                <dx:ASPxComboBox ID="cmbFiltroProd" runat="server" ValueType="System.String">
                                                                </dx:ASPxComboBox>
                                                            </FilterTemplate>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="NO. REF." FieldName="StrNroReferencia" ShowInCustomizationForm="True" VisibleIndex="11" ToolTip="Número de Referencia">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn ShowInCustomizationForm="True" VisibleIndex="1" Caption="#" FieldName="LngId" ToolTip="Funciones">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="FECHA" FieldName="StrFechaEmision" ShowInCustomizationForm="True" VisibleIndex="3">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="ESTADO" FieldName="StrEstado" ShowInCustomizationForm="True" VisibleIndex="5" ToolTip="Estado del Movimiento">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="NO.AUT.VTA." FieldName="LngNroAutVenta" ShowInCustomizationForm="True" VisibleIndex="12" ToolTip="Número de Autorización de Venta">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                    <Styles CssPostfix="DevGlp"><AlternatingRow BackColor="#EEEEEE">
                                                        </AlternatingRow></Styles>
                                                </dx:ASPxGridView>
                                            </td>
                                        
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>
                                                    &nbsp;</td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td colspan="3">
                                                    <asp:Label ID="lblTipoError" runat="server" Font-Bold="True" ForeColor="Maroon" Width="133%">Registros Observados</asp:Label>
                                                </td>

                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td colspan="6">
                                                    <dx:ASPxGridView ID="dgvTransaccionError" runat="server" AutoGenerateColumns="False" ClientInstanceName="dgvTransaccionError" EnableTheming="True" Font-Size="Smaller" OnHtmlRowPrepared="dgvTransaccionError_HtmlRowPrepared" Theme="Default" Width="100%" KeyFieldName="LngId">
                                                        <SettingsContextMenu EnableRowMenu="False">
                                                        </SettingsContextMenu>
                                                        <Templates>
                                                            <EditForm>
                                                                <table>
                                                                    <tr>
                                                                        <td class="auto-style13">&nbsp;</td>
                                                                        <td class="auto-style18">
                                                                            <dx:ASPxLabel ID="ASPxLabel28" runat="server" Text="ID :">
                                                                            </dx:ASPxLabel>
                                                                        </td>
                                                                        <td class="auto-style16">
                                                                            <dx:ASPxTextBox ID="txtId0" runat="server" ClientInstanceName="txtId" ReadOnly="True" Width="170px">
                                                                            </dx:ASPxTextBox>
                                                                        </td>
                                                                        <td>&nbsp;</td>
                                                                        <td class="auto-style19">&nbsp;</td>
                                                                        <td>&nbsp;</td>
                                                                        <td>&nbsp;</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="auto-style13">&nbsp;</td>
                                                                        <td class="auto-style18">
                                                                            <dx:ASPxLabel ID="ASPxLabel29" runat="server" Text="Fecha :">
                                                                            </dx:ASPxLabel>
                                                                        </td>
                                                                        <td class="auto-style16">
                                                                            <dx:ASPxDateEdit ID="detFecha0" runat="server" EditFormat="DateTime" Font-Size="Smaller">
                                                                                <TimeSectionProperties>
                                                                                    <TimeEditProperties>
                                                                                        <ClearButton Visibility="Auto">
                                                                                        </ClearButton>
                                                                                    </TimeEditProperties>
                                                                                </TimeSectionProperties>
                                                                                <ClearButton Visibility="Auto">
                                                                                </ClearButton>
                                                                                <ValidationSettings>
                                                                                    <RequiredField IsRequired="True" />
                                                                                </ValidationSettings>
                                                                            </dx:ASPxDateEdit>
                                                                        </td>
                                                                        <td>&nbsp;</td>
                                                                        <td class="auto-style19">
                                                                            <dx:ASPxLabel ID="ASPxLabel30" runat="server" Height="16px" Text="Válido Cred. Fiscal :" Width="110px">
                                                                            </dx:ASPxLabel>
                                                                        </td>
                                                                        <td>
                                                                            <dx:ASPxComboBox ID="cmbTipoCred0" runat="server" Font-Size="Smaller" ValueType="System.Int32">
                                                                                <Items>
                                                                                    <dx:ListEditItem Text="SI" Value="1" />
                                                                                    <dx:ListEditItem Text="NO" Value="2" />
                                                                                </Items>
                                                                                <ClearButton Visibility="Auto">
                                                                                </ClearButton>
                                                                                <ValidationSettings>
                                                                                    <RequiredField IsRequired="True" />
                                                                                </ValidationSettings>
                                                                            </dx:ASPxComboBox>
                                                                        </td>
                                                                        <td>&nbsp;</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="auto-style13">&nbsp;</td>
                                                                        <td class="auto-style18">
                                                                            <dx:ASPxLabel ID="ASPxLabel31" runat="server" Text="Tipo Pago :">
                                                                            </dx:ASPxLabel>
                                                                        </td>
                                                                        <td class="auto-style16">
                                                                            <dx:ASPxComboBox ID="cmbTipoPago0" runat="server" Font-Size="Smaller" ValueType="System.Int32">
                                                                                <Items>
                                                                                    <dx:ListEditItem Text="EFECTIVO" Value="1" />
                                                                                    <dx:ListEditItem Text="PREPAGO" Value="2" />
                                                                                    <dx:ListEditItem Text="VALE" Value="3" />
                                                                                </Items>
                                                                                <ClearButton Visibility="Auto">
                                                                                </ClearButton>
                                                                                <ValidationSettings>
                                                                                    <RequiredField IsRequired="True" />
                                                                                </ValidationSettings>
                                                                            </dx:ASPxComboBox>
                                                                        </td>
                                                                        <td>&nbsp;</td>
                                                                        <td class="auto-style19">
                                                                            <br />
                                                                            <dx:ASPxLabel ID="ASPxLabel32" runat="server" Text="Tipo Consumidor :" Width="110px">
                                                                            </dx:ASPxLabel>
                                                                        </td>
                                                                        <td>
                                                                            <dx:ASPxComboBox ID="cmbTipoCons0" runat="server" Font-Size="Smaller" ValueType="System.Int32">
                                                                                <Items>
                                                                                    <dx:ListEditItem Text="MOTORIZADO" Value="1" />
                                                                                    <dx:ListEditItem Text="BIDON" Value="2" />
                                                                                    <dx:ListEditItem Text="BOTELLA" Value="3" />
                                                                                    <dx:ListEditItem Text="OTRO" Value="4" />
                                                                                </Items>
                                                                                <ClearButton Visibility="Auto">
                                                                                </ClearButton>
                                                                                <ValidationSettings>
                                                                                    <RequiredField IsRequired="True" />
                                                                                </ValidationSettings>
                                                                            </dx:ASPxComboBox>
                                                                        </td>
                                                                        <td>&nbsp;</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="auto-style20"></td>
                                                                        <td class="auto-style21">
                                                                            <dx:ASPxLabel ID="ASPxLabel33" runat="server" Text="Cliente :">
                                                                            </dx:ASPxLabel>
                                                                        </td>
                                                                        <td class="auto-style22">
                                                                            <dx:ASPxTextBox ID="txtCliente0" runat="server" Font-Size="Smaller" Height="16px" MaxLength="90" Width="170px">
                                                                                <ClientSideEvents TextChanged="function(s, e) {
	        s.SetText(s.GetText().toUpperCase());
        }" />
                                                                                <ValidationSettings>
                                                                                    <RequiredField IsRequired="True" />
                                                                                </ValidationSettings>
                                                                            </dx:ASPxTextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="auto-style13">&nbsp;</td>
                                                                        <td class="auto-style18">
                                                                            <dx:ASPxLabel ID="ASPxLabel34" runat="server" Text="NIT :">
                                                                            </dx:ASPxLabel>
                                                                        </td>
                                                                        <td class="auto-style16">
                                                                            <dx:ASPxTextBox ID="txtNit0" runat="server" Font-Size="Smaller" MaxLength="15" Width="170px">
                                                                                <MaskSettings Mask="&lt;0..999999999999999&gt;" PromptChar=" " />
                                                                                <ValidationSettings>
                                                                                    <RequiredField IsRequired="True" />
                                                                                </ValidationSettings>
                                                                            </dx:ASPxTextBox>
                                                                        </td>
                                                                        <td>&nbsp;</td>
                                                                        <td class="auto-style19">
                                                                            <dx:ASPxLabel ID="ASPxLabel35" runat="server" Text="Usuario :">
                                                                            </dx:ASPxLabel>
                                                                        </td>
                                                                        <td>
                                                                            <dx:ASPxTextBox ID="txtUsuario0" runat="server" ClientInstanceName="txtId" Font-Size="Smaller" MaxLength="100" Width="170px">
                                                                                <ValidationSettings>
                                                                                    <RequiredField IsRequired="True" />
                                                                                </ValidationSettings>
                                                                            </dx:ASPxTextBox>
                                                                        </td>
                                                                        <td>&nbsp;</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="auto-style13">&nbsp;</td>
                                                                        <td class="auto-style18">
                                                                            <dx:ASPxLabel ID="ASPxLabel36" runat="server" Text="Tipo de Venta :">
                                                                            </dx:ASPxLabel>
                                                                        </td>
                                                                        <td class="auto-style16">
                                                                            <dx:ASPxRadioButtonList ID="rbtTipoVenta0" runat="server" ClientInstanceName="rbtTipoVenta" SelectedIndex="0" ValueType="System.Int32">
                                                                                <ClientSideEvents ValueChanged="function(s, e) {
	        console.log('entraaaaaa....');
        alert(&quot;xxxxxxxxxxxx&quot;);
        txtNoAutoExt.SetVisible(false);

        }" />
                                                                                <Items>
                                                                                    <dx:ListEditItem Selected="True" Text="Nacional" Value="1" />
                                                                                    <dx:ListEditItem Text="Internacional" Value="2" />
                                                                                </Items>
                                                                            </dx:ASPxRadioButtonList>
                                                                        </td>
                                                                        <td>&nbsp;</td>
                                                                        <td class="auto-style19">&nbsp;</td>
                                                                        <td>&nbsp;</td>
                                                                        <td>&nbsp;</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="auto-style13">&nbsp;</td>
                                                                        <td class="auto-style18">
                                                                            <dx:ASPxLabel ID="ASPxLabel37" runat="server" Text="País :">
                                                                            </dx:ASPxLabel>
                                                                        </td>
                                                                        <td class="auto-style16">
                                                                            <dx:ASPxComboBox ID="cmbPais0" runat="server">
                                                                                <Items>
                                                                                    <dx:ListEditItem Text="BOLIVIA" Value="68" />
                                                                                    <dx:ListEditItem Text="ARGENTINA" Value="32" />
                                                                                    <dx:ListEditItem Text="BRASIL" Value="76" />
                                                                                    <dx:ListEditItem Text="CHILE" Value="152" />
                                                                                    <dx:ListEditItem Text="COLOMBIA" Value="170" />
                                                                                    <dx:ListEditItem Text="ECUADOR" Value="218" />
                                                                                    <dx:ListEditItem Text="PARAGUAY" Value="600" />
                                                                                    <dx:ListEditItem Text="PERU" Value="604" />
                                                                                    <dx:ListEditItem Text="URUGUAY" Value="858 " />
                                                                                    <dx:ListEditItem Text="VENEZUELA" Value="862" />
                                                                                </Items>
                                                                                <ClearButton Visibility="Auto">
                                                                                </ClearButton>
                                                                                <ValidationSettings>
                                                                                    <RequiredField IsRequired="True" />
                                                                                </ValidationSettings>
                                                                            </dx:ASPxComboBox>
                                                                        </td>
                                                                        <td>&nbsp;</td>
                                                                        <td class="auto-style19">
                                                                            <dx:ASPxLabel ID="ASPxLabel38" runat="server" Text="Placa :">
                                                                            </dx:ASPxLabel>
                                                                        </td>
                                                                        <td>
                                                                            <dx:ASPxTextBox ID="txtPlaca0" runat="server" ClientInstanceName="txtId" Font-Size="Smaller" MaxLength="11" Width="170px">
                                                                                <ClientSideEvents TextChanged="function(s, e) {
	        s.SetText(s.GetText().toUpperCase());
        }" />
                                                                                <ValidationSettings>
                                                                                    <RequiredField IsRequired="True" />
                                                                                </ValidationSettings>
                                                                            </dx:ASPxTextBox>
                                                                        </td>
                                                                        <td>&nbsp;</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="auto-style13">&nbsp;</td>
                                                                        <td class="auto-style18">
                                                                            <dx:ASPxLabel ID="ASPxLabel39" runat="server" Text="No. Autorización :" Width="100px">
                                                                            </dx:ASPxLabel>
                                                                        </td>
                                                                        <td class="auto-style16">
                                                                            <dx:ASPxTextBox ID="txtNoAuto0" runat="server" Font-Size="Smaller" MaxLength="15" Width="170px">
                                                                                <MaskSettings Mask="&lt;0..999999999999999&gt;" />
                                                                                <ValidationSettings>
                                                                                    <RequiredField IsRequired="True" />
                                                                                </ValidationSettings>
                                                                            </dx:ASPxTextBox>
                                                                        </td>
                                                                        <td>&nbsp;</td>
                                                                        <td class="auto-style19">
                                                                            <dx:ASPxLabel ID="ASPxLabel40" runat="server" Text="No. Factura :">
                                                                            </dx:ASPxLabel>
                                                                        </td>
                                                                        <td>
                                                                            <dx:ASPxTextBox ID="txtNoFact0" runat="server" Font-Size="Smaller" MaxLength="15" Width="170px">
                                                                                <MaskSettings Mask="&lt;0..999999999999999&gt;" />
                                                                                <ValidationSettings>
                                                                                    <RequiredField IsRequired="True" />
                                                                                </ValidationSettings>
                                                                            </dx:ASPxTextBox>
                                                                        </td>
                                                                        <td>&nbsp;</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="auto-style13">&nbsp;</td>
                                                                        <td class="auto-style18">
                                                                            <dx:ASPxLabel ID="ASPxLabel41" runat="server" Text="Cod. Control :">
                                                                            </dx:ASPxLabel>
                                                                        </td>
                                                                        <td class="auto-style16">
                                                                            <dx:ASPxTextBox ID="txtCodCtrl0" runat="server" Font-Size="Smaller" MaxLength="14" Width="170px">
                                                                                <MaskSettings Mask="" />
                                                                                <ValidationSettings SetFocusOnError="True">
                                                                                </ValidationSettings>
                                                                            </dx:ASPxTextBox>
                                                                        </td>
                                                                        <td>&nbsp;</td>
                                                                        <td class="auto-style19">
                                                                            <dx:ASPxLabel ID="ASPxLabel42" runat="server" Text="Importe :">
                                                                            </dx:ASPxLabel>
                                                                        </td>
                                                                        <td>
                                                                            <dx:ASPxTextBox ID="txtImporte0" runat="server" Width="170px">
                                                                                <MaskSettings IncludeLiterals="DecimalSymbol" Mask="&lt;0..9999999&gt;.&lt;00..99&gt;" />
                                                                                <ValidationSettings>
                                                                                    <RequiredField IsRequired="True" />
                                                                                </ValidationSettings>
                                                                            </dx:ASPxTextBox>
                                                                        </td>
                                                                        <td>&nbsp;</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="auto-style13">&nbsp;</td>
                                                                        <td class="auto-style18">&nbsp;</td>
                                                                        <td class="auto-style16">&nbsp;</td>
                                                                        <td>&nbsp;</td>
                                                                        <td class="auto-style19">&nbsp;</td>
                                                                        <td>&nbsp;</td>
                                                                        <td>&nbsp;</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="auto-style13">&nbsp;</td>
                                                                        <td runat="server" class="auto-style18">
                                                                            <dx:ASPxLabel ID="ASPxLabel43" runat="server" Text="No. Autorización Ext. :" Width="130px">
                                                                            </dx:ASPxLabel>
                                                                        </td>
                                                                        <td class="auto-style16">
                                                                            <dx:ASPxTextBox ID="txtNoAutoExt0" runat="server" ClientInstanceName="txtNoAutoExt" Font-Size="Smaller" MaxLength="15" Width="170px">
                                                                                <MaskSettings Mask="&lt;0..999999999999999&gt;" />
                                                                            </dx:ASPxTextBox>
                                                                        </td>
                                                                        <td>&nbsp;</td>
                                                                        <td class="auto-style19">
                                                                            <dx:ASPxLabel ID="ASPxLabel44" runat="server" Text="No. Factura Ext. :" Width="120px">
                                                                            </dx:ASPxLabel>
                                                                        </td>
                                                                        <td>
                                                                            <dx:ASPxTextBox ID="txtNoFactExt0" runat="server" ClientInstanceName="txtNoFactExt" Font-Size="Smaller" MaxLength="15" Width="170px">
                                                                                <MaskSettings Mask="&lt;0..999999999999999&gt;" />
                                                                            </dx:ASPxTextBox>
                                                                        </td>
                                                                        <td>&nbsp;</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="auto-style13">&nbsp;</td>
                                                                        <td class="auto-style18">
                                                                            <dx:ASPxLabel ID="ASPxLabel45" runat="server" Text="Cod. Control Ext. :">
                                                                            </dx:ASPxLabel>
                                                                        </td>
                                                                        <td class="auto-style16">
                                                                            <dx:ASPxTextBox ID="txtCodCtrlExt0" runat="server" Font-Size="Smaller" MaxLength="14" Width="170px">
                                                                                <MaskSettings Mask="" />
                                                                                <ValidationSettings SetFocusOnError="True">
                                                                                </ValidationSettings>
                                                                            </dx:ASPxTextBox>
                                                                        </td>
                                                                        <td>&nbsp;</td>
                                                                        <td class="auto-style19">
                                                                            <dx:ASPxLabel ID="ASPxLabel46" runat="server" Text="Importe Ext. :">
                                                                            </dx:ASPxLabel>
                                                                        </td>
                                                                        <td>
                                                                            <dx:ASPxTextBox ID="txtImporteExt0" runat="server" Width="170px">
                                                                                <MaskSettings IncludeLiterals="DecimalSymbol" Mask="&lt;0..9999999&gt;.&lt;00..99&gt;" />
                                                                            </dx:ASPxTextBox>
                                                                        </td>
                                                                        <td>&nbsp;</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="auto-style13">&nbsp;</td>
                                                                        <td class="auto-style18">&nbsp;</td>
                                                                        <td class="auto-style16">&nbsp;</td>
                                                                        <td>&nbsp;</td>
                                                                        <td class="auto-style19">&nbsp;</td>
                                                                        <td>&nbsp;</td>
                                                                        <td>&nbsp;</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="auto-style13">&nbsp;</td>
                                                                        <td class="auto-style18">
                                                                            <dx:ASPxLabel ID="ASPxLabel47" runat="server" Text="No. Doc. Referencia :" Width="130px">
                                                                            </dx:ASPxLabel>
                                                                        </td>
                                                                        <td class="auto-style16">
                                                                            <dx:ASPxTextBox ID="txtNoDocRef0" runat="server" Font-Size="Smaller" MaxLength="15" Width="170px">
                                                                                <MaskSettings Mask="&lt;0..999999999999999&gt;" />
                                                                            </dx:ASPxTextBox>
                                                                        </td>
                                                                        <td>&nbsp;</td>
                                                                        <td class="auto-style19">
                                                                            <dx:ASPxLabel ID="ASPxLabel48" runat="server" Text="Tipo Factura :" Visible="False">
                                                                            </dx:ASPxLabel>
                                                                        </td>
                                                                        <td>
                                                                            <dx:ASPxComboBox ID="cmbTipoFact0" runat="server" Font-Size="Smaller" style="margin-top: 0" ValueType="System.Int32" Visible="False">
                                                                                <Items>
                                                                                    <dx:ListEditItem Text="MANUAL" Value="1" />
                                                                                    <dx:ListEditItem Text="COMPUTARIZADA" Value="2" />
                                                                                </Items>
                                                                                <ClearButton Visibility="Auto">
                                                                                </ClearButton>
                                                                            </dx:ASPxComboBox>
                                                                        </td>
                                                                        <td>&nbsp;</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>&nbsp;</td>
                                                                        <td class="auto-style18">&nbsp;</td>
                                                                        <td class="auto-style16">&nbsp;</td>
                                                                        <td>&nbsp;</td>
                                                                        <td class="auto-style19">&nbsp;</td>
                                                                        <td>&nbsp;</td>
                                                                        <td>&nbsp;</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="auto-style13">&nbsp;</td>
                                                                        <td class="auto-style18">
                                                                            <dx:ASPxLabel ID="ASPxLabel49" runat="server" Text="Estado :">
                                                                            </dx:ASPxLabel>
                                                                        </td>
                                                                        <td class="auto-style16">
                                                                            <dx:ASPxComboBox ID="cmbEstado0" runat="server" Font-Size="Smaller" ValueType="System.Int32">
                                                                                <Items>
                                                                                    <dx:ListEditItem Text="VALIDO" Value="1" />
                                                                                    <dx:ListEditItem Text="ANULADO" Value="2" />
                                                                                </Items>
                                                                                <ClearButton Visibility="Auto">
                                                                                </ClearButton>
                                                                                <ValidationSettings>
                                                                                    <RequiredField IsRequired="True" />
                                                                                </ValidationSettings>
                                                                            </dx:ASPxComboBox>
                                                                        </td>
                                                                        <td>&nbsp;</td>
                                                                        <td class="auto-style19">&nbsp;</td>
                                                                        <td>&nbsp;</td>
                                                                        <td>&nbsp;</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="auto-style13">&nbsp;</td>
                                                                        <td class="auto-style18">&nbsp;</td>
                                                                        <td class="auto-style16">&nbsp;</td>
                                                                        <td>&nbsp;</td>
                                                                        <td class="auto-style19">&nbsp;</td>
                                                                        <td>&nbsp;</td>
                                                                        <td>&nbsp;</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="auto-style13">&nbsp;</td>
                                                                        <td class="auto-style18">
                                                                            <dx:ASPxLabel ID="ASPxLabel50" runat="server" Text="Producto :" Width="100px">
                                                                            </dx:ASPxLabel>
                                                                        </td>
                                                                        <td class="auto-style16">
                                                                            <dx:ASPxComboBox ID="cmbProd0" runat="server" Font-Size="Smaller" ValueType="System.Int32">
                                                                                <Items>
                                                                                    <dx:ListEditItem Text="GNV" Value="1" />
                                                                                    <dx:ListEditItem Text="DIESEL OIL" Value="2" />
                                                                                    <dx:ListEditItem Text="GASOLINA ESPECIAL" Value="3" />
                                                                                    <dx:ListEditItem Text="GASOLINA PREMIUM" Value="4" />
                                                                                </Items>
                                                                                <ClearButton Visibility="Auto">
                                                                                </ClearButton>
                                                                                <ValidationSettings>
                                                                                    <RequiredField IsRequired="True" />
                                                                                </ValidationSettings>
                                                                            </dx:ASPxComboBox>
                                                                        </td>
                                                                        <td>&nbsp;</td>
                                                                        <td class="auto-style19">&nbsp;</td>
                                                                        <td>&nbsp;</td>
                                                                        <td>&nbsp;</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="auto-style13">&nbsp;</td>
                                                                        <td class="auto-style18">
                                                                            <dx:ASPxLabel ID="ASPxLabel51" runat="server" Text="Cantidad :">
                                                                            </dx:ASPxLabel>
                                                                        </td>
                                                                        <td class="auto-style16">
                                                                            <dx:ASPxTextBox ID="txtCantidad0" runat="server" Width="170px">
                                                                                <MaskSettings IncludeLiterals="DecimalSymbol" Mask="&lt;0..9999999&gt;.&lt;00..99&gt;" />
                                                                                <ValidationSettings>
                                                                                    <RequiredField IsRequired="True" />
                                                                                </ValidationSettings>
                                                                            </dx:ASPxTextBox>
                                                                        </td>
                                                                        <td>&nbsp;</td>
                                                                        <td class="auto-style19">
                                                                            <dx:ASPxLabel ID="ASPxLabel52" runat="server" Text="Precio :">
                                                                            </dx:ASPxLabel>
                                                                        </td>
                                                                        <td>
                                                                            <dx:ASPxTextBox ID="txtPrecio0" runat="server" Width="170px">
                                                                                <MaskSettings IncludeLiterals="DecimalSymbol" Mask="&lt;0..999&gt;.&lt;00..99&gt;" PromptChar=" " />
                                                                                <ValidationSettings>
                                                                                    <RequiredField IsRequired="True" />
                                                                                </ValidationSettings>
                                                                            </dx:ASPxTextBox>
                                                                        </td>
                                                                        <td>&nbsp;</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="auto-style13">&nbsp;</td>
                                                                        <td class="auto-style18">&nbsp;</td>
                                                                        <td class="auto-style16">&nbsp;</td>
                                                                        <td>&nbsp;</td>
                                                                        <td class="auto-style19">&nbsp;</td>
                                                                        <td>&nbsp;</td>
                                                                        <td>&nbsp;</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="auto-style13">&nbsp;</td>
                                                                        <td class="auto-style18">
                                                                            <dx:ASPxLabel ID="ASPxLabel53" runat="server" Text="Autorización de Venta :" Width="150px">
                                                                            </dx:ASPxLabel>
                                                                        </td>
                                                                        <td class="auto-style16">
                                                                            <dx:ASPxComboBox ID="cmbAutoVta0" runat="server">
                                                                                <Items>
                                                                                    <dx:ListEditItem Text="SUSTANCIAS CONTROLADAS" Value="1" />
                                                                                    <dx:ListEditItem Text="COMANDO CONJUNTO" Value="2" />
                                                                                </Items>
                                                                                <ClearButton Visibility="Auto">
                                                                                </ClearButton>
                                                                            </dx:ASPxComboBox>
                                                                        </td>
                                                                        <td>&nbsp;</td>
                                                                        <td class="auto-style19">
                                                                            <dx:ASPxLabel ID="ASPxLabel54" runat="server" Text="No. Hoja Ruta :">
                                                                            </dx:ASPxLabel>
                                                                        </td>
                                                                        <td>
                                                                            <dx:ASPxTextBox ID="txtNoAutoVta0" runat="server" Font-Size="Smaller" MaxLength="15" Width="170px">
                                                                                <MaskSettings Mask="&lt;0..999999999999999&gt;" />
                                                                            </dx:ASPxTextBox>
                                                                        </td>
                                                                        <td>&nbsp;</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="auto-style13">&nbsp;</td>
                                                                        <td class="auto-style18">&nbsp;</td>
                                                                        <td class="auto-style16">&nbsp;</td>
                                                                        <td>&nbsp;</td>
                                                                        <td class="auto-style19">&nbsp;</td>
                                                                        <td>&nbsp;</td>
                                                                        <td>&nbsp;</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="auto-style13">&nbsp;</td>
                                                                        <td class="auto-style18">&nbsp;</td>
                                                                        <td class="auto-style16">&nbsp;</td>
                                                                        <td>&nbsp;</td>
                                                                        <td class="auto-style19">&nbsp;</td>
                                                                        <td>&nbsp;</td>
                                                                        <td>&nbsp;</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="auto-style13">&nbsp;</td>
                                                                        <td class="auto-style18">
                                                                            <dx:ASPxLabel ID="lblMensaje0" runat="server" Text="hghgh">
                                                                            </dx:ASPxLabel>
                                                                        </td>
                                                                        <td class="auto-style16">&nbsp;</td>
                                                                        <td>&nbsp;</td>
                                                                        <td class="auto-style19">&nbsp;</td>
                                                                        <td>&nbsp;</td>
                                                                        <td>&nbsp;</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="auto-style13">&nbsp;</td>
                                                                        <td class="auto-style18">&nbsp;</td>
                                                                        <td class="auto-style16">&nbsp;</td>
                                                                        <td>&nbsp;</td>
                                                                        <td class="auto-style19">
                                                                            <dx:ASPxButton ID="btnAceptar0" runat="server" AutoPostBack="False" ClientInstanceName="btnAceptar" Text="Aceptar" ForeColor="White" BackColor="#307D40" Width="72px">
                                                                                <ClientSideEvents Click="function(s, e) {
                                                                                        dgvTransacciones.UpdateEdit()	
                                                                                }" />
                                                                            </dx:ASPxButton>
                                                                        </td>
                                                                        <td>
                                                                            <dx:ASPxButton ID="btnCancelar0" runat="server" AutoPostBack="False" ClientInstanceName="btnCancelar" Text="Cerrar" ForeColor="White" BackColor="#307D40">
                                                                                <ClientSideEvents Click="function(s, e) {
                                                                                        dgvTransacciones.CancelEdit()	
                                                                                }" />
                                                                            </dx:ASPxButton>
                                                                        </td>
                                                                        <td>&nbsp;</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="auto-style13">&nbsp;</td>
                                                                        <td class="auto-style18">&nbsp;</td>
                                                                        <td class="auto-style16">&nbsp;</td>
                                                                        <td>&nbsp;</td>
                                                                        <td class="auto-style19">&nbsp;</td>
                                                                        <td>&nbsp;</td>
                                                                        <td>&nbsp;</td>
                                                                    </tr>
                                                                </table>
                                                            </EditForm>
                                                        </Templates>
                                                        <SettingsPager NumericButtonCount="5" PageSize="50">
                                                            <PageSizeItemSettings Items="50, 100, 200, 500, 1000" Visible="True">
                                                            </PageSizeItemSettings>
                                                        </SettingsPager>
                                                        <SettingsEditing Mode="PopupEditForm">
                                                        </SettingsEditing>
                                                        <Settings ShowHeaderFilterButton="True" />
                                                        <SettingsBehavior AllowFocusedRow="True" />
                                                        <SettingsPopup>
                                                            <EditForm Modal="True" />
                                                        </SettingsPopup>
                                                        <SettingsText CommandNew="Nuevo" />
                                                        <Columns>
                                                            <dx:GridViewCommandColumn ShowInCustomizationForm="True" Visible="False" VisibleIndex="0">
                                                            </dx:GridViewCommandColumn>
                                                            <dx:GridViewDataTextColumn Caption="ID MOV." FieldName="StrIdVenta" ShowInCustomizationForm="True" VisibleIndex="2" ToolTip="Identificador del Movimiento">
                                                                <Settings AutoFilterCondition="Contains" />
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="ERROR" FieldName="StrErrores" ShowInCustomizationForm="True" VisibleIndex="11">
                                                                <Settings AutoFilterCondition="Contains" />
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="NIT" FieldName="LngNit" ShowInCustomizationForm="True" VisibleIndex="5">
                                                                <Settings AutoFilterCondition="Contains" />
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="FECHA" FieldName="LngFechaEmision" ShowInCustomizationForm="True" VisibleIndex="4">
                                                                <Settings AutoFilterCondition="Contains" />
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="NO. FACTURA" FieldName="LngNroFactura" ShowInCustomizationForm="True" VisibleIndex="9" ToolTip="Número de Factura">
                                                                <Settings AutoFilterCondition="Contains" />
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="NO. AUTORIZACION" FieldName="LngNroAutorizacion" ShowInCustomizationForm="True" ToolTip="Número de Autorización" VisibleIndex="7">
                                                                <Settings AutoFilterCondition="Contains" />
                                                                <FilterTemplate>
                                                                    <dx:ASPxComboBox ID="cmbFiltroProd0" runat="server" ValueType="System.String">
                                                                    </dx:ASPxComboBox>
                                                                </FilterTemplate>
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="NO. REF." FieldName="LngNroReferencia" ShowInCustomizationForm="True" VisibleIndex="10" ToolTip="Número de Referencia">
                                                                <Settings AutoFilterCondition="Equals" />
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="#" FieldName="LngId" ShowInCustomizationForm="True" VisibleIndex="1" ToolTip="Funciones">
                                                            </dx:GridViewDataTextColumn>
                                                        </Columns>
                                                    </dx:ASPxGridView>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <dx:ASPxButton ID="btnExportar" runat="server" OnClick="btnExportar_Click" Text="Exportar Registros Observados" Theme="Metropolis" Font-Bold="True" ForeColor="Maroon">
                                                        <ClientSideEvents Click="function(s, e) {
	        e.processOnServer = confirm('¿Desea exportar los registros observados?'); 
        }" />
                                                        <Image IconID="export_exporttoxls_16x16">
                                                        </Image>
                                                    </dx:ASPxButton>
                                                </td>
                                                <td>
                                                    &nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>
                                                    &nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <dx:ASPxGridViewExporter ID="dgeExportar" runat="server">
                                                    </dx:ASPxGridViewExporter>
                                                </td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>
                                                    
                                                </td>
                                            </tr>

                                                                    </table>
                                                                </dx:ContentControl>
                                                            </ContentCollection>
                                                        </dx:TabPage>
                                                        <dx:TabPage Text="DECLARACION JURADA">
                                                            <ContentCollection>
                                                                <dx:ContentControl runat="server">
                                                                    <table style="width: 100%">
                                                                        <tr>
                                                                            <td>
                                                                                
                                                                                <dx:ASPxButton ID="btnDeclaracion" runat="server" AutoPostBack="false" Font-Bold="True" ForeColor="Maroon" Text="EMITIR DECLARACION JURADA" Theme="Metropolis" Width="280px">
                                                                                    <ClientSideEvents Click="function(s, e) {
    if(confirm('¿Desea emitir la Declaracion Jurada? - Una vez realizada la acción no podra modificar los datos del periodo seleccionado. ¿Desea continuar?'))
    {
    pucDatos.Show();
    pucDatos.PerformCallback();
    }
    }" />
                                                                                    <Image IconID="actions_addfile_16x16">
                                                                                    </Image>
                                                                                </dx:ASPxButton>
                                                                                
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                
                                                                                <dx:ASPxGridView ID="dgvDdjjCab" runat="server" AutoGenerateColumns="False" ClientInstanceName="dgvDdjjCab" EnableTheming="True" Font-Size="Smaller" KeyFieldName="ID" Theme="Default" Width="100%" OnHtmlRowPrepared="dgvDdjjCab_HtmlRowPrepared">
                                                                                    <ClientSideEvents CustomButtonClick="function(s, e) {
	
    switch(e.buttonID) {

    case&nbsp; 'btnDetalle':
    s.GetRowValues(e.visibleIndex, 'STR_MES', function (value) {
    lblTituloDetalle.SetValue('DETALLE DEL MES  : '+value);
    });
    dgvDdjjDet.PerformCallback();
    break;

    } 

    }" />
                                                                                    <SettingsContextMenu EnableRowMenu="False">
                                                                                    </SettingsContextMenu>
                                                                                    <Templates>
                                                                                        <EditForm>
                                                                                        </EditForm>
                                                                                    </Templates>
                                                                                    <SettingsPager PageSize="25">
                                                                                        <PageSizeItemSettings Items="25, 100, 1000" Visible="True" ShowAllItem="True">
                                                                                        </PageSizeItemSettings>
                                                                                    </SettingsPager>
                                                                                    <SettingsEditing Mode="PopupEditForm">
                                                                                    </SettingsEditing>
                                                                                    <Settings ShowFilterRow="True" ShowFooter="True" ShowHeaderFilterButton="True" ShowGroupPanel="True" />
                                                                                    <SettingsBehavior AllowFocusedRow="True" AutoExpandAllGroups="True" />
                                                                                    <SettingsPopup>
                                                                                        <EditForm Modal="True" />
                                                                                        <CustomizationWindow HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" />
                                                                                    </SettingsPopup>
                                                                                    <SettingsText CommandNew="Nuevo" />
                                                                                    <Columns>
                                                                                        <dx:GridViewCommandColumn ShowClearFilterButton="True" ShowInCustomizationForm="True" ToolTip="Funciones" VisibleIndex="8">
                                                                                            <CustomButtons>
                                                                                                <dx:GridViewCommandColumnCustomButton ID="btnDetalle" Text=" ">
                                                                                                    <Image IconID="actions_productcomparisons_16x16devav" ToolTip="VerDetalle">
                                                                                                    </Image>
                                                                                                </dx:GridViewCommandColumnCustomButton>
                                                                                            </CustomButtons>
                                                                                        </dx:GridViewCommandColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="PRODUCTO" FieldName="PRODUCTO" ShowInCustomizationForm="True" VisibleIndex="2" GroupIndex="0" SortIndex="0" SortOrder="Ascending">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="VOLUMEN TOTAL" FieldName="TOT_VOLUMEN" ShowInCustomizationForm="True" VisibleIndex="5">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                            <CellStyle HorizontalAlign="Right">
                                                                                            </CellStyle>
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="NO. FACTURAS" FieldName="CANT_FACT" ShowInCustomizationForm="True" VisibleIndex="6" ToolTip="NUMERO DE FACTURAS">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="ID_PRODUCTO" FieldName="ID_PRODUCTO" ShowInCustomizationForm="True" VisibleIndex="1" Visible="False">
                                                                                            <Settings AutoFilterCondition="Equals" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="MES" FieldName="STR_MES" ShowInCustomizationForm="True" VisibleIndex="4">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="IMPORTE TOTAL" FieldName="TOT_IMPORTE" ShowInCustomizationForm="True" VisibleIndex="7">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="ID" FieldName="ID" ShowInCustomizationForm="True" Visible="False" VisibleIndex="0">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="MES" FieldName="MES" ShowInCustomizationForm="True" VisibleIndex="3" Visible="False">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                    </Columns>
                                                                                    <Styles CssPostfix="DevGlp"><AlternatingRow BackColor="#EEEEEE">
                                                        </AlternatingRow></Styles>
                                                                                    <TotalSummary>
                                                                                        <dx:ASPxSummaryItem FieldName="TOT_VOLUMEN" ShowInColumn="TOT_VOLUMEN" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="CANT_FACT" ShowInColumn="CANT_FACT" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="TOT_IMPORTE" ShowInColumn="TOT_IMPORTE" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                    </TotalSummary>
                                                                                </dx:ASPxGridView>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <dx:ASPxLabel ID="lblTituloDetalle" runat="server" ClientInstanceName="lblTituloDetalle" Text="DETALLE DEL MES :" Font-Bold="True" ForeColor="#003300">
                                                                                </dx:ASPxLabel>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="auto-style23">
                                                                                <dx:ASPxGridView ID="dgvDdjjDet" runat="server" AutoGenerateColumns="False" ClientInstanceName="dgvDdjjDet" EnableTheming="True" Font-Size="Smaller" KeyFieldName="ID" OnCustomCallback="dgvDdjjDet_CustomCallback" Theme="Default" Width="100%" OnHtmlRowPrepared="dgvDdjjDet_HtmlRowPrepared">
                                                                                    <Styles>
                                                                                        <Header Wrap="True" HorizontalAlign="Center"></Header>
                                                                                    </Styles>
                                                                                    <SettingsContextMenu EnableRowMenu="False">
                                                                                    </SettingsContextMenu>
                                                                                    <Templates>
                                                                                        <EditForm>
                                                                                        </EditForm>
                                                                                    </Templates>
                                                                                    <SettingsPager PageSize="5">
                                                                                        <PageSizeItemSettings Items="5, 25, 100, 1000" Visible="True" ShowAllItem="True">
                                                                                        </PageSizeItemSettings>
                                                                                    </SettingsPager>
                                                                                    <SettingsEditing Mode="PopupEditForm">
                                                                                    </SettingsEditing>
                                                                                    <Settings ShowFilterRow="True" ShowFooter="True" ShowHeaderFilterButton="True" />
                                                                                    <SettingsBehavior AllowFocusedRow="True" />
                                                                                    <SettingsPopup>
                                                                                        <EditForm Modal="True" />
                                                                                        <CustomizationWindow HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" />
                                                                                    </SettingsPopup>
                                                                                    <SettingsText CommandNew="Nuevo" />
                                                                                    <Columns>
                                                                                        <dx:GridViewCommandColumn ShowClearFilterButton="True" ShowInCustomizationForm="True" ToolTip="Funciones" Visible="False" VisibleIndex="23">
                                                                                        </dx:GridViewCommandColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="PLACA" FieldName="IDENTIFICADOR" ShowInCustomizationForm="True" VisibleIndex="5">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="ID_PRODUCTO" FieldName="ID_PRODUCTO" ShowInCustomizationForm="True" Visible="False" VisibleIndex="6">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                            <CellStyle HorizontalAlign="Right">
                                                                                            </CellStyle>
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="PRODUCTO" FieldName="PRODUCTO" ShowInCustomizationForm="True" VisibleIndex="7" Visible="False">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="ID REGISTRO" FieldName="ID_INTEGRACION" ShowInCustomizationForm="True" VisibleIndex="4">
                                                                                            <Settings AutoFilterCondition="Equals" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="FECHA_OPERACION" FieldName="FECHA_OPERACION" ShowInCustomizationForm="True" Visible="False" VisibleIndex="2">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="NIT EMISOR" FieldName="FACT_NEM" ShowInCustomizationForm="True" VisibleIndex="10">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="ID" FieldName="ID" ShowInCustomizationForm="True" Visible="False" VisibleIndex="0">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="ID_MOVIMIENTO" FieldName="ID_MOVIMIENTO" ShowInCustomizationForm="True" Visible="False" VisibleIndex="1">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="NIT CLIENTE" FieldName="FACT_NIT" ShowInCustomizationForm="True" VisibleIndex="11">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="RAZON SOCIAL" FieldName="FACT_RSO" ShowInCustomizationForm="True" VisibleIndex="12">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="FACT. NO.AUT." FieldName="FACT_NAU" ShowInCustomizationForm="True" VisibleIndex="13">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="NO FACTURA" FieldName="FACT_NRO" ShowInCustomizationForm="True" VisibleIndex="14">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="CODIGO CONTROL" FieldName="FACT_CCT" ShowInCustomizationForm="True" VisibleIndex="15">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="CANTIDAD" FieldName="CANTIDAD" ShowInCustomizationForm="True" VisibleIndex="16">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="PRECIO" FieldName="FACT_PRECIO" ShowInCustomizationForm="True" VisibleIndex="17">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="IMPORTE" FieldName="FACT_IMPORTE" ShowInCustomizationForm="True" VisibleIndex="18">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="NRO. SERVICIO SUIC" FieldName="SUIC_NSR" ShowInCustomizationForm="True" VisibleIndex="19">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="NRO. DOCUMENTO SUIC" FieldName="SUIC_NDC" ShowInCustomizationForm="True" VisibleIndex="20">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="PRECIO SUIC" FieldName="SIUC_PRU" ShowInCustomizationForm="True" VisibleIndex="21">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="IMPORTE SUIC" FieldName="SIUC_IMP" ShowInCustomizationForm="True" VisibleIndex="22">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="FECHA" FieldName="STR_FECHA" ShowInCustomizationForm="True" VisibleIndex="3">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="ID_UNIDAD_MEDIDA" FieldName="ID_UNIDAD_MEDIDA" ShowInCustomizationForm="True" Visible="False" VisibleIndex="8">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="UNIDAD_MEDIDA" FieldName="UNIDAD_MEDIDA" ShowInCustomizationForm="True" Visible="False" VisibleIndex="9">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                    </Columns>
                                                                                    <TotalSummary>
                                                                                        <dx:ASPxSummaryItem FieldName="CANTIDAD" ShowInColumn="CANTIDAD" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="FACT_IMPORTE" ShowInColumn="FACT_IMPORTE" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="SIUC_IMP" ShowInColumn="SIUC_IMP" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                    </TotalSummary>
                                                                                </dx:ASPxGridView>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>&nbsp;</td>
                                                                        </tr>
                                                                    </table>
                                                                </dx:ContentControl>
                                                            </ContentCollection>
                                                        </dx:TabPage>
                                                        <dx:TabPage Text="MONITOREO">
                                                            <ContentCollection>
                                                                <dx:ContentControl runat="server">
                                                                    <table style="width: 100%">
                                                                        <tr>
                                                                            <td>
                                                                                
                                                                                

                                                                                <dx:ASPxButton ID="btnReimprimir" runat="server" Font-Bold="True" ForeColor="Maroon" OnClick="btnReimprimir_Click" Text="Reimprimir" Theme="Metropolis" Width="20%">
                                                                                    <Image IconID="print_printer_16x16">
                                                                                    </Image>
                                                                                </dx:ASPxButton>
                                                                                
                                                                                

                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <dx:ASPxGridView ID="dgvCabecera" runat="server" AutoGenerateColumns="False" ClientInstanceName="dgvCabecera" EnableTheming="True" Font-Size="Smaller" KeyFieldName="ID_REPORTE_DATO_PRINC" Theme="Default" Width="100%" OnCustomButtonCallback="dgvCabecera_CustomButtonCallback" OnHtmlRowPrepared="dgvCabecera_HtmlRowPrepared" OnCustomButtonInitialize="dgvCabecera_CustomButtonInitialize">
                                                                                    <ClientSideEvents CustomButtonClick="function(s, e) {
	
    switch(e.buttonID) {

    case&nbsp; 'btnHistorial':
    pucHistorial.Show();
    pucHistorial.PerformCallback();
    break;

    case  'btnPdf':
    s.GetRowValues(e.visibleIndex, 'ESTADO', function (value) {
    if(value==2){
    alert('No puede ejecutar la función debido a que la declaración se encuentra anulada.');
    }
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
                                                                                    <Settings ShowFilterRow="True" ShowFooter="True" ShowHeaderFilterButton="True" />
                                                                                    <SettingsBehavior AllowFocusedRow="True" />
                                                                                    <SettingsPopup>
                                                                                        <EditForm Modal="True" />
                                                                                        <CustomizationWindow HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" />
                                                                                    </SettingsPopup>
                                                                                    <SettingsText CommandNew="Nuevo" />
                                                                                    <Columns>
                                                                                        <dx:GridViewCommandColumn ShowClearFilterButton="True" ShowInCustomizationForm="True" ToolTip="Funciones" VisibleIndex="7" Name="opciones">
                                                                                            <CustomButtons>
                                                                                                <dx:GridViewCommandColumnCustomButton ID="btnHistorial" Text=" ">
                                                                                                    <Image IconID="functionlibrary_lookupreference_16x16" ToolTip="VerHistorial">
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
                                                                                        <dx:GridViewDataTextColumn Caption="ESTADO" FieldName="ESTADO" ShowInCustomizationForm="True" Visible="False" VisibleIndex="5">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="CODIGO" FieldName="CITE_GENERADO" ShowInCustomizationForm="True" VisibleIndex="2">
                                                                                            <Settings AutoFilterCondition="Equals" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="FECHA" FieldName="FECHA_REPORTE" ShowInCustomizationForm="True" Visible="False" VisibleIndex="1">
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
                                                                            <td>&nbsp;</td>
                                                                        </tr>
                                                                    </table>
                                                                </dx:ContentControl>
                                                            </ContentCollection>
                                                        </dx:TabPage>
                                                    </TabPages>
                                                </dx:ASPxPageControl>
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



<dx:ASPxPopupControl ID="pucDatos" runat="server" AllowDragging="True" ClientInstanceName="pucDatos" EnableTheming="True" HeaderText="Datos Adicionales" Height="93px" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Above" Theme="Metropolis" Width="600px" OnWindowCallback="pucDatos_WindowCallback">
    <HeaderImage IconID="mail_contact_32x32">
    </HeaderImage>
    <HeaderStyle>
    <BorderBottom BorderStyle="Solid" />
    </HeaderStyle>
    <ContentCollection>
        <dx:PopupControlContentControl runat="server">
            <table class="dxflInternalEditorTable_Youthful">
                
                <tr>
                    <td style="width: 30%">
                        <dx:ASPxLabel ID="ASPxLabel347" runat="server" Text="Lugar :" ForeColor="#666666" Font-Bold="True">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 70%">
                        <dx:ASPxComboBox ID="cmbDepartamento" runat="server" ClientInstanceName="cmbDepartamento" Theme="Metropolis" Width="170px" TextField="DESCRIPCION" TextFormatString="{0}" ValueField="ID_UBICACION_GEOGRAFICA">
                            <Columns>
                                <dx:ListBoxColumn Caption="DESCRIPCION" FieldName="DESCRIPCION" Name="DESCRIPCION" />
                                <dx:ListBoxColumn Caption="ABREVIACION2" FieldName="ABREVIACION2" Name="ABREVIACION2" Width="0px" />
                                <dx:ListBoxColumn Caption="ID_UBICACION_GEOGRAFICA" FieldName="ID_UBICACION_GEOGRAFICA" Name="ID_UBICACION_GEOGRAFICA" Width="0px" />
                            </Columns>
                            <ClearButton Visibility="Auto">
                            </ClearButton>
                        </dx:ASPxComboBox>
                    </td>
                </tr>
                <tr>
                    <td style="padding-bottom: 5px">
                        <dx:ASPxLabel ID="ASPxLabel344" runat="server" Font-Bold="True" ForeColor="#666666" Text="Código :">
                        </dx:ASPxLabel>
                    </td>
                    <td style="padding-bottom: 5px">
                        <dx:ASPxTextBox ID="txtPadCodProy" runat="server" MaxLength="12" ReadOnly="True" Width="170px">
                            <ReadOnlyStyle BackColor="#FFE1C4">
                            </ReadOnlyStyle>
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <dx:ASPxLabel ID="ASPxLabel348" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Underline="True" ForeColor="#333333" Text="Representante Legal" Width="100%">
                        </dx:ASPxLabel>
                    </td>

                </tr>
                <tr>
                    <td class="auto-style14">
                        <dx:ASPxLabel ID="ASPxLabel340" runat="server" ForeColor="#666666" Text="Nombre :" Font-Bold="True">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxComboBox ID="cmbRepresentante" runat="server" Width="350px">
                            <ClearButton Visibility="Auto">
                            </ClearButton>
                        </dx:ASPxComboBox>
                    </td>
                </tr>
                
                <tr>
                    <td colspan="2">
                        <dx:ASPxLabel ID="ASPxLabel345" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Underline="True" ForeColor="#333333" Text="Propietario" Width="100%">
                        </dx:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel346" runat="server" Font-Bold="True" ForeColor="#666666" Text="Nombre :">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="txtPadPropietario" runat="server" ReadOnly="True" Width="350px">
                            <ClientSideEvents TextChanged="function(s, e) {
	s.SetText(s.GetText().toUpperCase());
}" />
                            <ReadOnlyStyle BackColor="#FFE1C4">
                            </ReadOnlyStyle>
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                
                <tr>
                    <td colspan="2">
                        <dx:ASPxLabel ID="ASPxLabel349" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Underline="True" ForeColor="#333333" Text="Usuario Emisor" Width="100%">
                        </dx:ASPxLabel>
                    </td>

                </tr>

                <tr>
                    <td class="auto-style14">
                        <dx:ASPxLabel ID="ASPxLabel342" runat="server" ForeColor="#666666" Text="Nombre :" Font-Bold="True">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="txtPadUsuEmisor" runat="server" Width="350px" ReadOnly="True">
                            <ClientSideEvents TextChanged="function(s, e) {
	s.SetText(s.GetText().toUpperCase());
}" />
                            <ReadOnlyStyle BackColor="#FFE1C4">
                            </ReadOnlyStyle>
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td style="padding-bottom: 10px">
                        <dx:ASPxLabel ID="ASPxLabel343" runat="server" ForeColor="#666666" Text="C.I. :" Font-Bold="True">
                        </dx:ASPxLabel>
                    </td>
                    <td style="padding-bottom: 10px">
                        <dx:ASPxTextBox ID="txtPadUsuEmisorCi" runat="server" MaxLength="12" Width="170px" ReadOnly="True">
                            <ReadOnlyStyle BackColor="#FFE1C4">
                            </ReadOnlyStyle>
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td style="padding-bottom: 10px">
                        <dx:ASPxLabel ID="ASPxLabel350" runat="server" ForeColor="#666666" Text="Observación :" Width="100%" Font-Bold="True">
                        </dx:ASPxLabel>
                    </td>
                    <td style="padding-bottom: 10px">
                        <dx:ASPxTextBox ID="txtPadObserv" runat="server" MaxLength="50" Width="350px">
                            <ClientSideEvents LostFocus="function(s, e) {
	s.SetText(s.GetText().toUpperCase());
}" />
                            <MaskSettings PromptChar=" " />
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" class="auto-style13">
                        <dx:ASPxButton ID="btnEmiteDeclaracion" runat="server" EnableTheming="True" Font-Bold="True" ForeColor="Maroon" OnClick="btnEmiteDeclaracion_Click" Text="EMITIR DECLARACION JURADA" Theme="Metropolis" Width="100%" AutoPostBack="false">
                            <ClientSideEvents Click="function(s, e) {
if(confirm('¿Desea emitir la Declaracion Jurada? - Una vez realizada la acción no podra modificar los datos del periodo seleccionado. ¿Desea continuar?'))
{
LoadingPanel.Show();
e.processOnServer = true; 
}
else{
e.processOnServer = false; 
}
}
" />
                            <Image IconID="actions_addfile_16x16">
                            </Image>
                        </dx:ASPxButton>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center;">
                        <dx:ASPxRoundPanel ID="pnlPuMensaje" runat="server" ShowHeader="False" Visible="False" Width="100%" ClientInstanceName="pnlPuMensaje">
                            <PanelCollection>
                                <dx:PanelContent runat="server">
                                    <dx:ASPxLabel ID="lblPuMensajeError" runat="server" ForeColor="#666666" style="text-align:center">
                                    </dx:ASPxLabel>
                                </dx:PanelContent>
                            </PanelCollection>
                            <Border BorderStyle="Ridge" BorderWidth="1px" />
                        </dx:ASPxRoundPanel>
                    </td>
                </tr>
            </table>
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>































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
                        <dx:ASPxLabel ID="ASPxLabel351" runat="server" Text="Declaración Jurada:" Font-Bold="True" ForeColor="#333333">
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
                        
                        <dx:ASPxGridView ID="dgvHistorico" runat="server" AutoGenerateColumns="False" ClientInstanceName="dgvHistorico" EnableTheming="True" Font-Size="Smaller" KeyFieldName="APP_FECHA_REGISTRO" Theme="Default" Width="100%">
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
                        <dx:ASPxLabel ID="ASPxLabel352" runat="server" Font-Bold="True" ForeColor="#333333" Text="Declaración Jurada:">
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
                        <dx:ASPxLabel ID="ASPxLabel353" runat="server" Font-Bold="True" ForeColor="#333333" Text="Observaciones :">
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
                        <dx:ASPxLabel ID="ASPxLabel354" runat="server" Font-Bold="True" ForeColor="#333333" Text="Archivo (PDF) :">
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
                        <dx:ASPxLabel ID="ASPxLabel355" runat="server" Text="Declaración Jurada:" Font-Bold="True" ForeColor="#333333">
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