<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wfAutenticacionS.aspx.cs" Inherits="AnhHydroSisgenPresentacion.Sitio.Persona.wfAutenticacion" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../../UI/bootstrap/js/bootstrap.min.js"></script>
    <link href="../../UI/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <style>

        
    </style>
</head>
<body>
    <div class="container">
        <form id="form1" runat="server" class="form-horizontal">
            <div class="row">
                <div class="col-sm-4 col-sm-offset-3 col-lg-3 form-box">
                    <img src="../../UI/img/hydro_logo.png" width="100%" /><br />
                </div>

                <div class="col-xs-12 col-sm-5 col-sm-offset-3 text">
                    <h3><strong>Palataforma Única Informática</strong></h3>
                    <div class="description">
                        <p>
                            Plataforma única informática de la Agencia Nacional de Hidrocarburos donde se encuentran todos los módulos de administración y control por áreas.
                        </p>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-sm-4 col-sm-offset-3 col-lg-5 form-box">
                    <div class="text-center">
                        <span class="glyphicon glyphicon-user" style="font-size: 70px"></span>
                    </div>
                    <h2 class="text-center login-title">Iniciar sesión</h2>
                    <div class="text-center alert alert-warning" id="divMensaje" runat="server" visible="false">
                        <asp:Label ID="lblMensajeError" runat="server" Text=""></asp:Label>
                    </div>
                    <asp:Login ID="loginUsuario" runat="server" OnAuthenticate="loginUsuario_Authenticate" OnLoggedIn="loginUsuario_LoggedIn" Width="100%">
                        <LayoutTemplate>
                            <form role="form" method="post" class="form-horizontal">
                                <div class="form-group">
                                    <asp:Label ID="Label1" runat="server" Text="Usuario" CssClass="control-label col-sm-3"></asp:Label>
                                    <div class="col-sm-7">
                                        <asp:TextBox ID="UserName" runat="server" CssClass="form-control" Width="100%"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" CssClass="text-danger" ErrorMessage="El nombre de usuario es obligatorio." ToolTip="El nombre de usuario es obligatorio." ValidationGroup="loginUsuario">*</asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="Label2" runat="server" Text="Contraseña" CssClass="control-label col-sm-3"></asp:Label>
                                    <div class="col-sm-7">
                                        <asp:TextBox ID="Password" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" CssClass="text-danger" ErrorMessage="La contraseña es obligatoria." ToolTip="La contraseña es obligatoria." ValidationGroup="loginUsuario">*</asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-10 text-center">
                                        <asp:Button ID="LoginButton" runat="server" CommandName="Login" CssClass="btn btn-default" Text="Inicio de sesión" ValidationGroup="loginUsuario" />
                                    </div>
                                </div>
                            </form>
                        </LayoutTemplate>
                    </asp:Login>

                </div>
            </div>
            <div class="backstretch" style="left: 0px; top: 0px; overflow: hidden; margin: 0px; padding: 0px; height: 100%; width: 100%; z-index: -999999; position: fixed;">
                <img src="../../UI/img/bg.jpg" style="position: absolute; margin: 0px; padding: 0px; border: none; width: 100%; height: 100%; max-height: none; max-width: none; z-index: -999999; top: 0px;">
            </div>
        </form>
    </div>
</body>
</html>
