//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.

#pragma warning disable 0414,0649

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

	/// <summary>
	/// Simple sound player.
	/// Store the SoundEntries.
	/// </summary>
	[ AddComponentMenu( "ViNo/Sound/SimpleSoundPlayer" ) ]
	public class SimpleSoundPlayer : ISoundPlayer {
			
		// ---------------------- Properties ---------------------------------.

		static public AudioSource currentAudio{get; set;}
		static private AudioSource prevAudio{ get; set; }

		static public string currentAudioKey{ get; set; }
			
		private Dictionary<string , SoundMetaDataEntry> m_SoundDict;
		private Dictionary<string , AudioClip> m_SE_and_VoiceSoundDict;

		void InitDict(){
			m_SoundDict = new Dictionary<string, SoundMetaDataEntry>();
			m_SE_and_VoiceSoundDict = new Dictionary<string, AudioClip>();
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
//				StartCoroutine( "DelayPlayBGM" , info.m_BgmName );			
				bool loop = true;
				PlayMusic( info.m_BgmName , loop , 0f );
//				player.PlayMusic( item , true , 0f );
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
				PlayMusic( m_StartMusicName , true , m_StartMusicDelay );
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
		
		struct FadeAudioInfo{
			public float volume;
			public AudioSource audio;
		}


		AudioSource LoadAudioClipAndPlayDelayed( AudioSource source , string audioPath , float vol , float xchbgmTime , bool loop ){
			AudioClip clip = Resources.Load( audioPath ) as AudioClip;
			source.clip = clip;
			source.volume = vol;
			source.loop = loop;			
			source.PlayDelayed ( xchbgmTime );			
			return source;
		}

		public override void PlayMusic( string path , bool loop , float xchbgmTime = 0f ){
			if( currentAudio != null ){
				StopMusic( xchbgmTime );
			}

			float volume = ViNoConfig.prefsBgmVolume;				
			GameObject sourceGO = new GameObject( path );
			AudioSource source = sourceGO.AddComponent<AudioSource>();
			currentAudio = LoadAudioClipAndPlayDelayed( source , path , volume ,  xchbgmTime , loop );
			currentAudioKey = path;

			// Fadein Music.
#if false			
			FadeAudioInfo info = new FadeAudioInfo();
			info.audio = currentAudio;
			info.volume = volume;
			StartCoroutine( "Fadein" , info );
#endif			
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
//		public override void PlaySE( string path , float volume , float delay ){	
		public override void PlaySE( string path , bool loop , float xchbgmTime = 0f ){
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

			float volume = ViNoConfig.prefsSeVolume;				
			LoadAudioClipAndPlayDelayed( audioSource , path , volume ,  xchbgmTime , loop );
		}
		
		public override void PlayVoice( string path , bool loop , float xchbgmTime = 0f ){
			AudioSource audioSource = m_AudioPool[ m_VoiceIndex ].GetComponent<AudioSource>();

			float volume = ViNoConfig.prefsVoiceVolume;				
			LoadAudioClipAndPlayDelayed( audioSource , path , volume ,  xchbgmTime , loop );			
		}
/*
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
				AudioSource	audioSource = m_AudioPool[ m_VoiceIndex ].audio;						
				AudioClip clip = m_SE_and_VoiceSoundDict[ name ];						
				audioSource.clip = clip;
				audioSource.volume = volume;				
				audioSource.PlayDelayed( delay );
			}		
		}		
//*/
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

/*
		//TODO : IMPL.
		public override void StopSE( string name ,  float fadeTime ){
			
		}

		//TODO : IMPL.
		public override void StopVoice( string name ,  float fadeTime ){
			
		}
//*/		

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
			
		IEnumerator Fadein ( FadeAudioInfo info ){//AudioSource audioSource , int targetVolulme ){
			info.audio.enabled = true;
			int vol = 0;
			int targetVolume = (int)( info.volume * 100 );
			for (; vol<targetVolume; vol++) {
//				Debug.Log( "Fadein.." );
				info.audio.volume = (float)vol / 100;
				yield return null;
			}
			info.audio.volume = info.volume;
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

	}
