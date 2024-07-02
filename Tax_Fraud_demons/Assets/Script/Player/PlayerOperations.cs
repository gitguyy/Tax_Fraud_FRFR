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
    DialogueSystem system;

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


    private void OnEnable()
    {
       
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
        if(dialogueBox == null)
        {
            if(FindAnyObjectByType<DialogueBox>()!= null)
            {
               
                dialogueBox = GameObject.FindAnyObjectByType<DialogueBox>().gameObject;
                dialogueBox.gameObject.SetActive(false);
                
            }
            
        }
        if(dialogueBox != null)
        {
            if (canTalkWith && Input.GetMouseButtonDown(0))
            {
                //Debug.Log("talking initiated");
                talking = true;
               
                dialogueBox.SetActive(true);



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

                dialogueBox.SetActive(false);
                system.dialogue.onExit(ref system.t);


            }
            if (!canInteractWith && !canTalkWith && Input.GetMouseButtonDown(0) && dialogueBox.activeSelf == true)
            {
                if (InteractObject != null)
                {
                    talking = false;
                    dialogueBox.SetActive(false);
                    
                    system.dialogue.onExit(ref system.t);
                    system.resetText();
               

                    system.onTalk(InteractObject);
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
