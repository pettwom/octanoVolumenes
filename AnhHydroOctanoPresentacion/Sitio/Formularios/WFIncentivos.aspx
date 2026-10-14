<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio/ANH.Master" AutoEventWireup="true" CodeBehind="WFIncentivos.aspx.cs" Inherits="AnhHydroOctanoPresentacion.Sitio.Formularios.WFIncentivos" %>

<%@ Register Src="~/Controles/CtrIncentivos.ascx" TagPrefix="uc1" TagName="CtrIncentivos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
       <asp:ScriptManager runat="server"></asp:ScriptManager>
    
       <uc1:CtrIncentivos runat="server" ID="CtrIncentivos"/>

</asp:Content>
