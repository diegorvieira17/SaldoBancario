using System;

namespace SaldoBancarioAPI.Model
{
    public class Movimentacao
    {
        public Guid Id { get; set; }
        public DateTime DataMovimento { get;  set; }
        public string Descricao { get;  set; }
        public decimal Valor { get;  set; }
        public decimal SaldoAtual { get;  set; }
        public Importacao Importacao { get; set; }
    }
}
