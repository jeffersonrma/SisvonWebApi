
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Conexao
{
    public class GrupoDAO
    {
        private readonly Conectar _conexao;
        public GrupoDAO(Conectar conexao)
        {
            _conexao = conexao;
        }
        public Grupo BuscarPorProduto(int ordem)
        {
            SqlCommand comando = new SqlCommand
            {
                CommandType = CommandType.Text,
                CommandText = @" SELECT Grupos.Ordem,
		                                    Grupos.Nome
                                       FROM Grupos, 
		                                    Prod_Serv
                                      WHERE Prod_Serv.Ordem_Grupo = Grupos.Ordem
	                                    AND Prod_Serv.Ordem = @Ordem"
            };

            comando.Parameters.AddWithValue("@Ordem", ordem);
            SqlDataReader dr = _conexao.Buscar(comando);
            if (!dr.HasRows) return null;
            dr.Read();

            Grupo objGrupo = new Grupo();
            objGrupo.GrupoId = Convert.ToInt32(dr["Ordem"]);
            objGrupo.Nome = Convert.ToString(dr["Nome"]);
            return objGrupo;
        }
    }
}
