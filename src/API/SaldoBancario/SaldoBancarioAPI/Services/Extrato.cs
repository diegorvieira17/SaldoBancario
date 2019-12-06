using OFXParser.Entities;
using SaldoBancarioAPI.Model;
using System;
using System.Collections.Generic;

namespace SaldoBancarioAPI.Services
{
    public static class Extrato
    {
        public static List<Movimentacao> MontaExtrato(decimal saldo, Importacao importacao)
        {
            Extract extratoBancario = OFXParser.OFXParser.GetExtract(importacao.NomeArquivo);

            if (extratoBancario != null)
            {
                List<Movimentacao> movimentacoes = new List<Movimentacao>();

                var saldoAtual = saldo;

                foreach (var transacao in extratoBancario.Transactions)
                {
                     saldoAtual = saldoAtual + Convert.ToDecimal(transacao.TransactionValue);

                    movimentacoes.Add(new Movimentacao()
                    {
                        Id = Guid.NewGuid(),
                        DataMovimento = transacao.Date,
                        Descricao = transacao.Description,
                        Valor = Convert.ToDecimal(transacao.TransactionValue),
                        SaldoAtual = saldoAtual,
                        Importacao = importacao
                    });
                }

                return movimentacoes;
            }

            return null;
        }
    }
}
