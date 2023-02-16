//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
using UnityEngine;
using System.Collections;

public class TouchScreen : MonoBehaviour {
	static public GameObject selectedGameObject;

	private RaycastHit hit = new RaycastHit();
	private Ray ray = new Ray();
	
	// Update is called once per frame	
	void Update () {		
		bool rayTest = false;
		string broadcastMessage = "";
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER
	    // if left clicked,then test raycast.
	    rayTest = Input.GetMouseButtonDown( 0 ) || Input.GetMouseButtonUp( 0 ) || Input.GetMouseButton( 0 );
	    if( Input.GetMouseButtonDown( 0 ) ){
	    	broadcastMessage = "OnTouchBegan";
	    }
	    else if( Input.GetMouseButton( 0 ) ){
	    	broadcastMessage = "OnTouchMoving";
	    }
	    else if( Input.GetMouseButtonUp( 0 ) ){
	    	broadcastMessage = "OnTouchEnded";
	    }

	    if( rayTest ){
		    ray = GetComponent<Camera>().ScreenPointToRay( Input.mousePosition );
	    }
	    
#elif UNITY_IPHONE || UNITY_ANDROID

		if ( Input.touches.Length > 0){
		    Touch touch = Input.touches[ 0 ];
		    ray = camera.ScreenPointToRay( touch.position );    
			if( touch.phase == TouchPhase.Began ){
				broadcastMessage = "OnTouchBegan";
			}
			else if( touch.phase == TouchPhase.Moved ){
				broadcastMessage = "OnTouchMoving";				
			}
			else if( touch.phase == TouchPhase.Ended ){
				broadcastMessage = "OnTouchEnded";
			}
		    rayTest = true;
		}
#endif
		if( rayTest ){
		    if (Physics.Raycast( ray , out hit , 100)) {
		    	selectedGameObject = hit.collider.gameObject;
			    if( ! string.IsNullOrEmpty( broadcastMessage ) ){
				    BroadcastMessage( broadcastMessage , SendMessageOptions.DontRequireReceiver );
				}
		    }
		    else {
			    BroadcastMessage( "OnTouchOut" , SendMessageOptions.DontRequireReceiver );
		    	selectedGameObject = null;
		    }			    
		}
	}
}


