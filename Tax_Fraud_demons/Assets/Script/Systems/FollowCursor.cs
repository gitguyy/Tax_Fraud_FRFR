using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FollowCursor : MonoBehaviour
{
    public Sprite cursorSprite; // The sprite to use for the cursor
    public LayerMask interactableLayer; // The layer that contains objects the cursor can interact with

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // Create a sprite renderer component for the cursor object
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = cursorSprite;
        spriteRenderer.sortingLayerName = "UI"; // Set the sorting layer to UI to ensure it's rendered on top
    }

    void Update()
    {
        // Update the cursor position based on mouse position
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        mousePos.z = 0f;
        transform.position = new Vector3(mousePos.x, mousePos.y, transform.position.z);
        
        if (Input.GetMouseButtonDown(0)) // Change the button as needed (e.g., 0 for left mouse button)
        {
            //Debug.Log("Mouse clicked at position: " + mousePos); // Log mouse position for debugging

            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity, interactableLayer);
            if (hit.collider != null)
            {
                // If the cursor interacts with an object, perform your desired action
                GameObject interactedObject = hit.collider.gameObject;
                Debug.Log("Interacted with: " + interactedObject.name);

                // Add the item to the inventory
                Item item = interactedObject.GetComponent<Item>();
                if (item != null)
                {
                    InventoryManager inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
                    inventoryManager.AddItem(item.ItemName, item.Sprite);
                    Destroy(interactedObject);
                }
            }
            else
            {
                //Debug.Log("No interactable object hit.");
            }
        }
        //add animation? or resize?
    }
}
