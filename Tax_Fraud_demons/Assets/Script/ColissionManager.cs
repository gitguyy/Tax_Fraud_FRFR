using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ColissionManager : MonoBehaviour
{
    PlayerOperations player;
    public UnityEvent<Transform> setTransform;
    public UnityEvent<GameObject> setObject;
    Transform transformToSend;
    GameObject gameObjectToSend;

    private void OnEnable()
    {
        
    }
    private void Start()
    {
        setObject.AddListener(PlayerOperations.instance.setInteract);
       
        player = PlayerOperations.instance;
        if(player == null)
        {
            player = PlayerOperations.instance;
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        Debug.Log("Entered");

        transformToSend = other.transform;
        gameObjectToSend = other.gameObject;

        checkTag(other.tag);
        
        



    }

   

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("exited");
        player.canInteractWith = false;
        player.canTalkWith = false;
      
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        checkTag(other.tag);
        transformToSend = other.transform;
        gameObjectToSend = other.gameObject;
    }


    public void checkTag(string tag)
    {
      
        switch (tag)
        {
            case "NPC":
                Debug.Log("NPC");
                player.canTalkWith = true;
                player.canInteractWith = true;
               
               
                break;
            case "DOOR":
                Debug.Log("DOOR");
                player.canTalkWith = false;
                player.canInteractWith = true;
                
                break;
            default:
                
                break;
            

        }
        
    }

    public void getObjectInfo()
    {
       
        setObject.Invoke(gameObjectToSend);
        setTransform.Invoke(transformToSend);
    }

 
      
    

}
