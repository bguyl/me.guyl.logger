namespace Guyl.Logger.Editor
{
    using UnityEngine;
    using UnityEngine.UIElements;
    using UnityEngine.Scripting;
    using UnityEditor;

    public class LogEntry : VisualElement
    {
        [Preserve]
        public new class UxmlFactory : UxmlFactory<LogEntry, UxmlTraits> { }
        
        [Preserve]
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
            private readonly UxmlEnumAttributeDescription<GLogType> _logType = new UxmlEnumAttributeDescription<GLogType> { name = "log-type", defaultValue = GLogType.Log };

            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);

                LogType = _logType.GetValueFromBag(bag, cc);
                // var item = ve as Tooltip;
                //
                // item.Delay = delay.GetValueFromBag(bag, cc);
                // item.FadeTime = fadeTime.GetValueFromBag(bag, cc);
            }
            
            public GLogType LogType { get; private set; } = GLogType.Log;
        }

        // private Label label;
        // private IVisualElementScheduledItem task;

        private const string _markupResource = "Packages/me.guyl.logger/Editor/LogEntry/LogEntry.uxml";

        [SerializeField] private VisualTreeAsset m_VisualTreeAsset = default;
        
        public LogEntry()
        {
            VisualTreeAsset visualTreeAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(_markupResource);
            Add(visualTreeAsset.CloneTree());
        }
    }

}