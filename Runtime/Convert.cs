namespace Guyl.Logger
{
    using System;
    using UnityEngine;

    public static class Convert
    {
        /// <summary>
        /// Converts a GLogType to an Unity LogType
        /// </summary>
        /// <param name="gLogType">The <c><see cref="Guyl.Logger.GLogType"/></c> to convert</param>
        /// <returns>The corresponding <c><see cref="UnityEngine.LogType"/></c></returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the GLogType conversion is not handled yet</exception>
        public static LogType ToLogType( GLogType gLogType )
        {
            switch ( gLogType )
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

        /// <summary>
        /// Converts a LogType to GLogType
        /// </summary>
        /// <remarks>
        /// While a simple cast should be enough, use this method in case of future changes
        /// </remarks>
        /// <param name="logType">The <c><see cref="UnityEngine.LogType"/></c> to convert</param>
        /// <returns>The corresponding <c><see cref="Guyl.Logger.GLogType"/></c></returns>
        public static GLogType ToGLogType( LogType logType ) => ( GLogType )logType;

        public static GLogTypeFlag ToGLogTypeFlag(GLogType logType)
        {
            int val = 1 << (int)logType;
            return (GLogTypeFlag) val;
        }
    }
}