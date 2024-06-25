using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class progressionManager : MonoBehaviour
{
    #region Variables
    public static progressionManager Instance;
    public int[] characterDialogue;
    public int[] characterProgress;
    public int progressionLevel;
    private DialogueSystem dialogue;
    #endregion;
    #region Singleton

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        dialogue = DialogueSystem.Instance;
        characterDialogue = new int[2];
        characterProgress = new int[2];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void progress(int[] charactersToProgress)
    {
        for (int i = 0; i < charactersToProgress.Length; i++)
        {
            characterProgress[charactersToProgress[i]]++;
        }
        progressionLevel++;
    }
    public void giveCharacterProgress(int[]progress)
    {
        characterDialogue = progress;
    }
        
    public void progress(int characterToProgress)
    {


        characterProgress[characterToProgress]++;
        
    }
    public void progress()
    {
        int temp = characterProgress.Length;
        for (int i= 0; i< temp; i++)
        {
            characterProgress[i]++;
        }
        dialogue.ProgressAll(ref characterDialogue);
        progressionLevel++;
    }

    
}
