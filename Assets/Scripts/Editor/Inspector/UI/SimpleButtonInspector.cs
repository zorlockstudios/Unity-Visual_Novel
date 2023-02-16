//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 		VisualNovelToolkit		/_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
using UnityEngine;
using UnityEditor;
using System.Collections;

[ CustomEditor( typeof(SimpleButton))]
public class SimpleButtonInspector : Editor {

	public override void OnInspectorGUI(){
		DrawDefaultInspector();
	}

}
