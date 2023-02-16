//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
using UnityEngine;
using System.Collections;

[ ExecuteInEditMode]
[ AddComponentMenu( "ViNo/UI/Grid" )]
public class ViNoGrid : MonoBehaviour {
	public enum Direction{
		UP=0,
		RIGHT,
		DOWN,
		LEFT,
	}
	public Direction direction = Direction.RIGHT;
	public bool repositionAndChangeDirection;
	public bool reposition;
	public bool toggleActiveWidgets;

	public float width = 128f;
	public float height = 44f;

	public GameObject[] widgets;

	private bool m_ActiveToggle;

	public void Reposition(){
		if( widgets != null && widgets.Length > 0 ){
			Transform currTra = widgets[ 0 ].transform;				
			for( int i=1;i<widgets.Length;i++){
				Vector3 translation = Vector3.zero;
				switch( direction ){
					case Direction.RIGHT:	translation = new Vector3( width , 0f );	break;						
					case Direction.DOWN:	translation = new Vector3( 0f , - height );	break;						
					case Direction.LEFT:	translation = new Vector3( -width , 0f );	break;						
					case Direction.UP:		translation = new Vector3( 0f , height );	break;						
				}
				widgets[ i ].transform.localPosition = currTra.localPosition + translation;
				currTra = widgets[ i ].transform;
			}
		}		
	}

	public void ChangeDirection(){
		switch( direction ){
			case Direction.RIGHT:	direction = Direction.DOWN;		break;						
			case Direction.DOWN:	direction = Direction.LEFT;		break;						
			case Direction.LEFT:	direction = Direction.UP;		break;						
			case Direction.UP:		direction = Direction.RIGHT;	break;						
		}
	}

	public void SetActiveWidgets( bool t ){
		for(int i=0;i<widgets.Length;i++){
			widgets[ i ].SetActive( t );
		}
	}
	
	void Update(){
		if( reposition ){
			reposition = false;
			Reposition();
		}
		if( repositionAndChangeDirection ){
			repositionAndChangeDirection = false;
			ChangeDirection();
			Reposition();
		}
		if( toggleActiveWidgets ){
			toggleActiveWidgets = false;
			SetActiveWidgets( m_ActiveToggle );
			m_ActiveToggle = ! m_ActiveToggle;
		}
	}

}
