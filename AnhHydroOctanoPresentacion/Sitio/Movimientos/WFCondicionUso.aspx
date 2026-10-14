<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio/ANH.Master" AutoEventWireup="true" CodeBehind="WFCondicionUso.aspx.cs" Inherits="AnhHydroOctanoPresentacion.Sitio.Movimientos.WFCondicionUso" %>

<%@ Register Src="~/Controles/CtrCondicionUso.ascx" TagPrefix="uc1" TagName="CtrCondicionUso" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
       <asp:ScriptManager runat="server"></asp:ScriptManager>
    
       <uc1:CtrCondicionUso runat="server" ID="CtrCondicionUso"/>

</asp:Content>
