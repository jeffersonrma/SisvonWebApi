using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Conexao
{
    public class Conectar : IDisposable
    {
        private SqlConnection _conexao;
        
        public Conectar()
        {
            string stringConexao = ConfigurationManager.ConnectionStrings["s9sql"].ToString();

            _conexao = new SqlConnection(stringConexao);
            _conexao.Open();
        }

        public SqlDataReader Buscar(SqlCommand comando)
        {
            try
            {
                SqlConnection con = _conexao;
                comando.Connection = con;
                SqlDataReader dr = comando.ExecuteReader();
                return dr;
            }
            catch (SqlException ex)
            {           
                throw ex;
            }

        }
        public void CRUD(SqlCommand comando)
        {
            try
            {
                SqlConnection con = _conexao;
                comando.Connection = con;
                comando.ExecuteNonQuery();
                con.Close();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public void Dispose()
        {
            if (_conexao != null && _conexao.State == ConnectionState.Open)
                _conexao.Close();
            if (_conexao != null)
                _conexao.Dispose();
            _conexao = null;
        }
    }
}
