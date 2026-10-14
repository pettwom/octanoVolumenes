<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtrProde.ascx.cs" Inherits="AnhHydroOctanoPresentacion.Controles.CtrProde" %>

<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxPivotGrid.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPivotGrid" tagprefix="dx" %>

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
    
</style>

<script type="text/javascript">
    function onClickRegistrarTechoProde(opcion) {
        var mensaje = confirm("¿Desea registrar el volumen techo de PRODE?");

        var numTecho = parseFloat(txtTecho.GetText(), 10);
        var numReserva = parseFloat(txtReserva.GetText(), 10);
        var numAsignado = parseFloat(txtAsignado.GetText(), 10);

        var strIdMovimientoTecho = txtIdMovimientoTecho.GetText();
        var strIdMovimientoReserva = txtIdMovimientoReserva.GetText();
        var strIdMovimientoAsignado = txtIdMovimientoAsignado.GetText();

        if (mensaje) {
            if (numTecho == 0 && numReserva == 0 && numAsignado == 0) {
                alert("¡El volumen de Techo, Reserva y Asignado no pueden tener valor 0!");
            }
            else if (numTecho < 0) {
                alert("¡El volumen de Techo, no debe ser menor a 0!");
            }
            else if (numReserva < 0) {
                alert("¡El volumen de Reserva, no debe ser menor a 0!");
            }
            else if (numAsignado < 0) {
                alert("¡El volumen de Asignado, no debe ser menor a 0!");
            }
            else if ((numReserva + numAsignado) != numTecho) {
                alert("¡La sumatoria del volumen Reserva y Asignado, no puede ser mayor o menor al volumen Techo insertado!");
            }
            else if (numReserva > numAsignado) {
                alert("¡El volumen Reserva no puede ser mayor al Volumen Asignado!");
            }
            else if (strIdMovimientoTecho != "0" && strIdMovimientoReserva != "0" && strIdMovimientoAsignado != "0") {
                alert("¡Ya existe datos PRODE registrados!");
            }
            else {
                pnlDatos.PerformCallback(opcion);
            }
        }
        else {
            alert('¡Ha cancelado la operacion!');
        }
    }

    function onClicEliminarTechoProde(opcion) {
        var mensaje = confirm("¿Desea eliminar el volumen techo, volumen asignado y volumen reservado de PRODE?");

        var strIdMovimientoTecho = txtIdMovimientoTecho.GetText();
        var strIdMovimientoReserva = txtIdMovimientoReserva.GetText();
        var strIdMovimientoAsignado = txtIdMovimientoAsignado.GetText();

        if (mensaje) {
            if (strIdMovimientoTecho == "0" && strIdMovimientoReserva == "0" && strIdMovimientoAsignado == "0") {
                alert("¡No se tienen datos de PRODE registrado!");
            }
            else {
                pnlDatos.PerformCallback(opcion);
            }
        }
        else {
            alert('¡Ha cancelado la operacion!');
        }
    }

</script>

<div class="col-md-12">
    <table style="width: 100%">
        <tr>
            <td style="text-align: center; padding-bottom: 1px">
                <dx:ASPxRoundPanel runat="server" ShowHeader="False" Width="100%" ID="pnlTitulo">
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent1" runat="server">
                            <dx:ASPxLabel runat="server" ForeColor="#367B2F" ID="lblTitulo" Font-Bold="True" Font-Names="Rockwell" Font-Size="Larger" Text="REGISTRO PRODE"></dx:ASPxLabel>
                        </dx:PanelContent>
                    </PanelCollection>
                    <Border BorderStyle="Ridge" BorderWidth="1px" BorderColor="#003300"></Border>
                </dx:ASPxRoundPanel>
            </td>
        </tr>
    </table>

    <dx:ASPxSplitter ID="SplEntorno" runat="server" Height="100%" ClientInstanceName="SplEntorno">
        <Panes>
            <dx:SplitterPane Size="15%" AllowResize="True" AutoHeight="True">
                <ContentCollection>
                    <dx:SplitterContentControl ID="SplitterContentControl1" runat="server">                        
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
                                    <dx:ASPxCallbackPanel ID="pnlCabecera" runat="server" ClientInstanceName="pnlCabecera" Width="100%">
                                        <PanelCollection>
                                            <dx:PanelContent ID="PanelContent2" runat="server">                                                
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxLabel ID="ASPxLabel1" runat="server" ForeColor="#003300" Text="Periodo Mes :">
                                                            </dx:ASPxLabel>
                                                            <dx:ASPxComboBox ID="cmbPeriodoMes" runat="server" EnableTheming="True" Theme="Metropolis" ValueType="System.Int32" Width="100%" ToolTip="Seleccione un Periodo Mes" ClientInstanceName="cmbPeriodoMes">
                                                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
	                                                                                pnlDatos.PerformCallback();
                                                                                }" />
                                                                <Items>
                                                                    <dx:ListEditItem Selected="True" Text="ENERO" Value="1" />
                                                                    <dx:ListEditItem Text="FEBRERO" Value="2" />
                                                                    <dx:ListEditItem Text="MARZO" Value="3" />
                                                                    <dx:ListEditItem Text="ABRIL" Value="4" />
                                                                    <dx:ListEditItem Text="MAYO" Value="5" />
                                                                    <dx:ListEditItem Text="JUNIO" Value="6" />
                                                                    <dx:ListEditItem Text="JULIO" Value="7" />
                                                                    <dx:ListEditItem Text="AGOSTO" Value="8" />
                                                                    <dx:ListEditItem Text="SEPTIEMBRE" Value="9" />
                                                                    <dx:ListEditItem Text="OCTUBRE" Value="10" />
                                                                    <dx:ListEditItem Text="NOVIEMBRE" Value="11" />
                                                                    <dx:ListEditItem Text="DICIEMBRE" Value="12" />
                                                                </Items>
                                                                <ClearButton Visibility="Auto">
                                                                </ClearButton>
                                                            </dx:ASPxComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxLabel ID="ASPxLabel56" runat="server" ForeColor="#003300" Text="Periodo Gestión:">
                                                            </dx:ASPxLabel>
                                                            <dx:ASPxComboBox ID="cmbPeriodoGestion" runat="server" EnableTheming="True" Theme="Metropolis" ValueType="System.Int32" Width="100%" ToolTip="Seleccione un Periodo Gestion" ClientInstanceName="cmbPeriodoGestion">
                                                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
	                                                                                pnlDatos.PerformCallback();
                                                                                }" />
                                                                <Items>                                                                    
                                                                    <dx:ListEditItem Selected="True" Text="2019" Value="2019" />
                                                                    <dx:ListEditItem Text="2020" Value="2020" />
                                                                    <dx:ListEditItem Text="2021" Value="2021" />
                                                                    <dx:ListEditItem Text="2022" Value="2022" />
                                                                    <dx:ListEditItem Text="2023" Value="2023" />
                                                                    <dx:ListEditItem Text="2024" Value="2024" />
                                                                    <dx:ListEditItem Text="2025" Value="2025" />
                                                                    <dx:ListEditItem Text="2026" Value="2026" />                                                                  
                                                                </Items>
                                                                <ClearButton Visibility="Auto">
                                                                </ClearButton>
                                                            </dx:ASPxComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxLabel ID="ASPxLabel57" runat="server" ForeColor="#003300" Text="">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <%--<dx:ASPxTreeList ID="tlsOperadores" runat="server" AutoGenerateColumns="False" ClientInstanceName="tlsOperadores" Font-Size="Smaller" Height="187px" KeyFieldName="intId" ParentFieldName="intIdPadre" Theme="Youthful" Styles-Header-BackColor="#317D40" ToolTip="Seleccione un proyecto" OnCustomCallback="tlsOperadores_CustomCallback">--%>
                                                            <dx:ASPxTreeList ID="tlsOperadores" runat="server" AutoGenerateColumns="False" ClientInstanceName="tlsOperadores" Font-Size="Smaller" Height="187px" KeyFieldName="intId" ParentFieldName="intIdPadre" Theme="Youthful" Styles-Header-BackColor="#317D40" ToolTip="Seleccione un proyecto">
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

                                                            </dx:ASPxTreeList>
                                                        </td>
                                                    </tr>
                                                    
                                                    <tr>
                                                        <td style="padding-bottom: 5px">                                                            
                                                        &nbsp;</td>
                                                    </tr>
                                                </table>
                                            </dx:PanelContent>
                                        </PanelCollection>
                                    </dx:ASPxCallbackPanel>
                                </td>
                            </tr>
                        </table>
                    </dx:SplitterContentControl>
                </ContentCollection>
            </dx:SplitterPane>
            <dx:SplitterPane ScrollBars="Auto">
                <ContentCollection>
                    <dx:SplitterContentControl ID="SplitterContentControl2" runat="server">
                                                
                        <dx:ASPxPageControl ID="apcProgramacion" Width="100%" runat="server" ActiveTabIndex="0" Theme="Metropolis" EnableHierarchyRecreation="true">
                            <TabPages>
                                <dx:TabPage Text="Actividad">
                                    <ContentCollection>
                                        <dx:ContentControl ID="ContentControl1" runat="server">
                                            <table style="width:100%">
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
                                                    <td style="width:100%">
                                                        <legend>
                                                            <asp:Label ID="Label12" runat="server" Font-Bold="True" Font-Names="Rockwell" Font-Size="Medium" ForeColor="#367B2F" Text="PROGRAMACION PRODE POR ACTIVIDAD"></asp:Label>
                                                        </legend>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td style="width:100%">
                                                        <dx:ASPxPivotGrid ID="pvgProdeActividad" runat="server" ClientIDMode="AutoID" Width="100%" EnableTheming="True" Font-Size="X-Small" Theme="Default">
	                                                        <Fields>
                                                                <dx:PivotGridField ID="fieldIdProde" AreaIndex="0" Caption="Id" FieldName="ID_PRODE" Visible="False">
		                                                        </dx:PivotGridField>
                                                                <dx:PivotGridField ID="fieldActividad" AreaIndex="1" Caption="Actividad" FieldName="ACTIVIDAD" Area="RowArea">
		                                                        </dx:PivotGridField>
                                                                <dx:PivotGridField ID="fieldProducto" AreaIndex="2" Caption="Producto" FieldName="PRODUCTO" Area="DataArea">
		                                                        </dx:PivotGridField>
                                                            </Fields>
                                                            <Styles CssPostfix="DevGlp"></Styles>
	                                                        <OptionsView ShowColumnGrandTotalHeader="False" ShowColumnGrandTotals="False" />
	                                                        <OptionsPager RowsPerPage="100" AlwaysShowPager="True">
		                                                        <PageSizeItemSettings Items="10, 100, 1000" Visible="True" ShowAllItem="True">
		                                                        </PageSizeItemSettings>
	                                                        </OptionsPager>
                                                        </dx:ASPxPivotGrid>                                                        
                                                    </td>
                                                </tr>
                                            </table>

                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:TabPage>
                                <dx:TabPage Text="Departamento">
                                    <ContentCollection>
                                        <dx:ContentControl ID="ContentControl2" runat="server">
                                            <table style="width:100%">
                                                <tr>
                                                    <td colspan="6">
                                                        <legend>
                                                            <asp:Label ID="Label13" runat="server" Font-Bold="True" Font-Names="Rockwell" Font-Size="Medium" ForeColor="#367B2F" Text="PROGRAMACION PRODE POR DEPARTAMENTO"></asp:Label>
                                                        </legend>
                                                    </td>
                                                </tr>
                                            </table>

                                            <dx:ASPxPivotGrid ID="pvgProdeActResumen" runat="server" ClientIDMode="AutoID" Width="100%" EnableTheming="True" Font-Size="X-Small" Theme="Default">
	                                            <Fields>
                                                    <dx:PivotGridField ID="PivotGridField1" AreaIndex="0" Caption="Id" FieldName="ID_PRODE" Visible="False">
		                                            </dx:PivotGridField>
                                                    <dx:PivotGridField ID="PivotGridField2" AreaIndex="1" Caption="Tipo Prode" FieldName="TIPO_PRODE" Area="RowArea">
		                                            </dx:PivotGridField>
                                                    <dx:PivotGridField ID="PivotGridField3" AreaIndex="2" Caption="Producto" FieldName="PRODUCTO" Area="DataArea">
		                                            </dx:PivotGridField>
                                                </Fields>
                                                <Styles CssPostfix="DevGlp"></Styles>
	                                            <OptionsView ShowColumnGrandTotalHeader="False" ShowColumnGrandTotals="False" />
	                                            <OptionsPager RowsPerPage="100" AlwaysShowPager="True">
		                                            <PageSizeItemSettings Items="10, 100, 1000" Visible="True" ShowAllItem="True">
		                                            </PageSizeItemSettings>
	                                            </OptionsPager>
                                            </dx:ASPxPivotGrid>
                                            <br />
                                            <dx:ASPxPivotGrid ID="pvgProdeDepartamento" runat="server" ClientIDMode="AutoID" Width="100%" EnableTheming="True" Font-Size="X-Small" Theme="Default">
	                                            <Fields>
                                                    <dx:PivotGridField ID="PivotGridField4" AreaIndex="0" Caption="Id" FieldName="ID_PRODE" Visible="False">
		                                            </dx:PivotGridField>
                                                    <dx:PivotGridField ID="PivotGridField5" AreaIndex="1" Caption="Departamento" FieldName="DEPARTAMENTO" Area="RowArea">
		                                            </dx:PivotGridField>
                                                    <dx:PivotGridField ID="PivotGridField6" AreaIndex="2" Caption="Producto" FieldName="PRODUCTO" Area="DataArea">
		                                            </dx:PivotGridField>
                                                    <dx:PivotGridField ID="PivotGridField7" AreaIndex="3" Caption="Tipo Prode" FieldName="TIPO_PRODE" Area="DataArea">
		                                            </dx:PivotGridField>
                                                </Fields>
                                                <Styles CssPostfix="DevGlp"></Styles>
	                                            <OptionsView ShowColumnGrandTotalHeader="False" ShowColumnGrandTotals="False" />
	                                            <OptionsPager RowsPerPage="100" AlwaysShowPager="True">
		                                            <PageSizeItemSettings Items="10, 100, 1000" Visible="True" ShowAllItem="True">
		                                            </PageSizeItemSettings>
	                                            </OptionsPager>
                                            </dx:ASPxPivotGrid>

                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:TabPage>
                                <dx:TabPage Text="Operador">
                                    <ContentCollection>
                                        <dx:ContentControl ID="ContentControl3" runat="server">
                                            <table style="width:100%">
                                                <tr>
                                                    <td colspan="6">
                                                        <legend>
                                                            <asp:Label ID="Label14" runat="server" Font-Bold="True" Font-Names="Rockwell" Font-Size="Medium" ForeColor="#367B2F" Text="PROGRAMACION PRODE POR OPERADOR"></asp:Label>
                                                        </legend>
                                                    </td>
                                                </tr>
                                            </table>

                                            <dx:ASPxPageControl ID="apcSemanal" Width="100%" runat="server" ActiveTabIndex="0" Theme="Metropolis" EnableHierarchyRecreation="true">
                                                <TabPages>
                                                    <dx:TabPage Text="Semana 1">
                                                        <ContentCollection>
                                                            <dx:ContentControl ID="ContentControl4" runat="server">
                                                            </dx:ContentControl>
                                                        </ContentCollection>
                                                    </dx:TabPage>
                                                    <dx:TabPage Text="Semana 2">
                                                        <ContentCollection>
                                                            <dx:ContentControl ID="ContentControl5" runat="server">                                                                
                                                            </dx:ContentControl>
                                                        </ContentCollection>
                                                    </dx:TabPage>
                                                    <dx:TabPage Text="Semana 3">
                                                        <ContentCollection>
                                                            <dx:ContentControl ID="ContentControl6" runat="server">                                                                
                                                            </dx:ContentControl>
                                                        </ContentCollection>
                                                    </dx:TabPage>
                                                    <dx:TabPage Text="Semana 4">
                                                        <ContentCollection>
                                                            <dx:ContentControl ID="ContentControl7" runat="server">                                                                
                                                            </dx:ContentControl>
                                                        </ContentCollection>
                                                    </dx:TabPage>
                                                    <dx:TabPage Text="Semana 5">
                                                        <ContentCollection>
                                                            <dx:ContentControl ID="ContentControl8" runat="server">                                                                
                                                            </dx:ContentControl>
                                                        </ContentCollection>
                                                    </dx:TabPage>
                                                </TabPages>
                                            </dx:ASPxPageControl>

                                        </dx:ContentControl>
                                    </ContentCollection>
                                </dx:TabPage>
                            </TabPages>
                        </dx:ASPxPageControl>
                        
                    </dx:SplitterContentControl>
                </ContentCollection>
            </dx:SplitterPane>
        </Panes>
    </dx:ASPxSplitter>
</div>