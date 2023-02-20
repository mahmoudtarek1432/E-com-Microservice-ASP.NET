using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Cache.CacheManager;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureLogging((hostContext, loggingBuilder) =>
{
    loggingBuilder.AddConfiguration(hostContext.Configuration.GetSection("Logging"));
    loggingBuilder.AddConsole();
    loggingBuilder.AddDebug();
});

builder.Host.ConfigureAppConfiguration((hostcontext, config) =>
{
    config.AddJsonFile($"ocelot.{hostcontext.HostingEnvironment.EnvironmentName}.json");
});

builder.Services.AddOcelot().AddCacheManager(settings => settings.WithDictionaryHandle());

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
await app.UseOcelot();
app.Run();
