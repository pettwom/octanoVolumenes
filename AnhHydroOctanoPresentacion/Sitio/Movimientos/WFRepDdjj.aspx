<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio/ANH.Master" AutoEventWireup="true" CodeBehind="WFRepDdjj.aspx.cs" Inherits="AnhHydroOctanoPresentacion.Sitio.Movimientos.WFRepDdjj" %>

<%@ Register Src="~/Controles/CtrRepDdjj.ascx" TagPrefix="uc1" TagName="CtrRepDdjj" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
       <asp:ScriptManager runat="server"></asp:ScriptManager>
    

        <uc1:CtrRepDdjj runat="server" ID="CtrRepDdjj"/>
    
    
    

</asp:Content>
