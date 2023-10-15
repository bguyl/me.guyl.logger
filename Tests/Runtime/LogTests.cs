using System;
using UnityEngine;

namespace Guyl.Logger.Tests.Runtime
{
	using NUnit.Framework;
	using Guyl.Logger;

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
			
			GDebug.LogVeryVerbose(message);
			GDebug.LogTrace(message);
			GDebug.LogDebug(message);
			GDebug.LogInfo(message);
			GDebug.Log(message);
			GDebug.LogWarning(message);
			GDebug.LogError(message);
			GDebug.LogCritical(message);
			GDebug.LogAssert(message);
			GDebug.LogException(exception);
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
			GDebug.Log(logChan, message);
			GDebug.Log(message, new GameObject());
			GDebug.LogWarning(logChan, message);
			GDebug.LogError(logChan, message);
			GDebug.LogCritical(logChan, message);
			GDebug.LogAssert(message);
			GDebug.LogException(exception);
		}
		
		[Test]
		public void TestLogHandlerReplacement( )
		{
			const string logChan = "TESTCHAN";
			const string message = "This is a test message";
			Exception exception = new Exception("Test exception");

			Debug.Log(message);
			Debug.LogWarning(message);
			Debug.LogError(message);
			Debug.LogAssertion(message);
			Debug.LogException(exception);
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