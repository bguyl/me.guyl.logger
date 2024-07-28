using InternalBridge;

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
		public List<string> AllowedChannels { get; }
		
		/// <summary>
		/// 
		/// </summary>
		public GLogTypeFlag AllowedLogTypes { get; set; }
		
		/// <summary>
		/// 
		/// </summary>
		public bool LogEnabled { get; set; } = true;

		#region Statics
// #if UNITY_EDITOR
// 		/// <summary>
// 		/// 
// 		/// </summary>
// 		public static MethodInfo FormatMethodInfo { get; set; }
// #endif
		#endregion
		#endregion Properties

		#region Constructors
		/// <summary>
		/// 
		/// </summary>
		/// <param name="allowedChannels"></param>
		/// <param name="allowedLogTypes"></param>
		public GLogHandler(List<string> allowedChannels = null, GLogTypeFlag allowedLogTypes = GLogTypeFlag.All)
		{
			AllowedChannels = allowedChannels ?? new List<string>() { K.DefaultChan };
			AllowedLogTypes = allowedLogTypes;

			// #if UNITY_EDITOR
			// 			Assembly coreAssembly = Assembly.GetAssembly( typeof(UnityEngine.Debug) );
			// 			if (coreAssembly == null)
			// 			{
			// 				UnityEngine.Debug.LogWarning($"[{typeof(GLogHandler)}] Couldn't find Unity coreAssembly");
			// 				return;
			// 			}
			//
			// 			Type debugLogHandlerType = coreAssembly.GetType( "UnityEngine.DebugLogHandler" );
			// 			if (debugLogHandlerType == null)
			// 			{
			// 				UnityEngine.Debug.LogWarning($"[{typeof(GLogHandler)}] Couldn't find DebugLogHandler type");
			// 				return;
			// 			}
			//
			// 			FormatMethodInfo = debugLogHandlerType.GetMethod("LogFormat", new Type[]
			// 			{
			// 				typeof(LogType),
			// 				typeof(LogOption),
			// 				typeof(UnityEngine.Object),
			// 				typeof(String),
			// 				typeof(System.Object[])
			// 			});
			// #endif
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
			
// #if UNITY_EDITOR
// 			if (FormatMethodInfo != null)
// 			{
// 				FormatMethodInfo.Invoke(GDebug.DefaultUnityLogHandler, new object[]
// 				{
// 					Convert.ToLogType(logType),
// 					logOptions,
// 					context,
// 					FormatMessageForUnityConsole(logType, K.DefaultChan, context, format, caller, args),
// 					new object[] { }
// 				});	
// 			}
// 			else
// 			{
// 				GDebug.DefaultUnityLogHandler.LogFormat(Convert.ToLogType(logType), context, format, args);
// 			}
// #endif
			var debugLogIHandler = InternalWrapper.GetDebugLogHandler();
			debugLogIHandler.LogFormat(Convert.ToLogType(logType), context, format, args);

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
			
// #if UNITY_EDITOR
// 			if (FormatMethodInfo != null)
// 			{
// 				// FormatMethodInfo.Invoke(GDebug.DefaultUnityLogHandler, new object[]
// 				// {
// 				// 	logType, LogOption.None, context, "{0}", new object[]
// 				// 	{
// 				// 		FormatMessageForUnityConsole(Convert.ToGLogType(logType), K.DefaultChan, context, format, null, args)
// 				// 	}
// 				// });	
// 			}
// 			else
// 			{
// 				GDebug.DefaultUnityLogHandler.LogFormat(logType, context, format, args);
// 			}
// #endif
			var debugLogIHandler = InternalWrapper.GetDebugLogHandler();
			debugLogIHandler.LogFormat(logType, context, format, args);

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