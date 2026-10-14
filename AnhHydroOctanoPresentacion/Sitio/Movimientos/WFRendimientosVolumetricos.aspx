<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio/ANH.Master" AutoEventWireup="true" CodeBehind="WFRendimientosVolumetricos.aspx.cs" Inherits="AnhHydroOctanoPresentacion.Sitio.Movimientos.WFRendimientosVolumetricos" %>

<%@ Register Src="~/Controles/CtrImportacionDatos.ascx" TagPrefix="uc1" TagName="CtrImportacionDatos" %>
<%@ Register TagPrefix="dx" Namespace="DevExpress.Web" Assembly="DevExpress.Web.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>
<%@ Register TagPrefix="dx" Namespace="DevExpress.Web.ASPxTreeList" Assembly="DevExpress.Web.ASPxTreeList.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <%--<uc1:CtrImportacionDatos runat="server" ID="CtrImportacionDatos"/>--%>
    <style type="text/css">
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
                                <dx:ASPxLabel runat="server" ForeColor="#367B2F" ID="lblTitulo" Font-Bold="True" Font-Names="Rockwell" Font-Size="Larger" Text="RENDIMIENTOS VOLUMETRICOS - INSUMOS - PLANTAS"></dx:ASPxLabel>
                            </dx:PanelContent>
                        </PanelCollection>
                        <Border BorderStyle="Ridge" BorderWidth="1px" BorderColor="#003300"></Border>
                    </dx:ASPxRoundPanel>
                </td>
            </tr>
        </table>
        <script type="text/javascript">
            function iniciaRegistro(accion) {
                //$("#modalAgregarMedioVerifcacionCorreccion").modal();
                //pucDatos.Hide();

                var textocmbPeriodo = cmbPeriodo.GetText();
                if (textocmbPeriodo == "") {
                    alert("Seleccione Periodo");
                } else {
                    pucDatos.Show();
                    panelRegistroSeguimiento.PerformCallback(accion);
                }

            }
            function OnLinkClick_AgregarImplemtaCorreccion(id, visibleIndex) {
                //alert(id);
                var mensaje = confirm("¿Esta seguro de realizar la modificación?");
                if (mensaje) {
                    var textoAccion = "agregarRegistro";
                    pucRegistroPorcentageRendimiento.Show();
                    dgvIncidentesRegistro.GetSelectedFieldValues("ID_RENDIMIEN;COMB_LIQUIDO", OnGetSelectedFieldValues);
                    panelRegistroendimientoPorcentual.PerformCallback(textoAccion);
                }
            }
            function OnGetSelectedFieldValues(result) {
                for (var i = 0; i < result.length; i++)
                    for (var j = 0; j < result[i].length; j++) {
                        alert(result[i][j]);
                    }
            }
            function OnLinClick_ModificarElemtaCorreccion(id, visibleIndex) {
                var mensaje = confirm("¿Esta seguro de ELIMINAR EL REGISTRO?");
                if (mensaje) {
                    var textoAccion = "eliminar";
                    pnlDatos.PerformCallback(textoAccion);
                }
            }

            function OnLinClick_EliminarElemtRendimientoPorcen(id, visibleIndex) {
                //var mensaje = confirm("¿Esta seguro de ELIMINAR EL REGISTRO?");
                //if (mensaje) {
                var textoAccion = "eliminar-" + id;
                grbListaRegistroRendPorVolum.PerformCallback(textoAccion);
                //}
            }
            function OnLinClick_AgregarRendimientoPorcen() {
                var textocmbTipRendimVol = cmbTipRendimVol.GetText();

                if (textocmbTipRendimVol == "") {
                    alert('campo requerido');
                } else {
                    var textoAccion = "agregar";
                    grbListaRegistroRendPorVolum.PerformCallback(textoAccion);
                }
            }
            //ClientInstanceName="memoDescripcionriesgoOportunidad" ClientSideEvents="memoDescripcionriesgoOportunidad"
            function fRegistroIncidente() {
                var mensaje = confirm("¿Esta seguro de realizar el registro?");
                if (mensaje) {
                    pnlDatos.PerformCallback('registro');
                    pucDatos.Hide();
                }
            }
            var curentEditingIndex;
            var lastCountry = null;
            var isCustomCascadingCallback = false;
            function fRegistroRendimientoPorcentualVolum() {
                var mensaje = confirm("¿Esta seguro de realizar el registro?");
                if (mensaje) {
                    panelRegistroSeguimiento.PerformCallback('registro');
                    pucRegistroPorcentageRendimiento.Hide();
                }
            }
            function OnBatchEditEndEditing(s, e) {
                templateColumn = dgvIncidentesRegistro.GetSelectedKeysOnPage();
                idItem = templateColumn[0];


                var operacionDias = e.rowValues[12];
                var cargaTotal = e.rowValues[13];
                var cargaPromedio = e.rowValues[14];
                var capacidadOperacion = e.rowValues[15];

                var comLiquidoM3 = e.rowValues[7];
                var combGas = e.rowValues[11];
                var vaporTn = e.rowValues[10];
                var aguaNfrM3 = e.rowValues[9];
                var aireComNm3 = e.rowValues[8];

                var propiaKws = e.rowValues[4];
                var compradaKWats = e.rowValues[5];


                resultado = operacionDias.value + "_" + cargaTotal.value + "_" + cargaPromedio.value + "_" + capacidadOperacion.value + "_" + comLiquidoM3.value + "_" + combGas.value + "_" + vaporTn.value + "_" + aguaNfrM3.value + "_" + aireComNm3.value + "_" + compradaKWats.value + "_" + propiaKws.value;
                panelActulizaLista.PerformCallback(resultado);
            }
            function OnBatchEditStartEditing(s, e) {
                visibleIndex = s.GetRowKey(e.visibleIndex);
                curentEditingIndex = e.visibleIndex;
                var currentCountry = dgvIncidentesRegistro.batchEditApi.GetCellValue(curentEditingIndex, "ID_NORMATIVA");
                var keys = dgvIncidentesRegistro.GetSelectedKeysOnPage();

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
                                            <ClientSideEvents EndCallback="function(s, e) {

	    pnlDatos.PerformCallback();

}" />
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

                                            <ClientSideEvents FocusedNodeChanged="function(s, e) {pnlCabecera.PerformCallback();}"
                                                NodeDblClick="function(s, e) {pnlCabecera.PerformCallback();}"
                                                EndCallback="function(s, e) {pnlCabecera.PerformCallback();}" />
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
LoadingPanel.Hide(); panelMensajeError.PerformCallback();
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
                                            <dx:ASPxButton ID="btnAgregarRegistro" runat="server" AutoPostBack="False" CssClass="btn btn-xs btn-default pull-right" Text="AGREGAR REGISTRO" Theme="Metropolis" Visible="False">
                                                <Image IconID="actions_download_16x16">
                                                </Image>
                                                <ClientSideEvents Click="function(s, e) {

	iniciaRegistro('iniciarRegistro');
}" />
                                            </dx:ASPxButton>
                                            <dx:ASPxButton ID="btnModificarRegistro" runat="server" AutoPostBack="False" CssClass="btn btn-xs btn-default pull-right" Text="MODIFICAR REGISTRO" Theme="Metropolis" Visible="False">
                                                <Image IconID="actions_download_16x16">
                                                </Image>
                                                <ClientSideEvents Click="function(s, e) {
	iniciaRegistro('modificarRegistro');
}" />
                                            </dx:ASPxButton>
                                            <dx:ASPxButton ID="btnReporte" runat="server" AutoPostBack="False" CssClass="btn btn-xs btn-default pull-right" Text="REPORTE" Theme="Metropolis" Visible="False">
                                                <Image IconID="actions_download_16x16">
                                                </Image>
                                                <ClientSideEvents Click="function(s, e) {
                    pnlDatos.PerformCallback('reporte');
}" />
                                            </dx:ASPxButton>
                                        </div>

                                        <div class="col-md-12">
                                            <dx:ASPxGridView ID="dgvIncidentes" runat="server" AutoGenerateColumns="False" ClientInstanceName="dgvIncidentes" CssPostfix="DevGlp" EnableTheming="True" Font-Size="Smaller" KeyFieldName="ID_RENDIMIEN" OnCustomCallback="dgvIncidentes_CustomCallback" Theme="Default" Width="100%">
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
                                                    <dx:GridViewDataTextColumn ShowInCustomizationForm="True" Visible="False" VisibleIndex="0" FieldName="ID_RENDIMIEN">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="TIPO PRODUCTO" ShowInCustomizationForm="True" VisibleIndex="1">
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="AREA/UNIDAD" ShowInCustomizationForm="True" VisibleIndex="2" FieldName="AREA_UNIDAD">
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewBandColumn Caption="E. ELECTRICA" ShowInCustomizationForm="True" VisibleIndex="15">
                                                        <Columns>
                                                            <dx:GridViewDataSpinEditColumn Caption="PROPIA kWH" ShowInCustomizationForm="True" VisibleIndex="1" FieldName="PROPIA_KWH">
                                                                <PropertiesSpinEdit DisplayFormatString="F" DecimalPlaces="2" NumberFormat="Custom">
                                                                    <ClearButton Visibility="Auto">
                                                                    </ClearButton>
                                                                </PropertiesSpinEdit>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                                            </dx:GridViewDataSpinEditColumn>
                                                            <dx:GridViewDataSpinEditColumn Caption="COMPRADA Kwh" ShowInCustomizationForm="True" VisibleIndex="0" FieldName="COMPARAD_KWH">
                                                                <PropertiesSpinEdit DisplayFormatString="0.0" DecimalPlaces="2" NumberFormat="Custom">
                                                                    <ClearButton Visibility="Auto">
                                                                    </ClearButton>
                                                                </PropertiesSpinEdit>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                                            </dx:GridViewDataSpinEditColumn>
                                                        </Columns>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                                    </dx:GridViewBandColumn>
                                                    <dx:GridViewBandColumn Caption="CONSUMOS" ShowInCustomizationForm="True" VisibleIndex="11">
                                                        <Columns>
                                                            <dx:GridViewDataSpinEditColumn Caption="COMB LIQUIDO M3" ShowInCustomizationForm="True" VisibleIndex="0" FieldName="COMB_LIQUIDO">
                                                                <PropertiesSpinEdit DisplayFormatString="0.0" DecimalPlaces="2" NumberFormat="Custom">
                                                                    <ClearButton Visibility="Auto">
                                                                    </ClearButton>
                                                                </PropertiesSpinEdit>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                                            </dx:GridViewDataSpinEditColumn>
                                                            <dx:GridViewDataSpinEditColumn Caption="AIRE COMP NM3" ShowInCustomizationForm="True" VisibleIndex="4" FieldName="AIRE_COMP">
                                                                <PropertiesSpinEdit DisplayFormatString="0.0" DecimalPlaces="2" NumberFormat="Custom">
                                                                    <ClearButton Visibility="Auto">
                                                                    </ClearButton>
                                                                </PropertiesSpinEdit>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                                            </dx:GridViewDataSpinEditColumn>
                                                            <dx:GridViewDataSpinEditColumn Caption="AGUA ENFR M3" ShowInCustomizationForm="True" VisibleIndex="3" FieldName="AGUA_ENFR">
                                                                <PropertiesSpinEdit DisplayFormatString="0.0" DecimalPlaces="2" NumberFormat="Custom">
                                                                    <ClearButton Visibility="Auto">
                                                                    </ClearButton>
                                                                </PropertiesSpinEdit>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                                            </dx:GridViewDataSpinEditColumn>
                                                            <dx:GridViewDataSpinEditColumn Caption="VAPOR TN" ShowInCustomizationForm="True" VisibleIndex="2" FieldName="VAPOR_TN">
                                                                <PropertiesSpinEdit DisplayFormatString="0.0" DecimalPlaces="2" NumberFormat="Custom">
                                                                    <ClearButton Visibility="Auto">
                                                                    </ClearButton>
                                                                </PropertiesSpinEdit>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                                            </dx:GridViewDataSpinEditColumn>
                                                            <dx:GridViewDataSpinEditColumn Caption="COMB GAS MMSCF" ShowInCustomizationForm="True" VisibleIndex="1" FieldName="COMB_GAS">
                                                                <PropertiesSpinEdit DisplayFormatString="0.0" DecimalPlaces="2" NumberFormat="Custom">
                                                                    <ClearButton Visibility="Auto">
                                                                    </ClearButton>
                                                                </PropertiesSpinEdit>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                                            </dx:GridViewDataSpinEditColumn>
                                                        </Columns>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                                    </dx:GridViewBandColumn>
                                                    <dx:GridViewDataSpinEditColumn Caption="OPERACIÓN DIAS" ShowInCustomizationForm="True" VisibleIndex="3" FieldName="OPERACION_DIA">
                                                        <PropertiesSpinEdit DisplayFormatString="0.0" DecimalPlaces="2" NumberFormat="Custom">
                                                            <ClearButton Visibility="Auto">
                                                            </ClearButton>
                                                        </PropertiesSpinEdit>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                                    </dx:GridViewDataSpinEditColumn>
                                                    <dx:GridViewDataSpinEditColumn Caption="CARGA TOTAL M3" ShowInCustomizationForm="True" VisibleIndex="4" FieldName="CARGA_TOTAL">
                                                        <PropertiesSpinEdit DisplayFormatString="0.0" DecimalPlaces="2" NumberFormat="Custom">
                                                            <ClearButton Visibility="Auto">
                                                            </ClearButton>
                                                        </PropertiesSpinEdit>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                                    </dx:GridViewDataSpinEditColumn>
                                                    <dx:GridViewDataSpinEditColumn Caption="CARGA PROMEDIO BPDO" VisibleIndex="5" FieldName="CARGA_PROMEDIO">

                                                        <PropertiesSpinEdit DisplayFormatString="g">
                                                            <ClearButton Visibility="Auto"></ClearButton>
                                                        </PropertiesSpinEdit>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                                    </dx:GridViewDataSpinEditColumn>
                                                    <dx:GridViewDataSpinEditColumn Caption="% CAPACIDAD OPERACIÓN" VisibleIndex="9" FieldName="CAPACIDAD_OPERACION">
                                                        <PropertiesSpinEdit DisplayFormatString="0.0" DecimalPlaces="2" NumberFormat="Custom">
                                                            <ClearButton Visibility="Auto">
                                                            </ClearButton>
                                                        </PropertiesSpinEdit>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                                    </dx:GridViewDataSpinEditColumn>
                                                    <dx:GridViewDataTextColumn Caption="RENDIMIENTO % VOL" ShowInCustomizationForm="True" VisibleIndex="10" FieldName="RENDIMIENTO_PORCENT_VOLUMETRICO">
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
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

    <dx:ASPxPopupControl ID="pucDatos" runat="server" CloseAction="CloseButton" AllowDragging="True" CloseOnEscape="True" PopupHorizontalAlign="WindowCenter"
        ClientInstanceName="pucDatos" EnableTheming="True" HeaderText="RENDIMIENTOS VOLUMETRICOS - INSUMOS - PLANTAS" Height="100%" Modal="True"
        PopupVerticalAlign="Above" Theme="Office2003Olive" Width="800px">
        <HeaderImage IconID="setup_pagesetup_16x16">
        </HeaderImage>
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">

                <div class="col-md-12">
                    <dx:ASPxCallbackPanel runat="server" Width="100%" ID="panelRegistroSeguimiento" ClientInstanceName="panelRegistroSeguimiento" BorderBottom-BorderStyle="None" OnCallback="panelRegistroSeguimiento_Callback">
                        <%--                        <ClientSideEvents EndCallback="function(s, e) {finRegistro();}" />--%>
                        <PanelCollection>
                            <dx:PanelContent ID="PanelContent1" runat="server">

                                <table>
                                    <tr>
                                        <td>
                                            <dx:ASPxGridView ID="dgvIncidentesRegistro" runat="server" AutoGenerateColumns="False" ClientInstanceName="dgvIncidentesRegistro" CssPostfix="DevGlp" EnableTheming="True" Font-Size="Smaller" KeyFieldName="ID_RENDIMIEN" OnCustomCallback="dgvIncidentesRegistro_CustomCallback" Theme="Default" Width="100%" Visible="True">
                                                <%--OnBatchUpdate="Grid_BatchUpdate" OnRowUpdating="Grid_RowUpdating"--%>
                                                <%--<ClientSideEvents EndCallback="function(s, e) {panelMensajeError.PerformCallback();}" />--%>
                                                <ClientSideEvents BatchEditEndEditing="OnBatchEditEndEditing" BatchEditStartEditing="OnBatchEditStartEditing" />
                                                <SettingsContextMenu EnableRowMenu="False">
                                                </SettingsContextMenu>
                                                <SettingsPager PageSize="100">
                                                    <PageSizeItemSettings Items="50, 100, 200, 500, 1000" Visible="True">
                                                    </PageSizeItemSettings>
                                                </SettingsPager>
                                                <SettingsEditing Mode="Batch">
                                                    <BatchEditSettings ShowConfirmOnLosingChanges="False" />
                                                </SettingsEditing>
                                                <Settings ShowHeaderFilterButton="True" />
                                                <SettingsBehavior AllowFocusedRow="True" />
                                                <SettingsCommandButton>
                                                    <UpdateButton Text="   " ButtonType="Button">
                                                        <Styles>
                                                            <Style Font-Bold="True" ForeColor="White">
                                                </Style>
                                                        </Styles>
                                                    </UpdateButton>
                                                    <CancelButton Text="    ">
                                                        <Styles>
                                                            <Style ForeColor="White">
                                                </Style>
                                                        </Styles>
                                                    </CancelButton>
                                                </SettingsCommandButton>
                                                <SettingsPopup>
                                                    <EditForm Modal="True" />
                                                </SettingsPopup>
                                                <SettingsText CommandNew="Nuevo" />
                                                <Columns>
                                                    <dx:GridViewDataTextColumn ShowInCustomizationForm="True" VisibleIndex="0" FieldName="ID_RENDIMIEN" Visible="False">
                                                        <EditFormSettings Visible="False" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="TIPO PRODUCTO" ShowInCustomizationForm="True" VisibleIndex="1">
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                                        <EditFormSettings Visible="False" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="AREA/UNIDAD" ShowInCustomizationForm="True" VisibleIndex="2" FieldName="AREA_UNIDAD">
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                                        <EditFormSettings Visible="False" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewBandColumn Caption="E. ELECTRICA" ShowInCustomizationForm="True" VisibleIndex="13">
                                                        <Columns>
                                                            <dx:GridViewDataSpinEditColumn Caption="PROPIA kWH" ShowInCustomizationForm="True" VisibleIndex="1" FieldName="PROPIA_KWH">
                                                                <PropertiesSpinEdit DisplayFormatString="F" DecimalPlaces="2" NumberFormat="Custom" DisplayFormatInEditMode="True">
                                                                    <ClearButton Visibility="Auto">
                                                                    </ClearButton>
                                                                </PropertiesSpinEdit>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                                            </dx:GridViewDataSpinEditColumn>
                                                            <dx:GridViewDataSpinEditColumn Caption="COMPRADA Kwh" ShowInCustomizationForm="True" VisibleIndex="0" FieldName="COMPARAD_KWH">
                                                                <PropertiesSpinEdit DisplayFormatString="F" DecimalPlaces="2" NumberFormat="Custom">
                                                                    <ClearButton Visibility="Auto">
                                                                    </ClearButton>
                                                                </PropertiesSpinEdit>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                                            </dx:GridViewDataSpinEditColumn>
                                                        </Columns>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                                    </dx:GridViewBandColumn>
                                                    <dx:GridViewBandColumn Caption="CONSUMOS" ShowInCustomizationForm="True" VisibleIndex="9">
                                                        <Columns>
                                                            <dx:GridViewDataSpinEditColumn Caption="COMB LIQUIDO M3" ShowInCustomizationForm="True" VisibleIndex="0" FieldName="COMB_LIQUIDO">
                                                                <PropertiesSpinEdit DisplayFormatString="F" DecimalPlaces="2" NumberFormat="Custom">
                                                                    <ClearButton Visibility="Auto">
                                                                    </ClearButton>
                                                                </PropertiesSpinEdit>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                                            </dx:GridViewDataSpinEditColumn>
                                                            <dx:GridViewDataSpinEditColumn Caption="AIRE COMP NM3" ShowInCustomizationForm="True" VisibleIndex="4" FieldName="AIRE_COMP">
                                                                <PropertiesSpinEdit DisplayFormatString="F" DecimalPlaces="2" NumberFormat="Custom">
                                                                    <ClearButton Visibility="Auto">
                                                                    </ClearButton>
                                                                </PropertiesSpinEdit>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                                            </dx:GridViewDataSpinEditColumn>
                                                            <dx:GridViewDataSpinEditColumn Caption="AGUA ENFR M3" ShowInCustomizationForm="True" VisibleIndex="3" FieldName="AGUA_ENFR">
                                                                <PropertiesSpinEdit DisplayFormatString="F" DecimalPlaces="2" NumberFormat="Custom">
                                                                    <ClearButton Visibility="Auto">
                                                                    </ClearButton>
                                                                </PropertiesSpinEdit>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                                            </dx:GridViewDataSpinEditColumn>
                                                            <dx:GridViewDataSpinEditColumn Caption="VAPOR TN" ShowInCustomizationForm="True" VisibleIndex="2" FieldName="VAPOR_TN">
                                                                <PropertiesSpinEdit DisplayFormatString="F" DecimalPlaces="2" NumberFormat="Custom">
                                                                    <ClearButton Visibility="Auto">
                                                                    </ClearButton>
                                                                </PropertiesSpinEdit>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                                            </dx:GridViewDataSpinEditColumn>
                                                            <dx:GridViewDataSpinEditColumn Caption="COMB GAS MMSCF" ShowInCustomizationForm="True" VisibleIndex="1" FieldName="COMB_GAS">
                                                                <PropertiesSpinEdit DisplayFormatString="F" DecimalPlaces="2" NumberFormat="Custom">
                                                                    <ClearButton Visibility="Auto">
                                                                    </ClearButton>
                                                                </PropertiesSpinEdit>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                                            </dx:GridViewDataSpinEditColumn>
                                                        </Columns>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                                    </dx:GridViewBandColumn>
                                                    <dx:GridViewDataSpinEditColumn Caption="OPERACIÓN DIAS" ShowInCustomizationForm="True" VisibleIndex="3" FieldName="OPERACION_DIA">
                                                        <PropertiesSpinEdit DisplayFormatString="F" DecimalPlaces="2" NumberFormat="Custom">
                                                            <ClearButton Visibility="Auto">
                                                            </ClearButton>
                                                        </PropertiesSpinEdit>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                                    </dx:GridViewDataSpinEditColumn>
                                                    <dx:GridViewDataSpinEditColumn Caption="CARGA TOTAL M3" ShowInCustomizationForm="True" VisibleIndex="4" FieldName="CARGA_TOTAL">
                                                        <PropertiesSpinEdit DisplayFormatString="F" DecimalPlaces="2" NumberFormat="Custom" DisplayFormatInEditMode="True">
                                                            <ClearButton Visibility="Auto">
                                                            </ClearButton>
                                                        </PropertiesSpinEdit>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                                    </dx:GridViewDataSpinEditColumn>
                                                    <dx:GridViewDataSpinEditColumn Caption="CARGA PROMEDIO BPDO" VisibleIndex="5" FieldName="CARGA_PROMEDIO">

                                                        <PropertiesSpinEdit DisplayFormatString="F">
                                                            <ClearButton Visibility="Auto"></ClearButton>
                                                        </PropertiesSpinEdit>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                                    </dx:GridViewDataSpinEditColumn>

                                                    <dx:GridViewDataSpinEditColumn Caption="% CAPACIDAD OPERACIÓN" VisibleIndex="8" FieldName="CAPACIDAD_OPERACION">
                                                        <PropertiesSpinEdit DisplayFormatString="F" DecimalPlaces="2" NumberFormat="Custom">
                                                            <ClearButton Visibility="Auto">
                                                            </ClearButton>
                                                        </PropertiesSpinEdit>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                                    </dx:GridViewDataSpinEditColumn>
                                                    <dx:GridViewDataTextColumn Caption="RENDIMIENTO % VOLUMETRICO" FieldName="RENDIMIENTO_PORCENT_VOLUMETRICO" ShowInCustomizationForm="True" VisibleIndex="14">
                                                        <EditFormSettings Visible="False" />
                                                        <GroupFooterCellStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True">
                                                        </GroupFooterCellStyle>
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="OPCIONES" ShowInCustomizationForm="True" VisibleIndex="15">
                                                        <EditFormSettings Visible="False" />
                                                        <DataItemTemplate>
                                                            <%--                                                            <a id="botonRegistro" href="javascript:OnLinkClick_AgregarImplemtaCorreccion();">
                                                                <div class="btn btn-default btn-xs" data-toggle="tooltip" data-placement="top" title="Agregar RENDIMIENTO % VOLUMETRICO"><span class=" 	glyphicon glyphicon-edit" style="color: #2d7e3e  !important"></span></div>
                                                            </a>--%>
                                                            <a id="BotonEliminaItem" href="javascript:OnLinkClick_AgregarImplemtaCorreccion();">
                                                                <div class="btn btn-default btn-xs" data-toggle="tooltip" data-placement="top" title="Agregar RENDIMIENTO % VOLUMETRICO"><span class=" 	glyphicon glyphicon-pencil" style="color: #2d7e3e  !important"></span></div>
                                                            </a>
                                                        </DataItemTemplate>
                                                    </dx:GridViewDataTextColumn>
                                                </Columns>
                                                <Styles CssPostfix="DevGlp">
                                                    <AlternatingRow BackColor="#EEEEEE">
                                                    </AlternatingRow>
                                                </Styles>
                                            </dx:ASPxGridView>
                                        </td>

                                    </tr>
                                </table>
                                <%--                            <div class="col-md-8">
                            </div>
                            <div class="col-md-4">
                                <div class="col-md-5">
                                </div>
                                <div class="col-md-1">
                                </div>
                                <div class="col-md-6">
                                </div>
                            </div>--%>
                            </dx:PanelContent>
                        </PanelCollection>
                        <BorderBottom BorderStyle="None"></BorderBottom>
                    </dx:ASPxCallbackPanel>
                    <dx:ASPxCallbackPanel runat="server" Width="100%" ID="panelActulizaLista" ClientInstanceName="panelActulizaLista" BorderBottom-BorderStyle="None" OnCallback="panelActulizaLista_Callback">
                        <PanelCollection>
                            <dx:PanelContent ID="PanelContent3" runat="server">
                            </dx:PanelContent>
                        </PanelCollection>
                        <BorderBottom BorderStyle="None"></BorderBottom>
                    </dx:ASPxCallbackPanel>
                </div>
                <div class="col-md-12">
                    <dx:ASPxButton ID="ASPxButton2" runat="server" Text="CANCELAR" CssClass="btn btn-xs btn-default pull-right" AutoPostBack="False">
                        <ClientSideEvents Click="function(s, e) {
                pucDatos.Hide();
}" />
                    </dx:ASPxButton>
                    <dx:ASPxButton ID="btnRegistroIncidente" runat="server" Text="REGISTRAR" CssClass="btn btn-xs btn-default pull-right" AutoPostBack="False">
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



    <dx:ASPxPopupControl ID="pucRegistroPorcentageRendimiento" runat="server" AllowDragging="True" ClientInstanceName="pucRegistroPorcentageRendimiento" EnableTheming="True"
        HeaderText="RENDIMIENTOS % VOLUMETRICOS" Height="100%" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Above" Theme="Office2003Olive" Width="800px">
        <HeaderImage IconID="setup_pagesetup_16x16">
        </HeaderImage>
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">

                <div class="col-md-12">
                    <dx:ASPxCallbackPanel runat="server" Width="100%" ID="panelRegistroendimientoPorcentual" ClientInstanceName="panelRegistroendimientoPorcentual" BorderBottom-BorderStyle="None" OnCallback="panelRegistroendimientoPorcentual_Callback">
                        <%--                        <ClientSideEvents EndCallback="function(s, e) {finRegistro();}" />--%>
                        <PanelCollection>
                            <dx:PanelContent ID="PanelContent2" runat="server">

                                <table>
                                    <tr>
                                        <td>
                                            <strong>Tipo Rendim. % Vol.</strong>
                                            <dx:ASPxComboBox ID="cmbTipRendimVol" runat="server" TextFormatString="{0} {1} {2}" EnableTheming="True" Font-Bold="True" Theme="Metropolis" TextField="NOM_AREA_REFIN" ValueType="System.String" Width="100%" ToolTip="Seleccione un Periodo" ClientInstanceName="cmbTipRendimVol" ClientSideEvents="cmbTipRendimVol" ValueField="ID_PARAMETRICA_GLOBAL" OnCallback="cmbTipRendimVol_Callback" AutoPostBack="False">
                                                <ClearButton Visibility="Auto"></ClearButton>
                                            </dx:ASPxComboBox>
                                            <strong>Rendim. % Vol.</strong>
                                            <dx:ASPxTextBox ID="txtVolumnenRendPorVol" runat="server" CssClass="form-control" Width="100%" ForeColor="Black" ClientInstanceName="txtVolumnenRendPorVol" ClientSideEvents="txtVolumnenRendPorVol">
                                            </dx:ASPxTextBox>

                                            <%--                                            <dx:ASPxSpinEdit ID="spVolumnenRendPorVol" Number="00.00" runat="server" DecimalPlaces="2" ClientInstanceName="spVolumnenRendPorVol" ClientSideEvents="spVolumnenRendPorVol">
                                                <ClearButton Visibility="Auto"></ClearButton>
                                            </dx:ASPxSpinEdit>--%>
                                        </td>
                                        <td>
                                            <dx:ASPxButton ID="btnAgregarRendPorcVolum" runat="server" Text=">>" CssClass="btn btn-xs btn-default pull-right" ToolTip="Agregar RENDIMIENTO % VOLUMETRICO" AutoPostBack="False">
                                                <ClientSideEvents Click="function(s, e) {
                OnLinClick_AgregarRendimientoPorcen();
}" />
                                            </dx:ASPxButton>
                                        </td>
                                        <td>
                                            <dx:ASPxGridView ID="grbListaRegistroRendPorVolum" runat="server" AutoGenerateColumns="False" ClientInstanceName="grbListaRegistroRendPorVolum" CssPostfix="DevGlp" EnableTheming="True" Font-Size="Smaller" KeyFieldName="ID_RENDIMIEN_VOLUM" OnCustomCallback="grbListaRegistroRendPorVolum_CustomCallback" Theme="Default" Width="100%">

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
                                                    <dx:GridViewDataTextColumn ShowInCustomizationForm="True" Visible="False" VisibleIndex="0" FieldName="ID_RENDIMIEN_VOLUM">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn ShowInCustomizationForm="True" Visible="False" VisibleIndex="0" FieldName="ID_RENDIMIEN">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn ShowInCustomizationForm="True" Visible="False" VisibleIndex="0" FieldName="ID_PRODUCTO">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="PROD." ShowInCustomizationForm="True" VisibleIndex="2" FieldName="PRODUCTO">
                                                        <PropertiesTextEdit DisplayFormatString="0.0">
                                                        </PropertiesTextEdit>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="VOL." ShowInCustomizationForm="True" VisibleIndex="3" FieldName="VOLUMEN">
                                                        <PropertiesTextEdit DisplayFormatString="0.0">
                                                        </PropertiesTextEdit>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="#" ShowInCustomizationForm="True" VisibleIndex="4">
                                                        <DataItemTemplate>
                                                            <a id="BotonEliminaItem" href="javascript:OnLinClick_EliminarElemtRendimientoPorcen(<%# Container.KeyValue.ToString()%>, <%# Container.VisibleIndex.ToString()%>);">
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
                                        </td>
                                    </tr>
                                </table>
                                <%--                            <div class="col-md-8">
                            </div>
                            <div class="col-md-4">
                                <div class="col-md-5">
                                </div>
                                <div class="col-md-1">
                                </div>
                                <div class="col-md-6">
                                </div>
                            </div>--%>
                            </dx:PanelContent>
                        </PanelCollection>
                        <BorderBottom BorderStyle="None"></BorderBottom>
                    </dx:ASPxCallbackPanel>
                </div>
                <div class="col-md-12">
                    <dx:ASPxButton ID="ASPxButton3" runat="server" Text="CANCELAR" CssClass="btn btn-xs btn-default pull-right" AutoPostBack="False">
                        <ClientSideEvents Click="function(s, e) {
                pucRegistroPorcentageRendimiento.Hide();
}" />
                    </dx:ASPxButton>
                    <dx:ASPxButton ID="ASPxButton4" runat="server" Text="REGISTRAR" CssClass="btn btn-xs btn-default pull-right" AutoPostBack="False">
                        <ClientSideEvents Click="function(s, e) {
 fRegistroRendimientoPorcentualVolum();
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
