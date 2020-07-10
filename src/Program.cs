using Microsoft.Extensions.DependencyInjection;
using ReportGenerator.Interfaces;
using ReportGenerator.Services;

namespace ReportGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create service collection and configure our services
            var services = ConfigureServices();
            // Generate a provider
            var serviceProvider = services.BuildServiceProvider();

            // Kick off our actual code
            serviceProvider.GetService<ConsoleApp>().Run();
        }

        private static IServiceCollection ConfigureServices() 
        {
            IServiceCollection services = new ServiceCollection();
            services.AddTransient<ConsoleApp>();
            services.AddSingleton<IDataReaderService, DataReaderService>();
            services.AddSingleton<IQuoteHandlerService, QuoteHandlerService>();
            services.AddSingleton<IQuoteRewriterService, QuoteRewriterService>();
            return services;
        }
    }
}
