//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
using UnityEngine;
using System.Collections;

[ ExecuteInEditMode ]
[ System.Serializable ]
public class TransformGUIToolbar : MonoBehaviour {
	public Texture2D[] textures;
	public GameObject eventReceiver;
	public string sendMessage;
	
	[ Range( 1 , 10 ) ]
	public int toolItemXNum = 3;
	public string[] toolItems;
	
	[ SerializeField ] private int m_SelectedID;
	
	private Rect m_Rect;
	private Transform thisTransform;
	
	// Use this for initialization
	void Start () {
		thisTransform = transform;
	}
	
	void OnGUI(){	
		Vector3 pos = thisTransform.position;
		Vector3 scale = thisTransform.localScale;		
		pos.x += Screen.width /2f;
		pos.y += - Screen.height /2f;
		m_Rect = new Rect( pos.x , - pos.y , scale.x , scale.y );
		if( toolItems != null ){
//			m_SelectedID = GUI.Toolbar( m_Rect , m_SelectedID , toolItems  );			
			m_SelectedID = GUI.SelectionGrid( m_Rect , m_SelectedID , toolItems , toolItemXNum );
		}
		else if( textures !=null ) {
//			m_SelectedID = GUI.Toolbar( m_Rect , m_SelectedID , textures );			
			m_SelectedID = GUI.SelectionGrid( m_Rect , m_SelectedID , textures , toolItemXNum  );
		}				
		if( GUI.changed ){
			if( eventReceiver != null ){
				eventReceiver.SendMessage( sendMessage , m_SelectedID , SendMessageOptions.DontRequireReceiver );
			}
		}
	}
	
}
