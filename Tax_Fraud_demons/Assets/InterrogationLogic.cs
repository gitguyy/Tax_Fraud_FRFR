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
    private talkingBehavior t;
    private suspect mySuspect;
    private string curText;
    private int curTextID;
    InventoryManager inventoryManager;
    ShowDialogue  d;
    int curBlock;
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
    static InterrogationLogic Instance;
    private void Awake()
    {
        Instance = this;
    }
    #endregion
    #region Structs(Suspect)
    struct suspect
    {
       public phase[] phases; public string[] angryAnswer;
    }

    struct phase
    {
        public textBlock[] block;
        
        
    }

    struct textBlock
    {
        public string[] dialogue;
        public int clue;
    }
    #endregion

    private void OnEnable()
    {
        text = info.GetText();
        mySuspect = JsonUtility.FromJson<suspect>(text.ToString());
        inventoryManager = InventoryManager.Instance;
        d = GameObject.FindAnyObjectByType<ShowDialogue>();
    }
    #region thirdPartyUsedMethods
    public bool checkForClueID(int ID)
    {
        if (mySuspect.phases[curPhase].block[curBlock].clue == ID)
        {
            return true;
        }
        return false;
        

    }

    public void goToNextPhase()
    {
        curPhase++;
    }

    public string getText()
    {
        return mySuspect.phases[curPhase].block[curBlock].dialogue[curTextID].Remove(0,2);
    }

    #endregion;
    #region AnimationInformation
    public suspectState returnState()
    {
        switch ((mySuspect.phases[curPhase].block[curBlock].dialogue[curTextID][0]))
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
