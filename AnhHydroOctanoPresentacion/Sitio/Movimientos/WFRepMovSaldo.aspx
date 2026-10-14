<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio/ANH.Master" AutoEventWireup="true" CodeBehind="WFRepMovSaldo.aspx.cs" Inherits="AnhHydroOctanoPresentacion.Sitio.Movimientos.WFRepMovSaldo" %>

<%@ Register Src="~/Controles/CtrRepMovSaldo.ascx" TagPrefix="uc1" TagName="CtrRepMovSaldo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <uc1:CtrRepMovSaldo runat="server" ID="CtrRepMovSaldo"/>
</asp:Content>
