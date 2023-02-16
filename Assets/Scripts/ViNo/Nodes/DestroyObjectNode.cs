//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Load ScenarioNode Attached GameObject from Resources.
/// </summary>
[ System.Serializable]
[AddComponentMenu("ViNo/Nodes/Object/DestroyObject")]
public class DestroyObjectNode : ViNode {
	public GameObject targetObject;	// set a prefab.
	
// TODO:		
	public override void ToScenarioScript( ref System.Text.StringBuilder sb ){
//		sb.Append( "[destroy TODO]" );		
//		sb.Append( System.Environment.NewLine );
	}
	
	public override void ToByteCode( ByteCodes code  ){
		base.ToByteCode( code );
		List<byte> byteList = new List<byte>();		
		
		// Add Parent Name.
		if( targetObject != null ){
			ByteCodeScriptTools.AddTextLiteralCode( byteList ,  targetObject.name ) ;
		}
		else{
			Debug.LogError( "DestroyObjectNode: target not set !" );
		}
		
		byteList.Add( Opcode.DESTROY_OBJECT );	
		
		code.Add( byteList.ToArray () );
		
		ToByteCodeInternal( code );
	}
}