using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryButtonController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject inventoryObject; // Assign your inventory GameObject in the Inspector
    private Button inventoryButton;
    private Image buttonImage;
    public Sprite openSprite;
    public Sprite closedSprite;
    private bool isInventoryOpen = false;

    void Start()
    {
        inventoryButton = GetComponent<Button>();
        buttonImage = inventoryButton.targetGraphic as Image;

        // Add listener for button click
        inventoryButton.onClick.AddListener(ToggleInventory);

        // Set initial state based on the inventory object
        isInventoryOpen = inventoryObject.activeSelf;
        SetButtonState(isInventoryOpen);
    }

    void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen;
        inventoryObject.SetActive(isInventoryOpen);
        SetButtonState(isInventoryOpen);
    }

    public void SetButtonState(bool isOpen)
    {
        if (isOpen)
        {
            buttonImage.sprite = openSprite;
        }
        else
        {
            buttonImage.sprite = closedSprite;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isInventoryOpen)
        {
            buttonImage.sprite = openSprite;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isInventoryOpen)
        {
            buttonImage.sprite = closedSprite;
        }
    }
}