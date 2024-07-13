using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using TMPro;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
 //======ITEM DATA======//
 public string itemName;
 public Sprite itemSprite;
 public bool isFull;
 public string itemDescription;
 public Sprite emptySprite;
 GameObject selectedItemMenu;
 public UnityEvent sendId;

    
 public int itemID; // Add itemID
 
 //======ITEM SLOT======//
 [SerializeField] private Image itemImage;

 //======ITEM DESCRIPTION SLOT======//
 public Image itemDescriptionImage;
 public TMP_Text ItemDescriptionNameText;
 public TMP_Text ItemDescriptionText;
 
 
 public GameObject selectedShader;
 public bool thisItemSelected;

 private InventoryManager inventoryManager; // refference to invMang

 private void Start()//ref
 {

        inventoryManager = InventoryManager.Instance;
       
 }
    

    public void AddItem(int itemID, string itemName, Sprite itemSprite, string itemDescription)
    {
        this.itemName = itemName;
        this.itemSprite = itemSprite;
        this.itemDescription = itemDescription;
        this.itemID = itemID;
  
        itemImage.sprite = itemSprite;
  isFull = true;
    }

 public void OnPointerClick(PointerEventData eventData)
 {
  if (eventData.button == PointerEventData.InputButton.Left)
  {
   OnLeftClick();
            if(isFull)
            {
                inventoryManager.clicked(this);
            }
  }

  if (eventData.button == PointerEventData.InputButton.Right)
  {
   OnRightClick();
  }
 }

 public void OnLeftClick()
 {
  inventoryManager.DeselectAllSlots();
  selectedShader.SetActive(true);
  thisItemSelected = true;
  // FOR INFO ON LEFT //
  ItemDescriptionNameText.text = itemName;
  ItemDescriptionText.text = itemDescription;
  itemDescriptionImage.sprite = itemSprite;
  // empty thingy
  if (itemDescriptionImage.sprite == null)
   itemDescriptionImage.sprite = emptySprite;
  
  // Play click sound
  SoundManager.instance.PlayItemSlotSound();
 }
 public void OnRightClick()
 {
  
 }
}
