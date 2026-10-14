<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtrFlujoOperadorOrig.ascx.cs" Inherits="AnhHydroOctanoPresentacion.Controles.CtrFlujoOperadorOrig" %>

<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>

<style type="text/css">
    .auto-style5 {
        width: 168px;
    }
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
    
    </style>

<table style="width: 100%">
    <tr>
        <td style="text-align: center;">
            <dx:ASPxRoundPanel runat="server" ShowHeader="False" Width="100%" ID="pnlTitulo">
                <PanelCollection>
                    <dx:PanelContent runat="server">
                        <dx:ASPxLabel runat="server" ForeColor="#367B2F" ID="lblTitulo" Font-Bold="True" Font-Names="Rockwell" Font-Size="Larger" Text="DETALLE DE MOVIMIENTOS"></dx:ASPxLabel>
                </dx:PanelContent>
            </PanelCollection>
            <Border BorderStyle="Ridge" BorderWidth="1px" BorderColor="#003300"></Border>
            </dx:ASPxRoundPanel>
        </td>
    </tr>
</table>

<dx:ASPxSplitter ID="ASPxSplitter1" runat="server" Height="700px">
    <Panes>
        <dx:SplitterPane Size="11%" ScrollBars="Auto">
            <ContentCollection>
                <dx:SplitterContentControl runat="server">
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <dx:ASPxCallbackPanel ID="pnlCabecera" runat="server" ClientInstanceName="pnlCabecera" OnCallback="pnlCabecera_Callback" Width="160px">
                                    <PanelCollection>
                                        <dx:PanelContent runat="server">
                                            <table style="width: 100%">
                                                <tr>
                                                    <td colspan="2">
                                                        <dx:ASPxLabel ID="ASPxLabel338" runat="server" ForeColor="#003300" Text="Selección de Fechas :">
                                                        </dx:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>De :</td>
                                                    <td>
                                                        <dx:ASPxDateEdit ID="detFechaIni" runat="server" ClientInstanceName="detFechaIni" EnableTheming="True" Height="23px" Theme="Metropolis" Width="120px" ToolTip="Seleccione la fecha inicial">
                                                            <TimeSectionProperties>
                                                                <TimeEditProperties>
                                                                    <ClearButton Visibility="Auto">
                                                                    </ClearButton>
                                                                </TimeEditProperties>
                                                            </TimeSectionProperties>
                                                            <ClientSideEvents DateChanged="function(s, e) {
cmbTipoOperacion.PerformCallback();
pnlDatos.PerformCallback();
}" />
                                                            <ClearButton Visibility="Auto">
                                                            </ClearButton>
                                                        </dx:ASPxDateEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>A :</td>
                                                    <td>
                                                        <dx:ASPxDateEdit ID="detFechaFin" runat="server" ClientInstanceName="detFechaFin" Theme="Metropolis" Width="120px" ToolTip="Seleccione la fecha final">
                                                            <TimeSectionProperties>
                                                                <TimeEditProperties>
                                                                    <ClearButton Visibility="Auto">
                                                                    </ClearButton>
                                                                </TimeEditProperties>
                                                            </TimeSectionProperties>
                                                            <ClientSideEvents DateChanged="function(s, e) {
cmbTipoOperacion.PerformCallback();
pnlDatos.PerformCallback();
}" />
                                                            <ClearButton Visibility="Auto">
                                                            </ClearButton>
                                                        </dx:ASPxDateEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <dx:ASPxLabel ID="ASPxLabel339" runat="server" ForeColor="#003300" Text="Tipo de Operación :">
                                                        </dx:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <dx:ASPxComboBox ID="cmbTipoOperacion" runat="server" ClientInstanceName="cmbTipoOperacion" OnCallback="cmbTipoOperacion_Callback" TextField="NOMBRE" TextFormatString="{0};{2};" Theme="Metropolis" ValueField="ID_TIPO_OPERACION" Width="160px" ToolTip="Seleccione un tipo de operación">
                                                            <ClientSideEvents CloseUp="function(s, e) {
	pnlDatos.PerformCallback();
}" />
                                                            <Columns>
                                                                <dx:ListBoxColumn Caption="NOMBRE" FieldName="NOMBRE" Name="NOMBRE" />
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
                                <dx:ASPxTreeList ID="tlsOperadores" runat="server" AutoGenerateColumns="False" ClientInstanceName="tlsOperadores" Font-Size="Smaller" Height="187px" KeyFieldName="intId" ParentFieldName="intIdPadre" Theme="Youthful" Styles-Header-BackColor="#317D40" ToolTip="Seleccione una entidad">
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
cmbTipoOperacion.SetValue(&quot;&quot;);

pnlCabecera.PerformCallback();
pnlDatos.PerformCallback();
}" Init="function(s, e) {
pnlCabecera.PerformCallback();
pnlDatos.PerformCallback();
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
                                        <td>
                                            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Rockwell" Font-Size="Large" ForeColor="#367B2F" Text="DATOS DE LA ENTIDAD"></asp:Label>
                                        </td>
                                        <td></td>
                                        <td>
                                            &nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td colspan="2">
                                            &nbsp;</td>
                                        <td style="text-align: right;">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan="7">
                                            <hr/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style5">
                                            <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="#10420F" Text="NOMBRE COMERCIAL"></asp:Label>
                                        </td>
                                        <td></td>
                                        <td>
                                            <asp:Label ID="Label3" runat="server" Font-Bold="True" ForeColor="#10420F" Text="NIT"></asp:Label>
                                        </td>
                                        <td></td>
                                        <td>
                                            <asp:Label ID="Label5" runat="server" Font-Bold="True" ForeColor="#10420F" Text="DEPARTAMENTO"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label4" runat="server" Font-Bold="True" ForeColor="#10420F" Text="DIRECCION"></asp:Label>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblRazonSocial" runat="server"></asp:Label>
                                        </td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:Label ID="lblNit" runat="server"></asp:Label>
                                        </td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:Label ID="lblDepto" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblRepLegal" runat="server"></asp:Label>
                                        </td>
                                        <td style="width: 160px; text-align: right;">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan="7">
                                            <dx:ASPxGridView ID="dgvTransacciones" runat="server" Theme="Default" AutoGenerateColumns="False" KeyFieldName="ID_MOVIMIENTO" EnableTheming="True" Font-Size="Smaller" OnHtmlEditFormCreated="dgvTransacciones_HtmlEditFormCreated" ClientInstanceName="dgvTransacciones" OnRowInserting="dgvTransacciones_RowInserting" OnRowUpdating="dgvTransacciones_RowUpdating" OnHtmlRowPrepared="dgvTransacciones_HtmlRowPrepared" Width="100%" OnRowValidating="dgvTransacciones_RowValidating" OnCustomButtonCallback="dgvTransacciones_CustomButtonCallback" OnCustomSummaryCalculate="dgvTransacciones_CustomSummaryCalculate">
                                                <SettingsContextMenu EnableRowMenu="False">
                                                </SettingsContextMenu>
                                                <Templates>
                                                    <GroupFooterRow>
                                                        <dx:ASPxLabel ID="ASPxLabel38" runat="server" Text="ASPxLabel">
                                                        </dx:ASPxLabel>
                                                    </GroupFooterRow>
                                                    <EditForm >
                                                        <table style="width: 100%">
                                                            <tr>
                                                                <td style="width: 25%; text-align: right;">
                                                                    <dx:ASPxLabel ID="ASPxLabel2" runat="server" Font-Bold="True" ForeColor="#003300" Text="Fecha :">
                                                                    </dx:ASPxLabel>
                                                                </td>
                                                                <td style="width: 25%">
                                                                    <dx:ASPxDateEdit ID="detFechaEf" runat="server" EditFormat="DateTime" Font-Bold="False" Font-Size="Smaller" Theme="Metropolis" Width="260px">
                                                                        <TimeSectionProperties>
                                                                            <TimeEditProperties>
                                                                                <ClearButton Visibility="Auto">
                                                                                </ClearButton>
                                                                            </TimeEditProperties>
                                                                        </TimeSectionProperties>
                                                                        <ClearButton Visibility="Auto">
                                                                        </ClearButton>
                                                                        <ValidationSettings>
                                                                            <RequiredField ErrorText="ERROR: Introduzca la fecha." IsRequired="True" />
                                                                        </ValidationSettings>
                                                                        <ReadOnlyStyle BackColor="#DEE8DB">
                                                                        </ReadOnlyStyle>
                                                                        <FocusedStyle BackColor="#FFFFCC">
                                                                        </FocusedStyle>
                                                                        <DisabledStyle BackColor="#DEE8DB">
                                                                        </DisabledStyle>
                                                                    </dx:ASPxDateEdit>
                                                                </td>
                                                                <td style="width: 25%; text-align: right;">
                                                                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="ID :" Font-Bold="True" ForeColor="#003300">
                                                                    </dx:ASPxLabel>
                                                                </td>
                                                                <td style="width: 25%">
                                                                    <dx:ASPxTextBox ID="txtIdEf" runat="server" ClientInstanceName="txtIdEf" Width="260px" Text="0" ReadOnly="True">
                                                                        <MaskSettings Mask="&lt;0..999999999999999&gt;" />
                                                                        <ReadOnlyStyle BackColor="#DEE8DB">
                                                                        </ReadOnlyStyle>
                                                                        <DisabledStyle BackColor="#DEE8DB">
                                                                        </DisabledStyle>
                                                                    </dx:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 25%; text-align: right;">
                                                                    <dx:ASPxLabel ID="lblTipoPagoEf" runat="server" Font-Bold="True" ForeColor="#003300" Text="Tipo de Flujo :">
                                                                    </dx:ASPxLabel>
                                                                </td>
                                                                <td style="width: 25%">
                                                                    <dx:ASPxComboBox ID="cmbTipoFlujoEf" runat="server" ClientInstanceName="cmbTipoFlujoEf" Font-Size="Smaller" TextField="DESCRIPCION" TextFormatString="{0}" Theme="Metropolis" ValueField="ID_PARAMETRICA_GLOBAL" Width="260px">
                                                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
	if(cmbTipoPagoEf.GetValue()==3 || cmbTipoPagoEf.GetValue()==-99999){
cmbTipoCredEf.SetValue(9);
	}
else{
cmbTipoCredEf.SetValue(10);
}
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
                                                                        <ReadOnlyStyle BackColor="#DEE8DB">
                                                                        </ReadOnlyStyle>
                                                                        <FocusedStyle BackColor="#FFFFCC">
                                                                        </FocusedStyle>
                                                                        <DisabledStyle BackColor="#DEE8DB">
                                                                        </DisabledStyle>
                                                                    </dx:ASPxComboBox>
                                                                </td>
                                                                <td style="width: 25%; text-align: right;">
                                                                    <dx:ASPxLabel ID="lblEstadoEf" runat="server" Text="Estado :" Font-Bold="True" ForeColor="#003300">
                                                                    </dx:ASPxLabel>
                                                                </td>
                                                                <td style="width: 25%">
                                                                    <dx:ASPxComboBox ID="cmbEstadoEf" runat="server" Font-Size="Smaller" ValueType="System.Int32" SelectedIndex="0" Width="260px" Theme="Metropolis">
                                                                        <Items>
                                                                            <dx:ListEditItem Text="VALIDO" Value="11" Selected="True" />
                                                                            <dx:ListEditItem Text="ANULADO" Value="12" />
                                                                        </Items>
                                                                        <ClearButton Visibility="Auto">
                                                                        </ClearButton>
                                                                        <ValidationSettings>
                                                                            <RequiredField IsRequired="True" ErrorText="ERROR: Introduzca el estado." />
                                                                        </ValidationSettings>
                                                                        <ReadOnlyStyle BackColor="#DEE8DB">
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
                                                                    <dx:ASPxLabel ID="lblEntidadAuxialiarEf" runat="server" Font-Bold="True" ForeColor="#003300" Text="Proyecto Origen/Destino :" Width="100%">
                                                                    </dx:ASPxLabel>
                                                                </td>
                                                                <td style="width: 25%">
                                                                    <dx:ASPxComboBox ID="cmbEntidadAuxialiarEf" runat="server" Font-Size="Smaller" Width="260px" Theme="Metropolis" TextField="NOMBRE" TextFormatString="{0}" ValueField="ID_IDENTIFICADOR">
                                                                        <Columns>
                                                                            <dx:ListBoxColumn Caption="RAZON SOCIAL" FieldName="NOMBRE" />
                                                                            <dx:ListBoxColumn Caption="ALIAS" FieldName="DESCRIPCION" />
                                                                            <dx:ListBoxColumn Caption="ID_IDENTIFICADOR" FieldName="ID_IDENTIFICADOR" Width="0px" />
                                                                            <dx:ListBoxColumn Caption="ACTIVIDAD" FieldName="SUBID_NOMBRE" />
                                                                            <dx:ListBoxColumn Caption="ID_SUB_IDENTIFICADOR" FieldName="ID_SUB_IDENTIFICADOR" Width="0px" />
                                                                        </Columns>
                                                                        <ClearButton Visibility="Auto">
                                                                        </ClearButton>
                                                                        <ReadOnlyStyle BackColor="#DEE8DB">
                                                                        </ReadOnlyStyle>
                                                                        <FocusedStyle BackColor="#FFFFCC">
                                                                        </FocusedStyle>
                                                                        <DisabledStyle BackColor="#DEE8DB">
                                                                        </DisabledStyle>
                                                                    </dx:ASPxComboBox>
                                                                </td>
                                                                <td style="width: 25%; text-align: right;">
                                                                    <dx:ASPxLabel ID="lblIntegracionEf" runat="server" Font-Bold="True" ForeColor="#003300" Text="Id. Integración :">
                                                                    </dx:ASPxLabel>
                                                                </td>
                                                                <td style="width: 25%">
                                                                    <dx:ASPxTextBox ID="txtIntegracionEf" runat="server" ClientInstanceName="txtIdEf" Text="0" Width="260px">
                                                                        <MaskSettings Mask="&lt;0..999999999999999&gt;" />
                                                                        <ReadOnlyStyle BackColor="#DEE8DB">
                                                                        </ReadOnlyStyle>
                                                                        <DisabledStyle BackColor="#DEE8DB">
                                                                        </DisabledStyle>
                                                                    </dx:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 25%; text-align: right;">
                                                                    <dx:ASPxLabel ID="lblTipoMovEf" runat="server" Font-Bold="True" ForeColor="#003300" Text="Tipo Movimiento :">
                                                                    </dx:ASPxLabel>
                                                                </td>
                                                                <td style="width: 25%">
                                                                    <dx:ASPxComboBox ID="cmbTipoMovEf" runat="server" Font-Size="Smaller" Width="260px" TextField="DESCRIPCION" TextFormatString="{0}" ValueField="ID_PARAMETRICA_GLOBAL" ClientInstanceName="cmbTipoMovEf" Theme="Metropolis">
                                                                        <ClientSideEvents ValueChanged="function(s, e) {
	

var item = s.GetSelectedItem();
if(item)
{

if(item.GetColumnText('CODIGO')=='NN')
{
cmbPaisEf.SetVisible(false);
lblPaisEf.SetVisible(false);
}
else
{
cmbPaisEf.SetVisible(true);
lblPaisEf.SetVisible(true);
}


}


}" />
                                                                        <Columns>
                                                                            <dx:ListBoxColumn Caption="DESCRIPCION" FieldName="DESCRIPCION" Name="DESCRIPCION" />
                                                                            <dx:ListBoxColumn Caption="ID_PARAMETRICA_GLOBAL" FieldName="ID_PARAMETRICA_GLOBAL" Name="ID_PARAMETRICA_GLOBAL" Width="0px" />
                                                                            <dx:ListBoxColumn Caption="CODIGO" FieldName="CODIGO" Name="CODIGO" Width="0px" />
                                                                        </Columns>
                                                                        <ClearButton Visibility="Auto">
                                                                        </ClearButton>
                                                                        <ValidationSettings ValidateOnLeave="False">
                                                                        </ValidationSettings>
                                                                        <ReadOnlyStyle BackColor="#DEE8DB">
                                                                        </ReadOnlyStyle>
                                                                        <FocusedStyle BackColor="#FFFFCC">
                                                                        </FocusedStyle>
                                                                        <DisabledStyle BackColor="#DEE8DB">
                                                                        </DisabledStyle>
                                                                    </dx:ASPxComboBox>
                                                                </td>
                                                                <td style="width: 25%; text-align: right;">
                                                                    <dx:ASPxLabel ID="lblPaisEf" runat="server" ClientInstanceName="lblPaisEf" Font-Bold="True" ForeColor="#003300" Text="País :">
                                                                        <ClientSideEvents Init="function(s, e) {

try{
var item = cmbTipoMovEf.GetSelectedItem();
if(item)
{
if(item.GetColumnText('CODIGO')=='NN')
{
lblPaisEf.SetVisible(false);
}
}
}
catch(e){
lblPaisEf.SetVisible(false);
}

}" />
                                                                    </dx:ASPxLabel>
                                                                </td>
                                                                <td style="width: 25%">
                                                                    <dx:ASPxComboBox ID="cmbPaisEf" runat="server" ClientInstanceName="cmbPaisEf" Width="260px" TextField="DESCRIPCION" TextFormatString="{0}" ValueField="ID_UBICACION_GEOGRAFICA" Theme="Metropolis">
                                                                        <ClientSideEvents Init="function(s, e) {

try{
var item = cmbTipoMovEf.GetSelectedItem();
if(item)
{
if(item.GetColumnText('CODIGO')=='NN')
{
cmbPaisEf.SetVisible(false);
}
}
}
catch(e){
cmbPaisEf.SetVisible(false);
}

}" />
                                                                        <Columns>
                                                                            <dx:ListBoxColumn Caption="DESCRIPCION" FieldName="DESCRIPCION" Name="DESCRIPCION" />
                                                                            <dx:ListBoxColumn Caption="ID_UBICACION_GEOGRAFICA" FieldName="ID_UBICACION_GEOGRAFICA" Name="ID_UBICACION_GEOGRAFICA" Width="0px" />
                                                                        </Columns>
                                                                        <ClearButton Visibility="Auto">
                                                                        </ClearButton>
                                                                        <ReadOnlyStyle BackColor="#DEE8DB">
                                                                        </ReadOnlyStyle>
                                                                        <FocusedStyle BackColor="#FFFFCC">
                                                                        </FocusedStyle>
                                                                        <DisabledStyle BackColor="#DEE8DB">
                                                                        </DisabledStyle>
                                                                    </dx:ASPxComboBox>
                                                                </td>
                                                            </tr>
                                                            
                                                            <tr>
                                                                <td style="width: 25%; text-align: right; padding-bottom: 10px;">
                                                                    <dx:ASPxLabel ID="lblMedioTransEf" runat="server" Font-Bold="True" ForeColor="#003300" Text="Infraestructura (Orig./Dest.) :" Width="100%">
                                                                    </dx:ASPxLabel>
                                                                </td>
                                                                <td style="width: 25%; padding-bottom: 10px;">
                                                                    <dx:ASPxComboBox ID="cmbMedioTransEf" runat="server" Font-Size="Smaller" TextField="CODIGO_OPERADOR" TextFormatString="{0}" Theme="Metropolis" ValueField="ID_INFRA" Width="260px" ClientInstanceName="cmbMedioTransEf">
                                                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
var item = s.GetSelectedItem();
if(item)
{
cmbTipoTransEf.SetValue(item.GetColumnText('ID_TIPO'));
}
}" />
                                                                        <Columns>
                                                                            <dx:ListBoxColumn Caption="CODIGO" FieldName="CODIGO_OPERADOR" Name="CODIGO_OPERADOR" />
                                                                            <dx:ListBoxColumn Caption="ID_INFRA" FieldName="ID_INFRA" Name="ID_INFRA" Width="0px" />
                                                                            <dx:ListBoxColumn Caption="ID_INFRAESTRUCTURA" FieldName="ID_INFRAESTRUCTURA" Name="ID_INFRAESTRUCTURA" Width="0px" />
                                                                            <dx:ListBoxColumn Caption="ID_PRODUCTO" FieldName="ID_PRODUCTO" Name="ID_PRODUCTO" Width="0px" />
                                                                            <dx:ListBoxColumn Caption="ID_UNIDAD_MEDIDA" FieldName="ID_UNIDAD_MEDIDA" Name="ID_UNIDAD_MEDIDA" Width="0px" />
                                                                            <dx:ListBoxColumn Caption="ID_TIPO" FieldName="ID_TIPO" Name="ID_TIPO" Width="0px" />
                                                                        </Columns>
                                                                        <Items>
                                                                            <dx:ListEditItem Text="No Definido" Value="-99999" />
                                                                        </Items>
                                                                        <ClearButton Visibility="Auto">
                                                                        </ClearButton>
                                                                        <ValidationSettings>
                                                                            <RequiredField ErrorText="ERROR: Introduzca el medio de transporte." IsRequired="True" />
                                                                        </ValidationSettings>
                                                                        <ReadOnlyStyle BackColor="#DEE8DB">
                                                                        </ReadOnlyStyle>
                                                                        <FocusedStyle BackColor="#FFFFCC">
                                                                        </FocusedStyle>
                                                                        <DisabledStyle BackColor="#DEE8DB">
                                                                        </DisabledStyle>
                                                                    </dx:ASPxComboBox>
                                                                </td>
                                                                <td style="width: 25%; text-align: right; padding-bottom: 10px;">
                                                                    <dx:ASPxLabel ID="lblTipoTransEf" runat="server" Font-Bold="True" ForeColor="#003300" Text="Tipo Infra. (Orig./Dest.) :" Width="100%">
                                                                    </dx:ASPxLabel>
                                                                </td>
                                                                <td style="width: 25%; padding-bottom: 10px;">
                                                                    <dx:ASPxComboBox ID="cmbTipoTransEf" runat="server" Font-Size="Smaller" TextField="VALOR" TextFormatString="{0}" Theme="Metropolis" ValueField="ID_PARAMETRO" Width="260px" ClientInstanceName="cmbTipoTransEf">
                                                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
var item = s.GetSelectedItem();
if(item)
{
cmbMedioTransEf.SetValue('9999999999');
}
}
" />
                                                                        <Columns>
                                                                            <dx:ListBoxColumn Caption="NOMBRE" FieldName="VALOR" Name="VALOR" />
                                                                            <dx:ListBoxColumn Caption="ID_PARAMETRO" FieldName="ID_PARAMETRO" Name="ID_PARAMETRO" Width="0px" />
                                                                            <dx:ListBoxColumn Caption="CODIGO" FieldName="CODIGO" Name="CODIGO" Width="0px" />
                                                                        </Columns>
                                                                        <ClearButton Visibility="Auto">
                                                                        </ClearButton>
                                                                        <ValidationSettings>
                                                                            <RequiredField ErrorText="ERROR: Introduzca el tipo de transporte." IsRequired="True" />
                                                                        </ValidationSettings>
                                                                        <ReadOnlyStyle BackColor="#DEE8DB">
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
                                                                    <dx:ASPxLabel ID="lblTanqueEf" runat="server" Font-Bold="True" ForeColor="#003300" Text="Infraestructura (Recep./Desp.) :" Width="100%">
                                                                    </dx:ASPxLabel>
                                                                </td>
                                                                <td style="width: 25%">
                                                                    <dx:ASPxComboBox ID="cmbTanqueEf" runat="server" ClientInstanceName="cmbTanqueEf" Font-Size="Smaller" TextField="CODIGO_OPERADOR" TextFormatString="{0}" Theme="Metropolis" ValueField="ID_INFRA" Width="260px">
                                                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
var item = s.GetSelectedItem();
if(item)
{

cmbTipoInfEf.SetValue(item.GetColumnText('ID_TIPO'));

if(item.GetColumnText('ID_INFRAESTRUCTURA')!=-99999)
{
cmbProdEf.SetValue(item.GetColumnText('ID_PRODUCTO'));
var prod = cmbProdEf.GetSelectedItem();
if(prod)
{
cmbUnidadEf.SetValue(prod.GetColumnText('ID_SUB_IDENTIFICADOR'));
}
}
}
}" />
                                                                        <Columns>
                                                                            <dx:ListBoxColumn Caption="CODIGO" FieldName="CODIGO_OPERADOR" Name="CODIGO_OPERADOR" />
                                                                            <dx:ListBoxColumn Caption="ID_INFRA" FieldName="ID_INFRA" Name="ID_INFRA" Width="0px" />
                                                                            <dx:ListBoxColumn Caption="ID_INFRAESTRUCTURA" FieldName="ID_INFRAESTRUCTURA" Name="ID_INFRAESTRUCTURA" Width="0px" />
                                                                            <dx:ListBoxColumn Caption="ID_PRODUCTO" FieldName="ID_PRODUCTO" Name="ID_PRODUCTO" Width="0px" />
                                                                            <dx:ListBoxColumn Caption="ID_UNIDAD_MEDIDA" FieldName="ID_UNIDAD_MEDIDA" Name="ID_UNIDAD_MEDIDA" Width="0px" />
                                                                            <dx:ListBoxColumn Caption="ID_TIPO" FieldName="ID_TIPO" Name="ID_TIPO" Width="0px" />
                                                                        </Columns>
                                                                        <ClearButton Visibility="Auto">
                                                                        </ClearButton>
                                                                        <ReadOnlyStyle BackColor="#DEE8DB">
                                                                        </ReadOnlyStyle>
                                                                        <FocusedStyle BackColor="#FFFFCC">
                                                                        </FocusedStyle>
                                                                        <DisabledStyle BackColor="#DEE8DB">
                                                                        </DisabledStyle>
                                                                    </dx:ASPxComboBox>
                                                                </td>
                                                                <td style="width: 25%; text-align: right;">
                                                                    <dx:ASPxLabel ID="lblTipoInfEf" runat="server" ClientInstanceName="lblTipoInfEf" Font-Bold="True" ForeColor="#003300" Text="Tipo Infra. (Recep./Desp.) :">
                                                                    </dx:ASPxLabel>
                                                                </td>
                                                                <td style="width: 25%">
                                                                    <dx:ASPxComboBox ID="cmbTipoInfEf" runat="server" ClientInstanceName="cmbTipoInfEf" Font-Size="Smaller" ReadOnly="True" TextField="VALOR" TextFormatString="{0}" Theme="Metropolis" ValueField="ID_PARAMETRO" Width="260px">
                                                                        <Columns>
                                                                            <dx:ListBoxColumn Caption="NOMBRE" FieldName="VALOR" Name="VALOR" />
                                                                            <dx:ListBoxColumn Caption="ID_PARAMETRO" FieldName="ID_PARAMETRO" Name="ID_PARAMETRO" Width="0px" />
                                                                            <dx:ListBoxColumn Caption="CODIGO" FieldName="CODIGO" Name="CODIGO" Width="0px" />
                                                                        </Columns>
                                                                        <ClearButton Visibility="Auto">
                                                                        </ClearButton>
                                                                        <ValidationSettings>
                                                                            <RequiredField ErrorText="ERROR: Introduzca el tipo de transporte." IsRequired="True" />
                                                                        </ValidationSettings>
                                                                        <ReadOnlyStyle BackColor="#DEE8DB">
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
                                                                    <dx:ASPxLabel ID="lblProdEf" runat="server" Text="Producto :" Width="100px" Font-Bold="True" ForeColor="#003300">
                                                                    </dx:ASPxLabel>
                                                                </td>
                                                                <td style="width: 25%">
                                                                    <dx:ASPxComboBox ID="cmbProdEf" runat="server" Font-Size="Smaller" ClientInstanceName="cmbProdEf" TextField="PRODUCTO" TextFormatString="{0}" ValueField="ID_IDENTIFICADOR" Width="260px" Theme="Metropolis">
                                                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
try{
var tanque = cmbTanqueEf.GetSelectedItem();
if(tanque)
{
if(tanque.GetColumnText('ID_INFRAESTRUCTURA')!=-99999)
{
s.SetValue(tanque.GetColumnText('ID_PRODUCTO'));
}
var prod = s.GetSelectedItem();
if(prod)
{
cmbUnidadEf.SetValue(prod.GetColumnText('ID_SUB_IDENTIFICADOR'));
}
}

}
catch(e){
var prod = s.GetSelectedItem();
if(prod)
{
cmbUnidadEf.SetValue(prod.GetColumnText('ID_SUB_IDENTIFICADOR'));
}

}


}
" Init="function(s, e) {
try{
var tanque = cmbTanqueEf.GetSelectedItem();
if(tanque)
{
cmbTipoInfEf.SetValue(tanque.GetColumnText('ID_TIPO'));
if(tanque.GetColumnText('ID_INFRAESTRUCTURA')!=-99999)
{
s.SetValue(tanque.GetColumnText('ID_PRODUCTO'));
}
}
}
catch(e){
}
}
" />
                                                                        <Columns>
                                                                            <dx:ListBoxColumn Caption="PRODUCTO" FieldName="NOMBRE" Name="NOMBRE" />
                                                                            <dx:ListBoxColumn Caption="ID_IDENTIFICADOR" FieldName="ID_IDENTIFICADOR" Name="ID_IDENTIFICADOR" Width="0px" />
                                                                            <dx:ListBoxColumn Caption="ID_SUB_IDENTIFICADOR" FieldName="ID_SUB_IDENTIFICADOR" Name="ID_SUB_IDENTIFICADOR" Width="0px" />
                                                                        </Columns>
                                                                        <ClearButton Visibility="Auto">
                                                                        </ClearButton>
                                                                        <ValidationSettings>
                                                                            <RequiredField IsRequired="True" ErrorText="ERROR: Introduzca el producto." />
                                                                        </ValidationSettings>
                                                                        <ReadOnlyStyle BackColor="#DEE8DB">
                                                                        </ReadOnlyStyle>
                                                                        <FocusedStyle BackColor="#FFFFCC">
                                                                        </FocusedStyle>
                                                                        <DisabledStyle BackColor="#DEE8DB">
                                                                        </DisabledStyle>
                                                                    </dx:ASPxComboBox>
                                                                </td>
                                                                <td style="width: 25%; text-align: right;">
                                                                    <dx:ASPxLabel ID="lblUnidadEf" runat="server" Text="Unidad de Medida :" Font-Bold="True" ForeColor="#003300">
                                                                    </dx:ASPxLabel>
                                                                </td>
                                                                <td style="width: 25%">
                                                                    <dx:ASPxComboBox ID="cmbUnidadEf" runat="server" Font-Size="Smaller" ClientInstanceName="cmbUnidadEf" Width="260px" TextField="NOMBRE" TextFormatString="{0}" ValueField="ID_CATALOGO" ReadOnly="True" Theme="Metropolis">
                                                                        <ClientSideEvents Init="function(s, e) {
	
var item = cmbProdEf.GetSelectedItem();
if(item)
{
s.SetValue(item.GetColumnText('ID_SUB_IDENTIFICADOR'));
}

}" />
                                                                        <Columns>
                                                                            <dx:ListBoxColumn Caption="NOMBRE" FieldName="NOMBRE" Name="NOMBRE" />
                                                                            <dx:ListBoxColumn Caption="ID_CATALOGO" FieldName="ID_CATALOGO" Name="ID_CATALOGO" Width="0px" />
                                                                            <dx:ListBoxColumn Caption="CODIGO" FieldName="CODIGO" Name="CODIGO" Width="0px" />
                                                                        </Columns>
                                                                        <ClearButton Visibility="Auto">
                                                                        </ClearButton>
                                                                        <ReadOnlyStyle BackColor="#DEE8DB">
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
                                                                    <dx:ASPxLabel ID="lblCantidadEf" runat="server" Font-Bold="True" ForeColor="#003300" Text="Cantidad :">
                                                                    </dx:ASPxLabel>
                                                                </td>
                                                                <td style="width: 25%">
                                                                    <dx:ASPxTextBox ID="txtCantidadEf" runat="server" ClientInstanceName="txtCantidadEf" Text="0" Width="260px">
                                                                        <MaskSettings IncludeLiterals="DecimalSymbol" Mask="&lt;0..9999999&gt;.&lt;00..99&gt;" />
                                                                        <ValidationSettings>
                                                                            <RequiredField ErrorText="ERROR: Introduzca la cantidad." IsRequired="True" />
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
                                                                <td style="width: 25%">&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 25%; text-align: right;">
                                                                    <dx:ASPxLabel ID="lblTipoMedicEf" runat="server" Font-Bold="True" ForeColor="#003300" Text="Tipo Medición :">
                                                                    </dx:ASPxLabel>
                                                                </td>
                                                                <td style="width: 25%">
                                                                    <dx:ASPxComboBox ID="cmbTipoMedicEf" runat="server" Font-Size="Smaller" style="margin-top: 0px" TextField="NOMBRE" TextFormatString="{0}" Theme="Metropolis" ValueField="ID_CATALOGO" Width="260px">
                                                                        <Columns>
                                                                            <dx:ListBoxColumn Caption="NOMBRE" FieldName="NOMBRE" Name="NOMBRE" />
                                                                            <dx:ListBoxColumn Caption="ID_CATALOGO" FieldName="ID_CATALOGO" Name="ID_CATALOGO" Width="0px" />
                                                                            <dx:ListBoxColumn Caption="CODIGO" FieldName="CODIGO" Name="CODIGO" Width="0px" />
                                                                        </Columns>
                                                                        <ClearButton Visibility="Auto">
                                                                        </ClearButton>
                                                                        <ReadOnlyStyle BackColor="#DEE8DB">
                                                                        </ReadOnlyStyle>
                                                                        <FocusedStyle BackColor="#FFFFCC">
                                                                        </FocusedStyle>
                                                                        <DisabledStyle BackColor="#DEE8DB">
                                                                        </DisabledStyle>
                                                                    </dx:ASPxComboBox>
                                                                </td>
                                                                <td style="width: 25%; text-align: right;">
                                                                    <dx:ASPxLabel ID="lblVolAguaEf" runat="server" Font-Bold="True" ForeColor="#003300" Text="Volumen de Agua :" Width="130px">
                                                                    </dx:ASPxLabel>
                                                                </td>
                                                                <td style="width: 25%">
                                                                    <dx:ASPxTextBox ID="txtVolAguaEf" runat="server" ClientInstanceName="txtId" Font-Size="Smaller" MaxLength="11" Text="0" Width="260px">
                                                                        <ClientSideEvents TextChanged="function(s, e) {
	s.SetText(s.GetText().toUpperCase());
}" />
                                                                        <MaskSettings IncludeLiterals="DecimalSymbol" Mask="&lt;0..9999999&gt;.&lt;00..99&gt;" />
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
                                                                    <dx:ASPxLabel ID="lblPlacaEf" runat="server" Font-Bold="True" ForeColor="#003300" Text="Identificador :">
                                                                    </dx:ASPxLabel>
                                                                </td>
                                                                <td style="width: 25%">
                                                                    <dx:ASPxTextBox ID="txtPlacaEf" runat="server" Font-Size="Smaller" MaxLength="11" Width="260px">
                                                                        <ClientSideEvents KeyPress="function(s, e) {
	//if (!((event.keyCode &gt;= 48 &amp;&amp; event.keyCode &lt;= 57)||(event.keyCode &gt;= 65 &amp;&amp; event.keyCode &lt;= 70)||(event.keyCode &gt;= 97 &amp;&amp; event.keyCode &lt;= 102)) ) event.returnValue = false;

//if ((event.keyCode&gt;32&amp;&amp;event.keyCode&lt;38)||(event.keyCode&gt;38&amp;&amp;event.keyCode&lt;48)) event.returnValue = false;

if (!((event.keyCode&gt;47&amp;&amp;event.keyCode&lt;58)||(event.keyCode&gt;64&amp;&amp;event.keyCode&lt;91)||(event.keyCode&gt;96&amp;&amp;event.keyCode&lt;123))) event.returnValue = false;

}" TextChanged="function(s, e) {
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
                                                                <td style="width: 25%; text-align: right;">
                                                                    <dx:ASPxLabel ID="lblSubIdentEf" runat="server" Font-Bold="True" ForeColor="#003300" Text="Sub Identificador :">
                                                                    </dx:ASPxLabel>
                                                                </td>
                                                                <td style="width: 25%">
                                                                    <dx:ASPxTextBox ID="txtSubIdentEf" runat="server" Font-Size="Smaller" MaxLength="15" Width="260px">
                                                                        <ClientSideEvents KeyPress="function(s, e) {
	if (!((event.keyCode&gt;47&amp;&amp;event.keyCode&lt;58)||(event.keyCode&gt;64&amp;&amp;event.keyCode&lt;91)||(event.keyCode&gt;96&amp;&amp;event.keyCode&lt;123))) event.returnValue = false;

}" TextChanged="function(s, e) {
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
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 25%; text-align: right;">
                                                                    <dx:ASPxLabel ID="lblBarCodeEf" runat="server" Font-Bold="True" ForeColor="#003300" Text="Código de Barras :" ClientInstanceName="lblBarCodeEf">
                                                                    </dx:ASPxLabel>
                                                                </td>
                                                                <td style="width: 25%">
                                                                    <dx:ASPxTextBox ID="txtBarCodeEf" runat="server" Font-Size="Smaller" MaxLength="11" Width="260px" ClientInstanceName="txtBarCodeEf">
                                                                        <ClientSideEvents KeyPress="function(s, e) {
	//if (!((event.keyCode &gt;= 48 &amp;&amp; event.keyCode &lt;= 57)||(event.keyCode &gt;= 65 &amp;&amp; event.keyCode &lt;= 70)||(event.keyCode &gt;= 97 &amp;&amp; event.keyCode &lt;= 102)) ) event.returnValue = false;

//if ((event.keyCode&gt;32&amp;&amp;event.keyCode&lt;38)||(event.keyCode&gt;38&amp;&amp;event.keyCode&lt;48)) event.returnValue = false;

if (!((event.keyCode&gt;47&amp;&amp;event.keyCode&lt;58)||(event.keyCode&gt;64&amp;&amp;event.keyCode&lt;91)||(event.keyCode&gt;96&amp;&amp;event.keyCode&lt;123))) event.returnValue = false;

}" TextChanged="function(s, e) {
	s.SetText(s.GetText().toUpperCase());
}" />
                                                                        <MaskSettings Mask="&lt;0..99999999999&gt;" />
                                                                        <ReadOnlyStyle BackColor="#DEE8DB">
                                                                        </ReadOnlyStyle>
                                                                        <FocusedStyle BackColor="#FFFFCC">
                                                                        </FocusedStyle>
                                                                        <DisabledStyle BackColor="#DEE8DB">
                                                                        </DisabledStyle>
                                                                    </dx:ASPxTextBox>
                                                                </td>
                                                                <td style="width: 25%; text-align: right;">&nbsp;</td>
                                                                <td style="width: 25%">&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 25%; text-align: right;">
                                                                    <dx:ASPxLabel ID="lblCodigoEf" runat="server" Font-Bold="True" ForeColor="#003300" Text="Código Tanque Cisterna :">
                                                                    </dx:ASPxLabel>
                                                                </td>
                                                                <td style="width: 25%">
                                                                    <dx:ASPxTextBox ID="txtCodigoEf" runat="server" Font-Size="Smaller" MaxLength="11" Width="260px">
                                                                        <ClientSideEvents KeyPress="function(s, e) {
if (!((event.keyCode&gt;47&amp;&amp;event.keyCode&lt;58)||(event.keyCode&gt;64&amp;&amp;event.keyCode&lt;91)||(event.keyCode&gt;96&amp;&amp;event.keyCode&lt;123))) event.returnValue = false;
}" TextChanged="function(s, e) {
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
                                                                <td style="width: 25%; text-align: right;">
                                                                    <dx:ASPxLabel ID="lblTipoEnvaseEf" runat="server" Font-Bold="True" ForeColor="#003300" Text="Tipo Envase p/Despacho :" ClientInstanceName="lblTipoEnvaseEf">
                                                                    </dx:ASPxLabel>
                                                                </td>
                                                                <td style="width: 25%">
                                                                    <dx:ASPxComboBox ID="cmbTipoEnvaseEf" runat="server" Font-Size="Smaller" TextField="NOMBRE" TextFormatString="{0}" Theme="Metropolis" ValueField="ID_TIPO_CONTENEDOR" Width="260px" ClientInstanceName="cmbTipoEnvaseEf">
                                                                        <Columns>
                                                                            <dx:ListBoxColumn Caption="NOMBRE" FieldName="NOMBRE" Name="NOMBRE" />
                                                                            <dx:ListBoxColumn Caption="ID_TIPO_CONTENEDOR" FieldName="ID_TIPO_CONTENEDOR" Name="ID_TIPO_CONTENEDOR" Width="0px" />
                                                                            <dx:ListBoxColumn Caption="CODIGO" FieldName="CODIGO" Name="CODIGO" Width="0px" />
                                                                        </Columns>
                                                                        <ClearButton Visibility="Auto">
                                                                        </ClearButton>
                                                                        <ValidationSettings>
                                                                            <RequiredField ErrorText="ERROR: Introduzca el tipo de transporte." IsRequired="True" />
                                                                        </ValidationSettings>
                                                                        <ReadOnlyStyle BackColor="#DEE8DB">
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
                                                                    <dx:ASPxLabel ID="lblTemperaturaEf" runat="server" Font-Bold="True" ForeColor="#003300" Text="Temperatura :">
                                                                    </dx:ASPxLabel>
                                                                </td>
                                                                <td style="width: 25%">
                                                                    <dx:ASPxTextBox ID="txtTemperaturaEf" runat="server" ClientInstanceName="txtCantidadEf" Text="0" Width="50%" MaxLength="7">
                                                                        <MaskSettings IncludeLiterals="None" Mask="&lt;0..9999999&gt;" />
                                                                        <ValidationSettings>
                                                                            <RequiredField ErrorText="ERROR: Introduzca la cantidad." IsRequired="True" />
                                                                        </ValidationSettings>
                                                                        <ReadOnlyStyle BackColor="#DEE8DB">
                                                                        </ReadOnlyStyle>
                                                                        <FocusedStyle BackColor="#FFFFCC">
                                                                        </FocusedStyle>
                                                                        <DisabledStyle BackColor="#DEE8DB">
                                                                        </DisabledStyle>
                                                                    </dx:ASPxTextBox>
                                                                    <dx:ASPxComboBox ID="cmbTipoTempEf" runat="server" Font-Size="Smaller" TextField="NOMBRE" TextFormatString="{0}" Theme="Metropolis" ValueField="ID_CATALOGO" Width="45%" ClientInstanceName="cmbTipoTempEf">
                                                                        <Columns>
                                                                            <dx:ListBoxColumn Caption="NOMBRE" FieldName="NOMBRE" Name="NOMBRE" />
                                                                            <dx:ListBoxColumn Caption="ID_CATALOGO" FieldName="ID_CATALOGO" Name="ID_CATALOGO" Width="0px" />
                                                                            <dx:ListBoxColumn Caption="CODIGO" FieldName="CODIGO" Name="CODIGO" Width="0px" />
                                                                        </Columns>
                                                                        <ClearButton Visibility="Auto">
                                                                        </ClearButton>
                                                                        <ReadOnlyStyle BackColor="#DEE8DB">
                                                                        </ReadOnlyStyle>
                                                                        <FocusedStyle BackColor="#FFFFCC">
                                                                        </FocusedStyle>
                                                                        <DisabledStyle BackColor="#DEE8DB">
                                                                        </DisabledStyle>
                                                                    </dx:ASPxComboBox>
                                                                </td>
                                                                <td style="width: 25%; text-align: right;">
                                                                    <dx:ASPxLabel ID="lblGravedadEf" runat="server" Font-Bold="True" ForeColor="#003300" Text="Gravedad Especifica :">
                                                                    </dx:ASPxLabel>
                                                                </td>
                                                                <td style="width: 25%">
                                                                    <dx:ASPxTextBox ID="txtGravedadEf" runat="server" Font-Size="Smaller" MaxLength="11" Width="260px">
                                                                        <ClientSideEvents KeyPress="function(s, e) {
if (!((event.keyCode&gt;47&amp;&amp;event.keyCode&lt;58)||(event.keyCode&gt;64&amp;&amp;event.keyCode&lt;91)||(event.keyCode&gt;96&amp;&amp;event.keyCode&lt;123))) event.returnValue = false;
}" TextChanged="function(s, e) {
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
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 25%; text-align: right;">
                                                                    <dx:ASPxLabel ID="lblObsEf" runat="server" Font-Bold="True" ForeColor="#003300" Text="Observaciones :" Width="150px">
                                                                    </dx:ASPxLabel>
                                                                </td>
                                                                <td colspan="3">
                                                                    <dx:ASPxTextBox ID="txtObsEf" runat="server" Font-Size="Smaller" MaxLength="15" Width="98%">
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
                                                                    <dx:ASPxLabel ID="lblTipoDocEf" runat="server" Font-Bold="True" ForeColor="#003300" Text="Tipo Documento :" Width="150px">
                                                                    </dx:ASPxLabel>
                                                                </td>
                                                                <td style="width: 25%;">
                                                                    <dx:ASPxComboBox ID="cmbTipoDocEf" runat="server" ClientInstanceName="cmbTipoDocEf" Font-Size="Smaller" TextField="NOMBRE" TextFormatString="{0};{3}" ValueField="ID_TIPO_TRANSACCION" Width="260px" Theme="Metropolis">
                                                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
var item = s.GetSelectedItem();
if(item)
{
if(item.GetColumnText('ID_TIPO_TRANSACCION_PADRE')=='1')
{
txtClienteEf.SetVisible(true);
lblClienteEf.SetVisible(true);
lblNitEf.SetVisible(true);
txtNitEf.SetVisible(true);
}
else
{
txtClienteEf.SetVisible(false);
lblClienteEf.SetVisible(false);
lblNitEf.SetVisible(false);
txtNitEf.SetVisible(false);
}
}
}" />
                                                                        <Columns>
                                                                            <dx:ListBoxColumn Caption="NOMBRE" FieldName="NOMBRE" Name="NOMBRE" />
                                                                            <dx:ListBoxColumn Caption="ID_TIPO_TRANSACCION" FieldName="ID_TIPO_TRANSACCION" Name="ID_TIPO_TRANSACCION" Width="0px" />
                                                                            <dx:ListBoxColumn Caption="CODIGO" FieldName="CODIGO" Name="CODIGO" Width="0px" />
                                                                            <dx:ListBoxColumn Caption="ID_TIPO_TRANSACCION_PADRE" FieldName="ID_TIPO_TRANSACCION_PADRE" Name="ID_TIPO_TRANSACCION_PADRE" Width="0px" />
                                                                        </Columns>
                                                                        <ClearButton Visibility="Auto">
                                                                        </ClearButton>
                                                                        <ValidationSettings>
                                                                            <RequiredField ErrorText="ERROR: Introduzca la moneda." IsRequired="True" />
                                                                        </ValidationSettings>
                                                                        <ReadOnlyStyle BackColor="#DEE8DB">
                                                                        </ReadOnlyStyle>
                                                                        <FocusedStyle BackColor="#FFFFCC">
                                                                        </FocusedStyle>
                                                                        <DisabledStyle BackColor="#DEE8DB">
                                                                        </DisabledStyle>
                                                                    </dx:ASPxComboBox>
                                                                </td>
                                                                <td style="width: 25%; text-align: left;">
                                                                    <dx:ASPxButton ID="btnAgrega" runat="server" Text="Agregar Documento" AutoPostBack="False" Theme="Metropolis" Width="160px">
                                                                        <ClientSideEvents Click="function(s, e) {
txtIdDocEf.SetValue(0);
pucDocumento.Show();
pucDocumento.PerformCallback();
}" />
                                                                        <Image IconID="reports_addgroupheader_16x16">
                                                                        </Image>
                                                                    </dx:ASPxButton>
                                                                </td>
                                                                <td style="width: 25%;">
                                                                    &nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 25%; text-align: right;">
                                                                    <dx:ASPxLabel ID="lblClienteEf" runat="server" Text="Razón Social :" Font-Bold="True" ForeColor="#003300" ClientInstanceName="lblClienteEf">
                                                                    </dx:ASPxLabel>
                                                                </td>
                                                                <td style="width: 25%" colspan="3">
                                                                    <dx:ASPxTextBox ID="txtClienteEf" runat="server" Font-Size="Smaller" Height="16px" MaxLength="90" Width="98%" ClientInstanceName="txtClienteEf">
                                                                        <ClientSideEvents TextChanged="function(s, e) {
	                                                                            s.SetText(s.GetText().toUpperCase());
                                                                            }" KeyPress="function(s, e) {
	if ((event.keyCode&gt;32&amp;&amp;event.keyCode&lt;38)||(event.keyCode&gt;38&amp;&amp;event.keyCode&lt;46)||(event.keyCode&gt;57&amp;&amp;event.keyCode&lt;64)) event.returnValue = false;
}" />
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
                                                                <td style="width: 25%; text-align: right; padding-bottom: 10px;">
                                                                    <dx:ASPxLabel ID="lblNitEf" runat="server" Font-Bold="True" ForeColor="#003300" Text="NIT :" ClientInstanceName="lblNitEf">
                                                                    </dx:ASPxLabel>
                                                                </td>
                                                                <td style="width: 25%; padding-bottom: 10px;">
                                                                    <dx:ASPxTextBox ID="txtNitEf" runat="server" Font-Size="Smaller" MaxLength="15" Text="0" Width="260px" ClientInstanceName="txtNitEf">
                                                                        <MaskSettings Mask="&lt;0..999999999999999&gt;" PromptChar=" " />
                                                                        <ReadOnlyStyle BackColor="#DEE8DB">
                                                                        </ReadOnlyStyle>
                                                                        <FocusedStyle BackColor="#FFFFCC">
                                                                        </FocusedStyle>
                                                                        <DisabledStyle BackColor="#DEE8DB">
                                                                        </DisabledStyle>
                                                                    </dx:ASPxTextBox>
                                                                </td>
                                                                <td style="width: 25%; padding-bottom: 10px; text-align: left;">&nbsp;</td>
                                                                <td style="width: 25%; padding-bottom: 10px;">&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 100%;" colspan="4" style="padding-bottom: 10px;">
                                                                    <dx:ASPxGridView ID="dgvDocumentos" runat="server" AutoGenerateColumns="False" ClientInstanceName="dgvDocumentos" EnableTheming="True" Font-Size="Smaller" KeyFieldName="ID_TRANSACCION" OnCustomButtonCallback="dgvDocumentos_CustomButtonCallback" OnCustomCallback="dgvDocumentos_CustomCallback" OnHtmlRowPrepared="dgvDocumentos_HtmlRowPrepared" Theme="Default" Width="100%">
                                                                        <ClientSideEvents CustomButtonClick="function(s, e) {
	
switch(e.buttonID) {

case&nbsp; 'btnEditar':
txtIdDocEf.SetValue(1);
pucDocumento.Show();
pucDocumento.PerformCallback();
break;

case&nbsp; 'btnQuitar':
e.processOnServer = confirm('¿Desea quitar el documento? - Una vez realizada la acción y despues de grabar el movimiento no podrá revertir el proceso. ¿Desea continuar?'); 
break;

case&nbsp; 'btnAgregar':
e.processOnServer = confirm('¿Desea agregar el documento?'); 
break;

}

}" />
                                                                        <SettingsContextMenu EnableRowMenu="False">
                                                                        </SettingsContextMenu>
                                                                        <Templates>
                                                                            <EditForm>
                                                                            </EditForm>
                                                                        </Templates>
                                                                        <SettingsPager PageSize="50" Visible="False">
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
                                                                            <dx:GridViewCommandColumn ShowInCustomizationForm="True" VisibleIndex="19">
                                                                                <CustomButtons>
                                                                                    <dx:GridViewCommandColumnCustomButton ID="btnEditar" Text="Editar">
                                                                                    </dx:GridViewCommandColumnCustomButton>
                                                                                    <dx:GridViewCommandColumnCustomButton ID="btnQuitar" Text="Quitar">
                                                                                    </dx:GridViewCommandColumnCustomButton>
                                                                                    <dx:GridViewCommandColumnCustomButton ID="btnAgregar" Text="Agregar">
                                                                                    </dx:GridViewCommandColumnCustomButton>
                                                                                </CustomButtons>
                                                                            </dx:GridViewCommandColumn>
                                                                            <dx:GridViewDataTextColumn Caption="NO. FACT./DOC." FieldName="NRO_TRANSACCION" ShowInCustomizationForm="True" VisibleIndex="14">
                                                                                <Settings AutoFilterCondition="Contains" />
                                                                            </dx:GridViewDataTextColumn>
                                                                            <dx:GridViewDataTextColumn Caption="OBSERVACIONES" FieldName="OBSERVACIONES" ShowInCustomizationForm="True" Visible="False" VisibleIndex="21">
                                                                                <Settings AutoFilterCondition="Contains" />
                                                                                <CellStyle HorizontalAlign="Right">
                                                                                </CellStyle>
                                                                            </dx:GridViewDataTextColumn>
                                                                            <dx:GridViewDataTextColumn Caption="EMISOR" FieldName="TIPO_EMISOR" ShowInCustomizationForm="True" VisibleIndex="6">
                                                                                <Settings AutoFilterCondition="Contains" />
                                                                            </dx:GridViewDataTextColumn>
                                                                            <dx:GridViewDataTextColumn Caption="TIPO DOC." FieldName="TIPO_TRANSACCION" ShowInCustomizationForm="True" VisibleIndex="4">
                                                                                <Settings AutoFilterCondition="Equals" />
                                                                            </dx:GridViewDataTextColumn>
                                                                            <dx:GridViewDataTextColumn Caption="NO. AUTORIZACION" FieldName="NRO_AUTORIZACION" ShowInCustomizationForm="True" VisibleIndex="13">
                                                                                <Settings AutoFilterCondition="Contains" />
                                                                            </dx:GridViewDataTextColumn>
                                                                            <dx:GridViewDataTextColumn Caption="PRECIO" FieldName="PRECIO_UNITARIO" ShowInCustomizationForm="True" VisibleIndex="17">
                                                                            </dx:GridViewDataTextColumn>
                                                                            <dx:GridViewDataTextColumn Caption="IMPORTE" FieldName="PRECIO_TOTAL" ShowInCustomizationForm="True" VisibleIndex="18">
                                                                            </dx:GridViewDataTextColumn>
                                                                            <dx:GridViewDataTextColumn Caption="FECHA" FieldName="FECHA_HORA" ShowInCustomizationForm="True" VisibleIndex="2">
                                                                            </dx:GridViewDataTextColumn>
                                                                            <dx:GridViewDataTextColumn Caption="CANTIDAD" FieldName="CANTIDAD" VisibleIndex="16" Visible="False">
                                                                            </dx:GridViewDataTextColumn>
                                                                            <dx:GridViewDataTextColumn Caption="ID" FieldName="ID_TRANSACCION" VisibleIndex="0">
                                                                            </dx:GridViewDataTextColumn>
                                                                            <dx:GridViewDataTextColumn Caption="USUARIO_TRANSACCION" FieldName="USUARIO_TRANSACCION" Visible="False" VisibleIndex="20">
                                                                            </dx:GridViewDataTextColumn>
                                                                            <dx:GridViewDataTextColumn Caption="COD. CTRL." FieldName="CODIGO_CONTROL" VisibleIndex="15">
                                                                            </dx:GridViewDataTextColumn>
                                                                            <dx:GridViewDataTextColumn Caption="ID_MOVIMIENTO" FieldName="ID_MOVIMIENTO" Visible="False" VisibleIndex="22">
                                                                            </dx:GridViewDataTextColumn>
                                                                            <dx:GridViewDataTextColumn Caption="ID_TIPO_MONEDA" FieldName="ID_TIPO_MONEDA" Visible="False" VisibleIndex="23">
                                                                            </dx:GridViewDataTextColumn>
                                                                            <dx:GridViewDataTextColumn Caption="ID_ESTADO_TRANSACCION" FieldName="ID_ESTADO_TRANSACCION" Visible="False" VisibleIndex="24">
                                                                            </dx:GridViewDataTextColumn>
                                                                            <dx:GridViewDataTextColumn Caption="FECHA_HORA_EMISION" FieldName="FECHA_HORA_EMISION" Visible="False" VisibleIndex="1">
                                                                            </dx:GridViewDataTextColumn>
                                                                            <dx:GridViewDataTextColumn Caption="ID_TIPO_TRANSACCION" FieldName="ID_TIPO_TRANSACCION" Visible="False" VisibleIndex="3">
                                                                            </dx:GridViewDataTextColumn>
                                                                            <dx:GridViewDataTextColumn Caption="ID_TIPO_EMISOR" FieldName="ID_TIPO_EMISOR" Visible="False" VisibleIndex="5">
                                                                            </dx:GridViewDataTextColumn>
                                                                            <dx:GridViewDataTextColumn Caption="ID_FORMA_PAGO" FieldName="ID_FORMA_PAGO" Visible="False" VisibleIndex="7">
                                                                            </dx:GridViewDataTextColumn>
                                                                            <dx:GridViewDataTextColumn Caption="FORMA PAGO " FieldName="FORMA_PAGO" VisibleIndex="8">
                                                                            </dx:GridViewDataTextColumn>
                                                                            <dx:GridViewDataTextColumn Caption="ID_TIPO_CREDITO_FISCAL" FieldName="ID_TIPO_CREDITO_FISCAL" Visible="False" VisibleIndex="9">
                                                                            </dx:GridViewDataTextColumn>
                                                                            <dx:GridViewDataTextColumn Caption="TIPO_CREDITO_FISCAL" FieldName="TIPO_CREDITO_FISCAL" VisibleIndex="10">
                                                                            </dx:GridViewDataTextColumn>
                                                                            <dx:GridViewDataTextColumn Caption="NIT_CONSUMIDOR" FieldName="NIT_CONSUMIDOR" Visible="False" VisibleIndex="11">
                                                                            </dx:GridViewDataTextColumn>
                                                                            <dx:GridViewDataTextColumn Caption="RAZON_SOCIAL" FieldName="RAZON_SOCIAL" Visible="False" VisibleIndex="12">
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
                                                                <td style="width: 25%; text-align: right; padding-top: 10px;">
                                                                    <dx:ASPxTextBox ID="txtAdicionalEf" runat="server" Width="170px" ForeColor="White" ReadOnly="True" ViewStateMode="Disabled">
                                                                        <Border BorderStyle="None" />
                                                                        <BorderBottom BorderStyle="None" />
                                                                    </dx:ASPxTextBox>
                                                                </td>
                                                                <td style="width: 25%; padding-top: 10px;">&nbsp;</td>
                                                                <td style="width: 25%; padding-top: 10px; text-align: right;">
                                                                    <dx:ASPxButton ID="btnAceptar" runat="server" AutoPostBack="False" ClientInstanceName="btnAceptar" Text="Grabar" Theme="Metropolis" Width="140px">
                                                                        <ClientSideEvents Click="function(s, e) {
	dgvTransacciones.UpdateEdit();
}" />
                                                                        <Image IconID="actions_apply_16x16">
                                                                        </Image>
                                                                    </dx:ASPxButton>
                                                                </td>
                                                                <td style="width: 25%; padding-top: 10px;">
                                                                    <dx:ASPxButton ID="btnCancelar" runat="server" AutoPostBack="False" ClientInstanceName="btnCancelar" Text="Volver" Theme="Metropolis" Width="140px">
                                                                        <ClientSideEvents Click="function(s, e) {
                                                                                dgvTransacciones.CancelEdit();
                                                                        }" />
                                                                        <Image IconID="actions_reset_16x16">
                                                                        </Image>
                                                                    </dx:ASPxButton>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4" style="text-align: right;">&nbsp;</td>
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
                                                <Settings ShowFilterRow="True" ShowGroupPanel="True" ShowHeaderFilterButton="True" ShowFooter="True" />
                                                <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" />
                                                <SettingsPopup>
                                                    <EditForm Modal="True" HorizontalAlign="Center" VerticalAlign="Middle" VerticalOffset="0" />
                                                    <CustomizationWindow HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" />
                                                </SettingsPopup>
                                                <SettingsSearchPanel Visible="True" />
                                                <SettingsText CommandNew="Nuevo" />
                                                <Columns>
                                                    <dx:GridViewCommandColumn ShowEditButton="True" ShowInCustomizationForm="True" ShowNewButtonInHeader="True" VisibleIndex="47" ToolTip="Funciones">
                                                        <CustomButtons>
                                                            <dx:GridViewCommandColumnCustomButton ID="btnAnular" Text="Anular">
                                                            </dx:GridViewCommandColumnCustomButton>
                                                            <dx:GridViewCommandColumnCustomButton ID="btnActivar" Text="Activar">
                                                            </dx:GridViewCommandColumnCustomButton>
                                                            <dx:GridViewCommandColumnCustomButton ID="btnEliminar" Text="Eliminar">
                                                            </dx:GridViewCommandColumnCustomButton>
                                                        </CustomButtons>
                                                        <CellStyle ForeColor="#006600">
                                                        </CellStyle>
                                                    </dx:GridViewCommandColumn>
                                                    <dx:GridViewDataTextColumn Caption="FECHA_OPERACION" FieldName="FECHA_OPERACION" ShowInCustomizationForm="True" VisibleIndex="5" ToolTip="Fecha del Movimiento" Visible="False">
                                                        <Settings AutoFilterCondition="Contains" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="ORIGEN REG." FieldName="COD_FUENTE_ORIGEN" ShowInCustomizationForm="True" VisibleIndex="8" ToolTip="Origen del Registro">
                                                        <Settings AutoFilterCondition="Contains" />
                                                        <CellStyle HorizontalAlign="Center">
                                                        </CellStyle>
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="T.MOV." FieldName="TIPO_MOVIMIENTO" ShowInCustomizationForm="True" VisibleIndex="9" ToolTip="Tipo de Movimiento">
                                                        <Settings AutoFilterCondition="Contains" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="NIT" FieldName="NIT_CONSUMIDOR" ShowInCustomizationForm="True" VisibleIndex="33">
                                                        <Settings AutoFilterCondition="Contains" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="RAZON SOCIAL" FieldName="RAZON_SOCIAL" ShowInCustomizationForm="True" VisibleIndex="34">
                                                        <Settings AutoFilterCondition="Contains" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="PLACA" FieldName="IDENTIFICADOR" ShowInCustomizationForm="True" VisibleIndex="21">
                                                        <Settings AutoFilterCondition="Contains" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="OBSERVACION_MOVIMIENTO" FieldName="OBSERVACION_MOVIMIENTO" ShowInCustomizationForm="True" VisibleIndex="48" Visible="False">
                                                        <Settings AutoFilterCondition="Contains" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="TIPO TRANSACCION" FieldName="TIPO_TRANSACCION" ShowInCustomizationForm="True" VisibleIndex="28">
                                                        <Settings AutoFilterCondition="Contains" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="NO. FACT./DOC." FieldName="NRO_TRANSACCION" ShowInCustomizationForm="True" VisibleIndex="36" ToolTip="Número de Factura o Documento">
                                                        <Settings AutoFilterCondition="Contains" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="NO. AUTORIZACION" FieldName="NRO_AUTORIZACION" ShowInCustomizationForm="True" VisibleIndex="35" ToolTip="Número de Autorización">
                                                        <Settings AutoFilterCondition="Contains" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="USUARIO" FieldName="APP_USUARIO" ShowInCustomizationForm="True" VisibleIndex="22" ToolTip="Entidad de Referencia">
                                                        <Settings AutoFilterCondition="Contains" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="ID_ESTADO_MOVIMIENTO" FieldName="ID_ESTADO_MOVIMIENTO" ShowInCustomizationForm="True" VisibleIndex="11" Visible="False">
                                                        <Settings AutoFilterCondition="Contains" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="ID_HIDROCARBURO" FieldName="ID_HIDROCARBURO" ShowInCustomizationForm="True" VisibleIndex="27" Visible="False">
                                                        <Settings AutoFilterCondition="Contains" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="PRECIO" FieldName="PRECIO_UNITARIO" ShowInCustomizationForm="True" VisibleIndex="39">
                                                        <Settings AutoFilterCondition="Contains" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="CANTIDAD" FieldName="CANTIDAD" ShowInCustomizationForm="True" VisibleIndex="38">
                                                        <Settings AutoFilterCondition="Equals" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="IMPORTE" FieldName="PRECIO_TOTAL" ShowInCustomizationForm="True" VisibleIndex="40">
                                                        <Settings AutoFilterCondition="Equals" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn ShowInCustomizationForm="True" VisibleIndex="0" Caption="ID" FieldName="ID_MOVIMIENTO" ToolTip="Identificador del Registro del Movimiento">
                                                        <Settings AutoFilterCondition="Contains" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="ID_CONSUMIDOR" FieldName="ID_CONSUMIDOR" ShowInCustomizationForm="True" VisibleIndex="2" Visible="False">
                                                        <Settings AutoFilterCondition="Equals" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="ID_INFRAESTRUCTURA" FieldName="ID_INFRAESTRUCTURA" ShowInCustomizationForm="True" VisibleIndex="25" Visible="False">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="ID_TIPO_TRANSACCION" FieldName="ID_TIPO_TRANSACCION" ShowInCustomizationForm="True" Visible="False" VisibleIndex="32">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="ID_UNIDAD_ENVASES" FieldName="ID_UNIDAD_ENVASES" ShowInCustomizationForm="True" Visible="False" VisibleIndex="45">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="GRADO_API" FieldName="GRADO_API" ShowInCustomizationForm="True" Visible="False" VisibleIndex="43">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="TEMPERATURA" FieldName="TEMPERATURA" ShowInCustomizationForm="True" Visible="False" VisibleIndex="41">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="ID_TIPO_FLUJO" FieldName="ID_TIPO_FLUJO" ShowInCustomizationForm="True" Visible="False" VisibleIndex="13">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="ID_TIPO_OPERACION" FieldName="ID_TIPO_OPERACION" ShowInCustomizationForm="True" Visible="False" VisibleIndex="4">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="ID_TIPO_MEDIO_TRANSPORTE" FieldName="ID_TIPO_MEDIO_TRANSPORTE" ShowInCustomizationForm="True" Visible="False" VisibleIndex="19">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="ID_MEDIO_TRANSPORTE" FieldName="ID_MEDIO_TRANSPORTE" ShowInCustomizationForm="True" Visible="False" VisibleIndex="24">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="ID_TIPO_MEDICION" FieldName="ID_TIPO_MEDICION" ShowInCustomizationForm="True" Visible="False" VisibleIndex="18">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="ID_UNIDAD_TEMPERATURA" FieldName="ID_UNIDAD_TEMPERATURA" ShowInCustomizationForm="True" Visible="False" VisibleIndex="42">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="CANTIDAD_ENVASES" FieldName="CANTIDAD_ENVASES" ShowInCustomizationForm="True" Visible="False" VisibleIndex="44">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="APP_FECHA_REGISTRO" FieldName="APP_FECHA_REGISTRO" ShowInCustomizationForm="True" Visible="False" VisibleIndex="50">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="ID_ENTIDAD" FieldName="ID_ENTIDAD" ShowInCustomizationForm="True" Visible="False" VisibleIndex="17">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="ID_TIPO_MOVIMIENTO" FieldName="ID_TIPO_MOVIMIENTO" ShowInCustomizationForm="True" Visible="False" VisibleIndex="14">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="SUB_IDENTIFICADOR" FieldName="SUB_IDENTIFICADOR" ShowInCustomizationForm="True" Visible="False" VisibleIndex="31">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="PAIS" FieldName="UBICACION_OPERACION" ShowInCustomizationForm="True" VisibleIndex="10">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="EST." FieldName="ESTADO_MOVIMIENTO" ShowInCustomizationForm="True" VisibleIndex="12" ToolTip="Estado del Movimiento">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="PRODUCTO" FieldName="COD_HIDROCARBURO" ShowInCustomizationForm="True" VisibleIndex="37">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="T.FLUJO" FieldName="TIPO_FLUJO" ShowInCustomizationForm="True" VisibleIndex="7" ToolTip="Tipo de Flujo">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="ID_MOVIMIENTO_PADRE" FieldName="ID_MOVIMIENTO_PADRE" ShowInCustomizationForm="True" Visible="False" VisibleIndex="1">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="BINARIO_CONTROL" FieldName="BINARIO_CONTROL" ShowInCustomizationForm="True" Visible="False" VisibleIndex="49">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="ID_FUENTE_ORIGEN" FieldName="ID_FUENTE_ORIGEN" ShowInCustomizationForm="True" Visible="False" VisibleIndex="15">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="FECHA_OPERACION_DIA" FieldName="FECHA_OPERACION_DIA" ShowInCustomizationForm="True" Visible="False" VisibleIndex="51">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="ID_UBICACION_OPERACION" FieldName="ID_UBICACION_OPERACION" ShowInCustomizationForm="True" Visible="False" VisibleIndex="26">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="ID_UNIDAD_HIDROCARBURO" FieldName="ID_UNIDAD_HIDROCARBURO" ShowInCustomizationForm="True" Visible="False" VisibleIndex="30">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="DES_FUENTE_ORIGEN" FieldName="DES_FUENTE_ORIGEN" ShowInCustomizationForm="True" Visible="False" VisibleIndex="16">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="TIPO_MEDIO_TRANSAPORTE" FieldName="TIPO_MEDIO_TRANSAPORTE" ShowInCustomizationForm="True" Visible="False" VisibleIndex="20">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="DES_HIDROCARBURO " FieldName="DES_HIDROCARBURO " ShowInCustomizationForm="True" Visible="False" VisibleIndex="29">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="ID_TIPO_ACTIVIDAD" FieldName="ID_TIPO_ACTIVIDAD" ShowInCustomizationForm="True" Visible="False" VisibleIndex="3">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="CANTIDAD_ADICIONAL" FieldName="CANTIDAD_ADICIONAL" ShowInCustomizationForm="True" Visible="False" VisibleIndex="46">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="EST.D.J." FieldName="STR_ESTADO_DEC" ShowInCustomizationForm="True" VisibleIndex="23" ToolTip="Estado de Declaracion Jurada">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="FECHA" FieldName="STR_FECHA" ShowInCustomizationForm="True" VisibleIndex="6">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="CODIGO" FieldName="CODIGO" ShowInCustomizationForm="True" Visible="False" VisibleIndex="52">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="ID_INTEGRACION" FieldName="ID_INTEGRACION" ShowInCustomizationForm="True" Visible="False" VisibleIndex="53">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="BARCODE" FieldName="BARCODE" ShowInCustomizationForm="True" Visible="False" VisibleIndex="54">
                                                    </dx:GridViewDataTextColumn>
                                                </Columns>
                                                <Styles CssPostfix="DevGlp"><AlternatingRow BackColor="#EEEEEE">
                                                        </AlternatingRow></Styles>
                                                <ClientSideEvents CustomButtonClick="function (s, e) {
switch(e.buttonID) {
&nbsp;&nbsp;&nbsp; case&nbsp; 'btnAnular':
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; e.processOnServer = confirm('¿Desea anular el registro?'); 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; break;
&nbsp;&nbsp;&nbsp; case&nbsp; 'btnActivar':
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; e.processOnServer = confirm('¿Desea activar el registro?'); 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; break;
    case&nbsp; 'btnEliminar':
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; e.processOnServer = confirm('¿Desea eliminar el registro?'); 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; break;
} 
}" />

                                                <TotalSummary>
                                                    <dx:ASPxSummaryItem FieldName="PRECIO_TOTAL" ShowInColumn="IMPORTE" SummaryType="Custom" Tag="imp" />
                                                    <dx:ASPxSummaryItem FieldName="CANTIDAD" ShowInColumn="CANTIDAD" SummaryType="Custom" Tag="vol" />
                                                    <dx:ASPxSummaryItem FieldName="ESTADO_MOVIMIENTO" ShowInColumn="EST." SummaryType="Custom" Tag="est" />
                                                </TotalSummary>
                                            </dx:ASPxGridView>
                                        </td>
                                        
                                    </tr>
                                    <tr>
                                        <td class="auto-style11">
                                            <dx:ASPxButton ID="btnExportar" runat="server" Font-Bold="True" ForeColor="#003300" OnClick="btnExportar_Click" Text="..Exportar Excel" Theme="Metropolis">
                                                <ClientSideEvents Click="function(s, e) {
	e.processOnServer = confirm('¿Desea exportar los registros observados?'); 
}" />
                                                <Image IconID="export_exporttoxls_16x16">
                                                </Image>
                                            </dx:ASPxButton>
                                        </td>
                                        <td class="auto-style10">
                                            <dx:ASPxGridViewExporter ID="dgeExportar" runat="server">
                                            </dx:ASPxGridViewExporter>
                                        </td>
                                        <td class="auto-style10"></td>
                                        <td class="auto-style12"></td>
                                        <td class="auto-style10">
                                            &nbsp;</td>
                                        <td class="auto-style10">&nbsp;</td>
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

<dx:ASPxPopupControl ID="pucDatos" runat="server" AllowDragging="True" ClientInstanceName="pucDatos" EnableTheming="True" HeaderText="Identificación de Llave B-Sisa" Height="93px" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Theme="Metropolis" Width="334px">
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
                        <dx:ASPxLabel ID="ASPxLabel39" runat="server" Font-Bold="True" ForeColor="#003300" Text="Llave B-Sisa :">
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
                        <dx:ASPxButton ID="btnSubirLlave" runat="server" EnableTheming="True" OnClick="btnSubirLlave_Click" Text="Validar Llave B-Sisa" Theme="Metropolis" Width="100%">
                            <Image IconID="actions_apply_16x16">
                            </Image>
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>









<dx:ASPxPopupControl ID="pucDocumento" runat="server" AllowDragging="True" ClientInstanceName="pucDocumento" EnableTheming="True" HeaderText="Documentos" Height="93px" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Theme="Metropolis" Styles-Header-BackColor="#317D40" Width="350px" OnWindowCallback="pucDocumento_WindowCallback">
    <HeaderImage IconID="actions_insert_16x16">
    </HeaderImage>
    <ContentCollection>
        <dx:PopupControlContentControl runat="server">
            <table class="dxflInternalEditorTable_Youthful">
                <tr>
                    <td style="width: 25%; text-align: right;">
                        <dx:ASPxLabel ID="ASPxLabel40" runat="server" Font-Bold="True" ForeColor="#003300" Text="Fecha :">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 25%;">
                        <dx:ASPxDateEdit ID="detFechaDocEf" runat="server" EditFormat="DateTime" Font-Bold="False" Font-Size="Smaller" Theme="Metropolis">
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
                        <dx:ASPxLabel ID="lblIdDocEf" runat="server" ClientInstanceName="lblIdDocEf" Font-Bold="True" ForeColor="#003300" Height="16px" Text="id" Width="115px">
                        </dx:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td style="width: 25%; text-align: right;">
                        <dx:ASPxLabel ID="lblTipoEmisorEf" runat="server" Font-Bold="True" ForeColor="#003300" Height="16px" Text="Entidad Emisora :" Width="110px">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 25%;">
                        <dx:ASPxComboBox ID="cmbTipoEmisorEf" runat="server" ClientInstanceName="cmbTipoCredEf" Font-Size="Smaller" TextField="DESCRIPCION" TextFormatString="{0}" ValueField="ID_PARAMETRICA_GLOBAL" Theme="Metropolis">
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
                            <ReadOnlyStyle BackColor="#DEE8DB">
                            </ReadOnlyStyle>
                            <FocusedStyle BackColor="#FFFFCC">
                            </FocusedStyle>
                            <DisabledStyle BackColor="#DEE8DB">
                            </DisabledStyle>
                        </dx:ASPxComboBox>
                    </td>
                    <td style="width: 25%; text-align: right;">
                        <dx:ASPxLabel ID="lblTipoCredEf" runat="server" Font-Bold="True" ForeColor="#003300" Height="16px" Text="Válido Cred. Fiscal :" Width="120px">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 25%; text-align: right;">
                        <dx:ASPxComboBox ID="cmbTipoCredEf" runat="server" ClientInstanceName="cmbTipoCredEf" Font-Size="Smaller" TextField="DESCRIPCION" TextFormatString="{0}" ValueField="ID_PARAMETRICA_GLOBAL" Theme="Metropolis">
                            <Columns>
                                <dx:ListBoxColumn Caption="DESCRIPCION" FieldName="DESCRIPCION" Name="DESCRIPCION" />
                                <dx:ListBoxColumn Caption="ID_PARAMETRICA_GLOBAL" FieldName="ID_PARAMETRICA_GLOBAL" Name="ID_PARAMETRICA_GLOBAL" Width="0px" />
                                <dx:ListBoxColumn Caption="CODIGO" FieldName="CODIGO" Name="CODIGO" Width="0px" />
                            </Columns>
                            <ClearButton Visibility="Auto">
                            </ClearButton>
                            <ReadOnlyStyle BackColor="#DEE8DB">
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
                        <dx:ASPxLabel ID="lblMonedaEf" runat="server" Font-Bold="True" ForeColor="#003300" Text="Moneda :">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 25%;">
                        <dx:ASPxComboBox ID="cmbMonedaEf" runat="server" Font-Size="Smaller" ReadOnly="True" TextField="NOMBRE" TextFormatString="{0}" ValueField="ID_CATALOGO" Theme="Metropolis">
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
                            <ReadOnlyStyle BackColor="#DEE8DB">
                            </ReadOnlyStyle>
                            <FocusedStyle BackColor="#FFFFCC">
                            </FocusedStyle>
                            <DisabledStyle BackColor="#DEE8DB">
                            </DisabledStyle>
                        </dx:ASPxComboBox>
                    </td>
                    <td style="width: 25%; text-align: right;">
                        <dx:ASPxLabel ID="lblTipoConsEf" runat="server" Font-Bold="True" ForeColor="#003300" Text="Forma de Pago :" Width="115px">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 25%; text-align: right;">
                        <dx:ASPxComboBox ID="cmbTipoPagoEf" runat="server" ClientInstanceName="cmbTipoPagoEf" Font-Size="Smaller" TextField="DESCRIPCION" TextFormatString="{0}" ValueField="ID_PARAMETRICA_GLOBAL" Theme="Metropolis">
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
                            <ReadOnlyStyle BackColor="#DEE8DB">
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
                        <dx:ASPxLabel ID="lblUsuarioEf" runat="server" Font-Bold="True" ForeColor="#003300" Text="Usuario :">
                        </dx:ASPxLabel>
                    </td>
                    <td style="padding-bottom: 10px; width: 25%;">
                        <dx:ASPxTextBox ID="txtUsuarioEf" runat="server" ClientInstanceName="txtId" Font-Size="Smaller" MaxLength="100">
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
                        <dx:ASPxLabel ID="lblNoAutoEf" runat="server" Font-Bold="True" ForeColor="#003300" Text="No. Autorización :" Width="100%" ClientInstanceName="lblNoAutoEf">
                        </dx:ASPxLabel>
                        <dx:ASPxLabel ID="lblNoDocRefEf" runat="server" ClientInstanceName="lblNoDocRefEf" Font-Bold="True" ForeColor="#003300" Text="No. Doc. Referencia :" Width="130px">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 25%;">
                        <dx:ASPxTextBox ID="txtNoDocRefEf" runat="server" ClientInstanceName="txtNoDocRefEf" Font-Size="Smaller" MaxLength="15" Width="170px">
                            <ReadOnlyStyle BackColor="#DEE8DB">
                            </ReadOnlyStyle>
                            <FocusedStyle BackColor="#FFFFCC">
                            </FocusedStyle>
                            <DisabledStyle BackColor="#DEE8DB">
                            </DisabledStyle>
                        </dx:ASPxTextBox>
                        <dx:ASPxTextBox ID="txtNoAutoEf" runat="server" ClientInstanceName="txtNoAutoEf" Font-Size="Smaller" MaxLength="15" Width="170px">
                            <ReadOnlyStyle BackColor="#DEE8DB">
                            </ReadOnlyStyle>
                            <FocusedStyle BackColor="#FFFFCC">
                            </FocusedStyle>
                            <DisabledStyle BackColor="#DEE8DB">
                            </DisabledStyle>
                        </dx:ASPxTextBox>
                    </td>
                    <td style="width: 25%; text-align: right;">
                        <dx:ASPxLabel ID="lblNoFactEf" runat="server" Font-Bold="True" ForeColor="#003300" Text="No. Factura :" ClientInstanceName="lblNoFactEf">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 25%">
                        <dx:ASPxTextBox ID="txtNoFactEf" runat="server" Font-Size="Smaller" MaxLength="15" Text="0" Width="170px" ClientInstanceName="txtNoFactEf">
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
                        <dx:ASPxLabel ID="lblCodCtrlEf" runat="server" ClientInstanceName="lblCodCtrlEf" Font-Bold="True" ForeColor="#003300" Text="Cod. Control :">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 25%;">
                        <dx:ASPxTextBox ID="txtCodCtrlEf" runat="server" ClientInstanceName="txtCodCtrlEf" Font-Size="Smaller" MaxLength="14" Width="170px">
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
                        <dx:ASPxLabel ID="lblResponsableEf" runat="server" Font-Bold="True" ForeColor="#003300" Text="Responsable :">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 25%;" style="padding-bottom: 10px;">
                        <dx:ASPxTextBox ID="txtResponsableEf" runat="server" ClientInstanceName="txtResponsableEf" Font-Size="Smaller" Height="16px" MaxLength="90">
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
                        <dx:ASPxLabel ID="lblCiEf" runat="server" Font-Bold="True" ForeColor="#003300" Text="C.I. :">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 25%; padding-bottom: 10px;">
                        <dx:ASPxTextBox ID="txtCiEf" runat="server" ClientInstanceName="txtCiEf" Font-Size="Smaller" MaxLength="15" Text="0" Width="170px">
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
                        <dx:ASPxLabel ID="lblPrecioEf" runat="server" Font-Bold="True" ForeColor="#003300" Text="Precio :" ClientInstanceName="lblPrecioEf">
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
                        <dx:ASPxLabel ID="lblCantidadDocEf" runat="server" Font-Bold="True" ForeColor="#003300" Text="Cantidad :" ClientInstanceName="lblCantidadDocEf">
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
                            <ReadOnlyStyle BackColor="#DEE8DB">
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
                        <dx:ASPxLabel ID="lblImporteEf" runat="server" Font-Bold="True" ForeColor="#003300" Text="Importe :" ClientInstanceName="lblImporteEf">
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
                        <dx:ASPxLabel ID="lblObsDocEf" runat="server" Font-Bold="True" ForeColor="#003300" Text="Observaciones :" Width="150px">
                        </dx:ASPxLabel>
                    </td>
                    <td colspan="3" style="padding-bottom: 10px;">
                        <dx:ASPxTextBox ID="txtObsDocEf" runat="server" Font-Size="Smaller" MaxLength="15" Width="100%" ClientInstanceName="txtObsDocEf">
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
                        <dx:ASPxButton ID="btnAgregaDoc" runat="server" AutoPostBack="False" Text="Agregar Documento" Theme="Metropolis" EnableTheming="True" Width="100%">
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
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>









