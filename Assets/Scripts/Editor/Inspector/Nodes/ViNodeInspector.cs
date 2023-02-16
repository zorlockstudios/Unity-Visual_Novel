//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.

using UnityEditor;
using UnityEngine;
using System.Collections;

[ CustomEditor( typeof( ViNode) ) ] 
public class ViNodeInspector : Editor {
		
	public GameObject m_CurrSelectedObj;
	public bool m_Show = true;
	
	static public bool[] orderBools;

	void OnEnable(){
		int childNodeNum = ( target as ViNode ).gameObject.transform.childCount;
		orderBools = new bool[ childNodeNum ];
	}

	public override void OnInspectorGUI(){		
		ViNode node = target as ViNode;
		
		NodeGUI.OnGUI_ViNode( node , true , true );			
	}
	
}

