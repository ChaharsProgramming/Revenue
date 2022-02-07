using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace Revenue.AI.Import.Webjob
{
    class Program
    {
        static async Task Main()
        {
            
            var builder = new HostBuilder();
            builder.ConfigureWebJobs(b =>
            {
                b.AddAzureStorageCoreServices();
            });
            var host = builder.Build();
            using (host)
            {
                await host.RunAsync();
            }
        }
        private static void Main(string[] args)
        {
            var importWebjob = new ImportWebJob();
            importWebjob.Host(args);
        }
    }
}
