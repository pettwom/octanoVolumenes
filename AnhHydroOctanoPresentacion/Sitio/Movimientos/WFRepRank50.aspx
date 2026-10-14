<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio/ANH.Master" AutoEventWireup="true" CodeBehind="WFRepRank50.aspx.cs" Inherits="AnhHydroOctanoPresentacion.Sitio.Movimientos.WFRepRank50" %>
<%@ Register Src="~/Controles/CtrRepRank50.ascx" TagPrefix="uc1" TagName="CtrRepRank50" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
       <asp:ScriptManager runat="server"></asp:ScriptManager>
    

        <uc1:CtrRepRank50 runat="server" ID="CtrRepRank50"/>
</asp:Content>
