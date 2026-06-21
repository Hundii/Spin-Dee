using UnityEngine;

namespace Common
{
    public class PrimitiveLogger : ICustomLogger
    {
        public void Log(string message, LogGroupFlags logGroupFlags = LogGroupFlags.Default, bool trackTime = false)
        {
            Debug.Log(message);
        }

        public void Log(string message, bool trackTime)
        {
            Debug.Log(message);
        }

        public void LogDebug(string message, LogGroupFlags logGroupFlags = LogGroupFlags.Default, bool trackTime = false)
        {
            Debug.Log(message);
        }

        public void LogDebug(string message, bool trackTime)
        {
            Debug.Log(message);
        }

        public void LogError(string message, LogGroupFlags logGroupFlags = LogGroupFlags.Default, bool trackTime = false)
        {
            Debug.LogError(message);
        }

        public void LogError(string message, bool trackTime)
        {
            Debug.LogError(message);
        }

        public void LogWarning(string message, LogGroupFlags logGroupFlags = LogGroupFlags.Default, bool trackTime = false)
        {
            Debug.LogWarning(message);
        }

        public void LogWarning(string message, bool trackTime)
        {
            Debug.LogWarning(message);
        }

        public void ResetTimer()
        {

        }
    }
}
