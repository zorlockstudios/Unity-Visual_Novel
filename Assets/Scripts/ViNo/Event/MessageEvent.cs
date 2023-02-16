//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
using UnityEngine;
using System.Collections;

public class MessageEvent : ViNoEventListener {

	// TODO /
	void resetfont( Hashtable param ){
		SystemUIEvent sys = SystemUIEvent.Instance;
		if( sys == null ){
			Debug.LogError( "SystemUIEvent not found in this scene." );
			return;
		}

		// Commit Changes to Current Message Target.
		ViNoTextBox textBox = sys.GetCurrentTextBox();
		if( textBox != null ){
			textBox.ResetFont();
		}		
	}

	// On "font" Kirikiri Tag.
	void font( Hashtable param ){
		SystemUIEvent sys = SystemUIEvent.Instance;
		if( sys == null ){
			Debug.LogError( "SystemUIEvent not found in this scene." );
			return;
		}

		// Commit Changes to Current Message Target.
		ViNoTextBox textBox = sys.GetCurrentTextBox();
		if( textBox != null ){
			textBox.ChangeProperty( param );
		}
	}

	public void OnEnterViNode( ViNoNodeEvtData data ){
//		Debug.Log( "Enter : " +  data.nodeName );
	}
	
	public void OnHideMessage( MessageEventData data ){
		SystemUIEvent sys = SystemUIEvent.Instance;
		sys.HideMessage( data );
	}

	public void OnMessageTargetChanged( MessageEventData data ){
		SystemUIEvent sys = SystemUIEvent.Instance;
		sys.ChangeTargetTextBox( data.textBoxID );
	}

	// TODO : IMPL.
	void OnTextBoxOpt( Hashtable param ){
		
		
	}
		
}
