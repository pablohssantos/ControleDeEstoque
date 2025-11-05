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
    public partial class GerenciadorDeEstoque : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Bloqueia acesso caso o usuário não esteja logado ou não seja do tipo "Usuario"
            if (Session["TipoUsuario"] == null || Session["TipoUsuario"].ToString() != "Usuario")
            {
                Response.Redirect("Default.aspx");
                return;
            }

            if (!IsPostBack)
            {
                // Carrega os produtos ao abrir a página pela primeira vez
                CarregarProdutos();

                // Exibe mensagens de sucesso vindas por querystring
                if (Request.QueryString["msg"] == "cadastrado")
                {
                    MostrarMensagem("✅ Produto cadastrado com sucesso!", true);
                }
                else if (Request.QueryString["msg"] == "atualizado")
                {
                    MostrarMensagem("✅ Produto atualizado com sucesso!", true);
                }
            }
        }

        // Consulta e exibe os produtos cadastrados (com filtro opcional)
        private void CarregarProdutos(string filtro = "")
        {
            ProdutoDAL dal = new ProdutoDAL();
            DataTable dt = dal.ListarProdutos(filtro);
            gvProdutos.DataSource = dt;
            gvProdutos.DataBind();
        }

        // Executa pesquisa conforme o texto informado pelo usuário
        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            string filtro = txtPesquisa.Text.Trim();
            CarregarProdutos(filtro);
        }

        // Redireciona para a página de cadastro de novo produto
        protected void btnNovo_Click(object sender, EventArgs e)
        {
            Response.Redirect("CadastrarProduto.aspx");
        }

        // Trata os comandos disparados pelos botões dentro da GridView
        protected void gvProdutos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int produtoId = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Editar")
            {
                // Redireciona para a tela de edição do produto selecionado
                Response.Redirect($"EditarProduto.aspx?id={produtoId}");
            }
            else if (e.CommandName == "ToggleAtivo")
            {
                // Alterna o status ativo/inativo do produto
                ProdutoDAL dal = new ProdutoDAL();
                bool sucesso = dal.ToggleAtivoProduto(produtoId);

                if (sucesso)
                {
                    MostrarMensagem("Status do produto alterado com sucesso!", true);
                    CarregarProdutos(txtPesquisa.Text.Trim());
                }
                else
                {
                    MostrarMensagem("Erro ao alterar status do produto.", false);
                }
            }
        }

        // Destaca visualmente as linhas de produtos inativos
        protected void gvProdutos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = (DataRowView)e.Row.DataItem;
                bool ativo = Convert.ToBoolean(drv["ativo"]);

                if (!ativo)
                {
                    e.Row.CssClass = "gerenciar-estoque-inativo";
                }
            }
        }

        // Exibe mensagens de feedback na tela
        private void MostrarMensagem(string texto, bool sucesso)
        {
            lblMensagem.Text = texto;
            lblMensagem.ForeColor = sucesso ? Color.Green : Color.Red;
            lblMensagem.Visible = true;
        }
    }
}
