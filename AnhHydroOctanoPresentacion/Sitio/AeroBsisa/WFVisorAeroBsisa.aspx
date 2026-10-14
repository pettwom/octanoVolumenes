<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio/ANH.Master" AutoEventWireup="true" CodeBehind="WFVisorAeroBsisa.aspx.cs" Inherits="AnhHydroOctanoPresentacion.Sitio.AeroBsisa.WFVisorAeroBsisa" %>

<%@ Register Src="~/Controles/CtrVisorAeroBsisa.ascx" TagPrefix="uc1" TagName="CtrVisorAeroBsisa" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
       <asp:ScriptManager runat="server"></asp:ScriptManager>
    
        <uc1:CtrVisorAeroBsisa runat="server" ID="CtrVisorAeroBsisa"/>
    
</asp:Content>
