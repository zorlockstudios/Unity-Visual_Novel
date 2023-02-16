//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
using UnityEngine;
using System.Collections.Generic;

public class ViNoBackLog : MonoBehaviour {
//	static private List<string> m_Log = new List<string>();
	static private List<DialogPartData> m_Log = new List<DialogPartData>();
	static private ViNoBackLog m_Instance = null;	
	static private bool m_Show;
	static public bool show{	get; set; }
		
#if false	
	[ System.ObsoleteAttribute]
	static public void Add( string str ){
		// If str equals the latest Added String , return.
		if( m_Log.Count > 1 ){
			int lastIndex = m_Log.Count - 1;
			if( m_Log[ lastIndex ].Equals( str ) ){
				return;	
			}
		}
		
		if (m_Log.Count > ViNo.backLogLimit ) m_Log.RemoveAt(0);
		m_Log.Add( str );		
		if (m_Instance == null ){				
			GameObject go = new GameObject("_ViNoBackLog");
			go.hideFlags = HideFlags.HideInHierarchy;
			m_Instance = go.AddComponent<ViNoBackLog>();
			DontDestroyOnLoad(go);
		}
	}
#else
	static public void Add( DialogPartData unit ){
		// If str equals the latest Added String , return.
/*		if( m_Log.Count > 1 ){
			int lastIndex = m_Log.Count - 1;
			if( m_Log[ lastIndex ].Equals( str ) ){
				return;	
			}
		}
//*/		
		if (m_Log.Count > ViNo.backLogLimit ) m_Log.RemoveAt(0);
		m_Log.Add( unit );		
		if (m_Instance == null ){				
			GameObject go = new GameObject("_ViNoBackLog");
			go.hideFlags = HideFlags.HideInHierarchy;
			m_Instance = go.AddComponent<ViNoBackLog>();
			DontDestroyOnLoad(go);
		}
	}	
#endif
	
	static public void Clear(){
		m_Log.Clear();	
	}
	
	static public void ToggleShow( ){
		show = ! show; 	
	}
		
	static public int GetLogTextCount(){
		return m_Log.Count;
	}
	
	static public bool IsLogStored(){
		return ( m_Log.Count > 0 );
	}

	static public string GetLogText( int index ){
		if( index >= m_Log.Count ){
			Debug.LogError( "Index Error. Log Count is " + m_Log.Count.ToString() + " but index is :" + index.ToString() );
			return string.Empty;
		}
		else{
			return m_Log[ index ].dialogText;
		}
	}

	static public DialogPartData GetItemAt( int index ){
		if( index >= m_Log.Count ){
			Debug.LogError( "Index Error. Log Count is " + m_Log.Count.ToString() + " but index is :" + index.ToString() );
			return null;
		}
		else{
			return m_Log[ index ];
		}
	}
		
	static public string GetAppendedText( bool stubBr ){
		System.Text.StringBuilder sb = new System.Text.StringBuilder();
		List<DialogPartData> list = ViNoBackLog.GetLogList();			
		for (int i = 0, imax = list.Count; i < imax; ++i){
			if( list[ i ].isName ){
				sb.Append( list[ i ].nameText );
				if( stubBr ){
					sb.Append( "\n" );					
				}			
			}
			sb.Append( list[ i ].dialogText );
			if( stubBr ){
				sb.Append( "\n" );					
				sb.Append( "\n" );					
			}
		}
		return sb.ToString();	
	}

//	static public List<string> GetLogList( ){
	static public List<DialogPartData> GetLogList( ){
		return m_Log;	
	}

}
