//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
using UnityEngine;
using System.Collections;

namespace ViNoToolkit{

	static public class ViNoSceneUtil {

		static public void SetUpSceneInfo( Scene scene , string sceneName , string texturePath ){
        	scene.sceneNodesData = new SceneData.SceneNodeData[ 2 ];
            scene.sceneNodesData[ 0 ] = new SceneData.SceneNodeData();
            scene.sceneNodesData[ 1 ] = new SceneData.SceneNodeData();

        	scene.sceneNodesData[ 0 ].name = "BG";
            scene.sceneNodesData[ 0 ].parentname = "ADVScene";

            scene.sceneNodesData[ 1 ].name = "layer1";
            scene.sceneNodesData[ 1 ].posZ = 2;
            
            scene.sceneNodesData[ 1 ].parentname = "BG";
            scene.sceneNodesData[ 1 ].alpha = 1f;
            scene.sceneNodesData[ 1 ].texturePath = texturePath;//texture.name;
		}
//*/
		static public Mesh CreateMesh( MeshFilter mf ){     
		    mf.mesh = new Mesh();
			Mesh mesh = mf.sharedMesh;
			mesh.name = "ViNoSimplePlaneMesh";
			mesh.vertices = new Vector3[] { new Vector3(-0.5f, -0.5f, 0f), new Vector3(+0.5f, -0.5f, 0f), new Vector3(-0.5f, +0.5f, 0f), new Vector3(+0.5f, +0.5f, 0f) };
	        mesh.normals = new Vector3[] { new Vector3(0f, 0f, 1f), new Vector3(0f, 0f, 1f), new Vector3(0f, 0f, 1f), new Vector3(0f, 0f, 1f) };					
	     	mesh.uv = new Vector2[] {
				new Vector2( 0f , 0f ) , new Vector2( 1f , 0f ) , 
				new Vector2( 0f , 1f ) , new Vector2( 1f , 1f ) , 		
			};					
	        mesh.triangles = new int[] { 0,2,1,1,2,3 };			
			return mesh;
		}

	}

}