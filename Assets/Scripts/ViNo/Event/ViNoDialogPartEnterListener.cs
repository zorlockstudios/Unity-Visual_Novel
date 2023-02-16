//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
using UnityEngine;
using System.Collections;

public class ViNoDialogPartEnterListener : MonoBehaviour {
	
	void OnEnterViNode( ViNoNodeEvtData data ){
		Debug.Log( "OnEnterViNode" + data.nodeName ) ;
				
	}

	void OnExitViNode( ViNoNodeEvtData data ){
	
	}
	
	// args is "nodeName" and "ID" is required.
	void OnEnterDialog( Hashtable args ){
		GameObject dlgNodeObj = GameObject.Find( args[ "nodeName" ] as string );
		if( dlgNodeObj != null ){			
			DialogPartNode dlg = dlgNodeObj.GetComponent<DialogPartNode>();
			string id = args[ "ID" ] as string;			
			int messageID =  int.Parse( id );
			dlg.TriggerOnEnterEvent( messageID );
		}				
	}
	
	void OnExitDialog( Hashtable args ){
		GameObject dlgNodeObj = GameObject.Find( args[ "nodeName" ] as string );
		if( dlgNodeObj != null ){			
			DialogPartNode dlg = dlgNodeObj.GetComponent<DialogPartNode>();
			string id = args[ "ID" ] as string;			
			int messageID =  int.Parse( id );
			dlg.TriggerOnExitEvent( messageID );
		}				
	}
	
}
