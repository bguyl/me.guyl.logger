namespace Guyl.GLogger
{
    using UnityEngine;
    using System;
    using Object = UnityEngine.Object;

    /// <summary>
    /// Use this class as an replacement of the Unity <c><see cref="UnityEngine.Debug"/></c> class to have access
    /// to the logger features
    /// </summary>
    public class GDebug : Debug
    {
        public static ILogHandler DefaultUnityLogHandler { get; set; }
        public static MainLogHandler MainLogHandler { get; set; }

        #region LogFormat
        [HideInCallstack]
        public new static void LogFormat(string format, params object[] args)
        {
            MainLogHandler.LogFormat(GLogType.Log, K.DEFAULT_CHAN, null, format, null, LogOption.None, args);
        }

        [HideInCallstack]
        public new static void LogFormat(Object context, string format, params object[] args)
        {
            MainLogHandler.LogFormat(GLogType.Log, K.DEFAULT_CHAN, context, format, null, LogOption.None, args);
        }

        [HideInCallstack]
        public new static void LogFormat(LogType logType, LogOption logOptions, Object context, string format, params object[] args)
        {
            MainLogHandler.LogFormat(Convert.ToGLogType(logType), K.DEFAULT_CHAN, context, format, null, logOptions, args);
        }
        
        [HideInCallstack]
        public static void LogFormat(GLogType logType, string channel, LogOption logOptions, Object context, string format, object caller, params object[] args)
        {
            MainLogHandler.LogFormat(logType, channel, context, format, caller, logOptions, args);
        }
        #endregion LogFormat

        #region LogWarning
        [HideInCallstack]
        public new static void LogWarning(object message, Object context = null)
        {
            MainLogHandler.LogFormat(GLogType.Warning, K.DEFAULT_CHAN, context, "{0}", null, LogOption.None, message);
        }

        [HideInCallstack]
        public new static void LogWarningFormat(string format, params object[] args)
        {
            MainLogHandler.LogFormat(GLogType.Warning, K.DEFAULT_CHAN, null, format, null, LogOption.None, args);
        }

        [HideInCallstack]
        public new static void LogWarningFormat(Object context, string format, params object[] args)
        {
            MainLogHandler.LogFormat(GLogType.Warning, K.DEFAULT_CHAN, context, format, null, LogOption.None, args);
        }
        
        [HideInCallstack]
        public static void LogWarning(string channel, object message, Object context = null, object caller = null, LogOption logOptions = LogOption.None)
        {
            MainLogHandler.LogFormat(GLogType.Warning, channel, context, "{0}", caller, logOptions, message);
        }
        #endregion LogWarning

        #region LogError
        [HideInCallstack]
        public new static void LogError(object message, Object context = null)
        {
            MainLogHandler.LogFormat(GLogType.Error, K.DEFAULT_CHAN, context, "{0}", null, LogOption.None, message);
        }

        [HideInCallstack]
        public new static void LogErrorFormat(string format, params object[] args)
        {
            MainLogHandler.LogFormat(GLogType.Error, K.DEFAULT_CHAN, null, format, null, LogOption.None, args);
        }

        [HideInCallstack]
        public new static void LogErrorFormat(Object context, string format, params object[] args)
        {
            MainLogHandler.LogFormat(GLogType.Error, K.DEFAULT_CHAN, context, format, null, LogOption.None, args);
        }

        [HideInCallstack]
        public static void LogError(string channel, object message, Object context = null, object caller = null, LogOption logOptions = LogOption.None)
        {
            MainLogHandler.LogFormat(GLogType.Error, channel, context, "{0}", caller, logOptions, message);
        }
        #endregion LogError

        #region LogException
        [HideInCallstack]
        public new static void LogException(Exception exception)
        {
            MainLogHandler.LogException(exception, null);
        }

        [HideInCallstack]
        public new static void LogException(Exception exception, Object context)
        {
            MainLogHandler.LogException(exception, context);
        }
        #endregion LogException

        #region LogAssertion
        [HideInCallstack]
        public static void LogAssert(object message, Object context = null)
        {
            MainLogHandler.LogFormat(LogType.Assert, context, "{0}", message);
        }
        #endregion

        #region LogVeryVerbose
        [HideInCallstack]
        public static void LogVeryVerbose(object message, Object context = null, object caller = null, LogOption logOptions = LogOption.NoStacktrace)
        {
            MainLogHandler.LogFormat(GLogType.VeryVerbose, K.DEFAULT_CHAN, context, "{0}", caller, logOptions, message);
        }
        
        [HideInCallstack]
        public static void LogVeryVerbose(string channel, object message, Object context = null, object caller = null, LogOption logOptions = LogOption.NoStacktrace)
        {
            MainLogHandler.LogFormat(GLogType.VeryVerbose, channel, context, "{0}", caller, logOptions, message);
        }
        #endregion LogVeryVerbose
        
        #region LogTrace
        [HideInCallstack]
        public static void LogTrace(object message, Object context = null, object caller = null, LogOption logOptions = LogOption.NoStacktrace)
        {
            MainLogHandler.LogFormat(GLogType.Trace, K.DEFAULT_CHAN, context, "{0}", caller, logOptions, message);
        }
        
        [HideInCallstack]
        public static void LogTrace(string channel, object message, Object context = null, object caller = null, LogOption logOptions = LogOption.NoStacktrace)
        {
            MainLogHandler.LogFormat(GLogType.Trace, channel, context, "{0}", caller, logOptions, message);
        }
        #endregion LogTrace

        #region LogDebug
        [HideInCallstack]
        public static void LogDebug(object message, Object context = null, object caller = null, LogOption logOptions = LogOption.None)
        {
            MainLogHandler.LogFormat(GLogType.Debug, K.DEFAULT_CHAN, context, "{0}", caller, logOptions, message);
        }
        
        [HideInCallstack]
        public static void LogDebug(string channel, object message, Object context = null, object caller = null, LogOption logOptions = LogOption.None)
        {
            MainLogHandler.LogFormat(GLogType.Debug, channel, context, "{0}", caller, logOptions, message);
        }
        #endregion LogDebug

        #region LogInfo
        [HideInCallstack]
        public static void LogInfo(object message, Object context = null, object caller = null, LogOption logOptions = LogOption.None)
        {
            MainLogHandler.LogFormat(GLogType.Information, K.DEFAULT_CHAN, context, "{0}", caller, logOptions, message);
        }
        
        [HideInCallstack]
        public static void LogInfo(string channel, object message, Object context = null, object caller = null, LogOption logOptions = LogOption.None)
        {
            MainLogHandler.LogFormat(GLogType.Information, channel, context, "{0}", caller, logOptions, message);
        }
        #endregion LogInfo

        #region LogCritical
        [HideInCallstack]
        public static void LogCritical(object message, Object context = null, object caller = null, LogOption logOptions = LogOption.None)
        {
            MainLogHandler.LogFormat(GLogType.Critical, K.DEFAULT_CHAN, context, "{0}", caller, logOptions, message);
        }

        [HideInCallstack]
        public static void LogCritical(string channel, object message, Object context = null, object caller = null, LogOption logOptions = LogOption.None)
        {
            MainLogHandler.LogFormat(GLogType.Critical, channel, context, "{0}", caller, logOptions, message);
        }
        #endregion LogCritical
    }
}