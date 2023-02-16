//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.

using UnityEngine;
using UnityEditor;
using System.Collections;

public class ScriptableObjectUtility{
	readonly static string[] labels = { "ViNo" };

	static public ScriptableObject CreateScriptableObject( string className , string path ){
		ScriptableObject obj = ScriptableObject.CreateInstance ( className );
		AssetDatabase.CreateAsset (obj, path  );
		ScriptableObject sobj = AssetDatabase.LoadAssetAtPath (path, typeof(ScriptableObject)) as ScriptableObject;
		AssetDatabase.SetLabels (sobj, labels);
		EditorUtility.SetDirty (sobj);
		return obj;
	}
	
}