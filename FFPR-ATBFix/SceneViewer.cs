using Last.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace FFPR_ATBFix
{
    public class SceneViewer
    {
        public List<string> loadedScenes;
        static string[] targetScenes = { "BattleMenu" };//might test more in the future, as this approach can lead to generally cleaner UI
        public SceneViewer()
        {
            Awake();
        }
        public void Awake()
        {
            loadedScenes = new List<string>();
        }
        public void Update()
        {
            foreach(string scene in targetScenes)
            {
                int sceneIndex = isScene_CurrentlyLoaded(scene);
                if (sceneIndex != -1)
                {
                    if (!loadedScenes.Contains(scene))
                    {
                        Scene scn = SceneManager.GetSceneAt(sceneIndex);
                        List<GameObject> gobs = new List<GameObject>(scn.GetRootGameObjects());
                        foreach(GameObject go in gobs)
                        {
                            if(go.name == "RootObject")
                            {
                                //it's the only one for BattleMenu, figure out a better method if we fix the rest of everything
                                BattleUIManager manager = go.GetComponent<BattleUIManager>();
                                Canvas cvs = manager.canvas;
                                cvs.pixelPerfect = true;
                            }
                        }
                    }
                }
                else
                {
                    if (loadedScenes.Contains(scene))
                    {
                        loadedScenes.Remove(scene);
                    }
                }

            }
        }
        int isScene_CurrentlyLoaded(string sceneName_no_extention)
        {
            for (int i = 0; i < SceneManager.sceneCount; ++i)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                if (scene.name == sceneName_no_extention)
                {
                    //the scene is already loaded
                    return i;
                }
            }

            return -1;//scene not currently loaded in the hierarchy
        }
    }
}