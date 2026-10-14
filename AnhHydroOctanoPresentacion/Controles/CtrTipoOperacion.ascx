<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtrTipoOperacion.ascx.cs" Inherits="AnhHydroOctanoPresentacion.Controles.CtrTipoOperacion" %>

<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>

<style type="text/css">
    .auto-style11 {
        width: 168px;
        height: 26px;
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

    .alert-box {
        color: #555;
        border-radius: 10px;
        font-family: Tahoma,Geneva,Arial,sans-serif;
        font-size: 11px;
        padding: 10px 10px 10px 36px;
        margin: 10px;
    }

    .auto-style12 {
        width: 210px;
    }

    .auto-style14 {
        height: 27px;
    }

    .auto-style15 {
        border: 1px dotted transparent;
        padding-left: 7px;
        padding-right: 7px;
        padding-top: 2px;
        padding-bottom: 3px;
    }
</style>

<div class="col-md-12">
    <table style="width: 100%">
        <tr>
            <td style="text-align: center; padding-bottom: 1px">
                <dx:ASPxRoundPanel runat="server" ShowHeader="False" Width="100%" ID="pnlTitulo">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <dx:ASPxLabel runat="server" ForeColor="#367B2F" ID="lblTitulo" Font-Bold="True" Font-Names="Rockwell" Font-Size="Larger" Text="CONFIGURACIÓN DE TIPOS DE OPERACIONES"></dx:ASPxLabel>
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
                                    <dx:ASPxLabel ID="ASPxLabel58" runat="server" ForeColor="#003300" Text="Actividades Asignadas :">
                                    </dx:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="col-lg-12">
                                        <div class="btn-group btn-group-justified" role="group" aria-label="group button">
                                            <div class="btn-group" role="group">
                                                <dx:ASPxComboBox ID="cmbListaActividadesHydro" runat="server" ClientInstanceName="cmbListaActividadesHydro" TextField="DESCRIPCION" TextFormatString="{0}" ValueField="ID_TIPO_ACTIVIDAD" Width="150px">
                                                    <%--                                                    <Columns>
                                                        <dx:ListBoxColumn Caption="DESCRIPCION" FieldName="DESCRIPCION" Name="DESCRIPCION" Width="70%" />
                                                        <dx:ListBoxColumn Caption="MNEMONICO" FieldName="MNEMONICO" Name="MNEMONICO" Width="30%" />
                                                        <dx:ListBoxColumn FieldName="ID_TIPO_ACTIVIDAD" Name="ID_TIPO_ACTIVIDAD" Width="0px" Visible="False" />
                                                    </Columns>--%>
                                                    <ClearButton Visibility="Auto">
                                                    </ClearButton>
                                                    <ReadOnlyStyle BackColor="#FFE1C4">
                                                    </ReadOnlyStyle>
                                                    <FocusedStyle BackColor="#FFFFCC">
                                                    </FocusedStyle>
                                                    <DisabledStyle BackColor="#DEE8DB">
                                                    </DisabledStyle>
                                                </dx:ASPxComboBox>
                                            </div>
                                            <div class="btn-group" role="group">
                                                <dx:ASPxButton ID="btnAgregarActividad" runat="server" AutoPostBack="False" ClientInstanceName="btnAgregarActividad" Font-Bold="True" Text="Agregar Activiad" Theme="Metropolis" ForeColor="Maroon">
                                                    <ClientSideEvents Click="function(s, e) {tlsOperadores.PerformCallback('Agregar');}" />
                                                    <Image IconID="maps_inlinesizelegend_16x16">
                                                    </Image>
                                                </dx:ASPxButton>
                                            </div>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <script>
                                        //function accionEliminarActividad() {
                                        //    tlsOperadores.PerformCallback('Eliminar');

                                        //}
                                        function OnLinkClick_EliminarRegistro(id, visibleIndex) {
                                            var mensaje = confirm("¿Decea eliminar el registro?");
                                            if (mensaje) {
                                                //if (id != '') {
                                                var accion = "Eliminar-" + id;
                                                tlsOperadores.PerformCallback(accion);
                                                //}
                                            }
                                        }
                                        function OnLinkClick_ModificarEstado(id, visibleIndex) {
                                            var mensaje = confirm("¿Decea modificar el estado del registro?");
                                            if (mensaje) {
                                                //if (id != '') {
                                                var accion = "Modificar-" + id;
                                                tlsOperadores.PerformCallback(accion);
                                                //}
                                            }
                                        }
                                        function AlertaCallbakj(id, visibleIndex) {
                                            var mensaje = confirm("¿Decea modificar el estado del registro?");
                                            if (mensaje) {
                                                //if (id != '') {
                                                var accion = "Modificar-" + id;
                                                tlsOperadores.PerformCallback(accion);
                                                //}
                                            }
                                        }
                                    </script>
                                    <dx:ASPxCallbackPanel ID="panelBusquedaGlobals" runat="server" OnCallback="panelBusquedaGlobals_Callback" Width="100%" ClientInstanceName="panelBusquedaGlobals" >
                                        <PanelCollection>
                                            <dx:PanelContent runat="server">
                                                <asp:Panel ID="panelAlertaMensaje" runat="server" Visible="False">

                                                    <div class="alert alert-success alert-dismissible show" role="alert">
                                                        <asp:Label ID="lblErrorActividad" runat="server" Text="Label"></asp:Label>
                                                        <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                                            <span aria-hidden="true">&times;</span>
                                                        </button>
                                                    </div>
                                                </asp:Panel>
                                                <asp:Panel ID="panelErro" runat="server" Visible="False">

                                                    <div class="alert alert-danger alert-dismissible show" role="alert">
                                                        <asp:Label ID="lblAlertErro" runat="server" Text="Label"></asp:Label>
                                                        <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                                            <span aria-hidden="true">&times;</span>
                                                        </button>
                                                    </div>
                                                </asp:Panel>
                                            </dx:PanelContent>
                                        </PanelCollection>
                                    </dx:ASPxCallbackPanel>
                                    <dx:ASPxTreeList ID="tlsOperadores" runat="server" AutoGenerateColumns="False" ClientInstanceName="tlsOperadores" Font-Size="Smaller" Height="187px" Theme="Youthful" Styles-Header-BackColor="#317D40" OnCustomCallback="tlsOperadores_CustomCallback" OnHtmlDataCellPrepared="tlsOperadores_HtmlDataCellPrepared"
                                        KeyFieldName="ID_ACTIVIDAD">
                                        <Columns>
                                            <%--                                            <dx:TreeListTextColumn Caption="ID" FieldName="intId" ShowInCustomizationForm="True" Visible="False" VisibleIndex="0">
                                            </dx:TreeListTextColumn>
                                            <dx:TreeListTextColumn Caption="PADRE" FieldName="intIdPadre" ShowInCustomizationForm="True" Visible="False" VisibleIndex="1">
                                            </dx:TreeListTextColumn>
                                            <dx:TreeListTextColumn Caption="DESCRIPCION" FieldName="strDescripcion" ShowInCustomizationForm="True" VisibleIndex="3" Width="70%">
                                            </dx:TreeListTextColumn>
                                            <dx:TreeListTextColumn Caption="NIT" FieldName="lngNit" ShowInCustomizationForm="True" Visible="False" VisibleIndex="4">
                                            </dx:TreeListTextColumn>
                                            <dx:TreeListTextColumn Caption="LICENCIA" FieldName="lngLicencia" ShowInCustomizationForm="True" Visible="False" VisibleIndex="5">
                                            </dx:TreeListTextColumn>
                                            <dx:TreeListTextColumn Caption="REP_LEGAL" FieldName="strRepLegal" ShowInCustomizationForm="True" Visible="False" VisibleIndex="6">
                                            </dx:TreeListTextColumn>
                                            <dx:TreeListTextColumn Caption="DIRECCION" FieldName="strDireccion" ShowInCustomizationForm="True" Visible="False" VisibleIndex="7">
                                            </dx:TreeListTextColumn>--%>
                                            <dx:TreeListTextColumn FieldName="ID_ACTIVIDAD_OCT_VOL" ShowInCustomizationForm="True" Visible="False" VisibleIndex="8">
                                            </dx:TreeListTextColumn>
                                            <dx:TreeListTextColumn FieldName="ID_ACTIVIDAD" ShowInCustomizationForm="True" Visible="False" VisibleIndex="0">
                                            </dx:TreeListTextColumn>
                                            <dx:TreeListTextColumn Caption="DESCRIPCION" FieldName="DESCRIPCION" ShowInCustomizationForm="True" VisibleIndex="3" Width="70%" SortOrder="Ascending">
                                            </dx:TreeListTextColumn>
                                            <dx:TreeListTextColumn Caption="ID_ESTADO" FieldName="ID_ESTADO" ShowInCustomizationForm="True" Visible="False" VisibleIndex="7">
                                            </dx:TreeListTextColumn>
                                            <dx:TreeListTextColumn Caption="OPCIONES" VisibleIndex="2" Width="30%">
                                                <DataCellTemplate>
                                                    <%--class=" glyphicon glyphicon-edit" <asp:LinkButton ID="lnkEliminarItemGrilla" ToolTip="Eliminar Actividad" runat="server" class="btn btn-default btn-xs" role="button" CommandName="Eliminar" OnClientClick="accionEliminarActividad()" OnClick="lnkEliminarItemGrilla_Click"><span class="glyphicon glyphicon-trash" style="color:#2d7e3e  !important"></span></asp:LinkButton>--%>

                                                    <%--                                                    <a id="botonModificarEstado" href="javascript:OnLinkClick_ModificarEstado(<%# Container.NodeKey%>, <%# Container.ItemIndex.ToString()%>);">
                                                        <div class="btn btn-default btn-xs" data-toggle="tooltip" data-placement="top" title="Eliminar Actividad">
                                                            <span style="background-image: url(../../UI/img/margollo.png); color: #2d7e3e  !important"></span>
                                                        </div>
                                                    </a>
                                                    <a id="botonEliminar" href="javascript:OnLinkClick_EliminarRegistro(<%# Container.NodeKey%>, <%# Container.ItemIndex.ToString()%>);">
                                                        <div class="btn btn-default btn-xs" data-toggle="tooltip" data-placement="top" title="Eliminar Actividad"><span class=" glyphicon glyphicon-trash" style="color: #2d7e3e  !important"></span></div>
                                                    </a>--%>

                                                    <a id="botonModificarEstado" href="javascript:OnLinkClick_ModificarEstado(<%# Container.NodeKey%>, <%# Container.ItemIndex.ToString()%>);">
                                                        <div class="btn btn-default btn-xs" data-toggle="tooltip" data-placement="top" title="Modificar Estado">
                                                            <img src="../../UI/img/icono_modificarEstado.png" alt="Mi titulo de la imagen">
                                                            <span></span>
                                                        </div>
                                                    </a>
                                                    <a id="botonEliminar" href="javascript:OnLinkClick_EliminarRegistro(<%# Container.NodeKey%>, <%# Container.ItemIndex.ToString()%>);">
                                                        <div class="btn btn-default btn-xs" data-toggle="tooltip" data-placement="top" title="Eliminar Actividad">
                                                            <img src="../../UI/img/icono_eliminar_registro.png" alt="Mi titulo de la imagen">
                                                            <%--                                                            <span class=" glyphicon glyphicon-trash" style="color: #2d7e3e  !important"></span>--%>
                                                        </div>
                                                    </a>
                                                </DataCellTemplate>
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
	panelBusquedaGlobals.PerformCallback();
}" />
                                        <%--                                        <asp:Panel ID="PanErrorForm" runat="server">
                                            <asp:Label ID="LblError" runat="server" ForeColor="#FF3300" Text="123">                                 </asp:Label>
                                        </asp:Panel>--%>
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
                            }"
                                BeginCallback="function(s, e) {
	                            LoadingPanel.Show();
                            }" />
                            <PanelCollection>
                                <dx:PanelContent runat="server">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td colspan="7" style="text-align: center">
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
                                                    <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="Rockwell" Font-Size="Medium" ForeColor="#367B2F" Text="LISTADO DE TIPOS DE OPERACION"></asp:Label>
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
                                            <td colspan="4" style="width: 80%">
                                                <asp:Label ID="lblActividad" runat="server" Font-Bold="False"></asp:Label>
                                            </td>
                                            <td colspan="2" style="width: 1%">&nbsp;</td>
                                            <td style="text-align: right; width: 19%">
                                                <%--<dx:ASPxButton ID="btnFormatoDj" runat="server" AutoPostBack="False" ClientInstanceName="btnFormatoDj" Font-Bold="True" Text="Formato de la DDJJ" Theme="Metropolis" Width="170px" ForeColor="Maroon">--%>
                                                <dx:ASPxButton ID="btnFormatoDj" runat="server" AutoPostBack="False" ClientInstanceName="btnFormatoDj" Font-Bold="True" Text="Registro Operacion actividad" Theme="Metropolis" Width="170px" ForeColor="Maroon">
                                                    <ClientSideEvents Click="function(s, e) {
pucFormRegistroOperacionActividad.Show();
pucFormRegistroOperacionActividad.PerformCallback();
}
" />
                                                    <Image IconID="maps_inlinesizelegend_16x16">
                                                    </Image>
                                                </dx:ASPxButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <%--txtId.SetValue(1);--%>
                                            <td colspan="7">
                                                <dx:ASPxGridView ID="dgvTipoOperacion" runat="server" Theme="Default" AutoGenerateColumns="False" KeyFieldName="ID_TIPO_OPERACION" EnableTheming="True" Font-Size="Smaller" ClientInstanceName="dgvTipoOperacion" OnHtmlRowPrepared="dgvTipoOperacion_HtmlRowPrepared" Width="100%" OnCustomButtonCallback="dgvTipoOperacion_CustomButtonCallback" OnCustomButtonInitialize="dgvTipoOperacion_CustomButtonInitialize">
                                                    <ClientSideEvents CustomButtonClick="function(s, e) {
switch(e.buttonID) {

case&nbsp; 'btnModificar':
pucConfig.Show();

pucConfig.PerformCallback();
break;

case&nbsp; 'btnEliminar':
e.processOnServer = confirm('Atención. Una vez eliminado el registro no podrá deshacer el proceso. ¿Desea eliminar el tipo de operación?'); 
break;

case&nbsp; 'btnActivar':
e.processOnServer = confirm('Atención. Una vez activado el registro aparecera en todos los procesos de la actividad. ¿Desea activar el tipo de operación?'); 
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
                                                        <EditForm>
                                                        </EditForm>
                                                    </Templates>
                                                    <SettingsPager NumericButtonCount="3" PageSize="30">
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
                                                        <dx:GridViewCommandColumn ShowInCustomizationForm="True" VisibleIndex="21" ShowClearFilterButton="True" ToolTip="Funciones">
                                                            <CustomButtons>
                                                                <dx:GridViewCommandColumnCustomButton ID="btnModificar" Text=" " Visibility="Invisible">
                                                                    <Image IconID="edit_edit_16x16" ToolTip="Modificar">
                                                                    </Image>
                                                                </dx:GridViewCommandColumnCustomButton>
                                                                <dx:GridViewCommandColumnCustomButton ID="btnDesactivar" Text="Desactivar" Visibility="Invisible">
                                                                </dx:GridViewCommandColumnCustomButton>
                                                                <dx:GridViewCommandColumnCustomButton ID="btnEliminar" Text=" ">
                                                                    <Image IconID="reports_deletegroupfooter_16x16" ToolTip="Baja Registro">
                                                                    </Image>
                                                                </dx:GridViewCommandColumnCustomButton>
                                                                <dx:GridViewCommandColumnCustomButton ID="btnActivar" Text=" ">
                                                                    <Image IconID="content_checkbox_16x16" ToolTip="Activar Registro">
                                                                    </Image>
                                                                </dx:GridViewCommandColumnCustomButton>
                                                            </CustomButtons>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <CellStyle ForeColor="#006600">
                                                            </CellStyle>
                                                            <HeaderTemplate>
                                                                <dx:ASPxHyperLink ID="hlkNuevo" runat="server" Font-Overline="False" Font-Size="Small" Font-Underline="True" Text="" Visible="False" Enabled="False">
                                                                    <ClientSideEvents Click="function(s, e) {

pucConfig.Show()
txtId.SetValue(0);
pucConfig.PerformCallback();

}" />
                                                                </dx:ASPxHyperLink>
                                                            </HeaderTemplate>
                                                        </dx:GridViewCommandColumn>
                                                        <dx:GridViewDataTextColumn Caption="DESCRIPCION" FieldName="NOMBRE" ShowInCustomizationForm="True" VisibleIndex="2">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="ORDEN" FieldName="ORDEN" ShowInCustomizationForm="True" VisibleIndex="3">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="FACTOR_CALCULO" FieldName="FACTOR_CALCULO" ShowInCustomizationForm="True" VisibleIndex="5" Visible="False">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="TIPO" FieldName="STRTIPO" ShowInCustomizationForm="True" VisibleIndex="4">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="LIMITE MINIMO" FieldName="LIMITE_MINIMO" ShowInCustomizationForm="True" VisibleIndex="6">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="ID" FieldName="ID_TIPO_OPERACION" ShowInCustomizationForm="True" VisibleIndex="0" ToolTip="Identificador del Tipo de Operación">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="CODIGO" FieldName="CODIGO" ShowInCustomizationForm="True" VisibleIndex="1">
                                                            <Settings AutoFilterCondition="Contains" />
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="LIMITE MAXIMO" FieldName="LIMITE_MAXIMO" ShowInCustomizationForm="True" VisibleIndex="7">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="ID_REPORTE_VARIABLE" FieldName="ID_REPORTE_VARIABLE" ShowInCustomizationForm="True" Visible="False" VisibleIndex="8">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="CODIGO_REPORTE_VARIABLE" FieldName="CODIGO_REPORTE_VARIABLE" ShowInCustomizationForm="True" Visible="False" VisibleIndex="9">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="AUD_ESTADO" FieldName="AUD_ESTADO" ShowInCustomizationForm="True" Visible="False" VisibleIndex="11">
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                    <Styles CssPostfix="DevGlp">
                                                        <AlternatingRow BackColor="#EEEEEE">
                                                        </AlternatingRow>
                                                    </Styles>
                                                    <TotalSummary>
                                                        <dx:ASPxSummaryItem FieldName="F_IMPORTE" ShowInColumn="IMPORTE" SummaryType="Custom" Tag="imp" />
                                                        <dx:ASPxSummaryItem FieldName="VOLUMEN" ShowInColumn="CANTIDAD" SummaryType="Custom" Tag="vol" />
                                                        <dx:ASPxSummaryItem FieldName="STR_ESTADO" ShowInColumn="EST." SummaryType="Custom" Tag="est" />
                                                    </TotalSummary>
                                                </dx:ASPxGridView>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td class="auto-style11">&nbsp;</td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
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




<dx:ASPxPopupControl ID="pucConfig" runat="server" AllowDragging="True" ClientInstanceName="pucConfig" EnableTheming="True" HeaderText="Modificación" Height="93px" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Above" Theme="Metropolis" Width="718px" OnWindowCallback="pucConfig_WindowCallback">
    <HeaderImage IconID="chart_addchartpane_32x32">
    </HeaderImage>
    <HeaderStyle>
        <BorderBottom BorderStyle="Solid" />
    </HeaderStyle>
    <ContentCollection>
        <dx:PopupControlContentControl runat="server">
            <table class="dxflInternalEditorTable_Youthful">
                <tr>
                    <td style="text-align: right;">
                        <dx:ASPxLabel ID="ASPxLabel45" runat="server" Text="ID :" Font-Bold="True" ForeColor="#666666" Visible="False">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="txtId" runat="server" Width="150px" ClientInstanceName="txtId" Text="0" BackColor="#FFE1C4" ReadOnly="True" Visible="False">
                            <MaskSettings Mask="&lt;0..999999999999999&gt;" />
                            <DisabledStyle BackColor="#DEE8DB">
                            </DisabledStyle>
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">
                        <dx:ASPxLabel ID="ASPxLabel44" runat="server" Text="Código :" Font-Bold="True" ForeColor="#666666">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="txtCodigo" runat="server" Width="150px" ClientInstanceName="txtCodigo" MaxLength="6">
                            <ClientSideEvents LostFocus="function(s, e) {
	 s.SetText(s.GetText().toUpperCase());
}" />
                            <ValidationSettings SetFocusOnError="True">
                                <RequiredField ErrorText="Incoherencia de datos. Introduzca el código." IsRequired="True" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">
                        <dx:ASPxLabel ID="ASPxLabel40" runat="server" Text="Descripción :" Font-Bold="True" ForeColor="#666666">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="txtDescripcion" runat="server" Width="100%" ClientInstanceName="tstDescripcion">
                            <ClientSideEvents LostFocus="function(s, e) {
	 s.SetText(s.GetText().toUpperCase());
}" />
                            <ValidationSettings SetFocusOnError="True">
                                <RequiredField ErrorText="Introduzca la descripción." IsRequired="True" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">
                        <dx:ASPxLabel ID="ASPxLabel51" runat="server" Font-Bold="True" ForeColor="#666666" Text="Tipo de Movimiento :">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxRadioButtonList ID="rbtTipo" runat="server" ClientInstanceName="rbtTipo" SelectedIndex="0" ValueType="System.Int32">
                            <ClientSideEvents ValueChanged="function(s, e) {
	cmbReporteVariable.PerformCallback();

}" />
                            <Items>
                                <dx:ListEditItem Selected="True" Text="Ingreso" Value="1" />
                                <dx:ListEditItem Text="Egreso" Value="-1" />
                                <dx:ListEditItem Text="Neutro" Value="0" />
                            </Items>
                        </dx:ASPxRadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">
                        <dx:ASPxLabel ID="ASPxLabel52" runat="server" Font-Bold="True" ForeColor="#666666" Text="Orden :">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxSpinEdit ID="spnOrden" runat="server" ClientInstanceName="spnOrden" MaxValue="20" MinValue="1" Number="1" Width="60px">
                            <ClearButton Visibility="Auto">
                            </ClearButton>
                        </dx:ASPxSpinEdit>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">
                        <dx:ASPxLabel ID="ASPxLabel53" runat="server" Font-Bold="True" ForeColor="#666666" Text="Límite Mínimo :" Visible="False">
                        </dx:ASPxLabel>
                    </td>
                    <td class="auto-style14">
                        <dx:ASPxTextBox ID="txtLimiteMin" runat="server" ClientInstanceName="txtLimiteMin" Text="0" Width="150px" Visible="False">
                            <MaskSettings Mask="&lt;0..9999999&gt;.&lt;00..99&gt;" />
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">
                        <dx:ASPxLabel ID="ASPxLabel54" runat="server" Font-Bold="True" ForeColor="#666666" Text="Límite Máximo :" Visible="False">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="txtLimiteMax" runat="server" ClientInstanceName="txtLimiteMax" Text="0" Width="150px" Visible="False">
                            <MaskSettings Mask="&lt;0..9999999&gt;.&lt;00..99&gt;" />
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <%--                <tr>
                    <td class="auto-style12">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>--%>
                <tr>
                    <td class="auto-style12">
                        <dx:ASPxLabel ID="ASPxLabel46" runat="server" Font-Bold="True" Font-Size="Smaller" Font-Underline="True" ForeColor="#333333" Text="FORMATO P/DECLARACION JURADA" Visible="False">
                        </dx:ASPxLabel>
                    </td>
                    <%--<td>&nbsp;</td>--%>
                </tr>
                <tr>
                    <td style="text-align: right;">
                        <dx:ASPxLabel ID="ASPxLabel55" runat="server" Font-Bold="True" ForeColor="#666666" Text="Columna de Visualización :" Visible="False">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxComboBox ID="cmbReporteVariable" runat="server" ClientInstanceName="cmbReporteVariable" OnCallback="cmbReporteVariable_Callback" TextField="CODIGO" TextFormatString="{0}" ValueField="iD_REPORTE_VARIABLE" Width="200px" Visible="False">
                            <Columns>
                                <dx:ListBoxColumn Caption="CODIGO" FieldName="CODIGO" Name="CODIGO" />
                                <dx:ListBoxColumn Caption="NOMBRE COLUMNA" FieldName="NOMBRE" Name="NOMBRE" />
                                <dx:ListBoxColumn Caption="iD_REPORTE_VARIABLE" FieldName="iD_REPORTE_VARIABLE" Name="iD_REPORTE_VARIABLE" Width="0px" />
                            </Columns>
                            <ClearButton Visibility="Auto">
                            </ClearButton>
                            <ValidationSettings>
                                <RequiredField ErrorText="ERROR : Introduzca la forma de pago." />
                            </ValidationSettings>
                            <ReadOnlyStyle BackColor="#FFE1C4">
                            </ReadOnlyStyle>
                            <FocusedStyle BackColor="#FFFFCC">
                            </FocusedStyle>
                            <DisabledStyle BackColor="#DEE8DB">
                            </DisabledStyle>
                        </dx:ASPxComboBox>
                    </td>
                </tr>
                <%--                <tr>
                    <td style="text-align: right;">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>--%>
                <tr>
                    <td colspan="2">
                        <dx:ASPxButton ID="btnGrabar" runat="server" EnableTheming="True" Text="Guardar Configuración" Theme="Metropolis" Width="100%" OnClick="btnGrabar_Click" Font-Bold="True" ForeColor="Maroon">
                            <Image IconID="actions_apply_16x16">
                            </Image>
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>

<dx:ASPxPopupControl ID="pucFormatoDj" runat="server" AllowDragging="True" ClientInstanceName="pucFormatoDj" EnableTheming="True" HeaderText="Formato de la Declaración Jurada" Height="93px" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Above" Theme="Metropolis" Width="718px" OnWindowCallback="pucFormatoDj_WindowCallback">
    <HeaderImage IconID="maps_inlinesizelegend_32x32">
    </HeaderImage>
    <HeaderStyle>
        <BorderBottom BorderStyle="Solid" />
    </HeaderStyle>
    <ContentCollection>
        <dx:PopupControlContentControl runat="server">
            <table class="dxflInternalEditorTable_Youthful">
                <tr>
                    <td style="padding-right: 2px">
                        <dx:ASPxLabel ID="ASPxLabel68" runat="server" Text="ACTIVIDAD" Font-Bold="True" ForeColor="#333333" Font-Size="Smaller" Font-Underline="True">
                        </dx:ASPxLabel>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: right;">
                        <dx:ASPxLabel ID="ASPxLabel59" runat="server" Font-Bold="True" ForeColor="#666666" Text="ID :">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="txtPfIdActividad" runat="server" BackColor="#FFE1C4" ClientInstanceName="txtPfIdActividad" ReadOnly="True" Text="0" Width="150px">
                            <MaskSettings Mask="&lt;0..999999999999999&gt;" />
                            <DisabledStyle BackColor="#DEE8DB">
                            </DisabledStyle>
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">
                        <dx:ASPxLabel ID="ASPxLabel61" runat="server" Text="Descripción :" Font-Bold="True" ForeColor="#666666">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="txtPfActividad" runat="server" Width="100%" ClientInstanceName="txtPfActividad" AutoResizeWithContainer="True" ReadOnly="True">
                            <ClientSideEvents LostFocus="function(s, e) {
	 s.SetText(s.GetText().toUpperCase());
}" />
                            <ValidationSettings>
                                <RequiredField ErrorText="" />
                            </ValidationSettings>
                            <ReadOnlyStyle BackColor="#FFE1C4">
                            </ReadOnlyStyle>
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">
                        <dx:ASPxLabel ID="ASPxLabel70" runat="server" Font-Bold="True" ForeColor="#666666" Text="Variables Rep. DDJJ :">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxSpinEdit ID="spnNoIngreso" runat="server" MaxValue="5" Number="5">
                            <ClearButton Visibility="Auto">
                            </ClearButton>
                        </dx:ASPxSpinEdit>
                        <dx:ASPxSpinEdit ID="spnNoEgreso" runat="server" MaxValue="5" Number="5">
                            <ClearButton Visibility="Auto">
                            </ClearButton>
                        </dx:ASPxSpinEdit>
                        <dx:ASPxButton ID="btnIniciaAct" runat="server" AutoPostBack="False" EnableTheming="True" OnClick="btnIniciaAct_Click" Text="Inicia Actividad" Theme="Metropolis">
                            <ClientSideEvents Click="function(s, e) {
pucFormatoDj.Hide();	
}" />
                        </dx:ASPxButton>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style12">
                        <dx:ASPxLabel ID="ASPxLabel66" runat="server" Font-Bold="True" Font-Size="Smaller" Font-Underline="True" ForeColor="#333333" Text="TITULOS DEL REPORTE ACUMULADO">
                        </dx:ASPxLabel>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: right;">
                        <dx:ASPxLabel ID="ASPxLabel67" runat="server" Font-Bold="True" ForeColor="#666666" Text="Columna de Visualización :">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxComboBox ID="cmbPfColumnas" runat="server" ClientInstanceName="cmbPfColumnas" Font-Size="Smaller" OnCallback="cmbReporteVariable_Callback" TextField="NOMBRE" TextFormatString="{0}" ValueField="CODIGO" Width="250px">
                            <ClientSideEvents SelectedIndexChanged="function(s, e) {

try
{

var titulo = s.GetSelectedItem();

if(titulo)
{
	txtPfIdColumna.SetValue(titulo.GetColumnText('iD_REPORTE_VARIABLE'));
	txtPfColumna.SetValue(titulo.GetColumnText('NOMBRE'));

}

}
catch(e)
{

}

}" />
                            <Columns>
                                <dx:ListBoxColumn Caption="CODIGO" FieldName="CODIGO" Name="CODIGO" />
                                <dx:ListBoxColumn Caption="NOMBRE" FieldName="NOMBRE" Name="NOMBRE" />
                                <dx:ListBoxColumn Caption="iD_REPORTE_VARIABLE" FieldName="iD_REPORTE_VARIABLE" Name="iD_REPORTE_VARIABLE" Width="0px" />
                            </Columns>
                            <ClearButton Visibility="Auto">
                            </ClearButton>
                            <ValidationSettings>
                                <RequiredField ErrorText="Seleccione una opción." />
                            </ValidationSettings>
                            <ReadOnlyStyle BackColor="#FFE1C4">
                            </ReadOnlyStyle>
                            <FocusedStyle BackColor="#FFFFCC">
                            </FocusedStyle>
                            <DisabledStyle BackColor="#DEE8DB">
                            </DisabledStyle>
                        </dx:ASPxComboBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">
                        <dx:ASPxLabel ID="ASPxLabel60" runat="server" Font-Bold="True" ForeColor="#666666" Text="Id :">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="txtPfIdColumna" runat="server" ClientInstanceName="txtPfIdColumna" MaxLength="6" Width="150px" ReadOnly="True">
                            <ClientSideEvents LostFocus="function(s, e) {
	 s.SetText(s.GetText().toUpperCase());
}" />
                            <ValidationSettings>
                                <RequiredField ErrorText="Seleccione una opción." />
                            </ValidationSettings>
                            <ReadOnlyStyle BackColor="#FFE1C4">
                            </ReadOnlyStyle>
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">
                        <dx:ASPxLabel ID="ASPxLabel69" runat="server" Font-Bold="True" ForeColor="#666666" Text="Título :">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="txtPfColumna" runat="server" ClientInstanceName="txtPfColumna" MaxLength="20" Width="250px">
                            <ClientSideEvents LostFocus="function(s, e) {
	 s.SetText(s.GetText().toUpperCase());
}" />
                            <ValidationSettings SetFocusOnError="True">
                                <RequiredField ErrorText="Introduzca un título para la columna." />
                            </ValidationSettings>
                            <ReadOnlyStyle BackColor="#FFE1C4">
                            </ReadOnlyStyle>
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style12">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2">
                        <dx:ASPxButton ID="btnPfGrabar" runat="server" EnableTheming="True" Text="Guardar Título de la Columna" Theme="Metropolis" Width="100%" OnClick="btnPfGrabar_Click" Font-Bold="True" ForeColor="Maroon">
                            <Image IconID="actions_apply_16x16">
                            </Image>
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>

<dx:ASPxPopupControl ID="pucFormRegistroOperacionActividad" runat="server" AllowDragging="True" ClientInstanceName="pucFormRegistroOperacionActividad" EnableTheming="True" HeaderText="Registro" Height="93px" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Above" Theme="Metropolis" Width="718px" OnWindowCallback="pucFormRegistroOperacionActividad_WindowCallback">
    <HeaderImage IconID="chart_addchartpane_32x32">
    </HeaderImage>
    <HeaderStyle>
        <BorderBottom BorderStyle="Solid" />
    </HeaderStyle>
    <ContentCollection>
        <dx:PopupControlContentControl runat="server">
            <table class="dxflInternalEditorTable_Youthful">
                <tr>
                    <td style="text-align: right;">
                        <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="Código :" Font-Bold="True" ForeColor="#666666">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="txtCodigoRegOperacionActividad" runat="server" Width="150px" ClientInstanceName="txtCodigoRegOperacionActividad" MaxLength="6">
                            <ClientSideEvents LostFocus="function(s, e) {
	 s.SetText(s.GetText().toUpperCase());
}" />
                            <ValidationSettings SetFocusOnError="True">
                                <RequiredField ErrorText="Incoherencia de datos. Introduzca el código." IsRequired="True" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">
                        <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="Descripción :" Font-Bold="True" ForeColor="#666666">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="txtDescripcionOperacionActividad" runat="server" Width="100%" ClientInstanceName="txtDescripcionOperacionActividad">
                            <ClientSideEvents LostFocus="function(s, e) {
	 s.SetText(s.GetText().toUpperCase());
}" />
                            <ValidationSettings SetFocusOnError="True">
                                <RequiredField ErrorText="Introduzca la descripción." IsRequired="True" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">
                        <dx:ASPxLabel ID="ASPxLabel4" runat="server" Font-Bold="True" ForeColor="#666666" Text="Tipo de Movimiento :">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxRadioButtonList ID="rbRegOpActivTipoMovimiento" runat="server" ClientInstanceName="rbRegOpActivTipoMovimiento" SelectedIndex="0" ValueType="System.Int32">
                            <%--                            <ClientSideEvents ValueChanged="function(s, e) {
	panelspinEdit.PerformCallback();

}" />--%>
                            <Items>
                                <dx:ListEditItem Selected="True" Text="Ingreso" Value="1" />
                                <dx:ListEditItem Text="Egreso" Value="-1" />
                                <dx:ListEditItem Text="Neutro" Value="0" />
                            </Items>
                        </dx:ASPxRadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">
                        <dx:ASPxLabel ID="ASPxLabel5" runat="server" Font-Bold="True" ForeColor="#666666" Text="Orden :">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxCallbackPanel runat="server" Width="100%" ID="panelspinEdit" ClientInstanceName="panelspinEdit" BorderBottom-BorderStyle="None" OnCallback="panelspinEdit_Callback">
                            <PanelCollection>
                                <dx:PanelContent ID="PanelContent3" runat="server">

                                    <dx:ASPxSpinEdit ID="spRegisOpeActivListaOrden" runat="server" ClientInstanceName="spRegisOpeActivListaOrden" MaxValue="20" MinValue="1" Number="1" Width="60px">
                                        <ClearButton Visibility="Auto">
                                        </ClearButton>
                                    </dx:ASPxSpinEdit>

                                </dx:PanelContent>
                            </PanelCollection>
                            <BorderBottom BorderStyle="None"></BorderBottom>
                        </dx:ASPxCallbackPanel>

                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">
                        <dx:ASPxLabel ID="ASPxLabel6" runat="server" Font-Bold="True" ForeColor="#666666" Text="Codigo Fformulario:">
                        </dx:ASPxLabel>
                    </td>
                    <td class="auto-style14">
                        <dx:ASPxTextBox ID="txtTextoNomFormuario" runat="server" ClientInstanceName="txtTextoNomFormuario" Text="UD/AB-RDJ-P01-F01" Width="150px">
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style12">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: right;">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2">
                        <dx:ASPxButton ID="btnRegistrarOperacionActividad" runat="server" EnableTheming="True" Text="Guardar Configuración" Theme="Metropolis" Width="100%" Font-Bold="True" ForeColor="Maroon" OnClick="btnRegistrarOperacionActividad_Click">
                            <Image IconID="actions_apply_16x16">
                            </Image>
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>
