//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.

using UnityEngine;
using UnityEditor;
using System.Collections;

public class ViNoEditorUtil {
	
	static private Color savedColor;
	static private Color savedBackgroundColor;
	static private Color savedContentColor;
	
	/// <summary>
	/// Draws the warning string.
	/// </summary>
	/// <param name='exp'>
	/// Expression.
	/// </param>
	static public void DrawWarningString( string exp ){
		Color temp = GUI.color;
		GUI.color = Color.yellow;
		GUILayout.Label( exp );
		GUI.color = temp;
		GUI.enabled = false;		
	}

	/// <summary>
	/// Creates the prefab and return it.
	/// </summary>
	static public GameObject CreatePrefab( GameObject obj , string path ){
		Object prefab = PrefabUtility.CreateEmptyPrefab( path );
		PrefabUtility.ReplacePrefab( obj , prefab , ReplacePrefabOptions.ConnectToPrefab );		
		
		EditorGUIUtility.PingObject( prefab );
		
		return prefab as GameObject;
	}	
	
	static public void BeginGreenColor(){
		if( EditorGUIUtility.isProSkin ){
			ViNoEditorUtil.BeginGUIColor( GUI.color , Color.green , GUI.contentColor );
		}
		else{
			ViNoEditorUtil.BeginGUIColor( Color.white , new Color( 0.8f , 0.8f , 0.8f , 1f ) , Color.black );
		}					
	}
	
	static public void BeginGUIColor( Color guiCol , Color bgCol , Color contentCol){
		savedColor = GUI.color;
		savedBackgroundColor = GUI.backgroundColor;
		savedContentColor = GUI.contentColor;

		GUI.color = guiCol;
		GUI.backgroundColor = bgCol;//Color.gray;
		GUI.contentColor = contentCol;// Color.white;		
	}
	
	static public void EndGUIColor(){
		GUI.color = savedColor;
		GUI.contentColor = savedContentColor;
		GUI.backgroundColor = savedBackgroundColor;		
	}

	static public void BeginAnimationGUIColor(){		
		BeginGUIColor( Color.yellow , GUI.backgroundColor , GUI.contentColor );
	}

	static public void BeginSoundGUIColor(){
		BeginGUIColor( GUI.color , Color.gray , Color.white );
	}
	
	static public void EndSoundGUIColor(){
		EndGUIColor();
	}	
	
	
}
