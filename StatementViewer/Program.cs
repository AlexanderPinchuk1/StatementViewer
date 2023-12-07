using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StatementViewer.Foundation;
using StatementViewer.Foundation.Interfaces;
using StatementViewer.Repositories;

namespace StatementViewer
{
    internal static class Program
    {
        public static IServiceProvider? ServiceProvider { get; private set; }



        [STAThread]
        static void Main()
        {
            var host = CreateHostBuilder().Build();
            ServiceProvider = host.Services;
            Application.Run(ServiceProvider.GetRequiredService<MenuForm>());
        }

        static IHostBuilder CreateHostBuilder()
        {
            var builder = new ConfigurationBuilder();
            var config = builder.Build();

            return Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddDbContext<StatementViewerDbContext>(options =>
                    {
                        options.UseSqlServer(System.Configuration
                            .ConfigurationManager
                            .ConnectionStrings["DefaultConnection"]
                            .ConnectionString);
                        options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                    }, ServiceLifetime.Transient);

                    services.AddTransient<MenuForm>();
                    services.AddTransient<IDatabaseService, DatabaseService>();
                    services.AddTransient<IStatementParserService, StatementParserService>();
                });
        }
    }
}