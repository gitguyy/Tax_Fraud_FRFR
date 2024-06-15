using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sendInfo : MonoBehaviour
{
    public InterrogationInformation interrogationInfo;
    public static sendInfo Instance;
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void receiveInfo(InterrogationInformation info)
    {
        interrogationInfo = info;
        Debug.Log("info given");
        
    }
}
