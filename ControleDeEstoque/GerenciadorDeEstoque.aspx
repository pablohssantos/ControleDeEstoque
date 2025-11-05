<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="GerenciadorDeEstoque.aspx.cs" Inherits="ControleDeEstoque.GerenciadorDeEstoque" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="conteudo" runat="server">

    <div class="gerenciar-container">

        <h2 class="titulo-gerenc-container">Cadastrar Produtos</h2>

        <%-- Mensagem de feedback exibida após ações como salvar, editar ou excluir --%>
        <asp:Label ID="lblMensagem" runat="server" CssClass="gerenciar-mensagem" Visible="false"></asp:Label>

        <%-- Barra de pesquisa e botão para cadastrar novo produto --%>
        <div class="gerenciar-barra-topo">
            <div class="gerenciar-pesquisa-box">
                <asp:TextBox ID="txtPesquisa" runat="server" Placeholder="Pesquisar produto por nome ou código..."></asp:TextBox>
                <asp:Button ID="btnPesquisar" runat="server" Text="🔍 Pesquisar" CssClass="gerenciar-btn-pesquisar" OnClick="btnPesquisar_Click" />
            </div>
            <asp:Button ID="btnNovo" runat="server" Text="➕ Novo Produto" CssClass="gerenciar-btn-novo" OnClick="btnNovo_Click" />
        </div>

        <%-- Grade com a listagem dos produtos cadastrados --%>
        <asp:GridView ID="gvProdutos" runat="server"
            CssClass="gerenciar-gridview"
            AutoGenerateColumns="False"
            DataKeyNames="id"
            OnRowCommand="gvProdutos_RowCommand"
            OnRowDataBound="gvProdutos_RowDataBound"
            EmptyDataText="Nenhum Produto encontrado.">

            <Columns>

                <%-- Colunas básicas de identificação e informações do produto --%>
                <asp:BoundField DataField="id" HeaderText="ID" ItemStyle-Width="50px" />
                <asp:BoundField DataField="nome" HeaderText="Produto" />
                <asp:BoundField DataField="codigo" HeaderText="Código" ItemStyle-Width="100px" />
                <asp:BoundField DataField="fornecedor" HeaderText="Fornecedor" ItemStyle-Width="150px" />
                <asp:BoundField DataField="quantidadeEstoque" HeaderText="Estoque" ItemStyle-Width="80px" />
                <asp:BoundField DataField="precoCusto" HeaderText="Custo" DataFormatString="{0:C2}" />
                <asp:BoundField DataField="precoVenda" HeaderText="Venda" DataFormatString="{0:C2}" />

                <%-- Exibe o status visual (ativo ou inativo) do produto --%>
                <asp:TemplateField HeaderText="Status">
                    <ItemTemplate>
                        <span class='badge <%# Convert.ToBoolean(Eval("ativo")) ? "badge-ativo" : "badge-inativo" %>'>
                            <%# Convert.ToBoolean(Eval("ativo")) ? "Ativo" : "Inativo" %>
                        </span>
                    </ItemTemplate>
                </asp:TemplateField>

                <%-- Data de criação do registro --%>
                <asp:BoundField DataField="dataCadastro" HeaderText="Data Criação" DataFormatString="{0:dd/MM/yyyy HH:mm}" />

                <%-- Ações disponíveis: editar e ativar/desativar produto --%>
                <asp:TemplateField HeaderText="Ações" ItemStyle-Width="200px">
                    <ItemTemplate>
                        <div class="gerenciar-acoes-celula">
                            <asp:Button ID="btnEditar" runat="server"
                                Text="✏️ Editar"
                                CssClass="gerenciar-btn-editar"
                                CommandName="Editar"
                                CommandArgument='<%# Eval("id") %>' />

                            <asp:Button ID="btnToggleAtivo" runat="server"
                                Text='<%# Convert.ToBoolean(Eval("ativo")) ? "🚫 Desativar" : "✅ Ativar" %>'
                                CssClass='<%# Convert.ToBoolean(Eval("ativo")) ? "gerenciar-btn-desativar" : "gerenciar-btn-ativar" %>'
                                CommandName="ToggleAtivo"
                                CommandArgument='<%# Eval("id") %>'
                                OnClientClick="return confirm('Tem certeza que deseja alterar o status deste produto?');" />
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
        </asp:GridView>

    </div>

</asp:Content>























