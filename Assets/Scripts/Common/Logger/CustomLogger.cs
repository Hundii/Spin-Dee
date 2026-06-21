using System;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Common
{
    public class CustomLogger : MonoBehaviour, ILoadingSceneEntity
    {
        private static CustomLogger instance;
        [SerializeField] private LogFlags logFlags = LogFlags.All;
        [SerializeField] private LogGroupFlags debugGroupFlags = LogGroupFlags.All;
        [SerializeField] private LogGroupFlags warningGroupFlags = LogGroupFlags.All;
        [SerializeField] private LogGroupFlags errorGroupFlags = LogGroupFlags.All;

        private Action<UnityEngine.Object> _onReady;
        private Stopwatch stopwatch = new();

        void ILoadingSceneEntity.OnCreation(Action<UnityEngine.Object> onReady)
        {
            _onReady = onReady;
        }
        private void Awake()
        {
            instance = this;
            _onReady.Invoke(this);
        }

        private static bool HasFlag(LogFlags logFlag)
        {
            return (instance.logFlags & logFlag) != 0;
        }
        private static bool HasFlag(LogGroupFlags logGroupFlags1, LogGroupFlags logGroupFlag2)
        {
            return (logGroupFlags1 & logGroupFlag2) != 0;
        }
        public static void Log(string message, LogGroupFlags logGroupFlags = LogGroupFlags.Default, bool trackTime = false)
        {
            if (!HasFlag(LogFlags.Debug))
            {
                return;
            }
            if (!HasFlag(instance.debugGroupFlags, logGroupFlags))
            {
                return;
            }
            if (trackTime)
            {
                instance.stopwatch.Stop();
                message += " Elapsed time (ms): " + instance.stopwatch.ElapsedMilliseconds;
                instance.stopwatch.Restart();
            }
            Debug.Log(message);
        }
        public static void Log(string message, bool trackTime)
        {
            Log(message, LogGroupFlags.Default, trackTime);
        }

        public static void Log(object message)
        {
            Log(message.ToStringJson(), LogGroupFlags.Default, false);
        }

        public static void Log(object[] message)
        {
            Log(message.ToStringJson(), LogGroupFlags.Default, false);
        }

        public static void LogDebug(string message, LogGroupFlags logGroupFlags = LogGroupFlags.Default, bool trackTime = false)
        {
            if (!HasFlag(LogFlags.Debug))
            {
                return;
            }
            if (!HasFlag(instance.debugGroupFlags, logGroupFlags))
            {
                return;
            }
            if (trackTime)
            {
                instance.stopwatch.Stop();
                message += " Elapsed time (ms): " + instance.stopwatch.ElapsedMilliseconds;
                instance.stopwatch.Restart();
            }
            Debug.Log(message);
        }
        public static void LogDebug(string message, bool trackTime)
        {
            LogDebug(message, LogGroupFlags.Default, trackTime);
        }

        public static void LogError(string message, LogGroupFlags logGroupFlags = LogGroupFlags.Default, bool trackTime = false)
        {
            if (!HasFlag(LogFlags.Error))
            {
                return;
            }
            if (!HasFlag(instance.errorGroupFlags, logGroupFlags))
            {
                return;
            }
            if (trackTime)
            {
                instance.stopwatch.Stop();
                message += " Elapsed time (ms): " + instance.stopwatch.ElapsedMilliseconds;
                instance.stopwatch.Restart();
            }
            Debug.LogError(message);
        }
        public static void LogError(string message, bool trackTime)
        {
            LogError(message, LogGroupFlags.Default, trackTime);
        }

        public static void LogWarning(string message, LogGroupFlags logGroupFlags = LogGroupFlags.Default, bool trackTime = false)
        {
            if (!HasFlag(LogFlags.Warning))
            {
                return;
            }
            if (!HasFlag(instance.warningGroupFlags, logGroupFlags))
            {
                return;
            }
            if (trackTime)
            {
                instance.stopwatch.Stop();
                message += " Elapsed time (ms): " + instance.stopwatch.ElapsedMilliseconds;
                instance.stopwatch.Restart();
            }
            Debug.LogWarning(message);
        }
        public static void LogWarning(string message, bool trackTime)
        {
            LogWarning(message, LogGroupFlags.Default, trackTime);
        }

        public static void ResetTimer()
        {
            instance.stopwatch.Reset();
        }

    }

    [Flags]
    public enum LogFlags
    {
        None = 0,
        Debug = 1 << 0,
        Warning = 1 << 1,
        Error = 1 << 2,
        All = Debug | Warning | Error,
    }

    [Flags]
    public enum LogGroupFlags
    {
        None = 0,
        Default = 1 << 0,
        Playground = 1 << 1,
        Test = 1 << 2,
        SaveAndLoad = 1 << 3,
        UI = 1 << 4,
        All = Default | Playground | Test | SaveAndLoad | UI
    }
}
