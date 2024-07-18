using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowNPCNames : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI nameMesh;
    [SerializeField]
    string text;
    [SerializeField]
    Vector3 offset;

    public GameObject box;
    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Mouse"))
        {
            //nameMesh.transform.position = cam.WorldToScreenPoint(transform.position + offset);
            nameMesh.text = text;
        }
        
       
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Mouse"))
        {
            //nameMesh.transform.position = cam.WorldToScreenPoint(transform.position + offset);
            nameMesh.text = text;
        }
        

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        nameMesh.text = "";
    }


}
