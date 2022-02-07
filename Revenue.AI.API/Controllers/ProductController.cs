using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Revenue.AI.Application.Services.Interfaces;
using Revenue.AI.Entity.Entities;

namespace Revenue.AI.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;
        private readonly IDistributedCache myDistributedCache;
        private readonly string RedisKey = "Revenue.AI_Source_CachedTimeUTC_";

        public ProductController(ILogger<ProductController> logger, IProductService productService, 
            IDistributedCache distributedCache)
        {
            _logger = logger;
            _productService = productService;
            myDistributedCache = distributedCache;
        }

        [HttpGet("{id}")]
        public async Task<Product> Get(int id)
        {
            _logger.LogInformation($"Getting order id {id}");
            return await _productService.Get(id);
        }
        
        [HttpPost]
        public async Task<Product> Create([FromBody] Product product)
        {
            _logger.LogInformation($"Add product");
            var response = await _productService.Add(product);
            var jsonData = JsonSerializer.Serialize(product);
            await myDistributedCache.SetStringAsync(RedisKey + DateTime.Now.ToString("yyyyMMdd_hhmm"), jsonData);
            return response;
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            _logger.LogInformation($"Add product");
            await _productService.Remove(id);
        }
    }
}