$(document).ready(function () {
    $("#panelNuevoUsuarioOverlay").dialog({ autoOpen: false, resizable: false, height: "auto", width: 750, modal: true, title: "Crear Nuevo Usuario" });
    var actividadRealizada = "";
    //Datos de la tabla detalle
    var trHeaderSuperficie = '<tr>' + $("#trHeaderSuperficie").html() + '</tr>';
    var trVaciosDocsSuperficie = '<tr>' + $("#trVaciosSuperficie").html() + '</tr>';
    //Llenamos espacios vacios
    for (var i = 1; i <= 1; i++)
        $("#tblDetalleSuperficie").append(trVaciosDocsSuperficie);
    pad = function (n, width, z) {
        z = z || '0';
        n = n + '';
        return n.length >= width ? n : new Array(width - n.length + 1).join(z) + n;
    };
    //Agregar objetoDocsPresentados a la Tabla
    AgregarobjetoSuperficie = function (obj) {
        if (ExisteObjetoSuperficie(obj)) {
            if (ValidarSuperficie(obj)) {
                objSuperficieTrabajada.push(obj);
                //ActualizarTablaDetalleSuperficie();
                return true;
            } else
                return false;
        }
        else {
            alert("El registro de superficie ya se encuentra declarada");
            return false;
        }
    };
    //Verifica si no se encuentra en la lista
    ExisteObjetoSuperficie = function (obj) {
        var resultado = true;
        for (var i = 0; i < objSuperficieTrabajada.length; i++)
            if (JSON.stringify(objSuperficieTrabajada[i]) == JSON.stringify(obj))
                return false;
        return resultado;
    };
    LlenarRecursivo = function (lstProductosAgricolas) {
        var lstEntidadesUsuarios = [];
        var lstObjProductos = [];
        for (var i = 0; i < lstProductosAgricolas.length; i++) {
            if (ExisteCategoria(lstObjProductos, lstProductosAgricolas[i].ID_ENTIDAD_PADRE)) {
                for (var j = 0; j < lstObjProductos.length; j++)
                    if (lstObjProductos[j].ID_ENTIDAD_PADRE == lstProductosAgricolas[i].ID_ENTIDAD_PADRE)
                        lstObjProductos[j].ATRIBUTOS.push({ "ID_ENTIDAD": lstProductosAgricolas[i].ID_ENTIDAD, "NOMBRE": lstProductosAgricolas[i].DENOMINACION +"|" + lstProductosAgricolas[i].ACTIVIDAD, "ID_ACTIVIDAD": lstProductosAgricolas[i].ID_ACTIVIDAD }); //, "ID_USUARIO": lstProductosAgricolas[i].ID_USUARIO_ASIGNADO
            }
            else {
                var atributos = [];
                atributos.push({ "ID_ENTIDAD": lstProductosAgricolas[i].ID_ENTIDAD, "NOMBRE": lstProductosAgricolas[i].DENOMINACION, "ID_ACTIVIDAD": lstProductosAgricolas[i].ID_ACTIVIDAD });
                lstObjProductos.push({ "ID_ENTIDAD_PADRE": lstProductosAgricolas[i].ID_ENTIDAD_PADRE, "CATALOGADOR": lstProductosAgricolas[i].DENOMINACION_PADRE, "ATRIBUTOS": atributos });
            }
        }
        var html = '<option value="0"></option>';
        for (var j = 0; j < lstObjProductos.length; j++) {
            if (lstObjProductos[j] != null) {
                html += '<optgroup label="' + lstObjProductos[j].CATALOGADOR + '" style="font-size:14px">';
                html += LlenarHijos(lstObjProductos[j].ATRIBUTOS);
                html += '</optgroup>';
            }
        }
        console.log("Verificando html general: " + html);
        return html;
    };
    LlenarPerfiles = function (lstPerfiles) {
        var lstEntidadesUsuarios = [];
        var lstObjPerfiless = [];
        var html = '<option value="0"></option>';
        for (var i = 0; i < lstPerfiles.length; i++) {
            var jsonPerfiles = "{ID_PERFIL:" + lstPerfiles[i].ID_PERFIL + "}";
            html += '<option value="' + jsonPerfiles + '">' + lstPerfiles[i].NOMBRE_PERFIL + '</option>';

        }
        console.log("Verificando html general: " + html);
        return html;
    };
    ExisteCategoria = function (lst, ID_ENTIDAD_PADRE) {
        var existe = false;
        for (var i = 0; i < lst.length; i++)
            if (lst[i].ID_ENTIDAD_PADRE == ID_ENTIDAD_PADRE)
                existe = true;
        return existe;
    };
    LlenarHijos = function (objProductoAgricola) {
        var html = '';
        for (var k = 0; k < objProductoAgricola.length; k++) {
            var jsonEntidadUsuario = '{ID_ENTIDAD:' + objProductoAgricola[k].ID_ENTIDAD + ',TIPO_ACTIVIDAD:' + objProductoAgricola[k].ID_ACTIVIDAD + "}";
            //html += '<option value="' + objProductoAgricola[k].ID_ENTIDAD + '">' + objProductoAgricola[k].NOMBRE + '</option>';}
            html += '<option value=  "' + jsonEntidadUsuario + '">' + objProductoAgricola[k].NOMBRE + '</option>';
        }
        return html;
    };
    //****************************ACCIONES USUARIOS**************************
    //************************** POP UP Agregar cupo *******************
    $("#btnAgregarUsuario").click(function (event) {
        event.preventDefault();
        event.stopPropagation();
        console.log("Entra al metodo");
        $("#btnAgregarUsuario").attr("disabled", "disabled");
        $("#panelNuevoUsuarioOverlay").dialog("open").show();
        //LlenarParametricas tipos combustible
        //LlamarWebMethod(URL_BASE + "/CargarTipoCombustible", null, function (oResultado) {
        //    oResultado = JSON.parse(oResultado.d);
        //    $("#btnAgregarCupo").removeAttr("disabled");
        //    $("#cmbTipoCombustible").html("");
        //    if (parseInt(oResultado.RESULTADO) == 1) {
        //        var lstProductos = oResultado.DATOS;

        //        var html = '';
        //        if (lstProductos.length > 0)
        //            html += '<option value="0"></option>';

        //        for (var i = 0; i < lstProductos.length; i++)
        //            if (lstCupos != null) {
        //                for (var j = 0; j < lstCupos.length; j++) {
        //                    if (lstProductos[i].ID == lstCupos[j].PRODUCTO_ID)
        //                        html += '<option value="' + lstProductos[i].ID + '">' + lstProductos[i].DESCRIPCION + '</option>';
        //                }
        //            }
        //        $("#cmbTipoCombustible").html(html);
        //        $("#panelNuevoCupoOverlay").dialog("open").show();
        //        $("#btnCrearGrupo").attr("disabled", "disabled");
        //    } else
        //        alert(oResultado.MENSAJE);
        //});
        CargarEntidadesUsuario();
    });
    $("#cmbEntidades").chosen().change(function () {
        PasarValorEntidad($(this).val());
        console.log("Pasar Valor Entidad cmbEntidades: " + JSON.stringify($(this).val()));
        //cmbEntidades = "" + $(this).val();
        //var algo = $(this);
        //console.log("Esto es todo: " + JSON.stringify(algo));
        //console.log("La cmbEntidad es en chosen change: " + JSON.stringify(cmbEntidades));

        //if (cmbEntidades == null) {
        //    alert("Indique la actividad realizada");
        //    return false;
        //}
        //var lstProductos = [];
        //var arrCultivos = cmbEntidades.split(",");
        //console.log("Listado arrCultivos: " + JSON.stringify(arrCultivos));

        //        document.getElementById('hdntxtbxTaksit').value = JSON.stringify(cmbEntidades);
        //document.getElementById('hdntxtbxTaksit').value = 10;}

        //$("#<%=hdntxtbxTaksit.ClientID %>").val(1);
        //$("#<%=hdntxtbxTaksit.ClientID %>").val(2);
        //var algo2 = JSON.stringify(arrCultivos);
        //console.log("Con value: " + algo2.value);
        //for (var m = 0; m < arrCultivos.length; m++)
        //    lstProductos.push(arrCultivos[m]);
        //console.log("Listado bien: " + arrCultivos.toString);
        //var obj = { "usuarioentidad": lstProductos };

        //document.getElementById("<%=HiddenFieldEntidadesUsuario.ClientID%>").value = JSON.stringify(arrCultivos);
    });
    $("#cmbPerfiles").chosen().change(function () {
        PasarValorPerfil($(this).val());
    });
});