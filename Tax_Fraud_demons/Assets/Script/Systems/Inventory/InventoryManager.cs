using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject InventoryMenu;
    private bool menuActivated;
    public ItemSlot[] itemSlot; // use to deselect other things

    void Start()
    {
        menuActivated = InventoryMenu.activeSelf;
    }

    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            ToggleInventory();
        }
    }

    public void AddItem(int itemID,string itemName, Sprite itemSprite, string itemDescription)
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (!itemSlot[i].isFull)
            {
                itemSlot[i].AddItem(itemID, itemName, itemSprite, itemDescription);
                return;
            }
        }
    }

    public void DeselectAllSlots()
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            itemSlot[i].selectedShader.SetActive(false);
            itemSlot[i].thisItemSelected = false;
        }
    }

    public void ToggleInventory()
    {
        menuActivated = !menuActivated;
        InventoryMenu.SetActive(menuActivated);
    }
}