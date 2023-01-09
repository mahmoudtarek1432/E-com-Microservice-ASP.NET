using Microsoft.Extensions.Logging;
using Npgsql;

namespace Discount.API.Extentions
{
    public static class HostExtentions
    {
        public static IHost MigrateDatabase<TContext>(this IHost host, int? retry=0)
        {
            int retryAvailability = retry.Value;

            using(var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var configuration = services.GetRequiredService<IConfiguration>(); //DI env configuration
                var Logger = services.GetRequiredService<ILogger<TContext>>();
                try
                {
                    Logger.LogInformation("migrating postgres database");

                    using (var connection = new NpgsqlConnection(configuration.GetValue<string>("DatabaseSettings:ConnectionString")))
                    {
                        connection.Open();

                        var command = new NpgsqlCommand
                        {
                            Connection = connection
                        };

                        command.CommandText = "DROP TABLE IF EXIST Coupon";
                        command.ExecuteNonQuery();
                        command.CommandText = @"CREATE TABLE Coupon(Id SERIAL PRIMARY KEY, 
                                                                ProductName VARCHAR(24) NOT NULL,
                                                                Description TEXT,
                                                                Amount INT)";
                        command.ExecuteNonQuery();

                        command.CommandText = "INSERT INTO Coupon(ProductName, Description, Amount) VALUES('IPhone X', 'IPhone Discount', 150);";
                        command.ExecuteNonQuery();

                        command.CommandText = "INSERT INTO Coupon(ProductName, Description, Amount) VALUES('Samsung 10', 'Samsung Discount', 100);";
                        command.ExecuteNonQuery();

                        Logger.LogInformation("Migrated postresql database.");
                    }
                }
                catch (NpgsqlException ex)
                {
                    Logger.LogError(ex, "An error occurred while migrating the postresql database");

                    if (retryAvailability < 50)
                    {
                        retryAvailability++;
                        System.Threading.Thread.Sleep(2000);
                        MigrateDatabase<TContext>(host, retryAvailability);
                    }
                }
            }

            return host;
        }
    }
}
