using UnityEngine;
using System.Collections;

/// <summary>
/// Sound entry. name is the key of AudioSource.
/// </summary>
[ System.Serializable ]
public class SoundMetaDataEntry{
	public string name;
	public ResourceMetaData resourceMetaData;
}

[ System.Serializable ]
public class SEorVoiceDataEntry{
	public string name;
	public AudioClip audioClip;
}
