<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Historico.aspx.cs" Inherits="ControleDeEstoque.Historico" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Espaço reservado para conteúdo adicional no cabeçalho, caso necessário -->
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="conteudo" runat="server">
    <div class="gerenciar-container">
        <!-- Título principal da página -->
        <h2 class="titulo-gerenc-container">📋 Histórico de Movimentações</h2>

        <!-- Barra de pesquisa para filtrar movimentações -->
        <div class="gerenciar-barra-topo">
            <div class="gerenciar-pesquisa-box">
                <asp:TextBox ID="txtPesquisa" runat="server" Placeholder="Pesquisar por produto, fornecedor ou cliente..."></asp:TextBox>
                <asp:Button ID="btnPesquisar" runat="server" Text="🔍 Pesquisar" CssClass="gerenciar-btn-pesquisar" OnClick="btnPesquisar_Click" />
            </div>
        </div>

        <!-- Tabela com o histórico de movimentações -->
        <asp:GridView ID="gvHistorico" runat="server"
            CssClass="gerenciar-gridview"
            AutoGenerateColumns="False"
            OnRowDataBound="gvHistorico_RowDataBound"
            EmptyDataText="Nenhuma movimentação encontrada.">

            <Columns>
                <asp:BoundField DataField="id" HeaderText="ID" ItemStyle-Width="50px" />

                <asp:BoundField DataField="dataMovimentacao" HeaderText="Data/Hora"
                    DataFormatString="{0:dd/MM/yyyy HH:mm}" ItemStyle-Width="140px" />

                <asp:TemplateField HeaderText="Tipo" ItemStyle-Width="100px">
                    <ItemTemplate>
                        <span class='badge <%# Eval("tipo").ToString() == "Entrada" ? "badge-entrada" : "badge-saida" %>'>
                            <%# Eval("tipo").ToString() == "Entrada" ? "📦 Entrada" : "📤 Saída" %>
                        </span>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField DataField="produtoNome" HeaderText="Produto" />

                <asp:BoundField DataField="quantidade" HeaderText="Qtd" ItemStyle-Width="60px" />

                <asp:BoundField DataField="fornecedor" HeaderText="Fornecedor" />

                <asp:BoundField DataField="cliente" HeaderText="Cliente" />

                <asp:BoundField DataField="notaFiscal" HeaderText="NF" ItemStyle-Width="80px" />

                <asp:BoundField DataField="observacao" HeaderText="Observação" />

                <asp:BoundField DataField="usuarioNome" HeaderText="Responsável" ItemStyle-Width="120px" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
