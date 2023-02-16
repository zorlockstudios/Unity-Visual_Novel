//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Show GUI text_ level name at the top of left corner.
/// </summary>
public class GUIText_LevelName : MonoBehaviour {

	Text guiText = null;
	// Use this for initialization
	void Start () {

		if (GetComponent<Text>())
		{
			guiText = GetComponent<Text>();
			if (guiText != null)
			{
				SetCurrentLevelName();
			}
		}
	}
	
	void Update(){
		if( guiText != null ){	
			if( string.IsNullOrEmpty( guiText.text ) || guiText.text != Application.loadedLevelName  ){	
				SetCurrentLevelName();
			}
		}
	}
	
	void SetCurrentLevelName( ){
		guiText.text = Application.loadedLevelName;			
	}
}
