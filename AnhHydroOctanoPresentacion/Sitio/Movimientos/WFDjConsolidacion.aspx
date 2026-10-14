<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio/ANH.Master" AutoEventWireup="true" CodeBehind="WFDjConsolidacion.aspx.cs" Inherits="AnhHydroOctanoPresentacion.Sitio.Movimientos.WFDjConsolidacion" %>

<%@ Register Src="~/Controles/CtrDjConsolidacion.ascx" TagPrefix="uc1" TagName="CtrDjConsolidacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
       <asp:ScriptManager runat="server"></asp:ScriptManager>
    
       <uc1:CtrDjConsolidacion runat="server" ID="CtrDjConsolidacion"/>

</asp:Content>
