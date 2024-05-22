using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DialogueBubble : MonoBehaviour
{
    Transform thisTransform;
    Vector2 letterSize;
    Vector2 collumnSize;
    public BubbleInformation data;
    [SerializeField]
    float damp;
    BoxCollider2D col;
    public bool hovering;
 

    //neeed a way to initialize each individual letterBlock
    

    
    // Start is called before the first frame update
    void Start()
    {
        data = gameObject.GetComponent<BubbleInformation>();
        thisTransform = this.transform;
        if(gameObject.GetComponent<BoxCollider2D>() !=  null)
        {
            col = gameObject.GetComponent<BoxCollider2D>();
        }
       
       
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(LerpSize().x, LerpSize().y, thisTransform.localScale.z);
        if (col != null)
        {
            col.size = new Vector3(LerpSize().x, LerpSize().y, thisTransform.localScale.z);
        }
    }

    private void OnMouseEnter()
    {
        hovering = true;
    }

    private void OnMouseExit()
    {
        hovering = false;
    }

    void IncreasesSize(int letter)
    {

    }

    Vector2 LerpSize()
    {
        Vector2 temp = Vector2.Lerp(transform.localScale, data.giveSize(), damp);
        return temp;
    }
    


}
