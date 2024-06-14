using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class InterrogationLogic : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private InterrogationInformation info;
    private Sprite[] npcSprites;
    private TextAsset text;
    
    private suspectContainer mySuspectContainer = new();
    private suspect mySuspect = new();
    public bool won;

    private string curText;
    private int curTextID;
    private int curBlock;
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
        text = info.GetText();
        mySuspectContainer = JsonUtility.FromJson<suspectContainer>(text.ToString());
        mySuspect = mySuspectContainer.suspect;
        inventoryManager = InventoryManager.Instance;
        
    }

    private void Start()
    {
        
    }
    #endregion
    #region thirdPartyUsedMethods
    public bool checkForClueID(int ID)
    {
        if (mySuspect.phases[curPhase].phase.textBlocks[curBlock].block.clue == ID)
        {
            return true;
        }
        return false;
        

    }

    public void goToNextPhase()
    {
        if(mySuspect.phases.Length > curPhase)
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
