//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
using UnityEngine;
using UnityEditor;
using System.Collections;

public class CreateSampleGUI {

	[ MenuItem( "GameObject/ViNo/SampleGUI/NextMessage" )]
	static public void CreateGUINextMessage(){
		GameObject next = new GameObject( "GUINextMessage");		
		next.AddComponent<GUINextMessage>();
	}

	
}
