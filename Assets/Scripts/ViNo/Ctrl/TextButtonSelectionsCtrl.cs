//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
using UnityEngine;
using System.Collections;

/// <summary>
/// Text button selections ctrl.
/// </summary>
public class TextButtonSelectionsCtrl : SelectionsCtrl {
	public TextButton[] textButtons;	// Set TextButtons in inspector.
	
	/// <summary>
	/// Raises the awake event.
	/// </summary>
	public override void OnAwake(){

	}

	/// <summary>
	/// Adds the selection.
	/// </summary>
	/// <returns>
	/// The selection.
	/// </returns>
	/// <param name='title'>
	/// Title.
	/// </param>
	/// <param name='target'>
	/// Target.
	/// </param>
	public override GameObject AddSelection( string title , string target ){
		TextButton sel = textButtons[ m_CurrSelectIndex ];
		sel.onClick += OnClickSelectCallback;
		sel.OnInitialize( title );
		sel.selectText.text = title;

		m_CurrSelectIndex++;
		if( m_CurrSelectIndex > base._SELECTION_CACHE_NUM ){
			ViNoDebugger.LogError(  "selection index range error." );	
		}
		
//		Debug.Log( "AddSel:" + sel.name );
		m_SelectionDict.Add( sel.name , new SelectionUnit( title , target ) );		
				
		// Now , Show Selection.
		sel.gameObject.SetActive( true );		
		
		return sel.gameObject;
	}	
	
	/// <summary>
	/// Deactivates the selections.
	/// </summary>
	public override void DeactivateSelections(){
		for( int i=0;i<textButtons.Length;i++){
			if( textButtons[ i ] != null ){
				textButtons[ i ].gameObject.SetActive( false );
			}
		}
	}	

	
}
