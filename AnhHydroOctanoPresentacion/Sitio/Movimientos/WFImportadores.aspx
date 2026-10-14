<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio/ANH.Master" AutoEventWireup="true" CodeBehind="WFImportadores.aspx.cs" Inherits="AnhHydroOctanoPresentacion.Sitio.Movimientos.WFImportadores" %>

<%@ Register Src="~/Controles/CtrImportadores.ascx" TagPrefix="uc1" TagName="CtrImportadores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>
    
    <uc1:CtrImportadores runat="server" ID="CtrImportadores"/>
</asp:Content>
