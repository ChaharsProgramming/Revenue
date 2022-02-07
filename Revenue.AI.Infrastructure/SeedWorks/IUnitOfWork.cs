using System;
using System.Threading.Tasks;

namespace Revenue.AI.Infrastructure.SeedWorks
{
    public interface IUnitOfWork    
    {
        Task<int> SaveChangeAsync();   
		Task<TResult> ExecuteTransactionAsync<TResult>(Func<Task<TResult>> func);
    }  
}