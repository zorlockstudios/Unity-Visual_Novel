using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode ]
public class TestScenarioSelector : MonoBehaviour {
	public ScenarioNode[] scenarios;
					
	void OnGUI(){			
		if( scenarios != null && scenarios.Length > 0 ){								
//			GUILayout.BeginHorizontal();
			for( int i=0;i<scenarios.Length;i++){
				if( GUILayout.Button( scenarios[ i ].name ) ){
					ViNoAPI.PlayScenario( scenarios[ i ].name );
				}
			}			
//			GUILayout.EndHorizontal();
		}
	}	
}
