using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Ponder.Settings;

namespace Ponder.Services
{
    /// <summary>
    /// Provides methods for managing files and directories.
    /// </summary>
    public class FileManager : IFileManager
    {
        private readonly ILogger<FileManager> _logger;
        private readonly IOptions<Librarian> _settings;

        public FileManager(ILogger<FileManager> logger, IOptions<Librarian> settings)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        /// <inheritdoc />
        public void InitializeDirectories()
        {
            // create necessary folders if they don't already exist
            if (!Directory.Exists(_settings.Value.TemplateFolder))
                Directory.CreateDirectory(_settings.Value.TemplateFolder);
            if (!Directory.Exists(_settings.Value.DalieFolder))
                Directory.CreateDirectory(_settings.Value.DalieFolder);
            if (!File.Exists(_settings.Value.DefaultTemplatePath))
                File.Copy("Default.md", _settings.Value.DefaultTemplatePath);
            
            _logger.LogInformation("Directories initialized");
            _logger.LogInformation("Templates path: {0}", _settings.Value.TemplateFolder);
            _logger.LogInformation("Dalies path: {0}", _settings.Value.DalieFolder);
            _logger.LogInformation("Default template path: {0}", _settings.Value.DefaultTemplatePath);
        }

        /// <inheritdoc />
        public void CreateNewDalie(string dailieName)
        {
            InitializeDirectories();
            dailieName = ValidateDalieName(dailieName);
            
            var newDailiePath = Path.Combine(_settings.Value.DalieFolder, dailieName + ".md");
            
            // check if dailie already exists
            if (File.Exists(newDailiePath))
            {
                _logger.LogInformation("Dailie {DailieName} already exists", dailieName);
                return;
            }
            
            File.Copy(_settings.Value.DefaultTemplatePath, newDailiePath);
            InitializeNewDay(dailieName, newDailiePath);
            _logger.LogInformation("Created new dailie {DailieName}", dailieName);
        }
        
        /// <summary>
        /// Update the new dailie file with the current date along with other information in appropriate placeholders.
        /// </summary>
        /// <param name="dailieName">Dailie file name</param>
        /// <param name="dailiePath">Path to Dailie file</param>
        private void InitializeNewDay(string dailieName, string dailiePath)
        {
            var template = File.ReadAllText(dailiePath);
            template = template.Replace("<today>", dailieName);
            template = template.Replace("<last_modified>", DateTime.Now.ToString("dd-MM-yy HH:mm:ss"));
            File.WriteAllText(dailiePath, template);
        }
        
        private string ValidateDalieName(string dalieName)
        {
            if (Regex.IsMatch(dalieName, @"^\d{2}-\d{2}-\d{2}$"))
                return dalieName;
        
            if (dalieName.Trim() == "today")
                return DateTime.Now.ToString("dd-MM-yy");

            throw new ArgumentException("Invalid date format. Please use 'dd-MM-yy' or 'today'.");
        }
    }

    public interface IFileManager
    {
        /// <summary>
        /// Initializes the necessary directories for the application.
        /// </summary>
        void InitializeDirectories();
        
        /// <summary>
        /// Creates a new dailie file.
        /// <exception cref="IOException">Thrown when the file already exists.</exception>
        /// <exception cref="UnauthorizedAccessException">Thrown when the file cannot be created.</exception>
        /// <exception cref="ArgumentException">Thrown when the file name is invalid.</exception>
        /// </summary>
        void CreateNewDalie(string dailieName);
    }
}