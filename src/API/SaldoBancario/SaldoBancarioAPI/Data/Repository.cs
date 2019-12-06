using SaldoBancarioAPI.Data.Interfaces;
using System.Threading.Tasks;

namespace SaldoBancarioAPI.Data
{
    public class Repository : IRepository
    {
        public DataContext _context { get; }
        public Repository(DataContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : new()
        {
            _context.Add(entity);
        }
        public void Update<T>(T entity) where T : new()
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : new()
        {
            _context.Remove(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }

    }
}
