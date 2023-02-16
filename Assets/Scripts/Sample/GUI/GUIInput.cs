//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.

using UnityEngine;
using System.Collections;

[ ExecuteInEditMode ]
public class GUIInput : GUIBase {
//	public Texture2D m_AutoModeTex;
	private string m_InputStr = "";
	
	public override void OnGUICustom(){		
//		float w = 200f;		
//		float h = 50f;

		m_AreaRect = new Rect( Screen.width/2f - 100f , Screen.height/2f , m_AreaWidth , Screen.height  );
		GUILayout.BeginArea( m_AreaRect );

			GUILayout.BeginHorizontal();

				m_InputStr = GUILayout.TextField( m_InputStr , GUILayout.Width( m_AreaWidth - 200f ) );//, GUILayout.Height( h )  );								
					
				if( GUILayout.Button( "OK" , GUILayout.Width( 200f ) ) ){
					ScenarioNode.Instance.flagTable.SetStringValue( "username" , m_InputStr );				
					Destroy( this.gameObject );			
				}

			GUILayout.EndHorizontal();

		GUILayout.EndArea();		
		
	}
	
}
