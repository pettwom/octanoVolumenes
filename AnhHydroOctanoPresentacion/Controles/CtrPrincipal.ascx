<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtrPrincipal.ascx.cs" Inherits="AnhHydroOctanoPresentacion.Controles.CtrPrincipal" %>

<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>

<%@ Register Src="CtrPersonaSegip.ascx" TagPrefix="uc1" TagName="CtrPersonaSegip" %>

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
    <table style="width: 100%">
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
    </table> 
    <dx:ASPxSplitter ID="ASPxSplitter1" runat="server" Height="100%">
        <Panes>
            <dx:SplitterPane Size="25%" AllowResize="True" AutoHeight="True">
                <ContentCollection>
                    <dx:SplitterContentControl runat="server">
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
}" Init="function(s, e) {
pnlDatos.PerformCallback();
}" EndCallback="function(s, e) {
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
                                    <table style="width: 100%;">
                                        <tr>
                                            <td colspan="7" style="text-align:center">
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
                                                <legend>
                                                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Rockwell" Font-Size="Medium" ForeColor="#367B2F" Text="INFORMACIÓN GENERAL"></asp:Label>
                                                </legend>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="#10420F" Text="ACTIVIDAD"></asp:Label>
                                            </td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td colspan="2">&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <asp:Label ID="lblActividad" runat="server"></asp:Label>
                                            </td>
                                            <td colspan="2">
                                                &nbsp;</td>
                                            <td class="auto-style1">&nbsp;</td>
                                        </tr>
                                        <tr>
                                        
                                            <td colspan="7">
                                                <dx:ASPxGridView ID="dgvEntidades" runat="server" Theme="Default" AutoGenerateColumns="False" KeyFieldName="ID_CONSUMIDOR" EnableTheming="True" Font-Size="Smaller" ClientInstanceName="dgvEntidades" Width="100%" Font-Strikeout="False" Font-Underline="False" CssPostfix="DevGlp">
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
                                            <td class="auto-style11">
                                                &nbsp;</td>
                                            <td>
                                            </td>
                                            <td></td>
                                            <td></td>
                                            <td>
                                                </td>
                                            <td>&nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                    </table>
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxCallbackPanel>

                        <%--<script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyA91qY_abkXqPC8vwE8zmErdiSvKvO7nOo&callback=initMap"></script>--%>
                        <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyA91qY_abkXqPC8vwE8zmErdiSvKvO7nOo&callback=initMap" async defer></script>
                        <script type="text/javascript">
                            // Define initMap ANTES de cargar la API
                            window.initMap = function () {
                                // Tu código de inicialización del mapa
                                console.log("Mapa inicializado");
                            };
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
                                try {
                                    var alertText = s.cp_Alerta;
                                    if (alertText && alertText != "") {
                                        if (typeof MensajeAdvertencia !== 'undefined') {
                                            MensajeAdvertencia(alertText);
                                        } else {
                                            alert(alertText);
                                        }
                                    }

                                    // VALIDAR QUE LOS DATOS EXISTAN ANTES DE USARLOS
                                    var a = s.cp_latitud;
                                    var b = s.cp_longitud;
                                    var c = s.cp_descripcion;

                                    // Verificar que a, b, c existan y sean arrays
                                    if (!a || !b || !c) return;
                                    if (!Array.isArray(a) || !Array.isArray(b) || !Array.isArray(c)) return;
                                    if (a.length === 0 || b.length === 0) return;

                                    var intZoom = 6;

                                    // Verificar que a[0] existe antes de usarlo
                                    if (a[0] == 0 && b[0] == 0) {
                                        a[0] = -16.500;
                                        b[0] = -64.150;
                                        intZoom = 5;
                                    }

                                    // Verificar que Google Maps está cargado
                                    if (typeof google === 'undefined' || !google.maps) return;

                                    var mapProp = {
                                        center: new google.maps.LatLng(a[0], b[0]),
                                        zoom: intZoom,
                                        mapTypeId: google.maps.MapTypeId.ROADMAP
                                    };

                                    var mapDiv = document.getElementById("map_canvas");
                                    if (!mapDiv) return;

                                    var map = new google.maps.Map(mapDiv, mapProp);
                                    var infowindow = new google.maps.InfoWindow();

                                    for (var index = 1; index < a.length; ++index) {
                                        // Validar que los valores existen y no son cero
                                        if (a[index] && b[index] && c[index] && a[index] != 0 && b[index] != 0) {
                                            var marker = new google.maps.Marker({
                                                position: new google.maps.LatLng(a[index], b[index]),
                                                map: map,
                                                icon: '../../ui/img/marcas/azul.png'
                                            });

                                            google.maps.event.addListener(marker, 'click', (function (marker, idx) {
                                                return function () {
                                                    if (c[idx]) {
                                                        infowindow.setContent(c[idx]);
                                                        infowindow.open(map, marker);
                                                    }
                                                }
                                            })(marker, index));
                                        }
                                    }
                                } catch (err) {
                                    // Silenciar errores para no romper la interfaz
                                    console.log("Error en mapa: " + err.message);
                                }
                            }

                        </script>
                                            
                        <div id="map_canvas"  style="width: 100%; height: 600px">
                        </div>

                    </dx:SplitterContentControl>
                </ContentCollection>
            </dx:SplitterPane>
        </Panes>
    </dx:ASPxSplitter>
</div>

<dx:ASPxPopupControl ID="pucInformativo" runat="server" AllowDragging="True" ClientInstanceName="pucInformativo" EnableTheming="True" HeaderText=" PARA LA ACTUALIZACION DE SU SISTEMA DE FACTURACION CONTACTESE CON EL PROVEEDOR DEL MISMO" Height="850px" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Above" Theme="Metropolis" Width="850px" >
    <HeaderImage IconID="toolboxitems_pageinfo_32x32">
    </HeaderImage>
    <HeaderStyle>
    <BorderBottom BorderStyle="Solid" />
    </HeaderStyle>
    <ContentCollection>
        <dx:PopupControlContentControl runat="server">
            <table class="dxflInternalEditorTable_Youthful">                            
                <tr>
                    <td>
                        <dx:ASPxPageControl ID="pctPanel" runat="server" ActiveTabIndex="1" Font-Names="Verdana" Font-Size="Smaller" Theme="Metropolis" Width="100%">
                            <TabPages>
                                <dx:TabPage Text="ANH-08448-DDT-0131-2025">
                                    <ContentCollection>
                                        <dx:ContentControl ID="ContentControl1" runat="server">
                                            Si no puede visualizar el archivo descargue <a target="_blank" href="http://get.adobe.com/reader/">Adobe PDF Reader</a>.
                                            <object data="../Manuales/Instructivo14032025.pdf" type="application/pdf" width="850px" height="850px" internalinstanceid="88">                                                   
                                            </object>
                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:TabPage>
                              <%--  <dx:TabPage Text="INSTRUCTIVO">
                                    <ContentCollection>
                                        <dx:ContentControl ID="ContentControl2" runat="server">
                                            Si no puede visualizar el archivo descargue <a target="_blank" href="http://get.adobe.com/reader/">Adobe PDF Reader</a>.
                                            <object data="../Manuales/recordatorio.pdf" type="application/pdf" width="850px" height="850px" internalinstanceid="88">                                                  
                                            </object>
                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:TabPage>--%>
                               <%--  <dx:TabPage Text="TAB 3">
                                    <ContentCollection>
                                        <dx:ContentControl ID="ContentControl3" runat="server">
                                            <object data="../Manuales/CIRCULAR 31_2024.pdf" type="application/pdf" width="850px" height="850px">
                                                Tu navegador no soporta visualizar PDFs.       
                                            </object>
                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:TabPage>--%>
                            </TabPages>
                        </dx:ASPxPageControl>   
                    </td>
                   <%-- <td >Si no puede visualizar el manual de Usuario descargue <a target="_blank" href="http://get.adobe.com/reader/">Adobe PDF Reader</a>.

                        <object data="../Manuales/ANH-03641-DGRCR-0014-2024.pdf" type="application/pdf" width="850px" height="850px" internalinstanceid="88">
                        </object>
                    </td>   --%>           
                </tr>
            </table>
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>

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

<%-- POPUP NOTIFICACIONES --%>
<dx:ASPxPopupControl 
    ID="pucNotificaciones" 
    runat="server"
    CloseAction="CloseButton" 
    CloseOnEscape="True" 
    Modal="True"
    PopupHorizontalAlign="WindowCenter" 
    PopupVerticalAlign="WindowCenter"
    ClientInstanceName="pucNotificaciones"
    HeaderText="Alerta de Seguimiento de las DJ Saldo Diario"
    AllowDragging="True" 
    PopupAnimationType="None"
    EnableViewState="False" 
    AllowResize="True" 
    EnableTheming="True"
    Width="780px"
    Height="600px"
    ScrollBars="Auto" 
    ShowMaximizeButton="True" 
    Theme="Metropolis">
    <ClientSideEvents EndCallback="function(s, e) {debugger;
  if (s.cp_ShowAlert == 'true') {
            var mensaje = s.cp_AlertMessage || '';
            var tipo = s.cp_AlertType || 'info';
            
            switch(tipo) {
                case 'success':
                    alert('✓ ' + mensaje);
                    break;
                case 'warning':
                    alert('⚠ ' + mensaje);
                    break;
                case 'error':
                    alert('✗ ' + mensaje);
                    break;
                default:
                    alert('ℹ ' + mensaje);
                    break;
            }
            
            // Limpiar las propiedades para no mostrar el mismo mensaje varias veces
            s.cp_ShowAlert = '';
            s.cp_AlertMessage = '';
            s.cp_AlertType = '';
        }	
}" />
    <HeaderStyle>
        <BorderBottom BorderStyle="Solid" />
    </HeaderStyle>
    <HeaderImage IconID="office_bell_32x32" />
    <ContentCollection>
        <dx:PopupControlContentControl runat="server">
            <dx:ASPxCallbackPanel 
                ID="pnlNotificaciones" 
                runat="server"
                ClientInstanceName="pnlNotificaciones"
                OnCallback="pnlNotificaciones_Callback">            
                <PanelCollection>
                    <dx:PanelContent runat="server">
                        <table style="width:100%;">
                            <tr>
                                <td style="width:12%;">
                                    <dx:ASPxComboBox 
                                        ID="cmbDepartamento" 
                                        runat="server"
                                        ClientInstanceName="cmbDepartamento"
                                        SelectedIndex="0"
                                        Theme="Metropolis"
                                        Width="100%"
                                        Height="27px"
                                        ToolTip="Filtrar por Departamento">
                                        <Items>
                                            <dx:ListEditItem Text="TODOS" Value="0" Selected="True" />
                                            <dx:ListEditItem Text="CHUQUISACA" Value="CHUQUISACA" />
                                            <dx:ListEditItem Text="LA PAZ" Value="LA PAZ" />
                                            <dx:ListEditItem Text="COCHABAMBA" Value="COCHABAMBA" />
                                            <dx:ListEditItem Text="ORURO" Value="ORURO" />
                                            <dx:ListEditItem Text="POTOSI" Value="POTOSI" />
                                            <dx:ListEditItem Text="TARIJA" Value="TARIJA" />
                                            <dx:ListEditItem Text="SANTA CRUZ" Value="SANTA CRUZ" />
                                            <dx:ListEditItem Text="BENI" Value="BENI" />
                                            <dx:ListEditItem Text="PANDO" Value="PANDO" />                                              
                                        </Items>

<ClearButton Visibility="Auto"></ClearButton>
                                    </dx:ASPxComboBox>
                                </td>
                                <td style="width:12%;">
                                    <dx:ASPxComboBox 
                                        ID="cmbEstadoNotif" 
                                        runat="server"
                                        ClientInstanceName="cmbEstadoNotif"
                                        SelectedIndex="0"
                                        Theme="Metropolis"
                                        Width="100%"
                                        Height="27px"
                                        ToolTip="Filtrar por Estado">
                                        <Items>
                                            <dx:ListEditItem Text="TODOS" Value="0" Selected="True" />
                                            <dx:ListEditItem Text="PLAZO VENCIDO" Value="PLAZO VENCIDO" />
                                            <dx:ListEditItem Text="EN PLAZO" Value="EN_PLAZO" />
                                            <dx:ListEditItem Text="DECLARADO" Value="DECLARADO" />
                                        </Items>

<ClearButton Visibility="Auto"></ClearButton>
                                    </dx:ASPxComboBox>
                                </td>
                                <td style="width:12%;">
                                     <dx:ASPxDateEdit 
                                        ID="detFechaIni" 
                                        runat="server"
                                        ClientInstanceName="detFechaIni"
                                        Theme="Metropolis"
                                        Width="100%"
                                        Height="27px"
                                        DisplayFormatString="dd/MM/yyyy"
                                        EditFormat="DateTime"
                                        ToolTip="Fecha Inicio">
                                        <CaptionCellStyle>
                                            <Paddings Padding="0px" />
                                        </CaptionCellStyle>
                                        <ClearButton Visibility="Auto">
                                        </ClearButton>
<TimeSectionProperties>
<TimeEditProperties>
<ClearButton Visibility="Auto"></ClearButton>
</TimeEditProperties>
</TimeSectionProperties>

                                        <ClientSideEvents Init="function(s, e) {
                                            s.SetText('dd/mm/aaaa');
                                        }" />
                                    </dx:ASPxDateEdit>
                                </td>
                                <td style="width:12%;">
                                   <dx:ASPxDateEdit 
                                        ID="detFechaFin" 
                                        runat="server"
                                        ClientInstanceName="detFechaFin"
                                        Theme="Metropolis"
                                        Width="100%"
                                        Height="27px"
                                        DisplayFormatString="dd/MM/yyyy"
                                        EditFormat="DateTime"
                                        ToolTip="Fecha Fin">
                                        <ClearButton Visibility="Auto">
                                        </ClearButton>
<TimeSectionProperties>
<TimeEditProperties>
<ClearButton Visibility="Auto"></ClearButton>
</TimeEditProperties>
</TimeSectionProperties>

                                        <ClientSideEvents Init="function(s, e) {
                                            s.SetText('dd/mm/aaaa');
                                        }" />
                                    </dx:ASPxDateEdit>
                                </td>
                                <td style="width:15%;">
                                    <dx:ASPxButton ID="btnFiltrarNotif" 
                                        runat="server"
                                        Text="Filtrar"
                                        Font-Bold="True"
                                        ForeColor="Maroon"
                                        Theme="Metropolis"
                                        Width="100%"
                                        AutoPostBack="False">
                                        <Image IconID="find_find_16x16" />                                   
                                        <ClientSideEvents Click="function(s, e) {
                                            var fechaIni = detFechaIni.GetValue();
                                            var fechaFin = detFechaFin.GetValue();
                                            var hoy = new Date();
                                            hoy.setHours(0, 0, 0, 0);    
                                            // Validar que fecha fin no sea mayor a hoy
                                            if (fechaFin && fechaFin > hoy) {
                                                alert('La fecha fin no puede ser mayor al día actual');
                                                return;
                                            }
    
                                            // Validar que fecha inicio no sea mayor a fecha fin
                                            if (fechaIni && fechaFin && fechaIni > fechaFin) {
                                                alert('La fecha inicio no puede ser mayor a la fecha fin');
                                                return;
                                            }
    
                                            pnlNotificaciones.PerformCallback(
                                                cmbDepartamento.GetValue() + '|' + 
                                                cmbEstadoNotif.GetValue() + '|' +
                                                (fechaIni ? fechaIni.toLocaleDateString() : '') + '|' +
                                                (fechaFin ? fechaFin.toLocaleDateString() : '')
                                            );
                                            e.processOnServer = false;
                                        }" />
                                    </dx:ASPxButton>
                                </td>
                                <td style="width:15%;">
                                    <dx:ASPxButton 
                                        ID="btnExportarNotif" 
                                        runat="server"
                                        Text="Exportar Excel"
                                        Font-Bold="True"
                                        ForeColor="Maroon"
                                        Theme="Metropolis"
                                        Width="100%"
                                        OnClick="btnExportarNotif_Click">
                                        <Image IconID="export_exporttoxls_16x16" />
                                    </dx:ASPxButton>
                                </td>
                                <td style="width:22%; text-align:right;">
                                    <dx:ASPxButton 
                                        ID="btnMarcarLeidas" 
                                        runat="server"
                                        Text="Marcar todas leídas"
                                        Font-Bold="True"
                                        ForeColor="#2d7a2d"
                                        Theme="Metropolis"
                                        AutoPostBack="False">
                                        <Image IconID="actions_apply_16x16" />
                                        <ClientSideEvents Click="function(s, e) {
                                            pnlNotificaciones.PerformCallback('MARCAR_TODAS');
                                            e.processOnServer = false;
                                        }" />
                                    </dx:ASPxButton>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="7" style="padding:4px 0;">
                                    <dx:ASPxLabel 
                                        ID="lblResumenNotif" 
                                        runat="server"
                                        ClientInstanceName="lblResumenNotif"
                                        Font-Bold="True"
                                        ForeColor="#FF8800"
                                        Font-Names="Verdana"
                                        Font-Size="Smaller"
                                        Width="100%"
                                        Text="Cargando notificaciones...">
                                        <BorderTop BorderStyle="Solid" />
                                    </dx:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="7">
                                    <dx:ASPxGridView
                                        ID="dgvNotificaciones"
                                        runat="server"
                                        AutoGenerateColumns="False"
                                        ClientInstanceName="dgvNotificaciones"
                                        EnableTheming="True"
                                        Font-Size="Smaller"
                                        KeyFieldName="Id"
                                        OnHtmlRowPrepared="dgvNotificaciones_HtmlRowPrepared"
                                        OnCustomButtonInitialize="dgvNotificaciones_CustomButtonInitialize"
                                        Theme="Default"
                                        Width="100%">
                                        <ClientSideEvents CustomButtonClick="function(s, e) {
                                            switch(e.buttonID) {
                                                case 'btnVerDetalle':
                                                    pnlDetalleNotif.PerformCallback(s.GetRowKey(e.visibleIndex));
                                                    pnlDetalleNotif.SetVisible(true);
                                                    break;
                                                case 'btnMarcarLeida':
                                                    pnlNotificaciones.PerformCallback('LEIDA|' + s.GetRowKey(e.visibleIndex));
                                                    break;
                                            }
                                        }"   />
                                        <Columns>
                                            <dx:GridViewCommandColumn VisibleIndex="1" Width="70px">
                                                <CustomButtons>
                                                    <dx:GridViewCommandColumnCustomButton ID="btnVerDetalle" Text=" ">
                                                        <Image IconID="actions_productcomparisons_16x16devav" ToolTip="Ver Detalle" />
                                                    </dx:GridViewCommandColumnCustomButton>
                                                    <dx:GridViewCommandColumnCustomButton ID="btnMarcarLeida" Text=" ">
                                                        <Image IconID="actions_apply_16x16" ToolTip="Marcar como leída" />
                                                    </dx:GridViewCommandColumnCustomButton>
                                                </CustomButtons>
                                            </dx:GridViewCommandColumn>

                                            <dx:GridViewDataTextColumn Caption="ID" FieldName="ID" Visible="False" />
                                            <dx:GridViewDataTextColumn Caption="ESTADO" FieldName="ESTADO" SortOrder="Ascending" VisibleIndex="8">
                                                <CellStyle Font-Bold="True" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="DEPARTAMENTO" FieldName="DEPARTAMENTO" VisibleIndex="2" />
                                            <dx:GridViewDataTextColumn Caption="RAZON SOCIAL" FieldName="RAZON_SOCIAL" VisibleIndex="3" />
                                            <dx:GridViewDataTextColumn Caption="NIT" FieldName="NIT" VisibleIndex="4" />
                                            <dx:GridViewDataTextColumn Caption="PERIODO" FieldName="PERIODO" VisibleIndex="5" Visible="false" />
                                            <dx:GridViewDataTextColumn Caption="FECHA" FieldName="FECHA_NOTIFICACION" VisibleIndex="6" />
                                            <dx:GridViewDataTextColumn Caption="DIAS VENCIDO" FieldName="DIAS_VENCIDO" VisibleIndex="7">
                                                <CellStyle Font-Bold="True" ForeColor="Red" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="LEIDA" FieldName="LEIDA" Visible="False" VisibleIndex="0" />
                                        </Columns>

                                        <Settings ShowFilterRow="True" />
                                        <SettingsBehavior AllowFocusedRow="True" />
                                        <SettingsPager NumericButtonCount="5">
                                            <PageSizeItemSettings Items="10, 50, 100" Visible="True" ShowAllItem="True" />
                                        </SettingsPager>
                                    </dx:ASPxGridView>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="7">
                                    <dx:ASPxCallbackPanel 
    ID="pnlDetalleNotif" 
    runat="server"
    Width="100%"
    ClientInstanceName="pnlDetalleNotif"
    OnCallback="pnlDetalleNotif_Callback">
    <ClientSideEvents Init="function(s, e) { s.SetVisible(false); }" />
    <PanelCollection>
        <dx:PanelContent runat="server">
            <table style="width:100%;">
                <tr>
                    <td>
                        <dx:ASPxLabel ID="lblDetalleNotif" runat="server"
                            ClientInstanceName="lblDetalleNotif"
                            Font-Bold="True" ForeColor="#FF8800"
                            Font-Names="Verdana" Font-Size="Smaller"
                            Width="100%" Text="DETALLE DE LA ESTACION">
                            <BorderTop BorderStyle="Solid" />
                        </dx:ASPxLabel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <dx:ASPxGridView ID="dgvDetalleEstacion" runat="server"
                            AutoGenerateColumns="False"
                            ClientInstanceName="dgvDetalleEstacion"
                            EnableTheming="True" Font-Size="Smaller"
                            KeyFieldName="CAMPO" Theme="Default" Width="100%">
                            <Columns>
                                <dx:GridViewDataTextColumn Caption="CAMPO" FieldName="CAMPO" VisibleIndex="0">
                                    <CellStyle Font-Bold="True" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="VALOR" FieldName="VALOR" VisibleIndex="1" />
                            </Columns>
                        </dx:ASPxGridView>
                    </td>
                </tr>
                <tr>
                    <td style="text-align:right; padding-top:6px;">
                        <dx:ASPxButton ID="btnReenviarNotif" runat="server"
                            Text="Reenviar Notificación" Font-Bold="True" Visible="false"
                            ForeColor="Red" Theme="Metropolis"
                            OnClick="btnReenviarNotif_Click">
                            <Image IconID="mail_reply_16x16" />
                        </dx:ASPxButton>
                        &nbsp;
                        <dx:ASPxButton ID="btnRegistrarMulta" runat="server"
                            Text="Registrar Multa" Font-Bold="True" Visible="false"
                            ForeColor="Maroon" Theme="Metropolis"
                            OnClick="btnRegistrarMulta_Click">
                            <Image IconID="actions_addto_16x16" />
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
                                <td colspan="7">
                                    <dx:ASPxGridViewExporter 
                                        ID="dgeNotificaciones" 
                                        runat="server"
                                        GridViewID="dgvNotificaciones">
                                    </dx:ASPxGridViewExporter>
                                </td>
                            </tr>
                        </table>
                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxCallbackPanel>
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>

	<!-- PopUp FormularioPersona -->
<uc1:CtrPersonaSegip runat="server" ID="CtrPersonaSegip"/>


