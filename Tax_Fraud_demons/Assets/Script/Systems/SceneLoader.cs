using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string[] sceneNames;
    
    public void loadScene(string _sceneName)
    {
        SceneManager.LoadScene(_sceneName);
    }

    public string giveSceneNameAt(int _sceneName)
    {
        string name = "huh";

        //check if sceneName is inside of sceneNames array
        if (_sceneName > sceneNames.Length)
        {
            Debug.LogError("sceneName outside of Array");
            return name;
        }
        else
        {
            name = sceneNames[_sceneName];
            return name;
        }
    }
}
