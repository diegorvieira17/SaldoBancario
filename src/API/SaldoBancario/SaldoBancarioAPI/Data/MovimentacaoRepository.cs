using Microsoft.EntityFrameworkCore;
using SaldoBancarioAPI.Data.Interfaces;
using SaldoBancarioAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaldoBancarioAPI.Data
{
    public class MovimentacaoRepository : Repository, IMovimentacaoRepository
    {
        public MovimentacaoRepository(DataContext context) : base(context)
        {
        }

        public async Task<List<Movimentacao>> Get()
        {
            return await _context.Movimentacoes.AsNoTracking().OrderByDescending(m => m.DataMovimento).ToListAsync();
        }

        public async Task<Movimentacao> Get(Guid movimentacaoId)
        {
            return await _context.Movimentacoes.AsNoTracking().Where(m => m.Id == movimentacaoId).FirstOrDefaultAsync();
        }

        public async Task<decimal> GetSaldo()
        {
            return await _context.Movimentacoes.AsNoTracking().Select(m => m.SaldoAtual).LastOrDefaultAsync();
        }
    }
}
