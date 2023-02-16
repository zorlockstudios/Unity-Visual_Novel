//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Selections node.
/// Display Dialog Selections
/// </summary>
[ System.Serializable ]
[AddComponentMenu("ViNo/Nodes/Label_Jump/Selections")]
public class SelectionsNode : ViNode {
	[System.Serializable ]
	public class SelectUnit{
		[HideInInspector] public int index;
		public ViNode targetNode;
		public string text;
		[HideInInspector] public bool checkFlag;
		[HideInInspector] public string flagName;
	}	
	public string m_Title = string.Empty;
	public SelectUnit[] units;

	[HideInInspector] public Vector3 position = Vector3.zero;
	
	void Start(){
//		NotEditable();		
	}

	static public bool IsTargetSet( SelectionsNode.SelectUnit[] units ){
		for( int i=0;i<units.Length;i++){
			if( units[ i ].targetNode == null ){
				Debug.Log( "TargetNode is not set" );				
				return false;
			}
		}
		return true;
	}
	
	public override void ToScenarioScript( ref System.Text.StringBuilder sb ){
		if( units != null && units.Length > 0 ){
			for( int i=0;i<units.Length;i++){
				ViNode target = units[ i ].targetNode;
				string targetStr = target.GetNodeTag( target.name );
				sb.Append( "[link target=*"+ targetStr + "]" + units[ i ].text + "[endlink][r]" );	// This is KiriKiri Script.
				sb.Append( System.Environment.NewLine );
			}
			sb.Append( "[s]" );
			sb.Append( System.Environment.NewLine );
		}
	}

	public override string GetNodeLabel(){
		return GetNodeTag( name );//+ "_SELE" );
	}

	public override void ToByteCode( ByteCodes code ){
		List<byte> byteList=  new List<byte>();
		
//		AddNodeCodeWithTag( byteList , name );
		AddNodeCode( byteList );
//		AddNodeCodeWithTag( byteList , GetNodeLabel() );//name + "_TEST_SEL" );

		CodeGenerator.GenerateSelections( byteList , units , m_Title );
		
		code.Add( byteList.ToArray() );
				
		ToByteCodeInternal( code );
		
	}
	
}
