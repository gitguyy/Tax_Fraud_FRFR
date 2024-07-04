using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    [SerializeField]
   
    ProgressBaseObjects[] objects;
    [SerializeField]
    GameObject[] items;
    [SerializeField]
    MySceneManager sceneManager;
    [SerializeField]
    public int[] angerLevels;
    public bool[] itemsPickedUp { private set; get; }
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
        angerLevels = new int[5];
        sceneManager = MySceneManager.Instance;
        dialogue = DialogueSystem.Instance;
        characterDialogue = new int[5];
        characterProgress = new int[5];
      
        objects = FindObjectsOfType<ProgressBaseObjects>();
        for (int i = 0; i < objects.Length; i++)
        {

            objects[i].Initialize();
        }



    }

    private void OnEnable()
    {
        
        itemsPickedUp = new bool[items.Length];
       
    }

    public void setItemPickUp(int ID)
    {
        itemsPickedUp[ID] = true;
      
    }

    // Update is called once per frame
   public void onTransition()
    {
        objects = FindObjectsOfType<ProgressBaseObjects>();
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
        for(int i = 0; i< objects.Length; i++)
        {
            if (objects[i] != null)
            {
                objects[i].SetShow(progressionLevel);
            }
           

        }
    }

    public void progressPlusExit()
    {
        int temp = characterProgress.Length;
        for (int i = 0; i < temp; i++)
        {
            characterProgress[i]++;
        }
        dialogue.ProgressAll(ref characterDialogue);
        progressionLevel++;
        for (int i = 0; i < objects.Length; i++)
        {
            if (objects[i] != null)
            {
                objects[i].SetShow(progressionLevel);
            }


        }
        dialogue.dialogue.onExit();
    }



    
}
