using System.Threading.Tasks;

namespace SaldoBancarioAPI.Data.Interfaces
{
    public interface IRepository
    {
        void Add<T>(T entity)where T: new();
        void Update<T>(T entity)where T: new();
        void Delete<T>(T entity)where T: new();
        Task<bool> SaveChangesAsync();
    }
}
