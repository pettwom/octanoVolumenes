<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio/ANH.Master" AutoEventWireup="true" CodeBehind="WFRecepciones.aspx.cs" Inherits="AnhHydroOctanoPresentacion.Sitio.Movimientos.WFRecepciones" %>

<%@ Register Src="~/Controles/CtrRecepciones.ascx" TagPrefix="uc1" TagName="CtrRecepciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
       <asp:ScriptManager runat="server"></asp:ScriptManager>
    
       <uc1:CtrRecepciones runat="server" ID="CtrRecepciones"/>

</asp:Content>
