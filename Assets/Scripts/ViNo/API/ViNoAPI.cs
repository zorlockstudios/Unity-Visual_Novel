//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public static class ViNoAPI{

	

	static public void DoNewGame(){
		ScenarioCtrl.Instance.DoNewGame();
	}

	static public void DoContinue(){
		ScenarioCtrl.Instance.DoContinue();
	}

	static public void NextMessage(){
		if( SystemUIEvent.Instance != null ){
			SystemUIEvent.Instance.NextMessage();
		}
	}

	static public void ForceAutoMode(){
		ViNo.autoMode = true;
	}

	static public void UnlockForceAutoMode(){
		ViNo.autoMode = false;
	}

	// this is used when the Config ColorPanel is shown in game.
	static public void EnableUpdateADV( bool isUpdate = true ){
		if( VM.Instance != null ){
			VM.Instance.gameObject.SetActive( isUpdate );		
		}
	}

	static public bool SaveData( string fileName , ViNoSaveInfo info ){
		return ViNo.SaveData( fileName , info );
	}

	static public bool DoQuickSave(){
		return ViNo.DoQuickSave();
	}

	static public bool DoQuickLoad(){	
		return ViNo.DoQuickLoad();		
	}

	[System.Obsolete]
	static public void PlayScenario( string scenarioResourcePath ){
		ScenarioCtrl.PlayScenario( Application.loadedLevelName, scenarioResourcePath );
	}

	// if info's ScenarioNode found in this Level, then play that.
	static public void PlayScenario( ViNoSaveInfo info ){
		ScenarioCtrl.PlayScenario( info );
	}

}