<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtrPersonaSegip.ascx.cs" Inherits="AnhHydroOctanoPresentacion.Controles.CtrPersonaSegip" %>
<%--<script src="../../UI/bootstrap/js/bootstrap.min.js"></script>    --%>
<style type="text/css">
    /* Estilos param formulario */
    .modal-xs {
    max-width: 400px;
    max-height: 390px
    }
    .myCircle {
    padding: 3px;
    border-radius: 100%;
        height: 70px;
        width: 80px;
    }
    .wrap {
    word-wrap: break-word;
    white-space:normal;
        text-wrap: normal;
    }
    .modal-header,.close {
      background-color:seagreen;
      color:white !important;
      /*text-align:  ;*/
      font-size: 10px;
      word-wrap: break-word;
      height: 28px;
    }
</style>
<script>
    //script para Invocacíón de Información de Persona
    $(document).ready(function (e) {
        $('#modalPersona').on('show.bs.modal', function (e) {
            var id = $(e.relatedTarget).data().id;
            console.log("Este es el ci:" + id);
            CargarDatosPersona(id);
        });
        $("#modalPersona").on('hidden.bs.modal', function () {
            console.log("Hace algo al cerrar...");
            document.getElementById('lblNombre').innerHTML = "No existen Datos...";
            var contenedor = document.getElementById("imagenPersona");
            contenedor.src = "data:image/jpg;base64,xxx";
            document.getElementById('documentoPersona').innerHTML = " ";
            document.getElementById('docExpPersona').innerHTML = " ";
            document.getElementById('fechaNacPersona').innerHTML = " ";
            document.getElementById('direccionPersona').innerHTML = " ";
            document.getElementById('lugarNacPersona').innerHTML = " ";
        });
    });
    function CargarDatosPersona(strCi) {
        console.log("entra al metodo...");
        $.ajax({
            type: "POST",
            url: "../../Default.aspx/CargarDatosSegip",
            data: "{strCi:" + JSON.stringify(strCi) + "}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.d != "0") {
                    try {
                        var objPersona = JSON.parse(result.d);
                        var nombre = objPersona.strNombres + " " + objPersona.strPrimerApellido + " " + objPersona.strSegundoApellido;
                        var foto = "data:image/jpg;base64," + objPersona.btFoto;
                        document.getElementById('lblNombre').innerHTML = nombre;
                        var contenedor = document.getElementById("imagenPersona");
                        contenedor.src = foto;
                        document.getElementById('documentoPersona').innerHTML = objPersona.strNumeroDocumento;
                        document.getElementById('docExpPersona').innerHTML = objPersona.strComplemento;
                        document.getElementById('fechaNacPersona').innerHTML = objPersona.strFechaNacimiento;
                        document.getElementById('direccionPersona').innerHTML = objPersona.strDomicilio;
                        document.getElementById('lugarNacPersona').innerHTML = objPersona.strLugarNacimientoPais + "/" + objPersona.strLugarNacimientoDepartamento + "/" + objPersona.strLugarNacimientoProvincia + "/" + objPersona.strLugarNacimientoLocalidad;
                        $('#modalPersona').show();
                    }
                    catch (e) {
                        console.log(e);
                        alert("Error al cargar datos de persona");
                        $('#modalPersona').show();
                    }
                } else {
                    alert("No se encontraron datos en el SEGIP de la persona...");
                    $('#modalPersona').show();
                }
            },
            error: function (xhr, status, text) {
                alert("Se produjo un error al realizar la consulta");
                console.log('xhr' + JSON.stringify(xhr));
                console.log('status' + status.toString());
                console.log('text' + text.toString());
            }
        });
    }
</script>
<asp:PlaceHolder ID="PlaceHolder1" runat="server" >
        <script src="../../UI/bootstrap/js/bootstrap.js"></script>
        <script src="../../UI/bootstrap/js/bootstrap.min.js"></script>
</asp:PlaceHolder>
<!-- PopUp FormularioPersona -->
<div class="modal fade" id="modalPersona" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-controls-modal="modalPersona">
    <div class="modal-dialog top modal-xs">
        <div class="modal-content">
            <div class="modal-header" style="vertical-align: top">DATOS PERSONALES
            </div>
            <div class="modal-body" style="text-align: center">
                <div class="input-group input-group-xs input-group-addon " style="background: beige">
                    <div class="col-xs-12 col-xs-12 col-xs-12">
                            <img src="" id="imagenPersona" class="myCircle" alt="foto" align="left"/>                       
                           <h5><label id="lblNombre" class="wrap" style="width: 220px; color:darkgreen; text-align: center;font-size: 11px;" ></label></h5>                          
                        <div class="row center">
                            <div class="col-md-2">
                                <label style="font-size: 10px">C.I.: </label>   
                                </div>
                               <div class="col-md-4">                                                              
                                <label id="documentoPersona" name="documentoPersona" style="font-size: 10px"/>
                            </div>
                            <div class="col-md-2">
                                <label id="docExpPersona" name="docExpPersona" style="font-size: 10px" />
                            </div>
                           </div>
                    </div>
                    <div class="row col-xs-12">
                        <label style="font-size: 10px; width: 70px">Fecha de Nac.: </label>
                        <label id="fechaNacPersona" name="fechaNacPersona" style="width: 180px; font-size: 10px; width: 250px; background: white" class="wrap"/>                    
                    </div>
                    <div class="row col-xs-12">
                        <label style="font-size: 10px; width: 70px">Dirección: </label>
                        <label id="direccionPersona" name="direccionPersona" style="font-size: 10px; width: 250px; background: white" class="wrap" />
                    </div>
                    <div class="row col-xs-12">
                        <label style="font-size: 10px; width: 70px">Lugar de Nac.:  </label>
                        <label id="lugarNacPersona" name="lugarNacPersona" style="font-size: 10px; width: 250px; background: white" class="wrap" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>