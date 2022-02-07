using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Revenue.AI.Entity.Entities;
using Revenue.AI.Entity.Entities.Orders;
using Revenue.AI.Infrastructure.SeedWorks;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace Revenue.AI.Import.Webjob
{
    public class ImportProcessor : IImportProcessor
    {
        private readonly IRepository<Order> myOrderRepository;
        private readonly IRepository<Product> myProductRepository;
        private readonly IDistributedCache myDistributedCache;
        private readonly IConfiguration myConfiguration;

        public ImportProcessor(IRepository<Order> OrderRepository, IRepository<Product> ProductRepository,
            IDistributedCache distributedCache, IConfiguration configuration)
        {
            myOrderRepository = OrderRepository;
            myProductRepository = ProductRepository;
            myDistributedCache = distributedCache;
            myConfiguration = configuration;
        }

        public void Process()
        {
            //Validate source--using webhook- cosmos bd with applicationname registration, need to do

            DateTime dt;
            var getRedisKeyValue = myDistributedCache.GetStringAsync("").Result;
            var success = DateTime.TryParseExact(getRedisKeyValue, "yyyyMMdd_hhmm", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);

            if (success)
            {
                var getLatestProduct = myProductRepository.GetAsync(dt);
                var getLatestOrder = myOrderRepository.GetAsync(dt);

                if(getLatestOrder!=null || getLatestProduct!=null)
                {
                    //backup db
                    Task.Run(() => BackupAsync(""));
                }
            }
        }

        public void BackupAsync(string strDestination)
        {
            using (var location = new SqliteConnection(myConfiguration["SourceConnectionString"]))
            using (var destination = new SqliteConnection(string.Format(myConfiguration["SourceConnectionString"], strDestination)))
            {
                location.Open();
                destination.Open();
                location.BackupDatabase(destination,"","");
            }
        }
    }
}
