<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio/ANH.Master" AutoEventWireup="true" CodeBehind="WFProde.aspx.cs" Inherits="AnhHydroOctanoPresentacion.Sitio.Movimientos.WFProde" %>
<%@ Register Src="~/Controles/CtrImportacionDatosPDE.ascx" TagPrefix="uc1" TagName="CtrImportacionDatosPDE" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
       <asp:ScriptManager runat="server"></asp:ScriptManager>
    

        <uc1:CtrImportacionDatosPDE runat="server" ID="CtrImportacionDatosPDE"/>
    
    
</asp:Content>
