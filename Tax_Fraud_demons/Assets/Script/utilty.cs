using UnityEngine;

public class utilty : MonoBehaviour
{
    public string jsonToString(TextAsset text)
    {
        return text.text;
    }

    

    //public float LerpToValue(float timeItTakes, float goalTime)
    //{
    //    float timeElapsed = 0;
    //    float increaseAmount = 0;
    //    float value = 0;
    //    if (timeElapsed < timeItTakes)
    //    {
    //        timeElapsed += Time.deltaTime;

    //    }

    //    increaseAmount = (goalTime / timeItTakes);



    //    if (timeElapsed <= goalTime)
    //    {
    //        value += increaseAmount;
    //    }

    //    return value;

    //}



}
