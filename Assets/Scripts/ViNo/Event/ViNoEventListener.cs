//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.

using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ViNoEventListener : MonoBehaviour{
	protected TweenOperandData m_CachedData = new TweenOperandData();
			
	protected void FadeCommon( string layerName , float duration , float fadeTo ){
		m_CachedData.tweenTarget = GameObject.Find( layerName );
		if( m_CachedData.tweenTarget  != null ){
			m_CachedData.paramTable = new Hashtable();
			m_CachedData.paramTable[ "duration" ] = duration.ToString();
			m_CachedData.paramTable[ "fadeTo" ] = fadeTo.ToString();				
		}
		else{
			ViNoDebugger.Log( "DialogPartEvent" , "there is expected that " +  layerName + " Layer Exists Under the SavedLayer." );			
		}
	}	
		
	protected void FadePanel( string layerName , float duration , float fadeTo ){		
		if( VM.Instance.scriptBinder != null ){
			FadeCommon( layerName , duration  , fadeTo );
			VM.Instance.scriptBinder.FADE_PANEL( m_CachedData );
		}		
	}
	
	protected void CrossFade( string layerName , float duration , float fadeTo ){
		if( VM.Instance.scriptBinder != null ){
			FadeCommon( layerName , duration  , fadeTo );
			VM.Instance.scriptBinder.CROSS_FADE( m_CachedData );
		}		
	}	
	
	protected void TweenMoveX( string layerName , float duration , float moveX ){
		m_CachedData.tweenTarget = GameObject.Find( layerName );
		if( VM.Instance.scriptBinder != null ){					
			m_CachedData.paramTable = new Hashtable();
			m_CachedData.paramTable[ "duration" ] = duration.ToString();
			m_CachedData.paramTable[ "moveX" ] = moveX.ToString();				
			VM.Instance.scriptBinder.TWEEN( m_CachedData );
		}
	}
		
	
}
