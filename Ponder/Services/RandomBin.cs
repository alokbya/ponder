using Microsoft.Extensions.Logging;

namespace Ponder.Services
{
    internal class RandomBin : IRandomService
    {
        private ILogger _logger;

        public RandomBin(ILogger<RandomBin> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc />
        public int Next()
        {
            // create a random number generator
            var random = new Random();
            
            // log the next random number
            _logger.LogInformation("Next random number: {0}", random.Next());
            
            // get the next random number
            return random.Next();
        }
    }
}
