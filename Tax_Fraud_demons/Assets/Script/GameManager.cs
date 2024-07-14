using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager thisManager;
    SceneLoader loader;
    utilty util;
    InventoryManager inventoryManager;
    progressionManager progression;
    
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
        if(thisManager == null)
        {
            thisManager = this;
        }else
        {
            Destroy(this);
        }
        //load the correct scene
        //give npcs indeces

    }
    private void Start()
    {
        inventoryManager = InventoryManager.Instance;
        progression = progressionManager.Instance;
    }

    public void Restart()
    {
        inventoryManager.EmptyInventory();
        progression.RestartProgress();
    }


}
