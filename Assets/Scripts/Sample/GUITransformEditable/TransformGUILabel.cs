//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
using UnityEngine;
using System.Collections;

[ ExecuteInEditMode ]
public class TransformGUILabel : MonoBehaviour {
	public string label;

	private Rect m_Rect;
	private Transform thisTransform;
	
	protected float originalWidth = 854.0f;
	protected float originalHeight = 480.0f;
	protected Vector3 s = Vector3.zero;	
	protected Matrix4x4 svMat;
	
	// Use this for initialization
	void Awake () {
		thisTransform = transform;
	}
	
	void OnGUI(){			
		Vector3 pos = thisTransform.position;
		Vector3 scale = thisTransform.localScale;

		pos.x += Screen.width /2f;
		pos.y += - Screen.height /2f;
		m_Rect = new Rect( pos.x , - pos.y , scale.x , scale.y );

		GUI.Label( m_Rect , label );

	}
	
}
