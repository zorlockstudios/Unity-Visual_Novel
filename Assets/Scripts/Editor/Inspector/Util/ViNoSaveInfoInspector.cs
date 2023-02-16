//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.

using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[ CustomEditor( typeof( ViNoSaveInfo ) ) ]
public class ViNoSaveInfoInspector : Editor {
	
	public bool m_Edit;
	public ScenarioNode scenarioNode;
	public int m_SelectedID;
	
	public override void OnInspectorGUI(){
		ViNoSaveInfo info = target as ViNoSaveInfo;
		
		m_Edit = EditorGUILayout.BeginToggleGroup( "" , m_Edit );
				
			info.data.m_LoadedLevelIndex =  EditorGUILayout.IntField( "LevelIndex" , info.data.m_LoadedLevelIndex );
			info.data.m_LoadedLevelName = EditorGUILayout.TextField( "LevelName" , info.data.m_LoadedLevelName );
	
//			string[] pop = { info.data.m_CurrentScenarioName };
//			int sel = 0;		
//			sel =EditorGUILayout.Popup( "ScenarioName" , sel , pop );
			info.data.m_CurrentScenarioName = EditorGUILayout.TextField( "ScenarioName" , info.data.m_CurrentScenarioName );					
			
//			string[] pop2 = { info.data.m_NodeName };
//			int sel2 = 0;
//			sel2 =EditorGUILayout.Popup( "CurrentNode" , sel2 , pop2 );
			info.data.m_NodeName = EditorGUILayout.TextField( "CurrentNode" , info.data.m_NodeName );		
			
			info.data.m_BgmName = EditorGUILayout.TextField( "BGM" , info.data.m_BgmName );		
			info.data.m_ScenarioResourceFilePath = EditorGUILayout.TextField( "ScenarioFilePath" , info.data.m_ScenarioResourceFilePath );		
		
			EditorGUILayout.LabelField( "SceneXml" );		
			info.data.m_SceneXmlData = EditorGUILayout.TextArea( info.data.m_SceneXmlData );
			
			EditorGUILayout.LabelField( "Saved Date" );
			EditorGUILayout.SelectableLabel( info.data.m_Date );

			EditorGUILayout.LabelField( "Desc" );
			EditorGUILayout.SelectableLabel( info.data.m_ScenarioDescription );

			scenarioNode = EditorGUILayout.ObjectField( scenarioNode , typeof( ScenarioNode ) , true ) as ScenarioNode; 
			if( scenarioNode != null ){		
				List<string> tagList = scenarioNode.GetNodeTagsUnderMe();
				m_SelectedID =EditorGUILayout.Popup( "NodeTagList" , m_SelectedID , tagList.ToArray() );			
			}
		
			if( GUILayout.Button ( "Clear Data" ) ){
				bool yes = EditorUtility.DisplayDialog( " ! " , "Are you sure you want to Clear Data" , "yes" , "no" );
				if( yes) {
					info.ClearData();
				}
			}

		EditorGUILayout.EndToggleGroup();		
		
		if( GUI.changed ){
			EditorUtility.SetDirty( target );	
		}
	}
}
