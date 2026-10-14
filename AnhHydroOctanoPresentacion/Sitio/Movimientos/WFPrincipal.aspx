<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio/ANH.Master" AutoEventWireup="true" CodeBehind="WFPrincipal.aspx.cs" Inherits="AnhHydroOctanoPresentacion.Sitio.Movimientos.WFPrincipal" %>

<%@ Register Src="~/Controles/CtrPrincipal.ascx" TagPrefix="uc1" TagName="CtrPrincipal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
       <asp:ScriptManager runat="server"></asp:ScriptManager>
    
       <uc1:CtrPrincipal runat="server" ID="CtrPrincipal"/>

</asp:Content>
