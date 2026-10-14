<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio/ANH.Master" AutoEventWireup="true" CodeBehind="WFManual.aspx.cs" Inherits="AnhHydroOctanoPresentacion.Sitio.Movimientos.WFManual" %>

<%@ Register Src="~/Controles/CtrManual.ascx" TagPrefix="uc1" TagName="CtrManual" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
       <asp:ScriptManager runat="server"></asp:ScriptManager>
    
       <uc1:CtrManual runat="server" ID="CtrManual"/>

</asp:Content>
