//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.

using UnityEngine;
using System.Collections;

public class SampleFadeEventReceiver : MonoBehaviour {

	void DidFadeIn () {
		Debug.Log( "Did Fade In" );
	}
	
	void DidFadeOut () {
		Debug.Log( "Did Fade Out" );	
	}

}
