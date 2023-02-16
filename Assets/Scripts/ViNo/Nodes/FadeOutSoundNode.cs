//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

/// <summary>
/// Fade out Sound.
/// </summary>
[ System.Serializable]
[AddComponentMenu("ViNo/Nodes/Audio/FadeOutSound")]
public class FadeOutSoundNode : ViNode {	
	public enum SoundType{
		MUSIC=0,
//		SE=1,
//		VOICE=2
	}	
	public SoundType m_SoundType = SoundType.MUSIC;
	
	[Range( 0f , 10f ) ]
	public float	m_FadeOutSeconds = 2f;

	[HideInInspector] public string m_SoundName;
	
	void Start(){
//		NotEditable();		
	}
	
	public override void ToByteCode( ByteCodes code  ){
		List<byte> byteList = new List<byte>();

		AddNodeCode( byteList );
		
		ISoundPlayer pl = ISoundPlayer.Instance;
		if( pl as ViNoSoundPlayer ){
			Hashtable tbl = new Hashtable();
			tbl[ "name" ] = m_SoundName;
			switch( m_SoundType ){
				case SoundType.MUSIC:
					tbl[ "category" ] = "Music";
					break;

/*			case SoundType.SE:
					tbl[ "category" ] = "SE";
					break;
				
			case SoundType.VOICE:
					tbl[ "category" ] = "Voice";
					break;
//*/					
			}
			tbl[ "fadeOutSeconds" ] = m_FadeOutSeconds.ToString();			
			ByteCodeScriptTools.AddTablePairsCode( byteList , tbl );
			ByteCodeScriptTools.AddMessagingCode( byteList , "env" , OpcodeMessaging.STOP_SOUND );			
		}
		else if ( pl as SimpleSoundPlayer  ){
			Hashtable args = new Hashtable();
			args[ "eventType" ] = "fadeoutbgm";
			int time  = (int)( m_FadeOutSeconds * 1000f );
			args[ "time" ] = time.ToString();
			ByteCodeScriptTools.AddTablePairsCode( byteList , args );
			ByteCodeScriptTools.AddMessagingCode( byteList , " " , OpcodeMessaging.TRIGGER_EVENT_WITH_ARGS );
		}		
		code.Add( byteList.ToArray() );

		ToByteCodeInternal( code );
	}
}

