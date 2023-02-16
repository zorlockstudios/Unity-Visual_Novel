//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.

using UnityEngine;
using System.Collections;

public class ViNoAnimationListener : ViNoEventListener {
		
	void AnimationFinishedCallback(){		
		// Enable Updating VM.
		if( VM.Instance != null ){
			VM.Instance.EnableUpdate( true );		
		}
		
	}	
	
}
