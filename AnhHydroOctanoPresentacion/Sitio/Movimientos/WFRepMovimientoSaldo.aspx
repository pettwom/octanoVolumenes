<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio/ANH.Master" AutoEventWireup="true" CodeBehind="WFRepMovimientoSaldo.aspx.cs" Inherits="AnhHydroOctanoPresentacion.Sitio.Movimientos.WFRepMovimientoSaldo" %>

<%@ Register Src="~/Controles/CtrRepMovimientoSaldo.ascx" TagPrefix="uc1" TagName="CtrRepMovimientoSaldo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
       <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    

        <uc1:CtrRepMovimientoSaldo runat="server" ID="CtrRepMovimientoSaldo"/>
      
    
</asp:Content>


