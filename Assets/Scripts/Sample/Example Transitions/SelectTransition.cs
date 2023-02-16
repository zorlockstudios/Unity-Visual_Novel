using UnityEngine;
using System.Collections;

public class SelectTransition : MonoBehaviour {

	void OnGUI(){
		if( GUILayout.Button( "Start TileFadeDemo" ) ){
			ViNoAPI.PlayScenario( "TileFadeDemo" );
		}

		if( GUILayout.Button( "Start BlindDemo" ) ){
			ViNoAPI.PlayScenario( "BlindDemo" );
		}
	}
}
