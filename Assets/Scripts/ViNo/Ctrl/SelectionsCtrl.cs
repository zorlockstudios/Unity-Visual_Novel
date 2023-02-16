using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SelectionsCtrl : ISelectionsCtrl {
	public ViNoTextBox titleTextBox;
	public TextMesh m_TitleText;
	public GameObject selectionPrefab;	
	public bool hideSystemUIWhileEnabled;
	public GameObject deactiveObjWhileShowing;

	protected int _SELECTION_CACHE_NUM = 4;
	protected GameObject[] m_SelectionCache;	
	protected Dictionary<string,SelectionUnit> m_SelectionDict;
	protected int m_CurrSelectIndex = 0;

	private bool m_IsSkip;

	void OnEnable(){
		if( deactiveObjWhileShowing != null ){			
			deactiveObjWhileShowing.SetActive( false );
		}

		if( hideSystemUIWhileEnabled ){
//			SystemUtility.ShowSystemUI( false );			
			SystemUIEvent sys = SystemUIEvent.Instance;
			if( sys != null ){
				sys.HideSystemUI();
			}
		}
		m_IsSkip = ViNo.skip;

		OnEnabled();
	}

	void OnDisable(){
		
		OnDisabled();

		if( hideSystemUIWhileEnabled ){
//			SystemUtility.ShowSystemUI( true );			
			SystemUIEvent sys = SystemUIEvent.Instance;
			if( sys != null ){
				sys.ShowSystemUI( true );			
			}
		}

		if( deactiveObjWhileShowing != null ){			
			deactiveObjWhileShowing.SetActive( true );			
		}		

		if( m_IsSkip ){
			ViNoEventManager.Instance.TriggerEvent( "OnClickSkip" );
			ViNo.skip = false;
		}
	}

	public override void SetTitle( string str ){
		if( m_TitleText != null ){
			m_TitleText.text = str;
		}
		if( titleTextBox != null ){
			titleTextBox.SetText( str );
		}
	}	

	public override void ChangeActive( bool t ){
//		Debug.Log( "ChangeActive");
		if( t ){
			m_SelectionDict = new Dictionary<string,SelectionUnit>();
		}
		// inactive then selections destroyed.
		else{
			DeleteSelections();
		}

		if( t ){
			SystemUtility.ShowSystemUI( false );
		}
		else{
			SystemUtility.ShowSystemUI();			
		}

		gameObject.SetActive( t );
	}

	public override void DeleteSelections(){
		m_CurrSelectIndex = 0;
		if( m_SelectionDict != null ){
			m_SelectionDict.Clear();
			m_SelectionDict = null;	
			for( int i=0;i<_SELECTION_CACHE_NUM;i++){
				if( m_SelectionCache != null ){
					m_SelectionCache[ i ].SetActive( false );
				}
			}
		}
	}

	/// <summary>
	/// Creates the selection instance ( Cache Selection Objects ).
	/// </summary>
	public void CreateSelectionInstance( ){
		m_SelectionCache = new GameObject[ _SELECTION_CACHE_NUM ];
		for( int i=0;i<_SELECTION_CACHE_NUM;i++){
			GameObject selection = (GameObject)Instantiate( selectionPrefab );				
			ViNoGOExtensions.InitTransform( selection , this.gameObject );						
			m_SelectionCache[ i ] = selection;
			selection.name = "select-" + i;
			selection.SetActive( false );								
		}							
	}

	/// <summary>
	/// Adds the selection.
	/// </summary>
	/// <param name='paramHash'>
	/// Parameter hash.
	/// </param>
	public override void AddSelection( ref Hashtable paramHash  ){
		// FLAG CHECK TEST.
		if( paramHash.ContainsKey( "IsFlagOn" ) ){
			string flagName = paramHash[ "IsFlagOn" ] as string;			
			
			// If not match the Flag return.
			if( ! ScenarioNode.Instance.flagTable.CheckFlagBy( flagName ) ){
				return;
			}
		}
				
		if(  paramHash.ContainsKey( "target" )  &&
			 paramHash.ContainsKey( "text" ) ){			
			string target = paramHash[ "target" ] as string;
			string title = paramHash[ "text" ] as string;				
			AddSelection( title , target );					
		}

		SystemUtility.EnableColliderCurrentTextBox( false );		
	}
		
	/// <summary>
	/// Raises the click select callback event.
	/// </summary>
	/// <param name='obj'>
	/// Object.
	/// </param>
	public void OnClickSelectCallback( GameObject obj ){
		string objName = obj.name;
		ViNoDebugger.Log( "SELECTION" , "click callback obj : " + objName );
		if( m_SelectionDict == null ){
			return;
		}
		if( m_SelectionDict.ContainsKey( objName ) ){
			string destNodeName = m_SelectionDict[ objName ].m_Target;
			if( ! destNodeName.Equals( string.Empty ) ){
				if( ScenarioNode.Instance.flagTable != null ){
					ScenarioNode.Instance.flagTable.SetFlagBy( "SELECTED/" + destNodeName , true );				
				}
				VM.Instance.GoToLabel( m_SelectionDict[ objName ].m_Target );	// Run with Tag.
			}
			else{
				Debug.LogError( "SelectionUnit Target must not be empty ." );							
			}
		}
		else{
			Debug.LogError( "SelectionUnit  NOT FOUND in SelectionDictionary ." );	
		}
		SystemUtility.EnableColliderCurrentTextBox( true );

		DeactivateSelections();

		ChangeActive( false );	
	}		
}
