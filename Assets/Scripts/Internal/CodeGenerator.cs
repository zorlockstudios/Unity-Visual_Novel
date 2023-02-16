//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Code generator.
///  Helper Functions about Byte codes script .
/// </summary>
public class CodeGenerator  {

	static private SystemUIEvent m_System;
	static private SystemUIEvent system{
		get{ 	
			if( m_System == null ){
				m_System = GameObject.FindObjectOfType( typeof( SystemUIEvent ) ) as SystemUIEvent; 
			}
			return m_System;
		}
	}

	static public void AddHideMessageCode( List<byte> byteList , string textBoxName ){
		if( system != null ){
			byte id = (byte)system.GetTextBoxIDBy( textBoxName );		
			byteList.Add( Opcode.HIDE_MESSAGE );
			byteList.Add( id );	
		}		
	}

	static public void AddCurrentCode( List<byte> byteList , string textBoxName ){
		if( system != null ){
//			Debug.Log( "AddCurrentCode textBox:" +textBoxName );
			byte id = (byte)system.GetTextBoxIDBy( textBoxName );		
//			Debug.Log( "AddCurrentCode id:" + id.ToString() );
			byteList.Add( Opcode.CURRENT );
			byteList.Add( id );
		}
	}

	static public void AddSetTextCode( List<byte> byteList , string textBoxName ){
		if( system != null ){
			byte id = (byte)system.GetTextBoxIDBy( textBoxName );		
			byteList.Add( Opcode.SET_TEXT );
			byteList.Add( id );
		}
	}

	static public void GenerateWaitCode( List<byte> byteList , float waitSeconds  ){
		ByteCodeScriptTools.AddTextLiteralCode( byteList , waitSeconds.ToString () );
		byteList.Add( Opcode.START_WAIT );	
		byteList.Add( Opcode.UPDATE_WAIT );				
	}
	
	/// <summary>
	/// Generates a Dialog code.
	/// </summary>
	/// <param name='byteList'>
	/// Byte list.
	/// </param>
	/// <param name='isName'>
	/// Is name.
	/// </param>
	/// <param name='isClearMessageAfter'>
	/// Is clear message after.
	/// </param>
	/// <param name='textNameHndStr'>
	/// Text name hnd string.
	/// </param>
	/// <param name='nameText'>
	/// Name text.
	/// </param>
	/// <param name='textDialogHndStr'>
	/// Text dialog hnd string.
	/// </param>
	/// <param name='dialogText'>
	/// Dialog text.
	/// </param>
	static public void GenerateADialogCode( 
		List<byte> byteList , DialogPartData data , string textNameHndStr , string textDialogHndStr ){

// Currently not show.
#if false
		if( data.isClearScene ){
			// Clear Current Shown Scene.
			byteList.Add( Opcode.CLEAR_SCENE );
		}

		if( ! string.IsNullOrEmpty( data.sceneFilePath ) ){
			AddSceneAdditiveCode( byteList , data.sceneFilePath , data.isFadeInStart );
		}
#endif		
/*		else{
			Debug.LogWarning( "Action:Scene data.sceneFilePath must not be null or empty." );
		}
//*/
		switch( data.actionID ){
			case DialogPartNodeActionType.EnterActor:
				for( int i=0;i<data.enterActorEntries.Length;i++){
					DialogPartData.ActorEntry entry= data.enterActorEntries[ i ];				
//					Debug.Log ( "ADDCODE ACTOR:" + entry.actorName );
					Hashtable args = new Hashtable();
					args[ "eventType" ] = data.actionID.ToString().ToLower();	
					args[ "name" ] = entry.actorName;
					args[ "position" ] = entry.position.ToString();							
					args[ "fade" ] = entry.withFade.ToString().ToLower();
					ByteCodeScriptTools.AddTablePairsCode( byteList , args );
					ByteCodeScriptTools.AddMessagingCode( byteList , " " , OpcodeMessaging.TRIGGER_EVENT_WITH_ARGS );
				}
				break;
				
			case DialogPartNodeActionType.MoveActor:
				for( int i=0;i<data.enterActorEntries.Length;i++){
					DialogPartData.ActorEntry entry= data.enterActorEntries[ i ];				
					Hashtable args = new Hashtable();
					args[ "eventType" ] = data.actionID.ToString().ToLower();	
					args[ "name" ] = entry.actorName;
					args[ "position" ] = entry.position.ToString();							
					ByteCodeScriptTools.AddTablePairsCode( byteList , args );
					ByteCodeScriptTools.AddMessagingCode( byteList , " " , OpcodeMessaging.TRIGGER_EVENT_WITH_ARGS );
				}						
				break;
			
			case DialogPartNodeActionType.ExitActor:
				for( int i=0;i<data.exitActorEntries.Length;i++){
					DialogPartData.ActorEntry entry= data.exitActorEntries[ i ];				
					Hashtable args = new Hashtable();
					args[ "eventType" ] = data.actionID.ToString().ToLower();	
					args[ "name" ] = entry.actorName;
					args[ "fade" ] = entry.withFade.ToString().ToLower();
					ByteCodeScriptTools.AddTablePairsCode( byteList , args );
					ByteCodeScriptTools.AddMessagingCode( byteList , " " , OpcodeMessaging.TRIGGER_EVENT_WITH_ARGS );
				}			
				break;
			
			case DialogPartNodeActionType.Dialog:
				AddAudioCode( byteList , data );
			
				if( data.isName ){			
					AddSetTextCode( byteList , data.nameText , textNameHndStr );				
				}
				else{			
					AddHideMessageCode( byteList , textNameHndStr );
				}
				
				AddCurrentCode( byteList ,  textDialogHndStr );
				string str = data.dialogText;	
				if( ! data.isClearMessageAfter ){		
					str += System.Environment.NewLine;
				}	
				ByteCodeScriptTools.AddStringCode( byteList , str );		
				byteList.Add( Opcode.PRINT );
				byteList.Add( Opcode.WAIT_TOUCH );		

				if( data.isClearMessageAfter ){			
					byteList.Add( Opcode.CM );		
				}		
				break;
			
/*			case DialogPartNodeActionType.ChangeState:
				for( int i=0;i<data.exitActorEntries.Length;i++){
					DialogPartData.ActorEntry entry= data.exitActorEntries[ i ];				
					Hashtable args = new Hashtable();
					args[ "eventType" ] = data.actionID.ToString().ToLower();	
					args[ "name" ] = entry.actorName;
					args[ "state" ] = entry.state;
					args[ "fade" ] = entry.withFade.ToString().ToLower();
					ByteCodeScriptTools.AddTablePairsCode( byteList , args );
					ByteCodeScriptTools.AddMessagingCode( byteList , " " , OpcodeMessaging.TRIGGER_EVENT_WITH_ARGS );
				}			
				break;
						
			case DialogPartNodeActionType.Shake:
				for( int i=0;i<data.exitActorEntries.Length;i++){
					DialogPartData.ActorEntry entry= data.exitActorEntries[ i ];				
					Hashtable args = new Hashtable();
					args[ "eventType" ] = data.actionID.ToString().ToLower();	
					args[ "name" ] = entry.actorName;
					ByteCodeScriptTools.AddTablePairsCode( byteList , args );
					ByteCodeScriptTools.AddMessagingCode( byteList , " " , OpcodeMessaging.TRIGGER_EVENT_WITH_ARGS );
				}						
				break;
			case DialogPartNodeActionType.EnterScene:
				Hashtable args = new Hashtable();
				args[ "eventType" ] = data.actionID.ToString().ToLower();	
				args[ "name" ] = data.scene.sceneName;
				args[ "fade" ] = data.scene.withFade.ToString().ToLower();
				ByteCodeScriptTools.AddTablePairsCode( byteList , args );
				ByteCodeScriptTools.AddMessagingCode( byteList , " " , OpcodeMessaging.TRIGGER_EVENT_WITH_ARGS );				
				break;

			case DialogPartNodeActionType.ExitScene:
				 args = new Hashtable();
				args[ "eventType" ] = data.actionID.ToString().ToLower();	
				args[ "name" ] = data.scene.sceneName;
				args[ "fade" ] = data.scene.withFade.ToString().ToLower();
				ByteCodeScriptTools.AddTablePairsCode( byteList , args );
				ByteCodeScriptTools.AddMessagingCode( byteList , " " , OpcodeMessaging.TRIGGER_EVENT_WITH_ARGS );				
				break;
//*/


// Show later update...
#if false
			case DialogPartNodeActionType.Scene:		
				break;
			
			case DialogPartNodeActionType.ClearScene:
				byteList.Add( Opcode.CLEAR_SCENE );
				break;

			case DialogPartNodeActionType.PlayAnimation:
				if( data.isAnim ){
					byteList.Add( Opcode.PLAY_ANIMATION );
					byteList.Add( (byte)data.animationID );			
				}		
				break;

			case DialogPartNodeActionType.ClearScene:
				byteList.Add( Opcode.CLEAR_SCENE );
				break;

			case DialogPartNodeActionType.EnterActor:
				
				break;

			case "Scenario Script":
				AddAudioCode( byteList , data );
				AddCurrentCode( byteList ,  textDialogHndStr );
				KAGInterpreter ip = new KAGInterpreter();
				List<byte> temp = ip.Interpret( data.dialogText );
				for( int i=0;i<temp.Count;i++){
					byteList.Add( temp[ i ] );
				}
				temp = null;
				break;

			case "Selections":
//				if( data.isSelections ){
					ByteCodeScriptTools.AddMessagingCode( byteList , "_" , OpcodeMessaging.SELECTIONS );	
					ByteCodeScriptTools.AddTextLiteralCode( byteList , "_" );		
					byteList.Add( Opcode.SELECTIONS );
		//			for( int i=0;i<units.Length;i++){
		//				SelectionsNode1.SelectUnit unit = units[ i ];
		//				CodeGenerator.GenerateASelection( byteList , i , data.targetNodeName , unit.text , false , "_" );
		//			}
						CodeGenerator.GenerateASelection( byteList , 0 , data.selection.targetNodeName , data.selection.text , false , "_" );

					byteList.Add( Opcode.STOP );			
//				}
				break;
#endif		
		}
	}

	static public void AddSetTextCode( List<byte> byteList , string text, string textNameHndStr ){
		if( ! string.IsNullOrEmpty( textNameHndStr ) ){
			ByteCodeScriptTools.AddTextLiteralCode( byteList , text );
			AddSetTextCode( byteList , textNameHndStr );
		}
	}

	static private void AddAudioCode( List<byte> byteList , DialogPartData data ){
		ISoundPlayer pl = ISoundPlayer.Instance;
		if( pl as ViNoSoundPlayer ){
			if( data.isBGM ){
				byteList.Add( Opcode.PLAY_SOUND );
				byteList.Add( 0 );	// 0: BGM.
				byteList.Add( (byte)data.bgmAudioID );
			}

			if( data.isSE ){
				byteList.Add( Opcode.PLAY_SOUND );
				byteList.Add( 1 );	// 1: SE.
				byteList.Add( (byte)data.seAudioID );
			}

			if( data.isVoice ){
				byteList.Add( Opcode.PLAY_SOUND );
				byteList.Add( 2 );	// 2: VOICE.
				byteList.Add( (byte)data.voiceAudioID );
			}
			else{
				byteList.Add( Opcode.STOP_VOICE );				
			}
		}
		else if( pl as SimpleSoundPlayer ){			
			if( data.isBGM ){
				// Loop.
				AddPlaySoundCode( byteList , "playbgm" , data.bgmAudioKey , "true" );				
/*				
				Hashtable args = new Hashtable();
				args[ "eventType" ] = "playbgm";
				args[ "storage" ] = data.bgmAudioKey;		
				args[ "loop" ] = "true";		
				ByteCodeScriptTools.AddTablePairsCode( byteList , args );
				ByteCodeScriptTools.AddMessagingCode( byteList , " " , OpcodeMessaging.TRIGGER_EVENT_WITH_ARGS );			
//*/				
			}			

			if( data.isSE ){
				AddPlaySoundCode( byteList , "playse" , data.seAudioKey , "false" );
			}			

			if( data.isVoice ){
				AddPlaySoundCode( byteList , "playvoice" , data.voiceAudioKey , "false" );
			}			
		}
	}

	static public void AddPlaySoundCode( List<byte> byteList , string tag , string audioPath , string loop ){
		Hashtable args = new Hashtable();
		args[ "eventType" ] = tag;
		args[ "storage" ] = audioPath;
		args[ "loop" ] = loop;		
		ByteCodeScriptTools.AddTablePairsCode( byteList , args );
		ByteCodeScriptTools.AddMessagingCode( byteList , " " , OpcodeMessaging.TRIGGER_EVENT_WITH_ARGS );
	}

	static public void AddDialogEventArgs( List<byte> byteList , string eventType , int messageID , string nodeName , string dialogText ){
		Hashtable args = new Hashtable();
		args[ "eventType" ] = eventType;		
		args[ "ID" ] = messageID.ToString();		
		args[ "nodeName" ] = nodeName;		
		args[ "dialogText" ] = dialogText;
		
		ByteCodeScriptTools.AddTablePairsCode( byteList , args );
		ByteCodeScriptTools.AddMessagingCode( byteList , nodeName , OpcodeMessaging.TRIGGER_EVENT_WITH_ARGS );
	}

	static public void GenerateSelections( List<byte> byteList , SelectionsNode.SelectUnit[] units , string title ){		
		if( ISelectionsCtrl.Instance == null ){
			ISelectionsCtrl selObj = GameObject.FindObjectOfType( typeof( ISelectionsCtrl )) as ISelectionsCtrl;
			if( selObj == null ){
				Debug.LogError( "ISelectionsCtrl Not Exists." );
				return;						
			}
		}
		if( ! SelectionsNode.IsTargetSet( units ) ){
			return;
		}

		ByteCodeScriptTools.AddMessagingCode( byteList , "Dialog" , OpcodeMessaging.SELECTIONS );	
		ByteCodeScriptTools.AddTextLiteralCode( byteList , title );		
		byteList.Add( Opcode.SELECTIONS );
		for( int i=0;i<units.Length;i++){
			if( units[ i ].targetNode != null ){
				SelectionsNode.SelectUnit unit = units[ i ];
								
				string targetFunc = units[ i ].targetNode.GetNodeLabel();//Tag(  unit.targetNode.name );
				CodeGenerator.GenerateASelection( byteList , i , targetFunc , unit.text , unit.checkFlag , unit.flagName );
			}
		}
		byteList.Add( Opcode.STOP );
	}		
	
	static public void GenerateASelection( List<byte> byteList , int index ,  string targetFuncName , string text , bool checkFlag , string flagName  ){						
		Hashtable param = new Hashtable();
		param[ "target" ]		= targetFuncName;
		param[ "text" ]			= text;
		if( checkFlag ){
			param[ "IsFlagOn" ]	= flagName;
		}
		ByteCodeScriptTools.AddTablePairsCode( byteList , param );
		byteList.Add( Opcode.LINK );
//		ByteCodeScriptTools.AddMessagingCode( byteList , "Select-" + index.ToString() , OpcodeMessaging.LINK );
	}	

}
