using System;
using System.Collections.Generic;

namespace Conexao
{
    public class Produto
    {

        private int ordem;
        private string codigo;
        private string nome;
        private double preco;
        private char tipo;
        private string unidadeVenda;
        private IList<Lote> lotes;
        private IList<Grade> grades;
        private double estoque;
        private double promocao;

        public Exception Erro { get; set; }

        public double Promocao
        {
            get { return promocao; }
            set { promocao = value; }
        }

        public IList<Grade> Grades
        {
            get { return grades; }
            set { grades = value; }
        }
        public string UnidadeVenda
        {
            get { return unidadeVenda; }
            set { unidadeVenda = value; }
        }
        public string Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }
        public int Ordem
        {
            get { return ordem; }
            set { ordem = value; }
        }
        public char Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }

        public IList<Lote> Lotes
        {
            get { return lotes; }
            set { lotes = value; }
        }

        public double Estoque
        {
            get { return estoque; }
            set { estoque = value; }
        }

        public double Preco
        {
            get { return preco; }
            set { preco = value; }
        }

        public string Nome
        {
            get { return nome; }
            set { nome = value; }
        }


        public Produto Buscar(string EAN)
        {
            using (Conectar conexao = new Conectar())
            {
                ProdutoDao ObjDAO = new ProdutoDao(conexao);
                Produto produto = ObjDAO.Buscar(EAN);

                if (produto == null)
                    throw new Exception("Produto nao emcontrado");

                if (produto.tipo == 'G')
                {
                    produto.grades = new Grade(conexao).BuscarListaGrade(produto.ordem);
                    if (produto.grades == null)
                        throw new Exception("Erro ao buscar Grade");
                }
                else if (produto.tipo == 'L')
                {
                    produto.lotes = new Lote().BuscarListaLote(produto.ordem);
                    if (produto.lotes == null)
                        throw new Exception("Erro ao buscar Lotes");
                }

                produto.estoque = new EstoqueDAO(conexao).Buscar(produto.ordem);

                produto.promocao = new PromocaoDAO(conexao).Buscar(produto.ordem);

                if (estoque == -1)
                    throw new Exception("Erro ao buscar quantidade");

                return produto;
            }
        }

    }
}
