using UnityEngine;
using System.Collections;

public class OnActiveSaveLoadPanel : MonoBehaviour {
	public ViNoTextBox saveLoadText;

	void OnEnable(){
		if( SystemUIEvent.saveMode ){
			saveLoadText.SetText( "Save" );
		}
		else{
			saveLoadText.SetText( "Load" );
		}
	}

}
