//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.

using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Sample GUI back log.
/// </summary>
public class GUIBackLog : GUIBase {
	public enum DisplayStyle{
		TEXT_ONLY = 0,
		TEXT_AND_VOICE,
	}
	public float nameWidth = 200f;

	public DisplayStyle displayStyle = DisplayStyle.TEXT_AND_VOICE;
	public Vector2 m_ScrollPos = Vector2.zero;
	public Texture2D voiceTex;
	public GUISkin nameSkin;
	public GUISkin textSkin;

	
	private string m_AllText;

	void OnClickClose(){
		gameObject.SetActive( false );		
	}

	/// <summary>
	/// Raises the enable event.
	/// </summary>
	void OnEnable(){
		if( displayStyle == DisplayStyle.TEXT_ONLY ){
			m_AllText = ViNoBackLog.GetAppendedText( true );
			m_ScrollPos.x = m_AllText.Length * 144f;
		}		
	}
	
	/// <summary>
	/// Raises the disable event.
	/// </summary>
	void OnDisable(){
		m_AllText = null;
	}

	void Update(){

	}
	
	public override void OnGUICustom(){
		List<DialogPartData> list = ViNoBackLog.GetLogList();
		
		
		GUILayout.BeginHorizontal();
		
			float closeBtnW = 100f;
			GUILayout.Label( "BackLog" , GUILayout.Width( Screen.width - 100f )  );
		
			if( GUILayout.Button( "x" , GUILayout.Width( closeBtnW ) , GUILayout.Height( 50f) ) ){
				gameObject.SetActive( false );
			}
	
			GUILayout.Space( 30f );

		GUILayout.EndHorizontal();
//*/

		m_ScrollPos = GUILayout.BeginScrollView(m_ScrollPos , false , true , GUILayout.Width( Screen.width  ) );

		switch( displayStyle ){
			case DisplayStyle.TEXT_ONLY:
				GUILayout.BeginHorizontal();

					GUILayout.Space( 30f );

					GUILayout.Label( m_AllText );

				GUILayout.EndHorizontal();
				break;

			case DisplayStyle.TEXT_AND_VOICE:
				for (int i = 0, imax = list.Count; i < imax; ++i){
					if( nameSkin != null ){
						GUI.skin = nameSkin;
					}			
					if( ! string.IsNullOrEmpty(  list[i].nameText ) ){
						if( list[i].nameText.Trim() != string.Empty ){
							if( GUILayout.Button( list[i].nameText , GUILayout.Width( nameWidth ) , GUILayout.Height( 44f ))  ){
							}
						}
					}

					GUILayout.BeginHorizontal();

					if( list[i].isVoice ){
						if( GUILayout.Button( voiceTex , GUILayout.Width( 64f ) , GUILayout.Height( 64f ) ) ) {// "Voice" , GUILayout.Width( 66f ) ,  GUILayout.Height( 88f ))  ){				
							if(  ISoundPlayer.Instance != null ){				
								if( ISoundPlayer.Instance as ViNoSoundPlayer ){
									ISoundPlayer.Instance.PlayVoice( list[i].voiceAudioID , ViNoConfig.prefsVoiceVolume , 0f );								
								}
								else if( ISoundPlayer.Instance as SimpleSoundPlayer ){
									ISoundPlayer.Instance.PlayVoice( list[i].voiceAudioKey , false , 0f );																	
								}
							}
						}
					}
					else{
						GUILayout.Space( 66f );
					}

					if( textSkin != null ){
						GUI.skin = textSkin;
					}			

					if( GUILayout.Button( list[i].dialogText ) ){//, GUILayout.Height( 100f ))  ){				
						
					}

					GUILayout.EndHorizontal();
				}
				break;
		} 
		
		GUILayout.EndScrollView();	

// In mobile , GUIScrollview cannot swipe. instead Show up down buttons.
#if UNITY_IPHONE || UNITY_ANDROID
		GUILayout.BeginHorizontal();

			if( GUILayout.Button( "Up" ) ){
				m_ScrollPos.y -= 100f;
			}

			if( GUILayout.Button( "Dwn" ) ){
				m_ScrollPos.y += 100f;
			}

		GUILayout.EndHorizontal();
#else
		if(  Input.GetKey( KeyCode.UpArrow ) ){
			m_ScrollPos.y -= 10f;
		}
		else if( Input.GetKey( KeyCode.DownArrow ) ){
			m_ScrollPos.y += 10f;
		}		
#endif

	}


}
