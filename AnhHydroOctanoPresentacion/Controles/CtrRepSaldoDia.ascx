<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtrRepSaldoDia.ascx.cs" Inherits="AnhHydroOctanoPresentacion.Controles.CtrRepSaldoDia" %>

<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxPivotGrid.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPivotGrid" tagprefix="dx" %>


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
    
</style>

<div class="col-md-12">
    <table style="width: 100%">
        <tr>
            <td style="text-align: center; padding-bottom: 1px">
                <dx:ASPxRoundPanel runat="server" ShowHeader="False" Width="100%" ID="pnlTitulo">
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent1" runat="server">
                            <dx:ASPxLabel runat="server" ForeColor="#367B2F" ID="lblTitulo" Font-Bold="True" Font-Names="Rockwell" Font-Size="Larger" Text="REPORTE - SALDOS ACTUAL"></dx:ASPxLabel>
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
                    <dx:SplitterContentControl ID="SplitterContentControl1" runat="server">                        
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
                                    <%--<dx:ASPxCallbackPanel ID="pnlCabecera" runat="server" ClientInstanceName="pnlCabecera" OnCallback="pnlCabecera_Callback" Width="100%">
                                        <PanelCollection>
                                            <dx:PanelContent runat="server">--%>
                                                
                                                <table style="width: 100%;">                                                   
                                                    <tr>
                                                        <td>Fecha Inicial :
                                                        <dx:ASPxDateEdit ID="detFechaConsulta" runat="server" ClientInstanceName="detFechaConsulta" Theme="Metropolis" ToolTip="Seleccione la fecha" Width="60%">
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
                                                        <td> Departamento :
                                                            <dx:ASPxComboBox ID="cmbDepartamento" runat="server" SelectedIndex="0" Theme="Metropolis" Width="60%">
                                                                <Items>
                                                                    <dx:ListEditItem Selected="True" Text="--TODO--" Value="10" />
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
                                                        <td>IdConsumidor :
                                                            <dx:ASPxTokenBox ID="tkblstIdConsumidor" ClientInstanceName="tkblstIdConsumidor" runat="server" Width="60%" TextField="NOMBRE" ValueField="ID_PRODUCTO">
                                                            </dx:aspxtokenbox>
                                                        </td>
                                                    </tr>                                                
                                                    <tr>
                                                         <td style="padding-bottom: 5px">
                                                <dx:ASPxButton ID="btnConsultar" runat="server" AutoPostBack="False" Font-Bold="True" ForeColor="Maroon" Text="Obtener Información" Theme="Metropolis" Width="60%">
                                                    <ClientSideEvents Click="function(s, e) {
pnlDatos.PerformCallback();
}" />
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

                         <%--                   </dx:PanelContent>
                                        </PanelCollection>
                                    </dx:ASPxCallbackPanel>--%>
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
                        </table>
                    </dx:SplitterContentControl>
                </ContentCollection>
            </dx:SplitterPane>
            <dx:SplitterPane ScrollBars="Auto">
                <ContentCollection>
                    <dx:SplitterContentControl ID="SplitterContentControl2" runat="server">                                               
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
                                                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Rockwell" Font-Size="Medium" ForeColor="#367B2F" Text="REPORTE SALDO"></asp:Label>
                                                </legend>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="#10420F" Text="ACTIVIDAD"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label3" runat="server" Font-Bold="True" ForeColor="#10420F" Text=""></asp:Label>
                                            </td>
                                            <td colspan="2" style="text-align:right">
                                                <dx:ASPxButton ID="btnExportar" runat="server" OnClick="btnExportar_Click" Text="Exportar Registros" Theme="Metropolis" Font-Bold="True" ForeColor="Maroon">
                                                    <ClientSideEvents Click="function(s, e) {
	    e.processOnServer = confirm('¿Desea exportar los registros?'); 
    }" />
                                                    <Image IconID="export_exporttoxls_16x16">
                                                    </Image>
                                                </dx:ASPxButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" style="padding-bottom: 5px; width: 50%">
                                                <asp:Label ID="lblActividad" runat="server"></asp:Label>
                                            </td>
                                            <td style="padding-bottom: 5px; width: 10%">
                                                <asp:Label ID="lblCantidad" runat="server"></asp:Label>
                                            </td>
                                            <td colspan="2" style="padding-bottom: 5px; width: 40%">
                                                
                                            </td>
                                        </tr>
                                       
                                        <tr>                                        
                                            <td colspan="6">
                                                <dx:ASPxGridView ID="dgvTransacciones" runat="server" Theme="Default" AutoGenerateColumns="False" EnableTheming="True" Font-Size="Smaller" ClientInstanceName="dgvTransacciones" Width="100%" OnCustomCallback="dgvTransacciones_CustomCallback">
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
                                                        <PageSizeItemSettings Items="50, 100, 500, 1000" Visible="True" AllItemText="Todos" ShowAllItem="True">
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
                                                        <dx:GridViewDataTextColumn Caption="FECHA" FieldName="StrFecha" ShowInCustomizationForm="True" VisibleIndex="3">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="DENOMINACION" FieldName="StrProducto" ShowInCustomizationForm="True" VisibleIndex="2" ToolTip="Descripcion del nombre del proyecto">
                                                            <Settings AutoFilterCondition="Contains" />
                                                            <CellStyle HorizontalAlign="Center">
                                                            </CellStyle>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="VENTAS" FieldName="DecSal01" ShowInCustomizationForm="True" VisibleIndex="9">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="DEPARTAMENTO" FieldName="StrUnidadMedida" ShowInCustomizationForm="True" VisibleIndex="1" ToolTip="Descripcion del nombre del departamento ">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="SALDO_INICIAL" FieldName="DecSaldoInicial" ShowInCustomizationForm="True" VisibleIndex="5">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="SALDO_FINAL" FieldName="DecSaldoFinal" ShowInCustomizationForm="True" VisibleIndex="15" ToolTip="Cantidad de volumen asignado  ">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="PRODUCTO" FieldName="StrCodProducto" ShowInCustomizationForm="True" VisibleIndex="4" ToolTip="Descrpcion del nombre del producto hidrocarburifero">
                                                            <Settings AutoFilterCondition="Contains" />
                                                            <FilterTemplate>
                                                                <dx:ASPxComboBox ID="cmbFiltroProd" runat="server" ValueType="System.String">
                                                                </dx:ASPxComboBox>
                                                            </FilterTemplate>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="ID_CONSUMIDOR" FieldName="IntId" ShowInCustomizationForm="True" VisibleIndex="0">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="TOTAL_ING." FieldName="DecTotalIng" ShowInCustomizationForm="True" VisibleIndex="8">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="TOTAL_SAL." FieldName="DecTotalSal" ShowInCustomizationForm="True" VisibleIndex="14">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="DEMASIA" FieldName="DecIng02" ShowInCustomizationForm="True" VisibleIndex="7">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="MERMA" FieldName="DecSal02" ShowInCustomizationForm="True" VisibleIndex="10">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="RECEPCION" FieldName="DecIng01" ShowInCustomizationForm="True" VisibleIndex="6">
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
                                            <td>
                                                
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