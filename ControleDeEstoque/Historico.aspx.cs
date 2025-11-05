using System;
using System.Data;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ControleDeEstoque
{
    public partial class Historico : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Impede acesso se o usuário não estiver logado ou não for do tipo "Usuario"
            if (Session["TipoUsuario"] == null || Session["TipoUsuario"].ToString() != "Usuario")
            {
                Response.Redirect("Default.aspx");
                return;
            }

            // Carrega o histórico apenas na primeira carga da página
            if (!IsPostBack)
            {
                CarregarHistorico();
            }
        }

        // Carrega o histórico de movimentações, com ou sem filtro
        private void CarregarHistorico(string filtro = "")
        {
            MovimentacaoDAL dal = new MovimentacaoDAL();
            DataTable dt = dal.ListarMovimentacoes(filtro);
            gvHistorico.DataSource = dt;
            gvHistorico.DataBind();
        }

        // Evento do botão de pesquisa
        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            string filtro = txtPesquisa.Text.Trim();
            CarregarHistorico(filtro);
        }

        // Personaliza visualmente as linhas do GridView conforme o tipo da movimentação
        protected void gvHistorico_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // Verifica se a linha é de dados (não cabeçalho ou rodapé)
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = (DataRowView)e.Row.DataItem;
                string tipo = drv["tipo"].ToString();

                // Define classe CSS diferente para "Entrada" e "Saída"
                if (tipo == "Entrada")
                {
                    e.Row.CssClass = "historico-entrada";
                }
                else if (tipo == "Saida")
                {
                    e.Row.CssClass = "historico-saida";
                }
            }
        }
    }
}
