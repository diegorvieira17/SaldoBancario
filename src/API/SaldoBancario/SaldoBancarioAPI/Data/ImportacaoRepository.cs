using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SaldoBancarioAPI.Data.Interfaces;
using SaldoBancarioAPI.Model;

namespace SaldoBancarioAPI.Data
{
    public class ImportacaoRepository : Repository, IImportacaoRepository
    {
        public ImportacaoRepository(DataContext context) : base(context)
        {
        }

        public async Task<List<Importacao>> Get()
        {
            return await _context.Importacoes.AsNoTracking().OrderByDescending(i => i.DataImportacao).ToListAsync();
        }

        public async Task<Importacao> Get(Guid importacaoId)
        {
            return await _context.Importacoes.AsNoTracking().Where(i => i.Id == importacaoId).FirstOrDefaultAsync();
        }
    }
}
