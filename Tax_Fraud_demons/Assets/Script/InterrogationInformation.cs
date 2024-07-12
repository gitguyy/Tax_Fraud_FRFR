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
    [SerializeField]
    public int ID;

    
    public TextAsset GetText()
    {
        return text;
    }

}
