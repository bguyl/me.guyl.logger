namespace Guyl.Logger
{
    using UnityEngine;

    public static class Bootstrapper
    {
#if UNITY_EDITOR
        [UnityEditor.InitializeOnLoadMethod]
        private static void InitializeOnLoadEditor()
        {
            InitializeOnLoad();
        }
#else
        [UnityEngine.RuntimeInitializeOnLoadMethod]
        private static void InitializeOnLoadRuntime()
        {
            InitializeOnLoad();
        }
#endif

        private static void InitializeOnLoad()
        {
            GLogHandler logHandler = new GLogHandler();
            GDebug.DefaultUnityLogHandler = UnityEngine.Debug.unityLogger.logHandler;
            GDebug.GLogHandler = logHandler;

            if (Settings.OverrideDebugLogger)
            {
                UnityEngine.Debug.unityLogger.logHandler = logHandler;
            }
        }
    }
}