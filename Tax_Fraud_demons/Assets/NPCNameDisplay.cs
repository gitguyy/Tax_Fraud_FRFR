using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCNameDisplay : MonoBehaviour
{
    // Reference to the GameObject that displays the NPC's name
    public GameObject npcName;

    // Layer to check for the player
    public LayerMask playerLayer;

    // Start is called before the first frame update
    void Start()
    {
        // Ensure the NPC name is initially deactivated
        if (npcName != null)
        {
            npcName.SetActive(false);
        }
    }

    // This function is called when another Collider2D enters the trigger
    void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & playerLayer) != 0) // Check if the collider belongs to the player layer
        {
            if (npcName != null)
            {
                npcName.SetActive(true);
            }
        }
    }

    // This function is called when another Collider2D exits the trigger
    void OnTriggerExit2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & playerLayer) != 0) // Check if the collider belongs to the player layer
        {
            if (npcName != null)
            {
                npcName.SetActive(false);
            }
        }
    }
}