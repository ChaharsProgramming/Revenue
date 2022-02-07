
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Revenue.AI.Adapter;
using Revenue.AI.Infrastructure;
using Revenue.AI.Middleware.Common;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Revenue.AI.Import.Webjob
{
    public class ImportWebJob
    {
        private IConfiguration myConfiguration;
        private readonly IMessageReceiver myMessageReceiver;

        public ImportWebJob(IConfiguration configuration, IMessageReceiver messageReceiver)
        {
            myConfiguration = configuration;
            myMessageReceiver = messageReceiver;
        }

        private const string WebJobName = "ImportOrder";
        public void Host(string[] args)
        {
            HostWebJob(args);
        }

        private void HostWebJob(string[] args)
        {
            var host = Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                services.AddScoped<IProductOrderAdapter, ProductOrderAdapter>();
                services.AddScoped<IImportProcessor, ImportProcessor>();
                var provider = services.BuildServiceProvider();
                ProcessCheckAndBackup(services, provider);

            }).Build();

            using (host)
            {
                host.Run();
            }
        }

        public void ProcessCheckAndBackup(IServiceCollection services, ServiceProvider provider)
        {
            var test = new ImportProcessor();
        }
    }
}
