<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio/Site.Master" AutoEventWireup="true" CodeBehind="wfPrincipal1.aspx.cs" Inherits="AnhHydroOctanoPresentacion.Sitio.Movimientos.wfPrincipal" %>
<%@ Register TagPrefix="cc" Namespace="Subgurim.Controles" Assembly="GMaps, Version=4.1.0.5, Culture=neutral, PublicKeyToken=564d55b144e7aa5a" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table>
        <td rowspan="10">
                <%--<div>                
                    <cc:GMap ID="GMap1" runat="server" Width="500px" />
                </div>--%>
            
            

            </td>
    </table>

<asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="True"></asp:ScriptManager>

<asp:Button runat="server" Text="Update first panel" ID="Button1" />

<asp:UpdatePanel runat="server" ID="Panel1" UpdateMode="Conditional">
    <ContentTemplate>
        <div style="border: 1px solid red; padding: 6px">
            <p><strong>First panel</strong></p>
            <p><%=DateTime.Now.ToString() %></p>
        </div>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click" />
    </Triggers>
</asp:UpdatePanel>
<asp:UpdatePanel runat="server" ID="Panel2" UpdateMode="Conditional" EnableViewState="True">
    <ContentTemplate>
        <div style="border: 1px solid blue; padding: 6px">
            <p><strong>Second panel</strong></p>
            <cc:GMap ID="GMap1" runat="server" />
        </div>
    </ContentTemplate>
</asp:UpdatePanel>

</asp:Content>
