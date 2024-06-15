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
    public UnityEvent<InterrogationInformation> giveInfo;
    sendInfo receiver;

    private void Start()
    {
        button.onClick.AddListener(OnCardClick);
        receiver = sendInfo.Instance;
    }

    private void OnCardClick()
    {
        //PlayerPrefs.SetString("SelectedPlayerID", playerData.playerID);
        Debug.Log(gameObject.name);
        giveInfo.AddListener(receiver.receiveInfo);
        giveInfo.Invoke(info);
        SceneManager.LoadScene("Interrogation");
       
        
    }
}
