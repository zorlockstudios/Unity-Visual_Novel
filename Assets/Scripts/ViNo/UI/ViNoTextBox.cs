//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ViNoToolkit;

[ System.Serializable]
[ AddComponentMenu( "ViNo/UI/TextBox" ) ]
public class ViNoTextBox : ViNoToolkit.TextBox{
	public AnimationNode textDisplayEffect;	// For example , attach a AnimationNode with Fade in Effect.
	public AnimationNode textAfterEffect;	// For example , attach a AnimationNode with Fade out Effect.

	public Blink blink;
				
	void Awake(){
		if( ClearMessageAtAwake ){
			ClearMessage();
		}
		m_Panel = GetComponent<PanelBase>();
	}

	void Start(){		
		textSpeed = m_TextSpeed;
	}
		
	// Update is called once per frame
	void Update(){				
		if( ! m_Update ){
			return;	
		}
		
		if( ! reachedEnd ){
			if( Process( Time.deltaTime * ViNoConfig.prefsTextSpeed ) ){
				m_CurrLength++;				
				if( textEventListener != null) {
					textEventListener.OnUpdateText( textShown );
				}
			}
		}
	}

	/// <summary>
	/// Initialize the specified message.
	/// </summary>
	/// <param name='message'>
	/// Message.
	/// </param>
	public override void Initialize( string message ){
		text = message;
						
		if( blink != null ){
			blink.DontShow();
		}			
		// Init.
		m_Update = true;
		m_TimeElapsed = 0f;			
		startIndex = 0;
		m_CurrLength = 0;
		m_ReachedEnd = false;		
	
	}

	public override void NextMessage(){
		if( blink != null ){
			blink.DontShow();
		}

		if( ! ViNo.isLockNextMessage ){
			if( useTextAfterEffect ){
				if( ! reachedEnd ){		
					ViNo.NextMessage();
				}
				else{
					DoTextAfterEffect();
				}
			}
			else{
				ViNo.NextMessage();				
			}
		}		
	}	

	/// <summary>
	/// Append the specified message.
	/// </summary>
	/// <param name='message'>
	/// Message.
	/// </param>
	public override void Append( string message  ){
		if( blink != null ){
			blink.DontShow();
		}

		if( wrapText ){
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			int counter = 0;
			for(int i=0;i<message.Length;i++){
				sb.Append( message[ i ] );
				counter++;
				if( counter > wrapEvery ){
					counter = 0;
					sb.Append( System.Environment.NewLine );
				}
			}
			text += sb.ToString();
		}
		else{
			text += message;
		}

		if( ! m_Update ){
			if( textEventListener != null) {
				textEventListener.OnStartMessage();
			}		
		}
		
		m_Update = true;
		m_TimeElapsed = 0f;			

		m_ReachedEnd = false;			
		
		if( textEventListener != null) {
			textEventListener.OnAppend( message );
		}		
	}

	public override void DoTextDisplayEffect(){
		if( textDisplayEffect != null ){
			textDisplayEffect.Preview();
		}		
	}

	public override void DoTextAfterEffect(){
		StartCoroutine( "TextAfterEffectAndNextMessage" );		
	}

	IEnumerator TextAfterEffectAndNextMessage( ){		
		ViNo.LockNextMessage();

		if( textAfterEffect != null ){
			textAfterEffect.Preview();
		}

		// Wait Until the Text After Effect is Finished.
		if( textAfterEffect != null ){			
			yield return new WaitForSeconds( textAfterEffect.duration/1000f );
		}
		else{
			yield return new WaitForSeconds( 0.001f );
		}

		ViNo.UnlockNextMessage();
		ViNo.NextMessage();
	}				

/*
	public void ShowPrompt( bool show ){
		if( blink != null ){
			blink.show = show;
		}
	}
//*/

/*			
 	This is moved to SystemUtility.
	static public void ClearAllTextBoxMessage(){		
		ViNoTextBox[] boxes = GameObject.FindObjectsOfType( typeof( ViNoTextBox ) ) as ViNoTextBox[];
		for( int i=0;i<boxes.Length;i++){
			Debug.Log( "<color=cyan>" + boxes[ i ].name + " is cleared." + "</color>");
			boxes[ i ].ClearMessage();
		}
	}
//*/		

}

