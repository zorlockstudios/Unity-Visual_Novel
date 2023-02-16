//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// LoadImage Node.
/// 
/// </summary>
[ ExecuteInEditMode ]
[AddComponentMenu("ViNo/Nodes/Object/LoadImage")]
[ System.Serializable]
public class LoadImageNode : LoadResourceNode {
	public GameObject targetObject;	
	public string resourceAssetPath;
	public string m_TargetObjectName;
	
	void OnEnable(){
		if( targetObject == null ){
			targetObject = GameObject.Find( m_TargetObjectName );
		}
	}

	public string GetTargetName(){
		if( targetObject != null ){
			m_TargetObjectName = ViNoGOExtensions.GetNameAppendedParent( targetObject );
		}
		return m_TargetObjectName;
	}					
	
	public override void ToScenarioScript( ref System.Text.StringBuilder sb ){
		sb.Append( "[image layer=" + GetTargetName() + " storage=" + anyResourcePath + "]" );
		sb.Append( System.Environment.NewLine );
	}
		
	public override void ToByteCode( ByteCodes code  ){
		base.ToByteCode( code );
		
		List<byte> byteList  = new List<byte>();
		string target = GetTargetName();
//		Debug.Log( "LoadImageTarget:" + target );
		ByteCodeScriptTools.AddMessagingCode( byteList , target , OpcodeMessaging.SET_RESOURCE_AS_TEXTURE );
		
		code.Add( byteList.ToArray() );
		
		ToByteCodeInternal( code );
	}
}