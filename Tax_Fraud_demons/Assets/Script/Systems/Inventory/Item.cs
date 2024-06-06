using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private string itemName;
    [SerializeField] private Sprite sprite;
    [SerializeField] private int itemID; // Add itemID
    [TextArea]
    [SerializeField] private string itemDescription;
    
    //[SerializeField] private int quantity; if needed quantitfy prob not
    private InventoryManager inventoryManager;
    
    void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }
    
    // Getter for itemID
    public int GetItemID()
    {
        return itemID;
    }
    
     //TO COLLIDE WITH ITEMS
    private void OnCollisionEnter2D(Collision2D collision)
    {
        inventoryManager.AddItem(itemID, itemName, sprite, itemDescription);
        Destroy(gameObject);
    }
}
