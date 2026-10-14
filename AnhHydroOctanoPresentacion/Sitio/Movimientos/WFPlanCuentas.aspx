<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio/ANH.Master" AutoEventWireup="true" CodeBehind="WFPlanCuentas.aspx.cs" Inherits="AnhHydroOctanoPresentacion.Sitio.Movimientos.WFPlanCuentas" %>

<%@ Register Src="~/Controles/CtrPlanCuentas.ascx" TagPrefix="uc1" TagName="CtrPlanCuentas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
       <asp:ScriptManager runat="server"></asp:ScriptManager>
    
       <uc1:CtrPlanCuentas runat="server" ID="CtrPlanCuentas"/>

</asp:Content>
