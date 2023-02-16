//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Set Message Target.
/// </summary>
[ System.Serializable]
[AddComponentMenu("ViNo/Nodes/Message/SetText")]
public class SetTextNode : ViNode {
	public ViNoTextBox targetTextBox;
	[ MultilineAttribute ]
	public string text;
	
// TODO : IMPL		
	public override void ToScenarioScript( ref System.Text.StringBuilder sb ){
//		sb.Append( "[current layer=" + textBoxName + "]" );
//		sb.Append( System.Environment.NewLine );
	}
	
	public override void ToByteCode( ByteCodes code  ){
		List<byte> byteList  = new List<byte>();
//		AddNodeCodeWithTag( byteList , name );				

		AddNodeCode( byteList );

		CodeGenerator.AddSetTextCode( byteList , text , targetTextBox.name );
		code.Add( byteList.ToArray() );
		
		ToByteCodeInternal( code );
	}
}

