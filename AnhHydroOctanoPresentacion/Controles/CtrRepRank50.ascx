<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtrRepRank50.ascx.cs" Inherits="AnhHydroOctanoPresentacion.Controles.CtrRepRank50" %>

<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPivotGrid" TagPrefix="dx" %>

<style type="text/css">
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

    .auto-style14 {
        width: 202px;
    }
</style>

<script type="text/javascript">
    function onCellClick(rowIndex, fieldName, cell) {
        if (fieldName != 'MDIN') {
            cell.style.backgroundColor = "orange";
        }
        for (var i = 0; i <= 100; i++) { }
        if (fieldName == 'DecSaldoInicial' || fieldName == 'MDIN' || !isNaN(fieldName.substr(6, 2))) {
            pucDetalle.SetHeaderText('Detalle de Movimientos - ' + lblRazonSocial.GetText());
            pnlTransaccion.PerformCallback(rowIndex + "|" + fieldName + "|1");
            pucDetalle.Show();
        }
    }

</script>

<div class="col-md-12">

    <table style="width: 100%; height: 100%">
        <tr>
            <td style="text-align: center; padding-bottom: 1px">
                <dx:ASPxRoundPanel runat="server" ShowHeader="False" Width="100%" ID="pnlTitulo">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <%--<dx:ASPxLabel runat="server" ForeColor="#367B2F" ID="lblTitulo" Font-Bold="True" Font-Names="Rockwell" Font-Size="Larger" Text="REPORTE - CONSUMO"></dx:ASPxLabel>--%>
                            <dx:ASPxLabel runat="server" ForeColor="#367B2F" ID="lblTitulo" Font-Bold="True" Font-Names="Rockwell" Font-Size="Larger" Text="REPORTE - CARGIOS IRREGULARES"></dx:ASPxLabel>
                        </dx:PanelContent>
                    </PanelCollection>
                    <Border BorderStyle="Ridge" BorderWidth="1px" BorderColor="#003300"></Border>
                </dx:ASPxRoundPanel>
            </td>
        </tr>

        <tr>
            <td style="height: 100%">


                <dx:ASPxCallbackPanel ID="pnlOpciones" runat="server" Theme="Youthful" Width="100%" ClientInstanceName="pnlOpciones" OnCallback="pnlOpciones_Callback">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <table style="width: 100%">
                                <tr>
                                    <td colspan="4">
                                        <legend>
                                            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Rockwell" Font-Size="Medium" ForeColor="#367B2F" Text="CARACTERISTICAS DEL REPORTE"></asp:Label>
                                        </legend>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <dx:ASPxLabel ID="ASPxLabel57" runat="server" ForeColor="#003300" Text="Fecha Inicial :">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td style="padding-left: 15px">
                                        <dx:ASPxLabel ID="ASPxLabel58" runat="server" ForeColor="#003300" Text="Fecha Final :">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td style="padding-left: 20px">
                                        &nbsp;
                                        <%--<dx:ASPxLabel ID="ASPxLabel59" runat="server" ForeColor="#003300" Text="Actividad :">
                                        </dx:ASPxLabel>--%>
                                    </td>
                                    <td style="padding-left: 20px">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 20%">
                                        <dx:ASPxDateEdit ID="detFechaIni" runat="server" ClientInstanceName="detFechaIni" EnableTheming="True" Height="23px" Theme="Metropolis" ToolTip="Seleccione el periodo inicial" Width="100%" Font-Bold="True">
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
                                    <td style="width: 20%; padding-left: 15px;">
                                        <dx:ASPxDateEdit ID="detFechaFin" runat="server" ClientInstanceName="detFechaFin" Theme="Metropolis" ToolTip="Seleccione el periodo final" Width="100%" Font-Bold="True">
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
                                    <td style="padding-left: 15px">                                      
                                        <%--  <dx:ASPxComboBox ID="cmbActividad" runat="server" SelectedIndex="0" Theme="Metropolis" Width="55%" ValueType="System.Int32">
                                            <Items>
                                                <dx:ListEditItem Selected="True" Text="ESTACION SERVICIO (LIQ, GNV)" Value="1" />
                                                <dx:ListEditItem Text="PLANTAS DE SUMINISTRO (PSCAA)" Value="2" />
                                            </Items>
                                            <ClearButton Visibility="Auto">
                                            </ClearButton>
                                        </dx:ASPxComboBox>--%>
                                        &nbsp;
                                    </td>
                                    <td style="width: 20%; padding-left: 20px">
                                     &nbsp;                                     
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <%--<dx:ASPxLabel ID="ASPxLabel61" runat="server" ForeColor="#003300" Text="Producto :">
                                        </dx:ASPxLabel>--%>
                                        &nbsp;
                                    </td>
                                    <td style="padding-left: 15px">
                                        <%--<dx:ASPxLabel ID="ASPxLabel1" runat="server" ForeColor="#003300" Text="Identificador :">
                                        </dx:ASPxLabel>--%>
                                        &nbsp;
                                    </td>
                                    <td style="padding-left: 15px">
                                        <%--<dx:ASPxLabel ID="ASPxLabel2" runat="server" ForeColor="#003300" Text="Tipo Documento :">
                                        </dx:ASPxLabel>--%>
                                        &nbsp;
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td style="width: 20%;">
                                      <%--  <dx:ASPxTokenBox ID="tkblistaProducto" ClientInstanceName="tkblistaProducto" runat="server" Width="100%" TextField="CODIGO" ValueField="ID_PRODUCTO">
                                        </dx:ASPxTokenBox>--%>
                                        &nbsp;
                                    </td>
                                    <td style="padding-left: 15px;">
                                       <%--  <dx:ASPxTokenBox ID="tkblstIdentificador" ClientInstanceName="tkblstIdentificador" runat="server" Width="100%">
                                        </dx:ASPxTokenBox>  --%>     
                                        &nbsp;                                 
                                    </td>
                                    <td style="padding-left: 15px;">
                                          <%--<dx:ASPxSpinEdit ID="spnPeriodo" runat="server" MaxValue="100" MinValue="50" Number="50" Theme="Metropolis" Width="60px">
                                            <ClearButton Visibility="Auto">
                                            </ClearButton>
                                        </dx:ASPxSpinEdit>--%>
                                        &nbsp;
                                    </td>

                                    <td>
                                        <dx:ASPxButton ID="btnConsultar" runat="server" AutoPostBack="False" Font-Bold="True" ForeColor="Maroon" Text="Obtener Registros" Theme="Metropolis" Width="100%">
                                            <ClientSideEvents Click="function(s, e) {
pnlDatos.PerformCallback();
}" />
                                            <Image IconID="actions_download_16x16">
                                            </Image>
                                        </dx:ASPxButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">

                                        <dx:ASPxLabel ID="ASPxLabel63" runat="server" ForeColor="#003300" Height="0px" Width="100%">
                                            <BorderBottom BorderStyle="Solid" BorderWidth="2px" />
                                        </dx:ASPxLabel>

                                    </td>
                                </tr>
                            </table>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxCallbackPanel>

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
                                    <td colspan="7" style="text-align: center; padding-top: 10px">
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
                                    <td colspan="7"></td>
                                </tr>
                                <tr>
                                    <td style="width: 15%; padding-top: 10px">
                                        <dx:ASPxComboBox ID="cmbTipoExport" runat="server" ClientInstanceName="cmbTipoExport" Height="27px" SelectedIndex="0" Theme="Metropolis" ToolTip="Seleccione el Formato del Reporte" ValueType="System.Int32" Width="100%" Enabled="false">
                                            <Items>
                                                <%--<dx:ListEditItem Text="EXCEL" Value="1" />--%>
                                                <dx:ListEditItem Selected="True" Text="PDF" Value="2" />
                                            </Items>
                                            <ClearButton Visibility="Auto">
                                            </ClearButton>
                                        </dx:ASPxComboBox>
                                    </td>
                                    <td style="width: 15%; padding-top: 10px">
                                        <dx:ASPxButton ID="btnExportar" runat="server" Font-Bold="True" ForeColor="Maroon" OnClick="btnExportar_Click" Text="Exportar Listado" Theme="Metropolis" Width="100%">
                                            <Image IconID="actions_converttorange_16x16">
                                            </Image>
                                        </dx:ASPxButton>
                                    </td>
                                    <td style="width: 70%" colspan="5"></td>
                                </tr>
                                <tr>
                                    <td colspan="7">
                                        <dx:ASPxGridView ID="dgvRepRank" runat="server" AutoGenerateColumns="False" ClientInstanceName="dgvRepRank" EnableTheming="True" Font-Size="Smaller" KeyFieldName="ID" Theme="Default" Width="100%">
                                            <SettingsContextMenu EnableRowMenu="False">
                                            </SettingsContextMenu>
                                            <Templates>
                                                <EditForm>
                                                </EditForm>
                                            </Templates>
                                            <SettingsPager PageSize="100">
                                                <PageSizeItemSettings Items="10, 50, 100, 200, 500, 1000" Visible="True">
                                                </PageSizeItemSettings>
                                            </SettingsPager>
                                            <SettingsEditing Mode="PopupEditForm">
                                            </SettingsEditing>
                                            <Settings ShowFooter="True" ShowHeaderFilterButton="True" ShowFilterRow="True" />
                                            <SettingsBehavior AllowFocusedRow="True" />
                                            <SettingsPopup>
                                                <EditForm Modal="True" />
                                                <CustomizationWindow HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" />
                                            </SettingsPopup>
                                            <SettingsText CommandNew="Nuevo" />
                                            <Columns>
                                                <dx:GridViewCommandColumn ShowInCustomizationForm="True" Visible="False" VisibleIndex="21" ShowClearFilterButton="True">
                                                </dx:GridViewCommandColumn>
                                                <dx:GridViewDataTextColumn Caption="IDENTIFICADOR" FieldName="IDENTIFICADOR" ShowInCustomizationForm="True" VisibleIndex="6">
                                                    <Settings AutoFilterCondition="Contains" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="DEPARTAMENTO" FieldName="DEPARTAMENTO" ShowInCustomizationForm="True" VisibleIndex="3">
                                                    <Settings AutoFilterCondition="Contains" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="VOLUMEN PROMEDIO" FieldName="PROM_VOL" ShowInCustomizationForm="True" VisibleIndex="14">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="VOLUMEN" FieldName="VOLUMEN" ShowInCustomizationForm="True" VisibleIndex="8" ToolTip="Número de Factura">
                                                    <Settings AutoFilterCondition="Contains" />
                                                    <CellStyle HorizontalAlign="Right">
                                                    </CellStyle>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="PRODUCTO" FieldName="PRODUCTO" ShowInCustomizationForm="True" ToolTip="Producto" VisibleIndex="7" ReadOnly="True">
                                                    <Settings AutoFilterCondition="Equals" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="GESTION" FieldName="GESTION" ShowInCustomizationForm="True" VisibleIndex="1">
                                                    <Settings AutoFilterCondition="Contains" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="VOLUMEN MAX " FieldName="MAX_VOL" ShowInCustomizationForm="True" VisibleIndex="12">
                                                    <Settings AutoFilterCondition="Contains" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="VOLUMEN MIN" FieldName="MIN_VOL" ShowInCustomizationForm="True" VisibleIndex="13">
                                                    <Settings AutoFilterCondition="Equals" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="ID" FieldName="ID" ShowInCustomizationForm="True" VisibleIndex="0">
                                                    <Settings AutoFilterCondition="Equals" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="CLASE" FieldName="CLASE" ShowInCustomizationForm="True" VisibleIndex="5">
                                                    <Settings AutoFilterCondition="Contains" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="MES" FieldName="MES" ShowInCustomizationForm="True" VisibleIndex="2">
                                                    <Settings AutoFilterCondition="Contains" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="RANK X CONSUMO" FieldName="CONT" ShowInCustomizationForm="True" VisibleIndex="15">
                                                    <Settings AutoFilterCondition="Equals" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="DENOMINACION" FieldName="DENOMINACION" ShowInCustomizationForm="True" VisibleIndex="4">
                                                </dx:GridViewDataTextColumn>
                                            </Columns>
                                            <TotalSummary>
                                                <dx:ASPxSummaryItem FieldName="VOLUMEN" ShowInColumn="VOLUMEN" SummaryType="Sum" Tag="imp" />
                                            </TotalSummary>
                                        </dx:ASPxGridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style11">                                        
                                        <dx:ASPxGridViewExporter ID="dgeExportar" runat="server">
                                        </dx:ASPxGridViewExporter>
                                    </td>
                                    <td class="auto-style10">&nbsp;</td>
                                    <td class="auto-style10"></td>
                                    <td class="auto-style12"></td>
                                    <td class="auto-style10">&nbsp;</td>
                                    <td class="auto-style10">&nbsp;</td>
                                    <td class="auto-style14">&nbsp;</td>
                                </tr>



                            </table>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxCallbackPanel>

            </td>
        </tr>
    </table>

</div>
