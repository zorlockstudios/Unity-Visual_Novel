//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 		VisualNovelToolkit		/_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
using UnityEngine;
using UnityEditor;
using System.Collections;

[ CustomEditor( typeof(ViNoTextEventListener ))]
public class ViNoTextEventListenerInspector : Editor {

	protected void CommitChanges(){
		if( GUI.changed ){
			( target as ViNoTextEventListener ).Enabled();
		}
	}

	public override void OnInspectorGUI(){
		DrawDefaultInspector();

		CommitChanges();
	}

}
