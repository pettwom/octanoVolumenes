<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio/ANH.Master" AutoEventWireup="true" CodeBehind="WFDescarga.aspx.cs" Inherits="AnhHydroOctanoPresentacion.Sitio.Movimientos.WFDescarga" %>

<%@ Register assembly="DevExpress.XtraReports.v15.1.Web, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraReports.Web" tagprefix="dx" %>

<%@ Register assembly="DevExpress.Web.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <dx1:ASPxGridView runat="server" ClientInstanceName="dgvTransaccionError" KeyFieldName="LngId" AutoGenerateColumns="False" Theme="Default" Width="100%" EnableTheming="True" Font-Size="Smaller" ID="dgvTransaccionError" >
        <SettingsContextMenu EnableRowMenu="False">
        </SettingsContextMenu>
        <Templates>
            <EditForm>
                <table>
                    <tr>
                        <td class="auto-style13">&nbsp;</td>
                        <td class="auto-style18">
                            <dx1:aspxlabel ID="ASPxLabel28" runat="server" Text="ID :">
                            </dx1:aspxlabel>
                        </td>
                        <td class="auto-style16">
                            <dx1:aspxtextbox ID="txtId0" runat="server" ClientInstanceName="txtId" ReadOnly="True" Width="170px">
                            </dx1:aspxtextbox>
                        </td>
                        <td>&nbsp;</td>
                        <td class="auto-style19">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style13">&nbsp;</td>
                        <td class="auto-style18">
                            <dx1:aspxlabel ID="ASPxLabel29" runat="server" Text="Fecha :">
                            </dx1:aspxlabel>
                        </td>
                        <td class="auto-style16">
                            <dx1:aspxdateedit ID="detFecha0" runat="server" EditFormat="DateTime" Font-Size="Smaller">
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
                            </dx1:aspxdateedit>
                        </td>
                        <td>&nbsp;</td>
                        <td class="auto-style19">
                            <dx1:aspxlabel ID="ASPxLabel30" runat="server" Height="16px" Text="Válido Cred. Fiscal :" Width="110px">
                            </dx1:aspxlabel>
                        </td>
                        <td>
                            <dx1:aspxcombobox ID="cmbTipoCred0" runat="server" Font-Size="Smaller" ValueType="System.Int32">
                                <Items>
                                    <dx1:listedititem Text="SI" Value="1" />
                                    <dx1:listedititem Text="NO" Value="2" />
                                </Items>
                                <ClearButton Visibility="Auto">
                                </ClearButton>
                                <ValidationSettings>
                                    <RequiredField IsRequired="True" />
                                </ValidationSettings>
                            </dx1:aspxcombobox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style13">&nbsp;</td>
                        <td class="auto-style18">
                            <dx1:aspxlabel ID="ASPxLabel31" runat="server" Text="Tipo Pago :">
                            </dx1:aspxlabel>
                        </td>
                        <td class="auto-style16">
                            <dx1:aspxcombobox ID="cmbTipoPago0" runat="server" Font-Size="Smaller" ValueType="System.Int32">
                                <Items>
                                    <dx1:listedititem Text="EFECTIVO" Value="1" />
                                    <dx1:listedititem Text="PREPAGO" Value="2" />
                                    <dx1:listedititem Text="VALE" Value="3" />
                                </Items>
                                <ClearButton Visibility="Auto">
                                </ClearButton>
                                <ValidationSettings>
                                    <RequiredField IsRequired="True" />
                                </ValidationSettings>
                            </dx1:aspxcombobox>
                        </td>
                        <td>&nbsp;</td>
                        <td class="auto-style19">
                            <br />
                            <dx1:aspxlabel ID="ASPxLabel32" runat="server" Text="Tipo Consumidor :" Width="110px">
                            </dx1:aspxlabel>
                        </td>
                        <td>
                            <dx1:aspxcombobox ID="cmbTipoCons0" runat="server" Font-Size="Smaller" ValueType="System.Int32">
                                <Items>
                                    <dx1:listedititem Text="MOTORIZADO" Value="1" />
                                    <dx1:listedititem Text="BIDON" Value="2" />
                                    <dx1:listedititem Text="BOTELLA" Value="3" />
                                    <dx1:listedititem Text="OTRO" Value="4" />
                                </Items>
                                <ClearButton Visibility="Auto">
                                </ClearButton>
                                <ValidationSettings>
                                    <RequiredField IsRequired="True" />
                                </ValidationSettings>
                            </dx1:aspxcombobox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style20"></td>
                        <td class="auto-style21">
                            <dx1:aspxlabel ID="ASPxLabel33" runat="server" Text="Cliente :">
                            </dx1:aspxlabel>
                        </td>
                        <td class="auto-style22">
                            <dx1:aspxtextbox ID="txtCliente0" runat="server" Font-Size="Smaller" Height="16px" MaxLength="90" Width="170px">
                                <ClientSideEvents TextChanged="function(s, e) {
	s.SetText(s.GetText().toUpperCase());
}" />
                                <ValidationSettings>
                                    <RequiredField IsRequired="True" />
                                </ValidationSettings>
                            </dx1:aspxtextbox>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style13">&nbsp;</td>
                        <td class="auto-style18">
                            <dx1:aspxlabel ID="ASPxLabel34" runat="server" Text="NIT :">
                            </dx1:aspxlabel>
                        </td>
                        <td class="auto-style16">
                            <dx1:aspxtextbox ID="txtNit0" runat="server" Font-Size="Smaller" MaxLength="15" Width="170px">
                                <MaskSettings Mask="&lt;0..999999999999999&gt;" PromptChar=" " />
                                <ValidationSettings>
                                    <RequiredField IsRequired="True" />
                                </ValidationSettings>
                            </dx1:aspxtextbox>
                        </td>
                        <td>&nbsp;</td>
                        <td class="auto-style19">
                            <dx1:aspxlabel ID="ASPxLabel35" runat="server" Text="Usuario :">
                            </dx1:aspxlabel>
                        </td>
                        <td>
                            <dx1:aspxtextbox ID="txtUsuario0" runat="server" ClientInstanceName="txtId" Font-Size="Smaller" MaxLength="100" Width="170px">
                                <ValidationSettings>
                                    <RequiredField IsRequired="True" />
                                </ValidationSettings>
                            </dx1:aspxtextbox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style13">&nbsp;</td>
                        <td class="auto-style18">
                            <dx1:aspxlabel ID="ASPxLabel36" runat="server" Text="Tipo de Venta :">
                            </dx1:aspxlabel>
                        </td>
                        <td class="auto-style16">
                            <dx1:aspxradiobuttonlist ID="rbtTipoVenta0" runat="server" ClientInstanceName="rbtTipoVenta" SelectedIndex="0" ValueType="System.Int32">
                                <ClientSideEvents ValueChanged="function(s, e) {
	console.log('entraaaaaa....');
alert(&quot;xxxxxxxxxxxx&quot;);
txtNoAutoExt.SetVisible(false);

}" />
                                <Items>
                                    <dx1:listedititem Selected="True" Text="Nacional" Value="1" />
                                    <dx1:listedititem Text="Internacional" Value="2" />
                                </Items>
                            </dx1:aspxradiobuttonlist>
                        </td>
                        <td>&nbsp;</td>
                        <td class="auto-style19">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style13">&nbsp;</td>
                        <td class="auto-style18">
                            <dx1:aspxlabel ID="ASPxLabel37" runat="server" Text="País :">
                            </dx1:aspxlabel>
                        </td>
                        <td class="auto-style16">
                            <dx1:aspxcombobox ID="cmbPais0" runat="server">
                                <Items>
                                    <dx1:listedititem Text="BOLIVIA" Value="68" />
                                    <dx1:listedititem Text="ARGENTINA" Value="32" />
                                    <dx1:listedititem Text="BRASIL" Value="76" />
                                    <dx1:listedititem Text="CHILE" Value="152" />
                                    <dx1:listedititem Text="COLOMBIA" Value="170" />
                                    <dx1:listedititem Text="ECUADOR" Value="218" />
                                    <dx1:listedititem Text="PARAGUAY" Value="600" />
                                    <dx1:listedititem Text="PERU" Value="604" />
                                    <dx1:listedititem Text="URUGUAY" Value="858 " />
                                    <dx1:listedititem Text="VENEZUELA" Value="862" />
                                </Items>
                                <ClearButton Visibility="Auto">
                                </ClearButton>
                                <ValidationSettings>
                                    <RequiredField IsRequired="True" />
                                </ValidationSettings>
                            </dx1:aspxcombobox>
                        </td>
                        <td>&nbsp;</td>
                        <td class="auto-style19">
                            <dx1:aspxlabel ID="ASPxLabel38" runat="server" Text="Placa :">
                            </dx1:aspxlabel>
                        </td>
                        <td>
                            <dx1:aspxtextbox ID="txtPlaca0" runat="server" ClientInstanceName="txtId" Font-Size="Smaller" MaxLength="11" Width="170px">
                                <ClientSideEvents TextChanged="function(s, e) {
	s.SetText(s.GetText().toUpperCase());
}" />
                                <ValidationSettings>
                                    <RequiredField IsRequired="True" />
                                </ValidationSettings>
                            </dx1:aspxtextbox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style13">&nbsp;</td>
                        <td class="auto-style18">
                            <dx1:aspxlabel ID="ASPxLabel39" runat="server" Text="No. Autorización :" Width="100px">
                            </dx1:aspxlabel>
                        </td>
                        <td class="auto-style16">
                            <dx1:aspxtextbox ID="txtNoAuto0" runat="server" Font-Size="Smaller" MaxLength="15" Width="170px">
                                <MaskSettings Mask="&lt;0..999999999999999&gt;" />
                                <ValidationSettings>
                                    <RequiredField IsRequired="True" />
                                </ValidationSettings>
                            </dx1:aspxtextbox>
                        </td>
                        <td>&nbsp;</td>
                        <td class="auto-style19">
                            <dx1:aspxlabel ID="ASPxLabel40" runat="server" Text="No. Factura :">
                            </dx1:aspxlabel>
                        </td>
                        <td>
                            <dx1:aspxtextbox ID="txtNoFact0" runat="server" Font-Size="Smaller" MaxLength="15" Width="170px">
                                <MaskSettings Mask="&lt;0..999999999999999&gt;" />
                                <ValidationSettings>
                                    <RequiredField IsRequired="True" />
                                </ValidationSettings>
                            </dx1:aspxtextbox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style13">&nbsp;</td>
                        <td class="auto-style18">
                            <dx1:aspxlabel ID="ASPxLabel41" runat="server" Text="Cod. Control :">
                            </dx1:aspxlabel>
                        </td>
                        <td class="auto-style16">
                            <dx1:aspxtextbox ID="txtCodCtrl0" runat="server" Font-Size="Smaller" MaxLength="14" Width="170px">
                                <MaskSettings Mask="" />
                                <ValidationSettings SetFocusOnError="True">
                                </ValidationSettings>
                            </dx1:aspxtextbox>
                        </td>
                        <td>&nbsp;</td>
                        <td class="auto-style19">
                            <dx1:aspxlabel ID="ASPxLabel42" runat="server" Text="Importe :">
                            </dx1:aspxlabel>
                        </td>
                        <td>
                            <dx1:aspxtextbox ID="txtImporte0" runat="server" Width="170px">
                                <MaskSettings IncludeLiterals="DecimalSymbol" Mask="&lt;0..9999999&gt;.&lt;00..99&gt;" />
                                <ValidationSettings>
                                    <RequiredField IsRequired="True" />
                                </ValidationSettings>
                            </dx1:aspxtextbox>
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
                            <dx1:aspxlabel ID="ASPxLabel43" runat="server" Text="No. Autorización Ext. :" Width="130px">
                            </dx1:aspxlabel>
                        </td>
                        <td class="auto-style16">
                            <dx1:aspxtextbox ID="txtNoAutoExt0" runat="server" ClientInstanceName="txtNoAutoExt" Font-Size="Smaller" MaxLength="15" Width="170px">
                                <MaskSettings Mask="&lt;0..999999999999999&gt;" />
                            </dx1:aspxtextbox>
                        </td>
                        <td>&nbsp;</td>
                        <td class="auto-style19">
                            <dx1:aspxlabel ID="ASPxLabel44" runat="server" Text="No. Factura Ext. :" Width="120px">
                            </dx1:aspxlabel>
                        </td>
                        <td>
                            <dx1:aspxtextbox ID="txtNoFactExt0" runat="server" ClientInstanceName="txtNoFactExt" Font-Size="Smaller" MaxLength="15" Width="170px">
                                <MaskSettings Mask="&lt;0..999999999999999&gt;" />
                            </dx1:aspxtextbox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style13">&nbsp;</td>
                        <td class="auto-style18">
                            <dx1:aspxlabel ID="ASPxLabel45" runat="server" Text="Cod. Control Ext. :">
                            </dx1:aspxlabel>
                        </td>
                        <td class="auto-style16">
                            <dx1:aspxtextbox ID="txtCodCtrlExt0" runat="server" Font-Size="Smaller" MaxLength="14" Width="170px">
                                <MaskSettings Mask="" />
                                <ValidationSettings SetFocusOnError="True">
                                </ValidationSettings>
                            </dx1:aspxtextbox>
                        </td>
                        <td>&nbsp;</td>
                        <td class="auto-style19">
                            <dx1:aspxlabel ID="ASPxLabel46" runat="server" Text="Importe Ext. :">
                            </dx1:aspxlabel>
                        </td>
                        <td>
                            <dx1:aspxtextbox ID="txtImporteExt0" runat="server" Width="170px">
                                <MaskSettings IncludeLiterals="DecimalSymbol" Mask="&lt;0..9999999&gt;.&lt;00..99&gt;" />
                            </dx1:aspxtextbox>
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
                            <dx1:aspxlabel ID="ASPxLabel47" runat="server" Text="No. Doc. Referencia :" Width="130px">
                            </dx1:aspxlabel>
                        </td>
                        <td class="auto-style16">
                            <dx1:aspxtextbox ID="txtNoDocRef0" runat="server" Font-Size="Smaller" MaxLength="15" Width="170px">
                                <MaskSettings Mask="&lt;0..999999999999999&gt;" />
                            </dx1:aspxtextbox>
                        </td>
                        <td>&nbsp;</td>
                        <td class="auto-style19">
                            <dx1:aspxlabel ID="ASPxLabel48" runat="server" Text="Tipo Factura :" Visible="False">
                            </dx1:aspxlabel>
                        </td>
                        <td>
                            <dx1:aspxcombobox ID="cmbTipoFact0" runat="server" Font-Size="Smaller" style="margin-top: 0" ValueType="System.Int32" Visible="False">
                                <Items>
                                    <dx1:listedititem Text="MANUAL" Value="1" />
                                    <dx1:listedititem Text="COMPUTARIZADA" Value="2" />
                                </Items>
                                <ClearButton Visibility="Auto">
                                </ClearButton>
                            </dx1:aspxcombobox>
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
                            <dx1:aspxlabel ID="ASPxLabel49" runat="server" Text="Estado :">
                            </dx1:aspxlabel>
                        </td>
                        <td class="auto-style16">
                            <dx1:aspxcombobox ID="cmbEstado0" runat="server" Font-Size="Smaller" ValueType="System.Int32">
                                <Items>
                                    <dx1:listedititem Text="VALIDO" Value="1" />
                                    <dx1:listedititem Text="ANULADO" Value="2" />
                                </Items>
                                <ClearButton Visibility="Auto">
                                </ClearButton>
                                <ValidationSettings>
                                    <RequiredField IsRequired="True" />
                                </ValidationSettings>
                            </dx1:aspxcombobox>
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
                            <dx1:aspxlabel ID="ASPxLabel50" runat="server" Text="Producto :" Width="100px">
                            </dx1:aspxlabel>
                        </td>
                        <td class="auto-style16">
                            <dx1:aspxcombobox ID="cmbProd0" runat="server" Font-Size="Smaller" ValueType="System.Int32">
                                <Items>
                                    <dx1:listedititem Text="GNV" Value="1" />
                                    <dx1:listedititem Text="DIESEL OIL" Value="2" />
                                    <dx1:listedititem Text="GASOLINA ESPECIAL" Value="3" />
                                    <dx1:listedititem Text="GASOLINA PREMIUM" Value="4" />
                                </Items>
                                <ClearButton Visibility="Auto">
                                </ClearButton>
                                <ValidationSettings>
                                    <RequiredField IsRequired="True" />
                                </ValidationSettings>
                            </dx1:aspxcombobox>
                        </td>
                        <td>&nbsp;</td>
                        <td class="auto-style19">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style13">&nbsp;</td>
                        <td class="auto-style18">
                            <dx1:aspxlabel ID="ASPxLabel51" runat="server" Text="Cantidad :">
                            </dx1:aspxlabel>
                        </td>
                        <td class="auto-style16">
                            <dx1:aspxtextbox ID="txtCantidad0" runat="server" Width="170px">
                                <MaskSettings IncludeLiterals="DecimalSymbol" Mask="&lt;0..9999999&gt;.&lt;00..99&gt;" />
                                <ValidationSettings>
                                    <RequiredField IsRequired="True" />
                                </ValidationSettings>
                            </dx1:aspxtextbox>
                        </td>
                        <td>&nbsp;</td>
                        <td class="auto-style19">
                            <dx1:aspxlabel ID="ASPxLabel52" runat="server" Text="Precio :">
                            </dx1:aspxlabel>
                        </td>
                        <td>
                            <dx1:aspxtextbox ID="txtPrecio0" runat="server" Width="170px">
                                <MaskSettings IncludeLiterals="DecimalSymbol" Mask="&lt;0..999&gt;.&lt;00..99&gt;" PromptChar=" " />
                                <ValidationSettings>
                                    <RequiredField IsRequired="True" />
                                </ValidationSettings>
                            </dx1:aspxtextbox>
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
                            <dx1:aspxlabel ID="ASPxLabel53" runat="server" Text="Autorización de Venta :" Width="150px">
                            </dx1:aspxlabel>
                        </td>
                        <td class="auto-style16">
                            <dx1:aspxcombobox ID="cmbAutoVta0" runat="server">
                                <Items>
                                    <dx1:listedititem Text="SUSTANCIAS CONTROLADAS" Value="1" />
                                    <dx1:listedititem Text="COMANDO CONJUNTO" Value="2" />
                                </Items>
                                <ClearButton Visibility="Auto">
                                </ClearButton>
                            </dx1:aspxcombobox>
                        </td>
                        <td>&nbsp;</td>
                        <td class="auto-style19">
                            <dx1:aspxlabel ID="ASPxLabel54" runat="server" Text="No. Hoja Ruta :">
                            </dx1:aspxlabel>
                        </td>
                        <td>
                            <dx1:aspxtextbox ID="txtNoAutoVta0" runat="server" Font-Size="Smaller" MaxLength="15" Width="170px">
                                <MaskSettings Mask="&lt;0..999999999999999&gt;" />
                            </dx1:aspxtextbox>
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
                            <dx1:aspxlabel ID="lblMensaje0" runat="server" Text="hghgh">
                            </dx1:aspxlabel>
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
                            <dx1:aspxbutton ID="btnAceptar0" runat="server" AutoPostBack="False" ClientInstanceName="btnAceptar" Text="Aceptar" Theme="Office2003Olive" Width="72px">
                                <ClientSideEvents Click="function(s, e) {
                                                                                dgvTransacciones.UpdateEdit()	
                                                                        }" />
                            </dx1:aspxbutton>
                        </td>
                        <td>
                            <dx1:aspxbutton ID="btnCancelar0" runat="server" AutoPostBack="False" ClientInstanceName="btnCancelar" Text="Cerrar" Theme="Office2003Olive">
                                <ClientSideEvents Click="function(s, e) {
                                                                                dgvTransacciones.CancelEdit()	
                                                                        }" />
                            </dx1:aspxbutton>
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
        <SettingsPager PageSize="50" NumericButtonCount="5">
            <PageSizeItemSettings Items="50, 100, 200, 500, 1000" Visible="True">
            </PageSizeItemSettings>
        </SettingsPager>
        <SettingsEditing Mode="PopupEditForm">
        </SettingsEditing>
        <Settings ShowHeaderFilterButton="True">
        </Settings>
        <SettingsBehavior AllowFocusedRow="True">
        </SettingsBehavior>
        <SettingsPopup>
            <EditForm Modal="True">
            </EditForm>
        </SettingsPopup>
        <SettingsText CommandNew="Nuevo">
        </SettingsText>
        <Columns>
            <dx1:gridviewcommandcolumn ShowInCustomizationForm="True" Visible="False" VisibleIndex="0">
            </dx1:gridviewcommandcolumn>
            <dx1:gridviewdatatextcolumn FieldName="StrIdVenta" ShowInCustomizationForm="True" Caption="ID VENTA" VisibleIndex="2">
                <Settings AutoFilterCondition="Contains">
                </Settings>
            </dx1:gridviewdatatextcolumn>
            <dx1:gridviewdatatextcolumn FieldName="StrErrores" ShowInCustomizationForm="True" Caption="ERROR" ToolTip="Tipo de Consumidor" VisibleIndex="11">
                <Settings AutoFilterCondition="Contains">
                </Settings>
            </dx1:gridviewdatatextcolumn>
            <dx1:gridviewdatatextcolumn FieldName="LngNit" ShowInCustomizationForm="True" Caption="NIT" VisibleIndex="5">
                <Settings AutoFilterCondition="Contains">
                </Settings>
            </dx1:gridviewdatatextcolumn>
            <dx1:gridviewdatatextcolumn FieldName="LngFechaEmision" ShowInCustomizationForm="True" Caption="FECHA" VisibleIndex="4">
                <Settings AutoFilterCondition="Contains">
                </Settings>
            </dx1:gridviewdatatextcolumn>
            <dx1:gridviewdatatextcolumn FieldName="LngNroFactura" ShowInCustomizationForm="True" Caption="NO. FACTURA" VisibleIndex="9">
                <Settings AutoFilterCondition="Contains">
                </Settings>
            </dx1:gridviewdatatextcolumn>
            <dx1:gridviewdatatextcolumn FieldName="LngNroAutorizacion" ShowInCustomizationForm="True" Caption="NO AUTORIZACION" ToolTip="Producto" VisibleIndex="7">
                <Settings AutoFilterCondition="Contains">
                </Settings>
                <FilterTemplate>
                    <dx1:aspxcombobox ID="cmbFiltroProd0" runat="server" ValueType="System.String">
                    </dx1:aspxcombobox>
                </FilterTemplate>
            </dx1:gridviewdatatextcolumn>
            <dx1:gridviewdatatextcolumn FieldName="LngNroReferencia" ShowInCustomizationForm="True" Caption="NO REF." VisibleIndex="10">
                <Settings AutoFilterCondition="Equals">
                </Settings>
            </dx1:gridviewdatatextcolumn>
            <dx1:gridviewdatatextcolumn FieldName="LngId" ShowInCustomizationForm="True" Caption="#" VisibleIndex="1">
            </dx1:gridviewdatatextcolumn>
        </Columns>
    </dx1:ASPxGridView>
    <dx1:ASPxGridViewExporter runat="server" ID="dgeExportar">
    </dx1:ASPxGridViewExporter>


</asp:Content>
