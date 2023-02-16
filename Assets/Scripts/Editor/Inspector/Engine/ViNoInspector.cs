//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.

using UnityEditor;
using UnityEngine;
using System.Collections;

[ CustomEditor( typeof( ViNo) ) ] 
public class ViNoInspector : Editor {
	
	public override void OnInspectorGUI(){	
		Color saved = GUI.color;
		GUI.color = Color.green;
		
		DrawDefaultInspector();
						
		IScriptEngine se = target as IScriptEngine;					
		GUICommon.DrawScriptEngineCommonInspector( se );
		
		GUI.color = saved;
	}	
}
