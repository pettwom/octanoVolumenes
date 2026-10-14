<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtrRepEstadoGral.ascx.cs" Inherits="AnhHydroOctanoPresentacion.Controles.CtrRepEstadoGral" %>

<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>



<%@ Register assembly="DevExpress.XtraReports.v15.1.Web, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraReports.Web" tagprefix="dx" %>



<%@ Register assembly="DevExpress.Web.ASPxScheduler.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxScheduler" tagprefix="dxwschs" %>



<%@ Register assembly="DevExpress.Web.ASPxPivotGrid.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPivotGrid" tagprefix="dx" %>



<style type="text/css">
    .auto-style1 {
        height: 18px;
    }
    .auto-style4 {
        height: 18px;
        width: 168px;
    }
    .auto-style5 {
        width: 168px;
    }
    .auto-style6 {
        height: 18px;
        width: 138px;
    }
    .auto-style9 {
        width: 138px;
    }
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
    
    </style>



<dx:ASPxSplitter ID="ASPxSplitter1" runat="server" Height="700px">
    <Panes>
        <dx:SplitterPane Size="11%" ScrollBars="Auto">
            <ContentCollection>
                <dx:SplitterContentControl runat="server">
                    <table style="width: 100%;">
                        <tr>
                            <td>Mes :<dx:ASPxSpinEdit ID="spnMes" runat="server" MaxValue="12" MinValue="1" NumberType="Integer" Width="60px">
                                <ClientSideEvents NumberChanged="function(s, e) {
tacMovimientos.SetActiveTabIndex(0);
	dgvAcumulado.SetVisible(true);
dgvInternacional.SetVisible(false);
	pnlDatos.PerformCallback();
}" />
                                <ClearButton Visibility="Auto">
                                </ClearButton>
                                </dx:ASPxSpinEdit>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="auto-style1">Año :<dx:ASPxSpinEdit ID="spnGestion" runat="server" MaxValue="2050" MinValue="2010" NumberType="Integer" Width="60px">
                                <ClientSideEvents NumberChanged="function(s, e) {
tacMovimientos.SetActiveTabIndex(0);
	dgvAcumulado.SetVisible(true);
dgvInternacional.SetVisible(false);
	pnlDatos.PerformCallback();
}" />
                                <ClearButton Visibility="Auto">
                                </ClearButton>
                                </dx:ASPxSpinEdit>
                            </td>
                            <td class="auto-style1">
                                &nbsp;</td>
                            <td class="auto-style1">&nbsp;</td>
                        </tr>

                        <tr>
                            <td colspan="2">
                                <dx:ASPxTreeList ID="tlsOperadores" runat="server" AutoGenerateColumns="False" Height="187px" KeyFieldName="intId" ParentFieldName="intIdPadre" Font-Size="Smaller" Theme="Youthful" ClientInstanceName="tlsOperadores">
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
                                    <SettingsBehavior AllowFocusedNode="True" />
                                    <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
                                    <ClientSideEvents FocusedNodeChanged="function(s, e) {
	tacMovimientos.SetActiveTabIndex(0);
	dgvAcumulado.SetVisible(true);
dgvInternacional.SetVisible(false);
	pnlDatos.PerformCallback();
}" />
                                </dx:ASPxTreeList>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="2">&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                &nbsp;</td>
                        </tr>
                    </table>
                </dx:SplitterContentControl>
            </ContentCollection>
        </dx:SplitterPane>
        <dx:SplitterPane ScrollBars="Auto">
            <ContentCollection>
                <dx:SplitterContentControl runat="server">
                    <dx:ASPxCallbackPanel ID="pnlDatos" runat="server" Theme="Youthful" Width="100%" ClientInstanceName="pnlDatos" OnCallback="pnlDatos_Callback">
                        <PanelCollection>
                            <dx:PanelContent runat="server">
                                <table style="width: 100%;">
                                    <tr>
                                        <td colspan="2" >
                                            <asp:Label ID="Label1" runat="server" Font-Names="Calibri" Font-Size="Large" ForeColor="#25452B" Text="INFORMACION GENERAL" Font-Bold="True" Width="100%"></asp:Label>
                                        </td>
                                        <td class="auto-style1" ></td>
                                        <td>
                                            
                                            <dx:ASPxLabel ID="lblActividad" runat="server" Visible="False">
                                            </dx:ASPxLabel>
                                            
                                        </td>
                                        
                                        <td colspan="2">
                                            <dx:ASPxRoundPanel ID="pnlMensaje" runat="server" ShowHeader="False" Visible="False" Width="100%">
                                                <PanelCollection>
                                                    <dx:PanelContent runat="server">
                                                        <dx:ASPxLabel ID="lblMensajeError" runat="server" ForeColor="#666666" style="text-align:center">
                                                        </dx:ASPxLabel>
                                                    </dx:PanelContent>
                                                </PanelCollection>
                                                <Border BorderStyle="Ridge" BorderWidth="1px" />
                                            </dx:ASPxRoundPanel>
                                        </td>
                                        <td class="auto-style1">&nbsp;</td>
                                        
                                        <td class="auto-style1" ></td>
                                    </tr>
                                    <tr>
                                        <td colspan="6">
                                            <hr/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style4">
                                            <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="#10420F" Text="NOMBRE COMERCIAL"></asp:Label>
                                        </td>
                                        <td class="auto-style1"></td>
                                        <td class="auto-style1">
                                            <asp:Label ID="Label3" runat="server" Font-Bold="True" ForeColor="#10420F" Text="NIT"></asp:Label>
                                        </td>
                                        <td class="auto-style6"></td>
                                        <td class="auto-style1">
                                            <asp:Label ID="Label5" runat="server" Font-Bold="True" ForeColor="#10420F" Text="DEPARTAMENTO"></asp:Label>
                                        </td>
                                        <td class="auto-style1">
                                            <asp:Label ID="Label4" runat="server" Font-Bold="True" ForeColor="#10420F" Text="DIRECCION"></asp:Label>
                                        </td>
                                        <td class="auto-style1">
                                            <asp:Label ID="lblPeriodo" runat="server"></asp:Label>
                                        </td>
                                        <td class="auto-style1"></td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style5">
                                            <asp:Label ID="lblRazonSocial" runat="server"></asp:Label>
                                        </td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:Label ID="lblNit" runat="server"></asp:Label>
                                        </td>
                                        <td class="auto-style9">&nbsp;</td>
                                        <td>
                                            <asp:Label ID="lblDepto" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblRepLegal" runat="server"></asp:Label>
                                        </td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style5">&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td class="auto-style9">&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan="7">
                                            <dx:ASPxPivotGrid ID="pvgReporte" runat="server" ClientIDMode="AutoID" ClientInstanceName="pvgReporte" EnableTheming="True" Theme="Office2003Olive" Width="100%">
                                                <Fields>
                                                    <dx:PivotGridField ID="fieldstrDepto" Area="RowArea" AreaIndex="1" Caption="strDepto" FieldName="strDepto">
                                                    </dx:PivotGridField>
                                                    <dx:PivotGridField ID="fieldstrConsumidor" Area="RowArea" AreaIndex="4" Caption="strConsumidor" FieldName="strConsumidor">
                                                    </dx:PivotGridField>
                                                    <dx:PivotGridField ID="fieldstrActividad" Area="RowArea" AreaIndex="0" Caption="strActividad" FieldName="strActividad">
                                                    </dx:PivotGridField>
                                                    <dx:PivotGridField ID="fieldstrEstado" Area="RowArea" AreaIndex="2" Caption="strEstado" FieldName="strEstado">
                                                    </dx:PivotGridField>
                                                    <dx:PivotGridField ID="fielddecSaldoFin" Area="DataArea" AreaIndex="3" Caption="decSaldoFin" FieldName="decSaldoFin" TotalsVisibility="None">
                                                    </dx:PivotGridField>
                                                    <dx:PivotGridField ID="fielddecTotSal" Area="DataArea" AreaIndex="2" Caption="decTotSal" FieldName="decTotSal">
                                                    </dx:PivotGridField>
                                                    <dx:PivotGridField ID="fielddecTotIng" Area="DataArea" AreaIndex="1" Caption="decTotIng" FieldName="decTotIng">
                                                    </dx:PivotGridField>
                                                    <dx:PivotGridField ID="fielddecSaldoIni" Area="DataArea" AreaIndex="0" Caption="decSaldoIni" FieldName="decSaldoIni" TotalsVisibility="None">
                                                    </dx:PivotGridField>
                                                    <dx:PivotGridField ID="fieldstrProducto" Area="RowArea" AreaIndex="5" Caption="strProducto" FieldName="strProducto">
                                                    </dx:PivotGridField>
                                                    <dx:PivotGridField ID="fieldstrFechaPresentacion" AreaIndex="2" Caption="strFechaPresentacion" FieldName="strFechaPresentacion">
                                                    </dx:PivotGridField>
                                                    <dx:PivotGridField ID="fieldstrCite" AreaIndex="1" Caption="strCite" FieldName="strCite">
                                                    </dx:PivotGridField>
                                                    <dx:PivotGridField ID="fieldstrPeriodo" AreaIndex="0" Caption="strPeriodo" FieldName="strPeriodo">
                                                    </dx:PivotGridField>
                                                    <dx:PivotGridField ID="fieldstrAccion" AllowedAreas="RowArea" Area="RowArea" AreaIndex="3" Caption="strAccion" FieldName="strAccion">
                                                        <ValueTemplate>
                                                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">LinkButton</asp:LinkButton>
                                                        </ValueTemplate>
                                                    </dx:PivotGridField>
                                                </Fields>
                                                <ClientSideEvents CellDblClick="function(s, e) {
	alert('sdfujsidsj');
}" />
                                                <OptionsPager RowsPerPage="100" AlwaysShowPager="True">
                                                </OptionsPager>
                                            </dx:ASPxPivotGrid>
                                        </td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan="7">&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan="5">
                                            &nbsp;</td>
                                        <td>
                                            <dx:ASPxComboBox ID="cmbFormato" runat="server" SelectedIndex="0" Theme="Office2003Olive">
                                                <Items>
                                                    <dx:ListEditItem Selected="True" Text="EXCEL" Value="0" />
                                                    <dx:ListEditItem Text="PDF" Value="1" />
                                                </Items>
                                                <ClearButton Visibility="Auto">
                                                </ClearButton>
                                            </dx:ASPxComboBox>
                                        </td>
                                        <td>
                                            <dx:ASPxButton ID="btnEmitir" runat="server" Text="Emitir Reporte" Theme="Office2003Olive">
                                            </dx:ASPxButton>
                                        </td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan="5">
                                            <dx:ASPxTabControl ID="tacMovimientos" runat="server" ActiveTabIndex="2" ClientInstanceName="tacMovimientos" EnableTheming="True" TabIndex="1" Theme="Office2003Olive">
                                                <Tabs>
                                                    <dx:Tab Name="tcpCump" Text="Cumplimiento">
                                                    </dx:Tab>
                                                    <dx:Tab Name="tcpResumen" Text="Mov. Acumulado">
                                                    </dx:Tab>
                                                    <dx:Tab Name="tcpEspecial" Text="Mov. Internacional">
                                                    </dx:Tab>
                                                </Tabs>
                                                <ClientSideEvents ActiveTabChanged="function(s, e) {

dgvReporte.SetVisible(false);
dgvAcumulado.SetVisible(false);
dgvInternacional.SetVisible(false);

switch(expression) { 
	case 0:
		dgvAcumulado.SetVisible(true);
break;
case 1:
		dgvInternacional.SetVisible(true);
	break;
case 2:
dgvReporte.SetVisible(true);
	break;
}

//	if(tacMovimientos.GetActiveTabIndex()==1){
//		dgvAcumulado.SetVisible(false);
//dgvInternacional.SetVisible(true);
//	}
//	else{
//		dgvInternacional.SetVisible(false);
//dgvAcumulado.SetVisible(true);
//	}
//alert(tacMovimientos.GetTabByName());

}" />
                                            </dx:ASPxTabControl>
                                        </td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan="7">
                                            <dx:ASPxGridView ID="dgvReporte" runat="server" AutoGenerateColumns="False" ClientInstanceName="dgvReporte" EnableTheming="True" Font-Size="Smaller" KeyFieldName="IntId" OnCustomSummaryCalculate="dgvAcumulado_CustomSummaryCalculate" OnHtmlRowPrepared="dgvAcumulado_HtmlRowPrepared" Theme="Office2003Olive" Width="100%">
                                                <SettingsContextMenu EnableRowMenu="False">
                                                </SettingsContextMenu>
                                                <Templates>
                                                    <EditForm>
                                                    </EditForm>
                                                </Templates>
                                                <SettingsPager NumericButtonCount="50" PageSize="50">
                                                    <PageSizeItemSettings Items="50, 100, 200, 500, 1000" Visible="True">
                                                    </PageSizeItemSettings>
                                                </SettingsPager>
                                                <SettingsEditing Mode="PopupEditForm">
                                                </SettingsEditing>
                                                <Settings ShowFooter="True" ShowHeaderFilterButton="True" />
                                                <SettingsBehavior AllowFocusedRow="True" />
                                                <SettingsPopup>
                                                    <EditForm Modal="True" />
                                                    <CustomizationWindow HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" />
                                                </SettingsPopup>
                                                <SettingsText CommandNew="Nuevo" />
                                                <Columns>
                                                    <dx:GridViewCommandColumn ShowInCustomizationForm="True" Visible="False" VisibleIndex="8">
                                                        <CustomButtons>
                                                            <dx:GridViewCommandColumnCustomButton ID="btnAnular" Text="Anular">
                                                            </dx:GridViewCommandColumnCustomButton>
                                                            <dx:GridViewCommandColumnCustomButton ID="btnReimprimir" Text="Reimprimir">
                                                            </dx:GridViewCommandColumnCustomButton>
                                                        </CustomButtons>
                                                    </dx:GridViewCommandColumn>
                                                    <dx:GridViewDataTextColumn Caption="CITE DEC. JURADA" FieldName="StrProducto" ShowInCustomizationForm="True" VisibleIndex="3">
                                                        <Settings AutoFilterCondition="Contains" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="OBSERVACIONES" FieldName="DecTotalIng" ShowInCustomizationForm="True" ToolTip="Tipo de Consumidor" VisibleIndex="7">
                                                        <Settings AutoFilterCondition="Contains" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="DEPTO." FieldName="DecSaldoInicial" ShowInCustomizationForm="True" VisibleIndex="0">
                                                        <Settings AutoFilterCondition="Contains" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="ESTADO" FieldName="DecDemasia" ShowInCustomizationForm="True" VisibleIndex="6">
                                                        <Settings AutoFilterCondition="Contains" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="RAZON SOCIAL" FieldName="IntId" ShowInCustomizationForm="True" VisibleIndex="2">
                                                        <Settings AutoFilterCondition="Contains" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="FECHA PRESENTACION" FieldName="StrFecha" ShowInCustomizationForm="True" VisibleIndex="1">
                                                        <Settings AutoFilterCondition="Equals" />
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
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan="7">
                                            <dx:ASPxGridView ID="dgvAcumulado" runat="server" AutoGenerateColumns="False" ClientInstanceName="dgvAcumulado" EnableTheming="True" Font-Size="Smaller" KeyFieldName="IntId" OnCustomSummaryCalculate="dgvAcumulado_CustomSummaryCalculate" OnHtmlRowPrepared="dgvAcumulado_HtmlRowPrepared" Theme="Office2003Olive" Width="100%">
                                                <SettingsContextMenu EnableRowMenu="False">
                                                </SettingsContextMenu>
                                                <Templates>
                                                    <EditForm>
                                                    </EditForm>
                                                </Templates>
                                                <SettingsPager NumericButtonCount="50" PageSize="50">
                                                    <PageSizeItemSettings Items="50, 100, 200, 500, 1000" Visible="True">
                                                    </PageSizeItemSettings>
                                                </SettingsPager>
                                                <SettingsEditing Mode="PopupEditForm">
                                                </SettingsEditing>
                                                <Settings ShowFooter="True" ShowHeaderFilterButton="True" />
                                                <SettingsBehavior AllowFocusedRow="True" />
                                                <SettingsPopup>
                                                    <EditForm Modal="True" />
                                                    <CustomizationWindow HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" />
                                                </SettingsPopup>
                                                <SettingsText CommandNew="Nuevo" />
                                                <Columns>
                                                    <dx:GridViewCommandColumn ShowInCustomizationForm="True" Visible="False" VisibleIndex="22">
                                                    </dx:GridViewCommandColumn>
                                                    <dx:GridViewDataTextColumn Caption="RAZON SOCIAL" FieldName="DecRecepcion" ShowInCustomizationForm="True" ToolTip="Tipo de Pago" VisibleIndex="2">
                                                        <Settings AutoFilterCondition="Contains" />
                                                        <CellStyle HorizontalAlign="Right">
                                                        </CellStyle>
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="S.I. GE" FieldName="DecTotalIng" ShowInCustomizationForm="True" ToolTip="Tipo de Consumidor" VisibleIndex="4">
                                                        <Settings AutoFilterCondition="Contains" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="REC. GE" FieldName="DecVenta" ShowInCustomizationForm="True" ToolTip="Tipo de Crédito Fiscal" VisibleIndex="5">
                                                        <Settings AutoFilterCondition="Contains" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="D/M  GE" FieldName="DecMerma" ShowInCustomizationForm="True" VisibleIndex="6">
                                                        <Settings AutoFilterCondition="Contains" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="FECHA" FieldName="DecSaldoInicial" ShowInCustomizationForm="True" VisibleIndex="1">
                                                        <Settings AutoFilterCondition="Contains" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="NO. CITE DJ" FieldName="DecDemasia" ShowInCustomizationForm="True" VisibleIndex="3">
                                                        <Settings AutoFilterCondition="Contains" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="S.F. GE" FieldName="DecSaldoFinal" ShowInCustomizationForm="True" ToolTip="Tipo de Factura" VisibleIndex="8">
                                                        <Settings AutoFilterCondition="Equals" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="VTA. GE" FieldName="DecTotalSal" ShowInCustomizationForm="True" VisibleIndex="7">
                                                        <Settings AutoFilterCondition="Contains" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="DEPTO." FieldName="StrFecha" ShowInCustomizationForm="True" VisibleIndex="0">
                                                        <Settings AutoFilterCondition="Equals" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="S.F. GP" ShowInCustomizationForm="True" VisibleIndex="15">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="VTA. GP" ShowInCustomizationForm="True" VisibleIndex="14">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="D/M GP" ShowInCustomizationForm="True" VisibleIndex="13">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="REC. GP" ShowInCustomizationForm="True" VisibleIndex="12">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="S.I. GP" ShowInCustomizationForm="True" VisibleIndex="11">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="S.F. DO" ShowInCustomizationForm="True" VisibleIndex="20">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="VTA. DO" ShowInCustomizationForm="True" VisibleIndex="19">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="D/M DO" ShowInCustomizationForm="True" VisibleIndex="18">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="REC. DO" ShowInCustomizationForm="True" VisibleIndex="17">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="S.I. DO" ShowInCustomizationForm="True" VisibleIndex="16">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="VTA. GNV" ShowInCustomizationForm="True" VisibleIndex="21">
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
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan="7">
                                            <dx:ASPxGridView ID="dgvInternacional" runat="server" AutoGenerateColumns="False" ClientInstanceName="dgvInternacional" EnableTheming="True" Font-Size="Smaller" KeyFieldName="LngId" Theme="Office2003Olive" Width="100%">
                                                <ClientSideEvents Init="function(s, e) {
	dgvInternacional.SetVisible(false);
}" />
                                                <SettingsContextMenu EnableRowMenu="False">
                                                </SettingsContextMenu>
                                                <Templates>
                                                    <EditForm>
                                                    </EditForm>
                                                </Templates>
                                                <SettingsPager NumericButtonCount="50" PageSize="50">
                                                    <PageSizeItemSettings Items="50, 100, 200, 500, 1000" Visible="True">
                                                    </PageSizeItemSettings>
                                                </SettingsPager>
                                                <SettingsEditing Mode="PopupEditForm">
                                                </SettingsEditing>
                                                <Settings ShowFooter="True" ShowHeaderFilterButton="True" />
                                                <SettingsBehavior AllowFocusedRow="True" />
                                                <SettingsPopup>
                                                    <EditForm Modal="True" />
                                                    <CustomizationWindow HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" />
                                                </SettingsPopup>
                                                <SettingsText CommandNew="Nuevo" />
                                                <Columns>
                                                    <dx:GridViewCommandColumn ShowInCustomizationForm="True" Visible="False" VisibleIndex="21">
                                                    </dx:GridViewCommandColumn>
                                                    <dx:GridViewDataTextColumn Caption="RAZON SOCIAL" FieldName="StrPlaca" ShowInCustomizationForm="True" VisibleIndex="2">
                                                        <Settings AutoFilterCondition="Contains" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="NO. CITE DJ" FieldName="LngNit" ShowInCustomizationForm="True" ToolTip="Tipo de Pago" VisibleIndex="3">
                                                        <Settings AutoFilterCondition="Contains" />
                                                        <CellStyle HorizontalAlign="Right">
                                                        </CellStyle>
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="CANTIDAD GE" FieldName="LngNoFactProp" ShowInCustomizationForm="True" ToolTip="Tipo de Consumidor" VisibleIndex="6">
                                                        <Settings AutoFilterCondition="Contains" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="CANTIDAD GP" FieldName="DecImporteExt" ShowInCustomizationForm="True" ToolTip="Tipo de Crédito Fiscal" VisibleIndex="7">
                                                        <Settings AutoFilterCondition="Contains" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="CANTIDAD DO" FieldName="DecImporteProp" ShowInCustomizationForm="True" VisibleIndex="8">
                                                        <Settings AutoFilterCondition="Contains" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="CANTIDAD GNV" FieldName="DecPrecioDo" ShowInCustomizationForm="True" VisibleIndex="9">
                                                        <Settings AutoFilterCondition="Contains" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="FECHA" FieldName="StrFecha" ShowInCustomizationForm="True" VisibleIndex="1">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="IMPORTE EESS" FieldName="LngNoFactExt" ShowInCustomizationForm="True" VisibleIndex="5">
                                                        <Settings AutoFilterCondition="Contains" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="IMPORTE YPFB" FieldName="StrProducto" ShowInCustomizationForm="True" VisibleIndex="4">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="DEPTO." FieldName="LngId" ShowInCustomizationForm="True" VisibleIndex="0">
                                                    </dx:GridViewDataTextColumn>
                                                </Columns>
                                                <TotalSummary>
                                                    <dx:ASPxSummaryItem FieldName="DecImporteProp" ShowInColumn="IMP. EESS" SummaryType="Custom" Tag="impP" />
                                                    <dx:ASPxSummaryItem FieldName="DecImporteExt" ShowInColumn="IMP. YPFB" SummaryType="Custom" Tag="impE" />
                                                    <dx:ASPxSummaryItem FieldName="DecCantidadGe" ShowInColumn="VOL. GE" SummaryType="Custom" Tag="canGe" />
                                                    <dx:ASPxSummaryItem FieldName="DecCantidadDo" ShowInColumn="VOL. DO" SummaryType="Custom" Tag="canDo" />
                                                    <dx:ASPxSummaryItem FieldName="DecCantidadGn" ShowInColumn="VOL. GNV" SummaryType="Custom" Tag="canGn" />
                                                </TotalSummary>
                                            </dx:ASPxGridView>
                                        </td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style11">&nbsp;</td>
                                        <td class="auto-style10">&nbsp;</td>
                                        <td class="auto-style10">&nbsp;</td>
                                        <td class="auto-style12">&nbsp;</td>
                                        <td class="auto-style10">
                                            &nbsp;</td>
                                        <td class="auto-style10">&nbsp;</td>
                                        <td class="auto-style10">
                                            &nbsp;</td>
                                        <td class="auto-style10">&nbsp;</td>
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








