using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DialogueSystem;
using UnityEngine.Events;
using Unity.VisualScripting;
using System.Numerics;

public class talkingBehavior : dialogueEnumerator
{
    string curText;
    float spellTimer;
    public Actor actor;
    float curTime;
    int iterator = 0;
    string spelledString = "";
    public int stringID;
    public UnityEvent<int> sendID = new();
    InitializeAnswers init;
    talkingBehavior[] behaviors;
    int startID;
    EventManager manager;
    DialogueSystem system;


    private void Start()
    {
        manager = EventManager.Instance;
        system = DialogueSystem.Instance;
    }



    public PlayerOperations player {  get;  set; }

    

    public void getActor()
    {
        actor.dialogue = null;
        if (GameObject.FindObjectOfType<talkingBehavior>() != null)
        {
            behaviors = FindObjectsOfType<talkingBehavior>();
            foreach (talkingBehavior t in behaviors)
            {
                if (t.actor.dialogue != null)
                {
                    actor = t.actor;
                    break;
                }
            }
           // Debug.Log("got actor, dialogue is :"+ actor.dialogue[0]);

        }
    }





    public  override void onEnter(Actor _actor)
    {
        Debug.Log("onEnter");
        startID = 0;
        actor = _actor;
        curText = actor.dialogue[0];
        iterator = 0;
        spelledString = "";
        stringID = 0;
       


        init = FindObjectOfType<InitializeAnswers>();
       
            //Debug.Log("initializing answer");
        
        //sendID.AddListener(init.setID);
        
       
    }
    public override void onEnter()
    {
        //get the currentLine
        //spell the line that is given

    }

    public override void talkUpdate(ref string text)
    {
        
            curText = actor.dialogue[stringID];
        if (checkDialogueType(curText) == DialogueType.Player)
        {
            Debug.Log("player is talking");
            showPlayerSprite listener = FindObjectOfType<showPlayerSprite>();
            ShowNPCSprite listenerNPC = FindObjectOfType<ShowNPCSprite>();
            manager.Event.AddListener(listenerNPC.hideNPC);
            manager.Event.AddListener(listener.showSprite);
            manager.RaiseEvent();
            
            
        }
        if (checkDialogueType(curText) == DialogueType.NPC)
        {
            Debug.Log("NPC is talking");
            showPlayerSprite listener = FindObjectOfType<showPlayerSprite>();
            ShowNPCSprite listenerNPC = FindObjectOfType<ShowNPCSprite>();
            manager.EventInt.AddListener(listenerNPC.showNPC);
            manager.Event.AddListener(listener.hideSprite);
            manager.RaiseEvent();
            manager.RaiseEvent(system.ID);
            
        }
        if (iterator < getText().Length)
        {
            text = spellLine(ref curTime, ref iterator, ref spelledString);
        }
        else
        {
            text = getText();
            getNextText(ref stringID);
            spelledString = "";
            iterator = 0;
            player.setTalking(false);
        }
       
    }

    public string spellLine(ref float curtime, ref int iterator, ref string spelledString)
    {
        curtime += Time.deltaTime;

        if (curtime >= spellTimer)
        {
            spelledString += getText().ToCharArray()[iterator];
            iterator++;
            curtime = 0;
            return spelledString;
        }

        return spelledString;


    }
    public string spellLine(ref float curtime, ref int iterator, ref string spelledString, string spellString)
    {
        curtime += Time.deltaTime;

        if (curtime >= spellTimer)
        {
            spelledString += spellString.ToCharArray()[iterator];
            iterator++;
            curtime = 0;
            return spelledString;
        }

        return spelledString;


    }

    public void spellLineUpdate(ref string spellString, ref bool done, string original)
    {

        
        if (iterator < original.Length)
        {
            spellString = spellLine(ref curTime, ref iterator, ref spelledString, original);
        }
        else
        {
            
            //getNextText(ref stringID);
            //spelledString = "";
            iterator = 0;
            done = true;
          
        }
    }

     string getText()
    {
        

        
        
            return actor.dialogue[stringID].Remove(0, 2);
        
        





    }



    public void setTimer(float timer)
    {
        spellTimer = timer;
    }

    public override void onExit()
    {

        curText = null;
        iterator = 0;
        stringID = 0;
        Debug.Log("exited dialogue");

        //resetting everything
    }

    public  void onExit(ref string s)
    {

        s = null;
       
        stringID = 0;
        startID = 0;
       if(actor.dialogue[0]!= null)
        {
            curText = actor.dialogue[0];
        }
       
        iterator = 0;
        spelledString = "";
        stringID = 0;
        sendID.RemoveAllListeners();

       


        //resetting everything
    }


    public DialogueType checkDialogueType(string type)
    {

        switch (type[0])
        {
            case 'N':return DialogueType.NPC;
            case 'P': return DialogueType.Player;
            case 'C': return DialogueType.Clue;
            case 'E':return DialogueType.End;
            case 'S': return DialogueType.Start;
            default: return DialogueType.Null;


        }

    }

    public int getAnswerAmount()
    {
        int temp;
        temp = stringID;
        while (checkDialogueType(actor.dialogue[temp]) != DialogueType.Start)
        {
            temp++;
        }
        return temp;
    }
    public int getAnswerAmount(int _stringID)
    {
        int temp = 0;
        
        while (checkDialogueType(actor.dialogue[_stringID + temp]) != DialogueType.Start)
        {
            temp++;
        }
        return temp -1;
    }

    public int getNextText(ref int dialogueID)
    {
        Debug.Log("dialogue: " + actor.dialogue[dialogueID]);
        string checkText = actor.dialogue[dialogueID];
        int temp = dialogueID;
        if (checkDialogueType(checkText) == DialogueType.Player)
        {

          
                dialogueID++;
            



            return temp++;
        }
        if (checkDialogueType(checkText) == DialogueType.NPC)
        {


           
            dialogueID++;
            //showNpc.Invoke(DialogueSystem.instance.ID);



            return temp++;
        }


      
        if (checkDialogueType(checkText) == DialogueType.End)
        {
            // do question logic
            dialogueID = startID;

           
            return dialogueID;
        }
       
        return temp;
    }


}
