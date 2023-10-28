namespace Guyl.Logger
{
    using System;
    using UnityEngine;

    public static class Convert
    {
        public static LogType ToLogType(GLogType gLogType)
        {
            switch (gLogType)
            {
                case GLogType.Error:
                case GLogType.Critical:
                    return LogType.Error;
                case GLogType.Assert:
                    return LogType.Assert;
                case GLogType.Exception:
                    return LogType.Exception;
                case GLogType.Warning:
                    return LogType.Warning;
                case GLogType.Information:
                case GLogType.Debug:
                case GLogType.Trace:
                case GLogType.VeryVerbose:
                    return LogType.Log;
                default:
                    throw new ArgumentOutOfRangeException(nameof(gLogType), gLogType, null);
            }
        }

        public static GLogType ToGLogType(LogType logType) => (GLogType)logType;

        public static GLogTypeFlag ToGLogTypeFlag(GLogType logType)
        {
            int val = 1 << (int)logType;
            return (GLogTypeFlag) val;
        }
    }
}