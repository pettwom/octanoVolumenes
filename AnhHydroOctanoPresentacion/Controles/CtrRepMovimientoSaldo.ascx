<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtrRepMovimientoSaldo.ascx.cs" Inherits="AnhHydroOctanoPresentacion.Controles.CtrRepMovimientoSaldo" %>

<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>

<%@ Register src="CtrFecheroMes.ascx" tagname="CtrFecheroMes" tagprefix="uc1" %>





<style type="text/css">
    .hr-text {
      line-height: 1em;
      position: relative;
      outline: 0;
      border: 0;
      color: black;
      text-align: center;
      height: 1.5em;
      opacity: 0.5;
    }
    .hr-text:before {
      content: "";
      background: linear-gradient(to right, transparent, #818078, transparent);
      position: absolute;
      left: 0;
      top: 50%;
      width: 100%;
      height: 1px;
    }
    .hr-text:after {
      content: attr(data-content);
      position: relative;
      display: inline-block;
      color: black;
      padding: 0 0.5em;
      line-height: 1.5em;
      color: black;
      background-color: #fcfcfa;
    }
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
    
    .auto-style14 {
        width: 202px;
    }
    
    </style>

<script type="text/javascript">
    function numbersonly(e) {
        var unicode = e.charCode ? e.charCode : e.keyCode
        if (unicode != 8 && unicode != 44) {
            if (unicode < 48 || unicode > 57) //if not a number
            { return false } //disable key press    
        }
    }

   
   

</script>

<div class="col-md-12">
    
    <table style="width: 100%; height: 100%">
    <tr>
        <td style="text-align: center; padding-bottom: 1px">
            <dx:ASPxRoundPanel runat="server" ShowHeader="False" Width="100%" ID="pnlTitulo">
                <PanelCollection>
                    <dx:PanelContent ID="PanelContent1" runat="server">
                        <dx:ASPxLabel runat="server" ForeColor="#367B2F" ID="lblTitulo" Font-Bold="True" Font-Names="Rockwell" Font-Size="Larger" Text="REPORTE DE SALDOS EN BASE A LA INFRAESTRUCTURA"></dx:ASPxLabel>
                </dx:PanelContent>
            </PanelCollection>
            <Border BorderStyle="Ridge" BorderWidth="1px" BorderColor="#003300"></Border>
            </dx:ASPxRoundPanel>
        </td>
    </tr>
    
    <tr>
        <td style="height: 100%">
            
            <dx:ASPxSplitter ID="ASPxSplitter1" runat="server" Height="98%" Width="100%" >
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
                            <td style="padding-bottom: 5px">
                                <dx:ASPxCallbackPanel ID="pnlCabecera" runat="server" ClientInstanceName="pnlCabecera" Width="160px">
                                    <PanelCollection>
                                        <dx:PanelContent ID="PanelContent2" runat="server">
                                            <table style="width: 100%">
                                                <tr>
                                                    <td>
                                                        Fecha Inicial :
                                                        <dx:ASPxDateEdit ID="detFechaIni" runat="server" ClientInstanceName="detFechaIni" Theme="Metropolis" ToolTip="Seleccione la fecha inicial" Width="140px">
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
                                                <tr style="padding-bottom: 5px">
                                                    <td>
                                                        Fecha Final :
                                                        <dx:ASPxDateEdit ID="detFechaFin" runat="server" ClientInstanceName="detFechaFin" Theme="Metropolis" Width="140px" ToolTip="Seleccione la fecha final">
                                                            <TimeSectionProperties>
                                                                <TimeEditProperties>
                                                                    <ClearButton Visibility="Auto">
                                                                    </ClearButton>
                                                                </TimeEditProperties>
                                                            </TimeSectionProperties>
                                                            <ClientSideEvents DateChanged="function(s, e) {
//pnlDatos.PerformCallback();
}" />
                                                            <ClearButton Visibility="Auto">
                                                            </ClearButton>
                                                        </dx:ASPxDateEdit>
                                                    </td>
                                                </tr>
                                            </table>
                                        </dx:PanelContent>
                                    </PanelCollection>
                                </dx:ASPxCallbackPanel>
                            </td>
                        </tr>
                        

                        <tr>
                            <td style="padding-bottom: 3px">
                                Selección Directa de Mes :
                                <uc1:CtrFecheroMes ID="cfmMes" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-bottom: 5px">
                                <dx:ASPxButton ID="btnConsultar" runat="server" AutoPostBack="False" Font-Bold="True" ForeColor="Maroon" Text="Obtener Información" Theme="Metropolis" Width="55%">
                                    <ClientSideEvents Click="function(s, e) {
pnlDatos.PerformCallback();
}" />
                                    <Image IconID="actions_download_16x16">
                                    </Image>
                                </dx:ASPxButton>
                            </td>
                        </tr>

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
                                <dx:ASPxButton ID="btnBuscarProyecto" runat="server" AutoPostBack="false" ClientInstanceName="btnBuscarProyecto" Text="Buscar Proyecto" >
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
                                <dx:ASPxTreeList ID="tlsOperadores" runat="server" AutoGenerateColumns="False" ClientInstanceName="tlsOperadores" Font-Size="Smaller" Height="187px" KeyFieldName="intId" ParentFieldName="intIdPadre" Theme="Youthful" Styles-Header-BackColor="#317D40" ToolTip="Seleccione un proyecto" OnCustomCallback="tlsOperadores_CustomCallback" >
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

                                    <ClientSideEvents NodeDblClick="function(s, e) {
pnlDatos.PerformCallback();
}" />
                                </dx:ASPxTreeList>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                &nbsp;</td>
                        </tr>

                    </table>
                </dx:SplitterContentControl>
            </ContentCollection>
        </dx:SplitterPane>
        <dx:SplitterPane ScrollBars="Auto">
            <ContentCollection>
                 <dx:SplitterContentControl ID="SplitterContentControl2" runat="server">
                    <dx:ASPxCallbackPanel ID="pnlDatos" runat="server" Theme="Youthful" Width="100%" ClientInstanceName="pnlDatos" OnCallback="pnlDatos_Callback">
                        <ClientSideEvents EndCallback="function(s, e) {
	grafficardatosprinc(s, e);
	LoadingPanel.Hide();
}" BeginCallback="function(s, e) {
	LoadingPanel.Show();
}" />
                        <PanelCollection>
                            <dx:PanelContent ID="PanelContent3" runat="server">
                                <table style="width: 100%;">
                                    <tr>
                                        <td colspan="7" style="text-align: center;width: 100%;">
                                            <dx:ASPxRoundPanel ID="pnlMensaje" runat="server" ShowHeader="False" Visible="False" Width="100%">
                                                <PanelCollection>
                                                    <dx:PanelContent ID="PanelContent4" runat="server">
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
                                    <tr>
                                        <td colspan="3" style="width: 50%">
                                            <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="#10420F" Text="NOMBRE COMERCIAL"></asp:Label>
                                        </td>
                                        <td style="width: 15%">
                                            <asp:Label ID="Label3" runat="server" Font-Bold="True" ForeColor="#10420F" Text="NIT"></asp:Label>
                                        </td>
                                        <td style="width: 15%">
                                            <asp:Label ID="Label5" runat="server" Font-Bold="True" ForeColor="#10420F" Text="DEPARTAMENTO"></asp:Label>
                                        </td>
                                        <td colspan="2" style="width: 20%">
                                            <asp:Label ID="Label4" runat="server" Font-Bold="True" ForeColor="#10420F" Text="DIRECCION"></asp:Label>
                                        </td>

                                    </tr>
                                        <tr>
                                            <td colspan="3" style="width: 50%">
                                                <dx:ASPxLabel ID="lblRazonSocial" runat="server" ClientInstanceName="lblRazonSocial">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td style="width: 15%">
                                                <asp:Label ID="lblNit" runat="server"></asp:Label>
                                            </td>
                                            <td style="width: 15%">
                                                <asp:Label ID="lblDepto" runat="server"></asp:Label>
                                            </td>
                                            <td colspan="2" style="width: 20%">
                                                <asp:Label ID="lblRepLegal" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="7">
                                                
                                                <dx:ASPxPageControl ID="pctPanel" runat="server" ActiveTabIndex="0" Font-Names="Verdana" Font-Size="Smaller" Theme="Metropolis" Width="100%">
                                                    <TabPages>
                                                        <dx:TabPage Text="REPORTE ACUMULADO">
                                                            <ContentCollection>
                                                                <dx:ContentControl ID="ContentControl1" runat="server">
                                                                    <table style="width: 100%">
                                                                        <tr>
                                                                            <td style="width: 55%">
                                                                                
                                                                            </td>
                                                                            <td style="width: 15%">
                                                                                
                                                                                &nbsp;</td>
                                                                            <td style="width: 15%">
                                                                                
                                                                                &nbsp;</td>
                                                                            <td style="width: 15%">
                                                                                <table>
                                                                                    <tr>
                                                                                       
                                                                                         <td>
                                                                                             &nbsp;</td>
                                                                                        <td>
                                                                                            <dx:ASPxButton ID="btnExportarAcum" runat="server" AutoPostBack="False" Font-Bold="True" ForeColor="Maroon" OnClick="btnExportarAcum_Click" Text="Exportar Excel" Theme="Metropolis" Width="200px">
                                                                                                                                                                                <ClientSideEvents Click="function(s, e) {
	                                                                                            e.processOnServer = confirm('¿Desea exportar el resumen acumulado?'); 
            }" />
                                                                                                <Image IconID="export_exporttoxls_16x16">
                                                                                                </Image>
                                                                                            </dx:ASPxButton>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                              
                                                                                

                                                                                
                                                                                
                                                                                
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="4">
                                                                                
                                                                                <dx:ASPxGridView ID="dgvAcumulado" runat="server" AutoGenerateColumns="False" ClientInstanceName="dgvAcumulado" EnableTheming="True" Font-Size="Smaller" KeyFieldName="IntId" OnHtmlDataCellPrepared="dgvAcumulado_HtmlDataCellPrepared" OnHtmlRowPrepared="dgvAcumulado_HtmlRowPrepared" Theme="Default" Width="100%">
                                                                                    <Styles>
                                                                                        <Header Wrap="True" HorizontalAlign="Center"></Header>

<AlternatingRow BackColor="#EEEEEE"></AlternatingRow>
                                                                                    </Styles>
                                                                                    <ClientSideEvents CustomButtonClick="function(s, e) {
	
    switch(e.buttonID) {
    case&nbsp; 'btnMed':
    onCellClick(s.GetFocusedRowIndex(), 'MDIN',this);
    break;
    } 


}" />
                                                                                    <SettingsContextMenu EnableRowMenu="False">
                                                                                    </SettingsContextMenu>
                                                                                    <Templates>
                                                                                        <EditForm>
                                                                                        </EditForm>
                                                                                    </Templates>
                                                                                    <SettingsPager PageSize="100">
                                                                                        <PageSizeItemSettings Items="50, 100, 200, 500, 1000" Visible="True">
                                                                                        </PageSizeItemSettings>
                                                                                    </SettingsPager>
                                                                                    <SettingsEditing Mode="PopupEditForm">
                                                                                    </SettingsEditing>
                                                                                    <Settings ShowFooter="True" ShowGroupFooter="VisibleAlways" ShowGroupPanel="True" ShowHeaderFilterButton="True" />
                                                                                    <SettingsBehavior AllowFocusedRow="True" ColumnResizeMode="NextColumn" />
                                                                                    <SettingsPopup>
                                                                                        <EditForm Modal="True" />
                                                                                        <CustomizationWindow HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" />
                                                                                    </SettingsPopup>
                                                                                    <SettingsText CommandNew="Nuevo" />
                                                                                    <EditFormLayoutProperties ColCount="2">
                                                                                        <Items>
                                                                                            <dx:GridViewColumnLayoutItem ColumnName="StrFecha">
                                                                                            </dx:GridViewColumnLayoutItem>
                                                                                            <dx:GridViewColumnLayoutItem ColumnName="StrCodProducto">
                                                                                            </dx:GridViewColumnLayoutItem>
                                                                                            <dx:GridViewColumnLayoutItem ColumnName="DecSaldoInicial">
                                                                                            </dx:GridViewColumnLayoutItem>
                                                                                            <dx:GridViewColumnLayoutItem ColumnName="DecTotalIng">
                                                                                            </dx:GridViewColumnLayoutItem>
                                                                                            <dx:GridViewColumnLayoutItem ColumnName="DecTotalSal">
                                                                                            </dx:GridViewColumnLayoutItem>
                                                                                            <dx:GridViewColumnLayoutItem ColumnName="DecSaldoFinal">
                                                                                            </dx:GridViewColumnLayoutItem>
                                                                                            <dx:EditModeCommandLayoutItem ColSpan="2" HorizontalAlign="Right">
                                                                                            </dx:EditModeCommandLayoutItem>
                                                                                        </Items>
                                                                                    </EditFormLayoutProperties>
                                                                                    <Columns>
                                                                                        <dx:GridViewDataTextColumn Caption="IntId" FieldName="IntId" ShowInCustomizationForm="True" Visible="False" VisibleIndex="0">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="IntContProd" FieldName="IntContProd" ShowInCustomizationForm="True" Visible="False" VisibleIndex="1">
                                                                                        </dx:GridViewDataTextColumn>                                                                                        
                                                                                        <dx:GridViewDataTextColumn Caption="FECHA" FieldName="StrFecha" ShowInCustomizationForm="True" VisibleIndex="2">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="PROD." FieldName="StrCodProducto" GroupIndex="0" ShowInCustomizationForm="True" SortIndex="0" SortOrder="Ascending" ToolTip="Producto" VisibleIndex="3">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="TIPO INFRA." FieldName="StrTipoInfraestructura"  Visible="False"  ShowInCustomizationForm="True" SortIndex="1" SortOrder="Ascending" ToolTip="Tipo de Infrastructura" VisibleIndex="4">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="COD. INFRA." FieldName="StrCodigoInfraestructura" Visible="False"  ShowInCustomizationForm="True" SortIndex="2" SortOrder="Ascending" ToolTip="Código de Infraestructura" VisibleIndex="5">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="DIA" FieldName="DecDia" ShowInCustomizationForm="True" VisibleIndex="6">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="DESCRIPCION DIA" FieldName="StrDescripcionDia" ShowInCustomizationForm="True" VisibleIndex="7">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="MES" FieldName="StrMes" ShowInCustomizationForm="True" VisibleIndex="8">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="AÑO" FieldName="StrAnio" ShowInCustomizationForm="True" VisibleIndex="9">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="SALDO INICIAL" FieldName="DecSaldoInicial" ShowInCustomizationForm="True" VisibleIndex="10">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="DecIng01" FieldName="DecIng01" ShowInCustomizationForm="True" Visible="false"  VisibleIndex="11">
                                                                                            <PropertiesTextEdit ClientInstanceName="DecInf01" EnableClientSideAPI="True">
                                                                                            </PropertiesTextEdit>
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="DecIng02" FieldName="DecIng02" ShowInCustomizationForm="True" Visible="false" VisibleIndex="12">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="DecIng03" FieldName="DecIng03" ShowInCustomizationForm="True" Visible="False" VisibleIndex="13">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="DecIng04" FieldName="DecIng04" ShowInCustomizationForm="True" Visible="False" VisibleIndex="14">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="DecIng05" FieldName="DecIng05" ShowInCustomizationForm="True" Visible="False" VisibleIndex="15">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="DecIng06" FieldName="DecIng06" ShowInCustomizationForm="True" Visible="False" VisibleIndex="16">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="DecIng07" FieldName="DecIng07" ShowInCustomizationForm="True" Visible="False" VisibleIndex="17">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="DecIng08" FieldName="DecIng08" ShowInCustomizationForm="True" Visible="False" VisibleIndex="18">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="DecIng09" FieldName="DecIng09" ShowInCustomizationForm="True" Visible="False" VisibleIndex="19">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="DecIng10" FieldName="DecIng10" ShowInCustomizationForm="True" Visible="False" VisibleIndex="20">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="DecIng11" FieldName="DecIng11" ShowInCustomizationForm="True" Visible="False" VisibleIndex="21">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="DecIng12" FieldName="DecIng12" ShowInCustomizationForm="True" Visible="False" VisibleIndex="22">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="DecIng13" FieldName="DecIng13" ShowInCustomizationForm="True" Visible="False" VisibleIndex="23">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="DecIng14" FieldName="DecIng14" ShowInCustomizationForm="True" Visible="False" VisibleIndex="24">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="DecIng15" FieldName="DecIng15" ShowInCustomizationForm="True" Visible="False" VisibleIndex="25">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="DecIng16" FieldName="DecIng16" ShowInCustomizationForm="True" Visible="False" VisibleIndex="26">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="DecIng17" FieldName="DecIng17" ShowInCustomizationForm="True" Visible="False" VisibleIndex="27">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="DecIng18" FieldName="DecIng18" ShowInCustomizationForm="True" Visible="False" VisibleIndex="28">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="DecIng19" FieldName="DecIng19" ShowInCustomizationForm="True" Visible="False" VisibleIndex="29">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="DecIng20" FieldName="DecIng20" ShowInCustomizationForm="True" Visible="false" VisibleIndex="30">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="TOTAL ING." FieldName="DecTotalIng" ShowInCustomizationForm="True" Visible="false" ToolTip="Total de Ingresos" VisibleIndex="31">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="DecSal01" FieldName="DecSal01" ShowInCustomizationForm="True" Visible="false" VisibleIndex="32">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="DecSal02" FieldName="DecSal02" ShowInCustomizationForm="True" Visible="False" VisibleIndex="33">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="DecSal03" FieldName="DecSal03" ShowInCustomizationForm="True" Visible="False" VisibleIndex="34">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="DecSal04" FieldName="DecSal04" ShowInCustomizationForm="True" Visible="False" VisibleIndex="35">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="DecSal05" FieldName="DecSal05" ShowInCustomizationForm="True" Visible="False" VisibleIndex="36">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="DecSal06" FieldName="DecSal06" ShowInCustomizationForm="True" Visible="False" VisibleIndex="37">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="DecSal07" FieldName="DecSal07" ShowInCustomizationForm="True" Visible="False" VisibleIndex="38">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="DecSal08" FieldName="DecSal08" ShowInCustomizationForm="True" Visible="False" VisibleIndex="39">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="DecSal09" FieldName="DecSal09" ShowInCustomizationForm="True" Visible="False" VisibleIndex="40">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="DecSal10" FieldName="DecSal10" ShowInCustomizationForm="True" Visible="False" VisibleIndex="41">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="DecSal11" FieldName="DecSal11" ShowInCustomizationForm="True" Visible="False" VisibleIndex="42">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="DecSal12" FieldName="DecSal12" ShowInCustomizationForm="True" Visible="False" VisibleIndex="43">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="DecSal13" FieldName="DecSal13" ShowInCustomizationForm="True" Visible="False" VisibleIndex="44">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="DecSal14" FieldName="DecSal14" ShowInCustomizationForm="True" Visible="False" VisibleIndex="45">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="DecSal15" FieldName="DecSal15" ShowInCustomizationForm="True" Visible="False" VisibleIndex="46">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="DecSal16" FieldName="DecSal16" ShowInCustomizationForm="True" Visible="False" VisibleIndex="47">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="DecSal17" FieldName="DecSal17" ShowInCustomizationForm="True" Visible="False" VisibleIndex="48">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="DecSal18" FieldName="DecSal18" ShowInCustomizationForm="True" Visible="False" VisibleIndex="49">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="DecSal19" FieldName="DecSal19" ShowInCustomizationForm="True" Visible="False" VisibleIndex="50">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="DecSal20" FieldName="DecSal20" ShowInCustomizationForm="True" Visible="false" VisibleIndex="51">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="TOTAL .SAL." FieldName="DecTotalSal"  Visible="true" ShowInCustomizationForm="True" ToolTip="Total de Salidas" VisibleIndex="52">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="SALDO FINAL" FieldName="DecSaldoFinal" ShowInCustomizationForm="True" VisibleIndex="53">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="IMPORTE TOTAL" FieldName="DecImporteTotal" ShowInCustomizationForm="True" Visible="false" VisibleIndex="54">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="PRODUCTO" FieldName="StrProducto"  Visible="false" VisibleIndex="55">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="INFRA" FieldName="StrInfraestructura"  Visible="False" VisibleIndex="56">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="RESOLUCION"  ShowInCustomizationForm="True" FieldName="StrObservaciones"  Visible="False" VisibleIndex="57">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        
                                                                                        
                                                                                        <dx:GridViewDataTextColumn Caption="DETALLE DE TANQUES" FieldName="StrDescripcionTanque" ShowInCustomizationForm="True" VisibleIndex="58">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="CAPACIDAD TANQUE" FieldName="DecVolumenNominal" ShowInCustomizationForm="True" VisibleIndex="59">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="PROGRAMACION" FieldName="DecProgramacion" ShowInCustomizationForm="True" VisibleIndex="60">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="RECEP./PROG." FieldName="DecRecepProg" ShowInCustomizationForm="True" VisibleIndex="61">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        <dx:GridViewDataTextColumn Caption="VENTA / PROGRAMACION" FieldName="DecVentaProg" ShowInCustomizationForm="True" VisibleIndex="62">
                                                                                        </dx:GridViewDataTextColumn>
                                                                                        
                                                                                    </Columns>
                                                                                    <Styles CssPostfix="DevGlp"><AlternatingRow BackColor="#EEEEEE">
                                                        </AlternatingRow></Styles>
                                                                                    <TotalSummary>
                                                                                        <dx:ASPxSummaryItem FieldName="DecIng01" ShowInColumn="DecIng01" SummaryType="Sum" Tag="DecIng01" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="DecIng02" ShowInColumn="DecIng02" SummaryType="Sum" Tag="DecIng02" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="DecIng03" ShowInColumn="DecIng03" SummaryType="Sum" Tag="DecIng03" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="DecIng04" ShowInColumn="DecIng04" SummaryType="Sum" Tag="DecIng04" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="DecIng05" ShowInColumn="DecIng05" SummaryType="Sum" Tag="DecIng05" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="DecIng06" ShowInColumn="DecIng06" SummaryType="Sum" Tag="DecIng06" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="DecIng07" ShowInColumn="DecIng07" SummaryType="Sum" Tag="DecIng07" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="DecIng08" ShowInColumn="DecIng08" SummaryType="Sum" Tag="DecIng08" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="DecIng09" ShowInColumn="DecIng09" SummaryType="Sum" Tag="DecIng09" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="DecIng10" ShowInColumn="DecIng10" SummaryType="Sum" Tag="DecIng10" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="DecIng11" ShowInColumn="DecIng11" SummaryType="Sum" Tag="DecIng11" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="DecIng12" ShowInColumn="DecIng12" SummaryType="Sum" Tag="DecIng12" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="DecIng13" ShowInColumn="DecIng13" SummaryType="Sum" Tag="DecIng13" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="DecIng14" ShowInColumn="DecIng14" SummaryType="Sum" Tag="DecIng14" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="DecIng15" ShowInColumn="DecIng15" SummaryType="Sum" Tag="DecIng15" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="DecIng16" ShowInColumn="DecIng16" SummaryType="Sum" Tag="DecIng16" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="DecIng17" ShowInColumn="DecIng17" SummaryType="Sum" Tag="DecIng17" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="DecIng18" ShowInColumn="DecIng18" SummaryType="Sum" Tag="DecIng18" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="DecIng19" ShowInColumn="DecIng19" SummaryType="Sum" Tag="DecIng19" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="DecIng20" ShowInColumn="DecIng20" SummaryType="Sum" Tag="DecIng20" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="DecTotalIng" ShowInColumn="DecTotalIng" SummaryType="Sum" Tag="DecTotalIng" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="DecSal01" ShowInColumn="DecSal01" SummaryType="Sum" Tag="DecSal01" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="DecSal02" ShowInColumn="DecSal02" SummaryType="Sum" Tag="DecSal02" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="DecSal03" ShowInColumn="DecSal03" SummaryType="Sum" Tag="DecSal03" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="DecSal04" ShowInColumn="DecSal04" SummaryType="Sum" Tag="DecSal04" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="DecSal05" ShowInColumn="DecSal05" SummaryType="Sum" Tag="DecSal05" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="DecSal06" ShowInColumn="DecSal06" SummaryType="Sum" Tag="DecSal06" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="DecSal07" ShowInColumn="DecSal07" SummaryType="Sum" Tag="DecSal07" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="DecSal08" ShowInColumn="DecSal08" SummaryType="Sum" Tag="DecSal08" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="DecSal09" ShowInColumn="DecSal09" SummaryType="Sum" Tag="DecSal09" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="DecSal10" ShowInColumn="DecSal10" SummaryType="Sum" Tag="DecSal10" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="DecSal11" ShowInColumn="DecSal11" SummaryType="Sum" Tag="DecSal11" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="DecSal12" ShowInColumn="DecSal12" SummaryType="Sum" Tag="DecSal12" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="DecSal13" ShowInColumn="DecSal13" SummaryType="Sum" Tag="DecSal13" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="DecSal14" ShowInColumn="DecSal14" SummaryType="Sum" Tag="DecSal14" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="DecSal15" ShowInColumn="DecSal15" SummaryType="Sum" Tag="DecSal15" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="DecSal16" ShowInColumn="DecSal16" SummaryType="Sum" Tag="DecSal16" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="DecSal17" ShowInColumn="DecSal17" SummaryType="Sum" Tag="DecSal17" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="DecSal18" ShowInColumn="DecSal18" SummaryType="Sum" Tag="DecSal18" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="DecSal19" ShowInColumn="DecSal19" SummaryType="Sum" Tag="DecSal19" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="DecSal20" ShowInColumn="DecSal20" SummaryType="Sum" Tag="DecSal20" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="DecTotalSal" ShowInColumn="DecTotalSal" SummaryType="Sum" Tag="DecTotalSal" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="DecImporteTotal" ShowInColumn="DecImporteTotal" SummaryType="Sum" Tag="DecImporteTotal" DisplayFormat="{0}" />
                                                                                    </TotalSummary>
                                                                                    <GroupSummary>
                                                                                        <dx:ASPxSummaryItem FieldName="DecIng01" ShowInGroupFooterColumn="DecIng01" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="DecIng02" ShowInGroupFooterColumn="DecIng02" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="DecIng03" ShowInGroupFooterColumn="DecIng03" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="DecIng04" ShowInGroupFooterColumn="DecIng04" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="DecIng05" ShowInGroupFooterColumn="DecIng05" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="DecIng06" ShowInGroupFooterColumn="DecIng06" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="DecIng07" ShowInGroupFooterColumn="DecIng07" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="DecIng08" ShowInGroupFooterColumn="DecIng08" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="DecIng09" ShowInGroupFooterColumn="DecIng09" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="DecIng10" ShowInGroupFooterColumn="DecIng10" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="DecIng11" ShowInGroupFooterColumn="DecIng11" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="DecIng12" ShowInGroupFooterColumn="DecIng12" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="DecIng13" ShowInGroupFooterColumn="DecIng13" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="DecIng14" ShowInGroupFooterColumn="DecIng14" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="DecIng15" ShowInGroupFooterColumn="DecIng15" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="DecIng16" ShowInGroupFooterColumn="DecIng16" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="DecIng17" ShowInGroupFooterColumn="DecIng17" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="DecIng18" ShowInGroupFooterColumn="DecIng18" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="DecIng19" ShowInGroupFooterColumn="DecIng19" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="DecIng20" ShowInGroupFooterColumn="DecIng20" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="DecTotalIng" ShowInGroupFooterColumn="DecTotalIng" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="DecSal01" ShowInGroupFooterColumn="DecSal01" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="DecSal02" ShowInGroupFooterColumn="DecSal02" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="DecSal03" ShowInGroupFooterColumn="DecSal03" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="DecSal04" ShowInGroupFooterColumn="DecSal04" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="DecSal05" ShowInGroupFooterColumn="DecSal05" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="DecSal06" ShowInGroupFooterColumn="DecSal06" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="DecSal07" ShowInGroupFooterColumn="DecSal07" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="DecSal08" ShowInGroupFooterColumn="DecSal08" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="DecSal09" ShowInGroupFooterColumn="DecSal09" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="DecSal10" ShowInGroupFooterColumn="DecSal1" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="DecSal11" ShowInGroupFooterColumn="DecSal11" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="DecSal12" ShowInGroupFooterColumn="DecSal12" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="DecSal13" ShowInGroupFooterColumn="DecSal13" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="DecSal14" ShowInGroupFooterColumn="DecSal14" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="DecSal15" ShowInGroupFooterColumn="DecSal15" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="DecSal16" ShowInGroupFooterColumn="DecSal16" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="DecSal17" ShowInGroupFooterColumn="DecSal17" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="DecSal18" ShowInGroupFooterColumn="DecSal18" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="DecSal19" ShowInGroupFooterColumn="DecSal19" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="DecSal20" ShowInGroupFooterColumn="DecSal20" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="DecTotalSal" ShowInGroupFooterColumn="DecTotalSal" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                        <dx:ASPxSummaryItem FieldName="DecImporteTotal" ShowInGroupFooterColumn="DecImporteTotal" SummaryType="Sum"  DisplayFormat="{0}"/>
                                                                                    </GroupSummary>
                                                                                </dx:ASPxGridView>

                                                                            
                                                                                
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="4" style="width: 100%">
                                                                                <div id="contGrafPrinc" aria-expanded="true" style="min-width: 310px; height: 400px; margin: 0 auto">
                                                                                </div>
                                                                                
                                                                                <script type="text/javascript">

                                                                                    function grafficardatosprinc(s, e) {

                                                                                        var js_title = s.cp_title;
                                                                                        var js_subtitle = s.cp_subtitle;
                                                                                        var js_xAxis = s.cp_xAxis;
                                                                                        var js_Series = s.cp_series;

                                                                                        Highcharts.chart('contGrafPrinc', {

                                                                                            chart: {
                                                                                                type: 'column'
                                                                                            },

                                                                                            title: {
                                                                                                text: js_title
                                                                                            },
                                                                                            subtitle: {
                                                                                                text: js_subtitle
                                                                                            },
                                                                                            xAxis: {
                                                                                                categories: js_xAxis
                                                                                            },

                                                                                            yAxis: {
                                                                                                allowDecimals: false,
                                                                                                min: 0,
                                                                                                title: {
                                                                                                    text: 'Cantidad'
                                                                                                },
                                                                                                stackLabels: {
                                                                                                    enabled: true,
                                                                                                    style: {
                                                                                                        fontWeight: 'bold',
                                                                                                        color: (Highcharts.theme && Highcharts.theme.textColor) || 'gray'
                                                                                                    }
                                                                                                }
                                                                                            },

                                                                                            tooltip: {
                                                                                                formatter: function () {
                                                                                                    return '<b>' + this.x + '</b><br/>' +
                                                                                                        this.series.name + ': ' + this.y + '<br/>' +
                                                                                                        'Total: ' + this.point.stackTotal;
                                                                                                }
                                                                                            },

                                                                                            plotOptions: {
                                                                                                column: {
                                                                                                    stacking: 'normal'
                                                                                                }
                                                                                            },

                                                                                            series: js_Series
                                                                                        });
                                                                                    }
                                                                                </script>

                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </dx:ContentControl>
                                                            </ContentCollection>
                                                        </dx:TabPage>
                                                       
                                                    </TabPages>
                                                </dx:ASPxPageControl>
                                                
                                            </td>
                                    </tr>
                                        

                                    

                                    <tr>
                                        <td colspan="7">
                                            <dx:ASPxPanel ID="pnlHerramienta" runat="server" Width="100%" ClientInstanceName="pnlHerramienta" ClientVisible="False">
                                                <PanelCollection>
                                                    <dx:PanelContent ID="PanelContent6" runat="server">
                                                        <table style="width: 100%">
                                                            <tr>
                                                                <td colspan="4">
                                                                    <dx:ASPxLabel ID="ASPxLabel3" runat="server" Font-Bold="True" Font-Names="Rockwell" ForeColor="#999999" Text="HERRAMINTAS ADICIONALES" Width="100%">
                                                                        <BorderBottom BorderStyle="Solid" />
                                                                    </dx:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 20%">
                                                                    
                                                                    &nbsp;</td>
                                                                <td style="width: 20%">
                                                                    
                                                                    &nbsp;</td>
                                                                <td style="width: 20%">
                                                                    
                                                                    &nbsp;</td>
                                                                <td style="width: 40%">
                                                                    &nbsp;</td>
                                                            </tr>
                                                        </table>
                                                    </dx:PanelContent>
                                                </PanelCollection>
                                            </dx:ASPxPanel>
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
                                        <td class="auto-style14">
                                            &nbsp;</td>
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

        </td>
    </tr>
</table>

</div>




    



















