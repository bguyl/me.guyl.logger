namespace Guyl.GLogger.ScriptableObjects
{
    using System.Collections.Generic;
    using NaughtyAttributes;
    using UnityEngine;

    [CreateAssetMenu(fileName = "New GLogger Settings", menuName = K.CREATE_ASSET_MENU + "GLogger Settings")]
    public class GLoggerSettings : ScriptableObject
    {
        [SerializeField] private bool _overrideDebugLogger = true;
        [SerializeReference] private List<IGLogFormatter> _logFormatters = new List<IGLogFormatter>();
        [SerializeReference] private IGLogFormatter _thisFormatter;

        [Button ("Foobar ??")]
        private void AddUnityFormatter()
        {
            Debug.LogWarning("Hello");  
            UnityEditorLogFormatter unity = new UnityEditorLogFormatter();
            _logFormatters.Add(unity);
            _thisFormatter = unity;
        }
    }
}