//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.

using UnityEngine;
using UnityEditor;
using System.Collections;

[ CustomEditor( typeof( ViNoSoundPlayer))]
public class ViNoSoundPlayerInspector : Editor {

	public override void OnInspectorGUI(){
		
		ViNoEditorUtil.BeginSoundGUIColor();
		
		DrawDefaultInspector();
		
		ViNoEditorUtil.EndSoundGUIColor();
	}
}
