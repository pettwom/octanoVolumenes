<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wfAutenticacion.aspx.cs" Inherits="AnhHydroOctanoPresentacion.Sitio.Persona.wfAutenticacion" %>

<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>ANH - OCTANO VOLUMENES</title>
    <link rel="shortcut icon" href="../../UI/img/Volumenes/octano128.ico" type="image/x-icon" />
    <link href="../../UI/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../UI/css/hydro_login.css" rel="stylesheet" />
    <link href="../../UI/css/icons.css" rel="stylesheet" />
</head>
<body>
    <div class="wrapper">
        <div class="row">
            <div class="col-lg-12 col-md-12">
                <img src="../../UI/img/anh_logo.png" class="div_left img_anh_logo" /><img src="../../UI/img/hydro_logo.png" class="div_right img_hydro_logo" />

                <div class="containerlg">
                    <div class="text-center alert alert-warning" id="divMensaje" runat="server" visible="false">
                        <asp:Label ID="lblMensajeError" runat="server" Text=""></asp:Label>
                    </div>
                    <img src="../../UI/img/nacho.png" class="div_left img_nacho_logo" />
                    <div class="div_nombre_sistema">
                        Bienvenido al sistema:
                       
                        <br />
                        <br />
                        <div class="hydro_div_line_short">
                            <span class="hydro_nombre_sistema">OCTANO</span>
                            <br />
                            <span class="hydro_nombre_sistema_sub">sistema octano volúmenes</span>
                        </div>
                    </div>
                    <form id="form2" runat="server" role="form" method="post" class="form">
                        <asp:Login ID="loginUsuario" runat="server" OnAuthenticate="LoginUsuario_Authenticate" OnLoggedIn="LoginUsuario_LoggedIn">
                            <LayoutTemplate>

                                <div class="form_line">
                                    <i class="icons icons-user icon-style"></i>
                                    <asp:TextBox ID="UserName" runat="server" CssClass="loginInput" name="UserName"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" CssClass="text-danger" ErrorMessage="El nombre de usuario es obligatorio." ToolTip="El nombre de usuario es obligatorio." ValidationGroup="loginUsuario">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="form_line">
                                    <i class="icons icons-keys icon-style"></i>
                                    <asp:TextBox ID="Password" runat="server" CssClass="loginInput" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" CssClass="text-danger" ErrorMessage="La contraseña es obligatoria." ToolTip="La contraseña es obligatoria." ValidationGroup="loginUsuario">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="form_line">
                                    <span style="color: white;">Escriba el código mostrado</span>
                                    <dx:ASPxCaptcha ID="Captcha_ID" runat="server" CodeLength="6" Theme="Moderno" style="text-align: center">
                                        <ChallengeImage BorderWidth="1" Width="285"></ChallengeImage>
                                        <ValidationSettings ErrorText="Código incorrecto"></ValidationSettings>
                                        <TextBox LabelText="Escriba el código mostrado" Position="Top" ShowLabel="false"/>
                                        <TextBoxStyle VerticalAlign="Middle" HorizontalAlign="Center" Width="285px"/>
                                        <RefreshButton Text="Mostrar otro código" Position="Bottom"></RefreshButton>
                                    </dx:ASPxCaptcha>                                
                                </div>
                                <div class="form_line">
                                    <asp:Button ID="LoginButton" runat="server" CommandName="Login" CssClass="btnLogin" Text="Inicio de Sesión" Width="100%" ValidationGroup="loginUsuario" />
                                </div>

                            </LayoutTemplate>
                        </asp:Login>
                    </form>
<%--                    <div class="row_buttons">
                        <a class="side_buttons"href="https://ppa3vrsger02p.anh.gob.bo:9343/H201Y7070D4/Sitio/Persona/wfRecuperacionCuenta.aspx">
                            <div class="hydro_class_div_multilinea"><i class="icons icons-lock icon-style3"></i><br/>Restituir contraseña</div>
                        </a>
                        <a class="side_buttons side_buttons_extra"href="https://ppa3vrsger02p.anh.gob.bo:9343/H201Y7070D4/Sitio/Persona/wfSolicitarCuenta.aspx">
                            <div class="hydro_class_div_multilinea"><i class="icons icons-user icon-style3"></i><br/>Crear<br/>Cuenta</div>
                        </a>
                    </div>--%>
                </div>     
                <ul class="bg-bubbles">
                    <li></li>
                    <li></li>
                    <li></li>
                    <li></li>
                    <li></li>
                    <li></li>
                    <li></li>
                    <li></li>
                    <li></li>
                    <li></li>
                </ul>
            </div>
        </div>

        <div class="wraper_footer">
            Agencia Nacional de Hidrocarburos | Av. 20 de Octubre Nro. 2685 | NIT: 120919027 | La Paz, Bolivia<br />
            Linea Gratuita: 800-10-6006 | Tel: (591)-2-2614000 | Fax: (591)-2-2434007 | Email: contacto @ anh.gob.bo<br />
            Desarrollo y mantenimiento: Dirección de Tecnologías de Información y Comunicación<br />
            AGENCIA NACIONAL DE HIDROCARBUROS &copy; | <%: DateTime.Now.Year %>
   
        </div>

    </div>

</body>
</html>
