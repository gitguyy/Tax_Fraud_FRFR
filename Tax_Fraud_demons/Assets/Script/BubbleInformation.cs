using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleInformation : MonoBehaviour
{
    int curlength;
    int lineLength = 25;
    int lineAmnt = 0;
    [SerializeField]
    float xSegmentSize = 0.3f;
    [SerializeField]
    float ySegmentSize = 0.8f;
    Vector2 finalSize;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (newLine(curlength))
        {
            lineAmnt++;
           
            curlength = 0;

        }
    }
    
    public void updateLength(int _length)
    {
        if(_length <= 0)
        {
            lineAmnt = 0;
        }
        curlength = _length -(lineLength* lineAmnt);

        //Debug.Log("length:" + curlength + "culumns:" + lineAmnt);


    }

    public bool newLine(int amnt)
    {
        if (amnt >= lineLength)
        {
            return true;
        }
        return false;
       
    }

    public Vector2 giveSize()
    {
        if(lineAmnt == 0)
        {
            return new Vector2(curlength * xSegmentSize, ySegmentSize);
        }
        else
        {
            return new Vector2(lineLength * xSegmentSize, ySegmentSize + (lineAmnt * ySegmentSize));
        }
    }
}
