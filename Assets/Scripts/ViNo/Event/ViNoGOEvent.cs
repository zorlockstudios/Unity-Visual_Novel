//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.

using UnityEngine;
using System.Collections;

/// <summary>
/// Vi no GO created evt data. GO is GameObject.
/// </summary>
public class ViNoGOCreatedEvtData : ViNoEventData{
	public string goName;
	
	public ViNoGOCreatedEvtData( string name ){
		goName = name;	
	}
}
