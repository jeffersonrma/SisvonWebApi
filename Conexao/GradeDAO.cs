using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Conexao
{
    public class GradeDAO
    {
        private readonly Conectar _conexao;
        public GradeDAO(Conectar conexao)
        {
            _conexao = conexao;
        }   
        public IList<Grade> Buscar(int ordem)
        {
            SqlCommand comando = new SqlCommand
            {
                CommandType = CommandType.Text,
                CommandText = @"SELECT Cores.Nome, 
	                                       Estoque_Atual.Qtde_Estoque_Atual, 
	                                       Prod_Serv_Grade.Codigo 
                                      FROM Cores, 
	                                       Estoque_Atual, 
	                                       Prod_Serv_Grade 
                                     WHERE Prod_Serv_Grade.Ordem_Cor = Cores.Ordem 
                                       AND Estoque_Atual.Ordem_Cor = Cores.Ordem 
                                       AND Estoque_Atual.Ordem_Prod_Serv = Prod_Serv_Grade.Ordem_Prod_Serv
                                       AND Estoque_Atual.Ordem_Filial = 1
                                       AND Prod_Serv_Grade.Ordem_Prod_Serv = @Ordem"
            };

            comando.Parameters.AddWithValue("@Ordem", ordem);
            SqlDataReader dr = _conexao.Buscar(comando);
            IList<Grade> lista = new List<Grade>();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Grade objGrade = new Grade()
                    {
                        Estoque = Convert.ToDouble(dr["Qtde_Estoque_Atual"]),
                        Cor = (string) dr["Nome"],
                        Codigo = (string) dr["Codigo"]
                    };
                    lista.Add(objGrade);
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
