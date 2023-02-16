//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
using UnityEngine;
using UnityEditor;
using System.Collections;

public class ObjectCreateTool : EditorWindow {

	[MenuItem( "Window/ViNo/ObjectCreateTool") ]
	static public void Init(){
		GetWindow( typeof( ObjectCreateTool) );
	}

	void OnGUI(){
		DrawObjectsTab.Draw();
	}

}

