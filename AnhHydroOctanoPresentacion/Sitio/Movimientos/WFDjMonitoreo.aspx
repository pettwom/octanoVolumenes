<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio/ANH.Master" AutoEventWireup="true" CodeBehind="WFDjMonitoreo.aspx.cs" Inherits="AnhHydroOctanoPresentacion.Sitio.Movimientos.WFDjMonitoreo" %>

<%@ Register Src="~/Controles/CtrDjMonitoreo.ascx" TagPrefix="uc1" TagName="CtrDjMonitoreo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
       <asp:ScriptManager runat="server"></asp:ScriptManager>
    

        <uc1:CtrDjMonitoreo runat="server" ID="CtrDjMonitoreo"/>
    
    
    

</asp:Content>
