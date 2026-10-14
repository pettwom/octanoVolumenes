<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtrRepDdjj.ascx.cs" Inherits="AnhHydroOctanoPresentacion.Controles.CtrRepDdjj" %>

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
                        <dx:ASPxLabel runat="server" ForeColor="#367B2F" ID="lblTitulo" Font-Bold="True" Font-Names="Rockwell" Font-Size="Larger" Text="REPORTE - CONSOLIDADO DE DECLARACIONES JURADAS"></dx:ASPxLabel>
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
                                            <dx:ASPxLabel ID="ASPxLabel61" runat="server" ForeColor="#003300" Text="Tipo Reporte :">
                                            </dx:ASPxLabel>
                                        </td>
                                        <td>
                                            <dx:ASPxLabel ID="ASPxLabel62" runat="server" ForeColor="#003300" Text="Formato Base :" Visible="False">
                                            </dx:ASPxLabel>
                                        </td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dx:ASPxComboBox ID="cmbTipoReporte" runat="server" Theme="Metropolis" Width="100%" SelectedIndex="0">
                                                <Items>
                                                    <dx:ListEditItem Text="MOVIMIENTO ACUMULADO" Value="1" Selected="True" />
                                                    <dx:ListEditItem Text="MOVIMIENTO PRECIO INTERNACIONAL" Value="2" />
                                                    <dx:ListEditItem Text="MOVIMIENTO FUERA DE ALA" Value="3" />
                                                    <dx:ListEditItem Text="MOVIMIENTO SIVETUR" Value="4" />
                                                </Items>
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
                                                <dx:ASPxPivotGrid ID="pvgAcumulado" runat="server" ClientIDMode="AutoID" Width="100%" EnableTheming="True" Font-Size="X-Small" Theme="Default">
                                                    <Fields>
                                                        <dx:PivotGridField ID="fieldPROYECTO" Area="RowArea" AreaIndex="2" Caption="PROYECTO" FieldName="PROYECTO">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fielddatFechaRegistro" Area="RowArea" AreaIndex="3" Caption="FECHA PRESENTACION" FieldName="datFechaRegistro">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldPERIODO" Area="RowArea" AreaIndex="0" Caption="PERIODO" FieldName="PERIODO">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldstrDia" AreaIndex="2" Caption="DIA" FieldName="strDia">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldstrProducto" AreaIndex="5" Caption="PRODUCTO" FieldName="strProducto" Area="RowArea">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldstrTipoInf" AreaIndex="3" Caption="TIPO INFRAESTRUCTURA" FieldName="strTipoInf">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldstrInf" AreaIndex="4" Caption="INFRAESTRUCTURA" FieldName="strInf">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldstrTipoOp" AreaIndex="1" Caption="TIPO OPERACION" FieldName="strTipoOp" Area="ColumnArea" CellFormat-FormatType="Numeric" TotalsVisibility="None">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fielddecCantidad" AreaIndex="0" Caption="CANTIDAD" FieldName="decCantidad" Area="DataArea" CellFormat-FormatType="Numeric" TotalsVisibility="None">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDEPTO" AreaIndex="1" Caption="DEPARTAMENTO" FieldName="DEPTO" Area="RowArea">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldCITE" AreaIndex="4" Caption="CITE" FieldName="CITE" Area="RowArea">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldstrFactorTipo" AreaIndex="0" Caption="GRUPO TI. OP." FieldName="strFactorTipo" Area="ColumnArea">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldstrGestion" AreaIndex="0" Caption="GESTION" FieldName="strGestion">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldstrMes" AreaIndex="1" Caption="MES" FieldName="strMes">
                                                        </dx:PivotGridField>
                                                    </Fields>
                                                    <Styles CssPostfix="DevGlp"></Styles>
                                                    <OptionsView ShowColumnGrandTotalHeader="False" ShowColumnGrandTotals="False" />
                                                    <OptionsPager RowsPerPage="100" AlwaysShowPager="True">
                                                        <PageSizeItemSettings Items="10, 100, 1000" Visible="True" ShowAllItem="True">
                                                        </PageSizeItemSettings>
                                                    </OptionsPager>
                                                </dx:ASPxPivotGrid>
                                                <dx:ASPxPivotGrid ID="pvgInternacional" runat="server" ClientIDMode="AutoID" EnableTheming="True" Font-Size="X-Small" Theme="Default" Width="100%">
                                                    <Fields>
                                                        <dx:PivotGridField ID="fieldPROYECTO0" AreaIndex="4" Caption="PROYECTO" FieldName="PROYECTO">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fielddatFechaRegistro0" AreaIndex="5" Caption="FECHA PRESENTACION" FieldName="datFechaRegistro">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldPERIODO0" Area="RowArea" AreaIndex="0" Caption="PERIODO" FieldName="PERIODO">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldstrDia0" AreaIndex="2" Caption="DIA" FieldName="strDia">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldstrProducto0" Area="ColumnArea" AreaIndex="0" Caption="PRODUCTO" FieldName="strProducto">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldstrPais" AreaIndex="3" Caption="PAIS" FieldName="strPais">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldstrIdentificador" AreaIndex="7" Caption="IDENTIFICADOR" FieldName="strIdentificador">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fielddecCantidad0" Area="DataArea" AreaIndex="0" Caption="CANTIDAD" CellFormat-FormatType="Numeric" FieldName="decCantidad">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDEPTO0" Area="RowArea" AreaIndex="1" Caption="DEPARTAMENTO" FieldName="DEPTO">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldCITE0" AreaIndex="6" Caption="CITE" FieldName="CITE">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldstrGestion0" AreaIndex="0" Caption="GESTION" FieldName="strGestion">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldstrMes0" AreaIndex="1" Caption="MES" FieldName="strMes">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldstrCliente" AreaIndex="8" Caption="CLIENTE" FieldName="strCliente">
                                                        </dx:PivotGridField>
                                                    </Fields>
                                                    <Styles CssPostfix="DevGlp"></Styles>
                                                    <OptionsPager AlwaysShowPager="True" RowsPerPage="100">
                                                        <PageSizeItemSettings Items="10, 100, 1000" ShowAllItem="True" Visible="True">
                                                        </PageSizeItemSettings>
                                                    </OptionsPager>
                                                </dx:ASPxPivotGrid>
                                                <dx:ASPxPivotGrid ID="pvgFueraAla" runat="server" ClientIDMode="AutoID" EnableTheming="True" Font-Size="X-Small" Theme="Default" Width="100%">
                                                    <Fields>
                                                        <dx:PivotGridField ID="fieldPROYECTO1" AreaIndex="3" Caption="PROYECTO" FieldName="PROYECTO">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fielddatFechaRegistro1" AreaIndex="4" Caption="FECHA PRESENTACION" FieldName="datFechaRegistro">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldPERIODO1" Area="RowArea" AreaIndex="0" Caption="PERIODO" FieldName="PERIODO">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldstrDia1" AreaIndex="2" Caption="DIA" FieldName="strDia">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldstrProducto1" Area="ColumnArea" AreaIndex="0" Caption="PRODUCTO" FieldName="strProducto">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldstrIdentificador0" AreaIndex="6" Caption="IDENTIFICADOR" FieldName="strIdentificador">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fielddecCantidad1" Area="DataArea" AreaIndex="0" Caption="CANTIDAD" CellFormat-FormatType="Numeric" FieldName="decCantidad">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDEPTO1" Area="RowArea" AreaIndex="1" Caption="DEPARTAMENTO" FieldName="DEPTO">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldCITE1" AreaIndex="5" Caption="CITE" FieldName="CITE">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldstrGestion1" AreaIndex="0" Caption="GESTION" FieldName="strGestion">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldstrMes1" AreaIndex="1" Caption="MES" FieldName="strMes">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldstrCliente0" AreaIndex="7" Caption="CLIENTE" FieldName="strCliente">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldstrNroSustCtrl" AreaIndex="8" Caption="NRO. SUST.CTRL." FieldName="strNroSustCtrl">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldstrTipoEnvase" AreaIndex="9" Caption="TIPO ENVASE" FieldName="strTipoEnvase">
                                                        </dx:PivotGridField>
                                                    </Fields>
                                                    <Styles CssPostfix="DevGlp"></Styles>
                                                    <OptionsPager AlwaysShowPager="True" RowsPerPage="100">
                                                        <PageSizeItemSettings Items="10, 100, 1000" ShowAllItem="True" Visible="True">
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








    
