using System;
using Object = UnityEngine.Object;

namespace Guyl.Logger
{
    public class UnityEditorLogFormatter : IGLogFormatter
    {
        public string Format(GLogType logType, string channel, Object context, string format, object caller, params object[] args)
        {
            string colorTagFormat = null;

            switch (logType)
            {
                case GLogType.Information:
                    colorTagFormat = "<color=blue>";
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
            // message = GetString(format);
            message = $"{logType.ToString().ToUpperInvariant()} [{channel}]{contextCallerText} - {message}" ;
            if (colorTagFormat != null)
            {
                message = colorTagFormat + message + "</color>";
            }
			
            return message;
        }
    }
}