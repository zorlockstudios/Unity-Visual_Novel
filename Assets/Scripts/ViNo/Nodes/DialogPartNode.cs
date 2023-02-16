//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Dialog part node. 
/// Please Edit the Dialog Text.
/// </summary>
[AddComponentMenu("ViNo/Nodes/Message/Dialog")]
[ System.Serializable ]
public class DialogPartNode : ViNode {	
	public ViNoTextBox m_NameTextBox;
	public ViNoTextBox m_ViNoTextBox;
//	public ScriptableSoundData soundData;

	public string m_NameHndName = "TextBox_Name";
	public string m_DialogHndName = "TextBox";
		
	public TextAsset xmlData;
	
	public List<DialogPartData> dlgDataList;
	
	static private int m_IdCounter = 0;
		
	void Start(){
//		NotEditable();		
	}
	
	/// <summary>
	/// OnEnable and Find TextBox Object.
	/// </summary>
	void OnEnable( ){
// Now , At ScenarioNode OnEnable		
#if false
		ViNoSoundPlayer pl = ISoundPlayer.Instance as ViNoSoundPlayer;
		pl.SetSoundData( soundData );
#endif
		FindTextBoxObjects();
	}
				
	/// <summary>
	/// Creates the Dialog data and return it.
	/// </summary>
	static public DialogPartData CreateData( string name , string text ){
		DialogPartData data = new DialogPartData();
		data.dialogID = m_IdCounter;		
		data.dialogText = text;
		data.nameText = name;
		m_IdCounter++;		
		return data;		
	}
	
	public void ReAssignDialogIDsInThisObject(){
		DialogPartNode[] dlgNodes = gameObject.GetComponents<DialogPartNode>();
		if( dlgNodes != null ){
			int startIndex = 0;
			for( int i=0;i<dlgNodes.Length;i++){				
				dlgNodes[ i ].AssignDialogID( startIndex );
				startIndex = dlgNodes[ i ].GetLastItemDialogID() + 1;
			}
		}		
	}
	
	public void AssignDialogID( int startID ){
		if( dlgDataList == null ){
			return;
		}		
		for( int i=0;i<dlgDataList.Count;i++){
			DialogPartData dlgData = dlgDataList[ i ];
			dlgData.dialogID = startID + i;
		}
	}
		
	/// <summary>
	/// Adds the data.
	/// </summary>
	/// <param name='data'>
	/// Data.
	/// </param>
	public void AddData( DialogPartData data ){
		if( dlgDataList == null ){
			dlgDataList =new List<DialogPartData>();
		}
		dlgDataList.Add( data );			
	}
	
	/// <summary>
	/// Adds the data.
	/// </summary>
	/// <returns>
	/// The data.
	/// </returns>
	/// <param name='name'>
	/// Name.
	/// </param>
	/// <param name='text'>
	/// Text.
	/// </param>
	public DialogPartData AddData( string name , string text ){		
		DialogPartData dpd = CreateData( name , text );		
		AddData( dpd );		
		return dpd;
	}

	public void AddItemAt( int index ){
		DialogPartData data = new DialogPartData();
		if( index - 1 > 0 ){
			DialogPartData prevData = dlgDataList[ index - 1 ];
			data.isName = prevData.isName;
			data.nameText = prevData.nameText;
			data.dialogText = "";
		}
		dlgDataList.Insert( index , data );
	}

	/// <summary>
	/// Finds the text box objects.
	/// </summary>
	public void FindTextBoxObjects( ){
		SystemUIEvent system = GameObject.FindObjectOfType( typeof( SystemUIEvent ) ) as SystemUIEvent;
		if( system != null ){
			m_NameTextBox = system.GetTextBoxBy( m_NameHndName );
			m_ViNoTextBox = system.GetTextBoxBy( m_DialogHndName );
		}
		else{
			GameObject nameTextObj = GameObject.Find( m_NameHndName );
			if( nameTextObj != null ){
				m_NameTextBox = nameTextObj.GetComponent<ViNoTextBox>();
			}

			GameObject textObj = GameObject.Find( m_DialogHndName );
			if( textObj != null ){
				m_ViNoTextBox = textObj.GetComponent<ViNoTextBox>();
			}
		}
	}
	
	public void RemoveItemAt( int index ){
		dlgDataList.RemoveAt( index );
	}
	
	/// <summary>
	/// Triggers the on enter event.
	/// </summary>
	/// <param name='messageID'>
	/// Message I.
	/// </param>
	public void TriggerOnEnterEvent( int messageID ){		
//		TriggerOnEvent( messageID );
	}	

	public void TriggerOnExitEvent (int messageID){
		
	}
	
	public override string SerializeXML( string fileName ){
		string path = ViNoGameSaveLoad.GetDataPath() + "/" + fileName;			
		if( dlgDataList != null ){
			string xmlStr = ViNoGameSaveLoad.SerializeObject<List<DialogPartData>>( dlgDataList );
			ViNoGameSaveLoad.CreateXML( fileName , xmlStr );
		}
		return path;
	}
	
	public override void DeserializeXML( string xmlStr ){
		dlgDataList = (List<DialogPartData>) ViNoGameSaveLoad.DeserializeObject<List<DialogPartData>>( xmlStr );		
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name='sb'>
	/// Sb.
	/// </param>
	public override void ToScenarioScript( ref System.Text.StringBuilder sb ){
		base.ToScenarioScript( ref sb );

		// set current messagelayer name.
//		Dictionary<string,string> actorNameDict = new Dictionary<string,string>();

		sb.Append( "[cm]" );
		sb.Append( System.Environment.NewLine );
		sb.Append( "[current layer=" + m_DialogHndName + "]" );
		sb.Append( System.Environment.NewLine );
		sb.Append( System.Environment.NewLine );

		// DEFINE MACRO !
		int msgNum = GetMessageNum();		
#if false		
		for( int i=0;i<msgNum;i++){
			string actorName = GetNameBy( i );
			if( ! actorNameDict.ContainsKey( actorName ) && ! string.IsNullOrEmpty( actorName ) ){
				actorNameDict[ actorName ] = "[" + actorName + "]";
				sb.Append( "#" + actorNameDict[ actorName ] + ",[settext text=" + actorName + " textbox=" + m_NameHndName + "]" );				
				sb.Append( System.Environment.NewLine );
			}	
		}

		sb.Append( System.Environment.NewLine );
#endif
		for( int i=0;i<msgNum;i++){
			DialogPartData data = dlgDataList[ i ];
			string tag = data.actionID.ToString().ToLower();
			if( data.actionID == DialogPartNodeActionType.Dialog ){
				string dialogText = GetMessageBy( i );
			 	if( data.isName ){
				 	string actorName = GetNameBy( i );
//				 	Debug.Log( "actorName:" + actorName );
				 	if( ! string.IsNullOrEmpty( actorName ) ){
#if false
						sb.Append( actorNameDict[ actorName ] );
#else
						sb.Append( "[settext text=" + actorName + " textbox=" + m_NameHndName + "]" );
#endif						
						sb.Append( System.Environment.NewLine );
					}
				}			
//				sb.Append( " voiceAudioID=" + data.voiceAudioID.ToString() );
//				sb.Append( dialogText + "[l][cm]" );
				sb.Append( dialogText );
				if( data.isClearMessageAfter ){
					sb.Append( "[p]" );
				}
				else{
					sb.Append( "[r]" );					
				}
			}
			else if( data.actionID == DialogPartNodeActionType.EnterActor || data.actionID == DialogPartNodeActionType.MoveActor ){
				for(int k=0;k<data.enterActorEntries.Length;k++){
					DialogPartData.ActorEntry actorEnt = data.enterActorEntries[ k ];
					string actorName = actorEnt.actorName;
				 	if( ! string.IsNullOrEmpty( actorName ) ){					
						sb.Append( "[" + tag + " name=" + actorName + " position=" + actorEnt.position.ToString() + " " );					
						if( actorEnt.withFade ){
							sb.Append( "fade=true" );
						}
						sb.Append( "]" );
						sb.Append( System.Environment.NewLine );
					}
				}
			}
			else if( data.actionID == DialogPartNodeActionType.ExitActor ){
				for(int k=0;k<data.exitActorEntries.Length;k++){					
					DialogPartData.ActorEntry actorEnt = data.exitActorEntries[ k ];
					string actorName = actorEnt.actorName;
				 	if( ! string.IsNullOrEmpty( actorName ) ){					
						sb.Append( "[" + tag + " name=" + actorName + " " );
						if( actorEnt.withFade ){
							sb.Append( "fade=true" );
						}
						sb.Append( "]" );
						sb.Append( System.Environment.NewLine );
					}
				}
			}
/*			else if( data.actionID == DialogPartNodeActionType.ChangeState ){
				for(int k=0;k<data.exitActorEntries.Length;k++){
					DialogPartData.ActorEntry actorEnt = data.exitActorEntries[ k ];
					string actorName = actorEnt.actorName;
				 	if( ! string.IsNullOrEmpty( actorName ) ){					
						sb.Append( "[" + tag + " name=" + actorName + " state=" + actorEnt.state + " " );
						if( actorEnt.withFade ){
							sb.Append( "fade=true" );
						}
						sb.Append( "]" );
						sb.Append( System.Environment.NewLine );
					}
				}
			}
			else if( data.actionID == DialogPartNodeActionType.Shake ){
				for(int k=0;k<data.exitActorEntries.Length;k++){
					DialogPartData.ActorEntry actorEnt = data.exitActorEntries[ k ];
					string actorName = actorEnt.actorName;
				 	if( ! string.IsNullOrEmpty( actorName ) ){					
						sb.Append( "[" + tag + " name=" + actorName + "]" );
						sb.Append( System.Environment.NewLine );
					}
				}
			}
			else if( data.actionID == DialogPartNodeActionType.EnterScene ){
				sb.Append( "[" + tag + " name=" + data.scene.sceneName + " fade=" + data.scene.withFade.ToString().ToLower() + "]" );
				sb.Append( System.Environment.NewLine );				
			}
			else if( data.actionID == DialogPartNodeActionType.ExitScene ){
				sb.Append( "[" + tag + " fade=" + data.scene.withFade.ToString().ToLower() + "]" );
				sb.Append( System.Environment.NewLine );
			}
//*/

			sb.Append( System.Environment.NewLine );
			sb.Append( System.Environment.NewLine );						
		}
	}
		
	/// <summary>
	/// Tos the byte code.
	/// </summary>
	public override void ToByteCode( ByteCodes code ){		
		List<byte> byteList = new List<byte>();
		
		AddNodeCode( byteList );

		string nameHndName = ( m_NameTextBox != null ) ? m_NameTextBox.name : "" ;
		string dialogHndName = ( m_ViNoTextBox != null ) ? m_ViNoTextBox.name : "" ;		
		
		for( int i=0;i<dlgDataList.Count;i++){
			DialogPartData dlgData = dlgDataList[ i ];

// if a item is not Checked , the item is shown.
#if false
			if( ! dlgData.active ){
				continue;
			}			
#endif			
			string tag = GetSingleDialogIDNodeTag( dlgData.dialogID );
			ByteCodeScriptTools.AddNodeCode( byteList , tag );
			
			CodeGenerator.GenerateADialogCode( byteList  , dlgData , nameHndName , dialogHndName );
		}
		
		code.Add( byteList.ToArray() );		
				
		// To Byte codes this Children.
		ToByteCodeInternal( code );
	}	
	
	public override string GetNodeLabel(){
		return GetNodeTag( name );
	}

	// ------------------- Accessor --------------------.

	public DialogPartData GetItemAt( int index ){
		if( dlgDataList == null ){
			return null;	
		}
		
		if( index < dlgDataList.Count ){
			return dlgDataList[ index ];			
		}
		else{
			return null;	
		}
	}
	
	public int GetMessageNum(){
		if( dlgDataList != null ){
			return dlgDataList.Count;
		}
		else{
			return 0;	
		}
	}
			
	public string GetSingleDialogIDNodeTag( int dialogID ){
		return GetNodeTag( name ) + "/ID_" + dialogID.ToString();	
	}

	public string[] GetNodeTags(){
		int msgNum = GetMessageNum();
		msgNum += 1;	// Add MySelf.
		string[] ret = new string[ msgNum ];
		ret[ 0 ] = GetNodeTag( name );		
		for( int i=1;i<msgNum;i++){
			ret[ i ] = GetSingleDialogIDNodeTag( i );			
		}
		return ret;
	}

	public string GetNameBy( int index ){
		if( dlgDataList == null ){
			return string.Empty;	
		}
		
		if( index < dlgDataList.Count ){
			return dlgDataList[ index ].nameText;			
		}
		else{
			return string.Empty;	
		}
	}
		
	public string GetMessageBy( int index ){		
		if( dlgDataList == null ){
			return string.Empty;	
		}
		
		if( index < dlgDataList.Count ){
			return dlgDataList[ index ].dialogText;			
		}
		else{
			return string.Empty;	
		}
	}	

	public int GetLastItemDialogID( ){
		if( dlgDataList != null && dlgDataList.Count > 0  ){
			return dlgDataList[ dlgDataList.Count - 1 ].dialogID;
		}
		else{
			return 0;
		}
	}
		
}
