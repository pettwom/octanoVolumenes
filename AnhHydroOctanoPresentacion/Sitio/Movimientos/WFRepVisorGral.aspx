<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio/ANH.Master" AutoEventWireup="true" CodeBehind="WFRepVisorGral.aspx.cs" Inherits="AnhHydroOctanoPresentacion.Sitio.Movimientos.WFRepVisorGral" %>

<%@ Register Src="~/Controles/CtrRepVisorGral.ascx" TagPrefix="uc1" TagName="CtrRepVisorGral" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
       <asp:ScriptManager runat="server"></asp:ScriptManager>
    

        <uc1:CtrRepVisorGral runat="server" ID="CtrRepVisorGral"/>
    
    
    

</asp:Content>
