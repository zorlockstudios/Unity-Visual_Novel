//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[ CustomEditor( typeof( JumpTargetNode ) ) ] 
public class JumpTargetNodeInspector : Editor {

	public override void OnInspectorGUI(){						
//		ViNode jumpNode = ( target as JumpTargetNode ).jumpTarget;		
//		Color savedCol = GUI.backgroundColor;
//		GUI.backgroundColor = Color.blue;
		
		DrawDefaultInspector();

//		GUI.backgroundColor = savedCol;						
	}
	
}
