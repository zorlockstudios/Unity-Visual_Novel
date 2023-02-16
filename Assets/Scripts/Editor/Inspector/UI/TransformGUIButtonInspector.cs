//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.

using UnityEngine;
using UnityEditor;
using System.Collections;

#if false
[ CustomEditor( typeof( TransformGUIButton ) )]
public class TransformGUIButtonInspector : Editor {

	public override void OnInspectorGUI(){
		TransformGUIButton btn = target as TransformGUIButton;		
		EditorGUILayout.BeginHorizontal();				
			EditorGUILayout.LabelField ( "Label" , GUILayout.Width( 55f )  );
			btn.label = EditorGUILayout.TextArea( btn.label );
		EditorGUILayout.EndHorizontal ();		
		EditorGUILayout.BeginHorizontal();		
			
			EditorGUILayout.LabelField ( "OnClick" , GUILayout.Width ( 50f ) );
		
			EditorGUILayout.BeginVertical();
				EditorGUILayout.LabelField ( " " );
				btn.onClick = EditorGUILayout.ObjectField( btn.onClick , typeof( GameObject ) , true , GUILayout.Width( 80f) ) as GameObject;
			EditorGUILayout.EndVertical();
		
			EditorGUILayout.LabelField ( "=>" , GUILayout.Width ( 20f)  );
		
			EditorGUILayout.BeginVertical();
				EditorGUILayout.LabelField ( "Msg" );
				btn.sendMessage = EditorGUILayout.TextField( btn.sendMessage  );
			EditorGUILayout.EndVertical();
		
		EditorGUILayout.EndHorizontal ();
	}
}
#endif