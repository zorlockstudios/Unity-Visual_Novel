//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Base Script node.
/// </summary>
[ System.Serializable ]
[AddComponentMenu("ViNo/Nodes/ViNode")]
public class ViNode : BaseNode {	
	
	[HideInInspector] public bool startAndActive = true;
	[HideInInspector] public bool useAlreadyCompiledCode;
	[HideInInspector] public TextAsset m_CompiledInternalCodeAsset;
	
//	static public string kCachedCodePath = "CompiledScenario/Cached/";

	void Start(){}

	static public void SaveByteCodesToFile( string filename , byte[] code ){
		ExternalAccessor.SaveAsBinaryFile( filename , code );
	}

	/// <summary>
	/// Compile the specified filename.
	/// </summary>
	/// <param name='filename'>
	/// Filename.
	/// </param>
	public void CompileInternalCode( string filename ){
		ByteCodes btcodes = CompileInternalCode();
		// Save as ByteCode File Resources.
		SaveByteCodesToFile( filename , btcodes.GetCode() );			
	}
	
	// --------------- Virtual Methods --------------------------------------.
	
	public override void ToScenarioScript( ref System.Text.StringBuilder sb ){
		sb.Append( "*" + GetNodeTag( name ) );
		sb.Append( System.Environment.NewLine );
	}
	
	public override void ToByteCode( ByteCodes code ){
		List<byte> byteList=  new List<byte>();
//		AddNodeCodeWithTag( byteList , name );		
		AddNodeCode( byteList );

		code.Add( byteList.ToArray() );
				
		ToByteCodeInternal( code );
	}
	
//	virtual public string SerializeXML( string fileName ){
//		return string.Empty;
//	}	
//	virtual public void DeserializeXML( string xmlStr ){}	
//	virtual public void SerializeTextScript() {}	
//	virtual public void DeserializeTextScript(){}
	
	public override string GetNodeLabel(){
		return GetNodeTag( name );
	}

	// --------------- public Methods --------------------------------------.

	public void AddNode<T>( ViNode aNode ) where T : ViNode{
		aNode.transform.parent = transform;
	}
	
	public void RefreshChildren(){
		ViNode[] nodes = GetComponentsInChildren<ViNode>();
		if( nodes != null ){
			for( int i=0;i<nodes.Length;i++){	
				Transform parantTra = nodes[ i ].transform.parent;
				nodes[ i ].transform.parent = null;
				nodes[ i ].transform.parent = parantTra;
			}
		}			
	}
	
	public void ReIndexChildren(){
		ViNode[] nodes = GetComponentsInChildren<ViNode>();
		if( nodes != null ){				
			int index = 0;
			GameObject nodeObj = null;
			for( int i=0;i<nodes.Length;i++){
				ViNode node = nodes[ i ];
				if( node.name.Equals ( name ) || node.gameObject == nodeObj  ){										
					continue;	// Dont Rename MySelf.
				}
				
				nodeObj = nodes[ i ].gameObject;

				// Extract 1_Dialog to "1_" and "Dialog".
				string[] strs = node.name.Split( "_"[0] );
				if( strs.Length > 1 ){
					node.name = ( index  ).ToString () + "_" + strs[ 1 ];
				}
				else{
					node.name = ( index  ).ToString () + "_" + node.name;					
				}
				index++;
			}
		}			
	}
		
	public void SetChild( ViNode childNode ){
		if( childNode == null ){
			return;
		}
		
		Transform thisTra = transform;
		int childCount = this.transform.childCount;
		int index = childCount - 1;
		
		childNode.name = index + "_" + childNode.name;
		childNode.transform.parent = thisTra;				
	}

}

