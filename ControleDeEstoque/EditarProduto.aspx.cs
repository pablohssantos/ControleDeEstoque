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
    public partial class EditarProduto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Verifica se o usuário está logado e possui permissão de "Usuário"
            if (Session["TipoUsuario"] == null || Session["TipoUsuario"].ToString() != "Usuario")
            {
                Response.Redirect("Default.aspx");
                return;
            }

            // Carrega dados do produto apenas se houver ID na querystring
            if (Request.QueryString["id"] != null)
            {
                int produtoId = Convert.ToInt32(Request.QueryString["id"]);

                // Carrega informações apenas na primeira carga da página
                if (!IsPostBack)
                {
                    CarregarDadosProduto(produtoId);
                }
            }
            else
            {
                // Redireciona caso o ID não seja informado
                Response.Redirect("GerenciadorDeEstoque.aspx");
                return;
            }
        }

        // Busca os dados do produto no banco e preenche os campos do formulário
        private void CarregarDadosProduto(int produtoId)
        {
            ProdutoDAL dal = new ProdutoDAL();
            DataTable dt = dal.BuscarProdutoPorId(produtoId);

            if (dt.Rows.Count > 0)
            {
                txtNomeProduto.Text = dt.Rows[0]["nome"].ToString();
                txtCodigoProduto.Text = dt.Rows[0]["codigo"].ToString();
                txtDescricao.Text = dt.Rows[0]["descricao"].ToString();
                txtQntdEstoque.Text = dt.Rows[0]["quantidadeEstoque"].ToString();
                txtPrecoCusto.Text = Convert.ToDecimal(dt.Rows[0]["precoCusto"]).ToString("F2");
                txtPrecoVenda.Text = Convert.ToDecimal(dt.Rows[0]["precoVenda"]).ToString("F2");
                chkAtivo.Checked = Convert.ToBoolean(dt.Rows[0]["ativo"]);
            }
            else
            {
                // Retorna à listagem caso o produto não seja encontrado
                Response.Redirect("GerenciadorDeEstoque.aspx");
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            // Volta para a tela de gerenciamento sem salvar alterações
            Response.Redirect("GerenciadorDeEstoque.aspx");
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            // Garante que o ID foi informado
            if (Request.QueryString["id"] == null)
            {
                Response.Redirect("GerenciadorDeEstoque.aspx");
                return;
            }

            int produtoId = Convert.ToInt32(Request.QueryString["id"]);

            // Coleta e valida os dados do formulário
            string nome = txtNomeProduto.Text.Trim();
            string codigo = txtCodigoProduto.Text.Trim();
            string descricao = txtDescricao.Text.Trim();

            // Conversão e validação de quantidade
            int quantidade;
            if (!int.TryParse(txtQntdEstoque.Text.Trim(), out quantidade))
            {
                MostrarMensagem("Quantidade inválida!", false);
                return;
            }

            // Conversão e validação de preço de custo
            decimal precoCusto;
            if (!decimal.TryParse(txtPrecoCusto.Text.Trim().Replace(".", ","), out precoCusto))
            {
                MostrarMensagem("Preço de custo inválido!", false);
                return;
            }

            // Conversão e validação de preço de venda
            decimal precoVenda;
            if (!decimal.TryParse(txtPrecoVenda.Text.Trim().Replace(".", ","), out precoVenda))
            {
                MostrarMensagem("Preço de venda inválido!", false);
                return;
            }

            bool ativo = chkAtivo.Checked;

            // Validação: preço de venda deve ser maior que o de custo
            if (precoVenda <= precoCusto)
            {
                MostrarMensagem("O preço de venda deve ser maior que o preço de custo!", false);
                return;
            }

            // Validação: margem mínima de 20%
            decimal margemMinima = precoCusto * 1.20m;
            if (precoVenda < margemMinima)
            {
                decimal margemAtual = ((precoVenda - precoCusto) / precoCusto) * 100;
                MostrarMensagem($"Margem muito baixa! Preço de venda deve ser no mínimo R$ {margemMinima:F2} (20% acima do custo). Margem atual: {margemAtual:F1}%", false);
                return;
            }

            // Validações de texto e regras de formato
            if (string.IsNullOrEmpty(nome))
            {
                MostrarMensagem("O nome é obrigatório!", false);
                return;
            }

            if (nome.Length < 3)
            {
                MostrarMensagem("O nome deve ter no mínimo 3 caracteres!", false);
                return;
            }

            if (string.IsNullOrEmpty(codigo))
            {
                MostrarMensagem("O código é obrigatório!", false);
                return;
            }

            if (codigo.Length < 2)
            {
                MostrarMensagem("O código deve ter no mínimo 2 caracteres!", false);
                return;
            }

            if (precoCusto <= 0)
            {
                MostrarMensagem("Preço de custo deve ser maior que zero!", false);
                return;
            }

            if (precoVenda <= 0)
            {
                MostrarMensagem("Preço de venda deve ser maior que zero!", false);
                return;
            }

            // Verifica se já existe outro produto com o mesmo código
            ProdutoDAL dal = new ProdutoDAL();
            if (dal.ProdutoExiste(codigo, produtoId))
            {
                MostrarMensagem("Este código já está em uso por outro produto!", false);
                return;
            }

            // Atualiza o produto no banco de dados
            bool sucesso = dal.AtualizarProduto(produtoId, nome, codigo, descricao, quantidade, precoCusto, precoVenda, ativo);

            if (sucesso)
            {
                Response.Redirect("GerenciadorDeEstoque.aspx?msg=atualizado");
            }
            else
            {
                MostrarMensagem("Erro ao atualizar produto. Tente novamente.", false);
            }
        }

        // Exibe mensagens de erro ou sucesso
        private void MostrarMensagem(string texto, bool sucesso)
        {
            lblMensagem.Text = texto;
            lblMensagem.ForeColor = sucesso ? Color.Green : Color.Red;
            lblMensagem.Visible = true;
        }
    }
}
