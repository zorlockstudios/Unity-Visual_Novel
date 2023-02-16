//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Wait node.
/// in seconds.
/// </summary>
[AddComponentMenu("ViNo/Nodes/System/Wait")]
[ System.Serializable ]
public class WaitNode : ViNode {
	[ Range( 0 , 5 ) ]
	public float m_WaitSec = 1f;
		
	void Start(){
		
	}

	/// <summary>
	/// Tos the scenario script.
	/// </summary>
	/// <param name='sb'>
	/// Sb.
	/// </param>
	public override void ToScenarioScript( ref System.Text.StringBuilder sb ){
		int ms = (int)( m_WaitSec * 1000f );		
		string script = "[wait time=" + ms.ToString() + "]" + System.Environment.NewLine;
		sb.Append( script );
	}	
	
	/// <summary>
	/// Tos the byte code.
	/// </summary>
	/// <param name='code'>
	/// Code.
	/// </param>
	public override void ToByteCode( ByteCodes code ){					
		List<byte> byteList = new List<byte>();
		
//		AddNodeCodeWithTag( byteList , name );
		AddNodeCode( byteList );
//		AddNodeCodeWithTag( byteList , name );
				
		CodeGenerator.GenerateWaitCode( byteList , m_WaitSec );
		
		code.Add( byteList.ToArray() );		
		
		// ToByteCode this Children.
		ToByteCodeInternal( code );		
	}
}



