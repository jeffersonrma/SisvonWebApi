using System.Collections.Generic;

namespace Conexao
{
    public class Lote
    {
        private string nome;
        private double estoque;

        public double Estoque
        {
            get { return estoque; }
            set { estoque = value; }
        }

        public string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        public IList<Lote> BuscarListaLote(int ordemProduto)
        {
            using (Conectar conexao = new Conectar())
            {
                return new LoteDAO(conexao).Buscar(ordemProduto);
            }
        }
    }
}
