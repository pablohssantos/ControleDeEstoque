<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditarProduto.aspx.cs" Inherits="ControleDeEstoque.EditarProduto" %>

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

        <!-- Container principal do formulário de edição -->
        <div class="form-container">
            <h2 class="form-titulo">➕ Editar Produto</h2>

            <!-- Exibe mensagens de sucesso ou erro -->
            <asp:Label ID="lblMensagem" runat="server" CssClass="form-mensagem" Visible="false"></asp:Label>

            <!-- Campo: Nome do produto -->
            <div class="form-grupo">
                <label>Nome do Produto <span class="form-campo-obrigatorio">*</span></label>
                <asp:TextBox ID="txtNomeProduto" runat="server" placeholder="Digite o nome do Produto para cadastro"></asp:TextBox>
                <div class="form-ajuda">Mínimo 3 caracteres</div>
            </div>

            <!-- Campo: Código do produto -->
            <div class="form-grupo">
                <label>Código <span class="form-campo-obrigatorio">*</span></label>
                <asp:TextBox ID="txtCodigoProduto" runat="server" placeholder="Digite o Código do Produto"></asp:TextBox>
                <div class="form-ajuda">Mínimo 2 caracteres</div>
            </div>

            <!-- Campo: Quantidade em estoque (somente leitura) -->
            <div class="form-grupo">
                <label>Quantidade em Estoque</label>
                <asp:TextBox ID="txtQntdEstoque" runat="server"
                    TextMode="Number"
                    ReadOnly="true"
                    CssClass="campo-readonly">
                </asp:TextBox>
                <div class="form-ajuda">A quantidade só pode ser alterada na movimentação de estoque</div>
            </div>

            <!-- Campo: Valor unitário -->
            <div class="form-grupo">
                <label>Valor unitário <span class="form-campo-obrigatorio">*</span></label>
                <asp:TextBox ID="txtPrecoCusto" runat="server" placeholder="Digite o valor unitário"></asp:TextBox>
            </div>

            <!-- Campo: Valor de venda -->
            <div class="form-grupo">
                <label>Valor unitário de venda <span class="form-campo-obrigatorio">*</span></label>
                <asp:TextBox ID="txtPrecoVenda" runat="server" placeholder="Digite o valor unitário de venda"></asp:TextBox>
            </div>

            <!-- Checkbox para indicar se o produto está ativo -->
            <div class="form-grupo">
                <div class="form-checkbox-grupo">
                    <asp:CheckBox ID="chkAtivo" runat="server" Checked="true" />
                    <label for="<%= chkAtivo.ClientID %>">Produto ativo</label>
                </div>
                <div class="form-ajuda">Apenas produtos ativos podem ser vendidos</div>
            </div>

            <!-- Campo: Descrição do produto -->
            <div class="form-grupo">
                <label>Descrição</label>
                <asp:TextBox ID="txtDescricao" runat="server"
                    TextMode="MultiLine"
                    Rows="4"
                    placeholder="Descrição detalhada do produto (opcional)..."
                    Style="width: 100%;">
                </asp:TextBox>
            </div>

            <!-- Botões de ação -->
            <div class="form-botoes">
                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="form-btn-cancelar" OnClick="btnCancelar_Click" CausesValidation="false" />
                <asp:Button ID="btnSalvar" runat="server" Text="💾 Salvar Produto" CssClass="form-btn-salvar" OnClick="btnSalvar_Click" />
            </div>
        </div>
    </form>
</body>
</html>
