using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class ShowNPCSprite : MonoBehaviour
{
    [SerializeField]Sprite[] npcSprites;
    // Start is called before the first frame update
    Texture2D texture;
     Color col = new();
    Image pic;


    private void Start()
    {

        pic = gameObject.GetComponent<UnityEngine.UI.Image>();
        col = pic.color;
        col.a = 0;
        pic.color = col;

    }

    public void showNPC(int ID)
    {
        col.a = 100;
        pic.sprite = npcSprites[ID];
        
        pic.color = col;

        //Debug.Log("showing NPC" + col.a);
    }

    public void hideNPC()
    {

        
         
        col.a = 0;
        pic.color = col;
        //Debug.Log("hide NPC" + col.a);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
