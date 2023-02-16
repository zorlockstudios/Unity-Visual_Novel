//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Jump to another TargetNode.
/// </summary>
[AddComponentMenu("ViNo/Nodes/Label_Jump/JumpTarget")]
[ System.Serializable ]
public class JumpTargetNode : ViNode {
	public ViNode jumpTarget;
	
	void Start(){
//		NotEditable();		
	}
	
	/// <summary>
	/// Tos the scenario script.
	/// </summary>
	/// <param name='sb'>
	/// Sb.
	/// </param>
	public override void ToScenarioScript( ref System.Text.StringBuilder sb ){
		string script = "[jump target=*" + jumpTarget.GetNodeTag( jumpTarget.name ) + "]";
		sb.Append( script );
	}
	
	public override void ToByteCode( ByteCodes code ){				
		if( jumpTarget != null ){		
						
			List<byte> byteList = new List<byte>();
			
			AddNodeCode( byteList );

			string tag = jumpTarget.GetNodeTag( jumpTarget.name );			// TODO: GetNodeLabel			
			ByteCodeScriptTools.AddJumpTargetCode( byteList , tag );		
						
			code.Add( byteList.ToArray() );								
		}
		else{			
			Debug.LogError( "JumpTargetNode:" + name + " isn't set jumpTarget." );					
		}

		ToByteCodeInternal( code );
		
	}
	
}
