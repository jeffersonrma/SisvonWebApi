using System;

namespace Conexao
{
    public class Promocao
    {
        private DateTime dataFinal;
        private double promocaoPreco;

        public double PromocaoPreco
        {
            get { return promocaoPreco; }
            set { promocaoPreco = value; }
        }

        public DateTime DataFinal
        {
            get { return dataFinal; }
            set { dataFinal = value; }
        }
    }
}
