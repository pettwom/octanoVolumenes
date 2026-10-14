<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio/ANH.Master" AutoEventWireup="true" CodeBehind="WFDeclaracionJurada.aspx.cs" Inherits="AnhHydroOctanoPresentacion.Sitio.Movimientos.WFDeclaracionJurada" %>

<%@ Register Src="~/Controles/CtrDeclaracionJurada.ascx" TagPrefix="uc1" TagName="CtrDeclaracionJurada" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
       
    <asp:ScriptManager runat="server"></asp:ScriptManager>
    
    <uc1:CtrDeclaracionJurada runat="server" ID="CtrDeclaracionJurada"/>
    
</asp:Content>
