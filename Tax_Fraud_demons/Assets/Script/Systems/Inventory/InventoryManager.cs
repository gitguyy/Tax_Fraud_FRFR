using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public GameObject InventoryMenu;
    private bool menuActivated;
    public ItemSlot[] itemSlot; // use to deselect other things
    public GameObject useItemMenu;
    MySceneManager sceneLoader;
    InterrogationLogic interrogation;

    bool isInterrogation;
    int itemID;
    

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
        InitializeUseMenu();

    }

    void InitializeUseMenu()
    {
        useItemMenu.SetActive(true);
        Transform secondChildTransform = useItemMenu.transform.GetChild(2);
        Button noButton;
        noButton = secondChildTransform.GetComponent<Button>();
        noButton.onClick.AddListener(this.DeactivateMenu);
        useItemMenu.SetActive(false);
    }

    void DeactivateMenu()
    {
        useItemMenu.SetActive(false);
    }



    void Update()
    {
        if(sceneLoader.hasTransitioned == true)
        {
            
            sceneLoader.hasTransitioned = false;
        }
        if (Input.GetButtonDown("Inventory"))
        {
            ToggleInventory();
        }
    }

    public void onTransition()
    {
        if (FindObjectOfType<InterrogationLogic>() != null)
        {
            SuspectAnimator anim = FindAnyObjectByType<SuspectAnimator>();

            interrogation = InterrogationLogic.Instance;
            isInterrogation = true;
            useItemMenu.SetActive(true);


            Transform secondChildTransform = useItemMenu.transform.GetChild(1);
            Button yesButton;
            yesButton = secondChildTransform.GetComponent<Button>();
            yesButton.onClick.RemoveAllListeners();
            yesButton.onClick.AddListener(checkItemId);
            anim.LoadAnimator();
            useItemMenu.SetActive(false);
        }
        else
        {
            Debug.Log("transitioned to 2d sidescroll");
            isInterrogation = false;
            useItemMenu.SetActive(false);
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
        itemID = Item.itemID;
        Debug.Log(itemID);
        if (isInterrogation)
        {
           
            useItemMenu.SetActive(true);

        }
    }

    public void checkItemId()
    {
        
        interrogation.checkForClueID(itemID);
        InterrogationInteraction getNextText = GameObject.FindAnyObjectByType<InterrogationInteraction>();
        DeactivateMenu();
        getNextText.spellNextText();
        
        InventoryMenu.SetActive(false);
    }

    
}