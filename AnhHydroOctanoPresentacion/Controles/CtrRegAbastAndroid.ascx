<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtrRegAbastAndroid.ascx.cs" Inherits="AnhHydroOctanoPresentacion.Controles.CtrRegAbastAndroid" %>

<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>

<div class="col-md-12">
    <table style="width: 100%; height: 100%">
        <tr>
            <td style="text-align: center; padding-bottom: 1px">
                <dx:ASPxRoundPanel runat="server" ShowHeader="False" Width="100%" ID="pnlTitulo">
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent1" runat="server">
                            <dx:ASPxLabel runat="server" ForeColor="#367B2F" ID="lblTitulo" Font-Bold="True" Font-Names="Rockwell" Font-Size="Larger" Text="REGISTRO DE ABASTECIMIENTO EN TIEMPO REAL"></dx:ASPxLabel>
                        </dx:PanelContent>
                    </PanelCollection>
                    <Border BorderStyle="Ridge" BorderWidth="1px" BorderColor="#003300"></Border>
                </dx:ASPxRoundPanel>
            </td>
        </tr>
        <tr>
            <td style="height: 100%">
                <dx:ASPxSplitter ID="ASPxSplitter1" runat="server" Height="98%" Width="100%">
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
                                                <dx:ASPxButton ID="btnBuscarProyecto" runat="server" AutoPostBack="false" ClientInstanceName="btnBuscarProyecto" Text="Buscar Proyecto">
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

                                                    <ClientSideEvents NodeDblClick="function(s, e) {
pnlDatos.PerformCallback();
}" />
                                                </dx:ASPxTreeList>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>&nbsp;</td>
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
	grafficardatosprinc(s, e);
	LoadingPanel.Hide();
}"
                                            BeginCallback="function(s, e) {
	LoadingPanel.Show();
}" />
                                        <PanelCollection>
                                            <dx:PanelContent ID="PanelContent3" runat="server">
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td colspan="7" style="text-align: center; width: 100%;">
                                                            <dx:ASPxRoundPanel ID="pnlMensaje" runat="server" ShowHeader="False" Visible="False" Width="100%">
                                                                <PanelCollection>
                                                                    <dx:PanelContent ID="PanelContent4" runat="server">
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
                                                        <td colspan="3" style="width: 50%">
                                                            <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="#10420F" Text="NOMBRE COMERCIAL"></asp:Label>
                                                        </td>
                                                        <td style="width: 15%">
                                                            <asp:Label ID="Label3" runat="server" Font-Bold="True" ForeColor="#10420F" Text="NIT"></asp:Label>
                                                        </td>
                                                        <td style="width: 15%">
                                                            <asp:Label ID="Label5" runat="server" Font-Bold="True" ForeColor="#10420F" Text="DEPARTAMENTO"></asp:Label>
                                                        </td>
                                                        <td colspan="2" style="width: 20%">
                                                            <asp:Label ID="Label4" runat="server" Font-Bold="True" ForeColor="#10420F" Text="DIRECCION"></asp:Label>
                                                        </td>

                                                    </tr>
                                                    <tr>
                                                        <td colspan="3" style="width: 50%">
                                                            <dx:ASPxLabel ID="lblRazonSocial" runat="server" ClientInstanceName="lblRazonSocial">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 15%">
                                                            <asp:Label ID="lblNit" runat="server"></asp:Label>
                                                        </td>
                                                        <td style="width: 15%">
                                                            <asp:Label ID="lblDepto" runat="server"></asp:Label>
                                                        </td>
                                                        <td colspan="2" style="width: 20%">
                                                            <asp:Label ID="lblRepLegal" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="7">

                                                            <dx:ASPxPageControl ID="pctPanel" runat="server" ActiveTabIndex="0" Font-Names="Verdana" Font-Size="Smaller" Theme="Metropolis" Width="100%">
                                                                <TabPages>
                                                                    <dx:TabPage Text="REGISTRO">
                                                                        <ContentCollection>
                                                                            <dx:ContentControl ID="ContentControl1" runat="server">
                                                                                <table style="width: 100%">
                                                                                    <tr>
                                                                                        <td style="width: 55%"></td>
                                                                                        <td style="width: 15%">&nbsp;</td>
                                                                                        <td style="width: 15%">&nbsp;</td>
                                                                                        <td style="width: 15%">
                                                                                            <table>
                                                                                                <tr>

                                                                                                    <td>&nbsp;</td>
                                                                                                    <td>
                                                                                                        <%--  <dx:ASPxButton ID="btnExportarAcum" runat="server" AutoPostBack="False" Font-Bold="True" ForeColor="Maroon" OnClick="btnExportarAcum_Click" Text="Exportar Excel" Theme="Metropolis" Width="200px">
                                                                                                                                                                                <ClientSideEvents Click="function(s, e) {
	                                                                                            e.processOnServer = confirm('¿Desea exportar el resumen acumulado?'); 
            }" />
                                                                                                <Image IconID="export_exporttoxls_16x16">
                                                                                                </Image>
                                                                                            </dx:ASPxButton>--%>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td colspan="4">
                                                                                            <table>
                                                                                                <tr style="text-align: right;">
                                                                                                    <td>Fecha de Medición :
                                                                                                    </td>
                                                                                                    <td style="width: 136px;">
                                                                                                        <dx:ASPxDateEdit ID="detFecha" runat="server" ReadOnly="true" ClientInstanceName="detFechaRegistro" EnableTheming="True" Height="23px" Theme="Metropolis" ToolTip="Seleccione la fecha Documento" Font-Bold="True" Width="90px">
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
                                                                                                    <td>Medicion de saldo en tiempo real:
                                                                                                    </td>
                                                                                                                                                                                                        
                                                                                                    <td>
                                                                                                        <dx:ASPxGridView ID="dgvProductoRecep" runat="server" AutoGenerateColumns="False" ClientInstanceName="dgvProductoRecep" EnableTheming="False" Font-Size="Smaller" KeyFieldName="ID_IDENTIFICADOR" Theme="Default" Width="100%">
                                                                                                            <SettingsContextMenu EnableRowMenu="False">
                                                                                                            </SettingsContextMenu>
                                                                                                            <Templates>
                                                                                                                <GroupFooterRow>
                                                                                                                    <dx:ASPxLabel ID="ASPxLabel54" runat="server" Text="ASPxLabel">
                                                                                                                    </dx:ASPxLabel>
                                                                                                                </GroupFooterRow>
                                                                                                                <EditForm>
                                                                                                                </EditForm>
                                                                                                            </Templates>
                                                                                                            <SettingsPager NumericButtonCount="5" PageSize="100">
                                                                                                                <PageSizeItemSettings Items="50, 100, 200, 500, 1000" Visible="False">
                                                                                                                </PageSizeItemSettings>
                                                                                                            </SettingsPager>
                                                                                                            <SettingsEditing Mode="Inline">
                                                                                                            </SettingsEditing>                                                                                                            
                                                                                                            <SettingsPopup>
                                                                                                                <EditForm HorizontalAlign="Center" Modal="True" VerticalAlign="Middle" VerticalOffset="0" />
                                                                                                                <CustomizationWindow HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" />
                                                                                                            </SettingsPopup>
                                                                                                            <SettingsText CommandNew="Nuevo" />
                                                                                                            <Columns>

                                                                                                                <dx:GridViewDataTextColumn Caption="ID_AUTORIZACION_CONDICION" FieldName="ID_AUTORIZACION_CONDICION" ShowInCustomizationForm="True" VisibleIndex="13" Visible="False">
                                                                                                                </dx:GridViewDataTextColumn>
                                                                                                                <dx:GridViewDataTextColumn Caption="ID_IDENTIFICADOR" FieldName="ID_IDENTIFICADOR" ShowInCustomizationForm="True" VisibleIndex="15" Visible="False">
                                                                                                                </dx:GridViewDataTextColumn>
                                                                                                                <dx:GridViewDataTextColumn Caption="PRODUCTO" FieldName="NOMBRE" ShowInCustomizationForm="True" VisibleIndex="16">
                                                                                                                </dx:GridViewDataTextColumn>
                                                                                                                <dx:GridViewDataTextColumn Caption="UNIDAD MEDIDA" FieldName="SUBID_NOMBRE" ShowInCustomizationForm="True" VisibleIndex="18">
                                                                                                                </dx:GridViewDataTextColumn>
                                                                                                                <dx:GridViewDataTextColumn Caption="CANTIDAD" FieldName="CODIGO" ShowInCustomizationForm="True" VisibleIndex="19" ToolTip="Cantidad">
                                                                                                                    <DataItemTemplate>
                                                                                                                        <dx:ASPxTextBox ID="txtCantidad" runat="server" Text='<%# Eval("CODIGO") %>' Width="90px">
                                                                                                                            <Border BorderStyle="None" />
                                                                                                                            <MaskSettings IncludeLiterals="DecimalSymbol" Mask="&lt;0..999999999&gt;.&lt;000000..999999&gt;" />
                                                                                                                            <ValidationSettings>
                                                                                                                                <RequiredField ErrorText="ERROR: Introduzca la cantidad." IsRequired="True" />
                                                                                                                            </ValidationSettings>
                                                                                                                            <ReadOnlyStyle BackColor="#DEE8DB">
                                                                                                                            </ReadOnlyStyle>
                                                                                                                            <FocusedStyle BackColor="#FFFFCC">
                                                                                                                            </FocusedStyle>
                                                                                                                            <DisabledStyle BackColor="#DEE8DB">
                                                                                                                            </DisabledStyle>
                                                                                                                        </dx:ASPxTextBox>
                                                                                                                    </DataItemTemplate>
                                                                                                                </dx:GridViewDataTextColumn>
                                                                                                                <dx:GridViewDataTextColumn Caption="OBSERV." FieldName="OBSERVACIONES" ShowInCustomizationForm="True" VisibleIndex="20" ToolTip="Observaciones">
                                                                                                                    <DataItemTemplate>
                                                                                                                        <dx:ASPxTextBox ID="txtObservacion" runat="server" Text='<%# Eval("OBSERVACIONES") %>' Width="100px">
                                                                                                                            <Border BorderStyle="None" />
                                                                                                                            <ReadOnlyStyle BackColor="#DEE8DB">
                                                                                                                            </ReadOnlyStyle>
                                                                                                                            <FocusedStyle BackColor="#FFFFCC">
                                                                                                                            </FocusedStyle>
                                                                                                                            <DisabledStyle BackColor="#DEE8DB">
                                                                                                                            </DisabledStyle>
                                                                                                                        </dx:ASPxTextBox>
                                                                                                                    </DataItemTemplate>
                                                                                                                </dx:GridViewDataTextColumn>
                                                                                                                <dx:GridViewDataTextColumn Caption="ESTADO" FieldName="ESTADO" ShowInCustomizationForm="True" Visible="False" VisibleIndex="21">
                                                                                                                </dx:GridViewDataTextColumn>
                                                                                                                <dx:GridViewDataTextColumn Caption="ID_SUB_IDENTIFICADOR" FieldName="ID_SUB_IDENTIFICADOR" ShowInCustomizationForm="True" Visible="False" VisibleIndex="17">
                                                                                                                </dx:GridViewDataTextColumn>
                                                                                                            </Columns>
                                                                                                            <Styles CssPostfix="DevGlp">
                                                                                                                <AlternatingRow BackColor="#EEEEEE">
                                                                                                                </AlternatingRow>
                                                                                                            </Styles>
                                                                                                            <TotalSummary>
                                                                                                                <dx:ASPxSummaryItem FieldName="F_IMPORTE" ShowInColumn="IMPORTE" SummaryType="Custom" Tag="imp" />
                                                                                                                <dx:ASPxSummaryItem FieldName="VOLUMEN" ShowInColumn="CANTIDAD" SummaryType="Custom" Tag="vol" />
                                                                                                                <dx:ASPxSummaryItem FieldName="STR_ESTADO" ShowInColumn="EST." SummaryType="Custom" Tag="est" />
                                                                                                            </TotalSummary>
                                                                                                        </dx:ASPxGridView>
                                                                                                        <br />
                                                                                                        <dx:ASPxButton ID="btnGuarfarDJ" runat="server" Text="Guardar Declaracion Saldo Fisico(Medicion)" Theme="Metropolis" Width="200px" AutoPostBack="False" OnClick="btnGuarfarDJ_Click" Font-Bold="True" ForeColor="Maroon">
                                                                                                            <ClientSideEvents Click="function(s, e) {
                                                                        if(confirm('¿Desea registrar saldo fisico(Medicion)?'))
                                                                        {
                                                                        LoadingPanel.Show();
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
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            Estado de la estacion de sservicio : 
                                                                                        </td>
                                                                                        <td>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td colspan="4" style="width: 100%">&nbsp;

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




                                                    <tr>
                                                        <td colspan="7">
                                                            <dx:ASPxPanel ID="pnlHerramienta" runat="server" Width="100%" ClientInstanceName="pnlHerramienta" ClientVisible="False">
                                                                <PanelCollection>
                                                                    <dx:PanelContent ID="PanelContent6" runat="server">
                                                                        <table style="width: 100%">
                                                                            <tr>
                                                                                <td colspan="4">
                                                                                    <dx:ASPxLabel ID="ASPxLabel3" runat="server" Font-Bold="True" Font-Names="Rockwell" ForeColor="#999999" Text="HERRAMINTAS ADICIONALES" Width="100%">
                                                                                        <BorderBottom BorderStyle="Solid" />
                                                                                    </dx:ASPxLabel>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 20%">&nbsp;</td>
                                                                                <td style="width: 20%">&nbsp;</td>
                                                                                <td style="width: 20%">&nbsp;</td>
                                                                                <td style="width: 40%">&nbsp;</td>
                                                                            </tr>
                                                                        </table>
                                                                    </dx:PanelContent>
                                                                </PanelCollection>
                                                            </dx:ASPxPanel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="auto-style11">
                                                            <dx:ASPxGridViewExporter ID="dgeExportar" runat="server">
                                                            </dx:ASPxGridViewExporter>
                                                        </td>
                                                        <td class="auto-style10">&nbsp;</td>
                                                        <td class="auto-style10"></td>
                                                        <td class="auto-style12"></td>
                                                        <td class="auto-style10">&nbsp;</td>
                                                        <td class="auto-style10">&nbsp;</td>
                                                        <td class="auto-style14">&nbsp;</td>
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

