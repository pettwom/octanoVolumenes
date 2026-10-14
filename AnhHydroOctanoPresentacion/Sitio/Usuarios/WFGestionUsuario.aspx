<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio/ANH.Master" AutoEventWireup="true" CodeBehind="WFGestionUsuario.aspx.cs" Inherits="AnhHydroOctanoPresentacion.Sitio.Usuarios.WFGestionUsuario" %>

<%@ Register Src="~/Controles/CtrGestionUsuario.ascx" TagPrefix="uc1" TagName="CtrGestionUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <uc1:CtrGestionUsuario runat="server" ID="CtrGestionUsuario"/>
</asp:Content>
