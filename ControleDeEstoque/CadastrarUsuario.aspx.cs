using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ControleDeEstoque
{
    public partial class CadastrarUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Garante que apenas administradores podem acessar esta página
            if (Session["TipoUsuario"] == null || Session["TipoUsuario"].ToString() != "Admin")
            {
                Response.Redirect("Default.aspx");
                return;
            }
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            // Coleta os valores dos campos do formulário
            string nome = txtNome.Text.Trim();
            string usuario = txtUsuario.Text.Trim();
            string senha = txtSenha.Text.Trim();
            string confirmarSenha = txtConfirmarSenha.Text.Trim();
            string tipo = ddlTipoUsuario.SelectedValue;
            bool ativo = chkAtivo.Checked;

            // Validações básicas dos campos
            if (string.IsNullOrEmpty(nome))
            {
                MostrarMensagem("O nome é obrigatório!", false);
                return;
            }

            if (string.IsNullOrEmpty(usuario))
            {
                MostrarMensagem("O nome de usuário é obrigatório!", false);
                return;
            }

            if (string.IsNullOrEmpty(senha))
            {
                MostrarMensagem("A senha é obrigatória!", false);
                return;
            }

            if (senha.Length < 4)
            {
                MostrarMensagem("A senha deve ter no mínimo 4 caracteres!", false);
                return;
            }

            if (senha != confirmarSenha)
            {
                MostrarMensagem("As senhas não coincidem!", false);
                return;
            }

            // Verifica se o nome de usuário já existe no banco
            UsuarioDAL dal = new UsuarioDAL();
            if (dal.UsuarioExiste(usuario))
            {
                MostrarMensagem("Este nome de usuário já está em uso! Escolha outro.", false);
                return;
            }

            // Tenta inserir o novo usuário no banco de dados
            bool sucesso = dal.InserirUsuario(nome, usuario, senha, tipo, ativo);

            // Redireciona em caso de sucesso ou exibe erro
            if (sucesso)
            {
                Response.Redirect("GerenciarUsuarios.aspx?msg=cadastrado");
            }
            else
            {
                MostrarMensagem("Erro ao cadastrar usuário. Tente novamente.", false);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            // Retorna à tela de gerenciamento de usuários
            Response.Redirect("GerenciarUsuarios.aspx");
        }

        private void MostrarMensagem(string texto, bool sucesso)
        {
            // Exibe mensagem de feedback visual na tela
            lblMensagem.Text = texto;
            lblMensagem.ForeColor = sucesso ? Color.Green : Color.Red;
            lblMensagem.Visible = true;
        }
    }
}
