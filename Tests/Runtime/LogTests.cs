using System;
using System.Reflection;
using UnityEngine;

namespace Guyl.GLogger.Tests.Runtime
{
	using NUnit.Framework;
	using GLogger;

	public class LogTests
	{
		public void Foobar()
		{
			// UnityEngine.Debug.LogFormat( UnityEngine.LogType.Error, LogOption.None, null, "Message 1" );
			// GLogger.Debug.LogFormat( UnityEngine.LogType.Error, LogOption.None, null, "Message 2" );
			//
			// GLogger.Logger logger = new GLogger.Logger((ILogHandler) UnityEngine.Debug.unityLogger);
			// UnityEngine.Debug.unityLogger.LogFormat( UnityEngine.LogType.Error, "Message 3" );
			// logger.LogFormat( UnityEngine.LogType.Error, "Message 4" );
			//
			// UnityEngine.Debug.unityLogger.Log( "Message 5" );
			// logger.Log("Message 6" );
			//
			// logger.LogInfo("Message 6" );
		}
		
		[Test]
		public void TestClassicLogs( )
		{
			const string message = "This is a test message";
			Exception exception = new Exception("Test exception");
			
			GDebug.LogVeryVerbose("This is a VeryVerbose message");
			GDebug.LogTrace("This is a Trace message");
			GDebug.LogDebug("This is a Debug message");
			GDebug.LogInfo("This is a Info message");
			GDebug.Log("This is a Log message");
			// GDebug.LogWarning("This is a Warning message");
			
			Debug.LogWarningFormat("This is a Warning {0} message", "Foobar");
			
			Debug.LogError("This is a Error message");
			GDebug.LogCritical("This is a Critical message");
			GDebug.LogAssertion("This is a Assertion message");
			GDebug.LogException(exception);
			
			UnityEngine.Debug.Log("Vanilla: This is a Log message");
			UnityEngine.Debug.LogWarning("Vanilla:  is a Warning message");
			UnityEngine.Debug.LogError("Vanilla: This is a Error message");
			UnityEngine.Debug.LogAssertion("Vanilla:  is a Assertion message");
			UnityEngine.Debug.LogException(exception);
		}
		
		[Test]
		public void TestLogsWithChannels( )
		{
			const string logChan = "TESTCHAN";
			const string message = "This is a test message";
			Exception exception = new Exception("Test exception");
			
			GDebug.LogVeryVerbose(logChan, message);
			GDebug.LogTrace(logChan, message);
			GDebug.LogDebug(logChan, message);
			GDebug.LogInfo(logChan, message);
			// GDebug.Log(logChan, message);
			// GDebug.Log(message, new GameObject());
			GDebug.LogWarning(logChan, message);
			GDebug.LogError(logChan, message);
			GDebug.LogCritical(logChan, message);
			GDebug.LogAssertion(message);
			GDebug.LogException(exception);
		}
		
		[Test]
		public void TestLogHandlerReplacement( )
		{
			const string logChan = "TESTCHAN";
			const string message = "This is a test message";
			Exception exception = new Exception("Test exception");

			Assembly coreAssembly = Assembly.GetAssembly( typeof(UnityEngine.Debug) );
			Type debugLogHandler = coreAssembly.GetType( "UnityEngine.DebugLogHandler" );
			
			MethodInfo logFormatMethodInfo = debugLogHandler.GetMethod("LogFormat", new Type[]
			{
				typeof(LogType),
				typeof(LogOption),
				typeof(UnityEngine.Object),
				typeof(String),
				typeof(System.Object[])
			});
			logFormatMethodInfo.Invoke(UnityEngine.Debug.unityLogger.logHandler, new object[] { LogType.Warning, LogOption.None, null, "format", new object[] {} });
			
			// Debug.Log( debugLogHandler );
			
			
			// Debug.Log( Assembly.GetAssembly( typeof(Debug) ) );
			
			// Debug.Log(message);
			// Debug.LogWarning(message);
			// Debug.LogError(message);
			// Debug.LogAssertion(message);
			// Debug.LogException(exception);
		}

		public void Signatures()
		{
			// ===== Debug.cs =====
			// -- Log --
			// Log(object message)
			// Log(object message, Object context)
			// -- LogFormat --
			// LogFormat(string format, params object[] args)
			// LogFormat(Object context, string format, params object[] args)
			// LogFormat(LogType logType, LogOption logOptions,Object context, string format, params object[] args)
			// -- LogError --
			// LogError(object message)
			// LogError(object message, Object context)
			// LogErrorFormat(string format, params object[] args)
			// LogErrorFormat(Object context, string format, params object[] args)
			// -- LogException --
			// LogException(Exception exception)
			// LogException(Exception exception, Object context)
			// -- LogWarning --
			// LogWarning(object message)
			// LogWarning(object message, Object context)
			// LogWarningFormat(string format, params object[] args)
			// LogWarningFormat(Object context, string format, params object[] args)
			// -- Assert --
			// Assert(bool condition)
			// Assert(bool condition, Object context)
			// Assert(bool condition, object message)
			// Assert(bool condition, string message)
			// Assert(bool condition, object message, Object context)
			// Assert(bool condition, string message, Object context)
			// AssertFormat(bool condition, string format, params object[] args)
			// AssertFormat(bool condition, Object context,string format, params object[] args)
			// -- LogAssertion --
			// LogAssertion(object message)
			// LogAssertion(object message, Object context)
			// LogAssertionFormat(string format, params object[] args)
			// LogAssertionFormat(Object context, string format, params object[] args)
			
			
			// ===== Debug.cs - Internal =====
			// LogError(string message, string fileName, int lineNumber, int columnNumber)
			// LogWarning(string message, string fileName, int lineNumber, int columnNumber)
			// LogInfo(string message, string fileName, int lineNumber, int columnNumber)
			// LogSticky(int identifier, LogType logType,LogOption logOptions, string message, Object context = null)
		}
	}
}