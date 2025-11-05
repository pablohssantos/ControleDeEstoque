using ControleDeEstoque;
using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Data;

public class ProdutoDAL
{
    private string conexaoBD = "Server=localhost;Database=controledeestoque;Uid=root;Pwd=ZnTCruZ1@;";

    // ========== LISTAR PRODUTOS (com filtro opcional) ==========
    public DataTable ListarProdutos(string filtro = "")
    {
        DataTable dt = new DataTable();
        using (MySqlConnection conn = new MySqlConnection(conexaoBD))
        {
            string query = @"SELECT id, nome, codigo, fornecedor, quantidadeEstoque, precoCusto, precoVenda, ativo, dataCadastro 
                             FROM produtos";

            if (!string.IsNullOrEmpty(filtro))
            {
                query += " WHERE nome LIKE @filtro " +
                         "OR codigo LIKE @filtro " +
                         "OR CAST(id AS CHAR) LIKE @filtro";
            }

            query += " ORDER BY dataCadastro DESC";

            MySqlCommand cmd = new MySqlCommand(query, conn);

            if (!string.IsNullOrEmpty(filtro))
            {
                cmd.Parameters.AddWithValue("@filtro", "%" + filtro + "%");
            }

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(dt);
        }
        return dt;
    }

    // ========== ALTERNAR STATUS ATIVO/INATIVO (Produto) ==========
    public bool ToggleAtivoProduto(int id)
    {
        try
        {
            using (MySqlConnection conn = new MySqlConnection(conexaoBD))
            {
                conn.Open();
                string query = "UPDATE produtos SET ativo = NOT ativo WHERE id = @id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);

                int linhasAfetadas = cmd.ExecuteNonQuery();
                return linhasAfetadas > 0;
            }
        }
        catch
        {
            return false;
        }
    }

    // ========== VERIFICAR SE PRODUTO JÁ EXISTE (evitar duplicatas) ==========
    public bool ProdutoExiste(string codigo, int? idIgnorar = null)
    {
        using (MySqlConnection conn = new MySqlConnection(conexaoBD))
        {
            conn.Open();
            string query = "SELECT COUNT(*) FROM produtos WHERE codigo = @codigo";

            if (idIgnorar.HasValue)
            {
                query += " AND id != @id";
            }

            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@codigo", codigo);

            if (idIgnorar.HasValue)
            {
                cmd.Parameters.AddWithValue("@id", idIgnorar.Value);
            }

            int count = Convert.ToInt32(cmd.ExecuteScalar());
            return count > 0;
        }
    }

    // ========== INSERIR NOVO PRODUTO ==========
    public bool InserirProduto(string nome, string codigo, string descricao, string fornecedor, int quantidade, decimal precoCusto, decimal precoVenda, bool ativo)
    {
        try
        {
            using (MySqlConnection conn = new MySqlConnection(conexaoBD))
            {
                conn.Open();
                string query = @"INSERT INTO produtos 
                                (nome, codigo, descricao, fornecedor, quantidadeEstoque, precoCusto, precoVenda, ativo, dataCadastro) 
                                VALUES 
                                (@nome, @codigo, @descricao, @fornecedor, @quantidade, @precoCusto, @precoVenda, @ativo, NOW())";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@nome", nome);
                cmd.Parameters.AddWithValue("@codigo", codigo);
                cmd.Parameters.AddWithValue("@descricao", descricao);
                cmd.Parameters.AddWithValue("@fornecedor", fornecedor);
                cmd.Parameters.AddWithValue("@quantidade", quantidade);
                cmd.Parameters.AddWithValue("@precoCusto", precoCusto);
                cmd.Parameters.AddWithValue("@precoVenda", precoVenda);
                cmd.Parameters.AddWithValue("@ativo", ativo);

                int linhasAfetadas = cmd.ExecuteNonQuery();
                return linhasAfetadas > 0;
            }
        }
        catch
        {
            return false;
        }
    }

    // ========== BUSCAR PRODUTO POR ID (para edição) ==========
    public DataTable BuscarProdutoPorId(int id)
    {
        DataTable dt = new DataTable();
        using (MySqlConnection conn = new MySqlConnection(conexaoBD))
        {
            string query = "SELECT * FROM produtos WHERE id = @id";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", id);

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(dt);
        }
        return dt;
    }

    // ========== ATUALIZAR PRODUTO EXISTENTE ==========
    public bool AtualizarProduto(int id, string nome, string codigo, string descricao, int quantidade, decimal precoCusto, decimal precoVenda, bool ativo)
    {
        try
        {
            using (MySqlConnection conn = new MySqlConnection(conexaoBD))
            {
                conn.Open();
                string query = @"UPDATE produtos SET 
                                nome = @nome, 
                                codigo = @codigo, 
                                descricao = @descricao,
                                quantidadeEstoque = @quantidade,
                                precoCusto = @precoCusto,
                                precoVenda = @precoVenda,
                                ativo = @ativo
                                WHERE id = @id";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@nome", nome);
                cmd.Parameters.AddWithValue("@codigo", codigo);
                cmd.Parameters.AddWithValue("@descricao", descricao);
                cmd.Parameters.AddWithValue("@quantidade", quantidade);
                cmd.Parameters.AddWithValue("@precoCusto", precoCusto);
                cmd.Parameters.AddWithValue("@precoVenda", precoVenda);
                cmd.Parameters.AddWithValue("@ativo", ativo);

                int linhasAfetadas = cmd.ExecuteNonQuery();
                return linhasAfetadas > 0;
            }
        }
        catch
        {
            return false;
        }
    }
}


