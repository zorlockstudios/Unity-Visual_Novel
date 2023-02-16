//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
using UnityEngine;
using System.Collections.Generic;

public class ViNoSceneSaveUtil {
		
	static public void LoadSceneXMLFromTextAsset( TextAsset txt ){
		ViNoGameSaveLoad.LoadSceneFromXmlString(  txt.text );		
	}
	
	static public void LoadSceneXMLFromTextAssetPath( string path ){
		TextAsset txt = Resources.Load( path ,  typeof( TextAsset ) ) as TextAsset;
		if( txt != null ){		
			LoadSceneXMLFromTextAsset( txt );			
		}
		else{
			ViNoDebugger.LogError( "SaveInfo" , "LOAD_SCENE_XML : Resources.Load Error! " );	
		}					
	}
	
	static public void SaveDataRecursively( GameObject savedObj , ref List<SceneData.SceneNodeData> nodelist , ref ISpriteFactory spriteFact ){
		SceneData.SceneNodeData data = new SceneData.SceneNodeData();
		nodelist.Add( data );
		spriteFact.SaveData( ref data , savedObj );					
		
		int count = savedObj.transform.childCount;
		if( count > 0 ){
			for(int i=0;i<count;i++){		
				GameObject childObj =  savedObj.transform.GetChild( i ).gameObject;
				SaveDataRecursively( childObj ,ref nodelist , ref spriteFact );
			}									
		}		
	}
	
	static public void SavePanelChildren( GameObject savedObj , ref List<SceneData.SceneNodeData> nodelist , ref ISpriteFactory spriteFact ){
		nodelist.Clear();				
		
		// savedObj <== named "SavedLayer" GameObject and this is not serialized.
		int count = savedObj.transform.childCount;
		if( count > 0 ){
			for(int i=0;i<count;i++){		
				GameObject childObj =  savedObj.transform.GetChild( i ).gameObject;
				SaveDataRecursively( childObj ,ref nodelist , ref spriteFact );
			}									
		}		
		
// TEST . TEXT BOX POSITION SAVE.
/*						
		SceneData.SceneNodeData dat = new SceneData.SceneNodeData();
		GameObject _TEXTBOX = GameObject.Find( "TextBox" );			
		if( _TEXTBOX != null ){
			dat.nodeType = SceneNodeType.NODE;		
			nodelist.Add( dat );					
			spriteFact.SaveData( ref dat , _TEXTBOX );
		}	
		else{
			ViNoDebugger.LogError( "TextBox Not Found !" );	
		}
//*/		
	}

	static public void SaveViNoSceneNodes(  ref List<SceneData.SceneNodeData> nodelist , ISpriteFactory fact ){
		ViNoSceneNode[] sceneNodesData = GameObject.FindObjectsOfType( typeof( ViNoSceneNode ) ) as ViNoSceneNode[];
		if( sceneNodesData != null ){
			for( int i=0;i<sceneNodesData.Length;i++){
				GameObject sceneNode = sceneNodesData[ i ].gameObject;			
				if( sceneNodesData[ i ].singleNodeData != null ){
					SceneData.SceneNodeData data =  sceneNodesData[ i ].singleNodeData;									
					nodelist.Add( data );				
					fact.SaveData( ref data , sceneNode );					
				}
/*				
				else{
					if( sceneNodesData[ i ].scrNodeData.nodesData != null ){
						int len = sceneNodesData[ i ].scrNodeData.nodesData.Length;				
						for( int k=0;k<len;k++){
							SceneData.SceneNodeData data =  sceneNodesData[ i ].scrNodeData.nodesData[ k ];					
							nodelist.Add( data );							
							GameObject obj = GameObject.Find( data.name );//sceneNode.gameObject.transform.FindChild( data.name ).gameObject;					
							fact.SaveData( ref data , obj );
						}
					}
				}
//*/				
			}
		}		
	}
						
}
