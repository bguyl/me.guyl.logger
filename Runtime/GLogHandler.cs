namespace Guyl.Logger
{
	using System;
	using System.Collections.Generic;
	using UnityEngine;
	using System.Globalization;
	using JetBrains.Annotations;

	public class GLogHandler : IGLogHandler
	{
		#region Fields
		private readonly List<IGLogHandler> _logHandlers = new List<IGLogHandler>();
		#endregion

		#region Properties
		private IGLogHandler ThisInterface => this;

		[UsedImplicitly] public bool UseUnityEngineLogger { get; set; } = true;
		[UsedImplicitly] public bool UseUnityEditorLogger { get; set; } = true;
		public List<string> AllowedChannels { get; }
		public GLogTypeFlag AllowedLogTypes { get; set; }
		public bool LogEnabled { get; set; }
		#endregion Properties

		#region Constructors
		public GLogHandler(List<string> allowedChannels = null)
		{
			AllowedChannels = allowedChannels ?? new List<string>();
		}
		#endregion Constructors
		
		#region Log handlers
		public void Register(IGLogHandler logHandler)
		{
			_logHandlers.Add(logHandler);
		}

		public void Unregister(IGLogHandler logHandler)
		{
			_logHandlers.Remove(logHandler);
		}
		#endregion Log handlers

		#region Handle logs
		public void LogFormat(GLogType logType, string channel, UnityEngine.Object context, string format, object caller, LogOption logOption = LogOption.None, params object[] args)
		{
			// TODO: Handle LogOptions
			// Debug.LogFormat will only work if Debug.unityLogger.logHandler is not swapped
#if UNITY_EDITOR
			if (UseUnityEditorLogger && ThisInterface.IsLogAllowed(channel, logType))
			{
				GDebug.DefaultUnityLogHandler.LogFormat(Convert.ToLogType(logType), context, format, logOption, args);			
			}
#else
			IsLogAllowed("", logType);
			if (UseUnityEngineLogger && ThisInterface.IsLogAllowed(channel, logType))
			{
				GDebug.DefaultUnityLogHandler.LogFormat(Convert.ToLogType(logType), context, format, args);
			}
#endif

			for (var i = 0; i < _logHandlers.Count; i++)
			{
				IGLogHandler logHandler = _logHandlers[i];
				if (logHandler.IsLogAllowed(channel, logType))
				{
					logHandler.LogFormat(logType, channel, context, format, caller, logOption, args);					
				}
			}
		}

		public void LogFormat(LogType logType, UnityEngine.Object context, string format, params object[] args)
		{
#if UNITY_EDITOR
			if (UseUnityEditorLogger && ThisInterface.IsLogAllowed(K.DefaultChan, Convert.ToGLogType(logType)))
			{
				GDebug.DefaultUnityLogHandler.LogFormat(logType, context, format, args);
			}
#else
			if (UseUnityEngineLogger && ThisInterface.IsLogAllowed(K.DefaultChan, Convert.ToGLogType(logType)))
			{
				GDebug.DefaultUnityLogHandler.LogFormat(logType, context, format, args);
			}
#endif
			
			for (var i = 0; i < _logHandlers.Count; i++)
			{
				IGLogHandler logHandler = _logHandlers[i];
				if (logHandler.IsLogAllowed(K.DefaultChan, Convert.ToGLogType(logType)))
				{
					logHandler.LogFormat(logType, context, format, args);					
				}
			}
		}

		public void LogException(Exception exception, UnityEngine.Object context)
		{
#if UNITY_EDITOR
			if (UseUnityEditorLogger)
			{
				GDebug.DefaultUnityLogHandler.LogException(exception, context);			
			}
#else
			if (UseUnityEngineLogger)
			{
				GDebug.DefaultUnityLogHandler.LogException(exception, context);
			}
#endif
			for (var i = 0; i < _logHandlers.Count; i++)
			{
				IGLogHandler logHandler = _logHandlers[i];
				logHandler.LogException(exception, context);
			}
			
		}
		#endregion Handle logs
		
		protected static string GetString(object message)
		{
			if (message == null)
				return "Null";
			return message is IFormattable formattable ? formattable.ToString((string) null, (IFormatProvider) CultureInfo.InvariantCulture) : message.ToString();
		}
	}
}