<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio/ANH.Master" AutoEventWireup="true" CodeBehind="WFRepCumplRecepDiaVis.aspx.cs" Inherits="AnhHydroOctanoPresentacion.Sitio.Movimientos.WFRepCumplRecepDiaVis" %>

<%@ Register Src="~/Controles/CtrRepCumplRecepDiaVis.ascx" TagPrefix="uc1" TagName="CtrRepCumplRecepDiaVis" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <asp:ScriptManager runat="server"></asp:ScriptManager>    
       <uc1:CtrRepCumplRecepDiaVis runat="server" ID="CtrRepCumplRecepDiaVis"/>
</asp:Content>
