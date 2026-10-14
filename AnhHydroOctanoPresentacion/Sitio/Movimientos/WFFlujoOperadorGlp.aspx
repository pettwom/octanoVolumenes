<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio/ANH.Master" AutoEventWireup="true" CodeBehind="WFFlujoOperadorGlp.aspx.cs" Inherits="AnhHydroOctanoPresentacion.Sitio.Movimientos.WFFlujoOperadorGlp" %>

<%@ Register Src="~/Controles/CtrFlujoOperadorGlp.ascx" TagPrefix="uc1" TagName="CtrFlujoOperadorGlp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <uc1:CtrFlujoOperadorGlp runat="server" ID="CtrFlujoOperador"/>
</asp:Content>
