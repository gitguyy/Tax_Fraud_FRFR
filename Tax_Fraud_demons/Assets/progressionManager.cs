using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class progressionManager : MonoBehaviour
{
    #region Variables
    public static progressionManager Instance;
    public int[] characterDialogue;
    public int progressionLevel;
    #endregion;
    #region Singleton

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void progress(int[] charactersToProgress)
    {
        for (int i = 0; i < charactersToProgress.Length; i++)
        {
            characterDialogue[charactersToProgress[i]]++;
        }
        progressionLevel++;
    }
    public void progress(int characterToProgress)
    {
        
        
            characterDialogue[characterToProgress]++;
        
    }
    public void progress()
    {
        int temp = characterDialogue.Length;
        for (int i= 0; i< temp; i++)
        {
            characterDialogue[i]++;
        }
        progressionLevel++;
    }
}
