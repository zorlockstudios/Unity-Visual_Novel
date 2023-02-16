//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
using UnityEngine;
using System.Collections;

public class TextButton : SimpleButton{
	public TextMesh selectText;
	public BoxCollider boxCollider;
	public Color activeTextColor = Color.red;
	public Color deactiveTextColor = Color.white;
	public enum ClickTimes{
		ONE=0,
		SECOND,
	}
	public ClickTimes clickTimes = ClickTimes.ONE;

	public delegate void OnClick( GameObject obj );
	public event OnClick onClick;

	private enum State{
		NONE=0,		
		SELECTED,
	}

	private State m_State = State.NONE;
	private string shownText;

	[ HideInInspector ] public GameObject colorObject;

	public void OnInitialize( string text ){
		shownText = text;
	}

	void OnEnable(){
		m_State = State.NONE;
	}

	public override bool OnTouchEnded(){
	 	bool handled = base.OnTouchEnded();
	 	
		if( clickTimes == ClickTimes.ONE ){
		 	if( handled ){
				if( onClick != null ){
					onClick( this.gameObject );
				}
			}
		}
		else{
			string actColStr = ColorUtil.BeginColorTag( activeTextColor );
			string deactColStr = ColorUtil.BeginColorTag( deactiveTextColor );
			string endColTag = ColorUtil.EndColorTag();
			switch( m_State ){
				case State.NONE:
					if ( TouchScreen.selectedGameObject  == this.gameObject ){
						selectText.text = actColStr + shownText + endColTag;
						m_State = State.SELECTED;
					}
					 else {
						selectText.text = deactColStr + shownText + endColTag;
					}
					break;

				case State.SELECTED:
					if ( TouchScreen.selectedGameObject  == this.gameObject ){
						if( onClick != null ){
							onClick( this.gameObject );
						}
					}
					else{
						selectText.text = deactColStr + shownText + endColTag;
						m_State = State.NONE;
					}
					break;
			}
		}
		return true;
	}

}