using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    Button button;
    [SerializeField]
    SoundManager soundManager;
    
    void OnEnable()
    {
        if(soundManager == null)
        {
            soundManager = FindAnyObjectByType<SoundManager>();
        }
       
        button = GetComponent<Button>();
        button.onClick.RemoveListener(soundManager.PlayButtonSound);
        button.onClick.AddListener(soundManager.PlayButtonSound);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Start()
    {
        
    }
}
