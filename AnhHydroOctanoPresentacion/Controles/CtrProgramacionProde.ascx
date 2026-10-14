<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtrProgramacionProde.ascx.cs" Inherits="AnhHydroOctanoPresentacion.Controles.CtrProgramacionProde" %>

<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPivotGrid" TagPrefix="dx" %>

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
<script type="text/javascript">
    function onCellClick(Parametro, strProducto) {
        console.log(Parametro);
        pucDetalle.SetHeaderText('Detalle del Documento de Autorización de Producto- ' + strProducto);
        pnlTransaccion.PerformCallback(Parametro);
        pucDetalle.Show();

    }
</script>
<div class="col-md-12">
    <table style="width: 100%">
        <tr>
            <td style="text-align: center; padding-bottom: 1px">
                <dx:ASPxRoundPanel runat="server" ShowHeader="False" Width="100%" ID="pnlTitulo">
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent1" runat="server">
                            <dx:ASPxLabel runat="server" ForeColor="#367B2F" ID="lblTitulo" Font-Bold="True" Font-Names="Rockwell" Font-Size="Larger" Text="PROGRAMACION PRODE"></dx:ASPxLabel>
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
                                                                <%--<ClientSideEvents SelectedIndexChanged="function(s, e) {
	                                                                                pnlDatos.PerformCallback();
                                                                                }" />--%>
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
                                                                <%--<ClientSideEvents SelectedIndexChanged="function(s, e) {
	                                                                                pnlDatos.PerformCallback();
                                                                                }" />--%>
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
                                                                <%-- <ClientSideEvents SelectedIndexChanged="function(s, e) {
	                                                                                pnlDatos.PerformCallback();
                                                                                }" />     --%>
                                                                <Items>
                                                                    <dx:ListEditItem Text="ALMACENAJE DE HIDROCARBUROS LÍQUIDOS" Value="5" />
                                                                    <dx:ListEditItem Selected="True" Text="COMERCIALIZACION DE COMBUSTIBLES LÍQUIDOS EN ESTACION DE SERVICIO" Value="1" />
                                                                    <dx:ListEditItem Text="COMERCIALIZACION DE COMBUSTIBLES LIQUIDOS EN PUESTOS DE VENTA" Value="15" />
                                                                    <dx:ListEditItem Text="CONSUMO DE PRODUCTOS REGULADOS - GRACO" Value="37" />
                                                                    <dx:ListEditItem Text="CONSUMIDOR DIRECTO EN PLANTAS DE ALMACENAJE DE HIDROCARBUROS LÍQUIDOS" Value="104" />
                                                                </Items>
                                                                <ClearButton Visibility="Auto">
                                                                </ClearButton>
                                                            </dx:ASPxComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxLabel ID="ASPxLabel2" runat="server" ForeColor="#003300" Text="Producto :" Visible="false">
                                                            </dx:ASPxLabel>
                                                            <dx:ASPxComboBox ID="cmbProductoProde" runat="server" Theme="Metropolis" TextFormatString="{0}" ValueField="ID_PRODUCTO" ValueType="System.Int32" Width="100%" ClientInstanceName="cmbProductoProde" Visible="false">
                                                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
	                                                                                pnlDatos.PerformCallback();
                                                                                }" />
                                                                <Columns>
                                                                    <dx:ListBoxColumn Caption="NOMBRE" FieldName="NOMBRE" Name="NOMBRE" Width="550px" />
                                                                    <dx:ListBoxColumn Caption="ID_PRODUCTO" FieldName="ID_PRODUCTO" Name="ID_PRODUCTO" Width="0px" />
                                                                </Columns>
                                                                <ClearButton Visibility="Auto">
                                                                </ClearButton>
                                                            </dx:ASPxComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <div style="text-align: center; float: left; width: 100%; padding: 5px 0;">
                                                                <dx:ASPxButton ID="btnConsultar" runat="server" AutoPostBack="False" Font-Bold="True" ForeColor="Maroon" Text="Obtener Información" Theme="Metropolis" Width="55%">
                                                                    <ClientSideEvents Click="function(s, e) {pnlDatos.PerformCallback();}" />
                                                                    <Image IconID="actions_download_16x16">
                                                                    </Image>
                                                                </dx:ASPxButton>
                                                            </div>
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
                                                }"
                                BeginCallback="function(s, e) {
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
                                                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Rockwell" Font-Size="Medium" ForeColor="#367B2F" Text="DATOS DE VOLUMEN PROGRAMADO"></asp:Label>
                                                </legend>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width:50%;">                                   
                                            </td>
                                            <td style="width:10%;"></td>
                                            <td style="width:10%;"></td>
                                            <td style="width:10%;"></td>
                                            <td style="width:10%;">
                                                  <dx:ASPxButton ID="btnExportar" runat="server" AutoPostBack="False" Font-Bold="True" ForeColor="Maroon" Text="Exportar Excel" Theme="Metropolis" Width="200px" OnClick="btnExportar_Click">
                                                    <ClientSideEvents Click="function(s, e) {e.processOnServer = confirm('¿Desea exportar el prode programado?'); }" />
                                                  
                                                    <Image IconID="export_exporttoxls_16x16">

                                                    </Image>
                                                </dx:ASPxButton>
                                            </td>
                                                        <%-- lblMes.txt=cmbPeriodoMes.GetText();
                                                        console.log(lblMes.txt) ;
                                                            lblGestion.txt=cmbPeriodoGestion.GetText();
                                                        console.log(lblGestion.txt) ;
                                                            txtCodBarras.txt='';--%>
                                            <td style="width:10%;">
                                                <dx:ASPxButton ID="btnCierre" runat="server" AutoPostBack="False" Font-Bold="True" ForeColor="Maroon" Text="Cierre Gestion" Theme="Metropolis" Width="200px">
                                                    <%--<ClientSideEvents Click="function(s, e) { e.processOnServer = confirm('¿Desea Cerrar el periodo?');  pucCerrarPeriodo.PerformCallback(); pucCerrarPeriodo.Show();}" />--%>
                                                    <ClientSideEvents Click="function(s, e) {
                                                        if(confirm('¿Desea Cerrar el periodo?'))
                                                        {                                                          
                                                            pucCerrarPeriodo.PerformCallback();
                                                            pucCerrarPeriodo.Show();                                                       

                                                        }
                                                        else{
                                                        e.processOnServer = false; 
                                                        }
                                                    }
                                                    " />
                                                    <Image IconID="actions_apply_16x16">
                                                    </Image>
                                                </dx:ASPxButton>
                                            </td>
                                        </tr>
                                        <tr>

                                            <td colspan="6">
                                                <dx:ASPxGridView ID="dgvProgramado" runat="server" Theme="Default" AutoGenerateColumns="False" EnableTheming="True" Font-Size="Smaller" ClientInstanceName="dgvProgramado" Width="100%" KeyFieldName="IDCONSUMIDOR">
                                                    <SettingsContextMenu EnableRowMenu="False">
                                                    </SettingsContextMenu>
                                                    <SettingsPager PageSize="100">
                                                        <PageSizeItemSettings Items="100, 500, 1000" Visible="True" AllItemText="Todo" ShowAllItem="True">
                                                        </PageSizeItemSettings>
                                                    </SettingsPager>
                                                    <Settings ShowFilterRow="True" />
                                                    <SettingsBehavior AllowFocusedRow="True" ColumnResizeMode="NextColumn" />
                                                    <Columns>
                                                        <%--  <dx:GridViewCommandColumn Name="Nro" VisibleIndex="0">
                                                        </dx:GridViewCommandColumn>--%>
                                                        <dx:GridViewDataTextColumn Caption="Id Consumidor" FieldName="IDCONSUMIDOR" ShowInCustomizationForm="True" VisibleIndex="0" Visible="false" ToolTip="Id Consumidor">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="RAZON SOCIAL" FieldName="DENOMINACION" ShowInCustomizationForm="True" VisibleIndex="2" ToolTip="RAZON SOCIAL">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="CODIGO SIREHIDRO" FieldName="CODIGO" ShowInCustomizationForm="True" VisibleIndex="7" ToolTip="CODIGO SIREHIDRO">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="Id Hidrocarburo" FieldName="IDPRODUCTO" ShowInCustomizationForm="True" VisibleIndex="8" Visible="false" ToolTip="Id Hidrocarburo">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="DEPARTAMENTO" FieldName="DEPARTAMENTO" ShowInCustomizationForm="True" VisibleIndex="1" ToolTip="DEPARTAMENTO">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="PERIODO" FieldName="PERIODO" ShowInCustomizationForm="True" VisibleIndex="9" ToolTip="PERIODO">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="PRODUCTO" FieldName="PRODUCTO" ShowInCustomizationForm="True" VisibleIndex="10" ToolTip="PRODUCTO">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="PROGRAMADO" FieldName="PROGRAMADO" ShowInCustomizationForm="True" VisibleIndex="11" ToolTip="PROGRAMADO">
                                                            <CellStyle Font-Bold="True" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="DESPACHO" FieldName="DESPACHO" ShowInCustomizationForm="True" VisibleIndex="13" ToolTip="DESPACHO">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="SALDO" FieldName="SALDO" ShowInCustomizationForm="True" VisibleIndex="14" ToolTip="SALDO">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="ACTIVIDAD" FieldName="ACTIVIDAD" ShowInCustomizationForm="True" VisibleIndex="3" ToolTip="ACTIVIDAD">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="LICENCIA" FieldName="LICENCIA" ShowInCustomizationForm="True" VisibleIndex="4" ToolTip="LICENCIA">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="VIGENCIA" FieldName="VIGENCIA" ShowInCustomizationForm="True" VisibleIndex="5" ToolTip="VIGENCIA">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="VENCIMIENTO" FieldName="VENCIMIENTO" ShowInCustomizationForm="True" VisibleIndex="6" ToolTip="VENCIMIENTO">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Name="OPCION" VisibleIndex="16">
                                                            <DataItemTemplate>
                                                                <a href="javascript:void(0);" onclick="onCellClick('<%# Eval("PERIODO") +"|"+ Eval("IDPRODUCTO")+"|"+Eval("IDCONSUMIDOR")+"|"+"2" %>','<%# Eval("PRODUCTO")%>');">
                                                                  <%--  <image iconid="actions_download_16x16">
                                                                </image>--%>
                                                                     <div class="btn btn-default btn-sm" data-toggle="tooltip" data-placement="top" title="DOC. AUT. DGSC"><span class="glyphicon glyphicon-list-alt" style="color: #2d7e3e  !important"></span></div>
                                                                
                                                                </a>
                                                                <a href="javascript:void(0);" onclick="onCellClick('<%# Eval("PERIODO") +"|"+ Eval("IDPRODUCTO")+"|"+Eval("IDCONSUMIDOR")+"|"+"3" %>','<%# Eval("PRODUCTO")%>');">
                                                                  <%--  <image iconid="actions_download_16x16">
                                                                </image>--%>
                                                                     <div class="btn btn-default btn-sm" data-toggle="tooltip" data-placement="top" title="DOC. AUT. YPFB"><span class="glyphicon glyphicon-list-alt" style="color: #2d7e3e  !important"></span></div>
                                                                    
                                                                </a>
                                                                <a href="javascript:void(0);" onclick="onCellClick('<%# Eval("PERIODO") +"|"+ Eval("IDPRODUCTO")+"|"+Eval("IDCONSUMIDOR")+"|"+"4" %>','<%# Eval("PRODUCTO")%>');">
                                                                  <%--  <image iconid="actions_download_16x16">
                                                                </image>--%>
                                                                     <div class="btn btn-default btn-sm" data-toggle="tooltip" data-placement="top" title="DOC. AUT. DGSC & YPFB"><span class="glyphicon glyphicon-duplicate" style="color: #2d7e3e  !important"></span></div>
                                                                    
                                                                </a>
                                                            </DataItemTemplate>
                                                        </dx:GridViewDataTextColumn>

                                                        <dx:GridViewDataTextColumn Caption="Id Actividad" FieldName="IDACTIVIDAD" ShowInCustomizationForm="True" Visible="False" VisibleIndex="15">
                                                        </dx:GridViewDataTextColumn>

                                                        <dx:GridViewDataTextColumn Caption="DETALLE PRODE" FieldName="DESCRIPCION" ShowInCustomizationForm="True" VisibleIndex="12">
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
                                            <td>
                                                <dx:ASPxGridViewExporter ID="dgeExportar" runat="server">
                                                </dx:ASPxGridViewExporter>
                                            </td>
                                        </tr>
                                    </table>
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxCallbackPanel>
                        <!--Ventana Emergente-->
                        <dx:ASPxPopupControl ID="pucDetalle" runat="server" CloseAction="CloseButton" CloseOnEscape="True" Modal="True"
                            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Above" ClientInstanceName="pucDetalle"
                            HeaderText="Detalle de Movimientos" AllowDragging="True" PopupAnimationType="None" EnableViewState="False" Maximized="false" ShowMaximizeButton="true" Theme="Metropolis" ScrollBars="Auto" AllowResize="True">
                            <HeaderImage IconID="grid_horizontalgridlinesmajorminor_32x32">
                            </HeaderImage>
                            <HeaderStyle>
                                <BorderBottom BorderStyle="Solid" />
                            </HeaderStyle>
                            <ContentCollection>
                                <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                                    <dx:ASPxCallbackPanel ID="pnlTransaccion" runat="server" ClientInstanceName="pnlTransaccion" OnCallback="pnlTransaccion_Callback">
                                        <%--<ClientSideEvents BeginCallback="function(s, e) { LoadingPanel.Show(); }"
                                            EndCallback="function(s, e) {LoadingPanel.Hide();}" />--%>
                                        <PanelCollection>
                                            <dx:PanelContent ID="PanelContent7" runat="server">
                                                <table class="dxflInternalEditorTable_Youthful">
                                                    <tr>
                                                        <td></td>
                                                        <td></td>
                                                        <td></td>
                                                        <td></td>
                                                        <td style="width: 30%; text-align: right">
                                                            <dx:ASPxButton ID="btnExportarTrans" runat="server" Font-Bold="True" ForeColor="Maroon" Text="Exportar Excel.." Theme="Metropolis" Width="40%" OnClick="btnExportarTrans_Click">
                                                                <ClientSideEvents Click="function(s, e) { e.processOnServer = confirm('¿Desea exportar los movimientos?'); }" />
                                                                <Image IconID="export_exporttoxls_16x16">
                                                                </Image>
                                                            </dx:ASPxButton>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="5">
                                                            <dx:ASPxGridView ID="dgvTransacciones" runat="server" AutoGenerateColumns="False" ClientInstanceName="dgvTransacciones" EnableTheming="True" Font-Size="Smaller" KeyFieldName="decIdDocumento" Theme="Default" Width="100%">
                                                                <SettingsContextMenu EnableRowMenu="False">
                                                                </SettingsContextMenu>

                                                                <SettingsPager NumericButtonCount="5">
                                                                    <PageSizeItemSettings Items="10, 100, 1000" Visible="True" ShowAllItem="True" AllItemText="Todo">
                                                                    </PageSizeItemSettings>
                                                                </SettingsPager>
                                                                <Settings ShowFooter="True" ShowHeaderFilterButton="True" />
                                                                <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" AutoExpandAllGroups="True" />
                                                                <SettingsCommandButton>
                                                                    <NewButton Text=" ">
                                                                        <Image IconID="reports_addgroupheader_16x16" ToolTip="Nuevo Movimiento....">
                                                                        </Image>
                                                                    </NewButton>
                                                                    <EditButton Text=" ">
                                                                        <Image IconID="edit_edit_16x16" ToolTip="Editar">
                                                                        </Image>
                                                                    </EditButton>
                                                                </SettingsCommandButton>
                                                                <SettingsPopup>
                                                                    <EditForm HorizontalAlign="Center" Modal="True" VerticalAlign="Middle" VerticalOffset="0" CloseOnEscape="True" />
                                                                    <CustomizationWindow HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" />
                                                                </SettingsPopup>

                                                                <Columns>
                                                                    <dx:GridViewDataTextColumn Caption="FECHA_VIGENCIA" FieldName="strFechaVigencia" ShowInCustomizationForm="True" VisibleIndex="30">
                                                                        <Settings AutoFilterCondition="Contains" />
                                                                        <CellStyle HorizontalAlign="Center">
                                                                        </CellStyle>
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="CODIGO CONTROL" FieldName="strCodigoControl" ShowInCustomizationForm="True" ToolTip="Codigo Control" VisibleIndex="11">
                                                                        <Settings AutoFilterCondition="Contains" />
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="NIT" FieldName="decNit" ShowInCustomizationForm="True" VisibleIndex="9">
                                                                        <Settings AutoFilterCondition="Contains" />
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="ESTADO" FieldName="strEstado" ShowInCustomizationForm="True" VisibleIndex="36">
                                                                        <Settings AutoFilterCondition="Contains" />
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="PLACA" FieldName="strIdentificador" ShowInCustomizationForm="True" VisibleIndex="12">
                                                                        <Settings AutoFilterCondition="Contains" />
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="TIPO DGSC" FieldName="strTipoDocDgsc" ShowInCustomizationForm="True" VisibleIndex="29">
                                                                        <Settings AutoFilterCondition="Contains" />
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="NO. FACT./DOC." FieldName="strNroDocumento" ShowInCustomizationForm="True" ToolTip="Número de Factura o Documento" VisibleIndex="8">
                                                                        <Settings AutoFilterCondition="Contains" />
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="NRO AUTORIZACION" FieldName="strNroAutorizacion" ShowInCustomizationForm="True" ToolTip="Número de Autorización" VisibleIndex="10">
                                                                        <Settings AutoFilterCondition="Contains" />
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="PRECIO" FieldName="decPrecio" ShowInCustomizationForm="True" VisibleIndex="38">
                                                                        <Settings AutoFilterCondition="Equals" />
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="CANTIDAD" FieldName="decCantidad" ShowInCustomizationForm="True" VisibleIndex="37">
                                                                        <Settings AutoFilterCondition="Equals" />
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="IMPORTE" FieldName="decImporte" ShowInCustomizationForm="True" VisibleIndex="39">
                                                                        <Settings AutoFilterCondition="Equals" />
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="ID" FieldName="decIdDocumento" ShowInCustomizationForm="True" ToolTip="Identificador del Registro del Movimiento" VisibleIndex="0">
                                                                        <Settings AutoFilterCondition="Equals" />
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="CHOFER" FieldName="strChofer" ShowInCustomizationForm="True" VisibleIndex="13">
                                                                        <Settings AutoFilterCondition="Contains" />
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="PRECINTO" FieldName="strPrecintos" ShowInCustomizationForm="True" ToolTip="Estado del Movimiento" VisibleIndex="15">
                                                                        <Settings HeaderFilterMode="CheckedList" />
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="USUARIO EXTERNO" FieldName="strUsuario" ShowInCustomizationForm="True" VisibleIndex="40">
                                                                        <Settings AutoFilterCondition="Contains" />
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="TIPO DOCUMENTO" FieldName="strTipoDocumento" ShowInCustomizationForm="True" ToolTip="Tipo de Documento" VisibleIndex="7">
                                                                        <Settings AutoFilterCondition="Contains" />
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="FECHA EMISION" FieldName="strFechaEmision" ShowInCustomizationForm="True" VisibleIndex="6">
                                                                        <Settings AutoFilterCondition="Contains" />
                                                                    </dx:GridViewDataTextColumn>
                                                                </Columns>
                                                                <TotalSummary>
                                                                    <dx:ASPxSummaryItem FieldName="PRECIO_TOTAL_TRANS" ShowInColumn="IMPORTE (RECAUDO)" SummaryType="Custom" Tag="impRec" />
                                                                    <dx:ASPxSummaryItem FieldName="CANTIDAD_TRANS" ShowInColumn="CANTIDAD (RECAUDO)" SummaryType="Custom" Tag="volRec" />
                                                                    <dx:ASPxSummaryItem FieldName="PRECIO_TOTAL" ShowInColumn="IMPORTE" SummaryType="Custom" Tag="imp" />
                                                                    <dx:ASPxSummaryItem FieldName="CANTIDAD" ShowInColumn="CANTIDAD" SummaryType="Custom" Tag="vol" />
                                                                    <dx:ASPxSummaryItem FieldName="ESTADO_MOVIMIENTO" ShowInColumn="EST." SummaryType="Custom" Tag="est" />
                                                                </TotalSummary>
                                                            </dx:ASPxGridView>
                                                        </td>
                                                        <td>&nbsp;</td>
                                                    </tr>

                                                </table>

                                            </dx:PanelContent>
                                        </PanelCollection>
                                    </dx:ASPxCallbackPanel>
                                </dx:PopupControlContentControl>
                            </ContentCollection>
                            <ContentStyle>
                                <Paddings PaddingBottom="5px" />
                            </ContentStyle>
                        </dx:ASPxPopupControl>
                        <!--End Ventana Emergente-->
                        <!--Ventana Emergente cierre de periodo-->
                        <dx:ASPxPopupControl ID="pucCerrarPeriodo" runat="server" AllowDragging="True" ClientInstanceName="pucCerrarPeriodo" EnableTheming="True" HeaderText="Cierre de Periodo" Height="93px" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Theme="Metropolis" Width="420px" OnWindowCallback="pucCerrarPeriodo_WindowCallback" 
                            >
                            <HeaderImage IconID="navigation_up_16x16">
                            </HeaderImage>
                            <HeaderStyle>
                                <BorderBottom BorderStyle="Solid" />
                            </HeaderStyle>
                            <ContentCollection>
                                <dx:PopupControlContentControl ID="PopupControlContentControl3" runat="server">
                                    <table class="dxflInternalEditorTable_Youthful" align="center">                                  
                                        <tr>
                                            <td style="text-align: right">
                                                <dx:ASPxLabel ID="lblPrecioEf0" runat="server" ClientInstanceName="lblPrecioEf" Font-Bold="True" ForeColor="#666666" Text="Perido Mes : ">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td>
                                                 <dx:ASPxLabel ID="lblMes" runat="server" ClientInstanceName="lblPrecioEf" Font-Bold="True" ForeColor="#666666" Text="">
                                                </dx:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right">
                                                <dx:ASPxLabel ID="lblPrecioEf1" runat="server" ClientInstanceName="lblPrecioEf" Font-Bold="True" ForeColor="#666666" Text="Periodo Gestion : ">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td>
                                                 <dx:ASPxLabel ID="lblGestion" runat="server" ClientInstanceName="lblGestion" Font-Bold="True" ForeColor="#666666" Text="">
                                                </dx:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dx:ASPxLabel ID="ASPxLabel3" runat="server" ClientInstanceName="lblPrecioEf" Font-Bold="True" ForeColor="#666666" Text="Codigo de Barras :">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td>
                                                <dx:ASPxTextBox ID="txtCodBarras" runat="server" ClientInstanceName="txtCodBarras" MaxLength="8" Text="0">
                                                    <MaskSettings Mask="&lt;0..99999999&gt;" PromptChar=" " />
                                                    <ReadOnlyStyle BackColor="#DEE8DB">
                                                    </ReadOnlyStyle>
                                                    <FocusedStyle BackColor="#FFFFCC">
                                                    </FocusedStyle>
                                                    <DisabledStyle BackColor="#DEE8DB">
                                                    </DisabledStyle>
                                                </dx:ASPxTextBox>
                                            </td>
                                        </tr>                                      
                                        <tr><td colspan="2">&nbsp;</td></tr>
                                        <tr>
                                            <td colspan="2">
                                                <dx:ASPxButton ID="btnCerrarPerido" runat="server"  AutoPostBack="False" Font-Bold="True" ForeColor="Maroon" Text="Cerrar Periodo" Theme="Metropolis" Width="100%" OnClick="btnCerrarPerido_Click">
                                                    <ClientSideEvents Click="function(s, e) {
                                                                                             if(confirm('¿Desea cerrar el periodo? - Una vez realizada la acción no podrá revertir el proceso. ¿Desea continuar?'))
                                                                                    {
                                                                                    e.processOnServer = true;
                                                                                    }
                                                                                    else{
                                                                                    e.processOnServer = false; 
                                                                                    }             
}" />
                                                    <Image IconID="actions_apply_16x16">
                                                    </Image>
                                                </dx:ASPxButton>
                                            </td>
                                        </tr>
                                    </table>
                                </dx:PopupControlContentControl>
                            </ContentCollection>
                        </dx:ASPxPopupControl>
                        <!--End Ventana Emergente-->
                    </dx:SplitterContentControl>
                </ContentCollection>
            </dx:SplitterPane>
        </Panes>
    </dx:ASPxSplitter>
</div>
