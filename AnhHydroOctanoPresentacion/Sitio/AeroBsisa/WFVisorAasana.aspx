<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio/ANH.Master" AutoEventWireup="true" CodeBehind="WFVisorAasana.aspx.cs" Inherits="AnhHydroOctanoPresentacion.Sitio.AeroBsisa.WFVisorAasana" %>

<%@ Register Src="~/Controles/CtrVisorAasana.ascx" TagPrefix="uc1" TagName="CtrVisorAasana" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
       <asp:ScriptManager runat="server"></asp:ScriptManager>
    
        <uc1:CtrVisorAasana runat="server" ID="CtrVisorAasana"/>
    
</asp:Content>
