namespace Ponder.Services
{
    internal interface IRandomService
    {
        /// <summary>
        /// Get the next random number.
        /// </summary>
        int Next();
    }
}