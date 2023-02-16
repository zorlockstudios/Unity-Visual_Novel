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
[AddComponentMenu("ViNo/Nodes/Object/CreateObject")]
public class CreateObjectNode : LoadResourceNode {
	public GameObject parent;	// This is Instantiated Scenario Object Parent.
	
	[HideInInspector ][SerializeField ] private string m_ParentName = "";

	/// <summary>
	/// OnEnable and Get Parent by name if parent is null.
	/// </summary>
	void OnEnable(){
		if( parent == null &&	! string.IsNullOrEmpty( m_ParentName ) ){
			parent = GameObject.Find( m_ParentName );
		}
	}
	
	public override void ToScenarioScript( ref System.Text.StringBuilder sb ){
		sb.Append( "[instantiate storage=" + anyResourcePath + " " );
		if( parent != null ){
			sb.Append( "parent=" + parent.name );
		}
		sb.Append( "]");
		sb.Append( System.Environment.NewLine );
	}
	
	public override void ToByteCode( ByteCodes code  ){
		base.ToByteCode( code );

		List<byte> byteList = new List<byte>();		
		
		if( parent != null ){ 	
			ByteCodeScriptTools.AddCreateGOCode( byteList , parent.name );		
		}
		else{
			ByteCodeScriptTools.AddCreateGOCode( byteList , "" );		
		}	
		code.Add( byteList.ToArray () );
		
		ToByteCodeInternal( code );
	}

}