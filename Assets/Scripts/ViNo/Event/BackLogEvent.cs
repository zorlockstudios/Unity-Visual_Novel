//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Back Log event.
/// </summary>
public class BackLogEvent : MonoBehaviour {
	public enum DisplayStyle{
		DISP_A_TEXT=0,
		DISP_APPENDED_TEXT,
	}
	public DisplayStyle displayStyle = DisplayStyle.DISP_A_TEXT;
	public ViNoTextBox nameTextTarget;	// Back Log Text View.
	public ViNoTextBox textTarget;		// Back Log Text View.
	
	public GameObject systemMenu;
	public GameObject voiceButton;
	public GameObject prevButton;
	public GameObject nextButton;

//	private List<string> m_LogStrList;
	private int m_LogIndex = 0;
	
	/// <summary>
	/// SendMessage to the TextTarget.
	/// </summary>
	/// <param name='index'>
	/// Index.
	/// </param>
	void OnChangedLogIndex( int index ){
//		string logStr = ViNoBackLog.GetLogText( index );
		DialogPartData data = ViNoBackLog.GetItemAt( index );
		if( voiceButton != null ){
			voiceButton.SetActive( data.isVoice );
		}
		if( nameTextTarget != null){
			if(  ! string.IsNullOrEmpty( data.nameText ) && data.nameText.Trim() != string.Empty ){
				nameTextTarget.gameObject.SetActive( true );
				nameTextTarget.gameObject.SendMessage( "OnUpdateText" , data.nameText );
			}
			else{
				nameTextTarget.gameObject.SetActive( false );			
			}
		}
		textTarget.gameObject.SendMessage( "OnUpdateText" , data.dialogText );		
	}

	void SetLogTextAlltoTarget( ){
		bool stubLineBreak = true;
		string logStr = ViNoBackLog.GetAppendedText( stubLineBreak );
		textTarget.gameObject.SendMessage( "OnUpdateText" , logStr );		

		gameObject.BroadcastMessage( "OnBackLogTextChanged");
	}
	
	/// <summary>
	/// Raises the enable event.
	/// </summary>
	void OnEnable(){
		Initialize();

//		SystemUtility.ShowSystemUI( false );
	}
	
	/// <summary>
	/// Raises the disable event.
	/// </summary>
	void OnDisable(){
//		SystemUtility.ShowSystemUI( true );

//		m_LogStrList = null;
		m_LogIndex = 0;
	}
	
	/// <summary>
	/// Initialize BackLog View.
	/// </summary>
	void Initialize(){
		m_LogIndex = ViNoBackLog.GetLogTextCount() - 1;
		switch( displayStyle ){
		 case DisplayStyle.DISP_A_TEXT:
			OnChangedLogIndex( m_LogIndex );
			// Activate the Buttons.
			prevButton.SetActive( true );
			nextButton.SetActive( true );
			break;

		 case DisplayStyle.DISP_APPENDED_TEXT:
			SetLogTextAlltoTarget();
			break;
		}
	}
	
	/// <summary>
	/// Check if 
	/// </summary>
	void RangeCheck(){
		Debug.Log( "LogCount :" + ViNoBackLog.GetLogList().Count );

		if( m_LogIndex <= 0 ){
			m_LogIndex = 0;
			if( prevButton != null ){
				prevButton.SetActive( false );
			}
		}
		else{
			if( prevButton != null ){
				prevButton.SetActive( true );
			}
		}

		if( m_LogIndex >= ViNoBackLog.GetLogList().Count - 1 ){
			m_LogIndex = ViNoBackLog.GetLogList().Count - 1;
		}
		else{
			if( nextButton != null ){
				nextButton.SetActive( true );
			}
		}
	}

	void OnClickVoice(){
		DialogPartData data = ViNoBackLog.GetItemAt( m_LogIndex );
		if( data.isVoice && ISoundPlayer.Instance != null ){			
			ISoundPlayer.Instance.PlayVoice( data.voiceAudioID , ViNoConfig.prefsVoiceVolume , 0f );
		}
	}
	
	/// <summary>
	/// Raises the click previous event.
	/// </summary>
	void OnClickPrev(){
		Debug.Log( "Click Prev " );
		m_LogIndex--;

		RangeCheck();

		OnChangedLogIndex( m_LogIndex );
	}
	
	/// <summary>
	/// Raises the click next event.
	/// </summary>
	void OnClickNext(){
		Debug.Log( "Click Next " );
		m_LogIndex++;
		if( m_LogIndex == ViNoBackLog.GetLogList().Count ){
			ClosePanel();
		}
		else{
			RangeCheck();

			OnChangedLogIndex( m_LogIndex );
		}
	}
	
	/// <summary>
	/// Raises the click return event.
	/// </summary>
	void OnClickReturn(){
		OnChangedLogIndex( ViNoBackLog.GetLogList().Count - 1 );
		ClosePanel();
	}
	
	/// <summary>
	/// Closes the panel.
	/// </summary>
	void ClosePanel(){
//		systemMenu.SetActive( false );
		gameObject.SetActive( false );	
	}

}
