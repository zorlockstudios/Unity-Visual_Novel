//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Virtual Machine of Script Engine.
/// VM runs byte code , 
/// </summary>
public class VM : VirtualMachine{	

	static protected VM instance;
	public static VM Instance {
		get {	return VM.instance;	}
	}

	protected MessageEventData m_MessageEventData = new MessageEventData();

	void Awake(){
		if( instance == null ){
			instance=  this;
			Init( _CODE_SIZE );
		}
		else{
			Destroy( gameObject );
		}
	}

	// ------------- Override --------------------.

	/// <summary>
	/// Handles the opcode.
	/// </summary>
    public override void OnUpdate() {
		if( IsFinish() ){
			return;	
		}
							
		// Pomp Message to MessagingHandler.
		if( m_MessagePompToMsghandler ){
			if( m_MessagingHandler != null ){				
				bool handled = m_MessagingHandler.HandleOpcode( this );				
				m_MessagePompToMsghandler = ! handled;						
			}
			return;			
		}
		
		m_CanTextProgress = false;
        switch ( code[ pc ] ) {        	
		case Opcode.STRING:		pc = ByteCodeReader.readString( code , pc + 1 );	m_TextBuilder.Append( loadedString );	break;
		case Opcode.TEXT:		pc = ReadText( pc );																		break;
		case Opcode.VAR:		pc = ByteCodeReader.readVar( code, pc , ref paramHash , ref m_TextBuilder , stubIndent );	break;						
		case Opcode.TABLE:		pc = ByteCodeReader.readTable( code , pc , ref paramHash );									break;		
		case Opcode.ASSIGN_STRING:
//			Debug.Log("OPCODE>ASSIGN_STRING");
			pc = ByteCodeReader.readString( code , pc + 2 );
			leftHand = loadedString;
			pc = ByteCodeReader.readString( code , pc + 1 );			
			rightHand = loadedString;
//			Debug.Log(  "Opcode.ASSIGN key=" + leftHand + " value=\"" + rightHand + "\"" );				

// Assign Value to Hashtable ?.
#if false
			symbolTable[ leftHand ] = rightHand;			

// Assign Value to FlagTable.			
#else
			ScenarioNode scenario = ScenarioNode.Instance;
			if( scenario != null && scenario.flagTable != null ){
				scenario.flagTable.SetStringValue( leftHand , rightHand );
			}
#endif			
			leftHand = "";
			rightHand = "";
			break;

        case Opcode.NULL :		pc++;																						break;											
		case Opcode.MESSAGING:
			pc = ByteCodeReader.readString( code , pc + 2 );
			messagingTargetName = loadedString;					
			m_MessagePompToMsghandler = true;
			bool isIgnoreObj = loadedString.Equals( "env" );
			if( ! isIgnoreObj ){
				if( tweenDataCached.tweenTarget != null ){
					if( ! tweenDataCached.tweenTarget.name.Equals( messagingTargetName ) ){
						tweenDataCached.tweenTarget = GameObject.Find( messagingTargetName );
					}
				}
				else{
					tweenDataCached.tweenTarget = GameObject.Find( messagingTargetName );				
				}
			}						
			tweenDataCached.paramTable = paramHash;					
			break;		

		case Opcode.NODE:
			m_PrevNodeName = m_CurrNodeName;			
			pc = ByteCodeReader.readString( code , pc + 2 );
			ViNoDebugger.Log( "NODE" , loadedString );

			m_CurrNodeName = loadedString;
			m_NodePcMap[ loadedString ] = pc;	
			SetCurrentNodeToScriptEngine();					
			
			// Callback to ScriptBinder. 
			scriptBinder.OnEnterNode( this );	
			break;

       case Opcode.LOAD_RESOURCE:
			m_LoadedResourcePath = VirtualMachine.loadedTextLiteralString;
			m_LoadedResource = UnityWrapper.LoadResource( m_LoadedResourcePath );			
			pc++;
			break;

// TODO : Need to Test .
		case Opcode.PLAY_AUDIO_FROM_RESOURCE:
			m_LoadedResourcePath = VirtualMachine.loadedTextLiteralString;
			ISoundPlayer.Instance.PlayAudioClip( m_LoadedResource as AudioClip , m_LoadedResourcePath , ViNoConfig.prefsBgmVolume , 0f );
			m_LoadedResource = null;
			Resources.UnloadUnusedAssets();

			pc++;
			break;

		case Opcode.INSTANTIATE_AS_GAMEOBJECT:
			if( m_LoadedResource != null) {
				string parentName = VirtualMachine.loadedTextLiteralString;
				
				UnityWrapper.InstantiateAsGameObject( m_LoadedResource , parentName );
				
				m_LoadedResource = null;
				Resources.UnloadUnusedAssets();
			}
			else{
				Debug.LogError( "Resource not loaded." );
			}
			pc++;
			break;

		case Opcode.DESTROY_OBJECT:
			string goName = VirtualMachine.loadedTextLiteralString;
			GameObject go = GameObject.Find( goName );			// TODO : GO.Find is Slow .
			GameObject.Destroy( go );
			pc++;
			break;

		case Opcode.JUMP:	// Jump to loadedString Node  .
			ByteCodeReader.readString( code , pc + 2 );		
			GoToLabel( loadedString );
			ViNoDebugger.Log( "NODE" , "jump to :" + loadedString );			
			break;

		case Opcode.IF:
			pc = ByteCodeReader.readString( code , pc + 2 );
			string flagName = VirtualMachine.loadedString;
			Debug.Log ( "flag name :" + flagName );
			
			bool isOnOrOff = ( code[ pc ] == 1 ) ? true : false;
			pc++;

			pc = ByteCodeReader.readString( code , pc + 1 );
			string ifTarget = VirtualMachine.loadedString;
			Debug.Log( "IF =>" + ifTarget);
			pc = ByteCodeReader.readString( code , pc + 1 );
			string elseTarget = VirtualMachine.loadedString;
			Debug.Log( "ELSE =>" + elseTarget);

			bool isFlagOn = ScenarioNode.Instance.flagTable.CheckFlagBy( flagName );
			if( isFlagOn == isOnOrOff ){
				Debug.Log( "IF" );
				GoToLabel( ifTarget );				
			}			
			else{
				Debug.Log( "ELSE" );
				GoToLabel( elseTarget );				
			}
			break;

		// ----- Layer -----.
#if false			
		case Opcode.LAYOPT:				GOOptionNode.Do( paramHash );		pc++;	break;
#endif		
        case Opcode.BEGIN_TRANSITION:	UnityWrapper.BeginTransition();		pc++;	break;	       	
        case Opcode.END_TRANSITION:		UnityWrapper.EndTransition();		pc++;	break;
		case Opcode.SCENE_NODE:
			SceneData.SceneNodeData data = SceneCreator.CreateNodeData( paramHash );
			SceneCreator.Create( data );
			pc++;
			break;

		case Opcode.LOAD_SCENE:
//			bool destroy = ( code[ pc + 1 ] == 0 ) ? true : false ;			
//			UnityWrapper.LoadScene( m_LoadedResource , destroy );
			LoadSceneNode.Do( m_LoadedResource , paramHash );
			m_LoadedResource = null;
			Resources.UnloadUnusedAssets();
			pc++;
			break;
			
		case Opcode.CLEAR_SCENE:
			GameObject advSceneRoot = ViNoSceneManager.Instance.theSavedPanel;
			bool immediateDestroy = false;
			SceneCreator.DestroyScene( advSceneRoot , immediateDestroy );
			pc++;
			break;

/*		case Opcode.PLAY_ANIMATION:
			byte animationID = code[ pc + 1 ];
			ViNoAnimationManager.Instance.PlayAnimation( (int)animationID );
			pc+= 2;
			break;
//*/
			
		// ----- Message -----.
		case Opcode.BR:		
//			m_TextBuilder.Append( "\n" );
			pc++;		break;//scriptBinder.BR( this );	pc++;		break;

		case Opcode.CM:		ClearMessage();				pc++;		break;
		case Opcode.ER:
			AddToBacklog();
			ClearTextBuilder();
			if( m_MsgTargetTextBox != null ){
				m_MsgTargetTextBox.ClearMessage();
			}
			else{
				Debug.LogWarning( "Current Message Target Not Set." );
			}
			pc++;
			break;

		case Opcode.PRINT:		scriptBinder.PRINT( this );			pc++;		break;
		case Opcode.CURRENT:	
			TriggerMessageEvent( "OnMessageTargetChanged" , code[ pc + 1 ] , "" , true  );
			break;
		
		case Opcode.SET_TEXT:
			TriggerMessageEvent( "OnSetText" , code[ pc + 1 ] , VirtualMachine.loadedTextLiteralString , true  );
			m_CurrentText = m_MessageEventData.message;
			break;

		case Opcode.HIDE_MESSAGE:
			TriggerMessageEvent( "OnHideMessage" , code[ pc + 1 ] , "" , false  );
			break;

		// ------ System Opcode -----.
		case Opcode.START_WAIT:
			m_ElapsedSec = 0f;
			if( ! string.IsNullOrEmpty( loadedTextLiteralString ) ){
				m_WaitSec = float.Parse( loadedTextLiteralString );	
			}
			else{
				m_WaitSec = kWaitSec;
			}
			pc++;
			break;

		case Opcode.UPDATE_WAIT:
//			ViNoDebugger.Log( "VM" , "waiting ...");
			m_ElapsedSec += Time.deltaTime;
			if( m_ElapsedSec > m_WaitSec ){
				pc++;
			}
			break;

		case Opcode.STOP:
			// Wait Until Player choosing from options . or reached to the end of a leaf node .			
			// Nothing to do...
							
			break;

		case Opcode.END:
			ViNoEventManager.Instance.TriggerEvent( "OnFinishScenario" );
			update = false;
			break;

		case Opcode.PLAY_SCENARIO:
			pc = ByteCodeReader.readString( code , pc + 2 );
			string scenarioName = loadedString;
			GameObject scenarioObj = GOCache.SetActive( scenarioName , true );
			if( scenarioObj != null ){
				 ScenarioNode s = scenarioObj.GetComponent<ScenarioNode>();
				 s.Play();
			}
			break;
						
		case Opcode.FLAG_ON:	
			if( ScenarioNode.Instance != null && ScenarioNode.Instance.flagTable != null ){
				ScenarioNode.Instance.flagTable.SetFlagBy( VirtualMachine.loadedTextLiteralString  , true );
			}		
			pc++;
			break;
		
		case Opcode.FLAG_OFF:
			if( ScenarioNode.Instance != null && ScenarioNode.Instance.flagTable != null ){
				ScenarioNode.Instance.flagTable.SetFlagBy( VirtualMachine.loadedTextLiteralString  , false );
			}		
			pc++;
			break;

		case Opcode.SELECTIONS:				
			ISelectionsCtrl selCtrl = ISelectionsCtrl.Instance;
			if( selCtrl != null ){
				if( !selCtrl.IsActive() ){
					string title = VirtualMachine.loadedTextLiteralString;
					selCtrl.SetTitle( title );
					ISelectionsCtrl.Instance.ChangeActive( true );
				}
			}
			else{
				Debug.LogError( "ISelectionsCtrl instance not found." );
			}
			pc++;
			break;

		case Opcode.LINK:		ISelectionsCtrl.Instance.AddSelection( ref paramHash );	pc++;	break;
		case Opcode.WAIT_TOUCH:
			if( IScriptEngine.skip ){
				if( IScriptEngine.skipAlreadyPass && ! VirtualMachine._ALREADY_PASS_THE_NODE ){
					return;
				}

				_SkipText( );
				
				ISoundPlayer pl = ISoundPlayer.Instance;
				if( pl != null ){
					if( pl.IsPlayingVoice() ){
						pl.StopVoice();
					}
				}			
				m_CanTextProgress = true;					
				return;				
			}
			
			if( autoMode ){	
				float dt = Time.deltaTime;				
				m_TimeElapsed += dt;
				if( m_TimeElapsed > _AUTO_MODE_WAIT_TIME ){
					m_TimeElapsed = 0f;					
					_SkipText();									
				}
				return;
			}
			m_CanTextProgress = true;
			break;
	
//		case Opcode.PLAY_BGM:
//			break;

		// -----Audio -----.		
		case Opcode.PLAY_SOUND:
			byte soundCategory = code[ pc + 1 ];	// 0: BGM 1:SE 2: VOICE.
			byte soundID = code[ pc + 2 ];

			UnityWrapper.PlaySound( soundCategory , soundID );
			if( soundCategory == 2 ){
				SET_CURRENT_VOICE_ID( true , soundID );
			}
			pc+= 3;
			break;

		case Opcode.STOP_SOUND:
			//TODO :
		
			pc++;
			break;

		case Opcode.STOP_VOICE:
			SET_CURRENT_VOICE_ID( false , 0 );
			if( ISoundPlayer.Instance != null ){
				ISoundPlayer.Instance.StopVoice();
			}
			pc++;
			break;
					
        default :
			ViNoDebugger.LogError( "VM" , "PC : " + pc );
            break;
        }
    }
		
	// ------------------- public methods ----------------------.

	public void UpdateMessageEvtData( MessageEventData data ){
		m_MessageEventData = data;
		m_CurrentText = data.message;
	}

	/// <summary>
	/// Progress Text Message.
	/// </summary>
	public void TextProgress(){		
		if( m_CanTextProgress ){
			_SkipText( );
		}
	}

	public void SET_CURRENT_VOICE_ID( bool isVoice , byte voiceID ){
		m_CurrentVoiceID = voiceID;
		m_IsPlayVoice = isVoice;
	}

	// ------------------- Private methods ----------------------.
						
	private void TriggerMessageEvent( string evtName , byte textBoxID , string message , bool show ){
		if( ViNoEventManager.Instance != null ){
			m_MessageEventData.textBoxID = textBoxID;
			m_MessageEventData.message = message;
			m_MessageEventData.show = show;
			ViNoEventManager.Instance.TriggerEvent( evtName , m_MessageEventData );
		}
		else{
			Debug.LogError( "ViNoEventManager Instance Not Found. couldn't Trigger MessageEvent." );
		}
		pc+=2;
	}

	private void AddToBacklog(){
		if( m_MsgTargetTextBox != null ){
			if( ! string.IsNullOrEmpty( m_MsgTargetTextBox.text	) ){
				DialogPartData data 	= new DialogPartData();
				data.isName 			= string.IsNullOrEmpty( m_CurrentText ) ? false : true;
				if( data.isName ){
					data.nameText = m_CurrentText;
				}
				data.dialogText		= m_MsgTargetTextBox.text;
				data.isVoice 		= m_IsPlayVoice;
				data.voiceAudioID 	= m_CurrentVoiceID;
				data.voiceAudioKey	= m_CurrentVoiceKey;
				ViNoBackLog.Add( data );

				m_CurrentText = "";
				m_IsPlayVoice = false;
				m_CurrentVoiceID = 0;					
			}
		}
	}

	private void ClearMessage(){
		AddToBacklog();

		ClearTextBuilder();
		
		SystemUtility.ClearAllTextBoxMessage();
	}
}
