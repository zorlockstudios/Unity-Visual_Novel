//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
//          VisualNovelToolkit           /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe. /_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.

using UnityEditor;
using UnityEngine;

namespace ViNoToolkit{

    public class SceneCreateWizard : ScriptableWizard {
        public string sceneName;
        public Texture2D texture;    

        [MenuItem ("GameObject/Create Scene Data")]
        static void CreateWizard () {
            ScriptableWizard.DisplayWizard<SceneCreateWizard>("Create your Scene", "Create" );
        }

        void OnWizardCreate () {
            EditorUtility.DisplayDialog( "Note" , "Please check that the BG Texture is placed at under a \"Resources/BG/\" folder." , "ok" );

            if( ! System.IO.Directory.Exists( "Assets/Scenes") ){
                AssetDatabase.CreateFolder( "Assets" , "Scenes" );
            }

        	Scene scene = ScriptableObjectUtility.CreateScriptableObject( "Scene" , "Assets/Scenes/" + sceneName + ".asset") as Scene;
            ViNoSceneUtil.SetUpSceneInfo( scene , sceneName , "BG/" + texture.name );

            EditorGUIUtility.PingObject( scene );
        }  
        
    }
}