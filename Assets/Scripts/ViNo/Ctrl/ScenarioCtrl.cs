//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
using UnityEngine;
using System.Collections;

public class ScenarioCtrl : MonoBehaviour {	
	public enum ExecuteMode{
		HIERARCHY=0,
		KAG_INTERPRETER,		
	}

	public ViNoSaveInfo newGameInfo;		// if tapped NewGame , this is used.
	public ViNoSaveInfo quickSaveSlot;			// SaveInfo. Already Loaded from Storage.
	public ViNoSaveInfo saveInfo;			// SaveInfo. Already Loaded from Storage.

 	[HideInInspector] public bool destroyPrevScenario = true;	// Destroy ScenarioNode object when another ScenarioNode is Played.

 	public bool useMouseWheelScroll = true;
 	public bool useKeyboard = true;

	static private bool m_LoadLevelAndStartScenario;
	static private bool m_LoadLevelAndNewGame;

	static private ScenarioCtrl m_Instance;
	static public ScenarioCtrl Instance{
		get{ return m_Instance; }
	}

	public string fileName{ get; set; }
	
	static public void PlayScenario( string levelName , string scenarioName ){
		if( levelName != Application.loadedLevelName ){// currentlyLoadedLevelName ){
			Debug.LogWarning( "ScenarioCtrl couldn't play because LoadLevelName not match." );				
			return;			
		}

		// Get ScenarioObject.
		GameObject scenarioObj = GameObject.Find( scenarioName );
		bool isFromResource = false;
		if( scenarioObj == null ){
			Debug.LogWarning( "ScenarioNode object NOT FOUND in scene. now Find in Resources." );
			scenarioObj = ViNoGOExtensions.InstantiateFromResource( scenarioName , null );
			if( scenarioObj == null ){
				Debug.LogError( "ScenarioNode :" + scenarioName + " also Not Found in Resources." );
			}
			else{
				isFromResource = true;
				ViNoGOExtensions.StripGameObjectName( scenarioObj , "(Clone)" , "" );
			}
		}
		ScenarioNode scenario = scenarioObj.GetComponent<ScenarioNode>();
		if( scenario != null ){
			if( m_LoadLevelAndStartScenario ){
				scenario.startFromSave = true;
			}
			if( ! isFromResource ){
				scenario.Play();
			}
		}
		else{
			Debug.LogError( "Scenario :" + scenarioName + "object not attached a ScenarioNode." );
		}
		m_LoadLevelAndStartScenario = false;
		m_LoadLevelAndNewGame = false;	
	}

	static public void PlayScenario( ViNoSaveInfo info ){
		if( info.data == null  ){
			Debug.LogWarning( "ScenarioCtrl couldn't play because ViNoSaveInfo not attached." );				
			return;
		}		
		string levelName = info.data.m_LoadedLevelName;
		string scenarioName = info.data.m_CurrentScenarioName;
		PlayScenario( levelName , scenarioName );
	}

	public void LoadLevelAndNewGame(){
		ViNoSaveInfo info = m_Instance.newGameInfo;
		if( ! string.IsNullOrEmpty( info.data.m_LoadedLevelName ) ){
			m_LoadLevelAndNewGame = true;
			// If this is the SaveInfo Level Name , Play Scenario at once.
			if( info.data.m_LoadedLevelName == Application.loadedLevelName ){
			}
			else{
				Application.LoadLevel( info.data.m_LoadedLevelName );
			}
		}
		else{
			Debug.LogError( "Level Name is Empty !" );
		}
	}

	public void DoNewGame(){
		ViNoSaveInfo info = m_Instance.newGameInfo;
		PlayScenario( info );				
	}

	public void DoContinue(){
		ViNoSaveInfo info = m_Instance.saveInfo;
		if( ! string.IsNullOrEmpty( info.data.m_LoadedLevelName ) ){
			m_LoadLevelAndStartScenario = true;
			// If this is the SaveInfo Level Name , Play Scenario at once.
			if( info.data.m_LoadedLevelName == Application.loadedLevelName ){
				PlayScenario( info );				
			}
			// LoadLevel and Play Scenario.
			else{
				Application.LoadLevel( info.data.m_LoadedLevelName );
			}
		}
	}

	void Awake(){
		if( m_Instance == null ){
			m_Instance = this;			
			DontDestroyOnLoad( this.gameObject );
		}
		else{
			Destroy( gameObject );
		}
	}

// Platform StandAlone Key Input.
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER
	// Update is called once per frame
	void Update () {
		SystemUIEvent sys = SystemUIEvent.Instance;
		if( sys != null ){
			ISelectionsCtrl sel = ISelectionsCtrl.Instance;
			if( sel != null && sel.IsActive() ){
				return;
			}

			// Handle Keyboard.
			if( useKeyboard ){				
				if( Input.GetKeyDown( KeyCode.Return ) ){
					if( sys.IsActiveCurrentMessageTarget() ){
						ViNoAPI.NextMessage();
					}
				}

				if( Input.GetKeyDown( KeyCode.Escape ) ){
					if( sys.IsActiveBackLog() ){
						ViNoEventManager.Instance.TriggerEvent( "ToggleActiveBackLog" );
					}
					else if( sys.IsActiveConfig() ){
						ViNoEventManager.Instance.TriggerEvent( "ToggleActiveConfig" );						
					}
					else if( sys.IsActiveSaveLoadPanel() ){
						ViNoEventManager.Instance.TriggerEvent( "ToggleActiveSaveLoadPanel" );						
					}
					else{
						ViNoEventManager.Instance.TriggerEvent( "ToggleActiveMenuPanel" );
					}
				}

				if( Input.GetKeyDown( KeyCode.UpArrow ) ){
					ViNoEventManager.Instance.TriggerEvent( "OnClickBackLog" );
				}
				else if( Input.GetKeyDown( KeyCode.DownArrow ) ){
					if( sys.IsActiveCurrentMessageTarget() ){
						ViNoAPI.NextMessage();						
//						ViNoEventManager.Instance.TriggerEvent( "ToggleActiveSaveLoadPanel" );
					}					
				}

				// Menu is Activated.
				bool isActiveMenuPanel = sys.IsActiveMenuPanel();
				if( isActiveMenuPanel ){
					if( Input.GetKeyDown( KeyCode.S ) ){
						ViNoEventManager.Instance.TriggerEvent( "OnClickSave" );
					}
					else if( Input.GetKeyDown( KeyCode.L ) ){
						ViNoEventManager.Instance.TriggerEvent( "OnClickLoad" );
					}
					else if( Input.GetKeyDown( KeyCode.P ) ){
						ViNoEventManager.Instance.TriggerEvent( "ToggleActiveConfig" );
					}
				}
				// Normal.
				else{
					if( Input.GetKeyDown( KeyCode.S ) ){
						ViNoEventManager.Instance.TriggerEvent( "ShowSavePanel" );
					}
					else if( Input.GetKeyDown( KeyCode.L ) ){
//						ViNoEventManager.Instance.TriggerEvent( "OnClickLoad" );
						ViNoEventManager.Instance.TriggerEvent( "ShowLoadPanel" );
					}
					else if( Input.GetKeyDown( KeyCode.A ) ){
						ViNoEventManager.Instance.TriggerEvent( "OnClickAuto" );
					}

/*					else if( Input.GetKeyDown( KeyCode.S ) ){
						ViNoEventManager.Instance.TriggerEvent( "OnClickSkip" );
					}
					else if( Input.GetKeyDown( KeyCode.L ) ){
						ViNoEventManager.Instance.TriggerEvent( "ToggleActiveBackLog" );
					}
//*/					
				}

				if( Input.GetKeyDown( KeyCode.Space ) ||
					 Input.GetKeyDown( KeyCode.RightArrow ) || Input.GetKeyDown( KeyCode.LeftArrow ) )
				{
					bool t = sys.IsActiveSystemUI( );
					sys.ShowSystemUI( ! t );
				}
			}

			// Handle Mouse Wheel. 
			if( useMouseWheelScroll ){
				float scroll = Input.GetAxis( "Mouse ScrollWheel" );

				bool isActiveSystem = sys.IsActiveSystemUI( );

				// Wheel Up and show BackLog.
				if( isActiveSystem && scroll > 0 ){
					ViNoEventManager.Instance.TriggerEvent( "OnClickBackLog" );
				}
				// Wheel Down and Next Message.
				else if( scroll < 0 ){
					if( isActiveSystem && sys.IsActiveCurrentMessageTarget() ){
						ViNoAPI.NextMessage();			
					}
				}						
			}
		}

	}
#endif		

	void OnLevelWasLoaded( int index ){
		Debug.Log( "OnLevelWasLoaded" + index.ToString() );
		if( m_Instance == null ){
			Debug.LogWarning( "ScenarioCtrl Instance Not Found." );			
			return;
		}
		Debug.Log( "Quick Save Data Cleared." );		
		
		if( m_LoadLevelAndNewGame || m_LoadLevelAndStartScenario ){
			ViNoSaveInfo info = m_LoadLevelAndNewGame ? m_Instance.newGameInfo : m_Instance.saveInfo;
			PlayScenario( info );
		}
	}
}
