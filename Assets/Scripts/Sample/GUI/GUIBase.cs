//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.

using UnityEngine;
using System.Collections;

public class GUIBase : MonoBehaviour {
	public SampleGUI sampleGUI;
	
	public GUISkin m_Skin;
	public float m_AreaWidth = 250f;
	
	protected float originalWidth = 854f;
	protected float originalHeight = 480f;
	protected Vector3 scale = Vector3.zero;	
	protected Matrix4x4 svMat;
	protected Rect m_AreaRect;
	
	// Use this for initialization
	void Start () {
		OnStart();
	}
		
	void OnGUI(){
		PreOnGUI();
		
		OnGUICustom();
		
		PostOnGUI();		
	}
	
	virtual public void OnStart(){
		
	}
	
	virtual public void OnGUICustom(){
	
	}
	
	protected void PreOnGUI(){
		if( m_Skin != null ){
			GUI.skin = m_Skin;
		}
#if false						
		scale.x = Screen.width / originalWidth;
		scale.y = Screen.height / originalHeight;
		scale.z = 1f;
		
		svMat = GUI.matrix; // save current mat;
		GUI.matrix = Matrix4x4.TRS( Vector3.zero , Quaternion.identity , scale );		
#endif		
	}
	
	protected void PostOnGUI(){
#if false
		// End of Scaling.
		GUI.matrix = svMat;				
#endif		
	}
	
}
