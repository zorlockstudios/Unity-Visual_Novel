//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class ClickAndLoadLevel : MonoBehaviour {

	void OnClickAndLoadLevel( string levelName ){
		if( ! string.IsNullOrEmpty( levelName ) ){
			SceneManager.LoadScene( levelName );
			//Application.LoadLevel( levelName ); 
		} 
	}
}
