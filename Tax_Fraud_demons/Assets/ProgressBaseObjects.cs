using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBaseObjects : MonoBehaviour,IProgressObject
{
    [SerializeField]
    protected int changeAt;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public virtual void onCLick()
    {
        Debug.Log("base progress called");
    }

    public virtual void SetShow(int tryAt)
    {
        
    }

    public virtual void Initialize()
    {

    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
