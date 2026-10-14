<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio/ANH.Master" AutoEventWireup="true" CodeBehind="WFRepSuicDdjj.aspx.cs" Inherits="AnhHydroOctanoPresentacion.Sitio.Movimientos.WFRepSuicDdjj" %>

<%@ Register Src="~/Controles/CtrRepSuicDdjj.ascx" TagPrefix="uc1" TagName="CtrRepSuicDdjj" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <uc1:CtrRepSuicDdjj runat="server" ID="CtrRepSuicDdjj"/>
</asp:Content>
