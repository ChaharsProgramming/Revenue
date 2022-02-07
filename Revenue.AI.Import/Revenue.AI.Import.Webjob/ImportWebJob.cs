
using Microsoft.Azure.ServiceBus.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Revenue.AI.Adapter;


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
            //todo call ImportProcessor.Process()
        }
    }
}
