<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtrImportacionDatos.ascx.cs" Inherits="AnhHydroOctanoPresentacion.Controles.CtrImportacionDatos" %>

<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>

<style type="text/css">
    .auto-style1 {
        height: 18px;
    }

    .auto-style5 {
        width: 168px;
    }

    .auto-style6 {
        height: 18px;
        width: 138px;
    }

    .auto-style9 {
        width: 138px;
    }

    .panelPersona {
        position: fixed;
        top: 410px;
        width: 500px;
        background: white;
    }

    .hydro_smart_container {
        margin: 0;
        padding: 0;
        Height: 690px;
    }

    .hydro_smart_header {
        margin: 0;
        padding: 5px 10px;
        /*background: orange;*/
        background: rgb(0, 132, 38);
        color: white;
        font-size: medium;
        font-weight: bold;
    }

    .hydro_smart_table {
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
        color: #555;
        border-radius: 10px;
        font-family: Tahoma,Geneva,Arial,sans-serif;
        font-size: 11px;
        padding: 10px 10px 10px 36px;
        margin: 10px;
}
</style>

<div class="col-md-12">
    <table style="width: 100%">
        <tr>
            <td style="text-align: center; padding-bottom: 1px">
                <dx:ASPxRoundPanel runat="server" ShowHeader="False" Width="100%" ID="pnlTitulo">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <dx:ASPxLabel runat="server" ForeColor="#367B2F" ID="lblTitulo" Font-Bold="True" Font-Names="Rockwell" Font-Size="Larger" Text="IMPORTACIÓN MASIVA DE MOVIMIENTOS"></dx:ASPxLabel>
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
                                                            <dx:ASPxLabel ID="ASPxLabel56" runat="server" ForeColor="#003300" Text="Periodo :">
                                                            </dx:ASPxLabel>
                                                            <dx:ASPxComboBox ID="cmbPeriodo" runat="server" EnableTheming="True" Font-Bold="True" Theme="Metropolis" ValueType="System.Int32" Width="160px" ToolTip="Seleccione un Periodo" ClientInstanceName="cmbPeriodo">
                                                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
	    pnlDatos.PerformCallback();
    }" />
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
                                                            <dx:ASPxLabel ID="ASPxLabel57" runat="server" ForeColor="#003300" Text="Tipo de Operación :">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxComboBox ID="cmbTipoOperacion" runat="server" ClientInstanceName="cmbTipoOperacion" TextField="NOMBRE" TextFormatString="{0};{2};" Theme="Metropolis" ValueField="ID_TIPO_OPERACION" Width="160px" ToolTip="Seleccione un tipo de operación">
                                                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
	    pnlDatos.PerformCallback();
    }" />
                                                                <Columns>
                                                                    <dx:ListBoxColumn Caption="NOMBRE" FieldName="NOMBRE" Name="NOMBRE" Width="300px" />
                                                                    <dx:ListBoxColumn Caption="ID_TIPO_OPERACION" FieldName="ID_TIPO_OPERACION" Name="ID_TIPO_OPERACION" Width="0px" />
                                                                    <dx:ListBoxColumn Caption="CODIGO" FieldName="CODIGO" Name="CODIGO" Width="0px" />
                                                                    <dx:ListBoxColumn Caption="ORDEN" FieldName="ORDEN" Name="ORDEN" Width="0px" />
                                                                    <dx:ListBoxColumn Caption="LIMITE_MINIMO" FieldName="LIMITE_MINIMO" Name="LIMITE_MINIMO" Width="0px" />
                                                                    <dx:ListBoxColumn Caption="LIMITE_MAXIMO" FieldName="LIMITE_MAXIMO" Name="LIMITE_MAXIMO" Width="0px" />
                                                                </Columns>
                                                                <ClearButton Visibility="Auto">
                                                                </ClearButton>
                                                            </dx:ASPxComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-bottom: 5px">
                                                            <dx:ASPxButton ID="btnDescargar" runat="server" Font-Bold="True" ForeColor="Maroon" OnClick="btnDescargar_Click" Text="Descargar Plantilla" Theme="Metropolis" Width="160px" ToolTip="Descarga la plantilla del tipo de operación seleccionado">
                                                                <Image IconID="actions_download_16x16">
                                                                </Image>
                                                            </dx:ASPxButton>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-bottom: 5px">                                                            
                                                        </td>
                                                    </tr>
                                                </table>

                                            </dx:PanelContent>
                                        </PanelCollection>
                                    </dx:ASPxCallbackPanel>
                                </td>
                            </tr>
                            
                            <tr>
                                <td></td>
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
                                    <dx:ASPxButton ID="btnBuscarProyecto" runat="server" AutoPostBack="false" ClientInstanceName="btnBuscarProyecto" Text="Buscar Proyecto">
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

                                        <ClientSideEvents FocusedNodeChanged="function(s, e) {
pnlCabecera.PerformCallback();
dgvTransacciones.PerformCallback();
}"
                                            NodeDblClick="function(s, e) {
pnlCabecera.PerformCallback();
dgvTransacciones.PerformCallback();
}"
                                            EndCallback="function(s, e) {
pnlCabecera.PerformCallback();
dgvTransacciones.PerformCallback();
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
}"
                                BeginCallback="function(s, e) {
LoadingPanel.Show();
}" />
                            <PanelCollection>
                                <dx:PanelContent runat="server">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td colspan="6" style="text-align: center;">
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
                                            <td colspan="6">
                                                <legend>
                                                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Rockwell" Font-Size="Medium" ForeColor="#367B2F" Text="DATOS DEL PROYECTO"></asp:Label>
                                                </legend>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="#10420F" Text="NOMBRE COMERCIAL"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label3" runat="server" Font-Bold="True" ForeColor="#10420F" Text="NIT"></asp:Label>
                                            </td>
                                            <td colspan="2">
                                                <asp:Label ID="Label4" runat="server" Font-Bold="True" ForeColor="#10420F" Text="DIRECCION"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" style="padding-bottom: 5px; width: 50%">
                                                <asp:Label ID="lblRazonSocial" runat="server"></asp:Label>
                                            </td>
                                            <td style="padding-bottom: 5px; width: 10%">
                                                <asp:Label ID="lblNit" runat="server"></asp:Label>
                                            </td>
                                            <td colspan="2" style="padding-bottom: 5px; width: 40%">
                                                <asp:Label ID="lblDireccion" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30%">
                                                <asp:FileUpload ID="FileUploader" runat="server" ToolTip="Seleccione el archivo .CSV" Width="100%" />
                                            </td>
                                            <td colspan="4" style="width: 60%">
                                                <dx:ASPxButton ID="btnImportar" runat="server" AutoPostBack="False" Enabled="False" Text="Visualizar Registros" Theme="Metropolis" OnClick="btnImportar_Click" Font-Bold="True" ForeColor="Maroon">
                                                    <ClientSideEvents Click="function(s, e) {
	    LoadingPanel.Show();
    }" />
                                                    <Image IconID="zoom_zoom_16x16">
                                                    </Image>
                                                </dx:ASPxButton>
                                                <dx:ASPxButton ID="btnGrabar" runat="server" OnClick="btnGrabar_Click" Text="Importar Registros" Theme="Metropolis" Enabled="False" Font-Bold="True" ForeColor="Maroon">
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
                                                
                                                <dx:ASPxButton ID="btnLimpiaTo" runat="server" Enabled="False" Font-Bold="True" ForeColor="Maroon" Text="Eliminar Mov. por  T.Op." Theme="Metropolis" ToolTip="Eliminar los Movimientos del Tipo de Operacion del Periodo" Width="160px" OnClick="btnLimpiaTo_Click">
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
                                                <dx:ASPxGridView ID="dgvTransacciones" runat="server" Theme="Default" AutoGenerateColumns="False" KeyFieldName="LngId" EnableTheming="True" Font-Size="Smaller" ClientInstanceName="dgvTransacciones" Width="100%" OnCustomCallback="dgvTransacciones_CustomCallback">
                                                    <SettingsContextMenu EnableRowMenu="False">
                                                    </SettingsContextMenu>
                                                    <Templates>
                                                        <EditForm>
                                                            <table>
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="ID :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtId" runat="server" ClientInstanceName="txtId" Width="170px" ReadOnly="True">
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
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
                                                                    <td>
                                                                        <br />
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
                                                                    <td>
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
                                                                        <dx:ASPxComboBox ID="cmbTipoFact" runat="server" Font-Size="Smaller" Style="margin-top: 0" ValueType="System.Int32" Visible="False">
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
                                                                    <td>&nbsp;</td>
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
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
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
                                                                    <td>&nbsp;</td>
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
                                                        <dx:GridViewDataTextColumn Caption="PRODUCTO" FieldName="StrProducto" ShowInCustomizationForm="True" VisibleIndex="14">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="CANTIDAD" FieldName="DecVolumen" ShowInCustomizationForm="True" VisibleIndex="16" ToolTip="Tipo de Factura">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="PRECIO" FieldName="DecPrecio" ShowInCustomizationForm="True" VisibleIndex="15">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="IMPORTE" FieldName="DecImporte" ShowInCustomizationForm="True" VisibleIndex="17">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="NO. FACTURA" FieldName="LngNroFactura" ShowInCustomizationForm="True" VisibleIndex="11" ToolTip="Número de Factura">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="NRO. AUTORIZACION" FieldName="LngNroAutorizacion" ShowInCustomizationForm="True" VisibleIndex="9" ToolTip="Número de Autorización">
                                                            <Settings AutoFilterCondition="Contains" />
                                                            <FilterTemplate>
                                                                <dx:ASPxComboBox ID="cmbFiltroProd" runat="server" ValueType="System.String">
                                                                </dx:ASPxComboBox>
                                                            </FilterTemplate>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="NO. REF." FieldName="StrNroReferencia" ShowInCustomizationForm="True" VisibleIndex="12" ToolTip="Número de Referencia">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn ShowInCustomizationForm="True" VisibleIndex="1" Caption="#" FieldName="LngId" ToolTip="Funciones">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="FECHA" FieldName="StrFechaEmision" ShowInCustomizationForm="True" VisibleIndex="3">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="ESTADO" FieldName="StrEstado" ShowInCustomizationForm="True" VisibleIndex="5" ToolTip="Estado del Movimiento">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="NO.AUT.VTA." FieldName="LngNroAutVenta" ShowInCustomizationForm="True" VisibleIndex="13" ToolTip="Número de Autorización de Venta">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="COD. AUTORIZACION" FieldName="strCodigoAutorizacion" ShowInCustomizationForm="True" ToolTip="Codigo de Autorización" VisibleIndex="10">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                    <Styles CssPostfix="DevGlp">
                                                        <AlternatingRow BackColor="#EEEEEE">
                                                        </AlternatingRow>
                                                    </Styles>
                                                </dx:ASPxGridView>
                                                <dx:ASPxGridView ID="dgvTransaccionRefineria" runat="server" Theme="Default" AutoGenerateColumns="False" KeyFieldName="LngId" EnableTheming="True" Font-Size="Smaller" ClientInstanceName="dgvTransaccionRefineria" Width="100%" OnCustomCallback="dgvTransaccionRefineria_CustomCallback" Visible="False">
                                                    <SettingsContextMenu EnableRowMenu="False">
                                                    </SettingsContextMenu>
                                                    <Templates>
                                                        <EditForm>
                                                            <table>
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="ID :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtId" runat="server" ClientInstanceName="txtId" Width="170px" ReadOnly="True">
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
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
                                                                    <td>
                                                                        <br />
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
                                                                    <td>
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
                                                                        <dx:ASPxComboBox ID="cmbTipoFact" runat="server" Font-Size="Smaller" Style="margin-top: 0" ValueType="System.Int32" Visible="False">
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
                                                                    <td>&nbsp;</td>
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
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
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
                                                                    <td>&nbsp;</td>
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
                                                        <dx:GridViewDataTextColumn ShowInCustomizationForm="True" VisibleIndex="1" Caption="#" FieldName="LngId" ToolTip="Funciones">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="FECHA" FieldName="DecFecha" ShowInCustomizationForm="True" VisibleIndex="13" ToolTip="Número de Autorización de Venta">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="NO. SIS. ORI." FieldName="DecIdOrigen" ShowInCustomizationForm="True" VisibleIndex="14" ToolTip="Número de Autorización de Venta">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="TIP. INFR." FieldName="StrTInfraestructura" ShowInCustomizationForm="True" VisibleIndex="15" ToolTip="Número de Autorización de Venta">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="NO.AUT.VTA." FieldName="StrInfraestructura" ShowInCustomizationForm="True" VisibleIndex="16" ToolTip="Número de Autorización de Venta">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>


                                                        <dx:GridViewDataTextColumn Caption="INFRAESTRUCTURA" FieldName="StrProyOrigen" ShowInCustomizationForm="True" VisibleIndex="17" ToolTip="Número de Autorización de Venta">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="PROY. ORIGEN" FieldName="StrPais" ShowInCustomizationForm="True" VisibleIndex="18" ToolTip="Número de Autorización de Venta">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="PAIS" FieldName="StrIdentificador" ShowInCustomizationForm="True" VisibleIndex="19" ToolTip="Número de Autorización de Venta">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="IDENTIF." FieldName="StrLote" ShowInCustomizationForm="True" VisibleIndex="20" ToolTip="Número de Autorización de Venta">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="LOTE" FieldName="StrDui" ShowInCustomizationForm="True" VisibleIndex="21" ToolTip="Número de Autorización de Venta">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>

                                                        <dx:GridViewDataTextColumn Caption="DUI" FieldName="StrNroRecinto" ShowInCustomizationForm="True" VisibleIndex="22" ToolTip="Número de Autorización de Venta">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="NO. PRESIN." FieldName="StrCodigoTanqueCisterna" ShowInCustomizationForm="True" VisibleIndex="23" ToolTip="Número de Autorización de Venta">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="COD. TAN. CIS." FieldName="DecNroFactura" ShowInCustomizationForm="True" VisibleIndex="24" ToolTip="Número de Autorización de Venta">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="NO. FAC." FieldName="DecNroAutorizacion" ShowInCustomizationForm="True" VisibleIndex="25" ToolTip="Número de Autorización de Venta">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="NO. AUT." FieldName="StrCodigoControl" ShowInCustomizationForm="True" VisibleIndex="26" ToolTip="Número de Autorización de Venta">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="COD. CTRL." FieldName="DecNit" ShowInCustomizationForm="True" VisibleIndex="27" ToolTip="Número de Autorización de Venta">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="NIT" FieldName="StrNroReferencia" ShowInCustomizationForm="True" VisibleIndex="28" ToolTip="Número de Autorización de Venta">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="RAZ. SOC." FieldName="StrCertificadoRecepcion" ShowInCustomizationForm="True" VisibleIndex="29" ToolTip="Número de Autorización de Venta">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="NO. REF." FieldName="StrCIUsuario" ShowInCustomizationForm="True" VisibleIndex="30" ToolTip="Número de Autorización de Venta">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="CERT. RECEP." FieldName="StrEstado" ShowInCustomizationForm="True" VisibleIndex="31" ToolTip="Número de Autorización de Venta">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="CI. USU." FieldName="StrProducto" ShowInCustomizationForm="True" VisibleIndex="32" ToolTip="Número de Autorización de Venta">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="ESTADO" FieldName="StrRazonSocial" ShowInCustomizationForm="True" VisibleIndex="33" ToolTip="Número de Autorización de Venta">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="PRODUCTO" FieldName="DecCantidad" ShowInCustomizationForm="True" VisibleIndex="34" ToolTip="Número de Autorización de Venta">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="PRECIO" FieldName="DecPrecio" ShowInCustomizationForm="True" VisibleIndex="35" ToolTip="Número de Autorización de Venta">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="IMPORTE" FieldName="DecImporte" ShowInCustomizationForm="True" VisibleIndex="36" ToolTip="Número de Autorización de Venta">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                    <Styles CssPostfix="DevGlp">
                                                        <AlternatingRow BackColor="#EEEEEE">
                                                        </AlternatingRow>
                                                    </Styles>
                                                </dx:ASPxGridView>
                                                <dx:ASPxGridView ID="dgvTransTransporteDuctos" runat="server" Theme="Default" AutoGenerateColumns="False" KeyFieldName="LngId" EnableTheming="True" Font-Size="Smaller" ClientInstanceName="dgvTransTransporteDuctos" Width="100%" OnCustomCallback="dgvTransTransporteDuctos_CustomCallback" Visible="False">
                                                    <SettingsContextMenu EnableRowMenu="False">
                                                    </SettingsContextMenu>                                                    
                                                    <SettingsPager PageSize="100">
                                                        <PageSizeItemSettings Items="50, 100, 200, 500, 1000" Visible="True">
                                                        </PageSizeItemSettings>
                                                    </SettingsPager>
                                                    <SettingsEditing Mode="PopupEditForm">
                                                    </SettingsEditing>
                                                    <Settings ShowHeaderFilterButton="True" />
                                                    <SettingsBehavior AllowFocusedRow="True" />
                                                    <Columns>                                                        
                                                        <dx:GridViewDataTextColumn ShowInCustomizationForm="True" VisibleIndex="0" Caption="#" FieldName="LngId" ToolTip="Registro">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="FECHA" FieldName="StrFecha" ShowInCustomizationForm="True" VisibleIndex="1" ToolTip="Fecha del transporte">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="DUCTO" FieldName="StrDucto" ShowInCustomizationForm="True" VisibleIndex="2" ToolTip="Ducto">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="TRANSACCION" FieldName="StrTransaccion" ShowInCustomizationForm="True" VisibleIndex="3" ToolTip="Transaccion">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="POI" FieldName="StrPoi" ShowInCustomizationForm="True" VisibleIndex="4" ToolTip="POI">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="NOMBRE" FieldName="StrNombre" ShowInCustomizationForm="True" VisibleIndex="5" ToolTip="Nombre">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="POI ACTUAL" FieldName="StrPoiActual" ShowInCustomizationForm="True" VisibleIndex="6" ToolTip="POI Actual">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="NOMBRE CARGADOR" FieldName="StrCargador" ShowInCustomizationForm="True" VisibleIndex="7" ToolTip="Cargador">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="USUARIO" FieldName="StrUsuario" ShowInCustomizationForm="True" VisibleIndex="8" ToolTip="Usuario">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="LOCALIDAD" FieldName="StrLocalidad" ShowInCustomizationForm="True" VisibleIndex="9" ToolTip="Localidad">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="DEPARTAMENTO" FieldName="StrDepartamento" ShowInCustomizationForm="True" VisibleIndex="10" ToolTip="Departamento">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="PRODUCTO" FieldName="StrProducto" ShowInCustomizationForm="True" VisibleIndex="11" ToolTip="Producto">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="VOLUMEN (PCS)" FieldName="StrVolumen" ShowInCustomizationForm="True" VisibleIndex="12" ToolTip="Volumen">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="SISTEMA" FieldName="StrSistema" ShowInCustomizationForm="True" VisibleIndex="13" ToolTip="Sistema">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="PODER CALORIFICO" FieldName="StrPoderCalorifico" ShowInCustomizationForm="True" VisibleIndex="14" ToolTip="Poder Calorifico">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="TIPO TRANSPORTE" FieldName="StrTipoTransporte" ShowInCustomizationForm="True" VisibleIndex="15" ToolTip="Tipo Transporte">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>                                                                                                                
                                                    </Columns>
                                                    <Styles CssPostfix="DevGlp">
                                                        <AlternatingRow BackColor="#EEEEEE">
                                                        </AlternatingRow>
                                                    </Styles>
                                                </dx:ASPxGridView>                                                

                                                <dx:ASPxGridView ID="dgvTransRedesGas" runat="server" Theme="Default" AutoGenerateColumns="False" KeyFieldName="LngId" EnableTheming="True" Font-Size="Smaller" ClientInstanceName="dgvTransTransporteDuctos" Width="100%" OnCustomCallback="dgvTransTransporteDuctos_CustomCallback" Visible="False">
                                                    <SettingsContextMenu EnableRowMenu="False">
                                                    </SettingsContextMenu>                                                    
                                                    <SettingsPager PageSize="1000">
                                                        <PageSizeItemSettings Items="50, 100, 200, 500, 1000" Visible="True">
                                                        </PageSizeItemSettings>
                                                    </SettingsPager>
                                                    <SettingsEditing Mode="PopupEditForm">
                                                    </SettingsEditing>
                                                    <Settings ShowHeaderFilterButton="True" />
                                                    <SettingsBehavior AllowFocusedRow="True" />
                                                    <Columns>  
                                                        <dx:GridViewDataTextColumn Caption="COD. USUARIO" FieldName="StrCodUsuario" ShowInCustomizationForm="True" VisibleIndex="0" ToolTip="Sistema">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="COD. UBICACIÓN" FieldName="StrCodUbicacion" ShowInCustomizationForm="True" VisibleIndex="1" ToolTip="Volumen">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="CIUDAD/POBLACION" FieldName="StrCiudadPoblacion" ShowInCustomizationForm="True" VisibleIndex="2" ToolTip="Tipo Transporte">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn> 
                                                        <dx:GridViewDataTextColumn Caption="CATEGORIA" FieldName="StrCategoria" ShowInCustomizationForm="True" VisibleIndex="3" ToolTip="Departamento">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="NIT" FieldName="DecNit" ShowInCustomizationForm="True" VisibleIndex="4" ToolTip="Tipo Transporte">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="RAZON SOCIAL" FieldName="StrRazonSocial" ShowInCustomizationForm="True" VisibleIndex="5" ToolTip="Tipo Transporte">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="DIRECCIÓN" FieldName="StrDireccion" ShowInCustomizationForm="True" VisibleIndex="6" ToolTip="Tipo Transporte">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="SECTOR <br/> ECONOMICO" FieldName="StrSectorEcon" ShowInCustomizationForm="True" VisibleIndex="7" ToolTip="Producto">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <%--<dx:GridViewDataTextColumn Caption="PRODUCTO" FieldName="StrProducto" ShowInCustomizationForm="True" VisibleIndex="8" ToolTip="Transaccion">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>--%>
                                                       <%-- <dx:GridViewDataTextColumn Caption="TIPO MEDIDOR" FieldName="StrTipoMedidor" ShowInCustomizationForm="True" VisibleIndex="8" ToolTip="Tipo Medidor">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>--%>
                                                        <dx:GridViewDataTextColumn Caption="COD Ó &lt;br/&gt;SERIE DE MEDIDOR" FieldName="DecCodMedidor" ShowInCustomizationForm="True" VisibleIndex="8" ToolTip="Poder Calorifico">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="FECHA&lt;br/&gt;LECTURA" FieldName="StrFecha" ShowInCustomizationForm="True" VisibleIndex="9" ToolTip="Fecha del transporte">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="LECTURA&lt;br/&gt; MEDIDOR M3" FieldName="DecLecMedidor" ShowInCustomizationForm="True" VisibleIndex="10" ToolTip="Ducto">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                       <%-- <dx:GridViewDataTextColumn Caption="VOLUMEN NO CORREGIDO (M3)" FieldName="DecCantNoCorregido" ShowInCustomizationForm="True" VisibleIndex="12" ToolTip="POI">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="TEMPERATURA C°" FieldName="DecTemperatura" ShowInCustomizationForm="True" VisibleIndex="13" ToolTip="Cargador">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="PRESION DE FLUJO (PSIG)" FieldName="DecPresionPsig" ShowInCustomizationForm="True" VisibleIndex="14" ToolTip="Usuario">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="FACTOR CORRECIÓN GLOBAL" FieldName="DecFactorCorrec" ShowInCustomizationForm="True" VisibleIndex="15" ToolTip="POI Actual">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>--%>
                                                        <dx:GridViewDataTextColumn Caption="VOLUMEN COMERCIALIZADO <br/> FACTURABLE (MPCS)" FieldName="DecCantFactura" ShowInCustomizationForm="True" VisibleIndex="11" ToolTip="Nombre">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                      <%--  <dx:GridViewDataTextColumn Caption="PRECIO" FieldName="DecPrecio" ShowInCustomizationForm="True" VisibleIndex="17" ToolTip="PRECIO UNITARIO">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>--%>
                                                        <dx:GridViewDataTextColumn Caption="IMPORTE X CONSUMO &lt;br/&gt; DE GN (Bs)" FieldName="DecImporte" ShowInCustomizationForm="True" VisibleIndex="12" ToolTip="Tipo Transporte">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="CARGOS &lt;br/&gt;ADICIONANALES (Bs)" FieldName="DecImportAdicional" ShowInCustomizationForm="True" VisibleIndex="13" ToolTip="Tipo Transporte">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                       
                                                        <dx:GridViewDataTextColumn Caption="DESCUENTO" FieldName="DecImportDescuento" ShowInCustomizationForm="True" VisibleIndex="14" ToolTip="Descuento">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="IMP. TOTAL &lt;br/&gt;FACTURADO" FieldName="DecImporte" ShowInCustomizationForm="True" VisibleIndex="15" ToolTip="Importe Total Facturado">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>

                                                        <dx:GridViewDataTextColumn Caption="N° FACTURA" FieldName="DecNroFactura" ShowInCustomizationForm="True" VisibleIndex="16" ToolTip="Tipo Transporte">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="N° AUTORIZACION" FieldName="DecNroAutorizacion" ShowInCustomizationForm="True" VisibleIndex="17" ToolTip="Nro Autorizacion">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="CODIGO CONTROL" FieldName="StrCodigoControl" ShowInCustomizationForm="True" VisibleIndex="19" ToolTip="Tipo Transporte">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="FECHA DE EMISION &lt;br/&gt;DE LA FACTURACION" FieldName="StrFechaFactura" ShowInCustomizationForm="True" VisibleIndex="20" ToolTip="Tipo Transporte">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>   
                                                        <%--                                                   
                                                        <dx:GridViewDataTextColumn ShowInCustomizationForm="True" VisibleIndex="0" Caption="#" FieldName="LngId" ToolTip="Registro">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="FECHA LECTURA" FieldName="StrFecha" ShowInCustomizationForm="True" VisibleIndex="1" ToolTip="Fecha del transporte">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="LECTURA MEDIDOR" FieldName="DecLecMedidor" ShowInCustomizationForm="True" VisibleIndex="2" ToolTip="Ducto">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="PRODUCTO" FieldName="StrProducto" ShowInCustomizationForm="True" VisibleIndex="3" ToolTip="Transaccion">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="CANTIDAD LECTURA" FieldName="DecCantLectura" ShowInCustomizationForm="True" VisibleIndex="4" ToolTip="POI">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="CANTIDAD FACTURA" FieldName="DecCantFactura" ShowInCustomizationForm="True" VisibleIndex="5" ToolTip="Nombre">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="FACTOR CORRECION" FieldName="DecFactorCorrec" ShowInCustomizationForm="True" VisibleIndex="6" ToolTip="POI Actual">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="TEMPERATURA" FieldName="DecTemperatura" ShowInCustomizationForm="True" VisibleIndex="7" ToolTip="Cargador">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="PRESION PSIG" FieldName="DecPresionPsig" ShowInCustomizationForm="True" VisibleIndex="8" ToolTip="Usuario">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="ESTADO" FieldName="StrEstado" ShowInCustomizationForm="True" VisibleIndex="9" ToolTip="Localidad">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="CATEGORIA" FieldName="StrCategoria" ShowInCustomizationForm="True" VisibleIndex="10" ToolTip="Departamento">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="SECTOR ECONOMICO" FieldName="StrSectorEcon" ShowInCustomizationForm="True" VisibleIndex="11" ToolTip="Producto">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="CODIO UBICACION" FieldName="StrCodUbicacion" ShowInCustomizationForm="True" VisibleIndex="12" ToolTip="Volumen">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="CODIGO USUARIO" FieldName="StrCodUsuario" ShowInCustomizationForm="True" VisibleIndex="13" ToolTip="Sistema">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="CODIGO MEDIDOR" FieldName="DecCodMedidor" ShowInCustomizationForm="True" VisibleIndex="14" ToolTip="Poder Calorifico">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="FORMA PAGO" FieldName="StrFormaPago" ShowInCustomizationForm="True" VisibleIndex="15" ToolTip="Tipo Transporte">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="NIT" FieldName="DecNit" ShowInCustomizationForm="True" VisibleIndex="15" ToolTip="Tipo Transporte">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="RAZON SOCIAL" FieldName="StrRazonSocial" ShowInCustomizationForm="True" VisibleIndex="15" ToolTip="Tipo Transporte">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="DIRECCION" FieldName="StrDireccion" ShowInCustomizationForm="True" VisibleIndex="15" ToolTip="Tipo Transporte">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="NRO FACTURA" FieldName="DecNroFactura" ShowInCustomizationForm="True" VisibleIndex="15" ToolTip="Tipo Transporte">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="NRO AUTORIZACION" FieldName="DecNroAutorizacion" ShowInCustomizationForm="True" VisibleIndex="15" ToolTip="Tipo Transporte">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="CODIGO CONTROL" FieldName="StrCodigoControl" ShowInCustomizationForm="True" VisibleIndex="15" ToolTip="Tipo Transporte">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="FECHA FACTURA" FieldName="StrFechaFactura" ShowInCustomizationForm="True" VisibleIndex="15" ToolTip="Tipo Transporte">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn> --%>
                                                        
                                                        <%--<dx:GridViewDataTextColumn Caption="IMPORTE" FieldName="DecImporte" ShowInCustomizationForm="True" VisibleIndex="15" ToolTip="Tipo Transporte">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="NRO COMPROBANTE" FieldName="DecNroComprobante" ShowInCustomizationForm="True" VisibleIndex="15" ToolTip="Tipo Transporte">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="RESPONSABLE MEDICION" FieldName="StrResponsableMedicion" ShowInCustomizationForm="True" VisibleIndex="15" ToolTip="Tipo Transporte">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="CI RESPONSABLE MEDICION" FieldName="StrCiResponMed" ShowInCustomizationForm="True" VisibleIndex="15" ToolTip="Tipo Transporte">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="NRO REFERENCIA" FieldName="DecNroReferencia" ShowInCustomizationForm="True" VisibleIndex="15" ToolTip="Tipo Transporte">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="TIPO REF. ADICIONAL" FieldName="StrTipoRefAdicional" ShowInCustomizationForm="True" VisibleIndex="15" ToolTip="Tipo Transporte">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="IMPORTE ADICIONAL" FieldName="DecImportAdicional" ShowInCustomizationForm="True" VisibleIndex="15" ToolTip="Tipo Transporte">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="CIUDAD/POBLACION" FieldName="StrCiudadPoblacion" ShowInCustomizationForm="True" VisibleIndex="15" ToolTip="Tipo Transporte">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>  --%>                                                                                                              
                                                        <dx:GridViewDataTextColumn Caption="COD AUTORIZACION" FieldName="codigoAutorizacion" ShowInCustomizationForm="True" VisibleIndex="18">
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                    <Styles CssPostfix="DevGlp">
                                                        <AlternatingRow BackColor="#EEEEEE">
                                                        </AlternatingRow>
                                                    </Styles>
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
                                                                        <dx:ASPxComboBox ID="cmbTipoFact0" runat="server" Font-Size="Smaller" Style="margin-top: 0" ValueType="System.Int32" Visible="False">
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
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
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
                                                <dx:ASPxButton ID="btnVerificaLlave" runat="server" AutoPostBack="False" ClientInstanceName="btnVerificaLlave" Font-Bold="True" ForeColor="#003300" OnClick="btnVerificaLlave_Click" Text="Identificación B-sisa" Theme="Metropolis" Visible="False" Width="160px">
                                                    <Image IconID="businessobjects_bocontact2_16x16">
                                                    </Image>
                                                </dx:ASPxButton>
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

<dx:ASPxPopupControl ID="pucDatos" runat="server" AllowDragging="True" ClientInstanceName="pucDatos" EnableTheming="True" HeaderText="Identificación de Llave B-Sisa" Height="93px" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Theme="Office2003Olive" Width="334px">
    <HeaderImage IconID="setup_pagesetup_16x16">
    </HeaderImage>
    <ContentCollection>
        <dx:PopupControlContentControl runat="server">
            <table class="dxflInternalEditorTable_Youthful">
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel55" runat="server" Font-Bold="True" ForeColor="#003300" Text="Llave B-Sisa :">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="txtLlaveMiddleware" runat="server" Width="170px">
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2">
                        <dx:ASPxButton ID="btnSubirLlave" runat="server" EnableTheming="True" OnClick="btnSubirLlave_Click" Text="Validar Llave B-Sisa" ForeColor="White" BackColor="#307D40" Width="100%">
                            <Image IconID="actions_apply_16x16">
                            </Image>
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>
















