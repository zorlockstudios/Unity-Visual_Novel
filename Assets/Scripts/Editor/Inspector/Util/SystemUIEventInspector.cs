using UnityEngine;
using UnityEditor;
using System.Collections;

[ CustomEditor( typeof( SystemUIEvent ))]
public class SystemUIEventInspector : Editor {

	void OnEnable(){
		bool findAndSet = false;
		SystemUIEvent sys = target as SystemUIEvent;
		if( sys.messageBoxes == null ){
			findAndSet = true;
		}
		else{
			// If missing Component , then find and set.
			for( int i=0;i<sys.messageBoxes.Length;i++){
				if( sys.messageBoxes[ i ] == null ){
					findAndSet = true;
					break;
				}
			}
		}

		if( findAndSet ){
			FindAndSetMessageTargets();
		}

	}

	void FindAndSetMessageTargets(){
		SystemUIEvent sys = target as SystemUIEvent;		
		sys.messageBoxes = GameObject.FindObjectsOfType( typeof( ViNoTextBox) ) as ViNoTextBox[];
	}

	public override void OnInspectorGUI(){
		DrawDefaultInspector();

		// Collect Message Targets TextBoxes if targets is empty or null.		
		if( ! Application.isPlaying ){			
			GUILayout.Label( "<color=yellow>find ViNoTextBoxes</color>" );

			if( GUILayout.Button( "Find and Set Message Targets" ) ){
				FindAndSetMessageTargets();
			}
		}
	}


}
