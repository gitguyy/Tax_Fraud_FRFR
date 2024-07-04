using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InterrogationInteraction : MonoBehaviour
{
    #region Variables
    InterrogationLogic system;
    
    talkingBehavior t;
    string text = "";
    string originalText = "";
    [SerializeField]
    bool doneSpelling = true;
    ShowDialogue s;
    [SerializeField]
    public SuspectAnimator anim;
    
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
        
        
        
        if (!doneSpelling && !system.isAngry)
        {
           

            t.spellLineUpdate(ref text, ref doneSpelling, originalText);
            s.setText(text);
            
            if (doneSpelling)
            {
                
                system.NextDialogue();

            }
        }

        if(!doneSpelling && system.isAngry)
        {
            t.spellLineUpdate(ref text, ref doneSpelling, originalText);
            s.setText(text);
            if (doneSpelling)
            {
              
                system.isAngry = false;
               

            }

        }


    }

    public void spellNextText()
    {
       
        if (doneSpelling)
        {
            originalText = system.getText();
            anim.SetTrigger();
            
            doneSpelling = false;
           
            //spellNextText();
        }
        
    }

    

    void spellPrevioustext()
    {

    }
    
}
