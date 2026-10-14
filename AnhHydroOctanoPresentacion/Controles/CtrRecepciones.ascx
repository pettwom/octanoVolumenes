<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtrRecepciones.ascx.cs" Inherits="AnhHydroOctanoPresentacion.Controles.CtrRecepciones" %>

<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>

<%@ Register src="CtrFecheroMes.ascx" tagname="CtrFecheroMes" tagprefix="uc2" %>


<style type="text/css">
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

<div class="col-md-12">
    <table style="width: 100%">
    <tr>
        <td style="text-align: center; padding-bottom: 1px">
            <dx:ASPxRoundPanel runat="server" ShowHeader="False" Width="100%" ID="pnlTitulo">
                <PanelCollection>
                    <dx:PanelContent runat="server">
                        <dx:ASPxLabel runat="server" ForeColor="#367B2F" ID="lblTitulo" Font-Bold="True" Font-Names="Rockwell" Font-Size="Larger" Text="REPORTE DE RECEPCIONES FACTURADAS"></dx:ASPxLabel>
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
                            <td>Fecha Inicial :<dx:ASPxDateEdit ID="detFechaIni" runat="server" Theme="Metropolis" ClientInstanceName="detFechaIni" Width="140px">
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
                            <td>
                                Fecha Final :<dx:ASPxDateEdit ID="detFechaFin" runat="server" Theme="Metropolis" ClientInstanceName="detFechaFin" Width="140px">
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
                                Selección Directa de Mes :
                                <uc2:CtrFecheroMes ID="cfmMes" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-bottom: 10px">
                                <dx:ASPxButton ID="btnConsulta" runat="server" AutoPostBack="False" Font-Bold="True" ForeColor="Maroon" Text="Obtener Información" Theme="Metropolis" Width="57%">
                                    <ClientSideEvents Click="function(s, e) {
	pnlDatos.PerformCallback();
}" />
                                    <Image IconID="actions_download_16x16">
                                    </Image>
                                </dx:ASPxButton>
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
                                <dx:ASPxTreeList ID="tlsOperadores" runat="server" AutoGenerateColumns="False" ClientInstanceName="tlsOperadores" Font-Size="Smaller" Height="187px" KeyFieldName="intId" ParentFieldName="intIdPadre" Theme="Youthful" Styles-Header-BackColor="#317D40" ToolTip="Seleccione una actividad">
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
                                </dx:ASPxTreeList>
                            </td>
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
                                        <td colspan ="7">
                                            <legend>
                                                <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="Rockwell" Font-Size="Medium" ForeColor="#367B2F" Text="DATOS DEL PROYECTO"></asp:Label>
                                            </legend>
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="#10420F" Text="NOMBRE COMERCIAL"></asp:Label>
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
                                            <asp:Label ID="Label6" runat="server" Font-Bold="True" ForeColor="#10420F" Text="DIRECCION"></asp:Label>
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
                                         <td colspan="2">
                                            <asp:Label ID="lblDireccion" runat="server"></asp:Label>
                                         </td>
                                     </tr>

                                    <tr>
                                        <td colspan="7" style="width: 100%">
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="width: 70%">
                                                        <dx:ASPxLabel ID="ASPxLabel339" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="Smaller" ForeColor="#FF8800" Width="100%">
                                                            <BorderBottom BorderStyle="Solid" />
                                                        </dx:ASPxLabel>
                                                    </td>
                                                    <td style="width: 15%">
                                                        <dx:ASPxComboBox ID="cmbTipoExport" runat="server" ClientInstanceName="cmbTipoExport" Height="27px" SelectedIndex="0" Theme="Metropolis" ToolTip="Seleccione el Formato del Reporte" ValueType="System.Int32" Width="100%">
                                                            <Items>
                                                                <dx:ListEditItem Selected="True" Text="EXCEL" Value="1" />
                                                                <dx:ListEditItem Text="PDF" Value="2" />
                                                            </Items>
                                                            <ClearButton Visibility="Auto">
                                                            </ClearButton>
                                                        </dx:ASPxComboBox>
                                                    </td>
                                                    <td style="text-align: right; width: 15%">
                                                        <dx:ASPxButton ID="btnEmitir" runat="server" Font-Bold="True" ForeColor="Maroon" OnClick="btnEmitir_Click" Text="Emitir Reporte" Theme="Metropolis" Width="100%">
                                                            <Image IconID="arrange_withtextwrapping_topleft_16x16">
                                                            </Image>
                                                        </dx:ASPxButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="7" style="width: 80%">
                                            <dx:ASPxGridView ID="dgvDatos" runat="server" AutoGenerateColumns="False" EnableTheming="True" Font-Size="Smaller" OnHtmlRowPrepared="dgvDatos_HtmlRowPrepared" Theme="Default" Width="100%">
                                                <SettingsPager NumericButtonCount="2">
                                                    <PageSizeItemSettings Items="50, 100, 200, 500, 1000" Visible="True">
                                                </PageSizeItemSettings>
                                                </SettingsPager>
                                                <Settings ShowFilterRow="True" ShowFooter="True" ShowGroupPanel="True" ShowHeaderFilterButton="True" />
                                                <SettingsBehavior AllowFocusedRow="True" />
                                                <Columns>
                                                    <dx:GridViewCommandColumn ShowClearFilterButton="True" ShowInCustomizationForm="True" Visible="False" VisibleIndex="0">
                                                    </dx:GridViewCommandColumn>
                                                    <dx:GridViewDataTextColumn Caption="ID_MOVIMIENTO" FieldName="ID_MOVIMIENTO" ShowInCustomizationForm="True" Visible="False" VisibleIndex="1">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="FECHA_OPERACION" FieldName="FECHA_OPERACION" ShowInCustomizationForm="True" Visible="False" VisibleIndex="2">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="IDENTIFICADOR" FieldName="IDENTIFICADOR" ShowInCustomizationForm="True" VisibleIndex="9">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="TIPO FACTURA" FieldName="TIPO_TRANSACCION" ShowInCustomizationForm="True" ToolTip="Tipo de Documento de Factura" VisibleIndex="5" Visible="False">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="FECHA_HORA_EMISION" FieldName="FECHA_HORA_EMISION" ShowInCustomizationForm="True" Visible="False" VisibleIndex="6">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="NO. FACTURA" FieldName="NRO_TRANSACCION" ShowInCustomizationForm="True" ToolTip="Número de Factura" VisibleIndex="11">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="NO. AUTORIZACION" FieldName="NRO_AUTORIZACION" ShowInCustomizationForm="True" ToolTip="Número de Autorización" VisibleIndex="10">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="COD. CONTROL" FieldName="CODIGO_CONTROL" ShowInCustomizationForm="True" ToolTip="Código de Control" VisibleIndex="12">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="NIT" FieldName="NIT_EMISOR" ShowInCustomizationForm="True" VisibleIndex="7">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="RAZON SOCIAL" FieldName="RAZON_SOCIAL" ShowInCustomizationForm="True" VisibleIndex="8">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="ID_ESTADO_MOVIMIENTO" FieldName="ID_ESTADO_MOVIMIENTO" ShowInCustomizationForm="True" Visible="False" VisibleIndex="16">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="ID_HIDROCARBURO" FieldName="ID_HIDROCARBURO" ShowInCustomizationForm="True" Visible="False" VisibleIndex="17">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="PRODUCTO" FieldName="PRODUCTO" ShowInCustomizationForm="True" VisibleIndex="18">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="CANTIDAD" FieldName="CANTIDAD" ShowInCustomizationForm="True" VisibleIndex="19">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="PRECIO" FieldName="PRECIO_UNITARIO" ShowInCustomizationForm="True" VisibleIndex="20">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="IMPORTE" FieldName="PRECIO_TOTAL" ShowInCustomizationForm="True" VisibleIndex="21">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="NO. DESPACHO" FieldName="NRO_DESPACHO" ShowInCustomizationForm="True" ToolTip="Número de despacho" VisibleIndex="13">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="PRECINTOS" FieldName="PRECINTOS" ShowInCustomizationForm="True" VisibleIndex="14">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="FECHA RECEP." FieldName="STR_FECHA_OPERACION" ShowInCustomizationForm="True" VisibleIndex="3" ToolTip="Fecha de Recepción">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="FECHA EMISION" FieldName="STR_FECHA_HORA_EMISION" ShowInCustomizationForm="True" VisibleIndex="4">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="EST." FieldName="STR_ESTADO_MOVIMIENTO" ShowInCustomizationForm="True" VisibleIndex="15">
                                                    </dx:GridViewDataTextColumn>
                                                </Columns>
                                                <Styles CssPostfix="DevGlp"><AlternatingRow BackColor="#EEEEEE">
                                                        </AlternatingRow></Styles>
                                                <TotalSummary>
                                                    <dx:ASPxSummaryItem DisplayFormat="{0}" FieldName="CANTIDAD" ShowInColumn="CANTIDAD" SummaryType="Sum" Tag="can" />
                                                    <dx:ASPxSummaryItem DisplayFormat="{0}" FieldName="PRECIO_TOTAL" ShowInColumn="PRECIO_TOTAL" SummaryType="Sum" Tag="pre" />
                                                </TotalSummary>
                                            </dx:ASPxGridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="width: 80%">
                                            <dx:ASPxGridViewExporter ID="dgeExportar" runat="server">
                                            </dx:ASPxGridViewExporter>
                                        </td>
                                        <td colspan="2" style="width: 1%">&nbsp;</td>
                                        <td style="text-align: right; width: 19%">&nbsp;</td>
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










