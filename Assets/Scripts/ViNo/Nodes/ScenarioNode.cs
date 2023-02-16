//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// The Enabled ScenarioNode is the Current Instance.
/// </summary>
[ System.Serializable ]
[AddComponentMenu("ViNo/Nodes/Scenario/Scenario")]
public class ScenarioNode : FunctionNode {	

	public FlagTable flagTable;

	public bool startFromSave;
	public bool m_PlayAtStart;
	public bool m_PlayOnEnable;
	public bool m_PlayAndLoadFlags;

	public bool useScenarioScript;
	public TextAsset scenarioScript;

	public ViNode startNode;	
	public bool m_CacheCode;		

	[ HideInInspector ] public ViNoSaveInfo saveInfo;	// OBSOLETE.
	[ HideInInspector ] public TextAsset compiledScriptFile;
	[ HideInInspector ] public bool useCompiledScript;		
	[ HideInInspector ] public string startFunc = "start";	
	[ HideInInspector ] public bool playAndSaveFile = false;
	[ HideInInspector ] public string scenarioName = "";
	
	private bool m_Compiled;
//	[ HideInInspector ] public ScriptableSoundData soundData;
	
	static private ScenarioNode instance = null;
	public static ScenarioNode Instance {
		get {	return ScenarioNode.instance;	}
	}				
			
	/// <summary>
	/// Raises the enable event.
	/// </summary>
	void OnEnable(){
		if( m_PlayOnEnable ){
			Play();
		}
	}

	// Use this for initialization
	void Start () {
		// A GameObject Attached ScenarioNode is Cached.
		GOCache.Add( gameObject );
				
		if( m_PlayAtStart ){
			Play ();
		}					
	}

	/// <summary>
	/// Checks the instance.
	/// if Played , the Played Scenario is the instance.
	/// if Awaken , the Awaken Scenairo is the instance.
	/// </summary>
	private void CheckInstance(){
// DialogPartNode OnEnable is off.		

// SOUND DATA IS NOT USED CURRENTLY.		
#if false
		if( soundData != null ){
			ViNoSoundPlayer pl = ISoundPlayer.Instance as ViNoSoundPlayer;
			pl.SetSoundData( soundData );
		}
#endif
		
//		Debug.Log( "ScenarioNode Check Instance.");
/*		if( saveInfo == null ){
			Debug.LogWarning( "SaveInfo not Attached to " + name + "ScenarioNode. The GameState of this ScenarioNode is NOT Saved." ) ;
		}			
//*/		
		if( flagTable == null ){
			Debug.LogWarning( "FlagTable not Attached to " + name + "ScenarioNode. Flag Information is NOT to be Saved." ) ;
		}

		if( ScenarioCtrl.Instance != null 
							&&
			 ScenarioCtrl.Instance.destroyPrevScenario ){		
				if( instance != null &&  instance.gameObject != this.gameObject ){
		//			instance.gameObject.SetActive( false) ;
					Destroy( instance.gameObject );
				}
		}
//		Debug.Log( "ScenarioNode Instance is now :" + this.name );
		instance = this;
		// Play a Scenario and Active. Currently false.
//		instance.gameObject.SetActive( true );
	}
	
	/// <summary>
	/// Compiles this Scenario and set code.
	/// </summary>
	public void CompileAndSetCode( ){
		ByteCodes btcodes;
		btcodes = Compile( );
		VM.Instance.SetCode( btcodes.GetCode() );
	}
			
	void TEST_ADD_CODE( ByteCodes btcodes , BaseNode[] nodes ){
		for( int i=0;i<nodes.Length;i++){
			FunctionNode sn = nodes[ i ] as FunctionNode;			
			if( sn != this && nodes[ i ].enabled ){
				Debug.Log( "node:" + nodes[ i ].name  );
				nodes[ i ].ToByteCode( btcodes );
			}
//			else{
//				Debug.Log( "the Node is Me !" );
//			}
		}	
		btcodes.Add( Opcode.STOP );
	}
#if false
	public void InterpretAndPlay( string scenario ){
		CheckInstance();

		SystemUtility.ClearAllTextBoxMessage();

		VM vm = VM.Instance;
		vm.currentScenarioName = gameObject.name;
					
		ViNoToolkit.KAGInterpreter kag = new ViNoToolkit.KAGInterpreter();
 		List<byte> byteList = kag.Interpret( scenario); 		
		ByteCodes btcodes = new ByteCodes( byteList.ToArray() );
		
		btcodes.Add( Opcode.END );

		vm.SetCode( btcodes.GetCode() );
		vm.Run();
	}
#endif
	/// <summary>
	/// If Playing a ScenarioNode , then the Played Scenario is the Instance.
	/// </summary>
	public void Play(){

		CheckInstance();

		SystemUtility.ClearAllTextBoxMessage();

		VM vm = VM.Instance;
		vm.currentScenarioName = gameObject.name;
					
		// ScenarioScript Mode.
/*		if( useScenarioScript ){
			ViNoToolkit.KAGInterpreter kag = new ViNoToolkit.KAGInterpreter();
	 		List<byte> byteList = kag.Interpret( scenarioScript.text ); 		
			ByteCodes btcodes = new ByteCodes( byteList.ToArray() );
			
			btcodes.Add( Opcode.END );

			vm.SetCode( btcodes.GetCode() );
		}
//*/
		// Default Mode.
//		else{
			ByteCodes btcodes = null;
			if( startNode.gameObject == this.gameObject ){
				btcodes = new ByteCodes();
				btcodes.Init();	
#if true					
				startNode.ToByteCode( btcodes );				
#else				
				TEST_ADD_CODE( btcodes , GetComponents<ViNode>() );
#endif				
			}
			else{
				btcodes = Compile();//useCompiledScript ? new ByteCodes( compiledScriptFile.bytes ) : Compile();
			}

			btcodes.Add( Opcode.END );

			vm.SetCode( btcodes.GetCode() );
//		}

		if( startFromSave ){
			//if ScenarioCtrl exists in this scene, then loaddata from that info.
			ScenarioCtrl sc = ScenarioCtrl.Instance;
			if( sc != null ){
				ViNo.LoadData( sc.fileName , ref sc.saveInfo );
			}
		}
		else{
			if( useScenarioScript ){
				vm.Run();
			}
			else{
				if( startNode == null ){
					vm.Run();		
				}
				else{
					PlayFromStartNode();
				}
			}
		}		
	}
	
	/// <summary>
	/// Play From a Given Node Name's tag.
	/// </summary>
	public void PlayFrom( string tag ){
		CheckInstance();
				
		VM.Instance.GoToLabel( tag );				
	}
	
	/// <summary>
	/// Play From Start.
	/// </summary>
	public void PlayFromStartNode( ){
		CheckInstance();

		string tag = startNode.GetNodeTag( startNode.name );
//		Debug.Log( "PlayFromStart:" + tag );

		VM vm = VM.Instance;
		vm.GoToLabel( tag );		
	}

	/// <summary>
	/// Gets the node tags under this object.
	/// </summary>
	/// <returns>
	/// The node tags under me.
	/// </returns>
	public List<string> GetNodeTagsUnderMe( ){		
		List<string> tagList = new List<string>();
		ViNode[] nodes = GetComponentsInChildren<ViNode>();
		if( nodes != null && nodes.Length > 0 ) {
			for( int i=0;i<nodes.Length;i++){
				DialogPartNode asDlg = nodes[ i ] as DialogPartNode;
				if( asDlg != null ){
					string[] tags = asDlg.GetNodeTags();
					for( int k=0;k<tags.Length;k++){
						tagList.Add( tags[ k ] );
					}
				}
				else{					
					string tag = nodes[ i ].GetNodeTag( nodes[ i ].name );
					tagList.Add ( tag );
				}
			}
		}
		return tagList;
	}

	/// <summary>
	/// To Byte Coded Scripts.
	/// </summary>
	/// <param name='code'>
	/// Code.
	/// </param>
	public override void ToByteCode( ByteCodes code  ){
		ToByteCodeInternal( code );	
	}	
		
#if false
	private string m_ScenarioResourceFilePath;

	public string scenarioResourceFilePath{
		get{ return m_ScenarioResourceFilePath; }
	}

	public void Play( string compiledScenarioFilePath , bool startFrom , string startNodeName = "START" ){
		CheckInstance();
		
		useCompiledScript = true;

		compiledScriptFile = Resources.Load( compiledScenarioFilePath , typeof(TextAsset) ) as TextAsset;
		if( compiledScriptFile != null ){
			VM.Instance.currentScenarioName = gameObject.name;
			VM.Instance.SetCode( compiledScriptFile.bytes );
		}
		else{
			Debug.LogError( "Resource Load Error ! " + compiledScenarioFilePath + " not exists." );
		}

		// Memorize the CompiledScenarioFilePath.
		m_ScenarioResourceFilePath = compiledScenarioFilePath;	

		if( startFrom ){
			PlayFrom( startNodeName );
		}
		else{
			Play();
		}
	}

	/// <summary>
	/// Play From a Node.
	/// </summary>
	public void PlayFrom( ViNode node ){
		CheckInstance();
				
		string tag = node.GetNodeTag( node.name );
		VM.Instance.RunWithTag( tag );				
	}

#endif		
}
