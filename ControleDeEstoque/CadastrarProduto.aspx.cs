using System;
using System.Drawing;
using System.Web.UI;

namespace ControleDeEstoque
{
    public partial class CadastrarProduto : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Verifica se o usuário está logado e é do tipo "Usuário"
            if (Session["TipoUsuario"] == null || Session["TipoUsuario"].ToString() != "Usuario")
            {
                Response.Redirect("Default.aspx");
                return;
            }
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            // Captura dos valores dos campos
            string nome = txtNomeProduto.Text.Trim();
            string codigo = txtCodigoProduto.Text.Trim();
            string descricao = txtDescricao.Text.Trim();
            string fornecedor = txtFornecedor.Text.Trim();

            int quantidade;
            if (!int.TryParse(txtQntdEstoque.Text.Trim(), out quantidade))
            {
                MostrarMensagem("Quantidade inválida.", false);
                return;
            }

            // Conversão dos preços
            decimal precoCusto;
            if (!decimal.TryParse(txtPrecoCusto.Text.Trim().Replace(".", ","), out precoCusto))
            {
                MostrarMensagem("Preço de custo inválido.", false);
                return;
            }

            decimal precoVenda;
            if (!decimal.TryParse(txtPrecoVenda.Text.Trim().Replace(".", ","), out precoVenda))
            {
                MostrarMensagem("Preço de venda inválido.", false);
                return;
            }

            bool ativo = chkAtivo.Checked;

            // Validações gerais
            if (string.IsNullOrEmpty(nome) || nome.Length < 3)
            {
                MostrarMensagem("O nome do produto deve ter pelo menos 3 caracteres.", false);
                return;
            }

            if (string.IsNullOrEmpty(codigo) || codigo.Length < 2)
            {
                MostrarMensagem("O código do produto deve ter pelo menos 2 caracteres.", false);
                return;
            }

            if (string.IsNullOrEmpty(fornecedor) || fornecedor.Length < 3)
            {
                MostrarMensagem("O nome do fornecedor deve ter pelo menos 3 caracteres.", false);
                return;
            }

            if (quantidade < 0)
            {
                MostrarMensagem("A quantidade não pode ser negativa.", false);
                return;
            }

            if (precoCusto <= 0 || precoVenda <= 0)
            {
                MostrarMensagem("Os valores de custo e venda devem ser maiores que zero.", false);
                return;
            }

            if (precoVenda <= precoCusto)
            {
                MostrarMensagem("O preço de venda deve ser maior que o preço de custo.", false);
                return;
            }

            // Verifica margem mínima de 20%
            decimal margemMinima = precoCusto * 1.20m;
            if (precoVenda < margemMinima)
            {
                decimal margemAtual = ((precoVenda - precoCusto) / precoCusto) * 100;
                MostrarMensagem($"Margem muito baixa. O preço de venda deve ser no mínimo R$ {margemMinima:F2} (20% acima do custo). Margem atual: {margemAtual:F1}%.", false);
                return;
            }

            // Verifica duplicidade no banco
            ProdutoDAL dal = new ProdutoDAL();
            if (dal.ProdutoExiste(codigo))
            {
                MostrarMensagem("Este produto já foi cadastrado.", false);
                return;
            }

            // Inserção no banco
            bool sucesso = dal.InserirProduto(nome, codigo, descricao, fornecedor, quantidade, precoCusto, precoVenda, ativo);

            if (sucesso)
                Response.Redirect("GerenciadorDeEstoque.aspx?msg=cadastrado");
            else
                MostrarMensagem("Erro ao cadastrar o produto.", false);
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            // Retorna à tela de gerenciamento
            Response.Redirect("GerenciadorDeEstoque.aspx");
        }

        private void MostrarMensagem(string texto, bool sucesso)
        {
            lblMensagem.Text = texto;
            lblMensagem.ForeColor = sucesso ? Color.Green : Color.Red;
            lblMensagem.Visible = true;
        }
    }
}
