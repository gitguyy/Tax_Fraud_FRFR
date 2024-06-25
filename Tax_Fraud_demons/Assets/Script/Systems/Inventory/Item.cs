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
    
    //[SerializeField] private int quantity; if needed quantitfy prob not
    private InventoryManager inventoryManager;
    
    void Start()
    {
        
        inventoryManager = InventoryManager.Instance;
        
    }
    
    /*
    private void OnMouseDown()
    {
        inventoryManager.AddItem(itemName, sprite, itemDescription);
        Destroy(gameObject);
    }
    */
    
     //TO COLLIDE WITH ITEMS
    private void OnTriggerStay2D(Collider2D other)
    {
        if(Input.GetMouseButton(0) && other.tag == "Mouse")
        {
            if(IsProgress != null)
            {
                IsProgress.onCLick();
            }
            inventoryManager.AddItem(itemID,itemName, sprite, itemDescription);
            Destroy(gameObject);
          
        }
        
    }
}
