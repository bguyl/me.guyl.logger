namespace Guyl.Logger
{
    public class Draft
    {
  //       		// public void LogFormat(GLogger.UnityEngine.LogType logType, UnityEngine.Object context, string format, params object[] args)
		// // {
		// //     UnityEngine.Debug.LogFormat(  );
		// //     throw new NotImplementedException();
		// // }
		// //
		// // public void LogFormat(UnityEngine.UnityEngine.LogType logType, UnityEngine.Object context, string format, params object[] args)
		// // {
		// //     throw new NotImplementedException();
		// // }
		// //
		// // public void LogException(Exception exception, UnityEngine.Object context)
		// // {
		// //     throw new NotImplementedException();
		// // }
		// private const string kNoTagFormat = "{0}";
		// private const string kTagFormat = "{0}: {1}";
		//
		// private GLogHandler( )
		// {
		// }
		//
		// /// <summary>
		// ///   <para>Create a custom Logger.</para>
		// /// </summary>
		// /// <param name="logHandler">Pass in default log handler or custom log handler.</param>
		// public GLogHandler( ILogHandler logHandler )
		// {
		// 	this.logHandler = logHandler;
		// 	this.logEnabled = true;
		// 	this.FilterGLogType = GLogType.Information;
		// }
		//
		// /// <summary>
		// ///   <para>Set  Logger.ILogHandler.</para>
		// /// </summary>
		// public ILogHandler logHandler { get; set; }
		//
		// public GLogHandler SuperGLogHandler => (GLogHandler) logHandler;
		//
		// /// <summary>
		// ///   <para>To runtime toggle debug logging [ON/OFF].</para>
		// /// </summary>
		// public bool logEnabled { get; set; }
		//
		// // /// <summary>
		// // ///   <para>To selective enable debug log message.</para>
		// // /// </summary>
		// // public UnityEngine.LogType filterLogType { get; set; }
		// /// <summary>
		// ///   <para>To selective enable debug log message.</para>
		// /// </summary>
		// public GLogType FilterGLogType { get; set; }
		//
		// /// <summary>
		// ///   <para>Check logging is enabled based on the UnityEngine.LogType.</para>
		// /// </summary>
		// /// <param name="logType">The type of the log message.</param>
		// /// <returns>
		// ///   <para>Retrun true in case logs of UnityEngine.LogType will be logged otherwise returns false.</para>
		// /// </returns>
		// public bool IsLogTypeAllowed( UnityEngine.LogType logType )
		// {
		// 	if ( this.logEnabled )
		// 	{
		// 		if ( logType == UnityEngine.LogType.Exception ) return true;
		// 		if ( this.FilterGLogType != (GLogType) UnityEngine.LogType.Exception ) return (GLogType) logType <= this.FilterGLogType;
		// 	}
		//
		// 	return false;
		// }
		//
		// public bool IsLogTypeAllowed( GLogType gLogType )
		// {
		// 	if ( this.logEnabled )
		// 	{
		// 		if ( gLogType == GLogType.Exception ) return true;
		// 		if ( this.FilterGLogType != (GLogType) UnityEngine.LogType.Exception ) return (GLogType) gLogType <= this.FilterGLogType;
		// 	}
		//
		// 	return false;
		// }
		//
		// private static string GetString( object message )
		// {
		// 	if ( message == null ) return "Null";
		// 	return message is IFormattable formattable ?
		// 		formattable.ToString( ( string )null, ( IFormatProvider )CultureInfo.InvariantCulture ) :
		// 		message.ToString();
		// }
		//
		// /// <summary>
		// ///   <para>Logs message to the Unity Console using default logger.</para>
		// /// </summary>
		// /// <param name="logType">The type of the log message.</param>
		// /// <param name="tag">Used to identify the source of a log message. It usually identifies the class where the log call occurs.</param>
		// /// <param name="message">String or object to be converted to string representation for display.</param>
		// /// <param name="context">Object to which the message applies.</param>
		// public void Log( UnityEngine.LogType logType, object message )
		// {
		// 	if ( !this.IsLogTypeAllowed( logType ) ) return;
		// 	this.logHandler.LogFormat( logType, ( UnityEngine.Object )null, "{0}", ( object )GLogHandler.GetString( message ) );
		// }
		//
		// /// <summary>
		// ///   <para>Logs message to the Unity Console using default logger.</para>
		// /// </summary>
		// /// <param name="logType">The type of the log message.</param>
		// /// <param name="tag">Used to identify the source of a log message. It usually identifies the class where the log call occurs.</param>
		// /// <param name="message">String or object to be converted to string representation for display.</param>
		// /// <param name="context">Object to which the message applies.</param>
		// public void Log( UnityEngine.LogType logType, object message, UnityEngine.Object context )
		// {
		// 	if ( !this.IsLogTypeAllowed( logType ) ) return;
		// 	this.logHandler.LogFormat( logType, context, "{0}", ( object )GLogHandler.GetString( message ) );
		// }
		//
		// /// <summary>
		// ///   <para>Logs message to the Unity Console using default logger.</para>
		// /// </summary>
		// /// <param name="logType">The type of the log message.</param>
		// /// <param name="tag">Used to identify the source of a log message. It usually identifies the class where the log call occurs.</param>
		// /// <param name="message">String or object to be converted to string representation for display.</param>
		// /// <param name="context">Object to which the message applies.</param>
		// public void Log( UnityEngine.LogType logType, string tag, object message )
		// {
		// 	if ( !this.IsLogTypeAllowed( logType ) ) return;
		// 	this.logHandler.LogFormat( logType, ( UnityEngine.Object )null, "{0}: {1}", ( object )tag,
		// 							   ( object )GLogHandler.GetString( message ) );
		// }
		//
		// /// <summary>
		// ///   <para>Logs message to the Unity Console using default logger.</para>
		// /// </summary>
		// /// <param name="logType">The type of the log message.</param>
		// /// <param name="tag">Used to identify the source of a log message. It usually identifies the class where the log call occurs.</param>
		// /// <param name="message">String or object to be converted to string representation for display.</param>
		// /// <param name="context">Object to which the message applies.</param>
		// public void Log( UnityEngine.LogType logType, string tag, object message, UnityEngine.Object context )
		// {
		// 	if ( !this.IsLogTypeAllowed( logType ) ) return;
		// 	this.logHandler.LogFormat( logType, context, "{0}: {1}", ( object )tag,
		// 							   ( object )GLogHandler.GetString( message ) );
		// }
		//
		// /// <summary>
		// ///   <para>Logs message to the Unity Console using default logger.</para>
		// /// </summary>
		// /// <param name="logType">The type of the log message.</param>
		// /// <param name="tag">Used to identify the source of a log message. It usually identifies the class where the log call occurs.</param>
		// /// <param name="message">String or object to be converted to string representation for display.</param>
		// /// <param name="context">Object to which the message applies.</param>
		// public void Log( object message )
		// {
		// 	if ( !this.IsLogTypeAllowed( UnityEngine.LogType.Log ) ) return;
		// 	this.logHandler.LogFormat( UnityEngine.LogType.Log, ( UnityEngine.Object )null, "{0}", ( object )GLogHandler.GetString( message ) );
		// }
		//
		//
		// public void LogInfo( string message )
		// {
		// 	if ( !this.IsLogTypeAllowed( GLogType.Information ) ) return;
		// 	// this.logHandler.LogFormat( GLogger.LogType.Information, ( UnityEngine.Object )null, "{0}", ( object )Logger.GetString( message ) );
		// 	LogFormat( (UnityEngine.LogType) GLogType.Information, ( UnityEngine.Object )null, "{0}", ( object )GLogHandler.GetString( message ) );
		// }
		//
		// /// <summary>
		// ///   <para>Logs message to the Unity Console using default logger.</para>
		// /// </summary>
		// /// <param name="logType">The type of the log message.</param>
		// /// <param name="tag">Used to identify the source of a log message. It usually identifies the class where the log call occurs.</param>
		// /// <param name="message">String or object to be converted to string representation for display.</param>
		// /// <param name="context">Object to which the message applies.</param>
		// public void Log( string tag, object message )
		// {
		// 	if ( !this.IsLogTypeAllowed( UnityEngine.LogType.Log ) ) return;
		// 	this.logHandler.LogFormat( UnityEngine.LogType.Log, ( UnityEngine.Object )null, "{0}: {1}", ( object )tag,
		// 							   ( object )GLogHandler.GetString( message ) );
		// }
		//
		// /// <summary>
		// ///   <para>Logs message to the Unity Console using default logger.</para>
		// /// </summary>
		// /// <param name="logType">The type of the log message.</param>
		// /// <param name="tag">Used to identify the source of a log message. It usually identifies the class where the log call occurs.</param>
		// /// <param name="message">String or object to be converted to string representation for display.</param>
		// /// <param name="context">Object to which the message applies.</param>
		// public void Log( string tag, object message, UnityEngine.Object context )
		// {
		// 	if ( !this.IsLogTypeAllowed( UnityEngine.LogType.Log ) ) return;
		// 	this.logHandler.LogFormat( UnityEngine.LogType.Log, context, "{0}: {1}", ( object )tag,
		// 							   ( object )GLogHandler.GetString( message ) );
		// }
		//
		// /// <summary>
		// ///   <para>A variant of Logger.Log that logs an warning message.</para>
		// /// </summary>
		// /// <param name="tag">Used to identify the source of a log message. It usually identifies the class where the log call occurs.</param>
		// /// <param name="message">String or object to be converted to string representation for display.</param>
		// /// <param name="context">Object to which the message applies.</param>
		// public void LogWarning( string tag, object message )
		// {
		// 	if ( !this.IsLogTypeAllowed( UnityEngine.LogType.Warning ) ) return;
		// 	this.logHandler.LogFormat( UnityEngine.LogType.Warning, ( UnityEngine.Object )null, "{0}: {1}", ( object )tag,
		// 							   ( object )GLogHandler.GetString( message ) );
		// }
		//
		// /// <summary>
		// ///   <para>A variant of Logger.Log that logs an warning message.</para>
		// /// </summary>
		// /// <param name="tag">Used to identify the source of a log message. It usually identifies the class where the log call occurs.</param>
		// /// <param name="message">String or object to be converted to string representation for display.</param>
		// /// <param name="context">Object to which the message applies.</param>
		// public void LogWarning( string tag, object message, UnityEngine.Object context )
		// {
		// 	if ( !this.IsLogTypeAllowed( UnityEngine.LogType.Warning ) ) return;
		// 	this.logHandler.LogFormat( UnityEngine.LogType.Warning, context, "{0}: {1}", ( object )tag,
		// 							   ( object )GLogHandler.GetString( message ) );
		// }
		//
		// /// <summary>
		// ///   <para>A variant of Logger.Log that logs an error message.</para>
		// /// </summary>
		// /// <param name="tag">Used to identify the source of a log message. It usually identifies the class where the log call occurs.</param>
		// /// <param name="message">String or object to be converted to string representation for display.</param>
		// /// <param name="context">Object to which the message applies.</param>
		// public void LogError( string tag, object message )
		// {
		// 	if ( !this.IsLogTypeAllowed( UnityEngine.LogType.Error ) ) return;
		// 	this.logHandler.LogFormat( UnityEngine.LogType.Error, ( UnityEngine.Object )null, "{0}: {1}", ( object )tag,
		// 							   ( object )GLogHandler.GetString( message ) );
		// }
		//
		// /// <summary>
		// ///   <para>A variant of Logger.Log that logs an error message.</para>
		// /// </summary>
		// /// <param name="tag">Used to identify the source of a log message. It usually identifies the class where the log call occurs.</param>
		// /// <param name="message">String or object to be converted to string representation for display.</param>
		// /// <param name="context">Object to which the message applies.</param>
		// public void LogError( string tag, object message, UnityEngine.Object context )
		// {
		// 	if ( !this.IsLogTypeAllowed( UnityEngine.LogType.Error ) ) return;
		// 	this.logHandler.LogFormat( UnityEngine.LogType.Error, context, "{0}: {1}", ( object )tag,
		// 							   ( object )GLogHandler.GetString( message ) );
		// }
		//
		// /// <summary>
		// ///   <para>A variant of Logger.Log that logs an exception message.</para>
		// /// </summary>
		// /// <param name="exception">Runtime Exception.</param>
		// /// <param name="context">Object to which the message applies.</param>
		// public void LogException( Exception exception )
		// {
		// 	if ( !this.logEnabled ) return;
		// 	this.logHandler.LogException( exception, ( UnityEngine.Object )null );
		// }
		//
		// /// <summary>
		// ///   <para>A variant of Logger.Log that logs an exception message.</para>
		// /// </summary>
		// /// <param name="exception">Runtime Exception.</param>
		// /// <param name="context">Object to which the message applies.</param>
		// public void LogException( Exception exception, UnityEngine.Object context )
		// {
		// 	if ( !this.logEnabled ) return;
		// 	this.logHandler.LogException( exception, context );
		// }
		//
		// /// <summary>
		// ///   <para>Logs a formatted message.</para>
		// /// </summary>
		// /// <param name="logType">The type of the log message.</param>
		// /// <param name="context">Object to which the message applies.</param>
		// /// <param name="format">A composite format string.</param>
		// /// <param name="args">Format arguments.</param>
		// public void LogFormat( UnityEngine.LogType logType, string format, params object[] args )
		// {
		// 	if ( !this.IsLogTypeAllowed( logType ) )
		// 		return;
		// 	this.logHandler.LogFormat( logType, ( UnityEngine.Object )null, format, args );
		// }
		//
		// /// <summary>
		// ///   <para>Logs a formatted message.</para>
		// /// </summary>
		// /// <param name="logType">The type of the log message.</param>
		// /// <param name="context">Object to which the message applies.</param>
		// /// <param name="format">A composite format string.</param>
		// /// <param name="args">Format arguments.</param>
		// public void LogFormat( UnityEngine.LogType logType, UnityEngine.Object context, string format, params object[] args )
		// {
		// 	if ( !this.IsLogTypeAllowed( logType ) ) return;
		// 	this.logHandler.LogFormat( logType, context, format, args );
		// }
		//
		// public void LogFormat( GLogType gLogType, UnityEngine.Object context, string format, params object[] args )
		// {
		// 	if ( !this.IsLogTypeAllowed( gLogType ) ) return;
		// 	this.logHandler.LogFormat( (UnityEngine.LogType) gLogType, context, format, args );
		// }
		
		#region Log
		// [HideInCallstack]
		// public static void Log(object message)
		// {
		//     GLogHandler.LogFormat(GLogType.Log, K.DefaultChan, null, "{0}", null, LogOption.None, message);
		// }
		//
		// [HideInCallstack]
		// public new static void Log(object message, Object context)
		// {
		//     GLogHandler.LogFormat(GLogType.Log, K.DefaultChan, context, "{0}", null, LogOption.None, message);
		// }
		//
		// [HideInCallstack]
		// public static void Log(string channel, object message, Object context = null, object caller = null, LogOption logOptions = LogOption.None)
		// {
		//     GLogHandler.LogFormat(GLogType.Log, channel, context, "{0}", caller, logOptions, message);
		// }
		#endregion Log

    }
}