<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtrRepMovimientoDdjj.ascx.cs" Inherits="AnhHydroOctanoPresentacion.Controles.CtrRepMovimientoDdjj" %>

<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxPivotGrid.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPivotGrid" tagprefix="dx" %>

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
                        <dx:ASPxLabel runat="server" ForeColor="#367B2F" ID="lblTitulo" Font-Bold="True" Font-Names="Rockwell" Font-Size="Larger" Text="REPORTE - MOVIMIENTO DETALLADO DECLARADO"></dx:ASPxLabel>
                </dx:PanelContent>
            </PanelCollection>
            <Border BorderStyle="Ridge" BorderWidth="1px" BorderColor="#003300"></Border>
            </dx:ASPxRoundPanel>
        </td>
    </tr>
    
    <tr>
        <td style="height: 100%">
            
    
            <dx:ASPxCallbackPanel ID="pnlOpciones" runat="server" Theme="Youthful" Width="100%" ClientInstanceName="pnlOpciones" OnCallback="pnlDatos_Callback">
                        <PanelCollection>
                            <dx:PanelContent runat="server">
                                <table style="width: 100%">
                                    <tr>
                                        <td colspan="4">
                                            <legend>
                                                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Rockwell" Font-Size="Medium" ForeColor="#367B2F" Text="CARACTERISTICAS DEL REPORTE"></asp:Label>
                                            </legend>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dx:ASPxLabel ID="ASPxLabel57" runat="server" ForeColor="#003300" Text="Periodo Inicial :">
                                            </dx:ASPxLabel>
                                        </td>
                                        <td>
                                            <dx:ASPxLabel ID="ASPxLabel58" runat="server" ForeColor="#003300" Text="Periodo Final :">
                                            </dx:ASPxLabel>
                                        </td>
                                        <td style="padding-left: 20px">
                                            <dx:ASPxLabel ID="ASPxLabel59" runat="server" ForeColor="#003300" Text="Actividad :">
                                            </dx:ASPxLabel>
                                        </td>
                                        <td style="padding-left: 20px">
                                            <dx:ASPxLabel ID="ASPxLabel60" runat="server" ForeColor="#003300" Text="Departamento :">
                                            </dx:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 20%">
                                            <dx:ASPxDateEdit ID="detFechaIni" runat="server" ClientInstanceName="detFechaIni" EditFormat="Custom" EditFormatString="MM/yyyy" EnableTheming="True" Height="23px" Theme="Metropolis" ToolTip="Seleccione el periodo inicial" Width="100%" Font-Bold="True">
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
                                        <td style="width: 20%">
                                            <dx:ASPxDateEdit ID="detFechaFin" runat="server" ClientInstanceName="detFechaFin" EditFormat="Custom" EditFormatString="MM/yyyy" Theme="Metropolis" ToolTip="Seleccione el periodo final" Width="100%" Font-Bold="True">
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
                                        <td style="width: 40%; padding-left: 20px">
                                            <dx:ASPxComboBox ID="cmbActividad" runat="server" SelectedIndex="1" Theme="Metropolis" Width="100%">
                                                <ClientSideEvents Init="function(s, e) {
cmbTipoOperacion.PerformCallback();	
}" SelectedIndexChanged="function(s, e) {
cmbTipoOperacion.PerformCallback();	
}" />
                                                <Items>
                                                    <dx:ListEditItem Text="ALMACENAJE DE HIDROCARBUROS LÍQUIDOS" Value="5" />
                                                    <dx:ListEditItem Selected="True" Text="COMERCIALIZACION DE COMBUSTIBLES LÍQUIDOS EN ESTACION DE SERVICIO" Value="1" />
                                                    <dx:ListEditItem Text="COMERCIALIZACION DE COMBUSTIBLES LIQUIDOS EN PUESTOS DE VENTA" Value="15" />
                                                    <dx:ListEditItem Text="COMERCIALIZACION DE GAS NATURAL VEHICULAR EN ESTACION DE SERVICIO" Value="2" />
                                                    <dx:ListEditItem Text="SUMINISTRO DE COMBUSTIBLE DE AVIACIÓN EN AEROPUERTOS" Value="50" />
                                                    <dx:ListEditItem Text="DISTRIBUCION DE GAS NATURAL POR REDES" Value="34" />
                                                </Items>
                                                <ClearButton Visibility="Auto">
                                                </ClearButton>
                                            </dx:ASPxComboBox>
                                        </td>
                                        <td style="width: 20%; padding-left: 20px">
                                            <dx:ASPxComboBox ID="cmbDepartamento" runat="server" SelectedIndex="0" Theme="Metropolis" Width="100%">
                                                <Items>
                                                    <dx:ListEditItem Selected="True" Text="--SELECCIONAR--" Value="10" />
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
                                        <td>
                                            <dx:ASPxLabel ID="ASPxLabel61" runat="server" ForeColor="#003300" Text="Tipo de Operación :">
                                            </dx:ASPxLabel>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dx:ASPxComboBox ID="cmbTipoOperacion" runat="server" Theme="Metropolis" Width="100%" ClientInstanceName="cmbTipoOperacion" OnCallback="cmbTipoOperacion_Callback" ValueField="ID_TIPO_OPERACION" ValueType="System.Int32">
                                                <Columns>
                                                    <dx:ListBoxColumn Caption="ID_TIPO_OPERACION" FieldName="ID_TIPO_OPERACION" Visible="False" />
                                                    <dx:ListBoxColumn Caption="NOMBRE" FieldName="NOMBRE" />
                                                </Columns>
                                                <ClearButton Visibility="Auto">
                                                </ClearButton>
                                            </dx:ASPxComboBox>
                                        </td>
                                        <td colspan="2">
                                            &nbsp;</td>
                                        
                                        <td>
                                            <dx:ASPxButton ID="btnConsultar" runat="server" AutoPostBack="False" Font-Bold="True" ForeColor="Maroon" Text="Obtener Registros" Theme="Metropolis" Width="100%">
                                                <ClientSideEvents Click="function(s, e) {
pnlDatos.PerformCallback();
}" />
                                                <Image IconID="actions_download_16x16">
                                                </Image>
                                            </dx:ASPxButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            
                                            <dx:ASPxLabel ID="ASPxLabel63" runat="server" ForeColor="#003300" Height="0px" Width="100%">
                                                <BorderBottom BorderStyle="Solid" BorderWidth="2px" />
                                            </dx:ASPxLabel>
                                            
                                        </td>
                                    </tr>
                                </table>
							</dx:PanelContent>
                        </PanelCollection>
                    </dx:ASPxCallbackPanel>

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
                                        <td colspan="7" style="text-align: center; padding-top: 10px">
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
                                            
                                            
                                            
                                        </td>
                                    </tr>
                                        <tr>
                                            <td style="width: 15%; padding-top: 10px">
                                                <dx:ASPxComboBox ID="cmbTipoExport" runat="server" ClientInstanceName="cmbTipoExport" Height="27px" SelectedIndex="0" Theme="Metropolis" ToolTip="Seleccione el Formato del Reporte" ValueType="System.Int32" Width="100%">
                                                    <Items>
                                                        <dx:ListEditItem Selected="True" Text="EXCEL" Value="1" />
                                                        <dx:ListEditItem Text="PDF" Value="2" />
                                                    </Items>
                                                    <ClearButton Visibility="Auto">
                                                    </ClearButton>
                                                </dx:ASPxComboBox>
                                            </td>
                                            <td style="width: 15%; padding-top: 10px">
                                                <dx:ASPxButton ID="btnExportar" runat="server" Font-Bold="True" ForeColor="Maroon" OnClick="btnExportar_Click" Text="Exportar Listado" Theme="Metropolis" Width="100%">
                                                    <Image IconID="actions_converttorange_16x16">
                                                    </Image>
                                                </dx:ASPxButton>
                                            </td>
                                            <td style="width: 70%" colspan="5">
                                            </td>
                                    </tr>
                                        <tr>
                                            <td colspan="7">
                                                <dx:ASPxPivotGrid ID="pvgReporte" runat="server" ClientIDMode="AutoID" Width="100%" EnableTheming="True" Font-Size="X-Small" Theme="Default">
                                                    <Fields>
                                                        <dx:PivotGridField ID="fieldFECHAOPERACIONDIA" AreaIndex="2" Caption="DIA" FieldName="FECHA_OPERACION_DIA">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldINFTIPO" AreaIndex="6" Caption="TIPO INFRAESTRUCTURA" FieldName="INF_TIPO">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldINFCODIGO" AreaIndex="7" Caption="INFRAESTRUCTURA" FieldName="INF_CODIGO">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldCANTIDAD" AreaIndex="0" Caption="CANTIDAD" FieldName="CANTIDAD" Area="DataArea" CellFormat-FormatType="Numeric" TotalsVisibility="None">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDECGESTION" AreaIndex="0" Caption="GESTION" FieldName="DEC_GESTION">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldSTRPERIODO" AreaIndex="1" Caption="PERIODO" FieldName="STR_PERIODO" Area="RowArea">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDEPTO1" Area="RowArea" AreaIndex="0" Caption="DEPARTAMENTO" FieldName="DEPTO">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldPROYECTO" AreaIndex="3" Caption="PROYECTO" FieldName="PROYECTO">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldFECHAOPERACION" AreaIndex="4" Caption="FECHA MOV." FieldName="FECHA_OPERACION">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldPROYORIGEN" AreaIndex="5" Caption="PROY. ORIGEN" FieldName="PROY_ORIGEN">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldTIPODESPACHO" AreaIndex="4" Caption="CATEGORIA" FieldName="TIPO_DESPACHO" Area="RowArea">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldENVASE" AreaIndex="8" Caption="ENVASE" FieldName="ENVASE" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldCANTIDADENVASES" AreaIndex="9" Caption="CANTIDAD ENVASES" CellFormat-FormatType="Numeric" FieldName="CANTIDAD_ENVASES" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldESTADO" AreaIndex="8" Caption="ESTADO" FieldName="ESTADO">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldTIPOFLUJO" AreaIndex="10" Caption="TIPO FLUJO" FieldName="TIPO_FLUJO">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldTIPOMOV" AreaIndex="11" Caption="TIPO MOVIM." FieldName="TIPO_MOV">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldPAIS" AreaIndex="12" Caption="PAIS" FieldName="PAIS">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldIDENTIFICADOR" AreaIndex="9" Caption="IDENTIFICADOR" FieldName="IDENTIFICADOR">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldSUBIDENTIFICADOR" AreaIndex="13" Caption="SUB IDENTIFICADOR" FieldName="SUB_IDENTIFICADOR" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldCODIGO" AreaIndex="13" Caption="CODIGO TK" FieldName="CODIGO" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldBARCODE" AreaIndex="13" Caption="BARCODE BSA" FieldName="BARCODE" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldGRADOAPI" AreaIndex="13" Caption="GRADO API" FieldName="GRADO_API" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldUTEMP" AreaIndex="13" Caption="UNID. TEMP." FieldName="UTEMP" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldTEMPERATURA" AreaIndex="13" Caption="TEMPERATURA" CellFormat-FormatType="Numeric" FieldName="TEMPERATURA" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldPRODUCTO" Area="RowArea" AreaIndex="3" Caption="PRODUCTO" FieldName="PRODUCTO">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldUMEDIDA" AreaIndex="13" Caption="UNID. MEDIDA" FieldName="UMEDIDA" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldTIPOMEDICION" AreaIndex="13" Caption="TIPO MEDICION" FieldName="TIPO_MEDICION" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldCANTIDADADICIONAL" AreaIndex="13" Caption="CANTIDAD ADIC." CellFormat-FormatType="Numeric" FieldName="CANTIDAD_ADICIONAL" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDOCNMOV" AreaIndex="13" Caption="DOC. NOTA MOV." FieldName="DOC_NMOV" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldAUTSCTRL" AreaIndex="13" Caption="AUT. SUS.CTRL." FieldName="AUT_SCTRL" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDOCPRECINTO" AreaIndex="13" Caption="DOC PRECINTOS" FieldName="DOC_PRECINTO" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDOCCALIDAD" AreaIndex="13" Caption="DOC. CALIDAD" FieldName="DOC_CALIDAD" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDOCPVUELO" AreaIndex="13" Caption="DOC. P.VUELO" FieldName="DOC_PVUELO" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDOCCETREC" AreaIndex="13" Caption="DOC. CET.RECEP." FieldName="DOC_CETREC" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDOCLOTE" AreaIndex="13" Caption="DOC. LOTE" FieldName="DOC_LOTE" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDOCVALE" AreaIndex="13" Caption="DOC. VALE" FieldName="DOC_VALE" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDOCDESP" AreaIndex="13" Caption="DOC. DESPACHO" FieldName="DOC_DESP" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldAUTANHFA" AreaIndex="13" Caption="DOC. ANH FUERA ALA" FieldName="AUT_ANHFA" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDOCMED" AreaIndex="13" Caption="DOC. MEDICION" FieldName="DOC_MED" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDOCCTA" AreaIndex="13" Caption="DOC. CUENTA" FieldName="DOC_CTA" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDOCPRTSAL" AreaIndex="13" Caption="DOC. PARTE SAL." FieldName="DOC_PRTSAL" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldCITE" AreaIndex="2" Caption="CITE" FieldName="CITE" Area="RowArea">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldFPNIT" AreaIndex="13" Caption="FACT.PROP - NIT" FieldName="FP_NIT" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldFPRS" AreaIndex="13" Caption="FACT.PROP - R.SOCIAL" FieldName="FP_RS" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldFPFP" AreaIndex="13" Caption="FACT.PROP - F. PAGO" FieldName="FP_FP" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldFPNRO" AreaIndex="13" Caption="FACT.PROP - NRO." FieldName="FP_NRO" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldFPNAUT" AreaIndex="13" Caption="FACT.PROP - NRO.AUT." FieldName="FP_NAUT" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldFPCCTRL" AreaIndex="13" Caption="FACT.PROP - COD.CTRL." FieldName="FP_CCTRL" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldFPPRECIO" AreaIndex="13" Caption="FACT.PROP - PRECIO" CellFormat-FormatType="Numeric" FieldName="FP_PRECIO" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldFPIMPORTE" AreaIndex="13" Caption="FACT.PROP - IMPORTE" CellFormat-FormatType="Numeric" FieldName="FP_IMPORTE" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldFENIT" AreaIndex="13" Caption="FACT.EXT - NIT" FieldName="FE_NIT" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldFERS" AreaIndex="13" Caption="FACT.EXT - R.SOCIAL" FieldName="FE_RS" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldFEFP" AreaIndex="13" Caption="FACT.EXT - F.PAGO" FieldName="FE_FP" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldFENRO" AreaIndex="13" Caption="FACT.EXT - NRO." FieldName="FE_NRO" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldFENAUT" AreaIndex="13" Caption="FACT.EXT - NRO.AUT." FieldName="FE_NAUT" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldFECCTRL" AreaIndex="13" Caption="FACT.EXT - COD.CTRL." FieldName="FE_CCTRL" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldFEPRECIO" AreaIndex="13" Caption="FACT.EXT - PRECIO" CellFormat-FormatType="Numeric" FieldName="FE_PRECIO" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldFEIMPORTE" AreaIndex="13" Caption="FACT.EXT - IMPORTE" CellFormat-FormatType="Numeric" FieldName="FE_IMPORTE" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldSATIPO" AreaIndex="13" Caption="SRV.ADIC. - TIPO" FieldName="SA_TIPO" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldSANRO" AreaIndex="13" Caption="SRV.ADIC. - NRO." FieldName="SA_NRO" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldSAPRECIO" AreaIndex="13" Caption="SRV.ADIC. - PRECIO" CellFormat-FormatType="Numeric" FieldName="SA_PRECIO" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldSAIMPORTE" AreaIndex="13" Caption="SRV.ADIC. - IMPORTE" CellFormat-FormatType="Numeric" FieldName="SA_IMPORTE" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldFRNIT" AreaIndex="13" Caption="FACT.REF - NIT" FieldName="FR_NIT" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldFRRS" AreaIndex="13" Caption="FACT.REF - R.SOCIAL" FieldName="FR_RS" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldFRNRO" AreaIndex="13" Caption="FACT.REF - NRO." FieldName="FR_NRO" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldFRNAUT" AreaIndex="13" Caption="FACT.REF - NRO.AUT." FieldName="FR_NAUT" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldFRCCTRL" AreaIndex="13" Caption="FACT.REF - COD.CTRL." FieldName="FR_CCTRL" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldFRPRECIO" AreaIndex="13" Caption="FACT.REF - PRECIO" CellFormat-FormatType="Numeric" FieldName="FR_PRECIO" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldFRIMPORTE" AreaIndex="13" Caption="FACT.REF - IMPORTE" CellFormat-FormatType="Numeric" FieldName="FR_IMPORTE" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldSTRMES" AreaIndex="1" Caption="MES" FieldName="STR_MES">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldIDMOVIMIENTO" AreaIndex="13" FieldName="ID_MOVIMIENTO" Visible="False">
                                                        </dx:PivotGridField>
                                                    </Fields><Styles CssPostfix="DevGlp"></Styles>
                                                    <OptionsView ShowColumnGrandTotalHeader="False" ShowColumnGrandTotals="False" />
                                                    <OptionsPager RowsPerPage="100" AlwaysShowPager="True">
                                                        <PageSizeItemSettings Items="10, 100, 1000" Visible="True" ShowAllItem="True">
                                                        </PageSizeItemSettings>
                                                    </OptionsPager>
                                                </dx:ASPxPivotGrid>
                                            </td>
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

        </td>
    </tr>
</table>

</div>








    
