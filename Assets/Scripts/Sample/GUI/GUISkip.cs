//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.

using UnityEngine;
using System.Collections;

public class GUISkip : GUIBase {
	public float m_Height = 100f;
	
	private bool m_ToggleSkip;

	public override void OnGUICustom(){		
		float w = 200f;		
		m_AreaRect = new Rect( Screen.width - w , 0f , w , Screen.height  );
		
		GUILayout.BeginArea( m_AreaRect );		

			m_ToggleSkip = GUILayout.Toggle(  m_ToggleSkip , "Skip" );			
			ViNo.skip = m_ToggleSkip;
			
		GUILayout.EndArea();		
		
	}	
}
