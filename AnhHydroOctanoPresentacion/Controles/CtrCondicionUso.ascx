<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtrCondicionUso.ascx.cs" Inherits="AnhHydroOctanoPresentacion.Controles.CtrCondicionUso" %>

<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>

<style type="text/css">
    .auto-style1 {
        height: 18px;
    }

    .auto-style4 {
        height: 18px;
        width: 168px;
    }

    .auto-style6 {
        height: 18px;
        width: 138px;
    }

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

    .auto-style22 {
        width: 226px;
    }

    .auto-style23 {
        width: 70%;
    }

    .auto-style24 {
        height: 19px;
    }
</style>

<div class="col-md-12">
    <table style="width: 100%">
        <tr>
            <td style="text-align: center; padding-bottom: 1px">
                <dx:ASPxRoundPanel runat="server" ShowHeader="False" Width="100%" ID="pnlTitulo">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <dx:ASPxLabel runat="server" ForeColor="#367B2F" ID="lblTitulo" Font-Bold="True" Font-Names="Rockwell" Font-Size="Larger" Text="CONFIGURACIÓN DE CONDICIONES DE USO"></dx:ASPxLabel>
                        </dx:PanelContent>
                    </PanelCollection>
                    <Border BorderStyle="Ridge" BorderWidth="1px" BorderColor="#003300"></Border>
                </dx:ASPxRoundPanel>
            </td>
        </tr>
    </table>

    <dx:ASPxSplitter ID="ASPxSplitter1" runat="server" Height="700px">
        <Panes>
            <dx:SplitterPane Size="25%" ScrollBars="Auto">
                <ContentCollection>
                    <dx:SplitterContentControl runat="server">
                        <table style="width: 100%;">

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
                                    <dx:ASPxTreeList ID="tlsOperadores" runat="server" AutoGenerateColumns="False" ClientInstanceName="tlsOperadores" Font-Size="Smaller" Height="187px" KeyFieldName="intId" ParentFieldName="intIdPadre" Theme="Youthful" Styles-Header-BackColor="#317D40" ToolTip="Seleccione los proyectos" OnCustomCallback="tlsOperadores_CustomCallback">
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
pnlDatos.PerformCallback();
}"
                                            Init="function(s, e) {
	    pnlDatos.PerformCallback();
    }"
                                            NodeDblClick="function(s, e) {
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
                                                    <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="Rockwell" Font-Size="Medium" ForeColor="#367B2F" Text="PROYECTOS SELECCIONADOS"></asp:Label>
                                                </legend>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style4">
                                                <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="#10420F" Text="ACTIVIDAD"></asp:Label>
                                            </td>
                                            <td class="auto-style1">&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td class="auto-style6">&nbsp;</td>
                                            <td colspan="2">
                                                <asp:Label ID="Label3" runat="server" Font-Bold="True" ForeColor="#10420F" Text="DEPARTAMENTO"></asp:Label>
                                            </td>
                                            <td class="auto-style1">&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" style="width: 70%">
                                                <asp:Label ID="lblActividad" runat="server" Font-Bold="False"></asp:Label>
                                            </td>
                                            <td colspan="2" style="width: 15%">
                                                <asp:Label ID="lblDepartamento" runat="server" Font-Bold="False"></asp:Label>
                                            </td>
                                            <td style="text-align: right; width: 15%">
                                                <dx:ASPxButton ID="btnConfigurar" runat="server" AutoPostBack="False" Font-Bold="True" ForeColor="Maroon" Text="Configuración Grupal" Theme="Metropolis" Width="170px">
                                                    <ClientSideEvents Click="function(s, e) {
    pucConfig.Show();
    txtIdConsumidor.SetValue('0');
    pucConfig.PerformCallback();
    }" />
                                                    <Image IconID="filterelements_showall_16x16">
                                                    </Image>
                                                </dx:ASPxButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="7">
                                                <dx:ASPxGridView ID="dgvEntidades" runat="server" AutoGenerateColumns="False" ClientInstanceName="dgvEntidades" EnableTheming="True" Font-Size="Smaller" KeyFieldName="ID_CONSUMIDOR" Theme="Default" Width="100%">
                                                    <ClientSideEvents CustomButtonClick="function(s, e) {
    switch(e.buttonID) {
    case&nbsp; 'btnConfig':
    	pucConfig.Show();
    	txtIdConsumidor.SetValue('1');
    	pucConfig.PerformCallback();
    break;
    case 'btnSincronizar':
pucImportaMov.Show();	
pucImportaMov.PerformCallback();
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
                                                    <SettingsPager NumericButtonCount="5" PageSize="2000">
                                                        <PageSizeItemSettings Items="50, 100, 200, 500, 1000" Visible="True">
                                                        </PageSizeItemSettings>
                                                    </SettingsPager>
                                                    <SettingsEditing Mode="PopupEditForm">
                                                    </SettingsEditing>
                                                    <Settings ShowFilterRow="True" ShowHeaderFilterButton="True" />
                                                    <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" />
                                                    <SettingsPopup>
                                                        <EditForm HorizontalAlign="Center" Modal="True" VerticalAlign="Middle" VerticalOffset="0" />
                                                        <CustomizationWindow HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" />
                                                    </SettingsPopup>
                                                    <SettingsText CommandNew="Nuevo" />
                                                    <Columns>
                                                        <dx:GridViewCommandColumn ShowClearFilterButton="True" ShowInCustomizationForm="True" ToolTip="Funciones" VisibleIndex="21">
                                                            <CustomButtons>
                                                                <dx:GridViewCommandColumnCustomButton ID="btnConfig" Text=" ">
                                                                    <Image IconID="setup_properties_16x16" ToolTip="Configurar">
                                                                    </Image>
                                                                </dx:GridViewCommandColumnCustomButton>
                                                            </CustomButtons>
                                                            <CustomButtons>
                                                                <dx:GridViewCommandColumnCustomButton ID="btnSincronizar" Text=" ">
                                                                    <Image IconID="data_addnewdatasource_16x16" ToolTip="Obtener registros de informacion B-SISA y MOV">
                                                                    </Image>
                                                                </dx:GridViewCommandColumnCustomButton>
                                                            </CustomButtons>
                                                            <CellStyle ForeColor="#006600">
                                                            </CellStyle>
                                                        </dx:GridViewCommandColumn>
                                                        <dx:GridViewDataTextColumn Caption="RAZON SOCIAL" FieldName="DENOMINACION" ShowInCustomizationForm="True" VisibleIndex="2">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="NIT" FieldName="NIT" ShowInCustomizationForm="True" VisibleIndex="3">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="HAB. CSV OTROS" FieldName="LATITUD" ShowInCustomizationForm="True" Visible="False" VisibleIndex="5">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="REPRESENTANTE LEGAL" FieldName="REPRESENTANTE_LEGAL" ShowInCustomizationForm="True" VisibleIndex="4">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="HAB. FUNC. MOV." FieldName="LONGITUD" ShowInCustomizationForm="True" Visible="False" VisibleIndex="6">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="DEPTO." FieldName="DEPARTAMENTO" ShowInCustomizationForm="True" ToolTip="Departamento" VisibleIndex="0">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="ID" FieldName="ID_CONSUMIDOR" ShowInCustomizationForm="True" ToolTip="Identificador de la Entidad" VisibleIndex="1">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="PER. DIAS P/DECL." FieldName="NIT" ShowInCustomizationForm="True" Visible="False" VisibleIndex="7">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="PER. TIPO" FieldName="NIT" ShowInCustomizationForm="True" Visible="False" VisibleIndex="9">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="PER. DIAS APERTURA" FieldName="NIT" ShowInCustomizationForm="True" Visible="False" VisibleIndex="11">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="PERIODICIDAD" FieldName="NIT" ShowInCustomizationForm="True" Visible="False" VisibleIndex="13">
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


<dx:ASPxPopupControl ID="pucConfig" runat="server" AllowDragging="True" ClientInstanceName="pucConfig" EnableTheming="True" HeaderText="Configuración" Height="105px" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Above" Theme="Metropolis" Width="800px" OnWindowCallback="pucConfig_WindowCallback">
    <HeaderImage IconID="filterelements_showall_32x32">
    </HeaderImage>
    <FooterStyle>
        <BorderBottom BorderStyle="None" />
    </FooterStyle>
    <HeaderStyle>
        <BorderBottom BorderStyle="Solid" />
    </HeaderStyle>
    <ContentCollection>
        <dx:PopupControlContentControl runat="server">
            <table class="dxflInternalEditorTable_Youthful">
                <tr>
                    <td class="auto-style22">
                        <dx:ASPxLabel ID="ASPxLabel45" runat="server" Text="Actividad :" Font-Bold="True" ForeColor="#333333">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="txtActividad" runat="server" Width="100%" BackColor="#FFE1C4" ClientInstanceName="txtActividad">
                        </dx:ASPxTextBox>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style22">
                        <dx:ASPxLabel ID="ASPxLabel44" runat="server" Text="Departamento :" Font-Bold="True" ForeColor="#333333">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="txtDepto" runat="server" Width="100%" BackColor="#FFE1C4" ClientInstanceName="txtDepto">
                        </dx:ASPxTextBox>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style22">
                        <dx:ASPxLabel ID="ASPxLabel40" runat="server" Text="ID Consumidor :" Font-Bold="True" ForeColor="#333333">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="txtIdConsumidor" runat="server" Width="170px" BackColor="#FFE1C4" ClientInstanceName="txtIdConsumidor" ReadOnly="True" Text="0">
                            <MaskSettings Mask="&lt;0..999999999999999&gt;" />
                        </dx:ASPxTextBox>
                        <dx:ASPxTextBox ID="txtIdEntidad" runat="server" BackColor="#FFE1C4" ClientInstanceName="txtIdEntidad" ReadOnly="True" Text="0" Width="170px">
                            <MaskSettings Mask="&lt;0..999999999999999&gt;" />
                        </dx:ASPxTextBox>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="3">
                        <hr />
                    </td>
                </tr>

                <tr>
                    <td colspan="3">
                        <asp:Panel ID="pnlGeneral" runat="server" EnableTheming="False">
                            <table style="width: 100%">

                                <tr>
                                    <td style="width: 45%; padding-bottom: 10px">
                                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Font-Bold="True" Font-Size="Smaller" Font-Underline="True" ForeColor="#333333" Text="PARAMETROS GENERALES">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td></td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="Días de habilitación para el cumplimiento de obligaciones mensuales :">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <dx:ASPxSpinEdit ID="spnPeriodo" runat="server" MaxValue="30" MinValue="1" Number="10" Theme="Metropolis" Width="60px">
                                            <ClearButton Visibility="Auto">
                                            </ClearButton>
                                        </dx:ASPxSpinEdit>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="Periodicidad de la Declaración Jurada :">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <dx:ASPxComboBox ID="cmbPeriodicidad" runat="server" ReadOnly="True" SelectedIndex="0" Theme="Metropolis">
                                            <Items>
                                                <dx:ListEditItem Selected="True" Text="MENSUAL" Value="3" />
                                            </Items>
                                            <ClearButton Visibility="Auto">
                                            </ClearButton>
                                            <ReadOnlyStyle BackColor="#FFE1C4">
                                            </ReadOnlyStyle>
                                        </dx:ASPxComboBox>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="Días por defecto para la ampliación de plazos :">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <dx:ASPxSpinEdit ID="spnApertura" runat="server" MaxValue="15" MinValue="1" Number="2" Theme="Metropolis" Width="60px">
                                            <ClearButton Visibility="Auto">
                                            </ClearButton>
                                        </dx:ASPxSpinEdit>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <dx:ASPxCheckBox ID="chkDiaHabil" runat="server" CheckState="Unchecked" Text="Utilizar &quot;DIAS CALENDARIO&quot; para el cálculo de fechas según los días de habilitación" ValueChecked="1" ValueType="System.Int32" ValueUnchecked="0" Height="19px">
                                        </dx:ASPxCheckBox>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <dx:ASPxCheckBox ID="chkDigital" runat="server" CheckState="Unchecked" Text="Establecer la digitalización de la Declaración Jurada como requisito para la emisión de la misma en el siguiente periodo" ValueChecked="1" ValueType="System.Int32" ValueUnchecked="0" ReadOnly="True">
                                        </dx:ASPxCheckBox>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <dx:ASPxCheckBox ID="chkIntimacion" runat="server" CheckState="Unchecked" Text="Impedir la emisión de la declaracion jurada si la entidad presenta una Intimación" ValueChecked="1" ValueType="System.Int32" ValueUnchecked="0" ReadOnly="True">
                                        </dx:ASPxCheckBox>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        <dx:ASPxLabel ID="ASPxLabel59" runat="server" Text="Tipo de manejo de saldos :">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td style="padding-bottom: 3px">
                                        <dx:ASPxComboBox ID="cmbTipoSaldo" runat="server" SelectedIndex="0" Theme="Metropolis" ValueType="System.Int32" Width="100%">
                                            <Items>
                                                <dx:ListEditItem Selected="True" Text="Registro de Saldos y Declaración Jurada por Producto" Value="1" />
                                                <dx:ListEditItem Text="Registro de Saldos y Declaración jurada por Infraestructura" Value="2" />
                                            </Items>
                                            <ClearButton Visibility="Auto">
                                            </ClearButton>
                                            <ReadOnlyStyle BackColor="#DEE8DB">
                                            </ReadOnlyStyle>
                                        </dx:ASPxComboBox>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        <dx:ASPxLabel ID="ASPxLabel63" runat="server" Text="Texto de la cabecera (Formulario Declaración Jurada) :">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td style="padding-bottom: 3px">
                                        <dx:ASPxTextBox ID="txtCabeceraDj" runat="server" Text="Código: UD/XX-X00-X00-X00 ANEXO 0|Versión : 0.0|Aprobado :" Width="100%">
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        <dx:ASPxLabel ID="ASPxLabel64" runat="server" Text="Actividades Relacionadas con la Actividad :">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td style="padding-bottom: 3px">
                                        <dx:ASPxTextBox ID="txtActRel" runat="server" Width="100%">
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <dx:ASPxCheckBox ID="chkTipoImpresion" runat="server" CheckState="Unchecked" Text="Agrupar Impresión de la DDJJ por producto" ValueChecked="1" ValueType="System.Int32" ValueUnchecked="0">
                                        </dx:ASPxCheckBox>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;" colspan="2">
                                        <legend style="padding-bottom: 5px">
                                            <dx:ASPxButton ID="btnGuardaGral" runat="server" EnableTheming="True" OnClick="btnGuardaGral_Click" Text="Guardar Configuración" Theme="Metropolis" Width="200px" Font-Bold="True" ForeColor="Maroon">
                                                <Image IconID="actions_apply_16x16">
                                                </Image>
                                            </dx:ASPxButton>
                                        </legend>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>

                    <td colspan="2">
                        <dx:ASPxPageControl ID="pgcOpciones" runat="server" ActiveTabIndex="1" ClientInstanceName="pgcOpciones" Font-Bold="True" ForeColor="#333333" Theme="Metropolis" Width="100%">
                            <TabPages>
                                <dx:TabPage Text="Tipos de Operaciones">
                                    <ContentCollection>
                                        <dx:ContentControl runat="server">
                                            <table>
                                                <tr>
                                                    <td class="auto-style1">
                                                        <script type="text/javascript">

                                                            function UncheckCheckboxes() {
                                                                var selected = gridView.GetSelectedKeysOnPage();
                                                                for (var i = 0; i < selected.length; i++) {
                                                                    var checkBox = ASPxClientControl.GetControlCollection().GetByName("cbSelected_" + selected[i]);
                                                                    checkBox.SetChecked(false);
                                                                }
                                                            }

                                                            function cbSelectedHIM_CheckedChanged(s, visibleIndex) {
                                                                //var mensaje = confirm("¿Confirme el Registro?");
                                                                //if (s.GetChecked() == true) {
                                                                //    //alert(selected);




                                                                //} else {
                                                                //    alert(s);
                                                                //}
                                                                panelDatosValidacion.PerformCallback(visibleIndex + "|HHIM|" + s.GetChecked());
                                                            }

                                                            function cbSelectedHFDM_CheckedChanged(s, visibleIndex) {
                                                                panelDatosValidacion.PerformCallback(visibleIndex + "|HFDM|" + s.GetChecked());
                                                            }
                                                            function cbSelectedECG_CheckedChanged(s, visibleIndex) {
                                                                panelDatosValidacion.PerformCallback(visibleIndex + "|ECG|" + s.GetChecked());
                                                            }

                                                            function txtObservacion_TextChanged(s, visibleIndex) {
                                                                //alert('hola mundo 1');

                                                                var nombreCampo = "txtObservacion_" + visibleIndex;
                                                                //alert('hola mundo 2');

                                                                //var texto = nombreCampo.GetText();
                                                                //alert('hola mundo 3:' + texto);

                                                                panelDatosValidacion.PerformCallback(visibleIndex + "|OBS|" + s.cpHFKey + "|ddd|" + s.GetValue());
                                                                //alert('hola mundo 4');

                                                            }

                                                        </script>
                                                        <dx:ASPxCallbackPanel runat="server" Width="100%" ID="panelDatosValidacion" ClientInstanceName="panelDatosValidacion" BorderBottom-BorderStyle="None" OnCallback="panelDatosValidacionRegistro_Callback">
                                                            <PanelCollection>
                                                                <dx:PanelContent runat="server"></dx:PanelContent>
                                                            </PanelCollection>
                                                            <BorderBottom BorderStyle="None"></BorderBottom>
                                                        </dx:ASPxCallbackPanel>




                                                        <dx:ASPxGridView ID="dgvTipoOperacion" runat="server" AutoGenerateColumns="False" ClientInstanceName="dgvTipoOperacion" EnableTheming="True" Font-Size="Smaller" KeyFieldName="ID_TIPO_OPERACION" OnHtmlRowPrepared="dgvTipoOperacion_HtmlRowPrepared" Theme="Default" Width="100%">
                                                            <ClientSideEvents CustomButtonClick="function(s, e) {
                                switch(e.buttonID) {

                                case&nbsp; 'btnModificar':
                                pucConfig.Show();
                                txtId.SetValue(1);
                                pucConfig.PerformCallback();
                                break;

                                case&nbsp; 'btnEliminar':
                                e.processOnServer = confirm('¿Desea eliminar el registro?'); 
                                break;


                                } 
                                }" />
                                                            <SettingsContextMenu EnableRowMenu="False">
                                                            </SettingsContextMenu>
                                                            <Templates>
                                                                <GroupFooterRow>
                                                                    <dx:ASPxLabel ID="ASPxLabel51" runat="server" Text="ASPxLabel">
                                                                    </dx:ASPxLabel>
                                                                </GroupFooterRow>
                                                                <EditForm>
                                                                </EditForm>
                                                            </Templates>
                                                            <SettingsPager NumericButtonCount="5" PageSize="50">
                                                                <PageSizeItemSettings Items="50, 100, 200, 500, 1000" Visible="True">
                                                                </PageSizeItemSettings>
                                                            </SettingsPager>
                                                            <SettingsEditing Mode="PopupEditForm">
                                                            </SettingsEditing>
                                                            <Settings ShowHeaderFilterButton="True" />
                                                            <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" />
                                                            <SettingsPopup>
                                                                <EditForm HorizontalAlign="Center" Modal="True" VerticalAlign="Middle" VerticalOffset="0" />
                                                                <CustomizationWindow HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" />
                                                            </SettingsPopup>
                                                            <SettingsText CommandNew="Nuevo" />
                                                            <Columns>
                                                                <dx:GridViewCommandColumn ShowInCustomizationForm="True" Visible="False" VisibleIndex="21">
                                                                    <CustomButtons>
                                                                        <dx:GridViewCommandColumnCustomButton ID="btn" Text="Deshabilitar">
                                                                        </dx:GridViewCommandColumnCustomButton>
                                                                        <dx:GridViewCommandColumnCustomButton ID="btnEliminar" Text="Eliminar">
                                                                        </dx:GridViewCommandColumnCustomButton>
                                                                    </CustomButtons>
                                                                    <CellStyle ForeColor="#006600">
                                                                    </CellStyle>
                                                                </dx:GridViewCommandColumn>
                                                                <dx:GridViewDataTextColumn Caption="DESCRIPCION" FieldName="TIPO_OPERACION" ShowInCustomizationForm="True" VisibleIndex="3">
                                                                    <Settings AutoFilterCondition="Contains" />
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="FACTOR_CALCULO" FieldName="FACTOR_CALCULO" ShowInCustomizationForm="True" Visible="False" VisibleIndex="4">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="ID_AUTORIZACION_ACCION" FieldName="ID_AUTORIZACION_ACCION" ShowInCustomizationForm="True" Visible="False" VisibleIndex="5">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="TIPO" FieldName="strTipo" ShowInCustomizationForm="True" VisibleIndex="2">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="LIMITE_MINIMO" FieldName="LIMITE_MINIMO" ShowInCustomizationForm="True" Visible="False" VisibleIndex="6">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="ID" FieldName="ID_TIPO_OPERACION" ShowInCustomizationForm="True" VisibleIndex="0" ToolTip="Identificador del tipo de Operación">
                                                                    <Settings AutoFilterCondition="Contains" />
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="CODIGO" FieldName="CODIGO" ShowInCustomizationForm="True" VisibleIndex="1">
                                                                    <Settings AutoFilterCondition="Contains" />
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="BINARIO_ESTADO" FieldName="BINARIO_ESTADO" ShowInCustomizationForm="True" Visible="False" VisibleIndex="7">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="H.I.M." FieldName="decMasivo" ShowInCustomizationForm="True" VisibleIndex="8" ToolTip="Habilitado para Importación Masiva">
                                                                    <DataItemTemplate>
                                                                        <dx:ASPxCheckBox ID="chkMasivo" runat="server" CheckState="Unchecked" ValueChecked="1" ValueType="System.Decimal" ValueUnchecked="0" Value='<%# Eval("decMasivo") %>' OnInit="chkMasivo_Init" AutoPostBack="false">
                                                                        </dx:ASPxCheckBox>
                                                                    </DataItemTemplate>
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="H.F.D.M." FieldName="decFuncion" ShowInCustomizationForm="True" ToolTip="Habilitado para Funciones en Detalle de Movimientos" VisibleIndex="9">
                                                                    <DataItemTemplate>
                                                                        <dx:ASPxCheckBox ID="chkFuncion" runat="server" CheckState="Unchecked" Value='<%# Eval("decFuncion") %>' ValueChecked="1" ValueType="System.Decimal" ValueUnchecked="0" OnInit="chkFuncion_Init" AutoPostBack="false">
                                                                        </dx:ASPxCheckBox>
                                                                    </DataItemTemplate>
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="OBSERV." FieldName="OBSERVACIONES" Name="strObservacion" ShowInCustomizationForm="True" VisibleIndex="11" ToolTip="Observaciones">
                                                                    <DataItemTemplate>
                                                                        <dx:ASPxTextBox ID="txtObservacion" runat="server" Width="170px" Text='<%# Eval("OBSERVACIONES") %>' OnInit="txtObservacion_Init">

                                                                            <Border BorderStyle="None" />
                                                                        </dx:ASPxTextBox>
                                                                    </DataItemTemplate>
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="FECHA_INICIO" FieldName="FECHA_INICIO" ShowInCustomizationForm="True" Visible="False" VisibleIndex="13">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="FECHA_FIN" FieldName="FECHA_FIN" ShowInCustomizationForm="True" Visible="False" VisibleIndex="14">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="APP_FECHA_REGISTRO" FieldName="APP_FECHA_REGISTRO" ShowInCustomizationForm="True" Visible="False" VisibleIndex="15">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="APP_ID_USUARIO" FieldName="APP_ID_USUARIO" ShowInCustomizationForm="True" Visible="False" VisibleIndex="16">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="E.C.G." FieldName="decExcluye" ShowInCustomizationForm="True" ToolTip="Excluir de la Configuración Grupal" VisibleIndex="12">
                                                                    <DataItemTemplate>
                                                                        <dx:ASPxCheckBox ID="chkExcluye" runat="server" CheckState="Unchecked" Value='<%# Eval("decExcluye") %>' ValueChecked="1" ValueType="System.Decimal" ValueUnchecked="0" OnInit="chkExcluye_Init" AutoPostBack="false">
                                                                        </dx:ASPxCheckBox>
                                                                    </DataItemTemplate>
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="H.R.C.D." FieldName="decCumpDiario" ShowInCustomizationForm="True" ToolTip="Habilitado para Reporte Cumplimiento Diario" VisibleIndex="10" Visible="False">
                                                                    <DataItemTemplate>
                                                                        <dx:ASPxCheckBox ID="chkCumpDiario" runat="server" CheckState="Unchecked" Value='<%# Eval("decCumpDiario") %>' ValueChecked="1" ValueType="System.Decimal" ValueUnchecked="0">
                                                                        </dx:ASPxCheckBox>
                                                                    </DataItemTemplate>
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
                                                    <td style="text-align: right;">
                                                        <%-- <dx:ASPxButton ID="btnGuardaTo" runat="server" Text="Guardar Configuración" Theme="Metropolis" Width="200px" OnClick="btnGuardaTo_Click" AutoPostBack="False" Font-Bold="True" ForeColor="Maroon" Visible="False">
                                                            <Image IconID="actions_apply_16x16">
                                                            </Image>-
                                                        </dx:ASPxButton>--%>
                                                        <dx:ASPxButton ID="btnToEliminar" runat="server" AutoPostBack="False" ClientInstanceName="btnToEliminar" EnableTheming="True" Font-Bold="False" OnClick="btnToEliminar_Click" Text="Eliminar Tipos de Op." Theme="Office2010Silver" Width="200px">
                                                            <ClientSideEvents Click="function(s, e) {
e.processOnServer = confirm('¿Desea ELIMINAR la configuración de tipos de operación de toda la actividad? - Una vez realizada la acción no podrá volver atras. ¿Desea continuar?'); 
}" />
                                                            <Image IconID="spreadsheet_deletesheetcells_16x16">
                                                            </Image>
                                                        </dx:ASPxButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:TabPage>
                                <dx:TabPage Text="Hidrocarburos">
                                    <ContentCollection>
                                        <dx:ContentControl runat="server">
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="width: 25%">
                                                        <table>
                                                            <tr>
                                                                <td>Producto:</td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <dx:ASPxComboBox ID="cmbHidrocarburo" runat="server" ClientInstanceName="cmbHidrocarburo" Font-Size="X-Small" TextField="NOMBRE" TextFormatString="{0}" ValueField="ID_PRODUCTO" Width="160px" Theme="Metropolis" ToolTip="Seleccione un producto">
                                                                        <Columns>
                                                                            <dx:ListBoxColumn Caption="NOMBRE" FieldName="NOMBRE" Name="NOMBRE" />
                                                                            <dx:ListBoxColumn Caption="ID_PRODUCTO" FieldName="ID_PRODUCTO" Name="ID_PRODUCTO" Width="0px" />
                                                                        </Columns>
                                                                        <ClearButton Visibility="Auto">
                                                                        </ClearButton>
                                                                        <ValidationSettings>
                                                                            <RequiredField ErrorText="" />
                                                                        </ValidationSettings>
                                                                        <ReadOnlyStyle BackColor="#DEE8DB">
                                                                        </ReadOnlyStyle>
                                                                        <FocusedStyle BackColor="#FFFFCC">
                                                                        </FocusedStyle>
                                                                        <DisabledStyle BackColor="#DEE8DB">
                                                                        </DisabledStyle>
                                                                    </dx:ASPxComboBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Unidad de Medida:</td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <dx:ASPxComboBox ID="cmbUnidadMedida" runat="server" ClientInstanceName="cmbUnidadMedida" Font-Size="X-Small" TextField="NOMBRE" TextFormatString="{0}" ValueField="ID_CATALOGO" Width="160px" Theme="Metropolis" ToolTip="Seleccione una unidad de medida">
                                                                        <Columns>
                                                                            <dx:ListBoxColumn Caption="NOMBRE" FieldName="NOMBRE" Name="NOMBRE" />
                                                                            <dx:ListBoxColumn Caption="ID_CATALOGO" FieldName="ID_CATALOGO" Name="ID_CATALOGO" Width="0px" />
                                                                            <dx:ListBoxColumn Caption="CODIGO" FieldName="CODIGO" Name="CODIGO" Width="0px" />
                                                                        </Columns>
                                                                        <ClearButton Visibility="Auto">
                                                                        </ClearButton>
                                                                        <ReadOnlyStyle BackColor="#DEE8DB">
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
                                                                    <dx:ASPxButton ID="btnAgregar" runat="server" ClientInstanceName="btnAgregar" AutoPostBack="False" ImagePosition="Right" Text="Agregar" Theme="Default" ToolTip="Agregar Producto" Width="100px">
                                                                        <ClientSideEvents Click="function(s, e) {
	
	                                dgvHidrocarburo.PerformCallback();
                                }" />
                                                                        <Image IconID="navigation_forward_16x16">
                                                                        </Image>
                                                                    </dx:ASPxButton>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td style="width: 75%">
                                                        <dx:ASPxGridView ID="dgvHidrocarburo" runat="server" AutoGenerateColumns="False" ClientInstanceName="dgvHidrocarburo" EnableTheming="True" Font-Size="Smaller" KeyFieldName="ID_IDENTIFICADOR" OnHtmlRowPrepared="dgvHidrocarburo_HtmlRowPrepared" Theme="Default" Width="100%" OnCustomButtonCallback="dgvHidrocarburo_CustomButtonCallback" OnCustomCallback="dgvHidrocarburo_CustomCallback">
                                                            <ClientSideEvents CustomButtonClick="function(s, e) {
                                switch(e.buttonID) {

                                case&nbsp; 'btnHidQuitar':
                                e.processOnServer = confirm('¿Desea quitar el registro del hidrocarburo?'); 
                                break;

                                } 
                                }" />
                                                            <SettingsContextMenu EnableRowMenu="False">
                                                            </SettingsContextMenu>
                                                            <Templates>
                                                                <GroupFooterRow>
                                                                    <dx:ASPxLabel ID="ASPxLabel54" runat="server" Text="ASPxLabel">
                                                                    </dx:ASPxLabel>
                                                                </GroupFooterRow>
                                                                <EditForm>
                                                                </EditForm>
                                                            </Templates>
                                                            <SettingsPager NumericButtonCount="5" PageSize="50">
                                                                <PageSizeItemSettings Items="50, 100, 200, 500, 1000" Visible="True">
                                                                </PageSizeItemSettings>
                                                            </SettingsPager>
                                                            <SettingsEditing Mode="Inline">
                                                            </SettingsEditing>
                                                            <Settings ShowHeaderFilterButton="True" />
                                                            <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" />
                                                            <SettingsPopup>
                                                                <EditForm HorizontalAlign="Center" Modal="True" VerticalAlign="Middle" VerticalOffset="0" />
                                                                <CustomizationWindow HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" />
                                                            </SettingsPopup>
                                                            <SettingsText CommandNew="Nuevo" />
                                                            <Columns>
                                                                <dx:GridViewCommandColumn ShowInCustomizationForm="True" VisibleIndex="20" ToolTip="Funciones">
                                                                    <CustomButtons>
                                                                        <dx:GridViewCommandColumnCustomButton ID="btnHidQuitar" Text="Quitar">
                                                                        </dx:GridViewCommandColumnCustomButton>
                                                                    </CustomButtons>
                                                                    <CellStyle ForeColor="#006600">
                                                                    </CellStyle>
                                                                </dx:GridViewCommandColumn>
                                                                <dx:GridViewDataTextColumn Caption="ID_AUTORIZACION_CONDICION" FieldName="ID_AUTORIZACION_CONDICION" ShowInCustomizationForm="True" VisibleIndex="13" Visible="False">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="ID_IDENTIFICADOR" FieldName="ID_IDENTIFICADOR" ShowInCustomizationForm="True" VisibleIndex="15" Visible="False">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="PRODUCTO" FieldName="NOMBRE" ShowInCustomizationForm="True" VisibleIndex="16">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="UNIDAD MEDIDA" FieldName="SUBID_NOMBRE" ShowInCustomizationForm="True" VisibleIndex="18">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="OBSERV." FieldName="OBSERVACIONES" ShowInCustomizationForm="True" VisibleIndex="19" ToolTip="Observaciones">
                                                                    <DataItemTemplate>
                                                                        <dx:ASPxTextBox ID="txtObservacion" runat="server" Text='<%# Eval("OBSERVACIONES") %>' Width="100%">
                                                                            <Border BorderStyle="None" />
                                                                        </dx:ASPxTextBox>
                                                                    </DataItemTemplate>
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="ESTADO" FieldName="ESTADO" ShowInCustomizationForm="True" Visible="False" VisibleIndex="21">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="ID_SUB_IDENTIFICADOR" FieldName="ID_SUB_IDENTIFICADOR" ShowInCustomizationForm="True" Visible="False" VisibleIndex="17">
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
                                                    <td>&nbsp;</td>
                                                    <td style="text-align: right;">
                                                        <dx:ASPxButton ID="btnGuardaHu" runat="server" Text="Guardar Configuración" Theme="Metropolis" Width="200px" AutoPostBack="False" OnClick="btnGuardaHu_Click" Font-Bold="True" ForeColor="Maroon">
                                                            <Image IconID="actions_apply_16x16">
                                                            </Image>
                                                        </dx:ASPxButton>
                                                        <dx:ASPxButton ID="btnHiEliminar" runat="server" AutoPostBack="False" ClientInstanceName="btnHiEliminar" EnableTheming="True" Font-Bold="False" OnClick="btnHiEliminar_Click" Text="Eliminar Hidrocarburos" Theme="Office2010Silver" Width="200px">
                                                            <ClientSideEvents Click="function(s, e) {
e.processOnServer = confirm('¿Desea ELIMINAR la configuración de hidrocarburos de toda la actividad? - Una vez realizada la acción no podrá volver atras. ¿Desea continuar?'); 
}
" />
                                                            <Image IconID="spreadsheet_deletesheetcells_16x16">
                                                            </Image>
                                                        </dx:ASPxButton>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <hr />
                                                    </td>


                                                </tr>
                                                <tr>
                                                    <td class="auto-style24">
                                                        <dx:ASPxLabel ID="ASPxLabel62" runat="server" Font-Bold="True" Font-Size="Smaller" Font-Underline="True" ForeColor="#333333" Text="INTEGRACIÓN SIREL">
                                                        </dx:ASPxLabel>
                                                    </td>
                                                    <td style="text-align: right;" class="auto-style24"></td>
                                                </tr>
                                                <tr>
                                                    <td>Unidad de Medida:<dx:ASPxComboBox ID="cmbUnidadMedidaAux" runat="server" ClientInstanceName="cmbUnidadMedidaAux" Font-Size="X-Small" TextField="NOMBRE" TextFormatString="{0}" Theme="Metropolis" ToolTip="Seleccione una unidad de medida" ValueField="ID_CATALOGO" Width="160px">
                                                        <Columns>
                                                            <dx:ListBoxColumn Caption="NOMBRE" FieldName="NOMBRE" Name="NOMBRE" />
                                                            <dx:ListBoxColumn Caption="ID_CATALOGO" FieldName="ID_CATALOGO" Name="ID_CATALOGO" Width="0px" />
                                                            <dx:ListBoxColumn Caption="CODIGO" FieldName="CODIGO" Name="CODIGO" Width="0px" />
                                                        </Columns>
                                                        <ClearButton Visibility="Auto">
                                                        </ClearButton>
                                                        <ReadOnlyStyle BackColor="#DEE8DB">
                                                        </ReadOnlyStyle>
                                                        <FocusedStyle BackColor="#FFFFCC">
                                                        </FocusedStyle>
                                                        <DisabledStyle BackColor="#DEE8DB">
                                                        </DisabledStyle>
                                                    </dx:ASPxComboBox>
                                                    </td>
                                                    <td style="text-align: right;">
                                                        <dx:ASPxButton ID="btnGuardaSirel" runat="server" AutoPostBack="False" OnClick="btnGuardaSirel_Click" Text="Configuración SIREL" Theme="Metropolis" Width="200px" Font-Bold="True" ForeColor="Maroon">
                                                            <Image IconID="actions_apply_16x16">
                                                            </Image>
                                                        </dx:ASPxButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:TabPage>
                                <dx:TabPage Text="Proyectos Relacionados">
                                    <ContentCollection>
                                        <dx:ContentControl runat="server">

                                            <table style="width: 100%">
                                                <tr>
                                                    <td colspan="2" class="auto-style1">

                                                        <dx:ASPxButton ID="btnCargaEnt" runat="server" Text="Cargar Proyectos" AutoPostBack="False" OnClick="btnCargaEnt_Click" Theme="Default">
                                                            <Image IconID="navigation_next_16x16">
                                                            </Image>
                                                        </dx:ASPxButton>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 30%">Listado de Proyectos :</td>
                                                    <td class="auto-style23">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 30%">
                                                        <div style="overflow: auto; width: 200px; height: 300px; border: 1px">
                                                            <dx:ASPxTreeView ID="trvEntidades" runat="server"
                                                                AllowCheckNodes="True"
                                                                AllowSelectNode="True"
                                                                CheckNodesRecursive="True"
                                                                Font-Size="Smaller" Theme="Office2003Olive" Width="100%">
                                                                <Styles>
                                                                    <NodeText VerticalAlign="Top">
                                                                    </NodeText>
                                                                </Styles>
                                                                <Border BorderStyle="Solid" BorderWidth="1px" />
                                                            </dx:ASPxTreeView>
                                                        </div>
                                                    </td>
                                                    <td class="auto-style23">
                                                        <dx:ASPxGridView ID="dgvEntidad" runat="server" AutoGenerateColumns="False" ClientInstanceName="dgvEntidad" EnableTheming="True" Font-Size="Smaller" KeyFieldName="ID_SUB_IDENTIFICADOR" Theme="Default" Width="100%" OnCustomCallback="dgvEntidad_CustomCallback" OnCustomButtonCallback="dgvEntidad_CustomButtonCallback" OnHtmlRowPrepared="dgvEntidad_HtmlRowPrepared">
                                                            <ClientSideEvents CustomButtonClick="function(s, e) {

switch(e.buttonID) {

case&nbsp; 'btnQuitar':
e.processOnServer = confirm('¿Desea quitar el registro del proyecto?'); 
break;

} 

}
" />
                                                            <SettingsContextMenu EnableRowMenu="False">
                                                            </SettingsContextMenu>
                                                            <Templates>
                                                                <GroupFooterRow>
                                                                    <dx:ASPxLabel ID="ASPxLabel61" runat="server" Text="ASPxLabel">
                                                                    </dx:ASPxLabel>
                                                                </GroupFooterRow>
                                                                <EditForm>
                                                                </EditForm>
                                                            </Templates>
                                                            <SettingsPager NumericButtonCount="1" PageSize="1000">
                                                                <PageSizeItemSettings Items="50, 100, 200, 500, 1000" Visible="True">
                                                                </PageSizeItemSettings>
                                                            </SettingsPager>
                                                            <SettingsEditing Mode="PopupEditForm">
                                                            </SettingsEditing>
                                                            <Settings ShowHeaderFilterButton="True" VerticalScrollableHeight="250" VerticalScrollBarMode="Auto" />
                                                            <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" />
                                                            <SettingsPopup>
                                                                <EditForm HorizontalAlign="Center" Modal="True" VerticalAlign="Middle" VerticalOffset="0" />
                                                                <CustomizationWindow HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" />
                                                            </SettingsPopup>
                                                            <SettingsText CommandNew="Nuevo" />
                                                            <Columns>
                                                                <dx:GridViewCommandColumn ShowInCustomizationForm="True" VisibleIndex="21">
                                                                    <CustomButtons>
                                                                        <dx:GridViewCommandColumnCustomButton ID="btnQuitar" Text="Quitar">
                                                                        </dx:GridViewCommandColumnCustomButton>
                                                                    </CustomButtons>
                                                                    <CellStyle ForeColor="#006600">
                                                                    </CellStyle>
                                                                </dx:GridViewCommandColumn>
                                                                <dx:GridViewDataTextColumn Caption="ACTIVIDAD" FieldName="SUBID_NOMBRE" ShowInCustomizationForm="True" VisibleIndex="2">
                                                                    <Settings AutoFilterCondition="Contains" />
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="ID_SUB_IDENTIFICADOR" FieldName="ID_SUB_IDENTIFICADOR" ShowInCustomizationForm="True" Visible="False" VisibleIndex="1">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="ID" FieldName="ID_IDENTIFICADOR" ShowInCustomizationForm="True" VisibleIndex="3">
                                                                    <Settings AutoFilterCondition="Contains" />
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="RAZON SOCIAL" FieldName="NOMBRE" ShowInCustomizationForm="True" VisibleIndex="4">
                                                                    <Settings AutoFilterCondition="Contains" />
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="ALIAS" FieldName="DESCRIPCION" Name="strAlias" ShowInCustomizationForm="True" VisibleIndex="10">
                                                                    <DataItemTemplate>
                                                                        <dx:ASPxTextBox ID="txtAlias" runat="server" Text='<%# Eval("DESCRIPCION") %>' Width="100%">
                                                                            <Border BorderStyle="None" />
                                                                        </dx:ASPxTextBox>
                                                                    </DataItemTemplate>
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="ID_AUTORIZACION_CONDICION" FieldName="ID_AUTORIZACION_CONDICION" ShowInCustomizationForm="True" Visible="False" VisibleIndex="0">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="ESTADO" FieldName="ESTADO" ShowInCustomizationForm="True" Visible="False" VisibleIndex="12">
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
                                                    <td style="text-align: right">
                                                        <dx:ASPxButton ID="btnAgregarEnt" runat="server" AutoPostBack="False" Font-Bold="False" ImagePosition="Right" Text="Agregar Proyectos" Theme="Default">
                                                            <ClientSideEvents Click="function(s, e) {
dgvEntidad.PerformCallback();
//pnlEntidad.PerformCallback();
}" />
                                                            <Image IconID="navigation_forward_16x16">
                                                            </Image>
                                                        </dx:ASPxButton>
                                                    </td>
                                                    <td style="text-align: right">
                                                        <dx:ASPxButton ID="btnGuardaEn" runat="server" AutoPostBack="False" OnClick="btnGuardaEn_Click" Text="Guardar Configuración" Theme="Metropolis" Width="200px" Font-Bold="True" ForeColor="Maroon">
                                                            <Image IconID="actions_apply_16x16">
                                                            </Image>
                                                        </dx:ASPxButton>
                                                        <dx:ASPxButton ID="btnPrEliminar" runat="server" AutoPostBack="False" ClientInstanceName="btnPrEliminar" EnableTheming="True" Font-Bold="False" OnClick="btnPrEliminar_Click" Text="Eliminar Proy. Rel." Theme="Office2010Silver" Width="200px">
                                                            <ClientSideEvents Click="function(s, e) {
e.processOnServer = confirm('¿Desea ELIMINAR la configuración de los proyectos relacionados de toda la actividad? - Una vez realizada la acción no podrá volver atras. ¿Desea continuar?'); 
}
" />
                                                            <Image IconID="spreadsheet_deletesheetcells_16x16">
                                                            </Image>
                                                        </dx:ASPxButton>
                                                    </td>
                                                </tr>
                                            </table>





                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:TabPage>
                                <dx:TabPage Text="Definicion de Cuentas">
                                    <ContentCollection>
                                        <dx:ContentControl runat="server">
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="width: 30%">

                                                        <table style="width: 100%">
                                                            <tr>
                                                                <td>Id. Padre :
                                                                    <dx:ASPxTextBox ID="txtCtaId" runat="server" BackColor="#FFE1C4" ClientInstanceName="txtCtaId" Font-Size="Smaller" ReadOnly="True" Style="margin-bottom: 0px" Text="0" Width="90%">
                                                                    </dx:ASPxTextBox>

                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Código Padre :
                                                                    <dx:ASPxTextBox ID="txtCtaPadre" runat="server" BackColor="#FFE1C4" ClientInstanceName="txtCtaPadre" Font-Size="Smaller" ReadOnly="True" Width="90%">
                                                                    </dx:ASPxTextBox>

                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Código de Cuenta :
                                                                    <dx:ASPxTextBox ID="txtCtaCodigo" runat="server" ClientInstanceName="txtCtaCodigo" Font-Size="Smaller" Width="90%">
                                                                    </dx:ASPxTextBox>

                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Descripcion de la Cuenta :
                                                                    <dx:ASPxTextBox ID="txtCtaDescripcion" runat="server" ClientInstanceName="txtCtaDescripcion" Font-Size="Smaller" Width="90%">
                                                                    </dx:ASPxTextBox>

                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>

                                                                    <dx:ASPxButton ID="btnLimpiar" runat="server" AutoPostBack="False" EnableTheming="True" Font-Bold="False" Text="Limpiar" Theme="Default" Width="90%">
                                                                        <ClientSideEvents Click="function(s, e) {
txtCtaId.SetValue('0');
txtCtaCodigo.SetValue('');
txtCtaDescripcion.SetValue('');
txtCtaPadre.SetValue('');

btnPfGrabar.SetText('Agregar Padre');
}" />
                                                                        <Image IconID="actions_new_16x16">
                                                                        </Image>
                                                                    </dx:ASPxButton>

                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>

                                                                    <dx:ASPxButton ID="btnPfGrabar" runat="server" ClientInstanceName="btnPfGrabar" EnableTheming="True" Font-Bold="False" Text="Agregar Padre" Theme="Default" Width="90%" OnClick="btnPfGrabar_Click" AutoPostBack="False" ImagePosition="Right">
                                                                        <Image IconID="navigation_forward_16x16">
                                                                        </Image>
                                                                    </dx:ASPxButton>

                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>

                                                                    <dx:ASPxButton ID="btnDcEliminar" runat="server" AutoPostBack="False" ClientInstanceName="btnDcEliminar" EnableTheming="True" Font-Bold="False" OnClick="btnDcEliminar_Click" Text="Eliminar Cuentas" Theme="Office2010Silver" Width="90%">
                                                                        <ClientSideEvents Click="function(s, e) {
e.processOnServer = confirm('¿Desea ELIMINAR la configuración de cuentas de toda la actividad? - Una vez realizada la acción no podrá volver atras. ¿Desea continuar?'); 
}
" />
                                                                        <Image IconID="spreadsheet_deletesheetcells_16x16">
                                                                        </Image>
                                                                    </dx:ASPxButton>

                                                                </td>
                                                            </tr>
                                                        </table>

                                                    </td>
                                                    <td style="width: 70%">

                                                        <dx:ASPxGridView ID="dgvCuentas" runat="server" AutoGenerateColumns="False" ClientInstanceName="dgvCuentas" EnableTheming="True" Font-Size="Smaller" KeyFieldName="ID_AUTORIZACION_CONDICION" OnCustomButtonCallback="dgvCuentas_CustomButtonCallback" OnHtmlRowPrepared="dgvCuentas_HtmlRowPrepared" Theme="Default" Width="100%">
                                                            <ClientSideEvents CustomButtonClick="function(s, e) {

txtCtaId.SetValue('0');
txtCtaCodigo.SetValue('');
txtCtaDescripcion.SetValue('');
txtCtaPadre.SetValue('');

switch(e.buttonID) {

case 'btnCtaModificar':
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

case 'btnCtaEliminar':
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
                                                                    <dx:ASPxLabel ID="ASPxLabel65" runat="server" Text="ASPxLabel">
                                                                    </dx:ASPxLabel>
                                                                </GroupFooterRow>
                                                                <EditForm>
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
                                                                <EditForm HorizontalAlign="Center" Modal="True" VerticalAlign="Middle" VerticalOffset="0" />
                                                                <CustomizationWindow HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" />
                                                            </SettingsPopup>
                                                            <SettingsText CommandNew="Nuevo" />
                                                            <Columns>
                                                                <dx:GridViewCommandColumn ShowClearFilterButton="True" ShowInCustomizationForm="True" ToolTip="Funciones" VisibleIndex="21">
                                                                    <CustomButtons>
                                                                        <dx:GridViewCommandColumnCustomButton ID="btnCtaModificar" Text=" " Visibility="Invisible">
                                                                            <Image IconID="edit_edit_16x16" ToolTip="Modificar">
                                                                            </Image>
                                                                        </dx:GridViewCommandColumnCustomButton>
                                                                        <dx:GridViewCommandColumnCustomButton ID="btnCtaEliminar" Text=" ">
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
                                                                <dx:GridViewDataTextColumn Caption="ESTADO" FieldName="ESTADO" ShowInCustomizationForm="True" Visible="False" VisibleIndex="2">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="ID" FieldName="ID_AUTORIZACION_CONDICION" ShowInCustomizationForm="True" ToolTip="Identificador del Tipo de Operación" Visible="False" VisibleIndex="1">
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
                                            </table>
                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:TabPage>
                            </TabPages>
                        </dx:ASPxPageControl>
                    </td>
                    <td>&nbsp;</td>
                </tr>

            </table>
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>
<%-- <dx:ASPxButton ID="btnGuardaTo" runat="server" Text="Guardar Configuración" Theme="Metropolis" Width="200px" OnClick="btnGuardaTo_Click" AutoPostBack="False" Font-Bold="True" ForeColor="Maroon" Visible="False">
                                                            <Image IconID="actions_apply_16x16">
                                                            </Image>-
                                                        </dx:ASPxButton>--%><%-- <dx:ASPxButton ID="btnGuardaTo" runat="server" Text="Guardar Configuración" Theme="Metropolis" Width="200px" OnClick="btnGuardaTo_Click" AutoPostBack="False" Font-Bold="True" ForeColor="Maroon" Visible="False">
                                                            <Image IconID="actions_apply_16x16">
                                                            </Image>-
                                                        </dx:ASPxButton>--%>
<dx:ASPxPopupControl ID="pucImportaMov" runat="server" AllowDragging="True" ClientInstanceName="pucImportaMov" EnableTheming="True" HeaderText="Importar Movimiento" Height="93px" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Theme="Metropolis" Width="500px" OnWindowCallback="pucImportaMov_WindowCallback">
    <HeaderImage IconID="navigation_up_16x16">
    </HeaderImage>
    <HeaderStyle>
        <BorderBottom BorderStyle="Solid" />
    </HeaderStyle>
    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl4" runat="server">
            <table class="dxflInternalEditorTable_Youthful">
                <tr>
                    <td>&nbsp;
                    </td>
                    <td>
                        <dx:ASPxCheckBox ID="chkAccion" runat="server" CheckState="Unchecked" Text="Actividad (LIQ)" Theme="Metropolis" ValueType="System.Int32"  ClientInstanceName="chkAccion">
                            <ClientSideEvents CheckedChanged="function(s, e) {
	pucImportaMov.PerformCallback('changePagerMode');&nbsp; 
}" />
                        </dx:ASPxCheckBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <dx:ASPxLabel ID="lblIdConsumidor" runat="server" Font-Bold="True" ForeColor="#333333" Text="Id Consumidor :">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxTextBox runat="server" ID="txtIdConsumidorMov" ClientInstanceName="txtEessHydro" BackColor="#FFE1C4" ReadOnly="true">
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <dx:ASPxLabel ID="lblEessHydro" runat="server" Font-Bold="True" ForeColor="#333333" Text="Eess Hydro :">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxTextBox runat="server" ID="txtEessHydro" ClientInstanceName="txtEessHydro">
                            <ClientSideEvents TextChanged="function(s, e) {
	                                    s.SetText(s.GetText().toUpperCase());
                                    }" />
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <dx:ASPxLabel ID="lblPrecioEf2" runat="server" ClientInstanceName="lblPrecioEf" Font-Bold="True" ForeColor="#666666" Text="Fecha Inicio :">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxDateEdit ID="detFechaImpIni" runat="server" ClientInstanceName="detFechaImpIni" EnableTheming="True" Height="23px" Theme="Metropolis" ToolTip="Seleccione la fecha inicial" Width="170px">
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
                    <td style="text-align: right">
                        <dx:ASPxLabel ID="lblPrecioEf3" runat="server" ClientInstanceName="lblPrecioEf" Font-Bold="True" ForeColor="#666666" Text="Fecha Fin :">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxDateEdit ID="detFechaImpFin" runat="server" ClientInstanceName="detFechaImpFin" EnableTheming="True" Height="23px" Theme="Metropolis" ToolTip="Seleccione la fecha inicial" Width="170px">
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
                        <dx:ASPxLabel ID="lblEessbsisa" runat="server" Font-Bold="True" ForeColor="#333333" Text="Eess B-Sisa :">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxComboBox ID="cmbEessBsa" runat="server" ClientInstanceName="cmbEessBsa" TextField="NOMBRE" TextFormatString="{0}" Theme="Metropolis" ValueField="ID_EESS" Width="100%">
                            <Columns>
                                <dx:ListBoxColumn Caption="DENOMINACION" FieldName="DENOMINACION" Name="DENOMINACION" />
                                <dx:ListBoxColumn Caption="DEPTO" FieldName="DEPTO" Name="DEPTO" />
                            </Columns>
                            <ClearButton Visibility="Auto">
                            </ClearButton>
                            <ReadOnlyStyle BackColor="#DEE8DB">
                            </ReadOnlyStyle>
                            <DisabledStyle ForeColor="Black">
                            </DisabledStyle>
                        </dx:ASPxComboBox>
                </tr>
                <tr>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel5" runat="server" Font-Bold="True" ForeColor="#333333" Text="Id Consumidor B-Sisa:">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxTextBox runat="server" ID="txtIdConsumidorBsa" ClientInstanceName="txtIdConsumidorBsa" BackColor="#FFE1C4" ReadOnly="true">
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <dx:ASPxButton ID="btnImportarMov" runat="server" Font-Bold="True" ForeColor="Maroon" Text="Importar Movimientos" Theme="Metropolis" Width="100%" OnClick="btnImportarMov_Click">
                            <ClientSideEvents Click="function(s, e) { if(confirm('¿Desea IMPORTAR la data BSA? - Una vez realizada la acción no podrá volver atras. ¿El proceso puede tardar 6 min, desea continuar?')){LoadingPanel.Show();}else{}}" />
                            <Image IconID="actions_apply_16x16">
                            </Image>
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>
<%----%><%----%>
<script type="text/javascript">
    function OnCheckedChanged(s, e) {
        label.SetText("Checked nodeoooos:");
        ListCheckedNodes(s);
    }

    function ListCheckedNodes(parent) {
        alert(parent.GetSelectedNode().name);
        for (var i = 0; i < parent.GetNodeCount() ; i++) {
            if (parent.GetNode(i).GetChecked()) {
                label.SetText(label.GetText() + " " + parent.GetNode(i).GetText());
            }
            if (parent.GetNode(i).GetNodeCount() != 0) {
                ListCheckedNodes(parent.GetNode(i));
            }
        }
    }

</script>





