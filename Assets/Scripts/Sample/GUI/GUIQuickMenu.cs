//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
[ ExecuteInEditMode ]
public class GUIQuickMenu : GUIBase {	
	public ViNoTextBox textBox;
	
	public Text statusLabel;	
	public Texture2D quickMenuTex;
	
	public float m_ButtonHeight = 40f;
	public float m_VerticalSpace = 15f;			
	
	private bool m_ShowMenu;
	
	public override void OnStart (){
	}
		
	public override void OnGUICustom(){	
				
		m_AreaRect = new Rect( Screen.width - m_AreaWidth ,  0f , m_AreaWidth , Screen.height  );		
		GUILayout.BeginArea( m_AreaRect );
				
		if( quickMenuTex != null ){
			if( GUILayout.Button( quickMenuTex ) ){
				m_ShowMenu = ! m_ShowMenu;
			}								
		}
		else{
			if( GUILayout.Button( "Menu" , GUILayout.Height( m_ButtonHeight )  ) ){
				m_ShowMenu = ! m_ShowMenu;
			}			
		}
		
			GUILayout.Space( m_VerticalSpace );
		
			if( m_ShowMenu ){		
// TODO.				
#if true
				if( GUILayout.Button( "QuickSave" , GUILayout.Height( m_ButtonHeight )  ) ){					
					Debug.Log( "Quick Save" );
					if( statusLabel != null ){
						statusLabel.text = "Saved !";
					}
					if( ViNoAPI.DoQuickSave() ){
						Debug.Log( "Quick Save succeeded" );
					}							
				}
			
				GUILayout.Space( m_VerticalSpace );
				
				if( GUILayout.Button( "QuickLoad" , GUILayout.Height( m_ButtonHeight ) ) ){									
					Debug.Log( "Quick Load" );
					if( statusLabel != null ){
						statusLabel.text = "Load !";
					}
					if( ViNoAPI.DoQuickSave() ){
						Debug.Log( "Quick Load succeeded" );
					}							
				}
#endif				
				GUILayout.Space( m_VerticalSpace );
			
				if( GUILayout.Button( "Back Log" ,  GUILayout.Height( m_ButtonHeight ) ) ){
					ViNo.ToggleShowBackLog();
					
					sampleGUI.EnableShowBackLog( true );
					sampleGUI.EnableShowQuickMenu( false );
					sampleGUI.EnableShowNextMessage( false );				
				
					m_ShowMenu = false;
				}		
			
				GUILayout.Space( m_VerticalSpace );	
			}		
		
		GUILayout.EndArea();
		
	}
			
}
