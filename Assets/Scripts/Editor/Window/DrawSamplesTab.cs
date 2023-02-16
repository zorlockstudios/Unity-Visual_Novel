//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.

using UnityEngine;
using UnityEditor;
using System.Collections;

public class DrawSamplesTab {

	static public void Draw(){			
		EditorGUILayout.LabelField( "Create Example BG" , GUILayout.Width( 300f ) );
		
		EditorGUILayout.BeginHorizontal();
		
		string bgPath = "Assets/" + ViNoToolUtil.kAssetName + "/Examples/SampleObjects/BG/";
		if( GUILayout.Button( "1"  , GUILayout.Width(50f) , GUILayout.Height (50f) ) ){
			ViNoToolbar.ImportExampleCharacter( bgPath + "1.prefab" , "BG" );
		}		

		if( GUILayout.Button( "2"  , GUILayout.Width(50f) , GUILayout.Height (50f) ) ){
			ViNoToolbar.ImportExampleCharacter( bgPath + "2.prefab" , "BG" );
		}		

		if( GUILayout.Button( "3"  , GUILayout.Width(50f) , GUILayout.Height (50f) ) ){
			ViNoToolbar.ImportExampleCharacter( bgPath + "3.prefab" , "BG" );
		}		
		
		EditorGUILayout.EndHorizontal();		
		
		EditorGUILayout.LabelField( "Create Example Characters" , GUILayout.Width( 300f ) );
		EditorGUILayout.BeginHorizontal();
		
		string charaPath = "Assets/" + ViNoToolUtil.kAssetName + "/Examples/SampleObjects/Characters/";
		if( GUILayout.Button( "1"  , GUILayout.Width(50f) , GUILayout.Height (50f) ) ){
			ViNoToolbar.ImportExampleCharacter( charaPath + "sachi.prefab" , "character" );
		}

		if( GUILayout.Button( "2"  , GUILayout.Width(50f) , GUILayout.Height (50f) ) ){
			ViNoToolbar.ImportExampleCharacter( charaPath + "yoshino.prefab" , "character" );
		}

		if( GUILayout.Button( "3"  , GUILayout.Width(50f) , GUILayout.Height (50f) ) ){
			ViNoToolbar.ImportExampleCharacter( charaPath + "yuki.prefab" , "character" );
		}
		
		EditorGUILayout.EndHorizontal();	
		EditorGUILayout.LabelField( "Create SampleScenario" , GUILayout.Width( 300f ) );
		EditorGUILayout.BeginHorizontal();
		

		if( GUILayout.Button( "1"  , GUILayout.Width(50f) , GUILayout.Height (50f) ) ){
//			ViNoToolbarTabs.ImportExampleCharacter( charaPath + "sachi.prefab" , "character" );
			GameObject obj = AssetDatabase.LoadAssetAtPath(
					"Assets/" + ViNoToolUtil.kAssetName + "/Templates/ViNoSceneTemplate.prefab" , typeof( GameObject) ) as GameObject;
			
			GameObject clone = GameObject.Instantiate( obj ) as GameObject;
			ViNoGOExtensions.StripGameObjectName( clone , "(Clone)" , "" );
		}
		
		EditorGUILayout.BeginHorizontal();		
	}
		

}
