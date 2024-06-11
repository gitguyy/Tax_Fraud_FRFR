using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InterrogationLogic : MonoBehaviour
{
    private InterrogationInformation info;
    private Sprite[] npcSprites;
    private TextAsset text;
    private talkingBehavior t;
    private suspect mySuspect;


    #region Singleton
    static InterrogationLogic Instance;
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    string curText;

    #region Structs(suspect)
    struct suspect
    {
        phase[] phases;  
    }

    struct phase
    {
        string[] text;
    }
    #endregion

    private void Start()
    {
        mySuspect = JsonUtility.FromJson<suspect>(text.ToString());
    }

    public bool checkForClueID(int ID)
    {
        return true;

    }
    
}
