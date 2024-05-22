using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;


public class EventManager : MonoBehaviour
{

    public static EventManager Instance;

    private void Awake() // To make this class a singleton, there is only a single static instance in your scene
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public UnityEvent<int> OnEvent = new UnityEvent<int>();

    public void RaiseEvent(int i)
    {
        OnEvent.Invoke(i);
        Debug.Log("Raised Event");

        //You can use this function to have additional things happen when raising your event, but you can also directly invoke it elsewhere
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
