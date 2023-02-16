//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
using UnityEngine;
using System.Collections.Generic;


[ System.Serializable ]
public class FlagTable : ScriptableObject {
	[ System.Serializable ]
	public class FlagUnit{
		public string m_FlagName;
		public bool m_IsFlagOn;
	}

	[ System.Serializable ]
	public class VarUnit<T>{
		public string m_FlagName = string.Empty;
		public T theValue;
	}
	
	public FlagUnit[] flags;
	public VarUnit<string>[] stringValues;
	public VarUnit<float>[] floatValues;

	public void SetStringValue( string key , string val ){
		bool exists = false;
		if( stringValues == null ){
			stringValues = new VarUnit<string>[ 1 ];
			stringValues[ 0 ] = new VarUnit<string>();
		}
		for( int i=0;i<stringValues.Length;i++){	
			if( stringValues[ i ].m_FlagName.Equals( key ) ){	
//				Debug.Log( "Set Flag:" + key + "=true" );
				stringValues[ i ].theValue = val;
				exists = true;
				break;
			}
		}
		
		// Create the Flag Unit  If not exists the flag  .
		if( ! exists ){
			List<VarUnit<string>> varlist = new List<VarUnit<string>>();
			for( int i=0;i<stringValues.Length;i++){
				varlist.Add( stringValues[ i ] );				
			}			
			VarUnit<string> unit = new VarUnit<string>();
			unit.m_FlagName = key;
			unit.theValue = val;
			varlist.Add( unit );			
			stringValues = varlist.ToArray();
		}
	}	

	public string GetStringValue( string key ){
		for( int i=0;i<stringValues.Length;i++){	
			if( stringValues[ i ].m_FlagName.Equals( key ) ){	
				return stringValues[ i ].theValue;
			}
		}
		return string.Empty;
	}

	////////////////////////////////////////////////////////////////////////////

	public void ClearFlags(){
		flags = new FlagTable.FlagUnit[ 1 ];
		flags[ 0 ] = new FlagUnit();
		flags[ 0 ].m_FlagName = "";
	}
	
	public string GetFlagNameAt( int index){
		return flags[ index ].m_FlagName;
	}
	
	public string[] GetFlagNames(){
		if( flags != null ){
			List<string> flagNames = new List<string>();
			for( int i=0;i<flags.Length;i++){
				flagNames.Add( flags[ i ].m_FlagName );
			}
			return flagNames.ToArray();
		}
		else{
			return null;
		}
	}
	
	public bool CheckFlagBy( string key ){
		for( int i=0;i<flags.Length;i++){	
			if( flags[ i ].m_FlagName.Equals( key ) ){	
				return flags[ i ].m_IsFlagOn;
			}
		}
		return false;
	}
	
	public void SetFlagBy( string key , bool t ){
		bool exists = false;
		for( int i=0;i<flags.Length;i++){	
			if( flags[ i ].m_FlagName.Equals( key ) ){	
//				Debug.Log( "Set Flag:" + key + "=true" );
				flags[ i ].m_IsFlagOn = t;
				exists = true;
				break;
			}
		}
		
		// Create the Flag Unit  If not exists the flag  .
		if( ! exists ){
			List<FlagUnit> flagList = new List<FlagUnit>();
			for( int i=0;i<flags.Length;i++){
				flagList.Add( flags[ i ] );				
			}
			
			FlagUnit unit = new FlagUnit();
			unit.m_FlagName = key;
			unit.m_IsFlagOn = t;
			flagList.Add( unit );			
			flags = flagList.ToArray();
		}
	}	
	
	
}
