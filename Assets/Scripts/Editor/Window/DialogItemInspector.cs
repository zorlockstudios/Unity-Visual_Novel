//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
using UnityEngine;
using UnityEditor;
using System.Collections;

public class DialogItemInspector : EditorWindow {
	public Vector2 m_ScrollPos = Vector2.zero;
	public DialogPartNode current;

	public int m_CurrentID = 0;
	public int m_DialogItemNum = 0;
	public DialogPartData currentData;

	static private bool sceneToggle = true;
	static private bool actorToggle = true;
	static private bool dialogToggle = true;
	static private bool soundsToggle = true;

	[MenuItem( "Window/ViNo/DialogItemInspector") ]
	static public void Init(){
		GetWindow( typeof( DialogItemInspector) );
//		LoadIcons();		
	}

	public void InitWith( DialogPartNode node , int id ){
		current = node;
		m_CurrentID = id;
		UpdateGameView();
	}

	void OnGUI(){
/*		if( m_DialogIconTex == null || m_ArrowBackTex == null || m_ArrowForwardTex == null ){
			LoadIcons();
		}
//*/

#if false		
		if( EditorGUIUtility.isProSkin ){
			ViNoEditorUtil.BeginGUIColor( GUI.color , Color.green , GUI.contentColor );
		}
		else{
//			ViNoEditorUtil.BeginGUIColor( Color.white , new Color( 0f , 0.8f , 0f , 1f ) , Color.black );
			ViNoEditorUtil.BeginGUIColor( Color.white , new Color( 0.8f , 0.8f , 0.8f , 1f ) , Color.black );
		}
#endif

		m_ScrollPos = EditorGUILayout.BeginScrollView( m_ScrollPos );

#if false
			EditorGUILayout.BeginHorizontal();
//			GUILayout.Box( m_DialogIconTex , GUILayout.Width( 44f ) , GUILayout.Height( 44f ) );
				current = EditorGUILayout.ObjectField( "Target" , current , typeof( DialogPartNode ) , true ) as DialogPartNode;
//			current = EditorGUILayout.ObjectField( m_DialogIconTex , current , typeof( DialogPartNode ) , true ) as DialogPartNode;
			EditorGUILayout.EndHorizontal();
/*
		if( Selection.activeGameObject != null ){
			if( current != null ){
				if( current.gameObject != Selection.activeGameObject ){
					current = Selection.activeGameObject.GetComponent<DialogPartNode>();
					UpdateGameView();
				}
			}
			else{
				current = Selection.activeGameObject.GetComponent<DialogPartNode>();
				UpdateGameView();				
			}
		}
		else{
			m_CurrentID = 0;
			current = null;
			return;
		}
//*/
		if( current != null ){
			m_DialogItemNum = current.GetMessageNum();
			GUI.enabled = true;
		}
		else{
			GUI.enabled = false;
		}

		DrawMessageControl();
#endif

		if( current != null ){
			if( currentData != null ){
				currentData.show = false;
			}

			if( current.dlgDataList.Count == 0 ){
				ShowNotificate();
			}
			else{
				if( m_CurrentID >= current.dlgDataList.Count ){
					m_CurrentID = 0;
				}
				currentData = current.dlgDataList[ m_CurrentID ];//current.GetItemAt( m_CurrentID );
				currentData.show = true;

	//			NodeGUI.DrawDialogItemBar( current , ref currentData , m_CurrentID , current.m_ViNoTextBox , current.m_NameTextBox  );
				switch( currentData.actionID ){
					case DialogPartNodeActionType.Dialog:			
	//				DrawSceneCategory( current , currentData );
	//				DrawActorCategory( current , currentData );
					DrawSoundCategory( current , currentData );
					DrawDialogCategory( current , currentData );
					break;
					
				case DialogPartNodeActionType.EnterActor:
				case DialogPartNodeActionType.ExitActor:
//				case DialogPartNodeActionType.Shake:
//				case DialogPartNodeActionType.ChangeState:
				case DialogPartNodeActionType.MoveActor:
					DrawActorCategory( current , currentData );
					break;

//				case DialogPartNodeActionType.EnterScene:
//				case DialogPartNodeActionType.ExitScene:
//					DrawSceneCategory( current , currentData );
//					break;
				}
			}
		}
		else{
			ShowNotificate();
		}
		
		// If not play mode , Change the scene  and Message .
		if( GUI.changed ){//&& ! Application.isPlaying ){
			UpdateGameView();
		}

//		EditorGUILayout.LabelField( "Dialog Item Num" + m_DialogItemNum.ToString() );
		EditorGUILayout.EndScrollView();

		GUI.enabled = true;



#if false		
		ViNoEditorUtil.EndGUIColor();	
#endif
	}



	void ShowNotificate(){
		this.ShowNotification( new GUIContent( "Please click Edit button in a DialogPartNode." ));
	}

	void UpdateGameView(){
		if( current != null ){
			if( current.dlgDataList.Count <= m_CurrentID ){
				m_CurrentID = current.dlgDataList.Count - 1;
			}
			currentData = current.dlgDataList[ m_CurrentID ];//current.GetItemAt( m_CurrentID );
			DialogPartNodeUtility.ViewDialog( currentData , current.m_ViNoTextBox , current.m_NameTextBox );
			DialogPartNodeUtility.ViewScene( currentData );
		}
	}

	static public void DrawSceneCategory( DialogPartNode node , DialogPartData unit ){
		if( EditorGUIUtility.isProSkin ){
			GUI.contentColor = Color.white;
			GUICommon.DrawLineSpace( 2f , 22f );

			EditorGUILayout.BeginHorizontal();
				GUILayout.Space( 15f );
				sceneToggle = EditorGUILayout.Foldout( sceneToggle , "Scene" );

			EditorGUILayout.EndHorizontal();
		}
		else{
			EditorGUILayout.BeginHorizontal();
				GUILayout.Space( 15f );
				sceneToggle = EditorGUILayout.Foldout( sceneToggle , "Scene" );
			EditorGUILayout.EndHorizontal();

			GUI.contentColor = Color.white;
			GUICommon.DrawLineSpace( 3f , 1f );
		}

		if( sceneToggle ){
			NodeGUI.DrawLayoutSceneField( unit );
		}
	}
	
	static public void DrawActorCategory( DialogPartNode node , DialogPartData unit ){		
		if( EditorGUIUtility.isProSkin ){
			GUI.contentColor = Color.white;
			GUICommon.DrawLineSpace( 2f , 22f );

			EditorGUILayout.BeginHorizontal();
				GUILayout.Space( 15f );
				actorToggle = EditorGUILayout.Foldout( actorToggle , "Actors" );

			EditorGUILayout.EndHorizontal();
		}
		else{
			EditorGUILayout.BeginHorizontal();
				GUILayout.Space( 15f );
				actorToggle = EditorGUILayout.Foldout( actorToggle , "Actors" );
				
			EditorGUILayout.EndHorizontal();

			GUI.contentColor = Color.white;
			GUICommon.DrawLineSpace( 3f , 1f );
		}

		if( actorToggle ){			
			switch( unit.actionID ){
				case DialogPartNodeActionType.MoveActor:
				case DialogPartNodeActionType.EnterActor:
					NodeGUI.DrawLayoutEnterActorField( unit );
				
					break;
				
//			case DialogPartNodeActionType.Shake:
			case DialogPartNodeActionType.ExitActor:
					NodeGUI.DrawLayoutExitActorField( unit );				
					break;
			
/*			case DialogPartNodeActionType.ChangeState:
					NodeGUI.DrawLayoutChangeStateActorField( unit );
					break;
//*/					
			}
		}
	}	

	static private GUIContent folderIconContent{
		get{
			return new GUIContent( ViNoEditorResources.folderIcon );
		}
	}

	static public void DrawSoundCategory( DialogPartNode node , DialogPartData unit ){
		if( EditorGUIUtility.isProSkin ){
			GUI.contentColor = Color.white;
			GUICommon.DrawLineSpace( 2f , 22f );

			EditorGUILayout.BeginHorizontal();
				GUILayout.Space( 15f );
				soundsToggle = EditorGUILayout.Foldout( soundsToggle , "Sounds" );

			EditorGUILayout.EndHorizontal();
		}
		else{
			EditorGUILayout.BeginHorizontal();
				GUILayout.Space( 15f );
				soundsToggle = EditorGUILayout.Foldout( soundsToggle , "Sounds" );
			EditorGUILayout.EndHorizontal();

			GUI.contentColor = Color.white;
			GUICommon.DrawLineSpace( 3f , 1f );
		}

		if( soundsToggle ){
			float space = 30f;
			EditorGUILayout.BeginHorizontal();
				GUILayout.Space( space );
				unit.isBGM = EditorGUILayout.Toggle( unit.isBGM , GUILayout.Width( 10f ) );
				EditorGUILayout.LabelField( "Bgm" , GUILayout.Width( 35f ) );																		
				EditorGUILayout.LabelField( folderIconContent , GUILayout.Width( 16f ) );
				if( unit.isBGM ){
					DrawBGMPopupField( node , unit );
				}

			EditorGUILayout.EndHorizontal();					
			EditorGUILayout.BeginHorizontal();
				GUILayout.Space( space );
				unit.isSE = EditorGUILayout.Toggle( unit.isSE , GUILayout.Width( 10f ) );
				EditorGUILayout.LabelField( "SE" , GUILayout.Width(35f ) );																		
				EditorGUILayout.LabelField( folderIconContent , GUILayout.Width( 16f ) );
				if( unit.isSE ){
					DrawSEPopupField( node , unit );
				}

			EditorGUILayout.EndHorizontal();					
			EditorGUILayout.BeginHorizontal();
				GUILayout.Space( space );
				unit.isVoice = EditorGUILayout.Toggle( unit.isVoice , GUILayout.Width( 10f ) );
				EditorGUILayout.LabelField( "Voice" , GUILayout.Width(35f ) );																		
				EditorGUILayout.LabelField( folderIconContent , GUILayout.Width( 16f ) );
				if( unit.isVoice ){
					DrawVoicePopupField( node , unit );
				}

			EditorGUILayout.EndHorizontal();					
		}
	}

	static public void DrawDialogCategory( DialogPartNode node , DialogPartData unit ){	
		if( EditorGUIUtility.isProSkin ){
			GUI.contentColor = Color.white;
			GUICommon.DrawLineSpace( 2f , 22f );
			EditorGUILayout.BeginHorizontal();
				GUILayout.Space( 15f );
				dialogToggle = EditorGUILayout.Foldout( dialogToggle , "Dialog" );
			EditorGUILayout.EndHorizontal();
		}
		else{
			EditorGUILayout.BeginHorizontal();
				GUILayout.Space( 15f );
				dialogToggle = EditorGUILayout.Foldout( dialogToggle , "Dialog" );
			EditorGUILayout.EndHorizontal();

			GUI.contentColor = Color.white;
			GUICommon.DrawLineSpace( 3f , 1f );
		}
		if( dialogToggle ){

			// will be Drawn TextBox and NameTextBox field...
			//  .
			// ...

			NodeGUI.DrawLayoutNameField( unit );
			NodeGUI.DrawLayoutDialogTextField( unit );
		}
	}

//	static private string[] kDummyContents = { "NO ENTRY" }; 

	static public void DrawBGMPopupField( DialogPartNode node , DialogPartData unit  ){
#if true		
		unit.bgmAudioKey =  EditorGUILayout.TextField( unit.bgmAudioKey , GUILayout.Width( 175f )  );
/*		if( DialogPartNodeInspector.bgmEntries != null ){						
			unit.bgmAudioID =  EditorGUILayout.Popup( unit.bgmAudioID , DialogPartNodeInspector.bgmEntries , GUILayout.Width( 75f )  );
			if( DialogPartNodeInspector.bgmEntries.Length > unit.bgmAudioID ){
				unit.bgmAudioKey = DialogPartNodeInspector.bgmEntries[ unit.bgmAudioID ];
			}				
		}
		// Show Dummy Contents.
		else{
			unit.bgmAudioID =  EditorGUILayout.Popup( unit.bgmAudioID , kDummyContents , GUILayout.Width( 75f )  );						
		}	
//*/			

#else
		if( node.soundData != null ){		
			if( node.soundData.bgmEntries != null ){
				string[] entries = node.soundData.GetSoundEntryNames();
				unit.bgmAudioID =  EditorGUILayout.Popup( unit.bgmAudioID , entries , GUILayout.Width( 75f )  );
				if( entries.Length > unit.bgmAudioID ){
					unit.bgmAudioKey = entries[ unit.bgmAudioID ];
				}
			}
			// Show Dummy Contents.
			else{
				unit.bgmAudioID =  EditorGUILayout.Popup( unit.bgmAudioID , kDummyContents , GUILayout.Width( 75f )  );						
			}	
		}
#endif		
	}

	static public void DrawSEPopupField( DialogPartNode node , DialogPartData unit ){
#if true		
		unit.seAudioKey =  EditorGUILayout.TextField( unit.seAudioKey , GUILayout.Width( 175f )  );
/*		
		if( DialogPartNodeInspector.seEntries != null ){			
			unit.seAudioID =  EditorGUILayout.Popup( unit.seAudioID , DialogPartNodeInspector.seEntries , GUILayout.Width( 75f )  );
			if( DialogPartNodeInspector.seEntries.Length > unit.seAudioID ){
				unit.seAudioKey = DialogPartNodeInspector.seEntries[ unit.seAudioID ];
			}				
		}
		// Show Dummy Contents.
		else{
			unit.seAudioID =  EditorGUILayout.Popup( unit.seAudioID , kDummyContents , GUILayout.Width( 75f )  );						
		}	
//*/				
#else
		if( node.soundData != null ){
			if( node.soundData.seEntries != null ){		
				string[] entries = node.soundData.GetSeEntryNames();				
				unit.seAudioID =  EditorGUILayout.Popup( unit.seAudioID , entries , GUILayout.Width( 75f )  );
				if( node.soundData.seEntries.Length > unit.seAudioID ){
					unit.seAudioKey = entries[ unit.seAudioID ];
				}				
			}
			// Show Dummy Contents.
			else{
				unit.seAudioID =  EditorGUILayout.Popup( unit.seAudioID , kDummyContents , GUILayout.Width( 75f )  );						
			}	
		}
#endif
	}

	static public void DrawVoicePopupField( DialogPartNode node , DialogPartData unit  ){
#if true		

		unit.voiceAudioKey =  EditorGUILayout.TextField( unit.voiceAudioKey , GUILayout.Width( 175f )  );
/*		
		if( DialogPartNodeInspector.voiceEntries != null ){
			unit.voiceAudioID =  EditorGUILayout.Popup( unit.voiceAudioID , DialogPartNodeInspector.voiceEntries , GUILayout.Width( 75f )  );
			if( DialogPartNodeInspector.voiceEntries.Length > unit.voiceAudioID ){
				unit.voiceAudioKey = DialogPartNodeInspector.voiceEntries[ unit.voiceAudioID ];
			}
		}
		// Show Dummy Contents.
		else{
			unit.voiceAudioID =  EditorGUILayout.Popup( unit.voiceAudioID , kDummyContents , GUILayout.Width( 75f )  );						
		}	
//		unit.voiceAudioKey = EditorGUILayout.TextField( unit.voiceAudioKey , GUI.skin.textArea, GUILayout.Width(50f ) , GUILayout.Height(20f));					
//*/
#else		
		if( node.soundData != null ){
			if( node.soundData.voiceEntries != null ){
				string[] entries = node.soundData.GetVoiceEntryNames();
				unit.voiceAudioID =  EditorGUILayout.Popup( unit.voiceAudioID , entries , GUILayout.Width( 75f )  );
				if( entries.Length > unit.voiceAudioID ){
					unit.voiceAudioKey = entries[ unit.voiceAudioID ];
				}
			}
			// Show Dummy Contents.
			else{
				unit.voiceAudioID =  EditorGUILayout.Popup( unit.voiceAudioID , kDummyContents , GUILayout.Width( 75f )  );						
			}
		}	
#endif
	}

	void DrawMessageControl(){
//		EditorGUILayout.BeginHorizontal();
			m_CurrentID = (int)EditorGUILayout.IntSlider( "Dialog_ID" , m_CurrentID , 0 , m_DialogItemNum - 1 );
//			EditorGUILayout.LabelField(  m_CurrentID.ToString() ,GUILayout.Width( 100f ) );
//		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();

			if( GUILayout.Button( "<<" , GUILayout.Height( 32f ) ) ){
//			if( GUILayout.Button( m_ArrowSeekBackTex , GUILayout.Height( 64f ) ) ){
				m_CurrentID = 0;
			}

			if( GUILayout.Button( "<" , GUILayout.Height( 32f ) ) ){
//			if( GUILayout.Button( m_ArrowBackTex , GUILayout.Height( 64f ) ) ){
				m_CurrentID--;
			}

			if( GUILayout.Button( ">" , GUILayout.Height( 32f ) ) ){
//			if( GUILayout.Button( m_ArrowForwardTex , GUILayout.Height( 64f ) ) ){
				m_CurrentID++;
			}

			if( GUILayout.Button( ">>" , GUILayout.Height( 32f ) ) ){
//			if( GUILayout.Button( m_ArrowSeekForwardTex , GUILayout.Height( 64f ) ) ){
				m_CurrentID = m_DialogItemNum - 1;
			}

			GUI.enabled = true;

			if( m_CurrentID < 0 ){
				m_CurrentID = 0;
			}
			else if( m_CurrentID >= m_DialogItemNum ){
				m_CurrentID = m_DialogItemNum - 1;
			}

		EditorGUILayout.EndHorizontal();
	}

}

