namespace Guyl.Logger
{
	using System;
	using System.Collections.Generic;
	using UnityEngine;
#if UNITY_EDITOR
	using System.Reflection;
#endif

	/// <summary>
	/// Main log handler, it will manage logs in Unity Editor and handle sub log handlers
	/// </summary>
	public class MainLogHandler : IGLogHandler
	{
		#region FIELDS
		#region Private
		private readonly List<IGLogHandler> _logHandlers = new List<IGLogHandler>();
		#endregion Private
		#endregion FIELDS

		#region Properties
		private IGLogHandler IGLogHandlerInterface => this;

		/// <summary>
		/// 
		/// </summary>
		public string[] ChannelsIncludeFilters { get; set; }

		/// <summary>
		/// /
		/// </summary>
		public string[] ChannelsExcludeFilters { get; set; }

		/// <summary>
		/// LogTypes to display
		/// </summary>
		public GLogTypeFlag AllowedLogTypes { get; set; }

		/// <summary>
		/// Log formatters, applied in the given order, to modify the display of the log
		/// </summary>
		public List<IGLogFormatter> LogFormatters { get; }

		/// <summary>
		/// Property to enable or disable logging functionality in runtime
		/// </summary>
		public bool LogEnabled { get; set; } = true;

		#region Statics
#if UNITY_EDITOR
		/// <summary>
		/// Cache of the method information, used for calling Unity default log format with Reflexion
		/// </summary>
		public static MethodInfo FormatMethodInfo { get; set; }
#endif
		#endregion
		#endregion Properties

		#region Constructors
		public MainLogHandler( string[] channelsIncludeFilters = null, string[] channelsExcludeFilters = null,
			GLogTypeFlag allowedLogTypes = GLogTypeFlag.All, List<IGLogFormatter> formatters = null)
		{
			ChannelsIncludeFilters = channelsIncludeFilters ?? new string[] {};
			ChannelsExcludeFilters = channelsExcludeFilters ?? new string[] { };
			
			AllowedLogTypes = allowedLogTypes;

			LogFormatters = formatters ?? new List<IGLogFormatter>() { new UnityEditorLogFormatter() };

#if UNITY_EDITOR
			// Retrieve the internal Unity DebugLogHandler.LogFormat method
			// It allow us to use the `LogOption` parameter without using Debug.LogFormat, and thus, having the correct
			// highlight on log message click 
			Assembly coreAssembly = Assembly.GetAssembly( typeof(UnityEngine.Debug) );
			if (coreAssembly == null)
			{
				UnityEngine.Debug.LogWarning($"[{typeof(MainLogHandler)}] Couldn't find Unity coreAssembly");
				return;
			}

			Type debugLogHandlerType = coreAssembly.GetType( "UnityEngine.DebugLogHandler" );
			if (debugLogHandlerType == null)
			{
				UnityEngine.Debug.LogWarning($"[{typeof(MainLogHandler)}] Couldn't find DebugLogHandler type");
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
		/// Add the given log handler as a sub handler for the MainLogHandler
		/// </summary>
		/// <param name="logHandler">The log handler to be registered</param>
		/// <returns>The MainLogHandler instance</returns>
		public MainLogHandler Register( IGLogHandler logHandler )
		{
			_logHandlers.Add(logHandler);
			return this;
		}

		/// <summary>
		/// Remove the specified log handler from the MainLogHandler
		/// </summary>
		/// <param name="logHandler">The log handler to unregister</param>
		/// <returns>The MainLogHandler instance</returns>
		public MainLogHandler Unregister( IGLogHandler logHandler )
		{
			_logHandlers.Remove(logHandler);
			return this;
		}
		#endregion Log handlers

		#region Handle logs
		/// <summary>
		/// Formats and logs a message
		/// </summary>
		/// <param name="logType">The type of the log message</param>
		/// <param name="channel">The channel to which the log message belongs</param>
		/// <param name="context">The Unity Object associated with the log message</param>
		/// <param name="format">The format string of the log message</param>
		/// <param name="caller">The object that called the LogFormat method</param>
		/// <param name="logOptions">The log options</param>
		/// <param name="args">The arguments to be formatted into the log message</param>
		[HideInCallstack]
		public void LogFormat( GLogType logType, string channel, UnityEngine.Object context, string format,
			object caller, LogOption logOptions = LogOption.None, params object[] args )
		{
			if ( !IGLogHandlerInterface.IsLogAllowed( channel, logType ) ) return;

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
		/// Formats and logs a message
		/// </summary>
		/// <param name="logType">The type of log message</param>
		/// <param name="context">The object associated with the log message</param>
		/// <param name="format">The format string for the log message</param>
		/// <param name="args">The arguments to be inserted into the format string</param>
		[HideInCallstack]
		public void LogFormat( LogType logType, UnityEngine.Object context, string format, params object[] args )
		{
			if ( !IGLogHandlerInterface.IsLogAllowed( K.DefaultChan, Convert.ToGLogType(logType))) return;
			
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
	}
}