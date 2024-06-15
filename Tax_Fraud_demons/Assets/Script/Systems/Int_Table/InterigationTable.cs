using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterigationTable : MonoBehaviour
{
    public GameObject interrogationCanvas; // Assign this in the inspector

    void Start()
    {
        if (interrogationCanvas != null)
        {
            interrogationCanvas.SetActive(false); // Ensure the canvas is hidden initially
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetMouseButton(0) && other.CompareTag("Mouse"))
        {
           
            if (interrogationCanvas != null)
            {
                Debug.Log("cmon");
                interrogationCanvas.SetActive(true); // Show the canvas
            }
        }
    }
}