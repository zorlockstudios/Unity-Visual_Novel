//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TitleMenuEvent : MonoBehaviour {

	// Below functions will be called in the Sequence of "Title" ScenarioNode.
	// SelectionsNode shows 3 choices,  "NewGame" and "Continue" and "Quit".

	// Check whether the Quick Save Data File is Exists.
	// if Exists in your storage , then Set a Flag named "QSaveDataExists".
	void CheckQuickSaveFile(){
		bool isQuickSaveFileExists = SystemUtility.CheckQuickSaveFileAndLoadFromStorage();
		ScenarioNode.Instance.flagTable.SetFlagBy( "QSaveDataExists" , isQuickSaveFileExists );
	}

	void OnEnterTitle(){
		SystemUtility.ShowSystemUI( false );
	}	

	void OnClickNewGame(){
		ViNoAPI.DoNewGame();
	}
	
	void OnClickQuickContinue(){
		ViNoAPI.DoQuickLoad();
	}

	void OnClickApplicationQuit(){
		Application.Quit();
	}

	void OnExitTitle(){
		SystemUtility.ShowSystemUI( true );		
	}
}
