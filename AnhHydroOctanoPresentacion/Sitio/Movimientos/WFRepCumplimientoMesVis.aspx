<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio/ANH.Master" AutoEventWireup="true" CodeBehind="WFRepCumplimientoMesVis.aspx.cs" Inherits="AnhHydroOctanoPresentacion.Sitio.Movimientos.WFRepCumplimientoMesVis" %>

<%@ Register Src="~/Controles/CtrRepCumplimientoMesVis.ascx" TagPrefix="uc1" TagName="CtrRepCumplimientoMesVis" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
       <asp:ScriptManager runat="server"></asp:ScriptManager>
    
       <uc1:CtrRepCumplimientoMesVis runat="server" ID="CtrRepCumplimientoMesVis"/>

</asp:Content>
