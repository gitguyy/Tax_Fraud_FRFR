using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PlayerOperations : MonoBehaviour
{
    [HideInInspector]
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
        
        if (instance == null) instance = this;
        else Destroy(gameObject.GetComponent<PlayerOperations>()) ;
    }



    // Start is called before the first frame update
    void Start()
    {
        system = DialogueSystem.Instance;
        dialogueBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(canTalkWith && Input.GetMouseButtonDown(0))
        {
            talking = true;
            dialogueBox.SetActive(true);
           

        }
        if(talking)
        {
            talk.Invoke(InteractObject);
        }
        
        if (canInteractWith && !canTalkWith && Input.GetKeyDown(KeyCode.E))
        {
            dialogueBox.SetActive(false);
            Debug.Log("im trying to talk mate");
            
            
        }
        if (!canInteractWith && !canTalkWith && Input.GetMouseButtonDown(0))
        {
            if(InteractObject != null)
            {
                dialogueBox.SetActive(false);
                Debug.Log("im trying to talk mate");
                system.dialogue.onExit(ref system.t);
                system.onTalk(InteractObject);
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
