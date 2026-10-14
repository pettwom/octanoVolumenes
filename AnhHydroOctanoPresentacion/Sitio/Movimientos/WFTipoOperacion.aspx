<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio/ANH.Master" AutoEventWireup="true" CodeBehind="WFTipoOperacion.aspx.cs" Inherits="AnhHydroOctanoPresentacion.Sitio.Movimientos.WFTipoOperacion" %>

<%@ Register Src="~/Controles/CtrTipoOperacion.ascx" TagPrefix="uc1" TagName="CtrTipoOperacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
       <asp:ScriptManager runat="server"></asp:ScriptManager>
    
       <uc1:CtrTipoOperacion runat="server" ID="CtrTipoOperacion"/>

</asp:Content>
