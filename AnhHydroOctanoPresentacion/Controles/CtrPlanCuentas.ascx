<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtrPlanCuentas.ascx.cs" Inherits="AnhHydroOctanoPresentacion.Controles.CtrPlanCuentas" %>

<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>

<style type="text/css">
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
    
    .auto-style12 {
        width: 210px;
    }
    
    .auto-style14 {
        height: 27px;
    }
    
    </style>

<div class="col-md-12">
    <table style="width: 100%">
    <tr>
        <td style="text-align: center; padding-bottom: 1px">
            <dx:ASPxRoundPanel runat="server" ShowHeader="False" Width="100%" ID="pnlTitulo">
                <PanelCollection>
                    <dx:PanelContent runat="server">
                        <dx:ASPxLabel runat="server" ForeColor="#367B2F" ID="lblTitulo" Font-Bold="True" Font-Names="Rockwell" Font-Size="Larger" Text="PLAN DE CUENTAS"></dx:ASPxLabel>
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
                                <dx:ASPxTreeList ID="tlsOperadores" runat="server" AutoGenerateColumns="False" ClientInstanceName="tlsOperadores" Font-Size="Smaller" Height="187px" KeyFieldName="intId" ParentFieldName="intIdPadre" Theme="Youthful" ToolTip="Seleccione una actividad">
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
                                    <ClientSideEvents FocusedNodeChanged="function(s, e) {
	pnlDatos.PerformCallback();
}" Init="function(s, e) {
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
                            }" BeginCallback="function(s, e) {
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
                                        <td colspan ="7">
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
                                        <td colspan="2" style="width: 1%">
                                            &nbsp;</td>
                                        <td style="text-align: right; width: 19%">
                                            <dx:ASPxButton ID="btnFormatoDj" runat="server" AutoPostBack="False" ClientInstanceName="btnFormatoDj" Font-Bold="True" Text="Formato de la DDJJ" Theme="Metropolis" Width="170px" ForeColor="Maroon">
                                                <ClientSideEvents Click="function(s, e) {
pucFormatoDj.Show();
pucFormatoDj.PerformCallback();
}
" />
                                                <Image IconID="maps_inlinesizelegend_16x16">
                                                </Image>
                                            </dx:ASPxButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        
                                        <td colspan="7">
                                            <dx:ASPxGridView ID="dgvCuentas" runat="server" Theme="Office2003Olive" AutoGenerateColumns="False" KeyFieldName="ID_AUTORIZACION_CONDICION" EnableTheming="True" Font-Size="Smaller" ClientInstanceName="dgvCuentas" OnHtmlRowPrepared="dgvCuentas_HtmlRowPrepared" Width="100%" OnCustomButtonCallback="dgvCuentas_CustomButtonCallback">
                                                <ClientSideEvents CustomButtonClick="function(s, e) {

txtCtaId.SetValue('0');
txtCtaCodigo.SetValue('');
txtCtaDescripcion.SetValue('');
txtCtaPadre.SetValue('');

switch(e.buttonID) {

case 'btnModificar':
s.GetRowValues(e.visibleIndex, 'ID_AUTORIZACION_CONDICION', function (value) {
txtCtaId.SetValue(value);
});
s.GetRowValues(e.visibleIndex, 'CODIGO', function (value) {
txtCtaCodigo.SetValue(value);
});
s.GetRowValues(e.visibleIndex, 'DESCRIPCION', function (value) {
txtCtaDescripcion.SetValue(value);
});
break;

case 'btnEliminar':
if(confirm('¿Desea eliminar el registro? - Una vez realizada la acción no podrá revertir el proceso. ¿Desea continuar?'))
{
e.processOnServer = true;
}
else{
e.processOnServer = false; 
}
break;

case  'btnHijo':
s.GetRowValues(e.visibleIndex, 'ESTADO', function (value) {
if(value==2) {
alert('El registro no apica para la función.');
}
else{
s.GetRowValues(e.visibleIndex, 'ID_AUTORIZACION_CONDICION', function (value) {
txtCtaId.SetValue(value);
});
s.GetRowValues(e.visibleIndex, 'CODIGO', function (value) {
txtCtaPadre.SetValue(value);
});
btnPfGrabar.SetText('Agregar Hijo');
}
});
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
                                                <SettingsPager NumericButtonCount="3" PageSize="30">
                                                    <PageSizeItemSettings Items="50, 100, 200, 500, 1000" Visible="True">
                                                    </PageSizeItemSettings>
                                                </SettingsPager>
                                                <SettingsEditing Mode="Inline">
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
                                                            <dx:GridViewCommandColumnCustomButton ID="btnEliminar" Text=" ">
                                                                <Image IconID="reports_deletegroupfooter_16x16" ToolTip="Eliminar Registro">
                                                                </Image>
                                                            </dx:GridViewCommandColumnCustomButton>
                                                            <dx:GridViewCommandColumnCustomButton ID="btnHijo" Text=" ">
                                                                <Image IconID="actions_insert_16x16" ToolTip="Agregar Cuenta Hijo">
                                                                </Image>
                                                            </dx:GridViewCommandColumnCustomButton>
                                                        </CustomButtons>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <CellStyle ForeColor="#006600">
                                                        </CellStyle>
                                                    </dx:GridViewCommandColumn>
                                                    <dx:GridViewDataTextColumn Caption="DESCRIPCION" FieldName="DESCRIPCION" ShowInCustomizationForm="True" VisibleIndex="4">
                                                        <Settings AutoFilterCondition="Contains" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="ESTADO" FieldName="ESTADO" ShowInCustomizationForm="True" VisibleIndex="2" Visible="False">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="ID" FieldName="ID_AUTORIZACION_CONDICION" ShowInCustomizationForm="True" VisibleIndex="1" ToolTip="Identificador del Tipo de Operación" Visible="False">
                                                        <Settings AutoFilterCondition="Contains" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="CODIGO" FieldName="CODIGO" ShowInCustomizationForm="True" VisibleIndex="3">
                                                        <Settings AutoFilterCondition="Contains" />
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="TIPO" FieldName="SUBID_NOMBRE" ShowInCustomizationForm="True" VisibleIndex="0">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="ID_IDENTIFICADOR" ShowInCustomizationForm="True" Visible="False" VisibleIndex="6">
                                                    </dx:GridViewDataTextColumn>
                                                </Columns>
                                                
                                                <TotalSummary>
                                                    <dx:ASPxSummaryItem FieldName="F_IMPORTE" ShowInColumn="IMPORTE" SummaryType="Custom" Tag="imp" />
                                                    <dx:ASPxSummaryItem FieldName="VOLUMEN" ShowInColumn="CANTIDAD" SummaryType="Custom" Tag="vol" />
                                                    <dx:ASPxSummaryItem FieldName="STR_ESTADO" ShowInColumn="EST." SummaryType="Custom" Tag="est" />
                                                </TotalSummary>
                                            </dx:ASPxGridView>
                                        </td>
                                        
                                    </tr>
                                    <tr>
                                        <td class="auto-style11">
                                            <dx:ASPxCallbackPanel ID="pnlCta" runat="server" Width="100%" ClientInstanceName="pnlCta" OnCallback="pnlCta_Callback">
                                                <PanelCollection>
                                                    <dx:PanelContent runat="server">
                                                        <table style="width: 100%">
                                                            <tr>
                                                                <td>
                                                                    
                                                                    <dx:ASPxTextBox ID="txtCtaId" runat="server" BackColor="#FFE1C4" ClientInstanceName="txtCtaId" Font-Size="Smaller" ReadOnly="True" style="margin-bottom: 0px" Text="0" Width="170px">
                                                                    </dx:ASPxTextBox>
                                                                    
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    
                                                                    <dx:ASPxTextBox ID="txtCtaPadre" runat="server" BackColor="#FFE1C4" ClientInstanceName="txtCtaPadre" Font-Size="Smaller" ReadOnly="True" Width="170px">
                                                                    </dx:ASPxTextBox>
                                                                    
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    
                                                                    <dx:ASPxTextBox ID="txtCtaCodigo" runat="server" ClientInstanceName="txtCtaCodigo" Font-Size="Smaller" Width="170px">
                                                                    </dx:ASPxTextBox>
                                                                    
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    
                                                                    <dx:ASPxTextBox ID="txtCtaDescripcion" runat="server" ClientInstanceName="txtCtaDescripcion" Font-Size="Smaller" Width="170px">
                                                                    </dx:ASPxTextBox>
                                                                    
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    
                                                                    <dx:ASPxButton ID="btnLimpiar" runat="server" AutoPostBack="False" EnableTheming="True" Font-Bold="True" ForeColor="Maroon" Text="Limpiar" Theme="Metropolis" Width="100%">
                                                                        <ClientSideEvents Click="function(s, e) {
txtCtaId.SetValue('0');
txtCtaCodigo.SetValue('');
txtCtaDescripcion.SetValue('');
txtCtaPadre.SetValue('');

btnPfGrabar.SetText('Agregar Padre');
}" />
                                                                        <Image IconID="actions_clear_16x16">
                                                                        </Image>
                                                                    </dx:ASPxButton>
                                                                    
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    
                                                                    <dx:ASPxButton ID="btnPfGrabar" runat="server" ClientInstanceName="btnPfGrabar" EnableTheming="True" Font-Bold="True" ForeColor="Maroon" OnClick="btnPfGrabar_Click" Text="Agregar Padre" Theme="Metropolis" Width="100%">
                                                                        <Image IconID="actions_apply_16x16">
                                                                        </Image>
                                                                    </dx:ASPxButton>
                                                                    
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </dx:PanelContent>
                                                </PanelCollection>
                                            </dx:ASPxCallbackPanel>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style11">
                                            &nbsp;</td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style11">
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style11">
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
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




<dx:ASPxPopupControl ID="pucConfig" runat="server" AllowDragging="True" ClientInstanceName="pucConfig" EnableTheming="True" HeaderText="Registro/Modificación" Height="93px" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Above" Theme="Metropolis" Width="718px" OnWindowCallback="pucConfig_WindowCallback">
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
                        <dx:ASPxLabel ID="ASPxLabel45" runat="server" Text="ID :" Font-Bold="True" ForeColor="#666666">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="txtId" runat="server" Width="150px" ClientInstanceName="txtId" Text="0" BackColor="#FFE1C4" ReadOnly="True">
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
                        <dx:ASPxLabel ID="ASPxLabel53" runat="server" Font-Bold="True" ForeColor="#666666" Text="Límite Mínimo :">
                        </dx:ASPxLabel>
                    </td>
                    <td class="auto-style14">
                        <dx:ASPxTextBox ID="txtLimiteMin" runat="server" ClientInstanceName="txtLimiteMin" Text="0" Width="150px">
                            <MaskSettings Mask="&lt;0..9999999&gt;.&lt;00..99&gt;" />
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">
                        <dx:ASPxLabel ID="ASPxLabel54" runat="server" Font-Bold="True" ForeColor="#666666" Text="Límite Máximo :">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="txtLimiteMax" runat="server" ClientInstanceName="txtLimiteMax" Text="0" Width="150px">
                            <MaskSettings Mask="&lt;0..9999999&gt;.&lt;00..99&gt;" />
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style12">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style12">
                        <dx:ASPxLabel ID="ASPxLabel46" runat="server" Font-Bold="True" Font-Size="Smaller" Font-Underline="True" ForeColor="#333333" Text="FORMATO P/DECLARACION JURADA">
                        </dx:ASPxLabel>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: right;">
                        <dx:ASPxLabel ID="ASPxLabel55" runat="server" Font-Bold="True" ForeColor="#666666" Text="Columna de Visualización :">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxComboBox ID="cmbReporteVariable" runat="server" ClientInstanceName="cmbReporteVariable" OnCallback="cmbReporteVariable_Callback" TextField="CODIGO" TextFormatString="{0}" ValueField="iD_REPORTE_VARIABLE" Width="200px">
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
                <tr>
                    <td style="text-align: right;">&nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
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
                    <td>
                        &nbsp;</td>
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
                    <td class="auto-style12">&nbsp;</td>
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
                        &nbsp;</td>
                </tr>
            </table>
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>





