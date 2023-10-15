using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Guyl.Logger
{
    using UnityEngine;

    public class GDebug : Debug
    {
        public static ILogHandler DefaultUnityLogHandler { get; set; } = Debug.unityLogger.logHandler;
        public static IGLogHandler GLogHandler { get; set; }

        #region Log
        public new static void Log(object message)
        {
            GLogHandler.LogFormat(GLogType.Log, K.DefaultChan, null, "{0}", null, LogOption.None, message);
        }

        public new static void Log(object message, Object context)
        {
            GLogHandler.LogFormat(GLogType.Log, K.DefaultChan, context, "{0}", null, LogOption.None, message);
        }
        
        public static void Log(string channel, object message, Object context = null, object caller = null)
        {
            GLogHandler.LogFormat(GLogType.Log, channel, context, "{0}", caller, LogOption.None, message);
        }
        #endregion Log

        #region LogFormat

        public new static void LogFormat(string format, params object[] args)
        {
            GLogHandler.LogFormat(GLogType.Log, K.DefaultChan, null, format, null, LogOption.None, args);
        }

        public new static void LogFormat(Object context, string format, params object[] args)
        {
            GLogHandler.LogFormat(GLogType.Log, K.DefaultChan, context, format, null, LogOption.None, args);
        }

        public new static void LogFormat(LogType logType, LogOption logOptions, Object context, string format, params object[] args)
        {
            GLogHandler.LogFormat(Convert.ToGLogType(logType), K.DefaultChan, context, format, null, logOptions, args);
        }
        
        public new static void LogFormat(GLogType logType, string channel, LogOption logOptions, Object context, string format, object caller, params object[] args)
        {
            GLogHandler.LogFormat(logType, channel, context, format, caller, logOptions, args);
        }
        #endregion LogFormat

        #region LogWarning
        public new static void LogWarning(object message)
        {
            GLogHandler.LogFormat(GLogType.Log, K.DefaultChan, null, "{0}", null, LogOption.None, message);
        }

        public new static void LogWarning(object message, Object context)
        {
            GLogHandler.LogFormat(GLogType.Log, K.DefaultChan, context, "{0}", null, LogOption.None, message);
        }
        
        public static void LogWarning(string channel, object message, Object context = null, object caller = null)
        {
            GLogHandler.LogFormat(GLogType.Log, channel, context, "{0}", caller, LogOption.None, message);
        }
        #endregion
        
        public static void LogTrace(string channel, object message, object sender)
        {
            
        }
    }
}