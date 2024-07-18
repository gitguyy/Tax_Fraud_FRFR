using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedItem : MonoBehaviour
{
    public ItemToInventoryAnimation anim;
    bool hasReachedGoal;
    SpriteRenderer spriteRenderer;
    Transform moveTransform;
    private void OnEnable()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        moveTransform = transform;
    }

    private void Update()
    {
        if(anim != null)
        {
            
            anim.playAnimation(ref hasReachedGoal, ref moveTransform);
            transform.position = moveTransform.position;
        }
       
        if(hasReachedGoal)
        {
            Debug.Log("why not destroy?");
            Destroy(this.gameObject);
        }
    }

    public void setSprite( Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }
}
