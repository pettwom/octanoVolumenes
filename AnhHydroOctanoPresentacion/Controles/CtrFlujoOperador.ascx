<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtrFlujoOperador.ascx.cs" Inherits="AnhHydroOctanoPresentacion.Controles.CtrFlujoOperador" %>

<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>

<%@ Register Src="CtrFecheroMes.ascx" TagName="CtrFecheroMes" TagPrefix="uc1" %>

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

    .auto-style10 {
        height: 26px;
    }

    .auto-style11 {
        width: 168px;
        height: 26px;
    }

    .auto-style12 {
        width: 138px;
        height: 26px;
    }

    .panelPersona {
        position: fixed;
        top: 410px;
        width: 500px;
        background: white;
    }

    .hydro_smart_container {
        margin: 0;
        padding: 0;
        Height: 690px;
    }

    .hydro_smart_header {
        margin: 0;
        padding: 5px 10px;
        /*background: orange;*/
        background: rgb(0, 132, 38);
        color: white;
        font-size: medium;
        font-weight: bold;
    }

    .hydro_smart_table {
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
        color: #555;
        border-radius: 10px;
        font-family: Tahoma,Geneva,Arial,sans-serif;
        font-size: 11px;
        padding: 10px 10px 10px 36px;
        margin: 10px;
    }

    .auto-style14 {
        width: 202px;
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
    function multiplicar() {
        var a = document.getElementById("<%=txtPreUnitarioFacProducto.ClientID %>").value;
        var b = document.getElementById("<%=txtVolrecibidoFacProducto.ClientID %>").value;

        var a = a.replace(",", ".");
        var b = b.replace(",", ".");

        var resultado = (a * b).toFixed(2);

        var resultadofinal = resultado.toString().replace(".", ",");

        document.getElementById("<%=txtPreTotalFacProducto.ClientID %>").value = resultadofinal;
    }

    function multiplicarEdicion() {

        var a = document.getElementById("<%=txtPrecioEfImpEdt.ClientID %>").value;
        var b = document.getElementById("<%=txtFactVolRecibidoImpEdt.ClientID %>").value;

        var a = a.replace(",", ".");
        var b = b.replace(",", ".");

        var resultado = (a * b).toFixed(2);

        var resultadofinal = resultado.toString().replace(".", ",");

        document.getElementById("<%=txtImporteEfImpEdt.ClientID %>").value = resultadofinal;

    }

    function onCellClick(rowIndex, fieldName, cell) {
        if (fieldName != 'MDIN') {
            cell.style.backgroundColor = "orange";
        } 
        for (var i = 0; i <= 100; i++) { }
        if (fieldName == 'DecSaldoInicial' || fieldName == 'MDIN' || !isNaN(fieldName.substr(6, 2))) {

            var str = fieldName;
            var res = str.substr(str.length - 3, 3);


            if (fieldName == 'DecIng01Im' || fieldName == 'DecSal01Im' || fieldName == 'DecSal02Im') {

                pnlTransaccionImportacion.PerformCallback(rowIndex + "|" + fieldName + "|1");
                pucDetalleImportacion.Show();
            }
                /*COMERCIALIZACION*/
                //else if (res == 'Glp') {
                //else if (fieldName == 'DecSal03Glp') {    
                //    pnlTransaccionGlp.PerformCallback(rowIndex + "|" + fieldName + "|1");
                //    pucDetalleGlp.Show();
                //}
            else if (fieldName == 'DecSal06NoD') {
                pnlTransaccionNotaDespacho.PerformCallback(rowIndex + "|" + fieldName + "|1");
                pucDetalleNotaDespachoYpfb.Show();
            }
            else {
                pucDetalle.SetHeaderText('Detalle de Movimientos - ' + lblRazonSocial.GetText());
                pnlTransaccion.PerformCallback(rowIndex + "|" + fieldName + "|1");
                pucDetalle.Show(); 
            }


        }
    }

    function tb1_OnKeyUp(s, e) {
        s.SetText(s.GetText().toUpperCase());
    }
    document.onkeydown = function (evt) { return (evt ? evt.which : event.keyCode) != 13; }

    function onClickRegistrarFacturas() {

        if (txtProductoSeleccionado.GetText() == "GBLANCA") {
            if (cmbTipoFacturaImpAdd.GetValue() == 1) {

                var validacion = 0;
                //para facturas de producto
                if (txtSecFacProducto.GetText() != "0") {

                    var numPrecioUnitarioFacProducto = document.getElementById("<%=txtPreUnitarioFacProducto.ClientID %>").value;
                    var numVolrecibidoFacProducto = document.getElementById("<%=txtVolrecibidoFacProducto.ClientID %>").value;

                    if (numPrecioUnitarioFacProducto == "") {
                        alert("El precio unitario debe ser mayor a 0");
                    }
                    else if (numVolrecibidoFacProducto == "") {
                        alert("El volúmen facturado debe ser mayor a 0");
                    }
                    else {
                        validacion = 1;
                    }
                }
                else {
                    //sin implementar
                }

                //para facturas de inspeccion
                if (txtSecFacInOrigen.GetText() != "0") {

                    var numMonFacInOrigen = document.getElementById("<%=txtMonFacInOrigen.ClientID %>").value;
                    var numVolFactInOrigen = document.getElementById("<%=txtVolFactInOrigen.ClientID %>").value;

                    if (txtFacInOrigen.GetText() == "") {
                        alert("Inserte el número de factura de inspección de origen");
                    }
                    else if (txtEmpresaFacInOrigen.GetText() == "") {
                        alert("Inserte el nombre de la empresa inspectora");
                    }
                    else if (numMonFacInOrigen == "") {
                        alert("El monto de factura de inspeción de origen debe ser mayo a 0");
                    }
                    else if (numVolFactInOrigen == "") {
                        alert("El volumen facturado por inspeccion de origen debe ser mayor a 0");
                    }
                    else {
                        validacion = 1;
                    }
                }
                else {
                    //sin implementar
                }

                if (txtSecFacInIn.GetText() != "0") {

                    var numMontoFacInIn = document.getElementById("<%=txtMontoFacInIn.ClientID %>").value;
                    var numVolFactInIn = document.getElementById("<%=txtVolFactInIn.ClientID %>").value;

                    if (txtFacInIn.GetText() == "") {
                        alert("Inserte el número de factura de inspección intermedia");
                    }
                    else if (txtEmpresaFacInIn.GetText() == "") {
                        alert("Inserte el nombre de la empresa inspectora");
                    }
                    else if (numMontoFacInIn == "") {
                        alert("El monto de factura de inspeción intermedia debe ser mayo a 0");
                    }
                    else if (numVolFactInIn == "0") {
                        alert("El volumen facturado por inspeccion intermedia debe ser mayor a 0");
                    }
                    else {
                        validacion = 1;
                    }
                }
                else {
                    //sin implementar
                }

                if (txtSecFacInFin.GetText() != "0") {

                    var numMontoFacInFin = document.getElementById("<%=txtMontoFacInFin.ClientID %>").value;
                    var numVolFactInFin = document.getElementById("<%=txtVolFactInFin.ClientID %>").value;

                    if (txtFacInFin.GetText() == "") {
                        alert("Inserte el número de factura de inspección final");
                    }
                    else if (txtEmpresaFacInFin.GetText() == "") {
                        alert("Inserte el nombre de la empresa inspectora");
                    }
                    else if (numMontoFacInFin == "") {
                        alert("El monto de factura de inspeción final debe ser mayo a 0");
                    }
                    else if (numVolFactInFin == "0") {
                        alert("El volumen facturado por inspeccion final debe ser mayor a 0");
                    }
                    else {
                        validacion = 1;
                    }
                }
                else {
                    //sin implementar
                }

                //para facturas de monitoreo
                if (txtSecFacMon.GetText() != "0") {

                    var numMontoFacMon = document.getElementById("<%=txtMontoFacMon.ClientID %>").value;
                    var numVolReMon = document.getElementById("<%=txtVolReMon.ClientID %>").value;

                    if (txtFacMon.GetText() == "") {
                        alert("Inserte el número de factura de monitoreo");
                    }
                    else if (txtEmpresaFacMon.GetText() == "") {
                        alert("Inserte el nombre de la empresa de monitoreo");
                    }
                    else if (numMontoFacMon == "") {
                        alert("El monto de factura de monitoreo debe ser mayo a 0");
                    }
                    else if (numVolReMon == "0") {
                        alert("El volumen recibido del monitoreo debe ser mayor a 0");
                    }
                    else {
                        validacion = 1;
                    }
                }
                else {
                    //sin implementar
                }

                if (validacion == 1) {
                    txtIdDocEfImpEdt.SetValue(1);
                    dgvDocumentosImp.PerformCallback();
                    pudDocumentoImpAdd.Hide();
                    alert("Documento agregado");
                }
                else {
                    alert("No se tienen documentos para agregar");
                }
            }
            else if (cmbTipoFacturaImpAdd.GetValue() == 2) {

                var validacion = 0;

                if (txtSecFacTransFlu.GetText() != "0") {

                    var numCostoUniFacTransFlu = document.getElementById("<%=txtCostoUniFacTransFlu.ClientID %>").value;
                    var numFleteFacTransFlu = document.getElementById("<%=txtFleteFacTransFlu.ClientID %>").value;
                    var numVolDespachadoFacTransFlu = document.getElementById("<%=txtVolDespachadoFacTransFlu.ClientID %>").value;
                    var numVolRecibidoFacTransFlu = document.getElementById("<%=txtVolRecibidoFacTransFlu.ClientID %>").value;

                    if (txtEmpresaFacTransFlu.GetText() == "") {
                        alert("Inserte el nombre de la empresa de transporte");
                    }
                    else if (txtFacTransFlu.GetText() == "") {
                        alert("Inserte el numero de factura de transporte");
                    }
                    else if (numCostoUniFacTransFlu == "") {
                        alert("El monto unitario de factura de transporte debe ser mayo a 0");
                    }
                    else if (numFleteFacTransFlu == "") {
                        alert("El monto de flete de factura de transporte debe ser mayor a 0");
                    }
                    else if (txtPlantaDespachoFacTransFlu.GetText() == "") {
                        alert("Inserte el nombre de la planta de despacho");
                    }
                    else if (numVolDespachadoFacTransFlu == "") {
                        alert("El volumen de despacho debe ser mayor a 0");
                    }
                    else if (txtPlantaRecepcionFacTransFlu.GetText() == "") {
                        alert("Inserte el nombre de la planta de recepción");
                    }
                    else if (numVolRecibidoFacTransFlu == "") {
                        alert("El volumen de recepcion debe ser mayor a 0");
                    }
                    else {
                        validacion = 1;
                    }
                }
                else {
                    //sin implementar
                }

                if (txtSecFacTransTe.GetText() != "0") {

                    var numCostoUniFacTransTe = document.getElementById("<%=txtCostoUniFacTransTe.ClientID %>").value;
                    var numFleteFacTransTe = document.getElementById("<%=txtFleteFacTransTe.ClientID %>").value;
                    var numVolDespachadoFacTransTe = document.getElementById("<%=txtVolDespachadoFacTransTe.ClientID %>").value;
                    var numVolRecibidoFacTransTe = document.getElementById("<%=txtVolRecibidoFacTransTe.ClientID %>").value;

                    if (txtEmpresaFacTransTe.GetText() == "") {
                        alert("Inserte el nombre de la empresa de transporte");
                    }
                    else if (txtFacTransTe.GetText() == "") {
                        alert("Inserte el numero de factura de transporte");
                    }
                    else if (numCostoUniFacTransTe == "") {
                        alert("El monto unitario de factura de transporte debe ser mayo a 0");
                    }
                    else if (numFleteFacTransTe == "") {
                        alert("El monto de flete de factura de transporte debe ser mayor a 0");
                    }
                    else if (txtPlantaDespachoFacTransTe.GetText() == "") {
                        alert("Inserte el nombre de la planta de despacho");
                    }
                    else if (numVolDespachadoFacTransTe == "") {
                        alert("El volumen de despacho debe ser mayor a 0");
                    }
                    else if (txtPlantaRecepcionFacTransTe.GetText() == "") {
                        alert("Inserte el nombre de la planta de recepción");
                    }
                    else if (numVolRecibidoFacTransTe == "") {
                        alert("El volumen de recepcion debe ser mayor a 0");
                    }
                    else {
                        validacion = 1;
                    }
                }
                else {
                    //sin implementar
                }
                //ductos gasolina blanca
                if (txtSecFacTransTeGb.GetText() != "0") {

                    var numVolFacTransDu = document.getElementById("<%=txtVolFacTransDu.ClientID %>").value;
                    var numPreUniFacTransDu = document.getElementById("<%=txtPreUniFacTransDu.ClientID %>").value;

                    if (numVolFacTransDu == "") {
                        alert("El volumen de factura de transporte por ducto Gasolina Blanca debe ser mayo a 0");
                    }
                    else if (numPreUniFacTransDu == "") {
                        alert("El precio unitario de factura de transporte por ducto Gasolina Blanca debe ser mayor a 0");
                    }
                    else {
                        validacion = 1;
                    }
                }
                else {
                    //sin implementar
                }

                if (txtSecFacTransFe.GetText() != "0") {

                    var numCostoUniFacTransFe = document.getElementById("<%=txtCostoUniFacTransFe.ClientID %>").value;
                    var numFleteFacTransFe = document.getElementById("<%=txtFleteFacTransFe.ClientID %>").value;
                    var numVolDespachadoFacTransFe = document.getElementById("<%=txtVolDespachadoFacTransFe.ClientID %>").value;
                    var numVolRecibidoFacTransFe = document.getElementById("<%=txtVolRecibidoFacTransFe.ClientID %>").value;

                    if (txtEmpresaFacTransFe.GetText() == "") {
                        alert("Inserte el nombre de la empresa de transporte");
                    }
                    else if (txtFacTransFe.GetText() == "") {
                        alert("Inserte el numero de factura de transporte");
                    }
                    else if (numCostoUniFacTransFe == "") {
                        alert("El monto unitario de factura de transporte debe ser mayo a 0");
                    }
                    else if (numFleteFacTransFe == "") {
                        alert("El monto de flete de factura de transporte debe ser mayor a 0");
                    }
                    else if (numVolDespachadoFacTransFe == "") {
                        alert("El volumen 1 debe ser mayor a 0");
                    }
                    else if (numVolRecibidoFacTransFe == "") {
                        alert("El volumen 2 debe ser mayor a 0");
                    }
                    else {
                        validacion = 1;
                    }
                }
                else {
                    //sin implementar
                }

                if (txtSecFacAlmacenajeIntermedio.GetText() != "0") {

                    var numCostoUniFacAlmacenajeIntermedio = document.getElementById("<%=txtCostoUniFacAlmacenajeIntermedio.ClientID %>").value;
                    var numVolRecibidoFacAlmacenajeIntermedio = document.getElementById("<%=txtVolRecibidoFacAlmacenajeIntermedio.ClientID %>").value;

                    if (txtFacAlmacenajeIntermedio.GetText() == "") {
                        alert("Inserte el numero de factura de almacenaje intermedio");
                    }
                    else if (numCostoUniFacAlmacenajeIntermedio == "") {
                        alert("El costo unitario de factura de almacenaje intermedio debe ser mayo a 0");
                    }
                    else if (numVolRecibidoFacAlmacenajeIntermedio == "") {
                        alert("El volumen recibido de la factura de almacenaje intermedio debe ser mayor a 0");
                    }
                    else {
                        validacion = 1;
                    }
                }
                else {
                    //sin implementar
                }

                if (txtSecFacAlmacenajeFinal.GetText() != "0") {

                    var numCostoUniFacAlmacenajeFinal = document.getElementById("<%=txtCostoUniFacAlmacenajeFinal.ClientID %>").value;
                    var numVolRecibidoFacAlmacenajeFinal = document.getElementById("<%=txtVolRecibidoFacAlmacenajeFinal.ClientID %>").value;

                    if (txtFacAlmacenajeFinal.GetText() == "") {
                        alert("Inserte el numero de factura de almacenaje intermedio");
                    }
                    else if (numCostoUniFacAlmacenajeFinal == "") {
                        alert("El costo unitario de factura de almacenaje intermedio debe ser mayo a 0");
                    }
                    else if (numVolRecibidoFacAlmacenajeFinal == "") {
                        alert("El volumen recibido de la factura de almacenaje intermedio debe ser mayor a 0");
                    }
                    else {
                        validacion = 1;
                    }
                }
                else {
                    //sin implementar
                }

                if (validacion == 1) {
                    txtIdDocEfImpEdt.SetValue(1);
                    dgvDocumentosImp.PerformCallback();
                    pudDocumentoImpAdd.Hide();
                    alert("Documento agregado");
                }
                else {
                    alert("No se tienen documentos para agregar");
                }
            }
            else if (cmbTipoFacturaImpAdd.GetValue() == 3) {
                alert("Opción no habilitada");
            }
            else if (cmbTipoFacturaImpAdd.GetValue() == 4) {

                var numMontoFacSeguro = document.getElementById("<%=txtMontoFacSeguro.ClientID %>").value;

                if (txtNumFacSeguro.GetText() == "" && txtNumFacSeguro.GetText() == "0") {
                    alert("Numero de Factura de Seguro no debe ser vacio o 0");
                }
                else if (txtEmpAseguradoraFacSeguro.GetText() == "") {
                    alert("Inserte el nombre de una Empresa Aseguradora");
                }
                else if (numMontoFacSeguro == "0") {
                    alert("Inserte el monto de la Factura de Seguro");
                }
                else {
                    txtIdDocEfImpEdt.SetValue(1);
                    dgvDocumentosImp.PerformCallback();
                    pudDocumentoImpAdd.Hide();
                    alert("Documento agregado");
                }
            }
            else if (cmbTipoFacturaImpAdd.GetValue() == 5) {

                var numVolumenIyA = document.getElementById("<%=txtVolumenIyA.ClientID %>").value;
                var numVolumenGb = document.getElementById("<%=txtVolumenGb.ClientID %>").value;

                if (txtPlantaV.GetText() == "") {
                    alert("Ingrese una planta");
                }
                else if (numVolumenIyA == "") {
                    alert("El volumen por Insumo y Aditivo tiene q ser mayor a 0");
                }
                else if (numVolumenGb == "") {
                    alert("El volumen por Gasolina Blanca tiene q ser mayor a 0");
                }
                else if (numVolumenIyA == "0") {
                    alert("El volumen por Insumo y Aditivo tiene q ser mayor a 0");
                }
                else if (numVolumenGb == "0") {
                    alert("El volumen por Gasolina Blanca tiene q ser mayor a 0");
                }
                else {
                    txtIdDocEfImpEdt.SetValue(1);
                    dgvDocumentosImp.PerformCallback();
                    pudDocumentoImpAdd.Hide();
                    alert("Documento agregado");
                }
            }
            else {
                alert("Opción no habilitada");
            }
}
            //para la nueva resolucion
else if (txtProductoSeleccionado.GetText() == "GRON 80" || txtProductoSeleccionado.GetText() == "GRON 81" || txtProductoSeleccionado.GetText() == "GRON 82" || txtProductoSeleccionado.GetText() == "GRON 83" || txtProductoSeleccionado.GetText() == "GRON 84") {
    if (cmbTipoFacturaImpAdd.GetValue() == 1) {
        var validacion = 0;
        //para facturas de producto                
        if (txtSecFacProducto.GetText() != "0") {

            var numPrecioUnitarioFacProducto = document.getElementById("<%=txtPreUnitarioFacProducto.ClientID %>").value;
            var numVolrecibidoFacProducto = document.getElementById("<%=txtVolrecibidoFacProducto.ClientID %>").value;

            if (txtDuiFacProducto.GetText() == "") {
                alert("Inserte el número de DUI para registrar el dato del producto");
            }
            else if (txtFacProducto.GetText() == "") {
                alert("Inserte el número de factura para registrar el dato del producto");
            }
            else if (txtFacProducto.GetText() == "0") {
                alert("El número de factura debe ser mayor a 0");
            }
            else if (txtRazonSocialFacProducto.GetText() == "") {
                alert("Inserte el nombre del proveedor para registrar el dato del producto");
            }
            else if (txtNitFacProducto.GetText() == "") {
                alert("Inserte el número de NIT para registrar el dato del producto");
            }
            else if (numPrecioUnitarioFacProducto == "") {
                alert("El precio unitario debe ser mayor a 0");
            }
            else if (numVolrecibidoFacProducto == "") {
                alert("El volúmen facturado debe ser mayor a 0");
            }
            else {
                validacion = 1;
            }
        }
        else {
            //sin implementar
        }

        //para facturas de inspeccion
        if (txtSecFacInOrigen.GetText() != "0") {

            var numMonFacInOrigen = document.getElementById("<%=txtMonFacInOrigen.ClientID %>").value;
                    var numVolFactInOrigen = document.getElementById("<%=txtVolFactInOrigen.ClientID %>").value;

                    if (txtDuiFacProducto.GetText() == "") {
                        alert("Inserte el número de DUI para registrar el dato del producto");
                    }
                    else if (txtFacInOrigen.GetText() == "") {
                        alert("Inserte el número de factura de inspección de origen");
                    }
                    else if (txtEmpresaFacInOrigen.GetText() == "") {
                        alert("Inserte el nombre de la empresa inspectora");
                    }
                    else if (numMonFacInOrigen == "") {
                        alert("El monto de factura de inspeción de origen debe ser mayo a 0");
                    }
                    else if (txtFacProducto.GetText() == "") {
                        alert("Inserte el número de factura para registrar el dato del producto");
                    }
                    else if (txtFacProducto.GetText() == "0") {
                        alert("El número de factura debe ser mayor a 0");
                    }
                    else if (txtRazonSocialFacProducto.GetText() == "") {
                        alert("Inserte el nombre del proveedor para registrar el dato del producto");
                    }
                    else if (numVolFactInOrigen == "0") {
                        alert("El volumen facturado por inspeccion de origen debe ser mayor a 0");
                    }
                    else {
                        validacion = 1;
                    }
                }
                else {
                    //sin implementar
                }

                if (txtSecFacInIn.GetText() != "0") {

                    var numMontoFacInIn = document.getElementById("<%=txtMontoFacInIn.ClientID %>").value;
                    var numVolFactInIn = document.getElementById("<%=txtVolFactInIn.ClientID %>").value;

                    if (txtDuiFacProducto.GetText() == "") {
                        alert("Inserte el número de DUI para registrar el dato del producto");
                    }
                    else if (txtFacInIn.GetText() == "") {
                        alert("Inserte el número de factura de inspección intermedia");
                    }
                    else if (txtEmpresaFacInIn.GetText() == "") {
                        alert("Inserte el nombre de la empresa inspectora");
                    }
                    else if (numMontoFacInIn == "") {
                        alert("El monto de factura de inspeción intermedia debe ser mayo a 0");
                    }
                    else if (txtFacProducto.GetText() == "") {
                        alert("Inserte el número de factura para registrar el dato del producto");
                    }
                    else if (txtFacProducto.GetText() == "0") {
                        alert("El número de factura debe ser mayor a 0");
                    }
                    else if (txtRazonSocialFacProducto.GetText() == "") {
                        alert("Inserte el nombre del proveedor para registrar el dato del producto");
                    }
                    else if (numVolFactInIn == "0") {
                        alert("El volumen facturado por inspeccion intermedia debe ser mayor a 0");
                    }
                    else {
                        validacion = 1;
                    }
                }
                else {
                    //sin implementar
                }

                if (txtSecFacInFin.GetText() != "0") {

                    var numMontoFacInFin = document.getElementById("<%=txtMontoFacInFin.ClientID %>").value;
                    var numVolFactInFin = document.getElementById("<%=txtVolFactInFin.ClientID %>").value;

                    if (txtDuiFacProducto.GetText() == "") {
                        alert("Inserte el número de DUI para registrar el dato del producto");
                    }
                    else if (txtFacInFin.GetText() == "") {
                        alert("Inserte el número de factura de inspección final");
                    }
                    else if (txtEmpresaFacInFin.GetText() == "") {
                        alert("Inserte el nombre de la empresa inspectora");
                    }
                    else if (numMontoFacInFin == "") {
                        alert("El monto de factura de inspeción final debe ser mayo a 0");
                    }
                    else if (txtFacProducto.GetText() == "") {
                        alert("Inserte el número de factura para registrar el dato del producto");
                    }
                    else if (txtFacProducto.GetText() == "0") {
                        alert("El número de factura debe ser mayor a 0");
                    }
                    else if (txtRazonSocialFacProducto.GetText() == "") {
                        alert("Inserte el nombre del proveedor para registrar el dato del producto");
                    }
                    else if (numVolFactInFin == "0") {
                        alert("El volumen facturado por inspeccion final debe ser mayor a 0");
                    }
                    else {
                        validacion = 1;
                    }
                }
                else {
                    //sin implementar
                }

        //para facturas de monitoreo
                if (txtSecFacMon.GetText() != "0") {

                    var numMontoFacMon = document.getElementById("<%=txtMontoFacMon.ClientID %>").value;
                    var numVolReMon = document.getElementById("<%=txtVolReMon.ClientID %>").value;

                    if (txtDuiFacProducto.GetText() == "") {
                        alert("Inserte el número de DUI para registrar el dato del producto");
                    }
                    else if (txtFacMon.GetText() == "") {
                        alert("Inserte el número de factura de monitoreo");
                    }
                    else if (txtEmpresaFacMon.GetText() == "") {
                        alert("Inserte el nombre de la empresa de monitoreo");
                    }
                    else if (numMontoFacMon == "") {
                        alert("El monto de factura de monitoreo debe ser mayo a 0");
                    }
                    else if (txtFacProducto.GetText() == "") {
                        alert("Inserte el número de factura para registrar el dato del producto");
                    }
                    else if (txtFacProducto.GetText() == "0") {
                        alert("El número de factura debe ser mayor a 0");
                    }
                    else if (numVolReMon == "0") {
                        alert("El volumen recibido del monitoreo debe ser mayor a 0");
                    }
                    else {
                        validacion = 1;
                    }
                }
                else {
                    //sin implementar
                }

                if (validacion == 1) {
                    txtIdDocEfImpEdt.SetValue(1);
                    dgvDocumentosImp.PerformCallback();
                    pudDocumentoImpAdd.Hide();
                    alert("Documento agregado");
                }
                else {
                    alert("No se tienen documentos para agregar");
                }
            }
            else if (cmbTipoFacturaImpAdd.GetValue() == 2) {

                var validacion = 0;

                if (txtSecFacTransFlu.GetText() != "0") {

                    var numCostoUniFacTransFlu = document.getElementById("<%=txtCostoUniFacTransFlu.ClientID %>").value;
                    var numFleteFacTransFlu = document.getElementById("<%=txtFleteFacTransFlu.ClientID %>").value;
                    var numVolDespachadoFacTransFlu = document.getElementById("<%=txtVolDespachadoFacTransFlu.ClientID %>").value;
                    var numVolRecibidoFacTransFlu = document.getElementById("<%=txtVolRecibidoFacTransFlu.ClientID %>").value;

                    if (txtDuiFacProducto.GetText() == "") {
                        alert("Inserte el número de DUI para registrar el dato del producto");
                    }
                    else if (txtFacProducto.GetText() == "") {
                        alert("Inserte el número de factura para registrar el dato del producto");
                    }
                    else if (txtFacProducto.GetText() == "0") {
                        alert("El número de factura debe ser mayor a 0");
                    }
                    else if (txtEmpresaFacTransFlu.GetText() == "") {
                        alert("Inserte el nombre de la empresa de transporte");
                    }
                    else if (txtFacTransFlu.GetText() == "") {
                        alert("Inserte el numero de factura de transporte");
                    }
                    else if (numCostoUniFacTransFlu == "") {
                        alert("El monto unitario de factura de transporte debe ser mayo a 0");
                    }
                    else if (numFleteFacTransFlu == "") {
                        alert("El monto de flete de factura de transporte debe ser mayor a 0");
                    }
                    else if (txtPlantaDespachoFacTransFlu.GetText() == "") {
                        alert("Inserte el nombre de la planta de despacho");
                    }
                    else if (numVolDespachadoFacTransFlu == "") {
                        alert("El volumen de despacho debe ser mayor a 0");
                    }
                    else if (txtPlantaRecepcionFacTransFlu.GetText() == "") {
                        alert("Inserte el nombre de la planta de recepción");
                    }
                    else if (numVolRecibidoFacTransFlu == "") {
                        alert("El volumen de recepcion debe ser mayor a 0");
                    }
                    else {
                        validacion = 1;
                    }
                }
                else {
                    //sin implementar
                }

                if (txtSecFacTransTe.GetText() != "0") {

                    var numCostoUniFacTransTe = document.getElementById("<%=txtCostoUniFacTransTe.ClientID %>").value;
                    var numFleteFacTransTe = document.getElementById("<%=txtFleteFacTransTe.ClientID %>").value;
                    var numVolDespachadoFacTransTe = document.getElementById("<%=txtVolDespachadoFacTransTe.ClientID %>").value;
                    var numVolRecibidoFacTransTe = document.getElementById("<%=txtVolRecibidoFacTransTe.ClientID %>").value;

                    if (txtDuiFacProducto.GetText() == "") {
                        alert("Inserte el número de DUI para registrar el dato del producto");
                    }
                    else if (txtFacProducto.GetText() == "") {
                        alert("Inserte el número de factura para registrar el dato del producto");
                    }
                    else if (txtFacProducto.GetText() == "0") {
                        alert("El número de factura debe ser mayor a 0");
                    }
                    else if (txtEmpresaFacTransTe.GetText() == "") {
                        alert("Inserte el nombre de la empresa de transporte");
                    }
                    else if (txtFacTransTe.GetText() == "") {
                        alert("Inserte el numero de factura de transporte");
                    }
                    else if (numCostoUniFacTransTe == "") {
                        alert("El monto unitario de factura de transporte debe ser mayo a 0");
                    }
                    else if (numFleteFacTransTe == "") {
                        alert("El monto de flete de factura de transporte debe ser mayor a 0");
                    }
                    else if (txtPlantaDespachoFacTransTe.GetText() == "") {
                        alert("Inserte el nombre de la planta de despacho");
                    }
                    else if (numVolDespachadoFacTransTe == "") {
                        alert("El volumen de despacho debe ser mayor a 0");
                    }
                    else if (txtPlantaRecepcionFacTransTe.GetText() == "") {
                        alert("Inserte el nombre de la planta de recepción");
                    }
                    else if (numVolRecibidoFacTransTe == "") {
                        alert("El volumen de recepcion debe ser mayor a 0");
                    }
                    else {
                        validacion = 1;
                    }
                }
                else {
                    //sin implementar
                }

                if (txtSecFacTransFe.GetText() != "0") {

                    var numCostoUniFacTransFe = document.getElementById("<%=txtCostoUniFacTransFe.ClientID %>").value;
                    var numFleteFacTransFe = document.getElementById("<%=txtFleteFacTransFe.ClientID %>").value;
                    var numVolDespachadoFacTransFe = document.getElementById("<%=txtVolDespachadoFacTransFe.ClientID %>").value;
                    var numVolRecibidoFacTransFe = document.getElementById("<%=txtVolRecibidoFacTransFe.ClientID %>").value;

                    if (txtDuiFacProducto.GetText() == "") {
                        alert("Inserte el número de DUI para registrar el dato del producto");
                    }
                    else if (txtFacProducto.GetText() == "") {
                        alert("Inserte el número de factura para registrar el dato del producto");
                    }
                    else if (txtFacProducto.GetText() == "0") {
                        alert("El número de factura debe ser mayor a 0");
                    }
                    else if (txtEmpresaFacTransFe.GetText() == "") {
                        alert("Inserte el nombre de la empresa de transporte");
                    }
                    else if (txtFacTransFe.GetText() == "") {
                        alert("Inserte el numero de factura de transporte");
                    }
                    else if (numCostoUniFacTransFe == "") {
                        alert("El monto unitario de factura de transporte debe ser mayo a 0");
                    }
                    else if (numFleteFacTransFe == "") {
                        alert("El monto de flete de factura de transporte debe ser mayor a 0");
                    }
                    else if (numVolDespachadoFacTransFe == "") {
                        alert("El volumen 1 debe ser mayor a 0");
                    }
                    else if (numVolRecibidoFacTransFe == "") {
                        alert("El volumen 2 debe ser mayor a 0");
                    }
                    else {
                        validacion = 1;
                    }
                }
                else {
                    //sin implementar
                }

                if (txtSecFacAlmacenajeIntermedio.GetText() != "0") {

                    var numCostoUniFacAlmacenajeIntermedio = document.getElementById("<%=txtCostoUniFacAlmacenajeIntermedio.ClientID %>").value;
                    var numVolRecibidoFacAlmacenajeIntermedio = document.getElementById("<%=txtVolRecibidoFacAlmacenajeIntermedio.ClientID %>").value;

                    if (txtDuiFacProducto.GetText() == "") {
                        alert("Inserte el número de DUI para registrar el dato del producto");
                    }
                    else if (txtFacAlmacenajeIntermedio.GetText() == "") {
                        alert("Inserte el numero de factura de almacenaje intermedio");
                    }
                    else if (txtEmpresaFacAlmacenajeIntermedio.GetText() == "") {
                        alert("Inserte el nombre de la empresa de almacenaje intermedio");
                    }
                    else if (numCostoUniFacAlmacenajeIntermedio == "") {
                        alert("El costo unitario de factura de almacenaje intermedio debe ser mayo a 0");
                    }
                    else if (numVolRecibidoFacAlmacenajeIntermedio == "") {
                        alert("El volumen recibido de la factura de almacenaje intermedio debe ser mayor a 0");
                    }
                    else {
                        validacion = 1;
                    }
                }
                else {
                    //sin implementar
                }

                if (txtSecFacAlmacenajeFinal.GetText() != "0") {

                    var numCostoUniFacAlmacenajeFinal = document.getElementById("<%=txtCostoUniFacAlmacenajeFinal.ClientID %>").value;
                    var numVolRecibidoFacAlmacenajeFinal = document.getElementById("<%=txtVolRecibidoFacAlmacenajeFinal.ClientID %>").value;

                    if (txtDuiFacProducto.GetText() == "") {
                        alert("Inserte el número de DUI para registrar el dato del producto");
                    }
                    else if (txtFacAlmacenajeFinal.GetText() == "") {
                        alert("Inserte el numero de factura de almacenaje intermedio");
                    }
                    else if (txtEmpresaAlmacenajeFinal.GetText() == "") {
                        alert("Inserte el nombre de la empresa de almacenaje intermedio");
                    }
                    else if (numCostoUniFacAlmacenajeFinal == "") {
                        alert("El costo unitario de factura de almacenaje intermedio debe ser mayo a 0");
                    }
                    else if (numVolRecibidoFacAlmacenajeFinal == "") {
                        alert("El volumen recibido de la factura de almacenaje intermedio debe ser mayor a 0");
                    }
                    else {
                        validacion = 1;
                    }
                }
                else {
                    //sin implementar
                }

                if (validacion == 1) {
                    txtIdDocEfImpEdt.SetValue(1);
                    dgvDocumentosImp.PerformCallback();
                    pudDocumentoImpAdd.Hide();
                    alert("Documento agregado");
                }
                else {
                    alert("No se tienen documentos para agregar");
                }
            }
            else if (cmbTipoFacturaImpAdd.GetValue() == 3) {

                var numGravamenFacAduana = document.getElementById("<%=txtGravamenFacAduana.ClientID %>").value;
                var numSiduineaFacAduana = document.getElementById("<%=txtSiduineaFacAduana.ClientID %>").value;
                var numDepArancelarioFacAduana = document.getElementById("<%=txtDepArancelarioFacAduana.ClientID %>").value;

                if (numGravamenFacAduana == "" && numSiduineaFacAduana == "" && numDepArancelarioFacAduana == "") {
                    alert("Ingrese por lo menos un valor de factura aduanera");
                }
                else if (numGravamenFacAduana == "") {
                    alert("Monto de gravamen no debe ser vacio");
                }
                else if (numSiduineaFacAduana == "") {
                    alert("Monto Siduinea no debe ser vacio");
                }
                else if (numDepArancelarioFacAduana == "") {
                    alert("Monto arancelario no debe ser vacio");
                }
                else if (txtDuiFacProducto.GetText() == "") {
                    alert("Ingrese el dato de la DUI");
                }
                else {
                    txtIdDocEfImpEdt.SetValue(1);
                    dgvDocumentosImp.PerformCallback();
                    pudDocumentoImpAdd.Hide();
                    alert("Documento agregado");
                }
            }
            else if (cmbTipoFacturaImpAdd.GetValue() == 4) {

                var numMontoFacSeguro = document.getElementById("<%=txtMontoFacSeguro.ClientID %>").value;

                    if (txtNumFacSeguro.GetText() == "") {
                        alert("Numero de Factura de Seguro no debe ser vacio o 0");
                    }
                    else if (txtNumFacSeguro.GetText() == "0") {
                        alert("Numero de Factura de Seguro no debe ser vacio o 0");
                    }
                    else if (txtEmpAseguradoraFacSeguro.GetText() == "") {
                        alert("Inserte el nombre de una Empresa Aseguradora");
                    }
                    else if (numMontoFacSeguro == "0") {
                        alert("Inserte el monto de la Factura de Seguro");
                    }
                    else if (txtDuiFacProducto.GetText() == "") {
                        alert("Inserte el dato de la DUI");
                    }
                    else {
                        txtIdDocEfImpEdt.SetValue(1);
                        dgvDocumentosImp.PerformCallback();
                        pudDocumentoImpAdd.Hide();
                        alert("Documento agregado");
                    }
                }
                else if (cmbTipoFacturaImpAdd.GetValue() == 5) {
                    alert("Opción no habilitada");
                }
                else if (cmbTipoFacturaImpAdd.GetValue() == 6) {
                    if (txtErrorMasivo.GetText() == "1") {
                        alert("El archivo tiene errores");
                    }
                    else {
                        txtIdDocEfImpEdt.SetValue(1);
                        dgvDocumentosImp.PerformCallback();
                        pudDocumentoImpAdd.Hide();
                        alert("Documento agregado");
                    }
                }
                else {
                    alert("Opción no habilitada");
                }
}
else {// para la antigua resolucion
    if (cmbTipoFacturaImpAdd.GetValue() == 1) {

        var validacion = 0;

        //para facturas de producto
        if (txtSecFacProducto.GetText() != "0") {

            var numPrecioUnitarioFacProducto = document.getElementById("<%=txtPreUnitarioFacProducto.ClientID %>").value;
            var numVolrecibidoFacProducto = document.getElementById("<%=txtVolrecibidoFacProducto.ClientID %>").value;

            if (txtDuiFacProducto.GetText() == "") {
                alert("Inserte el número de DUI para registrar el dato del producto");
            }
            else if (txtFacProducto.GetText() == "") {
                alert("Inserte el número de factura para registrar el dato del producto");
            }
            else if (txtRazonSocialFacProducto.GetText() == "") {
                alert("Inserte el nombre del proveedor para registrar el dato del producto");
            }
            else if (txtNitFacProducto.GetText() == "") {
                alert("Inserte el número de NIT para registrar el dato del producto");
            }
            else if (numPrecioUnitarioFacProducto == "") {
                alert("El precio unitario debe ser mayor a 0");
            }
            else if (numVolrecibidoFacProducto == "") {
                alert("El volúmen facturado debe ser mayor a 0");
            }
            else {
                validacion = 1;
            }
        }
        else {
            //sin implementar
        }

        //para facturas de inspeccion
        if (txtSecFacInOrigen.GetText() != "0") {

            var numMonFacInOrigen = document.getElementById("<%=txtMonFacInOrigen.ClientID %>").value;
                    var numVolFactInOrigen = document.getElementById("<%=txtVolFactInOrigen.ClientID %>").value;

                    if (txtDuiFacProducto.GetText() == "") {
                        alert("Inserte el número de DUI para registrar el dato del producto");
                    }
                    else if (txtFacInOrigen.GetText() == "") {
                        alert("Inserte el número de factura de inspección de origen");
                    }
                    else if (txtEmpresaFacInOrigen.GetText() == "") {
                        alert("Inserte el nombre de la empresa inspectora");
                    }
                    else if (numMonFacInOrigen == "") {
                        alert("El monto de factura de inspeción de origen debe ser mayo a 0");
                    }
                    else if (numVolFactInOrigen == "") {
                        alert("El volúmen facturado de inspeccion de origen debe ser mayor a 0");
                    }
                    else {
                        validacion = 1;
                    }
                }
                else {
                    //sin implementar
                }

                if (txtSecFacInIn.GetText() != "0") {

                    var numMontoFacInIn = document.getElementById("<%=txtMontoFacInIn.ClientID %>").value;
                    var numVolFactInIn = document.getElementById("<%=txtVolFactInIn.ClientID %>").value;

                    if (txtDuiFacProducto.GetText() == "") {
                        alert("Inserte el número de DUI para registrar el dato del producto");
                    }
                    else if (txtFacInIn.GetText() == "") {
                        alert("Inserte el número de factura de inspección intermedia");
                    }
                    else if (txtEmpresaFacInIn.GetText() == "") {
                        alert("Inserte el nombre de la empresa inspectora");
                    }
                    else if (numMontoFacInIn == "") {
                        alert("El monto de factura de inspeción intermedia debe ser mayo a 0");
                    }
                    else if (numVolFactInIn == "") {
                        alert("El volúmen facturado de inspeccion intermedia debe ser mayor a 0");
                    }
                    else {
                        validacion = 1;
                    }
                }
                else {
                    //sin implementar
                }

                if (txtSecFacInFin.GetText() != "0") {

                    var numMontoFacInFin = document.getElementById("<%=txtMontoFacInFin.ClientID %>").value;
                    var numVolFactInFin = document.getElementById("<%=txtVolFactInFin.ClientID %>").value;

                    if (txtDuiFacProducto.GetText() == "") {
                        alert("Inserte el número de DUI para registrar el dato del producto");
                    }
                    else if (txtFacInFin.GetText() == "") {
                        alert("Inserte el número de factura de inspección final");
                    }
                    else if (txtEmpresaFacInFin.GetText() == "") {
                        alert("Inserte el nombre de la empresa inspectora");
                    }
                    else if (numMontoFacInFin == "") {
                        alert("El monto de factura de inspeción final debe ser mayo a 0");
                    }
                    else if (numVolFactInFin == "") {
                        alert("El volúmen facturado de inspeccion final debe ser mayor a 0");
                    }
                    else {
                        validacion = 1;
                    }
                }
                else {
                    //sin implementar
                }

        //para facturas de monitoreo
                if (txtSecFacMon.GetText() != "0") {

                    var numMontoFacMon = document.getElementById("<%=txtMontoFacMon.ClientID %>").value;
                    var numVolReMon = document.getElementById("<%=txtVolReMon.ClientID %>").value;

                    if (txtDuiFacProducto.GetText() == "") {
                        alert("Inserte el número de DUI para registrar el dato del producto");
                    }
                    else if (txtFacMon.GetText() == "") {
                        alert("Inserte el número de factura de monitoreo");
                    }
                    else if (txtEmpresaFacMon.GetText() == "") {
                        alert("Inserte el nombre de la empresa de monitoreo");
                    }
                    else if (numMontoFacMon == "") {
                        alert("El monto de factura de monitoreo debe ser mayo a 0");
                    }
                    else if (numVolReMon == "") {
                        alert("El volúmen facturado de monitoreo debe ser mayor a 0");
                    }
                    else {
                        validacion = 1;
                    }
                }
                else {
                    //sin implementar
                }

                if (validacion == 1) {
                    txtIdDocEfImpEdt.SetValue(1);
                    dgvDocumentosImp.PerformCallback();
                    pudDocumentoImpAdd.Hide();
                    alert("Documento agregado");
                }
                else {
                    alert("No se tienen documentos para agregar");
                }
            }
            else if (cmbTipoFacturaImpAdd.GetValue() == 2) {

                var validacion = 0;

                if (txtSecFacTransFlu.GetText() != "0") {

                    var numCostoUniFacTransFlu = document.getElementById("<%=txtCostoUniFacTransFlu.ClientID %>").value;
                    var numFleteFacTransFlu = document.getElementById("<%=txtFleteFacTransFlu.ClientID %>").value;
                    var numVolDespachadoFacTransFlu = document.getElementById("<%=txtVolDespachadoFacTransFlu.ClientID %>").value;
                    var numVolRecibidoFacTransFlu = document.getElementById("<%=txtVolRecibidoFacTransFlu.ClientID %>").value;

                    if (txtDuiFacProducto.GetText() == "") {
                        alert("Inserte el número de DUI para registrar el dato del producto");
                    }
                    else if (txtFacProducto.GetText() == "") {
                        alert("Inserte el número de factura para registrar el dato del producto");
                    }
                    else if (txtRazonSocialFacProducto.GetText() == "") {
                        alert("Inserte el nombre del proveedor para registrar el dato del producto");
                    }
                    else if (txtEmpresaFacTransFlu.GetText() == "") {
                        alert("Inserte el nombre de la empresa de transporte");
                    }
                    else if (txtFacTransFlu.GetText() == "") {
                        alert("Inserte el numero de factura de transporte");
                    }
                    else if (numCostoUniFacTransFlu == "") {
                        alert("El monto unitario de factura de transporte debe ser mayo a 0");
                    }
                    else if (numFleteFacTransFlu == "") {
                        alert("El monto de flete de factura de transporte debe ser mayor a 0");
                    }
                    else if (txtPlantaDespachoFacTransFlu.GetText() == "") {
                        alert("Inserte el nombre de la planta de despacho");
                    }
                    else if (numVolDespachadoFacTransFlu == "") {
                        alert("El volumen de despacho debe ser mayor a 0");
                    }
                    else if (txtPlantaRecepcionFacTransFlu.GetText() == "") {
                        alert("Inserte el nombre de la planta de recepción");
                    }
                    else if (numVolRecibidoFacTransFlu == "") {
                        alert("El volumen de recepcion debe ser mayor a 0");
                    }
                    else {
                        validacion = 1;
                    }
                }
                else {
                    //sin implementar
                }

                if (txtSecFacTransTe.GetText() != "0") {

                    var numCostoUniFacTransTe = document.getElementById("<%=txtCostoUniFacTransTe.ClientID %>").value;
                    var numFleteFacTransTe = document.getElementById("<%=txtFleteFacTransTe.ClientID %>").value;
                    var numVolDespachadoFacTransTe = document.getElementById("<%=txtVolDespachadoFacTransTe.ClientID %>").value;
                    var numVolRecibidoFacTransTe = document.getElementById("<%=txtVolRecibidoFacTransTe.ClientID %>").value;

                    if (txtDuiFacProducto.GetText() == "") {
                        alert("Inserte el número de DUI para registrar el dato del producto");
                    }
                    else if (txtFacProducto.GetText() == "") {
                        alert("Inserte el número de factura para registrar el dato del producto");
                    }
                    else if (txtRazonSocialFacProducto.GetText() == "") {
                        alert("Inserte el nombre del proveedor para registrar el dato del producto");
                    }
                    else if (txtEmpresaFacTransTe.GetText() == "") {
                        alert("Inserte el nombre de la empresa de transporte");
                    }
                    else if (txtFacTransTe.GetText() == "") {
                        alert("Inserte el numero de factura de transporte");
                    }
                    else if (numCostoUniFacTransTe == "") {
                        alert("El monto unitario de factura de transporte debe ser mayo a 0");
                    }
                    else if (numFleteFacTransTe == "") {
                        alert("El monto de flete de factura de transporte debe ser mayor a 0");
                    }
                    else if (txtPlantaDespachoFacTransTe.GetText() == "") {
                        alert("Inserte el nombre de la planta de despacho");
                    }
                    else if (numVolDespachadoFacTransTe == "") {
                        alert("El volumen de despacho debe ser mayor a 0");
                    }
                    else if (txtPlantaRecepcionFacTransTe.GetText() == "") {
                        alert("Inserte el nombre de la planta de recepción");
                    }
                    else if (numVolRecibidoFacTransTe == "") {
                        alert("El volumen de recepcion debe ser mayor a 0");
                    }
                    else {
                        validacion = 1;
                    }
                }
                else {
                    //sin implementar
                }

                if (txtSecFacTransFe.GetText() != "0") {

                    var numCostoUniFacTransFe = document.getElementById("<%=txtCostoUniFacTransFe.ClientID %>").value;
                    var numFleteFacTransFe = document.getElementById("<%=txtFleteFacTransFe.ClientID %>").value;
                    var numVolDespachadoFacTransFe = document.getElementById("<%=txtVolDespachadoFacTransFe.ClientID %>").value;
                    var numVolRecibidoFacTransFe = document.getElementById("<%=txtVolRecibidoFacTransFe.ClientID %>").value;

                    if (txtDuiFacProducto.GetText() == "") {
                        alert("Inserte el número de DUI para registrar el dato del producto");
                    }
                    else if (txtFacProducto.GetText() == "") {
                        alert("Inserte el número de factura para registrar el dato del producto");
                    }
                    else if (txtRazonSocialFacProducto.GetText() == "") {
                        alert("Inserte el nombre del proveedor para registrar el dato del producto");
                    }
                    else if (txtEmpresaFacTransFe.GetText() == "") {
                        alert("Inserte el nombre de la empresa de transporte");
                    }
                    else if (txtFacTransFe.GetText() == "") {
                        alert("Inserte el numero de factura de transporte");
                    }
                    else if (numCostoUniFacTransFe == "") {
                        alert("El monto unitario de factura de transporte debe ser mayo a 0");
                    }
                    else if (numFleteFacTransFe == "") {
                        alert("El monto de flete de factura de transporte debe ser mayor a 0");
                    }
                    else if (numVolDespachadoFacTransFe == "") {
                        alert("El volumen 1 debe ser mayor a 0");
                    }
                    else if (numVolRecibidoFacTransFe == "") {
                        alert("El volumen 2 debe ser mayor a 0");
                    }
                    else {
                        validacion = 1;
                    }
                }
                else {
                    //sin implementar
                }

                if (txtSecFacAlmacenajeIntermedio.GetText() != "0") {

                    var numCostoUniFacAlmacenajeIntermedio = document.getElementById("<%=txtCostoUniFacAlmacenajeIntermedio.ClientID %>").value;
                    var numVolRecibidoFacAlmacenajeIntermedio = document.getElementById("<%=txtVolRecibidoFacAlmacenajeIntermedio.ClientID %>").value;

                    if (txtDuiFacProducto.GetText() == "") {
                        alert("Inserte el número de DUI para registrar el dato del producto");
                    }
                    else if (txtFacAlmacenajeIntermedio.GetText() == "") {
                        alert("Inserte el numero de factura de almacenaje intermedio");
                    }
                    else if (txtEmpresaFacAlmacenajeIntermedio.GetText() == "") {
                        alert("Inserte el nombre de la empresa de almacenaje intermedio");
                    }
                    else if (numCostoUniFacAlmacenajeIntermedio == "") {
                        alert("El costo unitario de factura de almacenaje intermedio debe ser mayo a 0");
                    }
                    else if (numVolRecibidoFacAlmacenajeIntermedio == "") {
                        alert("El volumen recibido de la factura de almacenaje intermedio debe ser mayor a 0");
                    }
                    else {
                        validacion = 1;
                    }
                }
                else {
                    //sin implementar
                }

                if (txtSecFacAlmacenajeFinal.GetText() != "0") {

                    var numCostoUniFacAlmacenajeFinal = document.getElementById("<%=txtCostoUniFacAlmacenajeFinal.ClientID %>").value;
                    var numVolRecibidoFacAlmacenajeFinal = document.getElementById("<%=txtVolRecibidoFacAlmacenajeFinal.ClientID %>").value;

                    if (txtDuiFacProducto.GetText() == "") {
                        alert("Inserte el número de DUI para registrar el dato del producto");
                    }
                    else if (txtFacAlmacenajeFinal.GetText() == "") {
                        alert("Inserte el numero de factura de almacenaje intermedio");
                    }
                    else if (txtEmpresaAlmacenajeFinal.GetText() == "") {
                        alert("Inserte el nombre de la empresa de almacenaje intermedio");
                    }
                    else if (numCostoUniFacAlmacenajeFinal == "") {
                        alert("El costo unitario de factura de almacenaje intermedio debe ser mayo a 0");
                    }
                    else if (numVolRecibidoFacAlmacenajeFinal == "") {
                        alert("El volumen recibido de la factura de almacenaje intermedio debe ser mayor a 0");
                    }
                    else {
                        validacion = 1;
                    }
                }
                else {
                    //sin implementar
                }

                if (validacion == 1) {
                    txtIdDocEfImpEdt.SetValue(1);
                    dgvDocumentosImp.PerformCallback();
                    pudDocumentoImpAdd.Hide();
                    alert("Documento agregado");
                }
                else {
                    alert("No se tienen documentos para agregar");
                }


            }
            else if (cmbTipoFacturaImpAdd.GetValue() == 3) {

                var numGravamenFacAduana = document.getElementById("<%=txtGravamenFacAduana.ClientID %>").value;
                var numSiduineaFacAduana = document.getElementById("<%=txtSiduineaFacAduana.ClientID %>").value;
                var numDepArancelarioFacAduana = document.getElementById("<%=txtDepArancelarioFacAduana.ClientID %>").value;

                if (numGravamenFacAduana == "" && numSiduineaFacAduana == "" && numDepArancelarioFacAduana == "") {
                    alert("Ingrese por lo menos un valor de factura aduanera");
                }
                else if (numGravamenFacAduana == "") {
                    alert("El monto por gravamen no debe ser vacio");
                }
                else if (numSiduineaFacAduana == "") {
                    alert("El monto por siduinea no debe ser vacio");
                }
                else if (numDepArancelarioFacAduana == "") {
                    alert("El monto por deposito arancelario no debe ser vacio");
                }
                else if (txtDuiFacProducto.GetText() == "") {
                    alert("Ingrese el dato de la DUI");
                }
                else {
                    txtIdDocEfImpEdt.SetValue(1);
                    dgvDocumentosImp.PerformCallback();
                    pudDocumentoImpAdd.Hide();
                    alert("Documento agregado");
                }
            }
            else if (cmbTipoFacturaImpAdd.GetValue() == 4) {

                var numMontoFacSeguro = document.getElementById("<%=txtMontoFacSeguro.ClientID %>").value;

                    if (txtNumFacSeguro.GetText() == "" && txtNumFacSeguro.GetText() == "0") {
                        alert("Numero de Factura de Seguro no debe ser vacio o 0");
                    }
                    else if (txtEmpAseguradoraFacSeguro.GetText() == "") {
                        alert("Inserte el nombre de una Empresa Aseguradora");
                    }
                    else if (numMontoFacSeguro == "0") {
                        alert("Inserte el monto de la Factura de Seguro");
                    }
                    else if (txtDuiFacProducto.GetText() == "") {
                        alert("Inserte el dato de la DUI");
                    }
                    else {
                        txtIdDocEfImpEdt.SetValue(1);
                        dgvDocumentosImp.PerformCallback();
                        pudDocumentoImpAdd.Hide();
                        alert("Documento agregado");
                    }
                }
                else if (cmbTipoFacturaImpAdd.GetValue() == 5) {
                    alert("Opción no habilitada");
                }
                else if (cmbTipoFacturaImpAdd.GetValue() == 6) {
                    if (txtErrorMasivo.GetText() == "1") {
                        alert("El archivo tiene errores");
                    }
                    else {
                        txtIdDocEfImpEdt.SetValue(1);
                        dgvDocumentosImp.PerformCallback();
                        pudDocumentoImpAdd.Hide();
                        alert("Documento agregado");
                    }
                }
                else {
                    alert("Opción no habilitada");
                }
}
}

</script>

<div class="col-md-12">

    <table style="width: 100%; height: 100%">
        <tr>
            <td style="text-align: center; padding-bottom: 1px">
                <dx:ASPxRoundPanel runat="server" ShowHeader="False" Width="100%" ID="pnlTitulo">
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent1" runat="server">
                            <dx:ASPxLabel runat="server" ForeColor="#367B2F" ID="lblTitulo" Font-Bold="True" Font-Names="Rockwell" Font-Size="Larger" Text="DETALLE DE MOVIMIENTOS"></dx:ASPxLabel>
                        </dx:PanelContent>
                    </PanelCollection>
                    <Border BorderStyle="Ridge" BorderWidth="1px" BorderColor="#003300"></Border>
                </dx:ASPxRoundPanel>
            </td>
        </tr>

        <tr>
            <td style="height: 100%">

                <dx:ASPxSplitter ID="ASPxSplitter1" runat="server" Height="98%" Width="100%">
                    <Panes>
                        <dx:SplitterPane Size="15%" AllowResize="True" AutoHeight="True">
                            <ContentCollection>
                                <dx:SplitterContentControl ID="SplitterContentControl1" runat="server">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td style="padding-bottom: 5px">
                                                <dx:ASPxLabel ID="ASPxLabel60" runat="server" Font-Bold="True" Font-Names="Rockwell" ForeColor="#487B5C" Text="CARACTERISTICAS" Width="100%">
                                                    <BorderBottom BorderStyle="Solid" />
                                                </dx:ASPxLabel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding-bottom: 5px">
                                                <dx:ASPxCallbackPanel ID="pnlCabecera" runat="server" ClientInstanceName="pnlCabecera" Width="160px">
                                                    <PanelCollection>
                                                        <dx:PanelContent ID="PanelContent2" runat="server">
                                                            <table style="width: 100%">
                                                                <tr>
                                                                    <td>Fecha Inicial :
                                                        <dx:ASPxDateEdit ID="detFechaIni" runat="server" ClientInstanceName="detFechaIni" Theme="Metropolis" ToolTip="Seleccione la fecha inicial" Width="140px">
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
                                                                <tr style="padding-bottom: 5px">
                                                                    <td>Fecha Final :
                                                        <dx:ASPxDateEdit ID="detFechaFin" runat="server" ClientInstanceName="detFechaFin" Theme="Metropolis" Width="140px" ToolTip="Seleccione la fecha final">
                                                            <TimeSectionProperties>
                                                                <TimeEditProperties>
                                                                    <ClearButton Visibility="Auto">
                                                                    </ClearButton>
                                                                </TimeEditProperties>
                                                            </TimeSectionProperties>
                                                            <ClientSideEvents DateChanged="function(s, e) {
//pnlDatos.PerformCallback();
}" />
                                                            <ClearButton Visibility="Auto">
                                                            </ClearButton>
                                                        </dx:ASPxDateEdit>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </dx:PanelContent>
                                                    </PanelCollection>
                                                </dx:ASPxCallbackPanel>
                                            </td>
                                        </tr>


                                        <tr>
                                            <td style="padding-bottom: 3px">Selección Directa de Mes :
                                <uc1:CtrFecheroMes ID="cfmMes" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding-bottom: 5px">
                                                <dx:ASPxButton ID="btnConsultar" runat="server" AutoPostBack="False" Font-Bold="True" ForeColor="Maroon" Text="Obtener Información" Theme="Metropolis" Width="55%">
                                                    <ClientSideEvents Click="function(s, e) {
pnlDatos.PerformCallback();
}" />
                                                    <Image IconID="actions_download_16x16">
                                                    </Image>
                                                </dx:ASPxButton>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>
                                                <dx:ASPxLabel ID="ASPxLabel58" runat="server" ForeColor="#003300" Text="Proyectos Asignados :">
                                                </dx:ASPxLabel>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>
                                                <dx:ASPxTextBox ID="txtBuscador" runat="server" BackColor="#FFEFDF" ClientInstanceName="txtBuscador" Font-Size="Smaller" Theme="Metropolis" ToolTip="Buscador" Width="100%">
                                                    <BorderTop BorderColor="#FF6600" BorderStyle="Solid" />
                                                </dx:ASPxTextBox>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>
                                                <dx:ASPxButton ID="btnBuscarProyecto" runat="server" AutoPostBack="false" ClientInstanceName="btnBuscarProyecto" Text="Buscar Proyecto">
                                                    <ClientSideEvents Click="function(s, e) {
	                                                tlsOperadores.PerformCallback();
                                                }" />
                                                    <Image IconID="zoom_zoom_16x16">
                                                    </Image>
                                                </dx:ASPxButton>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>
                                                <dx:ASPxTreeList ID="tlsOperadores" runat="server" AutoGenerateColumns="False" ClientInstanceName="tlsOperadores" Font-Size="Smaller" Height="187px" KeyFieldName="intId" ParentFieldName="intIdPadre" Theme="Youthful" Styles-Header-BackColor="#317D40" ToolTip="Seleccione un proyecto" OnCustomCallback="tlsOperadores_CustomCallback">
                                                    <Columns>
                                                        <dx:TreeListTextColumn Caption="ID" FieldName="intId" ShowInCustomizationForm="True" Visible="False" VisibleIndex="0">
                                                        </dx:TreeListTextColumn>
                                                        <dx:TreeListTextColumn Caption="PADRE" FieldName="intIdPadre" ShowInCustomizationForm="True" Visible="False" VisibleIndex="1">
                                                        </dx:TreeListTextColumn>
                                                        <dx:TreeListTextColumn Caption="DESCRIPCION" FieldName="strDescripcion" ShowInCustomizationForm="True" VisibleIndex="2">
                                                        </dx:TreeListTextColumn>
                                                        <dx:TreeListTextColumn Caption="NIT" FieldName="lngNit" ShowInCustomizationForm="True" Visible="False" VisibleIndex="3">
                                                        </dx:TreeListTextColumn>
                                                        <dx:TreeListTextColumn Caption="LICENCIA" FieldName="lngLicencia" ShowInCustomizationForm="True" Visible="False" VisibleIndex="4">
                                                        </dx:TreeListTextColumn>
                                                        <dx:TreeListTextColumn Caption="REP_LEGAL" FieldName="strRepLegal" ShowInCustomizationForm="True" Visible="False" VisibleIndex="5">
                                                        </dx:TreeListTextColumn>
                                                        <dx:TreeListTextColumn Caption="DIRECCION" FieldName="strDireccion" ShowInCustomizationForm="True" Visible="False" VisibleIndex="6">
                                                        </dx:TreeListTextColumn>
                                                    </Columns>
                                                    <Settings ScrollableHeight="450" />
                                                    <SettingsBehavior AllowFocusedNode="True" FocusNodeOnExpandButtonClick="False" />
                                                    <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />

                                                    <Styles>
                                                        <Header BackColor="#317D40"></Header>
                                                    </Styles>

                                                    <ClientSideEvents NodeDblClick="function(s, e) {
pnlDatos.PerformCallback();
}" />
                                                </dx:ASPxTreeList>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>&nbsp;</td>
                                        </tr>

                                    </table>
                                </dx:SplitterContentControl>
                            </ContentCollection>
                        </dx:SplitterPane>
                        <dx:SplitterPane ScrollBars="Auto">
                            <ContentCollection>
                                <dx:SplitterContentControl ID="SplitterContentControl2" runat="server">
                                    <dx:ASPxCallbackPanel ID="pnlDatos" runat="server" Theme="Youthful" Width="100%" ClientInstanceName="pnlDatos" OnCallback="pnlDatos_Callback">
                                        <ClientSideEvents EndCallback="function(s, e) {
	grafficardatosprinc(s, e);
	LoadingPanel.Hide();
}"
                                            BeginCallback="function(s, e) {
	LoadingPanel.Show();
}" />
                                        <PanelCollection>
                                            <dx:PanelContent ID="PanelContent3" runat="server">
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td colspan="7" style="text-align: center; width: 100%;">
                                                            <dx:ASPxRoundPanel ID="pnlMensaje" runat="server" ShowHeader="False" Visible="False" Width="100%">
                                                                <PanelCollection>
                                                                    <dx:PanelContent ID="PanelContent4" runat="server">
                                                                        <dx:ASPxLabel ID="lblMensajeError" runat="server" ForeColor="#666666">
                                                                        </dx:ASPxLabel>
                                                                    </dx:PanelContent>
                                                                </PanelCollection>
                                                                <Border BorderStyle="Ridge" BorderWidth="1px" />
                                                            </dx:ASPxRoundPanel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="7">
                                                            <legend>
                                                                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Rockwell" Font-Size="Medium" ForeColor="#367B2F" Text="DATOS DEL PROYECTO"></asp:Label>
                                                            </legend>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3" style="width: 50%">
                                                            <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="#10420F" Text="NOMBRE COMERCIAL"></asp:Label>
                                                        </td>
                                                        <td style="width: 15%">
                                                            <asp:Label ID="Label3" runat="server" Font-Bold="True" ForeColor="#10420F" Text="NIT"></asp:Label>
                                                        </td>
                                                        <td style="width: 15%">
                                                            <asp:Label ID="Label5" runat="server" Font-Bold="True" ForeColor="#10420F" Text="DEPARTAMENTO"></asp:Label>
                                                        </td>
                                                        <td colspan="2" style="width: 20%">
                                                            <asp:Label ID="Label4" runat="server" Font-Bold="True" ForeColor="#10420F" Text="DIRECCION"></asp:Label>
                                                        </td>

                                                    </tr>
                                                    <tr>
                                                        <td colspan="3" style="width: 50%">
                                                            <dx:ASPxLabel ID="lblRazonSocial" runat="server" ClientInstanceName="lblRazonSocial">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 15%">
                                                            <asp:Label ID="lblNit" runat="server"></asp:Label>
                                                        </td>
                                                        <td style="width: 15%">
                                                            <asp:Label ID="lblDepto" runat="server"></asp:Label>
                                                        </td>
                                                        <td colspan="2" style="width: 20%">
                                                            <asp:Label ID="lblRepLegal" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="7">

                                                            <dx:ASPxPageControl ID="pctPanel" runat="server" ActiveTabIndex="0" Font-Names="Verdana" Font-Size="Smaller" Theme="Metropolis" Width="100%">
                                                                <TabPages>
                                                                    <dx:TabPage Text="MOVIMIENTO ACUMULADO">
                                                                        <ContentCollection>
                                                                            <dx:ContentControl ID="ContentControl1" runat="server">
                                                                                <table style="width: 100%">
                                                                                    <tr>
                                                                                        <td style="width: 40%"></td>
                                                                                        <td style="width: 15%">
                                                                                            <dx:ASPxButton ID="btnAnularSaldoFisico" runat="server" AutoPostBack="False" Text="Anular Saldo Fisico(Medicion)" Visible="false" Font-Bold="True" ForeColor="Maroon" Theme="Metropolis">
                                                                                                <ClientSideEvents Click="function(s, e) {
                                                                                                                    pucSaldosFisco.Show();    
                                                                                                    pucSaldosFisco.PerformCallback();                                                                                                                                                                                                                        
                                                                                                  }" />
                                                                                                <Image IconID="reports_deletegroupfooter_16x16office2013">
                                                                                                </Image>
                                                                                            </dx:ASPxButton>
                                                                                        </td>
                                                                                        <td style="width: 15%">
                                                                                            <dx:ASPxButton ID="btnSaldoFisico" runat="server" AutoPostBack="False" Text="Registrar Saldo Fisico(Medicion)" Visible="false" Font-Bold="True" ForeColor="Maroon" Theme="Metropolis">
                                                                                                <ClientSideEvents Click="function(s, e) {
                                                                                                                    pucDJRecepciones.Show();                                                                                                        
                                                                                                                    pucDJRecepciones.PerformCallback();
                                                                                                  }" />
                                                                                                 <Image IconID="scheduling_timelineview_16x16office2013">
                                                                                                </Image>
                                                                                            </dx:ASPxButton>

                                                                                        </td>
                                                                                        <td style="width: 15%">
                                                                                             <dx:ASPxButton ID="btnRegistrarPrecio" runat="server" AutoPostBack="False" Text="Precio de Productos Importados" Visible="false" Font-Bold="True" ForeColor="Maroon" Theme="Metropolis">
                                                                                                <ClientSideEvents Click="function(s, e) {
                                                                                                                    pucRegistroPrecio.Show();                                                                                                        
                                                                                                                    pucRegistroPrecio.PerformCallback();
                                                                                                  }" />
                                                                                                 <Image IconID="reports_addfooter_16x16office2013">
                                                                                                </Image>
                                                                                            </dx:ASPxButton>
                                                                                        </td>
                                                                                        <td style="width: 15%">
                                                                                            <table>
                                                                                                <tr>

                                                                                                    <td>
                                                                                                        <dx:ASPxButton ID="ASPxButton2" runat="server" AutoPostBack="False" Text="agregar producto" Visible="false">
                                                                                                            <ClientSideEvents Click="function(s, e) {
                                                                                                                    pucConfig.Show();
                                                                                                        
                                                                                                                    pucConfig.PerformCallback();
                                                                                                  }" />
                                                                                                        </dx:ASPxButton>
                                                                                                    </td>
                                                                                                    <td>                                                                                                    
                                                                                                        <dx:ASPxButton ID="btnExportarAcum" runat="server" AutoPostBack="False" Font-Bold="True" ForeColor="Maroon" OnClick="btnExportarAcum_Click" Text="Exportar Excel" Theme="Metropolis" Width="200px">
                                                                                                            <ClientSideEvents Click="function(s, e) {
	                                                                                            e.processOnServer = confirm('¿Desea exportar el resumen acumulado?'); 
            }" />
                                                                                                            <Image IconID="export_exporttoxls_16x16">
                                                                                                            </Image>
                                                                                                        </dx:ASPxButton>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>






                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td colspan="5">

                                                                                            <dx:ASPxGridView ID="dgvAcumulado" runat="server" AutoGenerateColumns="False" ClientInstanceName="dgvAcumulado" EnableTheming="True" Font-Size="Smaller" KeyFieldName="IntId" OnHtmlDataCellPrepared="dgvAcumulado_HtmlDataCellPrepared" OnHtmlRowPrepared="dgvAcumulado_HtmlRowPrepared" Theme="Default" Width="100%">
                                                                                                <Styles>
                                                                                                    <Header Wrap="True" HorizontalAlign="Center"></Header>

                                                                                                    <AlternatingRow BackColor="#EEEEEE"></AlternatingRow>
                                                                                                </Styles>
                                                                                                <ClientSideEvents CustomButtonClick="function(s, e) { console.log(s); console.log(e); console.log(this);
	
    switch(e.buttonID) {
     case&nbsp; 'btnMed':
    onCellClick(s.GetFocusedRowIndex(), 'MDIN',this);
    break;
    } 


}" />
                                                                                                <SettingsContextMenu EnableRowMenu="False">
                                                                                                </SettingsContextMenu>
                                                                                                <Templates>
                                                                                                    <EditForm>
                                                                                                    </EditForm>
                                                                                                </Templates>
                                                                                                <SettingsPager PageSize="100">
                                                                                                    <PageSizeItemSettings Items="50, 100, 200, 500, 1000" Visible="True">
                                                                                                    </PageSizeItemSettings>
                                                                                                </SettingsPager>
                                                                                                <SettingsEditing Mode="Inline">
                                                                                                </SettingsEditing>
                                                                                                <Settings ShowFooter="True" ShowGroupFooter="VisibleAlways" ShowGroupPanel="True" ShowHeaderFilterButton="True" />
                                                                                                <SettingsBehavior AllowFocusedRow="True" ColumnResizeMode="NextColumn" />
                                                                                                <SettingsPopup>
                                                                                                    <EditForm Modal="True" />
                                                                                                    <CustomizationWindow HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" />
                                                                                                </SettingsPopup>
                                                                                                <SettingsText CommandNew="Nuevo" />
                                                                                                <EditFormLayoutProperties ColCount="2">
                                                                                                    <Items>
                                                                                                        <dx:GridViewColumnLayoutItem ColumnName="StrFecha">
                                                                                                        </dx:GridViewColumnLayoutItem>
                                                                                                        <dx:GridViewColumnLayoutItem ColumnName="StrCodProducto">
                                                                                                        </dx:GridViewColumnLayoutItem>
                                                                                                        <dx:GridViewColumnLayoutItem ColumnName="DecSaldoInicial">
                                                                                                        </dx:GridViewColumnLayoutItem>
                                                                                                        <dx:GridViewColumnLayoutItem ColumnName="DecImporteTotal">
                                                                                                        </dx:GridViewColumnLayoutItem>
                                                                                                        <dx:GridViewColumnLayoutItem ColumnName="DecTotalIng">
                                                                                                        </dx:GridViewColumnLayoutItem>
                                                                                                        <dx:GridViewColumnLayoutItem ColumnName="DecTotalSal">
                                                                                                        </dx:GridViewColumnLayoutItem>
                                                                                                        <dx:GridViewColumnLayoutItem ColumnName="DecSaldoFinal">
                                                                                                        </dx:GridViewColumnLayoutItem>

                                                                                                        <dx:EditModeCommandLayoutItem ColSpan="2" HorizontalAlign="Right">
                                                                                                        </dx:EditModeCommandLayoutItem>
                                                                                                    </Items>
                                                                                                </EditFormLayoutProperties>
                                                                                                <Columns>
                                                                                                    <dx:GridViewCommandColumn ShowInCustomizationForm="True" VisibleIndex="51">
                                                                                                        <CustomButtons>
                                                                                                            <dx:GridViewCommandColumnCustomButton ID="btnMed" Text=" ">
                                                                                                                <Image IconID="scheduling_timelineview_16x16office2013" ToolTip="Registrar Saldo Fisico (Medición)">
                                                                                                                </Image>
                                                                                                            </dx:GridViewCommandColumnCustomButton>
                                                                                                        </CustomButtons>
                                                                                                    </dx:GridViewCommandColumn>
                                                                                                    <dx:GridViewDataTextColumn Caption="IntId" FieldName="IntId" ShowInCustomizationForm="True" Visible="False" VisibleIndex="0">
                                                                                                    </dx:GridViewDataTextColumn>
                                                                                                    <dx:GridViewDataTextColumn Caption="IntContProd" FieldName="IntContProd" ShowInCustomizationForm="True" Visible="False" VisibleIndex="1">
                                                                                                    </dx:GridViewDataTextColumn>
                                                                                                    <dx:GridViewDataTextColumn Caption="FECHA" FieldName="StrFecha" ShowInCustomizationForm="True" VisibleIndex="2">
                                                                                                    </dx:GridViewDataTextColumn>
                                                                                                    <dx:GridViewDataTextColumn Caption="PROD." FieldName="StrCodProducto" GroupIndex="0" ShowInCustomizationForm="True" SortIndex="0" SortOrder="Ascending" ToolTip="Producto" VisibleIndex="3">
                                                                                                    </dx:GridViewDataTextColumn>
                                                                                                    <dx:GridViewDataTextColumn Caption="TIPO INFRA." FieldName="StrTipoInfraestructura" GroupIndex="1" ShowInCustomizationForm="True" SortIndex="1" SortOrder="Ascending" ToolTip="Tipo de Infrastructura" VisibleIndex="4">
                                                                                                    </dx:GridViewDataTextColumn>
                                                                                                    <dx:GridViewDataTextColumn Caption="COD. INFRA." FieldName="StrCodigoInfraestructura" GroupIndex="2" ShowInCustomizationForm="True" SortIndex="2" SortOrder="Ascending" ToolTip="Código de Infraestructura" VisibleIndex="5">
                                                                                                    </dx:GridViewDataTextColumn>
                                                                                                    <dx:GridViewDataTextColumn Caption="SALDO INICIAL" FieldName="DecSaldoInicial" ShowInCustomizationForm="True" VisibleIndex="6">
                                                                                                    </dx:GridViewDataTextColumn>
                                                                                                    <dx:GridViewDataTextColumn Caption="DecIng01" FieldName="DecIng01" ShowInCustomizationForm="True" Visible="False" VisibleIndex="7">
                                                                                                        <PropertiesTextEdit ClientInstanceName="DecInf01" EnableClientSideAPI="True">
                                                                                                        </PropertiesTextEdit>
                                                                                                    </dx:GridViewDataTextColumn>
                                                                                                    <dx:GridViewDataTextColumn Caption="DecIng02" FieldName="DecIng02" ShowInCustomizationForm="True" Visible="False" VisibleIndex="8">
                                                                                                    </dx:GridViewDataTextColumn>
                                                                                                    <dx:GridViewDataTextColumn Caption="DecIng03" FieldName="DecIng03" ShowInCustomizationForm="True" Visible="False" VisibleIndex="9">
                                                                                                    </dx:GridViewDataTextColumn>
                                                                                                    <dx:GridViewDataTextColumn Caption="DecIng04" FieldName="DecIng04" ShowInCustomizationForm="True" Visible="False" VisibleIndex="10">
                                                                                                    </dx:GridViewDataTextColumn>
                                                                                                    <dx:GridViewDataTextColumn Caption="DecIng05" FieldName="DecIng05" ShowInCustomizationForm="True" Visible="False" VisibleIndex="11">
                                                                                                    </dx:GridViewDataTextColumn>
                                                                                                    <dx:GridViewDataTextColumn Caption="DecIng06" FieldName="DecIng06" ShowInCustomizationForm="True" Visible="False" VisibleIndex="12">
                                                                                                    </dx:GridViewDataTextColumn>
                                                                                                    <dx:GridViewDataTextColumn Caption="DecIng07" FieldName="DecIng07" ShowInCustomizationForm="True" Visible="False" VisibleIndex="13">
                                                                                                    </dx:GridViewDataTextColumn>
                                                                                                    <dx:GridViewDataTextColumn Caption="DecIng08" FieldName="DecIng08" ShowInCustomizationForm="True" Visible="False" VisibleIndex="14">
                                                                                                    </dx:GridViewDataTextColumn>
                                                                                                    <dx:GridViewDataTextColumn Caption="DecIng09" FieldName="DecIng09" ShowInCustomizationForm="True" Visible="False" VisibleIndex="15">
                                                                                                    </dx:GridViewDataTextColumn>
                                                                                                    <dx:GridViewDataTextColumn Caption="DecIng10" FieldName="DecIng10" ShowInCustomizationForm="True" Visible="False" VisibleIndex="16">
                                                                                                    </dx:GridViewDataTextColumn>
                                                                                                    <dx:GridViewDataTextColumn Caption="DecIng11" FieldName="DecIng11" ShowInCustomizationForm="True" Visible="False" VisibleIndex="17">
                                                                                                    </dx:GridViewDataTextColumn>
                                                                                                    <dx:GridViewDataTextColumn Caption="DecIng12" FieldName="DecIng12" ShowInCustomizationForm="True" Visible="False" VisibleIndex="18">
                                                                                                    </dx:GridViewDataTextColumn>
                                                                                                    <dx:GridViewDataTextColumn Caption="DecIng13" FieldName="DecIng13" ShowInCustomizationForm="True" Visible="False" VisibleIndex="19">
                                                                                                    </dx:GridViewDataTextColumn>
                                                                                                    <dx:GridViewDataTextColumn Caption="DecIng14" FieldName="DecIng14" ShowInCustomizationForm="True" Visible="False" VisibleIndex="20">
                                                                                                    </dx:GridViewDataTextColumn>
                                                                                                    <dx:GridViewDataTextColumn Caption="DecIng15" FieldName="DecIng15" ShowInCustomizationForm="True" Visible="False" VisibleIndex="21">
                                                                                                    </dx:GridViewDataTextColumn>
                                                                                                    <dx:GridViewDataTextColumn Caption="DecIng16" FieldName="DecIng16" ShowInCustomizationForm="True" Visible="False" VisibleIndex="22">
                                                                                                    </dx:GridViewDataTextColumn>
                                                                                                    <dx:GridViewDataTextColumn Caption="DecIng17" FieldName="DecIng17" ShowInCustomizationForm="True" Visible="False" VisibleIndex="23">
                                                                                                    </dx:GridViewDataTextColumn>
                                                                                                    <dx:GridViewDataTextColumn Caption="DecIng18" FieldName="DecIng18" ShowInCustomizationForm="True" Visible="False" VisibleIndex="24">
                                                                                                    </dx:GridViewDataTextColumn>
                                                                                                    <dx:GridViewDataTextColumn Caption="DecIng19" FieldName="DecIng19" ShowInCustomizationForm="True" Visible="False" VisibleIndex="25">
                                                                                                    </dx:GridViewDataTextColumn>
                                                                                                    <dx:GridViewDataTextColumn Caption="DecIng20" FieldName="DecIng20" ShowInCustomizationForm="True" Visible="False" VisibleIndex="26">
                                                                                                    </dx:GridViewDataTextColumn>
                                                                                                    <dx:GridViewDataTextColumn Caption="TOTAL ING." FieldName="DecTotalIng" ShowInCustomizationForm="True" ToolTip="Total de Ingresos" VisibleIndex="27">
                                                                                                    </dx:GridViewDataTextColumn>
                                                                                                    <dx:GridViewDataTextColumn Caption="DecSal01" FieldName="DecSal01" ShowInCustomizationForm="True" Visible="False" VisibleIndex="28">
                                                                                                    </dx:GridViewDataTextColumn>
                                                                                                    <dx:GridViewDataTextColumn Caption="DecSal02" FieldName="DecSal02" ShowInCustomizationForm="True" Visible="False" VisibleIndex="29">
                                                                                                    </dx:GridViewDataTextColumn>
                                                                                                    <dx:GridViewDataTextColumn Caption="DecSal03" FieldName="DecSal03" ShowInCustomizationForm="True" Visible="False" VisibleIndex="30">
                                                                                                    </dx:GridViewDataTextColumn>
                                                                                                    <dx:GridViewDataTextColumn Caption="DecSal04" FieldName="DecSal04" ShowInCustomizationForm="True" Visible="False" VisibleIndex="31">
                                                                                                    </dx:GridViewDataTextColumn>
                                                                                                    <dx:GridViewDataTextColumn Caption="DecSal05" FieldName="DecSal05" ShowInCustomizationForm="True" Visible="False" VisibleIndex="32">
                                                                                                    </dx:GridViewDataTextColumn>
                                                                                                    <dx:GridViewDataTextColumn Caption="DecSal06" FieldName="DecSal06" ShowInCustomizationForm="True" Visible="False" VisibleIndex="33">
                                                                                                    </dx:GridViewDataTextColumn>
                                                                                                    <dx:GridViewDataTextColumn Caption="DecSal07" FieldName="DecSal07" ShowInCustomizationForm="True" Visible="False" VisibleIndex="34">
                                                                                                    </dx:GridViewDataTextColumn>
                                                                                                    <dx:GridViewDataTextColumn Caption="DecSal08" FieldName="DecSal08" ShowInCustomizationForm="True" Visible="False" VisibleIndex="35">
                                                                                                    </dx:GridViewDataTextColumn>
                                                                                                    <dx:GridViewDataTextColumn Caption="DecSal09" FieldName="DecSal09" ShowInCustomizationForm="True" Visible="False" VisibleIndex="36">
                                                                                                    </dx:GridViewDataTextColumn>
                                                                                                    <dx:GridViewDataTextColumn Caption="DecSal10" FieldName="DecSal10" ShowInCustomizationForm="True" Visible="False" VisibleIndex="37">
                                                                                                    </dx:GridViewDataTextColumn>
                                                                                                    <dx:GridViewDataTextColumn Caption="DecSal11" FieldName="DecSal11" ShowInCustomizationForm="True" Visible="False" VisibleIndex="38">
                                                                                                    </dx:GridViewDataTextColumn>
                                                                                                    <dx:GridViewDataTextColumn Caption="DecSal12" FieldName="DecSal12" ShowInCustomizationForm="True" Visible="False" VisibleIndex="39">
                                                                                                    </dx:GridViewDataTextColumn>
                                                                                                    <dx:GridViewDataTextColumn Caption="DecSal13" FieldName="DecSal13" ShowInCustomizationForm="True" Visible="False" VisibleIndex="40">
                                                                                                    </dx:GridViewDataTextColumn>
                                                                                                    <dx:GridViewDataTextColumn Caption="DecSal14" FieldName="DecSal14" ShowInCustomizationForm="True" Visible="False" VisibleIndex="41">
                                                                                                    </dx:GridViewDataTextColumn>
                                                                                                    <dx:GridViewDataTextColumn Caption="DecSal15" FieldName="DecSal15" ShowInCustomizationForm="True" Visible="False" VisibleIndex="42">
                                                                                                    </dx:GridViewDataTextColumn>
                                                                                                    <dx:GridViewDataTextColumn Caption="DecSal16" FieldName="DecSal16" ShowInCustomizationForm="True" Visible="False" VisibleIndex="43">
                                                                                                    </dx:GridViewDataTextColumn>
                                                                                                    <dx:GridViewDataTextColumn Caption="DecSal17" FieldName="DecSal17" ShowInCustomizationForm="True" Visible="False" VisibleIndex="44">
                                                                                                    </dx:GridViewDataTextColumn>
                                                                                                    <dx:GridViewDataTextColumn Caption="DecSal18" FieldName="DecSal18" ShowInCustomizationForm="True" Visible="False" VisibleIndex="45">
                                                                                                    </dx:GridViewDataTextColumn>
                                                                                                    <dx:GridViewDataTextColumn Caption="DecSal19" FieldName="DecSal19" ShowInCustomizationForm="True" Visible="False" VisibleIndex="46">
                                                                                                    </dx:GridViewDataTextColumn>
                                                                                                    <dx:GridViewDataTextColumn Caption="DecSal20" FieldName="DecSal20" ShowInCustomizationForm="True" Visible="False" VisibleIndex="47">
                                                                                                    </dx:GridViewDataTextColumn>
                                                                                                    <dx:GridViewDataTextColumn Caption="TOTAL SAL." FieldName="DecTotalSal" ShowInCustomizationForm="True" ToolTip="Total de Salidas" VisibleIndex="48">
                                                                                                    </dx:GridViewDataTextColumn>
                                                                                                    <dx:GridViewDataTextColumn Caption="SALDO FINAL" FieldName="DecSaldoFinal" ShowInCustomizationForm="True" VisibleIndex="49">
                                                                                                    </dx:GridViewDataTextColumn>
                                                                                                    <dx:GridViewDataTextColumn Caption="IMPORTE TOTAL BS." FieldName="DecImporteTotal" ShowInCustomizationForm="True" Visible="False" VisibleIndex="50">
                                                                                                    </dx:GridViewDataTextColumn>
                                                                                                    <dx:GridViewDataTextColumn Caption="PRODUCTO" FieldName="StrProducto" Visible="False" VisibleIndex="52">
                                                                                                    </dx:GridViewDataTextColumn>
                                                                                                    <dx:GridViewDataTextColumn Caption="INFRA" FieldName="StrInfraestructura" Visible="False" VisibleIndex="53">
                                                                                                    </dx:GridViewDataTextColumn>
                                                                                                    <dx:GridViewDataTextColumn Caption="RESOLUCION" GroupIndex="1" ShowInCustomizationForm="True" FieldName="StrObservaciones" Visible="False" VisibleIndex="54">
                                                                                                    </dx:GridViewDataTextColumn>

                                                                                                </Columns>
                                                                                                <Styles CssPostfix="DevGlp">
                                                                                                    <AlternatingRow BackColor="#EEEEEE">
                                                                                                    </AlternatingRow>
                                                                                                </Styles>
                                                                                                <TotalSummary>
                                                                                                    <dx:ASPxSummaryItem FieldName="DecIng01" ShowInColumn="DecIng01" SummaryType="Sum" Tag="DecIng01" DisplayFormat="{0}" />
                                                                                                    <dx:ASPxSummaryItem FieldName="DecIng02" ShowInColumn="DecIng02" SummaryType="Sum" Tag="DecIng02" DisplayFormat="{0}" />
                                                                                                    <dx:ASPxSummaryItem FieldName="DecIng03" ShowInColumn="DecIng03" SummaryType="Sum" Tag="DecIng03" DisplayFormat="{0}" />
                                                                                                    <dx:ASPxSummaryItem FieldName="DecIng04" ShowInColumn="DecIng04" SummaryType="Sum" Tag="DecIng04" DisplayFormat="{0}" />
                                                                                                    <dx:ASPxSummaryItem FieldName="DecIng05" ShowInColumn="DecIng05" SummaryType="Sum" Tag="DecIng05" DisplayFormat="{0}" />
                                                                                                    <dx:ASPxSummaryItem FieldName="DecIng06" ShowInColumn="DecIng06" SummaryType="Sum" Tag="DecIng06" DisplayFormat="{0}" />
                                                                                                    <dx:ASPxSummaryItem FieldName="DecIng07" ShowInColumn="DecIng07" SummaryType="Sum" Tag="DecIng07" DisplayFormat="{0}" />
                                                                                                    <dx:ASPxSummaryItem FieldName="DecIng08" ShowInColumn="DecIng08" SummaryType="Sum" Tag="DecIng08" DisplayFormat="{0}" />
                                                                                                    <dx:ASPxSummaryItem FieldName="DecIng09" ShowInColumn="DecIng09" SummaryType="Sum" Tag="DecIng09" DisplayFormat="{0}" />
                                                                                                    <dx:ASPxSummaryItem FieldName="DecIng10" ShowInColumn="DecIng10" SummaryType="Sum" Tag="DecIng10" DisplayFormat="{0}" />
                                                                                                    <dx:ASPxSummaryItem FieldName="DecIng11" ShowInColumn="DecIng11" SummaryType="Sum" Tag="DecIng11" DisplayFormat="{0}" />
                                                                                                    <dx:ASPxSummaryItem FieldName="DecIng12" ShowInColumn="DecIng12" SummaryType="Sum" Tag="DecIng12" DisplayFormat="{0}" />
                                                                                                    <dx:ASPxSummaryItem FieldName="DecIng13" ShowInColumn="DecIng13" SummaryType="Sum" Tag="DecIng13" DisplayFormat="{0}" />
                                                                                                    <dx:ASPxSummaryItem FieldName="DecIng14" ShowInColumn="DecIng14" SummaryType="Sum" Tag="DecIng14" DisplayFormat="{0}" />
                                                                                                    <dx:ASPxSummaryItem FieldName="DecIng15" ShowInColumn="DecIng15" SummaryType="Sum" Tag="DecIng15" DisplayFormat="{0}" />
                                                                                                    <dx:ASPxSummaryItem FieldName="DecIng16" ShowInColumn="DecIng16" SummaryType="Sum" Tag="DecIng16" DisplayFormat="{0}" />
                                                                                                    <dx:ASPxSummaryItem FieldName="DecIng17" ShowInColumn="DecIng17" SummaryType="Sum" Tag="DecIng17" DisplayFormat="{0}" />
                                                                                                    <dx:ASPxSummaryItem FieldName="DecIng18" ShowInColumn="DecIng18" SummaryType="Sum" Tag="DecIng18" DisplayFormat="{0}" />
                                                                                                    <dx:ASPxSummaryItem FieldName="DecIng19" ShowInColumn="DecIng19" SummaryType="Sum" Tag="DecIng19" DisplayFormat="{0}" />
                                                                                                    <dx:ASPxSummaryItem FieldName="DecIng20" ShowInColumn="DecIng20" SummaryType="Sum" Tag="DecIng20" DisplayFormat="{0}" />
                                                                                                    <dx:ASPxSummaryItem FieldName="DecTotalIng" ShowInColumn="DecTotalIng" SummaryType="Sum" Tag="DecTotalIng" DisplayFormat="{0}" />
                                                                                                    <dx:ASPxSummaryItem FieldName="DecSal01" ShowInColumn="DecSal01" SummaryType="Sum" Tag="DecSal01" DisplayFormat="{0}" />
                                                                                                    <dx:ASPxSummaryItem FieldName="DecSal02" ShowInColumn="DecSal02" SummaryType="Sum" Tag="DecSal02" DisplayFormat="{0}" />
                                                                                                    <dx:ASPxSummaryItem FieldName="DecSal03" ShowInColumn="DecSal03" SummaryType="Sum" Tag="DecSal03" DisplayFormat="{0}" />
                                                                                                    <dx:ASPxSummaryItem FieldName="DecSal04" ShowInColumn="DecSal04" SummaryType="Sum" Tag="DecSal04" DisplayFormat="{0}" />
                                                                                                    <dx:ASPxSummaryItem FieldName="DecSal05" ShowInColumn="DecSal05" SummaryType="Sum" Tag="DecSal05" DisplayFormat="{0}" />
                                                                                                    <dx:ASPxSummaryItem FieldName="DecSal06" ShowInColumn="DecSal06" SummaryType="Sum" Tag="DecSal06" DisplayFormat="{0}" />
                                                                                                    <dx:ASPxSummaryItem FieldName="DecSal07" ShowInColumn="DecSal07" SummaryType="Sum" Tag="DecSal07" DisplayFormat="{0}" />
                                                                                                    <dx:ASPxSummaryItem FieldName="DecSal08" ShowInColumn="DecSal08" SummaryType="Sum" Tag="DecSal08" DisplayFormat="{0}" />
                                                                                                    <dx:ASPxSummaryItem FieldName="DecSal09" ShowInColumn="DecSal09" SummaryType="Sum" Tag="DecSal09" DisplayFormat="{0}" />
                                                                                                    <dx:ASPxSummaryItem FieldName="DecSal10" ShowInColumn="DecSal10" SummaryType="Sum" Tag="DecSal10" DisplayFormat="{0}" />
                                                                                                    <dx:ASPxSummaryItem FieldName="DecSal11" ShowInColumn="DecSal11" SummaryType="Sum" Tag="DecSal11" DisplayFormat="{0}" />
                                                                                                    <dx:ASPxSummaryItem FieldName="DecSal12" ShowInColumn="DecSal12" SummaryType="Sum" Tag="DecSal12" DisplayFormat="{0}" />
                                                                                                    <dx:ASPxSummaryItem FieldName="DecSal13" ShowInColumn="DecSal13" SummaryType="Sum" Tag="DecSal13" DisplayFormat="{0}" />
                                                                                                    <dx:ASPxSummaryItem FieldName="DecSal14" ShowInColumn="DecSal14" SummaryType="Sum" Tag="DecSal14" DisplayFormat="{0}" />
                                                                                                    <dx:ASPxSummaryItem FieldName="DecSal15" ShowInColumn="DecSal15" SummaryType="Sum" Tag="DecSal15" DisplayFormat="{0}" />
                                                                                                    <dx:ASPxSummaryItem FieldName="DecSal16" ShowInColumn="DecSal16" SummaryType="Sum" Tag="DecSal16" DisplayFormat="{0}" />
                                                                                                    <dx:ASPxSummaryItem FieldName="DecSal17" ShowInColumn="DecSal17" SummaryType="Sum" Tag="DecSal17" DisplayFormat="{0}" />
                                                                                                    <dx:ASPxSummaryItem FieldName="DecSal18" ShowInColumn="DecSal18" SummaryType="Sum" Tag="DecSal18" DisplayFormat="{0}" />
                                                                                                    <dx:ASPxSummaryItem FieldName="DecSal19" ShowInColumn="DecSal19" SummaryType="Sum" Tag="DecSal19" DisplayFormat="{0}" />
                                                                                                    <dx:ASPxSummaryItem FieldName="DecSal20" ShowInColumn="DecSal20" SummaryType="Sum" Tag="DecSal20" DisplayFormat="{0}" />
                                                                                                    <dx:ASPxSummaryItem FieldName="DecTotalSal" ShowInColumn="DecTotalSal" SummaryType="Sum" Tag="DecTotalSal" DisplayFormat="{0}" />
                                                                                                    <dx:ASPxSummaryItem FieldName="DecImporteTotal" ShowInColumn="DecImporteTotal" SummaryType="Sum" Tag="DecImporteTotal" DisplayFormat="{0}" />
                                                                                                </TotalSummary>
                                                                                                <GroupSummary>
                                                                                                    <dx:ASPxSummaryItem FieldName="DecIng01" ShowInGroupFooterColumn="DecIng01" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                                    <dx:ASPxSummaryItem FieldName="DecIng02" ShowInGroupFooterColumn="DecIng02" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                                    <dx:ASPxSummaryItem FieldName="DecIng03" ShowInGroupFooterColumn="DecIng03" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                                    <dx:ASPxSummaryItem FieldName="DecIng04" ShowInGroupFooterColumn="DecIng04" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                                    <dx:ASPxSummaryItem FieldName="DecIng05" ShowInGroupFooterColumn="DecIng05" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                                    <dx:ASPxSummaryItem FieldName="DecIng06" ShowInGroupFooterColumn="DecIng06" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                                    <dx:ASPxSummaryItem FieldName="DecIng07" ShowInGroupFooterColumn="DecIng07" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                                    <dx:ASPxSummaryItem FieldName="DecIng08" ShowInGroupFooterColumn="DecIng08" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                                    <dx:ASPxSummaryItem FieldName="DecIng09" ShowInGroupFooterColumn="DecIng09" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                                    <dx:ASPxSummaryItem FieldName="DecIng10" ShowInGroupFooterColumn="DecIng10" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                                    <dx:ASPxSummaryItem FieldName="DecIng11" ShowInGroupFooterColumn="DecIng11" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                                    <dx:ASPxSummaryItem FieldName="DecIng12" ShowInGroupFooterColumn="DecIng12" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                                    <dx:ASPxSummaryItem FieldName="DecIng13" ShowInGroupFooterColumn="DecIng13" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                                    <dx:ASPxSummaryItem FieldName="DecIng14" ShowInGroupFooterColumn="DecIng14" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                                    <dx:ASPxSummaryItem FieldName="DecIng15" ShowInGroupFooterColumn="DecIng15" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                                    <dx:ASPxSummaryItem FieldName="DecIng16" ShowInGroupFooterColumn="DecIng16" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                                    <dx:ASPxSummaryItem FieldName="DecIng17" ShowInGroupFooterColumn="DecIng17" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                                    <dx:ASPxSummaryItem FieldName="DecIng18" ShowInGroupFooterColumn="DecIng18" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                                    <dx:ASPxSummaryItem FieldName="DecIng19" ShowInGroupFooterColumn="DecIng19" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                                    <dx:ASPxSummaryItem FieldName="DecIng20" ShowInGroupFooterColumn="DecIng20" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                                    <dx:ASPxSummaryItem FieldName="DecTotalIng" ShowInGroupFooterColumn="DecTotalIng" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                                    <dx:ASPxSummaryItem FieldName="DecSal01" ShowInGroupFooterColumn="DecSal01" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                                    <dx:ASPxSummaryItem FieldName="DecSal02" ShowInGroupFooterColumn="DecSal02" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                                    <dx:ASPxSummaryItem FieldName="DecSal03" ShowInGroupFooterColumn="DecSal03" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                                    <dx:ASPxSummaryItem FieldName="DecSal04" ShowInGroupFooterColumn="DecSal04" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                                    <dx:ASPxSummaryItem FieldName="DecSal05" ShowInGroupFooterColumn="DecSal05" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                                    <dx:ASPxSummaryItem FieldName="DecSal06" ShowInGroupFooterColumn="DecSal06" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                                    <dx:ASPxSummaryItem FieldName="DecSal07" ShowInGroupFooterColumn="DecSal07" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                                    <dx:ASPxSummaryItem FieldName="DecSal08" ShowInGroupFooterColumn="DecSal08" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                                    <dx:ASPxSummaryItem FieldName="DecSal09" ShowInGroupFooterColumn="DecSal09" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                                    <dx:ASPxSummaryItem FieldName="DecSal10" ShowInGroupFooterColumn="DecSal1" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                                    <dx:ASPxSummaryItem FieldName="DecSal11" ShowInGroupFooterColumn="DecSal11" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                                    <dx:ASPxSummaryItem FieldName="DecSal12" ShowInGroupFooterColumn="DecSal12" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                                    <dx:ASPxSummaryItem FieldName="DecSal13" ShowInGroupFooterColumn="DecSal13" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                                    <dx:ASPxSummaryItem FieldName="DecSal14" ShowInGroupFooterColumn="DecSal14" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                                    <dx:ASPxSummaryItem FieldName="DecSal15" ShowInGroupFooterColumn="DecSal15" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                                    <dx:ASPxSummaryItem FieldName="DecSal16" ShowInGroupFooterColumn="DecSal16" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                                    <dx:ASPxSummaryItem FieldName="DecSal17" ShowInGroupFooterColumn="DecSal17" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                                    <dx:ASPxSummaryItem FieldName="DecSal18" ShowInGroupFooterColumn="DecSal18" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                                    <dx:ASPxSummaryItem FieldName="DecSal19" ShowInGroupFooterColumn="DecSal19" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                                    <dx:ASPxSummaryItem FieldName="DecSal20" ShowInGroupFooterColumn="DecSal20" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                                    <dx:ASPxSummaryItem FieldName="DecTotalSal" ShowInGroupFooterColumn="DecTotalSal" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                                    <dx:ASPxSummaryItem FieldName="DecImporteTotal" ShowInGroupFooterColumn="DecImporteTotal" SummaryType="Sum" DisplayFormat="{0}" />
                                                                                                </GroupSummary>
                                                                                            </dx:ASPxGridView>

                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td colspan="5" style="width: 100%">
                                                                                            <div id="contGrafPrinc" aria-expanded="true" style="min-width: 310px; height: 400px; margin: 0 auto">
                                                                                            </div>

                                                                                            <script type="text/javascript">

                                                                                                function grafficardatosprinc(s, e) {
                                                                                                    var alertText = s.cp_Alerta;
                                                                                                    if (alertText != "") {
                                                                                                        MensajeAdvertencia(alertText);
                                                                                                    } else {
                                                                                                    }

                                                                                                    var js_title = s.cp_title;
                                                                                                    var js_subtitle = s.cp_subtitle;
                                                                                                    var js_xAxis = s.cp_xAxis;
                                                                                                    var js_Series = s.cp_series;

                                                                                                    Highcharts.chart('contGrafPrinc', {

                                                                                                        chart: {
                                                                                                            type: 'column'
                                                                                                        },

                                                                                                        title: {
                                                                                                            text: js_title
                                                                                                        },
                                                                                                        subtitle: {
                                                                                                            text: js_subtitle
                                                                                                        },
                                                                                                        xAxis: {
                                                                                                            categories: js_xAxis
                                                                                                        },

                                                                                                        yAxis: {
                                                                                                            allowDecimals: false,
                                                                                                            min: 0,
                                                                                                            title: {
                                                                                                                text: 'Cantidad'
                                                                                                            },
                                                                                                            stackLabels: {
                                                                                                                enabled: true,
                                                                                                                style: {
                                                                                                                    fontWeight: 'bold',
                                                                                                                    color: (Highcharts.theme && Highcharts.theme.textColor) || 'gray'
                                                                                                                }
                                                                                                            }
                                                                                                        },

                                                                                                        tooltip: {
                                                                                                            formatter: function () {
                                                                                                                return '<b>' + this.x + '</b><br/>' +
                                                                                                                    this.series.name + ': ' + this.y + '<br/>' +
                                                                                                                    'Total: ' + this.point.stackTotal;
                                                                                                            }
                                                                                                        },

                                                                                                        plotOptions: {
                                                                                                            column: {
                                                                                                                stacking: 'normal'
                                                                                                            }
                                                                                                        },

                                                                                                        series: js_Series
                                                                                                    });
                                                                                                }
                                                                                            </script>

                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </dx:ContentControl>
                                                                        </ContentCollection>
                                                                    </dx:TabPage>
                                                                    <dx:TabPage Text="GRAFICO - MOV. DIARIO">
                                                                        <ContentCollection>
                                                                            <dx:ContentControl ID="ContentControl2" runat="server">

                                                                                <dx:ASPxCallbackPanel ID="pnlGrafico" runat="server" Width="100%" ClientInstanceName="pnlGrafico" OnCallback="pnlGrafico_Callback">

                                                                                    <ClientSideEvents EndCallback="function(s, e) {
                                            grafficardatos(s, e);
                                            }" />
                                                                                    <Paddings PaddingTop="10px" />
                                                                                    <PanelCollection>
                                                                                        <dx:PanelContent ID="PanelContent5" runat="server">
                                                                                            <table style="width: 100%">
                                                                                                <tr>
                                                                                                    <td style="width: 70%">Especificación de Grupo (Producto - Tipo de Infraestructura - Infraestructura) :
                                                                                            <dx:ASPxComboBox ID="cmbFiltro" runat="server" Theme="Metropolis" ValueField="StrIdGrupoUnico" Width="100%">
                                                                                                <Columns>
                                                                                                    <dx:ListBoxColumn FieldName="StrIdGrupoUnico" Visible="False" />
                                                                                                    <dx:ListBoxColumn FieldName="StrProducto" Name="Producto" />
                                                                                                    <dx:ListBoxColumn FieldName="StrTipoInfraestructura" Name="Tipo Infraestructura" />
                                                                                                    <dx:ListBoxColumn FieldName="StrCodigoInfraestructura" Name="Codigo Infraestructura" />
                                                                                                </Columns>
                                                                                                <ClearButton Visibility="Auto">
                                                                                                </ClearButton>
                                                                                            </dx:ASPxComboBox>
                                                                                                    </td>
                                                                                                    <td style="width: 30%">&nbsp;</td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td style="width: 70%">Tipo de Gráfico :
                                                                                            <dx:ASPxComboBox ID="cmbTipoGrafico" runat="server" EnableTheming="True" SelectedIndex="0" Theme="Metropolis" Width="40%">
                                                                                                <Items>
                                                                                                    <dx:ListEditItem Selected="True" Text="Linea" Value="line" />
                                                                                                    <dx:ListEditItem Text="Barras" Value="column" />
                                                                                                </Items>
                                                                                                <ClearButton Visibility="Auto">
                                                                                                </ClearButton>
                                                                                            </dx:ASPxComboBox>
                                                                                                    </td>
                                                                                                    <td style="width: 30%">&nbsp;</td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <dx:ASPxLabel ID="ASPxLabel45" runat="server" Width="100%">
                                                                                                            <BorderBottom BorderColor="#738D2C" BorderStyle="Solid" />
                                                                                                        </dx:ASPxLabel>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <dx:ASPxButton ID="btnGraficar" runat="server" AutoPostBack="False" EnableTheming="True" Font-Bold="True" ForeColor="Maroon" Text="Generar Gráfico" Theme="Metropolis" Width="100%">
                                                                                                            <ClientSideEvents Click="function(s, e) {
pnlGrafico.PerformCallback();   
}" />
                                                                                                            <Image IconID="chart_fullstackedline_16x16">
                                                                                                            </Image>
                                                                                                        </dx:ASPxButton>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td colspan="2">
                                                                                                        <div id="contMovDia" aria-expanded="true" style="min-width: 310px; height: 400px; margin: 0 auto">
                                                                                                        </div>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr style="padding-top: 5px">
                                                                                                    <td colspan="2">

                                                                                                        <div id="contSaldos" aria-expanded="true" style="min-width: 310px; height: 400px; margin: 0 auto">
                                                                                                        </div>

                                                                                                        <script type="text/javascript">

                                                                                                            function grafficardatos(s, e) {

                                                                                                                var js_type = s.cp_type;
                                                                                                                var js_title = s.cp_title;
                                                                                                                var js_subtitle = s.cp_subtitle;
                                                                                                                var js_xAxis = s.cp_xAxis;
                                                                                                                var js_Series = s.cp_series;

                                                                                                                var js_titSal = s.cp_titSal;
                                                                                                                var js_serSal = s.cp_serSal;

                                                                                                                Highcharts.chart('contMovDia', {
                                                                                                                    chart: {
                                                                                                                        type: js_type
                                                                                                                    },
                                                                                                                    title: {
                                                                                                                        text: js_title
                                                                                                                    },
                                                                                                                    subtitle: {
                                                                                                                        text: js_subtitle
                                                                                                                    },
                                                                                                                    xAxis: {
                                                                                                                        categories: js_xAxis,
                                                                                                                        crosshair: true
                                                                                                                    },
                                                                                                                    yAxis: {
                                                                                                                        min: 0,
                                                                                                                        title: {
                                                                                                                            text: 'Cantidad'
                                                                                                                        }
                                                                                                                    },
                                                                                                                    tooltip: {
                                                                                                                        headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                                                                                                                        pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                                                                                                                            '<td style="padding:0"><b>{point.y:.1f} </b></td></tr>',
                                                                                                                        footerFormat: '</table>',
                                                                                                                        shared: true,
                                                                                                                        useHTML: true
                                                                                                                    },
                                                                                                                    plotOptions: {
                                                                                                                        column: {
                                                                                                                            pointPadding: 0.2,
                                                                                                                            borderWidth: 0
                                                                                                                        },

                                                                                                                        series: {
                                                                                                                            pointStart: 0,
                                                                                                                            cursor: 'pointer',
                                                                                                                            point: {
                                                                                                                                events: {
                                                                                                                                    click: function (e) {
                                                                                                                                        pucDetalle.SetHeaderText('Detalle de Movimientos - ' + lblRazonSocial.GetText());
                                                                                                                                        pnlTransaccion.PerformCallback(this.category + "|" + this.series.name + "|2");
                                                                                                                                        pucDetalle.Show();
                                                                                                                                    }
                                                                                                                                }
                                                                                                                            },
                                                                                                                        }


                                                                                                                    },
                                                                                                                    series: js_Series
                                                                                                                });

                                                                                                                Highcharts.chart('contSaldos', {
                                                                                                                    chart: {
                                                                                                                        type: js_type
                                                                                                                    },
                                                                                                                    title: {
                                                                                                                        text: js_titSal
                                                                                                                    },
                                                                                                                    subtitle: {
                                                                                                                        text: js_subtitle
                                                                                                                    },
                                                                                                                    xAxis: {
                                                                                                                        categories: js_xAxis,
                                                                                                                        crosshair: true
                                                                                                                    },
                                                                                                                    yAxis: {
                                                                                                                        min: 0,
                                                                                                                        title: {
                                                                                                                            text: 'Cantidad'
                                                                                                                        }
                                                                                                                    },
                                                                                                                    tooltip: {
                                                                                                                        headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                                                                                                                        pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                                                                                                                            '<td style="padding:0"><b>{point.y:.1f} </b></td></tr>',
                                                                                                                        footerFormat: '</table>',
                                                                                                                        shared: true,
                                                                                                                        useHTML: true
                                                                                                                    },
                                                                                                                    plotOptions: {
                                                                                                                        column: {
                                                                                                                            pointPadding: 0.2,
                                                                                                                            borderWidth: 0
                                                                                                                        }
                                                                                                                    },
                                                                                                                    series: js_serSal
                                                                                                                });
                                                                                                            }
                                                                                                        </script>

                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>

                                                                                        </dx:PanelContent>
                                                                                    </PanelCollection>
                                                                                </dx:ASPxCallbackPanel>


                                                                            </dx:ContentControl>
                                                                        </ContentCollection>
                                                                    </dx:TabPage>
                                                                </TabPages>
                                                            </dx:ASPxPageControl>

                                                        </td>
                                                    </tr>




                                                    <tr>
                                                        <td colspan="7">
                                                            <dx:ASPxPanel ID="pnlHerramienta" runat="server" Width="100%" ClientInstanceName="pnlHerramienta" ClientVisible="False">
                                                                <PanelCollection>
                                                                    <dx:PanelContent ID="PanelContent6" runat="server">
                                                                        <table style="width: 100%">
                                                                            <tr>
                                                                                <td colspan="4">
                                                                                    <dx:ASPxLabel ID="ASPxLabel3" runat="server" Font-Bold="True" Font-Names="Rockwell" ForeColor="#999999" Text="HERRAMINTAS ADICIONALES" Width="100%">
                                                                                        <BorderBottom BorderStyle="Solid" />
                                                                                    </dx:ASPxLabel>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="width: 20%">

                                                                                    <dx:ASPxButton ID="btnImportaSaldo" runat="server" AutoPostBack="False" ClientInstanceName="btnImportaSaldo" Font-Bold="True" ForeColor="Maroon" OnClick="btnImportaSaldo_Click" Text="Importar Saldos del DCODificador" Theme="Metropolis" Width="100%">
                                                                                        <ClientSideEvents Click="function(s, e) {
if(confirm('¿Desea importar los saldos iniciales del periodo desde el DCODIFICADOR? - Esta acción anulará los saldos iniciales que se encuentren registrados en el periodo seleccionado. ¿Desea continuar?'))
{
LoadingPanel.Show();
e.processOnServer = true; 
}
else{
e.processOnServer = false; 
}
}
" />
                                                                                        <Image IconID="actions_converttorange_16x16">
                                                                                        </Image>
                                                                                    </dx:ASPxButton>

                                                                                </td>
                                                                                <td style="width: 20%">

                                                                                    <dx:ASPxButton ID="btnImporta" runat="server" AutoPostBack="False" ClientInstanceName="btnImporta" Font-Bold="True" ForeColor="Maroon" Text="Importar Recepciones DCODificador" Theme="Metropolis" Width="100%">
                                                                                        <ClientSideEvents Click="function(s, e) {
	pucImportaRec.PerformCallback();
	pucImportaRec.Show();
}" />
                                                                                        <Image IconID="actions_converttorange_16x16">
                                                                                        </Image>
                                                                                    </dx:ASPxButton>

                                                                                </td>
                                                                                <td style="width: 20%">

                                                                                    <dx:ASPxButton ID="btnExporta" runat="server" AutoPostBack="False" ClientInstanceName="btnExporta" Font-Bold="True" ForeColor="Maroon" Text="Exportación CSV de Movimientos " Theme="Metropolis" Width="100%">
                                                                                        <ClientSideEvents Click="function(s, e) {
	pucExportacion.PerformCallback();
	pucExportacion.Show();
}" />
                                                                                        <Image IconID="navigation_up_16x16">
                                                                                        </Image>
                                                                                    </dx:ASPxButton>

                                                                                </td>
                                                                                <td style="width: 40%">&nbsp;</td>
                                                                            </tr>
                                                                        </table>
                                                                    </dx:PanelContent>
                                                                </PanelCollection>
                                                            </dx:ASPxPanel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="auto-style11">
                                                            <dx:ASPxGridViewExporter ID="dgeExportar" runat="server">
                                                            </dx:ASPxGridViewExporter>
                                                        </td>
                                                        <td class="auto-style10">&nbsp;</td>
                                                        <td class="auto-style10"></td>
                                                        <td class="auto-style12"></td>
                                                        <td class="auto-style10">&nbsp;</td>
                                                        <td class="auto-style10">&nbsp;</td>
                                                        <td class="auto-style14">&nbsp;</td>
                                                    </tr>



                                                </table>
                                            </dx:PanelContent>
                                        </PanelCollection>
                                    </dx:ASPxCallbackPanel>
                                </dx:SplitterContentControl>
                            </ContentCollection>
                        </dx:SplitterPane>
                    </Panes>
                </dx:ASPxSplitter>

            </td>
        </tr>
    </table>

</div>




<dx:ASPxPopupControl ID="pucDetalle" runat="server" CloseAction="CloseButton" CloseOnEscape="True" Modal="True"
    PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Above" ClientInstanceName="pucDetalle"
    HeaderText="Detalle de Movimientos" AllowDragging="True" PopupAnimationType="None" EnableViewState="False" Maximized="True" ShowMaximizeButton="True" Theme="Metropolis" ScrollBars="Auto" AllowResize="True">
    <HeaderImage IconID="grid_horizontalgridlinesmajorminor_32x32">
    </HeaderImage>

    <HeaderStyle>
        <BorderBottom BorderStyle="Solid" />
    </HeaderStyle>
    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
            <dx:ASPxCallbackPanel ID="pnlTransaccion" runat="server" DefaultButton="btOK" ClientInstanceName="pnlTransaccion" OnCallback="pnlTransaccion_Callback">
                <ClientSideEvents BeginCallback="function(s, e) {
	LoadingPanel.Show();
}"
                    EndCallback="function(s, e) {
	LoadingPanel.Hide();
}" />
                <PanelCollection>
                    <dx:PanelContent ID="PanelContent7" runat="server">

                        <table class="dxflInternalEditorTable_Youthful">
                            <tr>
                                <td style="width: 10%; padding-bottom: 10px;">
                                    <dx:ASPxLabel ID="lblTipoOperacion" runat="server" ClientInstanceName="lblTipoOperacion" Font-Bold="True" ForeColor="#666666" Text="Tipo de Operación :" Width="100%">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 40%; padding-bottom: 10px;">
                                    <dx:ASPxComboBox ID="cmbTipoOperacion" runat="server" ClientEnabled="False" ClientInstanceName="cmbTipoOperacion" TextField="NOMBRE" TextFormatString="{0}" Theme="Metropolis" ValueField="ID_TIPO_OPERACION" Width="250px" BackColor="#FFE1C4" ForeColor="#DEE8DB">
                                        <ClientSideEvents Init="function(s, e) {
	                    s.GetInputElement().readOnly = true;
                    }" />
                                        <Columns>
                                            <dx:ListBoxColumn Caption="NOMBRE" FieldName="NOMBRE" Name="NOMBRE" />
                                            <dx:ListBoxColumn Caption="ID_TIPO_OPERACION" FieldName="ID_TIPO_OPERACION" Name="ID_TIPO_OPERACION" Width="0px" />
                                            <dx:ListBoxColumn Caption="CODIGO" FieldName="CODIGO" Name="CODIGO" Width="0px" />
                                            <dx:ListBoxColumn Caption="ORDEN" FieldName="ORDEN" Name="ORDEN" Width="0px" />
                                            <dx:ListBoxColumn Caption="LIMITE_MINIMO" FieldName="LIMITE_MINIMO" Name="LIMITE_MINIMO" Width="0px" />
                                            <dx:ListBoxColumn Caption="LIMITE_MAXIMO" FieldName="LIMITE_MAXIMO" Name="LIMITE_MAXIMO" Width="0px" />
                                        </Columns>
                                        <ClearButton Visibility="Auto">
                                        </ClearButton>
                                        <ReadOnlyStyle BackColor="#DEE8DB">
                                        </ReadOnlyStyle>
                                        <DisabledStyle ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                </td>
                                <td style="width: 10%; text-align: right">
                                    <dx:ASPxButton runat="server" ID="btnCollapse" Text="Contraer filas" UseSubmitBehavior="false" Theme="Default" Width="80%"
                                        AutoPostBack="false">
                                        <ClientSideEvents Click="function() { dgvTransacciones.CollapseAll() }" />
                                    </dx:ASPxButton>
                                </td>
                                <td style="width: 10%; text-align: left">
                                    <dx:ASPxButton runat="server" ID="btnExpand" Text="Expandir filas" UseSubmitBehavior="false" Theme="Default" Width="80%"
                                        AutoPostBack="false">
                                        <ClientSideEvents Click="function() { dgvTransacciones.ExpandAll() }" />
                                    </dx:ASPxButton>
                                </td>
                                <td style="width: 30%; text-align: right">
                                    <dx:ASPxButton ID="btnExportar" runat="server" Font-Bold="True" ForeColor="Maroon" OnClick="btnExportar_Click" Text="Exportar Excel.." Theme="Metropolis" Width="40%">
                                        <ClientSideEvents Click="function(s, e) {
	                e.processOnServer = confirm('¿Desea exportar los movimientos?'); 
                }" />
                                        <Image IconID="export_exporttoxls_16x16">
                                        </Image>
                                    </dx:ASPxButton>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="5">
                                    <dx:ASPxGridView ID="dgvTransacciones" runat="server" AutoGenerateColumns="False" ClientInstanceName="dgvTransacciones" EnableTheming="True" Font-Size="Smaller" KeyFieldName="ID_MOVIMIENTO" OnCustomButtonCallback="dgvTransacciones_CustomButtonCallback" OnCustomCallback="dgvTransacciones_CustomCallback" OnCustomSummaryCalculate="dgvTransacciones_CustomSummaryCalculate" OnHtmlEditFormCreated="dgvTransacciones_HtmlEditFormCreated" OnHtmlRowPrepared="dgvTransacciones_HtmlRowPrepared" OnRowInserting="dgvTransacciones_RowInserting" OnRowUpdating="dgvTransacciones_RowUpdating" OnRowValidating="dgvTransacciones_RowValidating" Theme="Default" Width="100%" OnCustomButtonInitialize="dgvTransacciones_CustomButtonInitialize" OnCommandButtonInitialize="dgvTransacciones_CommandButtonInitialize">
                                        <ClientSideEvents CustomButtonClick="function (s, e) {
                switch(e.buttonID) {
                &nbsp;&nbsp;&nbsp; case&nbsp; 'btnAnular':
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; e.processOnServer = confirm('¿Desea anular el registro?'); 
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; break;
                &nbsp;&nbsp;&nbsp; case&nbsp; 'btnActivar':
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; e.processOnServer = confirm('¿Desea activar el registro?'); 
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; break;
                    case&nbsp; 'btnEliminar':
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; e.processOnServer = confirm('ATENCION: Si elimina el registro no podrá revertir el proceso. ¿Desea eliminar el registro?'); 
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; break;
                } 
                }"
                                            BeginCallback="function(s, e) {
	                LoadingPanel.Show();
                }"
                                            EndCallback="function(s, e) {
	                LoadingPanel.Hide();
                }" />
                                        <SettingsContextMenu EnableRowMenu="False">
                                        </SettingsContextMenu>
                                        <Templates>
                                            <GroupFooterRow>
                                                <dx:ASPxLabel ID="ASPxLabel38" runat="server" Text="ASPxLabel">
                                                </dx:ASPxLabel>
                                            </GroupFooterRow>


                                            <EditForm>

                                                <table style="width: 100%">
                                                    <tr>
                                                        <td style="width: 25%; text-align: right; padding-top: 5px">
                                                            <dx:ASPxLabel ID="ASPxLabel2" runat="server" Font-Bold="True" ForeColor="#666666" Text="Fecha :">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 25%">
                                                            <dx:ASPxDateEdit ID="detFechaEf" runat="server" EditFormat="DateTime" Font-Bold="False" Theme="Metropolis" Width="260px">
                                                                <TimeSectionProperties>
                                                                    <TimeEditProperties>
                                                                        <ClearButton Visibility="Auto">
                                                                        </ClearButton>
                                                                    </TimeEditProperties>
                                                                </TimeSectionProperties>
                                                                <ClearButton Visibility="Auto">
                                                                </ClearButton>
                                                                <ValidationSettings>
                                                                    <RequiredField ErrorText="ERROR: Introduzca la fecha." IsRequired="True" />
                                                                </ValidationSettings>
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#FFE1C4" ForeColor="Black">
                                                                </DisabledStyle>
                                                            </dx:ASPxDateEdit>
                                                        </td>
                                                        <td style="width: 25%; text-align: right;">
                                                            <dx:ASPxLabel ID="ASPxLabel1" runat="server" Font-Bold="True" ForeColor="#666666" Text="ID :">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 25%">
                                                            <dx:ASPxTextBox ID="txtIdEf" runat="server" ClientInstanceName="txtIdEf" ReadOnly="True" Text="0" Width="260px">
                                                                <MaskSettings Mask="&lt;0..999999999999999&gt;" />
                                                                <ReadOnlyStyle BackColor="#FFE1C4">
                                                                </ReadOnlyStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 25%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblTipoPagoEf" runat="server" Font-Bold="True" ForeColor="#666666" Text="Tipo de Flujo :">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 25%">
                                                            <dx:ASPxComboBox ID="cmbTipoFlujoEf" runat="server" ClientInstanceName="cmbTipoFlujoEf" TextField="DESCRIPCION" TextFormatString="{0}" Theme="Metropolis" ValueField="ID_PARAMETRICA_GLOBAL" Width="260px">
                                                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
	                                    if(cmbTipoPagoEf.GetValue()==3 || cmbTipoPagoEf.GetValue()==-99999){
                                    cmbTipoCredEf.SetValue(9);
	                                    }
                                    else{
                                    cmbTipoCredEf.SetValue(10);
                                    }
                                    }" />
                                                                <Columns>
                                                                    <dx:ListBoxColumn Caption="DESCRIPCION" FieldName="DESCRIPCION" Name="DESCRIPCION" />
                                                                    <dx:ListBoxColumn Caption="ID_PARAMETRICA_GLOBAL" FieldName="ID_PARAMETRICA_GLOBAL" Name="ID_PARAMETRICA_GLOBAL" Width="0px" />
                                                                    <dx:ListBoxColumn Caption="CODIGO" FieldName="CODIGO" Name="CODIGO" Width="0px" />
                                                                </Columns>
                                                                <ClearButton Visibility="Auto">
                                                                </ClearButton>
                                                                <ValidationSettings>
                                                                    <RequiredField ErrorText="ERROR : Introduzca la forma de pago." IsRequired="True" />
                                                                </ValidationSettings>
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxComboBox>
                                                        </td>
                                                        <td style="width: 25%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblEstadoEf" runat="server" Font-Bold="True" ForeColor="#666666" Text="Estado :">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 25%">
                                                            <dx:ASPxComboBox ID="cmbEstadoEf" runat="server" SelectedIndex="0" Theme="Metropolis" ValueType="System.Int32" Width="260px">
                                                                <Items>
                                                                    <dx:ListEditItem Selected="True" Text="VALIDO" Value="11" />
                                                                    <dx:ListEditItem Text="ANULADO" Value="12" />
                                                                </Items>
                                                                <ClearButton Visibility="Auto">
                                                                </ClearButton>
                                                                <ValidationSettings>
                                                                    <RequiredField ErrorText="ERROR: Introduzca el estado." IsRequired="True" />
                                                                </ValidationSettings>
                                                                <ReadOnlyStyle BackColor="#FFE1C4">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#FFE1C4">
                                                                </DisabledStyle>
                                                            </dx:ASPxComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 25%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblEntidadAuxialiarEf" runat="server" Font-Bold="True" ForeColor="#666666" Text="Proyecto Origen/Destino :" Width="100%">
                                                            </dx:ASPxLabel>


                                                        </td>
                                                        <td style="width: 25%">
                                                            <dx:ASPxComboBox ID="cmbEntidadAuxialiarEf" runat="server" TextField="NOMBRE" TextFormatString="{1}" Theme="Metropolis" ValueField="ID_IDENTIFICADOR" Width="260px">
                                                                <Columns>
                                                                    <dx:ListBoxColumn Caption="ACTIVIDAD" FieldName="SUBID_NOMBRE" />
                                                                    <dx:ListBoxColumn Caption="RAZON SOCIAL" FieldName="NOMBRE" />
                                                                    <dx:ListBoxColumn Caption="ALIAS" FieldName="DESCRIPCION" />
                                                                    <dx:ListBoxColumn Caption="ID_SUB_IDENTIFICADOR" FieldName="ID_SUB_IDENTIFICADOR" Width="0px" />
                                                                    <dx:ListBoxColumn Caption="ID_IDENTIFICADOR" FieldName="ID_IDENTIFICADOR" Width="0px" />
                                                                </Columns>
                                                                <ClearButton Visibility="Auto">
                                                                </ClearButton>
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxComboBox>

                                                        </td>
                                                        <td style="width: 25%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblIntegracionEf" runat="server" Font-Bold="True" ForeColor="#666666" Text="Id. Integración :">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 25%">
                                                            <dx:ASPxTextBox ID="txtIntegracionEf" runat="server" ClientInstanceName="txtIntegracionEf" Text="0" Width="260px">
                                                                <MaskSettings Mask="&lt;0..999999999999999&gt;" />
                                                                <ReadOnlyStyle BackColor="#FFE1C4">
                                                                </ReadOnlyStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 25%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblTipoMovEf" runat="server" Font-Bold="True" ForeColor="#666666" Text="Tipo Movimiento :">
                                                            </dx:ASPxLabel>

                                                        </td>
                                                        <td style="width: 25%">
                                                            <dx:ASPxComboBox ID="cmbTipoMovEf" runat="server" ClientInstanceName="cmbTipoMovEf" TextField="DESCRIPCION" TextFormatString="{0}" Theme="Metropolis" ValueField="ID_PARAMETRICA_GLOBAL" Width="260px">
                                                                <ClientSideEvents ValueChanged="function(s, e) {
	
                                                                                        var item = s.GetSelectedItem();
                                                                                        if(item)
                                                                                        {

                                                                                            if(item.GetColumnText('CODIGO')=='NN')
                                                                                            {
                                                                                            cmbPaisEf.SetVisible(false);
                                                                                            lblPaisEf.SetVisible(false);
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                            cmbPaisEf.SetVisible(true);
                                                                                            lblPaisEf.SetVisible(true);
                                                                                            }
                                                                                        }

                                                                                        }" />
                                                                <Columns>
                                                                    <dx:ListBoxColumn Caption="DESCRIPCION" FieldName="DESCRIPCION" Name="DESCRIPCION" />
                                                                    <dx:ListBoxColumn Caption="ID_PARAMETRICA_GLOBAL" FieldName="ID_PARAMETRICA_GLOBAL" Name="ID_PARAMETRICA_GLOBAL" Width="0px" />
                                                                    <dx:ListBoxColumn Caption="CODIGO" FieldName="CODIGO" Name="CODIGO" Width="0px" />
                                                                </Columns>
                                                                <ClearButton Visibility="Auto">
                                                                </ClearButton>
                                                                <ValidationSettings ValidateOnLeave="False">
                                                                </ValidationSettings>
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxComboBox>


                                                        </td>
                                                        <td style="width: 25%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblPaisEf" runat="server" ClientInstanceName="lblPaisEf" Font-Bold="True" ForeColor="#666666" Text="País :">
                                                                <ClientSideEvents Init="function(s, e) {

                                    try{
                                    var item = cmbTipoMovEf.GetSelectedItem();
                                    if(item)
                                    {
                                    if(item.GetColumnText('CODIGO')=='NN')
                                    {
                                    lblPaisEf.SetVisible(false);
                                    }
                                    }
                                    }
                                    catch(e){
                                    lblPaisEf.SetVisible(false);
                                    }

                                    }" />
                                                            </dx:ASPxLabel>


                                                        </td>
                                                        <td style="width: 25%">
                                                            <dx:ASPxComboBox ID="cmbPaisEf" runat="server" ClientInstanceName="cmbPaisEf" TextField="DESCRIPCION" TextFormatString="{0}" Theme="Metropolis" ValueField="ID_UBICACION_GEOGRAFICA" Width="260px">
                                                                <ClientSideEvents Init="function(s, e) {

                                    try{
                                    var item = cmbTipoMovEf.GetSelectedItem();
                                    if(item)
                                    {
                                    if(item.GetColumnText('CODIGO')=='NN')
                                    {
                                    cmbPaisEf.SetVisible(false);
                                    }
                                    }
                                    }
                                    catch(e){
                                    cmbPaisEf.SetVisible(false);
                                    }

                                    }" />
                                                                <Columns>
                                                                    <dx:ListBoxColumn Caption="DESCRIPCION" FieldName="DESCRIPCION" Name="DESCRIPCION" />
                                                                    <dx:ListBoxColumn Caption="ID_UBICACION_GEOGRAFICA" FieldName="ID_UBICACION_GEOGRAFICA" Name="ID_UBICACION_GEOGRAFICA" Width="0px" />
                                                                </Columns>
                                                                <ClearButton Visibility="Auto">
                                                                </ClearButton>
                                                                <ReadOnlyStyle BackColor="#FFE1C4">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxComboBox>


                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 25%; text-align: right; padding-bottom: 10px;">
                                                            <dx:ASPxLabel ID="lblMedioTransEf" runat="server" Font-Bold="True" ForeColor="#666666" Text="Infraestructura (Orig./Dest.) :" Width="100%">
                                                            </dx:ASPxLabel>


                                                        </td>
                                                        <td style="width: 25%; padding-bottom: 10px;">
                                                            <dx:ASPxComboBox ID="cmbMedioTransEf" runat="server" ClientInstanceName="cmbMedioTransEf" TextField="CODIGO_OPERADOR" TextFormatString="{0}" Theme="Metropolis" ValueField="ID_INFRA" Width="260px">
                                                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
                                    var item = s.GetSelectedItem();
                                    if(item)
                                    {
                                    cmbTipoTransEf.SetValue(item.GetColumnText('ID_TIPO'));
                                    }
                                    }" />
                                                                <Columns>
                                                                    <dx:ListBoxColumn Caption="CODIGO" FieldName="CODIGO_OPERADOR" Name="CODIGO_OPERADOR" />
                                                                    <dx:ListBoxColumn Caption="ID_INFRA" FieldName="ID_INFRA" Name="ID_INFRA" Width="0px" />
                                                                    <dx:ListBoxColumn Caption="ID_INFRAESTRUCTURA" FieldName="ID_INFRAESTRUCTURA" Name="ID_INFRAESTRUCTURA" Width="0px" />
                                                                    <dx:ListBoxColumn Caption="ID_PRODUCTO" FieldName="ID_PRODUCTO" Name="ID_PRODUCTO" Width="0px" />
                                                                    <dx:ListBoxColumn Caption="ID_UNIDAD_MEDIDA" FieldName="ID_UNIDAD_MEDIDA" Name="ID_UNIDAD_MEDIDA" Width="0px" />
                                                                    <dx:ListBoxColumn Caption="ID_TIPO" FieldName="ID_TIPO" Name="ID_TIPO" Width="0px" />
                                                                </Columns>
                                                                <Items>
                                                                    <dx:ListEditItem Text="No Definido" Value="-99999" />
                                                                </Items>
                                                                <ClearButton Visibility="Auto">
                                                                </ClearButton>
                                                                <ValidationSettings>
                                                                    <RequiredField ErrorText="ERROR: Introduzca el medio de transporte." IsRequired="True" />
                                                                </ValidationSettings>
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxComboBox>


                                                        </td>
                                                        <td style="width: 25%; text-align: right; padding-bottom: 10px;">
                                                            <dx:ASPxLabel ID="lblTipoTransEf" runat="server" Font-Bold="True" ForeColor="#666666" Text="Tipo Infra. (Orig./Dest.) :" Width="100%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 25%; padding-bottom: 10px;">
                                                            <dx:ASPxComboBox ID="cmbTipoTransEf" runat="server" ClientInstanceName="cmbTipoTransEf" TextField="VALOR" TextFormatString="{0}" Theme="Metropolis" ValueField="ID_PARAMETRO" Width="260px">
                                                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
                                    var item = s.GetSelectedItem();
                                    if(item)
                                    {
                                    cmbMedioTransEf.SetValue('9999999999');
                                    }
                                    }
                                    " />
                                                                <Columns>
                                                                    <dx:ListBoxColumn Caption="NOMBRE" FieldName="VALOR" Name="VALOR" />
                                                                    <dx:ListBoxColumn Caption="ID_PARAMETRO" FieldName="ID_PARAMETRO" Name="ID_PARAMETRO" Width="0px" />
                                                                    <dx:ListBoxColumn Caption="CODIGO" FieldName="CODIGO" Name="CODIGO" Width="0px" />
                                                                </Columns>
                                                                <ClearButton Visibility="Auto">
                                                                </ClearButton>
                                                                <ValidationSettings>
                                                                    <RequiredField ErrorText="ERROR: Introduzca el tipo de transporte." IsRequired="True" />
                                                                </ValidationSettings>
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxComboBox>
                                                        </td>
                                                    </tr>




                                                    <tr style="width: 25%; text-align: right; padding-bottom: 10px;">
                                                        <td>
                                                            <dx:ASPxLabel ID="lblCodUsuario" runat="server" Font-Bold="True" ForeColor="#666666" Text="id Codigo de Usuario :" Width="100%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td>
                                                            <dx:ASPxTextBox ID="txtCodUsuario" runat="server" ClientInstanceName="txtCodUsuario" Text="0" Width="260px">
                                                                <ReadOnlyStyle BackColor="#DEE8DB"></ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC"></FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB"></DisabledStyle>
                                                            </dx:ASPxTextBox>

                                                        </td>

                                                        <td>
                                                            <dx:ASPxLabel ID="lblCodUbicacion" runat="server" Font-Bold="True" ForeColor="#666666" Text="Codigo de Ubicacion :"></dx:ASPxLabel>
                                                        </td>
                                                        <td>
                                                            <dx:ASPxTextBox ID="txtCodUbicacion" runat="server" ClientInstanceName="txtCodUbicacion" Text="0" Width="260px">
                                                                <ReadOnlyStyle BackColor="#DEE8DB"></ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC"></FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB"></DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr style="width: 25%; text-align: right; padding-bottom: 10px;">
                                                        <td>
                                                            <dx:ASPxLabel ID="lblCiudadPoblacion" runat="server" Font-Bold="True" ForeColor="#666666" Text="Ciudad/Poblacion :"></dx:ASPxLabel>
                                                        </td>
                                                        <td>
                                                            <dx:ASPxTextBox ID="txtCiudadPoblacion" runat="server" ClientInstanceName="txtCiudadPoblacion" Text="0" Width="260px">
                                                                <ReadOnlyStyle BackColor="#DEE8DB"></ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC"></FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB"></DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>

                                                        <td>
                                                            <dx:ASPxLabel ID="lblCategoria" runat="server" Font-Bold="True" ForeColor="#666666" Text="Categoria :" Width="100%"></dx:ASPxLabel>
                                                        </td>
                                                        <td>
                                                            <dx:ASPxTextBox ID="txtCategoria" runat="server" ClientInstanceName="txtCategoria" Text="0" Width="260px">
                                                                <ReadOnlyStyle BackColor="#DEE8DB"></ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC"></FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB"></DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                    </tr>



                                                    <tr style="width: 25%; text-align: right; padding-bottom: 10px;">
                                                        <td>
                                                            <dx:ASPxLabel ID="lblDireccion" runat="server" Font-Bold="True" ForeColor="#666666" Text="DirecciÓn :" Width="100%"></dx:ASPxLabel>
                                                        </td>

                                                        <td>
                                                            <dx:ASPxTextBox ID="txtDireccion" runat="server" ClientInstanceName="txtDireccion" Text="0" Width="260px">
                                                                <ReadOnlyStyle BackColor="#DEE8DB"></ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC"></FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB"></DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>

                                                        <td>
                                                            <dx:ASPxLabel ID="lblSectorEcon" runat="server" Font-Bold="True" ForeColor="#666666" Text="Sector Economico :" Width="100%"></dx:ASPxLabel>
                                                        </td>

                                                        <td>



                                                            <dx:ASPxComboBox ID="cmbSectorEconomico" runat="server" ClientInstanceName="cmbSectorEconomico" TextField="NOMBRE" TextFormatString="{0}" Theme="Metropolis" ValueField="CODIGO" Width="260px">

                                                                <Columns>


                                                                    <dx:ListBoxColumn Caption="SECTOR ECONOMICO" FieldName="NOMBRE" Name="NOMBRE" />
                                                                    <dx:ListBoxColumn Caption="CODIGO" FieldName="CODIGO" Name="CODIGO" Width="0px" />
                                                                    <dx:ListBoxColumn Caption="ID_SECTOR_SUPER" FieldName="ID_SECTOR_SUPER" Name="ID_SECTOR_SUPER" Width="0px" />
                                                                    <dx:ListBoxColumn Caption="NOMBRE" FieldName="NOMBRE" Name="NOMBRE" Width="0px" />
                                                                    <dx:ListBoxColumn Caption="DESCRIPCION" FieldName="DESCRIPCION" Name="DESCRIPCION" Width="0px" />

                                                                </Columns>
                                                                <Items>
                                                                    <dx:ListEditItem Text="No Definido" Value="NOID" />
                                                                </Items>
                                                                <ClearButton Visibility="Auto">
                                                                </ClearButton>
                                                                <ValidationSettings>
                                                                    <RequiredField ErrorText="ERROR: Introduzca Sector Economico." IsRequired="True" />
                                                                </ValidationSettings>
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxComboBox>




                                                        </td>
                                                    </tr>
                                                    <tr style="width: 25%; text-align: right; padding-bottom: 10px;">
                                                        <td>
                                                            <dx:ASPxLabel ID="lblTipoMedidor" runat="server" Font-Bold="True" ForeColor="#666666" Text="Tipo Medidor :" Width="100%"></dx:ASPxLabel>
                                                        </td>

                                                        <td>
                                                            <dx:ASPxTextBox ID="txtTipoMedidor" runat="server" ClientInstanceName="txtTipoMedidor" Text="0" Width="260px">
                                                                <ReadOnlyStyle BackColor="#DEE8DB"></ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC"></FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB"></DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>

                                                        <td>
                                                            <dx:ASPxLabel ID="lblCodMedidor" runat="server" Font-Bold="True" ForeColor="#666666" Text="Codigo Medidor :" Width="100%"></dx:ASPxLabel>
                                                        </td>

                                                        <td>
                                                            <dx:ASPxTextBox ID="txtCodMedidor" runat="server" ClientInstanceName="txtCodMedidor" Text="0" Width="260px">
                                                                <ReadOnlyStyle BackColor="#DEE8DB"></ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC"></FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB"></DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr style="width: 25%; text-align: right; padding-bottom: 10px;">


                                                        <td>
                                                            <dx:ASPxLabel ID="lblFechaLec" runat="server" Font-Bold="True" ForeColor="#666666" Text="Fecha Lectura :" Width="100%"></dx:ASPxLabel>
                                                        </td>

                                                        <td>
                                                            <dx:ASPxTextBox ID="txtFechaLec" runat="server" ClientInstanceName="txtFechaLec" Text="0" Width="260px">
                                                                <ReadOnlyStyle BackColor="#DEE8DB"></ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC"></FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB"></DisabledStyle>
                                                            </dx:ASPxTextBox>

                                                        </td>

                                                        <td>
                                                            <dx:ASPxLabel ID="lblLecMedido" runat="server" Font-Bold="True" ForeColor="#666666" Text="Lectura Medidor :" Width="100%"></dx:ASPxLabel>
                                                        </td>

                                                        <td>
                                                            <dx:ASPxTextBox ID="txtLecMedidor" runat="server" ClientInstanceName="txtLecMedidor" Text="0" Width="260px">
                                                                <ReadOnlyStyle BackColor="#DEE8DB"></ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC"></FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB"></DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                    </tr>

                                                    <tr style="width: 25%; text-align: right; padding-bottom: 10px;">
                                                        <td>
                                                            <dx:ASPxLabel ID="lblPresionPsig" runat="server" Font-Bold="True" ForeColor="#666666" Text="Presion de Flujo :" Width="100%"></dx:ASPxLabel>
                                                        </td>

                                                        <td>
                                                            <dx:ASPxTextBox ID="txtPresionPsig" runat="server" ClientInstanceName="txtPresionPsig" Text="0" Width="260px">
                                                                <ReadOnlyStyle BackColor="#DEE8DB"></ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC"></FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB"></DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>

                                                        <td>
                                                            <dx:ASPxLabel ID="lblFactorCorrec" runat="server" Font-Bold="True" ForeColor="#666666" Text="Factor Correccion :" Width="100%"></dx:ASPxLabel>
                                                        </td>

                                                        <td>
                                                            <dx:ASPxTextBox ID="txtFactorCorrec" runat="server" ClientInstanceName="txtFactorCorrec" Text="0" Width="260px">
                                                                <ReadOnlyStyle BackColor="#DEE8DB"></ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC"></FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB"></DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr style="width: 25%; text-align: right; padding-bottom: 10px;">
                                                        <td>
                                                            <dx:ASPxLabel ID="lblCantNoCorregido" runat="server" Font-Bold="True" ForeColor="#666666" Text="Vol. No Corregido :" Width="100%"></dx:ASPxLabel>
                                                        </td>

                                                        <td>
                                                            <dx:ASPxTextBox ID="txtCantNoCorregido" runat="server" ClientInstanceName="txtCantNoCorregido" Text="0" Width="260px">
                                                                <ReadOnlyStyle BackColor="#DEE8DB"></ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC"></FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB"></DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>


                                                        <td>
                                                            <dx:ASPxLabel ID="lblImportAdicional" runat="server" Font-Bold="True" ForeColor="#666666" Text="Cargo Adicional :" Width="100%"></dx:ASPxLabel>
                                                        </td>

                                                        <td>
                                                            <dx:ASPxTextBox ID="txtImportAdicional" runat="server" ClientInstanceName="txtImportAdicional" Text="0" Width="260px">
                                                                <ReadOnlyStyle BackColor="#DEE8DB"></ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC"></FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB"></DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                            <br />
                                                        </td>


                                                    </tr>














                                                    <tr>
                                                        <td style="width: 25%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblTanqueEf" runat="server" Font-Bold="True" ForeColor="#666666" Text="Infraestructura (Recep./Desp.) :" Width="100%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 25%">
                                                            <dx:ASPxComboBox ID="cmbTanqueEf" runat="server" ClientInstanceName="cmbTanqueEf" TextField="CODIGO_OPERADOR" TextFormatString="{0}" Theme="Metropolis" ValueField="ID_INFRA" Width="260px">
                                                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
                                    var item = s.GetSelectedItem();
                                    if(item)
                                    {

                                    cmbTipoInfEf.SetValue(item.GetColumnText('ID_TIPO'));

                                    if(item.GetColumnText('ID_INFRAESTRUCTURA')!=-99999)
                                    {
                                    cmbProdEf.SetValue(item.GetColumnText('ID_PRODUCTO'));
                                    var prod = cmbProdEf.GetSelectedItem();
                                    if(prod)
                                    {
                                    cmbUnidadEf.SetValue(prod.GetColumnText('ID_SUB_IDENTIFICADOR'));
                                    }
                                    }
                                    }
                                    }" />
                                                                <Columns>
                                                                    <dx:ListBoxColumn Caption="CODIGO" FieldName="CODIGO_OPERADOR" Name="CODIGO_OPERADOR" />
                                                                    <dx:ListBoxColumn Caption="ID_INFRA" FieldName="ID_INFRA" Name="ID_INFRA" Width="0px" />
                                                                    <dx:ListBoxColumn Caption="ID_INFRAESTRUCTURA" FieldName="ID_INFRAESTRUCTURA" Name="ID_INFRAESTRUCTURA" Width="0px" />
                                                                    <dx:ListBoxColumn Caption="ID_PRODUCTO" FieldName="ID_PRODUCTO" Name="ID_PRODUCTO" Width="0px" />
                                                                    <dx:ListBoxColumn Caption="ID_UNIDAD_MEDIDA" FieldName="ID_UNIDAD_MEDIDA" Name="ID_UNIDAD_MEDIDA" Width="0px" />
                                                                    <dx:ListBoxColumn Caption="ID_TIPO" FieldName="ID_TIPO" Name="ID_TIPO" Width="0px" />
                                                                </Columns>
                                                                <ClearButton Visibility="Auto">
                                                                </ClearButton>
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxComboBox>
                                                        </td>
                                                        <td style="width: 25%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblTipoInfEf" runat="server" ClientInstanceName="lblTipoInfEf" Font-Bold="True" ForeColor="#666666" Text="Tipo Infra. (Recep./Desp.) :">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 25%">
                                                            <dx:ASPxComboBox ID="cmbTipoInfEf" runat="server" ClientInstanceName="cmbTipoInfEf" ReadOnly="True" TextField="VALOR" TextFormatString="{0}" Theme="Metropolis" ValueField="ID_PARAMETRO" Width="260px">
                                                                <Columns>
                                                                    <dx:ListBoxColumn Caption="NOMBRE" FieldName="VALOR" Name="VALOR" />
                                                                    <dx:ListBoxColumn Caption="ID_PARAMETRO" FieldName="ID_PARAMETRO" Name="ID_PARAMETRO" Width="0px" />
                                                                    <dx:ListBoxColumn Caption="CODIGO" FieldName="CODIGO" Name="CODIGO" Width="0px" />
                                                                </Columns>
                                                                <ClearButton Visibility="Auto">
                                                                </ClearButton>
                                                                <ValidationSettings>
                                                                    <RequiredField ErrorText="ERROR: Introduzca el tipo de transporte." IsRequired="True" />
                                                                </ValidationSettings>
                                                                <ReadOnlyStyle BackColor="#FFE1C4">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 25%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblProdEf" runat="server" Font-Bold="True" ForeColor="#666666" Text="Producto :" Width="100px">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 25%">
                                                            <dx:ASPxComboBox ID="cmbProdEf" runat="server" ClientInstanceName="cmbProdEf" TextField="PRODUCTO" TextFormatString="{0}" Theme="Metropolis" ValueField="ID_IDENTIFICADOR" Width="260px">
                                                                <ClientSideEvents Init="function(s, e) {
                                    try{
                                    var tanque = cmbTanqueEf.GetSelectedItem();
                                    if(tanque)
                                    {
                                    cmbTipoInfEf.SetValue(tanque.GetColumnText('ID_TIPO'));
                                    if(tanque.GetColumnText('ID_INFRAESTRUCTURA')!=-99999)
                                    {
                                    s.SetValue(tanque.GetColumnText('ID_PRODUCTO'));
                                    }
                                    }
                                    }
                                    catch(e){
                                    }
                                    }
                                    "
                                                                    SelectedIndexChanged="function(s, e) {
                                    try{
                                    var tanque = cmbTanqueEf.GetSelectedItem();
                                    if(tanque)
                                    {
                                    if(tanque.GetColumnText('ID_INFRAESTRUCTURA')!=-99999)
                                    {
                                    s.SetValue(tanque.GetColumnText('ID_PRODUCTO'));
                                    }
                                    var prod = s.GetSelectedItem();
                                    if(prod)
                                    {
                                    cmbUnidadEf.SetValue(prod.GetColumnText('ID_SUB_IDENTIFICADOR'));
                                    }
                                    }

                                    }
                                    catch(e){
                                    var prod = s.GetSelectedItem();
                                    if(prod)
                                    {
                                    cmbUnidadEf.SetValue(prod.GetColumnText('ID_SUB_IDENTIFICADOR'));
                                    }

                                    }


                                    }
                                    " />
                                                                <Columns>
                                                                    <dx:ListBoxColumn Caption="PRODUCTO" FieldName="NOMBRE" Name="NOMBRE" />
                                                                    <dx:ListBoxColumn Caption="ID_IDENTIFICADOR" FieldName="ID_IDENTIFICADOR" Name="ID_IDENTIFICADOR" Width="0px" />
                                                                    <dx:ListBoxColumn Caption="ID_SUB_IDENTIFICADOR" FieldName="ID_SUB_IDENTIFICADOR" Name="ID_SUB_IDENTIFICADOR" Width="0px" />
                                                                </Columns>
                                                                <ClearButton Visibility="Auto">
                                                                </ClearButton>
                                                                <ValidationSettings>
                                                                    <RequiredField ErrorText="ERROR: Introduzca el producto." IsRequired="True" />
                                                                </ValidationSettings>
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxComboBox>
                                                        </td>
                                                        <td style="width: 25%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblUnidadEf" runat="server" Font-Bold="True" ForeColor="#666666" Text="Unidad de Medida :">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 25%">
                                                            <dx:ASPxComboBox ID="cmbUnidadEf" runat="server" ClientInstanceName="cmbUnidadEf" ReadOnly="True" TextField="NOMBRE" TextFormatString="{0}" Theme="Metropolis" ValueField="ID_CATALOGO" Width="260px">
                                                                <ClientSideEvents Init="function(s, e) {
	
                                    var item = cmbProdEf.GetSelectedItem();
                                    if(item)
                                    {
                                    s.SetValue(item.GetColumnText('ID_SUB_IDENTIFICADOR'));
                                    }

                                    }" />
                                                                <Columns>
                                                                    <dx:ListBoxColumn Caption="NOMBRE" FieldName="NOMBRE" Name="NOMBRE" />
                                                                    <dx:ListBoxColumn Caption="ID_CATALOGO" FieldName="ID_CATALOGO" Name="ID_CATALOGO" Width="0px" />
                                                                    <dx:ListBoxColumn Caption="CODIGO" FieldName="CODIGO" Name="CODIGO" Width="0px" />
                                                                </Columns>
                                                                <ClearButton Visibility="Auto">
                                                                </ClearButton>
                                                                <ReadOnlyStyle BackColor="#FFE1C4">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 25%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblCantidadEf" runat="server" Font-Bold="True" ForeColor="#666666" Text="Cantidad :">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 25%">
                                                            <dx:ASPxTextBox ID="txtCantidadEf" runat="server" ClientInstanceName="txtCantidadEf" Text="0" Width="260px">
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
                                                        </td>
                                                        <td style="width: 25%; text-align: right;">&nbsp;</td>
                                                        <td style="width: 25%">&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 25%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblTipoMedicEf" runat="server" Font-Bold="True" ForeColor="#666666" Text="Tipo Medición :">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 25%">
                                                            <dx:ASPxComboBox ID="cmbTipoMedicEf" runat="server" Style="margin-top: 0px" TextField="NOMBRE" TextFormatString="{0}" Theme="Metropolis" ValueField="ID_CATALOGO" Width="260px">
                                                                <Columns>
                                                                    <dx:ListBoxColumn Caption="NOMBRE" FieldName="NOMBRE" Name="NOMBRE" />
                                                                    <dx:ListBoxColumn Caption="ID_CATALOGO" FieldName="ID_CATALOGO" Name="ID_CATALOGO" Width="0px" />
                                                                    <dx:ListBoxColumn Caption="CODIGO" FieldName="CODIGO" Name="CODIGO" Width="0px" />
                                                                </Columns>
                                                                <ClearButton Visibility="Auto">
                                                                </ClearButton>
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxComboBox>
                                                        </td>
                                                        <td style="width: 25%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblVolAguaEf" runat="server" Font-Bold="True" ForeColor="#666666" Text="Volumen de Agua :" Width="130px">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 25%">
                                                            <dx:ASPxTextBox ID="txtVolAguaEf" runat="server" ClientInstanceName="txtId" Text="0" Width="260px">
                                                                <ClientSideEvents TextChanged="function(s, e) {
	                                    s.SetText(s.GetText().toUpperCase());
                                    }" />
                                                                <MaskSettings IncludeLiterals="DecimalSymbol" Mask="&lt;0..9999999&gt;.&lt;00000..99999&gt;" />
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 25%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblPlacaEf" runat="server" Font-Bold="True" ForeColor="#666666" Text="Identificador :">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 25%">
                                                            <dx:ASPxTextBox ID="txtPlacaEf" runat="server" MaxLength="11" Width="260px">
                                                                <ClientSideEvents KeyPress="function(s, e) {
	                                    //if (!((event.keyCode &gt;= 48 &amp;&amp; event.keyCode &lt;= 57)||(event.keyCode &gt;= 65 &amp;&amp; event.keyCode &lt;= 70)||(event.keyCode &gt;= 97 &amp;&amp; event.keyCode &lt;= 102)) ) event.returnValue = false;

                                    //if ((event.keyCode&gt;32&amp;&amp;event.keyCode&lt;38)||(event.keyCode&gt;38&amp;&amp;event.keyCode&lt;48)) event.returnValue = false;

                                    if (!((event.keyCode&gt;47&amp;&amp;event.keyCode&lt;58)||(event.keyCode&gt;64&amp;&amp;event.keyCode&lt;91)||(event.keyCode&gt;96&amp;&amp;event.keyCode&lt;123))) event.returnValue = false;

                                    }"
                                                                    TextChanged="function(s, e) {
	                                    s.SetText(s.GetText().toUpperCase());
                                    }" />
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                        <td style="width: 25%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblSubIdentEf" runat="server" Font-Bold="True" ForeColor="#666666" Text="Sub Identificador :">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 25%">
                                                            <dx:ASPxTextBox ID="txtSubIdentEf" runat="server" MaxLength="15" Width="260px">
                                                                <ClientSideEvents KeyPress="function(s, e) {
	                                    if (!((event.keyCode&gt;47&amp;&amp;event.keyCode&lt;58)||(event.keyCode&gt;64&amp;&amp;event.keyCode&lt;91)||(event.keyCode&gt;96&amp;&amp;event.keyCode&lt;123))) event.returnValue = false;

                                    }"
                                                                    TextChanged="function(s, e) {
	                                    s.SetText(s.GetText().toUpperCase());
                                    }" />
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 25%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblBarCodeEf" runat="server" ClientInstanceName="lblBarCodeEf" Font-Bold="True" ForeColor="#666666" Text="Código de Barras :">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 25%">
                                                            <dx:ASPxTextBox ID="txtBarCodeEf" runat="server" ClientInstanceName="txtBarCodeEf" MaxLength="11" Width="260px" Text="0">
                                                                <ClientSideEvents KeyPress="function(s, e) {
	                                    //if (!((event.keyCode &gt;= 48 &amp;&amp; event.keyCode &lt;= 57)||(event.keyCode &gt;= 65 &amp;&amp; event.keyCode &lt;= 70)||(event.keyCode &gt;= 97 &amp;&amp; event.keyCode &lt;= 102)) ) event.returnValue = false;

                                    //if ((event.keyCode&gt;32&amp;&amp;event.keyCode&lt;38)||(event.keyCode&gt;38&amp;&amp;event.keyCode&lt;48)) event.returnValue = false;

                                    if (!((event.keyCode&gt;47&amp;&amp;event.keyCode&lt;58)||(event.keyCode&gt;64&amp;&amp;event.keyCode&lt;91)||(event.keyCode&gt;96&amp;&amp;event.keyCode&lt;123))) event.returnValue = false;

                                    }"
                                                                    TextChanged="function(s, e) {
	                                    s.SetText(s.GetText().toUpperCase());
                                    }" />
                                                                <MaskSettings Mask="&lt;0..99999999999&gt;" />
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                        <td style="width: 25%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblTipoDespachoEf" runat="server" ClientInstanceName="lblTipoDespachoEf" Font-Bold="True" ForeColor="#666666" Text="Tipo Despacho :">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 25%">
                                                            <dx:ASPxComboBox ID="cmbTipoDespachoEf" runat="server" ClientInstanceName="cmbTipoDespachoEf" TextField="DESCRIPCION" TextFormatString="{0}" Theme="Metropolis" ValueField="ID_PARAMETRICA_GLOBAL" Width="260px">
                                                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
	                                    if(cmbTipoPagoEf.GetValue()==3 || cmbTipoPagoEf.GetValue()==-99999){
                                    cmbTipoCredEf.SetValue(9);
	                                    }
                                    else{
                                    cmbTipoCredEf.SetValue(10);
                                    }
                                    }" />
                                                                <Columns>
                                                                    <dx:ListBoxColumn Caption="DESCRIPCION" FieldName="DESCRIPCION" Name="DESCRIPCION" />
                                                                    <dx:ListBoxColumn Caption="ID_PARAMETRICA_GLOBAL" FieldName="ID_PARAMETRICA_GLOBAL" Name="ID_PARAMETRICA_GLOBAL" Width="0px" />
                                                                    <dx:ListBoxColumn Caption="CODIGO" FieldName="CODIGO" Name="CODIGO" Width="0px" />
                                                                </Columns>
                                                                <ClearButton Visibility="Auto">
                                                                </ClearButton>
                                                                <ValidationSettings>
                                                                    <RequiredField ErrorText="ERROR : Introduzca la forma de pago." IsRequired="True" />
                                                                </ValidationSettings>
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 25%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblCodigoEf" runat="server" Font-Bold="True" ForeColor="#666666" Text="Código Tanque Cisterna :">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 25%">
                                                            <dx:ASPxTextBox ID="txtCodigoEf" runat="server" MaxLength="11" Width="260px">
                                                                <ClientSideEvents KeyPress="function(s, e) {
                                    if (!((event.keyCode&gt;47&amp;&amp;event.keyCode&lt;58)||(event.keyCode&gt;64&amp;&amp;event.keyCode&lt;91)||(event.keyCode&gt;96&amp;&amp;event.keyCode&lt;123))) event.returnValue = false;
                                    }"
                                                                    TextChanged="function(s, e) {
	                                    s.SetText(s.GetText().toUpperCase());
                                    }" />
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                        <td style="width: 25%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblTipoEnvaseEf" runat="server" ClientInstanceName="lblTipoEnvaseEf" Font-Bold="True" ForeColor="#666666" Text="Tipo Envase p/Despacho :">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 25%">
                                                            <dx:ASPxComboBox ID="cmbTipoEnvaseEf" runat="server" ClientInstanceName="cmbTipoEnvaseEf" TextField="NOMBRE" TextFormatString="{0}" Theme="Metropolis" ValueField="ID_TIPO_CONTENEDOR" Width="260px">
                                                                <Columns>
                                                                    <dx:ListBoxColumn Caption="NOMBRE" FieldName="NOMBRE" Name="NOMBRE" />
                                                                    <dx:ListBoxColumn Caption="ID_TIPO_CONTENEDOR" FieldName="ID_TIPO_CONTENEDOR" Name="ID_TIPO_CONTENEDOR" Width="0px" />
                                                                    <dx:ListBoxColumn Caption="CODIGO" FieldName="CODIGO" Name="CODIGO" Width="0px" />
                                                                </Columns>
                                                                <ClearButton Visibility="Auto">
                                                                </ClearButton>
                                                                <ValidationSettings>
                                                                    <RequiredField ErrorText="ERROR: Introduzca el tipo de transporte." IsRequired="True" />
                                                                </ValidationSettings>
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 25%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblTemperaturaEf" runat="server" Font-Bold="True" ForeColor="#666666" Text="Temperatura :">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 25%">
                                                            <dx:ASPxTextBox ID="txtTemperaturaEf" runat="server" ClientInstanceName="txtCantidadEf" MaxLength="7" Text="0" Width="50%">
                                                                <MaskSettings IncludeLiterals="None" Mask="&lt;0..9999999&gt;" />
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
                                                            <dx:ASPxComboBox ID="cmbTipoTempEf" runat="server" ClientInstanceName="cmbTipoTempEf" TextField="NOMBRE" TextFormatString="{0}" Theme="Metropolis" ValueField="ID_CATALOGO" Width="45%">
                                                                <Columns>
                                                                    <dx:ListBoxColumn Caption="NOMBRE" FieldName="NOMBRE" Name="NOMBRE" />
                                                                    <dx:ListBoxColumn Caption="ID_CATALOGO" FieldName="ID_CATALOGO" Name="ID_CATALOGO" Width="0px" />
                                                                    <dx:ListBoxColumn Caption="CODIGO" FieldName="CODIGO" Name="CODIGO" Width="0px" />
                                                                </Columns>
                                                                <ClearButton Visibility="Auto">
                                                                </ClearButton>
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxComboBox>
                                                        </td>
                                                        <td style="width: 25%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblGravedadEf" runat="server" Font-Bold="True" ForeColor="#666666" Text="Gravedad Especifica :">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 25%">
                                                            <dx:ASPxTextBox ID="txtGravedadEf" runat="server" MaxLength="11" Width="260px">
                                                                <ClientSideEvents KeyPress="function(s, e) {
                                    if (!((event.keyCode&gt;47&amp;&amp;event.keyCode&lt;58)||(event.keyCode&gt;64&amp;&amp;event.keyCode&lt;91)||(event.keyCode&gt;96&amp;&amp;event.keyCode&lt;123))) event.returnValue = false;
                                    }"
                                                                    TextChanged="function(s, e) {
	                                    s.SetText(s.GetText().toUpperCase());
                                    }" />
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 25%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblObsEf" runat="server" Font-Bold="True" ForeColor="#666666" Text="Observaciones :" Width="150px">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td colspan="3">
                                                            <dx:ASPxTextBox ID="txtObsEf" runat="server" MaxLength="50" Width="98%">
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 25%; text-align: right;">
                                                            <dx:ASPxTextBox ID="txtJsonRef" runat="server" Width="260px" ClientVisible="False">
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                        <td style="width: 25%;">
                                                            <dx:ASPxTextBox ID="txtEstJsonRef" runat="server" Width="260px" ClientVisible="False">
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                        <td style="width: 25%; text-align: left;">
                                                            <dx:ASPxTextBox ID="txtIdCuenta" runat="server" Width="260px" ClientVisible="False">
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                        <td style="width: 25%;">
                                                            <dx:ASPxTextBox ID="txtIdCuentaAux" runat="server" Width="260px" ClientVisible="False">
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 25%; text-align: right;">
                                                            <dx:ASPxTextBox ID="txtIdParGl" runat="server" Width="260px" ClientVisible="False">
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                        <td style="width: 25%;">
                                                            <dx:ASPxTextBox ID="txtIdParEs" runat="server" Width="260px" ClientVisible="False">
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                        <td style="width: 25%; text-align: left;">
                                                            <dx:ASPxTextBox ID="txtIdInfAux" runat="server" ClientVisible="False" Width="260px">
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                        <td style="width: 25%;">
                                                            <dx:ASPxTextBox ID="txtIdTipoInfAux" runat="server" Width="260px" ClientVisible="False">
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 25%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblTipoDocEf" runat="server" Font-Bold="True" ForeColor="#666666" Text="Tipo Documento :." Width="150px">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 25%;">
                                                            <dx:ASPxComboBox ID="cmbTipoDocEf" runat="server" ClientInstanceName="cmbTipoDocEf" TextField="NOMBRE" TextFormatString="{0};{3}" Theme="Metropolis" ValueField="ID_TIPO_TRANSACCION" Width="260px">
                                                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
                                    var item = s.GetSelectedItem();
                                    if(item)
                                    {
                                    if(item.GetColumnText('ID_TIPO_TRANSACCION_PADRE')=='1')
                                    {
                                    txtClienteEf.SetVisible(true);
                                    lblClienteEf.SetVisible(true);
                                    lblNitEf.SetVisible(true);
                                    txtNitEf.SetVisible(true);
                                    }
                                    else
                                    {
                                    txtClienteEf.SetVisible(false);
                                    lblClienteEf.SetVisible(false);
                                    lblNitEf.SetVisible(false);
                                    txtNitEf.SetVisible(false);
                                    }
                                    }
                                    }" />
                                                                <Columns>
                                                                    <dx:ListBoxColumn Caption="NOMBRE" FieldName="NOMBRE" Name="NOMBRE" />
                                                                    <dx:ListBoxColumn Caption="ID_TIPO_TRANSACCION" FieldName="ID_TIPO_TRANSACCION" Name="ID_TIPO_TRANSACCION" Width="0px" />
                                                                    <dx:ListBoxColumn Caption="CODIGO" FieldName="CODIGO" Name="CODIGO" Width="0px" />
                                                                    <dx:ListBoxColumn Caption="ID_TIPO_TRANSACCION_PADRE" FieldName="ID_TIPO_TRANSACCION_PADRE" Name="ID_TIPO_TRANSACCION_PADRE" Width="0px" />
                                                                </Columns>
                                                                <ClearButton Visibility="Auto">
                                                                </ClearButton>
                                                                <ValidationSettings>
                                                                    <RequiredField ErrorText="ERROR: Introduzca la moneda." IsRequired="True" />
                                                                </ValidationSettings>
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxComboBox>
                                                        </td>
                                                        <td style="width: 25%; text-align: left;">
                                                            <dx:ASPxButton ID="btnAgrega" runat="server" AutoPostBack="False" Font-Bold="True" ForeColor="Maroon" Text="Agregar Documento" Theme="Metropolis" Width="160px">
                                                                <ClientSideEvents Click="function(s, e) {
                                    txtIdDocEf.SetValue(0);
                                    pucDocumento.PerformCallback();
                                    pucDocumento.Show();
//pcCreateAccount.Show();
                                    }" />
                                                                <Image IconID="reports_addgroupheader_16x16">
                                                                </Image>
                                                            </dx:ASPxButton>
                                                        </td>
                                                        <td style="width: 25%;">&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 25%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblClienteEf" runat="server" ClientInstanceName="lblClienteEf" Font-Bold="True" ForeColor="#666666" Text="Razón Social :">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td colspan="3" style="width: 25%">
                                                            <dx:ASPxTextBox ID="txtClienteEf" runat="server" ClientInstanceName="txtClienteEf" Height="16px" MaxLength="90" Width="98%">
                                                                <ClientSideEvents KeyPress="function(s, e) {
	                                    if ((event.keyCode&gt;32&amp;&amp;event.keyCode&lt;38)||(event.keyCode&gt;38&amp;&amp;event.keyCode&lt;46)||(event.keyCode&gt;57&amp;&amp;event.keyCode&lt;64)) event.returnValue = false;
                                    }"
                                                                    TextChanged="function(s, e) {
	                                                                                                                s.SetText(s.GetText().toUpperCase());
                                                                                                                }" />
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 25%; text-align: right; padding-bottom: 10px;">
                                                            <dx:ASPxLabel ID="lblNitEf" runat="server" ClientInstanceName="lblNitEf" Font-Bold="True" ForeColor="#666666" Text="NIT :">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 25%; padding-bottom: 10px;">
                                                            <dx:ASPxTextBox ID="txtNitEf" runat="server" ClientInstanceName="txtNitEf" MaxLength="15" Text="0" Width="260px">
                                                                <MaskSettings Mask="&lt;0..999999999999999&gt;" PromptChar=" " />
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                        <td style="width: 25%; padding-bottom: 10px; text-align: left;">&nbsp;</td>
                                                        <td style="width: 25%; padding-bottom: 10px;">&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" style="width: 100%;">
                                                            <dx:ASPxGridView ID="dgvDocumentos" runat="server" AutoGenerateColumns="False" ClientInstanceName="dgvDocumentos" EnableTheming="True" Font-Size="Smaller" KeyFieldName="ID_TRANSACCION" OnCustomButtonCallback="dgvDocumentos_CustomButtonCallback" OnCustomCallback="dgvDocumentos_CustomCallback" OnHtmlRowPrepared="dgvDocumentos_HtmlRowPrepared" Theme="Default" Width="100%">
                                                                <ClientSideEvents CustomButtonClick="function(s, e) {
	
                                    switch(e.buttonID) {

                                    case&nbsp; 'btnEditar':
                                    txtIdDocEf.SetValue(1);
                                    pucDocumento.Show();
                                    pucDocumento.PerformCallback();
                                    break;

                                    case&nbsp; 'btnQuitar':
                                    e.processOnServer = confirm('¿Desea quitar el documento? - Una vez realizada la acción y despues de grabar el movimiento no podrá revertir el proceso. ¿Desea continuar?'); 
                                    break;

                                    case&nbsp; 'btnAgregar':
                                    e.processOnServer = confirm('¿Desea agregar el documento?'); 
                                    break;

                                    }

                                    }" />
                                                                <SettingsContextMenu EnableRowMenu="False">
                                                                </SettingsContextMenu>
                                                                <Templates>
                                                                    <EditForm>
                                                                    </EditForm>
                                                                </Templates>
                                                                <SettingsPager PageSize="50" Visible="False">
                                                                    <PageSizeItemSettings Items="50, 100, 200, 500, 1000" Visible="True">
                                                                    </PageSizeItemSettings>
                                                                </SettingsPager>
                                                                <SettingsEditing Mode="PopupEditForm">
                                                                </SettingsEditing>
                                                                <Settings ShowFooter="True" ShowHeaderFilterButton="True" />
                                                                <SettingsBehavior AllowFocusedRow="True" />
                                                                <SettingsPopup>
                                                                    <EditForm Modal="True" />
                                                                    <CustomizationWindow HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" />
                                                                </SettingsPopup>
                                                                <SettingsText CommandNew="Nuevo" />
                                                                <Columns>
                                                                    <dx:GridViewCommandColumn ShowInCustomizationForm="True" VisibleIndex="19">
                                                                        <CustomButtons>
                                                                            <dx:GridViewCommandColumnCustomButton ID="btnEditar" Text="Editar">
                                                                            </dx:GridViewCommandColumnCustomButton>
                                                                            <dx:GridViewCommandColumnCustomButton ID="btnQuitar" Text="Quitar">
                                                                            </dx:GridViewCommandColumnCustomButton>
                                                                            <dx:GridViewCommandColumnCustomButton ID="btnAgregar" Text="Agregar">
                                                                            </dx:GridViewCommandColumnCustomButton>
                                                                        </CustomButtons>
                                                                    </dx:GridViewCommandColumn>
                                                                    <dx:GridViewDataTextColumn Caption="NO. FACT./DOC." FieldName="NRO_TRANSACCION" ShowInCustomizationForm="True" VisibleIndex="14">
                                                                        <Settings AutoFilterCondition="Contains" />
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="OBSERVACIONES" FieldName="OBSERVACIONES" ShowInCustomizationForm="True" Visible="False" VisibleIndex="21">
                                                                        <Settings AutoFilterCondition="Contains" />
                                                                        <CellStyle HorizontalAlign="Right">
                                                                        </CellStyle>
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="EMISOR" FieldName="TIPO_EMISOR" ShowInCustomizationForm="True" VisibleIndex="6">
                                                                        <Settings AutoFilterCondition="Contains" />
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="TIPO DOC." FieldName="TIPO_TRANSACCION" ShowInCustomizationForm="True" VisibleIndex="4">
                                                                        <Settings AutoFilterCondition="Equals" />
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="NO. AUTORIZACION" FieldName="NRO_AUTORIZACION" ShowInCustomizationForm="True" VisibleIndex="13">
                                                                        <Settings AutoFilterCondition="Contains" />
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="PRECIO" FieldName="PRECIO_UNITARIO" ShowInCustomizationForm="True" VisibleIndex="17">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="IMPORTE" FieldName="PRECIO_TOTAL" ShowInCustomizationForm="True" VisibleIndex="18">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="FECHA" FieldName="FECHA_HORA" ShowInCustomizationForm="True" VisibleIndex="2">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="CANTIDAD" FieldName="CANTIDAD" Visible="False" VisibleIndex="16">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="ID" FieldName="ID_TRANSACCION" VisibleIndex="0">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="USUARIO_TRANSACCION" FieldName="USUARIO_TRANSACCION" Visible="False" VisibleIndex="20">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="COD. CTRL." FieldName="CODIGO_CONTROL" VisibleIndex="15">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="ID_MOVIMIENTO" FieldName="ID_MOVIMIENTO" Visible="False" VisibleIndex="22">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="ID_TIPO_MONEDA" FieldName="ID_TIPO_MONEDA" Visible="False" VisibleIndex="23">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="ID_ESTADO_TRANSACCION" FieldName="ID_ESTADO_TRANSACCION" Visible="False" VisibleIndex="24">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="FECHA_HORA_EMISION" FieldName="FECHA_HORA_EMISION" Visible="False" VisibleIndex="1">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="ID_TIPO_TRANSACCION" FieldName="ID_TIPO_TRANSACCION" Visible="False" VisibleIndex="3">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="ID_TIPO_EMISOR" FieldName="ID_TIPO_EMISOR" Visible="False" VisibleIndex="5">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="ID_FORMA_PAGO" FieldName="ID_FORMA_PAGO" Visible="False" VisibleIndex="7">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="FORMA PAGO " FieldName="FORMA_PAGO" VisibleIndex="8">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="ID_TIPO_CREDITO_FISCAL" FieldName="ID_TIPO_CREDITO_FISCAL" Visible="False" VisibleIndex="9">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="TIPO_CREDITO_FISCAL" FieldName="TIPO_CREDITO_FISCAL" VisibleIndex="10">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="NIT_CONSUMIDOR" FieldName="NIT_CONSUMIDOR" Visible="False" VisibleIndex="11">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="RAZON_SOCIAL" FieldName="RAZON_SOCIAL" Visible="False" VisibleIndex="12">
                                                                    </dx:GridViewDataTextColumn>
                                                                </Columns>
                                                                <TotalSummary>
                                                                    <dx:ASPxSummaryItem FieldName="DecRecepcion" ShowInColumn="RECEPCION" SummaryType="Custom" Tag="rec" />
                                                                    <dx:ASPxSummaryItem FieldName="DecDemasia" ShowInColumn="DEMASIA" SummaryType="Custom" Tag="dem" />
                                                                    <dx:ASPxSummaryItem FieldName="DecTotalIng" ShowInColumn="TOT. ING." SummaryType="Custom" Tag="ing" />
                                                                    <dx:ASPxSummaryItem FieldName="DecVenta" ShowInColumn="VENTA" SummaryType="Custom" Tag="ven" />
                                                                    <dx:ASPxSummaryItem FieldName="DecMerma" ShowInColumn="MERMA" SummaryType="Custom" Tag="mer" />
                                                                    <dx:ASPxSummaryItem FieldName="DecTotalSal" ShowInColumn="TOT. SAL." SummaryType="Custom" Tag="sal" />
                                                                </TotalSummary>
                                                            </dx:ASPxGridView>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 25%; text-align: right; padding-top: 10px;">
                                                            <dx:ASPxTextBox ID="txtAdicionalEf" runat="server" ForeColor="White" ReadOnly="True" ViewStateMode="Disabled" Width="170px" ClientEnabled="False">
                                                                <Border BorderStyle="None" />
                                                                <BorderBottom BorderStyle="None" />
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                        <td style="width: 25%; padding-top: 10px;">&nbsp;</td>
                                                        <td style="width: 25%; padding-top: 10px; text-align: right;">
                                                            <dx:ASPxButton ID="btnAceptar" runat="server" AutoPostBack="False" ClientInstanceName="btnAceptar" Text="Grabar Movimiento" Theme="Metropolis" Width="100%" Font-Bold="True" ForeColor="Maroon">
                                                                <ClientSideEvents Click="function(s, e) {
	                                                                                                                                dgvTransacciones.UpdateEdit();
                                                                                                                                }" />
                                                                <Image IconID="actions_apply_16x16">
                                                                </Image>
                                                            </dx:ASPxButton>
                                                        </td>
                                                        <td style="width: 25%; padding-top: 10px;">
                                                            <dx:ASPxButton ID="btnCancelar" runat="server" AutoPostBack="False" ClientInstanceName="btnCancelar" Text="Volver" Theme="Metropolis" Width="40%" Font-Bold="True" ForeColor="Maroon">
                                                                <ClientSideEvents Click="function(s, e) {
                                                                                                                    dgvTransacciones.CancelEdit();
                                                                                                            }" />
                                                                <Image IconID="actions_reset_16x16">
                                                                </Image>
                                                            </dx:ASPxButton>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" style="text-align: right;">&nbsp;</td>
                                                    </tr>
                                                </table>

                                            </EditForm>


                                        </Templates>
                                        <SettingsPager NumericButtonCount="5">
                                            <PageSizeItemSettings Items="10, 100, 1000" Visible="True" ShowAllItem="True">
                                            </PageSizeItemSettings>
                                        </SettingsPager>
                                        <SettingsEditing Mode="PopupEditForm">
                                        </SettingsEditing>
                                        <Settings ShowFilterRow="True" ShowFooter="True" ShowGroupPanel="True" ShowHeaderFilterButton="True" />
                                        <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" AutoExpandAllGroups="True" />
                                        <SettingsCommandButton>
                                            <NewButton Text=" ">
                                                <Image IconID="reports_addgroupheader_16x16" ToolTip="Nuevo Movimiento....">
                                                </Image>
                                            </NewButton>
                                            <EditButton Text=" ">
                                                <Image IconID="edit_edit_16x16" ToolTip="Editar">
                                                </Image>
                                            </EditButton>
                                        </SettingsCommandButton>
                                        <SettingsPopup>
                                            <EditForm HorizontalAlign="Center" Modal="True" VerticalAlign="Middle" VerticalOffset="0" CloseOnEscape="True" />
                                            <CustomizationWindow HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" />
                                        </SettingsPopup>
                                        <SettingsSearchPanel Visible="True" />
                                        <SettingsText CommandNew="Nuevo" />

                                        <Columns>
                                            <dx:GridViewCommandColumn ShowEditButton="True" ShowInCustomizationForm="True" ShowNewButtonInHeader="True" ToolTip="Funciones" VisibleIndex="51">
                                                <CustomButtons>
                                                    <dx:GridViewCommandColumnCustomButton ID="btnAnular" Text=" ">
                                                        <Image IconID="databars_gradientreddatabar_16x16" ToolTip="Anular">
                                                        </Image>
                                                    </dx:GridViewCommandColumnCustomButton>
                                                    <dx:GridViewCommandColumnCustomButton ID="btnActivar" Text=" ">
                                                        <Image IconID="databars_solidgreendatabar_16x16" ToolTip="Activar">
                                                        </Image>
                                                    </dx:GridViewCommandColumnCustomButton>
                                                    <dx:GridViewCommandColumnCustomButton ID="btnEliminar" Text=" ">
                                                        <Image IconID="reports_deletegroupfooter_16x16office2013" ToolTip="Eliminar">
                                                        </Image>
                                                    </dx:GridViewCommandColumnCustomButton>
                                                </CustomButtons>
                                                <CellStyle ForeColor="#006600">
                                                </CellStyle>
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewDataTextColumn Caption="FECHA_OPERACION" FieldName="FECHA_OPERACION" ShowInCustomizationForm="True" ToolTip="Fecha del Movimiento" Visible="False" VisibleIndex="5">
                                                <Settings AutoFilterCondition="Contains" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="ORIGEN REG." FieldName="COD_FUENTE_ORIGEN" ShowInCustomizationForm="True" ToolTip="Origen del Registro" VisibleIndex="8">
                                                <Settings AutoFilterCondition="Contains" />
                                                <CellStyle HorizontalAlign="Center">
                                                </CellStyle>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="T.MOV." FieldName="TIPO_MOVIMIENTO" ShowInCustomizationForm="True" ToolTip="Tipo de Movimiento" VisibleIndex="9">
                                                <Settings AutoFilterCondition="Contains" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="NIT" FieldName="NIT_CONSUMIDOR" ShowInCustomizationForm="True" VisibleIndex="32">
                                                <Settings AutoFilterCondition="Contains" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="RAZON SOCIAL" FieldName="RAZON_SOCIAL" ShowInCustomizationForm="True" VisibleIndex="33">
                                                <Settings AutoFilterCondition="Contains" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="PLACA" FieldName="IDENTIFICADOR" ShowInCustomizationForm="True" VisibleIndex="21">
                                                <Settings AutoFilterCondition="Contains" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="OBSERVACION_MOVIMIENTO" FieldName="OBSERVACION_MOVIMIENTO" ShowInCustomizationForm="True" Visible="False" VisibleIndex="52">
                                                <Settings AutoFilterCondition="Contains" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="TIPO TRANSACCION" FieldName="TIPO_TRANSACCION" ShowInCustomizationForm="True" VisibleIndex="27">
                                                <Settings AutoFilterCondition="Contains" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="NO. FACT./DOC." FieldName="NRO_TRANSACCION" ShowInCustomizationForm="True" ToolTip="Número de Factura o Documento" VisibleIndex="35">
                                                <Settings AutoFilterCondition="Contains" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="(NRO/COD) AUTORIZACION" FieldName="CODIGO_CUF" ShowInCustomizationForm="True" ToolTip="Número de Autorización" VisibleIndex="34">
                                                <Settings AutoFilterCondition="Contains" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="USUARIO" FieldName="APP_USUARIO" ShowInCustomizationForm="True" ToolTip="Entidad de Referencia" Visible="False" VisibleIndex="49">
                                                <Settings AutoFilterCondition="Contains" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="ID_ESTADO_MOVIMIENTO" FieldName="ID_ESTADO_MOVIMIENTO" ShowInCustomizationForm="True" Visible="False" VisibleIndex="11">
                                                <Settings AutoFilterCondition="Contains" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="ID_HIDROCARBURO" FieldName="ID_HIDROCARBURO" ShowInCustomizationForm="True" Visible="False" VisibleIndex="26">
                                                <Settings AutoFilterCondition="Contains" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="PRECIO" FieldName="PRECIO_UNITARIO" ShowInCustomizationForm="True" VisibleIndex="38">
                                                <Settings AutoFilterCondition="Equals" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="CANTIDAD" FieldName="CANTIDAD" ShowInCustomizationForm="True" VisibleIndex="37">
                                                <Settings AutoFilterCondition="Equals" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="IMPORTE" FieldName="PRECIO_TOTAL" ShowInCustomizationForm="True" VisibleIndex="39">
                                                <Settings AutoFilterCondition="Equals" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="ID" FieldName="ID_MOVIMIENTO" ShowInCustomizationForm="True" ToolTip="Identificador del Registro del Movimiento" VisibleIndex="0">
                                                <Settings AutoFilterCondition="Equals" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="ID_CONSUMIDOR" FieldName="ID_CONSUMIDOR" ShowInCustomizationForm="True" Visible="False" VisibleIndex="2">
                                                <Settings AutoFilterCondition="Equals" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="ID_INFRAESTRUCTURA" FieldName="ID_INFRAESTRUCTURA" ShowInCustomizationForm="True" Visible="False" VisibleIndex="24">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="ID_TIPO_TRANSACCION" FieldName="ID_TIPO_TRANSACCION" ShowInCustomizationForm="True" Visible="False" VisibleIndex="31">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="ID_UNIDAD_ENVASES" FieldName="ID_UNIDAD_ENVASES" ShowInCustomizationForm="True" Visible="False" VisibleIndex="44">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="GRADO_API" FieldName="GRADO_API" ShowInCustomizationForm="True" Visible="False" VisibleIndex="42">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="TEMPERATURA" FieldName="TEMPERATURA" ShowInCustomizationForm="True" Visible="False" VisibleIndex="40">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="ID_TIPO_FLUJO" FieldName="ID_TIPO_FLUJO" ShowInCustomizationForm="True" Visible="False" VisibleIndex="13">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="ID_TIPO_OPERACION" FieldName="ID_TIPO_OPERACION" ShowInCustomizationForm="True" Visible="False" VisibleIndex="4">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="ID_TIPO_MEDIO_TRANSPORTE" FieldName="ID_TIPO_MEDIO_TRANSPORTE" ShowInCustomizationForm="True" Visible="False" VisibleIndex="19">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="ID_MEDIO_TRANSPORTE" FieldName="ID_MEDIO_TRANSPORTE" ShowInCustomizationForm="True" Visible="False" VisibleIndex="23">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="ID_TIPO_MEDICION" FieldName="ID_TIPO_MEDICION" ShowInCustomizationForm="True" Visible="False" VisibleIndex="18">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="ID_UNIDAD_TEMPERATURA" FieldName="ID_UNIDAD_TEMPERATURA" ShowInCustomizationForm="True" Visible="False" VisibleIndex="41">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="CANTIDAD_ENVASES" FieldName="CANTIDAD_ENVASES" ShowInCustomizationForm="True" Visible="False" VisibleIndex="43">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="APP_FECHA_REGISTRO" FieldName="APP_FECHA_REGISTRO" ShowInCustomizationForm="True" VisibleIndex="50">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="ID_ENTIDAD" FieldName="ID_ENTIDAD" ShowInCustomizationForm="True" Visible="False" VisibleIndex="17">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="ID_TIPO_MOVIMIENTO" FieldName="ID_TIPO_MOVIMIENTO" ShowInCustomizationForm="True" Visible="False" VisibleIndex="14">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="SUB_IDENTIFICADOR" FieldName="SUB_IDENTIFICADOR" ShowInCustomizationForm="True" Visible="False" VisibleIndex="30">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="PAIS" FieldName="UBICACION_OPERACION" ShowInCustomizationForm="True" VisibleIndex="10">
                                                <Settings AutoFilterCondition="Contains" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="EST." FieldName="ESTADO_MOVIMIENTO" ShowInCustomizationForm="True" ToolTip="Estado del Movimiento" VisibleIndex="12">
                                                <Settings HeaderFilterMode="CheckedList" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="PRODUCTO" FieldName="COD_HIDROCARBURO" ShowInCustomizationForm="True" VisibleIndex="36">
                                                <Settings AutoFilterCondition="Contains" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="T.FLUJO" FieldName="TIPO_FLUJO" ShowInCustomizationForm="True" ToolTip="Tipo de Flujo" VisibleIndex="7">
                                                <Settings AutoFilterCondition="Contains" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="ID_MOVIMIENTO_PADRE" FieldName="ID_MOVIMIENTO_PADRE" ShowInCustomizationForm="True" Visible="False" VisibleIndex="1">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="BINARIO_CONTROL" FieldName="BINARIO_CONTROL" ShowInCustomizationForm="True" Visible="False" VisibleIndex="53">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="ID_FUENTE_ORIGEN" FieldName="ID_FUENTE_ORIGEN" ShowInCustomizationForm="True" Visible="False" VisibleIndex="15">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="FECHA_OPERACION_DIA" FieldName="FECHA_OPERACION_DIA" ShowInCustomizationForm="True" Visible="False" VisibleIndex="54">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="ID_UBICACION_OPERACION" FieldName="ID_UBICACION_OPERACION" ShowInCustomizationForm="True" Visible="False" VisibleIndex="25">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="ID_UNIDAD_HIDROCARBURO" FieldName="ID_UNIDAD_HIDROCARBURO" ShowInCustomizationForm="True" Visible="False" VisibleIndex="29">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="DES_FUENTE_ORIGEN" FieldName="DES_FUENTE_ORIGEN" ShowInCustomizationForm="True" Visible="False" VisibleIndex="16">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="TIPO_MEDIO_TRANSAPORTE" FieldName="TIPO_MEDIO_TRANSAPORTE" ShowInCustomizationForm="True" Visible="False" VisibleIndex="20">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="DES_HIDROCARBURO " FieldName="DES_HIDROCARBURO " ShowInCustomizationForm="True" Visible="False" VisibleIndex="28">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="ID_TIPO_ACTIVIDAD" FieldName="ID_TIPO_ACTIVIDAD" ShowInCustomizationForm="True" Visible="False" VisibleIndex="3">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="CANTIDAD_ADICIONAL" FieldName="CANTIDAD_ADICIONAL" ShowInCustomizationForm="True" Visible="False" VisibleIndex="45">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="EST.D.J." FieldName="STR_ESTADO_DEC" ShowInCustomizationForm="True" ToolTip="Estado de Declaracion Jurada" VisibleIndex="22" Visible="False">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="FECHA" FieldName="STR_FECHA" ShowInCustomizationForm="True" VisibleIndex="6">
                                                <Settings AutoFilterCondition="Contains" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="CODIGO" FieldName="CODIGO" ShowInCustomizationForm="True" Visible="False" VisibleIndex="55">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="ID_INTEGRACION" FieldName="ID_INTEGRACION" ShowInCustomizationForm="True" Visible="False" VisibleIndex="56">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="BARCODE" FieldName="BARCODE" ShowInCustomizationForm="True" Visible="False" VisibleIndex="57">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="CANTIDAD (RECAUDO)" FieldName="CANTIDAD_TRANS" ShowInCustomizationForm="True" VisibleIndex="46">
                                                <Settings AutoFilterCondition="Equals" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="PRECIO (RECAUDO)" FieldName="PRECIO_UNITARIO_TRANS" ShowInCustomizationForm="True" VisibleIndex="47">
                                                <Settings AutoFilterCondition="Equals" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="IMPORTE (RECAUDO)" FieldName="PRECIO_TOTAL_TRANS" ShowInCustomizationForm="True" VisibleIndex="48">
                                                <Settings AutoFilterCondition="Equals" />
                                            </dx:GridViewDataTextColumn>
                                        </Columns>
                                        <TotalSummary>
                                            <dx:ASPxSummaryItem FieldName="PRECIO_TOTAL_TRANS" ShowInColumn="IMPORTE (RECAUDO)" SummaryType="Custom" Tag="impRec" />
                                            <dx:ASPxSummaryItem FieldName="CANTIDAD_TRANS" ShowInColumn="CANTIDAD (RECAUDO)" SummaryType="Custom" Tag="volRec" />
                                            <dx:ASPxSummaryItem FieldName="PRECIO_TOTAL" ShowInColumn="IMPORTE" SummaryType="Custom" Tag="imp" />
                                            <dx:ASPxSummaryItem FieldName="CANTIDAD" ShowInColumn="CANTIDAD" SummaryType="Custom" Tag="vol" />
                                            <dx:ASPxSummaryItem FieldName="ESTADO_MOVIMIENTO" ShowInColumn="EST." SummaryType="Custom" Tag="est" />
                                        </TotalSummary>
                                    </dx:ASPxGridView>
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="3"></td>
                            </tr>

                        </table>

                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxCallbackPanel>
        </dx:PopupControlContentControl>
    </ContentCollection>
    <ClientSideEvents Closing="function(s, e) {
	pnlDatos.PerformCallback();
}" />
    <ContentStyle>
        <Paddings PaddingBottom="5px" />
    </ContentStyle>
</dx:ASPxPopupControl>


<dx:ASPxPopupControl ID="pucDocumento" runat="server" CloseAction="CloseButton" CloseOnEscape="true"
    PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Above" ClientInstanceName="pucDocumento"
    HeaderText="Documentos" AllowDragging="True" Modal="True" PopupAnimationType="Fade"
    EnableViewState="False" PopupHorizontalOffset="40" PopupVerticalOffset="40" OnWindowCallback="pucDocumento_WindowCallback" Theme="Metropolis">
    <ClientSideEvents PopUp="function(s, e) { ASPxClientEdit.ClearGroup('createAccountGroup'); }" />
    <HeaderImage IconID="actions_insert_32x32">
    </HeaderImage>
    <SizeGripImage Width="11px" />
    <HeaderStyle>
        <BorderBottom BorderStyle="Solid" />
    </HeaderStyle>
    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
            <dx:ASPxPanel ID="Panel2" runat="server" DefaultButton="btCreate">
                <PanelCollection>
                    <dx:PanelContent ID="PanelContent8" runat="server">
                        <table class="dxflInternalEditorTable_Youthful">

                            <tr>
                                <td style="width: 25%; text-align: right;">
                                    <dx:ASPxLabel ID="ASPxLabel40" runat="server" Font-Bold="True" ForeColor="#666666" Text="Fecha :">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 25%;">
                                    <dx:ASPxDateEdit ID="detFechaDocEf" runat="server" EditFormat="DateTime" Font-Bold="False" Theme="Metropolis">
                                        <TimeSectionProperties>
                                            <TimeEditProperties>
                                                <ClearButton Visibility="Auto">
                                                </ClearButton>
                                            </TimeEditProperties>
                                        </TimeSectionProperties>
                                        <ClearButton Visibility="Auto">
                                        </ClearButton>
                                        <ValidationSettings>
                                            <RequiredField ErrorText="ERROR: Introduzca la fecha." />
                                        </ValidationSettings>
                                        <ReadOnlyStyle BackColor="#DEE8DB">
                                        </ReadOnlyStyle>
                                        <FocusedStyle BackColor="#FFFFCC">
                                        </FocusedStyle>
                                        <DisabledStyle BackColor="#DEE8DB">
                                        </DisabledStyle>
                                    </dx:ASPxDateEdit>
                                </td>
                                <td style="width: 25%; text-align: right;">&nbsp;</td>
                                <td style="width: 25%; text-align: right;">
                                    <dx:ASPxLabel ID="lblIdDocEf" runat="server" ClientInstanceName="lblIdDocEf" Font-Bold="True" ForeColor="#333333" Height="16px" Text="id" Width="115px">
                                    </dx:ASPxLabel>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 25%; text-align: right;">
                                    <dx:ASPxLabel ID="lblTipoEmisorEf" runat="server" Font-Bold="True" ForeColor="#666666" Height="16px" Text="Entidad Emisora :" Width="110px">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 25%;">
                                    <dx:ASPxComboBox ID="cmbTipoEmisorEf" runat="server" ClientInstanceName="cmbTipoEmisorEf" TextField="DESCRIPCION" TextFormatString="{0}" ValueField="ID_PARAMETRICA_GLOBAL" Theme="Metropolis">
                                        <Columns>
                                            <dx:ListBoxColumn Caption="DESCRIPCION" FieldName="DESCRIPCION" Name="DESCRIPCION" />
                                            <dx:ListBoxColumn Caption="ID_PARAMETRICA_GLOBAL" FieldName="ID_PARAMETRICA_GLOBAL" Name="ID_PARAMETRICA_GLOBAL" Width="0px" />
                                            <dx:ListBoxColumn Caption="CODIGO" FieldName="CODIGO" Name="CODIGO" Width="0px" />
                                        </Columns>
                                        <ClearButton Visibility="Auto">
                                        </ClearButton>
                                        <ValidationSettings>
                                            <RequiredField IsRequired="True" />
                                        </ValidationSettings>
                                        <ReadOnlyStyle BackColor="#FFE1C4">
                                        </ReadOnlyStyle>
                                        <FocusedStyle BackColor="#FFFFCC">
                                        </FocusedStyle>
                                        <DisabledStyle BackColor="#DEE8DB">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                </td>
                                <td style="width: 25%; text-align: right;">
                                    <dx:ASPxLabel ID="lblTipoCredEf" runat="server" Font-Bold="True" ForeColor="#666666" Height="16px" Text="Válido Cred. Fiscal :" Width="120px">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 25%; text-align: right;">
                                    <dx:ASPxComboBox ID="cmbTipoCredEf" runat="server" ClientInstanceName="cmbTipoCredEf" TextField="DESCRIPCION" TextFormatString="{0}" ValueField="ID_PARAMETRICA_GLOBAL" Theme="Metropolis" AllowMouseWheel="False" AnimationType="None" EnableCallbackMode="False" EnableViewState="False">
                                        <Columns>
                                            <dx:ListBoxColumn Caption="DESCRIPCION" FieldName="DESCRIPCION" Name="DESCRIPCION" />
                                            <dx:ListBoxColumn Caption="ID_PARAMETRICA_GLOBAL" FieldName="ID_PARAMETRICA_GLOBAL" Name="ID_PARAMETRICA_GLOBAL" Width="0px" />
                                            <dx:ListBoxColumn Caption="CODIGO" FieldName="CODIGO" Name="CODIGO" Width="0px" />
                                        </Columns>
                                        <ClearButton Visibility="Auto">
                                        </ClearButton>
                                        <ReadOnlyStyle BackColor="#FFE1C4">
                                        </ReadOnlyStyle>
                                        <FocusedStyle BackColor="#FFFFCC">
                                        </FocusedStyle>
                                        <DisabledStyle BackColor="#DEE8DB">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 25%; text-align: right;">
                                    <dx:ASPxLabel ID="lblMonedaEf" runat="server" Font-Bold="True" ForeColor="#666666" Text="Moneda :">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 25%;">
                                    <dx:ASPxComboBox ID="cmbMonedaEf" runat="server" ReadOnly="True" TextField="NOMBRE" TextFormatString="{0}" ValueField="ID_CATALOGO" Theme="Metropolis">
                                        <Columns>
                                            <dx:ListBoxColumn Caption="NOMBRE" FieldName="NOMBRE" Name="NOMBRE" />
                                            <dx:ListBoxColumn Caption="ID_CATALOGO" FieldName="ID_CATALOGO" Name="ID_CATALOGO" Width="0px" />
                                            <dx:ListBoxColumn Caption="CODIGO" FieldName="CODIGO" Name="CODIGO" Width="0px" />
                                        </Columns>
                                        <ClearButton Visibility="Auto">
                                        </ClearButton>
                                        <ValidationSettings>
                                            <RequiredField ErrorText="ERROR: Introduzca la moneda." IsRequired="True" />
                                        </ValidationSettings>
                                        <ReadOnlyStyle BackColor="#FFE1C4">
                                        </ReadOnlyStyle>
                                        <FocusedStyle BackColor="#FFFFCC">
                                        </FocusedStyle>
                                        <DisabledStyle BackColor="#DEE8DB">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                </td>
                                <td style="width: 25%; text-align: right;">
                                    <dx:ASPxLabel ID="lblTipoConsEf" runat="server" Font-Bold="True" ForeColor="#666666" Text="Forma de Pago :" Width="115px">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 25%; text-align: right;">
                                    <dx:ASPxComboBox ID="cmbTipoPagoEf" runat="server" ClientInstanceName="cmbTipoPagoEf" TextField="DESCRIPCION" TextFormatString="{0}" ValueField="ID_PARAMETRICA_GLOBAL" Theme="Metropolis">
                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {

//cmbTipoFlujoEf

//	if(cmbTipoPagoEf.GetValue()==3 || cmbTipoPagoEf.GetValue()==-99999){
//cmbTipoCredEf.SetValue('9');
//	}
//else{
//cmbTipoCredEf.SetValue('10');
//}
}" />
                                        <Columns>
                                            <dx:ListBoxColumn Caption="DESCRIPCION" FieldName="DESCRIPCION" Name="DESCRIPCION" />
                                            <dx:ListBoxColumn Caption="ID_PARAMETRICA_GLOBAL" FieldName="ID_PARAMETRICA_GLOBAL" Name="ID_PARAMETRICA_GLOBAL" Width="0px" />
                                            <dx:ListBoxColumn Caption="CODIGO" FieldName="CODIGO" Name="CODIGO" Width="0px" />
                                        </Columns>
                                        <ClearButton Visibility="Auto">
                                        </ClearButton>
                                        <ValidationSettings>
                                            <RequiredField ErrorText="ERROR : Introduzca la forma de pago." IsRequired="True" />
                                        </ValidationSettings>
                                        <ReadOnlyStyle BackColor="#FFE1C4">
                                        </ReadOnlyStyle>
                                        <FocusedStyle BackColor="#FFFFCC">
                                        </FocusedStyle>
                                        <DisabledStyle BackColor="#DEE8DB">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 25%; text-align: right; padding-bottom: 10px;">
                                    <dx:ASPxLabel ID="lblUsuarioEf" runat="server" Font-Bold="True" ForeColor="#666666" Text="Usuario :">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="padding-bottom: 10px; width: 25%;">
                                    <dx:ASPxTextBox ID="txtUsuarioEf" runat="server" ClientInstanceName="txtId" MaxLength="100">
                                        <ReadOnlyStyle BackColor="#DEE8DB">
                                        </ReadOnlyStyle>
                                        <FocusedStyle BackColor="#FFFFCC">
                                        </FocusedStyle>
                                        <DisabledStyle BackColor="#DEE8DB">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                </td>
                                <td style="width: 25%; text-align: right; padding-bottom: 10px;">&nbsp;</td>
                                <td style="width: 25%; padding-bottom: 10px;">
                                    <dx:ASPxTextBox ID="txtCodigoAux" runat="server" ClientInstanceName="txtId" MaxLength="100">
                                        <ReadOnlyStyle BackColor="#DEE8DB">
                                        </ReadOnlyStyle>
                                        <FocusedStyle BackColor="#FFFFCC">
                                        </FocusedStyle>
                                        <DisabledStyle BackColor="#DEE8DB">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 25%; text-align: right;">
                                    <dx:ASPxLabel ID="lblNoAutoEf" runat="server" Font-Bold="True" ForeColor="#666666" Text="No. Autorización :" Width="100%" ClientInstanceName="lblNoAutoEf">
                                    </dx:ASPxLabel>
                                    <dx:ASPxLabel ID="lblNoDocRefEf" runat="server" ClientInstanceName="lblNoDocRefEf" Font-Bold="True" ForeColor="#666666" Text="No. Doc. Referencia :" Width="130px">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 25%;">
                                    <dx:ASPxTextBox ID="txtNoDocRefEf" runat="server" ClientInstanceName="txtNoDocRefEf" MaxLength="15" Width="170px">
                                        <ReadOnlyStyle BackColor="#DEE8DB">
                                        </ReadOnlyStyle>
                                        <FocusedStyle BackColor="#FFFFCC">
                                        </FocusedStyle>
                                        <DisabledStyle BackColor="#DEE8DB">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <dx:ASPxTextBox ID="txtNoAutoEf" runat="server" ClientInstanceName="txtNoAutoEf" MaxLength="15" Width="170px" ForeColor="#666666">
                                        <ReadOnlyStyle BackColor="#DEE8DB">
                                        </ReadOnlyStyle>
                                        <FocusedStyle BackColor="#FFFFCC">
                                        </FocusedStyle>
                                        <DisabledStyle BackColor="#DEE8DB">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                </td>
                                <td style="width: 25%; text-align: right;">
                                    <dx:ASPxLabel ID="lblNoFactEf" runat="server" Font-Bold="True" ForeColor="#666666" Text="No. Factura :" ClientInstanceName="lblNoFactEf">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 25%">
                                    <dx:ASPxTextBox ID="txtNoFactEf" runat="server" MaxLength="15" Text="0" Width="170px" ClientInstanceName="txtNoFactEf">
                                        <MaskSettings Mask="&lt;0..999999999999999&gt;" />
                                        <ReadOnlyStyle BackColor="#DEE8DB">
                                        </ReadOnlyStyle>
                                        <FocusedStyle BackColor="#FFFFCC">
                                        </FocusedStyle>
                                        <DisabledStyle BackColor="#DEE8DB">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 25%; text-align: right;">
                                    <dx:ASPxLabel ID="lblCodCtrlEf" runat="server" ClientInstanceName="lblCodCtrlEf" Font-Bold="True" ForeColor="#666666" Text="Cod. Control :">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 25%;">
                                    <dx:ASPxTextBox ID="txtCodCtrlEf" runat="server" ClientInstanceName="txtCodCtrlEf" MaxLength="14" Width="170px">
                                        <ClientSideEvents GotFocus="function(s, e) {


try{
str = s.GetValue().split('-').join(''); 
s.SetValue(str);
}
catch(e){
}


}

"
                                            KeyPress="function(s, e) {

if (!((event.keyCode &gt;= 48 &amp;&amp; event.keyCode &lt;= 57)||(event.keyCode &gt;= 65 &amp;&amp; event.keyCode &lt;= 70)||(event.keyCode &gt;= 97 &amp;&amp; event.keyCode &lt;= 102)) ) event.returnValue = false;

}"
                                            LostFocus="function(s, e) {
s.SetValue(s.GetValue().toUpperCase());

if(s.GetValue().length%2==1)
{
s.SetValue(s.GetValue()+'0');
}

y= s.GetValue();
var x='';
while(y.length&gt;0)
{&nbsp;
z=y.substr(0,2);
x=x+z;
y=y.substr(2,y.length);

if(y.length&gt;0)
{
x=x+'-';
}
} 

s.SetValue(x);

}" />
                                        <ValidationSettings SetFocusOnError="True">
                                        </ValidationSettings>
                                        <ReadOnlyStyle BackColor="#DEE8DB">
                                        </ReadOnlyStyle>
                                        <FocusedStyle BackColor="#FFFFCC">
                                        </FocusedStyle>
                                        <DisabledStyle BackColor="#DEE8DB">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                </td>
                                <td style="width: 25%; text-align: right;">&nbsp;</td>
                                <td style="width: 25%;">&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 25%; text-align: right; padding-bottom: 10px;">
                                    <dx:ASPxLabel ID="lblResponsableEf" runat="server" Font-Bold="True" ForeColor="#666666" Text="Responsable :">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 25%;" style="padding-bottom: 10px;">
                                    <dx:ASPxTextBox ID="txtResponsableEf" runat="server" ClientInstanceName="txtResponsableEf" Height="16px" MaxLength="90">
                                        <ClientSideEvents TextChanged="function(s, e) {
 s.SetText(s.GetText().toUpperCase());
}" />
                                        <ReadOnlyStyle BackColor="#DEE8DB">
                                        </ReadOnlyStyle>
                                        <FocusedStyle BackColor="#FFFFCC">
                                        </FocusedStyle>
                                        <DisabledStyle BackColor="#DEE8DB">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                </td>
                                <td style="width: 25%; text-align: right; padding-bottom: 10px;">
                                    <dx:ASPxLabel ID="lblCiEf" runat="server" Font-Bold="True" ForeColor="#666666" Text="C.I. :">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 25%; padding-bottom: 10px;">
                                    <dx:ASPxTextBox ID="txtCiEf" runat="server" ClientInstanceName="txtCiEf" MaxLength="15" Text="0" Width="170px">
                                        <MaskSettings Mask="&lt;0..999999999999999&gt;" />
                                        <ReadOnlyStyle BackColor="#DEE8DB">
                                        </ReadOnlyStyle>
                                        <FocusedStyle BackColor="#FFFFCC">
                                        </FocusedStyle>
                                        <DisabledStyle BackColor="#DEE8DB">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 25%; text-align: right;">
                                    <dx:ASPxLabel ID="lblPrecioEf" runat="server" Font-Bold="True" ForeColor="#666666" Text="Precio :" ClientInstanceName="lblPrecioEf">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 25%;">
                                    <dx:ASPxTextBox ID="txtPrecioEf" runat="server" ClientInstanceName="txtPrecioEf" Text="0" Width="170px">
                                        <ClientSideEvents LostFocus="function(s, e) {
if(parseFloat(txtImporteEf.GetText(), 10)==0 )
{
var num1 = parseFloat(txtPrecioEf.GetText(), 10);
var num2 = parseFloat(txtCantidadDocEf.GetText(), 10);
txtImporteEf.SetText((num1*num2).toFixed(2));
}
}" />
                                        <MaskSettings IncludeLiterals="DecimalSymbol" Mask="&lt;0..999&gt;.&lt;00..99&gt;" PromptChar=" " />
                                        <ReadOnlyStyle BackColor="#DEE8DB">
                                        </ReadOnlyStyle>
                                        <FocusedStyle BackColor="#FFFFCC">
                                        </FocusedStyle>
                                        <DisabledStyle BackColor="#DEE8DB">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                </td>
                                <td style="width: 25%; text-align: right;">&nbsp;</td>
                                <td style="width: 25%">&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 25%; text-align: right;">
                                    <dx:ASPxLabel ID="lblCantidadDocEf" runat="server" Font-Bold="True" ForeColor="#666666" Text="Cantidad :" ClientInstanceName="lblCantidadDocEf">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 25%;">
                                    <dx:ASPxTextBox ID="txtCantidadDocEf" runat="server" ClientInstanceName="txtCantidadDocEf" ReadOnly="True" Text="0.00" Width="170px">
                                        <ClientSideEvents LostFocus="function(s, e) {
if(parseFloat(txtImporteEf.GetText(), 10)==0 )
{
var num1 = parseFloat(txtPrecioEf.GetText(), 10);
var num2 = parseFloat(txtCantidadEf.GetText(), 10);
txtImporteEf.SetText((num1*num2).toFixed(2));
}
}" />
                                        <MaskSettings IncludeLiterals="DecimalSymbol" Mask="&lt;0..9999999&gt;.&lt;00..99&gt;" PromptChar=" " />
                                        <ReadOnlyStyle BackColor="#FFE1C4">
                                        </ReadOnlyStyle>
                                        <FocusedStyle BackColor="#FFFFCC">
                                        </FocusedStyle>
                                        <DisabledStyle BackColor="#DEE8DB">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                </td>
                                <td style="width: 25%; text-align: right;">&nbsp;</td>
                                <td style="width: 25%">&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="text-align: right; padding-bottom: 10px;">
                                    <dx:ASPxLabel ID="lblImporteEf" runat="server" Font-Bold="True" ForeColor="#666666" Text="Importe :" ClientInstanceName="lblImporteEf">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="padding-bottom: 10px;" style="width: 25%;">
                                    <dx:ASPxTextBox ID="txtImporteEf" runat="server" ClientInstanceName="txtImporteEf" Text="0" Width="170px">
                                        <MaskSettings IncludeLiterals="DecimalSymbol" Mask="&lt;0..9999999&gt;.&lt;00..99&gt;" />
                                        <ReadOnlyStyle BackColor="#DEE8DB">
                                        </ReadOnlyStyle>
                                        <FocusedStyle BackColor="#FFFFCC">
                                        </FocusedStyle>
                                        <DisabledStyle BackColor="#DEE8DB">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                </td>
                                <td style="text-align: right; padding-bottom: 10px;"></td>
                                <td style="padding-bottom: 10px;"></td>
                            </tr>
                            <tr>
                                <td style="width: 25%; text-align: right; padding-bottom: 10px;">
                                    <dx:ASPxLabel ID="lblObsDocEf" runat="server" Font-Bold="True" ForeColor="#666666" Text="Observaciones :" Width="150px">
                                    </dx:ASPxLabel>
                                </td>
                                <td colspan="3" style="padding-bottom: 10px;">
                                    <dx:ASPxTextBox ID="txtObsDocEf" runat="server" MaxLength="15" Width="100%" ClientInstanceName="txtObsDocEf">
                                        <ReadOnlyStyle BackColor="#DEE8DB">
                                        </ReadOnlyStyle>
                                        <FocusedStyle BackColor="#FFFFCC">
                                        </FocusedStyle>
                                        <DisabledStyle BackColor="#DEE8DB">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 25%; text-align: right;">&nbsp;</td>
                                <td style="width: 25%;">
                                    <dx:ASPxTextBox ID="txtIdDocEf" runat="server" ClientInstanceName="txtIdDocEf" Text="0" Width="170px" Font-Overline="False" Font-Size="XX-Small" ForeColor="White">
                                        <MaskSettings Mask="&lt;0..999999999999999&gt;" />
                                        <ReadOnlyStyle ForeColor="White">
                                        </ReadOnlyStyle>
                                        <FocusedStyle BackColor="White">
                                        </FocusedStyle>
                                        <Border BorderStyle="None" />
                                        <BorderBottom BorderStyle="None" />
                                    </dx:ASPxTextBox>
                                </td>
                                <td style="width: 25%; text-align: right;">
                                    <asp:TextBox ID="txtIdPadre" runat="server" BorderStyle="None" ForeColor="White" Style="margin-top: 0px"></asp:TextBox>
                                </td>
                                <td style="width: 25%">
                                    <dx:ASPxButton ID="btnAgregaDoc" runat="server" AutoPostBack="False" Text="Agregar Documento" Theme="Metropolis" EnableTheming="True" Width="100%" Font-Bold="True" ForeColor="Maroon">
                                        <ClientSideEvents Click="function(s, e) {
dgvDocumentos.PerformCallback();
pucDocumento.Hide();
}" />
                                        <Image IconID="reports_addgroupheader_16x16">
                                        </Image>
                                    </dx:ASPxButton>
                                </td>
                            </tr>
                        </table>
                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxPanel>
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>




<dx:ASPxPopupControl ID="pucExportacion" runat="server" AllowDragging="True" ClientInstanceName="pucExportacion" EnableTheming="True" HeaderText="Exportación en Formato CSV dede Movimientos" Height="93px" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Theme="Metropolis" Width="420px" OnWindowCallback="pucExportacion_WindowCallback">
    <HeaderImage IconID="navigation_up_16x16">
    </HeaderImage>
    <HeaderStyle>
        <BorderBottom BorderStyle="Solid" />
    </HeaderStyle>
    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl3" runat="server">
            <table class="dxflInternalEditorTable_Youthful">
                <tr>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel339" runat="server" Font-Bold="True" ForeColor="#333333" Text="Fechas :">
                        </dx:ASPxLabel>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <dx:ASPxLabel ID="lblPrecioEf0" runat="server" ClientInstanceName="lblPrecioEf0" Font-Bold="True" ForeColor="#666666" Text="Del :">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxDateEdit ID="detFechaExpIni" runat="server" ClientInstanceName="detFechaExpIni" EnableTheming="True" Height="23px" Theme="Metropolis" ToolTip="Seleccione la fecha inicial" Width="170px">
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
                    <td style="text-align: right">
                        <dx:ASPxLabel ID="lblPrecioEf1" runat="server" ClientInstanceName="lblPrecioEf1" Font-Bold="True" ForeColor="#666666" Text="Al :">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxDateEdit ID="detFechaExpFin" runat="server" ClientInstanceName="detFechaExpFin" EnableTheming="True" Height="23px" Theme="Metropolis" ToolTip="Seleccione la fecha inicial" Width="170px">
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
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel340" runat="server" Font-Bold="True" ForeColor="#333333" Text="Tipo de Operación :">
                        </dx:ASPxLabel>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2">
                        <dx:ASPxComboBox ID="cmbTipoOperacionExp" runat="server" ClientInstanceName="cmbTipoOperacionExp" TextField="NOMBRE" TextFormatString="{0}; {2}" Theme="Metropolis" ValueField="ID_TIPO_OPERACION" Width="100%">
                            <Columns>
                                <dx:ListBoxColumn Caption="NOMBRE" FieldName="NOMBRE" Name="NOMBRE" />
                                <dx:ListBoxColumn Caption="ID_TIPO_OPERACION" FieldName="ID_TIPO_OPERACION" Name="ID_TIPO_OPERACION" Width="0px" />
                                <dx:ListBoxColumn Caption="CODIGO" FieldName="CODIGO" Name="CODIGO" Width="0px" />
                                <dx:ListBoxColumn Caption="ORDEN" FieldName="ORDEN" Name="ORDEN" Width="0px" />
                                <dx:ListBoxColumn Caption="LIMITE_MINIMO" FieldName="LIMITE_MINIMO" Name="LIMITE_MINIMO" Width="0px" />
                                <dx:ListBoxColumn Caption="LIMITE_MAXIMO" FieldName="LIMITE_MAXIMO" Name="LIMITE_MAXIMO" Width="0px" />
                            </Columns>
                            <ClearButton Visibility="Auto">
                            </ClearButton>
                            <ReadOnlyStyle BackColor="#DEE8DB">
                            </ReadOnlyStyle>
                            <DisabledStyle ForeColor="Black">
                            </DisabledStyle>
                        </dx:ASPxComboBox>
                </tr>
                <tr>
                    <td colspan="2">
                        <dx:ASPxButton ID="btnExportaMov" runat="server" Font-Bold="True" ForeColor="Maroon" OnClick="btnExportaMov_Click" Text="Exportar Movimientos" Theme="Metropolis" Width="100%">
                            <Image IconID="actions_apply_16x16">
                            </Image>
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>




<dx:ASPxPopupControl ID="pucImportaRec" runat="server" AllowDragging="True" ClientInstanceName="pucImportaRec" EnableTheming="True" HeaderText="Importación de Recepciones" Height="93px" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Theme="Metropolis" Width="420px" OnWindowCallback="pucImportaRec_WindowCallback">
    <HeaderImage IconID="navigation_up_16x16">
    </HeaderImage>
    <HeaderStyle>
        <BorderBottom BorderStyle="Solid" />
    </HeaderStyle>
    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl4" runat="server">
            <table class="dxflInternalEditorTable_Youthful">
                <tr>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel341" runat="server" Font-Bold="True" ForeColor="#333333" Text="Fechas :">
                        </dx:ASPxLabel>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <dx:ASPxLabel ID="lblPrecioEf2" runat="server" ClientInstanceName="lblPrecioEf2" Font-Bold="True" ForeColor="#666666" Text="Del :">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxDateEdit ID="detFechaImpIni" runat="server" ClientInstanceName="detFechaImpIni" EnableTheming="True" Height="23px" Theme="Metropolis" ToolTip="Seleccione la fecha inicial" Width="170px">
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
                    <td style="text-align: right">
                        <dx:ASPxLabel ID="lblPrecioEf3" runat="server" ClientInstanceName="lblPrecioEf3" Font-Bold="True" ForeColor="#666666" Text="Al :">
                        </dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxDateEdit ID="detFechaImpFin" runat="server" ClientInstanceName="detFechaImpFin" EnableTheming="True" Height="23px" Theme="Metropolis" ToolTip="Seleccione la fecha inicial" Width="170px">
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
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2">
                        <dx:ASPxButton ID="btnImportaRec" runat="server" Font-Bold="True" ForeColor="Maroon" OnClick="btnImportaRec_Click" Text="Importar Recepciones del DCODificador" Theme="Metropolis" Width="100%">
                            <Image IconID="actions_apply_16x16">
                            </Image>
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>

<dx:ASPxPopupControl ID="pucDetalleImportacion" runat="server" CloseAction="CloseButton" CloseOnEscape="True" Modal="True"
    PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Above" ClientInstanceName="pucDetalleImportacion"
    HeaderText="Detalle de Movimientos Importación" AllowDragging="True" PopupAnimationType="None" EnableViewState="False" Maximized="True" ShowMaximizeButton="True" Theme="Metropolis" ScrollBars="Auto" AllowResize="True">
    <HeaderImage IconID="grid_horizontalgridlinesmajorminor_32x32">
    </HeaderImage>

    <HeaderStyle>
        <BorderBottom BorderStyle="Solid" />
    </HeaderStyle>
    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl5" runat="server">
            <dx:ASPxCallbackPanel ID="pnlTransaccionImportacion" runat="server" DefaultButton="btOK" ClientInstanceName="pnlTransaccionImportacion" OnCallback="pnlTransaccionImportacion_Callback">
                <ClientSideEvents BeginCallback="function(s, e) {
	LoadingPanel.Show();
}"
                    EndCallback="function(s, e) {
	LoadingPanel.Hide();
}" />
                <PanelCollection>
                    <dx:PanelContent ID="PanelContent9" runat="server">
                        <table class="dxflInternalEditorTable_Youthful">
                            <tr>
                                <td style="width: 10%; padding-bottom: 10px;">
                                    <dx:ASPxLabel ID="lblTipoOperacionImp" runat="server" ClientInstanceName="lblTipoOperacionImp" Font-Bold="True" ForeColor="#666666" Text="Tipo de Operación :" Width="100%">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 60%; padding-bottom: 10px;">
                                    <dx:ASPxComboBox ID="cmbTipoOperacionImp" runat="server" ClientEnabled="False" ClientInstanceName="cmbTipoOperacionImp" TextField="NOMBRE" TextFormatString="{0}" Theme="Metropolis" ValueField="ID_TIPO_OPERACION" Width="250px" BackColor="#FFE1C4" ForeColor="#DEE8DB">
                                        <ClientSideEvents Init="function(s, e) {
	                        s.GetInputElement().readOnly = true;
                        }" />
                                        <Columns>
                                            <dx:ListBoxColumn Caption="NOMBRE" FieldName="NOMBRE" Name="NOMBRE" />
                                            <dx:ListBoxColumn Caption="ID_TIPO_OPERACION" FieldName="ID_TIPO_OPERACION" Name="ID_TIPO_OPERACION" Width="0px" />
                                            <dx:ListBoxColumn Caption="CODIGO" FieldName="CODIGO" Name="CODIGO" Width="0px" />
                                            <dx:ListBoxColumn Caption="ORDEN" FieldName="ORDEN" Name="ORDEN" Width="0px" />
                                            <dx:ListBoxColumn Caption="LIMITE_MINIMO" FieldName="LIMITE_MINIMO" Name="LIMITE_MINIMO" Width="0px" />
                                            <dx:ListBoxColumn Caption="LIMITE_MAXIMO" FieldName="LIMITE_MAXIMO" Name="LIMITE_MAXIMO" Width="0px" />
                                        </Columns>
                                        <ClearButton Visibility="Auto">
                                        </ClearButton>
                                        <ReadOnlyStyle BackColor="#DEE8DB">
                                        </ReadOnlyStyle>
                                        <DisabledStyle ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                </td>
                                <td style="width: 30%; text-align: right"></td>
                            </tr>
                            <tr>
                                <td></td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <dx:ASPxGridView ID="dgvTransaccionesImp" runat="server" AutoGenerateColumns="False" ClientInstanceName="dgvTransaccionesImp" EnableTheming="True" Font-Size="Smaller" KeyFieldName="ID_MOVIMIENTO" OnCustomButtonCallback="dgvTransaccionesImp_CustomButtonCallback" OnCustomCallback="dgvTransaccionesImp_CustomCallback" OnHtmlRowPrepared="dgvTransaccionesImp_HtmlRowPrepared" OnCustomSummaryCalculate="dgvTransaccionesImp_CustomSummaryCalculate" OnHtmlEditFormCreated="dgvTransaccionesImp_HtmlEditFormCreated" OnRowInserting="dgvTransaccionesImp_RowInserting" OnRowUpdating="dgvTransaccionesImp_RowUpdating" OnRowValidating="dgvTransaccionesImp_RowValidating" Theme="Default" Width="100%" OnCustomButtonInitialize="dgvTransaccionesImp_CustomButtonInitialize">
                                        <ClientSideEvents CustomButtonClick="function (s, e) {
                                    switch(e.buttonID) {
                                    &nbsp;&nbsp;&nbsp; case&nbsp; 'btnAnularImp':
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; e.processOnServer = confirm('¿Desea anular el registro?'); 
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; break;
                                    &nbsp;&nbsp;&nbsp; case&nbsp; 'btnActivarImp':
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; e.processOnServer = confirm('¿Desea activar el registro?'); 
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; break;
                                        case&nbsp; 'btnEliminarImp':
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; e.processOnServer = confirm('ATENCION: Si elimina el registro no podrá revertir el proceso. ¿Desea eliminar el registro?'); 
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; break;
                                    } 
                                    }"
                                            BeginCallback="function(s, e) {
	                                    LoadingPanel.Show();
                                    }"
                                            EndCallback="function(s, e) {
	                                    LoadingPanel.Hide();
                                    }" />
                                        <SettingsContextMenu EnableRowMenu="False">
                                        </SettingsContextMenu>

                                        <Templates>
                                            <GroupFooterRow>
                                                <dx:ASPxLabel ID="ASPxLabel38" runat="server" Text="ASPxLabel">
                                                </dx:ASPxLabel>
                                            </GroupFooterRow>


                                            <EditForm>

                                                <table style="width: 100%" border="0">
                                                    <tr>
                                                        <td style="width: 25%; text-align: right; padding-top: 5px">
                                                            <dx:ASPxLabel ID="ASPxLabel2" runat="server" Font-Bold="True" ForeColor="#666666" Text="Fecha DUI :">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 25%">
                                                            <dx:ASPxDateEdit ID="detFechaEfImp" runat="server" EditFormat="DateTime" Font-Bold="False" Theme="Metropolis" Width="260px">
                                                                <TimeSectionProperties>
                                                                    <TimeEditProperties>
                                                                        <ClearButton Visibility="Auto">
                                                                        </ClearButton>
                                                                    </TimeEditProperties>
                                                                </TimeSectionProperties>
                                                                <ClearButton Visibility="Auto">
                                                                </ClearButton>
                                                                <ValidationSettings>
                                                                    <RequiredField ErrorText="ERROR: Introduzca la fecha DUI." IsRequired="True" />
                                                                </ValidationSettings>
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#FFE1C4" ForeColor="Black">
                                                                </DisabledStyle>
                                                            </dx:ASPxDateEdit>
                                                        </td>
                                                        <td style="width: 25%; text-align: right;">
                                                            <dx:ASPxLabel ID="ASPxLabel1" runat="server" Font-Bold="True" ForeColor="#666666" Text="ID :">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 25%">
                                                            <dx:ASPxTextBox ID="txtIdEfImp" runat="server" ClientInstanceName="txtIdEfImp" ReadOnly="True" Text="0" Width="260px">
                                                                <MaskSettings Mask="&lt;0..999999999999999&gt;" />
                                                                <ReadOnlyStyle BackColor="#FFE1C4">
                                                                </ReadOnlyStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 25%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblEstadoEfImp" runat="server" Font-Bold="True" ForeColor="#666666" Text="Estado :">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 25%">
                                                            <dx:ASPxComboBox ID="cmbEstadoEfImp" runat="server" SelectedIndex="0" Theme="Metropolis" ValueType="System.Int32" Width="260px">
                                                                <Items>
                                                                    <dx:ListEditItem Selected="True" Text="VALIDO" Value="11" />
                                                                    <dx:ListEditItem Text="ANULADO" Value="12" />
                                                                </Items>
                                                                <ClearButton Visibility="Auto">
                                                                </ClearButton>
                                                                <ValidationSettings>
                                                                    <RequiredField ErrorText="ERROR: Introduzca el estado." IsRequired="True" />
                                                                </ValidationSettings>
                                                                <ReadOnlyStyle BackColor="#FFE1C4">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#FFE1C4">
                                                                </DisabledStyle>
                                                            </dx:ASPxComboBox>
                                                        </td>
                                                        <td style="width: 25%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblIntegracionEfImp" runat="server" Font-Bold="True" ForeColor="#666666" Text="Id. Integración :">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 25%">
                                                            <dx:ASPxTextBox ID="txtIntegracionEfImp" runat="server" ClientInstanceName="txtIntegracionEfImp" Text="0" Width="260px">
                                                                <MaskSettings Mask="&lt;0..999999999999999&gt;" />
                                                                <ReadOnlyStyle BackColor="#FFE1C4">
                                                                </ReadOnlyStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 25%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblProdEfImp" runat="server" Font-Bold="True" ForeColor="#666666" Text="Producto :" Width="100px">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 25%">
                                                            <dx:ASPxComboBox ID="cmbProdEfImp" runat="server" ClientInstanceName="cmbProdEfImp" TextField="PRODUCTO" TextFormatString="{0}" Theme="Metropolis" ValueField="ID_IDENTIFICADOR" Width="260px">
                                                                <Columns>
                                                                    <dx:ListBoxColumn Caption="PRODUCTO" FieldName="CODIGO_NOMBRE" Name="NOMBRE" />
                                                                    <dx:ListBoxColumn Caption="ID_IDENTIFICADOR" FieldName="ID_IDENTIFICADOR" Name="ID_IDENTIFICADOR" Width="0px" />
                                                                    <dx:ListBoxColumn Caption="ID_SUB_IDENTIFICADOR" FieldName="ID_SUB_IDENTIFICADOR" Name="ID_SUB_IDENTIFICADOR" Width="0px" />
                                                                </Columns>
                                                                <ClearButton Visibility="Auto">
                                                                </ClearButton>
                                                                <ValidationSettings>
                                                                    <RequiredField ErrorText="ERROR: Introduzca el producto." IsRequired="True" />
                                                                </ValidationSettings>
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxComboBox>
                                                        </td>
                                                        <td style="width: 25%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblUnidadEfImp" runat="server" Font-Bold="True" ForeColor="#666666" Text="Unidad de Medida :">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 25%">
                                                            <dx:ASPxComboBox ID="cmbUnidadEfImp" runat="server" ClientInstanceName="cmbUnidadEfImp" ReadOnly="True" TextField="NOMBRE" TextFormatString="{0}" Theme="Metropolis" ValueField="ID_CATALOGO" Width="260px">
                                                                <ClientSideEvents Init="function(s, e) {
	
                                                                                        var item = cmbProdEfImp.GetSelectedItem();
                                                                                        if(item)
                                                                                        {
                                                                                            s.SetValue(item.GetColumnText('ID_SUB_IDENTIFICADOR'));
                                                                                        }

                                                                                        }" />
                                                                <Columns>
                                                                    <dx:ListBoxColumn Caption="NOMBRE" FieldName="NOMBRE" Name="NOMBRE" />
                                                                    <dx:ListBoxColumn Caption="ID_CATALOGO" FieldName="ID_CATALOGO" Name="ID_CATALOGO" Width="0px" />
                                                                    <dx:ListBoxColumn Caption="CODIGO" FieldName="CODIGO" Name="CODIGO" Width="0px" />
                                                                </Columns>
                                                                <ClearButton Visibility="Auto">
                                                                </ClearButton>
                                                                <ReadOnlyStyle BackColor="#FFE1C4">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxComboBox>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td style="width: 25%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblCantidadEfImp" runat="server" Font-Bold="True" ForeColor="#666666" Text="Cantidad:">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 25%">
                                                            <dx:ASPxTextBox ID="txtCantidadEfImp" runat="server" ClientInstanceName="txtCantidadEfImp" Text="0" Width="260px">
                                                                <MaskSettings IncludeLiterals="DecimalSymbol" Mask="<0..999999999>.<000000..999999999>" />
                                                                <%--<MaskSettings IncludeLiterals="DecimalSymbol" Mask="&lt;0..999999999&gt;.&lt;000000..999999&gt;" />--%>
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

                                                        </td>
                                                        <td style="width: 25%; text-align: right;">&nbsp;</td>
                                                        <td style="width: 25%">&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <!--lmqc para importadores-->
                                                        <td style="width: 25%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblDui" runat="server" Font-Bold="True" ForeColor="#666666" Text="DUI/DIM :" Width="150px">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 25%;">
                                                            <dx:ASPxTextBox ID="txtDui" runat="server" Width="260px">
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                        <td style="width: 25%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblMonSolCompensar" runat="server" Font-Bold="True" ForeColor="#666666" Text="Monto solicitado a Compensar :" Width="150px">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 25%;">
                                                            <dx:ASPxTextBox ID="txtMonSolCompensar" runat="server" Width="260px">
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 25%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblXgasolinaBlanca" runat="server" Font-Bold="True" ForeColor="#666666" Text="X Gasolina Blanca (%):" Width="150px">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 25%;">
                                                            <dx:ASPxTextBox ID="txtXgasolinaBlanca" runat="server" Width="260px" ReadOnly="True">
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                        <td style="width: 25%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblXimp" runat="server" Font-Bold="True" ForeColor="#666666" Text="Ximp (%):" Width="150px">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 25%;">
                                                            <dx:ASPxTextBox ID="txtXimp" runat="server" Width="260px" ReadOnly="True">
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 25%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblPreGasolinaBlanca" runat="server" Font-Bold="True" ForeColor="#666666" Text="Precio Gasolina Blanca :" Width="150px">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 25%;">
                                                            <dx:ASPxTextBox ID="txtPreGasolinaBlanca" runat="server" Width="260px" ClientInstanceName="txtPreGasolinaBlanca">
                                                                <ClientSideEvents LostFocus="function(s, e) {
                            //if(parseFloat(txtPreNetoIva.GetText(), 10)==0)
                            //{
                            var num1 = parseFloat(txtPreGasolinaBlanca.GetText(), 10);
                            txtPreNetoIva.SetText((num1*0.87).toFixed(2));
                            //}
                            }" />
                                                                <MaskSettings IncludeLiterals="DecimalSymbol" Mask="&lt;0..999&gt;.&lt;00..99&gt;" PromptChar=" " />
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                        <td style="width: 25%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblPreNetoIva" runat="server" Font-Bold="True" ForeColor="#666666" Text="Precio Neto Iva :" Width="150px">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 25%;">
                                                            <dx:ASPxTextBox ID="txtPreNetoIva" runat="server" Width="260px" ClientInstanceName="txtPreNetoIva" ReadOnly="True">
                                                                <MaskSettings IncludeLiterals="DecimalSymbol" Mask="&lt;0..999&gt;.&lt;00..99&gt;" PromptChar=" " />
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 25%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblTarifaTransPol" runat="server" Font-Bold="True" ForeColor="#666666" Text="Tarifa Transporte :" Width="150px">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 25%;">
                                                            <dx:ASPxTextBox ID="txtTarifaTransPol" runat="server" Width="260px" ClientInstanceName="txtTarifaTransPol">
                                                                <ClientSideEvents LostFocus="function(s, e) {
                            //if(parseFloat(txtTarifaNetoIva.GetText(), 10)==0)
                            //{
                            var num1 = parseFloat(txtTarifaTransPol.GetText(), 10);
                            txtTarifaNetoIva.SetText((num1*0.87).toFixed(2));
                            //}
                            }" />
                                                                <MaskSettings IncludeLiterals="DecimalSymbol" Mask="&lt;0..999&gt;.&lt;00..99&gt;" PromptChar=" " />
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                        <td style="width: 25%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblTarifaNetoIva" runat="server" Font-Bold="True" ForeColor="#666666" Text="Tarifa Neto Iva :" Width="150px">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 25%;">
                                                            <dx:ASPxTextBox ID="txtTarifaNetoIva" runat="server" Width="260px" ClientInstanceName="txtTarifaNetoIva" ReadOnly="True">
                                                                <MaskSettings IncludeLiterals="DecimalSymbol" Mask="&lt;0..999&gt;.&lt;00..99&gt;" PromptChar=" " />
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 25%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblPpt" runat="server" Font-Bold="True" ForeColor="#666666" Text="PPT :" Width="150px">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 25%;">
                                                            <dx:ASPxTextBox ID="txtPpt" runat="server" Width="260px">
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                        <td style="width: 25%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblIncoterms" runat="server" Font-Bold="True" ForeColor="#666666" Text="INCOTERMS :" Width="150px">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 25%;">
                                                            <dx:ASPxTextBox ID="txtIncoterms" runat="server" Width="260px">
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 25%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblFronteraIngreso" runat="server" Font-Bold="True" ForeColor="#666666" Text="Frontera de Ingreso :" Width="150px">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 25%;">
                                                            <dx:ASPxTextBox ID="txtFronteraIngreso" runat="server" Width="260px">
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                        <td style="width: 25%; text-align: left;"></td>
                                                        <td style="width: 25%;"></td>
                                                    </tr>


                                                    <tr>
                                                        <td style="width: 25%; text-align: right;">
                                                            <dx:ASPxTextBox ID="txtJsonRef" runat="server" Width="260px" ClientVisible="False">
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                        <td style="width: 25%;">
                                                            <dx:ASPxTextBox ID="txtEstJsonRef" runat="server" Width="260px" ClientVisible="False">
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                        <td style="width: 25%; text-align: left;">
                                                            <dx:ASPxTextBox ID="txtIdCuenta" runat="server" Width="260px" ClientVisible="False">
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                        <td style="width: 25%;">
                                                            <dx:ASPxTextBox ID="txtIdCuentaAux" runat="server" Width="260px" ClientVisible="False">
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 25%; text-align: right;">
                                                            <dx:ASPxTextBox ID="txtIdParGl" runat="server" Width="260px" ClientVisible="False">
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                        <td style="width: 25%;">
                                                            <dx:ASPxTextBox ID="txtIdParEs" runat="server" Width="260px" ClientVisible="False">
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                        <td style="width: 25%; text-align: left;">
                                                            <dx:ASPxTextBox ID="txtIdInfAux" runat="server" ClientVisible="False" Width="260px">
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                        <td style="width: 25%;">
                                                            <dx:ASPxTextBox ID="txtIdTipoInfAux" runat="server" Width="260px" ClientVisible="False">
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 25%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblObsEfImp" runat="server" Font-Bold="True" ForeColor="#666666" Text="Observaciones :" Width="150px">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td colspan="3">
                                                            <dx:ASPxTextBox ID="txtObsEfImp" runat="server" MaxLength="50" Width="98%">
                                                                <ClientSideEvents KeyPress="function(s, e) {
                                                                                        var code = null; 
                                                                                        code = (e.keyCode? e.keyCode: e.which); 
                                                                                        return (code == 13)? false: verdadero; 
                                                                                        }" />
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                    </tr>

                                                    <%-- <tr> 
                                                                            <td  style="width: 25%; text-align: right;">
                                                                                <asp:Label ID="lbArchivo" runat="server" Text="Archivo :" Font-Bold="True" ForeColor="#666666" Width="150px"></asp:Label>
                                                                            </td>
                                                                            <td colspan="3">
                                                                                <asp:FileUpload ID="fileArchivo" runat="server" />
                                                                            </td>
                                                                            <td></td>
                                                                        </tr> --%>
                                                    <tr>
                                                        <td style="width: 25%; text-align: right;"></td>
                                                        <td style="width: 25%;"></td>
                                                        <td style="width: 25%; text-align: left;">
                                                            <dx:ASPxButton ID="btnAgrega_Import" runat="server" AutoPostBack="False" Font-Bold="True" ForeColor="Maroon" Text="Agregar Documento" Theme="Metropolis" Width="160px">
                                                                <ClientSideEvents Click="function(s, e) {
                                                                                            //txtIdDocEf.SetValue(0);
                                                                                            cmbTipoFacturaImpAdd.SetValue(0);
                                                                                            pudDocumentoImpAdd.PerformCallback();
                                                                                            pudDocumentoImpAdd.Show(); 
                                                                            //pcCreateAccount.Show();
                                                                                                                }" />
                                                                <Image IconID="reports_addgroupheader_16x16">
                                                                </Image>
                                                            </dx:ASPxButton>
                                                        </td>
                                                        <td style="width: 25%;"></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" style="width: 100%;">
                                                            <dx:ASPxGridView ID="dgvDocumentosImp" runat="server" AutoGenerateColumns="False" ClientInstanceName="dgvDocumentosImp" EnableTheming="True" Font-Size="Smaller" KeyFieldName="ID_TRANSACCION" OnCustomButtonCallback="dgvDocumentosImp_CustomButtonCallback" OnCustomCallback="dgvDocumentosImp_CustomCallback" OnHtmlRowPrepared="dgvDocumentosImp_HtmlRowPrepared" Theme="Default" Width="100%">
                                                                <ClientSideEvents CustomButtonClick="function(s, e) {
	
                                switch(e.buttonID) {

                                case&nbsp; 'btnEditarImTran':
                                //txtIdDocEfImpEdt.SetValue(1);
                                pucDocumentoImp.PerformCallback();
                                pucDocumentoImp.Show();                                    
                                break;

                                case&nbsp; 'btnQuitarImTran':
                                e.processOnServer = confirm('¿Desea quitar el documento? - Una vez realizada la acción y despues de grabar el movimiento no podrá revertir el proceso. ¿Desea continuar?'); 
                                break;                                    

                                }

                                }" />
                                                                <SettingsContextMenu EnableRowMenu="False">
                                                                </SettingsContextMenu>
                                                                <Templates>
                                                                    <EditForm>
                                                                    </EditForm>
                                                                </Templates>
                                                                <SettingsPager PageSize="100" Visible="true">
                                                                    <PageSizeItemSettings Items="100,500" Visible="True" ShowAllItem="True">
                                                                    </PageSizeItemSettings>
                                                                </SettingsPager>
                                                                <SettingsEditing Mode="PopupEditForm">
                                                                </SettingsEditing>
                                                                <Settings ShowFooter="True" ShowHeaderFilterButton="True" VerticalScrollBarMode="Visible" />
                                                                <SettingsBehavior AllowFocusedRow="True" />
                                                                <SettingsPopup>
                                                                    <EditForm Modal="True" />
                                                                    <CustomizationWindow HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" />
                                                                </SettingsPopup>
                                                                <SettingsText CommandNew="Nuevo" />
                                                                <Columns>
                                                                    <dx:GridViewCommandColumn ShowInCustomizationForm="True" VisibleIndex="19">
                                                                        <CustomButtons>
                                                                            <dx:GridViewCommandColumnCustomButton ID="btnEditarImTran" Text="Editar">
                                                                            </dx:GridViewCommandColumnCustomButton>
                                                                            <dx:GridViewCommandColumnCustomButton ID="btnQuitarImTran" Text="Quitar">
                                                                            </dx:GridViewCommandColumnCustomButton>
                                                                        </CustomButtons>
                                                                    </dx:GridViewCommandColumn>
                                                                    <dx:GridViewDataTextColumn Caption="NO. FACT./DOC." FieldName="NRO_TRANSACCION" ShowInCustomizationForm="True" VisibleIndex="14">
                                                                        <Settings AutoFilterCondition="Contains" />
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="OBSERVACIONES" FieldName="OBSERVACIONES" ShowInCustomizationForm="True" Visible="False" VisibleIndex="21">
                                                                        <Settings AutoFilterCondition="Contains" />
                                                                        <CellStyle HorizontalAlign="Right">
                                                                        </CellStyle>
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="EMISOR" FieldName="TIPO_EMISOR" ShowInCustomizationForm="True" Visible="False" VisibleIndex="6">
                                                                        <Settings AutoFilterCondition="Contains" />
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="TIPO DOC." FieldName="TIPO_TRANSACCION" ShowInCustomizationForm="True" Visible="False" VisibleIndex="4">
                                                                        <Settings AutoFilterCondition="Equals" />
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="NO. AUTORIZACION" FieldName="NRO_AUTORIZACION" ShowInCustomizationForm="False" Visible="False" VisibleIndex="13">
                                                                        <Settings AutoFilterCondition="Contains" />
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="PRECIO" FieldName="PRECIO_UNITARIO" ShowInCustomizationForm="True" VisibleIndex="17">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="IMPORTE" FieldName="PRECIO_TOTAL" ShowInCustomizationForm="True" VisibleIndex="18">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="FECHA" FieldName="FECHA_HORA" ShowInCustomizationForm="True" VisibleIndex="2">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="CANTIDAD" FieldName="CANTIDAD" Visible="False" VisibleIndex="16">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="ID" FieldName="ID_TRANSACCION" VisibleIndex="0">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="USUARIO_TRANSACCION" FieldName="USUARIO_TRANSACCION" Visible="False" VisibleIndex="20">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="COD. CTRL." FieldName="CODIGO_CONTROL" Visible="False" VisibleIndex="15">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="ID_MOVIMIENTO" FieldName="ID_MOVIMIENTO" Visible="False" VisibleIndex="22">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="ID_TIPO_MONEDA" FieldName="ID_TIPO_MONEDA" Visible="False" VisibleIndex="23">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="ID_ESTADO_TRANSACCION" FieldName="ID_ESTADO_TRANSACCION" Visible="False" VisibleIndex="24">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="FECHA_HORA_EMISION" FieldName="FECHA_HORA_EMISION" Visible="False" VisibleIndex="1">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="ID_TIPO_TRANSACCION" FieldName="ID_TIPO_TRANSACCION" Visible="False" VisibleIndex="3">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="ID_TIPO_EMISOR" FieldName="ID_TIPO_EMISOR" Visible="False" VisibleIndex="5">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="ID_FORMA_PAGO" FieldName="ID_FORMA_PAGO" Visible="False" VisibleIndex="7">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="FORMA PAGO " FieldName="FORMA_PAGO" Visible="False" VisibleIndex="8">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="ID_TIPO_CREDITO_FISCAL" FieldName="ID_TIPO_CREDITO_FISCAL" Visible="False" VisibleIndex="9">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="TIPO_CREDITO_FISCAL" FieldName="TIPO_CREDITO_FISCAL" Visible="False" VisibleIndex="10">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="NIT_CONSUMIDOR" FieldName="NIT_CONSUMIDOR" Visible="true" VisibleIndex="11">
                                                                    </dx:GridViewDataTextColumn>
                                                                    <dx:GridViewDataTextColumn Caption="RAZON_SOCIAL" FieldName="RAZON_SOCIAL" Visible="true" VisibleIndex="12">
                                                                    </dx:GridViewDataTextColumn>
                                                                </Columns>
                                                            </dx:ASPxGridView>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 25%; text-align: right; padding-top: 10px;">
                                                            <dx:ASPxTextBox ID="txtAdicionalEfImp" runat="server" ForeColor="White" ReadOnly="True" ViewStateMode="Disabled" Width="170px" ClientEnabled="False">
                                                                <Border BorderStyle="None" />
                                                                <BorderBottom BorderStyle="None" />
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                        <td style="width: 25%; padding-top: 10px;">&nbsp;</td>
                                                        <td style="width: 25%; padding-top: 10px; text-align: right;">
                                                            <dx:ASPxButton ID="btnAceptarImp" runat="server" AutoPostBack="False" ClientInstanceName="btnAceptarImp" Text="Grabar Movimiento Importacion" Theme="Metropolis" Width="100%" Font-Bold="True" ForeColor="Maroon">
                                                                <ClientSideEvents Click="function(s, e) {
	                                dgvTransaccionesImp.UpdateEdit();
                                }" />
                                                                <Image IconID="actions_apply_16x16">
                                                                </Image>
                                                            </dx:ASPxButton>
                                                        </td>
                                                        <td style="width: 25%; padding-top: 10px;">
                                                            <dx:ASPxButton ID="btnCancelarImp" runat="server" AutoPostBack="False" ClientInstanceName="btnCancelarImp" Text="Volver" Theme="Metropolis" Width="40%" Font-Bold="True" ForeColor="Maroon">
                                                                <ClientSideEvents Click="function(s, e) {
                                                                                                                dgvTransaccionesImp.CancelEdit();
                                                                                                        }" />
                                                                <Image IconID="actions_reset_16x16">
                                                                </Image>
                                                            </dx:ASPxButton>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" style="text-align: right;">&nbsp;</td>
                                                    </tr>

                                                </table>

                                            </EditForm>


                                        </Templates>

                                        <SettingsEditing Mode="PopupEditForm">
                                        </SettingsEditing>
                                        <Settings ShowFilterRow="True" ShowFooter="True" ShowGroupPanel="True" ShowHeaderFilterButton="True" />
                                        <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" />
                                        <SettingsCommandButton>
                                            <NewButton Text=" ">
                                                <Image IconID="reports_addgroupheader_16x16" ToolTip="Nuevo Movimiento">
                                                </Image>
                                            </NewButton>
                                            <EditButton Text=" ">
                                                <Image IconID="edit_edit_16x16" ToolTip="Editar">
                                                </Image>
                                            </EditButton>
                                        </SettingsCommandButton>
                                        <SettingsPopup>
                                            <EditForm HorizontalAlign="Center" Modal="True" VerticalAlign="Middle" VerticalOffset="0" CloseOnEscape="True" />
                                            <CustomizationWindow HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" />
                                        </SettingsPopup>
                                        <SettingsSearchPanel Visible="True" />
                                        <SettingsText CommandNew="Nuevo" />

                                        <Columns>
                                            <dx:GridViewCommandColumn ShowEditButton="True" ShowInCustomizationForm="True" ShowNewButtonInHeader="True" ToolTip="Funciones" VisibleIndex="47">
                                                <CustomButtons>
                                                    <dx:GridViewCommandColumnCustomButton ID="btnAnularImp" Text=" ">
                                                        <Image IconID="databars_gradientreddatabar_16x16" ToolTip="Anular">
                                                        </Image>
                                                    </dx:GridViewCommandColumnCustomButton>
                                                    <dx:GridViewCommandColumnCustomButton ID="btnActivarImp" Text=" ">
                                                        <Image IconID="databars_solidgreendatabar_16x16" ToolTip="Activar">
                                                        </Image>
                                                    </dx:GridViewCommandColumnCustomButton>
                                                    <dx:GridViewCommandColumnCustomButton ID="btnEliminarImp" Text=" ">
                                                        <Image IconID="reports_deletegroupfooter_16x16office2013" ToolTip="Eliminar">
                                                        </Image>
                                                    </dx:GridViewCommandColumnCustomButton>
                                                </CustomButtons>
                                                <CellStyle ForeColor="#006600">
                                                </CellStyle>
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewDataTextColumn Caption="FECHA_OPERACION" FieldName="FECHA_OPERACION" ShowInCustomizationForm="True" ToolTip="Fecha del Movimiento" Visible="False" VisibleIndex="5">
                                                <Settings AutoFilterCondition="Contains" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="ORIGEN REG." FieldName="COD_FUENTE_ORIGEN" ShowInCustomizationForm="True" ToolTip="Origen del Registro" VisibleIndex="8">
                                                <Settings AutoFilterCondition="Contains" />
                                                <CellStyle HorizontalAlign="Center">
                                                </CellStyle>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="T.MOV." FieldName="TIPO_MOVIMIENTO" ShowInCustomizationForm="True" ToolTip="Tipo de Movimiento" VisibleIndex="9">
                                                <Settings AutoFilterCondition="Contains" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="NIT" FieldName="NIT_CONSUMIDOR" ShowInCustomizationForm="True" VisibleIndex="33">
                                                <Settings AutoFilterCondition="Contains" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="RAZON SOCIAL" FieldName="RAZON_SOCIAL" ShowInCustomizationForm="True" VisibleIndex="34">
                                                <Settings AutoFilterCondition="Contains" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="PLACA" FieldName="IDENTIFICADOR" ShowInCustomizationForm="True" VisibleIndex="21">
                                                <Settings AutoFilterCondition="Contains" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="OBSERVACION_MOVIMIENTO" FieldName="OBSERVACION_MOVIMIENTO" ShowInCustomizationForm="True" Visible="False" VisibleIndex="48">
                                                <Settings AutoFilterCondition="Contains" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="TIPO TRANSACCION" FieldName="TIPO_TRANSACCION" ShowInCustomizationForm="True" VisibleIndex="28">
                                                <Settings AutoFilterCondition="Contains" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="NO. FACT./DOC." FieldName="NRO_TRANSACCION" ShowInCustomizationForm="True" ToolTip="Número de Factura o Documento" VisibleIndex="36">
                                                <Settings AutoFilterCondition="Contains" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="(NRO/COD) AUTORIZACION" FieldName="NRO_AUTORIZACION" ShowInCustomizationForm="True" ToolTip="Número de Autorización" VisibleIndex="35">
                                                <Settings AutoFilterCondition="Contains" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="USUARIO" FieldName="APP_USUARIO" ShowInCustomizationForm="True" ToolTip="Entidad de Referencia" VisibleIndex="22" Visible="False">
                                                <Settings AutoFilterCondition="Contains" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="ID_ESTADO_MOVIMIENTO" FieldName="ID_ESTADO_MOVIMIENTO" ShowInCustomizationForm="True" Visible="False" VisibleIndex="11">
                                                <Settings AutoFilterCondition="Contains" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="ID_HIDROCARBURO" FieldName="ID_HIDROCARBURO" ShowInCustomizationForm="True" Visible="False" VisibleIndex="27">
                                                <Settings AutoFilterCondition="Contains" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="PRECIO" FieldName="PRECIO_UNITARIO" ShowInCustomizationForm="True" VisibleIndex="39">
                                                <Settings AutoFilterCondition="Equals" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="CANTIDAD" FieldName="CANTIDAD" ShowInCustomizationForm="True" VisibleIndex="38">
                                                <Settings AutoFilterCondition="Equals" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="IMPORTE" FieldName="PRECIO_TOTAL" ShowInCustomizationForm="True" VisibleIndex="40">
                                                <Settings AutoFilterCondition="Equals" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="ID" FieldName="ID_MOVIMIENTO" ShowInCustomizationForm="True" ToolTip="Identificador del Registro del Movimiento" VisibleIndex="0" Visible="False">
                                                <Settings AutoFilterCondition="Equals" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="ID_CONSUMIDOR" FieldName="ID_CONSUMIDOR" ShowInCustomizationForm="True" Visible="False" VisibleIndex="2">
                                                <Settings AutoFilterCondition="Equals" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="ID_INFRAESTRUCTURA" FieldName="ID_INFRAESTRUCTURA" ShowInCustomizationForm="True" Visible="False" VisibleIndex="25">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="ID_TIPO_TRANSACCION" FieldName="ID_TIPO_TRANSACCION" ShowInCustomizationForm="True" Visible="False" VisibleIndex="32">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="ID_UNIDAD_ENVASES" FieldName="ID_UNIDAD_ENVASES" ShowInCustomizationForm="True" Visible="False" VisibleIndex="45">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="GRADO_API" FieldName="GRADO_API" ShowInCustomizationForm="True" Visible="False" VisibleIndex="43">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="TEMPERATURA" FieldName="TEMPERATURA" ShowInCustomizationForm="True" Visible="False" VisibleIndex="41">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="ID_TIPO_FLUJO" FieldName="ID_TIPO_FLUJO" ShowInCustomizationForm="True" Visible="False" VisibleIndex="13">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="ID_TIPO_OPERACION" FieldName="ID_TIPO_OPERACION" ShowInCustomizationForm="True" Visible="False" VisibleIndex="4">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="ID_TIPO_MEDIO_TRANSPORTE" FieldName="ID_TIPO_MEDIO_TRANSPORTE" ShowInCustomizationForm="True" Visible="False" VisibleIndex="19">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="ID_MEDIO_TRANSPORTE" FieldName="ID_MEDIO_TRANSPORTE" ShowInCustomizationForm="True" Visible="False" VisibleIndex="24">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="ID_TIPO_MEDICION" FieldName="ID_TIPO_MEDICION" ShowInCustomizationForm="True" Visible="False" VisibleIndex="18">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="ID_UNIDAD_TEMPERATURA" FieldName="ID_UNIDAD_TEMPERATURA" ShowInCustomizationForm="True" Visible="False" VisibleIndex="42">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="CANTIDAD_ENVASES" FieldName="CANTIDAD_ENVASES" ShowInCustomizationForm="True" Visible="False" VisibleIndex="44">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="APP_FECHA_REGISTRO" FieldName="APP_FECHA_REGISTRO" ShowInCustomizationForm="True" Visible="False" VisibleIndex="50">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="ID_ENTIDAD" FieldName="ID_ENTIDAD" ShowInCustomizationForm="True" Visible="False" VisibleIndex="17">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="ID_TIPO_MOVIMIENTO" FieldName="ID_TIPO_MOVIMIENTO" ShowInCustomizationForm="True" Visible="False" VisibleIndex="14">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="SUB_IDENTIFICADOR" FieldName="SUB_IDENTIFICADOR" ShowInCustomizationForm="True" Visible="False" VisibleIndex="31">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="PAIS" FieldName="UBICACION_OPERACION" ShowInCustomizationForm="True" VisibleIndex="10">
                                                <Settings AutoFilterCondition="Contains" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="EST." FieldName="ESTADO_MOVIMIENTO" ShowInCustomizationForm="True" ToolTip="Estado del Movimiento" VisibleIndex="12">
                                                <Settings HeaderFilterMode="CheckedList" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="PRODUCTO" FieldName="COD_HIDROCARBURO" ShowInCustomizationForm="True" VisibleIndex="37">
                                                <Settings AutoFilterCondition="Contains" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="T.FLUJO" FieldName="TIPO_FLUJO" ShowInCustomizationForm="True" ToolTip="Tipo de Flujo" VisibleIndex="7">
                                                <Settings AutoFilterCondition="Contains" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="ID_MOVIMIENTO_PADRE" FieldName="ID_MOVIMIENTO_PADRE" ShowInCustomizationForm="True" Visible="False" VisibleIndex="1">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="BINARIO_CONTROL" FieldName="BINARIO_CONTROL" ShowInCustomizationForm="True" Visible="False" VisibleIndex="49">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="ID_FUENTE_ORIGEN" FieldName="ID_FUENTE_ORIGEN" ShowInCustomizationForm="True" Visible="False" VisibleIndex="15">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="FECHA_OPERACION_DIA" FieldName="FECHA_OPERACION_DIA" ShowInCustomizationForm="True" Visible="False" VisibleIndex="51">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="ID_UBICACION_OPERACION" FieldName="ID_UBICACION_OPERACION" ShowInCustomizationForm="True" Visible="False" VisibleIndex="26">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="ID_UNIDAD_HIDROCARBURO" FieldName="ID_UNIDAD_HIDROCARBURO" ShowInCustomizationForm="True" Visible="False" VisibleIndex="30">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="DES_FUENTE_ORIGEN" FieldName="DES_FUENTE_ORIGEN" ShowInCustomizationForm="True" Visible="False" VisibleIndex="16">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="TIPO_MEDIO_TRANSAPORTE" FieldName="TIPO_MEDIO_TRANSAPORTE" ShowInCustomizationForm="True" Visible="False" VisibleIndex="20">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="DES_HIDROCARBURO " FieldName="DES_HIDROCARBURO " ShowInCustomizationForm="True" Visible="False" VisibleIndex="29">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="ID_TIPO_ACTIVIDAD" FieldName="ID_TIPO_ACTIVIDAD" ShowInCustomizationForm="True" Visible="False" VisibleIndex="3">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="CANTIDAD_ADICIONAL" FieldName="CANTIDAD_ADICIONAL" ShowInCustomizationForm="True" Visible="False" VisibleIndex="46">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="EST.D.J." FieldName="STR_ESTADO_DEC" ShowInCustomizationForm="True" ToolTip="Estado de Declaracion Jurada" VisibleIndex="23" Visible="False">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="FECHA" FieldName="STR_FECHA" ShowInCustomizationForm="True" VisibleIndex="6">
                                                <Settings AutoFilterCondition="Contains" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="CODIGO" FieldName="CODIGO" ShowInCustomizationForm="True" Visible="False" VisibleIndex="52">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="ID_INTEGRACION" FieldName="ID_INTEGRACION" ShowInCustomizationForm="True" Visible="False" VisibleIndex="53">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="BARCODE" FieldName="BARCODE" ShowInCustomizationForm="True" Visible="False" VisibleIndex="54">
                                            </dx:GridViewDataTextColumn>
                                        </Columns>
                                        <TotalSummary>
                                            <dx:ASPxSummaryItem FieldName="PRECIO_TOTAL" ShowInColumn="IMPORTE" SummaryType="Custom" Tag="imp" />
                                            <dx:ASPxSummaryItem FieldName="CANTIDAD" ShowInColumn="CANTIDAD" SummaryType="Custom" Tag="vol" />
                                            <dx:ASPxSummaryItem FieldName="ESTADO_MOVIMIENTO" ShowInColumn="EST." SummaryType="Custom" Tag="est" />
                                        </TotalSummary>
                                    </dx:ASPxGridView>
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="3"></td>
                            </tr>

                        </table>
                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxCallbackPanel>
        </dx:PopupControlContentControl>
    </ContentCollection>
    <ClientSideEvents Closing="function(s, e) {
	pnlDatos.PerformCallback();
}" />
    <ContentStyle>
        <Paddings PaddingBottom="5px" />
    </ContentStyle>
</dx:ASPxPopupControl>


<dx:ASPxPopupControl ID="pucDocumentoImp" runat="server" CloseAction="CloseButton" CloseOnEscape="true" Maximized="True"
    PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Above" ClientInstanceName="pucDocumentoImp"
    HeaderText="Documentos de Importación" AllowDragging="True" Modal="True" PopupAnimationType="None" AllowResize="true"
    EnableViewState="False" OnWindowCallback="pucDocumentoImp_WindowCallback" Theme="Metropolis" Style="margin-right: 1px">
    <ClientSideEvents PopUp="function(s, e) { ASPxClientEdit.ClearGroup('createAccountGroup'); }" />
    <HeaderImage IconID="actions_insert_32x32">
    </HeaderImage>
    <SizeGripImage Width="11px" />
    <HeaderStyle>
        <BorderBottom BorderStyle="Solid" />
    </HeaderStyle>
    <ContentCollection>

        <dx:PopupControlContentControl ID="PopupControlContentControl6" runat="server">
            <dx:ASPxPanel ID="ASPxPanel1" runat="server" DefaultButton="btCreate">
                <PanelCollection>
                    <dx:PanelContent ID="PanelContent10" runat="server">
                        <table class="dxflInternalEditorTable_Youthful" style="width: 100%">
                            <tr>
                                <!--lmqc importacion-->
                                <td style="width: 25%; text-align: right;">
                                    <dx:ASPxLabel ID="ASPxLabel4" runat="server" Font-Bold="True" ForeColor="#666666" Text="Tipo Factura :">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxComboBox ID="cmbTipoFacturaImpEdt" runat="server" ClientInstanceName="cmbTipoFacturaImpEdt" TextField="DESCRIPCION" TextFormatString="{0}" Theme="Metropolis" ValueField="ID_PARAMETRICA_GLOBAL" Width="100%" Enabled="false">
                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
	pnlFacturasImpEdt.PerformCallback();
}" />
                                        <Columns>
                                            <dx:ListBoxColumn Caption="DESCRIPCION" FieldName="DESCRIPCION" Name="DESCRIPCION" />
                                            <dx:ListBoxColumn Caption="ID_PARAMETRICA_GLOBAL" FieldName="ID_PARAMETRICA_GLOBAL" Name="ID_PARAMETRICA_GLOBAL" Width="0px" />
                                            <dx:ListBoxColumn Caption="CODIGO" FieldName="CODIGO" Name="CODIGO" Width="0px" />
                                        </Columns>
                                        <ClearButton Visibility="Auto">
                                        </ClearButton>
                                        <ValidationSettings>
                                            <RequiredField ErrorText="ERROR : Introduzca tipo de factura de importacion." IsRequired="True" />
                                        </ValidationSettings>
                                        <ReadOnlyStyle BackColor="#DEE8DB">
                                        </ReadOnlyStyle>
                                        <FocusedStyle BackColor="#FFFFCC">
                                        </FocusedStyle>
                                        <DisabledStyle BackColor="#DEE8DB">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                </td>
                            </tr>
                            <!--lmqc importacion-->
                        </table>

                        <dx:ASPxCallbackPanel ID="pnlFacturasImpEdt" runat="server" Width="100%" OnCallback="pnlFacturasImpEdt_Callback" ClientInstanceName="pnlFacturasImpEdt">
                            <PanelCollection>
                                <dx:PanelContent ID="PanelContent11" runat="server">
                                    <table style="width: 100%">
                                        <tr>
                                            <!--lmqc para facturas de importacion-->
                                            <td style="width: 10%; text-align: right;">
                                                <dx:ASPxLabel ID="lblFactProveedorImpEdt" runat="server" Font-Bold="True" Text="Nro Factura Proveedor :" Width="90%">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td style="width: 30%;">
                                                <dx:ASPxTextBox ID="txtFactProveedorImpEdt" ClientInstanceName="txtFactProveedorImpEdt" runat="server" Width="90%">
                                                </dx:ASPxTextBox>
                                            </td>
                                            <td style="width: 25%; text-align: right;">
                                                <dx:ASPxLabel ID="lblProveedorImpEdt" runat="server" Font-Bold="True" Text="Proveedor :" Width="90%">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td style="width: 35%;">
                                                <dx:ASPxTextBox ID="txtProveedorImpEdt" ClientInstanceName="txtProveedorImpEdt" runat="server" Width="90%">
                                                </dx:ASPxTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <!--lmqc para facturas de importacion-->
                                            <td style="width: 10%; text-align: right;">
                                                <dx:ASPxLabel ID="lblFactDuiImpEdt" runat="server" Font-Bold="True" Text="DUI :" Width="90%">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td style="width: 30%;">
                                                <dx:ASPxTextBox ID="txtFactDuiImpEdt" ClientInstanceName="txtFactDuiImpEdt" runat="server" Width="90%">
                                                </dx:ASPxTextBox>
                                            </td>
                                            <td style="width: 25%; text-align: right;">
                                                <dx:ASPxLabel ID="lblSecFactImpEdt" runat="server" Font-Bold="True" Text="Sec. Factura :" Width="90%">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td style="width: 35%;">
                                                <dx:ASPxTextBox ID="txtSecFactImpEdt" ClientInstanceName="txtSecFactImpEdt" runat="server" Width="90%">
                                                    <MaskSettings Mask="&lt;0..999999999999999&gt;" PromptChar=" " />
                                                </dx:ASPxTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10%; text-align: right;">
                                                <dx:ASPxLabel ID="lblFechaDocumento" runat="server" Font-Bold="True" Text="Fecha Documento:" Width="90%">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td style="width: 30%;">
                                                <dx:ASPxDateEdit ID="detFechaDocEfImpEdt" ClientInstanceName="detFechaDocEfImpEdt" runat="server" EditFormat="DateTime" Font-Bold="False" Theme="Metropolis">
                                                    <TimeSectionProperties>
                                                        <TimeEditProperties>
                                                            <ClearButton Visibility="Auto">
                                                            </ClearButton>
                                                        </TimeEditProperties>
                                                    </TimeSectionProperties>
                                                    <ClearButton Visibility="Auto">
                                                    </ClearButton>
                                                    <ValidationSettings>
                                                        <RequiredField ErrorText="ERROR: Introduzca la fecha." />
                                                    </ValidationSettings>
                                                </dx:ASPxDateEdit>
                                            </td>
                                            <td style="width: 25%; text-align: right;">
                                                <dx:ASPxTextBox runat="server" ID="txtMarcaFactura" ClientInstanceName="txtMarcaFactura" ClientVisible="false"></dx:ASPxTextBox>
                                            </td>
                                            <td style="width: 35%; text-align: right;"></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10%; text-align: right;">
                                                <dx:ASPxLabel ID="lblClienteEfImpEdt" runat="server" ClientInstanceName="lblClienteEfImpEdt" Font-Bold="True" Text="Razón Social :" Width="90%">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td colspan="3">
                                                <dx:ASPxTextBox ID="txtClienteEfImpEdt" runat="server" ClientInstanceName="txtClienteEfImpEdt" Height="16px" MaxLength="90" Width="90%">
                                                    <ClientSideEvents KeyPress="function(s, e) {
	    if ((event.keyCode&gt;32&amp;&amp;event.keyCode&lt;38)||(event.keyCode&gt;38&amp;&amp;event.keyCode&lt;46)||(event.keyCode&gt;57&amp;&amp;event.keyCode&lt;64)) event.returnValue = false;
                                                                                          
    }"
                                                        TextChanged="function(s, e) {
	                                                                                s.SetText(s.GetText().toUpperCase());
                                                                                }" />
                                                    <ReadOnlyStyle BackColor="#DEE8DB">
                                                    </ReadOnlyStyle>
                                                    <FocusedStyle BackColor="#FFFFCC">
                                                    </FocusedStyle>
                                                    <DisabledStyle BackColor="#DEE8DB">
                                                    </DisabledStyle>
                                                </dx:ASPxTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10%; text-align: right; padding-bottom: 10px;">
                                                <dx:ASPxLabel ID="lblNitEfImpEdt" runat="server" ClientInstanceName="lblNitEfImpEdt" Font-Bold="True" Text="NIT :" Width="90%">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td style="width: 30%; padding-bottom: 10px;">
                                                <dx:ASPxTextBox ID="txtNitEfImpEdt" runat="server" ClientInstanceName="txtNitEfImpEdt" MaxLength="15" Text="0" Width="90%">
                                                    <MaskSettings Mask="&lt;0..999999999999999&gt;" PromptChar=" " />
                                                </dx:ASPxTextBox>
                                            </td>
                                            <td style="width: 25%; padding-bottom: 10px; text-align: left;"></td>
                                            <td style="width: 35%; padding-bottom: 10px;"></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10%; text-align: right;">
                                                <dx:ASPxLabel ID="lblNoFactEfImpEdt" runat="server" Font-Bold="True" Text="No. Factura :" ClientInstanceName="lblNoFactEfImpEdt" Width="90%">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td style="width: 30%">
                                                <dx:ASPxTextBox ID="txtNoFactEfImpEdt" runat="server" MaxLength="15" Text="0" Width="90%" ClientInstanceName="txtNoFactEfImpEdt">
                                                </dx:ASPxTextBox>
                                            </td>
                                            <td style="width: 25%; text-align: right;"></td>
                                            <td style="width: 35%;"></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10%; text-align: right;">
                                                <dx:ASPxLabel ID="lblPrecioEfImpEdt" runat="server" Font-Bold="True" Text="Precio Unitario :" ClientInstanceName="lblPrecioEfImpEdt" Width="90%">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td style="width: 30%;">
                                                <asp:TextBox name="txtPrecioEfImpEdt" ID="txtPrecioEfImpEdt" runat="server" Width="90%" value="0" onChange="multiplicarEdicion();" onkeypress="return numbersonly(event);"></asp:TextBox>
                                            </td>
                                            <td style="width: 25%; text-align: right;"></td>
                                            <td style="width: 35%"></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10%; text-align: right;">
                                                <dx:ASPxLabel ID="lblFactVolRecibidoImpEdt" runat="server" Font-Bold="True" Text="Volumen Recibido :" Width="90%">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td style="width: 30%;">
                                                <asp:TextBox name="txtFactVolRecibidoImpEdt" ID="txtFactVolRecibidoImpEdt" runat="server" Width="90%" value="0" onChange="multiplicarEdicion();" onkeypress="return numbersonly(event);"></asp:TextBox>
                                            </td>
                                            <td style="width: 25%; text-align: right;"></td>
                                            <td style="width: 35%"></td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right; width: 10%">
                                                <dx:ASPxLabel ID="lblImporteEfImpEdt" runat="server" Font-Bold="True" Text="Importe Total :" ClientInstanceName="lblImporteEfImpEdt" Width="90%">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td style="width: 30%;">
                                                <asp:TextBox name="txtImporteEfImpEdt" ID="txtImporteEfImpEdt" runat="server" Width="90%" value="0" ReadOnly="true"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right; width: 25%"></td>
                                            <td style="width: 35%"></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10%; text-align: right;">
                                                <dx:ASPxLabel ID="lblFactPlantaOrigenImpEdt" runat="server" Font-Bold="True" Text="Planta Origen :" Width="90%">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td style="width: 30%;">
                                                <dx:ASPxTextBox ID="txtFactPlantaOrigenImpEdt" ClientInstanceName="txtFactPlantaOrigenImpEdt" runat="server" Width="90%">
                                                </dx:ASPxTextBox>
                                            </td>
                                            <td style="width: 25%; text-align: right;">
                                                <dx:ASPxLabel ID="lblFactPlantaDestinoImpEdt" runat="server" Font-Bold="True" Text="Planta Destino :" Width="90%">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td style="width: 35%;">
                                                <dx:ASPxTextBox ID="txtFactPlantaDestinoImpEdt" ClientInstanceName="txtFactPlantaDestinoImpEdt" runat="server" Width="90%">
                                                </dx:ASPxTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10%; text-align: right;">
                                                <dx:ASPxLabel ID="lblFactVolDespachadoImpEdt" runat="server" Font-Bold="True" Text="Volumen Despachado :" Width="90%">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td style="width: 30%;">
                                                <dx:ASPxTextBox ID="txtFactVolDespachadoImpEdt" ClientInstanceName="txtFactVolDespachadoImpEdt" runat="server" Width="90%">
                                                </dx:ASPxTextBox>
                                            </td>
                                            <td style="width: 25%; text-align: right;"></td>
                                            <td style="width: 35%;"></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10%; text-align: right;"></td>
                                            <td style="width: 30%;"></td>
                                            <td style="width: 25%; text-align: right;">
                                                <dx:ASPxLabel ID="lblFactFleteTransporteImpEdt" runat="server" Font-Bold="True" Text="Flete Transporte Bs. :" Width="90%">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td style="width: 35%;">
                                                <dx:ASPxTextBox ID="txtFactFleteTransporteImpEdt" runat="server" Width="90%">
                                                </dx:ASPxTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10%; text-align: right;">
                                                <dx:ASPxLabel ID="lblFactGravamenArancelarioImpEdt" runat="server" Font-Bold="True" Text="Gramaven Arancelaro :" Width="90%">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td style="width: 30%;">
                                                <asp:TextBox name="txtFactGravamenArancelarioImpEdt" ID="txtFactGravamenArancelarioImpEdt" runat="server" Width="90%" value="0" onkeypress="return numbersonly(event);"></asp:TextBox>
                                            </td>
                                            <td style="width: 25%; text-align: right;">
                                                <dx:ASPxLabel ID="lblFactSiduineaImpEdt" runat="server" Font-Bold="True" Text="SIDUINEA :" Width="90%">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td style="width: 35%;">
                                                <asp:TextBox name="txtFactSiduineaImpEdt" ID="txtFactSiduineaImpEdt" runat="server" Width="90%" value="0" onkeypress="return numbersonly(event);"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10%; text-align: right;">
                                                <dx:ASPxLabel ID="lblFactCostoArancelarioImpEdt" runat="server" Font-Bold="True" Text="Costo Deposito Arancelario :" Width="90%">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td style="width: 30%;">
                                                <asp:TextBox name="txtFactCostoArancelarioImpEdt" ID="txtFactCostoArancelarioImpEdt" runat="server" Width="90%" value="0" onkeypress="return numbersonly(event);"></asp:TextBox>
                                            </td>
                                            <td style="width: 25%; text-align: right;"></td>
                                            <td style="width: 35%;"></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10%; text-align: right; padding-bottom: 10px;">
                                                <dx:ASPxLabel ID="lblObsDocEfImpEdt" runat="server" Font-Bold="True" Text="Observaciones :" Width="90%">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td colspan="3" style="padding-bottom: 10px;">
                                                <dx:ASPxTextBox ID="txtObsDocEfImpEdt" runat="server" MaxLength="15" Width="100%" ClientInstanceName="txtObsDocEfImpEdt">
                                                </dx:ASPxTextBox>
                                            </td>
                                        </tr>

                                    </table>
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxCallbackPanel>

                        <table style="width: 100%">
                            <tr>
                                <td style="width: 25%; text-align: right;">&nbsp;</td>
                                <td style="width: 25%;">
                                    <dx:ASPxTextBox ID="txtIdDocEfImpEdt" runat="server" ClientInstanceName="txtIdDocEfImpEdt" Text="0" Width="170px" Font-Overline="False" Font-Size="XX-Small" ForeColor="White">
                                        <MaskSettings Mask="&lt;0..999999999999999&gt;" />
                                        <ReadOnlyStyle ForeColor="White">
                                        </ReadOnlyStyle>
                                        <FocusedStyle BackColor="White">
                                        </FocusedStyle>
                                        <Border BorderStyle="None" />
                                        <BorderBottom BorderStyle="None" />
                                    </dx:ASPxTextBox>
                                </td>
                                <td style="width: 25%; text-align: right;">
                                    <asp:TextBox ID="txtIdPadreImpEdt" runat="server" BorderStyle="None" ForeColor="White" Style="margin-top: 0px"></asp:TextBox>
                                </td>
                                <td style="width: 25%">
                                    <dx:ASPxButton ID="btnAgregaDocImpEdt" runat="server" AutoPostBack="False" Text="Actualizar Documento" Theme="Metropolis" EnableTheming="True" Width="100%" Font-Bold="True" ForeColor="Maroon">
                                        <ClientSideEvents Click="function(s, e) {
                                                                                        //txtIdDocEfImpEdt.SetValue(0);
                                                                                        dgvDocumentosImp.PerformCallback();
                                                                                        pucDocumentoImp.Hide();
                                                                                        }" />
                                        <Image IconID="reports_addgroupheader_16x16">
                                        </Image>
                                    </dx:ASPxButton>
                                </td>
                            </tr>
                        </table>

                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxPanel>
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>

<dx:ASPxPopupControl ID="pudDocumentoImpAdd" runat="server" CloseAction="CloseButton" CloseOnEscape="True" ScrollBars="Auto"
    PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Above" ClientInstanceName="pudDocumentoImpAdd" Maximized="True"
    HeaderText="Documentos de Importación" AllowDragging="True" Modal="True" PopupAnimationType="None" AllowResize="True"
    EnableViewState="False" OnWindowCallback="pucDocumentoImpAdd_WindowCallback" Theme="Metropolis">
    <ClientSideEvents PopUp="function(s, e) { ASPxClientEdit.ClearGroup('createAccountGroup'); }" />
    <HeaderImage IconID="actions_insert_32x32">
    </HeaderImage>
    <SizeGripImage Width="11px" />
    <HeaderStyle>
        <BorderBottom BorderStyle="Solid" />
    </HeaderStyle>
    <ContentCollection>

        <dx:PopupControlContentControl ID="PopupControlContentControl7" runat="server">
            <dx:ASPxPanel ID="ASPxPanel2" runat="server" DefaultButton="btCreate">
                <PanelCollection>
                    <dx:PanelContent ID="PanelContent12" runat="server">
                        <table class="dxflInternalEditorTable_Youthful" border="0">
                            <tr>
                                <td style="width: 10%; text-align: right;">
                                    <dx:ASPxLabel runat="server" ID="lblProducto" ClientInstanceName="lblProducto" Text="Producto:"></dx:ASPxLabel>
                                </td>
                                <td style="width: 10%">
                                    <dx:ASPxTextBox runat="server" ID="txtProductoSeleccionado" ClientInstanceName="txtProductoSeleccionado" ReadOnly="true" Font-Bold="true"></dx:ASPxTextBox>
                                </td>
                                <td style="width: 10%; text-align: right;">
                                    <dx:ASPxLabel ID="ASPxLabel7" runat="server" Font-Bold="True" ForeColor="#666666" Text="DUI :" Width="150px">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 15%">
                                    <dx:ASPxTextBox ID="txtDuiFacProducto" ClientInstanceName="txtDuiFacProducto" runat="server" Width="260px">
                                        <ReadOnlyStyle BackColor="#DEE8DB">
                                        </ReadOnlyStyle>
                                        <FocusedStyle BackColor="#FFFFCC">
                                        </FocusedStyle>
                                        <DisabledStyle BackColor="#DEE8DB">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                </td>
                                <td style="width: 10%; text-align: right;">
                                    <dx:ASPxLabel ID="ASPxLabel9" runat="server" Font-Bold="True" ForeColor="#666666" Text="Fecha Documento:" Width="150px">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 10%;">
                                    <dx:ASPxDateEdit ID="detFechaFacproducto" runat="server" EditFormat="DateTime" Font-Bold="False" Theme="Metropolis">
                                        <TimeSectionProperties>
                                            <TimeEditProperties>
                                                <ClearButton Visibility="Auto">
                                                </ClearButton>
                                            </TimeEditProperties>
                                        </TimeSectionProperties>
                                        <ClearButton Visibility="Auto">
                                        </ClearButton>
                                        <ValidationSettings>
                                            <RequiredField ErrorText="ERROR: Introduzca la fecha." />
                                        </ValidationSettings>
                                        <ReadOnlyStyle BackColor="#DEE8DB">
                                        </ReadOnlyStyle>
                                        <FocusedStyle BackColor="#FFFFCC">
                                        </FocusedStyle>
                                        <DisabledStyle BackColor="#DEE8DB">
                                        </DisabledStyle>
                                    </dx:ASPxDateEdit>
                                </td>
                                <td style="width: 8%; text-align: right;">
                                    <dx:ASPxLabel ID="ASPxLabel6" runat="server" Font-Bold="True" ForeColor="#666666" Text="Tipo Factura :">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 27%;">
                                    <dx:ASPxComboBox ID="cmbTipoFacturaImpAdd" runat="server" ClientInstanceName="cmbTipoFacturaImpAdd" TextField="DESCRIPCION" TextFormatString="{0}" Theme="Metropolis" ValueField="ID_PARAMETRICA_GLOBAL" Width="100%">
                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
	                                                                                                pnlFacturasImpAdd.PerformCallback(0);
                                                                                                }" />
                                        <Items>
                                            <dx:ListEditItem Selected="True" Text="Seleccione una opción" Value="0"></dx:ListEditItem>
                                            <dx:ListEditItem Text="Costo de Producto, Inspeccion y Monitoreo" Value="1"></dx:ListEditItem>
                                            <dx:ListEditItem Text="Costo de Transporte y Almacenaje" Value="2"></dx:ListEditItem>
                                            <dx:ListEditItem Text="Costo Aduanero" Value="3"></dx:ListEditItem>
                                            <dx:ListEditItem Text="Costo de Seguro" Value="4"></dx:ListEditItem>
                                            <dx:ListEditItem Text="Volumen Gasolina Base" Value="5"></dx:ListEditItem>
                                            <dx:ListEditItem Text="Costo Transporte Cisternas Masivo" Value="6"></dx:ListEditItem>
                                        </Items>
                                        <ClearButton Visibility="Auto">
                                        </ClearButton>
                                        <ValidationSettings>
                                            <RequiredField ErrorText="ERROR : Introduzca tipo de factura de importacion." IsRequired="True" />
                                        </ValidationSettings>
                                        <ReadOnlyStyle BackColor="#DEE8DB">
                                        </ReadOnlyStyle>
                                        <FocusedStyle BackColor="#FFFFCC">
                                        </FocusedStyle>
                                        <DisabledStyle BackColor="#DEE8DB">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <dx:ASPxCallbackPanel ID="pnlFacturasImpAdd" runat="server" Width="100%" OnCallback="pnlFacturasImpAdd_Callback" ClientInstanceName="pnlFacturasImpAdd">
                            <PanelCollection>
                                <dx:PanelContent ID="PanelContent13" runat="server">
                                    <br />
                                    <!--LMQC PANEL DE FACTURAS POR COSTOS DE PRODUCTO-->
                                    <dx:ASPxRoundPanel runat="server" ID="pnlFacturasCostoProducto" Visible="false" Width="100%" HeaderText="Facturas Costo de Producto" ShowHeader="true">
                                        <PanelCollection>
                                            <dx:PanelContent ID="PanelContent14" runat="server">
                                                <table style="width: 100%" border="0">
                                                    <tr>
                                                        <td style="width: 10%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblFacProducto" runat="server" Font-Bold="True" ForeColor="#666666" Text="No. Factura Proveedor :" ClientInstanceName="lblFacProducto" Width="90%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 10%">
                                                            <dx:ASPxTextBox ID="txtFacProducto" runat="server" MaxLength="15" Text="0" Width="90%" ClientInstanceName="txtFacProducto">
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                        <td style="width: 10%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblRazonSocialFacProducto" runat="server" ClientInstanceName="lblRazonSocialFacProducto" Font-Bold="True" Width="90%" ForeColor="#666666" Text="Proveedor :">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 15%">
                                                            <dx:ASPxTextBox ID="txtRazonSocialFacProducto" runat="server" ClientInstanceName="txtRazonSocialFacProducto" Height="16px" MaxLength="90" Width="90%">
                                                                <ClientSideEvents KeyPress="function(s, e) {
	                                                                                        if ((event.keyCode&gt;32&amp;&amp;event.keyCode&lt;38)||(event.keyCode&gt;38&amp;&amp;event.keyCode&lt;46)||(event.keyCode&gt;57&amp;&amp;event.keyCode&lt;64)) event.returnValue = false;                                                                                            
                                                                                        }"
                                                                    TextChanged="function(s, e) {
	                                                                                            s.SetText(s.GetText().toUpperCase());
                                                                                            }" />
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                        <td style="width: 10%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblNitFacProducto" runat="server" ClientInstanceName="lblNitFacProducto" Font-Bold="True" ForeColor="#666666" Text="NIT :" Width="90%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 10%;">
                                                            <dx:ASPxTextBox ID="txtNitFacProducto" runat="server" ClientInstanceName="txtNitFacProducto" MaxLength="15" Width="150px">
                                                                <MaskSettings Mask="&lt;0..999999999999999&gt;" PromptChar=" " />
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                        <td style="width: 8%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblTipoCambio" ClientInstanceName="lblTipoCambio" runat="server" Font-Bold="True" ForeColor="#666666" Text="Tipo de Cambio :" Width="90%" ClientVisible="false">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 12%">
                                                            <asp:TextBox name="txtTipoCambio" ID="txtTipoCambio" runat="server" Width="200px" value="0" onkeypress="return numbersonly(event);" Visible="false"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 10%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblSecFacProducto" ClientInstanceName="lblSecFacProducto" runat="server" Font-Bold="True" ForeColor="#666666" Text="Secuencia Factura :" Width="90%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 5%;">
                                                            <dx:ASPxTextBox ID="txtSecFacProducto" ClientInstanceName="txtSecFacProducto" runat="server" Width="50px" Text="0">
                                                                <MaskSettings Mask="&lt;0..999999999999999&gt;" PromptChar=" " />
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 10%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblPreUnitarioFacProducto" runat="server" Font-Bold="True" ForeColor="#666666" Text="Precio Unitario (Bs) :" ClientInstanceName="lblPreUnitarioFacProducto">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 10%">
                                                            <asp:TextBox name="txtPreUnitarioFacProducto" ID="txtPreUnitarioFacProducto" runat="server" Width="90%" value="0" onkeypress="return numbersonly(event);"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 10%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblVolRecibidoFacProducto" ClientInstanceName="lblVolRecibidoFacProducto" runat="server" Font-Bold="True" ForeColor="#666666" Text="Volumen Facturado (Lts) :" Width="150px">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 15%">
                                                            <asp:TextBox name="txtVolrecibidoFacProducto" ID="txtVolrecibidoFacProducto" runat="server" Width="90%" value="0" onChange="multiplicar();" onkeypress="return numbersonly(event);"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 10%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblPreTotalFacProducto" runat="server" Font-Bold="True" ForeColor="#666666" Text="Valor Facturado (Bs) :" ClientInstanceName="lblPreTotalFacProducto">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 10%;">
                                                            <asp:TextBox name="txtPreTotalFacProducto" ID="txtPreTotalFacProducto" runat="server" Width="90%" value="0" ReadOnly="true"></asp:TextBox>

                                                        </td>
                                                        <td style="width: 8%; text-align: right;"></td>
                                                        <td style="width: 12%"></td>
                                                        <td style="width: 10%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblPreUnitarioSusFacProducto" ClientInstanceName="lblPreUnitarioSusFacProducto" runat="server" Font-Bold="True" ForeColor="#666666" Text="Precio Unitario $us. :" ClientVisible="false">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 5%;">
                                                            <dx:ASPxTextBox ID="txtPreUnitarioSusFacProducto" runat="server" ClientInstanceName="txtPreUnitarioSusFacProducto" Text="0" Width="170px" ClientVisible="false">
                                                                <ClientSideEvents LostFocus="function(s, e) {
                                                                                                                    //if(parseFloat(txtPreUnitarioFacProducto.GetText(), 10)==0)
                                                                                                                    //{
                                                                                                                    var num1 = parseFloat(txtPreUnitarioSusFacProducto.GetText(), 10);
                                                                                                                    var num2 = parseFloat(txtTipoCambio.GetText(), 10);
                                                                                                                    txtPreUnitarioFacProducto.SetText((num1*num2));
                                                                                                                    //}
                                                                                                                }" />
                                                                <MaskSettings IncludeLiterals="DecimalSymbol" PromptChar=" " />
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 10%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblObsFacProducto" ClientInstanceName="lblObsFacProducto" runat="server" Font-Bold="True" ForeColor="#666666" Text="Observaciones :" Width="90%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td colspan="9">
                                                            <dx:ASPxTextBox ID="txtObsFacProducto" runat="server" MaxLength="1500" Width="90%" ClientInstanceName="txtObsFacProducto">
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                    </tr>

                                                </table>
                                            </dx:PanelContent>
                                        </PanelCollection>
                                    </dx:ASPxRoundPanel>

                                    <!--LMQC PANEL DE FACTURAS POR COSTOS DE INSPECCION-->
                                    <dx:ASPxRoundPanel runat="server" ID="pnlFacturasCostoInspeccion" Visible="false" Width="100%" HeaderText="Facturas Costo de Inspección" ShowHeader="true">
                                        <PanelCollection>
                                            <dx:PanelContent ID="PanelContent15" runat="server">
                                                <table style="width: 100%" border="0">
                                                    <tr>
                                                        <td colspan="10" style="text-align: center;">
                                                            <dx:ASPxLabel ID="ASPxLabel8" runat="server" Text="FACTURAS DE INSPECCION DE ORIGEN" Font-Bold="true" Font-Underline="true"></dx:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 10%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblFacInOrigen" ClientInstanceName="lblFacInOrigen" runat="server" Font-Bold="True" ForeColor="#666666" Text="Nro Factura :" Width="90%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 10%">
                                                            <dx:ASPxTextBox ID="txtFacInOrigen" ClientInstanceName="txtFacInOrigen" runat="server" Width="90%" Text="0">
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                        <td style="width: 10%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblEmpresaFacInOrigen" ClientInstanceName="lblEmpresaFacInOrigen" runat="server" Font-Bold="True" ForeColor="#666666" Text="Empresa Inspectora :" Width="90%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 15%">
                                                            <dx:ASPxTextBox ID="txtEmpresaFacInOrigen" ClientInstanceName="txtEmpresaFacInOrigen" runat="server" Width="90%">
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                        <td style="width: 10%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblMonFacInOrigen" ClientInstanceName="lblMonFacInOrigen" runat="server" Font-Bold="True" ForeColor="#666666" Text="Monto Inspeccion Origen Bs :" Width="90%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 10%;">
                                                            <asp:TextBox name="txtMonFacInOrigen" ID="txtMonFacInOrigen" runat="server" Width="90%" value="0" onkeypress="return numbersonly(event);"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 8%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblMonSusFacInOrigen" ClientInstanceName="lblMonSusFacInOrigen" runat="server" Font-Bold="True" ForeColor="#666666" Text="Monto Inspeccion Origen Sus :" Width="90%" ClientVisible="false">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 10%">
                                                            <dx:ASPxTextBox ID="txtMonSusFacInOrigen" runat="server" Width="90%" ClientInstanceName="txtMonSusFacInOrigen" ClientVisible="false">
                                                                <ClientSideEvents LostFocus="function(s, e) {
                                                                                                                //if(parseFloat(txtPreTotalFacProducto.GetText(), 10)==0)
                                                                                                                //{
                                                                                                                var num1 = parseFloat(txtMonSusFacInOrigen.GetText(), 10);
                                                                                                                var num2 = parseFloat(txtTipoCambio.GetText(), 10);
                                                                                                                txtMonFacInOrigen.SetText((num1*num2));
                                                                                                                //}
                                                                                                                }" />
                                                                <MaskSettings IncludeLiterals="DecimalSymbol" PromptChar=" " />
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                        <td style="width: 10%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblSecFacInOrigen" ClientInstanceName="lblSecFacInOrigen" runat="server" Font-Bold="True" ForeColor="#666666" Text="Secuencia Factura :" Width="90%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 7%;">
                                                            <dx:ASPxTextBox ID="txtSecFacInOrigen" ClientInstanceName="txtSecFacInOrigen" runat="server" Width="50px" Text="0">
                                                                <MaskSettings Mask="&lt;0..999999999999999&gt;" PromptChar=" " />
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 10%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblVolFactInOrigen" ClientInstanceName="lblVolFactInOrigen" runat="server" Font-Bold="True" ForeColor="#666666" Text="Volumen Facturado :" Width="90%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 10%">
                                                            <asp:TextBox name="txtVolFactInOrigen" ID="txtVolFactInOrigen" runat="server" Width="90%" value="0" onkeypress="return numbersonly(event);"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 10%; text-align: right;"></td>
                                                        <td style="width: 15%"></td>
                                                        <td style="width: 10%; text-align: right;"></td>
                                                        <td style="width: 10%;"></td>
                                                        <td style="width: 8%; text-align: right;"></td>
                                                        <td style="width: 10%"></td>
                                                        <td style="width: 10%; text-align: right;"></td>
                                                        <td style="width: 7%;"></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 10%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblObsFacInOrigen" ClientInstanceName="lblObsFacInOrigen" runat="server" Font-Bold="True" ForeColor="#666666" Text="Observaciones :" Width="90%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td colspan="9">
                                                            <dx:ASPxTextBox ID="txtObsFacInOrigen" runat="server" MaxLength="1500" Width="90%" ClientInstanceName="txtObsFacInOrigen">
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="10" style="text-align: center;">
                                                            <dx:ASPxLabel ID="ASPxLabel10" runat="server" Text="FACTURAS DE INSPECCION INTERMEDIO" Font-Bold="true" Font-Underline="true"></dx:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 10%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblFacInIn" ClientInstanceName="lblFacInIn" runat="server" Font-Bold="True" ForeColor="#666666" Text="Nro Factura :" Width="90%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 10%">
                                                            <dx:ASPxTextBox ID="txtFacInIn" ClientInstanceName="txtFacInIn" runat="server" Width="90%" Text="0">
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                        <td style="width: 10%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblEmpresaFacInIn" ClientInstanceName="lblEmpresaFacInIn" runat="server" Font-Bold="True" ForeColor="#666666" Text="Empresa Inspectora :" Width="90%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 15%">
                                                            <dx:ASPxTextBox ID="txtEmpresaFacInIn" ClientInstanceName="txtEmpresaFacInIn" runat="server" Width="90%">
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                        <td style="width: 10%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblMontoFacInIn" ClientInstanceName="lblMontoFacInIn" runat="server" Font-Bold="True" ForeColor="#666666" Text="Monto Inspeccion Intermedio Bs :" Width="90%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 10%;">
                                                            <asp:TextBox name="txtMontoFacInIn" ID="txtMontoFacInIn" runat="server" Width="90%" value="0" onkeypress="return numbersonly(event);"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 8%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblMontoSusFacInIn" ClientInstanceName="lblMontoSusFacInIn" runat="server" Font-Bold="True" ForeColor="#666666" Text="Monto Inspeccion Intermedio $us :" Width="90%" ClientVisible="false">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 10%">
                                                            <dx:ASPxTextBox ID="txtMontoSusFacInIn" runat="server" Width="90%" ClientInstanceName="txtMontoSusFacInIn" ClientVisible="false">
                                                                <ClientSideEvents LostFocus="function(s, e) {
                                                                                                                    //if(parseFloat(txtPreTotalFacProducto.GetText(), 10)==0)
                                                                                                                    //{
                                                                                                                    var num1 = parseFloat(txtMontoSusFacInIn.GetText(), 10);
                                                                                                                    var num2 = parseFloat(txtTipoCambio.GetText(), 10);
                                                                                                                    txtMontoFacInIn.SetText((num1*num2));
                                                                                                                    //}
                                                                                                                    }" />
                                                                <MaskSettings IncludeLiterals="DecimalSymbol" PromptChar=" " />
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                        <td style="width: 10%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblSecFacInIn" ClientInstanceName="lblSecFacInIn" runat="server" Font-Bold="True" ForeColor="#666666" Text="Secuencia Factura :" Width="90%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 7%;">
                                                            <dx:ASPxTextBox ID="txtSecFacInIn" ClientInstanceName="txtSecFacInIn" runat="server" Width="50px" Text="0">
                                                                <MaskSettings Mask="&lt;0..999999999999999&gt;" PromptChar=" " />
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 10%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblVolFactInIn" ClientInstanceName="lblVolFactInIn" runat="server" Font-Bold="True" ForeColor="#666666" Text="Volumen Facturado :" Width="90%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 10%">
                                                            <asp:TextBox name="txtVolFactInIn" ID="txtVolFactInIn" runat="server" Width="90%" value="0" onkeypress="return numbersonly(event);"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 10%; text-align: right;"></td>
                                                        <td style="width: 15%"></td>
                                                        <td style="width: 10%; text-align: right;"></td>
                                                        <td style="width: 10%;"></td>
                                                        <td style="width: 8%; text-align: right;"></td>
                                                        <td style="width: 10%"></td>
                                                        <td style="width: 10%; text-align: right;"></td>
                                                        <td style="width: 7%;"></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 10%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblObsFacInIn" ClientInstanceName="lblObsFacInIn" runat="server" Font-Bold="True" ForeColor="#666666" Text="Observaciones :" Width="90%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td colspan="9">
                                                            <dx:ASPxTextBox ID="txtObsFacInIn" runat="server" MaxLength="1500" Width="90%" ClientInstanceName="txtObsFacInIn">
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="10" style="text-align: center;">
                                                            <dx:ASPxLabel ID="ASPxLabel11" runat="server" Text="FACTURAS DE INSPECCION FINAL" Font-Bold="true" Font-Underline="true"></dx:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 10%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblFacInFin" ClientInstanceName="lblFacInFin" runat="server" Font-Bold="True" ForeColor="#666666" Text="Nro Factura :" Width="90%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 10%">
                                                            <dx:ASPxTextBox ID="txtFacInFin" ClientInstanceName="txtFacInFin" runat="server" Width="90%" Text="0">
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                        <td style="width: 10%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblEmpresaFacInFin" ClientInstanceName="lblEmpresaFacInFin" runat="server" Font-Bold="True" ForeColor="#666666" Text="Empresa Inspectora :" Width="90%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 15%">
                                                            <dx:ASPxTextBox ID="txtEmpresaFacInFin" ClientInstanceName="txtEmpresaFacInFin" runat="server" Width="90%">
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                        <td style="width: 10%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblMontoFacInFin" ClientInstanceName="lblMontoFacInFin" runat="server" Font-Bold="True" ForeColor="#666666" Text="Monto Inspeccion Final Bs :" Width="90%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 10%;">
                                                            <asp:TextBox name="txtMontoFacInFin" ID="txtMontoFacInFin" runat="server" Width="90%" value="0" onkeypress="return numbersonly(event);"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 8%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblMontoSusFacInFin" ClientInstanceName="lblMontoSusFacInFin" runat="server" Font-Bold="True" ForeColor="#666666" Text="Monto Inspeccion Final $us :" Width="90%" ClientVisible="false">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 10%">
                                                            <dx:ASPxTextBox ID="txtMontoSusFacInFin" runat="server" Width="90%" ClientInstanceName="txtMontoSusFacInFin" ClientVisible="false">
                                                                <ClientSideEvents LostFocus="function(s, e) {
                                                                                                                //if(parseFloat(txtPreTotalFacProducto.GetText(), 10)==0)
                                                                                                                //{
                                                                                                                var num1 = parseFloat(txtMontoSusFacInFin.GetText(), 10);
                                                                                                                var num2 = parseFloat(txtTipoCambio.GetText(), 10);
                                                                                                                txtMontoFacInFin.SetText((num1*num2));
                                                                                                                //}
                                                                                                                }" />
                                                                <MaskSettings IncludeLiterals="DecimalSymbol" PromptChar=" " />
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                        <td style="width: 10%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblSecFacInFin" ClientInstanceName="lblSecFacInFin" runat="server" Font-Bold="True" ForeColor="#666666" Text="Secuencia Factura :" Width="90%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 7%;">
                                                            <dx:ASPxTextBox ID="txtSecFacInFin" ClientInstanceName="txtSecFacInFin" runat="server" Width="50px" Text="0">
                                                                <MaskSettings Mask="&lt;0..999999999999999&gt;" PromptChar=" " />
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 10%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblVolFactInFin" ClientInstanceName="lblVolFactInFin" runat="server" Font-Bold="True" ForeColor="#666666" Text="Volumen Facturado :" Width="90%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 10%">
                                                            <asp:TextBox name="txtVolFactInFin" ID="txtVolFactInFin" runat="server" Width="90%" value="0" onkeypress="return numbersonly(event);"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 10%; text-align: right;"></td>
                                                        <td style="width: 15%"></td>
                                                        <td style="width: 10%; text-align: right;"></td>
                                                        <td style="width: 10%;"></td>
                                                        <td style="width: 8%; text-align: right;"></td>
                                                        <td style="width: 10%"></td>
                                                        <td style="width: 10%; text-align: right;"></td>
                                                        <td style="width: 7%;"></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 10%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblObsFacInFin" ClientInstanceName="lblObsFacInFin" runat="server" Font-Bold="True" ForeColor="#666666" Text="Observaciones :" Width="90%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td colspan="9">
                                                            <dx:ASPxTextBox ID="txtObsFacInFin" runat="server" MaxLength="1500" Width="90%" ClientInstanceName="txtObsFacInFin">
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </dx:PanelContent>
                                        </PanelCollection>
                                    </dx:ASPxRoundPanel>

                                    <!--LMQC PANEL DE FACTURAS POR COSTOS DE MONITOREO-->
                                    <dx:ASPxRoundPanel runat="server" ID="pnlFacturasCostoMonitoreo" Visible="false" Width="100%" HeaderText="Facturas Costo de Monitoreo" ShowHeader="true">
                                        <PanelCollection>
                                            <dx:PanelContent ID="PanelContent16" runat="server">
                                                <table style="width: 100%" border="0">
                                                    <tr>
                                                        <td style="width: 10%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblFacMon" ClientInstanceName="lblFacMon" runat="server" Font-Bold="True" ForeColor="#666666" Text="Nº Factura Monitoreo :" Width="90%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 10%">
                                                            <dx:ASPxTextBox ID="txtFacMon" ClientInstanceName="txtFacMon" runat="server" Width="90%" Text="0">
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                        <td style="width: 10%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblEmpresaFacMon" ClientInstanceName="lblEmpresaFacMon" runat="server" Font-Bold="True" ForeColor="#666666" Text="Empresa Monitoreo :" Width="90%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 15%">
                                                            <dx:ASPxTextBox ID="txtEmpresaFacMon" ClientInstanceName="txtEmpresaFacMon" runat="server" Width="90%">
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                        <td style="width: 10%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblMontoFacMon" ClientInstanceName="lblMontoFacMon" runat="server" Font-Bold="True" ForeColor="#666666" Text="Monto Monitoreo Bs. :" Width="90%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 10%;">
                                                            <asp:TextBox name="txtMontoFacMon" ID="txtMontoFacMon" runat="server" Width="90%" value="0" onkeypress="return numbersonly(event);"></asp:TextBox>

                                                        </td>
                                                        <td style="width: 8%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblMontoSusFacMon" ClientInstanceName="lblMontoSusFacMon" runat="server" Font-Bold="True" ForeColor="#666666" Text="Monto Monitoreo $us. :" Width="90%" ClientVisible="false">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 12%">
                                                            <dx:ASPxTextBox ID="txtMontoSusFacMon" runat="server" Width="90%" ClientInstanceName="txtMontoSusFacMon" ClientVisible="false">
                                                                <ClientSideEvents LostFocus="function(s, e) {
                                                                                                                //if(parseFloat(txtPreTotalFacProducto.GetText(), 10)==0)
                                                                                                                //{
                                                                                                                var num1 = parseFloat(txtMontoSusFacMon.GetText(), 10);
                                                                                                                var num2 = parseFloat(txtTipoCambio.GetText(), 10);
                                                                                                                txtMontoFacMon.SetText((num1*num2));
                                                                                                                //}
                                                                                                                }" />
                                                                <MaskSettings IncludeLiterals="DecimalSymbol" PromptChar=" " />
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                        <td style="width: 10%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblSecFacMon" ClientInstanceName="lblSecFacMon" runat="server" Font-Bold="True" ForeColor="#666666" Text="Secuencia Factura :" Width="90%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 5%;">
                                                            <dx:ASPxTextBox ID="txtSecFacMon" runat="server" ClientInstanceName="txtSecFacMon" Width="50px" Text="0">
                                                                <MaskSettings Mask="&lt;0..999999999999999&gt;" PromptChar=" " />
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 10%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblVolReMon" ClientInstanceName="lblVolReMon" runat="server" Font-Bold="True" ForeColor="#666666" Text="Volumen Recibido :" Width="90%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 10%">
                                                            <asp:TextBox name="txtVolReMon" ID="txtVolReMon" runat="server" Width="90%" value="0" onkeypress="return numbersonly(event);"></asp:TextBox>

                                                        </td>
                                                        <td style="width: 10%; text-align: right;"></td>
                                                        <td style="width: 15%"></td>
                                                        <td style="width: 10%; text-align: right;"></td>
                                                        <td style="width: 10%;"></td>
                                                        <td style="width: 8%; text-align: right;"></td>
                                                        <td style="width: 12%"></td>
                                                        <td style="width: 10%; text-align: right;"></td>
                                                        <td style="width: 5%;"></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 10%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblObsFacMon" ClientInstanceName="lblObsFacMon" runat="server" Font-Bold="True" ForeColor="#666666" Text="Observaciones :" Width="90%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td colspan="9">
                                                            <dx:ASPxTextBox ID="txtObsFacMon" runat="server" MaxLength="1500" Width="90%" ClientInstanceName="txtObsFacMon">
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </dx:PanelContent>
                                        </PanelCollection>
                                    </dx:ASPxRoundPanel>

                                    <!--LMQC PANEL DE FACTURAS POR COSTOS DE TRANSPORTE-->
                                    <dx:ASPxRoundPanel runat="server" ID="pnlFacturasCostoTransporte" Visible="false" Width="100%" HeaderText="Facturas Costo Transporte" ShowHeader="true">
                                        <PanelCollection>
                                            <dx:PanelContent ID="PanelContent17" runat="server">
                                                <table style="width: 100%" border="0">
                                                    <tr>
                                                        <td colspan="10" style="text-align: center;">
                                                            <dx:ASPxLabel ID="ASPxLabel12" runat="server" Text="FACTURAS DE TRANSPORTE FLUVIAL / BARCAZAS" Font-Bold="true" Font-Underline="true"></dx:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 10%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblEmpresaFacTransFlu" ClientInstanceName="lblEmpresaFacTransFlu" runat="server" Font-Bold="True" ForeColor="#666666" Text="Empresa :" Width="90%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 10%">
                                                            <dx:ASPxTextBox ID="txtEmpresaFacTransFlu" ClientInstanceName="txtEmpresaFacTransFlu" runat="server" Width="90%">
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                        <td style="width: 10%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblFacTransFlu" ClientInstanceName="lblFacTransFlu" runat="server" Font-Bold="True" ForeColor="#666666" Text="Nº Factura Transporte :" Width="90%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 15%">
                                                            <dx:ASPxTextBox ID="txtFacTransFlu" ClientInstanceName="txtFacTransFlu" runat="server" Width="90%" Text="0">
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                        <td style="width: 10%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblCostoUniFacTransFlu" ClientInstanceName="lblCostoUniFacTransFlu" runat="server" Font-Bold="True" ForeColor="#666666" Text="Costo Unitario :" Width="90%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 10%;">
                                                            <asp:TextBox name="txtCostoUniFacTransFlu" ID="txtCostoUniFacTransFlu" runat="server" Width="90%" value="0" onkeypress="return numbersonly(event);"></asp:TextBox>

                                                        </td>
                                                        <td style="width: 8%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblFleteFacTransFlu" ClientInstanceName="lblFleteFacTransFlu" runat="server" Font-Bold="True" ForeColor="#666666" Text="Flete Transporte Bs. :" Width="90%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 10%">
                                                            <asp:TextBox name="txtFleteFacTransFlu" ID="txtFleteFacTransFlu" runat="server" Width="90%" value="0" onkeypress="return numbersonly(event);"></asp:TextBox>

                                                        </td>
                                                        <td style="width: 10%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblSecFacTransFlu" ClientInstanceName="lblSecFacTransFlu" runat="server" Font-Bold="True" ForeColor="#666666" Text="Secuencia Factura :" Width="90%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 7%;">
                                                            <dx:ASPxTextBox ID="txtSecFacTransFlu" ClientInstanceName="txtSecFacTransFlu" runat="server" Width="50px" Text="0">
                                                                <MaskSettings Mask="&lt;0..999999999999999&gt;" PromptChar=" " />
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 10%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblPlantaDespachoFacTransFlu" ClientInstanceName="lblPlantaDespachoFacTransFlu" runat="server" Font-Bold="True" ForeColor="#666666" Text="Planta Despacho :" Width="90%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 10%">
                                                            <dx:ASPxTextBox ID="txtPlantaDespachoFacTransFlu" ClientInstanceName="txtPlantaDespachoFacTransFlu" runat="server" Width="90%">
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                        <td style="width: 10%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblVolDespachadoFacTransFlu" ClientInstanceName="lblVolDespachadoFacTransFlu" runat="server" Font-Bold="True" ForeColor="#666666" Text="Volumen Despachado :" Width="90%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 15%">
                                                            <asp:TextBox name="txtVolDespachadoFacTransFlu" ID="txtVolDespachadoFacTransFlu" runat="server" Width="90%" value="0" onkeypress="return numbersonly(event);"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 10%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblPlantaRecepcionFacTransFlu" ClientInstanceName="lblPlantaRecepcionFacTransFlu" runat="server" Font-Bold="True" ForeColor="#666666" Text="Planta Recepcion :" Width="90%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 10%;">
                                                            <dx:ASPxTextBox ID="txtPlantaRecepcionFacTransFlu" ClientInstanceName="txtPlantaRecepcionFacTransFlu" runat="server" Width="90%">
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                        <td style="width: 8%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblVolRecibidoFacTransFlu" ClientInstanceName="lblVolRecibidoFacTransFlu" runat="server" Font-Bold="True" ForeColor="#666666" Text="Volumen Recibido :" Width="90%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 10%">
                                                            <asp:TextBox name="txtVolRecibidoFacTransFlu" ID="txtVolRecibidoFacTransFlu" runat="server" Width="90%" value="0" onkeypress="return numbersonly(event);"></asp:TextBox>

                                                        </td>
                                                        <td style="width: 10%; text-align: right;"></td>
                                                        <td style="width: 7%;"></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 10%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblObsFacTransFlu" ClientInstanceName="lblObsFacTransFlu" runat="server" Font-Bold="True" ForeColor="#666666" Text="Observaciones :" Width="90%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td colspan="9">
                                                            <dx:ASPxTextBox ID="txtObsFacTransFlu" runat="server" MaxLength="1500" Width="90%" ClientInstanceName="txtObsFacTransFlu">
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="10" style="text-align: center;">
                                                            <dx:ASPxLabel ID="ASPxLabel13" runat="server" Text="FACTURAS DE TRANSPORTE TERRESTRE - CISTERNAS" Font-Bold="true" Font-Underline="true"></dx:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 10%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblEmpresaFacTransTe" ClientInstanceName="lblEmpresaFacTransTe" runat="server" Font-Bold="True" ForeColor="#666666" Text="Empresa :" Width="90%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 10%">
                                                            <dx:ASPxTextBox ID="txtEmpresaFacTransTe" ClientInstanceName="txtEmpresaFacTransTe" runat="server" Width="90%">
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                        <td style="width: 10%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblFacTransTe" ClientInstanceName="lblFacTransTe" runat="server" Font-Bold="True" ForeColor="#666666" Text="Nº Factura Transporte :" Width="90%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 15%">
                                                            <dx:ASPxTextBox ID="txtFacTransTe" ClientInstanceName="txtFacTransTe" runat="server" Width="90%" Text="0">
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                        <td style="width: 10%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblCostoUniFacTransTe" ClientInstanceName="lblCostoUniFacTransTe" runat="server" Font-Bold="True" ForeColor="#666666" Text="Costo Unitario :" Width="90%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 10%;">
                                                            <asp:TextBox name="txtCostoUniFacTransTe" ID="txtCostoUniFacTransTe" runat="server" Width="90%" value="0" onkeypress="return numbersonly(event);"></asp:TextBox>

                                                        </td>
                                                        <td style="width: 8%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblFleteFacTransTe" ClientInstanceName="lblFleteFacTransTe" runat="server" Font-Bold="True" ForeColor="#666666" Text="Flete Transporte Bs. :" Width="90%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 10%">
                                                            <asp:TextBox name="txtFleteFacTransTe" ID="txtFleteFacTransTe" runat="server" Width="90%" value="0" onkeypress="return numbersonly(event);"></asp:TextBox>

                                                        </td>
                                                        <td style="width: 10%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblSecFacTransTe" ClientInstanceName="lblSecFacTransTe" runat="server" Font-Bold="True" ForeColor="#666666" Text="Secuencia Factura :" Width="90%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 7%;">
                                                            <dx:ASPxTextBox ID="txtSecFacTransTe" ClientInstanceName="txtSecFacTransTe" runat="server" Width="50px" Text="0">
                                                                <MaskSettings Mask="&lt;0..999999999999999&gt;" PromptChar=" " />
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 10%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblPlantaDespachoFacTransTe" ClientInstanceName="lblPlantaDespachoFacTransTe" runat="server" Font-Bold="True" ForeColor="#666666" Text="Planta Despacho :" Width="90%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 10%">
                                                            <dx:ASPxTextBox ID="txtPlantaDespachoFacTransTe" ClientInstanceName="txtPlantaDespachoFacTransTe" runat="server" Width="90%">
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                        <td style="width: 10%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblVolDespachadoFacTransTe" ClientInstanceName="lblVolDespachadoFacTransTe" runat="server" Font-Bold="True" ForeColor="#666666" Text="Volumen Despachado :" Width="90%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 15%">
                                                            <asp:TextBox name="txtVolDespachadoFacTransTe" ID="txtVolDespachadoFacTransTe" runat="server" Width="90%" value="0" onkeypress="return numbersonly(event);"></asp:TextBox>

                                                        </td>
                                                        <td style="width: 10%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblPlantaRecepcionFacTransTe" ClientInstanceName="lblPlantaRecepcionFacTransTe" runat="server" Font-Bold="True" ForeColor="#666666" Text="Planta Recepcion :" Width="90%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 10%;">
                                                            <dx:ASPxTextBox ID="txtPlantaRecepcionFacTransTe" ClientInstanceName="txtPlantaRecepcionFacTransTe" runat="server" Width="90%">
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                        <td style="width: 8%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblVolRecibidoFacTransTe" ClientInstanceName="lblVolRecibidoFacTransTe" runat="server" Font-Bold="True" ForeColor="#666666" Text="Volumen Recibido :" Width="90%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 10%">
                                                            <asp:TextBox name="txtVolRecibidoFacTransTe" ID="txtVolRecibidoFacTransTe" runat="server" Width="90%" value="0" onkeypress="return numbersonly(event);"></asp:TextBox>

                                                        </td>
                                                        <td style="width: 10%; text-align: right;"></td>
                                                        <td style="width: 7%;"></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 10%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblObsFacTransTe" ClientInstanceName="lblObsFacTransTe" runat="server" Font-Bold="True" ForeColor="#666666" Text="Observaciones :" Width="90%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td colspan="9">
                                                            <dx:ASPxTextBox ID="txtObsFacTransTe" runat="server" MaxLength="1500" Width="90%" ClientInstanceName="txtObsFacTransTe">
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <br />
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td colspan="10" style="text-align: center;">
                                                            <dx:ASPxLabel ID="lblTituloTransTeDuGb" runat="server" Text="FACTURAS DE TRANSPORTE GASOLINA BLANCA DUCTOS" Font-Bold="true" Font-Underline="true"></dx:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 10%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblVolumenFacTransTeDuGb" ClientInstanceName="lblVolumenFacTransTeDuGb" runat="server" Font-Bold="True" ForeColor="#666666" Text="Volumen :" Width="90%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 10%">
                                                            <asp:TextBox name="txtVolFacTransDu" ID="txtVolFacTransDu" runat="server" Width="90%" value="0" onkeypress="return numbersonly(event);"></asp:TextBox>

                                                        </td>
                                                        <td style="width: 10%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblPreUniFacTransTeDuGb" ClientInstanceName="lblPreUniFacTransTeDuGb" runat="server" Font-Bold="True" ForeColor="#666666" Text="Precio (Bs/Lt) :" Width="90%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 15%">
                                                            <asp:TextBox name="txtPreUniFacTransDu" ID="txtPreUniFacTransDu" runat="server" Width="90%" value="0" onkeypress="return numbersonly(event);"></asp:TextBox>

                                                        </td>
                                                        <td style="width: 10%; text-align: right;"></td>
                                                        <td style="width: 10%;"></td>
                                                        <td style="width: 8%; text-align: right;"></td>
                                                        <td style="width: 10%"></td>
                                                        <td style="width: 10%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblSecFacTransTeGb" ClientInstanceName="lblSecFacTransTeGb" runat="server" Font-Bold="True" ForeColor="#666666" Text="Secuencia Factura :" Width="90%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 7%;">
                                                            <dx:ASPxTextBox ID="txtSecFacTransTeGb" ClientInstanceName="txtSecFacTransTeGb" runat="server" Width="50px" Text="0">
                                                                <MaskSettings Mask="&lt;0..999999999999999&gt;" PromptChar=" " />
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td colspan="10" style="text-align: center;">
                                                            <dx:ASPxLabel ID="ASPxLabel14" runat="server" Text="FACTURAS DE TRANSPORTE FERREO" Font-Bold="true" Font-Underline="true"></dx:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 10%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblEmpresaFacTransFe" ClientInstanceName="lblEmpresaFacTransFe" runat="server" Font-Bold="True" ForeColor="#666666" Text="Empresa :" Width="90%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 10%">
                                                            <dx:ASPxTextBox ID="txtEmpresaFacTransFe" ClientInstanceName="txtEmpresaFacTransFe" runat="server" Width="90%">
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                        <td style="width: 10%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblFacTransFe" ClientInstanceName="lblFacTransFe" runat="server" Font-Bold="True" ForeColor="#666666" Text="Nº Factura Transporte :" Width="90%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 15%">
                                                            <dx:ASPxTextBox ID="txtFacTransFe" ClientInstanceName="txtFacTransFe" runat="server" Width="90%" Text="0">
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                        <td style="width: 10%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblCostoUniFacTransFe" ClientInstanceName="lblCostoUniFacTransFe" runat="server" Font-Bold="True" ForeColor="#666666" Text="Costo Unitario :" Width="90%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 10%;">
                                                            <asp:TextBox name="txtCostoUniFacTransFe" ID="txtCostoUniFacTransFe" runat="server" Width="90%" value="0" onkeypress="return numbersonly(event);"></asp:TextBox>

                                                        </td>
                                                        <td style="width: 8%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblFleteFacTransFe" ClientInstanceName="lblFleteFacTransFe" runat="server" Font-Bold="True" ForeColor="#666666" Text="Flete Transporte Bs. :" Width="90%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 10%">
                                                            <asp:TextBox name="txtFleteFacTransFe" ID="txtFleteFacTransFe" runat="server" Width="90%" value="0" onkeypress="return numbersonly(event);"></asp:TextBox>

                                                        </td>
                                                        <td style="width: 10%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblSecFacTransFe" ClientInstanceName="lblSecFacTransFe" runat="server" Font-Bold="True" ForeColor="#666666" Text="Secuencia Factura :" Width="90%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 7%;">
                                                            <dx:ASPxTextBox ID="txtSecFacTransFe" ClientInstanceName="txtSecFacTransFe" runat="server" Width="50px" Text="0">
                                                                <MaskSettings Mask="&lt;0..999999999999999&gt;" PromptChar=" " />
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 10%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblVolDespachadoFacTransFe" ClientInstanceName="lblVolDespachadoFacTransFe" runat="server" Font-Bold="True" ForeColor="#666666" Text="Volumen Destino 1 :" Width="90%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 10%">
                                                            <asp:TextBox name="txtVolDespachadoFacTransFe" ID="txtVolDespachadoFacTransFe" runat="server" Width="90%" value="0" onkeypress="return numbersonly(event);"></asp:TextBox>

                                                        </td>
                                                        <td style="width: 10%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblVolRecibidoFacTransFe" ClientInstanceName="lblVolRecibidoFacTransFe" runat="server" Font-Bold="True" ForeColor="#666666" Text="Volumen Destino 2 :" Width="90%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 15%">
                                                            <asp:TextBox name="txtVolRecibidoFacTransFe" ID="txtVolRecibidoFacTransFe" runat="server" Width="90%" value="0" onkeypress="return numbersonly(event);"></asp:TextBox>

                                                        </td>
                                                        <td style="width: 10%; text-align: right;"></td>
                                                        <td style="width: 10%;"></td>
                                                        <td style="width: 8%; text-align: right;"></td>
                                                        <td style="width: 10%"></td>
                                                        <td style="width: 10%; text-align: right;"></td>
                                                        <td style="width: 7%;"></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 10%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblObsFacTransFe" ClientInstanceName="lblObsFacTransFe" runat="server" Font-Bold="True" ForeColor="#666666" Text="Observaciones :" Width="90%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td colspan="9">
                                                            <dx:ASPxTextBox ID="txtObsFacTransFe" runat="server" MaxLength="1500" Width="90%" ClientInstanceName="txtObsFacTransFe">
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </dx:PanelContent>
                                        </PanelCollection>
                                    </dx:ASPxRoundPanel>

                                    <!--LMQC PANEL DE FACTURAS POR COSTOS DE ALMACENAJE-->
                                    <dx:ASPxRoundPanel runat="server" ID="pnlFacturascostoAlmacenaje" Visible="false" Width="100%" HeaderText="Facturas Costo de Almacenaje" ShowHeader="true">
                                        <PanelCollection>
                                            <dx:PanelContent ID="PanelContent18" runat="server">
                                                <table style="width: 100%" border="0">
                                                    <tr>
                                                        <td colspan="10" style="text-align: center;">
                                                            <dx:ASPxLabel ID="ASPxLabel15" runat="server" Text="COSTO DE ALMACENAJE INTERMEDIO" Font-Bold="true" Font-Underline="true"></dx:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 10%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblFacAlmacenajeIntermedio" ClientInstanceName="lblFacAlmacenajeIntermedio" runat="server" Font-Bold="True" ForeColor="#666666" Text="Nº Factura Almacenaje :" Width="90%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 10%">
                                                            <dx:ASPxTextBox ID="txtFacAlmacenajeIntermedio" ClientInstanceName="txtFacAlmacenajeIntermedio" runat="server" Width="90%" Text="0">
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                        <td style="width: 10%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblEmpresaFacAlmacenajeIntermedio" ClientInstanceName="lblEmpresaFacAlmacenajeIntermedio" runat="server" Font-Bold="True" ForeColor="#666666" Text="Empresa Almacenaje :" Width="90%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 15%">
                                                            <dx:ASPxTextBox ID="txtEmpresaFacAlmacenajeIntermedio" ClientInstanceName="txtEmpresaFacAlmacenajeIntermedio" runat="server" Width="90%">
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                        <td style="width: 10%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblCostoUniFacAlmacenajeIntermedio" ClientInstanceName="lblCostoUniFacAlmacenajeIntermedio" runat="server" Font-Bold="True" ForeColor="#666666" Text="Tarifa Unitaria :" Width="90%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 10%;">
                                                            <asp:TextBox name="txtCostoUniFacAlmacenajeIntermedio" ID="txtCostoUniFacAlmacenajeIntermedio" runat="server" Width="90%" value="0" onkeypress="return numbersonly(event);"></asp:TextBox>

                                                        </td>
                                                        <td style="width: 8%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblSecFacAlmacenajeIntermedio" ClientInstanceName="lblSecFacAlmacenajeIntermedio" runat="server" Font-Bold="True" ForeColor="#666666" Text="Secuencia Factura:" Width="90%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 12%">
                                                            <dx:ASPxTextBox ID="txtSecFacAlmacenajeIntermedio" ClientInstanceName="txtSecFacAlmacenajeIntermedio" runat="server" Width="90%" Text="0">
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                        <td style="width: 10%; text-align: right;"></td>
                                                        <td style="width: 5%;"></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 10%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblVolRecibidoFacAlmacenajeIntermedio" ClientInstanceName="lblVolRecibidoFacAlmacenajeIntermedio" runat="server" Font-Bold="True" ForeColor="#666666" Text="Volumen Recibido :" Width="90%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 10%">
                                                            <asp:TextBox name="txtVolRecibidoFacAlmacenajeIntermedio" ID="txtVolRecibidoFacAlmacenajeIntermedio" runat="server" Width="90%" value="0" onkeypress="return numbersonly(event);"></asp:TextBox>

                                                        </td>
                                                        <td style="width: 10%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblObsFacAlmacenajeIntermedio" ClientInstanceName="lblObsFacAlmacenajeIntermedio" runat="server" Font-Bold="True" ForeColor="#666666" Text="Observaciones :" Width="90%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td colspan="7">
                                                            <dx:ASPxTextBox ID="txtObsFacAlmacenajeIntermedio" runat="server" MaxLength="1500" Width="90%" ClientInstanceName="txtObsFacAlmacenajeIntermedio">
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="10" style="text-align: center;">
                                                            <dx:ASPxLabel ID="ASPxLabel16" runat="server" Text="COSTO DE ALMACENAJE DESTINO FINAL" Font-Bold="true" Font-Underline="true"></dx:ASPxLabel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 10%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblFacAlmacenajeFinal" ClientInstanceName="lblFacAlmacenajeFinal" runat="server" Font-Bold="True" ForeColor="#666666" Text="Nº Factura Almacenaje :" Width="90%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 10%">
                                                            <dx:ASPxTextBox ID="txtFacAlmacenajeFinal" ClientInstanceName="txtFacAlmacenajeFinal" runat="server" Width="90%" Text="0">
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                        <td style="width: 10%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblEmpresaAlmacenajeFinal" ClientInstanceName="lblEmpresaAlmacenajeFinal" runat="server" Font-Bold="True" ForeColor="#666666" Text="Empresa Almacenaje :" Width="90%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 15%">
                                                            <dx:ASPxTextBox ID="txtEmpresaAlmacenajeFinal" ClientInstanceName="txtEmpresaAlmacenajeFinal" runat="server" Width="90%">
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                        <td style="width: 10%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblCostoUniFacAlmacenajeFinal" ClientInstanceName="lblCostoUniFacAlmacenajeFinal" runat="server" Font-Bold="True" ForeColor="#666666" Text="Tarifa Unitaria :" Width="90%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 10%;">
                                                            <asp:TextBox name="txtCostoUniFacAlmacenajeFinal" ID="txtCostoUniFacAlmacenajeFinal" runat="server" Width="90%" value="0" onkeypress="return numbersonly(event);"></asp:TextBox>

                                                        </td>
                                                        <td style="width: 8%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblSecFacAlmacenajeFinal" ClientInstanceName="lblSecFacAlmacenajeFinal" runat="server" Font-Bold="True" ForeColor="#666666" Text="Secuencia Factura:" Width="90%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 12%">
                                                            <dx:ASPxTextBox ID="txtSecFacAlmacenajeFinal" ClientInstanceName="txtSecFacAlmacenajeFinal" runat="server" Width="90%" Text="0">
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                        <td style="width: 10%; text-align: right;"></td>
                                                        <td style="width: 5%;"></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 10%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblVolRecibidoFacAlmacenajeFinal" ClientInstanceName="lblVolRecibidoFacAlmacenajeFinal" runat="server" Font-Bold="True" ForeColor="#666666" Text="Volumen Recibido :" Width="90%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 10%">
                                                            <asp:TextBox name="txtVolRecibidoFacAlmacenajeFinal" ID="txtVolRecibidoFacAlmacenajeFinal" runat="server" Width="90%" value="0" onkeypress="return numbersonly(event);"></asp:TextBox>

                                                        </td>
                                                        <td style="width: 10%; text-align: right;">
                                                            <dx:ASPxLabel ID="lblObsFacAlmacenajeFinal" ClientInstanceName="lblObsFacAlmacenajeFinal" runat="server" Font-Bold="True" ForeColor="#666666" Text="Observaciones :" Width="90%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td colspan="7">
                                                            <dx:ASPxTextBox ID="txtObsFacAlmacenajeFinal" runat="server" MaxLength="1500" Width="90%" ClientInstanceName="txtObsFacAlmacenajeFinal">
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </dx:PanelContent>
                                        </PanelCollection>
                                    </dx:ASPxRoundPanel>

                                    <!--LMQC PANEL DE FACTURAS POR COSTOS DE SEGURO-->
                                    <dx:ASPxRoundPanel runat="server" ID="pnlFacturasCostoSeguro" Visible="false" Width="100%" HeaderText="Facturas Costo de Seguro" ShowHeader="true">
                                        <PanelCollection>
                                            <dx:PanelContent ID="PanelContent19" runat="server">
                                                <table style="width: 100%" border="0">
                                                    <tr>
                                                        <td style="width: 10%; text-align: right;">
                                                            <dx:ASPxLabel ID="ASPxLabel33" runat="server" Font-Bold="True" ForeColor="#666666" Text="Nº Factura Aseguradora :" Width="150px">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 10%">
                                                            <dx:ASPxTextBox ID="txtNumFacSeguro" ClientInstanceName="txtNumFacSeguro" runat="server" Width="260px" Text="0">
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                        <td style="width: 10%; text-align: right;">
                                                            <dx:ASPxLabel ID="ASPxLabel30" runat="server" Font-Bold="True" ForeColor="#666666" Text="Empresa Aseguradora :" Width="150px">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 15%">
                                                            <dx:ASPxTextBox ID="txtEmpAseguradoraFacSeguro" ClientInstanceName="txtEmpAseguradoraFacSeguro" runat="server" Width="260px">
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                        <td style="width: 10%; text-align: right;">
                                                            <dx:ASPxLabel ID="ASPxLabel34" runat="server" Font-Bold="True" ForeColor="#666666" Text="Monto Facturado :" Width="150px">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 10%;">
                                                            <asp:TextBox name="txtMontoFacSeguro" ID="txtMontoFacSeguro" runat="server" Width="90%" value="0" onkeypress="return numbersonly(event);"></asp:TextBox>

                                                        </td>
                                                        <td style="width: 8%; text-align: right;"></td>
                                                        <td style="width: 12%"></td>
                                                        <td style="width: 10%; text-align: right;"></td>
                                                        <td style="width: 5%;"></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 10%; text-align: right;">
                                                            <dx:ASPxLabel ID="ASPxLabel31" runat="server" Font-Bold="True" ForeColor="#666666" Text="Observaciones :" Width="150px">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td colspan="9">
                                                            <dx:ASPxTextBox ID="txtObsFacSeguro" runat="server" MaxLength="1500" Width="100%" ClientInstanceName="txtObsFacSeguro">
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </dx:PanelContent>
                                        </PanelCollection>
                                    </dx:ASPxRoundPanel>

                                    <!--LMQC PANEL DE FACTURAS POR COSTOS DE ADUANERO-->
                                    <dx:ASPxRoundPanel runat="server" ID="pnlFacturasCostoAduanero" Visible="false" Width="100%" HeaderText="Facturas Costo Aduanero" ShowHeader="true">
                                        <PanelCollection>
                                            <dx:PanelContent ID="PanelContent20" runat="server">
                                                <table style="width: 100%" border="0">
                                                    <tr>
                                                        <td style="width: 10%; text-align: right;">
                                                            <dx:ASPxLabel ID="ASPxLabel27" runat="server" Font-Bold="True" ForeColor="#666666" Text="Gramaven Arancelaro :" Width="90%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 15%">
                                                            <asp:TextBox name="txtGravamenFacAduana" ID="txtGravamenFacAduana" runat="server" Width="90%" value="0" onkeypress="return numbersonly(event);"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 10%; text-align: right;">
                                                            <dx:ASPxLabel ID="ASPxLabel28" runat="server" Font-Bold="True" ForeColor="#666666" Text="SIDUINEA :" Width="90%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 15%">
                                                            <asp:TextBox name="txtSiduineaFacAduana" ID="txtSiduineaFacAduana" runat="server" Width="90%" value="0" onkeypress="return numbersonly(event);"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 20%; text-align: right;">
                                                            <dx:ASPxLabel ID="ASPxLabel55" runat="server" Font-Bold="True" ForeColor="#666666" Text="Costo Deposito Arancelario :" Width="90%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 15%;">
                                                            <asp:TextBox name="txtDepArancelarioFacAduana" ID="txtDepArancelarioFacAduana" runat="server" Width="90%" value="0" onkeypress="return numbersonly(event);"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 10%; text-align: right;"></td>
                                                        <td style="width: 5%;"></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 10%">
                                                            <dx:ASPxLabel ID="ASPxLabel56" runat="server" Font-Bold="True" ForeColor="#666666" Text="Observaciones :" Width="90%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td colspan="7">
                                                            <dx:ASPxTextBox ID="txtObsFacAduana" ClientInstanceName="txtObsFacAduana" runat="server" MaxLength="1500" Width="90%">
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </dx:PanelContent>
                                        </PanelCollection>
                                    </dx:ASPxRoundPanel>

                                    <!--LMQC PANEL DE VOLUMEN DE GASOLINA BASE-->
                                    <dx:ASPxRoundPanel runat="server" ID="pnlFacturasGasolinaBase" ClientInstanceName="pnlFacturasGasolinaBase" Visible="false" Width="100%" HeaderText="Facturas Costo Aduanero" ShowHeader="true">
                                        <PanelCollection>
                                            <dx:PanelContent ID="PanelContent26" runat="server">
                                                <table style="width: 100%" border="0">
                                                    <tr>
                                                        <td style="width: 10%; text-align: right;">
                                                            <dx:ASPxLabel ID="ASPxLabel17" runat="server" Font-Bold="True" ForeColor="#666666" Text="VI&A (lt) :" Width="90%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 15%">
                                                            <asp:TextBox name="txtVolumenIyA" ID="txtVolumenIyA" runat="server" Width="90%" value="0" onkeypress="return numbersonly(event);"></asp:TextBox>

                                                        </td>
                                                        <td style="width: 10%; text-align: right;">
                                                            <dx:ASPxLabel ID="ASPxLabel18" runat="server" Font-Bold="True" ForeColor="#666666" Text="VGB (lt):" Width="90%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td style="width: 15%">
                                                            <asp:TextBox name="txtVolumenGb" ID="txtVolumenGb" runat="server" Width="90%" value="0" onkeypress="return numbersonly(event);"></asp:TextBox>

                                                        </td>
                                                        <td style="width: 20%; text-align: right;"></td>
                                                        <td style="width: 15%;"></td>
                                                        <td style="width: 10%; text-align: right;"></td>
                                                        <td style="width: 5%;"></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 10%">
                                                            <dx:ASPxLabel ID="ASPxLabel20" runat="server" Font-Bold="True" ForeColor="#666666" Text="Planta :" Width="90%">
                                                            </dx:ASPxLabel>
                                                        </td>
                                                        <td colspan="7">
                                                            <dx:ASPxTextBox ID="txtPlantaV" ClientInstanceName="txtPlantaV" runat="server" MaxLength="1500" Width="90%">
                                                                <ReadOnlyStyle BackColor="#DEE8DB">
                                                                </ReadOnlyStyle>
                                                                <FocusedStyle BackColor="#FFFFCC">
                                                                </FocusedStyle>
                                                                <DisabledStyle BackColor="#DEE8DB">
                                                                </DisabledStyle>
                                                            </dx:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </dx:PanelContent>
                                        </PanelCollection>
                                    </dx:ASPxRoundPanel>

                                    <!--LMQC PANEL DE TRANSPORTE CISTERNA MASIVO-->
                                    <dx:ASPxRoundPanel runat="server" ID="pnlFacturasTransMasivo" ClientInstanceName="pnlFacturasTransMasivo" Visible="false" Width="100%" HeaderText="Facturas Costo Transporte Cisternas Masivo" ShowHeader="true">
                                        <PanelCollection>
                                            <dx:PanelContent ID="PanelContent27" runat="server">
                                                <table style="width: 100%" border="0">
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxUploadControl runat="server" ID="upcArchivosRA" ClientInstanceName="upcArchivosRA" Width="100%"
                                                                AutoStartUpload="True" ShowProgressPanel="True" ShowTextBox="False" BrowseButton-Text="Add documents"
                                                                UploadMode="Auto" dropzoneid="DropZone" CssClass="fa-upload" OnFilesUploadComplete="upcArchivosRA_FilesUploadComplete" Height="50px" Theme="SoftOrange">
                                                                <BrowseButton Text="AGREGAR ARCHIVO">
                                                                </BrowseButton>
                                                                <CancelButton Text="CANCELAR">
                                                                </CancelButton>
                                                                <AdvancedModeSettings
                                                                    EnableMultiSelect="false" EnableDragAndDrop="true" ExternalDropZoneID="dropZone">
                                                                </AdvancedModeSettings>
                                                                <ValidationSettings
                                                                    AllowedFileExtensions=".csv"
                                                                    MaxFileSize="500000000" MultiSelectionErrorText="Atención! 

    {0} Los archivos no son válidos y no serán cargados. Las posibles razones son: exceden el tamaño de archivo permitido ({1}), sus extensiones no están permitidas o sus nombres de archivo contienen caracteres no válidos. Estos archivos han sido eliminados de la selección: 

    {2}">
                                                                </ValidationSettings>
                                                                <ClientSideEvents
                                                                    FilesUploadComplete="function(s, e) {pnlFacTransMasImmAdd.PerformCallback();}" />
                                                            </dx:ASPxUploadControl>

                                                            <dx:ASPxButton ID="btnDescargar" runat="server" Font-Bold="True" ForeColor="Maroon" OnClick="btnDescargar_Click" Text="Descargar Plantilla" Theme="Metropolis" Width="160px" ToolTip="Descarga la plantilla del tipo de operación seleccionado">
                                                                <Image IconID="actions_download_16x16">
                                                                </Image>
                                                            </dx:ASPxButton>

                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxCallbackPanel ID="pnlFacTransMasImmAdd" runat="server" Width="100%" OnCallback="pnlFacTransMasImmAdd_Callback" ClientInstanceName="pnlFacTransMasImmAdd">
                                                                <PanelCollection>
                                                                    <dx:PanelContent ID="PanelContent29" runat="server">
                                                                        <table>
                                                                            <tr>
                                                                                <td colspan="6">
                                                                                    <dx:ASPxGridView ID="dgvTransCisMas" runat="server" Theme="Default" AutoGenerateColumns="False" KeyFieldName="LngId" EnableTheming="True" Font-Size="Smaller" ClientInstanceName="dgvTransCisMas" Width="100%">
                                                                                        <SettingsContextMenu EnableRowMenu="False">
                                                                                        </SettingsContextMenu>
                                                                                        <Settings ShowHeaderFilterButton="True" />
                                                                                        <SettingsBehavior AllowFocusedRow="True" />
                                                                                        <Columns>
                                                                                            <dx:GridViewDataTextColumn ShowInCustomizationForm="True" VisibleIndex="0" Caption="#" FieldName="LngId" ToolTip="Funciones">
                                                                                                <Settings AutoFilterCondition="Contains" />
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="Secuencia" FieldName="DecSecuencia" ShowInCustomizationForm="True" VisibleIndex="1">
                                                                                                <Settings AutoFilterCondition="Contains" />
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="FECHA DUI" FieldName="StrFechaDui" ShowInCustomizationForm="True" VisibleIndex="2">
                                                                                                <Settings AutoFilterCondition="Equals" />
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="DUI" FieldName="StrDui" ShowInCustomizationForm="True" VisibleIndex="3">
                                                                                                <Settings AutoFilterCondition="Contains" />
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="Nº Fact. Proveedor" FieldName="StrNroFactProveedor" ShowInCustomizationForm="True" VisibleIndex="4">
                                                                                                <Settings AutoFilterCondition="Contains" />
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="Proveedor" FieldName="StrProveedor" ShowInCustomizationForm="True" VisibleIndex="5">
                                                                                                <Settings AutoFilterCondition="Contains" />
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="Empresa Transporte" FieldName="StrEmpresa" ShowInCustomizationForm="True" VisibleIndex="6">
                                                                                                <Settings AutoFilterCondition="Contains" />
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="Nº Fact. Transporte" FieldName="StrFactTransporte" ShowInCustomizationForm="True" VisibleIndex="7">
                                                                                                <Settings AutoFilterCondition="Contains" />
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="Costo Unitario" FieldName="StrCostoUnitario" ShowInCustomizationForm="True" VisibleIndex="8">
                                                                                                <Settings AutoFilterCondition="Contains" />
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="Flete Transporte" FieldName="StrFleteTransporte" ShowInCustomizationForm="True" VisibleIndex="9">
                                                                                                <Settings AutoFilterCondition="Contains" />
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="Planta Despacho" FieldName="StrPlantaDespacho" ShowInCustomizationForm="True" VisibleIndex="10">
                                                                                                <Settings AutoFilterCondition="Contains" />
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="Planta Recepción" FieldName="StrPlantaRecepcion" ShowInCustomizationForm="True" VisibleIndex="11">
                                                                                                <Settings AutoFilterCondition="Contains" />
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="Volumen Despachado" FieldName="DecVolumenDespachado" ShowInCustomizationForm="True" VisibleIndex="12">
                                                                                                <Settings AutoFilterCondition="Contains" />
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="Volumen Recibido" FieldName="DecVolumenRecibido" ShowInCustomizationForm="True" VisibleIndex="13">
                                                                                                <Settings AutoFilterCondition="Contains" />
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="Observaciones" FieldName="StrObservacion" ShowInCustomizationForm="True" VisibleIndex="14">
                                                                                                <Settings AutoFilterCondition="Contains" />
                                                                                            </dx:GridViewDataTextColumn>
                                                                                        </Columns>
                                                                                        <Styles CssPostfix="DevGlp">
                                                                                            <AlternatingRow BackColor="#EEEEEE">
                                                                                            </AlternatingRow>
                                                                                        </Styles>
                                                                                    </dx:ASPxGridView>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>&nbsp;</td>
                                                                                <td>&nbsp;</td>
                                                                                <td>&nbsp;</td>
                                                                                <td>&nbsp;</td>
                                                                                <td>&nbsp;</td>
                                                                                <td>
                                                                                    <dx:ASPxTextBox runat="server" ID="txtErrorMasivo" ClientInstanceName="txtErrorMasivo" ClientVisible="false"></dx:ASPxTextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="3">
                                                                                    <asp:Label ID="lblTipoError" runat="server" Font-Bold="True" ForeColor="Maroon" Width="133%">Registros Observados</asp:Label>
                                                                                </td>

                                                                                <td>&nbsp;</td>
                                                                                <td>&nbsp;</td>
                                                                                <td>&nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="6">
                                                                                    <dx:ASPxGridView ID="dgvTransaccionError" runat="server" AutoGenerateColumns="False" ClientInstanceName="dgvTransaccionError" EnableTheming="True" Font-Size="Smaller" OnHtmlRowPrepared="dgvTransaccionError_HtmlRowPrepared" Theme="Default" Width="100%" KeyFieldName="LngId">
                                                                                        <SettingsContextMenu EnableRowMenu="False">
                                                                                        </SettingsContextMenu>
                                                                                        <Settings ShowHeaderFilterButton="True" />
                                                                                        <SettingsBehavior AllowFocusedRow="True" />
                                                                                        <Columns>
                                                                                            <dx:GridViewDataTextColumn Caption="ID MOV." FieldName="StrIdVenta" ShowInCustomizationForm="True" VisibleIndex="2" ToolTip="Identificador del Movimiento">
                                                                                                <Settings AutoFilterCondition="Contains" />
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="ERROR" FieldName="StrErrores" ShowInCustomizationForm="True" VisibleIndex="11">
                                                                                                <Settings AutoFilterCondition="Contains" />
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="NIT" FieldName="LngNit" ShowInCustomizationForm="True" VisibleIndex="5">
                                                                                                <Settings AutoFilterCondition="Contains" />
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="FECHA" FieldName="LngFechaEmision" ShowInCustomizationForm="True" VisibleIndex="4">
                                                                                                <Settings AutoFilterCondition="Contains" />
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="NO. FACTURA" FieldName="LngNroFactura" ShowInCustomizationForm="True" VisibleIndex="9" ToolTip="Número de Factura">
                                                                                                <Settings AutoFilterCondition="Contains" />
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="NO. AUTORIZACION" FieldName="LngNroAutorizacion" ShowInCustomizationForm="True" ToolTip="Número de Autorización" VisibleIndex="7">
                                                                                                <Settings AutoFilterCondition="Contains" />
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="NO. REF." FieldName="LngNroReferencia" ShowInCustomizationForm="True" VisibleIndex="10" ToolTip="Número de Referencia">
                                                                                                <Settings AutoFilterCondition="Equals" />
                                                                                            </dx:GridViewDataTextColumn>
                                                                                            <dx:GridViewDataTextColumn Caption="#" FieldName="LngId" ShowInCustomizationForm="True" VisibleIndex="1" ToolTip="Funciones">
                                                                                            </dx:GridViewDataTextColumn>
                                                                                        </Columns>
                                                                                    </dx:ASPxGridView>
                                                                                </td>
                                                                            </tr>
                                                                        </table>

                                                                    </dx:PanelContent>
                                                                </PanelCollection>
                                                            </dx:ASPxCallbackPanel>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </dx:PanelContent>
                                        </PanelCollection>
                                    </dx:ASPxRoundPanel>

                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxCallbackPanel>

                        <br />
                        <table>
                            <tr>
                                <td style="width: 25%; text-align: right;">&nbsp;</td>
                                <td style="width: 25%;">
                                    <dx:ASPxTextBox ID="txtValidacionDatos" runat="server" ClientInstanceName="txtValidacionDatos" Text="0" Width="170px" Font-Overline="False" Font-Size="XX-Small" ForeColor="White">
                                        <ReadOnlyStyle ForeColor="White">
                                        </ReadOnlyStyle>
                                        <FocusedStyle BackColor="White">
                                        </FocusedStyle>
                                        <Border BorderStyle="None" />
                                        <BorderBottom BorderStyle="None" />
                                    </dx:ASPxTextBox>
                                </td>
                                <td style="width: 25%; text-align: right;"></td>
                                <td style="width: 25%">
                                    <dx:ASPxButton ID="btnActualizarDocumentoImpAdd" runat="server" AutoPostBack="False" Text="Agregar Documento" Theme="Metropolis" EnableTheming="True" Width="100%" Font-Bold="True" ForeColor="Maroon">
                                        <ClientSideEvents Click="function(s, e) {
	                                                                                    onClickRegistrarFacturas();
                                                                                    }" />
                                        <Image IconID="reports_addgroupheader_16x16">
                                        </Image>
                                    </dx:ASPxButton>
                                </td>
                            </tr>
                        </table>
                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxPanel>
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>


<dx:ASPxPopupControl ID="pucDetalleNotaDespachoYpfb" runat="server" CloseAction="CloseButton" CloseOnEscape="True" Modal="True"
    PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Above" ClientInstanceName="pucDetalleNotaDespachoYpfb"
    HeaderText="NOTA DE ENTREGA DE PRODUCTO/DECLARACION JURADA" AllowDragging="True" PopupAnimationType="None" EnableViewState="False" Maximized="True" ShowMaximizeButton="True" Theme="Metropolis" ScrollBars="Auto" AllowResize="True">
    <HeaderImage IconID="grid_horizontalgridlinesmajorminor_32x32">
    </HeaderImage>

    <HeaderStyle>
        <BorderBottom BorderStyle="Solid" />
    </HeaderStyle>
    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl9" runat="server">
            <dx:ASPxCallbackPanel ID="pnlTransaccionNotaDespacho" runat="server" DefaultButton="btOK" ClientInstanceName="pnlTransaccionNotaDespacho" OnCallback="pnlTransaccionNotaDespacho_Callback">
                <ClientSideEvents BeginCallback="function(s, e) {
	LoadingPanel.Show();
}"
                    EndCallback="function(s, e) {
	LoadingPanel.Hide();
}" />
                <PanelCollection>
                    <dx:PanelContent ID="PanelContent22" runat="server">
                        <hr class="hr-text" data-content="...">
                        <table border="0" style="width: 100%; border-spacing: 2px">
                            <tr>
                                <td style="width: 10%; text-align: right;">
                                    <dx:ASPxLabel ID="ASPxLabel80" runat="server" Font-Bold="True" ForeColor="#666666" Text="Nº Documento : " Width="100%">
                                    </dx:ASPxLabel>
                                </td>
                                <td colspan="3">
                                    <dx:ASPxTextBox runat="server" Width="93%" ID="txtDocumentoNotaDespacho" ReadOnly="true"></dx:ASPxTextBox>
                                </td>
                                <td style="width: 10%; text-align: right;">
                                    <dx:ASPxLabel ID="ASPxLabel93" runat="server" Font-Bold="True" ForeColor="#666666" Text="Fecha Suministro : " Width="100%">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 40%;">
                                    <dx:ASPxTextBox runat="server" Width="50%" ID="txtFechaSuministroNotaDespacho" ReadOnly="true"></dx:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 10%; text-align: right;">
                                    <dx:ASPxLabel ID="ASPxLabel112" runat="server" Font-Bold="True" ForeColor="#666666" Text="Operador : " Width="100%">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 20%;">
                                    <dx:ASPxTextBox runat="server" Width="90%" ID="txtOperadorNotaDespacho"></dx:ASPxTextBox>
                                </td>
                                <td style="width: 6%; text-align: right;">
                                    <dx:ASPxLabel ID="ASPxLabel113" runat="server" Font-Bold="True" ForeColor="#666666" Text="NIT : " Width="100%">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 14%;">
                                    <dx:ASPxTextBox runat="server" Width="80%" ID="txtNitOperadorNotaDespacho" ReadOnly="true"></dx:ASPxTextBox>
                                </td>
                                <td style="width: 10%; text-align: right;">
                                    <dx:ASPxLabel ID="ASPxLabel114" runat="server" Font-Bold="True" ForeColor="#666666" Text="Planta : " Width="100%">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 40%;">
                                    <dx:ASPxTextBox runat="server" Width="90%" ID="txtPlantaNotaDespacho" ReadOnly="true"></dx:ASPxTextBox>
                                </td>
                            </tr>
                        </table>
                        <hr class="hr-text" data-content="...">
                        <table border="0" style="width: 100%; border-spacing: 2px">
                            <tr>
                                <td style="width: 10%; text-align: right;">
                                    <dx:ASPxLabel ID="ASPxLabel115" runat="server" Font-Bold="True" ForeColor="#666666" Text="Matricula : ">
                                    </dx:ASPxLabel>
                                </td>
                                <td style="width: 10%;">
                                    <dx:ASPxTextBox runat="server" ID="txtMatriculaNotaDespacho"></dx:ASPxTextBox>
                                </td>
                                <td style="width: 10%;">
                                    <dx:ASPxButton runat="server" AutoPostBack="false" Text="Buscar" ID="btnBuscarIdentificadorNotaDespacho">
                                        <ClientSideEvents Click="function(s, e) {
pnlDatoAeronave.PerformCallback();   
}" />
                                    </dx:ASPxButton>
                                </td>
                                <td style="width: 70%;"></td>
                            </tr>
                        </table>
                        <hr class="hr-text" data-content="...">
                        <dx:ASPxCallbackPanel ID="pnlDatoAeronave" runat="server" DefaultButton="btOK" ClientInstanceName="pnlDatoAeronave" OnCallback="pnlDatoAeronave_Callback">
                            <PanelCollection>
                                <dx:PanelContent ID="PanelContent23" runat="server">
                                    <table border="0" style="width: 100%; border-spacing: 5px">
                                        <tr>
                                            <td style="width: 10%; text-align: right;">
                                                <dx:ASPxLabel ID="ASPxLabel118" runat="server" Font-Bold="True" ForeColor="#666666" Text="Nº Tarjeta : ">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td style="width: 20%;">
                                                <dx:ASPxTextBox runat="server" Width="90%" ID="txtBarcodeNotaDespacho" ReadOnly="true"></dx:ASPxTextBox>
                                            </td>
                                            <td style="width: 10%; text-align: right;">
                                                <dx:ASPxLabel ID="ASPxLabel119" runat="server" Font-Bold="True" ForeColor="#666666" Text="Modelo : ">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td style="width: 20%;">
                                                <dx:ASPxTextBox runat="server" Width="90%" ID="txtModeloNotaDespacho" ReadOnly="true"></dx:ASPxTextBox>
                                            </td>
                                            <td style="width: 5%; text-align: right;">
                                                <dx:ASPxLabel ID="ASPxLabel120" runat="server" Font-Bold="True" ForeColor="#666666" Text="Nac. : ">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td style="width: 5%;">
                                                <dx:ASPxRadioButton runat="server" ID="rbNalNotaDespacho" GroupName="TipoVuelo"></dx:ASPxRadioButton>
                                            </td>
                                            <td style="width: 5%; text-align: right;">
                                                <dx:ASPxLabel ID="ASPxLabel121" runat="server" Font-Bold="True" ForeColor="#666666" Text="Int. : ">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td style="width: 5%;">
                                                <dx:ASPxRadioButton runat="server" ID="rbIntNotaDespacho" GroupName="TipoVuelo"></dx:ASPxRadioButton>
                                            </td>
                                            <td style="width: 6%; text-align: right;">
                                                <dx:ASPxLabel ID="ASPxLabel122" runat="server" Font-Bold="True" ForeColor="#666666" Text="NIT : ">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td style="width: 14%;">
                                                <dx:ASPxTextBox runat="server" Width="69%" ID="txtNitNotaDespacho"></dx:ASPxTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10%; text-align: right;">
                                                <dx:ASPxLabel ID="ASPxLabel116" runat="server" Font-Bold="True" ForeColor="#666666" Text="Razon Social : ">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td colspan="9">
                                                <dx:ASPxTextBox runat="server" Width="95%" ID="txtRazonSocialNotaDespacho"></dx:ASPxTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10%; text-align: right;">
                                                <dx:ASPxLabel ID="ASPxLabel117" runat="server" Font-Bold="True" ForeColor="#666666" Text="Encargado de PSCAA : ">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td colspan="9">
                                                <dx:ASPxTextBox runat="server" Width="95%" ID="txtEncargadoNotaDespacho"></dx:ASPxTextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxCallbackPanel>
                        <hr class="hr-text" data-content="...">
                        <table border="1" style="width: 100%;">
                            <tr>
                                <td style="background-color: #C0C0C0; text-align: center; width: 13%">VENTA</td>
                                <td style="background-color: #C0C0C0; text-align: center; width: 13%">REFUELLER</td>
                                <td style="background-color: #C0C0C0; text-align: center; width: 13%">INICIAL</td>
                                <td style="background-color: #C0C0C0; text-align: center; width: 13%">FINAL</td>
                                <td style="background-color: #C0C0C0; text-align: center; width: 13%">PRODUCTO</td>
                                <td style="background-color: #C0C0C0; text-align: center; width: 12%">CANTIDAD</td>
                                <td style="background-color: #C0C0C0; text-align: center; width: 11%">PRECIO UNITARIO</td>
                                <td style="background-color: #C0C0C0; text-align: center; width: 12%">TOTAL Bs.</td>
                            </tr>
                            <tr>
                                <td style="width: 13%">
                                    <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Width="100%" Text="PRECIO NACIONAL" ReadOnly="true">
                                    </dx:ASPxTextBox>
                                </td>
                                <td style="width: 13%">
                                    <dx:ASPxTextBox runat="server" Width="100%" ID="txtRefuellerNotaDespachoN" ReadOnly="true">
                                    </dx:ASPxTextBox>
                                </td>
                                <td style="width: 13%">
                                    <dx:ASPxTextBox runat="server" Width="100%" ID="txtValorInicialNal">
                                    </dx:ASPxTextBox>
                                </td>
                                <td style="width: 13%">
                                    <dx:ASPxTextBox runat="server" Width="100%" ID="txtValorFinalNal">
                                    </dx:ASPxTextBox>
                                </td>
                                <td style="width: 13%">
                                    <dx:ASPxTextBox runat="server" Width="100%" ID="txtProductoNacionalDespacho" ReadOnly="true">
                                    </dx:ASPxTextBox>
                                </td>
                                <td style="width: 13%">
                                    <dx:ASPxTextBox runat="server" Width="100%" ID="txtCantidadNalNotaDespacho" ClientInstanceName="txtCantidadNalNotaDespacho">
                                        <ClientSideEvents LostFocus="function(s, e) {
                                                            //if(parseFloat(txtPreTotalFacProducto.GetText(), 10)==0)
                                                            //{
                                                            var num1 = parseFloat(txtCantidadNalNotaDespacho.GetText(), 10);
                                                            var num2 = parseFloat(txtPrecioUnitarioNacional.GetText(), 10);
                                                            txtPrecioTotalNal.SetText((num1*num2).toFixed(2));
                                                            //}
                                                        }" />
                                    </dx:ASPxTextBox>
                                </td>
                                <td style="width: 13%">
                                    <dx:ASPxTextBox runat="server" Width="100%" ID="txtPrecioUnitarioNacional" ClientInstanceName="txtPrecioUnitarioNacional" ReadOnly="true">
                                    </dx:ASPxTextBox>
                                </td>
                                <td style="width: 13%">
                                    <dx:ASPxTextBox runat="server" Width="100%" ID="txtPrecioTotalNal" ClientInstanceName="txtPrecioTotalNal" ReadOnly="true" Text="0">
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 13%">
                                    <dx:ASPxTextBox ID="ASPxTextBox2" runat="server" Width="100%" Text="PRECIO INTERNACIONAL" ReadOnly="true">
                                    </dx:ASPxTextBox>
                                </td>
                                <td style="width: 13%">
                                    <dx:ASPxTextBox runat="server" Width="100%" ID="txtRefuellerNotaDespachoI" ReadOnly="true">
                                    </dx:ASPxTextBox>
                                </td>
                                <td style="width: 13%">
                                    <dx:ASPxTextBox runat="server" Width="100%" ID="txtValorInicialInterNal">
                                    </dx:ASPxTextBox>
                                </td>
                                <td style="width: 13%">
                                    <dx:ASPxTextBox runat="server" Width="100%" ID="txtValorFinalInterNal">
                                    </dx:ASPxTextBox>
                                </td>
                                <td style="width: 13%">
                                    <dx:ASPxTextBox runat="server" Width="100%" ID="txtProductoInterNacionalDespacho" ReadOnly="true">
                                    </dx:ASPxTextBox>
                                </td>
                                <td style="width: 13%">
                                    <dx:ASPxTextBox runat="server" Width="100%" ID="txtCantidadInterNalNotaDespacho" ClientInstanceName="txtCantidadInterNalNotaDespacho">
                                        <ClientSideEvents LostFocus="function(s, e) {
                                                            //if(parseFloat(txtPreTotalFacProducto.GetText(), 10)==0)
                                                            //{
                                                            var num1 = parseFloat(txtCantidadInterNalNotaDespacho.GetText(), 10);
                                                            var num2 = parseFloat(txtPrecioUnitarioInternacional.GetText(), 10);
                                                            txtPrecioTotalInterNal.SetText((num1*num2).toFixed(2));
                                                            //}
                                                        }" />
                                    </dx:ASPxTextBox>
                                </td>
                                <td style="width: 13%">
                                    <dx:ASPxTextBox runat="server" Width="100%" ID="txtPrecioUnitarioInternacional" ReadOnly="true" ClientInstanceName="txtPrecioUnitarioInternacional">
                                    </dx:ASPxTextBox>
                                </td>
                                <td style="width: 13%">
                                    <dx:ASPxTextBox runat="server" Width="100%" ID="txtPrecioTotalInterNal" ReadOnly="true" ClientInstanceName="txtPrecioTotalInterNal" Text="0">
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                        </table>
                        <hr class="hr-text" data-content="...">
                        <table border="0" style="width: 100%;">
                            <tr>
                                <td style="width: 20%;"></td>                               
                                            <td style="width: 60%;">
                                                <table border="1" style="width: 100%;">
                                                    <tr>
                                                        <td colspan="4" style="background-color: #C0C0C0; text-align: center;">AEROPUERTO</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="background-color: #C0C0C0; text-align: center; width: 25%">ORIGEN</td>
                                                        <td style="background-color: #C0C0C0; text-align: center; width: 25%">DESTINO</td>
                                                        <td style="background-color: #C0C0C0; text-align: center; width: 25%">ALTERNO</td>
                                                        <td style="background-color: #C0C0C0; text-align: center; width: 25%">FECHA HORA SALIDA</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 25%">
                                                            <dx:ASPxComboBox ID="cmbOrigenNotaDespacho" runat="server" EnableTheming="True" TextFormatString="{0}" Theme="Metropolis" ValueField="ID_AEROPUERTO" Width="100%" ClientInstanceName="cmbOrigenNotaDespacho">
                                                                <Columns>
                                                                    <dx:ListBoxColumn Caption="ID_AEROPUERTO" FieldName="ID_AEROPUERTO" />
                                                                    <dx:ListBoxColumn Caption="NOMBRE" FieldName="NOMBRE" />
                                                                    <dx:ListBoxColumn Caption="PAIS" FieldName="PAIS" />
                                                                    <dx:ListBoxColumn Caption="CIUDAD" FieldName="CIUDAD" />
                                                                </Columns>
                                                                <ClearButton Visibility="Auto">
                                                                </ClearButton>
                                                            </dx:ASPxComboBox>
                                                        </td>
                                                        <td style="width: 25%">
                                                            <dx:ASPxComboBox ID="cmbDestinoNotaDespacho" runat="server" EnableTheming="True" TextFormatString="{0}" Theme="Metropolis" ValueField="ID_AEROPUERTO" Width="100%" ClientInstanceName="cmbDestinoNotaDespacho">
                                                                <Columns>
                                                                    <dx:ListBoxColumn Caption="ID_AEROPUERTO" FieldName="ID_AEROPUERTO" />
                                                                    <dx:ListBoxColumn Caption="NOMBRE" FieldName="NOMBRE" />
                                                                    <dx:ListBoxColumn Caption="PAIS" FieldName="PAIS" />
                                                                    <dx:ListBoxColumn Caption="CIUDAD" FieldName="CIUDAD" />
                                                                </Columns>
                                                                <ClearButton Visibility="Auto">
                                                                </ClearButton>
                                                            </dx:ASPxComboBox>
                                                        </td>
                                                        <td style="width: 25%">
                                                            <dx:ASPxComboBox ID="cmbAlternoNotaDespacho" runat="server" EnableTheming="True" TextFormatString="{0}" Theme="Metropolis" ValueField="ID_AEROPUERTO" Width="100%" ClientInstanceName="cmbAlternoNotaDespacho">
                                                                <Columns>
                                                                    <dx:ListBoxColumn Caption="ID_AEROPUERTO" FieldName="ID_AEROPUERTO" />
                                                                    <dx:ListBoxColumn Caption="NOMBRE" FieldName="NOMBRE" />
                                                                    <dx:ListBoxColumn Caption="PAIS" FieldName="PAIS" />
                                                                    <dx:ListBoxColumn Caption="CIUDAD" FieldName="CIUDAD" />
                                                                </Columns>
                                                                <ClearButton Visibility="Auto">
                                                                </ClearButton>
                                                            </dx:ASPxComboBox>
                                                        </td>
                                                        <td style="width: 25%">
                                                            <dx:ASPxDateEdit runat="server" ID="deFechaSalidaNotaDesopacho" DisplayFormatString="dd/MM/yyyy HH:mm:ss" EditFormatString="dd/MM/yyyy HH:mm:ss">
                                                                <TimeSectionProperties Visible="true"></TimeSectionProperties>
                                                            </dx:ASPxDateEdit>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                <td style="width: 20%;"></td>
                            </tr>
                        </table>
                        <hr class="hr-text" data-content="...">
                        <table border="1" style="width: 100%;">
                            <tr>
                                <td style="background-color: #C0C0C0; text-align: center; width: 100%">OTRAS CONSIDERACIONES SOBRE EL SUMINISTRO</td>
                            </tr>
                            <tr>
                                <td style="width: 100%; height: 100px">
                                    <dx:ASPxTextBox runat="server" Width="100%" Height="100%" ID="txtConsideracionesNotaDespacho"></dx:ASPxTextBox>
                                </td>
                            </tr>
                        </table>
                        <dx:ASPxTextBox runat="server" Width="100%" Height="100%" ID="txtIdHidro" ClientInstanceName="txtIdHidro" ClientVisible="false"></dx:ASPxTextBox>
                        <dx:ASPxButton runat="server" ID="btnRegistrarNotaDespacho" OnClick="btnRegistrarNotaDespacho_Click" Text="Guardar Nota Despacho">
                            <ClientSideEvents Click="function(s, e) {
	                                    pucDetalleNotaDespachoYpfb.Hide();
                                    }" />
                        </dx:ASPxButton>
                        <dx:ASPxTextBox runat="server" ID="txtIdHidrocarburo" Visible="false"></dx:ASPxTextBox>
                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxCallbackPanel>

        </dx:PopupControlContentControl>
    </ContentCollection>
    <ClientSideEvents Closing="function(s, e) {
	pnlDatos.PerformCallback();
}" />
    <ContentStyle>
        <Paddings PaddingBottom="5px" />
    </ContentStyle>
</dx:ASPxPopupControl>


<dx:ASPxPopupControl ID="pucConfig" runat="server" AllowDragging="True" Modal="true" ClientInstanceName="pucConfig" EnableTheming="True" HeaderText="Configuración" Height="105px" PopupVerticalAlign="WindowCenter" PopupHorizontalAlign="WindowCenter" OnWindowCallback="pucConfig_WindowCallback">
    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl8" runat="server">
            <asp:Panel ID="Panel1" runat="server">
                <table>
                    <tr>
                        <td>
                            <dx:ASPxComboBox ID="cmbHidrocarburo" runat="server" ClientInstanceName="cmbHidrocarburo" Font-Size="X-Small" TextField="NOMBRE" TextFormatString="{0}" ValueField="ID_PRODUCTO" Width="160px" Theme="Metropolis" ToolTip="Seleccione un producto">
                                <Columns>
                                    <dx:ListBoxColumn Caption="NOMBRE" FieldName="NOMBRE" Name="NOMBRE" />
                                    <dx:ListBoxColumn Caption="ID_PRODUCTO" FieldName="ID_PRODUCTO" Name="ID_PRODUCTO" Width="0px" />
                                </Columns>
                                <ClearButton Visibility="Auto">
                                </ClearButton>
                                <ValidationSettings>
                                    <RequiredField ErrorText="" />
                                </ValidationSettings>
                                <ReadOnlyStyle BackColor="#DEE8DB">
                                </ReadOnlyStyle>
                                <FocusedStyle BackColor="#FFFFCC">
                                </FocusedStyle>
                                <DisabledStyle BackColor="#DEE8DB">
                                </DisabledStyle>
                            </dx:ASPxComboBox>
                            <br />

                            <dx:ASPxComboBox ID="cmbUnidadMedida" runat="server" ClientInstanceName="cmbUnidadMedida" Font-Size="X-Small" TextField="NOMBRE" TextFormatString="{0}" ValueField="ID_CATALOGO" Width="160px" Theme="Metropolis" ToolTip="Seleccione una unidad de medida">
                                <Columns>
                                    <dx:ListBoxColumn Caption="NOMBRE" FieldName="NOMBRE" Name="NOMBRE" />
                                    <dx:ListBoxColumn Caption="ID_CATALOGO" FieldName="ID_CATALOGO" Name="ID_CATALOGO" Width="0px" />
                                    <dx:ListBoxColumn Caption="CODIGO" FieldName="CODIGO" Name="CODIGO" Width="0px" />
                                </Columns>
                                <ClearButton Visibility="Auto">
                                </ClearButton>
                                <ReadOnlyStyle BackColor="#DEE8DB">
                                </ReadOnlyStyle>
                                <FocusedStyle BackColor="#FFFFCC">
                                </FocusedStyle>
                                <DisabledStyle BackColor="#DEE8DB">
                                </DisabledStyle>
                            </dx:ASPxComboBox>
                            <br />
                            <dx:ASPxButton ID="btnAgregar" runat="server" ClientInstanceName="btnAgregar" AutoPostBack="False" ImagePosition="Right" Text="Agregar" Theme="Default" ToolTip="Agregar Producto" Width="100px">
                                <ClientSideEvents Click="function(s, e) {
	
	        dgvHidrocarburo.PerformCallback();
        }" />
                                <Image IconID="navigation_forward_16x16">
                                </Image>
                            </dx:ASPxButton>
                        </td>
                        <td>
                            <dx:ASPxGridView ID="dgvHidrocarburo" runat="server" AutoGenerateColumns="False" ClientInstanceName="dgvHidrocarburo" EnableTheming="True" Font-Size="Smaller" KeyFieldName="ID_IDENTIFICADOR" OnHtmlRowPrepared="dgvHidrocarburo_HtmlRowPrepared" Theme="Default" Width="100%" OnCustomButtonCallback="dgvHidrocarburo_CustomButtonCallback" OnCustomCallback="dgvHidrocarburo_CustomCallback">
                                <ClientSideEvents CustomButtonClick="function(s, e) {
                                switch(e.buttonID) {

                                case&nbsp; 'btnHidQuitar':
                                e.processOnServer = confirm('¿Desea quitar el registro del hidrocarburo?'); 
                                break;

                                } 
                                }" />
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
                                <SettingsPager NumericButtonCount="5" PageSize="50">
                                    <PageSizeItemSettings Items="50, 100, 200, 500, 1000" Visible="True">
                                    </PageSizeItemSettings>
                                </SettingsPager>
                                <SettingsEditing Mode="Inline">
                                </SettingsEditing>
                                <Settings ShowHeaderFilterButton="True" />
                                <SettingsBehavior AllowFocusedRow="True" ConfirmDelete="True" />
                                <SettingsPopup>
                                    <EditForm HorizontalAlign="Center" Modal="True" VerticalAlign="Middle" VerticalOffset="0" />
                                    <CustomizationWindow HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" />
                                </SettingsPopup>
                                <SettingsText CommandNew="Nuevo" />
                                <Columns>
                                    <dx:GridViewCommandColumn ShowInCustomizationForm="True" VisibleIndex="20" ToolTip="Funciones">
                                        <CustomButtons>
                                            <dx:GridViewCommandColumnCustomButton ID="btnHidQuitar" Text="Quitar">
                                            </dx:GridViewCommandColumnCustomButton>
                                        </CustomButtons>
                                        <CellStyle ForeColor="#006600">
                                        </CellStyle>
                                    </dx:GridViewCommandColumn>
                                    <dx:GridViewDataTextColumn Caption="ID_AUTORIZACION_CONDICION" FieldName="ID_AUTORIZACION_CONDICION" ShowInCustomizationForm="True" VisibleIndex="13" Visible="False">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="ID_IDENTIFICADOR" FieldName="ID_IDENTIFICADOR" ShowInCustomizationForm="True" VisibleIndex="15" Visible="False">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="PRODUCTO" FieldName="NOMBRE" ShowInCustomizationForm="True" VisibleIndex="16">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="UNIDAD MEDIDA" FieldName="SUBID_NOMBRE" ShowInCustomizationForm="True" VisibleIndex="18">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="OBSERV." FieldName="OBSERVACIONES" ShowInCustomizationForm="True" VisibleIndex="19" ToolTip="Observaciones">
                                        <DataItemTemplate>
                                            <dx:ASPxTextBox ID="txtObservacion" runat="server" Text='<%# Eval("OBSERVACIONES") %>' Width="100%">
                                                <Border BorderStyle="None" />
                                            </dx:ASPxTextBox>
                                        </DataItemTemplate>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="ESTADO" FieldName="ESTADO" ShowInCustomizationForm="True" Visible="False" VisibleIndex="21">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="ID_SUB_IDENTIFICADOR" FieldName="ID_SUB_IDENTIFICADOR" ShowInCustomizationForm="True" Visible="False" VisibleIndex="17">
                                    </dx:GridViewDataTextColumn>
                                </Columns>
                                <TotalSummary>
                                    <dx:ASPxSummaryItem FieldName="F_IMPORTE" ShowInColumn="IMPORTE" SummaryType="Custom" Tag="imp" />
                                    <dx:ASPxSummaryItem FieldName="VOLUMEN" ShowInColumn="CANTIDAD" SummaryType="Custom" Tag="vol" />
                                    <dx:ASPxSummaryItem FieldName="STR_ESTADO" ShowInColumn="EST." SummaryType="Custom" Tag="est" />
                                </TotalSummary>
                            </dx:ASPxGridView>
                            <br />
                            <dx:ASPxButton ID="btnGuardaHu" runat="server" Text="Guardar Configuración" Theme="Metropolis" Width="200px" AutoPostBack="False" OnClick="btnGuardaHu_Click" Font-Bold="True" ForeColor="Maroon">
                                <Image IconID="actions_apply_16x16">
                                </Image>
                            </dx:ASPxButton>
                        </td>
                    </tr>

                </table>
            </asp:Panel>
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>

<dx:ASPxPopupControl ID="pucDJRecepciones" runat="server" AllowDragging="True" Modal="true" ClientInstanceName="pucDJRecepciones" EnableTheming="True" HeaderText="Declaracion Jurada de Saldos Fisicos" Height="105px" PopupVerticalAlign="WindowCenter" PopupHorizontalAlign="WindowCenter" OnWindowCallback="pucDJRecepciones_WindowCallback">
    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl10" runat="server">
            <asp:Panel ID="Panel3" runat="server" Visible="false">
                <table>                    
                    <tr style="text-align: right;">
                        <td>Fecha Saldo Fisico(Medición) :
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
                        <td style="text-align: right;"> <span style="color:black; font-weight:bold;">Los saldos consignados corresponden a Hrs.:</span>   <%--color:maroon  --%>                                               
                        </td>
                        <td >
                                 <dx:ASPxTimeEdit ID="tedHoraSaldo" runat="server" EditFormatString="HH:mm" Width="90px" ReadOnly="true">
<ClearButton Visibility="Auto"></ClearButton>
                                 </dx:ASPxTimeEdit>            
                        </td>                     
                    </tr>   
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
                            <br />
                            <dx:ASPxButton ID="btnGuarfarDJ" runat="server" Text="Guardar Declaracion Saldo Fisico(Medicion)" Theme="Metropolis" Width="200px" AutoPostBack="False" OnClick="btnGuarfarDJ_Click" Font-Bold="True" ForeColor="Maroon">
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
                        </td>
                    </tr>

                </table>
            </asp:Panel>
            <asp:Panel ID="Panel5" runat="server" Visible="false">
                <table style="width: 400px; height: 100%">
                    <tr>
                        <td style="text-align: center; padding-bottom: 1px">
                            <dx:ASPxRoundPanel runat="server" ShowHeader="False" Width="100%" ID="ASPxRoundPanel1">
                                <PanelCollection>
                                    <dx:PanelContent ID="PanelContent21" runat="server">

                                        <dx:ASPxLabel runat="server" ForeColor="Maroon" ID="ASPxLabel5" Font-Bold="True" Font-Names="Rockwell" Font-Size="18pt" Text="El registro de saldo físico (Declaración Jurada) se habilitará de horas 00:00:00 hasta 09:00:00"></dx:ASPxLabel>
                                    </dx:PanelContent>
                                </PanelCollection>
                                <Border BorderStyle="Ridge" BorderWidth="1px" BorderColor="#003300"></Border>
                            </dx:ASPxRoundPanel>
                        </td>
                    </tr>
                </table>                
                </asp:Panel>
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>


<dx:ASPxPopupControl ID="pucSaldosFisco" runat="server" AllowDragging="True" Modal="true" ClientInstanceName="pucSaldosFisco" EnableTheming="True" HeaderText="Lista los registros del saldo fisco" PopupVerticalAlign="WindowCenter" PopupHorizontalAlign="WindowCenter" OnWindowCallback="pucSaldosFisco_WindowCallback" Width="500px">    
    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl11" runat="server">
            <asp:Panel ID="Panel4" runat="server">
                <table style="width:100%">
                    <tr>
                         <td style="text-align: right;">
                            Fecha de Registro del Saldo Fisico(Medición) :                                                    
                        </td>
                        <td>
                            <dx:ASPxDateEdit ID="detFechaRegSF" runat="server" ClientInstanceName="detFechaDocRegistro" EnableTheming="True" Height="23px" Theme="Metropolis" ToolTip="Seleccione la fecha Documento" Font-Bold="True">
                                <TimeSectionProperties>
                                    <TimeEditProperties>
                                        <ClearButton Visibility="Auto">
                                        </ClearButton>
                                    </TimeEditProperties>
                                </TimeSectionProperties>
                                    <ClientSideEvents DateChanged="function(s, e) { pucSaldosFisco.PerformCallback();  }" />
                                <ClearButton Visibility="Auto">
                                </ClearButton>
                            </dx:ASPxDateEdit>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">&nbsp;
                        </td>
                    </tr>
                    <tr>                       
                        <td colspan="2">
                            <dx:ASPxGridView ID="dgvSaldoFisico" runat="server" AutoGenerateColumns="False" ClientInstanceName="dgvSaldoFisico" EnableTheming="True" Font-Size="Smaller" KeyFieldName="ID_REPORTE_DATO_PRINC" OnHtmlRowPrepared="dgvSaldoFisico_HtmlRowPrepared" Theme="Default" Width="100%" OnCustomButtonCallback="dgvSaldoFisico_CustomButtonCallback" OnCustomButtonInitialize="dgvSaldoFisico_CustomButtonInitialize">
                                <ClientSideEvents CustomButtonClick="function(s, e) {
                                                                                    switch(e.buttonID) {
                                                                                    case&nbsp; 'btnAnularSalF2':
                                                                                    if(confirm('¿Desea anular la Declaracion Jurada? - Una vez realizada la acción no podrá revertir el proceso. ¿Desea continuar?'))
                                                                                    {
                                                                                    e.processOnServer = true;
                                                                                    }
                                                                                    else{
                                                                                    e.processOnServer = false; 
                                                                                    }
                                                                                    break;
                                                                                    } 
                                                                                    }" />
                                <SettingsContextMenu EnableRowMenu="False">
                                </SettingsContextMenu>
                                <Templates>
                                    <EditForm>
                                    </EditForm>
                                </Templates>
                                <SettingsPager PageSize="50">
                                    <PageSizeItemSettings Items="50, 100, 200, 500, 1000" Visible="True">
                                    </PageSizeItemSettings>
                                </SettingsPager>
                                <SettingsEditing Mode="PopupEditForm">
                                </SettingsEditing>
                                <Settings ShowHeaderFilterButton="True" ShowFooter="True" ShowFilterRow="True" />
                                <SettingsBehavior AllowFocusedRow="True" />
                                <SettingsPopup>
                                    <EditForm Modal="True" />
                                    <CustomizationWindow HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" />
                                </SettingsPopup>
                                <SettingsText CommandNew="Nuevo" />
                                <Columns>
                                    <dx:GridViewCommandColumn ShowInCustomizationForm="True" VisibleIndex="7" ShowClearFilterButton="True" ToolTip="Funciones">
                                        <CustomButtons>                                            
                                            <dx:GridViewCommandColumnCustomButton ID="btnAnularSalF2" Text=" ">
                                                <Image IconID="reports_deleteheader_16x16" ToolTip="Anular Declaración Jurada">
                                                </Image>
                                            </dx:GridViewCommandColumnCustomButton>                                            
                                        </CustomButtons>
                                    </dx:GridViewCommandColumn>
                                    <dx:GridViewDataTextColumn Caption="USUARIO" FieldName="APP_USUARIO" ShowInCustomizationForm="True" VisibleIndex="3">
                                        <Settings AutoFilterCondition="Contains" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="OBSERVACIONES" FieldName="OBSERVACIONES" ShowInCustomizationForm="True" VisibleIndex="4">
                                        <Settings AutoFilterCondition="Contains" />
                                        <CellStyle HorizontalAlign="Right">
                                        </CellStyle>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="ESTADO" FieldName="ESTADO" ShowInCustomizationForm="True" VisibleIndex="5" Visible="False">
                                        <Settings AutoFilterCondition="Contains" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="CODIGO" FieldName="CITE_GENERADO" ShowInCustomizationForm="True" VisibleIndex="2">
                                        <Settings AutoFilterCondition="Equals" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="FECHA" FieldName="FECHA_REPORTE" ShowInCustomizationForm="True" VisibleIndex="1" Visible="False">
                                        <Settings AutoFilterCondition="Contains" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="ESTADO" FieldName="StrEstado" ShowInCustomizationForm="True" VisibleIndex="6">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="ID_REPORTE_DATO_PRINC" FieldName="ID_REPORTE_DATO_PRINC" ShowInCustomizationForm="True" Visible="False" VisibleIndex="8">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="FECHA Y HORA REPORTADO" FieldName="StrFecha" ShowInCustomizationForm="True" VisibleIndex="0">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="DIRECCION" FieldName="DIRECCION" ShowInCustomizationForm="True" Visible="False" VisibleIndex="9">
                                    </dx:GridViewDataTextColumn>
                                </Columns>
                                <Styles CssPostfix="DevGlp">
                                    <AlternatingRow BackColor="#EEEEEE">
                                    </AlternatingRow>
                                </Styles>
                                <TotalSummary>
                                    <dx:ASPxSummaryItem FieldName="DecRecepcion" ShowInColumn="RECEPCION" SummaryType="Custom" Tag="rec" />
                                    <dx:ASPxSummaryItem FieldName="DecDemasia" ShowInColumn="DEMASIA" SummaryType="Custom" Tag="dem" />
                                    <dx:ASPxSummaryItem FieldName="DecTotalIng" ShowInColumn="TOT. ING." SummaryType="Custom" Tag="ing" />
                                    <dx:ASPxSummaryItem FieldName="DecVenta" ShowInColumn="VENTA" SummaryType="Custom" Tag="ven" />
                                    <dx:ASPxSummaryItem FieldName="DecMerma" ShowInColumn="MERMA" SummaryType="Custom" Tag="mer" />
                                    <dx:ASPxSummaryItem FieldName="DecTotalSal" ShowInColumn="TOT. SAL." SummaryType="Custom" Tag="sal" />
                                </TotalSummary>
                            </dx:ASPxGridView>
                        </td>
                    </tr>                    
                </table>
            </asp:Panel>
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>

<%--<dx:ASPxPopupControl ID="pucRegistroPrecio" runat="server"
    CloseAction="CloseButton"
    CloseOnEscape="True"
    Modal="True"
    ClientInstanceName="pucRegistroPrecio"
    PopupHorizontalAlign="WindowCenter"
    PopupVerticalAlign="WindowCenter"
    HeaderText="PRECIO DE PRODUCTOS IMPORTADOS"
    AllowDragging="True"
    PopupAnimationType="None"
    EnableViewState="False"
    Maximized="True"
    ShowMaximizeButton="True"
    Theme="Metropolis"
    ScrollBars="Auto"
    Width="250px"
    AllowResize="True" OnWindowCallback="pucRegistroPrecio_WindowCallback">--%>

<%--<dx:ASPxPopupControl ID="pucRegistroPrecio" runat="server" AllowDragging="True" Modal="true" ClientInstanceName="pucRegistroPrecio" EnableTheming="True" HeaderText="PRECIO DE PRODUCTOS IMPORTADOS" PopupVerticalAlign="WindowCenter" PopupHorizontalAlign="WindowCenter" OnWindowCallback="pucRegistroPrecio_WindowCallback" Width="500px"> 
    <ContentCollection>
        <dx:PopupControlContentControl runat="server">

            <table border="0" style="width:100%; border-spacing:5px">

                <tr>
                    <td style="width:10%; text-align:right">
                        <dx:ASPxLabel runat="server" Text="Importadora :" Font-Bold="true" />
                    </td>
                    <td style="width:20%">
                        <dx:ASPxComboBox ID="cmbEntidadImportadoras" runat="server" TextField="NOMBRE" TextFormatString="{1}" Theme="Metropolis" ValueField="ID_IDENTIFICADOR" Width="100%">
                            <Columns>
                                <dx:ListBoxColumn Caption="ACTIVIDAD" FieldName="SUBID_NOMBRE" />
                                <dx:ListBoxColumn Caption="RAZON SOCIAL" FieldName="NOMBRE" />                              
                                <dx:ListBoxColumn Caption="ID_IDENTIFICADOR" FieldName="ID_IDENTIFICADOR" Width="0px" />
                            </Columns>
                            <ClearButton Visibility="Auto">
                            </ClearButton>
                            <ReadOnlyStyle BackColor="#DEE8DB">
                            </ReadOnlyStyle>
                            <FocusedStyle BackColor="#FFFFCC">
                            </FocusedStyle>
                            <DisabledStyle BackColor="#DEE8DB">
                            </DisabledStyle>
                        </dx:ASPxComboBox>
                    </td>

                    <td style="width:10%; text-align:right">
                        <dx:ASPxLabel runat="server" Text="Producto :" Font-Bold="true" />
                    </td>
                    <td style="width:20%">
                         <dx:ASPxComboBox ID="cmbHidrocarburoPrecio" runat="server" ClientInstanceName="cmbHidrocarburoPrecio" Font-Size="X-Small" TextField="PRODUCTO" TextFormatString="{0}" ValueField="ID_IDENTIFICADOR" Width="160px" Theme="Metropolis" ToolTip="Seleccione un producto">
                            <Columns>
                                <dx:ListBoxColumn Caption="PRODUCTO" FieldName="CODIGO_NOMBRE" Name="CODIGO_NOMBRE" />
                                <dx:ListBoxColumn Caption="ID_IDENTIFICADOR" FieldName="ID_IDENTIFICADOR" Name="ID_IDENTIFICADOR" Width="0px" />
                            </Columns>
                            <ClearButton Visibility="Auto">
                            </ClearButton>
                            <ValidationSettings>
                                <RequiredField ErrorText="" />
                            </ValidationSettings>
                            <ReadOnlyStyle BackColor="#DEE8DB">
                            </ReadOnlyStyle>
                            <FocusedStyle BackColor="#FFFFCC">
                            </FocusedStyle>
                            <DisabledStyle BackColor="#DEE8DB">
                            </DisabledStyle>
                        </dx:ASPxComboBox>
                    </td>             
                </tr>

                <tr>
                    <td style="text-align:right">
                        <dx:ASPxLabel runat="server" Text="Dispensador :" Font-Bold="true" />
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="txtDispensador" runat="server" MaxLength="30" Width="100%" ClientInstanceName="txtDispensador">
                            <ReadOnlyStyle BackColor="#DEE8DB">
                            </ReadOnlyStyle>
                            <FocusedStyle BackColor="#FFFFCC">
                            </FocusedStyle>
                            <DisabledStyle BackColor="#DEE8DB">
                            </DisabledStyle>
                        </dx:ASPxTextBox>
                    </td>

                    <td style="text-align:right">
                        <dx:ASPxLabel runat="server" Text="Mangeras :" Font-Bold="true" />
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="txtMangenras" runat="server" MaxLength="30" Width="100%" ClientInstanceName="txtMangenras">
                            <ReadOnlyStyle BackColor="#DEE8DB">
                            </ReadOnlyStyle>
                            <FocusedStyle BackColor="#FFFFCC">
                            </FocusedStyle>
                            <DisabledStyle BackColor="#DEE8DB">
                            </DisabledStyle>
                        </dx:ASPxTextBox>
                    </td>
                
                </tr>

                <tr>
                    <td style="text-align:right">
                        <dx:ASPxLabel runat="server" Text="Precio :" Font-Bold="true" />
                    </td>
                    <td>
                        <dx:ASPxTextBox ID="txtPrecio" runat="server" ClientInstanceName="txtPrecio" Text="0" Width="100%">                         
                            <MaskSettings IncludeLiterals="DecimalSymbol" Mask="&lt;0..999&gt;.&lt;00..99&gt;" PromptChar=" " />
                            <ReadOnlyStyle BackColor="#DEE8DB">
                            </ReadOnlyStyle>
                            <FocusedStyle BackColor="#FFFFCC">
                            </FocusedStyle>
                            <DisabledStyle BackColor="#DEE8DB">
                            </DisabledStyle>
                        </dx:ASPxTextBox>
                    </td>

                    <td style="text-align:right">
                        <dx:ASPxLabel runat="server" Text="Estado :" Font-Bold="true" />
                    </td>
                    <td>
                        <dx:ASPxRadioButton ID="rbActivo" runat="server" Text="Activo" GroupName="Estado" Checked="true" />
                        <dx:ASPxRadioButton ID="rbInactivo" runat="server" Text="Inactivo" GroupName="Estado" />
                    </td>              
                </tr>

                <tr>
                    <td style="text-align:right">
                        <dx:ASPxLabel runat="server" Text="Fecha Desde :" Font-Bold="true" />
                    </td>
                    <td>
                        <dx:ASPxDateEdit ID="dtDesde" runat="server"
                            DisplayFormatString="dd/MM/yyyy HH:mm:ss"
                            EditFormatString="dd/MM/yyyy HH:mm:ss">
                            <TimeSectionProperties Visible="true" />
                        </dx:ASPxDateEdit>
                    </td>

                    <td style="text-align:right">
                        <dx:ASPxLabel runat="server" Text="Fecha Hasta :" Font-Bold="true" />
                    </td>
                    <td>
                        <dx:ASPxDateEdit ID="dtHasta" runat="server"
                            DisplayFormatString="dd/MM/yyyy HH:mm:ss"
                            EditFormatString="dd/MM/yyyy HH:mm:ss">
                            <TimeSectionProperties Visible="true" />
                        </dx:ASPxDateEdit>
                    </td>                   
                </tr>

                <tr>
                    <td style="text-align:right">
                        <dx:ASPxLabel runat="server" Text="Observación :" Font-Bold="true" />
                    </td>
                    <td colspan="4">
                        <dx:ASPxTextBox ID="txtObsPrecio" runat="server" MaxLength="150" Width="100%" ClientInstanceName="txtMangenras">
                            <ReadOnlyStyle BackColor="#DEE8DB">
                            </ReadOnlyStyle>
                            <FocusedStyle BackColor="#FFFFCC">
                            </FocusedStyle>
                            <DisabledStyle BackColor="#DEE8DB">
                            </DisabledStyle>
                        </dx:ASPxTextBox>
                    </td>
                </tr>

            </table>

            <hr />

            <dx:ASPxGridView ID="grdPrecios" runat="server" Width="100%"
                AutoGenerateColumns="False" KeyFieldName="ID_PRECIO">
                <Columns>                    
                    <dx:GridViewDataTextColumn FieldName="IMPORTADORA" Caption="IMPORTADORA" />
                    <dx:GridViewDataTextColumn FieldName="PRODUCTO" />
                    <dx:GridViewDataTextColumn FieldName="DISPENSER" />
                    <dx:GridViewDataTextColumn FieldName="MANGERA" />
                    <dx:GridViewDataTextColumn FieldName="PRECIO_VENTA" />
                    <dx:GridViewDataTextColumn FieldName="FECHA_DESDE" />
                    <dx:GridViewDataTextColumn FieldName="FECHA_HASTA" />
                    <dx:GridViewDataTextColumn FieldName="OBSERVACION" />
                    <dx:GridViewDataTextColumn FieldName="ESTADO" />
                </Columns>
            </dx:ASPxGridView>

            <br />

            <dx:ASPxButton ID="btnGuardarPrecio" runat="server" Text="Guardar" />
            <dx:ASPxButton ID="btnEditarPrecio" runat="server" Text="Editar" />
            <dx:ASPxButton ID="btnEliminarPrecio" runat="server" Text="Eliminar" />

        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>--%>

<dx:ASPxPopupControl ID="pucRegistroPrecio" runat="server"
    ClientInstanceName="pucRegistroPrecio"
    Modal="true"
    Width="800px"
    PopupHorizontalAlign="WindowCenter"
    PopupVerticalAlign="WindowCenter"
    HeaderText="GESTIÓN DE PRECIOS - PRODUCTOS IMPORTADOS"
    AllowDragging="true"
    PopupAnimationType="Fade"
    ShowFooter="true"
    OnWindowCallback="pucRegistroPrecio_WindowCallback"
    Theme="Metropolis"
    Font-Names="Segoe UI, Arial"
    Border-BorderStyle="Solid"
    Border-BorderWidth="1px"
    Border-BorderColor="#f7f7f7">

    <ClientSideEvents Shown="function(s,e){ 
        pucRegistroPrecio.PerformCallback('CONFIG'); 
    }" />

    <%--<ClientSideEvents CloseUp="function(s,e){ grdPrecios.Refresh(); }" />--%>

    <ClientSideEvents EndCallback="function(s, e) {
      if(s.cpResult == 'OK') {
            //grdPrecios.Refresh();
            confirm(s.cpResult);
        }                   
    }" />

    <ContentCollection>
        <dx:PopupControlContentControl runat="server">
            <dx:ASPxHiddenField ID="hfIdPrecio" runat="server" ClientInstanceName="hfIdPrecio" />
            <dx:ASPxHiddenField ID="hfIdEntidad" runat="server" ClientInstanceName="hfIdEntidad" />
            <dx:ASPxHiddenField ID="hfOperacion" runat="server" ClientInstanceName="hfOperacion" />
            <div style="padding: 15px; background-color: #f8f9fa;">
                <!-- Panel de datos principales -->
                <dx:ASPxRoundPanel ID="rpDatosPrincipales" runat="server"
                    HeaderText="DATOS DEL PRECIO"
                    Theme="Metropolis"
                    Width="100%"
                    ShowCollapseButton="true"
                    Paddings-Padding="10px">

                    <PanelCollection>
                        <dx:PanelContent>

                            <table style="width: 100%; border-collapse: separate; border-spacing: 0 5px;">
                                <tr>
                                    <td style="width: 100px; font-weight: bold;">Importadora:</td>
                                    <td style="width: 250px;">
                                        <dx:ASPxComboBox ID="cmbEntidadImportadoras" ClientInstanceName="cmbEntidadImportadoras" runat="server" TextField="NOMBRE" TextFormatString="{1}" Theme="Metropolis" ValueField="ID_IDENTIFICADOR" Width="100%">
                                            <Columns>
                                                <dx:ListBoxColumn Caption="ACTIVIDAD" FieldName="SUBID_NOMBRE" />
                                                <dx:ListBoxColumn Caption="RAZON SOCIAL" FieldName="NOMBRE" />
                                                <dx:ListBoxColumn Caption="ID_IDENTIFICADOR" FieldName="ID_IDENTIFICADOR" Width="0px" />
                                            </Columns>
                                            <ClearButton Visibility="Auto">
                                            </ClearButton>
                                            <ReadOnlyStyle BackColor="#DEE8DB">
                                            </ReadOnlyStyle>
                                            <FocusedStyle BackColor="#FFFFCC">
                                            </FocusedStyle>
                                            <DisabledStyle BackColor="#DEE8DB">
                                            </DisabledStyle>
                                        </dx:ASPxComboBox>
                                    </td>
                                    <td style="width: 80px; font-weight: bold; padding-left: 10px;">Producto:</td>
                                    <td style="width: 250px;">
                                        <dx:ASPxComboBox ID="cmbHidrocarburoPrecio" runat="server" ClientInstanceName="cmbHidrocarburoPrecio" Font-Size="X-Small" TextField="PRODUCTO" TextFormatString="{0}" ValueField="ID_IDENTIFICADOR" Width="100%" Theme="Metropolis" ToolTip="Seleccione un producto">
                                            <Columns>
                                                <dx:ListBoxColumn Caption="PRODUCTO" FieldName="CODIGO_NOMBRE" Name="CODIGO_NOMBRE" />
                                                <dx:ListBoxColumn Caption="ID_IDENTIFICADOR" FieldName="ID_IDENTIFICADOR" Name="ID_IDENTIFICADOR" Width="0px" />
                                            </Columns>
                                            <ClearButton Visibility="Auto">
                                            </ClearButton>
                                            <ValidationSettings>
                                                <RequiredField ErrorText="" />
                                            </ValidationSettings>
                                            <ReadOnlyStyle BackColor="#DEE8DB">
                                            </ReadOnlyStyle>
                                            <FocusedStyle BackColor="#FFFFCC">
                                            </FocusedStyle>
                                            <DisabledStyle BackColor="#DEE8DB">
                                            </DisabledStyle>
                                        </dx:ASPxComboBox>
                                    </td>
                                </tr>

                                <tr>
                                    <td style="font-weight: bold;">Dispenser:</td>
                                    <td>
                                        <dx:ASPxTextBox ID="txtDispenser" runat="server"
                                            MaxLength="30"
                                            Width="100%"
                                            ClientInstanceName="txtDispenser"
                                            NullText="Ingrese código dispenser..."
                                            Theme="Metropolis">
                                        </dx:ASPxTextBox>
                                    </td>
                                    <td style="font-weight: bold; padding-left: 10px;">Manguera:</td>
                                    <td>
                                        <dx:ASPxTextBox ID="txtManguera" runat="server"
                                            MaxLength="30"
                                            Width="100%"
                                            ClientInstanceName="txtManguera"
                                            NullText="Ingrese código manguera..."
                                            Theme="Metropolis">
                                        </dx:ASPxTextBox>
                                    </td>
                                </tr>

                                <tr>
                                    <td style="font-weight: bold;">Precio:</td>
                                    <td>
                                        <dx:ASPxSpinEdit ID="txtPrecio" runat="server"
                                            ClientInstanceName="txtPrecio"
                                            Number="0"
                                            Width="100%"
                                            MinValue="0"
                                            MaxValue="999999.99"
                                            DecimalPlaces="2"
                                            DisplayFormatString="n2"
                                            Theme="Metropolis"
                                            NullText="0.00"
                                            AllowNull="false">
                                            <SpinButtons ShowIncrementButtons="true">
                                            </SpinButtons>
<ClearButton Visibility="Auto"></ClearButton>

                                            <ValidationSettings>
                                                <RequiredField IsRequired="true" ErrorText="Precio requerido" />
                                            </ValidationSettings>
                                        </dx:ASPxSpinEdit>
                                    </td>
                                    <td style="font-weight: bold; padding-left: 10px;">Volumen:</td>
                                    <td>
                                        <dx:ASPxSpinEdit ID="txtVolumen" runat="server"
                                            ClientInstanceName="txtVolumen"
                                            Number="0"
                                            Width="100%"
                                            MinValue="0"
                                            MaxValue="999999999.999999"
                                            DecimalPlaces="6"
                                            DisplayFormatString="n6"
                                            Theme="Metropolis"
                                            NullText="0.000000"
                                            AllowNull="false">
                                            <SpinButtons ShowIncrementButtons="true">
                                            </SpinButtons>
<ClearButton Visibility="Auto"></ClearButton>

                                            <ValidationSettings>
                                                <RequiredField IsRequired="true" ErrorText="Volumen requerido" />
                                            </ValidationSettings>
                                        </dx:ASPxSpinEdit>
                                    </td>
                                </tr>

                                <tr>
                                    <td style="font-weight: bold;">Vigencia Desde:</td>
                                    <td>
                                        <dx:ASPxDateEdit ID="detFechaDesde" runat="server"
                                            ClientInstanceName="detFechaDesde"
                                            EditFormat="DateTime"                                   
                                            Theme="Metropolis"
                                            ToolTip="Fecha inicio de vigencia"
                                            Width="100%"
                                            UseMaskBehavior="true"
                                            AllowNull="false">
                                            <TimeSectionProperties Visible="true" >
<TimeEditProperties>
<ClearButton Visibility="Auto"></ClearButton>
</TimeEditProperties>
                                            </TimeSectionProperties>

<ClearButton Visibility="Auto"></ClearButton>

                                            <ValidationSettings>
                                                <RequiredField IsRequired="true" ErrorText="Fecha desde requerida" />
                                            </ValidationSettings>
                                        </dx:ASPxDateEdit>
                                    </td>
                                    <td style="font-weight: bold; padding-left: 10px;">Vigencia Hasta:</td>
                                    <td>
                                        <dx:ASPxDateEdit ID="detFechaHasta" runat="server"
                                            ClientInstanceName="detFechaHasta"
                                            DisplayFormatString="dd/MM/yyyy"
                                            EditFormatString="dd/MM/yyyy"
                                            EditFormat="DateTime"
                                            Theme="Metropolis"
                                            ToolTip="Fecha fin de vigencia"
                                            Width="100%"                                         
                                            UseMaskBehavior="true"
                                            AllowNull="true">
                                            <TimeSectionProperties Visible="true" >
<TimeEditProperties>
<ClearButton Visibility="Auto"></ClearButton>
</TimeEditProperties>
                                            </TimeSectionProperties>
                                            <ClearButton Visibility="Auto">
                                            </ClearButton>
                                           <%-- <ValidationSettings>
                                                <RequiredField IsRequired="true" ErrorText="Fecha hasta requerida" />
                                            </ValidationSettings>--%>
                                            <ClientSideEvents Validation="function(s,e){
                                                                                        var desde = detFechaDesde.GetDate();
                                                                                        var hasta = s.GetDate();    
                                                                                        // Si no hay fecha hasta, no validar (permitir vacío)
                                                                                        if(!hasta) {
                                                                                            e.isValid = true;
                                                                                            return;
                                                                                        }    
                                                                                        // Si hay fecha hasta pero no hay fecha desde, no validar la comparación
                                                                                        if(!desde) {
                                                                                            e.isValid = true;
                                                                                            return;
                                                                                        }    
                                                                                        // Si ambas fechas existen, validar que hasta sea mayor o igual que desde
                                                                                        if(hasta &lt; desde) {
                                                                                            e.isValid = false;
                                                                                            e.errorText = 'Fecha hasta debe ser mayor o igual a fecha desde';
                                                                                        } else {
                                                                                            e.isValid = true;
                                                                                        }
                                                                                    }" />
                                        </dx:ASPxDateEdit>
                                    </td>
                                </tr>

                                <tr>
                                    <td style="font-weight: bold;">Estado:</td>
                                    <td>
                                        <dx:ASPxRadioButtonList ID="rblEstado" runat="server" ClientInstanceName="rblEstado"
                                            Theme="Metropolis"
                                            RepeatDirection="Horizontal"
                                            Border-BorderStyle="None"
                                            SelectedIndex="0">
                                            <Items>
                                                <dx:ListEditItem Text="Activo" Value="1" Selected="true" />
                                                <dx:ListEditItem Text="Inactivo" Value="0" />
                                            </Items>

<Border BorderStyle="None"></Border>
                                        </dx:ASPxRadioButtonList>
                                    </td>
                                    <td style="font-weight: bold; padding-left: 10px;">Observación:</td>
                                    <td>
                                        <dx:ASPxTextBox ID="txtObsPrecioImp" runat="server" ClientInstanceName="txtObsPrecioImp"
                                            Theme="Metropolis"
                                            Width="100%"
                                            MaxLength="250"
                                            NullText="Ingrese observación...">
                                        </dx:ASPxTextBox>
                                    </td>
                                </tr>
                            </table>

                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxRoundPanel>
                <br />
                <!-- Panel de lista de precios -->
                <dx:ASPxRoundPanel ID="rpListaPrecios" runat="server"
                    HeaderText="LISTADO DE PRECIOS REGISTRADOS"
                    Theme="Metropolis"
                    Width="100%"
                    Paddings-Padding="10px">

                    <PanelCollection>
                        <dx:PanelContent>
                            <%--<dx:ASPxGridView ID="grdPrecios" runat="server"
                                ClientInstanceName="grdPrecios"
                                KeyFieldName="ID_PRECIO_IMPORTADO"
                                AutoGenerateColumns="False"
                                Width="100%"
                                Theme="Default"                               
                                Styles-Header-BackColor="#2f8041"
                                Styles-Header-ForeColor="White"
                                Settings-ShowFilterRow="true"
                                Settings-ShowGroupPanel="true"
                                SettingsBehavior-AllowFocusedRow="true"
                                SettingsBehavior-EnableRowHotTrack="true"
                                Settings-VerticalScrollableHeight="200"
                                Settings-VerticalScrollBarMode="Auto"
                                OnDataBinding="grdPrecios_DataBinding">--%>

                                <%--<ClientSideEvents RowClick="function(s,e){
                                    s.GetRowValues(e.visibleIndex, 'ID_PRECIO;IMPORTADORA;ID_ENTIDAD;ID_PRODUCTO;DISPENSER;MANGUERA;PRECIO;VOLUMEN;FECHA_DESDE;FECHA_HASTA;OBSERVACIONES;ESTADO', function(v){
                                        hfIdPrecio.Set('ID', v[0]);
                                        hfOperacion.Set('operacion', 'EDITAR');
                                        
                                        // Cargar datos en los campos
                                        cmbEntidadImportadoras.SetText(v[1]);
                                        cmbEntidadImportadoras.SetValue(v[2]);
                                        cmbHidrocarburoPrecio.SetValue(v[3]);
                                        txtDispenser.SetValue(v[4]);
                                        txtManguera.SetValue(v[5]);
                                        txtPrecio.SetValue(v[6]);
                                        txtVolumen.SetValue(v[7]);
                                        
                                        // Convertir fechas (formato yyyyMMddHHmm)
                                        if(v[8]) {
                                            var fechaStr = v[8].toString();
                                            var año = fechaStr.substring(0,4);
                                            var mes = fechaStr.substring(4,6);
                                            var dia = fechaStr.substring(6,8);
                                            var hora = fechaStr.substring(8,10);
                                            var min = fechaStr.substring(10,12);
                                            detFechaDesde.SetDate(new Date(año, mes-1, dia, hora, min));
                                        }
                                        
                                        if(v[9]) {
                                            var fechaStr = v[9].toString();
                                            var año = fechaStr.substring(0,4);
                                            var mes = fechaStr.substring(4,6);
                                            var dia = fechaStr.substring(6,8);
                                            var hora = fechaStr.substring(8,10);
                                            var min = fechaStr.substring(10,12);
                                            detFechaHasta.SetDate(new Date(año, mes-1, dia, hora, min));
                                        }
                                        
                                        txtObsPrecioImp.SetValue(v[10]);
                                        rblEstado.SetValue(v[11].toString());
                                    });
                                }" />--%>

                                 <%--<Styles CssPostfix="DevGlp"><AlternatingRow BackColor="#EEEEEE">
                                                        </AlternatingRow></Styles>
<Settings ShowFilterRow="True" ShowGroupPanel="True" VerticalScrollBarMode="Auto"></Settings>

                                <SettingsBehavior AllowSelectByRowClick="true" AllowSelectSingleRowOnly="true" />

                                <Columns>
                                    <dx:GridViewDataTextColumn FieldName="ID_ENTIDAD" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="ID_PRODUCTO" Visible="false" />
                                    <dx:GridViewDataTextColumn FieldName="IMPORTADORA" Caption="IMPORTADORA" Width="150px" />
                                    <dx:GridViewDataTextColumn FieldName="PRODUCTO" Caption="PRODUCTO" Width="150px" />
                                    <dx:GridViewDataTextColumn FieldName="COD_DISDENSER" Caption="DISPENSER" Width="80px" />
                                    <dx:GridViewDataTextColumn FieldName="COD_MANGUERAS" Caption="MANGUERA" Width="80px" />
                                    <dx:GridViewDataTextColumn FieldName="PRECIO" Caption="PRECIO" Width="80px" PropertiesTextEdit-DisplayFormatString="n2" >
<PropertiesTextEdit DisplayFormatString="n2"></PropertiesTextEdit>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="VOLUMEN" Caption="VOLUMEN" Width="100px" PropertiesTextEdit-DisplayFormatString="n6" >
<PropertiesTextEdit DisplayFormatString="n6"></PropertiesTextEdit>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="FECHA_DESDE" Caption="DESDE" Width="130px">
                                        <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy HH:mm"></PropertiesTextEdit>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="FECHA_HASTA" Caption="HASTA" Width="130px">
                                        <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy HH:mm"></PropertiesTextEdit>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="OBSERVACIONES" Caption="OBSERVACIÓN" Width="150px" />
                                    <dx:GridViewDataTextColumn FieldName="ESTADO" Caption="ESTADO" Width="80px">
                                        <DataItemTemplate>
                                            <dx:ASPxLabel ID="lblEstado" runat="server"
                                                Text='<%# Eval("ESTADO").ToString() == "1" ? "ACTIVO" : "INACTIVO" %>'
                                                ForeColor='<%# Eval("ESTADO").ToString() == "1" ? System.Drawing.Color.Green : System.Drawing.Color.Red %>'
                                                Font-Bold="true" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataTextColumn>
                                </Columns>
                                <Styles>
                                <Header BackColor="#2F8041" ForeColor="White"></Header>

                                    <RowHotTrack BackColor="#E8F2FF" />
                                    <FocusedRow BackColor="#FFFFCC" ForeColor="#2e6e9e" Font-Bold="true" />
                                    <AlternatingRow Enabled="true" BackColor="#F5F5F5" />
                                </Styles>

                            </dx:ASPxGridView>--%>

                            <dx:ASPxGridView ID="dgvListadoPreciosImp" runat="server" AutoGenerateColumns="False" ClientInstanceName="dgvListadoPreciosImp" EnableTheming="True" Font-Size="Smaller" KeyFieldName="ID_PRECIO_IMPORTADO" Theme="Default" Width="100%" Settings-VerticalScrollableHeight="200"
                                Settings-VerticalScrollBarMode="Auto">
                                <SettingsContextMenu EnableRowMenu="False">
                                </SettingsContextMenu>
                                <Templates>
                                    <EditForm>
                                    </EditForm>
                                </Templates>
                                <SettingsPager PageSize="100">
                                    <PageSizeItemSettings Items="10, 50, 100, 200, 500, 1000" Visible="True">
                                    </PageSizeItemSettings>
                                </SettingsPager>
                                <SettingsEditing Mode="PopupEditForm">
                                </SettingsEditing>
                                <Settings ShowFooter="True" ShowHeaderFilterButton="True" ShowFilterRow="True" />
                                <SettingsBehavior AllowFocusedRow="True" />
                                <SettingsPopup>
                                    <EditForm Modal="True" />
                                    <CustomizationWindow HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" />
                                </SettingsPopup>
                                <SettingsText CommandNew="Nuevo" />
                                  <ClientSideEvents RowClick="function(s,e){
                                    s.GetRowValues(e.visibleIndex, 'ID_PRECIO_IMPORTADO;ID_ENTIDAD;ID_PRODUCTO;IMPORTADORA;COD_DISDENSER;COD_MANGUERAS;PRECIO;VOLUMEN;FECHA_DESDE;FECHA_HASTA;OBSERVACIONES;ESTADO;PRODUCTO', function(v){
                                      debugger;  
                                     
                                        hfIdPrecio.Set('ID', v[0]);
                                        hfIdEntidad.Set('ID_ENTIDAD', v[1]);
                                        hfOperacion.Set('operacion', 'EDITAR');
                                        
                                        // Cargar datos en los campos
                                       cmbEntidadImportadoras.SetText(v[3]);     // IMPORTADORA (índice 3)
                                       //cmbEntidadImportadoras.SetValue(v[1]);    // ID_ENTIDAD (índice 1)
                                       cmbHidrocarburoPrecio.SetText(v[12]);     // PRODUCTO (índice 12)
                                       cmbHidrocarburoPrecio.SetValue(v[2]);     // ID_PRODUCTO (índice 2)
                                       txtDispenser.SetValue(v[4]);               // COD_DISDENSER
                                       txtManguera.SetValue(v[5]);                 // COD_MANGUERAS
                                       txtPrecio.SetValue(v[6]);                   // PRECIO
                                       txtVolumen.SetValue(v[7]);                  // VOLUMEN
                                        
                                        // Convertir fechas desde formato DD/MM/YYYY HH:mm:ss
                                        if(v[8]) { // FECHA_DESDE
                                            var fechaStr = v[8].toString();
                                            console.log('Fecha DESDE original:', fechaStr);
            
                                            // Parsear formato 'dd/MM/yyyy HH:mm:ss'
                                            var partesFechaHora = fechaStr.split(' ');
                                            if (partesFechaHora.length >= 2) {
                                                var partesFecha = partesFechaHora[0].split('/');
                                                var partesHora = partesFechaHora[1].split(':');
                
                                                if (partesFecha.length === 3 && partesHora.length >= 2) {
                                                    var dia = parseInt(partesFecha[0]);
                                                    var mes = parseInt(partesFecha[1]) - 1;
                                                    var año = parseInt(partesFecha[2]);
                                                    var hora = parseInt(partesHora[0]);
                                                    var min = parseInt(partesHora[1]);
                    
                                                    detFechaDesde.SetDate(new Date(año, mes, dia, hora, min));
                                                    console.log(new Date(año, mes, dia, hora, min));
                                                }
                                            }
                                        }
                                        
                                         if(v[9]) { // FECHA_HASTA
                                                    var fechaStr = v[9].toString();
                                                    console.log('Fecha HASTA original:', fechaStr);
            
                                                    // Verificar si no es fecha inválida (como 01/01/1800 00:00:00)
                                                    if (!fechaStr.includes('01/01/1800')) {
                                                        var partesFechaHora = fechaStr.split(' ');
                                                        if (partesFechaHora.length >= 2) {
                                                            var partesFecha = partesFechaHora[0].split('/');
                                                            var partesHora = partesFechaHora[1].split(':');
                    
                                                            if (partesFecha.length === 3 && partesHora.length >= 2) {
                                                                var dia = parseInt(partesFecha[0]);
                                                                var mes = parseInt(partesFecha[1]) - 1;
                                                                var año = parseInt(partesFecha[2]);
                                                                var hora = parseInt(partesHora[0]);
                                                                var min = parseInt(partesHora[1]);
                        
                                                                detFechaHasta.SetDate(new Date(año, mes, dia, hora, min));
                                                                console.log('Fecha HASTA parseada:', new Date(año, mes, dia, hora, min));
                                                            }
                                                        }
                                                    } else {
                                                        console.log('Fecha HASTA inválida (01/01/1800), limpiando campo');
                                                        detFechaHasta.SetDate(null); // Limpiar fecha inválida
                                                    }
                                                }
                                        
                                        txtObsPrecioImp.SetText(v[10]);
                                        rblEstado.SetValue(v[11].toString());
                                    });
                                }" />
                                <Columns>
                                    <dx:GridViewCommandColumn ShowInCustomizationForm="True" Visible="False" VisibleIndex="21" ShowClearFilterButton="True">
                                    </dx:GridViewCommandColumn>
                                    <dx:GridViewDataTextColumn Caption="ID_PRECIO_IMPORTADO" FieldName="ID_PRECIO_IMPORTADO" Visible="False" ShowInCustomizationForm="True" VisibleIndex="12">                                        
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="ID_ENTIDAD" FieldName="ID_ENTIDAD" Visible="False" ShowInCustomizationForm="True" VisibleIndex="0">                                        
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="ID_PRODUCTO" FieldName="ID_PRODUCTO" Visible="False" ShowInCustomizationForm="True" VisibleIndex="1">
                                        <Settings AutoFilterCondition="Contains" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="IMPORTADORA" FieldName="IMPORTADORA" ShowInCustomizationForm="True" VisibleIndex="2">
                                        <Settings AutoFilterCondition="Contains" />
                                    </dx:GridViewDataTextColumn>                                                           
                                     <dx:GridViewDataTextColumn Caption="PRODUCTO" FieldName="PRODUCTO" ShowInCustomizationForm="True" VisibleIndex="3">
                                        <Settings AutoFilterCondition="Contains" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="DISDENSER" FieldName="COD_DISDENSER" ShowInCustomizationForm="True" VisibleIndex="4">
                                        <Settings AutoFilterCondition="Contains" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="MANGUERA" FieldName="COD_MANGUERAS" ShowInCustomizationForm="True" VisibleIndex="5">
                                        <Settings AutoFilterCondition="Contains" />
                                    </dx:GridViewDataTextColumn>
                                     <dx:GridViewDataTextColumn Caption="PRECIO" FieldName="PRECIO" ShowInCustomizationForm="True" VisibleIndex="6">
                                        <Settings AutoFilterCondition="Contains" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="VOLUMEN" FieldName="VOLUMEN" ShowInCustomizationForm="True" VisibleIndex="7">
                                        <Settings AutoFilterCondition="Contains" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="FECHA DESDE" FieldName="FECHA_DESDE" ShowInCustomizationForm="True" VisibleIndex="8">
                                        <Settings AutoFilterCondition="Contains" />
                                    </dx:GridViewDataTextColumn>
                                     <dx:GridViewDataTextColumn Caption="FECHA HASTA" FieldName="FECHA_HASTA" ShowInCustomizationForm="True" VisibleIndex="9">
                                        <Settings AutoFilterCondition="Contains" />
                                    </dx:GridViewDataTextColumn>
                                     <dx:GridViewDataTextColumn Caption="OBSERVACIONES" FieldName="OBSERVACIONES" ShowInCustomizationForm="True" VisibleIndex="10">
                                        <Settings AutoFilterCondition="Contains" />
                                    </dx:GridViewDataTextColumn>
                                     <dx:GridViewDataTextColumn Caption="ESTADO" FieldName="ESTADO" ShowInCustomizationForm="True" VisibleIndex="11">
                                        <DataItemTemplate>
                                            <dx:ASPxLabel ID="lblEstado" runat="server"
                                                Text='<%# Eval("ESTADO").ToString() == "1" ? "ACTIVO" : "INACTIVO" %>'
                                                ForeColor='<%# Eval("ESTADO").ToString() == "1" ? System.Drawing.Color.Green : System.Drawing.Color.Red %>'
                                                Font-Bold="true" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataTextColumn>
                                </Columns>
                                <TotalSummary>
                                    <dx:ASPxSummaryItem FieldName="VOLUMEN" ShowInColumn="VOLUMEN" SummaryType="Sum" Tag="imp" />
                                </TotalSummary>
                            </dx:ASPxGridView>

                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxRoundPanel>
                <br />
                <!-- Panel de botones -->
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: left;">
                            <dx:ASPxButton ID="btnNuevo" runat="server" Text="Nuevo" AutoPostBack="false"
                                Theme="Metropolis" Width="90px">
                                <ClientSideEvents Click="function(){ 
                                    pucRegistroPrecio.PerformCallback('NUEVO');
                                }" />
                                <Image IconID="actions_add_16x16" />
                            </dx:ASPxButton>
                        </td>
                        <td style="text-align: right;">
                            <dx:ASPxButton ID="btnGuardar" runat="server" Text="Guardar" AutoPostBack="false" Theme="Metropolis" Width="90px">
                                <ClientSideEvents Click="function(s, e) {  if(cmbEntidadImportadoras && cmbEntidadImportadoras.GetValue && 
                                                                           cmbEntidadImportadoras.GetValue() && 
                                                                           cmbHidrocarburoPrecio.GetValue()) {
                                                                            pucRegistroPrecio.PerformCallback('REGISTRAR'); 
                                                                        } else {
                                                                            alert('Complete los campos requeridos');
                                                                        }
                                                                    }" />
                                <Image IconID="actions_save_16x16devav" />
                            </dx:ASPxButton>

                             <dx:ASPxButton ID="btnModificarPrecio" runat="server" Text="Modificar" AutoPostBack="false" 
                                Theme="MetropolisBlue" Width="100px">
                                <ClientSideEvents Click="function(){
                                    var id = hfIdPrecio.Get('ID');
                                    var idEntidad = hfIdEntidad.Get('ID_ENTIDAD');
                                    if(!id){ 
                                        alert('Por favor, seleccione un registro de la lista'); 
                                        return; 
                                    }
                                    if(confirm('¿Desea Modificar el registro seleccionado?'))
                                        pucRegistroPrecio.PerformCallback('EDITAR|' + id + '|' + idEntidad);
                                }" />
                                <Image IconID="actions_saveas_16x16devav" />
                            </dx:ASPxButton>

                            <dx:ASPxButton ID="btnEliminarPrecio" runat="server" Text="Eliminar" AutoPostBack="false"
                                Theme="Metropolis" Width="90px">
                                <ClientSideEvents Click="function(){
                                    var id = hfIdPrecio.Get('ID');                                
                                    if(!id){ 
                                        alert('Por favor, seleccione un registro de la lista'); 
                                        return; 
                                    }
                                    if(confirm('¿Está seguro de eliminar el registro seleccionado?'))
                                        pucRegistroPrecio.PerformCallback('ELIMINAR|' + id);
                                }" />
                                <Image IconID="actions_cancel_16x16" />
                            </dx:ASPxButton>

                            <dx:ASPxButton ID="btnCancelar" runat="server" Text="Cerrar" AutoPostBack="false"
                                Theme="Metropolis" Width="90px">
                                <ClientSideEvents Click="function(){ pucRegistroPrecio.Hide(); }" />
                                <Image IconID="actions_close_16x16" />
                            </dx:ASPxButton>
                        </td>
                    </tr>
                </table>

            </div>

        </dx:PopupControlContentControl>
    </ContentCollection>

    <FooterContentTemplate>
        <div style="padding: 5px; text-align: right; font-size: 11px; color: #666;">
            <dx:ASPxLabel ID="lblInfo" runat="server" Text="Haga clic en un registro para editarlo"
                Font-Italic="true" ForeColor="#666666" />
        </div>
    </FooterContentTemplate>

<Border BorderColor="#F7F7F7" BorderStyle="Solid" BorderWidth="1px"></Border>

</dx:ASPxPopupControl>
