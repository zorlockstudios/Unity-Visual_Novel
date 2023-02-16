//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

/// <summary>
/// Load level node.
/// </summary>
[ System.Serializable]
[AddComponentMenu("ViNo/Nodes/Scene/Load")]
[ExecuteInEditMode]
public class LoadSceneNode : ViNode {
	public string sceneName;

	public enum Methods{
		DESTROY_AND_LOAD=0,
		ADDITIVE,
	}
	public Methods method = Methods.DESTROY_AND_LOAD;
	public bool withFadeIn = true;

	void Start(){}

#if false				
	public bool _Preview;
	void Update(){
		if( ! Application.isPlaying ){
			if( _Preview ){
				_Preview = false;
//				Object data = Resources.Load( anyResourcePath );
				ViNoSceneManager sm = GameObject.FindObjectOfType( typeof(ViNoSceneManager) ) as ViNoSceneManager;				
				bool destroy = ( method == Methods.DESTROY_AND_LOAD ) ? true : false;
				ViNoToolkit.SceneEvent sceneEvent = GameObject.FindObjectOfType( typeof(ViNoToolkit.SceneEvent)) as ViNoToolkit.SceneEvent;
				if( sceneEvent.sceneLib != null ){
					for(int i=0;i<sceneEvent.sceneLib.sceneEntries.Length;i++){						
						Scene scene = sceneEvent.sceneLib.sceneEntries[ i ];
						if( scene.name == sceneName ){
							Do( sm.theSavedPanel , scene , destroy , true , withFadeIn );
						}
					}
				}
				else{
					Debug.LogWarning( "SceneLibrary not attached to SceneEvent." );
				}
			}
		}
	}
#endif				

	static public void Do( GameObject advSceneRoot , Object scrSceneDataResource , bool destroy , bool destroyImmediate ,  bool fadeIn ){
#if true		
		if( destroy ){
			ClearSceneNode.Do( advSceneRoot , destroyImmediate );
		}	
#endif
		Scene sceneData = scrSceneDataResource as Scene;
		if( sceneData != null ){
			SceneCreator.CreateScene( sceneData , fadeIn );
		}
		else{
			Debug.LogError( "resource cast error ! Not Scene." );
		}
	}

	static public void Do( Object scrSceneDataResource , Hashtable paramHash ){
		bool destroy = false;
		if( paramHash.ContainsKey( "destroy" ) ){
			string destroyStr = paramHash[ "destroy"] as string;
			if( destroyStr == "true" ){
				destroy = true;
			}
		}		

		bool fadeIn = false;
		if( paramHash.ContainsKey( "fade" ) ){
			string fadeInStr = paramHash["fade"] as string;	
			fadeIn = ( fadeInStr == "true" ) ? true : false;
		}
		GameObject advSceneRoot = ViNoSceneManager.Instance.theSavedPanel;
		bool destroyImmediate = true;
		Do( advSceneRoot , scrSceneDataResource , destroy , destroyImmediate , fadeIn );
	}
	
	static public Hashtable MakeHash( string resourcePath , Methods method , bool fadeIn ){
		Hashtable hash = new Hashtable();
		hash["storage"] = resourcePath;
		hash["destroy"] = ( method == Methods.DESTROY_AND_LOAD ) ? "true" : "false";
		hash["fade"] = fadeIn ? "true" : "false";
		return hash;
	}

	public override void ToScenarioScript( ref System.Text.StringBuilder sb ){
		base.ToScenarioScript( ref sb );

		sb.Append( "[enterscene " );
		sb.Append( "name=" + sceneName + " " );
		string destroy = ( method == Methods.DESTROY_AND_LOAD ) ? "true" : "false";
		sb.Append( "destroy=" + destroy + " " );
		string fadeStr = withFadeIn ? "true" : "false";
		sb.Append( "fade=" + fadeStr );
		sb.Append( "]" );
		sb.Append( System.Environment.NewLine );		
	}

	public override void ToByteCode( ByteCodes code  ){				
		base.ToByteCode( code );

		List<byte> byteList  = new List<byte>();		

#if false		
		Hashtable hash = MakeHash( anyResourcePath , method , withFadeIn );
		ByteCodeScriptTools.AddTablePairsCode( byteList , hash );
		code.Add( byteList.ToArray() );
		code.Add( Opcode.LOAD_SCENE );
#else
		Hashtable args = new Hashtable();
		args[ "eventType" ] = "enterscene";		
		args[ "name" ] = sceneName;
		args[ "destroy" ]	= ( method == Methods.DESTROY_AND_LOAD ) ? "true" : "false";
		args[ "fade"	]	= withFadeIn ? "true" : "false";
		ByteCodeScriptTools.AddTablePairsCode( byteList , args );
		ByteCodeScriptTools.AddMessagingCode( byteList , " " , OpcodeMessaging.TRIGGER_EVENT_WITH_ARGS );		
		code.Add( byteList.ToArray() );
#endif
		ToByteCodeInternal( code );		
	}


// TEST SERIAL DESERIAL.
#if false
	public bool _SAVE;
	public bool _LOAD;

	[ System.Serializable]
	public class TestData{
		public string sceneName;
		public Methods method = Methods.DESTROY_AND_LOAD;
		public bool withFadeIn = true;		
	}

	void Serial(){
		TestData dat = new TestData();
		dat.sceneName = sceneName;
		dat.method = method;
		dat.withFadeIn = withFadeIn;
		string xmlStr = ViNoGameSaveLoad.SerializeObject<TestData>( dat );
		ViNoGameSaveLoad.CreateXML( name + ".xml" , xmlStr );
	}

	void Deseri(){
		string xmlStr = ViNoGameSaveLoad.LoadXML( name + ".xml" );

		TestData dat = ViNoGameSaveLoad.DeserializeObject<TestData>( xmlStr ) as TestData;
		sceneName = dat.sceneName;
		method = dat.method;
		withFadeIn = dat.withFadeIn;

		ViNoGameSaveLoad.CreateXML( name + ".xml" , xmlStr );

	}
#endif

}
