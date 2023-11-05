namespace Guyl.Logger.ScriptableObjects
{
    using System.Collections.Generic;
    using NaughtyAttributes;
    using UnityEngine;

    [CreateAssetMenu(fileName = "New GLogger Settings", menuName = "Guyl/GLogger Settings")]
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