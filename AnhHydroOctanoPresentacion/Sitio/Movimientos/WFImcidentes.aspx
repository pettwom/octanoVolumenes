<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio/ANH.Master" AutoEventWireup="true" CodeBehind="WFImcidentes.aspx.cs" Inherits="AnhHydroOctanoPresentacion.Sitio.Movimientos.WFImcidentes" %>

<%@ Register Src="~/Controles/CtrImportacionDatos.ascx" TagPrefix="uc1" TagName="CtrImportacionDatos" %>
<%@ Register TagPrefix="dx" Namespace="DevExpress.Web" Assembly="DevExpress.Web.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>
<%@ Register TagPrefix="dx" Namespace="DevExpress.Web.ASPxTreeList" Assembly="DevExpress.Web.ASPxTreeList.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <%--<uc1:CtrImportacionDatos runat="server" ID="CtrImportacionDatos"/>--%>
    <style type="text/css">
        .auto-style1 {
            height: 18px;
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

        .panelPersona {
            position: fixed;
            top: 410px;
            width: 500px;
            background: white;
        }

        .hydro_smart_container {
            margin: 0;
            padding: 0;
            Height: 690px;
        }

        .hydro_smart_header {
            margin: 0;
            padding: 5px 10px;
            /*background: orange;*/
            background: rgb(0, 132, 38);
            color: white;
            font-size: medium;
            font-weight: bold;
        }

        .hydro_smart_table {
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
            color: #555;
            border-radius: 10px;
            font-family: Tahoma,Geneva,Arial,sans-serif;
            font-size: 11px;
            padding: 10px 10px 10px 36px;
            margin: 10px;
        }
    </style>

    <div class="col-md-12">
        <table style="width: 100%">
            <tr>
                <td style="text-align: center; padding-bottom: 1px">
                    <dx:ASPxRoundPanel runat="server" ShowHeader="False" Width="100%" ID="pnlTitulo">
                        <PanelCollection>
                            <dx:PanelContent runat="server">
                                <dx:ASPxLabel runat="server" ForeColor="#367B2F" ID="lblTitulo" Font-Bold="True" Font-Names="Rockwell" Font-Size="Larger" Text="INCIDENTES"></dx:ASPxLabel>
                            </dx:PanelContent>
                        </PanelCollection>
                        <Border BorderStyle="Ridge" BorderWidth="1px" BorderColor="#003300"></Border>
                    </dx:ASPxRoundPanel>
                </td>
            </tr>
        </table>
        <script type="text/javascript">
            function iniciaRegistro() {
                //$("#modalAgregarMedioVerifcacionCorreccion").modal();
                //pucDatos.Hide();

                var textocmbPeriodo = cmbPeriodo.GetText();
                if (textocmbPeriodo == "") {
                    alert("Seleccione Periodo");
                } else {
                    pucDatos.Show();
                    panelRegistroSeguimiento.PerformCallback();
                }

            }
            function OnLinkClick_AgregarImplemtaCorreccion(id, visibleIndex) {
                var mensaje = confirm("¿Esta seguro de realizar la modificación?");
                if (mensaje) {
                    var textoAccion = "moficiar-" + id;
                    pucDatos.Show();
                    panelRegistroSeguimiento.PerformCallback(textoAccion);
                }
            }
            function OnLinkClick_EliminarElemtaCorreccion(id, visibleIndex) {
                var mensaje = confirm("¿Esta seguro de ELIMINAR EL REGISTRO?");
                if (mensaje) {
                    var textoAccion = "eliminar-" + id;
                    dgvIncidentes.PerformCallback(textoAccion);
                }
            }
            //ClientInstanceName="memoDescripcionriesgoOportunidad" ClientSideEvents="memoDescripcionriesgoOportunidad"
            function fRegistroIncidente() {
                var textocmbTipoIncidente = cmbTipoIncidente.GetText();
                var textotxtAreaUnidad = txtAreaUnidad.GetText();
                var textomemoDescripcion = memoDescripcion.GetText();
                var textodeFechaIncio = deFechaIncio.GetText();
                var textodecmbPeriodo = cmbPeriodo.GetText();
                if (textodecmbPeriodo == "") {
                    alert("Seleccione el periodo");
                } else if (textocmbTipoIncidente == "") {
                    alert("Seleccione Tipo de incidente");
                } else if (textotxtAreaUnidad == "") {
                    alert("El campo Area/Unidad es Obligatorio");
                } else if (textomemoDescripcion == "") {
                    alert("El Campo descripción es obligatorio");
                }
                else if (textodeFechaIncio == "") {
                    alert("La fecha Incio es Obligatoria");
                } else {
                    dgvIncidentes.PerformCallback('registro');
                    pucDatos.Hide();
                }
            }
        </script>
        <dx:ASPxSplitter ID="SplEntorno" runat="server" Height="100%" ClientInstanceName="SplEntorno">
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
                                    <td>
                                        <dx:ASPxCallbackPanel ID="pnlCabecera" runat="server" ClientInstanceName="pnlCabecera" OnCallback="pnlCabecera_Callback" Width="100%">
                                            <PanelCollection>
                                                <dx:PanelContent runat="server">

                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td>
                                                                <dx:ASPxLabel ID="ASPxLabel56" runat="server" ForeColor="#003300" Text="Periodo :">
                                                                </dx:ASPxLabel>
                                                                <dx:ASPxComboBox ID="cmbPeriodo" runat="server" EnableTheming="True" Font-Bold="True" Theme="Metropolis" ValueType="System.Int32" Width="160px" ToolTip="Seleccione un Periodo" ClientInstanceName="cmbPeriodo" ClientSideEvents="cmbPeriodo">
                                                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
	    pnlDatos.PerformCallback();
    }" />
                                                                    <ClearButton Visibility="Auto">
                                                                    </ClearButton>
                                                                    <CaptionStyle Font-Bold="False">
                                                                    </CaptionStyle>
                                                                    <DisabledStyle Font-Bold="False">
                                                                    </DisabledStyle>
                                                                </dx:ASPxComboBox>
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td style="padding-bottom: 5px"></td>
                                                        </tr>
                                                    </table>

                                                </dx:PanelContent>
                                            </PanelCollection>
                                        </dx:ASPxCallbackPanel>
                                    </td>
                                </tr>

                                <tr>
                                    <td></td>
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
                                        <dx:ASPxTreeList ID="tlsOperadores" runat="server" AutoGenerateColumns="False" Font-Size="Smaller" Height="187px" KeyFieldName="intId" ParentFieldName="intIdPadre" Theme="Youthful" Styles-Header-BackColor="#317D40" ToolTip="Seleccione un proyecto" ClientInstanceName="tlsOperadores" OnCustomCallback="tlsOperadores_CustomCallback">
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
pnlCabecera.PerformCallback();
	    pnlDatos.PerformCallback();
}"
                                                NodeDblClick="function(s, e) {
pnlCabecera.PerformCallback();
	    pnlDatos.PerformCallback();

}"
                                                EndCallback="function(s, e) {
pnlCabecera.PerformCallback();
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
                        <dx:SplitterContentControl runat="server">





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

                                                    <dx:ASPxCallbackPanel ID="panelMensajeError" runat="server" OnCallback="panelMensajeError_Callback" Width="100%" ClientInstanceName="panelMensajeError">
                                                        <PanelCollection>
                                                            <dx:PanelContent runat="server">
                                                                <dx:ASPxRoundPanel ID="pnlMensaje" runat="server" ShowHeader="False" Visible="False" Width="100%" ShowCollapseButton="True">
                                                                    <PanelCollection>
                                                                        <dx:PanelContent runat="server">
                                                                            <dx:ASPxLabel ID="lblMensajeError" runat="server" ForeColor="#666666" Width="100%">
                                                                            </dx:ASPxLabel>
                                                                        </dx:PanelContent>
                                                                    </PanelCollection>
                                                                    <Border BorderStyle="Ridge" BorderWidth="1px" />
                                                                </dx:ASPxRoundPanel>

                                                            </dx:PanelContent>
                                                        </PanelCollection>
                                                    </dx:ASPxCallbackPanel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6">
                                                    <legend>
                                                        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Rockwell" Font-Size="Medium" ForeColor="#367B2F" Text="DATOS DEL PROYECTO"></asp:Label>
                                                    </legend>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3">
                                                    <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="#10420F" Text="NOMBRE COMERCIAL"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label3" runat="server" Font-Bold="True" ForeColor="#10420F" Text="NIT"></asp:Label>
                                                </td>
                                                <td colspan="2">
                                                    <asp:Label ID="Label4" runat="server" Font-Bold="True" ForeColor="#10420F" Text="DIRECCION"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" style="padding-bottom: 5px; width: 50%">
                                                    <asp:Label ID="lblRazonSocial" runat="server"></asp:Label>
                                                </td>
                                                <td style="padding-bottom: 5px; width: 10%">
                                                    <asp:Label ID="lblNit" runat="server"></asp:Label>
                                                </td>
                                                <td colspan="2" style="padding-bottom: 5px; width: 40%">
                                                    <asp:Label ID="lblDireccion" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <tr>
                                                    <td></td>
                                                </tr>
                                            </tr>

                                        </table>
                                        <div class="col-md-12">
                                            <dx:ASPxButton ID="ASPxButton1" runat="server" AutoPostBack="False" CssClass="btn btn-xs btn-default pull-right" Text="AGREGAR REGISTRO" Theme="Metropolis">
                                                <Image IconID="actions_download_16x16">
                                                </Image>
                                                <ClientSideEvents Click="function(s, e) {
	iniciaRegistro();
}" />
                                            </dx:ASPxButton>
                                        </div>

                                        <div class="col-md-12">
                                            <dx:ASPxGridView ID="dgvIncidentes" runat="server" AutoGenerateColumns="False" ClientInstanceName="dgvIncidentes" CssPostfix="DevGlp" EnableTheming="True" Font-Size="Smaller" KeyFieldName="ID_INCIDENTES" OnCustomCallback="dgvIncidentes_CustomCallback" Theme="Default" Width="100%">
                                                <ClientSideEvents EndCallback="function(s, e) {panelMensajeError.PerformCallback();}" />
                                                <SettingsContextMenu EnableRowMenu="False">
                                                </SettingsContextMenu>
                                                <SettingsPager PageSize="100">
                                                    <PageSizeItemSettings Items="50, 100, 200, 500, 1000" Visible="True">
                                                    </PageSizeItemSettings>
                                                </SettingsPager>
                                                <Settings ShowHeaderFilterButton="True" />
                                                <SettingsBehavior AllowFocusedRow="True" />
                                                <SettingsPopup>
                                                    <EditForm Modal="True" />
                                                </SettingsPopup>
                                                <SettingsText CommandNew="Nuevo" />
                                                <Columns>
                                                    <dx:GridViewCommandColumn ShowInCustomizationForm="True" Visible="False" VisibleIndex="0">
                                                    </dx:GridViewCommandColumn>

                                                    <dx:GridViewDataTextColumn FieldName="ID_INCIDENTES" ShowInCustomizationForm="True" VisibleIndex="1" Visible="False" SortOrder="Descending">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="FECHA_REGISTRO" FieldName="FECHA_REGISTRO" ShowInCustomizationForm="True" ToolTip="Fecha Registro" VisibleIndex="2">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="CLASIFICADOR" ShowInCustomizationForm="True" VisibleIndex="3" Caption="CALSIFICADOR">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="TIPO_ACTIVIDAD" ShowInCustomizationForm="True" VisibleIndex="3" Visible="False">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="TIPO_INCIDENTE" ShowInCustomizationForm="True" Caption="Tipo Incidente" VisibleIndex="4">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="DESCRIPCION" ShowInCustomizationForm="True" VisibleIndex="5" Caption="Descripción">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="AREA_UYNIDAD" ShowInCustomizationForm="True" VisibleIndex="6" Caption="Area/Unidad">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="FECHA_INICIO_SUCESO" ShowInCustomizationForm="True" VisibleIndex="7" Caption="Fecha Incio de Suceso">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="FECHA_FIN_SUCESO" ShowInCustomizationForm="True" VisibleIndex="8" Caption="Fecha Fin de Suceso">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="FECHA_REINCIO" ShowInCustomizationForm="True" VisibleIndex="9" Caption="Fecha Reinicio">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="PEROP_GESTION" ShowInCustomizationForm="True" VisibleIndex="10" Caption="Periodo">
                                                        <Settings AutoFilterCondition="Equals" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="OPCIONES" ShowInCustomizationForm="True" VisibleIndex="11">
                                                        <Settings AutoFilterCondition="Equals" />
                                                        <DataItemTemplate>
                                                            <a id="BotonmODIFICARiTEM" href="javascript:OnLinkClick_AgregarImplemtaCorreccion(<%# Container.KeyValue.ToString()%>, <%# Container.VisibleIndex.ToString()%>);">
                                                                <div class="btn btn-default btn-xs" data-toggle="tooltip" data-placement="top" title="MODIFICAR REGISTRO"><span class=" 	glyphicon glyphicon-edit" style="color: #2d7e3e  !important"></span></div>
                                                            </a>
                                                            <a id="BotonEliminaItem" href="javascript:OnLinkClick_EliminarElemtaCorreccion(<%# Container.KeyValue.ToString()%>, <%# Container.VisibleIndex.ToString()%>);">
                                                                <div class="btn btn-default btn-xs" data-toggle="tooltip" data-placement="top" title="ELIMINAR REGISTRO"><span class=" 	glyphicon glyphicon-trash" style="color: #2d7e3e  !important"></span></div>
                                                            </a>
                                                        </DataItemTemplate>
                                                    </dx:GridViewDataTextColumn>
                                                </Columns>
                                                <Styles CssPostfix="DevGlp">
                                                    <AlternatingRow BackColor="#EEEEEE">
                                                    </AlternatingRow>
                                                </Styles>
                                            </dx:ASPxGridView>
                                        </div>
                                    </dx:PanelContent>
                                </PanelCollection>
                            </dx:ASPxCallbackPanel>
                        </dx:SplitterContentControl>
                    </ContentCollection>
                </dx:SplitterPane>
            </Panes>
        </dx:ASPxSplitter>
    </div>

    <dx:ASPxPopupControl ID="pucDatos" runat="server" AllowDragging="True" ClientInstanceName="pucDatos" EnableTheming="True" HeaderText="Registro de Incidentes" Height="93px" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Theme="Office2003Olive" Width="600px">
        <HeaderImage IconID="setup_pagesetup_16x16">
        </HeaderImage>
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">


                <dx:ASPxCallbackPanel runat="server" Width="100%" ID="panelRegistroSeguimiento" ClientInstanceName="panelRegistroSeguimiento" BorderBottom-BorderStyle="None" OnCallback="panelRegistroSeguimiento_Callback">
                    <%--                        <ClientSideEvents EndCallback="function(s, e) {finRegistro();}" />--%>
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent1" runat="server">
                            <div class="col-md-6">
                                <strong>Clasificador</strong>
                                <dx:ASPxComboBox ID="cmbClasificaIncident" runat="server" TextFormatString="{0} {1} {2}" EnableTheming="True" Font-Bold="True" Theme="Metropolis" ValueType="System.String" Width="100%" ToolTip="Seleccione un Periodo" ClientInstanceName="cmbClasificaIncident" ClientSideEvents="cmbClasificaIncident" ValueField="ID_PARAMETRICA_GLOBAL" TextField="DESCRIPCION">
                                </dx:ASPxComboBox>
                            </div>
                            <div class="col-md-12">
                                <strong>Tipo incidente</strong>
                                <dx:ASPxComboBox ID="cmbTipoIncidente" runat="server" TextFormatString="{0} {1} {2}" EnableTheming="True" Font-Bold="True" Theme="Metropolis" ValueType="System.String" Width="100%" ToolTip="Seleccione un Periodo" ClientInstanceName="cmbTipoIncidente" ClientSideEvents="cmbTipoIncidente" ValueField="ID_PARAMETRICA_GLOBAL">
                                    <Columns>
                                        <dx:ListBoxColumn FieldName="TIPO_INCIDENTE" Width="25%" />
                                        <dx:ListBoxColumn FieldName="ORDEN" Caption="NIVEL" Width="8%" />
                                        <dx:ListBoxColumn FieldName="DESCRIPCION" Width="77%" />
                                    </Columns>
                                </dx:ASPxComboBox>
                            </div>
                            <div class="col-md-12">
                                <strong>Area/Unidad</strong>
                                <dx:ASPxTextBox ID="txtAreaUnidad" runat="server" Width="100%" Theme="Metropolis" ClientInstanceName="txtAreaUnidad" ClientSideEvents="txtAreaUnidad">
                                </dx:ASPxTextBox>
                            </div>
                            <div class="col-md-12">
                                <strong>Descripcion Incidente</strong>
                                <dx:ASPxMemo runat="server" ID="memoDescripcion" ClientInstanceName="memoDescripcion" Theme="Metropolis" ClientSideEvents="memoDescripcion" Height="51px" Width="100%" Style="text-transform: uppercase" Visible="True">
                                    <%--<ClientSideEvents KeyUp="function(s, e) { s.SetText(s.GetText().toUpperCase());}" />--%>
                                </dx:ASPxMemo>

                            </div>
                            <div class="col-md-4"></div>
                            <div class="col-md-4"></div>
                            <div class="col-md-4"></div>
                            <div class="col-md-4">
                                <strong>Fecha hora Inicio de Suceso</strong>
                                <dx:ASPxDateEdit ID="deFechaIncio" runat="server" ClientInstanceName="deFechaIncio" ClientSideEvents="deFechaIncio" Theme="Metropolis">

                                    <TimeSectionProperties>
                                        <TimeEditProperties>
                                            <ClearButton Visibility="Auto">
                                            </ClearButton>
                                        </TimeEditProperties>
                                    </TimeSectionProperties>
                                    <ClearButton Visibility="Auto">
                                    </ClearButton>
                                    <ValidationSettings ErrorDisplayMode="Text" ErrorText="" ErrorTextPosition="Bottom">
                                        <RequiredField ErrorText="Campo obligatorio" IsRequired="True" />
                                    </ValidationSettings>
                                </dx:ASPxDateEdit>

                            </div>
                            <div class="col-md-4">
                                <strong>Fecha hora Fin de Suceso</strong>
                                <dx:ASPxDateEdit ID="dfFechaFin" runat="server" ClientInstanceName="dfFechaFin" ClientSideEvents="dfFechaFin" Theme="Metropolis">

                                    <TimeSectionProperties>
                                        <TimeEditProperties>
                                            <ClearButton Visibility="Auto">
                                            </ClearButton>
                                        </TimeEditProperties>
                                    </TimeSectionProperties>
                                    <ClearButton Visibility="Auto">
                                    </ClearButton>
                                </dx:ASPxDateEdit>

                            </div>
                            <div class="col-md-4">
                                <strong>Fecha hora de Re Inicio</strong>

                                <dx:ASPxDateEdit ID="dfFechaRInicio" runat="server" Theme="Metropolis" ClientInstanceName="deFechaPlazo" ClientSideEvents="dfFechaRInicio">

                                    <TimeSectionProperties>
                                        <TimeEditProperties>
                                            <ClearButton Visibility="Auto">
                                            </ClearButton>
                                        </TimeEditProperties>
                                    </TimeSectionProperties>
                                    <ClearButton Visibility="Auto">
                                    </ClearButton>

                                </dx:ASPxDateEdit>

                            </div>
                        </dx:PanelContent>
                    </PanelCollection>
                    <BorderBottom BorderStyle="None"></BorderBottom>
                </dx:ASPxCallbackPanel>
                <div class="col-md-12">
                    <dx:ASPxButton ID="ASPxButton2" runat="server" Text="CANCELAR" CssClass="btn btn-xs btn-default pull-right" AutoPostBack="False">
                        <ClientSideEvents Click="function(s, e) {
                pucDatos.Hide();
}" />
                    </dx:ASPxButton>
                    <dx:ASPxButton ID="btnRegistroIncidente" runat="server" Text="AGREGAR REGISTRO" CssClass="btn btn-xs btn-default pull-right" AutoPostBack="False">
                        <ClientSideEvents Click="function(s, e) {
 fRegistroIncidente();
}" />
                    </dx:ASPxButton>
                </div>


                <%--                <dx:ASPxButton ID="btnSubirLlave" runat="server" EnableTheming="True" Text="Validar Llave B-Sisa" ForeColor="White" BackColor="#307D40" Width="100%">
                    <Image IconID="actions_apply_16x16">
                    </Image>
                </dx:ASPxButton>--%>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>


















</asp:Content>
