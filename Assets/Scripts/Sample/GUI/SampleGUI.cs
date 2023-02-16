//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
using UnityEngine;
using System.Collections;

/// <summary>
/// Sample GUI.
/// </summary>
public class SampleGUI : MonoBehaviour {
	public GUIBackLog guiBackLog;
	public MonoBehaviour guiNextMessage;
	public GUIQuickMenu guiQuickMenu;
	public GUIAutoMode  guiAutoMode;
	
	public ViNoTextBox textBox;
	
	public void EnableShowAll( bool t ){
		guiBackLog.enabled = t;	
		guiQuickMenu.enabled = t;	
		guiNextMessage.enabled = t;
		guiAutoMode.enabled = t;
	}
		
	public void EnableShowBackLog( bool t ){
		guiBackLog.enabled = t;	
		
		if( t ){
			guiAutoMode.gameObject.SetActive( false );
			textBox.gameObject.SetActive( false );
		}
		else{
			guiAutoMode.gameObject.SetActive( true );
			textBox.gameObject.SetActive( true );				
		}
	}
	
	public void EnableShowNextMessage( bool t ){
		guiNextMessage.enabled = t;	
	}	
	
	public void EnableShowQuickMenu( bool t ){
		guiQuickMenu.enabled = t;	
	}		
		
	
}
