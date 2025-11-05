<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ControleEntrada.aspx.cs" Inherits="ControleDeEstoque.ControleEntrada" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="conteudo" runat="server">
    <div class="gerenciar-container">
        <!-- Título principal da página -->
        <h2 class="titulo-gerenc-container">Registrar Entrada de Produto</h2>

        <!-- Mensagem de feedback (sucesso ou erro) -->
        <asp:Label ID="lblMensagem" runat="server" CssClass="gerenciar-mensagem" Visible="false"></asp:Label>

        <!-- Barra de pesquisa de produtos -->
        <div class="gerenciar-barra-topo">
            <div class="gerenciar-pesquisa-box">
                <asp:TextBox ID="txtPesquisa" runat="server" Placeholder="Pesquisar produto por ID ou nome..."></asp:TextBox>
                <asp:Button ID="btnPesquisar" runat="server" Text="Pesquisar" CssClass="gerenciar-btn-pesquisar" OnClick="btnPesquisar_Click" />
            </div>
        </div>

        <!-- Lista de produtos disponíveis (GridView) -->
        <asp:GridView ID="gvProdutos" runat="server"
            CssClass="gerenciar-gridview"
            AutoGenerateColumns="False"
            DataKeyNames="id"
            OnRowCommand="gvProdutos_RowCommand"
            EmptyDataText="Nenhum produto encontrado. Use a pesquisa acima.">

            <Columns>
                <asp:BoundField DataField="id" HeaderText="ID" ItemStyle-Width="50px" />
                <asp:BoundField DataField="nome" HeaderText="Produto" />
                <asp:BoundField DataField="codigo" HeaderText="Código" ItemStyle-Width="100px" />
                <asp:BoundField DataField="quantidadeEstoque" HeaderText="Estoque Atual" ItemStyle-Width="100px" />
                <asp:BoundField DataField="precoCusto" HeaderText="Custo" DataFormatString="{0:C2}" ItemStyle-Width="100px" />


                <asp:TemplateField HeaderText="Ação" ItemStyle-Width="150px">
                    <ItemTemplate>
                        <asp:Button ID="btnSelecionarEntrada" runat="server"
                            Text="Dar Entrada"
                            CssClass="gerenciar-btn-editar"
                            CommandName="SelecionarEntrada"
                            CommandArgument='<%# Eval("id") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <!-- Modal exibido ao selecionar um produto -->
        <asp:Panel ID="pnlEntrada" runat="server" Visible="false" CssClass="modal-overlay">
            <div class="modal-content">
                <h3 class="modal-titulo">Registrar Entrada</h3>

                <!-- Informações básicas do produto selecionado -->
                <div class="form-info-box">
                    <strong>Produto:</strong> <asp:Label ID="lblProdutoNome" runat="server"></asp:Label><br />
                    <strong>Código:</strong> <asp:Label ID="lblProdutoCodigo" runat="server"></asp:Label><br />
                    <strong>Estoque Atual:</strong> <asp:Label ID="lblEstoqueAtual" runat="server"></asp:Label> unidades
                </div>

                <!-- Campo para quantidade de entrada -->
                <div class="form-grupo">
                    <label>Quantidade <span class="form-campo-obrigatorio">*</span></label>
                    <asp:TextBox ID="txtQuantidade" runat="server" TextMode="Number" placeholder="Digite a quantidade"></asp:TextBox>
                    <div class="form-ajuda">Quantidade a adicionar ao estoque</div>
                </div>

                <!-- Campo de fornecedor -->
                <div class="form-grupo">
                    <label>Fornecedor <span class="form-campo-obrigatorio">*</span></label>
                    <asp:TextBox ID="txtFornecedor" runat="server" placeholder="Nome do fornecedor"></asp:TextBox>
                </div>

                <!-- Campo opcional para nota fiscal -->
                <div class="form-grupo">
                    <label>Nota Fiscal</label>
                    <asp:TextBox ID="txtNotaFiscal" runat="server" placeholder="Número da nota fiscal (opcional)"></asp:TextBox>
                </div>

                <!-- Campo opcional para observações -->
                <div class="form-grupo">
                    <label>Observação</label>
                    <asp:TextBox ID="txtObservacao" runat="server"
                        TextMode="MultiLine"
                        Rows="3"
                        placeholder="Observações sobre esta entrada (opcional)..."
                        Style="width: 100%;">
                    </asp:TextBox>
                </div>

                <!-- Botões de ação -->
                <div class="form-botoes">
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="form-btn-cancelar" OnClick="btnCancelar_Click" CausesValidation="false" />
                    <asp:Button ID="btnConfirmarEntrada" runat="server" Text="Confirmar Entrada" CssClass="form-btn-salvar" OnClick="btnConfirmarEntrada_Click" />
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
