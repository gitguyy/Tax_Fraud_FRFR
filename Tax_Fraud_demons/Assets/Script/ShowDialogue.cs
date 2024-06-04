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
    private GameObject dialogueBox; // Reference to the DialogueBox GameObject //ERIH ADD
    
    void Start()
    {
        t = "";
        textShow = GetComponent<TextMeshProUGUI>();
        textShow.text = t;
        cam = Camera.main;
        
        // Initially hide the dialogue box //ERIH ADD
        dialogueBox.SetActive(false);
    }

    void Update()
    {
        //check for what transform to show the text at
        textShow.text = t;
        
        // Toggle dialogue box visibility based on whether there's text //ERIH ADD
        dialogueBox.SetActive(!string.IsNullOrEmpty(t));
    }
    
    //gets the current string
    public void setText(string _text)
    {
        t = _text;
        
        // Toggle dialogue box visibility based on whether there's text //ERIH ADD
        dialogueBox.SetActive(!string.IsNullOrEmpty(t));
    }
    
    //gets the transform of the object the player is talking to
    public void setTransform(Transform _transform)
    {
        if(curTransform != _transform)
        {
            t = "";
            dialogueBox.SetActive(false); // Hide dialogue box when switching target //ERIH ADD
        }
        curTransform = _transform;
    }
}
