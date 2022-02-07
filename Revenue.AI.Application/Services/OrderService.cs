using System.Threading.Tasks;
using Revenue.AI.Application.SeedWorks;
using Revenue.AI.Application.Services.Interfaces;
using Revenue.AI.Application.ViewModels.Order;
using Revenue.AI.Entity;
using Revenue.AI.Entity.Entities.Orders;
using Revenue.AI.Infrastructure.SeedWorks;

namespace Revenue.AI.Application.Services
{
    public class OrderService : BaseService, IOrderService
    {
        private readonly IRepository<Order> _repository;

        public OrderService(IUnitOfWork unitOfWork, IRepository<Order> repository) : base(unitOfWork)
        {
            _repository = repository;
        }

        public async Task<Order> Get(int id)
        {
            return await _repository.GetAsync(id);
        }

        public async Task<Order> CreateOrder(OrderRequestVM orderInfo)
        {
            var order = new Order(orderInfo.Type, orderInfo.CustomerName, orderInfo.Address);

            await UnitOfWork.ExecuteTransactionAsync(async () =>
            {
                _repository.Add(order);
                return await UnitOfWork.SaveChangeAsync();
            });
            return order;
        }

        public async Task MarkOrderAsPaid(int orderId)
        {
            var order = await _repository.GetAsync(orderId);
            order.Status = OrderStatus.Paid;
            await UnitOfWork.ExecuteTransactionAsync(async () =>
            {
                return await UnitOfWork.SaveChangeAsync();
            });
        }
    }
}