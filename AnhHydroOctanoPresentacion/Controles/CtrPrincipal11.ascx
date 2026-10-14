<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtrPrincipal11.ascx.cs" Inherits="AnhHydroOctanoPresentacion.Controles.CtrPrincipal11" %>

<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>



<%@ Register assembly="DevExpress.XtraReports.v15.1.Web, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraReports.Web" tagprefix="dx" %>



<%@ Register assembly="DevExpress.Web.ASPxScheduler.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxScheduler" tagprefix="dxwschs" %>


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
    
    .auto-style13 {
        height: 27px;
    }
    
    .auto-style14 {
        height: 18px;
        width: 10px;
    }
    .auto-style15 {
        width: 10px;
    }
    .auto-style16 {
        height: 26px;
        width: 10px;
    }
    
    </style>



<dx:ASPxSplitter ID="ASPxSplitter1" runat="server" Height="700px">
    <Panes>
        <dx:SplitterPane Size="11%" ScrollBars="Auto">
            <ContentCollection>
                <dx:SplitterContentControl runat="server">
                    <table style="width: 100%;">

                        <tr>
                            <td>
                                <dx:ASPxTreeList ID="tlsOperadores" runat="server" AutoGenerateColumns="False" ClientInstanceName="tlsOperadores" Font-Size="Smaller" Height="187px" KeyFieldName="intId" ParentFieldName="intIdPadre" Theme="Youthful">
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
                                    <SettingsBehavior AllowFocusedNode="True" AutoExpandAllNodes="True" />
                                    <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
                                    <ClientSideEvents FocusedNodeChanged="function(s, e) {
pnlDatos.PerformCallback();

alert(txtPrueba.GetText());
//SetMarker(txtPrueba.GetText());
//__doPostBack(&quot;pnlPrueba.PerformCallback()&quot;, &quot;&quot;); 
}" />
                                </dx:ASPxTreeList>
                            </td>
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
                    <table>
                        <tr>
                            <td>
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
                                        <td class="auto-style1" ></td>
                                        <td class="auto-style4" >
                                            <asp:Label ID="Label1" runat="server" Font-Names="Calibri" Font-Size="Large" ForeColor="#25452B" Text="Entidade Asignadas" Font-Bold="True"></asp:Label>
                                        </td>
                                        <td class="auto-style1" ></td>
                                        <td>
                                            
                                            &nbsp;</td>
                                        <td class="auto-style6" >
                                            <dx:ASPxTextBox ID="txtPrueba" runat="server" ClientInstanceName="txtPrueba" Width="170px">
                                            </dx:ASPxTextBox>
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
                                        
                                        <td class="auto-style14" ></td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style1">&nbsp;</td>
                                        <td class="auto-style4">
                                            <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="#10420F" Text="ACTIVIDAD"></asp:Label>
                                        </td>
                                        <td class="auto-style1">&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td class="auto-style6">&nbsp;</td>
                                        <td colspan="2">&nbsp;</td>
                                        <td class="auto-style1">&nbsp;</td>
                                        <td class="auto-style14">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style1">&nbsp;</td>
                                        <td colspan="4">
                                            <asp:Label ID="lblActividad" runat="server"></asp:Label>
                                        </td>
                                        <td colspan="2">&nbsp;</td>
                                        <td class="auto-style1">&nbsp;</td>
                                        <td class="auto-style14">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        
                                        <td colspan="7">
                                            <dx:ASPxGridView ID="dgvEntidades" runat="server" Theme="Office2003Olive" AutoGenerateColumns="False" KeyFieldName="ID_CONSUMIDOR" EnableTheming="True" Font-Size="Smaller" ClientInstanceName="dgvEntidades" OnHtmlRowPrepared="dgvEntidades_HtmlRowPrepared" Width="100%">
                                                <ClientSideEvents CustomButtonClick="function(s, e) {
	

    //pucDatos.PerformCallback(keyValue);
switch(e.buttonID) {
&nbsp;&nbsp;&nbsp; case&nbsp; 'btnInfo':
pucDatos.Show();
pucDatos.PerformCallback();
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; break;
&nbsp;&nbsp;&nbsp; case&nbsp; 'btnEstado':
pucModEstado.Show();
pucModEstado.PerformCallback();

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; break;
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
                                                <SettingsPager NumericButtonCount="100" PageSize="100">
                                                    <PageSizeItemSettings Items="50, 100, 200, 500, 1000" Visible="True">
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
                                                    <dx:GridViewCommandColumn ShowInCustomizationForm="True" VisibleIndex="21">
                                                        <CustomButtons>
                                                            <dx:GridViewCommandColumnCustomButton ID="btnInfo" Text="Info.">
                                                            </dx:GridViewCommandColumnCustomButton>
                                                            <dx:GridViewCommandColumnCustomButton ID="btnEstado" Text="Modif. Estado">
                                                            </dx:GridViewCommandColumnCustomButton>
                                                        </CustomButtons>
                                                        <CellStyle ForeColor="#006600">
                                                        </CellStyle>
                                                    </dx:GridViewCommandColumn>
                                                    <dx:GridViewDataTextColumn Caption="RAZON SOCIAL" FieldName="DENOMINACION" ShowInCustomizationForm="True" VisibleIndex="5" ToolTip="Tipo de Crédito Fiscal">
                                                        <Settings AutoFilterCondition="Contains" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="NIT" FieldName="NIT" ShowInCustomizationForm="True" VisibleIndex="6">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="PRODUCTOS" FieldName="LATITUD" ShowInCustomizationForm="True" VisibleIndex="8">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="REP. LEGAL" FieldName="REPRESENTANTE_LEGAL" ShowInCustomizationForm="True" VisibleIndex="7" ToolTip="Estado">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="ESTADO" FieldName="LONGITUD" ShowInCustomizationForm="True" VisibleIndex="9">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="DEPTO." FieldName="DEPARTAMENTO" ShowInCustomizationForm="True" VisibleIndex="2">
                                                        <Settings AutoFilterCondition="Contains" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="ID CONSUM." FieldName="ID_CONSUMIDOR" ShowInCustomizationForm="True" VisibleIndex="4">
                                                        <Settings AutoFilterCondition="Contains" />
                                                        <Settings AutoFilterCondition="Contains" />
                                                    </dx:GridViewDataTextColumn>
                                                </Columns>
                                                
                                                <TotalSummary>
                                                    <dx:ASPxSummaryItem FieldName="F_IMPORTE" ShowInColumn="IMPORTE" SummaryType="Custom" Tag="imp" />
                                                    <dx:ASPxSummaryItem FieldName="VOLUMEN" ShowInColumn="CANTIDAD" SummaryType="Custom" Tag="vol" />
                                                    <dx:ASPxSummaryItem FieldName="STR_ESTADO" ShowInColumn="EST." SummaryType="Custom" Tag="est" />
                                                </TotalSummary>
                                            </dx:ASPxGridView>
                                        </td>
                                        
                                        <td>
                                            


                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style10"></td>
                                        <td class="auto-style11">
                                            <dx:ASPxButton ID="ASPxButton1" runat="server" OnClick="ASPxButton1_Click1" Text="ASPxButton" ClientInstanceName="ASPxButton1">
                                            </dx:ASPxButton>
                                        </td>
                                        <td>
                                            
                                            
                                            <script type="text/javascript" src="../Scripts/jquery-1.3.2.min.js" ></script> 

                                            <script type = "text/javascript"> 
                                                jQuery(document).ready(function() { 
                                                    $('#btnVerMapa').click(getContactos);
                                                }); 

                                                function getContactos() { 
                                                    $.ajax({ 
                                                        type: "POST", 
                                                        url: "JQAjaxArrayJSon.aspx/ObtenerContactos", 
                                                        data: "{}", 
                                                        contentType: "application/json; charset=utf-8", 
                                                        dataType: "json", 
                                                        success: function (response) { 
                                                            var contactos = (typeof response.d) == 'string' ?
                                                                               eval('(' + response.d + ')') :
                                                                               response.d;

                                                            for (var i = 0; i < contactos.length; i++) {
                                                                alert(contactos[i].Nombre);
                                                            }
                                                        }, 
                                                        error: function (result) { 
                                                            alert('ERROR ' + result.status + ' ' + result.statusText); 
                                                        } 
                                                    }); 
                                                } 
                                            </script> 

                                            <dx:ASPxButton ID="btnVerMapa" runat="server" Text="Ver Mapa">
                                                <ClientSideEvents Click="function(s, e) {
	getContactos();
}" />
                                            </dx:ASPxButton>
                                        </td>
                                        <td class="auto-style10"></td>
                                        <td class="auto-style12"></td>
                                        <td class="auto-style10">
                                            </td>
                                        <td class="auto-style10">&nbsp;</td>
                                        <td class="auto-style10">
                                            &nbsp;</td>
                                        <td class="auto-style16"></td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td class="auto-style5">
                                            &nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td class="auto-style9">&nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td class="auto-style15">&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                </table>
                            </dx:PanelContent>
                        </PanelCollection>
                    </dx:ASPxCallbackPanel>
                            </td>
                            <td>
                                
                                

                                <%--<dx:ASPxCallbackPanel ID="pnlPrueba" runat="server" Width="500px" OnCallback="pnlPrueba_Callback" ClientInstanceName="pnlPrueba">
                                    
                                    <PanelCollection>
                                        <dx:PanelContent runat="server">
                                            <cc:GMap ID="GMap1" runat="server" Width="500px" />
                                        </dx:PanelContent>
                                    </PanelCollection>
                                    
                                </dx:ASPxCallbackPanel>--%>
                                
                                
                                <script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?sensor=false"></script>
                                <script type="text/javascript">
                                    var markers = [
                                        {
                                            "title": 'Aksa Beach',
                                            "lat": '19.1759668',
                                            "lng": '72.79504659999998',
                                            "description": 'Aksa Beach is a popular beach and a vacation spot in Aksa village at Malad, Mumbai.'
                                        },
                                        {
                                            "title": 'Juhu Beach',
                                            "lat": '19.0883595',
                                            "lng": '72.82652380000002',
                                            "description": 'Juhu Beach is one of favourite tourist attractions situated in Mumbai.'
                                        },
                                        {
                                            "title": 'Girgaum Beach',
                                            "lat": '18.9542149',
                                            "lng": '72.81203529999993',
                                            "description": 'Girgaum Beach commonly known as just Chaupati is one of the most famous public beaches in Mumbai.'
                                        },
                                        {
                                            "title": 'Jijamata Udyan',
                                            "lat": '18.979006',
                                            "lng": '72.83388300000001',
                                            "description": 'Jijamata Udyan is situated near Byculla station is famous as Mumbai (Bombay) Zoo.'
                                        },
                                        {
                                            "title": 'Sanjay Gandhi National Park',
                                            "lat": '19.2147067',
                                            "lng": '72.91062020000004',
                                            "description": 'Sanjay Gandhi National Park is a large protected area in the northern part of Mumbai city.'
                                        }
                                    ];
                                    window.onload = function () {
                                        LoadMap();
                                    };

                                    var map;
                                    var marker;
                                    function LoadMap() {
                                        var mapOptions = {
                                            center: new google.maps.LatLng(markers[0].lat, markers[0].lng),
                                            zoom: 10,
                                            mapTypeId: google.maps.MapTypeId.ROADMAP
                                        };
                                        map = new google.maps.Map(document.getElementById("dvMap"), mapOptions);
                                        SetMarker(0);
                                    };

                                    function SetMarker(position) {
                                        //Remove previous Marker.
                                        if (marker != null) {
                                            marker.setMap(null);
                                        }

                                        //Set Marker on Map.
                                        var data = markers[position];
                                        var myLatlng = new google.maps.LatLng(data.lat, data.lng);
                                        marker = new google.maps.Marker({
                                            position: myLatlng,
                                            map: map,
                                            title: data.title
                                        });
                                        

                                        //Create and open InfoWindow.
                                        var infoWindow = new google.maps.InfoWindow();
                                        infoWindow.setContent("<div style = 'width:200px;min-height:40px'>" + data.description + "</div>");
                                        infoWindow.open(map, marker);
                                    };
                                </script>

                                <%--<div>
                                    <label for="rbMarker0">
                                        <input type="radio" id="rbMarker0" name="rbMarker" value="0" onclick="SetMarker(this.value)"
                                            checked="checked" />Aksa Beach</label><br />
                                    <label for="rbMarker1">
                                        <input type="radio" id="rbMarker1" name="rbMarker" value="1" onclick="SetMarker(this.value)" />Juhu
                                        Beach</label><br />
                                    <label for="rbMarker2">
                                        <input type="radio" id="rbMarker2" name="rbMarker" value="2" onclick="SetMarker(this.value)" />Girgaum
                                        Beach</label><br />
                                    <label for="rbMarker3">
                                        <input type="radio" id="rbMarker3" name="rbMarker" value="3" onclick="SetMarker(this.value)" />Jijamata
                                        Udyan</label><br />
                                    <label for="rbMarker4">
                                        <input type="radio" id="rbMarker4" name="rbMarker" value="4" onclick="SetMarker(this.value)" />Sanjay
                                        Gandhi National Park</label>
                                </div>
                                <hr />
                                <div id="dvMap" style="width: 500px; height: 300px">
                                </div>--%>
                                
                                
                                
                                <style type="text/css">
                                #mapa { height: 500px; }
                                </style>
                                <script type="text/javascript" src="http://maps.google.com/maps/api/js?sensor=false"></script>
                                <script type="text/javascript">
                                    function initialize() {
                                        var marcadores = [
                                          ['León', 42.603, -5.577],
                                          ['Salamanca', 40.963, -5.669],
                                          ['Zamora', 41.503, -5.744]
                                        ];
                                        var map = new google.maps.Map(document.getElementById('mapa'), {
                                            zoom: 7,
                                            center: new google.maps.LatLng(41.503, -5.744),
                                            mapTypeId: google.maps.MapTypeId.ROADMAP
                                        });
                                        var infowindow = new google.maps.InfoWindow();
                                        var marker, i;
                                        for (i = 0; i < marcadores.length; i++) {
                                            marker = new google.maps.Marker({
                                                position: new google.maps.LatLng(marcadores[i][1], marcadores[i][2]),
                                                map: map
                                            });
                                            google.maps.event.addListener(marker, 'click', (function (marker, i) {
                                                return function () {
                                                    infowindow.setContent(marcadores[i][0]);
                                                    infowindow.open(map, marker);
                                                }
                                            })(marker, i));
                                        }
                                    }
                                    google.maps.event.addDomListener(window, 'load', initialize);
                                </script>
                                
                                
                                

                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div id="mapa"></div>
                            </td>
                        </tr>
                    </table>
                    
                    
                    

                </dx:SplitterContentControl>
            </ContentCollection>
        </dx:SplitterPane>
    </Panes>
</dx:ASPxSplitter>



<dx:ASPxPopupControl ID="pucDatos" runat="server" AllowDragging="True" ClientInstanceName="pucDatos" EnableTheming="True" HeaderText="Información" Height="93px" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Theme="Office2003Olive" Width="334px" OnWindowCallback="pucDatos_WindowCallback">
    <ContentCollection>
        <dx:PopupControlContentControl runat="server">
            <table class="dxflInternalEditorTable_Youthful">
                <tr>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel39" runat="server" Text="ID Consumidor :">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="txtPiId" runat="server" Width="170px">
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style13">
                        &nbsp;</td>
                    <td class="auto-style13">&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>














<dx:ASPxPopupControl ID="pucModEstado" runat="server" AllowDragging="True" ClientInstanceName="pucModEstado" EnableTheming="True" HeaderText="Modificar Estado" Height="93px" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Theme="Office2003Olive" Width="400px" OnWindowCallback="pucModEstado_WindowCallback">
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
                    </td>
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
                        <dx:ASPxButton ID="btnSubirLlave0" runat="server" EnableTheming="True" Text="Modificar Estado" Theme="Office2003Olive" Width="100%">
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>





