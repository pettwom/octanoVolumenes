<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio/ANH.Master" AutoEventWireup="true" CodeBehind="WFRepMovImpEntidad.aspx.cs" Inherits="AnhHydroOctanoPresentacion.Sitio.Movimientos.WFRepMovImpEntidad" %>
<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxPivotGrid.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPivotGrid" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="col-md-12">
    
    <table style="width: 100%; height: 100%">
    <tr>
        <td style="text-align: center; padding-bottom: 1px">
            <dx:ASPxRoundPanel runat="server" ShowHeader="False" Width="100%" ID="pnlTitulo">
                <PanelCollection>
                    <dx:PanelContent runat="server">
                        <dx:ASPxLabel runat="server" ForeColor="#367B2F" ID="lblTitulo" Font-Bold="True" Font-Names="Rockwell" Font-Size="Larger" Text="REPORTE - IMPORTACION ENTIDAD"></dx:ASPxLabel>
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
                                        <td>
                                            <dx:ASPxLabel ID="ASPxLabel57" runat="server" ForeColor="#003300" Text="Mes :">
                                            </dx:ASPxLabel>
                                        </td>
                                        <td>
                                            <dx:ASPxComboBox ID="cmbMes" runat="server" ClientInstanceName="cmbMes" Theme="Metropolis" ValueType="System.Int32" Width="140px" ToolTip="Seleccione un mes">                                    
                                    <Items>
                                        <dx:ListEditItem Text="ENERO" Value="1" />
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
                                        <td style="padding-left: 20px">
                                            <dx:ASPxLabel ID="ASPxLabel59" runat="server" ForeColor="#003300" Text="Gestion :">
                                            </dx:ASPxLabel>
                                        </td>
                                        <td style="padding-left: 20px">
                                            <dx:ASPxComboBox ID="cmbGestion" runat="server" ClientInstanceName="cmbGestion" Theme="Metropolis" ValueType="System.Int32" Width="140px" ToolTip="Seleccione un año">
                                    <Items>
                                        <dx:ListEditItem Text="2015" Value="2015" />
                                        <dx:ListEditItem Text="2016" Value="2016" />
                                        <dx:ListEditItem Text="2017" Value="2017" />
                                        <dx:ListEditItem Text="2018" Value="2018" />
                                        <dx:ListEditItem Text="2019" Value="2019" />
                                        <dx:ListEditItem Text="2020" Value="2020" />
                                        <dx:ListEditItem Text="2021" Value="2021" />
                                        <dx:ListEditItem Text="2022" Value="2022" />
                                        <dx:ListEditItem Text="2023" Value="2023" />
                                        <dx:ListEditItem Text="2024" Value="2024" />
                                        <dx:ListEditItem Text="2025" Value="2025" />
                                    </Items>
                                    <ClearButton Visibility="Auto">
                                    </ClearButton>
                                    </dx:ASPxComboBox>
                                        </td>
                                    </tr>                                    
                                    <tr>
                                        <td>
                                            
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
                                                <dx:ASPxPivotGrid ID="pvgDato" runat="server" ClientIDMode="AutoID" Width="100%" EnableTheming="True" Font-Size="X-Small" Theme="Default">
                                                    <Fields>
                                                        <dx:PivotGridField ID="fieldPROYECTO" AreaIndex="0" Caption="ID_MOVIMIENTO" FieldName="ID_MOVIMIENTO">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fielddatFechaRegistro"  Area="RowArea" AreaIndex="1" Caption="RESOLUCION" FieldName="RESOLUCION">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldPERIODO" Area="RowArea" AreaIndex="2" Caption="EMPRESA" FieldName="EMPRESA">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldstrDia" Area="RowArea" AreaIndex="3" Caption="MES" FieldName="MES">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldstrProducto" Area="RowArea" AreaIndex="4" Caption="GESTION" FieldName="GESTION">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldstrTipoInf" Area="RowArea" AreaIndex="5" Caption="ACTIVIDAD" FieldName="ACTIVIDAD">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldstrInf" Area="RowArea" AreaIndex="6" Caption="CLASIFICACION" FieldName="CLASIFICACION">
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fielddecCantidad" Area="RowArea" AreaIndex="7" Caption="MEDIDA" FieldName="MEDIDA" >
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
</asp:Content>
