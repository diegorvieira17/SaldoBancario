using System;
using System.Collections.Generic;

namespace SaldoBancarioAPI.Model
{
    public class Importacao
    {
        public Guid Id { get; set; }
        public string NomeArquivo { get; set; }
        public DateTime DataImportacao { get; set; }
        public List<Movimentacao> Movimentacoes { get; set; }
    }
}
