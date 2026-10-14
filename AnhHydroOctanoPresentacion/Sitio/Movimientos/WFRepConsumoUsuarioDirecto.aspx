<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio/ANH.Master" AutoEventWireup="true" CodeBehind="WFRepConsumoUsuarioDirecto.aspx.cs" Inherits="AnhHydroOctanoPresentacion.Sitio.Movimientos.WFRepConsumoUsuarioDirecto" %>
<%@ Register Src="~/Controles/CtrRepConsumoUsuarioDirecto.ascx" TagPrefix="uc1" TagName="CtrRepConsumoUsuarioDirecto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">     
    

        <uc1:CtrRepConsumoUsuarioDirecto runat="server" ID="CtrRepConsumoUsuarioDirecto"/>

</asp:Content>
