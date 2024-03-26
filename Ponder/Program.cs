using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Ponder;
using Ponder.Services;
using Ponder.Settings;

var services = new ServiceCollection();
var configuration = new ConfigurationBuilder()
    .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .Build();

services.AddLogging(builder =>
{
    builder.SetMinimumLevel(LogLevel.Information).AddConsole();
});
services.AddScoped<IFileManager, FileManager>();
services.AddSingleton<Application>();


services.Configure<Librarian>(configuration.GetSection("Librarian"));

var serviceProvider = services.BuildServiceProvider();

var application = serviceProvider.GetRequiredService<Application>();

await application.RunAsync(args);