using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Events;

public class PlayerOperations : MonoBehaviour
{

    public bool canTalkWith;
    [HideInInspector]
    public bool canInteractWith;
    public UnityEvent<GameObject> talk;
    GameObject InteractObject;
    UnityEvent interact;
    public bool talking;
    public GameObject bubble;
    public static PlayerOperations instance;
    [SerializeField]
    private GameObject dialogueBox;
    public ColissionManager col;
    DialogueSystem system;
    [SerializeField]
    private GameObject pressableArea;
    
    

    private void Awake() // To make this class a singleton, there is only a single static instance in your scene
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keep the GameObject persistent across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
        //instance = this;
        //else Destroy(gameObject.GetComponent<PlayerOperations>()) ;
    }

    public void onTransition()
    {
        pressableArea = GameObject.Find("pressableArea");
    }


    private void OnEnable()
    {
        pressableArea.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        dialogueBox = GameObject.FindAnyObjectByType<DialogueBox>().gameObject;
        
        system = DialogueSystem.Instance;
        dialogueBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(pressableArea != null)
        {
            if (dialogueBox.activeSelf == false)
            {
                pressableArea.SetActive(false);
            }
        }
      
        if (dialogueBox == null)
        {
            if(FindAnyObjectByType<DialogueBox>()!= null)
            {
               
                dialogueBox = GameObject.FindAnyObjectByType<DialogueBox>().gameObject;
                dialogueBox.gameObject.SetActive(false);
                
            }
            
        }
        if(dialogueBox != null)
        {
            if (talking && Input.GetMouseButtonDown(0) && canTalkWith)
            {
                Debug.Log("set timer_PLayerOperations");
                system.dialogue.setTimer(system.textSpeedAfterClick);
            }
            if (canTalkWith && Input.GetMouseButtonDown(0) &&!talking)
            {
               
                if(system == null)
                {
                    system = DialogueSystem.Instance;
                }
                pressableArea.SetActive(true);
                

                col.getObjectInfo();
                if (pressableArea.GetComponent<NpcInformations>() != null && InteractObject.GetComponent<NpcInformations>() != null)
                {
                   
                    int ID = InteractObject.GetComponent<NpcInformations>().getID();
                    //Debug.Log(ID + "name: " +InteractObject.gameObject.name);
                    pressableArea.GetComponent<NpcInformations>().setID(ID);

                }
                else
                    //Debug.Log("no info" + InteractObject.gameObject.name);
                
                //Debug.Log("talking initiated");
                system.dialogue.backToDialogue();
                talking = true;
                system.dialogue.setTimer(system.letterTimer);
               
                dialogueBox.SetActive(true);
               

                system.dialogue.setBox(dialogueBox);



            }

            if (talking)
            {
                talk.Invoke(InteractObject);
              

            }
            
            if (Input.GetMouseButtonDown(0))
            {
              
            }

            if (canInteractWith && !canTalkWith && Input.GetMouseButtonDown(0))
            {
                Debug.Log("exit_1");
                dialogueBox.SetActive(false);
                pressableArea.SetActive(false);
                system.dialogue.onExit(ref system.t);
                


            }
            if (!canInteractWith && !canTalkWith && Input.GetMouseButtonDown(0) && dialogueBox.activeSelf == true)
            {
                if (InteractObject != null)
                {
                    Debug.Log("exit_2");
                    talking = false;
                    dialogueBox.SetActive(false);
                    pressableArea.SetActive(false);

                    system.dialogue.onExit(ref system.t);
                    system.resetText();
               

                    //system.onTalk(InteractObject);
                }



            }
        }
        
        
    }
  

    public void setInteract(GameObject _object)
    {
        InteractObject = _object;
    }

    public void setTalking(bool _talking)
    {
        talking = _talking;
    }

    
}
