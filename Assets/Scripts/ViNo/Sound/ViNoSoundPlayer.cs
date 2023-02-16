//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Simple sound player.
/// Store the SoundEntries.
/// </summary>
[ AddComponentMenu( "ViNo/Sound/ViNoSoundPlayer" ) ]
//[ExecuteInEditMode]	
public class ViNoSoundPlayer : ISoundPlayer {
	public bool loopBGM;

	[ Range( 0.1f , 10f ) ]
	public float crossFadeTime = 4f;
		
	public SoundMetaDataEntry[] soundEntry;		// Max 255.
	public SEorVoiceDataEntry[] voiceEntries;	// Max 255.
	public SEorVoiceDataEntry[] seEntries;		// Max 255.

#if false	
	public ScriptableSoundData soundData;

	void OnEnable(){
		if( soundData != null ){
			SetSoundData( soundData );
		}
	}

	public void SetSoundData( ScriptableSoundData data ){
		soundData = data;

		soundEntry = data.bgmEntries;
		voiceEntries = data.voiceEntries;
		seEntries = data.seEntries;
		
		if( Application.isPlaying ){			
			InitDict();
		}
	}
	
#endif

	// ---------------------- Properties ---------------------------------.

	static public AudioSource currentAudio{get; set;}
	static private AudioSource prevAudio{ get; set; }

	static public string currentAudioKey{ get; set; }
		
	private Dictionary<string , SoundMetaDataEntry> m_SoundDict;
	private Dictionary<string , AudioClip> m_SE_and_VoiceSoundDict;
			
	void InitDict(){
		m_SoundDict = new Dictionary<string, SoundMetaDataEntry>();
		m_SE_and_VoiceSoundDict = new Dictionary<string, AudioClip>();
		if( soundEntry != null ){
			for( int i=0;i<soundEntry.Length;i++){
				string key = soundEntry[ i ].name;
				m_SoundDict[ key ] = soundEntry[ i ];
			}
		}
		
		if( seEntries != null ){
			for( int i=0;i<seEntries.Length;i++){
				string key = seEntries[ i ].name;
				m_SE_and_VoiceSoundDict[ key ] = seEntries[ i ].audioClip;			
			}
		}

		if( voiceEntries != null ){
			for( int i=0;i<voiceEntries.Length;i++){
				string key = voiceEntries[ i ].name;
				m_SE_and_VoiceSoundDict[ key ] = voiceEntries[ i ].audioClip;			
			}
		}
	}

	/// <summary>
	/// Raises the awake event.
	/// </summary>
	public override void OnAwake( ){
		InitDict();
	}
	
	/// <summary>
	/// Raises the load event.
	/// </summary>
	/// <param name='info'>
	/// Info.
	/// </param>
	public override void OnLoad( ViNoSaveData info ){ 
		if( currentAudioKey == info.m_BgmName ){
			ViNoDebugger.Log( "Sound" ,  "currentAudioKey:"+ currentAudioKey + "info.BGM:" + info.m_BgmName);
			return;
		}

		ViNoDebugger.Log( "Sound" ,  "<color=cyan>" + "OnLoad in ViNoSoundPlayer" + "</color>");

		StopMusic( 1f );
		
		ViNoDebugger.Log( "Sound" , info.m_BgmName + " Play. " );
		
		if( ! string.IsNullOrEmpty( info.m_BgmName ) || ! info.m_BgmName.Equals( "NotPlayed" ) ){
			StartCoroutine( "DelayPlayBGM" , info.m_BgmName );			
		}
		else{
			ViNoDebugger.Log ( "Sound" , "No Music was Played when Saved." );	
			StopMusic( kPlayDelayWhenLoaded );
		}	
	}
	
	/// <summary>
	/// Raises the save event.
	/// </summary>
	/// <param name='info'>
	/// Info.
	/// </param>
	public override void OnSave( ViNoSaveData info ){ 
		if( currentAudio != null ){
			if( currentAudio.isPlaying ){
				ViNoDebugger.Log( "Sound" , currentAudioKey + " is Playing when Save. Now , Save the Music Name." );
				info.m_BgmName = currentAudioKey;
			}
			else{
				info.m_BgmName = "NotPlayed";
			}
		}
		else{			
			info.m_BgmName = "NotPlayed";
		}		
	}
	
	/// <summary>
	/// Raises the start event.
	/// </summary>
	public override void OnStart(){
		if( m_PlayMusicAtStart ){
			PlayMusic( m_StartMusicName , ViNoConfig.prefsBgmVolume , m_StartMusicDelay );
		}
	}
	
	/// <summary>
	/// BGMs the volume changed.
	/// </summary>
	/// <param name='val'>
	/// Value.
	/// </param>
	public override void BGMVolumeChanged( float val ){
		if( currentAudio != null ){
			currentAudio.volume = val;
		}
	}
	
	void LoadAudioClipAndPlayDelayed( string audioPath , float vol , float delay ){
		AudioClip clip = Resources.Load( audioPath ) as AudioClip;
		if( clip == null ){
			currentAudio = null;
			return;
		}		

#if false
		if( m_BGMObject == null ){
			m_BGMObject = new GameObject( "__BGM" );// + metaData.anyResourcePath );
			m_BGMObject.transform.parent = transform;
			currentAudio = m_BGMObject.AddComponent<AudioSource>();
			prevAudio 	 = m_BGMObject.AddComponent<AudioSource>();
		}
#else
		GameObject bgmObj = new GameObject( "__BGM:" + audioPath );
		currentAudio = bgmObj.AddComponent<AudioSource>();
#endif
		currentAudio.clip = clip;
		currentAudio.volume = vol;
		currentAudio.loop = loopBGM;
		currentAudio.PlayDelayed ( delay );		
	}

	void LoadAudioClipAndPlayDelayed( ResourceMetaData metaData , float vol , float delay ){
		LoadAudioClipAndPlayDelayed( metaData.anyResourcePath , vol , delay );
	}
	
	/// <summary>
	/// Plaies the music.
	/// </summary>
	/// <param name='name'>
	/// Name.
	/// </param>
	/// <param name='volume'>
	/// Volume.
	/// </param>
	/// <param name='delay'>
	/// Delay.
	/// </param>
	public override void PlayMusic( string name , float volume , float delay ){
		if( currentAudio != null ){
			StopMusic( crossFadeTime );
		}
		
		if( m_SoundDict.ContainsKey( name ) ){
			LoadAudioClipAndPlayDelayed( m_SoundDict[ name ].resourceMetaData , volume ,  delay );
			currentAudioKey = name;
		}		
		else{
			currentAudio = null;
		}
	}
			
	/// <summary>
	/// Play SE Sound.
	/// </summary>
	/// <param name='name'>
	/// Name.
	/// </param>
	/// <param name='volume'>
	/// Volume.
	/// </param>
	/// <param name='delay'>
	/// Delay.
	/// </param>
	public override void PlaySE( string name , float volume , float delay ){	
		if( m_SE_and_VoiceSoundDict.ContainsKey( name ) ){
			AudioSource audioSource = null;
			for( int i=0;i<k_SeAndVoicePlayMax;i++){
				if( i == m_VoiceIndex ){
					continue;	
				}
				if( ! m_AudioPool[ i ].GetComponent<AudioSource>().isPlaying ){
					audioSource = m_AudioPool[ i ].GetComponent<AudioSource>();
					break;
				}
			}		
			if( audioSource == null ){
				audioSource = m_AudioPool[ 1 ].GetComponent<AudioSource>();	// m_AudioSourcePool[ 0 ] is Voice.
			}						
			AudioClip clip = m_SE_and_VoiceSoundDict[ name ];						
			audioSource.clip = clip;
			audioSource.volume = volume;
			audioSource.PlayDelayed( delay );
		}
	}
	
	public override void PlayMusic( int id , float volume , float delay ){
		if( soundEntry != null && soundEntry.Length > 0 && soundEntry.Length > id ){
			PlayMusic( soundEntry[ id ].name , volume , delay) ;
		}
	}
	
	public override void PlaySE( int id , float volume , float delay ){
		ViNoDebugger.Log( "Sound" ,  "" );
		if( seEntries != null && seEntries.Length > 0 && seEntries.Length > id ){
			PlaySE( seEntries[ id ].name , volume , delay) ;
		}
	}

	/// <summary>
	/// Play SE Voice.
	/// </summary>
	/// <param name='name'>
	/// Name.
	/// </param>
	/// <param name='volume'>
	/// Volume.
	/// </param>
	/// <param name='delay'>
	/// Delay.
	/// </param>
	public override void PlayVoice( string name , float volume , float delay ){
		if( m_SE_and_VoiceSoundDict.ContainsKey( name ) ){
			AudioSource	audioSource = m_AudioPool[ m_VoiceIndex ].GetComponent<AudioSource>();						
			AudioClip clip = m_SE_and_VoiceSoundDict[ name ];						
			audioSource.clip = clip;
			audioSource.volume = volume;				
			audioSource.PlayDelayed( delay );
		}		
	}
	
	public override void PlayVoice( int id , float volume , float delay ){
		if( voiceEntries != null && voiceEntries.Length > 0 && voiceEntries.Length > id ){
			PlayVoice( voiceEntries[ id ].name , volume , delay) ;
		}
	}
	

	private float m_FadeTime;

	/// <summary>
	/// Stops the music.
	/// </summary>
	/// <param name='fadeTime'>
	/// Fade time.
	/// </param>
	public override void StopMusic( float fadeTime ){
		m_FadeTime = fadeTime;

#if true
//		prevAudio = currentAudio;
#else		
		prevAudio.clip = currentAudio.clip;
		prevAudio.volume = currentAudio.volume;
		prevAudio.loop = currentAudio.loop;
		prevAudio.PlayDelayed( 0f );
#endif
		if( currentAudio != null ){
			StartCoroutine( "Fadeout" , currentAudio ) ;
		}
	}
	
	/// <summary>
	/// Stops the SE.
	/// </summary>
	/// <param name='name'>
	/// Name.
	/// </param>
	/// <param name='fadeTime'>
	/// Fade time.
	/// </param>
	public override void StopSE(){
		for( int i =0;i<k_SeAndVoicePlayMax;i++){
			m_AudioPool[ i ].GetComponent<AudioSource>().Stop();
		}
	}	
	
	/// <summary>
	/// Stops the voice.
	/// </summary>
	/// <param name='name'>
	/// Name.
	/// </param>
	/// <param name='fadeTime'>
	/// Fade time.
	/// </param>
	public override void StopVoice( ){
		m_AudioPool[ m_VoiceIndex ].GetComponent<AudioSource>().Stop();
	}

	//TODO : IMPL.
	public override void StopSE( string name ,  float fadeTime ){
		
	}

	//TODO : IMPL.
	public override void StopVoice( string name ,  float fadeTime ){
		
	}

	/// <summary>
	/// Stops the sound.
	/// </summary>
	/// <param name='name'>
	/// Name.
	/// </param>
	private void StopSound( string name ){
		if( m_SoundDict.ContainsKey( name ) ){
			if( currentAudio != null ){
				Destroy( currentAudio.gameObject );				
			}			
		}
	}
	

	/// <summary>
	/// Fadeout the specified duration.
	/// </summary>
	/// <param name='duration'>
	/// Duration.
	/// </param>
	IEnumerator Fadeout( AudioSource audioSource ){		
		float currentTime = 0.0f;
		float waitTime = 0.01f;
		if( audioSource != null ){
			float firstVol = audioSource.volume;
			while ( m_FadeTime  > currentTime){
			    currentTime += Time.fixedDeltaTime;
				if( audioSource == null ){
					break;	
				}
			    audioSource.volume = Mathf.Clamp01(firstVol * ( m_FadeTime - currentTime) / m_FadeTime);
			    yield return new WaitForSeconds( waitTime );
			}
			
			if( audioSource != null ){
				audioSource.Stop();
				audioSource.clip = null;
//				audioSource.gameObject.SetActive( false );
				Destroy( audioSource.gameObject );
			}
//			prevAudio = null;						
		}
	}	

	// ---------------------- GETTER ---------------------------------.

	public int GetSoundIDBy( string key ){
		if( soundEntry != null && soundEntry.Length > 0 ){
			for( int i=0;i<soundEntry.Length;i++){
				if( soundEntry[ i ].name == key ){
					return i;
				}
			}
		}
		return -1;
	}	

	public string[] GetSoundEntryNames( ){
		List<string> names = new List<string>();
		if( soundEntry != null && soundEntry.Length > 0 ){
			for( int i=0;i<soundEntry.Length;i++){
				names.Add( soundEntry[ i ].name );					
			}
		}
		return names.ToArray();
	}
	
	public string[] GetSeEntryNames( ){
		List<string> names = new List<string>();
		if( seEntries != null && seEntries.Length > 0 ){
			for( int i=0;i<seEntries.Length;i++){
				names.Add( seEntries[ i ].name );					
			}
		}
		return names.ToArray();
	}
			
	public string[] GetVoiceEntryNames( ){
		List<string> names = new List<string>();
		if( voiceEntries != null && voiceEntries.Length > 0 ){
			for( int i=0;i<voiceEntries.Length;i++){
				names.Add( voiceEntries[ i ].name );					
			}
		}
		return names.ToArray();
	}

}
