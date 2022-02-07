using System.Threading.Tasks;
using Revenue.AI.Application.ViewModels.Order;
using Revenue.AI.Entity.Entities.Orders;

namespace Revenue.AI.Application.Services.Interfaces
{
    public interface IOrderService
    {
        public Task<Order> Get(int id);
        public Task<Order> CreateOrder(OrderRequestVM orderInfo);
        public Task MarkOrderAsPaid(int orderId);
    }
}