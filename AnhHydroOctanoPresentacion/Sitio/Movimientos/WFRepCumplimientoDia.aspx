<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio/ANH.Master" AutoEventWireup="true" CodeBehind="WFRepCumplimientoDia.aspx.cs" Inherits="AnhHydroOctanoPresentacion.Sitio.Movimientos.WFRepCumplimientoDia" %>

<%@ Register Src="~/Controles/CtrRepCumplimientoDia.ascx" TagPrefix="uc1" TagName="CtrRepCumplimientoDia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
       <asp:ScriptManager runat="server"></asp:ScriptManager>
    
       <uc1:CtrRepCumplimientoDia runat="server" ID="CtrRepCumplimientoDia"/>

</asp:Content>
