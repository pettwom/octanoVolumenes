<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtrImportadores.ascx.cs" Inherits="AnhHydroOctanoPresentacion.Controles.CtrImportadores" %>

<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxPivotGrid.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPivotGrid" tagprefix="dx" %>

<div class="col-md-12">
    <table style="width: 100%">
        <tr>
            <td style="text-align: center; padding-bottom: 1px">
                <dx:ASPxRoundPanel runat="server" ShowHeader="False" Width="100%" ID="pnlTitulo">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <dx:ASPxLabel runat="server" ForeColor="#367B2F" ID="lblTitulo" Font-Bold="True" Font-Names="Rockwell" Font-Size="Larger" Text="FORMULARIOS DE IMPORTACION"></dx:ASPxLabel>
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
                                    <dx:ASPxCallbackPanel ID="pnlCabecera" runat="server" Width="160px" ClientInstanceName="pnlCabecera">
                                        <PanelCollection>
                                            <dx:PanelContent runat="server">
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td>                                                        
                                                            <dx:ASPxLabel ID="ASPxLabel339" runat="server" ForeColor="#003300" Text="Fecha :">
                                                            </dx:ASPxLabel>
                                                            <dx:ASPxDateEdit ID="detFechaIni" runat="server" ClientInstanceName="detFechaIni" Theme="Metropolis" ToolTip="Seleccione la fecha inicial" Width="140px">
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
                                                            <dx:ASPxComboBox ID="cmbResMin" ClientInstanceName="cmbResMin" runat="server" SelectedIndex="1" Theme="Metropolis" Width="100%">
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
                                                                    <dx:PanelContent runat="server">
                                                                        <dx:ASPxComboBox ID="cmbListadoDui" ClientInstanceName="cmbListadoDui" runat="server" Theme="Metropolis" Width="100%" ClientVisible="false" TextField="NRO_DUI" ValueField="DUI">                                                                            
                                                                        </dx:ASPxComboBox>
                                                                    </dx:PanelContent>
                                                                </PanelCollection>
                                                            </dx:ASPxCallbackPanel>
                                                        </td>
                                                    </tr>                                                   

                                                    <tr>
                                                        <td style="padding-bottom: 5px">
                                                            <dx:ASPxButton ID="btnConsultar" runat="server" AutoPostBack="False" Font-Bold="True" ForeColor="Maroon" Text="Obtener Información" Theme="Metropolis" Width="55%">
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
                                        <ClientSideEvents LostFocus="function(s, e) {
	tlsOperadores.PerformCallback();
}" />
                                        <BorderTop BorderColor="#FF6600" BorderStyle="Solid" />
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <dx:ASPxTreeList ID="tlsOperadores" runat="server" AutoGenerateColumns="False" KeyFieldName="intId" ClientInstanceName="tlsOperadores" Font-Size="Smaller" Height="187px"  ParentFieldName="intIdPadre" Theme="Youthful" Styles-Header-BackColor="#317D40" ToolTip="Seleccione un Proyecto" OnCustomCallback="tlsOperadores_CustomCallback">
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
//tacMovimientos.SetActiveTabIndex(0);
//pvgAcumulado.SetVisible(true);
//dgvInternacional.SetVisible(false);
//dgvContenedor.SetVisible(false);
//cmbPeriodo.SetText('');

//pnlDatos.PerformCallback();
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
                                            <td style="text-align: right; width: 10%;">
                                                <dx:ASPxButton ID="btnReimprimir" runat="server" Font-Bold="True" ForeColor="Maroon" Text="Imprimir Reporte" Theme="Metropolis" Width="100%" ClientVisible="false" OnClick="btnReimprimir_Click">
                                                    <Image IconID="print_printer_16x16">
                                                    </Image>
                                                </dx:ASPxButton>
                                            </td>
                                        </tr>                                                                              
                                        <tr>
                                            <td colspan="3">
                                                <dx:ASPxPageControl ID="pgcOpciones" runat="server" ActiveTabIndex="0" Theme="Metropolis" Width="100%">
                                                    <TabPages>
                                                        
                                                        <dx:TabPage Text="FORMULARIO 1">
                                                            <ContentCollection>
                                                                <dx:ContentControl runat="server">
                                                                    <table style="width: 100%">
                                                                        <tr>
                                                                            <td style="padding-bottom:10px; text-align:center">
                                                                                <dx:ASPxLabel runat="server" Text="DECLARACION JURADA DE SUBVENCION DE COSTOS POR LA OBTENCION DE GASOLINA BLANCA" Font-Bold="True" Font-Names="Rockwell" ForeColor="#487B5C">
                                                                                    <BorderBottom BorderStyle="Solid" />
                                                                                </dx:ASPxLabel>
                                                                            </td>                                                                            
                                                                        </tr>  
                                                                        <tr>
                                                                            <td style="padding-bottom: 5px">
                                                                                <dx:ASPxLabel runat="server" Text="RUBRO 1: IDENTIFICACION DEL SOLICITANTES" Width="100%" style="padding-bottom: 5px" Font-Bold="True" Font-Names="Rockwell" ForeColor="#487B5C">
                                                                                    <BorderBottom BorderStyle="Solid" />
                                                                                </dx:ASPxLabel>
                                                                            </td>
                                                                        </tr>                                                               
                                                                        <tr>
                                                                            <td >
                                                                                <dx:ASPxLabel id="lblNombreEmpresa" runat="server" Text="a) Nombre de la Empresa:" Width="100%"></dx:ASPxLabel>
                                                                            </td>
                                                                            <td >

                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-bottom: 5px; padding-top: 5px">
                                                                                <dx:ASPxLabel runat="server" Text="RUBRO 2: MONTO A SUBVENCIONAR" Width="100%" style="padding-bottom: 5px" Font-Bold="True" Font-Names="Rockwell" ForeColor="#487B5C">
                                                                                    <BorderBottom BorderStyle="Solid" />
                                                                                </dx:ASPxLabel>
                                                                            </td>
                                                                        </tr>      
                                                                        <tr>
                                                                            <td>
                                                                                <dx:ASPxLabel ID="lblMontoCompensar" runat="server" Text="a) Monto total solicitado para compensar:" Width="100%"></dx:ASPxLabel>                                                                                
                                                                            </td>
                                                                            <td>

                                                                            </td>
                                                                        </tr>                                                                        
                                                                        <tr>
                                                                            <td style="padding-bottom: 5px; padding-top: 5px">
                                                                                <dx:ASPxLabel runat="server" Text="RUBRO 3: COSTOS EROGADOS - ITEMS (Bs):" Width="100%" style="padding-bottom: 5px" Font-Bold="True" Font-Names="Rockwell" ForeColor="#487B5C">
                                                                                    <BorderBottom BorderStyle="Solid" />
                                                                                </dx:ASPxLabel>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-bottom: 5px; padding-top: 5px">
                                                                                <dx:ASPxGridView ID="dgvCostosErogados" runat="server" Theme="Default" AutoGenerateColumns="False" EnableTheming="True" Font-Size="Smaller" ClientInstanceName="dgvTransacciones" Width="100%" >
                                                                                    <SettingsContextMenu EnableRowMenu="False">
                                                                                    </SettingsContextMenu>
                                                                                    <SettingsPager PageSize="100">
                                                                                        <PageSizeItemSettings Items="50, 100, 200, 500, 1000" Visible="True">
                                                                                        </PageSizeItemSettings>
                                                                                    </SettingsPager>
                                                                                    <SettingsEditing Mode="PopupEditForm">
                                                                                    </SettingsEditing>
                                                                                    <Settings ShowFooter="True" ShowGroupFooter="VisibleAlways" ShowGroupPanel="True" ShowHeaderFilterButton="True" />
                                                                                    <SettingsBehavior AllowFocusedRow="True" />
                                                                                    <SettingsPopup>
                                                                                        <EditForm Modal="True" />
                                                                                    </SettingsPopup>
                                                                                    <SettingsText CommandNew="Nuevo" />
                                                                                    <Columns>
                                                                                        <dx:GridViewDataTextColumn Caption="CONCEPTO" FieldName="RON" ShowInCustomizationForm="True" VisibleIndex="0" ToolTip="Concepto">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="COSTO DE PRODUCTO" FieldName="A" ShowInCustomizationForm="True" VisibleIndex="1" ToolTip="Costo de Producto" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" /> 
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="COSTO DE TRANSPORTE" FieldName="B" ShowInCustomizationForm="True" VisibleIndex="2" ToolTip="Costo de Transporte" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="COSTO DE SEGURO" FieldName="C" ShowInCustomizationForm="True" VisibleIndex="3" ToolTip="Costo de Seguro" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="COSTO DE INSPECCION" FieldName="D" ShowInCustomizationForm="True" VisibleIndex="4" ToolTip="Costo de Inspeccion" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="COSTOS ADUANEROS" FieldName="E" ShowInCustomizationForm="True" VisibleIndex="5" ToolTip="Costo Aduanero" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="COSTOS FINANCIEROS" FieldName="F" ShowInCustomizationForm="True" VisibleIndex="6" ToolTip="Costo Financiero" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="COSTOS DE ALMACENAJE" FieldName="G" ShowInCustomizationForm="True" VisibleIndex="7" ToolTip="Costo de Almacenaje" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="COSTOS DE MONITOREO" FieldName="H" ShowInCustomizationForm="True" VisibleIndex="8" ToolTip="Costo de Monitoreo" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="TOTAL COSTOS" FieldName="I" ShowInCustomizationForm="True" VisibleIndex="9" ToolTip="Costo Total" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>                                                                                        
                                                                                    </Columns>
                                                                                    <TotalSummary>
                                                                                            <dx:ASPxSummaryItem FieldName="I" ShowInColumn="I" SummaryType="Sum" Tag="I" DisplayFormat="F2" />
                                                                                    </TotalSummary>
                                                                                    <Styles CssPostfix="DevGlp"><AlternatingRow BackColor="#EEEEEE">
                                                                                        </AlternatingRow></Styles>
                                                                                </dx:ASPxGridView>
                                                                                <dx:ASPxGridView ID="dgvCostosErogadosGb" runat="server" Theme="Default" AutoGenerateColumns="False" EnableTheming="True" Font-Size="Smaller" ClientInstanceName="dgvCostosErogadosGb" ClientVisible="false" Width="100%" >
                                                                                    <SettingsContextMenu EnableRowMenu="False">
                                                                                    </SettingsContextMenu>                                                                                    
                                                                                    <SettingsEditing Mode="PopupEditForm">
                                                                                    </SettingsEditing>
                                                                                    <Settings ShowFooter="True" ShowGroupFooter="VisibleAlways" ShowGroupPanel="True" ShowHeaderFilterButton="True" />
                                                                                    <SettingsBehavior AllowFocusedRow="True" />
                                                                                    <SettingsPopup>
                                                                                        <EditForm Modal="True" />
                                                                                    </SettingsPopup>
                                                                                    <SettingsText CommandNew="Nuevo" />
                                                                                    <Columns>
                                                                                        <dx:GridViewDataTextColumn Caption="VGBASE (lt)" FieldName="DATO_1" ShowInCustomizationForm="True" VisibleIndex="0" ToolTip="Concepto" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="CTGBASE (Bs/lt)" FieldName="DATO_2" ShowInCustomizationForm="True" VisibleIndex="1" ToolTip="Costo de Producto" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" /> 
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="PPT (Bs/lt)" FieldName="DATO_3" ShowInCustomizationForm="True" VisibleIndex="2" ToolTip="Costo de Transporte" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="Subvención (Bs)" FieldName="DATO_4" ShowInCustomizationForm="True" VisibleIndex="3" ToolTip="Costo de Seguro" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>                                                                                                                                                                                
                                                                                    </Columns>
                                                                                    <TotalSummary>
                                                                                            <dx:ASPxSummaryItem FieldName="DATO_4" ShowInColumn="DATO_4" SummaryType="Sum" Tag="DATO_4" DisplayFormat="F2" />
                                                                                    </TotalSummary>
                                                                                    <Styles CssPostfix="DevGlp"><AlternatingRow BackColor="#EEEEEE">
                                                                                        </AlternatingRow></Styles>
                                                                                </dx:ASPxGridView>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-bottom: 5px; padding-top: 5px">
                                                                                <dx:ASPxLabel runat="server" Text="RUBRO 4: VOLUMEN IMPORTADO Vimp (Lts.):" Width="100%" style="padding-bottom: 5px" Font-Bold="True" Font-Names="Rockwell" ForeColor="#487B5C">
                                                                                    <BorderBottom BorderStyle="Solid" />
                                                                                </dx:ASPxLabel>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-bottom: 5px; padding-top: 5px">
                                                                                <dx:ASPxGridView ID="dgvVolImportado" runat="server" Theme="Default" AutoGenerateColumns="False"  EnableTheming="True" Font-Size="Smaller" ClientInstanceName="dgvTransacciones" Width="100%" >
                                                                                    <SettingsContextMenu EnableRowMenu="False">
                                                                                    </SettingsContextMenu>
                                                                                    <SettingsPager PageSize="100">
                                                                                        <PageSizeItemSettings Items="50, 100, 200, 500, 1000" Visible="True">
                                                                                        </PageSizeItemSettings>
                                                                                    </SettingsPager>
                                                                                    <SettingsEditing Mode="PopupEditForm">
                                                                                    </SettingsEditing>
                                                                                    <Settings ShowFooter="True" ShowGroupFooter="VisibleAlways" ShowGroupPanel="True" ShowHeaderFilterButton="True" />
                                                                                    <SettingsBehavior AllowFocusedRow="True" />
                                                                                    <SettingsPopup>
                                                                                        <EditForm Modal="True" />
                                                                                    </SettingsPopup>
                                                                                    <SettingsText CommandNew="Nuevo" />
                                                                                    <Columns>
                                                                                        <dx:GridViewDataTextColumn Caption="CONCEPTO" FieldName="RON" ShowInCustomizationForm="True" VisibleIndex="0" ToolTip="Concepto">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="DESTINO FINAL" FieldName="VOLUMEN" ShowInCustomizationForm="True" VisibleIndex="1" ToolTip="Destino Final" PropertiesTextEdit-DisplayFormatString="F">
                                                                                            <Settings AutoFilterCondition="Contains" /> 
                                                                                        </dx:GridViewDataTextColumn>                                                           
                                                                                    </Columns>
                                                                                    <TotalSummary>
                                                                                            <dx:ASPxSummaryItem FieldName="VOLUMEN" ShowInColumn="VOLUMEN" SummaryType="Sum" Tag="VOLUMEN" DisplayFormat="{0}" />
                                                                                    </TotalSummary>
                                                                                    <Styles CssPostfix="DevGlp"><AlternatingRow BackColor="#EEEEEE">
                                                                                        </AlternatingRow></Styles>
                                                                                </dx:ASPxGridView>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-bottom: 5px; padding-top: 5px">
                                                                                <dx:ASPxLabel runat="server" Text="RUBRO 5: DECLARACIÓN:" Width="100%" style="padding-bottom: 5px" Font-Bold="True" Font-Names="Rockwell" ForeColor="#487B5C">
                                                                                    <BorderBottom BorderStyle="Solid" />
                                                                                </dx:ASPxLabel>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <dx:ASPxCheckBox ID="chbYacimiento" runat="server"></dx:ASPxCheckBox>
                                                                                <dx:ASPxLabel runat="server" Text="Yacimientos Petrolíferos Fiscales Bolivianos (YPFB)" ></dx:ASPxLabel>
                                                                            </td>                                                                            
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <dx:ASPxCheckBox ID="chbLogistica" runat="server"></dx:ASPxCheckBox>                                                                                
                                                                                <dx:ASPxLabel runat="server" Text="Yacimientos Petrolíferos Fiscales Bolivianos (YPFB) - Refinación SA" ></dx:ASPxLabel>
                                                                            </td>                                                                            
                                                                        </tr>
                                                                    </table>
                                                                </dx:ContentControl>
                                                            </ContentCollection>
                                                        </dx:TabPage> 
                                                        
                                                        <dx:TabPage Text="FORMULARIO 2">
                                                            <ContentCollection>
                                                                <dx:ContentControl runat="server">
                                                                    <table style="width: 100%">
                                                                        <tr>
                                                                            <td style="padding-bottom:10px; text-align:center" colspan="4">
                                                                                <dx:ASPxLabel runat="server" Text="RESUMEN CALCULO DE SUBVENCION" Font-Bold="True" Font-Names="Rockwell" ForeColor="#487B5C">
                                                                                    <BorderBottom BorderStyle="Solid" />
                                                                                </dx:ASPxLabel>
                                                                            </td>                                                                            
                                                                        </tr>  
                                                                        <tr>
                                                                            <td style="padding-bottom: 5px" colspan="4">
                                                                                <dx:ASPxLabel runat="server" Text="RUBRO 1: DATOS GENERALES:" Width="100%" style="padding-bottom: 5px" Font-Bold="True" Font-Names="Rockwell" ForeColor="#487B5C">
                                                                                    <BorderBottom BorderStyle="Solid" />
                                                                                </dx:ASPxLabel>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 25%; text-align: right;">
                                                                                <dx:ASPxLabel ID="lblForm2Dui" ClientInstanceName="lblForm2Dui" runat="server" Font-Bold="True" ForeColor="#666666" Text="Nro DUI Regularizada:">
                                                                                </dx:ASPxLabel>
                                                                            </td>
                                                                            <td style="width: 25%;">
                                                                                <dx:ASPxTextBox ID="txtDuiRegularizadaF2" ClientInstanceName="txtDuiRegularizadaF2" runat="server" Width="260px">
                                                                                    <ReadOnlyStyle BackColor="#DEE8DB">
                                                                                    </ReadOnlyStyle>
                                                                                    <FocusedStyle BackColor="#FFFFCC">
                                                                                    </FocusedStyle>
                                                                                    <DisabledStyle BackColor="#DEE8DB">
                                                                                    </DisabledStyle>
                                                                                </dx:ASPxTextBox>
                                                                            </td>
                                                                            <td style="width: 25%; text-align: right;">
                                                                                <dx:ASPxLabel ID="lblForm2FechaDui" ClientInstanceName="lblForm2FechaDui" runat="server" Font-Bold="True" ForeColor="#666666" Text="Fecha DUI :" Width="150px">
                                                                                </dx:ASPxLabel>
                                                                            </td>
                                                                            <td style="width: 25%;">
                                                                                <dx:ASPxTextBox ID="txtFechaDuiF2" ClientInstanceName="txtFechaDuiF2" runat="server" Width="260px">
                                                                                    <ReadOnlyStyle BackColor="#DEE8DB">
                                                                                    </ReadOnlyStyle>
                                                                                    <FocusedStyle BackColor="#FFFFCC">
                                                                                    </FocusedStyle>
                                                                                    <DisabledStyle BackColor="#DEE8DB">
                                                                                    </DisabledStyle>
                                                                                </dx:ASPxTextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 25%; text-align: right;">
                                                                                <dx:ASPxLabel ID="lblForms2FronIngr" ClientInstanceName="lblForms2FronIngr" runat="server" Font-Bold="True" ForeColor="#666666" Text="Frontera de Ingreso:">
                                                                                </dx:ASPxLabel>
                                                                            </td>
                                                                            <td style="width: 25%;">
                                                                                <dx:ASPxTextBox ID="txtFronteraIngresoF2" ClientInstanceName="txtFronteraIngresoF2" runat="server" Width="260px">
                                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                                </ReadOnlyStyle>
                                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                                </FocusedStyle>
                                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                                </DisabledStyle>
                                                                            </dx:ASPxTextBox>
                                                                            </td>
                                                                            <td style="width: 25%; text-align: right;">
                                                                                <dx:ASPxLabel ID="lblForm2Incoterms" ClientInstanceName="lblForm2Incoterms" runat="server" Font-Bold="True" ForeColor="#666666" Text="INCOTERMS :" Width="150px">
                                                                                </dx:ASPxLabel>
                                                                            </td>
                                                                            <td style="width: 25%;">
                                                                                <dx:ASPxTextBox ID="txtIncontermsF2" ClientInstanceName="txtIncontermsF2" runat="server" Width="260px">
                                                                                    <ReadOnlyStyle BackColor="#DEE8DB">
                                                                                    </ReadOnlyStyle>
                                                                                    <FocusedStyle BackColor="#FFFFCC">
                                                                                    </FocusedStyle>
                                                                                    <DisabledStyle BackColor="#DEE8DB">
                                                                                    </DisabledStyle>
                                                                                </dx:ASPxTextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 25%; text-align: right;">
                                                                                <dx:ASPxLabel ID="lblForm2MesRecibido" ClientInstanceName="lblForm2MesRecibido" runat="server" ClientVisible="false" Font-Bold="True" ForeColor="#666666" Text="Mes Recibido:">
                                                                                </dx:ASPxLabel>
                                                                            </td>
                                                                            <td style="width: 25%;">
                                                                                <dx:ASPxTextBox ID="txtMesRecibido" ClientInstanceName="txtMesRecibido" runat="server" Width="260px" ClientVisible="false">
                                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                                </ReadOnlyStyle>
                                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                                </FocusedStyle>
                                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                                </DisabledStyle>
                                                                            </dx:ASPxTextBox>
                                                                            </td>
                                                                            <td style="width: 25%; text-align: right;">
                                                                                <dx:ASPxLabel ID="lblForm2Sector" ClientInstanceName="lblForm2Sector" runat="server" ClientVisible="false" Font-Bold="True" ForeColor="#666666" Text="Sector :" Width="150px">
                                                                                </dx:ASPxLabel>
                                                                            </td>
                                                                            <td style="width: 25%;">
                                                                                <dx:ASPxTextBox ID="txtSector" ClientInstanceName="txtSector" runat="server" Width="260px" ClientVisible="false">
                                                                                    <ReadOnlyStyle BackColor="#DEE8DB">
                                                                                    </ReadOnlyStyle>
                                                                                    <FocusedStyle BackColor="#FFFFCC">
                                                                                    </FocusedStyle>
                                                                                    <DisabledStyle BackColor="#DEE8DB">
                                                                                    </DisabledStyle>
                                                                                </dx:ASPxTextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 25%; text-align: right;">
                                                                                <dx:ASPxLabel ID="lblPlantaOrigen" ClientInstanceName="lblPlantaOrigen" runat="server" ClientVisible="false" Font-Bold="True" ForeColor="#666666" Text="Planta Origen:">
                                                                                </dx:ASPxLabel>
                                                                            </td>
                                                                            <td style="width: 25%;">
                                                                                <dx:ASPxTextBox ID="txtPlantaOrigen" ClientInstanceName="txtPlantaOrigen" runat="server" Width="260px" ClientVisible="false">
                                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                                </ReadOnlyStyle>
                                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                                </FocusedStyle>
                                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                                </DisabledStyle>
                                                                            </dx:ASPxTextBox>
                                                                            </td>
                                                                            <td style="width: 25%; text-align: right;">
                                                                                <dx:ASPxLabel ID="lblPlantaDestino" ClientInstanceName="lblPlantaDestino" runat="server" ClientVisible="false" Font-Bold="True" ForeColor="#666666" Text="Planta Destino :" Width="150px">
                                                                                </dx:ASPxLabel>
                                                                            </td>
                                                                            <td style="width: 25%;">
                                                                                <dx:ASPxTextBox ID="txtPlantaDestino" ClientInstanceName="txtPlantaDestino" runat="server" Width="260px" ClientVisible="false">
                                                                                    <ReadOnlyStyle BackColor="#DEE8DB">
                                                                                    </ReadOnlyStyle>
                                                                                    <FocusedStyle BackColor="#FFFFCC">
                                                                                    </FocusedStyle>
                                                                                    <DisabledStyle BackColor="#DEE8DB">
                                                                                    </DisabledStyle>
                                                                                </dx:ASPxTextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-bottom: 5px" colspan="4">
                                                                                <dx:ASPxLabel runat="server" Text="RUBRO 2: COSTOS DE INSUMOS Y ADITIVOS IMPORTADOS (Bs.):" Width="100%" style="padding-bottom: 5px" Font-Bold="True" Font-Names="Rockwell" ForeColor="#487B5C">
                                                                                    <BorderBottom BorderStyle="Solid" />
                                                                                </dx:ASPxLabel>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-bottom: 5px; padding-top: 5px" colspan="4">
                                                                                <dx:ASPxGridView ID="dgvCostosF2" runat="server" Theme="Default" AutoGenerateColumns="False" EnableTheming="True" Font-Size="Smaller" ClientInstanceName="dgvTransacciones" Width="100%" >
                                                                                    <SettingsContextMenu EnableRowMenu="False">
                                                                                    </SettingsContextMenu>
                                                                                    <SettingsPager PageSize="100">
                                                                                        <PageSizeItemSettings Items="50, 100, 200, 500, 1000" Visible="True">
                                                                                        </PageSizeItemSettings>
                                                                                    </SettingsPager>
                                                                                    <SettingsEditing Mode="PopupEditForm">
                                                                                    </SettingsEditing>
                                                                                    <Settings ShowFooter="True" ShowGroupFooter="VisibleAlways" ShowGroupPanel="True" ShowHeaderFilterButton="True" />
                                                                                    <SettingsBehavior AllowFocusedRow="True" />
                                                                                    <SettingsPopup>
                                                                                        <EditForm Modal="True" />
                                                                                    </SettingsPopup>
                                                                                    <SettingsText CommandNew="Nuevo" />
                                                                                    <Columns>
                                                                                        <dx:GridViewDataTextColumn Caption="CONCEPTO" FieldName="RON" ShowInCustomizationForm="True" VisibleIndex="0" ToolTip="Concepto">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="COSTO DE PRODUCTO" FieldName="A" ShowInCustomizationForm="True" VisibleIndex="1" ToolTip="Costo de Producto" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" /> 
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="COSTO DE TRANSPORTE" FieldName="B" ShowInCustomizationForm="True" VisibleIndex="2" ToolTip="Costo de Transporte" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="COSTO DE SEGURO" FieldName="C" ShowInCustomizationForm="True" VisibleIndex="3" ToolTip="Costo de Seguro" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="COSTO DE INSPECCION" FieldName="D" ShowInCustomizationForm="True" VisibleIndex="4" ToolTip="Costo de Inspeccion" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="COSTOS ADUANEROS" FieldName="E" ShowInCustomizationForm="True" VisibleIndex="5" ToolTip="Costo Aduanero" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="COSTOS FINANCIEROS" FieldName="F" ShowInCustomizationForm="True" VisibleIndex="6" ToolTip="Costo Financiero" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="COSTOS DE ALMACENAJE" FieldName="G" ShowInCustomizationForm="True" VisibleIndex="7" ToolTip="Costo de Almacenaje" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="COSTOS DE MONITOREO" FieldName="H" ShowInCustomizationForm="True" VisibleIndex="8" ToolTip="Costo de Monitoreo" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="COSTOS DE IMPORTACION" FieldName="I" ShowInCustomizationForm="True" VisibleIndex="9" ToolTip="Costo de Importacion" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>                                                                                                                                                                               
                                                                                    </Columns>
                                                                                    <TotalSummary>
                                                                                            <dx:ASPxSummaryItem FieldName="I" ShowInColumn="I" SummaryType="Sum" Tag="I" DisplayFormat="F2" />
                                                                                    </TotalSummary>
                                                                                    <Styles CssPostfix="DevGlp"><AlternatingRow BackColor="#EEEEEE">
                                                                                        </AlternatingRow></Styles>
                                                                                </dx:ASPxGridView>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-bottom: 5px" colspan="4">
                                                                                <dx:ASPxLabel runat="server" Text="RUBRO 3: COSTOS DE GASOLINA BLANCA (Bs.):" Width="100%" style="padding-bottom: 5px" Font-Bold="True" Font-Names="Rockwell" ForeColor="#487B5C">
                                                                                    <BorderBottom BorderStyle="Solid" />
                                                                                </dx:ASPxLabel>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 25%; text-align: right;">
                                                                                <dx:ASPxLabel ID="lblformPreGb" ClientInstanceName="lblformPreGb" runat="server" Font-Bold="True" ForeColor="#666666" Text="Precio Gasolina Blanca:">
                                                                                </dx:ASPxLabel>
                                                                            </td>
                                                                            <td style="width: 25%;">
                                                                                <dx:ASPxTextBox ID="txtPrecioGasolinaBlancaF2" ClientInstanceName="txtPrecioGasolinaBlancaF2" runat="server" Width="260px">
                                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                                </ReadOnlyStyle>
                                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                                </FocusedStyle>
                                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                                </DisabledStyle>
                                                                            </dx:ASPxTextBox>
                                                                            </td>
                                                                            <td style="width: 25%; text-align: right;">
                                                                                <dx:ASPxLabel ID="lblPreNeto" ClientInstanceName="lblPreNeto" runat="server" Font-Bold="True" ForeColor="#666666" Text="Precio Neto de IVA :" Width="150px">
                                                                                </dx:ASPxLabel>
                                                                            </td>
                                                                            <td style="width: 25%;">
                                                                                <dx:ASPxTextBox ID="txtPrecioNetoIvaF2" ClientInstanceName="txtPrecioNetoIvaF2" runat="server" Width="260px">
                                                                                    <ReadOnlyStyle BackColor="#DEE8DB">
                                                                                    </ReadOnlyStyle>
                                                                                    <FocusedStyle BackColor="#FFFFCC">
                                                                                    </FocusedStyle>
                                                                                    <DisabledStyle BackColor="#DEE8DB">
                                                                                    </DisabledStyle>
                                                                                </dx:ASPxTextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 25%; text-align: right;">
                                                                                <dx:ASPxLabel ID="lblTaTraPol" ClientInstanceName="lblTaTraPol" runat="server" Font-Bold="True" ForeColor="#666666" Text="Tarifa Transport Poliductos:">
                                                                                </dx:ASPxLabel>
                                                                            </td>
                                                                            <td style="width: 25%;">
                                                                                <dx:ASPxTextBox ID="txtTarifaPoliductoF2" ClientInstanceName="txtTarifaPoliductoF2" runat="server" Width="260px">
                                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                                </ReadOnlyStyle>
                                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                                </FocusedStyle>
                                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                                </DisabledStyle>
                                                                            </dx:ASPxTextBox>
                                                                            </td>
                                                                            <td style="width: 25%; text-align: right;">
                                                                                <dx:ASPxLabel ID="lblTaNeto" ClientInstanceName="lblTaNeto" runat="server" Font-Bold="True" ForeColor="#666666" Text="Tarifa Neto de IVA :" Width="150px">
                                                                                </dx:ASPxLabel>
                                                                            </td>
                                                                            <td style="width: 25%;">
                                                                                <dx:ASPxTextBox ID="txtTarifaNetoIvaF2" ClientInstanceName="txtTarifaNetoIvaF2" runat="server" Width="260px">
                                                                                    <ReadOnlyStyle BackColor="#DEE8DB">
                                                                                    </ReadOnlyStyle>
                                                                                    <FocusedStyle BackColor="#FFFFCC">
                                                                                    </FocusedStyle>
                                                                                    <DisabledStyle BackColor="#DEE8DB">
                                                                                    </DisabledStyle>
                                                                                </dx:ASPxTextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 25%; text-align: right;">
                                                                                <dx:ASPxLabel ID="lblCosGasoBlanca" ClientInstanceName="lblCosGasoBlanca" runat="server" Font-Bold="True" ForeColor="#666666" Text="Costo Gasolina Blanca neto de IVA (CGB):">
                                                                                </dx:ASPxLabel>
                                                                            </td>
                                                                            <td style="width: 25%;">
                                                                                <dx:ASPxTextBox ID="txtCostoGasolinaBlancaNeto" ClientInstanceName="txtCostoGasolinaBlancaNeto" runat="server" Width="260px">
                                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                                </ReadOnlyStyle>
                                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                                </FocusedStyle>
                                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                                </DisabledStyle>
                                                                            </dx:ASPxTextBox>
                                                                            </td>
                                                                            <td style="width: 25%; text-align: right;">                                                                                
                                                                            </td>
                                                                            <td style="width: 25%;">                                                                                
                                                                            </td>
                                                                        </tr>

                                                                        <tr>
                                                                            <td style="padding-bottom: 5px; padding-top: 5px" colspan="4">
                                                                                <dx:ASPxGridView ID="dgvRubroCostoUnitario" runat="server" Theme="Default" AutoGenerateColumns="False"  EnableTheming="True" Font-Size="Smaller" ClientInstanceName="dgvRubroCostoUnitario" Width="100%" >
                                                                                    <SettingsContextMenu EnableRowMenu="False">
                                                                                    </SettingsContextMenu>                                                                                   
                                                                                    <SettingsEditing Mode="PopupEditForm">
                                                                                    </SettingsEditing>
                                                                                    <Settings ShowFooter="True" ShowGroupFooter="VisibleAlways" ShowGroupPanel="True" ShowHeaderFilterButton="True" />
                                                                                    <SettingsBehavior AllowFocusedRow="True" />
                                                                                    <SettingsPopup>
                                                                                        <EditForm Modal="True" />
                                                                                    </SettingsPopup>
                                                                                    <SettingsText CommandNew="Nuevo" />
                                                                                    <Columns>
                                                                                        <dx:GridViewDataTextColumn Caption="PRODUCTO" FieldName="DATO_1" ShowInCustomizationForm="True" VisibleIndex="0" ToolTip="Concepto">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="Volumen (Lt)" FieldName="DATO_2" ShowInCustomizationForm="True" VisibleIndex="1" ToolTip="Costo de Producto" PropertiesTextEdit-DisplayFormatString="F">
                                                                                            <Settings AutoFilterCondition="Contains" /> 
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="Costo (Bs)" FieldName="DATO_3" ShowInCustomizationForm="True" VisibleIndex="2" ToolTip="Costo de Transporte" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="Costo (Bs/lt)" FieldName="DATO_4" ShowInCustomizationForm="True" VisibleIndex="3" ToolTip="Costo de Seguro" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>                                                                                                                                                                                                                                                                       
                                                                                    </Columns>
                                                                                    <TotalSummary>
                                                                                            <dx:ASPxSummaryItem FieldName="DATO_4" ShowInColumn="DATO_4" SummaryType="Sum" Tag="DATO_4" DisplayFormat="F2" />
                                                                                    </TotalSummary>
                                                                                    <Styles CssPostfix="DevGlp"><AlternatingRow BackColor="#EEEEEE">
                                                                                        </AlternatingRow></Styles>
                                                                                </dx:ASPxGridView>
                                                                            </td>
                                                                        </tr>

                                                                        <tr>
                                                                            <td style="padding-bottom: 5px" colspan="4">
                                                                                <dx:ASPxLabel runat="server" Text="RUBRO 4: CALCULO DE VOLUMEN TOTAL Y COSTO TOTAL DE IMPORTACION:" Width="100%" style="padding-bottom: 5px" Font-Bold="True" Font-Names="Rockwell" ForeColor="#487B5C">
                                                                                    <BorderBottom BorderStyle="Solid" />
                                                                                </dx:ASPxLabel>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-bottom: 5px; padding-top: 5px" colspan="4">
                                                                                <dx:ASPxGridView ID="dgvCostoSubvencion" runat="server" Theme="Default" AutoGenerateColumns="False"  EnableTheming="True" Font-Size="Smaller" ClientInstanceName="dgvTransacciones" Width="100%" >
                                                                                    <SettingsContextMenu EnableRowMenu="False">
                                                                                    </SettingsContextMenu>
                                                                                    <SettingsPager PageSize="100">
                                                                                        <PageSizeItemSettings Items="50, 100, 200, 500, 1000" Visible="True">
                                                                                        </PageSizeItemSettings>
                                                                                    </SettingsPager>
                                                                                    <SettingsEditing Mode="PopupEditForm">
                                                                                    </SettingsEditing>
                                                                                    <Settings ShowFooter="True" ShowGroupFooter="VisibleAlways" ShowGroupPanel="True" ShowHeaderFilterButton="True" />
                                                                                    <SettingsBehavior AllowFocusedRow="True" />
                                                                                    <SettingsPopup>
                                                                                        <EditForm Modal="True" />
                                                                                    </SettingsPopup>
                                                                                    <SettingsText CommandNew="Nuevo" />
                                                                                    <Columns>
                                                                                        <dx:GridViewDataTextColumn Caption="Nº Factura Proveedor" FieldName="DATO_1" ShowInCustomizationForm="True" VisibleIndex="0" ToolTip="Concepto">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="RON" FieldName="DATO_2" ShowInCustomizationForm="True" VisibleIndex="1" ToolTip="Costo de Producto">
                                                                                            <Settings AutoFilterCondition="Contains" /> 
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="Vimp Lts." FieldName="DATO_3" ShowInCustomizationForm="True" VisibleIndex="2" ToolTip="Costo de Transporte" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="Cimp Bs/Lt" FieldName="DATO_4" ShowInCustomizationForm="True" VisibleIndex="3" ToolTip="Costo de Seguro" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="Ximp" FieldName="DATO_5" ShowInCustomizationForm="True" VisibleIndex="4" ToolTip="Costo de Inspeccion" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="GB Bs/Lt" FieldName="DATO_6" ShowInCustomizationForm="True" VisibleIndex="5" ToolTip="Costo Aduanero" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="XGB" FieldName="DATO_7" ShowInCustomizationForm="True" VisibleIndex="6" ToolTip="Costo Financiero" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="Vtotal Lt" FieldName="DATO_8" ShowInCustomizationForm="True" VisibleIndex="7" ToolTip="Costo de Almacenaje" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="CT Bs/Lt" FieldName="DATO_9" ShowInCustomizationForm="True" VisibleIndex="8" ToolTip="Costo de Monitoreo" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>                                                                                        
                                                                                    </Columns>
                                                                                    <TotalSummary>
                                                                                            <dx:ASPxSummaryItem FieldName="DATO_9" ShowInColumn="DATO_9" SummaryType="Sum" Tag="DATO_9" DisplayFormat="F2" />
                                                                                    </TotalSummary>
                                                                                    <Styles CssPostfix="DevGlp"><AlternatingRow BackColor="#EEEEEE">
                                                                                        </AlternatingRow></Styles>
                                                                                </dx:ASPxGridView>
                                                                            </td>
                                                                        </tr>

                                                                        <tr>
                                                                            <td style="padding-bottom: 5px; padding-top: 5px" colspan="4">
                                                                                <dx:ASPxGridView ID="dgvCostoUniSub" runat="server" Theme="Default" AutoGenerateColumns="False" EnableTheming="True" Font-Size="Smaller" ClientInstanceName="dgvCostoUniSub" Width="100%" >
                                                                                    <SettingsContextMenu EnableRowMenu="False">
                                                                                    </SettingsContextMenu>                                                                                   
                                                                                    <SettingsEditing Mode="PopupEditForm">
                                                                                    </SettingsEditing>
                                                                                    <Settings ShowFooter="True" ShowGroupFooter="VisibleAlways" ShowGroupPanel="True" ShowHeaderFilterButton="True" />
                                                                                    <SettingsBehavior AllowFocusedRow="True" />
                                                                                    <SettingsPopup>
                                                                                        <EditForm Modal="True" />
                                                                                    </SettingsPopup>
                                                                                    <SettingsText CommandNew="Nuevo" />
                                                                                    <Columns>
                                                                                        <dx:GridViewDataTextColumn Caption="PRODUCTO" FieldName="DATO_1" ShowInCustomizationForm="True" VisibleIndex="0" ToolTip="Concepto">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="VI&A (lt)" FieldName="DATO_2" ShowInCustomizationForm="True" VisibleIndex="1" ToolTip="Costo de Producto" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" /> 
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="VGB (lt)" FieldName="DATO_3" ShowInCustomizationForm="True" VisibleIndex="2" ToolTip="Costo de Transporte" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="VTGBASE" FieldName="DATO_4" ShowInCustomizationForm="True" VisibleIndex="3" ToolTip="Costo de Seguro" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>                                                                                                                                                                                
                                                                                    </Columns>
                                                                                    <TotalSummary>
                                                                                            <dx:ASPxSummaryItem FieldName="DATO_4" ShowInColumn="DATO_4" SummaryType="Sum" Tag="DATO_4" DisplayFormat="F2" />
                                                                                    </TotalSummary>
                                                                                    <Styles CssPostfix="DevGlp"><AlternatingRow BackColor="#EEEEEE">
                                                                                        </AlternatingRow></Styles>
                                                                                </dx:ASPxGridView>
                                                                            </td>
                                                                        </tr>

                                                                        <tr>
                                                                            <td style="padding-bottom: 5px" colspan="4">
                                                                                <dx:ASPxLabel runat="server" Text="RUBRO 5: CALCULO DE SUBVENCION YPFB o YPFB Refinacion SA:" Width="100%" style="padding-bottom: 5px" Font-Bold="True" Font-Names="Rockwell" ForeColor="#487B5C">
                                                                                    <BorderBottom BorderStyle="Solid" />
                                                                                </dx:ASPxLabel>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-bottom: 5px; padding-top: 5px" colspan="4">
                                                                                <dx:ASPxGridView ID="dgvSubvencionTotal" runat="server" Theme="Default" AutoGenerateColumns="False" EnableTheming="True" Font-Size="Smaller" ClientInstanceName="dgvTransacciones" Width="100%" >
                                                                                    <SettingsContextMenu EnableRowMenu="False">
                                                                                    </SettingsContextMenu>
                                                                                    <SettingsPager PageSize="100">
                                                                                        <PageSizeItemSettings Items="50, 100, 200, 500, 1000" Visible="True">
                                                                                        </PageSizeItemSettings>
                                                                                    </SettingsPager>
                                                                                    <SettingsEditing Mode="PopupEditForm">
                                                                                    </SettingsEditing>
                                                                                    <Settings ShowFooter="True" ShowGroupFooter="VisibleAlways" ShowGroupPanel="True" ShowHeaderFilterButton="True" />
                                                                                    <SettingsBehavior AllowFocusedRow="True" />
                                                                                    <SettingsPopup>
                                                                                        <EditForm Modal="True" />
                                                                                    </SettingsPopup>
                                                                                    <SettingsText CommandNew="Nuevo" />
                                                                                    <Columns>
                                                                                        <dx:GridViewDataTextColumn Caption="Empresa" FieldName="DATO_1" ShowInCustomizationForm="True" VisibleIndex="0" ToolTip="Concepto">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="Nº Factura Proveedor" FieldName="DATO_2" ShowInCustomizationForm="True" VisibleIndex="1" ToolTip="Costo de Producto">
                                                                                            <Settings AutoFilterCondition="Contains" /> 
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="VTOTAL (Lt)" FieldName="DATO_3" ShowInCustomizationForm="True" VisibleIndex="2" ToolTip="Costo de Transporte" PropertiesTextEdit-DisplayFormatString="F">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="CT" FieldName="DATO_4" ShowInCustomizationForm="True" VisibleIndex="3" ToolTip="Costo de Seguro" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="PEX-REF o PPT GE" FieldName="DATO_5" ShowInCustomizationForm="True" VisibleIndex="4" ToolTip="Costo de Inspeccion" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="Subvención (Bs)" FieldName="DATO_6" ShowInCustomizationForm="True" VisibleIndex="5" ToolTip="Costo Aduanero" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>                                                                                                                                                                               
                                                                                    </Columns>
                                                                                    <TotalSummary>
                                                                                            <dx:ASPxSummaryItem FieldName="DATO_6" ShowInColumn="DATO_6" SummaryType="Sum" Tag="DATO_6" DisplayFormat="F2" />
                                                                                    </TotalSummary>
                                                                                    <Styles CssPostfix="DevGlp"><AlternatingRow BackColor="#EEEEEE">
                                                                                        </AlternatingRow></Styles>
                                                                                </dx:ASPxGridView>
                                                                            </td>
                                                                        </tr>

                                                                        <tr>
                                                                            <td style="padding-bottom: 5px; padding-top: 5px" colspan="4">
                                                                                <dx:ASPxGridView ID="dgvCostoUnitarioSu" runat="server" Theme="Default" AutoGenerateColumns="False" EnableTheming="True" Font-Size="Smaller" ClientInstanceName="dgvCostoUnitarioSu" Width="100%" >
                                                                                    <SettingsContextMenu EnableRowMenu="False">
                                                                                    </SettingsContextMenu>
                                                                                    <SettingsEditing Mode="PopupEditForm">
                                                                                    </SettingsEditing>
                                                                                    <Settings ShowFooter="True" ShowGroupFooter="VisibleAlways" ShowGroupPanel="True" ShowHeaderFilterButton="True" />
                                                                                    <SettingsBehavior AllowFocusedRow="True" />
                                                                                    <SettingsPopup>
                                                                                        <EditForm Modal="True" />
                                                                                    </SettingsPopup>
                                                                                    <SettingsText CommandNew="Nuevo" />
                                                                                    <Columns>
                                                                                        <dx:GridViewDataTextColumn Caption="PRODUCTO" FieldName="DATO_1" ShowInCustomizationForm="True" VisibleIndex="0" ToolTip="Concepto">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="VI&A (lt)" FieldName="DATO_2" ShowInCustomizationForm="True" VisibleIndex="1" ToolTip="Costo de Producto" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" /> 
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="VGB (lt)" FieldName="DATO_3" ShowInCustomizationForm="True" VisibleIndex="2" ToolTip="Costo de Transporte" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="VTGBASE" FieldName="DATO_4" ShowInCustomizationForm="True" VisibleIndex="3" ToolTip="Costo de Seguro" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>                                                                                                                                                                                
                                                                                    </Columns>
                                                                                    <TotalSummary>
                                                                                            <dx:ASPxSummaryItem FieldName="DATO_4" ShowInColumn="DATO_4" SummaryType="Sum" Tag="DATO_4" DisplayFormat="F2" />
                                                                                    </TotalSummary>
                                                                                    <Styles CssPostfix="DevGlp"><AlternatingRow BackColor="#EEEEEE">
                                                                                        </AlternatingRow></Styles>
                                                                                </dx:ASPxGridView>
                                                                            </td>
                                                                        </tr>

                                                                    </table>
                                                                </dx:ContentControl>
                                                            </ContentCollection>
                                                        </dx:TabPage> 
                                                        
                                                        <dx:TabPage Text="ANEXO A">
                                                            <ContentCollection>
                                                                <dx:ContentControl runat="server">
                                                                    <table style="width: 100%">
                                                                        <tr>
                                                                            <td style="padding-bottom: 5px; padding-top: 5px">
                                                                                <dx:ASPxLabel runat="server" Text="ANEXO A" Width="100%" style="padding-bottom: 5px" Font-Bold="True" Font-Names="Rockwell" ForeColor="#487B5C">
                                                                                    <BorderBottom BorderStyle="Solid" />
                                                                                </dx:ASPxLabel>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-bottom: 5px; padding-top: 5px">
                                                                                <dx:ASPxLabel runat="server" Text="CALCULO DE COSTO DE PRODUCTO" Width="100%" style="padding-bottom: 5px" Font-Bold="True" Font-Names="Rockwell" ForeColor="#487B5C">
                                                                                    <BorderBottom BorderStyle="Solid" />
                                                                                </dx:ASPxLabel>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-bottom: 5px; padding-top: 5px">
                                                                                <dx:ASPxLabel ID="lblAaRubroIa" ClientInstanceName="lblAaRubroIa" runat="server" Text="Rubro 1: Insumos y Aditivos Importados" Width="100%" style="padding-bottom: 5px" Font-Bold="True" Font-Names="Rockwell" ForeColor="#487B5C">
                                                                                    <BorderBottom BorderStyle="Solid" />
                                                                                </dx:ASPxLabel>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-bottom: 5px; padding-top: 5px">
                                                                                <dx:ASPxGridView ID="dgvAnexoA" runat="server" Theme="Default" AutoGenerateColumns="False"  EnableTheming="True" Font-Size="Smaller" ClientInstanceName="dgvAnexoA" Width="100%" >
                                                                                    <SettingsContextMenu EnableRowMenu="False">
                                                                                    </SettingsContextMenu>
                                                                                    <SettingsPager PageSize="100">
                                                                                        <PageSizeItemSettings Items="50, 100, 200, 500, 1000" Visible="True">
                                                                                        </PageSizeItemSettings>
                                                                                    </SettingsPager>
                                                                                    <SettingsEditing Mode="PopupEditForm">
                                                                                    </SettingsEditing>
                                                                                    <Settings ShowFooter="True" ShowGroupFooter="VisibleAlways" ShowGroupPanel="True" ShowHeaderFilterButton="True" />
                                                                                    <SettingsBehavior AllowFocusedRow="True" />
                                                                                    <SettingsPopup>
                                                                                        <EditForm Modal="True" />
                                                                                    </SettingsPopup>
                                                                                    <SettingsText CommandNew="Nuevo" />
                                                                                    <Columns>
                                                                                        <dx:GridViewDataTextColumn Caption="Nº DUI" FieldName="NRO_DUI" ShowInCustomizationForm="True" VisibleIndex="0" ToolTip="Concepto">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="PROVEEDOR" FieldName="PROVEEDOR" ShowInCustomizationForm="True" VisibleIndex="1" ToolTip="Costo de Producto">
                                                                                            <Settings AutoFilterCondition="Contains" /> 
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="RON" FieldName="RON" ShowInCustomizationForm="True" VisibleIndex="2" ToolTip="Costo de Transporte">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="MES RECIBIDO" FieldName="MES" ShowInCustomizationForm="True" VisibleIndex="2" ToolTip="Mes Recibido">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="Nº DE FACTURA" FieldName="NRO_FACTURA" ShowInCustomizationForm="True" VisibleIndex="3" ToolTip="Costo de Seguro">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="VOLUMEN FACTURADO (Lt)" FieldName="VOLUMEN_FACTURADO" ShowInCustomizationForm="True" VisibleIndex="4" ToolTip="Costo de Inspeccion" PropertiesTextEdit-DisplayFormatString="F">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="VALOR FACTURADO (Bs)" FieldName="VALOR_FACTURADO" ShowInCustomizationForm="True" VisibleIndex="5" ToolTip="Costo Aduanero" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>                                                                                                                                                                              
                                                                                    </Columns>
                                                                                    <TotalSummary>
                                                                                            <dx:ASPxSummaryItem FieldName="VALOR_FACTURADO" ShowInColumn="VALOR_FACTURADO" SummaryType="Sum" Tag="VALOR_FACTURADO" DisplayFormat="F2" />
                                                                                    </TotalSummary>
                                                                                    <Styles CssPostfix="DevGlp"><AlternatingRow BackColor="#EEEEEE">
                                                                                        </AlternatingRow></Styles>
                                                                                </dx:ASPxGridView>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-bottom: 5px; padding-top: 5px">
                                                                                <dx:ASPxLabel runat="server" Text="La asignación de las facturas con sus volúmenes y montos a la DUI será presentado por YPFB como declaración jurada en el presente cuadro" Width="100%" style="padding-bottom: 5px" Font-Bold="True" Font-Names="Rockwell">                                                                                    
                                                                                </dx:ASPxLabel>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-bottom: 5px; padding-top: 5px">
                                                                                <dx:ASPxLabel ID="lblAnexoA" runat="server" Width="100%" style="padding-bottom: 5px" Font-Bold="True" Font-Names="Rockwell">                                                                                    
                                                                                </dx:ASPxLabel>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-bottom: 5px; padding-top: 5px">
                                                                                <dx:ASPxLabel ID="lblAaRubroGb" ClientInstanceName="lblAaRubroGb" runat="server" Text="Rubro 2: Gasolina Blanca" Width="100%" style="padding-bottom: 5px" Font-Bold="True" Font-Names="Rockwell" ForeColor="#487B5C">
                                                                                    <BorderBottom BorderStyle="Solid" />
                                                                                </dx:ASPxLabel>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-bottom: 5px; padding-top: 5px">
                                                                                <dx:ASPxGridView ID="dgvAnexoAGb" runat="server" Theme="Default" AutoGenerateColumns="False" EnableTheming="True" Font-Size="Smaller" ClientInstanceName="dgvAnexoAGb" Width="100%" >
                                                                                    <SettingsContextMenu EnableRowMenu="False">
                                                                                    </SettingsContextMenu>                                                                                    
                                                                                    <SettingsEditing Mode="PopupEditForm">
                                                                                    </SettingsEditing>
                                                                                    <Settings ShowFooter="True" ShowGroupFooter="VisibleAlways" ShowGroupPanel="True" ShowHeaderFilterButton="True" />
                                                                                    <SettingsBehavior AllowFocusedRow="True" />
                                                                                    <SettingsPopup>
                                                                                        <EditForm Modal="True" />
                                                                                    </SettingsPopup>
                                                                                    <SettingsText CommandNew="Nuevo" />
                                                                                    <Columns>
                                                                                        <dx:GridViewDataTextColumn Caption="PRODUCTO" FieldName="PRODUCTO" ShowInCustomizationForm="True" VisibleIndex="0" ToolTip="Concepto">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="MES RECIBIDO" FieldName="MES_RECIBIDO" ShowInCustomizationForm="True" VisibleIndex="1" ToolTip="Costo de Producto">
                                                                                            <Settings AutoFilterCondition="Contains" /> 
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="VOL (lt)" FieldName="VOLUMEN" ShowInCustomizationForm="True" VisibleIndex="2" ToolTip="Costo de Transporte" PropertiesTextEdit-DisplayFormatString="F">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="PRECIO (lt/bs)" FieldName="PRECIO" ShowInCustomizationForm="True" VisibleIndex="2" ToolTip="Mes Recibido" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="MONTO NETO DE IVA(Bs)" FieldName="MONTO_NETO" ShowInCustomizationForm="True" VisibleIndex="3" ToolTip="Costo de Seguro" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>                                                                                        
                                                                                    </Columns>
                                                                                    <TotalSummary>
                                                                                            <dx:ASPxSummaryItem FieldName="MONTO_NETO" ShowInColumn="MONTO_NETO" SummaryType="Sum" Tag="MONTO_NETO" DisplayFormat="F2" />
                                                                                    </TotalSummary>
                                                                                    <Styles CssPostfix="DevGlp"><AlternatingRow BackColor="#EEEEEE">
                                                                                        </AlternatingRow></Styles>
                                                                                </dx:ASPxGridView>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </dx:ContentControl>
                                                            </ContentCollection>
                                                        </dx:TabPage> 
                                                        
                                                        <dx:TabPage Text="ANEXO B">
                                                            <ContentCollection>
                                                                <dx:ContentControl runat="server">
                                                                    <table style="width: 100%">
                                                                        <tr>
                                                                            <td style="padding-bottom: 5px; padding-top: 5px">
                                                                                <dx:ASPxLabel runat="server" Text="ANEXO B1" Width="100%" style="padding-bottom: 5px" Font-Bold="True" Font-Names="Rockwell" ForeColor="#487B5C">
                                                                                    <BorderBottom BorderStyle="Solid" />
                                                                                </dx:ASPxLabel>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-bottom: 5px; padding-top: 5px">
                                                                                <dx:ASPxLabel runat="server" Text="CALCULO DEL COSTO DE TRANSPORTE FLUVIAL/BARCAZA" Width="100%" style="padding-bottom: 5px" Font-Bold="True" Font-Names="Rockwell" ForeColor="#487B5C">
                                                                                    <BorderBottom BorderStyle="Solid" />
                                                                                </dx:ASPxLabel>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-bottom: 5px; padding-top: 5px">
                                                                                <dx:ASPxGridView ID="dgvAnexoB1" runat="server" Theme="Default" AutoGenerateColumns="False"  EnableTheming="True" Font-Size="Smaller" ClientInstanceName="dgvAnexoB1" Width="100%" >
                                                                                    <SettingsContextMenu EnableRowMenu="False">
                                                                                    </SettingsContextMenu>
                                                                                    <SettingsPager PageSize="100">
                                                                                        <PageSizeItemSettings Items="50, 100, 200, 500, 1000" Visible="True">
                                                                                        </PageSizeItemSettings>
                                                                                    </SettingsPager>
                                                                                    <SettingsEditing Mode="PopupEditForm">
                                                                                    </SettingsEditing>
                                                                                    <Settings ShowFooter="True" ShowGroupFooter="VisibleAlways" ShowGroupPanel="True" ShowHeaderFilterButton="True" />
                                                                                    <SettingsBehavior AllowFocusedRow="True" />
                                                                                    <SettingsPopup>
                                                                                        <EditForm Modal="True" />
                                                                                    </SettingsPopup>
                                                                                    <SettingsText CommandNew="Nuevo" />
                                                                                    <Columns>
                                                                                        <dx:GridViewDataTextColumn Caption="Nº DUI" FieldName="NRO_DUI" ShowInCustomizationForm="True" VisibleIndex="0" ToolTip="Concepto">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="MES" FieldName="MES" ShowInCustomizationForm="True" VisibleIndex="1" ToolTip="Costo de Producto">
                                                                                            <Settings AutoFilterCondition="Contains" /> 
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="Nº FACTURA TRANSPORTE" FieldName="NRO_FACTURA_TRANSPORTE" ShowInCustomizationForm="True" VisibleIndex="2" ToolTip="Costo de Transporte">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="VOLUMEN DESPACHADO" FieldName="VOLUMEN_DESPACHADO" ShowInCustomizationForm="True" VisibleIndex="3" ToolTip="Costo de Inspeccion" PropertiesTextEdit-DisplayFormatString="F">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="VOLUMEN RECIBIDO" FieldName="VOLUMEN_RECIBIDO" ShowInCustomizationForm="True" VisibleIndex="4" ToolTip="Costo Aduanero" PropertiesTextEdit-DisplayFormatString="F">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="DIFERENCIA" FieldName="DIFERENCIA" ShowInCustomizationForm="True" VisibleIndex="5" ToolTip="Concepto">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="MERMA PERMISIBLE" FieldName="MERMA_PERMISIBLE" ShowInCustomizationForm="True" VisibleIndex="6" ToolTip="Costo de Producto" PropertiesTextEdit-DisplayFormatString="F">
                                                                                            <Settings AutoFilterCondition="Contains" /> 
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="MERMA EXCEDENTE" FieldName="MERMA_EXCEDENTE" ShowInCustomizationForm="True" VisibleIndex="7" ToolTip="Costo de Transporte" PropertiesTextEdit-DisplayFormatString="F">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="FLETE (BS/LT)" FieldName="FLETE" ShowInCustomizationForm="True" VisibleIndex="8" ToolTip="Costo de Seguro" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="COSTO DEL PRODUCTO (BS/LT)" FieldName="COSTO_PRODUCTO" ShowInCustomizationForm="True" VisibleIndex="9" ToolTip="Costo de Inspeccion" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="COSTO TRANSPORTE TOTAL" FieldName="TRANSPORTE_TOTAL" ShowInCustomizationForm="True" VisibleIndex="10" ToolTip="Costo Aduanero" PropertiesTextEdit-DisplayFormatString="F2"> 
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="DCTO. MERMA EXCEDENTE" FieldName="DESCUENTO_MERMA" ShowInCustomizationForm="True" VisibleIndex="11" ToolTip="Concepto" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="TRANSPORTE A SUBVENCIONAR" FieldName="COSTO_TRANSPORTE" ShowInCustomizationForm="True" VisibleIndex="12" ToolTip="Costo de Producto" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" /> 
                                                                                        </dx:GridViewDataTextColumn>                                                                                                                                                                                
                                                                                    </Columns>
                                                                                    <TotalSummary>
                                                                                            <dx:ASPxSummaryItem FieldName="COSTO_TRANSPORTE" ShowInColumn="COSTO_TRANSPORTE" SummaryType="Sum" Tag="COSTO_TRANSPORTE" DisplayFormat="F2" />
                                                                                    </TotalSummary>
                                                                                    <Styles CssPostfix="DevGlp"><AlternatingRow BackColor="#EEEEEE">
                                                                                        </AlternatingRow></Styles>
                                                                                </dx:ASPxGridView>
                                                                            </td>
                                                                        </tr>

                                                                        <tr>
                                                                            <td style="padding-bottom: 5px; padding-top: 5px">
                                                                                <dx:ASPxLabel ID="lblB1RubroGb" runat="server" Text="Rubro 2: Gasolina Blanca" Width="100%" style="padding-bottom: 5px" Font-Bold="True" Font-Names="Rockwell" ForeColor="#487B5C">
                                                                                    <BorderBottom BorderStyle="Solid" />
                                                                                </dx:ASPxLabel>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-bottom: 5px; padding-top: 5px">
                                                                                <dx:ASPxGridView ID="dgvAnexoB1Gb" runat="server" Theme="Default" AutoGenerateColumns="False" EnableTheming="True" Font-Size="Smaller" ClientInstanceName="dgvAnexoB1Gb" Width="100%" >
                                                                                    <SettingsContextMenu EnableRowMenu="False">
                                                                                    </SettingsContextMenu>                                                                                    
                                                                                    <SettingsEditing Mode="PopupEditForm">
                                                                                    </SettingsEditing>
                                                                                    <Settings ShowFooter="True" ShowGroupFooter="VisibleAlways" ShowGroupPanel="True" ShowHeaderFilterButton="True" />
                                                                                    <SettingsBehavior AllowFocusedRow="True" />
                                                                                    <SettingsPopup>
                                                                                        <EditForm Modal="True" />
                                                                                    </SettingsPopup>
                                                                                    <SettingsText CommandNew="Nuevo" />
                                                                                    <Columns>                                                                                        
                                                                                        <dx:GridViewDataTextColumn Caption="MES" FieldName="MES" ShowInCustomizationForm="True" VisibleIndex="1" ToolTip="Costo de Producto">
                                                                                            <Settings AutoFilterCondition="Contains" /> 
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="Nº FACTURA TRANSPORTE" FieldName="NRO_FACTURA_TRANSPORTE" ShowInCustomizationForm="True" VisibleIndex="2" ToolTip="Costo de Transporte">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="VOLUMEN DESPACHADO" FieldName="VOLUMEN_DESPACHADO" ShowInCustomizationForm="True" VisibleIndex="3" ToolTip="Costo de Inspeccion" PropertiesTextEdit-DisplayFormatString="F">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="VOLUMEN RECIBIDO" FieldName="VOLUMEN_RECIBIDO" ShowInCustomizationForm="True" VisibleIndex="4" ToolTip="Costo Aduanero" PropertiesTextEdit-DisplayFormatString="F">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="DIFERENCIA" FieldName="DIFERENCIA" ShowInCustomizationForm="True" VisibleIndex="5" ToolTip="Concepto">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="MERMA PERMISIBLE" FieldName="MERMA_PERMISIBLE" ShowInCustomizationForm="True" VisibleIndex="6" ToolTip="Costo de Producto" PropertiesTextEdit-DisplayFormatString="F">
                                                                                            <Settings AutoFilterCondition="Contains" /> 
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="MERMA EXCEDENTE" FieldName="MERMA_EXCEDENTE" ShowInCustomizationForm="True" VisibleIndex="7" ToolTip="Costo de Transporte" PropertiesTextEdit-DisplayFormatString="F">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="FLETE (BS/LT)" FieldName="FLETE" ShowInCustomizationForm="True" VisibleIndex="8" ToolTip="Costo de Seguro" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="COSTO DEL PRODUCTO (BS/LT)" FieldName="COSTO_PRODUCTO" ShowInCustomizationForm="True" VisibleIndex="9" ToolTip="Costo de Inspeccion" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="COSTO TRANSPORTE TOTAL" FieldName="TRANSPORTE_TOTAL" ShowInCustomizationForm="True" VisibleIndex="10" ToolTip="Costo Aduanero" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="DCTO. MERMA EXCEDENTE" FieldName="DESCUENTO_MERMA" ShowInCustomizationForm="True" VisibleIndex="11" ToolTip="Concepto" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="TRANSPORTE A SUBVENCIONAR" FieldName="COSTO_TRANSPORTE" ShowInCustomizationForm="True" VisibleIndex="12" ToolTip="Costo de Producto" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" /> 
                                                                                        </dx:GridViewDataTextColumn>                                                                                                                                                                                
                                                                                    </Columns>
                                                                                    <TotalSummary>
                                                                                            <dx:ASPxSummaryItem FieldName="COSTO_TRANSPORTE" ShowInColumn="COSTO_TRANSPORTE" SummaryType="Sum" Tag="COSTO_TRANSPORTE" DisplayFormat="F2" />
                                                                                    </TotalSummary>
                                                                                    <Styles CssPostfix="DevGlp"><AlternatingRow BackColor="#EEEEEE">
                                                                                        </AlternatingRow></Styles>
                                                                                </dx:ASPxGridView>
                                                                            </td>
                                                                        </tr>

                                                                        <tr>
                                                                            <td style="padding-bottom: 5px; padding-top: 5px">
                                                                                <dx:ASPxLabel runat="server" Text="ANEXO B2" Width="100%" style="padding-bottom: 5px" Font-Bold="True" Font-Names="Rockwell" ForeColor="#487B5C">
                                                                                    <BorderBottom BorderStyle="Solid" />
                                                                                </dx:ASPxLabel>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-bottom: 5px; padding-top: 5px">
                                                                                <dx:ASPxLabel runat="server" Text="CALCULO DEL COSTO DE TRANSPORTE TERRESTRE - CISTERNAS" Width="100%" style="padding-bottom: 5px" Font-Bold="True" Font-Names="Rockwell" >
                                                                                    <BorderBottom BorderStyle="Solid" />
                                                                                </dx:ASPxLabel>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-bottom: 5px; padding-top: 5px">
                                                                                <dx:ASPxGridView ID="dgvAnexoB2" runat="server" Theme="Default" AutoGenerateColumns="False" EnableTheming="True" Font-Size="Smaller" ClientInstanceName="dgvAnexoB2" Width="100%" >
                                                                                    <SettingsContextMenu EnableRowMenu="False">
                                                                                    </SettingsContextMenu>
                                                                                    <SettingsPager PageSize="100">
                                                                                        <PageSizeItemSettings Items="50, 100, 200, 500, 1000" Visible="True">
                                                                                        </PageSizeItemSettings>
                                                                                    </SettingsPager>
                                                                                    <SettingsEditing Mode="PopupEditForm">
                                                                                    </SettingsEditing>
                                                                                    <Settings ShowFooter="True" ShowGroupFooter="VisibleAlways" ShowGroupPanel="True" ShowHeaderFilterButton="True" />
                                                                                    <SettingsBehavior AllowFocusedRow="True" />
                                                                                    <SettingsPopup>
                                                                                        <EditForm Modal="True" />
                                                                                    </SettingsPopup>
                                                                                    <SettingsText CommandNew="Nuevo" />
                                                                                    <Columns>
                                                                                        <dx:GridViewDataTextColumn Caption="Nº DUI" FieldName="NRO_DUI" ShowInCustomizationForm="True" VisibleIndex="0" ToolTip="Concepto">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="Nº FACTURA PROVEEDOR" FieldName="NRO_FACTURA_PROVEEDOR" ShowInCustomizationForm="True" VisibleIndex="1" ToolTip="Costo de Producto">
                                                                                            <Settings AutoFilterCondition="Contains" /> 
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="EMPRESA TRANSPORTE" FieldName="EMPRESA_TRANSPORTE" ShowInCustomizationForm="True" VisibleIndex="2" ToolTip="Costo de Transporte">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="Nº FACTURA TRANSPORTE" FieldName="NRO_FACTURA_TRANSPORTE" ShowInCustomizationForm="True" VisibleIndex="3" ToolTip="Costo de Seguro">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="PLANTA DE DESPACHO" FieldName="PLANTA_DESPACHO" ShowInCustomizationForm="True" VisibleIndex="4" ToolTip="Costo de Producto">
                                                                                            <Settings AutoFilterCondition="Contains" /> 
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="PLANTA DE RECEPCIÓN" FieldName="PLANTA_RECEPCION" ShowInCustomizationForm="True" VisibleIndex="5" ToolTip="Costo de Transporte">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="VOLUMEN DESPACHADO" FieldName="VOLUMEN_DESPACHADO" ShowInCustomizationForm="True" VisibleIndex="6" ToolTip="Costo de Inspeccion" PropertiesTextEdit-DisplayFormatString="F">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="VOLUMEN RECIBIDO" FieldName="VOLUMEN_RECIBIDO" ShowInCustomizationForm="True" VisibleIndex="7" ToolTip="Costo Aduanero" PropertiesTextEdit-DisplayFormatString="F">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="DIFERENCIA" FieldName="DIFERENCIA" ShowInCustomizationForm="True" VisibleIndex="8" ToolTip="Concepto">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="MERMA PERMITIDA" FieldName="MERMA_PERMISIBLE" ShowInCustomizationForm="True" VisibleIndex="9" ToolTip="Costo de Producto" PropertiesTextEdit-DisplayFormatString="F">
                                                                                            <Settings AutoFilterCondition="Contains" /> 
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="MERMA EXCEDENTE" FieldName="MERMA_EXCEDENTE" ShowInCustomizationForm="True" VisibleIndex="10" ToolTip="Costo de Transporte" PropertiesTextEdit-DisplayFormatString="F">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="FLETE (BS/LT)" FieldName="FLETE" ShowInCustomizationForm="True" VisibleIndex="11" ToolTip="Costo de Seguro" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="COSTO DEL PRODUCTO (BS/LT)" FieldName="COSTO_PRODUCTO" ShowInCustomizationForm="True" VisibleIndex="12" ToolTip="Costo de Inspeccion" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="COSTO TRANSPORTE TOTAL" FieldName="TRANSPORTE_TOTAL" ShowInCustomizationForm="True" VisibleIndex="13" ToolTip="Costo Aduanero" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="DCTO. MERMA EXCEDENTE" FieldName="DESCUENTO_MERMA" ShowInCustomizationForm="True" VisibleIndex="14" ToolTip="Concepto" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="TRANSPORTE A SUBVENCIONAR" FieldName="COSTO_TRANSPORTE" ShowInCustomizationForm="True" VisibleIndex="15" ToolTip="Costo de Producto" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" /> 
                                                                                        </dx:GridViewDataTextColumn>                                                                                                                                                                                
                                                                                    </Columns>
                                                                                    <TotalSummary>
                                                                                            <dx:ASPxSummaryItem FieldName="COSTO_TRANSPORTE" ShowInColumn="COSTO_TRANSPORTE" SummaryType="Sum" Tag="COSTO_TRANSPORTE" DisplayFormat="F2" />
                                                                                    </TotalSummary>
                                                                                    <Styles CssPostfix="DevGlp"><AlternatingRow BackColor="#EEEEEE">
                                                                                        </AlternatingRow></Styles>
                                                                                </dx:ASPxGridView>
                                                                            </td>
                                                                        </tr>

                                                                        <tr>
                                                                            <td style="padding-bottom: 5px; padding-top: 5px">
                                                                                <dx:ASPxLabel ID="lblB2RubroDuGb" ClientInstanceName="lblB2RubroDuGb" runat="server" Text="Rubro 2: Gasolina Blanca Ductos" Width="100%" style="padding-bottom: 5px" Font-Bold="True" Font-Names="Rockwell" >
                                                                                    <BorderBottom BorderStyle="Solid" />
                                                                                </dx:ASPxLabel>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-bottom: 5px; padding-top: 5px">
                                                                                <dx:ASPxGridView ID="dgvAnexoB2DuGb" runat="server" Theme="Default" AutoGenerateColumns="False" EnableTheming="True" Font-Size="Smaller" ClientInstanceName="dgvAnexoB2" Width="100%" >
                                                                                    <SettingsContextMenu EnableRowMenu="False">
                                                                                    </SettingsContextMenu>                                                                                    
                                                                                    <SettingsEditing Mode="PopupEditForm">
                                                                                    </SettingsEditing>
                                                                                    <Settings ShowFooter="True" ShowGroupFooter="VisibleAlways" ShowGroupPanel="True" ShowHeaderFilterButton="True" />
                                                                                    <SettingsBehavior AllowFocusedRow="True" />
                                                                                    <SettingsPopup>
                                                                                        <EditForm Modal="True" />
                                                                                    </SettingsPopup>
                                                                                    <SettingsText CommandNew="Nuevo" />
                                                                                    <Columns>
                                                                                        <dx:GridViewDataTextColumn Caption="PRODUCTO" FieldName="PRODUCTO" ShowInCustomizationForm="True" VisibleIndex="0" ToolTip="Concepto">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="MES RECIBIDO" FieldName="MES_RECIBIDO" ShowInCustomizationForm="True" VisibleIndex="1" ToolTip="Costo de Producto">
                                                                                            <Settings AutoFilterCondition="Contains" /> 
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="VOL (lt)" FieldName="VOLUMEN" ShowInCustomizationForm="True" VisibleIndex="2" ToolTip="Costo de Transporte" PropertiesTextEdit-DisplayFormatString="F">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="PRECIO (Bs/Lt)" FieldName="PRECIO" ShowInCustomizationForm="True" VisibleIndex="3" ToolTip="Costo de Seguro" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="MONTO NETO DE IVA (Bs)" FieldName="MONTO" ShowInCustomizationForm="True" VisibleIndex="4" ToolTip="Costo de Producto" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" /> 
                                                                                        </dx:GridViewDataTextColumn>                                                                                                                                                                                                                                                                      
                                                                                    </Columns>
                                                                                    <TotalSummary>
                                                                                            <dx:ASPxSummaryItem FieldName="MONTO" ShowInColumn="MONTO" SummaryType="Sum" Tag="MONTO" DisplayFormat="F2" />
                                                                                    </TotalSummary>
                                                                                    <Styles CssPostfix="DevGlp"><AlternatingRow BackColor="#EEEEEE">
                                                                                        </AlternatingRow></Styles>
                                                                                </dx:ASPxGridView>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-bottom: 5px; padding-top: 5px">
                                                                                <dx:ASPxLabel ID="lblB2RubroGb" ClientInstanceName="lblB2RubroGb" runat="server" Text="Rubro 3: Gasolina Blacnca Cisternas" Width="100%" style="padding-bottom: 5px" Font-Bold="True" Font-Names="Rockwell" >
                                                                                    <BorderBottom BorderStyle="Solid" />
                                                                                </dx:ASPxLabel>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-bottom: 5px; padding-top: 5px">
                                                                                <dx:ASPxGridView ID="dgvAnexoB2Gb" runat="server" Theme="Default" AutoGenerateColumns="False" EnableTheming="True" Font-Size="Smaller" ClientInstanceName="dgvAnexoB2Gb" Width="100%" >
                                                                                    <SettingsContextMenu EnableRowMenu="False">
                                                                                    </SettingsContextMenu>                                                                                    
                                                                                    <SettingsEditing Mode="PopupEditForm">
                                                                                    </SettingsEditing>
                                                                                    <Settings ShowFooter="True" ShowGroupFooter="VisibleAlways" ShowGroupPanel="True" ShowHeaderFilterButton="True" />
                                                                                    <SettingsBehavior AllowFocusedRow="True" />
                                                                                    <SettingsPopup>
                                                                                        <EditForm Modal="True" />
                                                                                    </SettingsPopup>
                                                                                    <SettingsText CommandNew="Nuevo" />
                                                                                    <Columns>                                                                                        
                                                                                        <dx:GridViewDataTextColumn Caption="EMPRESA TRANSPORTE" FieldName="EMPRESA_TRANSPORTE" ShowInCustomizationForm="True" VisibleIndex="2" ToolTip="Costo de Transporte">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="Nº FACTURA TRANSPORTE" FieldName="NRO_FACTURA_TRANSPORTE" ShowInCustomizationForm="True" VisibleIndex="3" ToolTip="Costo de Seguro">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="PLANTA DE DESPACHO" FieldName="PLANTA_DESPACHO" ShowInCustomizationForm="True" VisibleIndex="4" ToolTip="Costo de Producto">
                                                                                            <Settings AutoFilterCondition="Contains" /> 
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="PLANTA DE RECEPCIÓN" FieldName="PLANTA_RECEPCION" ShowInCustomizationForm="True" VisibleIndex="5" ToolTip="Costo de Transporte">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="VOLUMEN DESPACHADO" FieldName="VOLUMEN_DESPACHADO" ShowInCustomizationForm="True" VisibleIndex="6" ToolTip="Costo de Inspeccion" PropertiesTextEdit-DisplayFormatString="F">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="VOLUMEN RECIBIDO" FieldName="VOLUMEN_RECIBIDO" ShowInCustomizationForm="True" VisibleIndex="7" ToolTip="Costo Aduanero" PropertiesTextEdit-DisplayFormatString="F">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="DIFERENCIA" FieldName="DIFERENCIA" ShowInCustomizationForm="True" VisibleIndex="8" ToolTip="Concepto">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="MERMA PERMITIDA" FieldName="MERMA_PERMISIBLE" ShowInCustomizationForm="True" VisibleIndex="9" ToolTip="Costo de Producto" PropertiesTextEdit-DisplayFormatString="F">
                                                                                            <Settings AutoFilterCondition="Contains" /> 
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="MERMA EXCEDENTE" FieldName="MERMA_EXCEDENTE" ShowInCustomizationForm="True" VisibleIndex="10" ToolTip="Costo de Transporte" PropertiesTextEdit-DisplayFormatString="F">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="FLETE (BS/LT)" FieldName="FLETE" ShowInCustomizationForm="True" VisibleIndex="11" ToolTip="Costo de Seguro" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="COSTO DEL PRODUCTO (BS/LT)" FieldName="COSTO_PRODUCTO" ShowInCustomizationForm="True" VisibleIndex="12" ToolTip="Costo de Inspeccion" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="COSTO TRANSPORTE TOTAL" FieldName="TRANSPORTE_TOTAL" ShowInCustomizationForm="True" VisibleIndex="13" ToolTip="Costo Aduanero" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="DCTO. MERMA EXCEDENTE" FieldName="DESCUENTO_MERMA" ShowInCustomizationForm="True" VisibleIndex="14" ToolTip="Concepto" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="TRANSPORTE A SUBVENCIONAR" FieldName="COSTO_TRANSPORTE" ShowInCustomizationForm="True" VisibleIndex="15" ToolTip="Costo de Producto" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" /> 
                                                                                        </dx:GridViewDataTextColumn>                                                                                                                                                                                
                                                                                    </Columns>
                                                                                    <TotalSummary>
                                                                                            <dx:ASPxSummaryItem FieldName="COSTO_TRANSPORTE" ShowInColumn="COSTO_TRANSPORTE" SummaryType="Sum" Tag="COSTO_TRANSPORTE" DisplayFormat="F2" />
                                                                                    </TotalSummary>
                                                                                    <Styles CssPostfix="DevGlp"><AlternatingRow BackColor="#EEEEEE">
                                                                                        </AlternatingRow></Styles>
                                                                                </dx:ASPxGridView>
                                                                            </td>
                                                                        </tr>


                                                                        <tr>
                                                                            <td style="padding-bottom: 5px; padding-top: 5px">
                                                                                <dx:ASPxLabel runat="server" Text="ANEXO B3" Width="100%" style="padding-bottom: 5px" Font-Bold="True" Font-Names="Rockwell" ForeColor="#487B5C">
                                                                                    <BorderBottom BorderStyle="Solid" />
                                                                                </dx:ASPxLabel>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-bottom: 5px; padding-top: 5px">
                                                                                <dx:ASPxLabel runat="server" Text="CALCULO DEL COSTO DE TRANSPORTE FERREO" Width="100%" style="padding-bottom: 5px" Font-Bold="True" Font-Names="Rockwell" ForeColor="#487B5C">
                                                                                    <BorderBottom BorderStyle="Solid" />
                                                                                </dx:ASPxLabel>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-bottom: 5px; padding-top: 5px">
                                                                                <dx:ASPxGridView ID="dgvAnexoB3" runat="server" Theme="Default" AutoGenerateColumns="False" EnableTheming="True" Font-Size="Smaller" ClientInstanceName="dgvAnexoB3" Width="100%" >
                                                                                    <SettingsContextMenu EnableRowMenu="False">
                                                                                    </SettingsContextMenu>
                                                                                    <SettingsPager PageSize="100">
                                                                                        <PageSizeItemSettings Items="50, 100, 200, 500, 1000" Visible="True">
                                                                                        </PageSizeItemSettings>
                                                                                    </SettingsPager>
                                                                                    <SettingsEditing Mode="PopupEditForm">
                                                                                    </SettingsEditing>
                                                                                    <Settings ShowFooter="True" ShowGroupFooter="VisibleAlways" ShowGroupPanel="True" ShowHeaderFilterButton="True" />
                                                                                    <SettingsBehavior AllowFocusedRow="True" />
                                                                                    <SettingsPopup>
                                                                                        <EditForm Modal="True" />
                                                                                    </SettingsPopup>
                                                                                    <SettingsText CommandNew="Nuevo" />
                                                                                    <Columns>
                                                                                        <dx:GridViewDataTextColumn Caption="Nº DUI" FieldName="NRO_DUI" ShowInCustomizationForm="True" VisibleIndex="0" ToolTip="Concepto">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="Nº FACTURA PROVEEDOR" FieldName="NRO_FACTURA_PROVEEDOR" ShowInCustomizationForm="True" VisibleIndex="1" ToolTip="Costo de Producto">
                                                                                            <Settings AutoFilterCondition="Contains" /> 
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="PROVEEDOR" FieldName="PROVEEDOR" ShowInCustomizationForm="True" VisibleIndex="2" ToolTip="Costo de Transporte">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="EMPRESA" FieldName="EMPRESA" ShowInCustomizationForm="True" VisibleIndex="3" ToolTip="Costo de Seguro">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="Nº FACTURA" FieldName="NRO_FACTURA" ShowInCustomizationForm="True" VisibleIndex="4" ToolTip="Costo de Inspeccion">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="VOLUMEN DESTINO 1" FieldName="VOLUMEN_DESTINO1" ShowInCustomizationForm="True" VisibleIndex="5" ToolTip="Costo Aduanero" PropertiesTextEdit-DisplayFormatString="F">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="VOLUMEN DESTINO 2" FieldName="VOLUMEN_DESTINO2" ShowInCustomizationForm="True" VisibleIndex="6" ToolTip="Concepto" PropertiesTextEdit-DisplayFormatString="F">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>                                                                                        
                                                                                        <dx:GridViewDataTextColumn Caption="FLETE (BS/LT)" FieldName="FLETE" ShowInCustomizationForm="True" VisibleIndex="7" ToolTip="Costo de Seguro" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="MONTO FACTURADO (BS)" FieldName="COSTO_FACTURADO" ShowInCustomizationForm="True" VisibleIndex="8" ToolTip="Costo de Inspeccion" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="MONTO NETO DE IVA TRANSPORTE" FieldName="MONTO_NETO_IVA" ShowInCustomizationForm="True" VisibleIndex="9" ToolTip="Costo Aduanero" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>                                                                                                                                                                                                                                                                       
                                                                                    </Columns>
                                                                                    <TotalSummary>
                                                                                            <dx:ASPxSummaryItem FieldName="MONTO_NETO_IVA" ShowInColumn="MONTO_NETO_IVA" SummaryType="Sum" Tag="MONTO_NETO_IVA" DisplayFormat="F2" />
                                                                                    </TotalSummary>
                                                                                    <Styles CssPostfix="DevGlp"><AlternatingRow BackColor="#EEEEEE">
                                                                                        </AlternatingRow></Styles>
                                                                                </dx:ASPxGridView>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-bottom: 5px; padding-top: 5px">
                                                                                <dx:ASPxLabel ID="lblAnexoB" runat="server" Width="100%" style="padding-bottom: 5px" Font-Bold="True" Font-Names="Rockwell">                                                                                    
                                                                                </dx:ASPxLabel>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </dx:ContentControl>
                                                            </ContentCollection>
                                                        </dx:TabPage>
                                                        
                                                        <dx:TabPage Text="ANEXO C">
                                                            <ContentCollection>
                                                                <dx:ContentControl runat="server">
                                                                    <table style="width: 100%">
                                                                        <tr>
                                                                            <td style="padding-bottom: 5px; padding-top: 5px">
                                                                                <dx:ASPxLabel runat="server" Text="ANEXO C" Width="100%" style="padding-bottom: 5px" Font-Bold="True" Font-Names="Rockwell" ForeColor="#487B5C">
                                                                                    <BorderBottom BorderStyle="Solid" />
                                                                                </dx:ASPxLabel>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-bottom: 5px; padding-top: 5px">
                                                                                <dx:ASPxLabel runat="server" Text="CALCULO DEL COSTO DE SEGURO" Width="100%" style="padding-bottom: 5px" Font-Bold="True" Font-Names="Rockwell" ForeColor="#487B5C">
                                                                                    <BorderBottom BorderStyle="Solid" />
                                                                                </dx:ASPxLabel>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-bottom: 5px; padding-top: 5px">
                                                                                <dx:ASPxLabel ID="ASPxLabel10" runat="server" Text="Rubro 1: Insumos y Aditivos" Width="100%" style="padding-bottom: 5px" Font-Bold="True" Font-Names="Rockwell" ForeColor="#487B5C">
                                                                                    <BorderBottom BorderStyle="Solid" />
                                                                                </dx:ASPxLabel>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-bottom: 5px; padding-top: 5px">
                                                                                <dx:ASPxGridView ID="dgvAnexoC" runat="server" Theme="Default" AutoGenerateColumns="False"  EnableTheming="True" Font-Size="Smaller" ClientInstanceName="dgvAnexoC" Width="100%" >
                                                                                    <SettingsContextMenu EnableRowMenu="False">
                                                                                    </SettingsContextMenu>                                                                                   
                                                                                    <SettingsEditing Mode="PopupEditForm">
                                                                                    </SettingsEditing>
                                                                                    <Settings ShowFooter="True" ShowGroupFooter="VisibleAlways" ShowGroupPanel="True" ShowHeaderFilterButton="True" />
                                                                                    <SettingsBehavior AllowFocusedRow="True" />
                                                                                    <SettingsPopup>
                                                                                        <EditForm Modal="True" />
                                                                                    </SettingsPopup>
                                                                                    <SettingsText CommandNew="Nuevo" />
                                                                                    <Columns>
                                                                                        <dx:GridViewDataTextColumn Caption="Nº DUI" FieldName="NRO_DUI" ShowInCustomizationForm="True" VisibleIndex="0" ToolTip="Concepto">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="EMPRESA ASEGURADORA" FieldName="EMPRESA_ASEGURADORA" ShowInCustomizationForm="True" VisibleIndex="1" ToolTip="Costo de Producto">
                                                                                            <Settings AutoFilterCondition="Contains" /> 
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="PERIODO COBERTURA" FieldName="PERIODO_COBERTURA" ShowInCustomizationForm="True" VisibleIndex="2" ToolTip="Costo de Transporte">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="Nº FACTURA ASEGURADORA" FieldName="NRO_FACTURA_ASEGURADORA" ShowInCustomizationForm="True" VisibleIndex="3" ToolTip="Costo de Seguro">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="MONTO FACTURADO SEGURO (BS)" FieldName="MONTO_FACTURADO" ShowInCustomizationForm="True" VisibleIndex="4" ToolTip="Costo de Inspeccion" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="MONTO NETO DE IVA (BS)" FieldName="MONTO_NETO_IVA" ShowInCustomizationForm="True" VisibleIndex="5" ToolTip="Costo Aduanero" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>                                                                                                                                                                                                                                                                                                                                                               
                                                                                    </Columns>
                                                                                    <TotalSummary>
                                                                                            <dx:ASPxSummaryItem FieldName="MONTO_NETO_IVA" ShowInColumn="MONTO_NETO_IVA" SummaryType="Sum" Tag="MONTO_NETO_IVA" DisplayFormat="F2" />
                                                                                    </TotalSummary>
                                                                                    <Styles CssPostfix="DevGlp"><AlternatingRow BackColor="#EEEEEE">
                                                                                        </AlternatingRow></Styles>
                                                                                </dx:ASPxGridView>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-bottom: 5px; padding-top: 5px">
                                                                                <dx:ASPxLabel ID="lblAnexoC" runat="server" Width="100%" style="padding-bottom: 5px" Font-Bold="True" Font-Names="Rockwell">                                                                                    
                                                                                </dx:ASPxLabel>
                                                                            </td>
                                                                        </tr>

                                                                        <tr>
                                                                            <td style="padding-bottom: 5px; padding-top: 5px">lblAcRubroGb
                                                                                <dx:ASPxLabel ID="lblAcRubroGb" ClientInstanceName="" runat="server" Text="Rubro 2: Gasolina Blanca" Width="100%" style="padding-bottom: 5px" Font-Bold="True" Font-Names="Rockwell" ForeColor="#487B5C">
                                                                                    <BorderBottom BorderStyle="Solid" />
                                                                                </dx:ASPxLabel>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-bottom: 5px; padding-top: 5px">
                                                                                <dx:ASPxGridView ID="dgvAnexoCGb" runat="server" Theme="Default" AutoGenerateColumns="False"  EnableTheming="True" Font-Size="Smaller" ClientInstanceName="dgvAnexoCGb" Width="100%" >
                                                                                    <SettingsContextMenu EnableRowMenu="False">
                                                                                    </SettingsContextMenu>                                                                                   
                                                                                    <SettingsEditing Mode="PopupEditForm">
                                                                                    </SettingsEditing>
                                                                                    <Settings ShowFooter="True" ShowGroupFooter="VisibleAlways" ShowGroupPanel="True" ShowHeaderFilterButton="True" />
                                                                                    <SettingsBehavior AllowFocusedRow="True" />
                                                                                    <SettingsPopup>
                                                                                        <EditForm Modal="True" />
                                                                                    </SettingsPopup>
                                                                                    <SettingsText CommandNew="Nuevo" />
                                                                                    <Columns>
                                                                                        <dx:GridViewDataTextColumn Caption="Nº DUI" FieldName="NRO_DUI" ShowInCustomizationForm="True" VisibleIndex="0" ToolTip="Concepto">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="EMPRESA ASEGURADORA" FieldName="EMPRESA_ASEGURADORA" ShowInCustomizationForm="True" VisibleIndex="1" ToolTip="Costo de Producto">
                                                                                            <Settings AutoFilterCondition="Contains" /> 
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="PERIODO COBERTURA" FieldName="PERIODO_COBERTURA" ShowInCustomizationForm="True" VisibleIndex="2" ToolTip="Costo de Transporte">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="Nº FACTURA ASEGURADORA" FieldName="NRO_FACTURA_ASEGURADORA" ShowInCustomizationForm="True" VisibleIndex="3" ToolTip="Costo de Seguro">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="MONTO FACTURADO SEGURO (BS)" FieldName="MONTO_FACTURADO" ShowInCustomizationForm="True" VisibleIndex="4" ToolTip="Costo de Inspeccion" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="MONTO NETO DE IVA (BS)" FieldName="MONTO_NETO_IVA" ShowInCustomizationForm="True" VisibleIndex="5" ToolTip="Costo Aduanero" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>                                                                                                                                                                                                                                                                                                                                                               
                                                                                    </Columns>
                                                                                    <TotalSummary>
                                                                                            <dx:ASPxSummaryItem FieldName="MONTO_NETO_IVA" ShowInColumn="MONTO_NETO_IVA" SummaryType="Sum" Tag="MONTO_NETO_IVA" DisplayFormat="F2" />
                                                                                    </TotalSummary>
                                                                                    <Styles CssPostfix="DevGlp"><AlternatingRow BackColor="#EEEEEE">
                                                                                        </AlternatingRow></Styles>
                                                                                </dx:ASPxGridView>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </dx:ContentControl>
                                                            </ContentCollection>
                                                        </dx:TabPage>  
                                                        
                                                        <dx:TabPage Text="ANEXO D">
                                                            <ContentCollection>
                                                                <dx:ContentControl runat="server">
                                                                    <table style="width: 100%">
                                                                        <tr>
                                                                            <td style="padding-bottom: 5px; padding-top: 5px">
                                                                                <dx:ASPxLabel runat="server" Text="ANEXO D1" Width="100%" style="padding-bottom: 5px" Font-Bold="True" Font-Names="Rockwell" ForeColor="#487B5C">
                                                                                    <BorderBottom BorderStyle="Solid" />
                                                                                </dx:ASPxLabel>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-bottom: 5px; padding-top: 5px">
                                                                                <dx:ASPxLabel runat="server" Text="CALCULO COSTO DE INSPECCION EN ORIGEN" Width="100%" style="padding-bottom: 5px" Font-Bold="True" Font-Names="Rockwell" ForeColor="#487B5C">
                                                                                    <BorderBottom BorderStyle="Solid" />
                                                                                </dx:ASPxLabel>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-bottom: 5px; padding-top: 5px">
                                                                                <dx:ASPxGridView ID="dgvAnexoD1" runat="server" Theme="Default" AutoGenerateColumns="False"  EnableTheming="True" Font-Size="Smaller" ClientInstanceName="dgvAnexoD1" Width="100%" >
                                                                                    <SettingsContextMenu EnableRowMenu="False">
                                                                                    </SettingsContextMenu>
                                                                                    <SettingsPager PageSize="100">
                                                                                        <PageSizeItemSettings Items="50, 100, 200, 500, 1000" Visible="True">
                                                                                        </PageSizeItemSettings>
                                                                                    </SettingsPager>
                                                                                    <SettingsEditing Mode="PopupEditForm">
                                                                                    </SettingsEditing>
                                                                                    <Settings ShowFooter="True" ShowGroupFooter="VisibleAlways" ShowGroupPanel="True" ShowHeaderFilterButton="True" />
                                                                                    <SettingsBehavior AllowFocusedRow="True" />
                                                                                    <SettingsPopup>
                                                                                        <EditForm Modal="True" />
                                                                                    </SettingsPopup>
                                                                                    <SettingsText CommandNew="Nuevo" />
                                                                                    <Columns>
                                                                                        <dx:GridViewDataTextColumn Caption="MES DE CARGA" FieldName="MES_CARGA" ShowInCustomizationForm="True" VisibleIndex="0" ToolTip="Concepto">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="DUI" FieldName="NRO_DUI" ShowInCustomizationForm="True" VisibleIndex="1" ToolTip="Costo de Producto">
                                                                                            <Settings AutoFilterCondition="Contains" /> 
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="PROVEEDOR" FieldName="PROVEEDOR" ShowInCustomizationForm="True" VisibleIndex="2" ToolTip="Costo de Transporte">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="Nº FACTURA PROVEEDOR" FieldName="NRO_FACTURA" ShowInCustomizationForm="True" VisibleIndex="3" ToolTip="Costo de Seguro">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="VOLUMEN FACTURADO(Lt)" FieldName="VOLUMEN_FACTURADO" ShowInCustomizationForm="True" VisibleIndex="4" ToolTip="Costo Aduanero" PropertiesTextEdit-DisplayFormatString="F">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn> 
                                                                                        <dx:GridViewDataTextColumn Caption="MONTO INSPECCION ORIGEN (Bs)" FieldName="MONTO_INSPECCION" ShowInCustomizationForm="True" VisibleIndex="5" ToolTip="Costo Aduanero" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>                                                                                                                                                                                                                                                                                                                                                              
                                                                                    </Columns>
                                                                                    <TotalSummary>
                                                                                            <dx:ASPxSummaryItem FieldName="MONTO_INSPECCION" ShowInColumn="MONTO_INSPECCION" SummaryType="Sum" Tag="MONTO_INSPECCION" DisplayFormat="F2" />
                                                                                    </TotalSummary>
                                                                                    <Styles CssPostfix="DevGlp"><AlternatingRow BackColor="#EEEEEE">
                                                                                        </AlternatingRow></Styles>
                                                                                </dx:ASPxGridView>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-bottom: 5px; padding-top: 5px">
                                                                                <dx:ASPxLabel runat="server" Text="ANEXO D2" Width="100%" style="padding-bottom: 5px" Font-Bold="True" Font-Names="Rockwell" ForeColor="#487B5C">
                                                                                    <BorderBottom BorderStyle="Solid" />
                                                                                </dx:ASPxLabel>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-bottom: 5px; padding-top: 5px">
                                                                                <dx:ASPxLabel runat="server" Text="CALCULO COSTO DE INSPECCION EN DESTINO INTERMEDIO" Width="100%" style="padding-bottom: 5px" Font-Bold="True" Font-Names="Rockwell" ForeColor="#487B5C">
                                                                                    <BorderBottom BorderStyle="Solid" />
                                                                                </dx:ASPxLabel>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-bottom: 5px; padding-top: 5px">
                                                                                <dx:ASPxGridView ID="dgvAnexoD2" runat="server" Theme="Default" AutoGenerateColumns="False"  EnableTheming="True" Font-Size="Smaller" ClientInstanceName="dgvAnexoD2" Width="100%" >
                                                                                    <SettingsContextMenu EnableRowMenu="False">
                                                                                    </SettingsContextMenu>
                                                                                    <SettingsPager PageSize="100">
                                                                                        <PageSizeItemSettings Items="50, 100, 200, 500, 1000" Visible="True">
                                                                                        </PageSizeItemSettings>
                                                                                    </SettingsPager>
                                                                                    <SettingsEditing Mode="PopupEditForm">
                                                                                    </SettingsEditing>
                                                                                    <Settings ShowFooter="True" ShowGroupFooter="VisibleAlways" ShowGroupPanel="True" ShowHeaderFilterButton="True" />
                                                                                    <SettingsBehavior AllowFocusedRow="True" />
                                                                                    <SettingsPopup>
                                                                                        <EditForm Modal="True" />
                                                                                    </SettingsPopup>
                                                                                    <SettingsText CommandNew="Nuevo" />
                                                                                    <Columns>
                                                                                        <dx:GridViewDataTextColumn Caption="MES DE RECEPCION" FieldName="MES_CARGA" ShowInCustomizationForm="True" VisibleIndex="0" ToolTip="Concepto">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="DUI" FieldName="NRO_DUI" ShowInCustomizationForm="True" VisibleIndex="1" ToolTip="Costo de Producto">
                                                                                            <Settings AutoFilterCondition="Contains" /> 
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="PROVEEDOR" FieldName="PROVEEDOR" ShowInCustomizationForm="True" VisibleIndex="2" ToolTip="Costo de Transporte">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="Nº FACTURA PROVEEDOR" FieldName="NRO_FACTURA" ShowInCustomizationForm="True" VisibleIndex="3" ToolTip="Costo de Seguro">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="VOLUMEN RECIBIDO(Lt)" FieldName="VOLUMEN_FACTURADO" ShowInCustomizationForm="True" VisibleIndex="4" ToolTip="Costo Aduanero" PropertiesTextEdit-DisplayFormatString="F">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn> 
                                                                                        <dx:GridViewDataTextColumn Caption="MONTO INSPECCION DESTINO INTERMEDIO (Bs)" FieldName="MONTO_INSPECCION" ShowInCustomizationForm="True" VisibleIndex="5" ToolTip="Costo Aduanero" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>                                                                                                                                                                                                                                                                                                                                                              
                                                                                    </Columns>
                                                                                    <TotalSummary>
                                                                                            <dx:ASPxSummaryItem FieldName="MONTO_INSPECCION" ShowInColumn="MONTO_INSPECCION" SummaryType="Sum" Tag="MONTO_INSPECCION" DisplayFormat="F2" />
                                                                                    </TotalSummary>
                                                                                    <Styles CssPostfix="DevGlp"><AlternatingRow BackColor="#EEEEEE">
                                                                                        </AlternatingRow></Styles>
                                                                                </dx:ASPxGridView>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-bottom: 5px; padding-top: 5px">
                                                                                <dx:ASPxLabel runat="server" Text="ANEXO D3" Width="100%" style="padding-bottom: 5px" Font-Bold="True" Font-Names="Rockwell" ForeColor="#487B5C">
                                                                                    <BorderBottom BorderStyle="Solid" />
                                                                                </dx:ASPxLabel>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-bottom: 5px; padding-top: 5px">
                                                                                <dx:ASPxLabel runat="server" Text="CALCULO COSTO DE INSPECCION EN DESTINO FINAL" Width="100%" style="padding-bottom: 5px" Font-Bold="True" Font-Names="Rockwell" ForeColor="#487B5C">
                                                                                    <BorderBottom BorderStyle="Solid" />
                                                                                </dx:ASPxLabel>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-bottom: 5px; padding-top: 5px">
                                                                                <dx:ASPxGridView ID="dgvAnexoD3" runat="server" Theme="Default" AutoGenerateColumns="False"  EnableTheming="True" Font-Size="Smaller" ClientInstanceName="dgvAnexoD3" Width="100%" >
                                                                                    <SettingsContextMenu EnableRowMenu="False">
                                                                                    </SettingsContextMenu>
                                                                                    <SettingsPager PageSize="100">
                                                                                        <PageSizeItemSettings Items="50, 100, 200, 500, 1000" Visible="True">
                                                                                        </PageSizeItemSettings>
                                                                                    </SettingsPager>
                                                                                    <SettingsEditing Mode="PopupEditForm">
                                                                                    </SettingsEditing>
                                                                                    <Settings ShowFooter="True" ShowGroupFooter="VisibleAlways" ShowGroupPanel="True" ShowHeaderFilterButton="True" />
                                                                                    <SettingsBehavior AllowFocusedRow="True" />
                                                                                    <SettingsPopup>
                                                                                        <EditForm Modal="True" />
                                                                                    </SettingsPopup>
                                                                                    <SettingsText CommandNew="Nuevo" />
                                                                                    <Columns>
                                                                                        <dx:GridViewDataTextColumn Caption="MES DE RECEPCION" FieldName="MES_CARGA" ShowInCustomizationForm="True" VisibleIndex="0" ToolTip="Concepto">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="DUI" FieldName="NRO_DUI" ShowInCustomizationForm="True" VisibleIndex="1" ToolTip="Costo de Producto">
                                                                                            <Settings AutoFilterCondition="Contains" /> 
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="PROVEEDOR" FieldName="PROVEEDOR" ShowInCustomizationForm="True" VisibleIndex="2" ToolTip="Costo de Transporte">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="Nº FACTURA PROVEEDOR" FieldName="NRO_FACTURA" ShowInCustomizationForm="True" VisibleIndex="3" ToolTip="Costo de Seguro">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="VOLUMEN RECIBIDO (Lt)" FieldName="VOLUMEN_FACTURADO" ShowInCustomizationForm="True" VisibleIndex="4" ToolTip="Costo Aduanero" PropertiesTextEdit-DisplayFormatString="F">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn> 
                                                                                        <dx:GridViewDataTextColumn Caption="MONTO INSPECCION DESTINO FINAL (Bs)" FieldName="MONTO_INSPECCION" ShowInCustomizationForm="True" VisibleIndex="5" ToolTip="Costo Aduanero" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>                                                                                                                                                                                                                                                                                                                                                              
                                                                                    </Columns>
                                                                                    <TotalSummary>
                                                                                            <dx:ASPxSummaryItem FieldName="MONTO_INSPECCION" ShowInColumn="MONTO_INSPECCION" SummaryType="Sum" Tag="MONTO_INSPECCION" DisplayFormat="F2" />
                                                                                    </TotalSummary>
                                                                                    <Styles CssPostfix="DevGlp"><AlternatingRow BackColor="#EEEEEE">
                                                                                        </AlternatingRow></Styles>
                                                                                </dx:ASPxGridView>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-bottom: 5px; padding-top: 5px">
                                                                                <dx:ASPxLabel ID="lblAnexoD" runat="server" Width="100%" style="padding-bottom: 5px" Font-Bold="True" Font-Names="Rockwell">                                                                                    
                                                                                </dx:ASPxLabel>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </dx:ContentControl>
                                                            </ContentCollection>
                                                        </dx:TabPage> 
                                                        
                                                        <dx:TabPage Text="ANEXO E">
                                                            <ContentCollection>
                                                                <dx:ContentControl runat="server">
                                                                    <table style="width: 100%">
                                                                        <tr>
                                                                            <td style="padding-bottom: 5px; padding-top: 5px">
                                                                                <dx:ASPxLabel runat="server" Text="ANEXO E" Width="100%" style="padding-bottom: 5px" Font-Bold="True" Font-Names="Rockwell" ForeColor="#487B5C">
                                                                                    <BorderBottom BorderStyle="Solid" />
                                                                                </dx:ASPxLabel>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-bottom: 5px; padding-top: 5px">
                                                                                <dx:ASPxLabel runat="server" Text="CALCULO COSTO ADUANERO" Width="100%" style="padding-bottom: 5px" Font-Bold="True" Font-Names="Rockwell" ForeColor="#487B5C">
                                                                                    <BorderBottom BorderStyle="Solid" />
                                                                                </dx:ASPxLabel>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-bottom: 5px; padding-top: 5px">
                                                                                <dx:ASPxGridView ID="dgvAnexoE" runat="server" Theme="Default" AutoGenerateColumns="False"  EnableTheming="True" Font-Size="Smaller" ClientInstanceName="dgvAnexoE" Width="100%" >
                                                                                    <SettingsContextMenu EnableRowMenu="False">
                                                                                    </SettingsContextMenu>
                                                                                    <SettingsPager PageSize="100">
                                                                                        <PageSizeItemSettings Items="50, 100, 200, 500, 1000" Visible="True">
                                                                                        </PageSizeItemSettings>
                                                                                    </SettingsPager>
                                                                                    <SettingsEditing Mode="PopupEditForm">
                                                                                    </SettingsEditing>
                                                                                    <Settings ShowFooter="True" ShowGroupFooter="VisibleAlways" ShowGroupPanel="True" ShowHeaderFilterButton="True" />
                                                                                    <SettingsBehavior AllowFocusedRow="True" />
                                                                                    <SettingsPopup>
                                                                                        <EditForm Modal="True" />
                                                                                    </SettingsPopup>
                                                                                    <SettingsText CommandNew="Nuevo" />
                                                                                    <Columns>
                                                                                        <dx:GridViewDataTextColumn Caption="DUI" FieldName="NRO_DUI" ShowInCustomizationForm="True" VisibleIndex="0" ToolTip="Concepto">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="GRAVAMEN ARANCELARIO (10%)" FieldName="GRAVAMEN" ShowInCustomizationForm="True" VisibleIndex="1" ToolTip="Costo de Producto" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" /> 
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="Form SIDUINEA (Bs)" FieldName="SIDUINEA" ShowInCustomizationForm="True" VisibleIndex="2" ToolTip="Costo de Transporte" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="COSTO DEPOSITOS ADUANEROS" FieldName="COSTO_DEPOSITO" ShowInCustomizationForm="True" VisibleIndex="3" ToolTip="Costo de Seguro" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="TOTAL COSTOS" FieldName="TOTAL_COSTO" ShowInCustomizationForm="True" VisibleIndex="4" ToolTip="Costo de Inspeccion" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>                                                                                                                                                                                                                                                                                                                                                                                                                                                      
                                                                                    </Columns>
                                                                                    <TotalSummary>
                                                                                            <dx:ASPxSummaryItem FieldName="TOTAL_COSTO" ShowInColumn="TOTAL_COSTO" SummaryType="Sum" Tag="TOTAL_COSTO" DisplayFormat="F2" />
                                                                                    </TotalSummary>
                                                                                    <Styles CssPostfix="DevGlp"><AlternatingRow BackColor="#EEEEEE">
                                                                                        </AlternatingRow></Styles>
                                                                                </dx:ASPxGridView>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-bottom: 5px; padding-top: 5px">
                                                                                <dx:ASPxLabel ID="lblAnexoE" runat="server" Width="100%" style="padding-bottom: 5px" Font-Bold="True" Font-Names="Rockwell">                                                                                    
                                                                                </dx:ASPxLabel>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </dx:ContentControl>
                                                            </ContentCollection>
                                                        </dx:TabPage>  
                                                        
                                                        <dx:TabPage Text="ANEXO F">
                                                            <ContentCollection>
                                                                <dx:ContentControl runat="server">
                                                                    <table style="width: 100%">
                                                                        <tr>
                                                                            <td style="padding-bottom: 5px; padding-top: 5px">
                                                                                <dx:ASPxLabel runat="server" Text="ANEXO F" Width="100%" style="padding-bottom: 5px" Font-Bold="True" Font-Names="Rockwell" ForeColor="#487B5C">
                                                                                    <BorderBottom BorderStyle="Solid" />
                                                                                </dx:ASPxLabel>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-bottom: 5px; padding-top: 5px">
                                                                                <dx:ASPxLabel runat="server" Text="CALCULO COSTOS FINANCIEROS" Width="100%" style="padding-bottom: 5px" Font-Bold="True" Font-Names="Rockwell" ForeColor="#487B5C">
                                                                                    <BorderBottom BorderStyle="Solid" />
                                                                                </dx:ASPxLabel>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-bottom: 5px; padding-top: 5px">
                                                                                <dx:ASPxLabel runat="server" Text="RUBRO 1: Cobro por las transferencias al exterior por pago de PRODUCTO:" Width="100%" style="padding-bottom: 5px" Font-Bold="True" Font-Names="Rockwell" ForeColor="#487B5C">
                                                                                    <BorderBottom BorderStyle="Solid" />
                                                                                </dx:ASPxLabel>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-bottom: 5px; padding-top: 5px">
                                                                                <dx:ASPxGridView ID="dgvCostoFinancieroProducto" runat="server" Theme="Default" AutoGenerateColumns="False"  EnableTheming="True" Font-Size="Smaller" ClientInstanceName="dgvTransacciones" Width="100%" >
                                                                                    <SettingsContextMenu EnableRowMenu="False">
                                                                                    </SettingsContextMenu>
                                                                                    <SettingsPager PageSize="100">
                                                                                        <PageSizeItemSettings Items="50, 100, 200, 500, 1000" Visible="True">
                                                                                        </PageSizeItemSettings>
                                                                                    </SettingsPager>
                                                                                    <SettingsEditing Mode="PopupEditForm">
                                                                                    </SettingsEditing>
                                                                                    <Settings ShowFooter="True" ShowGroupFooter="VisibleAlways" ShowGroupPanel="True" ShowHeaderFilterButton="True" />
                                                                                    <SettingsBehavior AllowFocusedRow="True" />
                                                                                    <SettingsPopup>
                                                                                        <EditForm Modal="True" />
                                                                                    </SettingsPopup>
                                                                                    <SettingsText CommandNew="Nuevo" />
                                                                                    <Columns>
                                                                                        <dx:GridViewDataTextColumn Caption="DUI" FieldName="NRO_DUI" ShowInCustomizationForm="True" VisibleIndex="0" ToolTip="Concepto">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="MES RECIBIDO" FieldName="MES_RECIBIDO" ShowInCustomizationForm="True" VisibleIndex="1" ToolTip="Concepto">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="PROVEEDOR" FieldName="PROVEEDOR" ShowInCustomizationForm="True" VisibleIndex="2" ToolTip="Costo de Producto">
                                                                                            <Settings AutoFilterCondition="Contains" /> 
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="Nº FACTURA PROVEEDOR" FieldName="NRO_FACTURA_PROVEEDOR" ShowInCustomizationForm="True" VisibleIndex="3" ToolTip="Costo de Transporte">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="MONTO FACTURA DEL PROVEEDOR (Bs)" FieldName="MONTO_FACTURA_PROVEEDOR" ShowInCustomizationForm="True" VisibleIndex="4" ToolTip="Costo de Seguro" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="COSTO FINANCIERO (Bs)" FieldName="COSTO_FINANCIERO" ShowInCustomizationForm="True" VisibleIndex="5" ToolTip="Costo de Inspeccion" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn> 
                                                                                        <dx:GridViewDataTextColumn Caption="COSTO FINANCIERO NETO DE IVA (Bs)" FieldName="COSTO_FINANCIERO_NETO" ShowInCustomizationForm="True" VisibleIndex="6" ToolTip="Costo de Seguro" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             
                                                                                    </Columns>
                                                                                    <TotalSummary>
                                                                                            <dx:ASPxSummaryItem FieldName="COSTO_FINANCIERO_NETO" ShowInColumn="COSTO_FINANCIERO_NETO" SummaryType="Sum" Tag="COSTO_FINANCIERO_NETO" DisplayFormat="F2" />
                                                                                    </TotalSummary>
                                                                                    <Styles CssPostfix="DevGlp"><AlternatingRow BackColor="#EEEEEE">
                                                                                        </AlternatingRow></Styles>
                                                                                </dx:ASPxGridView>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-bottom: 5px; padding-top: 5px">
                                                                                <dx:ASPxLabel runat="server" Text="RUBRO 2: Cobro por las transferencias al exterior por pago de SERVICIO:" Width="100%" style="padding-bottom: 5px" Font-Bold="True" Font-Names="Rockwell" ForeColor="#487B5C">
                                                                                    <BorderBottom BorderStyle="Solid" />
                                                                                </dx:ASPxLabel>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-bottom: 5px; padding-top: 5px">
                                                                                <dx:ASPxGridView ID="dgvCostoFinancieroServicio" runat="server" Theme="Default" AutoGenerateColumns="False" EnableTheming="True" Font-Size="Smaller" ClientInstanceName="dgvTransacciones" Width="100%" >
                                                                                    <SettingsContextMenu EnableRowMenu="False">
                                                                                    </SettingsContextMenu>
                                                                                    <SettingsPager PageSize="100">
                                                                                        <PageSizeItemSettings Items="50, 100, 200, 500, 1000" Visible="True">
                                                                                        </PageSizeItemSettings>
                                                                                    </SettingsPager>
                                                                                    <SettingsEditing Mode="PopupEditForm">
                                                                                    </SettingsEditing>
                                                                                    <Settings ShowFooter="True" ShowGroupFooter="VisibleAlways" ShowGroupPanel="True" ShowHeaderFilterButton="True" />
                                                                                    <SettingsBehavior AllowFocusedRow="True" />
                                                                                    <SettingsPopup>
                                                                                        <EditForm Modal="True" />
                                                                                    </SettingsPopup>
                                                                                    <SettingsText CommandNew="Nuevo" />
                                                                                    <Columns>
                                                                                        <dx:GridViewDataTextColumn Caption="DUI" FieldName="NRO_DUI" ShowInCustomizationForm="True" VisibleIndex="0" ToolTip="Concepto">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="MES RECIBIDO" FieldName="MES_RECIBIDO" ShowInCustomizationForm="True" VisibleIndex="1" ToolTip="Concepto">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="EMPRESA DE SERVICIO" FieldName="PROVEEDOR" ShowInCustomizationForm="True" VisibleIndex="2" ToolTip="Costo de Producto">
                                                                                            <Settings AutoFilterCondition="Contains" /> 
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="Nº FACTURA SERVICIO" FieldName="NRO_FACTURA_PROVEEDOR" ShowInCustomizationForm="True" VisibleIndex="3" ToolTip="Costo de Transporte">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="MONTO FACTURA DEL SERVICIO (Bs)" FieldName="MONTO_FACTURA_PROVEEDOR" ShowInCustomizationForm="True" VisibleIndex="4" ToolTip="Costo de Seguro" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="COSTO FINANCIERO (Bs)" FieldName="COSTO_FINANCIERO" ShowInCustomizationForm="True" VisibleIndex="5" ToolTip="Costo de Inspeccion" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn> 
                                                                                        <dx:GridViewDataTextColumn Caption="COSTO FINANCIERO NETO DE IVA (Bs)" FieldName="COSTO_FINANCIERO_NETO" ShowInCustomizationForm="True" VisibleIndex="6" ToolTip="Costo de Seguro" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             
                                                                                    </Columns>
                                                                                    <TotalSummary>
                                                                                            <dx:ASPxSummaryItem FieldName="COSTO_FINANCIERO_NETO" ShowInColumn="COSTO_FINANCIERO_NETO" SummaryType="Sum" Tag="COSTO_FINANCIERO_NETO" DisplayFormat="F2" />
                                                                                    </TotalSummary>
                                                                                    <Styles CssPostfix="DevGlp"><AlternatingRow BackColor="#EEEEEE">
                                                                                        </AlternatingRow></Styles>
                                                                                </dx:ASPxGridView>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-bottom: 5px; padding-top: 5px">
                                                                                <dx:ASPxLabel ID="lblAnexoF" runat="server" Width="100%" style="padding-bottom: 5px" Font-Bold="True" Font-Names="Rockwell">                                                                                    
                                                                                </dx:ASPxLabel>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </dx:ContentControl>
                                                            </ContentCollection>
                                                        </dx:TabPage>  
                                                        
                                                        <dx:TabPage Text="ANEXO G">
                                                            <ContentCollection>
                                                                <dx:ContentControl runat="server">
                                                                    <table style="width: 100%">
                                                                        <tr>
                                                                            <td style="padding-bottom: 5px; padding-top: 5px">
                                                                                <dx:ASPxLabel runat="server" Text="ANEXO G" Width="100%" style="padding-bottom: 5px" Font-Bold="True" Font-Names="Rockwell" ForeColor="#487B5C">
                                                                                    <BorderBottom BorderStyle="Solid" />
                                                                                </dx:ASPxLabel> 
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-bottom: 5px; padding-top: 5px">
                                                                                <dx:ASPxLabel runat="server" Text="RUBRO 1: CALCULO COSTOS DE ALMACENAJE INTERMEDIO:" Width="100%" style="padding-bottom: 5px" Font-Bold="True" Font-Names="Rockwell" ForeColor="#487B5C">
                                                                                    <BorderBottom BorderStyle="Solid" />
                                                                                </dx:ASPxLabel>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-bottom: 5px; padding-top: 5px">
                                                                                <dx:ASPxGridView ID="dgvAnexoG1" runat="server" Theme="Default" AutoGenerateColumns="False"  EnableTheming="True" Font-Size="Smaller" ClientInstanceName="dgvAnexoG1" Width="100%" >
                                                                                    <SettingsContextMenu EnableRowMenu="False">
                                                                                    </SettingsContextMenu>
                                                                                    <SettingsPager PageSize="100">
                                                                                        <PageSizeItemSettings Items="50, 100, 200, 500, 1000" Visible="True">
                                                                                        </PageSizeItemSettings>
                                                                                    </SettingsPager>
                                                                                    <SettingsEditing Mode="PopupEditForm">
                                                                                    </SettingsEditing>
                                                                                    <Settings ShowFooter="True" ShowGroupFooter="VisibleAlways" ShowGroupPanel="True" ShowHeaderFilterButton="True" />
                                                                                    <SettingsBehavior AllowFocusedRow="True" />
                                                                                    <SettingsPopup>
                                                                                        <EditForm Modal="True" />
                                                                                    </SettingsPopup>
                                                                                    <SettingsText CommandNew="Nuevo" />
                                                                                    <Columns>
                                                                                        <dx:GridViewDataTextColumn Caption="DUI" FieldName="NRO_DUI" ShowInCustomizationForm="True" VisibleIndex="0" ToolTip="Concepto">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="MES" FieldName="MES" ShowInCustomizationForm="True" VisibleIndex="1" ToolTip="Costo de Producto">
                                                                                            <Settings AutoFilterCondition="Contains" /> 
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="EMPRESA DE ALMACENAJE" FieldName="EMPRESA_ALMACENAJE" ShowInCustomizationForm="True" VisibleIndex="2" ToolTip="Costo de Transporte">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="Nº FACTURA ALMACENAJE" FieldName="NRO_FACTURA_ALMACENAJE" ShowInCustomizationForm="True" VisibleIndex="3" ToolTip="Costo de Seguro">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="TARIFA UNITARIA ALMACENAJE (Bs/Lt)" FieldName="TARIFA_UNITARIA" ShowInCustomizationForm="True" VisibleIndex="4" ToolTip="Costo de Inspeccion" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn> 
                                                                                        <dx:GridViewDataTextColumn Caption="VOLUMEN RECIBIDO (Lt)" FieldName="VOLUMEN_RECIBIDO" ShowInCustomizationForm="True" VisibleIndex="5" ToolTip="Costo de Seguro" PropertiesTextEdit-DisplayFormatString="F">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn> 
                                                                                        <dx:GridViewDataTextColumn Caption="COSTO ALMACENAJE (Bs)" FieldName="COSTO_ALMACENAJE" ShowInCustomizationForm="True" VisibleIndex="6" ToolTip="Costo de Inspeccion" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn> 
                                                                                        <dx:GridViewDataTextColumn Caption="MONTO NETO DE IVA (Bs)" FieldName="MONTO_NETO_IVA" ShowInCustomizationForm="True" VisibleIndex="7" ToolTip="Costo de Seguro" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             
                                                                                    </Columns>
                                                                                    <TotalSummary>
                                                                                            <dx:ASPxSummaryItem FieldName="MONTO_NETO_IVA" ShowInColumn="MONTO_NETO_IVA" SummaryType="Sum" Tag="MONTO_NETO_IVA" DisplayFormat="F2" />
                                                                                    </TotalSummary>
                                                                                    <Styles CssPostfix="DevGlp"><AlternatingRow BackColor="#EEEEEE">
                                                                                        </AlternatingRow></Styles>
                                                                                </dx:ASPxGridView>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-bottom: 5px; padding-top: 5px">
                                                                                <dx:ASPxLabel runat="server" Text="RUBRO 2: CALCULO COSTOS DE ALMACENAJE DESTINO FINAL" Width="100%" style="padding-bottom: 5px" Font-Bold="True" Font-Names="Rockwell" ForeColor="#487B5C">
                                                                                    <BorderBottom BorderStyle="Solid" />
                                                                                </dx:ASPxLabel>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-bottom: 5px; padding-top: 5px">
                                                                                <dx:ASPxGridView ID="dgvAnexoG2" runat="server" Theme="Default" AutoGenerateColumns="False"  EnableTheming="True" Font-Size="Smaller" ClientInstanceName="dgvAnexoG2" Width="100%" >
                                                                                    <SettingsContextMenu EnableRowMenu="False">
                                                                                    </SettingsContextMenu>
                                                                                    <SettingsPager PageSize="100">
                                                                                        <PageSizeItemSettings Items="50, 100, 200, 500, 1000" Visible="True">
                                                                                        </PageSizeItemSettings>
                                                                                    </SettingsPager>
                                                                                    <SettingsEditing Mode="PopupEditForm">
                                                                                    </SettingsEditing>
                                                                                    <Settings ShowFooter="True" ShowGroupFooter="VisibleAlways" ShowGroupPanel="True" ShowHeaderFilterButton="True" />
                                                                                    <SettingsBehavior AllowFocusedRow="True" />
                                                                                    <SettingsPopup>
                                                                                        <EditForm Modal="True" />
                                                                                    </SettingsPopup>
                                                                                    <SettingsText CommandNew="Nuevo" />
                                                                                    <Columns>
                                                                                        <dx:GridViewDataTextColumn Caption="DUI" FieldName="NRO_DUI" ShowInCustomizationForm="True" VisibleIndex="0" ToolTip="Concepto">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="MES" FieldName="MES" ShowInCustomizationForm="True" VisibleIndex="1" ToolTip="Costo de Producto">
                                                                                            <Settings AutoFilterCondition="Contains" /> 
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="EMPRESA DE ALMACENAJE" FieldName="EMPRESA_ALMACENAJE" ShowInCustomizationForm="True" VisibleIndex="2" ToolTip="Costo de Transporte">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="Nº FACTURA ALMACENAJE" FieldName="NRO_FACTURA_ALMACENAJE" ShowInCustomizationForm="True" VisibleIndex="3" ToolTip="Costo de Seguro">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="TARIFA UNITARIA ALMACENAJE (Bs/Lt)" FieldName="TARIFA_UNITARIA" ShowInCustomizationForm="True" VisibleIndex="4" ToolTip="Costo de Inspeccion" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn> 
                                                                                        <dx:GridViewDataTextColumn Caption="VOLUMEN RECIBIDO (Lt)" FieldName="VOLUMEN_RECIBIDO" ShowInCustomizationForm="True" VisibleIndex="5" ToolTip="Costo de Seguro" PropertiesTextEdit-DisplayFormatString="F">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn> 
                                                                                        <dx:GridViewDataTextColumn Caption="MONTO FACTURADO ALMACENAJE (Bs)" FieldName="COSTO_ALMACENAJE" ShowInCustomizationForm="True" VisibleIndex="6" ToolTip="Costo de Inspeccion" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn> 
                                                                                        <dx:GridViewDataTextColumn Caption="MONTO NETO DE IVA (Bs)" FieldName="MONTO_NETO_IVA" ShowInCustomizationForm="True" VisibleIndex="7" ToolTip="Costo de Seguro" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             
                                                                                    </Columns>
                                                                                    <TotalSummary>
                                                                                            <dx:ASPxSummaryItem FieldName="MONTO_NETO_IVA" ShowInColumn="MONTO_NETO_IVA" SummaryType="Sum" Tag="MONTO_NETO_IVA" DisplayFormat="F2" />
                                                                                    </TotalSummary>
                                                                                    <Styles CssPostfix="DevGlp"><AlternatingRow BackColor="#EEEEEE">
                                                                                        </AlternatingRow></Styles>
                                                                                </dx:ASPxGridView>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-bottom: 5px; padding-top: 5px">
                                                                                <dx:ASPxLabel ID="lblAnexoG" runat="server" Width="100%" style="padding-bottom: 5px" Font-Bold="True" Font-Names="Rockwell">                                                                                    
                                                                                </dx:ASPxLabel>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </dx:ContentControl>
                                                            </ContentCollection>
                                                        </dx:TabPage>  
                                                        
                                                        <dx:TabPage Text="ANEXO H">
                                                            <ContentCollection>
                                                                <dx:ContentControl runat="server">
                                                                    <table style="width: 100%">
                                                                        <tr>
                                                                            <td style="padding-bottom: 5px; padding-top: 5px">
                                                                                <dx:ASPxLabel runat="server" Text="ANEXO H" Width="100%" style="padding-bottom: 5px" Font-Bold="True" Font-Names="Rockwell" ForeColor="#487B5C">
                                                                                    <BorderBottom BorderStyle="Solid" />
                                                                                </dx:ASPxLabel>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-bottom: 5px; padding-top: 5px">
                                                                                <dx:ASPxLabel runat="server" Text="COSTO DE MONITOREO" Width="100%" style="padding-bottom: 5px" Font-Bold="True" Font-Names="Rockwell" ForeColor="#487B5C">
                                                                                    <BorderBottom BorderStyle="Solid" />
                                                                                </dx:ASPxLabel>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-bottom: 5px; padding-top: 5px">
                                                                                <dx:ASPxGridView ID="dgvAnexoH" runat="server" Theme="Default" AutoGenerateColumns="False"  EnableTheming="True" Font-Size="Smaller" ClientInstanceName="dgvAnexoH" Width="100%" >
                                                                                    <SettingsContextMenu EnableRowMenu="False">
                                                                                    </SettingsContextMenu>
                                                                                    <SettingsPager PageSize="100">
                                                                                        <PageSizeItemSettings Items="50, 100, 200, 500, 1000" Visible="True">
                                                                                        </PageSizeItemSettings>
                                                                                    </SettingsPager>
                                                                                    <SettingsEditing Mode="PopupEditForm">
                                                                                    </SettingsEditing>
                                                                                    <Settings ShowFooter="True" ShowGroupFooter="VisibleAlways" ShowGroupPanel="True" ShowHeaderFilterButton="True" />
                                                                                    <SettingsBehavior AllowFocusedRow="True" />
                                                                                    <SettingsPopup>
                                                                                        <EditForm Modal="True" />
                                                                                    </SettingsPopup>
                                                                                    <SettingsText CommandNew="Nuevo" />
                                                                                    <Columns>
                                                                                        <dx:GridViewDataTextColumn Caption="MES" FieldName="MES" ShowInCustomizationForm="True" VisibleIndex="0" ToolTip="Concepto">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="Nº DUI" FieldName="NRO_DUI" ShowInCustomizationForm="True" VisibleIndex="1" ToolTip="Costo de Producto">
                                                                                            <Settings AutoFilterCondition="Contains" /> 
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="Nº FACTURA PROVEEDOR" FieldName="NRO_FACTURA_PROVEEDOR" ShowInCustomizationForm="True" VisibleIndex="2" ToolTip="Costo de Transporte">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="VOLUMEN RECIBIDO (Lt)" FieldName="VOLUMEN_RECIBIDO" ShowInCustomizationForm="True" VisibleIndex="3" ToolTip="Costo de Seguro" PropertiesTextEdit-DisplayFormatString="F">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="MONTO DE MONITOREO (Bs)" FieldName="MONTO_MONITOREO" ShowInCustomizationForm="True" VisibleIndex="4" ToolTip="Costo de Inspeccion" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     
                                                                                    </Columns>
                                                                                    <TotalSummary>
                                                                                            <dx:ASPxSummaryItem FieldName="MONTO_MONITOREO" ShowInColumn="MONTO_MONITOREO" SummaryType="Sum" Tag="MONTO_MONITOREO" DisplayFormat="F2" />
                                                                                    </TotalSummary>
                                                                                    <Styles CssPostfix="DevGlp"><AlternatingRow BackColor="#EEEEEE">
                                                                                        </AlternatingRow></Styles>
                                                                                </dx:ASPxGridView>
                                                                            </td>
                                                                        </tr> 
                                                                        <tr>
                                                                            <td style="padding-bottom: 5px; padding-top: 5px">
                                                                                <dx:ASPxLabel ID="lblAnexoH" runat="server" Width="100%" style="padding-bottom: 5px" Font-Bold="True" Font-Names="Rockwell">                                                                                    
                                                                                </dx:ASPxLabel>
                                                                            </td>
                                                                        </tr>                                                                       
                                                                    </table>
                                                                </dx:ContentControl>
                                                            </ContentCollection>
                                                        </dx:TabPage>  
                                                        
                                                        <dx:TabPage Text="ANEXO I">
                                                            <ContentCollection>
                                                                <dx:ContentControl runat="server">
                                                                    <table style="width: 100%">
                                                                        <tr>
                                                                            <td style="padding-bottom: 5px; padding-top: 5px">
                                                                                <dx:ASPxLabel runat="server" Text="ANEXO I" Width="100%" style="padding-bottom: 5px" Font-Bold="True" Font-Names="Rockwell" ForeColor="#487B5C">
                                                                                    <BorderBottom BorderStyle="Solid" />
                                                                                </dx:ASPxLabel>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-bottom: 5px; padding-top: 5px">
                                                                                <dx:ASPxLabel runat="server" Text="Rubro 1: Volumenes en Destino Intermedio" Width="100%" style="padding-bottom: 5px" Font-Bold="True" Font-Names="Rockwell" ForeColor="#487B5C">
                                                                                    <BorderBottom BorderStyle="Solid" />
                                                                                </dx:ASPxLabel>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-bottom: 5px; padding-top: 5px">
                                                                                <dx:ASPxGridView ID="dgvAnexoI1" runat="server" Theme="Default" AutoGenerateColumns="False"  EnableTheming="True" Font-Size="Smaller" ClientInstanceName="dgvAnexoI1" Width="100%" >
                                                                                    <SettingsContextMenu EnableRowMenu="False">
                                                                                    </SettingsContextMenu>
                                                                                    <SettingsPager PageSize="100">
                                                                                        <PageSizeItemSettings Items="50, 100, 200, 500, 1000" Visible="True">
                                                                                        </PageSizeItemSettings>
                                                                                    </SettingsPager>
                                                                                    <SettingsEditing Mode="PopupEditForm">
                                                                                    </SettingsEditing>
                                                                                    <Settings ShowFooter="True" ShowGroupFooter="VisibleAlways" ShowGroupPanel="True" ShowHeaderFilterButton="True" />
                                                                                    <SettingsBehavior AllowFocusedRow="True" />
                                                                                    <SettingsPopup>
                                                                                        <EditForm Modal="True" />
                                                                                    </SettingsPopup>
                                                                                    <SettingsText CommandNew="Nuevo" />
                                                                                    <Columns>
                                                                                        <dx:GridViewDataTextColumn Caption="DUI" FieldName="NRO_DUI" ShowInCustomizationForm="True" VisibleIndex="0" ToolTip="Concepto">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="PROVEEDOR" FieldName="PROVEEDOR" ShowInCustomizationForm="True" VisibleIndex="1" ToolTip="Costo de Producto">
                                                                                            <Settings AutoFilterCondition="Contains" /> 
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="Nº FACTURA DEL PROVEEDOR" FieldName="NRO_FACTURA_PROVEEDOR" ShowInCustomizationForm="True" VisibleIndex="2" ToolTip="Costo de Transporte">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="MES DE RECEPCION" FieldName="MES_RECEPCION" ShowInCustomizationForm="True" VisibleIndex="3" ToolTip="Costo de Seguro">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="PLANTA" FieldName="PLANTA" ShowInCustomizationForm="True" VisibleIndex="4" ToolTip="Costo de Inspeccion">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn> 
                                                                                        <dx:GridViewDataTextColumn Caption="VOLUMENES (Lt)" FieldName="VOLUMEN" ShowInCustomizationForm="True" VisibleIndex="5" ToolTip="Costo de Seguro" PropertiesTextEdit-DisplayFormatString="F">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   
                                                                                    </Columns>
                                                                                    <TotalSummary>
                                                                                            <dx:ASPxSummaryItem FieldName="VOLUMEN" ShowInColumn="VOLUMEN" SummaryType="Sum" Tag="VOLUMEN" DisplayFormat="F2" />
                                                                                    </TotalSummary>
                                                                                    <Styles CssPostfix="DevGlp"><AlternatingRow BackColor="#EEEEEE">
                                                                                        </AlternatingRow></Styles>
                                                                                </dx:ASPxGridView>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-bottom: 5px; padding-top: 5px">
                                                                                <dx:ASPxLabel runat="server" Text="Rubro 2: Volumenes en Destino Final" Width="100%" style="padding-bottom: 5px" Font-Bold="True" Font-Names="Rockwell" ForeColor="#487B5C">
                                                                                    <BorderBottom BorderStyle="Solid" />
                                                                                </dx:ASPxLabel>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-bottom: 5px; padding-top: 5px">
                                                                                <dx:ASPxGridView ID="dgvAnexoI2" runat="server" Theme="Default" AutoGenerateColumns="False" EnableTheming="True" Font-Size="Smaller" ClientInstanceName="dgvAnexoI2" Width="100%" >
                                                                                    <SettingsContextMenu EnableRowMenu="False">
                                                                                    </SettingsContextMenu>
                                                                                    <SettingsPager PageSize="100">
                                                                                        <PageSizeItemSettings Items="50, 100, 200, 500, 1000" Visible="True">
                                                                                        </PageSizeItemSettings>
                                                                                    </SettingsPager>
                                                                                    <SettingsEditing Mode="PopupEditForm">
                                                                                    </SettingsEditing>
                                                                                    <Settings ShowFooter="True" ShowGroupFooter="VisibleAlways" ShowGroupPanel="True" ShowHeaderFilterButton="True" />
                                                                                    <SettingsBehavior AllowFocusedRow="True" />
                                                                                    <SettingsPopup>
                                                                                        <EditForm Modal="True" />
                                                                                    </SettingsPopup>
                                                                                    <SettingsText CommandNew="Nuevo" />
                                                                                    <Columns>
                                                                                        <dx:GridViewDataTextColumn Caption="DUI" FieldName="NRO_DUI" ShowInCustomizationForm="True" VisibleIndex="0" ToolTip="Concepto">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="PROVEEDOR" FieldName="PROVEEDOR" ShowInCustomizationForm="True" VisibleIndex="1" ToolTip="Costo de Producto">
                                                                                            <Settings AutoFilterCondition="Contains" /> 
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="Nº FACTURA DEL PROVEEDOR" FieldName="NRO_FACTURA_PROVEEDOR" ShowInCustomizationForm="True" VisibleIndex="2" ToolTip="Costo de Transporte">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="MES DE RECEPCION" FieldName="MES_RECEPCION" ShowInCustomizationForm="True" VisibleIndex="3" ToolTip="Costo de Seguro">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="PLANTA" FieldName="PLANTA" ShowInCustomizationForm="True" VisibleIndex="4" ToolTip="Costo de Inspeccion">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn> 
                                                                                        <dx:GridViewDataTextColumn Caption="VOLUMENES (Lt)" FieldName="VOLUMEN" ShowInCustomizationForm="True" VisibleIndex="5" ToolTip="Costo de Seguro" PropertiesTextEdit-DisplayFormatString="F">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   
                                                                                    </Columns>
                                                                                    <TotalSummary>
                                                                                            <dx:ASPxSummaryItem FieldName="VOLUMEN" ShowInColumn="VOLUMEN" SummaryType="Sum" Tag="VOLUMEN" DisplayFormat="F2" />
                                                                                    </TotalSummary>
                                                                                    <Styles CssPostfix="DevGlp"><AlternatingRow BackColor="#EEEEEE">
                                                                                        </AlternatingRow></Styles>
                                                                                </dx:ASPxGridView>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-bottom: 5px; padding-top: 5px">
                                                                                <dx:ASPxLabel ID="lblAnexoI" runat="server" Width="100%" style="padding-bottom: 5px" Font-Bold="True" Font-Names="Rockwell">                                                                                    
                                                                                </dx:ASPxLabel>
                                                                            </td>
                                                                        </tr>

                                                                        <tr>
                                                                            <td style="padding-bottom: 5px; padding-top: 5px">
                                                                                <dx:ASPxLabel ID="lblIcRubroGb" ClientInstanceName="lblIcRubroGb" runat="server" Text="Rubro 3: Volumen Gasolina Blanca" Width="100%" style="padding-bottom: 5px" Font-Bold="True" Font-Names="Rockwell" ForeColor="#487B5C">
                                                                                    <BorderBottom BorderStyle="Solid" />
                                                                                </dx:ASPxLabel>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-bottom: 5px; padding-top: 5px">
                                                                                <dx:ASPxGridView ID="dgvAnexoIGb" runat="server" Theme="Default" AutoGenerateColumns="False" EnableTheming="True" Font-Size="Smaller" ClientInstanceName="dgvAnexoIGb" Width="100%" >
                                                                                    <SettingsContextMenu EnableRowMenu="False">
                                                                                    </SettingsContextMenu>                                                                                   
                                                                                    <SettingsEditing Mode="PopupEditForm">
                                                                                    </SettingsEditing>
                                                                                    <Settings ShowFooter="True" ShowGroupFooter="VisibleAlways" ShowGroupPanel="True" ShowHeaderFilterButton="True" />
                                                                                    <SettingsBehavior AllowFocusedRow="True" />
                                                                                    <SettingsPopup>
                                                                                        <EditForm Modal="True" />
                                                                                    </SettingsPopup>
                                                                                    <SettingsText CommandNew="Nuevo" />
                                                                                    <Columns>
                                                                                        <dx:GridViewDataTextColumn Caption="MES RECIBIDO" FieldName="MES_RECIBIDO" ShowInCustomizationForm="True" VisibleIndex="0" ToolTip="Concepto">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="PLANTA" FieldName="PLANTA" ShowInCustomizationForm="True" VisibleIndex="1" ToolTip="Costo de Producto">
                                                                                            <Settings AutoFilterCondition="Contains" /> 
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="VOLUMENES (lt)" FieldName="VOLUMEN" ShowInCustomizationForm="True" VisibleIndex="2" ToolTip="Costo de Transporte" PropertiesTextEdit-DisplayFormatString="F0">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           
                                                                                    </Columns>
                                                                                    <TotalSummary>
                                                                                            <dx:ASPxSummaryItem FieldName="VOLUMEN" ShowInColumn="VOLUMEN" SummaryType="Sum" Tag="VOLUMEN" DisplayFormat="F2" />
                                                                                    </TotalSummary>
                                                                                    <Styles CssPostfix="DevGlp"><AlternatingRow BackColor="#EEEEEE">
                                                                                        </AlternatingRow></Styles>
                                                                                </dx:ASPxGridView>
                                                                            </td>
                                                                        </tr>

                                                                        <tr>
                                                                            <td style="padding-bottom: 5px; padding-top: 5px">
                                                                                <dx:ASPxLabel ID="lblIcRuGbase" ClientInstanceName="lblIlblIcRuGbasecRubroGb" runat="server" Text="Rubro 3: Volumen Gasolina Blanca" Width="100%" style="padding-bottom: 5px" Font-Bold="True" Font-Names="Rockwell" ForeColor="#487B5C">
                                                                                    <BorderBottom BorderStyle="Solid" />
                                                                                </dx:ASPxLabel>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding-bottom: 5px; padding-top: 5px">
                                                                                <dx:ASPxGridView ID="dgvAnexoIGbase" runat="server" Theme="Default" AutoGenerateColumns="False"  EnableTheming="True" Font-Size="Smaller" ClientInstanceName="dgvAnexoIGbase" Width="100%" >
                                                                                    <SettingsContextMenu EnableRowMenu="False">
                                                                                    </SettingsContextMenu>                                                                                   
                                                                                    <SettingsEditing Mode="PopupEditForm">
                                                                                    </SettingsEditing>
                                                                                    <Settings ShowFooter="True" ShowGroupFooter="VisibleAlways" ShowGroupPanel="True" ShowHeaderFilterButton="True" />
                                                                                    <SettingsBehavior AllowFocusedRow="True" />
                                                                                    <SettingsPopup>
                                                                                        <EditForm Modal="True" />
                                                                                    </SettingsPopup>
                                                                                    <SettingsText CommandNew="Nuevo" />
                                                                                    <Columns>
                                                                                        <dx:GridViewDataTextColumn Caption="MES RECIBIDO" FieldName="MES_RECIBIDO" ShowInCustomizationForm="True" VisibleIndex="0" ToolTip="Concepto">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="PLANTA" FieldName="PLANTA" ShowInCustomizationForm="True" VisibleIndex="1" ToolTip="Costo de Producto">
                                                                                            <Settings AutoFilterCondition="Contains" /> 
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="VI&A (Lt)" FieldName="DATO_1" ShowInCustomizationForm="True" VisibleIndex="2" ToolTip="Costo de Transporte" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="VGB (Lt)" FieldName="DATO_2" ShowInCustomizationForm="True" VisibleIndex="3" ToolTip="Costo de Producto" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" /> 
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="VTGBASE (Lt)" FieldName="DATO_3" ShowInCustomizationForm="True" VisibleIndex="4" ToolTip="Costo de Transporte" PropertiesTextEdit-DisplayFormatString="F2">
                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                        </dx:GridViewDataTextColumn>                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            
                                                                                    </Columns>
                                                                                    <TotalSummary>
                                                                                            <dx:ASPxSummaryItem FieldName="DATO_3" ShowInColumn="DATO_3" SummaryType="Sum" Tag="DATO_3" DisplayFormat="F2" />
                                                                                    </TotalSummary>
                                                                                    <Styles CssPostfix="DevGlp"><AlternatingRow BackColor="#EEEEEE">
                                                                                        </AlternatingRow></Styles>
                                                                                </dx:ASPxGridView>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </dx:ContentControl>
                                                            </ContentCollection>
                                                        </dx:TabPage>  
                                                                                                              
                                                    </TabPages>
                                                </dx:ASPxPageControl>
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