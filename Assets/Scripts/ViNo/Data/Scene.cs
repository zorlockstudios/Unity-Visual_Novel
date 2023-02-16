using UnityEngine;
using System.Collections;

[System.Serializable ]
public class Scene : ScriptableObject {
	public SceneData.SceneNodeData[] sceneNodesData;

	public void Apply(){
		SceneCreator.Create( this );
	}

}
