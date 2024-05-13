using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleInformation : MonoBehaviour
{
    int length;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(length);
    }
    
    public void updateLength(int _length)
    {
        length = _length;
    }
}
