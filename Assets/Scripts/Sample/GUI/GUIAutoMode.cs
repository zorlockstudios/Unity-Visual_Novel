//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.

using UnityEngine;
using System.Collections;

[ ExecuteInEditMode ]
public class GUIAutoMode : GUIBase {
	public Texture2D m_AutoModeTex;
	
	public override void OnGUICustom(){		
		float w = 200f;		
		float h = 50f;
		m_AreaRect = new Rect( Screen.width - w , Screen.height - h * 2 , w , Screen.height  );
		
		GUILayout.BeginArea( m_AreaRect );		
			if( m_AutoModeTex !=null ){
				ViNo.autoMode = GUILayout.Toggle( ViNo.autoMode , m_AutoModeTex , GUILayout.Width( w ) , GUILayout.Height( h )  );					
			}
			else{
				ViNo.autoMode = GUILayout.Toggle( ViNo.autoMode, "AutoMode" , GUILayout.Width( w ) , GUILayout.Height( h )  );								
			}

#if false		
			ViNo.skip = GUILayout.Toggle( ViNo.skip , "Skip" , GUILayout.Width( w ) , GUILayout.Height( h )  );					
#endif	
		
		GUILayout.EndArea();		
		
	}
	
}
