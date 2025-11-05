<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ControleDeEstoque.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="StyleSheetMaster.css" rel="stylesheet" />
</head>

<body>
    <form id="form1" runat="server">

        <!-- Cabeçalho com logomarca -->
        <div class="cabecalho">
            <div class="logomarca">
                <asp:Image ID="Logomarca" ImageUrl="Recusos/img/logo.png" runat="server" />
            </div>
        </div>

        <!-- Container principal da área de login -->
        <div class="login-container">
            <div class="login-box">
                <h2>Login</h2>

                <!-- Campos de entrada para usuário e senha -->
                <asp:TextBox ID="txtUsuario" runat="server" Placeholder="Usuário"></asp:TextBox>
                <asp:TextBox ID="txtSenha" runat="server" TextMode="Password" Placeholder="Senha"></asp:TextBox>

                <!-- Botão de login do usuário padrão -->
                <asp:Button ID="btnLogin" runat="server" Text="Entrar" CssClass="btn-login" OnClick="btnLogin_Click" />

                <!-- Label para exibição de mensagens de erro ou status -->
                <asp:Label ID="lblMensagem" runat="server" CssClass="mensagem"></asp:Label>

                <!-- Seção de login administrativo -->
                <asp:Label ID="gerencairLog" runat="server" Text="Gerenciar Logins"></asp:Label>
                <asp:Button ID="btnAdminLogin" runat="server" Text="Entrar" CssClass="btn-login-adm" OnClick="btnLoginAdmin_Click" />
            </div>
        </div>

    </form>
</body>
</html>
