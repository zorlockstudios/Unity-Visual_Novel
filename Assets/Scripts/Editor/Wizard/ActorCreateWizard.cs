//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
//          VisualNovelToolkit           /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe. /_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.

using UnityEditor;
using UnityEngine;

namespace ViNoToolkit{

    public class ActorCreateWizard : ScriptableWizard {
        public string actorName;
        public float sizeInPercent = 100f;
        public Color textColor = Color.white;
        public Texture2D[] textures;    

        [MenuItem ("GameObject/Create Actor Data")]
        static void CreateWizard () {
            ScriptableWizard.DisplayWizard<ActorCreateWizard>("Create your Actor", "Create" );
        }

        void OnWizardCreate () {
            EditorUtility.DisplayDialog( "Note" , "Please check that the BG Texture is placed at under a \"Resources/\" folder." , "ok" );

            if( ! System.IO.Directory.Exists( "Assets/Actors") ){
                AssetDatabase.CreateFolder( "Assets" , "Actors" );
            }

        	ActorInfo actor = ScriptableObjectUtility.CreateScriptableObject( "ActorInfo" , "Assets/Actors/" + actorName + ".asset") as ActorInfo;
            ActorUtility.SetUpActorInfo( actor , actorName , sizeInPercent , textColor , textures );
            EditorGUIUtility.PingObject( actor );
        }  
        
    }
}