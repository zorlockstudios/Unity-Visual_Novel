//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
using UnityEngine;
using UnityEditor;
using System.Collections;

[ CustomEditor(typeof(PlaySoundNode))]
public class PlaySoundNodeInspector : Editor {
	
	static private ISoundPlayer m_SoundPlInstance;
	static private string[] m_SoundEntries;
	static private string[] m_SeEntries;
	static private string[] m_VoiceEntries;
	
	/// <summary>
	/// OnEnable and Get the Music Names.
	/// </summary>
	void OnEnable( ){
		if( m_SoundPlInstance == null ){
			m_SoundPlInstance = GameObject.FindObjectOfType( typeof( ISoundPlayer ) ) as ISoundPlayer;
		}
		
		if( m_SoundPlInstance != null ){
			if( m_SoundPlInstance as ViNoSoundPlayer ){
				ViNoSoundPlayer pl = m_SoundPlInstance as ViNoSoundPlayer;
				m_SoundEntries	= pl.GetSoundEntryNames();	
				m_VoiceEntries	= pl.GetVoiceEntryNames();	
				m_SeEntries 	= pl.GetSeEntryNames();			
			}
		}
	}	
	
	public override void OnInspectorGUI(){
		ViNoEditorUtil.BeginSoundGUIColor();
		
		PlaySoundNode node = target as PlaySoundNode;
				
		node.m_SoundType = (PlaySoundNode.SoundType)EditorGUILayout.EnumPopup( "Category" , node.m_SoundType );// , GUILayout.Width( 75f ) );

		if( m_SoundPlInstance as ViNoSoundPlayer ){

			switch( node.m_SoundType ){
				case PlaySoundNode.SoundType.MUSIC:			
					node.m_SoundID =  EditorGUILayout.Popup( "Music" , node.m_SoundID , m_SoundEntries );
					if( m_SoundEntries != null && m_SoundEntries.Length > 0 ){
						if( node.m_SoundID >= m_SoundEntries.Length ){
							node.m_SoundID = 0;
						}
						node.m_SoundName = m_SoundEntries[ node.m_SoundID ];
					}		
					break;

				case PlaySoundNode.SoundType.SE:			
					node.m_SoundID =  EditorGUILayout.Popup( "SE" , node.m_SoundID , m_SeEntries  );
					if( m_SeEntries != null && m_SeEntries.Length > 0 ){
						if( node.m_SoundID >= m_SeEntries.Length ){
							node.m_SoundID = 0;
						}
						node.m_SoundName = m_SeEntries[ node.m_SoundID ];
					}				
					break;

				case PlaySoundNode.SoundType.VOICE:			
					node.m_SoundID =  EditorGUILayout.Popup( "Voice" , node.m_SoundID , m_VoiceEntries  );
					if( m_VoiceEntries != null && m_VoiceEntries.Length > 0 ){
						if( node.m_SoundID >= m_VoiceEntries.Length ){
							node.m_SoundID = 0;
						}
						node.m_SoundName = m_VoiceEntries[ node.m_SoundID ];
					}		
					break;
			}

//			EditorGUILayout.EndHorizontal();
		}
		else if( m_SoundPlInstance as SimpleSoundPlayer ){
			node.m_SoundName =  EditorGUILayout.TextField( "Path" , node.m_SoundName ); // new GUIContent( "path" ,  ViNoEditorResources.folderIcon ).
		}
		
		node.m_Delay = EditorGUILayout.Slider( "Delay(sec)" , node.m_Delay , 0f , 5f );		
		
		ViNoEditorUtil.EndSoundGUIColor();
	}
	
}
