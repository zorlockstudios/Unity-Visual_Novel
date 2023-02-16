//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
using UnityEngine;
using System.Collections;

/// <summary>
/// Vi no text mesh event listener.
/// </summary>
[ ExecuteInEditMode ]
[ AddComponentMenu( "ViNo/Event/TextMeshEvent" ) ]
public class ViNoTextMeshEventListener : ViNoTextEventListener{

	public enum TextEffect{
		NONE=0,
		SHADOW,
		OUTLINE,
	}

// . TEST !!!
//	public int lineWidth = 400;
	public Color baseTextColor = Color.white;
	public TextEffect textEffect = TextEffect.NONE;
	public Vector2 textEffectOffset = new Vector2( 2f , -2f );

	public Color effectTextColor = Color.black;
	public TextMesh textMesh;

	[ HideInInspector ] public TextMesh[] m_OutLineTextMeshes; // OutLine TextMeshes.		
	[ HideInInspector ] public bool m_ReceiveTextSetEvent;
	[ HideInInspector ] public ColorPanel[] m_TintPanels;	
	[ HideInInspector ] public string m_BaseColHexStr = "";
	[ HideInInspector ] public string m_TextEffectColHexStr = "";

	private System.Text.StringBuilder m_Sb;
	private System.Text.StringBuilder sb{
		get{
			if( m_Sb == null ){ m_Sb = new System.Text.StringBuilder(); }
			return m_Sb;
		}
	}

	public class FontSettings{
		public TextEffect effect;
		public Vector2 textEffectOffset = new Vector2( 2f , -2f );
		public Color effectTextColor = Color.black;

		// TextMesh properties.
		public float characterSize = 5.5f;
		public float lineSpacing;
		public TextAnchor anchor;
		public TextAlignment alignment;
		public float tabSize;
		public bool richText;
		public Font font;
		public FontStyle fontStyle ;
		public int fontSize = 25;
	}	

//	private FontSettings m_FontSettings = new FontSettings();

	void Start(){}

	public void ChangeTextEffect( TextEffect ef ) {
		textEffect = ef;
		CommitChanges();
	}

	public override void ResetFont(){

	}

	public override void OnPropertyChange( Hashtable param ){
		TextEffect effect = textEffect;

		bool foundEffectAttr = false;
		bool shadow = false;		
		if( param.ContainsKey( "shadow") ){
			shadow = ( param["shadow"] as string ) == "true" ? true : false;
			if( shadow ){
				effect = TextEffect.SHADOW;
			}
			foundEffectAttr = true;
		}

		bool edge = false;
		if( param.ContainsKey( "edge") ){
			edge = ( param["edge"] as string ) == "true" ? true : false;
			if( edge ){
				effect = TextEffect.OUTLINE;
			}
			foundEffectAttr = true;
		}

		if( shadow || edge ){
			ChangeTextEffect( effect );
		}
		else{
			if( foundEffectAttr ){
				ChangeTextEffect( TextEffect.NONE );
			}
		}

		if( shadow && edge ){
			ChangeTextEffect( TextEffect.OUTLINE );
		}

		if( param.ContainsKey( "edgecolor") ){

		}
		if( param.ContainsKey( "shadowcolor") ){

		}
		
		if( param.ContainsKey( "size") ){
			int size = int.Parse( param["size"] as string );
			textMesh.fontSize = size;
			CreateTextEffectObject();
		}

		if( param.ContainsKey( "color") ){

		}

		if( param.ContainsKey( "italic") ){

		}

// TODO:
		if( param.ContainsKey( "rubysize") ){

		}
		if( param.ContainsKey( "rubyoffset") ){

		}

		if( param.ContainsKey( "bold") ){

		}

	}

	public override void CommitChanges(){
		// Get Hex String  ( Like #ff00ff ) from baseTextColor for Html rich Text Color.
		m_BaseColHexStr = ColorUtil.GetHexColorString( baseTextColor );
		m_TextEffectColHexStr = ColorUtil.GetHexColorString( effectTextColor );
		CreateTextEffectObject();	

		if( textMesh != null ){
			string stripped = ColorUtil.GetHtmlTagStrippedText( textMesh.text );
			SetTextMeshText( stripped );
		}		
	}

	public override void Enabled(){				
//		Debug.Log( "Enabled "+name );
		if( ! Application.isPlaying ){
			CommitChanges();
		}
	}

	/// <summary>
	/// Raises the update text event.
	/// </summary>
	/// <param name='theText'>
	/// The text.
	/// </param>
	public override void OnUpdateText( string theText){
//		Debug.Log( "theText:" + theText );
		SetTextMeshText( theText );
	}

	/// <summary>
	/// Raises the text set event.
	/// </summary>
	/// <param name='theText'>
	/// The text.
	/// </param>
	public override void OnTextSet( string theText ){
		SetTextMeshText( theText );
	}

	public override void OnDisplayedText(){
		base.OnDisplayedText();
	}

	void CreateTextEffectObject(){
		if( textEffect != TextEffect.NONE ){
			if( m_OutLineTextMeshes == null || m_OutLineTextMeshes.Length != 4 ){
				m_OutLineTextMeshes = new TextMesh[ 4 ];
			}
		}
		
		switch( textEffect ){
			case TextEffect.SHADOW:
				SpawnShadowTextMesh( new Vector3(   textEffectOffset.x ,   textEffectOffset.y , 0.001f ) , 0 );
				SpawnShadowTextMesh( new Vector3( - textEffectOffset.x ,   textEffectOffset.y , 0.001f ) , 1 );
				SpawnShadowTextMesh( new Vector3( - textEffectOffset.x , - textEffectOffset.y , 0.001f ) , 2 );
				SpawnShadowTextMesh( new Vector3(   textEffectOffset.x , - textEffectOffset.y , 0.001f ) , 3 );
				
				m_OutLineTextMeshes[ 1 ].gameObject.SetActive( false );
				m_OutLineTextMeshes[ 2 ].gameObject.SetActive( false );
				m_OutLineTextMeshes[ 3 ].gameObject.SetActive( false );
				break;

			case TextEffect.OUTLINE:
				SpawnShadowTextMesh( new Vector3(   textEffectOffset.x ,   textEffectOffset.y , 0.001f ) , 0 );
				SpawnShadowTextMesh( new Vector3( - textEffectOffset.x ,   textEffectOffset.y , 0.001f ) , 1 );
				SpawnShadowTextMesh( new Vector3( - textEffectOffset.x , - textEffectOffset.y , 0.001f ) , 2 );
				SpawnShadowTextMesh( new Vector3(   textEffectOffset.x , - textEffectOffset.y , 0.001f ) , 3 );
				break;

			case TextEffect.NONE:
				if( m_OutLineTextMeshes != null && m_OutLineTextMeshes.Length > 0 ){
					for(int i=0;i<m_OutLineTextMeshes.Length;i++){
						if( m_OutLineTextMeshes[ i ] != null ){
							m_OutLineTextMeshes[ i ].gameObject.SetActive( false );
						}
					}
				}
				break;
		}		
	}

	void SetTextMeshFontToOutlines( TextMesh tm ){
	 	MeshRenderer ren = tm.gameObject.GetComponent<MeshRenderer>();
	 	ren.sharedMaterial = textMesh.font.material;
		tm.characterSize = textMesh.characterSize;
		tm.lineSpacing = textMesh.lineSpacing;
		tm.anchor = textMesh.anchor;
		tm.alignment = textMesh.alignment;
		tm.tabSize = textMesh.tabSize;
		tm.richText = textMesh.richText;
		tm.font = textMesh.font;
		tm.fontStyle = textMesh.fontStyle;
		tm.fontSize = textMesh.fontSize;
		tm.gameObject.layer = textMesh.gameObject.layer;
	}

	private TextMesh SpawnShadowTextMesh( Vector3 offset , int index ){
		TextMesh tm = null;
		GameObject textMeshObject = null;
		if( m_OutLineTextMeshes != null ){
			if( m_OutLineTextMeshes[ index ] != null ){
				tm = m_OutLineTextMeshes[ index ];
				textMeshObject = tm.gameObject;
			}
		}		
		if( tm == null ){
			textMeshObject = new GameObject( "_TextMesh");
			tm = textMeshObject.AddComponent<TextMesh>();
			textMeshObject.AddComponent<MeshRenderer>();		

			m_OutLineTextMeshes[ index ] = tm;
		}		
		textMeshObject.transform.parent = textMesh.transform;
		textMeshObject.transform.localPosition = offset;
		textMeshObject.transform.localScale = Vector3.one;
		m_OutLineTextMeshes[ index ].gameObject.SetActive( true );

		SetTextMeshFontToOutlines( tm );
		return tm;
	}
	
	private void SetTextMeshText( string theText ){
		textMesh.text = GetColoredString( m_BaseColHexStr , theText , hasPromptImageLast );//sb.ToString();
		if( m_OutLineTextMeshes != null ){
			for( int i=0;i<m_OutLineTextMeshes.Length;i++){
				if( m_OutLineTextMeshes[ i ] != null ){
					m_OutLineTextMeshes[ i ].text = GetColoredString( m_TextEffectColHexStr , theText , hasPromptImageLast );// sb.ToString();//"<color=" + m_TextEffectColHexStr + ">" + theText + "</color>";
				}
			}
		}		
	}

	private string GetColoredString( string colHexStr ,  string theText , bool hasImageLast ){
		sb.Length = 0;
		sb.Append( "<material=0>" );
		sb.Append( "<color=" );
		sb.Append( colHexStr );
		sb.Append( ">" );
		sb.Append( theText );		
		sb.Append( "</color>" );
		sb.Append( "</material>" );
		if( Application.isPlaying && m_TextBox != null ){
			if( m_TextBox.IsUpdate() ){
				sb.Append( "<material=1></material>" );			
			}
			else{
				if( hasImageLast ){

					sb.Append( "<quad material=1 x=0 y=0 width=1 height=1 size=" + textMesh.fontSize + " />" );
				}
				else{
					sb.Append( "<material=1></material>" );			
				}
			}
		}
		else{
			sb.Append( "<material=1></material>" );						
		}
		return sb.ToString();
	}	

	IEnumerator RequestCharactersInFontTexture( string reqStr ){
		if( textMesh != null && textMesh.font != null ){
			textMesh.font.RequestCharactersInTexture( reqStr );
		}

		yield return new WaitForEndOfFrame();
	}

}

