<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtrFichaTecnicaPrincipal.ascx.cs" Inherits="AnhHydroOctanoPresentacion.Controles.CtrFichaTecnicaPrincipal" %>

<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>

<%@ Register TagPrefix="dx" Namespace="DevExpress.Web" Assembly="DevExpress.Web.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>
<%@ Register TagPrefix="dxcharts" Namespace="DevExpress.XtraCharts.Web" Assembly="DevExpress.XtraCharts.v15.1.Web, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>
<%@ Register TagPrefix="dxcharts" Namespace="DevExpress.XtraCharts" Assembly="DevExpress.XtraCharts.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>
<%@ Register assembly="DevExpress.Web.ASPxPivotGrid.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPivotGrid" tagprefix="dx" %>

<%@ Register tagPrefix="asp" namespace="System.Web.UI.DataVisualization.Charting" assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"%>


<style type="text/css">
    .auto-style1 {
        height: 18px;
    }
    .auto-style11 {
        width: 168px;
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
<script>
    //function FindNode(s, e) {
    //    alert('entro');
    //    ////Check the input data
    //    //if (txbBuscador.GetText() == '') {
    //    //    alert('Please, input name of a node');
    //    //    return;
    //    //}
    //    //if (tlsOperadores.GetNodeByText(txbBuscador.GetText()) == null) {
    //    //    alert('The ' + txbBuscador.GetText() + ' node was not found');
    //    //    return;
    //    //}
    //    //var node = tlsOperadores.GetNodeByText(txbBuscador.GetText());

    //    ////Iterate through the parent nodes to expand them
    //    //var nodesparent = node.parent;
    //    //while (nodesparent != null) {
    //    //    nodesparent.SetExpanded(true);
    //    //    nodesparent = nodesparent.parent;
    //    //}

    //    ////Select the found node
    //    //tlsOperadores.SetSelectedNode(node);
    //}

    function Traverce(node) {
        if (node.GetNodeCount() > 0)
            for (var i = 0; i < node.GetNodeCount() ; i++) {
                var currentnode = node.GetNode(i);
                if (currentnode.GetText().toLowerCase().indexOf(searchedString) != -1) {
                    var nodesparent = currentnode.parent;
                    while (nodesparent != null) {
                        nodesparent.SetExpanded(true);
                        nodesparent = nodesparent.parent
                    }
                    tlsOperadores.SetSelectedNode(currentnode);
                    stop = true;
                    return;
                }
                else {
                    if (!stop) Traverce(currentnode);
                    else {
                        stop = false;
                        return;
                    }
                }
            }
    }
    var searchedString = "";
    var stop = false;

    function FindNode(s, e) {
        //alert('entro');
        searchedString = txbBuscador.GetText();
        if (searchedString == '') {
            alert('Please, input name of a node');
            return;
        }
        searchedString.toLowerCase();
        Traverce(tlsOperadores);
    }    
</script>

<div class="col-md-12">
    <%--<table style="width: 100%">
        <tr>
            <td style="text-align: center; padding-bottom: 1px">
                <dx:ASPxRoundPanel runat="server" ShowHeader="False" Width="100%" ID="pnlTitulo">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <table style="width: 100%">
                                <tr>
                                    <td>
                                        <dx:ASPxLabel runat="server" ForeColor="#367B2F" ID="lblTitulo" Font-Bold="True" Font-Names="Rockwell" Font-Size="Larger" Text="BIENVENIDO"></dx:ASPxLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <dx:ASPxLabel runat="server" ForeColor="#367B2F" ID="ASPxLabel1" Font-Bold="True" Font-Names="Rockwell" Font-Size="Larger" Text="Sistema de Control de Movimiento de Hidrocarburos"></dx:ASPxLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <dx:ASPxLabel runat="server" ForeColor="#367B2F" ID="ASPxLabel2" Font-Bold="True" Font-Names="Rockwell" Font-Size="Larger" Text="OCTANO VOLUMENES"></dx:ASPxLabel>
                                    </td>
                                </tr>
                            </table>
                        
                        </dx:PanelContent>
                    </PanelCollection>
                    <Border BorderStyle="Ridge" BorderWidth="1px" BorderColor="#003300"></Border>
                </dx:ASPxRoundPanel>
            </td>
        </tr>
    </table>--%>
    <dx:ASPxSplitter ID="ASPxSplitter1" runat="server" Height="100%">
        <Panes>
            <dx:SplitterPane Size="25%" AllowResize="True" AutoHeight="True">
                <ContentCollection>
                    <dx:SplitterContentControl runat="server">
                        <%--<table style="width: 100%;">
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
                                <td style="padding-bottom: 5px"></td>
                                .
                            </tr>
                        </table>--%>
                        <table style="width: 100%;">
                            <tr>
                                <td>
                                    <dx:ASPxLabel ID="ASPxLabel48" runat="server" ForeColor="#003300" Text="Proyectos Asignados :">
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
                                    <dx:ASPxTreeList ID="tlsOperadores" runat="server" AutoGenerateColumns="False" ClientInstanceName="tlsOperadores" Font-Size="Smaller" Height="187px" KeyFieldName="intId" ParentFieldName="intIdPadre" Theme="Youthful" Styles-Header-BackColor="#317D40" OnCustomCallback="tlsOperadores_CustomCallback">
                                        <Columns>
                                            <dx:TreeListTextColumn Caption="ID" FieldName="intId" ShowInCustomizationForm="True" Visible="False" VisibleIndex="0">
                                            </dx:TreeListTextColumn>
                                            <dx:TreeListTextColumn Caption="PADRE" FieldName="intIdPadre" ShowInCustomizationForm="True" Visible="False" VisibleIndex="1">
                                            </dx:TreeListTextColumn>
                                            <dx:TreeListTextColumn Caption="DESCRIPCION" FieldName="strDescripcion" ShowInCustomizationForm="True" VisibleIndex="2" ToolTip="Seleccione una entidad">
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
	pnlDatos.PerformCallback();
}"
                                            Init="function(s, e) {
pnlDatos.PerformCallback();
}"
                                            EndCallback="function(s, e) {
pnlDatos.PerformCallback();
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
                    <dx:SplitterContentControl runat="server">

                                    <dx:ASPxCallbackPanel ID="pnlDatos" runat="server" Theme="Youthful" Width="100%" ClientInstanceName="pnlDatos" OnCallback="pnlDatos_Callback">
                            <ClientSideEvents EndCallback="function(s, e) {
	                                LoadingPanel.Hide();
                                OnEndCallback(s, e);
                                }" BeginCallback="function(s, e) {
	                                LoadingPanel.Show();
                                }" />
                            <PanelCollection>
                                <dx:PanelContent runat="server">
                                    <table style="width: 100%;"  border="0">
                                        
                                        <tr>
                                            <td style="width: 15%;">
                                                &nbsp;</td>
                                            
                                            <td></td>
                                            <td></td>
                                            <td>
                                                </td>
                                            <td>&nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td colspan="7" style="text-align:center">
                                                <dx:ASPxRoundPanel ID="pnlMensaje" runat="server" ShowHeader="False" Visible="False" Width="100%">
                                                    <PanelCollection>
                                                        <dx:PanelContent ID="PanelContent1" runat="server">
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
                                                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Rockwell" Font-Size="Medium" ForeColor="#367B2F" Text="FICHA TÉCNICA"></asp:Label>
                                                </legend>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 150px">
                                                <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="#10420F" Text="ACTIVIDAD"></asp:Label>
                                            </td>
                                            <td style="width: 150px">&nbsp;</td>
                                            <td style="width: 150px">&nbsp;</td>
                                            <td >&nbsp;</td>
                                            <td  >&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr><tr><td><div  id="reporte" style = "display:none" ><table border="1"><tr>
                                            <td colspan="2">
                                                <asp:Label ID="lblActividad" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                             &nbsp;&nbsp;<asp:Label ID="Label3" runat="server" Font-Bold="True" ForeColor="#10420F" Text="DATOS GENERALES"></asp:Label>    
                                            </td>
                                            <td class="auto-style1">&nbsp;</td>
                                        </tr>
                                        <tr><td>
                                          

                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <div  id="map_canvas"  style="width: 100%; height: 280px";align-items: center> <%--DIV MAPA--%><img  id="mapaestatico" src=""/>
                                                </div>
                                              
      

                                            </td>
                                            <td colspan="2">
                                                
                                             <dx:ASPxGridView ID="dgvHydro" runat="server" Theme="Default" AutoGenerateColumns="False" KeyFieldName="" EnableTheming="True" Font-Size="Smaller" ClientInstanceName="dgvHydro" Width="100%" Font-Strikeout="False" Font-Underline="False" CssPostfix="DevGlp" Visible="true">
                                                                                                                                                         
                                                    <SettingsPager Mode="ShowAllRecords" NumericButtonCount="20" PageSize="20">
                                                    </SettingsPager>
                                                                                                                                                         
                                                    <Columns>
                                                        
                                                       
                                                        <dx:GridViewDataTextColumn Caption="DATOS GENERALES" FieldName="Name" ShowInCustomizationForm="True" VisibleIndex="2">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption=" " FieldName="ID" ShowInCustomizationForm="True" VisibleIndex="3">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption=" " FieldName="IDs" ShowInCustomizationForm="True" VisibleIndex="4" >
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption=" " FieldName="DATO" ShowInCustomizationForm="True" VisibleIndex="5" >
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption=" " FieldName="DATO1" ShowInCustomizationForm="True" VisibleIndex="6" >
                                                        </dx:GridViewDataTextColumn>
                                                                                                                                                                       
                                                        
                                                        
                                                    </Columns>
                                                     <Templates>
                                                         <EditForm>
                                                         <div id="map_canvas"  style="width: 100%; height: 400px"></div>
                                                             </EditForm>
                                                         </templates>
                                                    
                                                    <Styles CssPostfix="DevGlp">
                                                        <AlternatingRow BackColor="#EEEEEE">
                                                        </AlternatingRow>
                                                    </Styles>
                                                </dx:ASPxGridView>
                                                <dx:ASPxGridViewExporter ID="gridExport3" runat="server"></dx:ASPxGridViewExporter>
                                            </td>
                                        </tr>
                                        <tr>
                                        
                                            <td colspan="3">
                                                <dx:ASPxGridView ID="dgvEntidades" runat="server" Theme="Default" AutoGenerateColumns="False" KeyFieldName="ID_CONSUMIDOR" EnableTheming="True" Font-Size="Smaller" ClientInstanceName="dgvEntidades" Width="100%" Font-Strikeout="False" Font-Underline="False" CssPostfix="DevGlp" Visible="false">
                                                    <ClientSideEvents CustomButtonClick="function(s, e) {
    switch(e.buttonID) {
    case&nbsp; 'btnInfo':
    pucDatos.Show();
    pucDatos.PerformCallback();
    break;
    case&nbsp; 'btnEstado':
    pucModEstado.Show();
    pucModEstado.PerformCallback();
    break;
    } 
    }" />
                                                    <SettingsContextMenu EnableRowMenu="False">
                                                    </SettingsContextMenu>
                                                    <Templates>
                                                        <GroupFooterRow>
                                                            <dx:ASPxLabel ID="ASPxLabel38" runat="server" Text="ASPxLabel">
                                                            </dx:ASPxLabel>
                                                        </GroupFooterRow>
                                                        <EditForm >
                                                        </EditForm>
                                                    </Templates>
                                                    <SettingsPager NumericButtonCount="5">
                                                        <PageSizeItemSettings Items="10, 25, 100, 1000" Visible="True" ShowAllItem="True">
                                                        </PageSizeItemSettings>
                                                    </SettingsPager>
                                                    <SettingsEditing Mode="PopupEditForm">
                                                    </SettingsEditing>
                                                    <Settings ShowFilterRow="True" ShowHeaderFilterButton="True" />
                                                    <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" />
                                                    <SettingsPopup>
                                                        <EditForm Modal="True" HorizontalAlign="Center" VerticalAlign="Middle" VerticalOffset="0" />
                                                        <CustomizationWindow HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" />
                                                    </SettingsPopup>
                                                    <SettingsText CommandNew="Nuevo" />
                                                    <Columns>
                                                        <dx:GridViewCommandColumn ShowInCustomizationForm="True" VisibleIndex="21" ToolTip="Funciones">
                                                            <CustomButtons>
                                                                <dx:GridViewCommandColumnCustomButton ID="btnInfo" Text=" ">
                                                                    <Image AlternateText="Info" IconID="toolboxitems_pageinfo_16x16" ToolTip="Información Adicional">
                                                                    </Image>
                                                                </dx:GridViewCommandColumnCustomButton>

                                                                <dx:GridViewCommandColumnCustomButton ID="btnEstado" Text="Modif. Estado" Visibility="Invisible">
                                                                </dx:GridViewCommandColumnCustomButton>
                                                            </CustomButtons>
                                                            
                                                        </dx:GridViewCommandColumn>
                                                        <dx:GridViewDataTextColumn Caption="RAZON SOCIAL" FieldName="DENOMINACION" ShowInCustomizationForm="True" VisibleIndex="2">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="NIT" FieldName="NIT" ShowInCustomizationForm="True" VisibleIndex="3">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="LATITUD" FieldName="LATITUD" ShowInCustomizationForm="True" VisibleIndex="10" Visible="False">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="REP. LEGAL" FieldName="NOMBRES_REPRESENTANTE" ShowInCustomizationForm="True" VisibleIndex="7" ToolTip="Representante Legal">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="CI REP." FieldName="CI_REPRESENTANTE" ShowInCustomizationForm="True" VisibleIndex="6" Visible="True" ToolTip="Carnet de Identidad del Representante Legal">
                                                         <DataItemTemplate>
                                                             <a id="persona"                                                                 
                                                                data-target="#modalPersona" 
                                                                data-toggle="modal" 
                                                                data-id= '<%# Eval ("CI_REPRESENTANTE")%>'
                                                                href="#" 
                                                                title="Ver Datos Persona">
                                                                <i class="icon-eye-open"></i><%# Eval ("CI_REPRESENTANTE")%></a>
                                                         </DataItemTemplate>  
                                                             
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="LONGITUD" FieldName="LONGITUD" ShowInCustomizationForm="True" VisibleIndex="11" Visible="False">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="DEPARTAMENTO" FieldName="DEPARTAMENTO" ShowInCustomizationForm="True" VisibleIndex="0">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="ID" FieldName="ID_CONSUMIDOR" ShowInCustomizationForm="True" VisibleIndex="1" ToolTip="Identificador del Proyecto">
                                                            <Settings AutoFilterCondition="Contains" />
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="DIRECCION" FieldName="DIRECCION" ShowInCustomizationForm="True" VisibleIndex="4">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="TELEFONO" FieldName="TELEFONOS" ShowInCustomizationForm="True" VisibleIndex="5">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="LICENCIA" FieldName="NRO_LICENCIA" ShowInCustomizationForm="True" VisibleIndex="9">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="PROV. SG" FieldName="INF_PROV_SW" ShowInCustomizationForm="True" ToolTip="Proveedor de Sistema de Gestión" VisibleIndex="8">
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                     
                                                    <TotalSummary>
                                                        <dx:ASPxSummaryItem FieldName="F_IMPORTE" ShowInColumn="IMPORTE" SummaryType="Custom" Tag="imp" />
                                                        <dx:ASPxSummaryItem FieldName="VOLUMEN" ShowInColumn="CANTIDAD" SummaryType="Custom" Tag="vol" />
                                                        <dx:ASPxSummaryItem FieldName="STR_ESTADO" ShowInColumn="EST." SummaryType="Custom" Tag="est" />
                                                    </TotalSummary>
                                                    <Styles CssPostfix="DevGlp">
                                                        <AlternatingRow BackColor="#EEEEEE">
                                                        </AlternatingRow>
                                                    </Styles>
                                                </dx:ASPxGridView>
                                            </td>
                                        
                                        </tr>

                                       

                                        <tr>
                                            <td colspan="3" >
                                                 <fieldset>
     <div align="center" style="padding: 4px 0 0 0;">
         </div>
     <legend><h3>CONSUMO GE</h3></legend>
        <div align="center" style="padding: 4px 0 0 0">
            <dxcharts:WebChartControl ID="Grafico" runat="server" DataSourceID="pvgAcumulado"
                Width="580px" Height="200px" SeriesDataMember="Series" CssClass="TopMargin" CrosshairEnabled="True">
           
          
            <fillstyle>
                <OptionsSerializable>
                    <dxcharts:SolidFillOptions />
                </OptionsSerializable>
            </fillstyle>
           
<CrosshairOptions><CommonLabelPositionSerializable>
<dxcharts:CrosshairMousePosition></dxcharts:CrosshairMousePosition>
</CommonLabelPositionSerializable>
</CrosshairOptions>

<ToolTipOptions><ToolTipPositionSerializable>
<dxcharts:ToolTipMousePosition></dxcharts:ToolTipMousePosition>
</ToolTipPositionSerializable>
</ToolTipOptions>

            <Legend MaxHorizontalPercentage="30"></Legend>
           
          
                <DiagramSerializable>
<dxcharts:XYDiagram>
<AxisX VisibleInPanesSerializable="-1" title-text="."></AxisX>

<AxisY VisibleInPanesSerializable="-1" title-text="."></AxisY>
</dxcharts:XYDiagram>
</DiagramSerializable>

            <legend maxhorizontalpercentage="30" />
           
          
            <seriestemplate argumentdatamember="Arguments" valuedatamembersserializable="Values" argumentscaletype="Qualitative">
                <ViewSerializable>
                    <dxcharts:SideBySideBarSeriesView />
                </ViewSerializable>
                <LabelSerializable>
                    <dxcharts:SideBySideBarSeriesLabel>
                        <FillStyle>
                            <OptionsSerializable>
                                <dxcharts:SolidFillOptions />
                            </OptionsSerializable>
                        </FillStyle>
                    <PointOptionsSerializable>
                        <dxcharts:PointOptions />
                    </PointOptionsSerializable>
                </dxcharts:SideBySideBarSeriesLabel>
                </LabelSerializable>
<LegendPointOptionsSerializable>
<dxcharts:PointOptions></dxcharts:PointOptions>
</LegendPointOptionsSerializable>
            </seriestemplate>

        </dxcharts:WebChartControl>
      </div>
 </fieldset>
                                                 <div align="center" style="padding: 4px 0 0 0;">
                                                <dx:ASPxPivotGrid ID="pvgAcumulado" runat="server" ClientIDMode="AutoID" Width="100%"  EnableTheming="True" Font-Size="X-Small" Theme="Default">
                                                          <OptionsChartDataSource ProvideDataByColumns="False" FieldValuesProvideMode="Value" 
              DataProvideMode="UseCustomSettings"></OptionsChartDataSource>                                                                                                       
                                                    <Fields>
                                                        
                                                        <dx:PivotGridField ID="fielddecCantidad"  AreaIndex="0" Caption="." FieldName="decCantidad" Area="DataArea" CellFormat-FormatType="Numeric" Options-AllowDrag="False"  >
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="fieldstrGestion" AreaIndex="0" Caption="GESTION" FieldName="strGestion" Area="RowArea">
                                                        </dx:PivotGridField>
                                                       <%-- <dx:PivotGridField ID="fieldPROYECTO" Area="RowArea" AreaIndex="0" Caption="PROYECTO" FieldName="PROYECTO">
                                                        </dx:PivotGridField>--%>
                                                        <dx:PivotGridField ID="fieldstrMes" AreaIndex="1" Caption="." FieldName="strMes" Area="ColumnArea" >
                                                        </dx:PivotGridField>
                                                         
                                                    </Fields>
                                                    <Styles CssPostfix="DevGlp"></Styles>
                                                    <OptionsView ShowColumnGrandTotalHeader="False" ShowColumnGrandTotals="False" />
                                                    
                                                </dx:ASPxPivotGrid>
                                                 <dx:ASPxPivotGridExporter ID="gridExport" runat="server"></dx:ASPxPivotGridExporter>
                                                    </div>
                                                    
                                                   
                                                </td></tr>


                                         <tr><td colspan="3">

                                                     


                                        </td></tr>

                                        <tr>
                                            <td colspan="3" >
                                                 <fieldset>
     <div align="center" style="padding: 4px 0 0 0;">
         </div>
     <legend><h3>CONSUMO DO</h3></legend>
        <div align="center" style="padding: 4px 0 0 0">
            <dxcharts:WebChartControl ID="Grafico2" runat="server" DataSourceID="pvgAcumuladoDO"
                Width="580px" Height="200px" SeriesDataMember="Series" CssClass="TopMargin" CrosshairEnabled="True">
           
          
            <fillstyle>
                <OptionsSerializable>
                    <dxcharts:SolidFillOptions />
                </OptionsSerializable>
            </fillstyle>
           
<CrosshairOptions><CommonLabelPositionSerializable>
<dxcharts:CrosshairMousePosition></dxcharts:CrosshairMousePosition>
</CommonLabelPositionSerializable>
</CrosshairOptions>

<ToolTipOptions><ToolTipPositionSerializable>
<dxcharts:ToolTipMousePosition></dxcharts:ToolTipMousePosition>
</ToolTipPositionSerializable>
</ToolTipOptions>

            <Legend MaxHorizontalPercentage="30"></Legend>
           
          
                <DiagramSerializable>
<dxcharts:XYDiagram>
<AxisX VisibleInPanesSerializable="-1" title-text="."></AxisX>

<AxisY VisibleInPanesSerializable="-1" title-text="."></AxisY>
</dxcharts:XYDiagram>
</DiagramSerializable>

            <legend maxhorizontalpercentage="30" />
           
          
            <seriestemplate argumentdatamember="Arguments" valuedatamembersserializable="Values" argumentscaletype="Qualitative">
                <ViewSerializable>
                    <dxcharts:SideBySideBarSeriesView />
                </ViewSerializable>
                <LabelSerializable>
                    <dxcharts:SideBySideBarSeriesLabel>
                        <FillStyle>
                            <OptionsSerializable>
                                <dxcharts:SolidFillOptions />
                            </OptionsSerializable>
                        </FillStyle>
                    <PointOptionsSerializable>
                        <dxcharts:PointOptions />
                    </PointOptionsSerializable>
                </dxcharts:SideBySideBarSeriesLabel>
                </LabelSerializable>
<LegendPointOptionsSerializable>
<dxcharts:PointOptions></dxcharts:PointOptions>
</LegendPointOptionsSerializable>
            </seriestemplate>

        </dxcharts:WebChartControl>
      </div>
 </fieldset>
                                                <dx:ASPxPivotGrid ID="pvgAcumuladoDO" runat="server" ClientIDMode="AutoID" Width="100%" EnableTheming="True" Font-Size="X-Small" Theme="Default">
                                                     <OptionsChartDataSource ProvideDataByColumns="False" FieldValuesProvideMode="Value" 
              DataProvideMode="UseCustomSettings"></OptionsChartDataSource>   
                                                    <Fields>
                                                       
                                                        <dx:PivotGridField ID="PivotGridField1"  AreaIndex="0" Caption="." FieldName="decCantidad" Area="DataArea" CellFormat-FormatType="Numeric" Options-AllowDrag="False"  >
                                                        </dx:PivotGridField>
                                                        <dx:PivotGridField ID="PivotGridField2" AreaIndex="0" Caption="GESTION" FieldName="strGestion" Area="RowArea">
                                                        </dx:PivotGridField>
                                                   <%--     <dx:PivotGridField ID="PivotGridField3" Area="RowArea" AreaIndex="0" Caption="PROYECTO" FieldName="PROYECTO">
                                                        </dx:PivotGridField>--%>
                                                        <dx:PivotGridField ID="PivotGridField4" AreaIndex="1" Caption="." FieldName="strMes" Area="ColumnArea" >
                                                        </dx:PivotGridField>
                                                         
                                                    </Fields>
                                                    <Styles CssPostfix="DevGlp"></Styles>
                                                    <OptionsView ShowColumnGrandTotalHeader="False" ShowColumnGrandTotals="False" />
                                                    
                                                </dx:ASPxPivotGrid>
                                               
                                                </td></tr>

                                        <tr><td>
                                            
                                   <asp:Button ID="btnExport" runat="server" Text="Exportar a PDF" OnClick="btnExport_Click" /> 
                                </td>

                                           </tr>

  </table></div></td></tr>
                                    </table><table>
    <tr>
        <td align="center">
            <asp:Chart ID="Chart1" runat="server" Height="300px" Width="1000px" Visible="false">
                <Titles>
                    <asp:Title ShadowOffset="3" Name="Items" />
                    <asp:Title Name="TitleGE" Text="CONSUMO GE"></asp:Title>
                </Titles>
                <Legends>
                    <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="False" Name="Default"
                        LegendStyle="Row" />
                </Legends>
                <ChartAreas>
                    <asp:ChartArea Name="ChartArea1" BorderWidth="0" />
                </ChartAreas>               
            </asp:Chart>
             <asp:Chart ID="Chart2" runat="server" Height="300px" Width="1000px" Visible="false">
                <Titles>
                    <asp:Title ShadowOffset="3" Name="Items" />
                    <asp:Title Name="TitleDO" Text="CONSUMO DO"></asp:Title>
                </Titles>
                <Legends>
                    <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="False" Name="Default"
                        LegendStyle="Row" />
                </Legends>
                <ChartAreas>
                    <asp:ChartArea Name="ChartArea2" BorderWidth="0" />
                </ChartAreas>              
            </asp:Chart>
        </td>
    </tr>
    <tr>
        <td align="center">
            <asp:GridView ID="gvCustomers" runat="server">
                <PagerSettings Visible="False" />
            </asp:GridView>
        </td>
    </tr>
    
</table>
                                     

                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxCallbackPanel>

                        <%--<script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyA91qY_abkXqPC8vwE8zmErdiSvKvO7nOo&callback=initMap"></script>--%>
                        <%--<script type="text/javascript" src="https://maps.googleapis.com/maps/api/staticmap?&format=png&key=AIzaSyD3AkEndc2Z3UA3HycG0CPLySrLJnCoE9s&callback=initMa"></script>--%>
                        <script type="text/javascript">

                            window.onload = function () {
                                LoadMap();
                            };

                            function LoadMap() {
                                //var mapOptions = {
                                //    center: new google.maps.LatLng(-16.500, -64.150),
                                //    zoom: 5,
                                //    mapTypeId: google.maps.MapTypeId.ROADMAP
                                //};
                                //map = new google.maps.Map(document.getElementById("map_canvas"), mapOptions);
                                
                            };

                            function OnEndCallback(s, e) {

                                var alertText = s.cp_Alerta;                          
                                if (alertText != "") {                                    
                                    MensajeAdvertencia(alertText);
                                } else {
                                }

                                //var mapOptions = {
                                //    center: new google.maps.LatLng(-16.500, -64.150),
                                //    zoom: 5,
                                //    mapTypeId: google.maps.MapTypeId.ROADMAP
                                //};
                                //map = new google.maps.Map(document.getElementById("map_canvas"), mapOptions);

                               
                                var a = s.cp_latitud;
                                var b = s.cp_longitud;
                                var c = s.cp_descripcion;
                                var visible = s.cp_visible;
                                
                                if(visible=="ok")
                                {
                                    document.getElementById("reporte").style.display = "block"
                                }
                                else
                                {
                                    document.getElementById("reporte").style.display = "none"
                                }
                                /*var mapurl = `https://maps.googleapis.com/maps/api/staticmap?center=${a[0]},${b[0]}&zoom=17&size=500x290&markers=color:red%7Clabel:E%7C${a[0]},${b[0]}&key=AIzaSyD3AkEndc2Z3UA3HycG0CPLySrLJnCoE9s`
                                */
                                var mapurl = `https://maps.googleapis.com/maps/api/staticmap?center=${a[0]},${b[0]}&zoom=17&size=500x290&markers=color:red%7Clabel:E%7C${a[0]},${b[0]}&maptype=hybrid&key=AIzaSyD3AkEndc2Z3UA3HycG0CPLySrLJnCoE9s`
                                var map = document.getElementById('mapaestatico')
 
                                map.src = mapurl
                                //var intZoom = 6;
                                //if (a[0] == 0 && b[0] == 0) {
                                //    a[0] = -16.500;
                                //    b[0] = -64.150;
                                //    intZoom = 5;
                                //}

                                //var mapProp = {
                                //    center: new google.maps.LatLng(a[0], b[0]),
                                //    zoom: intZoom,
                                //    mapTypeId: google.maps.MapTypeId.ROADMAP
                                //};
                                //var map = new google.maps.Map(document.getElementById("map_canvas"), mapProp);
                                //var infowindow = new google.maps.InfoWindow();

                                //for (index = 1; index < a.length; ++index) {
                                //    if (a[index] != 0 && b[index] != 0) {
                                //        var marker = new google.maps.Marker({
                                //            position: new google.maps.LatLng(a[index], b[index]),
                                //            map: map,
                                //            icon: '../../ui/img/marcas/azul.png'
                                //        });
                                //        marker.setMap(map);

                                //        google.maps.event.addListener(marker, 'click', (function (marker, index) {
                                //            return function () {
                                //                infowindow.setContent(c[index]);
                                //                infowindow.open(map, marker);
                                //            }
                                //        })(marker, index));

                                //    }
                                //}
                                //color marca -> dentro de marker
                                //,icon: 'http://maps.google.com/mapfiles/ms/icons/yellow-dot.png'
                            }

                        </script>
                                            
                        <%--<div id="map_canvas"  style="width: 100%; height: 600px">
                        </div>--%>

                    </dx:SplitterContentControl>
                </ContentCollection>
            </dx:SplitterPane>
        </Panes>
    </dx:ASPxSplitter>
</div>

 


<dx:ASPxPopupControl ID="pucDatos" runat="server" AllowDragging="True" ClientInstanceName="pucDatos" EnableTheming="True" HeaderText="Información Adicional" Height="93px" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Above" Theme="Metropolis" Width="700px" OnWindowCallback="pucDatos_WindowCallback">
    <HeaderImage IconID="toolboxitems_pageinfo_32x32">
    </HeaderImage>
    <HeaderStyle>
    <BorderBottom BorderStyle="Solid" />
    </HeaderStyle>
    <ContentCollection>
        <dx:PopupControlContentControl runat="server">
            <table class="dxflInternalEditorTable_Youthful">
                <tr>
                    <td style="width: 25%; text-align: right; padding-bottom: 10px;">
                        <dx:ASPxLabel ID="ASPxLabel39" runat="server" Text="ID Consumidor :" Font-Bold="True" ForeColor="#333333">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 75%; padding-bottom: 10px;">
                        <dx:ASPxTextBox ID="txtPiId" runat="server" Width="170px" ReadOnly="True">
                            <ReadOnlyStyle BackColor="#FFE1C4">
                            </ReadOnlyStyle>
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 25%; text-align: right;">
                        <dx:ASPxLabel ID="ASPxLabel44" runat="server" Font-Bold="True" ForeColor="#333333" Text="Razón Social :">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 75%;">
                        <dx:ASPxTextBox ID="txtPiRazonSocial" runat="server" ReadOnly="True" Width="100%">
                            <ReadOnlyStyle BackColor="#FFE1C4">
                            </ReadOnlyStyle>
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 25%; text-align: right;">
                        <dx:ASPxLabel ID="ASPxLabel45" runat="server" Font-Bold="True" ForeColor="#333333" Text="Productos :">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 75%;">
                        <dx:ASPxTextBox ID="txtPiProducto" runat="server" ReadOnly="True" Width="100%" Text="ND">
                            <ReadOnlyStyle BackColor="#FFE1C4">
                            </ReadOnlyStyle>
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 25%; text-align: right;">
                        <dx:ASPxLabel ID="ASPxLabel46" runat="server" Font-Bold="True" ForeColor="#333333" Text="Licencia :">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 75%;">
                        <dx:ASPxTextBox ID="txtPiLicencia" runat="server" ReadOnly="True" Text="ND" Width="170px">
                            <ReadOnlyStyle BackColor="#FFE1C4">
                            </ReadOnlyStyle>
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 25%; text-align: right;">
                        <dx:ASPxLabel ID="ASPxLabel47" runat="server" Font-Bold="True" ForeColor="#333333" Text="Vigencia :">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 75%;">
                        <dx:ASPxTextBox ID="txtPiVigencia" runat="server" ReadOnly="True" Text="ND" Width="170px">
                            <ReadOnlyStyle BackColor="#FFE1C4">
                            </ReadOnlyStyle>
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 25%; text-align: right; padding-bottom: 10px;">
                        <dx:ASPxLabel ID="lblLlave" runat="server" ClientInstanceName="lblLlave" Font-Bold="True" ForeColor="#333333" Text="Llave Bsisa :" ClientVisible="False">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 75%; padding-bottom: 10px;">
                        <dx:ASPxTextBox ID="txtPiLlave" runat="server" ReadOnly="True" Text="ND" Width="170px" ClientVisible="False">
                            <ReadOnlyStyle BackColor="#FFE1C4">
                            </ReadOnlyStyle>
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 25%; padding-bottom: 5px;">
                        <dx:ASPxLabel ID="lblLlave2" runat="server" ClientInstanceName="lblLlave" Font-Bold="True" Font-Names="Verdana" Font-Size="Smaller" Font-Underline="True" ForeColor="#666666" Text="COORDENADAS">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 75%; padding-bottom: 5px;">&nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 25%; text-align: right;">
                        <dx:ASPxLabel ID="lblLlave0" runat="server" ClientInstanceName="lblLlave" Font-Bold="True" ForeColor="#333333" Text="Latitud :">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 75%;">
                        <dx:ASPxTextBox ID="txtPiLatitud" runat="server" ReadOnly="True" Text="ND" Width="170px">
                            <ReadOnlyStyle BackColor="#FFE1C4">
                            </ReadOnlyStyle>
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 25%; text-align: right; padding-bottom: 5px;">
                        <dx:ASPxLabel ID="lblLlave1" runat="server" ClientInstanceName="lblLlave" Font-Bold="True" ForeColor="#333333" Text="Longitud :">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 75%; padding-bottom: 5px;">
                        <dx:ASPxTextBox ID="txtPiLongitud" runat="server" ReadOnly="True" Text="ND" Width="170px">
                            <ReadOnlyStyle BackColor="#FFE1C4">
                            </ReadOnlyStyle>
                        </dx:ASPxTextBox>
                    </td>
                </tr>
            </table>
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>

<dx:ASPxPopupControl ID="pucModEstado" runat="server" AllowDragging="True" ClientInstanceName="pucModEstado" EnableTheming="True" HeaderText="Modificar Estado" Height="93px" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Theme="Metropolis" Width="400px" OnWindowCallback="pucModEstado_WindowCallback">
    <ContentCollection>
        <dx:PopupControlContentControl runat="server">
            <table class="dxflInternalEditorTable_Youthful">
                <tr>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel40" runat="server" Text="ID Consumidor :">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="txtPeId" runat="server" Width="170px">
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">
                        </td>
                    <td class="auto-style1"></td>
                </tr>
                <tr>
                    <td class="auto-style1">
                        <dx:ASPxLabel ID="ASPxLabel43" runat="server" Text="Fecha :">
                        </dx:ASPxLabel>
                    </td>
                    <td class="auto-style1">
                        <dx:ASPxDateEdit ID="detPeFecha" runat="server" ClientInstanceName="detFechaIni" EnableTheming="True" Height="23px" Theme="Metropolis">
                            <TimeSectionProperties>
                                <TimeEditProperties>
                                    <ClearButton Visibility="Auto">
                                    </ClearButton>
                                </TimeEditProperties>
                            </TimeSectionProperties>
                            <ClearButton Visibility="Auto">
                            </ClearButton>
                        </dx:ASPxDateEdit>
                        <br />
                        .</td>
                </tr>
                <tr>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel41" runat="server" Text="Estado :">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxComboBox ID="cmbPeEstado" runat="server" EnableTheming="True" Theme="Metropolis" ValueType="System.Int32">
                            <Items>
                                <dx:ListEditItem Text="ACTIVA" Value="1" />
                                <dx:ListEditItem Text="CERRADA" Value="2" />
                                <dx:ListEditItem Text="INTERVENIDA" Value="3" />
                            </Items>
                            <ClearButton Visibility="Auto">
                            </ClearButton>
                        </dx:ASPxComboBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel42" runat="server" Text="Observación :">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="txtPeObservacion" runat="server" Width="250px">
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2">
                        <dx:ASPxButton ID="btnSubirLlave0" runat="server" EnableTheming="True" Text="Modificar Estado" Theme="Metropolis" Width="100%">
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>