using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedsProgress : ProgressBaseObjects
{
    // Start is called before the first frame update
    public ChangeObject sendInfo;



    public override void Initialize()
    {
        sendInfo.Initialize();
        SetShow(0);
    }

    


    public override void SetShow(int tryAt)
    {
       
        if(tryAt == changeAt)
        {
            Debug.Log("change: " + gameObject.name);
            sendInfo.onCall();
        }
    }

   
    
}
