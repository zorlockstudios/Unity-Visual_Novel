//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
using UnityEngine;
using System.Collections;

[ System.Serializable ]
public class ViNoSaveInfo : ScriptableObject {	
	public ViNoSaveData data = new ViNoSaveData();

	public void ClearData(){
		data.m_LoadedLevelIndex = 0;
		data.m_LoadedLevelName = string.Empty;
		data.m_CurrentScenarioName = string.Empty;
		data.m_NodeName = string.Empty;
		data.m_SceneXmlData  = string.Empty;
		data.m_BgmName  = string.Empty;
		data.m_ScenarioResourceFilePath = string.Empty;
		data.m_Date = string.Empty;
		data.m_ScenarioDescription = string.Empty;
	}
}
