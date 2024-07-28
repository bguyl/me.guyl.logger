using UnityEngine;

namespace InternalBridge
{
	public class InternalWrapper
	{
		public static ILogHandler GetDebugLogHandler()
		{
			return new DebugLogHandler();
		}
	}
}