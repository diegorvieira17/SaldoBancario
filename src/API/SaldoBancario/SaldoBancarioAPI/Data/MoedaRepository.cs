using Microsoft.EntityFrameworkCore;
using SaldoBancarioAPI.Data.Interfaces;
using SaldoBancarioAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaldoBancarioAPI.Data
{
    public class MoedaRepository : Repository, IMoedaRepository
    {
        public MoedaRepository(DataContext context) : base(context)
        {
        }

        public async Task<List<Moeda>> Get()
        {
            return await _context.Moedas.AsNoTracking().ToListAsync();
        }

        public async Task<Moeda> Get(Guid moedaId)
        {
            return await _context.Moedas.AsNoTracking().Where(m => m.Id == moedaId).FirstOrDefaultAsync();
        }

        public async Task<Moeda> GetByName(string name)
        {
            return await _context.Moedas.AsNoTracking().Where(m => m.Nome == name).LastOrDefaultAsync();
        }
    }
}
