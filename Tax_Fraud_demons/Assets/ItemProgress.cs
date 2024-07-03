using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemProgress : ProgressBaseObjects
{
    
    progressionManager p;
    [SerializeField]
    ChangeObject sendInfo;
    private void Start()
    {
        p = progressionManager.Instance;
        
        
    }
    public override void Initialize()
    {
        sendInfo.Initialize();
        SetShow(0);
    }

    public override void onCLick()
    {
        p.progressPlusExit();
    }
    public override void SetShow(int tryAt)
    {
        if(tryAt == changeAt)
        {
            sendInfo.onCall();
        }
    }
    

}
