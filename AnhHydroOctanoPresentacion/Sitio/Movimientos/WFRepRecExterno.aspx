<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio/ANH.Master" AutoEventWireup="true" CodeBehind="WFRepRecExterno.aspx.cs" Inherits="AnhHydroOctanoPresentacion.Sitio.Movimientos.WFRepRecExterno" %>

<%@ Register Src="~/Controles/CtrRepRecExterno.ascx" TagPrefix="uc1" TagName="CtrRepRecExterno" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <uc1:CtrRepRecExterno runat="server" id="CtrRepRecExterno" />
</asp:Content>
