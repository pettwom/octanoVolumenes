<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio/ANH.Master" AutoEventWireup="true" CodeBehind="WFRepDosifGral.aspx.cs" Inherits="AnhHydroOctanoPresentacion.Sitio.Movimientos.WFRepDosifGral" %>

<%@ Register Src="~/Controles/CtrRepDosifGral.ascx" TagPrefix="uc1" TagName="CtrRepDosifGral" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <uc1:CtrRepDosifGral runat="server" ID="CtrRepDosifGral"/>
</asp:Content>
