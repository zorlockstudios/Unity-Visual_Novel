//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.

using UnityEngine;
using UnityEditor;
using System.Collections;

public class ViNoToolbar : EditorWindow {
	static public int m_Selected = 0;
	static public string[] m_MenuItems = {  "Scenario" , "Nodes" , "Templates" };
		
	static public ScenarioNode m_CurrScenarioNode;
	static public string m_ScenarioName = "";
	static public bool m_StartAndPlay = true;
	static public GameObject m_AddedObject;
	
	public GameObject m_CurrSelected;
		
	 [MenuItem ("Window/ViNo/Toolbar")]
    static void Init() {		
      	EditorWindow.GetWindow (typeof (ViNoToolbar));		
    }		
		
	void OnSelectionChange( ){
		Repaint();
	}

	void OnEnable(){   
/*		ScenarioNode[] scenarios = Resources.FindObjectsOfTypeAll( typeof(ScenarioNode) ) as ScenarioNode[];
		for( int i=0;i<scenarios.Length;i++){
			Debug.Log( "scenario : " + scenarios[ i ].name );
		}
//*/		
	}

	void OnGUI(){						
		float space = ViNoToolUtil.kToolbarHeight + 5f;
		GUICommon.DrawLineSpace( 5f , space );		
		
		m_Selected = GUILayout.Toolbar( m_Selected , m_MenuItems , GUILayout.Height ( ViNoToolUtil.kToolbarHeight ));		
		string selectedStr = m_MenuItems[ m_Selected ];
		switch( selectedStr ){
			case "Scenario":	DrawScenarioTab.Draw( this );		break;			
			case "Nodes":		DrawViNodesTab.Draw();				break;
//			case "Objects":		DrawObjectsTab.Draw();				break;
			case "Samples":		DrawSamplesTab.Draw();				break;			
			case "Templates":	DrawTemplatesTab.Draw();			break;
//			case "Animation":	DrawAnimationTab();					break;
		}
	}
	
	static public void SlideSelectionObjects( float toX , float fromX , float duration ){
			float localY = Selection.activeGameObject.transform.localPosition.y;								
			float localZ = Selection.activeGameObject.transform.localPosition.z;	
	
			AnimationNode animNode = GetAnimationNodeTempl( AnimationType.MOVE_TO , duration );
			animNode.amountX = toX;
			animNode.amountY = localY;
			animNode.amountZ = localZ;
	
			animNode.fromAmountX = fromX;// = new Vector3( -700f , 0f , z );
			animNode.fromAmountY = localY;
			animNode.fromAmountZ = localZ;
	
			animNode.toggleFromAmount = true;
			animNode.Preview();
	}
	
	static public AnimationNode GetAnimationNodeTempl(  AnimationType tp , float duration  ){
		string path =  ViNoToolUtil.GetAssetDataPath() + "Templates/AnimationNode.prefab";
		GameObject animObj = AssetDatabase.LoadAssetAtPath( path , typeof( GameObject) ) as GameObject;				
		AnimationNode animNode = animObj.GetComponent<AnimationNode>();				
		animNode.animationType = tp;
		animNode.animTarget = Selection.activeGameObject;
		animNode.duration = duration;					
		return animNode;		
	}
		
	static public GameObject ImportExampleCharacter( string path , string parentName ){
		GameObject obj = AssetDatabase.LoadAssetAtPath( path , typeof( GameObject) ) as GameObject;
		
		GameObject clone = GameObject.Instantiate( obj ) as GameObject;
		ViNoGOExtensions.StripGameObjectName( clone , "(Clone)" , "" );
		
		Vector3 originalPos = clone.transform.localPosition;		
		Quaternion originalRot = clone.transform.localRotation;		
		Vector3 originalScale = clone.transform.localScale;		
		
		GameObject character = GameObject.Find ( parentName );
		if( character != null ){
			clone.transform.parent = character.transform;
			clone.transform.localPosition = originalPos;
			clone.transform.localRotation = originalRot;
			clone.transform.localScale = originalScale;
		}
		return clone;
	}
	
#if false
	public GameObject animationTarget;
	public float duration = 1000f;
	
	public float m_SlideInL_X = -100f;
	public float m_SlideInR_X = 100f;

	static public Texture2D m_FadeInIcon;
	static public Texture2D m_FadeOutIcon;
	static public Texture2D m_SlideInIcon;
	static public Texture2D m_SlideOutIcon;
	static public Texture2D m_TurnIcon;
	
	private void DrawAnimationTab( ){
//				animationTarget = EditorGUILayout.ObjectField( animationTarget , typeof( GameObject ) , true ) as GameObject;										
		if( Application.isPlaying ){
			if( Selection.activeGameObject == null ){				
				GUIStyle myStyle = new GUIStyle();					
				myStyle.fontSize = 20;
				GUILayout.Label( "Please Select a Target GameObject in Hierarchy", myStyle);					
//						ViNoEditorUtil.DrawWarningString("Please Select a Target GameObject in Hierarchy" );					
			}
			else{
				if( m_CurrSelected != Selection.activeGameObject ){
					m_CurrSelected = Selection.activeGameObject;
				}					
				GUI.enabled = true;					
			}	
			bool isSelectedHasPanel = false;
			if( Selection.activeGameObject != null ){
				ColorPanel panel = Selection.activeGameObject.GetComponent<ColorPanel>();
				int childNum = Selection.activeGameObject.transform.GetChildCount();
				isSelectedHasPanel = ( panel != null );					
				if( panel == null ){					
					ViNoEditorUtil.DrawWarningString("FadeIn and FadeOut : Target GameObject is required to be attached a ColorPanel Component" );									
					GUI.enabled = false;							
				}
				else{
					GUI.enabled = true;				
				}					
			}				
		}
		 else{
			GUI.enabled = false;	
			GUIStyle myStyle = new GUIStyle();					
			myStyle.fontSize = 30;
		
			EditorGUILayout.BeginHorizontal();
								
				GUILayout.Space( 10f );									
				GUILayout.Label( "PlayMode Only", myStyle);									
			
			EditorGUILayout.EndHorizontal();
		
			return;
//					ViNoEditorUtil.DrawWarningString("Please use Animation Tab in PlayMode." );
		}
	
			EditorGUILayout.BeginHorizontal();
	
			if( GUILayout.Button( m_FadeInIcon ,  GUILayout.Width( 80f ) , GUILayout.Height ( 50f ) ) ){
//					if( GUILayout.Button( "FadeIn" ,  GUILayout.Width( 80f ) , GUILayout.Height ( 50f ) ) ){				
//						AnimationNode animNode = ViNoToolUtil.AddViNodeGameObject<AnimationNode>( "FadeIn" , m_AddedObject.transform );
				GameObject animObj = new GameObject( "_Anim" );
				AnimationNode animNode = animObj.AddComponent<AnimationNode>();				
				animNode.animTarget = Selection.activeGameObject;
				animNode.animationType = AnimationType.FADE_PANEL;
				animNode.duration = duration;
				animNode.fadeTo = 1f;
				animNode.fadeFrom = 0f;
				animNode.toggleFromAmount = true;				
				animNode.Preview();
			}

			if( GUILayout.Button( m_FadeOutIcon ,  GUILayout.Width( 80f ) , GUILayout.Height ( 50f ) ) ){
//						AnimationNode animNode = ViNoToolUtil.AddViNodeGameObject<AnimationNode>( "FadeOut" , m_AddedObject.transform );
				GameObject animObj = new GameObject( "_Anim" );
				AnimationNode animNode = animObj.AddComponent<AnimationNode>();								
				animNode.animTarget = Selection.activeGameObject;
				animNode.animationType = AnimationType.FADE_PANEL;
				animNode.duration = duration;
				animNode.fadeTo = 0f;
				animNode.fadeFrom = 1f;
				animNode.toggleFromAmount = true;				
				animNode.Preview();				
			}
	
			if( Selection.activeGameObject != null && Application.isPlaying ){
				GUI.enabled = true;				
			}
	
			// Slide IN.
			if( GUILayout.Button( m_SlideInIcon , GUILayout.Width( 80f ) , GUILayout.Height ( 50f ) ) ){													
				float slideToX = m_SlideInL_X;
				float slideFromX = m_SlideInL_X - 700f;
			
				SlideSelectionObjects( slideToX , slideFromX , duration );
			}

			// Slide OUT.
			if( GUILayout.Button( m_SlideOutIcon , GUILayout.Width( 80f ) , GUILayout.Height ( 50f ) ) ){													
				float slideFromX = Selection.activeGameObject.transform.localPosition.x;
				SlideSelectionObjects( -700f , slideFromX , duration );
			}
			
			// Rotate Object.
			if( GUILayout.Button( m_TurnIcon , GUILayout.Width( 80f ) , GUILayout.Height ( 50f ) ) ){													
				AnimationNode animNode = GetAnimationNodeTempl( AnimationType.ROTATE_TO , duration );
				
				float nowY = Selection.activeGameObject.transform.localEulerAngles.y;
				animNode.amountX = 0f;// = new Vector3( 0f , 358f , 0f );
				animNode.amountY = nowY + 180f;//180f;
				animNode.amountZ = 0f;
		
				animNode.toggleFromAmount = false;					
				animNode.Preview();
			}
							
		EditorGUILayout.EndHorizontal();
		
		float halfScrW = Screen.width/2f;
		m_SlideInL_X = EditorGUILayout.Slider( "Slide In LeftPosition" , m_SlideInL_X , - halfScrW ,  +halfScrW );				
		m_SlideInR_X = EditorGUILayout.Slider( "Slide In RightPosition" , m_SlideInR_X , - halfScrW ,  +halfScrW );				
		duration = EditorGUILayout.Slider( "duration( ms )" , duration , 1f , 5000f  );				
	
/*				if( GUILayout.Button( "OK" ) ){
					
					GameObject scenarioObj = ViNoToolUtil.CreateANewScenario( ViNoToolbox.m_ScenarioName , ViNoToolbox.m_StartAndPlay );
					
				}
//*/					
	}
#endif		
	
}
