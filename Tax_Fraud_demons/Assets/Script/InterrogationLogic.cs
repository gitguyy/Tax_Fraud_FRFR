using GameCreator.Runtime.Common;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class InterrogationLogic : MonoBehaviour
{
    #region Variables
    
    private InterrogationInformation info;
    private Sprite[] npcSprites;
    private TextAsset text;
    private sendInfo getInfo;
    
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
        if (sendInfo.Instance != null)
        {
            getInfo = sendInfo.Instance;
            text = getInfo.interrogationInfo.GetText();
        }else
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
        Debug.Log("ID: " + ID);
        Debug.Log("ID in file: " + mySuspect.phases[curPhase].phase.textBlocks[curBlock].block.clue);
        Debug.Log("block: " + curBlock);
        if (mySuspect.phases[curPhase].phase.textBlocks[curBlock].block.clue == ID)
        {
            curClueID = ID;
            Debug.Log("currect clue");
            isAngry = false;
            isCorrectClue = true;
            curTextID = 0;
            curBlock = 0;
            goToNextPhase();
            return;
            
        }
        Debug.Log("wrong clue");
        isAngry = true;
        isCorrectClue = false;
        curClueID = ID;
        

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

    
    public void NextDialogue()
    {
        if(curTextID < mySuspect.phases[curPhase].phase.textBlocks[curBlock].block.dialogue.Length)
        {
           
            curTextID++;
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
        loader.loadScene("Level_1");
        
    }

    public string getText()
    {
        if (isAngry == true)
        {
          
            return mySuspect.phases[curPhase].phase.angryAnswer[curClueID -1].Remove(0, 2);
            
        }
        Debug.Log("id to length ratio: " + mySuspect.phases[curPhase].phase.textBlocks[curBlock].block.dialogue.Length + "/" + curTextID);
        return mySuspect.phases[curPhase].phase.textBlocks[curBlock].block.dialogue[curTextID].Remove(0,2);
    }

    #endregion;
    #region AnimationInformation
    public suspectState returnState()
    {
        switch ((mySuspect.phases[curPhase].phase.textBlocks[curBlock].block.dialogue[curTextID][0]))
        {
            case 'T': return suspectState.Talking;
            case 'C':return suspectState.Contemplating;
            case 'N':return suspectState.Nervous;
            case 'A':return suspectState.Anxious;
            case 'B':return suspectState.Breakdown;
            case 'M': return suspectState.Mad;
            case 'F': return suspectState.Furious;
            default: return suspectState.Null;
        }
           
    }

    #endregion;

}
