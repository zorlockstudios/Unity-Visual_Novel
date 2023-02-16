//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
using UnityEngine;
using System.Collections;

/// <summary>
/// Clear All ViNoTextBox Messages.
/// </summary>
[AddComponentMenu("ViNo/Nodes/Message/ClearMessage")]
[ System.Serializable ]
public class ClearMessageNode : ViNode {

	public override void ToByteCode( ByteCodes code ){		
		code.Add( Opcode.CM );		
		// To Byte codes this Children.
		ToByteCodeInternal( code );
	}

}
