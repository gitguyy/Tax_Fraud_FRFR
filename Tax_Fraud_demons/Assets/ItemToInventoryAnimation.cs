using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemToInventoryAnimation : MonoBehaviour
{
    #region Variables

    [SerializeField]
    private Transform goalTransform;

    public Transform origin;
   
    [SerializeField]
    GameObject item;
    GameObject copiedObject;
    public Sprite itemSprite;
  
    bool hasReachedGoal;
    [SerializeField]
    private float time;
    public float threshold = 10f;
    GameObject destroyObject;
    #endregion


    public void playAnimation(ref bool hasReached, ref Transform objectToMove)
    {
        Vector3 endTransform = Vector3.Lerp(objectToMove.position, goalTransform.position, time * Time.deltaTime);
        objectToMove.position = endTransform;
        
        if (Vector3.Distance(objectToMove.position, goalTransform.position) < threshold)
        {
            Debug.Log("destroy plis");
            hasReached = true;
            return;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            CreateItem(itemSprite, origin);
        }
    }

    public void CreateItem(Sprite sprite, Transform itemToSet)
    {
        
        GameObject copy = Instantiate(item, itemToSet.transform.position, Quaternion.identity);
        copy.GetComponent<AnimatedItem>().setSprite(sprite);
        copy.GetComponent<AnimatedItem>().anim = this;
       



    }
   

    

}
