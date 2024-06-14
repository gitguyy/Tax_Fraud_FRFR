using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InterrogationInfo")]
public class InterrogationInformation : ScriptableObject
{
    [SerializeField]
   
    Sprite[] npcSprites;
    [SerializeField]
    private TextAsset text;


    public TextAsset GetText()
    {
        return text;
    }

    public Sprite[] getSprites()
    {
        return npcSprites;
    }



}
