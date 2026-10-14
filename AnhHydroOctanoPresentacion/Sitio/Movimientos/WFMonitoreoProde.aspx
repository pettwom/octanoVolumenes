<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio/ANH.Master" AutoEventWireup="true" CodeBehind="WFMonitoreoProde.aspx.cs" Inherits="AnhHydroOctanoPresentacion.Sitio.Movimientos.WFMonitoreoProde" %>
<%@ Register Src="~/Controles/CtrMonitoreoProde.ascx" TagPrefix="uc1" TagName="CtrMonitoreoProde" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
       <asp:ScriptManager runat="server"></asp:ScriptManager>
    

        <uc1:CtrMonitoreoProde runat="server" ID="CtrMonitoreoProde"/>
    
    
</asp:Content>