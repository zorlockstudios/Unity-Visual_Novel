//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;


[ CustomEditor( typeof( DialogPartNode ) ) ] 
[ System.Serializable ]
public class DialogPartNodeInspector : Editor {
	// DialogPartNode members.
/*	private enum Mode{
		SIMPLE=0,
		ADVANCED=1,
	}
//	static private Mode m_CurrMode;
//*/
	static private int m_ViewMode = 0;
//	static private string[] m_MenuItems ={ "Actions" , "Edit Text" , "XML" };
	static private string[] m_MenuItems ={ "Actions" , "Edit Text" , "XML" };

//	static private int m_ImportDataTypeID = 0;
//	static private string[] m_ImportDataTypes ={ "Text" , "XML" };

	static public TextAsset m_TxtAsset;
	static public string m_NameDelimiter = "<";

//	static private int m_SelectedModeID = 0;

#if false	
	static private ViNoAnimationManager m_AnimManagerInstance;
	static public ViNoAnimationManager animManagerInstance{
		get{ return m_AnimManagerInstance; }
	}
	static private string[] m_AnimationEntries;
	static public string[] animationEntries{
		get{	
			return m_AnimationEntries;
		}
	}	
#endif	
	
	static public bool m_ShowAllToggle;

	static private ViNoSoundPlayer m_SoundPlInstance;
	static private string[] m_BgmEntries;
	static private string[] m_SeEntries;
	static private string[] m_VoiceEntries;	

	static public string[] bgmEntries{
		get{	
			return m_BgmEntries;
		}
	}

	static public string[] seEntries{
		get{	
			return m_SeEntries;
		}
	}

	static public string[] voiceEntries{
		get{	
			return m_VoiceEntries;
		}
	}
		
//	private SerializedProperty soundDataProp = null;
	static private string m_FlagmentText; 
	static private bool m_IsOverwrite;
	static private string m_BlockDelimiter;

//	static private Vector2 m_ScrollPos = Vector2.zero;

	void OnEnable( ){			
//		Debug.Log( "OnEnable DialogPartNode");
		DialogPartNode dlgNode = ( target as DialogPartNode );

		serializedObject.FindProperty ("__dummy__");
//	    soundDataProp = serializedObject.FindProperty ("soundData");

//	    DialogPartNodeUtility.RestoreSceneData( dlgNode );

		dlgNode.FindTextBoxObjects();

		// Assign DialogID.
		dlgNode.ReAssignDialogIDsInThisObject();
		
		if( m_SoundPlInstance == null ){
			m_SoundPlInstance = GameObject.FindObjectOfType( typeof( ViNoSoundPlayer ) ) as ViNoSoundPlayer;
		}		

		if( m_SoundPlInstance != null ){
			m_BgmEntries = m_SoundPlInstance.GetSoundEntryNames();	
			m_VoiceEntries = m_SoundPlInstance.GetVoiceEntryNames();	
			m_SeEntries = m_SoundPlInstance.GetSeEntryNames();			
#if false				
			ScriptableSoundData sound = soundDataProp.objectReferenceValue as ScriptableSoundData;
			if( sound != null ){				
/*				m_SoundPlInstance.soundEntry = sound.bgmEntries;
				m_SoundPlInstance.seEntries = sound.seEntries;
				m_SoundPlInstance.voiceEntries = sound.voiceEntries;

//*/				
			}
#endif			
		}

#if false
		if( m_AnimManagerInstance == null ){
			m_AnimManagerInstance = GameObject.FindObjectOfType( typeof( ViNoAnimationManager ) ) as ViNoAnimationManager;
		}
		if( m_AnimManagerInstance != null ){			
			m_AnimManagerInstance.CollectAnimationNames();
			m_AnimationEntries = m_AnimManagerInstance.animNames;	
		}
#endif		
	}
		
	public override void OnInspectorGUI(){
		serializedObject.Update();		

		DialogPartNode targetNode = target as DialogPartNode;		

//	 	m_ScrollPos = EditorGUILayout.BeginScrollView( m_ScrollPos );

			OnGUI_DialogPart( targetNode );

//		EditorGUILayout.EndScrollView();

		serializedObject.ApplyModifiedProperties();
	}

	static public void DrawDialogTextBoxAndNameField( DialogPartNode node ){
		EditorGUILayout.BeginHorizontal();
			GUILayout.Space( 15f );
			if( GUILayout.Button( "Find" ) ){
				DialogPartNodeUtility.FindNameTextBox( node );
			}
			EditorGUILayout.LabelField( "NameTextBox" , GUILayout.Width( 100f) );
			node.m_NameTextBox = (ViNoTextBox)EditorGUILayout.ObjectField( node.m_NameTextBox , typeof( ViNoTextBox ) , true );
			if( node.m_NameTextBox != null ){
				node.m_NameHndName = node.m_NameTextBox.name;
			}
		EditorGUILayout.EndHorizontal();
		
		EditorGUILayout.BeginHorizontal();
			GUILayout.Space( 15f );
			if( GUILayout.Button( "Find" ) ){
				DialogPartNodeUtility.FindDialogTextBox( node );
			}
			EditorGUILayout.LabelField( "TextBox", GUILayout.Width( 100f) );
		 	node.m_ViNoTextBox = (ViNoTextBox)EditorGUILayout.ObjectField( node.m_ViNoTextBox , typeof( ViNoTextBox ) , true );

			if( node.m_ViNoTextBox != null ){
				node.m_DialogHndName = node.m_ViNoTextBox.name;
			}

		EditorGUILayout.EndHorizontal();
	}

	public void OnGUI_DialogPart( DialogPartNode targetNode ){
//		m_SelectedModeID = GUILayout.Toolbar( m_SelectedModeID , m_ModeItems );
//		m_CurrMode = (Mode)m_SelectedModeID;

		// ------- There are two TextBox field before ! ------.

			DrawDialogTextBoxAndNameField( targetNode );

		// ---------------------------------------------------.

//TODO : TEST SOUND DATA PROPERTY!!
#if false			
		if ( soundDataProp != null){
			EditorGUILayout.PropertyField( soundDataProp );
		}		
/*			
			EditorGUILayout.BeginHorizontal();
				EditorGUILayout.LabelField( "SoundData", GUILayout.Width( 100f) );
			 	soundData = EditorGUILayout.ObjectField( soundData , typeof( ScriptableSoundData ) , true ) as ScriptableSoundData;
//			 	soundDataProp = EditorGUILayout.ObjectField( soundDataProp, typeof( ScriptableSoundData ) , true ) as ScriptableSoundData;
			EditorGUILayout.EndHorizontal();
//*/			
#endif

//		EditorGUILayout.EndVertical();
//		EditorGUILayout.BeginHorizontal();
//		EditorGUILayout.EndHorizontal();

		m_ViewMode = GUILayout.Toolbar( m_ViewMode , m_MenuItems );

//		ViNoEditorUtil.BeginGreenColor();

			string menu = m_MenuItems[ m_ViewMode ];

			// View Mode : XML.
			if( menu == "XML" ){
				ViNoEditorUtil.BeginGreenColor();
			
					DrawImportField( targetNode );

				ViNoEditorUtil.EndGUIColor();	
			}
			// Actions or Edit Text Mode.
			else{
#if true
				if( menu == "Actions" ){
					EditorGUILayout.BeginHorizontal();

	/*				if( GUILayout.Button( "Open "+ System.Environment.NewLine +" Dialogs" )){
						m_ShowAllToggle = ! m_ShowAllToggle;
						for( int i=0;i<targetNode.dlgDataList.Count;i++){
							targetNode.dlgDataList[ i ].show = m_ShowAllToggle;
						}
					}
	//*/
					if( GUILayout.Button( "Check " + System.Environment.NewLine + " ALL" )){
	//				if( GUILayout.Button( new GUIContent( "All" , ViNoEditorResources.checkAllcon ) , GUILayout.Width( 62f ) , GUILayout.Height( 22f ) ) ){// "Check " + System.Environment.NewLine + " ALL" )){
						for( int i=0;i<targetNode.dlgDataList.Count;i++){
							targetNode.dlgDataList[ i ].active = true;
						}
					}

					if( GUILayout.Button( "UNCheck" + System.Environment.NewLine + "ALL" )){
	//				if( GUILayout.Button( uncheckTex ) ){
						for( int i=0;i<targetNode.dlgDataList.Count;i++){
							targetNode.dlgDataList[ i ].active = false;
						}
					}
					
					if( GUILayout.Button( "Swap Checked"+ System.Environment.NewLine + "2 Items" )){
						DialogPartNodeUtility.Swap2Items( targetNode );
					}
			
					GUI.enabled = true;

					EditorGUILayout.EndHorizontal();
				}
#endif						
				if( targetNode.dlgDataList != null ){
					for( int i=0;i<targetNode.dlgDataList.Count;i++){
						DialogPartData unit = targetNode.dlgDataList[ i ];
						if( menu == "Actions"  ){//&& unit.actionID != DialogPartNodeActionType.Dialog ){
							NodeGUI.DrawDialogItemBar( targetNode , ref unit , i , targetNode.m_ViNoTextBox , targetNode.m_NameTextBox  );					
						}
						NodeGUI.OnGUI_a( targetNode , ref unit , i , targetNode.m_ViNoTextBox , targetNode.m_NameTextBox , m_ViewMode );				
					}		
				}
				else{
					DialogPartNodeUtility.Initialize( targetNode );
				}
				DialogPartNodeInspector.DrawDialogListUtil( targetNode );
			}
//			ViNoEditorUtil.EndGUIColor();	
		}	

	static public void DrawDialogListUtil( DialogPartNode node ){
		GUICommon.DrawLineSpace( 10f , 5f );
		
		ViNoEditorUtil.BeginGUIColor( Color.green , GUI.backgroundColor , GUI.contentColor );//Color.black , Color.white );
		
		GUILayout.BeginHorizontal();
			
//			GUILayout.Space( Screen.width/2f );

			if( GUILayout.Button( "+" ) ){
//			if( GUILayout.Button( ViNoEditorResources.plusIcon ) ){
//				node.AddData( new DialogPartData() );// AddData( node );
//				Debug.Log( "Register Scene Undo");
				Undo.RegisterUndo ( node , node.name );
//					 Undo.SetSnapshotTarget( node , node.name );

				int itemNum = node.GetMessageNum();
				node.AddItemAt( itemNum );
				node.ReAssignDialogIDsInThisObject();
			}
			
			if( GUILayout.Button( "-" ) ){
//			if( GUILayout.Button( ViNoEditorResources.minusIcon ) ){//} , GUILayout.Width( sa ) ) ){
				Undo.RegisterUndo ( node , node.name );
//				if( EditorUtility.DisplayDialog( "Remove the Last Item ?" , "Are you sure you really want to remove?"
//					, "Yes", "Cancel" ) ){
					if( node.dlgDataList != null && node.dlgDataList.Count > 0 ){
						node.dlgDataList.RemoveAt( node.dlgDataList.Count - 1 );
					}
					node.ReAssignDialogIDsInThisObject();
//				}
			}

		GUILayout.EndHorizontal();		
		
		ViNoEditorUtil.EndGUIColor();
	}

	static public void DrawImportField( DialogPartNode targetNode ){
		GUICommon.DrawLineSpace( 10f , 5f );

#if false
		if( GUILayout.Button ( "Log Scenario Script in Console" ) ){
			DialogPartNodeUtility.ExportScenarioScript( targetNode );
		}
#endif

/*		m_ImportDataTypeID = GUILayout.Toolbar( m_ImportDataTypeID , m_ImportDataTypes );
		string dataType = m_ImportDataTypes[ m_ImportDataTypeID ];
		switch( dataType ){
		 case "XML":
//*/

		 	DrawXMLImportField( targetNode );
/*			break;

		case "Text":
			EditorGUILayout.LabelField( "Overwrite ?" );
			m_IsOverwrite = EditorGUILayout.Toggle( m_IsOverwrite );

			EditorGUILayout.LabelField( "Name Delimiter" );
			m_NameDelimiter = EditorGUILayout.TextArea( m_NameDelimiter );

			EditorGUILayout.LabelField( "Block Delimiter" );
			m_BlockDelimiter = EditorGUILayout.TextArea( m_BlockDelimiter );

			m_TxtAsset = EditorGUILayout.ObjectField( "Text" , m_TxtAsset , typeof(TextAsset) , false ) as TextAsset;
			if( m_TxtAsset == null ){
				GUI.enabled = false;
			}

			if( GUILayout.Button( "Import" ) ){
				if( EditorUtility.DisplayDialog( "Overwrite ?" , "Are you sure you really want to import?"
					, "Yes", "Cancel") ){
					string[] nameDelimiter = { m_NameDelimiter  };
					DialogPartNodeUtility.ImportText( targetNode , m_TxtAsset.text , m_BlockDelimiter , nameDelimiter , true );
				}
			}

			GUI.enabled = true;					

			EditorGUILayout.LabelField( "Text Flagment" );					
			m_FlagmentText = EditorGUILayout.TextArea( m_FlagmentText );					
			if( GUILayout.Button( "Add Text Flagment" ) ){
				string[] nameDelimiter = { m_NameDelimiter  };						
				DialogPartNodeUtility.ImportText( targetNode , m_FlagmentText , m_BlockDelimiter , nameDelimiter , false );
			}

			break;
		}

//*/				
	}

	static public void DrawXMLImportField( DialogPartNode targetNode ){
		GUILayout.BeginHorizontal();

			GUILayout.Space( 30f );

			targetNode.xmlData = EditorGUILayout.ObjectField( "XMLData" , targetNode.xmlData , typeof( TextAsset ) , false ) as TextAsset;

		EditorGUILayout.EndHorizontal();
		EditorGUILayout.BeginHorizontal();		

			if( GUILayout.Button ( "Export" ) ){
				string fileName =targetNode.name + ".xml";
				targetNode.SerializeXML( fileName );
				string path = ViNoGameSaveLoad.GetDataPath() + "/" + fileName;
				AssetDatabase.Refresh();

				Debug.Log( "Saved as Xml file path:" + path );
			}

			GUI.enabled = ( targetNode.xmlData != null ) ? true : false;

			if( GUILayout.Button ( "Import" ) ){
				if( EditorUtility.DisplayDialog( "Overwrite ?" , "Are you sure you really want to import?"
					, "Yes", "Cancel") ){
					targetNode.DeserializeXML( targetNode.xmlData.text );
					m_ViewMode = 0;
				}
			}	

			GUI.enabled = true;				

		EditorGUILayout.EndHorizontal();		
	}

}
