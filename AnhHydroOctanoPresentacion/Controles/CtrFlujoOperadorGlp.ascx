<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtrFlujoOperadorGlp.ascx.cs" Inherits="AnhHydroOctanoPresentacion.Controles.CtrFlujoOperadorGlp" %>

<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>

<script type="text/javascript">
    function buscarPersona() {
        pnlDatosVentaGlp.PerformCallback(1);
        pnlConsumoGlp.PerformCallback();
    }

    function grabarVentaGlp() {
        pnlDatosVentaGlp.PerformCallback(2);
        pnlConsumoGlp.PerformCallback();
    }

    function grabarAsignacionGlp() {                
        pnlDatosAsignacionCamion.PerformCallback(1);        
    }

    function OnChangedHandler(s) {
        pnlDatosAsignacionCamion.PerformCallback(10);
    }

    function OnChangedVentaHandler(s) {
        pnlDatosVentaGlp.PerformCallback(10);
    }

</script>

<br />
<br />
<br />

<div class="container-fluid">    
    <br />
    <br />
    <div class="row">
      <div class="col-xs-12 col-md-6"><button type="button" class="btn btn-primary" data-toggle="modal" data-target="#modalCamion">Despacho de Garrafas en Camiones</button></div>
      <div class="col-xs-12 col-md-6"><button type="button" class="btn btn-primary" data-toggle="modal" data-target="#modalVenta">Venta a los consumidores finales</button></div>      
    </div>
</div>

<div id="modalVenta" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
  <div class="modal-dialog">
    <!-- Modal content-->
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal">&times;</button>
        <h4 class="modal-title">Venta a los consumidores finales</h4>
      </div>
        <div class="modal-body">
            <div class="container-fluid">                
                <div class="panel panel-default">
                    <div class="panel-body">
                        <div class="row">
                            <label class="col-xs-12 col-md-12 control-label">Planta de Distribución de GLP</label>                                 
                        </div>
                        <div class="row">                    
                            <div class="col-xs-12 col-md-7">
                                <dx:ASPxComboBox ID="cmbEntidad" runat="server" ClientInstanceName="cmbEntidad" TextField="DENOMINACION" TextFormatString="{0}" Theme="Metropolis" ValueField="ID_CONSUMIDOR" Width="100%">                                    
                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {  
                                            OnChangedVentaHandler(s);
                                        }" />
                                    <Columns>
                                        <dx:ListBoxColumn Caption="NOMBRE" FieldName="DENOMINACION" Name="DENOMINACION" />
                                        <dx:ListBoxColumn Caption="NIT" FieldName="NIT" Name="NIT" Visible="false" />
                                    </Columns>
                                </dx:ASPxComboBox>
                            </div>                                
                            <label class="col-xs-3 col-md-2 control-label">Placa:</label>                           
                            <div class="col-xs-6 col-md-3"><dx:ASPxTextBox ID="txtPlacaVenta" ClientInstanceName="txtPlacaVenta" runat="server" Width="80%"></dx:ASPxTextBox></div>
                        </div> 
                            
                        <dx:ASPxCallbackPanel ID="pnlDatosVentaGlp" runat="server" OnCallback="pnlDatosVentaGlp_Callback" ClientInstanceName="pnlDatosVentaGlp">
                            <PanelCollection>
                                <dx:PanelContent runat="server">

                                    <div class="row">
                                        <label class="col-xs-12 col-md-12">Operaciones a Realizar</label>                    
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-xs-12 col-md-6 text-center"><asp:RadioButton runat="server" ID="rbBuscarConCi" Checked="true" Text="Venta con CI" GroupName="opcionGlp" /></div>                                        
                                        <div class="col-xs-12 col-md-6 text-center"><asp:RadioButton runat="server" ID="rbBuscarSinCi" Text="Venta sin CI" GroupName="opcionGlp" /></div>                                        
                                    </div>
                                    <br />
                                    <div class="row">
                                        <label class="col-xs-1 col-md-2">C.I. :</label>
                                        <div class="col-xs-11 col-md-4">
                                            <dx:ASPxTextBox ID="txtDocumentoIdentidadGlp" ClientInstanceName="txtDocumentoIdentidadGlp" runat="server" Width="100%">
                                                <ClientSideEvents  
                                                    TextChanged="function(s, e) {  
                                                        OnChangedVentaHandler(s);  
                                                    }" />
                                            </dx:ASPxTextBox>
                                        </div>
                                        <div class="col-xs-12 col-md-4">
                                            <button type="button" class="btn btn-primary btn-sm" onclick="buscarPersona()">Buscar CI</button>
                                        </div>
                                        <div class="col-xs-12 col-md-2"></div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <label class="col-xs-12 col-md-3">Nombre(s) :</label>
                                        <div class="col-xs-12 col-md-9"><dx:ASPxTextBox ID="txtNombres_Glp" ClientInstanceName="txtNombres_Glp" runat="server" Width="90%" Enabled="true"></dx:ASPxTextBox></div>                        
                                    </div>
                                    <div class="row">
                                        <label class="col-xs-12 col-md-3">Apellido Pat.:</label>
                                        <div class="col-xs-12 col-md-9"><dx:ASPxTextBox ID="txtApellidoPat_Glp" ClientInstanceName="txtApellidoPat_Glp" runat="server" Width="90%" Enabled="true"></dx:ASPxTextBox></div>
                                    </div>
                                    <div class="row">
                                        <label class="col-xs-12 col-md-3">Apellido Mat.:</label>
                                        <div class="col-xs-12 col-md-9"><dx:ASPxTextBox ID="txtApellidoMaterno_Glp" ClientInstanceName="txtApellidoMaterno_Glp" runat="server" Width="90%" Enabled="true"></dx:ASPxTextBox></div>
                                    </div> 
                                    <br />
                                    <div class="row">
                                        <label class="col-xs-6 col-md-2">Precio Unitario:</label>
                                        <div class="col-xs-6 col-md-2">
                                            <dx:ASPxTextBox runat="server" ID="txtPrecioUnitariolGlp" ClientInstanceName="txtPrecioUnitariolGlp" Width="90%" ReadOnly="true" Text="22.50" >
                                            </dx:ASPxTextBox>
                                        </div>                                    
                                        <label class="col-xs-6 col-md-2">Cantidad:</label>
                                        <div class="col-xs-6 col-md-2">
                                            <dx:ASPxTextBox runat="server" Width="70px" ID="txtCantidad_Glp" ClientInstanceName="txtCantidad_Glp">
                                                <MaskSettings Mask="&lt;0..99&gt;" />
                                                <ClientSideEvents  LostFocus ="function(s, e) {                                                
                                                            var num1 = parseFloat(txtCantidad_Glp.GetText(), 10);                                                                                    
                                                            txtPrecioTotalGlp.SetText((num1*22.5));                                                
                                                        }"
                                                        TextChanged="function(s, e) {  
                                                            OnChangedVentaHandler(s);  
                                                        }"
                                                             />                                    
                                            </dx:ASPxTextBox>
                                        </div>                                    
                                        <label class="col-xs-6 col-md-2">Precio Total:</label>
                                        <div class="col-xs-6 col-md-2">
                                            <dx:ASPxTextBox ID="txtPrecioTotalGlp" runat="server" ClientInstanceName="txtPrecioTotalGlp" ReadOnly="true" Width="90%">
                                            </dx:ASPxTextBox>
                                        </div>
                                    </div>
                                    <br />
                                    <dx:ASPxRoundPanel ID="pnlMensajeGrabar" runat="server" ShowHeader="False" Visible="False" Width="100%">
                                        <PanelCollection>
                                            <dx:PanelContent runat="server">                                                
                                                <dx:ASPxLabel ID="lblMensajeErrorGrabar" runat="server" ForeColor="#666666">
                                                </dx:ASPxLabel>
                                            </dx:PanelContent>
                                        </PanelCollection>
                                        <Border BorderStyle="Ridge" BorderWidth="1px" />
                                    </dx:ASPxRoundPanel>

                                    <button type="button" class="btn btn-primary" onclick="grabarVentaGlp()">Guardar registro</button>
                                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxCallbackPanel>

                        <dx:ASPxCallbackPanel ID="pnlConsumoGlp" runat="server" OnCallback="pnlConsumoGlp_Callback" ClientInstanceName="pnlConsumoGlp">
                            <PanelCollection>
                                <dx:PanelContent runat="server">
                                    <div class="panel panel-default">
                                        <div class="panel-body">                                                                        
                                            <br />
                                            <div class="row">
                                                <label class="col-xs-12 col-md-6">Compras del último mes</label>                    
                                                <label class="col-xs-12 col-md-4 text-right">Ventas del Consumidor :</label>
                                                <div class="col-xs-12 col-md-2"><dx:ASPxTextBox runat="server" Width="90%" ID="txtConsumoUsuario" ClientInstanceName="txtConsumoUsuario"></dx:ASPxTextBox></div>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-xs-12 col-md-12">
                                                    <dx:ASPxGridView ID="pvgDato" runat="server" AutoGenerateColumns="False" ClientInstanceName="pvgDato" EnableTheming="True" Font-Size="Smaller" KeyFieldName="IntId" Theme="Default" Width="100%">
                                                        <Styles>
                                                            <Header Wrap="True" HorizontalAlign="Center"></Header>
                                                        </Styles>                                                                        
                                                        <SettingsPager PageSize="100">
                                                            <PageSizeItemSettings Items="50, 100, 200, 500, 1000" Visible="True">
                                                            </PageSizeItemSettings>
                                                        </SettingsPager>                   
                                                        <Columns>                                                                                                                                                                              
                                                            <dx:GridViewDataTextColumn Caption="IntId" FieldName="ID_MOVIMIENTO" ShowInCustomizationForm="True" Visible="False" ToolTip="Identificador del Movimiento" VisibleIndex="0">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Fecha Hora" FieldName="FECHA_OPERACION" ShowInCustomizationForm="True" Visible="True" SortOrder="Descending" ToolTip="Fecha del Movimiento" VisibleIndex="1">
                                                            </dx:GridViewDataTextColumn>                                                                                        
                                                            <dx:GridViewDataTextColumn Caption="Fecha" FieldName="FECHA_OPERACION_DIA" ShowInCustomizationForm="True" Visible="False" ToolTip="Fecha entera del Movimiento" VisibleIndex="2">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Precio Unitario" FieldName="PRECIO_UNITARIO" ShowInCustomizationForm="True" Visible="False" ToolTip="Precio Unitario" VisibleIndex="3">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Cantidad" FieldName="CANTIDAD" ShowInCustomizationForm="True" Visible="True" ToolTip="Cantidad" VisibleIndex="4">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="Precio Total" FieldName="PRECIO_TOTAL" ShowInCustomizationForm="True" Visible="False" ToolTip="Precio Total" VisibleIndex="5">
                                                            </dx:GridViewDataTextColumn>                                                                                        
                                                        </Columns>
                                                        <Styles CssPostfix="DevGlp"><AlternatingRow BackColor="#EEEEEE"></AlternatingRow></Styles>                                                                                    
                                                    </dx:ASPxGridView>
                                                </div>                    
                                            </div>                                            
                                        </div>
                                    </div>
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxCallbackPanel>
                    </div>
                </div> 
            </div>        
        </div>
      <div class="modal-footer">
        
      </div>
    </div>
  </div>
</div>

<div id="modalCamion" class="modal fade" role="dialog">
  <div class="modal-dialog">

    <!-- Modal content-->
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal">&times;</button>
        <h4 class="modal-title">Carga de garrafas a los camiones</h4>
      </div>
      <div class="modal-body">
            <div class="container-fluid">               

                <div class="panel panel-default">
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-xs-12 col-md-12">Planta de Distribución de GLP</div>                            
                        </div>
                        <div class="row">                    
                            <div class="col-xs-12 col-md-12">
                                <dx:ASPxComboBox ID="cmbPlanta" runat="server" ClientInstanceName="cmbPlanta" TextField="DENOMINACION" TextFormatString="{0}" Theme="Metropolis" ValueField="ID_CONSUMIDOR" Width="80%">                                    
                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {  
                                            OnChangedHandler(s);
                                        }" />
                                    <Columns>
                                        <dx:ListBoxColumn Caption="NOMBRE" FieldName="DENOMINACION" Name="DENOMINACION" />
                                        <dx:ListBoxColumn Caption="NIT" FieldName="NIT" Name="NIT" Visible="false" />
                                    </Columns>
                                </dx:ASPxComboBox>
                            </div>                    
                        </div> 
                        <br />
                                    
                        <dx:ASPxCallbackPanel ID="pnlDatosAsignacionCamion" runat="server" OnCallback="pnlDatosAsignacionCamion_Callback" ClientInstanceName="pnlDatosAsignacionCamion">
                            <PanelCollection>
                                <dx:PanelContent runat="server">

                                    <div class="row">
                                        <div class="col-xs-6 col-md-2">Fecha:</div>
                                        <div class="col-xs-6 col-md-6"><dx:ASPxTextBox runat="server" Width="80%" ID="txtFechaAsignacionCamion" Enabled="false"></dx:ASPxTextBox></div>                            
                                        <div class="col-xs-12 col-md-4"></div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-xs-6 col-md-2">Placa:</div>
                                        <div class="col-xs-6 col-md-4">
                                            <dx:ASPxTextBox runat="server" Width="80%" ID="txtPlacaCamionAsignacion" ClientInstanceName="txtPlacaCamionAsignacion" MaxLengt="7">
                                                <ClientSideEvents  
                                                    TextChanged="function(s, e) {  
                                                        s.SetText(s.GetText().toUpperCase());
                                                        OnChangedHandler(s);  
                                                    }"  />
                                            </dx:ASPxTextBox>
                                        </div>
                                        <div class="col-xs-6 col-md-2">Cantidad:</div>
                                        <div class="col-xs-6 col-md-4">
                                            <dx:ASPxTextBox runat="server" Width="100px" ID="txtCantidadCamionAsignacion" ClientInstanceName="txtCantidadCamionAsignacion" MaxLength="4" >
                                                <MaskSettings Mask="&lt;0..9999&gt;" />
                                                <ClientSideEvents  
                                                    TextChanged="function(s, e) {  
                                                        OnChangedHandler(s);  
                                                    }" />
                                            </dx:ASPxTextBox>
                                        </div>
                                    </div>
                                    <br />
                                    <dx:ASPxRoundPanel ID="pnlMensaje" runat="server" ShowHeader="False" Visible="False" Width="100%">
                                        <PanelCollection>
                                            <dx:PanelContent runat="server">                                                
                                                <dx:ASPxLabel ID="lblMensajeError" runat="server" ForeColor="#666666">
                                                </dx:ASPxLabel>
                                            </dx:PanelContent>
                                        </PanelCollection>
                                        <Border BorderStyle="Ridge" BorderWidth="1px" />
                                    </dx:ASPxRoundPanel>

                                    <br />
                                    <button type="button" class="btn btn-primary" onclick="grabarAsignacionGlp()">Guardar registro</button>
                                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                                            
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxCallbackPanel>                
                    </div>
                </div>     
            </div>
      </div>
      <div class="modal-footer">
        
      </div>
    </div>
  </div>
</div>






