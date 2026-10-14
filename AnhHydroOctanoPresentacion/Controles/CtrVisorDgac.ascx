<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtrVisorDgac.ascx.cs" Inherits="AnhHydroOctanoPresentacion.Controles.CtrVisorDgac" %>

<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

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
    
    .auto-style1 {
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
                        <dx:ASPxLabel runat="server" ForeColor="#367B2F" ID="lblTitulo" Font-Bold="True" Font-Names="Rockwell" Font-Size="Larger" Text="INTEGRACION D.G.A.C."></dx:ASPxLabel>
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
                    <table style="width: 100%;">

                        <tr>
                            <td style="padding-bottom: 5px">
                                <dx:ASPxLabel ID="ASPxLabel338" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="Smaller" ForeColor="#999999" Text="CARACTERISTICAS" Width="100%">
                                    <BorderBottom BorderStyle="Solid" />
                                </dx:ASPxLabel>
                            </td>
                            

                        </tr>
                        <tr>
                            <td>
                                <dx:ASPxLabel ID="ASPxLabel58" runat="server" ForeColor="#003300" Text="Origen :">
                                </dx:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <dx:ASPxRadioButtonList ID="rbtOrigen" runat="server" SelectedIndex="1" Theme="Metropolis" Width="100%" ValueType="System.Int32" ClientInstanceName="rbtOrigen">
                                    <Items>
                                        <dx:ListEditItem Text="ANH" Value="1" />
                                        <dx:ListEditItem Selected="True" Text="DGAC" Value="2" />
                                    </Items>
                                </dx:ASPxRadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-bottom: 5px">
                                &nbsp;<dx:ASPxLabel ID="ASPxLabel340" runat="server" ForeColor="#003300" Text="Tipo de Información :">
                                </dx:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-bottom: 10px">
                                <dx:ASPxRadioButtonList ID="rbtServicio" runat="server" EnableTheming="True" SelectedIndex="0" Theme="Metropolis" Width="100%" ValueType="System.Int32" ClientInstanceName="rbtServicio">
                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
	
rbtTipo.SetValue(0);
rbtEspAeronave.SetVisible(false);
rbtEspPiloto.SetVisible(false);
txtEspecifico.SetVisible(false);
txtEspecifico.SetText('');

}" />
                                    <Items>
                                        <dx:ListEditItem Text="AERONAVES" Value="1" Selected="True" />
                                        <dx:ListEditItem Text="PILOTOS" Value="2" />
                                    </Items>
                                </dx:ASPxRadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <dx:ASPxLabel ID="ASPxLabel341" runat="server" ForeColor="#003300" Text="Especificación :">
                                </dx:ASPxLabel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <dx:ASPxRadioButtonList ID="rbtTipo" runat="server" EnableTheming="True" SelectedIndex="0" Theme="Metropolis" ValueType="System.Int32" Width="100%" ClientInstanceName="rbtTipo">
                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {

rbtEspAeronave.SetVisible(false);
rbtEspPiloto.SetVisible(false);
txtEspecifico.SetVisible(false);
txtEspecifico.SetText('');

if(rbtTipo.GetValue()==1)
{
txtEspecifico.SetVisible(true);
if(rbtServicio.GetValue()==1)
{
rbtEspAeronave.SetVisible(true);
}
else
{
rbtEspPiloto.SetVisible(true);
}
}

}" />
                                    <Items>
                                        <dx:ListEditItem Selected="True" Text="TODO" Value="0" />
                                        <dx:ListEditItem Text="ESPECIFICO" Value="1" />
                                    </Items>
                                </dx:ASPxRadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <dx:ASPxRadioButtonList ID="rbtEspAeronave" runat="server" ClientInstanceName="rbtEspAeronave" ClientVisible="False" EnableTheming="True" SelectedIndex="0" Theme="Metropolis" ValueType="System.Int32" Width="100%">
                                    <Items>
                                        <dx:ListEditItem Selected="True" Text="MATRICULA" Value="1" />
                                        <dx:ListEditItem Text="NO. SERIE" Value="2" />
                                        <dx:ListEditItem Text="MARCA" Value="3" />
                                        <dx:ListEditItem Text="MODELO" Value="4" />
                                    </Items>
                                </dx:ASPxRadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <dx:ASPxRadioButtonList ID="rbtEspPiloto" runat="server" ClientInstanceName="rbtEspPiloto" ClientVisible="False" EnableTheming="True" SelectedIndex="0" Theme="Metropolis" ValueType="System.Int32" Width="100%">
                                    <Items>
                                        <dx:ListEditItem Selected="True" Text="LICENCIA" Value="1" />
                                        <dx:ListEditItem Text="AP. PATERNO" Value="2" />
                                        <dx:ListEditItem Text="AP. MATERNO" Value="3" />
                                        <dx:ListEditItem Text="NOMBRES" Value="4" />
                                        <dx:ListEditItem Text="TITULO" Value="5" />
                                        <dx:ListEditItem Text="VIGENCIA (dd/mm/yyyy)" Value="6" />
                                        <dx:ListEditItem Text="LICENCIA (PIC Y SIC)" Value="7" />
                                    </Items>
                                </dx:ASPxRadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style1">
                                <dx:ASPxTextBox ID="txtEspecifico" runat="server" BackColor="#317D40" ForeColor="White" ClientInstanceName="txtEspecifico" ClientVisible="False" Width="100%">
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-bottom: 5px">
                                <dx:ASPxCheckBox ID="chkAccion" runat="server" CheckState="Unchecked" Text="Actualizar ANH" Theme="Metropolis" ValueChecked="1" ValueType="System.Int32" ValueUnchecked="0" ClientInstanceName="chkAccion">
                                </dx:ASPxCheckBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <dx:ASPxButton ID="btnConsulta" runat="server" AutoPostBack="False" Font-Bold="True" ForeColor="Maroon" Text="Obtener Registros" Theme="Metropolis" Width="100%">
                                    <ClientSideEvents Click="function(s, e) {
if(rbtOrigen.GetValue()==2 &amp;&amp; chkAccion.GetValue()==1)
{
if(confirm('Actualizar las tablas ANH - Esta acción eliminara los registros pasados. ¿Desea continuar?'))
{
pnlDatos.PerformCallback();
}
}
else
{
pnlDatos.PerformCallback();
}
}" />
                                    <Image IconID="actions_download_16x16">
                                    </Image>
                                </dx:ASPxButton>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
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
                                        <td colspan="3" style="text-align: center">
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
                                        <td colspan ="3">
                                            <legend>
                                                <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="Rockwell" Font-Size="Medium" ForeColor="#367B2F" Text="INFORMACIÓN OBTENIDA"></asp:Label>
                                            </legend>
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                         <td colspan="3" style="width: 100%">
                                             <table style="width: 100%">
                                                 <tr>
                                                     <td style="width: 85%">
                                                         <dx:ASPxLabel ID="ASPxLabel339" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="Smaller" ForeColor="#FF8800" Width="100%">
                                                             <BorderBottom BorderStyle="Solid" />
                                                         </dx:ASPxLabel>
                                                     </td>
                                                     <td style="text-align: right; width: 15%">
                                                         <dx:ASPxButton ID="btnEmitir" runat="server" Font-Bold="True" ForeColor="Maroon" OnClick="btnEmitir_Click" Text="Exportar Excel" Theme="Metropolis" Width="100%">
                                                             <Image IconID="export_exporttoxlsx_16x16">
                                                             </Image>
                                                         </dx:ASPxButton>
                                                     </td>
                                                 </tr>
                                             </table>
                                         </td>
                                     </tr>

                                    <tr>
                                        <td colspan="3" style="width: 100%">
                                            <dx:ASPxGridView ID="dgvDatos" runat="server" Font-Size="Smaller" OnHtmlRowPrepared="dgvDatos_HtmlRowPrepared" Theme="Default" Width="100%">
                                                <SettingsPager PageSize="100">
                                                    <PageSizeItemSettings Items="10, 100, 1000" Visible="True" ShowAllItem="True">
                                                    </PageSizeItemSettings>
                                                </SettingsPager>
                                                <Settings ShowFilterRow="True" ShowFooter="True" ShowHeaderFilterButton="True" />
                                                <SettingsBehavior AllowFocusedRow="True" />
                                                <Styles CssPostfix="DevGlp">
                                                        <AlternatingRow BackColor="#EEEEEE">
                                                        </AlternatingRow>
                                                    </Styles>
                                            </dx:ASPxGridView>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td colspan="3" style="width: 80%">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 80%">
                                            <dx:ASPxGridViewExporter ID="dgeExportar" runat="server">
                                            </dx:ASPxGridViewExporter>
                                        </td>
                                        <td style="width: 1%">&nbsp;</td>
                                        <td style="text-align: right; width: 19%">
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
</div>










