//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.

using UnityEngine;
using System.Collections;

public class CheckQuickSaveData : MonoBehaviour {
	public TransformGUIButton continueButton;

	// Use this for initialization
	void Start () {
		bool isQuickSaveFileExists = SystemUtility.CheckQuickSaveFileAndLoadFromStorage();
		continueButton.isEnabled = isQuickSaveFileExists;
	}

	void DidQuickSave(){
		Debug.Log( "Quick Save Succeeded callback.");
		continueButton.isEnabled = true;
	}	
}
