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
    [SerializeField]
    private ItemProgress IsProgress;
    
    [TextArea]
    [SerializeField] private string itemDescription;
    progressionManager manager;
    
    private InventoryManager inventoryManager;
    [SerializeField] private Animator animator;
    private bool isClicked = false;
    
    void Start()
    {
        inventoryManager = InventoryManager.Instance;
        animator = GetComponent<Animator>();
    }
    
    // Check if item has been picked up before
    private void OnEnable()
    {
        manager = progressionManager.Instance;
        if (manager != null)
        {
            if (manager.itemsPickedUp[itemID-1] == true)
            {
                Destroy(this.gameObject);
            }
        }
    }

    // TO COLLIDE WITH ITEMS
    private void OnTriggerStay2D(Collider2D other)
    {
        if(Input.GetMouseButton(0) && other.tag == "Mouse" && !isClicked)
        {
            if(animator != null)
            {
                animator.SetTrigger("Clicked");
            }
            if(animator == null)
            {
                isClicked = true;
                manager.setItemPickUp(itemID - 1);
                if (IsProgress != null)
                {
                    IsProgress.onCLick();
                }
                Destroy(gameObject);
                inventoryManager.AddItem(itemID, itemName, sprite, itemDescription);
            }
                
            

        }
    }

    public void OnAnimationComplete()
    {
        Debug.Log("Animation complete, adding item to inventory.");
        manager.setItemPickUp(itemID-1);
        if(IsProgress != null)
        {
            IsProgress.onCLick();
        }
        inventoryManager.AddItem(itemID, itemName, sprite, itemDescription);
        Destroy(gameObject);
    }
}