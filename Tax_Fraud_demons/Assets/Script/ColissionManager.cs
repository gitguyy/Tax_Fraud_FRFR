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

        player.talking = false;

        
       
        checkTag(other.tag);
        getObjectInfo(other);
        



    }

   

    private void OnTriggerExit2D(Collider2D other)
    {
        player.canInteractWith = false;
        player.canTalkWith = false;
      
    }


    void checkTag(string tag)
    {
      
        switch (tag)
        {
            case "NPC":

                player.canTalkWith = true;
                player.canInteractWith = true;
               
               
                break;
            case "DOOR":
                player.canTalkWith = false;
                player.canInteractWith = true;
                
                break;
            default:
                
                break;
            

        }
        
    }

    void getObjectInfo(Collider2D other)
    {
        transformToSend = other.transform;
        gameObjectToSend = other.gameObject;
        setObject.Invoke(gameObjectToSend);
        setTransform.Invoke(transformToSend);
    }

 
      
    

}
