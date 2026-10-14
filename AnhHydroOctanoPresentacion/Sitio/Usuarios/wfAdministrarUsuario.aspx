<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio/ANH.Master"
    AutoEventWireup="True" CodeBehind="wfAdministrarUsuario.aspx.cs" Inherits="AnhHydroOctanoPresentacion.Sitio.Usuarios.wfAdministrarUsuario"
    Debug="true" %>
<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>
<%@ Register TagPrefix="uc1" TagName="CtrAnhDatosPersona" Src="~/Controles/CtrAnhDatosPersona.ascx" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=4.1.60501.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="../../UI/js/jquery-ui-1.10.3.custom.min.js" type="text/javascript"></script>
    <link href="../../UI/css/jquery-ui-1.10.3.custom.min.css" rel="stylesheet" type="text/css" />
    <link href="../../UI/css/tablas.css" rel="stylesheet" />
    <link href="../../UI/css/chosen.css" rel="stylesheet" />
    <script src="../../UI/js/Volumenes/usuarios.js" type="text/javascript"></script>
    <script src="../../UI/js/chosen.jquery.js" type="text/javascript"></script>
    <style type="text/css">
        .panelDetalleOverlay {
            position: fixed;
            width: 100%;
            height: 100%;
            top: 0;
            left: 0;
            background: rgba(0,0,0,0.4);
            z-index: 100000;
        }
         
        .panelDetalle {
            position: relative;
            background: white;
            overflow-y: scroll;
            margin: 0 auto;
            top: 100px;
            z-index: 100001;
            padding: 10px 20px 50px 20px;
            max-height: 710px;
            left: 0px;
        }

        .TextIndicator_TextBox1 {
            background-color: Gray;
            color: White;
            font-family: Arial;
            font-size: x-small;
            font-style: italic;
            padding: 2px 3px 2px 3px;
        }

        .BarIndicator_TextBox2_weak {
            color: Red;
            background-color: Red;
        }

        .BarIndicator_TextBox2_average {
            color: Blue;
            /*/**background-color: Blue;*/ /*/*/
        }

        .BarIndicator_TextBox2_good {
            color: Green;
            background-color: Green;
        }

        .BarBorder_TextBox2 {
            border-style: solid;
            border-width: 1px;
            padding: 2px 2px 2px 2px;
            width: 150px;
            vertical-align: middle;
        }

        .temp {
            float: left;
            width: 100px;
        }
    </style>
    <script type="text/javascript">
        var cmbEntidades;
        var hiddenSeparatedList = "";
        function afterLoad(sender, args) {
            $("#MainContent_datosPersona_datosPersona_txtNroIdentificacion").change(function () {
                showLoading();
            });
        }
        function CargarEntidadesUsuario() {
            $("#cmbEntidades").html("");
            $("#cmbEntidades").trigger('chosen:updated');

            console.log("Entra al arbol");
            $.ajax({
                type: "POST",
                url: "wfAdministrarUsuario.aspx/CargarEntidadesUsuario",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.d != "0") {
                        var objProductos = JSON.parse(result.d);
                        $("#cmbEntidades").append(LlenarRecursivo(objProductos));
                        CargarPerfiles();
                        console.log(JSON.parse(result.d));
                        var config = {
                            '.chosen-select': { width: "100%" },
                            '.chosen-select-deselect': { allow_single_deselect: true },
                            '.chosen-select-no-single': { disable_search_threshold: 10 },
                            '.chosen-select-no-results': { no_results_text: 'No se obtuvieron resultados!' },
                            '.chosen-select-width': { width: "100%" }
                        };
                        for (var selector in config) {
                            $(selector).chosen(config[selector]);
                        }
                        $("#cmbEntidades").chosen({ no_results_text: "No se encontraron resultados!" });
                        $("#cmbEntidades").trigger('chosen:updated');
                    } else
                        alert("Error al cargar EntidadesUsuario");
                }, error: function (xhr, status, text) {
                    var response = $.parseJSON(xhr.responseText);
                    console.log('xhr' + JSON.stringify(xhr));
                    console.log('status' + status.toString());
                    console.log('text' + text.toString());
                    console.log(actividad);
                }
            });
        }
        function CargarPerfiles() {
            $("#cmbPerfiles").html("");
            $("#cmbPerfiles").trigger('chosen:updated');

            console.log("Entra al arbol perfiles");
            $.ajax({
                type: "POST",
                url: "wfAdministrarUsuario.aspx/ListarPerfiles",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.d != "0") {
                        var objPerfiles = JSON.parse(result.d);
                        $("#cmbPerfiles").append(LlenarPerfiles(objPerfiles));
                        console.log(JSON.parse(result.d));
                        var config = {
                            '.chosen-select': { width: "100%" },
                            '.chosen-select-deselect': { allow_single_deselect: true },
                            '.chosen-select-no-single': { disable_search_threshold: 10 },
                            '.chosen-select-no-results': { no_results_text: 'No se obtuvieron resultados!' },
                            '.chosen-select-width': { width: "100%" }
                        };
                        for (var selector in config) {
                            $(selector).chosen(config[selector]);
                            //hiddenSeparatedList += $(selector).chosen(config[selector]) + ','
                            //$('#hidTest').val(hiddenSeparatedList);
                        }
                        $("#cmbPerfiles").chosen({ no_results_text: "No se encontraron resultados!" });
                        $("#cmbPerfiles").trigger('chosen:updated');
                    } else
                        alert("Error Perfiles");
                }, error: function (xhr, status, text) {
                    var response = $.parseJSON(xhr.responseText);
                    console.log('xhr' + JSON.stringify(xhr));
                    console.log('status' + status.toString());
                    console.log('text' + text.toString());
                    //console.log(actividad);
                }
            });
        }
        function PasarValorEntidad(obj) {
            if (obj != null) {
                $("#<%=hidenEntidades.ClientID %>").val(obj);
            }
        }
        function PasarValorPerfil(obj) {
            if (obj != null) {
                $("#<%=hidenPerfiles.ClientID %>").val(obj);
            }
        }
    </script>
    <script type="text/javascript">
        var config = {
            '.chosen-select': { width: "100%" },
            '.chosen-select-deselect': { allow_single_deselect: true },
            '.chosen-select-no-single': { disable_search_threshold: 10 },
            '.chosen-select-no-results': { no_results_text: 'Oops, nothing found!' },
            '.chosen-select-width': { width: "100%" }
        };
        for (var selector in config) {
            $(selector).chosen(config[selector]);
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager runat="server" ID="ScriptManager"></asp:ScriptManager>
    <div class="col-md-12">
        <table style="width: 100%" id="tabl">
            <tr>
                <td style="text-align: center; padding-bottom: 1px">
                <dx:ASPxRoundPanel runat="server" ShowHeader="False" Width="100%" ID="pnlTitulo">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                            <dx:ASPxLabel runat="server" ForeColor="#367B2F" ID="lblTitulo" Font-Bold="True" Font-Names="Rockwell" Font-Size="Larger" Text="ADMINISTRACIÓN DE USUARIOS"></dx:ASPxLabel>
                        </dx:PanelContent>
                    </PanelCollection>
                    <Border BorderStyle="Ridge" BorderWidth="1px" BorderColor="#003300"></Border>
                </dx:ASPxRoundPanel>
                </td>
            </tr>
        </table>
        <p runat="server" id="mensaje" class="mensaje_error"></p>
        <dx:ASPxSplitter ID="ASPxSplitter1" runat="server" Height="100%">
            <Panes>
                <dx:SplitterPane Size="20%" AllowResize="True" AutoHeight="True">
                    <ContentCollection>
                        <dx:SplitterContentControl runat="server">
                            <p/>
                            <table>
                                <tr>
                                    <td>
                                        <dx:ASPxButton ID="btnNuevaCuenta" AutoPostBack="False" runat="server" Font-Bold="True" ForeColor="Maroon" Text="Nueva Cuenta" Theme="Metropolis" Width="73%" OnClick="btnNuevaCuenta_OnClick" >
                                            <Image IconID="people_customer_16x16"></Image>
                                        </dx:ASPxButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <dx:ASPxTreeView ID="treeViewOperadores" runat="server" 
                                            AllowSelectNode="true" AutoPostBack="False"
                                            OnNodeClick="treeViewOperadores_OnSelectedNodeChanged"
                                            ExpandDepth="0" 
                                            ImageSet="BulletedList" Width="200px" NodeWrap="True" Styles="" Theme="Youthful" Font-Size="Smaller">                                        
                                        </dx:ASPxTreeView>
                                    </td>
                                </tr>
                            </table>
                        </dx:SplitterContentControl>
                    </ContentCollection>
                </dx:SplitterPane>
                <dx:SplitterPane ScrollBars="Auto">
                    <ContentCollection>
                        <dx:SplitterContentControl runat="server">
                            <dx:ASPxCallbackPanel ID="pnlDatos" runat="server" Theme="Youthful" Width="100%" ClientInstanceName="pnlDatos">
                                <PanelCollection>
                                    <dx:PanelContent runat="server">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <dx:ASPxLabel runat="server" ForeColor="#367B2F" ID="ASPxLabel1" Font-Bold="True" Font-Names="Rockwell" Font-Size="Larger" Text="ASIGNAR PERFILES A USUARIOS"></dx:ASPxLabel>
                                            </div>
                                        </div>
                                        <table runat="server" id="tablaUsuariosAnh">
                                            <tr>
                                                <td><dx:ASPxLabel ID="lblUsuario" runat="server" Text="USUARIO:"/></td>
                                                <td>
                                                    <dx:ASPxTextBox ID="txtUsuario" runat="server" onkeyup="javascript:this.value=this.value.toUpperCase();"/>
                                                </td>
                                                <td>
                                                    <dx:ASPxButton ID="btnBuscarUsuarios" AutoPostBack="False" runat="server" Text="BUSCAR" Font-Bold="True" ForeColor="Maroon"  Theme="Metropolis" OnClick="btnBuscarUsuarios_Click">
                                                        <Image IconID="zoom_zoom_16x16"></Image>
                                                    </dx:ASPxButton>
                                                </td>
                                                <td>
                                                    <dx:ASPxButton ID="btnUsuarioAnh" AutoPostBack="False" runat="server" Text="MAS PERMISOS" Font-Bold="True" ForeColor="Maroon"  Theme="Metropolis" OnClick="btnUsuarioAnh_OnClick" >
                                                        <Image IconID="actions_add_16x16"></Image>
                                                    </dx:ASPxButton>
                                                </td>
                                            </tr>
                                        </table>
                                        <asp:TextBox ID="txtIdEntidad" runat="server"></asp:TextBox>
                                        <asp:TextBox ID="txtIdActividad" runat="server"></asp:TextBox>
                                        <dx:ASPxButton ID="btnAgregaEnt" runat="server" OnClick="btnAgregaEnt_Click" Text="ASPxButton">
                                        </dx:ASPxButton>
                                        <br/>
                                        <p class="descripcion">Asignar/Quitar Perfiles a un Usuario</p>
                                        <fieldset>
                                            <legend>USUARIO - MENÚS</legend>
                                            <table>
                                                <tr>
                                                    <td valign="top">
                                                        <dx:ASPxGridView ID="grvListaUsuarios" runat="server" Theme="Youthful" AutoGenerateColumns="False" KeyFieldName="ID_USUARIO" Width="300px"  >
                                                            <Settings VerticalScrollableHeight="150" ShowVerticalScrollBar="true" VerticalScrollBarStyle="Standard" />
                                                            <Columns>
                                                                <dx:GridViewDataTextColumn FieldName="ID_USUARIO" VisibleIndex="1" Caption="ID_USUARIO" Visible="False">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn FieldName="ID_TIPO_ACTIVIDAD" VisibleIndex="2" Caption="ID_TIPO_ACTIVIDAD" Visible="False">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="USUARIO" FieldName="NOMBRE_USUARIO" VisibleIndex="3">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="ACTIVIDAD" FieldName="NOMBRE_ACTIVIDAD" VisibleIndex="4">
                                                                </dx:GridViewDataTextColumn>
                                                            </Columns>
                                                            <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" AllowSelectByRowClick="True" ProcessSelectionChangedOnServer="True" />
                                                        </dx:ASPxGridView>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxButton ID="btnVer2" AutoPostBack="False" runat="server" Font-Bold="True" ForeColor="Maroon" Text="Ver menu" Theme="Metropolis" Width="50px" OnClick="btnVer_Click" />                                                       
                                                    </td>

                                                    <td valign="top">
                                                        <dx:ASPxTreeList ID="treeMenuUsuario" runat="server" Theme="Youthful" Height="150px" AutoGenerateColumns="False" KeyFieldName="ID_MENU" ParentFieldName="ID_MENU_PADRE" ClientInstanceName="treeMenuUsuario" Width="200px" Style="margin-left: 0px">
                                                            <Columns>
                                                                <dx:TreeListTextColumn FieldName="ID_MENU" VisibleIndex="0" Visible="False">
                                                                </dx:TreeListTextColumn>
                                                                <dx:TreeListTextColumn FieldName="ID_MENU_PADRE" VisibleIndex="1" Visible="False">
                                                                </dx:TreeListTextColumn>
                                                                <dx:TreeListTextColumn Caption="---- MENÚS DE LOS PERFILES DEL USUARIO ----" FieldName="TITULO" VisibleIndex="2">
                                                                </dx:TreeListTextColumn>
                                                            </Columns>
                                                        </dx:ASPxTreeList>
                                                    </td>
                                                    <td valign="top">
                                                        <dx:ASPxGridView ID="grvListaEntidades" runat="server" Theme="Youthful" AutoGenerateColumns="False" Width="300"><%-- KeyFieldName="ID_ENTIDAD" --%>
                                                            <Settings VerticalScrollableHeight="150" ShowVerticalScrollBar="true" VerticalScrollBarStyle="Standard" />
                                                            <SettingsBehavior AllowFocusedRow="True" />
                                                            <Columns>
                                                                <dx:GridViewDataTextColumn Caption="ENTIDAD" FieldName="NOMBRE_ENTIDAD" VisibleIndex="3">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="ACTIVIDAD" FieldName="NOMBRE_ACTIVIDAD" VisibleIndex="3">
                                                                </dx:GridViewDataTextColumn>
                                                            </Columns>                                                           
                                                        </dx:ASPxGridView>

                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                        <p class="mensaje" id="mensaje_usuario" runat="server"></p>
                                        <fieldset>
                                            <table>
                                                <tr>
                                                    <td colspan="3" align="center"><h4>USUARIO-PERFIL</h4></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <p class="subtitulo">PERFILES DISPONIBLES</p>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                    <td>
                                                        <p class="subtitulo">PERFILES ASIGNADOS AL USUARIO</p>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:ListBox ID="lbxPerfilesDisponibles" runat="server" Width="300px" DataTextField="NOMBRE_PERFIL" DataValueField="ID_PERFIL" Height="300px"></asp:ListBox>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnAsignarUno" runat="server" Text="&gt;" OnClick="btnAsignarUno_Click" Width="50px" />
                                                        <br />
                                                        <asp:Button ID="btnAsignarTodo" runat="server" Text="&gt;&gt;" OnClick="btnAsignarTodo_Click" Width="50px" OnClientClick="return confirm('¿DESEA ASIGNAR TODOS LOS PERFILES AL USUARIO?');" />
                                                        <br />
                                                        <asp:Button ID="btnQuitarTodos" runat="server" Text="&lt;&lt;" OnClick="btnQuitarTodos_Click" Width="50px" OnClientClick="return confirm('¿DESEA QUITAR TODOS LOS PERFILES DEL USUARIO?');" />
                                                        <br />
                                                        <asp:Button ID="btnQuitarUno" runat="server" Text="&lt;" OnClick="btnQuitarUno_Click" Width="50px" OnClientClick="return confirm('¿DESEA QUITAR EL PERFIL DEL USUARIO?');" />
                                                    </td>
                                                    <td>
                                                        <asp:ListBox ID="lbxPerfilesAsignados" runat="server" Width="300" DataTextField="NOMBRE_PERFIL" DataValueField="ID_PERFIL" Height="300px"></asp:ListBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </dx:PanelContent>
                                </PanelCollection>
                            </dx:ASPxCallbackPanel>
                        </dx:SplitterContentControl>
                    </ContentCollection>
                </dx:SplitterPane>
            </Panes>
        </dx:ASPxSplitter>
    </div>

    <dx:ASPxPopupControl ID="pucDatos" runat="server" AllowDragging="True" ClientInstanceName="pucDatos" EnableTheming="True" HeaderText="Asociación de nuevo usuario" Height="93px" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Theme="Office2003Olive" Width="334px">
        <HeaderImage IconID="setup_pagesetup_16x16"></HeaderImage>
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <table width="85%">
                    <tr>
                        <td style="vertical-align: top;">
                            <asp:ValidationSummary ID="PersonaValidationSummary" runat="server" DisplayMode="List" Visible="true" ForeColor="Red" ShowSummary="true" ValidationGroup="PersonaValidationGroup" />
                            <p class="mensaje_error" id="mensajeCreaUsuario" runat="server"></p>
                            <asp:HiddenField runat="server" ID="hidenEntidades" ClientIDMode="Static" />
                            <asp:HiddenField runat="server" ID="hidenPerfiles" ClientIDMode="Static" />
                            <asp:Panel runat="server" ID="panelUsuario" DefaultButton="btnSiguiente">
                                <table>
                                    <tr>
                                        <td>
                                            <h3>Solicitud de Acceso al Sistema </h3>
                                            <uc1:CtrAnhDatosPersona Width="650" ID="datosPersona" runat="server" OnNroIdentificacionTextChanged="datosPersonaTextChanged" OnTipoPersonaSelectedIndexChanged="datosPersonaSelectedIndexChange" />
                                            <fieldset>
                                                <h3>Credenciales de Acceso al Sistema</h3>
                                                <table id="tableContraseña" runat="server" width="100%">
                                                    <tr>
                                                        <td style="width: 50%"><%----%>Contraseña<span class="obligatorio">*</span>
                                                            <asp:Image ID="Image5" runat="server" AlternateText="Contraseña de acceso al sistema, no se refiere a la contraseña de su correo." CssClass="showtooltip icon_ayuda_16" /><br/> <%--ImageUrl="~/UI/img/pixel.gif"--%>
                                                            <asp:TextBox ID="txtUsuarioPassword1" runat="server" TextMode="Password" Width="95%"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvPass1" runat="server" ControlToValidate="txtUsuarioPassword1" Display="None" ErrorMessage="La Contraseña es Obligatoria." ValidationGroup="PersonaValidationGroup">*</asp:RequiredFieldValidator>
                                                            <br />                                                            
                                                            <asp:PasswordStrength runat="server" PreferredPasswordLength="8" BarBorderCssClass="BarBorder_TextBox2" DisplayPosition="BelowLeft" StrengthIndicatorType="BarIndicator" StrengthStyles="BarIndicator_TextBox2_weak;BarIndicator_TextBox2_average;BarIndicator_TextBox2_good" TextStrengthDescriptionStyles="BarIndicator_TextBox2_weak;BarIndicator_TextBox2_average;BarIndicator_TextBox2_good" Enabled="True" TargetControlID="txtUsuarioPassword1" ID="PasswordStrength2"></asp:PasswordStrength>
                                                        </td>
                                                        <td style="width: 50%"><%-- --%>Confirme su Contraseña<span class="obligatorio">*</span><br />
                                                            <asp:TextBox ID="txtUsuarioPassword2" runat="server" TextMode="Password" ValidationGroup="PersonaValidationGroup" Width="95%"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvPass2" runat="server" ControlToValidate="txtUsuarioPassword2" Display="None" ErrorMessage="La Confirmación de su Contraseña es Obligatoria." ValidationGroup="PersonaValidationGroup">*</asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <table width="100%">
                                                    <tr>
                                                        <td>Entidad(es) a ser asociadas con el usuario<br/>
                                                            <select id="cmbEntidades" class="chosen-select" data-placeholder="Seleccione entidades para ser asociadas con el usuario creado... " multiple style="width: 100%; overflow-scrolling: auto " tabindex="4">
                                                            </select>
                                                            <select id="Select1" runat="server" style="width: 100%" visible="False">
                                                            </select>
                                                        </td>
                                                        <td>Perfil(es) a ser asociadas con el usuario<br/>
                                                            <select id="cmbPerfiles" class="chosen-select" data-placeholder="Seleccione los perfiles a ser asociadas con el usuario creado... " multiple style="width: 100%;" tabindex="4"></select>
                                                            <select id="Select2" runat="server" style="width: 100%" visible="False"></select>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <p align="justify">
                                                    El correo electónico ingresado será su usuario de acceso al sistema.
                                                    <br/>Asegurese de introducirlos correctamente y recordalos. 
                                                </p>
                                            </fieldset>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table width="90%">
                                                <tr>
                                                    <td align="left">
                                                        <dx:ASPxButton ID="btnVolver" AutoPostBack="False" runat="server" Font-Bold="True" OnClick="btnVolver_Click" ForeColor="#003300" Text="Volver" Theme="Metropolis" Width="150px">
                                                            <Image IconID="arrows_prev_16x16">
                                                            </Image>
                                                        </dx:ASPxButton>
                                                    </td>
                                                    <td align="right">                                                       
                                                        <dx:ASPxButton ID="btnSiguiente" AutoPostBack="False" runat="server" Font-Bold="True" OnClick="btnSiguiente_Click" ForeColor="#003300" Text="Siguiente" Theme="Metropolis" Width="150px">
                                                            <Image IconID="arrows_next_16x16"></Image>
                                                        </dx:ASPxButton>
                                                    </td>
                                                </tr>   
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Panel ID="panelVistaPrevia" runat="server" Visible="false" Width="550px" DefaultButton="btnRegistrar">
                                <fieldset>
                                    <h2>Revise la información introducida:</h2>
                                    <table style="width: 80%; margin: 0 auto;" runat="server" id="tablaVistaPrevia"></table>
                                    <p>Presione <strong>"Volver"</strong> para editar la información.</p>
                                    <p>Presione <strong>"Registrar Información"</strong> para guardar los datos</p>
                                </fieldset>
                                <table width="100%">
                                    <tr>
                                        <td id="tbs" align="left">                                            
                                            <dx:ASPxButton ID="btnVolver2" AutoPostBack="False" runat="server" Font-Bold="True" OnClick="btnVolver2_Click" ForeColor="#003300" Text="Volver" Theme="Metropolis" Width="150px">
                                                <Image IconID="arrows_prev_16x16"></Image>
                                            </dx:ASPxButton>
                                        </td>
                                        <td align="right">                                            
                                            <dx:ASPxButton ID="btnRegistrar" AutoPostBack="False" runat="server" Font-Bold="True" OnClick="btnRegistrarInformacion_Click" ForeColor="#003300" Text="Registrar Información" Theme="Metropolis" Width="150px">
                                                <Image IconID="actions_apply_16x16"></Image>
                                            </dx:ASPxButton>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>

    <%--ImageUrl="~/UI/img/pixel.gif"--%>
    <dx:ASPxPopupControl ID="pucUsuariosAnh" runat="server" AllowDragging="True" ClientInstanceName="pucUsuariosAnh" EnableTheming="True" HeaderText="Permisos para usuarios ANH" Height="93px" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Theme="Office2003Olive" Width="334px">
        <HeaderImage></HeaderImage>
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <table width="85%">
                    <tr>
                        <td style="vertical-align: top;">
                            <asp:Panel runat="server" ID="panel1" > 
                                <table>
                                    <tr>
                                        <td valign="top">
                                            <dx:ASPxGridView ID="grvListaUsuariosAnh" runat="server" Theme="Youthful" AutoGenerateColumns="False" KeyFieldName="ID_USUARIO" Width="100%" OnFocusedRowChanged="grvListaUsuariosAnh_OnFocusedRowChanged">
                                                <%--<ClientSideEvents FocusedRowChanged="function(s, e) {treeMenuUsuario.Callback();}" />--%>
                                                <Columns>
                                                    <dx:GridViewCommandColumn VisibleIndex="0">                                                       
                                                    </dx:GridViewCommandColumn>
                                                    <dx:GridViewDataTextColumn FieldName="ID_USUARIO" VisibleIndex="1" Caption="ID_USUARIO" Visible="False">                                                        
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn FieldName="USUARIO" VisibleIndex="2">
                                                    </dx:GridViewDataTextColumn>
                                                    <dx:GridViewDataTextColumn Caption="NOMBRE COMPLETO" FieldName="NOMBRES" VisibleIndex="3">
                                                    </dx:GridViewDataTextColumn>
                                                </Columns>
                                                <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" AllowSelectByRowClick="True" ProcessSelectionChangedOnServer="True" />
                                            </dx:ASPxGridView>
                                        </td>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td>Actividad</td>
                                                    <td><dx:ASPxComboBox ID="cmbActividades" runat="server" ClientInstanceName="cmbReporteVariable" TextField="ID_TIPO_ACTIVIDAD" TextFormatString="{0}" ValueField="ID_TIPO_ACTIVIDAD">  <%--OnCallback="cmbReporteVariable_Callback"--%>
                                                        <Columns>
                                                            <dx:ListBoxColumn Caption="DESCRIPCION" FieldName="DESCRIPCION" Name="DESCRIPCION" />
                                                            <%--<dx:ListBoxColumn Caption="ID_CATEGORIA_ACTIVIDAD" FieldName="ID_CATEGORIA_ACTIVIDAD" Name="ID_CATEGORIA_ACTIVIDAD" />--%>
                                                            <dx:ListBoxColumn Caption="ID_TIPO_ACTIVIDAD" FieldName="ID_TIPO_ACTIVIDAD" Name="ID_TIPO_ACTIVIDAD" Width="0px" />
                                                        </Columns>
                                                        <ClearButton Visibility="Auto">
                                                        </ClearButton>
                                                    </dx:ASPxComboBox></td>
                                                </tr>
                                                <tr>
                                                    <td>Departamento</td>
                                                    <td><dx:ASPxComboBox ID="cmbDepartamento" runat="server" ClientInstanceName="cmbDepartamento" TextField="NOMBRE" TextFormatString="{0}" ValueField="ID" ValueType="System.Int32">
                                                            <Columns>
                                                                <dx:ListBoxColumn Caption="NOMBRE" FieldName="NOMBRE" Name="NOMBRE" />
                                                                <%--<dx:ListBoxColumn Caption="ABREVIACION2" FieldName="ABREVIACION2" Name="ABREVIACION2" Width="0px" />--%>
                                                                <dx:ListBoxColumn Caption="ID" FieldName="ID" Width="0px" />
                                                            </Columns>
                                                        <ClearButton Visibility="Auto">                                                                                                                
                                                        </ClearButton>
                                                    </dx:ASPxComboBox></td>
                                                </tr>
                                                <tr>
                                                    <td>Fecha Fin Vigencia</td>
                                                    <td><dx:ASPxDateEdit ID="dateFechaFin" runat="server" ClientInstanceName="dateFechaFin" EnableTheming="True" Height="23px" Theme="Metropolis" ToolTip="Seleccione la fecha final de vigencia">
                                                            <TimeSectionProperties>
                                                                <TimeEditProperties>
                                                                    <ClearButton Visibility="Auto">
                                                                    </ClearButton>
                                                                </TimeEditProperties>
                                                            </TimeSectionProperties>
                                                            <ClientSideEvents DateChanged="function(s, e) {
                                                                pnlDatos.PerformCallback();
                                                                }" />
                                                        <ClearButton Visibility="Auto"></ClearButton>
                                                        </dx:ASPxDateEdit></td>
                                                </tr>
                                                <tr>
                                                    <td>Observaciones</td> 
                                                    <td>
                                                        <dx:ASPxTextBox ID="txtObservaciones" runat="server" MaxLength="15" Width="100%" ClientInstanceName="txtObservaciones">
                                                            <ReadOnlyStyle BackColor="#DEE8DB"></ReadOnlyStyle>
                                                            <FocusedStyle BackColor="#FFFFCC"></FocusedStyle>
                                                            <DisabledStyle BackColor="#DEE8DB"></DisabledStyle>
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <dx:ASPxButton ID="btnAgregarControl" runat="server" AutoPostBack="False" Text="Agregar permiso a usuario" Theme="Metropolis" EnableTheming="True" Width="100%" Font-Bold="True" ForeColor="Maroon" OnClick="btnAgregarControl_Click" >
                                                <Image IconID="reports_addgroupheader_16x16"></Image>
                                            </dx:ASPxButton>
                                        </td>
                                        <td style="width: 70%"></td>
                                    </tr>
                                   <tr>
                                    <td colspan="3">
                                        <dx:ASPxGridView ID="grvTiposControl" runat="server" AutoGenerateColumns="False" ClientInstanceName="grvTiposControl" EnableTheming="True" Font-Size="Smaller" KeyFieldName="ID_CONTROL_USUARIO" OnCustomButtonCallback="grvTiposControl_CustomButtonCallback" Theme="Default" Width="100%"> <%--OnCustomCallback="grvTiposControl_CustomCallback" OnCustomSummaryCalculate="grvTiposControl_CustomSummaryCalculate" OnHtmlEditFormCreated="grvTiposControl_HtmlEditFormCreated" OnHtmlRowPrepared="grvTiposControl_HtmlRowPrepared" OnRowInserting="grvTiposControl_RowInserting" OnRowUpdating="grvTiposControl_RowUpdating" OnRowValidating="grvTiposControl_RowValidating"--%>
                                            <ClientSideEvents CustomButtonClick="function (s, e) {
                                                switch(e.buttonID) {
                                                case&nbsp; 'btnEliminar':
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; e.processOnServer = confirm('¿Desea eliminar el registro?');
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; break;
                                                }
                                                }"
                                                BeginCallback="function(s, e) {
                                                LoadingPanel.Show();
                                                }"
                                                EndCallback="function(s, e) {
                                                LoadingPanel.Hide();
                                                }" />
                                            <SettingsContextMenu EnableRowMenu="False"></SettingsContextMenu>
                                            <Columns>
                                                <dx:GridViewCommandColumn ShowInCustomizationForm="True" ToolTip="Funciones" VisibleIndex="47">
                                                    <CustomButtons>
                                                        <%--<dx:GridViewCommandColumnCustomButton ID="btnActivar" Text=" ">
                                                            <Image IconID="databars_solidgreendatabar_16x16" ToolTip="Activar">
                                                            </Image>
                                                        </dx:GridViewCommandColumnCustomButton>--%>
                                                        <dx:GridViewCommandColumnCustomButton ID="btnEliminar" Text=" ">
                                                            <Image IconID="reports_deletegroupfooter_16x16office2013" ToolTip="Eliminar">
                                                            </Image>
                                                        </dx:GridViewCommandColumnCustomButton>
                                                    </CustomButtons>
                                                    <CellStyle ForeColor="#006600">
                                                    </CellStyle>
                                                </dx:GridViewCommandColumn>
                                                <dx:GridViewDataTextColumn Caption="ID CONTROL" FieldName="ID_CONTROL_USUARIO" ShowInCustomizationForm="True" ToolTip="#" Visible="True" VisibleIndex="1">
                                                    <Settings AutoFilterCondition="Contains" />
                                                </dx:GridViewDataTextColumn>
                                                <%--<dx:GridViewDataTextColumn Caption="FECHA INICIO" FieldName="FECHA_INICIO" ShowInCustomizationForm="True" ToolTip="Fecha del Movimiento" Visible="False" VisibleIndex="2">
                                                    <Settings AutoFilterCondition="Contains" />
                                                </dx:GridViewDataTextColumn>--%>
                                                <dx:GridViewDataTextColumn Caption="FECHA FIN" FieldName="FECHA_FIN" ShowInCustomizationForm="True" ToolTip="Fecha fin de vigencia del permiso" VisibleIndex="3">
                                                    <Settings AutoFilterCondition="Contains" />
                                                    <CellStyle HorizontalAlign="Center">
                                                    </CellStyle>
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="ACTIVIDAD" FieldName="ID_TIPO_ACTIVIDAD" ShowInCustomizationForm="True" ToolTip="Tipo de Actividad" VisibleIndex="4">
                                                    <Settings AutoFilterCondition="Contains" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="DEPARTAMENTO" FieldName="ID_UBICACION" ShowInCustomizationForm="True" VisibleIndex="5">
                                                    <Settings AutoFilterCondition="Contains" />
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn Caption="OBSERVACIONES" FieldName="OBSERVACIONES" ShowInCustomizationForm="True" VisibleIndex="6">
                                                    <Settings AutoFilterCondition="Contains" />
                                                </dx:GridViewDataTextColumn>
                                            </Columns>
                                        </dx:ASPxGridView>
                                    </td>
                                </tr>
                                </table>
                            </asp:Panel>                            
                        </td>
                    </tr>
                </table>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>
</asp:Content>
