<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio/ANH.Master" AutoEventWireup="true" CodeBehind="WFRepGrafico.aspx.cs" Inherits="AnhHydroOctanoPresentacion.Sitio.Movimientos.WFRepGrafico" %>

<%@ Register Src="~/Controles/CtrRepGrafico.ascx" TagPrefix="uc1" TagName="CtrPrincipal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
       <asp:ScriptManager runat="server"></asp:ScriptManager>
    
       <uc1:CtrPrincipal runat="server" ID="CtrRepGrafico"/>

</asp:Content>
