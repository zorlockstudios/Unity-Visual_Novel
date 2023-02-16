//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
using UnityEngine;
using System.Collections;

public class MessageEventData : ViNoEventData{	

	public MessageEventData(){}

	public MessageEventData( string msg ,  byte id ){
		message = msg;
		textBoxID = id;
	}

	public string message;
	public byte textBoxID;
	public bool show;
}
