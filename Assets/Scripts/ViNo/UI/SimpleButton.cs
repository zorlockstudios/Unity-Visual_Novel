//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
using UnityEngine;
using System.Collections;

[ AddComponentMenu( "ViNo/UI/Button" )]
public class SimpleButton : ViNoToolkit.ButtonBase{

	public override bool OnTouchBegan(){
		if( TouchScreen.selectedGameObject == this.gameObject ){
			ViNoDebugger.Log( "INPUT" , "OnTouchBegan :" + name );
			m_TouchObjectResponse = touchResponse;			
			switch( touchResponse ){
				case TouchResponse.OFFSET:
					m_TouchedObject = this.gameObject;
					m_TouchBeganAmount = m_TouchedObject.transform.localPosition;
					m_TouchedObject.transform.localPosition = m_TouchBeganAmount + offsetAmount;// new Vector3( 5f , - 5f , 0f );
					break;
				case TouchResponse.SCALE:
					m_TouchedObject = this.gameObject;
					m_TouchBeganAmount = m_TouchedObject.transform.localScale;
					m_TouchedObject.transform.localScale = new Vector3( m_TouchBeganAmount.x * scaleAmount.x ,m_TouchBeganAmount.y * scaleAmount.y , scaleAmount.z );
					break;
				case TouchResponse.NONE:
					break;
			}
			return true;
		}
		else{
			return false;
		}
	}

	public override bool OnTouchOut(){
		if( m_TouchedObject != null ){
			ViNoDebugger.Log( "INPUT" , "OnTouchOut:" + name );
			ResetBeganAmount( m_TouchedObject.transform );
			m_TouchedObject = null;
		}
		return true;
	}

	public override bool OnTouchEnded(){		
		if( m_TouchedObject != null ){
			ResetBeganAmount( m_TouchedObject.transform );
			m_TouchedObject = null;
		}
		if( TouchScreen.selectedGameObject == this.gameObject ){
			ViNoDebugger.Log( "INPUT" , "OnTouchEnded :" + name );			
			if( findObjectAndSendMessage ){
				eventReceiver = GameObject.Find( targetObjectName );
				if( eventReceiver == null ){
					Debug.Log( "Target :" + targetObjectName + " not found." );
				}
			}
			if( eventReceiver != null ){
				if( ! string.IsNullOrEmpty( paramString )){
					eventReceiver.SendMessage( sendMessage , paramString , SendMessageOptions.DontRequireReceiver );
				}
				else{
					eventReceiver.SendMessage( sendMessage , SendMessageOptions.DontRequireReceiver );
				}
			}						
			return true;
		}
		else{
			return false;
		}
	}

}
