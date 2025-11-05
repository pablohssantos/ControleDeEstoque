<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ControleSaida.aspx.cs" Inherits="ControleDeEstoque.ControleSaida" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="conteudo" runat="server">
    <div class="gerenciar-container">
        <h2 class="titulo-gerenc-container">📤 Registrar Saída de Produto</h2>

        <!-- Label usada para mensagens de erro ou sucesso -->
        <asp:Label ID="lblMensagem" runat="server" CssClass="gerenciar-mensagem" Visible="false"></asp:Label>

        <!-- Área de pesquisa de produtos -->
        <div class="gerenciar-barra-topo">
            <div class="gerenciar-pesquisa-box">
                <asp:TextBox ID="txtPesquisa" runat="server" Placeholder="Pesquisar produto por ID ou nome..."></asp:TextBox>
                <asp:Button ID="btnPesquisar" runat="server" Text="🔍 Pesquisar" CssClass="gerenciar-btn-pesquisar" OnClick="btnPesquisar_Click" />
            </div>
        </div>

        <!-- GridView que exibe os produtos do estoque -->
        <asp:GridView ID="gvProdutos" runat="server"
            CssClass="gerenciar-gridview"
            AutoGenerateColumns="False"
            DataKeyNames="id"
            OnRowCommand="gvProdutos_RowCommand"
            OnRowDataBound="gvProdutos_RowDataBound"
            EmptyDataText="Nenhum produto encontrado. Use a pesquisa acima.">

            <Columns>
                <asp:BoundField DataField="id" HeaderText="ID" ItemStyle-Width="50px" />
                <asp:BoundField DataField="nome" HeaderText="Produto" />
                <asp:BoundField DataField="codigo" HeaderText="Código" ItemStyle-Width="100px" />
                <asp:BoundField DataField="quantidadeEstoque" HeaderText="Estoque Disponível" ItemStyle-Width="130px" />
                <asp:BoundField DataField="precoVenda" HeaderText="Preço" DataFormatString="{0:C2}" ItemStyle-Width="100px" />

                <asp:TemplateField HeaderText="Ação" ItemStyle-Width="150px">
                    <ItemTemplate>
                        <asp:Button ID="btnSelecionarSaida" runat="server"
                            Text="📤 Dar Saída"
                            CssClass="gerenciar-btn-editar"
                            CommandName="SelecionarSaida"
                            CommandArgument='<%# Eval("id") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <!-- Modal exibido ao registrar a saída de um produto -->
        <asp:Panel ID="pnlSaida" runat="server" Visible="false" CssClass="modal-overlay">
            <div class="modal-content">
                <h3 class="modal-titulo">📤 Registrar Saída</h3>

                <!-- Exibe informações do produto selecionado -->
                <div class="form-info-box" id="divInfoProduto" runat="server">
                    <strong>Produto:</strong> <asp:Label ID="lblProdutoNome" runat="server"></asp:Label><br />
                    <strong>Código:</strong> <asp:Label ID="lblProdutoCodigo" runat="server"></asp:Label><br />
                    <strong>Estoque Disponível:</strong> <asp:Label ID="lblEstoqueAtual" runat="server"></asp:Label> unidades
                </div>

                <!-- Campo de quantidade a ser retirada -->
                <div class="form-grupo">
                    <label>Quantidade <span class="form-campo-obrigatorio">*</span></label>
                    <asp:TextBox ID="txtQuantidade" runat="server" TextMode="Number" placeholder="Digite a quantidade"></asp:TextBox>
                    <div class="form-ajuda">Quantidade a retirar do estoque</div>
                </div>

                <!-- Campo para nome do cliente ou destino -->
                <div class="form-grupo">
                    <label>Cliente/Destino <span class="form-campo-obrigatorio">*</span></label>
                    <asp:TextBox ID="txtCliente" runat="server" placeholder="Nome do cliente ou destino"></asp:TextBox>
                </div>

                <!-- Campo opcional de observação -->
                <div class="form-grupo">
                    <label>Observação</label>
                    <asp:TextBox ID="txtObservacao" runat="server"
                        TextMode="MultiLine"
                        Rows="3"
                        placeholder="Observações sobre esta saída (opcional)..."
                        Style="width: 100%;">
                    </asp:TextBox>
                </div>

                <!-- Botões de ação -->
                <div class="form-botoes">
                    <!-- Fecha o modal sem salvar -->
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="form-btn-cancelar" OnClick="btnCancelar_Click" CausesValidation="false" />

                    <!-- Confirma a saída e salva no banco -->
                    <asp:Button ID="btnConfirmarSaida" runat="server" Text="✅ Confirmar Saída" CssClass="form-btn-salvar" OnClick="btnConfirmarSaida_Click" />
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
