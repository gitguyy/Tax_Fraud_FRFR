using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sendInfo : MonoBehaviour
{
    InterrogationInformation interrogationInfo;
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

    void receiveInfo(InterrogationInformation info)
    {
        interrogationInfo = info;
        
    }
}
