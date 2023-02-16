//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
using UnityEngine;
using System.Collections;

/// <summary>
/// It is required that there is a ViNo Component attached Object in your Scene.
/// </summary>
[ AddComponentMenu( "ViNo/Util/ViNo" )]
public class ViNo : IScriptEngine{	
	
	void OnLevelWasLoaded( int index ){
		if( loadLevelAndClearBackLog ){
			ViNoBackLog.Clear();
		}
	}

	/// <summary>
	/// Internal.
	/// </summary>
	public override IScriptBinder CreateScriptBind( GameObject scriptBindObj ) {						
		// AddComponent DefaultScriptBinder.
		IScriptBinder scriptBinder = scriptBindObj.AddComponent<DefaultScriptBinder>();						
		return scriptBinder;
	}	

	// Protected Functions.	
	public override VirtualMachine CreateVM(){
		Transform thisTra = transform;
			
		GameObject msgHandObj = new GameObject( "_MessagingHandler" );
		MessagingHandler msgHnd = msgHandObj.AddComponent<MessagingHandler>();
		msgHandObj.transform.parent = thisTra;
				
		GameObject vmObj = new GameObject( "_VM" );
		vmObj.transform.parent = thisTra;				
		VM vm = vmObj.AddComponent<VM>();
		vm.m_MessagingHandler = msgHnd;				
		vm.scriptEngineBehaviour = this;
		
		// Dont Show in Hierarchy these objects.
		msgHandObj.hideFlags = HideFlags.HideInHierarchy;
		vmObj.hideFlags = HideFlags.HideInHierarchy;			
		return vm;
	}
		
	static public void NextMessage(){
		if( ! m_LockNextMessage ){
			VM vm = VM.Instance;
			if( vm != null ){
				vm.TextProgress();
			}						
		}
	}
	
	// if save Succeeded , then return true.
	static public bool DoQuickSave(){
		ViNoSaveInfo info = ScenarioCtrl.Instance.quickSaveSlot;//Info;
		if( info == null ){
			Debug.LogError( "ScenarioCtrl not attached QuickSaveSlot. couldn't Save." );
			return false;
		}
		else{
			return ViNo.SaveData( "QSaveData" , info );
		}
	}

	static public bool DoQuickLoad(){
		SystemUtility.ClearAllTextBoxMessage();
		ViNoSaveInfo info = ScenarioCtrl.Instance.quickSaveSlot;
		if( info != null ){
			return ViNo.LoadData( "QSaveData" , ref info );
		}
		else{
			Debug.LogError( "ScenarioCtrl not attached QuickSaveSlot. couldn't Load." );
			return false;
		}
	}			

	/// <summary>
	/// Saves the data. if a Scenario is Played.
	/// </summary>
	static public bool SaveData( string fileName , ViNoSaveInfo info ){
		ScenarioNode scenario = ScenarioNode.Instance;
		if( scenario != null ){
//			if( vino.saveToExternalFile ){	
				// ScenarioNode has a ViNoSaveInfo. saveInfo is a ScriptableObject Data.
			ViNo.SaveToExternalFile( fileName , info , scenario.flagTable );
/*			}
			else{
				ViNo.Save( info );					
			}		
//*/			
			return true;
		}
		else{
			Debug.LogWarning( "ScenarioNode instance NOT FOUND." );
			return false;
		}
	}
	
	/// <summary>
	/// Loads the data.
	/// </summary>
	static public bool LoadData( string fileName , ref ViNoSaveInfo info ){
//		IScriptEngine vino =  IScriptEngine.Instance;				
		ScenarioNode scenario = ScenarioNode.Instance;		
		if( scenario != null ){	
//			if( vino.saveToExternalFile ){
				// Load Flag Data from Storage.
//				FlagTable.FlagUnit[] flags = LoadFlagTable( fileName );
				FlagTableData flagData = LoadFlagTable( fileName );
				if( scenario.flagTable != null ){
#if false					
//					scenario.flagTable.flags = flags;
#else				
					scenario.flagTable.flags = flagData.flags;
					scenario.flagTable.stringValues = flagData.stringValues;
#endif					
				}
				else{
					Debug.LogWarning( "ScenarioNode.Instance " + scenario.name + " flagTable Not attached." );					
				}

				// Load SaveData from Storage.
				LoadDataFromStorage( fileName , ref info );
				return ViNo.Load( info );				
//			}
/*			else{
				return ViNo.Load( info );				
			}		
//*/			
		}
		else{
			Debug.LogWarning( "ScenarioNode instance NOT FOUND." );
			return false;
		}
	}

	static public void LoadDataFromStorage( string fileName , ref ViNoSaveInfo reloadedInfo ){
//		Debug.Log( "FileName:" + fileName);
#if UNITY_WEBPLAYER
		string xmlStr = PlayerPrefs.GetString( fileName + ".xml" );//ViNoGameSaveLoad.LoadXML( fileName + ".xml" );
#else		
		string xmlStr = ViNoGameSaveLoad.LoadXML( fileName + ".xml" );
#endif		
		ViNoSaveData saveData = ViNoGameSaveLoad.DeserializeObject<ViNoSaveData>( xmlStr ) as ViNoSaveData;

		reloadedInfo.data.m_BgmName = saveData.m_BgmName;
		reloadedInfo.data.m_CurrentScenarioName = saveData.m_CurrentScenarioName;
		reloadedInfo.data.m_LoadedLevelIndex = saveData.m_LoadedLevelIndex;
		reloadedInfo.data.m_LoadedLevelName = saveData.m_LoadedLevelName;
		reloadedInfo.data.m_NodeName = saveData.m_NodeName;
		reloadedInfo.data.m_SceneXmlData = saveData.m_SceneXmlData;
		reloadedInfo.data.m_ScenarioResourceFilePath = saveData.m_ScenarioResourceFilePath;
		reloadedInfo.data.m_Date = saveData.m_Date;
		reloadedInfo.data.m_ScenarioDescription = saveData.m_ScenarioDescription;
	}
	
	static public FlagTableData LoadFlagTable( string fileName ){
#if UNITY_WEBPLAYER
		string flagXmlStr = PlayerPrefs.GetString( fileName + "Flag.xml" );
#else		
		string flagXmlStr = ViNoGameSaveLoad.LoadXML( fileName + "Flag.xml" );
#endif
		FlagTableData flagData = ViNoGameSaveLoad.DeserializeObject<FlagTableData>( flagXmlStr ) as FlagTableData;
		return flagData;
//		return flagData.flags;
	}

	/// <summary>
	/// Save the specified info.
	/// </summary>
	/// <param name='info'>
	/// Info.
	/// </param>
	static public void Save( ViNoSaveInfo info ){
		info.data.m_LoadedLevelIndex = Application.loadedLevel;
		info.data.m_LoadedLevelName = Application.loadedLevelName;
				
		// Serialization of Scene.
		if( ViNoSceneManager.Instance != null ){
			info.data.m_SceneXmlData = ViNoSceneManager.Instance.Save( );				
		}
		
		// Serialization of VM. 
		if( VM.Instance != null ){
			VM.SerializationInfo vmSerInfo = VM.Instance.Serialize( );
			info.data.m_NodeName = vmSerInfo.m_NodeName;	
			info.data.m_CurrentScenarioName = vmSerInfo.m_ScenarioName;
		}
		else{
			ViNoDebugger.LogError( "SaveInfo" , "VM NOT Found. Can't serialize VM Info." );	
		}
		
		// Serialization of BGM.
		if( ISoundPlayer.Instance != null ){
			ISoundPlayer pl = ISoundPlayer.Instance;
//			ViNoSoundPlayer pl = ISoundPlayer.Instance as ViNoSoundPlayer;	
			pl.OnSave( info.data );
		}

/*		if( ScenarioNode.Instance != null ){
			info.data.m_ScenarioResourceFilePath = ScenarioNode.Instance.scenarioResourceFilePath;							
		}
//*/

		// Set DateTime.
		info.data.m_Date = ViNoStringExtensions.GetDateTimeNowString();

		// Set Message.
		SystemUIEvent sys = GameObject.FindObjectOfType( typeof(SystemUIEvent) ) as SystemUIEvent;
		string str = sys.GetCurrentMessage();
		if( str.Length >= 14 ){
			str = str.Substring( 0 , 14 ) + "...";
		}
		info.data.m_ScenarioDescription = str;
	}
	
	/// <summary>
	/// Save the specified info and fileName.
	/// </summary>
	/// <param name='info'>
	/// Info.
	/// </param>
	/// <param name='fileName'>
	/// File name. auto stub ".xml".
	/// If UNITY_EDITOR => Application.dataPath + fileName + ".xml".
	/// else => Application.persistentDataPath + fileName + ".xml".
	/// </param>
	static public void SaveToExternalFile( string fileName , ViNoSaveInfo info , FlagTable flagTable ){
		Save ( info );
/*		
		ViNoSaveData data = new ViNoSaveData();
		data.m_BgmName = info.m_BgmName;
		data.m_CurrentScenarioName = info.m_CurrentScenarioName;
		data.m_LoadedLevelIndex = info.m_LoadedLevelIndex;
		data.m_LoadedLevelName = info.m_LoadedLevelName;
		data.m_NodeName = info.m_NodeName;
		data.m_SceneXmlData = info.m_SceneXmlData;
		data.m_Date = info.m_Date;
		data.m_ScenarioDescription = info.m_ScenarioDescription;
//		data.m_ScenarioResourceFilePath = ScenarioNode.Instance.scenarioResourceFilePath;		
//*/
		string xmlStr = ViNoGameSaveLoad.SerializeObject<ViNoSaveData>( info.data );
#if UNITY_WEBPLAYER
		PlayerPrefs.SetString( fileName + ".xml" , xmlStr );								
#else	
		ViNoGameSaveLoad.CreateXML( fileName + ".xml" , xmlStr );								
#endif	
		SaveFlagTable( fileName , flagTable );		
	}	

	static public void SaveFlagTable( string fileName ,  FlagTable flagTable ){
		if( flagTable != null ){
			FlagTableData flagData = new FlagTableData();
			flagData.flags = flagTable.flags;
			flagData.stringValues = flagTable.stringValues;

			string flagXmlStr = ViNoGameSaveLoad.SerializeObject<FlagTableData>( flagData );		
#if UNITY_WEBPLAYER
			PlayerPrefs.SetString( fileName + "Flag.xml" , flagXmlStr );								
#else
			ViNoGameSaveLoad.CreateXML( fileName + "Flag.xml" , flagXmlStr );				
#endif			
		}
	}

	/// <summary>
	/// Load the specified info.
	/// </summary>
	/// <returns>
	/// If Load Succeed return true , Load Failed return false.
	/// </returns>
	static public bool Load( ViNoSaveInfo info ){
		bool levelNameNotMatchThisScene = ! info.data.m_LoadedLevelName.Equals( Application.loadedLevelName );
		if( levelNameNotMatchThisScene ){
			ViNoDebugger.LogError( "SaveData Level Name is : \"" 
					+ info.data.m_LoadedLevelName + "\" but this level is \""+Application.loadedLevelName + "\"" );		
			return false;
		}
		
		// Load Scene from XML.
		if( ViNoSceneManager.Instance != null ){		
			ViNoSceneManager.Instance.Load( info );									
		}
		
		bool haveLevelName = ! string.IsNullOrEmpty( info.data.m_LoadedLevelName );
		bool haveNodeName = ! string.IsNullOrEmpty( info.data.m_NodeName );
		bool isLoad = ( haveLevelName && haveNodeName );										
		if( isLoad ){						
			// Deserialize VM.
			VM vm = VM.Instance;
			if( vm != null ){
				vm.ClearTextBuilder();			

				SystemUtility.ClearAllTextBoxMessage();

				vm.Deserialize( info.data.m_NodeName );		
				
				GameObject scenarioObj = GetScenarioObject( info );

				// Play from File ?.
				ScenarioNode scenario = scenarioObj.GetComponent<ScenarioNode>();				
				scenario.startFromSave = true;
				scenario.PlayFrom( info.data.m_NodeName );
			}
			
			// Load Sound.
			if( ISoundPlayer.Instance != null ){
				ISoundPlayer pl = ISoundPlayer.Instance;
				pl.OnLoad( info.data );
			}
			
			// Deactivate Selections.
			if( ISelectionsCtrl.Instance != null ){
				ISelectionsCtrl.Instance.ChangeActive( false );	
			}				
		}
		return isLoad;
	}		

	static public GameObject GetScenarioObject( ViNoSaveInfo info ){
		string scenarioName = info.data.m_CurrentScenarioName;
		GameObject scenarioObj = GameObject.Find( scenarioName );
		if( scenarioObj == null ){
			Debug.LogWarning( "ScenarioNode object NOT FOUND in scene. now Find in Resources." );
			scenarioObj = ViNoGOExtensions.InstantiateFromResource( scenarioName , null );
			if( scenarioObj == null ){
				Debug.LogError( "ScenarioNode :" + scenarioName + " also Not Found in Resources." );
			}
			else{
				ViNoGOExtensions.StripGameObjectName( scenarioObj , "(Clone)" , "" );
			}
		}
		return scenarioObj;
	}

	/// <summary>
	/// Toggles the show back log.
	/// </summary>
	static public void ToggleShowBackLog(){
		ViNoBackLog.ToggleShow();		
	}



}
