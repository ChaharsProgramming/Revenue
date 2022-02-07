using Revenue.AI.Entity;

namespace Revenue.AI.Application.ViewModels.Order
{
    public class OrderRequestVM
    {
        public OrderType Type { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public int BasketId { get; set; }
    }
}