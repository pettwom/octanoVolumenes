<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtrRepKardex.ascx.cs" Inherits="AnhHydroOctanoPresentacion.Controles.CtrRepKardex" %>

<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>

<%@ Register src="CtrFecheroMes.ascx" tagname="CtrFecheroMes" tagprefix="uc1" %>

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
    
    .auto-style15 {
        height: 18px;
    }
    
    </style>


<div class="col-md-12">
    
    <table style="width: 100%; height: 100%">
    <tr>
        <td style="text-align: center; padding-bottom: 1px">
            <dx:ASPxRoundPanel runat="server" ShowHeader="False" Width="100%" ID="pnlTitulo">
                <PanelCollection>
                    <dx:PanelContent runat="server">
                        <dx:ASPxLabel runat="server" ForeColor="#367B2F" ID="lblTitulo" Font-Bold="True" Font-Names="Rockwell" Font-Size="Larger" Text="REPORTE - KARDEX"></dx:ASPxLabel>
                </dx:PanelContent>
            </PanelCollection>
            <Border BorderStyle="Ridge" BorderWidth="1px" BorderColor="#003300"></Border>
            </dx:ASPxRoundPanel>
        </td>
    </tr>
    
    <tr>
        <td style="height: 100%">
            
            <dx:ASPxSplitter ID="ASPxSplitter1" runat="server" Height="98%" Width="100%" >
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
                                <dx:ASPxCallbackPanel ID="pnlCabecera" runat="server" ClientInstanceName="pnlCabecera" Width="100%" OnCallback="pnlCabecera_Callback">
                                    <PanelCollection>
                                        <dx:PanelContent runat="server">
                                            <table style="width: 100%">
                                                <tr>
                                                    <td>
                                                        Fecha Inicial :
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
                                                <tr style="padding-bottom: 5px">
                                                    <td>
                                                        Fecha Final :
                                                        <dx:ASPxDateEdit ID="detFechaFin" runat="server" ClientInstanceName="detFechaFin" Theme="Metropolis" Width="140px" ToolTip="Seleccione la fecha final">
                                                            <TimeSectionProperties>
                                                                <TimeEditProperties>
                                                                    <ClearButton Visibility="Auto">
                                                                    </ClearButton>
                                                                </TimeEditProperties>
                                                            </TimeSectionProperties>
                                                            <ClientSideEvents DateChanged="function(s, e) {
//pnlDatos.PerformCallback();
}" />
                                                            <ClearButton Visibility="Auto">
                                                            </ClearButton>
                                                        </dx:ASPxDateEdit>
                                                    </td>
                                                </tr>
                                                <tr style="padding-bottom: 5px">
                                                    <td>Selección Directa de Mes :<uc1:CtrFecheroMes ID="cfmMes" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr style="padding-bottom: 5px">
                                                    <td>
                                                        Agrupado Por :
                                                        <dx:ASPxRadioButtonList ID="rbtAgrupa" runat="server" EnableTheming="True" SelectedIndex="0" Theme="Metropolis" ValueType="System.Int32">
                                                            <Items>
                                                                <dx:ListEditItem Text="INFRAESTRUCTURA" Value="1" Selected="True" />
                                                                <dx:ListEditItem Text="PRODUCTO" Value="2" />
                                                            </Items>
                                                        </dx:ASPxRadioButtonList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </dx:PanelContent>
                                    </PanelCollection>
                                </dx:ASPxCallbackPanel>
                            </td>
                        </tr>
                        

                        <tr>
                            <td style="padding-bottom: 5px">
                                <dx:ASPxButton ID="btnConsultar" runat="server" AutoPostBack="False" Font-Bold="True" ForeColor="Maroon" Text="Obtener Información" Theme="Metropolis" Width="65%">
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
                                <dx:ASPxButton ID="btnBuscarProyecto" runat="server" AutoPostBack="false" ClientInstanceName="btnBuscarProyecto" Text="Buscar Proyecto" >
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
pnlCabecera.PerformCallback();
}" FocusedNodeChanged="function(s, e) {
pnlCabecera.PerformCallback();
}" />
                                </dx:ASPxTreeList>
                            </td>
                        </tr>

                        <tr>
                            <td>
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
                        <ClientSideEvents EndCallback="function(s, e) {
LoadingPanel.Hide();
}" BeginCallback="function(s, e) {
LoadingPanel.Show();
}" />
                        <PanelCollection>
                            <dx:PanelContent runat="server">
                                <table style="width: 100%;">
                                    <tr>
                                        <td colspan="4" style="text-align: center;width: 100%;">
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
                                        <td colspan="4">
                                            <legend>
                                                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Rockwell" Font-Size="Medium" ForeColor="#367B2F" Text="DATOS DEL PROYECTO"></asp:Label>
                                            </legend>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 50%">
                                            <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="#10420F" Text="NOMBRE COMERCIAL"></asp:Label>
                                        </td>
                                        <td style="width: 10%">
                                            <asp:Label ID="Label3" runat="server" Font-Bold="True" ForeColor="#10420F" Text="NIT"></asp:Label>
                                        </td>
                                        <td style="width: 15%">
                                            <asp:Label ID="Label5" runat="server" Font-Bold="True" ForeColor="#10420F" Text="DEPARTAMENTO"></asp:Label>
                                        </td>
                                        <td style="width: 25%">
                                            <asp:Label ID="Label4" runat="server" Font-Bold="True" ForeColor="#10420F" Text="DIRECCION"></asp:Label>
                                        </td>

                                    </tr>
                                        <tr>
                                            <td style="width: 50%; padding-bottom: 7px">
                                                <dx:ASPxLabel ID="lblRazonSocial" runat="server" ClientInstanceName="lblRazonSocial">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td style="width: 10%">
                                                <asp:Label ID="lblNit" runat="server"></asp:Label>
                                            </td>
                                            <td style="width: 15%">
                                                <asp:Label ID="lblDepto" runat="server"></asp:Label>
                                            </td>
                                            <td style="width: 25%; padding-bottom: 7px">
                                                <asp:Label ID="lblRepLegal" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="width: 50%">
                                                <dx:ASPxLabel ID="ASPxLabel61" runat="server" Width="100%">
                                                    <BorderBottom BorderColor="#FF6600" BorderStyle="Solid" />
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
                                            <td style="width: 20%">
                                                <dx:ASPxButton ID="btnEmiteReporte" runat="server" AutoPostBack="False" Font-Bold="True" ForeColor="Maroon" Text="Emitir Reporte" Theme="Metropolis" Width="100%" OnClick="btnEmiteReporte_Click">
                                                    <Image IconID="print_printer_16x16">
                                                    </Image>
                                                </dx:ASPxButton>
                                            </td>
                                    </tr>
                                        <tr>
                                            <td colspan="4">
                                                
                                                <dx:ASPxGridView ID="dgvReporte" runat="server" AutoGenerateColumns="False" ClientInstanceName="dgvReporte" EnableTheming="True" Font-Size="Smaller" KeyFieldName="FECHA_OPERACION" Theme="Default" Width="100%" >
                                                    <ClientSideEvents CustomButtonClick="function(s, e) {
	
    switch(e.buttonID) {
    case&nbsp; 'btnMed':
    onCellClick(s.GetFocusedRowIndex(), 'MDIN',this);
    break;
    } 


}" />
                                                    <SettingsContextMenu EnableRowMenu="False">
                                                    </SettingsContextMenu>
                                                    <Templates>
                                                        <EditForm>
                                                        </EditForm>
                                                    </Templates>
                                                    <SettingsPager PageSize="500">
                                                        <PageSizeItemSettings Items="50, 100, 200, 500, 1000, 2000" Visible="True">
                                                        </PageSizeItemSettings>
                                                    </SettingsPager>
                                                    <SettingsEditing Mode="PopupEditForm">
                                                    </SettingsEditing>
                                                    <Settings ShowFooter="True" ShowGroupFooter="VisibleAlways" ShowGroupPanel="True" ShowHeaderFilterButton="True" />
                                                    <SettingsBehavior AllowFocusedRow="True" />
                                                    <SettingsPopup>
                                                        <EditForm Modal="True" />
                                                        <CustomizationWindow HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" />
                                                    </SettingsPopup>
                                                    <SettingsText CommandNew="Nuevo" />
                                                    
                                                    <Columns>
                                                        <dx:GridViewCommandColumn ShowInCustomizationForm="True" Visible="False" VisibleIndex="50">
                                                        </dx:GridViewCommandColumn>
                                                        <dx:GridViewDataTextColumn Caption="TIPO_INFRAESTRUCTURA" FieldName="TIPO_INFRAESTRUCTURA" ShowInCustomizationForm="True" Visible="False" VisibleIndex="2">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="ID_INFRAESTRUCTURA" FieldName="ID_INFRAESTRUCTURA" ShowInCustomizationForm="True" Visible="False" VisibleIndex="3">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="TIPO INFRA." FieldName="INF_TIPO" ShowInCustomizationForm="True" ToolTip="Tipo de Infraestructura" VisibleIndex="4">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="COD. INFRA." FieldName="INF_CODIGO" ShowInCustomizationForm="True" ToolTip="Código de Infraestructura" VisibleIndex="5">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="ID_HIDROCARBURO" FieldName="ID_HIDROCARBURO" ShowInCustomizationForm="True" ToolTip="Tipo de Infrastructura" Visible="False" VisibleIndex="6">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="PRODUCTO" FieldName="PRODUCTO" ShowInCustomizationForm="True" VisibleIndex="7">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="ID_UNIDAD_HIDROCARBURO" FieldName="ID_UNIDAD_HIDROCARBURO" ShowInCustomizationForm="True" Visible="False" VisibleIndex="8">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="U. MEDIDA" FieldName="U_MEDIDA" ShowInCustomizationForm="True" ToolTip="Unidad de Medida" VisibleIndex="9">
                                                            <PropertiesTextEdit ClientInstanceName="DecInf01" EnableClientSideAPI="True">
                                                            </PropertiesTextEdit>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="FECHA_OPERACION" FieldName="FECHA_OPERACION" ShowInCustomizationForm="True" Visible="False" VisibleIndex="10">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="ID_TIPO_OPERACION" FieldName="ID_TIPO_OPERACION" ShowInCustomizationForm="True" Visible="False" VisibleIndex="12">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="TIPO OPERACION" FieldName="TIPO_OPERACION" ShowInCustomizationForm="True" VisibleIndex="13">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="FACTOR_CALCULO" FieldName="FACTOR_CALCULO" ShowInCustomizationForm="True" Visible="False" VisibleIndex="14">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="CANTIDAD" FieldName="CANTIDAD" ShowInCustomizationForm="True" Visible="False" VisibleIndex="15">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="IDENTIFICADOR" FieldName="IDENTIFICADOR" ShowInCustomizationForm="True" VisibleIndex="16">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="FECHA" FieldName="FECHA" ShowInCustomizationForm="True" VisibleIndex="11">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="INGRESO" FieldName="INGRESO" ShowInCustomizationForm="True" VisibleIndex="17">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="SALIDA" FieldName="SALIDA" ShowInCustomizationForm="True" VisibleIndex="18">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="SALDO" FieldName="SALDO" ShowInCustomizationForm="True" VisibleIndex="19">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="ID_AGRUPACION" FieldName="ID_AGRUPACION" ShowInCustomizationForm="True" Visible="False" VisibleIndex="0">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="AGRUPACION" FieldName="AGRUPACION" GroupIndex="0" ShowInCustomizationForm="True" SortIndex="0" SortOrder="Ascending" VisibleIndex="1">
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                    <Styles CssPostfix="DevGlp"><AlternatingRow BackColor="#EEEEEE">
                                                        </AlternatingRow></Styles>
                                                    <TotalSummary>
                                                        <dx:ASPxSummaryItem FieldName="SALIDA" ShowInColumn="SALIDA" SummaryType="Sum" Tag="SALIDA" DisplayFormat="{0}" />
                                                        <dx:ASPxSummaryItem FieldName="INGRESO" ShowInColumn="INGRESO" SummaryType="Sum" Tag="INGRESO" DisplayFormat="{0}" />
                                                    </TotalSummary>
                                                    <GroupSummary>
                                                        <dx:ASPxSummaryItem FieldName="SALIDA" ShowInGroupFooterColumn="SALIDA" SummaryType="Sum" DisplayFormat="{0}" />
                                                        <dx:ASPxSummaryItem FieldName="INGRESO" ShowInGroupFooterColumn="INGRESO" SummaryType="Sum" DisplayFormat="{0}" />
                                                    </GroupSummary>
                                                </dx:ASPxGridView>
                                                
                                            </td>
                                        </tr>


                                    

                                    <tr>
                                        <td class="auto-style15" colspan="4">
                                            <dx:ASPxGridViewExporter ID="dgeExportar" runat="server">
                                            </dx:ASPxGridViewExporter>
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

        </td>
    </tr>
</table>

</div>




    








    
