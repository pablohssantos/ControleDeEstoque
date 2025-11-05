<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditarUsuario.aspx.cs" Inherits="ControleDeEstoque.EditarUsuario" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <!-- Importa o arquivo de estilo principal -->
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

        <!-- Container principal do formulário -->
        <div class="form-container">
            <h2 class="form-titulo-editar">✏️ Editar Usuário</h2>

            <!-- Exibe mensagens de sucesso ou erro -->
            <asp:Label ID="lblMensagem" runat="server" CssClass="form-mensagem" Visible="false"></asp:Label>

            <!-- Exibe o ID do usuário sendo editado -->
            <div class="form-info-box">
                <strong>ID do Usuário:</strong>
                <asp:Label ID="lblId" runat="server"></asp:Label>
            </div>

            <!-- Campo de nome completo -->
            <div class="form-grupo">
                <label>Nome Completo <span class="form-campo-obrigatorio">*</span></label>
                <asp:TextBox ID="txtNome" runat="server" placeholder="Digite o nome completo do funcionário"></asp:TextBox>
            </div>

            <!-- Campo de nome de usuário (login) -->
            <div class="form-grupo">
                <label>Nome de Usuário <span class="form-campo-obrigatorio">*</span></label>
                <asp:TextBox ID="txtUsuario" runat="server" placeholder="Digite o nome de usuário para login"></asp:TextBox>
                <div class="form-ajuda">Este será o nome usado para fazer login no sistema</div>
            </div>

            <!-- Campo de senha opcional -->
            <div class="form-grupo">
                <label>Nova Senha (deixe em branco para manter a atual)</label>
                <asp:TextBox ID="txtSenha" runat="server" TextMode="Password" placeholder="Digite a nova senha (opcional)"></asp:TextBox>
                <div class="form-ajuda">Deixe vazio se não quiser alterar a senha</div>
            </div>

            <!-- Confirmação da nova senha -->
            <div class="form-grupo">
                <label>Confirmar Nova Senha</label>
                <asp:TextBox ID="txtConfirmarSenha" runat="server" TextMode="Password" placeholder="Confirme a nova senha"></asp:TextBox>
            </div>

            <!-- Seleção de tipo de usuário -->
            <div class="form-grupo">
                <label>Tipo de Usuário <span class="form-campo-obrigatorio">*</span></label>
                <asp:DropDownList ID="ddlTipoUsuario" runat="server">
                    <asp:ListItem Value="Usuario">Usuário Comum</asp:ListItem>
                    <asp:ListItem Value="Admin">Administrador</asp:ListItem>
                </asp:DropDownList>
                <div class="form-ajuda">Administradores podem gerenciar usuários</div>
            </div>

            <!-- Checkbox de status do usuário -->
            <div class="form-grupo">
                <div class="form-checkbox-grupo">
                    <asp:CheckBox ID="chkAtivo" runat="server" />
                    <label for="<%= chkAtivo.ClientID %>">Usuário ativo</label>
                </div>
                <div class="form-ajuda">Apenas usuários ativos podem fazer login</div>
            </div>

            <!-- Botões de ação -->
            <div class="form-botoes">
                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="form-btn-cancelar" OnClick="btnCancelar_Click" CausesValidation="false" />
                <asp:Button ID="btnSalvar" runat="server" Text="💾 Salvar Alterações" CssClass="form-btn-salvar-editar" OnClick="btnSalvar_Click" />
            </div>
        </div>
    </form>
</body>
</html>
