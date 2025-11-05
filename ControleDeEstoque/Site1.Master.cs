using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ControleDeEstoque
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            // Limpar sessão
            Session.Clear();
            Session.Abandon();

            // Redirecionar para login
            Response.Redirect("Default.aspx");
        }
    }
}