<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio/ANH.Master" AutoEventWireup="true" CodeBehind="WFRepMovimientoDdjj.aspx.cs" Inherits="AnhHydroOctanoPresentacion.Sitio.Movimientos.WFRepMovimientoDdjj" %>

<%@ Register Src="~/Controles/CtrRepMovimientoDdjj.ascx" TagPrefix="uc1" TagName="CtrRepMovimientoDdjj" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
       <asp:ScriptManager runat="server"></asp:ScriptManager>
    

        <uc1:CtrRepMovimientoDdjj runat="server" ID="CtrRepMovimientoDdjj"/>
    
    
    

</asp:Content>
