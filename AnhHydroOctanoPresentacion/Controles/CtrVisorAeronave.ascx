<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtrVisorAeronave.ascx.cs" Inherits="AnhHydroOctanoPresentacion.Controles.CtrVisorAeronave" %>
<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>
<%@ Register TagPrefix="dx" Namespace="DevExpress.Web.ASPxPivotGrid" Assembly="DevExpress.Web.ASPxPivotGrid.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>

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
                            <dx:ASPxLabel runat="server" ForeColor="#367B2F" ID="lblTitulo" Font-Bold="True" Font-Names="Rockwell" Font-Size="Larger" Text="VISOR DE AERONAVES REGISTRADAS"></dx:ASPxLabel>
                    </dx:PanelContent>
                </PanelCollection>
                <Border BorderStyle="Ridge" BorderWidth="1px" BorderColor="#003300"></Border>
                </dx:ASPxRoundPanel>
            </td>
        </tr>
    </table>

    <dx:ASPxSplitter ID="ASPxSplitter1" runat="server" Height="100%">
        <Panes>
            <dx:SplitterPane Size="15%" AllowResize="True" AutoHeight="True">
                <ContentCollection>
                    <dx:SplitterContentControl runat="server">
                        
                        <dx:ASPxCallbackPanel ID="pnlLateral" runat="server" Width="200px" ClientInstanceName="pnlLateral" OnCallback="pnlLateral_Callback">
                            <PanelCollection>
                                <dx:PanelContent runat="server">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td>
                                                <dx:ASPxLabel ID="ASPxLabel44" runat="server" Font-Bold="True" ForeColor="#003300" Text="TIPO DE AGRUPACION :" Font-Names="Verdana" Font-Size="Smaller">
                                                </dx:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dx:ASPxRadioButtonList ID="rblTipoAgrupacion" runat="server" SelectedIndex="0" ValueType="System.Int32" Width="100%">
                                                    <Items>
                                                        <dx:ListEditItem Selected="True" Text="Explotador" Value="1" />
                                                        <dx:ListEditItem Text="Propietario" Value="2" />
                                                    </Items>
                                                </dx:ASPxRadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dx:ASPxCheckBox ID="chkFiltro" runat="server" Checked="True" CheckState="Checked" Font-Bold="True" Font-Size="Smaller" ForeColor="#003300" Text="Ver Solo Activos" ValueChecked="1" ValueType="System.Int32" ValueUnchecked="2">
                                                </dx:ASPxCheckBox>
                                            </td>
                                        </tr>
                                        <tr style="padding-bottom: 5px">
                                            <td>
                                    
                                                <dx:ASPxButton ID="btnActualiza" runat="server" Text="Actualizar Listado" Theme="Metropolis" Width="100%" AutoPostBack="False" Font-Bold="True" ForeColor="Maroon">
                                                    <ClientSideEvents Click="function(s, e) {
	pnlLateral.PerformCallback();
pnlDatos.PerformCallback();
}" />
                                                    <Image IconID="actions_reset2_16x16">
                                                    </Image>
                                                </dx:ASPxButton>
                                    
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dx:ASPxGridView ID="dgvListado" runat="server" AutoGenerateColumns="False" ClientInstanceName="dgvCabecera" EnableTheming="True" Font-Size="Smaller" KeyFieldName="ID_EXPLOTADOR" Theme="Metropolis" Width="100%">
                                                    <ClientSideEvents FocusedRowChanged="function(s, e) {
	            pnlDatos.PerformCallback();
            }" />
                                                    <SettingsContextMenu EnableRowMenu="False">
                                                    </SettingsContextMenu>
                                                    <Templates>
                                                        <EditForm>
                                                        </EditForm>
                                                    </Templates>
                                                    <SettingsPager Mode="ShowAllRecords" NumericButtonCount="1" PageSize="0" Visible="False">
                                                        <PageSizeItemSettings Items="50, 100, 200, 500, 1000" Visible="True">
                                                        </PageSizeItemSettings>
                                                    </SettingsPager>
                                                    <Settings ShowFilterRow="True" ShowFooter="True" ShowHeaderFilterButton="True" />
                                                    <SettingsBehavior AllowFocusedRow="True" />
                                                    <SettingsPopup>
                                                        <EditForm Modal="True" />
                                                        <CustomizationWindow HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" />
                                                    </SettingsPopup>
                                                    <Columns>
                                                        <dx:GridViewCommandColumn ShowClearFilterButton="True" ShowInCustomizationForm="True" Visible="False" VisibleIndex="0">
                                                        </dx:GridViewCommandColumn>
                                                        <dx:GridViewDataTextColumn Caption="ID_EXPLOTADOR" FieldName="ID_EXPLOTADOR" ShowInCustomizationForm="True" Visible="False" VisibleIndex="1">
                                                            <Settings AllowAutoFilter="True" AllowHeaderFilter="True" AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="LISTADO DE EXPLOTADORES" FieldName="EXPLOTADOR" ShowInCustomizationForm="True" VisibleIndex="2">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="PAIS_EXPLOTADOR" FieldName="PAIS_EXPLOTADOR" ShowInCustomizationForm="True" Visible="False" VisibleIndex="3">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="CIUDAD_EXPLOTADOR" FieldName="CIUDAD_EXPLOTADOR" ShowInCustomizationForm="True" Visible="False" VisibleIndex="4">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="DOMICILIO_EXPLOTADOR" FieldName="DOMICILIO_EXPLOTADOR" ShowInCustomizationForm="True" Visible="False" VisibleIndex="5">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="ID_PROPIETARIO" FieldName="ID_PROPIETARIO" ShowInCustomizationForm="True" Visible="False" VisibleIndex="6">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="LISTADO DE PROPIETARIOS" FieldName="PROPIETARIO" ShowInCustomizationForm="True" Visible="False" VisibleIndex="7">
                                                            <Settings AutoFilterCondition="Contains" />
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="PAIS_PROPIETARIO" FieldName="PAIS_PROPIETARIO" ShowInCustomizationForm="True" Visible="False" VisibleIndex="8">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="CIUDAD_PROPIETARIO" FieldName="CIUDAD_PROPIETARIO" ShowInCustomizationForm="True" Visible="False" VisibleIndex="9">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="DOMICILIO_PROPIETARIO" FieldName="DOMICILIO_PROPIETARIO" ShowInCustomizationForm="True" Visible="False" VisibleIndex="10">
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                    <TotalSummary>
                                                        <dx:ASPxSummaryItem FieldName="DecRecepcion" ShowInColumn="RECEPCION" SummaryType="Custom" Tag="rec" />
                                                        <dx:ASPxSummaryItem FieldName="DecDemasia" ShowInColumn="DEMASIA" SummaryType="Custom" Tag="dem" />
                                                        <dx:ASPxSummaryItem FieldName="DecTotalIng" ShowInColumn="TOT. ING." SummaryType="Custom" Tag="ing" />
                                                        <dx:ASPxSummaryItem FieldName="DecVenta" ShowInColumn="VENTA" SummaryType="Custom" Tag="ven" />
                                                        <dx:ASPxSummaryItem FieldName="DecMerma" ShowInColumn="MERMA" SummaryType="Custom" Tag="mer" />
                                                        <dx:ASPxSummaryItem FieldName="DecTotalSal" ShowInColumn="TOT. SAL." SummaryType="Custom" Tag="sal" />
                                                    </TotalSummary>
                                                </dx:ASPxGridView>
                                            </td>
                                        </tr>
                                    </table>
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxCallbackPanel>

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
                                            <td colspan="7"><legend>
                                                <asp:Label ID="lblSubTitulo" runat="server" Font-Bold="True" Font-Names="Rockwell" Font-Size="Medium" ForeColor="#367B2F" Text="DATOS DEL EXPLOTADOR"></asp:Label>
                                                </legend></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="#10420F" Text="NOMBRE"></asp:Label>
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                            <td></td>
                                            <td>
                                                <asp:Label ID="Label3" runat="server" Font-Bold="True" ForeColor="#10420F" Text="PAIS"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label5" runat="server" Font-Bold="True" ForeColor="#10420F" Text="CIUDAD"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label4" runat="server" Font-Bold="True" ForeColor="#10420F" Text="DIRECCION"></asp:Label>
                                            </td>
                                            <td style="text-align: right;">
                                                <dx:ASPxButton ID="btnEmitir" runat="server" Font-Bold="True" ForeColor="Maroon" OnClick="btnEmitir_Click" Text="Exportar Aeronaves Registradas" Theme="Metropolis" Width="250px">
                                                    <Image IconID="export_exporttoxlsx_16x16">
                                                    </Image>
                                                </dx:ASPxButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <asp:Label ID="lblRazonSocial" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblPais" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCiudad" runat="server"></asp:Label>
                                            </td>
                                            <td colspan="2">
                                                <asp:Label ID="lblDireccion" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="7">
                                                <dx:ASPxGridView ID="dgvCabecera" runat="server" AutoGenerateColumns="False" ClientInstanceName="dgvCabecera" EnableTheming="True" Font-Size="Smaller" KeyFieldName="IDENTIFICADOR" OnHtmlRowPrepared="dgvCabecera_HtmlRowPrepared" Theme="Default" Width="100%">
                                                    <Styles>
                                                        <Header Wrap="True" HorizontalAlign="Center"></Header>
                                                    </Styles>
                                                    <ClientSideEvents CustomButtonClick="function(s, e) {
	
    switch(e.buttonID) {

    case&nbsp; 'btnHistorial':
//s.GetRowValues(e.visibleIndex, 'IDENTIFICADOR', function (value) {
//    pucHistorial.SetHeaderText('Tarjetas Registradas - Matricula:  '+value);
//   });
//    pucHistorial.Show();
//    pucHistorial.PerformCallback();
    alert('No se encontraron registros.');
    break;

    case&nbsp; 'btnFotos':
    imgFotos.PerformCallback();
    s.GetRowValues(e.visibleIndex, 'IDENTIFICADOR', function (value) {
    pucFoto.SetHeaderText('Fotografias - Matricula:  '+value);
    });
    pucFoto.Show();
    pucFoto.PerformCallback();
    break;

    case&nbsp; 'btnConsumo':
    s.GetRowValues(e.visibleIndex, 'IDENTIFICADOR', function (value) {
    pucConsumo.SetHeaderText('Consumo Registrado - Matricula:  '+value);
    });

    pucConsumo.PerformCallback();
    pucConsumo.Show();
   break;

   } 

}" />
                                                    <SettingsContextMenu EnableColumnMenu="True" Enabled="True">
                                                    </SettingsContextMenu>
                                                    <Templates>
                                                        <EditForm>
                                                        </EditForm>
                                                    </Templates>
                                                    <SettingsPager PageSize="100">
                                                        <PageSizeItemSettings Items="50, 100, 500, 1000" Visible="True" ShowAllItem="True">
                                                        </PageSizeItemSettings>
                                                    </SettingsPager>
                                                    <SettingsEditing Mode="EditForm">
                                                    </SettingsEditing>
                                                    <Settings ShowHeaderFilterButton="True" ShowFilterRow="True" ShowGroupPanel="True" ShowFooter="True" ShowGroupFooter="VisibleIfExpanded" />
                                                    <SettingsBehavior AllowFocusedRow="True" EnableCustomizationWindow="True" ColumnResizeMode="NextColumn" />
                                                    <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
                                                    <SettingsPopup>
                                                        <EditForm Modal="True" />
                                                        <CustomizationWindow HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" />
                                                    </SettingsPopup>
                                                    <SettingsSearchPanel Visible="True" />
                                                    <SettingsText CommandNew="Nuevo" />
                                                    <Columns>
                                                        <dx:GridViewCommandColumn ShowInCustomizationForm="True" VisibleIndex="40" ShowClearFilterButton="True" ToolTip="Funciones">
                                                            <CustomButtons>
                                                                <dx:GridViewCommandColumnCustomButton ID="btnHistorial" Text=" " Visibility="Invisible">
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
                                                        <dx:GridViewDataTextColumn Caption="TIPO_MTR" FieldName="TIPO_MTR" ShowInCustomizationForm="True" Visible="False" VisibleIndex="0">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="ID EXPLOT." FieldName="ID_EXPLOTADOR" ShowInCustomizationForm="True" Visible="False" VisibleIndex="1" ToolTip="Identificador del Explotador">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="EXPLOTADOR" FieldName="EXPLOTADOR" ShowInCustomizationForm="True" VisibleIndex="2">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="PAIS EXPLOT." FieldName="PAIS_EXPLOTADOR" ShowInCustomizationForm="True" Visible="False" VisibleIndex="3" ToolTip="Pais del Explotador">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="CIUDAD EXPLOT." FieldName="CIUDAD_EXPLOTADOR" ShowInCustomizationForm="True" Visible="False" VisibleIndex="4" ToolTip="Ciudade del Explotador">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="DOMICILIO EXPLOT." FieldName="DOMICILIO_EXPLOTADOR" ShowInCustomizationForm="True" Visible="False" VisibleIndex="5" ToolTip="Domicilio del Explotador">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="FECHA REG." FieldName="FECHA_REGISTRO" ShowInCustomizationForm="True" ToolTip="Fecha de Registro" VisibleIndex="26">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="LUGAR REG." FieldName="LUGAR_REGISTRO" ShowInCustomizationForm="True" ToolTip="Lugar de Registro" VisibleIndex="27">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="USUARIO REG." FieldName="USUARIO_NOMBRE" ShowInCustomizationForm="True" ToolTip="Usuario de Registro" VisibleIndex="28">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="IMEI" FieldName="IMEI" ShowInCustomizationForm="True" Visible="False" VisibleIndex="29">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="TIPO US. REG." FieldName="TIPO_USUARIO" ShowInCustomizationForm="True" Visible="False" VisibleIndex="30" ToolTip="Tipo de Usuario de Registro">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="MATRICULA" FieldName="IDENTIFICADOR" ShowInCustomizationForm="True" VisibleIndex="11">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="SUB IDENT." FieldName="SUB_IDENTIFICADOR" ShowInCustomizationForm="True" Visible="False" VisibleIndex="12" ToolTip="Sub Identificador">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="TARJETA" FieldName="BARCODE" ShowInCustomizationForm="True" VisibleIndex="13" ToolTip="Tarjeta Asignada">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="CLASE" FieldName="CLASE" ShowInCustomizationForm="True" VisibleIndex="14">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="MARCA" FieldName="MARCA" ShowInCustomizationForm="True" VisibleIndex="15">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="SERVICIO" FieldName="SERVICIO" ShowInCustomizationForm="True" VisibleIndex="16">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="MODELO" FieldName="MODELO" ShowInCustomizationForm="True" VisibleIndex="17">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="SERIE" FieldName="SERIE" ShowInCustomizationForm="True" VisibleIndex="18">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="AÑO FAB." FieldName="GESTION_FABRICACION" ShowInCustomizationForm="True" ToolTip="Año de Fabricación" VisibleIndex="20">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="CARGA" FieldName="CARGA" ShowInCustomizationForm="True" VisibleIndex="21" Visible="False">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="TIPO MATRICULA" FieldName="RA_TIPO_MATRICULA" ShowInCustomizationForm="True" ToolTip="Tipo de Matricula" VisibleIndex="22">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="BASE OPER." FieldName="BASE_OPERACIONES" ShowInCustomizationForm="True" ToolTip="Base de Operaciones" VisibleIndex="23">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="CHASIS" FieldName="CHASIS" ShowInCustomizationForm="True" Visible="False" VisibleIndex="24">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="EST. REP." FieldName="ESTADO_REPOSICION" ShowInCustomizationForm="True" Visible="False" VisibleIndex="31" ToolTip="Estado de Reposicion">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="EST. BLO." FieldName="ESTADO_BLOQUEO" ShowInCustomizationForm="True" Visible="False" VisibleIndex="32" ToolTip="Estado de Bloqueo">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="MENSAJE IMP." FieldName="MENSAJE_IMPRESION" ShowInCustomizationForm="True" Visible="False" VisibleIndex="33" ToolTip="Mensaje para Impresion">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="MENSAJE PANT." FieldName="MENSAJE_PANTALLA" ShowInCustomizationForm="True" Visible="False" VisibleIndex="34" ToolTip="Mensaje para Pantalla">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="ESTACION" FieldName="ESTACION" ShowInCustomizationForm="True" Visible="False" VisibleIndex="25">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="ID. PROPIETARIO" FieldName="ID_PROPIETARIO" ShowInCustomizationForm="True" Visible="False" VisibleIndex="6" ToolTip="Identificador del Propietario">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="PROPIETARIO" FieldName="PROPIETARIO" ShowInCustomizationForm="True" Visible="False" VisibleIndex="7">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="PAIS PROP." FieldName="PAIS_PROPIETARIO" ShowInCustomizationForm="True" Visible="False" VisibleIndex="8" ToolTip="Pais del Propietario">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="CIUDAD PROP." FieldName="CIUDAD_PROPIETARIO" ShowInCustomizationForm="True" Visible="False" VisibleIndex="9" ToolTip="Ciudad del Propietario">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="DOMICILIO PROP." FieldName="DOMICILIO_PROPIETARIO" ShowInCustomizationForm="True" Visible="False" VisibleIndex="10" ToolTip="Domicilio del Propietario">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="ESTADO" FieldName="ESTADO" ShowInCustomizationForm="True" VisibleIndex="37" Visible="False">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="BLOQUEO" FieldName="BLOQUEO" ShowInCustomizationForm="True" VisibleIndex="35">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="AUD_ESTADO" FieldName="AUD_ESTADO" ShowInCustomizationForm="True" Visible="False" VisibleIndex="38">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="DEUDA" FieldName="TOTAL_CXC" ShowInCustomizationForm="True" VisibleIndex="36" Visible="False">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="ID_PRODUCTO" FieldName="ID_PRODUCTO" ShowInCustomizationForm="True" Visible="False" VisibleIndex="39">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="PRODUCTO" FieldName="PRODUCTO" ShowInCustomizationForm="True" VisibleIndex="19">
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                    <Styles CssPostfix="DevGlp">
                                                        <AlternatingRow BackColor="#EEEEEE">
                                                        </AlternatingRow>
                                                    </Styles>
                                                    <TotalSummary>
                                                        <dx:ASPxSummaryItem DisplayFormat="{0}" FieldName="TOTAL_CXC" ShowInColumn="DEUDA" SummaryType="Sum" />
                                                    </TotalSummary>
                                                    <GroupSummary>
                                                        <dx:ASPxSummaryItem DisplayFormat="{0}" FieldName="TOTAL_CXC" ShowInGroupFooterColumn="DEUDA" SummaryType="Sum" />
                                                    </GroupSummary>
                                                </dx:ASPxGridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dx:ASPxGridViewExporter ID="dgeExportar" runat="server">
                                                </dx:ASPxGridViewExporter>
                                            </td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>
                                                &nbsp;</td>
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



<dx:ASPxPopupControl ID="pucFoto" runat="server" AllowDragging="True" ClientInstanceName="pucFoto" EnableTheming="True" HeaderText="Fotografias" Height="93px" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Theme="Metropolis" Width="850px" OnWindowCallback="pucFoto_WindowCallback">
    <HeaderImage IconID="toolboxitems_picturebox_32x32">
    </HeaderImage>
    <HeaderStyle>
    <BorderBottom BorderStyle="Solid" />
    </HeaderStyle>
    <ContentCollection>
        <dx:PopupControlContentControl runat="server">
            <table class="dxflInternalEditorTable_Youthful" style="width: 100%">
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2">
                        
                        <dx:ASPxImageGallery ID="imgFotos" runat="server"  EnableViewState="False" ImageContentBytesField="IMAGEN" ClientInstanceName="imgFotos" OnCustomCallback="imgFotos_CustomCallback" Theme="Metropolis">
                            <SettingsFolder ImageCacheFolder="~/Thumb/" />
                            <SettingsTableLayout RowsPerPage="1" />
                            
                            <ItemTextTemplate>
                            <div class="item">
                                <div class="campoValor"><%# Container.EvalDataItem("TIPO_IMAGEN") %></div>
                            </div>
                        </ItemTextTemplate>
                         <FullscreenViewerItemTextTemplate>
                            <div class="details">
                                <div class="campoNombre"><%# Container.EvalDataItem("TIPO_IMAGEN") %></div>
                            </div>
                        </FullscreenViewerItemTextTemplate>
                            

                        </dx:ASPxImageGallery>
                        
                    </td>

                </tr>
                <tr>
                    <td colspan="2">&nbsp;</td>
                </tr>
            </table>
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>



<dx:ASPxPopupControl ID="pucHistorial" runat="server" AllowDragging="True" ClientInstanceName="pucHistorial" EnableTheming="True" HeaderText="Historial de Tarjetas" Height="93px" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Above" Theme="Metropolis" Width="850px" OnWindowCallback="pucHistorial_WindowCallback" Maximized="True" ShowMaximizeButton="True">
    <HeaderImage IconID="mail_contact_32x32">
    </HeaderImage>
    <HeaderStyle>
    <BorderBottom BorderStyle="Solid" />
    </HeaderStyle>
    <ContentCollection>
        <dx:PopupControlContentControl runat="server">
            <table style="width: 100%">
                <tr>
                    <td style="width: 15%">
                        <dx:ASPxComboBox ID="cmbTipoExport" runat="server" ClientInstanceName="cmbTipoExport" Height="27px" SelectedIndex="0" Theme="Metropolis" ToolTip="Seleccione el Formato del Reporte" ValueType="System.Int32" Width="100%">
                            <Items>
                                <dx:ListEditItem Selected="True" Text="EXCEL" Value="1" />
                                <dx:ListEditItem Text="PDF - GENERAL" Value="2" />
                                <dx:ListEditItem Text="PDF - DETALLADO" Value="3" />
                            </Items>
                            <ClearButton Visibility="Auto">
                            </ClearButton>
                        </dx:ASPxComboBox>
                    </td>
                    <td style="width: 15%">
                        <dx:ASPxButton ID="btnReporte" runat="server" Font-Bold="True" ForeColor="Maroon" OnClick="btnReporte_Click" Text="Emitir Reporte" Theme="Metropolis" Width="100%">
                            <Image IconID="arrange_withtextwrapping_topleft_16x16">
                            </Image>
                        </dx:ASPxButton>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="3">
                        
                        <dx:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server"
                        KeyFieldName="BARCODE" Width="100%" OnDetailRowExpandedChanged="grid_DetailRowExpandedChanged" Theme="Default" EnableTheming="True" OnHtmlRowPrepared="grid_HtmlRowPrepared" >
                        <Templates>
                            <DetailRow>
                                <dx:ASPxGridView ID="detailGrid" runat="server"
                                    Width="100%" 
                                    AutoGenerateColumns="False" Theme="Office2010Black" EnableTheming="True" KeyFieldName="NRO_REF" OnHtmlRowPrepared="detailGrid_HtmlRowPrepared">
                                    <SettingsPager NumericButtonCount="1">
                                    </SettingsPager>
                                    <Settings ShowFooter="True" />
                                    <SettingsBehavior AllowFocusedRow="True" />
                                    <Columns>
                                        <dx:GridViewDataTextColumn Caption="BARCODE" FieldName="BARCODE" VisibleIndex="0" Visible="False">
                                            <Settings AllowSort="False" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="ID_TIPO" FieldName="ID_TIPO" VisibleIndex="1" Visible="False">
                                            <Settings AllowSort="False" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="TIPO" FieldName="TIPO" VisibleIndex="2">
                                            <Settings AllowSort="False" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="FECHA" FieldName="FECHA" VisibleIndex="3" Visible="False">
                                            <Settings AllowSort="False" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="FECHA" FieldName="FECHA_STR" VisibleIndex="4">
                                            <Settings AllowSort="False" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="USUARIO" FieldName="USUARIO" VisibleIndex="5">
                                            <Settings AllowSort="False" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="ESTADO" FieldName="ESTADO" VisibleIndex="6">
                                            <Settings AllowSort="False" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="TIPO_PAGO" FieldName="TIPO_PAGO" VisibleIndex="7">
                                            <Settings AllowSort="False" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="BANCO" FieldName="BANCO" VisibleIndex="8">
                                            <Settings AllowSort="False" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="NRO_CTA" FieldName="NRO_CTA" VisibleIndex="9">
                                            <Settings AllowSort="False" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="NRO_REF" FieldName="NRO_REF" VisibleIndex="10">
                                            <Settings AllowSort="False" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="CARGO" FieldName="CARGO" VisibleIndex="11">
                                            <Settings AllowSort="False" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="ABONO" FieldName="ABONO" VisibleIndex="12">
                                            <Settings AllowSort="False" />
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                    <TotalSummary>
                                        <dx:ASPxSummaryItem DisplayFormat="{0}" FieldName="CARGO" SummaryType="Sum" ShowInColumn="CARGO" />
                                        <dx:ASPxSummaryItem DisplayFormat="{0}" FieldName="ABONO" SummaryType="Sum" ShowInColumn="ABONO" />
                                    </TotalSummary> 
                                </dx:ASPxGridView>
                            </DetailRow>
                        </Templates>
                        <SettingsDetail ShowDetailRow="true" ExportMode="Expanded" />
                            <Settings ShowFooter="True" />
                            <SettingsBehavior AllowFocusedRow="True" />
                            <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
                            <Columns>
                                <dx:GridViewDataTextColumn Caption="BARCODE" FieldName="BARCODE" ShowInCustomizationForm="True" VisibleIndex="0">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="TIPO" FieldName="TIPO" ShowInCustomizationForm="True" VisibleIndex="1">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="FECHA_ENTREGA" FieldName="FECHA_ENTREGA" ShowInCustomizationForm="True" VisibleIndex="2">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="LUGAR_ENTREGA" FieldName="LUGAR_ENTREGA" ShowInCustomizationForm="True" VisibleIndex="3">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="USUARIO_ENTREGA" FieldName="USUARIO_ENTREGA" ShowInCustomizationForm="True" VisibleIndex="4">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="ESTADO" FieldName="ESTADO" ShowInCustomizationForm="True" VisibleIndex="5">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="CARGO" FieldName="CARGO" ShowInCustomizationForm="True" VisibleIndex="6">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="ABONO" FieldName="ABONO" ShowInCustomizationForm="True" VisibleIndex="7">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="SALDO" FieldName="SALDO" ShowInCustomizationForm="True" VisibleIndex="8">
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <TotalSummary>
                                <dx:ASPxSummaryItem DisplayFormat="{0}" FieldName="CARGO" ShowInColumn="CARGO" SummaryType="Sum" />
                                <dx:ASPxSummaryItem DisplayFormat="{0}" FieldName="ABONO" ShowInColumn="ABONO" SummaryType="Sum" />
                                <dx:ASPxSummaryItem DisplayFormat="{0}" FieldName="SALDO" ShowInColumn="SALDO" SummaryType="Sum" />
                            </TotalSummary>
                    </dx:ASPxGridView>

                    </td>

                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>



<dx:ASPxPopupControl ID="pucConsumo" runat="server" AllowDragging="True" ClientInstanceName="pucConsumo" EnableTheming="True" HeaderText="Historial de Consumo" Height="93px" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Above" Theme="Metropolis" Width="900px" OnWindowCallback="pucConsumo_WindowCallback" Maximized="True" ShowMaximizeButton="True">
    <HeaderImage IconID="functionlibrary_financial_32x32">
    </HeaderImage>
    <ClientSideEvents EndCallback="function(s, e) {
	pnlGrafico.PerformCallback();
}" />
    <HeaderStyle>
    <BorderBottom BorderStyle="Solid" />
    </HeaderStyle>
    <ContentCollection>
        <dx:PopupControlContentControl runat="server">
            
            <table style="width: 100%">
                <tr>
                    <td style="width: 10%">
                        <dx:ASPxLabel ID="ASPxLabel46" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="Smaller" ForeColor="#333333" Text="CARACTERISTICAS" Width="100%">
                            <BorderBottom BorderColor="#333333" />
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 10%"></td>
                    <td style="width: 55%">
                        &nbsp;</td>
                    <td style="width: 25%"></td>
                </tr>
                <tr style="padding-bottom: 5px">
                    <td>
                        Fecha Inicio :<dx:ASPxDateEdit ID="detFechaIni" runat="server" Theme="Metropolis" Width="100%">
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
                    <td>
                        Fecha Final :<dx:ASPxDateEdit ID="detFechaFin" runat="server" Theme="Metropolis" Width="100%">
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
                    <td></td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2">
                        Tipo :
                        <dx:ASPxRadioButtonList ID="rbtTipo" runat="server" EnableTheming="True" SelectedIndex="0" Theme="Metropolis" ValueType="System.Int32" Width="100%">
                            <Items>
                                <dx:ListEditItem Selected="True" Text="Total" Value="1" />
                                <dx:ListEditItem Text="Planta de Suministro" Value="2" />
                            </Items>
                        </dx:ASPxRadioButtonList>
                    </td>
                    
                    <td>
                        <dx:ASPxRadioButtonList ID="rbtAgrupacion" runat="server" ClientEnabled="False" ClientVisible="False" SelectedIndex="1" Theme="Metropolis" ValueType="System.Int32">
                            <Items>
                                <dx:ListEditItem Text="Mensual" Value="1" />
                                <dx:ListEditItem Selected="True" Text="Diario" Value="2" />
                            </Items>
                        </dx:ASPxRadioButtonList>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2">
                        Tipo de Gráfico :
                        <dx:ASPxComboBox ID="cmbTipoGrafico" runat="server" EnableTheming="True" SelectedIndex="0" Theme="Metropolis" Width="100%">
                            <Items>
                                <dx:ListEditItem Selected="True" Text="Linea" Value="line" />
                                <dx:ListEditItem Text="Barras" Value="column" />
                            </Items>
                            <ClearButton Visibility="Auto">
                            </ClearButton>
                        </dx:ASPxComboBox>
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="3">
                        <dx:ASPxLabel ID="ASPxLabel45" runat="server" Width="100%">
                            <BorderBottom BorderColor="#FF6600" BorderStyle="Solid" />
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxButton ID="btnGraficar" runat="server" AutoPostBack="False" EnableTheming="True" Font-Bold="True" ForeColor="Maroon" Text="Generar Gráfico" Theme="Metropolis" Width="100%">
                            <ClientSideEvents Click="function(s, e) {
pnlGrafico.PerformCallback();   
}" />
                            <Image IconID="chart_fullstackedline_16x16">
                            </Image>
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
            
                   


                        <dx:ASPxCallbackPanel ID="pnlGrafico" runat="server" Width="100%" ClientInstanceName="pnlGrafico" OnCallback="pnlGrafico_Callback">
                            <ClientSideEvents EndCallback="function(s, e) {
grafficardatos(s, e);
}" />
                            <Paddings PaddingTop="10px" />
                            <PanelCollection>
                                <dx:PanelContent runat="server">
                                    <table style="width: 100%">
                                        <tr>
                                            <td>
                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div id="container" style="min-width: 310px; height: 400px; margin: 0 auto" aria-expanded="true"></div>
                                                <script type="text/javascript">

                                                    function grafficardatos(s, e) {

                                                        var js_type = s.cp_type;
                                                        var js_title = s.cp_title;
                                                        var js_subtitle = s.cp_subtitle;
                                                        var js_ytitle = s.cp_ytitle;
                                                        var js_xAxis = s.cp_xAxis;
                                                        var js_Series = s.cp_Series;

                                                        Highcharts.chart('container', {
                                                            chart: {
                                                                type: js_type
                                                            },
                                                            title: {
                                                                text: js_title
                                                            },
                                                            subtitle: {
                                                                text: js_subtitle
                                                            },
                                                            xAxis: {
                                                                categories: js_xAxis,
                                                                crosshair: true
                                                            },
                                                            yAxis: {
                                                                min: 0,
                                                                title: {
                                                                    text: js_ytitle
                                                                }
                                                            },
                                                            tooltip: {
                                                                headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                                                                pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                                                                    '<td style="padding:0"><b>{point.y:.1f} </b></td></tr>',
                                                                footerFormat: '</table>',
                                                                shared: true,
                                                                useHTML: true
                                                            },
                                                            plotOptions: {
                                                                column: {
                                                                    pointPadding: 0.2,
                                                                    borderWidth: 0
                                                                },

                                                                series: {
                                                                    pointStart: 0,
                                                                    cursor: 'pointer',
                                                                    point: {
                                                                        events: {
                                                                            click: function (e) {
                                                                                pucDetalle.SetHeaderText('Detalle de Movimientos - ' + lblRazonSocial.GetText());
                                                                                pnlTransaccion.PerformCallback(this.category + "|" + this.series.name + "|2");
                                                                                pucDetalle.Show();
                                                                            }
                                                                        }
                                                                    },
                                                                }


                                                            },
                                                            series: js_Series
                                                        });


                                                        
                                
                                                        //Highcharts.chart('container', {
                                                        //    chart: {
                                                        //        type: js_type
                                                        //    },
                                                        //    title: {
                                                        //        text: js_title
                                                        //    },
                                                        //    subtitle: {
                                                        //        text: js_subtitle
                                                        //    },
                                                        //    xAxis: {
                                                        //        categories: js_xAxis
                                                        //    },
                                                        //    yAxis: {
                                                        //        title: {
                                                        //            text: js_ytitle
                                                        //        }
                                                        //    },
                                                        //    plotOptions: {
                                        
                                                        //        series: {
                                                        //            pointStart: 0//,
                                                        //            //cursor: 'pointer',
                                                        //            //point: {
                                                        //            //    events: {
                                                        //            //        click: function (e) {
                                                                                

                                                        //            //            //alert('aa');

                                                        //            //            //alert(e.clientX);
                                                        //            //            //alert(this.y);

                                                        //            //            //ojo sesionar objResultado y recuperar los datos con estos
                                                        //            //            //alert(this.series.name);
                                                        //            //            //alert(this.x);

                                                        //            //            //pucDatos.Show();
                                                        //            //            //pucDatos.PerformCallback(this.series.name);

                                                        //            //            //alert(this.xAxis.category.name);
                                                        //            //            //hs.htmlExpand(null, {
                                                        //            //            //    pageOrigin: {
                                                        //            //            //        x: e.pageX || e.clientX,
                                                        //            //            //        y: e.pageY || e.clientY
                                                        //            //            //    },
                                                        //            //            //    headingText: this.series.name,
                                                        //            //            //    maincontentText: Highcharts.dateFormat('%A, %b %e, %Y', this.x) + ':<br/> ' +
                                                        //            //            //        this.y + ' visits',
                                                        //            //            //    width: 200
                                                        //            //            //});
                                                        //            //        }
                                                        //            //    }
                                                        //            //},
                                                        //        }
                                                        //    },
                                                        //    series: js_Series
                                                        //});
                                                    }




                                                </script>
                                            </td>
                                        </tr>
                                    </table>
                                    

                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxCallbackPanel>


        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>




























































    <dx:ASPxPopupControl ID="pucDatos" runat="server" CloseAction="CloseButton" CloseOnEscape="True" Modal="True"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Above" ClientInstanceName="pucDatos"
        HeaderText="Detalle de Proyectos" AllowDragging="True" PopupAnimationType="None" EnableViewState="False" AllowResize="True" EnableTheming="True" Maximized="True" ScrollBars="Auto" ShowMaximizeButton="True" Theme="Metropolis" OnWindowCallback="pucDatos_WindowCallback">
        <HeaderStyle>
        <BorderBottom BorderStyle="Solid" />
        </HeaderStyle>
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <dx:ASPxCallbackPanel ID="pnlReporte" runat="server" DefaultButton="btOK" ClientInstanceName="pnlReporte" >
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <table class="dxflInternalEditorTable_Youthful">
                                <tr style="width: 100%">
                                    <td style="width: 15%">
                                        <dx:ASPxComboBox ID="cmbTipoExport0" runat="server" ClientInstanceName="cmbTipoExport" SelectedIndex="0" Theme="Metropolis" ValueType="System.Int32" Width="100%" Height="27px" ToolTip="Seleccione el Formato del Reporte">
                                            <Items>
                                                <dx:ListEditItem Selected="True" Text="EXCEL" Value="1" />
                                                <dx:ListEditItem Text="PDF" Value="2" />
                                            </Items>
                                            <ClearButton Visibility="Auto">
                                            </ClearButton>
                                        </dx:ASPxComboBox>
                                    </td>
                                    <td style="width: 15%">
                                        <dx:ASPxButton ID="btnEmitir0" runat="server" Font-Bold="True" ForeColor="Maroon" OnClick="btnEmitir_Click" Text="Emitir Reporte" Theme="Metropolis" Width="100%">
                                            <Image IconID="arrange_withtextwrapping_topleft_16x16">
                                            </Image>
                                        </dx:ASPxButton>
                                    </td>
                                    <td style="width: 70%"></td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <dx:ASPxGridView ID="dgvReporte" runat="server" AutoGenerateColumns="False" ClientInstanceName="dgvReporte" EnableTheming="True" Font-Size="Smaller" KeyFieldName="ID_CONSUMIDOR" Theme="Default" Width="100%">
                                            <ClientSideEvents CustomButtonClick="function(s, e) {

                switch(e.buttonID) {

                case&nbsp; 'btnHistorial':
                pucHistorial.Show();
                pucHistorial.PerformCallback();
                break;

                case&nbsp; 'btnResumen':

                s.GetRowValues(e.visibleIndex, 'CITE_GENERADO', function (value) {
                if(value!=null){
                lblTituloDetalle.SetValue('RESUME DE LA DECLARACION JURADA : '+value);
                pnlResumen.SetVisible(true);
                }
                else{
                lblTituloDetalle.SetValue('RESUME DE LA DECLARACION JURADA : ');
                pnlResumen.SetVisible(false);
                }

                });

                dgvAcumulado.PerformCallback();
                dgvInternacional.PerformCallback();
                dgvContenedor.PerformCallback();

                break;

                case&nbsp; 'btnAmpliar':
                pucAmpliacion.PerformCallback();
                pucAmpliacion.Show();

                break;

                case&nbsp; 'btnIntimar':
                pucIntimacion.Show();
                pucIntimacion.PerformCallback();
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
                                            <Settings ShowFilterRow="True" ShowFooter="True" ShowGroupPanel="True" ShowHeaderFilterButton="True" />
                                            <SettingsBehavior AllowFocusedRow="True" />
                                            <SettingsPopup>
                                                <EditForm Modal="True" />
                                                <CustomizationWindow HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" />
                                            </SettingsPopup>
                                            <SettingsText CommandNew="Nuevo" />
                                            <Columns>
                                                <dx:GridViewCommandColumn ShowClearFilterButton="True" ShowInCustomizationForm="True" VisibleIndex="11">
                                                    <CustomButtons>
                                                        <dx:GridViewCommandColumnCustomButton ID="btnResumen" Text=" ">
                                                            <Image IconID="actions_productcomparisons_16x16devav" ToolTip="Ver Resumen">
                                                            </Image>
                                                        </dx:GridViewCommandColumnCustomButton>
                                                        <dx:GridViewCommandColumnCustomButton ID="btnHistorial0" Text=" ">
                                                            <Image IconID="functionlibrary_lookupreference_16x16" ToolTip="Ver Historial">
                                                            </Image>
                                                        </dx:GridViewCommandColumnCustomButton>
                                                        <dx:GridViewCommandColumnCustomButton ID="btnAmpliar" Text=" ">
                                                            <Image IconID="scheduling_switchtimescalesto_16x16" ToolTip="Ampliación">
                                                            </Image>
                                                        </dx:GridViewCommandColumnCustomButton>
                                                        <dx:GridViewCommandColumnCustomButton ID="btnIntimar" Text=" ">
                                                            <Image IconID="functionlibrary_datetime_16x16" ToolTip="Intimación">
                                                            </Image>
                                                        </dx:GridViewCommandColumnCustomButton>
                                                    </CustomButtons>
                                                </dx:GridViewCommandColumn>
                                                <dx:GridViewDataTextColumn Caption="CITE DEC. JURADA" FieldName="CITE_GENERADO" ShowInCustomizationForm="True" ToolTip="Cite de la Declaración Jurada" VisibleIndex="8">
                                                    <Settings AutoFilterCondition="Contains" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="ID_TIPO_ACTIVIDAD" FieldName="ID_TIPO_ACTIVIDAD" ShowInCustomizationForm="True" ToolTip="Tipo de Consumidor" Visible="False" VisibleIndex="13">
                                                    <Settings AutoFilterCondition="Contains" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="DEPTO." FieldName="DEPARTAMENTO" ShowInCustomizationForm="True" ToolTip="Departamento" VisibleIndex="1">
                                                    <Settings AutoFilterCondition="Contains" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="ESTADO" FieldName="MENSAJE_MENSUAL" GroupIndex="0" ShowInCustomizationForm="True" SortIndex="0" SortOrder="Ascending" VisibleIndex="10">
                                                    <Settings AutoFilterCondition="Contains" />
                                                    <CellStyle Font-Bold="True">
                                                    </CellStyle>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="RAZON SOCIAL" FieldName="DENOMINACION" ShowInCustomizationForm="True" VisibleIndex="2">
                                                    <Settings AutoFilterCondition="Contains" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="FECHA EMISION" FieldName="strFechaReg" ShowInCustomizationForm="True" VisibleIndex="7">
                                                    <Settings AutoFilterCondition="Equals" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="ID_CONSUMIDOR" FieldName="ID_CONSUMIDOR" ShowInCustomizationForm="True" Visible="False" VisibleIndex="0">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="ESTADO_MENSUAL" FieldName="ESTADO_MENSUAL" ShowInCustomizationForm="True" Visible="False" VisibleIndex="9">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="JSON_REPORTE_DATOS" FieldName="JSON_REPORTE_DATOS" ShowInCustomizationForm="True" Visible="False" VisibleIndex="12">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="FECHA_REPORTE" FieldName="FECHA_REPORTE" ShowInCustomizationForm="True" Visible="False" VisibleIndex="6">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="NIT" FieldName="NIT" ShowInCustomizationForm="True" VisibleIndex="3">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="DIRECCION" FieldName="DIRECCION" ShowInCustomizationForm="True" VisibleIndex="4">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="REP. LEGAL" FieldName="NOMBRES_REPRESENTANTE" ShowInCustomizationForm="True" ToolTip="Representante Legal" VisibleIndex="5">
                                                </dx:GridViewDataTextColumn>
                                            </Columns>
                                            <TotalSummary>
                                                <dx:ASPxSummaryItem FieldName="DecRecepcion" ShowInColumn="RECEPCION" SummaryType="Custom" Tag="rec" />
                                                <dx:ASPxSummaryItem FieldName="DecDemasia" ShowInColumn="DEMASIA" SummaryType="Custom" Tag="dem" />
                                                <dx:ASPxSummaryItem FieldName="DecTotalIng" ShowInColumn="TOT. ING." SummaryType="Custom" Tag="ing" />
                                                <dx:ASPxSummaryItem FieldName="DecVenta" ShowInColumn="VENTA" SummaryType="Custom" Tag="ven" />
                                                <dx:ASPxSummaryItem FieldName="DecMerma" ShowInColumn="MERMA" SummaryType="Custom" Tag="mer" />
                                                <dx:ASPxSummaryItem FieldName="DecTotalSal" ShowInColumn="TOT. SAL." SummaryType="Custom" Tag="sal" />
                                            </TotalSummary>
                                        </dx:ASPxGridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                        
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <dx:ASPxPanel ID="pnlResumen" runat="server" Width="100%" ClientInstanceName="pnlResumen">
                                            <ClientSideEvents Init="function(s, e) {
	                s.SetVisible(false);
                }" />
                                            <PanelCollection>
                                                <dx:PanelContent runat="server">
                                                    <table style="width: 100%">
                                                        <tr>
                                                            <td>
                                                
                                                                &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                &nbsp;</td>
                                                        </tr>
                                                    </table>
                                                </dx:PanelContent>
                                            </PanelCollection>
                                        </dx:ASPxPanel>
                                    </td>
                                </tr>
                            </table>

                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxCallbackPanel>

            </dx:PopupControlContentControl>
        </ContentCollection>
        <HeaderImage IconID="grid_horizontalgridlinesmajorminor_32x32">
        </HeaderImage>
        <ContentStyle>
            <Paddings PaddingBottom="5px" />
        </ContentStyle>
    </dx:ASPxPopupControl>


    




























































