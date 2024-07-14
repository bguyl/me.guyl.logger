using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine.UIElements;
using Assembly = System.Reflection.Assembly;

namespace Guyl.GLogger.Editor {
	public class TempSettingWindow : EditorWindow
	{

		public class TempChanEntry
		{
			public string channelId;
			public string channelName;
			public bool enabled;
		}
		
		private List<TempChanEntry> _chanEntries = new();
		private MultiColumnListView listView;
		
		[MenuItem( K.MENU_ITEM + "GLogger Settings" )]
		private static void ShowWindow( )
		{
			TempSettingWindow window = GetWindow<TempSettingWindow>();
			window.titleContent = new UnityEngine.GUIContent( "GLogger Settings" );
			window.Show();
		}

		private void OnEnable( )
		{
			CreateUI();
		}

		private void CreateUI( )
		{
			VisualElement root = rootVisualElement;
			
			// Create a button
			Button button = new Button
			{
				text = "Click Me"
			};
			button.clickable.clicked -= OnButtonClick;
			button.clickable.clicked += OnButtonClick;
			
			Columns columns = new()
			{
				new Column
				{
					title = "Enabled",
					bindCell = OnBindCellA,
					makeCell = OnMakeBooleanCell,
					sortable = true,
				},
				new Column
				{
					title = "Channel Name",
					bindCell = OnBindCellB,
					makeCell = OnMakeLabelCell,
				},
				new Column
				{
					title = "Variable Name",
					bindCell = OnBindCellC,
					makeCell = OnMakeLabelCell,
				}
			};
			
			listView = new(columns) { itemsSource = _chanEntries };

			root.Add( button );
			root.Add( listView );
		}
		
		private void OnBindCellA(VisualElement ve, int index)
		{
			Toggle toggle = (Toggle) ve;
			toggle.value = _chanEntries[ index ].enabled;
		}

		private void OnBindCellB(VisualElement ve, int index)
		{
			Label label = (Label) ve;
			label.text = _chanEntries[ index ].channelId;
		}

		private void OnBindCellC(VisualElement ve, int index)
		{
			Label label = (Label) ve;
			label.text = _chanEntries[ index ].channelName;
		}

		private VisualElement OnMakeLabelCell( ) => new Label();
		private VisualElement OnMakeBooleanCell( ) => new Toggle();
		
		private void OnButtonClick( )
		{
			_chanEntries.Clear();
			List<Assembly> allAssemblies = new();
		
			Assembly[] editorAssemblies = AppDomain.CurrentDomain.GetAssemblies();
			allAssemblies.AddRange( editorAssemblies );
		
			foreach ( Assembly assembly in allAssemblies )
			{
				string assemblyName = assembly.GetName().Name;
				if ( assemblyName.StartsWith( "UnityEngine" ) ||
					 assemblyName.StartsWith( "UnityEditor" ) ||
					 assemblyName.StartsWith( "Unity" ) ||
					 assemblyName.StartsWith( "System" ) ||
					 assemblyName.StartsWith( "mscorlib" ) ||
					 assemblyName.StartsWith( "netstandard" ) ||
					 assemblyName.StartsWith( "Mono" )
				)
				{
					continue;
				}
		
				foreach ( Type type in assembly.GetTypes() )
				{
					IEnumerable<FieldInfo> fields =
						type.GetFields( BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.FlattenHierarchy );
		
					foreach ( FieldInfo fieldInfo in fields )
					{
						if ( fieldInfo.FieldType != typeof(string) || fieldInfo.FieldType != typeof(string) )
						{
							continue;
						}
		
						LogChannelAttribute attribute =
							Attribute.GetCustomAttribute( fieldInfo, typeof(LogChannelAttribute) ) as
								LogChannelAttribute;
						if ( attribute == null )
						{
							continue;
						}
		
						string name = fieldInfo.Name;
						string value = (string) fieldInfo.GetValue( type );
						
						_chanEntries.Add( new TempChanEntry() { channelId = name, channelName = value} );
					}
				}
			}
			UnityEngine.Debug.Log( $"Refresh list ({_chanEntries.Count} found(s))" );
			listView.RefreshItems();
		}
	}
}

// Dirty drafts
			//
			// // Create a button
			// Button button = new Button
			// {
			// 	text = "Click Me"
			// };
			// button.clickable.clicked -= OnButtonClick;
			// button.clickable.clicked += OnButtonClick;
			// root.Add( button );
			//
			// // Create a new MultiColumnListView
			// listView = new MultiColumnListView();
			//
			// _list = new List<ListItem>()
			// {
			// 	new()
			// 	{
			// 		f1 = "1_1",
			// 		f2 = "1_2",
			// 		f3 = "1_3",
			// 	},
			// 	new()
			// 	{
			// 		f1 = "2_1",
			// 		f2 = "2_2",
			// 		f3 = "2_3",
			// 	},
			// 	new()
			// 	{
			// 		f1 = "3_1",
			// 		f2 = "3_2",
			// 		f3 = "3_3",
			// 	}
			// };
			//
			//
			// Columns columns = new Columns
			// {
			// 	new Column
			// 	{
			// 		name = "column1",
			// 		title = "title1",
			// 		bindCell = (x, y) => { OnBindCell(x, y, 0); },
			// 		makeCell = OnMakeCell,
			// 		sortable = true,
			// 	},
			// 	new Column
			// 	{
			// 		name = "column2",
			// 		title = "title2",
			// 		bindCell = (x, y) => { OnBindCell(x, y, 1); },
			// 		makeCell = OnMakeCell,
			// 	},
			// 	new Column
			// 	{
			// 		name = "column3",
			// 		title = "title3",
			// 		bindCell = (x, y) => { OnBindCell(x, y, 2); },
			// 		makeCell = OnMakeCell,
			// 	}
			// };
			// MultiColumnListView mc = new MultiColumnListView(columns)
			// {
			// 	itemsSource = _list
			// };
			//
			// // listView.itemsSource = _chanEntries;
			// //
			// // // Add columns to the list view
			// // // listView.columns.Add( new MultiColumnHeaderState.Column() { headerContent = new GUIContent("Column 1") } );
			// // Columns columns = new Columns();
			// // columns.Add(new Column()
			// // {
			// // 	name = "column1",
			// // 	title = "title1",
			// // 	bindCell = BindCell1,
			// // 	makeCell = ( ) => new Label( "Foobar" ),
			// // 	sortable = true
			// // });
			// // columns.Add(new Column()
			// // {
			// // 	name = "column2",
			// // 	title = "title2",
			// // 	bindCell = BindCell2,
			// // 	makeCell = ( ) => new Label( "Foobar2" ),
			// // 	sortable = true
			// // });
			// // listView.columns.Add( new Column()
			// // {
			// // 	width = 100,
			// // 	bindHeader = BindHeader,
			// // 	makeHeader = MakeHeader
			// // 	
			// // } );
			// // listView.AddColumn(new MultiColumnHeader.Column() { width = 100, headerContent = new GUIContent("Column 1") });
			// // listView.AddColumn(new MultiColumnHeader.Column() { width = 100, headerContent = new GUIContent("Column 2") });
			// // listView.AddColumn(new MultiColumnHeader.Column() { width = 100, headerContent = new GUIContent("Column 3") });
			//
			// // Create a list
			// // listView = new ListView( _chanEntries, 30, CreateElement, BindElement );
			// root.Add( listView );
		//
		//
		// private VisualElement MakeHeader( )
		// {
		// 	return new Label( "Foobar" );
		// }
		//
		// private void BindHeader( VisualElement obj )
		// {
		// 	Label label = ( Label ) obj;
		// 	label.text = "Item"; }
		//
		// private void CreateGUI( )
		// {
		// }
		//
		// private VisualElement CreateElement( )
		// {
		// 	Label label = new Label
		// 	{
		// 		style =
		// 		{
		// 			unityTextAlign = TextAnchor.MiddleCenter
		// 		}
		// 	};
		// 	return label;
		// }
		//
		// private void BindElement( VisualElement element, int index )
		// {
		// 	Label label = ( Label ) element;
		// 	label.text = $"{_chanEntries[index].channelId} - {_chanEntries[index].channelName}";
		// }
		//
		// private void BindCell1( VisualElement element, int index )
		// {
		// 	Label label = ( Label ) element;
		// 	label.text = $"{_chanEntries[index].channelId}";
		// }
		//
		// private void BindCell2( VisualElement element, int index )
		// {
		// 	Label label = ( Label ) element;
		// 	label.text = $"{_chanEntries[index].channelName}";
		// }
		//
		// private VisualElement OnMakeCell()
		// {
		// 	VisualElement ve = new VisualElement();
		// 	Label label = new Label();
		// 	ve.Add(label);
 	//
		// 	return ve;
		// }
 	//
		// private void OnBindCell(VisualElement ve, int index, int columnIndex)
		// {
		// 	if (columnIndex == 0) ve.Q<Label>().text = _list[index].f1;
		// 	if (columnIndex == 1) ve.Q<Label>().text = _list[index].f2;
		// 	if (columnIndex == 3) ve.Q<Label>().text = _list[index].f3;
		// }
		//
		// private void OnButtonClick( )
		// {
		// 	_chanEntries.Clear();
		// 	List<Assembly> allAssemblies = new();
		//
		// 	Assembly[] editorAssemblies = AppDomain.CurrentDomain.GetAssemblies();
		// 	allAssemblies.AddRange( editorAssemblies );
		//
		// 	foreach ( Assembly assembly in allAssemblies )
		// 	{
		// 		string assemblyName = assembly.GetName().Name;
		// 		if ( assemblyName.StartsWith( "UnityEngine" ) ||
		// 			 assemblyName.StartsWith( "UnityEditor" ) ||
		// 			 assemblyName.StartsWith( "Unity" ) ||
		// 			 assemblyName.StartsWith( "System" ) ||
		// 			 assemblyName.StartsWith( "mscorlib" ) ||
		// 			 assemblyName.StartsWith( "netstandard" ) ||
		// 			 assemblyName.StartsWith( "Mono" )
		// 		)
		// 		{
		// 			continue;
		// 		}
		//
		// 		foreach ( Type type in assembly.GetTypes() )
		// 		{
		// 			IEnumerable<FieldInfo> fields =
		// 				type.GetFields( BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.FlattenHierarchy );
		//
		// 			foreach ( FieldInfo fieldInfo in fields )
		// 			{
		// 				if ( fieldInfo.FieldType != typeof(string) || fieldInfo.FieldType != typeof(string) )
		// 				{
		// 					continue;
		// 				}
		//
		// 				LogChannelAttribute attribute =
		// 					Attribute.GetCustomAttribute( fieldInfo, typeof(LogChannelAttribute) ) as
		// 						LogChannelAttribute;
		// 				if ( attribute == null )
		// 				{
		// 					continue;
		// 				}
		//
		// 				string name = fieldInfo.Name;
		// 				string value = (string) fieldInfo.GetValue( type );
		// 				
		// 				_chanEntries.Add( new TempChanEntry() { channelId = name, channelName = value} );
		// 			}
		// 		}
		// 	}
		// 	UnityEngine.Debug.Log( $"Refresh list ({_chanEntries.Count} found(s))" );
		// 	listView.RefreshItems();
		// }