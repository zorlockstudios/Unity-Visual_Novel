//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
using UnityEngine;
using System.Collections;

public class ContinueEvent : MonoBehaviour {
	public string fileName;

	public ViNoTextEventListener dateTextEvtLis;
	public ViNoTextEventListener descriptionTextEvtLis;

//	public UITexture screenshotUITexture;
//	public UIButton continueButton;

	[HideInInspector] public ViNoSaveInfo info;

//	private bool m_MaterialCreated = false;
	private bool m_FileExists = false;

	void UpdateText( string dateStr , string desc ){
//		ViNoSaveInfo info = ScenarioCtrl.Instance.saveInfo;
		// Update Text View.
		if( dateTextEvtLis != null && descriptionTextEvtLis != null ){
			dateTextEvtLis.OnUpdateText( dateStr );
			descriptionTextEvtLis.OnUpdateText( desc );
		}
	}

#if false
	void OnDestroy(){
		// Destroy Allocated Material.
		if( m_MaterialCreated ){
			Destroy( screenshotUITexture.material );
		}
	}
#endif

	void Start(){
		if( info == null ){
			info = ScriptableObject.CreateInstance<ViNoSaveInfo>();
		}

		m_FileExists = ExternalAccessor.IsSaveDataFileExists( fileName );
		if( m_FileExists ){
			// Load SaveInfo from Storage.
			ViNo.LoadDataFromStorage( fileName , ref info) ;
			UpdateText( info.data.m_Date , info.data.m_ScenarioDescription );
		}
		else{
			UpdateText( "" , "NO DATA" );
		}

// NGUI Dependency...		
#if false
		// Load Mode.
		if( ! SystemUIEvent.saveMode ){			
			continueButton.isEnabled = m_FileExists;
		}
#endif		
	}

	void DoSave( bool t ){
		if( t ){
			ViNo.SaveData( fileName , info );
			UpdateText( info.data.m_Date , info.data.m_ScenarioDescription );
		}
	}

	void OnClickContinue(){
		// Attach THIS info.
		ScenarioCtrl.Instance.saveInfo = info;
		ScenarioCtrl.Instance.fileName = fileName;

		// Save.
		if( SystemUIEvent.saveMode ){
#if false				
			if( m_FileExists ){
				Debug.Log( "SaveFileExists");
				if( Application.isEditor ){
					DoSave( true );
				}
				else{
					DialogManager.Instance.ShowSelectDialog( "Overwrite File" , "Are you sure you want to overwrite file ?" , DoSave );
				}
			}
			else{
#endif				
				DoSave( true );
//			}
//			CaptureScreenShot.StartCapture( fileName );
		}
		// Load.
		else{

			// Go to Level and Continue.
			ScenarioCtrl.Instance.DoContinue();

			// Deactive SaveLoadPanel.
			ViNoEventManager.Instance.TriggerEvent( "ToggleActiveSaveLoadPanel" );
			// Destroy Scene-Continue( Close This AdditiveLevel ).
		}

// OBSOLETE .
#if false		
		if( Application.loadedLevelName != "Continue" ){
			if( sceneRoot != null ){
				sceneRoot.SendMessage( "OnClickDestroy" );
			}
		}	
#else
//		this.gameObject.SetActive( false );		
#endif		
	}

#if false

    IEnumerator ProjectScreenShot( string filePath ){    	
		WWW www = new WWW( filePath );

		yield return www;

		if( www.texture != null ){
			screenshotUITexture.mainTexture = www.texture;
		}
	}

	void DispScreenShot(){
		Material newMat = new Material( Shader.Find( "Unlit/Transparent Colored (AlphaClip)" ) );		
		screenshotUITexture.material = newMat;
		m_MaterialCreated = true;
		string screenShotPath = Application.persistentDataPath + "/" + fileName + ".png";
		bool screenShotExists = System.IO.File.Exists( screenShotPath );
		if( screenShotExists ){
			StartCoroutine( "ProjectScreenShot" , "file://" + screenShotPath );
		}
	}
#endif
}
