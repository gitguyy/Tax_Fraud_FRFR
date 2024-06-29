using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InterrogationInfo")]
public class InterrogationInformation : ScriptableObject
{


    [SerializeField]
    public RuntimeAnimatorController sprites;
    [SerializeField]
    private TextAsset text;


    public TextAsset GetText()
    {
        return text;
    }

    



}
