using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using System.Text;

namespace ControleDeEstoque
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Evento disparado ao carregar a página
            // (Pode ser utilizado para verificações de sessão ou inicializações futuras)
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string senha = txtSenha.Text.Trim();

            // Verifica se os campos obrigatórios foram preenchidos
            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(senha))
            {
                lblMensagem.Text = "Preencha usuário e senha!";
                lblMensagem.ForeColor = System.Drawing.Color.Red;
                return;
            }

            // Chama o método de validação no banco de dados
            UsuarioDAL dal = new UsuarioDAL();
            DataTable dt = dal.ValidarLogin(usuario, senha);

            // Se encontrou o usuário no banco
            if (dt.Rows.Count > 0)
            {
                // Verifica se o usuário é do tipo comum
                if (dt.Rows[0]["tipoUsuario"].ToString() == "Usuario")
                {
                    // Armazena os dados do usuário na sessão
                    Session["UsuarioId"] = dt.Rows[0]["id"];
                    Session["UsuarioNome"] = dt.Rows[0]["nomeFuncionario"];
                    Session["UsuarioLogin"] = dt.Rows[0]["usuarioFuncionario"];
                    Session["TipoUsuario"] = dt.Rows[0]["tipoUsuario"];

                    // Redireciona para a página principal do sistema
                    Response.Redirect("GerenciadorDeEstoque.aspx");
                }
                else
                {
                    // Caso o admin tente entrar pelo login comum
                    lblMensagem.Text = "Acesso ao sistema requer um usuário comum! Use o botão 'Admin' para gerenciar.";
                    lblMensagem.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                // Caso as credenciais estejam incorretas
                lblMensagem.Text = "Usuário ou senha incorretos!";
                lblMensagem.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnLoginAdmin_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string senha = txtSenha.Text.Trim();

            // Realiza a validação do login no banco de dados
            UsuarioDAL dal = new UsuarioDAL();
            DataTable dt = dal.ValidarLogin(usuario, senha);

            // Verifica se o login existe
            if (dt.Rows.Count > 0)
            {
                // Confirma se o usuário é administrador
                if (dt.Rows[0]["tipoUsuario"].ToString() == "Admin")
                {
                    // Armazena dados do admin na sessão
                    Session["UsuarioId"] = dt.Rows[0]["id"];
                    Session["UsuarioNome"] = dt.Rows[0]["nomeFuncionario"];
                    Session["TipoUsuario"] = "Admin";

                    // Redireciona para a página de administração
                    Response.Redirect("GerenciarUsuarios.aspx");
                }
                else
                {
                    // Impede o acesso de usuários comuns à área administrativa
                    lblMensagem.Text = "Acesso negado! Apenas administradores.";
                    lblMensagem.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                // Credenciais inválidas
                lblMensagem.Text = "Usuário ou senha incorretos!";
                lblMensagem.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}
