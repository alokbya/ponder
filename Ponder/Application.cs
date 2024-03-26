using System.CommandLine;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Ponder.Options;
using Ponder.Services;
using Ponder.Settings;

namespace Ponder
{
    internal class Application
    {
        private readonly ILogger<Application> _logger;
        private readonly IOptions<Librarian> _settings;

        public Application(ILogger<Application> logger, IRandomService randomService, IOptions<Librarian> settings)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public async Task<int> RunAsync(string[] args)
        {
            var rootCommand = new RootCommand();
            
            var newDayOption = new Option<string>
                (aliases: new string[] { "--new-day", "-nd" },
                description: "Create a new file for the day.",
                getDefaultValue: () => {
                    var currentDate = DateTime.Now;
                    return currentDate.ToString("dd-MM-yy");
                });

            rootCommand.AddOption(newDayOption);

            rootCommand.SetHandler((newDayOptionValue) =>
                {
                    Console.WriteLine("Hello from the handler!");
                    _logger.LogInformation("Creating new day: {0}", newDayOptionValue);
                    Dalie.CreateNewDay(newDayOptionValue, _settings);
                },
                newDayOption);

            return await rootCommand.InvokeAsync(args);
        }
    }
}
