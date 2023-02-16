//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
using UnityEngine;
using System.Collections;
using System.Xml.Serialization;

/// <summary>
/// Dialog Unit.
/// </summary>
[ System.Serializable]
public class DialogPartData {			
	[ XmlAttribute()] public bool active = true;
	[ XmlAttribute()] public int dialogID = 0;	
	[ XmlAttribute()] public bool show = false;
	[ XmlAttribute()] public bool isName = false;
	[ XmlAttribute()] public bool isBGM = false;
	[ XmlAttribute()] public bool isSE = false;
	[ XmlAttribute()] public bool isVoice = false;
	[ XmlAttribute()] public bool isAnim = false;
	[ XmlAttribute()] public bool isClearMessageAfter = true;
	[ XmlAttribute()] public int  textBoxID = 0;
	
	[ System.NonSerialized ] public bool toggle;	

	public string nameText = "";
	public string dialogText = "";
	
	[ XmlAttribute()] public DialogPartNodeActionType actionID;	

	// About Audio.
	public string bgmAudioKey;
	public string seAudioKey;
	public string voiceAudioKey;

	public int bgmAudioID;
	public int seAudioID;
	public int voiceAudioID;
	
	/// <summary>
	/// Actor entry.
	/// </summary>
	[ System.Serializable]
	public class ActorEntry{
		public string actorName;
		public string state = null;
		public ViNoToolkit.SceneEvent.ActorPosition position;
		public bool withFade;
	}

	/// <summary>
	/// Actor entry.
	/// </summary>
	[ System.Serializable]
	public class SceneEntry{
		public string sceneName;
		public bool withFade;
	}
	public SceneEntry scene = null;

	public ActorEntry[] enterActorEntries;
	public ActorEntry[] exitActorEntries;

}