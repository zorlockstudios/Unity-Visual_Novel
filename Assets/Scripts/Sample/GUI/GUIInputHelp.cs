using UnityEngine;
using System.Collections;

public class GUIInputHelp : MonoBehaviour {

	[MultilineAttribute]
	public string helpString;

	private Vector2 m_ScrollPos = Vector2.zero;
	private Rect m_WinRect;
	private bool m_Show;

	void Start(){
//		m_WinRect = new Rect( Screen.width/2f , Screen.height/2f , Screen.width/2f , Screen.height/2f );
		m_WinRect = new Rect( Screen.width/2f , 0f , Screen.width/2f , Screen.height );
	}

	void OnGUI(){
		m_Show = GUILayout.Toggle( m_Show , "Help" );
		if( ! m_Show ){
			return;
		}

		m_WinRect = GUILayout.Window( 0 , m_WinRect , Draw , "help" );


	}

	void Draw( int windowID ){

		m_ScrollPos = GUILayout.BeginScrollView( m_ScrollPos );

			GUILayout.Label( helpString );

		GUILayout.EndScrollView();
	}

}
