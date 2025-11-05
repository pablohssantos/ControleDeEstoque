using System;
using System.Security.Cryptography;
using System.Text;

namespace ControleDeEstoque
{
    public class CriptografiaHelper
    {
        // Método estático para gerar um hash SHA256 a partir de uma string
        public static string GerarHashSHA256(string texto)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // Converter o texto em bytes usando codificação UTF-8
                byte[] bytesTexto = Encoding.UTF8.GetBytes(texto);

                // Gerar o hash do texto em bytes
                byte[] bytesHash = sha256.ComputeHash(bytesTexto);

                // Converter os bytes do hash em formato hexadecimal legível
                StringBuilder resultado = new StringBuilder();

                for (int i = 0; i < bytesHash.Length; i++)
                {
                    resultado.Append(bytesHash[i].ToString("x2")); // Cada byte vira dois caracteres hexadecimais
                }

                // Retorna o hash final em formato string
                return resultado.ToString();
            }
        }
    }
}
