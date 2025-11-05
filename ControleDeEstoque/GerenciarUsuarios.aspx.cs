using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ControleDeEstoque
{
    public partial class GerenciarUsuarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Verificar se usuário está logado e é Admin
            if (Session["TipoUsuario"] == null || Session["TipoUsuario"].ToString() != "Admin")
            {
                Response.Redirect("Default.aspx");
                return;
            }

            if (!IsPostBack)
            {
                CarregarUsuarios();

                // Verificar se veio mensagem de sucesso na URL
                if (Request.QueryString["msg"] == "cadastrado")
                {
                    MostrarMensagem("✅ Usuário cadastrado com sucesso!", true);
                }
                else if (Request.QueryString["msg"] == "atualizado")
                {
                    MostrarMensagem("✅ Usuário atualizado com sucesso!", true);
                }
            }
        }

        // Carregar todos os usuários na tabela
        private void CarregarUsuarios(string filtro = "")
        {
            UsuarioDAL dal = new UsuarioDAL();
            DataTable dt = dal.ListarUsuarios(filtro);
            gvUsuarios.DataSource = dt;
            gvUsuarios.DataBind();
        }

        // Botão Pesquisar
        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            string filtro = txtPesquisa.Text.Trim();
            CarregarUsuarios(filtro);
        }

        // Botão Novo Usuário - Redireciona para página de cadastro
        protected void btnNovo_Click(object sender, EventArgs e)
        {
            Response.Redirect("CadastrarUsuario.aspx");
        }

        // Botão Voltar - Fazer logout e limpar sessão
        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            FazerLogout();
        }

        // Método para fazer logout (limpar sessão)
        private void FazerLogout()
        {
            // Limpar todas as variáveis da sessão
            Session.Clear();
            Session.Abandon();

            // Redirecionar para a tela de login
            Response.Redirect("Default.aspx");
        }

        // Ações dos botões na GridView
        protected void gvUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int usuarioId = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Editar")
            {
                // Redirecionar para página de edição passando o ID
                Response.Redirect($"EditarUsuario.aspx?id={usuarioId}");
            }
            else if (e.CommandName == "ToggleAtivo")
            {
                // Alternar status ativo/inativo
                UsuarioDAL dal = new UsuarioDAL();
                bool sucesso = dal.ToggleAtivoUsuario(usuarioId);

                if (sucesso)
                {
                    MostrarMensagem("Status do usuário alterado com sucesso!", true);
                    CarregarUsuarios(txtPesquisa.Text.Trim());
                }
                else
                {
                    MostrarMensagem("Erro ao alterar status do usuário.", false);
                }
            }
        }

        // Colorir linha se usuário estiver inativo
        protected void gvUsuarios_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = (DataRowView)e.Row.DataItem;
                bool ativo = Convert.ToBoolean(drv["ativo"]);

                if (!ativo)
                {
                    e.Row.CssClass = "gerenciar-usuario-inativo";
                }
            }
        }

        // Exibir mensagens de feedback
        private void MostrarMensagem(string texto, bool sucesso)
        {
            lblMensagem.Text = texto;
            lblMensagem.ForeColor = sucesso ? Color.Green : Color.Red;
            lblMensagem.Visible = true;
        }
    }
}
    
