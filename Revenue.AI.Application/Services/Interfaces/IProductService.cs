using System.Threading.Tasks;
using Revenue.AI.Entity.Entities;

namespace Revenue.AI.Application.Services.Interfaces
{
    public interface IProductService
    {
        public Task<Product> Get(int id);
        public Task<Product> Add(Product product);
        public Task Remove(int id);
    }
}