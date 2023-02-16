using UnityEngine;
using System.Collections;

public class OnEnablePlayAnimations : MonoBehaviour {
	public float waitSecNext = 0.1f;
	public float waitSecOnIdle = 2f;

	public ViNoGrid grid;
	public GameObject subtile;
	public int divineNum = 8;
	public bool lateTogglePlay;

	private float k_WaitSecondsWhenFinished = 2f;
	private AnimationNode[] cachedAnimations;

	void OnEnable(){
		if( subtile != null ){
			Transform tra = transform;
			grid.widgets = new GameObject[ divineNum ];
			float width = Screen.width / divineNum;

			for(int i=0;i<divineNum;i++){
				GameObject tile = GameObject.Instantiate( subtile ) as GameObject;
				Transform tileTra = tile.transform;
				tileTra.parent = tra;
				tileTra.localPosition = Vector3.zero;
//				tileTra.localScale = Vector3.zero;//new Vector3( width , Screen.height , 1f );

				AnimationNode anim = tile.GetComponent<AnimationNode>();
				anim.amountX  = width;
				anim.amountY  = 1000f;//Screen.height;
				anim.amountZ  = 1f;
//				tile.SetActive( false );				
				grid.widgets[ i ] = tile;
			}

			grid.Reposition();
			tra.localPosition = new Vector3( - 400f, 0f , -100f );
		}

		StartCoroutine( "Playing" );
	}

	IEnumerator Playing(){
		yield return StartCoroutine( "LateActiveGrid" );

		yield return new WaitForSeconds( waitSecOnIdle );

		yield return StartCoroutine( "LateTogglePlayAnimationsAndDestroySelf" );
	}

	IEnumerator LateActiveGrid(){
		// Cache Animations.
		cachedAnimations = new AnimationNode[ grid.widgets.Length ];
		for(int i=0;i<grid.widgets.Length;i++){
			cachedAnimations[ i ] = grid.widgets[ i ].GetComponent<AnimationNode>();
		}

		// Late Activate widgets.
		for(int i=0;i<grid.widgets.Length;i++){
			GameObject widget = grid.widgets[ i ];
//			Debug.Log( "widget name:" + widget.name );
			yield return new WaitForSeconds( waitSecNext );

			widget.SetActive( true );
		}		
	}

	IEnumerator LateTogglePlayAnimationsAndDestroySelf(){
		for(int i=0;i<cachedAnimations.Length;i++){
			AnimationNode anim = cachedAnimations[ i ];

			yield return new WaitForSeconds( waitSecNext );

			if( anim != null ){
				anim.TogglePlay();
			}
		}

		yield return new WaitForSeconds( k_WaitSecondsWhenFinished );

		Destroy( this.gameObject );
	}


}
