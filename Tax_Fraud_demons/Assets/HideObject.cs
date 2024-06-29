using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideObject : ChangeObject
{
  
    public override void onCall()
    {
        this.gameObject.SetActive(false);
    }

    public override void Initialize()
    {
        this.gameObject.SetActive(true);
    }
}
