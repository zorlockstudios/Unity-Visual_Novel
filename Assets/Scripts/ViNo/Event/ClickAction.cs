//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
using UnityEngine;
using System.Collections;

public class ClickAction : MonoBehaviour {

	void OnClickDestroy(){
		Destroy( this.gameObject );
	}

	void OnClickActivate(){
		this.gameObject.SetActive( true );
	}
	
	void OnClickDeactivate(){
		this.gameObject.SetActive( false );
	}

}
