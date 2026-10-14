<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio/ANH.Master" AutoEventWireup="true" CodeBehind="WFRepConsumo.aspx.cs" Inherits="AnhHydroOctanoPresentacion.Sitio.Movimientos.WFRepConsumo" %>

<%@ Register Src="~/Controles/CtrRepConsumo.ascx" TagPrefix="uc1" TagName="CtrRepConsumo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
       <asp:ScriptManager runat="server"></asp:ScriptManager>
    

        <uc1:CtrRepConsumo runat="server" ID="CtrRepConsumo"/>
    
    
    

</asp:Content>
