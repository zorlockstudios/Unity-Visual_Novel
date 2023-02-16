//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.

using UnityEngine;
using UnityEditor;
using System.Collections;

static public class ViNoScenarioDataUtil {	


	// Menu Functions.
	
	[ MenuItem( "GameObject/ViNo/Create/ActorInfo")]
	static public Object CreateActorInfo(){
		return CreateActorInfo( "Assets/An ActorInfo.asset" );
	}

	[ MenuItem( "GameObject/ViNo/Create/ActorLibrary")]
	static public ViNoToolkit.ActorLibrary CreateActorLibrary(){		
		return CreateActorLibrary( "Assets/ActorLibrary.prefab" );
	}

	[ MenuItem( "GameObject/ViNo/Create/SceneData")]
	static public void CreateScene(){
		ScriptableObjectUtility.CreateScriptableObject( "Scene" , "Assets/A Scene Data.asset" );		
	}

	[ MenuItem( "GameObject/ViNo/Create/SceneLibrary")]
	static public ViNoToolkit.SceneLibrary CreateSceneLibrary(){	
		return CreateSceneLibrary( "Assets/A SceneLibrary.prefab" );
	}

	[ MenuItem( "GameObject/ViNo/Create/SaveInfo" ) ]
	static public void CreateGameSaveData(){
		 string path  = "Assets/A SaveData.asset";
		ScriptableObjectUtility.CreateScriptableObject( "ViNoSaveInfo" , path  );
	}
	
	[ MenuItem( "GameObject/ViNo/Create/FlagTableData" ) ]
	static public void CreateFlagTableData(){
		 string path  = "Assets/A FlagTableData.asset";
		ScriptableObjectUtility.CreateScriptableObject( "FlagTable" , path  );		
	}	
	
	[ MenuItem( "GameObject/ViNo/Create/MetaResourceData" ) ]
	static public void CreateMetaResource(){
		 string path  = "Assets/A MetaResource.asset";
		ScriptableObjectUtility.CreateScriptableObject( "ResourceMetaData" , path  );
	}

#if false
	[ MenuItem( "GameObject/ViNo/Create/SoundData" ) ]
	static public void CreateSoundData(){
		 string path  = "Assets/SoundData.asset";
		ScriptableObjectUtility.CreateScriptableObject( "ScriptableSoundData" , path );
	}
#endif

	static public Object CreateActorInfo( string createPath ){
		return ScriptableObjectUtility.CreateScriptableObject( "ActorInfo" , createPath );		
	}

	static public ViNoToolkit.ActorLibrary CreateActorLibrary( string createPath ){		
		string path =  ViNoToolUtil.GetAssetDataPath() + "Templates/ActorLibrary.prefab";	
		string parentName = "";
		if( Selection.activeGameObject != null ){
			parentName = Selection.activeGameObject.name;
		}
		GameObject obj = ViNoToolbar.ImportExampleCharacter( path , parentName );

		Object prefab = PrefabUtility.CreateEmptyPrefab( createPath );
		PrefabUtility.ReplacePrefab( obj , prefab , ReplacePrefabOptions.ConnectToPrefab );		

		EditorGUIUtility.PingObject( prefab );	

		return obj.GetComponent<ViNoToolkit.ActorLibrary>();
	}

	static public ViNoToolkit.SceneLibrary CreateSceneLibrary( string createPath ){			
		string path =  ViNoToolUtil.GetAssetDataPath() + "Templates/SceneLibrary.prefab";	
		string parentName = "";
		if( Selection.activeGameObject != null ){
			parentName = Selection.activeGameObject.name;
		}
		GameObject obj = ViNoToolbar.ImportExampleCharacter( path , parentName );

		Object prefab = PrefabUtility.CreateEmptyPrefab( createPath );
		PrefabUtility.ReplacePrefab( obj , prefab , ReplacePrefabOptions.ConnectToPrefab );		

		EditorGUIUtility.PingObject( prefab );	

		return obj.GetComponent<ViNoToolkit.SceneLibrary>();
	}

}
