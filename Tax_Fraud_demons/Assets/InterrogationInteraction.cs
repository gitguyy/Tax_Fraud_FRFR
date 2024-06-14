using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InterrogationInteraction : MonoBehaviour
{
    #region Variables
    InterrogationLogic system = new();
    talkingBehavior t = new();
    string text = "";
    string originalText = "";
    [SerializeField]
    bool doneSpelling = true;
    ShowDialogue s;
    
    #endregion
    // Start is called before the first frame update
    
    void Start()
    {
        t= gameObject.GetComponent<talkingBehavior>();
        doneSpelling = true;
        system = InterrogationLogic.Instance;
        s = FindAnyObjectByType<ShowDialogue>();
       
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0)&& doneSpelling)
        {
          
            originalText = system.getText();
            doneSpelling = false;
        }
        
        if (!doneSpelling )
        {
           

            t.spellLineUpdate(ref text, ref doneSpelling, originalText);
            s.setText(text);
            
            if (doneSpelling)
            {
                Debug.Log("got in....somehow");
                system.NextDialogue();

            }
        }


    }
    
}
