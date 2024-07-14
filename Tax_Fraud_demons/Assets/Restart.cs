using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restart : MonoBehaviour
{
    GameManager gameManager;
    SceneLoader loader;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        loader = gameObject.AddComponent<SceneLoader>();
        gameManager = GameManager.thisManager;
    }

    public void ResetEverything()
    {
        gameManager.Restart();
        loader.loadScene("Level_1");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
