<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio/ANH.Master" AutoEventWireup="true" CodeBehind="WFProgramacionProde.aspx.cs" Inherits="AnhHydroOctanoPresentacion.Sitio.Movimientos.WFProgramacionProde" %>

<%@ Register Src="~/Controles/CtrProgramacionProde.ascx" TagPrefix="uc1" TagName="CtrProgramacionProde" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <uc1:CtrProgramacionProde runat="server" ID="CtrProgramacionProde"/>
</asp:Content>

