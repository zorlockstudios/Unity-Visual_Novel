//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
using UnityEngine;
using System.Collections;

/// <summary>
/// Screen Fade Effect.
/// </summary>
public class GUIFadeTransition : ITransition {		
	public bool _PLAY;

	public bool awakeAndPlay;
	public bool isFadeIn;
	public bool fadeAndLoadLevel;
	[ HideInInspector ] public bool dontDestroyOnLoad;

	public string nextSceneName;
	public Color fadeColor;
	public GameObject eventReceiver;

	[ Range(0.1f,3f) ]
	public float fadeSpeed = 1f;

	private Texture2D m_Texture;
    private float m_Counter = 0;
    private bool m_IsFadeIn = true;
	private bool m_Update = false;

	private float k_FadeTick = 0.01f;
	
	void Awake(){
		if( awakeAndPlay ){
			StartFade( isFadeIn );
		}
	}

    void OnApplicationQuit(){
        Destroy( m_Texture );
    }
	
    void OnGUI(){				
		if( m_Texture == null ){
			return;
		}
      	GUI.DrawTexture(new Rect(0, 0, Screen.width + 2f, Screen.height), m_Texture  );
    }

    public void TogglePlay(){
    	m_IsFadeIn = ! m_IsFadeIn;
		if( m_IsFadeIn ){
			fadeColor.a = 0f;
		}
		else{
			fadeColor.a = 1f;
		}
		StartFade( m_IsFadeIn );
    }

    public override void BeginTransition(){
    	Debug.Log( "Begin GUIFadeTransition" );
    	StartFade( true );
    }

    public override void EndTransition(){
    	Debug.Log( "End GUIFadeTransition" );
    	StartFade( false );
    }

    void Update(){
    	if( _PLAY ){
    		_PLAY = false;
			StartFade( isFadeIn );
    	}

		if( m_Update ){			
			Color col = fadeColor;
			col.a = m_Counter;			
	        m_Texture.SetPixel(0, 0, col );
    	    m_Texture.Apply();
    	    float tick = k_FadeTick * fadeSpeed;
  	    	if (m_IsFadeIn){
           		 m_Counter += tick;
           		 if (m_Counter > 1){
					m_Counter = 1;
	           	    m_IsFadeIn = false;
					m_Update = false;
					if( eventReceiver != null ){
						eventReceiver.SendMessage( "DidFadeIn" , SendMessageOptions.DontRequireReceiver );
					}
					
					if( fadeAndLoadLevel ){
						if( ! nextSceneName.Equals( "" )) {
							Application.LoadLevel( nextSceneName );
						}
					}
				}
	   	   	}			
   	     	else{
        	   	 m_Counter -= tick;
	           	 if(m_Counter < 0){
					m_Counter = 0;
					m_IsFadeIn = true;
					m_Update = false;
					if( eventReceiver != null ){
						eventReceiver.SendMessage( "DidFadeOut" , SendMessageOptions.DontRequireReceiver );						
					}
				}
			}
		}    	
    }

	static public Texture2D  Createm_Texture (   Color color   ){
		Texture2D ret = new Texture2D( 1 , 1 );
	    ret.SetPixel(0, 0, color );
	    ret.Apply(); //Do not Forget to do this !	    
		return ret;
	}
	
	public void StartFadeAndLoadLevel( string levelName ){
		nextSceneName = levelName;
		m_IsFadeIn = true;
		m_Update = true;
		if( ISoundPlayer.Instance != null ){
			ISoundPlayer.Instance.StopMusic( ISoundPlayer.kMusicFadeTimeOnClickBack );	
		}		
	}
	
	// speed is the tick .
	public void StartFade( Color color , bool fadeIn , float speed  ){
		fadeColor = color;
		StartFade( fadeIn , speed );		
	}

	public void StartFade(  bool fadeIn ){
		StartFade( fadeIn , fadeSpeed );
	}
	
	public void StartFade(  bool fadeIn , float speed  ){
		if( m_Texture == null ){
			m_Texture = Createm_Texture( fadeColor );
		    m_Texture.SetPixel(0, 0, fadeColor );
		    m_Texture.Apply();		
		}
		fadeSpeed = speed;
		m_IsFadeIn = fadeIn;
		m_Update = true;
		if( fadeIn ){
			m_Counter = 0f;	
		}
		else{
			m_Counter =1f;
		}
	}
		
	void SetNextSceneName( string next ){
		nextSceneName = next;		
	}
    
	IEnumerator FadeInAndFadeOut(){
		StartFade( true );

		yield return new WaitForSeconds( 1f );

		StartFade( false );
	}
	
}