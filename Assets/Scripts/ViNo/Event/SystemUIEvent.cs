//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
using UnityEngine;
using System.Collections;

/// <summary>
/// This is needed to save or load or viewing BackLog.
/// </summary>
[ AddComponentMenu( "ViNo/Event/SystemUIEvent" ) ]
public class SystemUIEvent : MonoBehaviour {
	static private SystemUIEvent m_Instance;
	static public SystemUIEvent Instance{
		get{ 
			if( m_Instance == null ){
				m_Instance = GameObject.FindObjectOfType( typeof( SystemUIEvent )) as SystemUIEvent;
			}
			return m_Instance;
		}
	}

	public ViNoTextBox[] messageBoxes;		// Attach TextBoxes.

	public bool deactivePrevMessageTarget = true;
	public bool StartAtDeactiveMessages;

	public GameObject systemUIRoot;
//	public bool deactiveSysUIAtAwake;

	public GameObject menuBar;
	public GameObject menuButton;
	public GameObject menuPanel;
	public GameObject backLogPanel;
	public GameObject configPanel;
	public GameObject backLogButton;

	public GameObject saveLoadPanel;

	public ColorPanel logButtonPanel;
	public ColorPanel autoSkipPanel;
	public ColorPanel autoModePanel;

	private ViNoTextBox currentNameTextBox;
	private ViNoTextBox currentTextBox;
	private bool m_BackLogEnabled;
	private bool m_BackLogDisplayed;
	private bool m_PrevBackLogState;

	static public bool saveMode{ set; get; }

	public void SetText( string text , int textBoxID ){
		messageBoxes[ textBoxID ].gameObject.SetActive( true );
		messageBoxes[ textBoxID ].SetText( text );// data.message );
	}

	public void NextMessage(){
		if( currentTextBox != null ){
			currentTextBox.NextMessage();
		}
		else{
			Debug.LogWarning( "Current TextBox Target not set." );
		}
	}

	public void ChangeTargetTextBox( byte id ){		
		if( messageBoxes == null ){
			Debug.LogError( "SystemUIEvent messageBoxes Not Attached. please attach some textboxes." );
			return;
		}
			
		if( messageBoxes.Length <= id ){
			Debug.LogError( "SystemUIEvent Text Message Target ID error. the id : " + id.ToString() );
			return;
		}

		if( systemUIRoot != null ){
			if( ! systemUIRoot.activeInHierarchy ){
				systemUIRoot.SetActive( true );
			}
		}

		ViNoDebugger.Log( "SystemUIEvent" , "ChangeTargetTextBox :" + id);

		if( deactivePrevMessageTarget ){
			// Deactivate prev textBox.
			if( currentTextBox != null && currentTextBox != messageBoxes[ id ] ){
				currentTextBox.gameObject.SetActive( false );
			}
		}
		currentTextBox = messageBoxes[ id ];

		// Deactivate all textboxes.
//		ShowAllTextBoxes( false );
		// or DeactiveColor.
		// DeactiveColorTextBoxes();

		// Activate Current.
		if( ! currentTextBox.gameObject.activeInHierarchy ){
			currentTextBox.gameObject.SetActive( true );
		}

		VM.Instance.m_MsgTargetTextBox = currentTextBox;
	}

	public void ForceAutoMode(){
		ViNoAPI.ForceAutoMode();
	}

	public void UnlockForceAutoMode(){
		ViNoAPI.UnlockForceAutoMode();
	}

	public void HideSystemUI(){
//		Debug.Log( "HideSystemUI");
		systemUIRoot.SetActive( false );		
	}

	public void ShowSystemUI(){
		systemUIRoot.SetActive( true );
	}

	public void ShowSystemUI( bool t = true ){
		systemUIRoot.SetActive( t );
	}

	public void ShowSavePanel(){
		saveMode = true;		
		if( saveLoadPanel == null ){			
			return;
		}
		saveLoadPanel.SetActive( true );
	}

	public void ShowLoadPanel(){
		saveMode = false;		
		if( saveLoadPanel == null ){			
			return;
		}
		saveLoadPanel.SetActive( true );
	}

	public void ShowSaveLoadPanel( bool t = true ){
		if( saveLoadPanel == null ){			
			return;
		}
		saveLoadPanel.SetActive( t );
	}

	public void EnableColliderCurrentTextBox( bool enable ){
		if( currentTextBox != null ){
			Collider col = currentTextBox.GetComponent<Collider>();
			col.enabled = enable;
		}
//		else{
//			Debug.Log( "currentTextBox not set." );
//		}
	}

	public void ShowMenuBar( bool show ){
		if( menuBar != null ){
			menuBar.SetActive( show );
		}
	}

	public void ShowMenuButton( bool show ){
		if( menuButton != null ){
			menuButton.SetActive( show );
		}
	}

	public void HideMessage( MessageEventData data ){
		messageBoxes[ data.textBoxID ].gameObject.SetActive( false );
	}

	public void ShowCurrentTextBox( bool show ){
		if( currentTextBox != null ){
			currentTextBox.gameObject.SetActive( show );
		}
	}

	public void ShowAllTextBoxes( bool t ){
		for(int i=0;i<messageBoxes.Length;i++){
			if( currentTextBox != null && currentTextBox.gameObject.activeInHierarchy == t ){
				continue;
			}
			messageBoxes[ i ].gameObject.SetActive( t );
		}		
	}

	public void CheckInstance(){
		if( m_Instance == null ){
			m_Instance = GameObject.FindObjectOfType( typeof( SystemUIEvent )) as SystemUIEvent;	
		}
	}

	void Awake(){
		CheckInstance();

		if( StartAtDeactiveMessages ){
			ShowAllTextBoxes( false );
		}
		if( backLogPanel != null ){
			backLogPanel.SetActive( false );
		}
/*		if( deactiveSysUIAtAwake ){
			systemUIRoot.SetActive( false );
		}
//*/		
	}

	void Start(){
		if( ViNo.autoMode ){
			OnClickAuto();
		}

		if( ViNo.skip ){
			OnClickSkip();
		}
	}

	void Update(){
		m_BackLogEnabled = ViNoBackLog.IsLogStored();
		if( backLogButton != null ){
			if( m_PrevBackLogState != m_BackLogEnabled ){
				if( m_BackLogEnabled && backLogButton != null ){					
					backLogButton.SendMessage( "SetActiveColor" , SendMessageOptions.DontRequireReceiver );
				}
				else{
					backLogButton.SendMessage( "SetDeactiveColor" , SendMessageOptions.DontRequireReceiver );					
				}
			}
		}
		m_PrevBackLogState = m_BackLogEnabled;

		if( backLogPanel != null ){
			if( backLogPanel.activeInHierarchy ){
				return;
			}
		}
/*
		if( currentTextBox != null && menuButton != null ){
			if( ! currentTextBox.reachedEnd ){
				menuButton.SetActive( false );
			}
			else{
				menuButton.SetActive( true );
			}
		}
//*/		
	}

	void ToggleActiveBackLog(){
		backLogPanel.SetActive( ! backLogPanel.activeInHierarchy );		
	}

	void ToggleActiveConfig(){
		configPanel.SetActive( ! configPanel.activeInHierarchy );		
	}

	void ToggleActiveMenuPanel(){
		menuPanel.SetActive( ! menuPanel.activeInHierarchy );		
	}

	void ToggleActiveSaveLoadPanel(){
		saveLoadPanel.SetActive( ! saveLoadPanel.activeInHierarchy );		
	}

	void OnClickMenu( ){
//		Debug.Log( "OnClickMenu");
		ToggleActiveMenuPanel();
	}

	void OnClickQSave(){
//		Debug.Log( "Click QSave" );
		if( ViNoAPI.DoQuickSave() ){
			// Q Save Succeeded.
			BroadcastMessage( "DidQuickSave" , SendMessageOptions.DontRequireReceiver );
		}

//		ToggleActiveMenuPanel();
	}

	void OnClickQLoad(){
//		Debug.Log( "Click QLoad" );
		if( ViNoAPI.DoQuickLoad() ){
			// Q Load Succeeded.
		}
//		ToggleActiveMenuPanel();
	}

	void OnClickSave(){
//		Debug.Log( "Click Save" );

		// Before Calling SaveLoad ColorPanel , saveMode is true.
		saveMode = true;		
#if false
		Application.LoadLevelAdditive( "Continue" );
#else
		saveLoadPanel.SetActive( true );
#endif		
		ToggleActiveMenuPanel();
	}

	void OnClickLoad(){
//		Debug.Log( "Click Load" );

		// Before Calling SaveLoad ColorPanel , saveMode is false ( loadMode ).
		saveMode = false;		
#if false
		Application.LoadLevelAdditive( "Continue" );
#else
		saveLoadPanel.SetActive( true );
#endif		
		ToggleActiveMenuPanel();		
	}

	void OnClickNextMessage(){
		NextMessage();
	}

	void OnClickBackLog(){
//		Debug.Log( "Click BackLog" );
		if( ! m_BackLogEnabled ){
			Debug.Log( "BackLog Data not exists");
			return;
		}
		backLogPanel.SetActive( true );
	}

	void OnClickConfig(){
//		Debug.Log( "Click Config" );

// Test Use GUI.
#if false
		Application.LoadLevelAdditive( "Config" );
#else		
		configPanel.SetActive( true );
#endif
		ToggleActiveMenuPanel();
	}

	void OnClickSkip(){
		// Toggle Skip.
		ViNo.skip = ! ViNo.skip;
		if( autoSkipPanel != null ){
			if( ViNo.skip ){
				autoSkipPanel.SetActiveColor();
			}
			else{
				autoSkipPanel.SetDeactiveColor();
			}
		}
	}

	void OnPressSkip(){
		ViNo.skip = true;
	}

	void OnReleaseSkip(){
		ViNo.skip = false;
	}

	void OnClickLoadLevelTitle(){
		Application.LoadLevel( "Title" );		
	}

	void OnClickToTitle(){
#if false		
		Application.LoadLevel( "Title" );
#else
		ScenarioCtrl.PlayScenario( Application.loadedLevelName , "Title" );
#endif		
		ToggleActiveMenuPanel();
	}

	void OnActivateAuto( bool t ){
		ViNo.autoMode = t;
	}

	// OnClickAuto and Toggle Active Auto mode.
	void OnClickAuto(){
		ViNo.autoMode = ! ViNo.autoMode;
		if( autoModePanel != null ){
			if( ViNo.autoMode ){
				autoModePanel.SetActiveColor();
			}
			else{
				autoModePanel.SetDeactiveColor();
			}		
		}
	}

	void OnClickApplicationQuit(){
		Application.Quit();
	}


	void OnFinishScenario( ){
/*		ScenarioNode theScenario = ScenarioNode.Instance;
		Debug.Log( "Scenario :" + theScenario.name + " was finished." );

		ViNoAPI.PlayScenario( "ScenarioNode2");
//*/		
	}

	// ----------- GETTER ------------------------.

	public bool IsActiveBackLog(){
		if( backLogPanel != null ){
			return backLogPanel.activeInHierarchy;
		}
		else{
			return false;
		}
	}

	public bool IsActiveConfig(){
		if( configPanel != null ){
			return configPanel.activeInHierarchy;
		}
		else{
			return false;
		}
	}

	public bool IsActiveSaveLoadPanel(){
		if( saveLoadPanel != null ){
			return saveLoadPanel.activeInHierarchy;
		}
		else{
			return false;
		}
	}

	public bool IsActiveSystemUI(){
		if( systemUIRoot != null ){
			return systemUIRoot.activeInHierarchy;
		}
		else{
			return false;
		}
	}

	public bool IsActiveMenuPanel(){
		if( menuPanel != null ){
			return menuPanel.activeInHierarchy;
		}
		else{
			return false;
		}
	}

	public bool IsActiveCurrentMessageTarget(){
		if( currentTextBox != null ){
			return currentTextBox.gameObject.activeInHierarchy;
		}
		return false;
	}

	public string GetCurrentMessage(){
		if( currentTextBox != null ){
			return currentTextBox.textShown;
		}
		else{
			return string.Empty;
		}
	}

	public ViNoTextBox GetCurrentTextBox(){
		return currentTextBox;
	}

	/// <summary>
	/// Get TextBox ID by Key.
	/// </summary>
	/// <returns>
	/// <c>id</c> if matching the key <c>-1</c>if not matching.
	/// </returns>
	public int GetTextBoxIDBy( string key ){
		for(int i=0;i<messageBoxes.Length;i++){
			if( messageBoxes[ i ].name == key ){
				return i;
			}
		}
		Debug.LogError( "TextBox:"+key + " not exists.");
		return -1;
	}	

	public ViNoTextBox GetTextBoxBy( string key ){
		for(int i=0;i<messageBoxes.Length;i++){
			if( messageBoxes[ i ].name == key ){
				return messageBoxes[ i ];
			}
		}
		Debug.LogError( "TextBox:"+key + " not exists.");		
		return null;
	}	

	public string GetBeginColorTag( Color col , int textBoxID ){
		return messageBoxes[ textBoxID ].GetBeginColorTag( col );
	}

	public string GetEndColorTag( int textBoxID ){
		return messageBoxes[ textBoxID ].GetEndColorTag();
	}
	
}
