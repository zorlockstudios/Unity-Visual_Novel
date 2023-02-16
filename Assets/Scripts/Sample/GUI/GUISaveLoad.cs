//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
using UnityEngine;
using System.Collections;

[ ExecuteInEditMode ]
public class GUISaveLoad : GUIBase {
						
	public override void OnGUICustom(){		
//		m_AreaRect = new Rect( Screen.width - m_AreaWidth ,  0f , m_AreaWidth , Screen.height  );		
//		GUILayout.BeginArea( m_AreaRect );
						
			if( GUILayout.Button( "QuickSave" , GUILayout.Width( 200f ) , GUILayout.Height( 50f )  ) ){					
				if( ViNoAPI.DoQuickSave() ){
					Debug.Log( "Quick Save succeeded" );
				}							
			}
					
			if( GUILayout.Button( "QuickLoad" , GUILayout.Width( 200f ) , GUILayout.Height( 50f ) ) ){									
				if( ViNoAPI.DoQuickLoad() ){
					Debug.Log( "Quick Load succeeded" );
				}							
			}
						
//		GUILayout.EndArea();
		
	}		
}
