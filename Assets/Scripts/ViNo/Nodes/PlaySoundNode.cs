//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

/// <summary>
/// Play sound node.
/// </summary>
[ System.Serializable]
[AddComponentMenu("ViNo/Nodes/Audio/PlaySound")]
public class PlaySoundNode : ViNode {
	
	public enum SoundType{
		MUSIC=0,
		SE=1,
		VOICE=2,
	}
		
	public string m_SoundName;
	public float	m_Delay = 0f;
	public bool loop;
	public SoundType m_SoundType = SoundType.MUSIC;
	public int m_SoundID = 0;
	
	void Start(){
//		NotEditable();		
	}
	
	// TODO.
	public override void ToScenarioScript( ref System.Text.StringBuilder sb ){
		sb.Append( "[playbgm storage=" + m_SoundName + "]" );
		sb.Append( System.Environment.NewLine );
	}
	
	public override void ToByteCode( ByteCodes code  ){
		List<byte> byteList  = new List<byte>();		
		ISoundPlayer pl = ISoundPlayer.Instance;
		if( pl as ViNoSoundPlayer ){
			byteList.Add( Opcode.PLAY_SOUND );
			byteList.Add( (byte)m_SoundType );
			byteList.Add( (byte)m_SoundID );
		}
		else if( pl as SimpleSoundPlayer ) {
			string tag = "";
			switch( m_SoundType ){
				case SoundType.MUSIC:	tag = "playbgm";	break;	
				case SoundType.SE:		tag = "playse";		break;
				case SoundType.VOICE:	tag = "playvoice";	break;
				default:
					Debug.LogError( "undefined SoundType : " + m_SoundType.ToString() );
					break;
			}
			string loopStr = loop ? "true" : "false";

			CodeGenerator.AddPlaySoundCode( byteList , tag , m_SoundName , loopStr );			
		}		
		code.Add( byteList.ToArray() );		
		ToByteCodeInternal( code );		
	}
}

