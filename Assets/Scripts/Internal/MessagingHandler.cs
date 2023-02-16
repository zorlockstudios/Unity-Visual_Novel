//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
using UnityEngine;
using System.Collections;

/// <summary>
/// Messaging handler.
/// In HandleOpcode Method , .
/// returning true means	=> Progress ProgramCounter.
/// returning false means	=> Not Progress ProgramCounter.
/// </summary>
public class MessagingHandler : IOpcodeHandler {				
	[HideInInspector] public string kPivotCenterStr = "center";
	[HideInInspector] public string kPivotTopLeftStr = "topleft";
			
	void Awake( ){
		hideFlags = HideFlags.HideInHierarchy | HideFlags.HideInInspector;	
	}
	
	/// <summary>
	/// Sends the message to VM ScriptBinder.
	/// </summary>
	/// <param name='msg'>
	/// Message.
	/// </param>
	void SendMessageToBinder( string msg ){
		if( m_Vm.scriptBinder != null ){
			m_Vm.scriptBinder.SendMessage( msg , m_Vm.tweenDataCached );				
		}
		else
		{
			ViNoDebugger.LogWarning( "Tween Receiver is not Set. Please Set GameObject with " +
				" OnTween Function is Implemented." );	
		}			
	}
/*		
	public void HandleAOpcode( VM vm , byte op ){
		vm.code = new byte[ 1 ];
		vm.code[ 0 ] = op;
		HandleOpcode( vm );
	}
//*/	
	/// <summary>
	/// Handles the opcode. 
	/// </summary>
	/// <returns>
	/// The opcode.
	/// </returns>
	/// <param name='vm'>
	/// If set to <c>true</c> vm.
	/// </param>
	public override bool HandleOpcode( VirtualMachine vm ){				
		m_Vm = vm;
		switch( vm.code[ vm.pc ] ){
		case OpcodeMessaging.PUNCH_POSITION:	vm.scriptBinder.PUNCH_POSITION( vm );	break;
		case OpcodeMessaging.SIZE:				vm.scriptBinder.SIZE ( vm );						break;			
		case OpcodeMessaging.LOAD_IMAGE:		vm.scriptBinder.LOAD_IMAGE( vm.tweenDataCached );			break;			
		case OpcodeMessaging.CHANGE_IMAGE:		vm.scriptBinder.CHANGE_IMAGE( vm.tweenDataCached );		break;					
		case OpcodeMessaging.MSG_TARGET:
			GameObject obj = vm.tweenDataCached.tweenTarget;
			vm.scriptBinder.MSG_TARGET( obj );
			break;			
		case OpcodeMessaging.SET_TEXT:						vm.scriptBinder.SET_TEXT( vm );											break;			
		case OpcodeMessaging.SET_RESOURCE_AS_TEXTURE:
			Texture2D image = vm.loadedResource as Texture2D;
			MeshRenderer ren = vm.tweenDataCached.tweenTarget.GetComponent<MeshRenderer>();
			if( ren != null ){
				ren.sharedMaterial.mainTexture = image;
				ren.transform.localScale = new Vector3( image.width , image.height , 1f );

				// VinoSceneNode component is needed.
				vm.tweenDataCached.tweenTarget.SendMessage( "OnChangeTexture" , vm.loadedResourcePath , SendMessageOptions.DontRequireReceiver );
			}
			else{
				Debug.LogError( "MeshRenderer not Found." );	
			}
			break;
			
		case OpcodeMessaging.SET_RESOURCE_AS_TRANSITION_TEXTURE1:
			image = vm.loadedResource as Texture2D;
			ren = vm.tweenDataCached.tweenTarget.GetComponent<MeshRenderer>();
			if( ren != null ){
				ren.sharedMaterial.SetTexture( "_tex0" , image );
			}
			else{
				Debug.LogError( "MeshRenderer not Found." );	
			}
			break;

		case OpcodeMessaging.SET_RESOURCE_AS_TRANSITION_TEXTURE2:
			image = vm.loadedResource as Texture2D;
			ren = vm.tweenDataCached.tweenTarget.GetComponent<MeshRenderer>();
			if( ren != null ){
				ren.sharedMaterial.SetTexture( "_tex1" , image );
			}
			else{
				Debug.LogError( "MeshRenderer not Found." );	
			}
			break;

		case OpcodeMessaging.FADE_PANEL:			vm.update = false;		vm.scriptBinder.FADE_PANEL( vm.tweenDataCached );	break;		
		case OpcodeMessaging.CROSS_FADE:			vm.update = false;		vm.scriptBinder.CROSS_FADE( vm.tweenDataCached );	break;	
		case OpcodeMessaging.TWEEN:					vm.update = false;		vm.scriptBinder.TWEEN( vm.tweenDataCached );			break;			
		case OpcodeMessaging.DESTROY:				GameObject.Destroy( vm.tweenDataCached.tweenTarget );		break;			
		case OpcodeMessaging.DESTROY_CHILDREN:		ViNoGOExtensions.FindAndDestroyChildren( vm.messagingTargetName );					break;
		case OpcodeMessaging.TARGET:				vm.m_CurrTarget = GameObject.Find( VM.loadedTextLiteralString );				break;	
		case OpcodeMessaging.LOAD_LEVEL:			Application.LoadLevel( VM.loadedTextLiteralString );			break;
		case OpcodeMessaging.LOAD_SCENE_XML:		vm.scriptBinder.LOAD_SCENE_XML( vm );						break;
		case OpcodeMessaging.PLAY_SOUND:
			if( ISoundPlayer.Instance == null ){
				vm.ProgressProgramCounter(1);
				ViNoDebugger.LogError( "VM" , "PLAY_SOUND : ISoundPlayer.Instance NOT FOUND." );
				return false;
			}			
			string soundName = vm.paramHash[ "name" ] as string;
			string soundCat = vm.paramHash[ "category" ] as string;
			string delayStr = vm.paramHash[ "delay" ] as string;
			float delay = float.Parse( delayStr );
			
			if( ISoundPlayer.Instance != null ){
				ISoundPlayer.Instance.PlaySoundCallback( soundName , soundCat  , 1f  , delay );
			}
			break;
			
		case OpcodeMessaging.STOP_SOUND:
			if( ISoundPlayer.Instance == null ){
				vm.ProgressProgramCounter(1);
				ViNoDebugger.LogWarning( "ISoundPlayer.Instance NOT FOUND." );
				return false;
			}			
			soundName = vm.paramHash[ "name" ] as string;
			soundCat = vm.paramHash[ "category" ] as string;
			string fadeStr = vm.paramHash[ "fadeOutSeconds" ] as string;
			float fadeOutSeconds = float.Parse( fadeStr );
				
			if( ISoundPlayer.Instance != null ){
				ISoundPlayer.Instance.StopSoundCallback( soundName , soundCat  , fadeOutSeconds );
			}					
			break;		

// TODO: 
#if false																
		case OpcodeMessaging.SET_ACTIVE:	// OBSOLETE. ===> OPCODE.LAYOPT.
			GOOptionNode.Do( vm.paramHash );
			break;
#endif

		case OpcodeMessaging.SET_POS_3D:
			Vector3 pos = ViNoStringExtensions.ParseVector3( VM.loadedTextLiteralString );									
			if( vm.tweenDataCached.tweenTarget != null ){
				vm.tweenDataCached.tweenTarget.transform.localPosition = pos;
			}
			break;
			
		case OpcodeMessaging.SET_SCALE_3D:			
			Vector3 scale = ViNoStringExtensions.ParseVector3( VM.loadedTextLiteralString );									
			if( vm.tweenDataCached.tweenTarget != null ){
				vm.tweenDataCached.tweenTarget.transform.localScale = scale;
			}		
			break;			
			
		case OpcodeMessaging.TRANSLATE:
			if( ! string.IsNullOrEmpty( VM.loadedTextLiteralString ) ){
				string[] strs = VM.loadedTextLiteralString.Split( ',' );
				if( strs.Length > 0 ){
					if( strs.Length == 3 ){
						GameObject target = vm.tweenDataCached.tweenTarget;
						if( target != null ){
							Vector3 tra = ViNoStringExtensions.ParseVector3( VM.loadedTextLiteralString );
							Vector3 temp = target.transform.localPosition;
							temp += tra;
							target.transform.localPosition = temp;
						}						
					}
				}
			}
			break;
			
		case OpcodeMessaging.TRIGGER_EVENT:
			ViNoEventManager em = ViNoEventManager.Instance;
			if( ! string.IsNullOrEmpty( VM.loadedTextLiteralString ) && em != null   ){
				em.TriggerEvent( VM.loadedTextLiteralString );	
			}			
			break;									

		case OpcodeMessaging.TRIGGER_EVENT_WITH_ARGS:
			em = ViNoEventManager.Instance;
			if( em != null && vm.tweenDataCached.paramTable.ContainsKey( "eventType" ) ){
				string evtType = vm.tweenDataCached.paramTable[ "eventType" ] as string;
				em.TriggerEvent( evtType , vm.tweenDataCached.paramTable );	
			}
			break;									
			
		default:
			ViNoDebugger.LogError( "PC : " + vm.pc );
			break;
		}
		
		// Progress Counter.
		vm.ProgressProgramCounter(1);					
		
		return true;
	}
		
}
