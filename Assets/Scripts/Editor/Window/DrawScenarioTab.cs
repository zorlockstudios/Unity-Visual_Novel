//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.

using UnityEngine;
using UnityEditor;
using System.Collections;

public class DrawScenarioTab  {
	
	static public void CreateANewScenario( ){
		ScenarioNode[] scenariosInScene = GameObject.FindObjectsOfType( typeof(ScenarioNode) ) as ScenarioNode[];
		if( scenariosInScene.Length > 0 ){

		}

		for(int i=0;i<scenariosInScene.Length;i++){
			scenariosInScene[ i ].gameObject.SetActive( false );			
		}


		if( ! System.IO.Directory.Exists( "Assets/" + ViNoToolbar.m_ScenarioName ) ){
			AssetDatabase.CreateFolder( "Assets" , ViNoToolbar.m_ScenarioName );
		}
			
		GameObject scenarioObj = ViNoToolUtil.CreateANewScenario( ViNoToolbar.m_ScenarioName , ViNoToolbar.m_StartAndPlay );			
		GameObject startObj = scenarioObj.transform.Find( "START" ).gameObject;
			
		EditorGUIUtility.PingObject( startObj  );
		
		Selection.activeObject = startObj;
		
		ViNoToolbar.m_AddedObject = startObj;			
		ViNoToolbar.m_Selected = 1;		// Jump to Nodes Tab.		
		
		ViNoToolbar.m_CurrScenarioNode = scenarioObj.GetComponent<ScenarioNode>();
		if( ViNoToolbar.m_CurrScenarioNode  == null ){
			Debug.Log ( "CurrScenario NULL" );
		}
	
		string scenarioFlagDataPath = "Assets/" + ViNoToolbar.m_ScenarioName + "/" + ViNoToolbar.m_ScenarioName + "Flags.asset";
		string scenarioSaveDataPath = "Assets/" + ViNoToolbar.m_ScenarioName + "/" + ViNoToolbar.m_ScenarioName + "SaveData.asset";	
		FlagTable flagTable = ScriptableObjectUtility.CreateScriptableObject( "FlagTable" , scenarioFlagDataPath ) as FlagTable;
		ScriptableObjectUtility.CreateScriptableObject( "ViNoSaveInfo" , scenarioSaveDataPath );				
		ViNoToolbar.m_CurrScenarioNode.flagTable = flagTable;
		
		ViNoToolbar.m_CurrScenarioNode.startNode = startObj.GetComponent<ViNode>();
		ViNoToolbar.m_CurrScenarioNode.m_PlayAtStart = true;//ViNoToolbar.m_StartAndPlay;
		ViNoToolbar.m_CurrScenarioNode = scenarioObj.GetComponent<ScenarioNode>();			
//		DrawViNodesTab.CreateDialogNodeOfMenu();		
	}

	/// <summary>
	/// Draw the specified toolBox.
	/// </summary>
	static public void Draw( ViNoToolbar toolBar ){							
		ViNoToolbar.m_StartAndPlay = EditorGUILayout.Toggle( "Start And Play" , ViNoToolbar.m_StartAndPlay );
		
		EditorGUILayout.BeginHorizontal();
		
			ViNoToolbar.m_ScenarioName = EditorGUILayout.TextField( "Name" , ViNoToolbar.m_ScenarioName , GUILayout.Width ( 300f ) );		
			
			// Create a new Scenario.
			if( GUILayout.Button( "New" , GUILayout.Width(100f) ) ){//, GUILayout.Height( 25f ) ) ){	
				CreateANewScenario();		
				// For Editor Only Functions.
				toolBar.Repaint();		
			}
/*
			if( GUILayout.Button( "Play" , GUILayout.Width(100f) ) ){//, GUILayout.Height( 25f ) ) ){	
				GameObject scenarioObj = ViNoToolUtil.CreateANewScenario( ViNoToolbar.m_ScenarioName , ViNoToolbar.m_StartAndPlay );			
				ScenarioNode node = scenarioObj.GetComponent<ScenarioNode>();
				node.useScenarioScript = true;
				node.scenarioScript = Resources.Load( ViNoToolbar.m_ScenarioName , typeof(TextAsset) ) as TextAsset;
				
			}
//*/
		
		EditorGUILayout.EndHorizontal();
	}
	
}
