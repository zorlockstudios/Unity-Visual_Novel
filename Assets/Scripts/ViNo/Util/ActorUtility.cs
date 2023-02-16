using UnityEngine;
using System.Collections;

namespace ViNoToolkit{

	static public class ActorUtility {

        static public void SetUpActorInfo( ActorInfo actor , string actorName , float sizeInPercent , Color nameTextColor , Texture2D[] textures ){
            actor.actorName = actorName;
            actor.textColor = nameTextColor;

        	actor.baseActorState = new ActorInfo.ActorState();
        	actor.baseActorState.sizeInPercent = sizeInPercent;
            actor.baseActorState.dataArray = new SceneData.SceneNodeData[ textures.Length ];
            for( int i =0;i<textures.Length;i++){
                actor.baseActorState.dataArray[ i ] = new SceneData.SceneNodeData();
            	actor.baseActorState.dataArray[ i ].name = actorName + "_" + i.ToString();
                actor.baseActorState.dataArray[ i ].parentname = actorName;
                actor.baseActorState.dataArray[ i ].parentname = actorName;            
                actor.baseActorState.dataArray[ i ].alpha = 1f;
                actor.baseActorState.dataArray[ i ].posZ = - i;
                actor.baseActorState.dataArray[ i ].texturePath = textures[ i ].name;
            }
        }

        static public void SetUpActorInfo( ActorInfo actor , string actorName , float sizeInPercent , Color nameTextColor , string[] texturePaths ){
            actor.actorName = actorName;
            actor.textColor = nameTextColor;
        	actor.baseActorState = new ActorInfo.ActorState();
        	actor.baseActorState.sizeInPercent = sizeInPercent;
            actor.baseActorState.dataArray = new SceneData.SceneNodeData[ texturePaths.Length ];
            for( int i =0;i<texturePaths.Length;i++){
                actor.baseActorState.dataArray[ i ] = new SceneData.SceneNodeData();
            	actor.baseActorState.dataArray[ i ].name = actorName + "_" + i.ToString();
                actor.baseActorState.dataArray[ i ].parentname = actorName;
                actor.baseActorState.dataArray[ i ].parentname = actorName;            
                actor.baseActorState.dataArray[ i ].alpha = 1f;
                actor.baseActorState.dataArray[ i ].posZ = - i;
                actor.baseActorState.dataArray[ i ].texturePath = texturePaths[ i ];
            }
        }
			
		static public SceneEvent.ActorPosition GetActorPosition( string position ){
			position = position.ToLower();
			SceneEvent.ActorPosition pos = SceneEvent.ActorPosition.center;		
			switch( position ){
				case "center":			pos = SceneEvent.ActorPosition.center;				break;
				case "left":			pos = SceneEvent.ActorPosition.left;				break;
				case "right":			pos = SceneEvent.ActorPosition.right;				break;
				case "middle_left":		pos = SceneEvent.ActorPosition.middle_left;		break;
				case "middle_right":	pos = SceneEvent.ActorPosition.middle_right;		break;
			}
			return pos;
		}								

	}

}