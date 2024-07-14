namespace Guyl.GLogger.Editor
{
	using UnityEngine;
	using UnityEngine.UIElements;
	using UnityEditor;

	public class GConsole : EditorWindow
	{
		[SerializeField]
		private VisualTreeAsset _visualTreeAsset = default;
		
		[MenuItem( K.MENU_ITEM + "GConsole" )]
		private static void ShowWindow( )
		{
			GConsole window = GetWindow<GConsole>();
			window.titleContent = new GUIContent( "GConsole" );
			window.Show();
		}
		
		private void OnEnable( )
		{
			CreateUI();
		}

		private void CreateUI( )
		{
			VisualElement root = rootVisualElement;
			_visualTreeAsset.CloneTree( root );
		}
	}
}