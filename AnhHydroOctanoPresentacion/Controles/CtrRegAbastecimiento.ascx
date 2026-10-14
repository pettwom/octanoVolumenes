<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtrRepMovimientoSaldo.ascx.cs" Inherits="AnhHydroOctanoPresentacion.Controles.CtrRepMovimientoSaldo" %>
<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%--<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>--%>

<style type="text/css">
    .hr-text {
      line-height: 1em;
      position: relative;
      outline: 0;
      border: 0;
      color: black;
      text-align: center;
      height: 1.5em;
      opacity: 0.5;
    }
    .hr-text:before {
      content: "";
      background: linear-gradient(to right, transparent, #818078, transparent);
      position: absolute;
      left: 0;
      top: 50%;
      width: 100%;
      height: 1px;
    }
    .hr-text:after {
      content: attr(data-content);
      position: relative;
      display: inline-block;
      color: black;
      padding: 0 0.5em;
      line-height: 1.5em;
      color: black;
      background-color: #fcfcfa;
    }
    .panelPersona
    {
        position: fixed;
        top: 410px;
        width: 500px;
        background: white;
    }
    .hydro_smart_container
    {
        margin: 0;
        padding: 0;
        Height: 690px;
    }
    .hydro_smart_header
    {
        margin: 0;
        padding: 5px 10px;
        /*background: orange;*/
        background: rgb(0, 132, 38);
        color: white;
        font-size: medium;
        font-weight: bold;
    }
    .hydro_smart_table
    {
        font-size: smaller;
        width: 95%;
        margin: auto;
    }
    .hydro_smart_info {
        /*border: #FFCE4F solid thin;*/
        border: #1d9915 solid thin;
        margin: 5px;
        /*background: #FFF8F0;*/
        background: #f0fff8;
        -ms-border-radius: 3px;
        border-radius: 3px;
    }
    .alert-box {
    color:#555;
    border-radius:10px;
    font-family:Tahoma,Geneva,Arial,sans-serif;font-size:11px;
    padding:10px 10px 10px 36px;
    margin:10px;
}
    
    </style>

<script type="text/javascript">
    function numbersonly(e) {
        var unicode = e.charCode ? e.charCode : e.keyCode
        if (unicode != 8 && unicode != 44) {
            if (unicode < 48 || unicode > 57) //if not a number
            { return false } //disable key press    
        }
    }

</script>

<div class="col-md-12">

    <table style="width: 100%; height: 100%">
        <tr>
            <td style="text-align: left; padding-bottom: 1px">
                <dx:ASPxLabel runat="server" ForeColor="#367B2F" ID="lblTitulo" Font-Bold="True" Font-Names="Rockwell" Font-Size="Larger" Text="REGISTRO DE ABASTECIMIENTO EN TIEMPO REAL"></dx:ASPxLabel>
            </td>
        </tr>

        <tr>            
            <td style="text-align: center; padding-bottom: 1px">
                <table>
                    <tr>
                        <td>
                            <asp:panel id="Panel3" runat="server" visible="false">
                <table> 
                                       
                    <tr style="text-align: right;">
                        <td>Fecha de la Medición :
                        </td>
                        <td style="width: 136px;">
                            <dx:ASPxDateEdit ID="detFechaSalFisico" runat="server" ReadOnly="true" ClientInstanceName="detFechaDocRegistro" EnableTheming="True" Height="23px" Theme="Metropolis" ToolTip="Seleccione la fecha Documento" Font-Bold="True" Width="90px">
                                <TimeSectionProperties>
                                    <TimeEditProperties>
                                        <ClearButton Visibility="Auto">
                                        </ClearButton>
                                    </TimeEditProperties>
                                </TimeSectionProperties>
                                <ClearButton Visibility="Auto">
                                </ClearButton>
                            </dx:ASPxDateEdit>                            
                        </td>                     
                    </tr>  
                    <tr>
                        <td colspan="2">
                            Medicion de saldo en tiempo real
                        </td>
                    </tr>      
                  <%--  <tr>
                        <td style="text-align: right;"> <span style="color:maroon; font-weight:bold;">Los saldos consignados corresponden a Hrs.:</span>                                                    
                        </td>
                        <td >
                                 <dx:ASPxTimeEdit ID="tedHoraSaldo" runat="server" EditFormatString="HH:mm" Width="90px"></dx:ASPxTimeEdit>            
                        </td>                     
                    </tr>  --%> 
                    <tr>                       
                        <td colspan="2">                      
                            <dx:ASPxGridView ID="dgvProductoRecep" runat="server" AutoGenerateColumns="False" ClientInstanceName="dgvProductoRecep" EnableTheming="False" Font-Size="Smaller" KeyFieldName="ID_IDENTIFICADOR" Theme="Default" Width="100%">
                              <%--  <ClientSideEvents CustomButtonClick="function(s, e) {
                                switch(e.buttonID) {

                                case&nbsp; 'btnHidQuitar':
                                e.processOnServer = confirm('¿Desea quitar el registro del hidrocarburo?'); 
                                break;

                                } 
                                }" />--%>
                                <SettingsContextMenu EnableRowMenu="False">
                                </SettingsContextMenu>
                                <Templates>
                                    <GroupFooterRow>
                                        <dx:ASPxLabel ID="ASPxLabel54" runat="server" Text="ASPxLabel">
                                        </dx:ASPxLabel>
                                    </GroupFooterRow>
                                    <EditForm>
                                    </EditForm>
                                </Templates>
                                <SettingsPager NumericButtonCount="5" PageSize="100">
                                    <PageSizeItemSettings Items="50, 100, 200, 500, 1000" Visible="False">
                                    </PageSizeItemSettings>
                                </SettingsPager>
                                <SettingsEditing Mode="Inline">
                                </SettingsEditing>
                               <%-- <Settings ShowHeaderFilterButton="False" />--%>
                               <%-- <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="False" />--%>
                                <SettingsPopup>
                                    <EditForm HorizontalAlign="Center" Modal="True" VerticalAlign="Middle" VerticalOffset="0" />
                                    <CustomizationWindow HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" />
                                </SettingsPopup>
                                <SettingsText CommandNew="Nuevo" />
                                <Columns>
                        
                                    <dx:GridViewDataTextColumn Caption="ID_AUTORIZACION_CONDICION" FieldName="ID_AUTORIZACION_CONDICION" ShowInCustomizationForm="True" VisibleIndex="13" Visible="False">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="ID_IDENTIFICADOR" FieldName="ID_IDENTIFICADOR" ShowInCustomizationForm="True" VisibleIndex="15" Visible="False">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="PRODUCTO" FieldName="NOMBRE" ShowInCustomizationForm="True" VisibleIndex="16">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="UNIDAD MEDIDA" FieldName="SUBID_NOMBRE" ShowInCustomizationForm="True" VisibleIndex="18">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="CANTIDAD" FieldName="CODIGO" ShowInCustomizationForm="True" VisibleIndex="19" ToolTip="Cantidad">
                                        <DataItemTemplate>
                                            <dx:ASPxTextBox ID="txtCantidad" runat="server" Text='<%# Eval("CODIGO") %>' Width="90px">
                                                <Border BorderStyle="None" />
                                                <MaskSettings IncludeLiterals="DecimalSymbol" Mask="&lt;0..999999999&gt;.&lt;000000..999999&gt;" />
                                                                <ValidationSettings>
                                                                    <RequiredField ErrorText="ERROR: Introduzca la cantidad." IsRequired="True" />
                                                                </ValidationSettings>
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                            </dx:ASPxTextBox>
                                        </DataItemTemplate>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="OBSERV." FieldName="OBSERVACIONES" ShowInCustomizationForm="True" VisibleIndex="20" ToolTip="Observaciones">
                                        <DataItemTemplate>
                                            <dx:ASPxTextBox ID="txtObservacion" runat="server" Text='<%# Eval("OBSERVACIONES") %>' Width="100px">
                                                <Border BorderStyle="None" />
                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                </ReadOnlyStyle>
                                                <FocusedStyle BackColor="#FFFFCC">
                                                </FocusedStyle>
                                                <DisabledStyle BackColor="#DEE8DB">
                                                </DisabledStyle>
                                            </dx:ASPxTextBox>
                                        </DataItemTemplate>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="ESTADO" FieldName="ESTADO" ShowInCustomizationForm="True" Visible="False" VisibleIndex="21">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="ID_SUB_IDENTIFICADOR" FieldName="ID_SUB_IDENTIFICADOR" ShowInCustomizationForm="True" Visible="False" VisibleIndex="17">
                                    </dx:GridViewDataTextColumn>
                                </Columns>
                                <Styles CssPostfix="DevGlp">
                                    <AlternatingRow BackColor="#EEEEEE">
                                    </AlternatingRow>
                                </Styles>
                                <TotalSummary>
                                    <dx:ASPxSummaryItem FieldName="F_IMPORTE" ShowInColumn="IMPORTE" SummaryType="Custom" Tag="imp" />
                                    <dx:ASPxSummaryItem FieldName="VOLUMEN" ShowInColumn="CANTIDAD" SummaryType="Custom" Tag="vol" />
                                    <dx:ASPxSummaryItem FieldName="STR_ESTADO" ShowInColumn="EST." SummaryType="Custom" Tag="est" />
                                </TotalSummary>
                            </dx:ASPxGridView>
                          
                        </td>
                    </tr>
                   <tr>
                       <td> 
                          Estado de la estacion:
                       </td>
                       <td>
                           <asp:RadioButtonList ID="rblEstado" runat="server">
                            <asp:ListItem Text="Cerrado" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Abierto" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Mantenimiento" Value="3"></asp:ListItem>
                        </asp:RadioButtonList>
                        </td>                       
                    </tr>

                    <tr>
                       <td> 
                          Volumen programado:
                       </td>
                       <td>
                           <asp:RadioButtonList ID="RadioButtonList1" runat="server">
                            <asp:ListItem Text="En planta" Value="1"></asp:ListItem>
                            <asp:ListItem Text="En camino" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Sin programación" Value="3"></asp:ListItem>
                        </asp:RadioButtonList>
                        </td>  
                    </tr>
                </table>
                                <table>                    
                    <tr>
                     
                        <td>
                                               
                            &nbsp;</td>                     
                    </tr>        
                
                    <tr>                       
                        <td>                      
                            <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" ClientInstanceName="dgvProductoRecep" EnableTheming="False" Font-Size="Smaller" KeyFieldName="ID_IDENTIFICADOR" Theme="Default" Width="100%">
                              <%--  <ClientSideEvents CustomButtonClick="function(s, e) {
                                switch(e.buttonID) {

                                case&nbsp; 'btnHidQuitar':
                                e.processOnServer = confirm('¿Desea quitar el registro del hidrocarburo?'); 
                                break;

                                } 
                                }" />--%>
                                <SettingsContextMenu EnableRowMenu="False">
                                </SettingsContextMenu>
                                <Templates>
                                    <GroupFooterRow>
                                        <dx:ASPxLabel ID="ASPxLabel54" runat="server" Text="ASPxLabel">
                                        </dx:ASPxLabel>
                                    </GroupFooterRow>
                                    <EditForm>
                                    </EditForm>
                                </Templates>
                                <SettingsPager NumericButtonCount="5" PageSize="100">
                                    <PageSizeItemSettings Items="50, 100, 200, 500, 1000" Visible="False">
                                    </PageSizeItemSettings>
                                </SettingsPager>
                                <SettingsEditing Mode="Inline">
                                </SettingsEditing>
                               <%-- <Settings ShowHeaderFilterButton="False" />--%>
                               <%-- <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="False" />--%>
                                <SettingsPopup>
                                    <EditForm HorizontalAlign="Center" Modal="True" VerticalAlign="Middle" VerticalOffset="0" />
                                    <CustomizationWindow HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" />
                                </SettingsPopup>
                                <SettingsText CommandNew="Nuevo" />
                                <Columns>
                        
                                    <dx:GridViewDataTextColumn Caption="ID_AUTORIZACION_CONDICION" FieldName="ID_AUTORIZACION_CONDICION" ShowInCustomizationForm="True" VisibleIndex="13" Visible="False">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="ID_IDENTIFICADOR" FieldName="ID_IDENTIFICADOR" ShowInCustomizationForm="True" VisibleIndex="15" Visible="False">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="PRODUCTO" FieldName="NOMBRE" ShowInCustomizationForm="True" VisibleIndex="16">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="UNIDAD MEDIDA" FieldName="SUBID_NOMBRE" ShowInCustomizationForm="True" VisibleIndex="18">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="CANTIDAD" FieldName="CODIGO" ShowInCustomizationForm="True" VisibleIndex="19" ToolTip="Cantidad">
                                        <DataItemTemplate>
                                            <dx:ASPxTextBox ID="txtCantidad" runat="server" Text='<%# Eval("CODIGO") %>' Width="90px">
                                                <Border BorderStyle="None" />
                                                <MaskSettings IncludeLiterals="DecimalSymbol" Mask="&lt;0..999999999&gt;.&lt;000000..999999&gt;" />
                                                                <ValidationSettings>
                                                                    <RequiredField ErrorText="ERROR: Introduzca la cantidad." IsRequired="True" />
                                                                </ValidationSettings>
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                            </dx:ASPxTextBox>
                                        </DataItemTemplate>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="OBSERV." FieldName="OBSERVACIONES" ShowInCustomizationForm="True" VisibleIndex="20" ToolTip="Observaciones">
                                        <DataItemTemplate>
                                            <dx:ASPxTextBox ID="txtObservacion" runat="server" Text='<%# Eval("OBSERVACIONES") %>' Width="100px">
                                                <Border BorderStyle="None" />
                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                </ReadOnlyStyle>
                                                <FocusedStyle BackColor="#FFFFCC">
                                                </FocusedStyle>
                                                <DisabledStyle BackColor="#DEE8DB">
                                                </DisabledStyle>
                                            </dx:ASPxTextBox>
                                        </DataItemTemplate>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="ESTADO" FieldName="ESTADO" ShowInCustomizationForm="True" Visible="False" VisibleIndex="21">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="ID_SUB_IDENTIFICADOR" FieldName="ID_SUB_IDENTIFICADOR" ShowInCustomizationForm="True" Visible="False" VisibleIndex="17">
                                    </dx:GridViewDataTextColumn>
                                </Columns>
                                <Styles CssPostfix="DevGlp">
                                    <AlternatingRow BackColor="#EEEEEE">
                                    </AlternatingRow>
                                </Styles>
                                <TotalSummary>
                                    <dx:ASPxSummaryItem FieldName="F_IMPORTE" ShowInColumn="IMPORTE" SummaryType="Custom" Tag="imp" />
                                    <dx:ASPxSummaryItem FieldName="VOLUMEN" ShowInColumn="CANTIDAD" SummaryType="Custom" Tag="vol" />
                                    <dx:ASPxSummaryItem FieldName="STR_ESTADO" ShowInColumn="EST." SummaryType="Custom" Tag="est" />
                                </TotalSummary>
                            </dx:ASPxGridView>
                          
                        </td>
                    </tr>                

                
                </table>
                   <br />
                            <dx:ASPxButton ID="btnGuardarRegistro" runat="server" Text="Guardar Declaracion Saldo Fisico(Medicion)" Theme="Metropolis" Width="200px" AutoPostBack="False"  Font-Bold="True" ForeColor="Maroon">
                                <ClientSideEvents Click="function(s, e) {
                                                                        if(confirm('¿Desea registrar saldo fisico(Medicion)?'))
                                                                        {
                                                                        LoadingPanel.Show();
                                                                        e.processOnServer = true; 
                                                                        }
                                                                        else{
                                                                        e.processOnServer = false; 
                                                                        }	
                                                                        }" />
                                <Image IconID="actions_apply_16x16">
                                </Image>
                            </dx:ASPxButton>
            </asp:panel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
       
    </table>

</div>
