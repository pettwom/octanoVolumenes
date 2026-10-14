<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio/ANH.Master" AutoEventWireup="true" CodeBehind="WFImportacionDatos.aspx.cs" Inherits="AnhHydroOctanoPresentacion.Sitio.Movimientos.WFImportacionDatos" %>

<%@ Register Src="~/Controles/CtrImportacionDatos.ascx" TagPrefix="uc1" TagName="CtrImportacionDatos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
       <asp:ScriptManager runat="server"></asp:ScriptManager>
    
       <uc1:CtrImportacionDatos runat="server" ID="CtrImportacionDatos"/>

</asp:Content>
