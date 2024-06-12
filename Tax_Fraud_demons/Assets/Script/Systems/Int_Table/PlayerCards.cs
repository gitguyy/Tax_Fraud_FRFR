using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerCards : MonoBehaviour
{
    //public PlayerData playerData;
    public Button button;

    private void Start()
    {
        button.onClick.AddListener(OnCardClick);
    }

    private void OnCardClick()
    {
        //PlayerPrefs.SetString("SelectedPlayerID", playerData.playerID);
        SceneManager.LoadScene("New Scene");
    }
}
