using System.CommandLine;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Ponder.Services;
using Ponder.Settings;

namespace Ponder
{
    internal class Application
    {
        private readonly ILogger<Application> _logger;
        private readonly IOptions<Librarian> _settings;
        private readonly IFileManager _fileManager;

        public Application(ILogger<Application> logger, IOptions<Librarian> settings, IFileManager fileManager)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
            _fileManager = fileManager ?? throw new ArgumentNullException(nameof(fileManager));
        }

        public async Task<int> RunAsync(string[] args)
        {
            var rootCommand = new RootCommand();
            
            var newDayOption = new Option<string>
                (aliases: new[] { "--new-day", "-nd" },
                description: "Create a new file for the day.",
                getDefaultValue: () => {
                    var currentDate = DateTime.Now;
                    return currentDate.ToString("dd-MM-yy");
                });

            rootCommand.AddOption(newDayOption);

            rootCommand.SetHandler((newDayOptionValue) =>
                {
                    Console.WriteLine("Hello from the handler!");
                    _logger.LogDebug("Creating new day: {0}", newDayOptionValue);
                    _fileManager.CreateNewDalie(newDayOptionValue);
                },
                newDayOption);

            return await rootCommand.InvokeAsync(args);
        }
    }
}
