using System;

namespace SaldoBancarioAPI.Model
{
    public class Moeda
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public decimal Compra { get; set; }
        public decimal Venda { get; set; }
        public decimal Variacao { get; set; }
        public DateTime DataCotacao { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Moeda)
            {
                Moeda moeda = (Moeda)obj;
                Moeda m = (Moeda)obj;
                if (this.Nome == m.Nome && this.Compra == m.Compra && this.Venda == m.Venda && this.Variacao == m.Variacao)
                    return true;

                return false;
            }

            return false;
        }
    }
}
