//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
using UnityEngine;
using System.Collections;

public class GUISelection : MonoBehaviour {
	public GUISkin skin;
	public bool useStyle;
	public GUIStyle style;
	
	public string text{ get; set; }
	
	private GUISelectionsCtrl ctrl;
	private Rect rect;
	private Vector2 pos;
	private float width = 200f;
	private float height = 100f;
	
	public void SetGUIDiglogCtrl( GUISelectionsCtrl ctrl ){
		this.ctrl = ctrl;
	}
	
	public void SetPosition( Vector2 pos , float width , float height ){
		this.pos = pos;
		this.width = width;
		this.height = height;
	}
			
	void OnGUI(){
		if( skin != null ){
			GUI.skin = skin;	
		}
		if( useStyle ){
			rect = new Rect( pos.x  , pos.y , width , height );		
			
			if( GUI.Button( rect , text , style ) ){	
				if( ctrl != null ){
					ctrl.OnClickSelectCallback( this.gameObject );
				}
			}						
		}
		else{
			rect = new Rect( pos.x  , pos.y , width , height );		
			
			if( GUI.Button( rect , text ) ){	
				if( ctrl != null ){
					ctrl.OnClickSelectCallback( this.gameObject );
				}
			}		
		}
	}
	
}
