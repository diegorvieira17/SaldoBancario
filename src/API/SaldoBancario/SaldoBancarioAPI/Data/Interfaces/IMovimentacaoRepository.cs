using SaldoBancarioAPI.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SaldoBancarioAPI.Data.Interfaces
{
    public interface IMovimentacaoRepository : IRepository
    {
        Task<List<Movimentacao>> Get();
        Task<Movimentacao> Get(Guid movimentacaoId);
        Task<Decimal> GetSaldo();
    }
}
