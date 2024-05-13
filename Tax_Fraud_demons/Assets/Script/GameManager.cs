using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    GameManager thisManager;
    SceneLoader loader;
    utilty util;
    
    private void Awake()
    {
        if (FindAnyObjectByType<utilty>() != null)
        {
            Debug.LogError("too many Utils");
        }
        else
        {
            gameObject.AddComponent<utilty>();
            Debug.Log("utility added");
        }
        //check if there is another thisManager active
        if(thisManager != null)
        {
            Debug.LogError("too many gameManagers");
        }
        else
        {
            thisManager = gameObject.GetComponent<GameManager>();
            Debug.Log("manager assigned");
        }
        //load the correct scene
        //give npcs indeces

    }


}
