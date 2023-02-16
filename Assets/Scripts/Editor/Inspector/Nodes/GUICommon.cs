//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// GUI common in Editor OnGUI.
/// </summary>
public class GUICommon {
	
	static public void DrawScriptEngineCommonInspector( IScriptEngine se ){
		if( se.CURRENT != null ){
			EditorGUIUtility.PingObject( se.CURRENT.gameObject );				
		}		
	}

	static public void DrawAnimationNodeInspector( AnimationNode node ){
#if false
		node.animationName = EditorGUILayout.TextField( "AnimationName" , node.animationName );					
#endif		
		node.playAtStart =  EditorGUILayout.Toggle( "PlayAtStart" , node.playAtStart  );		
		node.playLoop 	 = 	EditorGUILayout.Toggle( "PlayLoop" , node.playLoop  );		
		node.playAtRandom = EditorGUILayout.Toggle( "PlayAtRandom" , node.playAtRandom  );		
		node.playOnEnable = EditorGUILayout.Toggle( "playOnEnable" , node.playOnEnable  );		
		node.playPingPong = EditorGUILayout.Toggle( "playPingPong" , node.playPingPong  );		
		
		if( node.playPingPong ){
			node.playLoop =  true;
		}

		EditorGUILayout.LabelField( "Target GameObject in Hierarchy." );					
		node.animTarget = (GameObject)EditorGUILayout.ObjectField( "target" , node.animTarget , typeof(GameObject) , true  );				
		node.animationType = (AnimationType)EditorGUILayout.EnumPopup( "AnimationType" ,  node.animationType );
		
		if( node.animTarget != null ){
			node.isTargetInAsset = AssetDatabase.Contains( node.animTarget );
/*			if( node.isTargetInAsset ){
				Debug.Log( "target is in Project folder." );
			}
			else{
				Debug.Log( "target is in Scene." );
			}
//*/			
		}

		node.duration = EditorGUILayout.Slider( "duration( ms )" , node.duration , 1f , 10000f );				
		
		node.waitUntilAnimationFinish = EditorGUILayout.Toggle( "Wait next Until Finish" , node.waitUntilAnimationFinish );			
		if( node.waitUntilAnimationFinish ){
			node.delaySecToNextMessage = node.duration; // = EditorGUILayout.FloatField( "delaySecToNext" , node.delaySecToNextMessage  );			
		}
		else{
			node.delaySecToNextMessage = 0f;
		}
		
//		node.wrapMode = (WrapMode)EditorGUILayout.EnumPopup( "WrapMode" , node.wrapMode );		
		if( node.animationType != AnimationType.CROSS_FADE && node.animationType != AnimationType.FADE_PANEL ){
			node.method = EditorGUILayout.Popup( "Method" , node.method , AnimationNode.methods );
		}
		
		if( node.animTarget != null ){
			node.targetName = node.animTarget.name;
		}
		node.amountX = node.amount.x;
		node.amountY = node.amount.y;
		node.amountZ = node.amount.z;		
		
		node.fromAmountX = node.fromAmount.x;
		node.fromAmountY = node.fromAmount.y;
		node.fromAmountZ = node.fromAmount.z;		
		
		switch( node.animationType ){
			case AnimationType.FADE_PANEL:
				node.fadeTo = EditorGUILayout.FloatField( "fadeTo" , node.fadeTo  );						
				
				node.toggleFromAmount = EditorGUILayout.Toggle( "Fade From ?" , node.toggleFromAmount );
				if( node.toggleFromAmount ){
					node.fadeFrom = EditorGUILayout.FloatField( "fadeFrom" , node.fadeFrom );						
				}
				break;			
			
			case AnimationType.MOVE_TO:			
				node.amount = EditorGUILayout.Vector3Field( "amount" , node.amount );
				if( GUILayout.Button( "Set" ) ){
					if( node.animTarget != null ){
						node.animTarget.transform.localPosition = node.amount;
					}
				}	
				node.toggleFromAmount = EditorGUILayout.Toggle( "Start From?" , node.toggleFromAmount );
				if( node.toggleFromAmount ){
					node.fromAmount = EditorGUILayout.Vector3Field( "from" , node.fromAmount );				
					if( GUILayout.Button( "Set" ) ){
						if( node.animTarget != null ){
							node.animTarget.transform.localPosition = node.fromAmount;
						}				
					}
				}
				break;
			
			case AnimationType.ROTATE_TO:
				node.amount = EditorGUILayout.Vector3Field( "amount" , node.amount );
				if( GUILayout.Button( "Set" ) ){
					if( node.animTarget != null ){
						node.animTarget.transform.localEulerAngles = node.amount;
					}
				}
				node.toggleFromAmount = EditorGUILayout.Toggle( "Start From?" , node.toggleFromAmount );
				if( node.toggleFromAmount ){
					node.fromAmount = EditorGUILayout.Vector3Field( "from" , node.fromAmount );				
					if( GUILayout.Button( "Set" ) ){
						if( node.animTarget != null ){
							node.animTarget.transform.localEulerAngles = node.fromAmount;
						}				
					}
				}
		
				break;
			
			case AnimationType.SCALE_TO:			
				node.amount = EditorGUILayout.Vector3Field( "amount" , node.amount );
				if( GUILayout.Button( "Set" ) ){
					if( node.animTarget != null ){
						node.animTarget.transform.localScale = node.amount;
					}
				}
				node.toggleFromAmount = EditorGUILayout.Toggle( "Start From?" , node.toggleFromAmount );
				if( node.toggleFromAmount ){
					node.fromAmount = EditorGUILayout.Vector3Field( "from" , node.fromAmount );				
					if( GUILayout.Button( "Set" ) ){
						if( node.animTarget != null ){
							node.animTarget.transform.localScale = node.fromAmount;
						}				
					}
				}
				break;
		case AnimationType.CROSS_FADE:
			node.crossfadeTargetA = (GameObject)EditorGUILayout.ObjectField( "crossFadeTargetA" , node.crossfadeTargetA , typeof( GameObject ) , true );
			node.crossfadeTargetB = (GameObject)EditorGUILayout.ObjectField( "crossFadeTargetB" , node.crossfadeTargetB , typeof( GameObject ) , true );			
			if( node.animTarget != null ){
				if( node.crossfadeTargetA != null ){
					node.targetAName = node.animTarget.name + "/" + node.crossfadeTargetA.name;
				}
				if( node.crossfadeTargetB != null ){
					node.targetBName = node.animTarget.name + "/" + node.crossfadeTargetB.name;
				}
			}			
			break;			
		}

		node.RestoreTargets();
				
		GUILayout.Label( "Preview is enabled in PlayMode." );
		if( Application.isPlaying ){		
			GUI.enabled = true;
		}
		else{
			GUI.enabled = false;
		}
		
		if( GUILayout.Button( "Preview" ) ){
			node.Preview();	
		}
		
		GUI.enabled = true;
	}
	
	static public void DrawLineSpace ( float space , float lineHeight )
	{
		GUILayout.Space( space );

		if (Event.current.type == EventType.Repaint)
		{
			Texture2D tex = EditorGUIUtility.whiteTexture;
			Rect rect = GUILayoutUtility.GetLastRect();
			GUI.color = new Color(0f, 0f, 0f, 0.5f);
//			GUI.DrawTexture(new Rect(0f, rect.yMin + 5f, Screen.width, 6f), tex);
			GUI.DrawTexture(new Rect(0f, rect.yMin + 5f, Screen.width, lineHeight ), tex);
//			GUI.DrawTexture(new Rect(0f, rect.yMin + 10f, Screen.width, 2f), tex);
			GUI.color = Color.white;
		}
	}
	
#if false		
	static public void DrawAnimationMoveNodeInspector( AnimationMoveNode animNode , AnimationMoveData data ){
		data.targetName = EditorGUILayout.TextField( "Target Name" , data.targetName );
		data.duration = EditorGUILayout.Slider( "duration( ms )" , data.duration , 1f , 10000f );				
		data.toX = EditorGUILayout.Slider( "toX" , data.toX , -500f , 500f );
		data.waitUntilAnimationFinish = EditorGUILayout.Toggle( "Wait next Until Finish" , data.waitUntilAnimationFinish );			
		data.toggleFromAmount = EditorGUILayout.Toggle( "Start From?" , data.toggleFromAmount );
//		data.method = EditorGUILayout.Popup( "Method" , data.method , AnimationNode.methods );
		if( data.toggleFromAmount ){
			data.fromX = EditorGUILayout.Slider( "fromX" , data.fromX , -500f , 500f );
		}

		GUI.enabled = Application.isPlaying;

		if( GUILayout.Button( "Preview" ) ){
		 	GameObject tgt = GameObject.Find( data.targetName );
		 	if( tgt != null ){
				AnimationMoveNode.Animate( animNode , tgt , data );
		 	}
		}

		GUI.enabled = true;
	}
#endif	
}
