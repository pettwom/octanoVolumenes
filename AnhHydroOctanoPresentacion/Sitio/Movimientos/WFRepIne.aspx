<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio/ANH.Master" AutoEventWireup="true" CodeBehind="WFRepIne.aspx.cs" Inherits="AnhHydroOctanoPresentacion.Sitio.Movimientos.WFRepIne" %>
<%@ Register Src="~/Controles/CtrRepIne.ascx" TagPrefix="uc1" TagName="CtrRepIne" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<uc1:CtrRepIne runat="server" ID="CtrRepIne"/>

</asp:Content>
