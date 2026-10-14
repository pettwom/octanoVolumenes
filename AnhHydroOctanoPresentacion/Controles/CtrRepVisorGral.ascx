<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtrRepVisorGral.ascx.cs" Inherits="AnhHydroOctanoPresentacion.Controles.CtrRepVisorGral" %>

<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxPivotGrid.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPivotGrid" tagprefix="dx" %>

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

    </style>

<script type="text/javascript">
    function onCellClick(rowIndex, fieldName, cell) {
        if (fieldName != 'MDIN') {
            cell.style.backgroundColor = "orange";
        }
        for (var i = 0; i<=100; i++) {}
        if (fieldName == 'DecSaldoInicial' || fieldName == 'MDIN' || !isNaN(fieldName.substr(6, 2))) {
            pucDetalle.SetHeaderText('Detalle de Movimientos - ' + lblRazonSocial.GetText());
            pnlTransaccion.PerformCallback(rowIndex + "|" + fieldName + "|1");
            pucDetalle.Show();
        }
    }
</script>

<div class="col-md-12">
    
    <table style="width: 100%; height: 100%">
    <tr>
        <td style="text-align: center; padding-bottom: 1px">
            <dx:ASPxRoundPanel runat="server" ShowHeader="False" Width="100%" ID="pnlTitulo">
                <PanelCollection>
                    <dx:PanelContent runat="server">
                        <dx:ASPxLabel runat="server" ForeColor="#367B2F" ID="lblTitulo" Font-Bold="True" Font-Names="Rockwell" Font-Size="Larger" Text="VISOR GENERAL DE MOVIMIENTOS"></dx:ASPxLabel>
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
                                <dx:ASPxCallbackPanel ID="pnlCabecera" runat="server" ClientInstanceName="pnlCabecera" Width="100%" OnCallback="pnlCabecera_Callback">
                                    <PanelCollection>
                                        <dx:PanelContent runat="server">
                                            <table style="width: 100%">
                                                <tr>
                                                    <td colspan="2" style="padding-bottom: 5px">
                                                        <dx:ASPxLabel ID="ASPxLabel60" runat="server" Font-Bold="True" Font-Names="Rockwell" ForeColor="#487B5C" Text="CARACTERISTICAS" Width="100%">
                                                            <BorderBottom BorderStyle="Solid" />
                                                        </dx:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">Fecha Inicial :<dx:ASPxDateEdit ID="detFechaIni" runat="server" ClientInstanceName="detFechaIni" EnableTheming="True" Height="23px" Theme="Metropolis" ToolTip="Seleccione la fecha inicial" Width="140px">
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
                                                    <td colspan="2">Fecha Final :<dx:ASPxDateEdit ID="detFechaFin" runat="server" ClientInstanceName="detFechaFin" Theme="Metropolis" ToolTip="Seleccione la fecha final" Width="140px">
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
                                                        Selección Directa de Mes :
                                                        <uc1:CtrFecheroMes ID="cfmMes" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" style="padding-bottom: 5px">
                                                        Tipo de Operación :<dx:ASPxComboBox ID="cmbTipoOp" runat="server" ClientInstanceName="cmbTipoOp" TextField="NOMBRE" TextFormatString="{0};{2};" Theme="Metropolis" ToolTip="Seleccione un tipo de operación" ValueField="ID_TIPO_OPERACION" Width="57%">
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
                                                <tr>
                                                    <td colspan="2" style="padding-bottom: 5px">
                                                        <dx:ASPxButton ID="btnConsultar" runat="server" Text="Obtener Registros" AutoPostBack="False" Font-Bold="True" ForeColor="Maroon" Theme="Metropolis" Width="57%">
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

                                    <ClientSideEvents FocusedNodeChanged="function(s, e) {
pnlCabecera.PerformCallback();
}" NodeDblClick="function(s, e) {
pnlCabecera.PerformCallback();	
}" Init="function(s, e) {
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
                                        <td>&nbsp;</td>
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
                                            <td colspan="3" style="width: 35%">
                                                <dx:ASPxLabel ID="lblRazonSocial" runat="server" ClientInstanceName="lblRazonSocial">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td style="width: 15%">
                                                <asp:Label ID="lblNit" runat="server"></asp:Label>
                                            </td>
                                            <td style="width: 15%">
                                                <asp:Label ID="lblDepto" runat="server"></asp:Label>
                                            </td>
                                            <td colspan="2" style="padding-bottom: 5px">
                                                <asp:Label ID="lblRepLegal" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="7" style="text-align: right">
                                                <dx:ASPxButton ID="btnExportar" runat="server" Font-Bold="True" ForeColor="Maroon" OnClick="btnExportar_Click" Text="Exportar Listado Excel" Theme="Metropolis" Width="200px">
                                                    <Image IconID="export_exporttoxls_16x16">
                                                    </Image>
                                                </dx:ASPxButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="7">
                                                <dx:ASPxPivotGrid ID="pvgDatos" runat="server" ClientIDMode="AutoID" Width="100%" EnableTheming="True" Font-Size="Smaller" Theme="Default">
                                                    <Fields>
                                                        <dx:PivotGridField ID="fieldCANTIDADMOV" Area="DataArea" AreaIndex="0" Caption="MOV. CANT." FieldName="CANTIDAD_MOV" CellFormat-FormatType="Numeric">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldIDMOVIMIENTO" Area="RowArea" AreaIndex="0" Caption="ID" FieldName="ID_MOVIMIENTO">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldSTRFECHAOPERACION" AreaIndex="0" Caption="MOV. FECHA/HORA" FieldName="STR_FECHA_OPERACION">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldIDENTIFICADOR" AreaIndex="14" Caption="IDENTIFICADOR" FieldName="IDENTIFICADOR">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldPRODUCTO" Area="RowArea" AreaIndex="4" Caption="PRODUCTO" FieldName="PRODUCTO">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldSTRESTADOMOVIMIENTO" Area="RowArea" AreaIndex="5" Caption="EST." FieldName="STR_ESTADO_MOVIMIENTO">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldENVASE" AreaIndex="12" Caption="ENVASE" FieldName="ENVASE">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldTIPOFLUJO" Area="RowArea" AreaIndex="6" Caption="T. FLUJO" FieldName="TIPO_FLUJO">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldTIPOMOVIMIENTO" Area="RowArea" AreaIndex="7" Caption="TIPO MOV." FieldName="TIPO_MOVIMIENTO">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldPAIS" Area="RowArea" AreaIndex="8" Caption="PAIS" FieldName="PAIS">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldTIPOTRANSACCION" AreaIndex="3" Caption="TIPO DOC. RESP." FieldName="TIPO_TRANSACCION" Area="RowArea">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldFECHAHORAEMISION" AreaIndex="1" Caption="D.R. FECHA/HORA" FieldName="FECHA_HORA_EMISION">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldNROTRANSACCION" AreaIndex="2" Caption="NRO. D.R." FieldName="NRO_TRANSACCION" Area="RowArea">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldNROAUTORIZACION" AreaIndex="2" Caption="NRO. AUTORIZACION" FieldName="NRO_AUTORIZACION">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldCODIGOCONTROL" AreaIndex="3" Caption="COD. CONTROL" FieldName="CODIGO_CONTROL">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldNITEMISOR" AreaIndex="4" Caption="NIT EMISOR" FieldName="NIT_EMISOR">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldNITCONSUMIDOR" AreaIndex="5" Caption="NIT CONSUMIDOR" FieldName="NIT_CONSUMIDOR">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldRAZONSOCIAL" AreaIndex="6" Caption="RAZON SOCIAL" FieldName="RAZON_SOCIAL">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldPRECIOUNITARIO" AreaIndex="7" Caption="PRECIO" FieldName="PRECIO_UNITARIO" CellFormat-FormatType="Numeric" TotalsVisibility="None">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldPRECIOTOTAL" AreaIndex="8" Caption="IMPORTE" FieldName="PRECIO_TOTAL" CellFormat-FormatType="Numeric">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldUSUARIOTRANSACCION" AreaIndex="9" Caption="USUARIO D.R." FieldName="USUARIO_TRANSACCION">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldFORMAPAGO" AreaIndex="10" Caption="FORMA PAGO" FieldName="FORMA_PAGO">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldCANTIDAD" AreaIndex="11" Caption="CANT. D.R." FieldName="CANTIDAD" CellFormat-FormatType="Numeric">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldFECHAMOV" Area="RowArea" AreaIndex="1" Caption="MOV. FECHA" FieldName="FECHA_MOV">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldHORAMOV" AreaIndex="13" Caption="MOV. HORA" FieldName="HORA_MOV">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldTIPOCONSUMIDOR" AreaIndex="9" Caption="CATEGORIA" FieldName="TIPO_CONSUMIDOR" Area="RowArea">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldBARCODE" Caption="BARCODE" FieldName="BARCODE" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldIDTRANSACCION1" AreaIndex="14" Caption="ID_TRANSACCION" FieldName="ID_TRANSACCION" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDESCFUENTEORIGEN" AreaIndex="14" Caption="DESC_FUENTE_ORIGEN" FieldName="DESC_FUENTE_ORIGEN" Visible="False">
                                                        </dx:PivotGridField>
                                                    </Fields>
                                                    <Styles CssPostfix="DevGlp"></Styles>
                                                    <OptionsPager RowsPerPage="100">
                                                        <PageSizeItemSettings Items="10, 50, 100, 200, 500, 1000" Visible="True">
                                                        </PageSizeItemSettings>
                                                    </OptionsPager>
                                                </dx:ASPxPivotGrid>
                                            </td>
                                    </tr>
                                        <tr>
                                            <td colspan="7">
                                                
                                                &nbsp;</td>
                                    </tr>
                                        <tr>
                                            <td class="auto-style11">
                                                <dx:ASPxPivotGridExporter ID="pgeExportar" runat="server">
                                                </dx:ASPxPivotGridExporter>
                                            </td>
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








    
