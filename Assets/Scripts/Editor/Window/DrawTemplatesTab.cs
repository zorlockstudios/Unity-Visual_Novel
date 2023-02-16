//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 		VisualNovelToolkit		/_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.

using UnityEngine;
using UnityEditor;
using System.Collections;

public class DrawTemplatesTab {
	static public int m_Selected = 0;
	static public string[] m_MenuItems = { "Scene" , "Scenario"  };//, "Samples" };
	static public bool m_HaveTextBox = true;	
			
#if true		
	static public void Draw(){				
		GUILayout.BeginHorizontal();

//			if( GUILayout.Button( "Create a Conversation Scene" , GUILayout.Width(200f) , GUILayout.Height (60f) ) ){
			if( GUILayout.Button( new GUIContent( "Conversation Scene" , ViNoEditorResources.convTemplateIcon ) , GUILayout.Width(150f) , GUILayout.Height (60f) ) ){
//				CreateTemplScene();
				CreateConvScene();
			}
		
		GUILayout.EndHorizontal();

	}
	
//	[ MenuItem( "GameObject/ViNo/Templates/CreateTemplScene" ) ]	
#endif	
	
	/// <summary>
	/// Creates the templ scene.
	/// </summary>
	/// <returns>
	///  return the Scene Root Object.
	/// </returns>
	static public GameObject CreateTemplScene( string templPrefabPath ){
		GameObject obj = AssetDatabase.LoadAssetAtPath(
				templPrefabPath , typeof( GameObject) ) as GameObject;
		
		GameObject clone = GameObject.Instantiate( obj ) as GameObject;
		ViNoGOExtensions.StripGameObjectName( clone , "(Clone)" , "" );
		return clone;
	}

// Old GUI Version.
#if false	
	[ MenuItem( "GameObject/ViNo/Templates/Create a Conversation Scene" ) ]	
	static public void CreateAConversationScene( ){
		string path = "Assets/" + ViNoToolUtil.kAssetName + "/Templates/ConversationSceneTempl.prefab";
		CreateConvScene( path );
	}
#else
	[ MenuItem( "GameObject/ViNo/Templates/Create a Conversation Scene" ) ]	
	static public void CreateConvScene( ){
		string path = "Assets/" + ViNoToolUtil.kAssetName + "/Templates/ConversationScene with SimpleUI.prefab";
		CreateConvScene( path );
//		EditorApplication.SaveScene();
	}
#endif

	static public void CreateConvScene( string templScenePath ){
		GameObject sceneRoot = CreateTemplScene( templScenePath );
		sceneRoot.name = "A Conversation Scene";

		string scenarioName = "A_Conversation";
		if( ! System.IO.Directory.Exists( "Assets/" + scenarioName )){
			AssetDatabase.CreateFolder( "Assets" , scenarioName );
		}

		GameObject scenarioObj = ViNoToolUtil.CreateANewScenario( scenarioName , true );
		GameObject startObj = scenarioObj.transform.Find( "START" ).gameObject;		
//		scenarioObj.transform.parent.transform.parent = sceneRoot.transform;		

		GameObject node0=  new GameObject( "0_Scene" );
		LoadSceneNode loadSceneNode = node0.AddComponent<LoadSceneNode>();
		loadSceneNode.sceneName = "Scene1";
		loadSceneNode.method = LoadSceneNode.Methods.DESTROY_AND_LOAD;
		loadSceneNode.withFadeIn = true;
		
		loadSceneNode.transform.parent = startObj.transform;
			
		DialogPartNode dlgNode = ViNoToolUtil.AddDialogPartNode( startObj.transform );
		dlgNode.name = "1_Dialog";
		DialogPartData data0 = dlgNode.AddData( "" , ""  );
		DialogPartData data1 = dlgNode.AddData( "Sachi" , "Hello. I am Sachi."  );
		DialogPartData data2 = dlgNode.AddData( "" , ""  );
		DialogPartData data3 = dlgNode.AddData( "Maiko" , "Hi. Sachi."  );
		DialogPartData data4 = dlgNode.AddData( "Sachi" , "Bye Bye !"  );						
		DialogPartData data5 = dlgNode.AddData( "Maiko" , "Bye Bye !"  );						
		DialogPartData data6 = dlgNode.AddData( "" , ""  );						

		data0.enterActorEntries = new DialogPartData.ActorEntry[ 1 ];
		data0.enterActorEntries[0] = new DialogPartData.ActorEntry();
		data0.enterActorEntries[0].actorName = "Sachi";
		data0.actionID = DialogPartNodeActionType.EnterActor;
		data0.enterActorEntries[0].position = ViNoToolkit.SceneEvent.ActorPosition.middle_left;

		data1.isName = true;

		data2.enterActorEntries = new DialogPartData.ActorEntry[ 1 ];
		data2.enterActorEntries[0] = new DialogPartData.ActorEntry();
		data2.enterActorEntries[0].actorName = "Maiko";
		data2.actionID = DialogPartNodeActionType.EnterActor;
		data2.enterActorEntries[0].position = ViNoToolkit.SceneEvent.ActorPosition.middle_right;
		data3.isName = true;

		data4.isName = true;
		data5.isName = true;
		
		data6.actionID = DialogPartNodeActionType.ExitActor;
		data6.exitActorEntries = new DialogPartData.ActorEntry[ 2 ];
		data6.exitActorEntries[0] = new DialogPartData.ActorEntry();
		data6.exitActorEntries[1] = new DialogPartData.ActorEntry();
		data6.exitActorEntries[0].actorName = "Sachi";
		data6.exitActorEntries[1].actorName = "Maiko";
		
#if false		
		GameObject parentObj = GameObject.Find( "Panels" );
		
		DrawObjectsTab.CreateBG( "BG" , parentObj );
		GameObject ch1 = DrawObjectsTab.Create2Layer( "Ch1" , parentObj );
		GameObject ch2 = DrawObjectsTab.Create2Layer( "Ch2" , parentObj );		
		
		ch1.transform.localPosition = new Vector3( -120f , 0f , 0f );
		ch2.transform.localPosition = new Vector3( 120f , 0f , 0f );
				
		AssetDatabase.MoveAsset( "Assets/BG" , "Assets/" + scenarioName  + "/BG" );
		AssetDatabase.MoveAsset( "Assets/Ch1" , "Assets/" + scenarioName  + "/Ch1" );
		AssetDatabase.MoveAsset( "Assets/Ch2" , "Assets/" + scenarioName  + "/Ch2" );				
#endif

		string scenarioFlagDataPath = "Assets/" + scenarioName + "/" + scenarioName + "Flags.asset";
		string scenarioSaveDataPath = "Assets/" + scenarioName + "/" + scenarioName + "SaveData.asset";	
		FlagTable flagTable = ScriptableObjectUtility.CreateScriptableObject( "FlagTable" , scenarioFlagDataPath ) as FlagTable;
		ViNoSaveInfo saveInfo = ScriptableObjectUtility.CreateScriptableObject( "ViNoSaveInfo" , scenarioSaveDataPath ) as ViNoSaveInfo;				
		ScenarioNode scenarioNode = scenarioObj.GetComponent<ScenarioNode>();		
		scenarioNode.m_PlayAtStart = true;
		scenarioNode.flagTable = flagTable;
//		scenarioNode.saveInfo = saveInfo;	

		ScenarioCtrl scenarioCtrl = GameObject.FindObjectOfType( typeof(ScenarioCtrl) ) as ScenarioCtrl;
		scenarioCtrl.quickSaveSlot = saveInfo;
		scenarioCtrl.saveInfo = saveInfo;

		// Create an ActorLibrary and a SceneLibrary.
		string actorLibPrefabPath = "Assets/" + scenarioName + "/ActorLibrary.prefab";
		string sceneLibPrefabPath = "Assets/" + scenarioName + "/SceneLibrary.prefab";

		ViNoScenarioDataUtil.CreateActorLibrary( actorLibPrefabPath );
		ViNoScenarioDataUtil.CreateSceneLibrary( sceneLibPrefabPath );
		GameObject.DestroyImmediate( GameObject.Find( "ActorLibrary") );
		GameObject.DestroyImmediate( GameObject.Find( "SceneLibrary") );

//		ViNoToolkit.ActorInfo actor1 = ViNoScenarioDataUtil.CreateActorInfo() as ViNoToolkit.ActorInfo;
		ViNoToolkit.SceneEvent sceneEvt = GameObject.FindObjectOfType( typeof( ViNoToolkit.SceneEvent ) ) as ViNoToolkit.SceneEvent;

		ViNoToolkit.ActorLibrary actorLib = AssetDatabase.LoadAssetAtPath( actorLibPrefabPath , typeof( ViNoToolkit.ActorLibrary) ) as ViNoToolkit.ActorLibrary;		
		ViNoToolkit.SceneLibrary sceneLib = AssetDatabase.LoadAssetAtPath( sceneLibPrefabPath , typeof( ViNoToolkit.SceneLibrary) ) as ViNoToolkit.SceneLibrary;		

/*		actorLib.actorEntries = new ViNoToolkit.ActorInfo[ 1 ];
		actorLib.actorEntries[ 0 ] = actor1;
//*/
		sceneEvt.actorLib = actorLib;
		sceneEvt.sceneLib = sceneLib;
	}

}
