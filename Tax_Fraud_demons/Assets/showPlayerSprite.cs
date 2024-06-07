using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class showPlayerSprite : MonoBehaviour
{
    UnityEngine.UI.Image pic;
    [SerializeField]
    
    Texture2D texture;
    Color col = new();

    private void Start()
    {
        
        pic = gameObject.GetComponent<UnityEngine.UI.Image>();
        col = pic.color;
        col.a = 0;
        pic.color = col;
        
    }
    // Start is called before the first frame update
    public void showSprite()
    {
        Debug.Log("sprite is being shown");
        col.a = 100;
        pic.color = col;
    }
    public void hideSprite()
    {
        col.a = 0;
        pic.color = col;
    }
}
