
using System;
using System.Data;
using System.Data.SqlClient;

namespace Conexao
{
    public class SubClasseDAO
    {
        private readonly Conectar _conexao;
        public SubClasseDAO(Conectar conexao)
        {
            _conexao = conexao;
        }
        public SubClasse BuscarPorProduto(int ordem)
        {
            SqlCommand comando = new SqlCommand
            {
                CommandType = CommandType.Text,
                CommandText = @" SELECT SubClasses.Ordem,
		                                SubClasses.Nome
                                   FROM SubClasses, 
		                                Prod_Serv
                                  WHERE Prod_Serv.Ordem_SubClasse = SubClasses.Ordem
	                                AND Prod_Serv.Ordem = @Ordem"
            };

            comando.Parameters.AddWithValue("@Ordem", ordem);
            SqlDataReader dr = _conexao.Buscar(comando);
            if (!dr.HasRows) return null;
            dr.Read();

            SubClasse objSubClasse = new SubClasse();
            objSubClasse.SubClasseId = Convert.ToInt32(dr["Ordem"]);
            objSubClasse.Nome = Convert.ToString(dr["Nome"]);

            return objSubClasse;
        }
    }
}
