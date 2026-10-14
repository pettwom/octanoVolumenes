<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtrVisorAeroBsisa.ascx.cs" Inherits="AnhHydroOctanoPresentacion.Controles.CtrVisorAeroBsisa" %>
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
                            <dx:ASPxLabel runat="server" ForeColor="#367B2F" ID="lblTitulo" Font-Bold="True" Font-Names="Rockwell" Font-Size="Larger" Text="VISOR AERO B-SISA"></dx:ASPxLabel>
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
                        
                        <dx:ASPxCallbackPanel ID="pnlLateral" runat="server" Width="200px" ClientInstanceName="pnlLateral">
                            <PanelCollection>
                                <dx:PanelContent runat="server">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td>
                                                <dx:ASPxButton ID="btnRegistroNuevo" runat="server" AutoPostBack="False" Font-Bold="True" ForeColor="Maroon" Text="Registrar Aeronave Extranjera" Theme="Metropolis" Width="100%">
                                                    <ClientSideEvents Click="function(s, e) {
//pucRegistroExt.PerformCallback();
pucRegistroExt.Show();
}" />
                                                    <Image IconID="actions_addfile_16x16">
                                                    </Image>
                                                </dx:ASPxButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dx:ASPxLabel ID="ASPxLabel44" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="Smaller" ForeColor="#003300" Text="CONSULTA :">
                                                </dx:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dx:ASPxLabel ID="ASPxLabel47" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="Small" ForeColor="#003300" Text="Cite Infraestructura:">
                                                </dx:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dx:ASPxTextBox ID="txtCite" runat="server" Width="100%" Theme="Metropolis" Text="ANH-IFR 0000971/2017" ClientInstanceName="txtCite">
                                                </dx:ASPxTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dx:ASPxLabel ID="ASPxLabel48" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="Small" ForeColor="#003300" Text="Tipo de Consulta :">
                                                </dx:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dx:ASPxRadioButtonList ID="rbtTipo" runat="server" EnableTheming="True" SelectedIndex="0" Theme="Metropolis" ValueType="System.Int32" Width="100%">
                                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
txtIdentificador.SetText('');
txtSubIdentificador.SetText('');
txtNoVuelo.SetText('');
cmbOrigen.SetText('');
cmbDestino.SetText('');
cmbAlterno.SetText('');
}" />
                                                    <Items>
                                                        <dx:ListEditItem Selected="True" Text="Matricula" Value="2" />
                                                        <dx:ListEditItem Text="Tarjeta (Codigo de Barra)" Value="1" />
                                                    </Items>
                                                </dx:ASPxRadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dx:ASPxLabel ID="ASPxLabel49" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="Small" ForeColor="#003300" Text="Identificador :">
                                                </dx:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dx:ASPxTextBox ID="txtIdentificador" runat="server" Width="100%" Theme="Metropolis" ClientInstanceName="txtIdentificador">
                                                </dx:ASPxTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dx:ASPxLabel ID="ASPxLabel50" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="Small" ForeColor="#003300" Text="Sub Identificador :">
                                                </dx:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dx:ASPxTextBox ID="txtSubIdentificador" runat="server" Theme="Metropolis" Width="100%" ClientInstanceName="txtSubIdentificador">
                                                </dx:ASPxTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dx:ASPxLabel ID="ASPxLabel52" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="Small" ForeColor="#003300" Text="Nro. de Vuelo :">
                                                </dx:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dx:ASPxTextBox ID="txtNoVuelo" runat="server" Theme="Metropolis" Width="100%" ClientInstanceName="txtNoVuelo">
                                                </dx:ASPxTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dx:ASPxLabel ID="ASPxLabel53" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="Small" ForeColor="#003300" Text="Aeropuerto Origen :">
                                                </dx:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dx:ASPxComboBox ID="cmbOrigen" runat="server" EnableTheming="True" TextFormatString="{0}" Theme="Metropolis" ValueField="ID_AEROPUERTO" Width="100%" ClientInstanceName="cmbOrigen">
                                                    <Columns>
                                                        <dx:ListBoxColumn Caption="ID_AEROPUERTO" FieldName="ID_AEROPUERTO" />
                                                        <dx:ListBoxColumn Caption="NOMBRE" FieldName="NOMBRE" />
                                                        <dx:ListBoxColumn Caption="PAIS" FieldName="PAIS" />
                                                        <dx:ListBoxColumn Caption="CIUDAD" FieldName="CIUDAD" />
                                                    </Columns>
                                                    <ClearButton Visibility="Auto">
                                                    </ClearButton>
                                                </dx:ASPxComboBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dx:ASPxLabel ID="ASPxLabel54" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="Small" ForeColor="#003300" Text="Aeropuerto Destino :">
                                                </dx:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dx:ASPxComboBox ID="cmbDestino" runat="server" EnableTheming="True" TextFormatString="{0}" Theme="Metropolis" ValueField="ID_AEROPUERTO" Width="100%" ClientInstanceName="cmbDestino">
                                                    <Columns>
                                                        <dx:ListBoxColumn Caption="ID_AEROPUERTO" FieldName="ID_AEROPUERTO" />
                                                        <dx:ListBoxColumn Caption="NOMBRE" FieldName="NOMBRE" />
                                                        <dx:ListBoxColumn Caption="PAIS" FieldName="PAIS" />
                                                        <dx:ListBoxColumn Caption="CIUDAD" FieldName="CIUDAD" />
                                                    </Columns>
                                                    <ClearButton Visibility="Auto">
                                                    </ClearButton>
                                                </dx:ASPxComboBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dx:ASPxLabel ID="ASPxLabel66" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="Small" ForeColor="#003300" Text="Aeropuerto Alterno :">
                                                </dx:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dx:ASPxComboBox ID="cmbAlterno" runat="server" EnableTheming="True" TextFormatString="{0}" Theme="Metropolis" ValueField="ID_AEROPUERTO" Width="100%" ClientInstanceName="cmbAlterno">
                                                    <Columns>
                                                        <dx:ListBoxColumn Caption="ID_AEROPUERTO" FieldName="ID_AEROPUERTO" />
                                                        <dx:ListBoxColumn Caption="NOMBRE" FieldName="NOMBRE" />
                                                        <dx:ListBoxColumn Caption="PAIS" FieldName="PAIS" />
                                                        <dx:ListBoxColumn Caption="CIUDAD" FieldName="CIUDAD" />
                                                    </Columns>
                                                    <ClearButton Visibility="Auto">
                                                    </ClearButton>
                                                </dx:ASPxComboBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td style="padding-top: 15px">
                                    
                                                <dx:ASPxButton ID="btnActualiza" runat="server" AutoPostBack="False" Font-Bold="True" ForeColor="Maroon" Text="obtenerConsumidor" Theme="Metropolis" Width="100%">
                                                    <ClientSideEvents Click="function(s, e) {
pnlDatos.PerformCallback();
}" />
                                                    <Image IconID="businessobjects_bocontact2_16x16">
                                                    </Image>
                                                </dx:ASPxButton>
                                    
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dx:ASPxButton ID="btnRegistro" runat="server" AutoPostBack="False" Font-Bold="True" ForeColor="Maroon" Text="Registro de Movimientos" Theme="Metropolis" Width="100%" Visible="False">
                                                    <ClientSideEvents Click="function(s, e) {
//pucDatos.PerformCallback();
pucDatos.Show();
}" />
                                                    <Image IconID="businessobjects_bocontact2_16x16">
                                                    </Image>
                                                </dx:ASPxButton>
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
                                                <asp:Label ID="lblSubTitulo" runat="server" Font-Bold="True" Font-Names="Rockwell" Font-Size="Medium" ForeColor="#367B2F" Text="RESULTADO - OBTENER CONSUMIDOR"></asp:Label>
                                                </legend></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td></td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td style="text-align: right;">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <dx:ASPxImageGallery ID="imgFotos" runat="server" ClientInstanceName="imgFotos" EnableViewState="False" Height="400px" ImageContentBytesField="IMAGEN" OnCustomCallback="imgFotos_CustomCallback" Theme="Metropolis" Width="250px" ThumbnailHeight="350px" ThumbnailImageSizeMode="FitProportional">
                                                    <ItemTextTemplate>
                                                        <div class="item">
                                                            <div class="campoValor">
                                                                <%# Container.EvalDataItem("TIPO_IMAGEN") %>
                                                            </div>
                                                        </div>
                                                    </ItemTextTemplate>
                                                    <FullscreenViewerItemTextTemplate>
                                                        <div class="details">
                                                            <div class="campoNombre">
                                                                <%# Container.EvalDataItem("TIPO_IMAGEN") %>
                                                            </div>
                                                        </div>
                                                    </FullscreenViewerItemTextTemplate>
                                                    <SettingsFolder ImageCacheFolder="~/Thumb/" />
                                                    <SettingsFlowLayout ItemsPerPage="1" />
                                                    <SettingsTableLayout ColumnCount="1" RowsPerPage="1" />
                                                    <PagerSettings NumericButtonCount="1" Position="Bottom">
                                                        <PageSizeItemSettings Visible="False">
                                                        </PageSizeItemSettings>
                                                    </PagerSettings>
                                                </dx:ASPxImageGallery>
                                            </td>
                                            <td colspan="5" style="width: 100%">
                                                
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td>
                                                            
                                                            <dx:ASPxLabel ID="ASPxLabel55" runat="server" ClientVisible="False" Font-Bold="True" Font-Names="Verdana" Font-Size="Smaller" ForeColor="#003300" Text="ESTADO :">
                                                            </dx:ASPxLabel>
                                                            
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            
                                                            <dx:ASPxLabel ID="lblEstado" runat="server" ClientVisible="False" Font-Bold="True" Font-Size="Medium" Width="100%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-top: 5px">
                                                            
                                                            <dx:ASPxLabel ID="ASPxLabel56" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="Smaller" ForeColor="#003300" Text="LISTA DE MENSAJES">
                                                            </dx:ASPxLabel>
                                                            
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-left: 10px">
                                                            
                                                            <dx:ASPxLabel ID="lblMensaje" runat="server" Font-Bold="True" Font-Size="Medium" Width="100%">
                                                            </dx:ASPxLabel>
                                                            
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-top: 5px">
                                                            <dx:ASPxLabel ID="ASPxLabel64" runat="server" Width="100%">
                                                                <BorderTop BorderColor="#367B2F" BorderStyle="Solid" />
                                                            </dx:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-top: 5px">
                                                            
                                                            <dx:ASPxLabel ID="ASPxLabel65" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="Smaller" ForeColor="#003300" Text="ID TRANSACCION">
                                                            </dx:ASPxLabel>
                                                            
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-left: 10px">
                                                            <dx:ASPxLabel ID="lblIdTransaccion" runat="server" Font-Bold="True" Font-Size="Medium" Width="100%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-top: 5px">
                                                            <dx:ASPxLabel ID="ASPxLabel57" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="Smaller" ForeColor="#003300" Text="EXPLOTADOR">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-left: 10px">
                                                            
                                                            <dx:ASPxLabel ID="lblExplotador" runat="server" Font-Bold="True" Font-Size="Medium" Width="100%">
                                                            </dx:ASPxLabel>
                                                            
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-top: 5px">
                                                            
                                                            <dx:ASPxLabel ID="ASPxLabel58" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="Smaller" ForeColor="#003300" Text="MODELO">
                                                            </dx:ASPxLabel>
                                                            
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-left: 10px">
                                                            <dx:ASPxLabel ID="lblModelo" runat="server" Font-Bold="True" Font-Size="Medium" Width="100%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-top: 5px">
                                                            
                                                            <dx:ASPxLabel ID="ASPxLabel59" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="Smaller" ForeColor="#003300" Text="MARCA">
                                                            </dx:ASPxLabel>
                                                            
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-left: 10px">
                                                            <dx:ASPxLabel ID="lblMarca" runat="server" Font-Bold="True" Font-Size="Medium" Width="100%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-top: 5px">
                                                            <dx:ASPxLabel ID="lblSep1" runat="server" Width="100%">
                                                                <BorderTop BorderColor="#367B2F" BorderStyle="Solid" />
                                                            </dx:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-top: 5px">
                                                            
                                                            <dx:ASPxLabel ID="ASPxLabel67" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="Smaller" ForeColor="#003300" Text="PRODUCTO">
                                                            </dx:ASPxLabel>
                                                            
                                                        </td>
                                                    </tr>
                                                    
                                                    <tr>
                                                        <td style="padding-top: 5px">
                                                            <dx:ASPxComboBox ID="cmbProducto" runat="server" TextField="strProducto" Theme="Metropolis" ValueField="idProducto" ValueType="System.Int32" Width="50%" ClientInstanceName="cmbProducto" Font-Bold="True" TextFormatString="{0}; {1}; {2}">
                                                                <ClientSideEvents SelectedIndexChanged="function(s, e) {

if(cmbProducto.GetValue()==129){
lblPrecioNac.SetValue('2.77');
lblPrecioInt.SetValue('6.65');
}
else{
lblPrecioNac.SetValue('4.57');
lblPrecioInt.SetValue('0');
}


}" />
                                                                <Columns>
                                                                    <dx:ListBoxColumn Caption="idProducto" FieldName="idProducto" Visible="False" />
                                                                    <dx:ListBoxColumn Caption="strProducto" FieldName="strProducto" />
                                                                    <dx:ListBoxColumn Caption="idUnidad" FieldName="idUnidad" Visible="False" />
                                                                    <dx:ListBoxColumn Caption="precioNac" FieldName="precioNac" />
                                                                    <dx:ListBoxColumn Caption="precioInt" FieldName="precioInt" />
                                                                </Columns>
                                                                <ClearButton Visibility="Auto">
                                                                </ClearButton>
                                                            </dx:ASPxComboBox>
                                                        </td>
                                                    </tr>
                                                    
                                                    <tr>
                                                        <td style="padding-top: 5px">
                                                            <dx:ASPxLabel ID="ASPxLabel60" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="Smaller" ForeColor="#003300" Text="PRECIO NACIONAL (BS.)">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                    
                                                    <tr>
                                                        <td style="padding-left: 10px">
                                                            <dx:ASPxLabel ID="lblPrecioNac" runat="server" Font-Bold="True" Font-Size="Medium" Width="100%" ClientInstanceName="lblPrecioNac">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-top: 5px">
                                                            
                                                            <dx:ASPxLabel ID="ASPxLabel61" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="Smaller" ForeColor="#003300" Text="PRECIO INTERNACIONAL (BS.)">
                                                            </dx:ASPxLabel>
                                                            
                                                        </td>
                                                    </tr>
                                                     <tr>
                                                        <td style="padding-left: 10px">
                                                            
                                                            <dx:ASPxLabel ID="lblPrecioInt" runat="server" Font-Bold="True" Font-Size="Medium" Width="100%" ClientInstanceName="lblPrecioInt">
                                                            </dx:ASPxLabel>
                                                            
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-top: 5px">
                                                            <dx:ASPxLabel ID="lblser2" runat="server" Width="100%">
                                                                <BorderTop BorderColor="#367B2F" BorderStyle="Solid" />
                                                            </dx:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                     <tr>
                                                        <td style="padding-top: 5px; padding-bottom: 5px">
                                                            
                                                            <dx:ASPxLabel ID="ASPxLabel62" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="Smaller" ForeColor="#003300" Text="CONDICIONES PARA EL CONSUMO">
                                                            </dx:ASPxLabel>
                                                            
                                                        </td>
                                                    </tr>
                                                     <tr>
                                                        <td>
                                                            
                                                            <dx:ASPxLabel ID="ASPxLabel63" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="Smaller" ForeColor="#003300" Text="VOLUMEN REFERENCIAL (LTS.)">
                                                            </dx:ASPxLabel>
                                                            
                                                        </td>
                                                    </tr>
                                                     <tr>
                                                        <td style="padding-left: 10px">
                                                            
                                                            <dx:ASPxLabel ID="lblVolumenMax" runat="server" Font-Bold="True" Font-Size="Medium" Width="100%">
                                                            </dx:ASPxLabel>
                                                            
                                                        </td>
                                                    </tr>
                                                     <tr>
                                                        <td>
                                                            
                                                            <dx:ASPxRadioButtonList ID="rbtProducto" runat="server" EnableTheming="True" ReadOnly="True" SelectedIndex="0" Theme="Metropolis" ValueType="System.Int32" Visible="False" Width="100%">
                                                                <Items>
                                                                    <dx:ListEditItem Selected="True" Text="GASOLINA DE AVIACION GRADO 100" Value="128" />
                                                                    <dx:ListEditItem Text="JET FUEL A1" Value="129" />
                                                                </Items>
                                                            </dx:ASPxRadioButtonList>
                                                            
                                                        </td>
                                                    </tr>
                                                     <tr>
                                                        <td>
                                                            
                                                        </td>
                                                    </tr>
                                                    

                                                </table>
                                                

                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="7">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;</td>
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



<dx:ASPxPopupControl ID="pucRegistroExt" runat="server" AllowDragging="True" ClientInstanceName="pucRegistroExt" EnableTheming="True" HeaderText="Registro de Aeronaves Extranjeras" Height="93px" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Theme="Metropolis" Width="600px" OnWindowCallback="pucRegistroExt_WindowCallback">
    <HeaderImage IconID="actions_addfile_32x32">
    </HeaderImage>
    <HeaderStyle>
    <BorderBottom BorderStyle="Solid" />
    </HeaderStyle>
    <ContentCollection>
        <dx:PopupControlContentControl runat="server">
            <table style="width: 100%">
                <tr>
                    <td style="width: 30%">
                        <dx:ASPxLabel ID="ASPxLabel339" runat="server" Font-Bold="True" ForeColor="#333333" Text="Fechas :">
                        </dx:ASPxLabel>
                    </td>
                    <td style="width: 70%">&nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <dx:ASPxLabel ID="lblPrecioEf0" runat="server" Font-Bold="True" ForeColor="#666666" Text="Nombre/Razon Social :">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="txtRegPropietario" runat="server" ClientInstanceName="txtRegPropietario" MaxLength="100" Width="90%">
                            <ValidationSettings>
                                <RequiredField ErrorText="Campo Obligatorio" IsRequired="True" />
                            </ValidationSettings>
                            <ReadOnlyStyle BackColor="#DEE8DB">
                            </ReadOnlyStyle>
                            <FocusedStyle BackColor="#FFFFCC">
                            </FocusedStyle>
                            <DisabledStyle BackColor="#DEE8DB">
                            </DisabledStyle>
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <dx:ASPxLabel ID="lblPrecioEf1" runat="server" Font-Bold="True" ForeColor="#666666" Text="No. Doc. Identificación :">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="txtRegNroIdentificacion" runat="server" ClientInstanceName="txtRegNroIdentificacion" MaxLength="100">
                            <ValidationSettings>
                                <RequiredField ErrorText="Campo Obligatorio" IsRequired="True" />
                            </ValidationSettings>
                            <ReadOnlyStyle BackColor="#DEE8DB">
                            </ReadOnlyStyle>
                            <FocusedStyle BackColor="#FFFFCC">
                            </FocusedStyle>
                            <DisabledStyle BackColor="#DEE8DB">
                            </DisabledStyle>
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        
                        <dx:ASPxLabel ID="lblPrecioEf2" runat="server" Font-Bold="True" ForeColor="#666666" Text="País :">
                        </dx:ASPxLabel>
                        
                    </td>
                    <td>
                        
                        <dx:ASPxComboBox ID="cmbRegPais" runat="server" TextField="DESCRIPCION" TextFormatString="{0}" Theme="Metropolis" ValueField="ID_UBICACION_GEOGRAFICA">
                            <Columns>
                                <dx:ListBoxColumn Caption="DESCRIPCION" FieldName="DESCRIPCION" Name="DESCRIPCION" />
                                <dx:ListBoxColumn Caption="ID_UBICACION_GEOGRAFICA" FieldName="ID_UBICACION_GEOGRAFICA" Name="ID_UBICACION_GEOGRAFICA" Width="0px" />
                            </Columns>
                            <ClearButton Visibility="Auto">
                            </ClearButton>
                            <ValidationSettings>
                                <RequiredField IsRequired="True" />
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
                    <td style="text-align: right">
                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Font-Bold="True" ForeColor="#666666" Text="Matricula :">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        
                        <dx:ASPxTextBox ID="txtRegMatricula" ClientInstanceName="txtRegMatricula" runat="server" MaxLength="100">
                            <ValidationSettings>
                                <RequiredField ErrorText="Campo Obligatorio" IsRequired="True" />
                            </ValidationSettings>
                            <ReadOnlyStyle BackColor="#DEE8DB">
                            </ReadOnlyStyle>
                            <FocusedStyle BackColor="#FFFFCC">
                            </FocusedStyle>
                            <DisabledStyle BackColor="#DEE8DB">
                            </DisabledStyle>
                        </dx:ASPxTextBox>
                        
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <dx:ASPxLabel ID="ASPxLabel2" runat="server" Font-Bold="True" ForeColor="#666666" Text="Clase :">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        
                        <dx:ASPxComboBox ID="cmbRegClase" runat="server" TextField="NOMBRE" TextFormatString="{0}" Theme="Metropolis" ValueField="ID" Width="90%">
                            <Columns>
                                <dx:ListBoxColumn Caption="NOMBRE" FieldName="NOMBRE" Name="NOMBRE" />
                                <dx:ListBoxColumn Caption="ID" FieldName="ID" Name="ID" Width="0px" />
                            </Columns>
                            <ClearButton Visibility="Auto">
                            </ClearButton>
                            <ValidationSettings>
                                <RequiredField IsRequired="True" />
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
                    <td style="text-align: right">
                        <dx:ASPxLabel ID="ASPxLabel3" runat="server" Font-Bold="True" ForeColor="#666666" Text="Marca :">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        
                        <dx:ASPxComboBox ID="cmbRegMarca" runat="server" TextField="NOMBRE" TextFormatString="{0}" Theme="Metropolis" ValueField="ID" Width="90%">
                            <Columns>
                                <dx:ListBoxColumn Caption="NOMBRE" FieldName="NOMBRE" Name="NOMBRE" />
                                <dx:ListBoxColumn Caption="ID" FieldName="ID" Name="ID" Width="0px" />
                            </Columns>
                            <ClearButton Visibility="Auto">
                            </ClearButton>
                            <ValidationSettings>
                                <RequiredField IsRequired="True" />
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
                    <td style="text-align: right">
                        <dx:ASPxLabel ID="ASPxLabel4" runat="server" Font-Bold="True" ForeColor="#666666" Text="Servicio :">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        
                        <dx:ASPxComboBox ID="cmbRegServicio" runat="server" TextFormatString="{0}" Theme="Metropolis" SelectedIndex="0" ValueType="System.Int32" Width="90%">
                            <Items>
                                <dx:ListEditItem Selected="True" Text="PARTICULAR" Value="1" />
                                <dx:ListEditItem Text="PUBLICO" Value="2" />
                                <dx:ListEditItem Text="OFICIAL" Value="3" />
                                <dx:ListEditItem Text="DIPLOMATICO" Value="4" />
                                <dx:ListEditItem Text="SERVICIO NO ESPECIFICADO" Value="-99999" />
                            </Items>
                            <ClearButton Visibility="Auto">
                            </ClearButton>
                            <ValidationSettings>
                                <RequiredField IsRequired="True" />
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
                    <td style="text-align: right">
                        <dx:ASPxLabel ID="ASPxLabel5" runat="server" Font-Bold="True" ForeColor="#666666" Text="Modelo :">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        
                        <dx:ASPxTextBox ID="txtRegModelo" ClientInstanceName="txtRegModelo" runat="server" MaxLength="100" Width="70%">
                            <ValidationSettings>
                                <RequiredField ErrorText="Campo Obligatorio" IsRequired="True" />
                            </ValidationSettings>
                            <ReadOnlyStyle BackColor="#DEE8DB">
                            </ReadOnlyStyle>
                            <FocusedStyle BackColor="#FFFFCC">
                            </FocusedStyle>
                            <DisabledStyle BackColor="#DEE8DB">
                            </DisabledStyle>
                        </dx:ASPxTextBox>
                        
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <dx:ASPxLabel ID="ASPxLabel6" runat="server" Font-Bold="True" ForeColor="#666666" Text="Serie :">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        
                        <dx:ASPxTextBox ID="txtRegSerie" runat="server" ClientInstanceName="txtRegSerie" MaxLength="100" Width="70%">
                            <ValidationSettings>
                                <RequiredField ErrorText="Campo Obligatorio" IsRequired="True" />
                            </ValidationSettings>
                            <ReadOnlyStyle BackColor="#DEE8DB">
                            </ReadOnlyStyle>
                            <FocusedStyle BackColor="#FFFFCC">
                            </FocusedStyle>
                            <DisabledStyle BackColor="#DEE8DB">
                            </DisabledStyle>
                        </dx:ASPxTextBox>
                        
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <dx:ASPxLabel ID="ASPxLabel7" runat="server" Font-Bold="True" ForeColor="#666666" Text="Año Fabricación :">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        
                        <dx:ASPxSpinEdit ID="spnRegGesFab" runat="server" MaxValue="3000" Number="2000" NumberType="Integer">
                            <ClearButton Visibility="Auto">
                            </ClearButton>
                        </dx:ASPxSpinEdit>
                        
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <dx:ASPxLabel ID="ASPxLabel8" runat="server" Font-Bold="True" ForeColor="#666666" Text="Producto :">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        
                        <dx:ASPxComboBox ID="cmbRegProducto" runat="server" TextFormatString="{0}" Theme="Metropolis" Width="90%" SelectedIndex="0">
                            <Items>
                                <dx:ListEditItem Selected="True" Text="GASOLINA DE AVIACION GRADO 100" Value="128" />
                                <dx:ListEditItem Text="JET FUEL A1" Value="129" />
                            </Items>
                            <ClearButton Visibility="Auto">
                            </ClearButton>
                            <ValidationSettings>
                                <RequiredField IsRequired="True" />
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
                    <td colspan="2">
                        &nbsp;</tr>
                <tr>
                    <td colspan="2">
                        <dx:ASPxButton ID="btnGrabar" runat="server" Font-Bold="True" ForeColor="Maroon" Text="Grabar Registro" Theme="Metropolis" Width="100%" OnClick="btnGrabar_Click">
                            <Image IconID="actions_apply_16x16">
                            </Image>
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
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
                                    <td style="width: 20%">
                                        <dx:ASPxButton ID="btnProcesa" runat="server" AutoPostBack="False" OnClick="btnProcesa_Click" Text="ASPxButton">
                                        </dx:ASPxButton>
                                    </td>
                                    <td style="width: 20%">
                                        
                                        <dx:ASPxButton ID="btnFormato" runat="server" AutoPostBack="False" OnClick="btnFormato_Click" Text="ASPxButton">
                                        </dx:ASPxButton>
                                        
                                    </td>
                                    <td style="width: 60%">
                                        <dx:ASPxSpinEdit ID="ASPxSpinEdit1" runat="server" MaxValue="100" Number="1">
                                            <ClearButton Visibility="Auto">
                                            </ClearButton>
                                        </dx:ASPxSpinEdit>
                                    </td>
                                </tr>
                                <tr style="width: 100%">
                                    <td style="width: 20%">
                                        <dx:ASPxRadioButtonList ID="rblTipoOperacion" runat="server" SelectedIndex="0" ValueType="System.Int32">
                                            <Items>
                                                <dx:ListEditItem Selected="True" Text="ING X RECEPCION" Value="1" />
                                                <dx:ListEditItem Text="SAL. X VENTA" Value="2" />
                                            </Items>
                                        </dx:ASPxRadioButtonList>
                                    </td>
                                    <td style="width: 20%">&nbsp;</td>
                                    <td style="width: 60%">
                                        <dx:ASPxButton ID="btnEnvia" runat="server" AutoPostBack="False" OnClick="btnEnvia_Click" Text="ASPxButton">
                                        </dx:ASPxButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <dx:ASPxMemo ID="memCadena" runat="server" Height="400px" Width="50%">
                                        </dx:ASPxMemo>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                        
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <dx:ASPxPanel ID="pnlResumen" runat="server" Width="100%" ClientInstanceName="pnlResumen">
                                            <PanelCollection>
                                                <dx:PanelContent runat="server">
                                                    <table style="width: 100%">
                                                        <tr>
                                                            <td>
                                                
                                                                <dx:ASPxGridView ID="dgvTransaccionError" runat="server" AutoGenerateColumns="False" ClientInstanceName="dgvTransaccionError" EnableTheming="True" Font-Size="Smaller" KeyFieldName="ID_VOLUMEN_REFERENCIAL" Theme="Default" Width="100%">
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
                                                                                        <dx:ASPxLabel ID="ASPxLabel68" runat="server" Text="No. Factura Ext. :" Width="120px">
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
                                                                                        <dx:ASPxLabel ID="ASPxLabel69" runat="server" Text="No. Doc. Referencia :" Width="130px">
                                                                                        </dx:ASPxLabel>
                                                                                    </td>
                                                                                    <td class="auto-style16">
                                                                                        <dx:ASPxTextBox ID="txtNoDocRef0" runat="server" Font-Size="Smaller" MaxLength="15" Width="170px">
                                                                                            <MaskSettings Mask="&lt;0..999999999999999&gt;" />
                                                                                        </dx:ASPxTextBox>
                                                                                    </td>
                                                                                    <td>&nbsp;</td>
                                                                                    <td class="auto-style19">
                                                                                        <dx:ASPxLabel ID="ASPxLabel70" runat="server" Text="Tipo Factura :" Visible="False">
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
                                                                                        <dx:ASPxLabel ID="ASPxLabel71" runat="server" Text="Estado :">
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
                                                                                        <dx:ASPxLabel ID="ASPxLabel72" runat="server" Text="Producto :" Width="100px">
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
                                                                                        <dx:ASPxLabel ID="ASPxLabel73" runat="server" Text="Precio :">
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
                                                                                        <dx:ASPxLabel ID="ASPxLabel74" runat="server" Text="Autorización de Venta :" Width="150px">
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
                                                                                        <dx:ASPxLabel ID="ASPxLabel75" runat="server" Text="No. Hoja Ruta :">
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
                                                                        <dx:GridViewDataTextColumn Caption="MNS_ERROR" FieldName="MNS_ERROR" ShowInCustomizationForm="True" ToolTip="Número de Referencia" VisibleIndex="10">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn Caption="NRO." FieldName="ID_VOLUMEN_REFERENCIAL" ShowInCustomizationForm="True" ToolTip="Funciones" VisibleIndex="1">
                                                                            <Settings AutoFilterCondition="Equals" />
                                                                        </dx:GridViewDataTextColumn>
                                                                    </Columns>
                                                                </dx:ASPxGridView>
                                                            </td>
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


    





    



    




























































