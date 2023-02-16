//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.

using UnityEngine;
using UnityEditor;
using System.Collections;

[ CustomEditor( typeof(ColorPanel) )]
public class ColorPanelInspector : Editor {
	
/*	void OnEnable(){
		ColorPanel panel = target as ColorPanel;
		panel.UpdateColor();		
	}
//*/	
	public override void OnInspectorGUI(){
		ColorPanel panel = target as ColorPanel;

		DrawDefaultInspector();

		if( GUI.changed ){
			panel.UpdateColor();
			EditorUtility.SetDirty( panel );			
		}	

	}
}
