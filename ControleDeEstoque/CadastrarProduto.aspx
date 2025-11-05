<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CadastrarProduto.aspx.cs" Inherits="ControleDeEstoque.CadastrarProduto" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Cadastrar Produto</title>
    <link href="StyleSheetMaster.css" rel="stylesheet" />
</head>

<body>
    <form id="form1" runat="server">
        <!-- Cabeçalho do sistema -->
        <div class="cabecalho">
            <div class="logomarca">
                <asp:Image ID="Logomarca" ImageUrl="Recusos/img/logo.png" runat="server" />
            </div>
        </div>

        <!-- Container principal do formulário -->
        <div class="form-container">
            <h2 class="form-titulo">➕ Cadastrar Novo Produto</h2>

            <!-- Mensagem dinâmica de sucesso/erro -->
            <asp:Label ID="lblMensagem" runat="server" CssClass="form-mensagem" Visible="false"></asp:Label>

            <!-- Campo: Nome do Produto -->
            <div class="form-grupo">
                <label>Nome do Produto <span class="form-campo-obrigatorio">*</span></label>
                <asp:TextBox ID="txtNomeProduto" runat="server" placeholder="Digite o nome do Produto para cadastro"></asp:TextBox>
                <div class="form-ajuda">Mínimo 3 caracteres</div>
            </div>

            <!-- Campo: Código do Produto -->
            <div class="form-grupo">
                <label>Código <span class="form-campo-obrigatorio">*</span></label>
                <asp:TextBox ID="txtCodigoProduto" runat="server" placeholder="Digite o Código do Produto"></asp:TextBox>
                <div class="form-ajuda">Mínimo 2 caracteres</div>
            </div>

            <!-- Campo: Fornecedor -->
            <div class="form-grupo">
                <label>Fornecedor <span class="form-campo-obrigatorio">*</span></label>
                <asp:TextBox ID="txtFornecedor" runat="server" placeholder="Digite o nome do fornecedor"></asp:TextBox>
                <div class="form-ajuda">Nome do fornecedor deste produto</div>
            </div>

            <!-- Campo: Quantidade -->
            <div class="form-grupo">
                <label>Quantidade em Estoque <span class="form-campo-obrigatorio">*</span></label>
                <asp:TextBox ID="txtQntdEstoque" runat="server" placeholder="0" TextMode="Number"></asp:TextBox>
            </div>

            <!-- Campo: Preço de custo -->
            <div class="form-grupo">
                <label>Valor unitário <span class="form-campo-obrigatorio">*</span></label>
                <asp:TextBox ID="txtPrecoCusto" runat="server" placeholder="Digite o valor unitário"></asp:TextBox>
            </div>

            <!-- Campo: Preço de venda -->
            <div class="form-grupo">
                <label>Valor unitário de venda <span class="form-campo-obrigatorio">*</span></label>
                <asp:TextBox ID="txtPrecoVenda" runat="server" placeholder="Digite o valor unitário de venda"></asp:TextBox>
            </div>

            <!-- Checkbox: Produto ativo -->
            <div class="form-grupo">
                <div class="form-checkbox-grupo">
                    <asp:CheckBox ID="chkAtivo" runat="server" Checked="true" />
                    <label for="<%= chkAtivo.ClientID %>">Produto ativo</label>
                </div>
                <div class="form-ajuda">Apenas produtos ativos podem ser vendidos</div>
            </div>

            <!-- Campo: Descrição opcional -->
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
