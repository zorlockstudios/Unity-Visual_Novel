//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
using UnityEngine;
using System.Collections;
using System.IO;

public class ExternalAccessor  {
	
	/// <summary>
	/// Return true if the file exists.
	/// </summary>
	static public bool IsExternalSavedFileExists( string fileName ){
#if UNITY_WEBPLAYER
		return PlayerPrefs.HasKey( fileName );
#else
		string dataPath = ViNoGameSaveLoad.GetDataPath();
		return ( System.IO.File.Exists( dataPath  +  "/" + fileName ) );
#endif		
	}

	static public bool IsSaveDataFileExists( string fileName ){
		bool existsSaveData = IsExternalSavedFileExists( fileName + ".xml"  );
		bool existsFlagData = IsExternalSavedFileExists( fileName + "Flag.xml" );
		if( !existsSaveData ){
			Debug.LogWarning( "SaveData File:" + fileName + ".xml Not Exists." );
		}
		if( !existsFlagData ){
			Debug.LogWarning( "SaveData File:" + fileName + "Flag.xml Not Exists." );
		}
		return existsSaveData && existsFlagData;
	}

	static public string GetCompiledScenarioPath( string outputFileName ){
		string fileNameWithExt = outputFileName + ".bytes";	
#if UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN
		string path = Application.dataPath + "/" + fileNameWithExt;
#else
		string path = Application.persistentDataPath + "/"+ fileNameWithExt;
#endif								
		return path;
	}
	
	// Save as BinaryFile.
	static public void SaveAsBinaryFile( string outputFileName , byte[] bytes ){		
		string path = GetCompiledScenarioPath( outputFileName );		
		WriteBinaryFile( bytes , path );					
	}
	
	
	static public void WriteBinaryFile( byte[] bytes , string path ){
		FileStream fs = new FileStream( path , FileMode.Create );
	    BinaryWriter bw  = new BinaryWriter( fs );
		bw.Write( bytes );
		bw.Close();		
		fs.Close();		
		ViNoDebugger.Log( "Wrote BinaryFile to " + path );		
	}

}
