<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio/ANH.Master" AutoEventWireup="true" CodeBehind="WFComparativoInv.aspx.cs" Inherits="AnhHydroOctanoPresentacion.Sitio.Movimientos.WFComparativoInv" %>

<%@ Register Src="~/Controles/CtrComparativoInv.ascx" TagPrefix="uc1" TagName="CtrComparativoInv" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
       <asp:ScriptManager runat="server"></asp:ScriptManager>
    

        <uc1:CtrComparativoInv runat="server" ID="CtrComparativoInv"/>
    
    
</asp:Content>
