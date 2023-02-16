//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.

using UnityEngine; 
using System.Collections; 
using System.Collections.Generic; 
using System.Xml; 
using System.Xml.Serialization; 
using System.IO; 
using System.Text; 

/// <summary>
/// _ game save load.
/// This Script is from ...
///	 http://wiki.unity3d.com/index.php?title=Save_and_Load_from_XML
/// Thanks.
/// </summary>
public class ViNoGameSaveLoad {   	
	static private string _FileLocation;
	static private string _FileName; 
 		 		
	static private string SaveAndGetXMLString(){
		ViNoSceneManager.Instance.SaveSceneNodes( );				
		
	    string xmlStr =SerializeObject<SceneData>( ViNoSceneManager.Instance.GetSceneData() ); 
		
		return xmlStr;
	}
		
	// Filename without Extension.
	static public void SaveToExternalFile( string fileName ){
#if UNITY_EDITOR		
      _FileLocation=Application.dataPath; 
#else
      _FileLocation=Application.persistentDataPath; 		
#endif						
      // Where we want to save and load to and from 
      _FileName = fileName+ ".xml";
		
		string xmlData = SaveAndGetXMLString();

    	CreateXML( xmlData ); 						
				
		ViNoDebugger.Log( "SaveInfo" , "Saved Scene as XML Data : " + xmlData );
	}
	
	static public void LoadFromExternalFile( string fileName ){
#if UNITY_EDITOR		
      _FileLocation=Application.dataPath; 
#else
      _FileLocation=Application.persistentDataPath; 		
#endif		
      _FileName = fileName+ ".xml";
				
	    string xmlData = LoadXML();
		
		ViNoGameSaveLoad.LoadSceneFromXmlString( xmlData );		
	}
	
	static public string Save( ){		
		return SaveAndGetXMLString();		
	}
	
	static public void Load( ViNoSaveInfo info ){
		ViNoGameSaveLoad.LoadSceneFromXmlString( info.data.m_SceneXmlData );
	}
	
	static public void LoadSceneFromXmlString( string xmlStr ){
		ViNoSceneManager sm = ViNoSceneManager.Instance;
		if( sm == null ){
			ViNoDebugger.LogError( "SceneManager Instance NOT FOUND. Can't Loaded." );	
			return;
		}
				
     	sm.SetSceneData( (SceneData)DeserializeObject<SceneData>( xmlStr ) ); 
		SceneData sceneData = sm.GetSceneData();		
		
		sm.SetNodeData( sceneData.m_DataArray );
		sm.CreateSceneNodes();				
		
		ViNoDebugger.Log( "SaveInfo" , "Loaded Scene from XmlString." );				
	}
	
 
   /* The following metods came from the referenced URL */ 
	static public string UTF8ByteArrayToString(byte[] characters) 
   {      
      UTF8Encoding encoding = new UTF8Encoding(); 
      string constructedString = encoding.GetString(characters); 
      return (constructedString); 
   } 
 
   static public byte[] StringToUTF8ByteArray(string pXmlString) 
   { 
      UTF8Encoding encoding = new UTF8Encoding(); 
      byte[] byteArray = encoding.GetBytes(pXmlString); 
      return byteArray; 
   } 
 
   // Here we serialize our SceneData object of sceneData 
   static public string SerializeObject<T>(object pObject) 
   { 
      string XmlizedString = null; 
      MemoryStream memoryStream = new MemoryStream(); 
      XmlSerializer xs = new XmlSerializer(typeof(T)); 
      XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8); 
		
      xs.Serialize(xmlTextWriter, pObject); 
		
      memoryStream = (MemoryStream)xmlTextWriter.BaseStream; 		
      XmlizedString = UTF8ByteArrayToString(memoryStream.ToArray()); 
      return XmlizedString; 
   } 
 
   // Here we deserialize it back into its original form 
   static public object DeserializeObject<T>(string pXmlizedString) 
   { 
      XmlSerializer xs = new XmlSerializer(typeof(T)); 
      MemoryStream memoryStream = new MemoryStream(StringToUTF8ByteArray(pXmlizedString)); 
      return xs.Deserialize(memoryStream); 
   }
	
	static public string SerializeStack<T>( Stack<T> stack ){
		List<T> tempList = new List<T>( stack );
		string xml =  ViNoGameSaveLoad.SerializeObject<List<T>>( tempList );
		return xml;	
	}
		
	static public Stack<T> DeserializeStack<T>( string xml ){	
		List<T> stackLoaded =  (List<T>)ViNoGameSaveLoad.DeserializeObject<List<T>>( xml );
		stackLoaded.Reverse();	
		Stack<T> retstack = new Stack<T>( stackLoaded );		
		return retstack;
	}
	
	 static public DictionaryEntry[] ConvertHashtableToArray(Hashtable ht)
	  {
	        DictionaryEntry[] entries = new DictionaryEntry[ht.Count];
	        int entryIndex = 0;
	        foreach (DictionaryEntry de in ht)
	        {
				ViNoDebugger.Log ( "De Val GetType:" +de.Value.GetType() );
	            entries[entryIndex] = de;
	            entryIndex++;
	        }
	        return entries;
	   }

    static public Hashtable ConvertArrayToHashtable(DictionaryEntry[] ary)
    {
        Hashtable ht = new Hashtable(ary.Length);
        foreach (DictionaryEntry de in ary)
        {
            ht.Add(de.Key, de.Value);
        }
        return ht;
    }		
 
 	static public void CreateXML( string fileName ,  string dataStr ){ 
		_FileName = fileName;
		CreateXML( dataStr );
	}
	
   // Finally our save and load methods for the file itself 
 	static public void CreateXML( string dataStr ){ 				
		_FileLocation = GetDataPath();
		/*
		if( Application.platform == RuntimePlatform.WindowsWebPlayer
								||
			 Application.platform == RuntimePlatform.OSXWebPlayer ){
				ViNoDebugger.LogWarning( "WebPlayer not support FileInfo. Can't CreateXML." );
			return;
		}
		*/
		
#if UNITY_EDITOR
		string path = _FileLocation+"/"+ _FileName	;
#else
		string path = _FileLocation + "/" + _FileName;
#endif	

		try
		{
		  using ( System.IO.StreamWriter sw = new System.IO.StreamWriter( path ) )
		  {
		    try
		    {
				sw.Write(dataStr); 
				ViNoDebugger.Log( "SaveInfo" , "File written."); 
		    }
		    finally
		    {
		      sw.Close();
		    }
		  }
		}
		catch(System.IO.IOException er)
		{
			// Disk Full or some exception.
			Debug.LogError( "An Error occured while Creating a File." );
			Debug.LogError( "Message:" + er.Message );

//			DialogManager.Instance.ShowSelectDialog( "IOException" , er.Message , Dummy );
		}
   } 

   static public void Dummy( bool t ){}
	
 	static public string LoadXML( string fileName ){ 		
		_FileName = fileName;
		string xmlStr = LoadXML( );
		return xmlStr;
	}
 
	static public string LoadXML( ){ 		
		_FileLocation = GetDataPath();		
		string path = "";
#if UNITY_EDITOR		
	path = _FileLocation+"/"+ _FileName;
#else
	path = _FileLocation + "/" + _FileName;
#endif					
		if( System.IO.File.Exists( path ) ){
	      StreamReader r = File.OpenText( path ); 
	      string _info = r.ReadToEnd(); 
	      r.Close(); 
	//      _data=_info; 
	     	ViNoDebugger.Log( "SaveInfo" ,  "File Read"); 
			return _info;
	     }
	     else{
	     	return string.Empty;
	     }
   } 
	
	static public string GetDataPath(){
#if UNITY_EDITOR		
      return Application.dataPath; 
#else
     return Application.persistentDataPath; 		
#endif		
		
	}
	
} 


