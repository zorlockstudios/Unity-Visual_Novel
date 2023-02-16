//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ViNoToolkit{

	public class SceneEvent : MonoBehaviour {
// TEST...

		[HideInInspector] public Material portraitMat1;
//public Material portraitMat2;
//		public Vector3 actorPos;

		public ActorLibrary actorLib;			// attach a ActorLibrary in project view.
		public SceneLibrary sceneLib;

		[HideInInspector] public bool highlightCurrentSpeaker;

		[ Range( -600f , 600f ) ]
		public float centerPosX = 0f;
		[ Range( -600f , 600f ) ]
		public float leftPosX = -180f;
		[ Range( -600f , 600f ) ]
		public float rightPosX = 180f;
		[ Range( -600f , 600f ) ]
		public float middleLeftPosX = -150f;
		[ Range( -600f , 600f ) ]
		public float middleRightPosX = 150f;

		// left , mid_left , center , mid_right , right .
		public enum ActorPosition{
			center=0,
			left,
			right,
			middle_left,
			middle_right,
		}

		private Dictionary<string,ActorInfo> m_ActorMap;
		private Dictionary<string,Scene> m_SceneMap;

//		private Dictionary<string,GameObject> m_ActorGOMap;

		void definescene( Hashtable param ){
			string sceneName = param["name"] as string;
			string texturePath = param["texture"] as string;
/*			if( param.ContainsKey( "size" ) ){
				sizeInPercent = float.Parse( param["size"] as string );
			}	
//*/			
			Scene info = ScriptableObject.CreateInstance<Scene>();
			m_SceneMap[ sceneName ] = info;

			string[] texs = new string[ 1 ];
			texs[ 0 ] = texturePath;
			ViNoSceneUtil.SetUpSceneInfo( info , sceneName , texturePath );	 		
		}

		// Test Define character. 
		void defineactor( Hashtable param ){
			string actorName = param["name"] as string;
//			string textColorStr = param["textColor"] as string;
			string texturePath = param["texture"] as string;
			float sizeInPercent = 100f;
			if( param.ContainsKey( "size" ) ){
				sizeInPercent = float.Parse( param["size"] as string );
			}	

//			Color textColor = C
			ActorInfo info = ScriptableObject.CreateInstance<ActorInfo>();
			m_ActorMap[ actorName ] = info;

			string[] texs = new string[ 1 ];
			texs[ 0 ] = texturePath;//Resources.Load( texturePath , typeof(Texture2D) ) as Texture2D;
			ActorUtility.SetUpActorInfo( info , actorName , sizeInPercent , Color.cyan , texs );	 		
		}

		void jump( Hashtable param ){
		 	string label = param[ "target"] as string;
		 	label = label.Replace( "*" , "" );
		 	if( param.ContainsKey( "storage" ) ){
			 	string storage = param[ "storage" ] as string;
			 	Debug.Log( "Now , Play scenario:" + storage);
			 	ViNoAPI.PlayScenario( storage );
		 	}
		 	Debug.Log( "label:" + label );
		 	VM.Instance.GoToLabel( label );
		}

		void if_( Hashtable param ){
		 	string exp = param[ "exp"] as string;
		 	Debug.Log( "exp:" + exp );
		}

		void endif( Hashtable param ){
			Debug.Log( "ENDIF");

		}

		void eval( Hashtable param ){
		 	string exp = param[ "exp"] as string;
		 	Debug.Log( "exp:" + exp );
#if false		 	
			exp = exp.Replace( "\"" , "" );
			string varName1 = string.Empty;
			string varName2 = string.Empty;
			string[] strs = exp.Split( "=="[0] );
			bool processed = false;
			// == or !=.
			if( strs.Length >= 2 ){
				processed = true;
				varName1 = strs[ 0 ];
				varName2 = strs[ 1 ];
			}
			else{
				strs = exp.Split( "!="[0] );
				if( strs.Length >= 2 ){
					processed = true;

				}
			}

			if( ! processed ){

			}

			ScenarioNode scenario = ScenarioNode.Instance;
			if( scenario != null && scenario.flagTable != null ){

//				scenario.flagTable.SetStringValue(  );
			}
#endif
		}

		void enterscene( Hashtable param ){
			string sceneName = param[ "name" ] as string;
			// Attached a sceneLib.
//			if( sceneLib != null ){
//				Debug.Log( "OnEnterScene :" + sceneName );
//				for( int i=0;i<sceneLib.sceneEntries.Length;i++){
//					Scene scene = sceneLib.sceneEntries[ i ];
//					if( scene.name == sceneName ){
			if( m_SceneMap.ContainsKey( sceneName ) ){
					Scene scene = m_SceneMap[ sceneName ];
//					Debug.Log( "matched Scene Name  ," + sceneName + " and Enter this actor." );
					LoadSceneNode.Do( scene , param );
//						break;
//					}
//				}
			}
			// If not attached sceneLib , then Load from Resources folder.
			else{
				Scene scene = Resources.Load( sceneName , typeof(Scene) ) as Scene;
				if( scene != null ){
					LoadSceneNode.Do( scene , param );
				}				
			}
		}

		void showsystemui( Hashtable param ){
			SystemUtility.ShowSystemUI( true );
		}

		void hidesystemui( Hashtable param ){
			SystemUtility.ShowSystemUI( false );
		}

		void trans( Hashtable param ){
		 	string method = "crossfade";
		 	if( param.ContainsKey( "method") ){
		 		method = param["method"] as string;		 		
		 	}
		 	string resourcePath = "";
		 	switch( method ){
		 		case "crossfade":
		 		//TODO !
//		 			resource = "";
		 			break;

		 		case "blind":
		 			resourcePath = "Effects/Blind";
		 			break;

		 		case "tile":
		 			resourcePath = "Effects/TileFade";
		 			break;
		 	}
//		 	float time = float.Parse( param["time"] as string )/1000f;

			Object loadedResource = Resources.Load( resourcePath );	
			if( loadedResource == null ){
				Debug.LogError( "Resources.Load Error ! path:" + resourcePath );	
				return;
			}

		 	ViNoSceneManager sm = ViNoSceneManager.Instance;
		 	string parentName = sm.transform.parent.gameObject.name;	// It is expected that the "Panels" object or the "Camera" object.
			UnityWrapper.InstantiateAsGameObject( loadedResource , parentName );
		}

		// **************************** Sound Tag ******************************* .

		void stopbgm( Hashtable param ){
			SimpleSoundPlayer player = ISoundPlayer.Instance as SimpleSoundPlayer;
			player.StopMusic( 0f );					
		}

		void fadeoutbgm( Hashtable param ){
			float time = 1f;
			if( param.ContainsKey("time") ){
				time = float.Parse( param["time"] as string )/1000f;
			}			
			SimpleSoundPlayer player = ISoundPlayer.Instance as SimpleSoundPlayer;
			player.StopMusic( time );								
		}

		void playbgm( Hashtable param ){
			string audioPath = param[ "storage"] as string;
			bool loop = true;
			if( param.ContainsKey( "loop") ){
				loop = ( ( param[ "loop" ] as string ) == "true" ) ? true : false;
			}			
			ISoundPlayer player = ISoundPlayer.Instance;
			player.PlayMusic( audioPath , loop , 0f );
		}

		void playse( Hashtable param ){
			string audioPath = param[ "storage"] as string;
			bool loop = false;
			if( param.ContainsKey( "loop") ){
				loop = ( ( param[ "loop" ] as string ) == "true" ) ? true : false;
			}			
			ISoundPlayer player = ISoundPlayer.Instance;
			player.PlaySE( audioPath , loop , 0f );
		}

		void playvoice( Hashtable param ){
			ISoundPlayer pl = ISoundPlayer.Instance;
			if( pl != null ){
				pl.StopVoice();
			}

			string audioPath = param[ "storage"] as string;
			bool loop = false;
			if( param.ContainsKey( "loop") ){
				loop = ( ( param[ "loop" ] as string ) == "true" ) ? true : false;
			}			
			ISoundPlayer player = ISoundPlayer.Instance;
			player.PlayVoice( audioPath , loop , 0f );

			VM.Instance.UpdateMessageVoiceData( audioPath , 0 );
		}

		void xchgbgm( Hashtable param ){
			string audioPath = param[ "storage"] as string;
			bool loop = true;
			if( param.ContainsKey( "loop") ){
				loop = ( ( param[ "loop" ] as string ) == "true" ) ? true : false;
			}			
			float time = float.Parse( param[ "time" ] as string )/1000f;
			ISoundPlayer player = ISoundPlayer.Instance;
			player.PlayMusic( audioPath , loop , time );			
		}

		// Actor Event.

		/// <summary>
		/// Raises the enter actor event.
		/// </summary>
		/// <param name='param'>
		/// Parameter.
		/// </param>
		void enteractor( Hashtable param ){			
			if( actorLib != null ){
				bool fadein = false;
				if( param.ContainsKey( "fade") ){
					string fade = param[ "fade" ] as string;
					fadein = ( fade == "true" ) ? true : false;
				}
				// Fade in Start ?.
				ISpriteFactory._FADEIN_AT_CREATE = fadein;

				string actorName = param[ "name" ] as string;
				string position = param["position"] as string;			

				ActorPosition pos = ActorUtility.GetActorPosition( position );

//				Debug.Log( "OnEnterActor :" + actorName );
				OnEnterActor( actorName , pos );
			}
		}

		// Wrapper of exitactor.
		void exitscene( Hashtable param ){
			bool fadein = false;
			if( param.ContainsKey( "fade") ){
				string fade = param[ "fade" ] as string;
				fadein = ( fade == "true" ) ? true : false;
			}
			ViNoSceneManager sm = ViNoSceneManager.Instance;
			SceneCreator.DestroyScene( sm.theSavedPanel , ! fadein );	// Fade and Destroy Actor.
		}

		void exitactor( Hashtable param ){
			string actorName = param[ "name" ] as string;
			bool fadeout = false;
			if( param.ContainsKey( "fade") ){
				string fade = param[ "fade" ] as string;
				fadeout = ( fade == "true" ) ? true : false;
			}

			OnExitActor( actorName , ! fadeout );
		}

		void setpropactor( Hashtable param ){
			string actorName = param[ "name" ] as string;
			Debug.Log( "Set Prop Actor : " + actorName );
//			if( m_ActorGOMap.ContainsKey( actorName ) ){
//				if( m_ActorGOMap[ actorName ] != null ){		

			// Is Active or Deactive ?.
			if( param.ContainsKey( "color" ) ){
				string col = param[ "color" ] as string;	
				if( col == "active" ){
					ViNoSceneManager.Instance.SetActiveColor( actorName );
				}
				else{
					ViNoSceneManager.Instance.SetDeactiveColor( actorName );					
				}
			}

			// is Visible ?.
			if( param.ContainsKey( "visible" ) ){
				string visible = param["visible"] as string;
				bool t = ( visible == "true" ) ? true : false;
				Debug.Log( "Actor visible" );
				GameObject actor = GameObject.Find( actorName );
				if( actor != null ){
//							m_ActorGOMap[ actorName ].SetActive( t );
					actor.SetActive( t );
				}
				else{
					Debug.LogWarning( "Actor name: " + actorName + " not found." );
				}
			}
//				}
//				else{
//					Debug.LogWarning( "Actor name: " + actorName + " found , but GO not assigned." );
//				}
//			}
		}

		void changestate( Hashtable param ){
//			Debug.Log( "OnChangeState" );
			if( actorLib != null ){
				string actorName = param[ "name" ] as string;
				string state = param["state"] as string;		
				bool fadein = false;
				if( param.ContainsKey( "fade") ){
					string fade = param[ "fade" ] as string;
					fadein = ( fade == "true" ) ? true : false;
				}
				// Fade in Start ?.
				ISpriteFactory._FADEIN_AT_CREATE = fadein;

				// Fadeout and destroy childs.
				if( fadein ){
					string destroyRootName = actorName + " will_Destroy";
					GameObject _destroyRoot = GameObject.Find( destroyRootName );
					if( _destroyRoot == null ){
						_destroyRoot = new GameObject( destroyRootName );
					}

					GameObject actor = GameObject.Find( actorName );
					Transform actorTra = actor.transform;
					int childNum = actorTra.childCount;
					// Rename Actors.
					while( childNum > 0 ){
						Transform tra = actorTra.GetChild( 0 );
						tra.name = tra.name + "_";
//						Debug.Log( "Destroy:" + tra.name );
						tra.parent = _destroyRoot.transform;

						childNum = actorTra.childCount;
					}
//					_destroyRoot.transform.parent =	.transform;

					SceneCreator.DestroyScene( _destroyRoot , ! fadein );	// Fade and Destroy Actor.
				}
				
//				Debug.Log ( "State:" + state );
				if( m_ActorMap.ContainsKey( actorName ) ){
					ActorInfo actorInfo = m_ActorMap[ actorName ];
//					Debug.Log ( "Actor Matched.:" + actorName );
					for(int k=0;k<actorInfo.actorStates.Length;k++){						
						if( actorInfo.actorStates[ k ].stateName == state ){
							ActorInfo.ActorState actorState = actorInfo.actorStates[ k ];
							CreateChildLayer( actorInfo , actorState );
							break;
						}
					}
				}

			}
		}

		void shake( Hashtable param ){
			string objectName = param[ "name" ] as string;
			GameObject obj = GameObject.Find( objectName );
			if( obj != null ){				
				iTween.PunchPosition( obj , new Vector3( 0.1f , 0.1f , 0f ) , 2f );
			}
		}

		void OnPlayScenario( Hashtable param ){
			string scenarioName = param[ "name" ] as string;
			ViNoAPI.PlayScenario( scenarioName );
		}

		// From Scenario Script.
		MessageEventData m_MessageEvtData = new MessageEventData();
		void settext( Hashtable param ){
			string text = param["text"] as string;
			string textBoxName = param["textbox"] as string;
			int textBoxID = SystemUIEvent.Instance.GetTextBoxIDBy( textBoxName );

			m_MessageEvtData.textBoxID = (byte)textBoxID;
			m_MessageEvtData.message = text;
			m_MessageEvtData.show = true;
			OnSetText( m_MessageEvtData );
		}

		private string prevSpeakerName;

		/// /// <summary>
		/// Change the Color of Name Text by Actor Name.
		/// </summary>
		void OnSetText( MessageEventData data ){
//			string actorName = data.message;
			SystemUIEvent sys = SystemUIEvent.Instance;
			if( m_ActorMap.ContainsKey( data.message ) ){
				ActorInfo actorInfo =  m_ActorMap[ data.message ];
				data.message = sys.GetBeginColorTag( actorInfo.textColor , data.textBoxID ) + data.message + sys.GetEndColorTag( data.textBoxID);

				// Test...
				if( actorInfo.portrait != null && portraitMat1 != null ){
				//	Debug.Log( "Actor Portrait : " );
					portraitMat1.mainTexture = actorInfo.portrait;
				}
				else{
				//	portraitMat1.gameObject.SetActive( false );	
				}

				sys.SetText( data.message , data.textBoxID );
			}
			else{
				sys.SetText( data.message , data.textBoxID );
			}
			VM.Instance.UpdateMessageEvtData( data );

// Highlight Current Speaker for future update.			
#if false
			if( highlightCurrentSpeaker ){
				if( prevSpeakerName == actorName ){
					return;
				}			

				if( ! string.IsNullOrEmpty( prevSpeakerName ) ){
					ViNoSceneManager.Instance.SetDeactiveColor( prevSpeakerName );
				}

				ViNoSceneManager.Instance.SetActiveColor( actorName );

				prevSpeakerName = actorName;
			}
#endif			
		}
		
		void OnAnimation( Hashtable param ){

		}

		void moveactor( Hashtable param ){
//			Debug.Log( "OnMoveActor" );
			string actorName = param[ "name" ] as string;
			string position = param["position"] as string;			
#if true
			GameObject go = GameObject.Find( actorName );
			if( go == null ){ 
//				Debug.LogWarning( "Actor name :" + actorName + " Object not found." );
				return;
			}			
#else			
			GameObject go = null;
			if( m_ActorGOMap.ContainsKey( actorName ) ){
				go = m_ActorGOMap[ actorName ];
				if( go == null ){ 
					Debug.LogWarning( "Actor name :" + actorName + " Object not found." );
					return;
				}
			}
#endif
			Vector3 toPos = go.transform.localPosition;
			ActorPosition pos = ActorUtility.GetActorPosition( position );
			toPos.x = GetPositionX( pos );
//			Debug.Log( "to :" + pos + " , value:" + toPos.x );
			AnimationNode.MoveTo( go , toPos , 1f );
//			TweenPosition.Begin( go , 1f , toPos );				
//			iTween.MoveTo( go , toPos , 1f );
		}

/*		IEnumerator Shake( GameObject obj ){
			yield return new WaitForSeconds(  );

		}
//*/
						
		void EnterActor( ActorInfo info ,  ActorInfo.ActorState actorState ){
			CreateActorRoot( info , actorState );				
			CreateChildLayer( info , actorState );
		}
		
		void CreateChildLayer( ActorInfo info ,  ActorInfo.ActorState actorState ){
			for(int i=0;i<actorState.dataArray.Length;i++){	
				actorState.dataArray[ i ].parentname = info.actorName;
				actorState.dataArray[ i ].sclZ = 1f;
				
				SceneCreator.Create( actorState.dataArray[ i ] );
			}			
		}
		
		void EnterActor( ActorInfo info ,  ActorInfo.ActorState actorState ,  float posX ){
			CreateActorRoot( info , actorState );
			GameObject go = GameObject.Find( info.actorName );//actorState.dataArray[ i ].parentname + "/" + actorState );
			if( go != null ){			
				Vector3 pos = go.transform.localPosition;
				pos.x = posX;
				go.transform.localPosition = pos;				
			}
			
			for(int i=0;i<actorState.dataArray.Length;i++){	
				actorState.dataArray[ i ].parentname = info.actorName;
				actorState.dataArray[ i ].sclZ = 1f;
				
				SceneCreator.Create( actorState.dataArray[ i ] );
/*
				go = GameObject.Find( actorState.dataArray[ i ].parentname + "/" + actorState.dataArray[ i ].name );
				if( go != null ){				
					Vector3 pos = go.transform.localPosition;
					pos.x = posX;
					go.transform.localPosition = pos;
				}
//*/				
			}
		}

		void OnEnterActor( string actorName , ActorPosition actorPos ){
			if( actorLib != null ){
//				for( int i=0;i<actorLib.actorEntries.Length;i++){
//					ActorInfo actorInfo = actorLib.actorEntries[ i ];
//					if( actorInfo.actorName == actorName ){
				if( m_ActorMap.ContainsKey( actorName ) ){
						ActorInfo actorInfo = m_ActorMap[ actorName ];
//						Debug.Log( "matched Actor Name  ," + actorName + " and Enter this actor." );						
						ActorInfo.ActorState currentActorState = actorInfo.baseActorState;
						float posX = GetPositionX( actorPos );
						EnterActor( actorInfo , currentActorState , posX  );
						return;
				}
			}
//			}
			else{
				Debug.Log( "ActorLibrary not attached. couldn't enter actor : " + actorName );

			}
		}

#if false				
		void OnCreateButton( Hashtable param ){
			string targetLabel = param[ "target" ] as string;
			string arrangement = param[ "arrangement"] as string;
			string background = param[ "background"] as string;
				
		}
#endif		

		void Awake(){
			// Register Actors to Map.
			if( actorLib == null ){
				Debug.LogWarning( "ActorLibrary not attached. Actor can't enter in scene." );							
			}
			else{
				m_ActorMap = new Dictionary<string,ActorInfo>();

				// Cache ActorInfo.
				for( int i=0;i<actorLib.actorEntries.Length;i++){
					string actorName = actorLib.actorEntries[ i ].actorName;
					m_ActorMap[ actorName ] = actorLib.actorEntries[ i ];
				}								
			}

			// Register Scenes to Map.
			if( sceneLib == null ){
				Debug.LogWarning( "SceneLibrary not attached. LoadSceneNode Not Working correctly." );			
			}
			else{
				m_SceneMap = new Dictionary<string,Scene>();

				// Cache SceneInfo.
				for( int i=0;i<sceneLib.sceneEntries.Length;i++){
					string sceneName = sceneLib.sceneEntries[ i ].name;
					m_SceneMap[ sceneName ] = sceneLib.sceneEntries[ i ];
				}								
			}
		}

		void OnExitActor( string actorName , bool immediate ){
			GameObject actor = GameObject.Find( actorName );
			SceneCreator.DestroyScene( actor , immediate );	// Fade and Destroy Actor.
		}

		// Helper .

		public float GetPositionX( SceneEvent.ActorPosition actorPos ){
			float posX = centerPosX;
			switch( actorPos ){
				case SceneEvent.ActorPosition.center:			posX = centerPosX;			break;							
				case SceneEvent.ActorPosition.left:				posX = leftPosX;			break;	
		 		case SceneEvent.ActorPosition.right:			posX = rightPosX;			break;	
		 		case SceneEvent.ActorPosition.middle_left:		posX = middleLeftPosX;		break;	
				case SceneEvent.ActorPosition.middle_right:		posX = middleRightPosX;		break;	
			}
			return posX;
		}

		GameObject CreateActorRoot( ActorInfo info , ActorInfo.ActorState actorState ){
			float scl = actorState.sizeInPercent / 100f;
			SceneData.SceneNodeData actorNodeData =  new SceneData.SceneNodeData();
			actorNodeData.name = info.actorName;
			actorNodeData.parentname = ViNoSceneManager.Instance.theSavedPanel.name;
			actorNodeData.sclX = scl;
			actorNodeData.sclY = scl;
			actorNodeData.sclZ = 1f;
			actorNodeData.alpha = 1f;
			actorNodeData.show = true;		
			GameObject actorGO = SceneCreator.Create( actorNodeData );
//			m_ActorGOMap[ info.actorName ] = actorGO;
			return 	actorGO;
		}
		
			// for testing...
	#if false
		public bool _CauseEnterActor;
		public bool _CauseExitActor;
		public string actorName = "Sachi";
		public ActorUtility.GetActorPosition actorPos = ActorUtility.GetActorPosition.center;
		
		void Update(){
			if( _CauseEnterActor) {
				_CauseEnterActor = false;
				OnEnterActor( actorName , actorPos );
			}
			if( _CauseExitActor ){
				_CauseExitActor = false;
				OnExitActor( actorName );
			}
		}
			
	#endif
			
	}
}
