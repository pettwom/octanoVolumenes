<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtrRepMovSaldo.ascx.cs" Inherits="AnhHydroOctanoPresentacion.Controles.CtrRepMovSaldo" %>

<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxPivotGrid.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPivotGrid" tagprefix="dx" %>

<div class="col-md-12">
    
    <table style="width: 100%; height: 100%">
    <tr>
        <td style="text-align: center; padding-bottom: 1px">
            <dx:ASPxRoundPanel runat="server" ShowHeader="False" Width="100%" ID="pnlTitulo">
                <PanelCollection>
                    <dx:PanelContent runat="server">
                        <dx:ASPxLabel runat="server" ForeColor="#367B2F" ID="lblTitulo" Font-Bold="True" Font-Names="Rockwell" Font-Size="Larger" Text="REPORTE - SALDOS ACTUAL"></dx:ASPxLabel>
                </dx:PanelContent>
            </PanelCollection>
            <Border BorderStyle="Ridge" BorderWidth="1px" BorderColor="#003300"></Border>
            </dx:ASPxRoundPanel>
        </td>
    </tr>
    
    <tr>
        <td style="height: 100%">
            <table style="width: 100%">
                                    <tr>
                                        <td colspan="4">
                                            <legend>
                                                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Rockwell" Font-Size="Medium" ForeColor="#367B2F" Text="CARACTERISTICAS DEL REPORTE"></asp:Label>
                                            </legend>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 20px">
                                            <dx:ASPxLabel ID="ASPxLabel60" runat="server" ForeColor="#003300" Text="Departamento :">
                                            </dx:ASPxLabel>
                                        </td>
                                        <td style="padding-left: 20px">
                                            
                                        </td>
                                    </tr>
                                    <tr>                                        
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
                                        <td style="width: 20%; padding-left: 20px">
                                            <dx:ASPxButton ID="btnConsultar" runat="server" AutoPostBack="False" Font-Bold="True" ForeColor="Maroon" Text="Obtener Registros" Theme="Metropolis" Width="100%">
                                                <ClientSideEvents Click="function(s, e) {
pnlDatos.PerformCallback();
}" />
                                                <Image IconID="actions_download_16x16">
                                                </Image>
                                            </dx:ASPxButton>
                                        </td>
                                        <td style="width: 20%; padding-left: 20px">
                                            <dx:ASPxButton ID="btnActualizar" runat="server" AutoPostBack="False" Font-Bold="True" ForeColor="Maroon" Text="Actualizar Saldos Inicio Mes" Theme="Metropolis" Width="100%" OnClick="btnActualizar_Click" Visible="false">
                                               
                                                <Image IconID="actions_download_16x16">
                                                </Image>
                                            </dx:ASPxButton>
                                        </td>
                                        <td style="width: 40%; padding-left: 20px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            
                                        </td>
                                        <td colspan="2">
                                            &nbsp;</td>
                                        
                                        <td>
                                            
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
                                                <dx:ASPxButton ID="btnExportar" runat="server" Font-Bold="True" ForeColor="Maroon" OnClick="btnExportar_Click" Text="Exportar Listado a Excel" Theme="Metropolis" Width="100%">
                                                    <Image IconID="actions_converttorange_16x16">
                                                    </Image>
                                                </dx:ASPxButton>
                                            </td>
                                            <td style="width: 15%; padding-top: 10px">
                                                
                                            </td>
                                            <td style="width: 70%" colspan="5">
                                            </td>
                                    </tr>
                                        <tr>
                                            <td colspan="7">
                                                <dx:ASPxPivotGrid ID="pvgDato" ClientInstanceName="pvgDato" runat="server" ClientIDMode="AutoID" Width="100%" EnableTheming="True" Font-Size="X-Small" Theme="Default" OnHtmlFieldValuePrepared="pvgDato_HtmlFieldValuePrepared">
                                                    <Fields>
                                                        <dx:PivotGridField ID="fieldIdConsumidor" AreaIndex="0" Caption="ID_DEPARTAMENTO" FieldName="ID_CONSUMIDOR" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldEntidad" Area="RowArea" AreaIndex="1" Caption="ENTIDAD" FieldName="DENOMINACION">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldIdProducto"  AreaIndex="2" Caption="ID_ENTIDAD" FieldName="ID_PRODUCTO" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldProducto" Area="ColumnArea" AreaIndex="3" Caption="PRODUCTO" FieldName="PRODUCTO">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldSaldoInicioMes" AreaIndex="4" Caption="SALDO INICIO MES" FieldName="SALDO_INICIO_MES" Area="DataArea" CellFormat-FormatString="F2">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldIngresos" AreaIndex="5" Caption="INGRESOS" FieldName="INGRESOS" Area="DataArea" CellFormat-FormatString="F2" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldEgresos" AreaIndex="6" Caption="EGRESOS" FieldName="EGRESOS" Area="DataArea" CellFormat-FormatString="F2" Visible="False">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldSaldoActual" AreaIndex="7" Caption="SALDO ACTUAL" FieldName="SALDO_ACTUAL" Area="DataArea" CellFormat-FormatString="F2">
                                                        </dx:PivotGridField>
                                                    </Fields>
                                                    <Styles CssPostfix="DevGlp"></Styles>
                                                    <OptionsView ShowColumnGrandTotalHeader="False" ShowColumnGrandTotals="False" />
                                                    <OptionsPager RowsPerPage="9999999" AlwaysShowPager="false">                                                        
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