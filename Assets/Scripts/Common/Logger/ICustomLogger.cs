using System;

namespace Common
{
    public interface ICustomLogger
    {
        public void Log(string message, LogGroupFlags logGroupFlags = LogGroupFlags.Default, bool trackTime = false);
        public void Log(string message, bool trackTime);
        public void LogDebug(string message, LogGroupFlags logGroupFlags = LogGroupFlags.Default, bool trackTime = false);
        public void LogDebug(string message, bool trackTime);

        public void LogWarning(string message, LogGroupFlags logGroupFlags = LogGroupFlags.Default, bool trackTime = false);
        public void LogWarning(string message, bool trackTime);
        public void LogError(string message, LogGroupFlags logGroupFlags = LogGroupFlags.Default, bool trackTime = false);
        public void LogError(string message, bool trackTime);
        public void LogError(Exception exception) => LogError(exception.Message);

        public void ResetTimer();

    }
}
