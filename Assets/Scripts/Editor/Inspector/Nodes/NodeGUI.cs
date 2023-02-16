//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using ViNoToolkit;

/// <summary>
/// Draw NodeGUI .
/// </summary>
static public class NodeGUI{

//	static private int k_TextDispNum = 25;
	
	static public void OnGUISelectionUnit( SelectionsNode.SelectUnit unit ){
		EditorGUILayout.BeginHorizontal();

			EditorGUILayout.LabelField( "target");
			unit.targetNode = EditorGUILayout.ObjectField( unit.targetNode , typeof(ViNode) , true ) as ViNode;

		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();

			EditorGUILayout.LabelField( "Text");
			unit.text = EditorGUILayout.TextField( unit.text );

		EditorGUILayout.EndHorizontal();
	}

#if false
	static public void OnGUISelectionNode1Unit( SelectionsNode1.SelectUnit unit ){
		EditorGUILayout.BeginHorizontal();

			EditorGUILayout.LabelField( "target");
			unit.targetNodeName = EditorGUILayout.TextField( unit.targetNodeName );

		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();

			EditorGUILayout.LabelField( "Text");
			unit.text = EditorGUILayout.TextField( unit.text );

		EditorGUILayout.EndHorizontal();
	}		
#endif
	
	static public void OnGUI_ViNode( ViNode node , bool drawChildList , bool showNextNode ){		
		GUICommon.DrawLineSpace( 7f , 7f );
		
		bool hasViNode = false;
		if( drawChildList ){
			
			Color savedCol = GUI.color;
			GUI.color = Color.green;
			
//			node.startAndActive = EditorGUILayout.Toggle( "Start and Active" , node.startAndActive  );
//			EditorGUILayout.LabelField( "ViNode List" , GUILayout.Width( 200f ) );
			
			int childCount= node.transform.childCount;
			for( int i=0;i<childCount;i++){			
				Transform childTra = node.transform.GetChild( i );
			 	ViNode vinode = childTra.GetComponent<ViNode>();
				if( vinode != null ){					
					hasViNode = true;					
					Undo.RegisterUndo( childTra.gameObject , "active" + childTra.name );				

					EditorGUILayout.BeginHorizontal();						
//						bool t = EditorGUILayout.Toggle( childTra.gameObject.activeInHierarchy , GUILayout.Width( 10f) );

						// Toggle active.
						if( GUILayout.Button( i.ToString () , GUILayout.Width ( 20f ) ) ){
							childTra.gameObject.SetActive( ! childTra.gameObject.activeInHierarchy  );
						}
// Need to be fixed.						
#if false
						if( GUILayout.Button( "up" , GUILayout.Width ( 35f ) ) ){
							childTra.name = childTra.name.Replace( i + "_" , ( i + 1 ) + "_" );
							Transform parantTra = childTra.parent;
							childTra.parent = null;
							childTra.parent = parantTra;

							Transform childTra2 = node.transform.GetChild( i + 1 );	
							childTra2.name = childTra2.name.Replace( ( i + 1 ) + "_" , i + "_" );
							parantTra = childTra2.parent;
							childTra2.parent = null;
							childTra2.parent = parantTra;
						}

						if( GUILayout.Button( "dwn" , GUILayout.Width ( 35f ) ) ){
							childTra.name = childTra.name.Replace( i + "_" , ( i - 1 )  + "_" );

							Transform childTra2 = node.transform.GetChild( i - 1 );	
							childTra2.name = childTra2.name.Replace( ( i - 1 ) + "_" , i + "_" );
							Transform parantTra = childTra2.parent;
							childTra2.parent = null;
							childTra2.parent = parantTra;
						}
#endif
						GUI.enabled = childTra.gameObject.activeInHierarchy;

						if( GUILayout.Button ( childTra.name ) ){
							EditorGUIUtility.PingObject( childTra.gameObject );
							if( Application.isPlaying ){
								VM.Instance.GoToLabel( vinode.GetNodeLabel() );
							}
						}								
					
						GUI.enabled = true;

					EditorGUILayout.EndHorizontal ();
				}
			}		
			GUI.color = savedCol;			
		}
					
		if( hasViNode ){
			EditorGUILayout.LabelField( "When execution order is not right in Children," );
			EditorGUILayout.LabelField( "Please push RefreshChildren button to fix." );
			
			EditorGUILayout.BeginHorizontal();
			
				if( GUILayout.Button( "RefreshChildren" ) ){
					node.RefreshChildren();
				}	
				
				if( GUILayout.Button( "ReIndexChildren" ) ){
					ViNode[] childNodes = node.GetComponentsInChildren<ViNode>();
					
					Undo.RegisterUndo( childNodes , "ReIndexChildren" );				
					
					node.ReIndexChildren ();
				}	
			
			EditorGUILayout.EndHorizontal();			
		}		

		GUICommon.DrawLineSpace( 5f , 5f );			
	}

	static public void DrawItemBarBackground(){
		if( EditorGUIUtility.isProSkin ){
			GUI.contentColor = Color.white;
			GUICommon.DrawLineSpace( 2f , 22f );
		}
		else{
			GUI.contentColor = Color.white;
			GUICommon.DrawLineSpace( 3f , 1f );
		}
	}

	// ViewMode 0 : Actions 1 : Edit Text.
	static public void DrawDialogItemBar( DialogPartNode node , ref DialogPartData unit , int index ,
												 ViNoTextBox textBox , ViNoTextBox nameTextBox ){		
		
#if true		
			DrawItemBarBackground();
#else
			EditorGUIUtility.LookLikeInspector();
#endif

		EditorGUILayout.BeginHorizontal();						
			unit.active = EditorGUILayout.Toggle( unit.active , GUILayout.Width( 10f ) );
				
#if true
			EditorGUILayout.LabelField( "ID_" + unit.dialogID , GUILayout.Width( 42f ) );
//*/
			unit.actionID = (DialogPartNodeActionType)EditorGUILayout.EnumPopup( unit.actionID , GUILayout.Width( 100f ) );

			if( node != null ){
				
//			if( unit.actionID != DialogPartNodeActionType.ClearScene ){
				if( GUILayout.Button( "Edit" , GUILayout.Width( 40f ) ) ){
					DialogItemInspector browser = EditorWindow.GetWindow( typeof(DialogItemInspector) ) as DialogItemInspector;
					browser.InitWith( node , index );
				}
//			}
		}

			switch( unit.actionID){
				case DialogPartNodeActionType.Dialog:
//					unit.show = EditorGUILayout.Foldout( unit.show , "" );					
					break;
			}

/*			if( GUILayout.Button( "View" , GUILayout.Width( 40f ) ) ){
				DialogPartNodeUtility.ViewDialog( unit , textBox , nameTextBox  );
				DialogPartNodeUtility.ViewScene( unit );	
			}
//*/

#else			
//			EditorGUILayout.LabelField( "ID_" + unit.dialogID , GUILayout.Width( 42f ) );
#endif
/////////////////////////
#if true

				if( GUILayout.Button( "+" , GUILayout.Width( 25f ) ) ){
					node.AddItemAt( index + 1 );
					node.ReAssignDialogIDsInThisObject();
				}

				if( GUILayout.Button( "-" , GUILayout.Width( 25f ) ) ){
					if( EditorUtility.DisplayDialog( "Remove Item at ID_" + index.ToString() + " ?" , "Are you sure you really want to remove?"
						, "Yes", "Cancel" ) ){
						node.RemoveItemAt( index );
						node.ReAssignDialogIDsInThisObject();
					}
				}
#else
//			unit.show = EditorGUILayout.Foldout( unit.show , "_" );					

		EditorGUILayout.BeginHorizontal();			
			GUILayout.Space( 15f );
			unit.isName = EditorGUILayout.Toggle(  unit.isName , GUILayout.Width( 10f ) );
			EditorGUILayout.LabelField( "Name" , GUILayout.Width(40f ) , GUILayout.Height( 20f ) );
			if( unit.isName ){
				unit.nameText = EditorGUILayout.TextField( unit.nameText , GUI.skin.textArea, GUILayout.Width(75f ) , GUILayout.Height(20f));
			}

			unit.dialogText = EditorGUILayout.TextArea( unit.dialogText , GUI.skin.textArea );

			DrawBGMPopupField( node ,  unit );
			DrawSEPopupField( node ,  unit );
			DrawVoicePopupField( node ,  unit );

		EditorGUILayout.EndHorizontal();								

#endif			

		EditorGUILayout.EndHorizontal();	
	}
	
	static private int k_PositionEntryNum = 5;		
	
	static public void DrawEnterActorActionsView(  DialogPartNode node , ref DialogPartData unit ){
		if( unit.enterActorEntries == null ||  unit.enterActorEntries.Length == 0 ){
			unit.enterActorEntries = new DialogPartData.ActorEntry[ 1 ];
			unit.enterActorEntries[ 0 ] = new DialogPartData.ActorEntry();
		}
	
		EditorGUILayout.BeginHorizontal();			
			GUI.enabled = false;
	
			GUILayout.Space( 15f );
	
			string[] label = new string[ k_PositionEntryNum ];
			int enterActorNum = unit.enterActorEntries.Length;
			for( int i=0;i<enterActorNum;i++){				
				int posIndex = 1;
				switch( unit.enterActorEntries[ i ].position  ){
					case ViNoToolkit.SceneEvent.ActorPosition.left:				posIndex = 0;	break;
					case ViNoToolkit.SceneEvent.ActorPosition.middle_left:		posIndex = 1;	break;
					case ViNoToolkit.SceneEvent.ActorPosition.center:			posIndex = 2;	break;
					case ViNoToolkit.SceneEvent.ActorPosition.middle_right:		posIndex = 3;	break;
					case ViNoToolkit.SceneEvent.ActorPosition.right:			posIndex = 4;	break;
				}
				label[ posIndex ] = unit.enterActorEntries[ i ].actorName;
			}
	
			for( int i=0;i<k_PositionEntryNum;i++){
				if( string.IsNullOrEmpty( label[ i ] ) ){
					label[ i ] = "";
				}
				GUILayout.Button( label[ i ] );//, GUILayout.Width( 100f ) );		
			}					
			GUI.enabled = true;
	
		EditorGUILayout.EndHorizontal();							
	}
	
	static public void DrawActionViewMode( DialogPartNodeActionType action ,  DialogPartNode node , ref DialogPartData unit , int index , ViNoTextBox textBox , ViNoTextBox nameTextBox ){		
		switch( action ){
			case DialogPartNodeActionType.EnterActor:
				DrawEnterActorActionsView( node , ref unit );
				break;

			case DialogPartNodeActionType.ExitActor:
				if( unit.exitActorEntries != null  && unit.exitActorEntries.Length != 0 ){
					int actorNum = unit.exitActorEntries.Length;
				
					GUILayout.BeginHorizontal();
						GUILayout.Space( 30f );
						string label = "";
						for( int i=0;i<actorNum;i++){				
							label +=  unit.exitActorEntries[ i ].actorName;
							if( i < actorNum - 1 ){
								label += ",";
							}									
						}
						GUILayout.Label( label , GUILayout.Width( 100f ) );
				
					GUILayout.EndHorizontal();
			}
			break;
			
/*			case DialogPartNodeActionType.Selections:
				if( unit.selection == null ){
					unit.selection = new SelectionsNode1.SelectUnit();
				}
				OnGUISelectionNode1Unit( unit.selection );
				break;

			case DialogPartNodeActionType.Scene:
				DrawLayoutSceneField( unit );
//				break;
//*/

			case DialogPartNodeActionType.Dialog:
				EditorGUILayout.BeginHorizontal();			
				GUILayout.Space( 15f );

				GUILayout.Box( ViNoEditorResources.dialogIcon , GUILayout.Width( 30f ) ,  GUILayout.Height( 25f ) );
	
/*				string subStr = ( unit.dialogText.Length > k_TextDispNum )?  unit.dialogText.Substring( 0  , k_TextDispNum ) : unit.dialogText;
				subStr += "...";
				subStr = subStr.Replace( "\n" , "" );
//*/			
//				unit.nameText  = unit.nameText .Replace ( "\n" , "" );
//				unit.show = EditorGUILayout.Foldout( unit.show  ,"ID_" + unit.dialogID + ":" + unit.nameText + " < " + subStr );
				if( unit.isName ){
					if( string.IsNullOrEmpty( unit.nameText) ){
//									unit.show = EditorGUILayout.Foldout( unit.show , subStr );	
						EditorGUILayout.LabelField( unit.dialogText );// subStr );
					}
					else{
//									unit.show = EditorGUILayout.Foldout( unit.show , unit.nameText + " : " + subStr );
						EditorGUILayout.LabelField( unit.nameText + " : " + unit.dialogText ); //subStr );
					}
				}
				else{
//								unit.show = EditorGUILayout.Foldout( unit.show , subStr );					
					EditorGUILayout.LabelField( unit.dialogText );//unit.nameText + " : " + subStr );
				}

				unit.isClearMessageAfter = EditorGUILayout.Toggle( unit.isClearMessageAfter ,  GUILayout.Width( 10f ) );
				EditorGUILayout.LabelField( "ClearMessage" , GUILayout.Width( 80f ) , GUILayout.Height( 20f ) );

			EditorGUILayout.EndHorizontal();	

/*			EditorGUILayout.BeginHorizontal();		
				GUILayout.Space( 30f );

			EditorGUILayout.EndHorizontal();					
//*/
			break;
		}
	}

	static public void DrawEditTextViewMode( DialogPartNodeActionType action , DialogPartNode node , ref DialogPartData unit , int index , ViNoTextBox textBox , ViNoTextBox nameTextBox ){
//		EditorGUILayout.LabelField( unit.actionName );
		switch( action ){
			case DialogPartNodeActionType.Dialog:
				DrawLayoutNameField( unit );
				DrawLayoutDialogTextField( unit );
				break;
			
			default:

				DrawItemBarBackground();

				EditorGUILayout.BeginHorizontal();

					GUILayout.Space( 15f );
	//				EditorGUILayout.SelectableLabel( action.ToString() );
					EditorGUILayout.LabelField( "[" + action.ToString() + "]" );

				EditorGUILayout.EndHorizontal();
				break;
		}
	}

	static public void OnGUI_a( DialogPartNode node , ref DialogPartData unit , int index , ViNoTextBox textBox , ViNoTextBox nameTextBox , int viewMode ){
			if( ! EditorGUIUtility.isProSkin ){
				GUICommon.DrawLineSpace( 0f , 1f );
			}

			switch( viewMode ){
				case 0 : // Actions.				
					DrawActionViewMode( unit.actionID , node , ref unit , index , textBox , nameTextBox );
					break;

				case 1: // Edit Text.
					DrawEditTextViewMode( unit.actionID , node , ref unit , index , textBox , nameTextBox );
					break;

//				case 2:	// XML.
//					DialogPartNodeInspector.DrawXMLImportField( node );
//					break;
			}
/*						
		case 1:
			
			EditorGUILayout.BeginHorizontal();
				EditorGUILayout.LabelField ( "DIALOG_ID:" + unit.dialogID.ToString () , GUILayout.Width( 100f) );
				unit.dialogText = EditorGUILayout.TextArea( unit.dialogText , GUI.skin.textArea );			
			
			EditorGUILayout.EndHorizontal();
			
			break;
		}
//*/		
	}

// TEST !!!!
/*	static private string[] m_ActorPopup;
	static private string[] actorPopup{
		get{
			if( m_ActorPopup == null ){
				List<string> actorNames = new List<string>();
				ActorLibrary actorLib = GameObject.FindObjectOfType( typeof(ActorLibrary) ) as ActorLibrary;
				for(int i=0;i<actorLib.actorEntries.Length;i++){
					actorNames.Add( actorLib.actorEntries[ i ].actorName );
				}
				m_ActorPopup = actorNames.ToArray();
			}			
			return m_ActorPopup;
		}
	}
//*/

	static public void DrawLayoutEnterActorField( DialogPartData unit ){
		if( unit.enterActorEntries == null || unit.enterActorEntries.Length == 0 ){		
			unit.enterActorEntries = new DialogPartData.ActorEntry[ 1 ];	
			unit.enterActorEntries[ 0 ] = new DialogPartData.ActorEntry();
		}

		EditorGUILayout.BeginHorizontal();
			GUILayout.Space( 30f );
			EditorGUILayout.LabelField( "ActorName" , GUILayout.Width( 150f ) );	
			EditorGUILayout.LabelField( "Position" , GUILayout.Width( 100f )  );	
			EditorGUILayout.LabelField( "WithFade" , GUILayout.Width( 70f ) );	

		EditorGUILayout.EndHorizontal();					

		for( int i=0;i<unit.enterActorEntries.Length;i++){
			EditorGUILayout.BeginHorizontal();
				GUILayout.Space( 30f );

#if true		
				unit.enterActorEntries[ i ].actorName = EditorGUILayout.TextField( unit.enterActorEntries[ i ].actorName , GUILayout.Width( 150f ) );
#else
				int actorID = 0;
				actorID = EditorGUILayout.Popup( actorID , actorPopup , GUILayout.Width( 150f ) );
#endif
				unit.enterActorEntries[ i ].position = (ViNoToolkit.SceneEvent.ActorPosition)EditorGUILayout.EnumPopup( unit.enterActorEntries[ i ].position , GUILayout.Width( 100f )  );
				unit.enterActorEntries[ i ].withFade = EditorGUILayout.Toggle( unit.enterActorEntries[ i ].withFade , GUILayout.Width( 70f ) );

				if( GUILayout.Button( "+" ) ){
					ArrayUtility.Insert<DialogPartData.ActorEntry>( ref unit.enterActorEntries , i + 1 , new DialogPartData.ActorEntry() );
				}	
				if( GUILayout.Button( "-" ) ){
					ArrayUtility.RemoveAt<DialogPartData.ActorEntry>( ref  unit.enterActorEntries , i );
				}
			
			EditorGUILayout.EndHorizontal();					
		}		
	}

	static public void DrawLayoutExitActorField( DialogPartData unit ){
		if( unit.exitActorEntries == null || unit.exitActorEntries.Length == 0 ){		
			unit.exitActorEntries = new DialogPartData.ActorEntry[ 1 ];	
			unit.exitActorEntries[ 0 ] = new DialogPartData.ActorEntry();
		}

		EditorGUILayout.BeginHorizontal();
			GUILayout.Space( 30f );
			EditorGUILayout.LabelField( "ActorName" , GUILayout.Width( 150f ) );	
			EditorGUILayout.LabelField( "WithFade" , GUILayout.Width( 100f ) );	

		EditorGUILayout.EndHorizontal();					
		
		for( int i=0;i<unit.exitActorEntries.Length;i++){
			EditorGUILayout.BeginHorizontal();
				GUILayout.Space( 30f );
		
				unit.exitActorEntries[ i ].actorName = EditorGUILayout.TextField( unit.exitActorEntries[ i ].actorName , GUILayout.Width( 150f ) );				
				unit.exitActorEntries[ i ].withFade = EditorGUILayout.Toggle( unit.exitActorEntries[ i ].withFade , GUILayout.Width( 100f ) );	
			
				if( GUILayout.Button( "+" ) ){
					ArrayUtility.Insert<DialogPartData.ActorEntry>( ref unit.exitActorEntries , i + 1 , new DialogPartData.ActorEntry() );
				}	
				if( GUILayout.Button( "-" ) ){
					ArrayUtility.RemoveAt<DialogPartData.ActorEntry>( ref  unit.exitActorEntries , i );
				}
			
			EditorGUILayout.EndHorizontal();					
		}		
	}
	
	static public void DrawLayoutChangeStateActorField( DialogPartData unit ){
		if( unit.exitActorEntries == null || unit.exitActorEntries.Length == 0 ){		
			unit.exitActorEntries = new DialogPartData.ActorEntry[ 1 ];	
			unit.exitActorEntries[ 0 ] = new DialogPartData.ActorEntry();
		}

		EditorGUILayout.BeginHorizontal();
			GUILayout.Space( 30f );
			EditorGUILayout.LabelField( "ActorName" , GUILayout.Width( 150f ) );	
			EditorGUILayout.LabelField( "State" , GUILayout.Width( 150f )  );	
			EditorGUILayout.LabelField( "WithFade" , GUILayout.Width( 100f ) );	

		EditorGUILayout.EndHorizontal();					

		for( int i=0;i<unit.exitActorEntries.Length;i++){			
			EditorGUILayout.BeginHorizontal();
				GUILayout.Space( 30f );
		
				unit.exitActorEntries[ i ].actorName = EditorGUILayout.TextField( unit.exitActorEntries[ i ].actorName , GUILayout.Width( 150f ) );				
				unit.exitActorEntries[ i ].state = EditorGUILayout.TextField( unit.exitActorEntries[ i ].state , GUILayout.Width( 150f ) );				
				unit.exitActorEntries[ i ].withFade = EditorGUILayout.Toggle( unit.exitActorEntries[ i ].withFade , GUILayout.Width( 100f ) );	
							
				if( GUILayout.Button( "+" , GUILayout.Width( 20f ) ) ){
					ArrayUtility.Insert<DialogPartData.ActorEntry>( ref unit.exitActorEntries , i + 1 , new DialogPartData.ActorEntry() );
				}	
				if( GUILayout.Button( "-" , GUILayout.Width( 20f ) ) ){
					ArrayUtility.RemoveAt<DialogPartData.ActorEntry>( ref  unit.exitActorEntries , i );
				}
			
			EditorGUILayout.EndHorizontal();					
		}		
	}
	
	static public void DrawLayoutSceneField( DialogPartData unit ){
#if false		
		EditorGUILayout.BeginHorizontal();
			GUILayout.Space( 30f );
			unit.isClearScene = EditorGUILayout.Toggle(  unit.isClearScene , GUILayout.Width( 10f ) );
			EditorGUILayout.LabelField( "ClearScene?" , GUILayout.Width( 75f ) );
		EditorGUILayout.EndHorizontal();			

		EditorGUILayout.BeginHorizontal();
			GUILayout.Space( 30f );
//					EditorGUILayout.BeginHorizontal();
//					unit.isLoadScene = EditorGUILayout.BeginToggleGroup( "Scene" , unit.isLoadScene );//, GUILayout.Width( 10f ) );
//			unit.isLoadScene = EditorGUILayout.Toggle( unit.isLoadScene , GUILayout.Width( 10f ) );
			EditorGUILayout.LabelField( "FilePath" , GUILayout.Width( 75f ) );
			EditorGUILayout.BeginHorizontal();
//				unit.Scene = EditorGUILayout.ObjectField( unit.Scene , typeof( ScriptableSceneData ) , false ) as ScriptableSceneData;
				unit.sceneFilePath = EditorGUILayout.TextField( unit.sceneFilePath );

//				DialogPartNodeUtility.RestoreSceneData( unit );
			EditorGUILayout.EndHorizontal();

		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
			GUILayout.Space( 30f );
				unit.isFadeInStart = EditorGUILayout.Toggle( unit.isFadeInStart , GUILayout.Width( 10f ) );
				EditorGUILayout.LabelField( "withFadeIn?" , GUILayout.Width( 75f ) );

		EditorGUILayout.EndHorizontal();	
#else
		if( unit.scene == null  ){		
			unit.scene = new DialogPartData.SceneEntry();	
		}

		EditorGUILayout.BeginHorizontal();
			GUILayout.Space( 30f );
			EditorGUILayout.LabelField( "SceneName" , GUILayout.Width( 150f ) );	
			EditorGUILayout.LabelField( "WithFade" , GUILayout.Width( 100f ) );	
		EditorGUILayout.EndHorizontal();					

		EditorGUILayout.BeginHorizontal();
			GUILayout.Space( 30f );
			unit.scene.sceneName = EditorGUILayout.TextField( unit.scene.sceneName , GUILayout.Width( 150f ) );				
			unit.scene.withFade = EditorGUILayout.Toggle( unit.scene.withFade , GUILayout.Width( 100f ) );							
		EditorGUILayout.EndHorizontal();					

#endif		

	}

	static public void DrawLayoutNameField( DialogPartData unit ){
		EditorGUILayout.BeginHorizontal();			
			GUILayout.Space( 30f );
			unit.isName = EditorGUILayout.Toggle(  unit.isName , GUILayout.Width( 10f ) );
			EditorGUILayout.LabelField( "Name/Title" , GUILayout.Width(80f ) , GUILayout.Height( 20f ) );
			if( unit.isName ){
				unit.nameText = EditorGUILayout.TextField( unit.nameText , GUI.skin.textArea, GUILayout.Width(200f ) , GUILayout.Height(20f));
			}
		EditorGUILayout.EndHorizontal();								
	}

	static public void DrawLayoutDialogTextField( DialogPartData unit ){
		ViNoEditorUtil.BeginGreenColor();
		
		EditorGUILayout.BeginHorizontal();				
/*					if( _DIALOG_TEX == null ){
				_DIALOG_TEX = AssetDatabase.LoadAssetAtPath("Assets/017722-dialog.png", typeof(Texture2D)) as Texture2D;
			}
			GUILayout.Box( _DIALOG_TEX , GUILayout.Width( 54f ) , GUILayout.Height( 54f ) );
//*/
			GUILayout.Space( 30f );
			if( ! unit.isClearMessageAfter  ){
				unit.dialogText = EditorGUILayout.TextArea( unit.dialogText , GUI.skin.textArea );
			}
			else{
				unit.dialogText = EditorGUILayout.TextArea( unit.dialogText , GUI.skin.textArea, GUILayout.Height(50f));						
			}

		EditorGUILayout.EndHorizontal();

		ViNoEditorUtil.EndGUIColor();
	}


#if false						
	static public void DrawAnimation( DialogPartData unit ){
		EditorGUILayout.BeginHorizontal();
			GUILayout.Space( 15f );

			unit.isAnim = EditorGUILayout.Toggle(  unit.isAnim , GUILayout.Width( 10f ) );
			EditorGUILayout.LabelField( "Animation" , GUILayout.Width( 75f ) );
			if( unit.isAnim ){
				EditorGUILayout.BeginHorizontal();
				
					if( DialogPartNodeInspector.animationEntries != null ){			
						unit.animationID =  EditorGUILayout.Popup( unit.animationID , DialogPartNodeInspector.animationEntries , GUILayout.Width( 85f )  );
						if( DialogPartNodeInspector.animationEntries != null && DialogPartNodeInspector.animationEntries.Length > 0 ){
							unit.animNameKey = DialogPartNodeInspector.animationEntries[ unit.animationID ];
						}
					}
					// Show Dummy Contents.
					else{
						unit.animationID =  EditorGUILayout.Popup( unit.animationID , kDummyContents , GUILayout.Width( 75f )  );						
					}	
//						unit.animNameKey = EditorGUILayout.TextField( unit.animNameKey );
//						unit.animationID = DialogPartNodeInspector.animManagerInstance.GetAnimationIDBy( unit.animNameKey );
					//EditorGUILayout.Popup( unit.animationID , kDummyContents , GUILayout.Width( 75f )  );
//					ViNoEditorUtil.EndGUIColor();

				EditorGUILayout.EndHorizontal();
			}

		EditorGUILayout.EndHorizontal();
	}
#endif				

}
