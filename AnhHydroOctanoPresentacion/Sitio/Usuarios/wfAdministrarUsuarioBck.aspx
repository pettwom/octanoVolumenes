<%@ Page Title="" Language="C#" MasterPageFile="~/Sitio/Site.Master"
    AutoEventWireup="True" CodeBehind="wfAdministrarUsuarioBck.aspx.cs" Inherits="AnhHydroOctanoPresentacion.Sitio.Usuarios.wfAdministrarUsuario"
    Debug="true" %>

<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>

<%@ Register TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=4.1.60501.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<%@ Register TagPrefix="uc1" TagName="CtrAnhDatosPersona" Src="~/Controles/CtrAnhDatosPersona.ascx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="../../UI/js/jquery-ui-1.10.3.custom.min.js"></script>
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
                        alert("Error");
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager runat="server" ID="ScriptManager1"></asp:ScriptManager>
    <p runat="server" id="mensaje" class="mensaje_error"></p>
    <dx:ASPxSplitter ID="ASPxSplitter1" runat="server" Height="700px">
        <Panes>
            <dx:SplitterPane Size="20%" ScrollBars="Auto">
                <ContentCollection>
                    <dx:SplitterContentControl runat="server">
                        <p>
                            <asp:Button ID="btnNuevaCuenta" runat="server" Text="Nueva Cuenta" OnClick="btnNuevaCuenta_OnClick" /></p>
                        <table>
                            <tr>
                                <td>
                                    <dx:ASPxTreeList ID="tlsOperadores" runat="server" AutoGenerateColumns="False" Font-Size="Smaller" Height="187px" KeyFieldName="intId" ParentFieldName="intIdPadre" Theme="Youthful">  <%--OnFocusedNodeChanged="btnNodeChanged_click"--%>
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
                                        <SettingsBehavior AllowFocusedNode="True" />
                                        <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
                                        <ClientSideEvents FocusedNodeChanged="function(s, e) { 
                                            pnlDatos.PerformCallback(); 
                                            }"
                                            />

                                    </dx:ASPxTreeList>
                                    <%--Init="function(s, e) {
                                    pnlDatos.PerformCallback();}"--%>
                                </td>
                            </tr>
                        </table>
                    </dx:SplitterContentControl>
                </ContentCollection>
            </dx:SplitterPane>
            <dx:SplitterPane ScrollBars="Auto">
                <ContentCollection>
                    <dx:SplitterContentControl runat="server">
                        <dx:ASPxCallbackPanel ID="pnlDatos" runat="server" Theme="Youthful" Width="100%" ClientInstanceName="pnlDatos" OnCallback="pnlDatos_Callback">
                            <%--<ClientSideEvents EndCallback="function(s, e) {
                                LoadingPanel.Hide();
                                OnEndCallback(s, e);
                                }"
                                BeginCallback="function(s, e) {
                                LoadingPanel.Show();
                                }" />--%>
                            <PanelCollection>
                                <dx:PanelContent runat="server">
                                    <h3>ASIGNAR PERFILES A USUARIOS</h3>
                                    <p class="descripcion">Asignar/Quitar Perfiles a un Usuario</p>
                                    <hr />
                                    <fieldset>
                                        <legend>USUARIO - MENÚS</legend>
                                        <table>
                                            <tr>
                                                <td valign="top">
                                                    <dx:ASPxGridView ID="grvListaUsuarios" runat="server" Theme="Youthful" AutoGenerateColumns="False" KeyFieldName="ID_USUARIO" Width="300px" >
                                                        <Settings VerticalScrollableHeight="150" ShowVerticalScrollBar="true" VerticalScrollBarStyle="Standard" />
                                                        <ClientSideEvents FocusedRowChanged="function(s, e) {treeMenuUsuario.Callback();}" />
                                                        <Columns>
                                                            <%--<dx:GridViewCommandColumn VisibleIndex="0">
                                                        </dx:GridViewCommandColumn>--%>
                                                            <dx:GridViewDataTextColumn FieldName="ID_USUARIO" VisibleIndex="1" Caption="ID_USUARIO_ENTIDAD" Visible="False">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn FieldName="ID_TIPO_ACTIVIDAD" VisibleIndex="1" Caption="ID_TIPO_ACTIVIDAD" Visible="False">
                                                            </dx:GridViewDataTextColumn>
                                                            <%--<dx:GridViewDataTextColumn FieldName="USUARIO" VisibleIndex="2" Caption="ID_USUARIO_ENTIDAD" VisibleIndex="2">
                                                        </dx:GridViewDataTextColumn>--%>
                                                            <dx:GridViewDataTextColumn Caption="USUARIO" FieldName="NOMBRE_USUARIO" VisibleIndex="3">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="ACTIVIDAD" FieldName="NOMBRE_ACTIVIDAD" VisibleIndex="3">
                                                            </dx:GridViewDataTextColumn>
                                                        </Columns>
                                                        <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" AllowSelectByRowClick="True" ProcessSelectionChangedOnServer="True" />
                                                    </dx:ASPxGridView>
                                                </td>
                                                <td>
                                                    <%--<asp:Button ID="btnVer" runat="server" OnClick="btnVer_Click" Text="Ver menu" />--%>
                                                    <dx:ASPxButton ID="btnVer2" AutoPostBack="False" runat="server" Font-Bold="True" ForeColor="#003300" Text="Ver menu" Theme="Metropolis" Width="50px" OnClick="btnVer_Click" />                                                       
                                                </td>

                                                <td valign="top">
                                                    <dx:ASPxTreeList ID="treeMenuUsuario" runat="server" Theme="Youthful" Height="150px" AutoGenerateColumns="False" KeyFieldName="ID_MENU" ParentFieldName="ID_MENU_PADRE" ClientInstanceName="treeMenuUsuario" Width="200px" Style="margin-left: 0px">
                                                        <Columns>
                                                            <dx:TreeListTextColumn FieldName="ID_MENU" VisibleIndex="0" Visible="False">
                                                            </dx:TreeListTextColumn>
                                                            <dx:TreeListTextColumn FieldName="ID_MENU_PADRE" VisibleIndex="1" Visible="False">
                                                            </dx:TreeListTextColumn>
                                                            <dx:TreeListTextColumn Caption="MENÚS DE LOS PERFILES DEL USUARIO PARA EL MÓDULO" FieldName="TITULO" VisibleIndex="2">
                                                            </dx:TreeListTextColumn>
                                                        </Columns>
                                                    </dx:ASPxTreeList>
                                                </td>
                                                <td valign="top">
                                                    <dx:ASPxGridView ID="grvListaEntidades" runat="server" Theme="Youthful" AutoGenerateColumns="False" Width="300"><%-- KeyFieldName="ID_ENTIDAD" --%>
                                                        <Settings VerticalScrollableHeight="150" ShowVerticalScrollBar="true" VerticalScrollBarStyle="Standard" />
                                                        <%--<ClientSideEvents FocusedRowChanged="function(s, e) {treeMenuUsuario.Callback();}" />--%>
                                                        <Columns>
                                                            <%--<dx:GridViewCommandColumn VisibleIndex="0">
                                                        </dx:GridViewCommandColumn>--%>
                                                           <%-- <dx:GridViewDataTextColumn FieldName="ID_ENTIDAD" VisibleIndex="1" Caption="ID_ENTIDAD" Visible="False">
                                                            </dx:GridViewDataTextColumn>--%>
                                                            <%--<dx:GridViewDataTextColumn FieldName="USUARIO" VisibleIndex="2" Caption="ID_USUARIO_ENTIDAD" VisibleIndex="2">
                                                        </dx:GridViewDataTextColumn>--%>
                                                            <dx:GridViewDataTextColumn Caption="ENTIDAD" FieldName="NOMBRE_ENTIDAD" VisibleIndex="3">
                                                            </dx:GridViewDataTextColumn>
                                                            <dx:GridViewDataTextColumn Caption="ACTIVIDAD" FieldName="NOMBRE_ACTIVIDAD" VisibleIndex="3">
                                                            </dx:GridViewDataTextColumn>
                                                        </Columns>
                                                       <%-- <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" AllowSelectByRowClick="True" ProcessSelectionChangedOnServer="True" />--%>
                                                    </dx:ASPxGridView>

                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                    <%--<dx:ASPxCallbackPanel ID="panelCallback" runat="server" Width="100%" ClientInstanceName="panelCallback">
                                        <PanelCollection><dx:PanelContent runat="server" SupportsDisabledAttribute="True">--%>
                                    <p class="mensaje" id="mensaje_usuario" runat="server"></p>
                                    <fieldset>
                                        <legend>USUARIO-PERFIL</legend>
                                        <table>
                                            <tr>
                                                <td>
                                                    <p class="subtitulo">PERFILES DISPONIBLES</p>
                                                    <hr />
                                                </td>
                                                <td>&nbsp;</td>
                                                <td>
                                                    <p class="subtitulo">PERFILES ASIGNADOS AL USUARIO</p>
                                                    <hr />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:ListBox ID="lbxPerfilesDisponibles" runat="server" Width="300px" DataTextField="NOMBRE_PERFIL" DataValueField="ID_PERFIL" Height="350px"></asp:ListBox>
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
                                                    <asp:ListBox ID="lbxPerfilesAsignados" runat="server" Width="300" DataTextField="NOMBRE_PERFIL" DataValueField="ID_PERFIL" Height="350px"></asp:ListBox>
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
    <dx:ASPxPopupControl ID="pucDatos" runat="server" AllowDragging="True" ClientInstanceName="pucDatos" EnableTheming="True" HeaderText="Asociación de nuevo usuario" Height="93px" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Theme="Office2003Olive" Width="334px">
        <HeaderImage IconID="setup_pagesetup_16x16"></HeaderImage>
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <%--<asp:HiddenField runat="server" ID="hidTest" ClientIDMode="Static" />--%>
                <%--<asp:RequiredFieldValidator ID="hidTestRequired" runat="server" ControlToValidate="hidTest" Display="None" ErrorMessage="Debe asociar al menos un proyecto al usuario" ValidationGroup="PersonaValidationGroup">*</asp:RequiredFieldValidator>--%>
                <table width="85%">
                    <%--width="100%"--%>
                    <tr>
                        <td style="vertical-align: top;">
                            <asp:ValidationSummary ID="PersonaValidationSummary" runat="server" DisplayMode="List" Visible="true" ForeColor="Red" ShowSummary="true" ValidationGroup="PersonaValidationGroup" />
                            <p class="mensaje_error" id="mensajeCreaUsuario" runat="server"></p>
                            <asp:HiddenField runat="server" ID="hidenEntidades" ClientIDMode="Static" />
                            <asp:HiddenField runat="server" ID="hidenPerfiles" ClientIDMode="Static" />
                            <%--<asp:RequiredFieldValidator ID="hidTestRequired" runat="server" ControlToValidate="hidTest" Display="None" ErrorMessage="Debe asociar al menos un proyecto al usuario" ValidationGroup="PersonaValidationGroup">*</asp:RequiredFieldValidator>--%>
                            <asp:Panel runat="server" ID="panelUsuario" DefaultButton="btnSiguiente">
                                <table>
                                    <%--<tr>
                                        <td align ="right"><asp:LinkButton   ID="lnkCerrar" runat="server" OnClick="lnkCerrar_OnClick" style="align-content: flex-end"><font style="font-size: 14px; color: #598024">Cerrar</font></asp:LinkButton></td>
                                    </tr>--%>
                                    <tr>
                                        <td>
                                            <h3>Solicitud de Acceso al Sistema </h3>
                                            <%--<uc1:CtrAnhDatosPersona ID="datosPersona" runat="server" OnNroIdentificacionTextChanged="datosPersonaTextChanged" OnTipoPersonaSelectedIndexChanged="datosPersonaSelectedIndexChange" />--%>
                                            <uc1:CtrAnhDatosPersona Width="650" ID="datosPersona" runat="server" OnNroIdentificacionTextChanged="datosPersonaTextChanged" OnTipoPersonaSelectedIndexChanged="datosPersonaSelectedIndexChange" />
                                            <fieldset>
                                                <h3>Credenciales de Acceso al Sistema</h3>
                                                <table id="tableContraseña" runat="server" width="100%">
                                                    <%--width="100%"--%>
                                                    <tr>
                                                        <td style="width: 50%"><%----%>Contraseña<span class="obligatorio">*</span>
                                                            <asp:Image ID="Image5" runat="server" AlternateText="Contraseña de acceso al sistema, no se refiere a la contraseña de su correo." CssClass="showtooltip icon_ayuda_16" ImageUrl="~/UI/img/pixel.gif" /><br>
                                                            <asp:TextBox ID="txtUsuarioPassword1" runat="server" TextMode="Password" Width="95%"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvPass1" runat="server" ControlToValidate="txtUsuarioPassword1" Display="None" ErrorMessage="La Contraseña es Obligatoria." ValidationGroup="PersonaValidationGroup">*</asp:RequiredFieldValidator>
                                                            <br />
                                                            <%--<ajaxToolkit:PasswordStrength ID="PasswordStrength2" runat="server" barbordercssclass="BarBorder_TextBox2" DisplayPosition="BelowLeft" preferredpasswordlength="8" strengthindicatortype="BarIndicator" strengthstyles="BarIndicator_TextBox2_weak;BarIndicator_TextBox2_average;BarIndicator_TextBox2_good" targetcontrolid="txtUsuarioPassword1" />--%>
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
                                                        <td>Entidad(es) a ser asociadas con el usuario<br>
                                                            <select id="cmbEntidades" class="chosen-select" data-placeholder="Seleccione entidades para ser asociadas con el usuario creado... " multiple style="width: 100%" tabindex="4">
                                                            </select>
                                                            <select id="Select1" runat="server" style="width: 100%" visible="False">
                                                            </select>
                                                            <%--<asp:RequiredFieldValidator ID="rfvCmbEntidades" runat="server" ControlToValidate="cmbEntidades" Display="None" ErrorMessage="Debe asociar entidades al usuario..." ValidationGroup="PersonaValidationGroup">*</asp:RequiredFieldValidator>--%>
                                                        </td>
                                                        <%--</tr>
                                                    <tr align="center">--%>
                                                        <td>Perfil(es) a ser asociadas con el usuario<br>
                                                            <select id="cmbPerfiles" class="chosen-select" data-placeholder="Seleccione los perfiles a ser asociadas con el usuario creado... " multiple style="width: 100%;" tabindex="4">
                                                            </select>
                                                            <select id="Select2" runat="server" style="width: 100%" visible="False">
                                                            </select>
                                                            <%--<asp:RequiredFieldValidator ID="rfvCmbEntidades" runat="server" ControlToValidate="cmbEntidades" Display="None" ErrorMessage="Debe asociar entidades al usuario..." ValidationGroup="PersonaValidationGroup">*</asp:RequiredFieldValidator>--%>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <p align="justify">
                                                    El correo electónico ingresado será su usuario de acceso al sistema.
                                                    <br>
                                                    Asegurese de introducirlos correctamente y recordalos. 
                                                </p>
                                            </fieldset>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table width="90%">
                                                <tr>
                                                    <td align="left">
                                                        <asp:ImageButton ID="btnVolver" runat="server" ImageUrl="~/UI/img/empadronamiento/btn_inicio_volver.png"
                                                            OnClick="btnVolver_Click" OnClientClick="showLoading()" CausesValidation="false" />
                                                    </td>
                                                    <td align="right">
                                                        <%--<button id="btnAgregarSuperficie" style="float: right; padding-left: 20px; padding-right: 20px" >Siguiente</button>--%>
                                                        <asp:ImageButton ID="btnSiguiente" runat="server" ValidationGroup="PersonaValidationGroup"
                                                            OnClick="btnSiguiente_Click" ImageUrl="~/UI/img/empadronamiento/btn_inicio_siguiente.png"
                                                            OnClientClick="showLoading('PersonaValidationGroup')" />
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
                                        <td align="left">
                                            <asp:ImageButton runat="server" ID="btnVolver2" CausesValidation="false" ImageUrl="~/UI/img/empadronamiento/btn_inicio_volver.png"
                                                OnClientClick="showLoading()" OnClick="btnVolver2_Click" ValidationGroup="Volver" />
                                        </td>
                                        <td align="right">
                                            <asp:Button ID="btnRegistrar" runat="server" OnClick="btnRegistrarInformacion_Click" Text="Registrar Información" class="auto-style2" Height="38px" Width="275px" BorderColor="gray"
                                                OnClientClick="if(!confirm('Asegurese de haber revisado la información ingresada. \nSeguro que desea registrar la información?')){return false;} else{showLoading();}" />
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
