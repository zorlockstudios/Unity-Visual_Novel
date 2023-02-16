//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.

using UnityEngine;
using System.Collections;
using System;
using System.Globalization;
using System.Security.Permissions;
using System.Threading;

/// <summary>
/// Standard script binder.
/// This class using Unity Animation Class.
/// </summary>
public class DefaultScriptBinder : ScriptBinder{
	/// <summary>
	/// Change Texture2D of renderer.sharedMaterial.
	/// </summary>
	/// <param name='tweenData'>
	/// Tween data.
	/// </param>
	public override void LOAD_IMAGE( TweenOperandData tweenData ){
		Resources.UnloadUnusedAssets();
		
		string texpath = VM.loadedTextLiteralString;
		Texture2D tex = Resources.Load( texpath ) as Texture2D;		
		GameObject obj = tweenData.tweenTarget;						
		if( obj.GetComponent<Renderer>() != null ){
			if( obj.GetComponent<Renderer>().sharedMaterial != null ){
				obj.GetComponent<Renderer>().sharedMaterial.mainTexture = tex;	

				ViNoDebugger.Log ( "loaded texture and the GameObject Name will change to :" + texpath );
				obj.name = texpath;
//				obj.transform.localScale = new Vector3( tex.width , tex.height , 1f );
			}	
			else{
				ViNoDebugger.LogError( "Material not attached !" );	
			}
		}		
		else{
			ViNoDebugger.LogError( "renderer not attached !" );	
		}
	}
	
	/// <summary>
	/// Do the Same Thing with LOAD_IMAGE.
	/// </summary>
	/// <param name='tweenData'>
	/// Tween data.
	/// </param>
	public override void CHANGE_IMAGE( TweenOperandData tweenData ){
		LOAD_IMAGE( tweenData );		
	}
	
	/// <summary>
	/// Cross Fade.
	/// </summary>
	/// <param name='tweenData'>
	/// Tween data.
	/// </param>
	public override void CROSS_FADE( TweenOperandData tweenData ) {	
		ContainsKey_sendDelayAndStartCoroutine( ref tweenData.paramTable );		
		float duration = ContainsKey_duration( ref tweenData.paramTable );				
		if( tweenData.tweenTarget != null ){
			if( tweenData.paramTable.ContainsKey( "objectA" )
												&&
			 tweenData.paramTable.ContainsKey( "objectB" ) ){
				
				string objAName = tweenData.paramTable[ "objectA" ] as string;
				string objBName = tweenData.paramTable[ "objectB" ] as string;
				
				Transform A  = tweenData.tweenTarget.transform.Find( objAName );
				Transform B = tweenData.tweenTarget.transform.Find( objBName );
				
				ColorPanel panelA =  A.GetComponent<ColorPanel>();
				ColorPanel panelB =  B.GetComponent<ColorPanel>();
				panelA.duration = duration;
				panelB.duration = duration;
				
				if( panelA.alpha > panelB.alpha ){
					panelA.StartFade( 0f , 1f );
					panelB.StartFade( 1f , 0f );		
				}
				else{
					panelA.StartFade( 1f , 0f );
					panelB.StartFade( 0f , 1f );	
				}		
			}			
		}
	}
	
	/// <summary>
	/// Fade ColorPanel.
	/// </summary>
	/// <param name='tweenData'>
	/// Tween data.
	/// </param>
	public override void FADE_PANEL( TweenOperandData tweenData ) {			
		Hashtable paramHash = tweenData.paramTable;
		GameObject panelAttachedObject = tweenData.tweenTarget;	
		ContainsKey_sendDelayAndStartCoroutine( ref paramHash );		
		float duration = ContainsKey_duration( ref paramHash );				
		string fadeStr = (string)paramHash[ "fadeTo" ].ToString();						
		float fadeTo =  float.Parse(fadeStr, CultureInfo.InvariantCulture );
		
		ColorPanel uipanel = panelAttachedObject.GetComponent<ColorPanel>();
		float fadeFrom = uipanel.alpha;
		if( paramHash.ContainsKey( "fadeFrom" ) ){	
			fadeFrom = GetValueFromKey( ref paramHash , "fadeFrom" );	
		}		
		if( paramHash.ContainsKey( "fadeTo" ) ){
			if( uipanel != null ){
				uipanel.enabled = true;
				uipanel.duration = duration;
				uipanel.StartFade( fadeTo , fadeFrom );
			}
		}
#if false		
		// If root node has Panels in children , Start Fading Each Panels of Children .
		ColorPanel[] childPanels = panelAttachedObject.GetComponentsInChildren<ColorPanel>();
		if( childPanels != null ){
			for( int i=0;i<childPanels.Length;i++){	
				childPanels[ i ].enabled = true;				
				uipanel.duration = duration;
				childPanels[ i ].StartFade( fadeTo , fadeFrom );
			}
		}		
#endif		
	}
	
	/// <summary>
	/// Using AnimationClip.
	/// </summary>
	/// <param name='tweenData'>
	/// Tween data.
	/// </param>
	public override void TWEEN( TweenOperandData tweenData ) {			
		AnimationMove( tweenData );				
	}
	
		
}