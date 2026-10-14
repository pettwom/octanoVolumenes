<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio/ANH.Master" AutoEventWireup="true" CodeBehind="WFLogErrores.aspx.cs" Inherits="AnhHydroOctanoPresentacion.Sitio.Movimientos.WFLogErrores" %>

<%@ Register Src="~/Controles/CtrLogErrores.ascx" TagPrefix="uc1" TagName="CtrLogErrores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <uc1:CtrLogErrores runat="server" ID="CtrLogErrores"/>
</asp:Content>
