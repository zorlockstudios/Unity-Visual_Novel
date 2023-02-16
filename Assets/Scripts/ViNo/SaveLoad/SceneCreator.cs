//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ViNoToolkit;

[ ExecuteInEditMode ]
public class SceneCreator : MonoBehaviour {
	static private SceneCreator m_Instance;
	static public SceneCreator Instance{
		get{
			return m_Instance;
		}
	}

	public bool awakeAndDestroy;
	public float fadeoutAndDestroyWaitTime = 1f;

	static private bool m_DestroyRequested;			// Destroy Calling Locker .

	static public SceneData.SceneNodeData CreateNodeData( Hashtable attrTable ){
		SceneData.SceneNodeData data = new SceneData.SceneNodeData();
		data.name = attrTable["name"] as string;
		data.parentname = attrTable["parentname"] as string;

		ParamUtil.SetFloatAttr( out data.posX , "posX" , attrTable , 0f );
		ParamUtil.SetFloatAttr( out data.posY , "posY" , attrTable , 0f );
		ParamUtil.SetFloatAttr( out data.posZ , "posZ" , attrTable , 0f );

		ParamUtil.SetFloatAttr( out data.rotX , "rotX" , attrTable , 0f );
		ParamUtil.SetFloatAttr( out data.rotY , "rotY" , attrTable , 0f );
		ParamUtil.SetFloatAttr( out data.rotZ , "rotZ" , attrTable , 0f );

		ParamUtil.SetFloatAttr( out data.sclX , "sclX" , attrTable , 1f );
		ParamUtil.SetFloatAttr( out data.sclY , "sclY" , attrTable , 1f );
		ParamUtil.SetFloatAttr( out data.sclZ , "sclZ" , attrTable , 1f );

		data.uiAtlasName = attrTable["uiAtlasName"] as string;
		data.texturePath = attrTable["texturePath"] as string;
		ParamUtil.SetFloatAttr( out data.alpha , "alpha" , attrTable , 1f );
		if( attrTable.ContainsKey( "show") ){
			data.show = ViNoStringExtensions.IsTrueOrYes( attrTable["show"] as string  );
		}
		if( attrTable.ContainsKey( "makePixelPerfect") ){
			data.makePixelPerfect = ViNoStringExtensions.IsTrueOrYes( attrTable["makePixelPerfect"] as string  );
		}	
		return data;	
	}

	static public void CreateScene( Scene sceneData , bool fadeInStart ){
//		Debug.Log( "Now , Create Scene ...");
		ISpriteFactory._FADEIN_AT_CREATE = fadeInStart;
/*		if( Instance._LATE_CREATE_SCENE ){		
			Instance.StartCoroutine( "LateCreateScene" , sceneData );
		}
		else{
//*/
		if( m_DestroyRequested ){
			Instance.StartCoroutine( "CreateSceneWhenDestroyed" , sceneData );
		}
		else{
			Create( sceneData );
		}
//		}
	}

	static public GameObject Create( SceneData.SceneNodeData data ){
		ViNoSceneManager sm = GameObject.FindObjectOfType( typeof( ViNoSceneManager) ) as ViNoSceneManager;
		GameObject go =  sm.CreateSceneNode( ref data );
		return go;
	}

	static public void Create( Scene sceneData ){
//		Debug.Log( "CreateScene");
		ViNoSceneManager sm = GameObject.FindObjectOfType( typeof( ViNoSceneManager) ) as ViNoSceneManager;
		for( int i=0;i<sceneData.sceneNodesData.Length;i++){
			sm.CreateSceneNode( ref sceneData.sceneNodesData[ i ] );
		}		
	}

	// root is not destroyed.
	static public void DestroyScene( GameObject advSceneRoot , bool immediate ){
		if( Application.isPlaying ){				
			if( immediate ){				
				DestroySceneImmediate( advSceneRoot );
			}
			else{
				if( m_DestroyRequested ){
//					Debug.Log( "<color=cyan>Destroy Already Requested...</color>");
					return;
				}
				if( advSceneRoot.transform.childCount > 0 ){
					VM vm = VM.Instance;
					if( vm != null && Application.isPlaying ){
						vm.EnableUpdate( false );
					}
//					Debug.Log( "<color=cyan>Start Destroying...</color>");
					Instance.StartCoroutine( "FadeoutAndDestroy" , advSceneRoot );
				}
				else{
					m_DestroyRequested = false;
				}
			}
		}
		else{
			DestroySceneImmediate( advSceneRoot );
		}
	}

	static public void DestroySceneImmediate( GameObject advSceneRoot ){
		// Destroy Created Materials to avoid Leaking .
//		Debug.Log( "Destroy Root:" + advSceneRoot.name );

		ViNoSceneManager sm = GameObject.FindObjectOfType( typeof(ViNoSceneManager) ) as ViNoSceneManager;
		sm.OnFadeoutAndDestroy( advSceneRoot );

		// Destroy Under ME.
		ViNoGOExtensions.DestroyImmediateChildren( advSceneRoot.name );	
	}

	IEnumerator CreateSceneWhenDestroyed( Scene sceneData ){
//		VM vm = VM.Instance;
//		if( vm != null && Application.isPlaying ){
//			vm.EnableUpdate( false );
		if( Application.isPlaying ){
			ViNoAPI.EnableUpdateADV( false );
		}

		while( m_DestroyRequested ){
			Debug.Log( "waiting for Cleaning Up the scene...");
			yield return new WaitForSeconds( 0.01f );
		}
//		Debug.Log( "CreateScene CreateSceneWhenDestroyed");
//		Debug.Log( "_FADEIN_AT_CREATE:" + _FADEIN_AT_CREATE.ToString() );
		Create( sceneData );
//		if( vm != null && Application.isPlaying ){
//			vm.EnableUpdate( true );
		
		if( Application.isPlaying ){
			ViNoAPI.EnableUpdateADV( true );
		}
	}

	IEnumerator FadeoutAndDestroy( GameObject destroyRoot ){
		m_DestroyRequested = true;
		VM vm = VM.Instance;
/*		if( vm != null && Application.isPlaying ){
			vm.EnableUpdate( false );
		}
//*/
//		SystemUtility.ShowSystemUI( false );

		ViNoSceneManager sm = ViNoSceneManager.Instance;
		sm.PreFadeoutAndDestroy( destroyRoot );//sm.theSavedPanel );

		yield return new WaitForSeconds( fadeoutAndDestroyWaitTime );

		sm.OnFadeoutAndDestroy( destroyRoot );//sm.theSavedPanel );

//		Debug.Log("Now , Destroy Scene");
		// Destroy Under ME.
		ViNoGOExtensions.DestroyChildren( destroyRoot );//Instance.name );	

		if( vm != null && Application.isPlaying ){
			vm.EnableUpdate( true );
		}

//		SystemUtility.ShowSystemUI( true );
		m_DestroyRequested = false;
	}

	void Awake(){
		m_Instance = this;
		if( Application.isPlaying ){
			if( awakeAndDestroy ){
	//			Debug.Log( "Awake and Destroy ADV SCENE...");
				DestroySceneImmediate( this.gameObject );
			}
		}
	}

#if false	

//	public bool _INIT_SCENE_AT_START;
//	public bool _LATE_CREATE_SCENE;
//	public float lateCreateWaitSeconds = 0.5f;

/*
	IEnumerator LateCreateScene( Scene sceneData ){
		VM vm = VM.Instance;
		if( vm != null && Application.isPlaying ){
			vm.EnableUpdate( false );
		}

		ViNoSceneManager sm = GameObject.FindObjectOfType( typeof( ViNoSceneManager) ) as ViNoSceneManager;
		for( int i=0;i<sceneData.sceneNodesData.Length;i++){			
			sm.CreateSceneNode( ref sceneData.sceneNodesData[ i ] );
			yield return new WaitForSeconds( lateCreateWaitSeconds );
		}

		if( vm != null && Application.isPlaying ){
			vm.EnableUpdate( true );
		}
	}
//*/

	void InitScene(){
		ClearTextureUnderChildren();
		SetPanelsAlpha( 0f );
	}

	void ClearTextureUnderChildren(){
		MeshRenderer[] rens = GetComponentsInChildren<MeshRenderer>();
		for( int i=0;i<rens.Length;i++){
			if( rens[ i ].sharedMaterial != null ){
				rens[ i ].sharedMaterial.mainTexture = null;
			}
		}		
	}

	void SetPanelsAlpha( float a ){
		ColorPanel[] panels = GetComponentsInChildren<ColorPanel>();
		for( int i=0;i<panels.Length;i++){
			panels[ i ].alpha = a;
			panels[ i ].UpdateColor();
		}
	}

	// For Testing.
	// Auto Alignment Object except BG.
	public ViNoGrid grid;
	public bool _AUTO_ALIGN;

	public bool _SET_PANEL_ALPHA_ZERO;
	public bool _CLEAR_TEXTURE_UNDER_CHILDREN;
	public Scene _SCENE_DATA;
	public bool create;
	public bool destroy;	// If Checked destroy and start , there is a bug when Starting a Scenario .

	// Update is called once per frame
	void Update () {
		if( create ){
			create = false;
			SceneCreator.CreateScene( _SCENE_DATA , true );
			if( _AUTO_ALIGN ){
				AlignUnderThisObject();
			}
		}
		
		if( destroy ){
			destroy = false;
			SceneCreator.DestroyScene( gameObject , false );
		}

		if( _SET_PANEL_ALPHA_ZERO ){
			_SET_PANEL_ALPHA_ZERO = false;
			SetPanelsAlpha( 0f );
		}

		if( _CLEAR_TEXTURE_UNDER_CHILDREN ){
			_CLEAR_TEXTURE_UNDER_CHILDREN = false;
			ClearTextureUnderChildren();
		}
	}

	void AlignUnderThisObject(){
		// Align Game Objects.
		Transform tra = transform;
		int childNum = tra.GetChildCount();
		List<GameObject> widgetList = new List<GameObject>();
		for( int i=0;i<childNum;i++){
			Transform child = tra.GetChild( i );
			if( child.gameObject.tag != "BG" ){
				widgetList.Add( child.gameObject );
			}
		}
		grid.widgets = widgetList.ToArray();
		if( grid.widgets != null ){
			int widgetNum = grid.widgets.Length;
			Vector3 pos = tra.localPosition;
			if( widgetNum == 1 ){
				tra.localPosition = Vector3.zero;
			}
			else if( widgetNum == 2 ){
				tra.localPosition = new Vector3( - grid.width /2f , pos.y , pos.z );
			}
			else if ( widgetNum > 2 ){
				tra.localPosition = new Vector3( - grid.width , pos.y , pos.z );				
			}
			grid.Reposition();
		}
	}
#endif		

}
