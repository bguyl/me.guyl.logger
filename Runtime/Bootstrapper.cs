using System.Text;
using UnityEngine.Windows;
using Path = System.IO.Path;

namespace Guyl.Logger
{
    using UnityEngine;

    /// <summary>
    /// The Bootstrapper class provides a central entry point for initializing and configuring the package
    /// </summary>
    internal static class Bootstrapper
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
            string path = Path.Combine(Application.streamingAssetsPath, "GLoggerConfig.json");
            // Debug.Log(path);
            if (!File.Exists(path))
            {
                string jsonConfig = JsonUtility.ToJson(ConfigurationSerialization.Default);
                File.WriteAllBytes(path, Encoding.UTF8.GetBytes(jsonConfig));
            }
            else
            {
                byte[] bytes = File.ReadAllBytes(path);
                // Debug.Log(bytes);
                string configJson = Encoding.UTF8.GetString(bytes);
                // Debug.Log(configJson);
                ConfigurationSerialization config = JsonUtility.FromJson<ConfigurationSerialization>(configJson);
            
                Debug.Log(configJson);   
            }

            MainLogHandler logHandler = new MainLogHandler();
            GDebug.DefaultUnityLogHandler = Debug.unityLogger.logHandler;
            GDebug.MainLogHandler = logHandler;

            if (Settings.OverrideDebugLogger)
            {
                Debug.unityLogger.logHandler = logHandler;
            }
        }
    }
}