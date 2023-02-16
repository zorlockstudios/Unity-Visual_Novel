//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 		VisualNovelToolkit		/_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
using UnityEngine;
using UnityEditor;
using System.Collections;

static public class ViNoEditorResources {
	
	static private void GetTexAtPath( out Texture2D tex , string fileName ){
		string path = ViNoToolUtil.GetAssetDataPath() + "Editor Icons/" + fileName;// dialogicon.png";
		tex = AssetDatabase.LoadAssetAtPath( path , typeof(Texture2D) ) as Texture2D;			
	}

	static private Texture2D m_StartMenuTex;
	static public Texture2D startMenuTexture{
		get{
			if( m_StartMenuTex == null ){
				GetTexAtPath( out m_StartMenuTex , "visual.jpg" );
			}
			return m_StartMenuTex;
		}
	}

	static private Texture2D m_DialogIcon;
	static public Texture2D dialogIcon{
		get{
			if( m_DialogIcon == null ){
				GetTexAtPath( out m_DialogIcon , "dialogicon.png" );
			}
			return m_DialogIcon;
		}
	}

	static private Texture2D m_ActorEnterIcon;
	static public Texture2D actorEnterIcon{
		get{
			if( m_ActorEnterIcon == null ){
				GetTexAtPath( out m_ActorEnterIcon , "slide-inc2.png" );
			}
			return m_ActorEnterIcon;
		}
	}

	static private Texture2D m_PlusIcon;
	static public Texture2D plusIcon{
		get{
			if( m_PlusIcon == null ){
				GetTexAtPath( out m_PlusIcon , "plusicon.png" );
			}
			return m_PlusIcon;
		}
	}

	static private Texture2D m_MinusIcon;
	static public Texture2D minusIcon{
		get{
			if( m_MinusIcon == null ){
				GetTexAtPath( out m_MinusIcon , "minusicon.png" );
			}
			return m_MinusIcon;
		}
	}

	static private Texture2D m_CheckAllIcon;
	static public Texture2D checkAllcon{
		get{
			if( m_CheckAllIcon == null ){
				GetTexAtPath( out m_CheckAllIcon , "checkbox.png" );
			}
			return m_CheckAllIcon;
		}
	}

	// Toolbox icons.

	static private Texture2D m_AnimIcon;
	static public Texture2D animationIcon{
		get{
			if( m_AnimIcon == null ){
				GetTexAtPath( out m_AnimIcon , "animationicon.png" );
			}
			return m_AnimIcon;
		}
	}

	static private Texture2D m_ConvTemplate;
	static public Texture2D convTemplateIcon{
		get{
			if( m_ConvTemplate == null ){
				GetTexAtPath( out m_ConvTemplate , "iconbase.png" );
			}
			return m_ConvTemplate;
		}
	}

	static private Texture2D m_BranchIcon;
	static public Texture2D branchIcon{
		get{
			if( m_BranchIcon == null ){
				GetTexAtPath( out m_BranchIcon , "arrow_branch.png" );
			}
			return m_BranchIcon;
		}
	}

	static private Texture2D m_SelectionsIcon;
	static public Texture2D selectionsIcon{
		get{
			if( m_SelectionsIcon == null ){
				GetTexAtPath( out m_SelectionsIcon , "menu_item.png" );
			}
			return m_SelectionsIcon;
		}
	}

	static private Texture2D m_JumpIcon;
	static public Texture2D jumpIcon{
		get{
			if( m_JumpIcon == null ){
				GetTexAtPath( out m_JumpIcon , "go_jump.png" );
			}
			return m_JumpIcon;
		}
	}

	static private Texture2D m_FolderIcon;
	static public Texture2D folderIcon{
		get{
			if( m_FolderIcon == null ){
				GetTexAtPath( out m_FolderIcon , "folder.png" );
			}
			return m_FolderIcon;
		}
	}	

	static private Texture2D m_TitleImage;
	static public Texture2D titleImage{
		get{
			if( m_TitleImage == null ){
				GetTexAtPath( out m_TitleImage , "TitleTemplate1.png" );
			}
			return m_TitleImage;
		}
	}			
	
}

#if false
	static private Texture2D m_DialogIconTex;
	static private Texture2D m_ArrowSeekBackTex;
	static private Texture2D m_ArrowBackTex;
	static private Texture2D m_ArrowForwardTex;
	static private Texture2D m_ArrowSeekForwardTex;

	static private void LoadIcons(){
		m_DialogIconTex = AssetDatabase.LoadAssetAtPath( "Assets/EditorIcons/017652-bubble.png" , typeof(Texture2D) ) as Texture2D;
		m_ArrowSeekBackTex = AssetDatabase.LoadAssetAtPath( "Assets/EditorIcons/000003-arrows-seek-back.png" , typeof(Texture2D) ) as Texture2D;
		m_ArrowSeekForwardTex = AssetDatabase.LoadAssetAtPath( "Assets/EditorIcons/000004-arrows-seek-forward.png" , typeof(Texture2D) ) as Texture2D;
		m_ArrowBackTex = AssetDatabase.LoadAssetAtPath( "Assets/EditorIcons/000001-arrow-back.png" , typeof(Texture2D) ) as Texture2D;
		m_ArrowForwardTex = AssetDatabase.LoadAssetAtPath( "Assets/EditorIcons/000002-arrow-forward1.png" , typeof(Texture2D) ) as Texture2D;
	}
	
#endif

