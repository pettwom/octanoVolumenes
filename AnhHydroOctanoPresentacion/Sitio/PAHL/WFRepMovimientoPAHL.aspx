<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio/ANH.Master" AutoEventWireup="true" CodeBehind="WFRepMovimientoPAHL.aspx.cs" Inherits="AnhHydroOctanoPresentacion.Sitio.PAHL.WFRepMovimientoPAHL" %>

<%@ Register Src="~/Controles/CtrRepMovimientoPAHL.ascx" TagPrefix="uc1" TagName="CtrRepMovimientoPAHL" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <uc1:ctrrepmovimientopahl runat="server" id="CtrRepMovimientoPAHL" />
</asp:Content>
