//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
using UnityEngine;
using System.Collections;

static public class SystemUtility {	

	static public string k_QuickSaveFileName = "QSaveData";

	static public bool CheckQuickSaveFileAndLoadFromStorage(){		
		bool isQuickSaveFileExists = ExternalAccessor.IsSaveDataFileExists( k_QuickSaveFileName );
		if( isQuickSaveFileExists ){
			ScenarioCtrl sc = ScenarioCtrl.Instance;
			// Load SaveInfo from Storage.
			ViNo.LoadDataFromStorage( k_QuickSaveFileName , ref sc.quickSaveSlot);
		}
		else{
			Debug.LogWarning( "Quick Save File not exists." );
		}

		return isQuickSaveFileExists;
	}

	static public void ShowSystemUI( bool t = true ){
		SystemUIEvent sys = SystemUIEvent.Instance;
		if( sys != null ){
			sys.ShowSystemUI( t );
		}
		else{
			Debug.LogWarning( "SystemUIEvent not found." );
		}
	}

	static public void ClearAllTextBoxMessage(){
		SystemUIEvent sys = SystemUIEvent.Instance;		
		if( sys != null ){
			for( int i=0;i<sys.messageBoxes.Length;i++){
				sys.messageBoxes[ i ].ClearMessage();
			}
		}
		else{
			Debug.LogWarning( "SystemUIEvent not found." );
		}
	}

	static public void EnableColliderCurrentTextBox( bool enable ){
		SystemUIEvent sys = SystemUIEvent.Instance;		
		if( sys != null ){
			sys.EnableColliderCurrentTextBox( enable );
		}
		else{
			Debug.LogWarning( "SystemUIEvent not found." );
		}
	}

}
