using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ToLeaveCanvas : MonoBehaviour
{
    public GameObject interrogationCanvas; // Assign this in the inspector
    
    public void CloseCanvas()
    {
        if (interrogationCanvas != null)
        {
            interrogationCanvas.SetActive(false); // Hide the canvas
        }
    }
}