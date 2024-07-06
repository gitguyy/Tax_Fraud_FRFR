using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;

public class Item : MonoBehaviour
{
    [SerializeField] private string itemName;
    [SerializeField] private Sprite sprite;
    [SerializeField] private int itemID;
    [SerializeField] private ItemProgress IsProgress;
    [TextArea]
    [SerializeField] private string itemDescription;
    [SerializeField] private AudioClip pickUpSound; // Add this line to specify the pick-up sound
    [SerializeField] [Range(0f, 1f)] private float pickUpVolume = 1f; // Add this line to control the volume
    
    private progressionManager manager;
    private InventoryManager inventoryManager;
    private Animator animator;
    private AudioSource audioSource; // Add this line to manage the audio source
    private bool isClicked = false;
    
    void Start()
    {
        
        inventoryManager = InventoryManager.Instance;
        animator = GetComponent<Animator>();
        audioSource = gameObject.GetComponent<AudioSource>(); // Add this line to add an audio source component
        audioSource.loop = false;
    }
    
    // Check if item has been picked up before
    private void OnEnable()
    {
        
        manager = progressionManager.Instance;
        if (manager != null)
        {
            if (manager.itemsPickedUp[itemID - 1] == true)
            {
                Destroy(this.gameObject);
            }
        }
        if(audioSource == null)
        {
            audioSource = gameObject.GetComponent<AudioSource>(); // Add this line to add an audio source component
        }
        
        audioSource.loop = false;
    }

    // TO COLLIDE WITH ITEMS
    private void OnTriggerStay2D(Collider2D other)
    {
        if(Input.GetMouseButton(0) && other.tag == "Mouse" && !isClicked)
        {
            if(animator != null)
            {
                isClicked = true;
                 // Play sound when animation starts
                animator.SetTrigger("Clicked");
            }
            else
            {
                PlayPickUpSound();
                PickUpItem();
            }
        }
    }

    private void PlayPickUpSound()
    {
        Debug.Log("sound played");
        if (pickUpSound != null)
        {
            audioSource.PlayOneShot(pickUpSound, pickUpVolume);
        }
    }

    private void PickUpItem()
    {
        manager.setItemPickUp(itemID - 1);
        if (IsProgress != null)
        {
            IsProgress.onCLick();
        }
        Destroy(gameObject);
        inventoryManager.AddItem(itemID, itemName, sprite, itemDescription);
    }

    public void OnAnimationComplete()
    {
        Debug.Log("Animation complete, adding item to inventory.");
        PickUpItem();
    }
}