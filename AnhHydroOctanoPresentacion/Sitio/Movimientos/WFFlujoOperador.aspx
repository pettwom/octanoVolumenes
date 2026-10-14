<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio/ANH.Master" AutoEventWireup="true" CodeBehind="WFFlujoOperador.aspx.cs" Inherits="AnhHydroOctanoPresentacion.Sitio.Movimientos.WFFlujoOperador" %>

<%@ Register Src="~/Controles/CtrFlujoOperador.ascx" TagPrefix="uc1" TagName="CtrFlujoOperador" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
       <asp:ScriptManager runat="server"></asp:ScriptManager>
    

        <uc1:CtrFlujoOperador runat="server" ID="CtrFlujoOperador"/>
    
    
</asp:Content>
