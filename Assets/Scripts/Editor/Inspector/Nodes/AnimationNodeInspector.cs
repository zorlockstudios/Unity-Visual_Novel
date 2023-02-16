//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.

using UnityEditor;
using UnityEngine;
using System.Collections;

[ CustomEditor( typeof( AnimationNode ) ) ] 
public class AnimationNodeInspector : Editor {
	
	/// <summary>
	/// Raises the inspector GUI event.
	/// </summary>
	public override void OnInspectorGUI(){				
#if true		
		AnimationNode node = target as AnimationNode;		
		
		Color saved = GUI.color;
		GUI.color = Color.yellow;
		 
		GUICommon.DrawAnimationNodeInspector( node );
		
		GUI.color = saved;
		
//		NodeGUI.OnGUI_ViNode( node , true , false );
#else
		DrawDefaultInspector();
#endif		
	}
}
