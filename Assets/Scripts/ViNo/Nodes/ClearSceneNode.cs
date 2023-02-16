//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
using UnityEngine;
using System.Collections;

/// <summary>
/// Clear All ViNoTextBox Messages.
/// </summary>
[AddComponentMenu("ViNo/Nodes/Scene/Clear")]
[ System.Serializable ]
public class ClearSceneNode : ViNode {
	
	static public void Do( GameObject advSceneRoot , bool immediateDestroy ){
		SceneCreator.DestroyScene( advSceneRoot , immediateDestroy );
	}

	public override void ToByteCode( ByteCodes code ){		
		code.Add( Opcode.CLEAR_SCENE );		
		// To Byte codes this Children.
		ToByteCodeInternal( code );
	}

}
