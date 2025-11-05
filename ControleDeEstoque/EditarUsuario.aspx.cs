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
    public partial class EditarUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Verifica se o usuário logado é administrador
            if (Session["TipoUsuario"] == null || Session["TipoUsuario"].ToString() != "Admin")
            {
                Response.Redirect("Default.aspx");
                return;
            }

            // Obtém o ID do usuário via QueryString (?id=123)
            if (Request.QueryString["id"] != null)
            {
                int usuarioId = Convert.ToInt32(Request.QueryString["id"]);

                // Carrega dados apenas na primeira exibição da página
                if (!IsPostBack)
                {
                    CarregarDadosUsuario(usuarioId);
                }
            }
            else
            {
                // Se não houver ID, redireciona para a listagem de usuários
                Response.Redirect("GerenciarUsuarios.aspx");
                return;
            }
        }

        private void CarregarDadosUsuario(int usuarioId)
        {
            UsuarioDAL dal = new UsuarioDAL();
            DataTable dt = dal.BuscarUsuarioPorId(usuarioId);

            if (dt.Rows.Count > 0)
            {
                // Preenche os campos com os dados do usuário
                lblId.Text = dt.Rows[0]["id"].ToString();
                txtNome.Text = dt.Rows[0]["nomeFuncionario"].ToString();
                txtUsuario.Text = dt.Rows[0]["usuarioFuncionario"].ToString();
                ddlTipoUsuario.SelectedValue = dt.Rows[0]["tipoUsuario"].ToString();
                chkAtivo.Checked = Convert.ToBoolean(dt.Rows[0]["ativo"]);

                // A senha não é exibida por motivos de segurança
            }
            else
            {
                // Caso o ID não exista, retorna à tela de gerenciamento
                Response.Redirect("GerenciarUsuarios.aspx");
            }
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            // Garante que há um ID válido
            if (Request.QueryString["id"] == null)
            {
                Response.Redirect("GerenciarUsuarios.aspx");
                return;
            }

            int usuarioId = Convert.ToInt32(Request.QueryString["id"]);

            // Captura os valores dos campos
            string nome = txtNome.Text.Trim();
            string usuario = txtUsuario.Text.Trim();
            string senha = txtSenha.Text.Trim();
            string confirmarSenha = txtConfirmarSenha.Text.Trim();
            string tipo = ddlTipoUsuario.SelectedValue;
            bool ativo = chkAtivo.Checked;

            // Validação de campos obrigatórios
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

            // Se o campo de senha for preenchido, aplicar validações
            if (!string.IsNullOrEmpty(senha))
            {
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
            }

            // Verifica duplicidade de nome de usuário, exceto para o próprio registro
            UsuarioDAL dal = new UsuarioDAL();
            if (dal.UsuarioExiste(usuario, usuarioId))
            {
                MostrarMensagem("Este nome de usuário já está em uso por outro usuário!", false);
                return;
            }

            // Atualiza os dados do usuário no banco
            bool sucesso = dal.AtualizarUsuario(usuarioId, nome, usuario, senha, tipo, ativo);

            if (sucesso)
            {
                // Redireciona com mensagem de sucesso
                Response.Redirect("GerenciarUsuarios.aspx?msg=atualizado");
            }
            else
            {
                MostrarMensagem("Erro ao atualizar usuário. Tente novamente.", false);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            // Retorna à tela de gerenciamento sem salvar alterações
            Response.Redirect("GerenciarUsuarios.aspx");
        }

        private void MostrarMensagem(string texto, bool sucesso)
        {
            // Exibe mensagem de feedback com cor correspondente
            lblMensagem.Text = texto;
            lblMensagem.ForeColor = sucesso ? Color.Green : Color.Red;
            lblMensagem.Visible = true;
        }

        protected void btnExcluirLogin_Click(object sender, EventArgs e)
        {
            // Este botão (caso implementado futuramente) deve excluir o login do usuário
            if (Request.QueryString["id"] == null)
            {
                Response.Redirect("GerenciarUsuarios.aspx");
                return;
            }

            int usuarioId = Convert.ToInt32(Request.QueryString["id"]);
            // Implementação da exclusão ainda não definida
        }
    }
}
