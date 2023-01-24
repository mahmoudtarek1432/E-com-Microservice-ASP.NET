using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Ordering.API.Extensions
{
    public static class HostExtensions
    {
        public static IHost MigrateDB<TContext>(this IHost host,
            Action<TContext, IServiceProvider> seeder, int? retry = 0) where TContext : DbContext
        {
            int retryForAvailability = retry.Value;

            using(var scope = host.Services.CreateScope())
            {
                var services =  scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<TContext>>();
                var context = services.GetService<TContext>();

                try
                {
                    logger.LogInformation("Creating sql servier migration for order context");

                    context.Database.Migrate();
                    seeder(context,services);

                    logger.LogInformation("Migrating database for order context complete");

                }
                catch(SqlException ex)
                {
                    logger.LogInformation($"database migration failed, an error has occured while migration db, message: {ex.Message}");
                    if(retryForAvailability <= 50)
                    {
                        retryForAvailability++;
                        System.Threading.Thread.Sleep(2000);
                        MigrateDB(host, seeder, retryForAvailability); 
                    }
                }
            }
            return host;
        }
    }
}
