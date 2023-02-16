//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
using UnityEngine;
using System.Collections;

public class OnEnableAndHideSystemUI : MonoBehaviour {

	private bool m_TempAutoModeState;

	IEnumerator DelayHideSystem( float afterSec ){
		yield return new WaitForSeconds( afterSec );

		SystemUIEvent sys = SystemUIEvent.Instance;
		sys.HideSystemUI();

	}

	// Use this for initialization
	void OnEnable () {
//		SystemUtility.ShowSystemUI( false );
#if false		
		SystemUIEvent sys = SystemUIEvent.Instance;
		sys.HideSystemUI();
#else
		StartCoroutine( "DelayHideSystem" , 0.1f );
#endif
		m_TempAutoModeState = ViNo.autoMode;
		ViNo.autoMode = false;
	}
	
	// Update is called once per frame
	void OnDisable () {
		SystemUIEvent sys = SystemUIEvent.Instance;
		if( sys != null ){
			sys.ShowSystemUI( true );
		}
//		SystemUtility.ShowSystemUI();
		ViNo.autoMode = m_TempAutoModeState;
	}
}
