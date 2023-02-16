//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
using UnityEngine;
using System.Collections;

/// <summary>
/// Sample GUI ctrl.
/// If textBox message is not reached to an end,
/// do not show gui until message is displayed all.
/// </summary>
public class SampleGUICtrl : MonoBehaviour {
	public GameObject gui;
	public ViNoTextBox textBox;
	
	// Update is called once per frame
	void Update(){		
		if( ! textBox.reachedEnd ){		
			gui.SetActive( false );
		}
		else{
			gui.SetActive( true );
		}
	}
}

