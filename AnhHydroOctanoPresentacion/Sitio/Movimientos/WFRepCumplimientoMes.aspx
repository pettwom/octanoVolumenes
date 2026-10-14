<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio/ANH.Master" AutoEventWireup="true" CodeBehind="WFRepCumplimientoMes.aspx.cs" Inherits="AnhHydroOctanoPresentacion.Sitio.Movimientos.WFRepCumplimientoMes" %>

<%@ Register Src="~/Controles/CtrRepCumplimientoMes.ascx" TagPrefix="uc1" TagName="CtrRepCumplimientoMes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
       <asp:ScriptManager runat="server"></asp:ScriptManager>
    
       <uc1:CtrRepCumplimientoMes runat="server" ID="CtrRepCumplimientoMes"/>

</asp:Content>
