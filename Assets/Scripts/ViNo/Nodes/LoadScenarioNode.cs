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
[AddComponentMenu("ViNo/Nodes/Label_Jump/LoadScenario")]
public class LoadScenarioNode : CreateObjectNode {

	public override void ToScenarioScript( ref System.Text.StringBuilder sb ){
		sb.Append( "[triggerevent eventType=OnPlayScenario " + "name=" + anyResourcePath + "]" );
		sb.Append( System.Environment.NewLine );
	}

}