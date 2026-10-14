<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio/ANH.Master" AutoEventWireup="true" CodeBehind="WFVisorAeronave.aspx.cs" Inherits="AnhHydroOctanoPresentacion.Sitio.AeroBsisa.WFVisorAeronave" %>

<%@ Register Src="~/Controles/CtrVisorAeronave.ascx" TagPrefix="uc1" TagName="CtrVisorAeronave" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
       <asp:ScriptManager runat="server"></asp:ScriptManager>
    
        <uc1:CtrVisorAeronave runat="server" ID="CtrVisorAeronave"/>
    
</asp:Content>
