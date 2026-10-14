<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio/ANH.Master" AutoEventWireup="true" CodeBehind="WFRepVentaHora.aspx.cs" Inherits="AnhHydroOctanoPresentacion.Sitio.Movimientos.WFRepVentaHora" %>
<%@ Register Src="~/Controles/CtrRepVentasHora.ascx" TagPrefix="uc1" TagName="CtrRepVentasHora" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>
    
    <uc1:ctrrepventashora runat="server" id="CtrRepVentasHora" />
</asp:Content>
