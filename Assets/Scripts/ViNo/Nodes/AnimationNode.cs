//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

/// <summary>
/// AnimationNode.
/// Animate a Object.
/// </summary>
[AddComponentMenu("ViNo/Nodes/Object/Animation")]
[ System.Serializable ]
public class AnimationNode : ViNode{
	[ XmlAttribute()] public string animationName = "";
	[ XmlAttribute()] public string targetName = "GameObjectName";
	[ XmlAttribute()] public string targetAName = "";
	[ XmlAttribute()] public string targetBName = "";
	[ XmlAttribute()] public AnimationType animationType;		
	[ XmlAttribute()] public float duration = 1000f;
	[ XmlAttribute()] public float delaySecToNextMessage = 1000f;	
	[ XmlAttribute()] public float fadeTo = 1f;
	[ XmlAttribute()] public float fadeFrom = 0.0f;
	[ XmlAttribute()] public float amountX = 0f;
	[ XmlAttribute()] public float amountY = 0f;
	[ XmlAttribute()] public float amountZ = 0f;
	[ XmlAttribute()] public WrapMode wrapMode = WrapMode.Once;
	[ XmlAttribute()] public int method = 0;	// 0 : Linear 1 : EaseInOut.
	
	[ XmlAttribute()] public float fromAmountX = 0f;
	[ XmlAttribute()] public float fromAmountY = 0f;
	[ XmlAttribute()] public float fromAmountZ = 0f;	
	[ XmlAttribute()] public bool toggleFromAmount;
	[ XmlAttribute()] public bool waitUntilAnimationFinish = true;
	public bool isTargetInAsset;
		
	private Hashtable paramTbl;
	
	public Hashtable paramHash{ get{ return paramTbl; } }
	
	public bool playAtStart;
	public bool playLoop;
	public bool playAtRandom;
	public bool playOnEnable;
	public bool playPingPong;

	public GameObject animTarget;
	public Vector3 amount;
	public Vector3 fromAmount;
	
	public GameObject crossfadeTargetA;
	public GameObject crossfadeTargetB;
			
	static public string[] methods ={ "Linear" , "EaseInOut" };
	
	private bool m_PlayPingPongToggle;
	private float m_ElapsedTime = 0f;
	
	static public void MoveTo( GameObject target , Vector3 position , float time ){
		Vector3 fromPos = target.transform.localPosition;

		Animation animation = target.GetComponent<Animation>();
		if( animation == null ){
			animation = target.AddComponent<Animation>();
		}					
		AnimationClip clip = new AnimationClip();
//        clip.wrapMode = WrapMode.;		
	    AnimationCurve animCurveX = AnimationCurve.Linear( 0f , fromPos.x , time , position.x );
	    AnimationCurve animCurveY = AnimationCurve.Linear( 0f , fromPos.y , time , position.y );
	    AnimationCurve animCurveZ = AnimationCurve.Linear( 0f , fromPos.z , time , position.z );
        clip.SetCurve("", typeof(Transform), "localPosition.x", animCurveX );
        clip.SetCurve("", typeof(Transform), "localPosition.y", animCurveY );
        clip.SetCurve("", typeof(Transform), "localPosition.z", animCurveZ );								
		
		ViNoAnimationListener animcb = target.GetComponent<ViNoAnimationListener>();
		if( animcb == null ){
			target.AddComponent<ViNoAnimationListener>();
		}		
		
        AnimationEvent tweenFinished = new AnimationEvent();
        tweenFinished.time = time;
        tweenFinished.intParameter = 123;
        tweenFinished.stringParameter = "end";
        tweenFinished.functionName = "AnimationFinishedCallback";			
 
		clip.AddEvent( tweenFinished );
						
        animation.AddClip(clip, target.name );
		
		// Now, Start the Animation.
        animation.Play( target.name );
	}

	public void TogglePlay( ){
		FlipToAndFromData();
		Preview ();
	}
	
	void OnEnable(){
		RestoreTargets();

		if( playOnEnable ){	
			Preview();	
		}
	}

	void Start(){
		if( playAtStart ){
			Preview();
		}
#if false		
		NotEditable();
#endif		
	}
		
	void Update(){		
		if( playLoop || playAtRandom ){
			m_ElapsedTime += Time.deltaTime;
			float waitTime = duration/1000f;
			if( playAtRandom ){
				waitTime = Random.Range( 1f , 5f );
			}
			else{
				waitTime = duration/1000f;
			}			
			if( m_ElapsedTime >= waitTime ){
				m_ElapsedTime = 0f;
				if( playPingPong ){
					TogglePlay();
				}else{
					Preview ();
				}
			}
		}						
	}
	
	public override void ToScenarioScript( ref System.Text.StringBuilder sb ){
		string str = name;//GetNodeTag( name );	
		sb.Append( "[anim TODO:" + str + System.Environment.NewLine );		
	}
	
	public override void ToByteCode( ByteCodes code ){		
		if( animTarget == null ){
			ViNoDebugger.LogWarning( "Animation Target Not Set" ) ;
			return;
		}
		
		SetUpParamTable();
		
		List<byte> byteList = new List<byte>();
		
		// Labeling.
		AddNodeCode( byteList );

#if false
// instantiate prefab , if target is in asset.
		if( isTargetInAsset ){
			Debug.Log( "add load resource :" + animTarget.name );
			// Add CreateObjectNode.ToByteCode...
			ByteCodeScriptTools.AddLoadResourceCode( byteList , animTarget.name );

			GameObject parent = null;
			if( Application.isPlaying ){
				parent = ViNoSceneManager.Instance.theSavedPanel;
			}
			else{
				ViNoSceneManager sm = GameObject.FindObjectOfType( typeof(ViNoSceneManager)) as ViNoSceneManager;				
				parent = sm.theSavedPanel;
			}

			if( parent == null ){
				// SceneManager Not Needed user .
				parent = Camera.main.gameObject;
			}

			Debug.Log( "AddCreateGOCode :" + parent.name );
			ByteCodeScriptTools.AddCreateGOCode( byteList , parent );
		}
		else{
			Debug.Log( "target is in scene..." );
		}
#endif

		ByteCodeScriptTools.AddTablePairsCode( byteList , paramTbl );	
		byte op = OpcodeMessaging.TWEEN;
		switch( animationType ){
			case AnimationType.FADE_PANEL:		op = OpcodeMessaging.FADE_PANEL;	break;
			case AnimationType.CROSS_FADE:		op = OpcodeMessaging.CROSS_FADE;	break;
		}		
		ByteCodeScriptTools.AddMessagingCode( byteList , animTarget.name , op );	

		code.Add( byteList.ToArray() );

		ToByteCodeInternal( code );
	}
				
	void SetUpParamTable( ){
		AmountToVec();
		
		paramTbl = new Hashtable();
		paramTbl[ "duration" ] = duration.ToString();
		paramTbl[ "sendDelay" ] = delaySecToNextMessage.ToString();
		paramTbl[ "wrapMode" ] = wrapMode.ToString();		
		paramTbl[ "method" ] = method.ToString();		
		switch( animationType ){
			case AnimationType.FADE_PANEL:		
				paramTbl[ "fadeTo" ] = fadeTo.ToString();
				if( toggleFromAmount ){
					paramTbl[ "fadeFrom" ] = fadeFrom.ToString();					
				}
				break;
			
			case AnimationType.MOVE_TO:						
				paramTbl[ "moveX" ] = amount.x.ToString();			
				paramTbl[ "moveY" ] = amount.y.ToString();			
				paramTbl[ "moveZ" ] = amount.z.ToString();	
				if( toggleFromAmount ){
					paramTbl[ "fromPosX" ] =  fromAmount.x.ToString();			
					paramTbl[ "fromPosY" ] =  fromAmount.y.ToString();			
					paramTbl[ "fromPosZ" ] =  fromAmount.z.ToString();			
				}
				break;				
			
			case AnimationType.ROTATE_TO:		
				paramTbl[ "rotX" ] = amount.x.ToString();			
				paramTbl[ "rotY" ] = amount.y.ToString();			
				paramTbl[ "rotZ" ] = amount.z.ToString();	
				if( toggleFromAmount ){
					paramTbl[ "fromRotX" ] =  fromAmount.x.ToString();			
					paramTbl[ "fromRotY" ] =  fromAmount.y.ToString();			
					paramTbl[ "fromRotZ" ] =  fromAmount.z.ToString();			
				}
				break;
						
			case AnimationType.SCALE_TO:			
				paramTbl[ "scaleX" ] = amount.x.ToString();			
				paramTbl[ "scaleY" ] = amount.y.ToString();			
				paramTbl[ "scaleZ" ] = amount.z.ToString();	
				if( toggleFromAmount ){
					paramTbl[ "fromScaleX" ] =  fromAmount.x.ToString();			
					paramTbl[ "fromScaleY" ] =  fromAmount.y.ToString();			
					paramTbl[ "fromScaleZ" ] =  fromAmount.z.ToString();			
				}			
				break;	

		case AnimationType.CROSS_FADE:
			if( crossfadeTargetA != null && crossfadeTargetB != null ){
				paramTbl[ "objectA" ] = 	crossfadeTargetA.name;
				paramTbl[ "objectB" ] = 	crossfadeTargetB.name;
			}							
			break;
		}
	}
	
	void AmountToVec(){
		amount.x = amountX;
		amount.y = amountY;
		amount.z = amountZ;		
		
		fromAmount.x = fromAmountX;
		fromAmount.y = fromAmountY;
		fromAmount.z = fromAmountZ;		
		
	}
	
	public void OnDeserialize(){
		animTarget = GameObject.Find( targetName );
		AmountToVec();
	}
	
	public void Preview( ){		
		SetUpParamTable();

// TODO :
		if( toggleFromAmount ){
//			Init();
		}
		
		// Tween.
		Animate( animTarget , paramTbl );		
	}
	
	/// <summary>
	/// Animate the specified target and paramTbl.
	/// </summary>
	public void Animate( GameObject target , Hashtable paramTbl ){
		string _FOR_PREVIEW = "_FOR_PREVIEW";
		GameObject forPreviewObj = GameObject.Find( _FOR_PREVIEW );
		DefaultScriptBinder sb = null ;
		if( forPreviewObj == null ){
			forPreviewObj = new GameObject( _FOR_PREVIEW );
			forPreviewObj.hideFlags = HideFlags.HideInHierarchy;
			sb = forPreviewObj.AddComponent<DefaultScriptBinder>();
		}
		else{
			sb = forPreviewObj.GetComponent<DefaultScriptBinder>();
		}					
		TweenOperandData data = new TweenOperandData();
		data.tweenTarget = target;
		data.paramTable = paramTbl;
				
		switch( animationType ){
		case AnimationType.FADE_PANEL:
			sb.FADE_PANEL( data );
			break;
			
		case AnimationType.CROSS_FADE:
			sb.CROSS_FADE( data );
			break;
			
		default:					
			sb.TWEEN( data );
			break;
		}
	}

	public void RestoreTargets( ){
		if( animTarget == null ){
			if( ! string.IsNullOrEmpty( targetName ) ){
				animTarget = GameObject.Find( targetName );
				if( animTarget != null ){

					switch ( animationType ){
					 case AnimationType.CROSS_FADE:
						if( ! string.IsNullOrEmpty( targetAName ) && ! string.IsNullOrEmpty( targetBName ) ){
						 	crossfadeTargetA = GameObject.Find( targetAName );
						 	crossfadeTargetB = GameObject.Find( targetBName );
						}
						break;
					}
				}
			}
		}		
	}

	private void FlipToAndFromData( ){
		float tX = amountX;
		float tY = amountY;
		float tZ = amountZ;
		
		amountX = fromAmountX;
		amountY = fromAmountY;
		amountZ = fromAmountZ;
		
		fromAmountX = tX;
		fromAmountY = tY;
		fromAmountZ = tZ;		
	}
	

	
}