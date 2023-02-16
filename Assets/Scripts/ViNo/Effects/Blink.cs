//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
using UnityEngine;
using System.Collections;

/// <summary>
/// Blink a Object which has MeshRenderer Component .
/// </summary>
[ AddComponentMenu( "ViNo/Effects/Blink" ) ]
public class Blink : MonoBehaviour {
	public GameObject		m_BlinkObject;	
	public MonoBehaviour	m_BlinkComponent;
	public MeshRenderer		m_MeshRenderer;	

//	public ViNoTextBox		textBox;
	public Material			blinkMaterial;	

	public float m_WaitTime = 0.5f;

	private bool _T;
	private float m_ElapsedTime = 0f;
	
	// Update is called once per frame
	void Update () {
		m_ElapsedTime += Time.deltaTime;
		if( m_ElapsedTime > m_WaitTime ){
			m_ElapsedTime = 0f;
			ToggleShow();			
		}
	}

	public Color activeCol = new Color( 1f,1f,1f,1f );
	private Color deactiveCol = new Color( 1f,1f,1f,0f );

	public void DontShow(){
		if( m_BlinkObject != null ){
			m_BlinkObject.SetActive( false );
		}
		if( m_BlinkComponent != null ){
			m_BlinkComponent.enabled = false;
		}			
		if( m_MeshRenderer != null ){
			m_MeshRenderer.enabled = false;			
		}
		if( blinkMaterial != null ){
			blinkMaterial.color = deactiveCol;
		}
	}

	public void ToggleShow(){
		if( m_BlinkObject != null ){
			m_BlinkObject.SetActive( ! m_BlinkObject.activeInHierarchy );
		}
		if( m_BlinkComponent != null ){
			m_BlinkComponent.enabled = ! m_BlinkComponent.enabled;
		}			
		if( m_MeshRenderer != null ){
			m_MeshRenderer.enabled = ! m_MeshRenderer.enabled;					
		}
		if( blinkMaterial != null ){
			if( _T ){				
				blinkMaterial.color = activeCol;//Color.white;
			}
			else{
				blinkMaterial.color = deactiveCol;// Color.black;
			}
			_T = ! _T;
		}

	}

}
