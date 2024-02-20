using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Guyl.Logger
{
    /// <summary>
    /// The JSONFormatter class provides a method to format log messages in JSON format
    /// </summary>
    /// <remarks>
    /// This can be useful for handling logs in database for example
    /// </remarks>
    public class JSONFormatter : IGLogFormatter
    {
        [Serializable]
        public class JSONLogEntry
        {
            [SerializeField] public GLogType logType;
            [SerializeField] public string channel;
            [SerializeField] public string context;
            [SerializeField] public string callerType;
            [SerializeField] public string message;
            [SerializeField] public DateTime timestamp;
        }
        
        public string Format(GLogType logType, string channel, Object context, string format, object caller, params object[] args)
        {
            string message = String.Format(format, args);

            JSONLogEntry logEntry = new JSONLogEntry()
            {
                logType = logType,
                channel = channel,
                context = context != null ? context.name : "",
                callerType = caller != null ? caller.GetType().ToString() : "",
                message = message,
                timestamp = DateTime.UtcNow
            };

            return JsonUtility.ToJson(logEntry);
        }
    }
}