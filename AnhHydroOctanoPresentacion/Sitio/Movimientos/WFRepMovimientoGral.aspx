<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio/ANH.Master" AutoEventWireup="true" CodeBehind="WFRepMovimientoGral.aspx.cs" Inherits="AnhHydroOctanoPresentacion.Sitio.Movimientos.WFRepMovimientoGral" %>

<%@ Register Src="~/Controles/CtrRepMovimientoGral.ascx" TagPrefix="uc1" TagName="CtrRepMovimientoGral" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
       <asp:ScriptManager runat="server"></asp:ScriptManager>
    
       <uc1:CtrRepMovimientoGral runat="server" ID="CtrRepMovimientoGral"/>

</asp:Content>
