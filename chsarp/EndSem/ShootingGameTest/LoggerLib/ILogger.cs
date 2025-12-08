namespace LoggerLib
{
    public enum LogLevel { Info, Warning, Error, Action }

    public interface ILogger
    {
        void WriteLog(LogLevel level, string message);
    }
}