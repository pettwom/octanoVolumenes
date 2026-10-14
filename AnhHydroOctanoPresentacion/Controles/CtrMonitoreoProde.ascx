<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtrMonitoreoProde.ascx.cs" Inherits="AnhHydroOctanoPresentacion.Controles.CtrMonitoreoProde" %>

<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxPivotGrid.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPivotGrid" tagprefix="dx" %>

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
                            <dx:ASPxLabel runat="server" ForeColor="#367B2F" ID="lblTitulo" Font-Bold="True" Font-Names="Rockwell" Font-Size="Larger" Text="MONITOREO PRODE"></dx:ASPxLabel>
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
                                    <dx:ASPxCallbackPanel ID="pnlCabecera" runat="server" ClientInstanceName="pnlCabecera" Width="100%">
                                        <PanelCollection>
                                            <dx:PanelContent ID="PanelContent2" runat="server">
                                                
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxLabel ID="ASPxLabel1" runat="server" ForeColor="#003300" Text="Periodo Mes :">
                                                            </dx:ASPxLabel>
                                                            <dx:ASPxComboBox ID="cmbPeriodoMes" runat="server" EnableTheming="True" Theme="Metropolis" ValueType="System.Int32" Width="100%" ToolTip="Seleccione un Periodo Mes" ClientInstanceName="cmbPeriodoMes">
                                                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
	                                                                                pnlDatos.PerformCallback();
                                                                                }" />
                                                                <Items>                                                                    
                                                                    <dx:ListEditItem Selected="True" Text="ENERO" Value="1" />
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
                                                        <td>
                                                            <dx:ASPxLabel ID="ASPxLabel56" runat="server" ForeColor="#003300" Text="Periodo Gestión:">
                                                            </dx:ASPxLabel>
                                                            <dx:ASPxComboBox ID="cmbPeriodoGestion" runat="server" EnableTheming="True" Theme="Metropolis" ValueType="System.Int32" Width="100%" ToolTip="Seleccione un Periodo Gestion" ClientInstanceName="cmbPeriodoGestion">
                                                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
	                                                                                pnlDatos.PerformCallback();
                                                                                }" />
                                                                <Items>                                                                    
                                                                    <dx:ListEditItem Selected="True" Text="2019" Value="2019" />
                                                                    <dx:ListEditItem Text="2020" Value="2020" />
                                                                    <dx:ListEditItem Text="2021" Value="2021" />
                                                                    <dx:ListEditItem Text="2022" Value="2022" />
                                                                    <dx:ListEditItem Text="2023" Value="2023" />
                                                                    <dx:ListEditItem Text="2024" Value="2024" />
                                                                    <dx:ListEditItem Text="2025" Value="2025" />
                                                                    <dx:ListEditItem Text="2026" Value="2026" />                                                                  
                                                                </Items>
                                                                <ClearButton Visibility="Auto">
                                                                </ClearButton>
                                                            </dx:ASPxComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxLabel ID="ASPxLabel57" runat="server" ForeColor="#003300" Text="Tipo de Actividad :">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxComboBox ID="cmbActividad" runat="server" SelectedIndex="1" Theme="Metropolis" Width="100%" ClientInstanceName="cmbActividad">
                                                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
	                                                                                pnlDatos.PerformCallback();
                                                                                }" />                                                                
                                                                <Items>
                                                                    <dx:ListEditItem Text="ALMACENAJE DE HIDROCARBUROS LÍQUIDOS" Value="5" />
                                                                    <dx:ListEditItem Selected="True" Text="COMERCIALIZACION DE COMBUSTIBLES LÍQUIDOS EN ESTACION DE SERVICIO" Value="1" />
                                                                    <dx:ListEditItem Text="COMERCIALIZACION DE COMBUSTIBLES LIQUIDOS EN PUESTOS DE VENTA" Value="15" />
                                                                    <dx:ListEditItem Text="COMERCIALIZACION DE GAS NATURAL VEHICULAR EN ESTACION DE SERVICIO" Value="2" />
                                                                    <dx:ListEditItem Text="SUMINISTRO DE COMBUSTIBLE DE AVIACIÓN EN AEROPUERTOS" Value="50" />
                                                                </Items>
                                                                <ClearButton Visibility="Auto">
                                                                </ClearButton>
                                                            </dx:ASPxComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxLabel ID="ASPxLabel2" runat="server" ForeColor="#003300" Text="Producto :">
                                                            </dx:ASPxLabel>
                                                            <dx:ASPxComboBox ID="cmbProductoProde" runat="server" Theme="Metropolis" TextFormatString="{0}" ValueField="ID_PRODUCTO" ValueType="System.Int32" Width="100%" ClientInstanceName="cmbProductoProde">                                                                
                                                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
	                                                                                pnlDatos.PerformCallback();
                                                                                }" />
                                                                <Columns>
                                                                    <dx:ListBoxColumn Caption="NOMBRE" FieldName="NOMBRE" Name="NOMBRE" Width="550px"/>
                                                                    <dx:ListBoxColumn Caption="ID_PRODUCTO" FieldName="ID_PRODUCTO" Name="ID_PRODUCTO" Width="0px" />
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
                                                }" BeginCallback="function(s, e) {
                                                LoadingPanel.Show();
                                                }" />
                            <PanelCollection>
                                <dx:PanelContent ID="PanelContent3" runat="server">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td colspan="6" style="text-align: center;">
                                                <dx:ASPxRoundPanel ID="pnlMensaje" runat="server" ShowHeader="False" Visible="False" Width="100%">
                                                    <PanelCollection>
                                                        <dx:PanelContent ID="PanelContent4" runat="server">
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
                                                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Rockwell" Font-Size="Medium" ForeColor="#367B2F" Text="DATOS DE VOLUMEN DEL PRODE"></asp:Label>
                                                </legend>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="#10420F" Text="VOLUMEN MAXIMO PARA ASIGNAR"></asp:Label>
                                            </td>
                                            <td colspan="1">
                                                <asp:Label ID="Label3" runat="server" Font-Bold="True" ForeColor="#10420F" Text="VOLUMEN ASIGNADO"></asp:Label>
                                            </td>
                                            <td colspan="1">
                                                <asp:Label ID="Label4" runat="server" Font-Bold="True" ForeColor="#10420F" Text="VOLUMEN EN RESERVA"></asp:Label>
                                            </td>
                                            <td colspan="1">
                                                <asp:Label ID="Label11" runat="server" Font-Bold="True" ForeColor="#10420F" Text="VOLUMEN PROGRAMADO"></asp:Label>
                                            </td>
                                            <td colspan="1">                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="padding-bottom: 5px;">
                                                <dx:ASPxTextBox ID="txtTecho" runat="server" ClientInstanceName="txtTecho" Enabled="false">
                                                    <MaskSettings Mask="&lt;0..999999999999999&gt;" PromptChar=" " />
                                                </dx:ASPxTextBox>                                                
                                            </td>
                                            <td colspan="1" style="padding-bottom: 5px;">
                                                <dx:ASPxTextBox ID="txtAsignado" runat="server" ClientInstanceName="txtAsignado" Enabled="false">
                                                    <MaskSettings Mask="&lt;0..999999999999999&gt;" PromptChar=" " />
                                                </dx:ASPxTextBox>                                                
                                            </td>
                                            <td colspan="1" style="padding-bottom: 5px;">
                                                <dx:ASPxTextBox ID="txtReserva" runat="server" ClientInstanceName="txtReserva" Enabled="false">
                                                    <MaskSettings Mask="&lt;0..999999999999999&gt;" PromptChar=" " />
                                                </dx:ASPxTextBox>
                                            </td>
                                            <td colspan="1" style="padding-bottom: 5px;">
                                                <dx:ASPxTextBox ID="txtProgramado" runat="server" ClientInstanceName="txtProgramado" Enabled="false">
                                                    <MaskSettings Mask="&lt;0..999999999999999&gt;" PromptChar=" " />
                                                </dx:ASPxTextBox>                                                
                                            </td>
                                            <td colspan="1" style="padding-bottom: 5px;">
                                                
                                            </td>
                                        </tr>

                                        <tr>
                                            <td colspan="1">
                                                <asp:Label ID="Label5" runat="server" Font-Bold="True" ForeColor="#10420F" Text="Semana 1" Visible="false"></asp:Label>
                                            </td>
                                            <td colspan="1">
                                                <asp:Label ID="Label6" runat="server" Font-Bold="True" ForeColor="#10420F" Text="Semana 2" Visible="false"></asp:Label>
                                            </td>
                                            <td colspan="1">
                                                <asp:Label ID="Label7" runat="server" Font-Bold="True" ForeColor="#10420F" Text="Semana 3" Visible="false"></asp:Label>
                                            </td>
                                            <td colspan="1">
                                                <asp:Label ID="Label8" runat="server" Font-Bold="True" ForeColor="#10420F" Text="Semana 4" Visible="false"></asp:Label>
                                            </td>
                                            <td colspan="1">
                                                <asp:Label ID="Label9" runat="server" Font-Bold="True" ForeColor="#10420F" Text="Semana 5" Visible="false"></asp:Label>
                                            </td>
                                            <td colspan="1">
                                                <asp:Label ID="Label10" runat="server" Font-Bold="True" ForeColor="#10420F" Text="Semana 6" Visible="false"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="1" style="padding-bottom: 5px; width: 16%">
                                                <dx:ASPxTextBox ID="txtSemana1" runat="server" ClientInstanceName="txtSemana1" ClientVisible="false"></dx:ASPxTextBox>
                                            </td>
                                            <td colspan="1" style="padding-bottom: 5px; width: 16%">
                                                <dx:ASPxTextBox ID="txtSemana2" runat="server" ClientInstanceName="txtSemana2" ClientVisible="false"></dx:ASPxTextBox>
                                            </td>
                                            <td colspan="1" style="padding-bottom: 5px; width: 16%">
                                                <dx:ASPxTextBox ID="txtSemana3" runat="server" ClientInstanceName="txtSemana3" ClientVisible="false"></dx:ASPxTextBox>
                                            </td>
                                            <td colspan="1" style="padding-bottom: 5px; width: 16%">
                                                <dx:ASPxTextBox ID="txtSemana4" runat="server" ClientInstanceName="txtSemana4" ClientVisible="false"></dx:ASPxTextBox>
                                            </td>
                                            <td colspan="1" style="padding-bottom: 5px; width: 16%">
                                                <dx:ASPxTextBox ID="txtSemana5" runat="server" ClientInstanceName="txtSemana5" ClientVisible="false"></dx:ASPxTextBox>
                                            </td>
                                            <td colspan="1" style="padding-bottom: 5px; width: 20%">
                                                <dx:ASPxTextBox ID="txtSemana6" runat="server" ClientInstanceName="txtSemana6" ClientVisible="false"></dx:ASPxTextBox>
                                            </td>
                                        </tr>

                                        <tr>
                                        
                                            <td colspan="6">
                                                <dx:ASPxGridView ID="dgvMonitoreo" runat="server" Theme="Default" AutoGenerateColumns="False" EnableTheming="True" Font-Size="Smaller" ClientInstanceName="dgvMonitoreo" Width="100%" OnHtmlRowPrepared="dgvMonitoreo_HtmlRowPrepared">
                                                    <SettingsContextMenu EnableRowMenu="False">
                                                    </SettingsContextMenu>                                                    
                                                    <SettingsPager PageSize="100">
                                                        <PageSizeItemSettings Items="50, 100, 200, 500, 1000" Visible="True">
                                                        </PageSizeItemSettings>
                                                    </SettingsPager>
                                                    <Columns>
                                                        <dx:GridViewDataTextColumn Caption="Id Consumidor" FieldName="DecIdConsumidor" ShowInCustomizationForm="True" VisibleIndex="0" Visible="false" ToolTip="Id Consumidor">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Estación de Servicion" FieldName="StrEstacionServicio" ShowInCustomizationForm="True" VisibleIndex="1" ToolTip="Estación de Servicion">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Codigo Sirehidro" FieldName="StrCodHydroProyecto" ShowInCustomizationForm="True" VisibleIndex="2" ToolTip="Codigo Sirehidro">
                                                            <Settings AutoFilterCondition="Contains" />                                                          
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Id Hidrocarburo" FieldName="DecIdHidrocarburo" ShowInCustomizationForm="True" VisibleIndex="3" Visible="false" ToolTip="Id Hidrocarburo">
                                                            <Settings AutoFilterCondition="Contains" />                                                          
                                                        </dx:GridViewDataTextColumn>                                                        
                                                        <dx:GridViewDataTextColumn Caption="Id Departamento" FieldName="DecIdDepartamento" ShowInCustomizationForm="True" VisibleIndex="4" Visible="false" ToolTip="Id Departamento">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Departamento" FieldName="StrDepartamento" ShowInCustomizationForm="True" VisibleIndex="5" ToolTip="Departamento">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Semana Programada" FieldName="StrSemana" ShowInCustomizationForm="True" VisibleIndex="6" ToolTip="Semana Programada">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Volumen Recepcion" FieldName="DecRecepcion" ShowInCustomizationForm="True" VisibleIndex="7" ToolTip="Volumen Recepcion">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Volumen Demasia" FieldName="DecDemasia" ShowInCustomizationForm="True" VisibleIndex="8" ToolTip="Volumen Demasia">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Total Ingreso" FieldName="DecTotalIngreso" ShowInCustomizationForm="True" VisibleIndex="9" CellStyle-Font-Bold="true" ToolTip="Total Ingreso">
                                                            <Settings AutoFilterCondition="Contains" />
                                                            <CellStyle Font-Bold="True"/>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Volumen Venta" FieldName="DecVenta" ShowInCustomizationForm="True" VisibleIndex="10" ToolTip="Volumen Venta">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Volumen Merma" FieldName="DecMerma" ShowInCustomizationForm="True" VisibleIndex="11" ToolTip="Volumen Merma">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Total Egreso" FieldName="DecTotalEgreso" ShowInCustomizationForm="True" VisibleIndex="12" CellStyle-Font-Bold="true" ToolTip="Total Egreso">
                                                            <Settings AutoFilterCondition="Contains" />
                                                            <CellStyle Font-Bold="True"/>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Prode Programado" FieldName="DecProde" ShowInCustomizationForm="True" VisibleIndex="13" CellStyle-Font-Bold="true" ToolTip="Prode Programado">
                                                            <Settings AutoFilterCondition="Contains" />
                                                            <CellStyle Font-Bold="True"/>
                                                        </dx:GridViewDataTextColumn>                                                        
                                                    </Columns>
                                                    <Styles CssPostfix="DevGlp"><AlternatingRow BackColor="#EEEEEE">
                                                        </AlternatingRow></Styles>
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
        </Panes>
    </dx:ASPxSplitter>
</div>