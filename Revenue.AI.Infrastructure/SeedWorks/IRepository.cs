using System;
using System.Threading.Tasks;

namespace Revenue.AI.Infrastructure.SeedWorks
{
    public interface IRepository<T> where T : class   
    {   
        void Add(T entity);   
        void Delete(T entity);   
        void Update(T entity);
        Task<T> GetAsync(int id);
        Task<T> GetAsync(DateTime dateTime);
    }
}