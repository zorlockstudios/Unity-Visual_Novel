//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[ CustomEditor( typeof( SelectionsNode ) ) ] 
public class SelectionsNodeInspector : Editor {
	
	TextMesh[] selectionTextMeshes;

	private ISelectionsCtrl m_SelectCtrlInstance;
	private ISelectionsCtrl selectCtrl{
		get{
			if( m_SelectCtrlInstance == null ){
				m_SelectCtrlInstance = GameObject.FindObjectOfType( typeof(ISelectionsCtrl) ) as ISelectionsCtrl;
				if( m_SelectCtrlInstance == null ){
					DrawObjectsTab.CreateGUISelectionsCtrl();
					m_SelectCtrlInstance = GameObject.FindObjectOfType( typeof(ISelectionsCtrl) ) as ISelectionsCtrl;
				}
			}
			if( selectionTextMeshes == null ){
				selectionTextMeshes = m_SelectCtrlInstance.GetComponentsInChildren<TextMesh>();
			}
			return m_SelectCtrlInstance;
		}
	}

	public override void OnInspectorGUI(){		
//		SelectionsNode node = target as SelectionsNode;					
				
		Color savedCol = GUI.backgroundColor;
		GUI.backgroundColor = Color.cyan;

#if true	
		DrawDefaultInspector();
#else		
		for( int i=0;i<node.units.Length;i++){
			NodeGUI.OnGUISelectionUnit( node.units[ i ] );
		}
#endif				
		GUI.backgroundColor = savedCol;			
		
#if false		
		if( GUI.changed ){			
			ISelectionsCtrl selCtrl = selectCtrl;
			if( selectionTextMeshes != null ){				
				for(int i=0;i<node.units.Length;i++){
					selectionTextMeshes[ i ].text =  node.units[ i ].text;
					selectionTextMeshes[ i ].transform.parent.localPosition = node.position;
					selectionTextMeshes[ i ].transform.parent.localPosition = node.position;
				}
			}
		}
#endif

	}
	
}
