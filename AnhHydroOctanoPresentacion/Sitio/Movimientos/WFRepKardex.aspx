<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio/ANH.Master" AutoEventWireup="true" CodeBehind="WFRepKardex.aspx.cs" Inherits="AnhHydroOctanoPresentacion.Sitio.Movimientos.WFRepKardex" %>

<%@ Register Src="~/Controles/CtrRepKardex.ascx" TagPrefix="uc1" TagName="CtrRepKardex" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
       <asp:ScriptManager runat="server"></asp:ScriptManager>
    

        <uc1:CtrRepKardex runat="server" ID="CtrRepKardex"/>
    
    
</asp:Content>
