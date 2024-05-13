using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

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
    

    private int ID;
    private int dialogueID;

    int iterator;
    float time;
    string spelledString;
    public UnityEvent<int> letters;
    ActorCollection myActors;

    public ShowDialogue show;
    public PlayerOperations player;
    
   
    [SerializeField]
    private float letterTimer;

    public enum DialogueType
    {
        Question,
        Answer,
        Text,
        Null
    }

    
   




    // Start is called before the first frame update
    void Start()
    {
        t = myText.text;
        myActors = JsonUtility.FromJson<ActorCollection>(t);
        Debug.Log(myActors.actors[0].dialogue[0]);
        
    }

    

    public string getText()
    {
        return t;
    }

    public void onTalk(GameObject g)
    {
        

      
        if(g.GetComponent<NpcInformations>() != null)
        {
            //update the text
            

            int temp = g.GetComponent<NpcInformations>().getID();
            ID = temp;
            if(curText().Length > iterator)
            {
                t = spellLine(curText(), letterTimer, ref time, ref iterator, ref spelledString);
                letters.Invoke(t.Length);
                
               // Debug.Log("text: " + curText());
                show.setText(t);
                
            }else if(curText().Length <= iterator)
            {
                Debug.Log("reset" + curText().Length);
                t = curText();
                show.setText(t);
                iterator = 0;
                spelledString = "";
                getNextText(ref dialogueID);
                player.setTalking(false);
                
               
            }

            
           
            
        }

        
    }
    //takes the starting id of where in the dialogue we currently are
    public string curText()
    {


        return myActors.actors[ID].dialogue[dialogueID].Remove(0,2);



    }
    
    public char curLetter(int thisChar, int thisLine)
    {
        return myActors.actors[ID].dialogue[thisLine].ToCharArray()[thisChar];
    }
    //just ignore all the references needed i assure you its not spaghetti future me(rewrite it)

    public string spellLine(string spellString, float timeBetweenLetters, ref float curtime, ref int iterator, ref string spelledString)
    {
        curtime += Time.deltaTime;
        
        if(curtime >= timeBetweenLetters)
        {
            spelledString += spellString.ToCharArray()[iterator];
            iterator++;
            curtime = 0;
            return spelledString;
        }

        return spelledString;
        
        
    }

    
    //gets the next text

    public int getNextText(ref int dialogueID)
    {

        string checkText = myActors.actors[ID].dialogue[dialogueID];
        int temp = dialogueID;
        if(checkDialogueType(checkText) == DialogueType.Text)
        {
            dialogueID++;
            return temp++;
        }
        if(checkDialogueType(checkText) == DialogueType.Answer)
        {
            //do answer logic thingy
            return temp;
        }
        if(checkDialogueType(checkText) == DialogueType.Question)
        {
            // do question logic

            return temp;
        }
        return temp;
    }

    public DialogueType checkDialogueType(string type)
    {

        switch (type[0])
        {
            case 'Q': return DialogueType.Question;
            case 'A': return DialogueType.Answer;
            case 'T': return DialogueType.Text;
            default: return DialogueType.Null;


        }

    }

    public int checkAnswer()
    {
        return 0;
    }






}
