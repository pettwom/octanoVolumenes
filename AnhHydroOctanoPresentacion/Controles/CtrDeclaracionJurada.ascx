<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtrDeclaracionJurada.ascx.cs" Inherits="AnhHydroOctanoPresentacion.Controles.CtrDeclaracionJurada" %>
<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxPivotGrid.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPivotGrid" tagprefix="dx" %>

<style type="text/css">
    .auto-style1 {
        height: 18px;
    }
    .auto-style4 {
        height: 18px;
        width: 168px;
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
    .auto-style11 {
        width: 168px;
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
    
    .auto-style13 {
        height: 37px;
    }
    
    .auto-style14 {
        width: 81px;
    }
    .auto-style15 {
        height: 18px;
        width: 81px;
    }
    
    </style>


<div class="col-md-12">
    <table style="width: 100%">
        <tr>
            <td style="text-align: center; padding-bottom: 1px">
                <dx:ASPxRoundPanel runat="server" ShowHeader="False" Width="100%" ID="pnlTitulo">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <dx:ASPxLabel runat="server" ForeColor="#367B2F" ID="lblTitulo" Font-Bold="True" Font-Names="Rockwell" Font-Size="Larger" Text="EMISIÓN DE DECLARACIÓN JURADA"></dx:ASPxLabel>
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
                                <td style="padding-bottom: 5px">
                                    <dx:ASPxCallbackPanel ID="pnlCabecera" runat="server" Width="160px" ClientInstanceName="pnlCabecera" OnCallback="pnlCabecera_Callback">
                                        <PanelCollection>
                                            <dx:PanelContent runat="server">
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxComboBox ID="cmbResMin" ClientInstanceName="cmbResMin" runat="server" ClientVisible="false" SelectedIndex="1" Theme="Metropolis" Width="100%">
                                                                <Items>
                                                                    <dx:ListEditItem Text="Resolución Ministerial 048-17" Value="1" />
                                                                    <dx:ListEditItem Selected="True" Text="Resolución Ministerial 107-19" Value="2" />
                                                                </Items>
                                                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
                                                                                pnlCargaDui.PerformCallback();
                                                                                }" />
                                                                <ClearButton Visibility="Auto">
                                                                </ClearButton>
                                                            </dx:ASPxComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxCallbackPanel ID="pnlCargaDui" runat="server" Width="100%" ClientInstanceName="pnlCargaDui" OnCallback="pnlCargaDui_Callback">
                                                                <PanelCollection>
                                                                    <dx:PanelContent ID="PanelContent1" runat="server">
                                                                        <dx:ASPxComboBox ID="cmbListadoDui" ClientInstanceName="cmbListadoDui" runat="server" Theme="Metropolis" Width="100%" ClientVisible="false" TextField="NRO_DUI" ValueField="DUI">                                                                            
                                                                        </dx:ASPxComboBox>
                                                                    </dx:PanelContent>
                                                                </PanelCollection>
                                                            </dx:ASPxCallbackPanel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>                                                        
                                                            <dx:ASPxLabel ID="ASPxLabel339" runat="server" ForeColor="#003300" Text="Periodo :">
                                                            </dx:ASPxLabel>
                                                            <dx:ASPxComboBox ID="cmbPeriodo" runat="server" ClientInstanceName="cmbPeriodo" EnableTheming="True" Font-Bold="True" Theme="Metropolis" ValueType="System.Int32" Width="160px" ToolTip="Seleccione un periodo">
                                                                <ClientSideEvents CloseUp="function(s, e) {
	pnlDatos.PerformCallback();
}" />
                                                                <ClearButton Visibility="Auto">
                                                                </ClearButton>
                                                                <CaptionStyle Font-Bold="False">
                                                                </CaptionStyle>
                                                                <DisabledStyle Font-Bold="False">
                                                                </DisabledStyle>
                                                            </dx:ASPxComboBox>
                                                            <dx:ASPxDateEdit ID="detFechaIni" runat="server" ClientInstanceName="detFechaIni" Theme="Metropolis" ToolTip="Seleccione la fecha inicial" Width="140px" Visible="false">
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
                                                        <td style="padding-bottom: 5px">
                                                            <dx:ASPxButton ID="btnConsultar" runat="server" AutoPostBack="False" Font-Bold="True" ForeColor="Maroon" Text="Obtener Información" Theme="Metropolis" Width="55%" Visible="false">
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
                                    <dx:ASPxTreeList ID="tlsOperadores" runat="server" AutoGenerateColumns="False" ClientInstanceName="tlsOperadores" Font-Size="Smaller" Height="187px" KeyFieldName="intId" ParentFieldName="intIdPadre" Theme="Youthful" Styles-Header-BackColor="#317D40" ToolTip="Seleccione un Proyecto" OnCustomCallback="tlsOperadores_CustomCallback">
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
cmbPeriodo.SetText('');
pnlCabecera.PerformCallback();
pnlDatos.PerformCallback();
}" Init="function(s, e) {
    pnlCabecera.PerformCallback();
}" NodeDblClick="function(s, e) {
	pnlCabecera.PerformCallback();
}" EndCallback="function(s, e) {
	pnlCabecera.PerformCallback();
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
                                            <td colspan="7" style="text-align: center">
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
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                <asp:Label ID="Label3" runat="server" Font-Bold="True" ForeColor="#10420F" Text="NIT"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label5" runat="server" Font-Bold="True" ForeColor="#10420F" Text="DEPARTAMENTO"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label4" runat="server" Font-Bold="True" ForeColor="#10420F" Text="DIRECCION"></asp:Label>
                                            </td>
                                            <td style="text-align: right">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" style="padding-bottom: 1px; width: 40%">
                                                <asp:Label ID="lblRazonSocial" runat="server"></asp:Label>
                                            </td>
                                            <td style="width: 10%">
                                                <asp:Label ID="lblNit" runat="server"></asp:Label>
                                            </td>
                                            <td style="padding-bottom: 1px; width: 10%">
                                                <asp:Label ID="lblDepto" runat="server"></asp:Label>
                                            </td>
                                            <td colspan="2" style="padding-bottom: 1px; width: 40%">
                                                <asp:Label ID="lblDireccion" runat="server"></asp:Label>
                                            </td>

                                        </tr>
                                        
                                        <tr>
                                            <td colspan="6">
                                                
                                                <dx:ASPxTabControl ID="tacMovimientos" runat="server" ActiveTabIndex="3" ClientInstanceName="tacMovimientos" EnableTheming="True" Theme="Metropolis" Width="100%">
                                                    <Tabs>
                                                        <dx:Tab Name="tcpResumen" Text="Mov. Acumulado">
                                                        </dx:Tab>
                                                        <dx:Tab Name="tcpEspecial" Text="Mov. Internacional" ClientVisible="False">
                                                        </dx:Tab>
                                                        <dx:Tab Name="tcpContenedor" Text="Mov. Fuera de Ala" ClientVisible="False">
                                                        </dx:Tab>
                                                        <dx:Tab Name="tcpRecaudo" Text="Recaudo Externo" ClientVisible="False">
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
&nbsp;&nbsp;&nbsp; case&nbsp;3:
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; dgvRecaudo.SetVisible(true);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; break;
&nbsp;&nbsp;&nbsp; default:
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;pvgAcumulado.SetVisible(true);
} 

}" />
                                                </dx:ASPxTabControl>
                                                
                                            </td>
                                            <td style="text-align: right">
                                                
                                                <dx:ASPxButton ID="btnDeclaracion" runat="server" AutoPostBack="False" Font-Bold="True" ForeColor="Maroon" Text="EMITIR DECLARACION JURADA" Theme="Metropolis" Width="280px">
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
                                            <td colspan="7">
                                                <dx:ASPxPivotGrid ID="pvgAcumulado" runat="server" ClientIDMode="AutoID" ClientInstanceName="pvgAcumulado" EnableTheming="True" Font-Size="Smaller" Theme="Default" Width="100%" OnCustomCellStyle="pvgAcumulado_CustomCellStyle">
                                                    <Fields>
                                                        <dx:PivotGridField ID="fieldStrProducto" Area="RowArea" AreaIndex="0" Caption="PRODUCTO" FieldName="StrProducto">
                                                            <HeaderStyle Wrap="True" />
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldStrCodProducto" Area="RowArea" AreaIndex="1" Caption="PRODUCTO" FieldName="StrCodProducto">
                                                            <HeaderStyle Wrap="True" />
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldStrUnidadMedida" Area="RowArea" AreaIndex="2" Caption="UNIDAD MEDIDA" FieldName="StrUnidadMedida">
                                                            <HeaderStyle Wrap="True" />
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldStrTipoInfraestructura" Area="RowArea" AreaIndex="3" Caption="TIPO DE INFRAESTRUCTURA" FieldName="StrTipoInfraestructura">
                                                            <HeaderStyle Wrap="True" />
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldStrInfraestructura" Area="RowArea" AreaIndex="4" Caption="INFRAESTRUCTURA" FieldName="StrInfraestructura">
                                                            <HeaderStyle Wrap="True" />
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldStrFecha" Area="RowArea" AreaIndex="5" Caption="FECHA" FieldName="StrFecha">
                                                            <HeaderStyle Wrap="True" />
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecSaldoInicial" Area="DataArea" AreaIndex="0" Caption="SALDO INICIAL" FieldName="DecSaldoInicial" CellFormat-FormatType="Custom" Tag="salIni" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecIng01" Area="DataArea" AreaIndex="0" Caption="DecIng01" FieldName="DecIng01" CellFormat-FormatType="Custom" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecIng02" Area="DataArea" AreaIndex="0" Caption="DecIng02" FieldName="DecIng02" CellFormat-FormatType="Custom" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecIng03" Area="DataArea" AreaIndex="0" Caption="DecIng03" FieldName="DecIng03" CellFormat-FormatType="Custom" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecIng04" Area="DataArea" AreaIndex="0" Caption="DecIng04" FieldName="DecIng04" CellFormat-FormatType="Custom" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecIng05" Area="DataArea" AreaIndex="0" Caption="DecIng05" FieldName="DecIng05" CellFormat-FormatType="Custom" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecIng06" Area="DataArea" AreaIndex="0" Caption="DecIng06" FieldName="DecIng06" CellFormat-FormatType="Custom" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecIng07" Area="DataArea" AreaIndex="0" Caption="DecIng07" FieldName="DecIng07" CellFormat-FormatType="Custom" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecIng08" Area="DataArea" AreaIndex="0" Caption="DecIng08" FieldName="DecIng08" CellFormat-FormatType="Custom" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecIng09" Area="DataArea" AreaIndex="0" Caption="DecIng09" FieldName="DecIng09" CellFormat-FormatType="Custom" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecIng10" Area="DataArea" AreaIndex="0" Caption="DecIng10" FieldName="DecIng10" CellFormat-FormatType="Custom" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecIng11" Area="DataArea" AreaIndex="0" Caption="DecIng11" FieldName="DecIng11" CellFormat-FormatType="Custom" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecIng12" Area="DataArea" AreaIndex="0" Caption="DecIng12" FieldName="DecIng12" CellFormat-FormatType="Custom" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecIng13" Area="DataArea" AreaIndex="0" Caption="DecIng13" FieldName="DecIng13" CellFormat-FormatType="Custom" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecIng14" Area="DataArea" AreaIndex="0" Caption="DecIng14" FieldName="DecIng14" CellFormat-FormatType="Custom" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecIng15" Area="DataArea" AreaIndex="0" Caption="DecIng15" FieldName="DecIng15" CellFormat-FormatType="Custom" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecIng16" Area="DataArea" AreaIndex="0" Caption="DecIng16" FieldName="DecIng16" CellFormat-FormatType="Custom" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecIng17" Area="DataArea" AreaIndex="0" Caption="DecIng17" FieldName="DecIng17" CellFormat-FormatType="Custom" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecIng18" Area="DataArea" AreaIndex="0" Caption="DecIng18" FieldName="DecIng18" CellFormat-FormatType="Custom" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecIng19" Area="DataArea" AreaIndex="0" Caption="DecIng19" FieldName="DecIng19" CellFormat-FormatType="Custom" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecIng20" Area="DataArea" AreaIndex="0" Caption="DecIng20" FieldName="DecIng20" CellFormat-FormatType="Custom" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecTotalIng" Area="DataArea" AreaIndex="0" Caption="TOTAL INGRESOS" FieldName="DecTotalIng" CellFormat-FormatType="Custom" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecSal01" Area="DataArea" AreaIndex="0" Caption="DecSal01" FieldName="DecSal01" CellFormat-FormatType="Custom" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecSal02" Area="DataArea" AreaIndex="0" Caption="DecSal02" FieldName="DecSal02" CellFormat-FormatType="Custom" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecSal03" Area="DataArea" AreaIndex="0" Caption="DecSal03" FieldName="DecSal03" CellFormat-FormatType="Custom" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecSal04" Area="DataArea" AreaIndex="0" Caption="DecSal04" FieldName="DecSal04" CellFormat-FormatType="Custom" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecSal05" Area="DataArea" AreaIndex="0" Caption="DecSal05" FieldName="DecSal05" CellFormat-FormatType="Custom" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecSal06" Area="DataArea" AreaIndex="0" Caption="DecSal06" FieldName="DecSal06" CellFormat-FormatType="Custom" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecSal07" Area="DataArea" AreaIndex="0" Caption="DecSal07" FieldName="DecSal07" CellFormat-FormatType="Custom" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecSal08" Area="DataArea" AreaIndex="0" Caption="DecSal08" FieldName="DecSal08" CellFormat-FormatType="Custom" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecSal09" Area="DataArea" AreaIndex="0" Caption="DecSal09" FieldName="DecSal09" CellFormat-FormatType="Custom" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecSal10" Area="DataArea" AreaIndex="0" Caption="DecSal10" FieldName="DecSal10" CellFormat-FormatType="Custom" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecSal11" Area="DataArea" AreaIndex="0" Caption="DecSal11" FieldName="DecSal11" CellFormat-FormatType="Custom" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecSal12" Area="DataArea" AreaIndex="0" Caption="DecSal12" FieldName="DecSal12" CellFormat-FormatType="Custom" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecSal13" Area="DataArea" AreaIndex="0" Caption="DecSal13" FieldName="DecSal13" CellFormat-FormatType="Custom" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecSal14" Area="DataArea" AreaIndex="0" Caption="DecSal14" FieldName="DecSal14" CellFormat-FormatType="Custom" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecSal15" Area="DataArea" AreaIndex="0" Caption="DecSal15" FieldName="DecSal15" CellFormat-FormatType="Custom" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecSal16" Area="DataArea" AreaIndex="0" Caption="DecSal16" FieldName="DecSal16" CellFormat-FormatType="Custom" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecSal17" Area="DataArea" AreaIndex="0" Caption="DecSal17" FieldName="DecSal17" CellFormat-FormatType="Custom" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecSal18" Area="DataArea" AreaIndex="0" Caption="DecSal18" FieldName="DecSal18" CellFormat-FormatType="Custom" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecSal19" Area="DataArea" AreaIndex="0" Caption="DecSal19" FieldName="DecSal19" CellFormat-FormatType="Custom" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecSal20" Area="DataArea" AreaIndex="0" Caption="DecSal20" FieldName="DecSal20" CellFormat-FormatType="Custom" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecTotalSal" Area="DataArea" AreaIndex="0" Caption="TOTAL SALIDAS" FieldName="DecTotalSal" CellFormat-FormatType="Custom" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDecSaldoFinal" Area="DataArea" AreaIndex="0" Caption="SALDO FINAL" FieldName="DecSaldoFinal" CellFormat-FormatType="Custom" Visible="False">
                                                        </dx:PivotGridField>
                                                    </Fields>
                                                    <Styles CssPostfix="DevGlp">
                                                        <HeaderStyle VerticalAlign="Middle" Wrap="True"></HeaderStyle> 
                                                    </Styles>
                                                    <OptionsPager RowsPerPage="500" AlwaysShowPager="True" NumericButtonCount="5">
                                                    </OptionsPager>
                                                    <Styles>
                                                        <HeaderStyle VerticalAlign="Middle" Wrap="True" />
                                                    </Styles>
                                                </dx:ASPxPivotGrid>
                                                <dx:ASPxGridView ID="dgvInternacional" runat="server" AutoGenerateColumns="False" ClientInstanceName="dgvInternacional" EnableTheming="True" Font-Size="Smaller" Theme="Default" Width="100%" KeyFieldName="LngId" OnCustomSummaryCalculate="dgvInternacional_CustomSummaryCalculate" OnHtmlRowPrepared="dgvInternacional_HtmlRowPrepared">
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
                                                    <Settings ShowHeaderFilterButton="True" ShowFooter="True" />
                                                    <SettingsBehavior AllowFocusedRow="True" />
                                                    <SettingsPopup>
                                                        <EditForm Modal="True" />
                                                        <CustomizationWindow HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" />
                                                    </SettingsPopup>
                                                    <SettingsText CommandNew="Nuevo" />
                                                    <Columns>
                                                        <dx:GridViewCommandColumn ShowInCustomizationForm="True" VisibleIndex="21" Visible="False">
                                                        </dx:GridViewCommandColumn>
                                                        <dx:GridViewDataTextColumn Caption="PLACA" FieldName="StrPlaca" ShowInCustomizationForm="True" VisibleIndex="3">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="NIT" FieldName="LngNit" ShowInCustomizationForm="True" VisibleIndex="4">
                                                            <Settings AutoFilterCondition="Contains" />
                                                            <CellStyle HorizontalAlign="Right">
                                                            </CellStyle>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="N.F. PROPIO" FieldName="LngNoFactProp" ShowInCustomizationForm="True" ToolTip="Número de Factura Propio" VisibleIndex="7">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="IMP. EXTERNO" FieldName="DecImporteExt" ShowInCustomizationForm="True" ToolTip="Importe Externo" VisibleIndex="8">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="IMP. PROPIO" FieldName="DecImporteProp" ShowInCustomizationForm="True" VisibleIndex="9" ToolTip="Importe Propio">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="FECHA" FieldName="StrFecha" ShowInCustomizationForm="True" VisibleIndex="1">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="EST." FieldName="StrEstado" ShowInCustomizationForm="True" VisibleIndex="18" ToolTip="Estado del Movimiento">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="N.F. EXTERNO" FieldName="LngNoFactExt" ShowInCustomizationForm="True" VisibleIndex="6" ToolTip="Número de Factura Externo">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="PROD." FieldName="StrCodProducto" ShowInCustomizationForm="True" VisibleIndex="5" ToolTip="Producto">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="ID" FieldName="LngId" ShowInCustomizationForm="True" VisibleIndex="0" Visible="False">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="PRECIO" FieldName="DecPrecioExt" ShowInCustomizationForm="True" VisibleIndex="10">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="CANTIDAD" FieldName="DecCantidadProp" ShowInCustomizationForm="True" VisibleIndex="13">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="PAIS" FieldName="StrPais" ShowInCustomizationForm="True" VisibleIndex="2">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="TIPO MOVIMIENTO" FieldName="StrTipoMovimiento" ShowInCustomizationForm="True" VisibleIndex="20">
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                    <Styles CssPostfix="DevGlp"><AlternatingRow BackColor="#EEEEEE">
                                                        </AlternatingRow></Styles>
                                                    <TotalSummary>
                                                        <dx:ASPxSummaryItem FieldName="DecImporteProp" ShowInColumn="IMP. PROPIO" SummaryType="Sum" Tag="impP" />
                                                        <dx:ASPxSummaryItem FieldName="DecImporteExt" ShowInColumn="IMP. EXTERNO" SummaryType="Sum" Tag="impE" />
                                                        <dx:ASPxSummaryItem FieldName="DecCantidadExt" ShowInColumn="CANTIDAD" SummaryType="Sum" Tag="cantidad" />
                                                    </TotalSummary>
                                                </dx:ASPxGridView>
                                                <dx:ASPxGridView ID="dgvRecaudo" runat="server" AutoGenerateColumns="False" ClientInstanceName="dgvRecaudo" EnableTheming="True" Font-Size="Smaller" Theme="Default" Width="100%" KeyFieldName="LngId" OnHtmlRowPrepared="dgvRecaudo_HtmlRowPrepared">
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
                                                    <Settings ShowHeaderFilterButton="True" ShowFooter="True" />
                                                    <SettingsBehavior AllowFocusedRow="True" />
                                                    <SettingsPopup>
                                                        <EditForm Modal="True" />
                                                        <CustomizationWindow HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" />
                                                    </SettingsPopup>
                                                    <SettingsText CommandNew="Nuevo" />
                                                    <Columns>
                                                        <dx:GridViewCommandColumn ShowInCustomizationForm="True" VisibleIndex="21" Visible="False">
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
                                                        <dx:GridViewDataTextColumn Caption="ID_TIPO_TRANSACCION" FieldName="ID_TIPO_TRANSACCION" ShowInCustomizationForm="True" VisibleIndex="0" Visible="False">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="ID_PRODUCTO" FieldName="ID_PRODUCTO" ShowInCustomizationForm="True" VisibleIndex="2" Visible="False">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="ID_UNIDAD_MEDIDA" FieldName="ID_UNIDAD_MEDIDA" ShowInCustomizationForm="True" Visible="False" VisibleIndex="4">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="UNIDAD MEDIDA" FieldName="UNIDAD_MEDIDA" ShowInCustomizationForm="True" VisibleIndex="5">
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                    <Styles CssPostfix="DevGlp"><AlternatingRow BackColor="#EEEEEE">
                                                        </AlternatingRow></Styles>
                                                    <TotalSummary>
                                                        <dx:ASPxSummaryItem FieldName="CANTIDAD_FACTURA" ShowInColumn="CANTIDAD_FACTURA" SummaryType="Sum" DisplayFormat="{0}" />
                                                        <dx:ASPxSummaryItem FieldName="IMPORTE_TOTAL" ShowInColumn="IMPORTE_TOTAL" SummaryType="Sum" DisplayFormat="{0}" />
                                                    </TotalSummary>
                                                </dx:ASPxGridView>
                                                <dx:ASPxGridView ID="dgvContenedor" runat="server" AutoGenerateColumns="False" ClientInstanceName="dgvContenedor" EnableTheming="True" Font-Size="Smaller" Theme="Default" Width="100%" KeyFieldName="LngId" OnHtmlRowPrepared="dgvContenedor_HtmlRowPrepared">
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
                                                    <Settings ShowHeaderFilterButton="True" ShowFooter="True" />
                                                    <SettingsBehavior AllowFocusedRow="True" />
                                                    <SettingsPopup>
                                                        <EditForm Modal="True" />
                                                        <CustomizationWindow HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" />
                                                    </SettingsPopup>
                                                    <SettingsText CommandNew="Nuevo" />
                                                    <Columns>
                                                        <dx:GridViewCommandColumn ShowInCustomizationForm="True" VisibleIndex="21" Visible="False">
                                                        </dx:GridViewCommandColumn>
                                                        <dx:GridViewDataTextColumn Caption="StrSubIdentificador" FieldName="StrSubIdentificador" ShowInCustomizationForm="True" VisibleIndex="5" Visible="False">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="TIPO CONT." FieldName="StrTipoContenedor" ShowInCustomizationForm="True" VisibleIndex="2" ToolTip="Tipo de Contenedor Utilizado">
                                                            <Settings AutoFilterCondition="Contains" />
                                                            <CellStyle HorizontalAlign="Right">
                                                            </CellStyle>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="NO. AUTORIZ." FieldName="LngNoAut" ShowInCustomizationForm="True" ToolTip="Número de Autorización de la Factura" VisibleIndex="8">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="NO. FACTURA" FieldName="StrNoTransaccion" ShowInCustomizationForm="True" VisibleIndex="9">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="COD. CTRL." FieldName="StrCodCtrl" ShowInCustomizationForm="True" VisibleIndex="10" ToolTip="Código de Control">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="FECHA" FieldName="StrFecha" ShowInCustomizationForm="True" VisibleIndex="1">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="DecIdProducto" FieldName="DecIdProducto" ShowInCustomizationForm="True" VisibleIndex="13" ToolTip="Estado del Movimiento" Visible="False">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="RAZON SOCIAL" FieldName="StrRazonSocial" ShowInCustomizationForm="True" VisibleIndex="7">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="NIT" FieldName="LngNit" ShowInCustomizationForm="True" VisibleIndex="6">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="ID" FieldName="LngId" ShowInCustomizationForm="True" VisibleIndex="0" Visible="False">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="AUT. SUST. CTRL." FieldName="AutSustCtrl" ShowInCustomizationForm="True" VisibleIndex="11" ToolTip="Autorización de Sustancias Controladas">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="AUT. ANH" FieldName="AutAnh" ShowInCustomizationForm="True" VisibleIndex="12" ToolTip="Autorización de ANH">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="IDENTIFICADOR" FieldName="StrIdentificador" ShowInCustomizationForm="True" VisibleIndex="4">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="StrProducto" FieldName="StrProducto" ShowInCustomizationForm="True" Visible="False" VisibleIndex="14">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="PROD." FieldName="StrCodProducto" ShowInCustomizationForm="True" VisibleIndex="3" ToolTip="Producto">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="StrUnidadMedida" FieldName="StrUnidadMedida" ShowInCustomizationForm="True" Visible="False" VisibleIndex="15">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="EST." FieldName="StrEstado" ShowInCustomizationForm="True" VisibleIndex="16" ToolTip="Estado">
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
                                                    <Styles CssPostfix="DevGlp"><AlternatingRow BackColor="#EEEEEE">
                                                        </AlternatingRow></Styles>
                                                    <TotalSummary>
                                                        <dx:ASPxSummaryItem FieldName="DecCantidad" ShowInColumn="DecCantidad" SummaryType="Sum" Tag="cant" />
                                                        <dx:ASPxSummaryItem FieldName="DecImporte" ShowInColumn="DecImporte" SummaryType="Sum" Tag="imp" />
                                                    </TotalSummary>
                                                </dx:ASPxGridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="7">
                                                <dx:ASPxButton ID="btnExportar" runat="server" Font-Bold="True" ForeColor="Maroon" OnClick="btnExportar_Click" Text="Exportar Excel" Theme="Metropolis" Width="200px">
                                                    <ClientSideEvents Click="function(s, e) {
	    e.processOnServer = confirm('¿Desea exportar a EXCEL el movimiento seleccionado?'); 
    }" />
                                                    <Image IconID="export_exporttoxls_16x16">
                                                    </Image>
                                                </dx:ASPxButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style11">
                                                <dx:ASPxGridViewExporter ID="dgeExportar" runat="server">
                                                </dx:ASPxGridViewExporter>
                                                <dx:ASPxPivotGridExporter ID="pgeExportar" runat="server">
                                                </dx:ASPxPivotGridExporter>
                                            </td>
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
                        <dx:ASPxLabel ID="ASPxLabel44" runat="server" Text="Lugar :" ForeColor="#666666" Font-Bold="True">
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
                        <dx:ASPxLabel ID="ASPxLabel48" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Underline="True" ForeColor="#333333" Text="Representante Legal" Width="100%">
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
                        <dx:ASPxLabel ID="ASPxLabel31" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Underline="True" ForeColor="#333333" Text="Usuario Emisor" Width="100%">
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
                        <dx:ASPxLabel ID="ASPxLabel42" runat="server" ForeColor="#666666" Text="Observación :" Width="100%" Font-Bold="True">
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
                        <dx:ASPxButton ID="btnEmiteDeclaracion" runat="server" EnableTheming="True" Font-Bold="True" ForeColor="Maroon" OnClick="btnEmiteDeclaracion_Click" Text="EMITIR DACLARACION JURADA" Theme="Metropolis" Width="100%">
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














