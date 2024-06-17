using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;
public class InitializeAnswers : MonoBehaviour
{
    talkingBehavior t;
    [SerializeField]
    GameObject textObject;

    string dialogue;
    int amnt;
    string curText;

    [SerializeField]
    Vector3 offset;
    UnityEvent<string> sendText;
    public ShowDialogue dia;
    public Transform player;

    int ID;
    public int i;
    bool initializing;

    private Camera cam;
    int counting;
    List<showAnswers> copies = new();
    

    GameObject parentAnswer;
    int answerID;
    PlayerOperations playerOp;

    public DialogueSystem dialogueSys;
    
    // Start is called before the first frame update
    void Start()
    {
        t = gameObject.AddComponent<talkingBehavior>();
        cam = Camera.main;
        parentAnswer = FindAnyObjectByType<Canvas>().gameObject;
        playerOp = PlayerOperations.instance;
    }

    // Update is called once per frame
    void Update()
    {
      if(initializing && counting < amnt)
        {
            if(counting != 0)
            {
                Vector3 vector2 = copies[counting - 1].copyBubble.GetComponent<BubbleInformation>().giveSize();
                Vector3 finalChange = new Vector3(0, -vector2.y, vector2.z);
               
                copies[counting].setTransform(copies[counting -1].copyBubble.transform.position + finalChange);
            }
            
            copies[counting].startTalking = true;
            if (copies[counting].answered)
            {
                copies[counting].startTalking = false;
                counting++;
                
            }
           
            
            
        }
      

      
    }

    public void Initialize()
    {
        EventManager e = EventManager.Instance;
        //e.OnEvent.AddListener(this.getAnswerID);
        copies.Clear();
        amnt = 0;
        t.getActor();
        //amnt = t.getAnswerAmount(ID);
        counting = 0;
         
        //Debug.Log(t.getAnswerAmount(ID));
        for (int i = 1; i <= amnt; i++)
        {
            GameObject copy;
            copy = Instantiate(textObject, cam.WorldToScreenPoint(player.transform.position + offset), Quaternion.identity);
            copy.GetComponent<showAnswers>().setString(t.actor.dialogue[ID + i].Remove(0, 2));
            copy.transform.SetParent(GameObject.FindAnyObjectByType<Canvas>().transform);
            copy.GetComponent<showAnswers>().setID(i);
            //Debug.Log("Copy ID " + copy.GetComponent<showAnswers>().answerID);
            copy.GetComponent<TextMeshProUGUI>().text = string.Empty;
            copy.transform.IsChildOf(parentAnswer.transform);
            
           
            copies.Add(copy.GetComponent<showAnswers>());
            
            
        }

        Debug.Log("list length " + copies.Count);
        initializing = true;
        
        
       





    }

    void setDialogue(string setDialogue)
    {
        dialogue = setDialogue;
    }

    public void setID(int _ID)
    {
        ID = _ID;
        if(!initializing)
        {
            Initialize();
        }
        
            
        
        
        
    }

    public void getAnswerID(int _answer)
    {
        answerID = _answer;
        //Debug.Log("got in " +answerID);

        emptyList();
        Debug.Log("list length "+ copies.Count);
        
        int start = dialogueSys.dialogue.stringID;
        Debug.Log("initial string: " + dialogueSys.dialogue.actor.dialogue[start]);
        


        //while(dialogueSys.dialogue.checkDialogueType(dialogueSys.dialogue.actor.dialogue[start]) != DialogueSystem.DialogueType.NewText)
        //{

        //    //Debug.Log("got into the thingy for: " + temp +"Times " +"answerID is " + answerID + "startAmount is " + startAmnt);
        //    //Debug.Log(dialogueSys.dialogue.actor.dialogue[start]);
        //    temp++;
        //    start++;
           
        //    if (dialogueSys.dialogue.checkDialogueType(dialogueSys.dialogue.actor.dialogue[start]) == DialogueSystem.DialogueType.Start)
        //    {
        //        Debug.Log("hah");
        //        startAmnt++;
        //        start++;
        //    }
           
        //    if (startAmnt == answerID)
        //    {
               
               
                
        //           // Debug.Log(start - dialogueSys.dialogue.stringID);
        //            dialogueSys.dialogue.stringID = start;
                    
        //            //Debug.Log("Heyo S" + dialogueSys.dialogue.actor.dialogue[start]);
                
               
             

                
        //        break;
        //    }
        //}


        

        //initializing = false;
        //t.actor.dialogue[0] = null;
        //playerOp.talking = true;
        //EventManager e = EventManager.Instance;
        //e.OnEvent.RemoveListener(this.getAnswerID);




    }

    public void emptyList()
    {
        for (int i = 0; i < copies.Count; i++)
        {
            copies[i].selfTerminate();
        }
        copies.Clear();
    }

   
}
