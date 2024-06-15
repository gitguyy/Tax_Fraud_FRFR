using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class PlayerCards : MonoBehaviour
{
    //public PlayerData playerData;
    public Button button;
    public InterrogationInformation info;
    public UnityEvent<InterrogationInformation> sendInfo;

    private void Start()
    {
        button.onClick.AddListener(OnCardClick);
       
    }

    private void OnCardClick()
    {
        //PlayerPrefs.SetString("SelectedPlayerID", playerData.playerID);
        Debug.Log(gameObject.name);
        
        sendInfo.Invoke(info);
        SceneManager.LoadScene("Interrogation");
       
        
    }
}
