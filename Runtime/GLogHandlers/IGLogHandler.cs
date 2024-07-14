using System.Text.RegularExpressions;

namespace Guyl.GLogger
{
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// A GLogHandler is responsible to determine if the log should be displayed or not and applying a given formatting.
    /// </summary>
    public interface IGLogHandler : ILogHandler
    {
        public string[] ChannelsIncludeFilters { get; }
        public string[] ChannelsExcludeFilters { get; }
        public GLogTypeFlag AllowedLogTypes { get; set; }
        public List<IGLogFormatter> LogFormatters { get; }
        
        public bool LogEnabled { get; set; }

        /// <summary>
        /// Formats and writes a log message with the specified log type, log channel, logging context, format string, caller object, log options, and additional arguments
        /// </summary>
        /// <param name="logType">The logging severity level</param>
        /// <param name="channel">The log channel</param>
        /// <param name="context">The Unity Object the logging is coming from</param>
        /// <param name="format">The format string for the log message, as used by String.Format</param>
        /// <param name="caller">The method that is calling the log method</param>
        /// <param name="logOptions">The log options</param>
        /// <param name="args">The additional arguments used to format the log message</param>
        void LogFormat( GLogType logType, string channel, Object context, string format, object caller,
            LogOption logOptions, params object[] args );

        bool IsLogAllowed( string channel, GLogType logType )
        {
            bool isChannelIncluded = false;
            bool isChannelExcluded = false;

            for ( int i = 0; i < ChannelsIncludeFilters.Length; i++)
            {
                string includeFilter = ChannelsIncludeFilters[i];
                Regex includeRegex = new Regex(
                    Regex.Escape(
                    includeFilter
                        .Replace("*", ".*")
                        .Replace("?", ".")
                        .Replace("#", "\\d")
                    )
                );

                if (includeRegex.IsMatch(channel))
                {
                    isChannelIncluded = true;
                    break;
                }
            }

            for (int i = 0; i < ChannelsExcludeFilters.Length; i++)
            {
                string excludeFilter = ChannelsExcludeFilters[i];
                Regex excludeRegex = new Regex(
                    Regex.Escape(
                        excludeFilter
                            .Replace("*", ".*")
                            .Replace("?", ".")
                            .Replace("#", "\\d")
                    )
                );

                if (excludeRegex.IsMatch(channel))
                {
                    isChannelExcluded = true;
                    break;
                }
            }
            
            return LogEnabled &&
                   AllowedLogTypes.HasFlag(Convert.ToGLogTypeFlag(logType)) &&
                   isChannelIncluded &&
                   !isChannelExcluded;
        }
    }
}