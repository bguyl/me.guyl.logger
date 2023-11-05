namespace Guyl.Logger
{
	using System;
	using System.Collections.Generic;
	using UnityEngine;
	using System.Globalization;
#if UNITY_EDITOR
	using System.Reflection;
#endif

	/// <summary>
	/// 
	/// </summary>
	public class GLogHandler : IGLogHandler
	{
		#region FIELDS
		#region Private
		/// <summary>
		/// 
		/// </summary>
		private readonly List<IGLogHandler> _logHandlers = new List<IGLogHandler>();
		#endregion Private
		#endregion FIELDS

		#region Properties
		/// <summary>
		/// 
		/// </summary>
		private IGLogHandler IGLogHandlerInterface => this;
		
		/// <summary>
		/// 
		/// </summary>
		public HashSet<string> MutedChannels { get; }
		
		/// <summary>
		/// 
		/// </summary>
		public GLogTypeFlag AllowedLogTypes { get; set; }

		public List<IGLogFormatter> LogFormatters { get; }

		/// <summary>
		/// 
		/// </summary>
		public bool LogEnabled { get; set; } = true;

		#region Statics
#if UNITY_EDITOR
		/// <summary>
		/// 
		/// </summary>
		public static MethodInfo FormatMethodInfo { get; set; }
#endif
		#endregion
		#endregion Properties

		#region Constructors

		/// <summary>
		/// 
		/// </summary>
		/// <param name="mutedChannels"></param>
		/// <param name="allowedLogTypes"></param>
		/// <param name="formatters"></param>
		public GLogHandler(HashSet<string> mutedChannels = null, GLogTypeFlag allowedLogTypes = GLogTypeFlag.All, List<IGLogFormatter> formatters = null)
		{
			MutedChannels = mutedChannels ?? new HashSet<string>();
			AllowedLogTypes = allowedLogTypes;
			LogFormatters = formatters ?? new List<IGLogFormatter>();
			
#if UNITY_EDITOR
			// Retrieve the internal Unity DebugLogHandler.LogFormat method
			// It allow us to use the `LogOption` parameter without using Debug.LogFormat, and thus, having the correct
			// highlight on log message click 
			Assembly coreAssembly = Assembly.GetAssembly( typeof(UnityEngine.Debug) );
			if (coreAssembly == null)
			{
				UnityEngine.Debug.LogWarning($"[{typeof(GLogHandler)}] Couldn't find Unity coreAssembly");
				return;
			}

			Type debugLogHandlerType = coreAssembly.GetType( "UnityEngine.DebugLogHandler" );
			if (debugLogHandlerType == null)
			{
				UnityEngine.Debug.LogWarning($"[{typeof(GLogHandler)}] Couldn't find DebugLogHandler type");
				return;
			}

			FormatMethodInfo = debugLogHandlerType.GetMethod("LogFormat", new Type[]
			{
				typeof(LogType),
				typeof(LogOption),
				typeof(UnityEngine.Object),
				typeof(String),
				typeof(System.Object[])
			});
#endif
		}
		#endregion Constructors
		
		#region Log handlers
		/// <summary>
		/// 
		/// </summary>
		/// <param name="logHandler"></param>
		public void Register(IGLogHandler logHandler)
		{
			_logHandlers.Add(logHandler);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="logHandler"></param>
		public void Unregister(IGLogHandler logHandler)
		{
			_logHandlers.Remove(logHandler);
		}
		#endregion Log handlers

		#region Handle logs
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
		[HideInCallstack]
		public void LogFormat(GLogType logType, string channel, UnityEngine.Object context, string format, object caller, LogOption logOptions = LogOption.None, params object[] args)
		{
			if (!IGLogHandlerInterface.IsLogAllowed(channel, logType)) return;

			string message = format;
			for (int i = 0; i < LogFormatters.Count; i++)
			{
				IGLogFormatter formatter = LogFormatters[i];
				message = formatter.Format(logType, channel, context, format, caller, args);
			}

#if UNITY_EDITOR
			if (FormatMethodInfo != null)
			{
				FormatMethodInfo.Invoke(GDebug.DefaultUnityLogHandler, new object[]
				{
					Convert.ToLogType(logType),
					logOptions,
					context,
					message,
					new object[] { }
				});	
			}
			else
			{
				GDebug.DefaultUnityLogHandler.LogFormat(Convert.ToLogType(logType), context, format, args);
			}
#endif

			for (int i = 0; i < _logHandlers.Count; i++)
			{
				IGLogHandler logHandler = _logHandlers[i];
				if (logHandler.IsLogAllowed(channel, logType))
				{
					logHandler.LogFormat(logType, channel, context, format, caller, logOptions, args);					
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="logType"></param>
		/// <param name="context"></param>
		/// <param name="format"></param>
		/// <param name="args"></param>
		[HideInCallstack]
		public void LogFormat(LogType logType, UnityEngine.Object context, string format, params object[] args)
		{
			if (!IGLogHandlerInterface.IsLogAllowed(K.DefaultChan, Convert.ToGLogType(logType))) return;
			
			string message = format;
			for (int i = 0; i < LogFormatters.Count; i++)
			{
				IGLogFormatter formatter = LogFormatters[i];
				message = formatter.Format(Convert.ToGLogType(logType), K.DefaultChan, context, format, null, args);
			}
			
#if UNITY_EDITOR
			if (FormatMethodInfo != null)
			{
				FormatMethodInfo.Invoke(GDebug.DefaultUnityLogHandler, new object[]
				{
					logType,
					LogOption.None,
					context,
					message,
					new object[] { }
				});	
			}
			else
			{
				GDebug.DefaultUnityLogHandler.LogFormat(logType, context, format, args);
			}
#endif

			for (int i = 0; i < _logHandlers.Count; i++)
			{
				IGLogHandler logHandler = _logHandlers[i];
				if (logHandler.IsLogAllowed(K.DefaultChan, Convert.ToGLogType(logType)))
				{
					logHandler.LogFormat(logType, context, format, args);
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="exception"></param>
		/// <param name="context"></param>
		[HideInCallstack]
		public void LogException(Exception exception, UnityEngine.Object context)
		{
			GDebug.DefaultUnityLogHandler.LogException(exception, context);

			for (var i = 0; i < _logHandlers.Count; i++)
			{
				IGLogHandler logHandler = _logHandlers[i];
				logHandler.LogException(exception, context);
			}
		}
		#endregion Handle logs

		#region Protected methods
		protected virtual string FormatMessageForUnityConsole(GLogType logType, string channel, UnityEngine.Object context, string format, object caller, params object[] args)
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
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="message"></param>
		/// <returns></returns>
		protected static string GetString(object message)
		{
			if (message == null)
				return "Null";
			return message is IFormattable formattable ? formattable.ToString((string) null, (IFormatProvider) CultureInfo.InvariantCulture) : message.ToString();
		}
		#endregion Protected methods
	}
}