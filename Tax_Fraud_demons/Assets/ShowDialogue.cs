using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowDialogue : MonoBehaviour
{
    
     string t;
    TextMeshProUGUI textShow;
    Camera cam;
    Transform curTransform;
    
    

    // Start is called before the first frame update
    void Start()
    {
        t = "";
        textShow = GetComponent<TextMeshProUGUI>();
        textShow.text = t;
        cam = Camera.main;
        
    }

    // Update is called once per frame
    void Update()
    {
        //check for what transform to show the text at
        if(curTransform != null)
        {
            textShow.transform.position = cam.WorldToScreenPoint(curTransform.position);
            //t = "press E to interact";
           


        }else if(curTransform == null)
        {
            //t = "";
        }

        textShow.text = t;
        
    }
    //gets the current string
    public void setText(string _text)
    {
        
        t = _text;
       
    }
    //gets the transform of the object the player is talking to
    public void setTransform(Transform _transfrom)
    {
        curTransform = _transfrom;
    }

    
}
