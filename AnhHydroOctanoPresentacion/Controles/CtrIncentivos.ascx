<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtrIncentivos.ascx.cs" Inherits="AnhHydroOctanoPresentacion.Controles.CtrIncentivos" %>

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
    
</style>

<div class="col-md-12">
    <table style="width: 100%">
        <tr>
            <td style="text-align: center; padding-bottom: 1px">
                <dx:ASPxRoundPanel runat="server" ShowHeader="False" Width="100%" ID="pnlTitulo">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <dx:ASPxLabel runat="server" ForeColor="#367B2F" ID="lblTitulo" Font-Bold="True" Font-Names="Rockwell" Font-Size="Larger" Text="IMPORTACION DE FORMULARIOS - INCENTIVOS"></dx:ASPxLabel>
                        </dx:PanelContent>
                    </PanelCollection>
                    <Border BorderStyle="Ridge" BorderWidth="1px" BorderColor="#003300"></Border>
                </dx:ASPxRoundPanel>
            </td>
        </tr>
    </table>
    
    <table style="width: 100%">
        <tr>
            <td>
                <dx:ASPxCallbackPanel ID="pnlCabecera" runat="server" ClientInstanceName="pnlCabecera" OnCallback="pnlCabecera_Callback" Width="100%">
                                        <PanelCollection>
                                            <dx:PanelContent runat="server">
                                                
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td style="width: 20%; padding-top: 10px">
                                                            <dx:ASPxLabel ID="ASPxLabel57" runat="server" ForeColor="#003300" Text="Formulario :">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 20%; padding-top: 10px">
                                                            <dx:ASPxLabel ID="ASPxLabel56" runat="server" ForeColor="#003300" Text="Periodo :">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 20%">
                                                        </td>
                                                        <td style="width: 20%">
                                                        </td>
                                                        <td style="width: 20%">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 20%">
                                                            <dx:ASPxComboBox ID="cmbTipoFormulario" runat="server" ClientInstanceName="cmbTipoFormulario" EnableTheming="True" SelectedIndex="0" Theme="Metropolis" ValueType="System.Int32" Width="100%">
                                                                <ClientSideEvents SelectedIndexChanged="function(s, e) {

dgvFormA.SetVisible(false);
dgvFormB.SetVisible(false);
dgvFormC.SetVisible(false);
dgvFormD.SetVisible(false);
dgvFormE.SetVisible(false);
dgvFormF.SetVisible(false);
dgvFormG.SetVisible(false);
dgvFormH.SetVisible(false);

switch(s.GetValue()) {
&nbsp;&nbsp;&nbsp; case&nbsp;1:
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; dgvFormA.SetVisible(true);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; break;
&nbsp;&nbsp;&nbsp; case&nbsp;2:
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; dgvFormB.SetVisible(true);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; break;
    case&nbsp;3:
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; dgvFormC.SetVisible(true);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; break;
    case&nbsp;4:
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; dgvFormD.SetVisible(true);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; break;
    case&nbsp;5:
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; dgvFormE.SetVisible(true);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; break;
    case&nbsp;6:
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; dgvFormF.SetVisible(true);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; break;
    case&nbsp;7:
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; dgvFormG.SetVisible(true);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; break;
    case&nbsp;8:
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; dgvFormH.SetVisible(true);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; break;
} 

}" />
                                                                <Items>
                                                                    <dx:ListEditItem Selected="True" Text="ANUAL A" Value="1" />
                                                                    <dx:ListEditItem Text="ANUAL B" Value="2" />
                                                                    <dx:ListEditItem Text="ANUAL C - GAS SECO" Value="3" />
                                                                    <dx:ListEditItem Text="MENSUAL D - IPET" Value="4" />
                                                                    <dx:ListEditItem Text="MENSUAL E -ICON2" Value="5" />
                                                                    <dx:ListEditItem Text="MENSUAL F - CALCIPRODPET" Value="6" />
                                                                    <dx:ListEditItem Text="MENSUAL G - CALCINCCOND" Value="7" />
                                                                    <dx:ListEditItem Text="MENSUAL H - CALINCPRODADICCOND" Value="8" />
                                                                </Items>
                                                                <ClearButton Visibility="Auto">
                                                                </ClearButton>
                                                            </dx:ASPxComboBox>
                                                        </td>
                                                        <td style="width: 20%">
                                                            <dx:ASPxComboBox ID="cmbPeriodo" runat="server" ClientInstanceName="cmbPeriodo" EnableTheming="True" Font-Bold="True" Theme="Metropolis" ToolTip="Seleccione un Periodo" ValueType="System.Int32" Width="100%">
                                                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
	    //pnlDatos.PerformCallback();
    }" />
                                                                <ClearButton Visibility="Auto">
                                                                </ClearButton>
                                                                <CaptionStyle Font-Bold="False">
                                                                </CaptionStyle>
                                                                <DisabledStyle Font-Bold="False">
                                                                </DisabledStyle>
                                                            </dx:ASPxComboBox>
                                                        </td>
                                                        <td colspan = "3" style="width: 20%">
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="5">
                                                            <dx:ASPxLabel ID="ASPxLabel277" runat="server" Width="100%">
                                                                <BorderBottom BorderColor="#FF6600" BorderStyle="Solid" />
                                                            </dx:ASPxLabel>
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
                                            <td colspan="6" style="padding-top: 10px">
                                                <legend>
                                                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Rockwell" Font-Size="Medium" ForeColor="#367B2F" Text="DETALLE DEL FORMULARIO"></asp:Label>
                                                </legend>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 25%">
                                                <asp:FileUpload ID="FileUploader" runat="server" ToolTip="Seleccione el archivo .CSV" Width="100%" Font-Size="Smaller" />
                                            </td>
                                            <td colspan="4" style="width: 55%">
                                                <dx:ASPxButton ID="btnImportar" runat="server" AutoPostBack="False" Text="Visualizar Registros" Theme="Metropolis" OnClick="btnImportar_Click" Font-Bold="True" ForeColor="Maroon">
                                                    <ClientSideEvents Click="function(s, e) {
	    LoadingPanel.Show();
    }" />
                                                    <Image IconID="zoom_zoom_16x16">
                                                    </Image>
                                                </dx:ASPxButton>
                                            </td>
                                            <td style="width: 20%">
                                                <dx:ASPxButton ID="btnGrabar" runat="server" Text="Declarar Formulario" Theme="Metropolis" Enabled="False" Font-Bold="True" ForeColor="Maroon" Width="100%" AutoPostBack="False">
                                                    <ClientSideEvents Click="function(s, e) {
    pucDdjj.Show();

    }" />
                                                    <Image IconID="actions_addfile_16x16">
                                                    </Image>
                                                </dx:ASPxButton>
                                            </td>
                                        </tr>
                                        
                                        <tr>
                                            <td colspan="6">
                                                <dx:ASPxGridView ID="dgvFormA" runat="server" AutoGenerateColumns="False" EnableTheming="True" Font-Size="X-Small" KeyFieldName="noRegistro" OnHtmlRowPrepared="dgvFormA_HtmlRowPrepared" Theme="Office2003Olive" Width="100%" ClientInstanceName="dgvFormA">
                                                    <Styles>
                                                        <Header Wrap="True" HorizontalAlign="Center"></Header>
                                                    </Styles>
                                                    <SettingsContextMenu EnableRowMenu="False">
                                                    </SettingsContextMenu>
                                                    <Templates>
                                                        <EditForm>
                                                            <table>
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel61" runat="server" Text="ID :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtId1" runat="server" ClientInstanceName="txtId" ReadOnly="True" Width="170px">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel62" runat="server" Text="Fecha :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxDateEdit ID="detFecha1" runat="server" EditFormat="DateTime" Font-Size="Smaller">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel63" runat="server" Height="16px" Text="Válido Cred. Fiscal :" Width="110px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxComboBox ID="cmbTipoCred1" runat="server" Font-Size="Smaller" ValueType="System.Int32">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel64" runat="server" Text="Tipo Pago :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxComboBox ID="cmbTipoPago1" runat="server" Font-Size="Smaller" ValueType="System.Int32">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel65" runat="server" Text="Tipo Consumidor :" Width="110px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxComboBox ID="cmbTipoCons1" runat="server" Font-Size="Smaller" ValueType="System.Int32">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel66" runat="server" Text="Cliente :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtCliente1" runat="server" Font-Size="Smaller" Height="16px" MaxLength="90" Width="170px">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel67" runat="server" Text="NIT :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtNit1" runat="server" Font-Size="Smaller" MaxLength="15" Width="170px">
                                                                            <MaskSettings Mask="&lt;0..999999999999999&gt;" PromptChar=" " />
                                                                            <ValidationSettings>
                                                                                <RequiredField IsRequired="True" />
                                                                            </ValidationSettings>
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel68" runat="server" Text="Usuario :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtUsuario1" runat="server" ClientInstanceName="txtId" Font-Size="Smaller" MaxLength="100" Width="170px">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel69" runat="server" Text="Tipo de Venta :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxRadioButtonList ID="rbtTipoVenta1" runat="server" ClientInstanceName="rbtTipoVenta" SelectedIndex="0" ValueType="System.Int32">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel70" runat="server" Text="País :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxComboBox ID="cmbPais1" runat="server">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel71" runat="server" Text="Placa :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtPlaca1" runat="server" ClientInstanceName="txtId" Font-Size="Smaller" MaxLength="11" Width="170px">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel72" runat="server" Text="No. Autorización :" Width="100px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtNoAuto1" runat="server" Font-Size="Smaller" MaxLength="15" Width="170px">
                                                                            <MaskSettings Mask="&lt;0..999999999999999&gt;" />
                                                                            <ValidationSettings>
                                                                                <RequiredField IsRequired="True" />
                                                                            </ValidationSettings>
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel73" runat="server" Text="No. Factura :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtNoFact1" runat="server" Font-Size="Smaller" MaxLength="15" Width="170px">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel74" runat="server" Text="Cod. Control :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtCodCtrl1" runat="server" Font-Size="Smaller" MaxLength="14" Width="170px">
                                                                            <MaskSettings Mask="" />
                                                                            <ValidationSettings SetFocusOnError="True">
                                                                            </ValidationSettings>
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel75" runat="server" Text="Importe :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtImporte1" runat="server" Width="170px">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel76" runat="server" Text="No. Autorización Ext. :" Width="130px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtNoAutoExt1" runat="server" ClientInstanceName="txtNoAutoExt" Font-Size="Smaller" MaxLength="15" Width="170px">
                                                                            <MaskSettings Mask="&lt;0..999999999999999&gt;" />
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel77" runat="server" Text="No. Factura Ext. :" Width="120px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtNoFactExt1" runat="server" ClientInstanceName="txtNoFactExt" Font-Size="Smaller" MaxLength="15" Width="170px">
                                                                            <MaskSettings Mask="&lt;0..999999999999999&gt;" />
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel78" runat="server" Text="Cod. Control Ext. :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtCodCtrlExt1" runat="server" Font-Size="Smaller" MaxLength="14" Width="170px">
                                                                            <MaskSettings Mask="" />
                                                                            <ValidationSettings SetFocusOnError="True">
                                                                            </ValidationSettings>
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel79" runat="server" Text="Importe Ext. :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtImporteExt1" runat="server" Width="170px">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel80" runat="server" Text="No. Doc. Referencia :" Width="130px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtNoDocRef1" runat="server" Font-Size="Smaller" MaxLength="15" Width="170px">
                                                                            <MaskSettings Mask="&lt;0..999999999999999&gt;" />
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel81" runat="server" Text="Tipo Factura :" Visible="False">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxComboBox ID="cmbTipoFact1" runat="server" Font-Size="Smaller" style="margin-top: 0" ValueType="System.Int32" Visible="False">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel82" runat="server" Text="Estado :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxComboBox ID="cmbEstado1" runat="server" Font-Size="Smaller" ValueType="System.Int32">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel83" runat="server" Text="Producto :" Width="100px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxComboBox ID="cmbProd1" runat="server" Font-Size="Smaller" ValueType="System.Int32">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel84" runat="server" Text="Cantidad :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td class="auto-style16">
                                                                        <dx:ASPxTextBox ID="txtCantidad1" runat="server" Width="170px">
                                                                            <MaskSettings IncludeLiterals="DecimalSymbol" Mask="&lt;0..9999999&gt;.&lt;00..99&gt;" />
                                                                            <ValidationSettings>
                                                                                <RequiredField IsRequired="True" />
                                                                            </ValidationSettings>
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td class="auto-style19">
                                                                        <dx:ASPxLabel ID="ASPxLabel85" runat="server" Text="Precio :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtPrecio1" runat="server" Width="170px">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel86" runat="server" Text="Autorización de Venta :" Width="150px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td class="auto-style16">
                                                                        <dx:ASPxComboBox ID="cmbAutoVta1" runat="server">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel87" runat="server" Text="No. Hoja Ruta :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtNoAutoVta1" runat="server" Font-Size="Smaller" MaxLength="15" Width="170px">
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
                                                                        <dx:ASPxLabel ID="lblMensaje1" runat="server" Text="hghgh">
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
                                                                        <dx:ASPxButton ID="btnAceptar1" runat="server" AutoPostBack="False" ClientInstanceName="btnAceptar" Text="Aceptar" Theme="Office2003Olive" Width="72px">
                                                                            <ClientSideEvents Click="function(s, e) {
                                                                                    dgvTransacciones.UpdateEdit()	
                                                                            }" />
                                                                        </dx:ASPxButton>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxButton ID="btnCancelar1" runat="server" AutoPostBack="False" ClientInstanceName="btnCancelar" Text="Cerrar" Theme="Office2003Olive">
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
                                                    <SettingsBehavior AllowFocusedRow="True" AllowGroup="False" AllowSort="False" ColumnResizeMode="NextColumn" />
                                                    <SettingsPopup>
                                                        <EditForm Modal="True" />
                                                    </SettingsPopup>
                                                    <SettingsText CommandNew="Nuevo" />
                                                    <Columns>
                                                        <dx:GridViewDataTextColumn Caption="Titular" FieldName="campo1" ShowInCustomizationForm="True" VisibleIndex="0">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Operador" FieldName="campo2" ShowInCustomizationForm="True" VisibleIndex="1">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Campo" FieldName="campo3" ShowInCustomizationForm="True" VisibleIndex="2">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Reservorio" FieldName="campo4" ShowInCustomizationForm="True" VisibleIndex="3">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Gas Natural Producido (MPC)" FieldName="campo5" ShowInCustomizationForm="True" VisibleIndex="4">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Petróleo/Condensado Producido o Producido (MBBL)" FieldName="campo6" ShowInCustomizationForm="True" VisibleIndex="5">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Relación Gas/Petróleo (PC/BBL)" FieldName="campo7" ShowInCustomizationForm="True" VisibleIndex="6">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Gravedad (°API)" FieldName="campo8" ShowInCustomizationForm="True" VisibleIndex="7">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Clasificación del Campo" FieldName="campo9" ShowInCustomizationForm="True" VisibleIndex="8">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="noRegistro" ShowInCustomizationForm="True" Visible="False" VisibleIndex="9">
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                </dx:ASPxGridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                                <dx:ASPxGridView ID="dgvFormB" runat="server" AutoGenerateColumns="False" ClientInstanceName="dgvFormB" ClientVisible="False" EnableTheming="True" Font-Size="X-Small" KeyFieldName="noRegistro" OnHtmlRowPrepared="dgvFormB_HtmlRowPrepared" Theme="Office2003Olive" Width="100%">
                                                    <Styles>
                                                        <Header Wrap="True" HorizontalAlign="Center"></Header>
                                                    </Styles>
                                                    <SettingsContextMenu EnableRowMenu="False">
                                                    </SettingsContextMenu>
                                                    <Templates>
                                                        <EditForm>
                                                            <table>
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel88" runat="server" Text="ID :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtId2" runat="server" ClientInstanceName="txtId" ReadOnly="True" Width="170px">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel89" runat="server" Text="Fecha :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxDateEdit ID="detFecha2" runat="server" EditFormat="DateTime" Font-Size="Smaller">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel90" runat="server" Height="16px" Text="Válido Cred. Fiscal :" Width="110px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxComboBox ID="cmbTipoCred2" runat="server" Font-Size="Smaller" ValueType="System.Int32">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel91" runat="server" Text="Tipo Pago :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxComboBox ID="cmbTipoPago2" runat="server" Font-Size="Smaller" ValueType="System.Int32">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel92" runat="server" Text="Tipo Consumidor :" Width="110px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxComboBox ID="cmbTipoCons2" runat="server" Font-Size="Smaller" ValueType="System.Int32">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel93" runat="server" Text="Cliente :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtCliente2" runat="server" Font-Size="Smaller" Height="16px" MaxLength="90" Width="170px">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel94" runat="server" Text="NIT :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtNit2" runat="server" Font-Size="Smaller" MaxLength="15" Width="170px">
                                                                            <MaskSettings Mask="&lt;0..999999999999999&gt;" PromptChar=" " />
                                                                            <ValidationSettings>
                                                                                <RequiredField IsRequired="True" />
                                                                            </ValidationSettings>
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel95" runat="server" Text="Usuario :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtUsuario2" runat="server" ClientInstanceName="txtId" Font-Size="Smaller" MaxLength="100" Width="170px">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel96" runat="server" Text="Tipo de Venta :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxRadioButtonList ID="rbtTipoVenta2" runat="server" ClientInstanceName="rbtTipoVenta" SelectedIndex="0" ValueType="System.Int32">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel97" runat="server" Text="País :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxComboBox ID="cmbPais2" runat="server">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel98" runat="server" Text="Placa :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtPlaca2" runat="server" ClientInstanceName="txtId" Font-Size="Smaller" MaxLength="11" Width="170px">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel99" runat="server" Text="No. Autorización :" Width="100px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtNoAuto2" runat="server" Font-Size="Smaller" MaxLength="15" Width="170px">
                                                                            <MaskSettings Mask="&lt;0..999999999999999&gt;" />
                                                                            <ValidationSettings>
                                                                                <RequiredField IsRequired="True" />
                                                                            </ValidationSettings>
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel100" runat="server" Text="No. Factura :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtNoFact2" runat="server" Font-Size="Smaller" MaxLength="15" Width="170px">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel101" runat="server" Text="Cod. Control :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtCodCtrl2" runat="server" Font-Size="Smaller" MaxLength="14" Width="170px">
                                                                            <MaskSettings Mask="" />
                                                                            <ValidationSettings SetFocusOnError="True">
                                                                            </ValidationSettings>
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel102" runat="server" Text="Importe :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtImporte2" runat="server" Width="170px">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel103" runat="server" Text="No. Autorización Ext. :" Width="130px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtNoAutoExt2" runat="server" ClientInstanceName="txtNoAutoExt" Font-Size="Smaller" MaxLength="15" Width="170px">
                                                                            <MaskSettings Mask="&lt;0..999999999999999&gt;" />
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel104" runat="server" Text="No. Factura Ext. :" Width="120px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtNoFactExt2" runat="server" ClientInstanceName="txtNoFactExt" Font-Size="Smaller" MaxLength="15" Width="170px">
                                                                            <MaskSettings Mask="&lt;0..999999999999999&gt;" />
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel105" runat="server" Text="Cod. Control Ext. :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtCodCtrlExt2" runat="server" Font-Size="Smaller" MaxLength="14" Width="170px">
                                                                            <MaskSettings Mask="" />
                                                                            <ValidationSettings SetFocusOnError="True">
                                                                            </ValidationSettings>
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel106" runat="server" Text="Importe Ext. :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtImporteExt2" runat="server" Width="170px">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel107" runat="server" Text="No. Doc. Referencia :" Width="130px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtNoDocRef2" runat="server" Font-Size="Smaller" MaxLength="15" Width="170px">
                                                                            <MaskSettings Mask="&lt;0..999999999999999&gt;" />
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel108" runat="server" Text="Tipo Factura :" Visible="False">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxComboBox ID="cmbTipoFact2" runat="server" Font-Size="Smaller" style="margin-top: 0" ValueType="System.Int32" Visible="False">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel109" runat="server" Text="Estado :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxComboBox ID="cmbEstado2" runat="server" Font-Size="Smaller" ValueType="System.Int32">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel110" runat="server" Text="Producto :" Width="100px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxComboBox ID="cmbProd2" runat="server" Font-Size="Smaller" ValueType="System.Int32">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel111" runat="server" Text="Cantidad :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td class="auto-style16">
                                                                        <dx:ASPxTextBox ID="txtCantidad2" runat="server" Width="170px">
                                                                            <MaskSettings IncludeLiterals="DecimalSymbol" Mask="&lt;0..9999999&gt;.&lt;00..99&gt;" />
                                                                            <ValidationSettings>
                                                                                <RequiredField IsRequired="True" />
                                                                            </ValidationSettings>
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td class="auto-style19">
                                                                        <dx:ASPxLabel ID="ASPxLabel112" runat="server" Text="Precio :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtPrecio2" runat="server" Width="170px">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel113" runat="server" Text="Autorización de Venta :" Width="150px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td class="auto-style16">
                                                                        <dx:ASPxComboBox ID="cmbAutoVta2" runat="server">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel114" runat="server" Text="No. Hoja Ruta :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtNoAutoVta2" runat="server" Font-Size="Smaller" MaxLength="15" Width="170px">
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
                                                                        <dx:ASPxLabel ID="lblMensaje2" runat="server" Text="hghgh">
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
                                                                        <dx:ASPxButton ID="btnAceptar2" runat="server" AutoPostBack="False" ClientInstanceName="btnAceptar" Text="Aceptar" Theme="Office2003Olive" Width="72px">
                                                                            <ClientSideEvents Click="function(s, e) {
                                                                                    dgvTransacciones.UpdateEdit()	
                                                                            }" />
                                                                        </dx:ASPxButton>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxButton ID="btnCancelar2" runat="server" AutoPostBack="False" ClientInstanceName="btnCancelar" Text="Cerrar" Theme="Office2003Olive">
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
                                                    <SettingsBehavior AllowFocusedRow="True" AllowSort="False" ColumnResizeMode="NextColumn" />
                                                    <SettingsPopup>
                                                        <EditForm Modal="True" />
                                                    </SettingsPopup>
                                                    <SettingsText CommandNew="Nuevo" />
                                                    <Columns>
                                                        <dx:GridViewDataTextColumn Caption="Titular" FieldName="campo1" ShowInCustomizationForm="True" VisibleIndex="0">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Operador" FieldName="campo2" ShowInCustomizationForm="True" VisibleIndex="1">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Campo" FieldName="campo3" ShowInCustomizationForm="True" VisibleIndex="2">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Reservorio" FieldName="campo4" ShowInCustomizationForm="True" VisibleIndex="3">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="RESERVA PROBADA [MPC]" FieldName="campo5" ShowInCustomizationForm="True" VisibleIndex="4">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="PRODUCCION ACUMULADA [MPC]" FieldName="campo6" ShowInCustomizationForm="True" VisibleIndex="5">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="RESERVAS PROBADAS ORIGINALES RECUPERABLES [MPC]" FieldName="campo7" ShowInCustomizationForm="True" VisibleIndex="6">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="RESERVA PROBADA REMANENTE
(MPC)" FieldName="campo8" ShowInCustomizationForm="True" VisibleIndex="7">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Porcentaje de Reserva Producida
[%]" FieldName="campo9" ShowInCustomizationForm="True" VisibleIndex="8">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="CLASIFICACION: CAMPO GASIFERO MARGINAL SI/NO" FieldName="campo10" ShowInCustomizationForm="True" VisibleIndex="9">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Reserva Probada Remanente
≤ 0,5 TCF
[MPC]" FieldName="campo11" ShowInCustomizationForm="True" VisibleIndex="10">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Prod. Fiscalizada de Gas Natural 
[MPCD]" FieldName="campo12" ShowInCustomizationForm="True" VisibleIndex="11">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Prod. Fiscalizada de Gas Natural 
[BOED]" FieldName="campo13" ShowInCustomizationForm="True" VisibleIndex="12">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Prod. Fiscalizada de Gasolina Natural
[BPD]" FieldName="campo14" ShowInCustomizationForm="True" VisibleIndex="13">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Prod. Fiscalizada de Condensado
[BPD]" FieldName="campo15" ShowInCustomizationForm="True" VisibleIndex="14">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Prod. Total de Gas Natural+Gasolina+Condensado
[BOED]" FieldName="campo16" ShowInCustomizationForm="True" VisibleIndex="15">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Clasificación Campo Gasífero
Pequeño SI/NO" FieldName="campo17" ShowInCustomizationForm="True" VisibleIndex="16">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="noRegistro" ShowInCustomizationForm="True" Visible="False" VisibleIndex="17">
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                </dx:ASPxGridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                                <dx:ASPxGridView ID="dgvFormC" runat="server" AutoGenerateColumns="False" ClientInstanceName="dgvFormC" ClientVisible="False" EnableTheming="True" Font-Size="X-Small" KeyFieldName="noRegistro" OnHtmlRowPrepared="dgvFormC_HtmlRowPrepared" Theme="Office2003Olive" Width="100%">
                                                    <Styles>
                                                        <Header Wrap="True" HorizontalAlign="Center"></Header>
                                                    </Styles>
                                                    <SettingsContextMenu EnableRowMenu="False">
                                                    </SettingsContextMenu>
                                                    <Templates>
                                                        <EditForm>
                                                            <table>
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel115" runat="server" Text="ID :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtId3" runat="server" ClientInstanceName="txtId" ReadOnly="True" Width="170px">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel116" runat="server" Text="Fecha :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxDateEdit ID="detFecha3" runat="server" EditFormat="DateTime" Font-Size="Smaller">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel117" runat="server" Height="16px" Text="Válido Cred. Fiscal :" Width="110px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxComboBox ID="cmbTipoCred3" runat="server" Font-Size="Smaller" ValueType="System.Int32">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel118" runat="server" Text="Tipo Pago :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxComboBox ID="cmbTipoPago3" runat="server" Font-Size="Smaller" ValueType="System.Int32">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel119" runat="server" Text="Tipo Consumidor :" Width="110px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxComboBox ID="cmbTipoCons3" runat="server" Font-Size="Smaller" ValueType="System.Int32">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel120" runat="server" Text="Cliente :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtCliente3" runat="server" Font-Size="Smaller" Height="16px" MaxLength="90" Width="170px">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel121" runat="server" Text="NIT :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtNit3" runat="server" Font-Size="Smaller" MaxLength="15" Width="170px">
                                                                            <MaskSettings Mask="&lt;0..999999999999999&gt;" PromptChar=" " />
                                                                            <ValidationSettings>
                                                                                <RequiredField IsRequired="True" />
                                                                            </ValidationSettings>
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel122" runat="server" Text="Usuario :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtUsuario3" runat="server" ClientInstanceName="txtId" Font-Size="Smaller" MaxLength="100" Width="170px">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel123" runat="server" Text="Tipo de Venta :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxRadioButtonList ID="rbtTipoVenta3" runat="server" ClientInstanceName="rbtTipoVenta" SelectedIndex="0" ValueType="System.Int32">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel124" runat="server" Text="País :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxComboBox ID="cmbPais3" runat="server">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel125" runat="server" Text="Placa :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtPlaca3" runat="server" ClientInstanceName="txtId" Font-Size="Smaller" MaxLength="11" Width="170px">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel126" runat="server" Text="No. Autorización :" Width="100px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtNoAuto3" runat="server" Font-Size="Smaller" MaxLength="15" Width="170px">
                                                                            <MaskSettings Mask="&lt;0..999999999999999&gt;" />
                                                                            <ValidationSettings>
                                                                                <RequiredField IsRequired="True" />
                                                                            </ValidationSettings>
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel127" runat="server" Text="No. Factura :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtNoFact3" runat="server" Font-Size="Smaller" MaxLength="15" Width="170px">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel128" runat="server" Text="Cod. Control :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtCodCtrl3" runat="server" Font-Size="Smaller" MaxLength="14" Width="170px">
                                                                            <MaskSettings Mask="" />
                                                                            <ValidationSettings SetFocusOnError="True">
                                                                            </ValidationSettings>
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel129" runat="server" Text="Importe :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtImporte3" runat="server" Width="170px">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel130" runat="server" Text="No. Autorización Ext. :" Width="130px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtNoAutoExt3" runat="server" ClientInstanceName="txtNoAutoExt" Font-Size="Smaller" MaxLength="15" Width="170px">
                                                                            <MaskSettings Mask="&lt;0..999999999999999&gt;" />
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel131" runat="server" Text="No. Factura Ext. :" Width="120px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtNoFactExt3" runat="server" ClientInstanceName="txtNoFactExt" Font-Size="Smaller" MaxLength="15" Width="170px">
                                                                            <MaskSettings Mask="&lt;0..999999999999999&gt;" />
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel132" runat="server" Text="Cod. Control Ext. :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtCodCtrlExt3" runat="server" Font-Size="Smaller" MaxLength="14" Width="170px">
                                                                            <MaskSettings Mask="" />
                                                                            <ValidationSettings SetFocusOnError="True">
                                                                            </ValidationSettings>
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel133" runat="server" Text="Importe Ext. :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtImporteExt3" runat="server" Width="170px">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel134" runat="server" Text="No. Doc. Referencia :" Width="130px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtNoDocRef3" runat="server" Font-Size="Smaller" MaxLength="15" Width="170px">
                                                                            <MaskSettings Mask="&lt;0..999999999999999&gt;" />
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel135" runat="server" Text="Tipo Factura :" Visible="False">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxComboBox ID="cmbTipoFact3" runat="server" Font-Size="Smaller" style="margin-top: 0" ValueType="System.Int32" Visible="False">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel136" runat="server" Text="Estado :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxComboBox ID="cmbEstado3" runat="server" Font-Size="Smaller" ValueType="System.Int32">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel137" runat="server" Text="Producto :" Width="100px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxComboBox ID="cmbProd3" runat="server" Font-Size="Smaller" ValueType="System.Int32">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel138" runat="server" Text="Cantidad :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td class="auto-style16">
                                                                        <dx:ASPxTextBox ID="txtCantidad3" runat="server" Width="170px">
                                                                            <MaskSettings IncludeLiterals="DecimalSymbol" Mask="&lt;0..9999999&gt;.&lt;00..99&gt;" />
                                                                            <ValidationSettings>
                                                                                <RequiredField IsRequired="True" />
                                                                            </ValidationSettings>
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td class="auto-style19">
                                                                        <dx:ASPxLabel ID="ASPxLabel139" runat="server" Text="Precio :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtPrecio3" runat="server" Width="170px">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel140" runat="server" Text="Autorización de Venta :" Width="150px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td class="auto-style16">
                                                                        <dx:ASPxComboBox ID="cmbAutoVta3" runat="server">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel141" runat="server" Text="No. Hoja Ruta :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtNoAutoVta3" runat="server" Font-Size="Smaller" MaxLength="15" Width="170px">
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
                                                                        <dx:ASPxLabel ID="lblMensaje3" runat="server" Text="hghgh">
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
                                                                        <dx:ASPxButton ID="btnAceptar3" runat="server" AutoPostBack="False" ClientInstanceName="btnAceptar" Text="Aceptar" Theme="Office2003Olive" Width="72px">
                                                                            <ClientSideEvents Click="function(s, e) {
                                                                                    dgvTransacciones.UpdateEdit()	
                                                                            }" />
                                                                        </dx:ASPxButton>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxButton ID="btnCancelar3" runat="server" AutoPostBack="False" ClientInstanceName="btnCancelar" Text="Cerrar" Theme="Office2003Olive">
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
                                                    <SettingsBehavior AllowFocusedRow="True" AllowGroup="False" AllowSort="False" ColumnResizeMode="NextColumn" />
                                                    <SettingsPopup>
                                                        <EditForm Modal="True" />
                                                    </SettingsPopup>
                                                    <SettingsText CommandNew="Nuevo" />
                                                    <Columns>
                                                        <dx:GridViewDataTextColumn Caption="Titular" FieldName="campo1" ShowInCustomizationForm="True" VisibleIndex="0">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Operador" FieldName="campo2" ShowInCustomizationForm="True" VisibleIndex="1">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Campo" FieldName="campo3" ShowInCustomizationForm="True" VisibleIndex="2">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Reservorio" FieldName="campo4" ShowInCustomizationForm="True" VisibleIndex="3">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Composicion
 C1%" FieldName="campo5" ShowInCustomizationForm="True" VisibleIndex="4">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Yield de Condensado [BBL/MMPC]" FieldName="campo6" ShowInCustomizationForm="True" VisibleIndex="5">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Caudal Promedio [MMPCD]" FieldName="campo7" ShowInCustomizationForm="True" VisibleIndex="6">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Aplica incentivo
 SI/NO" FieldName="campo8" ShowInCustomizationForm="True" VisibleIndex="7">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="noRegistro" ShowInCustomizationForm="True" Visible="False" VisibleIndex="9">
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                </dx:ASPxGridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                                <dx:ASPxGridView ID="dgvFormD" runat="server" AutoGenerateColumns="False" ClientInstanceName="dgvFormD" ClientVisible="False" EnableTheming="True" Font-Size="X-Small" KeyFieldName="noRegistro" OnHtmlRowPrepared="dgvFormD_HtmlRowPrepared" Theme="Office2003Olive" Width="100%">
                                                    <Styles>
                                                        <Header Wrap="True" HorizontalAlign="Center"></Header>
                                                    </Styles>
                                                    <SettingsContextMenu EnableRowMenu="False">
                                                    </SettingsContextMenu>
                                                    <Templates>
                                                        <EditForm>
                                                            <table>
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel142" runat="server" Text="ID :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtId4" runat="server" ClientInstanceName="txtId" ReadOnly="True" Width="170px">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel143" runat="server" Text="Fecha :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxDateEdit ID="detFecha4" runat="server" EditFormat="DateTime" Font-Size="Smaller">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel144" runat="server" Height="16px" Text="Válido Cred. Fiscal :" Width="110px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxComboBox ID="cmbTipoCred4" runat="server" Font-Size="Smaller" ValueType="System.Int32">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel145" runat="server" Text="Tipo Pago :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxComboBox ID="cmbTipoPago4" runat="server" Font-Size="Smaller" ValueType="System.Int32">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel146" runat="server" Text="Tipo Consumidor :" Width="110px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxComboBox ID="cmbTipoCons4" runat="server" Font-Size="Smaller" ValueType="System.Int32">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel147" runat="server" Text="Cliente :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtCliente4" runat="server" Font-Size="Smaller" Height="16px" MaxLength="90" Width="170px">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel148" runat="server" Text="NIT :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtNit4" runat="server" Font-Size="Smaller" MaxLength="15" Width="170px">
                                                                            <MaskSettings Mask="&lt;0..999999999999999&gt;" PromptChar=" " />
                                                                            <ValidationSettings>
                                                                                <RequiredField IsRequired="True" />
                                                                            </ValidationSettings>
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel149" runat="server" Text="Usuario :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtUsuario4" runat="server" ClientInstanceName="txtId" Font-Size="Smaller" MaxLength="100" Width="170px">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel150" runat="server" Text="Tipo de Venta :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxRadioButtonList ID="rbtTipoVenta4" runat="server" ClientInstanceName="rbtTipoVenta" SelectedIndex="0" ValueType="System.Int32">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel151" runat="server" Text="País :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxComboBox ID="cmbPais4" runat="server">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel152" runat="server" Text="Placa :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtPlaca4" runat="server" ClientInstanceName="txtId" Font-Size="Smaller" MaxLength="11" Width="170px">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel153" runat="server" Text="No. Autorización :" Width="100px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtNoAuto4" runat="server" Font-Size="Smaller" MaxLength="15" Width="170px">
                                                                            <MaskSettings Mask="&lt;0..999999999999999&gt;" />
                                                                            <ValidationSettings>
                                                                                <RequiredField IsRequired="True" />
                                                                            </ValidationSettings>
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel154" runat="server" Text="No. Factura :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtNoFact4" runat="server" Font-Size="Smaller" MaxLength="15" Width="170px">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel155" runat="server" Text="Cod. Control :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtCodCtrl4" runat="server" Font-Size="Smaller" MaxLength="14" Width="170px">
                                                                            <MaskSettings Mask="" />
                                                                            <ValidationSettings SetFocusOnError="True">
                                                                            </ValidationSettings>
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel156" runat="server" Text="Importe :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtImporte4" runat="server" Width="170px">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel157" runat="server" Text="No. Autorización Ext. :" Width="130px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtNoAutoExt4" runat="server" ClientInstanceName="txtNoAutoExt" Font-Size="Smaller" MaxLength="15" Width="170px">
                                                                            <MaskSettings Mask="&lt;0..999999999999999&gt;" />
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel158" runat="server" Text="No. Factura Ext. :" Width="120px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtNoFactExt4" runat="server" ClientInstanceName="txtNoFactExt" Font-Size="Smaller" MaxLength="15" Width="170px">
                                                                            <MaskSettings Mask="&lt;0..999999999999999&gt;" />
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel159" runat="server" Text="Cod. Control Ext. :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtCodCtrlExt4" runat="server" Font-Size="Smaller" MaxLength="14" Width="170px">
                                                                            <MaskSettings Mask="" />
                                                                            <ValidationSettings SetFocusOnError="True">
                                                                            </ValidationSettings>
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel160" runat="server" Text="Importe Ext. :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtImporteExt4" runat="server" Width="170px">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel161" runat="server" Text="No. Doc. Referencia :" Width="130px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtNoDocRef4" runat="server" Font-Size="Smaller" MaxLength="15" Width="170px">
                                                                            <MaskSettings Mask="&lt;0..999999999999999&gt;" />
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel162" runat="server" Text="Tipo Factura :" Visible="False">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxComboBox ID="cmbTipoFact4" runat="server" Font-Size="Smaller" style="margin-top: 0" ValueType="System.Int32" Visible="False">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel163" runat="server" Text="Estado :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxComboBox ID="cmbEstado4" runat="server" Font-Size="Smaller" ValueType="System.Int32">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel164" runat="server" Text="Producto :" Width="100px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxComboBox ID="cmbProd4" runat="server" Font-Size="Smaller" ValueType="System.Int32">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel165" runat="server" Text="Cantidad :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td class="auto-style16">
                                                                        <dx:ASPxTextBox ID="txtCantidad4" runat="server" Width="170px">
                                                                            <MaskSettings IncludeLiterals="DecimalSymbol" Mask="&lt;0..9999999&gt;.&lt;00..99&gt;" />
                                                                            <ValidationSettings>
                                                                                <RequiredField IsRequired="True" />
                                                                            </ValidationSettings>
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td class="auto-style19">
                                                                        <dx:ASPxLabel ID="ASPxLabel166" runat="server" Text="Precio :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtPrecio4" runat="server" Width="170px">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel167" runat="server" Text="Autorización de Venta :" Width="150px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td class="auto-style16">
                                                                        <dx:ASPxComboBox ID="cmbAutoVta4" runat="server">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel168" runat="server" Text="No. Hoja Ruta :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtNoAutoVta4" runat="server" Font-Size="Smaller" MaxLength="15" Width="170px">
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
                                                                        <dx:ASPxLabel ID="lblMensaje4" runat="server" Text="hghgh">
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
                                                                        <dx:ASPxButton ID="btnAceptar4" runat="server" AutoPostBack="False" ClientInstanceName="btnAceptar" Text="Aceptar" Theme="Office2003Olive" Width="72px">
                                                                            <ClientSideEvents Click="function(s, e) {
                                                                                    dgvTransacciones.UpdateEdit()	
                                                                            }" />
                                                                        </dx:ASPxButton>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxButton ID="btnCancelar4" runat="server" AutoPostBack="False" ClientInstanceName="btnCancelar" Text="Cerrar" Theme="Office2003Olive">
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
                                                    <SettingsBehavior AllowFocusedRow="True" AllowSort="False" ColumnResizeMode="NextColumn" />
                                                    <SettingsPopup>
                                                        <EditForm Modal="True" />
                                                    </SettingsPopup>
                                                    <SettingsText CommandNew="Nuevo" />
                                                    <Columns>
                                                        <dx:GridViewDataTextColumn Caption="Titular" FieldName="campo1" ShowInCustomizationForm="True" VisibleIndex="0">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Operador" FieldName="campo2" ShowInCustomizationForm="True" VisibleIndex="1">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Campo" FieldName="campo3" ShowInCustomizationForm="True" VisibleIndex="2">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="NUEVO CAMPO" FieldName="campo4" ShowInCustomizationForm="True" VisibleIndex="3">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Fecha de declaratoria de comercialidad del campo" FieldName="campo5" ShowInCustomizationForm="True" VisibleIndex="4">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Reservorio" FieldName="campo6" ShowInCustomizationForm="True" VisibleIndex="5">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="NUEVO RESERVORIO" FieldName="campo7" ShowInCustomizationForm="True" VisibleIndex="6">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Fecha de descubrimiento del reservorio" FieldName="campo8" ShowInCustomizationForm="True" VisibleIndex="7">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Periodo etapa de evaluación" FieldName="campo9" ShowInCustomizationForm="True" VisibleIndex="8">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Fecha de declaratoria de comercialidad del reservorio" FieldName="campo10" ShowInCustomizationForm="True" VisibleIndex="9">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="EN PRODUCCIÓN A LA FECHA DE PUBLICACIÓN LEY N° 767" FieldName="campo11" ShowInCustomizationForm="True" VisibleIndex="10">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Fecha de Presentación de nuevos PDD o Planes de Inversión Quinquenal por parte de los Titulares a YPFB" FieldName="campo12" ShowInCustomizationForm="True" VisibleIndex="11">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Fecha de Aprobación nuevo PDD o Plan de Inversión Quinquenal" FieldName="campo13" ShowInCustomizationForm="True" VisibleIndex="12">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Inversión Comprometida" FieldName="campo14" ShowInCustomizationForm="True" VisibleIndex="13">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Inversión Ejecutada Acumulado" FieldName="campo15" ShowInCustomizationForm="True" VisibleIndex="14">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Porcentaje de Ejecución de Inversión" FieldName="campo16" ShowInCustomizationForm="True" VisibleIndex="15">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="F.R. Final Estimado" FieldName="campo17" ShowInCustomizationForm="True" VisibleIndex="16">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="F.R. Final Estimado Despues de Ley 767" FieldName="campo18" ShowInCustomizationForm="True" VisibleIndex="17">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Pronóstico de Producción de petróleo crudo en PDD o PQI Antes de la fecha de promulgación del D.S. 2830" FieldName="campo19" ShowInCustomizationForm="True" VisibleIndex="18">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Pronóstico de Producción de petroleo crudo en PDD o PQI Actualizados despues de la fecha de promulgación del D.S. 2830" FieldName="campo20" ShowInCustomizationForm="True" VisibleIndex="19">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Producción mensual de petroleo crudo" FieldName="campo21" ShowInCustomizationForm="True" VisibleIndex="20">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="ACUMULACIÓN NO COMERCIAL" FieldName="campo22" ShowInCustomizationForm="True" VisibleIndex="21">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="FECHA INICIO DE PRODUCCIÓN" FieldName="campo23" ShowInCustomizationForm="True" VisibleIndex="22">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="CAMPO CERRADO ANTES DE LA FECHA DE PUBLICACIÓN LEY 767" FieldName="campo24" ShowInCustomizationForm="True" VisibleIndex="23">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="FECHA REACTIVACIÓN" FieldName="campo25" ShowInCustomizationForm="True" VisibleIndex="24">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="noRegistro" ShowInCustomizationForm="True" VisibleIndex="25" Visible="False">
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                </dx:ASPxGridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                                <dx:ASPxGridView ID="dgvFormE" runat="server" AutoGenerateColumns="False" ClientInstanceName="dgvFormE" ClientVisible="False" EnableTheming="True" Font-Size="X-Small" KeyFieldName="noRegistro" Theme="Office2003Olive" Width="100%" OnHtmlRowPrepared="dgvFormE_HtmlRowPrepared">
                                                    <Styles>
                                                        <Header Wrap="True" HorizontalAlign="Center"></Header>
                                                    </Styles>
                                                    <SettingsContextMenu EnableRowMenu="False">
                                                    </SettingsContextMenu>
                                                    <Templates>
                                                        <EditForm>
                                                            <table>
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel169" runat="server" Text="ID :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtId5" runat="server" ClientInstanceName="txtId" ReadOnly="True" Width="170px">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel170" runat="server" Text="Fecha :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxDateEdit ID="detFecha5" runat="server" EditFormat="DateTime" Font-Size="Smaller">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel171" runat="server" Height="16px" Text="Válido Cred. Fiscal :" Width="110px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxComboBox ID="cmbTipoCred5" runat="server" Font-Size="Smaller" ValueType="System.Int32">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel172" runat="server" Text="Tipo Pago :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxComboBox ID="cmbTipoPago5" runat="server" Font-Size="Smaller" ValueType="System.Int32">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel173" runat="server" Text="Tipo Consumidor :" Width="110px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxComboBox ID="cmbTipoCons5" runat="server" Font-Size="Smaller" ValueType="System.Int32">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel174" runat="server" Text="Cliente :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtCliente5" runat="server" Font-Size="Smaller" Height="16px" MaxLength="90" Width="170px">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel175" runat="server" Text="NIT :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtNit5" runat="server" Font-Size="Smaller" MaxLength="15" Width="170px">
                                                                            <MaskSettings Mask="&lt;0..999999999999999&gt;" PromptChar=" " />
                                                                            <ValidationSettings>
                                                                                <RequiredField IsRequired="True" />
                                                                            </ValidationSettings>
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel176" runat="server" Text="Usuario :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtUsuario5" runat="server" ClientInstanceName="txtId" Font-Size="Smaller" MaxLength="100" Width="170px">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel177" runat="server" Text="Tipo de Venta :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxRadioButtonList ID="rbtTipoVenta5" runat="server" ClientInstanceName="rbtTipoVenta" SelectedIndex="0" ValueType="System.Int32">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel178" runat="server" Text="País :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxComboBox ID="cmbPais5" runat="server">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel179" runat="server" Text="Placa :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtPlaca5" runat="server" ClientInstanceName="txtId" Font-Size="Smaller" MaxLength="11" Width="170px">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel180" runat="server" Text="No. Autorización :" Width="100px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtNoAuto5" runat="server" Font-Size="Smaller" MaxLength="15" Width="170px">
                                                                            <MaskSettings Mask="&lt;0..999999999999999&gt;" />
                                                                            <ValidationSettings>
                                                                                <RequiredField IsRequired="True" />
                                                                            </ValidationSettings>
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel181" runat="server" Text="No. Factura :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtNoFact5" runat="server" Font-Size="Smaller" MaxLength="15" Width="170px">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel182" runat="server" Text="Cod. Control :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtCodCtrl5" runat="server" Font-Size="Smaller" MaxLength="14" Width="170px">
                                                                            <MaskSettings Mask="" />
                                                                            <ValidationSettings SetFocusOnError="True">
                                                                            </ValidationSettings>
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel183" runat="server" Text="Importe :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtImporte5" runat="server" Width="170px">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel184" runat="server" Text="No. Autorización Ext. :" Width="130px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtNoAutoExt5" runat="server" ClientInstanceName="txtNoAutoExt" Font-Size="Smaller" MaxLength="15" Width="170px">
                                                                            <MaskSettings Mask="&lt;0..999999999999999&gt;" />
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel185" runat="server" Text="No. Factura Ext. :" Width="120px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtNoFactExt5" runat="server" ClientInstanceName="txtNoFactExt" Font-Size="Smaller" MaxLength="15" Width="170px">
                                                                            <MaskSettings Mask="&lt;0..999999999999999&gt;" />
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel186" runat="server" Text="Cod. Control Ext. :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtCodCtrlExt5" runat="server" Font-Size="Smaller" MaxLength="14" Width="170px">
                                                                            <MaskSettings Mask="" />
                                                                            <ValidationSettings SetFocusOnError="True">
                                                                            </ValidationSettings>
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel187" runat="server" Text="Importe Ext. :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtImporteExt5" runat="server" Width="170px">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel188" runat="server" Text="No. Doc. Referencia :" Width="130px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtNoDocRef5" runat="server" Font-Size="Smaller" MaxLength="15" Width="170px">
                                                                            <MaskSettings Mask="&lt;0..999999999999999&gt;" />
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel189" runat="server" Text="Tipo Factura :" Visible="False">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxComboBox ID="cmbTipoFact5" runat="server" Font-Size="Smaller" style="margin-top: 0" ValueType="System.Int32" Visible="False">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel190" runat="server" Text="Estado :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxComboBox ID="cmbEstado5" runat="server" Font-Size="Smaller" ValueType="System.Int32">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel191" runat="server" Text="Producto :" Width="100px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxComboBox ID="cmbProd5" runat="server" Font-Size="Smaller" ValueType="System.Int32">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel192" runat="server" Text="Cantidad :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td class="auto-style16">
                                                                        <dx:ASPxTextBox ID="txtCantidad5" runat="server" Width="170px">
                                                                            <MaskSettings IncludeLiterals="DecimalSymbol" Mask="&lt;0..9999999&gt;.&lt;00..99&gt;" />
                                                                            <ValidationSettings>
                                                                                <RequiredField IsRequired="True" />
                                                                            </ValidationSettings>
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td class="auto-style19">
                                                                        <dx:ASPxLabel ID="ASPxLabel193" runat="server" Text="Precio :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtPrecio5" runat="server" Width="170px">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel194" runat="server" Text="Autorización de Venta :" Width="150px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td class="auto-style16">
                                                                        <dx:ASPxComboBox ID="cmbAutoVta5" runat="server">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel195" runat="server" Text="No. Hoja Ruta :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtNoAutoVta5" runat="server" Font-Size="Smaller" MaxLength="15" Width="170px">
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
                                                                        <dx:ASPxLabel ID="lblMensaje5" runat="server" Text="hghgh">
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
                                                                        <dx:ASPxButton ID="btnAceptar5" runat="server" AutoPostBack="False" ClientInstanceName="btnAceptar" Text="Aceptar" Theme="Office2003Olive" Width="72px">
                                                                            <ClientSideEvents Click="function(s, e) {
                                                                                    dgvTransacciones.UpdateEdit()	
                                                                            }" />
                                                                        </dx:ASPxButton>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxButton ID="btnCancelar5" runat="server" AutoPostBack="False" ClientInstanceName="btnCancelar" Text="Cerrar" Theme="Office2003Olive">
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
                                                    <SettingsBehavior AllowFocusedRow="True" AllowSort="False" ColumnResizeMode="NextColumn" />
                                                    <SettingsPopup>
                                                        <EditForm Modal="True" />
                                                    </SettingsPopup>
                                                    <SettingsText CommandNew="Nuevo" />
                                                    <Columns>
                                                        <dx:GridViewDataTextColumn Caption="Titular" FieldName="campo1" ShowInCustomizationForm="True" VisibleIndex="0">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Operador" FieldName="campo2" ShowInCustomizationForm="True" VisibleIndex="1">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Campo" FieldName="campo3" ShowInCustomizationForm="True" VisibleIndex="2">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="NUEVO CAMPO" FieldName="campo4" ShowInCustomizationForm="True" VisibleIndex="3">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Fecha de declaratoria de comercialidad del campo" FieldName="campo5" ShowInCustomizationForm="True" VisibleIndex="4">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Reservorio" FieldName="campo6" ShowInCustomizationForm="True" VisibleIndex="5">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="NUEVO RESERVORIO" FieldName="campo7" ShowInCustomizationForm="True" VisibleIndex="6">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Fecha de descubrimiento del reservorio" FieldName="campo8" ShowInCustomizationForm="True" VisibleIndex="7">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Periodo de evaluacion" FieldName="campo9" ShowInCustomizationForm="True" VisibleIndex="8">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Fecha de declaratoria de comercialidad del reservorio" FieldName="campo10" ShowInCustomizationForm="True" VisibleIndex="9">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Fecha de suscripcion de contrato de servicios petroleros" FieldName="campo11" ShowInCustomizationForm="True" VisibleIndex="10">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="RANGO DE RECURSOS PROSPECTIVOS" FieldName="campo12" ShowInCustomizationForm="True" VisibleIndex="11">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="PLAZO DEL INCENTIVO ZT" FieldName="campo13" ShowInCustomizationForm="True" VisibleIndex="12">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="PLAZO DEL INCENTIVO ZNT" FieldName="campo14" ShowInCustomizationForm="True" VisibleIndex="13">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Fecha presentacion PDD Actualizado del Titular a YPFB" FieldName="campo15" ShowInCustomizationForm="True" VisibleIndex="14">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Fecha Aprobación PDD Actualizado/
Fecha Remisión al MH y la ANH" FieldName="campo16" ShowInCustomizationForm="True" VisibleIndex="15">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Inversión Comprometida en PDD actualizado" FieldName="campo17" ShowInCustomizationForm="True" VisibleIndex="16">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Inversión Ejecutada Acumulado" FieldName="campo18" ShowInCustomizationForm="True" VisibleIndex="17">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Porcentaje de Inversión Ejecutada" FieldName="campo19" ShowInCustomizationForm="True" VisibleIndex="18">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Linea base de produccion de condensado adicional" FieldName="campo20" ShowInCustomizationForm="True" VisibleIndex="19">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Variación de producción de condensado fiscalizada con relacion a la línea base" FieldName="campo21" ShowInCustomizationForm="True" VisibleIndex="20">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Objetivo de la inversion (de acuerdo a articulo 9 paragrafo III)" FieldName="campo22" ShowInCustomizationForm="True" VisibleIndex="21">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="noRegistro" ShowInCustomizationForm="True" VisibleIndex="22" Visible="False">
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                </dx:ASPxGridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                                <dx:ASPxGridView ID="dgvFormF" runat="server" AutoGenerateColumns="False" ClientInstanceName="dgvFormF" ClientVisible="False" EnableTheming="True" Font-Size="X-Small" KeyFieldName="noRegistro" OnHtmlRowPrepared="dgvFormF_HtmlRowPrepared" Theme="Office2003Olive" Width="100%">
                                                    <Styles>
                                                        <Header Wrap="True" HorizontalAlign="Center"></Header>
                                                    </Styles>
                                                    <SettingsContextMenu EnableRowMenu="False">
                                                    </SettingsContextMenu>
                                                    <Templates>
                                                        <EditForm>
                                                            <table>
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel196" runat="server" Text="ID :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtId6" runat="server" ClientInstanceName="txtId" ReadOnly="True" Width="170px">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel197" runat="server" Text="Fecha :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxDateEdit ID="detFecha6" runat="server" EditFormat="DateTime" Font-Size="Smaller">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel198" runat="server" Height="16px" Text="Válido Cred. Fiscal :" Width="110px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxComboBox ID="cmbTipoCred6" runat="server" Font-Size="Smaller" ValueType="System.Int32">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel199" runat="server" Text="Tipo Pago :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxComboBox ID="cmbTipoPago6" runat="server" Font-Size="Smaller" ValueType="System.Int32">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel200" runat="server" Text="Tipo Consumidor :" Width="110px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxComboBox ID="cmbTipoCons6" runat="server" Font-Size="Smaller" ValueType="System.Int32">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel201" runat="server" Text="Cliente :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtCliente6" runat="server" Font-Size="Smaller" Height="16px" MaxLength="90" Width="170px">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel202" runat="server" Text="NIT :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtNit6" runat="server" Font-Size="Smaller" MaxLength="15" Width="170px">
                                                                            <MaskSettings Mask="&lt;0..999999999999999&gt;" PromptChar=" " />
                                                                            <ValidationSettings>
                                                                                <RequiredField IsRequired="True" />
                                                                            </ValidationSettings>
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel203" runat="server" Text="Usuario :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtUsuario6" runat="server" ClientInstanceName="txtId" Font-Size="Smaller" MaxLength="100" Width="170px">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel204" runat="server" Text="Tipo de Venta :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxRadioButtonList ID="rbtTipoVenta6" runat="server" ClientInstanceName="rbtTipoVenta" SelectedIndex="0" ValueType="System.Int32">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel205" runat="server" Text="País :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxComboBox ID="cmbPais6" runat="server">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel206" runat="server" Text="Placa :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtPlaca6" runat="server" ClientInstanceName="txtId" Font-Size="Smaller" MaxLength="11" Width="170px">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel207" runat="server" Text="No. Autorización :" Width="100px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtNoAuto6" runat="server" Font-Size="Smaller" MaxLength="15" Width="170px">
                                                                            <MaskSettings Mask="&lt;0..999999999999999&gt;" />
                                                                            <ValidationSettings>
                                                                                <RequiredField IsRequired="True" />
                                                                            </ValidationSettings>
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel208" runat="server" Text="No. Factura :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtNoFact6" runat="server" Font-Size="Smaller" MaxLength="15" Width="170px">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel209" runat="server" Text="Cod. Control :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtCodCtrl6" runat="server" Font-Size="Smaller" MaxLength="14" Width="170px">
                                                                            <MaskSettings Mask="" />
                                                                            <ValidationSettings SetFocusOnError="True">
                                                                            </ValidationSettings>
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel210" runat="server" Text="Importe :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtImporte6" runat="server" Width="170px">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel211" runat="server" Text="No. Autorización Ext. :" Width="130px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtNoAutoExt6" runat="server" ClientInstanceName="txtNoAutoExt" Font-Size="Smaller" MaxLength="15" Width="170px">
                                                                            <MaskSettings Mask="&lt;0..999999999999999&gt;" />
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel212" runat="server" Text="No. Factura Ext. :" Width="120px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtNoFactExt6" runat="server" ClientInstanceName="txtNoFactExt" Font-Size="Smaller" MaxLength="15" Width="170px">
                                                                            <MaskSettings Mask="&lt;0..999999999999999&gt;" />
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel213" runat="server" Text="Cod. Control Ext. :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtCodCtrlExt6" runat="server" Font-Size="Smaller" MaxLength="14" Width="170px">
                                                                            <MaskSettings Mask="" />
                                                                            <ValidationSettings SetFocusOnError="True">
                                                                            </ValidationSettings>
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel214" runat="server" Text="Importe Ext. :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtImporteExt6" runat="server" Width="170px">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel215" runat="server" Text="No. Doc. Referencia :" Width="130px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtNoDocRef6" runat="server" Font-Size="Smaller" MaxLength="15" Width="170px">
                                                                            <MaskSettings Mask="&lt;0..999999999999999&gt;" />
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel216" runat="server" Text="Tipo Factura :" Visible="False">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxComboBox ID="cmbTipoFact6" runat="server" Font-Size="Smaller" style="margin-top: 0" ValueType="System.Int32" Visible="False">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel217" runat="server" Text="Estado :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxComboBox ID="cmbEstado6" runat="server" Font-Size="Smaller" ValueType="System.Int32">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel218" runat="server" Text="Producto :" Width="100px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxComboBox ID="cmbProd6" runat="server" Font-Size="Smaller" ValueType="System.Int32">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel219" runat="server" Text="Cantidad :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td class="auto-style16">
                                                                        <dx:ASPxTextBox ID="txtCantidad6" runat="server" Width="170px">
                                                                            <MaskSettings IncludeLiterals="DecimalSymbol" Mask="&lt;0..9999999&gt;.&lt;00..99&gt;" />
                                                                            <ValidationSettings>
                                                                                <RequiredField IsRequired="True" />
                                                                            </ValidationSettings>
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td class="auto-style19">
                                                                        <dx:ASPxLabel ID="ASPxLabel220" runat="server" Text="Precio :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtPrecio6" runat="server" Width="170px">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel221" runat="server" Text="Autorización de Venta :" Width="150px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td class="auto-style16">
                                                                        <dx:ASPxComboBox ID="cmbAutoVta6" runat="server">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel222" runat="server" Text="No. Hoja Ruta :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtNoAutoVta6" runat="server" Font-Size="Smaller" MaxLength="15" Width="170px">
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
                                                                        <dx:ASPxLabel ID="lblMensaje6" runat="server" Text="hghgh">
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
                                                                        <dx:ASPxButton ID="btnAceptar6" runat="server" AutoPostBack="False" ClientInstanceName="btnAceptar" Text="Aceptar" Theme="Office2003Olive" Width="72px">
                                                                            <ClientSideEvents Click="function(s, e) {
                                                                                    dgvTransacciones.UpdateEdit()	
                                                                            }" />
                                                                        </dx:ASPxButton>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxButton ID="btnCancelar6" runat="server" AutoPostBack="False" ClientInstanceName="btnCancelar" Text="Cerrar" Theme="Office2003Olive">
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
                                                    <SettingsBehavior AllowFocusedRow="True" AllowGroup="False" AllowSort="False" ColumnResizeMode="NextColumn" />
                                                    <SettingsPopup>
                                                        <EditForm Modal="True" />
                                                    </SettingsPopup>
                                                    <SettingsText CommandNew="Nuevo" />
                                                    <Columns>
                                                        <dx:GridViewDataTextColumn Caption="Titular" FieldName="campo1" ShowInCustomizationForm="True" VisibleIndex="0">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Operador" FieldName="campo2" ShowInCustomizationForm="True" VisibleIndex="1">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Campo" FieldName="campo3" ShowInCustomizationForm="True" VisibleIndex="2">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Reservorio" FieldName="campo4" ShowInCustomizationForm="True" VisibleIndex="3">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Volumen de producción de petroleo fiscalizado [BBL]" FieldName="campo5" ShowInCustomizationForm="True" VisibleIndex="4">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Volumen de Producción de líquido fiscalizado 
[BBL]" FieldName="campo6" ShowInCustomizationForm="True" VisibleIndex="5">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Porcentaje de producción de petroleo 
[%]" FieldName="campo7" ShowInCustomizationForm="True" VisibleIndex="6">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Volumen total de liquidos comercializado en mercado EXTERNO
[BBL]" FieldName="campo8" ShowInCustomizationForm="True" VisibleIndex="7">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Volumen total de LÍQUIDOS  comercializado en mercado INTERNO 
[BBL]" FieldName="campo9" ShowInCustomizationForm="True" VisibleIndex="8">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Volumen total de petroleo comercializado en mercado externo 
[BBL]" FieldName="campo10" ShowInCustomizationForm="True" VisibleIndex="9">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Volumen total de petroleo comercializado en mercado interno 
[BBL]" FieldName="campo11" ShowInCustomizationForm="True" VisibleIndex="10">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Volumen total de petroleo comercializado
[BBL]" FieldName="campo12" ShowInCustomizationForm="True" VisibleIndex="11">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Destino de la producción de petroleo crudo ME" FieldName="campo13" ShowInCustomizationForm="True" VisibleIndex="12">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Destino de la producción de petroleo crudo MI" FieldName="campo14" ShowInCustomizationForm="True" VisibleIndex="13">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Volumen petroleo sujeto a incentivo [BBL]" FieldName="campo15" ShowInCustomizationForm="True" VisibleIndex="14">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Zona Tradicional Produccion nuevos campos" FieldName="campo16" ShowInCustomizationForm="True" VisibleIndex="15">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Zona Tradicional Produccion nuevos reservorios en areas de explotacion" FieldName="campo17" ShowInCustomizationForm="True" VisibleIndex="16">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Zona Tradicional Reservorio de petroleo en produccion a la fecha de publicacion de Ley 767" FieldName="campo18" ShowInCustomizationForm="True" VisibleIndex="17">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Zona Tradicional Acumulaciones  descubiertas no comerciales y de campos cerrados, puestos en produccion y reactivados a la fecha de publicacion de la Ley 767" FieldName="campo19" ShowInCustomizationForm="True" VisibleIndex="18">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Zona No Tradicional Produccion nuevos campos" FieldName="campo20" ShowInCustomizationForm="True" VisibleIndex="19">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Monto total incentivo 
$us" FieldName="campo21" ShowInCustomizationForm="True" VisibleIndex="20">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="noRegistro" ShowInCustomizationForm="True" Visible="False" VisibleIndex="21">
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                </dx:ASPxGridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                                <dx:ASPxGridView ID="dgvFormG" runat="server" AutoGenerateColumns="False" ClientInstanceName="dgvFormG" ClientVisible="False" EnableTheming="True" Font-Size="X-Small" KeyFieldName="noRegistro" OnHtmlRowPrepared="dgvFormG_HtmlRowPrepared" Theme="Office2003Olive" Width="100%">
                                                    <Styles>
                                                        <Header Wrap="True" HorizontalAlign="Center"></Header>
                                                    </Styles>
                                                    <SettingsContextMenu EnableRowMenu="False">
                                                    </SettingsContextMenu>
                                                    <Templates>
                                                        <EditForm>
                                                            <table>
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel223" runat="server" Text="ID :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtId7" runat="server" ClientInstanceName="txtId" ReadOnly="True" Width="170px">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel224" runat="server" Text="Fecha :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxDateEdit ID="detFecha7" runat="server" EditFormat="DateTime" Font-Size="Smaller">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel225" runat="server" Height="16px" Text="Válido Cred. Fiscal :" Width="110px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxComboBox ID="cmbTipoCred7" runat="server" Font-Size="Smaller" ValueType="System.Int32">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel226" runat="server" Text="Tipo Pago :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxComboBox ID="cmbTipoPago7" runat="server" Font-Size="Smaller" ValueType="System.Int32">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel227" runat="server" Text="Tipo Consumidor :" Width="110px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxComboBox ID="cmbTipoCons7" runat="server" Font-Size="Smaller" ValueType="System.Int32">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel228" runat="server" Text="Cliente :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtCliente7" runat="server" Font-Size="Smaller" Height="16px" MaxLength="90" Width="170px">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel229" runat="server" Text="NIT :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtNit7" runat="server" Font-Size="Smaller" MaxLength="15" Width="170px">
                                                                            <MaskSettings Mask="&lt;0..999999999999999&gt;" PromptChar=" " />
                                                                            <ValidationSettings>
                                                                                <RequiredField IsRequired="True" />
                                                                            </ValidationSettings>
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel230" runat="server" Text="Usuario :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtUsuario7" runat="server" ClientInstanceName="txtId" Font-Size="Smaller" MaxLength="100" Width="170px">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel231" runat="server" Text="Tipo de Venta :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxRadioButtonList ID="rbtTipoVenta7" runat="server" ClientInstanceName="rbtTipoVenta" SelectedIndex="0" ValueType="System.Int32">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel232" runat="server" Text="País :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxComboBox ID="cmbPais7" runat="server">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel233" runat="server" Text="Placa :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtPlaca7" runat="server" ClientInstanceName="txtId" Font-Size="Smaller" MaxLength="11" Width="170px">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel234" runat="server" Text="No. Autorización :" Width="100px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtNoAuto7" runat="server" Font-Size="Smaller" MaxLength="15" Width="170px">
                                                                            <MaskSettings Mask="&lt;0..999999999999999&gt;" />
                                                                            <ValidationSettings>
                                                                                <RequiredField IsRequired="True" />
                                                                            </ValidationSettings>
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel235" runat="server" Text="No. Factura :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtNoFact7" runat="server" Font-Size="Smaller" MaxLength="15" Width="170px">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel236" runat="server" Text="Cod. Control :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtCodCtrl7" runat="server" Font-Size="Smaller" MaxLength="14" Width="170px">
                                                                            <MaskSettings Mask="" />
                                                                            <ValidationSettings SetFocusOnError="True">
                                                                            </ValidationSettings>
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel237" runat="server" Text="Importe :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtImporte7" runat="server" Width="170px">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel238" runat="server" Text="No. Autorización Ext. :" Width="130px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtNoAutoExt7" runat="server" ClientInstanceName="txtNoAutoExt" Font-Size="Smaller" MaxLength="15" Width="170px">
                                                                            <MaskSettings Mask="&lt;0..999999999999999&gt;" />
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel239" runat="server" Text="No. Factura Ext. :" Width="120px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtNoFactExt7" runat="server" ClientInstanceName="txtNoFactExt" Font-Size="Smaller" MaxLength="15" Width="170px">
                                                                            <MaskSettings Mask="&lt;0..999999999999999&gt;" />
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel240" runat="server" Text="Cod. Control Ext. :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtCodCtrlExt7" runat="server" Font-Size="Smaller" MaxLength="14" Width="170px">
                                                                            <MaskSettings Mask="" />
                                                                            <ValidationSettings SetFocusOnError="True">
                                                                            </ValidationSettings>
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel241" runat="server" Text="Importe Ext. :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtImporteExt7" runat="server" Width="170px">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel242" runat="server" Text="No. Doc. Referencia :" Width="130px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtNoDocRef7" runat="server" Font-Size="Smaller" MaxLength="15" Width="170px">
                                                                            <MaskSettings Mask="&lt;0..999999999999999&gt;" />
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel243" runat="server" Text="Tipo Factura :" Visible="False">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxComboBox ID="cmbTipoFact7" runat="server" Font-Size="Smaller" style="margin-top: 0" ValueType="System.Int32" Visible="False">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel244" runat="server" Text="Estado :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxComboBox ID="cmbEstado7" runat="server" Font-Size="Smaller" ValueType="System.Int32">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel245" runat="server" Text="Producto :" Width="100px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxComboBox ID="cmbProd7" runat="server" Font-Size="Smaller" ValueType="System.Int32">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel246" runat="server" Text="Cantidad :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td class="auto-style16">
                                                                        <dx:ASPxTextBox ID="txtCantidad7" runat="server" Width="170px">
                                                                            <MaskSettings IncludeLiterals="DecimalSymbol" Mask="&lt;0..9999999&gt;.&lt;00..99&gt;" />
                                                                            <ValidationSettings>
                                                                                <RequiredField IsRequired="True" />
                                                                            </ValidationSettings>
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td class="auto-style19">
                                                                        <dx:ASPxLabel ID="ASPxLabel247" runat="server" Text="Precio :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtPrecio7" runat="server" Width="170px">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel248" runat="server" Text="Autorización de Venta :" Width="150px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td class="auto-style16">
                                                                        <dx:ASPxComboBox ID="cmbAutoVta7" runat="server">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel249" runat="server" Text="No. Hoja Ruta :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtNoAutoVta7" runat="server" Font-Size="Smaller" MaxLength="15" Width="170px">
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
                                                                        <dx:ASPxLabel ID="lblMensaje7" runat="server" Text="hghgh">
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
                                                                        <dx:ASPxButton ID="btnAceptar7" runat="server" AutoPostBack="False" ClientInstanceName="btnAceptar" Text="Aceptar" Theme="Office2003Olive" Width="72px">
                                                                            <ClientSideEvents Click="function(s, e) {
                                                                                    dgvTransacciones.UpdateEdit()	
                                                                            }" />
                                                                        </dx:ASPxButton>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxButton ID="btnCancelar7" runat="server" AutoPostBack="False" ClientInstanceName="btnCancelar" Text="Cerrar" Theme="Office2003Olive">
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
                                                    <SettingsBehavior AllowFocusedRow="True" AllowGroup="False" AllowSort="False" ColumnResizeMode="NextColumn" />
                                                    <SettingsPopup>
                                                        <EditForm Modal="True" />
                                                    </SettingsPopup>
                                                    <SettingsText CommandNew="Nuevo" />
                                                    <Columns>
                                                        <dx:GridViewDataTextColumn Caption="Titular" FieldName="campo1" ShowInCustomizationForm="True" VisibleIndex="0">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Operador" FieldName="campo2" ShowInCustomizationForm="True" VisibleIndex="1">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Campo" FieldName="campo3" ShowInCustomizationForm="True" VisibleIndex="2">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Reservorio" FieldName="campo4" ShowInCustomizationForm="True" VisibleIndex="3">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Volumen de producción de condensado fiscalizado [BBL]" FieldName="campo5" ShowInCustomizationForm="True" VisibleIndex="4">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Volumen total de producción de liquido fiscalizado 
[BBL]" FieldName="campo6" ShowInCustomizationForm="True" VisibleIndex="5">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Porcentaje de producción de Condensado
" FieldName="campo7" ShowInCustomizationForm="True" VisibleIndex="6">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Volumen total de liquidos comercializado en mercado externo
[BBL]" FieldName="campo8" ShowInCustomizationForm="True" VisibleIndex="7">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Volumen total de liquidos comercializado en mercado interno
[BBL]" FieldName="campo9" ShowInCustomizationForm="True" VisibleIndex="8">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Volumen total de condensado comercializado en mercado externo 
[BBL]" FieldName="campo10" ShowInCustomizationForm="True" VisibleIndex="9">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Volumen total de condensado comercializado en mercado interno 
[BBL]" FieldName="campo11" ShowInCustomizationForm="True" VisibleIndex="10">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Volumen total de condensado comercializado  
[BBL]" FieldName="campo12" ShowInCustomizationForm="True" VisibleIndex="11">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Destino de la producción ME" FieldName="campo13" ShowInCustomizationForm="True" VisibleIndex="12">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Destino de la producción MI" FieldName="campo14" ShowInCustomizationForm="True" VisibleIndex="13">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Volumen condensado sujeto a incentivo" FieldName="campo15" ShowInCustomizationForm="True" VisibleIndex="14">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Zona Tradicional Produccion nuevos campos" FieldName="campo16" ShowInCustomizationForm="True" VisibleIndex="15">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Zona Tradicional Produccion nuevos reservorios en areas de explotacion posterior a la fecha de publicacion Ley 767" FieldName="campo17" ShowInCustomizationForm="True" VisibleIndex="16">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Zona No Tradicional Produccion nuevos campos " FieldName="campo18" ShowInCustomizationForm="True" VisibleIndex="17">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Zona No Tradicional Produccion nuevos reservorios en areas de explotacion posterior a la fecha de publicacion Ley 767" FieldName="campo19" ShowInCustomizationForm="True" VisibleIndex="18">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Monto total incentivo 
$us" FieldName="campo20" ShowInCustomizationForm="True" VisibleIndex="19">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="noRegistro" ShowInCustomizationForm="True" Visible="False" VisibleIndex="20">
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                </dx:ASPxGridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                                <dx:ASPxGridView ID="dgvFormH" runat="server" AutoGenerateColumns="False" ClientInstanceName="dgvFormH" ClientVisible="False" EnableTheming="True" Font-Size="X-Small" KeyFieldName="noRegistro" OnHtmlRowPrepared="dgvFormH_HtmlRowPrepared" Theme="Office2003Olive" Width="100%">
                                                    <Styles>
                                                        <Header Wrap="True" HorizontalAlign="Center"></Header>
                                                    </Styles>
                                                    <SettingsContextMenu EnableRowMenu="False">
                                                    </SettingsContextMenu>
                                                    <Templates>
                                                        <EditForm>
                                                            <table>
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel250" runat="server" Text="ID :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtId8" runat="server" ClientInstanceName="txtId" ReadOnly="True" Width="170px">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel251" runat="server" Text="Fecha :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxDateEdit ID="detFecha8" runat="server" EditFormat="DateTime" Font-Size="Smaller">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel252" runat="server" Height="16px" Text="Válido Cred. Fiscal :" Width="110px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxComboBox ID="cmbTipoCred8" runat="server" Font-Size="Smaller" ValueType="System.Int32">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel253" runat="server" Text="Tipo Pago :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxComboBox ID="cmbTipoPago8" runat="server" Font-Size="Smaller" ValueType="System.Int32">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel254" runat="server" Text="Tipo Consumidor :" Width="110px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxComboBox ID="cmbTipoCons8" runat="server" Font-Size="Smaller" ValueType="System.Int32">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel255" runat="server" Text="Cliente :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtCliente8" runat="server" Font-Size="Smaller" Height="16px" MaxLength="90" Width="170px">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel256" runat="server" Text="NIT :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtNit8" runat="server" Font-Size="Smaller" MaxLength="15" Width="170px">
                                                                            <MaskSettings Mask="&lt;0..999999999999999&gt;" PromptChar=" " />
                                                                            <ValidationSettings>
                                                                                <RequiredField IsRequired="True" />
                                                                            </ValidationSettings>
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel257" runat="server" Text="Usuario :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtUsuario8" runat="server" ClientInstanceName="txtId" Font-Size="Smaller" MaxLength="100" Width="170px">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel258" runat="server" Text="Tipo de Venta :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxRadioButtonList ID="rbtTipoVenta8" runat="server" ClientInstanceName="rbtTipoVenta" SelectedIndex="0" ValueType="System.Int32">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel259" runat="server" Text="País :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxComboBox ID="cmbPais8" runat="server">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel260" runat="server" Text="Placa :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtPlaca8" runat="server" ClientInstanceName="txtId" Font-Size="Smaller" MaxLength="11" Width="170px">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel261" runat="server" Text="No. Autorización :" Width="100px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtNoAuto8" runat="server" Font-Size="Smaller" MaxLength="15" Width="170px">
                                                                            <MaskSettings Mask="&lt;0..999999999999999&gt;" />
                                                                            <ValidationSettings>
                                                                                <RequiredField IsRequired="True" />
                                                                            </ValidationSettings>
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel262" runat="server" Text="No. Factura :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtNoFact8" runat="server" Font-Size="Smaller" MaxLength="15" Width="170px">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel263" runat="server" Text="Cod. Control :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtCodCtrl8" runat="server" Font-Size="Smaller" MaxLength="14" Width="170px">
                                                                            <MaskSettings Mask="" />
                                                                            <ValidationSettings SetFocusOnError="True">
                                                                            </ValidationSettings>
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel264" runat="server" Text="Importe :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtImporte8" runat="server" Width="170px">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel265" runat="server" Text="No. Autorización Ext. :" Width="130px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtNoAutoExt8" runat="server" ClientInstanceName="txtNoAutoExt" Font-Size="Smaller" MaxLength="15" Width="170px">
                                                                            <MaskSettings Mask="&lt;0..999999999999999&gt;" />
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel266" runat="server" Text="No. Factura Ext. :" Width="120px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtNoFactExt8" runat="server" ClientInstanceName="txtNoFactExt" Font-Size="Smaller" MaxLength="15" Width="170px">
                                                                            <MaskSettings Mask="&lt;0..999999999999999&gt;" />
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel267" runat="server" Text="Cod. Control Ext. :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtCodCtrlExt8" runat="server" Font-Size="Smaller" MaxLength="14" Width="170px">
                                                                            <MaskSettings Mask="" />
                                                                            <ValidationSettings SetFocusOnError="True">
                                                                            </ValidationSettings>
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel268" runat="server" Text="Importe Ext. :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtImporteExt8" runat="server" Width="170px">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel269" runat="server" Text="No. Doc. Referencia :" Width="130px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtNoDocRef8" runat="server" Font-Size="Smaller" MaxLength="15" Width="170px">
                                                                            <MaskSettings Mask="&lt;0..999999999999999&gt;" />
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel270" runat="server" Text="Tipo Factura :" Visible="False">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxComboBox ID="cmbTipoFact8" runat="server" Font-Size="Smaller" style="margin-top: 0" ValueType="System.Int32" Visible="False">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel271" runat="server" Text="Estado :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxComboBox ID="cmbEstado8" runat="server" Font-Size="Smaller" ValueType="System.Int32">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel272" runat="server" Text="Producto :" Width="100px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxComboBox ID="cmbProd8" runat="server" Font-Size="Smaller" ValueType="System.Int32">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel273" runat="server" Text="Cantidad :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td class="auto-style16">
                                                                        <dx:ASPxTextBox ID="txtCantidad8" runat="server" Width="170px">
                                                                            <MaskSettings IncludeLiterals="DecimalSymbol" Mask="&lt;0..9999999&gt;.&lt;00..99&gt;" />
                                                                            <ValidationSettings>
                                                                                <RequiredField IsRequired="True" />
                                                                            </ValidationSettings>
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td class="auto-style19">
                                                                        <dx:ASPxLabel ID="ASPxLabel274" runat="server" Text="Precio :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtPrecio8" runat="server" Width="170px">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel275" runat="server" Text="Autorización de Venta :" Width="150px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td class="auto-style16">
                                                                        <dx:ASPxComboBox ID="cmbAutoVta8" runat="server">
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
                                                                        <dx:ASPxLabel ID="ASPxLabel276" runat="server" Text="No. Hoja Ruta :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtNoAutoVta8" runat="server" Font-Size="Smaller" MaxLength="15" Width="170px">
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
                                                                        <dx:ASPxLabel ID="lblMensaje8" runat="server" Text="hghgh">
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
                                                                        <dx:ASPxButton ID="btnAceptar8" runat="server" AutoPostBack="False" ClientInstanceName="btnAceptar" Text="Aceptar" Theme="Office2003Olive" Width="72px">
                                                                            <ClientSideEvents Click="function(s, e) {
                                                                                    dgvTransacciones.UpdateEdit()	
                                                                            }" />
                                                                        </dx:ASPxButton>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxButton ID="btnCancelar8" runat="server" AutoPostBack="False" ClientInstanceName="btnCancelar" Text="Cerrar" Theme="Office2003Olive">
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
                                                    <SettingsBehavior AllowFocusedRow="True" AllowGroup="False" AllowSort="False" ColumnResizeMode="NextColumn" />
                                                    <SettingsPopup>
                                                        <EditForm Modal="True" />
                                                    </SettingsPopup>
                                                    <SettingsText CommandNew="Nuevo" />
                                                    <Columns>
                                                        <dx:GridViewDataTextColumn Caption="Titular" FieldName="campo1" ShowInCustomizationForm="True" VisibleIndex="0">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Operador" FieldName="campo2" ShowInCustomizationForm="True" VisibleIndex="1">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Campo" FieldName="campo3" ShowInCustomizationForm="True" VisibleIndex="2">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Reservorio" FieldName="campo4" ShowInCustomizationForm="True" VisibleIndex="3">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Linea Base de produccion de condensado Adicional
[BBL]" FieldName="campo5" ShowInCustomizationForm="True" VisibleIndex="4">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Produccion  total de condensado por Reservorio 
BBL" FieldName="campo6" ShowInCustomizationForm="True" VisibleIndex="5">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Volumen de produccion adicional de condensado asociado al Gas Natural fiscalizado Bbl" FieldName="campo7" ShowInCustomizationForm="True" VisibleIndex="6">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Volumen de produccion de liquido fiscalizado
Bbl" FieldName="campo8" ShowInCustomizationForm="True" VisibleIndex="7">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Porcentaje de produccion de Condensado
%" FieldName="campo9" ShowInCustomizationForm="True" VisibleIndex="8">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Volumen total de liquido comercializado en mercado externo
Bbl" FieldName="campo10" ShowInCustomizationForm="True" VisibleIndex="9">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Volumen total de condensado comercializado en mercado interno 
Bbl" FieldName="campo11" ShowInCustomizationForm="True" VisibleIndex="10">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Volumen total de líquidos comercializado 
Bbl" FieldName="campo12" ShowInCustomizationForm="True" VisibleIndex="11">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Volumen total de Condensado Asociado al Gas Natural  comercializado en mercado externo
Bbl" FieldName="campo13" ShowInCustomizationForm="True" VisibleIndex="12">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Volumen total de Condensado Asociado al Gas Natural  comercializado en mercado interno
Bbl" FieldName="campo14" ShowInCustomizationForm="True" VisibleIndex="13">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Volumen total de Condensado Asociado al Gas Natural comercializado 
Bbl" FieldName="campo15" ShowInCustomizationForm="True" VisibleIndex="14">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Destino de la produccion ME" FieldName="campo16" ShowInCustomizationForm="True" VisibleIndex="15">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Destino de la produccion MI" FieldName="campo17" ShowInCustomizationForm="True" VisibleIndex="16">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Volumen de produccion de condensado asociado al gas natural en punto de fiscalizacion sobre la linea base" FieldName="campo18" ShowInCustomizationForm="True" VisibleIndex="17">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Formula del incentivo Reservorios productores de campos en explotacion a la fecha de publicacion Ley 767" FieldName="campo19" ShowInCustomizationForm="True" VisibleIndex="18">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Observaciones" FieldName="campo20" ShowInCustomizationForm="True" VisibleIndex="19">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Monto total incentivo 
$us" FieldName="campo21" ShowInCustomizationForm="True" VisibleIndex="20">
                                                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="noRegistro" ShowInCustomizationForm="True" Visible="False" VisibleIndex="21">
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
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
                                            <td style="text-align: right">
                                                <dx:ASPxButton ID="btnExportar" runat="server" Font-Bold="True" ForeColor="Maroon" OnClick="btnExportar_Click" Text="Exportar Registros Observados" Theme="Metropolis">
                                                    <ClientSideEvents Click="function(s, e) {
	    e.processOnServer = confirm('¿Desea exportar los registros observados?'); 
    }" />
                                                    <Image IconID="export_exporttoxls_16x16">
                                                    </Image>
                                                </dx:ASPxButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                                <dx:ASPxGridView ID="dgvTransaccionError" runat="server" AutoGenerateColumns="False" ClientInstanceName="dgvTransaccionError" EnableTheming="True" Font-Size="X-Small" OnHtmlRowPrepared="dgvTransaccionError_HtmlRowPrepared" Theme="Default" Width="100%" KeyFieldName="LngId">
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
                                                                        <dx:ASPxButton ID="btnAceptar0" runat="server" AutoPostBack="False" ClientInstanceName="btnAceptar" Text="Aceptar" Theme="Office2003Olive" Width="72px">
                                                                            <ClientSideEvents Click="function(s, e) {
                                                                                    dgvTransacciones.UpdateEdit()	
                                                                            }" />
                                                                        </dx:ASPxButton>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxButton ID="btnCancelar0" runat="server" AutoPostBack="False" ClientInstanceName="btnCancelar" Text="Cerrar" Theme="Office2003Olive">
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
                                                        <dx:GridViewDataTextColumn Caption="ID MOV." FieldName="StrIdVenta" ShowInCustomizationForm="True" VisibleIndex="2" ToolTip="Identificador del Movimiento" Visible="False">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="DESCRIPCION" FieldName="StrErrores" ShowInCustomizationForm="True" VisibleIndex="11">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="NIT" FieldName="LngNit" ShowInCustomizationForm="True" VisibleIndex="5" Visible="False">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="FECHA" FieldName="LngFechaEmision" ShowInCustomizationForm="True" VisibleIndex="4" Visible="False">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="NO. FACTURA" FieldName="LngNroFactura" ShowInCustomizationForm="True" VisibleIndex="9" ToolTip="Número de Factura" Visible="False">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="NO. AUTORIZACION" FieldName="LngNroAutorizacion" ShowInCustomizationForm="True" ToolTip="Número de Autorización" VisibleIndex="7" Visible="False">
                                                            <Settings AutoFilterCondition="Contains" />
                                                            <FilterTemplate>
                                                                <dx:ASPxComboBox ID="cmbFiltroProd0" runat="server" ValueType="System.String">
                                                                </dx:ASPxComboBox>
                                                            </FilterTemplate>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="NO. REF." FieldName="LngNroReferencia" ShowInCustomizationForm="True" VisibleIndex="10" ToolTip="Número de Referencia" Visible="False">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="NUMERO DE REGISTRO OBSERVADO" FieldName="LngId" ShowInCustomizationForm="True" VisibleIndex="1" ToolTip="Número de Registro Observado" Width="20%">
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                </dx:ASPxGridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;</td>
                                            <td>&nbsp;</td>
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
                                                <dx:ASPxButton ID="btnVerificaLlave" runat="server" AutoPostBack="False" ClientInstanceName="btnVerificaLlave" Font-Bold="True" ForeColor="#003300" Text="Identificación B-sisa" Theme="Metropolis" Visible="False" Width="160px">
                                                    <Image IconID="businessobjects_bocontact2_16x16">
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
    </table>

</div>

<dx:ASPxPopupControl ID="pucDdjj" runat="server" AllowDragging="True" ClientInstanceName="pucDdjj" EnableTheming="True" HeaderText="Declaración Jurada" Height="93px" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Theme="Metropolis" Width="400px">
    <HeaderImage IconID="setup_pagesetup_16x16">
    </HeaderImage>
    <ContentCollection>
        <dx:PopupControlContentControl runat="server">
            <table width="100%">
                <tr>
                    <td colspan="2">
                        <dx:ASPxLabel ID="ASPxLabel278" runat="server" Font-Bold="True" Font-Names="Arial Narrow" ForeColor="#666666" Text="Elaborado Por" Width="100%">
                            <BorderBottom BorderStyle="Solid" BorderWidth="2px" />
                        </dx:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td style="width: 25%; text-align: right">
                        <dx:ASPxLabel ID="ASPxLabel55" runat="server" Font-Bold="True" ForeColor="#999999" Text="Nombre :">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 75%">
                        <dx:ASPxTextBox ID="txtLlaveMiddleware" runat="server" Width="100%">
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 25%; text-align: right">
                        <dx:ASPxLabel ID="ASPxLabel279" runat="server" Font-Bold="True" ForeColor="#999999" Text="Cargo :">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 75%">
                        <dx:ASPxTextBox ID="txtLlaveMiddleware0" runat="server" Width="100%">
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 25%; text-align: right">
                        <dx:ASPxLabel ID="ASPxLabel280" runat="server" Font-Bold="True" ForeColor="#999999" Text="Fecha :">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 75%">
                        <dx:ASPxDateEdit ID="ASPxDateEdit1" runat="server" Theme="Metropolis">
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
                    <td colspan="2">
                        <dx:ASPxLabel ID="ASPxLabel281" runat="server" Font-Bold="True" Font-Names="Arial Narrow" ForeColor="#666666" Text="Revisado Por" Width="100%">
                            <BorderBottom BorderStyle="Solid" BorderWidth="2px" />
                        </dx:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td style="width: 25%; text-align: right">
                        <dx:ASPxLabel ID="ASPxLabel282" runat="server" Font-Bold="True" ForeColor="#999999" Text="Nombre :">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 75%">
                        <dx:ASPxTextBox ID="txtLlaveMiddleware1" runat="server" Width="100%">
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 25%; text-align: right">
                        <dx:ASPxLabel ID="ASPxLabel283" runat="server" Font-Bold="True" ForeColor="#999999" Text="Cargo :">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 75%">
                        <dx:ASPxTextBox ID="txtLlaveMiddleware2" runat="server" Width="100%">
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 25%; text-align: right">
                        <dx:ASPxLabel ID="ASPxLabel284" runat="server" Font-Bold="True" ForeColor="#999999" Text="Fecha :">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 75%">
                        <dx:ASPxDateEdit ID="ASPxDateEdit2" runat="server" Theme="Metropolis">
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
                    <td colspan="2">
                        <dx:ASPxLabel ID="ASPxLabel285" runat="server" Font-Bold="True" Font-Names="Arial Narrow" ForeColor="#666666" Text="Aprobado Por" Width="100%">
                            <BorderBottom BorderStyle="Solid" BorderWidth="2px" />
                        </dx:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td style="width: 25%; text-align: right">
                        <dx:ASPxLabel ID="ASPxLabel286" runat="server" Font-Bold="True" ForeColor="#999999" Text="Nombre :">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 75%">
                        <dx:ASPxTextBox ID="txtLlaveMiddleware3" runat="server" Width="100%">
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 25%; text-align: right">
                        <dx:ASPxLabel ID="ASPxLabel287" runat="server" Font-Bold="True" ForeColor="#999999" Text="Cargo :">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 75%">
                        <dx:ASPxTextBox ID="txtLlaveMiddleware4" runat="server" Width="100%">
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 25%; text-align: right">
                        <dx:ASPxLabel ID="ASPxLabel288" runat="server" Font-Bold="True" ForeColor="#999999" Text="Fecha :">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 75%; padding-bottom: 10px">
                        <dx:ASPxDateEdit ID="ASPxDateEdit3" runat="server" Theme="Metropolis">
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
                    <td colspan="2">
                        <dx:ASPxButton ID="btnSubirLlave" runat="server" EnableTheming="True" Text="Grabar Declaración Jurada" Theme="Metropolis" Width="100%">
                            <Image IconID="actions_apply_16x16">
                            </Image>
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>
















