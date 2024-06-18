using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject InventoryMenu;
    private bool menuActivated;
    public ItemSlot[] itemSlot; // use to deselect other things
    public GameObject useItemMenu;
    MySceneManager sceneLoader;
    bool isInterrogation;

    #region Singleton
    public static InventoryManager Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else Destroy(gameObject);
    }
    #endregion

    void Start()
    {
        sceneLoader = MySceneManager.Instance;
        menuActivated = InventoryMenu.activeSelf;
    }

    void Update()
    {
        if(sceneLoader.hasTransitioned == true)
        {
            if(FindObjectOfType<InterrogationLogic>()!= null)
            {
                isInterrogation = true;
            }
            else
            {
                isInterrogation = false;
            }
            sceneLoader.hasTransitioned = false;
        }
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

    public void clicked(ItemSlot Item)
    {
        if (isInterrogation)
        {
           
            useItemMenu.SetActive(true);
        }
    }
}