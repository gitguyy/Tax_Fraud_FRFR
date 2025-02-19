using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DialogueSystem;
using UnityEngine.Events;
using Unity.VisualScripting;
using System.Numerics;
using UnityEditor;

public class talkingBehavior : dialogueEnumerator
{
    public string curText;
    [SerializeField]
    float spellTimer;
    public  Actor actor;
    float curTime;
    int iterator = 0;
    string spelledString = "";
    public int stringID;
    public UnityEvent<int> sendID = new();
    InitializeAnswers init;
    talkingBehavior[] behaviors;
    int[] startIDs;
    int startInt;
    EventManager manager;
    DialogueSystem system;
    progressionManager progress;
    public bool hasExited;
    GameObject box;
    int NPC;


    private void Start()
    {
        startIDs = new int[5];
        progress = progressionManager.Instance;
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





    public void onEnter(Actor _actor, int ID)
    {
        Debug.Log("enter dialogue");
        hasExited = false;
        NPC = ID;
        actor = _actor;


        //progress.giveCharacterProgress(startID);
        Debug.Log("start ID: " + startInt);
        if(progress !=null)
        {
            startInt = progress.characterDialogue[ID];
            startIDs = progress.characterDialogue;
        }
       
      
        
        curText = actor.dialogue[startInt];
        stringID = startInt;
        Debug.Log("string ID: " + stringID);
        iterator = 0;
        spelledString = "";
        
       


      
        
       
    }
    public void backToDialogue()
    {
        hasExited = false;
        if (progress != null)
        {
            startInt = progress.characterDialogue[NPC];
            startIDs = progress.characterDialogue;
           
        }
        Debug.Log("start ID: " + startInt + " string ID: " + stringID);
        

    }
    

    public override void talkUpdate(ref string text)
    {
        
            curText = actor.dialogue[stringID];
        if (checkDialogueType(curText) == DialogueType.Player && hasExited == false)
        {
            //Debug.Log("player is talking");
            showPlayerSprite listener = FindObjectOfType<showPlayerSprite>();
            ShowNPCSprite listenerNPC = FindObjectOfType<ShowNPCSprite>();
            manager.Event.AddListener(listenerNPC.hideNPC);
            manager.Event.AddListener(listener.showSprite);
            manager.RaiseEvent();
            manager.Event.RemoveAllListeners();
            
            
        }
        if (checkDialogueType(curText) == DialogueType.NPC && hasExited == false)
        {
            
            showPlayerSprite listener = FindObjectOfType<showPlayerSprite>();
            ShowNPCSprite listenerNPC = FindObjectOfType<ShowNPCSprite>();
            manager.EventInt.AddListener(listenerNPC.showNPC);
            manager.Event.AddListener(listener.hideSprite);
            manager.RaiseEvent();
            manager.RaiseEvent(system.ID);
            manager.Event.RemoveAllListeners();

        }
        if (iterator < getText().Length && !hasExited)
        {
            text = spellLine(ref curTime, ref iterator, ref spelledString);
        }
        else if(iterator >= getText().Length && !hasExited)
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
            
           
            spelledString = "";
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
        Debug.Log("timer set: " + spellTimer);
    }

    public override void onExit()
    {
        hasExited = true;
        showPlayerSprite listener = FindObjectOfType<showPlayerSprite>();
        Debug.Log("listener" + listener.gameObject.name);
        ShowNPCSprite listenerNPC = FindObjectOfType<ShowNPCSprite>();
        manager.Event.AddListener(listenerNPC.hideNPC);
        manager.Event.AddListener(listener.hideSprite);
        manager.RaiseEvent();
        manager.Event.RemoveAllListeners();
        system.show.setText("");
        startInt = progress.characterDialogue[NPC];

        startIDs = progress.characterDialogue;
        curText = actor.dialogue[startInt];
        stringID = startInt;
        startInt = startIDs[NPC];
        stringID = startInt;
        iterator = 0;
        stringID = startInt;
       
        

        //resetting everything
    }

    public void setBox(GameObject g)
    {
        box = g;
        
    }
        

    public  void onExit(ref string s)
    {
        hasExited = true;
        s = null;
        startInt = progress.characterDialogue[NPC];

        startIDs = progress.characterDialogue;


        //curText = actor.dialogue[startInt];

        showPlayerSprite listener = FindObjectOfType<showPlayerSprite>();
        ShowNPCSprite listenerNPC = FindObjectOfType<ShowNPCSprite>();
        Debug.Log("listener" + listener.gameObject.name);
        manager.Event.AddListener(listenerNPC.hideNPC);
        manager.Event.AddListener(listener.hideSprite);
        manager.RaiseEvent();
        manager.Event.RemoveAllListeners();
        


        stringID = startInt;

        curText = "";
       
        iterator = 0;
        spelledString = "";
      
        sendID.RemoveAllListeners();
        Debug.Log("start after exit: " + startInt);
        




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
    public DialogueType checkDialogueProgress(string type)
    {
        switch (type[1])
        {
            case 'P': return DialogueType.Progress;
            case 'C': return DialogueType.Clue;
            default: return DialogueType.Null;
        }
    }



    public int getNextText(ref int dialogueID)
    {
        //Debug.Log("dialogue: " + actor.dialogue[dialogueID]);
        string checkText = actor.dialogue[dialogueID];
        int temp = dialogueID;
        if (checkDialogueProgress(checkText) == DialogueType.Progress)
        {
            progress.progress();
            //system.ProgressAll(ref startID);
            //progress.giveCharacterProgress(startID);
            Debug.Log("start 1:" + startIDs[0] + "start 2: " + startIDs[1]);
            
            
        }

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
            dialogueID = startInt;
            showPlayerSprite listener = FindObjectOfType<showPlayerSprite>();
            ShowNPCSprite listenerNPC = FindObjectOfType<ShowNPCSprite>();
            manager.Event.AddListener(listenerNPC.hideNPC);
            manager.Event.AddListener(listener.hideSprite);
            manager.RaiseEvent();
            manager.Event.RemoveAllListeners();
            if (box != null)
            {
                box.SetActive(false);
            }
           
            onExit();
            return dialogueID;
            
        }
       
        return temp;
    }


}
