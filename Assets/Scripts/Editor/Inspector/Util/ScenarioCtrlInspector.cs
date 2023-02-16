//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
using UnityEngine;
using UnityEditor;
using System.Collections;

[ CustomEditor(typeof(ScenarioCtrl))]
public class ScenarioCtrlInspector : Editor {

	public override void OnInspectorGUI(){

#if true
		DrawDefaultInspector();

		GUI.enabled = false;

			GUILayout.Label( "Mouse Scroll down and next message." );
			GUILayout.Label( "Keyboard Space Down and next message." );

		GUI.enabled = true;

#else
		EditorGUILayout.LabelField( "please attach a NewGame Info ( ViNoSaveInfo ). " );		
		EditorGUILayout.LabelField( "LevelName and GO attached a ScenarioNode in Resources folder is required." );		

		ScenarioCtrl sc = target as ScenarioCtrl;
		sc.newGameInfo = EditorGUILayout.ObjectField( "NewGameInfo" , sc.newGameInfo , typeof(ViNoSaveInfo) , false ) as ViNoSaveInfo;		
		sc.quickSaveSlot = EditorGUILayout.ObjectField( "QuickSaveSlot" , sc.quickSaveSlot , typeof(ViNoSaveInfo) , false ) as ViNoSaveInfo;		
		sc.saveInfo = EditorGUILayout.ObjectField( "CurrentSaveInfo" , sc.saveInfo , typeof(ViNoSaveInfo) , false ) as ViNoSaveInfo;

		GUI.enabled = false;

			// This is true.
			sc.destroyPrevScenario = EditorGUILayout.Toggle( "DestroyPrevScenario" , sc.destroyPrevScenario );
			sc.fileName = EditorGUILayout.TextField( "CurrentFileName" , sc.fileName );// , typeof(ViNoSaveInfo) , false ) as ViNoSaveInfo;		

		GUI.enabled = true;
#endif

	}

}
