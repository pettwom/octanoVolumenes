<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio/ANH.Master" AutoEventWireup="true" CodeBehind="repFichaTecnicaPrincipal.aspx.cs" Inherits="AnhHydroOctanoPresentacion.Sitio.Reportes.repFichaTecnicaPrincipal" %>

<%@ Register Src="~/Controles/CtrFichaTecnicaPrincipal.ascx" TagPrefix="uc1" TagName="CtrFichaTecnicaPrincipal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
       <asp:ScriptManager runat="server"></asp:ScriptManager>
    
       <uc1:CtrFichaTecnicaPrincipal runat="server" ID="CtrFichaTecnicaPrincipal"/>

</asp:Content>
