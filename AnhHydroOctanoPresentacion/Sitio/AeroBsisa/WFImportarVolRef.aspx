<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio/ANH.Master" AutoEventWireup="true" CodeBehind="WFImportarVolRef.aspx.cs" Inherits="AnhHydroOctanoPresentacion.Sitio.AeroBsisa.WFImportarVolRef" %>

<%@ Register Src="~/Controles/CtrImportarVolRef.ascx" TagPrefix="uc1" TagName="CtrImportarVolRef" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
       <asp:ScriptManager runat="server"></asp:ScriptManager>
    
       <uc1:CtrImportarVolRef runat="server" ID="CtrImportarVolRef"/>

</asp:Content>
