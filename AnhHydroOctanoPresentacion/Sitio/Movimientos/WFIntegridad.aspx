<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio/ANH.Master" AutoEventWireup="true" CodeBehind="WFIntegridad.aspx.cs" Inherits="AnhHydroOctanoPresentacion.Sitio.Movimientos.WFIntegridad" %>

<%@ Register Src="~/Controles/CtrIntegridad.ascx" TagPrefix="uc1" TagName="CtrIntegridad" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <uc1:CtrIntegridad runat="server" ID="CtrIntegridad"/>
</asp:Content>
