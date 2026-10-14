<%--<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFDashbOctVol.aspx.cs" Inherits="AnhHydroOctanoPresentacion.Sitio.Movimientos.WFDashbOctVol" %>--%>
<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio/ANH.Master" AutoEventWireup="true" CodeBehind="WFDashbOctVol.aspx.cs" Inherits="AnhHydroOctanoPresentacion.Sitio.Movimientos.WFDashbOctVol" %>

<%@ Register Src="~/Controles/CtrDashbOctVol.ascx" TagPrefix="uc1" TagName="CtrDashbOctVol" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
       <%--<asp:ScriptManager runat="server"></asp:ScriptManager>--%>
    
       <uc1:CtrDashbOctVol runat="server" ID="CtrDashbOctVol"/>

</asp:Content>

