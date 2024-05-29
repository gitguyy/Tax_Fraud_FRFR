using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private string itemName;
    [SerializeField] private Sprite sprite;
    //[SerializeField] private int quantity; if needed quantitfy prob not
    
    private InventoryManager inventoryManager;
    void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }
    
    public string ItemName
    {
        get { return itemName; }
    }

    public Sprite Sprite
    {
        get { return sprite; }
    }
}
