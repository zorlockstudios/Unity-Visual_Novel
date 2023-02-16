//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.

using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ViNoEventManager : MonoBehaviour {
	public GameObject eventReceiver;
//	public GameObject messageEventReceiver;

	static private ViNoEventManager m_Instance;
	public static ViNoEventManager Instance {
		get {	return ViNoEventManager.m_Instance;	}
	}
			
	public void TriggerEvent( string evttype ){
		if( IsEvtReceiverAttach( ) ){
		 	eventReceiver.SendMessage( evttype , SendMessageOptions.DontRequireReceiver );			
		}
	}

	public void TriggerEvent( string evttype , string sendmessage ){
		if( IsEvtReceiverAttach( ) ){
		 	eventReceiver.SendMessage( evttype , sendmessage ,  SendMessageOptions.DontRequireReceiver );							
		}
	}

	public void TriggerEvent( string evttype , Hashtable args ){
		if( IsEvtReceiverAttach( ) ){
		 	eventReceiver.SendMessage( evttype , args , SendMessageOptions.DontRequireReceiver );							
		}
	}
	
	public void TriggerEvent( string evttype , ViNoEventData evtdata ){
		if( IsEvtReceiverAttach( ) ){
		 	eventReceiver.SendMessage( evttype , evtdata ,  SendMessageOptions.DontRequireReceiver );							
		}
	}

	void Awake(){
//		if( m_Instance == null ){			
			m_Instance=  this;	
			
			DontDestroyOnLoad( gameObject );

			MessageEvent msgEvt = gameObject.GetComponent<MessageEvent>();
			if( msgEvt == null ){
				gameObject.AddComponent<MessageEvent>();
			}

//		}
/*		else{
			if( Application.isPlaying ){				
				Destroy( gameObject );		
			}
		}		
//*/		
	}

	public bool IsEvtReceiverAttach( ){
		if( eventReceiver == null ){
			ViNoDebugger.LogWarning( "ViNoEventManager eventReceiver Not Attached." );
			return false;
		}
		else{
			return true;
		}
	}

}
