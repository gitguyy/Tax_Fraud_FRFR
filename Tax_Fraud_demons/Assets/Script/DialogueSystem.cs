using GameCreator.Runtime.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
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
    [System.Serializable]
    struct ActorSounds
    {
        public List<AudioClip> sounds;
    }

    [SerializeField]
    private TextAsset myText;
    public string t;
    public int ID;
    int dialogueID;
    public static DialogueSystem Instance;
    MySceneManager sceneManager;
    public int prevID;
    int prevSoundMitch = 0;
    int prevSoundNPC = 0;
    

    
    
    
    
    

   

    
    public UnityEvent<int> letters = new();
    ActorCollection myActors;

    public ShowDialogue show;
    public PlayerOperations player;
    public GameObject Bubble;
    public talkingBehavior dialogue;
    public UnityEvent onTransition;

    [SerializeField]
    private ActorSounds[] sounds;
    [SerializeField]
    private ActorSounds mitch;
    AudioSource source;

    

    GameObject curObject;
   

    [SerializeField]
    public float letterTimer;
    public float textSpeedAfterClick;

    [SerializeField]
    Vector3 offSet;
    

    public enum DialogueType
    {
        Player,
        End,
        Start,
        Clue,
        NPC,
        Progress,
        Null
    }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        
        
    }

    private void OnEnable()
    {
        t = myText.text;
        player = PlayerOperations.instance;
        myActors = JsonUtility.FromJson<ActorCollection>(t);
        dialogue = gameObject.GetComponent<talkingBehavior>();
        show = FindAnyObjectByType<ShowDialogue>();
        Debug.Log("name of show dialogue object: " + show.gameObject.name);
      
    }









    // Start is called before the first frame update
    void Start()
    {
        sceneManager = MySceneManager.Instance;
        t = myText.text;
        player = PlayerOperations.instance;
        myActors = JsonUtility.FromJson<ActorCollection>(t);
        //Debug.Log(myActors.actors[0].dialogue[0]);
        dialogue = gameObject.GetComponent<talkingBehavior>();
        ((talkingBehavior)dialogue).player = player;
        dialogue.onEnter(myActors.actors[0],ID) ;
        source = GetComponent<AudioSource>();

    }
    

    public void ProgressAll(ref int[] startPoint)
    {
        int temp = 0;
        foreach(Actor actor in myActors.actors)
        {
            while (dialogue.checkDialogueType(actor.dialogue[startPoint[temp]]) != DialogueType.End)
            {
                startPoint[temp]++;
                
            }
            startPoint[temp]++;
            temp++;
        }
    }

  

   
    
       
  

    void Update()
    {
        if(sceneManager.hasTransitioned)
        {
            Debug.Log("has transitioned dialogueManager");
            player.talk.RemoveAllListeners();
            player.talk.AddListener(onTalk);
            sceneManager.hasTransitioned = false;
            dialogue.onEnter(myActors.actors[0], ID);
        }
        
      
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
            if (curObject != g && temp != prevID)
            {
                prevID = temp;
                dialogue.onEnter(myActors.actors[ID],ID);
                curObject = g;

            }                          
            dialogue.talkUpdate(ref t);
           if(dialogue.checkDialogueType(dialogue.curText) == DialogueType.Player && dialogue.hasExited == false)
            {
                if (!source.isPlaying!)
                {
                    int randomSound = UnityEngine.Random.Range(0, mitch.sounds.Count - 1);
                    if(randomSound == prevSoundMitch)
                    {
                      
                        if(randomSound > 0)
                        {
                            randomSound--;
                           
                        }
                        else if (randomSound < mitch.sounds.Count-1 )
                        {
                            randomSound++;
                           
                        }
                        else
                        {
                            randomSound = UnityEngine.Random.Range(0, mitch.sounds.Count - 1);
                            
                        }
                       
                    }
                    
                    source.clip = mitch.sounds[randomSound];
                    source.PlayOneShot(mitch.sounds[randomSound]);
                    prevSoundMitch = randomSound;

                }
            }
            if (dialogue.checkDialogueType(dialogue.curText) == DialogueType.NPC && dialogue.hasExited == false)
            {
                ActorSounds curSounds = sounds[ID];
                if(!source.isPlaying!)
                {
                    int randomSound = UnityEngine.Random.Range(0, sounds[ID].sounds.Count - 1);
                    if (randomSound == prevSoundNPC)
                    {
                        if (randomSound > 0)
                        {
                            randomSound--;
                        }
                        else if (randomSound < curSounds.sounds.Count - 1)
                        {
                            randomSound++;
                        }
                    }
                    if (randomSound >= curSounds.sounds.Count)
                    {
                        randomSound = 0;
                    }
                    source.clip = curSounds.sounds[randomSound];
                    source.PlayOneShot(curSounds.sounds[randomSound]);
                    //Debug.Log("sound played");
                    prevSoundNPC = randomSound;
                    
                }
            }
            //Debug.Log("spelling");
            if (t.Length != 0)
                    {
                    letters.Invoke(t.Length);
                    }  
                show.setText(t);
        }
    }

    public void resetText()
    {
        if(show == null)
            show = FindAnyObjectByType<ShowDialogue>();
        show.resetText();      
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

   


}
