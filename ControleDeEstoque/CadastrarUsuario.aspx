<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CadastrarUsuario.aspx.cs" Inherits="ControleDeEstoque.CadastrarUsuario" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="StyleSheetMaster.css" rel="stylesheet" />
</head>
<body>
    <!-- Formulário principal da página de cadastro de usuário -->
    <form id="form1" runat="server">
        
        <!-- Cabeçalho do sistema com logomarca -->
        <div class="cabecalho">
            <div class="logomarca">
                <asp:Image ID="Logomarca" ImageUrl="Recusos/img/logo.png" runat="server" />
            </div>
        </div>

        <!-- Container principal do formulário -->
        <div class="form-container">
            <h2 class="form-titulo">➕ Cadastrar Novo Usuário</h2>

            <!-- Label para exibir mensagens de sucesso ou erro -->
            <asp:Label ID="lblMensagem" runat="server" CssClass="form-mensagem" Visible="false"></asp:Label>

            <!-- Campo: Nome completo do funcionário -->
            <div class="form-grupo">
                <label>Nome Completo <span class="form-campo-obrigatorio">*</span></label>
                <asp:TextBox ID="txtNome" runat="server" placeholder="Digite o nome completo do funcionário"></asp:TextBox>
            </div>

            <!-- Campo: Nome de usuário para login -->
            <div class="form-grupo">
                <label>Nome de Usuário <span class="form-campo-obrigatorio">*</span></label>
                <asp:TextBox ID="txtUsuario" runat="server" placeholder="Digite o nome de usuário para login"></asp:TextBox>
                <div class="form-ajuda">Este será o nome usado para fazer login no sistema</div>
            </div>

            <!-- Campo: Senha -->
            <div class="form-grupo">
                <label>Senha <span class="form-campo-obrigatorio">*</span></label>
                <asp:TextBox ID="txtSenha" runat="server" TextMode="Password" placeholder="Digite a senha"></asp:TextBox>
                <div class="form-ajuda">Mínimo 4 caracteres</div>
            </div>

            <!-- Campo: Confirmar senha -->
            <div class="form-grupo">
                <label>Confirmar Senha <span class="form-campo-obrigatorio">*</span></label>
                <asp:TextBox ID="txtConfirmarSenha" runat="server" TextMode="Password" placeholder="Digite a senha novamente"></asp:TextBox>
            </div>

            <!-- Campo: Seleção de tipo de usuário -->
            <div class="form-grupo">
                <label>Tipo de Usuário <span class="form-campo-obrigatorio">*</span></label>
                <asp:DropDownList ID="ddlTipoUsuario" runat="server">
                    <asp:ListItem Value="Usuario" Selected="True">Usuário Comum</asp:ListItem>
                    <asp:ListItem Value="Admin">Administrador</asp:ListItem>
                </asp:DropDownList>
                <div class="form-ajuda">Administradores podem gerenciar usuários</div>
            </div>

            <!-- Opção: Marcar se o usuário está ativo -->
            <div class="form-grupo">
                <div class="form-checkbox-grupo">
                    <asp:CheckBox ID="chkAtivo" runat="server" Checked="true" />
                    <label for="<%= chkAtivo.ClientID %>">Usuário ativo</label>
                </div>
                <div class="form-ajuda">Apenas usuários ativos podem fazer login</div>
            </div>

            <!-- Botões de ação -->
            <div class="form-botoes">
                <!-- Cancela o cadastro e volta para a tela anterior -->
                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="form-btn-cancelar" OnClick="btnCancelar_Click" CausesValidation="false" />
                
                <!-- Salva o novo usuário no banco de dados -->
                <asp:Button ID="btnSalvar" runat="server" Text="💾 Salvar Usuário" CssClass="form-btn-salvar" OnClick="btnSalvar_Click" />
            </div>
        </div>
    </form>
</body>
</html>
