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


    private void Start()
    {
        Debug.Log("name: " + this.gameObject.name);
        player = PlayerOperations.instance;
      
    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        player.talking = false;

        Debug.Log("collidin");
       
        checkTag(other.tag);
        getObjectInfo(other);
        



    }

   

    private void OnTriggerExit2D(Collider2D other)
    {
        player.canInteractWith = false;
        player.canTalkWith = false;
        Debug.Log("interacatable object is null");

    }


    void checkTag(string tag)
    {
      
        switch (tag)
        {
            case "NPC":

                player.canTalkWith = true;
                player.canInteractWith = true;
                Debug.Log("interacatable object is NPC");
                break;
            case "DOOR":
                player.canTalkWith = false;
                player.canInteractWith = true;
                Debug.Log("interacatable object is DOOR");
                break;
            default:
                Debug.Log("interacatable object is wether door nor npc");
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
