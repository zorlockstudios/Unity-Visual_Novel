//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.

#pragma warning disable 0414,0649

using UnityEngine;
using System.Collections;

[ ExecuteInEditMode ]
public class GUIConfig : MonoBehaviour {	
	public string bgmLabel = "BGM";
	public string seLabel = "SE";
	public string voiceLabel = "Voice";
	public string textSpeedLabel = "TextSpeed";
	public string autoSpeedLabel = "AutoSpeed";
	public string minLabel = "min";
	public string maxLabel = "max";

	[ Range( 0f , 200f  ) ]
	public float leftSpaceWidth = 15f;
	[ Range( 0f , 200f  ) ]
	public float rightSpaceWidth = 30f;
	[ Range( 0f , 300f  ) ]
	public float labelWidth = 190f;

	public Texture2D closeButtonTexture;
	public GUISkin configSkin;
	
	private Rect m_WindowRect;

	void OnGUI(){			
		if( configSkin != null ){
			GUI.skin = configSkin;
		}

		m_WindowRect = new Rect ( 0 , 0 , Screen.width , Screen.height );
//		m_WindowRect = GUI.Window (0, m_WindowRect, DrawConfigWindow, "Config");
		DrawConfigWindow( 0 );
	}

	void OnClickClose(){
		gameObject.SetActive( false );
	}

	void DrawConfigWindow( int windowID ){
		GUILayout.BeginHorizontal();
			GUILayout.Space( Screen.width - 100f );
			bool closeClicked = false;
			if( closeButtonTexture != null ){
				closeClicked = GUILayout.Button( closeButtonTexture );
			}
			else{
				closeClicked = GUILayout.Button( "X" );
			}
			if( closeClicked ){
				gameObject.SetActive( false );
			}
			
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
			GUILayout.Space( leftSpaceWidth );
			GUILayout.Label( bgmLabel , GUILayout.Width( labelWidth) );
			GUILayout.Label( minLabel , GUILayout.Width( 80f ) );
			ViNoConfig.prefsBgmVolume = GUILayout.HorizontalSlider( ViNoConfig.prefsBgmVolume , 0f , 1f );			
			GUILayout.Label( maxLabel , GUILayout.Width( 80f ) );
			GUILayout.Space( rightSpaceWidth );
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
			GUILayout.Space( leftSpaceWidth );
			GUILayout.Label( seLabel , GUILayout.Width( labelWidth) );
			GUILayout.Label( minLabel , GUILayout.Width( 80f ) );
			ViNoConfig.prefsSeVolume = GUILayout.HorizontalSlider( ViNoConfig.prefsSeVolume , 0f , 1f );
			GUILayout.Label( maxLabel , GUILayout.Width( 80f ) );
			GUILayout.Space( rightSpaceWidth );
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
			GUILayout.Space( leftSpaceWidth );
			GUILayout.Label( voiceLabel ,GUILayout.Width( labelWidth) );
			GUILayout.Label( minLabel , GUILayout.Width( 80f ) );
			ViNoConfig.prefsVoiceVolume = GUILayout.HorizontalSlider( ViNoConfig.prefsVoiceVolume , 0f , 1f );
			GUILayout.Label( maxLabel , GUILayout.Width( 80f ) );
			GUILayout.Space( rightSpaceWidth );
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
			GUILayout.Space( leftSpaceWidth );
			GUILayout.Label( textSpeedLabel , GUILayout.Width( labelWidth) );
			GUILayout.Label( minLabel , GUILayout.Width( 80f ) );
			ViNoConfig.prefsTextSpeed = GUILayout.HorizontalSlider( ViNoConfig.prefsTextSpeed , 0f , 1f );
			GUILayout.Label( maxLabel , GUILayout.Width( 80f ) );
			GUILayout.Space( rightSpaceWidth );
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
			GUILayout.Space( leftSpaceWidth );
			GUILayout.Label( autoSpeedLabel , GUILayout.Width( labelWidth) );
			GUILayout.Label( minLabel , GUILayout.Width( 80f ) );
			ViNo.autoModeWaitTime = GUILayout.HorizontalSlider( ViNo.autoModeWaitTime , 0f , 1f );
			GUILayout.Label( maxLabel , GUILayout.Width( 80f ) );
			GUILayout.Space( rightSpaceWidth );
		GUILayout.EndHorizontal();
		
	}
			
}
