namespace Guyl.Logger
{
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// 
    /// </summary>
    public interface IGLogHandler : ILogHandler
    {
        public HashSet<string> MutedChannels { get; }
        public GLogTypeFlag AllowedLogTypes { get; set; }
        public List<IGLogFormatter> LogFormatters { get; }
        
        public bool LogEnabled { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logType"></param>
        /// <param name="channel"></param>
        /// <param name="context"></param>
        /// <param name="format"></param>
        /// <param name="caller"></param>
        /// <param name="logOptions"></param>
        /// <param name="args"></param>
        void LogFormat(GLogType logType, string channel, Object context, string format, object caller, LogOption logOptions, params object[] args);

        bool IsLogAllowed(string channel, GLogType logType)
        {
            return LogEnabled &&
                    AllowedLogTypes.HasFlag(Convert.ToGLogTypeFlag(logType)) &&
                    !MutedChannels.Contains(channel);
        }
    }
}