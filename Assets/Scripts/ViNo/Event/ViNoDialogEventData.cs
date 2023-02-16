//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
using UnityEngine;
using System.Collections;

public class ViNoNodeEvtData : ViNoEventData {
	public GameObject nodeObject;
	public string nodeName;
}

public class ViNoDialogEventData : ViNoNodeEvtData {
	public DialogPartData dialogData;
}
