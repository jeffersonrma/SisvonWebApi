
using System;
using System.Runtime.Serialization;

namespace Conexao
{
    public class Sincronizar
    {
        public int Ordem { get; set; }
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public int Estoque { get; set; }
        public string GrupoNome { get; set; }
        public string GrupoOrdem { get; set; }
        public string SubClasseNome { get; set; }
        public string SubClasseOrdem { get; set; }
        public Exception Erro { get; set; }
    }

}
