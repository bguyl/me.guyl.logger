namespace Guyl.Logger
{
    using UnityEngine;
    using System;
    using Object = UnityEngine.Object;

    public class GDebug : Debug
    {
        public static ILogHandler DefaultUnityLogHandler { get; set; }
        public static IGLogHandler GLogHandler { get; set; }

        #region LogFormat
        [HideInCallstack]
        public new static void LogFormat(string format, params object[] args)
        {
            GLogHandler.LogFormat(GLogType.Log, K.DefaultChan, null, format, null, LogOption.None, args);
        }

        [HideInCallstack]
        public new static void LogFormat(Object context, string format, params object[] args)
        {
            GLogHandler.LogFormat(GLogType.Log, K.DefaultChan, context, format, null, LogOption.None, args);
        }

        [HideInCallstack]
        public new static void LogFormat(LogType logType, LogOption logOptions, Object context, string format, params object[] args)
        {
            GLogHandler.LogFormat(Convert.ToGLogType(logType), K.DefaultChan, context, format, null, logOptions, args);
        }
        
        [HideInCallstack]
        public static void LogFormat(GLogType logType, string channel, LogOption logOptions, Object context, string format, object caller, params object[] args)
        {
            GLogHandler.LogFormat(logType, channel, context, format, caller, logOptions, args);
        }
        #endregion LogFormat

        #region LogWarning
        [HideInCallstack]
        public static void LogWarn(object message)
        {
            GLogHandler.LogFormat(GLogType.Warning, K.DefaultChan, null, "{0}", null, LogOption.None, message);
        }

        [HideInCallstack]
        public static void LogWarn(object message, Object context)
        {
            GLogHandler.LogFormat(GLogType.Warning, K.DefaultChan, context, "{0}", null, LogOption.None, message);
        }

        [HideInCallstack]
        public static void LogWarnFormat(string format, params object[] args)
        {
            GLogHandler.LogFormat(GLogType.Warning, K.DefaultChan, null, format, null, LogOption.None, args);
        }

        [HideInCallstack]
        public static void LogWarnFormat(Object context, string format, params object[] args)
        {
            GLogHandler.LogFormat(GLogType.Warning, K.DefaultChan, context, format, null, LogOption.None, args);
        }
        
        [HideInCallstack]
        public static void LogWarn(string channel, object message, Object context = null, object caller = null, LogOption logOptions = LogOption.None)
        {
            GLogHandler.LogFormat(GLogType.Warning, channel, context, "{0}", caller, logOptions, message);
        }
        #endregion LogWarning

        #region LogError
        [HideInCallstack]
        public static void LogErr(object message)
        {
            GLogHandler.LogFormat(GLogType.Error, K.DefaultChan, null, "{0}", null, LogOption.None, message);
        }

        [HideInCallstack]
        public static void LogErr(object message, Object context)
        {
            GLogHandler.LogFormat(GLogType.Error, K.DefaultChan, context, "{0}", null, LogOption.None, message);
        }

        [HideInCallstack]
        public static void LogErrFormat(string format, params object[] args)
        {
            GLogHandler.LogFormat(GLogType.Error, K.DefaultChan, null, format, null, LogOption.None, args);
        }

        [HideInCallstack]
        public static void LogErrFormat(Object context, string format, params object[] args)
        {
            GLogHandler.LogFormat(GLogType.Error, K.DefaultChan, context, format, null, LogOption.None, args);
        }

        [HideInCallstack]
        public static void LogErr(string channel, object message, Object context = null, object caller = null, LogOption logOptions = LogOption.None)
        {
            GLogHandler.LogFormat(GLogType.Error, channel, context, "{0}", caller, logOptions, message);
        }
        #endregion LogError

        #region LogException
        [HideInCallstack]
        public new static void LogException(Exception exception)
        {
            GLogHandler.LogException(exception, null);   
        }

        [HideInCallstack]
        public new static void LogException(Exception exception, Object context)
        {
            GLogHandler.LogException(exception, context);
        }
        #endregion LogException

        #region LogAssertion
        [HideInCallstack]
        public static void LogAssert(object message)
        {
            GLogHandler.LogFormat(LogType.Assert, null, "{0}", message);
        }
        #endregion

        #region LogVeryVerbose
        [HideInCallstack]
        public static void LogVeryVerbose(object message, Object context = null, object caller = null, LogOption logOptions = LogOption.NoStacktrace)
        {
            GLogHandler.LogFormat(GLogType.VeryVerbose, K.DefaultChan, context, "{0}", caller, logOptions, message);
        }
        
        [HideInCallstack]
        public static void LogVeryVerbose(string channel, object message, Object context = null, object caller = null, LogOption logOptions = LogOption.NoStacktrace)
        {
            GLogHandler.LogFormat(GLogType.VeryVerbose, channel, context, "{0}", caller, logOptions, message);
        }
        #endregion LogVeryVerbose
        
        #region LogTrace
        [HideInCallstack]
        public static void LogTrace(object message, Object context = null, object caller = null, LogOption logOptions = LogOption.NoStacktrace)
        {
            GLogHandler.LogFormat(GLogType.Trace, K.DefaultChan, context, "{0}", caller, logOptions, message);
        }
        
        [HideInCallstack]
        public static void LogTrace(string channel, object message, Object context = null, object caller = null, LogOption logOptions = LogOption.NoStacktrace)
        {
            GLogHandler.LogFormat(GLogType.Trace, channel, context, "{0}", caller, logOptions, message);
        }
        #endregion LogTrace

        #region LogDebug
        [HideInCallstack]
        public static void LogDebug(object message, Object context = null, object caller = null, LogOption logOptions = LogOption.None)
        {
            GLogHandler.LogFormat(GLogType.Debug, K.DefaultChan, context, "{0}", caller, logOptions, message);
        }
        
        [HideInCallstack]
        public static void LogDebug(string channel, object message, Object context = null, object caller = null, LogOption logOptions = LogOption.None)
        {
            GLogHandler.LogFormat(GLogType.Debug, channel, context, "{0}", caller, logOptions, message);
        }
        #endregion LogDebug

        #region LogInfo
        [HideInCallstack]
        public static void LogInfo(object message, Object context = null, object caller = null, LogOption logOptions = LogOption.None)
        {
            GLogHandler.LogFormat(GLogType.Information, K.DefaultChan, context, "{0}", caller, logOptions, message);
        }
        
        [HideInCallstack]
        public static void LogInfo(string channel, object message, Object context = null, object caller = null, LogOption logOptions = LogOption.None)
        {
            GLogHandler.LogFormat(GLogType.Information, channel, context, "{0}", caller, logOptions, message);
        }
        #endregion LogInfo

        #region LogCritical
        [HideInCallstack]
        public static void LogCritical(object message, Object context = null, object caller = null, LogOption logOptions = LogOption.None)
        {
            GLogHandler.LogFormat(GLogType.Critical, K.DefaultChan, context, "{0}", caller, logOptions, message);
        }
        
        [HideInCallstack]
        public static void LogCritical(string channel, object message, Object context = null, object caller = null, LogOption logOptions = LogOption.None)
        {
            GLogHandler.LogFormat(GLogType.Critical, channel, context, "{0}", caller, logOptions, message);
        }
        #endregion LogCritical
    }
}