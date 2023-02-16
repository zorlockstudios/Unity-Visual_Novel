//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.

using UnityEngine;
using System.Collections;

[ ExecuteInEditMode ]
public class GUINextMessage : GUIBase {	
	public float m_Height = 100f;
//	public float m_LeftSpace;
//	public float m_CenterSpace = 100f;
		
	public override void OnStart (){
	}
	
	void OnClickNextMessage( ){
		ViNo.NextMessage();
	}
	
	void Update(){
//		if( Input.touches.	
	}
	
	public override void OnGUICustom(){								
		
		m_AreaRect = new Rect( 0f ,  Screen.height - m_Height , Screen.width , Screen.height  );		
		
		float w = Screen.width/3f;
		
		GUILayout.BeginArea( m_AreaRect  );			
				
			GUILayout.BeginHorizontal();
		
				GUILayout.Space( w );
				
				if( GUILayout.Button( "Next" ,  GUILayout.Width( w ) , GUILayout.Height( m_Height ) ) ){
					OnClickNextMessage();	
				}		
						
			GUILayout.EndHorizontal();
		
		GUILayout.EndArea();		
	}
}
