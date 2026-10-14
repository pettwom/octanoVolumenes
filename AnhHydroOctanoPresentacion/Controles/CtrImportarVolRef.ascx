<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtrImportarVolRef.ascx.cs" Inherits="AnhHydroOctanoPresentacion.Controles.CtrImportarVolRef" %>

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
    
.dxpcLite_Metropolis .dxpc-header,
.dxdpLite_Metropolis .dxpc-header 
{
	color: #ff8800;
	font-size: 1.66em;
	padding: 7px 2px 7px 12px;
	white-space: nowrap;
}

.dxflInternalEditorTable_Youthful {
    width: 100%;
}
    
</style>

<div class="col-md-12">
    <table style="width: 100%">
        <tr>
            <td style="text-align: center; padding-bottom: 1px">
                <dx:ASPxRoundPanel runat="server" ShowHeader="False" Width="100%" ID="pnlTitulo">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <dx:ASPxLabel runat="server" ForeColor="#367B2F" ID="lblTitulo" Font-Bold="True" Font-Names="Rockwell" Font-Size="Larger" Text="VOLUMENES DE CARGA REFERENCIALES"></dx:ASPxLabel>
                        </dx:PanelContent>
                    </PanelCollection>
                    <Border BorderStyle="Ridge" BorderWidth="1px" BorderColor="#003300"></Border>
                </dx:ASPxRoundPanel>
            </td>
        </tr>
    </table>
    
    <table style="width: 100%">
        <tr>
            <td colspan="3">
                <legend>
                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Rockwell" Font-Size="Medium" ForeColor="#367B2F" Text="LISTADO DE REGISTROS ACTIVOS"></asp:Label>
                </legend>
            </td>
        </tr>
        <tr>
            <td style="width: 64%"></td>
            <td style="width: 18%">
                
                <dx:ASPxButton ID="btnCargaDatos" runat="server" AutoPostBack="False" Text="Actualizar Información" Font-Bold="True" ForeColor="Maroon" Width="100%" EnableTheming="True" Theme="Metropolis">
                    <ClientSideEvents Click="function(s, e) {
	pnlDatos.PerformCallback();
}" />
                    <Image IconID="actions_reset2_16x16">
                    </Image>
                </dx:ASPxButton>
                
            </td>
            <td style="width: 18%">
                
                <dx:ASPxButton ID="btnImpMasivo" runat="server" Text="Importar Información" AutoPostBack="False" Font-Bold="True" ForeColor="Maroon" Theme="Metropolis" Width="100%">
                    <ClientSideEvents Click="function(s, e) {
pucImportacion.PerformCallback();
pucImportacion.Show();
}" />
                    <Image IconID="actions_converttorange_16x16">
                    </Image>
                </dx:ASPxButton>
                
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <dx:ASPxCallbackPanel ID="pnlDatos" runat="server" Theme="Default" Width="100%" ClientInstanceName="pnlDatos" OnCallback="pnlDatos_Callback">
                            <ClientSideEvents EndCallback="function(s, e) {
LoadingPanel.Hide();
}" BeginCallback="function(s, e) {
LoadingPanel.Show();
}" Init="function(s, e) {
	pnlDatos.PerformCallback();
}" />
                            <PanelCollection>
                                <dx:PanelContent runat="server">
                                    
                                    <table style="width: 100%;">
                                        <tr>
                                            <td style="text-align: center;">
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
                                            <td>
                                                <dx:ASPxGridView ID="dgvCabecera" runat="server" AutoGenerateColumns="False" ClientInstanceName="dgvCabecera" EnableTheming="True" Font-Size="X-Small" KeyFieldName="ID_VOLUMEN_REFERENCIAL" Theme="Default" Width="100%" >
                                                    <SettingsContextMenu EnableColumnMenu="True" Enabled="True">
                                                    </SettingsContextMenu>
                                                    <Templates>
                                                        <EditForm>
                                                        </EditForm>
                                                    </Templates>
                                                    <SettingsPager PageSize="100" AlwaysShowPager="True">
                                                        <PageSizeItemSettings Items="10, 100, 1000" Visible="True" ShowAllItem="True">
                                                        </PageSizeItemSettings>
                                                    </SettingsPager>
                                                    <SettingsEditing Mode="EditForm">
                                                    </SettingsEditing>
                                                    <Settings ShowFilterRow="True" ShowFooter="True" ShowHeaderFilterButton="True" />
                                                    <SettingsBehavior AllowFocusedRow="True" EnableCustomizationWindow="True" ColumnResizeMode="NextColumn" />
                                                    <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
                                                    <SettingsPopup>
                                                        <EditForm Modal="True" />
                                                        <CustomizationWindow HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" />
                                                    </SettingsPopup>
                                                    <SettingsText CommandNew="Nuevo" />
                                                    <Columns>
                                                        <dx:GridViewCommandColumn ShowClearFilterButton="True" ShowInCustomizationForm="True" ToolTip="Funciones" Visible="False" VisibleIndex="38">
                                                            <CustomButtons>
                                                                <dx:GridViewCommandColumnCustomButton ID="btnHistorial" Text=" ">
                                                                    <Image IconID="mail_contact_16x16" ToolTip="Ver Tarjetas Registradas">
                                                                    </Image>
                                                                </dx:GridViewCommandColumnCustomButton>
                                                                <dx:GridViewCommandColumnCustomButton ID="btnFotos" Text=" ">
                                                                    <Image IconID="toolboxitems_picturebox_16x16" ToolTip="Ver Fotografías">
                                                                    </Image>
                                                                </dx:GridViewCommandColumnCustomButton>
                                                                <dx:GridViewCommandColumnCustomButton ID="btnConsumo" Text=" ">
                                                                    <Image IconID="functionlibrary_financial_16x16" ToolTip="Ver Consumo Registrado">
                                                                    </Image>
                                                                </dx:GridViewCommandColumnCustomButton>
                                                            </CustomButtons>
                                                        </dx:GridViewCommandColumn>
                                                        <dx:GridViewDataTextColumn Caption="ID_VOLUMEN_REFERENCIAL" FieldName="ID_VOLUMEN_REFERENCIAL" ShowInCustomizationForm="True" Visible="False" VisibleIndex="0">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="ID_EXPLOTADOR" FieldName="ID_EXPLOTADOR" ShowInCustomizationForm="True" Visible="False" VisibleIndex="1">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="EXPLOTADOR" FieldName="EXPLOTADOR" ShowInCustomizationForm="True" VisibleIndex="2">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="MODELO" FieldName="MODELO" ShowInCustomizationForm="True" VisibleIndex="3">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="COD. AEROP. ORIGEN" FieldName="ID_AEROPUERTO_ORIGEN" ShowInCustomizationForm="True" VisibleIndex="4" ToolTip="Código del aeropuerto origen">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="AEROP. ORIGEN" FieldName="AEROPUERTO_ORIGEN" ShowInCustomizationForm="True" VisibleIndex="5">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="COD. AEROP. DESTINO" FieldName="ID_AEROPUERTO_DESTINO" ShowInCustomizationForm="True" VisibleIndex="6" ToolTip="Código del aeropuerto destino">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="AEROP. DESTINO" FieldName="AEROPUERTO_DESTINO" ShowInCustomizationForm="True" VisibleIndex="7">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="COD. AEROP. ALTERNO" FieldName="ID_AEROPUERTO_ALTERNO" ShowInCustomizationForm="True" VisibleIndex="8" ToolTip="Código del aeropuerto alterno">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="AEROP. ALTERNO" FieldName="AEROPUERTO_ALTERNO" ShowInCustomizationForm="True" VisibleIndex="9">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="TIEMPO DE VUELO" FieldName="TIEMPO_VUELO" ShowInCustomizationForm="True" VisibleIndex="10">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="CONSUMO X HORA DE VUELO" FieldName="CONSUMO_HORA_VUELO" ShowInCustomizationForm="True" VisibleIndex="11">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="TIEMPO HRS. BLOCK" FieldName="TIEMPO_HORAS_BLOCK" ShowInCustomizationForm="True" VisibleIndex="12">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="TANQUEO MAXIMO REQUERIDO" FieldName="TANQUEO_MAXIMO_REQUERIDO" ShowInCustomizationForm="True" VisibleIndex="13">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="TIEMPO SOBREVUELO MINIMO" FieldName="TIEMPO_SOBREVUELO_MINIMO" ShowInCustomizationForm="True" VisibleIndex="14">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="APP_ID_USUARIO" FieldName="APP_ID_USUARIO" ShowInCustomizationForm="True" Visible="False" VisibleIndex="15">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="USUARIO REG." FieldName="REGISTRO_USUARIO" ShowInCustomizationForm="True" VisibleIndex="16">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="FECHA REG." FieldName="REGISTRO_FECHA" ShowInCustomizationForm="True" VisibleIndex="17">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="MATRICULA" FieldName="MATRICULA" ShowInCustomizationForm="True" Visible="False" VisibleIndex="19">
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                    <Styles CssPostfix="DevGlp">
<Header HorizontalAlign="Center" Wrap="True"></Header>

                                                        <AlternatingRow BackColor="#EEEEEE">
                                                        </AlternatingRow></Styles>
                                                    <Styles>
                                                        <Header HorizontalAlign="Center" Wrap="True">
                                                        </Header>
                                                    </Styles>
                                                </dx:ASPxGridView>
                                            </td>
                                        </tr>
                                    </table>

                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxCallbackPanel>
            </td>
        </tr>
    
    
    </table>
    
</div>




    <dx:ASPxPopupControl ID="pucImportacion" runat="server" CloseAction="CloseButton" CloseOnEscape="True" Modal="True"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Above" ClientInstanceName="pucImportacion"
        HeaderText="Importacion Masiva" AllowDragging="True" PopupAnimationType="None" EnableViewState="False" Maximized="True" ShowMaximizeButton="True" Theme="Metropolis" ScrollBars="Auto" AllowResize="True" >
        <HeaderImage IconID="grid_horizontalgridlinesmajorminor_32x32">
        </HeaderImage>
        
        <HeaderStyle>
        <BorderBottom BorderStyle="Solid" />
        </HeaderStyle>
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <dx:ASPxCallbackPanel ID="pnlTransaccion" runat="server" DefaultButton="btOK" ClientInstanceName="pnlTransaccion">
                    <ClientSideEvents BeginCallback="function(s, e) {
	LoadingPanel.Show();
}" EndCallback="function(s, e) {
	LoadingPanel.Hide();
}" />
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            
                            <table style="width: 100%;">
                                        

                                        <tr>
                                            <td colspan="3" style="padding-bottom: 5px; width: 15%">
                                                <dx:ASPxLabel ID="ASPxLabel55" runat="server" ForeColor="#666666" Text="Explotador :">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td style="padding-bottom: 5px; width: 20%">
                                                &nbsp;</td>
                                            <td colspan="2" style="padding-bottom: 5px; width: 65%">
                                                &nbsp;</td>
                                        </tr>

                                        <tr>
                                            <td colspan="3" style="padding-bottom: 5px; width: 15%">
                                                <dx:ASPxComboBox ID="cmbExplotador" runat="server" TextField="PARAMETRICA" TextFormatString="{0}" Theme="Metropolis" ValueField="ID_PARAMETRICA" Width="100%" ValueType="System.Int32">
                                                    <ClientSideEvents LostFocus="function(s, e) {
if(s.GetText()=='')
{
s.SetSelectedIndex(0); 
}
}" />
                                                    <Columns>
                                                        <dx:ListBoxColumn Caption="ID" FieldName="ID_PARAMETRICA" Visible="False" />
                                                        <dx:ListBoxColumn Caption="EXPLOTADOR" FieldName="PARAMETRICA" />
                                                    </Columns>
                                                    <ClearButton Visibility="Auto">
                                                    </ClearButton>
                                                </dx:ASPxComboBox>
                                            </td>
                                            <td style="padding-bottom: 5px; width: 20%">
                                                <asp:FileUpload ID="FileUploader" runat="server" ToolTip="Seleccione el archivo .CSV" Width="100%" />
                                            </td>
                                            <td colspan="2" style="padding-bottom: 5px; width: 65%">
                                                <dx:ASPxButton ID="btnImportar" runat="server" AutoPostBack="False" Font-Bold="True" ForeColor="Maroon" OnClick="btnImportar_Click" Text="Visualizar Registros" Theme="Metropolis">
                                                    <ClientSideEvents Click="function(s, e) {
	    LoadingPanel.Show();
    }" />
                                                    <Image IconID="zoom_zoom_16x16">
                                                    </Image>
                                                </dx:ASPxButton>
                                                <dx:ASPxButton ID="btnGrabar" runat="server" Enabled="False" Font-Bold="True" ForeColor="Maroon" OnClick="btnGrabar_Click" Text="Importar Registros" Theme="Metropolis">
                                                    <ClientSideEvents Click="function(s, e) {
    if(confirm('¿Desea importar los registros de volúmenes referenciales?'))
    {
    LoadingPanel.Show();
    e.processOnServer = true;
    }
    else
    {
    e.processOnServer = false;
    }

    }" />
                                                    <Image IconID="actions_addfile_16x16">
                                                    </Image>
                                                </dx:ASPxButton>
                                            </td>
                                        </tr>
                                        <tr>
                                        
                                            <td colspan="6">
                                                <dx:ASPxGridView ID="dgvTransacciones" runat="server" Theme="Default" AutoGenerateColumns="False" KeyFieldName="ID_VOLUMEN_REFERENCIAL" EnableTheming="True" Font-Size="Smaller" ClientInstanceName="dgvTransacciones" Width="100%" >
                                                    <SettingsContextMenu EnableRowMenu="False">
                                                    </SettingsContextMenu>
                                                    <Templates>
                                                        <EditForm >
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="ID :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtId" runat="server" ClientInstanceName="txtId" Width="170px" ReadOnly="True">
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                </tr>

                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="Fecha :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxDateEdit ID="detFecha" runat="server" EditFormat="DateTime" Font-Size="Smaller">
                                                                            <TimeSectionProperties>
                                                                                <TimeEditProperties>
                                                                                    <ClearButton Visibility="Auto">
                                                                                    </ClearButton>
                                                                                </TimeEditProperties>
                                                                            </TimeSectionProperties>
                                                                            <ClearButton Visibility="Auto">
                                                                            </ClearButton>
                                                                            <ValidationSettings>
                                                                                <RequiredField IsRequired="True" />
                                                                            </ValidationSettings>
                                                                        </dx:ASPxDateEdit>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel12" runat="server" Height="16px" Text="Válido Cred. Fiscal :" Width="110px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxComboBox ID="cmbTipoCred" runat="server" Font-Size="Smaller" ValueType="System.Int32">
                                                                            <Items>
                                                                                <dx:ListEditItem Text="SI" Value="1" />
                                                                                <dx:ListEditItem Text="NO" Value="2" />
                                                                            </Items>
                                                                            <ClearButton Visibility="Auto">
                                                                            </ClearButton>
                                                                            <ValidationSettings>
                                                                                <RequiredField IsRequired="True" />
                                                                            </ValidationSettings>
                                                                        </dx:ASPxComboBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel11" runat="server" Text="Tipo Pago :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxComboBox ID="cmbTipoPago" runat="server" Font-Size="Smaller" ValueType="System.Int32">
                                                                            <Items>
                                                                                <dx:ListEditItem Text="EFECTIVO" Value="1" />
                                                                                <dx:ListEditItem Text="PREPAGO" Value="2" />
                                                                                <dx:ListEditItem Text="VALE" Value="3" />
                                                                            </Items>
                                                                            <ClearButton Visibility="Auto">
                                                                            </ClearButton>
                                                                            <ValidationSettings>
                                                                                <RequiredField IsRequired="True" />
                                                                            </ValidationSettings>
                                                                        </dx:ASPxComboBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td><br />
                                                                        <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="Tipo Consumidor :" Width="110px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxComboBox ID="cmbTipoCons" runat="server" Font-Size="Smaller" ValueType="System.Int32">
                                                                            <Items>
                                                                                <dx:ListEditItem Text="MOTORIZADO" Value="1" />
                                                                                <dx:ListEditItem Text="BIDON" Value="2" />
                                                                                <dx:ListEditItem Text="BOTELLA" Value="3" />
                                                                                <dx:ListEditItem Text="OTRO" Value="4" />
                                                                            </Items>
                                                                            <ClearButton Visibility="Auto">
                                                                            </ClearButton>
                                                                            <ValidationSettings>
                                                                                <RequiredField IsRequired="True" />
                                                                            </ValidationSettings>
                                                                        </dx:ASPxComboBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td></td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="Cliente :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtCliente" runat="server" Width="170px" Height="16px" Font-Size="Smaller" MaxLength="90">
                                                                            <ClientSideEvents TextChanged="function(s, e) {
	    s.SetText(s.GetText().toUpperCase());
    }" />
                                                                            <ValidationSettings>
                                                                                <RequiredField IsRequired="True" />
                                                                            </ValidationSettings>
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="NIT :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtNit" runat="server" Font-Size="Smaller" MaxLength="15" Width="170px">
                                                                            <MaskSettings Mask="&lt;0..999999999999999&gt;" PromptChar=" " />
                                                                            <ValidationSettings>
                                                                                <RequiredField IsRequired="True" />
                                                                            </ValidationSettings>
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel14" runat="server" Text="Usuario :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtUsuario" runat="server" ClientInstanceName="txtId" Font-Size="Smaller" MaxLength="100" Width="170px">
                                                                            <ValidationSettings>
                                                                                <RequiredField IsRequired="True" />
                                                                            </ValidationSettings>
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel20" runat="server" Text="Tipo de Venta :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxRadioButtonList ID="rbtTipoVenta" runat="server" SelectedIndex="0" ClientInstanceName="rbtTipoVenta" ValueType="System.Int32">
                                                                            <ClientSideEvents ValueChanged="function(s, e) {
	    console.log('entraaaaaa....');
    alert(&quot;xxxxxxxxxxxx&quot;);
    txtNoAutoExt.SetVisible(false);

    }" />
                                                                            <Items>
                                                                                <dx:ListEditItem Selected="True" Text="Nacional" Value="1" />
                                                                                <dx:ListEditItem Text="Internacional" Value="2" />
                                                                            </Items>
                                                                        </dx:ASPxRadioButtonList>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel19" runat="server" Text="País :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxComboBox ID="cmbPais" runat="server">
                                                                            <Items>
                                                                                <dx:ListEditItem Text="BOLIVIA" Value="68" />
                                                                                <dx:ListEditItem Text="ARGENTINA" Value="32" />
                                                                                <dx:ListEditItem Text="BRASIL" Value="76" />
                                                                                <dx:ListEditItem Text="CHILE" Value="152" />
                                                                                <dx:ListEditItem Text="COLOMBIA" Value="170" />
                                                                                <dx:ListEditItem Text="ECUADOR" Value="218" />
                                                                                <dx:ListEditItem Text="PARAGUAY" Value="600" />
                                                                                <dx:ListEditItem Text="PERU" Value="604" />
                                                                                <dx:ListEditItem Text="URUGUAY" Value="858 " />
                                                                                <dx:ListEditItem Text="VENEZUELA" Value="862" />
                                                                            </Items>
                                                                            <ClearButton Visibility="Auto">
                                                                            </ClearButton>
                                                                            <ValidationSettings>
                                                                                <RequiredField IsRequired="True" />
                                                                            </ValidationSettings>
                                                                        </dx:ASPxComboBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel13" runat="server" Text="Placa :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtPlaca" runat="server" ClientInstanceName="txtId" Font-Size="Smaller" MaxLength="11" Width="170px">
                                                                            <ClientSideEvents TextChanged="function(s, e) {
	    s.SetText(s.GetText().toUpperCase());
    }" />
                                                                            <ValidationSettings>
                                                                                <RequiredField IsRequired="True" />
                                                                            </ValidationSettings>
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel15" runat="server" Text="No. Autorización :" Width="100px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtNoAuto" runat="server" Font-Size="Smaller" MaxLength="15" Width="170px">
                                                                            <MaskSettings Mask="&lt;0..999999999999999&gt;" />
                                                                            <ValidationSettings>
                                                                                <RequiredField IsRequired="True" />
                                                                            </ValidationSettings>
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="No. Factura :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtNoFact" runat="server" Font-Size="Smaller" MaxLength="15" Width="170px">
                                                                            <MaskSettings Mask="&lt;0..999999999999999&gt;" />
                                                                            <ValidationSettings>
                                                                                <RequiredField IsRequired="True" />
                                                                            </ValidationSettings>
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel26" runat="server" Text="Cod. Control :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtCodCtrl" runat="server" Font-Size="Smaller" MaxLength="14" Width="170px">
                                                                            <MaskSettings Mask="" />
                                                                            <ValidationSettings SetFocusOnError="True">
                                                                            </ValidationSettings>
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel18" runat="server" Text="Importe :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtImporte" runat="server" Width="170px">
                                                                            <MaskSettings IncludeLiterals="DecimalSymbol" Mask="&lt;0..9999999&gt;.&lt;00..99&gt;" />
                                                                            <ValidationSettings>
                                                                                <RequiredField IsRequired="True" />
                                                                            </ValidationSettings>
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
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
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                    <td runat="server">
                                                                        <dx:ASPxLabel ID="ASPxLabel21" runat="server" Text="No. Autorización Ext. :" Width="130px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtNoAutoExt" runat="server" ClientInstanceName="txtNoAutoExt" Font-Size="Smaller" MaxLength="15" Width="170px">
                                                                            <MaskSettings Mask="&lt;0..999999999999999&gt;" />
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel22" runat="server" Text="No. Factura Ext. :" Width="120px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtNoFactExt" runat="server" ClientInstanceName="txtNoFactExt" Font-Size="Smaller" MaxLength="15" Width="170px">
                                                                            <MaskSettings Mask="&lt;0..999999999999999&gt;" />
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                    <td >
                                                                        <dx:ASPxLabel ID="ASPxLabel16" runat="server" Text="Cod. Control Ext. :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtCodCtrlExt" runat="server" Font-Size="Smaller" MaxLength="14" Width="170px">
                                                                            <MaskSettings Mask="" />
                                                                            <ValidationSettings SetFocusOnError="True">
                                                                            </ValidationSettings>
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel27" runat="server" Text="Importe Ext. :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtImporteExt" runat="server" Width="170px">
                                                                            <MaskSettings IncludeLiterals="DecimalSymbol" Mask="&lt;0..9999999&gt;.&lt;00..99&gt;" />
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
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
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel25" runat="server" Text="No. Doc. Referencia :" Width="130px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtNoDocRef" runat="server" Font-Size="Smaller" MaxLength="15" Width="170px">
                                                                            <MaskSettings Mask="&lt;0..999999999999999&gt;" />
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel7" runat="server" Text="Tipo Factura :" Visible="False">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxComboBox ID="cmbTipoFact" runat="server" Font-Size="Smaller" style="margin-top: 0" ValueType="System.Int32" Visible="False">
                                                                            <Items>
                                                                                <dx:ListEditItem Text="MANUAL" Value="1" />
                                                                                <dx:ListEditItem Text="COMPUTARIZADA" Value="2" />
                                                                            </Items>
                                                                            <ClearButton Visibility="Auto">
                                                                            </ClearButton>
                                                                        </dx:ASPxComboBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="Estado :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxComboBox ID="cmbEstado" runat="server" Font-Size="Smaller" ValueType="System.Int32">
                                                                            <Items>
                                                                                <dx:ListEditItem Text="VALIDO" Value="1" />
                                                                                <dx:ListEditItem Text="ANULADO" Value="2" />
                                                                            </Items>
                                                                            <ClearButton Visibility="Auto">
                                                                            </ClearButton>
                                                                            <ValidationSettings>
                                                                                <RequiredField IsRequired="True" />
                                                                            </ValidationSettings>
                                                                        </dx:ASPxComboBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel9" runat="server" Text="Producto :" Width="100px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxComboBox ID="cmbProd" runat="server" ValueType="System.Int32" Font-Size="Smaller">
                                                                            <Items>
                                                                                <dx:ListEditItem Text="GNV" Value="1" />
                                                                                <dx:ListEditItem Text="DIESEL OIL" Value="2" />
                                                                                <dx:ListEditItem Text="GASOLINA ESPECIAL" Value="3" />
                                                                                <dx:ListEditItem Text="GASOLINA PREMIUM" Value="4" />
                                                                            </Items>
                                                                            <ClearButton Visibility="Auto">
                                                                            </ClearButton>
                                                                            <ValidationSettings>
                                                                                <RequiredField IsRequired="True" />
                                                                            </ValidationSettings>
                                                                        </dx:ASPxComboBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                    <td>
                                                                        <dx:ASPxLabel ID="ASPxLabel10" runat="server" Text="Cantidad :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td class="auto-style16">
                                                                        <dx:ASPxTextBox ID="txtCantidad" runat="server" Width="170px">
                                                                            <MaskSettings Mask="&lt;0..9999999&gt;.&lt;00..99&gt;" IncludeLiterals="DecimalSymbol" />
                                                                            <ValidationSettings>
                                                                                <RequiredField IsRequired="True" />
                                                                            </ValidationSettings>
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td class="auto-style19">
                                                                        <dx:ASPxLabel ID="ASPxLabel17" runat="server" Text="Precio :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtPrecio" runat="server" Width="170px">
                                                                            <MaskSettings IncludeLiterals="DecimalSymbol" Mask="&lt;0..999&gt;.&lt;00..99&gt;" PromptChar=" " />
                                                                            <ValidationSettings>
                                                                                <RequiredField IsRequired="True" />
                                                                            </ValidationSettings>
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="auto-style13">&nbsp;</td>
                                                                    <td class="auto-style18">&nbsp;</td>
                                                                    <td class="auto-style16">&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td class="auto-style19">&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="auto-style13">&nbsp;</td>
                                                                    <td class="auto-style18">
                                                                        <dx:ASPxLabel ID="ASPxLabel23" runat="server" Text="Autorización de Venta :" Width="150px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td class="auto-style16">
                                                                        <dx:ASPxComboBox ID="cmbAutoVta" runat="server">
                                                                            <Items>
                                                                                <dx:ListEditItem Text="SUSTANCIAS CONTROLADAS" Value="1" />
                                                                                <dx:ListEditItem Text="COMANDO CONJUNTO" Value="2" />
                                                                            </Items>
                                                                            <ClearButton Visibility="Auto">
                                                                            </ClearButton>
                                                                        </dx:ASPxComboBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td class="auto-style19">
                                                                        <dx:ASPxLabel ID="ASPxLabel24" runat="server" Text="No. Hoja Ruta :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtNoAutoVta" runat="server" Font-Size="Smaller" MaxLength="15" Width="170px">
                                                                            <MaskSettings Mask="&lt;0..999999999999999&gt;" />
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="auto-style13">&nbsp;</td>
                                                                    <td class="auto-style18">&nbsp;</td>
                                                                    <td class="auto-style16">&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td class="auto-style19">&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="auto-style13">&nbsp;</td>
                                                                    <td class="auto-style18">&nbsp;</td>
                                                                    <td class="auto-style16">&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td class="auto-style19">&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="auto-style13">&nbsp;</td>
                                                                    <td class="auto-style18">
                                                                        <dx:ASPxLabel ID="lblMensaje" runat="server" Text="hghgh">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td class="auto-style16">&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td class="auto-style19">&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="auto-style13">&nbsp;</td>
                                                                    <td class="auto-style18">&nbsp;</td>
                                                                    <td class="auto-style16">&nbsp;</td>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                    <td class="auto-style19">
                                                                        <dx:ASPxButton ID="btnAceptar" runat="server" AutoPostBack="False" ClientInstanceName="btnAceptar" Text="Aceptar" ForeColor="White" BackColor="#307D40" Width="72px">
                                                                            <ClientSideEvents Click="function(s, e) {
                                                                                    dgvTransacciones.UpdateEdit()	
                                                                            }" />
                                                                        </dx:ASPxButton>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxButton ID="btnCancelar" runat="server" AutoPostBack="False" ClientInstanceName="btnCancelar" Text="Cerrar" ForeColor="White" BackColor="#307D40">
                                                                            <ClientSideEvents Click="function(s, e) {
                                                                                    dgvTransacciones.CancelEdit()	
                                                                            }" />
                                                                        </dx:ASPxButton>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                </tr>

                                                                <tr>
                                                                    <td class="auto-style13">&nbsp;</td>
                                                                    <td class="auto-style18">&nbsp;</td>
                                                                    <td class="auto-style16">&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td class="auto-style19">&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                </tr>

                                                            </table>
                                                        
                                                        </EditForm>
                                                    </Templates>
                                                    <SettingsPager PageSize="100">
                                                        <PageSizeItemSettings Items="10, 100, 1000" Visible="True" ShowAllItem="True">
                                                        </PageSizeItemSettings>
                                                    </SettingsPager>
                                                    <Settings ShowHeaderFilterButton="True" />
                                                    <SettingsBehavior AllowFocusedRow="True" />
                                                    <SettingsPopup>
                                                        <EditForm Modal="True" />
                                                    </SettingsPopup>
                                                    <SettingsText CommandNew="Nuevo" />
                                                    <Columns>
                                                        <dx:GridViewCommandColumn ShowInCustomizationForm="True" VisibleIndex="0" Visible="False">
                                                        </dx:GridViewCommandColumn>
                                                        <dx:GridViewDataTextColumn Caption="ID_EXPLOTADOR" FieldName="ID_EXPLOTADOR" ShowInCustomizationForm="True" VisibleIndex="2" ToolTip="Identificador del Movimiento" Visible="False">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="MODELO" FieldName="MODELO" ShowInCustomizationForm="True" VisibleIndex="4" ToolTip="Tipo de Movimiento">
                                                            <Settings AutoFilterCondition="Contains" />
                                                            <CellStyle HorizontalAlign="Center">
                                                            </CellStyle>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="COD.  AEROP. DESTINO" FieldName="ID_AEROPUERTO_DESTINO" ShowInCustomizationForm="True" VisibleIndex="6">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="COD.  AEROP. ALTERNO" FieldName="ID_AEROPUERTO_ALTERNO" ShowInCustomizationForm="True" VisibleIndex="7">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="TIEMPO DE VUELO" FieldName="TIEMPO_VUELO" ShowInCustomizationForm="True" VisibleIndex="8">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="TIEMPO HORAS BLOCK" FieldName="TIEMPO_HORAS_BLOCK" ShowInCustomizationForm="True" VisibleIndex="10" ToolTip="Número de Factura">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="CONSUMO HORA VUELO" FieldName="CONSUMO_HORA_VUELO" ShowInCustomizationForm="True" VisibleIndex="9" ToolTip="Número de Autorización">
                                                            <Settings AutoFilterCondition="Contains" />
                                                            <FilterTemplate>
                                                                <dx:ASPxComboBox ID="cmbFiltroProd" runat="server" ValueType="System.String">
                                                                </dx:ASPxComboBox>
                                                            </FilterTemplate>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="TANQUEO MAXIMO REQUERIDO" FieldName="TANQUEO_MAXIMO_REQUERIDO" ShowInCustomizationForm="True" VisibleIndex="11" ToolTip="Número de Referencia">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="NRO." FieldName="ID_VOLUMEN_REFERENCIAL" ShowInCustomizationForm="True" VisibleIndex="1">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="EXPLOTADOR" FieldName="EXPLOTADOR" ShowInCustomizationForm="True" VisibleIndex="3">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="COD.  AEROP. ORIGEN" FieldName="ID_AEROPUERTO_ORIGEN" ShowInCustomizationForm="True" VisibleIndex="5" ToolTip="Estado del Movimiento">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="TIEMPO SOBREVUELO MINIMO" FieldName="TIEMPO_SOBREVUELO_MINIMO" ShowInCustomizationForm="True" VisibleIndex="12" ToolTip="Número de Autorización de Venta">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                    <Styles CssPostfix="DevGlp"><AlternatingRow BackColor="#EEEEEE">
                                                        </AlternatingRow></Styles>
                                                </dx:ASPxGridView>
                                            </td>
                                        
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <asp:Label ID="lblTipoError" runat="server" Font-Bold="True" ForeColor="Maroon" Width="133%">Registros Observados</asp:Label>
                                            </td>

                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                                <dx:ASPxGridView ID="dgvTransaccionError" runat="server" AutoGenerateColumns="False" ClientInstanceName="dgvTransaccionError" EnableTheming="True" Font-Size="Smaller" OnHtmlRowPrepared="dgvTransaccionError_HtmlRowPrepared" Theme="Default" Width="100%" KeyFieldName="ID_VOLUMEN_REFERENCIAL">
                                                    <SettingsContextMenu EnableRowMenu="False">
                                                    </SettingsContextMenu>
                                                    <Templates>
                                                        <EditForm>
                                                            <table>
                                                                <tr>
                                                                    <td class="auto-style13">&nbsp;</td>
                                                                    <td class="auto-style18">
                                                                        <dx:ASPxLabel ID="ASPxLabel28" runat="server" Text="ID :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td class="auto-style16">
                                                                        <dx:ASPxTextBox ID="txtId0" runat="server" ClientInstanceName="txtId" ReadOnly="True" Width="170px">
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td class="auto-style19">&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="auto-style13">&nbsp;</td>
                                                                    <td class="auto-style18">
                                                                        <dx:ASPxLabel ID="ASPxLabel29" runat="server" Text="Fecha :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td class="auto-style16">
                                                                        <dx:ASPxDateEdit ID="detFecha0" runat="server" EditFormat="DateTime" Font-Size="Smaller">
                                                                            <TimeSectionProperties>
                                                                                <TimeEditProperties>
                                                                                    <ClearButton Visibility="Auto">
                                                                                    </ClearButton>
                                                                                </TimeEditProperties>
                                                                            </TimeSectionProperties>
                                                                            <ClearButton Visibility="Auto">
                                                                            </ClearButton>
                                                                            <ValidationSettings>
                                                                                <RequiredField IsRequired="True" />
                                                                            </ValidationSettings>
                                                                        </dx:ASPxDateEdit>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td class="auto-style19">
                                                                        <dx:ASPxLabel ID="ASPxLabel30" runat="server" Height="16px" Text="Válido Cred. Fiscal :" Width="110px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxComboBox ID="cmbTipoCred0" runat="server" Font-Size="Smaller" ValueType="System.Int32">
                                                                            <Items>
                                                                                <dx:ListEditItem Text="SI" Value="1" />
                                                                                <dx:ListEditItem Text="NO" Value="2" />
                                                                            </Items>
                                                                            <ClearButton Visibility="Auto">
                                                                            </ClearButton>
                                                                            <ValidationSettings>
                                                                                <RequiredField IsRequired="True" />
                                                                            </ValidationSettings>
                                                                        </dx:ASPxComboBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="auto-style13">&nbsp;</td>
                                                                    <td class="auto-style18">
                                                                        <dx:ASPxLabel ID="ASPxLabel31" runat="server" Text="Tipo Pago :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td class="auto-style16">
                                                                        <dx:ASPxComboBox ID="cmbTipoPago0" runat="server" Font-Size="Smaller" ValueType="System.Int32">
                                                                            <Items>
                                                                                <dx:ListEditItem Text="EFECTIVO" Value="1" />
                                                                                <dx:ListEditItem Text="PREPAGO" Value="2" />
                                                                                <dx:ListEditItem Text="VALE" Value="3" />
                                                                            </Items>
                                                                            <ClearButton Visibility="Auto">
                                                                            </ClearButton>
                                                                            <ValidationSettings>
                                                                                <RequiredField IsRequired="True" />
                                                                            </ValidationSettings>
                                                                        </dx:ASPxComboBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td class="auto-style19">
                                                                        <br />
                                                                        <dx:ASPxLabel ID="ASPxLabel32" runat="server" Text="Tipo Consumidor :" Width="110px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxComboBox ID="cmbTipoCons0" runat="server" Font-Size="Smaller" ValueType="System.Int32">
                                                                            <Items>
                                                                                <dx:ListEditItem Text="MOTORIZADO" Value="1" />
                                                                                <dx:ListEditItem Text="BIDON" Value="2" />
                                                                                <dx:ListEditItem Text="BOTELLA" Value="3" />
                                                                                <dx:ListEditItem Text="OTRO" Value="4" />
                                                                            </Items>
                                                                            <ClearButton Visibility="Auto">
                                                                            </ClearButton>
                                                                            <ValidationSettings>
                                                                                <RequiredField IsRequired="True" />
                                                                            </ValidationSettings>
                                                                        </dx:ASPxComboBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="auto-style20"></td>
                                                                    <td class="auto-style21">
                                                                        <dx:ASPxLabel ID="ASPxLabel33" runat="server" Text="Cliente :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td class="auto-style22">
                                                                        <dx:ASPxTextBox ID="txtCliente0" runat="server" Font-Size="Smaller" Height="16px" MaxLength="90" Width="170px">
                                                                            <ClientSideEvents TextChanged="function(s, e) {
	    s.SetText(s.GetText().toUpperCase());
    }" />
                                                                            <ValidationSettings>
                                                                                <RequiredField IsRequired="True" />
                                                                            </ValidationSettings>
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="auto-style13">&nbsp;</td>
                                                                    <td class="auto-style18">
                                                                        <dx:ASPxLabel ID="ASPxLabel34" runat="server" Text="NIT :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td class="auto-style16">
                                                                        <dx:ASPxTextBox ID="txtNit0" runat="server" Font-Size="Smaller" MaxLength="15" Width="170px">
                                                                            <MaskSettings Mask="&lt;0..999999999999999&gt;" PromptChar=" " />
                                                                            <ValidationSettings>
                                                                                <RequiredField IsRequired="True" />
                                                                            </ValidationSettings>
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td class="auto-style19">
                                                                        <dx:ASPxLabel ID="ASPxLabel35" runat="server" Text="Usuario :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtUsuario0" runat="server" ClientInstanceName="txtId" Font-Size="Smaller" MaxLength="100" Width="170px">
                                                                            <ValidationSettings>
                                                                                <RequiredField IsRequired="True" />
                                                                            </ValidationSettings>
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="auto-style13">&nbsp;</td>
                                                                    <td class="auto-style18">
                                                                        <dx:ASPxLabel ID="ASPxLabel36" runat="server" Text="Tipo de Venta :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td class="auto-style16">
                                                                        <dx:ASPxRadioButtonList ID="rbtTipoVenta0" runat="server" ClientInstanceName="rbtTipoVenta" SelectedIndex="0" ValueType="System.Int32">
                                                                            <ClientSideEvents ValueChanged="function(s, e) {
	    console.log('entraaaaaa....');
    alert(&quot;xxxxxxxxxxxx&quot;);
    txtNoAutoExt.SetVisible(false);

    }" />
                                                                            <Items>
                                                                                <dx:ListEditItem Selected="True" Text="Nacional" Value="1" />
                                                                                <dx:ListEditItem Text="Internacional" Value="2" />
                                                                            </Items>
                                                                        </dx:ASPxRadioButtonList>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td class="auto-style19">&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="auto-style13">&nbsp;</td>
                                                                    <td class="auto-style18">
                                                                        <dx:ASPxLabel ID="ASPxLabel37" runat="server" Text="País :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td class="auto-style16">
                                                                        <dx:ASPxComboBox ID="cmbPais0" runat="server">
                                                                            <Items>
                                                                                <dx:ListEditItem Text="BOLIVIA" Value="68" />
                                                                                <dx:ListEditItem Text="ARGENTINA" Value="32" />
                                                                                <dx:ListEditItem Text="BRASIL" Value="76" />
                                                                                <dx:ListEditItem Text="CHILE" Value="152" />
                                                                                <dx:ListEditItem Text="COLOMBIA" Value="170" />
                                                                                <dx:ListEditItem Text="ECUADOR" Value="218" />
                                                                                <dx:ListEditItem Text="PARAGUAY" Value="600" />
                                                                                <dx:ListEditItem Text="PERU" Value="604" />
                                                                                <dx:ListEditItem Text="URUGUAY" Value="858 " />
                                                                                <dx:ListEditItem Text="VENEZUELA" Value="862" />
                                                                            </Items>
                                                                            <ClearButton Visibility="Auto">
                                                                            </ClearButton>
                                                                            <ValidationSettings>
                                                                                <RequiredField IsRequired="True" />
                                                                            </ValidationSettings>
                                                                        </dx:ASPxComboBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td class="auto-style19">
                                                                        <dx:ASPxLabel ID="ASPxLabel38" runat="server" Text="Placa :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtPlaca0" runat="server" ClientInstanceName="txtId" Font-Size="Smaller" MaxLength="11" Width="170px">
                                                                            <ClientSideEvents TextChanged="function(s, e) {
	    s.SetText(s.GetText().toUpperCase());
    }" />
                                                                            <ValidationSettings>
                                                                                <RequiredField IsRequired="True" />
                                                                            </ValidationSettings>
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="auto-style13">&nbsp;</td>
                                                                    <td class="auto-style18">
                                                                        <dx:ASPxLabel ID="ASPxLabel39" runat="server" Text="No. Autorización :" Width="100px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td class="auto-style16">
                                                                        <dx:ASPxTextBox ID="txtNoAuto0" runat="server" Font-Size="Smaller" MaxLength="15" Width="170px">
                                                                            <MaskSettings Mask="&lt;0..999999999999999&gt;" />
                                                                            <ValidationSettings>
                                                                                <RequiredField IsRequired="True" />
                                                                            </ValidationSettings>
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td class="auto-style19">
                                                                        <dx:ASPxLabel ID="ASPxLabel40" runat="server" Text="No. Factura :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtNoFact0" runat="server" Font-Size="Smaller" MaxLength="15" Width="170px">
                                                                            <MaskSettings Mask="&lt;0..999999999999999&gt;" />
                                                                            <ValidationSettings>
                                                                                <RequiredField IsRequired="True" />
                                                                            </ValidationSettings>
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="auto-style13">&nbsp;</td>
                                                                    <td class="auto-style18">
                                                                        <dx:ASPxLabel ID="ASPxLabel41" runat="server" Text="Cod. Control :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td class="auto-style16">
                                                                        <dx:ASPxTextBox ID="txtCodCtrl0" runat="server" Font-Size="Smaller" MaxLength="14" Width="170px">
                                                                            <MaskSettings Mask="" />
                                                                            <ValidationSettings SetFocusOnError="True">
                                                                            </ValidationSettings>
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td class="auto-style19">
                                                                        <dx:ASPxLabel ID="ASPxLabel42" runat="server" Text="Importe :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtImporte0" runat="server" Width="170px">
                                                                            <MaskSettings IncludeLiterals="DecimalSymbol" Mask="&lt;0..9999999&gt;.&lt;00..99&gt;" />
                                                                            <ValidationSettings>
                                                                                <RequiredField IsRequired="True" />
                                                                            </ValidationSettings>
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="auto-style13">&nbsp;</td>
                                                                    <td class="auto-style18">&nbsp;</td>
                                                                    <td class="auto-style16">&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td class="auto-style19">&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="auto-style13">&nbsp;</td>
                                                                    <td runat="server" class="auto-style18">
                                                                        <dx:ASPxLabel ID="ASPxLabel43" runat="server" Text="No. Autorización Ext. :" Width="130px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td class="auto-style16">
                                                                        <dx:ASPxTextBox ID="txtNoAutoExt0" runat="server" ClientInstanceName="txtNoAutoExt" Font-Size="Smaller" MaxLength="15" Width="170px">
                                                                            <MaskSettings Mask="&lt;0..999999999999999&gt;" />
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td class="auto-style19">
                                                                        <dx:ASPxLabel ID="ASPxLabel44" runat="server" Text="No. Factura Ext. :" Width="120px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtNoFactExt0" runat="server" ClientInstanceName="txtNoFactExt" Font-Size="Smaller" MaxLength="15" Width="170px">
                                                                            <MaskSettings Mask="&lt;0..999999999999999&gt;" />
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="auto-style13">&nbsp;</td>
                                                                    <td class="auto-style18">
                                                                        <dx:ASPxLabel ID="ASPxLabel45" runat="server" Text="Cod. Control Ext. :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td class="auto-style16">
                                                                        <dx:ASPxTextBox ID="txtCodCtrlExt0" runat="server" Font-Size="Smaller" MaxLength="14" Width="170px">
                                                                            <MaskSettings Mask="" />
                                                                            <ValidationSettings SetFocusOnError="True">
                                                                            </ValidationSettings>
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td class="auto-style19">
                                                                        <dx:ASPxLabel ID="ASPxLabel46" runat="server" Text="Importe Ext. :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtImporteExt0" runat="server" Width="170px">
                                                                            <MaskSettings IncludeLiterals="DecimalSymbol" Mask="&lt;0..9999999&gt;.&lt;00..99&gt;" />
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="auto-style13">&nbsp;</td>
                                                                    <td class="auto-style18">&nbsp;</td>
                                                                    <td class="auto-style16">&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td class="auto-style19">&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="auto-style13">&nbsp;</td>
                                                                    <td class="auto-style18">
                                                                        <dx:ASPxLabel ID="ASPxLabel47" runat="server" Text="No. Doc. Referencia :" Width="130px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td class="auto-style16">
                                                                        <dx:ASPxTextBox ID="txtNoDocRef0" runat="server" Font-Size="Smaller" MaxLength="15" Width="170px">
                                                                            <MaskSettings Mask="&lt;0..999999999999999&gt;" />
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td class="auto-style19">
                                                                        <dx:ASPxLabel ID="ASPxLabel48" runat="server" Text="Tipo Factura :" Visible="False">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxComboBox ID="cmbTipoFact0" runat="server" Font-Size="Smaller" style="margin-top: 0" ValueType="System.Int32" Visible="False">
                                                                            <Items>
                                                                                <dx:ListEditItem Text="MANUAL" Value="1" />
                                                                                <dx:ListEditItem Text="COMPUTARIZADA" Value="2" />
                                                                            </Items>
                                                                            <ClearButton Visibility="Auto">
                                                                            </ClearButton>
                                                                        </dx:ASPxComboBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                    <td class="auto-style18">&nbsp;</td>
                                                                    <td class="auto-style16">&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td class="auto-style19">&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="auto-style13">&nbsp;</td>
                                                                    <td class="auto-style18">
                                                                        <dx:ASPxLabel ID="ASPxLabel49" runat="server" Text="Estado :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td class="auto-style16">
                                                                        <dx:ASPxComboBox ID="cmbEstado0" runat="server" Font-Size="Smaller" ValueType="System.Int32">
                                                                            <Items>
                                                                                <dx:ListEditItem Text="VALIDO" Value="1" />
                                                                                <dx:ListEditItem Text="ANULADO" Value="2" />
                                                                            </Items>
                                                                            <ClearButton Visibility="Auto">
                                                                            </ClearButton>
                                                                            <ValidationSettings>
                                                                                <RequiredField IsRequired="True" />
                                                                            </ValidationSettings>
                                                                        </dx:ASPxComboBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td class="auto-style19">&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="auto-style13">&nbsp;</td>
                                                                    <td class="auto-style18">&nbsp;</td>
                                                                    <td class="auto-style16">&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td class="auto-style19">&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="auto-style13">&nbsp;</td>
                                                                    <td class="auto-style18">
                                                                        <dx:ASPxLabel ID="ASPxLabel50" runat="server" Text="Producto :" Width="100px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td class="auto-style16">
                                                                        <dx:ASPxComboBox ID="cmbProd0" runat="server" Font-Size="Smaller" ValueType="System.Int32">
                                                                            <Items>
                                                                                <dx:ListEditItem Text="GNV" Value="1" />
                                                                                <dx:ListEditItem Text="DIESEL OIL" Value="2" />
                                                                                <dx:ListEditItem Text="GASOLINA ESPECIAL" Value="3" />
                                                                                <dx:ListEditItem Text="GASOLINA PREMIUM" Value="4" />
                                                                            </Items>
                                                                            <ClearButton Visibility="Auto">
                                                                            </ClearButton>
                                                                            <ValidationSettings>
                                                                                <RequiredField IsRequired="True" />
                                                                            </ValidationSettings>
                                                                        </dx:ASPxComboBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td class="auto-style19">&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="auto-style13">&nbsp;</td>
                                                                    <td class="auto-style18">
                                                                        <dx:ASPxLabel ID="ASPxLabel51" runat="server" Text="Cantidad :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td class="auto-style16">
                                                                        <dx:ASPxTextBox ID="txtCantidad0" runat="server" Width="170px">
                                                                            <MaskSettings IncludeLiterals="DecimalSymbol" Mask="&lt;0..9999999&gt;.&lt;00..99&gt;" />
                                                                            <ValidationSettings>
                                                                                <RequiredField IsRequired="True" />
                                                                            </ValidationSettings>
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td class="auto-style19">
                                                                        <dx:ASPxLabel ID="ASPxLabel52" runat="server" Text="Precio :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtPrecio0" runat="server" Width="170px">
                                                                            <MaskSettings IncludeLiterals="DecimalSymbol" Mask="&lt;0..999&gt;.&lt;00..99&gt;" PromptChar=" " />
                                                                            <ValidationSettings>
                                                                                <RequiredField IsRequired="True" />
                                                                            </ValidationSettings>
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="auto-style13">&nbsp;</td>
                                                                    <td class="auto-style18">&nbsp;</td>
                                                                    <td class="auto-style16">&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td class="auto-style19">&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="auto-style13">&nbsp;</td>
                                                                    <td class="auto-style18">
                                                                        <dx:ASPxLabel ID="ASPxLabel53" runat="server" Text="Autorización de Venta :" Width="150px">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td class="auto-style16">
                                                                        <dx:ASPxComboBox ID="cmbAutoVta0" runat="server">
                                                                            <Items>
                                                                                <dx:ListEditItem Text="SUSTANCIAS CONTROLADAS" Value="1" />
                                                                                <dx:ListEditItem Text="COMANDO CONJUNTO" Value="2" />
                                                                            </Items>
                                                                            <ClearButton Visibility="Auto">
                                                                            </ClearButton>
                                                                        </dx:ASPxComboBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                    <td class="auto-style19">
                                                                        <dx:ASPxLabel ID="ASPxLabel54" runat="server" Text="No. Hoja Ruta :">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxTextBox ID="txtNoAutoVta0" runat="server" Font-Size="Smaller" MaxLength="15" Width="170px">
                                                                            <MaskSettings Mask="&lt;0..999999999999999&gt;" />
                                                                        </dx:ASPxTextBox>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="auto-style13">&nbsp;</td>
                                                                    <td class="auto-style18">&nbsp;</td>
                                                                    <td class="auto-style16">&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td class="auto-style19">&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="auto-style13">&nbsp;</td>
                                                                    <td class="auto-style18">&nbsp;</td>
                                                                    <td class="auto-style16">&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td class="auto-style19">&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="auto-style13">&nbsp;</td>
                                                                    <td class="auto-style18">
                                                                        <dx:ASPxLabel ID="lblMensaje0" runat="server" Text="hghgh">
                                                                        </dx:ASPxLabel>
                                                                    </td>
                                                                    <td class="auto-style16">&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td class="auto-style19">&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="auto-style13">&nbsp;</td>
                                                                    <td class="auto-style18">&nbsp;</td>
                                                                    <td class="auto-style16">&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td class="auto-style19">
                                                                        <dx:ASPxButton ID="btnAceptar0" runat="server" AutoPostBack="False" ClientInstanceName="btnAceptar" Text="Aceptar" ForeColor="White" BackColor="#307D40" Width="72px">
                                                                            <ClientSideEvents Click="function(s, e) {
                                                                                    dgvTransacciones.UpdateEdit()	
                                                                            }" />
                                                                        </dx:ASPxButton>
                                                                    </td>
                                                                    <td>
                                                                        <dx:ASPxButton ID="btnCancelar0" runat="server" AutoPostBack="False" ClientInstanceName="btnCancelar" Text="Cerrar" ForeColor="White" BackColor="#307D40">
                                                                            <ClientSideEvents Click="function(s, e) {
                                                                                    dgvTransacciones.CancelEdit()	
                                                                            }" />
                                                                        </dx:ASPxButton>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="auto-style13">&nbsp;</td>
                                                                    <td class="auto-style18">&nbsp;</td>
                                                                    <td class="auto-style16">&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td class="auto-style19">&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                            </table>
                                                        </EditForm>
                                                    </Templates>
                                                    <SettingsPager NumericButtonCount="5" PageSize="50">
                                                        <PageSizeItemSettings Items="50, 100, 200, 500, 1000" Visible="True">
                                                        </PageSizeItemSettings>
                                                    </SettingsPager>
                                                    <SettingsEditing Mode="PopupEditForm">
                                                    </SettingsEditing>
                                                    <Settings ShowHeaderFilterButton="True" />
                                                    <SettingsBehavior AllowFocusedRow="True" />
                                                    <SettingsPopup>
                                                        <EditForm Modal="True" />
                                                    </SettingsPopup>
                                                    <SettingsText CommandNew="Nuevo" />
                                                    <Columns>
                                                        <dx:GridViewCommandColumn ShowInCustomizationForm="True" Visible="False" VisibleIndex="0">
                                                        </dx:GridViewCommandColumn>
                                                        <dx:GridViewDataTextColumn Caption="MNS_ERROR" FieldName="MNS_ERROR" ShowInCustomizationForm="True" VisibleIndex="10" ToolTip="Número de Referencia">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="NRO." FieldName="ID_VOLUMEN_REFERENCIAL" ShowInCustomizationForm="True" VisibleIndex="1" ToolTip="Funciones">
                                                            <Settings AutoFilterCondition="Equals" />
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                </dx:ASPxGridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dx:ASPxButton ID="btnExportar" runat="server" OnClick="btnExportar_Click" Text="Exportar Registros Observados" Theme="Metropolis" Font-Bold="True" ForeColor="Maroon">
                                                    <ClientSideEvents Click="function(s, e) {
	    e.processOnServer = confirm('¿Desea exportar los registros observados?'); 
    }" />
                                                    <Image IconID="export_exporttoxls_16x16">
                                                    </Image>
                                                </dx:ASPxButton>
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>
                                                &nbsp;</td>
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
                                                &nbsp;</td>
                                        </tr>
                                    </table>

                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxCallbackPanel>
            </dx:PopupControlContentControl>
        </ContentCollection>
        <ContentStyle>
            <Paddings PaddingBottom="5px" />
        </ContentStyle>
    </dx:ASPxPopupControl>


    
















