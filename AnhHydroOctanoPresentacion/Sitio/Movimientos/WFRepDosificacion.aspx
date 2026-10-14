<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio/ANH.Master" AutoEventWireup="true" CodeBehind="WFRepDosificacion.aspx.cs" Inherits="AnhHydroOctanoPresentacion.Sitio.Movimientos.WFRepDosificacion" %>

<%@ Register Src="~/Controles/CtrRepDosificacion.ascx" TagPrefix="uc1" TagName="CtrRepDosificacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <uc1:CtrRepDosificacion runat="server" ID="CtrRepDosificacion"/>
</asp:Content>
