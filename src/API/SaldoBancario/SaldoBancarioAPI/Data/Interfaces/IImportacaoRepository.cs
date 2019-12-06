using SaldoBancarioAPI.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SaldoBancarioAPI.Data.Interfaces
{
    public interface IImportacaoRepository : IRepository
    {
        Task<List<Importacao>> Get();
        Task<Importacao> Get(Guid importacaoId);
    }
}
