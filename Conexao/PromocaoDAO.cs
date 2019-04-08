using System;
using System.Data;
using System.Data.SqlClient;

namespace Conexao
{
    public class PromocaoDAO
    {
        private readonly Conectar _conexao;
        public PromocaoDAO(Conectar conexao)
        {
            _conexao = conexao;
        }
        public double Buscar(int Ordem)
        {
            SqlCommand comando = new SqlCommand
            {
                CommandType = CommandType.Text,
                CommandText = @" SELECT Prod_Serv_Promocao.Preco as 'promocao',
                                            Prod_Serv_Promocao.Data_Final
                                       FROM Prod_Serv_Promocao
                                      WHERE Prod_Serv_Promocao.Ordem_Prod_Serv = @Ordem"
            };

            comando.Parameters.AddWithValue("@Ordem", Ordem);
            SqlDataReader dr = _conexao.Buscar(comando);
            double promocao = -1;
            if (dr.HasRows)
            {
                dr.Read();

                DateTime dataFinal = Convert.ToDateTime(dr["Data_Final"]);
                if (DateTime.Today < dataFinal)
                    promocao = Convert.ToDouble(dr["promocao"]);
            }
            else
            {
                promocao = -1;
            }
            return promocao;
        }
    }
}
