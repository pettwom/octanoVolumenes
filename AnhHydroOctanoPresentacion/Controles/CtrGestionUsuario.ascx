<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtrGestionUsuario.ascx.cs" Inherits="AnhHydroOctanoPresentacion.Controles.CtrGestionUsuario" %>
<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>


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
    


.dxgvTable_Youthful
{
	background-color: #FFFFFF!important;
	border-width: 0px;
	border-collapse: separate!important;
	overflow: hidden;
} 
.dxgvHeader_Youthful
{
	cursor: pointer;
	white-space: nowrap;
	padding: 4px;
	border: 1px Solid White;
	background-color: #9CC525;
	overflow: hidden;
	font-weight: normal;
	text-align: left;
}

.dxgvFocusedRow_Youthful
{
	background-color: #F4921B;
	color: #FFFFFF;
}

.dxgvPagerTopPanel_Youthful,
.dxgvPagerBottomPanel_Youthful
{
	padding: 1px 0;
}

    .auto-style1 {
        height: 22px;
    }
    
    </style>

<script type="text/javascript">
    function rblistComment_SelectIndexChanged(s, e) {
        
        var a = rblTipoAccion.GetValue();        
        if (a == 4)
        { }

    }
                    </script>

<div class="col-md-12">
    <table style="width: 100%">
        <tr>
            <td style="text-align: center; padding-bottom: 1px">
                <dx:ASPxRoundPanel runat="server" ShowHeader="False" Width="100%" ID="pnlTitulo">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <dx:ASPxLabel runat="server" ForeColor="#367B2F" ID="lblTitulo" Font-Bold="True" Font-Names="Rockwell" Font-Size="Larger" Text="GESTION DE USUARIOS"></dx:ASPxLabel>
                    </dx:PanelContent>
                </PanelCollection>
                <Border BorderStyle="Ridge" BorderWidth="1px" BorderColor="#003300"></Border>
                </dx:ASPxRoundPanel>
            </td>
        </tr>
    </table>

    <dx:ASPxSplitter ID="ASPxSplitter1" runat="server" Height="100%">
        <Panes>
            <dx:SplitterPane Size="20%" AllowResize="True" AutoHeight="True">
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
                                    
                                    <dx:ASPxComboBox ID="cmbModulo" ClientInstanceName="cmbModulo" runat="server" EnableTheming="True" Theme="Metropolis" ValueField="ID_MODULO" ValueType="System.Int32" Width="73%">
                                        <Columns>
                                            <dx:ListBoxColumn Caption="ID_MODULO" FieldName="ID_MODULO" Visible="False" />
                                            <dx:ListBoxColumn Caption="NOMBRE" FieldName="NOMBRE" />
                                        </Columns>
                                        <ClearButton Visibility="Auto">
                                        </ClearButton>
                                    </dx:ASPxComboBox>
                                    
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <dx:ASPxTextBox ID="txtActividad" runat="server" Width="73%">
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <dx:ASPxTextBox ID="txtIdUsuario" runat="server" Width="73%">
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <dx:ASPxButton ID="btnRecargaArbol" runat="server" OnClick="btnRecargaArbol_Click" Text="Recargar Arbol" Font-Bold="True" ForeColor="White" BackColor="#307D40" Width="73%">
                                        <Image IconID="actions_refresh2_16x16">
                                        </Image>
                                    </dx:ASPxButton>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-top: 5px;padding-bottom: 5px">
                                    <dx:ASPxLabel ID="ASPxLabel63" runat="server" Font-Bold="True" Font-Names="Rockwell" ForeColor="#487B5C" Width="100%">
                                        <BorderBottom BorderStyle="Solid" BorderWidth="2px" />
                                    </dx:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <dx:ASPxButton ID="btnNuevo" runat="server" AutoPostBack="False" Font-Bold="True" ForeColor="Maroon" Text="Asignación Por Usuario" Theme="Metropolis" Width="73%">
                                        <ClientSideEvents Click="function(s, e) { 
                                            pucAsignacion.Show();
}" />
                                        <Image IconID="people_customer_16x16">
                                        </Image>
                                    </dx:ASPxButton>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-top: 5px;padding-bottom: 5px">
                                    <dx:ASPxLabel ID="ASPxLabel64" runat="server" Font-Bold="True" Font-Names="Rockwell" ForeColor="#487B5C" Width="100%">
                                        <BorderBottom BorderStyle="Solid" BorderWidth="2px" />
                                    </dx:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <dx:ASPxLabel ID="ASPxLabel58" runat="server" Font-Names="Arial Narrow" ForeColor="#003300" Text="PROYECTOS ASIGNADOS">
                                    </dx:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <dx:ASPxLabel ID="ASPxLabel65" runat="server" ForeColor="#003300" Text="Orden :">
                                    </dx:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <dx:ASPxRadioButtonList ID="rbtTipoOrden" runat="server" EnableTheming="True" SelectedIndex="0" Theme="Metropolis" ValueType="System.Int32" Width="73%">
                                        <Items>
                                            <dx:ListEditItem Selected="True" Text="EMPRESA" Value="1" />
                                            <dx:ListEditItem Text="ACTIVIDAD" Value="3" />
                                        </Items>
                                    </dx:ASPxRadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-top: 5px">
                                    <dx:ASPxButton ID="btnActualizar" runat="server" AutoPostBack="False" Font-Bold="True" ForeColor="Maroon" Text="Ordenar Proyectos" Theme="Metropolis" Width="73%" OnClick="btnActualizar_Click">
                                        <Image IconID="actions_download_16x16">
                                        </Image>
                                    </dx:ASPxButton>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-top: 5px">
                                    <dx:ASPxTextBox ID="txtBuscador" runat="server" BackColor="#FFEFDF" ClientInstanceName="txtBuscador" Font-Size="Smaller" Theme="Metropolis" ToolTip="Buscador" Width="100%">
                                        <ClientSideEvents LostFocus="function(s, e) {
	tlsOperadores.PerformCallback();
}" />
                                        <BorderTop BorderColor="#FF6600" BorderStyle="Solid" />
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <dx:ASPxTreeList ID="tlsOperadores" runat="server" AutoGenerateColumns="False" ClientInstanceName="tlsOperadores" Font-Size="Smaller" Height="187px" KeyFieldName="intId" OnCustomCallback="tlsOperadores_CustomCallback" ParentFieldName="intIdPadre" Theme="Youthful" Styles-Header-BackColor="#317D40" ToolTip="Seleccione una entidad">
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
                                        <SettingsBehavior AllowFocusedNode="True" FocusNodeOnExpandButtonClick="False" FocusNodeOnLoad="False" />
                                        <SettingsSelection Recursive="True" />
                                        <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
                                        <ClientSideEvents NodeDblClick="function(s, e) {
pnlDatos.PerformCallback();
}
" />
                                    </dx:ASPxTreeList>
                                </td>
                            </tr>
                        </table>
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
                                            <td colspan="7">
                                                <legend>
                                                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Rockwell" Font-Size="Medium" ForeColor="#367B2F" Text="DATOS DEL PROYECTO"></asp:Label>
                                                </legend>
                                            </td>
                                        </tr>
                                        <tr style="width: 100%">
                                            <td colspan="3" style="width: 35%">
                                                <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="#10420F" Text="NOMBRE EMPRESA"></asp:Label>
                                            </td>
                                            <td colspan="2" style="width: 20%">
                                                <asp:Label ID="Label5" runat="server" Font-Bold="True" ForeColor="#10420F" Text="ACTIVIDAD"></asp:Label>
                                            </td>
                                            <td style="width: 35%">
                                                <asp:Label ID="Label4" runat="server" Font-Bold="True" ForeColor="#10420F" Text="PROYECTO"></asp:Label>
                                            </td>
                                            <td style="width: 10%; text-align: right">
                                                
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <asp:Label ID="lblEmpresa" runat="server"></asp:Label>
                                            </td>
                                            <td colspan="2">
                                                <asp:Label ID="lblActividad" runat="server"></asp:Label>
                                            </td>
                                            <td colspan="2">
                                                <asp:Label ID="lblProyecto" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="7">
                                                <dx:ASPxGridView ID="dgvCabecera" runat="server" AutoGenerateColumns="False" ClientInstanceName="dgvCabecera" EnableTheming="True" Font-Size="Smaller" KeyFieldName="ID_TABLA" Theme="Default" Width="100%" OnHtmlRowPrepared="dgvCabecera_HtmlRowPrepared" OnCustomButtonCallback="dgvCabecera_CustomButtonCallback">
                                                    <ClientSideEvents CustomButtonClick="function(s, e) {

switch(e.buttonID) {
case  'btnQuitar':

if(confirm('¿Eliminar la asignación de usuario? - Una vez realizada la acción no podrá revertir el proceso. ¿Desea continuar?'))
{
s.GetRowValues(e.visibleIndex, 'TIPO_USUARIO', function (value) {
if(value==1)
{
alert('ATENCION. No puede eliminar un usuario de tipo PRINCIPAL.');
}
});
e.processOnServer = true;

}
else{
e.processOnServer = false;
}


//if(confirm('¿Eliminar la asignación de usuario? - Una vez realizada la acción no podrá revertir el proceso. //¿Desea continuar?'))
//{
//s.GetRowValues(e.visibleIndex, 'TIPO_USUARIO', function (value) {
//if(value==1)
//{
//alert('ATENCION. No puede eliminar un usuario de tipo PRINCIPAL.');
//}
//});
//e.processOnServer = true;
//
//}
//else{
//e.processOnServer = false;
//}

break;
} 
}" />
                                                    <SettingsContextMenu EnableRowMenu="False">
                                                    </SettingsContextMenu>
                                                    <Templates>
                                                        <EditForm>
                                                        </EditForm>
                                                    </Templates>
                                                    <SettingsPager NumericButtonCount="2">
                                                        <PageSizeItemSettings Items="50, 100, 200, 500, 1000" Visible="True">
                                                        </PageSizeItemSettings>
                                                    </SettingsPager>
                                                    <SettingsEditing Mode="PopupEditForm">
                                                    </SettingsEditing>
                                                    <Settings ShowHeaderFilterButton="True" ShowFooter="True" ShowFilterRow="True" />
                                                    <SettingsBehavior AllowFocusedRow="True" />
                                                    <SettingsPopup>
                                                        <EditForm Modal="True" />
                                                        <CustomizationWindow HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" />
                                                    </SettingsPopup>
                                                    <SettingsText CommandNew="Nuevo" />
                                                    <Columns>
                                                        <dx:GridViewCommandColumn ShowInCustomizationForm="True" VisibleIndex="20" ShowClearFilterButton="True" ToolTip="Funciones">
                                                            <CustomButtons>
                                                                <dx:GridViewCommandColumnCustomButton ID="btnDetalle" Text=" ">
                                                                    <Image IconID="businessobjects_boresume_16x16" ToolTip="Detalle">
                                                                    </Image>
                                                                </dx:GridViewCommandColumnCustomButton>
                                                                <dx:GridViewCommandColumnCustomButton ID="btnQuitar" Text=" ">
                                                                    <Image IconID="actions_close_16x16" ToolTip="Desasignar Usuario">
                                                                    </Image>
                                                                </dx:GridViewCommandColumnCustomButton>
                                                            </CustomButtons>
                                                        </dx:GridViewCommandColumn>
                                                        <dx:GridViewDataTextColumn Caption="ENTIDAD" FieldName="ENTIDAD" ShowInCustomizationForm="True" VisibleIndex="5" Visible="False">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="USUARIO" FieldName="USUARIO" ShowInCustomizationForm="True" VisibleIndex="3">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="ID_USUARIO" FieldName="ID_USUARIO" ShowInCustomizationForm="True" VisibleIndex="2" ToolTip="Número de Autorización">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="ID_ENTIDAD" FieldName="ID_ENTIDAD" ShowInCustomizationForm="True" VisibleIndex="4" Visible="False">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="ID_TABLA" FieldName="ID_TABLA" ShowInCustomizationForm="True" VisibleIndex="0">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="FECHA INICIO" FieldName="FECHA_DESDE" ShowInCustomizationForm="True" VisibleIndex="6" ToolTip="Fecha Inicio de Acceso">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="FECHA LIMITE" FieldName="FECHA_HASTA" ShowInCustomizationForm="True" VisibleIndex="7" ToolTip="Fecha Limite de Acceso">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="TIPO_USUARIO" FieldName="TIPO_USUARIO" ShowInCustomizationForm="True" Visible="False" VisibleIndex="11">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="ID_TIPO_ACTIVIDAD" FieldName="ID_TIPO_ACTIVIDAD" ShowInCustomizationForm="True" Visible="False" VisibleIndex="12">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="ACTIVIDAD" FieldName="ACTIVIDAD" ShowInCustomizationForm="True" Visible="False" VisibleIndex="9">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="FECHA REG." FieldName="APP_FECHA_REGISTRO" ShowInCustomizationForm="True" VisibleIndex="14" ToolTip="Fecha de Registro">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="APP_ID_USUARIO" FieldName="APP_ID_USUARIO" ShowInCustomizationForm="True" Visible="False" VisibleIndex="15">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="TIPO USUARIO" FieldName="STR_TIPO_USUARIO" ShowInCustomizationForm="True" VisibleIndex="1">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="ID_TIPO_CONTROL" FieldName="ID_TIPO_CONTROL" ShowInCustomizationForm="True" Visible="False" VisibleIndex="17">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="ID_UBICACION" FieldName="ID_UBICACION" ShowInCustomizationForm="True" Visible="False" VisibleIndex="18">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="DEPTO." FieldName="UBICACION" ShowInCustomizationForm="True" VisibleIndex="10" ToolTip="Departamento">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="TIPO CONTROL" FieldName="STR_TIPO_CONTROL" ShowInCustomizationForm="True" VisibleIndex="8">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="USUARIO REG." FieldName="APP_USUARIO" ShowInCustomizationForm="True" VisibleIndex="16" ToolTip="Usuario Registro">
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                    <Styles CssPostfix="DevGlp">
                                                        <AlternatingRow BackColor="#EEEEEE">
                                                        </AlternatingRow>
                                                    </Styles>
                                                </dx:ASPxGridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="7" style="padding-top: 5px">
                                                <dx:ASPxLabel ID="lblTituloDetalle" runat="server" ClientInstanceName="lblTituloDetalle" Font-Bold="True" Font-Underline="False" ForeColor="#FF8800" Text="DETALLE DE LA DOSIFICACION" Width="100%">
                                                    <BorderTop BorderStyle="Solid" />
                                                </dx:ASPxLabel>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td colspan="7">
                                                <dx:ASPxCallbackPanel ID="pnlDetalle" runat="server" Width="100%">
                                                    <PanelCollection>
                                                        <dx:PanelContent runat="server">
                                                        </dx:PanelContent>
                                                    </PanelCollection>
                                                </dx:ASPxCallbackPanel>
                                            </td>
                                        </tr>
                                        
                                        
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
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






    <dx:ASPxPopupControl ID="pucAsignacion" runat="server" CloseAction="CloseButton" CloseOnEscape="True" Modal="True"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Above" ClientInstanceName="pucAsignacion"
        HeaderText="Asignación Por Usuario" AllowDragging="True" PopupAnimationType="None" EnableViewState="False" Maximized="True" ShowMaximizeButton="True" Theme="Metropolis" ScrollBars="Auto" AllowResize="True" Width="1000px">
        <HeaderImage IconID="people_customer_32x32">
        </HeaderImage>
        
        <HeaderStyle>
        <BorderBottom BorderStyle="Solid" />
        </HeaderStyle>
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                
                
                <dx:ASPxSplitter ID="ASPxSplitter2" runat="server" Height="100%">
        <Panes>
            <dx:SplitterPane Size="30%" AllowResize="True" AutoHeight="True">
                <ContentCollection>
                    <dx:SplitterContentControl runat="server">
                        <table style="width: 100%;">
                            <tr>
                                <td colspan="2" style="padding-bottom: 5px">
                                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Font-Bold="True" Font-Names="Rockwell" ForeColor="#666666" Text="SELECCION DE USUARIO" Width="100%">
                                        <BorderBottom BorderStyle="Solid" />
                                    </dx:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 70%">
                                    <dx:ASPxTextBox ID="txtUsuario" runat="server" onkeyup="javascript:this.value=this.value.toUpperCase();" Width="100%" Height="26px">
                                    </dx:ASPxTextBox>
                                </td>
                                <td style="width: 30%">
                                    <dx:ASPxButton ID="btnBuscarUsuarios" runat="server" AutoPostBack="False" Font-Bold="True" ForeColor="Maroon" Text="Buscar" Theme="DevEx" Width="100%" Height="20px">
                                        <ClientSideEvents Click="function(s, e) {
pnlUsuario.PerformCallback();
}" />
                                        <Image IconID="zoom_zoom_16x16">
                                        </Image>
                                    </dx:ASPxButton>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="padding-top: 5px">
                                    <dx:ASPxCallbackPanel ID="pnlUsuario" runat="server" Width="100%" ClientInstanceName="pnlUsuario" OnCallback="pnlUsuario_Callback">
                                        <PanelCollection>
                                            <dx:PanelContent runat="server">
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxGridView ID="grvListaUsuarios" runat="server" AutoGenerateColumns="False" ClientInstanceName="grvListaUsuarios" KeyFieldName="ID_USUARIO" Theme="Metropolis" Width="100%">
                                                                <ClientSideEvents CustomButtonClick="function(s, e) {
switch(e.buttonID) {
case  'btnCargaUsuario':
pnlUsCabecera.PerformCallback();
tlsAsignar.PerformCallback(1);
break;
} 
}" />
                                                                <Settings ShowVerticalScrollBar="True" VerticalScrollableHeight="150" />
                                                                <SettingsBehavior AllowFocusedRow="True" ProcessSelectionChangedOnServer="True" />
                                                                <Columns>
                                                                    <dx:GridViewDataTextColumn Caption="ID_USUARIO" FieldName="ID_USUARIO" ShowInCustomizationForm="True" VisibleIndex="0">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="ID_TIPO_ACTIVIDAD" FieldName="ID_TIPO_ACTIVIDAD" ShowInCustomizationForm="True" Visible="False" VisibleIndex="1">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="USUARIO" FieldName="NOMBRE_USUARIO" ShowInCustomizationForm="True" VisibleIndex="2">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="ACTIVIDAD" FieldName="NOMBRE_ACTIVIDAD" ShowInCustomizationForm="True" Visible="False" VisibleIndex="3">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewCommandColumn ShowInCustomizationForm="True" VisibleIndex="4" Width="30px">
                                                                        <CustomButtons>
                                                                            <dx:GridViewCommandColumnCustomButton ID="btnCargaUsuario" Text=" ">
                                                                                <Image IconID="businessobjects_bocontact2_16x16" ToolTip="Cargar Caracteristicas">
                                                                                </Image>
                                                                            </dx:GridViewCommandColumnCustomButton>
                                                                        </CustomButtons>
                                                                    </dx:GridViewCommandColumn>
                                                                </Columns>
                                                            </dx:ASPxGridView>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </dx:PanelContent>
                                        </PanelCollection>
                                    </dx:ASPxCallbackPanel>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="padding-top: 5px">
                                    <dx:ASPxLabel ID="ASPxLabel62" runat="server" Font-Bold="True" Font-Names="Rockwell" ForeColor="#666666" Text="ESPECIFICACION DE LA ASIGNACION" Width="100%">
                                        <BorderBottom BorderStyle="Solid" />
                                    </dx:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="padding-top: 5px">
                                    <dx:ASPxButton ID="btnAsignarUsuario" runat="server" AutoPostBack="False" Font-Bold="True" ForeColor="Maroon" Text="Guardar Asignación" Theme="DevEx" Width="100%" OnClick="btnAsignarUsuario_Click">
                                        <ClientSideEvents Click="function(s, e) {
pnlUsCabecera.PerformCallback();
}" />
                                        <Image IconID="people_assigntome_16x16">
                                        </Image>
                                    </dx:ASPxButton>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" class="auto-style1" style="padding-top: 5px">
                                    <dx:ASPxLabel ID="ASPxLabel66" runat="server" ForeColor="#333333" Text="Fecha Limite de Vigencia :">
                                    </dx:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style1" colspan="2" style="padding-top: 5px">
                                    <dx:ASPxDateEdit ID="detFechaVigencia" runat="server" ClientInstanceName="detFechaVigencia" EnableTheming="True" Height="23px" Theme="Metropolis" ToolTip="Seleccione la fecha final de vigencia">
                                        <TimeSectionProperties>
                                            <TimeEditProperties>
                                                <ClearButton Visibility="Auto">
                                                </ClearButton>
                                            </TimeEditProperties>
                                        </TimeSectionProperties>
                                        <ClientSideEvents DateChanged="function(s, e) {
                                                                pnlDatos.PerformCallback();
                                                                }" />
                                        <ClearButton Visibility="Auto">
                                        </ClearButton>
                                    </dx:ASPxDateEdit>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style1" colspan="2" style="padding-top: 5px">
                                    <dx:ASPxLabel ID="ASPxLabel61" runat="server" ForeColor="#333333" Text="Tipo de Asignación :">
                                    </dx:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="padding-top: 5px">
                                    <dx:ASPxCallbackPanel ID="pnlTipoUs" runat="server" ClientInstanceName="pnlTipoUs" Width="100%">
                                        <PanelCollection>
                                            <dx:PanelContent runat="server">
                                                <dx:ASPxRadioButtonList ID="rbtTipoControl" runat="server" ClientInstanceName="rbtTipoControl" EnableTheming="True" SelectedIndex="0" Theme="Metropolis" ValueType="System.Int32" Width="100%">
                                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
//grvListaUsuarios.GetRowValues(grvListaUsuarios.GetFocusedRowIndex(), 'NOMBRE_USUARIO', function //(value){


////if(value.includes(&quot;@ANH.GOB.BO&quot;) &amp;&amp; rbtTipoControl.GetValue()&gt;2)
//if(!value.includes(&quot;@ANH.GOB.BO&quot;) &amp;&amp; rbtTipoControl.GetValue()&lt;3)
//{
//rbtTipoControl.SetValue(4);
//tlsAsignar.PerformCallback(1);
//}
//else
//{
////rbtTipoControl.SetValue(4);
//tlsAsignar.PerformCallback();
//}
////tlsAsignar.PerformCallback();

//});

tlsAsignar.PerformCallback();


}" />
                                                    <Items>
                                                        <dx:ListEditItem Selected="True" Text="EMPRESA" Value="4" />
                                                        <dx:ListEditItem Text="PROYECTO" Value="3" />
                                                        <dx:ListEditItem Text="ACTIVIDAD" Value="1" />
                                                        <dx:ListEditItem Text="DEPARTAMENTO" Value="2" />
                                                    </Items>
                                                </dx:ASPxRadioButtonList>
                                            </dx:PanelContent>
                                        </PanelCollection>
                                    </dx:ASPxCallbackPanel>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <dx:ASPxTreeList ID="tlsAsignar" runat="server" AutoGenerateColumns="False" ClientInstanceName="tlsAsignar" Font-Size="Smaller" Height="187px" KeyFieldName="intId" OnCustomCallback="tlsAsignar_CustomCallback" ParentFieldName="intIdPadre" Theme="Youthful" Styles-Header-BackColor="#317D40" ToolTip="Seleccione una entidad">
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
                                        <SettingsBehavior AllowFocusedNode="True" FocusNodeOnExpandButtonClick="False" FocusNodeOnLoad="False" />
                                        <SettingsSelection Enabled="True" />
                                        <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />

<Styles>
<Header BackColor="#317D40"></Header>
</Styles>
                                    </dx:ASPxTreeList>
                                </td>
                            </tr>
                        </table>

                    </dx:SplitterContentControl>
                </ContentCollection>
            </dx:SplitterPane>
            <dx:SplitterPane ScrollBars="Horizontal">
                <ContentCollection>
                    <dx:SplitterContentControl runat="server">
                        <dx:ASPxCallbackPanel ID="pnlUsCabecera" runat="server" Theme="Youthful" Width="100%" ClientInstanceName="pnlUsCabecera" OnCallback="pnlUsCabecera_Callback">
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
                                                <dx:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" ShowHeader="False" Visible="False" Width="100%">
                                                    <PanelCollection>
                                                        <dx:PanelContent runat="server">
                                                            <dx:ASPxLabel ID="ASPxLabel3" runat="server" ForeColor="#666666">
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
                                                </legend>
                                                <dx:ASPxLabel ID="ASPxLabel2" runat="server" ForeColor="#003300" Text="Tipo de Usuario :">
                                                </dx:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr style="width: 100%">
                                            <td colspan="3" style="width: 35%">
                                                &nbsp;</td>
                                            <td colspan="2" style="width: 20%">
                                                &nbsp;</td>
                                            <td style="width: 35%">
                                                &nbsp;</td>
                                            <td style="width: 10%; text-align: right">
                                                
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                &nbsp;</td>
                                            <td colspan="2">
                                                &nbsp;</td>
                                            <td colspan="2">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td colspan="7">
                                                <dx:ASPxGridView ID="dgvUsCabecera" runat="server" AutoGenerateColumns="False" ClientInstanceName="dgvUsCabecera" EnableTheming="True" Font-Size="Smaller" KeyFieldName="ID_TABLA" OnHtmlRowPrepared="dgvUsCabecera_HtmlRowPrepared" Theme="Default" Width="100%" OnCustomButtonCallback="dgvUsCabecera_CustomButtonCallback">
                                                    <ClientSideEvents CustomButtonClick="function(s, e) {

switch(e.buttonID) {
case  'btnUsQuitar':

if(confirm('¿Eliminar la asignación de usuario? - Una vez realizada la acción no podrá revertir el proceso. ¿Desea continuar?'))
{
s.GetRowValues(e.visibleIndex, 'TIPO_USUARIO', function (value) {
if(value==1)
{
alert('ATENCION. No puede eliminar un usuario de tipo PRINCIPAL.');
}
});
e.processOnServer = true;

}
else{
e.processOnServer = false;

}

break;
} 
}
" />
                                                    <SettingsContextMenu EnableRowMenu="False">
                                                    </SettingsContextMenu>
                                                    <Templates>
                                                        <EditForm>
                                                        </EditForm>
                                                    </Templates>
                                                    <SettingsPager NumericButtonCount="2">
                                                        <PageSizeItemSettings Items="50, 100, 200, 500, 1000" Visible="True">
                                                        </PageSizeItemSettings>
                                                    </SettingsPager>
                                                    <SettingsEditing Mode="PopupEditForm">
                                                    </SettingsEditing>
                                                    <Settings ShowFilterRow="True" ShowFooter="True" ShowHeaderFilterButton="True" />
                                                    <SettingsBehavior AllowFocusedRow="True" />
                                                    <SettingsPopup>
                                                        <EditForm Modal="True" />
                                                        <CustomizationWindow HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" />
                                                    </SettingsPopup>
                                                    <SettingsText CommandNew="Nuevo" />
                                                    <Columns>
                                                        <dx:GridViewCommandColumn ShowClearFilterButton="True" ShowInCustomizationForm="True" ToolTip="Funciones" VisibleIndex="20">
                                                            <CustomButtons>
                                                                <dx:GridViewCommandColumnCustomButton ID="btnUsQuitar" Text=" ">
                                                                    <Image IconID="actions_close_16x16" ToolTip="Desasignar Usuario">
                                                                    </Image>
                                                                </dx:GridViewCommandColumnCustomButton>
                                                            </CustomButtons>
                                                        </dx:GridViewCommandColumn>
                                                        <dx:GridViewDataTextColumn Caption="ENTIDAD" FieldName="ENTIDAD" ShowInCustomizationForm="True" VisibleIndex="8">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="USUARIO" FieldName="USUARIO" ShowInCustomizationForm="True" Visible="False" VisibleIndex="4">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="ID_USUARIO" FieldName="ID_USUARIO" ShowInCustomizationForm="True" ToolTip="Número de Autorización" Visible="False" VisibleIndex="3">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="ID_ENTIDAD" FieldName="ID_ENTIDAD" ShowInCustomizationForm="True" Visible="False" VisibleIndex="5">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="ID_TABLA" FieldName="ID_TABLA" ShowInCustomizationForm="True" VisibleIndex="0">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="FECHA INICIO" FieldName="FECHA_DESDE" ShowInCustomizationForm="True" ToolTip="Fecha Inicio de Acceso" VisibleIndex="9">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="FECHA LIMITE" FieldName="FECHA_HASTA" ShowInCustomizationForm="True" ToolTip="Fecha Limite de Acceso" VisibleIndex="10">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="TIPO_USUARIO" FieldName="TIPO_USUARIO" ShowInCustomizationForm="True" Visible="False" VisibleIndex="11">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="ID_TIPO_ACTIVIDAD" FieldName="ID_TIPO_ACTIVIDAD" ShowInCustomizationForm="True" Visible="False" VisibleIndex="12">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="ACTIVIDAD" FieldName="ACTIVIDAD" ShowInCustomizationForm="True" VisibleIndex="6">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="FECHA REG." FieldName="APP_FECHA_REGISTRO" ShowInCustomizationForm="True" ToolTip="Fecha de Registro" VisibleIndex="14">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="APP_ID_USUARIO" FieldName="APP_ID_USUARIO" ShowInCustomizationForm="True" Visible="False" VisibleIndex="15">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="TIPO USUARIO" FieldName="STR_TIPO_USUARIO" ShowInCustomizationForm="True" VisibleIndex="1">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="ID_TIPO_CONTROL" FieldName="ID_TIPO_CONTROL" ShowInCustomizationForm="True" Visible="False" VisibleIndex="17">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="ID_UBICACION" FieldName="ID_UBICACION" ShowInCustomizationForm="True" Visible="False" VisibleIndex="18">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="DEPTO." FieldName="UBICACION" ShowInCustomizationForm="True" ToolTip="Departamento" VisibleIndex="7">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="TIPO CONTROL" FieldName="STR_TIPO_CONTROL" ShowInCustomizationForm="True" VisibleIndex="2">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="USUARIO REG." FieldName="APP_USUARIO" ShowInCustomizationForm="True" ToolTip="Usuario Registro" VisibleIndex="16">
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                </dx:ASPxGridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="7">&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td colspan="7">&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td colspan="7" style="padding-top: 5px">
                                                <dx:ASPxLabel ID="ASPxLabel4" runat="server" ClientInstanceName="lblTituloDetalle" Font-Bold="True" Font-Underline="False" ForeColor="#FF8800" Width="100%">
                                                    <BorderTop BorderStyle="Solid" />
                                                </dx:ASPxLabel>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td colspan="7">
                                                <dx:ASPxCallbackPanel ID="ASPxCallbackPanel2" runat="server" Width="100%">
                                                    <PanelCollection>
                                                        <dx:PanelContent runat="server">
                                                            
                                                            <table>
                                                                <tr>
                                                                    <td colspan="3" align="center"><h4>USUARIO-PERFIL</h4></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <p class="subtitulo">PERFILES DISPONIBLES</p>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <p class="subtitulo">PERFILES ASIGNADOS AL USUARIO</p>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:ListBox ID="lbxPerfilesDisponibles" runat="server" Width="300px" DataTextField="NOMBRE_PERFIL" DataValueField="ID_PERFIL" Height="300px"></asp:ListBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Button ID="btnAsignarUno" runat="server" Text="&gt;" OnClick="btnAsignarUno_Click" Width="50px" />
                                                                        <br />
                                                                        <asp:Button ID="btnAsignarTodo" runat="server" Text="&gt;&gt;" OnClick="btnAsignarTodo_Click" Width="50px" OnClientClick="return confirm('¿DESEA ASIGNAR TODOS LOS PERFILES AL USUARIO?');" />
                                                                        <br />
                                                                        <asp:Button ID="btnQuitarTodos" runat="server" Text="&lt;&lt;" OnClick="btnQuitarTodos_Click" Width="50px" OnClientClick="return confirm('¿DESEA QUITAR TODOS LOS PERFILES DEL USUARIO?');" />
                                                                        <br />
                                                                        <asp:Button ID="btnQuitarUno" runat="server" Text="&lt;" OnClick="btnQuitarUno_Click" Width="50px" OnClientClick="return confirm('¿DESEA QUITAR EL PERFIL DEL USUARIO?');" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:ListBox ID="lbxPerfilesAsignados" runat="server" Width="300" DataTextField="NOMBRE_PERFIL" DataValueField="ID_PERFIL" Height="300px"></asp:ListBox>
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
                

            </dx:PopupControlContentControl>
        </ContentCollection>
        <ContentStyle>
            <Paddings PaddingBottom="5px" />
        </ContentStyle>
    </dx:ASPxPopupControl>

























































