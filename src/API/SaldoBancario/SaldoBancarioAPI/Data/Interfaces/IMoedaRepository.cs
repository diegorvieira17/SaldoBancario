using SaldoBancarioAPI.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SaldoBancarioAPI.Data.Interfaces
{
    public interface IMoedaRepository : IRepository
    {
        Task<List<Moeda>> Get();
        Task<Moeda> Get(Guid moedaId);
        Task<Moeda> GetByName(string name);
    }
}
