using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Conexao
{
    public class LoteDAO
    {
        private readonly Conectar _conexao;
        public LoteDAO(Conectar conexao)
        {
            _conexao = conexao;
        }
        public IList<Lote> Buscar(int ordem)
        {
            SqlCommand comando = new SqlCommand
            {
                CommandType = CommandType.Text,
                CommandText = @"SELECT Prod_Serv_Lote.Lote, 
	                                       Estoque_Atual_Lote.Quantidade 
                                      FROM Prod_Serv_Lote, 
	                                       Estoque_Atual_Lote
                                     WHERE Prod_Serv_Lote.Ordem = Estoque_Atual_Lote.Ordem_Prod_Serv_Lote
                                       AND Prod_Serv_Lote.Ordem_Prod_Serv = @Ordem"
            };

            comando.Parameters.AddWithValue("@Ordem", ordem);
            SqlDataReader dr = _conexao.Buscar(comando);
            IList<Lote> lista = new List<Lote>();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Lote objLote = new Lote
                    {
                        Estoque = Convert.ToDouble(dr["Quantidade"]),
                        Nome = (string) dr["Lote"]
                    };
                    lista.Add(objLote);
                }


            }
            else
            {
                lista = null;
            }
            return lista;
        }
    }
}
