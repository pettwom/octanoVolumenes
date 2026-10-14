<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio/ANH.Master" AutoEventWireup="true" CodeBehind="WFVisorPahl.aspx.cs" Inherits="AnhHydroOctanoPresentacion.Sitio.PAHL.WFVisorPahl" %>
<%@ Register Src="~/Controles/CtrVisorPahl.ascx" TagPrefix="uc1" TagName="CtrVisorPahl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:CtrVisorPahl runat="server" ID="CtrVisorPahl" />
</asp:Content>
