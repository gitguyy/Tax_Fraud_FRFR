using GameCreator.Runtime.Common;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InterrogationLogic : MonoBehaviour
{
    #region Variables
    
    public InterrogationInformation info {  get; private set; }
    private Sprite[] npcSprites;
    private TextAsset text;
    private sendInfo getInfo;
    bool resetSentence;
    private suspectContainer mySuspectContainer = new();
    private suspect mySuspect = new();
    public bool won;

    private string curText;
    private int curTextID;
    private int curBlock;
    
    private bool isCorrectClue;
    public bool isAngry;

    private int curClueID;
   
    InventoryManager inventoryManager;
    SceneLoader loader;

    public Slider angerSlider;
    public int curAnger;
    public int startAnger;
    progressionManager progManager;
    private int suspectID;

    
    
    UnityEvent<string> sendText;
    public enum suspectState
    {
        Talking,
        Contemplating,
        Nervous,
        Anxious,
        Breakdown,
        Mad,
        Furious,
        Null

    }
    [SerializeField]
    int curPhase;
    
    #endregion
    #region Singleton
    public static InterrogationLogic Instance;
    private void Awake()
    {
        Instance = this;
    }
    #endregion
    #region Structs(Suspect)
    [System.Serializable]
    struct suspectContainer
    {
        public suspect suspect;
    }
    [System.Serializable]
    struct suspect
    {

        public PhaseContainer[] phases;

    }
    [System.Serializable]
    struct PhaseContainer
    {
        public phase phase;
    }
    [System.Serializable]
    struct BlockContainer
    {
        public block block;
    }


    [System.Serializable]
    struct phase
    {
        public string[] angryAnswer;
        public string[] resetSentence;
        public BlockContainer[] textBlocks;
        
        
    }
    [System.Serializable]

   
    struct block
    {
        public string[] dialogue;
        public int clue;
    }
    #endregion
    #region Initializing
    private void OnEnable()
    {
        
        Debug.Log("got called");

        progManager = progressionManager.Instance;
        if (sendInfo.Instance != null)
        {
            Debug.Log("Anger doesnt get set");
            getInfo = sendInfo.Instance;
            info = getInfo.interrogationInfo;
            text = getInfo.interrogationInfo.GetText();
            suspectID = info.ID;
            startAnger = progManager.angerLevels[suspectID];
            curAnger = startAnger;
            GetAnger();

        }
        else
        {
            text = info.GetText();
        }
       

    }

    private void Start()
    {
       
        mySuspectContainer = JsonUtility.FromJson<suspectContainer>(text.ToString());
        mySuspect = mySuspectContainer.suspect;
        inventoryManager = InventoryManager.Instance;
        loader = new();
    }
    #endregion
    #region thirdPartyUsedMethods
    public void checkForClueID(int ID)
    {
        //Debug.Log("ID: " + ID);
        //Debug.Log("ID in file: " + mySuspect.phases[curPhase].phase.textBlocks[curBlock].block.clue);
        //Debug.Log("block: " + curBlock);
        if (mySuspect.phases[curPhase].phase.textBlocks[curBlock].block.clue == ID)
        {
            curClueID = ID;
            Debug.Log("currect clue");
            isAngry = false;
            isCorrectClue = true;
            QuickReset();
            goToNextPhase();
            return;
            
        }
        Debug.Log("wrong clue");
        AngerFilled();
        isAngry = true;
        isCorrectClue = false;
        curClueID = ID;
        
        

    }


    void AngerFilled()
    {
        curAnger++;
        angerSlider.value = curAnger;
        progManager.angerLevels[suspectID] += 1;
        if(curAnger == 10)
        {
            loader.loadScene("Lost");
        }
    }
    void GetAnger()
    {
        Debug.Log("curAnger " + curAnger);
        if(angerSlider == null)
        {
            Debug.Log("angerslider not set...");
        }
        angerSlider.value = curAnger;
        
    }


    public void goToNextPhase()
    {
        if(mySuspect.phases.Length >= curPhase)
        {
            curPhase++;
        }else
        {
            won = true;
        }
        
    }
    public void Update()
    {
        if(angerSlider.value != curAnger)
        {
            angerSlider.value = curAnger;
        }
        
    }


    public void NextDialogue()
    {
        if(curTextID < mySuspect.phases[curPhase].phase.textBlocks[curBlock].block.dialogue.Length)
        {
           if(!resetSentence)
            {
                curTextID++;
            }
           if(resetSentence)
            {
                resetSentence = false;
            }
         
            if(curTextID == mySuspect.phases[curPhase].phase.textBlocks[curBlock].block.dialogue.Length)
            {
                getNextBlock();
            }
           
        }
     
       
    }

    void getNextBlock()
    {
        if (curBlock < mySuspect.phases[curPhase].phase.textBlocks.Length)
        {
            Debug.Log("getting next block");

            curTextID = 0;
            curBlock++;
            if(curBlock == mySuspect.phases[curPhase].phase.textBlocks.Length)
            {
                Reset();
            }
        }
    }
    private void Reset()
    {
        Debug.Log("resetting block");
        curBlock = 0;
        curTextID = 0;
        //loader.loadScene("Level_1");
        
    }

    public void ExitInterrogation()
    {
        loader.loadScene("Level_1");
    }

    private void QuickReset()
    {
        Debug.Log("resetting block");
        curBlock = 0;
        curTextID = 0;
      

    }

    public string getText()
    {
        if (isAngry == true)
        {
            resetSentence = true;
            return mySuspect.phases[curPhase].phase.angryAnswer[curClueID -1].Remove(0, 2);
           
            
        }
        if(resetSentence)
        {
           
            curTextID = 0;
            
            return mySuspect.phases[curPhase].phase.resetSentence[curClueID - 1].Remove(0, 2);
        }

        Debug.Log("id to length ratio: " + mySuspect.phases[curPhase].phase.textBlocks[curBlock].block.dialogue.Length + "/" + curTextID);
        Debug.Log("curTextID: "+curTextID);
        return mySuspect.phases[curPhase].phase.textBlocks[curBlock].block.dialogue[curTextID].Remove(0,2);
    }

    #endregion;
    #region AnimationInformation
    public suspectState returnState()
    {
        if(resetSentence == true && isAngry == false)
        {
            Debug.Log("reset emotions"+ mySuspect.phases[curPhase].phase.resetSentence[curClueID - 1][0]);
            
            switch ((mySuspect.phases[curPhase].phase.resetSentence[curClueID - 1][0]))
            {
                case 'T': return suspectState.Talking;
                case 'C': return suspectState.Contemplating;
                case 'N': return suspectState.Nervous;
                case 'A': return suspectState.Anxious;
                case 'B': return suspectState.Breakdown;
                case 'M': return suspectState.Mad;
                case 'F': return suspectState.Furious;
                default: return suspectState.Null;
            }
        }
        if(isAngry != true)
        {
            switch ((mySuspect.phases[curPhase].phase.textBlocks[curBlock].block.dialogue[curTextID][0]))
            {
                case 'T': return suspectState.Talking;
                case 'C': return suspectState.Contemplating;
                case 'N': return suspectState.Nervous;
                case 'A': return suspectState.Anxious;
                case 'B': return suspectState.Breakdown;
                case 'M': return suspectState.Mad;
                case 'F': return suspectState.Furious;
                default: return suspectState.Null;
            }
        }
        else
            
        switch ((mySuspect.phases[curPhase].phase.angryAnswer[curClueID - 1][0]))
            {
                case 'T': return suspectState.Talking;
                case 'C': return suspectState.Contemplating;
                case 'N': return suspectState.Nervous;
                case 'A': return suspectState.Anxious;
                case 'B': return suspectState.Breakdown;
                case 'M': return suspectState.Mad;
                case 'F': return suspectState.Furious;
                default: return suspectState.Null;
            }


    }

    #endregion;

}
