//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// ViNoSceneManager.
/// </summary>
[ ExecuteInEditMode ]
public class ViNoSceneManager : MonoBehaviour {		
	static private ViNoSceneManager instance;
	public static ViNoSceneManager Instance {
		get {	return ViNoSceneManager.instance;	}
	}
	
	public enum SaveMethods{
		COLLECT_VINO_SCENE_NODES=0,
		TRAVERSE_CHILDREN
	}

	public GameObject theSavedPanel;		// saved the objects info under this.
	public ISpriteFactory spriteFactory;	// Attach a ViNoLayerFactory or NGUISpriteFactory prefab.

	[ HideInInspector ] public SaveMethods saveMethod = SaveMethods.TRAVERSE_CHILDREN;	// Saving methods.
	[ HideInInspector ] public bool destroyChildrenOnLoad = true;	// this is always true.

	private SceneData.SceneNodeData[] nodeData;	
	private SceneData m_SceneData;
	private List<SceneData.SceneNodeData> m_NodeDataList;
				
	void Awake(){		
		if( instance == null ){
			instance=  this;					
		    m_SceneData= new SceneData();

		    hideFlags = 0;

		}
		else{
			if( Application.isPlaying ){				
				Destroy( gameObject );		
			}
		}		
	}

	// Use this for initialization
	void Start () {
		// Initialize Node Data List.
		m_NodeDataList = new List<SceneData.SceneNodeData>();		
	}

	static public void FindInstance(){
		if( instance == null ){
			instance = GameObject.FindObjectOfType( typeof( ViNoSceneManager ) ) as ViNoSceneManager;
			instance.m_NodeDataList = new List<SceneData.SceneNodeData>();		
		}
	}
	
	public void SetNodeData( SceneData.SceneNodeData[] data ){
		nodeData = data;
	}	
		
	public void SetActiveColor( string goName ){
		GameObject go = GameObject.Find( goName );
		spriteFactory.OnSetActiveColor( go );
	}

	public void SetDeactiveColor( string goName ){
		GameObject go = GameObject.Find( goName );
		spriteFactory.OnSetDeactiveColor( go );
	}

	public void DestroyObjectsUnderSavedLayer(){
		if( theSavedPanel != null ){
			ViNoGOExtensions.DestroyImmediateChildren( theSavedPanel.name );
		}
	}
			
	public GameObject CreateSceneNode( ref SceneData.SceneNodeData data ){
		GameObject node;		
		string nodePath =  data.parentname + "/" + data.name;
		node = GameObject.Find( nodePath );		
		if( node == null ){
			node = GameObject.Find( data.texturePath );
		}				
		Vector3 pos = new Vector3( data.posX, data.posY , data.posZ  );
		Vector3 scl = new Vector3( data.sclX, data.sclY , data.sclZ );
		GameObject pr = GameObject.Find( data.parentname );
				
		if( spriteFactory != null ){
			node = spriteFactory.Create( ref data , node , pos , scl , pr );	
		}
		if( node != null ){			
			data.name = node.name;
		}
		return node;
	}
	
	public void CreateSceneNodes(){
		 for( int i=0;i<nodeData.Length;i++){	
			SceneData.SceneNodeData data = nodeData[ i ];
			CreateSceneNode( ref data );			
		 }	
	}
	
	public void SetSceneData( SceneData data ){
		m_SceneData = data;
	}
					
	/// <summary>
	/// Return XML string of Nodes Data.
	/// </summary>
	public string Save(){
		return ViNoGameSaveLoad.Save();
	}
		
	/// <summary>
	/// Load the specified info.
	/// </summary>
	/// <param name='info'>
	/// Info.
	/// </param>
	public void Load( ViNoSaveInfo info ){
		VM vm = VM.Instance;
		if( vm == null ){			
			ViNoDebugger.LogError( "VM Not Found . Can't Load." );
		}


		switch( saveMethod ){
		 case SaveMethods.COLLECT_VINO_SCENE_NODES:

		 	break;

		 case SaveMethods.TRAVERSE_CHILDREN:
		 	if( destroyChildrenOnLoad ){
				if( theSavedPanel != null ){
					ViNoGOExtensions.DestroyImmediateChildren( theSavedPanel.name );
				}
			}
			break;
		}
		
		ViNoGameSaveLoad.Load ( info );								
	}
	
	/// <summary>
	/// Saves the panel children.
	/// </summary>
	public void SaveSceneNodes(){	
		m_NodeDataList.Clear();
		switch( saveMethod ){
		 case SaveMethods.COLLECT_VINO_SCENE_NODES:
/*			ViNoSceneNodeFactory vsfact = spriteFactory as ViNoSceneNodeFactory;
			if( vsfact != null ){
				ViNoSceneSaveUtil.SaveViNoSceneNodes( ref m_NodeDataList ,  spriteFactory );	
			}
		 	break;
//*/
		 case SaveMethods.TRAVERSE_CHILDREN:
			if( theSavedPanel != null ){
				ViNoSceneSaveUtil.SavePanelChildren( theSavedPanel , ref m_NodeDataList , ref spriteFactory );
			}
			else{
				Debug.LogError( "theSavedPanel not set" );
			}
			break;
		}

		// For Saving as Xml...
		m_SceneData.m_DataArray	 = m_NodeDataList.ToArray();
		
		m_NodeDataList.Clear();
	}	

	public void PreFadeoutAndDestroy( GameObject root ){ 
		spriteFactory.PreFadeoutAndDestroy( root );
	}

	public void OnFadeoutAndDestroy( GameObject root ){ 
		spriteFactory.OnFadeoutAndDestroy( root );
	}
		
	// * Linear Search From Name. *//
	// IF NOT FOUND RETURN NULL.
	public SceneData.SceneNodeData GetNodeData( string name ){
		// RETURN LOADED Scene Node Data.
		foreach( SceneData.SceneNodeData data in m_NodeDataList ){
			if( data.name.Equals( name ) || data.texturePath.Equals( name ) ){
				return data;
			}
		}
		return null;	
	}

	public SceneData GetSceneData(){
		return m_SceneData;	
	}
	
	
}
