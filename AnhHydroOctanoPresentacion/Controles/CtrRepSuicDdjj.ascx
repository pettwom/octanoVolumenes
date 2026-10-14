<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtrRepSuicDdjj.ascx.cs" Inherits="AnhHydroOctanoPresentacion.Controles.CtrRepSuicDdjj" %>


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
        for (var i = 0; i <= 100; i++) { }
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
                        <dx:ASPxLabel runat="server" ForeColor="#367B2F" ID="lblTitulo" Font-Bold="True" Font-Names="Rockwell" Font-Size="Larger" Text="REPORTE - SUIC ANUAL DECLARADO"></dx:ASPxLabel>
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
                                        <td colspan="3">
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
                                            <dx:ASPxLabel ID="ASPxLabel60" runat="server" ForeColor="#003300" Text="Departamento :">
                                            </dx:ASPxLabel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 20%">
                                            <dx:ASPxComboBox ID="detFechaIni" runat="server" SelectedIndex="0" Theme="Metropolis" Width="100%">
                                                <Items>
                                                    <dx:ListEditItem Text="05/2014" Value="20140501" />
                                                    <dx:ListEditItem Text="01/2015" Value="20150101" />
                                                    <dx:ListEditItem Text="01/2016" Value="20160101" />
                                                    <dx:ListEditItem Text="01/2017" Value="20170101" />
                                                    <dx:ListEditItem Selected="True" Text="01/2018" Value="20180101" />
                                                </Items>
                                                <ClearButton Visibility="Auto">
                                                </ClearButton>
                                            </dx:ASPxComboBox>

                                        </td>
                                        <td style="width: 20%">
                                            
                                            <dx:ASPxComboBox ID="detFechaFin" runat="server" SelectedIndex="0" Theme="Metropolis" Width="100%">
                                                <Items>
                                                    <dx:ListEditItem Text="12/2014" Value="20141231" />
                                                    <dx:ListEditItem Text="12/2015" Value="20151231" />
                                                    <dx:ListEditItem Text="12/2016" Value="20161231" />
                                                    <dx:ListEditItem Text="12/2017" Value="20171231" />
                                                    <dx:ListEditItem Selected="True" Text="06/2018" Value="20180630" />
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
                                            &nbsp;</td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        
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
                                        <td colspan="3">
                                            
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
                                                <dx:ASPxComboBox ID="cmbTipoExport" runat="server" ClientInstanceName="cmbTipoExport" Height="27px" SelectedIndex="0" Theme="Default" ToolTip="Seleccione el Formato del Reporte" ValueType="System.Int32" Width="100%">
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
                                                        <dx:PivotGridField ID="fieldCANTMOV" AreaIndex="6" Caption="CANTIDAD MOVIMIENTO" FieldName="CANT_MOV" Area="RowArea" TotalsVisibility="None">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldPERIODODECLARADO" AreaIndex="3" Caption="PERIODO" FieldName="PERIODO_DECLARADO" Area="RowArea">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldDEPARTAMENTO" Area="RowArea" AreaIndex="0" Caption="DEPARTAMENTO" FieldName="DEPARTAMENTO">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldESTADODECLARACION" AreaIndex="5" Caption="ESTADO" FieldName="ESTADO_DECLARACION" Area="RowArea">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldCITEGENERADO" AreaIndex="4" Caption="CITE" FieldName="CITE_GENERADO" Area="RowArea">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldIDREGISTRO" AreaIndex="0" Caption="REGISTRO" FieldName="ID_REGISTRO">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldPROYECTO" Area="RowArea" AreaIndex="1" Caption="PROYECTO" FieldName="PROYECTO">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldTIPODECLARACION" Area="RowArea" AreaIndex="2" Caption="TIPO" FieldName="TIPO_DECLARACION">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldIDREPORTEDATOPRINC" Area="DataArea" AreaIndex="0" Caption="#" CellFormat-FormatType="Numeric" FieldName="ID_REPORTE_DATO_PRINC" SummaryType="Count" TotalCellFormat-FormatType="Numeric" TotalValueFormat-FormatType="Numeric" ValueFormat-FormatType="Numeric">
                                                        </dx:PivotGridField>
                                                    </Fields>
                                                     <Styles CssPostfix="DevGlp"></Styles>
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