//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Trigger a Event.
/// At this Node , if ViNoEventManager exists in your scene , ViNoEventManager
/// Send a Message to the EventReceiver.
/// </summary>
[AddComponentMenu("ViNo/Nodes/System/TriggerEvent")]
[ System.Serializable ]
public class EventTriggerNode : ViNode {
	public string sendMessage = "";
		
	void Start(){
		
	}

	public override void ToScenarioScript( ref System.Text.StringBuilder sb ){
		sb.Append( "[triggerevent eventType=" + sendMessage + "]" );
		sb.Append( System.Environment.NewLine );
	}	
	
	/// <summary>
	/// Tos the byte code.
	/// </summary>
	/// <param name='code'>
	/// Code.
	/// </param>
	public override void ToByteCode( ByteCodes code ){					
		List<byte> byteList = new List<byte>();
		
		AddNodeCode( byteList );

#if false				
		Hashtable args = new Hashtable();
		args[ "eventType" ] = eventType;		
		args[ "ID" ] = messageID.ToString();		
		args[ "nodeName" ] = nodeName;		
		args[ "dialogText" ] = dialogText;
#endif
		
		ByteCodeScriptTools.AddTextLiteralCode( byteList , sendMessage );
		ByteCodeScriptTools.AddMessagingCode( byteList , " " , OpcodeMessaging.TRIGGER_EVENT );
		
		code.Add( byteList.ToArray() );		
		
		// ToByteCode this Children.
		ToByteCodeInternal( code );		
	}
}



