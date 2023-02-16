//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.

using UnityEngine;
using System.Collections;

public class ViNoToolUtil  {
	readonly static public float kToolbarHeight = 20f;
	readonly static public string kAssetName = "VisualNovelToolkit";	
	
	static public GameObject Add_IDNamedGameObject( string name , Transform parent ){
		GameObject obj = new GameObject( name );
		if( parent != null ){
			int childNum = parent.childCount;
			int id = childNum + 1;
			string rename = id.ToString() + "_" + name;
			obj.name = rename;
		}
		obj.transform.parent = parent;		
		return obj;
	}
	
	static public T AddViNodeGameObject<T>( string name , Transform parent ) where T : Component{
		GameObject nodeObj = Add_IDNamedGameObject( name , parent );	
		T t = nodeObj.AddComponent<T>();
		return t;
	}
		
	static public GameObject GetViNoObject( ){
#if false		
		GameObject vinoObj = GameObject.Find( "ViNo" );	
		if( vinoObj == null ){
			vinoObj = new GameObject( "ViNo" );	
			vinoObj.AddComponent<ViNo>();
		}		
		return vinoObj;
#else		
		ViNo vino = GameObject.FindObjectOfType( typeof( ViNo ) ) as ViNo;		
		if( vino == null ){
			GameObject vinoObj = new GameObject( "ViNo" );	
			vinoObj.AddComponent<ViNo>();
			return vinoObj;
		}		
		else{
			return vino.gameObject;
		}		
#endif		
	}
	
	/// <summary>
	/// Creates A new scenario.
	/// </summary>
	/// <returns>
	/// return the Created GameObject Attached a New Scenario Node.
	/// </returns>
	static public GameObject CreateANewScenario( string scenarioName , bool startAndPlay ){
//		GameObject vinoObj = GetViNoObject();						
		GameObject scenarioObj = GameObject.Find( scenarioName );
		if( scenarioObj == null ){
			scenarioObj = new GameObject( scenarioName );
//			scenarioObj.transform.parent = vinoObj.transform;
			scenarioObj.AddComponent<ScenarioNode>();			
		}		
		
		// if a New ScenarioNode is Played at Start , Other ScenarioNode objects of all the are Not Played at Start.
		if( startAndPlay ){
			ScenarioNode[] snodes = GameObject.FindObjectsOfType( typeof( ScenarioNode ) ) as ScenarioNode[];
			for( int i=0;i<snodes.Length;i++){
				snodes[ i ].m_PlayAtStart = false;	
			}
		}
		
		GameObject startObj = new GameObject( "START" );
		ViNode startNode = startObj.AddComponent<ViNode>();		
		startObj.transform.parent = scenarioObj.transform;
		
		ScenarioNode scenarioNode = scenarioObj.GetComponent<ScenarioNode>();
		scenarioNode.startNode = startNode;
		scenarioNode.m_PlayAtStart = startAndPlay;
		return scenarioObj;
	}
	
	static public DialogPartNode AddDialogPartNode( Transform parent ){
		DialogPartNode dlgNode = ViNoToolUtil.AddViNodeGameObject<DialogPartNode>( "Dialog" , parent );//selection.transform );				
		GameObject textBoxObj = GameObject.Find( "TextBox" );
		if( textBoxObj != null ){
			dlgNode.m_ViNoTextBox = textBoxObj.GetComponent<ViNoTextBox>();
			Transform nameTextTra = textBoxObj.transform.Find( "TextBox_Name" );
			if( nameTextTra != null ){
				dlgNode.m_NameTextBox = nameTextTra.GetComponent<ViNoTextBox>();				
			}
		}		
		return dlgNode;
	}
		
	static public string GetAssetDataPath( ){
		return "Assets/" + kAssetName + "/"; 	
	}
				
}
