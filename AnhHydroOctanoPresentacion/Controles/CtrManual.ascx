<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtrManual.ascx.cs" Inherits="AnhHydroOctanoPresentacion.Controles.CtrManual" %>

<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%--<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.XtraReports.v15.1.Web, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraReports.Web" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxScheduler.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxScheduler" tagprefix="dxwschs" %>--%>

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
    
    </style>

<div class="col-md-12">

<table style="width: 100%">
    <tr>
        <td style="text-align: center;">
            <dx:ASPxRoundPanel runat="server" ShowHeader="False" Width="100%" ID="pnlTitulo">
                <PanelCollection>
                    <dx:PanelContent runat="server">
                        <table style="width: 100%">
                            <tr>
                                <td>
                                    <dx:ASPxLabel runat="server" ForeColor="#367B2F" ID="lblTitulo" Font-Bold="True" Font-Names="Rockwell" Font-Size="Larger" Text="MANUAL DE USUARIO"></dx:ASPxLabel>
                                </td>
                            </tr>
                        </table>
                        
                    </dx:PanelContent>
                </PanelCollection>
                <Border BorderStyle="Ridge" BorderWidth="1px" BorderColor="#003300"></Border>
            </dx:ASPxRoundPanel>
        </td>
    </tr>
</table>

Si no puede visualizar el manual de Usuario descargue <a target="_blank" href="http://get.adobe.com/reader/">Adobe PDF Reader</a>.

<object data="../Manuales/OctVol_ManualUsuario.pdf" type="application/pdf" width="100%" height="750px" internalinstanceid="88">
    
</object>

</div>















