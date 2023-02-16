//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ViNoFPS : MonoBehaviour{
	public int targetFPS = 60;
	public bool _CHANGE_TARGET_FPS;
	public bool SetTargetFPS;
	public string debugTextColorName;
	public TextMesh textMesh;
	Text guiText = null;
	static private ViNoFPS instance;
	public static ViNoFPS Instance {
		get {	return ViNoFPS.instance;	}
	}		
	
	
	void Awake(){		
		if( instance == null ){
			instance=  this;					
			DontDestroyOnLoad( gameObject );
		}else{
			Destroy( gameObject );		
		}		
	}		
	
	private void Start()
	{
		if( SetTargetFPS ){
			Application.targetFrameRate = targetFPS;
		}
		if(GetComponent<Text>())
        {
			guiText = GetComponent<Text>();
        }
		oldTime = Time.realtimeSinceStartup;
	}
 
	private void Update()
	{
		if( _CHANGE_TARGET_FPS ){
			_CHANGE_TARGET_FPS = false;
			Application.targetFrameRate = targetFPS;		
		}
		
		UpdateFPS();
	}

	private float oldTime;
	private int frame = 0;
	private float frameRate = 0f;
	private const float INTERVAL = 0.5f;

	void UpdateFPS(){
		frame++;
		float time = Time.realtimeSinceStartup - oldTime;
		if (time >= INTERVAL)
		{
			frameRate = frame / time;
			
		 	long memB = System.GC.GetTotalMemory( false );
			
			System.Text.StringBuilder str = new System.Text.StringBuilder();	
			str.Append( "<color=" + debugTextColorName + ">" );
			str.Append(  "FPS : " + frameRate.ToString() );
			str.Append( "\n" );
			str.Append( "System.GC.GetTotalMemory is ...\n" );
			str.Append( memB.ToString( "#,0" ) + " bytes\n" );			
			if( VM.Instance != null ){
				str.Append( "VM callStack :" + VM.Instance.callStack.Count);
			}		
			str.Append( "</color>" );
			
			if( guiText != null) {
				guiText.text = str.ToString();
			}
			else if( textMesh != null ){
				textMesh.text = str.ToString();
			}
			oldTime = Time.realtimeSinceStartup;
			frame = 0;
		}		
	}
	
}
 