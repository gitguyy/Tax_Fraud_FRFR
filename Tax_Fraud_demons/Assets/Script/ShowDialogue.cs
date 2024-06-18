using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShowDialogue : MonoBehaviour
{
    string t;
    TextMeshProUGUI textShow;
    Camera cam;
    Transform curTransform;
    public Action destroy;
    [SerializeField]
    Vector3 offSet; 
    [SerializeField]
 
    
    void Start()
    {
        t = "";
        textShow = GetComponent<TextMeshProUGUI>();
        textShow.text = t;
      
        
        // Initially hide the dialogue box //ERIH ADD
        //dialogueBox.SetActive(false);
    }

    void Update()
    {
        //check for what transform to show the text at
        textShow.text = t;
        
      
    }
    
    //gets the current string
    public void setText(string _text)
    {
        t = _text;
        //Debug.Log("set Dialogue");
        
      
    }
    public void resetText()
    {
        t = "";
        Debug.Log("reset text");
    }

    //gets the transform of the object the player is talking to
    public void setTransform(Transform _transform)
    {
        if(curTransform != _transform)
        {
            t = "";
           
        }
        curTransform = _transform;
    }
}
