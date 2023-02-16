//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
using UnityEngine;
using System.Collections;

namespace ViNoToolkit{

	[System.Serializable]
	public class ActorInfo : ScriptableObject {
		[System.Serializable]
		public class ActorState{
			public string stateName = "Normal";
			[Range( 0f , 200f ) ]
			public float sizeInPercent = 100f;
			public SceneData.SceneNodeData[] dataArray;				
		}

		public string actorName = "actor1";
		public Color textColor;	
		public ActorState baseActorState;
		public ActorState[] actorStates;

		[HideInInspector] public Texture2D portrait;	
	}

}