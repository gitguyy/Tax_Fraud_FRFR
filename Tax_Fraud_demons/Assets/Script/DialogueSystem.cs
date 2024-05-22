using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SearchService;

public class DialogueSystem : MonoBehaviour
{
    


    [System.Serializable]
    public struct Actor
    {
        public string[] dialogue;
    }
    [System.Serializable]
    public struct ActorCollection
    {
        public Actor[] actors;
    }

    [SerializeField]
    private TextAsset myText;
    private string t;
    int ID;
    int dialogueID;

    
    
    
    
    

   

    
    public UnityEvent<int> letters = new();
    ActorCollection myActors;

    public ShowDialogue show;
    public PlayerOperations player;
    public GameObject Bubble;
    public talkingBehavior dialogue;

    GameObject curObject;
    GameObject curBubble;

    [SerializeField]
    private float letterTimer;

    [SerializeField]
    Vector3 offSet;
    

    public enum DialogueType
    {
        Question,
        Answer,
        Text,
        End,
        Start,
        Clue,
        NewText,
        Null
    }

    
   




    // Start is called before the first frame update
    void Start()
    {
        
        t = myText.text;
        player = PlayerOperations.instance;
        myActors = JsonUtility.FromJson<ActorCollection>(t);
        Debug.Log(myActors.actors[0].dialogue[0]);
        dialogue = gameObject.AddComponent<talkingBehavior>();
        ((talkingBehavior)dialogue).player = player;
        show.destroy += DestroyBubble;
        

    }

    private void Update()
    {
        
    }



    public string getText()
    {
        return t;
    }

    public void onTalk(GameObject g)
    {

        


      
        if(g.GetComponent<NpcInformations>() != null)
        {
            ((talkingBehavior)dialogue).setTimer(letterTimer);

            //update the text


            int temp = g.GetComponent<NpcInformations>().getID();
            ID = temp;
            
            
           


            if (curObject != g )
            {
                dialogue.onEnter(myActors.actors[ID]);
                if (curBubble != null)
                {
                    DestroyBubble();
                    Debug.Log("reset");
                    ((talkingBehavior)dialogue).onExit(ref t);

                }
                
                
                    
                    curBubble = instantiateBubble(g);
                   
                
                curObject = g;

            }
            
            
            

                
                dialogue.talkUpdate(ref t);
                //Debug.Log("spelling");
               
                if(t.Length != 0)
                letters.Invoke(t.Length);
            
                
                
               
                show.setText(t);
                
            
            

            
           
            
        }

        
    }
    //takes the starting id of where in the dialogue we currently are
   

   
    
   
    //just ignore all the references needed i assure you its not spaghetti future me(rewrite it)

   

    
    //gets the next text

   

   

    

    private GameObject instantiateBubble(GameObject g)
    {
        GameObject copy = Instantiate(Bubble, g.transform.position +offSet, Quaternion.identity);
        letters.AddListener(copy.GetComponent<BubbleInformation>().updateLength);
        return copy;
        
        
    }

    public void DestroyBubble()
    {
        Destroy(curBubble);
        ((talkingBehavior)dialogue).onExit(ref t);




    }

}
