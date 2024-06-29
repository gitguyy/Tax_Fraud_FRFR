using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowObject : ChangeObject
{
    // Start is called before the first frame update
    public override void Initialize()
    {
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    public override void onCall()
    {
        Debug.Log("show Object: " + gameObject.name);
        this.gameObject.SetActive(true);
    }
}
