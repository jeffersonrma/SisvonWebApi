using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Conexao
{

    public class Grade
    {
        private string cor;
        private double estoque;
        private string codigo;
        private Conectar _conexao;
        public Grade(Conectar conexao)
        {
            _conexao = conexao;
        }

        public Grade()
        {
            
        }

        public string Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        public double Estoque
        {
            get { return estoque; }
            set { estoque = value; }
        }

        public string Cor
        {
            get { return cor; }
            set { cor = value; }
        }

        public IList<Grade> BuscarListaGrade(int ordemProduto)
        {
            return new GradeDAO(_conexao).Buscar(ordemProduto);
        }
    }
}
