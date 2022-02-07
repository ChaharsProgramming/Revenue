using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Revenue.AI.Application.Services.Interfaces;
using Revenue.AI.Application.ViewModels.Order;
using Revenue.AI.Entity.Entities.Orders;

namespace Revenue.AI.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IOrderService _orderService;
        private readonly IDistributedCache myDistributedCache;
        private readonly string RedisKey = "Revenue.AI_Source_CachedTimeUTC_";

        public OrderController(ILogger<OrderController> logger, IOrderService orderService, IDistributedCache distributedCache)
        {
            _logger = logger;
            _orderService = orderService;
            myDistributedCache = distributedCache;
        }

        [HttpGet("{id:int}")]
        public async Task<Order> Get(int id)
        {
            _logger.LogInformation("Getting order id {Id}", id);
            return await _orderService.Get(id);
        }
        
        [HttpPost]
        public async Task<Order> CreateOrder(OrderRequestVM orderInfo)
        {
            _logger.LogInformation("Creating order");
            var response= await _orderService.CreateOrder(orderInfo);
            var jsonData = JsonSerializer.Serialize(orderInfo);
            await myDistributedCache.SetStringAsync(RedisKey + DateTime.Now.ToString("yyyyMMdd_hhmm"), jsonData);
            return response;
        }
    }
}