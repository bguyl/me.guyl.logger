namespace Guyl.GLogger
{
    /// <summary>
    /// A GLogFormatter allows customization of log display
    /// </summary>
    /// <example>
    /// For example, Unity can handle rich text, so we might want to apply some color with &lt;color=green&gt; tags
    /// For CI tools, you might need to use ANSI colors instead, like \e[0;32m
    /// </example>
    /// <remarks>
    /// You can chain several GLogFormatters if you have a complex environment
    /// </remarks>
    public interface IGLogFormatter
    {
        /// <summary>
        /// Formats the log message based on the given parameters.
        /// </summary>
        /// <param name="logType">The type of log message</param>
        /// <param name="channel">The channel of the log message</param>
        /// <param name="context">The context of the log message</param>
        /// <param name="format">The format string for the log message</param>
        /// <param name="caller">The object that called the log message</param>
        /// <param name="args">Additional arguments to be formatted into the log message</param>
        /// <returns>The formatted log message for the current configuration</returns>
        public string Format( GLogType logType,
            string channel,
            UnityEngine.Object context,
            string format,
            object caller,
            params object[] args
        );
    }
}