namespace LoggingServices
{
    public interface ILoggingService
    {
        Task LogAsync(string message);
    }

}