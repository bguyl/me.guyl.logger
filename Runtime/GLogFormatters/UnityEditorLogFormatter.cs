using System;
using System.Globalization;
using Object = UnityEngine.Object;

namespace Guyl.Logger
{
    /// <summary>
    /// Example on how to format log messages for Unity Editor console
    /// </summary>
    /// <remarks>
    /// This give you an example on how we could use the different information given and apply rich text to our logs
    /// </remarks>
    public class UnityEditorLogFormatter : IGLogFormatter
    {
        /// <summary>
        /// Formats the log message based on the specified parameters.
        /// </summary>
        /// <param name="logType">The type of the log message</param>
        /// <param name="channel">The channel of the log message</param>
        /// <param name="context">The Unity Object context associated with the log message</param>
        /// <param name="format">The format string of the log message</param>
        /// <param name="caller">The caller object associated with the log message</param>
        /// <param name="args">Additional arguments to format the log message</param>
        /// <returns>The formatted log message</returns>
        public string Format( GLogType logType, string channel, Object context, string format, object caller,
            params object[] args )
        {
            string colorTagFormat = null;

            switch ( logType )
            {
                case GLogType.Information:
                    colorTagFormat = "<color=green>";
                    break;
                case GLogType.Critical:
                    colorTagFormat = "<color=red>";
                    break;
                case GLogType.Debug:
                    colorTagFormat = "<color=green>"; 
                    break;
                case GLogType.Trace:
                    colorTagFormat = "<color=cyan>";
                    break;
            }

            string callerText = caller != null ? $" {caller.GetType()}" : "";
            string contextText = context != null ? $" {context.name}" : "";

            string contextCallerText = "";
            if (!String.IsNullOrEmpty(callerText))
            {
                contextCallerText = callerText;
                if (!String.IsNullOrEmpty(contextText))
                {
                    contextCallerText += "/" + contextText;
                }
            }
            else
            {
                contextCallerText = contextText;
            }

            string message = String.Format(format, args);
            
            // TODO_BG: Have a better understating on why this is needed in the Unity code
            // message = GetString(format);

            message = $"{logType.ToString().ToUpperInvariant().Substring(0,5)} [{channel}]{contextCallerText} - {message}" ;
            if (colorTagFormat != null)
            {
                message = colorTagFormat + message + "</color>";
            }
			
            return message;
        }

        /// <summary>
        /// Converts an object to its string representation using format provided by IFormattable.
        /// </summary>
        /// <param name="message">The object to convert to string.</param>
        /// <remarks>This mimic the default UnityEngine behavior</remarks>
        /// <returns>The string representation of the object.</returns>
        protected static string GetString( object message )
        {
            if ( message == null ) return "Null";
            return message is IFormattable formattable ? formattable.ToString((string) null, (IFormatProvider) CultureInfo.InvariantCulture) : message.ToString();
        }
    }
}