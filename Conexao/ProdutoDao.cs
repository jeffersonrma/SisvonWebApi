using System;
using System.Data;
using System.Data.SqlClient;

namespace Conexao
{
    class ProdutoDao
    {
        private readonly Conectar _conexao;
        public ProdutoDao(Conectar conexao)
        {
            _conexao = conexao;
        }
        public Produto Buscar(string codBarras)
        {
            SqlCommand comando = new SqlCommand
            {
                CommandType = CommandType.Text,
                CommandText = @"SELECT Prod_Serv.Ordem, 
	                                       Prod_Serv.Codigo, 
	                                       Prod_Serv.Nome_Nota,
	                                       Unidades_Venda.Nome as 'unidade_venda', 
	                                       Prod_Serv_Precos.Preco, 
	                                       Prod_Serv.Tipo 
                                      FROM Prod_Serv, 
	                                       Prod_Serv_Precos, 
	                                       Unidades_Venda 
                                     WHERE Prod_Serv.Ordem = Prod_Serv_Precos.Ordem_Prod_Serv 
                                       AND Unidades_Venda.Ordem = Prod_Serv.Ordem_Unidade_Venda
                                       AND Prod_Serv_Precos.Ordem_Tabela_Preco = '4' 
                                       AND Prod_Serv.Codigo = @EAN"
            };

            comando.Parameters.AddWithValue("@EAN", codBarras);
            SqlDataReader dr = _conexao.Buscar(comando);
            Produto objProduto = new Produto();
            if (dr.HasRows)
            {
                dr.Read();

                objProduto.Ordem = Convert.ToInt32(dr["Ordem"]);
                objProduto.Codigo = (string)dr["Codigo"];
                objProduto.Nome = (string)dr["Nome_Nota"];
                objProduto.UnidadeVenda = (string)dr["unidade_venda"];
                objProduto.Preco = Convert.ToDouble(dr["Preco"]);
                objProduto.Tipo = Convert.ToChar(dr["Tipo"]);
            }
            else
            {
                objProduto = null;
            }
            return objProduto;
        }
    }
}
