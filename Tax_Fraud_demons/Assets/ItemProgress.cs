using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemProgress : MonoBehaviour,IProgressObject
{
    progressionManager p;
    private void Start()
    {
        p = progressionManager.Instance;
    }

    public void onCLick()
    {
        p.progress();
    }
    public void setShow()
    {

    }
    

}
