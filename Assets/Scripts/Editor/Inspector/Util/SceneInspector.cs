//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 		VisualNovelToolkit		/_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[ CustomEditor( typeof(Scene))]
[ System.Serializable ]
public class SceneInspector : Editor {	
	public TextAsset sceneXmlFile;

	static public void DrawSceneNodeField( SceneData.SceneNodeData data , int offset ){
		float y = 20 + offset;
		GUI.Box( new Rect( 20 , y , 200 , 200 ) , "" );
		y += 20;
			data.name = EditorGUI.TextField( new Rect( 30 , y , 200 , 20 ) , "Name" , data.name );
		y += 20;
			data.parentname = EditorGUI.TextField( new Rect( 30 , y , 200 , 20 ) , "Parent" , data.parentname );
		y += 20;
		Vector3 pos = Vector3.zero;
		pos.x = data.posX;
		pos.y = data.posY;
		pos.z = data.posZ;
		pos = EditorGUI.Vector3Field( new Rect( 30 , y , 150 , 20 ) , "position" , pos );
		data.posX = pos.x;
		data.posY = pos.y;
		data.posZ = pos.z;
	}

	public override void OnInspectorGUI(){
		Scene targetData = target as Scene;
		
		DrawDefaultInspector();

#if false		
		if( targetData.sceneNodesData == null || targetData.sceneNodesData.Length == 0 ){
			targetData.sceneNodesData = new SceneData.SceneNodeData[ 1 ];
			targetData.sceneNodesData[ 0 ] = new SceneData.SceneNodeData();
		}

		for( int i=0;i<targetData.sceneNodesData.Length;i++){
			DrawSceneNodeField( targetData.sceneNodesData[ i ] , i * 220 );
		}
#endif

#if true
		
		GUICommon.DrawLineSpace( 5f , 5f );
		GUICommon.DrawLineSpace( 5f , 5f );
#if false		
			if( GUILayout.Button( "Save as XML" ) ) {
				string xmlData =  ViNoGameSaveLoad.SerializeObject<SceneData.SceneNodeData[]>( targetData.sceneNodesData );
				ViNoGameSaveLoad.CreateXML( targetData.name + ".xml" , xmlData );
				AssetDatabase.Refresh();
				EditorUtility.FocusProjectWindow();
			}

		EditorGUILayout.BeginHorizontal();

			sceneXmlFile = EditorGUILayout.ObjectField( sceneXmlFile , typeof( TextAsset ) , false ) as TextAsset;
			GUI.enabled = ( sceneXmlFile == null ) ? false : true;

			if( GUILayout.Button( "Load XML" ) ) {
				targetData.sceneNodesData =  ViNoGameSaveLoad.DeserializeObject<SceneData.SceneNodeData[]>( sceneXmlFile.text ) as SceneData.SceneNodeData[];
				EditorUtility.SetDirty( targetData );
			}

			GUI.enabled = true;
		
		EditorGUILayout.EndHorizontal();
#endif

			if( ! Application.isPlaying ){
				GUI.enabled = false;
				EditorGUILayout.HelpBox( "Save Scene : PlayMode Only. \n Save under the \"ADVScene\" objects data." , MessageType.Info );
			}

		EditorGUILayout.BeginHorizontal();
			
//			GUILayout.Space( 20f );
		
			
			if( GUILayout.Button( "Save Scene" , GUILayout.Height( 44f ) )){			
//				if( EditorUtility.DisplayDialog( "Warning" , "Do you really want to overwrite ?" , "ok" , "cancel" ) ){
					ViNoSceneManager.FindInstance();
					ViNoSceneManager.Instance.SaveSceneNodes();
					targetData.sceneNodesData= ViNoSceneManager.Instance.GetSceneData().m_DataArray;
//				}
//				Application.CaptureScreenshot( "Assets/A Scene.png" );
			}
	
			GUI.enabled = true;					
		
			if( GUILayout.Button( "Load Scene" , GUILayout.Height( 44f ) )){				
//				if( EditorUtility.DisplayDialog( "Warning" , "Do you really want to apply ?" , "ok" , "cancel" ) ){
					Undo.RegisterSceneUndo("Load Scene");
					SceneCreator.Create( targetData );
//				}
			}

//			GUILayout.Space( 20f );

		EditorGUILayout.EndHorizontal();

		EditorGUILayout.HelpBox( "Clear under the \"ADVScene\" objects" , MessageType.Info );

		if( GUILayout.Button( "Clear Scene" , GUILayout.Height( 44f ) )){			
			ViNoSceneManager sm = GameObject.FindObjectOfType(typeof(ViNoSceneManager)) as ViNoSceneManager;
			SceneCreator.DestroySceneImmediate( sm.theSavedPanel );
		}

#endif
		
	}
}

			
/*		if( targetData.screenShot != null ){
//			float aspect = targetData.screenShot.width/ targetData.screenShot.height;
			rect.width = targetData.screenShot.width;// / 10f;
			rect.height =targetData.screenShot.height;// / 10f;

			EditorGUI.DrawPreviewTexture( rect , targetData.screenShot );
		}
//*/
/*
		float offsetY = 0f;
		float boxHeight = 50f;
		for( int i=0;i<targetData.sceneNodesData.Length;i++){
			SceneData.SceneNodeData data = targetData.sceneNodesData[ i ];
			GUI.Box( new Rect( 20f , 20f + offsetY , 250f , boxHeight ) , data.name );

			Texture2D image = null;
			if( ! string.IsNullOrEmpty( data.texturePath ) ){
				image = AssetDatabase.LoadAssetAtPath( "Assets/VisualNovelToolkit/Examples/Resources/BG/bedroom.png" , typeof(Texture2D) ) as Texture2D; //data.texturePath );
			}

			GUI.Box( new Rect( 25 , 25 + offsetY , 128f ) , image );

			data.name = EditorGUI.TextField( new Rect( 150f , 40f + offsetY , 100f , 25f ) , "name" , data.name );
			data.parentname = EditorGUI.TextField( new Rect( 40f , 40f + offsetY , 100f , 25f ) , data.parentname );

			offsetY += boxHeight;
		}
//*/		

/*		
		EditorGUILayout.LabelField ( "Drop a Scene Xml Data" );
		EditorGUILayout.BeginHorizontal();
			sceneXmlFile = EditorGUILayout.ObjectField( sceneXmlFile , typeof( TextAsset ) , false ) as TextAsset;
			if( sceneXmlFile == null ){
				GUI.enabled = false;
			}
				
			if( GUILayout.Button( "Load Scene XML" )){
				ViNoSceneManager.FindInstance();	

				ViNoGOExtensions.DestroyImmediateChildren( "SavedLayer" );
	
				ViNoSceneSaveUtil.LoadSceneXMLFromTextAsset( sceneXmlFile );
			}
		
		EditorGUILayout.EndHorizontal();

		GUI.enabled = true;
							
			if( GUILayout.Button( "Load Scene" )){
				ViNoSceneManager.FindInstance();
				ViNoSceneManager sm = ViNoSceneManager.Instance;
				
				ViNoGOExtensions.DestroyImmediateChildren( "SavedLayer" );
				SceneData sceneData = new SceneData();
				sceneData.m_DataArray = ( target as Scene ).sceneNodesData;
		     	sm.SetSceneData( sceneData ); 			
				sm.SetNodeData( sceneData.m_DataArray );
				sm.CreateSceneNodes();							
			}
//*/
