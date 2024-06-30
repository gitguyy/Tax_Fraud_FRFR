using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class MySceneManager : MonoBehaviour
{
    public static MySceneManager Instance;
    public bool hasTransitioned;
    public UnityEngine.Events.UnityEvent transitioned;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }else
        {
            Destroy(gameObject);
        }
        
        DontDestroyOnLoad(gameObject);
    }
    private Scene currentScene;

    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
    }

    void Update()
    {
        Scene newScene = SceneManager.GetActiveScene();
        if (newScene != currentScene)
        {
            transitioned.Invoke();
            Debug.Log("Scene transitioned from " + currentScene.name + " to " + newScene.name);
            currentScene = newScene;
            hasTransitioned = true;
            
            // Add your logic to handle scene transitions here
        }
    }
    
}

