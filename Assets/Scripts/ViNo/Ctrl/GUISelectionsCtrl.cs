//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
using UnityEngine;
using System.Collections;

/// <summary>
/// GUI selections ctrl.
/// </summary>
public class GUISelectionsCtrl : SelectionsCtrl {	
	public Vector2 initialPos = Vector2.zero;

	private float kOffsetY = 50f;
	private float kLineSpaceY = 10f;
	
	/// <summary>
	/// Raises the awake event.
	/// </summary>
	public override void OnAwake( ){
		CreateSelectionInstance();
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
		GameObject selection =	m_SelectionCache[ m_CurrSelectIndex ];
								
		m_CurrSelectIndex++;
		if( m_CurrSelectIndex > base._SELECTION_CACHE_NUM ){
			ViNoDebugger.LogError(  "selection index range error." );	
		}
		
		GUISelection guiSel = selection.GetComponent<GUISelection>();
		guiSel.SetGUIDiglogCtrl( this );		
		guiSel.text = title;
		
		float posY = Screen.height/2f;
		float w = Screen.width/4f;
		float h = 50f;
		posY += + ( m_CurrSelectIndex -1 ) * ( kOffsetY + kLineSpaceY );		
		Vector2 pos = initialPos;
		pos += new Vector2( Screen.width/2f - w/2f , posY );
		guiSel.SetPosition( pos , w , h );
		
		m_SelectionDict.Add( selection.name , new SelectionUnit( title , target ) );		
				
		// Now , Show Selection.
		selection.SetActive( true );		
		
		return selection;
	}	
	
}
