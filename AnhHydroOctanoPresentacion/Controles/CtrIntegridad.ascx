<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtrIntegridad.ascx.cs" Inherits="AnhHydroOctanoPresentacion.Controles.CtrIntegridad" %>
<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>
<%@ Register TagPrefix="dx" Namespace="DevExpress.Web.ASPxPivotGrid" Assembly="DevExpress.Web.ASPxPivotGrid.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>

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
    
    .dxflInternalEditorTable_Youthful {
    width: 100%;
}
    
    .auto-style1 {
        width: 60%;
        height: 18px;
    }
    .auto-style2 {
        width: 40%;
        height: 18px;
    }
    
    </style>

<div class="col-md-12">
    <table style="width: 100%">
        <tr>
            <td style="text-align: center; padding-bottom: 1px">
                <dx:ASPxRoundPanel runat="server" ShowHeader="False" Width="100%" ID="pnlTitulo">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <dx:ASPxLabel runat="server" ForeColor="#367B2F" ID="lblTitulo" Font-Bold="True" Font-Names="Rockwell" Font-Size="Larger" Text="CONTROL DE INTEGRIDAD DE DATOS"></dx:ASPxLabel>
                    </dx:PanelContent>
                </PanelCollection>
                <Border BorderStyle="Ridge" BorderWidth="1px" BorderColor="#003300"></Border>
                </dx:ASPxRoundPanel>
            </td>
        </tr>
    </table>

    <dx:ASPxSplitter ID="ASPxSplitter1" runat="server" Height="100%">
        <Panes>
            <dx:SplitterPane Size="14%" AllowResize="True" AutoHeight="True">
                <ContentCollection>
                    <dx:SplitterContentControl runat="server">
                        
                        <dx:ASPxCallbackPanel ID="pnlLateral" runat="server" Width="200px" ClientInstanceName="pnlLateral">
                            <PanelCollection>
                                <dx:PanelContent runat="server">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td colspan="2" style="padding-bottom: 5px">
                                                <dx:ASPxLabel ID="ASPxLabel60" runat="server" Font-Bold="True" Font-Names="Rockwell" ForeColor="#487B5C" Text="CARACTERISTICAS" Width="100%">
                                                    <BorderBottom BorderStyle="Solid" />
                                                </dx:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="padding-top: 10px">Tipo :
                                                <dx:ASPxRadioButtonList ID="rbtTabla" runat="server" ClientInstanceName="rbtTabla" SelectedIndex="0" Theme="Metropolis" ValueType="System.Int32" Width="100%">
                                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
if(s.GetValue()==1)
{
cmbSubTipo.SetEnabled(true);
}
else
{
cmbSubTipo.SetEnabled(false);
}
}" />
                                                    <Items>
                                                        <dx:ListEditItem Selected="True" Text="ENTIDADES (HYD - BSA)" Value="1" />
                                                        <dx:ListEditItem Text="PRECIOS" Value="2" />
                                                    </Items>
                                                </dx:ASPxRadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style1">
                                                Tipo de Prioridad :</td>
                                            <td class="auto-style2">
                                                </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <dx:ASPxComboBox ID="cmbSubTipo" runat="server" SelectedIndex="0" Theme="Metropolis" Width="100%" ValueType="System.Int32" ClientInstanceName="cmbSubTipo">
                                                    <Items>
                                                        <dx:ListEditItem Selected="True" Text="Prioridad Hydro - EESS LIQ" Value="1" />
                                                        <dx:ListEditItem Text="Prioridad Hydro - EESS GNV" Value="2" />
                                                        <dx:ListEditItem Text="Prioridad Bsisa - EESS LIQ" Value="3" />
                                                        <dx:ListEditItem Text="Prioridad Bsisa - EESS GNV" Value="4" />
                                                        <dx:ListEditItem Text="Bsisa - Sin Relación Hydro" Value="5" />
                                                        <dx:ListEditItem Text="Bsisa - Id Hydro Distinto" Value="6" />
                                                    </Items>
                                                    <ClearButton Visibility="Auto">
                                                    </ClearButton>
                                                </dx:ASPxComboBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 60%">&nbsp;</td>
                                            <td style="width: 40%">&nbsp;</td>
                                        </tr>
                                        <tr style="padding-bottom: 5px">
                                            <td colspan="2">
                                    
                                                <dx:ASPxButton ID="btnActualiza" runat="server" AutoPostBack="False" Font-Bold="True" ForeColor="Maroon" Text="Ver Registros" Theme="Metropolis" Width="100%">
                                                    <ClientSideEvents Click="function(s, e) {
pnlDatos.PerformCallback();
}" />
                                                    <Image IconID="actions_reset2_16x16">
                                                    </Image>
                                                </dx:ASPxButton>
                                    
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                &nbsp;</td>
                                        </tr>
                                    </table>
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxCallbackPanel>

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
                                            <td colspan="7" style="text-align: right;">
                                                <dx:ASPxButton ID="btnEmitir" runat="server" Font-Bold="True" ForeColor="Maroon" OnClick="btnEmitir_Click" Text="Exportar Registros" Theme="Metropolis" Width="250px">
                                                    <Image IconID="export_exporttoxlsx_16x16">
                                                    </Image>
                                                </dx:ASPxButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="7">
                                                <dx:ASPxGridView ID="dgvCabecera" runat="server" AutoGenerateColumns="False" ClientInstanceName="dgvCabecera" EnableTheming="True" Font-Size="Smaller" KeyFieldName="HYD_ID_ENTIDAD" OnHtmlRowPrepared="dgvCabecera_HtmlRowPrepared" Theme="Default" Width="100%">
                                                    <SettingsContextMenu EnableColumnMenu="True" Enabled="True">
                                                    </SettingsContextMenu>
                                                    <Templates>
                                                        <EditForm>
                                                        </EditForm>
                                                    </Templates>
                                                    <SettingsPager PageSize="50">
                                                        <PageSizeItemSettings Items="10, 50, 100, 200, 500, 1000" Visible="True">
                                                        </PageSizeItemSettings>
                                                    </SettingsPager>
                                                    <SettingsEditing Mode="EditForm">
                                                    </SettingsEditing>
                                                    <Settings ShowHeaderFilterButton="True" ShowFilterRow="True" ShowGroupPanel="True" ShowFooter="True" ShowGroupFooter="VisibleIfExpanded" ShowFilterBar="Visible" ShowFilterRowMenu="True" />
                                                    <SettingsBehavior AllowFocusedRow="True" EnableCustomizationWindow="True" />
                                                    <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
                                                    <SettingsPopup>
                                                        <EditForm Modal="True" />
                                                        <CustomizationWindow HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" />
                                                    </SettingsPopup>
                                                    <SettingsSearchPanel Visible="True" />
                                                    <SettingsText CommandNew="Nuevo" />
                                                    <Columns>
                                                        <dx:GridViewDataTextColumn Caption="HYD_DEPTO" FieldName="HYD_DEPTO" ShowInCustomizationForm="True" VisibleIndex="0">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="HYD_ID_ENTIDAD" FieldName="HYD_ID_ENTIDAD" ShowInCustomizationForm="True" VisibleIndex="1">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="HYD_ID_CONSUMIDOR" FieldName="HYD_ID_CONSUMIDOR" ShowInCustomizationForm="True" VisibleIndex="2">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="HYD_NIT" FieldName="HYD_NIT" ShowInCustomizationForm="True" VisibleIndex="3">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="HYD_DENOMINACION" FieldName="HYD_DENOMINACION" ShowInCustomizationForm="True" VisibleIndex="4">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="HYD_DIRECCION" FieldName="HYD_DIRECCION" ShowInCustomizationForm="True" VisibleIndex="5">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="HYD_SIST_GEST" FieldName="HYD_SIST_GEST" ShowInCustomizationForm="True" VisibleIndex="6">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="HYD_LICENCIA" FieldName="HYD_LICENCIA" ShowInCustomizationForm="True" VisibleIndex="7">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="HYD_LIC_VIGENCIA" FieldName="HYD_LIC_VIGENCIA" ShowInCustomizationForm="True" VisibleIndex="8">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="BSA_DEPTO" FieldName="BSA_DEPTO" ShowInCustomizationForm="True" VisibleIndex="9">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="BSA_ID_ENT_HYD" FieldName="BSA_ID_ENT_HYD" ShowInCustomizationForm="True" VisibleIndex="10">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="BSA_NIT" FieldName="BSA_NIT" ShowInCustomizationForm="True" VisibleIndex="11">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="BSA_DENOMINACION" FieldName="BSA_DENOMINACION" ShowInCustomizationForm="True" VisibleIndex="12">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="BSA_DIRECCION" FieldName="BSA_DIRECCION" ShowInCustomizationForm="True" VisibleIndex="13">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="BSA_SIST_GEST" FieldName="BSA_SIST_GEST" ShowInCustomizationForm="True" VisibleIndex="14">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="BSA_COD_EESS" FieldName="BSA_COD_EESS" ShowInCustomizationForm="True" VisibleIndex="15">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="BSA_VERSION_MDW" FieldName="BSA_VERSION_MDW" ShowInCustomizationForm="True" VisibleIndex="16">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="BSA_ID_HYLIQ" FieldName="BSA_ID_HYLIQ" ShowInCustomizationForm="True" VisibleIndex="17">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="BSA_ID_HYGNV" FieldName="BSA_ID_HYGNV" ShowInCustomizationForm="True" VisibleIndex="18">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewCommandColumn ShowInCustomizationForm="True" VisibleIndex="19">
                                                        </dx:GridViewCommandColumn>
                                                    </Columns>
                                                    <Styles CssPostfix="DevGlp"><AlternatingRow BackColor="#EEEEEE">
                                                        </AlternatingRow></Styles>
                                                </dx:ASPxGridView>
                                            </td>
                                        </tr>
                                        
                                        <tr>
                                            <td colspan="7">
                                                
                                                <asp:Panel ID="pnlPrecios" runat="server" Width="100%" Visible="False">
                                                    
                                                    
                                                    <table style="width: 100%">
                                                                <tr>
                                                                  <td colspan="2" style="width: 100%">
                                                              
                                                                      <dx:ASPxGridView ID="dgvPrecios" runat="server" AutoGenerateColumns="False" ClientInstanceName="dgvPrecios" EnableTheming="True" Font-Size="Smaller" KeyFieldName="ID_TABLA" Theme="Default" Width="100%">
                                                                          <ClientSideEvents CustomButtonClick="function(s, e) {
switch(e.buttonID) {
case&nbsp; 'btnHistorial':
pnlHistorico.PerformCallback();
break;
} 
}" />
                                                                          <SettingsContextMenu EnableColumnMenu="True" Enabled="True">
                                                                          </SettingsContextMenu>
                                                                          <Templates>
                                                                              <EditForm>
                                                                              </EditForm>
                                                                          </Templates>
                                                                          <SettingsPager NumericButtonCount="2">
                                                                              <PageSizeItemSettings Items="10, 50, 100, 200, 500, 1000" Visible="True">
                                                                              </PageSizeItemSettings>
                                                                          </SettingsPager>
                                                                          <SettingsEditing Mode="EditForm">
                                                                          </SettingsEditing>
                                                                          <Settings ShowFilterRow="True" ShowFooter="True" ShowGroupFooter="VisibleIfExpanded" ShowGroupPanel="True" ShowHeaderFilterButton="True" />
                                                                          <SettingsBehavior AllowFocusedRow="True" EnableCustomizationWindow="True" />
                                                                          <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
                                                                          <SettingsPopup>
                                                                              <EditForm Modal="True" />
                                                                              <CustomizationWindow HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" />
                                                                          </SettingsPopup>
                                                                          <SettingsSearchPanel Visible="True" />
                                                                          <SettingsText CommandNew="Nuevo" />
                                                                          <Columns>
                                                                              <dx:GridViewDataTextColumn Caption="CATEGORIA" FieldName="CATEGORIA" ShowInCustomizationForm="True" VisibleIndex="0">
                                                                              </dx:GridViewDataTextColumn>
                                                                              <dx:GridViewDataTextColumn Caption="ID_CATEGORIA_VEHICULO" FieldName="ID_CATEGORIA_VEHICULO" ShowInCustomizationForm="True" Visible="False" VisibleIndex="1">
                                                                              </dx:GridViewDataTextColumn>
                                                                              <dx:GridViewDataTextColumn Caption="HIDROCARBURO" FieldName="HIDROCARBURO" ShowInCustomizationForm="True" VisibleIndex="2">
                                                                              </dx:GridViewDataTextColumn>
                                                                              <dx:GridViewDataTextColumn Caption="HIDRO" FieldName="HIDRO" ShowInCustomizationForm="True" VisibleIndex="3">
                                                                              </dx:GridViewDataTextColumn>
                                                                              <dx:GridViewDataTextColumn Caption="ID_HIDROCARBURO" FieldName="ID_HIDROCARBURO" ShowInCustomizationForm="True" Visible="False" VisibleIndex="4">
                                                                              </dx:GridViewDataTextColumn>
                                                                              <dx:GridViewDataTextColumn Caption="PRECIO (Bs.)" FieldName="PRECIO_BS" ShowInCustomizationForm="True" VisibleIndex="5">
                                                                              </dx:GridViewDataTextColumn>
                                                                              <dx:GridViewDataTextColumn Caption="FECHA" FieldName="FECHA_DESDE" ShowInCustomizationForm="True" VisibleIndex="6">
                                                                              </dx:GridViewDataTextColumn>
                                                                              <dx:GridViewCommandColumn ShowInCustomizationForm="True" VisibleIndex="14">
                                                                                  <CustomButtons>
                                                                                      <dx:GridViewCommandColumnCustomButton ID="btnHistorial" Text=" ">
                                                                                          <Image IconID="pdfviewer_marqueezoom_16x16" ToolTip="Ver Historico">
                                                                                          </Image>
                                                                                      </dx:GridViewCommandColumnCustomButton>
                                                                                  </CustomButtons>
                                                                              </dx:GridViewCommandColumn>
                                                                          </Columns>
                                                                          <Styles CssPostfix="DevGlp"><AlternatingRow BackColor="#EEEEEE">
                                                        </AlternatingRow></Styles>
                                                                          <TotalSummary>
                                                                              <dx:ASPxSummaryItem DisplayFormat="{0}" FieldName="TOTAL_CXC" ShowInColumn="DEUDA" SummaryType="Sum" />
                                                                          </TotalSummary>
                                                                          <GroupSummary>
                                                                              <dx:ASPxSummaryItem DisplayFormat="{0}" FieldName="TOTAL_CXC" ShowInGroupFooterColumn="DEUDA" SummaryType="Sum" />
                                                                          </GroupSummary>
                                                                      </dx:ASPxGridView>
                                                              
                                                                  </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 25%">
                                                                        <dx:ASPxButton ID="btnEmitirDet" runat="server" Font-Bold="True" ForeColor="Maroon" OnClick="btnEmitirDet_Click" Text="Exportar Registros" Theme="Metropolis" Width="100%">
                                                                            <Image IconID="export_exporttoxlsx_16x16">
                                                                            </Image>
                                                                        </dx:ASPxButton>
                                                                    </td>
                                                                    <td style="width: 75%"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">

                                                                        <dx:ASPxCallbackPanel ID="pnlHistorico" runat="server" Width="100%" ClientInstanceName="pnlHistorico" OnCallback="pnlHistorico_Callback">
                                                                            <ClientSideEvents EndCallback="function(s, e) {
grafficardatosprinc(s, e);
}" />
                                                                            <PanelCollection>
                                                                                <dx:PanelContent runat="server">
                                                                                    
                                                                                    <table style="width: 100%">
                                                                                        
                                                                                        <tr>
                                                                                            <td style="width: 20%">
                                                                                                <dx:ASPxGridView ID="dgvHistorico" runat="server" AutoGenerateColumns="False" ClientInstanceName="dgvHistorico" EnableTheming="True" Font-Size="Smaller" KeyFieldName="ID_TABLA" OnHtmlRowPrepared="dgvHistorico_HtmlRowPrepared" Theme="Default" Width="100%">
                                                                                                    <SettingsContextMenu EnableRowMenu="False">
                                                                                                    </SettingsContextMenu>
                                                                                                    <Templates>
                                                                                                        <EditForm>
                                                                                                        </EditForm>
                                                                                                    </Templates>
                                                                                                    <SettingsPager NumericButtonCount="1" PageSize="20">
                                                                                                        <PageSizeItemSettings Items="50, 100, 200, 500, 1000" Visible="True">
                                                                                                        </PageSizeItemSettings>
                                                                                                    </SettingsPager>
                                                                                                    <Settings ShowFilterRow="True" ShowFooter="True" ShowHeaderFilterButton="True" />
                                                                                                    <SettingsBehavior AllowFocusedRow="True" />
                                                                                                    <SettingsPopup>
                                                                                                        <EditForm Modal="True" />
                                                                                                        <CustomizationWindow HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" />
                                                                                                    </SettingsPopup>
                                                                                                    <Columns>
                                                                                                        <dx:GridViewCommandColumn ShowClearFilterButton="True" ShowInCustomizationForm="True" Visible="False" VisibleIndex="0">
                                                                                                        </dx:GridViewCommandColumn>
                                                                                                        <dx:GridViewDataTextColumn Caption="ID_TABLA" FieldName="ID_TABLA" ShowInCustomizationForm="True" Visible="False" VisibleIndex="1">
                                                                                                            <Settings AllowAutoFilter="True" AllowHeaderFilter="True" AutoFilterCondition="Contains" />
                                                                                                        </dx:GridViewDataTextColumn>
                                                                                                        <dx:GridViewDataTextColumn Caption="FECHA" FieldName="FECHA_DESDE" ShowInCustomizationForm="True" VisibleIndex="2">
                                                                                                            <Settings AutoFilterCondition="Contains" />
                                                                                                        </dx:GridViewDataTextColumn>
                                                                                                        <dx:GridViewDataTextColumn Caption="FECHA_HASTA" FieldName="FECHA_HASTA" ShowInCustomizationForm="True" Visible="False" VisibleIndex="3">
                                                                                                        </dx:GridViewDataTextColumn>
                                                                                                        <dx:GridViewDataTextColumn Caption="PRECIO (Bs.)" FieldName="PRECIO_BS" ShowInCustomizationForm="True" VisibleIndex="4">
                                                                                                        </dx:GridViewDataTextColumn>
                                                                                                        <dx:GridViewDataTextColumn Caption="FECHA_REGISTRO" FieldName="FECHA_REGISTRO" ShowInCustomizationForm="True" Visible="False" VisibleIndex="5">
                                                                                                        </dx:GridViewDataTextColumn>
                                                                                                    </Columns>
                                                                                                    <TotalSummary>
                                                                                                        <dx:ASPxSummaryItem FieldName="DecRecepcion" ShowInColumn="RECEPCION" SummaryType="Custom" Tag="rec" />
                                                                                                        <dx:ASPxSummaryItem FieldName="DecDemasia" ShowInColumn="DEMASIA" SummaryType="Custom" Tag="dem" />
                                                                                                        <dx:ASPxSummaryItem FieldName="DecTotalIng" ShowInColumn="TOT. ING." SummaryType="Custom" Tag="ing" />
                                                                                                        <dx:ASPxSummaryItem FieldName="DecVenta" ShowInColumn="VENTA" SummaryType="Custom" Tag="ven" />
                                                                                                        <dx:ASPxSummaryItem FieldName="DecMerma" ShowInColumn="MERMA" SummaryType="Custom" Tag="mer" />
                                                                                                        <dx:ASPxSummaryItem FieldName="DecTotalSal" ShowInColumn="TOT. SAL." SummaryType="Custom" Tag="sal" />
                                                                                                    </TotalSummary>
                                                                                                </dx:ASPxGridView>
                                                                                            </td>
                                                                                            <td style="width: 80%">
                                                                                                <div id="container" aria-expanded="true" style="min-width: 310px; height: 400px; margin: 0 auto">
                                                                                                </div>
                                                                                            </td>
                                                                                        </tr>

                                                                                   </table>

                                                                                </dx:PanelContent>
                                                                            </PanelCollection>
                                                                        </dx:ASPxCallbackPanel>
                                                                        
                                                                        

                                                                    </td>
                                                                    
                                                                </tr>
                                                                
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                            </table>
                                                    

                                                </asp:Panel>
                                                
                                            </td>
                                            
                                        </tr>
                                        <tr>
                                            <td colspan="7">
                                                
                                                <dx:ASPxCallbackPanel ID="pnlPrecios2222" runat="server" Width="100%" ClientInstanceName="pnlPrecios2222" Visible="False">
                                                    <PanelCollection>
                                                        <dx:PanelContent runat="server">
                                                            
                                                            
                                                            
                                                        </dx:PanelContent>
                                                    </PanelCollection>
                                                </dx:ASPxCallbackPanel>
                                                
                                            </td>
                                            
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>
                                                <dx:ASPxGridViewExporter ID="dgeExportar" runat="server">
                                                </dx:ASPxGridViewExporter>
                                            </td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
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

<script type="text/javascript">

    function grafficardatosprinc(s, e) {

        var jsTitle = s.cp_title;
        var jsSubtitle = s.cp_subtitle;
        var jsXAxis = s.cp_xAxis;
        var jsSeries = s.cp_series;

        Highcharts.chart('container', {
            chart: {
                type: 'line'
            },
            title: {
                text: jsTitle
            },
            subtitle: {
                text: jsSubtitle
            },
            xAxis: {
                categories: jsXAxis,
                crosshair: true
            },
            yAxis: {
                min: 0,
                title: {
                    text: 'Precio Bs.'
                }
            },
            
            plotOptions: {
                line: {
                    dataLabels: {
                        enabled: true
                    },
                    enableMouseTracking: false
                }
            },
            series: jsSeries
        });
    }
</script>






    




























































