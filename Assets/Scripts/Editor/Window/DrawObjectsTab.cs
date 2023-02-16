//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.

using UnityEngine;
using UnityEditor;
using System.Collections;

[ System.Serializable ]
public class DrawObjectsTab  {		
	static public int m_SelectedObjTpID = 0;
	static public string[] m_ObjectTypeItems = { "Layer" , "Text"  };
	
	static public int m_SelectedCharaID = 0;
	static public string m_CharaName = "";
	static public string[] m_CharaMenuItems = {  "BG" , "1 Layer" , "2 Layers" , "3 Layers"  };
	static public Texture2D m_CharaTex1;
	static public Texture2D m_CharaTex2;
	static public Texture2D m_CharaTex3;
	static public string m_Layer1Name = "layer1";
	static public string m_Layer2Name = "layer2";
	static public string m_Layer3Name = "layer3";

	static public Texture2D m_TextBoxTex;
	static public float m_Width = 300f;
	static public float m_Height = 400f;
	
	static public GameObject m_ParentObject;
	
	[ MenuItem( "GameObject/ViNo/Object/BG" ) ]
	static public void CreateAEmptyBG( ){
		m_ParentObject = Selection.activeGameObject;
		CreateBG( "BG" , m_ParentObject  );
	}
	
	[ MenuItem( "GameObject/ViNo/Object/1 Layer Character" ) ]
	static public void Create1Layer( ){
		m_ParentObject = Selection.activeGameObject;
		Create1Layer( "1 Layer" , m_ParentObject  );
	}

	[ MenuItem( "GameObject/ViNo/Object/2 Layer Character" ) ]
	static public void Create2Layer( ){
		m_ParentObject = Selection.activeGameObject;
		Create2Layer( "2 Layer" , m_ParentObject  );
	}

	[ MenuItem( "GameObject/ViNo/Object/3 Layer Character" ) ]
	static public void Create3Layer( ){
		m_ParentObject = Selection.activeGameObject;
		Create3Layer( "3 Layer" , m_ParentObject  );
	}
	
	[ MenuItem( "GameObject/ViNo/Object/TextBox" ) ]
	static public void CreateTextBox( ){
		CreateTextBox( "TextBox" );
	}
	
	[ MenuItem( "GameObject/ViNo/Util/GUISelectionsCtrl" ) ]
	static public void CreateGUISelectionsCtrl( ){
		string path =  ViNoToolUtil.GetAssetDataPath() + "Templates/Utility/GUISelectionsCtrl.prefab";	
		string parentName = "";
		if( Selection.activeGameObject != null ){
			parentName = Selection.activeGameObject.name;
		}
		ViNoToolbar.ImportExampleCharacter( path , parentName );
	}

	[ MenuItem( "GameObject/ViNo/Util/TextButtonSelectionsCtrl" ) ]
	static public void CreateTextButtonSelectionsCtrl( ){
		string path =  ViNoToolUtil.GetAssetDataPath() + "Templates/Utility/TextButtonSelectionsCtrl.prefab";	
		string parentName = "";
		if( Selection.activeGameObject != null ){
			parentName = Selection.activeGameObject.name;
		}
		ViNoToolbar.ImportExampleCharacter( path , parentName );
	}

	[ MenuItem( "GameObject/ViNo/Object/SimpleButton" ) ]
	static public void CreateSimpleButton( ){
		string path =  ViNoToolUtil.GetAssetDataPath() + "Templates/Objects/UI/Button.prefab";	
		string parentName = "";
		if( Selection.activeGameObject != null ){
			parentName = Selection.activeGameObject.name;
		}
		ViNoToolbar.ImportExampleCharacter( path , parentName );
	}

	[ MenuItem( "GameObject/ViNo/Util/SceneManager" ) ]
	static public void CreateSceneManager( ){
		string path =  ViNoToolUtil.GetAssetDataPath() + "Templates/Utility/ViNoSceneManager.prefab";	
		string parentName = "";
		if( Selection.activeGameObject != null ){
			parentName = Selection.activeGameObject.name;
		}
		ViNoToolbar.ImportExampleCharacter( path , parentName );
	}
	
	[ MenuItem( "GameObject/ViNo/Util/ViNoSoundPlayer" ) ]
	static public void CreateViNoSoundPlayer( ){
		string path =  ViNoToolUtil.GetAssetDataPath() + "Templates/Utility/ViNoSoundPlayer.prefab";	
		string parentName = "";
		if( Selection.activeGameObject != null ){
			parentName = Selection.activeGameObject.name;
		}
		ViNoToolbar.ImportExampleCharacter( path , parentName );
	}

	[ MenuItem( "GameObject/ViNo/Util/SimpleSoundPlayer" ) ]
	static public void CreateSimpleSoundPlayer( ){
		string path =  ViNoToolUtil.GetAssetDataPath() + "Templates/Utility/SimpleSoundPlayer.prefab";	
		string parentName = "";
		if( Selection.activeGameObject != null ){
			parentName = Selection.activeGameObject.name;
		}
		ViNoToolbar.ImportExampleCharacter( path , parentName );
	}
	
	[ MenuItem( "GameObject/ViNo/Util/EventManager" ) ]
	static public void CreateViNoEventManager( ){
		ViNoEventManager em = GameObject.FindObjectOfType( typeof( ViNoEventManager ) ) as ViNoEventManager;
		if( em == null ){
			GameObject evtObj = new GameObject( "ViNoEventManager" );
			em = evtObj.AddComponent<ViNoEventManager>();
		}
//		em.gameObject.AddComponent<ViNoDialogPartEnterListener>();	
	}	
	
	
	static public void CreateTextBox( string objName ){
		m_CharaName = objName;
		string origin = ViNoToolUtil.GetAssetDataPath() + "Templates/Objects/TextBox/TextBox.prefab";
		string parentName = "";
		if( Selection.activeGameObject != null ){
			parentName = Selection.activeGameObject.name;
		}		
		GameObject clone = ViNoToolbar.ImportExampleCharacter( 
				origin , parentName );
		clone.name = objName;											
	}
	
	/// <summary>
	/// Creates A simple layer.
	/// </summary>
	/// <returns>
	/// The A simple layer.
	/// </returns>
	/// <param name='srcPath'>
	/// Source path.
	/// </param>
	static private GameObject CreateASimpleLayer( string objName , string srcPath , GameObject parentObj ){					
		string pathWithoutExt = "Assets/" + objName + "/";
		GameObject clone = CreateCharaNode( objName , parentObj , srcPath , pathWithoutExt );				
		CreateCharaChild( clone , "layer1" , m_CharaTex1, pathWithoutExt , 1f );							
		
		EditorGUIUtility.PingObject( clone );
		
		return clone;
	}
	
	/// <summary>
	/// Creates the chara node.
	/// </summary>
	/// <returns>
	/// The chara node.
	/// </returns>
	/// <param name='originalPath'>
	/// Original path.
	/// </param>
	/// <param name='pathWithoutExt'>
	/// Path without ext.
	/// </param>
	static private GameObject CreateCharaNode(  string objName , GameObject parentObj , string originalPath ,  string pathWithoutExt ){
		// Create the Character Folder.
		AssetDatabase.CreateFolder( "Assets" , objName );
		
		string parentName = "";
		if( parentObj != null ){
			parentName = parentObj.name;			
		}
		GameObject clone = ViNoToolbar.ImportExampleCharacter( 
				originalPath , parentName );
		clone.name = objName;									
		return clone;
	}
	
	/// <summary>
	/// Creates the chara child.
	/// </summary>
	/// <returns>
	/// The chara child.
	/// </returns>
	/// <param name='clone'>
	/// Clone.
	/// </param>
	/// <param name='childName'>
	/// Child name.
	/// </param>
	/// <param name='tex'>
	/// Tex.
	/// </param>
	/// <param name='pathWithoutExt'>
	/// Path without ext.
	/// </param>
	/// <param name='alpha'>
	/// Alpha.
	/// </param>
	static private GameObject  CreateCharaChild( GameObject clone , string childName , Texture2D tex , string pathWithoutExt , float alpha ){
		Transform body = clone.transform.Find( childName );
		ColorPanel panel = body.GetComponent<ColorPanel>();
		if( panel != null ){
			panel.alpha = alpha;
		}
		
		Material material = new Material (Shader.Find( "Unlit/UnlitAlphaWithFade" ));
		AssetDatabase.CreateAsset(material , pathWithoutExt + childName + ".mat");					
		
		body.GetComponent<Renderer>().sharedMaterial = material;
		body.GetComponent<Renderer>().sharedMaterial.mainTexture = tex;
				
		if( tex != null ){
			body.localScale = new Vector3( tex.width , tex.height , 1f );
		}	
		return body.gameObject;
	}	
	
	/// <summary>
	/// Draws the layer texture field.
	/// </summary>
	/// <param name='label'>
	/// Label.
	/// </param>
	/// <param name='tex'>
	/// Tex.
	/// </param>
	static public void DrawLayerTextureField( string label , ref Texture2D tex ){
		EditorGUILayout.BeginVertical();
			
			tex = EditorGUILayout.ObjectField( tex , typeof( Texture2D ) , false , GUILayout.Width( 100f ) , GUILayout.Height( 100f ) ) as Texture2D;
			EditorGUILayout.LabelField( label , GUILayout.Width( 100f ) );
			
		EditorGUILayout.EndVertical();		
	}
				
	/// <summary>
	/// Draw the specified toolBox.
	/// </summary>
	static public void Draw(){
		m_SelectedObjTpID = GUILayout.Toolbar( m_SelectedObjTpID , m_ObjectTypeItems , GUILayout.Height ( ViNoToolUtil.kToolbarHeight ) );	
		string objType = m_ObjectTypeItems[ m_SelectedObjTpID ];
						
		switch( objType ){
			case "Text":	
				if( m_TextBoxTex == null ){
					m_TextBoxTex = AssetDatabase.LoadAssetAtPath( "Assets/MenuIcon/btn053_04.png" , typeof( Texture2D ) ) as Texture2D;
				}			
				if( GUILayout.Button( "TextBox" , GUILayout.Width( 80f ) , GUILayout.Height( 40f ) ) ){//"TextBox"  ) ){		
					CreateTextBox( "TextBox" );					
				}
				break;
			
			case "Layer":						
			
				m_SelectedCharaID = GUILayout.Toolbar( m_SelectedCharaID , m_CharaMenuItems , GUILayout.Height ( ViNoToolUtil.kToolbarHeight ) );		
				string charaType = m_CharaMenuItems[ m_SelectedCharaID ];				
			
				m_ParentObject = EditorGUILayout.ObjectField( "parent" , m_ParentObject , typeof( GameObject)  , true ) as GameObject;
				if( m_ParentObject == null ){		
					m_ParentObject = GameObject.Find( "Panels" );
				}
			
				EditorGUILayout.BeginHorizontal();
			
					if( string.IsNullOrEmpty( m_CharaName )){ 
						m_CharaName = "A_New_" + objType;
					}
			
					EditorGUILayout.LabelField( "name" , GUILayout.Width( 100f )  );
					m_CharaName = EditorGUILayout.TextField( m_CharaName , GUILayout.Width( 200f ) );
						
					Color savedCol = GUI.color;
					GUI.color = Color.green;
					if( GUILayout.Button( "Create" ) ){
						switch( charaType ){
							case "BG":				CreateBG( m_CharaName		 , m_ParentObject );		break;	
							case "1 Layer":		Create1Layer( m_CharaName , m_ParentObject );		break;														
							case "2 Layers":		Create2Layer( m_CharaName , m_ParentObject );		break;						
							case "3 Layers":		Create3Layer( m_CharaName , m_ParentObject );		break;						
						}		
					}
	
			GUI.color = savedCol;			
			
			EditorGUILayout.EndHorizontal();
		
			switch( charaType ){		
				case "BG":
					EditorGUILayout.BeginHorizontal();
			
					DrawLayerTextureField( "Layer1" , ref m_CharaTex1 );				
//					DrawLayerTextureField( "Layer2" , ref m_CharaTex2 );
				
					GUILayout.Space( 200f );
				
					EditorGUILayout.EndHorizontal();					
					break;
				
				case "1 Layer":								
					DrawLayerTextureField( "Layer1( body )" , ref m_CharaTex1 );		
					break;
			
				case "2 Layers":
			
					EditorGUILayout.BeginHorizontal();
			
					DrawLayerTextureField( "Layer1( body )" , ref m_CharaTex1 );				
					DrawLayerTextureField( "Layer2( face )" , ref m_CharaTex2 );
				
					GUILayout.Space( 200f );
				
					EditorGUILayout.EndHorizontal();
								
					break;	
			
				case "3 Layers":
					EditorGUILayout.BeginHorizontal();
			
					DrawLayerTextureField( "Layer1( body )" , ref m_CharaTex1 );				
					DrawLayerTextureField( "Layer2( face1 )" , ref m_CharaTex2 );				
					DrawLayerTextureField( "Layer3( face2 )" , ref m_CharaTex3 );
							
					EditorGUILayout.EndHorizontal();
			
				break;
			}
			break;
		}
	}
	
	// --------------- CREATE PARTS TEST ! ------------------------------------.
#if false		
	[ MenuItem( "GameObject/ViNo/Object/Parts/BGTopAndSub" ) ]
	static public void CreateBGTop( ){
		m_ParentObject = Selection.activeGameObject;
		GameObject bg = CreateBGTop( "BG" , m_ParentObject  );
		GameObject layer1 = CreateBGSub( "layer1" , m_ParentObject  );
		layer1.transform.parent = bg.transform;
	}
	
	static public GameObject CreateBGTop( string objName , GameObject parentObj ){
		string origin = ViNoToolUtil.GetAssetDataPath() + "Templates/Objects/Parts/BGTop.prefab";					
		string pathWithoutExt = "Assets/" + objName + "/";					
		GameObject clone = CreateCharaNode( objName , parentObj , origin ,  pathWithoutExt );				
		EditorGUIUtility.PingObject( clone );				
//		string path = pathWithoutExt + objName + ".prefab";
//		ViNoEditorUtil.CreatePrefab( clone , path );
		return clone;
	}

	static public GameObject CreateBGSub( string objName , GameObject parentObj ){
		string origin = ViNoToolUtil.GetAssetDataPath() + "Templates/Objects/Parts/bg_sublayer.prefab";					
		string pathWithoutExt = "Assets/" + objName + "/";					
		GameObject clone = CreateCharaNode( objName , parentObj , origin ,  pathWithoutExt );				
		EditorGUIUtility.PingObject( clone );				
//		string path = pathWithoutExt + objName + ".prefab";
//		ViNoEditorUtil.CreatePrefab( clone , path );
		return clone;
	}	
#endif	
	// -----------------------------------------------------------------------------------.
	
	/// <summary>
	/// Creates the B.
	/// </summary>
	/// <returns>
	/// return the layer root Object.
	/// </returns>
	static public GameObject CreateBG( string objName , GameObject parentObj ){
		string origin = ViNoToolUtil.GetAssetDataPath() + "Templates/Objects/BG/SimpleBG.prefab";					
		string pathWithoutExt = "Assets/" + objName + "/";					
		GameObject clone = CreateCharaNode( objName , parentObj , origin ,  pathWithoutExt );				
		EditorGUIUtility.PingObject( clone );				
		float alpha = 1f;
		CreateCharaChild( clone , "layer1" , m_CharaTex1, pathWithoutExt , alpha );							
#if false		
		alpha = 0f;
		CreateCharaChild( clone , "layer2" , m_CharaTex2, pathWithoutExt , alpha );												
#endif		
		string path = pathWithoutExt + objName + ".prefab";
		ViNoEditorUtil.CreatePrefab( clone , path );
		return clone;
	}
	
	/// <summary>
	/// Create1s the layer.
	/// </summary>
	/// <returns>
	/// return the layer root Object.
	/// </returns>
	static public GameObject Create1Layer( string objName , GameObject parentObj ){
		string origin = ViNoToolUtil.GetAssetDataPath() + "Templates/Objects/SimpleCharacter/SimpleCharacter.prefab";
		string pathWithoutExt = "Assets/" + objName + "/";					
		GameObject layer = CreateASimpleLayer( objName ,  origin , parentObj );							
		string path = pathWithoutExt + objName + ".prefab";
		ViNoEditorUtil.CreatePrefab( layer , path );
		return layer;
	}
	
	/// <summary>
	/// Create2s the layer.
	/// </summary>
	/// <returns>
	/// return the layer root Object.
	/// </returns>
	static public GameObject Create2Layer( string objName , GameObject parentObj ){
		string origin = "Assets/" + ViNoToolUtil.kAssetName + "/Templates/Objects/SimpleCharacter2/SimpleCharacter2.prefab";
		string pathWithoutExt = "Assets/" + objName + "/";					
		GameObject clone = CreateCharaNode( objName , parentObj ,  origin ,  pathWithoutExt );				
		float alpha = 1f;
		CreateCharaChild( clone , "layer1" , m_CharaTex1,  pathWithoutExt , alpha );							
		alpha = 0f;
		CreateCharaChild( clone , "layer2" , m_CharaTex2, pathWithoutExt , alpha );												
		EditorGUIUtility.PingObject( clone );	
		string path = pathWithoutExt + objName + ".prefab";
		
		ViNoEditorUtil.CreatePrefab( clone , path );
		return clone;
	}
	
	/// <summary>
	/// Create3s the layer.
	/// </summary>
	/// <returns>
	/// return the layer root Object.
	/// </returns>
	static public GameObject Create3Layer( string objName , GameObject parentObj ){
		string origin = "Assets/" + ViNoToolUtil.kAssetName + "/Templates/Objects/CrossFadeTypeCharacter/CrossFadeTypeCharacter.prefab";
		string pathWithoutExt = "Assets/" + objName + "/";					
		GameObject clone = CreateCharaNode( objName , parentObj ,  origin ,  pathWithoutExt );				
		float alpha = 1f;
		CreateCharaChild( clone , "layer1" ,  m_CharaTex1, pathWithoutExt , alpha );							
		alpha = 1f;
		CreateCharaChild( clone , "layer2" ,  m_CharaTex2, pathWithoutExt , alpha );												
		alpha = 0f;
		CreateCharaChild( clone , "layer3"  ,  m_CharaTex3, pathWithoutExt , alpha );																	
		EditorGUIUtility.PingObject( clone );	
		string path = pathWithoutExt + objName + ".prefab";

		ViNoEditorUtil.CreatePrefab( clone , path );
		
		return clone;		
	}
	
	static public void CreatePrefab( GameObject obj ,  string objName  ){
		string path = "Assets/" + objName + "/" + objName + ".prefab";
		ViNoEditorUtil.CreatePrefab( obj , path );		
	}
	
#if false	
	[ MenuItem( "GameObject/ViNo/Object/Mesh1" ) ]
	static public void CreateAMesh( ){
		GameObject meshObj = new GameObject("MESH1");
		MeshRenderer ren = meshObj.AddComponent<MeshRenderer>();
		MeshFilter mf = meshObj.AddComponent<MeshFilter>();				
		ViNoSceneUtil.CreateMesh( mf );		
	}

#endif


}
