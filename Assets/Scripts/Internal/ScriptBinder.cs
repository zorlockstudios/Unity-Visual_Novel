//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
using UnityEngine;
using System.Collections;

/// <summary>
/// ScriptBinder. Implementation.
/// </summary>
public class ScriptBinder : IScriptBinder {

	private ViNoNodeEvtData nodeEvtData = new ViNoNodeEvtData();
	
	/// <summary>
	/// OnEnter and SetFlag the Node.
	/// </summary>
	/// <param name='vm'>
	/// Vm.
	/// </param>
	public override void OnEnterNode( VirtualMachine vm ){			
		ScenarioNode scenario = ScenarioNode.Instance;
		if( scenario.flagTable !=null ){
			VirtualMachine._ALREADY_PASS_THE_NODE = scenario.flagTable.CheckFlagBy( vm.currNodeName );
			scenario.flagTable.SetFlagBy( vm.currNodeName , true );			
		}
		if( ViNoEventManager.Instance !=null ){
			nodeEvtData.eventType = "NODE_ENTER";
			nodeEvtData.nodeName = vm.currNodeName;
			nodeEvtData.nodeObject = IScriptEngine.Instance.CURRENT;
			ViNoEventManager.Instance.TriggerEvent( "OnEnterViNode" , nodeEvtData );	
		}		
	}

// OBSOLETE.	
#if false	
	public override void OnExitNode( VirtualMachine vm ){			
		if( ViNoEventManager.Instance !=null ){
			nodeEvtData = new ViNoNodeEvtData();
			nodeEvtData.eventType = "NODE_EXIT";
			nodeEvtData.nodeName = vm.currNodeName;
			nodeEvtData.nodeObject = IScriptEngine.Instance.CURRENT;
			ViNoEventManager.Instance.TriggerEvent( "OnExitViNode" , nodeEvtData );	
		}		
	}	
#endif
	
	/// <summary>
	/// Skips the text.
	/// </summary>
	/// <param name='vm'>
	/// Vm.
	/// </param>
	public override void SkipText( VirtualMachine vm ){		
		if( vm.m_MsgTargetTextBox == null ){
			ViNoDebugger.LogWarning( "Current ViNoTextBox is null." );
			vm.ProgressProgramCounter( 1 );			
			return;
		}
		
		if( ! vm.m_MsgTargetTextBox.reachedEnd ){
			vm.m_MsgTargetTextBox.DispTextQuick();
		}
		else{
			// Stop Voice.
			ISoundPlayer pl = ISoundPlayer.Instance;
			if( pl != null ){
				if( pl.IsPlayingVoice() ){
					pl.StopVoice();
				}
			}			
			vm.ProgressProgramCounter( 1 );
		}				
	}
	

	public override void BR( VirtualMachine vm ){	
		if( vm.m_MsgTargetTextBox != null ){
			vm.m_MsgTargetTextBox.Append( System.Environment.NewLine );
		}
	}

	/// <summary>
	/// Clear Message.
	/// </summary>
	/// <param name='vm'>
	/// Vm.
	/// </param>
	public override void CM( VirtualMachine vm ){
	}
	
	/// <summary>
	/// PRINT.
	/// </summary>
	/// <param name='vm'>
	/// Vm.
	/// </param>
	public override void PRINT( VirtualMachine vm ){
		if( vm.m_MsgTargetTextBox != null ){
			vm.m_MsgTargetTextBox.Append( vm.ToStringBuilder() );
			
			vm.ClearTextBuilder();
		}
	}

#if false	
	// OBSOLETE.
	public override void SET_TEXT( VirtualMachine vm ){
		string theText = VirtualMachine.loadedTextLiteralString;			
		if( vm.tweenDataCached.tweenTarget != null ){
			ViNoTextBox txtBox = vm.tweenDataCached.tweenTarget.GetComponent<ViNoTextBox>();				
			if( txtBox != null ){				
				txtBox.SetText( theText );
				m_CurrentText = theText;
			}
			else{
				ViNoDebugger.LogError( "ViNoTextBox Object NOT FOUND." );		
			}
		}else{
			ViNoDebugger.LogError( "ViNoTextBox Object NOT FOUND." );		
		}						
	}
#endif
	
	public override void PUNCH_POSITION( VirtualMachine vm ){
#if false			
			float kTime = 1f;
			Vector3 kAmount = new Vector3( 0.2f , 0.2f , 0f );
			iTween.PunchPosition( vm.tweenDataCached.tweenTarget , kAmount , kTime );
#endif					
	}	
	
	public override void SIZE( VirtualMachine vm ){
	// TODO : 
/*		
		if( vm.m_MsgTargetTextBox != null ){				
			if( ! string.IsNullOrEmpty ( VirtualMachine.loadedTextLiteralString ) ){
				float size = float.Parse( VirtualMachine.loadedTextLiteralString );		
				vm.m_MsgTargetTextBox.SetTextSize( size );		// Not Changing the Font size but Changing the Transform.localScale.
			}	
		}		
//*/		
	}	
		
	/// <summary>
	/// LOAs the d_ SCEN e_ XM.
	/// </summary>
	/// <param name='vm'>
	/// Vm.
	/// </param>
	public override void LOAD_SCENE_XML( VirtualMachine vm ){
		if( ViNoSceneManager.Instance != null ){			
			// DestroyImmidiate Under the SavedLayer .
			ViNoSceneManager.Instance.DestroyObjectsUnderSavedLayer( );

			// Load XML.
			ViNoSceneSaveUtil.LoadSceneXMLFromTextAssetPath( VirtualMachine.loadedTextLiteralString );			
		}		
	}
	
	protected Vector3 ContainsKey_StrAndXorYorZ( out bool isChanged , string str , ref Hashtable paramHash  , Vector3 initialAmount ){
		Vector3 amount = initialAmount;
		isChanged = false;
		if( paramHash.ContainsKey( str + "X" ) ){
			isChanged = true;
			amount.x = float.Parse( (string)paramHash[ str + "X" ] );			
		}
		
		if( paramHash.ContainsKey( str + "Y" ) ){
			isChanged = true;
			amount.y = float.Parse( (string)paramHash[ str + "Y" ] );			
		}

		if( paramHash.ContainsKey( str + "Z" ) ){
			isChanged = true;
			amount.z = float.Parse( (string)paramHash[ str + "Z" ] );			
		}
		return amount;
	}
	
	protected Vector3 ContainsKey_moveXorYorZ( out bool isMove , ref Hashtable paramHash  , Vector3 localPos ){
		Vector3 move = localPos;
		isMove = false;
		if( paramHash.ContainsKey( "moveX" ) ){
			isMove = true;
			move.x = float.Parse( (string)paramHash[ "moveX" ] );			
		}
		
		if( paramHash.ContainsKey( "moveY" ) ){
			isMove = true;
			move.y = float.Parse( (string)paramHash[ "moveY" ] );			
		}

		if( paramHash.ContainsKey( "moveZ" ) ){
			isMove = true;
			move.z = float.Parse( (string)paramHash[ "moveZ" ] );			
		}		
		return move;		
	}
	
	
	
	protected Vector3 ContainsKey_rotateXorYorZ( out bool isRot ,  ref Hashtable paramHash  , Vector3 localEulers ){
		Vector3 euler = localEulers;
		isRot = false;
		if( paramHash.ContainsKey( "rotX" ) ){
			isRot = true;
			euler.x = float.Parse( (string)paramHash[ "rotX" ] );			
		}		
		if( paramHash.ContainsKey( "rotY" ) ){
			isRot = true;
			euler.y = float.Parse( (string)paramHash[ "rotY" ] );			
		}		
		if( paramHash.ContainsKey( "rotZ" ) ){
			isRot = true;
			euler.z = float.Parse( (string)paramHash[ "rotZ" ] );			
		}		
		return euler;
	}
	
	protected Vector3 ContainsKey_scaleXorYorZ( out bool isScale ,  ref Hashtable paramHash  , Vector3 localScale ){
		Vector3 scl = localScale;
		isScale = false;
		if( paramHash.ContainsKey( "scaleX" ) ){
			isScale = true;
			scl.x = float.Parse( (string)paramHash[ "scaleX" ] );			
		}		
		if( paramHash.ContainsKey( "scaleY" ) ){
			isScale = true;
			scl.y = float.Parse( (string)paramHash[ "scaleY" ] );			
		}		
		if( paramHash.ContainsKey( "scaleZ" ) ){
			isScale = true;
			scl.z = float.Parse( (string)paramHash[ "scaleZ" ] );			
		}		
		return scl;
	}
	
	protected string ContainsKey_name( ref Hashtable paramHash  ){	
		if( paramHash.ContainsKey( "name" ) ){
			return (string)paramHash[ "name" ];
		}
		else{
			return string.Empty;	
		}
	}
	
	// mode="loop" or "once" or "pingpong" .
	protected WrapMode ContainsKey_mode( ref Hashtable paramHash  ){	
		if( paramHash.ContainsKey( "mode" ) ){
			string mode = (string)paramHash[ "mode" ];
			mode = mode.ToLower();
			if( mode.Equals( "loop" ) ){
				return WrapMode.Loop;				
			}
			else if( mode.Equals( "once" ) ){
				return WrapMode.Once;				
			}
			else if( mode.Equals( "pingpong" ) ){
				return WrapMode.PingPong;				
			}
		}
		return WrapMode.Once;
	}
	
	protected float GetValueFromKey( ref Hashtable paramHash , string key ){
		if( paramHash.ContainsKey( key ) ){
			return float.Parse( (string)paramHash[ key ] );
		}
		else{
			return 0.0f;	
		}		
	}
		
	protected float ContainsKey_fadeTo( ref Hashtable paramHash  ){	
		if( paramHash.ContainsKey( "fadeTo" ) ){
			return float.Parse( (string)paramHash[ "fadeTo" ] );
		}
		else{
			return 1.0f;	
		}
	}
	
	protected float ContainsKey_duration( ref Hashtable paramHash  ){		
		VM vm = VM.Instance;
		if( vm != null && vm._ENABLE_DEBUG_MODE ){
			return 0.1f;
		}
		else{
			if( paramHash.ContainsKey( "duration" ) ){
				
				float temp = float.Parse( (string)paramHash[ "duration" ] );
				return temp/1000f;
			}
			else{		
				return 1f;
			}
		}
	}
	
	protected void ContainsKey_sendDelayAndStartCoroutine( ref Hashtable paramHash  ){	
		VM vm = VM.Instance;
		if( paramHash.ContainsKey( "sendDelay" ) ){
			float time = float.Parse( (string)paramHash[ "sendDelay" ] );
			if( vm != null && vm._ENABLE_DEBUG_MODE ){
				time = 0.1f;
			}else{
				time = time/1000f;				
			}
			if( vm != null ){
				if( vm.gameObject.activeInHierarchy ){
					vm.StartCoroutine( "SendEnableUpdateVMDelayed" , time );			
				}
			}
		}	
	}
		
	protected void CalledWhenTweenFinished(){
		VM vm = VM.Instance;
		if( vm != null ){			
			vm.EnableUpdate( true );
		}
	}	
	
	protected void AnimationMove( TweenOperandData tweenData ) {	
		Hashtable paramHash = tweenData.paramTable;
		string name = ContainsKey_name( ref paramHash );
		
		GameObject root = tweenData.tweenTarget;				
		if( root == null ){
			ViNoDebugger.LogError( "tween target not found !" );
			return;	
		}
		
		Transform rootra = root.transform;
		
		if( string.IsNullOrEmpty( name ) ){
			name= "__animation"; // default animation name.
		}
		
		Animation animation = root.GetComponent<Animation>();
		if( animation == null ){
			animation = root.AddComponent<Animation>();
		}					
		AnimationClip clip = new AnimationClip();
		float duration = ContainsKey_duration( ref paramHash );				
		bool isMove = false;
		bool isRotate = false;
		bool isScale = false;
		bool isFromPos = false;
		bool isFromRot = false;
		bool isFromScale = false;		
		WrapMode  wrapmode = ContainsKey_mode( ref paramHash );
		int method  = 0;
		if( paramHash.ContainsKey( "method" ) ){
			string methodStr = paramHash[ "method" ] as string;
			method = int.Parse( methodStr );
		}
		Vector3 move = ContainsKey_moveXorYorZ( out isMove , ref paramHash , rootra.localPosition );
		Vector3 euler = ContainsKey_rotateXorYorZ( out isRotate , ref paramHash , rootra.localEulerAngles );//localRotation.eulerAngles );		
		Vector3 scale = ContainsKey_scaleXorYorZ( 	out isScale , ref paramHash , rootra.localScale );		
		Vector3 fromPos = ContainsKey_StrAndXorYorZ( out isFromPos , "fromPos" , ref paramHash , rootra.localPosition );
		Vector3 fromRot = ContainsKey_StrAndXorYorZ( out isFromRot , "fromRot" , ref paramHash , rootra.localEulerAngles );
		Vector3 fromScale = ContainsKey_StrAndXorYorZ( out isFromScale , "fromScale" , ref paramHash , rootra.localScale );		
        clip.wrapMode = wrapmode;		
		
		if( isMove ){
			switch( method ){
				case 0 : // Linear.
				    AnimationCurve animCurveX = AnimationCurve.Linear( 0f , fromPos.x , duration , move.x );
				    AnimationCurve animCurveY = AnimationCurve.Linear( 0f , fromPos.y , duration , move.y );
				    AnimationCurve animCurveZ = AnimationCurve.Linear( 0f , fromPos.z , duration , move.z );
			        clip.SetCurve("", typeof(Transform), "localPosition.x", animCurveX );
			        clip.SetCurve("", typeof(Transform), "localPosition.y", animCurveY );
			        clip.SetCurve("", typeof(Transform), "localPosition.z", animCurveZ );								
					break;
				
				case 1: // Easeinout.
			      animCurveX = AnimationCurve.EaseInOut( 0f , fromPos.x , duration , move.x );
			      animCurveY = AnimationCurve.EaseInOut( 0f , fromPos.y , duration , move.y );
			      animCurveZ = AnimationCurve.EaseInOut( 0f , fromPos.z , duration , move.z );	
			      clip.SetCurve("", typeof(Transform), "localPosition.x", animCurveX );
			      clip.SetCurve("", typeof(Transform), "localPosition.y", animCurveY );
			      clip.SetCurve("", typeof(Transform), "localPosition.z", animCurveZ );								
				break;
			}
		}

		if( isRotate ){
			Quaternion q1 = Quaternion.Euler( fromRot );
			Quaternion q2 = Quaternion.Euler( euler );
			
			switch( method ){
				case 0 : // Linear.
				    AnimationCurve animCurveX = AnimationCurve.Linear( 0f , q1.x , duration , q2.x );
				    AnimationCurve animCurveY = AnimationCurve.Linear( 0f , q1.y , duration , q2.y );
				    AnimationCurve animCurveZ = AnimationCurve.Linear( 0f , q1.z , duration , q2.z );
				    AnimationCurve animCurveW = AnimationCurve.Linear( 0f , q1.w , duration , q2.w );
							
			        clip.SetCurve("", typeof(Transform), "localRotation.x", animCurveX );
			        clip.SetCurve("", typeof(Transform), "localRotation.y", animCurveY );
			        clip.SetCurve("", typeof(Transform), "localRotation.z", animCurveZ );	
			        clip.SetCurve("", typeof(Transform), "localRotation.w", animCurveW );	
				break;
				
				case 1: // Easeinout.
				    animCurveX = AnimationCurve.EaseInOut( 0f , q1.x , duration , q2.x );
				    animCurveY = AnimationCurve.EaseInOut( 0f , q1.y , duration , q2.y );
				    animCurveZ = AnimationCurve.EaseInOut( 0f , q1.z , duration , q2.z );
				    animCurveW = AnimationCurve.EaseInOut( 0f , q1.w , duration , q2.w );
							
			        clip.SetCurve("", typeof(Transform), "localRotation.x", animCurveX );
			        clip.SetCurve("", typeof(Transform), "localRotation.y", animCurveY );
			        clip.SetCurve("", typeof(Transform), "localRotation.z", animCurveZ );	
			        clip.SetCurve("", typeof(Transform), "localRotation.w", animCurveW );					
				break;
			}
		}		
		
		if( isScale ){
			switch( method ){
				case 0 : // Linear.
				    AnimationCurve animCurveX = AnimationCurve.Linear( 0f , fromScale.x , duration , scale.x );
				    AnimationCurve animCurveY = AnimationCurve.Linear( 0f , fromScale.y , duration , scale.y );
				    AnimationCurve animCurveZ = AnimationCurve.Linear( 0f , fromScale.z , duration , scale.z );			
	    		    clip.SetCurve("", typeof(Transform), "localScale.x", animCurveX );
	    		    clip.SetCurve("", typeof(Transform), "localScale.y", animCurveY );
	    		    clip.SetCurve("", typeof(Transform), "localScale.z", animCurveZ );
					break;				
				case 1: // Easeinout.
				    animCurveX = AnimationCurve.EaseInOut( 0f , fromScale.x , duration , scale.x );
				    animCurveY = AnimationCurve.EaseInOut( 0f , fromScale.y , duration , scale.y );
				    animCurveZ = AnimationCurve.EaseInOut( 0f , fromScale.z , duration , scale.z );			
	    		    clip.SetCurve("", typeof(Transform), "localScale.x", animCurveX );
	    		    clip.SetCurve("", typeof(Transform), "localScale.y", animCurveY );
	    			clip.SetCurve("", typeof(Transform), "localScale.z", animCurveZ );					
					break;
			}
		}		
						
		ViNoAnimationListener animcb = root.GetComponent<ViNoAnimationListener>();
		if( animcb == null ){
			root.AddComponent<ViNoAnimationListener>();
		}		
		
        AnimationEvent tweenFinished = new AnimationEvent();
        tweenFinished.time = duration;
        tweenFinished.intParameter = 123;
        tweenFinished.stringParameter = "end";
        tweenFinished.functionName = "AnimationFinishedCallback";			
 
		clip.AddEvent( tweenFinished );
						
        animation.AddClip(clip, name );
		
		// Now, Start the Animation.
        animation.Play( name );	
		
		// Is  paramHash Contains Key "sendDelay" ? .
		ContainsKey_sendDelayAndStartCoroutine( ref paramHash );			
	}	
}
