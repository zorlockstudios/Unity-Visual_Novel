//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.

using UnityEngine;
using UnityEditor;
using System.Collections;

public class DrawViNodesTab {
	static public int m_SelectedID = 0;
	static public string[] m_MenuItems = { "Dialog" , "Animation" , "Wait" , "Selections"  };
	
	static public AnimationNode animNode;
	static public DialogPartNode dialogNode;

	static private Vector2 m_ScrollPos = Vector2.zero;
	
	static private Transform GetStartTransform(){
		Transform tra = null;
		GameObject startObj = GameObject.Find( "START" );
		if( startObj != null ){
			tra = startObj.transform;
		}		
		return tra;
	}
	
	static public DialogPartNode CreateDialogNode( string nameText , string dialogText ){
		Transform parentNodeTra = ( Selection.activeGameObject != null ) ? Selection.activeGameObject.transform : null;
		
	  	DialogPartNode dlgNode = ViNoToolUtil.AddDialogPartNode( parentNodeTra );			
		dlgNode.AddData( nameText , dialogText );
		EditorGUIUtility.PingObject( dlgNode.gameObject );				
		return dlgNode;		
	}

	[ MenuItem( "GameObject/ViNo/Create/A New Scenario" ) ]
	static public void CreateANewScenario( ){
		ViNoToolbar.m_ScenarioName = "A New Scenario";		
		DrawScenarioTab.CreateANewScenario();
	}

	[ MenuItem( "GameObject/ViNo/Node/ViNode" ) ]
	static public void CreateViNodeOfMenu( ){
		Transform parentNodeTra = ( Selection.activeGameObject != null ) ? Selection.activeGameObject.transform : null;
		
		ViNode node = ViNoToolUtil.AddViNodeGameObject<ViNode>( "ViNode" , parentNodeTra );						
		EditorGUIUtility.PingObject( node.gameObject );
	}
	
	[ MenuItem( "GameObject/ViNo/Node/Animation" ) ]
	static public void CreateAnimationNodeOfMenu( ){
		Transform parentNodeTra = ( Selection.activeGameObject != null ) ? Selection.activeGameObject.transform : null;
		
		AnimationNode animNode = ViNoToolUtil.AddViNodeGameObject<AnimationNode>( "Animation" , parentNodeTra );						
		EditorGUIUtility.PingObject( animNode.gameObject );
	}

	[ MenuItem( "GameObject/ViNo/Node/DialogPart" ) ]
	static public void CreateDialogNodeOfMenu( ){
		CreateDialogNode( "name" , "text" );
	}	
	
	[ MenuItem( "GameObject/ViNo/Node/Selections" ) ]
	static public void CreateSelectionsNodeOfMenu( ){
		ISelectionsCtrl sel = GameObject.FindObjectOfType( typeof(ISelectionsCtrl) ) as ISelectionsCtrl; 
		if( sel == null ){
			DrawObjectsTab.CreateGUISelectionsCtrl();
		}

		Transform parentNodeTra = ( Selection.activeGameObject != null ) ? Selection.activeGameObject.transform : null;
		
		SelectionsNode node = ViNoToolUtil.AddViNodeGameObject<SelectionsNode>( "Selections" , parentNodeTra );								
		ViNode childNode1 = ViNoToolUtil.AddViNodeGameObject<ViNode>( "Selected1" , node.transform );						
		ViNode childNode2 = ViNoToolUtil.AddViNodeGameObject<ViNode>( "Selected2" , node.transform );						
		
		DialogPartNode dlg1 = CreateDialogNode( "name" , "Selected 1" );//ViNoToolUtil.AddViNodeGameObject<DialogPartNode>( "Dialog" , childNode1.transform );						
		dlg1.transform.parent = childNode1.transform;

		DialogPartNode dlg2 = CreateDialogNode( "name" , "Selected 2" );//ViNoToolUtil.AddViNodeGameObject<DialogPartNode>( "Dialog" , childNode2.transform );						
		dlg2.transform.parent = childNode2.transform;
		
		node.units = new SelectionsNode.SelectUnit [ 2 ];
		node.units[ 0 ] = new SelectionsNode.SelectUnit();
		node.units[ 0 ].targetNode = childNode1;
		node.units[ 0 ].text = "Selection 1";
		node.units[ 0 ].index = 0;
		
		node.units[ 1 ] = new SelectionsNode.SelectUnit();
		node.units[ 1 ].targetNode = childNode2;
		node.units[ 1 ].text = "Selection 2";
		node.units[ 1 ].index = 1;				
		
		EditorGUIUtility.PingObject( node.gameObject );
	}

	[ MenuItem( "GameObject/ViNo/Node/Jump" ) ]
	static public void CreateJumpNodeOfMenu( ){
		Transform parentNodeTra = ( Selection.activeGameObject != null ) ? Selection.activeGameObject.transform : null;
		
		JumpTargetNode node = ViNoToolUtil.AddViNodeGameObject<JumpTargetNode>( "Jump" , parentNodeTra );						
		EditorGUIUtility.PingObject( node.gameObject );
	}	
	
	[ MenuItem( "GameObject/ViNo/Node/LoadLevel" ) ]
	static public void CreateLoadLevelNodeOfMenu( ){
		Transform parentNodeTra = ( Selection.activeGameObject != null ) ? Selection.activeGameObject.transform : null;
		
		LoadLevelNode node = ViNoToolUtil.AddViNodeGameObject<LoadLevelNode>( "LoadLevel" , parentNodeTra );						
		EditorGUIUtility.PingObject( node.gameObject );
	}			
	
	[ MenuItem( "GameObject/ViNo/Node/Wait" ) ]
	static public void CreateWaitNodeOfMenu( ){
		Transform parentNodeTra = ( Selection.activeGameObject != null ) ? Selection.activeGameObject.transform : null;
		
		WaitNode node = ViNoToolUtil.AddViNodeGameObject<WaitNode>( "Wait" , parentNodeTra );						
		EditorGUIUtility.PingObject( node.gameObject );
	}			
	
	[ MenuItem( "GameObject/ViNo/Node/LoadScenario" ) ]
	static public void CreateLoadScenarioNodeOfMenu( ){
		Transform parentNodeTra = ( Selection.activeGameObject != null ) ? Selection.activeGameObject.transform : null;
		
		LoadScenarioNode node = ViNoToolUtil.AddViNodeGameObject<LoadScenarioNode>( "LoadScenario" , parentNodeTra );						
		EditorGUIUtility.PingObject( node.gameObject );
	}				
	
	static public void OnSelectionChange(){
		
	}
	
	static private T LoadViNodeTempl<T>( string nodeName ) where T : Component{
		string nodePath = "Assets/" + ViNoToolUtil.kAssetName + "/Templates/" + nodeName + ".prefab";
		GameObject obj = AssetDatabase.LoadAssetAtPath( nodePath  , typeof( GameObject) ) as GameObject;
		return obj.GetComponent<T>();				
	}

	static public void LoadViNodeTemplates( ){
		if( animNode == null ){
			animNode = LoadViNodeTempl<AnimationNode>( "AnimationNode" );			
		}			
		
		if( dialogNode == null ){
			dialogNode = LoadViNodeTempl<DialogPartNode>( "DialogPartNode" );							
			
		}
		
	}
	

	static public void Draw( ){
		LoadViNodeTemplates();

#if false
		m_SelectedID = EditorGUILayout.Popup( "Node" , m_SelectedID , m_MenuItems );		
		
		EditorGUILayout.BeginHorizontal();

			GUI.enabled = false;
			
				EditorGUILayout.ObjectField( "Add to " , Selection.activeGameObject , typeof( GameObject ) , true );
			
			GUI.enabled = true;

			if( GUILayout.Button( "Create") ){
				string nodeType = m_MenuItems[ m_SelectedID ];
				switch( nodeType ){
					case "Dialog":		CreateDialogNodeOfMenu();		break;
					case "Animation":	CreateAnimationNodeOfMenu();	break;
					case "Wait":		CreateWaitNodeOfMenu();			break;
					case "Selections":	CreateSelectionsNodeOfMenu();	break;
				}
			}

		EditorGUILayout.EndHorizontal();

#else		
			GUI.enabled = false;
			
				EditorGUILayout.ObjectField( "Add under " , Selection.activeGameObject , typeof( GameObject ) , true );
			
			GUI.enabled = true;

		m_ScrollPos = EditorGUILayout.BeginScrollView( m_ScrollPos );

		EditorGUILayout.BeginHorizontal();
		
		GUILayout.Space ( 20f );
			float buttonHeight = 44f;

			if( GUILayout.Button( new GUIContent( "Dialog" , ViNoEditorResources.dialogIcon ) , GUILayout.Width(100f) , GUILayout.Height (buttonHeight) ) ){ 
//			if( GUILayout.Button( "Dialog" , GUILayout.Width(100f) ) ){//, GUILayout.Height (buttonHeight) ) ){ 
				CreateDialogNodeOfMenu();
			}

/*			if( GUILayout.Button( "ViNode" , GUILayout.Width(100f) , GUILayout.Height (buttonHeight) ) ){						
				CreateViNodeOfMenu();
			}		
//*/		
//			if( GUILayout.Button( "Animation" , GUILayout.Width(100f) ) ){//} , GUILayout.Height (buttonHeight) ) ){								
			if( GUILayout.Button( new GUIContent( "Animation" , ViNoEditorResources.animationIcon ) , GUILayout.Width(100f) , GUILayout.Height (buttonHeight) ) ){//} , GUILayout.Height (buttonHeight) ) ){						
				CreateAnimationNodeOfMenu();
			}		
//*/
			if( GUILayout.Button( new GUIContent( "Selections" , ViNoEditorResources.selectionsIcon ) , GUILayout.Width(100f) , GUILayout.Height (buttonHeight) ) ){//} , GUILayout.Height (buttonHeight) ) ){						
				CreateSelectionsNodeOfMenu();
			}		

/*
//			if( GUILayout.Button( "Jump" , GUILayout.Width(100f) , GUILayout.Height (buttonHeight) ) ){						
			if( GUILayout.Button( new GUIContent( "Jump" , ViNoEditorResources.jumpIcon ) , GUILayout.Width(100f) , GUILayout.Height (buttonHeight) ) ){//} , GUILayout.Height (buttonHeight) ) ){						
				CreateJumpNodeOfMenu();
			}

			if( GUILayout.Button( new GUIContent( "Branch" , ViNoEditorResources.branchIcon ) , GUILayout.Width(100f) , GUILayout.Height (buttonHeight) ) ){//} , GUILayout.Height (buttonHeight) ) ){						
//				CreateSelectionsNodeOfMenu();
			}		
//*/
/*		
			if( GUILayout.Button( "Wait" , GUILayout.Width(100f) ) , GUILayout.Height (buttonHeight) ) ){
				CreateWaitNodeOfMenu();
			}		
							
		
			if( GUILayout.Button( "Selections" , GUILayout.Width(100f) , GUILayout.Height (buttonHeight) ) ){
				CreateSelectionsNodeOfMenu();
			}			

			if( GUILayout.Button( "LoadLevel" , GUILayout.Width(100f) , GUILayout.Height (buttonHeight) ) ){
				CreateLoadLevelNodeOfMenu();
			}				
//*/		
/*			if( GUILayout.Button( "PlaySound" , GUILayout.Width(100f) , GUILayout.Height (buttonHeight) ) ){
				Transform parentNodeTra = Selection.activeGameObject.transform
				ViNoToolUtil.AddViNodeGameObject<PlaySoundNode>( "PlaySound" , parentNodeTra );																	
			}

			if( GUILayout.Button( "FadeOutSound" , GUILayout.Width(100f) , GUILayout.Height (buttonHeight) ) ){
				Transform parentNodeTra = ViNoToolbar.m_AddedObject.transform;		
				ViNoToolUtil.AddViNodeGameObject<FadeOutSoundNode>( "FadeOutSound" , parentNodeTra );																	
			}
//*/		
		EditorGUILayout.EndHorizontal();		

		EditorGUILayout.EndScrollView();

/*		
		EditorGUILayout.BeginHorizontal();
		
			if( GUILayout.Button( "Activate" , GUILayout.Width(100f) , GUILayout.Height (buttonHeight) ) ){
				Transform parentNodeTra = ViNoToolbar.m_AddedObject.transform;		
				ViNoToolUtil.AddViNodeGameObject<GameObjectUtilNode>( "Activate" , parentNodeTra );																	
			}
		
		EditorGUILayout.EndHorizontal();		
//*/
		
#endif
		
	}
}
