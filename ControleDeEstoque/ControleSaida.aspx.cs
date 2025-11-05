using System;
using System.Data;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ControleDeEstoque
{
    public partial class ControleSaida : System.Web.UI.Page
    {
        // Armazena o ID do produto selecionado na ViewState (mantém o valor entre postbacks)
        private int ProdutoSelecionadoId
        {
            get { return ViewState["ProdutoSelecionadoId"] != null ? (int)ViewState["ProdutoSelecionadoId"] : 0; }
            set { ViewState["ProdutoSelecionadoId"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Verifica se o usuário está logado e tem permissão de acesso
            if (Session["TipoUsuario"] == null || Session["TipoUsuario"].ToString() != "Usuario")
            {
                Response.Redirect("Default.aspx");
                return;
            }

            // Carrega produtos apenas no primeiro carregamento da página
            if (!IsPostBack)
            {
                CarregarProdutos();

                // Exibe mensagem de sucesso após registrar uma saída
                if (Request.QueryString["msg"] == "saida")
                {
                    MostrarMensagem("✅ Saída registrada com sucesso!", true);
                }
            }
        }

        // Carrega os produtos no GridView (com ou sem filtro)
        private void CarregarProdutos(string filtro = "")
        {
            ProdutoDAL dal = new ProdutoDAL();
            DataTable dt = dal.ListarProdutos(filtro);
            gvProdutos.DataSource = dt;
            gvProdutos.DataBind();
        }

        // Pesquisa produtos pelo texto informado
        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            string filtro = txtPesquisa.Text.Trim();
            CarregarProdutos(filtro);
        }

        // Captura comandos do GridView (selecionar produto para saída)
        protected void gvProdutos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "SelecionarSaida")
            {
                int produtoId = Convert.ToInt32(e.CommandArgument);
                AbrirModalSaida(produtoId);
            }
        }

        // Aplica cores às linhas do GridView conforme o nível de estoque
        protected void gvProdutos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = (DataRowView)e.Row.DataItem;
                int estoque = Convert.ToInt32(drv["quantidadeEstoque"]);

                if (estoque == 0)
                {
                    e.Row.BackColor = ColorTranslator.FromHtml("#ffcccc"); // Vermelho claro: sem estoque
                }
                else if (estoque < 10)
                {
                    e.Row.BackColor = ColorTranslator.FromHtml("#fff3cd"); // Amarelo claro: estoque baixo
                }
            }
        }

        // Exibe o modal para registrar saída de produto
        private void AbrirModalSaida(int produtoId)
        {
            ProdutoDAL dal = new ProdutoDAL();
            DataTable dt = dal.BuscarProdutoPorId(produtoId);

            if (dt.Rows.Count > 0)
            {
                ProdutoSelecionadoId = produtoId;
                int estoqueAtual = Convert.ToInt32(dt.Rows[0]["quantidadeEstoque"]);

                // Preenche informações do produto
                lblProdutoNome.Text = dt.Rows[0]["nome"].ToString();
                lblProdutoCodigo.Text = dt.Rows[0]["codigo"].ToString();
                lblEstoqueAtual.Text = estoqueAtual.ToString();

                // Destaque visual conforme estoque
                if (estoqueAtual == 0)
                {
                    lblEstoqueAtual.ForeColor = Color.Red;
                    lblEstoqueAtual.Text += " ⚠️ SEM ESTOQUE";
                    divInfoProduto.Style["background"] = "#ffcccc";
                }
                else if (estoqueAtual < 10)
                {
                    lblEstoqueAtual.ForeColor = Color.Orange;
                    lblEstoqueAtual.Text += " ⚠️ ESTOQUE BAIXO";
                    divInfoProduto.Style["background"] = "#fff3cd";
                }
                else
                {
                    lblEstoqueAtual.ForeColor = Color.Green;
                    divInfoProduto.Style["background"] = "#e7f3ff";
                }

                // Limpa campos do formulário antes de abrir o modal
                txtQuantidade.Text = "";
                txtCliente.Text = "";
                txtObservacao.Text = "";

                pnlSaida.Visible = true; // Exibe modal
            }
        }

        // Fecha o modal sem registrar saída
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            pnlSaida.Visible = false;
        }

        // Confirma e registra a saída de produto
        protected void btnConfirmarSaida_Click(object sender, EventArgs e)
        {
            // Verificação adicional de sessão (prevenção contra requisições diretas)
            if (Session["TipoUsuario"] == null || Session["TipoUsuario"].ToString() != "Usuario")
            {
                Response.Redirect("Default.aspx");
                return;
            }

            // Validação da quantidade
            int quantidade;
            if (!int.TryParse(txtQuantidade.Text.Trim(), out quantidade) || quantidade <= 0)
            {
                MostrarMensagem("Digite uma quantidade válida!", false);
                return;
            }

            // Validação do cliente/destino
            string cliente = txtCliente.Text.Trim();
            if (string.IsNullOrEmpty(cliente))
            {
                MostrarMensagem("O cliente/destino é obrigatório!", false);
                return;
            }

            // Verifica estoque disponível no banco
            int produtoId = ProdutoSelecionadoId;
            ProdutoDAL prodDAL = new ProdutoDAL();
            DataTable dt = prodDAL.BuscarProdutoPorId(produtoId);
            int estoqueAtual = Convert.ToInt32(dt.Rows[0]["quantidadeEstoque"]);

            if (quantidade > estoqueAtual)
            {
                MostrarMensagem($"Estoque insuficiente! Disponível: {estoqueAtual} unidades", false);
                return;
            }

            // Dados do produto e usuário logado
            string produtoNome = lblProdutoNome.Text;
            int usuarioId = Convert.ToInt32(Session["UsuarioId"]);
            string usuarioNome = Session["UsuarioNome"].ToString();

            // Registra a movimentação de saída
            MovimentacaoDAL movDAL = new MovimentacaoDAL();
            bool sucesso = movDAL.RegistrarSaida(
                produtoId,
                produtoNome,
                quantidade,
                cliente,
                txtObservacao.Text.Trim(),
                usuarioId,
                usuarioNome
            );

            // Redireciona com mensagem de sucesso ou exibe erro
            if (sucesso)
            {
                Response.Redirect("ControleSaida.aspx?msg=saida");
            }
            else
            {
                MostrarMensagem("Erro ao registrar saída. Tente novamente.", false);
            }
        }

        // Exibe mensagem de feedback visual
        private void MostrarMensagem(string texto, bool sucesso)
        {
            lblMensagem.Text = texto;
            lblMensagem.ForeColor = sucesso ? Color.Green : Color.Red;
            lblMensagem.Visible = true;
        }
    }
}
