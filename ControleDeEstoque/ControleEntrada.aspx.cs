using System;
using System.Data;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ControleDeEstoque
{
    public partial class ControleEntrada : System.Web.UI.Page
    {
        // Armazena temporariamente o ID do produto selecionado
        private int ProdutoSelecionadoId
        {
            get { return ViewState["ProdutoSelecionadoId"] != null ? (int)ViewState["ProdutoSelecionadoId"] : 0; }
            set { ViewState["ProdutoSelecionadoId"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Verifica se o usuário está logado e se é do tipo "Usuário"
            if (Session["TipoUsuario"] == null || Session["TipoUsuario"].ToString() != "Usuario")
            {
                Response.Redirect("Default.aspx");
                return;
            }

            // Executa apenas na primeira carga da página (não em postbacks)
            if (!IsPostBack)
            {
                CarregarProdutos();

                // Exibe mensagem de sucesso caso venha da URL
                if (Request.QueryString["msg"] == "entrada")
                {
                    MostrarMensagem("Entrada registrada com sucesso!", true);
                }
            }
        }

        // Carrega lista de produtos do banco de dados (com filtro opcional)
        private void CarregarProdutos(string filtro = "")
        {
            ProdutoDAL dal = new ProdutoDAL();
            DataTable dt = dal.ListarProdutos(filtro);
            gvProdutos.DataSource = dt;
            gvProdutos.DataBind();
        }

        // Evento do botão de pesquisa
        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            string filtro = txtPesquisa.Text.Trim();
            CarregarProdutos(filtro);
        }

        // Captura o comando de linha do GridView (botão "Dar Entrada")
        protected void gvProdutos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "SelecionarEntrada")
            {
                int produtoId = Convert.ToInt32(e.CommandArgument);
                AbrirModalEntrada(produtoId);
            }
        }

        // Abre o modal e exibe as informações do produto selecionado
        private void AbrirModalEntrada(int produtoId)
        {
            ProdutoDAL dal = new ProdutoDAL();
            DataTable dt = dal.BuscarProdutoPorId(produtoId);

            if (dt.Rows.Count > 0)
            {
                ProdutoSelecionadoId = produtoId;
                lblProdutoNome.Text = dt.Rows[0]["nome"].ToString();
                lblProdutoCodigo.Text = dt.Rows[0]["codigo"].ToString();
                lblEstoqueAtual.Text = dt.Rows[0]["quantidadeEstoque"].ToString();

                // Limpa os campos do formulário
                txtQuantidade.Text = "";
                txtFornecedor.Text = "";
                txtNotaFiscal.Text = "";
                txtObservacao.Text = "";

                pnlEntrada.Visible = true;
            }
        }

        // Fecha o modal sem alterar dados
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            pnlEntrada.Visible = false;
        }

        // Confirma a entrada do produto e grava a movimentação no banco
        protected void btnConfirmarEntrada_Click(object sender, EventArgs e)
        {
            // Impede que a página tente registrar entrada se o usuário estiver sem sessão ativa
            if (Session["UsuarioId"] == null || Session["UsuarioNome"] == null)
            {
                Response.Redirect("Default.aspx");
                return;
            }
            if (ProdutoSelecionadoId == 0)
            {
                MostrarMensagem("Nenhum produto selecionado.", false);
                return;
            }
            // Validação da quantidade
            int quantidade;
            if (!int.TryParse(txtQuantidade.Text.Trim(), out quantidade) || quantidade <= 0)
            {
                MostrarMensagem("Digite uma quantidade válida.", false);
                return;
            }

            // Validação do fornecedor
            string fornecedor = txtFornecedor.Text.Trim();
            if (string.IsNullOrEmpty(fornecedor))
            {
                MostrarMensagem("O fornecedor é obrigatório.", false);
                return;
            }

            // Pega dados do produto e do usuário logado
            int produtoId = ProdutoSelecionadoId;
            string produtoNome = lblProdutoNome.Text;
            int usuarioId = Convert.ToInt32(Session["UsuarioId"]);
            string usuarioNome = Session["UsuarioNome"].ToString();

            // Registra a movimentação no banco
            MovimentacaoDAL movDAL = new MovimentacaoDAL();
            bool sucesso = movDAL.RegistrarEntrada(
                produtoId,
                produtoNome,
                quantidade,
                fornecedor,
                txtNotaFiscal.Text.Trim(),
                txtObservacao.Text.Trim(),
                usuarioId,
                usuarioNome
            );

            if (sucesso)
            {
                Response.Redirect("ControleEntrada.aspx?msg=entrada");
            }
            else
            {
                MostrarMensagem("Erro ao registrar entrada. Tente novamente.", false);
            }
        }

        // Exibe mensagens na interface
        private void MostrarMensagem(string texto, bool sucesso)
        {
            lblMensagem.Text = texto;
            lblMensagem.ForeColor = sucesso ? Color.Green : Color.Red;
            lblMensagem.Visible = true;
        }
    }
}
