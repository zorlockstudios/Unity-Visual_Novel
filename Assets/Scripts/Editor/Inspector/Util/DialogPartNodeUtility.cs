//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;

static public class DialogPartNodeUtility{

	static public void FindNameTextBox( DialogPartNode node ){
		GameObject nameTextBox = GameObject.Find( "TextBox_Name");
		if( nameTextBox != null ){
			 node.m_NameTextBox = nameTextBox.GetComponent<ViNoTextBox>();
			EditorGUIUtility.PingObject( node.m_NameTextBox.gameObject );
		}
	}

	static public void FindDialogTextBox( DialogPartNode node ){
		GameObject textBox = GameObject.Find( "TextBox");
		if( textBox != null ){
			node.m_ViNoTextBox = textBox.GetComponent<ViNoTextBox>();
			EditorGUIUtility.PingObject( node.m_ViNoTextBox.gameObject );
		}
	}

	static public void ViewDialog( DialogPartData data , ViNoTextBox dialogTextBox , ViNoTextBox nameTextBox ){
		if( dialogTextBox != null ){
			dialogTextBox.SetText( data.dialogText );
		}
		if( nameTextBox != null ){
			nameTextBox.SetText( data.nameText );
		}	
	}

	static public void ViewScene( DialogPartData data ){
// TODO...		
#if false		
		ViNoSceneManager sm = GameObject.FindObjectOfType( typeof(ViNoSceneManager )) as ViNoSceneManager;
		if( sm != null ){
			GameObject advSceneRoot = sm.theSavedPanel;

			if( data.isClearScene ){
				bool destroyImmediate = true;
				ClearSceneNode.Do( advSceneRoot , destroyImmediate );
			}					

//			if( data.isLoadScene && data.Scene != null ){
			if( ! string.IsNullOrEmpty( data.sceneFilePath ) && sm != null ){
				bool TODO1 = false;
				bool TODO2 = false;
				Scene scene = Resources.Load( data.sceneFilePath , typeof(Scene ) ) as Scene;
//				LoadSceneNode.Do( advSceneRoot , data.Scene , TODO1 , TODO2 ,  true );
				LoadSceneNode.Do( advSceneRoot , scene , TODO1 , TODO2 ,  true );
			}
		}
		
// No need to warning , if user do not need Scene Save and Load function.		
//		else{
//			Debug.LogWarning( "There is no ViNoSceneManager object. Scene Save and Load will not work..." );
//		}

#endif		
	}

	static public void ImportText( DialogPartNode dlg , string text , string blockDelimiter , string[] nameDelimiter , bool overWrite ) {
		string[] delimiter = { blockDelimiter }; //{ System.Environment.NewLine + System.Environment.NewLine };
		string[] blocks = text.Split(delimiter, System.StringSplitOptions.None );

		int dlgID = 0;
		if( dlg != null ){
			if( overWrite ){
				dlg.dlgDataList.Clear();
			}
			foreach (string s in blocks) {
			  string[] temp = s.Split( nameDelimiter, System.StringSplitOptions.None );
			  if( temp.Length > 1 ){
				DialogPartData data = new DialogPartData();
				data.dialogID = dlgID;
				data.nameText = temp[ 0 ];
				data.dialogText = temp[ 1 ];
				data.isName = true;
				dlg.AddData( data );
			  }
			  else{
				  dlg.AddData( "" , s );
			  }
			  dlgID++;
			}
//			Debug.Log( "Imported Text."); 
		}
	}

	static public void Initialize( DialogPartNode dlg ){		
		dlg.dlgDataList = new List<DialogPartData>();
		dlg.dlgDataList.Add( new DialogPartData( ) );
	}

	static public void Swap2Items( DialogPartNode targetNode ){
		int idx1 = -1;
		int idx2 = -1;
		for( int i=0;i<targetNode.dlgDataList.Count;i++){
			if( targetNode.dlgDataList[ i ].active ){
				if( idx1 == -1 ){
					idx1 = i;
				}
				else if( idx2 == -1 ){
					idx2 = i;
					break;
				}
			}
		}

		if( idx1 != -1 && idx2 != -1 ){
			DialogPartData data1 = targetNode.dlgDataList[ idx1 ];
			DialogPartData data2 = targetNode.dlgDataList[ idx2 ];

			targetNode.dlgDataList[ idx1 ] = data2;
			targetNode.dlgDataList[ idx2 ] = data1;			
		}
		else{
			Debug.Log( "please check 2 items when swap items.");
		}
	}	

	static public void ExportScenarioScript( DialogPartNode targetNode ){
//		int num = targetNode.GetMessageNum();
		System.Text.StringBuilder sb = new System.Text.StringBuilder();
		targetNode.ToScenarioScript( ref sb );

//	    TextAsset text = new TextAsset();
//	    AssetDatabase.CreateAsset(text, "Assets/" + targetNode.name + ".txt" );
//		text.text = sb.ToString();
//		AssetDatabase.SaveAssets();	    

		File.WriteAllText(Application.dataPath + "/" + targetNode.name + ".txt", sb.ToString() ) ;
		AssetDatabase.Refresh();

/*		
		for( int i=0;i<num;i++){
			DialogPartData data = targetNode.GetItemAt( i );
			switch( data.actionID ){			
				case DialogPartNodeActionType.Dialog:	
					string nm = targetNode.GetNameBy( i );
					string msg = targetNode.GetMessageBy( i );
					if( ! string.IsNullOrEmpty( nm ) ){
						sb.Append( nm + ":" );
						sb.Append( System.Environment.NewLine );
					}
					sb.Append( msg );
					sb.Append( System.Environment.NewLine );
					sb.Append( System.Environment.NewLine );
					break;		

				case DialogPartNodeActionType.EnterActor:
				case DialogPartNodeActionType.ExitActor:
				case DialogPartNodeActionType.Shake:
				case DialogPartNodeActionType.ChangeState:
				case DialogPartNodeActionType.MoveActor:
					sb.Append( "[" + data.actionID + "]" );
					sb.Append( System.Environment.NewLine );
					break;
			}
		}
//*/		
		Debug.Log ( sb.ToString() );
	}

/*
	static public void TrimText( DialogPartNode node , DialogPartData data , ViNoTextBox dialogTextBox ){

		data.dialogText = data.dialogText.Replace( System.Environment.NewLine , "" );

	}

	static public void RestoreSceneData( DialogPartNode node ){
		for( int i=0;i<node.dlgDataList.Count;i++){
			RestoreSceneData( node.dlgDataList[ i ] );
		}
	}

	static public void RestoreSceneData( DialogPartData unit ){
		if( unit.Scene != null ){
			unit.sceneFileAssetPath = AssetDatabase.GetAssetPath( unit.Scene );
			if( ! string.IsNullOrEmpty( unit.sceneFileAssetPath )) {
				unit.sceneFilePath = GetSceneResourcePathFromAssetPath( unit.sceneFileAssetPath );
			}
		}
		else{
			if( ! string.IsNullOrEmpty( unit.sceneFileAssetPath ) ){
				unit.Scene = AssetDatabase.LoadAssetAtPath( unit.sceneFileAssetPath , typeof( Scene) ) as Scene;
			}
		}
	}

	static public string GetSceneResourcePathFromAssetPath( string path ){
		return "scene/" + System.IO.Path.GetFileNameWithoutExtension( path );
	}

//*/

}
