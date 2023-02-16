//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

[ CustomEditor( typeof( ScenarioNode ) ) ] 
public class ScenarioNodeInspector : Editor {
	private string scenarioScript;
//	private SerializedProperty soundDataProp = null;

	static private void CompileAndSaveAsBinaryFile( string fileName , ScenarioNode scenario ){
		ByteCodes btcodes = scenario.Compile();
		ViNode.SaveByteCodesToFile( fileName , btcodes.GetCode() );					
	}

#if false
	void OnEnable(){
		serializedObject.FindProperty ("__dummy__");
//	    soundDataProp = serializedObject.FindProperty ("soundData");		
	}
#endif

	public override void OnInspectorGUI(){		
//		serializedObject.Update();		

		ScenarioNode scenario = target as ScenarioNode;
		
		if( Application.isPlaying ){
			// Ping Current Node in playmode.
			IScriptEngine scr = IScriptEngine.Instance;
			if( scr != null ){
				GUICommon.DrawScriptEngineCommonInspector( scr );
			}
		}
//*/		
		Color savedCol = GUI.backgroundColor;
		GUI.backgroundColor = Color.green;
		
		GUILayout.BeginHorizontal();
					
			scenario.m_PlayAtStart = EditorGUILayout.Toggle( "Play at Start" , scenario.m_PlayAtStart );//, GUILayout.Width( 15f ) );		
//			EditorGUILayout.LabelField( "Start and Play" , GUILayout.Width( 100f ) );
					
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal();
			scenario.startFromSave = EditorGUILayout.Toggle( "Start from SavePoint" , scenario.startFromSave );//, GUILayout.Width( 15f ) );		
//			EditorGUILayout.LabelField( "Start from Savepoint" , GUILayout.Width( 150f ) );
						
		GUILayout.EndHorizontal();
						
		scenario.flagTable = EditorGUILayout.ObjectField( "Flag" , scenario.flagTable , typeof( FlagTable ) , false ) as FlagTable;

//*/

//		scenario.saveInfo = EditorGUILayout.ObjectField( "SaveInfo" , scenario.saveInfo , typeof( ViNoSaveInfo ) , false ) as ViNoSaveInfo;
//		scenario.soundData = EditorGUILayout.ObjectField( "SoundData" , scenario.soundData , typeof( ScriptableSoundData ) , false ) as ScriptableSoundData;
/*		if ( soundDataProp != null){
			EditorGUILayout.PropertyField( soundDataProp );
		}		
//*/

// Dont show now.
// Adv Script Compile Option.
#if false			
		if( GUILayout.Button( "Node Names" )){
			List<string> list =	scenario.GetNodeTagsUnderMe();
			for( int i=0;i<list.Count;i++){
				Debug.Log( "Node:" + list[ i ] );
			}
		}

//		scenario.execMode = (ScenarioExecMode)EditorGUILayout.EnumPopup( "ExecLevel" , scenario.execMode );

		EditorGUILayout.LabelField( "Scenario Script" );
		scenarioScript = EditorGUILayout.TextArea( scenarioScript );
		
		GUILayout.BeginHorizontal();
		
			scenario.useCompiledScript = EditorGUILayout.Toggle( scenario.useCompiledScript );
			EditorGUILayout.LabelField( "useCompiled" , GUILayout.Width( 100f ) );

			scenario.compiledScriptFile = EditorGUILayout.ObjectField( scenario.compiledScriptFile , typeof( TextAsset ) , false ) as TextAsset;
		
		GUILayout.EndHorizontal();
		
		if( GUILayout.Button( "Compile" ) ){
			CompileAndSaveAsBinaryFile( scenario.name , scenario );	
		}
		
		GUILayout.BeginHorizontal();
	
			scenario.m_CacheCode = EditorGUILayout.Toggle( scenario.m_CacheCode , GUILayout.Width( 15f ) );		
			EditorGUILayout.LabelField( "IsCacheCode" , GUILayout.Width( 100f ) );
		
		GUILayout.EndHorizontal();
		
		GUILayout.BeginHorizontal();
		
			scenario.m_PlayOnEnable = EditorGUILayout.Toggle( scenario.m_PlayOnEnable , GUILayout.Width( 15f ) );
			EditorGUILayout.LabelField( "PlayOnEnable" , GUILayout.Width( 100f ) );

		GUILayout.EndHorizontal();

#endif		
		
/*			GUILayout.BeginHorizontal();
			
				scenario.useScenarioScript = EditorGUILayout.Toggle( "use Scenario Script" , scenario.useScenarioScript );// , GUILayout.Width( 15f ) );
//				EditorGUILayout.LabelField( "useScenarioScript" , GUILayout.Width( 100f ) );

			GUILayout.EndHorizontal();
//*/
/*			if( scenario.useScenarioScript ){
				scenario.scenarioScript = EditorGUILayout.ObjectField( "TextAsset" , scenario.scenarioScript , typeof( TextAsset ) , false  ) as TextAsset;
			}
			else{
//*/				
//		GUI.enabled = ! scenario.useScenarioScript;

				GUILayout.BeginHorizontal();
					scenario.startNode = (ViNode) EditorGUILayout.ObjectField( "START NODE" , scenario.startNode , typeof( ViNode) , true );
				GUILayout.EndHorizontal();
//			}

//		GUI.enabled = true;
		
//		GUI.enabled = Application.isPlaying;
//		NodeGUI.OnGUI_ViNode( scenario , true , false );

		if( Application.isPlaying ){
			if( GUILayout.Button( "PLAY" ) ){
				if( AssetDatabase.Contains( scenario.gameObject )  ){
					GameObject clone = Instantiate( scenario.gameObject ) as GameObject;
					ViNoGOExtensions.StripGameObjectName( clone , "(Clone)" , "" );
				}
				else{
					scenario.Play();
				}
			}			
		}
		else{

			GUILayout.BeginHorizontal();

				GUILayout.Space( 30f );

			if( AssetDatabase.Contains( scenario.gameObject )  ){
				if( GUILayout.Button( "Edit" ) ){				
					EditorUtility.DisplayDialog( "Note" , "If you finish editing in your scene , please do not forget to apply changes to the prefab" , "Ok" );
					GameObject obj = PrefabUtility.InstantiatePrefab( scenario.gameObject ) as GameObject;//AttachedAsset( scenario.gameObject );
					PrefabUtility.ReplacePrefab( obj, scenario.gameObject, ReplacePrefabOptions.ConnectToPrefab);					

					EditorGUIUtility.PingObject( obj );
				}
			}
			else{
/*				if( GUILayout.Button ( "Save as ScenarioScript" ) ){
		//			scenarioScript = KAGInterpreter._TEST_TO_KIRIKIRI_SCRIPT( scenario.gameObject );
					string text = ViNoToolkit.KAGInterpreter.ToScenarioScript( scenario.gameObject );
		//			System.Text.StringBuilder sb = new System.Text.StringBuilder();
		//			scenario.ToScenarioScript( ref sb );	
					string filePath = Application.dataPath + "/" + scenario.name + ".txt";
					Debug.Log( "Saved as a Scenario Script at :" + filePath );
					File.WriteAllText( filePath , text );
					AssetDatabase.Refresh();

		//		 	scenario.scenarioScript = AssetDatabase.LoadAssetAtPath( filePath , typeof(TextAsset) ) as TextAsset;
		//			scenarioScript = sb.ToString();
		//			Debug.Log ( sb.ToString ());
				}			
//*/				
			}

				GUILayout.Space( 30f );
			
			GUILayout.EndHorizontal();
		}

//		GUI.enabled = true;
						
		GUI.backgroundColor = savedCol;
//		serializedObject.ApplyModifiedProperties();
	}
	
}
