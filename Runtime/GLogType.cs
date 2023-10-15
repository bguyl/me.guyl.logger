using System;
using UnityEngine;

namespace Guyl.Logger
{
    /// <summary>
    /// Logging severity levels.
    /// </summary>
    public enum GLogType
    {
        // Values compatible with Unity LogType
        Error = 0,
        Assert = 1,
        Warning = 2,
        Log = 3,
        Exception = 4,
        
        // New values
        Information = 3, // Information is equivalent to Log
        Critical = 5,
        Debug = 6,
        Trace = 7,
        VeryVerbose = 8
    }

    [Flags]
    public enum GLogTypeFlag
    {
        None = 0,
        Error = 1 << 0,
        Assert = 1 << 1,
        Warning = 1 << 2,
        Log = 1 << 3,
        Information = 1 << 3, // Information is equivalent to Log
        Exception = 1 << 4,
        Critical = 1 << 5,
        Debug = 1 << 6,
        Trace = 1 << 7,
        VeryVerbose = 1 << 8,
        All = ~0
    }
    
    public static class Convert {
        public static LogType ToLogType(GLogType gLogType)
        {
            if (Enum.IsDefined(typeof(LogType), gLogType)) return (LogType)gLogType;
            return LogType.Log;
        }

        public static GLogType ToGLogType(LogType logType) => (GLogType)logType;
    }
}