<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GerenciarUsuarios.aspx.cs" Inherits="ControleDeEstoque.GerenciarUsuarios" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="StyleSheetMaster.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="gerenciar-container">

            <!-- Botão de logout -->
            <div style="margin-bottom: 15px;">
                <asp:Button ID="btnVoltar" runat="server" Text="← Sair" CssClass="btn-voltar" OnClick="btnVoltar_Click"
                    OnClientClick="return confirm('Deseja realmente sair? Você será deslogado do sistema.');" />
            </div>

            <h2 class="titulo-gerenc-container">Gerenciar Usuários</h2>

            <!-- Mensagem de feedback para ações (erro/sucesso) -->
            <asp:Label ID="lblMensagem" runat="server" CssClass="gerenciar-mensagem" Visible="false"></asp:Label>

            <!-- Barra superior: pesquisa e criação de novo usuário -->
            <div class="gerenciar-barra-topo">
                <div class="gerenciar-pesquisa-box">
                    <asp:TextBox ID="txtPesquisa" runat="server" Placeholder="Pesquisar por nome, usuário ou id..."></asp:TextBox>
                    <asp:Button ID="btnPesquisar" runat="server" Text="🔍 Pesquisar" CssClass="gerenciar-btn-pesquisar" OnClick="btnPesquisar_Click" />
                </div>

                <!-- Botão para redirecionar ao cadastro de novo usuário -->
                <asp:Button ID="btnNovo" runat="server" Text="➕ Novo Usuário" CssClass="gerenciar-btn-novo" OnClick="btnNovo_Click" />
            </div>

            <!-- GridView responsável por listar e gerenciar os usuários -->
            <asp:GridView ID="gvUsuarios" runat="server" 
                CssClass="gerenciar-gridview" 
                AutoGenerateColumns="False"
                DataKeyNames="id"
                OnRowCommand="gvUsuarios_RowCommand"
                OnRowDataBound="gvUsuarios_RowDataBound"
                EmptyDataText="Nenhum usuário encontrado.">

                <Columns>
                    <asp:BoundField DataField="id" HeaderText="ID" ItemStyle-Width="50px" />
                    <asp:BoundField DataField="nomeFuncionario" HeaderText="Nome" />
                    <asp:BoundField DataField="usuarioFuncionario" HeaderText="Usuário" />

                    <asp:TemplateField HeaderText="Tipo">
                        <ItemTemplate>
                            <span class='badge <%# Eval("tipoUsuario").ToString() == "Admin" ? "badge-admin" : "badge-usuario" %>'>
                                <%# Eval("tipoUsuario") %>
                            </span>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <span class='badge <%# Convert.ToBoolean(Eval("ativo")) ? "badge-ativo" : "badge-inativo" %>'>
                                <%# Convert.ToBoolean(Eval("ativo")) ? "Ativo" : "Inativo" %>
                            </span>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="dataCriacao" HeaderText="Data Criação" DataFormatString="{0:dd/MM/yyyy HH:mm}" />

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
                                    OnClientClick="return confirm('Tem certeza que deseja alterar o status deste usuário?');" />
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
