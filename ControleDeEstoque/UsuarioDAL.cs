using ControleDeEstoque;
using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Data;

public class UsuarioDAL
{
    private string conexaoBD = "Server=localhost;Database=controledeestoque;Uid=root;Pwd=ZnTCruZ1@;";

    // ========== VALIDAR LOGIN ==========
    public DataTable ValidarLogin(string usuario, string senha)
    {
        DataTable dt = new DataTable();

        using (MySqlConnection conn = new MySqlConnection(conexaoBD))
        {
            // Gerar hash da senha
            string senhaHash = CriptografiaHelper.GerarHashSHA256(senha);

            // SQL
            string sql = "SELECT * FROM logins WHERE usuarioFuncionario = @usuario AND senhaHash = @hash AND ativo = TRUE";

            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@usuario", usuario);
            cmd.Parameters.AddWithValue("@hash", senhaHash);

            // Executar
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            conn.Open();
            adapter.Fill(dt);
        }

        return dt;
    }

    // ========== LISTAR USUÁRIOS (com filtro opcional) ==========
    public DataTable ListarUsuarios(string filtro = "")
    {
        DataTable dt = new DataTable();
        using (MySqlConnection conn = new MySqlConnection(conexaoBD))
        {
            string query = @"SELECT id, nomeFuncionario, usuarioFuncionario, tipoUsuario, ativo, dataCriacao 
                           FROM logins";

            // Se houver filtro, adicionar WHERE
            if (!string.IsNullOrEmpty(filtro))
            {
                query += " WHERE CAST(id AS CHAR) LIKE @filtro " +
                     "OR nomeFuncionario LIKE @filtro " +
                     "OR usuarioFuncionario LIKE @filtro";
            }

            query += " ORDER BY dataCriacao DESC";

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

    // ========== ALTERNAR STATUS ATIVO/INATIVO ==========
    public bool ToggleAtivoUsuario(int id)
    {
        try
        {
            using (MySqlConnection conn = new MySqlConnection(conexaoBD))
            {
                conn.Open();
                string query = "UPDATE logins SET ativo = NOT ativo WHERE id = @id";
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

    // ========== BUSCAR USUÁRIO POR ID (para edição) ==========
    public DataTable BuscarUsuarioPorId(int id)
    {
        DataTable dt = new DataTable();
        using (MySqlConnection conn = new MySqlConnection(conexaoBD))
        {
            string query = "SELECT * FROM logins WHERE id = @id";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", id);

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(dt);
        }
        return dt;
    }

    // ========== INSERIR NOVO USUÁRIO (COM HASH) ==========
    public bool InserirUsuario(string nome, string usuario, string senha, string tipo, bool ativo)
    {
        try
        {
            using (MySqlConnection conn = new MySqlConnection(conexaoBD))
            {
                conn.Open();

                // Gerar hash da senha
                string senhaHash = CriptografiaHelper.GerarHashSHA256(senha);

                string query = @"INSERT INTO logins (nomeFuncionario, usuarioFuncionario, senhaHash, tipoUsuario, ativo) 
                               VALUES (@nome, @usuario, @senhaHash, @tipo, @ativo)";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@nome", nome);
                cmd.Parameters.AddWithValue("@usuario", usuario);
                cmd.Parameters.AddWithValue("@senhaHash", senhaHash);
                cmd.Parameters.AddWithValue("@tipo", tipo);
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

    // ========== ATUALIZAR USUÁRIO EXISTENTE (COM HASH) ==========
    public bool AtualizarUsuario(int id, string nome, string usuario, string senha, string tipo, bool ativo)
    {
        try
        {
            using (MySqlConnection conn = new MySqlConnection(conexaoBD))
            {
                conn.Open();

                string query;

                if (string.IsNullOrEmpty(senha))
                {
                    // Se senha vazia, não atualizar senha
                    query = @"UPDATE logins SET 
                            nomeFuncionario = @nome, 
                            usuarioFuncionario = @usuario, 
                            tipoUsuario = @tipo, 
                            ativo = @ativo 
                            WHERE id = @id";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@nome", nome);
                    cmd.Parameters.AddWithValue("@usuario", usuario);
                    cmd.Parameters.AddWithValue("@tipo", tipo);
                    cmd.Parameters.AddWithValue("@ativo", ativo);

                    int linhasAfetadas = cmd.ExecuteNonQuery();
                    return linhasAfetadas > 0;
                }
                else
                {
                    // Gerar hash da senha
                    string senhaHash = CriptografiaHelper.GerarHashSHA256(senha);

                    // Se senha informada, atualizar também
                    query = @"UPDATE logins SET 
                            nomeFuncionario = @nome, 
                            usuarioFuncionario = @usuario, 
                            senhaHash = @senhaHash, 
                            tipoUsuario = @tipo, 
                            ativo = @ativo 
                            WHERE id = @id";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@nome", nome);
                    cmd.Parameters.AddWithValue("@usuario", usuario);
                    cmd.Parameters.AddWithValue("@senhaHash", senhaHash);
                    cmd.Parameters.AddWithValue("@tipo", tipo);
                    cmd.Parameters.AddWithValue("@ativo", ativo);

                    int linhasAfetadas = cmd.ExecuteNonQuery();
                    return linhasAfetadas > 0;
                }
            }
        }
        catch
        {
            return false;
        }
    }

    // ========== VERIFICAR SE USUÁRIO JÁ EXISTE (evitar duplicatas) ==========
    public bool UsuarioExiste(string usuario, int? idIgnorar = null)
    {
        using (MySqlConnection conn = new MySqlConnection(conexaoBD))
        {
            conn.Open();
            string query = "SELECT COUNT(*) FROM logins WHERE usuarioFuncionario = @usuario";

            if (idIgnorar.HasValue)
            {
                query += " AND id != @id";
            }

            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@usuario", usuario);

            if (idIgnorar.HasValue)
            {
                cmd.Parameters.AddWithValue("@id", idIgnorar.Value);
            }

            int count = Convert.ToInt32(cmd.ExecuteScalar());
            return count > 0;
        }
    }
}
