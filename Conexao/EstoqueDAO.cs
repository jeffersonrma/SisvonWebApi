using System;
using System.Data;
using System.Data.SqlClient;

namespace Conexao
{
    public class EstoqueDAO
    {
        private readonly Conectar _conexao;
        public EstoqueDAO(Conectar conexao)
        {
            _conexao = conexao;
        }

        public double Buscar(int ordem)
        {
            SqlCommand comando = new SqlCommand
            {
                CommandType = CommandType.Text,
                CommandText =
                    @"SELECT SUM(Estoque_Atual.Qtde_Estoque_Atual) as 'estoque' FROM Estoque_Atual WHERE Ordem_Filial = 1 AND Ordem_Prod_Serv = @Ordem"
            };

            comando.Parameters.AddWithValue("@Ordem", ordem);
            SqlDataReader dr = _conexao.Buscar(comando);
            double estoque;
            if (dr.HasRows)
            {
                dr.Read();
                estoque = Convert.ToDouble(dr["estoque"]);
            }
            else
            {
                estoque = -1;
            }
            return estoque;
        }
    }
}
