using Microsoft.EntityFrameworkCore;
using SaldoBancarioAPI.Model;

namespace SaldoBancarioAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Importacao> Importacoes { get; set; }
        public DbSet<Moeda> Moedas { get; set; }
        public DbSet<Movimentacao> Movimentacoes { get; set; }
    }
}
