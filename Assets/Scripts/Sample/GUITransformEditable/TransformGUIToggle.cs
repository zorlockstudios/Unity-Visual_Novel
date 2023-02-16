//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
using UnityEngine;
using System.Collections;

[ ExecuteInEditMode ]
public class TransformGUIToggle : MonoBehaviour {
	public string label;
	public Texture2D texture;
	public GameObject onChanged;
	public string sendMessage;
	
	private Rect m_Rect;
	private bool m_Checked;
	private Transform thisTransform;
	
	// Use this for initialization
	void Start () {
		thisTransform = transform;
	}
	
	void OnGUI(){		
		Vector3 pos = thisTransform.localPosition;
		Vector3 scale = thisTransform.localScale;		
		pos.x += Screen.width /2f;
		pos.y += - Screen.height /2f;
		m_Rect = new Rect( pos.x , - pos.y , scale.x , scale.y );
		if( texture == null ){
			m_Checked = GUI.Toggle( m_Rect , m_Checked , label );
		}
		else{
			m_Checked = GUI.Toggle( m_Rect ,  m_Checked , texture );
		}		
		
		if( GUI.changed ){
			if( onChanged != null ){
				onChanged.SendMessage( sendMessage , m_Checked , SendMessageOptions.DontRequireReceiver );
			}
		}			
	}
	
}
