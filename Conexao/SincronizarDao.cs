using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Conexao
{
    public class SincronizarDao
    {
        private readonly Conectar _conexao;
        public SincronizarDao(Conectar conexao)
        {
            _conexao = conexao;
        }
        public ICollection<Sincronizar> BuscarTodos()
        {
            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.Text;
            comando.CommandText = @" SELECT Prod_Serv.Ordem, 
		                                    Prod_Serv.Codigo, 
		                                    Prod_Serv.Nome_Nota,
		                                    Prod_Serv_Precos.Preco
                                       FROM Prod_Serv, 
		                                    Prod_Serv_Precos
                                      WHERE Prod_Serv.Ordem = Prod_Serv_Precos.Ordem_Prod_Serv 
		                                    AND Prod_Serv_Precos.Ordem_Tabela_Preco = '4' 
		                                    AND Prod_Serv.Tipo = 'N'
		                                    AND Prod_Serv.Ordem_Pesquisa_3 = 2
		                                    ";

            SqlDataReader dr = _conexao.Buscar(comando);
            if (!dr.HasRows) return null;
            ICollection<Sincronizar> lista = new List<Sincronizar>();
            while (dr.Read())
            {
                Sincronizar objSincronizar = new Sincronizar
                {
                    Ordem = Convert.ToInt32(dr["Ordem"]),
                    Codigo = (string)dr["Codigo"],
                    Nome = (string)dr["Nome_Nota"],
                    Preco = Convert.ToDecimal(dr["Preco"])
                };

                objSincronizar.Estoque = Convert.ToInt32(new EstoqueDAO(_conexao).Buscar(objSincronizar.Ordem));
                var grupo = new GrupoDAO(_conexao).BuscarPorProduto(objSincronizar.Ordem);
                objSincronizar.GrupoNome = grupo.Nome;
                objSincronizar.GrupoOrdem = grupo.GrupoId.ToString();
                var subClasse = new SubClasseDAO(_conexao).BuscarPorProduto(objSincronizar.Ordem);
                objSincronizar.SubClasseNome = subClasse.Nome;
                objSincronizar.SubClasseOrdem = subClasse.SubClasseId.ToString();

                lista.Add(objSincronizar);
            }
            return lista;
        }
        public Sincronizar BuscarPorCodigo(string cod)
        {
            SqlCommand comando = new SqlCommand
            {
                CommandType = CommandType.Text,
                CommandText = @" SELECT Prod_Serv.Ordem, 
		                                Prod_Serv.Codigo, 
		                                Prod_Serv.Nome_Nota,
		                                Prod_Serv_Precos.Preco
                                    FROM Prod_Serv, 
		                                Prod_Serv_Precos
                                    WHERE Prod_Serv.Ordem = Prod_Serv_Precos.Ordem_Prod_Serv 
		                                AND Prod_Serv_Precos.Ordem_Tabela_Preco = '4' 
		                                AND Prod_Serv.Tipo = 'N'
		                                AND Prod_Serv.Ordem_Pesquisa_3 = 2
                                        AND Prod_Serv.Codigo = @Codigo
		                                "
            };

            comando.Parameters.AddWithValue("@Codigo", cod);
            SqlDataReader dr = _conexao.Buscar(comando);
            if (!dr.HasRows) return null;

            dr.Read();

            Sincronizar objSincronizar = new Sincronizar
            {
                Ordem = Convert.ToInt32(dr["Ordem"]),
                Codigo = (string)dr["Codigo"],
                Nome = (string)dr["Nome_Nota"],
                Preco = Convert.ToDecimal(dr["Preco"])
            };

            objSincronizar.Estoque = Convert.ToInt32(new EstoqueDAO(_conexao).Buscar(objSincronizar.Ordem));
            var grupo = new GrupoDAO(_conexao).BuscarPorProduto(objSincronizar.Ordem);
            objSincronizar.GrupoNome = grupo.Nome;
            objSincronizar.GrupoOrdem = grupo.GrupoId.ToString();
            var subClasse = new SubClasseDAO(_conexao).BuscarPorProduto(objSincronizar.Ordem);
            objSincronizar.SubClasseNome = subClasse.Nome;
            objSincronizar.SubClasseOrdem = subClasse.SubClasseId.ToString();


            return objSincronizar;
        }
        public Sincronizar BuscarPorOrden(int orden)
        {
            SqlCommand comando = new SqlCommand
            {
                CommandType = CommandType.Text,
                CommandText = @" SELECT Prod_Serv.Ordem, 
		                                Prod_Serv.Codigo, 
		                                Prod_Serv.Nome_Nota,
		                                Prod_Serv_Precos.Preco
                                    FROM Prod_Serv, 
		                                Prod_Serv_Precos
                                    WHERE Prod_Serv.Ordem = Prod_Serv_Precos.Ordem_Prod_Serv 
		                                AND Prod_Serv_Precos.Ordem_Tabela_Preco = '4' 
		                                AND Prod_Serv.Tipo = 'N'
		                                AND Prod_Serv.Ordem_Pesquisa_3 = 2
                                        AND Prod_Serv.Ordem = @Ordem
		                                "
            };

            comando.Parameters.AddWithValue("@Ordem", orden);
            SqlDataReader dr = _conexao.Buscar(comando);
            if (!dr.HasRows) return null;

            dr.Read();

            Sincronizar objSincronizar = new Sincronizar
            {
                Ordem = Convert.ToInt32(dr["Ordem"]),
                Codigo = (string)dr["Codigo"],
                Nome = (string)dr["Nome_Nota"],
                Preco = Convert.ToDecimal(dr["Preco"])
            };

            objSincronizar.Estoque = Convert.ToInt32(new EstoqueDAO(_conexao).Buscar(objSincronizar.Ordem));
            var grupo = new GrupoDAO(_conexao).BuscarPorProduto(objSincronizar.Ordem);
            objSincronizar.GrupoNome = grupo.Nome;
            objSincronizar.GrupoOrdem = grupo.GrupoId.ToString();
            var subClasse = new SubClasseDAO(_conexao).BuscarPorProduto(objSincronizar.Ordem);
            objSincronizar.SubClasseNome = subClasse.Nome;
            objSincronizar.SubClasseOrdem = subClasse.SubClasseId.ToString();


            return objSincronizar;
        }
    }
}
