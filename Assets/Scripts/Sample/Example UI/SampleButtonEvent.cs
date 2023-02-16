using UnityEngine;
using System.Collections;
using ViNoToolkit;

public class SampleButtonEvent : MonoBehaviour {
	public ViNoTextBox textBox;
	public ViNoTextEventListener textEventListener;
	
	public string[] descriptions;
	public ColorPanel[] buttonPanels;

	void Awake(){
/*		// Set Button Colors DeactiveColor.
		for( int i=0;i<buttonPanels.Length;i++){
			buttonPanels[ i ].SetDeactiveColor();
		}
//*/		
	}

	private void ChangeDescriptionAndStartMessage( int messageID ){
		textBox.ClearMessage();
		textBox.Append( descriptions[ messageID ] );
		textBox.DoTextDisplayEffect();

		buttonPanels[ messageID ].SetActiveColor();
	}

	void OnClickButton1(){
		Debug.Log( "OnClickButton1" );
		ChangeDescriptionAndStartMessage( 0 );
	}

	void OnClickButton2(){
		Debug.Log( "OnClickButton2" );

		ChangeDescriptionAndStartMessage( 1 );
	}

	void OnClickButton3(){
		Debug.Log( "OnClickButton3" );

		ChangeDescriptionAndStartMessage( 2 );
	}

	void OnClickButton4(){
		Debug.Log( "OnClickButton4" );

		ChangeDescriptionAndStartMessage( 3 );
	}

}
