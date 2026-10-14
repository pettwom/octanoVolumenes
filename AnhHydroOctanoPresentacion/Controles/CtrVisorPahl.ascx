<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtrVisorPahl.ascx.cs" Inherits="AnhHydroOctanoPresentacion.Controles.CtrVisorPahl" %>

<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxPivotGrid.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPivotGrid" tagprefix="dx" %>

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
    .auto-style13 {
        width: 103%;
    }
    .auto-style16 {
        width: 220px;
    }
    .auto-style18 {
        width: 108px;
    }
    .auto-style19 {
        width: 88px;
    }
    .auto-style20 {
        width: 103%;
        height: 27px;
    }
    .auto-style21 {
        width: 108px;
        height: 27px;
    }
    .auto-style22 {
        width: 220px;
        height: 27px;
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
                        <dx:PanelContent ID="PanelContent1" runat="server">
                            <dx:ASPxLabel runat="server" ForeColor="#367B2F" ID="lblTitulo" Font-Bold="True" Font-Names="Rockwell" Font-Size="Larger" Text="VISOR PLANTA DE ALMACENAJE DE HIDROCARBUROS LIQUIDOS (PAHL)"></dx:ASPxLabel>
                        </dx:PanelContent>
                    </PanelCollection>
                    <Border BorderStyle="Ridge" BorderWidth="1px" BorderColor="#003300"></Border>
                </dx:ASPxRoundPanel>
            </td>
        </tr>
    </table>

    <dx:ASPxSplitter ID="SplEntorno" runat="server" Height="100%" ClientInstanceName="SplEntorno">
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
                                <td>
                                    <dx:ASPxCallbackPanel ID="pnlCabecera" runat="server" ClientInstanceName="pnlCabecera" OnCallback="pnlCabecera_Callback" Width="100%">
                                        <PanelCollection>
                                            <dx:PanelContent runat="server">
                                                
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td>Fecha Inicial :<dx:ASPxDateEdit ID="detFechaIni" runat="server" ClientInstanceName="detFechaIni" EnableTheming="True" Height="23px" Theme="Metropolis" ToolTip="Seleccione la fecha inicial" Width="159px">
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
                                                        <td>Fecha Final :<dx:ASPxDateEdit ID="detFechaFin" runat="server" ClientInstanceName="detFechaFin" Theme="Metropolis" ToolTip="Seleccione la fecha final" Width="159px">
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
                                                      <td>&nbsp;</td>
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
                                                        <td style="padding-bottom: 5px">                                                            
                                                        </td>.
                                                    </tr>
                                                </table>

                                            </dx:PanelContent>
                                        </PanelCollection>
                                    </dx:ASPxCallbackPanel>
                                </td>
                            </tr>
                             <tr>
                                <td>
                                    <dx:ASPxTreeList ID="tlsOperadores" runat="server" AutoGenerateColumns="False" KeyFieldName="intId" ClientInstanceName="tlsOperadores" Font-Size="Smaller" Height="187px"  ParentFieldName="intIdPadre" Theme="Youthful" Styles-Header-BackColor="#317D40" ToolTip="Seleccione un Proyecto">
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
            <dx:SplitterPane ScrollBars="Auto">
                <ContentCollection>
                    <dx:SplitterContentControl ID="SplitterContentControl2" runat="server">                                               
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
                                            <td colspan="6" style="text-align: center;">
                                                <dx:ASPxRoundPanel ID="pnlMensaje" runat="server" ShowHeader="False" Visible="False" Width="100%">
                                                    <PanelCollection>
                                                        <dx:PanelContent runat="server">
                                                            <dx:ASPxLabel ID="lblMensajeError" runat="server" ForeColor="#666666" Width="100%">
                                                            </dx:ASPxLabel>
                                                        </dx:PanelContent>
                                                    </PanelCollection>
                                                    <Border BorderStyle="Ridge" BorderWidth="1px" />
                                                </dx:ASPxRoundPanel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                                <legend>
                                                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Rockwell" Font-Size="Medium" ForeColor="#367B2F" Text="DATOS DEL EXPLORADOR"></asp:Label>
                                                </legend>
                                            </td>
                                        </tr>
                                        <%--<tr>
                                            <td colspan="3">
                                                <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="#10420F" Text="ACTIVIDAD"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label3" runat="server" Font-Bold="True" ForeColor="#10420F" Text=""></asp:Label>
                                            </td>
                                            <td colspan="2">
                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" style="padding-bottom: 5px; width: 50%">
                                                <asp:Label ID="lblActividad" runat="server"></asp:Label>
                                            </td>
                                            <td style="padding-bottom: 5px; width: 10%">
                                                <asp:Label ID="lblCantidad" runat="server"></asp:Label>
                                            </td>
                                            <td colspan="2" style="padding-bottom: 5px; width: 40%">
                                                
                                            </td>
                                        </tr>--%>
                                      <tr>
                                          <td style="text-align: right;">
                                              <dx:ASPxButton ID="btnEmitir" runat="server" Font-Bold="True" ForeColor="Maroon" OnClick="btnEmitir_Click" Text="Exportar Aeronaves Registradas" Theme="Metropolis" Width="250px">
                                                    <Image IconID="export_exporttoxlsx_16x16">
                                                    </Image>
                                                </dx:ASPxButton>
                                          </td>
                                      </tr>
                                        <tr>
                                        
                                            <td colspan="6">
                                               <dx:ASPxGridView ID="dgvListadoPahl" runat="server" AutoGenerateColumns="False" ClientInstanceName="dgvListadoPahl" EnableTheming="True" Font-Size="Smaller" KeyFieldName="IDENTIFICADOR" Theme="Default" Width="100%">
                                                    <Styles>
                                                        <Header Wrap="True" HorizontalAlign="Center"></Header>
                                                        <AlternatingRow BackColor="#EEEEEE"></AlternatingRow>
                                                    </Styles>
                                                    <ClientSideEvents CustomButtonClick="function(s, e) {	
                                                            switch(e.buttonID) {
                                                            case&nbsp; 'btnFotos':
                                                            imgFotos.PerformCallback();
                                                            s.GetRowValues(e.visibleIndex, 'IDENTIFICADOR', function (value) {
                                                            pucFoto.SetHeaderText('Fotografias - Placa:  '+value);
                                                            });
                                                            pucFoto.Show();
                                                            pucFoto.PerformCallback();
                                                            break;
                                                           } 

                                                        }" />
                                                    <SettingsContextMenu EnableColumnMenu="True" Enabled="True">
                                                    </SettingsContextMenu>
                                                    <Templates>
                                                        <EditForm>
                                                        </EditForm>
                                                    </Templates>
                                                    <SettingsPager PageSize="100">
                                                        <PageSizeItemSettings Items="50, 100, 500, 1000" Visible="True" ShowAllItem="True">
                                                        </PageSizeItemSettings>
                                                    </SettingsPager>
                                                    <SettingsEditing Mode="EditForm">
                                                    </SettingsEditing>
                                                    <Settings ShowHeaderFilterButton="True" ShowFilterRow="True" ShowGroupPanel="True" ShowFooter="True" ShowGroupFooter="VisibleIfExpanded" />
                                                    <SettingsBehavior AllowFocusedRow="True" EnableCustomizationWindow="True" ColumnResizeMode="NextColumn" />
                                                    <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
                                                    <SettingsPopup>
                                                        <EditForm Modal="True" />
                                                        <CustomizationWindow HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" />
                                                    </SettingsPopup>
                                                    <SettingsSearchPanel Visible="True" />
                                                    <SettingsText CommandNew="Nuevo" />
                                                    <Columns>
                                                        <dx:GridViewCommandColumn ShowInCustomizationForm="True" VisibleIndex="40" ShowClearFilterButton="True" ToolTip="Funciones">
                                                            <CustomButtons>                                                       
                                                                <dx:GridViewCommandColumnCustomButton ID="btnFotos" Text=" ">
                                                                    <Image IconID="toolboxitems_picturebox_16x16" ToolTip="Ver Fotografías" >                                                                        
                                                                    </Image>

                                                                </dx:GridViewCommandColumnCustomButton>                                                     
                                                            </CustomButtons>
                                                        </dx:GridViewCommandColumn>                                                      
                                                        <dx:GridViewDataTextColumn Caption="DEPARTAMENTO" FieldName="DEPTO" ShowInCustomizationForm="True" VisibleIndex="1">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="FECHA LECTURA" FieldName="FECHA_LECTURA" ShowInCustomizationForm="True" VisibleIndex="13" ToolTip="Fecha de Lectura RFID">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="PLANTA" FieldName="PLANTA" ShowInCustomizationForm="True" VisibleIndex="10">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="TARJETA" FieldName="BARCODE" ShowInCustomizationForm="True" VisibleIndex="14" ToolTip="Tarjeta Asignada">
                                                        </dx:GridViewDataTextColumn>                                                
                                                        <dx:GridViewDataTextColumn Caption="IDENTIFICADOR" FieldName="IDENTIFICADOR" ShowInCustomizationForm="True" VisibleIndex="16">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="CLASE" FieldName="CLASE" ShowInCustomizationForm="True" VisibleIndex="17">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="NRO_COMPARTIMIENTO" FieldName="NRO_COMPARTIMIENTO" ShowInCustomizationForm="True" VisibleIndex="18">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="VOLUMEN_NOMINAL" FieldName="VOLUMEN_NOMINAL" ShowInCustomizationForm="True" VisibleIndex="19">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="VOLUMEN_NOMINAL_COMPTO" FieldName="VOLUMEN_NOMINAL_COMPTO" ShowInCustomizationForm="True" VisibleIndex="20">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="CODIGO ANTENA" FieldName="CODIGO_ANTENA" ShowInCustomizationForm="True" ToolTip="Codigo Antena" VisibleIndex="12">
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                    <Styles CssPostfix="DevGlp">
                                                        <AlternatingRow BackColor="#EEEEEE">
                                                        </AlternatingRow>
                                                    </Styles>
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
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>                                       
                                  
                                        <tr>
                                            <td>
                                                <dx:ASPxGridViewExporter ID="dgeExportar" runat="server">
                                                </dx:ASPxGridViewExporter>
                                            </td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>                                              
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

<dx:ASPxPopupControl ID="pucFoto" runat="server" AllowDragging="True" ClientInstanceName="pucFoto" EnableTheming="True" HeaderText="Imagen" Height="93px" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Theme="Metropolis" Width="350px" OnWindowCallback="pucFoto_WindowCallback" AllowResize="True" PopupAnimationType="None">
    <HeaderImage IconID="toolboxitems_picturebox_32x32">
    </HeaderImage>
    <HeaderStyle>
    <BorderBottom BorderStyle="Solid" />
    </HeaderStyle>
    <ContentCollection>
        <dx:PopupControlContentControl runat="server">
            <table class="dxflInternalEditorTable_Youthful" style="width: 100%">
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>               
                </tr>
                <tr>
                    <td>
                        <dx:ASPxImageGallery ID="imgFotos" runat="server" EnableViewState="False" ImageContentBytesField="IMAGEN" ClientInstanceName="imgFotos" OnCustomCallback="imgFotos_CustomCallback" Theme="Metropolis" ThumbnailImageSizeMode="ActualSizeOrFit" AllowPaging="False" Paddings-Padding="12px">
                            <SettingsFolder ImageCacheFolder="~/Thumb/" />
                            <SettingsTableLayout RowsPerPage="1" ColumnCount="1" />

                            <ItemTextTemplate>
                                <div class="item">
                                    <div class="campoValor">PLACA: <%# Container.EvalDataItem("IDENTIFICADOR") %></div>
                                    <div class="campoValor">BARCODE: <%# Container.EvalDataItem("BARCODE") %></div>
                                    <div class="campoValor">MARCA: <%# Container.EvalDataItem("MARCA") %></div>
                                </div>
                            </ItemTextTemplate>
                            <FullscreenViewerItemTextTemplate>
                                <div class="details">
                                    <div class="campoValor">PLACA: <%# Container.EvalDataItem("IDENTIFICADOR") %></div>
                                    <div class="campoValor">BARCODE: <%# Container.EvalDataItem("BARCODE") %></div>
                                    <div class="campoValor">MARCA: <%# Container.EvalDataItem("MARCA") %></div>
                                </div>
                            </FullscreenViewerItemTextTemplate>

                        </dx:ASPxImageGallery>
                    </td>               
                    <td>
                       <table style="width:150px">
                           <tr>
                               <td>
                                   <strong>PLACA: </strong><asp:Label ID="lblPlaca" runat="server"></asp:Label>
                               </td>
                           </tr>
                            <tr>
                               <td>
                                   <strong>BARCODE: </strong><asp:Label ID="lblBarcode" runat="server"></asp:Label>
                               </td>
                           </tr>
                            <tr>
                               <td>
                                   <strong>MARCA: </strong><asp:Label ID="lblMarca" runat="server"></asp:Label> 
                               </td>
                           </tr>
                       </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">&nbsp;</td>
                </tr>
            </table>
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>