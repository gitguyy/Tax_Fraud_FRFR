using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcInformations : MonoBehaviour
{
    private int ID = 0;
    bool isTalking;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setID(int _ID)
    {
        ID = _ID;
    }

    public int getID()
    {
        return ID;
    }
}
