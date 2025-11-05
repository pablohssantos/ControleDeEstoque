using MySql.Data.MySqlClient;
using System;
using System.Data;

public class MovimentacaoDAL
{
    private string conexaoBD = "Server=localhost;Database=controledeestoque;Uid=root;Pwd=ZnTCruZ1@;";

    // ========== REGISTRAR ENTRADA ==========
    // Registra uma movimentação de entrada e atualiza o estoque do produto.
    // Usa transação para garantir integridade entre a inserção da movimentação e a atualização do estoque.
    public bool RegistrarEntrada(int produtoId, string produtoNome, int quantidade,
                                 string fornecedor, string notaFiscal, string observacao,
                                 int usuarioId, string usuarioNome)
    {
        using (MySqlConnection conn = new MySqlConnection(conexaoBD))
        {
            conn.Open();
            MySqlTransaction transaction = conn.BeginTransaction();

            try
            {
                // Inserção na tabela de movimentações
                string queryMov = @"INSERT INTO movimentacoes 
                                   (produtoId, produtoNome, tipo, quantidade, fornecedor, 
                                    notaFiscal, observacao, usuarioId, usuarioNome) 
                                   VALUES 
                                   (@produtoId, @produtoNome, 'Entrada', @quantidade, @fornecedor, 
                                    @notaFiscal, @observacao, @usuarioId, @usuarioNome)";

                MySqlCommand cmdMov = new MySqlCommand(queryMov, conn, transaction);
                cmdMov.Parameters.AddWithValue("@produtoId", produtoId);
                cmdMov.Parameters.AddWithValue("@produtoNome", produtoNome);
                cmdMov.Parameters.AddWithValue("@quantidade", quantidade);
                cmdMov.Parameters.AddWithValue("@fornecedor", fornecedor);
                cmdMov.Parameters.AddWithValue("@notaFiscal", notaFiscal ?? "");
                cmdMov.Parameters.AddWithValue("@observacao", observacao ?? "");
                cmdMov.Parameters.AddWithValue("@usuarioId", usuarioId);
                cmdMov.Parameters.AddWithValue("@usuarioNome", usuarioNome);
                cmdMov.ExecuteNonQuery();

                // Atualiza o estoque do produto somando a quantidade
                string queryProd = "UPDATE produtos SET quantidadeEstoque = quantidadeEstoque + @quantidade WHERE id = @produtoId";
                MySqlCommand cmdProd = new MySqlCommand(queryProd, conn, transaction);
                cmdProd.Parameters.AddWithValue("@quantidade", quantidade);
                cmdProd.Parameters.AddWithValue("@produtoId", produtoId);
                cmdProd.ExecuteNonQuery();

                transaction.Commit();
                return true;
            }
            catch
            {
                // Em caso de erro, desfaz todas as alterações
                transaction.Rollback();
                return false;
            }
        }
    }

    // ========== REGISTRAR SAÍDA ==========
    // Registra uma movimentação de saída e reduz o estoque do produto.
    // Verifica antes se há quantidade suficiente disponível.
    public bool RegistrarSaida(int produtoId, string produtoNome, int quantidade,
                               string cliente, string observacao,
                               int usuarioId, string usuarioNome)
    {
        using (MySqlConnection conn = new MySqlConnection(conexaoBD))
        {
            conn.Open();
            MySqlTransaction transaction = conn.BeginTransaction();

            try
            {
                // Verifica se o estoque é suficiente para a saída
                string queryVerifica = "SELECT quantidadeEstoque FROM produtos WHERE id = @produtoId";
                MySqlCommand cmdVerifica = new MySqlCommand(queryVerifica, conn, transaction);
                cmdVerifica.Parameters.AddWithValue("@produtoId", produtoId);
                int estoqueAtual = Convert.ToInt32(cmdVerifica.ExecuteScalar());

                if (estoqueAtual < quantidade)
                {
                    transaction.Rollback();
                    return false; // Bloqueia operação se não houver estoque
                }

                // Insere o registro de movimentação de saída
                string queryMov = @"INSERT INTO movimentacoes 
                                   (produtoId, produtoNome, tipo, quantidade, cliente, 
                                    observacao, usuarioId, usuarioNome) 
                                   VALUES 
                                   (@produtoId, @produtoNome, 'Saida', @quantidade, @cliente, 
                                    @observacao, @usuarioId, @usuarioNome)";

                MySqlCommand cmdMov = new MySqlCommand(queryMov, conn, transaction);
                cmdMov.Parameters.AddWithValue("@produtoId", produtoId);
                cmdMov.Parameters.AddWithValue("@produtoNome", produtoNome);
                cmdMov.Parameters.AddWithValue("@quantidade", quantidade);
                cmdMov.Parameters.AddWithValue("@cliente", cliente ?? "");
                cmdMov.Parameters.AddWithValue("@observacao", observacao ?? "");
                cmdMov.Parameters.AddWithValue("@usuarioId", usuarioId);
                cmdMov.Parameters.AddWithValue("@usuarioNome", usuarioNome);
                cmdMov.ExecuteNonQuery();

                // Atualiza o estoque subtraindo a quantidade
                string queryProd = "UPDATE produtos SET quantidadeEstoque = quantidadeEstoque - @quantidade WHERE id = @produtoId";
                MySqlCommand cmdProd = new MySqlCommand(queryProd, conn, transaction);
                cmdProd.Parameters.AddWithValue("@quantidade", quantidade);
                cmdProd.Parameters.AddWithValue("@produtoId", produtoId);
                cmdProd.ExecuteNonQuery();

                transaction.Commit();
                return true;
            }
            catch
            {
                transaction.Rollback();
                return false;
            }
        }
    }

    // ========== LISTAR MOVIMENTAÇÕES ==========
    // Retorna todas as movimentações, filtrando por nome, fornecedor ou cliente, se informado.
    public DataTable ListarMovimentacoes(string filtro = "")
    {
        DataTable dt = new DataTable();
        using (MySqlConnection conn = new MySqlConnection(conexaoBD))
        {
            string query = @"SELECT id, produtoNome, tipo, quantidade, 
                            fornecedor, cliente, notaFiscal, observacao,
                            usuarioNome, dataMovimentacao 
                            FROM movimentacoes";

            // Adiciona filtro de busca opcional
            if (!string.IsNullOrEmpty(filtro))
            {
                query += " WHERE produtoNome LIKE @filtro OR fornecedor LIKE @filtro OR cliente LIKE @filtro";
            }

            query += " ORDER BY dataMovimentacao DESC";

            MySqlCommand cmd = new MySqlCommand(query, conn);

            if (!string.IsNullOrEmpty(filtro))
            {
                cmd.Parameters.AddWithValue("@filtro", "%" + filtro + "%");
            }

            // Preenche o DataTable com os resultados
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(dt);
        }
        return dt;
    }
}
