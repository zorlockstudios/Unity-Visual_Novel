//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
using UnityEngine;
using System.Collections;

public class GUISceneFade : MonoBehaviour {
	static private GUISceneFade instance;
	public static GUISceneFade Instance {
		get {	return GUISceneFade.instance;	}
	}			
		
	public Color _COLOR;
	public bool dontDestroyOnLoad;
	public GameObject receiver;
	public string nextSceneName;
	public float fadeSpeed = 0.01f;
    public bool fadeInFlag = true;
	public bool startFading = false;

	private Texture2D texture;
    private float cnt = 0;
	private bool _SCENE_TRANSITION;
	
    void Awake(){
		if( instance == null ){
			instance=  this;					
			gameObject.SetActive( false );
			if( dontDestroyOnLoad ){
				DontDestroyOnLoad( instance.gameObject );
			}

	        texture = CreateTexture( new Color( 1f,1f,1f,1f ) );			
		}
		else{
			Destroy( gameObject );		
		}		
    }
	
	static public Texture2D  CreateTexture (   Color color   ){
		Texture2D ret = new Texture2D( 1 , 1 );
	    ret.SetPixel(0, 0, color );
	    ret.Apply(); //Do not Forget to do this !	    
		return ret;
	}
	
	public void StartFadeAndLoadLevel( string levelName ){
		_SCENE_TRANSITION = true;
		nextSceneName = levelName;
		fadeInFlag = true;
		StartFading( true );		
		if( ISoundPlayer.Instance != null ){
			ISoundPlayer.Instance.StopMusic( ISoundPlayer.kMusicFadeTimeOnClickBack );	
		}		
	}
	
	// speed is the tick .
	public void StartFade( Color color , bool fadeIn , float speed  ){
		_COLOR = color;
		StartFade( fadeIn , speed );		
	}

	public void StartFade(  bool fadeIn ){
		StartFade( fadeIn , fadeSpeed );
	}
	
	public void StartFade(  bool fadeIn , float speed  ){
		fadeSpeed = speed;
		_SCENE_TRANSITION = false;
		fadeInFlag = fadeIn;
		if( fadeIn ){
			cnt = 0f;	
		}
		else{
			cnt =1f;
		}
		StartFading( true );					
	}
	
	public void StartFading( bool t  ){
		startFading = t;
	}
	
	void SetNextSceneName( string next ){
		nextSceneName = next;		
	}
	
	void Start(){		
		Color col = _COLOR;
		if( fadeInFlag ){
			col.a = 0f;
		}
		else{
			col.a = 1f;
		}
	    texture.SetPixel(0, 0, col );
	    texture.Apply();		
	}
	
	IEnumerator FadeInAndLoadLevelAndFadeOut(){
		StartFade( true );

		yield return new WaitForSeconds( 1f );

		Application.LoadLevel( "Scene 2 with SimpleUI" );

		StartFade( false );
	}

    void OnGUI()
    {				
#if false    	
    	if( GUILayout.Button("Fade and LoadLevel") ){
    		StartCoroutine( "FadeInAndLoadLevelAndFadeOut" );
		}	
    	if( GUILayout.Button("FADE_IN") ){
    		StartFade( true );
    	}
    	if( GUILayout.Button("FADE_OUT") ){
    		StartFade( false );
    	}
#endif
		if( texture == null ){
			ViNoDebugger.LogWarning( "texture must not be NULL." );
			return;
		}
		
		if( startFading ){			
			Color col = _COLOR;
			col.a = cnt;			
	        texture.SetPixel(0, 0, col );
    	    texture.Apply();
  	    	if (fadeInFlag){
           		 cnt += fadeSpeed;
           		 if (cnt > 1){
					cnt = 1;
	           	    fadeInFlag = false;
					startFading = false;
					if( receiver != null ){
						receiver.SendMessage( "DidFadeIn" , SendMessageOptions.DontRequireReceiver );
					}
					
					if( _SCENE_TRANSITION ){
						if( ! nextSceneName.Equals( "" )) {
							Application.LoadLevel( nextSceneName );
						}
					}
				}
	   	   	}			
   	     	else{
        	   	 cnt -= fadeSpeed;
	           	 if(cnt < 0){
					cnt = 0;
					fadeInFlag = true;
					startFading = false;
					if( receiver != null ){
						receiver.SendMessage( "DidFadeOut" , SendMessageOptions.DontRequireReceiver );						
					}
				}
			}
		}
      	GUI.DrawTexture(new Rect(0, 0, Screen.width + 2f, Screen.height), texture  );
    }
    
    void OnApplicationQuit()
    {
        Destroy(texture);
    }
}