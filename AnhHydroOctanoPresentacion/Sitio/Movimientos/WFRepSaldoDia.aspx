<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio/ANH.Master" AutoEventWireup="true" CodeBehind="WFRepSaldoDia.aspx.cs" Inherits="AnhHydroOctanoPresentacion.Sitio.Movimientos.WFRepSaldoDia" %>

<%@ Register Src="~/Controles/CtrRepSaldoDia.ascx" TagPrefix="uc1" TagName="CtrRepSaldoDia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      <asp:ScriptManager runat="server"></asp:ScriptManager>
      <uc1:CtrRepSaldoDia runat="server" ID="CtrRepSaldoDia"/>
</asp:Content>
