//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
using UnityEngine;
using UnityEditor;
using System.Collections;

[ CustomEditor( typeof( FlagTable ) )] 
public class FlagTableInspector : Editor {
	
	public bool m_ShowBoolean	= true;
	public bool m_ShowString	= true;
	public bool m_ShowFloat		= true;

	public override void OnInspectorGUI(){
		FlagTable flagTable = target as FlagTable;

//		m_ShowBoolean = EditorGUILayout.BeginToggleGroup( "Edit" , m_ShowBoolean );
		if( EditorGUIUtility.isProSkin ){
			GUICommon.DrawLineSpace( 5f , 15f );
		}
		else{
			GUICommon.DrawLineSpace( 5f , 5f );			
		}
		m_ShowBoolean = EditorGUILayout.Foldout( m_ShowBoolean , "Passed Labels" );
		if( m_ShowBoolean ){
			if(  flagTable.flags == null || flagTable.flags.Length == 0 ){ 
				flagTable.flags = new FlagTable.FlagUnit[ 1 ];
				flagTable.flags[ 0 ] = new FlagTable.FlagUnit();
			}

			if(  flagTable.stringValues == null || flagTable.stringValues.Length == 0 ){ 
				flagTable.stringValues = new FlagTable.VarUnit<string>[ 1 ];
				flagTable.stringValues[ 0 ] = new FlagTable.VarUnit<string>();
			}

			if(  flagTable.floatValues == null || flagTable.floatValues.Length == 0 ){ 
				flagTable.floatValues = new FlagTable.VarUnit<float>[ 1 ];
				flagTable.floatValues[ 0 ] = new FlagTable.VarUnit<float>();
			}
							
			EditorGUILayout.BeginHorizontal();			

				GUILayout.Space( 15f );		
				EditorGUILayout.LabelField( "FlagName"  );			
				EditorGUILayout.LabelField( "State" , GUILayout.Width( 34f ) );			
			
			EditorGUILayout.EndHorizontal();
			
			for( int i=0;i<flagTable.flags.Length;i++){
				EditorGUILayout.BeginHorizontal();	
					GUILayout.Space( 15f );		
	//				EditorGUILayout.LabelField( "FlagState" , GUILayout.Width( 80f )  ); 
					flagTable.flags[ i ].m_FlagName = EditorGUILayout.TextField( flagTable.flags[ i ].m_FlagName );			
					flagTable.flags[ i ].m_IsFlagOn = EditorGUILayout.Toggle( flagTable.flags[ i ].m_IsFlagOn , GUILayout.Width( 34f ));			
	//			EditorGUILayout.EndHorizontal();
	//			EditorGUILayout.BeginHorizontal();			

					if( GUILayout.Button( "+" ) ){
						ArrayUtility.Insert<FlagTable.FlagUnit>( ref flagTable.flags , i + 1 , new FlagTable.FlagUnit() );
					}
					if( GUILayout.Button( "-" ) ){
						ArrayUtility.RemoveAt<FlagTable.FlagUnit>( ref flagTable.flags , i );
					}

				EditorGUILayout.EndHorizontal();				
			}

/*			EditorGUILayout.BeginHorizontal();
			
			if(  flagTable.flags != null && flagTable.flags.Length > 0  ) {		
				if( GUILayout.Button( "+" ) ){
					ArrayUtility.Add<FlagTable.FlagUnit>( ref flagTable.flags , new FlagTable.FlagUnit() );
				}
				
				if( GUILayout.Button( "-" ) ){
					int lastIndex = flagTable.flags.Length - 1;
					FlagTable.FlagUnit lastOne = flagTable.flags[ lastIndex ];
					ArrayUtility.Remove<FlagTable.FlagUnit>( ref flagTable.flags , lastOne );
				}
			}		
			
			EditorGUILayout.EndHorizontal(); */
		}

#if false
		if( EditorGUIUtility.isProSkin ){
			GUICommon.DrawLineSpace( 5f , 15f );
		}
		else{
			GUICommon.DrawLineSpace( 5f , 5f );			
		}
		m_ShowString = EditorGUILayout.Foldout( m_ShowString , "Strings" );
		if( m_ShowString ){
			EditorGUILayout.BeginHorizontal();			
			
				GUILayout.Space( 15f );		
				EditorGUILayout.LabelField( "VariableName" , GUILayout.Width( 100f )  );			
				EditorGUILayout.LabelField( "StringValues" );			
			
			EditorGUILayout.EndHorizontal();

			for( int i=0;i<flagTable.stringValues.Length;i++){
				EditorGUILayout.BeginHorizontal();			
					GUILayout.Space( 15f );	
					flagTable.stringValues[ i ].m_FlagName = EditorGUILayout.TextField( flagTable.stringValues[ i ].m_FlagName , GUILayout.Width( 100f ) );			
					flagTable.stringValues[ i ].theValue = EditorGUILayout.TextField( flagTable.stringValues[ i ].theValue );
					if( GUILayout.Button( "+" ) ){
						ArrayUtility.Insert<FlagTable.VarUnit<string>>( ref flagTable.stringValues , i + 1 , new FlagTable.VarUnit<string>() );
					}
					if( GUILayout.Button( "-" ) ){
						ArrayUtility.RemoveAt<FlagTable.VarUnit<string>>( ref flagTable.stringValues , i );
					}
				EditorGUILayout.EndHorizontal();				
			}
		}
#endif		

// This is Test. 
#if false

		GUICommon.DrawLineSpace( 10f , 5f );
		m_ShowFloat = EditorGUILayout.Foldout( m_ShowFloat , "Floats" );
		if( m_ShowFloat ){
			EditorGUILayout.BeginHorizontal();			
			
				GUILayout.Space( 15f );		
				EditorGUILayout.LabelField( "FloatValues" , GUILayout.Width( 100f ) );			
				EditorGUILayout.LabelField( "VariableName"  );			
			
			EditorGUILayout.EndHorizontal();

			for( int i=0;i<flagTable.floatValues.Length;i++){
				EditorGUILayout.BeginHorizontal();			
					GUILayout.Space( 15f );	
					flagTable.floatValues[ i ].floatValue = EditorGUILayout.FloatField( flagTable.floatValues[ i ].floatValue , GUILayout.Width( 65f ));			
					flagTable.floatValues[ i ].m_FlagName = EditorGUILayout.TextField( flagTable.floatValues[ i ].m_FlagName );			
					if( GUILayout.Button( "+" ) ){
						ArrayUtility.Insert<FlagTable.VarFloatUnit>( ref flagTable.floatValues , i + 1 , new FlagTable.VarFloatUnit() );
					}
					if( GUILayout.Button( "-" ) ){
						ArrayUtility.RemoveAt<FlagTable.VarFloatUnit>( ref flagTable.floatValues , i );
					}
				EditorGUILayout.EndHorizontal();				
			}

			EditorGUILayout.BeginHorizontal();
			
/*			if(  flagTable.floatValues != null && flagTable.floatValues.Length > 0  ) {		
				if( GUILayout.Button( "+" ) ){
					ArrayUtility.Add<FlagTable.VarFloatUnit>( ref flagTable.floatValues , new FlagTable.VarFloatUnit() );
				}
				
				if( GUILayout.Button( "-" ) ){
					int lastIndex = flagTable.floatValues.Length - 1;
					FlagTable.VarFloatUnit lastOne = flagTable.floatValues[ lastIndex ];
					ArrayUtility.Remove<FlagTable.VarFloatUnit>( ref flagTable.floatValues , lastOne );
				}
			}		
//*/			
			EditorGUILayout.EndHorizontal();
		}
#endif				

			if( GUILayout.Button ( "Clear Flags" ) ){
				bool yes = EditorUtility.DisplayDialog( " ! " , "Are you sure you want to ClearFlags?" , "yes" , "no" );
				if( yes) {
					flagTable.ClearFlags();
				}
			}
			
//		EditorGUILayout.EndToggleGroup();		

		if( GUI.changed ){
			EditorUtility.SetDirty( target );	
		}		
	}
}
