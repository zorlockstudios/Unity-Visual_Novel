//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

/// <summary>
/// Load level node.
/// </summary>
[ System.Serializable]
[AddComponentMenu("ViNo/Nodes/Label_Jump/LoadLevel")]
public class LoadLevelNode : ViNode {
	public string m_NextLevelName;
	
	void Start(){
		NotEditable();		
	}
		
	public override void ToByteCode( ByteCodes code  ){
		List<byte> byteList = new List<byte>();
		
//		AddNodeCodeWithTag( byteList , name );
		AddNodeCode( byteList );
		
		ByteCodeScriptTools.AddTextLiteralCode( byteList , m_NextLevelName );
		ByteCodeScriptTools.AddMessagingCode( byteList , "env" , OpcodeMessaging.LOAD_LEVEL );			
		
		code.Add( byteList.ToArray() );

		ToByteCodeInternal( code );
		
	}
}
