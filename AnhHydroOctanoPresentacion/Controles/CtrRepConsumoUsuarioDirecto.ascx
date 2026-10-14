<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtrRepConsumoUsuarioDirecto.ascx.cs" Inherits="AnhHydroOctanoPresentacion.Controles.CtrRepConsumoUsuarioDirecto" %>

<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPivotGrid" TagPrefix="dx" %>


<div class="col-md-12">

    <table style="width: 100%; height: 100%">
        <tr>
            <td style="text-align: center; padding-bottom: 1px">

                <dx:ASPxRoundPanel runat="server" ShowHeader="False" Width="100%" ID="pnlTitulo">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <dx:ASPxLabel runat="server" ForeColor="#367B2F" ID="lblTitulo" Font-Bold="True" Font-Names="Rockwell" Font-Size="Larger" Text="REPORTE - CONSUMO DE USUARIOS DIRECTOS"></dx:ASPxLabel>
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
                                        <dx:ASPxLabel ID="ASPxLabel57" runat="server" ForeColor="#003300" Text="Fecha Inicial :">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <dx:ASPxLabel ID="ASPxLabel58" runat="server" ForeColor="#003300" Text="Fecha Final :">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td style="padding-left: 20px">
                                        <dx:ASPxLabel ID="ASPxLabel59" runat="server" ForeColor="#003300" Text="Actividad :">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td style="padding-left: 20px">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 5%">
                                        <dx:ASPxComboBox ID="cmbMes" runat="server" ClientInstanceName="cmbMes" Theme="Metropolis" ValueType="System.Int32" Width="140px" ToolTip="Seleccione un mes" Font-Bold="true">
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
                                       <%-- <dx:ASPxDateEdit ID="detFechaIni" runat="server" ClientInstanceName="detFechaIni" EnableTheming="True" Height="23px" Theme="Metropolis" ToolTip="Seleccione el periodo inicial" Width="100%" Font-Bold="True">
                                            <TimeSectionProperties>
                                                <TimeEditProperties>
                                                    <ClearButton Visibility="Auto">
                                                    </ClearButton>
                                                </TimeEditProperties>
                                            </TimeSectionProperties>
                                            <ClearButton Visibility="Auto">
                                            </ClearButton>
                                        </dx:ASPxDateEdit>--%>
                                    </td>
                                    <td style="width: 5%">
                                        <dx:ASPxComboBox ID="cmbGestion" runat="server" EnableTheming="True" Font-Bold="True" Theme="Metropolis" ValueType="System.Int32" Width="160px" ToolTip="Seleccione un Periodo" ClientInstanceName="cmbPeriodo">
                                            <ClearButton Visibility="Auto">
                                            </ClearButton>
                                            <CaptionStyle Font-Bold="False">
                                            </CaptionStyle>
                                            <DisabledStyle Font-Bold="False">
                                            </DisabledStyle>
                                        </dx:ASPxComboBox>
                                        <%--<dx:ASPxDateEdit ID="detFechaFin" runat="server" ClientInstanceName="detFechaFin" Theme="Metropolis" ToolTip="Seleccione el periodo final" Width="100%" Font-Bold="True">
                                            <TimeSectionProperties>
                                                <TimeEditProperties>
                                                    <ClearButton Visibility="Auto">
                                                    </ClearButton>
                                                </TimeEditProperties>
                                            </TimeSectionProperties>
                                            <ClearButton Visibility="Auto">
                                            </ClearButton>
                                        </dx:ASPxDateEdit>--%>
                                    </td>
                                    <td style="width: 20%; padding-left: 10px">
                                        <dx:ASPxComboBox ID="cmbActividad" runat="server" SelectedIndex="0" Theme="Metropolis" Width="100%" ValueType="System.Int32" Enabled="false">
                                            <Items>
                                                <dx:ListEditItem Selected="True" Text="CONSUMO USUARIOS DIRECTOS" Value="1" />
                                                <dx:ListEditItem Text="PLANTAS DE SUMINISTRO (PSCAA)" Value="2" />
                                            </Items>
                                            <ClearButton Visibility="Auto">
                                            </ClearButton>
                                        </dx:ASPxComboBox>
                                    </td>
                                    <td style="width: 20%; padding-left: 20px"></td>
                                </tr>
                                <tr>
                                    <td>
                                        <dx:ASPxLabel ID="ASPxLabel61" runat="server" ForeColor="#003300" Text="Identificador :">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>
                                        <dx:ASPxTextBox ID="txtIdentificador" runat="server" Width="100%" Font-Bold="True">
                                            <ValidationSettings>
                                                <RequiredField ErrorText="Introduzca un identificador" IsRequired="True" />
                                            </ValidationSettings>
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td colspan="2">&nbsp;</td>

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
}"
                        BeginCallback="function(s, e) {
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
                                    <td colspan="7"></td>
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
                                    <td style="width: 70%" colspan="5"></td>
                                </tr>
                                <tr>
                                    <td colspan="7">
                                        <dx:ASPxGridView ID="dgvReporte" runat="server" AutoGenerateColumns="False" ClientInstanceName="dgvReporte" EnableTheming="True" Font-Size="X-Small" KeyFieldName="ID" Theme="Default" Width="100%">
                                            <SettingsContextMenu EnableRowMenu="False">
                                            </SettingsContextMenu>
                                            <Templates>
                                                <EditForm>
                                                </EditForm>
                                            </Templates>
                                            <SettingsPager PageSize="100">
                                                <PageSizeItemSettings Items="25, 100, 1000" Visible="True" ShowAllItem="True">
                                                </PageSizeItemSettings>
                                            </SettingsPager>
                                            <SettingsEditing Mode="PopupEditForm">
                                            </SettingsEditing>
                                            <Settings ShowFilterRow="True" ShowFooter="True" ShowHeaderFilterButton="True" />
                                            <SettingsBehavior AllowFocusedRow="True" />
                                            <SettingsPopup>
                                                <EditForm Modal="True" />
                                                <CustomizationWindow HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" />
                                            </SettingsPopup>
                                            <SettingsText CommandNew="Nuevo" />
                                            <Columns>
                                                <dx:GridViewCommandColumn ShowClearFilterButton="True" ShowInCustomizationForm="True" Visible="False" VisibleIndex="21">
                                                </dx:GridViewCommandColumn>
                                                <dx:GridViewDataTextColumn Caption="DEPTO." FieldName="DEPARTAMENTO" ShowInCustomizationForm="True" VisibleIndex="3" ToolTip="Departamento">
                                                    <Settings AutoFilterCondition="Contains" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="NIT CONSUMIDOR" FieldName="NIT_CONSUMIDOR" ShowInCustomizationForm="True" VisibleIndex="9">
                                                    <Settings AutoFilterCondition="Contains" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="PRODUCTO" FieldName="PRODUCTO" ShowInCustomizationForm="True" ToolTip="Código de Control" VisibleIndex="12">
                                                    <Settings AutoFilterCondition="Contains" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="CANTIDAD" FieldName="CANTIDAD" ShowInCustomizationForm="True" VisibleIndex="13">
                                                    <Settings AutoFilterCondition="Contains" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="PRECIO" FieldName="PRECIO_UNITARIO" ShowInCustomizationForm="True" ToolTip="Importe Propio" VisibleIndex="14">
                                                    <Settings AutoFilterCondition="Contains" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="PROYECTO" FieldName="DENOMINACION" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="5">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="NRO. FACT./DOC." FieldName="NRO_TRANSACCION" ShowInCustomizationForm="True" VisibleIndex="8" ToolTip="Numero de Factura o Documento">
                                                    <Settings AutoFilterCondition="Equals" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="ID_HIDROCARBURO" FieldName="ID_HIDROCARBURO" ShowInCustomizationForm="True" ToolTip="Número de Factura" Visible="False" VisibleIndex="11">
                                                    <Settings AutoFilterCondition="Contains" />
                                                    <CellStyle HorizontalAlign="Right">
                                                    </CellStyle>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="RAZON SOCIAL" FieldName="RAZON_SOCIAL" ReadOnly="True" ShowInCustomizationForm="True" ToolTip="Producto" VisibleIndex="10">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="ID_CONSUMIDOR" FieldName="ID_CONSUMIDOR" ShowInCustomizationForm="True" Visible="False" VisibleIndex="1">
                                                    <Settings AutoFilterCondition="Contains" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="IMPORTE" FieldName="PRECIO_TOTAL" ShowInCustomizationForm="True" VisibleIndex="15">
                                                    <Settings AutoFilterCondition="Contains" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="FECHA_OPERACION" FieldName="FECHA_OPERACION" ShowInCustomizationForm="True" Visible="False" VisibleIndex="16">
                                                    <Settings AutoFilterCondition="Equals" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="ID" FieldName="ID" ShowInCustomizationForm="True" Visible="False" VisibleIndex="0">
                                                    <Settings AutoFilterCondition="Equals" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="FECHA" FieldName="DAT_FECHA" ShowInCustomizationForm="True" VisibleIndex="2">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="ID_DEPARTAMENTO" FieldName="ID_DEPARTAMENTO" ShowInCustomizationForm="True" Visible="False" VisibleIndex="6">
                                                    <Settings AutoFilterCondition="Contains" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="NIT EMISOR" FieldName="NIT_EMISOR" ShowInCustomizationForm="True" VisibleIndex="4">
                                                    <Settings AutoFilterCondition="Contains" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="TIPO DOC." FieldName="TIPO_DOC" ShowInCustomizationForm="True" VisibleIndex="7" ToolTip="Tipo de Documento">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="ID_TIPO_TRANSACCION" FieldName="ID_TIPO_TRANSACCION" ShowInCustomizationForm="True" Visible="False" VisibleIndex="17">
                                                </dx:GridViewDataTextColumn>
                                            </Columns>
                                            <Styles CssPostfix="DevGlp">
                                                <AlternatingRow BackColor="#EEEEEE">
                                                </AlternatingRow>
                                            </Styles>
                                            <TotalSummary>
                                                <dx:ASPxSummaryItem FieldName="CANTIDAD" ShowInColumn="CANTIDAD" SummaryType="Sum" DisplayFormat="{0}" />
                                                <dx:ASPxSummaryItem DisplayFormat="{0}" FieldName="PRECIO_TOTAL" ShowInColumn="PRECIO_TOTAL" SummaryType="Sum" />
                                            </TotalSummary>
                                        </dx:ASPxGridView>
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

            </td>
        </tr>
    </table>

</div>
